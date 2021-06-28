<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MOP633"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT> 

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

//% insStateZone: Habilita/inhabilita los campos según la acción
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
    self.document.forms[0].valUsercod.disabled = false
    self.document.forms[0].btnvalUsercod.disabled = false
//    self.document.forms[0].cbeOffice.disabled = false
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//%insFinish: Controla la acción "Finalizar" de la página.
//-----------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------
   return true
}


//insChangeUser: Llama al procedimiento que obtiene la oficina asociada al usuario
//--------------------------------------------------------------------------------------------
function insChangeUser(nUsercode){
//--------------------------------------------------------------------------------------------
	insDefValues("MOP633","nUser=" + nUsercode + "&sField=MOP633","/VTimeNet/Maintenance/MantCashBank");
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MOP633", "MOP633_k.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MOP633" ACTION="ValMantCashBank.aspx?sMode=1">
<BR><BR>
  <TABLE WIDTH="100%">
    <TR>
      <TD WIDTH="30%"><TD>
      <TD WIDTH="10%"><LABEL ID=0><%= GetLocalResourceObject("valUsercodCaption") %></LABEL></TD>
      <TD><%=mobjValues.PossiblesValues("valUsercod", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  , "insChangeUser(this.value)", True, 5, GetLocalResourceObject("valUsercodToolTip"),  ,  ,  , True)%></TD>
    </TR>
    <TR>  
      <TD><TD>    
      <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
      <TD><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True, 30, GetLocalResourceObject("cbeOfficeToolTip"), 2)%></TD>      
    </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>






