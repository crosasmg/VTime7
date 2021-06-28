<%@ Page explicit="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values

Dim lobjCash_movs As Object
Dim lintCount As Object


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "inc001_k"
	
	
Response.Write(" <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR><TD ALIGN=""CENTER"" COLSPAN=10>" & vbCrLf)
Response.Write("        ")

	With mobjGrid.Columns
		Call .AddTextColumn(101497, GetLocalResourceObject("txtBranchgridColumnCaption"), "txtBranchgrid", 10, " ",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(101495, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(101496, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddTextColumn(101498, GetLocalResourceObject("txtinsurColumnCaption"), "txtinsur", 10, "Cliente Inexistente",  ,  ,  ,  ,  , True)
	End With
	mobjGrid.DeleteButton = False
	mobjGrid.AddButton = False
	mobjGrid.Columns("Sel").GridVisible = False
	mobjGrid.bOnlyForQuery = True
	
Response.Write("" & vbCrLf)
Response.Write("	    </TD></TR>   " & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("   </DIV>")

	
	
	
	
End Sub

Private Sub insDefineHeader1()
	
Response.Write("      <TABLE WIDTH=""100%""> " & vbCrLf)
Response.Write("        <DIV>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=101474>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "table10", 1, CStr(2),  ,  ,  ,  ,  , "inschangebranch", True,  , GetLocalResourceObject("cbeBranchToolTip")))


Response.Write(" </TD>" & vbCrLf)
Response.Write("            <TD></TD>			" & vbCrLf)
Response.Write("		  	<TD><LABEL ID=101475>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("  			<TD>")

	With mobjValues
		.Parameters.Add("mintBranch", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valProductToolTip")))
	End With
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD></TD>			" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=101476><A NAME=""Poltype"">" & GetLocalResourceObject("AnchorPoltypeCaption") & "</A></LABEL></TD>          " & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101477>" & GetLocalResourceObject("cbePayFreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayFreq", "table36", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayFreqToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=101478><A NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</A></LABEL><HR></TD>" & vbCrLf)
Response.Write("            <TD></TD>                        " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(101492, "optTypePolicy", GetLocalResourceObject("optTypePolicy_Caption"), CStr(1)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101479>" & GetLocalResourceObject("tctCapitalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tctCapital", 18, " ",  , "", True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=101480>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecDate",  ,  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>                        " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(101493, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr2Caption"),  , CStr(2)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101481>" & GetLocalResourceObject("tctPremiumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tctPremium", 18, "",  , "", True, 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=101482>" & GetLocalResourceObject("tcdNullDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdNullDate",  ,  , GetLocalResourceObject("tcdNullDateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>      " & vbCrLf)
Response.Write("            <TD></TD>                        " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.OptionControl(101494, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr3Caption"),  , CStr(3)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TABLE>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        </TR><TR><TD COLSPAN=""9""><HR></DIV></TD>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101483>" & GetLocalResourceObject("cbeArticleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeArticle", "table118", 1,  ,  ,  ,  ,  ,  , "if(typeof(document.forms[0].valDetailArt)!=""undefined"")document.forms[0].valDetailArt.Parameters.Param1.sValue=this.value", True,  , GetLocalResourceObject("cbeArticleToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101484>" & GetLocalResourceObject("valDetailArtCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("   			<TD>")

	With mobjValues
		.Parameters.Add("nArticle", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valDetailArt", "tabtab_in_bus", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valDetailArtToolTip")))
	End With
Response.Write("				" & vbCrLf)
Response.Write("			</TD>	" & vbCrLf)
Response.Write("            <TD></TD>				" & vbCrLf)
Response.Write("            <TD><LABEL ID=101485>" & GetLocalResourceObject("cbeActivityCatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeActivityCat", "table7044", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeActivityCatToolTip")))


Response.Write("</TD>	" & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("            <TD><LABEL ID=101486>" & GetLocalResourceObject("cbeConstCatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeConstCat", "table233", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeConstCatToolTip")))


Response.Write("</TD>	" & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=101487>" & GetLocalResourceObject("tctFloor_quanCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tctFloor_quan", 4, "",  , "", True, 2))


Response.Write("</TD>          " & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=101488>" & GetLocalResourceObject("cbeCombTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCombType", "table7040", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCombTypeToolTip")))


Response.Write("</TD>	           " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101489>" & GetLocalResourceObject("cbeSideCloseTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeSideCloseType", "table7037", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSideCloseTypeToolTip")))


Response.Write("</TD>	" & vbCrLf)
Response.Write("            <TD></TD>                    " & vbCrLf)
Response.Write("            <TD><LABEL ID=101490>" & GetLocalResourceObject("tctIndPeriodCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tctIndPeriod", 4, "",  , "", True, 2))


Response.Write("</TD>          " & vbCrLf)
Response.Write("            <TD></TD>                    " & vbCrLf)
Response.Write("            <TD><LABEL ID=101491>" & GetLocalResourceObject("cbeRoofTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeRoofType", "table7038", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRoofTypeToolTip")))


Response.Write("</TD>	           " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        </TABLE> " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%""" & vbCrLf)
Response.Write("")

	
End Sub


'% insPreBVC001: Se cargan los datos en el grid de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreINC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsFire As eBranches.Fire
	Dim lcolFires As eBranches.Fires
	Dim lCountReg As Short
	Dim lclsacc As Object
	
	lclsFire = New eBranches.Fire
	lcolFires = New eBranches.Fires
	With Request
		If Not IsNothing(.QueryString("nBranch")) Then
			If lcolFires.Find_INC001_K("INC001", "QUERY", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPayFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.QueryString.Item("dEffecDate")), mobjValues.StringToDate(.QueryString.Item("dNullDate")), mobjValues.StringToType(.QueryString.Item("nTypePolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nArticle"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nDetailArt"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nActivityCat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nConstCat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nFloor_quan"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSpCombType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSideCloseType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nIndPeriod"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nRoofType"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				lCountReg = 1
				
Response.Write("<DIV ID=""Scroll"" style=""width:615;height:90;overflow:auto; outset gray"">")

				
				For	Each lclsFire In lcolFires
					With lclsFire
						mobjGrid.Columns("txtBranchgrid").DefValue = .sBranchName
						mobjGrid.Columns("tcnPolicy").DefValue = CStr(.npolicy)
						mobjGrid.Columns("tcnCertif").DefValue = CStr(.ncertif)
						mobjGrid.Columns("txtinsur").DefValue = .sCliename
						mobjGrid.Columns("txtBranchgrid").HRefScript = "ShowPopUp('/VTimeNet/Branches/BranchQue/ShowDefValues.aspx?sCodispl=INC001&nBranch=" & lclsFire.nBranch & "&nProduct=" & lclsFire.nProduct & "&ncertif=" & lclsFire.ncertif & "&nPolicy=" & lclsFire.npolicy & "','showdefvalueAuto',1, 1,'no','no',1000,1000)"
						
					End With
					Response.Write(mobjGrid.DoRow())
					lCountReg = lCountReg + 1
					If lCountReg = 100 Then
						Exit For
					End If
				Next lclsFire
				
Response.Write("</DIV>")

				
			End If
		End If
		Response.Write(mobjGrid.closeTable())
		Call insDefineHeader()
	End With
	lclsFire = Nothing
	lcolFires = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "inc001_k"
%>
<HTML>
<HEAD>

	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>

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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "INC001_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="post" ID="FORM" NAME="frmAutosPolInq" ACTION="ValBranchQue.aspx?X=1">
<%Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreINC001()
End If
Call insDefineHeader1()
mobjGrid = Nothing
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





