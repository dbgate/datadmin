<Attributes xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <Attributes>
        <xsl:for-each select='//InParam'>
            <DynamicAttribute>
                <Name><xsl:value-of select='@name'/></Name>
                <Title><xsl:value-of select='@title'/></Title>
                <Type><xsl:value-of select='@type'/></Type>
                <Description/>
                <Category>s_misc</Category>
            </DynamicAttribute>
        </xsl:for-each>
    </Attributes>
</Attributes>
