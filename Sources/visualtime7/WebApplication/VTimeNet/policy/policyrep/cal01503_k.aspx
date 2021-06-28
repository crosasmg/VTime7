<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim sCodispl As String
Dim sCodisplPage As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrPolicy As String


</script>
<%
sCodispl = Trim(Request.QueryString.Item("sCodispl"))
sCodisplPage = LCase(sCodispl) & "_k"

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(sCodisplPage)
mstrPolicy = ""
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = sCodisplPage
'~End Body Block VisualTimer Utility

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}


//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        cbeOfficeAgen.value="";
        cbeAgency.value="";
        cbeOfficeAgen_nBran_off.value = "";
        cbeAgency_nBran_off.value = "";
        cbeAgency_nOfficeAgen.value = "";
        cbeAgency_sDesAgen.value = "";
    }
    UpdateDiv('cbeOfficeAgenDesc','');
    UpdateDiv('cbeAgencyDesc','');
}


//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
//+ Cambia la sucursal 
     /*   if (nInd == 1){
            cbeOfficeAgen.value = '';
            UpdateDiv('cbeOfficeAgenDesc','');
            cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
            cbeOfficeAgen.Parameters.Param2.sValue = '0';
            cbeAgency.value = '';
            UpdateDiv('cbeAgencyDesc','');
            cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
            cbeAgency.Parameters.Param2.sValue = '0';
        }
//+ Cambia la oficina 
        else{*/
            if (nInd == 2){
                if(cbeOfficeAgen.value != ''){
                   /* cbeOffice.value = cbeOfficeAgen_nBran_off.value;*/
                    cbeAgency.value = '';
                    UpdateDiv('cbeAgencyDesc','');
                    /*cbeAgency.Parameters.Param1.sValue = cbeOffice.value;*/
                    cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
                }
                else{
                    cbeAgency.Parameters.Param2.sValue = '0';
                }
            }
//+ Cambia la Agencia
            else{
                if (nInd == 3){
                    if(cbeAgency.value != ''){
                        /*cbeOffice.value = cbeAgency_nBran_off.value;*/
                        if (cbeOfficeAgen.value == ''){
                            cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                            UpdateDiv('cbeOfficeAgenDesc',cbeAgency_sDesAgen.value);
                        }
                        /*cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;*/
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?'0':cbeOfficeAgen.value);
                    }
                }
            }
       /* }*/
    }
}








//%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
//%valor numerico al campo
//------------------------------------------------------------------------------------------
function insDefValue(Field){
//------------------------------------------------------------------------------------------
    if(Field.value=='')
        self.document.forms[0].tcnExcess.value='0'
}

</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="Policy" action="valpolicyrep.aspx?mode=1">
    <BR></BR>
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td align="left"><H2 class="WindowsName">Recepción de Pólizas<HR></H2></td>
		</tr>
	</table>
    <BR></BR>
	<table width="60%">
		
	    <tr>
			<td class="HighLighted" align="left" colspan="2">Opcionales:</td>
		</tr>
		<tr>
			<td colspan="2" class="HorLine" width="100%" align="left"></td>
		</tr>
        
        <tr>
	      <td style="height: 20px"><label><%= GetLocalResourceObject("cbeBranchCaption") %></label></td>
	      <td><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"))%></td>
		   
		</tr>
		<tr>
		    <td><label><%= GetLocalResourceObject("valProductCaption") %></label></td>
	      <td><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(1), eFunctions.Values.eValuesType.clngWindowType)%></td>
		</tr>
	    <!--<tr>
	      <td><label><%= GetLocalResourceObject("cbeOfficeCaption") %></label></td>
	      <td><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)", False,  , GetLocalResourceObject("cbeOfficeToolTip"))%></td>
		</tr>-->
	    <tr>
	        <td><label><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></label></td>
		        <td><%
mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nBran_off",  ,  , True)
Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)", False,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
%>
		        </td>
			
		</tr>
	        <td><label><%= GetLocalResourceObject("cbeAgencyCaption") %></label></td>
		     <td><%
mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nBran_off",  ,  , True)
mobjValues.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
mobjValues.Parameters.ReturnValue("sDesAgen",  ,  , True)
Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(3)", False,  , GetLocalResourceObject("cbeAgencyToolTip")))
%>
		    </td>
	    
        <tr>
			<td class="HighLighted" align="left" colspan="4">Periódo a consultar:</td>
		</tr>
		<tr>
			<td colspan="4" class="HorLine" width="100%" align="left"></td>
		</tr>
        <tr>
          <td style="height: 20px"><LABEL><%= GetLocalResourceObject("tcdIniDateCaption") %> </LABEL></td>
		  <td><%=mobjValues.DateControl("tcdIniDate",  ,  , GetLocalResourceObject("tcdIniDateToolTip"))%></td>
          <td style="height: 20px"><LABEL><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></td>
            <td><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"))%></td>  
        </tr>
        
	    <tr>
	      
	      
		   <TR>
			    
			<td><img height="20" src="/VTimeNet/images/blank.gif"/></td>
		</tr>
		<tr>
			<td COLSPAN="6">&nbsp</td>	        
		</tr>
	</table>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




