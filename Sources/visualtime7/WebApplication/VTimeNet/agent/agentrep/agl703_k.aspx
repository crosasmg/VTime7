<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**+ ----------------------------------------------------------------------------------------
'**+ Ventana Puntual.  Comentario General
'**+ Borrar todos los comentarios que comiencen con '**+ o con //**+
'**+ Sustituir "Codispl" por el código lógico de la transacción
'**+ ----------------------------------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL703_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AGL703_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 1 $|$$Date: 8/09/04 16:54 $|$$Author: Nvaplat53 $"
    </SCRIPT>
	<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("AGL703", "AGL703_k.aspx", 1, ""))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;
    with(self.document.forms[0]){
        for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
			elements[lintIndex].disabled=false
		
//		btnvalProduct.disabled=false;
    }
}


//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	//insReloadTop(false);
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="AGL703" ACTION="valagentrep.aspx?">
<BR></BR>
    <TABLE WIDTH="30%">
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDate_iniCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDate_ini", CStr(Today), True, GetLocalResourceObject("tcdDate_iniToolTip"),  ,  ,  ,  , False, 1)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdDate_endCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdDate_end", CStr(Today), True, GetLocalResourceObject("tcdDate_endToolTip"),  ,  ,  ,  , False, 2)%></TD>            
        </TR>
        
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
'^End Footer Block VisualTimer%>




