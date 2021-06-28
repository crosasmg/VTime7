Option Strict Off
Option Explicit On
Public Class Interm_typ
	'%-------------------------------------------------------%'
	'% $Workfile:: Interm_typ.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 26/07/04 17.12                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 16/04/2001
	
	'+ El campo llave corresponde a nIntertyp.
	
	'+ Column_name         Type                 Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ------ ----- ----- -------- ------------------ --------------------
	Public nInterTyp As Integer 'Long      2     10     0    yes      no                 yes
	Public sDescript As String 'char     30                 yes      no                 yes
	Public sParticin As String 'char      1                 yes      no                 yes
	Public sShort_des As String 'char     12                 yes      no                 yes
	Public sStatregt As String 'char      1                 yes      no                 yes
	Public nUsercode As Integer 'Long      2     10     0    yes      no                 yes
	Public nTyp_acco As Integer 'Long      2     10     0    yes      no                 yes
	Public sInd_FECU As String 'CHAR      1                 YES
	Public sGen_certif As String 'CHAR      1                 YES
	
	Public nStatusInstance As Integer
	
	'% Find: Busca la información de un determinado tipo de intermediario
	Public Function Find(ByVal nInterTyp As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaInterm_typ As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nInterTyp = Me.nInterTyp And Not lblnFind Then
			Find = True
		Else
			lrecreaInterm_typ = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaClient'
			'+ Información leída el 01/07/1999 03:20:55 PM
			
			With lrecreaInterm_typ
				.StoredProcedure = "reaInterm_typ_v"
				.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nInterTyp = .FieldToClass("nInterTyp")
					Me.sDescript = .FieldToClass("sDescript")
					Me.sParticin = .FieldToClass("sParticin")
					Me.sShort_des = .FieldToClass("sShort_des")
					Me.sStatregt = .FieldToClass("sStatregt")
					Me.nTyp_acco = .FieldToClass("nTyp_Acco")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaInterm_typ = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Add: Esta función se encarga de agregar información en la tabla principal de la clase.
	Public Function Add() As Boolean
		Dim lreccreInterm_typ As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lreccreInterm_typ = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creInterm_typ'
		'+Información leída el 16/04/01 11:17:58 a.m.
		
		With lreccreInterm_typ
			.StoredProcedure = "creInterm_typ"
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sParticin", sParticin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_FECU", sInd_FECU, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGen_certif", sGen_certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreInterm_typ = Nothing
	End Function
	
	'% Update: Esta función se encarga de actualizar información en la tabla principal de la clase.
	Public Function Update() As Boolean
		Dim lrecupdInterm_typ As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdInterm_typ = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updInterm_typ'
		'+Información leída el 16/04/01 03:51:44 p.m.
		
		With lrecupdInterm_typ
			.StoredProcedure = "updInterm_typ"
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sParticin", sParticin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_FECU", sInd_FECU, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGen_certif", sGen_certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdInterm_typ = Nothing
	End Function
	
	'% Delete: Esta función se encarga de eliminar información en la tabla interm_typ
	Public Function Delete() As Boolean
		Dim lrecdelInterm_typ As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecdelInterm_typ = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delInterm_typ'
		'+Información leída el 17/04/01 09:20:02 a.m.
		
		With lrecdelInterm_typ
			.StoredProcedure = "delInterm_typ"
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelInterm_typ = Nothing
	End Function
	
	'% IntermediaExists: Esta propiedad indica la existencia o no del tipo de intermediario dentro
	'% de la tabla de intermediarios
	Public ReadOnly Property IntermediaExist() As Boolean
		Get
			Dim lobjInterm_typ As eRemoteDB.Execute
			
			On Error GoTo IntermediaExist_Err
			
			lobjInterm_typ = New eRemoteDB.Execute
			With lobjInterm_typ
				.StoredProcedure = "reaIntermedia_typ"
				.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				IntermediaExist = .Run
			End With
			'UPGRADE_NOTE: Object lobjInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjInterm_typ = Nothing
			
IntermediaExist_Err: 
			If Err.Number Then
				IntermediaExist = False
			End If
			
			On Error GoTo 0
			
		End Get
	End Property
	
	'% insValMAG001: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'% forma.
	Public Function insValMAG001(ByVal sCodispl As String, ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nInterTyp As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sParticin As String = "", Optional ByVal sStatregt As String = "", Optional ByVal nTyp_acco As Integer = 0) As String
		
		'- Se define el objeto lclsInterm_Typ, el manejo de la libreria de tipos de intermediarios
		
		Dim lclsInterm_typ As eAgent.Interm_typ
		
		'- Se define la variable lclserrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAG001_Err
		
		lclsInterm_typ = New eAgent.Interm_typ
		lclsErrors = New eFunctions.Errors
		
		'+Se da inicio al ciclo de validaciones.
		
		If sAction = "Del" Then
			If lclsInterm_typ Is Nothing Then
				lclsInterm_typ = New eAgent.Interm_typ
			End If
			lclsInterm_typ.nInterTyp = nInterTyp
			If lclsInterm_typ.IntermediaExist Then
				
				Call lclsErrors.ErrorMessage(sCodispl, 10854)
			End If
		Else
			
			If nInterTyp <> eRemoteDB.Constants.intNull And sAction = "Add" Then
				
				If lclsInterm_typ Is Nothing Then
					lclsInterm_typ = New eAgent.Interm_typ
				End If
				
				'+Se valida que el valor introducido en el campo no se encuentre en la tabla registrado
				
				If lclsInterm_typ.Find(nInterTyp) Then
					Call lclsErrors.ErrorMessage(sCodispl, 9007)
				End If
			Else
				If nInterTyp = eRemoteDB.Constants.intNull And sAction = "Add" Then
					Call lclsErrors.ErrorMessage(sCodispl, 10095)
				End If
			End If
			
			If nInterTyp <> eRemoteDB.Constants.intNull Then
				
				'+Si el campo tipo de intermediario tiene valor la descripción debe estar llena.
				
				If sDescript = strNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 10855)
				End If
			End If
			
			If nInterTyp <> eRemoteDB.Constants.intNull Then
				
				'+Si el campo tipo de intermediario tiene valor la descripción corta debe estar llena.
				
				If sShort_des = strNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 10856)
				End If
			End If
			
			If nInterTyp <> eRemoteDB.Constants.intNull Then
				
				'+Si el campo tipo de intermediario tiene valor el campo de estado del registro debe estar lleno.
				
				If sStatregt = strNull Or sStatregt = "0" Then
					Call lclsErrors.ErrorMessage(sCodispl, 1016)
				End If
			End If
			
			If nInterTyp <> eRemoteDB.Constants.intNull Then
				
				'+Si el campo tipo de intermediario tiene valor el Tipo de Cuenta debe estar lleno.
				
				If nTyp_acco = eRemoteDB.Constants.intNull Or nTyp_acco = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 7107)
				End If
			End If
			
			
		End If
		
		insValMAG001 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInterm_typ = Nothing
		
insValMAG001_Err: 
		If Err.Number Then
			insValMAG001 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'% InsPostMAG001: Esta función se encarga de crear/actualizar los registros
	'% correspondientes en la tabla de Interm_typ
	Public Function insPostMAG001(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nInterTyp As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal sShort_des As String = "", Optional ByVal sParticin As String = "", Optional ByVal sStatregt As String = "", Optional ByVal nTyp_acco As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal sInd_FECU As String = "", Optional ByVal sGen_certif As String = "") As Boolean
		
		On Error GoTo InsPostMAG001_err
		
		With Me
			.nInterTyp = nInterTyp
			.sDescript = sDescript
			.sParticin = sParticin
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.nTyp_acco = nTyp_acco
			.sInd_FECU = IIf(sInd_FECU = String.Empty, "2", sInd_FECU)
			.sGen_certif = IIf(sGen_certif = String.Empty, "2", sGen_certif)
		End With
		
		insPostMAG001 = True
		
		Select Case sAction
			'+Si la opción seleccionada es Registrar
			Case "Add"
				insPostMAG001 = Add()
				'+Si la opción seleccionada es Modificar
			Case "Update"
				insPostMAG001 = Update()
				'+Si la opción seleccionada es Eliminar
			Case "Del"
				insPostMAG001 = Delete()
		End Select
		
InsPostMAG001_err: 
		If Err.Number Then
			insPostMAG001 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find_FECU: se busca el primer tipo que cumpla con las condiciones
	Public Function Find_FECU(Optional ByVal nInterTyp As Integer = eRemoteDB.Constants.intNull, Optional ByVal sClient As String = "", Optional ByVal sInd_FECU As String = "", Optional ByVal sGen_certif As String = "") As Object
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Find_FECU_err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "tabInterm_typ_FECUPKG.tabInterm_typ_FECU"
			.Parameters.Add("sShownum", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondition", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nInterTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_FECU", sInd_FECU, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sGen_certif", sGen_certif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Me.nInterTyp = .FieldToClass("nInterTyp")
				Me.sDescript = .FieldToClass("sDescript")
				Find_FECU = True
			End If
		End With
		
Find_FECU_err: 
		If Err.Number Then
			Find_FECU = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% Find_Typ_acco: Busca la información de un determinado tipo de intermediario
	Public Function Find_Typ_acco(ByVal nTyp_acco As Integer) As Boolean
		Dim lrecreaInterm_typ As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo Find_Typ_acco_Err
		
		lrecreaInterm_typ = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaClient'
		'+ Información leída el 01/07/1999 03:20:55 PM
		
		With lrecreaInterm_typ
			.StoredProcedure = "reaInterm_Typ_acco"
			.Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If .Parameters.Item("nExists").Value = 1 Then
					Find_Typ_acco = True
				Else
					Find_Typ_acco = False
				End If
			End If
			
		End With
		'UPGRADE_NOTE: Object lrecreaInterm_typ may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaInterm_typ = Nothing
		
Find_Typ_acco_Err: 
		If Err.Number Then
			Find_Typ_acco = False
		End If
		On Error GoTo 0
		
	End Function
End Class






