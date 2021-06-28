<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues



'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
'% o inhabilitar los botones de añadir y eliminar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngactionquery)
	
	Response.Write(mobjValues.ShowWindowsName("MS003") & "<BR>")
	
	With mobjGrid
		With .Columns
			.AddNumericColumn(101877, GetLocalResourceObject("nBk_agencyColumnCaption"), "nBk_agency", 4, CStr(0), True, GetLocalResourceObject("nBk_agencyColumnToolTip"), False)
			.AddTextColumn(101878, GetLocalResourceObject("sDescriptColumnCaption"), "sDescript", 30, vbNullString, True, GetLocalResourceObject("sDescriptColumnToolTip"))
			.AddTextColumn(101879, GetLocalResourceObject("sShort_desColumnCaption"), "sShort_des", 12, vbNullString, True, GetLocalResourceObject("sShort_desColumnToolTip"))
			.AddPossiblesColumn(101876, GetLocalResourceObject("sStatregtColumnCaption"), "sStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("sStatregtColumnToolTip"))
		End With
		
		.Height = 230
		.Width = 400
		.Codispl = "MS003"
		.sCodisplPage = "MS003"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("sDescript").EditRecord = Not .ActionQuery
		.sDelRecordParam = "nBk_agency='+ marrArray[lintIndex].nBk_agency + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMSI003upd: Esta función permite Actualizar un registro del Grid 
'-------------------------------------------------------------------------------------------
Private Sub inspreMS003upd()
	'-------------------------------------------------------------------------------------------
	Dim lobjError As eFunctions.Errors
	Dim lclsBank_acc As eCashBank.Bank_acc
	Dim lclsBank_trans As eCashBank.Bank_trans
	Dim lclsTab_bk_age As eCashBank.Tab_bk_age
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsBank_acc = New eCashBank.Bank_acc
			lclsBank_trans = New eCashBank.Bank_trans
			lclsTab_bk_age = New eCashBank.Tab_bk_age
			lobjError = New eFunctions.Errors
			
			If lclsBank_acc.Find_v(Session("nBank_code"), CInt(.QueryString.Item("nBk_agency"))) Then
				lobjError.Highlighted = True
				Response.Write(lobjError.ErrorMessage("MS003", 10305,  ,  ,  , True))
			Else
				If lclsBank_trans.Find_v(Session("nBank_code"), CInt(.QueryString.Item("nBk_agency"))) Then
					lobjError.Highlighted = True
					Response.Write(lobjError.ErrorMessage("MS003", 10306,  ,  ,  , True))
				Else
					If lclsTab_bk_age.Delete(Session("nBank_code"), .QueryString("nBk_agency")) Then
						Response.Write(mobjValues.ConfirmDelete())
					End If
				End If
			End If
			
			lobjError = Nothing
			lclsBank_acc = Nothing
			lclsBank_trans = Nothing
			lclsTab_bk_age = Nothing
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneral.aspx", "MS003", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

'% insPreMSI003: Esta función permite realizar la lectura de la tabla principal de la transacción.
'-------------------------------------------------------------------------------------------
Private Sub inspreMS003()
	'-------------------------------------------------------------------------------------------
	Dim lcolTab_bk_ages As eCashBank.Tab_bk_ages
	Dim lclsTab_bk_age As eCashBank.Tab_bk_age
	
	lcolTab_bk_ages = New eCashBank.Tab_bk_ages
	lclsTab_bk_age = New eCashBank.Tab_bk_age
	
	If lcolTab_bk_ages.Find(Session("nBank_code")) Then
		With mobjGrid
			For	Each lclsTab_bk_age In lcolTab_bk_ages
				.Columns("nBk_agency").DefValue = CStr(lclsTab_bk_age.nBk_agency)
				.Columns("sDescript").DefValue = lclsTab_bk_age.sDescript
				.Columns("sShort_des").DefValue = lclsTab_bk_age.sShort_des
				.Columns("sShort_des").DefValue = lclsTab_bk_age.sShort_des
				.Columns("sStatregt").DefValue = lclsTab_bk_age.sStatregt
				Response.Write(.DoRow)
			Next lclsTab_bk_age
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
	lcolTab_bk_ages = Nothing
	lclsTab_bk_age = Nothing
End Sub

</script>
<%Response.Expires = -1
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
		
	<%
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS003"

Response.Write(mobjValues.StyleSheet())

mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "MS003", "MS003.aspx"))
End If
%>
		
<SCRIPT LANGUAGE="JavaScript">
	var nMainAction = 304;
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MS003" ACTION="valMantGeneral.aspx?Time=1">
	<%Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreMS003()
Else
	inspreMS003upd()
End If

mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




