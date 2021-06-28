Option Strict Off
Option Explicit On
Public Class Tab_acc_mov
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_acc_mov.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:35p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system 06/18/2001
	'**- Tab_acc_mov:Movements type of current accounts
	'-Propiedades según la tabla en el sistema 18/06/2001
	'-Tab_acc_mov:Tipos de movimientos de cuentas corrientes
	
	'Column_name                   Type      Computed    Length      Prec  Scale Nullable  TrimTrailingBlanks                  FixedLenNullInSource
	Public nTypeMove As Object 'smallint no          2            5     0     no          (n/a)                               (n/a)
	Public sDescript As Object 'char     no          30                       yes          yes                                 yes
	Public sShort_des As Object 'char     no          12                       yes          yes                                 yes
	Public sStatregt As Object 'char     no          1                        yes          yes                                 yes
	Public sDebitside As Object 'char     no          1                        no           yes                                 no
	Public nUsercode As Object 'smallint no          2            5     0     yes         (n/a)                               (n/a)
	
	'**- Additional properties
	'- Propiedades auxiliares
	
	Public nAction As Integer
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Tab_acc_mov"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Tab_acc_mov"
	Public Function Find(ByVal nTypeMove As Integer, Optional ByVal blnFind As Boolean = False) As Boolean
		Dim lrecreaTab_acc_mov As eRemoteDB.Execute
		
		lrecreaTab_acc_mov = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Me.nTypeMove <> nTypeMove Or blnFind Then
			Me.nTypeMove = nTypeMove
			'**+ Parametes definition for the stored procedure 'insudb.reaTab_acc_mov'
			'+Definición de parámetros para stored procedure 'insudb.reaTab_acc_mov'
			'+** Data of 07/16/2001 10:57:13
			'+Información leída el 16/07/2001 10:57:13
			
			With lrecreaTab_acc_mov
				.StoredProcedure = "reaTab_acc_mov"
				.Parameters.Add("nTypeMove", nTypeMove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sDescript = .FieldToClass("sDescript")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					sDebitside = .FieldToClass("sDebitside")
					Find = True
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		'UPGRADE_NOTE: Object lrecreaTab_acc_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_acc_mov = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Tab_acc_mov".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Tab_acc_mov". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecinsTab_acc_mov As eRemoteDB.Execute
		
		lrecinsTab_acc_mov = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+ Parameters definition for the stored procedure 'insudb.insTab_acc_mov'
		'+Definición de parámetros para stored procedure 'insudb.insTab_acc_mov'
		'**+ Data of 07/16/2001 09:30:41
		'+Información leída el 16/07/2001 09:30:41
		
		With lrecinsTab_acc_mov
			.StoredProcedure = "insTab_acc_mov"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeMove", nTypeMove, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDebitside", sDebitside, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecinsTab_acc_mov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_acc_mov = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	
	'**%insValMGE632_K: This method validates the header section of the page "MGE632" as described in the
	'**%functional specifications
	'%InsValMGE632_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana "MGE632"
	Public Function insValMGE632_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nTypeMove As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMGE632_K_Err
		
		If nTypeMove <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 66151)
		Else
			If sAction = "Add" And Find(nTypeMove) Then
				lclsErrors.ErrorMessage(sCodispl, 12089)
			End If
		End If
		
		If Trim(sDescript) = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 10857)
		End If
		
		If Trim(sShort_des) = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 10858)
		End If
		
		If sStatregt = String.Empty Or sStatregt = "0" Then
			lclsErrors.ErrorMessage(sCodispl, 10826)
		End If
		
		insValMGE632_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMGE632_K_Err: 
		If Err.Number Then
			insValMGE632_K = insValMGE632_K & Err.Description
		End If
	End Function
	
	'**%insPostMGE632_K. This method updates the database (as described in the functional specifications)
	'**%for the page "MGE632"
	'%insPostMGE632_K: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "MGE632"
	Public Function insPostMGE632_K(ByVal sAction As String, ByVal nTypeMove As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal sStatregt As String, ByVal sDebitside As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMGE632_K_err
		
		With Me
			.nTypeMove = nTypeMove
			.sDescript = sDescript
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.sDebitside = IIf(sDebitside <> String.Empty, sDebitside, "0")
			.nUsercode = nUsercode
			If sAction = "Add" Then
				nAction = 1
			ElseIf sAction = "Update" Then 
				nAction = 2
			Else
				nAction = 3
			End If
			insPostMGE632_K = Update
		End With
		
insPostMGE632_K_err: 
		If Err.Number Then
			insPostMGE632_K = False
		End If
		On Error GoTo 0
	End Function
End Class






