<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjQuery As eRemoteDB.Query
'- Variables para el el manejo de los datos
Dim ldtmCtrol_dateV As Object
Dim ldtmCtrol_date1V As Date
Dim ldtmCtrol_dateS As Date
Dim ldtmCtrol_date1S As Date


'%insPreAGL605:Se cargan los controles de la ventana
'----------------------------------------------------------------------------
Private Sub insPreAGL605()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("			")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))


Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		 <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Proceso"">" & GetLocalResourceObject("AnchorProcesoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("            <TD></TD> " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD> " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>  		" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=4>&nbsp</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40006>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=12%>" & vbCrLf)
Response.Write("                ")

	Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_1Caption"), CStr(1), "1", "SetTyp(1);"))
Response.Write("" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("valInterm_TypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=50%>")

	
	mobjValues.Parameters.Add("optTyp", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valInterm_Typ", "TABINTERM_TYPVENSUP", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valInterm_TypToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=25%></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=12%>" & vbCrLf)
Response.Write("				")

	Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_2Caption"),  , "2", "SetTyp(2);"))
Response.Write("" & vbCrLf)
Response.Write("			<TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", ldtmCtrol_dateV,  , GetLocalResourceObject("tcdEffecdateToolTip")))


Response.Write("" & vbCrLf)
Response.Write("            <TD WIDTH=25%>")


Response.Write(mobjValues.HiddenControl("tcdEffecdateProc", "ldtmCtrol_date1V"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=25%>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl605_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjValues.sCodisplPage = "agl605_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjQuery = New eRemoteDB.Query
End With
'Fecha de proceso para vendedores
If mobjQuery.OpenQuery("ctrol_date", "dEffecdate", "nType_Proce=35") Then
	ldtmCtrol_dateV = mobjValues.StringToType(mobjQuery.FieldToClass("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
Else
	ldtmCtrol_dateV = Today
End If
If mobjQuery.OpenQuery("ctrol_dateag", "dEffecdate", "nType_Proce=36") Then
	ldtmCtrol_date1V = mobjValues.StringToType(mobjQuery.FieldToClass("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
Else
	ldtmCtrol_date1V = Today
End If
'Fecha de proceso para supervisores
If mobjQuery.OpenQuery("ctrol_dateag", "dEffecdate", "nType_Proce=37") Then
	ldtmCtrol_dateS = mobjValues.StringToType(mobjQuery.FieldToClass("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
Else
	ldtmCtrol_dateS = Today
End If
If mobjQuery.OpenQuery("ctrol_dateag", "dEffecdate", "nType_Proce=76") Then
	ldtmCtrol_date1S = mobjValues.StringToType(mobjQuery.FieldToClass("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
Else
	ldtmCtrol_date1S = Today
End If
mobjQuery = Nothing
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 24/05/04 19:33 $"

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
// SetTyp: Asigna la opcion seleccionada al parametro del package de tipo de  
//         intermediario y limpia los campos asociados al tipo de intermediario.
//-----------------------------------------------------------------------------------
function SetTyp(Field){
//-----------------------------------------------------------------------------------
	with (self.document.forms[0]){
		valInterm_Typ.Parameters.Param1.sValue=Field;
		if(Field==1){
			tcdEffecdate.value = '<%=ldtmCtrol_dateV%>'
			tcdEffecdateProc.value = '<%=ldtmCtrol_date1V%>'
            valInterm_Typ.value = '';
		    UpdateDiv('valInterm_TypDesc', '');
		}
		else{
			tcdEffecdate.value = '<%=ldtmCtrol_dateS%>'
			tcdEffecdateProc.value = '<%=ldtmCtrol_date1S%>'
            valInterm_Typ.value = '';
		    UpdateDiv('valInterm_TypDesc', '');
		}
	}
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL605_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRIntermAccount" ACTION="ValAgentRep.aspx?mode=1">
<%
Call insPreAGL605()
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl605_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




