<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

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
            Call .AddPossiblesColumn(8614, GetLocalResourceObject("cbeCategoryColumnCaption"), "cbeCategory", "Table240", eFunctions.Values.eValuesType.clngComboType, , , , , , , , 2, GetLocalResourceObject("cbeCategoryColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
            Call .AddNumericColumn(8615, GetLocalResourceObject("tcnExtraPremColumnCaption"), "tcnExtraPrem", 5, "", False, GetLocalResourceObject("tcnExtraPremColumnToolTip"), False, 2, , , , False)
            Call .AddNumericColumn(8616, GetLocalResourceObject("tcnDiscountColumnCaption"), "tcnDiscount", 5, "", False, GetLocalResourceObject("tcnDiscountColumnToolTip"), False, 2, , , , False)
        End With
	
        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MRO004"
            .Codisp = "MRO004"
            .Top = 100
            .Height = 224
            .Width = 595
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("cbeCategory").EditRecord = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 302
            .Columns("cbeCategory").Disabled = Request.QueryString.Item("Action") = "Update"
            .sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "&nCategory='+ marrArray[lintIndex].cbeCategory + '"
            .sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate")
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .AddButton = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd)
        End With
End Sub

'%insPreMRO004. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMRO004()
	'------------------------------------------------------------------------------
	Dim lcolTar_builds As eBranches.Tar_builds
	Dim lclsTar_build As Object
	
	With Request
		lcolTar_builds = New eBranches.Tar_builds
            With mobjGrid
                If lcolTar_builds.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
                    For Each lclsTar_build In lcolTar_builds
                        .Columns("cbeCategory").DefValue = lclsTar_build.nCategory
                        .Columns("tcnExtraPrem").DefValue = lclsTar_build.nExtraPrem
                        .Columns("tcnDiscount").DefValue = lclsTar_build.nDiscount
                        Response.Write(mobjGrid.DoRow())
                    Next lclsTar_build
                End If
            End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsTar_build = Nothing
	lcolTar_builds = Nothing
End Sub

'% insPreMRO004Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMRO004Upd()
	'------------------------------------------------------------------------------
	Dim lclsTar_build As eBranches.Tar_build
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTar_build = New eBranches.Tar_build
			Call lclsTar_build.InsPostMRO004(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCategory"), eFunctions.Values.eTypeData.etdInteger), 0, 0)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valmanttheft.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTar_build = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MRO004")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MRO004"
'~End Body Block VisualTimer Utility	
%>
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
            .Write(mobjMenu.setZone(2, "MRO004", "MRO004.aspx"))
            mobjMenu = Nothing
        End If
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMRO004" ACTION="valmanttheft.aspx?sZone=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
    
    Call insDefineHeader()
    
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreMRO004()
    Else
        Call insPreMRO004Upd()
    End If
    
    mobjValues = Nothing
    mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Call mobjNetFrameWork.FinishPage("MRO004")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








