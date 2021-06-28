Option Strict Off
Option Explicit On
Public Class Tab_damage
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_damage.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**-Defined the principal properties of the correspondent class to the Tab_damage table (11/13/2001)
	'**-Column_name
	'-Se definen las propiedades principales de la clase correspondientes a la tabla Tab_damage (13/11/2001)
	'-Column_name                               Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------------------ -------------------------------------------------------------------------------------------------------------------------------- ----------------------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nBranch As Integer 'smallint  no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nDamage_cod As Integer 'smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sDescript As String 'char                                                                                                                             no                                  30                      yes                                 no                                  yes
	Public sShort_des As String 'char                                                                                                                             no                                  12                      yes                                 no                                  yes
	Public sStatregt As String 'char                                                                                                                             no                                  1                       yes                                 no                                  yes
	Public nUsercode As Integer
	
	'**%Function Find: Find the damage type.
	'%Function Find: Busca el tipo de daño.
	Public Function Find(ByVal nBranch As Integer, ByVal nDamage_cod As Integer) As Boolean
		Dim lrecreaTab_damage As eRemoteDB.Execute
		Static lblnRead As Boolean
		
		On Error GoTo Find_Err
		
		lrecreaTab_damage = New eRemoteDB.Execute
		With lrecreaTab_damage
			.StoredProcedure = "reaTab_damage"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				sStatregt = .FieldToClass("sStatregt")
				lblnRead = True
				.RCloseRec()
			Else
				lblnRead = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_damage = Nothing
		
		Find = lblnRead
		
Find_Err: 
		If Err.Number Then
			lblnRead = False
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Add: This function add new register to the TAB_DAMAGE table
	'%Add: Esta función agrega registros a la tabla TAB_DAMAGE
	Public Function Add() As Boolean
		Dim lreccreTab_damage As eRemoteDB.Execute
		
		lreccreTab_damage = New eRemoteDB.Execute
		'**Parameters definition for stored procedure 'insudb.creClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.creClaim_caus'
		'**Infoemation read on October 04 of 2001 06:23:31 p.m.
		'Información leída el 04/10/2001 06:23:31 p.m.
		
		With lreccreTab_damage
			.StoredProcedure = "creTab_damage"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lreccreTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_damage = Nothing
		
	End Function
	'**%Update: This function update data of the TAB_DAMAGE table
	'%Update: Esta función actualiza registros en la tabla TAB_DAMAGE
	Public Function Update() As Boolean
		Dim lrecupdTab_damage As eRemoteDB.Execute
		
		lrecupdTab_damage = New eRemoteDB.Execute
		
		'**Parameters definition for stored procedure 'insudb.updClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.updClaim_caus'
		'**Infoemation read on October 04 of 2001 06:48:22 p.m.
		'Información leída el 04/10/2001 06:48:22 p.m.
		
		With lrecupdTab_damage
			.StoredProcedure = "updTab_damage"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecupdTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_damage = Nothing
		
	End Function
	
	'**%Delete: This function remove registers of the TAB_DAMAGE table
	'%Delete: Esta función elimina registros de la tabla TAB_DAMAGE
	Public Function Delete() As Boolean
		Dim lrecdelTab_damage As eRemoteDB.Execute
		
		lrecdelTab_damage = New eRemoteDB.Execute
		
		'**Parameters definition for stored procedure 'insudb.delClaim_caus'
		'Definición de parámetros para stored procedure 'insudb.delClaim_caus'
		'**Infoemation read on October 04 of 2001 06:52:26 p.m.
		'Información leída el 04/10/2001 06:52:26 p.m.
		
		With lrecdelTab_damage
			.StoredProcedure = "delTab_damage"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecdelTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_damage = Nothing
		
	End Function
	
	'**%valExistTab_damage: This function validate if there are damages related to a specific branch
	'%valExistTab_damage: Valida la existencia de daños asociadas a un ramo el cual es pasado como parámetro.
	Public Function valExistTab_damage(ByVal nBranch As Integer) As Boolean
		Dim lrecTab_damage As eRemoteDB.Execute
		
		valExistTab_damage = False
		
		lrecTab_damage = New eRemoteDB.Execute
		On Error GoTo valExistTab_damage_Err
		
		With lrecTab_damage
			.StoredProcedure = "valTab_damage_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("lCount") > 0 Then
					valExistTab_damage = True
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_damage = Nothing
		
valExistTab_damage_Err: 
		If Err.Number Then
			valExistTab_damage = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValClaim_dama: This function validate if there are damages related to claims.
	'%insValClaim_dama: Valida la existencia de daños asociados a un siniestro.
	Public Function insValClaim_dama(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nDamage_cod As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lrecTab_damage As eRemoteDB.Execute
		
		lrecTab_damage = New eRemoteDB.Execute
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValClaim_dama_Err
		
		lrecTab_damage.StoredProcedure = "valClaim_dama"
		lrecTab_damage.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		lrecTab_damage.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		If lrecTab_damage.Run(True) Then
			If lrecTab_damage.FieldToClass("lCount") > 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10874)
			End If
		End If
		
		insValClaim_dama = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lrecTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_damage = Nothing
		
insValClaim_dama_Err: 
		If Err.Number Then
			insValClaim_dama = "insValClaim_dama:" & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	
	'**%insValMSI014_K: This function perform validation over the fields of the header
	'%insValMSI014_K: Esta función se encarga de validar los datos introducidos en la cabecera de
	'%la forma.
	Public Function insValMSI014_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMSI014_K_Err
		
		
		'**+Validation of the field: Insurance branch
		'+Validación del campo: Ramo comercial.
		If nBranch = 0 Or nBranch = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
		End If
		
		'**+If the action is Duplicate Table, TAB_DAMAGE for this branch should be empty
		'+ Si la acción es duplicar no debe existir información registrada en la tabla TAB_DAMAGE.
		If nAction = 306 Then
			If nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
				If valExistTab_damage(nBranch) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10049)
				End If
			End If
		End If
		
		'**+If the action performed is inquiry, it validated if there is information related to this branch
		'+ Si la acción es consulta se verifica que exista información (daños asociados al ramo a consultar).
		If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			If nBranch <> 0 And nBranch <> eRemoteDB.Constants.intNull Then
				If Not valExistTab_damage(nBranch) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55949)
				End If
			End If
		End If
		
		insValMSI014_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMSI014_K_Err: 
		If Err.Number Then
			insValMSI014_K = Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%insValMSI014: This function perform validations over the fields of the folder
	'%insValMSI014: Esta función se encarga de validar los datos introducidos en la zona de detalle
	Public Function insValMSI014(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nDamage_cod As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_damage As eBranches.Tab_damage
		
		On Error GoTo insValMSI014_Err
		
		lclsErrors = New eFunctions.Errors
		lclsTab_damage = New eBranches.Tab_damage
		
		'**+Validations related to column: Code
		'+ Se valida la columna: Código.
		If nDamage_cod = 0 Or nDamage_cod = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 13209)
		Else
			If sAction = "Add" And lclsTab_damage.Find(nBranch, nDamage_cod) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10004)
			End If
		End If
		
		'**+Validations related to column: Description
		'+ Se valida la columna: Descripción larga.
		If sDescript = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10005)
		End If
		
		'**+Validations related to column: Short Description
		'+ Se valida la columna: Descripción corta.
		If sShort_des = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10006)
		End If
		
		'**+Validations related to column: Status.
		'+ Se valida la columna: Estado.
		If sStatregt = "0" Or sStatregt = strNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9089)
		End If
		
		insValMSI014 = lclsErrors.Confirm
		
insValMSI014_Err: 
		If Err.Number Then
			insValMSI014 = "insValMSI014: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_damage = Nothing
	End Function
	
	'*** InsPostMSI014: create/update corresponding data in the Tab_damage table
	'*InsPostMSI014: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla Tab_damage
	Public Function insPostMSI014(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nDamage_cod As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sStatregt As String = "", Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMSI014_err
		
		Me.nBranch = nBranch
		Me.nDamage_cod = nDamage_cod
		Me.sDescript = sDescript
		Me.sShort_des = sShort_des
		Me.sStatregt = sStatregt
		Me.nUsercode = nUsercode
		
		insPostMSI014 = True
		
		Select Case sAction
			
			'**+ If the selected option exists
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMSI014 = Add()
				
				'**+  If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMSI014 = Update()
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMSI014 = Delete()
		End Select
		
insPostMSI014_err: 
		If Err.Number Then
			insPostMSI014 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Duplicar Rutina que actualiza el ramo destino con los datos proveniente del ramo origen.
	Public Function insDuplicarMSI014(ByVal nLastBranch As Integer, ByVal nBranch As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecTab_damage As eRemoteDB.Execute
		
		lrecTab_damage = New eRemoteDB.Execute
		
		On Error GoTo insDuplicarMSI014_Err
		
		insDuplicarMSI014 = False
		
		'+ Duplica el registro correspondiente en TAB_DAMAGE
		With lrecTab_damage
			.StoredProcedure = "insDuplicateMSI014"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLastBranch", nLastBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insDuplicarMSI014 = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecTab_damage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_damage = Nothing
		
insDuplicarMSI014_Err: 
		If Err.Number Then
			insDuplicarMSI014 = False
		End If
		On Error GoTo 0
	End Function
End Class






