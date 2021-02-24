<Addon type="dockpanel" adtype="dashboard">
  <Filter type="dbobject" adtype="objectfilter">
    <DbObjectSchema>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </DbObjectSchema>
    <DbObjectName>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </DbObjectName>
    <DbObjectType>
      <IsRegex>0</IsRegex>
      <DefinedVal>view</DefinedVal>
      <Enabled>1</Enabled>
    </DbObjectType>
    <DatabaseName>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </DatabaseName>
    <ServerName>
      <IsRegex>0</IsRegex>
      <Enabled>0</Enabled>
    </ServerName>
    <DialectName>
      <Enabled>0</Enabled>
    </DialectName>
    <ObjectType>
      <IsRegex>0</IsRegex>
      <DefinedVal>dbobject</DefinedVal>
      <Enabled>1</Enabled>
    </ObjectType>
  </Filter>
  <Priority>0</Priority>
  <Layout>
    <DockPanel FormatVersion="1.0" DockLeftPortion="0.25" DockRightPortion="0.25" DockTopPortion="0.25" DockBottomPortion="0.25" ActiveDocumentPane="2" ActivePane="2">
      <Contents Count="4">
        <Content ID="0" PersistString="2c69aeca-008a-4e61-8f0e-e6a79fa1dfef" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="1" PersistString="86c24043-3eb3-4d4f-95db-80895df0d45f" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="2" PersistString="b861fee3-1fc4-4844-a3bd-f8ca78ce23ab" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="3" PersistString="59946427-71ec-42ef-bb1d-7fef832c375a" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
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
            <Pane ID="2" RefID="2" PrevPane="0" Alignment="Left" Proportion="0.5" />
            <Pane ID="3" RefID="3" PrevPane="2" Alignment="Right" Proportion="0.4957805907173" />
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
    <Widget type="db_object_sql" adtype="widget" />
    <PersistString>2c69aeca-008a-4e61-8f0e-e6a79fa1dfef</PersistString>
  </Widget>
  <Widget>
    <Widget type="table_data" adtype="widget">
      <ReadOnly>0</ReadOnly>
      <HideToolbars>0</HideToolbars>
    </Widget>
    <PersistString>86c24043-3eb3-4d4f-95db-80895df0d45f</PersistString>
  </Widget>
  <Widget>
    <Widget type="table_column_list" adtype="widget">
      <Column>
        <HeaderText>s_name</HeaderText>
        <DataSource>column</DataSource>
        <Width>120</Width>
      </Column>
      <Column>
        <HeaderText>s_type</HeaderText>
        <DataSource>datatype</DataSource>
        <Width>120</Width>
      </Column>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
      <DoubleClickAction>openintree</DoubleClickAction>
    </Widget>
    <PersistString>b861fee3-1fc4-4844-a3bd-f8ca78ce23ab</PersistString>
  </Widget>
  <Widget>
    <Widget type="table_info_html" adtype="widget">
      <ShowTableName>1</ShowTableName>
    </Widget>
    <PersistString>59946427-71ec-42ef-bb1d-7fef832c375a</PersistString>
  </Widget>
</Addon>