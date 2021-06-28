<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales del grid
Dim mobjGrid As eFunctions.Grid


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "vic005_k"
	
	With mobjGrid.Columns
		Call .AddTextColumn(102074, GetLocalResourceObject("txtBranchgridColumnCaption"), "txtBranchgrid", 10, "",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("txtProductColumnCaption"), "txtProduct", 30, "",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(102072, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(102073, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddTextColumn(102075, GetLocalResourceObject("txtClientColumnCaption"), "txtClient", 10, "Cliente Inexistente",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(102076, GetLocalResourceObject("txtClientNameColumnCaption"), "txtClientName", 10, "Cliente Inexistente",  ,  ,  ,  ,  , True)
	End With
	With mobjGrid
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.bOnlyForQuery = True
	End With
End Sub
'------------------------------------------------------------------------------
Private Sub insDefineHeader1()
	'------------------------------------------------------------------------------
	
Response.Write("    " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=5>" & vbCrLf)
Response.Write("        <TR><HR></TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102054>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	With mobjValues
		.Parameters.Add("sBrancht", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sBrancht_Not", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.BlankPosition = False
	End With
	Response.Write(mobjValues.PossiblesValues("cbeBranch", "TabTable10_t", 1, Session("nBranch"), True,  ,  ,  ,  , "insChangeField(this);", True,  , GetLocalResourceObject("cbeBranchToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=102055>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		.Parameters.Add("mintBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductToolTip")))
	End With
	Response.Write("<SCRIPT>document.forms[0].cbeBranch.onchange()</" & "Script>")
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=102056><A NAME=""Tipo de Póliza"">" & GetLocalResourceObject("AnchorTipo de PólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=102057><A NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>            " & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(102070, "optTypePol", GetLocalResourceObject("optTypePol_1Caption"), CStr(1), "1",  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD><TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102058>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(102071, "optTypePol", GetLocalResourceObject("optTypePol_2Caption"),  , "2",  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102059>" & GetLocalResourceObject("tcdExpirdatCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102060>" & GetLocalResourceObject("cbePayfreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=2>")


Response.Write(mobjValues.PossiblesValues("cbePayfreq", "table36", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayfreqToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102061>" & GetLocalResourceObject("tcnCapitalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnCapital", 18, "",  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=102064>" & GetLocalResourceObject("tcnPremiumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.NumericControl("tcnPremium", 18, "",  , GetLocalResourceObject("tcnPremiumToolTip"), True, 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=102062>" & GetLocalResourceObject("tcnAge_reinsuCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnAge_reinsu", 3, "",  , GetLocalResourceObject("tcnAge_reinsuToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=102063>" & GetLocalResourceObject("tcnAgeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnAge", 3, "",  , GetLocalResourceObject("tcnAgeToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("    </TABLE>")

End Sub

'% insPreVIC005: Se cargan los datos en el grid de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreVIC005()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife As eBranches.Life
	Dim lblnFind As Boolean
	Dim lintCount As Integer
	Dim lCountReg As Short
	
	lclsLife = New eBranches.Life
	With Request
		Response.Write("<DIV ID=""Scroll"" style=""Weight:150percent;height:160;overflow:auto; outset gray"">")
		If Not IsNothing(.QueryString("nBranch")) Then
			lblnFind = lclsLife.Find_VIC005_k("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypePolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("dExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nPayFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nAge_reinsu"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble, True))
			If lblnFind Then
				lCountReg = 1
				lintCount = 0
				For lintCount = 0 To lclsLife.Count - 1
					If lclsLife.ItemPolLife(lintCount) Then
						With mobjGrid
							.Columns("txtBranchgrid").DefValue = lclsLife.sDesBranch
							.Columns("txtProduct").DefValue = lclsLife.sDesProduct
							.Columns("tcnPolicy").DefValue = CStr(lclsLife.npolicy)
							.Columns("tcnCertif").DefValue = CStr(lclsLife.ncertif)
							.Columns("txtClient").DefValue = lclsLife.sClient
							.Columns("txtClientName").DefValue = lclsLife.sCliename
							.Columns("tcnPolicy").HRefScript = "insDefValues('VIC005'," & "'sCodispl=VIC005&nBranch=" & lclsLife.nBranch & "&nProduct=" & lclsLife.nProduct & "&nCertif=" & lclsLife.ncertif & "&nPolicy=" & lclsLife.npolicy & "&dEffecdate=" & mobjValues.TypeToString(lclsLife.dEffecdate, eFunctions.Values.eTypeData.etdDate) & "','/VTimeNet/Branches/BranchQue');"
						End With
						Response.Write(mobjGrid.DoRow())
						lCountReg = lCountReg + 1
						If lCountReg = 100 Then
							Exit For
						End If
					End If
				Next 
			End If
		End If
		Response.Write(mobjGrid.closeTable() & "</DIV>")
	End With
	lclsLife = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "vic005_k"

%>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:54 $|$$Author: Nvaplat18 $"

//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------
    return true;
}   
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
    var lintIndex;
    var error;
    try {
        for(lintIndex=1;lintIndex < self.document.forms[0].elements.length;lintIndex++){
            self.document.forms[0].elements[lintIndex].disabled=false;
            if(self.document.images.length>0)
                if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined')
                   self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled
        }
     }catch(error){}
     self.document.forms[0].cbeBranch.disabled=false;
     self.document.forms[0].btnvalProduct.disabled=false;     
}
//%insChangeField: Control de cambio de parámetros
//--------------------------------------------------------------------------------------------
function insChangeField(oField){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        switch(oField.name){
    	case 'cbeBranch':
    		valProduct.Parameters.Param1.sValue=oField.value;
//    		valProduct.disabled = btnvalProduct.disabled = (oField.value=='0'||oField.value=='');
    		valProduct.value=''
    		UpdateDiv('valProductDesc', '');
    		break;
        }
    }
}

</SCRIPT>
<META http-equiv="Content-Language" content="es">
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIC005_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
End If
%>
<FORM METHOD="post" ID="FORM" NAME="frmPoliciesQuery" ACTION="ValBranchQue.aspx?x=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreVIC005()
End If
Call insDefineHeader1()
mobjGrid = Nothing
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





