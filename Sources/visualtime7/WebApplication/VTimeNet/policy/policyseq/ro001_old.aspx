<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable para el manejo de la tabla 'Theft'
Dim mclsTheft As Object


'% insPreRO001: Realiza la lectura de los campos a mostrar en la forma RO001
'----------------------------------------------------------------------------------------------
Private Sub insPreRO001()
	'----------------------------------------------------------------------------------------------
	Call mclsTheft.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("RO001")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
'UPGRADE_NOTE: The 'ePolicy.Theft' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mclsTheft = Server.CreateObject("ePolicy.Theft")

mobjValues.ActionQuery = Session("bQuery")
Call insPreRO001()
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "RO001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRO001" ACTION="ValPolicySeq.aspx?x=1">
    <P ALIGN="Center">
    <LABEL ID=41046><A HREF="#Clasificación del riesgo"><%= GetLocalResourceObject("AnchorClasificación del riesgoCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
    <LABEL ID=41048><A HREF="#Vigilancia"><%= GetLocalResourceObject("AnchorVigilanciaCaption") %></A></LABEL>
    </P>

    <%Response.Write(mobjValues.ShowWindowsName("RO001", Request.QueryString.Item("sWindowDescript")))%>

    <TD>&nbsp;</TD>	
    <TABLE WIDTH="100%">    
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=41049><A NAME="Clasificación del riesgo"><%= GetLocalResourceObject("AnchorClasificación del riesgo2Caption") %></A></LABEL></TD>
		</TR>                             
        <TR>
		    <TD COLSPAN="4" CLASS="HorLine"></TD>
		</TR>      
		 <TR>
            <TD><LABEL ID=13433><%= GetLocalResourceObject("cbeUbicationCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeUbication", "Table239", 1, mclsTheft.nUbication,  ,  ,  ,  ,  ,  ,  ,  , "")%></TD>
            <TD><LABEL ID=13432><%= GetLocalResourceObject("cbeRiskClassCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeRiskClass", "table241", 1, mclsTheft.nRiskClass,  ,  ,  ,  ,  ,  ,  ,  , "")%></TD>
		 </TR>            
		 <TR>
            <TD><LABEL ID=13429><%= GetLocalResourceObject("cbeCategoryCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCategory", "Table233", 1, mclsTheft.nCategory,  ,  ,  ,  ,  ,  ,  ,  , "")%></TD>
            <TD><LABEL ID=13428><%= GetLocalResourceObject("valBussTrendCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valBussTrend", "Table1", 2, mclsTheft.nBussTrend,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBussTrendToolTip"))%></TD>
		 </TR>		 
	</TABLE>
	<TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=41050><A NAME="Vigilancia"><%= GetLocalResourceObject("AnchorVigilancia2Caption") %></A></LABEL></TD>
		</TR>                             
        <TR>
		    <TD COLSPAN="2">&nbsp;</TD>	
		    <TD COLSPAN="2"><HR></TD>
		</TR>		    		            		 
		 <TR>
			<TD WIDTH="40%"><LABEL ID=13431><%= GetLocalResourceObject("tcnInsuredCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.NumericControl("tcnInsured", 6, mclsTheft.nInsured,  , "",  , 0)%></TD>           
            <TD WIDTH="15%"><LABEL ID=13427><%= GetLocalResourceObject("tcnAreaCaption") %></LABEL></TD>            
            <TD WIDTH="15%"><%=mobjValues.NumericControl("tcnArea", 4, mclsTheft.nArea,  , "",  , 0)%></TD>
		</TR>            
		<TR>
            <TD><LABEL ID=13430><%= GetLocalResourceObject("tcnEmployeesCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnEmployees", 4, mclsTheft.nEmployees,  , "",  , 0)%></TD>
            <TD><LABEL ID=13434><%= GetLocalResourceObject("tcnVigilanceCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnVigilance", 4, mclsTheft.nVigilance,  , "",  , 0)%></TD>        
        </TR>
    </TABLE>
	<%
mobjValues = Nothing
mclsTheft = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("RO001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




