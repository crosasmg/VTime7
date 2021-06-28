<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader: Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------    
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "vi012"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(41199, GetLocalResourceObject("tcnCodeColumnCaption"), "tcnCode", 5, CStr(0), False, GetLocalResourceObject("tcnCodeColumnToolTip"), True,  ,  ,  ,  , True)
		Call .AddDateColumn(41205, GetLocalResourceObject("tcdLoan_dateColumnCaption"), "tcdLoan_date",  ,  , GetLocalResourceObject("tcdLoan_dateColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(41200, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(41201, GetLocalResourceObject("tcnInterestColumnCaption"), "tcnInterest", 9, CStr(0),  , GetLocalResourceObject("tcnInterestColumnCaption"), True, 2,  ,  ,  , True)
		Call .AddNumericColumn(41202, GetLocalResourceObject("tcnSald_iniColumnCaption"), "tcnSald_ini", 18, CStr(0),  , GetLocalResourceObject("tcnSald_iniColumnCaption"), True, 6,  ,  ,  , True)
		Call .AddDateColumn(41206, GetLocalResourceObject("tcdPay_dateColumnCaption"), "tcdPay_date",  ,  , GetLocalResourceObject("tcdPay_dateColumnToolTip"))
		Call .AddNumericColumn(41203, GetLocalResourceObject("tcnAportColumnCaption"), "tcnAport", 18, CStr(0), True, GetLocalResourceObject("tcnAportColumnToolTip"), True, 6,  ,  , "insCalcSaldoFinal(this);")
		Call .AddNumericColumn(41204, GetLocalResourceObject("tcnSald_finColumnCaption"), "tcnSald_fin", 18, CStr(0),  , GetLocalResourceObject("tcnSald_finColumnCaption"), True, 6,  ,  ,  , True)
	End With
	
	With mobjGrid
		.Codispl = "VI012"
		.Width = 350
		.Height = 330
		.Columns("tcnCode").EditRecord = True
		.Columns("Sel").OnClick = "insCheckSelClick(this)"
		.DeleteButton = False
		.AddButton = False
	End With
	
End Sub

'%insPreVI012: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI012()
	Dim mcolLoans As Object
	'--------------------------------------------------------------------------------------------
	'- Se define las variables para la carga del grid
	Dim lcolLoans As ePolicy.Loanss
	Dim lclsLoans As Object
	
Response.Write("    " & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%insCheckSelClick: Levanta la Popup" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCheckSelClick(Field){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    if (!Field.checked){" & vbCrLf)
Response.Write("        Field.checked = true" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    else {" & vbCrLf)
Response.Write("        EditRecord(Field.value,nMainAction)" & vbCrLf)
Response.Write("        Field.checked = !Field.checked" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	lcolLoans = New ePolicy.Loanss
	If lcolLoans.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsLoans In mcolLoans
			With mobjGrid
				.Columns("tcnCode").DefValue = lclsLoans.nCode
				.Columns("tcdLoan_date").DefValue = lclsLoans.dLoan_date
				.Columns("tcnAmount").DefValue = lclsLoans.nAmount
				.Columns("tcnInterest").DefValue = lclsLoans.nInterest
				.Columns("tcnSald_ini").DefValue = lclsLoans.nSumAmount
				.Columns("tcdPay_date").DefValue = vbNullString
				.Columns("tcnAport").DefValue = CStr(0)
				.Columns("tcnSald_fin").DefValue = lclsLoans.nSumAmount
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsLoans
	End If
	Response.Write(mobjGrid.CloseTable())
	lcolLoans = Nothing
End Sub

'% insPreVI012Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los abonos de anticipos
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreVI012Upd()
	'------------------------------------------------------------------------------------------------------------------        
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyTra.aspx", "VI012", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi012")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi012"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet)
If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjValues.ShowWindowsName("VI012", Request.QueryString.Item("sWindowDescript")))
		.Write(mobjMenu.setZone(2, "VI012", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjMenu = Nothing
End If

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<SCRIPT>
//--------------------------------------------------------------------------------------------------------------------------------------------
function insCalcSaldoFinal(Field){
//--------------------------------------------------------------------------------------------------------------------------------------------
    var nSaldoFinal=0;

    nSaldoFinal = insConvertNumber(self.document.forms[0].elements["tcnSald_ini"].value) - insConvertNumber(Field.value);
    self.document.forms[0].elements["tcnSald_fin"].value = nSaldoFinal;
}
</SCRIPT>
<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VI012" ACTION="valPolicyTra.aspx?x=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI012()
Else
	Call insPreVI012Upd()
End If
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi012")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




