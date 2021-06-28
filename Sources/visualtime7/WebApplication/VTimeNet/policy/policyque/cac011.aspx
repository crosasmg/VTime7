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
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "CAC011"
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cac011"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddAnimatedColumn(0, GetLocalResourceObject("btnDetailColumnCaption"), "btnDetail", "..\..\images\menu_query.png", GetLocalResourceObject("btnDetailColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnMovementColumnCaption"), "tcnMovement", 10,  ,  , GetLocalResourceObject("tcnMovementColumnToolTip"))
		.AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate",  ,  , GetLocalResourceObject("tcdEffecdateColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctType_histColumnCaption"), "tctType_hist", 30, "",  , GetLocalResourceObject("tctType_histColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctSinitialsColumnCaption"), "tctSinitials", 60, "",  , GetLocalResourceObject("tctSinitialsColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctType_amendColumnCaption"), "tctType_amend", 30, "",  , GetLocalResourceObject("tctType_amendColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnServ_orderColumnCaption"), "tcnServ_order", 10,  ,  , GetLocalResourceObject("tcnServ_orderColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnReferenceColumnCaption"), "tcnReference", 10,  ,  , GetLocalResourceObject("tcnReferenceColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 30, "",  , GetLocalResourceObject("tctCurrencyColumnToolTip"))
		.AddDateColumn(0, GetLocalResourceObject("tcdInitDateColumnCaption"), "tcdInitDate",  ,  , GetLocalResourceObject("tcdInitDateColumnToolTip"))
		.AddDateColumn(0, GetLocalResourceObject("tcdEndDateColumnCaption"), "tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateColumnToolTip"))
		.AddDateColumn(0, GetLocalResourceObject("tcdFerColumnCaption"), "tcdFer",  ,  , GetLocalResourceObject("tcdFerColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeWait_codeColumnCaption"), "cbeWait_code", "tab_waitpo", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeWait_codeColumnToolTip"))
		.AddButtonColumn(0, GetLocalResourceObject("SCA2-810ColumnCaption"), "SCA2-810", 0,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CAC011"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCAC011: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCAC011()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy_his As Object
	Dim lcolPolicy_his As ePolicy.Policy_hiss
	Dim lintCount As Short
	lcolPolicy_his = New ePolicy.Policy_hiss
	
	If lcolPolicy_his.Find(Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nType_amend"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintCount = 0
		For	Each lclsPolicy_his In lcolPolicy_his
			With mobjGrid
				.Columns("tcnMovement").DefValue = lclsPolicy_his.nMovement
				.Columns("tcdEffecdate").DefValue = lclsPolicy_his.dCompdate
				.Columns("tctType_hist").DefValue = lclsPolicy_his.sDesctran
				.Columns("tctSinitials").DefValue = lclsPolicy_his.sCliename
				.Columns("tctType_amend").DefValue = lclsPolicy_his.sDescType_amend
				.Columns("tcnServ_order").DefValue = lclsPolicy_his.nServ_order
				.Columns("tcnReference").DefValue = lclsPolicy_his.Reference(lclsPolicy_his.nType_hist)
				.Columns("tctCurrency").DefValue = lclsPolicy_his.sDescurr
				.Columns("tcdInitDate").DefValue = lclsPolicy_his.dEffecdate
				.Columns("tcdEndDate").DefValue = lclsPolicy_his.dNulldate
				.Columns("tcdFer").DefValue = lclsPolicy_his.dFer
				.Columns("btnNotenum").nNotenum = lclsPolicy_his.nNotenum
				.Columns("btnDetail").HRefScript = "InsShowCA001('" & lclsPolicy_his.sCertype & "','" & lclsPolicy_his.nCertif & "','" & mobjValues.TypeToString(lclsPolicy_his.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "');"
				.Columns("cbeWait_code").DefValue = lclsPolicy_his.nWait_code
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		Next lclsPolicy_his
	End If
	Response.Write(mobjGrid.closeTable())
	lcolPolicy_his = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac011")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "cac011"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 15/10/03 16:37 $|$$Author: Nvaplat61 $"
    
//% InsShowCA001: Llama a las ventanas de pago de siniestro y/o cualquiera que sea el caso
//-------------------------------------------------------------------------------------------
function InsShowCA001(sCertype, nCertif, dEffecdate){
//-------------------------------------------------------------------------------------------
	var lstrQueryString;
    var LoadWithAction;
    var nTransaction;
    var nCertif = '<%=Request.QueryString.Item("nCertif")%>';
    
    if(nCertif=='')
		nCertif = 0;;
		
//+ Se coloca como transacción "Consulta de...", dependiendo del sCertype (Table221)
    switch(sCertype){
//+ Propuesta
        case "1":
			LoadWithAction = '11';
			nTransaction = '11';
			break;
//+ Póliza
        case "2":
			LoadWithAction = (nCertif=='0'?'8':'9');
			nTransaction = (nCertif=='0'?'8':'9');
			break;
//+ Cotización
        case "3":
			LoadWithAction = '10';
			nTransaction = '10';
			break;
//+ Cotización de modificación
        case "4":
			LoadWithAction = '39';
			nTransaction = '39';
			break;
//+ Cotización de renovación
        case "5":
			LoadWithAction = '41';
			nTransaction = '41';
			break;
//+ Propuesta de modificación
		case "6":
			LoadWithAction = '40';
			nTransaction = '40';
			break;
//+ Propuesta de renovación
        case "7":
			LoadWithAction = '42';
			nTransaction = '42';
    }

	lstrQueryString = "&sCertype=" + sCertype + 
	                  "&sCodisplOrig=CAC011" + 
					  "&bMenu=1" +
	                  "&nBranch=<%=Request.QueryString.Item("nBranch")%>" +
	                  "&nProduct=<%=Request.QueryString.Item("nProduct")%>" +
	                  "&nPolicy=<%=Request.QueryString.Item("hddPolicy")%>" +
	                  "&nProponum=<%=Request.QueryString.Item("nPolicy")%>" +
	                  "&nCertif=" + nCertif +
	                  "&dStartdate=" + dEffecdate +
	                  "&LoadWithAction=" + 401 + 
	                  "&nTransaction=" + nTransaction;

	ShowPopUp("/VTimeNet/common/GoTo.aspx?sCodispl=CA001" + lstrQueryString,"CAC011_CA001",750,500,true,false,10,10);
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CAC011", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CAC011" ACTION="valPolicyQue.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("CAC011", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCAC011()

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac011")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




