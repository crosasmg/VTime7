Option Strict Off
Option Explicit On
Public Class Conmutativ
	'%-------------------------------------------------------%'
	'% $Workfile:: Conmutativ.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Define the variable that contains the conmutatives values for each age.
	'- Se definen las variables que contienen las valores conmutativos para cada edad.
	
	'**- Define the columns of the Conmutativ table.
	'- Se definen las columnas de la Tabla Conmutativ.
	
	'+ Column_name                     Type      Computed     Length    Prec  Scale Nullable    TrimTrailingBlanks   FixedLenNullInSource
	'-----------------------------    --------- ------------ --------- ----- ----- ----------- -------------------- -----------------------
	Public nInterest As Double 'decimal    no         5           4     2     no              (n/a)                (n/a)
	Public nAge As Integer 'smallint   no         2           5     0     no              (n/a)                (n/a)
	Public nMonth As Integer 'smallint   no         2           5     0     no              (n/a)                (n/a)
	Public nConmu_cx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_dx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_mx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_nx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_rx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_sx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_tx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nDeath_dx As Double 'decimal    no         9           12    0     yes             (n/a)                (n/a)
	Public nDeath_qx As Double 'decimal    no         5           9     5     yes             (n/a)                (n/a)
	Public nLive_lx As Double 'decimal    no         9           12    4     yes             (n/a)                (n/a)
	Public nLiver_px As Double 'decimal    no         5           9     8     yes             (n/a)                (n/a)
	Public nUsercode As Integer 'smallint   no         2           5     0     no              (n/a)                (n/a)
	Public nConmu_vx As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	Public nConmu_ex As Double 'decimal    no         9           15    5     yes             (n/a)                (n/a)
	
	'- Variable auxiliares
	Public bytAge As Byte
	Public nConm_D As Double
	Public nConm_C As Double
	Public nConm_N As Double
	Public nConm_M As Double
	Public nConm_S As Double
	Public nConm_R As Double
	Public nConm_T As Double
	Public sMortalco As String
	Public mdblInt As Double
	Public nConm_V As Double
	Public nConm_E As Double
	
	'- Muertos a la edad X
	Public ndx As Double
	
	'- Vivos a la edad X
	Public nlx As Double
	
	'- Probabildad de vida a la edad X
	Public npx As Double
	
	'- Probabildad de muerte a la edad X
	Public nqx As Double
	
	'- Se define la variable que indica si existen valores conmutativos para la tabla e interés especificados.
	Public mblnExistPrevInf As Boolean
	
	'- Se define la constante que contiene el máximo valor para el número de muertes.
	Const MAXDEATH As Double = 999999999999#
	
	'- Se define la constante que contiene el máximo valor para un conmutativo.
	Const MAXCONM As Double = 9999999999.99999
	
	'**% ADD: This method is in charge of adding new records to the table "Conmutativ".  It returns TRUE or FALSE
	'**% depending on whether the stored procedure executed correctly.
	'% ADD: Este método se encarga de agregar nuevos registros a la tabla "Conmutativ". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lrecConmutativ As eRemoteDB.Execute
		
		lrecConmutativ = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'**+ Parameter definition for stored procedure 'insudb.insMortalityCre'
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		With lrecConmutativ
			.StoredProcedure = "CreConmutativ"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", mdblInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", bytAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_cx", nConm_C, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_dx", nConm_D, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_mx", nConm_M, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_nx", nConm_N, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_rx", nConm_R, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_sx", nConm_S, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_tx", nConm_T, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeath_dx", ndx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeath_qx", nqx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLive_lx", nlx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 4, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLiver_px", npx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_vx", nConm_V, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_ex", nConm_E, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecConmutativ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecConmutativ = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**% AddConm_master: Adds the information in the conm_Master table.
	'% AddConm_master: Permite registrar la información en la tabla conm_Master.
	Public Function AddConm_master() As Boolean
		Dim lrecConm_master As eRemoteDB.Execute
		
		lrecConm_master = New eRemoteDB.Execute
		
		On Error GoTo AddConm_master_err
		
		'**+ Parameter definition for stored procedure 'insudb.insMortalityCre'
		'+ Definición de parámetros para stored procedure 'insudb.insMortalityCre'
		With lrecConm_master
			.StoredProcedure = "insCreConm_Master"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", mdblInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDelPrevInf", IIf(mblnExistPrevInf, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			AddConm_master = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecConm_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecConm_master = Nothing
		
AddConm_master_err: 
		If Err.Number Then
			AddConm_master = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValDP015_K: This method validates the header section of the page "DP015_K" as described in the
	'**% functional specifications
	'% InsValDP015_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'% descritas en el funcional de la ventana "DP015_K"
	Public Function insValDP015_k(ByVal sCodispl As String, Optional ByVal sMortalco As String = "", Optional ByVal nInterest As Double = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsMortality As eProduct.Mortality
		Dim lcolConmutativs As eProduct.Conmutativs
		Dim lblnError As Boolean
		
		On Error GoTo insValDP015_k_Err
		lobjErrors = New eFunctions.Errors
		
		'**+ Validate the field "Table"
		'+ Se valida el campo "Tabla".
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sMortalco) Or IsNothing(sMortalco) Or Trim(sMortalco) = String.Empty Or Trim(sMortalco) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 11169)
			lblnError = True
		Else
			lcolConmutativs = New eProduct.Conmutativs
			If Not lcolConmutativs.Find(sMortalco, True) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11006)
				lblnError = True
			End If
		End If
		
		'**+ Make the validation of the "Interest" field
		'+ Se realiza la validación del campo "Interés".
		
		If nInterest = 0 Or nInterest = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9045)
			lblnError = True
		End If
		
		If Not lblnError Then
			lclsMortality = New eProduct.Mortality
			mblnExistPrevInf = lclsMortality.insReaConm_master(sMortalco, nInterest)
			
			If mblnExistPrevInf Then
				Call lobjErrors.ErrorMessage(sCodispl, 11202)
			End If
		End If
		
		insValDP015_k = lobjErrors.Confirm
		
		
insValDP015_k_Err: 
		If Err.Number Then
			insValDP015_k = insValDP015_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
		'UPGRADE_NOTE: Object lcolConmutativs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolConmutativs = Nothing
		On Error GoTo 0
	End Function
	
	'**% insPostDP015: This function is in charge of keeping the data in the tables, in this case Conm_master
	'**% and conmutativ.
	'% insPostDP015: Esta función se encarga de almacenar los datos en las tablas, en este caso Conm_master y
	'% Conmutativ.
	Public Function insPostDP015(ByVal sMortalco As String, Optional ByVal nInterest As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		Dim lcolConmutativs As eProduct.Conmutativs
		
		On Error GoTo insPostDP015_err
		insPostDP015 = True
		lcolConmutativs = New eProduct.Conmutativs
		With lcolConmutativs
			If .Find(sMortalco, True) Then
			End If
			.sMortalco = sMortalco
			.mdblInt = nInterest
			.nUsercode = nUsercode
		End With
		
		insPostDP015 = lcolConmutativs.insCalConmutativ
		
insPostDP015_err: 
		If Err.Number Then
			insPostDP015 = False
		End If
		'UPGRADE_NOTE: Object lcolConmutativs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolConmutativs = Nothing
		On Error GoTo 0
	End Function
	
	'**% UpdateConmutativ: Updates the information of the modified conmutatives vales
	'% UpdateConmutativ: Permite actualizar la información de los valores conmutativos modificados.
	Public Function UpdateConmutativ() As Boolean
		Dim lrecupdconmutativ As eRemoteDB.Execute
		
		lrecupdconmutativ = New eRemoteDB.Execute
		
		On Error GoTo UpdateConmutativ_Err
		
		'**+ Parameter definition for stored procedure 'insudb.updConmutativ'
		'+ Definición de parámetros para stored procedure 'insudb.updConmutativ'
		With lrecupdconmutativ
			.StoredProcedure = "updConmutativ"
			
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge", nAge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth", nMonth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_cx", nConmu_cx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_dx", nConmu_dx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_mx", nConmu_mx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_nx", nConmu_nx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_rx", nConmu_rx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_sx", nConmu_sx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_tx", nConmu_tx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeath_dx", nDeath_dx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLiver_px", nLiver_px, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 8, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_vx", nConmu_vx, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConmu_ex", nConmu_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 20, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateConmutativ = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdconmutativ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdconmutativ = Nothing
		
UpdateConmutativ_Err: 
		If Err.Number Then
			UpdateConmutativ = False
		End If
		On Error GoTo 0
	End Function
	
	'**% DeleteConmutativ: Delete the information of the conmutatives vales
	'% DeleteConmutativ: Permite eliminar la información de los valores conmutativos modificados.
	Public Function DeleteConmutativ() As Boolean
		Dim lrecdelconmutativ As eRemoteDB.Execute
		
		lrecdelconmutativ = New eRemoteDB.Execute
		
		On Error GoTo DeleteConmutativ_Err
		
		'**+ Parameter definition for stored procedure 'delconmutativ'
		'+ Definición de parámetros para stored procedure 'delconmutativ'
		With lrecdelconmutativ
			.StoredProcedure = "delconmutativ"
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", mdblInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			DeleteConmutativ = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecdelconmutativ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelconmutativ = Nothing
		
DeleteConmutativ_Err: 
		If Err.Number Then
			DeleteConmutativ = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValDP016_K: This method validates the header section of the page "DP016_K" as described in the
	'**% functional specifications
	'% InsValDP016_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'% descritas en el funcional de la ventana "DP016_K"
	Public Function insValDP016_k(ByVal sCodispl As String, Optional ByVal sMortalco As String = "", Optional ByVal nInterest As Double = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsMortality As eProduct.Mortality
		Dim lblnError As Boolean
		
		lobjErrors = New eFunctions.Errors
		lclsMortality = New eProduct.Mortality
		
		insValDP016_k = String.Empty
		
		On Error GoTo insValDP016_k_Err
		
		lblnError = False
		
		'**+ Validate the "Table" field.
		'+ Se valida el campo "Tabla".
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sMortalco) Or IsNothing(sMortalco) Or Trim(sMortalco) = String.Empty Or Trim(sMortalco) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 11169)
			lblnError = True
		Else
			If Not lclsMortality.insValMort_master(sMortalco, "1") Then
				Call lobjErrors.ErrorMessage(sCodispl, 11006)
				lblnError = True
			End If
		End If
		
		'**+ Make the validations of the "Interest" field.
		'+ Se realizan las validaciones del campo "Interés".
		If nInterest = 0 Or nInterest = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 9045)
			lblnError = True
		End If
		
		If Not lblnError Then
			If Not lclsMortality.insReaConm_master(sMortalco, nInterest) Then
				Call lobjErrors.ErrorMessage(sCodispl, 11039)
			End If
		End If
		
		insValDP016_k = lobjErrors.Confirm
		
insValDP016_k_Err: 
		If Err.Number Then
			insValDP016_k = "insValDP016_k: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
		On Error GoTo 0
	End Function
	
	'**% insValDP016: This method validates the page "DP016" as described in the functional specifications
	'% InsValDP016: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'% de la ventana "DP016"
	Public Function insValDP016(ByVal sCodispl As String, Optional ByVal nDeath_dx As Double = 0, Optional ByVal nLiver_px As Double = 0, Optional ByVal nConmu_dx As Double = 0, Optional ByVal nConmu_cx As Double = 0, Optional ByVal nConmu_nx As Double = 0, Optional ByVal nConmu_mx As Double = 0, Optional ByVal nConmu_sx As Double = 0, Optional ByVal nConmu_rx As Double = 0, Optional ByVal nConmu_tx As Double = 0, Optional ByVal nConmu_vx As Double = 0, Optional ByVal nConmu_ex As Double = 0) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.valField
		Dim lblnError As Boolean
		
		On Error GoTo insValDP016_Err
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.valField
		'**+ Validation of the field " Number of deaths to the d(x) year"
		'+ Validación del campo "Número de muertes al año d(x)".
		If nDeath_dx = 0 Or nDeath_dx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Número de muertes al año d(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXDEATH
				
				'**+ Verify that it is not empty, and that it is inbside of the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nDeath_dx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the "Annual life probability P(x)"
		'+ Se realizan las validaciones de la "Probabilidad de vida anual P(x)".
		If nLiver_px = 0 Or nLiver_px = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Probabilidad de vida anual p(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = 1
				
				'**+ Verify that it is not empty, and that it is inside of the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nLiver_px,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validation of the field "Conmutative D(x)"
		'+ Se realizan las validaciones del campo "Conmutativo D(x)".
		If nConmu_dx = 0 Or nConmu_dx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo D(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_dx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative C (x)"
		'+ Se realizan las validaciones del campo "Conmutativo C(x)".
		If nConmu_cx = 0 Or nConmu_cx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo C(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_cx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative N(x)"
		'+ Se realizan las validaciones del campo "Conmutativo N(x)".
		If nConmu_nx = 0 Or nConmu_nx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo N(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_nx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative M(x)"
		'+ Se realizan las validaciones del campo "Conmutativo M(x)".
		If nConmu_mx = 0 Or nConmu_mx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo M(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_mx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative S (x)".
		'+ Se realizan las validaciones del campo "Conmutativo S(x)".
		If nConmu_sx = 0 Or nConmu_sx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo S(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_sx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative R (x)"
		'+ Se realizan las validaciones del campo "Conmutativo R(x)".
		If nConmu_rx = 0 Or nConmu_rx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo R(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_rx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative T(x)".
		'+ Se realizan las validaciones del campo "Conmutativo T(x)".
		If nConmu_tx = 0 Or nConmu_tx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo T(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_tx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative E(x)"
		'+ Se realizan las validaciones del campo "Conmutativo E(x)".
		If nConmu_ex = 0 Or nConmu_ex = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo E(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_ex,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		'**+ Make the validations of the field "Conmutative V(x)"
		'+ Se realizan las validaciones del campo "Conmutativo V(x)".
		If nConmu_vx = 0 Or nConmu_vx = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 3749,  , eFunctions.Errors.TextAlign.LeftAling, "Conmutativo V(x):")
		Else
			With lobjValues
				.objErr = lobjErrors
				
				.EqualMin = True
				.EqualMax = True
				.Min = 0
				.Max = MAXCONM
				
				'**+ Verify that it is not empty, and that it is inside the correct range.
				'+ Se verifica que no esté vacía, y que se encuentre dentro del rango correcto.
				If Not .ValNumber(nConmu_vx,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				End If
			End With
		End If
		
		insValDP016 = lobjErrors.Confirm
		
insValDP016_Err: 
		If Err.Number Then
			insValDP016 = "insValDP016: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		On Error GoTo 0
	End Function
	
	'**% insPostDP016: This function is in charge of modifing the data in the tables, in this case
	'% insPostDP016: Esta función se encarga de modificar los datos en las tablas, en este caso
	Public Function insPostDP016(ByVal sMortalco As String, Optional ByVal nInterest As Double = 0, Optional ByVal nAge As Integer = 0, Optional ByVal nMonth As Integer = 0, Optional ByVal nConmu_dx As Double = 0, Optional ByVal nConmu_cx As Double = 0, Optional ByVal nConmu_mx As Double = 0, Optional ByVal nConmu_nx As Double = 0, Optional ByVal nConmu_rx As Double = 0, Optional ByVal nConmu_sx As Double = 0, Optional ByVal nConmu_tx As Double = 0, Optional ByVal nDeath_dx As Double = 0, Optional ByVal nLiver_px As Double = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nConmu_vx As Double = 0, Optional ByVal nConmu_ex As Double = 0) As Boolean
		
		On Error GoTo insPostDP016_Err
		
		With Me
			.sMortalco = sMortalco
			.nInterest = nInterest
			.nAge = nAge
			.nMonth = nMonth
			.nConmu_dx = nConmu_dx
			.nConmu_cx = nConmu_cx
			.nConmu_mx = nConmu_mx
			.nConmu_nx = nConmu_nx
			.nConmu_rx = nConmu_rx
			.nConmu_sx = nConmu_sx
			.nConmu_tx = nConmu_tx
			.nDeath_dx = nDeath_dx
			.nLiver_px = nLiver_px
			.nUsercode = nUsercode
			.nConmu_vx = nConmu_vx
			.nConmu_ex = nConmu_ex
		End With
		
		insPostDP016 = UpdateConmutativ
		
insPostDP016_Err: 
		If Err.Number Then
			insPostDP016 = False
		End If
		On Error GoTo 0
	End Function
End Class






