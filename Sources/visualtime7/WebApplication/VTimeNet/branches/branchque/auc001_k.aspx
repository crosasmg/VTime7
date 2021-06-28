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
	mobjGrid.sCodisplPage = "auc001_k"
	
	
Response.Write(" <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR><TD ALIGN=""CENTER"" COLSPAN=10>" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("        ")

	With mobjGrid.Columns
		Call .AddAnimatedColumn(0, vbNullString, "btnFolder", "/VTimeNet/images/clfolder.png", GetLocalResourceObject("btnFolderColumnToolTip"))
		Call .AddTextColumn(101184, GetLocalResourceObject("txtBranchgridColumnCaption"), "txtBranchgrid", 10, " ",  ,  ,  ,  ,  , True)
		Call .AddTextColumn(101185, GetLocalResourceObject("txtProductColumnCaption"), "txtProduct", 10, " ",  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(101182, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddNumericColumn(101183, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 15, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		Call .AddTextColumn(101186, GetLocalResourceObject("txtinsurColumnCaption"), "txtinsur", 10, "Cliente Inexistente",  ,  ,  ,  ,  , True)
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

'------------------------------------------------------------------------------
Private Sub insDefineHeader1()
	'------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD WIDTH = ""20%""><LABEL ID=101159>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD WIDTH = ""25%"">")

	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"),  ,  ,  ,  , "inschangebranch", True, 1))
Response.Write(" </TD>" & vbCrLf)
Response.Write("		<TD WIDTH = ""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD WIDTH = ""45%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=101168><A NAME=""Patente"">" & GetLocalResourceObject("AnchorPatenteCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>	" & vbCrLf)
Response.Write("	    <TD COLSPAN=""3""></TD> " & vbCrLf)
Response.Write("	    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("    </TR>  " & vbCrLf)
Response.Write("	<TR>	" & vbCrLf)
Response.Write("	  	<TD><LABEL ID=101160>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("  		<TD>")

	Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True, Request.QueryString.Item("nProduct"),  ,  ,  ,  , 2))
Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    ")

	If Request.QueryString.Item("sLicense") = "1" Or IsNothing(Request.QueryString.Item("sLicense")) Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101180, "optLicence", GetLocalResourceObject("optLicence_CStr1Caption"), CStr(1), CStr(1),  , True, 11))


Response.Write("</TD> " & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101180, "optLicence", GetLocalResourceObject("optLicence_CStr1Caption"), CStr(2), CStr(1),  , True, 11))


Response.Write("</TD>     " & vbCrLf)
Response.Write("		")

	End If
Response.Write("     " & vbCrLf)
Response.Write("    </TR>  " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""></TD>            				" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		")

	If Request.QueryString.Item("sLicense") = "2" Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101181, "optLicence", GetLocalResourceObject("optLicence_CStr2Caption"), CStr(1), CStr(2),  , True, 12))


Response.Write("</TD> " & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101181, "optLicence", GetLocalResourceObject("optLicence_CStr2Caption"), CStr(2), CStr(2),  , True, 12))


Response.Write("</TD>    " & vbCrLf)
Response.Write("		")

	End If
Response.Write(" " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=101161><A NAME=""Poltype"">" & GetLocalResourceObject("AnchorPoltypeCaption") & "</A></LABEL></TD>            				" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		")

	If Request.QueryString.Item("sLicense") = "3" Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101187, "optLicence", GetLocalResourceObject("optLicence_CStr3Caption"), CStr(1), CStr(3),  , True, 12))


Response.Write("</TD> " & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101187, "optLicence", GetLocalResourceObject("optLicence_CStr3Caption"), CStr(2), CStr(3),  , True, 12))


Response.Write("</TD>    " & vbCrLf)
Response.Write("		")

	End If
Response.Write(" " & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("	</TR>	" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		")

	If Request.QueryString.Item("sTypePolicy") = "1" Or IsNothing(Request.QueryString.Item("sTypePolicy")) Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101177, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr1Caption"), CStr(1), CStr(1),  , True, 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101177, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr1Caption"), CStr(2), CStr(1),  , True, 3))


Response.Write("</TD>   " & vbCrLf)
Response.Write("		")

	End If
Response.Write("		" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101171>" & GetLocalResourceObject("tctRegisterCaption") & "</LABEL></TD>                              " & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRegister", 10, Request.QueryString.Item("sRegister"),  , GetLocalResourceObject("tctRegisterToolTip"),  ,  ,  ,  , True, 13))


Response.Write("</TD>		" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		")

	If Request.QueryString.Item("sTypePolicy") = "2" Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101178, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr2Caption"), CStr(1), CStr(2),  , True, 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101178, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr2Caption"), CStr(2), CStr(2),  , True, 4))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	End If
Response.Write(" 		" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>		" & vbCrLf)
Response.Write("		<TD><LABEL ID=101169>" & GetLocalResourceObject("tctMotorCaption") & "</LABEL></TD>                            " & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctMotor", 40, Request.QueryString.Item("sMotor"),  , GetLocalResourceObject("tctMotorToolTip"),  ,  ,  ,  , True, 14))


Response.Write("</TD>	" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		")

	If Request.QueryString.Item("sTypePolicy") = "3" Then
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101179, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr3Caption"), CStr(1), CStr(3),  , True, 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		    <TD>")


Response.Write(mobjValues.OptionControl(101179, "optTypePolicy", GetLocalResourceObject("optTypePolicy_CStr3Caption"), CStr(2), CStr(3),  , True, 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	End If
Response.Write(" 			" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("  		<TD><LABEL ID=101170>" & GetLocalResourceObject("tctChassisCaption") & "</LABEL></TD>                             " & vbCrLf)
Response.Write("  		<TD>")


Response.Write(mobjValues.TextControl("tctChassis", 40, Request.QueryString.Item("sChassis"),  , GetLocalResourceObject("tctChassisToolTip"),  ,  ,  ,  , True, 15))


Response.Write("</TD>		" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>    " & vbCrLf)
Response.Write("        <TD><LABEL ID=101162>" & GetLocalResourceObject("cbePayFreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayFreq", "table36", 1, Request.QueryString.Item("nPayFreq"),  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbePayFreqToolTip"),  , 6))


Response.Write("</TD>	" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101172>" & GetLocalResourceObject("tctColorCaption") & "</LABEL></TD>                            " & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctColor", 15, Request.QueryString.Item("sColor"),  , GetLocalResourceObject("tctColorToolTip"),  ,  ,  ,  , True, 16))


Response.Write("</TD>        " & vbCrLf)
Response.Write("    </TR> " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=101163><A NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101173>" & GetLocalResourceObject("cbeVehMarkCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeVehMark", "table7042", 1, Request.QueryString.Item("nVehMark"),  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeVehMarkToolTip"),  , 17))


Response.Write("</TD>		" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>	    " & vbCrLf)
Response.Write("	    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101165>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>					" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DateControl("tcdEffecDate", Request.QueryString.Item("dEffectDate"),  , GetLocalResourceObject("tcdEffecDateToolTip"),  ,  ,  ,  , True, 7))


Response.Write("</TD>	    		" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101174>" & GetLocalResourceObject("tctLVehModelCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctLVehModel", 20, Request.QueryString.Item("sVehModel"),  , GetLocalResourceObject("tctLVehModelToolTip"),  ,  ,  ,  , True, 18))


Response.Write("</TD>    " & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101167>" & GetLocalResourceObject("tcdNullDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DateControl("tcdNullDate", Request.QueryString.Item("dNullDate"),  , GetLocalResourceObject("tcdNullDateToolTip"),  ,  ,  ,  , True, 8))


Response.Write("</TD>      		" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101175>" & GetLocalResourceObject("cbeTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeType", "table226", 1, Request.QueryString.Item("nType"),  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeTypeToolTip"),  , 19))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>	" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101164>" & GetLocalResourceObject("tctCapitalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tctCapital", 18, Request.QueryString.Item("nCapital"),  , GetLocalResourceObject("tctCapitalToolTip"), True, 6,  ,  ,  ,  , True, 9))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101176>" & GetLocalResourceObject("cbeZoneCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeZone", "table224", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nZone"),  ,  ,  ,  ,  ,  , True, 10, GetLocalResourceObject("cbeZoneToolTip"),  , 20))


Response.Write("</TD>                       " & vbCrLf)
Response.Write("        </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101166>" & GetLocalResourceObject("tctPremiumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.NumericControl("tctPremium", 18, Request.QueryString.Item("nPremium"),  , GetLocalResourceObject("tctPremiumToolTip"), True, 6,  ,  ,  ,  , True, 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
End Sub

'% insPreBVC001: Se cargan los datos en el grid de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreAUC001()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As eBranches.Auto_db
	Dim lcolAuto_dbs As eBranches.Auto_dbs
	Dim lCountReg As Short
	Dim lclsacc As Object
	
	lclsAuto_db = New eBranches.Auto_db
	lcolAuto_dbs = New eBranches.Auto_dbs
	With Request
		If Not IsNothing(.QueryString("nBranch")) Then
			If lcolAuto_dbs.Find_AUC001_K(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPayFreq"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nPremium"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToDate(.QueryString.Item("dEffecDate")), mobjValues.StringToDate(.QueryString.Item("dNullDate")), .QueryString.Item("sTypePolicy"), .QueryString.Item("sLicense"), .QueryString.Item("sRegister"), .QueryString.Item("sMotor"), .QueryString.Item("sChassis"), .QueryString.Item("sColor"), mobjValues.StringToType(.QueryString.Item("nMark"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString.Item("sVehModel"), mobjValues.StringToType(.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nZone"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				lCountReg = 1
				
Response.Write("<DIV ID=""Scroll"" STYLE=""width:100percent;height:90;overflow:auto; outset gray"">")

				
				For	Each lclsAuto_db In lcolAuto_dbs
					With lclsAuto_db
						mobjGrid.Columns("txtBranchgrid").DefValue = .sBranchName
						mobjGrid.Columns("txtProduct").DefValue = .sDescript
						mobjGrid.Columns("tcnPolicy").DefValue = CStr(.npolicy)
						mobjGrid.Columns("tcnCertif").DefValue = CStr(.ncertif)
						mobjGrid.Columns("txtinsur").DefValue = .sCliename
						mobjGrid.Columns("txtBranchgrid").HRefScript = "insOpenFolder('" & CStr(lCountReg - 1) & "');insDefValues('AUC001','sCodispl=AUC001&nBranch=" & lclsAuto_db.nBranch & "&nProduct=" & lclsAuto_db.nProduct & "&ncertif=" & lclsAuto_db.ncertif & "&nPolicy=" & lclsAuto_db.npolicy & "','/VTimeNet/Branches/BranchQue')"
						mobjGrid.Columns("btnFolder").HRefScript = mobjGrid.Columns("txtBranchgrid").HRefScript
					End With
					Response.Write(mobjGrid.DoRow())
					lCountReg = lCountReg + 1
					If lCountReg = 100 Then
						Exit For
					End If
				Next lclsAuto_db
				
Response.Write("</DIV>")

				
			End If
		End If
		Response.Write(mobjGrid.closeTable())
		Call insDefineHeader()
	End With
	lclsAuto_db = Nothing
	lcolAuto_dbs = Nothing
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "auc001_k"
%>
<HTML>
<HEAD>

	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 14/11/03 12:54 $|$$Author: Nvaplat18 $"

var mlngCurrentFolder=''

//--------------------------------------------------------------------------------------------------
function insOpenFolder(llngIndex){
//--------------------------------------------------------------------------------------------------
    if (typeof(document.btnFolder.length)!='undefined'){
        if (mlngCurrentFolder!='')
            document.btnFolder[mlngCurrentFolder].src=document.btnFolder[mlngCurrentFolder].src.replace("opfolder","clfolder")
        document.btnFolder[llngIndex].src=document.btnFolder[llngIndex].src.replace("clfolder","opfolder")
        mlngCurrentFolder = llngIndex
    }
    else
        document.btnFolder.src=document.btnFolder.src.replace("clfolder","opfolder")
}

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
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=false;
			if ((self.document.forms[0].elements[lintIndex].name!='cbeBranch')&&(self.document.forms[0].elements[lintIndex].name.substring(0,3)!='opt')&&(self.document.forms[0].elements[lintIndex].name!='valProduct'))
			    self.document.forms[0].elements[lintIndex].value = ''
			if(self.document.images.length>0)
			    if(typeof(self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn_" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
			    if(typeof(self.document.images["btn" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled 
		}
	 }catch(error){}	
}

//--------------------------------------------------------------------------------------------------
//% inschangebranch: Función que deshabilita todo los demas campos
function inschangebranch(){	   
//--------------------------------------------------------------------------------------------------
    with (self.document.forms["AUC001"]){
        elements["valProduct"].disabled= false
        elements["cbePayFreq"].disabled= false
        elements["cbeType"].disabled   = false
        elements["cbeZone"].disabled   = false
        elements["tctMotor"].disabled  = false                
        elements["tctCapital"].disabled= false            
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AUC001_K.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAutosPolInq" ACTION="ValBranchQue.aspx?X=1">
<BR><BR>
	<%=mobjValues.ShowWindowsName("AUC001")%>
<%
Call insDefineHeader1()
Call insDefineHeader()
Call insPreAUC001()
mobjGrid = Nothing
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>







