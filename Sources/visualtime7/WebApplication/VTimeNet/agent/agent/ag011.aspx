<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menu
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones de la clase Intermedia    
Dim mobjIntermedia As eAgent.Intermedia

'- Variable para el manejo de la fecha a mostrat por defecto.
Dim mdtmEffecdate As Object

'- Variable para el manejo de la fecha que almacena la máxima fecha del control de historia.
Dim mdtmInpdate As Date



'%insPreAG011: Esta función se encaga de obtener los datos de verificación y control del intermediario
'--------------------------------------------------------------------------------------------
Private Sub insPreAG011()
	'--------------------------------------------------------------------------------------------	
	Dim lclsIntermed_his As eAgent.Intermed_his
	
	mobjIntermedia = New eAgent.Intermedia
	lclsIntermed_his = New eAgent.Intermed_his
	
	Call mobjIntermedia.Find(mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble))
	mdtmInpdate = mobjIntermedia.dInpdate
	
	With lclsIntermed_his
		.nIntermed = mobjValues.StringToType(Session("nIntermed"), eFunctions.Values.eTypeData.etdDouble)
		If .ReaLastDateIntermed_his Then
			mdtmEffecdate = .dEffecdate
			If mdtmEffecdate > Today Then
				mdtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, mdtmEffecdate)
			Else
				mdtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, Today)
			End If
		Else
			mdtmEffecdate = DateAdd(Microsoft.VisualBasic.DateInterval.Day, 1, Today)
		End If
	End With
	lclsIntermed_his = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.15 $"        

//------------------------------------------------------------------------------------------
function PutParameterValue(nValue1,nValue2){
//------------------------------------------------------------------------------------------
	if(typeof(self.document.forms[0].elements["cbeAgency"])!='undefined'){
		self.document.forms[0].elements["cbeAgency"].Parameters.Param1.sValue = nValue1;
		self.document.forms[0].elements["cbeAgency"].Parameters.Param2.sValue = nValue2;
	}
}
</SCRIPT>    	



    <%=mobjValues.StyleSheet()%>
    <%=mobjMenu.setZone(2, "AG011", "AG011.aspx")%>
</HEAD>
<%
Call insPreAG011()
%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmIntermNull" ACTION="ValAgent.aspx?x=1">
    <P ALIGN="Center">
		<LABEL ID=40016><A HREF="#Anulación/Suspensión"><%= GetLocalResourceObject("AnchorAnulación/SuspensiónCaption") %></A></LABEL>
		<LABEL ID=40017><A HREF="#Datos de verificación"><%= GetLocalResourceObject("AnchorDatos de verificaciónCaption") %></A></LABEL>		
		<LABEL ID=40018><A HREF="#Control"><%= GetLocalResourceObject("AnchorControlCaption") %></A></LABEL>
    </P>
    <%=mobjValues.ShowWindowsName("AG011")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40019><A NAME="Anulación/Suspensión"><%= GetLocalResourceObject("AnchorAnulación/Suspensión2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8101><%= GetLocalResourceObject("gmdNullDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("gmdNullDate", mdtmEffecdate,  , GetLocalResourceObject("gmdNullDateToolTip"))%></TD>
            <TD><LABEL ID=8100><%= GetLocalResourceObject("cbeNullCodeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeNullCode", "Table163", eFunctions.Values.eValuesType.clngComboType)%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCircular_docCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCircular_doc", 6, "",  , GetLocalResourceObject("tcnCircular_docToolTip"))%></TD>
            <TD WIDTH="15%">&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40020><A NAME="Datos de verificación"><%= GetLocalResourceObject("AnchorDatos de verificación2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8095><%= GetLocalResourceObject("lblClientCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%=mobjValues.ClientControl("lblClient", mobjIntermedia.sClient,  , "",  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
        
            <TD><LABEL ID=0><%= GetLocalResourceObject("lblSupervisCaption") %></LABEL></TD>
            <%mobjValues.ActionQuery = True%>
			<TD><%=mobjValues.TextControl("lblSupervis", 5, mobjValues.TypetoString(mobjIntermedia.nSupervis, eFunctions.Values.eTypeData.etdDouble))%></TD>            
			<TD COLSPAN="2"><%=mobjValues.PossiblesValues("lblSupervisName", "tabintermedia_o", eFunctions.Values.eValuesType.clngComboType, CStr(mobjIntermedia.nSupervis))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("lblOfficeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("lblOffice", "table9", eFunctions.Values.eValuesType.clngComboType, CStr(mobjIntermedia.nOffice),  , True)%></TD>
            <TD><LABEL ID=8235><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
			<TD><%
mobjValues.Parameters.Add("nOfficeagen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(mobjIntermedia.nAgency), True,  ,  ,  ,  , "PutParameterValue(" & mobjIntermedia.nOffice & "," & mobjIntermedia.nOfficeAgen & ")",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
%>
			</TD>
		</TR>
		<TR>
            <TD><LABEL ID=8098><%= GetLocalResourceObject("lblInterTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nIntertyp", mobjIntermedia.nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("lblInterType", "tabInterm_typ_o", eFunctions.Values.eValuesType.clngComboType, CStr(mobjIntermedia.nIntertyp), True, True))
%>
			</TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40021><A NAME="Control"><%= GetLocalResourceObject("AnchorControl2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8096><%= GetLocalResourceObject("lblInputDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("lblInputDate", mobjValues.TypetoString(mobjIntermedia.dInpdate, eFunctions.Values.eTypeData.etdDate),  , "", True)%></TD>
			<%=mobjValues.HiddenControl("dtmInputDate", mobjValues.TypetoString(mdtmInpdate, eFunctions.Values.eTypeData.etdDate))%>
			<TD><LABEL ID=8099><%= GetLocalResourceObject("lblIntStatusCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("lblIntStatus", "table200", eFunctions.Values.eValuesType.clngComboType, CStr(mobjIntermedia.nInt_status),  , True)%></TD>
        </TR>
    </TABLE>
	<%Response.Write(mobjValues.BeginPageButton)%>	
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valIntermed.value='" & Session("nIntermed") & "';top.fraHeader.$('#valIntermed').change()</SCRIPT>")
Response.Write("<SCRIPT>PutParameterValue(" & mobjIntermedia.nOffice & "," & mobjIntermedia.nOfficeAgen & ");</script>")

mobjIntermedia = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>




