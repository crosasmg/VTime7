Option Strict Off
Option Explicit On
Public Class Mortality
	'%-------------------------------------------------------%'
	'% $Workfile:: Mortality.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Defines the properties of the Mortality Table
	'- Se definen las propiedades de la Tabla Mortality.
	
	'+  Column_name      Type                     Computed   Length      Prec  Scale Nullable  TrimTrailingBlanks     FixedLenNullInSource
	'+  ---------------- ----------------------- ---------- ----------- ----- ----- --------- ---------------------- ----------------------
	Public sMortalco As String 'char          no         6                       no        no                     no
	Public nAge As Integer 'smallint      no         2           5     0     no        (n/a)                  (n/a)
	Public nDeath_dx As Double 'decimal       no         9           12    0     yes       (n/a)                  (n/a)
	Public nDeath_qx As Double 'decimal       no         5           9     5     yes       (n/a)                  (n/a)
	Public nLive_lx As Double 'decimal       no         9           12    4     yes       (n/a)                  (n/a)
	Public nUsercode As Integer 'smallint      no         2           5     0     no        (n/a)                  (n/a)
	Public nMonth As Integer 'number        no         2           0     5     no
	
	'**- Defines the auxiliary properties to use it in DP013.
	'- Se definen las propiedades auxiliares a ser utilizadas en DP013.
	Public nStatusInstance As Integer
	Public sInsert As String
	Public sUpdateF As String
	Public sUpdate As String
	Public nIni_Age As Integer
	Public nEnd_age As Integer
	Public sSel As String
	Public nExist As Integer
	Public nDeath_qxAux As Double
	
	'**- Defines the global constant to the management of the record status of the
	'**- mortality table
	'- Se define la constante global para el manejo de los estados de los registros de la
	'- las tablas de mortalidad.
	Public Enum eStatregt
		cstrActive = 1 '**Active
		'Activo
		cstrInInstallProcess = 2 '**IN process of installation
		'En proceso de instalación"
		cstrWithoutAccess = 3 '**Without access
		'Acceso restringido
	End Enum
	
	'**- Declares the variable mVarsMortalco that mantein "the property code value"
	'**- in the mortality table.
	'- Se declara la variable mVarsMortalco que mantienen el valor de la propiedad
	'- código de la tabla de mortalidad.
	Private mVarsMortalco As String
	
	'**- Declares the variable mVarsStatregt that mantains "the property status value"
	'**- in the mortality table.
	'- Se declara la variable mVarsStatregt que mantienen el valor de la propiedad
	'- estado de la tabla de mortalidad.
	Private mVarsStatregt As String
	
	'**- Constant that will be use in the validation of the death probability
	'- Constantes a ser utilizadas en la validación de la probabilidad de muerte.
	Const MaxQx As Double = 1#
	Const MinQx As Double = 0#
	
	'**% Add: Record the parameters information for the mortality table.
	'% Add: Permite registrar la información de los parámetros para la tabla de mortalidad.
	Public Function Add() As Boolean
		Dim lrecMortality As eRemoteDB.Execute
		
		lrecMortality = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'**+ Parameters definition for the stored procedure 'insudb.insMortalityCre'
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		If insValMort_master(sMortalco, String.Empty) Then
			sInsert = "NO"
			nLive_lx = 0
		Else
			sInsert = "YES"
		End If
		
		With lrecMortality
			.StoredProcedure = "insMortalityCre"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInsert", sInsert, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeath_qx", nDeath_qx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLive_lx", nLive_lx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUpdateF", "NO", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMortality = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update: Update the parameters information  for the mortality table.
	'% Update: Permite actualizar la información de los parámetros para la tabla de mortalidad.
	Public Function Update() As Boolean
		Dim lrecUpdMortality As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdMortality = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure insudb.insMortalityCre'
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		With lrecUpdMortality
			.StoredProcedure = "insMortalityUpd"
			.Parameters.Add("nIni_Age", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Age", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUpdate", "NO", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeath_qx", nDeath_qx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLive_lx", nLive_lx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", "YES", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Update = Update_Live(sMortalco, nLive_lx, nUsercode)
			End If
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecUpdMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdMortality = Nothing
	End Function
	
	'**% Delete:  Delete the parameters information for the mortality table.
	'% Delete: Permite borrar la información de los parámetros para la tabla de mortalidad.
	Public Function Delete(ByVal sMortalco As String, ByVal nAgeIni As Integer, ByVal nAgeEnd As Integer) As Boolean
		Dim lrecDelMortality As eRemoteDB.Execute
		
		lrecDelMortality = New eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.delMortality'
		'+ Definición de parámetros para stored procedure 'insudb.delMortality'
		With lrecDelMortality
			.StoredProcedure = "delMortality"
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeIni", nAgeIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgeEnd", nAgeEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDelMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelMortality = Nothing
	End Function
	
	'**% insCalculateDx:  Calculate the death probabilities for each age of the mortality
	'**% table in process.
	'% insCalculateDx: Permite calcular las probabilidades de muerte para cada edad de la tabla de
	'% Mortalidad en proceso.
	Public Function insCalculateDx(ByVal sMortalco As String, ByVal nLive_lx As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecCalcMortality As eRemoteDB.Execute
		
		On Error GoTo insCalculateDx_Err
		
		lrecCalcMortality = New eRemoteDB.Execute
		
		insCalculateDx = Update_Live(sMortalco, nLive_lx, nUsercode)
		
		If insCalculateDx Then
			With lrecCalcMortality
				.StoredProcedure = "insCalculateDx"
				.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				insCalculateDx = .Run(False)
			End With
		End If
		
insCalculateDx_Err: 
		If Err.Number Then
			insCalculateDx = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecCalcMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecCalcMortality = Nothing
	End Function
	
	'**% UpdateMort_master:Modifies the status of the mortality table in the Mort_master table.
	'% UpdateMort_master: Permite modificar el estado del de la tabla de mortalidad en la tabla Mort_master.
	Public Function UpdateMort_master(ByVal sMortalco As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecUpdMort_master As eRemoteDB.Execute
		
		lrecUpdMort_master = New eRemoteDB.Execute
		
		On Error GoTo UpdateMort_master_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.insMortalityCre'
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		With lrecUpdMort_master
			.StoredProcedure = "UpdMort_master"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateMort_master = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecUpdMort_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdMort_master = Nothing
		
UpdateMort_master_Err: 
		If Err.Number Then
			UpdateMort_master = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValDP013_K: Makes the validation of the Header fields in the window DP013 - Parameters for the mortality table.
	'% insValDP013_K: Realiza la validación de los campos del Header de la ventana DP013 - Parámetros para la tabla de mortalidad.
	Public Function insValDP013_K(ByVal sCodispl As String, ByVal nAction As String, Optional ByVal sMortalco As String = "") As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP013_k_Err
		lobjErrors = New eFunctions.Errors
		'**+Validate the "Table" field
		'+ Se valida el campo "Tabla".
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sMortalco) Or IsNothing(sMortalco) Or Trim(sMortalco) = String.Empty Or Trim(sMortalco) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 11169)
		Else
			If insValMort_master(sMortalco, CStr(eStatregt.cstrActive)) Then
				If nAction = CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
					Call lobjErrors.ErrorMessage(sCodispl, 11099)
				End If
			Else
				If nAction <> CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
					Call lobjErrors.ErrorMessage(sCodispl, 11006)
				End If
			End If
		End If
		
		insValDP013_K = lobjErrors.Confirm
		
insValDP013_k_Err: 
		If Err.Number Then
			insValDP013_K = insValDP013_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValMort_master: the objetive of this method is to verify if  the mortality table
	'**% indcated exists. In case it exists return the table data in the respective properties.
	'**% Parameters: sMortalco ---> Code of the mortality table
	'**%             sStatregt ---> Status of the record to search
	'% insValMort_master: El objetivo de este metodo es verificar si existe la tabla de mortalidad
	'% indicada. En caso de existir retorna los datos de la tabla en sus respectivas propiedades.
	'% Parametros: sMortalco ---> Código de la tabla de mortalidad.
	'%             sStatregt ---> Estado del registro a buscar.
	Public Function insValMort_master(ByVal sMortalco As String, ByVal sStatregt As String) As Boolean
		Dim lrecMort_master As New eRemoteDB.Execute
		
		On Error GoTo insValMort_master_err
		
		insValMort_master = False
		
		With lrecMort_master
			.StoredProcedure = "reaMort_master"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If (.Run) Then
				insValMort_master = True
				
				mVarsMortalco = .FieldToClass("sMortalco")
				mVarsStatregt = .FieldToClass("sStatregt")
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecMort_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMort_master = Nothing
		
insValMort_master_err: 
		If Err.Number Then
			insValMort_master = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValDP013: Makes the validation of the punctual fields of the transaction Detail DP013 - Parameters for the mortality table.
	'% insValDP013: Realiza la validación de los campos puntuales del Detalle de la transacción DP013 - Parámetros para la tabla de
	'% mortalidad.
	Public Function insValDP013(ByVal sCodispl As String, ByVal sMortalco As String, Optional ByVal nInit_age As Integer = 0, Optional ByVal nEnd_age As Integer = 0, Optional ByVal nLive_lx As Double = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lrecMortality As eRemoteDB.Execute
		Dim llngCount As Integer
		
		lobjErrors = New eFunctions.Errors
		
		insValDP013 = String.Empty
		
		On Error GoTo insValDP013_Err
		
		'**+ Makes the validation of the "Initial Age" field.
		'+ Se realizan las validaciones del campo "Edad Inicial".
		If (nInit_age > 130) Then
			Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "(Edad inicial: 0-130 años)")
		End If
		
		'**+Makes the validation of the "Final Age".
		'+ Se realizan las validaciones de la "Edad Final".
		If nEnd_age = 0 Or nEnd_age = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11110)
		Else
			If (nEnd_age > 130) Then
				Call lobjErrors.ErrorMessage(sCodispl, 12090,  , eFunctions.Errors.TextAlign.LeftAling, "(Edad final: 0-130 años)")
			Else
				If (nEnd_age <= nInit_age) Then
					Call lobjErrors.ErrorMessage(sCodispl, 11036)
				Else
					If (nEnd_age - nInit_age) > 115 Then
						Call lobjErrors.ErrorMessage(sCodispl, 11177)
					End If
				End If
			End If
		End If
		
		'**+ Makes the validation of "Alive number".
		'+ Se realizan las validaciones de "Número de vivos".
		If nLive_lx = 0 Or nLive_lx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage("DP013", 11401)
		End If
		
		llngCount = insValMortality(sMortalco)
		
		If insReaConm_master(sMortalco, 0) Then
			Call lobjErrors.ErrorMessage(sCodispl, 11403)
		End If
		
		insValDP013 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		
insValDP013_Err: 
		If Err.Number Then
			insValDP013 = insValDP013 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValDP013UPD: makes the validation of the  window PopUp fields-
	'% insValDP013UPD: Realiza la validación de los campos de la ventana PopUp.
	Public Function insValDP013UPD(ByVal sCodispl As String, Optional ByVal nDeath_qx As Double = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.valField
		
		On Error GoTo insValDP013UPD_Err
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.valField
		'**+ Makes the validation of the field "Death Probability"
		'+ Se realizan las validaciones del campo "Probabilidad de muerte".
		If nDeath_qx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 11037)
		End If
		
		With lobjValues
			.objErr = lobjErrors
			
			.EqualMin = True
			.EqualMax = True
			.Min = MinQx
			.Max = MaxQx
			
			'**+Verifies that is not empty and is inside the right range.
			'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
			If Not .ValNumber(nDeath_qx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
			End If
		End With
		
		insValDP013UPD = lobjErrors.Confirm
		
insValDP013UPD_Err: 
		If Err.Number Then
			insValDP013UPD = insValDP013UPD & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		On Error GoTo 0
	End Function
	
	'**% insPostDP013: this function is in charge of keeping the table data in this case Table10.
	'% insPostDP013: Esta función se encarga de almacenar los datos en las tablas, en este caso Table10.
	Public Function insPostDP013(ByVal nMainAction As Integer, ByVal sMortalco As String, Optional ByVal nAge As Integer = 0, Optional ByVal nMonth As Integer = 0, Optional ByVal nDeath_qx As Double = 0, Optional ByVal nLive_lx As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		Dim lclsProduct_ge As eProduct.Mortality
		
		On Error GoTo insPostDP013_Err
		
		insPostDP013 = True
		
		With Me
			.sMortalco = sMortalco
			.nAge = nAge
			.nMonth = nMonth
			.nDeath_qx = nDeath_qx
			.nLive_lx = nLive_lx
			.nUsercode = nUsercode
			
			Select Case nMainAction
				
				'**+If the selected option is register
				'+ Si la opción seleccionada es Registrar.
				Case 0
					insPostDP013 = Add()
					
					'**+If the selected option is Modify.
					'+ Si la opción seleccionada es Modificar.
				Case 1
					insPostDP013 = Update()
					
				Case 2
					insPostDP013 = insCalculateDx(.sMortalco, .nLive_lx, .nUsercode)
					
					If insPostDP013 Then
						insPostDP013 = UpdateMort_master(.sMortalco, .nUsercode)
					End If
			End Select
		End With
		
insPostDP013_Err: 
		If Err.Number Then
			insPostDP013 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insReaConm_master: read the information of the conmutative table
	'% insReaConm_master: Lee la información de la tabla de conmutativos
	Public Function insReaConm_master(ByRef sMortalco As String, Optional ByRef nInterest As Double = 0) As Boolean
		Dim lrecConm_master As eRemoteDB.Execute
		
		On Error GoTo insReaConmMaster_Err
		lrecConm_master = New eRemoteDB.Execute
		With lrecConm_master
			.StoredProcedure = "reaConm_master"
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insReaConm_master = True
				.RCloseRec()
			End If
		End With
		
insReaConmMaster_Err: 
		If Err.Number Then
			insReaConm_master = False
		End If
		'UPGRADE_NOTE: Object lrecConm_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecConm_master = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValMortality:  Validates the inclusion of one record at least.
	'% insValMortality: Permite validar la inclusión de al menos un registro.
	Public Function insValMortality(ByVal sMortalco As String) As Integer
		Dim lrecMortality As New eRemoteDB.Execute
		
		On Error GoTo insValMortality_Err
		
		With lrecMortality
			.StoredProcedure = "insValMortality"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insValMortality = .Parameters.Item(2).Value
				
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMortality = Nothing
		
insValMortality_Err: 
		If Err.Number Then
			insValMortality = 0
		End If
		On Error GoTo 0
	End Function
	
	'% Update_Live: actualiza el número de vivos l(x) a la edad del elemento de la tabla
	Public Function Update_Live(ByVal sMortalco As String, ByVal nLive_lx As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecMortality As eRemoteDB.Execute
		
		On Error GoTo Update_Live_err
		
		lrecMortality = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insUpdMortality'
		With lrecMortality
			.StoredProcedure = "insUpdMortality"
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLive_lx", nLive_lx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Live = .Run(False)
		End With
		
Update_Live_err: 
		If Err.Number Then
			Update_Live = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecMortality = Nothing
	End Function
End Class






