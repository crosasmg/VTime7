<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


Dim mstrYear As Integer
Dim mstrMonth As Integer


Private Sub insPreCO982()
	mstrYear = Today.Year
	mstrMonth = Today.Month
	Response.Write("<SCRIPT>")
	Response.Write("sDefinitiveYear='" & mstrYear & "';")
	Response.Write("sDefinitiveMonth='" & mstrMonth & "';")
	Response.Write("</" & "Script>")
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co982_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co982_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 22/10/09 7:15p $|$$Author: Gletelier $"

//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){ 
		btncbeBankExt.disabled = false;
		cbeBankExt.disabled = false;
		tcnYear.disabled = false;
		cboMonth.disabled = false;
	}
}

//% insCancel: Acciones al cancelar la transacción.
//-------------------------------------------------------------------------------------------   
function insCancel(){
//-------------------------------------------------------------------------------------------   
	return true
}
//% InsChangeValues:
//--------------------------------------------------------------------------------------------
function InsChangeValues(nField){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		btncbeCodRej.disabled = false;
		cbeBankExt.disabled = false;
		cbeCodRej.disabled = false;
		cbeCodRej.Parameters.Param1.sValue=nField.value
		}
}
</SCRIPT>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<%
Response.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjValues.StyleSheet() & vbCrLf)
Response.Write(mobjMenu.MakeMenu("CO982", "CO982_K.aspx", 1, vbNullString))
mobjMenu = Nothing

Call insPreCO982()
%> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmModCollect" ACTION="valCollectionTra.aspx?x=1">
	<BR><BR>    
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
	<BR>
    <TABLE WIDTH="70%">
        <TR>
           <TD><LABEL><%= GetLocalResourceObject("cbeBankExtCaption") %><LABEL></TD>
           <TD><%=mobjValues.PossiblesValues("cbeBankExt", "Tab_BankReject", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "InsChangeValues(this);", True,  , GetLocalResourceObject("cbeBankExtToolTip"),  , 3)%></TD>
        </TR>
        <TR>  
            <TD></TD>
            <TD></TD>
         </TR>
         <TR>  
          <TD><LABEL><%= GetLocalResourceObject("cbeCodRejCaption") %><LABEL></TD>
          <TD><%
                With mobjValues
	                .Parameters.Add("nBankExt", eRemoteDB.Constants.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	                Response.Write(mobjValues.PossiblesValues("cbeCodRej", "Tab_BankRejectCause", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCodRejToolTip"),  , 3))
                End With
              %>
          </TD>
        <TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear", 4, mstrYear,  , GetLocalResourceObject("tcnYearToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>  
            <TD></TD>
            <TD></TD>
         </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cboMonthCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cboMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, mstrMonth,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboMonthToolTip"),  , 1))%></TD>
        </TR>
        <TR>  
            <TD></TD>
            <TD></TD>
         </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateProcCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDateProc", Today.Date,  , GetLocalResourceObject("tcdDateProcToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>    
    <TABLE>    
<%
mobjValues = Nothing
%>
    </TABLE>    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co982_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





