<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para menjo de grid
Dim mobjGrid As eFunctions.Grid

'- Variables para almacenar parametros de pagina
Dim mstrCertype As String
Dim mintBranch As String
Dim mintProduct As String
Dim mlngPolicy As String
Dim mlngCertif As String

'- Variables utilizadas para guardar el valor de los distintos checked 
Dim nInd_cobra As Object
Dim nGen_cobra As Object


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "poldata"
	
	With mobjGrid
		.Codispl = "SCA003"
		.AddButton = False
		.DeleteButton = False
	End With
	With mobjGrid.Columns
		Call .AddClientColumn(40536, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString)
		Call .AddPossiblesColumn(40535, GetLocalResourceObject("tcnRoleColumnCaption"), "tcnRole", "Table12", eFunctions.Values.eValuesType.clngComboType, vbNullString)
	End With
	mobjGrid.Columns("Sel").GridVisible = False
End Sub

'% insPreSCA003: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA003()
	'--------------------------------------------------------------------------------------------
	Dim lcolRoles As ePolicy.Roleses
	Dim lclsRoles As ePolicy.Roles
	
	lclsRoles = New ePolicy.Roles
	lcolRoles = New ePolicy.Roleses
	
	Dim lclsProduct As eProduct.Product
	lclsProduct = New eProduct.Product
	
	Dim lclsCertificat As ePolicy.Certificat
	lclsCertificat = New ePolicy.Certificat
	
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	
	Dim lclsPremium As eCollection.Premium
	lclsPremium = New eCollection.Premium
	
	Call lclsProduct.FindProdMaster(mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble))
	
	Call lclsCertificat.Find(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble))
	
	Call lclsPolicy.Find(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble))
	
	Call lclsPremium.Find_Premium_CA001(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble))
	
        If lclsPremium.nWay_Pay = 1 Or lclsPremium.nWay_Pay = 2 And lclsPremium.nBulletins <> CDbl(eRemoteDB.Constants.intNull) Then
		
        End If
	
Response.Write("" & vbCrLf)
Response.Write("<A NAME=""BeginPage""></A>")


Response.Write(mobjValues.ShowWindowsName("SCA003"))


Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=13826>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPolicy.nOffice)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=13819>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCertificat.nBranch)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=13827>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	Response.Write(mobjValues.NumericControl("tcnPolicy", 10, CStr(lclsCertificat.nPolicy),  ,  , False, 0) & " - " & mobjValues.NumericControl("tcnDigit", 1, CStr(lclsCertificat.nDigit),  , "Digito Verificador de la Póliza", False,  ,  ,  ,  , "", True))
Response.Write("</TD>" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("				" & vbCrLf)
Response.Write("		" & vbCrLf)
Response.Write("		<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=13820>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnCertif", 10, CStr(lclsCertificat.nCertif)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=13823>" & GetLocalResourceObject("tcdIssuedateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.DateControl("tcdIssuedate", mobjValues.TypeToString(lclsPolicy.dIssuedat, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL ID=0>" & GetLocalResourceObject("tcnQuotPropCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnQuotProp", 10, CStr(lclsCertificat.nPolicy),  ,  , False, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted"" ><LABEL ID=40533>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40534>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2""><HR></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>        " & vbCrLf)
Response.Write("        <TD><LABEL ID=13821>" & GetLocalResourceObject("tcdFromDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.DateControl("tcdFromDate", mobjValues.TypeToString(lclsCertificat.dStartdate, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=13825>" & GetLocalResourceObject("tcdNullDatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.DateControl("tcdNullDat", mobjValues.TypeToString(lclsCertificat.dNulldate, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=13822>" & GetLocalResourceObject("tcdToDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.DateControl("tcdToDate", mobjValues.TypeToString(lclsCertificat.dExpirdat, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL ID=13824>" & GetLocalResourceObject("cbeNulldescCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeNulldesc", "Table13", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCertificat.nNullcode)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    ")

	If CStr(lclsProduct.sBrancht) = "3" Or CStr(lclsProduct.sBrancht) = "4" Then
Response.Write("          " & vbCrLf)
Response.Write("    <TR>    " & vbCrLf)
Response.Write("		<TD COLSPAN=""3"">&nbsp;</TD> 		 		   " & vbCrLf)
Response.Write("		<TD><LABEL ID=13822>" & GetLocalResourceObject("tcnRenewalNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnRenewalNum", 5, CStr(lclsCertificat.nRenewalnum),  , GetLocalResourceObject("tcnRenewalNumToolTip"),  , 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    ")

	End If
Response.Write("" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted"" ><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"" CLASS=""HighLighted"" ><LABEL ID=0>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>        " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2""><HR></TD>    " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2""><HR></TD>    " & vbCrLf)
Response.Write("    </TR>    " & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcdNextReceipCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.DateControl("tcdNextReceip", mobjValues.TypeToString(lclsCertificat.dNextReceip, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeWay_payCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeWay_pay", "Table5002", eFunctions.Values.eValuesType.clngComboType, mobjValues.TypeToString(lclsCertificat.nWay_Pay, eFunctions.Values.eTypeData.etdDouble)))


Response.Write("</TD>	" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcdChangdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.DateControl("tcdChangdat", mobjValues.TypeToString(lclsCertificat.dChangdat, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>		    " & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcnReceiptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnReceipt", 10, mobjValues.TypeToString(lclsPremium.nReceipt, eFunctions.Values.eTypeData.etdDouble)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>        " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=0>" & GetLocalResourceObject("tcdFerCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.DateControl("tcdFer", mobjValues.TypeToString(lclsCertificat.dFer, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>		    		    " & vbCrLf)
Response.Write("	    <TD><LABEL ID=0>" & GetLocalResourceObject("tcdExpirdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.DateControl("tcdExpirdat", mobjValues.TypeToString(lclsPremium.dExpirdat, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>		    		    " & vbCrLf)
Response.Write("	    <TD>&nbsp;</TD>		    		    " & vbCrLf)
Response.Write("	    ")

	
	'If lclsPremium.nBulletins <> CDbl("") Then
    If lclsPremium.nBulletins <> CDbl(eRemoteDB.Constants.intNull) Then
		nGen_cobra = 1
		If lclsPremium.nWay_Pay = 1 Or lclsPremium.nWay_Pay = 2 Then
			nInd_cobra = 1
		Else
			nInd_cobra = 2
		End If
	Else
		nGen_cobra = 2
		nInd_cobra = 2
	End If
	
Response.Write("" & vbCrLf)
Response.Write("	    <TD></TD>" & vbCrLf)
Response.Write("	    <TD>" & vbCrLf)
Response.Write("	    ")


Response.Write(mobjValues.CheckControl("chkind_cobra", GetLocalResourceObject("chkind_cobraCaption"), nGen_cobra, mobjValues.TypeToString(lclsCertificat.dFer, eFunctions.Values.eTypeData.etdDate)))


Response.Write("" & vbCrLf)
Response.Write("	    </TD><TD>" & vbCrLf)
Response.Write("	    ")


Response.Write(mobjValues.CheckControl("chkind_cobra", GetLocalResourceObject("chkind_cobraCaption"), nInd_cobra, mobjValues.TypeToString(lclsCertificat.dFer, eFunctions.Values.eTypeData.etdDate)))


Response.Write("" & vbCrLf)
Response.Write("	    </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3"" CLASS=""HighLighted"" ><LABEL ID=0>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3""><HR></TD>    " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("")

	
	If lcolRoles.Find_by_Policy(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), vbNullString, Today) Then
		For	Each lclsRoles In lcolRoles
			With mobjGrid
				.Columns("dtcClient").DefValue = lclsRoles.sClient
				.Columns("tcnRole").DefValue = CStr(lclsRoles.nRole)
				Response.Write(.DoRow)
			End With
		Next lclsRoles
	End If
	
	With Response
		
		.Write(mobjGrid.closeTable())
		.Write("<P ALIGN=""RIGHT"">")
		mobjValues.ActionQuery = False
		.Write(mobjValues.ButtonAcceptCancel("window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyAccept))
		.Write("</P>")
		
		.Write("<P ALIGN=""CENTER"">")
		.Write(mobjValues.BeginPageButton)
		.Write("</P>")
		
		.Write("<P ALIGN=""RIGHT"">")
		.Write(mobjValues.ButtonAbout("SCA003"))
		.Write("</P>")
		
	End With
	
	lcolRoles = Nothing
	lclsRoles = Nothing
	lclsCertificat = Nothing
	lclsPolicy = Nothing
	lclsProduct = Nothing
	lclsPremium = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

'+ Se deja la pagina en modo consulta     
mobjValues.ActionQuery = True

'+ Se asignan valores de parámetros     
mstrCertype = Request.QueryString.Item("sCertype")
mintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
mintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
mlngPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
mlngCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)

mobjValues.sCodisplPage = "poldata"
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.34 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
    <%=mobjValues.WindowsTitle("SCA003")%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPolData" ACTION="PolData.aspx">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreSCA003()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






