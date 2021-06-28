<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de menú        

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac002_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac002_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

</SCRIPT>

<HTML>
<HEAD>
<SCRIPT> 
//% insStateZone: habilita los campos de la forma
//-------------------------------------------------------------------------------------------		
function insStateZone(){
//-------------------------------------------------------------------------------------------		
	with(self.document.forms[0]){
		valOffice.disabled=false;
		cbeBranch.disabled=false;
		valProduct.disabled=false;
		btnvalProduct.disabled=false;
		Option[0].disabled=false;
		Option[1].disabled=false;
		valIntermed.disabled=false;
		btnvalIntermed.disabled=false;
	}
}
</SCRIPT>

	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CAC002", "CAC002_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
</HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="POST" ID="FORM" NAME="frmInqCapInc" ACTION="ValPolicyQue.aspx?x=1">
        	<BR><BR>
            <TABLE WIDTH="100%">
                <TR>
					<TD ALIGN="Left" COLSPAN="1" CLASS="HighLighted"><LABEL ID=40622><A NAME="Tipo de información"><%= GetLocalResourceObject("AnchorTipo de informaciónCaption") %></A></LABEL></TD>
					<TD>&nbsp</TD>
				    <TD><LABEL ID=13628><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
	    			<TD><%=mobjValues.PossiblesValues("cbeBranch", "table10", 1, CStr(0),  ,  ,  ,  ,  , "document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True,  , GetLocalResourceObject("cbeBranchToolTip"))%> </td>
                </TR>
                <TR> 
	    			<TD><HR></TD>
					<TD>&nbsp</TD>	    			
		            <TD><LABEL ID=13638><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
  				    <TD><%With mobjValues
	.Parameters.Add("mintBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductToolTip")))
End With
%>
					</TD>
                </TR>
                <TR>
					<TD><%=mobjValues.OptionControl(40623, "Option", GetLocalResourceObject("Option_CStr1Caption"), CStr(1), CStr(1),  , True)%></TD>                
					<TD>&nbsp</TD>
        			<TD><LABEL ID=13636><%= GetLocalResourceObject("valOfficeCaption") %></LABEL></TD>
					<TD>
					<%
With Response
					        'mobjValues.TypeList = 2
					        'mobjValues.Parameters.Add("nUserCode", Session("nUserCode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					        .Write(mobjValues.PossiblesValues("valOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(0), False, False, , , , , True, , GetLocalResourceObject("valOfficeToolTip")))
End With
%>
					</TD>
				</TR>
				<TR>
                    <TD><%=mobjValues.OptionControl(40624, "Option", GetLocalResourceObject("Option_CStr2Caption"), CStr(0), CStr(2),  , True)%> </TD>				
                    <TD>&nbsp</TD>
                    <TD><LABEL ID=13797><%= GetLocalResourceObject("valIntermedCaption") %></LABEL></TD>
                    <TD><%=mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valIntermedToolTip"), eFunctions.Values.eTypeCode.eNumeric)%> </TD>
                </TR>
            </TABLE>
            <%
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac002_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




