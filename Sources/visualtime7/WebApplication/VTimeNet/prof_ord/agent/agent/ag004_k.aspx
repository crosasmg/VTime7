<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.55
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
Call mobjNetFrameWork.BeginPage("ag004_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ag004_k"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AG004", Request.QueryString.Item("sWindowDescript")))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	.Write(mobjMenu.MakeMenu("AG004", "AG004_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write("<BR>")
End With
mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 6/10/03 19:20 $"        

// insStateZone : Inhabilita determinados campos de acuerdo a la acción en tratamiento.
//-----------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------
	self.document.forms[0].valIntermedia.disabled = false;
	self.document.btnvalIntermedia.disabled = false;
	self.document.forms[0].valIntermedia.focus()
}

// insCancel : Ejecuta la acción cancelar de la página.
//-----------------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------------
//+ Si la acción es registrar y nos encontramos en la zona 2, eliminamos la información del anticipo, al cancelar.
    if (top.fraSequence.plngMainAction == 301 &&
        self.document.forms[0].elements["valIntermedia"].value > 0 && 
        self.document.forms[0].elements["cbeLoanId"].value > 0) {
        insDefValues('delLoans', "Intermed=" + self.document.forms[0].valIntermedia.value + "&Loans=" + self.document.forms[0].cbeLoanId.value);
    }
    return(true);
}

// insDefValues : Valores por defecto
//-----------------------------------------------------------------------------------------
function insDefValues(sKey,sParameters,sPath){
//-------------------------------------------------------------------------------------------
    if (typeof(top)!='undefined')
        if (typeof(top.frames)!='undefined')
            if (typeof(top.frames["fraGeneric"])!='undefined'){
                sPath = (typeof(sPath)=='undefined'?'':sPath + '/')
                sParameters = (typeof(sParameters)=='undefined'?'':'&' + sParameters)
                top.frames["fraGeneric"].location.href = sPath + 'ShowDefValues.aspx?Field=' + sKey  + sParameters;
            }
}


// insFinish : Finalización de la página.
//-----------------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------------
    return(true);
}

// SetIntermedia: Establece el valor según el intermediario en selección para obtener
//                una lista de sus anticipos.
//-----------------------------------------------------------------------------------
function SetIntermedia(Field){
//-----------------------------------------------------------------------------------
    if (Field != ""){
        with(self.document.forms[0]){
			if (top.fraSequence.plngMainAction != 301){
			    cbeLoanId.Parameters.Param1.sValue=valIntermedia.value
			    self.document.forms[0].cbeLoanId.disabled = false;
			    self.document.forms[0].btncbeLoanId.disabled = false;
			}else{
			    self.document.forms[0].cbeLoanId.disabled = true;
			    self.document.forms[0].btncbeLoanId.disabled = true;
			}
    	    insDefValues('Interm_typ' ,'nIntermed=' + Field, "/VTimeNet/agent/agent");
		}
	}else{
        self.document.forms[0].cbeIntertyp.Parameters.Param1.sValue = Field;
	    self.document.forms[0].cbeIntertyp.value=Field;
	    $(self.document.forms[0].cbeIntertyp).change();
	    self.document.forms[0].cbeLoanId.value=Field;
	    $(self.document.forms[0].cbeLoanId).change();
	    self.document.forms[0].cbeLoanId.disabled = true;
	    self.document.forms[0].btncbeLoanId.disabled = true;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
    <FORM METHOD= "POST" ACTION="valAgent.aspx?TIMEINFO=1"ID=form1 NAME=form1>
	<BR>
        <TABLE WIDTH="100%">
        	<TR>
        		<TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
        		<TD WIDTH="90%"><%=mobjValues.PossiblesValues("valIntermedia", "tabintermedia", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , "SetIntermedia(this.value);", True, 10, GetLocalResourceObject("valIntermediaToolTip"))%></TD>
           </TR>
        </TABLE>
        <TABLE WIDTH="100%">
            <TR>
        		<TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("cbeIntertypCaption") %></LABEL></TD>
	 	        <TD WIDTH="30%"><%
mobjValues.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeIntertyp", "tabinterm_typ", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeIntertypToolTip"),  ,  ,  , True))
%></TD> 
        		<TD WIDTH="15%"><LABEL><%= GetLocalResourceObject("cbeLoanIdCaption") %></LABEL></TD>
        		<TD><%
mobjValues.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeLoanId", "tabLoans_int", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeLoanIdToolTip")))
%></TD>
           </TR>
        </TABLE>
    </FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.55
Call mobjNetFrameWork.FinishPage("ag004_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




