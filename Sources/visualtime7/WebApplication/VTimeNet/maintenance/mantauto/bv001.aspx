<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se declara la variable para la carga de los datos en la forma
Dim mclsAuto_db As ePolicy.Auto_db


'% Obtiene los datos del automóvil
'%----------------------------------------------------------------------------------------
Private Sub insPreBV001()
	'%----------------------------------------------------------------------------------------
	Call mclsAuto_db.insPreBV001(Request.QueryString.Item("sLicense_ty"), Request.QueryString.Item("sRegist"))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsAuto_db = New ePolicy.Auto_db

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
Else
	mobjValues.ActionQuery = False
End If

mobjValues.sCodisplPage = "BV001"

Call insPreBV001()
%>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){
		case "Auto_db1":
			insDefValues(sField,"nVehCode=" + self.document.forms[0].valVehCode.value,'/VTimeNet/Maintenance/MantAuto')
			break;
	}
	with (self.document.forms[0]) {	
		if(valVehCode.value=='') {
			UpdateDiv("lblVehMark",'','Normal')
			UpdateDiv("lblVehModel",'','Normal')
			UpdateDiv("lblType",'','Normal')
			UpdateDiv("lblDSeat",'','Normal')
			UpdateDiv("lblTonMet",'','Normal')
		}		
	}
}   

//% ShowCapital: Se muestra el valor de auto según el año introducido
//-------------------------------------------------------------------------------------------
function ShowCapital(sField){
//-------------------------------------------------------------------------------------------
	switch(sField){
		case "Capital":
			insDefValues(sField,"nVehCode=" + self.document.forms[0].valVehCode.value + "&nYear=" + self.document.forms[0].tcnYear.value)
			break; 		
	}
}	

</SCRIPT>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "BV001", "BV001.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantAuto.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sLicense_ty=<%=Request.QueryString.Item("sLicense_ty")%>&sRegist=<%=Request.QueryString.Item("sRegist")%>&sMotor=<%=Request.QueryString.Item("sMotor")%>&sChasis=<%=Request.QueryString.Item("sChasis")%>&sDigit=<%=Request.QueryString.Item("sDigit")%>&nLic_special=<%=Request.QueryString.Item("nLic_special")%>">
	<%=mobjValues.ShowWindowsName("BV001")%>
	<TABLE WIDTH = "100%">
		<TR>
            <TD><LABEL><%= GetLocalResourceObject("tctVehownCaption") %></LABEL></TD>
            <TD COLSPAN = 3><%=mobjValues.ClientControl("tctVehown", mclsAuto_db.sVeh_own,  , GetLocalResourceObject("tctVehownToolTip"),  ,  ,  ,  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("valVehCodeCaption") %></LABEL></TD>
				<%mobjValues.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
            <TD><%=mobjValues.PossiblesValues("valVehCode", "tabTab_au_veh", 2, mclsAuto_db.sVehCode, True,  ,  ,  ,  , "ShowChangeValues(""Auto_db1"")",  , 6, GetLocalResourceObject("valVehCodeToolTip"))%></TD>
            <TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
				<%=mobjValues.DIVControl("lblVehMark", True, mclsAuto_db.sVehBrand)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>
				<%=mobjValues.DIVControl("lblVehModel", True, mclsAuto_db.sVehModel)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("tctColorCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctColor", 15, mclsAuto_db.sColor,  , GetLocalResourceObject("tctColorToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>            
            <TD><%=mobjValues.NumericControl("tcnYear", 5, CStr(mclsAuto_db.nYear),  , GetLocalResourceObject("tcnYearToolTip"),  , 0,  ,  ,  , "ShowCapital('Capital')")%></TD>
            <TD><LABEL><%= GetLocalResourceObject("cboVehstateCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboVehstate", "Table220", 1, CStr(mclsAuto_db.nVestatus),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cboVehstateToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL>
				<%Response.Write(mobjValues.DIVControl("lblType", True, mclsAuto_db.sVehType))
Response.Write(mobjValues.HiddenControl("hddType", ""))
%>
			</TD>
				
            <TD><LABEL><%= GetLocalResourceObject("tcnValueCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnValue", 18, CStr(mclsAuto_db.nValue),  , GetLocalResourceObject("tcnValueToolTip"), True, 6, False)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL>
				<%=mobjValues.DIVControl("lblDSeat", True)%></TD>
            <TD><LABEL><%= GetLocalResourceObject("Anchor5Caption") %></LABEL>
				<%=mobjValues.DIVControl("lblTonMet", True)%></TD>
        </TR>
        <TR>
            <TD><LABEL><%= GetLocalResourceObject("SCA2-MCaption") %></LABEL></TD>
            <TD COLSPAN = 3>
				<%mobjValues.sQueryString = "sLicense_ty=" & Request.QueryString.Item("sLicense_ty") & "&sRegist=" & Request.QueryString.Item("sRegist")
Response.Write(mobjValues.ButtonNotes("SCA2-M", mclsAuto_db.nNoteNum, False, mobjValues.ActionQuery,  ,  ,  ,  ,  , "btnNotenum"))
%>
			</TD>
		</TR>
    </TABLE>
    <%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 301 AndAlso Not String.IsNullOrEmpty(mclsAuto_db.sVehcode) Then
	Response.Write(("<SCRIPT>"))
	Response.Write(("insDefValues('Auto_db1','nVehCode=' + " & mclsAuto_db.sVehCode & ",'/VTimeNet/Maintenance/MantAuto');"))
	Response.Write(("</SCRIPT>"))
End If
mobjValues = Nothing
mclsAuto_db = Nothing
%>

</FORM>
</BODY>
</HTML>




