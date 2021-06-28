Option Strict Off
Option Explicit On
Public Class Tab_short
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_short.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	
	'- Definición de varialbles públicas de la clase según la estructura de la tabla
	
	'Column_name                            Type        Computed    Length  Prec  Scale Nullable    TrimTrailingBlanks  FixedLenNullInSource
	Public nBranch As Integer 'smallint    no          2        5     0     no          (n/a)              (n/a)
	Public nProduct As Integer 'smallint    no          2        5     0     no          (n/a)              (n/a)
	Public nMonthMax As Integer 'smallint    no          2        5     0     no          (n/a)              (n/a)
	Public nDaysMax As Integer 'smallint    no          2        5     0     no          (n/a)              (n/a)
	Public dEffecdate As Date 'datetime    no          8                    no          (n/a)              (n/a)
	Public nRatedevo As Double 'decimal     no          5        5     2     yes         (n/a)              (n/a)
	Public nRateprem As Double 'decimal     no          5        5     2     yes         (n/a)              (n/a)
	Public nUsercode As Integer 'smallint    no          2        5     0     yes         (n/a)              (n/a)
	
	'- Variables privadas auxiliares
	Private dLastDate As Date
	
	Private Structure udtTab_short
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nMonthMax As Integer
		Dim nDaysMax As Integer
		Dim dEffecdate As Date
		Dim nRatedevo As Double
		Dim nRateprem As Double
		Dim nUsercode As Integer
	End Structure
	
	Private arrTabShort() As udtTab_short
	
	Private Structure udtTab_short_g
		Dim nMonthMax As Integer
		Dim nDaysMax As Integer
		Dim nRatedevo As Double
		Dim nRateprem As Double
	End Structure
	
	Private arrTabShort_g() As udtTab_short_g
	
	Private Structure udtTab_short_g_p
		Dim nMonthmax_g_p As Integer
		Dim nDaysmax_g_p As Integer
		Dim nRatedevo_g_p As Double
		Dim nRateprem_g_p As Double
	End Structure
	
	Private arrTabShort_g_p() As udtTab_short_g_p
	
	'% Add_Tab_Short_g: Función que retorna verdadero al insertar un registro en la tabla 'Tab_Short_g' 08/11/2001
	Public Function Add_Tab_Short_g() As Boolean
		Dim lrecinsCreTab_Short_g As eRemoteDB.Execute
		
		On Error GoTo Add_Tab_Short_g_Err
		
		lrecinsCreTab_Short_g = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insCreTab_Short_g'
		'+ Información leída el 08/11/2001 11:13:34 a.m.
		
		With lrecinsCreTab_Short_g
			.StoredProcedure = "insCreTab_Short_g"
			.Parameters.Add("nMonthMax", nMonthMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysMax", nDaysMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateDevo", nRatedevo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatePrem", nRateprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add_Tab_Short_g = .Run(False)
		End With
		
Add_Tab_Short_g_Err: 
		If Err.Number Then
			Add_Tab_Short_g = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsCreTab_Short_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsCreTab_Short_g = Nothing
	End Function
	
	'% Delete_Tab_Short_g: Función que retorna verdadero al borrar un registro en la tabla 'Tab_Short_g' 08/11/2001
	Public Function Delete_Tab_Short_g() As Boolean
		Dim lrecinsDelTab_Short_g As eRemoteDB.Execute
		
		On Error GoTo Delete_Tab_Short_g_Err
		
		lrecinsDelTab_Short_g = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insDelTab_Short_g'
		'+ Información leída el 08/11/2001 11:31:59 a.m.
		With lrecinsDelTab_Short_g
			.StoredProcedure = "insDelTab_Short_g"
			.Parameters.Add("nMonthMax", nMonthMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysMax", nDaysMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_Tab_Short_g = .Run(False)
		End With
		
Delete_Tab_Short_g_Err: 
		If Err.Number Then
			Delete_Tab_Short_g = False
		End If
		'UPGRADE_NOTE: Object lrecinsDelTab_Short_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelTab_Short_g = Nothing
	End Function
	
	'% reaTab_short_v: Esta funcion se encarga de leer en tab_shrot_g y validar si el registro
	'%                 incluido en el arreglo no se encuentra en la tabla registrado.
	Private Function reaTab_short_v(ByVal nMonthMax As Integer, ByVal nDaysMax As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaTab_short_g_v As eRemoteDB.Execute
		
		On Error GoTo reaTab_short_v_Err
		
		lrecreaTab_short_g_v = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaTab_short_g_v'
		'+ Información leída el 01/07/1999 10:40:41 AM
		
		With lrecreaTab_short_g_v
			.StoredProcedure = "reaTab_short_g_vpkg.reaTab_short_g_v"
			.Parameters.Add("nMonthMax", nMonthMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysMax", nDaysMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				reaTab_short_v = True
				.RCloseRec()
			End If
		End With
		
reaTab_short_v_Err: 
		If Err.Number Then
			reaTab_short_v = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_short_g_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_short_g_v = Nothing
	End Function
	
	'% insPostMDP037: Esta función retorna verdadero al actualizar un registro de la tabla "Tab_short_g"
	Public Function insPostMDP037(ByVal sCodispl As String, ByVal sAction As String, ByVal nSel As Integer, ByVal nMonthMax As Integer, ByVal nDaysMax As Integer, ByVal nRatedevo As Double, ByVal nRateprem As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostMDP037_Err
		
		insPostMDP037 = True
		
		With Me
			.nMonthMax = nMonthMax
			.nDaysMax = nDaysMax
			.nRatedevo = nRatedevo
			.nRateprem = nRateprem
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			
			Select Case sAction
				
				'+ Si la opción seleccionada es Registrar
				Case "Add"
					insPostMDP037 = .Add_Tab_Short_g
					
					'+ Si la opción seleccionada es Modificar
				Case "Update"
					If nSel = 1 Then
						insPostMDP037 = .Add_Tab_Short_g
					End If
					
					'+ Si la opción seleccionada es Eliminar
				Case "Del"
					insPostMDP037 = .Delete_Tab_Short_g
					
			End Select
			
		End With
		
insPostMDP037_Err: 
		If Err.Number Then
			insPostMDP037 = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindDP037: Se leen los datos para la transacción
	Public Function FindDP037(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecTab_short As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo FindDP037_Err
		lrecTab_short = New eRemoteDB.Execute
		With lrecTab_short
			.StoredProcedure = "reaTab_short"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ReDim arrTabShort(50)
				lintCount = 0
				Do While Not .EOF
					arrTabShort(lintCount).nBranch = .FieldToClass("nBranch")
					arrTabShort(lintCount).nProduct = .FieldToClass("nProduct")
					arrTabShort(lintCount).nMonthMax = .FieldToClass("nMonthmax")
					arrTabShort(lintCount).nDaysMax = .FieldToClass("nDaysmax")
					arrTabShort(lintCount).dEffecdate = .FieldToClass("dEffecdate")
					arrTabShort(lintCount).nRatedevo = .FieldToClass("nRatedevo")
					arrTabShort(lintCount).nRateprem = .FieldToClass("nRateprem")
					lintCount = lintCount + 1
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrTabShort(lintCount)
				FindDP037 = True
			End If
		End With
		
FindDP037_Err: 
		If Err.Number Then
			FindDP037 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_short may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_short = Nothing
	End Function
	
	'% InsValDP037: Se realizan las validaciones de la transacción
	Public Function insValDP037(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonthMax As Integer, ByVal nDaysMax As Integer, ByVal nRatePremium As Double, ByVal nRateDevolution As Double, ByVal dEffecdate As Date) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValDP037_err
		
		lobjErrors = New eFunctions.Errors
		insValDP037 = CStr(True)
		With lobjErrors
			'+ Deben indicarse los meses o días por transcurrir
			If nMonthMax = eRemoteDB.Constants.intNull And nDaysMax = eRemoteDB.Constants.intNull Then
				.ErrorMessage("DP037", 11328)
			Else
				If sAction = "Add" Then
					'+ No se puede duplicar el registro
					If valDuplicate(nBranch, nProduct, nMonthMax, nDaysMax, dEffecdate) Then
						Call .ErrorMessage("DP037", 11120)
					End If
				End If
				
				lclsValField = New eFunctions.valField
				With lclsValField
					.objErr = lobjErrors
					.Min = 1
					.Max = 12
					.EqualMax = True
					.EqualMin = True
					.Descript = "Meses"
					Call .ValNumber(nMonthMax,  , eFunctions.valField.eTypeValField.onlyvalid)
					
					.Min = 1
					.Max = 31
					.EqualMax = True
					.EqualMin = True
					.Descript = "Días"
					Call .ValNumber(nDaysMax,  , eFunctions.valField.eTypeValField.onlyvalid)
				End With
				If nRateDevolution = eRemoteDB.Constants.intNull And nRatePremium = eRemoteDB.Constants.intNull Then
					.ErrorMessage("DP037", 11305)
				End If
			End If
			
			insValDP037 = .Confirm
		End With
		
insValDP037_err: 
		If Err.Number Then
			insValDP037 = "insValDP037: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
	End Function
	
	'% insPostDP037: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'% especificaciones funcionales)de la ventana "DP037"
	Public Function insPostDP037(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonthMax As Integer, ByVal nDaysMax As Integer, ByVal dEffecdate As Date, ByVal nRatedevo As Double, ByVal nRateprem As Double, ByVal nUsercode As Integer) As Boolean
		Dim lstrContent As String
		Dim lclsProductWin As eProduct.Prod_win
		
		On Error GoTo insPostDP037_err
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nMonthMax = IIf(nMonthMax = eRemoteDB.Constants.intNull, 0, nMonthMax)
			.nDaysMax = IIf(nDaysMax = eRemoteDB.Constants.intNull, 0, nDaysMax)
			.dEffecdate = dEffecdate
			.nRatedevo = nRatedevo
			.nRateprem = nRateprem
			.nUsercode = nUsercode
			
			If sAction = "Add" Or sAction = "Update" Then
				insPostDP037 = Update
			Else
				insPostDP037 = Delete
			End If
			
			lstrContent = "1"
			If insPostDP037 Then
				lclsProductWin = New eProduct.Prod_win
				If FindDP037(.nBranch, .nProduct, .dEffecdate) Then
					If CountItemDP037 > 0 Then
						lstrContent = "2"
					End If
				End If
				insPostDP037 = lclsProductWin.Add_Prod_win(.nBranch, .nProduct, .dEffecdate, "DP037", lstrContent, .nUsercode)
			End If
		End With
		
insPostDP037_err: 
		If Err.Number Then
			insPostDP037 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProductWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProductWin = Nothing
	End Function
	
	'% FindMDP037: función que realiza el llenado del arreglo con los valores obtenidos de la lectura
	'% del Stored Procedure reaTab_short_g_p
	Public Function FindMDP037(ByVal dEffecdate As Date, Optional ByRef lblnIn As Boolean = False) As Boolean
		Dim lrecreatab_Short_g_p As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo FindMDP037_Err
		lrecreatab_Short_g_p = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reatab_Short_g_p'
		'+ Información leída el 08/11/2001 10:15:35 a.m.
		With lrecreatab_Short_g_p
			.StoredProcedure = "reatab_Short_g_p"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nMonthMax", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDaysMax", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not lblnIn Then
				.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("Option", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("dEffecDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("Option", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If .Run Then
				FindMDP037 = True
				If lblnIn Then
					dLastDate = .FieldToClass("LastDateUpdated")
				End If
				
				ReDim arrTabShort_g_p(1000)
				lintCount = 0
				Do While Not .EOF
					If Not lblnIn Then
						lintCount = lintCount + 1
						arrTabShort_g_p(lintCount).nMonthmax_g_p = .FieldToClass("nMonthmax")
						arrTabShort_g_p(lintCount).nDaysmax_g_p = .FieldToClass("nDaysmax")
						arrTabShort_g_p(lintCount).nRatedevo_g_p = .FieldToClass("nRatedevo")
						arrTabShort_g_p(lintCount).nRateprem_g_p = .FieldToClass("nRateprem")
					End If
					.RNext()
				Loop 
				.RCloseRec()
				ReDim Preserve arrTabShort_g_p(lintCount)
			Else
				FindMDP037 = False
			End If
		End With
		
FindMDP037_Err: 
		If Err.Number Then
			FindMDP037 = False
		End If
		'UPGRADE_NOTE: Object lrecreatab_Short_g_p may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_Short_g_p = Nothing
	End Function
	
	'% FindTab_short_g: busca la información general de corto plazo
	Public Function FindTab_short_g(ByVal dEffecdate As Date) As Boolean
		Dim lintCount As Integer
		Dim lrecreaTab_short_g As eRemoteDB.Execute
		
		On Error GoTo FindTab_short_g_Err
		
		lrecreaTab_short_g = New eRemoteDB.Execute
		
		With lrecreaTab_short_g
			.StoredProcedure = "reaTab_short_g"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				lintCount = 0
				ReDim arrTabShort_g(50)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrTabShort_g(lintCount).nMonthMax = .FieldToClass("nMonthmax")
					arrTabShort_g(lintCount).nDaysMax = .FieldToClass("nDaysmax")
					arrTabShort_g(lintCount).nRatedevo = .FieldToClass("nRatedevo")
					arrTabShort_g(lintCount).nRateprem = .FieldToClass("nRateprem")
					.RNext()
				Loop 
				ReDim Preserve arrTabShort_g(lintCount)
				.RCloseRec()
				FindTab_short_g = True
			End If
		End With
		
FindTab_short_g_Err: 
		If Err.Number Then
			FindTab_short_g = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaTab_short_g may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_short_g = Nothing
	End Function
	
	'%CountItemDP037: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountItemDP037() As Integer
		Get
			CountItemDP037 = UBound(arrTabShort)
		End Get
	End Property
	
	'%CountTabShort_g: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountTabShort_g() As Integer
		Get
			CountTabShort_g = UBound(arrTabShort_g)
		End Get
	End Property
	
	'%CountItemMDP037: propiedad que indica el número de registros que se encuentra en determinado momento en el arreglo de la clase
	Public ReadOnly Property CountItemMDP037() As Integer
		Get
			CountItemMDP037 = UBound(arrTabShort_g_p)
		End Get
	End Property
	
	'%ItemDP037: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemDP037(ByVal nIndex As Integer) As Boolean
		If nIndex <= UBound(arrTabShort) Then
			With arrTabShort(nIndex)
				nBranch = .nBranch
				nProduct = .nProduct
				nMonthMax = .nMonthMax
				nDaysMax = .nDaysMax
				dEffecdate = .dEffecdate
				nRatedevo = .nRatedevo
				nRateprem = .nRateprem
				nUsercode = .nUsercode
			End With
			ItemDP037 = True
		Else
			ItemDP037 = False
		End If
		
	End Function
	
	'%ItemTabShort_g: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemTabShort_g(ByVal nIndex As Integer) As Boolean
		If nIndex <= UBound(arrTabShort_g) Then
			With arrTabShort_g(nIndex)
				nMonthMax = .nMonthMax
				nDaysMax = .nDaysMax
				nRatedevo = .nRatedevo
				nRateprem = .nRateprem
			End With
			ItemTabShort_g = True
		Else
			ItemTabShort_g = False
		End If
		
	End Function
	
	'%ItemTabShort_g_p: Función que tomando en cuenta el valor del index carga en las variables de la clase la información del arreglo
	Public Function ItemMDP037(ByVal nIndex As Integer) As Boolean
		If nIndex <= UBound(arrTabShort_g_p) Then
			With arrTabShort_g_p(nIndex)
				nMonthMax = .nMonthmax_g_p
				nDaysMax = .nDaysmax_g_p
				nRatedevo = .nRatedevo_g_p
				nRateprem = .nRateprem_g_p
			End With
			ItemMDP037 = True
		Else
			ItemMDP037 = False
		End If
		
	End Function
	
	'% insValMDP037_k: Se realizan las validaciones de la página
	Public Function insValMDP037_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMDP037_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If dEffecdate = eRemoteDB.Constants.dtmNull Then
				'+ Debe estar lleno
				Call .ErrorMessage(sCodispl, 10081)
			ElseIf dEffecdate <= Today And nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then 
				Call .ErrorMessage(sCodispl, 10109)
			Else
				'+ La fecha debe ser posterior a la última fecha de actualizacion en la tabla
				If FindMDP037(dEffecdate, True) Then
					If dEffecdate <= dLastDate Then
						Call .ErrorMessage(sCodispl, 10177)
					End If
				End If
			End If
			
			insValMDP037_K = .Confirm
		End With
		
insValMDP037_K_Err: 
		If Err.Number Then
			insValMDP037_K = "insValMDP037_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insValMDP037: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'% forma.
	Public Function insValMDP037(ByVal sCodispl As String, ByVal sAction As String, ByVal nMonths As Integer, ByVal nDays As Integer, ByVal nPremCash As Double, ByVal nPremDev As Double, ByVal dEffecdate As Date) As String
		On Error GoTo insValMDP037_Err
		
		Dim lclsErrors As eFunctions.Errors
		Dim lobjValNum As eFunctions.valField
		Dim lobjValues As eFunctions.Values
		
		lclsErrors = New eFunctions.Errors
		lobjValNum = New eFunctions.valField
		lobjValues = New eFunctions.Values
		
		'+ Validación del campo Meses y Días (Duración/por transcurrir).
		If (nMonths = 0 Or nMonths = eRemoteDB.Constants.intNull) And (nDays = 0 Or nDays = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11328)
		Else
			If sAction = "Add" Then
				If reaTab_short_v(nMonths, nDays, dEffecdate) Then
					Call lclsErrors.ErrorMessage(sCodispl, 11120)
				End If
			End If
		End If
		
		'+ Validacion del campo Prima a cobrar y  del campo Prima a devolver
		If (nPremCash = 0 Or nPremCash = eRemoteDB.Constants.intNull) And (nPremDev = 0 Or nPremDev = eRemoteDB.Constants.intNull) And ((nMonths <> 0 And nMonths <> eRemoteDB.Constants.intNull) Or (nDays <> 0 And nDays <> eRemoteDB.Constants.intNull)) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11305)
		End If
		
		insValMDP037 = lclsErrors.Confirm
		
insValMDP037_Err: 
		If Err.Number Then
			insValMDP037 = insValMDP037 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lobjValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValNum = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
	End Function
	
	'% AddDefaultValue: se crean los conceptos de facturación en base a la tabla general
	Public Function AddDefaultValue(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsTab_bill_i As eRemoteDB.Execute
		
		On Error GoTo AddDefaultValue_Err
		
		lrecinsTab_bill_i = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.inscreTab_bill_i'
		'+Información leída el 23/05/2002
		
		With lrecinsTab_bill_i
			.StoredProcedure = "insDefaultTab_Short"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddDefaultValue = .Run(False)
		End With
		
AddDefaultValue_Err: 
		If Err.Number Then
			AddDefaultValue = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsTab_bill_i may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_bill_i = Nothing
	End Function
	
	'% Update: Actualiza los datos en la tabla
	Private Function Update() As Boolean
		Dim lrecTab_short As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecTab_short = New eRemoteDB.Execute
		
		With lrecTab_short
			.StoredProcedure = "insTab_short"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthmax", nMonthMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysmax", nDaysMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatePrem", nRateprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatedevo", nRatedevo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_short may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_short = Nothing
	End Function
	
	'% Delete: Elimina los datos en la tabla
	Private Function Delete() As Boolean
		Dim lrecTab_short As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecTab_short = New eRemoteDB.Execute
		
		With lrecTab_short
			.StoredProcedure = "insDelTab_short"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthmax", nMonthMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysmax", nDaysMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_short may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_short = Nothing
	End Function
	
	'% valDuplicate: Verifica registros duplicados en la tabla
	Private Function valDuplicate(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMonthMax As Integer, ByVal nDaysMax As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecTab_short As eRemoteDB.Execute
		
		On Error GoTo valDuplicate_err
		
		lrecTab_short = New eRemoteDB.Execute
		
		With lrecTab_short
			.StoredProcedure = "valTab_short_product"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonthmax", nMonthMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDaysmax", nDaysMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valDuplicate = IIf(.Parameters("nExists").Value = 0, False, True)
			End If
		End With
		
valDuplicate_err: 
		If Err.Number Then
			valDuplicate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_short may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_short = Nothing
	End Function
End Class






