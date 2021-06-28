<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

'- Variables para el manejo de la caja    
Dim lclsUser_cashnum As eCashBank.User_cashnum
Dim llngCashNum As Object


</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
lclsUser_cashnum = New eCashBank.User_cashnum

If lclsUser_cashnum.Find_nUser(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
	llngCashNum = lclsUser_cashnum.nCashNum
Else
	llngCashNum = ""
End If

lclsUser_cashnum = Nothing

mobjValues.sCodisplPage = "opl719_k"

%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT>
//%Variable para el control de Versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 16/10/03 16:47 $"
</SCRIPT>        
<SCRIPT LANGUAGE="JavaScript"> 
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------

}
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}
</SCRIPT>    
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjValues.WindowsTitle("OPL719"))
Response.Write(mobjMenu.MakeMenu("OPL719", "OPL719_k.aspx", 1, ""))
Response.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "OPL719_k.aspx"))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<SCRIPT>
//- Variable que almacena la fecha del sistema
	
	var mdtmDateSystem = GetDateSystem()
	
</SCRIPT>	
<SCRIPT>
//-------------------------------------------------------------------------------------------
function insInitialFields(){
	document.forms["OPL719"].elements["tcdProcDat"].value = mdtmDateSystem	
}	

//-------------------------------------------------------------------------------------------
function InsChangeOpertyp(nOpertyp){
	if (nOpertyp.value==1){ 
	    document.forms["OPL719"].elements["chkPrint"].checked  = true;
	    document.forms["OPL719"].elements["chkPrint"].disabled = true;
	}
	else{ 
	    document.forms["OPL719"].elements["chkPrint"].checked  = false;
	    document.forms["OPL719"].elements["chkPrint"].disabled = false;
	}
	    
}	



</SCRIPT>  
<FORM METHOD="post" ID="FORM" NAME="OPL719" ACTION="valCashBankRep.aspx?Zone=1">
<BR><BR>
<TABLE WIDTH="100%">
    <TR>
        <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("cbeCash_opertypCaption") %></LABEL></TD>
        <TD WIDTH="70%"><%=mobjValues.PossiblesValues("cbeCash_opertyp", "table5562", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "InsChangeOpertyp(this);",  ,  , GetLocalResourceObject("cbeCash_opertypToolTip"),  , 1)%> </TD>
    </TR>
    <TR>    
        <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("tcdProcDatCaption") %></LABEL></TD>
        <TD WIDTH="70%"><%=mobjValues.DateControl("tcdProcDat", Request.Form.Item("tcdProcDat"), True, GetLocalResourceObject("tcdProcDatToolTip"),  ,  ,  ,  ,  , 2)%></TD>
    </TR>
    <TR>    
        <TD WIDTH="20%"><LABEL ID=0><%= GetLocalResourceObject("tcnCashnumCaption") %></LABEL></TD>
        <TD WIDTH="70%"><%=mobjValues.NumericControl("tcnCashnum", 5, llngCashNum, True, GetLocalResourceObject("tcnCashnumToolTip"),  ,  ,  ,  ,  ,  ,  , 3)%> </TD>
    </TR>      
    
	<TR>
		<TD><%=mobjValues.CheckControl("chkPrint", GetLocalResourceObject("chkPrintCaption"),  , "1")%></TD>
		<TD COLSPAN="3"></TD>
	</TR>
    
</TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
Response.Write("<SCRIPT>insInitialFields()</SCRIPT>")
%>    






