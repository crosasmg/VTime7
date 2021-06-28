<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	mobjGrid.sCodisplPage = "DP011"
	
	With mobjGrid.Columns
		Call .AddNumericColumn(41273, GetLocalResourceObject("tcnBill_itemColumnCaption"), "tcnBill_item", 5, CStr(1),  , GetLocalResourceObject("tcnBill_itemColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddTextColumn(41274, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(41275, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString,  , GetLocalResourceObject("tctShort_desColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP011"
		.Width = 390
		.Height = 210
		.DeleteButton = False
		.bOnlyForQuery = Session("bQuery")
		.Columns("tctDescript").EditRecord = True
		If Request.QueryString.Item("nMainAction") = "401" Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
			.Columns("tctDescript").Disabled = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP011: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP011()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_bill_i As Object
	Dim lcolTab_bill_is As eProduct.Tab_bill_is
	Dim lblnExist As Boolean
	Dim lintCount As Short
	
	lcolTab_bill_is = New eProduct.Tab_bill_is
	
	lblnExist = False
	If lcolTab_bill_is.Find(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = 0
		With mobjGrid
			.DeleteButton = True
			For	Each lclsTab_bill_i In lcolTab_bill_is
				.Columns("tcnBill_item").DefValue = lclsTab_bill_i.nBill_item
				.Columns("tctDescript").DefValue = lclsTab_bill_i.sDescript
				.Columns("tctShort_des").DefValue = lclsTab_bill_i.sShort_des
				.sDelRecordParam = "nBill_item=' + marrArray[lintIndex].tcnBill_item  + '"
				.Columns("Sel").OnClick = "insDefValues(""DataAssociate"",""Index=" & lintCount & "&nBill_item=" & lclsTab_bill_i.nBill_item & """)"
				Response.Write(.DoRow)
				lintCount = lintCount + 1
			Next lclsTab_bill_i
		End With
		lblnExist = True
	End If
	Response.Write(mobjGrid.closeTable())
	If Not lblnExist And Not Session("bQuery") Then
		Response.Write(mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/FindPolicyOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "InitialValues()"))
	End If
	lclsTab_bill_i = Nothing
	lcolTab_bill_is = Nothing
End Sub

'% insPreDP011Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP011Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_bill_i As eProduct.Tab_bill_i
	
	lclsTab_bill_i = New eProduct.Tab_bill_i
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		Call lclsTab_bill_i.insPostDP011("DP011", "Del", Session("nBranch"), Session("nProduct"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nBill_item"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDescript"), Request.QueryString.Item("sShort_des"), Session("nUsercode"))
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP011", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	If Request.QueryString.Item("Action") = "Add" Then
		'+ Se calcula el Código del concepto
		Response.Write("<SCRIPT>GetNextCode()</" & "Script>")
	End If
	
	lclsTab_bill_i = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "DP011"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP011.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:01 $|$$Author: Nvaplat61 $"    

//% InitialValues: se inicializa el grid de la transacción, con los conceptos generales
//--------------------------------------------------------------------------------------------
function InitialValues(){
//--------------------------------------------------------------------------------------------
	insDefValues("Tab_bill_i")
}

//% GetNextCode: muestra el valor por defecto para el campo Código, si se está agregando
//--------------------------------------------------------------------------------------------
function GetNextCode(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el consecutivo más alto existente en el grid
	var llngMax = 0
	    
//+ Se genera el número consecutivo del Order
	for(var llngIndex = 0;llngIndex < top.opener.marrArray.length;llngIndex++)
	    if(eval(top.opener.marrArray[llngIndex].tcnBill_item)>eval(llngMax))
	        llngMax = top.opener.marrArray[llngIndex].tcnBill_item
	         
	if(eval(++llngMax.length) > eval(self.document.forms[0].tcnBill_item.maxLength))
//+ Se asigna null al campo, ya que el valor máximo no se puede ingresar en el campo
		self.document.forms[0].tcnBill_item.value = "";
	else
//+ Se asigna el valor por defecto del código
		self.document.forms[0].tcnBill_item.value = ++llngMax;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="fraContent" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP011Upd()
Else
	Call insPreDP011()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




