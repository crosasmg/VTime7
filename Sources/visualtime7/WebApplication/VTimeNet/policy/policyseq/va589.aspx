<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'+ variables de objecto de clase    
Dim mclsActivelife As ePolicy.Activelife


'% insPreVA589: Carga los datos iniciales de pantalla
'-------------------------------------------------------------------------------------------------
Sub insPreVA589()
	'-------------------------------------------------------------------------------------------------
	
	mclsActivelife.insPreVA589(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dStartdate"), eFunctions.Values.eTypeData.etdDate))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VA589")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mclsActivelife = New ePolicy.Activelife
Call insPreVA589()
%>
<HTML>
<HEAD> 


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VA589", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeField(nPremdeal){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (nPremdeal!=''){
            tcnCalPrem.value = insConvertNumber(nPremdeal)
                               * insConvertNumber(hddnRatepayf.value);
            tcnCalPrem.value = (Math.round(tcnCalPrem.value * 100) / 100);
            tcnCalPrem.value = VTFormat(tcnCalPrem.value, '', '', '', tcnCalPrem.DecimalPlace, true);
        }
        else tcnCalPrem.value = '';
    }
}

//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeTypeInvest(nTypeInvest){
//--------------------------------------------------------------------------------------------
    
	var strParams; 
	with (self.document.forms[0]){
		strParams = "nTypeInvest=" + nTypeInvest +
					"&nModulec=" + hddnModulec.value;
      
        insDefValues("TypeInvest",strParams,'/VTimeNet/Policy/PolicySeq'); 
   }
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmVA589" ACTION="ValPolicySeq.aspx?x=1">
    <P ALIGN="Center">
    <LABEL ID=0><A HREF="#Seguro"><%= GetLocalResourceObject("AnchorSeguroCaption") %></A></LABEL><LABEL ID=0>|</LABEL>
    <LABEL ID=0><A HREF="#DatosSeguro"><%= GetLocalResourceObject("AnchorDatosSeguroCaption") %></A></LABEL><LABEL ID=0>|</LABEL>
    <LABEL ID=0><A HREF="#Rentabilidad"><%= GetLocalResourceObject("AnchorRentabilidadCaption") %></A></LABEL>
    </P>
    <%=mobjValues.ShowWindowsName("VA589", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD>
            <TD COLSPAN="5">
            <%
mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nModulec", mclsActivelife.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("sLevelint",  ,  , True)
Response.Write(mobjValues.PossiblesValues("valAgreement", "tabPlan_agre", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsActivelife.nAgreement), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valAgreementToolTip")))
%>
            </TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Seguro"><%= GetLocalResourceObject("AnchorSeguro2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbeIduraindCaption") %></LABEL></TD>
            <TD>
            <%
mobjValues.TypeList = CShort("1")
mobjValues.BlankPosition = True
mobjValues.List = "0,1,2,5"
Response.Write(mobjValues.PossiblesValues("cbeIduraind", "Table5589", eFunctions.Values.eValuesType.clngComboType, CStr(mclsActivelife.nTypdurins),  ,  ,  ,  ,  ,  , mclsActivelife.bnTypdurinsDisable,  , GetLocalResourceObject("cbeIduraindToolTip")))
%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnInsurtimeCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnInsurtime", 5, CStr(mclsActivelife.nInsurtime),  , GetLocalResourceObject("tcnInsurtimeToolTip"),  ,  ,  ,  ,  ,  , mclsActivelife.bnInsurtimeDisable)%></TD> 
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="DatosSeguro"><%= GetLocalResourceObject("AnchorDatosSeguro2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbenCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbenCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsActivelife.nCurrency),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenCurrencyToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("cbenOptionCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbenOption", "Table5519", eFunctions.Values.eValuesType.clngComboType, CStr(mclsActivelife.nOption),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenOptionToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnCapitaldeathCaption") %></LABEL></TD>
            <TD>
            <%
Response.Write(mobjValues.NumericControl("tcnCapitaldeath", 18, CStr(mclsActivelife.nCapitaldeath),  , GetLocalResourceObject("tcnCapitaldeathToolTip"), True, 6))
Response.Write(mobjValues.HiddenControl("hddnCapitaldeath", CStr(mclsActivelife.nCapitaldeath)))
%>
            </TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnPremdealCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPremdeal", 18, CStr(mclsActivelife.nPremdeal),  , GetLocalResourceObject("tcnPremdealToolTip"), True, 6,  ,  ,  , "insChangeField(this.value)", mclsActivelife.bnPremdealDisable)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbenPayfreqCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbenPayfreq", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mclsActivelife.nPayFreq),  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenPayfreqToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnCalPremCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCalPrem", 18, CStr(mclsActivelife.nPremfreq),  , GetLocalResourceObject("tcnCalPremToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Rentabilidad"><%= GetLocalResourceObject("AnchorRentabilidad2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("cbenTypeinvestCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbenTypeinvest", "Table5520", eFunctions.Values.eValuesType.clngComboType, CStr(mclsActivelife.nTypeinvest),  ,  ,  ,  ,  , "insChangeTypeInvest(this.value);",  ,  , GetLocalResourceObject("cbenTypeinvestToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnIntprojectCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnIntproject", 4, CStr(mclsActivelife.nIntproject),  , GetLocalResourceObject("tcnIntprojectToolTip"), True, 2,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("tcnWarminintCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnWarminint", 4, CStr(mclsActivelife.nWarminint),  , GetLocalResourceObject("tcnWarminintToolTip"), True, 2,  ,  ,  ,  , True)%></TD>
        </TR>
        
    </TABLE>
<%
'+ Objetos ocultos para la transaccion 
Response.Write(mobjValues.HiddenControl("hddnModulec", CStr(mclsActivelife.nModulec)))
Response.Write(mobjValues.HiddenControl("hdddExpirdat", mobjValues.TypeToString(mclsActivelife.dExpirdat, eFunctions.Values.eTypeData.etdDate)))
Response.Write(mobjValues.HiddenControl("hddnRatepayf", CStr(mclsActivelife.nRatepayf)))
Response.Write(mobjValues.HiddenControl("hdddIssuedat", mobjValues.TypeToString(mclsActivelife.dIssuedat, eFunctions.Values.eTypeData.etdDate)))

Response.Write(mobjValues.HiddenControl("hddnPremMin", CStr(mclsActivelife.nPremimin)))
Response.Write(mobjValues.HiddenControl("hddnCapital", CStr(mclsActivelife.nCapital)))
Response.Write(mobjValues.HiddenControl("hddnPrsugest", CStr(mclsActivelife.nPrsugest)))
Response.Write(mobjValues.HiddenControl("hddnVPprsug", CStr(mclsActivelife.nVPprsug)))
Response.Write(mobjValues.HiddenControl("hddStartdate", mobjValues.TypeToString(mclsActivelife.dStartdate, eFunctions.Values.eTypeData.etdDate)))

Response.Write(mobjValues.HiddenControl("hddnPremiumbas", CStr(mclsActivelife.nPremiumbas)))
Response.Write(mobjValues.HiddenControl("hddnPremium", CStr(mclsActivelife.nPremium)))
Response.Write(mobjValues.HiddenControl("hddnVPprdeal", CStr(mclsActivelife.nVPprdeal)))


mobjValues = Nothing
mobjMenu = Nothing
mclsActivelife = Nothing

If Not Session("bquery") Then
	%>
		<SCRIPT>
		    insChangeField(self.document.forms[0].tcnPremdeal.value);
		</SCRIPT>
	<%	
End If
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("VA589")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




