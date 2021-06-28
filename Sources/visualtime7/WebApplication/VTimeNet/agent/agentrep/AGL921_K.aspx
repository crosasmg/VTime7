<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjNetFrameWork As eNetFrameWork.Layout


'%insPreAGL921: Se cargan los controles de la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insPreAGL921()
	'--------------------------------------------------------------------------------------------
	Dim lclsCtrol_date As eGeneral.Ctrol_date
	Dim lclsT_com_prod As eAgent.ValAgentRep
	
	lclsCtrol_date = New eGeneral.Ctrol_date
	lclsT_com_prod = New eAgent.ValAgentRep
	
	Call lclsCtrol_date.Find(78)
	Call lclsT_com_prod.Find_FECUS_range()
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""20%""><LABEL ID=0>" & GetLocalResourceObject("tcdInit_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdInit_date", CStr(lclsT_com_prod.dMin_pay_date),  , GetLocalResourceObject("tcdInit_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEnd_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEnd_date", CStr(lclsCtrol_date.dEffecdate),  , GetLocalResourceObject("tcdEnd_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"), "insChangeAGL921('Client')"))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("valInterm_typCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nIntertyp", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("sClient", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("sInd_FECU", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("sGen_certif", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valInterm_typ", "tabInterm_typ_FECU", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insChangeAGL921(""Interm_typ"");",  ,  , GetLocalResourceObject("valInterm_typToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkGen_certif", GetLocalResourceObject("chkGen_certifCaption"), "1", "1", "insChangeAGL921(""Gen_certif"");",  ,  , GetLocalResourceObject("chkGen_certifToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkNom_certif", GetLocalResourceObject("chkNom_certifCaption"),  , "1",  ,  ,  , GetLocalResourceObject("chkNom_certifToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lclsCtrol_date = Nothing
	lclsT_com_prod = Nothing
End Sub

</script>
<%Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL921_K")

Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = "AGL921_K"
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 15/12/03 16:11 $|$$Author: Nvaplat18 $"

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//-------------------------------------------------------------------------------------------
function insChangeAGL921(Option){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(Option){
			case "Client":
				valInterm_typ.Parameters.Param2.sValue=dtcClient.value;
				chkGen_certif.checked=false;
				insDefValues('AGL921', 'sClient=' + dtcClient.value, '/VTimeNet/Agent/AgentRep');
				break;
			case "Gen_certif":
				dtcClient.value='';
				dtcClient_Digit.value='';
				valInterm_typ.value='';
				valInterm_typ.Parameters.Param2.sValue='';
				UpdateDiv('dtcClient_Name', '');
				UpdateDiv('valInterm_typDesc', '');
				break;
			case "Interm_typ":
				if(valInterm_typ.value=='')
					chkGen_certif.checked=(dtcClient.value=='');
				else
					chkGen_certif.checked=false
				break;
		}
	}
}

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
}

//%insCancel: Acciones a efectuar al cancelar la transacción.
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return true;
}

//%insFinish: Acciones a efectuar al finalizar la transacción.
//-------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>	
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL921_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	'+ Se agrega zona para dejar des-habilitado el botón aceptar
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="AGL921" ACTION="ValAgentRep.aspx?Mode=1">
<BR><BR><BR>
<%
Call insPreAGL921()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("AGL921_K")
mobjNetFrameWork = Nothing
%>
<SCRIPT LANGUAGE=JavaScript FOR=dtcClient EVENT=onchange>
	with(self.document.forms[0]){
		if(dtcClient.value==''){
		    valInterm_typ.Parameters.Param2.sValue=dtcClient.value;
		    valInterm_typ.value='';
		    UpdateDiv('valInterm_typDesc','','Normal');
		    chkGen_certif.checked=true;
		}
	}
</SCRIPT>





