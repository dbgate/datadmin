using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public abstract class DataFrameRowsExtractor
    {
        protected TableDataFrame m_frame;

        public abstract BedTable GetPreviewRows(int count);
        public abstract int GetRowCount();
        public abstract void LoadAllRows(Action<IBedRecord> forEachRow);

        public DataFrameRowsExtractor(TableDataFrame frame)
        {
            m_frame = frame;
        }
    }

    public class SelectedRowsExtractor : DataFrameRowsExtractor
    {
        public SelectedRowsExtractor(TableDataFrame frame)
            : base(frame)
        {
        }

        public override BedTable GetPreviewRows(int count)
        {
            return m_frame.GetSelectedTable().GetFirstRows(count);
        }

        public override void LoadAllRows(Action<IBedRecord> forEachRow)
        {
            foreach (var row in m_frame.GetSelectedTable().Rows) forEachRow(row);
        }

        public override int GetRowCount()
        {
            return m_frame.GetSelectedRowCount();
        }

        public override string ToString()
        {
            return Texts.Get("s_selected_rows");
        }
    }

    public class LoadedRowsExtractor : DataFrameRowsExtractor
    {
        public LoadedRowsExtractor(TableDataFrame frame)
            : base(frame)
        {
        }

        public override BedTable GetPreviewRows(int count)
        {
            return m_frame.m_table.GetFirstRows(count);
        }

        public override void LoadAllRows(Action<IBedRecord> forEachRow)
        {
            foreach (var row in m_frame.m_table.Rows) forEachRow(row);
        }

        public override int GetRowCount()
        {
            return m_frame.m_table.Rows.Count;
        }

        public override string ToString()
        {
            return Texts.Get("s_loaded_rows");
        }
    }

    public class FilteredRowsExtractor : DataFrameRowsExtractor
    {
        public FilteredRowsExtractor(TableDataFrame frame)
            : base(frame)
        {
        }

        public override BedTable GetPreviewRows(int count)
        {
            var props = m_frame.GetDataProperties();
            props.Count = count;
            return m_frame.TabularData.LoadTableData(props);
        }

        public override void LoadAllRows(Action<IBedRecord> forEachRow)
        {
            m_frame.TabularData.LoadAllRows(m_frame.GetDataProperties(), forEachRow);
        }

        public override int GetRowCount()
        {
            return m_frame.TabularData.LoadRowCount(new TableDataSetProperties()) ?? 0;
        }

        public override string ToString()
        {
            return Texts.Get("s_filtered_rows");
        }
    }

    public class AllRowsExtractor : DataFrameRowsExtractor
    {
        public AllRowsExtractor(TableDataFrame frame)
            : base(frame)
        {
        }

        public override BedTable GetPreviewRows(int count)
        {
            var props = new TablePageProperties { Count = count };
            return m_frame.TabularData.LoadTableData(props);
        }

        public override void LoadAllRows(Action<IBedRecord> forEachRow)
        {
            m_frame.TabularData.LoadAllRows(new TablePageProperties(), forEachRow);
        }

        public override int GetRowCount()
        {
            return m_frame.TabularData.LoadRowCount(new TableDataSetProperties()) ?? 0;
        }

        public override string ToString()
        {
            return Texts.Get("s_all_rows");
        }
    }

    public class DataFrameNoInputSqlGeneratorBase : DataSqlGeneratorBase
    {
        protected TableDataFrame m_frame;
        public DataFrameNoInputSqlGeneratorBase(TableDataFrame frame)
        {
            m_frame = frame;
        }

        public override bool IsRowEnumerator
        {
            get { return false; }
        }
    }

    public abstract class DataFrameSqlScriptGenerator : DataFrameNoInputSqlGeneratorBase
    {
        TableDataScript m_script;
        public DataFrameSqlScriptGenerator(TableDataFrame frame, TableDataScript script)
            : base(frame)
        {
            m_script = script;
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            ITabularDataView tabdata = m_frame.TabularData;
            if (tabdata == null) return;
            var props = m_frame.GetDataProperties();
            var sw = new StringWriter();
            tabdata.GenerateScript(m_script, props, dmp);
        }
    }

    public class DataFrameSelectSqlGenerator : DataFrameSqlScriptGenerator
    {
        public DataFrameSelectSqlGenerator(TableDataFrame frame)
            : base(frame, TableDataScript.Select)
        {
        }

        public override string ToString()
        {
            return "SELECT";
        }
    }

    public class DataFrameSaveChangesSqlGenerator : DataFrameNoInputSqlGeneratorBase
    {
        public DataFrameSaveChangesSqlGenerator(TableDataFrame frame)
            : base(frame)
        {
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            m_frame.TabularData.SaveChanges(m_frame.m_table, dmp);
        }

        public override string ToString()
        {
            return "SAVE CHANGES";
        }
    }
}
