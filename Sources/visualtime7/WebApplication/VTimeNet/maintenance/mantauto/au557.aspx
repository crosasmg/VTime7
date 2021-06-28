<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se declara la variable para la carga de los datos en la forma
Dim mclsAuto_db As ePolicy.Auto_db


'% Obtiene los datos del automóvil
'%--------------------------------------------------------------------------------------
Private Sub insPreBV001()
	'%----------------------------------------------------------------------------------------
	Call mclsAuto_db.insPreBV001(Request.QueryString.Item("sLicense_ty"), Request.QueryString.Item("sRegist"))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsAuto_db = New ePolicy.Auto_db

mobjValues.ActionQuery = True
mobjValues.sCodisplPage = "AU557"
Call insPreBV001()
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% ChangeLicense: Deja el campo patente y dígito en blanco
//-------------------------------------------------------------------------------------------
    function ChangeLicense(){
//-------------------------------------------------------------------------------------------    
	self.document.forms[0].tctRegister.value='';
	self.document.forms[0].tctDigit.value='';
}
//% ShowData: Se muestra el dígito verificador de la patente
//-------------------------------------------------------------------------------------------
function ShowData(sField){
//-------------------------------------------------------------------------------------------
	if(self.document.forms[0].tctRegister.value!=''){
		insDefValues(sField,"sRegist=" + self.document.forms[0].tctRegister.value + "&sLicense_ty=" + self.document.forms[0].cbeLicense_ty.value)
	}
}
</SCRIPT>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 26/07/04 18:44 $|$$Author: Nvaplat7 $"
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "AU557", "AU557.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantAuto.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sRegistOld=<%=Request.QueryString.Item("sRegistOld")%>&sLicense_tyOld=<%=Request.QueryString.Item("sLicense_tyOld")%>">

<%=mobjValues.ShowWindowsName("AU557")%>
	<TABLE WIDTH = "100%">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Datos"><%= GetLocalResourceObject("AnchorDatosCaption") %></A></LABEL></TD>
		</TR>
		<TR>
	        <TD CLASS="HorLine" COLSPAN="4"></TD>
	    </TR>
	</TABLE>
	<TABLE WIDTH = "100%">
		<TR>
			<TD><LABEL ID=11769><%= GetLocalResourceObject("tctMotorCaption") %></LABEL></TD>
			<TD><%=mobjValues.TextControl("tctMotor", 40, mclsAuto_db.sMotor,  , GetLocalResourceObject("tctMotorToolTip"))%></TD></TD>
			<TD>&nbsp;</TD>
            <TD><LABEL ID=11769><%= GetLocalResourceObject("tctChasisCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctChasis", 40, mclsAuto_db.sChassis,  , GetLocalResourceObject("tctChasisToolTip"))%></TD></TD>
		</TR>
        <TR>
            <TD WIDTH = "15%"><LABEL ID=11773><%= GetLocalResourceObject("valVehCodeCaption") %></LABEL></TD>
			<%With mobjValues.Parameters
	.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With%>
            <TD WIDTH = "35%"><%=mobjValues.PossiblesValues("valVehCode", "tabTab_au_veh", 2, mclsAuto_db.sVehCode, True,  ,  ,  ,  , "ShowChangeValues(""Auto_db"")",  , 6, GetLocalResourceObject("valVehCodeToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD WIDTH = "15%"><LABEL ID=11764><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <%=mobjValues.DIVControl("lblVehMark", True, mclsAuto_db.sVehBrand)%>
        </TR>       
        <TR>
            <TD><LABEL ID=11769><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <%=mobjValues.DIVControl("lblVehModel", True, mclsAuto_db.sVehModel)%>
   			<TD WIDTH = "5%">&nbsp;</TD>
            <TD><LABEL ID=19646><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <%=mobjValues.DIVControl("lblType", True, mclsAuto_db.sVehType)%>
        </TR>
        <TR>
            <TD WIDTH = "15%"><LABEL ID=11766><%= GetLocalResourceObject("tctVehownCaption") %></LABEL></TD>
            <TD><%=mobjValues.ClientControl("tctVehown", mclsAuto_db.sVeh_own,  , GetLocalResourceObject("tctVehownToolTip"),  ,  , "tctName", True,  ,  ,  ,  ,  , True)%></TD>            
            <%=mobjValues.DIVControl("tctName", True)%>
        </TR>
    </TABLE>        
    <TABLE WIDTH = "100%" >
   		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Patentenueva"><%= GetLocalResourceObject("AnchorPatentenuevaCaption") %></A></LABEL></TD>
		</TR>
		<TR>
	        <TD CLASS="HorLine" COLSPAN="5"></TD>
	    </TR>
	    <%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
	mobjValues.ActionQuery = False
End If%>
	    <TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeLicense_tyCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = False
mobjValues.TypeList = 1
mobjValues.List = "1,2"
Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, "1",  ,  ,  ,  ,  , "ChangeLicense();", False,  , GetLocalResourceObject("cbeLicense_tyToolTip")))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL><%= GetLocalResourceObject("tctRegisterCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRegister", 10,  ,  , GetLocalResourceObject("tctRegisterToolTip"),  ,  ,  , "ShowData(""Digit"")", False)%>-<%=mobjValues.TextControl("tctDigit", 1,  ,  , "Dígito verificador de la patente",  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
    <%
mobjValues = Nothing
mclsAuto_db = Nothing
%>    
</FORM>
</BODY>
</HTML>





