<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca888_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca888_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT> 
//% insStateZone: se controla el estado de los controles de la página
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}
//% insCancel: se controla la acción Cancelar de la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
	return true;
}
//% InsShowCertificat: Habilita o deshabilita el campo del número del certificado 
//-----------------------------------------------------------------------------
function InsShowCertificat(Value){
//-----------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (cbeBranch.value != "" && valProduct.value != "" && Value != ""){
			insDefValues("insValsPolitype","nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + Value + "&sCodispl=" + '<%=Request.QueryString.Item("sCodispl")%>' + "&sCertype=" + self.document.forms[0].tctCertype.value + "&sFrame=");
		}
		else{
			tcnCertif.disabled = false
			tcnCertif.value = ""
		}
	}
}

//% InsShowPolicyDATA: Carga los datos para la póliza/cotización/propuesta
//-----------------------------------------------------------------------------
function InsShowPolicyDATA(Value) {
    //-----------------------------------------------------------------------------
    with (self.document.forms[0]) {
        if (tcnPolicy.value != "") {
            insDefValues("Policy_CA888", "nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + Value + "&sCodispl=" + '<%=Request.QueryString.Item("sCodispl")%>' + "&sCertype=" + self.document.forms[0].tctCertype.value + "&sFrame=");
        }
    }
}

//% InsShowUser: Carga el código de usuario que estaba emitiendo o modificando la póliza/cotización/propuesta
//-----------------------------------------------------------------------------
function InsShowUser(Value){
//-----------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (cbeBranch.value != "" && valProduct.value != "" && tcnPolicy.value != "" && Value != ""){
			insDefValues("ShowUser","nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value + "&nCertif=" + Value + "&sCodispl=" + '<%=Request.QueryString.Item("sCodispl")%>' + "&sCertype=" + self.document.forms[0].tctCertype.value + "&sFrame=");
		}
	}
}

//% insCheck: controla el estado de los campos de la página cuando se cambia el indicador 
//%           de cotizacion / propuesta
//------------------------------------------------------------------------------------------
function insCheck(Field){
//------------------------------------------------------------------------------------------      
	with(self.document.forms[0]){
//+ Si es cotizacion, se sacan las opciones de Anulacion, Rehabilitacion, 
//+ Saldado, Prorrogado, Rescate y Prestamo
	    if(optTypeDoc[0].checked){
	        valOrigin.disabled = true;	  
	        btnvalOrigin.disabled = true;  
            valOrigin.value    = "";
	        UpdateDiv('valOriginDesc',"");
	        self.document.forms[0].tctCertype.value = "2";	
	        self.document.forms[0].cbeBranch.disabled = false;
			self.document.forms[0].valProduct.disabled = false;
			self.document.forms[0].btnvalProduct.disabled = false;
			self.document.forms[0].tcnPolicy.disabled = false;
			self.document.forms[0].tcnCertif.disabled = false;
			self.document.forms[0].tcnPolicy.value = "";
			self.document.forms[0].tcnCertif.value = "";
			self.document.forms[0].cbeBranch.value = "";
			self.document.forms[0].valProduct.value = "";
			self.document.forms[0].valUsers.value = "";
			$(valUsers).change();
            UpdateDiv('valUsersDesc', "");

	    }
	    else{
	        valOrigin.disabled = false;
	        btnvalOrigin.disabled = false;
	        valOrigin.List     = "4,5,6,7,8,9" 
	        valOrigin.TypeList = 2;
            valOrigin.value    = 1;
            $(valOrigin).change();
            self.document.forms[0].tcnPolicy.value = "";
            self.document.forms[0].tcnCertif.value = "";
            self.document.forms[0].cbeBranch.value = "";
            self.document.forms[0].valProduct.value = "";
            self.document.forms[0].valUsers.value = "";
            $(valUsers).change();
            UpdateDiv('valUsersDesc', "");
        }	
        if(optTypeDoc[0].checked)
            tctCertype.value = "2";
        if (optTypeDoc[1].checked)
            tctCertype.value = "1";
        if (optTypeDoc[2].checked)
            tctCertype.value = "3";	

	}
}

//% ChangeValues: se maneja la habilitacion de los controles de la página
//------------------------------------------------------------------------------------------
function ChangeValues(Field){
//------------------------------------------------------------------------------------------

	if (Field.value!=''){
	
		self.document.forms[0].cbeBranch.disabled = false;
		self.document.forms[0].valProduct.disabled = false;
	    self.document.forms[0].btnvalProduct.disabled = false;
	    self.document.forms[0].tcnPolicy.disabled = false;
	    self.document.forms[0].tcnCertif.disabled = false;
		
		switch (Field.value)
		{
			case "1":
		        if (self.document.forms[0].optTypeDoc[1].checked)
		            self.document.forms[0].tctCertype.value = "1";
		        else
		            self.document.forms[0].tctCertype.value = "3";
				break;
			case "2":
		        if (self.document.forms[0].optTypeDoc[1].checked)
		            self.document.forms[0].tctCertype.value = "6";
		        else
		            self.document.forms[0].tctCertype.value = "4";
				break;
			case "3":
		        if (self.document.forms[0].optTypeDoc[1].checked)
		            self.document.forms[0].tctCertype.value = "7";
		        else
		            self.document.forms[0].tctCertype.value = "5";
		        break;
		}
	}
	else{
		if (!self.document.forms[0].optTypeDoc[0].checked){		
			self.document.forms[0].cbeBranch.value = 0;
			self.document.forms[0].cbeBranch.disabled = true;
			
			self.document.forms[0].valProduct.Param1 = 0;
			self.document.forms[0].valProduct.value = 0;
			$(self.document.forms[0].valProduct).change();
			self.document.forms[0].valProduct.disabled = true;
	        self.document.forms[0].btnvalProduct.disabled = true;
	        
	        self.document.forms[0].tcnPolicy.value = 0;
	        self.document.forms[0].tcnPolicy.disabled = true;
	        
	        self.document.forms[0].tcnCertif.value = 0;
	        self.document.forms[0].tcnCertif.disabled = true;
		}
	}
	
}


</SCRIPT>
	 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA888", "CA888_k.aspx", 2, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" NAME="CA888" ACTION="ValPolicyTra.aspx?x=1">
	<BR><BR>
	<%=mobjValues.ShowWindowsName("CA888", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="3"></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optTypeDoc", GetLocalResourceObject("optTypeDoc_1Caption"), CStr(1), "1", "insCheck(this)", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valOriginCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valOrigin", "Table5580", eFunctions.Values.eValuesType.clngWindowType, , , , , , , "ChangeValues(this)", True, , GetLocalResourceObject("valOriginToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optTypeDoc", GetLocalResourceObject("optTypeDoc_2Caption"),  , "2", "insCheck(this)", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(eRemoteDB.Constants.intNull), "valProduct",  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value")%></TD>
        </TR>
        <TR>
        
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optTypeDoc", GetLocalResourceObject("optTypeDoc_3Caption"),  , "3", "insCheck(this)", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eValuesType.clngWindowType, , CStr(eRemoteDB.Constants.intNull)))
                %>
            </TD>
        </TR>
        <TR>            
			<TD COLSPAN="3">&nbsp;</TD>	
            <TD><LABEL><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, "", , GetLocalResourceObject("tcnPolicyToolTip"), , 0, , , , "InsShowPolicyDATA(this.value);")%></TD>
        </TR>			
        <TR>            
			<TD COLSPAN="3">&nbsp;</TD>	
            <TD><LABEL><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  , "InsShowUser(this.value)")%></TD>
        </TR>			
        <TR>            
			<TD COLSPAN="3">&nbsp;</TD>	
            <TD><LABEL><%= GetLocalResourceObject("valUsersCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valUsers", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valUsersToolTip"))%></TD>
        </TR>
        <%Response.Write(mobjValues.HiddenControl("tctCertype", "2"))%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca888_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




