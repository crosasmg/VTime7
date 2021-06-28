Option Strict Off
Option Explicit On
Public Class cot_stand_alone
	'%-------------------------------------------------------%'
	'% $Workfile:: cot_stand_alone.cls                        $%'
	'% $Author:: Pmanzur                                    $%'
	'% $Date:: 21/02/06 17:37                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name          Type                     Computed  Length Prec  Scale Nullable
	'-------------------- ------------------------ --------- ------ ----- ----- ------------
	Public nId_object As Integer 'smallint     no        2      5     0     no
	Public sName As String 'char         no        70                 no
	Public nType_object As Integer 'smallint     no        2      5     0     no
	Public nLevel As Integer 'smallint     no        2      5     0     no
	Public nOrder As Integer 'smallint     no        2      5     0     yes
	Public dDate_update As Date 'Date         no                           yes
	Public dDate_struct As Date 'Date         no                           yes
	Public nUsercode As Integer 'smallint     no        2      5     0     no
	Public sPath As String 'char         no        70                 no
	Public nAction As Integer 'smallint     no        2      5     0     no
	
	
	'% Update: Agrega los datos correspondientes para una hoja
	Public Function Update() As Boolean
		Dim lreacot_stand_alone As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lreacot_stand_alone = New eRemoteDB.Execute
		
		With lreacot_stand_alone
			.StoredProcedure = "insupdcot_stand_alone"
			.Parameters.Add("nId_object", nId_object, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName", sName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 70, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_object", nType_object, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLevel", nLevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPath", sPath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 70, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreacot_stand_alone may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreacot_stand_alone = Nothing
	End Function
	
	
	
	'%insValMCA816_K: Esta función se encarga de validar los objetos del sincronizador del cotizador
	Public Function insValMCA816_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nId_object As Integer, ByVal sName As String, ByVal nType_object As Integer, ByVal nOrder As Integer) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValMCA816_K_Err
		
		lerrTime = New eFunctions.Errors
		
		With lerrTime
			
			If sAction <> "Del" Then
				If sName = String.Empty Then
					Call .ErrorMessage("MCA816", 10258,  , eFunctions.Errors.TextAlign.RigthAling, " (Nombre de objeto)")
				End If
				If nType_object <= 0 Then
					Call .ErrorMessage("MCA816", 10258,  , eFunctions.Errors.TextAlign.RigthAling, " (Tipo de objeto)")
				Else
					If (nType_object = 3 Or nType_object = 4) And nOrder <= 0 Then
						Call .ErrorMessage("MCA816", 10258,  , eFunctions.Errors.TextAlign.RigthAling, " en el orden si el tipo de objetos es tabla, procedure o package")
					End If
				End If
			End If
			insValMCA816_K = .Confirm
		End With
		
insValMCA816_K_Err: 
		If Err.Number Then
			insValMCA816_K = insValMCA816_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
	End Function
	
	'% insPostMCA816_K: Actualiza los objetos del sincronizador del cotizador
	Public Function insPostMCA816_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nId_object As Integer, Optional ByVal sName As String = "", Optional ByVal nType_object As Integer = 0, Optional ByVal nLevel As Integer = 0, Optional ByVal nOrder As Integer = 0, Optional ByVal sPath As String = "", Optional ByVal nUsercode As Integer = 0) As Boolean
		
		On Error GoTo insPostMCA816_K_err
		
		With Me
			.nId_object = nId_object
			.sName = sName
			.nType_object = nType_object
			.nLevel = nLevel
			.nOrder = nOrder
			.nUsercode = nUsercode
			.sPath = sPath
			
			sAction = Trim(sAction)
			Select Case sAction
				Case "Add"
					.nAction = 1
				Case "Update"
					.nAction = 2
				Case "Del"
					.nAction = 3
			End Select
			insPostMCA816_K = Update
		End With
		
insPostMCA816_K_err: 
		If Err.Number Then
			insPostMCA816_K = False
		End If
		On Error GoTo 0
		
	End Function
End Class






