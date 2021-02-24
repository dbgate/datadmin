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
      <DialectName>effiproz</DialectName>
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
      <Contents Count="4">
        <Content ID="0" PersistString="a81d8369-2fc8-4346-bd7c-dc5342dda8d5" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="1" PersistString="c05854e1-dd1b-41f8-a4f4-3c9f975b904e" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="2" PersistString="189cc12f-3ea9-472d-bd4f-c79826575548" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="3" PersistString="832b70bb-ba0f-479a-81b7-2b297065492f" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
      </Contents>
      <Panes Count="4">
        <Pane ID="0" DockState="Document" ActiveContent="0">
          <Contents Count="1">
            <Content ID="0" RefID="0" />
          </Contents>
        </Pane>
        <Pane ID="1" DockState="Document" ActiveContent="1">
          <Contents Count="1">
            <Content ID="0" RefID="1" />
          </Contents>
        </Pane>
        <Pane ID="2" DockState="Document" ActiveContent="2">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
        <Pane ID="3" DockState="Document" ActiveContent="3">
          <Contents Count="1">
            <Content ID="0" RefID="3" />
          </Contents>
        </Pane>
      </Panes>
      <DockWindows>
        <DockWindow ID="0" DockState="Document" ZOrderIndex="1">
          <NestedPanes Count="4">
            <Pane ID="0" RefID="0" PrevPane="-1" Alignment="Right" Proportion="0.5" />
            <Pane ID="1" RefID="1" PrevPane="0" Alignment="Bottom" Proportion="0.5" />
            <Pane ID="2" RefID="2" PrevPane="1" Alignment="Right" Proportion="0.686153846153846" />
            <Pane ID="3" RefID="3" PrevPane="2" Alignment="Right" Proportion="0.5" />
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
      <FloatWindows Count="0" />
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
      <ListStyle>list</ListStyle>
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
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>table</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>10c060ba-1e89-4c89-a61c-d6b3353b7787</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>procedure</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>189cc12f-3ea9-472d-bd4f-c79826575548</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>function</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>832b70bb-ba0f-479a-81b7-2b297065492f</PersistString>
  </Widget>
</Addon>