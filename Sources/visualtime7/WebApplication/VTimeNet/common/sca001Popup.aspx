<%@ Language=VBScript %>
<HTML>
<HEAD>
</HEAD>
<FRAMESET ROWS="*,0,0" FRAMESPACING="1">
  <FRAME FRAMESPACING="1" NAME="fraFolder" SCROLLING="No" FRAMEBORDER="0" TARGET="fraGeneric" SRC='SCA001.aspx?<%=Request.QueryString%>'>
  <FRAME FRAMESPACING="1" NAME="fraGeneric" SCROLLING="No"  FRAMEBORDER="0" SRC="Blank.HTM">
  <FRAME FRAMESPACING="1" NAME="fraSubmit" SCROLLING="No"  FRAMEBORDER="0" SRC="Blank.HTM">
  <NOFRAMES>
  <BODY>
	  <P>Esta p�gina usa marcos, pero su explorador no los admite.</P>
  </BODY>
  </NOFRAMES>
</FRAMESET>
</HTML>

