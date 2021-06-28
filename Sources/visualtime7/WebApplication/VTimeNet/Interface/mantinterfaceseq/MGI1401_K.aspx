<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Object for the handling of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Object for the handling of the areas of the page
'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MGI1401"
%>
<html>
<head>
	<meta NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></script>


<script>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 4 $|$$Date: 31/10/03 17:16 $"
//------------------------------------------------------------------------------------------

//% insStateZone: updates the status of the fields in the page
//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
    with (document.forms[0]) {
		cbnsystem.disabled=false;
        tcnsheet.disabled=false;
        btntcnsheet.disabled=false;
        tcsdescript.disabled=false;
        tcsshortdesc.disabled=false;
        optnintertype[0].disabled=false;
        optnintertype[1].disabled=false;
        optnintertype.disabled=false;
        cbeOpertype.disabled=false;
        tcsprocess.disabled=false;
        cbeFormat.disabled=false;
        chksautomatic.disabled=false;
        chksonline.disabled=false;
        chksgroupby.disabled=false;
    }
}

//% insCancel: It executes necessary routines at the moment for cancelling the page
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return true
}

function InsChangeField(obj,action){
	if ((self.document.forms[0].tcnsheet.value) != "" ){
		insDefValues("MasterSheet","nSheet=" + self.document.forms[0].tcnsheet.value,'/VTimeNet/Interface/mantinterfaceseq');
	}
}

//% InsChangeField: Limpia y habilita Campo Periodicidad segun si es Automatica o No.
//------------------------------------------------------------------------------------------
function InsChangeField2(vObj){
	with (self.document.forms[0]){
		cbePeriod.disabled = vObj.checked == '0';
	    cbePeriod.value='';
 	}
}

</script>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("MGI1401", "MGI1401_k.aspx", 1, vbNullString))
Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
mobjMenu = Nothing
%>    
</head>
	<body ONUNLOAD="closeWindows();">
		<form METHOD="POST" ID="FORM" NAME="MGI1401" ACTION="valmantinterfaceseq.aspx?smode=2">
			<br><br>
			<table WIDTH="100%" border=0>
				<tr>
					<td><label><%= GetLocalResourceObject("tcnsheetCaption") %></label></td>
					<td><%mobjValues.Parameters.add("NINTERTYPE", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.add("NSYSTEM", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nOpertype",  ,  , True)
mobjValues.Parameters.ReturnValue("sOpertype",  ,  , True)
mobjValues.Parameters.ReturnValue("nFormat",  ,  , True)
mobjValues.Parameters.ReturnValue("sFormat",  ,  , True)
Response.Write(mobjValues.PossiblesValues("tcnsheet", "TABTABLEMASTERSHEET", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "InsChangeField(this.value,0)", True,  , GetLocalResourceObject("tcnsheetToolTip"),  , 14,  , True))%></td>
					<td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("tcsdescriptCaption") %></label></td>
					<td colspan="2"><%=mobjValues.TextControl("tcsdescript", 30,  ,  , GetLocalResourceObject("tcsdescriptToolTip"),  ,  ,  ,  , True)%></td>
				</tr>
				<tr>
					<td><label><%= GetLocalResourceObject("tcsshortdescCaption") %></label></td>
					<td><%=mobjValues.TextControl("tcsshortdesc", 8,  ,  , GetLocalResourceObject("tcsshortdescToolTip"),  ,  ,  ,  , True)%></td>
					<td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("AnchorCaption") %></label></td>
					<td>
						<%=mobjValues.OptionControl(0, "optnintertype", GetLocalResourceObject("optnintertype_1Caption"), CStr(1), "1",  , True)%>
					    <%=mobjValues.OptionControl(0, "optnintertype", GetLocalResourceObject("optnintertype_2Caption"),  , "2",  , True)%>
					</td>
				</tr>
				<tr>
					<td><label><%= GetLocalResourceObject("cbeOpertypeCaption") %></label></td>
					<td><%=mobjValues.PossiblesValues("cbeOpertype", "Table5700", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOpertypeToolTip"),  , 14)%></td>
					<td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("tcsprocessCaption") %></label></td>
					<td><%=mobjValues.TextControl("tcsprocess", 30,  ,  , GetLocalResourceObject("tcsprocessToolTip"),  ,  ,  ,  , True)%></td>
				</tr>
				<tr><td><label><%= GetLocalResourceObject("cbeFormatCaption") %></label></td>
					<td><%=mobjValues.PossiblesValues("cbeFormat", "Table5701", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeFormatToolTip"),  , 14)%></td>
					<td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("cbnsystemCaption") %></label></td>
					<td><%=mobjValues.PossiblesValues("cbnsystem", "Table5705", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbnsystemToolTip"),  , 14)%></td>
				</tr>
				<tr>
					<td><label><%= GetLocalResourceObject("Anchor2Caption") %></label></td>
					<td><%=mobjValues.CheckControl("chksautomatic", "", CStr(2), "1", "InsChangeField2(this)", True)%></td>
					<td>&nbsp;</td>
					<td><label><%= GetLocalResourceObject("Anchor3Caption") %></label></td>
					<td><%=mobjValues.CheckControl("chksonline", "", CStr(2), "1",  , True)%></td>
				</tr>
				<tr>
					<td><label><%= GetLocalResourceObject("Anchor4Caption") %></label></td>
					<td><%=mobjValues.CheckControl("chksgroupby", "", CStr(2), "1",  , True)%></td>				
					<td>&nbsp;</td>
				    <td><label><%= GetLocalResourceObject("cbePeriodCaption") %></label></td>
					<td><%=mobjValues.PossiblesValues("cbePeriod", "Table5710", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePeriodToolTip"),  , 14)%></td>
				</tr>								
			</table>
		</form>
	</body>
</html>

<%
mobjValues = Nothing
%>




