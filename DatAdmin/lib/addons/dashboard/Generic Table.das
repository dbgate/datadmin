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
      <Enabled>0</Enabled>
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
      <DialectName>
      </DialectName>
      <Enabled>0</Enabled>
    </DialectName>
    <ObjectType>
      <IsRegex>0</IsRegex>
      <DefinedVal>table</DefinedVal>
      <Enabled>1</Enabled>
    </ObjectType>
  </Filter>
  <Priority>0</Priority>
  <Layout>
    <DockPanel FormatVersion="1.0" DockLeftPortion="0.25" DockRightPortion="0.25" DockTopPortion="0.25" DockBottomPortion="0.25" ActiveDocumentPane="3" ActivePane="3">
      <Contents Count="5">
        <Content ID="0" PersistString="f9b787d4-9b5a-4cd3-aa37-7ad5de844a92" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="1" PersistString="a9d6c7f3-0d94-494b-8ea6-416371e52167" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="2" PersistString="7aa9da7c-abcf-4921-95d5-f76cd3ae90f0" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="3" PersistString="c0d2fad8-b9b2-49a0-bcf2-a7ecae1a07da" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
        <Content ID="4" PersistString="2addab24-7cad-4dcb-a733-165d0bedcb71" AutoHidePortion="0.25" IsHidden="False" IsFloat="False" />
      </Contents>
      <Panes Count="7">
        <Pane ID="0" DockState="Document" ActiveContent="0">
          <Contents Count="1">
            <Content ID="0" RefID="0" />
          </Contents>
        </Pane>
        <Pane ID="1" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
        <Pane ID="2" DockState="Float" ActiveContent="-1">
          <Contents Count="1">
            <Content ID="0" RefID="1" />
          </Contents>
        </Pane>
        <Pane ID="3" DockState="Document" ActiveContent="2">
          <Contents Count="1">
            <Content ID="0" RefID="2" />
          </Contents>
        </Pane>
        <Pane ID="4" DockState="Document" ActiveContent="1">
          <Contents Count="1">
            <Content ID="0" RefID="1" />
          </Contents>
        </Pane>
        <Pane ID="5" DockState="Document" ActiveContent="3">
          <Contents Count="1">
            <Content ID="0" RefID="3" />
          </Contents>
        </Pane>
        <Pane ID="6" DockState="Document" ActiveContent="4">
          <Contents Count="1">
            <Content ID="0" RefID="4" />
          </Contents>
        </Pane>
      </Panes>
      <DockWindows>
        <DockWindow ID="0" DockState="Document" ZOrderIndex="1">
          <NestedPanes Count="5">
            <Pane ID="0" RefID="0" PrevPane="-1" Alignment="Right" Proportion="0.5" />
            <Pane ID="1" RefID="3" PrevPane="0" Alignment="Bottom" Proportion="0.5" />
            <Pane ID="2" RefID="4" PrevPane="0" Alignment="Right" Proportion="0.5" />
            <Pane ID="3" RefID="5" PrevPane="0" Alignment="Right" Proportion="0.5" />
            <Pane ID="4" RefID="6" PrevPane="5" Alignment="Bottom" Proportion="0.350415512465374" />
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
        <FloatWindow ID="0" Bounds="286, 356, 300, 300" ZOrderIndex="0">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="1" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
        <FloatWindow ID="1" Bounds="296, 297, 300, 300" ZOrderIndex="1">
          <NestedPanes Count="1">
            <Pane ID="0" RefID="2" PrevPane="-1" Alignment="Right" Proportion="0.5" />
          </NestedPanes>
        </FloatWindow>
      </FloatWindows>
    </DockPanel>
  </Layout>
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
    </Widget>
    <PersistString>f9b787d4-9b5a-4cd3-aa37-7ad5de844a92</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_table_sql" adtype="widget" />
    <PersistString>a9d6c7f3-0d94-494b-8ea6-416371e52167</PersistString>
  </Widget>
  <Widget>
    <Widget type="table_data" adtype="widget">
      <ReadOnly>1</ReadOnly>
      <HideToolbars>1</HideToolbars>
    </Widget>
    <PersistString>7aa9da7c-abcf-4921-95d5-f76cd3ae90f0</PersistString>
  </Widget>
  <Widget>
    <Widget type="db_constraint_list" adtype="widget">
      <Column>
        <HeaderText>s_name</HeaderText>
        <DataSource>constraint</DataSource>
        <Width>120</Width>
      </Column>
      <Column>
        <HeaderText>s_columns</HeaderText>
        <DataSource>columns</DataSource>
        <Width>120</Width>
      </Column>
      <ListStyle>details</ListStyle>
      <ShowToolbar>1</ShowToolbar>
    </Widget>
    <PersistString>c0d2fad8-b9b2-49a0-bcf2-a7ecae1a07da</PersistString>
  </Widget>
  <Widget>
    <Widget type="table_info_html" adtype="widget">
      <ShowTableName>0</ShowTableName>
    </Widget>
    <PersistString>2addab24-7cad-4dcb-a733-165d0bedcb71</PersistString>
  </Widget>
</Addon>