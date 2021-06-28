Option Strict Off
Option Explicit On
Public Class Claim_Dama
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_Dama.cls                           $%'
	'% $Author:: Nvaplat37                                  $%'
	'% $Date:: 9/09/03 11:01a                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Defined the principal properties of the corresponding class to the Claim_Dama table
	'- Se definen las propiedades principales de la clase correspondientes a la tabla Claim_Dama
	'**-The key field correspond to nClaim, nCase_num, nDeman_type, nDamage_cod
	'- El campo llave corresponde a nClaim, nCase_num, nDeman_type, nDamage_cod
	
	'Column_name                           Type                           Length Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'------------------------------------- ------------------------------ ------ ----- ----- ----------------------------------- ----------------------------------- -----------------------------------
	Public nClaim As Double 'int                           no     4     10    0     no                                  (n/a)                               (n/a)
	Public nCase_num As Integer 'smallint                      no     2     5     0     no                                  (n/a)                               (n/a)
	Public nDeman_type As Integer 'smallint                      no     2     5     0     no                                  (n/a)                               (n/a)
	Public nDamage_cod As Integer 'smallint                      no     2     5     0     no                                  (n/a)                               (n/a)
	Public nBranch As Short 'smallint                      no     2     5     0     no                                  (n/a)                               (n/a)
	Public sStatregt As String 'char                          no     1                 yes                                 no                                  yes
	Public nUsercode As Integer 'smallint                      no     2     5     0     yes                                 (n/a)                               (n/a)
	Public nMag_dam As Integer 'smallint                      no     2     5     0     yes                                 (n/a)                               (n/a)
	
	'**-Auxuliaries variables
	'-Variables auxiliares
	Public sDes_Damage_cod As String
	Public sDes_Mag_dam As String
	Public nAction As Integer
	
	'**% Update: Allows to insert or erase  a record according to the action
	'% Update: Permite insertar o borrar un registro dependiendo
	'% de la accion
	Public Function Update() As Object
		
		Dim lrecinsClaim_Dama As eRemoteDB.Execute
		
		lrecinsClaim_Dama = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**Parameters definition for the stored procedure 'insudbClaim_Dama'
		'Definición de parámetros para stored procedure 'insudb.insClaim_Dama'
		'**Data read on 02/13/2001 13:29:3022
		'Información leída el 13/02/2001 13:29:30
		
		With lrecinsClaim_Dama
			.StoredProcedure = "insClaim_Dama"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMag_dam", nMag_dam, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		lrecinsClaim_Dama = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		
	End Function
	
	'** insValSI020: Validates the window content
	'insValSI020: Valida el contenido de la ventana
	Public Function insValSI020(ByVal sCodispl As String, ByVal sAction As String, ByVal nDamage_cod As Integer, ByVal nMag_dam As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValSI020_err
		
		If nMag_dam = eRemoteDB.Constants.intNull Or nMag_dam = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Magnitud del daño ")
		End If
		
		If nDamage_cod = eRemoteDB.Constants.intNull Or nDamage_cod = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "Repuesto Dañado ")
		End If
		
		If sAction = "Add" Then
			If Me.InsValExistClaim_dama(nClaim, nCase_num, nDeman_type, nDamage_cod) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10284)
			End If
		End If
		insValSI020 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
insValSI020_err: 
		If Err.Number Then
			insValSI020 = "insValSI020: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'%insPostSI020: Realiza modificaciones en las tablas Claim Dama y Cases_win
	Public Function insPostSI020(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nDamage_cod As Integer, ByVal nUsercode As Integer, ByVal nMag_dam As Integer) As Boolean
		Dim lclsClaim_dama As Claim_Dama
		Dim lclsCases_win As Cases_win
		
		lclsClaim_dama = New Claim_Dama
		lclsCases_win = New Cases_win
		
		On Error GoTo insPostSI020_Err
		
		With lclsClaim_dama
			.nBranch = nBranch
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.nDamage_cod = nDamage_cod
			.sStatregt = "1"
			.nUsercode = nUsercode
			.nMag_dam = nMag_dam
			Select Case sAction
				Case "Add"
					.nAction = 1
				Case "Update"
					.nAction = 2
				Case "Del"
					.nAction = 3
			End Select
			If .Update Then
				If .InsValExistClaim_dama(nClaim, nCase_num, nDeman_type, eRemoteDB.Constants.intNull) Then
					insPostSI020 = lclsCases_win.Add_Cases_win(nClaim, nCase_num, nDeman_type, sCodispl, "2", nUsercode)
				Else
					insPostSI020 = lclsCases_win.Add_Cases_win(nClaim, nCase_num, nDeman_type, sCodispl, "1", nUsercode)
				End If
			Else
				insPostSI020 = False
			End If
		End With
		
insPostSI020_Err: 
		If Err.Number Then
			insPostSI020 = False
		End If
		On Error GoTo 0
		lclsClaim_dama = Nothing
		lclsCases_win = Nothing
	End Function
	'%InsValExistClaim_dama: Valida que el registro existe en la tabla Claim_dama
	Public Function InsValExistClaim_dama(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal nDamage_cod As Integer) As Boolean
		
		Dim lrecInsValExistClaim_dama As eRemoteDB.Execute
		
		lrecInsValExistClaim_dama = New eRemoteDB.Execute
		
		On Error GoTo InsValExistClaim_dama_Err
		
		With lrecInsValExistClaim_dama
			.StoredProcedure = "InsValExistClaim_dama"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDamage_cod", nDamage_cod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValExistClaim_dama = .Parameters("nExist").Value = 1
			Else
				InsValExistClaim_dama = False
			End If
			
		End With
InsValExistClaim_dama_Err: 
		If Err.Number Then
			InsValExistClaim_dama = False
		End If
		On Error GoTo 0
		lrecInsValExistClaim_dama = Nothing
	End Function
End Class






