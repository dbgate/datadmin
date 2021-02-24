<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xi="http://www.w3.org/2001/XInclude">
    <xsl:template match="/">
        <Objects>
            <Object type="DatAdmin.RunSqlCommand, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
                <RunSqlCommand>
                    <Original>true</Original>
                    <xi:include href='conditions.xsl'/>
                    <NodeTypeConditionsText><xsl:value-of select='Addon/NodeTypeConditionsText'/></NodeTypeConditionsText>
                    <Shortcut>None</Shortcut>
                    <Code><xsl:value-of select='Addon/Code'/></Code>
                    <Language><xsl:value-of select='Addon/@lang'/></Language>
                    <Title><xsl:value-of select='Addon/@title'/></Title>
                    <xi:include href='attributes.xsl'/>
                    <ConfirmQuestion/>
                    <ShowSqlScript>true</ShowSqlScript>
                </RunSqlCommand>
            </Object>
        </Objects>
    </xsl:template>
</xsl:stylesheet>
