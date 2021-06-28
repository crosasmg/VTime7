<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable para la carga de datos en la forma 
Dim mclsProd As eProduct.Product

'- Se define variable para manejo de funciones generales	
Dim lclsGeneral As eGeneral.GeneralFunction


'% insPreDP607: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreDP607()
	'------------------------------------------------------------------------------------------------
	Call mclsProd.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP607"
mobjMenu = New eFunctions.Menues
mclsProd = New eProduct.Product
lclsGeneral = New eGeneral.GeneralFunction

Call insPreDP607()

If CStr(Session("sBrancht")) = "1" And mclsProd.nProdClas = 7 Then
	mobjValues.ActionQuery = Session("bQuery")
Else
	mobjValues.ActionQuery = True
End If
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP607", "DP607.aspx"))
	.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>    
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%=mobjValues.StyleSheet()%>
</HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"
    
//% ShowSubSequence: Muestra la subsecuencia de características de vida Activa
//--------------------------------------------------------------------------------------------
function ShowSubSequence(){
//--------------------------------------------------------------------------------------------
	ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/ProdActLifeSeq&sCodispl=DP607', 'ProdActLifeSeq', 750, 500, 'no', 'no', 20, 20, 'yes')  
}
</SCRIPT>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProdActLife" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName("DP607")%>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="10%"><LABEL ID="14875"><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProd.nCurrency),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></td>            
        </TR> 

        <TR>
            <TD COLSPAN="5" CLASS="HIGHLIGHTED"><LABEL ID="41425"><A NAME="Información general"><%= GetLocalResourceObject("AnchorInformación generalCaption") %></A></LABEL></TD>
        </TR>                       
        <TR>                       
            <TD COLSPAN="5" CLASS="HORLINE"></TD>		    
		</TR>
    	<TR>
			<TD COLSPAN="2"><LABEL ID="0"><%= GetLocalResourceObject("tcnPremMinCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.NumericControl("tcnPremMin", 18, CStr(mclsProd.nPremMin),  , GetLocalResourceObject("tcnPremMinToolTip"), True, 6)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><LABEL ID="0"><%= GetLocalResourceObject("tcnQMonVPNCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnQMonVPN", 4, CStr(mclsProd.nQmonVPN),  , GetLocalResourceObject("tcnQMonVPNToolTip"))%></TD>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("tcnQMonToVPNCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnQMonToVPN", 4, CStr(mclsProd.nQmonToVPN),  , GetLocalResourceObject("tcnQMonToVPNToolTip"))%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><LABEL ID="0"><%= GetLocalResourceObject("tcnRateRehCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.NumericControl("tcnRateReh", 6, CStr(mclsProd.nRateReh),  , GetLocalResourceObject("tcnRateRehToolTip"), True, 3)%></TD>
		</TR>        
    	<TR>
    		<TD COLSPAN="5">&nbsp;</TD>
    	<TR>	
    	
    	<%If CStr(Session("sBrancht")) = "1" And mclsProd.nProdClas = 7 Then%>
		<TR>	
			<TD ALIGN="CENTER" COLSPAN="7">
				<LABEL ID="41434"><A HREF="JAVASCRIPT:ShowSubSequence()"><%= GetLocalResourceObject("btnSequenceCaption") %></A></LABEL>
				&nbsp;
				<%=mobjValues.AnimatedButtonControl("btnSequence", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("btnSequenceToolTip"),  , "ShowSubSequence()")%>
			</TD>
		</TR>
		<%Else
	Response.Write("<SCRIPT> alert(""" & "55865: " & lclsGeneral.insLoadMessage(55865) & """); </SCRIPT> ")
End If%>
    </TABLE>      
</FORM>
</BODY>
</HTML>




