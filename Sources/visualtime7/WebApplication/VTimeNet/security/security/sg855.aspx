<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores

    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenues As eFunctions.Menues



    '%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
    '%del grid.
    '---------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        mobjGrid.sCodisplPage = "SG855"

        '+Se definen todas las columnas del Grid.
        With mobjGrid.Columns
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valFolderColumnCaption"), "valFolder", "TABFOLDERS", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  , "", Request.QueryString.Item("Action") = "Update", 8, GetLocalResourceObject("valFolderColumnToolTip"), eFunctions.Values.eTypeCode.eString)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnInqLevelColumnCaption"), "tcnInqLevel", 1,  , True, GetLocalResourceObject("tcnInqLevelColumnToolTip"))
            Call .AddCheckColumn(0, GetLocalResourceObject("chkPermittedColumnCaption"), "chkPermitted", "",  ,  ,  , Request.QueryString.Item("type") <> "PopUp", GetLocalResourceObject("chkPermittedColumnToolTip"))
        End With


        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "SG855"
            .Columns("valFolder").EditRecord = True
            .Width = 500
            .Height = 200

            '+ Si la acción que viaja a través del QueryString es Consulta (401), Elimiación (303) o el
            '+ parámetro nMainAction tiene valor NULO (vbNUllString o ""), la propiedad ActionQuery se setea en TRUE,
            '+ de lo contrario se setea en FALSE
            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
                .Columns("Sel").GridVisible = False
                .ActionQuery = True
            Else
                .Columns("Sel").GridVisible = True
                .ActionQuery = False
            End If
            .sDelRecordParam = "nFolder=' + marrArray[lintIndex].valFolder + '"
        End With
    End Sub

    '%insPreSG855: Esta ventana se encarga de mostrar en el grid los valores leídos.
    '---------------------------------------------------------------------------------------
    Private Sub insPreSG855()
        '---------------------------------------------------------------------------------------
        Dim lclsFolder As Object
        Dim lcolFolders As eSecurity.SchemaFolders
        Dim llngIndex As Byte

        lcolFolders = New eSecurity.SchemaFolders

        If lcolFolders.Find(Session("sSche_codeWin")) Then
            llngIndex = 0
            For Each lclsFolder In lcolFolders
                With mobjGrid
                    .Columns("valFolder").DefValue = lclsFolder.nFolder
                    .Columns("tcnInqLevel").DefValue = lclsFolder.nInqLevel
                    .Columns("chkPermitted").Checked = lclsFolder.sPermitted
                    Response.Write(mobjGrid.DoRow())
                End With
            Next lclsFolder
        End If

        lclsFolder = Nothing
        lcolFolders = Nothing

        Response.Write(mobjGrid.CloseTable())
    End Sub

    '%insPreSG002Upd: Permite realizar el llamado a la ventana PopUp.
    '-----------------------------------------------------------------------------------------
    Private Sub insPreSG855Upd()
        '-----------------------------------------------------------------------------------------
        Dim lclsFolder As eSecurity.SchemaFolder
        If Request.QueryString.Item("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())

            lclsFolder = New eSecurity.SchemaFolder

            Call lclsFolder.PostSG855(Session("sSche_codeWin"), CInt(Request.QueryString.Item("nFolder")), CStr(0), CStr(0), 0, "DELETE")
            Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Security/Security/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</" & "Script>")
        End If

        lclsFolder = Nothing

        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValSecuritySeqSchema.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))

    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG855"
%>
<HTML>
<HEAD> 
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




	
<%
mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "SG855", "SG855.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG855"))
	.Write(mobjValues.StyleSheet())
End With
%>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG855" ACTION="ValSecuritySeqSchema.aspx?Time=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

   <%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("SG855"))
	Call insPreSG855()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreSG855Upd()
End If

%>
   
</FORM>
</BODY>
</HTML>








