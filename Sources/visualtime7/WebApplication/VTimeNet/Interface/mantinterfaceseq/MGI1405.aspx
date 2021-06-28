<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As Object
Dim mclsMasterSheep As eInterface.MasterSheet


'%insPreMGI1405_K: Esta función se encarga de cargar los datos en la forma "Folder" de la SEC1
'------------------------------------------------------------------------------
Private Sub insPreMGI1405_K()
	'------------------------------------------------------------------------------
	mclsMasterSheep = New eInterface.MasterSheet
	mclsMasterSheep.Find(session("nSheet"))
	
Response.Write("" & vbCrLf)
Response.Write("    <table WIDTH=""100%"" border=0>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("cbeStatusSheetCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.PossiblesValues("cbeStatusSheet", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsMasterSheep.sStatusSheet,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatusSheetToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctPrefix_fnameCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.TextControl("tctPrefix_fname", 9, mclsMasterSheep.sPrefix_fname,  , GetLocalResourceObject("tctPrefix_fnameToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctSeparatorCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.TextControl("tctSeparator", 1, mclsMasterSheep.sSeparator,  , GetLocalResourceObject("tctSeparatorToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctSpaceCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.TextControl("tctSpace", 1, mclsMasterSheep.sSpace,  , GetLocalResourceObject("tctSpaceToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("AnchorCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.OptionControl(0, "optnAling", GetLocalResourceObject("optnAling_1Caption"), CStr(mclsMasterSheep.nAling), "1"))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor2Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkHeader", "", mclsMasterSheep.sHeader))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<!--Segunda opción del tipo de alineación-->" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.OptionControl(0, "optnAling", GetLocalResourceObject("optnAling_2Caption"), CStr(mclsMasterSheep.nAling - 1), "2"))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor3Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkTotal", "", mclsMasterSheep.sTotal))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tcnPositionCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.NumericControl("tcnPosition", 1, CStr(mclsMasterSheep.nPosition),  , GetLocalResourceObject("tcnPositionToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor4Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkMassive", "", mclsMasterSheep.sMassive))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor5Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkNogrid", "", mclsMasterSheep.sNogrid))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor6Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkView_interface", "", mclsMasterSheep.sView_interface,  ,  , mclsMasterSheep.nIntertype <> 2))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor7Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkView_Report", "", mclsMasterSheep.sView_Report))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctReportCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.TextControl("tctReport", 50, mclsMasterSheep.sReport,  , GetLocalResourceObject("tctReportToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor8Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkSheet_father", "", mclsMasterSheep.sSheet_father))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor9Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkFile_unique", "", mclsMasterSheep.sFile_unique))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctName_routineCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.TextControl("tctName_routine", 12, mclsMasterSheep.sName_routine,  , GetLocalResourceObject("tctName_routineToolTip"),  ,  ,  ,  , mclsMasterSheep.nFormat <> 4))


Response.Write("</td>" & vbCrLf)
Response.Write("			<td>&nbsp;</td>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctOut_routineCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.TextControl("tctOut_routine", 12, mclsMasterSheep.sOut_routine,  , GetLocalResourceObject("tctOut_routineToolTip"),  ,  ,  ,  , mclsMasterSheep.nIntertype <> 2))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("Anchor10Caption") & "</label></td>" & vbCrLf)
Response.Write("			<td>")


Response.Write(mobjValues.CheckControl("chkXsl", "", mclsMasterSheep.sXsl,  ,  , mclsMasterSheep.nFormat <> 4))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctQueryCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td colspan=4>")


Response.Write(mobjValues.TextAreaControl("tctQuery", 5, 150, mclsMasterSheep.sQuery,  , GetLocalResourceObject("tctQueryToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctQuery_xslCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td colspan=4>")


Response.Write(mobjValues.TextAreaControl("tctQuery_xsl", 5, 150, mclsMasterSheep.sQuery_xsl,  , GetLocalResourceObject("tctQuery_xslToolTip")))


Response.Write("</td>" & vbCrLf)
Response.Write("		</tr>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctsworkflownameCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td colspan=4>")


Response.Write(mobjValues.TextAreaControl("tctsworkflowname", 2, 150, mclsMasterSheep.sWorkflowname,  , GetLocalResourceObject("tctsworkflownameToolTip"),  ,  ,  , "lengmax(this);"))


Response.Write("</td>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("		<tr>" & vbCrLf)
Response.Write("			<td><label>" & GetLocalResourceObject("tctsfolderCaption") & "</label></td>" & vbCrLf)
Response.Write("			<td colspan=4>")


Response.Write(mobjValues.TextAreaControl("tctsfolder", 2, 150, mclsMasterSheep.sFolder,  , GetLocalResourceObject("tctsfolderToolTip"),  ,  ,  , "lengmax(this);"))


Response.Write("</td>" & vbCrLf)
        Response.Write("		</tr>" & vbCrLf)

        Response.Write("		<tr>" & vbCrLf)
        Response.Write("		<tr>" & vbCrLf)
        Response.Write("			<td><label>" & GetLocalResourceObject("tctQueProcessCaption") & "</label></td>" & vbCrLf)
        Response.Write("			<td>")


        Response.Write(mobjValues.TextControl("tctQueProcess", 30, mclsMasterSheep.sQueProcess, , GetLocalResourceObject("tctQueProcessToolTip"), , , , , mclsMasterSheep.nIntertype = 2))


        Response.Write("</td>" & vbCrLf)
        Response.Write("			<td>&nbsp;</td>" & vbCrLf)
        Response.Write("			<td>&nbsp;</td>" & vbCrLf)
        Response.Write("			<td>&nbsp;</td>" & vbCrLf)
        Response.Write("		</tr>" & vbCrLf)
        Response.Write("		<tr>" & vbCrLf)
        Response.Write("			<td><label>" & GetLocalResourceObject("tctQueQueryCaption") & "</label></td>" & vbCrLf)
        Response.Write("			<td colspan=4>")


        Response.Write(mobjValues.TextAreaControl("tctQueQuery", 5, 150, mclsMasterSheep.sQueQuery, , GetLocalResourceObject("tctQueQueryToolTip"), , mclsMasterSheep.nIntertype = 2))


        Response.Write("</td>" & vbCrLf)
        Response.Write("		</tr>" & vbCrLf)
        
        
        
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	</table>")

	
	mclsMasterSheep = New eInterface.MasterSheet
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MGI1405"
%>
<html>
<head>
   <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
	 <%=mobjValues.WindowsTitle("MGI1405")%>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>


    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MGI1405", "MGI1405.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
<script>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31/10/03 17:16 $"
 
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}

//-------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
	
}

function lengmax(obj){
//------------------------------------------------------------------------------------------
	
	
	if (obj.value.length > 200 )
	{ 
	obj.value = obj.value.substring  (1,200);
	}
	
}
</script>		

</head>
<body ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>

<form METHOD="POST" ID="FORM" NAME="MGI1405" ACTION="valmantinterfaceseq.aspx?Type=<%=Request.QueryString.Item("Type")%>">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MGI1405"))
Call insPreMGI1405_K()
mobjValues = Nothing
%>
</form>
</body>
</html>




