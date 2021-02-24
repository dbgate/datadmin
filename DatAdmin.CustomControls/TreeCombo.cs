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
  [ToolboxBitmap(typeof(UtilityLibrary.Combos.TreeCombo), "UtilityLibrary.Combos.TreeCombo.bmp")]
	public class TreeCombo : CustomCombo
	{
    #region Class Internal declarations
    public class EventArgsTreeDataFill : CustomCombo.EventArgsBindDropDownControl
    {
      new public TreeView BindedControl
      {
        get
        {
          return (TreeView)base.BindedControl;
        }
        set
        {
          base.BindedControl = value;
        }
      }


      public EventArgsTreeDataFill( CustomCombo.EventArgsBindDropDownControl ev )
        : base( ev.Combo, ev.DropDownForm, ev.BindedControl )
      {
      }
    }

    public delegate void FillTreeByDataHandler( object sender, EventArgsTreeDataFill e );
    #endregion

    #region Class Members
    protected TreeView  m_tree = new TreeView();
    protected ImageList m_imgList;

    private bool      m_bFillCalled;
    #endregion

    #region Class Events
    public event FillTreeByDataHandler DataFill;
    #endregion

    #region Class Properties
    [Browsable(true)]
    public TreeView TreeDropDown
    {
      get{ return m_tree; }
    }
    
    [Browsable(true)]
    public ImageList TreeImageList
    {
      get
      {
        return m_imgList;
      }
      set
      {
        if( value != m_imgList )
        {
          m_imgList = value;
          m_tree.ImageList = m_imgList;
        } 
      }
    }
    #endregion

    #region Class Constructos
		public TreeCombo()
		{
      m_tree.BorderStyle = BorderStyle.None;
      m_tree.ImageList = m_imgList; 
      m_tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( OnTreeItemChanged );
      base.CloseDropDown += new CustomCombo.CloseDropDownHandler( OnCloseDropDownHandler );
      base.CustomEditSize += new CustomCombo.EditControlResizeHandler( OnItemSizeCalculate );
		}
    #endregion

    #region Class Overrides
    protected override void OnDropDownControlBinding( CustomCombo.EventArgsBindDropDownControl e )
    {
      e.BindedControl = m_tree;
      m_tree.ImageList = m_imgList;
      RaiseFillTreeByData( e );
      
      // in case when we do data load on scroll message then 
      m_ctrlBinded = m_tree;
      m_bControlBinded = true;
    }
    #endregion

    #region Class helper methods
    public void UserRaiseFillData()
    {
      CustomCombo.EventArgsBindDropDownControl e = new CustomCombo.EventArgsBindDropDownControl( this, m_dropDownForm, m_tree );
      OnDropDownControlBinding( e );
    }

    protected void RaiseFillTreeByData( CustomCombo.EventArgsBindDropDownControl e )
    {
      if( DataFill != null && m_bFillCalled == false )
      {
        EventArgsTreeDataFill ev = new EventArgsTreeDataFill( e );
        m_tree.BeginUpdate();
        DataFill( this, ev );
        m_tree.EndUpdate();
        m_bFillCalled = true;
      }
    }
    #endregion

    #region Scroll and Value Set Support
    protected override void OnPrevScrollItems()
    {
      if( m_tree.SelectedNode == null )
      {
        if( m_tree.Nodes.Count == 0 && m_bFillCalled == false )
        {
          CustomCombo.EventArgsBindDropDownControl e = new CustomCombo.EventArgsBindDropDownControl( this, m_dropDownForm, m_tree );
          OnDropDownControlBinding( e );
        }
        
        if( m_tree.Nodes.Count > 0 )
        {
          m_tree.SelectedNode = m_tree.Nodes[0];
          base.Value = m_tree.SelectedNode.Text;
        }
      }
      else
      {
        m_tree.SelectedNode = FindPrevNode( m_tree );
        if( m_tree.SelectedNode != null )
        {
          base.Value = m_tree.SelectedNode.Text;
        }
      }
    }

    protected override void OnNextScrollItems()
    {
      if( m_tree.SelectedNode == null )
      {
        if( m_tree.Nodes.Count == 0 && m_bFillCalled == false )
        {
          CustomCombo.EventArgsBindDropDownControl e = new CustomCombo.EventArgsBindDropDownControl( this, m_dropDownForm, m_tree );
          OnDropDownControlBinding( e );
        }

        if( m_tree.Nodes.Count > 0 )
        {
          m_tree.SelectedNode = m_tree.Nodes[0];
          base.Value = m_tree.SelectedNode.Text;
        }
      }
      else
      {
        m_tree.SelectedNode = FindNextNode( m_tree );
        
        if( m_tree.SelectedNode != null )
        {
          base.Value = m_tree.SelectedNode.Text;
        }
      }
    }
    protected override void OnValueChanged()
    {
      // TODO: find item by value name
      base.OnValueChanged();
    }
    protected virtual void OnCloseDropDownHandler( object sender, EventArgsCloseDropDown e )
    {
      if( m_tree.SelectedNode != null )
      {
        base.Value = m_tree.SelectedNode.Text;
      }
    }
    #endregion
    
    #region Tree Works Helper methods
    public static TreeNode FindNextNode( TreeView tree )
    {
      if( tree != null && tree.Nodes.Count > 0 )
      {
        TreeNode node1 = tree.SelectedNode;
        TreeNode backup = tree.SelectedNode;

        if( node1 == null ) return null;
      
        if( node1.Nodes.Count > 0 ) // if we have child the show it first
        {
          return node1.Nodes[0];
        }
        else
        {
          TreeNode node2 = node1;
          
          while( node2 != null )
          {
            if( node2.Parent == null ) // if we on the top of tree
            {
              if( node2.Index < tree.Nodes.Count-1 ) // check can we select next node or not
              {
                return tree.Nodes[ node2.Index + 1 ];
              }
              else // we on the last node in tree
              {
                // if we on last child of tree node
                if( node2 != backup ) return backup;
                
                return node2; 
              }
            }
            else // if we have a parent node
            {
              node1 = node2.Parent;
              
              if( node2.Index < node1.Nodes.Count-1 ) // can we select next node
              {
                return node1.Nodes[ node2.Index + 1 ];
              }
              else // go to the parent
              {
                node2 = node1;
              }
            }
          }
        }
      }

      return null;
    }

    public static TreeNode FindPrevNode( TreeView tree )
    {
      if( tree != null && tree.Nodes.Count > 0 )
      {
        TreeNode node1 = tree.SelectedNode;
        TreeNode backup = tree.SelectedNode;

        if( node1 == null ) return null; // if no selected node in tree

        if( node1.Parent == null ) 
        {
          // if we not on first node
          if( node1.Index > 0 && node1.Index < tree.Nodes.Count )
          {
            TreeNode node2 = tree.Nodes[ node1.Index - 1 ];
            node1 = node2;
            
            // find last child of new selected node
            if( node2.Nodes.Count > 0 )
            {
              do
              {
                node1 = node1.Nodes[ node2.Nodes.Count - 1 ];
              }
              while( node1.Nodes.Count != 0  );
            }

            return node1;
          }
          else
          {
            return node1;
          }
        }
        else
        {
          // if we are not a first child of parent
          if( node1.Index > 0 && node1.Index < node1.Parent.Nodes.Count )
          {
            TreeNode node2 = node1.Parent.Nodes[ node1.Index - 1 ];
            node1 = node2;
            
            // find last child of new selected node
            if( node2.Nodes.Count > 0 )
            {
              do
              {
                node1 = node1.Nodes[ node2.Nodes.Count - 1 ];
              }
              while( node1.Nodes.Count != 0  );
            }

            return node1;
          }
          else // first child of parent
          {
            return node1.Parent;
          }
        }
      }

      return null;
    }

    public static TreeNode FindNodeByText( TreeNodeCollection nodes, string value )
    {
      TreeNode found = null;

      foreach( TreeNode node in nodes )
      {
        if( node.Text == value )
          return node;
        
        if ( node.Nodes.Count > 0 && ( ( found = FindNodeByText( node.Nodes, value ) ) != null ) ) 
          return found;
      }
      
      return null;
    }

    public static TreeNode FindNodeByTag( TreeNodeCollection nodes, object value )
    {
      TreeNode found = null;
      
      foreach( TreeNode node in nodes )
      {
        if ( node.Tag != null && node.Tag.Equals(value) )
          return node;
        
        if ( node.Nodes.Count > 0 && ( ( found = FindNodeByTag( node.Nodes, value ) ) != null ) ) 
          return found;

      }

      return null;
    }

    public TreeNode FindNodeByText( string value )
    {
      return FindNodeByText( m_tree.Nodes, value );
    }
    
    public TreeNode FindNodeByTag( object value )
    {
      return FindNodeByTag( m_tree.Nodes, value );
    }
    
    public bool SelectNodeByTag( object value )
    {
      m_tree.SelectedNode = FindNodeByTag( value );
      
      return (m_tree.SelectedNode != null);
    }
    #endregion

    #region Custom Draw
    protected virtual void OnItemSizeCalculate( object sender, CustomCombo.EventArgsEditCustomSize e )
    {
      if( m_imgList != null )
      {
        int iWidth = m_imgList.ImageSize.Width + 2;
        e.xPos  += iWidth;
        e.Width -= iWidth;
      }
    }

    protected override void OnPaintCustomData(System.Windows.Forms.PaintEventArgs pevent)
    {
      Graphics g = pevent.Graphics;
      Rectangle rc = pevent.ClipRectangle;

      if( m_tree.SelectedNode != null && m_imgList != null )
      {
        Rectangle rcOut = new Rectangle( rc.X + 2, rc.Y+2, m_imgList.ImageSize.Width, rc.Height - 4 );
        int index = m_tree.SelectedNode.ImageIndex;
        
        if( m_imgList.Images.Count > index && m_imgList.Images.Count > 0 )
        {
          if( index < 0 ) index = 0;
          Image img = m_imgList.Images[ index ];
          g.DrawImage( img, rcOut );
        }
      }
    }
    #endregion

    #region Event Handlers
    protected virtual void OnTreeItemChanged( object sender, TreeViewEventArgs e )
    {
      if( m_tree.SelectedNode != null )
      {
        base.Value = m_tree.SelectedNode.Text;
      }
    }
    

    #endregion
  }
}
