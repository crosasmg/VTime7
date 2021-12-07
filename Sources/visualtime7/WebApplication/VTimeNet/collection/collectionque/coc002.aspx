<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
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
        '+ Se definen las columnas del grid
        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , True)
            'Call .AddNumericColumn(0, GetLocalResourceObject("tcnBulletinColumnCaption"), "tcnBulletin", 10, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , True)
            Call .AddTextColumn(0, GetLocalResourceObject("tcnBulletinColumnCaption"), "tcnBulletin", 10, "",  ,  ,  ,  ,  ,  , ) 'ehh - Ad. vt fase II rsis 2
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valCollectoColumnCaption"), "valCollecto", "tabCollector_Client", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCollectoColumnCaption"))
            Call .AddDateColumn(40420, GetLocalResourceObject("tcdStatDateColumnCaption"), "tcdStatDate",  ,  ,  ,  ,  ,  , True)
            Call .AddDateColumn(40420, GetLocalResourceObject("tcdExpirDatColumnCaption"), "tcdExpirDat",  ,  ,  ,  ,  ,  , True)
            Call .AddPossiblesColumn(40408, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumnColumnCaption"), "tcnPremiumn", 18, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6,  ,  ,  , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6,  ,  ,  , True)
            Call .AddPossiblesColumn(40405, GetLocalResourceObject("cbeStatus_PreColumnCaption"), "cbeStatus_Pre", "Table19", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatus_PreColumnToolTip"))
            Call .AddNumericColumn(40412, GetLocalResourceObject("tcnContratColumnCaption"), "tcnContrat", 10, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , True)
            Call .AddNumericColumn(40412, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 10, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  ,  , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6,  ,  ,  , True)
            Call .AddPossiblesColumn(40405, GetLocalResourceObject("cbeStat_DraftColumnCaption"), "cbeStat_Draft", "Table253", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStat_DraftColumnToolTip"))
        End With
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "COC002"
            .Columns("Sel").GridVisible = False
            .bOnlyForQuery = True
            .DeleteButton = False
            .AddButton = False
        End With
    End Sub

    '% insPreCOC002: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCOC002()
        '--------------------------------------------------------------------------------------------
        Dim lblnGridvisible As Object
        Dim lclsPremium As eCollection.Premium
        Dim lcolPremiums As eCollection.Premiums

        With Server
            lclsPremium = New eCollection.Premium
            lcolPremiums = New eCollection.Premiums
        End With

        If lcolPremiums.Find_Receipt_Pol(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nInd_PolPro"), eFunctions.Values.eTypeData.etdDouble)) Then

            For Each lclsPremium In lcolPremiums
                With mobjGrid
                    .Columns("tcnReceipt").DefValue = CStr(lclsPremium.nReceipt)
                    '.Columns("tcnBulletin").DefValue = CStr(lclsPremium.nBulletins)
                    .Columns("tcnBulletin").DefValue = IIf(lclsPremium.nBulletins > 0, CStr(lclsPremium.sSerie) & "-" & String.Format("{0:00000000}", lclsPremium.nBulletins), "") 'ehh - Ad. vt fase II rsis 2
                    .Columns("valCollecto").DefValue = CStr(lclsPremium.nCollecto)
                    .Columns("tcdStatDate").DefValue = CStr(lclsPremium.dEffecdate)
                    .Columns("tcdExpirDat").DefValue = CStr(lclsPremium.dExpirDat)
                    .Columns("cbeCurrency").DefValue = CStr(lclsPremium.nCurrency)
                    .Columns("tcnPremium").DefValue = CStr(lclsPremium.nPremium)
                    .Columns("tcnPremiumn").DefValue = CStr(lclsPremium.nPremiumn)
                    .Columns("cbeStatus_Pre").DefValue = CStr(lclsPremium.nStatus_Pre)
                    .Columns("tcnContrat").DefValue = CStr(lclsPremium.nContrat)
                    .Columns("tcnDraft").DefValue = CStr(lclsPremium.nDraft)
                    .Columns("tcnAmount").DefValue = CStr(lclsPremium.nAmount)
                    .Columns("cbeStat_Draft").DefValue = CStr(lclsPremium.nStat_draft)
                    Response.Write(.DoRow)
                End With
            Next lclsPremium
        End If
        Response.Write(mobjGrid.closeTable())

        lclsPremium = Nothing
        lcolPremiums = Nothing
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc002")
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "coc002"
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "coc002"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "COC002", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 24/10/03 11:16 $|$$Author: Nvaplat9 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCollectionQue.aspx?mode=2">
    <%Response.Write(mobjValues.ShowWindowsName("COC002", Request.QueryString.Item("sWindowDescript")))%>
<TABLE WIDTH="100%">
    <BR>
        <%Call insDefineHeader()
Call insPreCOC002()%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




