Option Strict Off
Option Explicit On
Public Class Sumcov_apl
	'%-------------------------------------------------------%'
	'% $Workfile:: Sumcov_apl.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Column according to the table in the system 04/18/2001
	'**- The key fields correspond to: nModulec, nCover, nSumins_co, dEffecdate, nBranch, nProduct
	'- Columnas segun tabla en el sistema al 18/04/2001
	'- Los campos llave corresponden a nModulec, nCover, nSumins_co, dEffecdate, nBranch, nProduct
	
	'+  Column_name                  Type                 Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------- -------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- --------------------
	Public nModulec As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public nCover As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public dEffecdate As Date 'datetime 8                       no                                  (n/a)                               (n/a)
	Public nSumins_co As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public nBranch As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public nSumins_rat As Double 'decimal  5           5     2     yes                                 (n/a)                               (n/a)
	Public dNulldate As Date 'datetime 8                       yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer 'smallint 2           5     0     no                                  (n/a)                               (n/a)
	Public dCompdate As Date 'datetime 8                       no                                  (n/a)                               (n/a)
	
	'**%FindByCover: Returns TRUE or FALSE if the records exists in the table "Sumcov_apl_checkcap"
	'%FindByCover: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Sumcov_apl_checkcap"
	Public Function FindByCover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaSumcov_apl_checkcap As eRemoteDB.Execute
		
		On Error GoTo FindByCover_err
		
		lrecreaSumcov_apl_checkcap = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.reaSumcov_apl_checkcap'
		'**+Data read on 04/18/2001 04:34:30 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaSumcov_apl_checkcap'
		'+ Información leída el 18/04/2001 04:34:30 p.m.
		
		With lrecreaSumcov_apl_checkcap
			.StoredProcedure = "reaSumcov_apl_checkcap"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindByCover = True
				nModulec = .FieldToClass("nModulec")
				nCover = .FieldToClass("nCover")
				dEffecdate = .FieldToClass("dEffecdate")
				nSumins_co = .FieldToClass("nSumins_co")
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nSumins_rat = .FieldToClass("nSumins_rat")
				dNulldate = .FieldToClass("dNulldate")
				.RCloseRec()
			Else
				FindByCover = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSumcov_apl_checkcap may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSumcov_apl_checkcap = Nothing
		
FindByCover_err: 
		If Err.Number Then
			FindByCover = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Find: Returns TRUE or FALSE if the records exists in the table "Sumcov_apl"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Sumcov_apl"
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nSumins_co As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaSumcov_apl As eRemoteDB.Execute
		
		lrecreaSumcov_apl = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		'**+Parameters definition for the stored procedure 'insudb.reaSumcov_apl'
		'**+Data read on 05/05/2001 12:25:59
		'+Definición de parámetros para stored procedure 'insudb.reaSumcov_apl'
		'+Información leída el 05/05/2001 12:25:59
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or Me.nSumins_co <> nSumins_co Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nModulec = nModulec
			Me.nCover = nCover
			Me.nSumins_co = nSumins_co
			Me.dEffecdate = dEffecdate
			With lrecreaSumcov_apl
				.StoredProcedure = "reaSumcov_apl"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSumins_co", nSumins_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nSumins_rat = .FieldToClass("nSumins_rat")
					dNulldate = .FieldToClass("dNulldate")
					Find = True
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		'UPGRADE_NOTE: Object lrecreaSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSumcov_apl = Nothing
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'%Find_Val: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Sumcov_apl"
	Public Function Find_Val(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaSumcov_apl As eRemoteDB.Execute
		
		lrecreaSumcov_apl = New eRemoteDB.Execute
		
		On Error GoTo Find_Val_Err
		'+Definición de parámetros para stored procedure 'reaSumcov_apl_val'
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or Me.dEffecdate <> dEffecdate Then
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nModulec = nModulec
			Me.nCover = nCover
			Me.dEffecdate = dEffecdate
			With lrecreaSumcov_apl
				.StoredProcedure = "reaSumcov_apl_val"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					nSumins_rat = .FieldToClass("nSumins_rat")
					dNulldate = .FieldToClass("dNulldate")
					Find_Val = True
					.RCloseRec()
				End If
			End With
		Else
			Find_Val = True
		End If
		'UPGRADE_NOTE: Object lrecreaSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSumcov_apl = Nothing
Find_Val_Err: 
		If Err.Number Then
			Find_Val = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**% FindExist: verify if exist any record in the table Sumcov_apl for the coverage on treatment
	'%FindExist: Permite verificar si existe algun registro en la tabla Sumcov_apl
	'%para la cobertura en tratamiento
	Public Function FindExist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaSumcov_apl_2 As eRemoteDB.Execute
		
		lrecreaSumcov_apl_2 = New eRemoteDB.Execute
		
		On Error GoTo FindExist_Err
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nModulec <> nModulec Or Me.nCover <> nCover Or Me.dEffecdate <> dEffecdate Then
			
			'**+Parameters definition for the stored procedure 'insudb.reaSumcov_apl_2'
			'**+Data read on 05/05/2001 12:44:05
			'+Definición de parámetros para stored procedure 'insudb.reaSumcov_apl_2'
			'+Información leída el 05/05/2001 12:44:05
			
			With lrecreaSumcov_apl_2
				.StoredProcedure = "reaSumcov_apl_2"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					FindExist = True
					.RCloseRec()
				End If
			End With
		Else
			FindExist = True
		End If
		
		'UPGRADE_NOTE: Object lrecreaSumcov_apl_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSumcov_apl_2 = Nothing
		
FindExist_Err: 
		If Err.Number Then
			FindExist = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: Updates records in the table "Sumcov_apl".  It returns TRUE or FALSE depending on the execution of the stored procedure.
	'%Update: Este método se encarga de actualizar registros en la tabla "Sumcov_apl". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Object
		Dim lrecinsSumcov_apl As eRemoteDB.Execute
		
		lrecinsSumcov_apl = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+Parameters definition for the stored procedure 'insudb.insSumcov_apl'
		'**+Data read on 05/07/2001 10:21:43
		'+Definición de parámetros para stored procedure 'insudb.insSumcov_apl'
		'+Información leída el 07/05/2001 10:21:43
		
		With lrecinsSumcov_apl
			.StoredProcedure = "insSumcov_apl"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_co", nSumins_co, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Process", IIf(nSumins_rat <> 0, 1, 2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSumins_rat", nSumins_rat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsSumcov_apl = Nothing
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: Updates records in the table "Sumcov_apl".  It returns TRUE or FALSE depending on the execution of the stored procedure.
	'%Update: Este método se encarga de actualizar registros en la tabla "Sumcov_apl". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Object
		Dim lrecinsSumcov_apl As eRemoteDB.Execute
		
		lrecinsSumcov_apl = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'+Definición de parámetros para stored procedure 'insSumcov_apl_del'
		
		With lrecinsSumcov_apl
			.StoredProcedure = "delSumcov_apl"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsSumcov_apl = Nothing
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		
	End Function
	
	
	'**%insValDP052A: Validates the page "DP052A" as described in the functional specifications
	'%InsValDP052A: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "DP052A"
	Public Function insValDP052A(ByVal nSumins_rat As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValDP052A_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		
		If nSumins_rat <> eRemoteDB.Constants.intNull Then
			lclsValField.objErr = lclsErrors
			lclsValField.Max = 100
			lclsValField.Min = 0
			lclsValField.Descript = "Porcentaje "
			Call lclsValField.ValNumber(nSumins_rat,  , eFunctions.valField.eTypeValField.ValAll)
		End If
		insValDP052A = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValDP052A_Err: 
		If Err.Number Then
			insValDP052A = "insValDP052A: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'**%insPostDP052A: Updates the database (as described in the functional specifications)
	'**%for the page "DP052A"
	'%insPostDP052A: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP052A"
	Public Function insPostDP052A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nSumins_co As Integer, ByVal dEffecdate As Date, ByVal nSumins_rat As Double, ByVal nUsercode As Integer) As Object
		On Error GoTo insPostDP052A_Err
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.nSumins_co = nSumins_co
			.dEffecdate = dEffecdate
			.nSumins_rat = nSumins_rat
			.nUsercode = nUsercode
			.nSumins_rat = IIf(nSumins_rat <> 0, nSumins_rat, 0)
			insPostDP052A = Update
		End With
		
insPostDP052A_Err: 
		If Err.Number Then
			insPostDP052A = False
		End If
		On Error GoTo 0
	End Function
	'**%insPostDP052A: Updates the database (as described in the functional specifications)
	'**%for the page "DP052A"
	'%insPostDP052A: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP052A"
	Public Function DeleteDP052A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Object
		On Error GoTo DeleteDP052A_Err
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			DeleteDP052A = Delete
		End With
		
DeleteDP052A_Err: 
		If Err.Number Then
			DeleteDP052A = False
		End If
		On Error GoTo 0
	End Function
	
	'**% InitializeValues: the publics variables values of the class will be iniciated
	'% IniatializeValues: se inicializan los valores de las variables públicas de la clase
	Private Sub IniatializeValues()
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		dEffecdate = CDate(Nothing)
		nSumins_co = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nSumins_rat = eRemoteDB.Constants.intNull
		dNulldate = CDate(Nothing)
	End Sub
	'**% Class_Initialized: the class values will be iniciated
	'% Class_Initialize: se inicializan los valores de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call IniatializeValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






