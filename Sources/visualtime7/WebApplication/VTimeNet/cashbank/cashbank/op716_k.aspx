<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores		
Dim mobjMenu As eFunctions.Menues

Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "op716_k"
%>
<HTML>
<HEAD>


    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
    <SCRIPT LANGUAGE="JavaScript">

        //+Variable para el control de versiones
        document.VssVersion = "$$Revision: 1 $|$$Date: 11/02/04 17:25 $|$$Author: Nvaplat7 $"

        //%insStateZone: Habilita/Deshabilita los campos de la ventana
        //--------------------------------------------------------------------------------------------------
        function insStateZone() {
            //--------------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                tcdStartDate.disabled = false;
                btn_tcdStartDate.disabled = false;
                tcdEndDate.disabled = false;
                btn_tcdEndDate.disabled = false;
            }
        }

        //%insCancel: Controla la acción "Cancelar" de la página
        //--------------------------------------------------------------------------------------------------
        function insCancel() {
            //--------------------------------------------------------------------------------------------------
            return true;
        }

        //%insFinish: Controla la acción "Finalizar" de la página
        //--------------------------------------------------------------------------------------------------
        function insFinish() {
            //--------------------------------------------------------------------------------------------------
            return true;
        }
</SCRIPT>
<META HTTP-EQUIV="Content-Language" CONTENT="Microsoft Visual Studio 6.0">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("OP716", "OP716.aspx", 1, ""))
mobjMenu = Nothing
%>
    <BR>
</HEAD>
<BODY CLASS="Header" VLINK=white LINK=white ALINK=white >
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmAproved" ACTION="valCashBank.aspx?sMode=1">
     <TABLE WIDTH="100%" >   
     <TR>
        <TD COLSPAN="2" CLASS=HIGHLIGHTED><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
     </TR>
     <TR>
 	    <TD COLSPAN="2" CLASS="Horline"></TD>
     </TR>     
     <TR>		
		 <TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartDateCaption") %></LABEL></TD>
         <TD><%=mobjValues.DateControl("tcdStartDate",  ,  , GetLocalResourceObject("tcdStartDateToolTip"),  ,  ,  ,  , True, 3)%></TD>
		 <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
         <TD><%=mobjValues.DateControl("tcdEndDate",  ,  , GetLocalResourceObject("tcdEndDateToolTip"),  ,  ,  ,  , True, 4)%></TD>
	</TR>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("valUserColumnCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("valReqUser", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, CStr(Session("nUsercode")),  ,  ,  ,  ,  ,  ,True,  , GetLocalResourceObject("valUserColumnToolTip"))%></TD>
    </TR>

    </TABLE>
<%
    mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




