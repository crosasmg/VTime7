<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objetos para menjo de grid
Dim mobjCoverGrid As eFunctions.Grid
Dim mobjFunds_polGrid As eFunctions.Grid
Dim mobjPlan_payGrid As eFunctions.Grid
Dim mobjShowVulGrid As eFunctions.Grid

'- Variables para almacenar parametros de pagina
Dim mstrCertype As String
Dim mintBranch As Object
Dim mintProduct As Object
Dim mlngPolicy As Object
Dim mlngCertif As Object

'- Variables utilizadas para guardar valores del repote 
Dim nInd_smoking As Object
Dim mstrWinName As String
Dim mstrTotalPrima As Byte
Dim mstrQueryGraph As String

Dim nGuaranty As Byte

Dim mintLine As Double
Dim i As Object
Dim nIntwarr2 As Double
Dim nIntwarrClear As Double
Dim lclsProduct As eProduct.Product
Dim lclsProduct1 As eProduct.Plan_IntWar


'% insCoverGrid: se definen los campos del grid de Coberturas
'--------------------------------------------------------------------------------------------
Private Sub insCoverGrid()
	'--------------------------------------------------------------------------------------------
	mobjCoverGrid = New eFunctions.Grid
	
	mobjCoverGrid.sCodisplPage = "CoverGrid"
	
	With mobjCoverGrid
		.Codispl = "SCAL001"
		
		.Top = 5
		.Left = 10
		.Width = 770
		.Height = 520
		
		.Splits_Renamed.AddSplit(0, "", 2)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		
		.AddButton = False
		.DeleteButton = False
		
	End With
	With mobjCoverGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", 100, vbNullString,  , GetLocalResourceObject("tctCoverColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0), True, GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremium2ColumnCaption"), "tcnPremium2", 18, CStr(0), True, GetLocalResourceObject("tcnPremium2ColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremium4ColumnCaption"), "tcnPremium4", 18, CStr(0), True, GetLocalResourceObject("tcnPremium4ColumnToolTip"), True, 2)
	End With
	mobjCoverGrid.Columns("Sel").GridVisible = False
	
End Sub

'% insFunds_polGrid: se definen los campos del grid de fondos
'--------------------------------------------------------------------------------------------
Private Sub insFunds_polGrid()
	'--------------------------------------------------------------------------------------------
	mobjFunds_polGrid = New eFunctions.Grid
	
	mobjFunds_polGrid.sCodisplPage = "CoverGrid"
	
	With mobjFunds_polGrid
		.Codispl = "SCAL001"
		
		.Top = 5
		.Left = 10
		.Width = 770
		.Height = 520
		
		.Splits_Renamed.AddSplit(0, "", 2)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		
		.AddButton = False
		.DeleteButton = False
		
	End With
	With mobjFunds_polGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctOrigyColumnCaption"), "tctOrigy", 100, vbNullString,  , GetLocalResourceObject("tctOrigyColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 100, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnintproyColumnCaption"), "tcnintproy", 18, CStr(0), True, GetLocalResourceObject("tcnintproyColumnToolTip"), True, 2)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnParticipColumnCaption"), "tcnParticip", 18, CStr(0), True, GetLocalResourceObject("tcnParticipColumnToolTip"), True, 2)
	End With
	mobjFunds_polGrid.Columns("Sel").GridVisible = False
	
End Sub

'% insPlan_payGrid: se definen los campos del grid de Plan de pagos
'--------------------------------------------------------------------------------------------
Private Sub insPlan_payGrid()
	'--------------------------------------------------------------------------------------------
	mobjPlan_payGrid = New eFunctions.Grid
	
	mobjPlan_payGrid.sCodisplPage = "CoverGrid"
	
	With mobjPlan_payGrid
		.Codispl = "SCAL001"
		
		.Top = 5
		.Left = 10
		.Width = 770
		.Height = 520
		
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		'.Splits_Renamed.AddSplit 0,"Aporte",4
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("1ColumnCaption"), 1)
		
		.AddButton = False
		.DeleteButton = False
	End With
	With mobjPlan_payGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_iniColumnCaption"), "tcnYear_ini", 5, vbNullString,  , GetLocalResourceObject("tcnYear_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnYear_endColumnCaption"), "tcnYear_end", 5, vbNullString,  , GetLocalResourceObject("tcnYear_endColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep3ColumnCaption"), "tcnAmountdep3", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep3ColumnToolTip"), True, 2,  ,  , "InsDisPremdel(11, this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep1ColumnCaption"), "tcnAmountdep1", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep1ColumnToolTip"), True, 2,  ,  , "InsDisPremdel(9, this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdepColumnCaption"), "tcnAmountdep", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdepColumnToolTip"), True, 2,  ,  , "InsDisPremdel(8, this.value, '')")
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremBasicColumnCaption"), "tcnPremBasic", 18, vbNullString,  , GetLocalResourceObject("tcnPremBasicColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnPremSavingColumnCaption"), "tcnPremSaving", 18, vbNullString,  , GetLocalResourceObject("tcnPremSavingColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnRecamountColumnCaption"), "tcnRecamount", 18, vbNullString,  , GetLocalResourceObject("tcnRecamountColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdep_auxColumnCaption"), "tcnAmountdep_aux", 18, vbNullString,  , GetLocalResourceObject("tcnAmountdep_auxColumnToolTip"), True, 2)
	End With
	
	mobjPlan_payGrid.Columns("Sel").GridVisible = False
	
End Sub

'% InsShowVulGrid: se definen las propiedades del grid de Ilustracion del VUL
'--------------------------------------------------------------------------------------------
Private Sub InsShowVulGrid()
	'--------------------------------------------------------------------------------------------
	mobjShowVulGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid
	With mobjShowVulGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, vbNullString,  , GetLocalResourceObject("tcnYearColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAge_reinsuColumnCaption"), "tcnAge_reinsu", 3, vbNullString,  , GetLocalResourceObject("tcnAge_reinsuColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmodepacumColumnCaption"), "tcnAmodepacum", 18, vbNullString,  , GetLocalResourceObject("tcnAmodepacumColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmodepacum2ColumnCaption"), "tcnAmodepacum2", 18, vbNullString,  , GetLocalResourceObject("tcnAmodepacum2ColumnToolTip"), True, 2)
		
		If nGuaranty = 1 Then
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpolig2ColumnCaption"), "tcnValpolig2", 18, vbNullString,  , GetLocalResourceObject("tcnValpolig2ColumnToolTip"), True, 2)
			'.AddHiddenColumn "tcnValpoliga2",""
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpoliga2ColumnCaption"), "tcnValpoliga2", 18, vbNullString,  , GetLocalResourceObject("tcnValpoliga2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnValsurig2ColumnCaption"), "tcnValsurig2", 18, vbNullString,  , GetLocalResourceObject("tcnValsurig2ColumnToolTip"), True, 2)
			'.AddHiddenColumn "tcnProdeathig2",""
			.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathig2ColumnCaption"), "tcnProdeathig2", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathig2ColumnToolTip"), True, 2)
			
		End If
		
		.AddNumericColumn(0, GetLocalResourceObject("tcnValpoligColumnCaption"), "tcnValpolig", 18, vbNullString,  , GetLocalResourceObject("tcnValpoligColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValpoligaColumnCaption"), "tcnValpoliga", 18, vbNullString,  , GetLocalResourceObject("tcnValpoligaColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValsurigColumnCaption"), "tcnValsurig", 18, vbNullString,  , GetLocalResourceObject("tcnValsurigColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathigColumnCaption"), "tcnProdeathig", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathigColumnToolTip"), True, 2)
		
		If nGuaranty = 0 Then
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpolig2ColumnCaption"), "tcnValpolig2", 18, vbNullString,  , GetLocalResourceObject("tcnValpolig2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnValpoliga2ColumnCaption"), "tcnValpoliga2", 18, vbNullString,  , GetLocalResourceObject("tcnValpoliga2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnValsurig2ColumnCaption"), "tcnValsurig2", 18, vbNullString,  , GetLocalResourceObject("tcnValsurig2ColumnToolTip"), True, 2)
			.AddNumericColumn(0, GetLocalResourceObject("tcnProdeathig2ColumnCaption"), "tcnProdeathig2", 18, vbNullString,  , GetLocalResourceObject("tcnProdeathig2ColumnToolTip"), True, 2)
		End If
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjShowVulGrid
		.Codispl = "VIL1410"
		'        .Height = 350
		'        .Width = 280
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		
		.Splits_Renamed.AddSplit(0, "", 4)
		
		If nGuaranty = 1 Then
			.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
			.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		Else
			.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
			.Splits_Renamed.AddSplit(0, GetLocalResourceObject("4ColumnCaption"), 4)
		End If
	End With
End Sub

'% insPrePrintPol: se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPrePrintPol()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsPolicy As ePolicy.Certificat
	Dim lclsCertificat As ePolicy.Life
	Dim lintGuaranty As Short
	Dim lstrName As String
	Dim lcolPer_deposit As ePolicy.Per_deposits
	Dim lclsPer_deposit As Object
	Dim ldblPrem_deal As Double
	Dim llngYear_end As Object
	
	lcolPer_deposit = New ePolicy.Per_deposits
	
	lclsPolicy = New ePolicy.Certificat
	lclsCertificat = New ePolicy.Life
	
	Dim lcolCover As ePolicy.Covers
	Dim lclsCover As Object
	lcolCover = New ePolicy.Covers
	
	Dim lcolFunds_pol As ePolicy.Funds_pols
	Dim lclsFunds_pol As Object
	lcolFunds_pol = New ePolicy.Funds_pols
	
	Dim lcolProjectvul As ePolicy.Projectvuls
	Dim lclsProjectvul As Object
	lcolProjectvul = New ePolicy.Projectvuls
	
	Call lclsPolicy.insReaPrintVUL(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble))
	Call lclsCertificat.insPreVI7006(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))
	
	If lclsPolicy.sSmoking = "1" Then
		nInd_smoking = 1
	Else
		nInd_smoking = 2
	End If
	
	lstrName = Trim(lclsPolicy.sLastname) & " " & Trim(lclsPolicy.sLastName2) & "," & Trim(lclsPolicy.sFirstname)
	
Response.Write("" & vbCrLf)
Response.Write("<A NAME=""BeginPage""></A>" & vbCrLf)
Response.Write("<IMG BORDER=1 ALIGN=LEFT SRC=""/VTimeNet/images/RepLogo.jpg"">")


Response.Write("<H6 ALIGN=RIGHT>Fecha: " & mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate) & "</H6>")


Response.Write("")


Response.Write("<H6 ALIGN=RIGHT>Hora: " & TimeOfDay() & "</H6>")


Response.Write("" & vbCrLf)
Response.Write("<BR></BR>")


Response.Write("<H2 ALIGN=CENTER>" & "&nbsp;" & mstrWinName & "</H2>")


Response.Write("")


Response.Write("<H2 ALIGN=CENTER>" & "&nbsp;" & "No." & Request.QueryString.Item("nPolicy") & "</H2>")


Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", mobjValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.TextControl("tctClient", 14, lclsPolicy.sClient))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DIVControl("tctName",  , lstrName))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcdBirthdatCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.DateControl("tcdBirthdat", mobjValues.TypeToString(lclsPolicy.dBirthdat, eFunctions.Values.eTypeData.etdDate)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tcnAgeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.NumericControl("tcnAge", 3, CStr(lclsPolicy.nAge),  ,  , False, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("cbeSexCliCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeSexCli", "Table18", eFunctions.Values.eValuesType.clngComboType, lclsPolicy.sSexclie))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeCivilCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeCivil", "Table14", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPolicy.nCivilsta)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.CheckControl("chkSmoking", "", nInd_smoking))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("cbeOptionCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")


Response.Write(mobjValues.PossiblesValues("cbeOption", "Table5519", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPolicy.nOption)))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbePayFreqCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbePayFreq", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPolicy.nPayfreq)))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeTyperiskCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("cbeTyperisk", "Table5639", eFunctions.Values.eValuesType.clngComboType, CStr(lclsPolicy.nTyperisk),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTyperiskToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=3>&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")


Response.Write(mobjValues.TextControl("tctClient", 14, lclsPolicy.sAgent_Cli))


Response.Write("</TD>" & vbCrLf)
Response.Write("		<TD COLSPAN=""2"">")


Response.Write(mobjValues.DIVControl("tctName",  , lclsPolicy.sAgent_name))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	'+Se muestran las coberturas asociadas 
	If lcolCover.Find(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
		mstrTotalPrima = 0
		For	Each lclsCover In lcolCover
			mstrTotalPrima = mstrTotalPrima + lclsCover.nPremium
			With mobjCoverGrid
				.Columns("tctCover").DefValue = lclsCover.sDescript
				.Columns("tcnCapital").DefValue = lclsCover.nCapital
				.Columns("tcnPremium").DefValue = lclsCover.nPremium
				.Columns("tcnPremium2").DefValue = CStr(System.Math.Round(lclsCover.nPremium / 2, 2))
				.Columns("tcnPremium4").DefValue = CStr(System.Math.Round(lclsCover.nPremium / 12, 2))
				
				mintLine = mintLine + 1
				
				Response.Write(.DoRow)
			End With
		Next lclsCover
	End If
	
Response.Write("" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("    <TD ALIGN=CENTER COLSPAN=2><LABEL>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcnAnual", False, FormatNumber(mstrTotalPrima, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcnsemestral", False, FormatNumber(mstrTotalPrima / 2, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	<TD ALIGN=RIGHT>")

	Response.Write(mobjValues.DIVControl("tcnmensual", False, FormatNumber(mstrTotalPrima / 12, 2)))
Response.Write("</TD>" & vbCrLf)
Response.Write("	</TR>")

	Response.Write(mobjCoverGrid.closeTable())
Response.Write("" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	
	If lcolFunds_pol.Find_policy(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.DateToString(lclsPolicy.dStartdate)) Then
		lintGuaranty = 0
		For	Each lclsFunds_pol In lcolFunds_pol
			lintGuaranty = lintGuaranty + 1
			With mobjFunds_polGrid
				If nGuaranty = 1 And lintGuaranty = 1 Then
					.Columns("tctOrigy").DefValue = "Rentabilidad Garantizada Cuenta Básica"
					.Columns("tctDescript").DefValue = ""
					.Columns("tcnintproy").DefValue = CStr(nIntwarr2)
					.Columns("tcnParticip").DefValue = CStr(100)
					Response.Write(.DoRow)
				Else
					If lintGuaranty = 1 Then
						.Columns("tctDescript").DefValue = ""
					Else
						.Columns("tctDescript").DefValue = lclsFunds_pol.sDescript
					End If
				End If
				.Columns("tctOrigy").DefValue = lclsFunds_pol.sPortafol
				.Columns("tcnintproy").DefValue = lclsFunds_pol.nIntProyVarCle
				.Columns("tcnParticip").DefValue = lclsFunds_pol.nParticip
				Response.Write(.DoRow)
			End With
		Next lclsFunds_pol
	End If
	Response.Write(mobjFunds_polGrid.closeTable())
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("Anchor7Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("")

	
	If lcolPer_deposit.Find_premium_det(mstrCertype, mintBranch, mintProduct, mlngPolicy, mlngCertif, mobjValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsPer_deposit In lcolPer_deposit
			With mobjPlan_payGrid
				.Columns("tcnYear_ini").DefValue = lclsPer_deposit.nYear_ini
				ldblPrem_deal = lclsPer_deposit.nAmountdep
				llngYear_end = lclsPer_deposit.nYear_end
				.Columns("tcnYear_end").DefValue = llngYear_end
				.Columns("tcnAmountdep").DefValue = CStr(ldblPrem_deal)
				.Columns("tcnAmountdep1").DefValue = CStr(ldblPrem_deal / 2)
				.Columns("tcnAmountdep3").DefValue = CStr(ldblPrem_deal / 12)
				.Columns("tcnAmountdep_aux").DefValue = lclsPer_deposit.nAmountdep_aux
				.Columns("tcnPremBasic").DefValue = lclsPer_deposit.nBasicPrem
				.Columns("tcnPremSaving").DefValue = lclsPer_deposit.nSavingPrem
				.Columns("tcnRecamount").DefValue = lclsPer_deposit.nRecamount
				Response.Write(.DoRow)
			End With
		Next lclsPer_deposit
	End If
	Response.Write(mobjPlan_payGrid.closeTable())
	
Response.Write("" & vbCrLf)
Response.Write("<H1 class=SaltoDePagina></H1>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("Anchor8Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""7""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("")

	
	Dim llngCount As Short
	
	llngCount = 0
	mintLine = 0
	If lcolProjectvul.Find(mstrCertype, mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
		
		For	Each lclsProjectvul In lcolProjectvul
			If mintLine = 30 Then
				mintLine = 0
Response.Write("" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("            <H1 class=SaltoDePagina></H1>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("Anchor8Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<TABLE WIDTH=100% COLS=12 CLASS=grddata>")

				If nGuaranty = 1 Then
Response.Write("       " & vbCrLf)
Response.Write("<TR><TH COLSPAN=4 ALIGN=CENTER></TH><TH COLSPAN=4 ALIGN=CENTER>Rentabilidad garantizada</TH><TH COLSPAN=4 ALIGN=CENTER>Rentabilidad compañía</TH></TR><TR><TH>Año póliza</TH><TH>Edad</TH><TH>Aporte Cta. Básica</TH><TH>Aporte Cta. de Ahorro</TH><TH>Valor cta básica</TH><TH>Valor cta ahorro</TH><TH>Valor de rescate</TH><TH>Monto asegurado</TH><TH>Valor cta básica</TH><TH>Valor cta ahorro</TH><TH>Valor de rescate</TH><TH>Monto asegurado</TH></TR>")

				Else
Response.Write("" & vbCrLf)
Response.Write("TR><TH COLSPAN=4 ALIGN=CENTER></TH><TH COLSPAN=4 ALIGN=CENTER>Rentabilidad compañía</TH><TH COLSPAN=4 ALIGN=CENTER>Rentabilidad mercado</TH></TR><TR><TH>Año póliza</TH><TH>Edad</TH><TH>Aporte Cta. Básica</TH><TH>Aporte Cta. de Ahorro</TH><TH>Valor cta básica</TH><TH>Valor cta ahorro</TH><TH>Valor de rescate</TH><TH>Monto asegurado</TH><TH>Valor cta básica</TH><TH>Valor cta ahorro</TH><TH>Valor de rescate</TH><TH>Monto asegurado</TH></TR>")

				End If
Response.Write("" & vbCrLf)
Response.Write("")

				
			End If
			With mobjShowVulGrid
				.Columns("tcnYear").DefValue = lclsProjectvul.nYear
				.Columns("tcnAge_reinsu").DefValue = lclsProjectvul.nAge
				.Columns("tcnAmodepacum").DefValue = lclsProjectvul.nPremium
				.Columns("tcnAmodepacum2").DefValue = lclsProjectvul.nPremium2
				.Columns("tcnValpolig").DefValue = lclsProjectvul.nVp_npremium
				.Columns("tcnValpoliga").DefValue = lclsProjectvul.nVp_saving
				.Columns("tcnValsurig").DefValue = lclsProjectvul.nSurramount
				.Columns("tcnProdeathig").DefValue = lclsProjectvul.nCapital
				.Columns("tcnValpolig2").DefValue = lclsProjectvul.nVp2_npremium
				.Columns("tcnValpoliga2").DefValue = lclsProjectvul.nVp2_saving
				.Columns("tcnValsurig2").DefValue = lclsProjectvul.nSurramount2
				.Columns("tcnProdeathig2").DefValue = lclsProjectvul.nCapital2
				Response.Write(.DoRow)
			End With
			llngCount = llngCount + 1
			mintLine = mintLine + 1
			
			If lclsProjectvul.nYear Mod 10 = 0 Then
Response.Write("" & vbCrLf)
Response.Write("            <TR></TR><TR></TR><TR></TR><TR></TR><TR></TR>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            ")

			End If
		Next lclsProjectvul
	End If
	
	Response.Write(mobjShowVulGrid.closeTable())
	
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<H1 class=SaltoDePagina></H1>" & vbCrLf)
Response.Write("")

	If Request.QueryString.Item("nGraph") = "1" Then
		
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5"" CLASS=""HighLighted"" ><LABEL>" & GetLocalResourceObject("Anchor10Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""5""><HR></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<CENTER>")


Response.Write("<IMG SRC=""/VTimeNet/common/graphic_VUL.aspx?" & mstrQueryGraph & "&nChartTyp=1"">")


Response.Write("" & vbCrLf)
Response.Write("</CENTER>" & vbCrLf)
Response.Write("<BR>" & vbCrLf)
Response.Write("<CENTER>")


Response.Write("<IMG SRC=""/VTimeNet/common/graphic_VUL.aspx?" & mstrQueryGraph & "&nChartTyp=2"">")


Response.Write("" & vbCrLf)
Response.Write("</CENTER>" & vbCrLf)
Response.Write("")

	End If
Response.Write("" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR></TR><TR></TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>La rentabilidad o las ganancias obtenidas en el pasado por la" & vbCrLf)
Response.Write("  (s) modalidad (es) de inversión elegida (s) no garantiza que ellas se repitan" & vbCrLf)
Response.Write("  en el futuro, debido a que los valores de las cuota de los fondos son" & vbCrLf)
Response.Write("  variables. Por lo tanto los 'Valores Proyectados' son suceptibles de cambiar" & vbCrLf)
Response.Write("  de acuerdo con el mercado.</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL>La presente es una ilustración del comportamiento de los saldos" & vbCrLf)
Response.Write("  de las cuentas y el monto asegurado, tomando en cuenta el pago periódico y oportuno de las primas" & vbCrLf)
Response.Write("  según se muestra en la ilustración.</LABEL></TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("<TR>" & vbCrLf)
Response.Write("    <TD><LABEL>" & GetLocalResourceObject("Anchor11Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("    <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("    <TD><LABEL>" & GetLocalResourceObject("Anchor12Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("</TR>" & vbCrLf)
Response.Write("<BR><BR><BR><BR>" & vbCrLf)
Response.Write("	<TD COLSPAN=""2"">")


Response.Write(mobjValues.DIVControl("tctName",  , lclsPolicy.sAgent_name))


Response.Write("</TD>" & vbCrLf)
Response.Write("	<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("	<TD COLSPAN=""2"">")


Response.Write(mobjValues.DIVControl("tctPhone",  , lclsPolicy.sAgent_Phones))


Response.Write("</TD>" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("")

	lcolCover = Nothing
	lclsCover = Nothing
	lclsPolicy = Nothing
	lclsCertificat = Nothing
	lclsProduct = Nothing
	
	lcolPer_deposit = Nothing
	lclsPer_deposit = Nothing
	
	lcolProjectvul = Nothing
	lclsProjectvul = Nothing
End Sub

</script>
<%Response.Expires = -1


lclsProduct = New eProduct.Product

mobjValues = New eFunctions.Values

nGuaranty = 0

lclsProduct1 = New eProduct.Plan_IntWar

'+ Se deja la pagina en modo consulta     
mobjValues.ActionQuery = True

'+ Se asignan valores de parámetros     
mstrCertype = Request.QueryString.Item("sCertype")
mintBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
mintProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
mlngPolicy = mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
mlngCertif = mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)

If lclsProduct1.Find(mintBranch, mintProduct, 0, Today, 1) Then
	
	nIntwarr2 = lclsProduct1.nIntWarrMin
	nIntwarrClear = lclsProduct1.nIntwarrClear
	nGuaranty = 1
End If

lclsProduct1 = Nothing

mobjValues.sCodisplPage = "PrintPol"

%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 12 $|$$Date: 3/07/06 7:37p $"

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


  
  <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/common/Custom1.css">
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<%Call lclsProduct.FindProdMaster(mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble))

mintLine = 28
If mstrCertype = "1" Then 'Propuesta
	mstrWinName = "Propuesta de " & lclsProduct.sDescript
ElseIf mstrCertype = "2" Then  'Póliza
	mstrWinName = "Póliza de " & lclsProduct.sDescript
ElseIf mstrCertype = "3" Then  'Cotización
	mstrWinName = "Cotización de " & lclsProduct.sDescript
End If

Response.Write("<SCRIPT>top.document.title='" & mstrWinName & "'</SCRIPT>")

mstrQueryGraph = "sCertype=" & mstrCertype & "&nBranch=" & mintBranch & "&nProduct=" & mintProduct & "&nPolicy=" & mlngPolicy & "&nCertif=" & mlngCertif & "&sProduct=" & lclsProduct.sDescript & "&sCurrency=US$"
%>
<FORM METHOD="POST" ID="FORM" NAME="PrintPol" ACTION="PrintPol.aspx">
<%
Call insCoverGrid()
Call insFunds_polGrid()
Call insPlan_payGrid()
Call InsShowVulGrid()

Call insPrePrintPol()

mobjCoverGrid = Nothing
mobjFunds_polGrid = Nothing
mobjShowVulGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






