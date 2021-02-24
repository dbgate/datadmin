<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xi="http://www.w3.org/2001/XInclude">
    <xsl:template match="/">
        <Objects>
            <xsl:if test='not(Addon/@view="0")'>
                <Object type="DatAdmin.SqlObjectView, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
                    <SqlObjectView>
                        <Original>true</Original>
                        <xi:include href='conditions.xsl'/>
                        <NodeTypeConditionsText><xsl:value-of select='Addon/NodeTypeConditionsText'/></NodeTypeConditionsText>
                        <MaximumRecords>200</MaximumRecords>
                        <PageTitle><xsl:value-of select='Addon/@title'/></PageTitle>
                        <Position><xsl:value-of select='Addon/@position'/></Position>
                        <Language><xsl:value-of select='Addon/@lang'/></Language>
                        <Code><xsl:value-of select='Addon/Code'/></Code>
                    </SqlObjectView>
                </Object>
            </xsl:if>

            <xsl:if test='not(Addon/@nodegen="0")'>
                <Object type="DatAdmin.DbListNodeGenerator, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
                    <DbListNodeGenerator>
                        <Original>false</Original>
                        <xi:include href='conditions.xsl'/>
                        <NodeTypeConditionsText><xsl:value-of select='Addon/NodeTypeConditionsText'/></NodeTypeConditionsText>
                        <ImageFile><xsl:value-of select='Addon/@image'/></ImageFile>
                        <NodeName><xsl:value-of select='Addon/@name'/></NodeName>
                        <ChildTypeTitle/>
                        <ChildTitleColumn><xsl:value-of select='Addon/ChildTitleColumn'/></ChildTitleColumn>
                        <ChildTitleExpr/>
                        <Code><xsl:value-of select='Addon/Code'/></Code>
                        <Language><xsl:value-of select='Addon/@lang'/></Language>
                        <TypeTitle><xsl:value-of select='Addon/@title'/></TypeTitle>
                        <Title><xsl:value-of select='Addon/@title'/></Title>
                        <NodeProperties/>

                        <ChildProperties>
                            <PropertyDefinition>
                                <PropertyName>dbentity</PropertyName>
                                <Expression>'<xsl:value-of select='Addon/@dbentity'/>'</Expression>
                            </PropertyDefinition>
                            
                            <PropertyDefinition>
                                <PropertyName>objname</PropertyName>
                                <Expression>row['<xsl:value-of select='Addon/ChildTitleColumn'/>']</Expression>
                            </PropertyDefinition>
                        </ChildProperties>                        
                    </DbListNodeGenerator>
                </Object>            
            </xsl:if>
        </Objects>
    </xsl:template>
</xsl:stylesheet>
