<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mstrMessage As String
Dim lclsGeneral As eGeneral.GeneralFunction
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object
Dim mclsErrors As eFunctions.Errors


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "DP010"
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(41269, GetLocalResourceObject("cboPay_fractiColumnCaption"), "cboPay_fracti", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  , "Disabled(1);",  ,  , GetLocalResourceObject("cboPay_fractiColumnCaption"))
		Call .AddNumericColumn(41271, GetLocalResourceObject("tcnQuotaColumnCaption"), "tcnQuota", 5, CStr(0),  , GetLocalResourceObject("tcnQuotaColumnToolTip"))
		Call .AddNumericColumn(41272, GetLocalResourceObject("tcnRatepayfColumnCaption"), "tcnRatepayf", 4, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnRatepayfColumnCaption"), , 2)
		Call .AddPossiblesColumn(41270, GetLocalResourceObject("cboStatregtColumnCaption"), "cboStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboStatregtColumnCaption"))
		Call .AddHiddenColumn("hddStatus", CStr(0))
		Call .AddHiddenColumn("hddAuxPay_fracti", CStr(0))
		Call .AddHiddenColumn("hddAuxQuota", CStr(0))
		Call .AddHiddenColumn("hddAuxRatepayf", CStr(0))
		Call .AddHiddenColumn("hddAuxStatregt", CStr(0))
		Call .AddHiddenColumn("hddAuxSel", CStr(2))
		Call .AddHiddenColumn("hddAuxPay_fracti_p", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP010"
		.Width = 400
		.Height = 220
		.DeleteButton = False
		.AddButton = False
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		ElseIf mstrTypeFind = "2" Then 
			.Columns("Sel").Title = ""
		ElseIf mstrTypeFind = "1" Then 
			.Columns("Sel").Title = ""
			.Columns("cboPay_fracti").EditRecord = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].hddAuxSel.length>0)document.forms[0].hddAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].hddAuxSel.value =(this.checked?1:2);"
	End With
End Sub
'% insPreDP01: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP010()
	'--------------------------------------------------------------------------------------------
	Dim lclsPay_fracti As ePolicy.Pay_Fracti
	Dim lcolPay_fracti As ePolicy.Pay_fractis
	Dim lclsProduct As eProduct.Product
	Dim lintCount As Short
	lclsProduct = New eProduct.Product
	mobjGrid.AddButton = True
	With Server
		lclsPay_fracti = New ePolicy.Pay_Fracti
		lcolPay_fracti = New ePolicy.Pay_fractis
	End With
	If lcolPay_fracti.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
		mobjGrid.DeleteButton = True
		lintCount = 0
		For	Each lclsPay_fracti In lcolPay_fracti
			With mobjGrid
				.Columns("cboPay_fracti").DefValue = CStr(lclsPay_fracti.nPayFreq)
				.Columns("tcnQuota").DefValue = CStr(lclsPay_fracti.nQuota)
				.Columns("tcnRatepayf").DefValue = CStr(lclsPay_fracti.nRatepayf)
				.Columns("cboStatregt").DefValue = lclsPay_fracti.sStatRegt
				.Columns("hddStatus").DefValue = CStr(1)
				.Columns("hddAuxPay_fracti").DefValue = CStr(lclsPay_fracti.nPayFreq)
				.Columns("hddAuxQuota").DefValue = CStr(lclsPay_fracti.nQuota)
				.Columns("hddAuxRatepayf").DefValue = CStr(lclsPay_fracti.nRatepayf)
				.Columns("hddAuxStatregt").DefValue = lclsPay_fracti.sStatRegt
				.Columns("hddAuxPay_fracti_p").DefValue = CStr(lclsPay_fracti.nPayFreq_p)
				.Columns("Sel").OnClick = "Validation(this," & lintCount & ")"
				.sDelRecordParam = "nPay_fracti=' +   marrArray[lintIndex].hddAuxPay_fracti + '&nQuota=' + marrArray[lintIndex].hddAuxQuota + '&nRatepayf=' + marrArray[lintIndex].hddAuxRatepayf + '&nStatregt=' + marrArray[lintIndex].hddAuxStatregt + '"
				Response.Write(.DoRow)
				lintCount = lintCount + 1
			End With
		Next lclsPay_fracti
	Else
		mblnVisible = True
	End If
	Response.Write(mobjGrid.closeTable())
	lclsPay_fracti = Nothing
	lcolPay_fracti = Nothing
End Sub
'% insPreDP010Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP010Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsPay_fracti As ePolicy.Pay_Fracti
	lclsPay_fracti = New ePolicy.Pay_Fracti
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsPay_fracti.insPostDP010(mobjValues.StringToType(CStr(2), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPay_fracti"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Request.QueryString.Item("nRatepayf"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nStatregt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nQuota"), eFunctions.Values.eTypeData.etdDouble, 0), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	End If
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP010", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>Disabled(2);</" & "Script>")
		End If
	End With
	lclsPay_fracti = Nothing
End Sub

</script>
<%Response.Expires = -1
mclsErrors = New eFunctions.Errors
mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
lclsGeneral = New eGeneral.GeneralFunction

mobjValues.sCodisplPage = "DP010"

mstrMessage = lclsGeneral.insLoadMessage(CInt("11381"))
If IsNothing(Request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If
mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:01 $"

//%Disabled: Deshabilita e inicializa los campos de acuerdo a la frecuencia de pago
//--------------------------------------------------------------------------------------------------
function Disabled(hddStatus)
//--------------------------------------------------------------------------------------------------
{
	if (self.document.forms[0].elements["cboPay_fracti"].value == 1 || self.document.forms[0].elements["cboPay_fracti"].value == 6) 
	{
		self.document.forms[0].elements["tcnQuota"].value = "0";
		self.document.forms[0].elements["tcnRatepayf"].value = "0";
		self.document.forms[0].elements["tcnQuota"].disabled=true;
		self.document.forms[0].elements["tcnRatepayf"].disabled=true;
	}
	if (self.document.forms[0].elements["cboPay_fracti"].value != 0 && self.document.forms[0].elements["cboPay_fracti"].value != 8 && self.document.forms[0].elements["cboPay_fracti"].value != 1 && self.document.forms[0].elements["cboPay_fracti"].value != 6)
	{
		self.document.forms[0].elements["tcnQuota"].value = "0";
		self.document.forms[0].elements["tcnQuota"].disabled=true;
		self.document.forms[0].elements["tcnRatepayf"].disabled=false;
	}
	if (self.document.forms[0].elements["cboPay_fracti"].value == 8)
	{	self.document.forms[0].elements["tcnQuota"].disabled=false;
		if (hddStatus == 2)
			self.document.forms[0].elements["tcnQuota"].disabled=true;
	}
}
//%Disabled: Deshabilita e inicializa los campos de acuerdo a la frecuencia de pago
//--------------------------------------------------------------------------------------------------
function Validation(Field, nIndex)
//--------------------------------------------------------------------------------------------------
{
	if (Field.checked)
	{
		if (marrArray.length == 1)
		{
			if (marrArray[nIndex].cboPay_fracti == marrArray[nIndex].hddAuxPay_fracti_p)
			{
				self.document.forms[0].elements["Sel"].checked = false
				alert('Err: 11381 : ' + '<%=mstrMessage%>')
			}
		}
		else
		if (marrArray[nIndex].cboPay_fracti == marrArray[nIndex].hddAuxPay_fracti_p)
		{
			self.document.forms[0].elements["Sel"][nIndex].checked = false;
			self.marrArray[nIndex].Sel=false;							
			alert('Err: 11381 : ' + '<%=mstrMessage%>')
		}
	}
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP010", "DP010.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP010" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP010"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP010Upd()
Else
	Call insPreDP010()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
mclsErrors = Nothing
lclsGeneral = Nothing
%>





