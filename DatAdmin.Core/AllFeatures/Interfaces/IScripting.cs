using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public enum TextGeneratorLanguage
    {
        Template,
        Python,
    }

    //public class CommandAttribute : RegisterAttribute
    //{
    //}

    //public interface ICommand
    //{
    //    //object GetDynamicProperties();
    //    //IAsyncResult BeginRunCommand(ITreeNode node, object dynamicProperties);
    //    //void EndRunCommand(IAsyncResult async);
    //    void AddToMenu(MenuBuilder mb, ITreeNode node);
    //}

    //public interface IAppObjectSqlGenerator
    //{
    //    void GenerateSql(AppObject appobj, TextWriter tw, ISqlDialect dialectOverride);
    //    string Title { get;}
    //}

    //public class FullDatabaseRelatedName
    //{
    //    public string ObjectType;
    //    public NameWithSchema ObjectName;
    //    public string SubName;
    //}

    //public interface ISqlGenerator
    //{
    //    void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect);
    //}

    ////public enum SqlGeneratorType { Server, Database, Table, Column, Constraint, SpecObject };

    //[AttributeUsage(AttributeTargets.Class)]
    //public class SqlGeneratorAttribute : RegisterAttribute
    //{
    //    //public SqlGeneratorType TargetObjectType { get; set; }
    //}

    //[AddonType]
    //public class CommandAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "command"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(ICommand); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(CommandAttribute); }
    //    }

    //    public static readonly CommandAddonType Instance = new CommandAddonType();
    //}

    //[AddonType]
    //public class SqlGeneratorAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "sqlgenerator"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(ISqlGenerator); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(SqlGeneratorAttribute); }
    //    }

    //    public static readonly SqlGeneratorAddonType Instance = new SqlGeneratorAddonType();
    //}

    //public delegate void GenerateNodeSqlDelegate(AppObject appobj, TextWriter tw, ISqlDialect dialectOverride);

    //public class DelegateSqlGenerator : IAppObjectSqlGenerator
    //{
    //    string m_title;
    //    GenerateNodeSqlDelegate m_func;
    //    internal DelegateSqlGenerator(string title, GenerateNodeSqlDelegate func)
    //    {
    //        m_title = title;
    //        m_func = func;
    //    }
    //    #region ISqlGenerator Members

    //    public void GenerateSql(AppObject appobj, TextWriter tw, ISqlDialect dialectOverride)
    //    {
    //        m_func(appobj, tw, dialectOverride);
    //    }

    //    public string Title
    //    {
    //        get { return m_title; }
    //    }

    //    #endregion
    //}

}
