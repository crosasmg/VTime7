<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Se define la variable mobjGrid para el manejo del Grid de la ventana
    Dim mobjGrid As eFunctions.Grid

    '- Objeto para el manejo de las zonas de la página
    Dim mobjMenu As eFunctions.Menues


    '%insDefineHeader: Se definen las columnas del grid
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
	
        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCap_initColumnCaption"), "tcnCap_init", 18, "", True, GetLocalResourceObject("tcnCap_initColumnTooltip"), False, 6, , , , False)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCap_endColumnCaption"), "tcnCap_end", 18, "", False, GetLocalResourceObject("tcnCap_endColumnTooltip"), False, 6, , , , False)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 5, "", False, GetLocalResourceObject("tcnRateColumnTooltip"), False, 2, , , , False)
        End With
	
        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MRC003"
            .Codisp = "MRC003"
            .Top = 100
            .Height = 224
            .Width = 505
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("tcnCap_init").EditRecord = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 302
            .Columns("tcnCap_init").Disabled = Request.QueryString.Item("Action") = "Update"
            .Columns("tcnCap_end").Disabled = Request.QueryString.Item("Action") = "Update"
            .sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nCap_init='+ marrArray[lintIndex].tcnCap_init + '"
            .sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate")
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '%insPreMRC003. Se crea la ventana madre (Principal)
    '------------------------------------------------------------------------------
    Private Sub insPreMRC003()
        '------------------------------------------------------------------------------
        Dim lcolTar_rc_dess As eBranches.Tar_rc_dess
        Dim lclsTar_rc_des As Object
	
        With Request
            lcolTar_rc_dess = New eBranches.Tar_rc_dess
            With mobjGrid
                If lcolTar_rc_dess.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
                    For Each lclsTar_rc_des In lcolTar_rc_dess
                        .Columns("tcnCap_init").DefValue = lclsTar_rc_des.nCap_init
                        .Columns("tcnCap_end").DefValue = lclsTar_rc_des.nCap_end
                        .Columns("tcnRate").DefValue = lclsTar_rc_des.nRate
                        Response.Write(mobjGrid.DoRow())
                    Next lclsTar_rc_des
                End If
            End With
		
        End With
        Response.Write(mobjGrid.closeTable())
	
        lclsTar_rc_des = Nothing
        lcolTar_rc_dess = Nothing
    End Sub

    '% insPreMRC003Upd. Se define esta funcion para contruir el contenido de la 
    '%                     ventana UPD de los archivos de datos particulares
    '------------------------------------------------------------------------------
    Private Sub insPreMRC003Upd()
        '------------------------------------------------------------------------------
        Dim lclsTar_rc_des As eBranches.Tar_rc_des
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclsTar_rc_des = New eBranches.Tar_rc_des
                Call lclsTar_rc_des.InsPostMRC003(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCap_init"), eFunctions.Values.eTypeData.etdDouble), 0, 0)
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmantliability.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
        lclsTar_rc_des = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjValues = New eFunctions.Values
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "MRC003"
%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->


<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
    mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
    With Response
        .Write(mobjValues.StyleSheet())
        .Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
        If Request.QueryString.Item("Type") <> "PopUp" Then
            mobjMenu = New eFunctions.Menues
            .Write(mobjMenu.setZone(2, "MRC003", "MRC003.aspx", CShort(Request.QueryString.Item("nWindowTy"))))
            mobjMenu = Nothing
        End If
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMRC003" ACTION="valmantliability.aspx?sZone=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
    Call insDefineHeader()
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreMRC003()
    Else
        Call insPreMRC003Upd()
    End If
    mobjValues = Nothing
    mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>








