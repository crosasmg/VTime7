Option Strict Off
Option Explicit On
Option Compare Text
Public Class Tab_Fonasa
	' Hoja de Analisis Diferencial : 4
	' Desarrollado por: Victor GAjardo
	' Fecha: 24-05-20001
	' Descripcion: Crear Transaccion para manejo de tabla Aranceles Fonasa TAB_FONASA
	'
	
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Fonasa.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	
	'Column_name                     Type        Null  Descripcion
	Public nYear As Integer 'Number(4)     N   Año de la tabla
	Public sService As String 'Char(10)      N   Código de la prestación o servicio
	Public sSubService As String 'Char(4)       N   Sub - Código de la prestación
	Public dEffecdate As Date 'Date          N   Fecha de efecto del registro
	Public dNulldate As Date 'Date              Fecha de anulación del registro
	Public nCurrency As Integer 'Number(5)     N   Código de la moneda en que se expresan los valores. Valores posibles según tabla 11.
	Public nAmount As Double 'Number(10,2)  N   Valor de la prestación o servicio según FONASA.
	Public sDescript As String 'Char(30)      N   Descripción de la prestación o servicio
	Public nUsercode As Integer 'Number(5)     N   Código del usuario que crea el registro
	Public dCompdate As Date 'Date          N   Fecha del computador
	
	
	
	'% insValMSI559_k: se realizan las validaciones del encabezado de la Tabla Fonasa
	Public Function insValMSI559_K(ByVal sCodispl As String, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal sAction As String, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMSI559_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		' Validacion del Año: nYear As long
		If nYear = eRemoteDB.Constants.intNull Or nYear = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55605)
		End If
		' Validacion de la Moneda: nCurrency As long
		If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1351)
		End If
		' Validacion del Fecha Efecto: ByVal dEffecdate As Date
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 2056)
		Else
			' Si la acción es "actualizar" debe ser mayor a la fecha del último registro
			If CShort(sAction) = eFunctions.Menues.TypeActions.clngActionUpdate Then
				Call Find_date(dEffecdate, nYear, nCurrency)
				If dEffecdate <= Me.dEffecdate Then
					Call lclsErrors.ErrorMessage(sCodispl, 1021,  ,  , "(" & CStr(Me.dEffecdate) & ")")
				End If
			End If
		End If
		
		insValMSI559_K = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
insValMSI559_K_Err: 
		If Err.Number Then
			insValMSI559_K = insValMSI559_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'%Find_date. Esta funcion se encarga de buscar la mayor de las
	'%fecha de efecto de los registros de la tabla FONASA(tab_fonasa)
	Private Function Find_date(ByVal dEffectdate As Date, ByVal nYear As Integer, ByVal nCurrency As Integer) As Boolean
		Dim lrecreatab_fonasa_date As eRemoteDB.Execute
		
		lrecreatab_fonasa_date = New eRemoteDB.Execute
		
		On Error GoTo Find_date_Err
		
		'**+ Parameter definitions for stored procedure 'reatab_fonasa_date'
		'+Definición de parámetros para stored procedure 'reatab_fonasa_date'
		With lrecreatab_fonasa_date
			.StoredProcedure = "reatab_fonasa_date"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Me.dEffecdate = .FieldToClass("dEffecdate")
				
				.RCloseRec()
				Find_date = True
			Else
				Me.dEffecdate = Today
				Find_date = False
			End If
		End With
		
Find_date_Err: 
		If Err.Number Then
			Find_date = False
		End If
		lrecreatab_fonasa_date = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMSI559: se realizan las validaciones para la ventana de Pago de siniestro
	Public Function insValMSI559(ByVal sCodispl As String, ByVal sAction As String, ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date, ByVal sService As String, ByVal sSubService As String, ByVal nAmount As Double, ByVal sDescript As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsTab_Fonasa As Tab_Fonasa
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsTab_Fonasa = New Tab_Fonasa
		
		On Error GoTo insValMSI559_Err
		
		With lclsErrors
			'+ Validacion de existencia de registro
			If sAction = "Add" Then
				If lclsTab_Fonasa.Find(nYear, sService, sSubService, nCurrency, dEffecdate) Then
					Call .ErrorMessage(sCodispl, 10004)
				End If
			End If
			
			'+ Validacion de Prestación
			If sService = String.Empty And sService <> "0" Then
				Call .ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.RigthAling, " - Prestación")
			Else
				
				'+ Validacion del Sub prestación
				If sSubService = String.Empty Or sSubService = "0" Then
					Call .ErrorMessage(sCodispl, 55606)
				End If
				
				'+ Validacion de la Descripcion
				If sDescript = String.Empty Then
					Call .ErrorMessage(sCodispl, 10005)
				End If
				'+ Validacion del Monto
				If nAmount = eRemoteDB.Constants.intNull Or nAmount = 0 Then
					Call .ErrorMessage(sCodispl, 55607)
				End If
				
			End If
			
			insValMSI559 = .Confirm
		End With
		
insValMSI559_Err: 
		If Err.Number Then
			insValMSI559 = "insValMSI559: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lclsValField = Nothing
		lclsTab_Fonasa = Nothing
	End Function
	
	'%Delete: Al eliminar un registro de tab_fonasa este es anulado
	Public Function Delete() As Boolean
		Dim lrecdelTab_Fonasa As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelTab_Fonasa = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.UpdTab_FonasaNull'
		With lrecdelTab_Fonasa
			.StoredProcedure = "UpdTab_FonasaNull"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sService", sService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSubService", sSubService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		lrecdelTab_Fonasa = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find: Permite cargar en la colección los daños posibles de un siniestro
	Public Function Find(ByVal nYear As Integer, ByVal sService As String, ByVal sSubService As String, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lreaTab_Fonasa As eRemoteDB.Execute
		
		lreaTab_Fonasa = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'Definición de parámetros para stored procedure 'insudb.ReaTab_Fonasa'
		'Información leída el 13/02/2001 11:51:00
		With lreaTab_Fonasa
			.StoredProcedure = "ReaTab_Fonasa"
			' Parametros de entrada a la StoreProcedure
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sService", sService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSubService", sSubService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
			Else
				Find = False
			End If
		End With
		
		lreaTab_Fonasa = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Add: se crean los registros en tab_Fonasa
	Public Function Add() As Boolean
		Dim lrecinscreTab_Fonasa As eRemoteDB.Execute
		
		On Error GoTo Add_err
		lrecinscreTab_Fonasa = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.creTab_Fonasa'
		
		With lrecinscreTab_Fonasa
			.StoredProcedure = "creTab_Fonasa"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sService", sService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSubService", sSubService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		lrecinscreTab_Fonasa = Nothing
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'%Update: Esta función se encarga de agregar/actualizar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecupdTab_Fonasa As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecupdTab_Fonasa = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updTab_Fonasa'
		With lrecupdTab_Fonasa
			.StoredProcedure = "updTab_Fonasa"
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sService", sService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSubService", sSubService, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		lrecupdTab_Fonasa = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostMSI559: Se realiza la actualización de los datos
	Public Function insPostMSI559(ByVal Action As String, ByVal nYear As Integer, ByVal sService As String, ByVal sSubService As String, ByVal dEffecdate As Date, ByVal nUsercode As String, Optional ByVal nCurrency As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal sDescript As String = "") As Boolean
		Dim lclsTab_Fonasa As Tab_Fonasa
		lclsTab_Fonasa = New Tab_Fonasa
		
		On Error GoTo insPostMSI559_Err
		
		With lclsTab_Fonasa
			.nYear = nYear
			.sService = sService
			.sSubService = sSubService
			.dEffecdate = dEffecdate
			.nUsercode = CInt(nUsercode)
			.nCurrency = nCurrency
			.nAmount = nAmount
			.sDescript = sDescript
			
			Select Case Action
				Case "ADD"
					insPostMSI559 = .Add
				Case "DELETE"
					insPostMSI559 = .Delete
				Case "UPDATE"
					insPostMSI559 = .Update
			End Select
		End With
		
		lclsTab_Fonasa = Nothing
		
insPostMSI559_Err: 
		If Err.Number Then
			insPostMSI559 = False
		End If
		On Error GoTo 0
	End Function
End Class






