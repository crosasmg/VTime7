<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página.
Dim mobjMenu As eFunctions.Menues
Dim lclsCtrol_date As eGeneral.Ctrol_date
Dim mdEffecdate As String

    '+ Generación de cesiones de primas RNP.
    Const clngGenCessPremiumRNP As Short = 110


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
lclsCtrol_date = New eGeneral.Ctrol_date

mobjValues.sCodisplPage = "CRL013_k"
%>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT> 

//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insPreZone(llngAction){
//-----------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL013", "CRL013_k.aspx", 1, ""))
Response.Write(mobjMenu.setZone(1, "CRL013", "", eFunctions.Menues.TypeForm.clngSpeWithOutHeader))
mobjMenu = Nothing
%>    
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL013" ACTION="valCoReinsuranRep.aspx?sMode=1">

<%
    If lclsCtrol_date.Find(clngGenCessPremiumRNP) Then
        mdEffecdate = mobjValues.TypeToString(lclsCtrol_date.dEffecdate, eFunctions.Values.eTypeData.etdDate)
        mdEffecdate = mdEffecdate
    End If
%>

<BR></BR>
	<BR>
		<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>    
	</BR>

	<TABLE WIDTH="100%">
		<TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="10%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateStartCaption") %> </LABEL></TD>
			<TD width="17%"><%=mobjValues.DateControl("tcdDateStart", mdEffecdate,  , GetLocalResourceObject("tcdDateStartToolTip"))%></TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
		</TR>
		<TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="10%"><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %> </LABEL></TD>
<TD width="17%"><% %>
<%=mobjValues.DateControl("tcdDateTo", CStr(Today),  , GetLocalResourceObject("tcdDateToToolTip"))%></TD>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
		</TR>		
	</TABLE>
	
	<TABLE>	
        <TR> <TD>&nbsp;</TD></TR>
        <TR>
			<TD width="38%">&nbsp;&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %> </LABEL>&nbsp;</TD>
            <TD> 
                <%Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_2Caption"), "1", "2"))%>
            </TD>
        </TR>
        <TR>
			<TD width="17%">&nbsp;</TD>
			<TD width="17%">&nbsp;</TD>
            <TD> 
                <%Response.Write(mobjValues.OptionControl(0, "optEjecucion", GetLocalResourceObject("optEjecucion_1Caption"), , "1"))%>
            </TD>
         </TR>   
	</TABLE>
</FORM>
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 3 $|$$Date: 24/04/06 12:40 $" 
</SCRIPT>
</BODY>
</HTML>

<%
mobjValues = Nothing
lclsCtrol_date = Nothing
%>
