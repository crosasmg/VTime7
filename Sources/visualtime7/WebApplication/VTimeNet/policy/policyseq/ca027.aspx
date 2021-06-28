<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de menu 
Dim mobjMenu As eFunctions.Menues

Dim ldblnpNocons As Object
Dim ldblnpOutStand As Object
Dim ldblAdjust As Object
Dim ldblCurrency As Integer
Dim ldblCommission As Object
Dim ldtmExpirDateR As String
Dim ldtmStartDateR As String
Dim ldblReceipt As Double
Dim lstrDocument As String
Dim lstrReceipts As String
Dim mobjGrid As eFunctions.Grid
Dim lobjTDetail_pre As ePolicy.TDetail_pre
Dim lCol As Microsoft.VisualBasic.Collection
Dim lstrQueryString As String
Dim mblnCount As Boolean
Dim mblnError As Boolean
Dim nErrornum As Integer
    Dim sWindowType As String

'% insInitialGridCA027: se muestran los datos del recibo en el grid
'------------------------------------------------------------------------------------
Private Sub insInitialGridCA027()
'------------------------------------------------------------------------------------
	Dim lobjPremium As eCollection.Premium
	Dim lobjtdetail_pre2 As Object
	
	If Not lCol Is Nothing Then
		For	Each lobjTDetail_pre In lCol
			With lobjTDetail_pre
				mobjGrid.Columns("tctConcept").DefValue = .sDescript
				mobjGrid.Columns("tcnPremiumAn").DefValue = CStr(.nPremium_an)
				mobjGrid.Columns("tcnP_NotCons").DefValue = CStr(.nP_NotCons)
				mobjGrid.Columns("tcnnP_Adjust").DefValue = CStr(.np_OutStand)
				mobjGrid.Columns("nAdjust").DefValue = CStr(.nP_Adjust)
				mobjGrid.Columns("tcnAmountAfec").DefValue = CStr(.nAmountAf)
				mobjGrid.Columns("tcnAmountExent").DefValue = CStr(.nAmountEx)
				Response.Write(mobjGrid.DoRow())
				
				Session("sKey") = .sKey
				'+ Se acumulan todas la primas no consumidas
				ldblnpNocons = ldblnpNocons + .nP_NotCons
				'+ Se acumulan las primas por devengar            
				ldblnpOutStand = ldblnpOutStand + .np_OutStand
				'+ Se acumulan los ajustes.
				ldblAdjust = ldblAdjust + .nP_Adjust
			End With
		Next lobjTDetail_pre
	End If
	
	If mblnError Then
		If nErrornum = 0 Then
			Response.Write("<SCRIPT>alert(""Err. 60584: " & eFunctions.Values.GetMessage(60584) & """);</" & "Script>")
		End If
	Else
		'+ Si no se generó recibo, se envía mensaje
		lobjPremium = New eCollection.Premium
		
		'+ Solo se manda la validacion 55947 cuando la prima ajuste total sea 0
		
		'+ Si es llamada desde la anulación
		If CStr(Session("sCodisplOri")) = "CA033" Then
			'+ Si no existen recibos a la fecha de anulación
			If Not lobjPremium.InsValMaxDexpirdateReceipt(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				If nErrornum = 0 Then
					If ldblAdjust = 0 Then
						Response.Write("<SCRIPT>alert(""Men. 55947: " & eFunctions.Values.GetMessage(55947) & ", no corresponde según fecha de anulación"");</" & "Script>")
					End If
				End If
			Else
				If nErrornum = 0 Then
					If ldblAdjust = 0 Then
						Response.Write("<SCRIPT>alert(""Men. 55947: " & eFunctions.Values.GetMessage(55947) & """);</" & "Script>")
					End If
				End If
			End If
		Else
			If ldblAdjust = 0 Then
				If nErrornum = 0 Then
					'			        if session("sCertype") = "6" then
					'						Set lobjtdetail_pre2 = Server.CreateObject("ePolicy.tdetail_pre")
					'						If not lobjtdetail_pre2.Val_nreceiptauto("2", '													         Session("nBranch"), '															 Session("nProduct"), '															 Session("nPolicy_Old"), '															 Session("certificat"), '															 Session("dEffecdate")) Then
					'							Response.Write "<NOTSCRIPT>alert(""Men. 55947: " & eFunctions.Values.GetMessage(55947) & ", Debe generar recibo manual"");</" & "Script>"
					'						Else
					'						    Response.Write "<NOTSCRIPT>alert(""Men. 55947: " & eFunctions.Values.GetMessage(55947) & """);</" & "Script>"
					'						End if
					'				    Else
					Response.Write("<SCRIPT>alert(""Men. 55947: " & eFunctions.Values.GetMessage(55947) & """);</" & "Script>")
					'				    End If		        
				End If
			End If
		End If
		lobjPremium = Nothing
	End If
	
	lobjTDetail_pre = Nothing
End Sub

'% insInitialCA027: se realiza el cálculo del recibo
'------------------------------------------------------------------------------------
Private Sub insInitialCA027()
	'------------------------------------------------------------------------------------
	'+ Se revisan los recibos generados por la rutina de cálculo
	Dim intExeMode As String
	Dim lclsPremium As eCollection.Premium
	Dim lclsPolicy_his As ePolicy.Policy_his
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim ldtmEffecdate_ca033 As Object
	Dim lstrMessage As String
	
	lclsPolicy_his = New ePolicy.Policy_his
	
	mblnCount = False
	mblnError = False
	
	If Request.QueryString.Item("sCodispl") = "CA027A" Then
		nErrornum = lobjTDetail_pre.InsValPreCA027A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"))
	Else
		nErrornum = 0
	End If
	If lclsPolicy_his.FindLastMovement(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
		If lclsPolicy_his.nReceipt <> eRemoteDB.Constants.intNull Then
			lclsPremium = New eCollection.Premium
			
			If lclsPremium.Find(Session("sCertype"), lclsPolicy_his.nReceipt, Session("nBranch"), Session("nProduct"), 0, 0) Then
				If lclsPremium.sManauti = "1" Then
					mblnError = True
				End If
			End If
			lclsPremium = Nothing
		End If
	End If
	
        If Request.QueryString.Item("sOnSeq") = "1" Then
            sWindowType = eRemoteDB.Constants.strNull
        Else
            sWindowType = "PopUp"
        End If
        
        lCol = Nothing
        If nErrornum = 0 Then
            If Not mblnError Then
                If Request.QueryString.Item("sOnSeq") = "1" Then
                    intExeMode = "2"
                Else
                    If Request.QueryString.Item("nExeMode") = vbNullString Then
                        intExeMode = "0"
                    Else
                        intExeMode = Request.QueryString.Item("nExeMode")
                    End If
                End If
			
                '+Cuando es llamada de la anulacion de polizas
                If CStr(Session("sCodisplOri")) = "CA033" Then
                    ldtmEffecdate_ca033 = Request.QueryString.Item("dNullDate")
                    If IsNothing(Request.QueryString.Item("dNullDate")) Then
                        ldtmEffecdate_ca033 = Session("dEffecdate")
                    End If
                Else
                    ldtmEffecdate_ca033 = Session("dEffecdate")
                End If
			
                lCol = lobjTDetail_pre.InsPreCA027(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(ldtmEffecdate_ca033, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dLedgerDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("SessionId"), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nPercent"), eFunctions.Values.eTypeData.etdDouble), 11, "2", Request.QueryString.Item("soptDev"), eRemoteDB.Constants.intNull, mobjValues.StringToType(intExeMode, eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("nProcess"), Request.QueryString.Item("sAdicCover"))
                If Not lCol Is Nothing Then
                    For Each lobjTDetail_pre In lCol
                        With lobjTDetail_pre
                            mblnCount = True
                            ldblCurrency = .nCurrency
                            ldblCommission = .nCommision
						
                            '+Cuando es llamada de la anulacion de polizas
                            If CStr(Session("sCodisplOri")) = "CA033" Then
                                ldtmEffecdate_ca033 = Request.QueryString.Item("dNullDate")
                            Else
                                ldtmEffecdate_ca033 = Session("dEffecdate")
                            End If
                            ldtmExpirDateR = mobjValues.TypeToString(.dExpirdat, eFunctions.Values.eTypeData.etdDate)
                            ldtmStartDateR = mobjValues.TypeToString(ldtmEffecdate_ca033, eFunctions.Values.eTypeData.etdDate)
                            ldblReceipt = .nReceipt
                            lstrDocument = .sDocument
                            lstrReceipts = .sReceipts
                            Exit For
                        End With
                    Next lobjTDetail_pre
                End If
            End If
        Else
            lclsGeneral = New eGeneral.GeneralFunction
            lstrMessage = lclsGeneral.insLoadMessage(nErrornum)
            Response.Write("<SCRIPT>alert(""Men. " & nErrornum & ": " & lstrMessage & """);</" & "Script>")
        End If
	
        lobjTDetail_pre = Nothing
        lclsPolicy_his = Nothing
	
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA027")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

nErrornum = 0

'+ Cuando es llamada desde la CA033 se agrega variables al QueryString
If CStr(Session("sCodisplOri")) <> "CA034" Then
	lstrQueryString = "&sCertype=" & Request.QueryString.Item("sCertype") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dNullDate=" & Request.QueryString.Item("dNullDate") & "&sNullReceipt=" & Request.QueryString.Item("sNullReceipt") & "&soptReceipt=" & Request.QueryString.Item("soptReceipt") & "&nExeMode=" & Request.QueryString.Item("nExeMode") & "&sExeReport=" & Request.QueryString.Item("sExeReport") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&nProponum=" & Request.QueryString.Item("nProponum") & "&nNullCode=" & Request.QueryString.Item("nNullCode")
Else
	'+ Cuando es llamada desde la CA034 se agrega variables al QueryString
	lstrQueryString = "&nExeMode=" & Request.QueryString.Item("nExeMode") & "&sExeReport=" & Request.QueryString.Item("sExeReport") & "&nAgency=" & Request.QueryString.Item("nAgency") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&dNullDate=" & Request.QueryString.Item("dNullDate") & "&chkNullReceipt=" & Request.QueryString.Item("chkNullReceipt") & "&sBrancht=" & Request.QueryString.Item("sBrancht")
End If

lobjTDetail_pre = New ePolicy.TDetail_pre
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjValues.ActionQuery = Session("bQuery")

ldblnpNocons = 0
ldblnpOutStand = 0
ldblAdjust = 0

Call insInitialCA027()

%>    
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("sOnSeq") = "1" Then
	mobjMenu = New eFunctions.Menues
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
mobjMenu = Nothing
%>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCA027" ACTION="valPolicySeq.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&WindowType=<%=sWindowType%><%=lstrQueryString%>">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=13554><A NAME="Recibo"><%= GetLocalResourceObject("AnchorReciboCaption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=40994><A NAME="Vigencia"><%= GetLocalResourceObject("AnchorVigenciaCaption") %></A></LABEL></TD>                                
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
                
        <TR>
            <TD><LABEL ID=0>
				<% 				    
				    If lstrDocument <> vbNullString Then
				        Response.Write(mobjValues.getMessage(CShort(CStr(lstrDocument))))
				    Else
				        Response.Write(mobjValues.getMessage(7))
				    End If
                %>
				</LABEL>
                <%= mobjValues.TextControl("",30, ldblReceipt, ,"", True) %>
			</TD>
            <TD ><LABEL ID=40995><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
            </TD>
            <TD>&nbsp;</TD>
            <TD>
                <LABEL ID=19286><%= GetLocalResourceObject("lblStartDateRCaption") %></LABEL>
                <%=mobjValues.TextControl("lblStartDateR", 30, ldtmStartDateR,  , "", True)%>
            </TD>
            <TD>
                <LABEL ID=19240><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>
                <%= mobjValues.TextControl("lblExpirDateR", 30, ldtmExpirDateR, , "", True)%>
            </TD>
        </TR>
        <TR><TD COLSPAN="5">&nbsp;</TD></TR>
        <TR>
            <TD COLSPAN="2"><LABEL ID=19289><%= GetLocalResourceObject("lblCurrencyCaption") %></LABEL>
			<%=mobjValues.PossiblesValues("lblCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(ldblCurrency),  , True)%>
            </TD>
            <TD>&nbsp;</TD>                                
            <TD COLSPAN="2"><LABEL ID=19290><%= GetLocalResourceObject("lblCommissionCaption") %></LABEL>
            <%=mobjValues.NumericControl("lblCommission", 18, ldblCommission,  , "", True, 6, True)%>
            </TD>
        </TR>
		<%
'+ Si se invoca desde la secuencia de Cartera
If (Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifPropAmendent) And Request.QueryString.Item("sOnSeq") = "1" Then%>
        <TR>
			<TD COLSPAN="5"><%=mobjValues.CheckControl("chkDelReceipt", GetLocalResourceObject("chkDelReceiptCaption"),  , "1",  , Not mblnCount Or mblnError,  , GetLocalResourceObject("chkDelReceiptToolTip"))%></TD>
		</TR>
		<%End If%>
    </TABLE>    
    <BR>
	<DIV ID="Scroll" STYLE="height:100;overflow:auto; outset gray">
<%
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
With mobjGrid.Columns
	.AddTextColumn(41002, GetLocalResourceObject("tctConceptColumnCaption"), "tctConcept", 25, vbNullString)
	.AddNumericColumn(40998, GetLocalResourceObject("tcnPremiumAnColumnCaption"), "tcnPremiumAn", 18, CStr(0),  ,  , True, 6)
	.AddNumericColumn(40999, GetLocalResourceObject("tcnP_NotConsColumnCaption"), "tcnP_NotCons", 18, CStr(0),  ,  , True, 6)
	.AddNumericColumn(41000, GetLocalResourceObject("tcnnP_AdjustColumnCaption"), "tcnnP_Adjust", 18, CStr(0),  ,  , True, 6)
	.AddNumericColumn(41001, GetLocalResourceObject("nAdjustColumnCaption"), "nAdjust", 18, CStr(0),  ,  , True, 6)
	.AddNumericColumn(0, GetLocalResourceObject("tcnAmountAfecColumnCaption"), "tcnAmountAfec", 18, CStr(0),  ,  , True, 6)
	.AddNumericColumn(0, GetLocalResourceObject("tcnAmountExentColumnCaption"), "tcnAmountExent", 18, CStr(0),  ,  , True, 6)
End With
mobjGrid.Columns("Sel").GridVisible = False
mobjGrid.AddButton = False
mobjGrid.DeleteButton = False
mobjGrid.Splits_Renamed.AddSplit(0, GetLocalResourceObject("7ColumnCaption"), 7)
Call insInitialGridCA027()
Response.Write(mobjGrid.closeTable())
mobjGrid = Nothing
%>
	</DIV>
    <BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40997><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
		</TR>
        <TR>
			<TD COLSPAN="5" CLASS="Horline"></TD>
		</TR>
        <TR>
            <TD><LABEL ID=19281><%= GetLocalResourceObject("lblPremNoConCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.NumericControl("lblPremNoCon", 30, ldblnpNocons,  , "", True, 6, True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=19280><%= GetLocalResourceObject("lblPremNewConCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("lblPremNewCon", 30, ldblnpOutStand,  , "", True, 6, True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=19282><%= GetLocalResourceObject("lblPremiumPayCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("lblPremiumPay", 30, ldblAdjust,  , "", True, 6, True)%></TD>
        </TR>
    </TABLE>
    <%If Request.QueryString.Item("sOnSeq") <> "1" Then%>
    <P ALIGN=RIGHT>
		<TABLE WIDTH=100%>
			<TR>
				<TD CLASS="Horline"></TD>
			</TR>
			<TR>
				<TD ALIGN=RIGHT><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyAccept)%></TD>
			</TR>
		</TABLE>
    </P>
    <%End If%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA027")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>

<%
mobjValues = Nothing

If Not mblnCount Or mblnError Then
	Response.Write("<SCRIPT>self.document.forms[0].lblReceipt.disabled=true</SCRIPT>")
End If
%>





