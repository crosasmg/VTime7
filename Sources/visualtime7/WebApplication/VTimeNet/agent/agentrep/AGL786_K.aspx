<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
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
Call mobjNetFrameWork.BeginPage("AGL786_K")

'- Objeto para el manejo particular de los datos de la página
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "AGL786_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
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
	
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 22/06/04 13:05 $|$$Author: Nvaplat53 $"
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insChangeField(nid){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		    if(cbeBranch.value!="" && cbeBranch.value>0){
			    valProduct.Parameters.Param1.sValue=cbeBranch.value;			
			    valProduct.disabled		= false;
			    btnvalProduct.disabled	= false;
			    }
			else{
			    valProduct.disabled		= true;
			    valProduct.value		= '';
				btnvalProduct.disabled	= true;
			}
	}
			
}

</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("AGL786", "AGL786_K.aspx", 1, vbNullString))
	'Response.Write mobjMenu.setZone(1,"AGL786", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="AGL786_K" ACTION="valagentrep.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("AGL786", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH=100% BORDER=0 CELLSPACING=2 CELLPADDING=2 >
        <TR>
            <TD WIDTH=100% VALIGN=TOP>
                <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=1 WIDTH=50%>
					<TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateFromCaption") %>&nbsp;</LABEL>
						<TD><%=mobjValues.DateControl("tcdDateFrom", "", True, GetLocalResourceObject("tcdDateFromToolTip"))%></TD>
			        </TR>
			        <TR>			
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %>&nbsp;</LABEL></TD>
						<TD><%=mobjValues.DateControl("tcdDateTo", "", True, GetLocalResourceObject("tcdDateToToolTip"))%></TD>
					</TR>
                    <TR>
						<TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
						<TD WIDTH=40%>
							<%With mobjValues
	.Parameters.Add("sBrancht", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0)
	.Parameters.Add("sBrancht_not", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0)
	Response.Write(mobjValues.PossiblesValues("cbeBranch", "tabTable10_t", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insChangeField(1);",  ,  , GetLocalResourceObject("cbeBranchToolTip"), eFunctions.Values.eTypeCode.eNumeric))
End With
%>
						</TD>
						<TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
						<TD WIDTH=30%>
								<%With mobjValues
	.Parameters.Add("nBranch", "40", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10)
	Response.Write(.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valProductToolTip")))
End With
%>
						</TD>                  
					</TR>
				</TABLE>
		     </TD>
		 </TR>
    </TABLE>
	<%=mobjValues.HiddenControl("hddUsercode", Session("nUsercode"))%>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("AGL786_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





