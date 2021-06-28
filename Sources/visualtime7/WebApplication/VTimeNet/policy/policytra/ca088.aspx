<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQueryString As String

'- Objeto para el manejo de las rutinas genéricas
Dim mclsValPolicyTra As ePolicy.ValPolicyTra


</script>
<%Response.Expires = 0

'- Variables que contendrán la información que está en las variables de Sesión
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsValPolicyTra = New ePolicy.ValPolicyTra
End With

With Request
	mstrQueryString = "nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&nPolicy=" & .QueryString.Item("nPolicy") & "&nCertif=" & .QueryString.Item("nCertif")
End With
%>

<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.setZone(2, "CA088", "CA088.aspx"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmNullPolicy" ACTION="ValPolicyTra.aspx?<%=mstrQueryString%>">
<%
Response.Write(mobjValues.ShowWindowsName("CA088"))
With Request
	mclsValPolicyTra.insPreCA088(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
End With
%>
    <TABLE WIDTH="100%">
        <TR>
		<TD width="50%">
			<TABLE WIDTH="100%" border =0>
				<TR>
					<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID="0"><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
				</TR>
				<TR>
					<TD COLSPAN= "2" CLASS="HORLINE"></TD>
				</TR>
        		<TR>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tcdRecepIntCaption") %></LABEL></TD>
					<TD><LABEL ID=0><%=mobjValues.DateControl("tcdRecepInt", CStr(mclsValPolicyTra.dRecepInt),  , GetLocalResourceObject("tcdRecepIntToolTip"),  ,  ,  ,  , False)%><BR>
									<%=mobjValues.TextControl("lblDate1", 30, mclsValPolicyTra.sUser_dRecepInt,  , "", True)%></LABEL></TD>
				</TR>
        			<TR>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tcdRecepInt_CompCaption") %></LABEL></TD>
					<TD><%=mobjValues.DateControl("tcdRecepInt_Comp", CStr(mclsValPolicyTra.dRecepInt_Comp),  , GetLocalResourceObject("tcdRecepInt_CompToolTip"),  ,  ,  ,  , False)%><BR>
					    <%=mobjValues.TextControl("lblDate2", 30, mclsValPolicyTra.sUser_dRecepInt_Comp,  , "", True)%></TD>
				</TR>
			</TABLE>

		</TD>            
		<TD>
			<TABLE WIDTH="100%" border=0>
				<TR>
					<TD COLSPAN= "2" CLASS="HighLighted"><LABEL ID="0"><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
				</TR>
				<TR>
					<TD COLSPAN= "2" CLASS="HORLINE"></TD>
				</TR>
				<TR>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tcdRecepInsuCaption") %></LABEL></TD>
					<TD><%=mobjValues.DateControl("tcdRecepInsu", CStr(mclsValPolicyTra.dRecepInsu),  , GetLocalResourceObject("tcdRecepInsuToolTip"),  ,  ,  ,  , False)%><BR>
					    <%=mobjValues.TextControl("lblDate3", 30, mclsValPolicyTra.sUser_dRecepInsu,  , "", True)%></TD>
				</TR>
        		<TR>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tcdRecepInsu_CompCaption") %></LABEL></TD>
					<TD><%=mobjValues.DateControl("tcdRecepInsu_Comp", CStr(mclsValPolicyTra.dRecepInsu_Comp),  , GetLocalResourceObject("tcdRecepInsu_CompToolTip"),  ,  ,  ,  , False)%><BR>
					<%=mobjValues.TextControl("lblDate4", 30, mclsValPolicyTra.sUser_dRecepInsu_Comp,  , "", True)%></TD>
				</TR>
			</TABLE>
		</TD>            
	</TR>
    </TABLE>
	<%=mobjValues.HiddenControl("tcnNullOutMov", CStr(mclsValPolicyTra.nNullOutMov))%>
	<%=mobjValues.HiddenControl("tctReverCertif", mclsValPolicyTra.sReverCertif)%>
</FORM>
</BODY>
</HTML>
<%
mclsValPolicyTra = Nothing
mobjValues = Nothing
%>





