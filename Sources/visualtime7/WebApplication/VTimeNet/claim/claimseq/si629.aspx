<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim mclsDecla_Benef As eClaim.Decla_Benef
Dim mclsbeneficiar As eClaim.Beneficiar
Dim mstrFirstCase As String
Dim mstrCase() As String
Dim mclsClaim_Case As eClaim.Claim_case
Private mclsProduct_li As eProduct.Product
Private sAPV As String

Dim lclsClaimCases As eClaim.Claim_cases


'----------------------------------------------------------------------------------------------
Private Sub CalculateRent(ByRef lintCase_num As Object, ByRef lintDeman_type As Object)
	'----------------------------------------------------------------------------------------------
	Dim lclsLife_Claim As eClaim.Life_claim
	lclsLife_Claim = New eClaim.Life_claim
	With lclsLife_Claim
		If .Find(CDbl(Session("nClaim")), lintCase_num, lintDeman_type) Then
			Session("nRent") = .nMonth_amou
			Session("dIniDat") = .dInit_date
			Session("dEndDat") = .dEnd_date
			Session("nIn_lif_typ") = .nIn_lif_typ
		Else
			Session("nRent") = 0
			Session("dIniDat") = eRemoteDB.Constants.dtmNull
			Session("dEndDat") = eRemoteDB.Constants.dtmNull
			Session("nIn_lif_typ") = 0
		End If
	End With
	'UPGRADE_NOTE: Object lclsLife_Claim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsLife_Claim = Nothing
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	Dim lstrCase() As Object
	Dim lintCase_num As Object
	Dim lintDeman_type As Object
	Dim lclsClaimCases As eClaim.Claim_cases
	Dim lblnFind As Object
	
	lclsClaimCases = New eClaim.Claim_cases
	If mstrFirstCase <> vbNullString Then
        lstrCase = mstrFirstCase.Split("/")
		lintCase_num = lstrCase(0)
		lintDeman_type = lstrCase(1)
		Session("nCase_num_629") = lintCase_num
		Session("nDeman_type_629") = lintDeman_type
		Call CalculateRent(lintCase_num, lintDeman_type)
	End If
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si629"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If
	
	With mobjGrid.Columns
		.AddPossiblesColumn(40603, "Cobertura", "valCover", "Tabcl_cover", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  , "ChangeValues('Cover');", False, 5, "Cobertura a la cual se encuentra asociado el cliente como beneficiario.")
		.AddClientColumn(9486, "Cliente", "tctClientCode", "",  , "Número de rut. del beneficiario.", "ChangeValues('Client_SI629')", ,  , True,  ,  ,  ,  ,  ,  , True)
		.AddPossiblesColumn(0, "Tipo de persona", "cbePersonTyp", "TABLE5006", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "", True ,  , "Indica si el beneficiario es  natural o jurídico.")
		.AddCheckColumn(0, "Contingente", "chkConti", "",  ,  ,  , True, "Indicador de beneficiario contingente")
        .AddCheckColumn(0, "Designado", "chkDesign", "",  ,  ,  , True, "Indicador de beneficiario designado")
		.AddTextColumn(0, "Apellido paterno", "tctLastName", 19, "",  , "Apellido paterno del beneficiario.",  ,  ,  , True)
		.AddTextColumn(0, "Apellido materno", "tctLastName2", 19, "",  , "Apellido materno del beneficiario.",  ,  ,  , True)
		
		If Request.QueryString("Type") <> "PopUp" Then
			.AddTextColumn(0, "Nombres", "tctFirstName", 60, "",  , "Nombres del beneficiario.",  ,  ,  , True)
		Else
			.AddTextAreaColumn(0, "Nombres", "tctFirstName", "", 3, 20, True, "Nombres del beneficiario.", True)
		End If
		
		.AddDateColumn(0, "Fecha de nacimiento", "tcdBirthdat", "",  , "Fecha de nacimiento del beneficiario",  ,  ,  , False)
		.AddPossiblesColumn(0, "Relación", "cbeRelaship", "Table15", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , "Relación entre el cliente introducido y el asegurado.", eFunctions.Values.eTypeCode.eString)
		.AddNumericColumn(9480, "% Participación", "tcnParticip", 9, "0", True, "Porcentaje de participación", True, 6,  ,  , "ChangeValues('Rent')")
		.AddClientColumn(9486, "Representante", "tctRepresentCode", "",  , "Código identificativo del representante legal.", "ChangeValues(""ClientRep"", this)", False,  , True,  ,  ,  ,  ,  ,  , True)
		.AddTextColumn(0, "Apellido paterno", "tctRLastName", 19, "",  , "Apellido paterno del Representante.",  ,  ,  , True)
		.AddTextColumn(0, "Apellido materno", "tctRLastName2", 19, "",  , "Apellido materno del Representante.",  ,  ,  , True)
		
		If Request.QueryString("Type") <> "PopUp" Then
			.AddTextColumn(0, "Nombres", "tctRFirstName", 60, "",  , "Nombres del Representante.",  ,  ,  , True)
		Else
			.AddTextAreaColumn(0, "Nombres", "tctRFirstName", vbNullString, 3, 20, False, "Nombres del Representante.", True)
		End If
		.AddPossiblesColumn(0, "Destino del cheque", "cbePaymentAddress", "TABLE7801", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , ";",  ,  , "Indica si el cheque será enviado a la sucursal o directo al beneficiario.")
		.Item("cbePaymentAddress").BlankPosition = False

		.AddPossiblesColumn(0, "Sucursal de pago", "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1,0)",  ,  , "Sucursal a donde debe ser enviado el pago del siniestro.")
		.AddPossiblesColumn(0, "Oficina de pago", "cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insInitialAgency(2,0)",  ,  , "Oficina a donde debe ser enviado el pago del siniestro.")
		With mobjGrid.Columns("cbeOfficeAgen").Parameters
			.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.ReturnValue("nBran_off",  ,  , True)
		End With
		
		.AddPossiblesColumn(9481, "Agencia de pago", "cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insInitialAgency(3,0)",  ,  , "Agencia a donde debe ser enviado el pago del siniestro.")
		With mobjGrid.Columns("cbeAgency").Parameters
			.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.ReturnValue("nBran_off",  ,  , True)
			.ReturnValue("nOfficeAgen",  ,  , True)
			.ReturnValue("sDesAgen",  ,  , True)
		End With
		
		If CDbl(Session("nIn_lif_typ")) = 2 Or CDbl(Session("nIn_lif_typ")) = 3 Then
			.AddNumericColumn(40791, "Renta", "tcnRent", 18, CStr(0), True, "Monto de la renta a pagar", True, 6,  ,  ,  , True)
			.AddDateColumn(0, "Desde", "tcdInitDate",  ,  , "Fecha desde cuando comienza a cancelar la renta",  ,  ,  , True)
			.AddDateColumn(0, "Hasta", "tcdEndDate",  ,  , "Fecha limite para el pago de la renta",  ,  ,  , True)
		End If
		
		.AddButtonColumn(0, "Notas", "SCA2-S", 0, True, Request.QueryString("Type") <> "PopUp" Or Session("bQuery"),  ,  ,  ,  , "btnNotenum")
		.AddDateColumn(0, "Fecha concurrencia", "tcdShowDate",  ,  , "Fecha en la que el beneficiario acude a la citación",  ,  ,  , False)

		If Request.QueryString("Type") = "PopUp" Then
			.AddAnimatedColumn(0, "Información adicional del destinatario", "btnQuery", "/VTimeNet/Images/clfolder.png", "Secuencia de clientes")
			.Item("btnQuery").HRefScript="refreshNavigationLinkStatus();"
			.AddTextColumn(0, "", "tctClientNavigation", 60, "",   , "",  ,,"ChangeValues(""Client_SI629"")") 
		End If

		.AddHiddenColumn("Indic_Benef", "")
		.AddHiddenColumn("hddSel", "")
		.AddHiddenColumn("hddCover", "")
		.AddHiddenColumn("hddModulec", "")
		.AddHiddenColumn("hddCurrency", "")
		.AddHiddenColumn("hddClientCode", "")
		.AddHiddenColumn("hddLastName", "")
		.AddHiddenColumn("hddLastName2", "")
		.AddHiddenColumn("hddFirstName", "")
		.AddHiddenColumn("hddBirthdat", "")
		.AddHiddenColumn("hddRelaship", "")
		.AddHiddenColumn("hddParticip", "")
		.AddHiddenColumn("hddRepresentCode", "")
		.AddHiddenColumn("hddRLastName", "")
		.AddHiddenColumn("hddRLastName2", "")
		.AddHiddenColumn("hddRFirstName", "")
		.AddHiddenColumn("hddOffice_pay", "")
		.AddHiddenColumn("hddOfficeAgen_pay", "")
		.AddHiddenColumn("hddAgency_pay", "")
		.AddHiddenColumn("hddRent", "")
		.AddHiddenColumn("hddInitDate", "")
		.AddHiddenColumn("hddEndDate", "")
		.AddHiddenColumn("hddAge", "")
		.AddHiddenColumn("hddIncapacity", "")
		.AddHiddenColumn("hddId", "")
		.AddHiddenColumn("hddCasenum", CStr(Session("nCase_num_629")))
		.AddHiddenColumn("hddDeman_type", CStr(Session("nDeman_type_629")))
		.AddHiddenColumn("hddNoteNum", "")
		.AddHiddenColumn("hddShowDate", "")
		.AddHiddenColumn("hddHas_Surv_Pension_Benefs", "")
		.AddHiddenColumn("hddSummon", "")
		.AddHiddenColumn("hddSummon_Limit", "")
		.AddHiddenColumn("hddPaymentAddress", "")
        
        .AddHiddenColumn("hddRPersonTyp", "")
	End With
	
	With mobjGrid.Columns("valCover").Parameters
		.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCase_num", Session("nCase_num_629"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nDeman_type", Session("nDeman_type_629"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.ReturnValue("nModulec",  ,  , True)
		.ReturnValue("nCurrency",  ,  , True)
	End With
	
	With mobjGrid
		.WidthDelete = 450
		.Codispl = "SI629"
		.Top = 10
		.Left = 100
		.Width = 650
		.Height = 550
		.FieldsByRow = 2
		
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctClientCode").EditRecord = True
		
		If CStr(Session("IndicBeneficiar")) = "1" Then
			.Columns("valCover").Disabled = True
		Else
			.Columns("valCover").Disabled = False
		End If
		
		.Columns("chkConti").PopUpVisible = False
        .Columns("chkDesign").PopUpVisible = False
		
		.DeleteButton = True
		
		.sDelRecordParam = "nCase_num='    + marrArray[lintIndex].hddCasenum + '" & "&nDeman_type=' + marrArray[lintIndex].hddDeman_type + '" & "&sClient='     + marrArray[lintIndex].hddClientCode + '" & "&nId='         + marrArray[lintIndex].hddId + '"
		
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub
'----------------------------------------------------------------------------------------------
Private Sub FindNumDecla()
	'----------------------------------------------------------------------------------------------
	Call mclsDecla_Benef.Find(CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate")))
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreSI629()
	'----------------------------------------------------------------------------------------------
	Dim lblnFind As Boolean
	Dim lclsClaimBenefs As eClaim.ClaimBenefs
	Dim lclsClaimBenef As eClaim.ClaimBenef
	Dim lclsClaim As eClaim.Claim
	Dim lintIndex As Short
	Dim lclsClaimCases As Object
	Dim lintCase_num As Object
	Dim lintDeman_type As Object
	Dim lstrClient As Object
	Dim lstrId As Object
	Dim lstrCase() As Object
	Dim lintOffice As Integer
	Dim lintOfficeAgen As Integer
	Dim lintAgency As Integer
	
	lblnFind = False
	
	lclsClaim = New eClaim.Claim
	lclsClaimBenef = New eClaim.ClaimBenef
	lclsClaimBenefs = New eClaim.ClaimBenefs
	
	
	If mstrFirstCase <> vbNullString Then
		lblnFind = True
        lstrCase = mstrFirstCase.Split("/")
		lintCase_num = lstrCase(0)
		lintDeman_type = lstrCase(1)
		lstrClient = lstrCase(2)
		lstrId = lstrCase(3)
		Session("nCase_num_629") = lintCase_num
		Session("nDeman_type_629") = lintDeman_type
		
		Call mclsClaim_Case.Find(CDbl(Session("nClaim")), CInt(Session("nCase_num_629")), CInt(Session("nDeman_type_629")))
		
		'Call lclsClaimBenef.insClaimBenefAPV(CDbl(Session("nClaim")), CInt(Session("nCase_num_629")), CInt(Session("nDeman_type_629")), CInt(Session("nOffice_pol")), CInt(Session("nOfficeAgen_pol")), CInt(Session("nAgency_pol")), CInt(Session("nUserCode")), CInt(Session("nBranch")), CInt(Session("nProduct")), CDate(Session("dEffecdate")))
		
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"" border = ""0"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2""><LABEL ID=9501>Caso</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""2"">")

	
	With mobjValues
		.BlankPosition = False
		.Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sDemandant", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeCase", "TabBuildingAllCases", eFunctions.Values.eValuesType.clngComboType, mstrFirstCase, True,  ,  ,  ,  , "insChangeCase(this)",  ,  , ""))
		Response.Write(mobjValues.HiddenControl("tctclient", lstrClient))
		Response.Write(mobjValues.HiddenControl("tcnDeman_typ", lintDeman_type))
		Response.Write(mobjValues.HiddenControl("cbeCases", lintCase_num))
	End With
	
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("		<TD ALIGN=""LEFT""><LABEL>Nro. declaración</LABEL></TD>" & vbCrLf)
Response.Write("        ")

	If mclsDecla_Benef.nNumdecla <> 0 Then
Response.Write("" & vbCrLf)
Response.Write("		   <TD ALIGN=""LEFT"">")


Response.Write(mobjValues.NumericControl("tcnNumDecla", 5, CStr(mclsDecla_Benef.nNumdecla),  , "Número de declaración de los beneficiarios.",  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("		   <TD ALIGN=""LEFT"">")


Response.Write(mobjValues.NumericControl("tcnNumDecla", 5,  ,  , "Número de declaración de los beneficiarios.",  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("		<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("    	<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkHas_Surv_Pension_Benefs", "Beneficiarios de sobrevivencia", mclsClaim_Case.sHas_Surv_Pension_Benefs, "1",  , False))


Response.Write("</TD>  	" & vbCrLf)
Response.Write("    	<TD><LABEL ID=0>Fecha de citación</LABEL></TD>" & vbCrLf)
Response.Write("    	<TD>")


Response.Write(mobjValues.DateControl("gmdSummon", CStr(mclsClaim_Case.dSummon),  , "Fecha de citación enviada a los beneficiarios."))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	<TD><LABEL ID=0>Fecha fin de citación</LABEL></TD>" & vbCrLf)
Response.Write("    	<TD>")


Response.Write(mobjValues.DateControl("gmdSummon_Limit", CStr(mclsClaim_Case.dSummon_Limit),  , "Fecha límite para la comparecencia de los beneficiarios citados."))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"" CLASS=""HighLighted"" ALIGN=""RIGHT""><LABEL ID=40302><A NAME=""tipo de Beneficiario"">Tipo de beneficiario</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        ")

	If mclsbeneficiar.InsExists(CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate"))) Then
Response.Write("" & vbCrLf)
Response.Write("            ")

		mobjGrid.AddButton = False
	    Session("IndicBeneficiar") = "1"
		
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    		<TD ALIGN=""CENTER"" COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "Póliza", CStr(1), CStr(1),  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""CENTER"" COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "Herederos legales", CStr(0), CStr(2),  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""CENTER"" COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "A declarar", CStr(0), CStr(3),  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	Else
Response.Write("" & vbCrLf)
Response.Write("            ")

		mobjGrid.AddButton = True
		Session("IndicBeneficiar") = "2"
		
Response.Write("" & vbCrLf)
Response.Write("    		<TD ALIGN=""CENTER"" COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "Póliza", CStr(0), CStr(1),  , True, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""CENTER"" COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "Herederos legales", CStr(0), CStr(2),  , False, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""CENTER"" COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optTypeBenef", "A declarar", CStr(1), CStr(3),  , False, 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("		")

	End If
Response.Write("" & vbCrLf)
Response.Write("		<TD COLSPAN=""2""> </TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	With Request
		lintIndex = 0
		
		
		Call lclsClaim.Find(CDbl(Session("nClaim")))
		If lclsClaimBenefs.Find_Benef_Si629(CDbl(Session("nClaim")), lintCase_num, lintDeman_type, CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dEffecdate"))) Then
			With mobjGrid
				For	Each lclsClaimBenef In lclsClaimBenefs
					If lclsClaimBenef.nOffice_pay > 0 Then
						lintOffice = lclsClaimBenef.nOffice_pay
					Else
						lintOffice = lclsClaim.nOffice_pay
					End If
					If lintOffice = eRemoteDB.Constants.intNull Or lintOffice = 0 Then
						If Session("nOffice_pol") <> eRemoteDB.Constants.intNull Then
							lintOffice = Session("nOffice_pol")
							lintOfficeAgen = Session("nOfficeAgen_pol")
							lintAgency = Session("nAgency_pol")
						End If
					Else
						If lclsClaimBenef.nOfficeAgen_pay > 0 Then
							lintOfficeAgen = lclsClaimBenef.nOfficeAgen_pay
						Else
							lintOfficeAgen = lclsClaim.nOfficeAgen_pay
						End If
						
						If lclsClaimBenef.nAgency_pay > 0 Then
							lintAgency = lclsClaimBenef.nAgency_pay
						Else
							lintAgency = lclsClaim.nAgency_pay
						End If
					End If
                    mobjGrid.Columns("cbePaymentAddress").DefValue=lclsClaimBenef.nPaymentAddress
                    mobjGrid.Columns("hddPaymentAddress").DefValue=lclsClaimBenef.nPaymentAddress
					With mobjGrid.Columns("cbeOfficeAgen").Parameters
						.Add("nOfficeAgen", lintOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End With
					
					With mobjGrid.Columns("cbeAgency").Parameters
						.Add("nOfficeAgen", lintOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Add("nAgency", lintOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					End With
					
					.Columns("valCover").DefValue = CStr(lclsClaimBenef.nCover)
                        
                    If lclsClaimBenef.nCover > 0 And lclsClaimBenef.Indic_Benef = 1 then
                        Session("IndicBeneficiar") = "1"            
                    End If
                        
					.Columns("tctClientCode").DefValue = lclsClaimBenef.sClient
					.Columns("cbePersonTyp").DefValue = lclsClaimBenef.nPerson_typ
					.Columns("cbeRelaship").DefValue = CStr(lclsClaimBenef.nRelation)
					
					If lclsClaimBenef.nParticip <> eRemoteDB.Constants.intNull Then
						.Columns("tcnParticip").DefValue = CStr(lclsClaimBenef.nParticip)
					Else
						.Columns("tcnParticip").DefValue = CStr(0)
					End If
					.Columns("tctLastName").DefValue = lclsClaimBenef.sLastName
					.Columns("tctLastName2").DefValue = lclsClaimBenef.sLastName2
					.Columns("tctFirstName").DefValue = lclsClaimBenef.sCliename
					.Columns("tcdBirthdat").DefValue = CStr(lclsClaimBenef.dBirthdat)
					.Columns("Indic_Benef").DefValue = CStr(lclsClaimBenef.Indic_Benef)
					.Columns("tctRepresentCode").DefValue = lclsClaimBenef.sClient_rep
					.Columns("cbeOffice").DefValue = CStr(lintOffice)
					.Columns("cbeOfficeAgen").DefValue = CStr(lintOfficeAgen)
					.Columns("cbeAgency").DefValue = CStr(lintAgency)
					.Columns("hddOffice_pay").DefValue = CStr(lintOffice)
					.Columns("hddOfficeAgen_pay").DefValue = CStr(lintOfficeAgen)
					.Columns("hddAgency_pay").DefValue = CStr(lintAgency)
					
					If CDbl(Session("nIn_lif_typ")) = 2 Or CDbl(Session("nIn_lif_typ")) = 3 Then
						If lclsClaimBenef.nAmount > 0 Then
							.Columns("tcnRent").DefValue = mobjValues.TypeToString(lclsClaimBenef.nAmount, eFunctions.Values.eTypeData.etdDouble)
						Else
							If lclsClaimBenef.nParticip <> eRemoteDB.Constants.intNull And lclsClaimBenef.nParticip <> 0 Then
								.Columns("tcnRent").DefValue = CStr((CDbl(Session("nRent")) * lclsClaimBenef.nParticip) / 100)
							Else
								.Columns("tcnRent").DefValue = Session("nRent")
							End If
						End If
						.Columns("tcdInitDate").DefValue = mobjValues.TypeToString(Session("dIniDat"), eFunctions.Values.eTypeData.etdDate)
						.Columns("tcdEndDate").DefValue = mobjValues.TypeToString(Session("dEndDat"), eFunctions.Values.eTypeData.etdDate)
					End If
					
					.Columns("tctRFirstName").DefValue = lclsClaimBenef.sClieName_Rep
					.Columns("tctRLastName").DefValue = lclsClaimBenef.sLastName_Rep
					.Columns("tctRLastName2").DefValue = lclsClaimBenef.sLastName2_Rep
					
					'+ Asignación de las variables ocultas
					.Columns("hddCover").DefValue = CStr(lclsClaimBenef.nCover)
					.Columns("hddModulec").DefValue = CStr(lclsClaimBenef.nModulec)
					.Columns("hddCurrency").DefValue = CStr(lclsClaimBenef.nCurrency)
					.Columns("hddClientCode").DefValue = lclsClaimBenef.sClient
					.Columns("hddLastName").DefValue = lclsClaimBenef.sLastName
					.Columns("hddLastName2").DefValue = lclsClaimBenef.sLastName2
					.Columns("hddFirstName").DefValue = lclsClaimBenef.sCliename
					.Columns("hddBirthdat").DefValue = CStr(lclsClaimBenef.dBirthdat)
					.Columns("hddRelaship").DefValue = CStr(lclsClaimBenef.nRelation)
					.Columns("hddParticip").DefValue = CStr(lclsClaimBenef.nParticip)
					.Columns("hddRepresentCode").DefValue = lclsClaimBenef.sClient_rep
					.Columns("hddRLastName").DefValue = lclsClaimBenef.sLastName_Rep
					.Columns("hddRLastName2").DefValue = lclsClaimBenef.sLastName2_Rep
					.Columns("hddRFirstName").DefValue = lclsClaimBenef.sClieName_Rep
					.Columns("hddRent").DefValue = CStr((CDbl(Session("nRent")) * lclsClaimBenef.nParticip) / 100)
					.Columns("hddInitDate").DefValue = Session("dIniDat")
					.Columns("hddEndDate").DefValue = Session("dEndDat")
					.Columns("hddAge").DefValue = CStr(lclsClaimBenef.nAge)
					.Columns("hddIncapacity").DefValue = CStr(lclsClaimBenef.nIncapacity)
					.Columns("hddId").DefValue = CStr(lclsClaimBenef.nId)
					.Columns("hddCasenum").DefValue = lintCase_num
					.Columns("hddDeman_type").DefValue = lintDeman_type
					
					.Columns("chkConti").Checked = CShort(lclsClaimBenef.sConting)
					.Columns("chkConti").DefValue = lclsClaimBenef.sConting
					
                        .Columns("chkDesign").Checked = CShort(lclsClaimBenef.sDesign)
                        .Columns("chkDesign").DefValue = lclsClaimBenef.sDesign
                    
                    .Columns("tcdShowDate").DefValue = CStr(lclsClaimBenef.dShowDate)
					.Columns("btnNotenum").nNoteNum = lclsClaimBenef.nNoteNum
					.Columns("hddShowDate").DefValue = CStr(lclsClaimBenef.dShowDate)
					.Columns("hddNotenum").DefValue = CStr(lclsClaimBenef.nNoteNum)
					
					.Columns("hddHas_Surv_Pension_Benefs").DefValue = mclsClaim_Case.sHas_Surv_Pension_Benefs
					.Columns("hddSummon").DefValue = CStr(mclsClaim_Case.dSummon)
					.Columns("hddSummon_Limit").DefValue = CStr(mclsClaim_Case.dSummon_Limit)
					
                    .sEditRecordParam = "sCase_num='	+  self.document.forms[0].cbeCase.value + '&nIndi=' + marrArray[" & lintIndex & "].Indic_Benef + '" & "&nAge=' + marrArray[" & lintIndex & "].hddAge + '" & "&nIncapacity=' + marrArray[" & lintIndex & "].hddIncapacity + '" & "&nCase_num=' + marrArray[" & lintIndex & "].hddCasenum + '" & "&nDeman_type=' + marrArray[" & lintIndex & "].hddDeman_type + '" & "&nCover=' + marrArray[" & lintIndex & "].hddCover + '" & "&dBirthdat=' + marrArray[" & lintIndex & "].hddBirthdat + '"    

					lintIndex = lintIndex + 1
					Response.Write(mobjGrid.DoRow())
				Next lclsClaimBenef
			End With
		Else
			mobjGrid.sEditRecordParam = "sCase_num='	+  self.document.forms[0].cbeCase.value + '"    
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	
	If sAPV = "1" Then
		
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"" border = ""0"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""8"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD COLSPAN=""8"" CLASS=""HighLighted"" ALIGN=""RIGHT""><LABEL ID=0><A NAME=""A.P.V."">Pagos APV</A></LABEL></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD COLSPAN=""8"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("	    <TD align=right><LABEL>AFP: </LABEL>")

Response.Write(mobjValues.PossiblesValues("cbeAFP", "Table5524", eFunctions.Values.eValuesType.clngComboType, CStr(mclsClaim_Case.nAFP),  ,  ,  ,  ,  ,  , True,  , "Administradora de fondos de pension (AFP) a la que se encuentra afiliado el cliente"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    <TD>")


Response.Write(mobjValues.NumericControl("tcnApv_tax", 18, CStr(mclsClaim_Case.nTransf_amount), False, "Número de declaración de los beneficiarios.",  , 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("   </TR> 	" & vbCrLf)
Response.Write("   <TR>" & vbCrLf)
Response.Write("    	<TD  align=right>")


Response.Write(mobjValues.ClientControl("tctTaxPayee", "00000060805000",  ,  ,  , True,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	<TD>")


Response.Write(mobjValues.NumericControl("tcnTransf_amount", 18, CStr(mclsClaim_Case.nApv_tax), False, "Número de declaración de los beneficiarios.",  , 6,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</TABLE>" & vbCrLf)
Response.Write("")

		
	End If
	
	'UPGRADE_NOTE: Object lclsClaimBenefs may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaimBenefs = Nothing
	'UPGRADE_NOTE: Object lclsClaimBenef may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaimBenef = Nothing
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreSI629Upd()
	'----------------------------------------------------------------------------------------------
	Dim lobjError As eFunctions.Errors
	Dim llbnDisabled As String
	Dim lclsClaimBenef As eClaim.ClaimBenef
	
	lclsClaimBenef = New eClaim.ClaimBenef
	
	With Request
		Response.Write(mobjValues.ShowWindowsName("SI629", Request.QueryString("sWindowDescript")))
		If .QueryString("Action") = "Del" Then
			
			'+ Muestra el mensaje para eliminar registros
			If lclsClaimBenef.FindClaimBenefChildren(CDbl(Session("nClaim")), .QueryString("nCase_num"), .QueryString("nDeman_type"), .QueryString("sClient"), .QueryString("nId")) Then
				lobjError = New eFunctions.Errors
				'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
				lobjError.sSessionID = Session.SessionID
				lobjError.nUsercode = Session("nUsercode")
				'~End Body Block VisualTimer Utility
				With lobjError
					.Highlighted = True
					Response.Write(lobjError.ErrorMessage("SI629", 4332,  ,  ,  , True))
				End With
				'UPGRADE_NOTE: Object lobjError may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
				lobjError = Nothing
			Else
				Call lclsClaimBenef.Del(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sClient"), mobjValues.StringToType(.QueryString("nId"), eFunctions.Values.eTypeData.etdDouble))
				Response.Write(mobjValues.ConfirmDelete())
				
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaimSeq.aspx", "SI629", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
	
	If Request.QueryString("Type") = "PopUp" And Request.QueryString("Action") <> "Add" And Request.QueryString("Action") <> "Del" Then
		If Request.QueryString("nIndi") = 1 Then
			llbnDisabled = "true"
		Else
			llbnDisabled = "false"
		End If

        If Request.QueryString("nIndi") = 1 And mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		    Response.Write("<SCRIPT>self.document.forms[0].valCover.disabled=" & llbnDisabled & ";</" & "Script>")
		    Response.Write("<SCRIPT>self.document.forms[0].btnvalCover.disabled=" & llbnDisabled & ";</" & "Script>")
        Else
		    Response.Write("<SCRIPT>self.document.forms[0].valCover.disabled=" & "false" & ";</" & "Script>")
		    Response.Write("<SCRIPT>self.document.forms[0].btnvalCover.disabled=" & "false" & ";</" & "Script>")
        End If

		Response.Write("<SCRIPT>self.document.forms[0].tctClientCode.disabled=" & llbnDisabled & ";</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tctClientCode_Digit.disabled=" & llbnDisabled & ";</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].cbeRelaship.disabled=" & llbnDisabled & ";</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tcnParticip.disabled=" & llbnDisabled & ";</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tctLastName.disabled=true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tctLastName2.disabled=true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tctFirstName.disabled=true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tcdBirthdat.disabled=true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
	End If
	
	If Request.QueryString("Type") = "PopUp" And Request.QueryString("Action") <> "Del" Then
		If (Request.QueryString("nAge") <> vbNullString And Request.QueryString("nAge") < "18") Or Request.QueryString("nIncapacity") > 0 Then
			llbnDisabled = "false"
		Else
			llbnDisabled = "true"
		End If
	End If
	'UPGRADE_NOTE: Object lclsClaimBenef may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaimBenef = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si629")

'- Variables de uso general que se toman de las variables de Sesión - ACM - 23/01/2001

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si629"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")

mclsDecla_Benef = New eClaim.Decla_Benef
mclsProduct_li = New eProduct.Product
Call mclsProduct_li.FindProduct_li(CInt(Session("nBranch")), CInt(Session("nProduct")), CDate(Session("deffecdate")))
sAPV = mclsProduct_li.sAPV
'UPGRADE_NOTE: Object mclsProduct_li may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsProduct_li = Nothing


%>
<HTML>
<HEAD>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Claim.aspx" -->
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI629", Request.QueryString("sWindowDescript")))
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI629", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
%>
</HEAD>
<BODY ONLOAD="RedrawFieldsByPersonTyp();" ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI629" ACTION="ValClaimSeq.aspx?x=1">

<%If Request.QueryString("Type") <> "PopUp" Then%>
	<A NAME="BeginPage"></A>
    <%	Response.Write(mobjValues.ShowWindowsName("SI629", Request.QueryString("sWindowDescript")))
	Call FindNumDecla()
End If
%>

<%If Request.QueryString("sCase_num") = vbNullString Then
	lclsClaimCases = New eClaim.Claim_cases
	If lclsClaimCases.Find(CDbl(Session("nClaim"))) Then
		mstrFirstCase = CStr(lclsClaimCases.Item(1).nCase_num) & "/" & CStr(lclsClaimCases.Item(1).nDeman_type) & "/" & lclsClaimCases.Item(1).sClient & "/" & lclsClaimCases.Item(1).nId
		'UPGRADE_NOTE: Object lclsClaimCases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		lclsClaimCases = Nothing
	End If
Else
	mstrFirstCase = Request.QueryString("sCase_num")
End If

If mstrFirstCase <> vbNullString Then
    mstrCase = mstrFirstCase.Split("/")
	Session("nCase_num_629") = mstrCase(0)
	Session("nDeman_type_629") = mstrCase(1)
End If

Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI629Upd()
Else
	mclsbeneficiar = New eClaim.Beneficiar
	mclsClaim_Case = New eClaim.Claim_case
	Call insPreSI629()
	'UPGRADE_NOTE: Object mclsbeneficiar may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mclsbeneficiar = Nothing
	'UPGRADE_NOTE: Object mclsClaim_Case may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mclsClaim_Case = Nothing
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mclsDecla_Benef may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsDecla_Benef = Nothing
%>
</FORM>
</BODY>
</HTML>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 25-03-13 7:35 $"
	
//ShowClientSequence: 
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ShowClientSequence(nTypeSequence){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    var nMainAction;
	var sClientCode;
	var sDigit;
	var nPerson_typ;
    var sFirstName;
    var sLastName;
    var sLastName2;

    // Si se trata de la secuencia del beneficiario del caso
    if (nTypeSequence == 1){
        nMainAction = ($("[name='tctFirstName']").attr("disabled")?302:301);
	    sClientCode = $("[name='tctClientCode']").val();;
	    sDigit=$("[name='tctClientCode_Digit']").val();
	    nPerson_typ=$("[name='cbePersonTyp']").val();
        sFirstName=encodeURIComponent($("[name='tctFirstName']").val());
        sLastName=encodeURIComponent($("[name='tctLastName']").val());
        sLastName2=encodeURIComponent($("[name='tctLastName2']").val());

        if (sClientCode==""){
            alert("Ingrese primero el RUT del cliente");
            return;

        }

	    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sCodispl=BC003_K&sModule=Client&sProject=ClientSeq&sRoleCode=&LinkParamsClient='+sClientCode+'&sClientCode='+sClientCode+'&nMainAction='+nMainAction+'&LinkSpecialAction='+nMainAction+'&sDigit='+sDigit+'&LinkParamsDigit='+sDigit+'&nPerson_typ='+nPerson_typ+'&sOriginalForm=&sLinkSpecial=1&LinkParamsClientControl=tctClientNavigation&sFirstName='+sFirstName+'&sLastName='+sLastName+'&sLastName2='+sLastName2, 'ClientSeq', 750, 500, 'no', 'yes', 20, 20)

    }
    // Si se trata de la secuencia del representante del beneficiario del caso
    else{
        nMainAction = ($("[name='tctRFirstName']").attr("disabled")?302:301);
	    sClientCode = $("[name='tctRepresentCode']").val();;
	    sDigit=$("[name='tctRepresentCode_Digit']").val();
	    nPerson_typ=$("[name='hddRPersonTyp']").val();
        sFirstName=encodeURIComponent($("[name='tctRFirstName']").val());
        sLastName=encodeURIComponent($("[name='tctRLastName']").val());
        sLastName2=encodeURIComponent($("[name='tctRLastName2']").val());

        ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sCodispl=BC003_K&sModule=Client&sProject=ClientSeq&sRoleCode=&LinkParamsClient='+sClientCode+'&sClientCode='+sClientCode+'&nMainAction='+nMainAction+'&LinkSpecialAction='+nMainAction+'&sDigit='+sDigit+'&LinkParamsDigit='+sDigit+'&nPerson_typ='+nPerson_typ+'&sOriginalForm=&sLinkSpecial=1&LinkParamsClientControl=tctClientNavigation&sFirstName='+sFirstName+'&sLastName='+sLastName+'&sLastName2='+sLastName2, 'ClientSeq', 750, 500, 'no', 'yes', 20, 20)
    }

}

	
	
//RedrawFieldsByPersonTyp::Cambia e comportamiento de los datos del beneficiario dependiendo si se trata de una persona natural o jurídica
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function RedrawFieldsByPersonTyp(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    if (qs("Type") == "PopUp"){
		if (document.forms[0].cbePersonTyp.value != '1'){
			document.forms[0].tctLastName.parentElement.style.display='none';
		    document.forms[0].tctLastName.parentElement.parentElement.getElementsByTagName('TD')[2].style.display='none';
			document.forms[0].tctFirstName.parentElement.parentElement.getElementsByTagName('LABEL')[1].innerHTML='Nombre jurídico';
			document.forms[0].tctFirstName.cols='62';
			document.forms[0].tctFirstName.parentElement.colSpan='3';
		    document.forms[0].tctFirstName.parentElement.parentElement.getElementsByTagName('TD')[0].style.display='none';
		    document.forms[0].tctFirstName.parentElement.parentElement.getElementsByTagName('TD')[1].style.display='none';
		}
		else{
			document.forms[0].tctLastName.parentElement.style.display='inline';
		    document.forms[0].tctLastName.parentElement.parentElement.getElementsByTagName('TD')[2].style.display='inline';
			document.forms[0].tctFirstName.parentElement.parentElement.getElementsByTagName('LABEL')[1].innerHTML='Nombres';
			document.forms[0].tctFirstName.cols='20';
			document.forms[0].tctFirstName.parentElement.colSpan='1';
		    document.forms[0].tctFirstName.parentElement.parentElement.getElementsByTagName('TD')[0].style.display='inline';
		    document.forms[0].tctFirstName.parentElement.parentElement.getElementsByTagName('TD')[1].style.display='inline';
		}
	}
}

//refreshNavigationLinkStatus: Levanta la secuencia del destinatario del pago.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function refreshNavigationLinkStatus(){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    if (self.document.forms[0].tctRepresentCode.value!="")
        ShowClientSequence(2)
    else
        ShowClientSequence(1);
}

//ChangeValues: Cambia y asigna los valores según la opción seleccionada.
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeValues(Option){
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	var ldblRent=0;
	var ldblParticip=0;
	var ldblCurrentRent = <%=Session("nRent")%>;
	
	
	switch(Option)
	{
		case "Client_SI629":
		{
		    if(self.document.forms[0].tctClientCode.value!="")
			    ShowPopUp("/VTimeNet/Claim/ClaimSeq/ShowDefValues.aspx?Field=Client_SI629&sClient=" + self.document.forms[0].tctClientCode.value, "ShowDefValuesClient", 1, 1,"no","no",2000,2000);

			break;
		}
		case "ClientRep":
		{
		    if(self.document.forms[0].tctRepresentCode.value!=""){
                //$("[name=tctClientNavigation]").parent().parent().parent().find("TD")[46].innerHTML = "Información adicional del representante"
			    ShowPopUp("/VTimeNet/Claim/ClaimSeq/ShowDefValues.aspx?Field=ClientRep&sClient=" + self.document.forms[0].tctRepresentCode.value, "ShowDefValuesClient", 1, 1,"no","no",2000,2000)
            }
            /*else
                $("[name=tctClientNavigation]").parent().parent().parent().find("TD")[46].innerHTML = "Información adicional del beneficiario";*/
			
            break;
		}
		
		case "Cover":
		{
		    top.fraFolder.document.forms[0].hddCurrency.value = top.fraFolder.document.forms[0].valCover_nCurrency.value;
		    break;
		}
		
		case "Rent":
		{		    
		    if(typeof(self.document.forms[0].tcnRent)!='undefined')
		    {
		        if(self.document.forms[0].tcnRent.value!=0)
		        {
		            ldblParticip = insConvertNumber(self.document.forms[0].tcnParticip.value);
		            ldblRent     = (ldblCurrentRent * ldblParticip) / 100;
		            self.document.forms[0].tcnRent.value= VTFormat(ldblRent,'','','',6,true);
                }
		    }
		}
	}
}

//------------------------------------------------------------------------------------
function insChangeCase(Field) {
//------------------------------------------------------------------------------------   
   var trCase_num = '';
   var lstrDeman_type = '';
   var lstrClient = '';
   var lstrString = '';
   var lstrLocation = '';

   lstrString += self.document.forms[0].cbeCase.value;

   lstrLocation += document.location.href;
   lstrLocation = lstrLocation.replace(/&sCase_num.*/,"");
   lstrLocation = lstrLocation + "&sCase_num=" + lstrString;
   document.location.href = lstrLocation;
}


$(function() {
	if (qs("Type")=="PopUp"){
		$("[name=tctClientNavigation]").toggle();
        //if(qs("Type")=="Update"){
        //    $("[name=tctLastName]").prop("disabled",true);
		//    $("[name=tctLastName2]").prop("disabled",true);
		//    $("[name=tctFirstName]").prop("disabled",true);
		//    $("[name=tcdBirthdat]").prop("disabled",true);
        //}
	}
});

</SCRIPT>
<%
'+ Si la transacción es Declaración de siniestro, asignar Sucursal Oficina  
'+ y Agencia de pago, los valores de la póliza relacionada con el siniestro 
If Request.QueryString("Type") = "PopUp" And Request.QueryString("Action") = "Add" Then
	Response.Write("<SCRIPT>self.document.forms[0].cbeOfficeAgen.Parameters.Param1.sValue =" & Session("nOffice_pol") & ";</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].cbeAgency.Parameters.Param1.sValue =" & Session("nOffice_pol") & ";</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].cbeAgency.Parameters.Param2.sValue =" & Session("nOfficeAgen_pol") & ";</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].cbeOffice.value=" & Session("nOffice_pol") & ";</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].cbeOfficeAgen.value=" & Session("nOfficeAgen_pol") & ";</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].cbeAgency.value=" & Session("nAgency_pol") & ";</SCRIPT>")
End If

If Request.QueryString("Type") = "PopUp" And Request.QueryString("Action") <> "Del" Then
	Response.Write("<SCRIPT>self.document.forms[0].hddHas_Surv_Pension_Benefs.value = top.opener.document.forms[0].chkHas_Surv_Pension_Benefs.value</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].hddSummon.value = top.opener.document.forms[0].gmdSummon.value</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].hddSummon_Limit.value = top.opener.document.forms[0].gmdSummon_Limit.value</SCRIPT>")
End If

'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
Call mobjNetFrameWork.FinishPage("si629")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




