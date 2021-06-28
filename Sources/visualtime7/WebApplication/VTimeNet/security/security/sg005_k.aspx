<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mstrQuote As String


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG005_K"

mstrQuote = """"
%>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:05 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


    
<%mobjMenu = New eFunctions.Menues
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SG005_k", "SG005_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
    
<SCRIPT>
//% insStateZone: 
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;
    
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
			       
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++)
         document.images[lintIndex].disabled = false
         
    if ((top.fraSequence.plngMainAction==401))
        document.forms[0].cbeWindowty.disabled = true
        
}
//% insCancel: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	if(top.frames['fraSequence'].pintZone==1)
		return true
	else
	{
		if(top.frames['fraSequence'].plngMainAction==301)
			ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=SG005_k", "EndProcess", 300, 150)
		else
			return true;
	}
	
}
//% UpperCase: Permite colocar en mayúscula los campos Lógico y Pseudónimo. 
//--------------------------------------------------------------------------------------------
function UpperCase(Field){
//--------------------------------------------------------------------------------------------
    var lstrCodispl = ""
    var lstrPseudo = ""

    if (top.fraSequence.plngMainAction==301){
        document.forms[0].valCodispl.value = document.forms[0].valCodispl.value.toUpperCase();
        document.forms[0].tctPseudo.value = document.forms[0].tctPseudo.value.toUpperCase();    
    }                    
    else{
        if(Field=='sCodispl') {
            document.forms[0].valCodispl.value = document.forms[0].valCodispl.value.toUpperCase();        
            lstrCodispl = self.document.forms[0].elements[<%=mstrQuote%>valCodispl<%=mstrQuote%>].value
            if (lstrCodispl != ''){
				insDefValues('sCodispl','sCodispl=' + lstrCodispl,'/VTimeNet/Security/Security')
			}
        }            
 	    else {
            document.forms[0].tctPseudo.value = document.forms[0].tctPseudo.value.toUpperCase();     	    
 	        lstrPseudo = self.document.forms[0].elements[<%=mstrQuote%>tctPseudo<%=mstrQuote%>].value
            if (lstrPseudo != ''){
 				insDefValues('sPseudo','sPseudo=' + lstrPseudo)
 			}
 	    }
    }
}

//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTransac" ACTION="valSecuritySeq.aspx?sMode=1">
<P>&nbsp;</P>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=15041><%= GetLocalResourceObject("valCodisplCaption") %></LABEL></TD>
            <TD COLSPAN=3><%=mobjValues.PossiblesValues("valCodispl", "tabWindows", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  , "UpperCase(""sCodispl"");", True, 8, GetLocalResourceObject("valCodisplToolTip"), eFunctions.Values.eTypeCode.eString, 1,  , True)%></TD>
        </TR>
        <TR>            
            <TD><LABEL ID=15043><%= GetLocalResourceObject("tctPseudoCaption") %></LABEL></TD>
            <TD> <%=mobjValues.TextControl("tctPseudo", 12, vbNullString, False, GetLocalResourceObject("tctPseudoToolTip"),  ,  ,  , "UpperCase(""sPseudo"");", True, 2)%></TD>
            
            <TD><LABEL ID=15044><%= GetLocalResourceObject("cbeWindowtyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeWindowty", "Table88", eFunctions.Values.eValuesType.clngComboType, eFunctions.Menues.TypeForm.clngSpeWithHeader,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWindowtyToolTip"),  , 3)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
                                                                                                                                                                                                 mobjValues = Nothing
%>





