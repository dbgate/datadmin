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
      <DialectName>postgre</DialectName>
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
    <DockPanel FormatVersion="1.0" DockLeftPortion="0.25" DockRightPortion="0.25" DockTopPortion="0.25" DockBottomPortion="0.25" ActiveDocumentPane="0" ActivePane="0">
      <Contents Count="3">
        <Content ID="0" PersistString="5ff88b68-c18f-471b-a59e-ca0f699ca347" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="1" PersistString="fc574e81-0b46-445a-98b7-7fafa3319013" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="2" PersistString="8b8d1381-97d0-4791-8a3c-b729234af205" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
      </Contents>
      <Panes Count="5">
        <Pane ID="0" DockState="Document" ActiveContent="1">
          <Contents Count="1">
            <Content ID="0" RefID="1" />
          </Contents>
        </Pane>
        <Pane ID="1" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="0" />
          </Contents>
        </Pane>
        <Pane ID="2" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
        <Pane ID="3" DockState="Document" ActiveContent="2">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
        <Pane ID="4" DockState="Document" ActiveContent="0">
          <Contents Count="1">
            <Content ID="0" RefID="0" />
          </Contents>
        </Pane>
      </Panes>
      <DockWindows>
        <DockWindow ID="0" DockState="Document" ZOrderIndex="1">
          <NestedPanes Count="3">
            <Pane ID="0" RefID="0" PrevPane="-1" Alignment="Right" Proportion="0.5" />
            <Pane ID="1" RefID="3" PrevPane="0" Alignment="Bottom" Proportion="0.5" />
            <Pane ID="2" RefID="4" PrevPane="3" Alignment="Left" Proportion="0.5" />
          </NestedPanes>
        </DockWindow>
        <DockWindow ID="1" DockState="DockLeft" ZOrderIndex="2">
          <NestedPanes Count="0" />
        </DockWindow>
        <DockWindow ID="2" DockState="DockRight" ZOrderIndex="3">
          <NestedPanes Count="0" />
        </DockWindow>
        <DockWindow ID="3" DockState="DockTop" ZOrderIndex="4">
          <NestedPanes Count="0" />
        </DockWindow>
        <DockWindow ID="4" DockState="DockBottom" ZOrderIndex="5">
          <NestedPanes Count="0" />
        </DockWindow>
      </DockWindows>
      <FloatWindows Count="2">
        <FloatWindow ID="0" Bounds="1008, 455, 300, 300" ZOrderIndex="0">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="2" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
        <FloatWindow ID="1" Bounds="281, 620, 300, 300" ZOrderIndex="1">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="1" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
      </FloatWindows>
    </DockPanel>
  </Layout>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>view</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
    </Widget>
    <PersistString>5ff88b68-c18f-471b-a59e-ca0f699ca347</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>table</DbObjectType>
      <ListStyle>list</ListStyle>
      <ShowToolbar>1</ShowToolbar>
    </Widget>
    <PersistString>fc574e81-0b46-445a-98b7-7fafa3319013</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>sequence</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
    </Widget>
    <PersistString>8b8d1381-97d0-4791-8a3c-b729234af205</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_object_list" adtype="widget">
      <DbObjectType>table</DbObjectType>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
    </Widget>
    <PersistString>3340126d-b657-4ee3-8858-1fca7302176d</PersistString>
  </Widget>
</Addon>