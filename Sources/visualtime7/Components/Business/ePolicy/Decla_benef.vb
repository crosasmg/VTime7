Option Strict Off
Option Explicit On
Public Class Decla_benef
	'%-------------------------------------------------------%'
	'% $Workfile:: Decla_benef.cls                          $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 26/09/03 13.20                               $%'
	'% $Revision:: 19                                       $%'
	'%-------------------------------------------------------%'
	
	'**+Properties according the table in the system on 06/11/2001
	'+ Propie dades según la tabla en el sistema al 06/11/2001.
	
	'   Column_name                     Type       Computed Length Prec  Scale Nullable    TrimTrailingBlanks  FixedLenNullInSource
	Public sCertype As String 'char          no      1                  no              no                    no
	Public nBranch As Integer 'smallint      no      2      5     0     no              (n/a)                 (n/a)
	Public nProduct As Integer 'smallint      no      2      5     0     no              (n/a)                 (n/a)
	Public nPolicy As Double 'int           no      4     10     0     no              (n/a)                 (n/a)
	Public nCertif As Double 'int           no      4     10     0     no              (n/a)                 (n/a)
	Public nNumdecla As Double 'int           no      4     10     0     no              (n/a)                 (n/a)
	Public dEffecDate As Date 'datetime      no      8                  no              (n/a)                 (n/a)
	Public sIrrevoc As String 'char          no      1                  yes             (n/a)                 (n/a)
	Public dDatedecla As Date 'datetime      no      8                  yes             (n/a)                 (n/a)
	Public dNulldate As Date 'datetime      no      8                  yes             (n/a)                 (n/a)
	Public nUsercode As Integer 'smallint      no      2      5     0     yes             (n/a)                 (n/a)
	
	Private mvarDecla_benefs As Decla_benefs
	
	
	Public Property Decla_benefs() As Decla_benefs
		Get
			If mvarDecla_benefs Is Nothing Then
				mvarDecla_benefs = New Decla_benefs
			End If
			Decla_benefs = mvarDecla_benefs
		End Get
		Set(ByVal Value As Decla_benefs)
			mvarDecla_benefs = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarDecla_benefs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarDecla_benefs = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% insPostVI769: Se realiza la actualización de los datos en la ventana VI769
	Public Function insPostVI769(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecDate As Date, ByVal nNumdecla As Double, ByVal sIrrevoc As String, ByVal dDatedecla As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostVI769_Err
		
		Dim lobjValues As eFunctions.Values
		Dim lclsPolicyWin As ePolicy.Policy_Win
		Dim lclsDecla_benefs As ePolicy.Decla_benefs
		Dim lsState As String
		Dim lintCount As Integer
		
		lobjValues = New eFunctions.Values
		lclsPolicyWin = New ePolicy.Policy_Win
		lclsDecla_benefs = New ePolicy.Decla_benefs
		
		insPostVI769 = True
		
		With Me
			.sCertype = sCertype
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nNumdecla = nNumdecla
			.dEffecDate = dEffecDate
			If sIrrevoc <> "1" Then
				sIrrevoc = "2"
			End If
			.sIrrevoc = sIrrevoc
			.dDatedecla = dDatedecla
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add"
				insPostVI769 = Add
			Case "Update"
				insPostVI769 = Update
			Case "Del"
				insPostVI769 = Delete
		End Select
		
		'+ Actualiza el estado de la ventana
		If insPostVI769 And sAction <> "Update" Then
			If lclsDecla_benefs.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate) Then
				lsState = "2"
			Else
				lsState = "1"
			End If
			Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecDate, nUsercode, "VI769", lsState)
		End If
		
insPostVI769_Err: 
		If Err.Number Then insPostVI769 = False
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicyWin = Nothing
		'UPGRADE_NOTE: Object lclsDecla_benefs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDecla_benefs = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdDecla_benef(1)
	End Function
	
	'%Update: Actualiza un registro en la tabla
	Public Function Update() As Boolean
		Update = InsUpdDecla_benef(2)
	End Function
	
	'%Delete: Borra un registro en la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdDecla_benef(3)
	End Function
	
	'%InsUpdDecla_benef: Crea un registro en la tabla
	Private Function InsUpdDecla_benef(ByVal nAction As Integer) As Boolean
		Dim lrecinsupddecla_benef As eRemoteDB.Execute
		
		On Error GoTo insupddecla_benef_Err
		
		lrecinsupddecla_benef = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insupdDecla_benef'
		'+Información leída el 07/11/2001
		With lrecinsupddecla_benef
			.StoredProcedure = "insupdDecla_benef"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumdecla", nNumdecla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIrrevoc", sIrrevoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDatedecla", dDatedecla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			InsUpdDecla_benef = .Run(False)
		End With
		
insupddecla_benef_Err: 
		If Err.Number Then
			InsUpdDecla_benef = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsupddecla_benef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsupddecla_benef = Nothing
	End Function
	
	'%InsValVI769: se validan los campos de las declaraciones de beneficiarios
	Public Function InsValVI769(ByVal sCodispl As String, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecDate As Date, ByVal nNumdecla As Double, ByVal dDatedecla As Date, ByVal sIrrevoc As String, ByVal sIrrevoc_old As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTable As eFunctions.Values
		Dim lclsProduct As eProduct.Product
		Dim lstrMessage As String
		Dim lstrDescript As String
		
		On Error GoTo InsValVI769_Err
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+Se valida el campo Número de declaración
			If nNumdecla = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 55636)
			Else
				If nNumdecla = 0 Then
					.ErrorMessage(sCodispl, 10076)
				End If
				If sAction = "Add" Then
					If Find(nNumdecla) Then
						If Me.sCertype = "1" Then
							lstrMessage = ".Propuesta:"
						ElseIf Me.sCertype = "2" Then 
							lstrMessage = ".Póliza:"
						Else
							lstrMessage = "."
						End If
						lclsProduct = New eProduct.Product
						Call lclsProduct.FindProdMaster(Me.nBranch, Me.nProduct, True)
						lstrDescript = lclsProduct.sDescript
						'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsProduct = Nothing
						lstrMessage = lstrMessage & Me.nPolicy & "-" & Me.nCertif & " (" & lstrDescript & ")"
						.ErrorMessage(sCodispl, 55942,  , eFunctions.Errors.TextAlign.RigthAling, lstrMessage)
					End If
				End If
			End If
			
			'+Se valida el campo Fecha de declaración
			If sAction = "Add" Then
				If dDatedecla = eRemoteDB.Constants.dtmNull Then
					.ErrorMessage(sCodispl, 3390)
				Else
					Call insvalDatedecla(sCodispl, sCertype, nBranch, nProduct, nPolicy, nCertif, dDatedecla, lclsErrors)
				End If
			End If
			
			'+ Si la declaración es irrevocable, no puede cambiarse
			If sIrrevoc_old = "1" And sIrrevoc <> "1" Then
				.ErrorMessage(sCodispl, 55134)
			End If
			
			InsValVI769 = .Confirm
		End With
		
InsValVI769_Err: 
		If Err.Number Then
			InsValVI769 = "InsValVI769: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTable = Nothing
	End Function
	
	'% insvalDatedecla: se verifica que la fecha de declaración sea válida
	Private Function insvalDatedecla(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dDatedecla As Date, ByRef oErrors As eFunctions.Errors) As Boolean
		Dim nValue_date As Short
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalDatedecla_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valDate_decla_benef"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDatedecla", dDatedecla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIrrevoc", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValue_date", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nValue_date = .Parameters("nValue_date").Value
				
				'+ La fecha debe ser mayor o igual a la fecha de emisión que generó la póliza
				'+ y menor o igual a la fecha de vencimiento de la póliza
				If nValue_date = 1 Then
					Call oErrors.ErrorMessage(sCodispl, 60585)
					'+ La fecha debe ser mayor a la última fecha de declaración de beneficiarios
				ElseIf nValue_date = 2 Then 
					Call oErrors.ErrorMessage(sCodispl, 60587)
					'+ Es la combinación de las 2 validaciones anteriores
				ElseIf nValue_date = 3 Then 
					Call oErrors.ErrorMessage(sCodispl, 60585)
					Call oErrors.ErrorMessage(sCodispl, 60587)
				End If
				
				'+ Si la declaración vigente es irrevocable
				If .Parameters("nIrrevoc").Value = 1 Then
					Call oErrors.ErrorMessage(sCodispl, 55133)
				End If
				
				insvalDatedecla = True
			End If
		End With
		
insvalDatedecla_Err: 
		If Err.Number Then
			insvalDatedecla = False
		End If
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
		On Error GoTo 0
	End Function
	
	'Find: Busca un registro con el número de declaración. Debe ser único
	Public Function Find(ByVal nNumdecla As Double) As Boolean
		Dim lrecreaDecla_benef_exist As eRemoteDB.Execute
		
		On Error GoTo reaDecla_benef_exist_Err
		
		lrecreaDecla_benef_exist = New eRemoteDB.Execute
		
		With lrecreaDecla_benef_exist
			.StoredProcedure = "reaDecla_benef_exist"
			.Parameters.Add("nNumdecla", nNumdecla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Find = True
			Else
				Find = False
			End If
		End With
		
reaDecla_benef_exist_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaDecla_benef_exist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDecla_benef_exist = Nothing
		On Error GoTo 0
	End Function
End Class






