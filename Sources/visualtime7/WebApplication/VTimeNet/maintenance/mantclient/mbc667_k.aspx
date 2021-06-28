<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolTab_req_docs As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		
		If Request.QueryString.Item("Action") = "Update" Then
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenTypeDocColumnCaption"), "cbenTypeDoc", "Table32", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenTypeDocColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenTypeDocColumnCaption"), "cbenTypeDoc", "Table32", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTypeDocColumnToolTip"))
		End If
		
		Call .AddCheckColumn(0, GetLocalResourceObject("chksRequireColumnCaption"), "chksRequire", vbNullString, CShort("0"),  ,  , Request.QueryString.Item("Type") <> "PopUp", GetLocalResourceObject("chksRequireColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQDaysColumnCaption"), "tcnQDays", 5, vbNullString, ,GetLocalResourceObject("tcnQDaysColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCostColumnCaption"), "tcnCost", 18, CStr(0),  , GetLocalResourceObject("tcnCostColumnToolTip") , True, 6)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbesStatregtColumnCaption"), "cbesStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbesStatregtColumnCaption"))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MBC667"
		.Codisp = "MBC667_K"
		.sCodisplPage = "MBC667"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		Else
			.Columns("cbenTypeDoc").EditRecord = True
		End If
		.Top = 200
		.Left = 300
		.Height = 280
		.Width = 350
		.sDelRecordParam = "nTypeDoc='+ marrArray[lintIndex].cbenTypeDoc + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMBC667: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMBC667()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_req_doc As eClient.Tab_req_doc
	Dim lcolTab_req_docs As eClient.Tab_req_docs
	
	With Server
		lclsTab_req_doc = New eClient.Tab_req_doc
		lcolTab_req_docs = New eClient.Tab_req_docs
	End With
	
	If lcolTab_req_docs.Find() Then
		For	Each lclsTab_req_doc In lcolTab_req_docs
			With mobjGrid
				.Columns("cbenTypeDoc").DefValue = CStr(lclsTab_req_doc.nTypeDoc)
				.Columns("chksRequire").Checked = CShort(lclsTab_req_doc.sRequire)
				.Columns("tcnQDays").DefValue = CStr(lclsTab_req_doc.nQDays)
				.Columns("tcnCost").DefValue = CStr(lclsTab_req_doc.nCost)
				.Columns("cbesStatregt").DefValue = lclsTab_req_doc.sStatregt
				Response.Write(.DoRow)
			End With
		Next lclsTab_req_doc
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMBC667Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMBC667Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjTab_req_doc As eClient.Tab_req_doc
	
	lobjTab_req_doc = New eClient.Tab_req_doc
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjTab_req_doc.insPostMBC667(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.QueryString.Item("nTypeDoc"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chksRequire"), mobjValues.StringToType(Request.Form.Item("tcnQDays"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbesStatregt"), mobjValues.StringToType(Request.Form.Item("tcnCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantClient.aspx", "MBC667", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
mobjValues.sCodisplPage = "MBC667"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

    
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>	
	   <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantclient\VTime\Scripts\tmenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%	
End If
%>		
	<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MBC667_K.aspx", 1, ""))
	Response.Write("<BR></BR>")
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	
End If
%>
 
<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 30/10/03 19:53 $"
    
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insPreZone: Define ubicacion de documento
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MBC667" ACTION="ValMantClient.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MBC667"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMBC667Upd()
Else
	Call insPreMBC667()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>





