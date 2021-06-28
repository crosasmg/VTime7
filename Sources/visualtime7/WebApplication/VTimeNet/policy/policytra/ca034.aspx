<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsPolicy As ePolicy.Policy
Dim lclsValptra As ePolicy.ValPolicyTra

Dim mclsCertificat As ePolicy.Certificat
Dim mclsRoles As ePolicy.Roles
Dim mclsClient As eClient.Client
Dim mclsAgents As eAgent.Intermedia
Dim mblnDisabled As Object

Dim mintTransacio As Byte
Dim mstrClienName As String
Dim mstrIntermedName As String
Dim lclsGuarant_Val As ePolicy.Guarant_val


'%insPreFolder: Esta función se encaga de validar todos los datos introducidos en la forma
'--------------------------------------------------------------------------------------------
Private Function insPreFolder() As Object
	'--------------------------------------------------------------------------------------------
	Dim lstrClient As String
	Dim lstrIntermed As String
	mintTransacio = 0
	
	With Request
		Call mclsPolicy.Find(.QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble))
		
		Call lclsValptra.InsPreVI009("1", .QueryString.Item("nExeMode"), .QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mclsPolicy.dStartdate, Session("nUsercode"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .QueryString.Item("sCodisplOri"))
		
		If lclsValptra.DefaultValueVI009("dEffecdate") <> eRemoteDB.Constants.dtmNull Then
			mclsPolicy.dnulldate = lclsValptra.DefaultValueVI009("dEffecdate")
		End If
		
		Call lclsGuarant_Val.Find(.QueryString.Item("sCertype"), CInt(.QueryString.Item("nBranch")), CInt(.QueryString.Item("nProduct")), CDbl(.QueryString.Item("nPolicy")), CDbl(.QueryString.Item("nCertif")), lclsValptra.DefaultValueVI009("tcnAge"), lclsValptra.DefaultValueVI009("tcnYear"), lclsValptra.DefaultValueVI009("tcnMonth"), Session("dRescuedate"))
		
		If CDbl(.QueryString.Item("nCertif")) = 0 Then
			mclsCertificat = mclsPolicy.Certificat(mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble))
			lstrClient = mclsCertificat.sClient
		Else
			Call mclsRoles.Find(.QueryString.Item("sCertype"), CInt(.QueryString.Item("nBranch")), CInt(.QueryString.Item("nProduct")), CDbl(.QueryString.Item("nPolicy")), CDbl(.QueryString.Item("nCertif")), 2, "", mclsPolicy.dStartdate)
			lstrClient = mclsRoles.sClient
		End If
		
	End With
	
	'-Se define la variable usada para manejar la información cargada por
	'-el metodo de tipo Collection llamado Find_CA033
	
	Dim lcol As Microsoft.VisualBasic.Collection
	With mclsAgents
		If .Find(mclsPolicy.nIntermed) Then
			If .sClient <> CStr(eRemoteDB.Constants.strnull) Then
				If .sClient <> CStr(eRemoteDB.Constants.strnull) Then
					lstrIntermed = .sClient
				Else
					lstrIntermed = vbNullString
				End If
			End If
		End If
	End With
	
	lcol = mclsClient.Find_CA033(lstrClient, lstrIntermed)
	mstrClienName = vbNullString
	mstrIntermedName = vbNullString
	If lcol.Count > 0 Then
		For	Each mclsClient In lcol
			With mclsClient
				If lstrClient = .sClient Then
					mstrClienName = .sCliename
				Else
					mstrIntermedName = .sCliename
				End If
			End With
		Next mclsClient
	End If
	
	lcol = Nothing
End Function

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA034")
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "CA034"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mclsPolicy = New ePolicy.Policy
	lclsValptra = New ePolicy.ValPolicyTra
	mclsCertificat = New ePolicy.Certificat
	mclsRoles = New ePolicy.Roles
	mclsClient = New eClient.Client
	mclsAgents = New eAgent.Intermedia
	lclsGuarant_Val = New ePolicy.Guarant_val
End With

Response.Write(mobjMenu.setZone(2, "CA034", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 10 $|$$Date: 16/08/04 16:58 $|$$Author: Nvaplat15 $"
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//% InsShowHeader: Muestra los valores en la página del encabezado
//-------------------------------------------------------------------
function InsShowHeader(){
//-------------------------------------------------------------------
    var lblnContinue=true
	if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].tcnCertif)!='undefined'){
				top.fraHeader.document.forms[0].cbeBranch.value=  '<%=Request.QueryString.Item("nBranch")%>'
				top.fraHeader.document.forms[0].valProduct.Parameters.Param1.sValue = '<%=Request.QueryString.Item("nBranch")%>'
				top.fraHeader.document.forms[0].valProduct.value=  '<%=Request.QueryString.Item("nProduct")%>'
				top.fraHeader.$('#valProduct').change();
				top.fraHeader.document.forms[0].tcnPolicy.value=  '<%=Request.QueryString.Item("nPolicy")%>'
				top.fraHeader.document.forms[0].tcnCertif.value=  '<%=Request.QueryString.Item("nCertif")%>'
				lblnContinue = false
			}
		}
	}
    if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
</SCRIPT>
</HEAD>
<%Call insPreFolder()%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmReahPolicy" ACTION="ValPolicyTra.aspx?nTransacio=<%=mintTransacio%>&sCertype=<%=Request.QueryString.Item("sCertype")%>&nBranch=<%=Request.QueryString.Item("nBranch")%>&nProduct=<%=Request.QueryString.Item("nProduct")%>&nPolicy=<%=Request.QueryString.Item("nPolicy")%>&nCertif=<%=Request.QueryString.Item("nCertif")%>&nExeMode=<%=Request.QueryString.Item("nExeMode")%>&nProcess=<%=Request.QueryString.Item("nProcess")%>&nServ_Order=<%=Request.QueryString.Item("nServ_Order")%>&nAgency=<%=Request.QueryString.Item("nAgency")%>&nProponum=<%=Request.QueryString.Item("nProponum")%>&sCodisplOri=<%=Request.QueryString.Item("sCodisplOri")%>"> 
    <%=mobjValues.ShowWindowsName("CA034", Request.QueryString.Item("sWindowDescript"))%>
<%
'+ Variable que habilita o deshabilita el check de generacion de solicitud dependiendo si
'+ la ejecucion es preliminar o definitiva
If Request.QueryString.Item("nExeMode") = "1" Then
	mblnDisabled = 1
Else
	mblnDisabled = 2
End If
    'mblnDisabled = 2
%>
    <P ALIGN="Center">
	<!--	<LABEL ID=41135><A HREF="#Rehabilitación"> <%= GetLocalResourceObject("AnchorRehabilitaciónCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=41137><A HREF="#Vigencia"> <%= GetLocalResourceObject("AnchorVigenciaCaption") %></A></LABEL>
    -->
    <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
		<TR>
            <TD ><LABEL ID=13906><%= GetLocalResourceObject("tcdNullDateCaption") %></LABEL></TD>
            <TD  ><%=mobjValues.DateControl("tcdNullDate", CStr(mclsPolicy.dnulldate),  , GetLocalResourceObject("tcdNullDateToolTip"),  ,  ,  ,  , CStr(Session("SCODISPL")) = "CA767")%></TD>
            <TD WIDTH="25%"><%=mobjValues.CheckControl("chkNullDevRec", GetLocalResourceObject("chkNullDevRecCaption"), lclsValptra.DefaultValueVI009("sNull_rec"))%><BR></TD>
            <TD WIDTH="25%"><%=mobjValues.CheckControl("chkAdicCover", GetLocalResourceObject("chkAdicCoverCaption"), "2")%><BR></TD>
       </TR>
       <TR>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkNullReceipt", GetLocalResourceObject("chkNullReceiptCaption"), CStr(1))%></TD>
            <TD COLSPAN="2"><%=mobjValues.HiddenControl("ValNullLetter", "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13907><%= GetLocalResourceObject("nDay_payCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("nDay_pay", 3, CStr(30),  , GetLocalResourceObject("nDay_payToolTip"))%></TD>
            <TD WIDTH="5%"><LABEL ID=13907><%= GetLocalResourceObject("SCA2-34Caption") %></LABEL></TD>
            <TD><%=mobjValues.ButtonNotes("SCA2-34", lclsValptra.DefaultValueVI009("nNotenum"), False, False)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" WIDTH="50%"><%=mobjValues.CheckControl("chkRescRequest", GetLocalResourceObject("chkRescRequestCaption"), mblnDisabled, CStr(1), , True)%></TD>
			<TD COLSPAN="2" WIDTH="50%"><%=mobjValues.CheckControl("chkRescReport", GetLocalResourceObject("chkRescReportCaption"), CStr(1), CStr(1))%></TD>
        </TR>
    </TABLE>
	<TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
	    <TR>
			<TD WIDTH="50%">
				<TABLE WIDTH="100%">
					<TR>
						<TD COLSPAN="2"><BR></TD>
					</TR>
					<TR>
						<TD COLSPAN="2"></TD>
					</TR>
					<TR>
						<TD WIDTH="20%"><LABEL ID=13903 ><%= GetLocalResourceObject("lblClienNameCaption") %></LABEL></TD>
						<TD WIDTH="30%"><%=mobjValues.TextControl("lblClienName", 30, mstrClienName,  , "", True)%></TD>
					</TR>
					<TR>
			 			 <TD WIDTH="20%"><LABEL ID=13905><%= GetLocalResourceObject("lblInterNameCaption") %></LABEL></TD>
						 <TD WIDTH="30%"><%=mobjValues.TextControl("lblInterName", 30, mstrIntermedName,  , "", True)%></TD>
					</TR>
			    </TABLE>
			</TD>
            <TD WIDTH="50%">
                <TABLE WIDTH="100%">
					<TR>
						<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
					</TR>
					<TR>
						<TD COLSPAN="2" CLASS="HorLine"></TD>
					</TR>
					<TR>
						<TD WIDTH="20%"><LABEL ID=13910><%= GetLocalResourceObject("lblStartdatCaption") %></LABEL></TD>
						<TD WIDTH="30%"><%=mobjValues.TextControl("lblStartdat", 30, mobjValues.TypeToString(mclsCertificat.dStartdate, eFunctions.Values.eTypeData.etdDate),  , "", True)%></TD>
				    </TR>
				    <TR>
						<TD WIDTH="20%"><LABEL ID=13904><%= GetLocalResourceObject("lblExpirDatCaption") %></LABEL></TD>
						<TD WIDTH="30%"><%=mobjValues.TextControl("lblExpirDat", 30, mobjValues.TypeToString(mclsCertificat.dExpirdat, eFunctions.Values.eTypeData.etdDate),  , "", True)%></TD>
			        </TR>
			    </TABLE>
			</TD>
		</TR>
		<TABLE WIDTH="100%">
			<TR>
				<TD WIDTH="35%"><LABEL ID=13906><%= GetLocalResourceObject("lblNullDateCaption") %></LABEL></TD>
				<TD WIDTH="15%"><%=mobjValues.TextControl("lblNullDate", 30, mobjValues.TypeToString(mclsCertificat.dnulldate, eFunctions.Values.eTypeData.etdDate),  , "", True)%></TD>
				<TD WIDTH="50%" COLSPAN="2"></TD>
			</TR>
	    </TABLE>
    </TABLE>
        <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD WIDTH="50%"><LABEL ID=13852><%= GetLocalResourceObject("nBalanceCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("nBalance", 25, CStr(lclsGuarant_Val.nResc_val),  , GetLocalResourceObject("nBalanceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			<TD WIDTH="50%"><LABEL ID=13852><%= GetLocalResourceObject("nSalvage_CurrCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("nSalvage_Curr", 25, CStr(lclsGuarant_Val.nSald_val),  , GetLocalResourceObject("nSalvage_CurrToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
	</TABLE>    
    <%
With Response
	.Write("<SCRIPT>InsShowHeader()</script><BR>")
End With
lclsGuarant_Val = Nothing
lclsValptra = Nothing
%>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
mobjMenu = Nothing
mclsClient = Nothing
mclsAgents = Nothing
mclsPolicy = Nothing
mclsCertificat = Nothing
mclsRoles = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("CA034")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




