<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xi="http://www.w3.org/2001/XInclude">
    <xsl:template match="/">
        <Objects>
            <Object type="DatAdmin.SyntaxTextObjectView, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
                <SyntaxTextObjectView>
                    <Original>false</Original>
                    <xi:include href='conditions.xsl'/>
                    <NodeTypeConditionsText><xsl:value-of select='Addon/NodeTypeConditionsText'/></NodeTypeConditionsText>
                    <PageTitle><xsl:value-of select='Addon/@title'/></PageTitle>
                    <Position><xsl:value-of select='Addon/@position'/></Position>
                    <Language><xsl:value-of select='Addon/@lang'/></Language>
                    <Code><xsl:value-of select='Addon/Code'/></Code>
                    <ViewLanguage>Sql</ViewLanguage>
                </SyntaxTextObjectView>
            </Object>        
        </Objects>
    </xsl:template>
</xsl:stylesheet>
