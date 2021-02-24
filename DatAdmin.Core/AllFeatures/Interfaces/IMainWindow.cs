using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Xml;
using System.Drawing;

namespace DatAdmin
{
    public class OpenQueryParameters
    {
        public Func<IPhysicalConnection, string> GenerateSql = null;
        //public string Filename = null;
        public bool GeneratingSql = false;
        public bool DisableContext = false;
        public IVirtualFile File = null;
        public string SqlText = null;
        public XmlDocument SavedContext = null;
        public XmlDocument SavedDesign = null;
        public bool HideDesign = false;
        public bool GoToDesign = false;
        public FullDatabaseRelatedName AddDesignTable = null;
        public Action ExecutedCallback = null;
    }

    public class ObjectEditorPars
    {
        public Action SavedCallback = null;
    }

    public class AlterTableEditorPars : ObjectEditorPars
    {
        public enum Tab { Columns, IndexesKeys, ForeignKeys, Checks }
        public Tab InitialTab = Tab.Columns;
    }

    public class ShowDockerPars
    {
        /// <summary>
        /// if true, docker behaves a bit like modal window
        /// if it is not visible, it gots focus and it is closed after pressing esc
        /// </summary>
        public bool ModalLikeMode;
        /// <summary>
        /// which control should be activated after pressing Esc
        /// </summary>
        public Control ModalParent;
    }

    //public enum CommandType {SingleSelect
    public interface IMainWindow : IFrameworkMainWindow
    {
        void OpenContent(ContentFrame content);
        void OpenContent(ContentFrame content, DocumentDockPosition position);
        void CloseContent(ContentFrame contentFrame);
        ContentFrame GetCurrentContent();
        List<ContentFrame> GetContents();
        void ShowContent(ContentFrame frame, bool visible);
        void ActivateContent(ContentFrame frame);
        void CloseAllContents();

        void ActivateDocker(DockerBase docker);
        void ShowDocker(IDockerFactory fact);
        void ShowDocker(IDockerFactory fact, ShowDockerPars pars);

        Form Window { get;}

        void UpdateContentTitle(ContentFrame contentFrame);
        void UpdateFrameEnabling(ContentFrame contentFrame);
        void CreateNewConnectionDialog();

        void SetLoadingFrame(ContentFrame frame, bool isloading);
    }

    public static class MainWindow
    {
        static IMainWindow m_instance;

        public static IMainWindow Instance
        {
            get { return m_instance; }
            set
            {
                m_instance = value;
                Framework.MainWindow = value;
            }
        }
    }

    public class ValueDataHolder : IDataHolder
    {
        object m_data;
        BedValueConvertor m_convertor = new BedValueConvertor(new DataFormatSettings());

        public ValueDataHolder(object data)
        {
            m_data = data;
        }

        public void GetData(IBedValueWriter writer) { writer.ReadFrom(m_data); }
        public void SetData(IBedValueReader reader) { }
        public TypeStorage GetTargetType() { return TypeStorage.Null; }
        public IBedValueConvertor BedConvertor { get { return m_convertor; } }
        public DataLookupInfo LookupInfo { get { return null; } }
        public bool IsReadOnly { get { return true; } }
    }

    public enum CodeLanguage { None, Sql, Python, Template }

    public interface IExplicitSaveableObject
    {
        void ExplicitSave();
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ShowInGridAttribute : Attribute
    {
        public int Order = 0;
    }
}
