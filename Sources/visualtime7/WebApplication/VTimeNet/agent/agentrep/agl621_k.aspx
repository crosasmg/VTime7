<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues



'%insPreAGL621:Se cargan los controles de la ventana
'----------------------------------------------------------------------------
Private Sub insPreAGL621()
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40006>" & GetLocalResourceObject("AnchorPeríodoCaption") & "</LABEL></TD>" & vbCrLf)
        
Response.Write("            <TD WIDTH=""10%"" COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""60%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")
Response.Write(mobjValues.DateControl("tcdEffecdate", vbNullString,  , GetLocalResourceObject("tcdEffecdateToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""17%"" COLSPAN=""1""><LABEL ID=0>" & GetLocalResourceObject("valInterm_TypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">")

	
	mobjValues.Parameters.Add("optTyp", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valInterm_Typ", "TABINTERM_TYPVENSUP", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "AssigParam(this)",  ,  , GetLocalResourceObject("valInterm_TypToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateEndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")
Response.Write(mobjValues.DateControl("tcdEffecdateEnd", "",  , GetLocalResourceObject("tcdEffecdateEndToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nInterTyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("dCommidate", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valIntermedia", "TabIntermedia1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , ,  , 10, GetLocalResourceObject("valIntermediaToolTip")))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	Response.Write(mobjValues.HiddenControl("tcdEffecdateProc", ""))
	Response.Write(mobjValues.HiddenControl("optTyp_Proc_Aux", "1"))
	
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl621_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjValues.sCodisplPage = "AGL621_K"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 24/05/04 19:34 $"

//%insStateZone: 
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
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
//% AssigParam: asigna parámetros al campo "valIntermedia"
//-------------------------------------------------------------------------------------------
function AssigParam(Field){
//-------------------------------------------------------------------------------------------
	var lintInterTyp = Field.value;
	if(lintInterTyp=='')
		lintInterTyp = 0;

	with(self.document.forms[0]){
		valIntermedia.Parameters.Param1.sValue = lintInterTyp;
		valIntermedia.value='';
		tcdEffecdate.disabled = false;
		UpdateDiv('valIntermediaDesc', '');

	}
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("AGL621", "AGL621.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>

</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRIntermAccount" ACTION="ValAgentRep.aspx?mode=1">
<%
Call insPreAGL621()
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("AGL621_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




