Option Strict Off
Option Explicit On
Public Class tax_fixval
	'%-------------------------------------------------------%'
	'% $Workfile:: tax_fixval.cls                           $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 31/10/03 17.16                               $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Defines the principal properties of the corresponding class to the Tax_fixval  table (05/12/2001)
	'-Se definen las propiedades principales de la clase+ correspondientes a la tabla Tax_fixval (05/12/2001)
	'Column_name                        Type              Computed      Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nTypeSupport As Integer 'Integer          no            2           5     0     no                                  (n/a)                               (n/a)
	Public nPercent As Double 'Decimal(5,2)     no            8           5     0     yes                                 (n/a)                              (n/a)
	Public nUsercode As Integer 'Integer          no            2           5     0     no                                  (n/a)                               (n/a)
	Public nCode As Integer 'Integer          no            2           5     0     no                                  (n/a)                               (n/a)
	Public sTypeTax As String 'Char(1)          no            8                       yes                                 (n/a)                               (n/a)
	Public deffecdate As Date 'Date                                                   no
	
	Private mvarTax_Fixvals As Tax_fixvals
	
	
	Public Property Tax_fixvals() As Tax_fixvals
		Get
			
			If mvarTax_Fixvals Is Nothing Then
				mvarTax_Fixvals = New Tax_fixvals
			End If
			
			Tax_fixvals = mvarTax_Fixvals
		End Get
		Set(ByVal Value As Tax_fixvals)
			
			mvarTax_Fixvals = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		
		mvarTax_Fixvals = New Tax_fixvals
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		
		'UPGRADE_NOTE: Object mvarTax_Fixvals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarTax_Fixvals = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Update: It allows to update registries in the Tax_Fixval table.
	'% Update: Permite actualizar registros en la tabla Tax_Fixval.
	Public Function InsUpdTax_Fixval(ByVal nAction As Integer) As Boolean
		Dim lexeTime As eRemoteDB.Execute
		On Error GoTo InsUpdTax_Fixval_Err
		lexeTime = New eRemoteDB.Execute
		
		With lexeTime
			.StoredProcedure = "InsUpdTax_Fixval"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeTax", sTypeTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsUpdTax_Fixval = True
			Else
				InsUpdTax_Fixval = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lexeTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTime = Nothing
		
InsUpdTax_Fixval_Err: 
		If Err.Number Then
			InsUpdTax_Fixval = False
		End If
		On Error GoTo 0
	End Function
	'%insPostMS5552: Función que realiza el llamado a los métodos de actualización, borrado e inserción de registros
	Public Function insPostMS5552(ByVal nTypeSupport As Integer, ByVal nPercent As Double, ByVal nUsercode As Integer, ByVal nCode As Integer, ByVal sTypeTax As String, ByVal sCodispl As String, ByVal sAction As String, ByVal deffecdate As Date) As Boolean
		On Error GoTo insPostMS5552_err
		
		sAction = Trim(sAction)
		
		With Me
			.nTypeSupport = nTypeSupport
			.nPercent = nPercent
			.nCode = nCode
			.nUsercode = nUsercode
			.sTypeTax = sTypeTax
			.deffecdate = deffecdate
			
		End With
		
		Select Case sAction
			
			'**+If the selected option is Register
			'+Si la opción seleccionada es Registrar
			Case "Add"
				insPostMS5552 = InsUpdTax_Fixval(1)
				
				'**+If the selected option is Modify
				'+Si la opción seleccionada es Modificar
			Case "Update"
				insPostMS5552 = InsUpdTax_Fixval(2)
				
				'**+If the selected option is Delete
				'+Si la opción seleccionada es Eliminar
			Case "Del"
				insPostMS5552 = InsUpdTax_Fixval(3)
				
				'**+If the selected option is Duplicate
				'+Si la opción seleccionada es Duplicar
				
				
		End Select
		
insPostMS5552_err: 
		If Err.Number Then
			insPostMS5552 = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% insValMS5552: Realiza la validación de los campos
	Public Function insValMS5552(ByVal nTypeSupport As Integer, ByVal nPercent As Double, ByVal nUsercode As Integer, ByVal nCode As Integer, ByVal sTypeTax As String, ByVal sCodispl As String, ByVal sAction As String, ByVal deffecdate As Date) As String
		Dim ncodeexist As Boolean
		Dim lobjErrors As eFunctions.Errors
		Dim lcolTax_fixval As Tax_fixvals
		Dim lclstax_fixval As tax_fixval
		Dim linterror As Integer
		
		insValMS5552 = String.Empty
		
		On Error GoTo insValMS5552_Err
		
		lobjErrors = New eFunctions.Errors
		lcolTax_fixval = New Tax_fixvals
		
		If nCode = eRemoteDB.Constants.intNull Or nCode = 0 Then
			lobjErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "El campo impuesto ")
		End If
		
		If sAction = "Add" Then
			If nCode <> eRemoteDB.Constants.intNull Then
				If insValnCodeexist(nCode, deffecdate) Then
					lobjErrors.ErrorMessage(sCodispl, 55538)
				End If
				
				If insValRecordExist(nTypeSupport, sTypeTax, deffecdate) Then
					Call lobjErrors.ErrorMessage(sCodispl, 60429)
				End If
			End If
		End If
		
		If nTypeSupport <= 0 Then
			lobjErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "El campo tipo de documento ")
		End If
		
		If CDbl(sTypeTax) <= 0 Then
			lobjErrors.ErrorMessage(sCodispl, 55537,  , eFunctions.Errors.TextAlign.LeftAling, "El campo tipo de impuesto ")
		End If
		
		If nPercent = eRemoteDB.Constants.intNull Or nPercent = 0 Then
			lobjErrors.ErrorMessage(sCodispl, 55540)
		Else
			If nPercent > 100 Then
				lobjErrors.ErrorMessage(sCodispl, 11239)
			End If
		End If
		
		If nTypeSupport > 0 And CDbl(sTypeTax) > 0 Then
			If lcolTax_fixval.Find(deffecdate) Then
				linterror = 0
				For	Each lclstax_fixval In lcolTax_fixval
					If lclstax_fixval.nTypeSupport = nTypeSupport And lclstax_fixval.sTypeTax <> sTypeTax Then
						linterror = linterror + 1
					End If
				Next lclstax_fixval
			End If
		End If
		
		If linterror > 0 Then
			lobjErrors.ErrorMessage(sCodispl, 55539)
		End If
		
		insValMS5552 = lobjErrors.Confirm
		
insValMS5552_Err: 
		If Err.Number Then
			insValMS5552 = insValMS5552 & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lcolTax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTax_fixval = Nothing
		'UPGRADE_NOTE: Object lclstax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstax_fixval = Nothing
		On Error GoTo 0
	End Function
	
	Public Function insValnCodeexist(ByVal nCode As Integer, ByVal deffecdate As Date) As Boolean
		
		Dim lexeTimes As eRemoteDB.Execute
		
		insValnCodeexist = False
		
		On Error GoTo insValnCodeexist_Err
		
		lexeTimes = New eRemoteDB.Execute
		
		With lexeTimes
			.StoredProcedure = "valtax_fixval"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					insValnCodeexist = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lexeTimes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTimes = Nothing
		
insValnCodeexist_Err: 
		If Err.Number Then
			insValnCodeexist = False
		End If
		On Error GoTo 0
	End Function
	
	Public Function insValRecordExist(ByVal nTypeSupport As Integer, ByVal sTypeTax As String, ByVal deffecdate As Date) As Boolean
		
		Dim lexeTimes As eRemoteDB.Execute
		
		insValRecordExist = False
		
		On Error GoTo insValRecordExist_Err
		
		lexeTimes = New eRemoteDB.Execute
		
		With lexeTimes
			.StoredProcedure = "valtax_fixval_u"
			.Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeTax", sTypeTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("lCount") > 0 Then
					insValRecordExist = True
				End If
			End If
		End With
		'UPGRADE_NOTE: Object lexeTimes may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeTimes = Nothing
		
insValRecordExist_Err: 
		If Err.Number Then
			insValRecordExist = False
		End If
		On Error GoTo 0
	End Function
	'Find: Función que realiza la busqueda en la tabla 'Tax_fixval'.
	Public Function Find(ByVal nCode As Integer, ByVal deffecdate As Date) As Boolean
		Dim lrecTax_fixval As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecTax_fixval = New eRemoteDB.Execute
		
		With lrecTax_fixval
			.StoredProcedure = "reatax_fixval"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'.Parameters.Add "sTypeTax", Null, rdbParamInput, rdbVarChar, 1, 0, 0, rdbParamNullable
			
			If .Run(True) Then
				Me.sTypeTax = .FieldToClass("sTypeTax")
				Me.nCode = .FieldToClass("nCode")
				Me.nPercent = .FieldToClass("nPercent")
				Me.nTypeSupport = .FieldToClass("nTypeSupport")
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecTax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTax_fixval = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'Find_o: Función que realiza la busqueda en la tabla 'Tax_fixval'.
	Public Function Find_o(ByVal nCode As Integer, ByVal sTypeTax As String) As Boolean
		Dim lrecTax_fixval As eRemoteDB.Execute
		
		On Error GoTo Find_o_Err
		
		lrecTax_fixval = New eRemoteDB.Execute
		
		With lrecTax_fixval
			.StoredProcedure = "reatax_fixval"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeTax", sTypeTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				sTypeTax = .FieldToClass("sTypeTax")
				nCode = .FieldToClass("nCode")
				nPercent = .FieldToClass("nPercent")
				nTypeSupport = .FieldToClass("nTypeSupport")
				.RCloseRec()
				Find_o = True
			Else
				Find_o = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecTax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTax_fixval = Nothing
		
Find_o_Err: 
		If Err.Number Then
			Find_o = False
		End If
		On Error GoTo 0
	End Function
	
	'Find_nTypesupport: Función que realiza la busqueda en la tabla 'Tax_fixval' por tipo de documento (nTypeSupport).
	Public Function Find_nTypesupport(ByVal nTypeSupport As Integer, ByVal deffecdate As Date) As Boolean
		Dim lrecTax_fixval As eRemoteDB.Execute
		
		On Error GoTo Find_nTypesupport_Err
		
		lrecTax_fixval = New eRemoteDB.Execute
		
		With lrecTax_fixval
			.StoredProcedure = "reaTax_fixval_nTypesupport"
			.Parameters.Add("nTypeSupport", nTypeSupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", deffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.sTypeTax = .FieldToClass("sTypeTax")
				Me.nCode = .FieldToClass("nCode")
				Me.nPercent = .FieldToClass("nPercent")
				Me.nTypeSupport = .FieldToClass("nTypeSupport")
				.RCloseRec()
				Find_nTypesupport = True
			Else
				Find_nTypesupport = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecTax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTax_fixval = Nothing
		
Find_nTypesupport_Err: 
		If Err.Number Then
			Find_nTypesupport = False
		End If
		On Error GoTo 0
	End Function
	
	'% insValMS5552_K: Realiza la validación de los campos
	Public Function insValMS5552_K(ByVal sCodispl As String, ByVal deffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		On Error GoTo insValMS5552_K_Err
		lobjErrors = New eFunctions.Errors
		
		insValMS5552_K = String.Empty
		
		If deffecdate = dtmNull Then
			lobjErrors.ErrorMessage(sCodispl, 10190)
		Else
			If deffecdate <= Today Then
				lobjErrors.ErrorMessage(sCodispl, 10868)
			Else
				If deffecdate <= Find_LastDate Then
					lobjErrors.ErrorMessage(sCodispl, 10869,  , eFunctions.Errors.TextAlign.RigthAling, "(" & Find_LastDate & ")")
				End If
			End If
		End If
		insValMS5552_K = lobjErrors.Confirm
		
insValMS5552_K_Err: 
		If Err.Number Then
			insValMS5552_K = insValMS5552_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'Find_LastDate: Función que realiza la busqueda en la tabla 'tax_fixval'
	Public Function Find_LastDate() As Date
		Dim lrecTax_fixval As eRemoteDB.Execute
		On Error GoTo Find_LastDate_Err
		
		lrecTax_fixval = New eRemoteDB.Execute
		
		With lrecTax_fixval
			.StoredProcedure = "reatax_fixval_lastdate"
			.Parameters.Add("dEffecdate", dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Find_LastDate = .Parameters("dEffecdate").Value
			Else
				Find_LastDate = dtmNull
			End If
		End With
		'UPGRADE_NOTE: Object lrecTax_fixval may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTax_fixval = Nothing
		
Find_LastDate_Err: 
		If Err.Number Then
			Find_LastDate = dtmNull
		End If
		On Error GoTo 0
		
	End Function
End Class






