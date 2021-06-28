<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CR768_k"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

<SCRIPT LANGUAGE=JavaScript>
// Variable para el control de versiones
document.VssVersion="$$Revision: 1 $|$$Date: 21/04/06 16:07 $|$$Author: Rnavarre $"


//% DisabledCoverGen: Habilita y desabilita el de cobertura generica si es Vida
//--------------------------------------------------------------------------------------------
function DisabledCoverGen(Field){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0])
	{
		alert('Field...' + Field);
		if(Field=='40')
		{
			valCovergen.disabled = false;
			btnvalCovergen.disabled = false;		
		}
		else
		{
			valCovergen.disabled = true;
			btnvalCovergen.disabled = true;
		}
	}
}

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
 	with (self.document.forms[0])
 	{
 		cbeBranch.disabled= false;
 		valProduct.disabled= false;
		tcnNumber.disabled= false;
		cbeBranchrei.disabled= false;
		valCovergen.disabled= false;
		tcnCapital.disabled= false;
		tcdEffecdate.disabled= false;
		tcdEffecdate.disabled= false;
		btn_tcdEffecdate.disabled= false;
		
		if(top.fraSequence.plngMainAction==301 || top.fraSequence.plngMainAction==302 ||
			top.fraSequence.plngMainAction==306 || top.fraSequence.plngMainAction==401)
		{
			valCovergen.disabled = false;
			btnvalCovergen.disabled = false;		
		}
	}
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
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("CR768", "CR768.aspx", 1, vbNullString))
mobjMenu = Nothing
'"DisabledCoverGen(this.value);"
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" ID="FORM" NAME="CR768" ACTION="ValCoReinsuranTra.aspx?sMode=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , True, 1)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True,  ,  ,  ,  ,  , 2, True)%></TD>            
        </TR>
		  <TR>
		    <TD><LABEL><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnNumber", 5, vbNullString,  , GetLocalResourceObject("tcnNumberToolTip"),  ,  ,  ,  ,  ,  , True, 3)%></TD>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchreiCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranchrei", GetLocalResourceObject("cbeBranchreiToolTip"),  , "valproductrea",  ,  ,  ,  , True, 4)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("valCovergenCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCovergen", "tabtab_lifcov2", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valCovergenToolTip"),  , 5)%> </TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True, 6)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCapital", 18, vbNullString,  , GetLocalResourceObject("tcnCapitalToolTip"),  ,  ,  ,  ,  ,  , True, 7)%></TD>
		</TR>
        <TR>
		</TR>
    </TABLE>
<BR>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>




