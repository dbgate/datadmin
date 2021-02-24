<Addon type="dockpanel" adtype="dashboard">
  <Filter type="database" adtype="objectfilter">
    <DatabaseName>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </DatabaseName>
    <ServerName>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </ServerName>
    <DialectName>
      <DialectName>mysql</DialectName>
      <Enabled>1</Enabled>
    </DialectName>
    <ObjectType>
      <IsRegex>0</IsRegex>
      <DefinedVal>database</DefinedVal>
      <Enabled>1</Enabled>
    </ObjectType>
  </Filter>
  <Priority>10</Priority>
  <Layout>
    <DockPanel FormatVersion="1.0" DockLeftPortion="0.25" DockRightPortion="0.322085889570552" DockTopPortion="0.25" DockBottomPortion="0.25" ActiveDocumentPane="3" ActivePane="3">
      <Contents Count="3">
        <Content ID="0" PersistString="a81d8369-2fc8-4346-bd7c-dc5342dda8d5" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="1" PersistString="c05854e1-dd1b-41f8-a4f4-3c9f975b904e" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="2" PersistString="6e90744d-3d31-4d65-a5ed-960e260f7b16" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
      </Contents>
      <Panes Count="6">
        <Pane ID="0" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="0" />
          </Contents>
        </Pane>
        <Pane ID="1" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="1" />
          </Contents>
        </Pane>
        <Pane ID="2" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
        <Pane ID="3" DockState="Document" ActiveContent="0">
          <Contents Count="1">
            <Content ID="0" RefID="0" />
          </Contents>
        </Pane>
        <Pane ID="4" DockState="Document" ActiveContent="1">
          <Contents Count="1">
            <Content ID="0" RefID="1" />
          </Contents>
        </Pane>
        <Pane ID="5" DockState="Document" ActiveContent="2">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
      </Panes>
      <DockWindows>
        <DockWindow ID="0" DockState="Document" ZOrderIndex="1">
          <NestedPanes Count="3">
            <Pane ID="0" RefID="3" PrevPane="-1" Alignment="Right" Proportion="0.5" />
            <Pane ID="1" RefID="4" PrevPane="3" Alignment="Bottom" Proportion="0.5" />
            <Pane ID="2" RefID="5" PrevPane="4" Alignment="Right" Proportion="0.480688253230724" />
          </NestedPanes>
        </DockWindow>
        <DockWindow ID="1" DockState="DockLeft" ZOrderIndex="3">
          <NestedPanes Count="0" />
        </DockWindow>
        <DockWindow ID="2" DockState="DockRight" ZOrderIndex="2">
          <NestedPanes Count="0" />
        </DockWindow>
        <DockWindow ID="3" DockState="DockTop" ZOrderIndex="4">
          <NestedPanes Count="0" />
        </DockWindow>
        <DockWindow ID="4" DockState="DockBottom" ZOrderIndex="5">
          <NestedPanes Count="0" />
        </DockWindow>
      </DockWindows>
      <FloatWindows Count="3">
        <FloatWindow ID="0" Bounds="133, 432, 300, 300" ZOrderIndex="0">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="0" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
        <FloatWindow ID="1" Bounds="659, 671, 300, 300" ZOrderIndex="1">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="1" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
        <FloatWindow ID="2" Bounds="448, 712, 300, 300" ZOrderIndex="2">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="2" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
      </FloatWindows>
    </DockPanel>
  </Layout>
  <Widget>
    <Widget type="tables_raw_grid" adtype="widget">
      <RowLimit>0</RowLimit>
    </Widget>
    <PersistString>5935ccb3-cf3f-4d4a-9a31-4850ffcc53f4</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>table</DbObjectType>
      <ListStyle>list</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>a81d8369-2fc8-4346-bd7c-dc5342dda8d5</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>view</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>c05854e1-dd1b-41f8-a4f4-3c9f975b904e</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>trigger</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>6e90744d-3d31-4d65-a5ed-960e260f7b16</PersistString>
  </Widget>
  <Widget>
    <Widget type="mysql_processlist" adtype="widget">
      <RowLimit>0</RowLimit>
    </Widget>
    <PersistString>ec21c04a-1bb7-48b5-b486-344a521e1176</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>table</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>3a0de016-1fdd-45c8-aa00-3fb37a77ea23</PersistString>
  </Widget>
</Addon>