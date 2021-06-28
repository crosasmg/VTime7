<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    Dim sCodispl As String
    Dim sCodisplPage As String
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues

</script>
<%
    sCodispl = Trim(Request.QueryString("sCodispl"))
    sCodisplPage = LCase(sCodispl) & "_k"

    Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage(sCodisplPage)
    

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    mobjValues.sCodisplPage = sCodisplPage
    '~End Body Block VisualTimer Utility

    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<script LANGUAGE="JavaScript" src="/VTimeNet/Scripts/json2.js" type="text/javascript"></script>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 1 $|$$Date: 2/09/03 19:01 $"
    //% insClaimChanged: se manejan los campos de la página
    //------------------------------------------------------------------------------------------
    function insClaimChanged()
    //------------------------------------------------------------------------------------------
    {
        $.get("/vtimenet/ajax/claimbynclaim.aspx?nclaim=" + $("[name=tcnClaim]").val(),
              function (data) {
                  var answer = JSON.parse(data);
                  $("#divBranch").html(answer.sBranch);
                  $("#divProduct").html(answer.sProduct);
                  $("#divPolicy").html(answer.nPolicy);
              }
        );
    }


    //% insValDateChanged: 
    //------------------------------------------------------------------------------------------
    function insValDateChanged()
    //------------------------------------------------------------------------------------------
    {
        $.get("/vtimenet/ajax/exchangebyvaldate.aspx?nCurrency=4&dValDate=" + $("[name=tcdValDate]").val(),
              function (data) {
                  var answer = JSON.parse(data);
                  $("#divExchange").html(answer.sExchange);
              }
        );

    }


//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
//%valor numerico al campo
//------------------------------------------------------------------------------------------
function insDefValue(Field){
//------------------------------------------------------------------------------------------
    if(Field.value=='')
        self.document.forms[0].tcnExcess.value='0'
}

</SCRIPT>
    <META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<%
    Response.Write(mobjValues.StyleSheet())
    Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
	Response.Write(mobjValues.WindowsTitle("SIL7482", Request.QueryString("sWindowDescript")))
    mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="frmClaim" action="valClaimRep.aspx?mode=1">
    <BR></BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
    <div style="float: left;">
	<table width="80%" border="0">
		<tr>
            <td><LABEL ID="0">Siniestro </LABEL></td>
  		    <TD WIDTH="80%"><%=mobjValues.NumericControl("tcnClaim", 10, CStr(eRemoteDB.Constants.strNull),  , "Número del siniestro que se desea procesar",  ,  ,  ,  ,  ,  "insClaimChanged();")%></TD>				    
        </tr>
		<tr>
            <td><LABEL ID="LABEL4">Ramo</LABEL></td>
            <TD><%Response.Write(mobjValues.DIVControl("divBranch",  , ""))%></TD> 
        </tr>
		<tr>
            <td><LABEL ID="LABEL5">Producto</LABEL></td>
            <TD><%Response.Write(mobjValues.DIVControl("divProduct",  , ""))%></TD>            
        </tr>
		<tr>
            <td><LABEL ID="LABEL6">Póliza</LABEL></td>
            <TD><%Response.Write(mobjValues.DIVControl("divPolicy",  , ""))%></TD>
        </tr>
		<tr>
            <td><LABEL ID="0">Fecha de valorización</LABEL></td>
            <td><%=mobjValues.DateControl("tcdValDate", , , "Fecha de valorización del pago", , , , "insValDateChanged();", False)%></td>
        </tr>
		<tr>
            <td><LABEL ID="LABEL1">Factor de cambio</LABEL></td>
            <TD><%Response.Write(mobjValues.DIVControl("divExchange",  , ""))%></TD> 
        </tr>
	</table>
    </div>
    <div style="float: right;">
    <br/>
    <table>
       <TR><TD COLSPAN="2" CLASS="HighLighted" ><DIV ID="divHorline"><!--LABEL ID=LABEL2--><A NAME="Indemnizaciones anteriores">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Indemnizaciones anteriores</A><!--/LABEL--></DIV>
       </TD></TR>
       <TR><TD COLSPAN="2" CLASS="Horline"></TD></TR>
       <tr><td><%Response.Write(mobjValues.OptionControl(0, "optHistoryBy", "Por póliza", , "1")) %></td></tr>
       <tr><td><%Response.Write(mobjValues.OptionControl(0, "optHistoryBy", "Por siniestro","1" , "2"))%></td></tr>
    </table>
    </div>
</FORM>
</BODY>
</HTML>
<% mobjValues = Nothing %>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




