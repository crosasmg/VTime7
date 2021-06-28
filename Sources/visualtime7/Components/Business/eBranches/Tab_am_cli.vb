Option Strict Off
Option Explicit On
Public Class Tab_am_cli
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_cli.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla TAB_AM_CLI "Clínicas permitidas", al 05-20-2002 15:57:45
	'- Column_name                  Type        Computed   Length  Prec  Scale Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'---------------------------- ----------- ---------- -------- ----- ----- --------- -------------------- ----------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nHospital As Integer ' NUMBER     22   0     10   N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	
	Public sCliename As String
	Public dEffecdate_reg As Date
	Public dEffecdate_Temp As Date
	
	'-Variable que contiene el nombre de la clínica
	
	Public sDescript As String
	
	'-Variable que contiene el estado del registro
	Public nStatInstanc As Tar_am_bas.eStatusInstance1
	
	'-Variables que almacenaran los valores para condicionar la consulta
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mdtmEffecdate As Date
	
	'- Variable para indicar si existen registros en la tabla TAB_AM_CLI
	Private mintHospitalExist As Integer
	
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	
	Private Structure typTab_am_cli
		Dim nStatInstanc As Tar_am_bas.eStatusInstance1
		Dim nBranch As Integer
		Dim nHospital As Integer
		Dim nProduct As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim sDescript As String
	End Structure
	
	Private mudtTab_am_cli() As typTab_am_cli
	
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	Private mblnCharge As Boolean
	Private mvarTab_am_clis As Tab_am_clis
	
	'%Se crea la clase correspondiente
	
	'%Se setea la clase
	Public Property Tab_am_clis() As Tab_am_clis
		Get
			If mvarTab_am_clis Is Nothing Then
				mvarTab_am_clis = New Tab_am_clis
			End If
			
			Tab_am_clis = mvarTab_am_clis
		End Get
		Set(ByVal Value As Tab_am_clis)
			mvarTab_am_clis = Value
		End Set
	End Property
	
	'*CountItem: Propiedad que indica el número de elementos en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(mudtTab_am_cli)
			Else
				CountItem = -1
			End If
		End Get
	End Property
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarTab_am_clis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTab_am_clis = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Load : Permite consultar las clínicas permitidas para el producto
	Public Function Load(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal ldtmEffecdate As Date, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_am_cli_1 As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo reaTab_am_cli_1_Err
		
		If lintBranch <> mintBranch Or lintProduct <> mintProduct Or ldtmEffecdate <> mdtmEffecdate Or lblnFind Then
			
			lrecreaTab_am_cli_1 = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'insudb.reaTab_am_cli_1'
			'+Información leída el 28/01/2000 13:59:17
			
			With lrecreaTab_am_cli_1
				.StoredProcedure = "reaTab_am_cli_1"
				.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ReDim mudtTab_am_cli(50)
					lintPos = 0
					Do While Not .EOF
						mudtTab_am_cli(lintPos).nStatInstanc = Insured_he.eStatusInstance.eftExist
						mudtTab_am_cli(lintPos).nBranch = lintBranch
						mudtTab_am_cli(lintPos).nProduct = lintProduct
						mudtTab_am_cli(lintPos).nHospital = .FieldToClass("nHospital")
						mudtTab_am_cli(lintPos).dEffecdate = .FieldToClass("dEffecdate")
						mudtTab_am_cli(lintPos).dNulldate = .FieldToClass("dNulldate")
						mudtTab_am_cli(lintPos).sDescript = .FieldToClass("sCliename")
						lintPos = lintPos + 1
						.RNext()
					Loop 
					
					Load = True
					
					ReDim Preserve mudtTab_am_cli(lintPos - 1)
					.RCloseRec()
				End If
			End With
			mintBranch = lintBranch
			mintProduct = lintProduct
			mdtmEffecdate = ldtmEffecdate
		Else
			Load = mblnCharge
		End If
		mblnCharge = Load
		
reaTab_am_cli_1_Err: 
		If Err.Number Then
			Load = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaTab_am_cli_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_cli_1 = Nothing
		On Error GoTo 0
		
	End Function
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tab_am_cli". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lintCount As Integer
		
		Dim lreccreTab_am_cli_1 As eRemoteDB.Execute
		
		On Error GoTo creTab_am_cli_1_Err
		lreccreTab_am_cli_1 = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creTab_am_cli_1'
		'+Información leída el 28/01/2000 14:38:40
		
		With lreccreTab_am_cli_1
			.StoredProcedure = "creTab_am_cli_1"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHospital", nHospital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Add = True
			End If
		End With
		
creTab_am_cli_1_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreTab_am_cli_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_am_cli_1 = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Tab_am_cli". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lintPos As Integer
		
		Dim lrecupdTab_am_cli As eRemoteDB.Execute
		
		On Error GoTo updTab_am_cli_Err
		
		lrecupdTab_am_cli = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updTab_am_cli'
		'+Información leída el 28/01/2000 14:41:38
		
		With lrecupdTab_am_cli
			.StoredProcedure = "updTab_am_cli"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHospital", nHospital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Update = True
			End If
		End With
		
updTab_am_cli_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdTab_am_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_am_cli = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Tab_am_cli". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lintPos As Integer
		
		Dim lrecdelTab_am_cli As eRemoteDB.Execute
		
		On Error GoTo delTab_am_cli_Err
		
		lrecdelTab_am_cli = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delTab_am_cli'
		'+Información leída el 28/01/2000 14:45:54
		
		With lrecdelTab_am_cli
			.StoredProcedure = "delTab_am_cli"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHospital", nHospital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		
delTab_am_cli_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTab_am_cli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_am_cli = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByRef lintindex As Integer) As Boolean
		If lintindex <= CountItem Then
			Item = True
			With mudtTab_am_cli(lintindex)
				nStatInstanc = .nStatInstanc
				nBranch = .nBranch
				nProduct = .nProduct
				nHospital = .nHospital
				dEffecdate = .dEffecdate
				dNulldate = .dNulldate
				sDescript = .sDescript
			End With
		End If
	End Function
	
	'%FindItem: Permite encontrar un elemento del arreglo de acuerdo al código de la clínica
	Public Function FindItem(ByRef lintHospital As Integer, Optional ByRef lblnItem As Boolean = False) As Boolean
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTab_am_cli(lintPos).nHospital = lintHospital Then
				lblnFind = True
				FindItem = IIf(lblnItem, Item(lintPos), True)
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByRef lintHospital As Integer) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTab_am_cli(lintPos).nHospital = lintHospital Then
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
		nHospital = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		dNulldate = dtmNull
		sDescript = strNull
		nUsercode = eRemoteDB.Constants.intNull
		
		mintBranch = eRemoteDB.Constants.intNull
		mintProduct = eRemoteDB.Constants.intNull
		mdtmEffecdate = dtmNull
		mintHospitalExist = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%insValDP059: Función que permite efectuar las validaciones.
	Public Function insValDP059(ByVal sCodispl As String, ByVal sAction As String, ByVal nHospital As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValDP059_Err
		
		'+Si se trata de una validación masiva es necesario mover el punto del grid a la primera posición.
		
		'+Validación del campo "Tarifa".
		
		If nHospital <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11428)
		End If
		
		If nHospital > 0 And sAction = "Add" Then
			If FindHospital(nHospital, nBranch, nProduct, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 11429)
			End If
		End If
		
		insValDP059 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		
insValDP059_Err: 
		If Err.Number Then
			insValDP059 = insValDP059 & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'%FindHospital: Permite encontrar un elemento en la tabla de acuerdo al código de la clínica
	Public Function FindHospital(ByVal nHospital As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_am_cli_Exist As eRemoteDB.Execute
		
		On Error GoTo reaTab_am_cli_exist_Err
		
		lrecreaTab_am_cli_Exist = New eRemoteDB.Execute
		
		
		With lrecreaTab_am_cli_Exist
			.StoredProcedure = "reaTab_am_cli_Exist"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHospital", nHospital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				dEffecdate_reg = .FieldToClass("dEffecdate")
				FindHospital = True
			End If
		End With
		
reaTab_am_cli_exist_Err: 
		If Err.Number Then
			FindHospital = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_am_cli_Exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_cli_Exist = Nothing
		On Error GoTo 0
		
	End Function
	
	'%InsPostDP057: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla Tar_am_deprod
	Public Function insPostDP059(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nHospital As Integer, ByVal dEffecdate As Date, ByVal dEffecdate_reg As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP059_err
		lclsProd_win = New eProduct.Prod_win
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.dEffecdate_reg = dEffecdate_reg
			.nHospital = nHospital
			.nUsercode = nUsercode
		End With
		
		insPostDP059 = True
		
		Select Case sAction
			
			'+Si la opción seleccionada es Registrar
			Case "Add"
				If dEffecdate_reg = dtmNull Then
					If dEffecdate_reg <> dEffecdate Then
						dEffecdate_Temp = dEffecdate
						Me.dNulldate = dEffecdate
						Me.dEffecdate = dEffecdate_reg
						insPostDP059 = Update()
						Me.dNulldate = dtmNull
						Me.dEffecdate = dEffecdate_Temp
						insPostDP059 = Add()
					End If
				Else
					Me.dNulldate = dtmNull
					insPostDP059 = Add()
				End If
				
				'+Si la opción seleccionada es Modificar
			Case "Update"
				If dEffecdate_reg <> dEffecdate Then
					dEffecdate_Temp = dEffecdate
					Me.dNulldate = dEffecdate
					Me.dEffecdate = dEffecdate_reg
					insPostDP059 = Update()
					Me.dNulldate = dtmNull
					Me.dEffecdate = dEffecdate_Temp
					insPostDP059 = Add()
				Else
					insPostDP059 = Update()
				End If
				
				'+Si la opción seleccionada es Eliminar
			Case "Del"
				If dEffecdate_reg <> dEffecdate Then
					dEffecdate_Temp = dEffecdate
					Me.dNulldate = dEffecdate
					Me.dEffecdate = dEffecdate_reg
					insPostDP059 = Update()
					Me.dNulldate = dtmNull
					Me.dEffecdate = dEffecdate_Temp
					
				Else
					insPostDP059 = Delete()
				End If
				
		End Select
		
		If insPostDP059 Then
			Call Find_Exist(nBranch, nProduct, dEffecdate)
			lclsProd_win = New eProduct.Prod_win
			If mintHospitalExist > 0 Then
				'+ Se actualiza la secuencia de ventana del producto con la transacción enviada como parámetro
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP059", "2", nUsercode)
			Else
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP059", "1", nUsercode)
			End If
		End If
		
insPostDP059_err: 
		If Err.Number Then
			insPostDP059 = False
		End If
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_Exist : Verifica si existen clínicas para el Ramo- Producto en tratamiento
	Public Function Find_Exist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecreaTab_am_cli_2 As eRemoteDB.Execute
		
		On Error GoTo reaTab_am_cli_2_Err
		
		lrecreaTab_am_cli_2 = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaTab_am_cli_2 al 06-24-2002 17:45:20
		'+
		With lrecreaTab_am_cli_2
			.StoredProcedure = "reaTab_am_cli_2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				mintHospitalExist = .FieldToClass("nHospital")
				Find_Exist = True
			Else
				Find_Exist = False
			End If
		End With
		
reaTab_am_cli_2_Err: 
		If Err.Number Then
			Find_Exist = False
		End If
		'UPGRADE_NOTE: Object lrecreaTab_am_cli_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_am_cli_2 = Nothing
		On Error GoTo 0
		
	End Function
End Class






