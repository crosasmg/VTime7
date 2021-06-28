<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<script language="VB" runat="Server">
Dim mstrFrameTag As String

'- Variables para el manejo del QueryString 
Dim mstrHref As String
Dim mstrQueryString As String
Dim mintIndex As Integer
Dim lintPosition As Integer
Dim sCodispl As String


</script>
<%
    With Response
        .AddHeader("pragma", "no-cache")
        .CacheControl = "Private"
        .Expires = 0
    End With
    If CStr(Session("SessionID")) = "" Then
        Response.Redirect(("/VTimeNet/VisualTime/VisualTime.htm"))
    End If

    sCodispl = Request.QueryString.Item("sCodispl")

    lintPosition = InStr(1, Session("sHistory"), Trim(sCodispl))

    If lintPosition = 0 Then
        Session("sHistory") = Trim(sCodispl) & New String(" ", 8 - Len(sCodispl)) & Session("sHistory")
    Else
        Session("sHistory") = Session("sHistory").replace(sCodispl, "")
        Session("sHistory") = Trim(sCodispl) & New String(" ", 8 - Len(sCodispl)) & Session("sHistory")
    End If

    Dim a As String = "jose miguel"
    a.Replace("miguel", "")

%>
<HTML>
<HEAD>
    <LINK REL="SHORTCUT ICON" HREF="/VTimeNet/images/favicon.ico">
    <!--SCRIPT>if (typeof(opener)=='undefined'){ self.close();self.location.href="/VTimeNet/common/Blank.aspx"} </SCRIPT-->
</HEAD>
<FRAMESET COLS="150,*" FRAMEBORDER="0" FRAMESPACING="1">
    <FRAME NAME="fraSequence" NORESIZE  TARGET="fraHeader" SRC="../Common/Sequence.aspx">
    <FRAMESET ROWS="*,10,10">
    <%
        mstrHref = "sCodispl=" & Request.QueryString.Item("sCodispl")
        'For mintIndex = 1 To Request.QueryString.Count
        For mintIndex = 0 To Request.QueryString.Count -1
            'UPGRADE_WARNING: Request property Request.QueryString.Key has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup2065.aspx'
            If InStr(1, mstrHref, Request.QueryString.GetKey(mintIndex)) = 0 Then
                'UPGRADE_WARNING: Request property Request.QueryString.Key has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup2065.aspx'
                mstrQueryString = mstrQueryString & "&" & Request.QueryString.GetKey(mintIndex) & "=" & Request.QueryString.Item(mintIndex)
            End If
        Next

        With Response
            mstrFrameTag = "<frame name=""fraHeader"" scrolling=""yes""  FrameBorder=0    SRC=""../" & Request.QueryString.Item("sModule") & "/" & Request.QueryString.Item("sProject") & "/" & Replace(UCase(Request.QueryString.Item("sCodisp")), "_K", vbNullString) & "_K.aspx"
            mstrFrameTag = mstrFrameTag & "?" & "sCodispl=" & Request.QueryString.Item("sCodispl")
            mstrFrameTag = mstrFrameTag & mstrQueryString
            If InStr(1, Request.Params.Get("Query_String"), "&sOption") > 0 Then
                mstrFrameTag = mstrFrameTag & Mid(Request.Params.Get("Query_String"), InStr(1, Request.Params.Get("Query_String"), "&sOption"))
            End If
            mstrFrameTag = mstrFrameTag & """>"
            .Write(mstrFrameTag)
            .Write("<frame name=""fraGeneric"" FrameBorder=0 scrolling=""Yes"" SRC=""Blank.aspx"">")
            .Write("<frame name=""fraSubmit"" FrameBorder=0 scrolling=""Yes"" SRC=""Blank.aspx"">")
        End With
%>
    </FRAMESET>
</FRAMESET>
  <NOFRAMES>
  <BODY>
  <P>Esta página utiliza frame, pero su BROWSER no lo soporta</P>
  </BODY>
  </NOFRAMES>
</FRAMESET>
</HTML>






