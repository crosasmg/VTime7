<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 12.13.11
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.13.11
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "BCC003"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddNumericColumn(40348, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 30, CStr(eRemoteDB.Constants.intNull)) '1
		Call .AddNumericColumn(40349, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 30, CStr(eRemoteDB.Constants.intNull)) '2
		Call .AddPossiblesColumn(40345, GetLocalResourceObject("tcnCurrencyColumnCaption"), "tcnCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, "Varias",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnCurrencyColumnCaption"))
		Call .AddNumericColumn(40350, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 34, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6) '4
		Call .AddNumericColumn(40351, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 34, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6) '5
		Call .AddTextColumn(40355, GetLocalResourceObject("tctdescBranchColumnCaption"), "tctdescBranch", 30, CStr(eRemoteDB.Constants.strNull)) '6 sdescBranch
		Call .AddTextColumn(40356, GetLocalResourceObject("tctDescProdColumnCaption"), "tctDescProd", 30, CStr(eRemoteDB.Constants.strNull)) '7 sDescProd
		Call .AddPossiblesColumn(40346, GetLocalResourceObject("tcnRoleColumnCaption"), "tcnRole", "Table12", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnRoleColumnCaption"))
		Call .AddTextColumn(40357, GetLocalResourceObject("tctOfficeDesColumnCaption"), "tctOfficeDes", 30, CStr(eRemoteDB.Constants.intNull)) '9 sOfficeDes
		Call .AddTextColumn(40358, GetLocalResourceObject("tctPolitypeColumnCaption"), "tctPolitype", 30, CStr(eRemoteDB.Constants.intNull)) '10 sPolitype
		Call .AddDateColumn(40362, GetLocalResourceObject("tcdStartDateColumnCaption"), "tcdStartDate", CStr(eRemoteDB.Constants.dtmNull)) '11 dStartDate
		Call .AddDateColumn(40363, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", CStr(eRemoteDB.Constants.dtmNull)) '12 dExpirdat
		Call .AddPossiblesColumn(40347, GetLocalResourceObject("tctStatus_polColumnCaption"), "tctStatus_pol", "Table181", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tctStatus_polColumnCaption"))
		If Session("sTypeCompanyUser") = eClient.Client.eType.cstrBrokerOrBrokerageFirm Then
			Call .AddTextColumn(40359, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 30, CStr(eRemoteDB.Constants.intNull)) '14 sCliename
			Call .AddTextColumn(40360, GetLocalResourceObject("tctDescOfficeInsColumnCaption"), "tctDescOfficeIns", 30, CStr(eRemoteDB.Constants.intNull)) '15 sDescOfficeIns
			Call .AddTextColumn(40361, GetLocalResourceObject("tctOriginalColumnCaption"), "tctOriginal", 30, CStr(eRemoteDB.Constants.intNull)) '16 sOriginal
			Call .AddNumericColumn(40352, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 30, CStr(eRemoteDB.Constants.intNull)) '17 nBranch
			Call .AddNumericColumn(40353, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 30, CStr(eRemoteDB.Constants.intNull)) '18 nProduct
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BCC003"
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
Private Sub insPreBCC003()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lcolPolicys As ePolicy.Policys
	Dim lIndex As Integer
	Dim nRole As Object
	Dim nPolicyAnt As Double
	Dim nTotPolicy As Object
	Dim sCurrency As Object
	
	lclsPolicy = New ePolicy.Policy
	
	lcolPolicys = New ePolicy.Policys
	
	'+ Se ejecuta la condicion de busqueda para cargar el grid
	If CDbl(Request.Form.Item("tctclient")) = 0 Then
		nRole = eRemoteDB.Constants.intNull
	Else
		nRole = Request.Form.Item("tctclient")
	End If
	If lcolPolicys.FindBCC003(Session("sTypeCompanyUser"), Session("optPolicy"), Session("tctclient"), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("tcdEffecdate"))) Then
		If CStr(Session("insvalClient")) = "" Then
			nPolicyAnt = 0
			nTotPolicy = 0
			For lIndex = 1 To lcolPolicys.Count
            'For lIndex = 0 To lcolPolicys.Count -1
				lclsPolicy = lcolPolicys.Item(lIndex)
				With lclsPolicy
					If nPolicyAnt <> .nPolicy Then
						nTotPolicy = nTotPolicy + .nPremium
					End If
					nPolicyAnt = .nPolicy
					mobjGrid.Columns("tcnPolicy").DefValue = CStr(.nPolicy)
					mobjGrid.Columns("tcnCertif").DefValue = CStr(.nCertif)
					
					If lclsPolicy.nCountCur > 0 Then
						If lclsPolicy.nCountCur = 1 Then
							mobjGrid.Columns("tcnCurrency").DefValue = CStr(.nMaxCurr)
							Session("sCurrency") = .nMaxCurr
						Else
							mobjGrid.Columns("tcnCurrency").DefValue = "Varias"
							Session("sCurrency") = "Varias"
						End If
					End If
					
					mobjGrid.Columns("tcnPremium").DefValue = CStr(.nPremium)
					mobjGrid.Columns("tcnCapital").DefValue = CStr(.nCapital)
					mobjGrid.Columns("tctdescBranch").DefValue = String.Empty'.sdescBranch
					mobjGrid.Columns("tctDescProd").DefValue = String.Empty'.sdescProd
					mobjGrid.Columns("tcnRole").DefValue = CStr(.nRole)
					mobjGrid.Columns("tctOfficeDes").DefValue = String.Empty'.sOfficeDes
					If CDbl(.sPolitype) = 1 Then
						mobjGrid.Columns("tctPolitype").DefValue = "Individual"
					Else
						mobjGrid.Columns("tctPolitype").DefValue = "Colectiva"
						
					End If
					mobjGrid.Columns("tcdStartDate").DefValue = CStr(.dStartdate)
					mobjGrid.Columns("tcdExpirdat").DefValue = CStr(.dExpirdat)
					If .nNullcode = 0 Then
						mobjGrid.Columns("tctStatus_pol").DefValue = .sStatus_pol
					Else
						mobjGrid.Columns("tctStatus_pol").DefValue = "Anulada"
					End If
					If Session("sTypeCompanyUser") = eClient.Client.eType.cstrBrokerOrBrokerageFirm Then
						mobjGrid.Columns("tctCliename").DefValue = .sCliename
						mobjGrid.Columns("tctDescOfficeIns").DefValue = String.Empty '.sDescOfficeIns
						mobjGrid.Columns("tctOriginal").DefValue = .sOriginal
						mobjGrid.Columns("tcnBranch").DefValue = CStr(.nBranch)
						mobjGrid.Columns("tcnProduct").DefValue = CStr(.nProduct)
					End If
					Response.Write(mobjGrid.DoRow())
				End With
			Next 
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	If nTotPolicy = eRemoteDB.Constants.intNull Then
		nTotPolicy = 0
	End If
	Response.Write("<LABEL ID=9696>" & GetLocalResourceObject("lblTotPrimaCaption") & "</LABEL></TD>&nbsp;&nbsp;")
	Response.Write(mobjValues.TextControl("lblTotPrima", 30, nTotPolicy,  , "", True))
	
	lclsPolicy = Nothing
	lcolPolicys = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("BCC003")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.13.11
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "BCC003"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="BCC003" NAME="BCC003" ACTION="XXXXXX.aspx?sCodispl=XXXXXX">
<%
Response.Write("<LABEL ID=40343>" & GetLocalResourceObject("lblClienameCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;&nbsp;")
Response.Write(mobjValues.TextControl("lblCliename", 30, Session("scliename"),  , "", True))

Call insDefineHeader()
Call insPreBCC003()
%>
</FORM>
</BODY>
</HTML>
<%
mobjGrid = Nothing
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 12.13.11
Call mobjNetFrameWork.FinishPage("BCC003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




