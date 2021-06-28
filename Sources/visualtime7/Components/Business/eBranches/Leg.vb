Option Strict Off
Option Explicit On
Public Class Leg
	'%-------------------------------------------------------%'
	'% $Workfile:: Leg.cls                                  $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Descripción de la tabla Leg (Límite de emisión garantizada), al 10/10/2001
	'+ Los campos llave corresponden a nBranch, nProduct, dEffecdate, nCapitalI
	
	'+ Column_name                     Type          Nullable
	'--------------------------------- ------------- --------
	Public nBranch As Integer 'NUMBER(5)    No
	Public nProduct As Integer 'NUMBER(5)    No
	Public dEffecdate As Date 'DATE         No
	Public nCapitalI As Double 'NUMBER(12,3) No
	Public nCapitalF As Double 'NUMBER(12,3) Yes
	Public nAmountbas As Double 'NUMBER(5,3)  Yes
	Public nFact As Double 'NUMBER(5,3)  Yes
	Public nAmountmax As Double 'NUMBER(12,3) Yes
	Public nCurrency As Integer 'NUMBER(5)    Yes
	Public nUsercode As Integer 'NUMBER(5)
	
	'- Este arreglo se emplea para cargar las figuras definidas para un producto
	Private Structure udtLEG
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim dEffecdate As Date
		Dim nCapitalI As Double
		Dim nCapitalF As Double
		Dim nAmountbas As Double
		Dim nAmountmax As Double
		Dim nFact As Double
		Dim nCurrency As Integer
	End Structure
	
	Private marrLEG() As udtLEG
	
	'- Se define la variable para saber si se cargó el arreglo en la búsque a la BD
	Public mblnCharge As Boolean
	
	'% Find_All: se realiza la lectura de los datos sobre la tabla
	Public Function Find_All(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lintindex As Integer
		Dim lrecreaLeg As eRemoteDB.Execute
		
		On Error GoTo FindAll_err
		
		lrecreaLeg = New eRemoteDB.Execute
		
		Find_All = False
		
		With lrecreaLeg
			.StoredProcedure = "reaLeg"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_All = True
				lintindex = 0
				mblnCharge = True
				ReDim marrLEG(50)
				Do While Not .EOF
					marrLEG(lintindex).nBranch = nBranch
					marrLEG(lintindex).nProduct = nProduct
					marrLEG(lintindex).dEffecdate = dEffecdate
					marrLEG(lintindex).nCapitalI = .FieldToClass("nCapitalI")
					marrLEG(lintindex).nCapitalF = .FieldToClass("nCapitalF")
					marrLEG(lintindex).nAmountbas = .FieldToClass("nAmountbas")
					marrLEG(lintindex).nFact = .FieldToClass("nFact")
					marrLEG(lintindex).nAmountmax = .FieldToClass("nAmountmax")
					marrLEG(lintindex).nCurrency = .FieldToClass("nCurrency")
					lintindex = lintindex + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve marrLEG(lintindex - 1)
			End If
		End With
		
FindAll_err: 
		If Err.Number Then
			Find_All = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLeg may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLeg = Nothing
	End Function
	
	'% insValMVI706_K: se realizan las validaciones de los campos que pertenecen al encabezado de la forma
	Public Function insValMVI706_K(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMVI706_Err
		
		lobjErrors = New eFunctions.Errors
		lvalField = New eFunctions.valField
		lvalField.objErr = lobjErrors
		
		lblnErrors = False
		
		With lobjErrors
			If nBranch = eRemoteDB.Constants.intNull Then
				'+ El ramo debe estar lleno
				Call .ErrorMessage("MVI706", 9064)
				lblnErrors = True
			End If
			
			If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
				'+ El producto debe estar lleno
				Call .ErrorMessage("MVI706", 11009)
				lblnErrors = True
			End If
			
			If dEffecdate = dtmNull Then
				'+ El campo Fecha debe estar lleno
				Call .ErrorMessage("MVI706", 5055)
				lblnErrors = True
			Else
				If lvalField.ValDate(dEffecdate,  , eFunctions.valField.eTypeValField.onlyvalid) Then
					If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
						If InsValEffecdate(nBranch, nProduct, dEffecdate) Then
							'+ Debe ser mayor o igual a la fecha de última actualización
							If dEffecdate < CDate(Format(Me.dEffecdate, "yyyy/MM/dd")) Then
								Call .ErrorMessage("MVI706", 55611,  , eFunctions.Errors.TextAlign.RigthAling, " (" & Me.dEffecdate & ")")
								lblnErrors = True
							End If
						End If
					End If
				End If
			End If
			
			insValMVI706_K = .Confirm
		End With
		
insValMVI706_Err: 
		If Err.Number Then
			insValMVI706_K = insValMVI706_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
	End Function
	
	'% insValMVI706: se realizan las validaciones de los campos que pertenecen al detalle de la forma
	Public Function insValMVI706(ByVal sWindowType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nCapitalI As Double, ByVal nCapitalF As Double, ByVal nIndex As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lvalField As eFunctions.valField
		
		On Error GoTo insValMVI706_Err
		
		lobjErrors = New eFunctions.Errors
		lvalField = New eFunctions.valField
		lvalField.objErr = lobjErrors
		
		With lobjErrors
			If nCurrency = eRemoteDB.Constants.intNull Then
				'+ La moneda debe estar llena
				Call .ErrorMessage("MVI706", 10827)
			End If
			
			If nCapitalI = eRemoteDB.Constants.intNull Or nCapitalI = 0 Then
				'+ El capital inicial debe estar lleno
				Call .ErrorMessage("MVI706", 10163)
			Else
				'+ No puede estar incluido en otro rango del mismo ramo-producto
				If Not insValCapital(nBranch, nProduct, dEffecdate, nCapitalI, nIndex) Then
					Call .ErrorMessage("MVI706", 10185,  , eFunctions.Errors.TextAlign.LeftAling, "Capital inicial:")
				End If
			End If
			
			If nCapitalF <> eRemoteDB.Constants.intNull Then
				'+ El capital inicial debe estar lleno
				If nCapitalF <= nCapitalI Then
					Call .ErrorMessage("MVI706", 10148)
				End If
				
				If nCapitalF > 0 Then
					'+ No puede estar incluido en otro rango del mismo ramo-producto
					If Not insValCapital(nBranch, nProduct, dEffecdate, nCapitalF, nIndex) Then
						Call .ErrorMessage("MVI706", 10185,  , eFunctions.Errors.TextAlign.LeftAling, "Capital final:")
					End If
				End If
			End If
			insValMVI706 = .Confirm
		End With
		
insValMVI706_Err: 
		If Err.Number Then
			insValMVI706 = insValMVI706 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalField = Nothing
	End Function
	
	'% insValEffecdate: realiza la búsqueda de la última actualización sobre la tabla
	Private Function InsValEffecdate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaLeg_maxEffecdate As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		
		lrecreaLeg_maxEffecdate = New eRemoteDB.Execute
		
		With lrecreaLeg_maxEffecdate
			.StoredProcedure = "realeg_maxeffecdate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				InsValEffecdate = True
				Me.dEffecdate = .FieldToClass("dMax_Effecdate")
				.RCloseRec()
			End If
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLeg_maxEffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLeg_maxEffecdate = Nothing
	End Function
	
	'% insPostMVI706: se realizan las actualizaciones de las tablas
	Public Function insPostMVI706(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapitalI As Double, Optional ByVal nCapitalF As Double = 0, Optional ByVal nAmountbas As Double = 0, Optional ByVal nFact As Double = 0, Optional ByVal nAmountmax As Double = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		On Error GoTo insPostMVI706_err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nCapitalI = nCapitalI
			.nCapitalF = nCapitalF
			.nAmountbas = nAmountbas
			.nFact = nFact
			.nAmountmax = nAmountmax
			.nCurrency = nCurrency
			.nUsercode = nUsercode
			'+ Se asigna valor a la acción para se tomada en el SP
			Select Case sAction
				Case "Add"
					insPostMVI706 = .Add()
				Case "Update"
					insPostMVI706 = .Update(2)
				Case "Del"
					insPostMVI706 = .Delete()
			End Select
		End With
		
insPostMVI706_err: 
		If Err.Number Then
			insPostMVI706 = False
		End If
		On Error GoTo 0
	End Function
	
	'% Add: se crean los registros en la tabla
	Public Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se eliminan los registros en la tabla
	Public Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: actualiza los campos de la tabla
	Public Function Update(ByVal nAction As Integer) As Boolean
		Dim lrecinsUpdLEG As eRemoteDB.Execute
		
		On Error GoTo insUpdLEG_err
		
		lrecinsUpdLEG = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insUpdLEG'
		'Información leída el 11/10/2001
		
		With lrecinsUpdLEG
			.StoredProcedure = "insUpdLEG"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitalI", nCapitalI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapitalF", nCapitalF, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountbas", nAmountbas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFact", nFact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountMax", nAmountmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
insUpdLEG_err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdLEG may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdLEG = Nothing
	End Function
	
	'% Item: Función que tomando en cuenta el valor del parámetro carga en las variables
	'%       de la clase la información del arreglo
	Public Function Item(ByVal lintindex As Integer) As Boolean
		If mblnCharge Then
			If lintindex <= UBound(marrLEG) Then
				With marrLEG(lintindex)
					Me.nBranch = .nBranch
					Me.nProduct = .nProduct
					Me.dEffecdate = .dEffecdate
					Me.nCapitalI = .nCapitalI
					Me.nCapitalF = .nCapitalF
					Me.nAmountbas = .nAmountbas
					Me.nAmountmax = .nAmountmax
					Me.nFact = .nFact
					Me.nCurrency = .nCurrency
				End With
				Item = True
			Else
				Item = False
			End If
		End If
	End Function
	
	'% insValCapital: Valida que los capitales no se encuentren en otro rango
	Private Function insValCapital(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital As Double, ByVal nIndex As Integer) As Boolean
		Dim lintindex As Integer
		Dim lblnFind As Boolean
		
		On Error GoTo insValCapital_err
		
		insValCapital = True
		
		lblnFind = mblnCharge
		
		If Not mblnCharge Then
			lblnFind = Find_All(nBranch, nProduct, dEffecdate)
		End If
		
		If lblnFind Then
			For lintindex = 0 To CountItem
				Call Item(lintindex)
				'+ Si no es el registro con el cual se está trabajando, se verifica que no se encuentre
				'+ dentro de otro rango
				If nIndex <> lintindex Then
					If nCapital >= nCapitalI And nCapital <= nCapitalF Then
						insValCapital = False
						Exit For
					End If
				End If
			Next lintindex
		End If
		
insValCapital_err: 
		If Err.Number Then
			insValCapital = False
		End If
		On Error GoTo 0
	End Function
	
	'% CountItem: propiedad que indica el número de registros que se encuentra en determinado
	'%            momento en el arreglo de la clase
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(marrLEG)
			Else
				CountItem = -1
			End If
		End Get
	End Property
	
	'* Class_Initialize: se controla la creación de la instancia del objeto
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nCapitalI = eRemoteDB.Constants.intNull
		nCapitalF = eRemoteDB.Constants.intNull
		nAmountbas = eRemoteDB.Constants.intNull
		nFact = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
		nAmountmax = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






