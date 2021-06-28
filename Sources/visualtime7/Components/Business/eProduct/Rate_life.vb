Option Strict Off
Option Explicit On
Public Class Rate_life
	'%-------------------------------------------------------%'
	'% $Workfile:: Rate_life.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Define the columns of the Rate_life table.
	'- Se definen las columnas de la tabla Rate_life.
	
	'+ Column_name          Type                  Computed    Length   Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'---------------------- --------------------- ----------- -------- ----- ----- ---------- -------------------- ---------------------
	Public nBranch As Integer 'smallint     no          2           5     0     no      (n/a)                (n/a)
	Public nProduct As Integer 'smallint     no          2           5     0     no      (n/a)                (n/a)
	Public nCover As Integer 'smallint     no          2           5     0     no      (n/a)                (n/a)
	Public nAgeStart As Integer 'smallint     no          2           5     0     no      (n/a)                (n/a)
	Public nAgeEnd As Integer 'smallint     no          2           5     0     no      (n/a)                (n/a)
	Public dEffecdate As Date 'datetime     no          8                       no      (n/a)                (n/a)
	Public dNulldate As Date 'datetime     no          8                       yes     (n/a)                (n/a)
	Public nRatenive As Double 'decimal      no          5           8     5     yes     (n/a)                (n/a)
	Public nRatenoni As Double 'decimal      no          5           8     5     yes     (n/a)                (n/a)
	Public nRatepure As Double 'decimal      no          5           8     5     yes     (n/a)                (n/a)
	Public nUsercode As Integer 'smallint     no          2           5     0     no      (n/a)                (n/a)
	
	'**- Define the auxiliary properties to be used in the DP017 - Rates according to ages for plans.
	'- Se definen las propiedades auxiliares a ser utilizadas en DP017 - Tasas según edades para planes.
	Public nStatusInstance As Integer
	
	'**% ADD: Adds new records to the table "Rate_life".  It returns TRUE or FALSE if stored procedure executed correctly.
	'% ADD: Este método se encarga de agregar nuevos registros a la tabla "Rate_life". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lrecRate_life As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lrecRate_life = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.creRate_life'
		'+ Definición de parámetros para stored procedure 'insudb.creRate_life'
		With lrecRate_life
			.StoredProcedure = "creRate_life"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeStart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatenive", nRatenive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatenoni", nRatenoni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatepure", nRatepure, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRate_life = Nothing
	End Function
	
	'**% Update: Updates records in the table "Rate_life".  It returns TRUE or FALSE depending on the execution of the stored procedure.
	'% Update: Este método se encarga de actualizar registros en la tabla "Rate_life". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecUpdRate_life As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdRate_life = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.updRate_life'
		'+ Definición de parámetros para stored procedure 'insudb.updRate_life'
		With lrecUpdRate_life
			.StoredProcedure = "updRate_life"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeStart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatenive", nRatenive, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatenoni", nRatenoni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatepure", nRatepure, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUpdRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdRate_life = Nothing
	End Function
	
	'**% Delete: Deletes records in the table "Rate_life".  It returns TRUE or FALSE depending on the execution of the stored procedure.
	'% Delete: Este método se encarga de eliminar registros en la tabla "Rate_life". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecDelRate_life As eRemoteDB.Execute
		
		lrecDelRate_life = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'**+ Delete the record in the 'tab_name_b' yable
		'+ Eliminar el registro de la tabla 'tab_name_b'
		With lrecDelRate_life
			.StoredProcedure = "delRate_life"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeStart", nAgeStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDelRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelRate_life = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValDP017_K: Make the validations of the fields of the DP017 window's Header - Rates according to ages for life plans
	'% insValDP017_K: Realiza las validaciones de los campos del Header de la ventana DP017 - Tasas según edades para planes de vida.
	Public Function insValDP017_k(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lObjValField As eFunctions.valField
		Dim lblnError As Boolean
		Dim ldtmMaxdate As Date
		
		lobjErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		lObjValField = New eFunctions.valField
		
		insValDP017_k = String.Empty
		
		On Error GoTo insValDP017_k_Err
		
		lblnError = False
		
		'**+ Validate the field Line of business code
		'+ Se valida el campo Código del Ramo.
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1005)
		End If
		
		'**+ Validate the field Product code
		'+ Se valida el campo Código del Producto.
		If nProduct = 0 Or nProduct = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11009)
		Else
			If Not lclsProduct.insValProdMaster(nBranch, nProduct) Then
				Call lobjErrors.ErrorMessage(sCodispl, 9066)
			Else
				
				'**+ Must be a life product
				'+ Dede ser un producto de vida.
				If lclsProduct.sBrancht <> 1 Then
					Call lobjErrors.ErrorMessage(sCodispl, 11149)
				End If
			End If
		End If
		
		'**+ Validate the field Coverage code.
		'+ Se valida el campo Código de la Cobertura.
		If nCover = 0 Or nCover = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 4061)
		Else
            If Not insValLife_cover(nBranch, nProduct, nCover, Today, "1") Then
                Call lobjErrors.ErrorMessage(sCodispl, 11004)
            End If
		End If
		
		'**+ Make the validations of the field Date
		'+ Se realizan las validaciones del campo Fecha.
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 2056)
		Else
			lObjValField.objErr = lobjErrors
			
			If Not lObjValField.ValDate(dEffecdate) Then
				Call lobjErrors.ErrorMessage(sCodispl, 1001)
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					If CDate(dEffecdate) <= Today Then
						lobjErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
                        Call lobjErrors.ErrorMessage(sCodispl, 10868, , eFunctions.Errors.TextAlign.RigthAling, CStr(Today))
                    End If
                    'Else
                    If (nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull) And (nProduct <> 0 And nProduct <> eRemoteDB.Constants.intNull) And (nCover <> 0 And nCover <> eRemoteDB.Constants.intNull) Then
                        ldtmMaxdate = insValEffecdate(nBranch, nProduct, nCover)

                        If ldtmMaxdate >= dEffecdate Then
                            lobjErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
                            Call lobjErrors.ErrorMessage(sCodispl, 10869, , eFunctions.Errors.TextAlign.RigthAling, CStr(ldtmMaxdate))
                        End If
                    End If

                End If
			End If
		End If
		
		insValDP017_k = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		'UPGRADE_NOTE: Object lObjValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lObjValField = Nothing
		
insValDP017_k_Err: 
		If Err.Number Then
			insValDP017_k = insValDP017_k & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValLife_cover: The objective of this method is obtain if there is data in the coverage
	'**% of a life product.
	'% insValLife_cover: El objetivo de este metodo es obtener si existen los datos de la cobertura
	'% de un producto de vida.
	Public Function insValLife_cover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String) As Boolean
		Dim lrecLife_cover As eRemoteDB.Execute
		
		On Error GoTo insValLife_cover_err
		lrecLife_cover = New eRemoteDB.Execute
		With lrecLife_cover
			.StoredProcedure = "reaLife_cover_3"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValLife_cover = True
				.RCloseRec()
			End If
		End With
		
insValLife_cover_err: 
		If Err.Number Then
			insValLife_cover = False
		End If
		'UPGRADE_NOTE: Object lrecLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_cover = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValEffecdate: The objective of this function is obtain the maximum date of modification.
	'% insValEffecdate: El objetivo de esta función es obtener la máxima fecha de modificación.
	Public Function insValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer) As Date
		Dim lrecRate_life As eRemoteDB.Execute
		
		On Error GoTo insValEffecdate_err
		
		insValEffecdate = CDate("01/01/1800")
		lrecRate_life = New eRemoteDB.Execute
		With lrecRate_life
			.StoredProcedure = "insValRate_life"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("dEffecdate")) Then
					insValEffecdate = .FieldToClass("dEffecdate")
				End If
				
				.RCloseRec()
			End If
		End With
		
insValEffecdate_err: 
		If Err.Number Then
			insValEffecdate = CDate("01/01/1800")
		End If
		'UPGRADE_NOTE: Object lrecRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRate_life = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValDP017: Make the validation of the Detail fields of the DP017 window - Rates according ages for life plans.
	'% insValDP017: Realiza la validación de los campos del Detalle de la ventana DP017 - Tasas según edades para planes de vida.
	Public Function insValDP017(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nAgeStart As Integer = 0, Optional ByVal nAgeEnd As Integer = 0, Optional ByVal nRatepure As Double = 0, Optional ByVal nRatenoni As Double = 0, Optional ByVal nRatenive As Double = 0) As String
		Dim lobjValues As eFunctions.valField
		Dim lobjErrors As eFunctions.Errors
		
		'**- Define the constants that contains the maximum and minimum value for the ages.
		'- Se definen las constantes que contiene el máximo y minimo valor para las edades.
		Const CN_MAXE As Integer = 130
		Const CN_MINE As Integer = 0
		
		lobjValues = New eFunctions.valField
		lobjErrors = New eFunctions.Errors
		
		insValDP017 = String.Empty
		
		On Error GoTo insValDP017_Err
		
		'**+ Make the validations of the field "Initial age"
		'+ Se realizan las validaciones del campo "Edad inicial".
		If nAgeStart = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial:")
		Else
			If sAction = "Add" Then
				With lobjValues
					.objErr = lobjErrors
					
					.EqualMin = True
					.EqualMax = True
					.ErrEmpty = 11109
					.Min = CN_MINE
					.Max = CN_MAXE
					
					'**+ Verify that it is not empty, and that it is in the correct range.
					'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
					If Not .ValNumber(nAgeStart,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					Else
						If insValOtherRange(nBranch, nProduct, nCover, nAgeStart, dEffecdate) Then
							Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial:")
						End If
					End If
				End With
			End If
		End If
		
		'**+ Make the validations of the field "Initial date"
		'+ Se realizan las validaciones del campo "Edad final".
		If nAgeEnd = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Edad final:")
		Else
			If sAction = "Add" Then
				With lobjValues
					.objErr = lobjErrors
					
					.EqualMin = True
					.EqualMax = True
					.ErrEmpty = 11110
					.Min = CN_MINE
					.Max = CN_MAXE
					
					'**+ Varify that it is not empty, and that it is in the correct range.
					'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
					If Not .ValNumber(nAgeEnd,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					Else
						If .ValNumber(nAgeStart,  , eFunctions.valField.eTypeValField.onlyvalid) Then
							If nAgeEnd < nAgeStart Then
								Call lobjErrors.ErrorMessage(sCodispl, 11409)
							Else
								If insValOtherRange(nBranch, nProduct, nCover, nAgeEnd, dEffecdate) Then
									Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad final:")
								End If
							End If
						End If
					End If
				End With
			End If
		End If
		'** If the initial and final age has value, validate the intersection of the ranges.
		'+ Si la edad inicial y final tienen valor se valida la intersección de los rangos.
		If (nAgeStart <> 0 And nAgeStart <> eRemoteDB.Constants.intNull) And (nAgeEnd <> 0 And nAgeEnd <> eRemoteDB.Constants.intNull) And sAction = "Add" Then
			If insValOtherRange1(nBranch, nProduct, nCover, nAgeStart, nAgeEnd, dEffecdate) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11138,  , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial - final:")
			End If
		End If
		
		'**+ Make the validations of the field "Premium pure".
		'+ Se realizan las validaciones del campo "Prima pura".
		If nRatepure = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Prima pura:")
		Else
			If nRatepure <= 0 Or nRatepure > 999.99999 Then
				Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "Prima pura - [0.00001-999.99999]:")
			End If
		End If
		
		'**+ Make the validation of the field "Commercial not leveled premium".
		'+ Se realizan las validaciones del campo "Prima comercial no nivelada".
		If nRatenoni = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Prima comercial no nivelada:")
		Else
			If nRatenoni <= 0 Or nRatenoni > 999.99999 Then
				Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "Prima comercial no nivelada - [0.00001-999.99999]:")
			End If
		End If
		
		'**+ Make the validations of the field "Commercial leveled premium"
		'+ Se realizan las validaciones del campo "Prima comercial nivelada".
		If nRatenive = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling, "Prima comercial nivelada:")
		Else
			If nRatenive <= 0 Or nRatenive > 999.99999 Then
				Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "Prima comercial nivelada - [0.00001-999.99999]:")
			End If
		End If
		
		insValDP017 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
insValDP017_Err: 
		If Err.Number Then
			insValDP017 = insValDP017 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValOtherRange: verify if there are intersections between the ranges.
	'% insValOtherRange: Permite verificar si existen intersecciones entre los rangos.
	Public Function insValOtherRange(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nAge As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecRate_life As eRemoteDB.Execute = New eRemoteDB.Execute

        On Error GoTo insValOtherRange_Err
		With lrecRate_life
			.StoredProcedure = "insValOtherRange"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValOtherRange = True
				.RCloseRec()
			End If
		End With
		
insValOtherRange_Err: 
		If Err.Number Then
			insValOtherRange = False
		End If
		'UPGRADE_NOTE: Object lrecRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRate_life = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValOtherRange1: verify if there are intersetions between the ranges.
	'% insValOtherRange1: Permite verificar si existen intersecciones entre los rangos.
	Public Function insValOtherRange1(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nAgeIni As Integer, ByVal nAgeEnd As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecRate_life As eRemoteDB.Execute
		
		On Error GoTo insValOtherRange1_err
		lrecRate_life = New eRemoteDB.Execute
		With lrecRate_life
			.StoredProcedure = "insValOtherRange_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeIni", nAgeIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValOtherRange1 = True
				.RCloseRec()
			End If
		End With
		
insValOtherRange1_err: 
		If Err.Number Then
			insValOtherRange1 = False
		End If
		'UPGRADE_NOTE: Object lrecRate_life may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRate_life = Nothing
		On Error GoTo 0
	End Function
	
	'**% insPostSP017: This function is in charge of keeping the data in the tables, in this case Rate_life
	'**% according to the introduced data in the DP017 window - Rates according ages for life plans
	'% insPostDP017: Esta función se encarga de almacenar los datos en las tablas, en este caso Rate_life
	'% según los datos introducidos en la ventana DP017 - Tasas según edades para planes de vida.
	Public Function insPostDP017(ByVal MainAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nAgeStart As Integer, ByVal nAgeEnd As Integer, ByVal nRatepure As Double, ByVal nRatenoni As Double, ByVal nRatenive As Double, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostDP017_err
		
		insPostDP017 = True
		
		If insPostDP017 Then
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.nCover = nCover
				.dEffecdate = dEffecdate
				.nAgeStart = nAgeStart
				.nAgeEnd = nAgeEnd
				.nRatepure = nRatepure
				.nRatenoni = nRatenoni
				.nRatenive = nRatenive
				.nUsercode = nUsercode
			End With
			
			Select Case MainAction
				
				'**+ If the action is Record
				'+ Si la acción es Registrar.
				Case "Add"
					insPostDP017 = Add()
					
					'**+ If the action is Modify
					'+ Si la acción es Modificar.
				Case "Update"
					insPostDP017 = Update()
					
					'**+ If the action is Delete
					'+ Si la acción es Eliminar.
				Case "Delete"
					insPostDP017 = Delete()
			End Select
		End If
		
insPostDP017_err: 
		If Err.Number Then
			insPostDP017 = False
		End If
		On Error GoTo 0
	End Function
End Class






