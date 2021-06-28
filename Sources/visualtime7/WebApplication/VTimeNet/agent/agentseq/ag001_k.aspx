<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1
Response.CacheControl = "private"
Session("ConnectID") = Session.SessionID
mobjValues = New eFunctions.Values
'+ El parámetro del QueryString "sAgentCode" viene lleno si esta ventana es invocada
'+ desde la CA025 (Clientes) - ACM - 08/08/2001
If Request.QueryString.Item("sAgentCode") <> vbNullString Then
	Session("nIntermed") = Request.QueryString.Item("sAgentCode")
End If

If CStr(Session("nLastIntermediary")) <> vbNullString And Session("MenuOption") <> 401 Then
	Session("nLastIntermediary") = vbNullString
End If
%>
<HTML>
<HEAD>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//%insStateZone. Esta funcion se ejecuta cada vez que el usuario selecciona una opcion
//%de las acciones del menu.
//----------------------------------------------------------------------------------
function insStateZone(){
//----------------------------------------------------------------------------------
	var lintIntermediary;
	
    self.document.forms[0].elements[0].disabled = false;
    self.document.btnvalIntermedia.disabled = false;

    if (top.fraSequence.plngMainAction== 301){
        self.document.forms[0].elements[0].value = "";
        UpdateDiv('valIntermediaDesc','','Normal');
    }

	if(top.fraSequence.plngMainAction== 306){
		lintIntermediary = "'" + <%=Session("nLastIntermediary")%> + "'";
		if(lintIntermediary!='0' && lintIntermediary!="" && lintIntermediary!="'NaN"){
			alert("Último intermediario consultado y a ser duplicado: " + lintIntermediary);
			self.document.forms[0].elements[0].value = "";
			UpdateDiv('valIntermediaDesc','','Normal');
		}
		else{
			alert("Debe consultar un intermediario antes de duplicalo");
			self.document.forms[0].elements[0].disabled = true;
			self.document.btnvalIntermedia.disabled = true;
		}
	}
}
   
//% insCancel: Se invoca a esta función una vez que el usuario decide cancelar la 
//%			   operación - ACM - 13/05/2002
//----------------------------------------------------------------------------------
function insCancel(){
//----------------------------------------------------------------------------------
    if (top.frames["fraSequence"].pintZone==2 && 
		(top.frames["fraSequence"].plngMainAction==301 || 
		top.frames["fraSequence"].plngMainAction==306) )
			ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=AG001_K","EndProcess",300,180)
	else
	    return (true);
}

//% insFinish: Se invoca a esta función una vez que el usuario decide finalizar la 
//%			   operación - ACM - 13/05/2002
//----------------------------------------------------------------------------------
function insFinish(){
//----------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<meta HTTP-EQUIV="Content-Language" CONTENT="es">
    <%mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("AG001_K", "AG001_K.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<SCRIPT>
    var marrActions = []
    var mintActionQ = -1

//% AddAction:
//----------------------------------------------------------------------------------
function AddAction(Action,ActionDes,HelAction)
//----------------------------------------------------------------------------------
{
    var larrAction = []
    larrAction[0] = Action
    larrAction[1] = ActionDes
    larrAction[2] = HelAction
    marrActions[++mintActionQ] = larrAction
}

//% ShowAction:
//----------------------------------------------------------------------------------
function ShowAction(Top)
//----------------------------------------------------------------------------------
{
    var lintIndex=0
    var lstrImage=""
    doc = self.document
    doc.write("<TABLE BORDER=\"0\" VSPACE=\"0\" HSPACE=\"0\" style=\"POSITION: relative; TOP: " + Top + "px\">")
    for (lintIndex==0;lintIndex<marrActions.length;lintIndex++){
        lstrImage = ""
        doc.write("<TD BGCOLOR=NAVY><a STYLE=\"TEXT-DECORATION: none\" href=\"JAVASCRIPT:alert('Hola')\">" + lstrImage + marrActions[lintIndex][1] + "</A></TD>")
    }
    doc.write("</TABLE>")
}
//% KeepIntermediaryNumber: Conserva en el campo de valores posibles el código
//%							del intermediario ingresado - ACM - 13/04/2002
//----------------------------------------------------------------------------------
function KeepIntermediaryNumber(nValue)
//----------------------------------------------------------------------------------
{
	if(top.frames["fraSequence"].plngMainAction!="" &&
		top.frames["fraSequence"].plngMainAction!=0)
		self.document.forms[0].elements["valIntermedia"].value = nValue;
}

</SCRIPT>

    <BODY CLASS="Header" VLINK=white LINK=white ALINK=white >
    <BR><BR><BR>
        <FORM METHOD= "POST" ACTION="valAgentSeq.aspx?TIMEINFO=1" ID=form1 NAME=form1>
        <TABLE WIDTH=100% >
          <TR>
           <TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
<%
If (Request.QueryString.Item("sOriginalForm") <> vbNullString) And (CStr(Session("nIntermed")) <> vbNullString Or Session("nIntermed") <> 0) Then
	%>
				<TD WIDTH=80%><%=mobjValues.PossiblesValues("valIntermedia", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType, Session("nIntermed"),  ,  ,  ,  ,  , "KeepIntermediaryNumber(this.value);", True, 10, GetLocalResourceObject("valIntermediaToolTip"),  ,  ,  , True)%></TD>
<%	
Else
	%>
				<TD WIDTH=80%><%=mobjValues.PossiblesValues("valIntermedia", "tabintermedia_o", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , "KeepIntermediaryNumber(this.value);", True, 10, GetLocalResourceObject("valIntermediaToolTip"),  ,  ,  , True)%></TD>
<%	
End If
%>
<!-- + Se añade un campo oculto para que almacene el valor del parámetro "sOriginalForm", el cual
	   contiene valor únicamente si esta ventana es llamada desde la CA025 - ACM - 31/07/2001 -->	
			<%=mobjValues.HiddenControl("tctOriginalForm", Request.QueryString.Item("sOriginalForm"))%>
        </TR>
        </TABLE>
		<BR>
        <HR>
        </FORM>
    </BODY>
</HTML>
<%
mobjValues = Nothing
%>
<SCRIPT>
if (top.frames["fraSequence"].plngMainAction!=0 && typeof(top.frames["fraSequence"].plngMainAction)!="undefined") {
    var lintAction = (top.frames["fraSequence"].plngMainAction);
    ClientRequest(lintAction);
}
</SCRIPT>





