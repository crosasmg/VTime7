<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cac003"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		.AddAnimatedColumn(0, "", "imgValues", "/VTimeNet/Images/lupa.bmp")
		.AddTextColumn(0, GetLocalResourceObject("tctOfficeColumnCaption"), "tctOffice", 30, "",  , GetLocalResourceObject("tctOfficeColumnCaption"))
		.AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "",  , GetLocalResourceObject("tctBranchColumnCaption"))
		.AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "",  , GetLocalResourceObject("tctProductColumnCaption"))
		If mobjValues.StringToType(Session("nOption"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
			.AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0))
		Else
			.AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0))
			.AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 15, CStr(0))
		End If
		.AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 15, CStr(0))
		.AddTextColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", 30, "",  , GetLocalResourceObject("tctClientColumnCaption"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CAC003"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Columns("imgValues").GridVisible = False
	End With
End Sub

'% insPreCAC003: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC003()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lobjGen As ePolicy.Policy
	Dim lobjObject As Object
	Dim lcolObj As Object
	
	lobjGen = New ePolicy.Policy
	
	lcolObj = lobjGen.insPreCAC003(mobjValues.StringToType(Session("nOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nOption"), eFunctions.Values.eTypeData.etdDouble))
	
	lintCount = 0
	
	For	Each lobjObject In lcolObj
		With lobjObject
			mobjGrid.Columns("tctOffice").DefValue = .sDesOffice
			mobjGrid.Columns("tctBranch").DefValue = .sDesBranch
			mobjGrid.Columns("tctProduct").DefValue = .sDesProduct
			mobjGrid.Columns("tctClient").DefValue = .sCliename
			If CStr(Session("nOption")) = "1" Then
				mobjGrid.Columns("tcnPolicy").DefValue = .nPolicy
			Else
				mobjGrid.Columns("tcnPolicy").DefValue = .nPolicy
				mobjGrid.Columns("tcnReceipt").DefValue = .nReceipt
			End If
			mobjGrid.Columns("tcnCertif").DefValue = .nCertif
			Response.Write(mobjGrid.DoRow())
		End With
		
		lintCount = lintCount + 1
		
		If lintCount = 200 Then
			Exit For
		End If
	Next lobjObject
	
	Response.Write(mobjGrid.closeTable())
	
	lobjGen = Nothing
	lobjObject = Nothing
	lcolObj = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac003")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac003"
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "CAC003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAC003" ACTION="ValPolicyQue.aspx?Zone=2">
<%
Call insDefineHeader()
Call insPreCAC003()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




