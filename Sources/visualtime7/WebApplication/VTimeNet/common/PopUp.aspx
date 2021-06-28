<%@ Page Language="VB" %>
<HTML>
<HEAD>
</HEAD>
<FRAMESET ROWS="*,10" FRAMESPACING="1">
  <FRAME FRAMESPACING="1" NAME="fraFolder" SCROLLING="No" FRAMEBORDER="0" TARGET="fraGeneric" SRC='<%=Request.QueryString.Item("sPageName")%>.aspx?<%=Request.Params.Get("Query_String")%>'>
  <FRAME FRAMESPACING="1" NAME="fraGeneric" SCROLLING="No"  FRAMEBORDER="0" SRC="Blank.htm">
  <NOFRAMES>
  <BODY>
	  <P>Esta página usa marcos, pero su explorador no los admite.</P>
  </BODY>
  </NOFRAMES>
</FRAMESET>
</HTML>






