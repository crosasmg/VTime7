<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**-Objetive:
'-Objetivo:
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: Object for the handling of the general functions of load of values
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**-Objetive: Object for the handling of the generics routines
'-Objetivo: Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


'**%Objetive: The controls of the page are loaded
'%Objetivo: Se cargan los controles de la página
'----------------------------------------------------------------------------------------------------------------------
Private Sub insPreVIL7021()
	'----------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("		    <TD WIDTH=""5%""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de Proceso"">" & GetLocalResourceObject("AnchorTipo de ProcesoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""5%""></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de Selección"">" & GetLocalResourceObject("AnchorTipo de SelecciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=2 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=2 CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(0, "optPT", GetLocalResourceObject("optPT_CStr1Caption"), CStr(1), CStr(1), "insEnableFields(this)", True,  , GetLocalResourceObject("optPT_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD><TD></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optST", GetLocalResourceObject("optST_CStr1Caption"), CStr(1), CStr(1), "insEnableFields(this)", True,  , GetLocalResourceObject("optST_CStr1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD></TD>	" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optPT", GetLocalResourceObject("optPT_CStr2Caption"), CStr(2), CStr(2), "insEnableFields(this)", True,  , GetLocalResourceObject("optPT_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD><TD></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optST", GetLocalResourceObject("optST_CStr2Caption"), CStr(2), CStr(2), "insEnableFields(this)", True,  , GetLocalResourceObject("optST_CStr2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR></TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""5%""></TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnYearCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearToolTip")))


Response.Write("</TD>        		" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tctClientCaption") & "<LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ClientControl("tctClient", vbNullString,  , GetLocalResourceObject("tctClientToolTip"),  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""5%""></TD>" & vbCrLf)
Response.Write("			<TD><LABEL>" & GetLocalResourceObject("tcnAnnualcertifnrCaption") & "<LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnAnnualcertifnr", 10, vbNullString,  , GetLocalResourceObject("tcnAnnualcertifnrToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>		" & vbCrLf)
Response.Write("			<TD><TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub

</script>
<%
Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Visual TIME Templates">
    <%=mobjValues.StyleSheet()%>



    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//**+Objetive: This line keeps the version coming from VSS
//+Objeto: Esta línea guarda la versión procedente de VSS
//----------------------------------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 1 $|$$Date: 15/12/03 18:53 $$Author: Nvaplat34 $"
//----------------------------------------------------------------------------------------------------------------------

//**%Objetive: This function enable/disable the fields of the page
//%Objetivo: Habilita los campos de la forma
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
    if (typeof(document.forms[0])!='undefined'){
		with (self.document.forms[0]){
		    optST[0].disabled=false
		    optST[1].disabled=false
		    optPT[0].disabled=false
		    optPT[1].disabled=false
		    tctClient.disabled=false
		    tctClient_Digit.disabled=false
		    btntctClient.disabled=false
		}
    }
}

//**%Objetive: It allows to cancel the page
//%Objetivo: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//**%Objetive: It allows to finish the page
//%Objetivo: Permite finalizar la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
    return true;
}

//% EnableFields: Habilita / Deshabilita los campos de la ventana
//-------------------------------------------------------------------------------------------------
function insEnableFields(Field){
//-------------------------------------------------------------------------------------------------
    
    with (self.document.forms[0])
    {
		switch (Field.name)
		{
			case 'optST':
				if (optST[0].checked)
				{
				    tctClient.disabled=false
				    tctClient_Digit.disabled=false
				    btntctClient.disabled=false
				    if (optPT[1].checked)
				        tcnAnnualcertifnr.disabled=false
				    else
						tcnAnnualcertifnr.disabled=true
				}    
				else{
					tctClient.value=''
					tctClient_Digit.value=''
					UpdateDiv('tctClient_Name','','Normal')
					tcnAnnualcertifnr.value=''
					tctClient.disabled=true
					tctClient_Digit.disabled=true
					btntctClient.disabled=true
					tcnAnnualcertifnr.disabled=true
				}					
				break;	
			case 'optPT':
				if (optPT[1].checked && optST[0].checked)
					tcnAnnualcertifnr.disabled=false
				else
					tcnAnnualcertifnr.disabled=true
		}
    }
}
</SCRIPT>
<%
With Request
	
	mobjValues.ActionQuery = (.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
	
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
	mobjMenu = New eFunctions.Menues
	mobjMenu.sSessionID = Session.SessionID
	Response.Write(mobjMenu.MakeMenu(.QueryString.Item("sCodispl"), .QueryString.Item("sCodispl") & "_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	mobjMenu = Nothing
End With

%>
</HEAD>    
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="<%=Request.QueryString.Item("sCodispl")%>" ACTION="valpolicyrep.aspx?sZone=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))

insPreVIL7021()
mobjValues = Nothing

mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
%>
</FORM>
</BODY>
</HTML>




