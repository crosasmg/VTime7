<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim mintBranch As Integer
Dim mintProduct As Integer
Dim mlngPolicy As Integer
Dim mlngCertif As Integer
Dim mdtmDate As Date

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mintBranch = eRemoteDB.Constants.intNull
	mintProduct = eRemoteDB.Constants.intNull
	mlngPolicy = eRemoteDB.Constants.intNull
	mlngCertif = eRemoteDB.Constants.intNull
	mdtmDate = eRemoteDB.Constants.dtmNull
End Sub

'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mintBranch <> eRemoteDB.Constants.intNull And mintProduct <> eRemoteDB.Constants.intNull And mlngPolicy <> eRemoteDB.Constants.intNull And mlngCertif <> eRemoteDB.Constants.intNull And mdtmDate <> eRemoteDB.Constants.dtmNull Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = " & CStr(mintBranch) & ";")
			.Write("var mintProduct = " & CStr(mintProduct) & ";")
			.Write("var mlngPolicy = " & CStr(mlngPolicy) & ";")
			If CStr(mlngCertif) = vbNullString Then
				.Write("var mlngCertif = 0;")
			Else
				.Write("var mlngCertif = " & CStr(mlngCertif) & ";")
			End If
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mintBranch = 0;")
			.Write("var mintProduct = 0;")
			.Write("var mlngPolicy = 0;")
			.Write("var mlngCertif = 0;")
			.Write("var mdtmDate = 0;")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

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

<SCRIPT LANGUAGE=JavaScript>

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
    var lintIndex = 0;
    with(self.document.forms[0]){
		cbeBranch.disabled=false;
		valProduct.disabled=false;
		btnvalProduct.disabled=false;
		tcnPolicy.disabled=false;
		tcdDate.disabled=false;
		btn_tcdDate.disabled=false;
		tctBankAuthNew.disabled=false;
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
    return true;
}

//% InsChangeUser: Llama al procedimiento que obtiene la oficina asociada al usuario
//--------------------------------------------------------------------------------------------
function GetDataCO722(Field){
//--------------------------------------------------------------------------------------------
//+ Se verifica que hubo cambio para hacer el llamado a showDefValues.
	with (self.document.forms[0]){
		if (tcnPolicy.value!='' && tcdDate.value!=''&& cbeBranch.value!='' && valProduct.value!=''){
		
			if ((mintBranch != cbeBranch.value) ||
			    (mintProduct != valProduct.value) ||
			    (mlngPolicy != tcnPolicy.value) ||
			    (mlngCertif != tcnCertif.value) ||
			    (mdtmDate != tcdDate.value)){
				mintBranch = cbeBranch.value;
			    mintProduct = valProduct.value;
			    mlngPolicy = tcnPolicy.value;
			    mlngCertif = tcnCertif.value;
			    mdtmDate = tcdDate.value;
          	    insDefValues("ShowDataCO722", "sField=" + "CO722" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertif.value + "&dDate=" + tcdDate.value);    

/*				ShowPopUp('/VTimeNet/Collection/CollectionTra/ShowDefValues.aspx?Field=CO722&nBranch='+ self.document.forms[0].cbeBranch.value + 
			                                                                   "&nProduct=" + self.document.forms[0].valProduct.value + 
			                                                                   "&nPolicy=" + self.document.forms[0].tcnPolicy.value +
			                                                                   "&nCertif=" + self.document.forms[0].tcnCertif.value + 
			                                                                   "&dDate=" + self.document.forms[0].tcdDate.value, 'ShowDefValues', 1, 1,'no','no',2000,2000);
*/
			}
		}	
	}

}

//% InsChangeField: se controla los parámetros del campo producto.
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue) {
    //--------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        switch (sField) {
            case 'Branch':
                valProduct.Parameters.Param1.sValue = sValue;
                valProduct.disabled = (sValue == '0');
                btnvalProduct.disabled = valProduct.disabled;
                break;
        }
        valProduct.value = '';
        UpdateDiv('valProductDesc', '');
    }
}

</SCRIPT>
	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CO722", "CO722_K.aspx", 1, vbNullString))
	.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing

Call insReaInitial()
Call insOldValues()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO722" ACTION="valCollectionTra.aspx?sMode=2">
	<BR><BR>
    <TABLE WIDTH="100%">
        <%Response.Write(mobjValues.HiddenControl("tctCertype", "2"))%>	
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD>
                <%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)", True,  , GetLocalResourceObject("cbeBranchToolTip"))%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD WIDTH ="80%"><%
mobjValues.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdMaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  , 20, "GetDataCO722();", True, 4, GetLocalResourceObject("valProductToolTip")))
%>
			</TD>
        </TR>
        
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 8, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "GetDataCO722();", True)%></TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=13902><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 4, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  , "GetDataCO722();", True)%></TD>
        </TR>    

        <TR>
	        <TD><LABEL ID=0><%= GetLocalResourceObject("dtcClientCaption") %></LABEL></TD>
		    <TD COLSPAN="3"><%=mobjValues.ClientControl("dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientToolTip"),  , True, "lblCliename")%></TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
                
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctBankAuthOldCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctBankAuthOld", 15, vbNullString,  , GetLocalResourceObject("tctBankAuthOldToolTip"),  ,  ,  ,  , True)%></TD> 
        </TR>
  
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctBankAuthNewCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctBankAuthNew", 15, vbNullString,  , GetLocalResourceObject("tctBankAuthNewToolTip"),  ,  ,  ,  , True)%></TD> 
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>




