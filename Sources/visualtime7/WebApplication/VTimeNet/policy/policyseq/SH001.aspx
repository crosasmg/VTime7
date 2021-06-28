<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues
Dim mblnShip As Boolean
Dim mclsShip As ePolicy.Ship


'%insPreSH001. Esta funcion se encarga de realizar la busqueda de los datos de cliente
'------------------------------------------------------------------------------------
Private Sub insPreSH001()
	'------------------------------------------------------------------------------------
	Dim lblnFound As Boolean
	mblnShip = False
	mclsShip = New ePolicy.Ship
	lblnFound = mclsShip.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sCodispl"))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
Call insPreSH001()
%>
<FORM METHOD="POST" ID="FORM" NAME="FRMSH001" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
  <TABLE WIDTH="100%" BORDER="0">
    <TR>
      <TD COLSPAN=8 CLASS="HighLighted">
      <LABEL ID=11568><A NAME="Embarcación"><%= GetLocalResourceObject("lblEmbarcacionCaption")%></A></LABEL>
      </TD>
    </TR>
    <TR>
	  <TD COLSPAN=8 CLASS="Horline"></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11569><%= GetLocalResourceObject("cbeShipUseCaption") %></LABEL></TD>
      <TD><%mobjValues.TypeOrder = 1
              Response.Write(mobjValues.PossiblesValues("cbeShipUse", "Table9100", eFunctions.Values.eValuesType.clngComboType, CStr(mclsShip.nShipUse), , , , , , , mblnShip, 4, GetLocalResourceObject("cbeShipUseToolTip")))%>
      </TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11570><%= GetLocalResourceObject("tctNameCaption") %></LABEL></TD>
      <TD COLSPAN=4><%=mobjValues.TextControl("tctName", 30, mclsShip.sName,  ,GetLocalResourceObject("tctNameToolTip"),  ,  ,  ,  , mblnShip)%></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11571><%= GetLocalResourceObject("cbeShipTypeCaption") %></LABEL></TD>
  	  <TD><%= mobjValues.PossiblesValues("cbeShipType", "Table9102", eFunctions.Values.eValuesType.clngComboType, CStr(mclsShip.nShipType), , , , , , , mblnShip, 4, GetLocalResourceObject("cbeShipTypeToolTip"))%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11572><%= GetLocalResourceObject("tctRegistCaption") %></LABEL></TD>
      <TD COLSPAN=4><%=mobjValues.TextControl("tctRegist", 10, mclsShip.sRegist,  ,GetLocalResourceObject("tctRegistToolTip"),  ,  ,  ,  , mblnShip)%></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11573><%= GetLocalResourceObject("valMaterialCaption") %></LABEL></TD>
  	  <TD><%= mobjValues.PossiblesValues("valMaterial", "Table9101", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsShip.nMaterial), , , , , , , mblnShip, 4, GetLocalResourceObject("valMaterialToolTip"))%></TD>
      <TD>&nbsp;</TD>
	  <TD><LABEL ID=11574><%= GetLocalResourceObject("tctColorCaption") %></LABEL></TD>
	  <TD><%= mobjValues.TextControl("tctColor", 15, mclsShip.sColor, , GetLocalResourceObject("tctColorToolTip"), , , , , mblnShip)%></TD>
	  <TD>&nbsp;</TD>
 	  <TD><LABEL ID=11575><%= GetLocalResourceObject("tctConstructorCaption") %></LABEL></TD>
	  <TD><%= mobjValues.TextControl("tctConstructor", 20, mclsShip.sConstructor, , GetLocalResourceObject("tctConstructorToolTip"), , , , , mblnShip)%></TD>
	</TR>
	<TR>
      <TD><LABEL ID=11576><%= GetLocalResourceObject("tcnConsYearCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnConsYear", 4, CStr(mclsShip.nConsYear), , GetLocalResourceObject("tcnConsYearToolTip"), , , , , , , mblnShip)%></TD>
	  <TD>&nbsp;</TD>
      <TD><LABEL ID=11577><%= GetLocalResourceObject("tcnEquivYearCaption") %></LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.NumericControl("tcnEquivYear", 4, CStr(mclsShip.nEquivYear), , GetLocalResourceObject("tcnEquivYearToolTip"), , , , , , , mblnShip)%></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11578><%= GetLocalResourceObject("tcdLastCareDateCaption") %></LABEL></TD>
      <TD><%= mobjValues.DateControl("tcdLastCareDate", CStr(mclsShip.dLastCareDate), , GetLocalResourceObject("tcdLastCareDateToolTip"), False, , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11579><%= GetLocalResourceObject("tctLastCarePlaceCaption") %></LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.TextControl("tctLastCarePlace", 30, mclsShip.sLastCarePlace,, GetLocalResourceObject("tctLastCarePlaceToolTip") , , , , , mblnShip)%></TD>
    </TR>
    <TR>
      <TD COLSPAN=8 CLASS="HighLighted"><LABEL ID=11580><A NAME="Dimensiones"><%= GetLocalResourceObject("Dimensiones") %></A></LABEL></TD>
    </TR>
    <TR>
	  <TD COLSPAN=8 CLASS="Horline"></TD>
    </TR>
	<TR>
      <TD><LABEL ID=11581><%= GetLocalResourceObject("tcnLengthCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnLength", 6, CStr(mclsShip.nLength), , GetLocalResourceObject("tcnLengthToolTip"), , 3, , , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11582><%= GetLocalResourceObject("tcnWatersCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnWaters", 6, CStr(mclsShip.nWaters), , GetLocalResourceObject("tcnWatersToolTip"), , 3, , , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11583><%= GetLocalResourceObject("tcnDepthCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnDepth", 6, CStr(mclsShip.nDepth), , GetLocalResourceObject("tcnDepthToolTip"), , 3, , , , , mblnShip)%></TD>
    </TR>
    <TR>
      <TD COLSPAN=8 CLASS="HighLighted"><LABEL ID=11584><A NAME="Motor"><%= GetLocalResourceObject("lblMotorCaption")%></A></LABEL></TD>
    </TR>
    <TR>
	  <TD COLSPAN=8 CLASS="Horline"></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11585><%= GetLocalResourceObject("tcnNumMotorsCaption") %></LABEL></TD>
      <TD><%=mobjValues.NumericControl("tcnNumMotors", 4, CStr(mclsShip.nNumMotors),  ,GetLocalResourceObject("tcnNumMotorsToolTip")	,  ,  ,  ,  ,  ,  , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11586><%= GetLocalResourceObject("tctModelMotorsCaption") %></LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.TextControl("tctModelMotors", 60, mclsShip.sModelMotors, , GetLocalResourceObject("tctModelMotorsToolTip"), , , , , mblnShip)%></TD>
    </TR>
     <TR>
      <TD><LABEL ID=11587><%= GetLocalResourceObject("tcnPowerCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnPower", 8, CStr(mclsShip.nPower), , GetLocalResourceObject("tcnPowerToolTip"), , 3, , , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11588><%= GetLocalResourceObject("tctSerialMotorsCaption") %></LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.TextControl("tctSerialMotors", 60, mclsShip.sSerialMotors, , GetLocalResourceObject("tctSerialMotorsToolTip"), , , , , mblnShip)%></TD>
    </TR>
    <TR>
      <TD COLSPAN=8 CLASS="HighLighted"><LABEL ID=11589><A NAME="Tonelajes"><%= GetLocalResourceObject("lblTonelajesCaption")%></A></LABEL></TD>
    </TR>
    <TR>
	  <TD COLSPAN=8 CLASS="Horline"></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11590><%= GetLocalResourceObject("tcnTRBCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnTRB", 6, CStr(mclsShip.nTRB), , GetLocalResourceObject("tcnTRBToolTip"), , 3, , , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11591><%= GetLocalResourceObject("tcnTRNCaption") %></LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.NumericControl("tcnTRN", 6, CStr(mclsShip.nTRN), , GetLocalResourceObject("tcnTRNToolTip"), , 3, , , , , mblnShip)%></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11592><%= GetLocalResourceObject("tcnCapacityCaption") %></LABEL></TD>
      <TD><%= mobjValues.NumericControl("tcnCapacity", 6, CStr(mclsShip.nCapacity), , GetLocalResourceObject("tcnCapacityToolTip"), , 3, , , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11593><%= GetLocalResourceObject("cbeUnitMesureCodeCaption") %></LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.PossiblesValues("cbeUnitMesureCode", "Table6013", eFunctions.Values.eValuesType.clngComboType, CStr(mclsShip.nUnitMesureCode), , , , , , , mblnShip, 4, GetLocalResourceObject("cbeUnitMesureCodeToolTip"))%></TD>
    </TR>
    <TR>
      <TD COLSPAN=8 CLASS="HighLighted"><LABEL ID=11594><A NAME="Datos Varios"><%= GetLocalResourceObject("lblDatosVariosCaption")%></A></LABEL></TD>
    </TR>
    <TR>
	  <TD COLSPAN=8 CLASS="Horline"></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11595><%= GetLocalResourceObject("tctSeaPortCaption") %></LABEL></TD>
      <TD><%= mobjValues.TextControl("tctSeaPort", 20, mclsShip.sSeaPort, , GetLocalResourceObject("tctSeaPortToolTip"), , , , , mblnShip)%></TD>
      <TD>&nbsp;</TD>
      <TD><LABEL ID=11596><%= GetLocalResourceObject("tctDotationCaption") %>   </LABEL></TD>
      <TD COLSPAN=4><%= mobjValues.TextControl("tctDotation", 50, mclsShip.sDotation, , GetLocalResourceObject("tctDotationToolTip"), , , , , mblnShip)%></TD>
    </TR>
    <TR>
      <TD><LABEL ID=11597><%= GetLocalResourceObject("tctActionZoneCaption") %></LABEL></TD>
      <TD COLSPAN=7><%= mobjValues.TextControl("tctActionZone", 100, mclsShip.sActionZone, , GetLocalResourceObject("tctActionZoneToolTip"), , , , , mblnShip)%></TD>
    </TR>
   </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
mclsShip = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








