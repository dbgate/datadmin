using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DatAdmin
{
    public partial class AddonSelectFrame : UserControl
    {
        string m_addonTypeName;
        AddonType m_addonType;
        AddonSpace m_space;
        string m_defaultAddonHolder;
        bool m_changingDesign;
        bool m_allowSwitchDesign = true;
        bool m_defaultUsed;
        bool m_compactDesign = true;
        bool m_canSaveAsTemplate = true;
        bool m_infoFrameVisible = true;
        Dictionary<string, int> m_addonHolderIndexes = new Dictionary<string, int>();

        public event GetSpecialItemsEvent GetSpecialItems;
        public event FilterAddonEvent FilterAddon;
        public event CreateSpecialItemEvent CreateSpecialItem;
        public event EventHandler ChangedSelectedObject;
        public event EventHandler CreatedObject;

        object[] m_objectCache;

        public AddonSelectFrame()
        {
            InitializeComponent();
            lbxTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        }

        [TypeConverter(typeof(AddonTypeNameTypeConverter))]
        public string AddonTypeName
        {
            get { return m_addonTypeName; }
            set
            {
                if (value == null) return;
                // must use registered instances
                bool changed = m_addonTypeName != value;
                m_addonTypeName = value;
                if (DesignMode) return;
                m_addonType = AddonRegister.Instance.FindAddonType(m_addonTypeName);
                Reload(changed);
            }
        }

        public string TypeTitle
        {
            get { return label1.Text; }
            set { label1.Text = label2.Text = value; }
        }

        public bool CompactDesign
        {
            get { return m_compactDesign; }
            set
            {
                m_changingDesign = true;
                int oldindex = CurrentList.SelectedIndex;
                ArrayList olditems = new ArrayList(CurrentItems);
                panelCompact.Visible = value;
                panelLarge.Visible = !value;
                m_compactDesign = value;
                CurrentItems.Clear();
                foreach (var obj in olditems) CurrentItems.Add(obj);
                CurrentList.SelectedIndex = oldindex;
                m_changingDesign = false;
            }
        }

        public bool ShowInfoFrame
        {
            get { return m_infoFrameVisible; }
            set
            {
                infoBoxFrame1.Visible = value;
                if (m_infoFrameVisible && !value)
                {
                    lbxTypes.Width += infoBoxFrame1.Width;
                }
                if (!m_infoFrameVisible && value)
                {
                    lbxTypes.Width -= infoBoxFrame1.Width;
                }
                m_infoFrameVisible = value;
            }
        }

        public bool AllowSwitchDesign
        {
            get { return m_allowSwitchDesign; }
            set
            {
                if (m_allowSwitchDesign && !value)
                {
                    lbxTypes.Width += btnSwitchDesign1.Width;
                    cbxTypes.Width += btnSwitchDesign2.Width;
                }
                if (!m_allowSwitchDesign && value)
                {
                    lbxTypes.Width -= btnSwitchDesign1.Width;
                    cbxTypes.Width -= btnSwitchDesign2.Width;
                }

                m_allowSwitchDesign = btnSwitchDesign2.Visible = btnSwitchDesign1.Visible = value;
            }
        }

        private void SetDefaultAddon()
        {
            if (m_defaultUsed) return;
            m_defaultUsed = true;

            if (m_defaultAddonHolder != null && m_addonHolderIndexes.ContainsKey(m_defaultAddonHolder.ToLower()))
            {
                int index = m_addonHolderIndexes[m_defaultAddonHolder.ToLower()];
                if (index < CurrentItems.Count)
                {
                    CurrentList.SelectedIndex = index;
                    if (ChangedSelectedObject != null) ChangedSelectedObject(this, EventArgs.Empty);
                }
            }
        }

        public string DefaultAddonHolder
        {
            get { return m_defaultAddonHolder; }
            set
            {
                m_defaultAddonHolder = value;
                if (DesignMode) return;
                if (Framework.MainWindow != null)
                {
                    Framework.MainWindow.RunInMainWindow(SetDefaultAddon);
                }
            }
        }

        private ListControl CurrentList
        {
            get
            {
                if (CompactDesign) return cbxTypes;
                else return lbxTypes;
            }
        }

        private IList CurrentItems
        {
            get
            {
                if (CompactDesign) return cbxTypes.Items;
                else return lbxTypes.Items;
            }
        }

        public void Reload(bool changedAddonType)
        {
            if (DesignMode) return;
            object oldSelected = propertyFrame1.SelectedObject;
            Dictionary<string, object> oldCache = new Dictionary<string, object>();
            if (m_objectCache != null && m_addonHolderIndexes != null && !changedAddonType)
            {
                foreach (string key in m_addonHolderIndexes.Keys)
                {
                    int index = m_addonHolderIndexes[key];
                    if (index >= 0 && index < m_objectCache.Length) oldCache[key] = m_objectCache[index];
                }
            }
            CurrentItems.Clear();
            m_addonHolderIndexes.Clear();
            if (m_addonType != null)
            {
                m_space = m_addonType.CommonSpace;
                if (GetSpecialItems != null)
                {
                    var ev = new GetSpecialItemsEventArgs();
                    GetSpecialItems(this, ev);
                    foreach (var s in ev.SpecialItems) CurrentItems.Add(new SpecialWrap { Name = s });
                }
                foreach (var holder in GetItems())
                {
                    m_addonHolderIndexes[holder.Name.ToLower()] = CurrentItems.Count;
                    if (String.IsNullOrEmpty(holder.ToString())) throw new InternalError("DAE-00067 Addon " + holder.Name + " has not description");
                    CurrentItems.Add(holder);
                }
                m_objectCache = new object[CurrentItems.Count];
                foreach (string key in oldCache.Keys)
                {
                    if (m_addonHolderIndexes.ContainsKey(key))
                    {
                        m_objectCache[m_addonHolderIndexes[key]] = oldCache[key];
                    }
                }
            }
            else
            {
                m_space = null;
            }
            SelectObject(oldSelected, false);
            if (SelectedObject == null && m_defaultAddonHolder != null && m_addonHolderIndexes.ContainsKey(m_defaultAddonHolder.ToLower()))
            {
                CurrentList.SelectedIndex = m_addonHolderIndexes[m_defaultAddonHolder.ToLower()];
            }
        }

        private void SelectObject(object value, bool canAddOther)
        {
            if (value == null)
            {
                CurrentList.SelectedIndex = -1;
                return;
            }
            string type = AddonTool.ExtractAddonName(value);
            if (type != null)
            {
                if (m_addonHolderIndexes.ContainsKey(type.ToLower()))
                {
                    int index = m_addonHolderIndexes[type.ToLower()];
                    m_objectCache[index] = value;
                    CurrentList.SelectedIndex = index;
                    // call on change event, even if index is the same
                    CurrentList_SelectedIndexChanged(this, EventArgs.Empty);
                    return;
                }
            }
            if (canAddOther)
            {
                object[] newcache = new object[m_objectCache.Length + 1];
                Array.Copy(m_objectCache, newcache, m_objectCache.Length);
                m_objectCache = newcache;
                CurrentItems.Add(value);
                m_objectCache[m_objectCache.Length - 1] = value;
                CurrentList.SelectedIndex = m_objectCache.Length - 1;
            }
        }

        private IEnumerable<AddonHolder> GetItems()
        {
            if (m_space == null) yield break;
            foreach (var item in m_space.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                if (FilterAddon != null)
                {
                    try
                    {
                        var ev = new FilterAddonEventArgs { InstanceModel = item.InstanceModel };
                        FilterAddon(this, ev);
                        if (ev.Skip) continue;
                    }
                    catch (Exception err)
                    {
                        Logging.Warning("Error loading addon {0}: {1}", item.Name, err);
                        continue;
                    }
                }
                yield return item;
            }
        }

        [Browsable(false)]
        public object SelectedObject
        {
            get
            {
                return propertyFrame1.SelectedObject;
            }
        }

        public void SelectObject(object value)
        {
            SelectObject(value, true);
        }

        public bool CanSaveAsTemplate
        {
            get { return m_canSaveAsTemplate; }
            set
            {
                btsaveastemplate.Visible = value;
                button1.Visible = value;
                if (m_canSaveAsTemplate && !value)
                {
                    cbxTypes.Width += btsaveastemplate.Width;
                }
                if (!m_canSaveAsTemplate && value)
                {
                    cbxTypes.Width -= btsaveastemplate.Width;
                }
                m_canSaveAsTemplate = value;
            }
        }

        protected virtual void OnChangedSelectedObject(EventArgs ev)
        {
            infoBoxFrame1.InfoText = AddonTool.ExtractDesctiption(SelectedObject) ?? "";
            if (ChangedSelectedObject != null) ChangedSelectedObject(this, EventArgs.Empty);
        }

        protected virtual void OnCreatedObject(object sender)
        {
            if (CreatedObject != null) CreatedObject(sender, EventArgs.Empty);
        }

        private void CurrentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_changingDesign) return;
            if (DesignMode) return;

            if (CurrentList.SelectedIndex < 0)
            {
                propertyFrame1.SelectedObject = null;
                OnChangedSelectedObject(EventArgs.Empty);
            }
            else
            {
                if (m_objectCache[CurrentList.SelectedIndex] == null)
                {
                    var holder = CurrentItems[CurrentList.SelectedIndex] as AddonHolder;
                    var spec = CurrentItems[CurrentList.SelectedIndex] as SpecialWrap;
                    if (holder != null)
                    {
                        m_objectCache[CurrentList.SelectedIndex] = holder.CreateInstance();
                        OnCreatedObject(m_objectCache[CurrentList.SelectedIndex]);
                    }
                    if (spec != null)
                    {
                        CreateSpecialItemEventArgs ev = new CreateSpecialItemEventArgs();
                        ev.SpecialItem = spec.Name;
                        CreateSpecialItem(this, ev);
                        m_objectCache[CurrentList.SelectedIndex] = ev.Instance;
                    }
                }
                propertyFrame1.SelectedObject = m_objectCache[CurrentList.SelectedIndex];
                OnChangedSelectedObject(EventArgs.Empty);
            }
            btsaveastemplate.Enabled = SelectedObject is IAddonInstance;
        }

        private string SelectedTemplateFileName
        {
            get
            {
                if (CurrentList.SelectedIndex < 0) return null;
                var s = CurrentItems[CurrentList.SelectedIndex].ToString();
                if (s.ToLower().EndsWith(".adx")) return s.Substring(0, s.Length - 4);
                return null;
            }
        }

        private void btsaveastemplate_Click(object sender, EventArgs e)
        {
            string tpl = m_addonType.SaveCommonTemplate((IAddonInstance)SelectedObject, SelectedTemplateFileName);
            if (tpl != null)
            {
                tpl = tpl.ToLower();
                if (m_addonHolderIndexes.ContainsKey(tpl))
                {
                    CurrentList.SelectedIndex = m_addonHolderIndexes[tpl];
                }
                else
                {
                    Reload(false);
                    if (m_addonHolderIndexes.ContainsKey(tpl))
                    {
                        CurrentList.SelectedIndex = m_addonHolderIndexes[tpl];
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CompactDesign = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CompactDesign = false;
        }

        public void SelectObjectImportant(object value)
        {
            m_defaultUsed = true;
            SelectObject(value);
        }

        private void panelLarge_Resize(object sender, EventArgs e)
        {
            lbxTypes.Left = 8;
            if (ShowInfoFrame)
            {
                lbxTypes.Width = Width - 3 * lbxTypes.Left - infoBoxFrame1.Width;
                infoBoxFrame1.Left = lbxTypes.Right + lbxTypes.Left;
                infoBoxFrame1.Width = Width - infoBoxFrame1.Left - lbxTypes.Left;
            }
            else
            {
                lbxTypes.Width = Width - lbxTypes.Left;
            }
        }
    }

    class SpecialWrap
    {
        internal string Name;

        public override string ToString()
        {
            return Texts.Get(Name);
        }
    }

    public class GetSpecialItemsEventArgs : EventArgs
    {
        public List<string> SpecialItems = new List<string>();
    }

    public delegate void GetSpecialItemsEvent(object sender, GetSpecialItemsEventArgs e);

    public class FilterAddonEventArgs : EventArgs
    {
        public object InstanceModel;
        public bool Skip = false;
    }

    public delegate void FilterAddonEvent(object sender, FilterAddonEventArgs e);

    public class CreateSpecialItemEventArgs : EventArgs
    {
        public string SpecialItem;
        public object Instance;
    }

    public delegate void CreateSpecialItemEvent(object sender, CreateSpecialItemEventArgs e);
}
