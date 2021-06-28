Option Strict Off
Option Explicit On
Public Class Tab_winpro
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_winpro.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Property accourding to the table in the system on 03/21/2001
	'**- The key fields of the table correspond to: sBranchtype and nSequence.
	'- Propiedades según la tabla en el sistema al 21/03/2001.
	'- Los campos llave de la tabla corresponden a: sBranchtype y nSequence.
	
	'   Column_name                    Type      Computed  Length      Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	Public sBranchtype As String 'char        no        1                       no           no                  no
	Public nSequence As Integer 'smallint    no        2           5     0     no           (n/a)               (n/a)
	Public sCodispl As String 'char        no        8                       yes          no                  yes
	Public sRequire As String 'char        no        1                       yes          no                  yes
	Public sChecked As String 'char        no        1                       yes          no                  yes
	Public nUsercode As Integer 'smallint    no        2           5     0     yes          (n/a)               (n/a)
	
	'**- Auxiliary variables
	'- Variables auxiliares
	Public sCodisp As String
	Public sDescript As String
	Public sShort_des As String
	Public nWindowty As Integer
	
	'**%Find: Returns TRUE or FALSE if the records exists in the table "WindowsProd"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "WindowsProd"
	Public Function Find(ByVal sBranchtype As String, ByVal sCodispl As String) As Boolean
		On Error GoTo Find_Err
		
		Dim lrecTab_winpro As eRemoteDB.Execute
		
		lrecTab_winpro = New eRemoteDB.Execute
		
		With lrecTab_winpro
			.StoredProcedure = "reaWindowsProd"
			.Parameters.Add("sBranchType", sBranchtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sRequire = .FieldToClass("sRequire")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_winpro may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_winpro = Nothing
	End Function
	
	'**%ADD: Adds new records to the table "Tab_winpro".  It returns TRUE or FALSE if stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tab_winpro". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add(ByVal sBranchtype As String, ByVal nSequence As Integer, ByVal sCodispl As String, ByVal sRequire As String, ByVal sChecked As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo Add_err
		
		Dim lrecTime As eRemoteDB.Execute
		
		lrecTime = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.creTab_winpro'
		'**+ Data read on 03/08/2000 02:11:49 PM
		'+ Definición de parámetros para stored procedure 'insudb.creTab_winpro'
		'+ Información leída el 08/03/2000 02:11:49 PM
		
		With lrecTime
			.StoredProcedure = "creTab_winpro"
			.Parameters.Add("sBranchtype", sBranchtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'**% Update: Updates the correspondent data for one client, year and specific concept
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecTime = New eRemoteDB.Execute
		With lrecTime
			.StoredProcedure = "insupdTab_winpro"
			.Parameters.Add("sBranchType", sBranchtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChecked", sChecked, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'**% Delete: delete the correspondent data for one client, year and specific concept
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete(ByVal sBranchtype As String) As Boolean
		On Error GoTo Delete_Err
		
		Dim lrecTime As eRemoteDB.Execute
		
		lrecTime = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.delTab_winpro'
		'**+ Data read on 03/08/2000 02:09:15 PM
		'+ Definición de parámetros para stored procedure 'insudb.delTab_winpro'
		'+ Información leída el 08/03/2000 02:09:15 PM
		
		With lrecTime
			.StoredProcedure = "delTab_winpro"
			.Parameters.Add("sBranchtype", sBranchtype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	'%insPostMDP001: Esta función se encarga de hacer las modificacion que haya
	'* solicitado el usario en tab_winpro
	Public Function insPostMDP001(ByVal sBrancht As String, ByVal nSequence As Integer, ByVal sCodispl As String, ByVal sRequire As String, ByVal sChecked As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMDP001_err
		
		With Me
			.sBranchtype = sBrancht
			.nSequence = nSequence
			.sCodispl = sCodispl
			.sRequire = sRequire
			.sChecked = sChecked
			.nUsercode = nUsercode
		End With
		insPostMDP001 = Update()
		
insPostMDP001_err: 
		If Err.Number Then
			insPostMDP001 = False
		End If
		On Error GoTo 0
	End Function
	
	'%insValMDP001_K: Esta función se encarga de validar los datos introducidos en la zona de
	'%encabezado de la forma.
	Public Function insValMDP001_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nSeleted As Integer = 0, Optional ByVal nBranchType As Integer = 0) As String
		On Error GoTo insValMDP001_K_Err
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se da inicio al ciclo de validaciones.
		If nBranchType = eRemoteDB.Constants.intNull Or nBranchType = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012)
		End If
		
		insValMDP001_K = lclsErrors.Confirm
		
insValMDP001_K_Err: 
		If Err.Number Then
			insValMDP001_K = "insValMDP001_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insValMDP001: Esta función se encarga de validar los datos introducidos en la zona de
	'%detalle de la forma.
	Public Function insValMDP001(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nSel As Integer = 0) As String
		On Error GoTo insValMDP001_Err
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		'+ Se da inicio al ciclo de validaciones.
		If nSel = eRemoteDB.Constants.intNull Or nSel = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 99007)
		End If
		
		insValMDP001 = lclsErrors.Confirm
		
insValMDP001_Err: 
		If Err.Number Then
			insValMDP001 = "insValMDP001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
End Class






