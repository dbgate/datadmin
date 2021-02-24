using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    [JobCommand(Name = "bulkcopy")]
    public class BulkCopyJobCommand : JobCommand
    {
        ITabularDataStore m_source;
        ITabularDataStore m_target;
        IRowTransform m_transform;
        XmlElement m_transformXml;
        TableCopyOptions m_copyOptions = new TableCopyOptions();

        ITableStructure m_sourceStruct;
        ITableStructure m_targetStruct;

        ICancelable m_cancelSrc;
        ICancelable m_cancelDst;

        public BulkCopyJobCommand() { }

        public BulkCopyJobCommand(ITabularDataStore source, ITabularDataStore target, IRowTransform rowTransform, TableCopyOptions copyOpts)
        {
            m_source = source;
            m_target = target;
            m_transform = rowTransform;
            m_copyOptions = copyOpts;
        }

        protected override void DoRun(IJobRunEnv env)
        {
            m_source.Mode = TabularDataStoreMode.Read;
            m_target.Mode = TabularDataStoreMode.Write;
            m_target.CopyOptions = m_copyOptions;

            Async.SafeOpen(m_source.Connection);
            Async.SafeOpen(m_target.Connection);

            IAsyncResult asyncs = m_source.BeginGetRowFormat(null);
            m_sourceStruct = m_source.EndGetRowFormat(asyncs);

            IAsyncResult asynct = m_target.BeginGetRowFormat(null);
            m_targetStruct = m_target.EndGetRowFormat(asynct);
            var targetFull = m_targetStruct;

            if (m_transform == null)
            {
                m_transform = RowTransformAddonType.Instance.LoadRowTransform(m_transformXml, m_sourceStruct, m_targetStruct);
                if (!m_target.AvailableRowFormat)
                {
                    m_target.SetRowFormat(m_transform.OutputFormat);
                    m_targetStruct = m_transform.OutputFormat;
                }
            }

            GenericDataQueue queue = new GenericDataQueue(m_sourceStruct, m_transform.OutputFormat, m_transform);

            if (m_target.Connection != null && m_target.Connection.SystemConnection != null)
            {
                var fi = m_source as IDataFormatHolder;
                var fmt = fi != null ? fi.FormatSettings : new DataFormatSettings();
                var outputAdapter = new RecordToDbAdapter(m_transform.OutputFormat, targetFull, m_target.Connection.Dialect, fmt);
                outputAdapter.ProgressInfo = ProgressInfo;
                queue.AddOutputAdapter(outputAdapter);
            }

            m_source.ProgressInfo = ProgressInfo;
            m_target.ProgressInfo = ProgressInfo;

            IAsyncResult async_src = m_source.BeginRead(null, queue);
            IAsyncResult async_dst = m_target.BeginWrite(null, queue);
            if (async_src is ICancelable) m_cancelSrc = (ICancelable)async_src;
            if (async_dst is ICancelable) m_cancelDst = (ICancelable)async_dst;
            try
            {
                m_source.EndRead(async_src);
            }
            finally
            {
                m_target.EndWrite(async_dst);
            }

            Async.SafeClose(m_source.Connection);
            Async.SafeClose(m_target.Connection);
        }

        public override void Cancel()
        {
            try { m_cancelSrc.Cancel(); }
            catch { }
            try { m_cancelDst.Cancel(); }
            catch { }
            Async.SafeClose(m_source.Connection);
            Async.SafeClose(m_target.Connection);
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_source = (ITabularDataStore)TabularDataStoreAddonType.Instance.LoadAddon(xml.FindElement("Source"));
            m_target = (ITabularDataStore)TabularDataStoreAddonType.Instance.LoadAddon(xml.FindElement("Target"));
            m_transformXml = xml.FindElement("Transform");
            m_copyOptions = new TableCopyOptions();
            m_copyOptions.LoadFromXml(xml.FindElement("CopyOptions"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "bulkcopy");
            m_source.SaveToXml(XmlTool.AddChild(xml, "Source"));
            m_target.SaveToXml(XmlTool.AddChild(xml, "Target"));
            if (m_transform == null)
            {
                xml.AppendChild(xml.OwnerDocument.ImportNode(m_transformXml, true));
            }
            else
            {
                m_transform.SaveToXml(XmlTool.AddChild(xml, "Transform"));
            }
            m_copyOptions.SaveToXml(xml.AddChild("CopyOptions"));
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", m_source, m_target);
        }

        public override string TypeTitle
        {
            get { return "s_copy"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.copy; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("s_source")]
        public ITabularDataStore Source { get { return m_source; } }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("s_target")]
        public ITabularDataStore Target { get { return m_target; } }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IRowTransform Transform { get { return m_transform; } }

        public override void GetUsageParams(UsageBuilder ub)
        {
            ub["src"] = m_source.SafeToString();
            ub["dst"] = m_target.SafeToString();
        }
    }

    public static class BulkCopyJob
    {
        public static Job Create(ITabularDataStore source, ITabularDataStore target, IRowTransform rowTransform, TableCopyOptions copyOpts, JobProperties jobProps)
        {
            return Job.FromCommand(new BulkCopyJobCommand(source, target, rowTransform, copyOpts), jobProps);
        }
    }
}

