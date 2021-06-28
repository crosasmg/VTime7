<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15

Private aImmutableCovs() = {1001, 1002}
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas	
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define las variables para la carga de datos del Grid de la ventana
Dim mclsCL_Cover As eClaim.Cl_Cover
Dim mcolCL_Covers As eClaim.CL_Covers
Dim mcolCL_GM As eClaim.CL_Covers    
Dim mclsProdmaster As eProduct.Product

'+ Variable para almacenar la moneda de la última cobertura del grid
Dim mintOldCurrency As Integer
Dim mPriorGroup As Integer

'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	Dim lobjColumn As eFunctions.Column
    Dim lblnFindGastos As Boolean
    Dim mcolCL_GM As eClaim.CL_Covers
    mcolCL_GM = New CL_Covers
        
      
        
	With mobjGrid
		.Codispl = "SI007"
		.Top = 50
		.Left = 100
		.Width = 650
		.Height = 480
	End With
	
	'+Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, "Asegurado", "txtClient", 60, "0",  , "Asegurado asociado a la cobertura",  ,  ,  , True)
            Call .AddTextColumn(0, "Cobertura", "tctDescover", 60, "0", , "Cobertura asociada a la poliza en tratamiento", , , , True)
            
            'Si es Rechazo, solo muestra el estado de Rechazo y el actual		
		If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimRejection Then
			lobjColumn = .AddPossiblesColumn(40307, "Estado", "cbeReservstat", "Table141", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insChangeStatus(this.value);",  ,  , "Estado de la cobertura")
			lobjColumn.TypeList = CShort("1")
			lobjColumn.List = "2,5"
			lobjColumn.GridVisible = False
		Else
			'call .AddPossiblesColumn (40307,"Estado","cbeReservstat","Table141",eFunctions.Values.eValuesType.clngComboType,,,,,,"insChangeStatus(this.value);",,,"Estado de la cobertura")
			lobjColumn = .AddPossiblesColumn(40307, "Estado", "cbeReservstat", "Table141", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insChangeStatus(this.value);",  ,  , "Estado de la cobertura")
			lobjColumn.TypeList = CShort("2")
			lobjColumn.List = "5"
			lobjColumn.GridVisible = False
		End If
		
           
            
                If Request.QueryString("Type") <> "PopUp" Then
                    Call .AddCheckColumn(0, "Presentación de facturas", "chkBill_ind", "", 1, "2", , True)
                Else
                    Call .AddCheckColumn(0, "Presentación de facturas", "chkBill_ind", "", 1, "2", "insChangeValue(this);")
                End If
            'If Request.QueryString("Action2") <> "Update_GM" Then
            If True Then
                If CStr(Session("sBrancht")) = "1" Then
                    Call .AddNumericColumn(40310, "Indemnización", "tcnDamages", 18, CStr(0), False, "Indemnización", True, 6, , , "insCalReserve(false);")
                Else
                    Call .AddNumericColumn(40310, "Indemnización", "tcnDamages", 18, CStr(0), False, "Indemnización", True, 6, , , "insCalReserve(false);")
                End If
            End If
            'Validacion se muestra solo para Gastos Medicos
            Dim serverPath As String = ConfigurationManager.AppSettings("Mutual.GastosMedicos").ToString()
            If serverPath = "true" Then
                If Request.QueryString("Type") = "PopUp" Then
                
                    'Realiza verificacion para mostrar los GM'   
                    
                    lblnFindGastos = mcolCL_GM.Find_SI007_GM(Session("nClaim"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Request.QueryString("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCover"), eFunctions.Values.eTypeData.etdDouble))
                    ' Call mcolCL_GM.Find_SI007_GM(Session("nClaim"), CInt(Session("nBranch")), CInt(Session("sCertype")), CInt(Session("nProduct")), 0)
                    'CInt(Session("nPolicy")), CInt(Session("nCertif")), mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate), CStr(Session("sBrancht")), lintCase_num, lintDeman_type, CInt(Session("nTransaction")))
                        
                    'Si tiene se muestRa boton para realizar consulta        
                    If lblnFindGastos Then
                        'muestra boton de Gasots Medicos                    
                        Call .AddAnimatedColumn(40310, "Gastos Medicos", "btnGm", "/VTimeNet/images/btn_ValuesOff.png", ,"/VTimeNet/Claim/claimseq/SendCaseRGM.aspx" , "JAVASCRIPT: insOpenGastosMedicos()", , 1)
                        'Boton para actualizar
                        
                        'Call .AddAnimatedColumn(40310, "", "btnGm2", "/VTimeNet/Images/A393Off.png", , , "JAVASCRIPT: insActualizaGastosMedicos()", , 1)
                                               
                        'Cuando se presiona el boton actualizar de la ventana
                        If Request.QueryString("Action2") = "Update_GM" Then
                            'se consulta en la tabla temporal si hay datos
                            Dim txtAmount As Double
                            Dim mcolCL_GM_TMP As New eClaim.Claim
                            'algo = "llega"
                            
                            'Call mcolCL_GM_TMP.Find_SI007_GM_TEMP()
                            'asigna el valor a la caja de texto
                            'Call .AddTextColumn(0, "Cobertura", "tctDescover", 60, "0", , "Cobertura asociada a la poliza en tratamiento", , , , True)
                           
                           
                            txtAmount = mcolCL_GM_TMP.nAmount
                            ' mobjGrid.Columns("tctDescover").DefValue = txtAmount
                            Dim mclsAuto = New Automobile
                            'Call mclsAuto.Find_SI007_GM_TEMP()
                            txtAmount = mclsAuto.sRegist
                           
                            'mobjGrid.Columns("tcnFra_amount2").DefValue = txtAmount
                                                   
                            
                            'Call .AddNumericColumn(40320, "Monto enviado", "TEMP", 18, CStr(0), False, "Monto Enviado", True, 6, , , "")
                            'Call .AddNumericColumn(40310, "Indemnización", "tcnDamages", 18, CStr(0), False, "Indemnización", True, 6, , , "insCalReserve(false);")
                            'Call .AddNumericColumn(40390, "Monto Enviado", "tcnFra_amount", 18, CStr(mclsAuto.sRegist), , "Monto Enviado", True, 6, , , , False)
                            'Call .AddNumericColumn(40395, "Monto Enviado", "tcnFra_amount2", 18, "1232352", False, "Monto Enviado", True, 6, , , "insCalReserve2(false);")
                            'Dim txt As New TextBox
                            'txt.Text = txtAmount
                            'nCase = mclsClaim_auto.nCase_num
                            'dCasualtyDate = mclsClaim_auto.dDoccurdat
                            'dClaimDate = mclsClaim_auto.dDecladat
                            
                            
                        End If
                    End If
                End If
            End If
            
         
            Call .AddPossiblesColumn(40308, "F/D", "cbeFrantype", "Table64", eFunctions.Values.eValuesType.clngComboType, CStr(0), , , , , , True, , vbNullString)
            Call .AddPossiblesColumn(40308, "Monto / %", "cbeFrantype_aux", "Table5584", eFunctions.Values.eValuesType.clngComboType, CStr(0), , , , , , True, , vbNullString)
            
            Call .AddNumericColumn(40311, "Franquicia / Deducible", "tcnFra_amount", 18, CStr(0), , "Franquicia/Deducible", True, 6, , , , False)
            
            Call .AddNumericColumn(40312, "Provisión", "tcnReserve", 18, CStr(0), , "Monto de provisión", True, 6, , , , True)
            Call .AddNumericColumn(40313, "Estimación", "tcnDamProf", 18, CStr(0), , "Estimación según profesional", True, 6, , , "FormatField(this.value);")
            Call .AddPossiblesColumn(40309, "Moneda", "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0), , , , , , False, , vbNullString)
            Call .AddNumericColumn(40314, "Factor de cambio", "tcnExchange", 11, CStr(0), , "Factor de cambio", True, 6, , , , True)
		
            Call .AddHiddenColumn("tctClient", "")
            Call .AddHiddenColumn("tcnReserveAnt", "0")
            Call .AddHiddenColumn("nReserve_o", "0")
            Call .AddHiddenColumn("nExchange_o", "0")
            Call .AddHiddenColumn("sAutomRep", "0")
            Call .AddHiddenColumn("nFixAmount", "0")
            Call .AddHiddenColumn("nMaxAmount", "0")
            Call .AddHiddenColumn("nMinAmount", "0")
            Call .AddHiddenColumn("nRate", "0")
            Call .AddHiddenColumn("nModulec", "0")
            Call .AddHiddenColumn("nCover", "0")
            Call .AddHiddenColumn("tcnCapital", "0")
            Call .AddHiddenColumn("tcnBranch_rei", "0")
            Call .AddHiddenColumn("tcnBranch_led", "0")
            Call .AddHiddenColumn("tcnBranch_est", "0")
            Call .AddHiddenColumn("tcnFrandeda", "0")
            Call .AddHiddenColumn("tcnPayAmount", "0")
            Call .AddHiddenColumn("tcnGroup", "0")
            Call .AddHiddenColumn("tctFran_Ind", "")
            Call .AddHiddenColumn("tctRoureser", "")
            Call .AddHiddenColumn("tctInsurini", "")
            Call .AddHiddenColumn("tcnCase_num", "0")
            Call .AddHiddenColumn("tcnDeman_type", "0")
            Call .AddHiddenColumn("nCurrency_o", "0")
            Call .AddHiddenColumn("tctsIndCapIliCover", "")
            Call .AddHiddenColumn("tctsSchema", "")
            Call .AddHiddenColumn("tctCaren_type", "")
            Call .AddHiddenColumn("tcnCaren_quan", "0")
            Call .AddHiddenColumn("hddBill_ind", "1")
            Call .AddHiddenColumn("hddnMonto", CStr(0))
            Call .AddHiddenColumn("hddsFrancapl", "")
            Call .AddHiddenColumn("hddnFranAmount", CStr(0))
            Call .AddHiddenColumn("tcdCover", "")
            Call .AddHiddenColumn("hddsCacalili", "")
            Call .AddHiddenColumn("tctCldeathi", "")
            Call .AddHiddenColumn("tcnRouAmount", "0")
            Call .AddHiddenColumn("hddImmutable", "2")
            
            mobjGrid.sEditRecordParam = "nModulec=' + marrArray[lintIndex].nModulec + '"
            mobjGrid.sEditRecordParam = mobjGrid.sEditRecordParam & "&ncover=' + marrArray[lintIndex].nCover + '"
           
        End With
	Dim lclslife_claim As eClaim.Life_claim
	With mobjGrid
		lclslife_claim = New eClaim.Life_claim
		If Request.QueryString("nCase_num") = vbNullString Or Request.QueryString("nDeman_type") = vbNullString Then
			Call lclslife_claim.Find(CDbl(Session("nClaim")), 1, 1, True)
		Else
			Call lclslife_claim.Find(CDbl(Session("nClaim")), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdInteger), True)
		End If
		If lclslife_claim.nIn_lif_typ <> 2 Then
			.Columns("tctDescover").EditRecord = True
			.Columns("Sel").OnClick = "insCheckSelClick(this)"
		Else
			.Columns("Sel").Disabled = True
		End If
		.DeleteButton = False
		.AddButton = False
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
            End If
            
		'UPGRADE_NOTE: Object lclslife_claim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		lclslife_claim = Nothing
        End With
End Sub

'%insPreSI007: Esta función se encarga de cargar los datos en la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreSI007()
	'--------------------------------------------------------------------------------------------    
	Dim lblnFind As Boolean
	Dim lstrAlert As String
	Dim lclsClaimCases As eClaim.ClaimBenefs
	Dim lstrFirstCase As String
	Dim lintCase_num As Integer
	Dim lintDeman_type As Integer
	Dim lstrClient As String
	Dim lclsClaim As eClaim.Claim
	
	Dim lobjErrors As eGeneral.GeneralFunction
	lobjErrors = New eGeneral.GeneralFunction
	
	lblnFind = False
	
	If Request.QueryString("nCase_num") = vbNullString Or Request.QueryString("nDeman_type") = vbNullString Or Request.QueryString("sClient") = vbNullString Then
		lclsClaimCases = New eClaim.ClaimBenefs
		If lclsClaimCases.Find_BenefByClaim(CDbl(Session("nClaim")), 0, 1) Then
			lstrFirstCase = CStr(lclsClaimCases.Item(1).nCase_num) & "/" & CStr(lclsClaimCases.Item(1).nDeman_type) & "/" & lclsClaimCases.Item(1).sClient
			lintCase_num = lclsClaimCases.Item(1).nCase_num
			lintDeman_type = lclsClaimCases.Item(1).nDeman_type
			lstrClient = lclsClaimCases.Item(1).sClient
			'UPGRADE_NOTE: Object lclsClaimCases may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			lclsClaimCases = Nothing
			lblnFind = True
		End If
	Else
		lstrFirstCase = Request.QueryString("nCase_num") & "/" & Request.QueryString("nDeman_type") & "/" & Request.QueryString("sClient")
		lintCase_num = mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble)
		lintDeman_type = mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)
		lstrClient = Request.QueryString("sClient")
		lblnFind = True
	End If
	
	'+ Si la fecha de ocurrencia del siniestro está fuera de la vigencia de la póliza, no se realiza 
	'+ la lectura de las coberturas. VVERA 25/03/2003.	
	If CBool(Session("bPolicyVigency")) = True Then
		lblnFind = False
		lstrAlert = "Err. 4019 " & lobjErrors.insLoadMessage(4019)
		Response.Write("<SCRIPT>alert('" & lstrAlert & "')</" & "Script>")
	End If
	'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lobjErrors = Nothing
	
	
	Response.Write("<SCRIPT> mintCase_num='" & lintCase_num & "';</" & "Script>")
	Response.Write("<SCRIPT> mintDeman_type='" & lintDeman_type & "';</" & "Script>")
	Response.Write("<SCRIPT> mstrClient='" & lstrClient & "';</" & "Script>")

	'+Se verifica que el proceso no venga desde SI021	
	If CStr(Session("SI007_Codispl")) = vbNullString Then
		Session("sProcess_SI021") = 0
	End If
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=9501>Caso</LABEL></TD>" & vbCrLf)
Response.Write("        <TD COLSPAN=""3"">")

	
	With mobjValues
		.BlankPosition = False
		.Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeCase", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, "" & lstrFirstCase, True,  ,  ,  ,  , "insChangeCase(this)",  ,  , "", eFunctions.Values.eTypeCode.eString))
		Response.Write(mobjValues.HiddenControl("tctclient", lstrClient))
		Response.Write(mobjValues.HiddenControl("tcnDeman_type", CStr(lintDeman_type)))
		Response.Write(mobjValues.HiddenControl("cbeCases", CStr(lintCase_num)))
	End With
	
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("    </TR>                " & vbCrLf)
Response.Write("</TABLE> " & vbCrLf)
Response.Write("")

	
	
	If lblnFind Then
		lclsClaim = New eClaim.Claim
		
		Call lclsClaim.Find(CDbl(Session("nClaim")))
		If CStr(Session("sBrancht")) = "" Then
			mclsProdmaster = New eProduct.Product
			Call mclsProdmaster.FindProdMaster(CInt(Session("nBranch")), CInt(Session("nProduct")))
			mclsProdmaster = New eProduct.Product
			Session("sBrancht") = mclsProdmaster.sBrancht
			'UPGRADE_NOTE: Object mclsProdmaster may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
			mclsProdmaster = Nothing
		End If
		If mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmnull Then
			Session("dOccurdate_l") = Session("dEffecdate")
		End If
		
		lblnFind = mcolCL_Covers.Find_SI007("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CInt(Session("nPolicy")), CInt(Session("nCertif")), mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate), CStr(Session("sBrancht")), CDbl(Session("nClaim")), lintCase_num, lintDeman_type, CInt(Session("nTransaction")))
		
		Session("nTotal") = 0
		'Session("nTotFra_amount") = 0 
        Dim lintIndex As Short
        lintIndex = 0
		
		For	Each mclsCL_Cover In mcolCL_Covers
			With mobjGrid
				.Columns("hddImmutable").DefValue = Iif(aImmutableCovs.Contains(mclsCL_Cover.nCover),"1","2")
  			    .Columns("Sel").Checked = mclsCL_Cover.nSel
				.Columns("tctClient").DefValue = mclsCL_Cover.sClient
                .Columns("txtClient").DefValue = mclsCL_Cover.sClient & " - " & mclsCL_Cover.sDigit & " " & mclsCL_Cover.sCliename
                 Session("sClient") = mclsCL_Cover.sClient
                 Session("sDigit") = mclsCL_Cover.sDigit
                 Session("sCliename") = mclsCL_Cover.sCliename
                    .sEditRecordParam = "nModulec=" & mclsCL_Cover.nModulec & "&ncover=" & mclsCL_Cover.nCover
                .Columns("tctDescover").DefValue = mclsCL_Cover.nCover & " - " & mclsCL_Cover.sDescover
                 Session("nCoverGM") = mclsCL_Cover.nCover
                .Columns("cbeReservstat").DefValue = mclsCL_Cover.sReservstat
				.Columns("cbeReservstat").Descript = mclsCL_Cover.sDesStatusCov
				If mclsCL_Cover.sBill_ind = "1" Then
					.Columns("chkBill_ind").DefValue = CStr(1)
					.Columns("hddBill_ind").DefValue = CStr(1)
				Else
					.Columns("chkBill_ind").Checked = False
					.Columns("hddBill_ind").DefValue = CStr(2)
				End If
				

				If Session("sBrancht") = "6" Then
					.Columns("tcnDamages").DefValue  = mclsCL_Cover.nReserve
				Else
					If (mclsCL_Cover.nDamages > 0 And mclsCL_Cover.nDamages >= mclsCL_Cover.nReserve) Then
						.Columns("tcnDamages").DefValue = CStr(mclsCL_Cover.nDamages)
					Else
						If mclsCL_Cover.sCldeathi <> vbNullString And mclsCL_Cover.nAmount > 0 Then
							.Columns("tcnDamages").DefValue = CStr(mclsCL_Cover.nAmount)
						End If
					End If
				End If
				
				.Columns("cbeFrantype").DefValue = mclsCL_Cover.sFrantype
				.Columns("cbeFrantype").Descript = mclsCL_Cover.sDesFrantype
                    '.Columns("tcnAmountSent").DefValue = "12314"
				If mclsCL_Cover.nFra_amount > 0 Then
					'+Si es mayor indica que es un porcentaje				    
					If mclsCL_Cover.nRate > 0 Then
						.Columns("cbeFrantype_aux").DefValue = CStr(1) '+ Porcentaje
						.Columns("tcnFra_amount").DefValue = CStr(mclsCL_Cover.nRate)
						.Columns("hddnMonto").DefValue = CStr(mclsCL_Cover.nFra_amount)
					Else
						.Columns("cbeFrantype_aux").DefValue = CStr(3) '+ Monto Fijo
                            .Columns("tcnFra_amount").DefValue = CStr(mclsCL_Cover.nFra_amount)
                            Dim txtAmount As Double
                            Dim mclsAuto = New Automobile
                           ' Call mclsAuto.Find_SI007_GM_TEMP()
                            txtAmount = mclsAuto.sRegist
                           
                            
                            
					End If
				Else
					If mclsCL_Cover.nRate > 0 Then
						.Columns("tcnFra_amount").DefValue = CStr(mclsCL_Cover.nRate)
						.Columns("cbeFrantype_aux").DefValue = CStr(1) '+ Porcentaje 
					Else
						.Columns("tcnFra_amount").DefValue = CStr(mclsCL_Cover.nFra_amount)
						.Columns("cbeFrantype_aux").DefValue = CStr(4) '+ No aplica 
					End If
				End If
				
                    .Columns("tcnReserve").DefValue = CStr(mclsCL_Cover.nReserve)
                    If mclsCL_Cover.nReserve <> vbNullString Then
                        Session("nReserveGM") = CStr(mclsCL_Cover.nReserve)
                    End If
                    .Columns("tcnReserveAnt").DefValue = CStr(mclsCL_Cover.nReserve)
                    .Columns("tcnDamProf").DefValue = CStr(mclsCL_Cover.nDamprof)
                    .Columns("cbeCurrency").DefValue = CStr(mclsCL_Cover.nCurrency)
                    .Columns("cbeCurrency").Descript = mclsCL_Cover.sDesCurrency
                    .Columns("tcnExchange").DefValue = CStr(mclsCL_Cover.nExchange)
                    .Columns("nReserve_o").DefValue = CStr(mclsCL_Cover.nReserve_o)
                    .Columns("nExchange_o").DefValue = CStr(mclsCL_Cover.nExchange_o)
                    .Columns("nCurrency_o").DefValue = CStr(mclsCL_Cover.nCurrency)
                    .Columns("sAutomRep").DefValue = mclsCL_Cover.sAutomrep
                    .Columns("nFixAmount").DefValue = CStr(mclsCL_Cover.nFixamount)
                    .Columns("nMaxAmount").DefValue = CStr(mclsCL_Cover.nMaxamount)
                    .Columns("nMinAmount").DefValue = CStr(mclsCL_Cover.nMinamount)
                    .Columns("nRate").DefValue = CStr(mclsCL_Cover.nRate)
                    .Columns("nModulec").DefValue = CStr(mclsCL_Cover.nModulec)
                    Session("nModulec") = CStr(mclsCL_Cover.nModulec)
                    .Columns("nCover").DefValue = CStr(mclsCL_Cover.nCover)
                    Session("nCover") = CStr(mclsCL_Cover.nCover)
                    .Columns("tcnCapital").DefValue = CStr(mclsCL_Cover.nCapital)
				
				
                    .Columns("tcnBranch_rei").DefValue = CStr(mclsCL_Cover.nBranch_rei)
                    .Columns("tcnBranch_led").DefValue = CStr(mclsCL_Cover.nBranch_led)
                    .Columns("tcnBranch_est").DefValue = CStr(mclsCL_Cover.nBranch_est)
                    .Columns("tcnFrandeda").DefValue = CStr(mclsCL_Cover.nFrandeda)
				
				
                    .Columns("tcnPayAmount").DefValue = CStr(mclsCL_Cover.nPay_amount)
                    .Columns("tcnGroup").DefValue = CStr(mclsCL_Cover.nGroup)
                    .Columns("tctFran_Ind").DefValue = mclsCL_Cover.sFran_Ind
				
                    .Columns("tctRoureser").DefValue = mclsCL_Cover.sRoureser
                    .Columns("tctCldeathi").DefValue = mclsCL_Cover.sCldeathi
				
                    If mclsCL_Cover.sCldeathi <> vbNullString Then
                        .Columns("tcnRouAmount").DefValue = CStr(mclsCL_Cover.nAmount)
                    Else
                        .Columns("tcnRouAmount").DefValue = CStr(0)
                    End If
				
                    .Columns("tctInsurini").DefValue = mclsCL_Cover.sInsurini
                    .Columns("tcnCase_num").DefValue = CStr(lintCase_num)
                    .Columns("tcnDeman_type").DefValue = CStr(lintDeman_type)
                    .Columns("tctCaren_type").DefValue = mclsCL_Cover.sCaren_type
                    .Columns("tcnCaren_quan").DefValue = CStr(mclsCL_Cover.nCaren_quan)
                    .Columns("tcdCover").DefValue = CStr(mclsCL_Cover.dCoverDate)
                    '+aplica sobre capital o siniestro
                    .Columns("hddsFrancapl").DefValue = mclsCL_Cover.sFrancapl
				
                    .Columns("hddsCacalili").DefValue = mclsCL_Cover.sCacalili
				
                    Session("nTotal") = CDbl(Session("nTotal")) + mclsCL_Cover.nReserve
                    '.Columns("btnGM").HRefScript = "insOpenGastosMedicos(" & lintIndex & ")"
                    '.Columns("btnGm2ReloadAction").HRefScript = "insOpenGastosMedicos(" & lintIndex & ")"
                End With
			
                
                
                
			Response.Write(mobjGrid.DoRow())
			
             mintOldCurrency = mclsCL_Cover.nCurrency
                lintIndex = lintIndex + 1
		Next mclsCL_Cover
	End If
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.HiddenControl("tcnOldCurrency", CStr(mintOldCurrency)))
End Sub

'% insPreSI007Upd. Se define esta funcion para contruir el contenido de la ventana UPD de las reservas del sinietro
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreSI007Upd()
	'------------------------------------------------------------------------------------------------------------------
	
	Response.Write(mobjValues.HiddenControl("htcnDeman_typ", ""))
	Response.Write(mobjValues.HiddenControl("hcbeCase", ""))
	Response.Write(mobjValues.HiddenControl("hddClient", ""))
	mclsCL_Cover.nAmount = 5678
	
	Response.Write("<SCRIPT>AssignValues();</" & "Script>")
	
	With Request

            Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaimSeq.aspx", "SI007", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
	End With
	
	If Request.QueryString("CalPremium") = "1" Then
		Response.Write("<SCRIPT>var mdblDamages ='0" & Request.Form("tcnDamages") & "'; var mdblFrandeda='0" & Request.Form("tcnFrandeda") & "'; var mdblReserve ='0" & Request.Form("tcnReserve") & "';</" & "Script>")
		
		With mclsCL_Cover
			.sCertype = "2"
			.nBranch = mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(CStr(Session("nProduct")), eFunctions.Values.eTypeData.etdDouble)
			.nPolicy = mobjValues.StringToType(CStr(Session("nPolicy")), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToType(CStr(Session("dOccurdate_l")), eFunctions.Values.eTypeData.etdDate)
			.nModulec = mobjValues.StringToType(Request.Form("nModulec"), eFunctions.Values.eTypeData.etdDouble)
			.nCover = mobjValues.StringToType(Request.Form("tcnCover"), eFunctions.Values.eTypeData.etdDouble)
			.nGroup_insu = mclsCL_Cover.nGroup
			.nClaim = mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble)
			.nCase_num = mobjValues.StringToType(Request.Form("tcnCase_num"), eFunctions.Values.eTypeData.etdDouble)
			.nDeman_type = mobjValues.StringToType(Request.Form("tcnDeman_type"), eFunctions.Values.eTypeData.etdDouble)
			.nFixAmount = mclsCL_Cover.nFixAmount
			.nMaxAmount = mclsCL_Cover.nMaxAmount
			.nMinAmount = mclsCL_Cover.nMinAmount
			.nRate = mobjValues.StringToType(Request.Form("nRate"), eFunctions.Values.eTypeData.etdDouble)
			.nCapital = mobjValues.StringToType(Request.Form("tcnCapital"), eFunctions.Values.eTypeData.etdDouble)
                .nDamages = mobjValues.StringToType(Request.Form("tcnDamages"), eFunctions.Values.eTypeData.etdDouble)
                .nAmount = mobjValues.StringToType(Request.Form("tcnAmountSent"), eFunctions.Values.eTypeData.etdDouble)
                If Request.Form("cbeFrantype") = eRemoteDB.Constants.intNull Then
				.sFrantype = ""
			Else
				.sFrantype = Request.Form("cbeFrantype")
			End If
			.sFrancapl = mclsCL_Cover.sFrancapl
			.nPay_amount = mobjValues.StringToType(Request.Form("tcnPayAmount"), eFunctions.Values.eTypeData.etdDouble)
			.nFra_amount = 0
			.sAutomRep = mclsCL_Cover.sAutomRep
			.sRoureser = mclsCL_Cover.sRoureser
			.nOpt_claityp = mobjValues.StringToType(CStr(Session("nTransaccion")), eFunctions.Values.eTypeData.etdDouble)
			.sRec_fra = "1"
			.sBrancht = Session("sBrancht")
			.sShowInd = "1"
			.nFrandeda = mobjValues.StringToType(Request.Form("tcnFrandeda"), eFunctions.Values.eTypeData.etdDouble)
			.nReserve = mobjValues.StringToType(Request.Form("tcnReserve"), eFunctions.Values.eTypeData.etdDouble)
			.nSel = mobjValues.StringToType(CStr(mclsCL_Cover.nSel), eFunctions.Values.eTypeData.etdDouble)
			.sClient = mclsCL_Cover.sClient
			.sBill_ind = mclsCL_Cover.sBill_ind
			Call .CalReserve()
			Response.Write("<SCRIPT>insUpdValues('" & CStr(.nDamages) & "', '" & CStr(.nFrandeda) & "', '" & CStr(.nReserve) & "');")
			Response.Write("document.forms[0]." & Request.QueryString("Field") & ".focus();</" & "Script>")
		End With
	Else
		Response.Write("<SCRIPT>var mdblDamages ='0'; var mdblFrandeda='0'; var mdblReserve='0'</" & "Script>")
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("	<SCRIPT>mblnDo=true</" & "SCRIPT>")

	
	Response.Write("<SCRIPT>insCalReserve(true);</" & "Script>")
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si007")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si007"

'-Se crean las instancias de las variables modulares

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si007"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))

mclsCL_Cover = New eClaim.Cl_Cover
mcolCL_Covers = New eClaim.CL_Covers

'- Variable en JScript que indica la acción que seleccionó el usuario
Response.Write("<SCRIPT>var mstrTransaction='" & Session("nTransaction") & "'</SCRIPT>")

If Request.QueryString("Type") <> "PopUp" Then
	With Response
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
        End With
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If
    End If
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>

<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
	End If
	.Write(mobjValues.StyleSheet() & vbCrLf)
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>      
<SCRIPT>
 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 25-03-13 7:35 $|$$Author: Jrengifo $"

var mblnDo=false;
//%FormatField: formatea el valor introducido en el campo 'Estimación según profesional'"
//---------------------------------------------------------------------------------"
function FormatField(Field)
//---------------------------------------------------------------------------------
{
    with (self.document.forms[0]){
        if(elements['tcnDamProf'].value!=''){
            elements['tcnDamProf'].value = VTFormat(insConvertNumber(Field),'','','',6, true);
        }else{
            elements['tcnDamProf'].value = VTFormat(insConvertNumber(0),'','','',6, true);
        }
    }
}
//-------------------------------------------------------------------------------------------
function insChangeStatus(Field)
//-------------------------------------------------------------------------------------------
{
    with(self.document.forms[0])
    {
      if (cbeReservstat.value == 5)
      {
          elements['tcnDamages'].value = VTFormat(insConvertNumber(0),'','','',6, true);
          elements['tcnReserve'].value = VTFormat(insConvertNumber(0),'','','',6, true);
      }
    }
}

//%AssignValues: Asigna los valores de la forma madre a la PopUp para luego pasarlos como Querystring
//---------------------------------------------------------------------------------
function AssignValues()
//---------------------------------------------------------------------------------
{    with (self.document.forms[0])
    {
        htcnDeman_typ.value = top.opener.document.forms[0].tcnDeman_type.value;
        hcbeCase.value = top.opener.document.forms[0].cbeCases.value;
        hddClient.value = top.opener.document.forms[0].tctclient.value;
		
    }
}

//%SetDamagesFieldStatus: Habilita o inhablita el campo de indemnización
//---------------------------------------------------------------------------------
function SetDamagesFieldStatus()
//---------------------------------------------------------------------------------
{    
    with (self.document.forms[0])
    {
		if (qs("Type")=="PopUp"){
			tcnDamages.disabled = (hddImmutable.value == "1");
		}
    }
}

//%SetReservstatFieldStatus: Habilita o inhablita el campo de status de la cobertura. Y le asogna valores por defecto
//---------------------------------------------------------------------------------
function SetReservstatFieldStatus()
//---------------------------------------------------------------------------------
{    
    with (self.document.forms[0])
    {
		if (qs("Type")=="PopUp" && mstrBrancht =='6' && insConvertNumber(tcnReserve.value)<=0){
			cbeReservstat.disabled = true;
			cbeReservstat.value = 10;
		}
    }
}	


//---------------------------------------------------------------------------------
function insUpdValues(ldblDamages, ldblFrandeda, ldblReserve){
//---------------------------------------------------------------------------------
    mdblFrandeda = ldblFrandeda;
    with (document.forms[0])
    {
        elements['tcnDamages'].value = VTFormat(ldblDamages,'','','',6, true);
        elements['tcnFrandeda'].value = VTFormat(ldblFrandeda,'','','',6, true);
        elements['tcnReserve'].value = VTFormat(ldblReserve,'','','',6, true);
    }
}
function insCalReserve2(){
var ldblFra_amount=0;
ldblFra_amount = tcnFra_amount2.value;
}
//---------------------------------------------------------------------------------
function insCalReserve(isLoading){
//---------------------------------------------------------------------------------
    var ldblDamages=0;
    var ldblDamages2=0;
    var ldblFrandeda=0;
    var ldblReserve=0;
    var ldblCapital=0;
    var ldblCapital_Cover=0;
    var ldblFra_amount=0;
    var ldblFranAmouPerc=0;
    var ldblRouAmount=0;
    var ldblMaxAmount=0;
    var ldblMinAmount=0;        
    var montoEnviado=0;
    
    
    if (!mblnDo) return;
    with (document.forms[0]){

        if(tcnDamages.value==''){
            ldblDamages=0;
        }else{
            ldblDamages=tcnDamages.value;
        }

//Estimacion de daños
        ldblDamages = ldblDamages;
//Monto fijo.
        ldblFrandeda = tcnFrandeda.value;
//Capital de la cobertura aplicada al porcentaje
        ldblCapital = hddnMonto.value;
//Capital de la cobertura
        ldblCapital_Cover = tcnCapital.value;
        
//Monto Franquicia / Deducible en Porcentaje o Monto Fijo
        ldblFra_amount = tcnFra_amount.value;
       // montoEnviado = tcnFra_amount2.value;
//Monto maximo de deducible 
        ldblMaxAmount = nMaxAmount.value;
//Monto minimo de deducible
        ldblMinAmount = nMinAmount.value;
//Monto pagado 
        lblPayAmount = tcnPayAmount.value;
        
//Provision o reserva calculada por rutina

        ldblRouAmount = tcnRouAmount.value;
    
//%tipo de aplicacion de la franquicia/deducible
//%aplica sobre capital
        if (hddsFrancapl.value=='2'){

    //+Cuando es deducible
            if (cbeFrantype.value==3){
    //+ si es un porcentaje    
                if (cbeFrantype_aux.value==1){
                    
    //+ Provision es igual al monto del capital de la cobertura 
                    if (insConvertNumber(ldblDamages) < insConvertNumber(ldblCapital)){
                        ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                    }else{
                        ldblReserve = VTFormat(insConvertNumber(ldblDamages) - insConvertNumber(ldblCapital) ,'', '', '', 6, true);
                        ldblFranAmouPerc = insConvertNumber(ldblCapital); 
                    }
    //+Monto fijo
                }else{
                    if (insConvertNumber(ldblDamages) < insConvertNumber(ldblFrandeda)){
                        ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                    }else{
                        ldblReserve = VTFormat(insConvertNumber(ldblDamages) - insConvertNumber(ldblFrandeda) ,'', '', '', 6, true);
                    }
                }
    //+si es una franquicia
            }else{
                if (cbeFrantype.value==2){
    //+ si es un porcentaje    
                               
                    if (cbeFrantype_aux.value==1){
                    
                     
                        if (insConvertNumber(ldblDamages) > insConvertNumber(ldblCapital)){
                            ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);
                        }else{
                            ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                        }
    //+Monto fijo
                    }else{
                        if (insConvertNumber(ldblDamages) > insConvertNumber(ldblFrandeda)){
                            ldblReserve = VTFormat(insConvertNumber(ldblFrandeda),'', '', '', 6, true);
                        }else{
                            ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                        }
                    }
    //+No aplica
                }else{
                    ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);
                }
            }
        }else{

//%aplica sobre siniestro
            if (hddsFrancapl.value=='3'){ 

        //+Cuando es deducible
                if (cbeFrantype.value==3){
        //+ si es un porcentaje    
                                
                    if (cbeFrantype_aux.value==1){
                        //+se consigue el monto del deducible
                        ldblDamages2 = insConvertNumber(ldblDamages) * insConvertNumber(ldblFra_amount) / 100;

                        if (insConvertNumber(ldblMaxAmount) > 0 && insConvertNumber(ldblDamages2) > 0){        
                            if (insConvertNumber(ldblDamages2) > insConvertNumber(ldblMaxAmount)){                
                                ldblDamages2 = ldblMaxAmount;                        
                            }                        
                        }                
                        if (insConvertNumber(ldblMinAmount) > 0 && insConvertNumber(ldblDamages2) > 0){        
                            if (insConvertNumber(ldblDamages2) < insConvertNumber(ldblMinAmount)){                
                                ldblDamages2 = ldblMinAmount;                        
                            }                        
                        }                
                        
                    
                        ldblReserve = VTFormat(insConvertNumber(ldblDamages) - ldblDamages2 ,'', '', '', 6, true);
                        ldblFranAmouPerc= VTFormat(ldblDamages2,'', '', '', 6, true);
                    
        //+Monto fijo
                    }else{
                        if (insConvertNumber(ldblDamages) < insConvertNumber(ldblFra_amount)){
                            ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                        }else{
                            ldblReserve = VTFormat(insConvertNumber(ldblDamages) - insConvertNumber(ldblFra_amount) ,'', '', '', 6, true);
                        }
                    }
        //+si es una franquicia
                }else{


//revisar este proceso
                    if (cbeFrantype.value==2){
        //+ si es un porcentaje    
                        if (cbeFrantype_aux.value==1){
                        //    if (insConvertNumber(ldblDamages) > insConvertNumber(ldblCapital)){
                                ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);
                        //    }else{
                        //        ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                        //    }
        //+Monto fijo
                        }else{
                            if (insConvertNumber(ldblDamages) > insConvertNumber(ldblFra_amount)){
                                ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);
                            }else{
                                ldblReserve = VTFormat(insConvertNumber(0),'', '', '', 6, true);
                            }
                        }
        //+No aplica
                    }else{
                        ldblReserve = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true);
                    }
                }
            }else{
//No aplica

                
                if (ldblDamages==VTFormat(0,'', '', '', 6, true)){
                    ldblReserve = tcnReserve.value;
                    ldblDamages    = ldblReserve;
                }
                else{
                    ldblReserve = ldblDamages;
                }
            }
        }

/*+ PROD00071471: Por ahora se debe mantener esta lógica ya que el manejo del deducible en la póliza no se ha 
implementado en ING.
En el mini-proyecto que se hará para poner a trabajar los pagos de siniestros por VT se verá en detalle el manejo 
del deducible.*/

// Si el tipo de calculo de capital es diferente de 'Ilimitado'
        if (hddsCacalili.value!='5'){
			if (mstrBrancht =='6'){
				if (insConvertNumber(ldblDamages) > insConvertNumber(ldblRouAmount)){
					ldblReserve    = ldblRouAmount;
				}
				else if(insConvertNumber(ldblDamages) <= 0){
					ldblReserve    = 0;
				}	
			}
			else{
				if ((tctCldeathi.value=='')||(insConvertNumber(ldblRouAmount)==0)){ 
					if (insConvertNumber(ldblDamages) > insConvertNumber(ldblCapital_Cover)){
						if (insConvertNumber(ldblCapital_Cover) > 0){         
							ldblReserve = VTFormat(insConvertNumber(ldblCapital_Cover) - ldblDamages2 ,'', '', '', 6, true);                        
						} else{
							ldblReserve = VTFormat(insConvertNumber(ldblDamages) ,'', '', '', 6, true);                        
						}        
					} else{
						ldblReserve = VTFormat(insConvertNumber(ldblDamages) - ldblDamages2 ,'', '', '', 6, true);                
					}
				
				} else{

					if (insConvertNumber(ldblDamages) > insConvertNumber(ldblRouAmount)){
						ldblReserve    = ldblRouAmount;
					} else{
						  if (ldblDamages2 > 0){
							   ldblReserve = VTFormat(insConvertNumber(ldblDamages) - ldblDamages2 ,'', '', '', 6, true);        
						  } else{        
							   ldblReserve    = ldblDamages;
						  }        
					}

				}
            }
        }else{
            ldblReserve = VTFormat(insConvertNumber(ldblDamages) - ldblDamages2 ,'', '', '', 6, true);                
        }
        if (ldblReserve < 0){        
            ldblReserve = 0;                        
        }                


//PROD00071471        

//+Se actualizan los datos de la página.
//ESTO CAUABA PROBLEMAS insConvertNumber(elements['tcnReserveAnt'].value) == 0 &&
    if (!isLoading){
            mdblFrandeda = ldblReserve; 
        elements['tcnDamages'].value = VTFormat(insConvertNumber(ldblDamages),'', '', '', 6, true); 
        elements['tcnFrandeda'].value = ldblFrandeda;
        elements['hddnFranAmount'].value = ldblFranAmouPerc;         
        
        ldblReserve = insConvertNumber(ldblReserve) - insConvertNumber(lblPayAmount);
        elements['tcnReserve'].value = VTFormat(ldblReserve,'', '', '', 6, true); 
       }
    }
}

$(function() {
    SetDamagesFieldStatus();
	SetReservstatFieldStatus();
});

//--------------------------------------------------------------------------------    
function insOpenGastosMedicos(lintIndex){
//--------------------------------------------------------------------------------    
//- Cadena con direccion
    var lstrQueryString;
//- Codigo de la accion
    var lstrAction;

    lstrAction = "<%=Request.QueryString("Action")%>";
<%If Request.QueryString("Type") = "PopUp" Then%>
     //lstrQueryString = "/VTimeNet/Claim/claimseq/SendCaseRGM.aspx?sCertype=2&sCodispl=<%=Request.QueryString("sCodispl")%>&Type&Action=" + lstrAction + "&sInsured=" + document.forms[0].tctClientCode.value ;
     lstrQueryString = "/VTimeNet/Claim/claimseq/SendCaseRGM.aspx";
         
<%End If%>        
     //ShowPopUp(lstrQueryString,"Values", 425,400,"no","no", 100, 100);
     //ShowPopUp(lstrQueryString,"Values", +screen.width,+screen.height,"yes","yes", 0, 0);

   $("a.link").on("click",function(){
         window.open('/VTimeNet/Claim/claimseq/SendCaseRGM.aspx',"Values", +screen.width,+screen.height,"yes","yes", 0, 0);
     });



}
function insActualizaGastosMedicos(lintIndex){
//--------------------------------------------------------------------------------    
   var lstrQueryString;
//- Codigo de la accion
    var lstrAction;
    lstrAction = "";
    lstrAction = "<%=Request.QueryString("Action")%>"+"_GM";
	var mstrLocation = '"' + top.opener.location + '"'
	//self.document.location = mstrLocation.substr(1, mstrLocation.indexOf('?',1) - 1) + <%="'?" & Request.Params.Get("Query_String") + "&Action2=Update_GM"  + "'"%>;
    //<% Response.Write("opener.document.location.href='si007.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'") %>      
     
    <% Response.Write("top.close()")%>
    //location.reload(true); 
    window.parent.reload;
    window.close();
}


function add() {

//Create an input type dynamically.
var element = document.createElement("input");

//Create Labels
var label = document.createElement("Label");
label.innerHTML = "New Label";     

//Assign different attributes to the element.
element.setAttribute("type", "text");
element.setAttribute("value", "");
element.setAttribute("name", "Test Name");
element.setAttribute("style", "width:200px");

label.setAttribute("style", "font-weight:normal");

// 'foobar' is the div id, where new fields are to be added
var foo = document.getElementById("fooBar");

//Append the element in page (in span).
foo.appendChild(label);
foo.appendChild(element);
}

</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
<FORM METHOD="post" ID="FORM" NAME="frmSI007" ACTION="valClaimSeq.aspx?Mode=1">
<%
Call insDefineHeader()

If Request.QueryString("Type") <> "PopUp" Then
	Session("nTotal") = Request.QueryString("nTotal")
	Call insPreSI007()
	'+ Se inhabilita el combo de casos, si solo está registrado un caso                
	%>
		<SCRIPT>if (self.document.forms[0].cbeCase.length == 1) self.document.forms[0].cbeCase.disabled = true;</SCRIPT>
<%	
Else
	Response.Write("<SCRIPT> mstrBrancht='" & Session("sBrancht") & "';</" & "Script>")
	Call insPreSI007Upd()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mclsCL_Cover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsCL_Cover = Nothing
'UPGRADE_NOTE: Object mcolCL_Covers may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mcolCL_Covers = Nothing
mcolCL_GM = Nothing
%>
</FORM>
</BODY>
<%
If Request.QueryString("nState") <> vbNullString Then
	Response.Write("<SCRIPT>self.document.forms[0].cbeReservstat.value = " & Request.QueryString("nState") & " ;</SCRIPT>")
End If
%>

</HTML>
<SCRIPT>
var mintCase_num 
var mintDeman_type 
var mstrClient 
//------------------------------------------------------------------------------------
function insChangeCase(Field) {
//------------------------------------------------------------------------------------   
   var lstrCase_num = '';
   var lstrDeman_type = '';
   var lstrClient = '';
   var lstrString = '';
   var lstrLocation = '';

                                                                
    lstrString += Field.value
    lstrCase_num = lstrString.substring(0,(lstrString.indexOf("/")))
    lstrDeman_type = lstrString.substr(lstrString.indexOf("/")+1,1)
    lstrClient += lstrString.replace(/.*\//,"")   
    lstrLocation += document.location.href
    lstrLocation = lstrLocation.replace(/&nCase_num.*/,"")
    lstrLocation = lstrLocation + "&nCase_num=" + lstrCase_num + "&nDeman_type=" + lstrDeman_type + "&sClient=" + lstrClient
    lstrString = "nCase_num=" + mintCase_num + "&nDeman_type=" + mintDeman_type + "&sClient=" + mstrClient + '&nOldCurrency=' + self.document.forms[0].tcnOldCurrency.value
    insDefValues('Reser_Total',lstrString,'/VTimeNet/Claim/ClaimSeq');
	mintCase_num=lstrCase_num;
	mintDeman_type=lstrDeman_type;
	mstrClient=lstrClient;
    document.location.href = lstrLocation;
   
}
//-------------------------------------------------------------------------------------------
function insChangeValue(Field){
//-------------------------------------------------------------------------------------------
    if (Field.checked)
    {
        self.document.forms[0].hddBill_ind.value= 1;
    }
    else
    {
        self.document.forms[0].hddBill_ind.value= 2;    
    }

}
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field){
//-------------------------------------------------------------------------------------------
//+ Se levanta la ventana PopUp para actualizar el registro
    var lstrString = '';
    lstrString = "nModulec=" + marrArray[Field.value].nModulec + '&nCover=' + marrArray[Field.value].nCover
    if(Field.checked){
        EditRecord(Field.value,nMainAction,'Update',lstrString)
        //EditRecord(Field.value, nMainAction)
        Field.checked = !Field.checked
    }
    else
    {
    
    
           
        if (marrArray[Field.value].tcnPayAmount > 0)
        {
			alert('No puede eliminar cobertura con pagos realizados')
			Field.checked = !Field.checked
        }
        else
        {
		    insDefValues('CoverDel','sClient='+ marrArray[Field.value].tctClient + 
		                            '&nCurrency=' +  marrArray[Field.value].cbeCurrency +
									'&nCurrency_o=' + marrArray[Field.value].nCurrency_o + 
									'&nDeman_type=' + marrArray[Field.value].tcnDeman_type +
									'&nCase_num=' + marrArray[Field.value].tcnCase_num +
									'&nExchange=' + marrArray[Field.value].tcnExchange +
									'&nDamages=0&nReserve=0'+ 
									'&nPayAmount=' + marrArray[Field.value].tcnPayAmount +
									'&nFra_amount=' + marrArray[Field.value].tcnFra_amount +
		                            '&tcnAmountSent=' + marrArray[Field.value].tcnAmountSent +
									'&nFrandeda=' + marrArray[Field.value].tcnFrandeda +
									'&nDamProf=' + marrArray[Field.value].tcnDamProf +
									'&nBranch_est=' + marrArray[Field.value].tcnBranch_est +
									'&nBranch_rei=' + marrArray[Field.value].tcnBranch_rei +
									'&nBranch_led=' + marrArray[Field.value].tcnBranch_led +
									'&nModulec=' + marrArray[Field.value].nModulec +
									'&nCover=' + marrArray[Field.value].nCover +
									'&nGroup=' + marrArray[Field.value].tcnGroup +
									'&nReservstat=' + marrArray[Field.value].cbeReservstat +
									'&nFrantype=' + marrArray[Field.value].cbeFrantype +
									'&sAutomRep=' + marrArray[Field.value].sAutomRep +
									'&nReserveAnt=' + marrArray[Field.value].tcnReserveAnt +
									'&nBill_ind=' + marrArray[Field.value].hddBill_ind,'/VTimeNet/Claim/ClaimSeq')		
        }
    }
}
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("si007")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




