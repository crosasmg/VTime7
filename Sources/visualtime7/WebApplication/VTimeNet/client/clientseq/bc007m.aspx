<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Private mobjClient As eClient.Client


'%insPreSi007M. Esta funcion se encarga deralizar la busqueda de los datos de cliente
'------------------------------------------------------------------------------------
Private Sub insPreSi007M()
	'------------------------------------------------------------------------------------
	mobjClient = New eClient.Client
	mobjClient.Find(Session("sClient"))
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues


If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

'+Se realiza el llamado a la funcion insPreSi007M, para obtener los datos del cliente en tratamiento
insPreSi007M()
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%Response.Write(mobjMenu.setZone(2, "BC007M", "BC007M.aspx"))
Response.Write(mobjValues.StyleSheet())
mobjMenu = Nothing
%>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
	
//% insEnabledFields: Inhabilita los campos de la ventana que estén llenos si la variable
//%					  de sesión "sOriginalForm" es diferente de blanco - ACM - 07/08/2001
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
//+ Nivel ingresos
		if(elements["cbeLevel"].value=="" || elements["cbeLevel"].value==0)
			elements["cbeLevel"].disabled=false
		else
			elements["cbeLevel"].disabled=true;
			
//+ Vivienda
		if(elements["cbeHouseType"].value=="" || elements["cbeHouseType"].value==0)
			elements["cbeHouseType"].disabled=false
		else
			elements["cbeHouseType"].disabled=true;

//+ Hijos
		if(elements["tcnChild"].value=="" || elements["tcnChild"].value==0)
			elements["tcnChild"].disabled=false
		else
			elements["tcnChild"].disabled=true;

//+ Autos
		if(elements["tcnCars"].value=="" || elements["tcnCars"].value==0)
			elements["tcnCars"].disabled=false
		else
			elements["tcnCars"].disabled=true;

//+ General
		if(elements["cbeClass"].value=="" || elements["cbeClass"].value==0)
			elements["cbeClass"].disabled=false
		else
			elements["cbeClass"].disabled=true;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmBC007M" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
	<TABLE CELLSPACING=1 CELLPADDING=1 WIDTH="100%">
		<TR>
			<TD><LABEL ID=9789><%= GetLocalResourceObject("cbeLevelCaption") %></LABEL></TD>
			<%mobjValues.TypeOrder = 1%>
			<TD><%=mobjValues.PossiblesValues("cbeLevel", "Table147", 1, CStr(mobjClient.nEconomic_l),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeLevelToolTip"))%></TD>
			<TD><LABEL ID=9791><%= GetLocalResourceObject("cbeHouseTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeHouseType", "Table149", 1, CStr(mobjClient.nHouse_type),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeHouseTypeToolTip"))%></TD>
		</TR>
		<TR>
		    <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
		</TR>
		<TR>
		    <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40384><A NAME="Quantity"><%= GetLocalResourceObject("AnchorQuantityCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=9793><%= GetLocalResourceObject("tcnChildCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnChild", 2, mobjValues.TypeToString(mobjClient.nQ_child, eFunctions.Values.eTypeData.etdDouble), False, GetLocalResourceObject("tcnChildToolTip"), False)%></TD>
			<TD><LABEL ID=9792><%= GetLocalResourceObject("tcnCarsCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCars", 2, mobjValues.TypeToString(mobjClient.nQ_cars, eFunctions.Values.eTypeData.etdDouble), False, GetLocalResourceObject("tcnCarsToolTip"), False)%></TD>
		</TR>
		<TR>
		    <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
		</TR>
		<TR>
		    <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40385><A NAME="Class"><%= GetLocalResourceObject("AnchorClassCaption") %></A></LABEL></TD>
		</TR>
		<TR>
		    <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
		</TR>  
		<TR>
			<TD><LABEL ID=9790><%= GetLocalResourceObject("cbeClassCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeClass", "Table146", 1, CStr(mobjClient.nClass),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeClassToolTip"))%></TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
'+ Si la variable de sesión "sOriginalForm" es diferente de blanco,
'+ entonces se invoca a la función "insEnabledFields" - ACM - 07/08/2001
If CStr(Session("sOriginalForm")) <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields();</script>")
End If

mobjValues = Nothing
mobjClient = Nothing
%>




