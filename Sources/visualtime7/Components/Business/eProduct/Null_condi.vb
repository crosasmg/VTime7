Option Strict Off
Option Explicit On
Public Class Null_condi
	Private mvarNull_condis As Null_condis
	
	'- Estructura de tabla null_condi al 05-29-2002 11:56:28
	'-     Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nNullcode As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nAmelevel As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public sRegtypen As String ' CHAR       1    0     0    S
	Public sReturn_ind As String ' CHAR       1    0     0    S
	Public nReturn_rat As Double ' NUMBER     22   2     5    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public sNotrehab As String ' CHAR       1    0     0    N
	Public sReaAuto As String ' CHAR       1    0     0    N
    Public sRoutine_Pay As String ' CHAR       1    0     0    N
    Public nRetraction As Integer ' NUMBER     22   0     5    S
	
	'% Delete: Nivel de actualizacion minimo que debe tener el usuario para
	'%         ejecutar acciones de actualizacion de la transaccion
	Public Function Delete(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nNullcode As Integer) As Boolean
		
		'- Se define la variable lrecinsdelNull_Condi
		
		Dim lrecinsdelNull_Condi As eRemoteDB.Execute
		
		On Error GoTo insDelnull_condi_Err
		
		lrecinsdelNull_Condi = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insdelNull_Condi'
		'+ Información leída el 18/04/2001 02:25:17 p.m.
		
		Delete = False
		
		With lrecinsdelNull_Condi
			.StoredProcedure = "insdelNull_Condi"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("datEfecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Delete = True
			End If
		End With
		
insDelnull_condi_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsdelNull_Condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsdelNull_Condi = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Update: Nivel de actualizacion minimo que debe tener el usuario para
	'%                 ejecutar acciones de actualizacion de la transaccion
	Public Function Update(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nNullcode As Integer, ByVal nAmelevel As Integer, ByVal sRegtypen As String, ByVal sReturn_ind As String, ByVal nReturn_rat As Double, ByVal sStatregt As String, ByVal sNotrehab As String, ByVal sReaAuto As String, ByVal sRoutine As String, ByVal nRetraction As Integer) As Boolean
		
		Dim lrecinsNull_Condi As eRemoteDB.Execute
		
		On Error GoTo insDelnull_condi_Err
		
		lrecinsNull_Condi = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insNull_Condi'
		'+ Información leída el 18/04/2001 02:09:56 p.m.
		
		Update = False
		
		With lrecinsNull_Condi
			.StoredProcedure = "insNull_Condi"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("datEfecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmelevel", nAmelevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegtypen", sRegtypen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReturn_ind", sReturn_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReturn_rat", nReturn_rat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sNotrehab", sNotrehab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReaAuto", sReaAuto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine_Pay", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRetraction", nRetraction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				Update = True
			End If
		End With
		
insDelnull_condi_Err: 
		If Err.Number Then
			Update = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsNull_Condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsNull_Condi = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValDP061: Se encarga de validar el ingreso de datos a la transacción DP061
	Public Function insValDP061(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nNullcode As Integer, ByVal sReturn_ind As String, ByVal nReturn_rat As Double, ByVal nAmelevel As Integer, ByVal sNotrehab As String, ByVal sRoutine As String, ByVal nRetraction As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsProduct As Product
		
		On Error GoTo insValDP061_err
		
		lclsErrors = New eFunctions.Errors
		lclsProduct = New Product
		
		
		insValDP061 = CStr(True)
		
		'+Se valida la causa de anulación
		If nNullcode > 0 Then
			If sAction = "Add" Then
				If ValDupNull_condi(nBranch, nProduct, nNullcode) Then
					Call lclsErrors.ErrorMessage("DP061", 11396)
				End If
			End If
			
			'+Se valida la devolución de dinero
			If nNullcode <> 0 And sReturn_ind = "0" Then
				Call lclsErrors.ErrorMessage("DP061", 10055)
			Else
				'+ Si la devolución de dinero es por corto plazo, se verifica que haya información registrada de dicha tabla (tab_short).
				If sReturn_ind = "3" Then
					'+ Si no existe información asociada a la tabla de corto plazo (tab_short), no se debe dejar seleccionar corto plazo.
					If Not lclsProduct.ValTab_short_a(nBranch, nProduct, dEffecdate) Then
						Call lclsErrors.ErrorMessage("DP061", 11398)
					End If
				End If
			End If
			
			'+Se valida el porcentaje
			If sReturn_ind = "4" Then
				If Fix(nReturn_rat) <> eRemoteDB.Constants.intNull Then
					If nReturn_rat > 100 Then
						Call lclsErrors.ErrorMessage("DP061", 10057)
					End If
					Select Case nReturn_rat
						Case 0
							Call lclsErrors.ErrorMessage("DP061", 10056)
						Case -1
							Call lclsErrors.ErrorMessage("DP061", 1937)
					End Select
				Else
					Call lclsErrors.ErrorMessage("DP061", 10056)
				End If
			End If
			
			'+ Se valida que la causa de anulación esté llena
		ElseIf nNullcode = 0 Then 
			If sReturn_ind <> "0" Or Fix(nReturn_rat) <> eRemoteDB.Constants.intNull Or nReturn_rat <> 0 Or nAmelevel <> 0 Or nAmelevel <> eRemoteDB.Constants.intNull Then
                Call lclsErrors.ErrorMessage("DP061", 10895)
			End If
		End If

        If sReturn_ind = "9" And sRoutine = String.Empty Then
            Call lclsErrors.ErrorMessage("DP061", 60324)
        End If
		
		insValDP061 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
		
insValDP061_err: 
		If Err.Number Then
			insValDP061 = insValDP061 & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insPostDP061: Esta función se encarga de validar los datos introducidos en la zona de
	'%              contenido para "frame" especifico.
	Public Function insPostDP061(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nNullcode As Integer, ByVal nAmelevel As Integer, ByVal sRegtypen As String, ByVal sReturn_ind As String, ByVal nReturn_rat As Double, ByVal sStatregt As String, ByVal sNotrehab As String, ByVal sReaAuto As String,ByVal sRoutine As String, ByVal nRetraction As Integer) As Boolean
		
		Dim lclsNull_condi As Null_condi
		Dim lclsProd_win As eProduct.Prod_win
		
		lclsNull_condi = New Null_condi
		lclsProd_win = New eProduct.Prod_win
		
		insPostDP061 = True
		
		If sAction <> String.Empty Then
			
			If sAction = "Add" Or sAction = "Update" Then
				insPostDP061 = lclsNull_condi.Update(nBranch, nProduct, dEffecdate, nUsercode, nNullcode, nAmelevel, sRegtypen, sReturn_ind, nReturn_rat, sStatregt, IIf(Trim(sNotrehab) = String.Empty, "2", "1"), IIf(Trim(sReaAuto) = String.Empty, "2", "1"),sRoutine,nRetraction)
			Else
				insPostDP061 = lclsNull_condi.Delete(nBranch, nProduct, dEffecdate, nUsercode, nNullcode)
			End If
			
			If insPostDP061 Then
				'+ Se valida que existan registros en la tabla para dejarla con 'contenido'
				If lclsNull_condi.Find(nBranch, nProduct, dEffecdate) Then
					Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP061", "2", nUsercode)
				Else
					Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP061", "1", nUsercode)
				End If
			Else
				Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP061", "1", nUsercode)
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsNull_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsNull_condi = Nothing
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		
	End Function
	
	'% ValDupNull_condi:  Valida que no exista causas de anulación duplicadas
	Public Function ValDupNull_condi(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNullcode As Integer) As Boolean
		Dim lrecreapay_fracti As eRemoteDB.Execute
		
		On Error GoTo ValDupNull_condi_Err
		lrecreapay_fracti = New eRemoteDB.Execute
		With lrecreapay_fracti
			.SQL = " Select * From null_condi " & " Where nBranch  = " & nBranch & "   and nproduct = " & nProduct & "   and nNullcode = " & nNullcode
			If .Run Then
				ValDupNull_condi = True
				.RCloseRec()
			End If
		End With
		
ValDupNull_condi_Err: 
		If Err.Number Then
			ValDupNull_condi = False
		End If
		'UPGRADE_NOTE: Object lrecreapay_fracti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreapay_fracti = Nothing
		On Error GoTo 0
	End Function
	
	'%Null_condis: Setea una variable para la colección
	
	'%Null_condis: Setea la variable mvarNull_condis
	Public Property Null_condis() As Null_condis
		Get
			If mvarNull_condis Is Nothing Then
				mvarNull_condis = New Null_condis
			End If
			Null_condis = mvarNull_condis
			
		End Get
		Set(ByVal Value As Null_condis)
			mvarNull_condis = Value
			
		End Set
	End Property
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarNull_condis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarNull_condis = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Devuelve la información de los campos requeridos en la emisión
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaNull_condi_o As eRemoteDB.Execute
		
		On Error GoTo reaNull_condi_o_Err
		
		lrecreaNull_condi_o = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaNull_condi_o al 06-20-2002 10:09:58
		'+
		With lrecreaNull_condi_o
			.StoredProcedure = "reaNull_condi_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find = True
			Else
				Find = False
			End If
		End With
		
reaNull_condi_o_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaNull_condi_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaNull_condi_o = Nothing
		On Error GoTo 0
		
	End Function
End Class






