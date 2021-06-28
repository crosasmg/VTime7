<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de los datos del cliente	
Dim mobjClient As eClient.Client

'-Variables para manejar el option de fumador
Dim loptNoInfo As String '3
Dim loptSmoker As String '1
Dim loptNoSmoker As String '2
Dim UsPerson As String
Dim NoUsPerson As String


</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mobjClient = New eClient.Client

    If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
        mobjValues.ActionQuery = True
    End If

    Session("dInpdate") = ""
    With mobjClient
        If .insPreBC001(Session("sClient")) Then
            Session("chkPEP") = mobjClient.sPEP
            Session("chkCRS") = mobjClient.sCRS
            Session("chkUSPERSON") = mobjClient.sUsPerson
            Response.Write("<SCRIPT>top.fraHeader.document.forms[0].tctClient.value='" & mobjClient.sClient & "';top.fraHeader.UpdateDiv('tctCliename', '" & Replace(mobjClient.sCliename, "'", "´") & "');" & "top.fraHeader.document.forms[0].cbePerson_typ.value='" & mobjClient.nPerson_typ & "';</SCRIPT>")
            If .dInpdate <> eRemoteDB.Constants.dtmNull Then
                Session("dInpdate") = .dInpdate
            Else
                Session("dInpdate") = Today
            End If
            If .nNationality <> eRemoteDB.Constants.intNull Then
                Session("nNationality") = .nNationality
            Else
                Session("nNationality") = "1"
            End If
        End If
        loptNoInfo = "2"
        loptSmoker = "2"
        loptNoSmoker = "2"
        UsPerson = "0"
        NoUsPerson = "1"

        If .sUsPerson = "1" Then
            UsPerson = "1"
            NoUsPerson = "2"
        ElseIf .sUsPerson = "2" Then
            NoUsPerson = "1"
            UsPerson = "2"
        Else
            NoUsPerson = "1"
            UsPerson = "2"
        End If

        If .sSmoking = "1" Then
            loptSmoker = "1"
        ElseIf .sSmoking = "2" Then
            loptNoSmoker = "1"
        Else
            loptNoInfo = "1"
        End If

    End With
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 4 $|$$Date: 20/01/04 11:44 $"
		
//% CancelErrors: se controla la acción Cancelar 
//---------------------------------------------------------------------------------------------------
	function CancelErrors(){
//---------------------------------------------------------------------------------------------------
	self.history.back
}
	
//% insEnabledFields: Habilita o deshabilita los campos de la ventana, dependiendo si están
//%					  llenos o no. ACM - 31/07/2001.	
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
//+ Fecha de Ingreso
	    if (elements["tcdInpDate"].value == "")
	        elements["tcdInpDate"].disabled = false;
	    else
	        elements["tcdInpDate"].disabled = true;

//+ Apellido Paterno
	    if (elements["tctLastName"].value == "")
	        elements["tctLastName"].disabled = false;
	    else
	        elements["tctLastName"].disabled = true;

//+ Apellido Materno
	    if (elements["tctLastName2"].value == "")
	        elements["tctLastName2"].disabled = false;
	    else
	        elements["tctLastName2"].disabled = true;
		
//+ Nombres
	    if (elements["tctFirstName"].value == "")
	        elements["tctFirstName"].disabled = false;
	    else
	        elements["tctFirstName"].disabled = true;
			
//+ Fecha de Nacimiento
	    if (elements["tcdBirthDate"].value == "")
	        elements["tcdBirthDate"].disabled = false;
	    else
	        elements["tcdBirthDate"].disabled = true;		

//+ Estado Civil
	    if (elements["cbeCivilsta"].value == "")
	        elements["cbeCivilsta"].disabled = false;
	    else
	        elements["cbeCivilsta"].disabled = true;
			
//+ Sexo
	    if (elements["cbeSex"].value == "")
	        elements["cbeSex"].disabled = false;
	    else
	        elements["cbeSex"].disabled = true;

//+ Nacionalidad
	    if (elements["cbeNationality"].value == "")
	        elements["cbeNationality"].disabled = false;
	    else
	        elements["cbeNationality"].disabled = true;
			
//+ Actividad Laboral
	    if (elements["cbeOccupat"].value == "")
	        elements["cbeOccupat"].disabled = false;
	    else
	        elements["cbeOccupat"].disabled = true;
			
//+ Rubro Económico
	    if (elements["cbeArea"].value == "")
	        elements["cbeArea"].disabled = false;
	    else
	        elements["cbeArea"].disabled = true;		

//+ Profesión
	    if (elements["cbeTitle"].value == "")
	        elements["cbeTitle"].disabled = false;
	    else
	        elements["cbeTitle"].disabled = true;
//+ PEP
	    if (elements["chkPEP"].value == 1)
	        elements["chkPEP"].disabled = false;
	    else
	        elements["chkPEP"].disabled = true;

//+ USPERSON
	    if (elements["chkUSPERSON"].value == 1)
	        elements["chkUSPERSON"].disabled = false;
	    else
	        elements["chkUSPERSON"].disabled = true;

 //+ Fecha de Otorgamiento
	    if (elements["tcdDriverDat"].value == "")
	        elements["tcdDriverDat"].disabled = false;
	    else
	        elements["tcdDriverDat"].disabled = true;
			
//+ Número de la Licencia
	    if (elements["tctDriverNum"].value == "")
	        elements["tctDriverNum"].disabled = false;
	    else
	        elements["tctDriverNum"].disabled = true;
					
//+ Fecha de Término
	    if (elements["tcdDrivExpDat"].value == "")
	        elements["tcdDrivExpDat"].disabled = false;
	    else
	        elements["tcdDrivExpDat"].disabled = true;
			
//+ Clase
	    if (elements["cbeTypDriver"].value == "")
	        elements["cbeTypDriver"].disabled = false;
	    else
	        elements["cbeTypDriver"].disabled = true;
			
//+ Restricciones
	    if (elements["cbeLimitDriv"].value == "")
	        elements["cbeLimitDriv"].disabled = false;
	    else
	        elements["cbeLimitDriv"].disabled = true;		
					
//+ Bloqueado
	    if (elements["chkBlockade"].value == 1)
	        elements["chkBlockade"].disabled = false;
	    else
	        elements["chkBlockade"].disabled = true;

//+ Dependiente
	    if (elements["chkDependant"].value == 1)
	        elements["chkDependant"].disabled = false;
	    else
	        elements["chkDependant"].disabled = true;

//+ Fecha de Defunción
	    if (elements["tcdDeathdate"].value == "")
	        elements["tcdDeathdate"].disabled = false;
	    else
	        elements["tcdDeathdate"].disabled = true;

//+ Institución de salud
	    if (elements["cbeHealth_Org"].value == "")
	        elements["cbeHealth_Org"].disabled = false;
	    else
	        elements["cbeHealth_Org"].disabled = true;

//+ AFP
	    if (elements["cbeAfp"].value == "")
	        elements["cbeAfp"].disabled = false;
	    else
	        elements["cbeAfp"].disabled = true;

//+ Fecha de Matrimonio
	    if (elements["tcdWedd"].value == "")
	        elements["tcdWedd"].disabled = false;
	    else
	        elements["tcdWedd"].disabled = true;
			
//+ Indicador de Factura
	    if (elements["chkBill_Ind"].value == "")
	        elements["chkBill_Ind"].disabled = false;
	    else
	        elements["chkBill_Ind"].disabled = true;		
	}
}

function SetNavigationParams() {
    if (top.fraHeader.qs("LinkSpecialAction") == "301" && 
        top.fraHeader.qs("LinkParamsClient") > "" &&
        $("[name=tctFirstName]").val() =="" &&
        $("[name=tctLastName]").val() =="" &&
        $("[name=tctLastName2]").val() ==""){

        $("[name=tctFirstName]").val(top.fraHeader.qs("sFirstName"));
        $("[name=tctLastName]").val(top.fraHeader.qs("sLastName"));
        $("[name=tctLastName2]").val(top.fraHeader.qs("sLastName2"));
    }
}

$(function () {
    SetNavigationParams();
});
</SCRIPT>

    <%Response.Write(mobjMenu.setZone(2, "BC001N", "BC001N.aspx"))
        mobjMenu = Nothing%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBC001N" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <A NAME="BeginPage"></A>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<TABLE WIDTH="100%">
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcdInpDateCaption") %></LABEL></TD>
			  <TD WIDTH="25%"> <%=mobjValues.DateControl("tcdInpDate", mobjValues.TypeToString(Session("dInpdate"), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdInpDateToolTip"),  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) <> 301)%> </TD>
			  <TD></TD>
	        <TD></TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("AnchorNombre2Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctLastNameCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.TextControl("tctLastName", 20, mobjClient.sLastName, True, GetLocalResourceObject("tctLastNameToolTip"))%> </TD>
			  <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tctLastName2Caption") %></LABEL></TD>
			  <TD WIDTH="25%"> <%=mobjValues.TextControl("tctLastName2", 20, mobjClient.sLastname2, True, GetLocalResourceObject("tctLastName2ToolTip"))%> </TD>
	    </TR>
	    <TR>
	        <TD><LABEL><%= GetLocalResourceObject("tctFirstNameCaption") %></LABEL></TD>
	        <TD COLSPAN = 3> <%=mobjValues.TextControl("tctFirstName", 20, mobjClient.sFirstName, True, GetLocalResourceObject("tctFirstNameToolTip"))%> </TD>
	    </TR>
	    <TR>
			  <TD><LABEL><%= GetLocalResourceObject("tcdBirthDateCaption") %></LABEL></TD>
			  <TD> <%=mobjValues.DateControl("tcdBirthDate", mobjValues.TypeToString(mobjClient.dBirthdat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdBirthDateToolTip"))%> </TD>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeCivilstaCaption") %></LABEL></TD>
			  <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeCivilsta", "Table14", 1, CStr(mobjClient.nCivilsta),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCivilstaToolTip"))%></TD>
	    </TR>
	    <TR>
			  <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeSexCaption") %></LABEL></TD>
			  <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeSex", "Table18", 1, mobjClient.sSexclien,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSexToolTip"))%></TD>
			  <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeNationalityCaption") %></LABEL></TD>
			  <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeNationality", "Table5518", 1, Session("nNationality"),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeNationalityToolTip"))%></TD>
	    </TR>
	    <TR>
			  <TD><LABEL><%= GetLocalResourceObject("cbeOccupatCaption") %></LABEL></TD>
			  <TD><%=mobjValues.PossiblesValues("cbeOccupat", "Table16", 1, CStr(mobjClient.nSpeciality),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOccupatToolTip"))%></TD>
			  <TD><LABEL><%= GetLocalResourceObject("cbeAreaCaption") %></LABEL></TD>
			  <TD><%=mobjValues.PossiblesValues("cbeArea", "Table5503", 1, CStr(mobjClient.nArea),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeAreaToolTip"))%></TD>
	    </TR>
	    <TR>
			  <TD><LABEL><%= GetLocalResourceObject("cbeTitleCaption") %></LABEL></TD>
			  <TD><%=mobjValues.PossiblesValues("cbeTitle", "Table222", 1, CStr(mobjClient.nTitle),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTitleToolTip"))%></TD>      
        </TR>
        <TR>
              <TD WIDTH="25%" colspan = 2><%= mobjValues.CheckControl("chkPEP", GetLocalResourceObject("chkPEPCaption"), mobjClient.sPEP)%></TD>              	            
              <TD WIDTH="25%" colspan = 2><%= mobjValues.CheckControl("chkCRS", GetLocalResourceObject("chkCRSCaption"), mobjClient.sCRS)%></TD>
             </TR>
            <tr></tr><tr></tr>
        <TR> 
	          <TD><LABEL><%= GetLocalResourceObject("chkUsPersonCaption")%></LABEL></TD> 
	        <TD><%=mobjValues.OptionControl(0, "chkUSPERSON", GetLocalResourceObject("chkUSPERSON_1Caption"), UsPerson, "1", , , , GetLocalResourceObject("chkUSPERSON_1ToolTip"))%>
			<%=mobjValues.OptionControl(0, "chkUSPERSON", GetLocalResourceObject("chkUSPERSON_2Caption"), NoUsPerson, "2", , , , GetLocalResourceObject("chkUSPERSON_2ToolTip"))%></TD>

<!--

			<TD><LABEL><%= GetLocalResourceObject("tctfatcaCaption")%></LABEL></TD>
	        <TD> <%= mobjValues.TextControl("tctfatca", 20, mobjClient.sFatca, False, GetLocalResourceObject("tctFatcaToolTip"))%> </TD>
-->
	    </TR>

         <%= mobjValues.HiddenControl("tctfatca", mobjClient.sFatca)%>
	    
	    <TR>
			  <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Licencia"><%= GetLocalResourceObject("AnchorLicencia2Caption") %></A></LABEL></TD>
		  </TR>
	    <TR>
			  <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR> 
	    
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcdDriverDatCaption") %></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.DateControl("tcdDriverDat", mobjValues.TypeToString(mobjClient.dDriverDat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdDriverDatToolTip"))%> </TD>
			  <TD><LABEL><%= GetLocalResourceObject("tctDriverNumCaption") %></LABEL></TD>
			  <%mobjValues.bNumericText = True
%>
	        <TD> <%=mobjValues.TextControl("tctDriverNum", 10, mobjClient.sLicense, False, GetLocalResourceObject("tctDriverNumToolTip"))%> </TD>
	    </TR>
	    <TR>
			  <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcdDrivExpDatCaption") %></LABEL></TD> 
			  <TD WIDTH="25%"> <%=mobjValues.DateControl("tcdDrivExpDat", mobjValues.TypeToString(mobjClient.dDrivexpdat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdDrivExpDatToolTip"))%> </TD>
			  <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeTypDriverCaption") %></LABEL></TD> <!--Falta el default values -->
			  <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeTypDriver", "Table5504", 1, CStr(mobjClient.nTypdriver),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypDriverToolTip"))%> </TD>
	    </TR>
	    <TR>
			  <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeLimitDrivCaption") %></LABEL></TD>
			  <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeLimitDriv", "Table5521", 1, CStr(mobjClient.nLimitdriv),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeLimitDrivToolTip"))%> </TD>
			  <TD></TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Control"><%= GetLocalResourceObject("AnchorControl2Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="25%" colspan = 2><%=mobjValues.CheckControl("chkBlockade", GetLocalResourceObject("chkBlockadeCaption"), mobjClient.sBlockade)%></TD>
	        <TD><LABEL><%= GetLocalResourceObject("tcdDeathdateCaption") %></LABEL></TD>
	        <TD> <%=mobjValues.DateControl("tcdDeathdate", mobjValues.TypeToString(mobjClient.dDeathdat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdDeathdateToolTip"),  ,  ,  ,  , Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd))%></TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Control"><%= GetLocalResourceObject("AnchorControl3Caption") %></A></LABEL></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeHealth_OrgCaption") %></LABEL></TD> 
	        <TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeHealth_Org", "TABLE5523", 1, CStr(mobjClient.nHealth_org),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeHealth_OrgToolTip"))%> </TD>
			<TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeAfpCaption") %></LABEL></TD>
			<TD WIDTH="25%"> <%=mobjValues.PossiblesValues("cbeAfp", "Table5524", 1, CStr(mobjClient.nAfp),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeAfpToolTip"))%> </TD>
	    </TR>
	    <TR>
	        <TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD> 
	        <TD><%=mobjValues.OptionControl(0, "optSmoking", GetLocalResourceObject("optSmoking_3Caption"), loptNoInfo, "3",  ,  ,  , GetLocalResourceObject("optSmoking_3ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(0, "optSmoking", GetLocalResourceObject("optSmoking_1Caption"), loptSmoker, "1",  ,  ,  , GetLocalResourceObject("optSmoking_1ToolTip"))%></TD>
			<TD><%=mobjValues.OptionControl(0, "optSmoking", GetLocalResourceObject("optSmoking_2Caption"), loptNoSmoker, "2",  ,  ,  , GetLocalResourceObject("optSmoking_2ToolTip"))%></TD>
	    </TR>
	    <TR>
			  <TD><LABEL><%= GetLocalResourceObject("tcdWeddCaption") %></LABEL></TD>
			  <TD> <%=mobjValues.DateControl("tcdWedd", mobjValues.TypeToString(mobjClient.dWedd, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdWeddToolTip"))%></TD>
			  <TD WIDTH="25%"><%=mobjValues.CheckControl("chkBill_Ind", GetLocalResourceObject("chkBill_IndCaption"), mobjClient.sBill_ind)%></TD>
	    </TR>

	    <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdDependantCaption") %></LABEL></TD>
	        <TD> <%=mobjValues.DateControl("tcdDependant", mobjValues.TypeToString(mobjClient.dDependant, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdDependantToolTip"))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdIndependantCaption") %></LABEL></TD>
	        <TD> <%=mobjValues.DateControl("tcdIndependant", mobjValues.TypeToString(mobjClient.dIndependant, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdIndependantToolTip"))%></TD>
	    </TR>
	    <TR>
	        <TD><LABEL><%= GetLocalResourceObject("tcdRetirementCaption") %></LABEL></TD>
	        <TD> <%=mobjValues.DateControl("tcdRetirement", mobjValues.TypeToString(mobjClient.dRetirement, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdRetirementToolTip"))%></TD>
	    </TR>
	</TABLE>
	<P ALIGN="Center"><%=mobjValues.BeginPageButton%></P>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjClient = Nothing

If CStr(Session("sOriginalForm")) <> vbNullString Then
	'If  Request.QueryString("nMainAction") <> 301 Then
	' Response.Write "<NOTSCRIPT>insEnabledFields();</SCRIPT>"
	'End If
End If
%>





