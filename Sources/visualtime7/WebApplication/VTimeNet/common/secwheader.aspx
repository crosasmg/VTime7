<%@ Page Language="VB" %>
<script language="VB" runat="Server">

Dim lintPosition As Integer
Dim sCodispl As String

Private mstrConfig As String
Private mstrHeight As String
Private mstrCodispl As String
Private mstrCodisp As String

'- Se declara la variable para indicar si se debe mostrar o no el Scroll
Dim lstrScroll As String

'- Se declara la variable para indicar si mostrara la ventana de Secuencia de /../Common o 
'- la del proyecto
Dim lstrSRCSequence As String

'- Se declara la variable que contiene los parámetros individuales a pasarle a la secuencia
Dim lstrQueryString As String


</script>
<%
    With Response
        .AddHeader("pragma", "no-cache")
        .CacheControl = "Private"
        .Expires = -1
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

%>
<HTML>
<HEAD>
    
    <META NAME="ProgId" CONTENT="FrontPage.Editor.Document">
    <LINK REL="SHORTCUT ICON" HREF="/VTimeNet/images/favicon.ico">
    <SCRIPT>if (typeof(opener)=='undefined'){ 
                self.close();
                self.location.href = "/VTimeNet/common/Blank.aspx"
            } 
    </SCRIPT>
</HEAD>
<%
        '+ Si se carga la secuencia, se muestra la página Sequence.aspx que se encuentra en cada
        '+ proyecto, sino la que se encuentra en /VTimeNet/Common    
        mstrConfig = Request.QueryString.Item("sConfig")

        If mstrConfig = "InSequence" Then
            lstrSRCSequence = "/VTimeNet/" & Request.QueryString.Item("sModule") & "/" & Request.QueryString.Item("sProject") & "/Sequence.aspx"
        Else
            lstrSRCSequence = "/VTimeNet/Common/Sequence.aspx"
        End If

        mstrCodispl = Request.QueryString.Item("sCodispl")
        mstrCodisp = Request.QueryString.Item("sCodisp")

        Session("sCodispl_Aux") = mstrCodispl


        If mstrCodispl = "CA099" Or mstrCodispl = "CA099C" Or mstrCodispl = "CO632" Or mstrCodispl = "CO633" Or mstrCodispl = "CO700" Then
            lstrSRCSequence = "/VTimeNet/Common/Sequence.aspx"
        End If

        If mstrCodispl <> "CA001" And mstrCodispl <> "CA001C" And mstrCodispl <> "CA001_K" And mstrCodispl <> "CO001_K" And mstrCodispl <> "CO01_K" And mstrCodispl <> "FI001_K" And mstrCodispl <> "FI001" And mstrCodispl <> "CA099" And mstrCodispl <> "CA099C" And mstrCodispl <> "CO633" And mstrCodispl <> "CO632" And mstrCodispl <> "CO700" And mstrCodispl <> "BC668_K" And mstrCodispl <> "CAL013_K" And mstrCodispl <> "SI001" Then
            mstrConfig = "InSequence"
        End If

        '+ Si se carga la secuencia, no se coloca el Scroll
        If mstrConfig = "InSequence" Then
            lstrScroll = "no"
        Else
            lstrScroll = "yes"
        End If

        If InStr(1, Request.Params.Get("Query_String"), "sConfig") > 0 Then
            lstrQueryString = Mid(Request.Params.Get("Query_String"), InStr(1, Request.Params.Get("Query_String"), "sConfig"))
        Else
            lstrQueryString = Mid(Request.Params.Get("Query_String"), InStr(1, Request.Params.Get("Query_String"), "sProject"))
        End If

        If InStr(1, lstrQueryString, "&") > 0 Then
            lstrQueryString = "&" & Mid(lstrQueryString, InStr(1, lstrQueryString, "&") + 1)
        Else
            lstrQueryString = vbNullString
        End If

        '+ NOTA: El parámetro LoadWithAction que se concatena a la página que se carga 
        '+ en el fraHeader, es para los casos en que se debe recargar la página con 
        '+ una acción por defecto
        If InStr(1, lstrQueryString, "LoadWithAction=") = 0 Then
            lstrQueryString = "&LoadWithAction=" & Request.QueryString.Item("LoadWithAction") & lstrQueryString
        End If

        If mstrConfig = "InSequence" Then
            mstrHeight = Request.QueryString.Item("nHeight")
            If mstrHeight = vbNullString Then
                mstrHeight = """130,*,10,10"""
            Else
                mstrHeight = """" & mstrHeight & ",*,10,10"""
            End If
        Else
            mstrHeight = """*,10,10"""
        End If

        lstrQueryString = lstrQueryString & "&sCodispl=" & mstrCodispl

%>
<FRAMESET COLS="150,*" FRAMEBORDER="1" FRAMESPACING="1">
    <FRAMESET ROWS="0,*" FRAMEBORDER="0">
        <FRAME NAME="fraSequence" NORESIZE TARGET="fraFolder" SRC="<%=lstrSRCSequence%>" SCROLLING="no">
        <FRAME NAME="TreeSequence" NORESIZE TARGET="fraFolder" SRC="/VTimeNet/Common/Sequence.aspx">
    </FRAMESET>
    <FRAMESET ROWS=<%=mstrHeight%> FRAMEBORDER="0">
    <%

If mstrCodisp = vbNullString Then
	mstrCodisp = mstrCodispl
End If

If mstrCodisp = "CO01_K" Then
	mstrCodisp = "CO001_K"
End If

If mstrCodisp = "CA001C" Then
	mstrCodisp = "CA001_K"
End If


Response.Write("<FRAME NAME=""fraHeader"" SCROLLING =""" & lstrScroll & """ FRAMEBORDER=""0"" TARGET=""fraFolder"" SRC=""../" & Request.QueryString.Item("sModule") & "/" & Request.QueryString.Item("sProject") & "/" & Replace(mstrCodisp, "_K", vbNullString) & "_K.aspx?sConfig=" & mstrConfig & lstrQueryString & """>")
If mstrConfig = "InSequence" Then
	Response.Write("<FRAME NAME=""fraFolder"" SRC=""Blank.aspx"">")
End If
Response.Write("<FRAME NAME=""fraGeneric"" FRAMEBORDER=0 SCROLLING=""Yes"" SRC=""Blank.aspx"">")
Response.Write("<FRAME NAME=""fraSubmit"" FRAMEBORDER=0 SCROLLING=""Yes"" SRC=""Blank.aspx"">")
%>
    </FRAMESET>
    <NOFRAMES>
        <BODY>
            <P>Esta página utiliza frame, pero su BROWSER no lo soporta</P>
        </BODY>
    </NOFRAMES>
</FRAMESET>
</HTML>




