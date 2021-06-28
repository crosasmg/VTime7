<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.16
Dim mobjNetFrameWork As eNetFrameWork.Layout
Dim sDebtCli As Object
Dim sCertype As Object
Dim nBranch As Object
Dim nProduct As Object
Dim nPolicy As Object
Dim nCertif As Object
Dim dEffecdate As Object
Dim nQuote As Object

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid

Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lintQuote As Object
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjGrid.ActionQuery = Session("bQuery")
	lintQuote = Request.QueryString.Item("nQuote")
	If lintQuote = vbNullString Then
		lintQuote = nQuote
	End If
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuoteColumnCaption"), "tcnQuote", 10, Request.QueryString.Item("nQuote"),  , GetLocalResourceObject("tcnQuoteColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddDateColumn(40150, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate",  ,  , GetLocalResourceObject("tcdStartdateColumnToolTip"))
		Call .AddNumericColumn(40145, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  , "insChangeField(this)")
		'Call .AddNumericColumn (40145,"Impuestos","tcnTax",18,0,,"Monto de impuestos asociado a la cuota",True,6,,,"insChangeField(this)",True)
		'Call .AddNumericColumn (40145,"Total","tcnTotal_Amount",18,0,,"Importe total de pago asociado a la cuota",True,6,,,,True)
		Call .AddTextColumn(40149, GetLocalResourceObject("tctCommentColumnCaption"), "tctComment", 60, vbNullString,  , GetLocalResourceObject("tctCommentColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "WT002"
		.Columns("tcnQuote").EditRecord = True
		.Height = 400
		.Width = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		.sDelRecordParam = "nQuote='+ marrArray[lintIndex].tcnQuote + '"
		.Columns("Sel").GridVisible = Not Session("bQuery")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreWT002: Lee los valores de la tabla WarrantyQuotesSales
'--------------------------------------------------------------------------------------------
Private Sub insPreWT002()
	'--------------------------------------------------------------------------------------------
	Dim mcolWarrantyQuotesSales As ePolicy.WarrantyQuotess
	Dim lintIndex As Integer
	Dim lintCount As Integer
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim lintIsError As Byte
	Dim lstrMessage As String
	Dim lclsErrors As eFunctions.Errors
	Dim lstrMessage_tmp As String
	lclsErrors = New eFunctions.Errors
	lstrMessage_tmp = lclsErrors.ErrorMessage("WT002", 56029,  ,  ,  , True)
	If InStr(1, lstrMessage_tmp, "Err.") > 0 Then
		lintIsError = 1
	Else
		If InStr(1, lstrMessage_tmp, "Adv.") > 0 Then
			lintIsError = 0
		End If
	End If
	lclsGeneral = New eGeneral.GeneralFunction
	lstrMessage = lclsGeneral.insLoadMessage(56029)
	mcolWarrantyQuotesSales = New ePolicy.WarrantyQuotess
	nQuote = 1
	If mcolWarrantyQuotesSales.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = mcolWarrantyQuotesSales.Count
		If lintCount > 0 Then
			For lintIndex = 1 To lintCount
				With mcolWarrantyQuotesSales(lintIndex)
					mobjGrid.Columns("tcnQuote").DefValue = CStr(.nQuote)
					mobjGrid.Columns("tcdStartdate").DefValue = CStr(.dStartdate)
					mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount)
					'mobjGrid.Columns("tcnTax").DefValue = .nTax
					'mobjGrid.Columns("tcnTotal_Amount").DefValue = .nTotal_Amount
					mobjGrid.Columns("tctComment").DefValue = .sComment
					
					nQuote = .nQuote + 1
					mobjGrid.sEditRecordParam = "nQuote=" & lintCount + 1
				End With
				Response.Write(mobjGrid.DoRow())
			Next 
		End If
	End If
	
	mobjGrid.sEditRecordParam = "nQuote=" & nQuote
	Response.Write(mobjGrid.closeTable())
	mcolWarrantyQuotesSales = Nothing
	lclsGeneral = Nothing
End Sub

'% insPreWT002Upd: Se realiza el manejo de los campos del grid 
'--------------------------------------------------------------------------------------------
Private Sub insPreWT002Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjWarrantyQuotesSales As ePolicy.WarrantyQuotes
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lobjWarrantyQuotesSales = New ePolicy.WarrantyQuotes
			
			If lobjWarrantyQuotesSales.InsPostWT002(3, Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nQuote"), eFunctions.Values.eTypeData.etdDouble), Today, 0, mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), vbNullString, Session("nUserCode")) Then
				
			End If
			lobjWarrantyQuotesSales = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "WT002", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("WT002")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If


%>
<HTML>
<HEAD>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "WT002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/09 10:29 $|$$Author: Cidler $"
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/09 10:29 $|$$Author: Cidler $"

//%insChangeField: Ejecuta acciones al cambiar valor de un campo
//-------------------------------------------------------------------------------------------
function insChangeField(objField){
//-------------------------------------------------------------------------------------------
    var frm = self.document.forms[0]
    var ldbltax
    var ldblAmount
    
  
//+Actualizar fecha de vencimiento
//+Se toma fecha desde
		with (self.document.forms[0])
		{
			if(typeof(tcnTotal_Amount)!='undefined') 
			{
				//lstrString = "nTax=" + tcnTax.value + "&nAmount=" + tcnAmount.value;
				insDefValues("Premium_WT002", lstrString, '/VTimeNet/Policy/PolicySeq');
			}
		}
}    
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmWT002" ACTION="valPolicySeq.aspx?sMode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("WT002", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreWT002Upd()
Else
	Call insPreWT002()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.16
Call mobjNetFrameWork.FinishPage("WT002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




