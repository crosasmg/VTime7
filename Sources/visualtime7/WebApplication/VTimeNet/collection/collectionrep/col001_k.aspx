<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues.sCodisplPage = "col001_k"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
</SCRIPT>    
<SCRIPT>
// insStateZone :
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
    var lintIndex = 0;
    
    if (typeof(document.forms[0])!='undefined')
    {
        for (lintIndex=0;lintIndex<document.forms[0].elements.length;lintIndex++)
            document.forms[0].elements[lintIndex].disabled = false;
        document.images["btn_tcdInitDate"].disabled = false
		document.images["btn_tcdEndDate"].disabled = false
    }
}

// insChange :
//-----------------------------------------------------------------------------------
function insChange(field){
//-----------------------------------------------------------------------------------
 
 if (field.name != 'undefined'){
 
	with (self.document.forms[0]){
	
		if (field.name == 'optRecEmi' || field.name == 'optRecRen'){
	
			optRecTodos.checked = false;

			if ((optRecEmi.checked == true ) && (optRecRen.checked == true )){
				hddnRecOri.value = 3
			}
			else if (optRecEmi.checked == true ){
				hddnRecOri.value = 2
			}
			else if (optRecRen.checked == true ){
				hddnRecOri.value = 1
			}
			else{
				hddnRecOri.value = 0
			}
		}
		else{

			optRecEmi.checked = false;
			optRecRen.checked = false;
			if (optRecTodos.checked == true) {
				hddnRecOri.value  = 4
			}
			else{
				hddnRecOri.value  = 0
			}

		}
	}	
 }
}

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
    <%With Response
	mobjMenu = New eFunctions.Menues
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL001"))
	.Write(mobjMenu.MakeMenu("COL001", "COL001_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRCollectOperat" ACTION="valCollectionRep.aspx?mode=1">
<BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL001"))%>
    <%Response.Write(mobjValues.HiddenControl("hddnRecOri", CStr(0)))%>
    <TABLE WIDTH="100%">
	    <TR>
	        <TD COLSPAN="6">&nbsp;</TD>
	    </TR>
	    <TR>
	        <TD WIDTH="16%">&nbsp;</TD>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="16%">&nbsp;</TD>
	        <TD WIDTH="100%" COLSPAN="4"><HR></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="16%">&nbsp;</TD>
			<TD WIDTH="16%"><LABEL><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
            <TD WIDTH="16%"><%=mobjValues.DateControl("tcdInitDate", CStr(Today),  , GetLocalResourceObject("tcdInitDateToolTip"))%></TD>
			<TD WIDTH="16%"><LABEL><%= GetLocalResourceObject("tcdEndDateCaption") %></LABEL></TD>
            <TD WIDTH="16%"><%=mobjValues.DateControl("tcdEndDate", CStr(Today),  , GetLocalResourceObject("tcdEndDateToolTip"))%></TD>
            <TD WIDTH="16%">&nbsp;</TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="6">&nbsp;</TD>
	    </TR>
	    <TR>
	        <TD WIDTH="16%">&nbsp;</TD>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
	    </TR>
	    <TR>
	        <TD WIDTH="16%">&nbsp;</TD>
	        <TD COLSPAN="4"><HR></TD>
	    </TR>
	    <TR>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("optRecEmi", GetLocalResourceObject("optRecEmiCaption"), CStr(False), CStr(1), "insChange(this)")%>
			<TD><%=mobjValues.CheckControl("optRecRen", GetLocalResourceObject("optRecRenCaption"), CStr(False), CStr(2), "insChange(this)")%>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
	    </TR>
	    <TR>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%"><%=mobjValues.CheckControl("optRecTodos", GetLocalResourceObject("optRecTodosCaption"), CStr(True), CStr(3), "insChange(this)")%>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
	    </TR>
	    <TR>
	        <TD COLSPAN="6">&nbsp;</TD>
	    </TR>
	    <TR>
            <TD WIDTH="16%">&nbsp;</TD>
	        <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
	    </TR>
	    <TR>
            <TD WIDTH="16%">&nbsp;</TD>
	        <TD COLSPAN="4"><HR></TD>
            <TD WIDTH="16%">&nbsp;</TD>
            <TD WIDTH="16%">&nbsp;</TD>
	    </TR>
    </TABLE>
    <TABLE WIDTH="100%">
	    <TR>
            <TD WIDTH="30%">&nbsp;</TD>
			<TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD WIDTH="20%"><%=mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <TD WIDTH="20%">&nbsp;</TD>
            <TD WIDTH="20%">&nbsp;</TD>
	    </TR>
	    <TR>
            <TD WIDTH="20%">&nbsp;</TD>
            <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("cbeMovTypeCaption") %></LABEL></TD>
			<TD WIDTH="20%"><%=mobjValues.PossiblesValues("cbeMovType", "table6", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("cbeMovTypeToolTip"))%></TD>
            <TD WIDTH="20%">&nbsp;</TD>
            <TD WIDTH="20%">&nbsp;</TD>
	    </TR>
	    <TR>
            <TD WIDTH="20%">&nbsp;</TD>
            <TD WIDTH="20%"><LABEL><%= GetLocalResourceObject("cbeInfoOrderCaption") %></LABEL></TD>            
            <TD WIDTH="20%"><%=mobjValues.PossiblesValues("cbeInfoOrder", "table300", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , 10)%></TD>
            <TD WIDTH="20%">&nbsp;</TD>
            <TD WIDTH="20%">&nbsp;</TD>
	    </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




