Option Strict Off
Option Explicit On
Public Class Tab_wincla
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_wincla.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**Define the principal properties of the correspondent class to the tab_wincla table
	'-Se definen las propiedades principales de la clase correspondientes a la tabla tab_wincla
	Public nTraTypec As Integer
	Public sBrancht As String
	Public sBussityp As String
	Public nSequence As Integer
	Public sCodispl As String
	Public dCompdate As Date
	Public sDefaulti As String
	Public sRequire As String
	Public nUsercode As Integer
	
	Private nClaim As Integer
	Public mblnCharge As Boolean
	Public sDescript As String
	
	Private Structure udtSecClaim
		Dim sDescript As String
		Dim sDefaulti As String
		Dim sRequire As String
		Dim sCodispl As String
		Dim nSequence As Integer
	End Structure
	
	Private arrSecClaim() As udtSecClaim
	
	Public sExist As String
	Public nIndex As Integer
	
	'**%DeleteTabwincla: Delete all the records of the table which matching with params.
	'%DeleteTabwincla : Elimina todos los registros que coincidan con lo parámetros
	Private Function DeleteTabwincla(ByVal nTraTypec As Integer, ByVal sBrancht As String, ByVal sBussityp As String) As Boolean
		
		On Error GoTo DeleteTabwincla_err
		
		Dim lrecdelTab_wincla As eRemoteDB.Execute
		lrecdelTab_wincla = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.delTab_wincla'
		'Información leída el 03/10/2001 01:52:14 p.m.
		
		With lrecdelTab_wincla
			.StoredProcedure = "delTab_wincla"
			.Parameters.Add("nTraTypec", nTraTypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteTabwincla = .Run(False)
		End With
		
DeleteTabwincla_err: 
		If Err.Number Then DeleteTabwincla = False
		lrecdelTab_wincla = Nothing
		On Error GoTo 0
	End Function
	
	'**% Item: Function that considering the index value charge in the variables of the class the information of the arrengement.
	'%Item: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function Item(ByVal lintIndex As Integer) As Boolean
		If mblnCharge Then
			If lintIndex <= UBound(arrSecClaim) Then
				With arrSecClaim(lintIndex)
					sDescript = .sDescript
					sDefaulti = .sDefaulti
					sRequire = .sRequire
					sCodispl = .sCodispl
					nSequence = .nSequence
				End With
				Item = True
			Else
				Item = False
			End If
		End If
		
	End Function
	
	
	'**%Find: find the claim data in the claim table from the given claim number
	'%Find: Busca los datos del siniestro en la tabla Claim a partir del número de siniestro dado
	Public Function Find(ByVal llngClaim As Double, ByVal llngTratypec As Integer, ByVal lstrBrancht As String, ByVal lstrBussityp As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		Dim lrecTab_wincla As eRemoteDB.Execute
		Dim lstrValue As String
		
		On Error GoTo Find_Err
		
		If llngClaim <> nClaim Or llngTratypec <> nTraTypec Or lstrBrancht <> sBrancht Or lstrBussityp <> sBussityp Or lblnFind Then
			lrecTab_wincla = New eRemoteDB.Execute
			
			'**+Parameters definition for the stored procedure 'insudb.reaTab_WinCla'
			'**+Data read on 01/11/2001 15.08.25
			'+Definición de parámetros para stored procedure 'insudb.reaTab_WinCla'
			'+Información leída el 11/01/2001 15.08.25
			
			With lrecTab_wincla
				.StoredProcedure = "reaTab_WinCla"
				.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTratypec", llngTratypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBrancht", lstrBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sBussityp", lstrBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			lrecTab_wincla = Nothing
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% CountItem: property that indicates the number of registration that is in a determined moment in the class arrengement.
	'%CountItem: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(arrSecClaim)
			Else
				CountItem = -1
			End If
			
		End Get
	End Property
	
	
	'%insValMSI001_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValSI001_K(ByVal sAction As String, ByVal optBussines As Integer, ByVal cbeBrancht As Integer, ByVal cbeTranType As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		On Error GoTo insValMSI001_K_Err
		lclsErrors = New eFunctions.Errors
		
		insValSI001_K = String.Empty
		
		'**+ Validate the technical branch
		'+ Valida el ramo técnico
		If cbeBrancht <= 0 Then
			Call lclsErrors.ErrorMessage("MSI001", 36090)
		End If
		
		'**+ Validate the type of transaction
		'+ Valida el tipo de transacción
		If cbeTranType <= 0 Then
			Call lclsErrors.ErrorMessage("MSI001", 7133)
		End If
		
		insValSI001_K = lclsErrors.Confirm
		
insValMSI001_K_Err: 
		If Err.Number Then insValSI001_K = insValSI001_K & Err.Description
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'% insValMSI001: Esta función se encarga de validar los datos introducidos en el folder
	Public Function insValSI001(ByVal sCodispl As String, ByVal sWindows As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMSI001_Err
		
		lclsErrors = New eFunctions.Errors
		
		insValSI001 = String.Empty
		
		'+ Debe seleccionar por lo menos una ventana
		With lclsErrors
			If sWindows = "0" Then
				Call .ErrorMessage(sCodispl, 99007)
			End If
			
			insValSI001 = .Confirm
		End With
		
insValMSI001_Err: 
		If Err.Number Then
			insValSI001 = "insValSI001: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdtab_wincla(1)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdtab_wincla(3)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdtab_wincla(2)
	End Function
	
	'%insPostSI001: Esta función se encarga de llamar al método correspondiente a la acción
	'* ejecutada (crear/actualizar/eliminar) sobre las tablas de Tab_wincla
	
	Public Function insPostSI001(ByVal nTraTypec As Integer, ByVal sBrancht As String, ByVal sBussityp As String, ByVal sExist As String, ByVal sSel As String, ByVal nSequence As Integer, ByVal sCodispl As String, ByVal sDefaulti As String, ByVal sRequire As String, ByVal nUsercode As Integer) As Boolean
		Dim lblnAdd As Boolean
		
		Dim lcolTab_winclas As Tab_winclas
		
		lcolTab_winclas = New Tab_winclas
		
		On Error GoTo InsPostSI001_err
		insPostSI001 = True
		With Me
			.nTraTypec = nTraTypec
			.sBrancht = sBrancht
			.sBussityp = sBussityp
			.nSequence = nSequence
			.sCodispl = sCodispl
			.sDefaulti = sDefaulti
			.sRequire = sRequire
			.nUsercode = nUsercode
			'+Si el registro existe y no esta seleccionado
			If sExist = "1" And sSel = "2" Then
				insPostSI001 = .Delete
				
				'+Si el registro no existe y esta seleccionado
			ElseIf sExist = "2" And sSel = "1" Then 
				insPostSI001 = .Add
				lblnAdd = True
			ElseIf sExist = "1" Then 
				insPostSI001 = .Update
			End If
			
		End With
		
		lcolTab_winclas = Nothing
		
InsPostSI001_err: 
		If Err.Number Then
			insPostSI001 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'%InsUpdtab_wincli: Realiza la actualización de la tabla
	Private Function InsUpdtab_wincla(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdtab_wincla As eRemoteDB.Execute
		
		On Error GoTo InsUpdtab_wincla_Err
		lrecInsUpdtab_wincla = New eRemoteDB.Execute
		
		'+ Definición de store procedure InsUpdtab_wincli al 03-23-2002 15:23:16
		With lrecInsUpdtab_wincla
			.StoredProcedure = "InsUpdtab_wincla"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTraTypec", nTraTypec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdtab_wincla = .Run(False)
		End With
		
InsUpdtab_wincla_Err: 
		If Err.Number Then
			InsUpdtab_wincla = False
		End If
		lrecInsUpdtab_wincla = Nothing
		On Error GoTo 0
	End Function
End Class






