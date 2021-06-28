<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues



'%insPreAGL620:Se cargan los controles de la ventana
'----------------------------------------------------------------------------
Private Sub insPreAGL620()
	'----------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40006>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"" COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""60%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>                                                                    " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), "1", "1"))


Response.Write(" </TD>           " & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>        " & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_1Caption"), CStr(1), "1", "SetTyp(1);"))
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("		    <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"),  , "2"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>    			" & vbCrLf)
Response.Write("            <TD>")

	Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_2Caption"),  , "2", "SetTyp(2);"))
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40006>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"" COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""60%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTyp_Proc", GetLocalResourceObject("optTyp_Proc_CStr1Caption"), CStr(1), CStr(1), "ChangeOptTyp_Proc()"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""17%"" COLSPAN=""1""><LABEL ID=0>" & GetLocalResourceObject("valInterm_TypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">")

	
	mobjValues.Parameters.Add("optTyp", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valInterm_Typ", "TABINTERM_TYPVENSUP", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "AssigParam(this)",  ,  , GetLocalResourceObject("valInterm_TypToolTip")))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTyp_Proc", GetLocalResourceObject("optTyp_Proc_CStr2Caption"), CStr(False), CStr(2), "ChangeOptTyp_Proc()"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.Parameters.Add("nInterTyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.ReturnValue("dCommidate", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valIntermedia", "TabIntermedia1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "Disabled()",  , 10, GetLocalResourceObject("valIntermediaToolTip")))
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40006><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodoCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Liquidación"">" & GetLocalResourceObject("AnchorLiquidaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""1""></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", vbNullString,  , GetLocalResourceObject("tcdEffecdateToolTip")))


Response.Write("" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateValCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdateVal", CStr(Today),  , GetLocalResourceObject("tcdEffecdateValToolTip")))


Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateEndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdateEnd", "",  , GetLocalResourceObject("tcdEffecdateEndToolTip")))


Response.Write("" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD COLSPAN = 1><LABEL ID=0>" & GetLocalResourceObject("tcnPay_commCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN = 1>")


Response.Write(mobjValues.NumericControl("tcnPay_comm", 10, "",  , GetLocalResourceObject("tcnPay_commToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("" & vbCrLf)
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
Call mobjNetFrameWork.BeginPage("agl620_k")
'~End Header Block VisualTimer Utility
Response.Cache.SetCacheability(HttpCacheability.NoCache)
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjValues.sCodisplPage = "AGL620_K"
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
//------------------------------------------------------------------------------------------
function ChangeOptTyp_Proc(){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){                
        if (optTyp_Proc[0].checked){   
			optTyp_Proc_Aux.value         = 1;
            valInterm_Typ.disabled        = false;
            btnvalInterm_Typ.disabled     = false;
            tcdEffecdate.disabled         = false;
            btn_tcdEffecdate.disabled     = false;
            tcdEffecdateVal.disabled      = false;
            btn_tcdEffecdateVal.disabled  = false;
            tcdEffecdateEnd.disabled      = false;
            btn_tcdEffecdateEnd.disabled  = false;
		    optTyp[0].disabled            = false;
		    optTyp[1].disabled            = false;
            valIntermedia.disabled        = false;
            btnvalIntermedia.disabled     = false;
tcdEffecdateVal.value         = '<% %>
<%=mobjValues.DateToString(Today)%>'
            tcnPay_comm.disabled          = true;
        }
        else{
			optTyp_Proc_Aux.value         = 2;
            valInterm_Typ.value           = '';
			valIntermedia.value           = '';            
		    UpdateDiv('valInterm_TypDesc', '');
		    UpdateDiv('valIntermediaDesc', '');
		    tcdEffecdate.value			  = '';
		    tcdEffecdateEnd.value         = '';
		    tcdEffecdateVal.value         = '';
//		    optTyp[0].checked             = true;
		    optTyp[0].disabled            = false;
		    optTyp[1].disabled            = false;
            valInterm_Typ.disabled        = true;
            btnvalInterm_Typ.disabled     = true;
            tcdEffecdate.disabled         = true;
            btn_tcdEffecdate.disabled     = true;
            tcdEffecdateEnd.disabled      = true;
            btn_tcdEffecdateEnd.disabled  = true;
            valIntermedia.disabled        = true;
            btnvalIntermedia.disabled     = true;
            btn_tcdEffecdate.disabled     = true;
            tcdEffecdateVal.disabled      = true;
            btn_tcdEffecdateVal.disabled  = true;
            tcnPay_comm.disabled          = false;
            tcnPay_comm.focus();
		}
	}	
}

//% Disabled: Habilita / Deshabilita campos
//-------------------------------------------------------------------------------------------
function Disabled(){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		if (valIntermedia.value > 0){
			tcdEffecdate.disabled = true;
			tcnPay_comm.disabled = true;

			tcdEffecdate.value = valIntermedia_dCommidate.value;
			tcdEffecdateProc.value = valIntermedia_dCommidate.value;
			if(valIntermedia_dCommidate.value=='<%=eRemoteDB.Constants.dtmNull%>' ||
			   valIntermedia_dCommidate.value=='0.00.00' ||
			   valIntermedia_dCommidate.value=='12:00:00 a.m.'){
				tcdEffecdate.value='';
				tcdEffecdateProc.value='';
				tcdEffecdate.disabled = false;
			}
		}
		else{
			if(valInterm_Typ.value==''){
				tcdEffecdate.value='';
				tcdEffecdateProc.value='';
				tcdEffecdate.disabled = false;
			}
			else
				insDefValues('LastProcess_date', 'sValue=AGL620&nInterTyp=' + valInterm_Typ.value  + '&optTyp=' + optTyp[0].checked,'/VTimeNet/Agent/AgentRep');
		}
	}
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

//+ Se busca la fecha de última ejecución del proceso
		if(valIntermedia.value=='')
			if(lintInterTyp==0){
				tcdEffecdate.disabled = false;
				tcdEffecdate.value = '';
				tcdEffecdateProc.value='';
			}
			else
				insDefValues('LastProcess_date', 'sValue=AGL620&nInterTyp=' + lintInterTyp  + '&optTyp=' + optTyp[0].checked,'/VTimeNet/Agent/AgentRep');
	}
}

// SetTyp: Asigna la opcion seleccionada al parametro del package de tipo de  
//         intermediario.
//-----------------------------------------------------------------------------------
function SetTyp(Field){
//-----------------------------------------------------------------------------------
	with (self.document.forms[0])
		valInterm_Typ.Parameters.Param1.sValue=Field;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("AGL620", "AGL620.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>

</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRIntermAccount" ACTION="ValAgentRep.aspx?mode=1">
<%
Call insPreAGL620()
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("AGL620_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




