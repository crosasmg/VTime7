<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjCollection As eCollection.Premium

    Dim mobjGrid As eFunctions.Grid

    '**%Objetive: Defines the columns of the grid 
    '%Objetivo: Define las columnas del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid

        With mobjGrid
            .sSessionID = Session.SessionID
            .sCodisplPage = Request.QueryString.Item("sCodispl")
        End With

        '**+ The columns of the grid are defined
        '+ Se definen las columnas del grid  

        With mobjGrid.Columns
            Call .AddNumericColumn(0, "Recibo", "tcnRecibo", 10,, False, "Número del recibo",,,,,, False)
            Call .AddDateColumn(0, "Desde", "tcdDesde",, False, "Fecha de inicio de vigencia del recibo",,,, False)
            Call .AddDateColumn(0, "Hasta", "tcdHasta",, False, "Fecha de vencimiento del recibo",,,, False)
            Call .AddTextColumn(0, "Origen del recibo", "tctOrigen", 12, "", False, "Descripción abreviada de la transacción que dio origen al recibo",,,, False)
            Call .AddNumericColumn(0, "Prima Neta", "tcnPrima", 18,, False, "Inporte de la prima neta", True, 6,,,, False)
            Call .AddHiddenColumn("hddCurrency", 0)
            Call .AddHiddenColumn("hddLedInvo", 0)
            Call .AddHiddenColumn("hddTratypei", 0)
            Call .AddHiddenColumn("hddClient", "")
            Call .AddHiddenColumn("hddCliename", "")
            Call .AddHiddenColumn("hddContrat", "")
        End With

        '**+ The general properties of the grid are defined
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .Height = 350
            .Width = 280
            .nMainAction = Request.QueryString.Item("nMainAction")
            .Columns("Sel").GridVisible = False
            .AddButton = False
            .DeleteButton = False

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .SetWindowParameters(Request.QueryString.Item("sCodispl"),
                             Request.QueryString.Item("sWindowDescript"),
                             Request.QueryString.Item("nWindowTy"))

            Call .Splits_Renamed.AddSplit(0, "", 1)
            Call .Splits_Renamed.AddSplit(0, "Vigencia", 2)
            Call .Splits_Renamed.AddSplit(0, "", 2)
        End With
    End Sub

    '%**Objetive: The controls of the page are load
    '%Objetivo: Se cargan los controles de la página
    '-------------------------------------------------------------------------------------------
    Private Sub insPreSCO6000()
        '-------------------------------------------------------------------------------------------
        Dim lcolPremiums As eCollection.Premiums
        Dim lclsPremium As eCollection.Premium
        Dim lintIndex

        lintIndex = 0
        lcolPremiums = New eCollection.Premiums
        With mobjGrid

            If lcolPremiums.FindReceipt_Pol("2",
                                            Request.QueryString.Item("nBranch"),
                                            Request.QueryString.Item("nProduct"),
                                            Request.QueryString.Item("nPolicy"),
                                            Request.QueryString.Item("nCertif"),
                                            Request.QueryString.Item("sPoliType"),
                                            "",
                                            mobjValues.StringToType(Request.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble),
                                            mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate),
                                            Request.QueryString.Item("sDevReceipt")) Then

                For Each lclsPremium In lcolPremiums
                    .Columns("tcnRecibo").DefValue = lclsPremium.nReceipt
                    .Columns("tcdDesde").DefValue = lclsPremium.dEffecdate
                    .Columns("tcdHasta").DefValue = lclsPremium.dExpirDat
                    .Columns("tctOrigen").DefValue = lclsPremium.sCadena
                    .Columns("tcnPrima").DefValue = lclsPremium.nPremium

                    .Columns("hddCurrency").DefValue = lclsPremium.nCurrency
                    .Columns("hddTratypei").DefValue = lclsPremium.nTratypei
                    .Columns("hddLedInvo").DefValue = lclsPremium.sLeadinvo
                    .Columns("hddClient").DefValue = lclsPremium.sClient

                    .Columns("hddCliename").DefValue = lclsPremium.sCliename

                    .Columns("hddContrat").DefValue = lclsPremium.nContrat

                    .Columns("tcnRecibo").HRefScript = "insCloseWindows2(this," & CStr(lintIndex) & ");"
                    lintIndex = lintIndex + 1

                    Response.Write(mobjGrid.DoRow())
                Next
            End If
        End With
        Response.Write(mobjGrid.closeTable)
        mobjValues.ActionQuery = False

        Response.Write("	<TABLE WIDTH=""100%"">")
        Response.Write("	    <TR>")
        Response.Write("	        <TD CLASS=""HeightRow""></TD>")
        Response.Write("	    </TR>")
        Response.Write("	    <TR>")
        Response.Write("	        <TD CLASS=""HorLine""></TD>")
        Response.Write("	    </TR>")
        Response.Write("	    <TR>")
        Response.Write("	        <TD ALIGN = ""RIGHT"" >")
        Response.Write(mobjValues.ButtonAcceptCancel("window.close();",,,, eFunctions.Values.eButtonsToShow.OnlyCancel))
        Response.Write("	        </TD>")
        Response.Write("	    </TR>")
        Response.Write("	</TABLE>")

        lclsPremium = Nothing
        lcolPremiums = Nothing
    End Sub

</script>
<%Response.Expires = -1441
        mobjNetFrameWork = New eNetFrameWork.Layout
        mobjNetFrameWork.sSessionID = Session.SessionID
        mobjNetFrameWork.nUsercode = Session("nUsercode")
        Call mobjNetFrameWork.BeginPage("SCO6000")

        mobjValues = New eFunctions.Values
        mobjNetFrameWork.sSessionID = Session.SessionID
        mobjNetFrameWork.nUsercode = Session("nUsercode")
        Call mobjNetFrameWork.BeginPage("SCO6000")

        mobjCollection = New eCollection.Premium

        mobjValues.sCodisplPage = "SCO6000"


%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%'<TITLE>Datos de verificación del recibo</TITLE>
        Response.Write(mobjValues.StyleSheet())
        Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
%>
<SCRIPT>
//**%Objetive: It allows to cancel the page
//%Objetivo: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//**%Objetive: It allows to finish the page
//%Objetivo: Permite finalizar la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
    return true;
}

//**%Objetive: The actions are defined
//%Objetivo: Se definen las acciones.
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 301:
        case 302:
        case 305:        
        case 401:
            document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
            break;
    }
}
//**%Objetive: 
//%Objetivo: 
//-------------------------------------------------------------------------------------------------------------------
function insCloseWindows2 (Field,lintIndex){
//-------------------------------------------------------------------------------------------------------------------

//Esta variable indica si la pgina se esta llamando desde la CA027 o de la CA028

    var nintReceiptManu = '<%=Request.QueryString.Item("ReceiptManu")%>';
    var nSequence = '<%=Request.QueryString.Item("nSequence")%>';
    
    if (top.opener.top.fraFolder!= null)
		var frm = top.opener.top.fraFolder;
	else
		var frm = top.opener.top;
       
   if (typeof(frm.document.forms[0].tcnReceipt_Collec)!='undefined'){
       frm.document.forms[0].tcnReceipt_Collec.value = marrArray[lintIndex].tcnRecibo;
       frm.document.forms[0].tcnPremium_Collec.value = marrArray[lintIndex].tcnPrima
       
       if (nintReceiptManu==1)
       {
           if (nSequence==1)
           {
           frm.document.forms[0].tctOrigReceipt.value = marrArray[lintIndex].hddLedInvo;
           frm.document.forms[0].cbeCurrency.value = marrArray[lintIndex].hddCurrency;
           
           frm.document.forms[0].hddClient_policy.value = marrArray[lintIndex].hddClient;
           frm.document.forms[0].tcnContrat.value = marrArray[lintIndex].hddContrat;
           }
           else
           {
           frm.document.forms[0].tctOrigReceipt.value = marrArray[lintIndex].hddLedInvo;
           frm.document.forms[0].cbeCurrency.value = marrArray[lintIndex].hddCurrency;

           frm.document.forms[0].tctClient.value = marrArray[lintIndex].hddClient;
		   frm.document.forms[0].tcnContrat.value = marrArray[lintIndex].hddContrat;
		   
           frm.UpdateDiv('tctClient_Name',marrArray[lintIndex].hddCliename);
           
           frm.document.forms[0].tctClient.disabled = true;
           frm.document.forms[0].btntctClient.disabled = true;
           frm.document.forms[0].tcnReceipt_Collec.blur();
           }
       }    
           
   }
   window.close(); 
}
</SCRIPT>
<%
    With Request
        mobjValues.ActionQuery = (.QueryString.Item("nMainAction") = vbNullString)
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSCO6000" ACTION="ValGeneralForm.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Response.Write("<BR><BR>")
    End If
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"),
                                      Request.QueryString.Item("sWindowDescript")))
    insDefineHeader()
    insPreSCO6000()

    mobjGrid = Nothing
    mobjValues = Nothing
    mobjCollection = Nothing
    '^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
    Call mobjNetFrameWork.FinishPage("SCO6000")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer
%>
</FORM>
</BODY>
</HTML>





