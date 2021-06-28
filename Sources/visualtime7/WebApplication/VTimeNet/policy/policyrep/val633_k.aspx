<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsPolicy As ePolicy.ValPolicyTra

Dim mstrOptInfo As String

Dim mintBranch As String
Dim mintProduct As String
Dim mlngPolicy As String
Dim mlngCertif As String


'% insPreFolder: Se controla la carga de los datos de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFolder()
	'--------------------------------------------------------------------------------------------
	mstrOptInfo = "1"
	If Request.QueryString.Item("mintBranch") <> vbNullString Then
		mintBranch = Request.QueryString.Item("mintBranch")
		Session("nBranch") = mintBranch
	Else
		Session("nBranch") = vbNullString
	End If
	If Request.QueryString.Item("mintProduct") <> vbNullString Then
		mintProduct = Request.QueryString.Item("mintProduct")
		Session("nProduct") = mintProduct
	Else
		Session("nProduct") = vbNullString
	End If
	If Request.QueryString.Item("mlngPolicy") <> vbNullString Then
		mlngPolicy = Request.QueryString.Item("mlngPolicy")
		Session("nPolicy") = mlngPolicy
	Else
		Session("nPolicy") = vbNullString
	End If
	If Request.QueryString.Item("mlngCertif") <> vbNullString Then
		mlngCertif = Request.QueryString.Item("mlngCertif")
		Session("nCertif") = mlngCertif
	Else
		Session("nCertif") = vbNullString
	End If
	If Request.QueryString.Item("sOptInfo") <> vbNullString Then
		mstrOptInfo = Request.QueryString.Item("sOptInfo")
	End If
	Session("dEffecdate") = Today
	
	Call mclsPolicy.insPreVAL633_K("2", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("val633_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "val633_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsPolicy = New ePolicy.ValPolicyTra

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16:40 $|$$Author: Nvaplat61 $"
    
//% ChangePolicy: se maneja el cambio de valor de los campos de la página
//-------------------------------------------------------------------------------------------
function ChangePolicy(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(tcnPolicy.value=='')	{
			tcnCertif.value = 0;
			tcnCertif.disabled = false;
		}
		else
			insDefValues('insValsPolitype', 'nBranch=' + cbeBranchP.value + '&nProduct=' + valProductP.value + '&nPolicy=' + tcnPolicy.value + "&sExecCertif=1");
	}
}
//% ChangeBranch: se maneja el cambio de valor del ramo 
//-------------------------------------------------------------------------------------------
function ChangeBranch(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		tcnPolicy.value= '';
		tcnPolicy.disabled = true;
		tcnCertif.value = '';
		tcnCertif.disabled = true;
		dtcClient.value = '';
		UpdateDiv('tctCliename','');
		dtcClient_Digit.value = '';
	    valIntermedia.value = '';
		UpdateDiv('valIntermediaDesc','');
		tcnPayfreq.value = '';
	    tcdNextReceip.value = '';
	    tcnNegVPMonths.value = '';
	}
}

//% ChangeProduct: se maneja el cambio de valor del Producto 
//-------------------------------------------------------------------------------------------
 function ChangeProduct(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (cbeBranchP.value != ''){
			if (cbeBranchP.value != "0"){ 
				tcnPolicy.disabled = false;
			}
		}
	}
}

//% insStateZone: se controla el estado de los campos de la ventana
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//%	LoadPolicy: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadPolicy(Field){
//-------------------------------------------------------------------------------------------
	var strParams; 
	with (self.document.forms[0]){
		strParams = "nBranch="   + cbeBranchP.value + 
                    "&nProduct=" + valProductP.value +  
                    "&nPolicy="  + tcnPolicy.value + 
                    "&nCertif=" + tcnCertif.value 
	    insDefValues('LoadPolicy', strParams,'/VTimeNet/Policy/PolicyRep'); 
    }
}

//% InsChangeOptInfo: se controla el cambio para la opción de ejecución
//--------------------------------------------------------------------------------------------
function InsChangeOptInfo(Field){
//--------------------------------------------------------------------------------------------	
	with(self.document.forms[0]){
//+ Si es masivo
	    if(Field.value == "1"){
			tcdFromDate.disabled = false;
			tcdToDate.disabled = false;
			btn_tcdFromDate.disabled = false;
			btn_tcdToDate.disabled = false;
			cbeBranchM.disabled = false;
			cbeBranchP.disabled = true;
			valProductP.disabled = true;
			btnvalProductP.disabled = true;
			tcnPolicy.disabled = true;
			tcnCertif.disabled = true;

			ShowDiv('DIVP', 'hide');
			ShowDiv('DIVM', 'show');
			document.forms[0].tcdFromDate.focus();
		}
	    else{
//+ Si es puntual
			tcdFromDate.disabled = true;
			tcdToDate.disabled = true;
			btn_tcdFromDate.disabled = true;
			btn_tcdToDate.disabled = true;
			cbeBranchM.disabled = true;
			valProductM.disabled = true;
			btnvalProductM.disabled = true;
			cbeBranchP.disabled = false;


			ShowDiv('DIVM', 'hide');
			ShowDiv('DIVP', 'show');
			document.forms[0].cbeBranchP.focus();

			if (cbeBranchP.value != ""){
				if (cbeBranchP.value != "0"){ 
					valProductP.disabled = false; 
					btnvalProductP.disabled = false; 
					valProductP.focus();
				} 
			} 
			if (valProductP.value != "") { 
				valProductP.disabled = false; 
				btnvalProductP.disabled = false; 
				valProductP.focus();
			} 
			if (tcnPolicy.value != "") { 
				tcnPolicy.disabled = false; 
				tcnPolicy.focus();
			}
			if (tcnCertif.value != "") { 
				tcnCertif.disabled = false;
			}
	    }
	}
};

//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true; 
} 
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VAL633", "VAL633_k.aspx", 1, vbNullString))
	.Write(mobjMenu.setZone(1, "VAL633", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With

Call insPreFolder()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRenDateChange" ACTION="ValPolicyRep.aspx?sColtimre=<%=mclsPolicy.sColtimre%>&sPolitype=<%=mclsPolicy.sPolitype%>">
	<BR><BR>
    	<%=mobjValues.ShowWindowsName("VAL633", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD  COLSPAN="2"><%
If mstrOptInfo <> "2" Then
	Response.Write(mobjValues.OptionControl(0, "OptInfo", GetLocalResourceObject("OptInfo_1Caption"), "1", "1", "InsChangeOptInfo(this);",  , 1, GetLocalResourceObject("OptInfo_1ToolTip")))
Else
	Response.Write(mobjValues.OptionControl(0, "OptInfo", GetLocalResourceObject("OptInfo_1Caption"), "2", "1", "InsChangeOptInfo(this);",  , 1, GetLocalResourceObject("OptInfo_1ToolTip")))
End If
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%
If mstrOptInfo = "2" Then
	Response.Write(mobjValues.OptionControl(0, "OptInfo", GetLocalResourceObject("OptInfo_2Caption"), "1", "2", "InsChangeOptInfo(this);",  , 2, GetLocalResourceObject("OptInfo_2ToolTip")))
Else
	Response.Write(mobjValues.OptionControl(0, "OptInfo", GetLocalResourceObject("OptInfo_2Caption"), "2", "2", "InsChangeOptInfo(this);",  , 2, GetLocalResourceObject("OptInfo_2ToolTip")))
End If
%>
			</TD>
        </TR>    
	</TABLE>
	<DIV ID="DIVM">
		<TABLE WIDTH="100%">
			<TR>
			    <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			</TR>
			<TR>
			    <TD COLSPAN="5" CLASS="HorLine"></TD>
			</TR>
			<TR>
			    <TD COLSPAN="2" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
			    <TD COLSPAN="3">&nbsp;</TD>
			</TR>
			<TR>
			    <TD COLSPAN="2" CLASS="HorLine"></TD>
			    <TD COLSPAN="3"></TD>
			</TR>
			<TR>
				<TD><LABEL ID=13939><%= GetLocalResourceObject("tcdFromDateCaption") %></LABEL></TD>
				<TD><%=mobjValues.DateControl("tcdFromDate", vbNullString,  , GetLocalResourceObject("tcdFromDateToolTip"),  ,  ,  ,  , mstrOptInfo <> "1", CShort("3"))%></TD>
				<TD>&nbsp;</TD>
				<TD><LABEL ID=13937><%= GetLocalResourceObject("cbeBranchMCaption") %></LABEL></TD>
				<TD><%=mobjValues.BranchControl("cbeBranchM", GetLocalResourceObject("cbeBranchMToolTip"), vbNullString, "valProductM",  ,  ,  ,  , mstrOptInfo <> "1", CShort("5"))%></TD>
			</TR>
			<TR>
				<TD><LABEL ID=13942><%= GetLocalResourceObject("tcdToDateCaption") %></LABEL></TD>
				<TD><%=mobjValues.DateControl("tcdToDate", vbNullString,  , GetLocalResourceObject("tcdToDateToolTip"),  ,  ,  ,  , mstrOptInfo <> "1", CShort("4"))%></TD>
				<TD>&nbsp;</TD>
			    <TD><LABEL ID=13947><%= GetLocalResourceObject("valProductMCaption") %></LABEL></TD>
				<TD><%=mobjValues.ProductControl("valProductM", GetLocalResourceObject("valProductMToolTip"),  , eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , CShort("5"),  ,  , eFunctions.Values.eProdClass.clngActiveLife)%></TD>
			</TR>
			<TR>
			    <TD COLSPAN="5">&nbsp;</TD>
			</TR>        
		</TABLE>
    </DIV>
	<DIV ID="DIVP">
		<TABLE WIDTH="100%">
			<TR>
	            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
	        </TR>
	        <TR>
	            <TD COLSPAN="5" CLASS="HorLine"></TD>
	        </TR>        
			<TR>
				<TD><LABEL ID=13937><%= GetLocalResourceObject("cbeBranchMCaption") %></LABEL></TD>
				<TD><%=mobjValues.BranchControl("cbeBranchP", GetLocalResourceObject("cbeBranchPToolTip"), Session("nBranch"), "valProductP",  ,  ,  , "ChangeBranch()", mstrOptInfo <> "2")%></TD>
				<TD>&nbsp;</TD>
			    <TD><LABEL ID=13947><%= GetLocalResourceObject("valProductMCaption") %></LABEL></TD>
				<TD><%=mobjValues.ProductControl("valProductP", GetLocalResourceObject("valProductPToolTip"), Session("nBranch"), eFunctions.Values.eValuesType.clngWindowType, mstrOptInfo <> "2", Session("nProduct"),  ,  ,  , "ChangeProduct();",  ,  ,  , eFunctions.Values.eProdClass.clngActiveLife)%></TD>
	        </TR>
	        <TR>
	            <TD><LABEL ID=13946><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
	            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, Session("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "ChangePolicy()", True)%></TD>
	            <TD>&nbsp;</TD>
				<TD><LABEL ID=13938><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
				<TD><%
If mclsPolicy.sPolitype <> "1" Then
	Response.Write(mobjValues.NumericControl("tcnCertif", 10, Session("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "LoadPolicy(this)", mstrOptInfo <> "2"))
Else
	Response.Write(mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  ,  , mstrOptInfo <> "2"))
End If
%>
				</TD>
	        </TR>
			<TR>
	            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
	        </TR>
	        <TR>
	            <TD COLSPAN="5" CLASS="HorLine"></TD>
	        </TR>
	        <TR>
				<TD><LABEL ID=13945><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
				<TD COLSPAN="4"><%=mobjValues.ClientControl("dtcClient", mclsPolicy.sClient,  , GetLocalResourceObject("dtcClientToolTip"),  , True, "tctCliename")%></TD>	
			</TR>
			<TR>	
				<TD><LABEL ID=13941><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
	            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("valIntermedia", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsPolicy.nIntermed),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("valIntermediaToolTip"))%></TD>
	        </TR>
	        <TR>        
				<TD><LABEL ID=13825><%= GetLocalResourceObject("tcnPayfreqCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("tcnPayfreq", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mclsPolicy.nPayfreq),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("tcnPayfreqToolTip"))%></TD>
				<TD>&nbsp;</TD>
	            <TD><LABEL ID=13821><%= GetLocalResourceObject("tcdNextReceipCaption") %></LABEL></TD> 
				<TD><%=mobjValues.DateControl("tcdNextReceip", CStr(mclsPolicy.dNextReceip),  , GetLocalResourceObject("tcdNextReceipToolTip"),  ,  ,  ,  , True)%></TD>
			</TR>
			<TR>	
				<TD><LABEL ID=13941><%= GetLocalResourceObject("tcnNegVPMonthsCaption") %></LABEL></TD>
	            <TD COLSPAN="4"><%=mobjValues.NumericControl("tcnNegVPMonths", 2, CStr(mclsPolicy.nNegVPMonths),  , GetLocalResourceObject("tcnNegVPMonthsToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
	        </TR>
		</TABLE>
	</DIV>
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
If mstrOptInfo = "1" Then
	Response.Write("ShowDiv('DIVP', 'hide');")
	Response.Write("ShowDiv('DIVM', 'show');")
Else
	Response.Write("ShowDiv('DIVP', 'show');")
	Response.Write("ShowDiv('DIVM', 'hide');")
End If
If CStr(Session("sPolitype")) = "1" Then
	Response.Write("self.document.forms[0].tcnCertif.disabled=true;")
End If
Response.Write("</SCRIPT>")

mobjValues = Nothing
mobjMenu = Nothing
mclsPolicy = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("val633_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




