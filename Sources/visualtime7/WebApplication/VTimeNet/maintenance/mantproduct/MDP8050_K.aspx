<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MDP8050", "MDP8050_k.aspx", 1, ""))
	Response.Write("<BR>")
End With
mobjMenu = Nothing
%>
      
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 18.57 $|$$Author: Gazuaje $"
    
//% insStateZone: Se controla el estado de los campos de la página.
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: Se controla la acción Cancelar de la página.
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: Se controla la acción Cancelar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MDP8050" ACTION="valMantProduct.aspx?sTime=1">
    <TABLE WIDTH="100%">
		<TR>
			<TD>&nbsp;</TD>			
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnYear", 4,  ,  , GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
			
        </TR>
         <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbenTypeInvestCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbenTypeInvest", "Table5520", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbenTypeInvestToolTip"))%></TD>             
         </TR>         
    </TABLE>
</FORM>
</BODY>
</HTML>




