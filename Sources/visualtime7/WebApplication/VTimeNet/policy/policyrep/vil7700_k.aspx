<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vil7700_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vil7700_k"

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    document.VssVersion="$$Revision: 1 $|$$Date: 8/10/03 19:15 $"

//**% insStateZone: This function enable/disable the fields of the page according to the action 
//**% to be performed
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
}

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insChangeDate: Esta funcion cambia las fechas de acuerdo al período
//------------------------------------------------------------------------------------------
function insChangeDate(Value){
//------------------------------------------------------------------------------------------
    nYear = self.document.forms[0].tcnYear.value
    
	with (self.document.forms[0]){	   
		switch (Value){
		    case "1":
		        tcdInitDate.value='01/01/' + nYear;
		        tcdEndDate.value='31/03/' + nYear;
		        break;
		    case "2":
		        tcdInitDate.value='01/04/' + nYear;
		        tcdEndDate.value='30/06/' + nYear;
		        break;
		    case "3":
		        tcdInitDate.value='01/07/' + nYear;
		        tcdEndDate.value='30/09/' + nYear;
		        break;
		    case "4":
		        tcdInitDate.value='01/10/' + nYear;
		        tcdEndDate.value='31/12/' + nYear;
		        break;
		}
	}
}

//% insChangeYear: Esta funcion cambia se ejecuta cuando cambia lel año
//------------------------------------------------------------------------------------------
function insChangeYear(){
//------------------------------------------------------------------------------------------
    
	with (self.document.forms[0]){	   
	    if (tcnYear.value!=''){
		    optMonth[0].checked=true;
		    optMonth[0].disabled=false;
		    optMonth[1].disabled=false;
		    optMonth[2].disabled=false;
		    optMonth[3].disabled=false;
	        insChangeDate('1');
	    }
	    else{
	        tcdInitDate.value=''
		    tcdEndDate.value='';
		    optMonth[0].checked=true;
		    optMonth[0].disabled=true;
		    optMonth[1].disabled=true;
		    optMonth[2].disabled=true;
		    optMonth[3].disabled=true;
	    }
	}
}

//**% FindShowCertifShowCertif: This function enabled or disabled the field nCertif. 
//% FindShowCertifShowCertif: Esta función habilita o inhabilita el campo nCertif.
//-----------------------------------------------------------------------------
function FindShowCertif(){
//-----------------------------------------------------------------------------
	ShowPopUp("/VTimeNet/Policy/PolicyRep/ShowDefValues.aspx?Field=Switch_Curr_Pol" + "&nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value,"ShowDefValuesCollectionTra", 1, 1,"no","no",2000,2000)
}

//% EnableFields: Habilita / Deshabilita los campos PÓLIZA y CERTIFICADO 
//-------------------------------------------------------------------------------------------------
function EnableFields(bChecked)
//-------------------------------------------------------------------------------------------------
{
	if(bChecked)
	{
//+ Si está encendido el checkbutton, se procesa de forma masiva, de lo contrario se
//+ debe especificar el número de la póliza y el certificado 
		self.document.forms[0].elements['tcnPolicy'].disabled = true;
		self.document.forms[0].elements['tcnCertif'].disabled = true;
		self.document.forms[0].elements['hddMassive'].value   = 1;
	}
	else
	{
		self.document.forms[0].elements['tcnPolicy'].disabled = false;
		self.document.forms[0].elements['tcnCertif'].disabled = false;
		self.document.forms[0].elements['hddMassive'].value   = 2;
	}
}

//% insInitials:se ejecuta al entrar en la transacción
//-----------------------------------------------------------------------------
function insInitials(){
//-----------------------------------------------------------------------------
lDate = '<% %>
<%=Today%>'
	with (self.document.forms[0]){	   
	    tcnYear.value = lDate.substr(6,4);
	    insChangeDate('1');
	}	
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    
 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VIL7700", "VIL7700_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With

mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="VIL7700" ACTION="valPolicyRep.aspx?x=1">
	<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
<BR>
<BR>
    <TABLE WIDTH="80%" ALIGN="CENTER">
		<TR>
            <TD><LABEL ID=13658><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD> <%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value",  , 3)%></TD>
            <%
With mobjValues.Parameters
	.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
            <TD WIDTH="15%"></TD>
            <TD><LABEL ID=13664><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD> <%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0), eFunctions.Values.eValuesType.clngWindowType, False, vbNullString,  ,  ,  ,  , 4)%></TD>            
        </TR>

        <TR>
            <TD><LABEL ID=13663><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "FindShowCertif()")%></TD>
            <TD></TD>
            <TD><LABEL ID=13660><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 8, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  , 0)%></TD>
        </TR>    
        <TR>        
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
		    <TD><%=mobjValues.NumericControl("tcnYear", 4,  ,  , GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  , "insChangeYear();")%></TD>        
		    <TD></TD>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnNumCartCaption") %></LABEL></TD>
		    <TD><%=mobjValues.PossiblesValues("tcnNumCart", "TabCartol", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnNumCartToolTip"))%></TD>
		</TR>
        <TR>        
            <TD COLSPAN=2 CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD></TD>
            <TD COLSPAN=2 CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>        
            <TD COLSPAN=2 CLASS="HorLine"></TD>
            <TD></TD>
            <TD COLSPAN=2 CLASS="HorLine"></TD>
        </TR>
        <TR>            
		    <TD colspan="2">
		        <TABLE WIDTH="100%"><TR>

		                    <TD><%=mobjValues.OptionControl(0, "optMonth", GetLocalResourceObject("optMonth_1Caption"), CStr(1), "1", "insChangeDate(this.value)")%></TD>
		                    <TD><%=mobjValues.OptionControl(3, "optMonth", GetLocalResourceObject("optMonth_2Caption"),  , "2", "insChangeDate(this.value)")%></TD>
		                </TR>
		        </TABLE>
		    </TD>
		    <TD></TD>
            <TD><LABEL ID=13644><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdInitDate",  ,  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
        <TR>
            <TD colspan="2">
		        <TABLE WIDTH="100%"><TR>
		                    <TD WIDTH="50%"><%=mobjValues.OptionControl(3, "optMonth", GetLocalResourceObject("optMonth_3Caption"),  , "3", "insChangeDate(this.value)")%></TD>
		                    <TD><%=mobjValues.OptionControl(3, "optMonth", GetLocalResourceObject("optMonth_4Caption"),  , "4", "insChangeDate(this.value)")%></TD>
		                </TR>
		        </TABLE>
            </TD>
            <TD></TD>
            <TD><LABEL ID=13644><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True)%></TD>
       </TR>
		<%Response.Write(mobjValues.HiddenControl("hddMassive", CStr(1)))%>
    </TABLE>

<%

With Response
	.Write("<SCRIPT>")
	.Write("insInitials();")
	'.Write "var nYear = '" & Date & "';"
	'		  .Write "FindShowCertif();"
	.Write("</SCRIPT>")
End With
%>

</FORM>
</BODY>
<%
mobjValues = Nothing%> 
</HTML>
<%
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
Call mobjNetFrameWork.FinishPage("vil7700_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>





