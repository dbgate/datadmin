<Conditions xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:for-each select='//PropertyCondition'>
        <CommandCondition>
            <PropertyName><xsl:value-of select='@name'/></PropertyName>
            <TestedValue><xsl:value-of select='@value'/></TestedValue>
        </CommandCondition>
    </xsl:for-each>
</Conditions>
