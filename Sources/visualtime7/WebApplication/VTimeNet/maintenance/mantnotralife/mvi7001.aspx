<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objetos genéricos para manejo de valores, menú y grilla.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader: Definición de las columnas del Grid.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid.Columns
		'+ Columna nueva en el grid por cambios APV2 - ACM - 13/08/2003
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_costColumnCaption"), "cbeType_cost", "Table5661", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeType_costColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonth_FromColumnCaption"), "tcnMonth_From", 4, vbNullString, False, GetLocalResourceObject("tcnMonth_FromColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMonth_UntilColumnCaption"), "tcnMonth_Until", 4, vbNullString, False, GetLocalResourceObject("tcnMonth_UntilColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCost_amountColumnCaption"), "tcnCost_amount", 14, vbNullString, False, GetLocalResourceObject("tcnCost_amountColumnToolTip"), True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		'+ Columna nueva en el grid por cambios APV2 - ACM - 13/08/2003
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 8, vbNullString, False, GetLocalResourceObject("tcnRateColumnToolTip"), True, 2)
		'+ Columna nueva en el grid por cambios APV2 - ACM - 13/08/2003
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTopAmountColumnCaption"), "tcnTopAmount", 12, vbNullString, False, GetLocalResourceObject("tcnTopAmountColumnToolTip"), True, 2)
		Call .AddCheckColumn(0, GetLocalResourceObject("sCreDebColumnCaption"), "sCreDeb", "",  , CStr(2),  ,  , GetLocalResourceObject("sCreDebColumnToolTip"))
		
		Call .AddHiddenColumn("nCreDeb", CStr(0))
	End With
	
	With mobjGrid
		.Columns("tcnMonth_From").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		.Columns("tcnMonth_Until").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		.Codispl = "MVI7001"
		.Codisp = "MVI7001"
		.sCodisplPage = "MVI7001"
		.Columns("tcnMonth_From").EditRecord = True
		.AddButton = True
		.DeleteButton = True
		.Height = 350
		.Width = 350
		.Top = 100
		
		.sDelRecordParam = "nMonth_From=' + marrArray[lintIndex].tcnMonth_From + '" & "&nMonth_Until=' + marrArray[lintIndex].tcnMonth_Until + '" & "&nType_cost=' + marrArray[lintIndex].cbeType_cost + '" & "&nRate=' + marrArray[lintIndex].tcnRate + '" & "&nTopAmount=' + marrArray[lintIndex].tcnTopAmount + '"
		
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("sCreDeb").Disabled = False
		Else
			.Columns("sCreDeb").Disabled = True
		End If
		
		.Columns("sCreDeb").OnClick = "insHandleGrid(this)"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI7001: Muestra la grilla con datos.
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreMVI7001()
	'--------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insPreZone: Define ubicación del documento." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	Dim lintCount As Short
	Dim lobjObject As Object
	Dim lcolTab_Ul_Costss As eBranches.Tab_Ul_Costss
	
	lcolTab_Ul_Costss = New eBranches.Tab_Ul_Costss
	
	If lcolTab_Ul_Costss.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		lintCount = 0
		
		For	Each lobjObject In lcolTab_Ul_Costss
			With lobjObject
				mobjGrid.Columns("tcnMonth_From").DefValue = .nMonth_From
				mobjGrid.Columns("tcnMonth_Until").DefValue = .nMonth_Until
				mobjGrid.Columns("tcnCost_amount").DefValue = .nCost_amount
				mobjGrid.Columns("cbeCurrency").DefValue = .nCurrency
				
				If .sCreDeb <> "2" And .sCreDeb <> "" Then
					mobjGrid.Columns("nCreDeb").DefValue = CStr(1)
					mobjGrid.Columns("sCreDeb").Checked = 1
				Else
					mobjGrid.Columns("nCreDeb").DefValue = CStr(2)
					mobjGrid.Columns("sCreDeb").Checked = 2
					mobjGrid.Columns("sCreDeb").Checked = False
				End If
				'+ Columnas nuevas. Cambios [APV2] - ACM - 13/08/2003
				mobjGrid.Columns("cbeType_cost").DefValue = .nType_cost
				mobjGrid.Columns("tcnRate").DefValue = .nRate
				mobjGrid.Columns("tcnTopAmount").DefValue = .nMax_amou
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 1000 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolTab_Ul_Costss = Nothing
	lobjObject = Nothing
End Sub

'% insPreMVI7001Upd: Muestra ventana para actualizar registros.
'-----------------------------------------------------------------------------------------
Private Sub insPreMVI7001Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsTab_Ul_costs As eBranches.Tab_Ul_Costs
	
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_Ul_costs = New eBranches.Tab_Ul_Costs
		
		'		If lclsTab_Ul_costs.insPostMVI7001("Del",'									        mobjValues.StringToType(Session("nBranch"),eFunctions.Values.eTypeData.etdDouble), '									        mobjValues.StringToType(Session("nProduct"),eFunctions.Values.eTypeData.etdDouble), '									        mobjValues.StringToType(Request.QueryString("nMonth_From"),eFunctions.Values.eTypeData.etdDouble), '											mobjValues.StringToType(Request.QueryString("nMonth_Until"),eFunctions.Values.eTypeData.etdDouble), '									        eRemoteDB.Constants.intNull, '									        eRemoteDB.Constants.intNull, '									        vbNullString, '									        mobjValues.StringToType (Session("nUsercode"),eFunctions.Values.eTypeData.etdDouble)) Then
		
		If lclsTab_Ul_costs.insPostMVI7001("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nMonth_From"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nMonth_Until"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nType_cost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTopAmount"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			Response.Write(mobjValues.ConfirmDelete())
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
		End If
		
		lclsTab_Ul_costs = Nothing
	Else
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	End If
End Sub

</script>
<%Response.Expires = -1

'- Nombre de tabla general.

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MVI7001"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>





<%
With Response
	.Write(mobjValues.StyleSheet())
	
	.Write("<SCRIPT>var sAction='" & Request.QueryString.Item("Action") & "'</SCRIPT>")%>	
	    
<%	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MVI7001", "MVI7001"))
		
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>

//- Variable para el control de versiones

    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:10 $|$$Author: Nvaplat61 $"

//% insCancel: Ejecuta la acción del botón cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insStateZone: Habilita o deshabilita los controles.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//% insHandleGrid: Esta función permite marcar la columna oculta.
//-------------------------------------------------------------------------------------------
function insHandleGrid(Field){
//-------------------------------------------------------------------------------------------
//+ Se actualiza la columna oculta con la marcada.
 
    if (Field.checked)
        self.document.forms[0].nCreDeb.value = 1
    else self.document.forms[0].nCreDeb.value = 2  
}    

</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MVI7001" ACTION="valMantNoTraLife.aspx?mode=1">


<%
'&sCodispl=MVI7001
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMVI7001()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMVI7001Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






