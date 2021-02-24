using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class RecordToDbAdapter : IRecordToDbAdapter
    {
        List<DbTypeBase> m_dstColTypes;
        IDialectDataAdapter m_dda;
        IBedValueConvertor m_outputConv;
        IProgressInfo m_progress;

        public RecordToDbAdapter(ITableStructure recordFormat, ITableStructure targetTable, ISqlDialect targetDialect, DataFormatSettings formatSettings)
        {
            m_dstColTypes = new List<DbTypeBase>();
            foreach (var col in recordFormat.Columns)
            {
                m_dstColTypes.Add(targetTable.Columns[col.ColumnName].DataType);
            }
            m_dda = targetDialect.CreateDataAdapter();
            m_outputConv = new BedValueConvertor(formatSettings);
        }

        public IProgressInfo ProgressInfo
        {
            get { return m_progress; }
            set
            {
                m_progress = value;
                m_outputConv.ProgressInfo = value;
            }
        }

        public IBedRecord AdaptRecord(IBedRecord record, ILogger logger)
        {
            ArrayDataRecord res = new ArrayDataRecord(record.Structure);
            for (int i = 0; i < res.FieldCount; i++)
            {
                record.ReadValue(i);
                res.SeekValue(i);
                m_dda.AdaptValue(record, m_dstColTypes[i], res, m_outputConv, logger);
            }
            return res;
        }
    }
}
