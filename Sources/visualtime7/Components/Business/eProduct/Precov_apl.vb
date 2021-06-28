Option Strict Off
Option Explicit On
Public Class Precov_apl
	'%-------------------------------------------------------%'
	'% $Workfile:: Precov_apl.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 26/04/04 19.10                               $%'
	'% $Revision:: 38                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Statement of the public variables
	'**- Precov_apl - ACM - 27/04/2001
	'- Declaración de las variables públicas de la clase según la estructura de la tabla
	'- Precov_apl - ACM - 27/04/2001
	
	'   Column_name                          Type        Computed    Length      Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	'   ---------------------------------------------------------------------------------------------------------------------------------------
	Public nModulec As Integer ' smallint   no          2           5     0     no        (n/a)               (n/a)
	Public nCover As Integer ' smallint   no          2           5     0     no        (n/a)               (n/a)
	Public nBranch As Integer ' smallint   no          2           5     0     no        (n/a)               (n/a)
	Public nSumins_co As Integer ' smallint   no          2           5     0     no        (n/a)               (n/a)
	Public nProduct As Integer ' smallint   no          2           5     0     no        (n/a)               (n/a)
	Public dEffecdate As Date ' datetime   no          8                       no        (n/a)               (n/a)
	Public dCompdate As Date ' datetime   no          8                       no        (n/a)               (n/a)
	Public dNulldate As Date ' datetime   no          8                       yes       (n/a)               (n/a)
	Public nUsercode As Integer ' smallint   no          2           5     0     no        (n/a)               (n/a)
	
	'**-Auxiliary Public variables declaration
	'- Declaración de variables públicas auxiliares
	Public sDescript As String
	Public sShort_des As String
	Public sCover_In As String
	Public nCover_in As Integer
	Public sOtherCover As String
	Public nOtherCover As Integer
	Public nCoverapl As Integer
	Public nOwnCapital As Integer
	Public sRoutine As String
	Public nPremiumFix As Double
	Public nPremiumCover As Double
	Public nCurrencyAmount As Double
	Public nRateCover As Double
	Public nPremiumMin As Double
	Public nPremiumMax As Double
	Public nPremiumAdd As Double
	Public nPremiumSub As Double
	Public nPremiumLev As Double
	Public nchkPremiumAdd As Integer
	Public nchkPremiumSub As Integer
	Public sSel As String
	Public sStatregt As String
	
	'- Variables auxialiares de la tabla Gen_cover - NDCB - 27/07/2001.
	
	Public sAddReini As String
	Public nBranch_led As Integer
	Public sAddSuini As String
	Public nBranch_est As Integer
	Public sAddTaxin As String
	Public sAutomrep As String
	Public nBill_item As Integer
	Public nBranch_gen As Integer
	Public nBranch_rei As Integer
	Public nCacalcov As Integer
	Public nCacalfix As Double
	Public sCacalfri As String
	Public sCacalili As String
	Public nCacalmax As Double
	Public nCacalper As Double
	Public sCacalrei As String
	Public nRateCapAdd As Double
	Public nRateCapSub As Double
	Public sCh_typ_cap As String
	Public nRatePreAdd As Double
	Public nRatePreSub As Double
	Public sChange_typ As String
	Public nCovergen As Integer
	Public nCurrency As Integer
	Public sDefaulti As String
	Public sFrancApl As String
	Public nFrancFix As Double
	Public nFrancMax As Double
	Public nFrancMin As Double
	Public nFrancrat As Double
	Public sFrantype As String
	Public nMedreser As Double
	Public nNotenum As Integer
	Public nPremifix As Double
	Public nPremimax As Double
	Public nPremimin As Double
	Public nPremirat As Double
	Public sRequire As String
	Public sRoucapit As String
	Public sRoufranc As String
	Public sRoupremi As String
	Public sRoureser As String
	Public nChCapLev As Integer
	Public nChPreLev As Integer
	Public nCacalmin As Double
	Public sFDRequire As String
	Public sFDChantyp As String
	Public nFDUserLev As Integer
	Public nFDRateAdd As Double
	Public nFDRateSub As Double
	Public nApply_Perc As Double
	Public sRou_verify As String
	
	Public nUseType As Precov_apl_UseType
	
	Public Enum Precov_apl_UseType
		Precov_aplUseRate = 1
		Precov_aplUseTarif = 2
	End Enum
	
	Public nId_table As Integer
	
	Private Structure udtBas_Sumins_Precov_apl
		Dim nUseType As Integer
		Dim nSumins_co As Integer
		Dim sDescript As String
		Dim sShort_des As String
		Dim sSel As String
	End Structure
	
	Private arrRate() As udtBas_Sumins_Precov_apl
	Private arrTarif() As udtBas_Sumins_Precov_apl
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mintModulec As Integer
	Private mintCover As Integer
	Private mintSumins_co As Integer
	Private mdtmEffecdate As Date
	
	Private mintCountRate As Short
	Private mintCountTarif As Short
	
	'**%insPreDP035: This function  validates data entered in the zone
	'**%contents for specific frame.
	'%insPreDP035: Esta función se encarga de validar los datos introducidos en la zona de
	'%contenido para "frame" especifico.
	Public Function insPreDP035(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPreDP035_err
		
		insPreDP035 = True
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			.nModulec = nModulec
			.nCover = nCover
		End With
		
		'**+ If accessing the frame for the first time
		'+ Si se entra por primera vez al FRAME
		Call insReaBas_sumins()
		Call insDisDP035()
		
insPreDP035_err: 
		If Err.Number Then
			insPreDP035 = False
		End If
		On Error GoTo 0
	End Function
	'%CountRate: propiedad que indica el número de registros de tarifa
	Public ReadOnly Property CountTarif() As Integer
		Get
			CountTarif = mintCountTarif
		End Get
	End Property
	
	'%CountTarif: propiedad que indica el número de registros de tasa
	Public ReadOnly Property CountRate() As Integer
		Get
			CountRate = mintCountRate
		End Get
	End Property
	
	'**ItemRate: Function that considering the index value loads in the class variables the array information
	'ItemRate: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemRate(ByVal lintIndex As Integer) As Boolean
		
		If lintIndex <= UBound(arrRate) Then
			With arrRate(lintIndex)
				nUseType = .nUseType
				nSumins_co = .nSumins_co
				sDescript = .sDescript
				sShort_des = .sShort_des
				sSel = .sSel
			End With
			ItemRate = True
		Else
			ItemRate = False
		End If
		
	End Function
	
	'**ItemTarif: Function that considering the index value loads in the class variables the array information
	'ItemTarif: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemTarif(ByVal lintIndex As Integer) As Boolean
		
		If lintIndex <= UBound(arrTarif) Then
			With arrTarif(lintIndex)
				nUseType = .nUseType
				nSumins_co = .nSumins_co
				sDescript = .sDescript
				sShort_des = .sShort_des
				sSel = .sSel
			End With
			ItemTarif = True
		Else
			ItemTarif = False
		End If
		
	End Function
	
	
	'**% Find: Returns the information of basic sum associated with a product
	'**% in which coverage rate applies
	'% Find: Devuelve la información de los capitales básicos asociados a un producto
	'% sobre los cuales aplica la tasa de la cobertura
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nSumins_co As Integer, ByVal dEffecdate As Date, ByVal nUseType As Precov_apl_UseType, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Static lblnRead As Boolean
		Dim lrecreaPrecov_apl As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaPrecov_apl = New eRemoteDB.Execute
		
		'**+ Parameters definition for stored procedure 'insudb.reaPrecov_apl'
		'**+ Information read on 08/01/2001 03:26:09 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaPrecov_apl'
		'+ Información leída el 01/08/2001 03:26:09 p.m.
		
		If nBranch <> mintBranch Or nProduct <> mintProduct Or nModulec <> mintModulec Or nCover <> mintCover Or nSumins_co <> mintSumins_co Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			
			mintBranch = nBranch
			mintProduct = nProduct
			mintModulec = nModulec
			mintCover = nCover
			mintSumins_co = nSumins_co
			mdtmEffecdate = dEffecdate
			
			With lrecreaPrecov_apl
				.StoredProcedure = "reaPrecov_apl"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsetype", nUseType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSumins_co", nSumins_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nModulec = .FieldToClass("nModulec")
					Me.nCover = .FieldToClass("nCover")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nSumins_co = .FieldToClass("nSumins_co")
					Me.nProduct = .FieldToClass("nProduct")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					dNulldate = .FieldToClass("dNulldate")
					nUsercode = .FieldToClass("nUsercode")
					
					.RCloseRec()
					lblnRead = True
				Else
					lblnRead = False
				End If
			End With
		End If
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaPrecov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaPrecov_apl = Nothing
	End Function
	
	'** insValDP035: Premium window validations (DP035)
	' insValDP035: Validaciones de la ventana de Primas (DP035)
	Public Function insValDP035(ByVal sCodispl As String, ByVal nInCover As Integer, ByVal sPremiumRoutine As String, ByVal nPremiumFix As Double, ByVal nRateCover As Double, ByVal nPremiumMin As Double, ByVal nPremiumMax As Double, ByVal nSelected As Integer, ByVal nOtherCover As Integer, ByVal nOwnCapital As Integer, ByVal nApply_Perc As Double, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nId_table As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim cont As Integer
		Dim lclsTab_modul As Tab_modul
		Dim lblnFind As Boolean
		
		On Error GoTo insValDp035_err
		
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lclsTab_modul = New Tab_modul
		
		lblnFind = True
		
		insValDP035 = String.Empty
		
		If nModulec > 0 Then
			Call lclsTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate)
			If lclsTab_modul.styp_rat = "1" Then
				If nApply_Perc <= 0 Then
					Call lobjErrors.ErrorMessage(sCodispl, 11311)
				End If
				lblnFind = False
			End If
		End If
		'+ Alguno de los campos "en cobertura", "rutina", "importe fijo" o "tasa"
		'+ debe estar lleno - Error 11172 08/05/2001
		If nInCover <= 0 And sPremiumRoutine = String.Empty And nPremiumFix <= 0 And nRateCover <= 0 And nId_table <= 0 And lblnFind Then
			Call lobjErrors.ErrorMessage(sCodispl, 11172)
		End If
		
		If nInCover <> 0 And nInCover <> eRemoteDB.Constants.intNull Then
			If sPremiumRoutine <> String.Empty Or nPremiumFix > 0 Or nRateCover > 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 11153)
			End If
		End If
		
		If nRateCover = eRemoteDB.Constants.intNull Then
			nRateCover = 0
		End If
		
		If nPremiumFix = eRemoteDB.Constants.intNull Then
			nPremiumFix = 0
		End If
		
		If nPremiumMin = eRemoteDB.Constants.intNull Then
			nPremiumMin = 0
		End If
		
		If nPremiumMax = eRemoteDB.Constants.intNull Then
			nPremiumMax = 0
		End If
		
		'**+ If nPremiumMax is full, must be greater than the minimum premium import - Error 11048
		'+ Si nPremiumMax está lleno, debe ser superior al importe de prima mínima - Error 11048
		If nPremiumMin <> 0 And nPremiumMax <> 0 Then
			If nPremiumMin > nPremiumMax Then
				Call lobjErrors.ErrorMessage(sCodispl, 11048)
			End If
		End If
		
		'**+ Validations on the segment fields "Rate applies on":
		'**+ 1) Only of the three shown options can have value - Error 11314
		'+ Validaciones de los campos del segmento "Tasa aplica sobre":
		'+ 1) Sólo una de las tres opciones presentadas puede tener valor - Error 11314
		cont = 0
		
		If nSelected <> 0 Then
			cont = cont + 1
		End If
		
		If nOwnCapital <> 0 And nOwnCapital <> eRemoteDB.Constants.intNull And nOwnCapital <> 2 Then
			cont = cont + 1
		End If
		
		If nOtherCover <> 0 And nOtherCover <> eRemoteDB.Constants.intNull Then
			cont = cont + 1
		End If
		
		If cont > 1 Then
			Call lobjErrors.ErrorMessage(sCodispl, 11314)
		End If
		
		'**+ 2) If the rate (permilage) is full, one of the three options
		'+ 2) Si la tasa (pormilaje) está lleno, una de las tres opciones debe estar llena - Error 11315
		If nRateCover <> 0 Then
			If cont = 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 11315)
			End If
		End If
		
		insValDP035 = lobjErrors.Confirm
		
		'**+ The instances created from used objects in this function are free from memory
		'+ Se liberan de memoria las instancias creadas de los objetos usados en esta función
insValDp035_err: 
		If Err.Number Then
			insValDP035 = "insValDP035: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lclsTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_modul = Nothing
	End Function
	
	'**%insPostDP035: Data update
	'%insPostDP035: Actualización de la data
	Public Function insPostDP035(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sActive As String, ByVal nInCover As Integer, ByVal sRoutine As String, ByVal nPremiumFix As Double, ByVal nRatePremium As Double, ByVal nOtherCover As Integer, ByVal nPremiumMin As Double, ByVal nPremiumMax As Double, ByVal nPremiumAdd As Double, ByVal nPremiumSub As Double, ByVal nPremiumLevel As Integer, ByVal sPremiumAdd As String, ByVal sPremiumSub As String, ByVal nApply_Perc As Double, ByVal sRou_verify As String, ByVal nUsercode As Integer, ByVal nOwnCapital As Integer, ByVal nId_table As Integer, Optional ByVal sSelected As String = "", Optional ByVal sSumins_co As String = "", Optional ByVal sTarifSel As String = "", Optional ByVal sTarifSumins_co As String = "") As Boolean
		Dim lstrSelected As String
		Dim lstrSumins_co As String
		Dim larrSel As Object
		Dim larrSumins As Object
		Dim lvarSel As Object
		Dim lintCount As Short
		
		On Error GoTo insPostDP035_err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.dEffecdate = dEffecdate
			.nCover_in = nInCover
			.sRoutine = sRoutine
			.nPremiumFix = nPremiumFix
			.nRateCover = nRatePremium
			.nCoverapl = nOtherCover
			.nPremiumMin = nPremiumMin
			.nPremiumMax = nPremiumMax
			.nPremiumAdd = nPremiumAdd
			.nPremiumSub = nPremiumSub
			.nPremiumLev = nPremiumLevel
			.nchkPremiumAdd = IIf(sPremiumAdd = "1", 1, 2)
			.nchkPremiumSub = IIf(sPremiumSub = "1", 1, 2)
			.nApply_Perc = nApply_Perc
			.sRou_verify = sRou_verify
			.nUsercode = nUsercode
			.sStatregt = IIf(Trim(sActive) <> String.Empty, sActive, "2")
			.nId_table = nId_table
		End With
		
		insPostDP035 = insGen_Cover()
		
		If insPostDP035 Then
			larrSel = Microsoft.VisualBasic.Split(sSelected, ", ")
			larrSumins = Microsoft.VisualBasic.Split(sSumins_co, ", ")
			
			Me.nUseType = 1
            lintCount = 0
            If larrSel.length > 1 Then
                For Each lvarSel In larrSel
                    Me.sSel = lvarSel
                    Me.nSumins_co = larrSumins(lintCount)

                    insPostDP035 = insUpdPrecov_apl()

                    lintCount = lintCount + 1
                Next lvarSel
            End If

			
			larrSel = Microsoft.VisualBasic.Split(sTarifSel, ", ")
			larrSumins = Microsoft.VisualBasic.Split(sTarifSumins_co, ", ")
			
			Me.nUseType = 2
            lintCount = 0
            If larrSel.length > 1 Then
                For Each lvarSel In larrSel
                    Me.sSel = lvarSel
                    Me.nSumins_co = larrSumins(lintCount)

                    insPostDP035 = insUpdPrecov_apl()

                    lintCount = lintCount + 1
                Next lvarSel
            End If
        End If

insPostDP035_err:
        If Err.Number Then
            insPostDP035 = False
        End If
        On Error GoTo 0
	End Function
	
	'**%insGen_cover: In this routine the value assignment of the active frame
	'**% is made for the corresponding parameters of the store-procedure that
	'**% performs the history maintenance in the structure 'Gen_cover'.
	'%insGen_cover: En esta rutina se realiza la asignación de los valores del
	'%frame activo a los parametros correspondientes del store-procedure que
	'%realiza el mantenimiento de la historia en la estructura 'Gen_cover'.
	Private Function insGen_Cover() As Boolean
        Dim lstrChangeType As String = ""
        Dim lclsGenCover As eProduct.Gen_cover
		
		On Error GoTo insGen_Cover_err
		
		lclsGenCover = New eProduct.Gen_cover
		
		With lclsGenCover
			If .Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
				.nBranch = nBranch
				.nProduct = Me.nProduct
				.nModulec = Me.nModulec
				.nCover = Me.nCover
				.dEffecdate = Me.dEffecdate
				.sStatregt = Me.sStatregt
				.nUsercode = Me.nUsercode
				.nCover_in = Me.nCover_in
				.sRoupremi = Me.sRoutine
				.nPremifix = Me.nPremiumFix
				.nPremirat = Me.nRateCover
				.nCoverapl = Me.nCoverapl
				.nPremimin = Me.nPremiumMin
				.nPremimax = Me.nPremiumMax
				.nId_table = Me.nId_table
				If Me.nchkPremiumAdd = 2 Then
					Me.nchkPremiumAdd = 0
				End If
				If Me.nchkPremiumSub = 2 Then
					Me.nchkPremiumSub = 0
				End If
				If (Me.nchkPremiumAdd = 0 Or Me.nchkPremiumAdd = eRemoteDB.Constants.intNull) And (Me.nchkPremiumSub = 0 Or Me.nchkPremiumSub = eRemoteDB.Constants.intNull) Then
					lstrChangeType = "1"
				End If
				If Me.nchkPremiumAdd = 1 And (Me.nchkPremiumSub = 0 Or Me.nchkPremiumSub = eRemoteDB.Constants.intNull) Then
					lstrChangeType = "2"
				End If
				If (Me.nchkPremiumAdd = 0 Or Me.nchkPremiumAdd = eRemoteDB.Constants.intNull) And Me.nchkPremiumSub = 1 Then
					lstrChangeType = "3"
				End If
				If Me.nchkPremiumAdd = 1 And Me.nchkPremiumSub = 1 Then
					lstrChangeType = "4"
				End If
				.sChange_typ = lstrChangeType
				.nRatePreAdd = Me.nPremiumAdd
				.nRatePreSub = Me.nPremiumSub
				.nChPreLev = Me.nPremiumLev
				.nApply_Perc = Me.nApply_Perc
				.sRou_verify = Me.sRou_verify
				
				insGen_Cover = .Update
			End If
		End With
		
insGen_Cover_err: 
		If Err.Number Then
			insGen_Cover = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsGenCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGenCover = Nothing
	End Function
	
	'**%insUpdPrecov_apl. This function updates the values in the table Precov_apl
	'%insUpdPrecov_apl. Esta funcion se encarga de actualizar los valores en la tabla Precov_apl
	Private Function insUpdPrecov_apl() As Boolean
		Dim lprmPrecov_apl As eRemoteDB.Execute
		
		On Error GoTo insUpdPrecov_apl_err
		
		lprmPrecov_apl = New eRemoteDB.Execute
		
		insUpdPrecov_apl = True
		With lprmPrecov_apl
			.StoredProcedure = "insPrecov_apl"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsetype", nUseType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_co", nSumins_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Process", IIf(sSel = "1", "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		insUpdPrecov_apl = lprmPrecov_apl.Run(False)
		
insUpdPrecov_apl_err: 
		If Err.Number Then
			insUpdPrecov_apl = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lprmPrecov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lprmPrecov_apl = Nothing
	End Function
	
	'**%insReaGen_coverVal. This function reads the coverage table and verifies that
	'**% coverage in treatment has some method to calculate the premium
	'%insReaGen_coverVal. Esta funcion se encarga de leer la tabla de coberturas y verificar
	'%que la cobertura en tratamiento tenga algun metodo de calculo de prima.
	Private Function insReaGen_coverVal() As Object
		Dim lrecGen_cover As New eRemoteDB.Execute
		
		On Error GoTo insReaGen_coverVal_err
		
		'**+Makes the reading of table Gen_cover
		'+Se realiza la lectura de la tabla Gen_cover
		lrecGen_cover.StoredProcedure = "reaGen_cover_3"
		'**+Assigns the code of the coverage to be read
		'+Se asigna el codigo de la cobertura a leer
		lrecGen_cover.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		lrecGen_cover.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		lrecGen_cover.Parameters.Add("nModulec", Me.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		lrecGen_cover.Parameters.Add("nCover", Me.nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		lrecGen_cover.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		'**+If not found, returns the value to false
		'+Si no se encuentra se retorna el valor de falso
		
		If Not lrecGen_cover.Run Then
			insReaGen_coverVal = False
		Else
			'**+ If coverage has not associated calculus method returns value to false, otherwise
			'**+ returns to true
			'+Si la cobertura no tiene metodo de calculo asociado se retorna el valor de falso, en caso contrario
			'+se retorna verdadero
			If (lrecGen_cover.FieldToClass("sRoupremi") = String.Empty) And (lrecGen_cover.FieldToClass("nPremifix") = eRemoteDB.Constants.intNull) And (lrecGen_cover.FieldToClass("nPremirat") = eRemoteDB.Constants.intNull) Then
				insReaGen_coverVal = False
			Else
				insReaGen_coverVal = True
			End If
		End If
		
		'UPGRADE_NOTE: Object lrecGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGen_cover = Nothing
		
insReaGen_coverVal_err: 
		If Err.Number Then
			insReaGen_coverVal = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insReaBas_sumins:   This function makes the reading of the table Relations
	'%insReaBas_sumins: Esta funcion se encarga de realizar la lectura de la tabla Relations
	Private Function insReaBas_sumins() As Boolean
		Const ARR_BLOCK As Short = 50
		Dim lrecreaBas_sumins_precov_apl As eRemoteDB.Execute
		Dim llngUseType As Short
		Dim lblnOwnCapital As Boolean
		Dim llngSumins_Co As Integer
		Dim llngSumins_Co_Pre As Integer
		
		On Error GoTo reaBas_sumins_precov_apl_Err
		
		lblnOwnCapital = True
		mintCountRate = 0
		mintCountTarif = 0
		
		lrecreaBas_sumins_precov_apl = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure reaBas_sumins_precov_apl al 04-01-2003 17:04:01
		'+
		With lrecreaBas_sumins_precov_apl
			.StoredProcedure = "reaBas_sumins_precov_apl"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsetype", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insReaBas_sumins = True
				
				'+Se cargan datos de tasa
				ReDim arrRate(ARR_BLOCK)
				ReDim arrTarif(ARR_BLOCK)
				
				llngSumins_Co_Pre = 0
				Do While Not .EOF
					llngUseType = .FieldToClass("nUsetype")
					llngSumins_Co = .FieldToClass("nSumins_co")
					
					If llngSumins_Co <> llngSumins_Co_Pre Then
						llngSumins_Co_Pre = llngSumins_Co
						mintCountRate = mintCountRate + 1
						
						If mintCountRate Mod ARR_BLOCK = 0 Then
							ReDim Preserve arrRate(mintCountRate + ARR_BLOCK)
						End If
						
						arrRate(mintCountRate).nUseType = 1
						arrRate(mintCountRate).nSumins_co = llngSumins_Co
						arrRate(mintCountRate).sDescript = .FieldToClass("sDescript")
						arrRate(mintCountRate).sShort_des = .FieldToClass("sShort_des")
						arrRate(mintCountRate).sSel = "2"
						
						mintCountTarif = mintCountTarif + 1
						If mintCountTarif Mod ARR_BLOCK = 0 Then
							ReDim Preserve arrTarif(mintCountTarif + ARR_BLOCK)
						End If
						
						arrTarif(mintCountTarif).nUseType = 2
						arrTarif(mintCountTarif).nSumins_co = llngSumins_Co
						arrTarif(mintCountTarif).sDescript = .FieldToClass("sDescript")
						arrTarif(mintCountTarif).sShort_des = .FieldToClass("sShort_des")
						arrTarif(mintCountTarif).sSel = "2"
						
					End If
					
					If llngUseType = 1 Then
						arrRate(mintCountRate).sSel = "1"
						lblnOwnCapital = False
					ElseIf llngUseType = 2 Then 
						arrTarif(mintCountTarif).sSel = "1"
					End If
					
					.RNext()
				Loop 
				
				ReDim Preserve arrRate(mintCountRate)
				ReDim Preserve arrTarif(mintCountTarif)
				
			Else
				insReaBas_sumins = False
			End If
		End With
		
		Me.nOwnCapital = IIf(lblnOwnCapital, 1, 2)
		
reaBas_sumins_precov_apl_Err: 
		If Err.Number Then
			insReaBas_sumins = False
		End If
		'UPGRADE_NOTE: Object lrecreaBas_sumins_precov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBas_sumins_precov_apl = Nothing
		On Error GoTo 0
	End Function
	'**%insDisDP035. This Routine displays the corresponding values
	'**% to the conditions of premium calculus
	'%insDisDP035. Esta Rutina se encarga de desplegar los valores correspondientes
	'%a las condiciones de calculo de prima
	Private Function insDisDP035() As Boolean
		
		'**-Defines the recordset to read the coverage description
		'-Se define el recordset para realizar la lectura de la descripcion de la cobertura
		Dim lrecDescript As New eRemoteDB.Execute
		Dim lcurAmount As Decimal
		Dim mrecgen_cover_2 As New eRemoteDB.Execute
		Dim lrecTab_genCov As New eRemoteDB.Execute
		Dim lobjValues As New eFunctions.Values
		Dim lrecreaGen_cover_3 As eRemoteDB.Execute
		
		On Error GoTo insDisDp035_err
		
		lrecreaGen_cover_3 = New eRemoteDB.Execute
		
		'**+ Parameters definition for stored procedure 'insudb.reaGen_cover_3'
		'+ Definición de parámetros para stored procedure 'insudb.reaGen_cover_3'
		
		With lrecreaGen_cover_3
			.StoredProcedure = "reaGen_cover_3"
			.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", Me.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", Me.nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insDisDP035 = True
				If Not .EOF Then
					If .FieldToClass("nCover_in") <> 0 And .FieldToClass("nCover_In") <> eRemoteDB.Constants.intNull Then
						lrecDescript.StoredProcedure = "reaGen_coverCoverGen2"
						lrecDescript.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("nCover", lobjValues.StringToType(.FieldToClass("nCover_in"), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("nModulec", Me.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If lrecDescript.Run Then
							sCover_In = .FieldToClass("sDescript")
							nCover_in = .FieldToClass("nCover_in")
							lrecDescript.RCloseRec()
						Else
							sCover_In = String.Empty
							nCover_in = eRemoteDB.Constants.intNull
						End If
					Else
						sCover_In = String.Empty
						nCover_in = eRemoteDB.Constants.intNull
					End If
					sRoutine = .FieldToClass("sRoupremi")
					nCurrencyAmount = lobjValues.StringToType(.FieldToClass("nPremifix"), eFunctions.Values.eTypeData.etdDouble)
					nPremiumFix = lobjValues.StringToType(.FieldToClass("nPremifix"), eFunctions.Values.eTypeData.etdDouble)
					nRateCover = lobjValues.StringToType(.FieldToClass("nPremirat"), eFunctions.Values.eTypeData.etdDouble)
					nApply_Perc = lobjValues.StringToType(.FieldToClass("nApply_Perc"), eFunctions.Values.eTypeData.etdDouble)
					sRou_verify = .FieldToClass("sRou_verify")
					nId_table = .FieldToClass("nId_table")
					
					If (nRateCover <> eRemoteDB.Constants.intNull And nRateCover <> 0) Or sRoutine <> String.Empty Then
						If nOwnCapital <> 2 Then
							nOwnCapital = 1
						End If
					End If
					
					If .FieldToClass("nCoverapl") <> 0 And .FieldToClass("nCoverapl") <> eRemoteDB.Constants.intNull Then
						lrecDescript.StoredProcedure = "reaGen_coverCoverGen"
						lrecDescript.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("nCovergen", lobjValues.StringToType(.FieldToClass("nCoverapl"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						lrecDescript.Parameters.Add("nModulec", Me.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If lrecDescript.Run Then
							sOtherCover = lrecDescript.FieldToClass("sDescript")
							nOtherCover = .FieldToClass("nCoverapl")
							nCoverapl = .FieldToClass("nCoverapl")
							nOwnCapital = 2
						End If
					Else
						sOtherCover = String.Empty
					End If
					
					nPremiumMin = lobjValues.StringToType(.FieldToClass("nPremimin"), eFunctions.Values.eTypeData.etdDouble)
					nPremiumMax = lobjValues.StringToType(.FieldToClass("nPremimax"), eFunctions.Values.eTypeData.etdDouble)
					nPremiumAdd = lobjValues.StringToType(.FieldToClass("nRatePreAdd"), eFunctions.Values.eTypeData.etdDouble)
					nPremiumSub = lobjValues.StringToType(.FieldToClass("nRatePreSub"), eFunctions.Values.eTypeData.etdDouble)
					nPremiumLev = lobjValues.StringToType(.FieldToClass("nChPreLev"), eFunctions.Values.eTypeData.etdLong)
				End If
				
				
				Select Case .FieldToClass("sChange_typ")
					Case "1"
						nchkPremiumAdd = 0
						nchkPremiumSub = 0
					Case "2"
						nchkPremiumAdd = 1
						nchkPremiumSub = 0
					Case "3"
						nchkPremiumAdd = 0
						nchkPremiumSub = 1
					Case "4"
						nchkPremiumAdd = 1
						nchkPremiumSub = 1
				End Select
				.RCloseRec()
			Else
				insDisDP035 = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaGen_cover_3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover_3 = Nothing
		'UPGRADE_NOTE: Object lrecDescript may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDescript = Nothing
		'UPGRADE_NOTE: Object mrecgen_cover_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mrecgen_cover_2 = Nothing
		'UPGRADE_NOTE: Object lrecTab_genCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_genCov = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lrecreaGen_cover_3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover_3 = Nothing
		
insDisDp035_err: 
		If Err.Number Then
			insDisDP035 = False
		End If
		On Error GoTo 0
	End Function
End Class






