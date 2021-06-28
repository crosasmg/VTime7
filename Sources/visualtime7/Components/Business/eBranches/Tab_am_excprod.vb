Option Strict Off
Option Explicit On
Public Class Tab_am_excprod
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_excprod.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 17                                       $%'
	'%-------------------------------------------------------%'
	
	'Column_name                  Type      Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'--------------------------- --------- ---------- -------- ----- ----- --------- -------------------- ----------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nTariff As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public sIllness As String ' CHAR       8    0     0    N
	Public nExc_code As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public dInit_date As Date ' DATE       7    0     0    S
	Public dEnd_date As Date ' DATE       7    0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	Public dEffecdate_reg As Date
	Public dEffecdate_Temp As Date
	
	Public sDescript As String
	
	
	'-Variable que contiene la descripción de la enfermedad
	
	Public sDes_Illness As String
	
	'-Variable que contiene el estado del registro
	
	Public nStatInstanc As Tar_am_bas.eStatusInstance1
	
	
	'-Variables que almacenaran los valores para condicionar la consulta
	
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mintTariff As Integer
	Private mdtmEffecdate As Date
	
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	
	Private Structure typTab_am_excprod
		Dim nStatInstanc As Tar_am_bas.eStatusInstance1
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nTariff As Integer
		Dim dEffecdate As Date
		Dim sIllness As String
		Dim nExc_code As Integer
		Dim dInit_date As Date
		Dim dEnd_date As Date
		Dim dNulldate As Date
		Dim sDes_Illness As String
	End Structure
	
	Private mudtTab_am_excprod() As typTab_am_excprod
	
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	
	Private mblnCharge As Boolean
	Private mvarTab_am_excprods As Tab_am_excprods
	
	
	
	
	Public Property Tab_am_excprods() As Tab_am_excprods
		Get
			If mvarTab_am_excprods Is Nothing Then
				mvarTab_am_excprods = New Tab_am_excprods
			End If
			
			
			Tab_am_excprods = mvarTab_am_excprods
		End Get
		Set(ByVal Value As Tab_am_excprods)
			mvarTab_am_excprods = Value
		End Set
	End Property
	
	'*CountItem: Propiedad que indica el número de elementos en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(mudtTab_am_excprod)
			Else
				CountItem = -1
			End If
		End Get
	End Property
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarTab_am_excprods may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTab_am_excprods = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Load: Permite consultar las enfermedades excluídas para una tarifa o producto
	Public Function Load(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintTariff As Integer, ByVal ldtmEffecdate As Date, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_am_excprod As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo reaTab_am_excprod_Err
		
		If lintBranch <> mintBranch Or lintProduct <> mintProduct Or lintTariff <> mintTariff Or ldtmEffecdate <> mdtmEffecdate Or lblnFind Then
			
			lrecreaTab_am_excprod = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'insudb.reaTab_am_excprod'
			'+Información leída el 26/01/2000 10:17:27
			
			With lrecreaTab_am_excprod
				.StoredProcedure = "reaTab_am_excprod"
				.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTariff", lintTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sTypeexcl", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ReDim mudtTab_am_excprod(50)
					lintPos = 0
					Do While Not .EOF
						mudtTab_am_excprod(lintPos).nStatInstanc = Insured_he.eStatusInstance.eftExist
						mudtTab_am_excprod(lintPos).nBranch = lintBranch
						mudtTab_am_excprod(lintPos).nProduct = lintProduct
						mudtTab_am_excprod(lintPos).nTariff = lintTariff
						mudtTab_am_excprod(lintPos).dEffecdate = .FieldToClass("dEffecdate")
						mudtTab_am_excprod(lintPos).sIllness = .FieldToClass("sIllness")
						mudtTab_am_excprod(lintPos).nExc_code = .FieldToClass("nExc_code")
						mudtTab_am_excprod(lintPos).dInit_date = .FieldToClass("dInit_date")
						mudtTab_am_excprod(lintPos).dEnd_date = .FieldToClass("dEnd_date")
						mudtTab_am_excprod(lintPos).dNulldate = .FieldToClass("dNulldate")
						mudtTab_am_excprod(lintPos).sDes_Illness = .FieldToClass("sDescript")
						lintPos = lintPos + 1
						.RNext()
					Loop 
					
					Load = True
					
					ReDim Preserve mudtTab_am_excprod(lintPos - 1)
					.RCloseRec()
				End If
			End With
			mintBranch = lintBranch
			mintProduct = lintProduct
			mintTariff = lintTariff
			mdtmEffecdate = ldtmEffecdate
			
		Else
			Load = mblnCharge
		End If
		mblnCharge = Load
		
reaTab_am_excprod_Err: 
		If Err.Number Then
			Load = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_am_excprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_excprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tab_am_excprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lintCount As Integer
		Dim lreccreTab_am_excprod As eRemoteDB.Execute
		
		On Error GoTo creTab_am_excprod_Err
		
		lreccreTab_am_excprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creTab_am_excprod'
		'+Información leída el 26/01/2000 10:35:40
		
		With lreccreTab_am_excprod
			.StoredProcedure = "creTab_am_excprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExc_code", nExc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
				
			End If
		End With
		
creTab_am_excprod_Err: 
		If Err.Number Then
			Add = False
		End If
		
		'UPGRADE_NOTE: Object lreccreTab_am_excprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_am_excprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Tab_am_excprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lintPos As Integer
		Dim lrecupdTab_am_excprod As eRemoteDB.Execute
		
		On Error GoTo updTab_am_excprod_Err
		
		lrecupdTab_am_excprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updTab_am_excprod'
		'+Información leída el 26/01/2000 13:23:15
		
		With lrecupdTab_am_excprod
			.StoredProcedure = "updTab_am_excprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExc_code", nExc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		
updTab_am_excprod_Err: 
		If Err.Number Then
			Update = False
		End If
		
		'UPGRADE_NOTE: Object lrecupdTab_am_excprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_am_excprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Tab_am_excprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lintPos As Integer
		Dim lrecdelTab_am_excprod As eRemoteDB.Execute
		
		On Error GoTo delTab_am_excprod_Err
		
		lrecdelTab_am_excprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delTab_am_excprod'
		'+Información leída el 26/01/2000 13:28:06
		
		With lrecdelTab_am_excprod
			.StoredProcedure = "delTab_am_excprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		
delTab_am_excprod_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		'UPGRADE_NOTE: Object lrecdelTab_am_excprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_am_excprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByRef lintindex As Integer) As Boolean
		If lintindex <= CountItem Then
			Item = True
			With mudtTab_am_excprod(lintindex)
				nStatInstanc = .nStatInstanc
				nBranch = .nBranch
				nProduct = .nProduct
				nTariff = .nTariff
				dEffecdate = .dEffecdate
				sIllness = .sIllness
				nExc_code = .nExc_code
				dInit_date = .dInit_date
				dEnd_date = .dEnd_date
				dNulldate = .dNulldate
				sDes_Illness = .sDes_Illness
			End With
		End If
	End Function
	
	'%FindItem: Permite encontrar un elemento del arreglo de acuerdo al código de la enfermedad
	Public Function FindItem(ByRef lstrIllness As String, Optional ByRef lblnItem As Boolean = False) As Boolean
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTab_am_excprod(lintPos).sIllness = lstrIllness Then
				lblnFind = True
				FindItem = IIf(lblnItem, Item(lintPos), True)
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByRef lstrIllness As String) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTab_am_excprod(lintPos).sIllness = lstrIllness Then
				lblnFind = True
				Position = lintPos
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nTariff = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		sIllness = strNull
		nExc_code = eRemoteDB.Constants.intNull
		dInit_date = dtmNull
		dEnd_date = dtmNull
		dNulldate = dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		sDes_Illness = strNull
		
		mintBranch = eRemoteDB.Constants.intNull
		mintProduct = eRemoteDB.Constants.intNull
		mintTariff = eRemoteDB.Constants.intNull
		mdtmEffecdate = dtmNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%insValDP058: Función que permite efectuar las validaciones.
	Public Function insValDP058(ByVal sCodispl As String, ByVal sAction As String, ByVal nTariff As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sIllness As String, ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal nExc_code As Integer, ByVal sOpttype_excl As String) As String
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP058_Err
		lclsErrors = New eFunctions.Errors
		
		'+ Validación de la tarifa
		If sOpttype_excl = "2" Then
			If nTariff <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 3550)
			End If
		End If
		
		'+Validación del campo "Código de la enfermedad ".
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(sIllness) Or sIllness = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 4230)
		Else
			If sAction = "Add" Then
				If insValDuplicate(sIllness, nBranch, nProduct, nTariff, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3609)
				End If
			End If
		End If
		
		'+ Validación de la causa de exclusión de la enfermedad
		If nExc_code <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3978)
		End If
		
		'+ Validación de la fecha desde de exclusión de la enfermedad
		If (dInit_date = dtmNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 3565,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha desde : ")
		Else
			If CDate(dInit_date) < dEffecdate Then
				Call lclsErrors.ErrorMessage(sCodispl, 11422,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha desde : ")
			End If
		End If
		
		'+ Validación de la fecha hasta de exclusión de la enfermedad
		If dEnd_date < dEffecdate Then
			If dEnd_date <> dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 11422,  , eFunctions.Errors.TextAlign.LeftAling, "Fecha hasta : ")
			End If
		Else
			If dEnd_date < dInit_date Then
				Call lclsErrors.ErrorMessage(sCodispl, 11425)
			End If
		End If
		
		insValDP058 = lclsErrors.Confirm
		
insValDP058_Err: 
		If Err.Number Then
			insValDP058 = "insValDP058: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostDP057: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla Tar_am_deprod
	Public Function insPostDP058(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal sIllness As String, ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal nExc_code As Integer, ByVal dEffecdate As Date, ByVal dEffecdate_reg As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP058_err
		insPostDP058 = True
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			.dEffecdate_reg = dEffecdate_reg
			
			.nTariff = nTariff
			.sIllness = sIllness
			.dInit_date = dInit_date
			.nExc_code = nExc_code
			.dEnd_date = dEnd_date
		End With
		
		Select Case sAction
			'+Si la opción seleccionada es Registrar
			Case "Add"
				If dEffecdate_reg <> dtmNull Then
					If dEffecdate_reg <> dEffecdate Then
						dEffecdate_Temp = dEffecdate
						Me.dNulldate = dEffecdate
						Me.dEffecdate = dEffecdate_reg
						insPostDP058 = Update()
						Me.dNulldate = dtmNull
						Me.dEffecdate = dEffecdate_Temp
						insPostDP058 = Add()
					End If
				Else
					Me.dNulldate = dtmNull
					insPostDP058 = Add()
				End If
				
				'+Si la opción seleccionada es Modificar
			Case "Update"
				If dEffecdate_reg <> dEffecdate Then
					dEffecdate_Temp = dEffecdate
					Me.dNulldate = dEffecdate
					Me.dEffecdate = dEffecdate_reg
					insPostDP058 = Update()
					Me.dNulldate = dtmNull
					Me.dEffecdate = dEffecdate_Temp
					insPostDP058 = Add()
				Else
					insPostDP058 = Update()
				End If
				
				'+Si la opción seleccionada es Eliminar
			Case "Del"
				If dEffecdate_reg <> dEffecdate Then
					dEffecdate_Temp = dEffecdate
					Me.dNulldate = dEffecdate
					Me.dEffecdate = dEffecdate_reg
					insPostDP058 = Update()
					Me.dNulldate = dtmNull
					Me.dEffecdate = dEffecdate_Temp
				Else
					insPostDP058 = Delete()
				End If
		End Select
		
		If insPostDP058 Then
			lclsProd_win = New eProduct.Prod_win
			If valTab_am_excProd_O(nBranch, nProduct, dEffecdate) Then
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP058", "2", nUsercode)
			Else
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP058", "1", nUsercode)
			End If
		End If
		
insPostDP058_err: 
		If Err.Number Then
			insPostDP058 = False
		End If
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		On Error GoTo 0
	End Function
	
	'%FindHospital: Permite encontrar un elemento en la tabla de acuerdo al código de la clínica
	Public Function insValDuplicate(ByVal sIllness As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_excprod_dup As eRemoteDB.Execute
		
		On Error GoTo reaTab_am_excprod_dup_Err
		
		lrecreaTab_am_excprod_dup = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaTab_am_excprod'
		'+Información leída el 26/01/2000 10:17:27
		
		With lrecreaTab_am_excprod_dup
			.StoredProcedure = "reaTab_am_excprod_Dup"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insValDuplicate = True
				.RCloseRec()
			End If
		End With
		
reaTab_am_excprod_dup_Err: 
		If Err.Number Then
			insValDuplicate = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_am_excprod_dup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_excprod_dup = Nothing
		On Error GoTo 0
		
	End Function
	
	'%valTab_am_excProd: devuelve la fecha de última modificación de la tabla
	Public Function valTab_am_excProd(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valTab_am_excProd_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "valExistsTab_am_excProd"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valTab_am_excProd = True
			End If
		End With
		
valTab_am_excProd_Err: 
		If Err.Number Then
			valTab_am_excProd = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'%valTab_am_excProd_O: Verifica si existen registros en la tabla Tab_am_excProd
	Public Function valTab_am_excProd_O(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valTab_am_excProd_O_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		With lclsExecute
			.StoredProcedure = "valExistsTab_am_excProd_O"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			If .Parameters("nExists").Value = 1 Then
				valTab_am_excProd_O = True
			End If
		End With
		
valTab_am_excProd_O_Err: 
		If Err.Number Then
			valTab_am_excProd_O = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
End Class






