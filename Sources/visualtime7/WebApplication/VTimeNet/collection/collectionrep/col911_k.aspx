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


</script>
<%Response.Expires = -1441

mobjDate = New eGeneral.GeneralFunction

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "col911_k"
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL911", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL911", "COL911_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
'	Response.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>" 
%> 
<SCRIPT LANGUAGE=JavaScript> 
//+ Variable para el control de versiones 
	document.VssVersion="$$Revision: 1 $|$$Date: 28/01/04 10:44 $|$$Author: Nvaplat61 $" 

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
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/OP006_K.js"></SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL911" ACTION="valCollectionRep.aspx?sMode=2">
	<BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>

	<BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
            <TD>
            <%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", 1,  ,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip")))
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nOfficeAgen", Session("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("cbeOfficeAgen"), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
End With
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
            <TD>
            <%
With mobjValues
	.Parameters.Add("nOfficeAgen", Session("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("cbeAgency"), True,  ,  ,  ,  , "insInitialAgency(3)",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
End With
%>
            </TD>
        </TR>        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdCollectDateEndCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdCollectDateEnd", CStr(mobjDate.GetLastFistDay("LAST")),  , GetLocalResourceObject("tcdCollectDateEndToolTip"))%></TD>
        </TR>        
    </TABLE>
</FORM> 
</BODY>
</HTML>





