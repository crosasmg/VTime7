<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para mostrar la descripción de la clase del producto y la moneda

Dim mclsProduct_li As eProduct.Product
Dim mobjMenu As eFunctions.Menues


Private Sub insPreDP607_k()
	Call mclsProduct_li.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	Session("nCurrency") = mclsProduct_li.nCurrency
	
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mclsProduct_li = New eProduct.Product

Call insPreDP607_k()

mobjValues.ActionQuery = True

mobjValues.sCodisplPage = "dp607_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//%insStateZone: Permite habilitar/deshabilitar los campos de la ventana
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: Se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	top.close()
	return true;
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}    
</SCRIPT>
    <%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP607", "DP607_K.aspx", 1, ""))
	.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP0043_K" ACTION="valProdActLifeSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="10%"><LABEL ID=14871><%= GetLocalResourceObject("cbeProdclasCaption") %></LABEL></TD>
			<TD WIDTH="10%"><%=mobjValues.PossiblesValues("cbeProdclas", "Table124", 1, CStr(mclsProduct_li.nProdClas),  ,  ,  ,  ,  ,  , True,  , "")%></TD>            
			<TD WIDTH="5%">&nbsp;</TD>
			<TD WIDTH="5%"><LABEL ID=14875><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD WIDTH="10%"><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", 1, CStr(mclsProduct_li.nCurrency),  , True,  ,  ,  ,  , True,  , "")%></TD>
        </TR> 
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mclsProduct_li = Nothing
mobjValues = Nothing
Response.Write("<SCRIPT>top.frames['fraSequence'].document.location.href='Sequence.aspx?sGoToNext=Yes'</SCRIPT>")
%>




