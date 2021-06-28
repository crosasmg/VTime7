<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mintBranch As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "crl004_k"
%>

<HTML>
<HEAD>
		<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------------------
}
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
	return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
//--------------------------------------------------------------------------------------------------
function EnabledField(Field)
//--------------------------------------------------------------------------------------------------
{
	if(Field==1 || Field==2 || Field==4){
		self.document.forms[0].elements["cbeBranchRei"].value=0;
		self.document.forms[0].elements["cbeBranchRei"].disabled=true;
	}
	else
		self.document.forms[0].elements["cbeBranchRei"].disabled=false;
}


</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("CRL004", "CRL004_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CRL004" ACTION="valCoReinsuranRep.aspx?sMode=1">
	<BR><BR><BR>
	<TABLE WIDTH="70%" align="center">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Período"><%= GetLocalResourceObject("AnchorPeríodoCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD>
				<LABEL><%= GetLocalResourceObject("tcdInitdateCaption") %></LABEL>
			</TD>
			<TD>
<%=mobjValues.DateControl("tcdInitdate", CStr(Today), True, GetLocalResourceObject("tcdInitdateToolTip"))%>
			</TD>
		</TR>
		<TR>	
			<TD>
				<LABEL ID=101675><%= GetLocalResourceObject("tcdEnddateCaption") %></LABEL>
			</TD>
			<TD>
<%=mobjValues.DateControl("tcdEnddate", CStr(Today), True, GetLocalResourceObject("tcdEnddateToolTip"))%>
			</TD>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Tipo de Ejecución"><%= GetLocalResourceObject("AnchorTipo de EjecuciónCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
        <TR>
            <TD> 
                <%Response.Write(mobjValues.OptionControl(40670, "optEjecucion", GetLocalResourceObject("optEjecucion_2Caption"), "1", "2"))%>
            </TD>
            <TD> 
                <%Response.Write(mobjValues.OptionControl(40671, "optEjecucion", GetLocalResourceObject("optEjecucion_1Caption"),  , "1"))%>
            </TD>
        </TR>

		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Reaseguro"><%= GetLocalResourceObject("AnchorReaseguroCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD>
				<LABEL ID=101679><%= GetLocalResourceObject("cbeBranchReiCaption") %></LABEL>
			</TD>
			<TD>
				<%=mobjValues.PossiblesValues("cbeBranchRei", "Table5000", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchReiToolTip"))%>
			</TD>
		</TR>
		<TR>
			<TD>
				<LABEL ID=101676><%= GetLocalResourceObject("cbeCompReiCaption") %></LABEL>
			</TD>
			<TD>
				<%=mobjValues.PossiblesValues("cbeCompRei", "Company", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCompReiToolTip"))%>
			</TD>
			<TD>&nbsp;</TD>
		</TR>

		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Poliza"><%= GetLocalResourceObject("AnchorPolizaCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
        <TR>
            <TD><LABEL ID=9380><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct")%></TD>
        <TR>
        </TR>
			<TD><LABEL ID=9389><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
        </TR>
        <TR> 
			<TD><LABEL ID=9388><%= GetLocalResourceObject("nPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("nPolicy", 10,  ,  , GetLocalResourceObject("nPolicyToolTip"))%></TD>
			<TD>&nbsp;</TD>
		</TR>

		<TR>
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40667><a NAME="Cliente"><%= GetLocalResourceObject("AnchorClienteCaption") %></a></LABEL></td>
		</TR>
		<TR>
			<TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TR>
			    <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
			    <TD WIDTH="30%"><%=mobjValues.ClientControl("dtcClient", "",  , GetLocalResourceObject("dtcClientToolTip"),  ,  , "lblCliename", True)%></TD>
			    <TD WIDTH="50%"><%=mobjValues.DIVControl("lblCliename", False, "")%>&nbsp;</TD>
			</TR>        
			<TD>&nbsp;</TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>




