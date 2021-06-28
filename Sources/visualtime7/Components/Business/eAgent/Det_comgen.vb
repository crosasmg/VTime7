Option Strict Off
Option Explicit On
Public Class Det_comgen
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_comgen.cls                           $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 19/01/04 1:55p                               $%'
	'% $Revision:: 19                                       $%'
	'%-------------------------------------------------------%'
	
	'**+ The key field correspond to nComtabge, nBranch, nProduct, nCover, dEffecdate.
	'+ El campo llave corresponde a nComtabge, nBranch, nProduct, nCover, dEffecdate.
	
	'+ Column_name         Type                 Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ---- ----- -------- ------------------ --------------------
	Public nComtabge As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nBranch As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public nCover As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                 no       (n/a)              (n/a)
	Public nWay_Pay As Integer 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nModulec As Integer 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nCurrency As Integer 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nAmount As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nInit_Month As Integer 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nFinal_Month As Integer 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nPercent As Double 'decimal  5      4    2     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2      5    0     no       (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                 yes      (n/a)              (n/a)
	Public nDuration As Integer 'number(5)                  yes
	Public nPayfreq As Integer 'number(5)                  yes
	Public nInstallments As Integer 'number(5)                  yes
	
	Public nStatusInstance As Integer
	Public sProductDes As String
    Public sShort_des As String

    Public nAgreement As Integer
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	'**% Find: Searches for the information in the general commissions table
	'% Find: Busca la información de una tabla de comisiones de generales.
    Public Function Find(ByVal nComtabge As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nWay_Pay As Integer, ByVal nModulec As Integer, ByVal nDuration As Integer, ByVal dEffecdate As Date, ByVal nInit_Month As Integer, Optional ByVal lblnFind As Boolean = False, Optional ByVal nAgreement As Integer = intNull) As Boolean
        Dim lrecreaDet_comgen_v As eRemoteDB.Execute

        On Error GoTo Find_Err

        If nComtabge = Me.nComtabge And nBranch = Me.nBranch And nProduct = Me.nProduct And nCover = Me.nCover And dEffecdate = Me.dEffecdate And nAgreement = Me.nAgreement And Not lblnFind Then
            Find = True
        Else
            lrecreaDet_comgen_v = New eRemoteDB.Execute
            With lrecreaDet_comgen_v
                .StoredProcedure = "reaDet_comgen_v"
                .Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nInit_Month", nInit_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    Me.nComtabge = .FieldToClass("nComtabge")
                    Me.nBranch = .FieldToClass("nBranch")
                    Me.nProduct = .FieldToClass("nProduct")
                    Me.nCover = .FieldToClass("nCover")
                    Me.dEffecdate = .FieldToClass("dEffecdate")
                    Me.nWay_Pay = .FieldToClass("nWay_pay")
                    Me.nModulec = .FieldToClass("nModulec")
                    Me.dNulldate = .FieldToClass("dNulldate")
                    Me.nCurrency = .FieldToClass("nCurrency")
                    Me.nAmount = .FieldToClass("nAmount")
                    Me.nInit_Month = .FieldToClass("nInit_Month")
                    Me.nFinal_Month = .FieldToClass("nFinal_Month")
                    Me.nAgreement = .FieldToClass("nAgreement")
                    .RCloseRec()
                    Find = True
                Else
                    Find = False
                End If
            End With

Find_Err:
            If Err.Number Then
                Find = False
            End If
            'UPGRADE_NOTE: Object lrecreaDet_comgen_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaDet_comgen_v = Nothing
            On Error GoTo 0
        End If
    End Function
	
	'**% Update: add/update the information in the main table for the transaction.
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Private Function Update() As Boolean
		Dim lrecinsDet_comgen As eRemoteDB.Execute
		
		lrecinsDet_comgen = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+Parameter definitions for stored procedure 'insudb.insDet_comgen'
		'+Definición de parámetros para stored procedure 'insudb.insDet_comgen'
		'**+ Data of May 03,2001 02:44:47 p.m.
		'+Información leída el 03/05/2001 02:44:47 p.m.
		
		With lrecinsDet_comgen
			.StoredProcedure = "insDet_comgen"
			.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Month", nInit_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFinal_Month", nFinal_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInstallments", nInstallments, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDet_comgen = Nothing
	End Function
	
	'**% Delete: Delete information in the main table of the class.
	'% Delete: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Private Function Delete() As Boolean
		Dim lrecinsDelDet_comgen As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecinsDelDet_comgen = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.insDelDet_comgen'
		'+Definición de parámetros para stored procedure 'insudb.insDelDet_comgen'
		
		With lrecinsDelDet_comgen
			.StoredProcedure = "insDelDet_comgen"
			.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", IIf(nWay_Pay = eRemoteDB.Constants.intNull, 0, nWay_Pay), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Month", nInit_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDuration", nDuration, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecinsDelDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelDet_comgen = Nothing
		On Error GoTo 0
	End Function
	
	'**% Find_date. This function seraches the older effect date in the commissions table.
	'%Find_date. Esta funcion se encarga de buscar la mayor de las
	'%fecha de efecto de los registros de una tabla de  comisiones.
	Public Function Find_date(ByVal nComtabge As Integer) As Boolean
		Dim lrecreaDet_comgen_date As eRemoteDB.Execute
		
		On Error GoTo Find_date_Err
		
		lrecreaDet_comgen_date = New eRemoteDB.Execute
		
		'**+ Parameter definitions for stored procedure 'insudb.reaDet_comgen_date'
		'+Definición de parámetros para stored procedure 'insudb.reaDet_comgen_date'
		'**+ Data of May 04, 2001  09:38:52 a.m.
		'+Información leída el 04/05/2001 09:38:52 a.m.
		
		With lrecreaDet_comgen_date
			.StoredProcedure = "reaDet_comgen_date"
			.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				Find_date = True
			Else
				Me.dEffecdate = CDate("01/01/1800")
				Find_date = False
			End If
		End With
		
Find_date_Err: 
		If Err.Number Then
			Find_date = False
		End If
		'UPGRADE_NOTE: Object lrecreaDet_comgen_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDet_comgen_date = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValMAG003_K: Validates the data entered on the header forma
	'%insValMAG003_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG003_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nComtabge As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Static lstrValField As String
		
		'**- Variable definition for lclsErrors for the errors of the window sending
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsDet_comgen As Det_comgen
		Dim lcolDet_comgens As Det_comgens
		
		Dim ldtmMaxDate As Date
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMAG003_K_Err
		
		lclsErrors = New eFunctions.Errors
		lcolDet_comgens = New eAgent.Det_comgens
		
		lblnInquiry = False
		lblnModify = False
		lblnErrors = False
		
		'**+ Validation field Table
		'+Validacion del campo TABLA
		
		If nComtabge = eRemoteDB.Constants.intNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10048)
		Else
			
			'**+ Validates the field TABLE is registered into the data base - ACM - Apr-26-2002
			'+ Se valida que el campo TABLA se encuentre registrado en la base de datos - ACM - 26/04/2002
			If Not Find_Table(nComtabge) Then
				Call lclsErrors.ErrorMessage(sCodispl, 60391)
			End If
		End If
		
		'**+ Validation of the field Date
		'+Validacion del campo FECHA
		
		If dEffecdate = dtmNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10190)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Or nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				'If nAction = eFunctions.Menues.TypeActions.clngActionUpdate And dEffecdate <= Today Then
				'	lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
				'	Call lclsErrors.ErrorMessage(sCodispl, 10868)
				'	lblnErrors = True
				'	lblnModify = False
				'	lblnInquiry = True
				'End If
				
				Me.nComtabge = nComtabge
				Call Find_date(nComtabge)
				
				ldtmMaxDate = Me.dEffecdate
				
				If ldtmMaxDate > dEffecdate Then
					If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
						lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
						Call lclsErrors.ErrorMessage(sCodispl, 10869,  ,  , CStr(ldtmMaxDate))
						lblnErrors = True
						lblnModify = False
						lblnInquiry = True
					Else
						If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
							lblnInquiry = True
							lblnModify = True
						Else
							lblnInquiry = False
							lblnModify = False
						End If
					End If
				Else
					If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
						lblnInquiry = True
						lblnModify = True
					Else
						lblnInquiry = False
						lblnModify = False
					End If
				End If
			End If
		End If
		
		If lblnErrors And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			lcolDet_comgens = New Det_comgens
			If Not lcolDet_comgens.Find(nComtabge, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1073)
			End If
		End If
		
		insValMAG003_K = lclsErrors.Confirm
		
insValMAG003_K_Err: 
		If Err.Number Then
			insValMAG003_K = "insValMAG003_K:" & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lcolDet_comgens may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolDet_comgens = Nothing
	End Function
	
	'**% insValMAG003: validate the data entered on the detail zone for the form
	'%insValMAG003: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
    Public Function insValMAG003(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nComtabge As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nWay_Pay As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nInit_Month As Integer = 0, Optional ByVal nFinal_Month As Integer = 0, Optional ByVal nPercent As Integer = 0, Optional ByVal nDuration As Integer = 0, Optional ByVal sBrancht As String = "", Optional ByVal nAgreement As Integer = 0) As String
        '**- Variable definition lclsErrors for the errors in the window sending
        '- Se define la variable lclsErrors para el envío de errores de la ventana

        Dim lclsErrors As eFunctions.Errors
        Dim lclsDet_comgen As eAgent.Det_comgen
        Dim lclsTab_modul As eProduct.Tab_modul
        Dim lclsCover As eProduct.Gen_cover
        Dim lclsValField As eFunctions.valField

        lclsErrors = New eFunctions.Errors
        lclsValField = New eFunctions.valField
        lclsValField.objErr = lclsErrors

        On Error GoTo insValMAG003_Err

        '**+ Validation of the field Line of Business
        '+ Validación del campo Ramo

        If nBranch = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9064)
        Else
            '+Validación del Producto
            If nProduct <> eRemoteDB.Constants.intNull Then
                '+ debe ser un producto de generales
                If sBrancht = "1" Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1025)
                End If

                '+ Validación del Módulo
                If nModulec <> eRemoteDB.Constants.intNull Then
                    lclsTab_modul = New eProduct.Tab_modul
                    If Not lclsTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 11030)
                    End If
                End If

                '+ Validación de la Cobertura
                If nCover <> eRemoteDB.Constants.intNull Then
                    lclsCover = New eProduct.Gen_cover
                    If Not lclsCover.Find(nBranch, nProduct, IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), nCover, dEffecdate) Then
                        Call lclsErrors.ErrorMessage(sCodispl, 11165)
                    End If
                End If
            Else
                If nModulec <> eRemoteDB.Constants.intNull Or nCover <> eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 1014)
                End If
            End If
        End If

        '+ La duración del seguro debe estar llena
        If nDuration = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 56005)
        End If

        '+ Validación del Mes Inicial
        If nInit_Month = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 36066)
        Else
            '+ Validación del Mes Final
            If nFinal_Month > 0 Then
                If nFinal_Month < nInit_Month Then
                    Call lclsErrors.ErrorMessage(sCodispl, 36068)
                End If
            End If
        End If

        '+ El porcentaje debe estár entre 0 y 100
        If nPercent < 0 Or nPercent > 100 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1938)
        End If

        '**+ Validate that the value is not previously registered in the Det_comgen table
        '+ Se valida que el valor no se encuentra previamente registrado en la tabla Det_comgen
        With Me
            .nComtabge = nComtabge
            .nBranch = nBranch
            .nProduct = nProduct
            .nModulec = nModulec
            .nCover = nCover
            .nWay_Pay = nWay_Pay
            .dEffecdate = dEffecdate
            .nInit_Month = nInit_Month
            .nFinal_Month = nFinal_Month
            .nDuration = nDuration
            .nAgreement = nAgreement
            If .Find_range() Then
                If .nFinal_Month = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage(sCodispl, 60214, , , " [" & .nInit_Month & ",->] ")
                Else
                    Call lclsErrors.ErrorMessage(sCodispl, 60214, , , " [" & .nInit_Month & "," & .nFinal_Month & "] ")
                End If
            End If
        End With

        '+ Validación del Porcentaje y/o Monto de comisión
        If (nPercent = eRemoteDB.Constants.intNull Or nPercent = 0) And (nAmount = eRemoteDB.Constants.intNull Or nAmount = 0) Then
            Call lclsErrors.ErrorMessage(sCodispl, 60428)
            'Else
            '    lclsValField.Min = 0.0#
            '    lclsValField.Max = 100.0#
            '   lclsValField.Descript = "% de Comisión"
            '   lclsValField.ErrRange = 11239
            '   lclsValField.ValNumber(nPercent)
        End If

        '**+ Validate that the values does not exist
        '+Se valida que los valores introducidos no estén registrados

        If nComtabge <> eRemoteDB.Constants.intNull And nBranch <> eRemoteDB.Constants.intNull And nBranch <> 0 And sAction = "Add" Then
            lclsDet_comgen = New eAgent.Det_comgen
            If lclsDet_comgen.Find(nComtabge, nBranch, IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), IIf(nWay_Pay = eRemoteDB.Constants.intNull, 0, nWay_Pay), IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), IIf(nDuration = eRemoteDB.Constants.intNull, 0, nDuration), dEffecdate, nInit_Month, False, IIf(nAgreement = eRemoteDB.Constants.intNull, 0, nAgreement)) Then
                Call lclsErrors.ErrorMessage(sCodispl, 8307)
            End If
        End If

        '**+ 
        '+ Si se selecciona una moneda se debe indicar la Comisión Fija
        If nCurrency <> eRemoteDB.Constants.intNull And (nAmount = eRemoteDB.Constants.intNull Or nAmount = 0) Then
            Call lclsErrors.ErrorMessage(sCodispl, 700100)
        End If


        insValMAG003 = lclsErrors.Confirm

insValMAG003_Err:
        If Err.Number Then
            insValMAG003 = "insValMAG003: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_modul = Nothing
        'UPGRADE_NOTE: Object lclsCover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCover = Nothing
        'UPGRADE_NOTE: Object lclsDet_comgen may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDet_comgen = Nothing
        'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValField = Nothing
        On Error GoTo 0
    End Function
	
	'*** InsPostMAG003: create/update correspondent
	'*** registrations in the Det_comlif table
	'*InsPostMAG003: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Det_comlif
    Public Function insPostMAG003(ByVal sAction As String, Optional ByVal nComtabge As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nWay_Pay As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nInit_Month As Integer = 0, Optional ByVal nFinal_Month As Integer = 0, Optional ByVal nPercent As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nDuration As Integer = 0, Optional ByVal nPayfreq As Integer = 0, Optional ByVal nInstallments As Integer = 0, Optional ByVal nAgreement As Integer = 0) As Boolean
        On Error GoTo insPostMAG003_err

        nProduct = IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct)
        nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
        nWay_Pay = IIf(nWay_Pay = eRemoteDB.Constants.intNull, 0, nWay_Pay)
        nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
        nDuration = IIf(nDuration = eRemoteDB.Constants.intNull, 0, nDuration)
        nAgreement = IIf(nAgreement = eRemoteDB.Constants.intNull, 0, nAgreement)

        insPostMAG003 = True

        With Me
            .nComtabge = nComtabge
            .nBranch = nBranch
            .nProduct = nProduct
            .nCover = nCover
            .dEffecdate = dEffecdate
            .nWay_Pay = nWay_Pay
            .nModulec = nModulec
            .nCurrency = nCurrency
            .nAmount = nAmount
            .nInit_Month = nInit_Month
            .nFinal_Month = nFinal_Month
            .nPercent = nPercent
            .nUsercode = nUsercode
            .nDuration = nDuration
            .nPayfreq = nPayfreq
            .nInstallments = nInstallments
            .nAgreement = nAgreement
        End With

        Select Case sAction

            '+Si la opción seleccionada es Registrar o Modificar
            Case "Add", "Update"
                insPostMAG003 = Update()

                '+Si la opción seleccionada es Eliminar
            Case "Del"
                insPostMAG003 = Delete()
        End Select

insPostMAG003_err:
        If Err.Number Then
            insPostMAG003 = False
        End If
        On Error GoTo 0
    End Function
	
	'% Find_Table: Esta función determina si el número de tabla que suministrado por el usuario
	'%             se encuentra o no registrado en la base de datos - ACM - 26/04/2002
	Public Function Find_Table(ByVal nTable As Integer) As Boolean
		Dim lrecreaTab_comgen_v As eRemoteDB.Execute
		
		On Error GoTo Find_Table_err
		
		lrecreaTab_comgen_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_comgen_v'
		'+ Información leída el 26/04/2002 10:55:18 a.m.
		
		With lrecreaTab_comgen_v
			.StoredProcedure = "reaTab_comgen_v"
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_Table = .Run(True)
			.RCloseRec()
		End With
		
Find_Table_err: 
		If Err.Number Then
			Find_Table = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_comgen_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_comgen_v = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_range. Esta funcion se encarga de buscar si el rango de Meses
	'%en la ventana, existe dentro de otro rango registrado en la tabla Det_comgen.
	Public Function Find_range() As Boolean
		Dim lrecreaDet_comgen_range As eRemoteDB.Execute
		
		On Error GoTo Find_range_Err
		
		lrecreaDet_comgen_range = New eRemoteDB.Execute
		
		'**+ Parameter deifinition for stored procedure 'insudb.reaDet_comgen_range'
		'+Definición de parámetros para stored procedure 'insudb.reaDet_comgen_range'
		'**+ Information read on May 14, 2001 03:36:16 p.m.
		'+Información leída el 14/05/2001 03:36:18 p.m.
		
		With lrecreaDet_comgen_range
			.StoredProcedure = "reaDet_comgen_range"
			.Parameters.Add("nComtabge", nComtabge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", IIf(nWay_Pay = eRemoteDB.Constants.intNull, 0, nWay_Pay), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Month", nInit_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFinal_Month", nFinal_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDuration", IIf(nDuration = eRemoteDB.Constants.intNull, 0, nDuration), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgreement", IIf(nAgreement = eRemoteDB.Constants.intNull, 0, nAgreement), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If .FieldToClass("nExist") = 1 Then
					Find_range = True
				End If
			End If
		End With
Find_range_Err: 
		If Err.Number Then
			Find_range = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDet_comgen_range may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDet_comgen_range = Nothing
	End Function
End Class






