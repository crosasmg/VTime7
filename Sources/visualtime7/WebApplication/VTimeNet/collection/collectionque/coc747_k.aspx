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
Call mobjNetFrameWork.BeginPage("coc747_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "coc747_k"
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
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
    </SCRIPT>
	<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("COC747", "COC747_k.aspx", 1, vbNullString))
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

//% InsChangeField: se controla los parámetros del campo producto.
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
				valProduct.Parameters.Param1.sValue=sValue;
				valProduct.disabled = (sValue == '0');
				btnvalProduct.disabled = valProduct.disabled;
				break;
		}
		valProduct.value = '';
		UpdateDiv('valProductDesc','');
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
	insReloadTop(false);
//    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COC747" ACTION="valCollectionQue.aspx?sMode=2">
<BR></BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=9906><%= GetLocalResourceObject("cbeInsur_areaCaption") %></LABEL></TD>
            <%mobjValues.BlankPosition = False%>  
            <TD><%=mobjValues.PossiblesValues("cbeInsur_area", "table5001", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInsur_areaToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)", True,  , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>	
			<TD><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valProductToolTip")))
End With
%>
			</TD>
		</TD>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
		
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc747_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




