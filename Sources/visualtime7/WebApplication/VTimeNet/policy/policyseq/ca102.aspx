<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeaderCover: se definen los campos del grid de coberturas
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeaderCover()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid de coberturas
	With mobjGrid
		Call .Columns.AddTextColumn(100716, GetLocalResourceObject("CoverColumnCaption"), "Cover", 30, "",  ,  ,  ,  ,  , True)
		Call .Columns.AddNumericColumn(100707, GetLocalResourceObject("SuminsuredColumnCaption"), "Suminsured", 18, CStr(0),  ,  , True, 6)
		Call .Columns.AddNumericColumn(100708, GetLocalResourceObject("AmolivesColumnCaption"), "Amolives", 10, CStr(0),  ,  , True, 0)
		Call .Columns.AddPossiblesColumn(100701, GetLocalResourceObject("RatetableColumnCaption"), "Ratetable", "Table9002", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddNumericColumn(100709, GetLocalResourceObject("RateColumnCaption"), "Rate", 9, "",  ,  ,  , 6)
		Call .Columns.AddPossiblesColumn(100701, GetLocalResourceObject("EffectiveColumnCaption"), "Effective", "Table9000", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddNumericColumn(100710, GetLocalResourceObject("EleinitColumnCaption"), "Eleinit", 4, "",  ,  , False)
		Call .Columns.AddNumericColumn(100711, GetLocalResourceObject("EleendColumnCaption"), "Eleend", 4, "",  ,  , False)
		Call .Columns.AddPossiblesColumn(100702, GetLocalResourceObject("PreexistColumnCaption"), "Preexist", "Table9006", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddNumericColumn(100712, GetLocalResourceObject("TerminationColumnCaption"), "Termination", 4, "",  ,  , False)
		Call .Columns.AddPossiblesColumn(100703, GetLocalResourceObject("PriorColumnCaption"), "Prior", "Table23", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddPossiblesColumn(100704, GetLocalResourceObject("ProofColumnCaption"), "Proof", "Table23", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddNumericColumn(100713, GetLocalResourceObject("WaitColumnCaption"), "Wait", 4, "",  ,  , False)
		Call .Columns.AddNumericColumn(100714, GetLocalResourceObject("FilingColumnCaption"), "Filing", 4, "",  ,  , False)
		Call .Columns.AddPossiblesColumn(100705, GetLocalResourceObject("FreqpayColumnCaption"), "Freqpay", "Table9004", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddPossiblesColumn(100706, GetLocalResourceObject("MethodpayColumnCaption"), "Methodpay", "Table9007", eFunctions.Values.eValuesType.clngComboType, "")
		Call .Columns.AddClientColumn(100715, GetLocalResourceObject("ContactColumnCaption"), "Contact", "",  , GetLocalResourceObject("ContactColumnCaption"),  ,  , "lbllContact")
		Call .Columns.AddHiddenColumn("Covercode", "")
		Call .Columns.AddHiddenColumn("Exists", "")
		Call .Columns.AddHiddenColumn("Name", "")
		
		.Codispl = "CA102"
		.Height = 460
		.Width = 600
		.DeleteButton = False
		.AddButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sArrayName = "marrCover"
		.sEditRecordParam = "sGrid=" & .sArrayName
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.FieldsByRow = 2
		.Top = 80
		.Columns("Sel").GridVisible = False
		.Columns("Effective").GridVisible = False
		.Columns("Effective").BlankPosition = False
		.Columns("Eleinit").GridVisible = False
		.Columns("Eleend").GridVisible = False
		.Columns("Preexist").GridVisible = False
		.Columns("Termination").GridVisible = False
		.Columns("Prior").GridVisible = False
		.Columns("Proof").GridVisible = False
		.Columns("Wait").GridVisible = False
		.Columns("Filing").GridVisible = False
		.Columns("Freqpay").GridVisible = False
		.Columns("Methodpay").GridVisible = False
		.Columns("Contact").GridVisible = False
		.Columns("Name").GridVisible = False
		.Columns("Cover").EditRecord = True
	End With
End Sub

'% insDefineHeaderClient: se definen los campos del grid de clientes
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeaderClient()
	'--------------------------------------------------------------------------------------------
	mobjGrid = Nothing
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid de clientes
	With mobjGrid
		Call .Columns.AddTextColumn(100717, GetLocalResourceObject("NameBranchColumnCaption"), "NameBranch", 40, "")
		Call .Columns.AddCheckColumn(100718, GetLocalResourceObject("AdminColumnCaption"), "Admin", "",  , "1")
		Call .Columns.AddCheckColumn(100719, GetLocalResourceObject("InsuranceColumnCaption"), "Insurance", "",  , "1")
		Call .Columns.AddHiddenColumn("IndExists", "")
		Call .Columns.AddHiddenColumn("Client", "")
		Call .Columns.AddHiddenColumn("AuxNameBranch", "")
		Call .Columns.AddHiddenColumn("AuxAdmin", "")
		Call .Columns.AddHiddenColumn("AuxInsurance", "")
		
		.Codispl = "CA102"
		.Height = 180
		.Width = 400
		.DeleteButton = False
		.AddButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sArrayName = "marrClient"
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCA102: se cargan los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreCA102()
	'--------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% MarkCheck: controla que los check sean excluyentes" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function MarkCheck(Field, Index){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	with(self.document.forms[0]){" & vbCrLf)
Response.Write("		switch(Field.name){" & vbCrLf)
Response.Write("			case ""Admin"":" & vbCrLf)
Response.Write("				if(typeof(Admin[Index])=='undefined'){" & vbCrLf)
Response.Write("					if(Insurance.checked)" & vbCrLf)
Response.Write("						Insurance.checked = false" & vbCrLf)
Response.Write("					AuxInsurance.value = (Insurance.checked)?1:2;" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("				else{" & vbCrLf)
Response.Write("					if(Insurance[Index].checked)" & vbCrLf)
Response.Write("						Insurance[Index].checked = false" & vbCrLf)
Response.Write("					AuxInsurance[Index].value = (Insurance[Index].checked)?1:2;" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("				break;" & vbCrLf)
Response.Write("			case ""Insurance"":" & vbCrLf)
Response.Write("				if(typeof(Insurance[Index])=='undefined'){" & vbCrLf)
Response.Write("					if(Admin.checked)" & vbCrLf)
Response.Write("						Admin.checked = false" & vbCrLf)
Response.Write("					AuxAdmin.value = (Admin.checked)?1:2;" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("				else{" & vbCrLf)
Response.Write("					if(Admin[Index].checked)" & vbCrLf)
Response.Write("						Admin[Index].checked = false				" & vbCrLf)
Response.Write("					AuxAdmin[Index].value = (Admin[Index].checked)?1:2;" & vbCrLf)
Response.Write("				}" & vbCrLf)
Response.Write("				break;" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" COLS=1>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("				")

	Call insDefineHeaderCover()
	Call insreaCreditInf()
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD VALIGN=TOP>" & vbCrLf)
Response.Write("                <DIV ID=""Scroll"" style=""width:400;height:350;overflow:auto; outset gray"">" & vbCrLf)
Response.Write("                ")

	Call insDefineHeaderClient()
	Call insreaBillingTypes()
Response.Write("" & vbCrLf)
Response.Write("                </DIV>" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE> ")

	
End Sub

'% insreaCreditInf: se cargan los campos de la información crediticia
'--------------------------------------------------------------------------------------------
Private Sub insreaCreditInf()
	'--------------------------------------------------------------------------------------------
	'+ Se cargan las condiciones para la poliza en el grid
	Dim lcolCreditInf As Object
	Dim lclsCreditInf As Object
'UPGRADE_NOTE: The 'ePolicy.CreditInfs' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lcolCreditInf = Server.CreateObject("ePolicy.CreditInfs")
	
	If lcolCreditInf.FindCA102(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsCreditInf In lcolCreditInf
			With mobjGrid
				.Columns("Exists").DefValue = lclsCreditInf.nSel
				.Columns("Covercode").DefValue = lclsCreditInf.nCover
				.Columns("Cover").DefValue = lclsCreditInf.sDescript
				.Columns("Suminsured").DefValue = lclsCreditInf.nSumInsur
				.Columns("Amolives").DefValue = lclsCreditInf.nQuantLives
				.Columns("Ratetable").DefValue = lclsCreditInf.nRateTable
				.Columns("Rate").DefValue = lclsCreditInf.nRate
				.Columns("Effective").DefValue = lclsCreditInf.nEffectiveTyp
				.Columns("Eleinit").DefValue = lclsCreditInf.nEleInit
				.Columns("Eleend").DefValue = lclsCreditInf.nEleEnd
				.Columns("Preexist").DefValue = lclsCreditInf.nPreExist
				.Columns("Termination").DefValue = lclsCreditInf.nTermination
				.Columns("Prior").DefValue = lclsCreditInf.nPrior
				.Columns("Proof").DefValue = lclsCreditInf.nProof
				.Columns("Wait").DefValue = lclsCreditInf.nWaitingP
				.Columns("Filing").DefValue = lclsCreditInf.nFiling
				.Columns("Freqpay").DefValue = lclsCreditInf.nFrequence
				.Columns("Methodpay").DefValue = lclsCreditInf.nMethodCl
				.Columns("Contact").DefValue = lclsCreditInf.sClient
				.Columns("Name").DefValue = lclsCreditInf.sCliename
				
				Response.Write(.DoRow)
			End With
		Next lclsCreditInf
	End If
	Response.Write(mobjGrid.closeTable())
	
	lcolCreditInf = Nothing
	
End Sub

'% insreaBillingTypes: Se cargan los "Dealers" para la poliza
'--------------------------------------------------------------------------------------------
Private Sub insreaBillingTypes()
	'--------------------------------------------------------------------------------------------
	Dim lcolBillingType As Object
	Dim lclsBillingType As Object
	Dim lintIndex As Short
	
'UPGRADE_NOTE: The 'ePolicy.BillingTypes' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lcolBillingType = Server.CreateObject("ePolicy.BillingTypes")
	
	If lcolBillingType.FindCA102(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "") Then
		lintIndex = 0
		For	Each lclsBillingType In lcolBillingType
			With mobjGrid
				.Columns("Namebranch").DefValue = lclsBillingType.sCliename
				.Columns("Admin").Checked = lclsBillingType.DefaultValueCA102("Admin_Checked")
				.Columns("Insurance").Checked = lclsBillingType.DefaultValueCA102("Insurance_Checked")
				.Columns("AuxNamebranch").DefValue = lclsBillingType.sCliename
				.Columns("AuxAdmin").DefValue = CStr(.Columns("Admin").Checked)
				.Columns("AuxInsurance").DefValue = CStr(.Columns("Insurance").Checked)
				.Columns("Client").DefValue = lclsBillingType.sClientBranch
				.Columns("IndExists").DefValue = lclsBillingType.nSel
				.Columns("Admin").OnClick = "MarkCheck(this," & lintIndex & ")"
				.Columns("Insurance").OnClick = "MarkCheck(this," & lintIndex & ")"
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsBillingType
	End If
	Response.Write(mobjGrid.closeTable())
	
	lcolBillingType = Nothing
End Sub

'% insPreCA102Upd: se manejan los datos cuando es un ventana PopUp
'--------------------------------------------------------------------------------------------
Private Sub insPreCA102Upd()
	'--------------------------------------------------------------------------------------------
	Call insDefineHeaderCover()
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "CA102", .QueryString.Item("nMainAction"), Session("bQuery"), CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA102")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.WindowsTitle("CA102", Request.QueryString.Item("sWindowDescript"))%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	Response.Write(mobjMenu.setZone(2, "CA102", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA102" ACTION="valPolicySeq.aspx?sTime=1">
    	<%Response.Write(mobjValues.ShowWindowsName("CA102", Request.QueryString.Item("sWindowDescript")))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA102()
Else
	Call insPreCA102Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA102")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




