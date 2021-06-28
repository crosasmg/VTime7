<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
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
sCodispl = Trim(Request.QueryString.Item("sCodispl"))
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

<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 24-08-09 11:13 $"

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

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		switch (objField.name){
		
		case 'cbeBranch':
			valProduct.Parameters.Param1.sValue=objField.value;			
			break;
		}
    }
}
//%   insChargeProduct: Se cargan los parámetros del campo producto.
//------------------------------------------------------------------------------------------
function insChargeProduct(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value!=0) {
	
		with(self.document.forms[0]){
			valProduct.disabled=false;
			btnvalProduct.disabled=false;
			valProduct1.value="";
			UpdateDiv("valProductDesc", "")
			valProduct.Parameters.Param1.sValue=lobject.value;
			valProduct.Parameters.Param2.sValue=0;

		}
    }
}


</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="Policy" action="valPolicyRep.aspx?mode=1">

    <BR></BR>
	    
	<%Response.Write(mobjValues.ShowWindowsName("VIL8007", Request.QueryString.Item("sWindowDescript")))%>
   
    <TABLE WIDTH=100% BORDER=0 CELLSPACING=2 CELLPADDING=2 >
        <TR>
			<TD> <LABEL ID=41208><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL> </TD>
			<TD> <%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , "insChargeProduct(this)",  ,  , "",  , 3)%></TD>
		</TR>
		<TR>
			<TD> <LABEL ID=40011><%= GetLocalResourceObject("valProductCaption") %></LABEL> </TD>
			<%With mobjValues
	.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
			<TD> <%=mobjValues.PossiblesValues("valProduct", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  ,  , "", eFunctions.Values.eTypeCode.eString, 4)%></TD>			
		</TR>  
        <TR>
	        <TD><LABEL><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
			<TD><%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeMonth", "Table7013", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip")))
%></TD>
		</TR>
		<TR>
	        <TD><LABEL><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnYear", 4, "",  , GetLocalResourceObject("tcnYearToolTip"),  ,  ,  ,  ,  ,  , False)%> 
		</TR>
		<TR>
			<TD><!--LABEL ID=0>Archivo</LABEL--></TD>
			<TD><%=mobjValues.HiddenControl("tctFile", "")%>
				<!--%=mobjValues.TextControl("tctFile",15,Vbnullstring,, GetLocalResourceObject("tctFileToolTip"),,,,,False)%-->
				
			</TD>
		</TR>
    </TABLE>
   
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




