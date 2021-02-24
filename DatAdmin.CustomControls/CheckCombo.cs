using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UtilityLibrary.Win32;
using UtilityLibrary.General;


namespace UtilityLibrary.Combos
{	
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof(UtilityLibrary.Combos.CheckCombo), "UtilityLibrary.Combos.CheckCombo.bmp")]
	public class CheckCombo : TreeCombo
	{
    #region Class members
    ArrayList m_checked = new ArrayList( 20 );
    #endregion

    #region Class Properties
    [Browsable(false)]
    public ArrayList CheckedItems
    {
      get
      {
        return m_checked;
      }
    }
    #endregion

    #region Constructor
		public CheckCombo()
    {
		}
    #endregion

    #region Class Overrides
    protected override void OnDropDownControlBinding( CustomCombo.EventArgsBindDropDownControl e )
    {
      base.OnDropDownControlBinding( e );
      
      TreeView tree = (TreeView)e.BindedControl;
      tree.CheckBoxes = true;
      tree.FullRowSelect = true;
      tree.HideSelection = false;
      tree.ShowLines = false;
      tree.ShowPlusMinus = false;
      tree.ShowRootLines = false;
      tree.Sorted = true;
      
      tree.AfterCheck += new TreeViewEventHandler( tree_AfterCheck );
    }

    protected override void OnCloseDropDownHandler( object sender, CustomCombo.EventArgsCloseDropDown e )
    {
      base.Value = CalculateValue();
    }

    protected override bool OnValueValidate( string value )
    {
      // TODO: add here parsing of setted value
      return true;
    }
    #endregion

    #region Class Helper methods
    private string CalculateValue()
    {
      string outString = "";
      
      foreach( TreeNode node in m_checked )
      {
        outString += node.Text;
        outString += ", ";
      }

      if( outString.Length > 2 )
        outString = outString.Remove( outString.Length - 2, 2 );
      
      return outString;
    }

    private void tree_AfterCheck( object sender, System.Windows.Forms.TreeViewEventArgs e )
    {
      if( e.Node.Checked == true )
      {
        if( m_checked.Contains( e.Node ) == false )
          m_checked.Add( e.Node );
      }
      else
      {
        if( m_checked.Contains( e.Node ) == true )
          m_checked.Remove( e.Node );
      }

      base.Value = CalculateValue();
    }
    #endregion

    #region Class Public methods
    public void CheckAll()
    {
      BeginUpdate();
      foreach( TreeNode node in m_tree.Nodes )
      {
        node.Checked = true;
      } 
      EndUpdate();
      Invalidate();
    }
    
    public void UnCheckAll()
    {
      BeginUpdate();
      foreach( TreeNode node in m_tree.Nodes )
      {
        node.Checked = false;
      } 
      EndUpdate();
      Invalidate();
    }
    #endregion

    protected override void OnTreeItemChanged(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {
      // do nothing here for correct update of data
    }

	}
}
