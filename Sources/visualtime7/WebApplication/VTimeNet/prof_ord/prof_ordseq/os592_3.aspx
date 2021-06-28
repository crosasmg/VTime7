<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim lobjTheft_risk As eClaim.Theft_risk
Dim lintwinprot As Integer
Dim lintsta_elecpub As Integer
Dim lintsta_elecpriv As Integer


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os592_3"
%>
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $"
</SCRIPT>	
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//% InsClickField: Se asigna valor de check
//---------------------------------------------------------------------------
function InsClickField(objField)
//---------------------------------------------------------------------------
{	
	if (objField.checked == true)
		objField.value = "1"
	else
		objField.value = "2"
}
</SCRIPT>
<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	'**+ Si se trata de una ventana que no forma parte del encabezado de la transacción colocar:
	Response.Write(mobjMenu.setZone(2, "OS592_3", "OS592_3.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS592_3" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName("OS592_3")%>
<%
lobjTheft_risk = New eClaim.Theft_risk

lobjTheft_risk.Find(Session("nServ_order"))

If lobjTheft_risk.nWinprot = eRemoteDB.Constants.intNull Then
	lintwinprot = 1
Else
	lintwinprot = lobjTheft_risk.nWinprot
End If

If lobjTheft_risk.nSta_elecpub = eRemoteDB.Constants.intNull Then
	lintsta_elecpub = 1
Else
	lintsta_elecpub = lobjTheft_risk.nSta_elecpub
End If

If lobjTheft_risk.nSta_elecpriv = eRemoteDB.Constants.intNull Then
	lintsta_elecpriv = 1
Else
	lintsta_elecpriv = lobjTheft_risk.nSta_elecpriv
End If
%>
    <TABLE WIDTH="100%" BORDER="0">
    <TR></TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeSector_typeCaption") %></LABEL></TD>
			<TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeSector_type", "Table5540", eFunctions.Values.eValuesType.clngComboType, CStr(lobjTheft_risk.nSector_type),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSector_typeToolTip"))%></TD>
            <TD ROWSPAN="2">
				<TABLE WIDTH="100%" BORDER=0>
					<TR>            
						<TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=0><A NAME="Protección de ventanas / tragaluces"><%= GetLocalResourceObject("AnchorProtección de ventanas / tragalucesCaption") %></A></LABEL></TD>            
					</TR>
					<TR>
						<TD WIDTH="10%"></TD>
					    <TD COLSPAN="2" CLASS="Horline"></TD>	    
					</TR>  
					<TR>
						<TD WIDTH="10%"></TD>
						<TD><%=mobjValues.OptionControl(0, "optwinprot", GetLocalResourceObject("optwinprot_1Caption"), CStr(2 - lintwinprot), "1")%>
							<%=mobjValues.OptionControl(0, "optwinprot", GetLocalResourceObject("optwinprot_2Caption"), CStr(3 - lintwinprot), "2")%>
							<%=mobjValues.OptionControl(0, "optwinprot", GetLocalResourceObject("optwinprot_3Caption"), CStr(4 - lintwinprot), "3")%>
						</TD>
					</TR>
				</TABLE>	
            </TD>
		</TR>
		<TR>	
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeLevel_sectorCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeLevel_sector", "Table5541", eFunctions.Values.eValuesType.clngComboType, CStr(lobjTheft_risk.nLevel_sector),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeLevel_sectorToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeLock_typeCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeLock_type", "Table5542", eFunctions.Values.eValuesType.clngComboType, CStr(lobjTheft_risk.nLock_type),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeLock_typeToolTip"))%></TD>
            <TD ROWSPAN="2">
				<TABLE WIDTH="100%" BORDER=0>            
					<TR>            
						<TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=0><A NAME="Tipo de alumbrado - Público"><%= GetLocalResourceObject("AnchorTipo de alumbrado - PúblicoCaption") %></A></LABEL></TD>            
					</TR>
					<TR>
						<TD WIDTH="10%"></TD>
					    <TD COLSPAN="2" CLASS="Horline"></TD>	    
					</TR>  
					<TR>
						<TD WIDTH="10%"></TD>
						<TD><%=mobjValues.OptionControl(0, "optsta_elecpub", GetLocalResourceObject("optsta_elecpub_1Caption"), CStr(2 - lintsta_elecpub), "1")%>
							<%=mobjValues.OptionControl(0, "optsta_elecpub", GetLocalResourceObject("optsta_elecpub_2Caption"), CStr(3 - lintsta_elecpub), "2")%>
							<%=mobjValues.OptionControl(0, "optsta_elecpub", GetLocalResourceObject("optsta_elecpub_3Caption"), CStr(4 - lintsta_elecpub), "3")%>
						</TD>
					</TR>	
				</TABLE>	
            </TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeprotec_typeCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.PossiblesValues("cbeprotec_type", "Table5543", eFunctions.Values.eValuesType.clngComboType, CStr(lobjTheft_risk.nprotec_type),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeprotec_typeToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcndist_polCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.NumericControl("tcndist_pol", 5, CStr(lobjTheft_risk.ndist_pol),  , GetLocalResourceObject("tcndist_polToolTip"))%></TD>
			<TD>        
				<TABLE WIDTH="100%" BORDER=0>                    
					<TR>
						<TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=0><A NAME="Tipo de alumbrado - Privado"><%= GetLocalResourceObject("AnchorTipo de alumbrado - PrivadoCaption") %></A></LABEL></TD>            
					</TR>
					<TR>
						<TD WIDTH="10%"></TD>
					    <TD COLSPAN="2" CLASS="Horline"></TD>	    
					</TR>  
					<TR>	
						<TD WIDTH="10%"></TD>
					    <TD><%=mobjValues.OptionControl(0, "optsta_elecpriv", GetLocalResourceObject("optsta_elecpriv_1Caption"), CStr(2 - lintsta_elecpriv), "1")%>
							<%=mobjValues.OptionControl(0, "optsta_elecpriv", GetLocalResourceObject("optsta_elecpriv_2Caption"), CStr(3 - lintsta_elecpriv), "2")%>
							<%=mobjValues.OptionControl(0, "optsta_elecpriv", GetLocalResourceObject("optsta_elecpriv_3Caption"), CStr(4 - lintsta_elecpriv), "3")%>
					    </TD>
					</TR>
				</TABLE>
			</TD>	
        </TR>
        </TABLE>
        <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="4" CLASS="HighLighted" ><LABEL ID=0><A NAME="Otros antecedentes"><%= GetLocalResourceObject("AnchorOtros antecedentesCaption") %></A></LABEL></TD>            
		</TR>
		<TR>
		    <TD COLSPAN="4" CLASS="Horline"></TD>	    
		</TR>  
        <TR>
	        <TD>
	        <%If lobjTheft_risk.sCorner = "1" Then
	Response.Write(mobjValues.CheckControl("chkcorner", GetLocalResourceObject("chkcornerCaption"), "1", "1", "InsClickField(this)"))
Else
	Response.Write(mobjValues.CheckControl("chkcorner", GetLocalResourceObject("chkcornerCaption"), "2", "2", "InsClickField(this)"))
End If
%>
			</TD>
			<TD WIDTH="45%">&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnnum_inhabCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnnum_inhab", 10, CStr(lobjTheft_risk.nNum_inhab), True, GetLocalResourceObject("tcnnum_inhabToolTip"))%></TD>
        </TR>
        <TR>
	        <TD>
	        <%If lobjTheft_risk.sUrban = "1" Then
	Response.Write(mobjValues.CheckControl("chkurban", GetLocalResourceObject("chkurbanCaption"), "1", "1", "InsClickField(this)"))
Else
	Response.Write(mobjValues.CheckControl("chkurban", GetLocalResourceObject("chkurbanCaption"), "2", "2", "InsClickField(this)"))
End If
%>
			</TD>
			<TD WIDTH="45%">&nbsp</TD>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("tcnnum_bedsCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnnum_beds", 10, CStr(lobjTheft_risk.nNum_beds),  , GetLocalResourceObject("tcnnum_bedsToolTip"))%></TD>
        </TR>
        <TR>
	        <TD>
	        <%If lobjTheft_risk.sserver = "1" Then
	Response.Write(mobjValues.CheckControl("chkserver", GetLocalResourceObject("chkserverCaption"), "1", "1", "InsClickField(this)"))
Else
	Response.Write(mobjValues.CheckControl("chkserver", GetLocalResourceObject("chkserverCaption"), "2", "2", "InsClickField(this)"))
End If
%>
			</TD>
		</TR>
    </TABLE>
<%
lobjTheft_risk = Nothing%>    
</FORM> 
</BODY>
</HTML>





