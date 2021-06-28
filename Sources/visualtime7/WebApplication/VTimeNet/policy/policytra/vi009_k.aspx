<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjSecurity As eSecurity.SecurScheSurr

'-Variables para el manejo de los valores iniciales de la transacción
Dim lclsPolicy As ePolicy.ValPolicyTra
Dim mblnDisabled As Boolean
Dim mstrCodisplOri As String
Dim mstrAgency As String
Dim sTotal As String
Dim sPartial As String
Dim sPreliminary As String
Dim sDefinitive As String
Dim lclsUsers As eGeneral.Users
Dim mstrOffice As Object
Dim mstrOfficeagen As String
</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi009_k")
lclsUsers = New eGeneral.Users
lclsUsers.Find(Session("nUsercode"))
    
If IsNothing(Request.QueryString.Item("nOffice")) Then
    mstrOffice = lclsUsers.nOffice
Else
	mstrOffice = mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble)
End If   
    
If IsNothing(Request.QueryString.Item("nOfficeagen")) Then
    mstrOfficeagen = lclsUsers.nOfficeagen
Else
	mstrOfficeagen = mobjValues.StringToType(Request.QueryString.Item("nOfficeagen"), eFunctions.Values.eTypeData.etdDouble)
End If    
    
If IsNothing(Request.QueryString.Item("nAgency")) Then
    mstrAgency = lclsUsers.nAgency
Else
	mstrAgency = mobjValues.StringToType(Request.QueryString.Item("nAgency"), eFunctions.Values.eTypeData.etdDouble)
End If
    

mobjSecurity = New eSecurity.SecurScheSurr
mobjValues = New eFunctions.Values
mobjSecurity = New eSecurity.SecurScheSurr


Call mobjSecurity.Find(Session("sSche_Code"), False)

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi009_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
lclsPolicy = New ePolicy.ValPolicyTra
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mstrCodisplOri = Request.QueryString.Item("sCodisplOri")
Call lclsPolicy.InsPreVI009_K(Request.QueryString.Item("sTyp_surr"), mstrCodisplOri, mobjValues.StringToType(Request.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble, True))


If lclsPolicy.DefaultValueVI009("sTyp_surr") = "1" And mobjSecurity.sRescTotV = "1" Then
	sTotal = "1"
ElseIf mobjSecurity.sRescParV = "1" Then 
	sPartial = "1"
End If

If lclsPolicy.DefaultValueVI009("sProcessType") = "1" And mobjSecurity.bAllowsPreliminaryExecutionsV Then
	sPreliminary = "1"
ElseIf mobjSecurity.bAllowsDefinitiveExecutionsV Then 
	sDefinitive = "1"
End If



%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"

//%insCancel: Función que maneja la acción cancelar de la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    if (top.frames['fraSequence'].pintZone == 1)
        <%=lclsPolicy.DefaultValueVI009("sScriptCancel")%>
    else
        return true;
}

//-------------------------------------------------------------------------------------------
function insPopulatePFields(nOffice, nOfficeAgen, nAgency, sOfficeAgenDesc, sAgencyDesc){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		cbeOfficeAgen.value = nOfficeAgen;
		UpdateDiv('cbeOfficeAgenDesc',sOfficeAgenDesc);
		cbeOfficeAgen.Parameters.Param1.sValue = nOffice;
		cbeOfficeAgen.Parameters.Param2.sValue = '0';
		cbeAgency.value = nAgency;
		UpdateDiv('cbeAgencyDesc',sAgencyDesc);
		cbeAgency.Parameters.Param1.sValue = nOffice;
		cbeAgency.Parameters.Param2.sValue = '0';
    }	
}


//-------------------------------------------------------------------------------------------
function insBlankPFields(nOffice){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		cbeOfficeAgen.value = '';
		UpdateDiv('cbeOfficeAgenDesc','');
		cbeOfficeAgen.Parameters.Param1.sValue = nOffice;
		cbeOfficeAgen.Parameters.Param2.sValue = '0';
		cbeAgency.value = '';
		UpdateDiv('cbeAgencyDesc','');
		cbeAgency.Parameters.Param1.sValue = nOffice;
		cbeAgency.Parameters.Param2.sValue = '0';
    }
}


//%insChangePolicy : Valida si la póliza es individual para deshabilitar el certificado
//------------------------------------------------------------------------------------------
function insChangePolicy(Form, sCodispl, sFrame){
//------------------------------------------------------------------------------------------
    if (typeof(sCodispl) == 'undefined' ) sCodispl = '';
    if (typeof(sFrame) == 'undefined' ) sFrame = 'fraHeader';
    with (Form){
        if (tcnPolicy.value != ''){
            insDefValues('Policy_CA099', 'nBranch=' + cbeBranch.value +
                                         '&nProduct=' + valProduct.value +
                                         '&nPolicy=' + tcnPolicy.value +
                                         '&sCodispl=' + sCodispl +
                                         '&sFrame=' + sFrame);
        }
    }
} 

//%insSurrDate : Obtiene la fecha de vigencia del rescate.
//------------------------------------------------------------------------------------------
function insSurrDate(){
//------------------------------------------------------------------------------------------
    
    with(self.document.forms[0]){
        insDefValues('SurrenValue');
        
    }
}

    //% insInitialAgency: manejo de sucursal/oficina/agencia
    //-------------------------------------------------------------------------------------------
    function insInitialAgency(nInd) {
        //-------------------------------------------------------------------------------------------    
        with (self.document.forms[0]) {
            //+ Cambia la sucursal 
            if (nInd == 1) {
                if (typeof (cbeOffice) != 'undefined') {
                    if (cbeOffice.value != 0) {
                        if (typeof (cbeOfficeAgen) != 'undefined') {
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeOfficeAgen.Parameters.Param2.sValue = 0;
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0)
                                cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                            else
                                cbeAgency.Parameters.Param2.sValue = 0;
                        }
                    }
                    else {
                        if (typeof (cbeOfficeAgen) != 'undefined') {
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeOfficeAgen.Parameters.Param2.sValue = 0;
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0) {
                                cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                            }
                            else {
                                cbeAgency.Parameters.Param2.sValue = 0;
                            }
                        }
                    }
                }
            }
            //+ Cambia la oficina 
            else {
                if (nInd == 2) {
                    if (cbeOfficeAgen.value != '') {
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                        cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                    }
                    else {
                        cbeAgency.Parameters.Param1.sValue = 0;
                        cbeAgency.Parameters.Param2.sValue = 0;
                    }
                }
                //+ Cambia la Agencia
                else {
                    if (nInd == 3) {
                        if (cbeAgency.value != "") {
                            cbeOffice.value = cbeAgency_nBran_off.value;
                            if (cbeOfficeAgen.value == '') {
                                cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                                UpdateDiv('cbeOfficeAgenDesc', cbeAgency_sDesAgen.value);
                            }
                            cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                            cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                        }
                    }
                }
            }
        }
    }

//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgencyOld(nInd) {
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
//+ Cambia la sucursal 
		switch(nInd){
		case 1:
		    if (typeof(cbeOffice)!='undefined'){
		        if (cbeOffice.value != 0){
	  				if (typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
							cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
						else
							cbeAgency.Parameters.Param2.sValue = 0;
					}
			    }
				else{
	  				if(typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
							cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
						else
							cbeAgency.Parameters.Param2.sValue = 0;
					}
				}
			}
		    break;

//+ Cambia la oficina
		case 2:
			if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
			    {
                cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
			    cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
			    cbeOffice.value = cbeOfficeAgen_nBran_off.value;
			    cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
			    }
			else{
			    cbeAgency.Parameters.Param1.sValue = 0;
			    cbeAgency.Parameters.Param2.sValue = 0;
			    }
			break;
//+ Cambia la Agencia			
	    case 3:
	        if(cbeAgency.value != ""){
                cbeOffice.value = cbeAgency_nBran_off.value;
                if (cbeOfficeAgen.value == ''){
                    cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                    UpdateDiv('cbeOfficeAgenDesc',cbeAgency_sDesAgen.value);
                }
                cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
            }
	    }	
	}
}

//% InsCheckNullPrem: Habilita o no el Check de anular recibos pendientes
//-------------------------------------------------------------------------------------------
function InsCheckNullPrem(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (optSurrType[0].checked &&
            optProcessType[1].checked){
                chkNullPrem.checked = false;
                chkNullPrem.disabled = ('<%=mobjSecurity.sAnulRec%>'=='3');
        }
        else{
            chkNullPrem.disabled = true;
            chkNullPrem.checked = false;
                
        }
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.MakeMenu("VI009", "VI009_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" ID="FORM" NAME="VI009" ACTION="valPolicyTra.aspx?x=1">
<%
Response.Write(mobjValues.HiddenControl("hddsCodisplOri", mstrCodisplOri))
Response.Write(mobjValues.HiddenControl("hddnOperat", Request.QueryString.Item("nOperat")))
mblnDisabled = mstrCodisplOri = "CA767"
%>
<TABLE WIDTH="100%">
    <TR>
        <TD WIDTH="20%"><LABEL ID=13848><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
        <TD WIDTH="30%"><%
mobjValues.BlankPosition = False
'mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
'mobjValues.Parameters.Add("sLife", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
'Response.Write(mobjValues.PossiblesValues("cbeBranch", "TabScheSurrBranch", 1, vbNullString, True,  ,  ,  ,  , "insChangePolicy(self.document.forms[0], ""VI009_K"", ""fraHeader"");", CBool(mblnDisabled),  , GetLocalResourceObject("cbeBranchToolTip")))

Response.write (mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),Request.QueryString("nBranch"),,,,, "insChangePolicy(self.document.forms[0], ""VI009_K"", ""fraHeader"");", mblnDisabled))%></TD>
        <TD>&nbsp;</TD>
        <TD WIDTH="20%"><LABEL ID=13852><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
        <TD WIDTH="30%"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"),  , mblnDisabled, Request.QueryString.Item("nProduct"),  ,  ,  , "insChangePolicy(self.document.forms[0], ""VI009_K"", ""fraHeader"");")%></TD>
    </TR>
    <TR>
        <TD><LABEL ID=13851><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insChangePolicy(self.document.forms[0], ""VI009_K"", ""fraHeader"");", mblnDisabled)%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL ID=13849><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "insSurrDate();", mblnDisabled)%></TD>
    </TR>
    <TR>
        <TD><LABEL ID=13837><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
        <TD><%=mobjValues.DateControl("tcdEffecdate", Request.QueryString.Item("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , mobjSecurity.SModDateRV <> "1")%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL ID=13850><%= GetLocalResourceObject("cbeSurrPayWayCaption") %></LABEL></TD>
        <TD><%mobjValues.BlankPosition = False
mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sLife", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeSurrPayWay", "TABSCHESURRPAYMENT", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTypePay"), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSurrPayWayToolTip")))

'mobjvalues.PossiblesValues("cbeSurrPayWay", "Table5527", eFunctions.Values.eValuesType.clngComboType, Request.QueryString("nTypePay"),,,,,,,,, GetLocalResourceObject("cbeSurrPayWayToolTip"))%></TD>
    </TR>
    <TR>
        <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
        <TD>
        <%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", 1,  ,  ,  ,  ,  ,  , "insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip")))
%>
        </TD>
        <TD>&nbsp;</TD>
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
        <TD>
        <%
With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, vbNullString, True,  ,  ,  ,  , "insInitialAgency(2)", ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
End With
%>
        </TD>
    </TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
        <TD>
        <%
With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, mstrAgency, True,  ,  ,  ,  , "insInitialAgency(3)", ,  , GetLocalResourceObject("cbeAgencyToolTip")))
End With
%>
        </TD>
        <TD>&nbsp;</TD>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
        <TD><%=mobjValues.NumericControl("tcnProponum", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnProponumToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>
    </TR>
</TABLE>
<TABLE WIDTH="100%">
    <TR>
        <TD>&nbsp;</TD>
        <TD CLASS="HighLighted"><LABEL ID=41180><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
        <TD>&nbsp;</TD>
        <TD CLASS="HighLighted"><LABEL ID=41180><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
    </TR>
    <TR>
        <TD></TD>
        <TD CLASS="Horline"></TD>
        <TD></TD>
        <TD CLASS="Horline"></TD>
    </TR>
    <TR>
    
        
        <TD><%=mobjValues.CheckControl("chkNullPrem", GetLocalResourceObject("chkNullPremCaption"), mobjSecurity.sAnulRec, "1",  , mobjSecurity.sAnulRec = "1" Or mobjSecurity.sAnulRec = "2",  , GetLocalResourceObject("chkNullPremToolTip"))%></TD>
        <TD><%=mobjValues.OptionControl(41185, "optSurrType", GetLocalResourceObject("optSurrType_1Caption"), sTotal, "1", "InsCheckNullPrem()", mobjSecurity.sRescTotV <> "1",  , GetLocalResourceObject("optSurrType_1ToolTip"))%></TD>
        <TD>&nbsp;</TD>
        <TD><%=mobjValues.OptionControl(41185, "optProcessType", GetLocalResourceObject("optProcessType_1Caption"), sPreliminary, "1", "InsCheckNullPrem()", CBool(mblnDisabled) Or Not mobjSecurity.bAllowsPreliminaryExecutionsV,  , GetLocalResourceObject("optProcessType_1ToolTip"))%></TD>
    </TR>
    <TR>
        <TD>&nbsp;</TD>
        <TD><%=mobjValues.OptionControl(41185, "optSurrType", GetLocalResourceObject("optSurrType_2Caption"), sPartial, "2", "InsCheckNullPrem()", mobjSecurity.sRescParV <> "1",  , GetLocalResourceObject("optSurrType_2ToolTip"))%></TD>
        <TD>&nbsp;</TD>
        <TD><%=mobjValues.OptionControl(41185, "optProcessType", GetLocalResourceObject("optProcessType_2Caption"), sDefinitive, "2", "InsCheckNullPrem()", CBool(mblnDisabled) Or Not mobjSecurity.bAllowsDefinitiveExecutionsV,  , GetLocalResourceObject("optProcessType_2ToolTip"))%></TD>
    </TR>
</TABLE>
<%
Response.Write(mobjValues.HiddenControl("hddCertype", "2"))
mobjValues = Nothing
lclsPolicy = Nothing
%>
</FORM>
<%
Response.Write("<SCRIPT>")



Response.Write("self.document.forms[0].cbeOffice.value='0';")

Response.Write("insInitialAgency(3);")
Response.Write("setTimeout('$(self.document.forms[0].cbeAgency).change()', 1);")
Response.Write("InsCheckNullPrem();")
Response.Write("</SCRIPT>")
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi009_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




