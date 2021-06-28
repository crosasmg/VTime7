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

%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("AGL008"))
Response.Write(mobjMenu.MakeMenu("AGL008", "AGL008_K.aspx", 1, ""))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<SCRIPT LANGUAGE="JavaScript"> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 26/08/04 13:14 $"

//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
//-----------------------------------------------------------------------------
function ChangeOption(){
//-----------------------------------------------------------------------------
with(self.document.forms[0]){

//si el área es vida, se deshabilita campo de traspaso de saldo
	if (cbeInsur_Area.value == 1)	
		chkSaldo.disabled = false		
	else
	{
	 chkSaldo.disabled = true		
     chkSaldo.checked =''
    }
    
    if (optProcess[0].checked)
        {       
         OptProcTyp[0].disabled = false
         OptProcTyp[1].disabled = false                  
        }
    else
        {
         OptProcTyp[0].disabled = true
         OptProcTyp[1].checked  = true         
         OptProcTyp[1].disabled = true
        }   
}
}
//-------------------------------------------------------------------------------------------
function insInitialFields(){
	document.forms["AGL008"].elements["tcdProcDat"].value = mdtmDateSystem
}	

//- Variable que almacena la fecha del sistema
	
	var mdtmDateSystem = GetDateSystem()

</SCRIPT>  
<FORM METHOD="post" ID="FORM" NAME="AGL008" ACTION="valAgentRep.aspx?Zone=1">
<BR><BR>
<TABLE WIDTH="100%">
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcdProcDatCaption") %></LABEL></TD>
        <TD><%=mobjValues.DateControl("tcdProcDat", Request.Form.Item("tcdProcDat"), True, GetLocalResourceObject("tcdProcDatToolTip"),  ,  ,  ,  ,  , 1)%></TD>
        <TD>&nbsp;</TD>
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_AreaCaption") %></LABEL></TD>
        <TD><% =mobjValues.PossiblesValues("cbeInsur_Area", "table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "ChangeOption()",  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip"),  , 2)%> </TD>
    </TR>
    <TR>
	    <TD WIDTH="50%" COLSPAN="2" CLASS="HighLighted" HEIGHT="25" VALING=TOP><LABEL ID=0><a NAME="Tipo de negocio a traspasar"><%= GetLocalResourceObject("AnchorTipo de negocio a traspasarCaption") %></a></LABEL></TD>
	    <TD>&nbsp;</TD>
        <TD WIDTH="40%" COLSPAN="2" CLASS="HighLighted" HEIGHT="25" VALING=TOP><LABEL ID=0><a NAME="Pólizas a traspasar"><%= GetLocalResourceObject("AnchorPólizas a traspasarCaption") %></a></LABEL></TD>				    
	</TR>
	<TR>
	    <TD COLSPAN="2" CLASS="HorLine"></TD>
	    <TD></TD>
        <TD COLSPAN="2" CLASS="HorLine"></TD>	    
	</TR>
	<TR>
	    <TD ROWSPAN="4" COLSPAN="2">
		    <%With Response
	.Write(mobjValues.OptionControl(0, "optBusiness", GetLocalResourceObject("optBusiness_1Caption"), "1", "1",  ,  , 3, GetLocalResourceObject("optBusiness_1ToolTip")))
	.Write(mobjValues.OptionControl(0, "optBusiness", GetLocalResourceObject("optBusiness_2Caption"), "0", "2",  ,  , 3, GetLocalResourceObject("optBusiness_2ToolTip")))
	.Write(mobjValues.OptionControl(0, "optBusiness", GetLocalResourceObject("optBusiness_3Caption"), "0", "3",  ,  , 3, GetLocalResourceObject("optBusiness_3ToolTip")))
	.Write(mobjValues.OptionControl(0, "optBusiness", GetLocalResourceObject("optBusiness_4Caption"), "0", "4",  ,  , 3, GetLocalResourceObject("optBusiness_4ToolTip")))
End With%>
		</TD>	
		<TD ROWSPAN="4">&nbsp;</TD>
		<TD ROWSPAN="4" COLSPAN="2">
    	    <%With Response
	.Write(mobjValues.OptionControl(0, "optPolicy", GetLocalResourceObject("optPolicy_1Caption"), "1", "1",  ,  , 4, GetLocalResourceObject("optPolicy_1ToolTip")))
	.Write(mobjValues.OptionControl(0, "optPolicy", GetLocalResourceObject("optPolicy_2Caption"), "0", "2",  ,  , 4, GetLocalResourceObject("optPolicy_2ToolTip")))
	.Write(mobjValues.OptionControl(0, "optPolicy", GetLocalResourceObject("optPolicy_3Caption"), "0", "3",  ,  , 4, GetLocalResourceObject("optPolicy_3ToolTip")))
End With%>		
		</TD>	
	</TR>
	<TR></TR>
	<TR></TR>
	<TR></TR>
	<TR></TR>
    <TR>
	    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeMunicipalityCaption") %></LABEL></TD>
        <TD><%=mobjValues.PossiblesValues("cbeMunicipality", "tabmunicipality", 1,  , False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMunicipalityToolTip"),  , 5)%></td>
        <TD>&nbsp;</TD>
		<TD><LABEL ID=0>
		<%With Response
	.Write(mobjValues.CheckControl("chkSaldo", GetLocalResourceObject("chkSaldoCaption"), "", "1",  , True,  , GetLocalResourceObject("chkSaldoToolTip")))
End With%>		
		</LABEL></td>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  ,  , False, 7)%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></td>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True,  ,  ,  ,  ,  , 8)%></TD>
		</TR>
        <TD ROWSPAN="4" COLSPAN="4">
        <TR>
			<TABLE WIDTH="100%">
			    <TR>
					<TD WIDTH="15%" CLASS="HighLighted" HEIGHT="25" VALING=TOP><LABEL ID=0><a NAME="Tipo de proceso"><%= GetLocalResourceObject("AnchorTipo de procesoCaption") %></a></LABEL></TD>
					<TD WIDTH="5%" >&nbsp;</TD>
					<TD WIDTH="15%" CLASS="HighLighted" HEIGHT="25" VALING=TOP><LABEL ID=0><a NAME="Opción de proceso"><%= GetLocalResourceObject("AnchorOpción de procesoCaption") %></a></LABEL></TD>					
					<TD WIDTH="5%" >&nbsp;</TD>
					<TD WIDTH="60%" COLSPAN="4" CLASS="HighLighted" HEIGHT="25" VALING=TOP><LABEL ID=0><a NAME="Intermediarios involucrados"><%= GetLocalResourceObject("AnchorIntermediarios involucradosCaption") %></a></LABEL></TD>
			    </TR>
				<TR>
				    <TD CLASS="HorLine"></TD>
					<TD></TD>
				    <TD CLASS="HorLine"></TD>
					<TD></TD>
					<TD COLSPAN="4" CLASS="HorLine"></TD>
				</TR>
				<TR>
				    <TD><%With Response
	.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1", "ChangeOption()",  , 6, GetLocalResourceObject("optProcess_1ToolTip")))
	.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"), "0", "2", "ChangeOption()",  , 6, GetLocalResourceObject("optProcess_2ToolTip")))
End With%>
					</TD>
					<TD>&nbsp;</TD>
				    <TD><%With Response
	.Write(mobjValues.OptionControl(0, "OptProcTyp", GetLocalResourceObject("OptProcTyp_1Caption"), "1", "1",  ,  , 6, GetLocalResourceObject("OptProcTyp_1ToolTip")))
	.Write(mobjValues.OptionControl(0, "OptProcTyp", GetLocalResourceObject("OptProcTyp_2Caption"), "0", "2",  ,  , 6, GetLocalResourceObject("OptProcTyp_2ToolTip")))
End With%>
					</TD>
					<TD>&nbsp;</TD>
					<TD WIDTH="12%"><LABEL ID=0><%= GetLocalResourceObject("valInterBeforeCaption") %></LABEL></TD>
		            <%mobjValues.Parameters.Add("NINTERTYP", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
					<TD ><%=mobjValues.PossiblesValues("valInterBefore", "tabintermedia2", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , False, 7, GetLocalResourceObject("valInterBeforeToolTip"),  , 9)%></TD>		
				<TR></TR>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD>&nbsp;</TD>
					<TD ><LABEL ID=0><%= GetLocalResourceObject("valInterNewCaption") %></LABEL></TD>
					<%mobjValues.Parameters.Add("NINTERTYP", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
					<TD ><%=mobjValues.PossiblesValues("valInterNew", "tabintermedia2", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , False, 7, GetLocalResourceObject("valInterNewToolTip"),  , 10)%></TD>							
				</TR>
			</TABLE>
		</TR>
    </TR>        
</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
Response.Write("<SCRIPT>insInitialFields()</SCRIPT>")
%>    






