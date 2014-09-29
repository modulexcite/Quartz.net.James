<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <html>
      <head>
        <title>
          Person info
        </title>
        <style type="text/css">
          h1 {
          text-align: center;
          }
          h2
          {
          text-indent: 0px;
          }
          h3
          {
          text-indent: 20px;
          }

          body {
          font-family: Calibri;
          margin:0px;
          padding:0px;
          background: #fff;
          }
          td
          {
          padding: 5px;
          border:1px solid #A4B1BF;
          }
        </style>
      </head>
      <body>
        <xsl:apply-templates select="Person" />
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Person">
    <h1>
      Person: <xsl:value-of select="FirstName" /> - <xsl:value-of select="LastName" />
    </h1>
    <table class="report-table" cellpadding="0" cellspacing="0">
      <tr>
        <td>Birth year</td>
        <td>
          <xsl:value-of select="BirthYear" />
        </td>
      </tr>
      <tr>
        <td>Nick:</td>
        <td>
          <xsl:value-of select="Nickname" />
        </td>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>