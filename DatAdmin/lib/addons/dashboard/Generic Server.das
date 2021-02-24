<Addon type="dockpanel" adtype="dashboard">
  <Filter type="server" adtype="objectfilter">
    <ServerName>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </ServerName>
    <DialectName>
      <Enabled>0</Enabled>
    </DialectName>
    <ObjectType>
      <IsRegex>0</IsRegex>
      <DefinedVal>server</DefinedVal>
      <Enabled>1</Enabled>
    </ObjectType>
  </Filter>
  <Priority>0</Priority>
  <Layout>
    <DockPanel FormatVersion="1.0" DockLeftPortion="0.25" DockRightPortion="0.25" DockTopPortion="0.25" DockBottomPortion="0.25" ActiveDocumentPane="0" ActivePane="0">
      <Contents Count="2">
        <Content ID="0" PersistString="f54244e0-8d5a-4f97-91a7-e160cb712b68" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="1" PersistString="4a2fe1cb-0708-4250-a376-b160dd17b474" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
      </Contents>
      <Panes Count="2">
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
      </Panes>
      <DockWindows>
        <DockWindow ID="0" DockState="Document" ZOrderIndex="1">
          <NestedPanes Count="2">
            <Pane ID="0" RefID="0" PrevPane="-1" Alignment="Right" Proportion="0.5" />
            <Pane ID="1" RefID="1" PrevPane="0" Alignment="Bottom" Proportion="0.5" />
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
      <FloatWindows Count="0" />
    </DockPanel>
  </Layout>
  <Widget>
    <Widget type="databases_raw_grid" adtype="widget">
      <RowLimit>0</RowLimit>
    </Widget>
    <PersistString>ed34f59f-783f-4c8a-8adf-c69ac207a85d</PersistString>
  </Widget>
  <Widget>
    <Widget type="database_list" adtype="widget">
      <ListStyle>list</ListStyle>
      <ShowToolbar>1</ShowToolbar>
    </Widget>
    <PersistString>f54244e0-8d5a-4f97-91a7-e160cb712b68</PersistString>
  </Widget>
  <Widget>
    <Widget type="connection_info_html" adtype="widget" />
    <PersistString>4a2fe1cb-0708-4250-a376-b160dd17b474</PersistString>
  </Widget>
</Addon>