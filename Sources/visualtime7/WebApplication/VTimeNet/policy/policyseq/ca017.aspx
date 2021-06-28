<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="ePolicy" %> 
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores 

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mintIndex As Integer
Dim mclsPremium As eCollection.Premium
Dim mintDefaultReceipt As Object
Dim mstrListReceipt As String
Dim mblnReceiptexists As Boolean
Dim mdblPremium As Object
Dim mdatStatdate As Object
Dim mdatExpirDat As Object
Dim mstrLabel As String
Dim mclsErrors As eFunctions.Errors
Dim mclsProduct_li As eProduct.Product
Dim mblActivAPV As Boolean

Dim lblRecal As Boolean
Dim lclsProdMaster As eProduct.Product
Dim lclsMove_acc As eCashBank.Move_acc
Dim lobjPolicy As ePolicy.Policy
Dim llngProponum As Object
Dim lclsSecurity As eSecurity.User


'% insDefineHeader: Definición del encabezado del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctBill_itemColumnCaption"), "tctBill_item", 80, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctBill_itemColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0),  , GetLocalResourceObject("tcnPremiumColumnCaption"), True, 6)
		'+ Si la frecuencia de pago es única, no se muestra la prima anual
		If lobjPolicy.nPayfreq <> 6 Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumAnColumnCaption"), "tcnPremiumAn", 18, CStr(0),  , GetLocalResourceObject("tcnPremiumAnColumnCaption"), True, 6)
		End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountAfecColumnCaption"), "tcnAmountAfec", 18, CStr(0),  , GetLocalResourceObject("tcnAmountAfecColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountExentColumnCaption"), "tcnAmountExent", 18, CStr(0),  , GetLocalResourceObject("tcnAmountExentColumnToolTip"), True, 6)
	End With
	
	With mobjGrid
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("6ColumnCaption"), 6)
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA017")

lobjPolicy = New ePolicy.Policy
lclsMove_acc = New eCashBank.Move_acc
lclsProdMaster = New eProduct.Product
mobjValues = New eFunctions.Values

lclsSecurity = New eSecurity.User
Call lclsSecurity.Find(Session("nUsercode"), True)

'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
    var marrCA017 = []
    var mintCount = -1

//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 4 $|$$Date: 1/09/09 11:12a $|$$Author: Pmanzur $"

/*% ShowReceipts: Esta función se encarga de dibujar una tabla con el contenido de los datos */
/*% del recibo seleccionado el cual se encuentra almecenado en el arreglo.                   */
/*---------------------------------------------------------------------------------------------------------*/
function ShowReceipts(Field){
/*---------------------------------------------------------------------------------------------------------*/
    var mstrString = ""; 
    mstrString += document.location; 
    mstrString = mstrString.replace(/&nReceipt=.*/, ""); 
    mstrString = mstrString + "&nReceipt=" + Field.value + "&sListReceipt=" + document.forms[0].hddsList.value; 
    document.location = mstrString; 
} 
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("CA017", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCA017" ACTION="ValPolicySeq.aspx?x=1">
<%
Response.Write(mobjValues.ShowWindowsName("CA017", Request.QueryString.Item("sWindowDescript")))

mstrListReceipt = vbNullString
mclsPremium = New eCollection.Premium

Call lobjPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), True)

If mclsPremium.insValPrevInfo(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction")) Then
	
	lblRecal = True
	If Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Then
		If lclsProdMaster.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
			If lclsProdMaster.nProdClas = 4 Then
				If Session("nTransaction2") = eCollection.Premium.PolTransac.clngQuotationConvertion Or Session("nTransaction2") = eCollection.Premium.PolTransac.clngProposalConvertion Then
					llngProponum = Session("nProponum")
					
					If lclsMove_acc.Find_sClient(lobjPolicy.sClient) Then
						lblRecal = False
					End If
				Else
					llngProponum = lobjPolicy.nProponum
				End If
				
				
			End If
		End If
		'        Elseif Session("nTransaction") = clngProprehabilitate Then
		'		    lblRecal = False
	End If
	
	If Request.QueryString.Item("nReceipt") = vbNullString Then
		If lblRecal Then
			Call mclsPremium.insPreCA017(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), eRemoteDB.Constants.dtmNull, Session("nTransaction"), Session("nUsercode"), Session("sBrancht"))
			
			mintDefaultReceipt = mclsPremium.nReceiptdefault
			mstrListReceipt = mclsPremium.sListReceipt
			Session("sColinvot") = mclsPremium.sColinvot
		Else
			If mclsPremium.Find_Premium_CA001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
				mintDefaultReceipt = mclsPremium.nReceipt
				mstrListReceipt = mclsPremium.sListReceipt
			End If
		End If
	Else
		mintDefaultReceipt = mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble)
		mstrListReceipt = Request.QueryString.Item("sListReceipt")
	End If
End If
'Response.Write "<NOTSCRIPT>alert('''mstrListReceipt''': '" & mstrListReceipt & "');</SCRIPT>"
mblnReceiptexists = mclsPremium.InsReaCA017(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("sPolitype"), Session("sColinvot"), mintDefaultReceipt, Session("sBrancht"))

If mclsPremium.dStatdate <> eRemoteDB.Constants.dtmNull Then
	mdatStatdate = mclsPremium.dStatdate
End If
If mclsPremium.dExpirDat <> eRemoteDB.Constants.dtmNull Then
	mdatExpirDat = mclsPremium.dExpirDat
End If

'+ Si el Tipo de póliza es Colectiva, la facturación no es por certificado y
'+ no es la póliza matriz
If CStr(Session("sPolitype")) = "2" And CStr(Session("sColinvot")) <> "2" And CStr(Session("nCertif")) <> "0" Then
        mstrLabel = "cboReceiptsCaptionMov"
Else
        mstrLabel = "cboReceiptsCaption"
End If
%>           
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="15%"><LABEL><%= GetLocalResourceObject(mstrLabel)%></LABEL></TD>
            <TD><%

Response.Write(mobjValues.ComboControl("cboReceipts", mstrListReceipt, mintDefaultReceipt, False,  , GetLocalResourceObject("cboReceiptsToolTip"), "ShowReceipts(this)"))
Response.Write(mobjValues.HiddenControl("hddsList", mstrListReceipt))
%> 
            </TD>
        </TR>
        <TR>
            <TD COLSPAN="3"></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Vigencia"><%= GetLocalResourceObject("AnchorVigenciaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="3"></TD> 
            <TD COLSPAN="2" CLASS="Horline"></TD>        
        </TR>  
        <TR>
            <TD WIDTH="10%"><LABEL ID="19259"><%= GetLocalResourceObject("lblCurrencyCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.PossiblesValues("lblCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsPremium.nCurrency),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("lblCurrencyToolTip"))%></TD>
            <TD WIDTH="10%"><LABEL ID="19254"><%= GetLocalResourceObject("lblStartDateRCaption") %></LABEL></TD>
            <TD WIDTH="12%"><%=mobjValues.TextControl("lblStartDateR", 30, mdatStatdate,  , GetLocalResourceObject("lblStartDateRToolTip"), True)%></TD>
        </TR>
		<%If lclsSecurity.sType = "3" Then%>
			<TR> 
				<TD WIDTH="10%"></TD> 
			    <TD WIDTH="20%"></TD> 
			    <TD></TD> 
            	<TD WIDTH="10%"><LABEL ID="19255"><%= GetLocalResourceObject("lblExpirDateRCaption") %></label></TD>
			    <TD WIDTH="12%"><%=mobjValues.TextControl("lblExpirDateR", 30, mdatExpirDat,  , GetLocalResourceObject("lblExpirDateRToolTip"), True)%></TD>
			</TR>
		<%Else%>
			<TR> 
			    <TD WIDTH="10%"><LABEL ID="19260"><%= GetLocalResourceObject("lblCommisionCaption") %></LABEL></TD> 
			    <TD WIDTH="20%"><%=mobjValues.NumericControl("lblCommision", 30, mobjValues.StringToType(CStr(mclsPremium.nComission), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("lblCommisionToolTip"), True, 6, True)%></TD> 
			    <TD></TD> 
			    <TD WIDTH="10%"><LABEL ID="19255"><%= GetLocalResourceObject("lblExpirDateRCaption") %></label></TD>
			    <TD WIDTH="12%"><%=mobjValues.TextControl("lblExpirDateR", 30, mdatExpirDat,  , GetLocalResourceObject("lblExpirDateRToolTip"), True)%></TD>
			</TR>
		<%End If%>
			
    </TABLE> 
<%
Session("nReceipt") = mintDefaultReceipt
Call insDefineHeader()
If mblnReceiptexists Then
	For mintIndex = 0 To mclsPremium.mobjPremium.CountReceipts
		If mclsPremium.mobjPremium.ReceiptItem(mintIndex) Then
			With mobjGrid
				.Columns("tctBill_item").DefValue = mclsPremium.mobjPremium.sDescript
				.Columns("tcnPremium").DefValue = mclsPremium.mobjPremium.nPremium
				If lobjPolicy.nPayfreq <> 6 Then
					.Columns("tcnPremiumAn").DefValue = mclsPremium.mobjPremium.nPremAnual
				End If
				.Columns("tcnAmountAfec").DefValue = mclsPremium.mobjPremium.nAmountAf
				.Columns("tcnAmountExent").DefValue = mclsPremium.mobjPremium.nAmountEx
				mdblPremium = mdblPremium + mclsPremium.mobjPremium.nPremium
				
				Response.Write(mobjGrid.DoRow())
			End With
		End If
	Next 
End If
Response.Write(mobjGrid.CloseTable())
%>
    <BR>
    <TABLE WIDTH="100%">
        <TR>
             <TD WIDTH="23%"><LABEL ID="19257"><%= GetLocalResourceObject("tcnPremiumn_totalCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPremiumn_total", 18, mdblPremium,  ,  , True, 6, True)%></TD>
          </TR>
    </TABLE>
    <P ALIGN="Center">
<%
Response.Write(mobjValues.HiddenControl("hddPremium", mdblPremium))
Response.Write(mobjValues.AnimatedButtonControl("btnBegin", "/VTimeNet/images/btnBack.gif", GetLocalResourceObject("btnBeginToolTip"), "#BeginPage"))

If mclsPremium.bError Then
	mclsErrors = New eFunctions.Errors
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mclsErrors.sSessionID = Session.SessionID
	mclsErrors.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	Response.Write(mclsErrors.ErrorMessage("CA017", mclsPremium.nErrornum,  ,  ,  , True))
End If

If CStr(Session("sBrancht")) = "1" Then
	mclsProduct_li = New eProduct.Product
	If mclsProduct_li.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), True) Then
		If mclsProduct_li.nProdClas = 4 Or mclsProduct_li.nProdClas = 7 Then
			mblActivAPV = True
		Else
			mblActivAPV = False
		End If
	End If
	mclsProduct_li = Nothing
Else
	mblActivAPV = False
End If

    'If Not mblActivAPV Then
    If 1 = 2 Then
        If Not mclsPremium.insValInterComm(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mintDefaultReceipt) Then
            mclsErrors = New eFunctions.Errors
            Response.Write(mclsErrors.ErrorMessage("CA017", 55137, , , , True))
        End If
    End If

    '+ Se destruyen todas las instancias de los objetos que se han creado en esta página
    mclsPremium = Nothing
    mobjValues = Nothing
    mobjGrid = Nothing
    mclsErrors = Nothing

    lclsProdMaster = Nothing
    lclsMove_acc = Nothing
    lobjPolicy = Nothing
    lclsSecurity = Nothing
%>    
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA017")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




