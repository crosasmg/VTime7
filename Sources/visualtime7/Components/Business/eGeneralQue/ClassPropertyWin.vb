Option Strict Off
Option Explicit On
Public Class ClassPropertyWin
	'%-------------------------------------------------------%'
	'% $Workfile:: ClassPropertyWin.cls                     $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on January 18,2000.
	'+ Propiedades según la tabla en el sistema el 18/01/2000.
	'**+ The key fields correspond to nLed_compan, sBud_code,nYear, nCurrency, sAccount, sAux_accoun, sCost_cente and nMonth
	'+ Los campos llaves corresponden a nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_cente y nMonth
	
	'Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public sCodispl As String '                                                                                                          char                                                                                                                             no                                  8                       no                                  no                                  no
	Public nIdClass As Integer '                                                                                                       int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public nIdProperty As Integer '                                                                                                       int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public sVisible As String '                                                                                                       char                                                                                                                             no                                  1                       no                                  no                                  no
	Public nOrder As Integer '                                                                                                       int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public sCaption As String '                                                                                                       char                                                                                                                             no                                  40                      no                                  no                                  no
	Public nWidth As Integer '                                                                                                       decimal                                                                                                                          no                                  9           10    2     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer '
	Public sProperty As String
	
	'**- Define additional variables
	'- Se definen las variable auxiliares
	'**- Define the variable for indicate the status of each instance in the collection
	'- Se define la variable para indicar el estado de cada instancia en la colección
	
	Public nStatusInstance As Integer
	Private Enum eActions
		clngAdd = 1
		clndUpdate = 2
		clngDelete = 3
	End Enum
	
	Private mcolclassPropertyLibrary As ClassPropertiesWin
	Private mcolPropertyLibraries As PropertyLibraries
	
	
	'**% Add: add records in the budget results table.
	'% Add: Permite añadir registros en la tabla de resultados presupuestarios
	Public Function Add() As Boolean
		Add = insUpdClassPropertyWin(eActions.clngAdd)
	End Function
	
	'**% Update: modify records in the budget results table.
	'% Update: Permite modificar registros en la tabla de resultados presupuestarios
	Public Function Update() As Boolean
		Update = insUpdClassPropertyWin(eActions.clndUpdate)
	End Function
	
	'**% Delete: delete records in the budget results table.
	'% Delete: Permite eliminar registros en la tabla de resultados presupuestarios
	Public Function Delete() As Boolean
		Delete = insUpdClassPropertyWin(eActions.clngDelete)
	End Function
	
	'**% Find: search for records in the budget results table.
	'% Find: Permite buscar registros en la tabla de resultados presupuestarios
	Function Find(ByVal Codispl As String, ByVal IdClass As Integer, ByVal IdProperty As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaClassPropertyWin As eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		lrecreaClassPropertyWin = New eRemoteDB.Execute
		If Codispl = sCodispl And IdClass = nIdClass And IdProperty = nIdProperty And Not lblnFind Then
			Find = True
		Else
			With lrecreaClassPropertyWin
				.StoredProcedure = "reaClassPropertyWin"
				.Parameters.Add("sCodispl", Codispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nIdClass", IdClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("mIdProperty", IdProperty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					sCodispl = .FieldToClass("sCodispl")
					nIdClass = .FieldToClass("nIdClass")
					nIdProperty = .FieldToClass("nIdProperty")
					nOrder = .FieldToClass("nOrder")
					sCaption = .FieldToClass("sCaption")
					nWidth = .FieldToClass("nWidth")
					nUsercode = .FieldToClass("nUsercode")
					sProperty = .FieldToClass("sProperty")
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaClassPropertyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaClassPropertyWin = Nothing
		End If
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'*** Class_Initialize: controls the opening of the class.
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% insUpdClassPropertyWin: This function updats the ClassPropertyWin table
	'**% in the data base. As a parameter for the call of the SP, use the values containes in the class properties.
	'%insUpdClassPropertyWin. Esta funcion se encarga de realizar la actualización de la tabla ClassPropertyWin
	'%en la base de datos. Como parametro para la llamada a los SP, utiliza los valores
	'%contenidos en las propiedades de la clase
	Private Function insUpdClassPropertyWin(ByRef llngAction As eActions) As Boolean
		Dim lrecinsUpdClassPropertyWin As eRemoteDB.Execute
		
		On Error GoTo insUpdClassPropertyWin_err
		
		lrecinsUpdClassPropertyWin = New eRemoteDB.Execute
		
		
		'**+ Parameter definition for stored procedure 'insudb.insUpdClassPropertyWin'
		'+Definición de parámetros para stored procedure 'insudb.insUpdClassPropertyWin'
		'**+ Information read on July 11,2000 11:08:23
		'+Información leída el 11/07/2000 11:08:23
		
		With lrecinsUpdClassPropertyWin
			.StoredProcedure = "insClassPropertyWin"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdClass", nIdClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdProperty", nIdProperty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVisible", sVisible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCaption", sCaption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWidth", nWidth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdClassPropertyWin = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdClassPropertyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdClassPropertyWin = Nothing
		
insUpdClassPropertyWin_err: 
		If Err.Number Then
			insUpdClassPropertyWin = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insValMGE004_K: Validates the Maintenance of a Window's folder.
	'% insValMGE004_K: Valida el Mantenimiento de Folder de una Ventana
	Public Function insValMGE004_K(ByVal sCodispl As String, ByVal nIdClass As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGE004_K_err
		
		lclsErrors = New eFunctions.Errors
		
		If nIdClass = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012)
		End If
		
		insValMGE004_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMGE004_K_err: 
		If Err.Number Then
			insValMGE004_K = insValMGE004_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	
	'**% insValMGE004 Validates the Maintenance of a window's folder
	'% insValMGE004: Valida el Mantenimiento de Folder de una Ventana
	Public Function insValMGE004(ByVal sCodispl As String, ByVal sAction As String, ByVal nIdClass As Integer, ByVal nIdProperty As Integer, ByVal sVisible As String, ByVal sCaption As String, ByVal nOrder As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGE004_err
		
		lclsErrors = New eFunctions.Errors
		
		sAction = Trim(sAction)
		
		If sAction = "Add" Or sAction = "Update" Then
			If nIdProperty = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 1012)
			Else
				If sAction = "Add" Then
					If insClassPropertyLibExists(nIdProperty, nIdClass) Then
						Call lclsErrors.ErrorMessage(sCodispl, 12101)
					Else
						If Not insPropertyLibExists(nIdProperty) Then
							Call lclsErrors.ErrorMessage(sCodispl, 1012)
						End If
					End If
				End If
			End If
			If sCaption = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 2207)
			End If
			
		End If
		
		insValMGE004 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMGE004_err: 
		If Err.Number Then
			insValMGE004 = insValMGE004 & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMGE004: Updates the Maintenance of a window's folder.
	'% insPostMGE004: Actualiza el Mantenimiento de Folder de una Ventana
	Public Function insPostMGE004(ByVal sCodispl As String, ByVal sAction As String, ByVal nIdClass As Integer, ByVal nIdProperty As Integer, ByVal sVisible As String, ByVal sCaption As String, ByVal nOrder As Integer, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo insPostMGE004_err
		
		sAction = Trim(sAction)
		
		With Me
			.sCodispl = "GE099"
			.nIdClass = nIdClass
			.nIdProperty = nIdProperty
			If (Trim(sVisible) = "True" Or Trim(sVisible) = "1") Then
				.sVisible = "1"
			Else
				.sVisible = "2"
			End If
			.sCaption = sCaption
			.nOrder = nOrder
			.nWidth = 4000
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			
			'**+ If the selected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMGE004 = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMGE004 = Update
				
				'**+ If the slected option is Delete.
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMGE004 = Delete
				
		End Select
		
insPostMGE004_err: 
		If Err.Number Then
			insPostMGE004 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insClassPropertyLibExists. This function verifys that the property does not exists in the table, for the selected folder
	'%insClassPropertyLibExists. Esta función se encarga de verificar que la propiedad no exista en la tabla, para la carpeta
	'% seleccionada.
	Private Function insClassPropertyLibExists(ByVal lintProperty As Integer, ByVal nIdClass As Integer) As Boolean
		Dim lobjClassPropertyWin As ClassPropertyWin
		
		On Error GoTo insClassPropertyLibExists_err
		
		If mcolclassPropertyLibrary Is Nothing Then
			mcolclassPropertyLibrary = New ClassPropertiesWin
			Call mcolclassPropertyLibrary.Find("GE099", nIdClass)
		End If
		insClassPropertyLibExists = False
		For	Each lobjClassPropertyWin In mcolclassPropertyLibrary
			If lobjClassPropertyWin.nIdProperty = lintProperty Then
				insClassPropertyLibExists = True
				Exit For
			End If
		Next lobjClassPropertyWin
		
insClassPropertyLibExists_err: 
		If Err.Number Then
			insClassPropertyLibExists = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPropertyLibExists. This function validates the existence of the field in
	'**% the available fields table for the consult (PropertyLibrary)
	'%insPropertyLibExists. Esta funcion se encarga de validar la existencia del campo en la tabla de campos disponibles para
	'%la consulta (PropertyLibrary)
	Private Function insPropertyLibExists(ByVal lintProperty As Integer) As Boolean
		Dim lobjPropertyLib As PropertyLibrary
		
		On Error GoTo insPropertyLibExists_err
		
		insPropertyLibExists = False
		If mcolPropertyLibraries Is Nothing Then
			mcolPropertyLibraries = New PropertyLibraries
			Call mcolPropertyLibraries.Find()
		End If
		For	Each lobjPropertyLib In mcolPropertyLibraries
			If lobjPropertyLib.nIdProperty = lintProperty Then
				insPropertyLibExists = True
				Exit For
			End If
		Next lobjPropertyLib
		
insPropertyLibExists_err: 
		If Err.Number Then
			insPropertyLibExists = False
		End If
		On Error GoTo 0
		
	End Function
End Class






