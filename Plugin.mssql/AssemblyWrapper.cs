using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using Microsoft.SqlServer.Server;
using System.Reflection;
using System.IO;
using System.Data.SqlTypes;

namespace Plugin.mssql
{
    public class UDAggregate
    {
        internal AssemblyWrapper m_wrapper;
        public SqlUserDefinedAggregateAttribute Attribute;
        public Type ClrType;

        public void GenerateRegister(ISqlDumper dmp)
        {
            dmp.PutCmd("^CREATE ^AGGREGATE %i (@Value nvarchar(4000)) ^RETURNS nvarchar(4000) ^EXTERNAL ^NAME %i.%i", Attribute.Name ?? ClrType.Name, m_wrapper.m_name, ClrType.FullName);
        }
    }

    public class UDFunction
    {
        internal AssemblyWrapper m_wrapper;
        public SqlFunctionAttribute Attribute;
        public MethodInfo Function;

        public void GenerateRegister(ISqlDumper dmp)
        {
            dmp.Put("^CREATE ^FUNCTION %i (", Attribute.Name ?? Function.Name);
            bool was = false;
            foreach (var arg in Function.GetParameters())
            {
                if (was) dmp.Put(", ");
                dmp.Put("@%s %s", arg.Name, AssemblyWrapper.GetSqlType(arg.ParameterType));
                was = true;
            }
            dmp.Put(") ^RETURNS %s", AssemblyWrapper.GetSqlType(Function.ReturnType));
            dmp.Put(" ^EXTERNAL ^NAME %i.%i.%i", m_wrapper.m_name, Function.DeclaringType.FullName, Function.Name);
            dmp.EndCommand();
        }
    }

    public class AssemblyWrapper
    {
        byte[] m_data;
        Assembly m_asm;
        internal string m_name;
        bool m_isScanned;
        List<UDAggregate> m_aggregates = new List<UDAggregate>();
        List<UDFunction> m_functions = new List<UDFunction>();

        public AssemblyWrapper(byte[] data, string name)
        {
            m_data = data;
            m_name = name;
            m_asm = Assembly.Load(m_data);
        }

        public static string GetSqlType(Type type)
        {
            DbTypeBase res;
            if (type == typeof(SqlString)) res = new DbTypeString();
            else if (type == typeof(SqlInt32)) res = new DbTypeInt { Bytes = 4 };
            else if (type == typeof(SqlInt16)) res = new DbTypeInt { Bytes = 2 };
            else if (type == typeof(SqlInt64)) res = new DbTypeInt { Bytes = 8 };
            else if (type == typeof(SqlDecimal)) res = new DbTypeNumeric();
            else res = TypeTool.GetDatAdminType(type);
            
            if (res is DbTypeString)
            {
                var stype = (DbTypeString)res;
                stype.Length = 4000;
                stype.IsUnicode = true;
            }
            return MsSqlDialect.Instance.GenericTypeToSpecific(res).ToString();
        }

        private void WantScan()
        {
            if (m_isScanned) return;

            foreach (var type in m_asm.GetTypes())
            {
                foreach (SqlUserDefinedAggregateAttribute attr in type.GetCustomAttributes(typeof(SqlUserDefinedAggregateAttribute), true))
                {
                    m_aggregates.Add(new UDAggregate
                    {
                        m_wrapper = this,
                        Attribute = attr,
                        ClrType = type,
                    });
                }
                foreach (var func in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
                {
                    foreach (SqlFunctionAttribute attr in func.GetCustomAttributes(typeof(SqlFunctionAttribute), true))
                    {
                        m_functions.Add(new UDFunction
                        {
                            m_wrapper = this,
                            Attribute = attr,
                            Function = func,
                        });
                    }
                }
            }

            m_isScanned = true;
        }

        public void GenerateDrop(ISqlDumper dmp)
        {
            dmp.WriteRaw(StdScripts.dropassembly.Replace("#ASMNAME#", m_name));
            dmp.EndCommand();
        }

        public void GenerateCreate(ISqlDumper dmp)
        {
            dmp.PutCmd("^create ^assembly %i ^from 0x%s ^with ^permission_set = ^safe", m_name, StringTool.EncodeHex(m_data));
        }

        public void GenerateRegisterAggr(ISqlDumper dmp)
        {
            WantScan();
            foreach (var aggr in m_aggregates)
            {
                aggr.GenerateRegister(dmp);
            }
        }

        public void GenerateRegisterFunc(ISqlDumper dmp)
        {
            WantScan();
            foreach (var func in m_functions)
            {
                func.GenerateRegister(dmp);
            }
        }
    }

    [CommandLineCommand(Name = "genasmsql", Description = "Generates MS SQL assembly creation script")]
    public class GenerateAssemblyScript : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        public class Instance : OutFileCommandInstanceBase
        {
            public Instance()
            {
            }

            [CommandLineParameter(Name = "infile", Description = "Assembly file (DLL)")]
            public string AsmFile { get; set; }

            [CommandLineParameter(Name = "skipdrop", Description = "Skip dropping old version of assembly")]
            public bool SkipDrop { get; set; }

            [CommandLineParameter(Name = "skipaggr", Description = "Skip registering aggregates")]
            public bool SkipAggr { get; set; }

            [CommandLineParameter(Name = "skipfunc", Description = "Skip registering functions")]
            public bool SkipFunc { get; set; }

            [CommandLineParameter(Name = "skipreg", Description = "Skip registering all objects")]
            public bool SkipRegister { get; set; }

            public override void RunCommand()
            {
                using (var fr = new FileInfo(AsmFile).OpenRead())
                {
                    var wrap = new AssemblyWrapper(fr.ReadAllBytes(), Path.GetFileNameWithoutExtension(AsmFile));
                    var fw = GetOutputStream();
                    var dmp = MsSqlDialect.Instance.CreateDumper(fw);
                    if (!SkipDrop)
                    {
                        wrap.GenerateDrop(dmp);
                    }
                    wrap.GenerateCreate(dmp);
                    if (!SkipRegister)
                    {
                        if (!SkipAggr)
                        {
                            wrap.GenerateRegisterAggr(dmp);
                        }
                        if (!SkipFunc)
                        {
                            wrap.GenerateRegisterFunc(dmp);
                        }
                    }
                }
            }
        }
    }
}
