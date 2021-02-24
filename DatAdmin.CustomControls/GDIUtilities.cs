using System;
using System.Collections;
using System.Drawing.Text;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.IO;

namespace UtilityLibrary.Wizards.Utility
{
  #region 3D Styles Enums
  public enum Canvas3DStyle
  {
    Single,
    Raised,
    Upped,
    Title,
    Flat
  }

  public enum HightlightStyle
  {
    Active,
    Selected
  }
  #endregion

  public sealed class GDIUtils
  {
    #region Class Members
    StringFormat format = new StringFormat();

    // store brushes to optimize memory managment
    private SolidBrush m_brushDark = null;
    private SolidBrush m_brushDarkDark = null;
    private SolidBrush m_brushLight = null;
    private SolidBrush m_brushLightLight = null;

    // colors on which brushes are based
    private Color m_clrDark;
    private Color m_clrDarkDark;
    private Color m_clrLight;
    private Color m_clrLightLight;

    // pens used by class for drawing
    Pen m_penDark = null;
    Pen m_penDarkDark = null;
    Pen m_penLight = null;
    Pen m_penLightLight = null;
    #endregion

    #region Class Properties
    public Color Dark
    {
      get{ return m_clrDark; }
      set
      {
        if( value == m_clrDark ) return;
        m_clrDark = value;
        
        // destoroy old values
        if( m_brushDark != null ) m_brushDark.Dispose();
        if( m_penDark != null ) m_penDark.Dispose();

        m_brushDark = new SolidBrush( m_clrDark );
        m_penDark = new Pen( m_brushDark );
      }
    }
    public Color DarkDark
    {
      get{ return m_clrDarkDark; }
      set
      {
        if( value == m_clrDarkDark ) return;
        m_clrDarkDark = value;

        // destoroy old values
        if( m_brushDarkDark != null ) m_brushDarkDark.Dispose();
        if( m_penDarkDark != null ) m_penDarkDark.Dispose();

        m_brushDarkDark = new SolidBrush( m_clrDarkDark );
        m_penDarkDark = new Pen( m_brushDarkDark );
      }
    }
    public Color Light
    {
      get{ return m_clrLight; }
      set
      {                         
        if( value == m_clrLight ) return;
        m_clrLight = value;

        // destoroy old values
        if( m_brushLight != null ) m_brushLight.Dispose();
        if( m_penLight != null ) m_penLight.Dispose();

        m_brushLight = new SolidBrush( m_clrLight );
        m_penLight = new Pen( m_brushLight );
      }
    }
    public Color LightLight
    {
      get{ return m_clrLightLight; }
      set
      {
        if( value == m_clrLightLight ) return;
        m_clrLightLight = value;

        // destoroy old values
        if( m_brushLightLight != null ) m_brushLightLight.Dispose();
        if( m_penLightLight != null ) m_penLightLight.Dispose();

        m_brushLightLight = new SolidBrush( m_clrLightLight );
        m_penLightLight = new Pen( m_brushLightLight );
      }
    }
    

    public Brush DarkBrush{ get{ return m_brushDark; } }
    public Brush DarkDarkBrush{ get{ return m_brushDarkDark; } }
    public Brush LightBrush{ get{ return m_brushLight; } }
    public Brush LightLightBrush{ get{ return m_brushLightLight; } }


    public Pen DarkPen{ get{ return m_penDark; } }
    public Pen DarkDarkPen{ get{ return m_penDarkDark; } }
    public Pen LightPen{ get{ return m_penLight; } }
    public Pen LightLightPen{ get{ return m_penLightLight; } }

    public StringFormat OneLineFormat
    {
      get
      {
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        format.Trimming = StringTrimming.EllipsisCharacter;
        format.FormatFlags = StringFormatFlags.LineLimit;
        format.HotkeyPrefix = HotkeyPrefix.Show;

        return format;
      }
    }

    public StringFormat OneLineNoTrimming
    {
      get
      {
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        format.Trimming = StringTrimming.None;
        format.FormatFlags = StringFormatFlags.LineLimit;
        format.HotkeyPrefix = HotkeyPrefix.Show;

        return format;
      }
    }
    #endregion

    #region Initialize/Finilize functions
    /// <summary>
    /// Default Constructor 
    /// </summary>
    public GDIUtils()
    {
      this.Dark = SystemColors.ControlDark;
      this.DarkDark = SystemColors.ControlDarkDark;
      this.Light = SystemColors.ControlLight;
      this.LightLight = SystemColors.ControlLightLight;
    }

    /// <summary>
    /// Constructor used to override default colors used by class
    /// </summary>
    public GDIUtils( Color Dark, Color DarkDark, Color Light, Color LightLight )
    {
      this.Dark = Dark;
      this.DarkDark = DarkDark;
      this.Light = Light;
      this.LightLight = LightLight;
    }

    /// <summary>
    /// Destroy all pens and brushes used by class
    /// </summary>
    public void Dispose()
    {
      if( m_brushDark != null ) m_brushDark.Dispose();
      if( m_penDark != null ) m_penDark.Dispose();
      if( m_brushDarkDark != null ) m_brushDarkDark.Dispose();
      if( m_penDarkDark != null ) m_penDarkDark.Dispose();
      if( m_brushLight != null ) m_brushLight.Dispose();
      if( m_penLight != null ) m_penLight.Dispose();
      if( m_brushLightLight != null ) m_brushLightLight.Dispose();
      if( m_penLightLight != null ) m_penLightLight.Dispose();
    }

    #endregion

    #region Custom Drawing functions
    /// <summary>
    /// Draw 3d Line. 3D Line is a simple line wich contains one dark and one light line.
    /// By dark and light line we create optical 3D effect.
    /// </summary>
    /// <param name="graph">Graphics object which used by function to draw</param>
    /// <param name="pnt1">Start point</param>
    /// <param name="pnt2">End point</param>
    public void Draw3DLine( Graphics graph, Point pnt1, Point pnt2 )
    {
      Pen penDark = new Pen( m_brushDark );
      Pen penLight = new Pen( m_brushLightLight );

      Point[] arrPoint = { pnt1, pnt2 }; // create copy of Point input params
      graph.DrawLine( penLight, pnt1, pnt2 ); // draw first line

      if( pnt1.X == pnt2.X )
      {
        arrPoint[0].X--;
        arrPoint[1].X--;
      }
      else if( pnt1.Y == pnt2.Y )
      {
        arrPoint[0].Y--;
        arrPoint[1].Y--;
      }
      else
      {
        arrPoint[0].X--; arrPoint[0].Y--;
        arrPoint[1].X--; arrPoint[1].Y--;
      }

      graph.DrawLine( penDark, arrPoint[0], arrPoint[1] );

      penDark.Dispose();
      penLight.Dispose();
    }

    /// <summary>
    /// Draw 3D box according to style specification. There are four styles which our
    /// function know how to draw.
    /// </summary>
    /// <param name="graph">Graphics object used for drawing</param>
    /// <param name="rect">Box rectangle</param>
    /// <param name="style">Style of Box</param>
    public void Draw3DBox( Graphics graph, Rectangle rect, Canvas3DStyle style )
    {
      Point 
        pnt1 = new Point(0), 
        pnt2 = new Point(0),
        pnt4 = new Point(0);
      
      Point[] arrPoints = { pnt1, pnt2, pnt1, pnt4 };

      switch( style ) 
      {
        case Canvas3DStyle.Flat:
          graph.DrawRectangle( m_penDark, rect );
          break;

        case Canvas3DStyle.Title:
          #region Canvas 3DStyle - Title
          graph.DrawRectangle( m_penDark, rect );

          pnt1.X = rect.X+1; pnt1.Y = rect.Y+1;
          pnt2.X = rect.X+1; pnt2.Y = rect.Height-1;
          pnt4.X = rect.Width-1; pnt4.Y = rect.Y+1;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
        
          graph.DrawLines( m_penLightLight, arrPoints );
          #endregion
          break;
        
        case Canvas3DStyle.Raised:
          #region Canvas 3DStyle Raised
          // draw left upper corner
          pnt1.X = rect.X; pnt1.Y = rect.Y;
          pnt2.X = rect.X + rect.Width; pnt2.Y = rect.Y;
          pnt4.X = rect.X; pnt4.Y = rect.Y + rect.Height;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
        
          graph.DrawLines( m_penDark, arrPoints );

          pnt1.X++; pnt1.Y++;
          pnt2.X-=2; pnt2.Y++;
          pnt4.X++; pnt4.Y-=2;
          
          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penDarkDark, arrPoints );

          pnt1.X = rect.X + rect.Width; pnt1.Y = rect.Y + rect.Height;
          pnt2.X = rect.X; pnt2.Y = rect.Y + rect.Height;
          pnt4.X = rect.X + rect.Width; pnt4.Y = rect.Y;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penLightLight, arrPoints );

          pnt1.X--; pnt1.Y--;
          pnt2.X++; pnt2.Y--;
          pnt4.X--; pnt4.Y++;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penLight, arrPoints );
          #endregion
          break;

        case Canvas3DStyle.Upped:
          #region Canvas 3D Style Upped
          // draw left upper corner
          pnt1.X = rect.X; pnt1.Y = rect.Y;
          pnt2.X = rect.X + rect.Width; pnt2.Y = rect.Y;
          pnt4.X = rect.X; pnt4.Y = rect.Y + rect.Height;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
        
          graph.DrawLines( m_penLightLight, arrPoints );

          pnt1.X++; pnt1.Y++;
          pnt2.X-=2; pnt2.Y++;
          pnt4.X++; pnt4.Y-=2;
          
          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penLight, arrPoints );

          pnt1.X = rect.X + rect.Width; pnt1.Y = rect.Y + rect.Height;
          pnt2.X = rect.X; pnt2.Y = rect.Y + rect.Height;
          pnt4.X = rect.X + rect.Width; pnt4.Y = rect.Y;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penDarkDark, arrPoints );

          pnt1.X--; pnt1.Y--;
          pnt2.X++; pnt2.Y--;
          pnt4.X--; pnt4.Y++;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penDark, arrPoints );
          #endregion
          break;
        
        case Canvas3DStyle.Single:
          #region Canvas 3D Style Single
          // draw left upper corner
          pnt1.X = rect.X; pnt1.Y = rect.Y;
          pnt2.X = rect.Width; pnt2.Y = rect.Y;
          pnt4.X = rect.X; pnt4.Y = rect.Height;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
        
          graph.DrawLines( m_penDark, arrPoints );
          
          // draw right low corner
          pnt1.X = rect.Width; pnt1.Y = rect.Height;
          pnt2.X = rect.X; pnt2.Y = rect.Height;
          pnt4.X = rect.Width; pnt4.Y = rect.Y;

          // set new values to array of pointers
          arrPoints[0] = arrPoints[2] = pnt1;
          arrPoints[1] = pnt2; arrPoints[3] = pnt4;
          
          graph.DrawLines( m_penLightLight, arrPoints );
          #endregion
          break;
      }
    } 

    /// <summary>
    /// Draw Active rectangle by blue colors
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="rect"></param>
    public void DrawActiveRectangle( Graphics graph, Rectangle rect, HightlightStyle state )
    {
      Color highlight = ( state == HightlightStyle.Active ) ? 
        Color.FromArgb( 182, 189, 210 ) : Color.FromArgb(212,213,216);

      Color highBorder = Color.FromArgb( 10, 36, 106 );

      SolidBrush high = new SolidBrush( highlight );
      SolidBrush bord = new SolidBrush( highBorder );
      Pen penBord = new Pen( bord );

      graph.FillRectangle( high, rect );
      graph.DrawRectangle( penBord, rect );

      penBord.Dispose();
      bord.Dispose();
      high.Dispose();
    }


    public void DrawActiveRectangle( Graphics graph, Rectangle rect, HightlightStyle state, bool bSubRect )
    {
      if( bSubRect == true )
      {
        rect.Width--;
        rect.Height--;
      }

      DrawActiveRectangle( graph, rect, state );
    
    }

    public Rectangle FixRectangleHeightWidth( Rectangle rect )
    {
      return new Rectangle( rect.X, rect.Y, rect.Width - 1, rect.Height - 1 );
    }
    #endregion

    #region Class Helper Functions
    /// <summary>
    /// Calculate X and Y coordiante to place object in center of Rectangle
    /// </summary>
    /// <param name="rect">Destination Rectangle</param>
    /// <param name="sz">Object Size</param>
    /// <returns>Point class with X and Y coordinate of center</returns>
    public Point CalculateCenter( Rectangle rect, Size sz )
    {
      Point pnt1 = new Point(0);

      pnt1.X = rect.X + (rect.Width - sz.Width)/2;
      pnt1.Y = rect.Y + (rect.Height - sz.Height)/2;

      return pnt1;
    }
    
    #endregion
  }
}
