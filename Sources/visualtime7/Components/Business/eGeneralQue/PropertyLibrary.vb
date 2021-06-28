Option Strict Off
Option Explicit On
Public Class PropertyLibrary
	'%-------------------------------------------------------%'
	'% $Workfile:: PropertyLibrary.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on January 18,2000
	'+ Propiedades según la tabla en el sistema el 18/01/2000.
	'**+ The key fields correspond to nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_account, sCost_cente and nMonth.
	'+ Los campos llaves corresponden a nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_cente y nMonth
	
	'Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nIdProperty As Integer 'int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public sProperty As String '                                                                                                                        char                                                                                                                             no                                  20                      no                                  no                                  no
	Public sFormat As String '                                                                                                                          varchar                                                                                                                          no                                  255                     yes                                 no                                  no
	Public nUsercode As Integer '
	
	'**- Define the additional variables
	'- Se definen las variable auxiliares
	'**- Define the variable to indicate the status of each instance in the collection.
	'- Se define la variable para indicar el estado de cada instancia en la colección
	
	Public nStatusInstance As Integer
	Private Enum eActions
		clngAdd = 1
		clndUpdate = 2
		clngDelete = 3
	End Enum
	
	
	'**% Add: add records in the budget results table.
	'% Add: Permite añadir registros en la tabla de resultados presupuestarios
	Public Function Add() As Boolean
		Add = insUpdPropertyLibrary(eActions.clngAdd)
	End Function
	
	'**% Update: modifiy the records in the budget results table.
	'% Update: Permite modificar registros en la tabla de resultados presupuestarios
	Public Function Update() As Boolean
		Update = insUpdPropertyLibrary(eActions.clndUpdate)
	End Function
	
	'**% Delete: delete records in the budget results table.
	'% Delete: Permite eliminar registros en la tabla de resultados presupuestarios
	Public Function Delete() As Boolean
		Delete = insUpdPropertyLibrary(eActions.clngDelete)
	End Function
	
	'**% Find: search records in the budget results table.
	'% Find: Permite buscar registros en la tabla de resultados presupuestarios
	Function Find(ByVal IdProperty As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaPropertyLibrary As eRemoteDB.Execute
		lrecreaPropertyLibrary = New eRemoteDB.Execute
		If IdProperty = nIdProperty And Not lblnFind Then
			Find = True
		Else
			With lrecreaPropertyLibrary
				.StoredProcedure = "reaPropertyLibrary"
				.Parameters.Add("nIdProperty", IdProperty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sProperty", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					
					nIdProperty = .FieldToClass("nIdProperty")
					sProperty = .FieldToClass("sProperty")
					sFormat = .FieldToClass("sFormat")
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaPropertyLibrary may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaPropertyLibrary = Nothing
		End If
	End Function
	
	'*** Class_Initialize: controls the opening of the clas.
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'    nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE")
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% insUpdPropertyLibrary. This function updates the PropertyLibrary table
	'**% in the data base. As a parameter for the call to the SP, use the contained values in the properies of the class.
	'%insUpdPropertyLibrary. Esta funcion se encarga de realizar la actualización de la tabla PropertyLibrary
	'%en la base de datos. Como parametro para la llamada a los SP, utiliza los valores
	'%contenidos en las propiedades de la clase
	Private Function insUpdPropertyLibrary(ByRef llngAction As eActions) As Boolean
		Dim lrecinsUpdPropertyLibrary As eRemoteDB.Execute
		lrecinsUpdPropertyLibrary = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.insUpdPropertyLibrary'
		'+Definición de parámetros para stored procedure 'insudb.insUpdPropertyLibrary'
		'**+ Information read on July 11,2000  11:08:23
		'+Información leída el 11/07/2000 11:08:23
		
		With lrecinsUpdPropertyLibrary
			.StoredProcedure = "insPropertyLibrary"
			.Parameters.Add("nIdProperty", nIdProperty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProperty", RTrim(sProperty), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFormat", RTrim(sFormat), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdPropertyLibrary = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdPropertyLibrary may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdPropertyLibrary = Nothing
	End Function
	'**% MISSING
	'% MISSING
	Public Function insValMGE001(ByVal strAction As String, ByVal nIdProperty As Integer, ByVal sProperty As String) As String
		Dim lerrTime As eFunctions.Errors
		lerrTime = New eFunctions.Errors
		
		
		'**+ Validate the content of the field key
		'+ Se valida el contenido del campo llave
		
		If nIdProperty > 0 And strAction = "Add" Then
			'**+ Validate that the introduced value in the field is not in the table
			'+Se valida que el valor introducido en el campo no se encuentre en la tabla registrado
			If insValPropertyLibrary(nIdProperty) Then
				lerrTime.ErrorMessage("MGE001", 12101)
			End If
		Else
			If nIdProperty <= 0 And strAction = "Add" Then
				lerrTime.ErrorMessage("MGE001", 10842)
			End If
		End If
		
		If nIdProperty > 0 Then
			'**+ If the code field has a value, the other fields must not be empty.
			'+Si el campo código tiene valor deben estar llenos los demas campos
			If Trim(sProperty) = String.Empty Then
				If strAction = "Update" Or strAction = "Add" Then
					lerrTime.ErrorMessage("MGE001", 2207)
				End If
			End If
		End If
		
		insValMGE001 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		
		If Err.Number Then
			insValMGE001 = "insValMGE001: " & Err.Description
		End If
	End Function
	
	'**% MISSING
	'% MISSING
	Private Function insValPropertyLibrary(ByVal lintIdProperty As Integer) As Boolean
		Dim lobjPropertyLibrary As PropertyLibrary
		lobjPropertyLibrary = New PropertyLibrary
		insValPropertyLibrary = lobjPropertyLibrary.Find(lintIdProperty)
		'UPGRADE_NOTE: Object lobjPropertyLibrary may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjPropertyLibrary = Nothing
		
	End Function
	
	'**% MISSING
	'% MISSING
	Public Function insPostMGE001(ByVal strAction As String, ByVal nIdProperty As Integer, ByVal sProperty As String, ByVal sFormat As String, ByVal nUsercode As Integer) As Boolean
		
		With Me
			.nIdProperty = nIdProperty
			.sProperty = sProperty
			.sFormat = sFormat
			.nUsercode = nUsercode
		End With
		
		Select Case strAction
			Case "Add"
				insPostMGE001 = Add
			Case "Update"
				insPostMGE001 = Update
			Case "Del"
				insPostMGE001 = Delete
		End Select
		
	End Function
End Class






