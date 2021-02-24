using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace DatAdmin
{
    public class DataLookupInfo
    {
        public DataTable Table;
        public int PkColIndex;
    }

    public interface IDataHolder
    {
        void GetData(IBedValueWriter writer);
        void SetData(IBedValueReader reader);
        TypeStorage GetTargetType();
        IBedValueConvertor BedConvertor { get; }
        DataLookupInfo LookupInfo { get; }
        bool IsReadOnly { get; }
    }

    public interface ICellDataEditor : IDockerFactoryCommon, IAddonInstance
    {
        CellDataFrameBase GetControl();
        /// <summary>
        /// returns support level for given data
        /// </summary>
        /// <param name="data">tested data</param>
        /// <returns>0: not supported, bigger value=>better support</returns>
        int SupportLevel(IDataHolder data, IBedValueReader holder);
    }

    public abstract class CellDataEditorBase : AddonBase, ICellDataEditor
    {
        CellDataFrameBase m_control;

        public override AddonType AddonType
        {
            get { return CellDataEditorAddonType.Instance; }
        }

        protected abstract CellDataFrameBase CreateControl();

        #region ICellDataEditor Members

        public CellDataFrameBase GetControl()
        {
            if (m_control == null)
            {
                m_control = CreateControl();
            }
            return m_control;
        }

        public abstract string MenuTitle
        {
            get;
        }

        public virtual Bitmap Icon
        {
            get { return null; }
        }

        public virtual string GetPersistString()
        {
            return XmlTool.GetRegisterAttr(this).Name;
        }

        public virtual DockerState InitialState
        {
            get { return DockerState.DockBottom; }
        }

        public Keys Shortcut
        {
            get { return Keys.None; }
        }

        public abstract int SupportLevel(IDataHolder data, IBedValueReader holder);

        #endregion
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class CellDataEditorAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class CellDataEditorAddonType : AddonType
    {
        public override string Name
        {
            get { return "celldataeditor"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ICellDataEditor); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(CellDataEditorAttribute); }
        }

        public static readonly CellDataEditorAddonType Instance = new CellDataEditorAddonType();
    }
}
