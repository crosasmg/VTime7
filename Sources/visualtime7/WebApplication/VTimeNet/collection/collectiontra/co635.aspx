<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mstrKey As String
Dim mstrRead As String
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim lstrChains As String

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolPremiums As eCollection.Premiums

Dim lobjGeneral As eGeneral.GeneralFunction


'% insDefineHeader: se definen las propiedades del grid
'-------------------------------------------------------------------------------------- ------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "co635"
	
	Response.Write(mobjValues.HiddenControl("hddnCollector", Request.QueryString.Item("nCollector")))
	Response.Write(mobjValues.HiddenControl("hdddEffectDate", Request.QueryString.Item("dEffecdate")))
	Response.Write(mobjValues.HiddenControl("hddtcnAgency", Request.QueryString.Item("nAgency")))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"))
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, vbNullString)
		Call .AddTextColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnReceiptColumnToolTip"))
		Call .AddPossiblesColumn(40590, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnCurrencyColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBulletinsColumnCaption"), "tcnBulletins", 10, vbNullString)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdLimitdateColumnCaption"), "tcdLimitdate", vbNullString,  , GetLocalResourceObject("tcdLimitdateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate", vbNullString,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", vbNullString,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"))
		Call .AddHiddenColumn("tctCertype", vbNullString)
		Call .AddHiddenColumn("tcnContrat", vbNullString)
		Call .AddHiddenColumn("tcnDraft", vbNullString)
		Call .AddHiddenColumn("hddBranch", vbNullString)
		Call .AddHiddenColumn("hddProduct", vbNullString)
		Call .AddHiddenColumn("hddPolicy", vbNullString)
		Call .AddHiddenColumn("hddReceipt", vbNullString)
		Call .AddHiddenColumn("hddBulletins", vbNullString)
		Call .AddHiddenColumn("hddSelAux", vbNullString)
		Call .AddHiddenColumn("hddEffecDate", vbNullString)
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CO635"
		.ActionQuery = mobjValues.ActionQuery
		.AddButton = False
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301 Then
			.DeleteButton = False
		End If
		.Height = 350
		.Width = 280
		.WidthDelete = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "nReceipt=' + marrArray[lintIndex].hddReceipt + '" & "&nBranch=' + marrArray[lintIndex].cbeBranch + '" & "&nProduct=' + marrArray[lintIndex].valProduct + '" & "&sCertype=' + marrArray[lintIndex].tctCertype + '" & "&dEffecdate=' + marrArray[lintIndex].tcdEffecdate + '" & "&nContrat=' + marrArray[lintIndex].tcnContrat + '" & "&nDraft=' + marrArray[lintIndex].tcnDraft + '"
		
		If Request.QueryString.Item("Reload") = "2" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCO635: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCO635()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As Object
	Dim lintIndex As Short
	
	mcolPremiums = New eCollection.Premiums
	If mcolPremiums.FindCO635(mstrKey, mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble, True), mstrRead, mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sColltype"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nCollector"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWay_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Request.QueryString.Item("dLimitdate"))) Then
		lintIndex = 0
		For	Each lclsPremium In mcolPremiums
			With mobjGrid
				
				If lclsPremium.sPrint = "1" Then
					.Columns("Sel").Checked = CShort("1")
				Else
					.Columns("Sel").Checked = CShort("2")
				End If
				.Columns("Sel").OnClick = "insSelected(this, " & lintIndex & ");sPrint(this, " & CStr(lintIndex) & ");"
				.Columns("cbeBranch").DefValue = lclsPremium.nBranch
				.Columns("cbeBranch").Descript = lclsPremium.sDesBranch
				.Columns("hddBranch").DefValue = lclsPremium.nBranch
				.Columns("valProduct").DefValue = lclsPremium.nProduct
				.Columns("valProduct").Descript = lclsPremium.sDescProd
				.Columns("hddProduct").DefValue = lclsPremium.nProduct
				.Columns("tcnPolicy").DefValue = lclsPremium.nPolicy
				.Columns("hddPolicy").DefValue = lclsPremium.nPolicy
				If mobjValues.StringToType(lclsPremium.nContrat, eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
					.Columns("tcnReceipt").DefValue = lclsPremium.nReceipt
					.Columns("hddReceipt").DefValue = lclsPremium.nReceipt
				Else
					.Columns("tcnReceipt").DefValue = lclsPremium.nContrat & " / " & lclsPremium.nDraft
				End If
				.Columns("tcnContrat").DefValue = lclsPremium.nContrat
				.Columns("tcnDraft").DefValue = lclsPremium.nDraft
				.Columns("tcnCurrency").DefValue = lclsPremium.nCurrency
				.Columns("tcnCurrency").Descript = lclsPremium.sDescCurrency
				.Columns("tcnAmount").DefValue = lclsPremium.nAmount
				If mobjValues.StringToType(lclsPremium.nBulletins, eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
					.Columns("tcnBulletins").DefValue = CStr(eRemoteDB.Constants.intNull)
					.Columns("hddBulletins").DefValue = CStr(eRemoteDB.Constants.intNull)
				Else
					.Columns("tcnBulletins").DefValue = lclsPremium.nBulletins
					.Columns("hddBulletins").DefValue = lclsPremium.nBulletins
				End If
				.Columns("tcdLimitdate").DefValue = lclsPremium.dLimitdate
				.Columns("tcdEffecdate").DefValue = lclsPremium.dEffecdate
				.Columns("hddEffecdate").DefValue = lclsPremium.dEffecdate
				.Columns("tcdExpirdat").DefValue = lclsPremium.dExpirDat
				.Columns("tctCertype").DefValue = lclsPremium.sCertype
				
				'				If lintRecordIndex = 1 Then
				'				    lstrChains = lclsPremium.mlngRows
				'				Else
				'				    lstrChains = lstrChains & "," & lclsPremium.mlngRows
				'				End If
				
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsPremium
		Response.Write(mobjValues.HiddenControl("hddChains", lstrChains))
		
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>    " & vbCrLf)
Response.Write("    var sChains=""""" & vbCrLf)
Response.Write("    var sChange=""""" & vbCrLf)
Response.Write("    sChains = self.document.forms[0].hddChains.value;" & vbCrLf)
Response.Write("</" & "SCRIPT>        ")

		
	End If
	Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow"))))
	Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')", lintIndex < 50))
	Response.Write(mobjGrid.closeTable())
	If Request.QueryString.Item("nMainAction") = "301" And lintIndex > 0 Then
Response.Write("" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TABLE ALIGN=""CENTER"">" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD ALIGN=""CENTER"" COLSPAN=1>						  " & vbCrLf)
Response.Write("					<P>" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=40202><A NAME=""Listado"">" & GetLocalResourceObject("AnchorListadoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD COLSPAN=""1"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("					</TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("					<TR>" & vbCrLf)
Response.Write("						<TD>")


Response.Write(mobjValues.CheckControl("chkPrint", GetLocalResourceObject("chkPrintCaption")))


Response.Write("</TD>" & vbCrLf)
Response.Write("					</TR> 							" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("			</TR>	" & vbCrLf)
Response.Write("		</TABLE>							    " & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    ")

	End If
	Response.Write(mobjValues.BeginPageButton)
End Sub

'% insPreCO635Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCO635Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	
	lclsPremium = New eCollection.Premium
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsPremium.insPostCO635("CO635", "Del", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCollector"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nContrat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nDraft"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCollectionTra.aspx", "CO635", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO635")
'~End Header Block VisualTimer Utility

Response.CacheControl = "private"

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

If IsNothing(Request.QueryString.Item("sKey")) Then
	lobjGeneral = New eGeneral.GeneralFunction
	mstrKey = lobjGeneral.getsKey(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	lobjGeneral = Nothing
	mstrRead = "1"
Else
	mstrKey = Request.QueryString.Item("sKey")
	mstrRead = "0"
End If

mobjValues.sCodisplPage = "co635"
lstrChains = ""
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 3 $|$$Date: 27/10/03 19:57 $|$$Author: Nvaplat11 $"

//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&sKey=.*/,'')
	lstrURL = lstrURL + "&sKey=" + "<%=mstrKey%>"
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
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CO635", "CO635.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO635" ACTION="valCollectionTra.aspx?<%="nMainAction=" & Request.QueryString.Item("nMainAction")%>">

<%
Response.Write(mobjValues.ShowWindowsName("CO635"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCO635Upd()
Else
	Call insPreCO635()
End If

mobjValues = Nothing
%>

<SCRIPT>

//sPrint: Permite seleccionar los recibos que se van a imprimir.
//------------------------------------------------------------------------------------------
function sPrint(Field,lintIndex){
//------------------------------------------------------------------------------------------
    if(Field.checked){
        sChains = sChains + ", " + Field.value;
        self.document.forms[0].hddChains.value=sChains;
        sChange = "1";
        
		lstrParam =  "sKey="         + "<%=mstrKey%>"   +
		             "&nBranch="     + marrArray[lintIndex].hddBranch  +
				     "&nProduct="    + marrArray[lintIndex].hddProduct +
		             "&nPolicy="     + marrArray[lintIndex].tcnPolicy  +
		             "&nReceipt="    + marrArray[lintIndex].tcnReceipt +
				     "&nDraft="      + marrArray[lintIndex].tcnDraft   +
				     "&sPrint=1";

		insDefValues('InsPrint', lstrParam);    
        
    }else{
        sChains = sChains.replace(Field.value + "," ,"");
        self.document.forms[0].hddChains.value=sChains;
        sChange = "1";
        
		lstrParam =  "sKey="         + "<%=mstrKey%>"   +
		             "&nBranch="     + marrArray[lintIndex].hddBranch  +
				     "&nProduct="    + marrArray[lintIndex].hddProduct +
		             "&nPolicy="     + marrArray[lintIndex].tcnPolicy  +
		             "&nReceipt="    + marrArray[lintIndex].tcnReceipt +
				     "&nDraft="      + marrArray[lintIndex].tcnDraft   +
				     "&sPrint=2";

		insDefValues('InsPrint', lstrParam);    
        
    }
    //alert(sChains);
}
//%insSelected: realiza el manejo para la edición de un registro particular del grid 
//%para eliminarlo, agregarlo o modificarlo
//------------------------------------------------------------------------------------------
function insSelected(Field, nIndex){
//------------------------------------------------------------------------------------------
    var aux;
	if(mintArrayCount >= 1){
		if(Field.checked)
		    self.document.forms[0].hddSelAux(nIndex).value = "1"
		else
		    self.document.forms[0].hddSelAux(nIndex).value = "2"
	}
	else {
		if(Field.checked)
		    self.document.forms[0].hddSelAux.value = "1"
		else
		    self.document.forms[0].hddSelAux.value = "2"
	}
}
</SCRIPT>
</FORM> 
</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("CO635")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




