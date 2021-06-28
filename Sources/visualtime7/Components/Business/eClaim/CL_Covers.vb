Option Strict Off
Option Explicit On
Public Class CL_Covers
	Implements System.Collections.IEnumerable
	'**% local variable to hold collection
	'% variable local para mantener la coleccion
	'%-------------------------------------------------------%'
	'% $Workfile:: CL_Covers.cls                            $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 25-03-13 8:36                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	Private mCol As Collection
	
	'% Add:
	Public Function Add(ByRef objClass As Cl_Cover) As Cl_Cover
		
		If objClass Is Nothing Then
			objClass = New Cl_Cover
		End If
		
		With objClass
			mCol.Add(objClass, "CLC" & .nClaim & .nCase_num & .nDeman_type & .nModulec & .nCover & .nCurrency & .sClient)
			
		End With
		
		'retorna el elemento creado
		Add = objClass
		
Add_err: 
		If Err.Number Then
			Add = Nothing
		End If
		On Error GoTo 0
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cl_Cover
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	Private Sub Class_Terminate_Renamed()
		'**%Class_Terminate: Controls the destruction of an instance of the collection
		'%Class_Terminate: Controla la destrucción de una instancia de la colección
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**%Find: This method fills the collection with records from the table "SI007" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "SI007" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros
	'% Find_SI007: se cargan los datos de la ventana SI007: Reservas del siniestro
	Public Function Find_SI007(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal sbrancht As String, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nOpt_claityp As Integer) As Boolean
		Dim lrecinsReaCover_a As eRemoteDB.Execute
		Dim lclsLife_Claim As Life_claim
		Dim lclsCl_cover As Cl_Cover
		
		Dim lblnIncapacity As Boolean
		
		On Error GoTo Find_SI007_Err
		
		Find_SI007 = False
		
		'+ Si el tipo de siniestro es "Incapacidad" o el tipo de indemnización es
		'+ "Liberación de pago de prima" se coloca pre-seleccionada la cobertura
		'+ de tipo exención (Sólo para vida)
		lblnIncapacity = False
		If sbrancht = CStr(eProduct.Product.pmBrancht.pmlife) Then
			lclsLife_Claim = New Life_claim
			With lclsLife_Claim
				If .Find(nClaim, nCase_num, nDeman_type) Then
					If (.nIn_lif_typ = Life_claim.enmClaim.eIncapacity Or .nIn_lif_typ = Life_claim.enmIndem.eLiber_pay_prem) Then
						lblnIncapacity = True
					End If
				End If
			End With
		End If
		
		
		lrecinsReaCover_a = New eRemoteDB.Execute
		
		'**+ Parameters definition for stored procedure 'insudb.insReaCover_a'
		'+ Definición de parámetros para stored procedure 'insudb.insReaCover_a'
		'**+ Data read on 17/01/2001 4:20:42 PM
		'+ Información leída el 17/01/2001 4:20:42 PM
		
		With lrecinsReaCover_a
			.StoredProcedure = "insReaCover_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sbrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOpt_claityp", nOpt_claityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsCl_cover = New Cl_Cover
					With lclsCl_cover
						'+ Se asigna valor a las variables de la clase
						.nClaim = nClaim
						.nCase_num = nCase_num
						.nDeman_type = nDeman_type
						.nModulec = lrecinsReaCover_a.FieldToClass("nModulec")
						.nCover = lrecinsReaCover_a.FieldToClass("nCover")
						.nCurrency = lrecinsReaCover_a.FieldToClass("nCurrency")
						.sDescover = lrecinsReaCover_a.FieldToClass("sDesCover")
						.sClient = lrecinsReaCover_a.FieldToClass("sClient")
						.sCliename = lrecinsReaCover_a.FieldToClass("sCliename")
						.sDigit = lrecinsReaCover_a.FieldToClass("sDigit")
						.sBill_ind = lrecinsReaCover_a.FieldToClass("sBill_ind")
						.nExchange = lrecinsReaCover_a.FieldToClass("nExchange")
						.nGroup = lrecinsReaCover_a.FieldToClass("nGroup")
						.sReservstat = IIf(lrecinsReaCover_a.FieldToClass("sReservstat") = String.Empty, "1", lrecinsReaCover_a.FieldToClass("sReservstat"))
						.nDamages = lrecinsReaCover_a.FieldToClass("nDamages")
						.nFra_amount = lrecinsReaCover_a.FieldToClass("nFra_amount")
						.nReserve = lrecinsReaCover_a.FieldToClass("nReserve")
						.nDamprof = lrecinsReaCover_a.FieldToClass("nDamProf")
						.nCapital = lrecinsReaCover_a.FieldToClass("nCapital")
						.sAutomrep = lrecinsReaCover_a.FieldToClass("sAutomRep")
						.nFixamount = lrecinsReaCover_a.FieldToClass("nFixAmount")
						.nMaxamount = lrecinsReaCover_a.FieldToClass("nMaxAmount")
						.nMinamount = lrecinsReaCover_a.FieldToClass("nMinAmount")
						.nRate = lrecinsReaCover_a.FieldToClass("nRate")
						.nPay_amount = lrecinsReaCover_a.FieldToClass("nPay_amount")
						.nBranch_est = lrecinsReaCover_a.FieldToClass("nBranch_est")
						.nBranch_rei = lrecinsReaCover_a.FieldToClass("nBranch_rei")
						.nBranch_led = lrecinsReaCover_a.FieldToClass("nBranch_led")
                        '+ Para SOAP se permite crear coberturas en cero
                        If sbrancht = CStr(eProduct.Product.pmBrancht.pmSegurosProvisionales) Then
                            .nSel = IIf(lrecinsReaCover_a.FieldToClass("NDAMAGES_CL_COVER") < 0, 0, 1) 'IIf(.nDamages = 0, 0, 1)
                        Else
						.nSel = IIf(lrecinsReaCover_a.FieldToClass("NDAMAGES_CL_COVER") <= 0, 0, 1) 'IIf(.nDamages = 0, 0, 1)
                        End If
						.sRoureser = lrecinsReaCover_a.FieldToClass("sRoureser")
						.nMedreser = lrecinsReaCover_a.FieldToClass("nMedreser")
						.sInsurini = lrecinsReaCover_a.FieldToClass("sInsurini")
						.sFrantype = lrecinsReaCover_a.FieldToClass("sFrantype")
						.nLoc_pay_am = lrecinsReaCover_a.FieldToClass("nLoc_pay_am")
						.sCacalili = lrecinsReaCover_a.FieldToClass("sCacalili")
						.sCaren_type = lrecinsReaCover_a.FieldToClass("sCaren_type")
						.nCaren_quan = lrecinsReaCover_a.FieldToClass("nCaren_quan")
						.dCoverDate = lrecinsReaCover_a.FieldToClass("DateCover")
						.nPayconre = eRemoteDB.Constants.intNull
						.nGroup_insu = eRemoteDB.Constants.intNull
						.sCldeathi = lrecinsReaCover_a.FieldToClass("sCldeathi")
						.nAmount = lrecinsReaCover_a.FieldToClass("nAmount")
                        .nAmountUsed = lrecinsReaCover_a.FieldToClass("NAMOUN_USED")

                        .nFrancdays = lrecinsReaCover_a.FieldToClass("nFrancdays")
                        .nDaydedamount = lrecinsReaCover_a.FieldToClass("nDaydedamount")
						'**+ In case it doesn't have the estimadted value of "Damage Estimate"
						'+ En caso de que no tenga valor el campo de "Estimado de daños".
						If .nDamages <= 0 Then
							'**+ If it posses medium cost, this value will be assigned by default to "Damage Estimate" field
							'+ Si posee costo medio, se asigna éste valor por defecto al campo "estimado de daños".
							If IIf(.nMedreser = eRemoteDB.Constants.intNull, 0, .nMedreser) <> 0 Then
								.nDamages = IIf(.nMedreser = eRemoteDB.Constants.intNull, 0, .nMedreser)
								.nSel = 1
							End If
						End If
						
						'                    If .nReserve <= .nFra_amount Then
						'                        If .nReserve > 0 Then
						'                            .nReserve = .nReserve - .nFra_amount
						'                        Else
						'                            .nReserve = 0
						'                        End If
						'                    End If
						.nReserve_o = .nReserve
						
						'**+ The original franchise is saved to verify if the user is the one modifying it.
						'+ Se guarda la franquicia original para verificar si el usuario la modifica
						.nFrandeda = .nFra_amount 'col 10
						.sAutomrep = IIf(.sAutomrep = String.Empty, "2", .sAutomrep)
						.sFran_Ind = "1"
						.nExchange_o = .nExchange
						
						'+ Si el tipo de siniestro es "Incapacidad" o el tipo de indemnización es
						'+ "Liberación de pago de prima" se coloca pre-seleccionada la cobertura
						'+ de tipo exención (Sólo para vida)
						If lblnIncapacity And lclsCl_cover.sInsurini = "3" Then
							lclsCl_cover.nSel = 1
						End If
						
						.sDesStatusCov = lrecinsReaCover_a.FieldToClass("sDesreservstat")
						.sDesFrantype = lrecinsReaCover_a.FieldToClass("sFrantypedesc")
						.sDesCurrency = lrecinsReaCover_a.FieldToClass("sCurrdes")
						
						'+aplica sobre capital o siniestro
						.sFrancapl = lrecinsReaCover_a.FieldToClass("sFrancapl")
						
						Call Add(lclsCl_cover)
						
					End With
					.RNext()
					lclsCl_cover = Nothing
				Loop 
				Find_SI007 = True
				.RCloseRec()
			End If
		End With
		
		lrecinsReaCover_a = Nothing
		lclsLife_Claim = Nothing
		lclsCl_cover = Nothing
		
Find_SI007_Err: 
		If Err.Number Then
			Find_SI007 = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function Find_ClaimReserve(ByVal nClaim As Double) As Boolean
		Dim lrecReaClaimReserve As eRemoteDB.Execute
		Dim lclsCl_cover As Cl_Cover
		
		On Error GoTo Find_ClaimReserve_Err
		
		lrecReaClaimReserve = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.reaClaimReserve'
		'Definición de parámetros para stored procedure 'insudb.reaClaimReserve'
		'**+ Data read on 22/01/2001 11.18.41
		'Información leída el 22/01/2001 11.18.41
		
		With lrecReaClaimReserve
			.StoredProcedure = "reaClaimReserve"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsCl_cover = New Cl_Cover
					With lclsCl_cover
						.nClaim = nClaim
						.nCase_num = 1
						.nDeman_type = 1
						.nCurrency = lrecReaClaimReserve.FieldToClass("nCurrency")
						.nModulec = 1
						.nGroup = 0
						.sReservstat = lrecReaClaimReserve.FieldToClass("sReservstat")
						.sBill_ind = "2"
					End With
					Call Add(lclsCl_cover)
					.RNext()
				Loop 
				Find_ClaimReserve = True
				.RCloseRec()
			Else
				Find_ClaimReserve = False
			End If
		End With
		lrecReaClaimReserve = Nothing
Find_ClaimReserve_Err: 
		If Err.Number Then
			Find_ClaimReserve = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function ChargeOtherCovers(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecinsReaCl_cover_1 As eRemoteDB.Execute
		Dim lclsCl_cover As Cl_Cover
		
		On Error GoTo ChargeOtherCovers_Err
		ChargeOtherCovers = False
		lrecinsReaCl_cover_1 = New eRemoteDB.Execute
		
		
		'**+ Parameters definition for the stored procedure 'insudb.insReaCl_cover_1'
		'Definición de parámetros para stored procedure 'insudb.insReaCl_cover_1'
		'**+ Data read on 22/01/2001 11.18.41
		'Información leída el 22/01/2001 11.18.41
		
		With lrecinsReaCl_cover_1
			.StoredProcedure = "insReaCl_cover_1"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					lclsCl_cover = New Cl_Cover
					With lclsCl_cover
						.nClaim = nClaim
						.nCase_num = nCase_num
						.nDeman_type = nDeman_type
						.sClient = lrecinsReaCl_cover_1.FieldToClass("sClient")
						.nCurrency = lrecinsReaCl_cover_1.FieldToClass("nCurrency")
						.nModulec = lrecinsReaCl_cover_1.FieldToClass("nModulec")
						.nCover = lrecinsReaCl_cover_1.FieldToClass("nCover")
						.nPayconre = lrecinsReaCl_cover_1.FieldToClass("nPayconre")
						.sCaren_type = lrecinsReaCl_cover_1.FieldToClass("sCaren_type")
						.nCaren_quan = lrecinsReaCl_cover_1.FieldToClass("nCaren_quan")
						.nGroup_insu = lrecinsReaCl_cover_1.FieldToClass("nGroup_insu")
						.sBill_ind = "2"
					End With
					Call Add(lclsCl_cover)
					.RNext()
				Loop 
				ChargeOtherCovers = True
				.RCloseRec()
			End If
		End With
		lrecinsReaCl_cover_1 = Nothing
ChargeOtherCovers_Err: 
		If Err.Number Then
			ChargeOtherCovers = False
		End If
		On Error GoTo 0
	End Function


    '**%Find: This method fills the collection with records from the table "SI007" returning TRUE or FALSE
    '**%depending on the existence of the records
    '%Find: Este metodo carga la coleccion de elementos de la tabla "SI007" devolviendo Verdadero o
    '%falso, dependiendo de la existencia de los registros
    '% Find_SI007_GM: Indica si tiene gastos medicos o no
    Public Function Find_SI007_GM(ByVal nClaim As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        'ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date,                                 ByVal sbrancht As String,
        'ByVal nDeman_type As Integer, ByVal nOpt_claityp As Integer) As Boolean
        Dim lrecinsReaCover_a As eRemoteDB.Execute
        Dim lclsLife_Claim As Life_claim
        Dim lclsCl_cover As Cl_Cover

        Dim lblnIncapacity As Boolean

        On Error GoTo Find_SI007_GM_Err

        Find_SI007_GM = False
        lrecinsReaCover_a = New eRemoteDB.Execute

        With lrecinsReaCover_a
            .StoredProcedure = "INSREAGASTOSMEDICOS"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run Then
                Do While Not .EOF
                    lclsCl_cover = New Cl_Cover
                    With lclsCl_cover
                        .nCover = lrecinsReaCover_a.FieldToClass("nCover")
                        If .nCover = 1004 Then
                            Find_SI007_GM = True
                        End If
                    End With
                    .RNext()
                Loop

            End If
        End With

        lrecinsReaCover_a = Nothing
        lclsLife_Claim = Nothing
        lclsCl_cover = Nothing

Find_SI007_GM_Err:
        If Err.Number Then
            Find_SI007_GM = False
        End If
        On Error GoTo 0
    End Function


   

End Class






