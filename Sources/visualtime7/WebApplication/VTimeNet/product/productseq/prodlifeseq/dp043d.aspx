<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable para la carga de datos en la forma 
Dim mclsProduct_li As eProduct.Product


'% insPreDP043D: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreDP043D()
	'------------------------------------------------------------------------------------------------	
	Call mclsProduct_li.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsProduct_li = New eProduct.Product

mobjValues.ActionQuery = Session("bQuery")
Call insPreDP043D()

mobjValues.sCodisplPage = "dp043d"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:08 $|$$Author: Nvaplat61 $"
</SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP043D", "DP043D.aspx"))
	.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>    
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP043C" ACTION="valProdLifeSeq.aspx?x=1">    	
<%=mobjValues.ShowWindowsName("DP043D")%>
</BODY>
</HTML>
	<TABLE WIDTH="100%">
		<TR>	
			<TD>&nbsp;</TD>  
		</TR>    		
		<TR>
			<TD WIDTH="15%">&nbsp;</TD>            
			<TD WIDTH="15%">&nbsp;</TD>            		
		    <TD><LABEL ID=14873><%= GetLocalResourceObject("tctRoureducCaption") %></LABEL></TD>
			<TD WIDTH="45%"><%=mobjValues.TextControl("tctRoureduc", 12, mclsProduct_li.sRoureduc,  , GetLocalResourceObject("tctRoureducToolTip"))%></TD>
		</TR>			
	    <TR>
			<TD WIDTH="15%">&nbsp;</TD>            
			<TD WIDTH="15%">&nbsp;</TD>	    
			<TD><LABEL ID=14886><%= GetLocalResourceObject("tctRouredccCaption") %></LABEL></TD>
			<TD WIDTH="45%"><%=mobjValues.TextControl("tctRouredcc", 12, mclsProduct_li.sRoureddc,  , GetLocalResourceObject("tctRouredccToolTip"))%></TD>
		</TR>			
	</TABLE>
</FORM>
</BODY>
</HTML>	




