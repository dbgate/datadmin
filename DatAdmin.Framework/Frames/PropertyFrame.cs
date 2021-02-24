using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DatAdmin
{
    public partial class PropertyFrame : UserControl
    {
        object m_selectedObject;
        Control m_customPropertyEditor;
        static Dictionary<string, string> m_lastState = new Dictionary<string, string>();
        Dictionary<object, Control> m_customEditorCache = new Dictionary<object, Control>();

        public PropertyFrame()
        {
            InitializeComponent();
            tabControl1.Visible = false;
            Disposed += new EventHandler(PropertyFrame_Disposed);
        }

        void PropertyFrame_Disposed(object sender, EventArgs e)
        {
            SaveObjectState();
            foreach (var ctrl in m_customEditorCache.Values)
            {
                ctrl.Dispose();
            }
            if (m_customPropertyEditor != null && !m_customPropertyEditor.IsDisposed)
            {
                m_customPropertyEditor.Dispose();
            }
        }

        public bool CacheCustomEditors { get; set; }

        public PropertyGrid Grid { get { return propertyGrid1; } }

        private static bool IsTabbed(object obj)
        {
            if (obj == null) return false;
            var type = obj.GetType();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                foreach (TabbedPropertyAttribute attr in prop.GetCustomAttributes(typeof(TabbedPropertyAttribute), true))
                {
                    return true;
                }
                foreach (TabbedEditorAttribute attr in prop.GetCustomAttributes(typeof(TabbedEditorAttribute), true))
                {
                    return true;
                }
            }
            return false;
        }

        private void SaveObjectState()
        {
            var obj = SelectedObject as IPropertyPageWithSerialization;
            if (obj == null) return;
            string key = obj.GetSerializationKey();
            if (key == null) return;
            string value = obj.GetSerializationValue();
            if (String.IsNullOrEmpty(value)) return;
            m_lastState[key] = value;
        }

        private void LoadObjectState()
        {
            var obj = SelectedObject as IPropertyPageWithSerialization;
            if (obj == null) return;
            string key = obj.GetSerializationKey();
            if (key == null) return;
            if (!m_lastState.ContainsKey(key)) return;
            obj.SetSerializedValue(m_lastState[key]);
        }

        public object SelectedObject
        {
            get { return m_selectedObject; }
            set
            {
                if (m_customPropertyEditor != null)
                {
                    Controls.Remove(m_customPropertyEditor);
                    if (CacheCustomEditors)
                    {
                        m_customEditorCache[m_selectedObject] = m_customPropertyEditor;
                    }
                    else
                    {
                        m_customPropertyEditor.Dispose();
                    }
                    m_customPropertyEditor = null;
                }
                SaveObjectState();
                m_selectedObject = value;
                LoadObjectState();
                tabControl1.TabPages.Clear();
                if (m_selectedObject is ICustomPropertyPage)
                {
                    m_customPropertyEditor = m_customEditorCache.Get(m_selectedObject, null);
                    if (m_customPropertyEditor == null)
                    {
                        m_customPropertyEditor = ((ICustomPropertyPage)m_selectedObject).CreatePropertyPage();
                    }
                    propertyGrid1.Visible = false;
                    tabControl1.Visible = false;
                    if (m_customPropertyEditor != null)
                    {
                        Translating.TranslateControl(m_customPropertyEditor);
                        m_customPropertyEditor.Dock = DockStyle.Fill;
                        Controls.Add(m_customPropertyEditor);
                    }
                }
                else
                {
                    if (IsTabbed(m_selectedObject))
                    {
                        propertyGrid1.Visible = false;
                        tabControl1.Visible = true;
                        DefaultPropertyTabAttribute defattr = null;
                        Type type = m_selectedObject.GetType();
                        foreach (DefaultPropertyTabAttribute attr in type.GetCustomAttributes(typeof(DefaultPropertyTabAttribute), true))
                        {
                            defattr = attr;
                        }
                        foreach (HideDefaultPropertyTabAttribute attr in type.GetCustomAttributes(typeof(HideDefaultPropertyTabAttribute), true))
                        {
                            defattr = null;
                        }
                        var pages = new List<TabPage>();
                        if (defattr != null)
                        {
                            var page = new TabPage(Texts.Get(defattr.PageTitle));
                            page.Tag = defattr.TabWeight;
                            PropertyGrid frm = new PropertyGrid();
                            page.Controls.Add(frm);
                            frm.Dock = DockStyle.Fill;
                            pages.Add(page);
                            frm.SelectedObject = m_selectedObject;
                        }
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            foreach (TabbedPropertyAttribute attr in prop.GetCustomAttributes(typeof(TabbedPropertyAttribute), true))
                            {
                                var page = new TabPage(Texts.Get(attr.PageTitle));
                                PropertyFrame frm = new PropertyFrame();
                                page.Controls.Add(frm);
                                page.Tag = attr.TabWeight;
                                frm.Dock = DockStyle.Fill;
                                pages.Add(page);
                                frm.SelectedObject = prop.GetGetMethod().Invoke(m_selectedObject, new object[] { });
                            }
                            foreach (TabbedEditorAttribute attr in prop.GetCustomAttributes(typeof(TabbedEditorAttribute), true))
                            {
                                if (attr.EnabledFunc != null)
                                {
                                    var func = type.GetMethod(attr.EnabledFunc);
                                    if (func == null) throw new InternalError(String.Format("DAE-00068 Enable function {0} doesn't exits in object {1}", attr.EnabledFunc, type.FullName));
                                    bool enabled = (bool)func.Invoke(m_selectedObject, new object[] { });
                                    if (!enabled) continue;
                                }
                                var editor = (ITabbedEditor)attr.EditorType.CreateNewInstance();
                                editor.LoadFromObject(m_selectedObject, prop);
                                var page = new TabPage(Texts.Get(editor.PageTitle));
                                var ctrl = (Control)editor;
                                page.Controls.Add(ctrl);
                                page.Tag = attr.TabWeight;
                                ctrl.Dock = DockStyle.Fill;
                                pages.Add(page);
                            }
                        }
                        pages.SortByKey(pg => -(int)pg.Tag);
                        tabControl1.TabPages.AddRange(pages.ToArray());
                    }
                    else
                    {
                        propertyGrid1.Visible = true;
                        tabControl1.Visible = false;
                        propertyGrid1.SelectedObject = value;
                    }
                }
            }
        }

        public void ReloadData()
        {
            propertyGrid1.Refresh();
        }

        public event PropertyValueChangedEventHandler PropertyValueChanged;

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (PropertyValueChanged != null) PropertyValueChanged(s, e);
        }

        public void FocusGrid()
        {
            try
            {
                foreach (GridItem item in propertyGrid1.SelectedGridItem.Parent.GridItems)
                {
                    propertyGrid1.Focus();
                    item.Select();
                    break;
                }
            }
            catch (Exception) { }
        }
    }
}
