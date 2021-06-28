<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'Dim insPreCO982Upd() As Object
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues

Dim mstrKey As String
Dim mstrRead As String
Dim mstrquery As String
Dim nTotRow As Object

Dim lobjGeneral As eGeneral.GeneralFunction

'Response.Write mobjValues.ShowWindowsName("CO982", Request.QueryString("sWindowDescript"))
Dim sLocacion As String


'% insDefineHeader: Definición de los campos del grid.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	Response.Write(mobjValues.HiddenControl("sKey", ""))
	mobjGrid.sCodisplPage = "co982"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		.Codispl = "CO982"
		.Codisp = "CO982"
		.DeleteButton = False
		.AddButton = (Request.QueryString.Item("sProcess") = "1" And CDbl(Request.QueryString.Item("nWay_pay")) <> 3)
		.Height = 500
		.Width = 410
		.Top = 120
		.Left = 350
	End With
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctnBankColumnCaption"), "tctnBank", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctnBankColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescnBankColumnCaption"), "tctDescnBank", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDescnBankColumnCaption"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctAccountColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tctbulletinsColumnCaption"), "tctbulletins", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tctbulletinsColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, "",  , GetLocalResourceObject("tcnReceiptColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  , "insShowData(this)", Request.QueryString.Item("sProcess") = "2" Or Request.QueryString.Item("Action") = "Update")
		Call .AddDateColumn(0, GetLocalResourceObject("tcndNextreceipColumnCaption"), "tcndNextreceip",  ,  , GetLocalResourceObject("tcndNextreceipColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", 12, "",  , GetLocalResourceObject("valProductColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("valProductDescColumnCaption"), "valProductDesc", 12, "",  , GetLocalResourceObject("valProductDescColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctCauseColumnCaption"), "tctCause", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctCauseColumnCaption"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescCauseColumnCaption"), "tctDescCause", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDescCauseColumnCaption"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6,  ,  ,  , True)
		mstrquery = "dExpirdat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&sKey=" & mstrKey & "&nBank=" & Request.QueryString.Item("nBank") & "&nCauseNull=" & Request.QueryString.Item("nCauseNull") & "&sProcess=" & Request.QueryString.Item("sProcess") & "&nRow=" & Request.QueryString.Item("nRow") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree")
		Call .AddHiddenColumn("tctquerystring", mstrquery)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		'+ Si la transacción es "Consulta", se oculta la columna SEL 
		.sEditRecordParam = "dExpirdat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&nBank=" & Request.QueryString.Item("nBank") & "&nCauseNull=" & Request.QueryString.Item("nCauseNull") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree") & "&sKey=" & mstrKey & "&sProcess=" & Request.QueryString.Item("sProcess")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreCO982: Se obtiene la información para llenar el grid.
'------------------------------------------------------------------------------
Private Sub insPreCO982()
	'------------------------------------------------------------------------------
	Dim lcolReject_causes As eCollection.Reject_causes
	Dim lclsReject_cause As eCollection.Reject_cause
	Dim lintIndex As Short
	Dim llngTotRow As Object
	Dim llngRow As Byte
	Dim ldblTotalImp As Object
	
	lcolReject_causes = New eCollection.Reject_causes
	lclsReject_cause = New eCollection.Reject_cause
	
	Response.Write("<SCRIPT>self.document.forms[0].sKey.value ='" & mstrKey & "';</" & "Script>")
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""2%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""4"">")


Response.Write(mobjValues.AnimatedButtonControl("btndeleteoff", "/VTimeNet/images/btnDeleteOff.png", GetLocalResourceObject("btndeleteoffToolTip"),  , "CallCO982(0)", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""4"">")


Response.Write(mobjValues.AnimatedButtonControl("btnaddoff", "/VTimeNet/images/btnAddOff.png", GetLocalResourceObject("btnaddoffToolTip"),  , "CallCO982(1)", False))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	
	If lcolReject_causes.FindCO982(Session("mstrKey")) Then
		
		sLocacion = mstrKey
		lintIndex = 0
		
		For	Each lclsReject_cause In lcolReject_causes
			With lclsReject_cause
				mobjGrid.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				mobjGrid.Columns("Sel").checked = CShort(.sSel)
				mobjGrid.Columns("tctnBank").DefValue = CStr(.nBank_code)
				mobjGrid.Columns("tctDescnBank").DefValue = .sDescbankcode
				mobjGrid.Columns("tctAccount").DefValue = .sDocument
				mobjGrid.Columns("tctBulletins").DefValue = CStr(.nBulletins)
				mobjGrid.Columns("tcnReceipt").DefValue = CStr(.nReceipt)
				mobjGrid.Columns("tcnPolicy").DefValue = CStr(.nPolicy)
				mobjGrid.Columns("valProduct").DefValue = CStr(.nProduct)
				mobjGrid.Columns("valProductDesc").DefValue = .sProduct
				mobjGrid.Columns("tctCause").DefValue = CStr(.nRejectcause)
				mobjGrid.Columns("tctDescCause").DefValue = .sDesc_Rejectcause
				mobjGrid.Columns("tcnPremium").DefValue = CStr(.nPremium)
				mobjGrid.Columns("tcndNextreceip").DefValue = CStr(.dNextreceip)
				lintIndex = lintIndex + 1
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsReject_cause
	End If
	
	If IsNothing(Request.QueryString.Item("nRow")) Then
		llngRow = 50
	Else
		llngRow = CDbl(Request.QueryString.Item("nRow")) + 49
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("tcnString", sLocacion))
	
	lclsReject_cause = Nothing
	lcolReject_causes = Nothing
End Sub

'% insPreCO501Upd: Se hace el llamado a la ventana PopUp.
'------------------------------------------------------------------------------
Private Sub insPreCO501Upd()
	'------------------------------------------------------------------------------
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valCollectionTra.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co982")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co982"
If IsNothing(Request.QueryString.Item("sKey")) Then
	lobjGeneral = New eGeneral.GeneralFunction
	mstrKey = lobjGeneral.getsKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	lobjGeneral = Nothing
	mstrRead = "1"
	
Else
	mstrKey = Request.QueryString.Item("sKey")
	mstrRead = "0"
End If

%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	
    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenues = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
		mobjMenues.sSessionID = Session.SessionID
		mobjMenues.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenues.setZone(2, "CO982", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenues = Nothing
	End If
	.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	.Write("<SCRIPT>var	sLocacion	= 'CO982.aspx?nMainAction=304" & "&dExpirDat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&nBank=" & Request.QueryString.Item("nBank") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree") & " ';</SCRIPT>")
End With
%>

<SCRIPT>
var lstrquerystring = <%=mstrquery%>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 8/10/09 3:15p $|$$Author: Gletelier $"
	     
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow;
    lstrURL = lstrURL.replace(/&sKey=.*/,'')
	lstrURL = lstrURL + "&sKey=" + "<%=mstrKey%>"

	<%If Not IsNothing(Request.QueryString.Item("nRow")) Then%>
	    llngRow = <%=Request.QueryString.Item("nRow")%>;
	<%End If%>
	
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}	
	self.document.location.href = lstrURL;
	
}

//% insTotRow: Agrega a la url el valor del total de filas seleccionadas por la consulta
//-------------------------------------------------------------------------------------------
function insTotRow(nTotRow){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href    
//	if (lstrURL.indexOf("&nTotRow=")<=0)
//	    self.document.location.href = lstrURL + "&nTotRow=" + nTotRow ;	
}

//% insReload: Recarga la página desde el showdefvalues agregándole el sKey
//-------------------------------------------------------------------------------------------
function insReload(sKey){
//-------------------------------------------------------------------------------------------

    var lstrURL = self.document.location.href    
    lstrURL = lstrURL.replace(/&sKey=.*/,'')	
	self.document.location.href = lstrURL + "&sKey=" + sKey ;
}

//% insCheckSelClick: Al cambiar el valor de la columna sel.
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		insDefValues("insUpdSelCO982", "nBulletins=" + marrArray[lintIndex].tctbulletins + "&nPolicy=" + marrArray[lintIndex].tcnPolicy + "&sKey=" + '<%=Session("mstrKey")%>' + "&sSel=" + (Field.checked?'1':'0'));
	}
}

//% ShowDataCO501: Al cambiar el valor de la columna sel.
//-------------------------------------------------------------------------------------------
function insShowData(Field){
//-------------------------------------------------------------------------------------------
    llngWay_pay = '<%=Request.QueryString.Item("nWay_pay")%>'
    ldtmEffecDate = '<%=Request.QueryString.Item("dExpirdat")%>'
    llngBank = '<%=mobjValues.TypeToString(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdLong)%>'
    llngcod_agree= '<%=Request.QueryString.Item("ncod_agree")%>'
    
    if (Field.value!=0)
		insDefValues("ShowDataCO501","nPolicy=" + Field.value + "&nWay_pay=" + llngWay_pay + "&dEffecDate=" + ldtmEffecDate + "&nBank=" + llngBank + "&ncod_agree="+llngcod_agree);
	else{
		self.document.forms[0].tctClient.value='';
		self.document.forms[0].tctCliename.value='';
		self.document.forms[0].tctnBank.value='';
		self.document.forms[0].tctDocument.value='';
		self.document.forms[0].tctAccount.value='';
		self.document.forms[0].tctbulletins.value='';
		self.document.forms[0].cbeBranch.value=0;
		self.document.forms[0].valProduct.value='';
		self.document.forms[0].tcnReceipt.value='';
		self.document.forms[0].tcnDraft.value='';
		self.document.forms[0].tctAmount.value='';
		self.document.forms[0].tctCause.value=0;
	}
}

//% insChangeCause: Al cambiar el valor del campo general causa de rechazo.
//-------------------------------------------------------------------------------------------
function insChangeCause(Field){
//-------------------------------------------------------------------------------------------
      top.fraFolder.document.location=top.fraFolder.document.location.href.replace(/&nCauseNull.*/,'') + "&nCauseNull=" + Field.value;
  }

//% CallCO982: Marca o desmarca todos los registros
//-------------------------------------------------------------------------------------------
function CallCO982(nField){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		insDefValues("InsUpdCO982check",'sSel=' + nField  + '&sKey=' + '<%=Session("mstrKey")%>');
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CO982" ACTION="valCollectionTra.aspx?sMode=2<%=Request.Params.Get("Query_String")%>">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCO982()
'Else
	'Call insPreCO982Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co501")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





