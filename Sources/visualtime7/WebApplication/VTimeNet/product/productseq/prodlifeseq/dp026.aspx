<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable para la carga de datos en la forma 
Dim mclsProduct_li As eProduct.Product


</script>
<%Response.Expires = 0

mclsProduct_li = New eProduct.Product
mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values

mobjValues.ActionQuery = Session("bQuery")

Call mclsProduct_li.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

mobjValues.sCodisplPage = "dp026"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">    


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP026", "DP026.aspx"))
	.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>    
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:08 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP026" ACTION="valProdLifeSeq.aspx?sMode=1">
	<%=mobjValues.ShowWindowsName("DP026")%>
    <TABLE WIDTH="100%">
		<TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100187><A NAME="Contratación"><%= GetLocalResourceObject("AnchorContrataciónCaption") %></A></LABEL></TD>
            <TD WIDTH="7%">&nbsp;</TD>            
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100187><A NAME="Renovación"><%= GetLocalResourceObject("AnchorRenovaciónCaption") %></A></LABEL></TD>
        </TR>                       
        <TR>                       
            <TD COLSPAN="4" CLASS="Horline"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>
		</TR>		
		<TR>
			<TD WIDTH="7%"><LABEL ID=14882><%= GetLocalResourceObject("tcnSuageMinCaption") %></LABEL></TD>
			<TD WIDTH="7%"><%=mobjValues.NumericControl("tcnSuageMin", 3, CStr(mclsProduct_li.nSuagemin),  , GetLocalResourceObject("tcnSuageMinToolTip"),  ,  ,  ,  ,  ,  ,  , 1)%></TD>
			<TD WIDTH="7%"><LABEL ID=14905><%= GetLocalResourceObject("tcnSuageMaxCaption") %></LABEL></TD>
			<TD WIDTH="7%"><%=mobjValues.NumericControl("tcnSuageMax", 3, CStr(mclsProduct_li.nSuagemax),  , GetLocalResourceObject("tcnSuageMaxToolTip"),  ,  ,  ,  ,  ,  ,  , 2)%></TD>
			<TD>&nbsp;</TD>            
			<TD WIDTH="9%"><LABEL ID=14883><%= GetLocalResourceObject("tcnReageMaxCaption") %></LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.NumericControl("tcnReageMax", 3, CStr(mclsProduct_li.nReagemax),  , GetLocalResourceObject("tcnReageMaxToolTip"),  ,  ,  ,  ,  ,  ,  , 3)%></TD>
		</TR>
	</TABLE>
	<BR>
	<TABLE WIDTH="100%">
		<TR>                       
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100189><A NAME="Edad"><%= GetLocalResourceObject("AnchorEdadCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
			<TD WIDTH="45%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100188><A NAME="Probabilidad"><%= GetLocalResourceObject("AnchorProbabilidadCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>		    
		    <TD></TD>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>      		
		<TR>
			<TD><LABEL ID=14880><%= GetLocalResourceObject("tcnYearminwCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnYearminw", 3, CStr(mclsProduct_li.nYearminw),  , GetLocalResourceObject("tcnYearminwToolTip"),  ,  ,  ,  ,  ,  ,  , 4)%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD><LABEL ID=14885><%= GetLocalResourceObject("tcnSmokeCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnSmoke", 4, CStr(mclsProduct_li.nTaxsmoke),  , GetLocalResourceObject("tcnSmokeToolTip"), True, 2,  ,  ,  ,  ,  , 7)%></TD>
		</TR>			
		<TR>
			<TD><LABEL ID=14879><%= GetLocalResourceObject("tcnYearMorsCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnYearMors", 3, CStr(mclsProduct_li.nYearmors),  , GetLocalResourceObject("tcnYearMorsToolTip"),  ,  ,  ,  ,  ,  ,  , 5)%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD><LABEL ID=14884><%= GetLocalResourceObject("tcnNSmokeCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnNSmoke", 4, CStr(mclsProduct_li.nTaxnsmoke),  , GetLocalResourceObject("tcnNSmokeToolTip"), True, 2,  ,  ,  ,  ,  , 8)%></TD>		
		</TR>	
		<TR>
			<TD><LABEL ID=14878><%= GetLocalResourceObject("tcnYearMinsCaption") %></LABEL></TD>    
			<TD><%=mobjValues.NumericControl("tcnYearMins", 3, CStr(mclsProduct_li.nYearmins),  , GetLocalResourceObject("tcnYearMinsToolTip"),  ,  ,  ,  ,  ,  ,  , 6)%></TD>
			<TD COLSPAN="3">&nbsp;</TD>
		</TR>			
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mclsProduct_li = Nothing
mobjValues = Nothing
%>




