<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.55
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
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "agc002"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnIntermedColumnCaption"), "tcnIntermed", "tabintermedia", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnIntermedColumnCaption"))
		Call .AddPossiblesColumn(40010, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchColumnCaption"))
		Call .AddPossiblesColumn(40011, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valProductColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull))
		Call .AddNumericColumn(40029, GetLocalResourceObject("tcnLoanColumnCaption"), "tcnLoan", 30, CStr(eRemoteDB.Constants.intNull))
		Call .AddDateColumn(40032, GetLocalResourceObject("tcdDateLoanColumnCaption"), "tcdDateLoan", CStr(eRemoteDB.Constants.dtmNull))
		Call .AddPossiblesColumn(40026, GetLocalResourceObject("tcnTypeLoanColumnCaption"), "tcnTypeLoan", "Table245", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnTypeLoanColumnToolTip"))
		Call .AddPossiblesColumn(40028, GetLocalResourceObject("tctStatLoanColumnCaption"), "tctStatLoan", "Table191", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctStatLoanColumnToolTip"))
		Call .AddPossiblesColumn(40027, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnCurrencyColumnCaption"))
		Call .AddNumericColumn(40030, GetLocalResourceObject("tcnAmoloanColumnCaption"), "tcnAmoloan", 34, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(40031, GetLocalResourceObject("tcnBalanLoanColumnCaption"), "tcnBalanLoan", 34, CStr(0),  ,  , True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "AGC002"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 600
		.Width = 600
		.Top = 10
		.Left = 10
		.bOnlyForQuery = True
	End With
End Sub

'% insPreBCC003: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreAGC002()
	'--------------------------------------------------------------------------------------------
	Dim lclsLoans_int As Object
	Dim lcolLoans_int As eAgent.Loans_ints
	
	lcolLoans_int = New eAgent.Loans_ints
	'+ Se ejecuta la condicion de busqueda para cargar el grid
	
	'	Response.Write "<LABEL ID=8023><%= GetLocalResourceObject("lblClienameCaption") %></LABEL>&nbsp;"
	'	Response.Write mobjValues.TextControl("lblCliename",30,session("scliename"),,"",true) 
	If lcolLoans_int.FindAGC002(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dStardate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("sStatloan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nLoan"), eFunctions.Values.eTypeData.etdDouble)) Then
		If CStr(Session("insvalAgent")) = "" Then
			For	Each lclsLoans_int In lcolLoans_int
				With lclsLoans_int
					mobjGrid.Columns("tcnIntermed").DefValue = .nIntermed
					mobjGrid.Columns("cbeBranch").DefValue = .nBranch
					
					If mobjValues.StringToType(.nBranch, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
						mobjGrid.Columns("valProduct").Parameters.Add("nBranch", .nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End If
					
					mobjGrid.Columns("valProduct").DefValue = .nProduct
					mobjGrid.Columns("tcnPolicy").DefValue = .nPolicy
					mobjGrid.Columns("tcnLoan").DefValue = .nLoan
					mobjGrid.Columns("tcdDateLoan").DefValue = .dDateLoan
					mobjGrid.Columns("tcnTypeLoan").DefValue = .nTypeLoan
					mobjGrid.Columns("tcnAmoloan").DefValue = .nAmoloan
					mobjGrid.Columns("tcnBalanLoan").DefValue = .nBalanLoan
					mobjGrid.Columns("tcnCurrency").DefValue = .nCurrency
					mobjGrid.Columns("tctStatLoan").DefValue = .sStatLoan
					
					Response.Write(mobjGrid.DoRow())
				End With
			Next lclsLoans_int
		End If
	Else
	End If
	Response.Write(mobjGrid.closeTable())
	
	lclsLoans_int = Nothing
	lcolLoans_int = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agc002")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agc002"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 21/10/03 10:11 $"        

</SCRIPT>        

    <%
        
        With Response
            .Write(mobjValues.StyleSheet())
            .Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "AGC002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
            End If
            mobjMenu = Nothing
            .Write(mobjValues.ShowWindowsName("AGC002", Request.QueryString.Item("sWindowDescript")))
        End With
        
        %>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmLoansQuery" ACTION="ValAgent.aspx?x=1">
<%

Call insDefineHeader()
Call insPreAGC002()

mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing
%>
</FORM>	
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.55

Call mobjNetFrameWork.FinishPage("agc002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




