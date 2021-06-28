Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Surr_originss_NET.Surr_originss")> Public Class Surr_originss
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class: Surr_originss
	'**+Version: $$Revision: 16 $
	'+Objetivo: Colección que le da soporte a la clase: Surr_originss
	'+Version: $$Revision: 16 $
	
	'**-Objective:
	'-Objetivo:
	Private mCol As Collection
	Public nParticip As Double
	Public sActivFound As Double
	
	Public dEffecdate As Date
	Public nExists As Short
	
	Public bIsCancelling As Boolean
	
	'**%Objective: Adds the fields to the collection of nominal values
	'%Objetivo: Agrega los campos a la colección de valores nominales
    Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                        ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, _
                        ByVal nOrigin_apv As Integer, ByVal nAvailable As Double, ByVal nAmount As Double, _
                        ByVal nCost_amo As Double, ByVal nRet_amo As Double, ByVal sSel_origin As String, _
                        ByVal nWDCost As Double, ByVal nRequestedamount As Double, ByVal nGrossAmount As Double, _
                        ByVal nCost_cov As Double, ByVal nLoans As Double, ByVal nIntLoans As Double, _
                        Optional ByVal dPaymentdate As Date = #12:00:00 AM#, Optional ByVal nLocal_amount As Integer = 0, _
                        Optional ByVal nExchange As Double = 0, Optional ByVal nCost_cov_dev As Double = 0, Optional ByVal nRentability As Double = 0, _
                        Optional ByVal nAmount_rec_dev As Double = 0, Optional ByVal nAmount_dev As Double = 0) As Surr_origins

        Dim objNewMember As Surr_origins

        On Error GoTo Add_err

        objNewMember = New Surr_origins

        If mCol Is Nothing Then
            mCol = New Collection
        End If
        '**+ Establishes the properties that transfers to the method
        '+ Se establecen las propiedades que se transfieren al método
        With objNewMember
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nOrigin_apv = nOrigin_apv
            .nAvailable = nAvailable
            .nAmount = nAmount
            .nCost_amo = nCost_amo
            .nRet_amo = nRet_amo
            .sSel_origin = sSel_origin
            .nWDCost = nWDCost
            .nRequestedamount = nRequestedamount
            .nGrossAmount = nGrossAmount
            .nCost_cov = nCost_cov
            .nLoans = nLoans
            .nIntLoans = nIntLoans
            .dPaymentdate = dPaymentdate
            .nLocal_amount = nLocal_amount
            .nExchange = nExchange
            .nCost_cov_dev = nCost_cov_dev
            .nRentability = nRentability
            .nAmount_rec_dev = nAmount_rec_dev
            .nAmount_dev = nAmount_dev

        End With
        mCol.Add(objNewMember)
        Add = objNewMember

Add_err:
        On Error GoTo 0
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
        '    Set mCol = Nothing
        'UPGRADE_NOTE: Object Add may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Add = Nothing
    End Function
	
	'**%Objective: Adds the fields to the collection of nominal values
	'%Objetivo: Agrega los campos a la colección de valores nominales para APV
    Public Function Add_Apv(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, _
                            ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, _
                            ByVal nOrigin_apv As Integer, ByVal nAvailable As Double, ByVal nAmount As Double, _
                            ByVal nCost_amo As Double, ByVal nRet_amo As Double, ByVal sSel_origin As String, _
                            ByVal nWDCost As Double, ByVal nRequestedamount As Double, ByVal nGrossAmount As Double, _
                            ByVal nTyp_Profitworker As Integer, Optional ByVal dPaymentdate As Date = #12:00:00 AM#, _
                            Optional ByVal nLocal_amount As Integer = 0, Optional ByVal nExchange As Double = 0, _
                            Optional ByVal nCost_cov_dev As Double = 0, Optional ByVal nRentability As Double = 0, _
                            Optional ByVal nAmount_rec_dev As Double = 0, Optional ByVal nAmount_dev As Double = 0) As Surr_origins

        Dim objNewMember As Surr_origins

        On Error GoTo Add_err

        objNewMember = New Surr_origins

        If mCol Is Nothing Then
            mCol = New Collection
        End If
        '**+ Establishes the properties that transfers to the method
        '+ Se establecen las propiedades que se transfieren al método
        With objNewMember
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nOrigin_apv = nOrigin_apv
            .nAvailable = nAvailable
            .nAmount = nAmount
            .nCost_amo = nCost_amo
            .nRet_amo = nRet_amo
            .sSel_origin = sSel_origin
            .nWDCost = nWDCost
            .nRequestedamount = nRequestedamount
            .nGrossAmount = nGrossAmount
            .nTyp_Profitworker = nTyp_Profitworker
            .dPaymentdate = dPaymentdate
            .nLocal_amount = nLocal_amount
            .nExchange = nExchange
            .nCost_cov_dev = nCost_cov_dev
            .nRentability = nRentability
            .nAmount_rec_dev = nAmount_rec_dev
            .nAmount_dev = nAmount_dev

        End With
        mCol.Add(objNewMember)
        Add_Apv = objNewMember

Add_err:
        On Error GoTo 0
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
        '    Set mCol = Nothing
        'UPGRADE_NOTE: Object Add_Apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Add_Apv = Nothing
    End Function
	
	'%Objetivo: Lee todos las cuentas origen asociadas al rescate de póliza
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaSurr_origins As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaSurr_origins = New eRemoteDB.Execute
		
		Find = True
		
		With lrecreaSurr_origins
			.StoredProcedure = "reaSurr_origins"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				Do While Not .EOF
                    Call Add(.FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), _
                             .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("dEffecdate"), _
                             .FieldToClass("nOrigin_APV"), .FieldToClass("nAvailable"), .FieldToClass("nAmount"), _
                             .FieldToClass("nCost_Amo"), .FieldToClass("nRet_Amo"), "1", .FieldToClass("nWDCost"), _
                             .FieldToClass("nRequestedAmount"), eRemoteDB.Constants.intNull, (.Parameters("nCost_cov").Value), _
                             (.Parameters("nLoans").Value), (.Parameters("nIntloans").Value), , , , (.Parameters("nCost_cov_dev").Value), (.Parameters("nRentability").Value), (.Parameters("nAmount_rec_dev").Value), (.Parameters("nAmount_dev").Value))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSurr_origins = Nothing
	End Function
	
	
	'% InsPreVI7000 : Permite predefinir los valores utilizados en el Grid de la VI7000
    Public Function InsPreVI7000_Origins(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nProponum As Double, ByVal nSurr_reason As Double, ByVal sSurrTotal As String, ByVal nUsercode As Double, ByVal sProcessType As String, ByVal nDelete As Integer, ByVal nTypeResc As Short, Optional ByVal nAgency As Integer = 0) As Boolean
        Dim lSel_Origin As String

        Dim lclsSurr_origins As Object
        Dim lrecinsPrevi7000 As eRemoteDB.Execute
        Dim lrecinsRouSurren As eRemoteDB.Execute

        Dim nCost_cov_dev As Double
        Dim nRentability As Double
        Dim nAmount_rec_dev As Double
        Dim nAmount_dev As Double

        Dim lblnAvailable As Double
        Dim lblnSurr_Amou As Double
        Dim ldblRequestedAmount As Double


        On Error GoTo InsPreVI7000_Err

        lclsSurr_origins = New Surr_origins
        lrecinsPrevi7000 = New eRemoteDB.Execute
        lrecinsRouSurren = New eRemoteDB.Execute

        '+
        '+ Se obtienen las diferentes cuentas origen asociadas a la póliza en tratamiento
        '+
        With lrecinsPrevi7000
            .StoredProcedure = "insPrevi7000_Origins"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", IIf(nSurr_reason = eRemoteDB.Constants.intNull, 0, nSurr_reason), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Surr", sSurrTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDelete", nDelete, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeResc", nTypeResc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessType", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsPreVI7000_Origins = .Run
            If InsPreVI7000_Origins Then
                Do While Not .EOF
                    '+ Por cada una de las cuentas se calcula el monto de Rescate disponible
                    With lrecinsRouSurren
                        .StoredProcedure = "InsRoutineSurrender"
                        .Parameters.Add("sRoutine", lrecinsPrevi7000.FieldToClass("sRouSurre"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate_val", lrecinsPrevi7000.FieldToClass("DEFFECDATE_VALUE"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sSurrType", sSurrTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nSurr_reason", nSurr_reason, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("sCodispl", "VI7000", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nValuePol", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nType", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        .Parameters.Add("nOrigin_Surr", IIf(lrecinsPrevi7000.FieldToClass("nOrigin_Surr") <= 0, lrecinsPrevi7000.FieldToClass("nOrigin"), lrecinsPrevi7000.FieldToClass("nOrigin_Surr")), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nOrigin_Loan", lrecinsPrevi7000.FieldToClass("nOrigin_Loan"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCharge", lrecinsPrevi7000.FieldToClass("nCharge"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nChargeAmo", lrecinsPrevi7000.FieldToClass("nChargeAmo"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nMonth", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nVP2", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nReverse", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        .Parameters.Add("nOrigin", lrecinsPrevi7000.FieldToClass("nOrigin"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nSurrAmount", lrecinsPrevi7000.FieldToClass("nRequestedAmount"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nSurrCost", lrecinsPrevi7000.FieldToClass("nWDCost"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nRetention", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nRet_pct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nGross_balance", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nCost_cov", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nLoans", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nIntloans", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                        .Parameters.Add("nCost_cov_dev", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nRentability", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nAmount_rec_dev", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nAmount_dev", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


                        If .Run(False) Then
                            If lrecinsPrevi7000.FieldToClass("nSurrAmount") > 0 Or sSurrTotal = "1" Then
                                lSel_Origin = "1"
                            Else
                                lSel_Origin = "2"
                            End If

                            nCost_cov_dev = .Parameters("nCost_cov_dev").Value
                            nRentability = .Parameters("nRentability").Value
                            nAmount_rec_dev = .Parameters("nAmount_rec_dev").Value
                            nAmount_dev = .Parameters("nAmount_dev").Value

                            lblnAvailable = .Parameters("nSurrAmount").Value

                            If sSurrTotal = "1" And .Parameters("nGross_balance").Value < .Parameters("nSurrCost").Value Then
                                bIsCancelling = True
                                ldblRequestedAmount = lblnAvailable
                                lblnSurr_Amou = 0
                            Else
                                If sSurrTotal = "1" Then
                                    ldblRequestedAmount = lblnAvailable
                                    lblnSurr_Amou = .Parameters("nGross_balance").Value
                                Else
                                    bIsCancelling = False
                                    lblnSurr_Amou = lrecinsPrevi7000.FieldToClass("nSurrAmount")
                                    ldblRequestedAmount = lrecinsPrevi7000.FieldToClass("nRequestedAmount")
                                End If
                            End If

                            Call Add(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lrecinsPrevi7000.FieldToClass("nOrigin"), lblnAvailable, lblnSurr_Amou, (.Parameters("nSurrCost").Value), (.Parameters("nRetention").Value), lSel_Origin, (.Parameters("nSurrCost").Value), ldblRequestedAmount, (.Parameters("NGROSS_BALANCE").Value), (.Parameters("nCost_cov").Value), (.Parameters("nLoans").Value), (.Parameters("nIntloans").Value), lrecinsPrevi7000.FieldToClass("dPaymentdate"), lrecinsPrevi7000.FieldToClass("nLocal_amount"), lrecinsPrevi7000.FieldToClass("nExchange"), nCost_cov_dev, nRentability, nAmount_rec_dev, nAmount_dev)

                            '+ Si el tipo de rescate el Total o ya existe en la tabla temporal,
                            '+ se insertan o actualizan directamente en la tabla temporal.
                            If (sSurrTotal = "1") Or lrecinsPrevi7000.FieldToClass("nSurrAmount") > 0 Then
                                Call lclsSurr_origins.CreT_Surr_Origins(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lrecinsPrevi7000.FieldToClass("nOrigin"), lblnAvailable, lblnSurr_Amou, .Parameters("nSurrCost").Value, .Parameters("nRetention").Value, nUsercode, nSurr_reason, sSurrTotal, ldblRequestedAmount, lrecinsPrevi7000.FieldToClass("nWDCost"), eRemoteDB.Constants.intNull, .Parameters("nCost_cov").Value, .Parameters("nLoans").Value, .Parameters("nIntloans").Value, lrecinsPrevi7000.FieldToClass("dPaymentdate"), nAgency, nCost_cov_dev, nRentability, nAmount_rec_dev, nAmount_dev)
                            End If
                        End If
                    End With
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With

InsPreVI7000_Err:
        If Err.Number Then
            InsPreVI7000_Origins = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSurr_origins = Nothing
        'UPGRADE_NOTE: Object lrecinsPrevi7000 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPrevi7000 = Nothing
        'UPGRADE_NOTE: Object lrecinsRouSurren may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRouSurren = Nothing
    End Function
	
	'% InsPreVI7004 : Permite predefinir los valores utilizados en el Grid de la VI7004 (Rescates APV)
    Public Function InsPreVI7004_Origins(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nProponum As Double, ByVal nSurr_reason As Double, ByVal sSurrTotal As String, ByVal nUsercode As Double, ByVal sProcessType As String, ByVal nDelete As Integer, ByVal nTypeResc As Short, Optional ByVal sCalc As String = "", Optional ByVal nOrigin As Short = 0, Optional ByVal nRequestedamount As Double = 0, Optional ByVal nTyp_Profitworker As Short = 0, Optional ByVal dPaymentdate As Date = eRemoteDB.Constants.dtmNull, Optional ByVal nValue_Typ As Integer = eRemoteDB.Constants.intNull, Optional ByVal nPercent As Double = 0) As Boolean
        Dim lSel_Origin As String

        Dim lclsSurr_origins As Object
        Dim lrecinsPrevi7004 As eRemoteDB.Execute
        Dim lrecinsRouSurren As eRemoteDB.Execute


        Dim lblnAvailable As Double
        Dim lblnSurr_Amou As Double
        Dim ldblRequestedAmount As Double
        Dim nCost_cov_dev As Double
        Dim nRentability As Double
        Dim nAmount_rec_dev As Double
        Dim nAmount_dev As Double


        On Error GoTo InsPreVI7004_Err

        lclsSurr_origins = New Surr_origins
        lrecinsPrevi7004 = New eRemoteDB.Execute
        lrecinsRouSurren = New eRemoteDB.Execute

        '+
        '+ Se obtienen las diferentes cuentas origen asociadas a la póliza en tratamiento
        '+
        With lrecinsPrevi7004
            .StoredProcedure = "insPrevi7000_Origins_Apv"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSurr_reason", IIf(nSurr_reason = eRemoteDB.Constants.intNull, 0, nSurr_reason), eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTyp_Surr", sSurrTotal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDelete", nDelete, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeResc", nTypeResc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessType", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRequestedamount", nRequestedamount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCalc", sCalc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_Profitworker", nTyp_Profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPaymentdate", dPaymentdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValue_Typ", nValue_Typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsPreVI7004_Origins = .Run
            If InsPreVI7004_Origins Then
                Do While Not .EOF
                    '+Si la cuenta posee saldo se procesa
                    If lrecinsPrevi7004.FieldToClass("nGross_balance") > 0 Then
                        If lrecinsPrevi7004.FieldToClass("nSurrAmount") > 0 Or sSurrTotal = "1" Then
                            lSel_Origin = "1"
                        Else
                            lSel_Origin = "2"
                        End If
                        nCost_cov_dev = lrecinsPrevi7004.FieldToClass("nCost_cov_dev")
                        nRentability = lrecinsPrevi7004.FieldToClass("nRentability")
                        nAmount_rec_dev = lrecinsPrevi7004.FieldToClass("nAmount_rec_dev")
                        nAmount_dev = lrecinsPrevi7004.FieldToClass("nAmount_dev")

                        Call Add_Apv(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, lrecinsPrevi7004.FieldToClass("nOrigin"), lrecinsPrevi7004.FieldToClass("nAvailable"), lrecinsPrevi7004.FieldToClass("nSurramount"), lrecinsPrevi7004.FieldToClass("nSurrCost"), lrecinsPrevi7004.FieldToClass("nRetention"), lSel_Origin, lrecinsPrevi7004.FieldToClass("nSurrCost"), lrecinsPrevi7004.FieldToClass("nRequestedAmount"), lrecinsPrevi7004.FieldToClass("nGross_Balance"), lrecinsPrevi7004.FieldToClass("nTyp_Profitworker"), lrecinsPrevi7004.FieldToClass("dPaymentdate"), lrecinsPrevi7004.FieldToClass("nLocal_amount"), lrecinsPrevi7004.FieldToClass("nExchange"), nCost_cov_dev, nRentability, nAmount_rec_dev, nAmount_dev)
                    End If
                    .RNext()
                Loop
                .RCloseRec()
            End If
        End With

InsPreVI7004_Err:
        If Err.Number Then
            InsPreVI7004_Origins = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsSurr_origins may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSurr_origins = Nothing
        'UPGRADE_NOTE: Object lrecinsPrevi7004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPrevi7004 = Nothing
        'UPGRADE_NOTE: Object lrecinsRouSurren may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsRouSurren = Nothing
    End Function
	
	
	'**%Objective: Use when making reference to an element of the collection
	'**%           vntIndexKey contains the index or the password of the collection,
	'%Objetivo: Se usa al hacer referencia a un elemento de la colección
	'%          vntIndexKey contiene el índice o la clave de la colección,
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Surr_origins
		Get
			On Error GoTo ErrorHandler
			Item = mCol.Item(vntIndexKey)
			
			Exit Property
ErrorHandler: 
			'UPGRADE_NOTE: Object Item may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			Item = Nothing
		End Get
	End Property
	
	'**%Objective: Returns the number of elements that the collection has
	'%Objetivo: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			On Error GoTo ErrorHandler
			Count = mCol.Count()
			
			Exit Property
ErrorHandler: 
			Count = 0
		End Get
	End Property
	
	'**%Objective: Enumerates the collection for use in a For Each...Next loop
	'%Objetivo: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'On Error GoTo ErrorHandler
			'NewEnum = mCol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			''UPGRADE_NOTE: Object NewEnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			'NewEnum = Nothing
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'**%Objective: Deletes an element from the collection
	'%Objetivo: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		On Error GoTo ErrorHandler
		mCol.Remove(vntIndexKey)
		
		Exit Sub
ErrorHandler: 
		
	End Sub
	
	'**%Objective: Controls the creation of an instance of the collection
	'%Objetivo: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		On Error GoTo ErrorHandler
		mCol = New Collection
		
		Exit Sub
ErrorHandler: 
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Controls the destruction of an instance of the collection
	'%Objetivo: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		On Error GoTo ErrorHandler
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
		
		Exit Sub
ErrorHandler: 
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'%DateVal_Surr: Valida y calcula fecha de valorizacion y valor cuota para la fecha de rescate
	Public Function insDateVal_Surr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTypeResc As Short, ByVal nSaving_pct As Short, ByVal sExecType As String) As Boolean
		Dim lrecDate_Val_surr As eRemoteDB.Execute
		
		On Error GoTo DateVal_Surr_Err
		
		lrecDate_Val_surr = New eRemoteDB.Execute
		
		With lrecDate_Val_surr
			.StoredProcedure = "insDate_Val_surr"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeResc", nTypeResc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSaving_pct", nSaving_pct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sExecType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				dEffecdate = .Parameters("dEffecdate").Value
				If .Parameters("nExists").Value = 1 Then
					insDateVal_Surr = True
				Else
					insDateVal_Surr = False
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecDate_Val_surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDate_Val_surr = Nothing
		
DateVal_Surr_Err: 
		If Err.Number Then
			insDateVal_Surr = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDate_Val_surr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDate_Val_surr = Nothing
	End Function
End Class






