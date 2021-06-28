<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
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
	mobjGrid.sCodisplPage = "co501"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		.Codispl = "CO501"
		.Codisp = "CO501"
		.DeleteButton = False
		.AddButton = (Request.QueryString.Item("sProcess") = "1" And CDbl(Request.QueryString.Item("nWay_pay")) <> 3)
		.Height = 500
		.Width = 410
		.Top = 120
		.Left = 350
	End With
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 15, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 30, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctClienameColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctnBankColumnCaption"), "tctnBank", "table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tctnBankColumnCaption"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDocumentColumnCaption"), "tctDocument", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDocumentColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 25, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctAccountColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tctbulletinsColumnCaption"), "tctbulletins", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tctbulletinsColumnToolTip"),  ,  ,  ,  ,  , True)
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 12, "",  , GetLocalResourceObject("tctBranchColumnToolTip"),  ,  ,  , True)
			Call .AddTextColumn(0, GetLocalResourceObject("valProductDescColumnCaption"), "valProductDesc", 12, "",  , GetLocalResourceObject("valProductDescColumnToolTip"),  ,  ,  , True)
			Call .AddHiddenColumn("cbeBranch", "")
			Call .AddHiddenColumn("valProduct", "")
		Else
			Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct", "",  ,  ,  , True)
			Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "cbeBranch",  ,  ,  ,  ,  , True)
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  , "insShowData(this)", Request.QueryString.Item("sProcess") = "2" Or Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, "",  , GetLocalResourceObject("tcnReceiptColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 10, "",  , GetLocalResourceObject("tcnDraftColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tctAmountColumnCaption"), "tctAmount", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tctAmountColumnCaption"), True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tctCauseColumnCaption"), "tctCause", "tabReject_cause", eFunctions.Values.eValuesType.clngComboType,  , True)
		
		Call .AddHiddenColumn("tcdExpirDatHdr", Request.QueryString.Item("dExpirDat"))
		Call .AddHiddenColumn("tcnWay_payHdr", Request.QueryString.Item("nWay_pay"))
		Call .AddHiddenColumn("tcnBankHdr", Request.QueryString.Item("nBank"))
		Call .AddHiddenColumn("tcnCauseNullHdr", Request.QueryString.Item("nCauseNull"))
		Call .AddHiddenColumn("ncod_agree", Request.QueryString.Item("ncod_agree"))
		mstrquery = "dExpirdat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&sKey=" & mstrKey & "&nBank=" & Request.QueryString.Item("nBank") & "&nCauseNull=" & Request.QueryString.Item("nCauseNull") & "&sProcess=" & Request.QueryString.Item("sProcess") & "&nRow=" & Request.QueryString.Item("nRow") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree")
		Call .AddHiddenColumn("tctquerystring", mstrquery)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		'+ Si la transacción es "Consulta", se oculta la columna SEL 
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		Else
			.Columns("tctClient").EditRecord = True
			.Columns("Sel").GridVisible = True
		End If
		
		With .Columns("tctCause").Parameters
			.Add("nBank_code", mobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdDouble, True), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nWay_Pay", mobjValues.StringToType(Request.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		.sEditRecordParam = "dExpirdat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&nBank=" & Request.QueryString.Item("nBank") & "&nCauseNull=" & Request.QueryString.Item("nCauseNull") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree") & "&sKey=" & mstrKey & "&sProcess=" & Request.QueryString.Item("sProcess")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreCO501: Se obtiene la información para llenar el grid.
'------------------------------------------------------------------------------
Private Sub insPreCO501()
	'------------------------------------------------------------------------------
	Dim lcolBulletins As eCollection.Bulletins
	Dim lclsBulletin As eCollection.Bulletin
	Dim lclsBulletin1 As eCollection.Bulletin
	Dim lintIndex As Short
	Dim llngTotRow As Object
	Dim llngRow As Byte
	Dim ldblTotalImp As Byte
	
	
	lcolBulletins = New eCollection.Bulletins
	lclsBulletin = New eCollection.Bulletin
	lclsBulletin1 = New eCollection.Bulletin
	Response.Write("<SCRIPT>self.document.forms[0].sKey.value ='" & mstrKey & "';</" & "Script>")
	
	
	If lcolBulletins.findPayToReject(mstrKey, mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble, True), mstrRead, mobjValues.StringToType(Request.QueryString.Item("dExpirDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sProcess"), False, mobjValues.StringToType(Request.QueryString.Item("ncod_agree"), eFunctions.Values.eTypeData.etdDouble)) Then
		ldblTotalImp = 0
		sLocacion = "CO501.aspx?&nMainAction=304" & "&dExpirDat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&nBank=" & Request.QueryString.Item("nBank") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree")
		
		lintIndex = 0
		
		For	Each lclsBulletin In lcolBulletins
			With lclsBulletin
				mobjGrid.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ",sLocacion)"
				mobjGrid.Columns("tctClient").DefValue = .sClient
				mobjGrid.Columns("tctCliename").DefValue = .sCliename
				mobjGrid.Columns("tctDocument").DefValue = .sDocument
				mobjGrid.Columns("tctnBank").DefValue = CStr(.nBank_code)
				mobjGrid.Columns("tctAccount").DefValue = .sAccount
				mobjGrid.Columns("tctBulletins").DefValue = CStr(.nBulletins)
				mobjGrid.Columns("tctCause").Parameters("nBank_code").Value=(mobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdDouble))
				mobjGrid.Columns("tctCause").Parameters("nWay_Pay").Value=(mobjValues.StringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdDouble))
				mobjGrid.Columns("tctCause").DefValue = CStr(.nRejectcause)
				mobjGrid.Columns("cbeBranch").DefValue = CStr(.nBranch)
				mobjGrid.Columns("tctBranch").DefValue = .sBranch
				mobjGrid.Columns("valProduct").DefValue = CStr(.nProduct)
				mobjGrid.Columns("valProductDesc").DefValue = .sProduct
				mobjGrid.Columns("tcnPolicy").DefValue = CStr(.nPolicy)
				mobjGrid.Columns("tcnReceipt").DefValue = CStr(.nReceipt)
				mobjGrid.Columns("tcnDraft").DefValue = CStr(.nDraft)
				If .nRejectcause > 0 Then
					mobjGrid.Columns("Sel").checked = CShort("1")
				Else
					mobjGrid.Columns("Sel").checked = CShort("0")
				End If
				mobjGrid.Columns("tctAmount").DefValue = CStr(.nAmount)
				lintIndex = lintIndex + 1
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsBulletin
	End If
	
	If IsNothing(Request.QueryString.Item("nRow")) Then
		llngRow = 50
	Else
		llngRow = CDbl(Request.QueryString.Item("nRow")) + 49
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.HiddenControl("tcnString", sLocacion))
	
	lclsBulletin = Nothing
	lclsBulletin1 = Nothing
	lcolBulletins = Nothing
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
Call mobjNetFrameWork.BeginPage("co501")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co501"

If String.IsNullOrEmpty(Request.QueryString.Item("sKey")) Then
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
		.Write(mobjMenues.setZone(2, "CO501", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenues = Nothing
	End If
	.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	.Write("<SCRIPT>var	sLocacion	= 'CO501.aspx?nMainAction=304" & "&dExpirDat=" & Request.QueryString.Item("dExpirDat") & "&nWay_pay=" & Request.QueryString.Item("nWay_pay") & "&nBank=" & Request.QueryString.Item("nBank") & "&ncod_agree=" & Request.QueryString.Item("ncod_agree") & " ';</SCRIPT>")
End With
%>

<SCRIPT>
var lstrquerystring = <%=mstrquery%>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 11 $|$$Date: 4/08/04 16:00 $|$$Author: Nvaplat40 $"
	     
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
function insCheckSelClick(Field,lintIndex,Field2){
//-------------------------------------------------------------------------------------------
    if (!Field.checked){
		if (marrArray[lintIndex].tctCause>0) {
			insDefValues("UpdSelCO501", "nBulletins=" + marrArray[lintIndex].tctbulletins + "&nPolicy=" + marrArray[lintIndex].tcnPolicy + "&sKey=" + '<%=mstrKey%>' + "&nCollecDocTyp=" + marrArray[lintIndex].cbeCollecDocTyp + "&nId=" + marrArray[lintIndex].tcnId +"&tcnString=" + Field2 +"&sSel=" + (Field.checked?'1':'0'));
			}
    }
    else {        
		EditRecord(lintIndex,nMainAction,'Update',marrArray[lintIndex].tctquerystring)
		Field.checked = !Field.checked;   
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
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CO501" ACTION="valCollectionTra.aspx?sMode=2<%=Request.Params.Get("Query_String")%>">
<%
Response.Write(mobjValues.ShowWindowsName("CO501", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCO501()
Else
	Call insPreCO501Upd()
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




