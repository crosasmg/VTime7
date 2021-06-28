Option Strict Off
Option Explicit On
Public Class User_cashnum
	'%-------------------------------------------------------%'
	'% $Workfile:: User_cashnum.cls                         $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 24                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on October 24,2001.
	'*-Propiedades según la tabla en el sistema el 24/10/2001
	
	'Column_name                 Type                  Nulldeable
	'---------------------   ------------------------ ---------------
	Public nCashNum As Integer 'Number(5)       No
	Public nUser As Integer 'Number(5)       No
	Public sStatus As String 'Char(1)         No
	Public nUsercode As Integer 'Number(5)       No
	Public dCompdate As Date 'Date            No
	Public nCashSup As Integer
	Public nHeadSup As Integer
	Public nOfficeAgen As Integer
	Public sClient As String
	Public sDigit As String
	
	'+ Propiedades auxiliares.
	Public nOffice As Integer
	Public sCliename As String
	
	'+ Propiedades auxiliares OPC824
	Public dCollect As Date
	Public sRel_Type As String
	Public sDesc_Reltype As String
	Public nBordereaux As Integer
	Public nBranch As Integer
	Public sDesc_Branch As String
	Public nProduct As Integer
	Public sDesc_Product As String
	Public nPolicy As Double
	Public nProponum As Double
	Public nBulletins As Integer
	Public nReceipt As Integer
	Public nDraft As Integer
	Public dValueDate As Date
	Public nCollecdoctyp As Integer
	Public nSequence As Integer
	Private mvarUser_cashnums As User_cashnums
	
	
	
	Public Property User_cashnums() As User_cashnums
		Get
			If mvarUser_cashnums Is Nothing Then
				mvarUser_cashnums = New User_cashnums
			End If
			
			
			User_cashnums = mvarUser_cashnums
		End Get
		Set(ByVal Value As User_cashnums)
			mvarUser_cashnums = Value
		End Set
	End Property
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarUser_cashnums may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarUser_cashnums = Nothing
		'UPGRADE_NOTE: Object mvarUser_cashnums may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarUser_cashnums = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%InsUpdUser_cashnum: Crea un registro en la tabla de caja por usu
	Private Function InsUpdUser_cashnum(ByVal nAction As Integer) As Boolean
		Dim lrecinsupdUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo insupdUser_cashnum_Err
		
		lrecinsupdUser_cashnum = New eRemoteDB.Execute
		
		With lrecinsupdUser_cashnum
			.StoredProcedure = "InsUpdUser_cashnum"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashSup", nCashSup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHeadSup", nHeadSup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdUser_cashnum = .Run(False)
		End With
		
insupdUser_cashnum_Err: 
		If Err.Number Then
			InsUpdUser_cashnum = False
		End If
		'UPGRADE_NOTE: Object lrecinsupdUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupdUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdUser_cashnum(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdUser_cashnum(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdUser_cashnum(3)
	End Function
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nCashNum As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If Me.nCashNum <> nCashNum Or lblnFind Then
			
			lrecReaUser_cashnum = New eRemoteDB.Execute
			
			With lrecReaUser_cashnum
				.StoredProcedure = "reaUser_cashnum"
				.Parameters.Add("nCashNum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nCashNum = nCashNum
					Me.nUser = .FieldToClass("nUser")
					Me.sStatus = .FieldToClass("sStatus")
					Me.nOffice = .FieldToClass("nOffice")
					Me.sCliename = .FieldToClass("sCliename")
					Me.nCashSup = .FieldToClass("nCashSup")
					Me.nHeadSup = .FieldToClass("nHeadSup")
					Me.nOfficeAgen = .FieldToClass("nOfficeAgen")
					Find = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_nUser: Lee los datos de la tabla de cajas asociadas a partir de un usuario.
	Public Function Find_nUser(ByVal nUser As Integer, Optional ByVal lblnFind_nUser As Boolean = False) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo Find_nUser_Err
		
		If Me.nUser <> nUser Or lblnFind_nUser Then
			
			lrecReaUser_cashnum = New eRemoteDB.Execute
			
			With lrecReaUser_cashnum
				.StoredProcedure = "reaUser_cashnum_nUser"
				.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					If .FieldToClass("sStatus") = "1" Then
						Me.nCashNum = .FieldToClass("nCashNum")
						Me.nUser = nUser
						Find_nUser = True
					End If
					.RCloseRec()
				End If
			End With
		End If
		
Find_nUser_Err: 
		If Err.Number Then
			Find_nUser = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_cashnum_by_Client: Busca el numero de caja dado un cliente determinado
	Public Function Find_cashnum_by_Client(ByVal sClient As String) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo Find_cashnum_by_Client_Err
		
		lrecReaUser_cashnum = New eRemoteDB.Execute
		
		nCashNum = 0
		With lrecReaUser_cashnum
			.StoredProcedure = "reacashnum_by_Client"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				nCashNum = .Parameters.Item("nCashnum").Value
				If nCashNum > 0 Then
					Find_cashnum_by_Client = True
				Else
					Find_cashnum_by_Client = False
				End If
			End If
		End With
		
Find_cashnum_by_Client_Err: 
		If Err.Number Then
			Find_cashnum_by_Client = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%FindClient_by_cashnum: Busca el cliente dada una caja
	Public Function FindClient_by_cashnum(ByVal llngCashnum As Integer) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo FindClient_by_cashnum_Err
		
		lrecReaUser_cashnum = New eRemoteDB.Execute
		
		sClient = String.Empty
		sDigit = String.Empty
		sCliename = String.Empty
		
		With lrecReaUser_cashnum
			.StoredProcedure = "reaclient_by_cashnum"
			.Parameters.Add("nCashnum", llngCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				sClient = RTrim(.Parameters.Item("sClient").Value)
				sDigit = RTrim(.Parameters.Item("sDigit").Value)
				sCliename = RTrim(.Parameters.Item("sCliename").Value)
				If sClient <> String.Empty Then
					FindClient_by_cashnum = True
				Else
					FindClient_by_cashnum = False
				End If
			End If
		End With
		
FindClient_by_cashnum_Err: 
		If Err.Number Then
			FindClient_by_cashnum = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	
	'%InsValMOP634: Validaciones de la transacción
	'%              Asignación de cajas a usuarios(MOP634)
	Public Function InsValMOP634(ByVal sCodispl As String, ByVal sAction As String, ByVal nCashNum As Integer, ByVal nUser As Integer, ByVal nCashSup As Integer, ByVal nHeadSup As Integer, ByVal sStatus As String, ByVal nOfficeAgen As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMOP634_Err
		lclsErrors = New eFunctions.Errors
		If nCashNum = eRemoteDB.Constants.intNull Then
			nCashNum = 0
		End If
		If nUser = eRemoteDB.Constants.intNull Then
			nUser = 0
		End If
		
		With lclsErrors
			'+Se valida el campo Número de caja tenga valor
			If nCashNum = 0 Then
				.ErrorMessage(sCodispl, 60007)
			Else
				If sAction = "Add" Then
					If Find(nCashNum) Then
						.ErrorMessage(sCodispl, 60101)
					End If
				End If
			End If
			
			'+Se valida el campo Usuario que tenga valor
			If nUser = 0 Then
				.ErrorMessage(sCodispl, 60008)
			Else
				'+Se valida que no exista el usuario asignado a una caja
				If valExistUser(nCashNum, nUser) Then
					.ErrorMessage(sCodispl, 60224)
				End If
			End If
			
			'+Se valida el campo estado que tenga valor
			If sStatus = "0" Then
				.ErrorMessage(sCodispl, 1922)
			End If
			
			'+Se valida el campo Oficina que tenga valor
			If nOfficeAgen <= 0 Then
				.ErrorMessage(sCodispl, 55519)
			End If
			
			
			'+Se valida que el campo codigo del supervisor tenga valor
			If nCashSup = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60801)
			Else
				'+Se valida que el campo código del supervisor no sea el mismo cajero
				If nCashSup = nUser Then
					.ErrorMessage(sCodispl, 60457)
				Else
					If Not insExistsUser_Office(nCashSup, nOfficeAgen) Then
						.ErrorMessage(sCodispl, 55938)
					End If
				End If
			End If
			
			'+Se valida que el campo codigo del supervisor jefe tenga valor
			If nHeadSup = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 60802)
			Else
				'+Se valida que el campo código del supervisor jefe no sea el mismo cajero
				If nHeadSup = nUser Then
					.ErrorMessage(sCodispl, 60458)
				Else
					If Not insExistsUser_Office(nHeadSup, nOfficeAgen) Then
						.ErrorMessage(sCodispl, 55938)
					End If
				End If
			End If
			
			InsValMOP634 = .Confirm
		End With
		
InsValMOP634_Err: 
		If Err.Number Then
			InsValMOP634 = "InsValMOP634: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostMOP634: Ejecuta el post de la transacción
	'%               Capitales crecientes(MOP634)
	Public Function InsPostMOP634(ByVal sAction As String, ByVal nCashNum As Integer, ByVal nUser As Integer, ByVal sStatus As String, ByVal nCashSup As Integer, ByVal nHeadSup As Integer, ByVal nUsercode As Integer, ByVal nOfficeAgen As Integer) As Boolean
		
		On Error GoTo InsPostMOP634_Err
		
		With Me
			.nCashNum = nCashNum
			.nUser = nUser
			.sStatus = sStatus
			.nCashNum = nCashNum
			.nCashSup = nCashSup
			.nHeadSup = nHeadSup
			.nUsercode = nUsercode
			.nOfficeAgen = nOfficeAgen
		End With
		
		Select Case sAction
			Case "Add"
				InsPostMOP634 = Add
			Case "Update"
				InsPostMOP634 = Update
			Case "Del"
				InsPostMOP634 = Delete
		End Select
		
InsPostMOP634_Err: 
		If Err.Number Then
			InsPostMOP634 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nCashNum = eRemoteDB.Constants.intNull
		nUser = eRemoteDB.Constants.intNull
		sStatus = strNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%valExistUser: Valida la existencia de un usuario con otra caja asignada
	Public Function valExistUser(ByVal nCashNumOld As Integer, ByVal nUser As Integer, Optional ByVal lblnvalExistUser As Boolean = False) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo valExistUser_Err
		
		If Me.nUser <> nUser Or lblnvalExistUser Then
			
			lrecReaUser_cashnum = New eRemoteDB.Execute
			
			With lrecReaUser_cashnum
				.StoredProcedure = "reaUser_cashnum_nUser"
				.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					If .FieldToClass("nCashNum") <> nCashNumOld Then
						valExistUser = True
					End If
					.RCloseRec()
				End If
			End With
		End If
		
valExistUser_Err: 
		If Err.Number Then
			valExistUser = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%valExistUserCashnum: Valida si un determinado usuario tiene una caja asignada.
	Public Function valExistUserCashnum(ByVal nUser As Integer) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		
		On Error GoTo valExistUserCashnum_Err
		
		lrecReaUser_cashnum = New eRemoteDB.Execute
		
		With lrecReaUser_cashnum
			.StoredProcedure = "reaUser_cashnum_nUser"
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				'+ Si el registro está activo.
				If .FieldToClass("sStatus") = "1" Then
					valExistUserCashnum = True
				End If
				.RCloseRec()
			End If
		End With
		
valExistUserCashnum_Err: 
		If Err.Number Then
			valExistUserCashnum = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_nUser: Lee los datos de la tabla de cajas asociadas a partir de un usuario.
	Public Function insExistsUser_Office(ByVal nUser As Integer, ByVal nOfficeAgen As Integer) As Boolean
		Dim lrecReaUser_cashnum As eRemoteDB.Execute
		Dim lintExist As Integer
		Dim nExists As Integer
		
		
		On Error GoTo insExistsUser_Office_Err
		
		nExists = 0
		
		lrecReaUser_cashnum = New eRemoteDB.Execute
		
		With lrecReaUser_cashnum
			.StoredProcedure = "InsExists_User_Office"
			.Parameters.Add("nUser", nUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					insExistsUser_Office = True
				Else
					insExistsUser_Office = False
				End If
			Else
				insExistsUser_Office = False
			End If
		End With
		
insExistsUser_Office_Err: 
		If Err.Number Then
			insExistsUser_Office = False
		End If
		'UPGRADE_NOTE: Object lrecReaUser_cashnum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaUser_cashnum = Nothing
		On Error GoTo 0
	End Function
	
	'%InsValOPC824: Validaciones de la transacción Consulta de relaciones por caja (OPC824)
	Public Function InsValOPC824(ByVal sCodispl As String, ByVal dCollect As Date, ByVal nCashNum As Integer, ByVal sStatus As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValOPC824_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If dCollect = dtmNull And nCashNum = eRemoteDB.Constants.intNull And sStatus = "0" Then
				.ErrorMessage(sCodispl, 3143)
			End If
			
			InsValOPC824 = .Confirm
		End With
		
InsValOPC824_Err: 
		If Err.Number Then
			InsValOPC824 = "InsValOPC824: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
End Class






