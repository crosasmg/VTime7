<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As Object


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "co685_k"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
    <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\collection\collectiontra\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>



<SCRIPT LANGUAGE=JavaScript>
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
var nMainAction=top.frames['fraSequence'].plngMainAction

   if (nMainAction != 301) { 
    self.document.btntcnCollector.disabled = false;
    self.document.forms[0].tcnCollector.disabled = false;
    }
   else{
    self.document.btntcnCollector.disabled = true;
    self.document.forms[0].tcnCollector.disabled = true;
    }
    self.document.forms[0].tcnCollector.value='';
    UpdateDiv('tcnCollectorDesc','');
}
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<%Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CO685", "CO685_K.aspx", 1, vbNullString))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTabCollector" ACTION="valCollectionTra.aspx?">
<BR><BR>
<TABLE>
    <TR>
        <TD><LABEL ID=0><%= GetLocalResourceObject("tcnCollectorCaption") %></LABEL></TD>
	    <TD><%=mobjValues.PossiblesValues("tcnCollector", "tabcollector_o", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("tcnCollectorToolTip"),  ,  ,  , True)%></TD>
	    <TD COLSPAN=2>&nbsp;</TD>
    </TR>
</TABLE>
</FORM>
</BODY>
</HTML>





