<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

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
Response.Write("" & vbCrLf)
Response.Write("    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39" & vbCrLf)
Response.Write("    mobjGrid.sSessionID = Session.SessionID" & vbCrLf)
Response.Write("    mobjGrid.nUsercode = Session(""nUsercode"")" & vbCrLf)
Response.Write("    '~End Body Block VisualTimer Utility" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    mobjGrid.sCodisplPage = ""vic005_k""" & vbCrLf)
Response.Write("    Call mobjGrid.SetWindowParameters(Request.QueryString(""sCodispl""),  Request.QueryString(""sWindowDescript""), Request.QueryString(""nWindowTy""))" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR><TD ALIGN=""CENTER"" COLSPAN=10>" & vbCrLf)
Response.Write("     ")

	With mobjGrid.Columns
		Call .AddTextColumn(102074, GetLocalResourceObject("txtBranchgridColumnCaption"), "txtBranchgrid", 10, "",  ,  ,  ,  ,  , True)
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
	
	
Response.Write("" & vbCrLf)
Response.Write("        </TD></TR>   " & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("   </DIV>")

	
End Sub

Private Sub insDefineHeader1()
	
Response.Write("    " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR><BR><BR></TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102054>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", 1, CStr(40),  ,  ,  ,  ,  , "self.document.forms[0].valProduct.Parameters.Param1.sValue=this.value", True,  , GetLocalResourceObject("cbeBranchToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=8%>&nbsp;</TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=102055>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("              <TD>")

	With mobjValues
		.Parameters.Add("mintBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valProductToolTip")))
	End With
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>            " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=102056><A NAME=""Tipo de Póliza"">" & GetLocalResourceObject("AnchorTipo de PólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL ID=102057><A NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>            " & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""4"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("        <TR>    " & vbCrLf)
Response.Write("            <TD WIDTH=""30%"">")


Response.Write(mobjValues.OptionControl(102070, "optTypePol", GetLocalResourceObject("optTypePol_1Caption"), CStr(1), "1",  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102058>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""30%"">")


Response.Write(mobjValues.OptionControl(102071, "optTypePol", GetLocalResourceObject("optTypePol_2Caption"),  , "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""20%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102059>" & GetLocalResourceObject("tcdExpirdatCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>            " & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">        " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102060>" & GetLocalResourceObject("cbePayfreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayfreq", "table36", 1,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbePayfreqToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102061>" & GetLocalResourceObject("tcnCapitalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tcnCapital", 14, "",  , "",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102062>" & GetLocalResourceObject("tcnAge_reinsuCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tcnAge_reinsu", 3, "",  , "",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102063>" & GetLocalResourceObject("tcnAgeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tcnAge", 3, "",  , "",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=102064>" & GetLocalResourceObject("tcnPremiumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tcnPremium", 12, "",  , "",  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
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
		If Not IsNothing(.QueryString("nBranch")) Then
			
			lblnFind = lclsLife.Find_VIC005_k(.QueryString.Item("sCertype"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypePolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.QueryString.Item("dEffecDate")), mobjValues.StringToDate(.QueryString.Item("dExpirdat")), mobjValues.StringToType(.QueryString.Item("nPayFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nAge"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nAge_reinsu"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble, True))
			If lblnFind Then
				lCountReg = 1
				lintCount = 0
				
Response.Write("<DIV ID=""Scroll"" style=""height:90;overflow:auto; outset gray"">")

				
				For lintCount = 0 To lclsLife.Count - 1
					If lclsLife.ItemPolLife(lintCount) Then
						With mobjGrid
							.Columns("txtBranchgrid").DefValue = lclsLife.sDesBranch
							.Columns("tcnPolicy").DefValue = CStr(lclsLife.npolicy)
							.Columns("tcnCertif").DefValue = CStr(lclsLife.ncertif)
							.Columns("txtClient").DefValue = lclsLife.sClient
							.Columns("txtClientName").DefValue = lclsLife.sCliename
							.Columns("tcnPolicy").HRefScript = "ShowPopUp('/VTimeNet/Branches/BranchQue/ShowDefValues.aspx?sCodispl=VIC005&nBranch=" & lclsLife.nBranch & "&nProduct=" & lclsLife.nProduct & "&nCertif=" & lclsLife.ncertif & "&nPolicy=" & lclsLife.npolicy & "&dEffecdate=" & lclsLife.dEffecdate & "','ShowDefValueLife',20, 20,'no','no',2000,2000)"
						End With
						Response.Write(mobjGrid.DoRow())
						lCountReg = lCountReg + 1
						If lCountReg = 100 Then
							Exit For
						End If
					End If
				Next 
				
Response.Write("</DIV>")

				
			End If
		End If
		Response.Write(mobjGrid.closeTable())
		Call insDefineHeader()
	End With
	
	lclsLife = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vic005_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vic005_k"

%>


<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
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
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "VIC005_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
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
<FORM METHOD="post" ID="FORM" NAME="frmPoliciesQuery" ACTION="ValBranchQue.aspx?x=1">
<%Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreVIC005()
End If
Call insDefineHeader1()
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vic005_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




