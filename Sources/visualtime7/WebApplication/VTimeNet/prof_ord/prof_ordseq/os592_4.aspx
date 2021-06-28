<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim lobjAdd_risk As eClaim.Add_risk


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os592_4"
%>
<HTML>
<HEAD>

<SCRIPT>
//+ Variable para el control de versiones

	document.VssVersion="$$Revision: 2 $|$$Date: 15/12/03 19:10 $"

//% insEnabledFields: Inhabilita los campos de la ventana 
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
//+ distancia al cauce mas cercano
		if(elements["chkriverbed"].checked==true)
			elements["tcndist_river"].disabled=false;
		else{
			elements["tcndist_river"].disabled=true;
			elements["tcndist_river"].value='';
		}
	//+ distancia al aeropuerto mas cercano
		if(elements["chkAirport"].checked==true)
			elements["tcndistair"].disabled=false;
		else{
			elements["tcndistair"].disabled=true;
			elements["tcndistair"].value='';
		}
	//+ distancia a la salida de mar mas cercana
		if(elements["chkSea"].checked==true)
			elements["tcndistsea"].disabled=false;
		else{
			elements["tcndistsea"].disabled=true;
			elements["tcndistsea"].value='';
		}
	}
}
//% InsClickField: Inhabilita algunos los campos de la ventana 
//-------------------------------------------------------------------------------------------------------------------
function InsClickField(objField){
//-------------------------------------------------------------------------------------------------------------------
	if (objField.checked == true)
		objField.value = "1"
	else
		objField.value = "2"
	with (self.document.forms[0]) {

	//+ distancia al cauce mas cercano
		if(elements["chkriverbed"].checked==true)
			elements["tcndist_river"].disabled=false;
		else{
			elements["tcndist_river"].disabled=true;
			elements["tcndist_river"].value='';
		}
	//+ distancia al aeropuerto mas cercano
		if(elements["chkAirport"].checked==true)
			elements["tcndistair"].disabled=false;
		else{
			elements["tcndistair"].disabled=true;
			elements["tcndistair"].value='';
		}
	//+ distancia a la salida de mar mas cercana
		if(elements["chkSea"].checked==true)
			elements["tcndistsea"].disabled=false;
		else{
			elements["tcndistsea"].disabled=true;
			elements["tcndistsea"].value='';
		}
	}
}
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



	<%

Response.Write(mobjValues.StyleSheet())
'**+ Si se trata de una ventana que no forma parte del encabezado de la transacción colocar:
Response.Write(mobjMenu.setZone(2, "OS592_4", "OS592_4.aspx"))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
%>
	</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS592_4" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<A NAME="BeginPage"></A>
    <P ALIGN="Center">
	    <LABEL><A HREF="#Sismo"><%= GetLocalResourceObject("AnchorSismoCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#Inundacion"><%= GetLocalResourceObject("AnchorInundacionCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#Actosterroristas"><%= GetLocalResourceObject("AnchorActosterroristasCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#roturacañerias"><%= GetLocalResourceObject("AnchorroturacañeriasCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#filtracionlluvias"><%= GetLocalResourceObject("AnchorfiltracionlluviasCaption") %></A></LABEL><LABEL> | </LABEL>
		<LABEL><A HREF="#otrosriesgos"><%= GetLocalResourceObject("AnchorotrosriesgosCaption") %></A></LABEL><LABEL> | </LABEL>
    </P>
<%=mobjValues.ShowWindowsName("OS592_4")%>
<%lobjAdd_risk = New eClaim.Add_risk
lobjAdd_risk.Find(Session("Nserv_order"))

%>
    <TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Sismo"><%= GetLocalResourceObject("AnchorSismo2Caption") %></A></LABEL></TD>
			<TD></TD>		
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Inundación"><%= GetLocalResourceObject("AnchorInundaciónCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN=2 CLASS="HorLine"></TD>		
			<TD></TD>		
			<TD COLSPAN=5 CLASS="HorLine"></TD>		
		</TR>		
  	    <TR>		
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeEarthquakeCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeEarthquake", "table5611", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.ncon_earthquake),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeEarthquakeToolTip")))%></TD>
			<TD></TD>		
			<TD><%=mobjValues.CheckControl("chkriverbed", GetLocalResourceObject("chkriverbedCaption"), lobjAdd_risk.sriverbed, lobjAdd_risk.sriverbed, "InsClickField(this)",  ,  , GetLocalResourceObject("chkriverbedToolTip"))%></TD>
            <TD><%=mobjValues.NumericControl("tcndist_river", 7, CStr(lobjAdd_risk.ndist_river),  , GetLocalResourceObject("tcndist_riverToolTip"),  , 2)%>
                  <LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>		
  	    <TR>		
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeDamageCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeDamage", "table5612", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.ndamage),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDamageToolTip")))%></TD>
			<TD></TD>		
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkInflu_risk", GetLocalResourceObject("chkInflu_riskCaption"), lobjAdd_risk.sInflu_risk, lobjAdd_risk.sInflu_risk, "InsClickField(this)",  ,  , GetLocalResourceObject("chkInflu_riskToolTip"))%></TD>
		</TR>		
  	    <TR>		
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeContainriskCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeContainrisk", "table5613", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.ncontainrisk),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeContainriskToolTip")))%></TD>
			<TD></TD>	
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeInundatCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeInundat", "table5614", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.ninundat),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInundatToolTip")))%></TD>

		</TR>		
		<TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Actos terroristas"><%= GetLocalResourceObject("AnchorActos terroristasCaption") %></A></LABEL></TD>
			<TD></TD>		
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Rotura de cañerías"><%= GetLocalResourceObject("AnchorRotura de cañeríasCaption") %></A></LABEL></TD>
		<TR>		
			<TD COLSPAN="2" CLASS="HorLine"></TD>		
			<TD></TD>		
			<TD COLSPAN="5" CLASS="HorLine"></TD>		
		</TR>		
		<TR>
	        <TD COLSPAN="2"><%=mobjValues.CheckControl("chkstratobj", GetLocalResourceObject("chkstratobjCaption"), lobjAdd_risk.sstratobj, lobjAdd_risk.sstratobj, "InsClickField(this)",  ,  , GetLocalResourceObject("chkstratobjToolTip"))%></TD>
	        <TD></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbewaterpipeCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbewaterpipe", "table5596", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.nwaterpipe),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbewaterpipeToolTip")))%></TD>
		</TR>
		<TR>
		</TR>		
        <TR>
	        <TD COLSPAN="2"><%=mobjValues.CheckControl("chkterrefy", GetLocalResourceObject("chkterrefyCaption"), lobjAdd_risk.sterrefy, lobjAdd_risk.sterrefy, "InsClickField(this)",  ,  , GetLocalResourceObject("chkterrefyToolTip"))%></TD>
			<TD COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("cbeDam_waterpipeCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeDam_waterpipe", "table5612", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.ndam_waterpipe),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDam_waterpipeToolTip")))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL><A NAME="Filtración de lluvías"><%= GetLocalResourceObject("AnchorFiltración de lluvíasCaption") %></A></LABEL></TD>
			<TD></TD>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("cbesewerpipeCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbesewerpipe", "table5596", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.nsewerpipe),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbesewerpipeToolTip")))%></TD>
		</TR>
		<TR>
			<TD COLSPAN=2 CLASS="HorLine"></TD>
		</TR>
        <TR>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatroofCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeStatroof", "table5611", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.nStatroof),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatroofToolTip")))%></TD>
			<TD></TD>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("cbeDam_SewerpipeCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeDam_Sewerpipe", "table5612", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.ndam_sewerpipe),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDam_SewerpipeToolTip")))%></TD>
        </TR>
        <TR>
        	<TD><LABEL ID=0><%= GetLocalResourceObject("cbeDamageCaption") %></LABEL></TD>
	        <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeDamroof", "table5612", eFunctions.Values.eValuesType.clngComboType, CStr(lobjAdd_risk.nDamroof),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDamroofToolTip")))%></TD>
        </TR>
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL><A NAME="Otros riesgos"><%= GetLocalResourceObject("AnchorOtros riesgosCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN=5 CLASS="HorLine"></TD>		
		</TR>
        <TR>
            <TD><%=mobjValues.CheckControl("chkAirport", GetLocalResourceObject("chkAirportCaption"), lobjAdd_risk.sAirport, lobjAdd_risk.sAirport, "InsClickField(this)",  ,  , GetLocalResourceObject("chkAirportToolTip"))%></TD>
            <TD><%=mobjValues.NumericControl("tcndistair", 7, CStr(lobjAdd_risk.nDistair),  , GetLocalResourceObject("tcndistairToolTip"),  , 2)%>
                  <LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD></TD>
            <TD><%=mobjValues.CheckControl("chkStorm", GetLocalResourceObject("chkStormCaption"), lobjAdd_risk.sStorm, lobjAdd_risk.sStorm, "InsClickField(this)",  ,  , GetLocalResourceObject("chkStormToolTip"))%></TD>         
			<TD><%=mobjValues.CheckControl("chkSnow", GetLocalResourceObject("chkSnowCaption"), lobjAdd_risk.sSnow, lobjAdd_risk.sSnow, "InsClickField(this)",  ,  , GetLocalResourceObject("chkSnowToolTip"))%></TD>
		</TR>
        <TR>
            <TD><%=mobjValues.CheckControl("chkSea", GetLocalResourceObject("chkSeaCaption"), lobjAdd_risk.sSea, lobjAdd_risk.sSea, "InsClickField(this)",  ,  , GetLocalResourceObject("chkSeaToolTip"))%></TD>
            <TD><%=mobjValues.NumericControl("tcndistsea", 7, CStr(lobjAdd_risk.nDistsea),  , GetLocalResourceObject("tcndistseaToolTip"),  , 2)%>
                  <LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			<TD></TD>
            <TD><%=mobjValues.CheckControl("chkWind", GetLocalResourceObject("chkWindCaption"), lobjAdd_risk.sWind, lobjAdd_risk.sWind, "InsClickField(this)",  ,  , GetLocalResourceObject("chkWindToolTip"))%></TD>          
            <TD><%=mobjValues.CheckControl("chkFallplane", GetLocalResourceObject("chkFallplaneCaption"), lobjAdd_risk.sFallplane, lobjAdd_risk.sFallplane, "InsClickField(this)",  ,  , GetLocalResourceObject("chkFallplaneToolTip"))%></TD>
		</TR>
        <TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkShockauto", GetLocalResourceObject("chkShockautoCaption"), lobjAdd_risk.sShockauto, lobjAdd_risk.sShockauto, "InsClickField(this)",  ,  , GetLocalResourceObject("chkShockautoToolTip"))%></TD>
		</TR>
    </TABLE>
    <P ALIGN="Center"><%=mobjValues.BeginPageButton%></P>
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
	Response.Write("<SCRIPT>insEnabledFields();</script>")
End If
lobjAdd_risk = Nothing
mobjValues = Nothing
%>    
</FORM> 
</BODY>
</HTML>




