<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


'%insPreAGL009:Se cargan los controles de la ventana
'--------------------------------------------------------------------------------------------------
Private Sub insPreAGL009()
	'--------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"" BORDER=0>  " & vbCrLf)
Response.Write("          <TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Proceso"">" & GetLocalResourceObject("AnchorProcesoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("            <TD></TD> " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD> 			" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD COLSPAN=""1"">")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("       </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD> 		         " & vbCrLf)
Response.Write("		</TR>	" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=""25%""><LABEL ID=0>" & GetLocalResourceObject("cbeInterm_typCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	With mobjValues
		'.List = "1,2,3,7,10" '"Agente directo/Contratante-Banco/Corredor/Sociedad de corretaje/Agentes libres"
		'.TypeList = 1 'Incluir
		'.BlankPosition = True
		Response.Write(mobjValues.PossiblesValues("cbeInterm_typ", "Interm_typ", 1, Session("nInterm_typ"),  ,  ,  ,  ,  , "insChange_AGL009(""cbeInterm_typ"", this)",  , 2, GetLocalResourceObject("cbeInterm_typToolTip"), 1))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeInsur_AreaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsur_Area", "Table5001", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valIntermediaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	mobjValues.Parameters.Add("nIntertyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("dCommidate", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valIntermedia", "TabIntermedia_Typ_o", 2,  , True,  ,  ,  ,  , "insChange_AGL009(""valIntermedia"", this)",  , 10, GetLocalResourceObject("valIntermediaToolTip"), 1))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdProcess_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdProcess_date",  ,  , GetLocalResourceObject("tcdProcess_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdValue_dateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdValue_date", CStr(Today), True, GetLocalResourceObject("tcdValue_dateToolTip")))


Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        
        
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPay_commCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnPay_comm", 10, "", False, GetLocalResourceObject("tcnPay_commToolTip"), False, 0, , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeType_SupportCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeType_Support", "Table5570", 1, , , , , , , , True, , GetLocalResourceObject("cbeType_SupportToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnDocSupportCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnDocSupport", 10, "", False, GetLocalResourceObject("tcnDocSupportToolTip"),False,0,,,,,True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        
Response.Write("	</TABLE>")

	
	Response.Write(mobjValues.HiddenControl("tcdEffecdateProc", vbNullString))
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl009_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl009_k"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 1/06/04 10:57 $|$$Author: Nvaplat9 $"

//% insChange_AGL009: se controla el cambio de valor en los campos de la página
//-------------------------------------------------------------------------------------------
function insChange_AGL009(Option, Field){
//-------------------------------------------------------------------------------------------
	var lintInterTyp;
	with(self.document.forms[0]){
		switch(Option){
			case "cbeInterm_typ":
				if(typeof(Field)!='undefined'){
					lintInterTyp = Field.value;
					if(lintInterTyp=='')
						lintInterTyp = 0;
					with(self.document.forms[0]){
						valIntermedia.Parameters.Param1.sValue = lintInterTyp;
						valIntermedia.value='';
						tcdProcess_date.disabled = false;
						UpdateDiv('valIntermediaDesc', '');
					
//+ Se busca la fecha de última ejecución del proceso
						if(valIntermedia.value=='')
							if(lintInterTyp==0){
								tcdProcess_date.disabled = false;
								tcdProcess_date.value = '';
								tcdEffecdateProc.value='';
							}
							else
								insDefValues('LastProcess_date', 'sValue=AGL009&nInterTyp=' + lintInterTyp,'/VTimeNet/Agent/AgentRep');
					}
				}
				break;

case "valIntermedia":

    if (valIntermedia.value > 0) {
        tcdProcess_date.value = valIntermedia_dCommidate.value;
        tcdEffecdateProc.value = valIntermedia_dCommidate.value;
        if (valIntermedia_dCommidate.value == '<%=eRemoteDB.Constants.dtmNull%>' ||
					   valIntermedia_dCommidate.value == '0.00.00' ||
					   valIntermedia_dCommidate.value == '00:00:00') {
            tcdProcess_date.value = '';
            tcdEffecdateProc.value = '';
        }

        tcnPay_comm.disabled = false;
        cbeType_Support.disabled = false;
        cbeType_Support.value = '1';
        tcnDocSupport.disabled = false;
    }
    else {
        tcnPay_comm.disabled = true;
        tcnPay_comm.value = '';
        cbeType_Support.disabled = true;
        cbeType_Support.value = '';
        tcnDocSupport.disabled = true;
        tcnDocSupport.value = '';


        if (cbeInterm_typ.value == '') {
            tcdProcess_date.value = '';
            tcdEffecdateProc.value = '';
        }
        else
            insDefValues('LastProcess_date', 'sValue=AGL009&nInterTyp=' + cbeInterm_typ.value, '/VTimeNet/Agent/AgentRep');
    }
    break;
		}
	}
}
//%insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
}
//%insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
	return true
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("AGL009", "AGL009_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="AGL009" ACTION="valAgentRep.aspx?sMode=1">
	<BR><BR><BR>
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
	<BR>
<%
Call insPreAGL009()
%>
</FORM>
</BODY>
</HTML>
 
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl009_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




