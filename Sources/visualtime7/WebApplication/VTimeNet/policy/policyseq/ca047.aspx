<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

Dim mclsProduct As eProduct.Product

Dim mdtmStayDate As Object
Dim mintTypeSolic As Byte


'%insPreCA047: Esta función se encarga de validar los datos introducidos en la zona de
'%contenido para "frame" especifico.
'--------------------------------------------------------------------------------------------
Private Function insPreCA047() As Boolean
	'--------------------------------------------------------------------------------------------
	insPreCA047 = True
	Call insLoadCA047()
End Function

'%insLoadCA047: Esta función carga los valores almacenados en policy.
'--------------------------------------------------------------------------------------------
Private Function insLoadCA047() As Object
	'--------------------------------------------------------------------------------------------
	Dim lrecPolicy As ePolicy.Policy
	lrecPolicy = New ePolicy.Policy
	
	Dim lobjValues As eFunctions.Values
	lobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	lobjValues.sSessionID = Session.SessionID
	lobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lobjValues.sCodisplPage = "CA047"
	
	If lrecPolicy.Find_DateMax_Type_Doc(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), 2), mobjValues.StringToType(Session("nProduct"), 2), mobjValues.StringToType(Session("nPolicy"), 3)) Then
		If lrecPolicy.dMaximum_da = eRemoteDB.Constants.dtmNull Then
			mdtmStayDate = insCalcStayDefault()
		Else
			mdtmStayDate = lrecPolicy.dMaximum_da
		End If
		If lrecPolicy.sType_prop Is System.DBNull.Value Then
		Else
			Select Case lrecPolicy.sType_prop
				Case "1"
					mintTypeSolic = 1
				Case "2"
					mintTypeSolic = 2
				Case Else
					mintTypeSolic = 1
			End Select
		End If
	End If
	lrecPolicy = Nothing
	lobjValues = Nothing
End Function

'--------------------------------------------------------------------------------------------
Private Function insCalcStayDefault() As Object
	'dim eRemoteDB.Constants.intNull As Integer
	'--------------------------------------------------------------------------------------------
	If mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		If Not mclsProduct.nQdays_pro = eRemoteDB.Constants.intNull Then
			insCalcStayDefault = Today
			insCalcStayDefault = DateAdd(Microsoft.VisualBasic.DateInterval.Day, mclsProduct.nQdays_pro, insCalcStayDefault)
		End If
	End If
	mclsProduct = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA047")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mclsProduct = New eProduct.Product

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


   	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "CA047", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows()">
<FORM METHOD="post" ID="FORM" NAME="CA047" ACTION="valPolicySeq.aspx?sMode=1">
    	<% Response.Write(mobjValues.ShowWindowsName("CA047", Request.QueryString.Item("sWindowDescript"))) %>
    <% Call insPreCA047()%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted">
				<LABEL ID=41006><A NAME="Tipo"><%= GetLocalResourceObject("AnchorTipoCaption") %></A></LABEL>
            </TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><HR></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>        
		<%If mintTypeSolic = 1 Then%>        
			<TR>
				<TD COLSPAN="2"><% Response.Write(mobjValues.OptionControl(41007, "optTypeSolic", GetLocalResourceObject("optTypeSolic_CStr1Caption"), CStr(1), CStr(1)))%></TD>
			</TR>
			<TR>
				<TD COLSPAN="3"><% Response.Write(mobjValues.OptionControl(41008, "optTypeSolic", GetLocalResourceObject("optTypeSolic_CStr0Caption"), CStr(0), CStr(0)))%></TD>
		<%Else%>     
			<TR>
				<TD COLSPAN="2"><% Response.Write(mobjValues.OptionControl(41009, "optTypeSolic", GetLocalResourceObject("optTypeSolic_CStr1Caption"), CStr(0), CStr(1)))%></TD>
			</TR>
			<TR>
				<TD COLSPAN="2"><% Response.Write(mobjValues.OptionControl(41010, "optTypeSolic", GetLocalResourceObject("optTypeSolic_CStr0Caption"), CStr(1), CStr(0)))%></TD>
		<%End If%>        
			<TD WIDTH="10%">&nbsp;</TD>
            <TD><LABEL ID=13154><%= GetLocalResourceObject("tcdStayDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdStayDate", mdtmStayDate,  , "")%></TD>
        </TR>
	</TABLE>

<%
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA047")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




