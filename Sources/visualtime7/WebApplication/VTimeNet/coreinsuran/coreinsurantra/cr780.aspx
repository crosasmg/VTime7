<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "CR780"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCovergenColumnCaption"), "valCovergen", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  , False,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valCovergenColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valClasriskColumnCaption"), "valClasrisk", "Table5563", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  , False,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valClasriskColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"), False, 6)
		
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CR780"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 500
		.WidthDelete = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("valCovergen").EditRecord = True
		If Request.QueryString.Item("Action") = "Add" Then
			.Columns("valCovergen").Disabled = False
			.Columns("valClasrisk").Disabled = False
		Else
			If Request.QueryString.Item("Action") = "Update" Then
				.Columns("valCovergen").Disabled = True
				.Columns("valClasrisk").Disabled = True
			End If
		End If
		
		.Columns("tcnRate").Disabled = False
		
		
		.sEditRecordParam = "nBranch_rei=" & Request.QueryString.Item("nBranch_rei") & "&nNumber=" & Request.QueryString.Item("nNumber") & "&nType=" & Request.QueryString.Item("nType") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.sDelRecordParam = "valCovergen='+ marrArray[lintIndex].valCovergen + '" & "&valClasrisk='+ marrArray[lintIndex].valClasrisk + '" & "&nRate='+ marrArray[lintIndex].tcnRate + '" & "&nBranch_rei=" & Request.QueryString.Item("nBranch_rei") & "&nNumber=" & Request.QueryString.Item("nNumber") & "&nType=" & Request.QueryString.Item("nType") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		
	End With
End Sub

'% insPreCR780: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR780()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_Cesrisk As eCoReinsuran.Tar_cesrisk
	Dim lcolTar_Cesrisks As eCoReinsuran.Tar_cesrisks
	Dim i As Integer
	
	Dim lblnFind As Boolean
	
	i = 0
	lclsTar_Cesrisk = New eCoReinsuran.Tar_cesrisk
	lcolTar_Cesrisks = New eCoReinsuran.Tar_cesrisks
	
	lblnFind = lcolTar_Cesrisks.Find(mobjValues.StringToType(Request.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	If lblnFind Then
		
		
		For i = 1 To lcolTar_Cesrisks.count
        'For i = 0 To lcolTar_Cesrisks.count -1
			With mobjGrid
				.Columns("valCovergen").DefValue = CStr(lcolTar_Cesrisks.Item(i).nCovergen)
				.Columns("valClasrisk").DefValue = CStr(lcolTar_Cesrisks.Item(i).nClass_risk)
				.Columns("tcnRate").DefValue = CStr(lcolTar_Cesrisks.Item(i).nRate)
				Response.Write(.DoRow)
			End With
		Next 
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCR780Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR780Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjCoReinsuranTra As eCoReinsuran.Tar_cesrisk
	
	lobjCoReinsuranTra = New eCoReinsuran.Tar_cesrisk
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			
			If lobjCoReinsuranTra.insPostCR780("CR780", .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valClasrisk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR780", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CR780"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR780", "CR780.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CR780" ACTION="valCoReinsuranTra.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CR780"))
Response.Write("<BR>")
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCR780Upd()
Else
	Call insPreCR780()
End If
%>
<SCRIPT LANGUAGE="JavaScript">
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 30/03/06 13:24 $" 
</SCRIPT>
</FORM> 
</BODY>
</HTML>






