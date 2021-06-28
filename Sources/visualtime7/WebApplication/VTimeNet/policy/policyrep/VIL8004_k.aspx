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
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

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


//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		switch (objField.name){
		case 'cbeStatQuota':
			cbeWaitCode.disabled=objField.value!=1;
			if (cbeWaitCode.disabled) cbeWaitCode.value=0;
			break;
		case 'cbeBranch':
			valProduct.Parameters.Param1.sValue=objField.value;			
			break;
		case 'cbeInsur_Area':
			if (objField.value==1) { // area de generales
				cbeBranch.Parameters.Param1.sValue='';
				cbeBranch.Parameters.Param2.sValue='1';
			}
			else{ // vida
				cbeBranch.Parameters.Param1.sValue=1;
				cbeBranch.Parameters.Param2.sValue='';
			}
			valProduct.Parameters.Param1.sValue = '';
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
	    
	<%Response.Write(mobjValues.ShowWindowsName("VIL8004", Request.QueryString.Item("sWindowDescript")))%>

    <TABLE WIDTH=100% BORDER=0 CELLSPACING=2 CELLPADDING=2 >
        <TR>
            <TD WIDTH=50% VALIGN=TOP>
                <TABLE WIDTH=100% BORDER=0 CELLSPACING=1 CELLPADDING=1>
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
                   				 
					<tr>
	      <td><label><%= GetLocalResourceObject("tcnPolicyCaption") %></label></td>
	      <td><%=mobjValues.NumericControl("tcnPolicy", 9, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , False)%> 
		</tr>
			<TR>
			<TD><LABEL ID=LABEL2><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("valIntermedia", "tabIntermedia", eFunctions.Values.eValuesType.clngWindowType, "", False,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valIntermediaToolTip"))%></TD>
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
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




