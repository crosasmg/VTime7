<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.24.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú de la página
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de la grilla 
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo de los errores 	
Dim lobjErrors As eGeneral.GeneralFunction

Dim mblnInd As Boolean
Dim mstrAlert As String
Dim mstrCert As Object



'% insDefineHeader: Se define la estructura del grid
'------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'------------------------------------------------------------------------
	Dim lclsGeneral As eGeneral.Users
	Dim lclsScheCur As eSecurity.Secur_sche
	
	With Server
		mobjGrid = New eFunctions.Grid
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
		mobjGrid.sSessionID = Session.SessionID
		mobjGrid.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		
		mobjGrid.sCodisplPage = "bc802"
		Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
		lclsGeneral = New eGeneral.Users
		lclsScheCur = New eSecurity.Secur_sche
	End With
	
	mblnInd = False
	
	With mobjGrid
		.Columns.AddHiddenColumn("sClient", Session("sClient"))
		If Request.QueryString.Item("Type") <> "PopUp" Or Request.QueryString.Item("Action") = "Update" Then
			.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnEvalColumnCaption"), "tcnEval", 5, "",  , GetLocalResourceObject("tcnEvalColumnToolTip"),  ,  ,  ,  ,  , True)
		End If
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate", "",  , GetLocalResourceObject("tcdStartdateColumnToolTip"),  ,  , "ShowSumValues(this)")
		.Columns.AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
		.Height = 0
		If lclsGeneral.Find(Session("nUsercode")) Then
			If lclsScheCur.insReaLevels_v(lclsGeneral.sSche_code, "2", "SCA804") Then
				If lclsScheCur.nAmelevel >= 5 Then
					.Columns.AddButtonColumn(0, GetLocalResourceObject("SCA804ColumnCaption"), "SCA804", CDbl(Request.QueryString.Item("nNoteNum")),  , Not Request.QueryString.Item("Type") = "PopUp")
					.Height = 30
					mblnInd = True
				End If
			End If
		End If
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatus_evalColumnCaption"), "cbeStatus_eval", "Table5572", eFunctions.Values.eValuesType.clngComboType, "3",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatus_evalColumnToolTip"))
		
		
		If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Add" Then
			.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeCertypeColumnCaption"), "cbeCertype", "Table5632", eFunctions.Values.eValuesType.clngComboType, "1",  ,  ,  ,  , "ChangeDisab();",  ,  , GetLocalResourceObject("cbeCertypeColumnCaption"))
		Else
			.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeCertypeColumnCaption"), "cbeCertype", "Table5632", eFunctions.Values.eValuesType.clngComboType, CStr(mstrCert),  ,  ,  ,  , "ChangeDisab();",  ,  , GetLocalResourceObject("cbeCertypeColumnCaption"))
		End If
		
		.Columns.AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnCaption"),  ,  ,  ,  , "ShowChange(this);")
		.Columns.AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnCaption"))
		
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  , "ShowChangeValues(""DataPolPo"")", True)
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, "",  , GetLocalResourceObject("tcnCertifColumnToolTip"),  ,  ,  ,  ,  , True)
		
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, "",  , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		If Request.QueryString.Item("Type") <> "PopUp" Or Request.QueryString.Item("Action") = "Update" Then
			.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCumulColumnCaption"), "tcnCumul", 18, "",  , GetLocalResourceObject("tcnCumulColumnToolTip"),  ,  ,  ,  ,  , True, 6)
		End If
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = Session("bQuery")
		.Codispl = "BC802"
		.AddButton = True
		.DeleteButton = True
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.Columns("cbeStatus_eval").BlankPosition = 0
		.Columns("tcdStartdate").EditRecord = True
		
		If Request.QueryString.Item("Action") = "Update" Then
			
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("    top.window.resizeBy(50,50)" & vbCrLf)
Response.Write("</" & "SCRIPT>    ")

			
		Else
			.Height = .Height + 450
		End If
		
		.Width = 350
		.Top = 100
		.Splits_Renamed.AddSplit(0, "", 4)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("5ColumnCaption"), 5)
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.sDelRecordParam = "nEval=' + marrArray[lintIndex].tcnEval + '"
	End With
	lclsScheCur = Nothing
	lclsGeneral = Nothing
End Function

'%InsPreBC802: Se carga la información del grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreBC802()
	'--------------------------------------------------------------------------------------------
	Dim lblnFind As Boolean
	Dim lobjEval_master As eClient.eval_master
	Dim lobjEval_masters As eClient.eval_masters
	Dim lstrInd As Object
	Dim lstrinderr As Byte
	Dim lclsGeneral As eGeneral.Users
	Dim lclsScheCur As eSecurity.Secur_sche
	
	lblnFind = True
	lstrinderr = 0
	
	With Server
		lclsGeneral = New eGeneral.Users
		lclsScheCur = New eSecurity.Secur_sche
		lobjEval_master = New eClient.eval_master
		lobjEval_masters = New eClient.eval_masters
	End With
	'+ Se buscan las relaciones del cliente
	If lobjEval_masters.Find(Session("sClient"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		If lobjEval_masters.Count > 0 Then
			For	Each lobjEval_master In lobjEval_masters
				With mobjGrid
					.Columns("tcnEval").DefValue = CStr(lobjEval_master.nEval)
					.Columns("tcdStartdate").DefValue = CStr(lobjEval_master.dStartdate)
					.Columns("tcdExpirdat").DefValue = CStr(lobjEval_master.dExpirdat)
					.Columns("cbeStatus_eval").DefValue = CStr(lobjEval_master.nStatus_eval)
					.Columns("tcnCapital").DefValue = CStr(lobjEval_master.nCapital)
					.Columns("cbeCurrency").DefValue = CStr(lobjEval_master.nCurrency)
					.Columns("tcnCumul").DefValue = CStr(lobjEval_master.nCumul)
					
					.Columns("cbeBranch").DefValue = CStr(lobjEval_master.nBranch)
					.Columns("valProduct").DefValue = CStr(lobjEval_master.nProduct)
					.Columns("tcnPolicy").DefValue = CStr(lobjEval_master.nPolicy)
					.Columns("tcnCertif").DefValue = CStr(lobjEval_master.nCertif)
					.Columns("cbeCertype").DefValue = lobjEval_master.sCertype
					
					If mblnInd Then
						.Columns("btnNotenum").nNotenum = lobjEval_master.nNoterest
					End If
					If lobjEval_master.sExist = "1" Then
						lstrinderr = 1
					Else
						lstrinderr = 2
					End If
					.Columns("Sel").OnClick = "InsChangeSel(this," & lstrinderr & ")"
					Response.Write(.DoRow)
				End With
			Next lobjEval_master
		Else
			lblnFind = False
		End If
	Else
		lblnFind = False
	End If
	Response.Write(mobjGrid.closeTable())
	lclsScheCur = Nothing
	lobjEval_masters = Nothing
	lobjEval_master = Nothing
	lclsGeneral = Nothing
End Sub

'%InsPreBC802Upd: Se efectúan las acciones de la ventana PopUp
'------------------------------------------------------------------------
Private Function InsPreBC802Upd() As Object
	'------------------------------------------------------------------------
	Dim lclseval_master As eClient.eval_master
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclseval_master = New eClient.eval_master
			Response.Write(mobjValues.ConfirmDelete())
			lclseval_master.InsPostBC802("Del", mobjValues.StringToType(.QueryString.Item("nEval"), eFunctions.Values.eTypeData.etdDouble), "", 0, 0, 0, Today, 0, Today, 0, 0, 0, 0, 0, 0, "")
			lclseval_master = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valDocumentSeq.aspx", "BC802", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	If mblnInd Then
		If Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
		End If
	End If
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("bc802")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "bc802"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.24.56
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	lobjErrors = New eGeneral.GeneralFunction
End With

mstrAlert = "Err. 55842 " & lobjErrors.insLoadMessage(55842)
lobjErrors = Nothing
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

If CStr(Session("sCertype")) = "1" And CStr(Session("nPolicy")) = "" Then
	mstrCert = ""
Else
	mstrCert = Session("sCertype")
End If
%> 
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"

//%ShowSumValues: Suma 20 años a la fecha de efecto
//-------------------------------------------------------------------------------------------
function ShowSumValues(sField){
//-------------------------------------------------------------------------------------------
    var lintYear = 0; 
    var lintYearSum = 0;
    var lintdate = 0;
    var ldtmDateSystem = "";
//+ Se descompone la cadena
	lintYear    = sField.value.substr(6, 5);
	lintdate    = sField.value.substr(0,6) 
	lintYearSum = insConvertNumber(lintYear) + 20
//+ Se concatena el día, mes y año
    ldtmDateSystem = lintdate + lintYearSum;
//+ Se asigna fecha al campo en asp
	self.document.forms[0].tcdExpirdat.value = ldtmDateSystem; 
}   
//%ShowChangeValues: Se asigna valor a los controles cuyo valor depende de otros controles
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	var lstrParams = ""; 

	switch(sField){
		case "DataPolPo":
		    with(self.document.forms[0]){ 
			    lstrParams = "nbranch=" + cbeBranch.value + 
							 "&nProduct=" + valProduct.value +
							 "&nPolicy=" + tcnPolicy.value +
							 "&sCertype=" + cbeCertype.value;							  
			}
		
			insDefValues(sField,lstrParams,'/VTimeNet/Client/DocumSeq');
			break;	
	}		
}			

//%ChangeDisab: Se desabilitan / = el campos ramo
//-------------------------------------------------------------------------------------------
function ChangeDisab(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){ 
		if (cbeCertype.value == "0"){
			cbeBranch.disabled = true;
			cbeBranch.value = '';
		}
		else{
			cbeBranch.disabled = false;
		}
	}
}   

//%ShowChange: Se habilita/deshabilita campos cuando se abandona el campo rut.
//-------------------------------------------------------------------------------------------
function ShowChange(sField){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){ 
		if (sField.value!=0){
			valProduct.disabled = false
			tcnPolicy.disabled = false
			tcnCertif.disabled = false
		}	
		else{
			valProduct.disabled = true
			tcnPolicy.disabled = true
			tcnCertif.disabled = true
		}
	}
}   
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "BC802", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
mobjMenu = Nothing
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.ShowWindowsName("BC802", Request.QueryString.Item("sWindowDescript")))
Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
%>
<FORM METHOD="POST" NAME="frmBC013" ACTION="valDocumentSeq.aspx?mode=1">

<%
insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreBC802()
Else
	InsPreBC802Upd()
End If
mobjGrid = Nothing

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%="<SCRIPT>"%>
//%InsChangeSel: Se envía mensaje de validación al no poder eliminar un registro
//------------------------------------------------------------------------------
function InsChangeSel(Field, sInd){
//------------------------------------------------------------------------------
	if (Field.checked && sInd == "1") {
		alert('<%=mstrAlert%>');
		Field.checked = false
	}
}
<%="</SCRIPT>"%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.24.56
Call mobjNetFrameWork.FinishPage("bc802")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




