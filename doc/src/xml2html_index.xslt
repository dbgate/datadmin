<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:param name='lang'/>
  <xsl:output method="html" encoding="utf-8"/>

  <xsl:template match="/">
    <HTML>
      <HEAD>
        <meta name="GENERATOR" content="xml2hhc"/>
        <!-- Sitemap 1.0 -->
      </HEAD>
      <BODY>
        <UL>
          <xsl:apply-templates/>
    		</UL>
    	</BODY>
    </HTML>
  </xsl:template>

  <xsl:template match="item">
    <LI>
        <a  target='content'>
        <xsl:if test="@href">
            <xsl:attribute name='href'>
                <xsl:value-of select="@href"/>
            </xsl:attribute>
        </xsl:if>
        <xsl:value-of select="@title"/>
        </a>        
	</LI>
	<xsl:if test='count(item)&gt;0'>
	  <UL>
	    <xsl:apply-templates/>
	  </UL>
	</xsl:if>
  </xsl:template>
	  
</xsl:stylesheet>