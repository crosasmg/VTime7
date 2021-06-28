Option Strict Off
Option Explicit On
Public Class Folder
	'%-------------------------------------------------------%'
	'% $Workfile:: Folder.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**+ Properties according to the table in the system on January 18,2000.
	'+ Propiedades según la tabla en el sistema el 18/01/2000.
	'**+ The key fields corresponds to nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_centeand nMonth
	'+ Los campos llaves corresponden a nLed_compan, sBud_code, nYear, nCurrency, sAccount, sAux_accoun, sCost_cente y nMonth
	
	'Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nFolder As Integer '                                                                                                   smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sFolderName As String '                                                                                                                      char                                                                                                                             no                                  30                      no                                  no                                  no
	Public sRootName As String '                                                                                                                        char                                                                                                                             no                                  30                      no                                  no                                  no
	Public nImage As Integer '                                                                                                                           smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public sClass As String '                                                                                                                           char                                                                                                                             no                                  30                      yes                                 no                                  yes
	Public sFolderKey As String '                                                                                                                     char                                                                                                                             no                                  25                      yes                                 no                                  yes
	Public nFolderKey As Integer '
	Public nUsercode As Integer '
	Public nQuantImages As Integer
	'----------------------------------------------------
	Public sOrigi As String
	'**- Define the auxiliary variables
	'- Se definen las variable auxiliares
	'**- Define the variable to indicate the status of each instance in the collection.
	'- Se define la variable para indicar el estado de cada instancia en la colección
	
	Public nStatusInstance As Integer
	Private Enum eActions
		clngAdd = 1
		clndUpdate = 2
		clngDelete = 3
	End Enum
	
	
	'**% Add: Allows to add records in the budget results table.
	'% Add: Permite añadir registros en la tabla de resultados presupuestarios
	Public Function Add() As Boolean
		Add = insUpdFolder(eActions.clngAdd)
	End Function
	
	'**% Update: Allows to modify records in the budget results table.
	'% Update: Permite modificar registros en la tabla de resultados presupuestarios
	Public Function Update() As Boolean
		Update = insUpdFolder(eActions.clndUpdate)
	End Function
	
	'**% Delete: allows to delete records in the budget results table.
	'% Delete: Permite eliminar registros en la tabla de resultados presupuestarios
	Public Function Delete() As Boolean
		Delete = insUpdFolder(eActions.clngDelete)
	End Function
	
	'**% Find: Allows to search records in the budget results table.
	'% Find: Permite buscar registros en la tabla de resultados presupuestarios
	
	Function Find(ByVal Folder As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaFolder As eRemoteDB.Execute
		lrecreaFolder = New eRemoteDB.Execute
		If Folder = nFolder And Not lblnFind Then
			Find = True
		Else
			With lrecreaFolder
				.StoredProcedure = "reaFolders"
				.Parameters.Add("nFolder", Folder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					nFolder = .FieldToClass("nFolder")
					sFolderName = .FieldToClass("sFolderName")
					sRootName = .FieldToClass("sRootName")
					nImage = .FieldToClass("nImage")
					sClass = .FieldToClass("sClass")
					sFolderKey = .FieldToClass("sFolderKey")
					nFolderKey = .FieldToClass("nFolderKey")
					sOrigi = .FieldToClass("sOrigi")
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaFolder = Nothing
		End If
	End Function
	
	'*** Class_Initialize: controls the opening of the class.
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nQuantImages = 5
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**% insUpdFolder. This function is in charge of making the update of the Folder table
	'**% in the database. As a parameter for the call of the SP, use the contained values in the
	'**% class properties.
	'%insUpdFolder. Esta funcion se encarga de realizar la actualización de la tabla Folder
	'%en la base de datos. Como parametro para la llamada a los SP, utiliza los valores
	'%contenidos en las propiedades de la clase
	Private Function insUpdFolder(ByRef llngAction As eActions) As Boolean
		Dim lrecinsUpdFolder As eRemoteDB.Execute
		lrecinsUpdFolder = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.insUpdFolder'
		'+Definición de parámetros para stored procedure 'insudb.insUpdFolder'
		'**+ Information read on July 11,2000  11:08:23
		'+Información leída el 11/07/2000 11:08:23
		
		With lrecinsUpdFolder
			.StoredProcedure = "insFolders"
			.Parameters.Add("nFolder", nFolder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFolderName", sFolderName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRootName", sRootName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFolderKey", sFolderKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFolderKey", nFolderKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If nImage = eRemoteDB.Constants.intNull Then
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nImage", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nImage", nImage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("sClass", sClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdFolder = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdFolder may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdFolder = Nothing
	End Function
	
	'**% insValMGE002_K: Validates the Folder Maintenance.
	'% insValMGE002_K: Valida el el Mantenimiento de Folder
	Public Function insValMGE002_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nFolder As Integer, ByVal sFolderName As String, ByVal sRootName As String, ByVal nImage As Integer, ByVal sClass As String, ByVal nFolderKey As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMGE002_K_err
		
		lclsErrors = New eFunctions.Errors
		
		sAction = Trim(sAction)
		
		If sAction = "Add" Then
			If nFolder = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 10842)
			End If
			If Find(nFolder) Then
				Call lclsErrors.ErrorMessage(sCodispl, 12101)
			End If
		End If
		If (sFolderName = String.Empty Or sRootName = String.Empty) Then
			Call lclsErrors.ErrorMessage(sCodispl, 2207)
		End If
		
		insValMGE002_K = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMGE002_K_err: 
		If Err.Number Then
			insValMGE002_K = insValMGE002_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'**% insPostMGE002_K: Validates the Folder Maintenance.
	'% insPostMGE002_K: Valida el el Mantenimiento de Folder
	Public Function insPostMGE002_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nFolder As Integer, ByVal sFolderName As String, ByVal sRootName As String, ByVal nImage As Integer, ByVal sClass As String, ByVal nFolderKey As Integer, ByVal nUsercode As Integer) As Boolean
		
		Dim lclsPropertyLibrary As PropertyLibrary
		
		On Error GoTo insPostMGE002_K_err
		
		lclsPropertyLibrary = New PropertyLibrary
		
		sAction = Trim(sAction)
		
		With Me
			.nFolder = nFolder
			.sFolderName = sFolderName
			.sRootName = sRootName
			.nImage = nImage
			.sClass = sClass
			.nFolderKey = nFolderKey
			If lclsPropertyLibrary.Find(nFolderKey) Then
				.sFolderKey = lclsPropertyLibrary.sProperty
			Else
				.sFolderKey = ""
			End If
			.nUsercode = nUsercode
		End With
		Select Case sAction
			
			'**+ If the slected option is Add
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMGE002_K = Add
				
				'**+ If the selected option is Modify
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMGE002_K = Update
				
				'**+ If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMGE002_K = Delete
				
		End Select
		
insPostMGE002_K_err: 
		If Err.Number Then
			insPostMGE002_K = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% PathImages: Allows to save the images route.
	'% PathImages: Permite guardar la ruta de las imágenes.
	Public Function PathImages(ByVal nIndex As Integer) As String
		PathImages = String.Empty
		Select Case nIndex
			Case 1
				PathImages = "/VTimeNet/images/GenQue10.gif"
			Case 2
				PathImages = "/VTimeNet/images/GenQue15.gif"
			Case 3
				PathImages = "/VTimeNet/images/GenQue16.gif"
			Case 4
				PathImages = "/VTimeNet/images/GenQue19.gif"
			Case 5
				PathImages = "/VTimeNet/images/GenQue21.gif"
			Case 11
				PathImages = "/VTimeNet/images/GenQue11.gif"
			Case Else
                PathImages = "/VTimeNet/images/clfolder.png"
        End Select
	End Function
End Class






