<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "mca006_k"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Valfunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(nAction) {
//------------------------------------------------------------------------------------------
	with(document.forms[0]){
	    cbeSheet.disabled=false
	    if (nAction!="401") {
			optInf[0].disabled=false
			optInf[1].disabled=false
		}
	}
}

//% LockControl: Habilita/Deshabilita los controles dependientes de la página
//-------------------------------------------------------------------------------------------
function LockControl(){
//-------------------------------------------------------------------------------------------
	with(document.forms[0]){
		if(optInf[0].checked){
			cbeBranch.value=""
			cbeBranch.disabled=false
		}
		else
			cbeBranch.disabled=true
	}
}

//% insShowData: Muestra la información al consultar cuando se selecciona la hoja de Excel.
//-------------------------------------------------------------------------------------------
function insShowData(){
//-------------------------------------------------------------------------------------------
    if (top.frames['fraSequence'].plngMainAction!="301") 
		with(document.forms[0])
			if(cbeSheet.value!='')
				insDefValues("ShowDataMCA006", "sField=" + "getData" + "&nSheet=" +  cbeSheet.value);
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel() {
//------------------------------------------------------------------------------------------
	return true
}

//% insFinish: Ejecuta rutinas necesarias en el momento de finalizar la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return(true);
}
</SCRIPT>
	<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MCA006", "MCA006_K.aspx", 1, ""))
	
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MCA006" ACTION="valMantPolicy.aspx?x=1">
	<BR><BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH = 30% CLASS = "HIGHLIGHTED">
				<LABEL ID=101925><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
			</TD>
			<TD WIDTH = 15%>&nbsp;</TD>
			<TD><LABEL ID=101926><%= GetLocalResourceObject("cbeSheetCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeSheet", "Table697", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insShowData()", True, 10, GetLocalResourceObject("cbeSheetToolTip"))%></TD>
		</TR>
		<TR>
			<TD CLASS = "HORLINE"></TD>
		</TR>
		<TR>
			<TD><%=mobjValues.OptionControl(101930, "optInf", GetLocalResourceObject("optInf_1Caption"),  , "1", "LockControl();", True)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=101927><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranchToolTip"))%></TD>
		</TR>
		<TR></TR>
		<TR>
			<TD><%=mobjValues.OptionControl(101929, "optInf", GetLocalResourceObject("optInf_0Caption"), "1", "0", "LockControl();", True)%></TD>
			<TD>&nbsp;</TD>
		</TR>
	</TABLE>
</FORM>
</BODY>
</HTML>




