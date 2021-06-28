<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo de la Fecha
    Dim mobjDate As eGeneral.GeneralFunction
    
    '- Objeto para el manejo de periodos
    Dim lclsCtrol_date As eGeneral.Ctrol_date
    
    '- Constante para determinar codigo de acción para obtener ultimo periodo de CTROL_DATE
    Const clngGenBookCollection As Short = 201
    
    'Variables para determinación de fechas
    Dim mdEffecdate As String
    Dim FirstDay As Date
    Dim LastDay As Date
    
</script>
<%Response.Expires = -1441

    mobjDate = New eGeneral.GeneralFunction
    lclsCtrol_date = New eGeneral.Ctrol_date
    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "col504_k"
    mobjMenu = New eFunctions.Menues
%>

<%  
    'Se obtiene la variable de fecha de CTROL_DATE para asignar valores a la pagina
    If lclsCtrol_date.Find(clngGenBookCollection) Then
        
        'Se determina la fecha de inicio del periodo
        FirstDay = lclsCtrol_date.dEffecdate.AddDays(1)
                
        'Se determina la fecha de fin del periodo
        'LastDay = lclsCtrol_date.dEffecdate.AddMonths(1)
        LastDay = FirstDay
    End If
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL504", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL504", "COL504_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
'	Response.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>" 
%>
<SCRIPT LANGUAGE=JavaScript> 
//+ Variable para el control de versiones 
	document.VssVersion="$$Revision: 1 $|$$Date: 16/11/04 15:05 $|$$Author: Nvaplat11 $" 

//% insStateZone: se controla el estado de los campos de la página 
//-------------------------------------------------------------------------------------------- 
function insStateZone(){ 
//-------------------------------------------------------------------------------------------- 
} 

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

</SCRIPT>


</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL504" ACTION="valbookrep.aspx?sMode=2">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>

	<BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateIniCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDateIni", FirstDay, , GetLocalResourceObject("tcdDateIniToolTip"), , , , , False)%></TD>
			<TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(101090, "tcnOption", GetLocalResourceObject("tcnOption_1Caption"), "1", "1")%></TD>
            <TD><%=mobjValues.OptionControl(0, "optOption", GetLocalResourceObject("optOption_3Caption"), "1", "1")%> </TD>
        </TR>        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateEndCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDateEnd", LastDay, , GetLocalResourceObject("tcdDateEndToolTip"), , , , ,False)%></TD>
			<TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(101091, "tcnOption", GetLocalResourceObject("tcnOption_2Caption"), "2", "2")%></TD>
            <TD><%=mobjValues.OptionControl(0, "optOption", GetLocalResourceObject("optOption_4Caption"), "2", "2")%> </TD>
        </TR>        
    </TABLE>

</FORM> 
</BODY>
</HTML>





