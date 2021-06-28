<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("va669_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "va669_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	var lintIndex = 0;
	try{
		with (self.document.forms[0]){
			for (lintIndex=0;lintIndex<length;lintIndex++)
			    elements[lintIndex].disabled = false;
			btnvalProduct.disabled = false;
			btn_tcdEffecdate.disabled = false;
		}
	} catch(error){}
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
    return true;
}
//% insChangeField: se controla cambio en controles dependientes
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
var lstrParams = new String;
    
    with(document.forms[0]){
        if (objField.name=='tcnPolicy'){
			if (cbeBranch.value != "" && valProduct.value != "" && objField.value != ""){
				lstrParams = 'nBranch=' + cbeBranch.value +
					         '&nProduct=' + valProduct.value +
						     '&nPolicy=' + tcnPolicy.value +
							 '&sFrame=';
				insDefValues("insValsPolitype",lstrParams,'/VTimeNet/Policy/PolicyTra');
			}
			else{
				tcnCertif.disabled = false
				tcnCertif.value = ""
			}
        }
    }

}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("VA669", "VA669_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="VA669_K" ACTION="valPolicyTra.aspx?sMode=2">
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD WIDTH="25%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(40))%></TD>
			<TD WIDTH="1%">&nbsp;</TD>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10)
Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(40), eFunctions.Values.eValuesType.clngWindowType,  , "",  ,  ,  ,  ,  ,  ,  , eFunctions.Values.eProdClass.clngActiveLife))
%> 
            </TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 10, CStr(eRemoteDB.Constants.intNull), True, GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insChangeField(this);")%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCertifToolTip"))%></TD> 
        </TR>
        <TR>
   			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeIllusttypeCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = 0
Response.Write(mobjValues.PossiblesValues("cbeIllusttype", "Table5599", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeIllusttypeToolTip")))
%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.23
Call mobjNetFrameWork.FinishPage("va669_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




