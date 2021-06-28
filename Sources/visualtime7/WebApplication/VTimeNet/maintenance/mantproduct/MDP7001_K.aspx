<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MDP7001", "MDP7001_k.aspx", 1, ""))
	Response.Write("<BR>")
End With
mobjMenu = Nothing
%>
      
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 12-08-09 14:46 $|$$Author: Mgonzalez $"
    
//% insStateZone: Se controla el estado de los campos de la página.
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeBranch.disabled=false;
		valProduct.disabled=false;
		btnvalProduct.disabled=false
		tctDescript.disabled=false;
	}
}

//% insCancel: Se controla la acción Cancelar de la página.
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: Se controla la acción Cancelar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% InsChangeField: Se controla el cambio de valor de los campos de la página.
//--------------------------------------------------------------------------------------------
function InsChangeField(vObj){
//--------------------------------------------------------------------------------------------

	var sValue = vObj.value;;
	var sName  = vObj.name;
	with (self.document.forms[0]){
		switch (sName){
			case 'cbeBranch':
				valWarrn_table.Parameters.Param1.sValue=sValue;
				break;
			case 'valProduct':
				valWarrn_table.Parameters.Param2.sValue=sValue;
				break;
		}
	    valWarrn_table.value = '';
		UpdateDiv('valWarrn_tableDesc','','NoPopup');
		valWarrn_table.disabled = cbeBranch.value  == '0' ||
		                          valProduct.value == '';
		btnvalWarrn_table.disabled = valWarrn_table.disabled;
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MDP7001" ACTION="valMantProduct.aspx?sTime=1">
    <TABLE WIDTH="100%">
		<TR>
			<TD>&nbsp;</TD>			
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "InsChangeField(this)", True)%></TD>
			
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "InsChangeField(this)")%></TD>
        </TR>
         <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valWarrn_tableCaption") %></LABEL></TD>
             <TD>
             <%
With mobjValues.Parameters
	.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valWarrn_table", "TABTAB_APV_WARRAN", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valWarrn_tableToolTip"), eFunctions.Values.eTypeCode.eNumeric,  ,  , True))
%>
             </TD>
			 <TD><LABEL ID=0><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
			 <TD><%=mobjValues.TextControl("tctDescript", 10, vbNullString,  , GetLocalResourceObject("tctDescriptToolTip"))%></TD>
         </TR>         
    </TABLE>
</FORM>
</BODY>
</HTML>




