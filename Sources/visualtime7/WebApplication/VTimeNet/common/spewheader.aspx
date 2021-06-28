<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<script language="VB" runat="Server">
Dim mstrFrameTag As String
Dim lintIndex As Integer
Dim lstrQueryString As String
Dim lstrHref As String

Dim lintPosition As Integer
Dim sCodispl As String


</script>
<%

    sCodispl = Request.QueryString.Item("sCodispl")

    lintPosition = InStr(1, Session("sHistory"), Trim(sCodispl))

    If lintPosition = 0 Then
        Session("sHistory") = Trim(sCodispl) & New String(" ", 8 - Len(sCodispl)) & Session("sHistory")
    Else
        Session("sHistory") = Session("sHistory").replace(sCodispl, "")
        Session("sHistory") = Trim(sCodispl) & New String(" ", 8 - Len(sCodispl)) & Session("sHistory")
    End If


    With Response
        .AddHeader("pragma", "no-cache")
        .CacheControl = "Private"
        .Expires = 0
    End With
    If CStr(Session("SessionID")) = "" Then
        Response.Redirect(("/VTimeNet/VisualTime/VisualTime.htm"))
    End If


%>
<HTML>
<HEAD>
    <LINK REL="SHORTCUT ICON" HREF="../images/favicon.ico">

</HEAD>
<%
If Request.QueryString.Item("sCodispl") = "GE099" Then
	With Response
		.Write("<FRAMESET COLS=""240,*"" FRAMEBORDER=""0"" FRAMESPACING=""1"">")
		.Write("<FRAME NAME=""fraSequence"" noresize  target=""fraHeader"" src=""/VTimeNet/GeneralQue/GeneralQue/Sequence.htm"">")
	End With
Else
	With Response
		.Write("<FRAMESET COLS=""150,*"" FRAMEBORDER=""0"" FRAMESPACING=""1"">")
		.Write("<FRAME NAME=""fraSequence"" noresize  target=""fraHeader"" src=""../Common/Sequence.aspx"">")
	End With
End If
%>
    <FRAMESET ROWS="<%=Request.QueryString.Item("nHeight")%>,*,10,10">
    <%
        lstrHref = "sCodispl=" & Request.QueryString.Item("sCodispl")
        'For lintIndex = 1 To Request.QueryString.Count
        For lintIndex = 0 To Request.QueryString.Count - 1
'UPGRADE_WARNING: Request property Request.QueryString.Key has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup2065.aspx'
            If InStr(1, lstrHref, Request.QueryString.GetKey(lintIndex)) = 0 Then
'UPGRADE_WARNING: Request property Request.QueryString.Key has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup2065.aspx'
                lstrQueryString = lstrQueryString & "&" & Request.QueryString.GetKey(lintIndex) & "=" & Request.QueryString.Item(lintIndex)
            End If
        Next
With Response
	mstrFrameTag = "<frame name=""fraHeader"" scrolling=""no""  FrameBorder=0  target=""fraFolder""  SRC=""/VTimeNet/" & Request.QueryString.Item("sModule") & "/" & Request.QueryString.Item("sProject") & "/" & Replace(UCase(Request.QueryString.Item("sCodisp")), "_K", vbNullString) & "_K.aspx"
	mstrFrameTag = mstrFrameTag & "?" & lstrHref & lstrQueryString
	mstrFrameTag = mstrFrameTag & """>"
	.Write(mstrFrameTag)
	.Write("<frame name=""fraFolder"" FrameBorder=0 scrolling=""Yes"" SRC=""Blank.aspx"">")
	.Write("<frame name=""fraGeneric"" FrameBorder=0 scrolling=""Yes"" SRC=""Blank.aspx"">")
	.Write("<frame name=""fraSubmit"" FrameBorder=0 scrolling=""Yes"" SRC=""Blank.aspx"">")
End With
%>
    </FRAMESET>
</FRAMESET>
  <NOFRAMES>
  <BODY>
  <P>Esta página utiliza frame, pero su BROWSER no lo soporta</P>
  </body>
  </NOFRAMES>
</FRAMESET>
</html>




