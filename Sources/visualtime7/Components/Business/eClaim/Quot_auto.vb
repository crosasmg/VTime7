Option Strict Off
Option Explicit On
Public Class Quot_auto
	'%-------------------------------------------------------%'
	'% $Workfile:: Quot_auto.cls                             $%'
	'% $Author:: Nvaplat22                                   $%'
	'% $Date:: 8/12/03 1:57p                                 $%'
	'% $Revision:: 3                                         $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla quot_auto al 11-26-2003 18:14:46
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public nServ_ord As Double ' NUMBER     22   0     10   N
	Public nId As Integer ' NUMBER     22   0     5    N
	Public dQuot_date As Date ' DATE       7    0     0    N
	Public nQuantity As Integer ' NUMBER     22   0     5    N
	Public sDescript As String ' CHAR       60   0     0    S
	Public nVehbrand As Integer ' NUMBER     22   0     5    N
	Public sVehmodel As String ' CHAR       20   0     0    S
	Public nAmount As Double ' NUMBER     22   6     18   N
	Public nyear As Integer ' NUMBER     22   0     5    N
	Public sCliename As String ' CHAR       60   0     0    S
	Public sSel As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    N
	
	'%Find: Lee los datos de la tabla
	Public Function Find(Optional ByVal lblnFind As Boolean = False) As Boolean
		
	End Function
	
	'%InsValSI830_K: Validaciones de la transacción(Header)
	Public Function InsValSI830_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal dQuot_date As Date, ByVal nServ_ord As Double) As String
		Dim lrecInsSI830_K As eRemoteDB.Execute
		Dim lclsErrors As New eFunctions.Errors
		Dim lstrError As String
		
		On Error GoTo insValSI830_K_Err
		
		lrecInsSI830_K = New eRemoteDB.Execute
		
		With lrecInsSI830_K
			.StoredProcedure = "insSi830pkg.insvalSI830_K"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dQuot_date", dQuot_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_Ord", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lclsErrors = New eFunctions.Errors
				With lclsErrors
					.ErrorMessage("SI830_K",  ,  ,  ,  ,  , lstrError)
					InsValSI830_K = lclsErrors.Confirm
				End With
			End If
		End With
		
insValSI830_K_Err:
        If Err.Number Then
            InsValSI830_K = ""
            InsValSI830_K = InsValSI830_K & " " & Err.Description
        End If
        On Error GoTo 0
		lclsErrors = Nothing
		lrecInsSI830_K = Nothing
	End Function
	
	'%InsValSI830: Validaciones de la transacción(Folder)
	'%              Tabla de control de prima mínima(SI830)
	Public Function InsValSI830(ByVal sCodispl As String, ByVal nOperat As Short, ByVal nSelCount As Short, ByVal nVehbrand As Integer, ByVal sVehmodel As String, ByVal nyear As Short) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lstrSep As String
        Dim lstrError As String = ""

        On Error GoTo insValSI830_Err
		
		lstrSep = "||"
		
		lclsErrors = New eFunctions.Errors
		
		'+ Debe existir a lo menos un registro seleccionado
		If nSelCount = eRemoteDB.Constants.intNull Or nSelCount = 0 Then
			lstrError = lstrError & lstrSep & "55764"
		End If
		
		'+ Marca debe estar lleno
		If nVehbrand = eRemoteDB.Constants.intNull Or nVehbrand = 0 Then
			lstrError = lstrError & lstrSep & "4220"
		End If
		
		'+ Modelo debe estar lleno
		If sVehmodel = String.Empty Then
			lstrError = lstrError & lstrSep & "3115"
		End If
		
		'+ Año debe estar lleno
		If nyear = eRemoteDB.Constants.intNull Or nyear = 0 Then
			lstrError = lstrError & lstrSep & "3114"
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI830",  ,  ,  ,  ,  , lstrError)
				InsValSI830 = .Confirm
			End With
		End If
		
insValSI830_Err: 
		If Err.Number Then
			InsValSI830 = "InsValSI830: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		
	End Function
	
	'%InsValSI830Upd: Validaciones de la transacción(Folder)
	Public Function InsValSI830Upd(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nQuantity As Double, ByVal sDescript As String, ByVal nAmount As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lstrSep As String
        Dim lstrError As String = ""

        On Error GoTo insValSI830Upd_Err
		
		lstrSep = "||"
		
		lclsErrors = New eFunctions.Errors
		
		'+ Cantidad debe estar lleno
		If nQuantity = eRemoteDB.Constants.intNull Or nQuantity = 0 Then
			lstrError = lstrError & lstrSep & "55765"
		End If
		
		'+ Descripción debe estar lleno
		If sDescript = String.Empty Then
			lstrError = lstrError & lstrSep & "13985"
		End If
		
		'+ Valor unitario debe estar lleno
		If nAmount = eRemoteDB.Constants.intNull Or nAmount = 0 Then
			lstrError = lstrError & lstrSep & "55665|0|1| Valor Unitario"
		End If
		
		If lstrError <> String.Empty Then
			lstrError = Mid(lstrError, 3)
			With lclsErrors
				.ErrorMessage("SI830",  ,  ,  ,  ,  , lstrError)
				InsValSI830Upd = .Confirm
			End With
		End If
		
insValSI830Upd_Err: 
		If Err.Number Then
			InsValSI830Upd = "InsValSI830Upd: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	'%InsPostSI830Upd: Ejecuta el post de la PopUp de la transacción Si830
	Public Function InsPostSI830Upd(ByVal nAction As Short, ByVal nServ_ord As Double, ByVal nId As Integer, ByVal dQuot_date As Date, ByVal nQuantity As Double, ByVal sDescript As String, ByVal nVehbrand As Integer, ByVal sVehmodel As String, ByVal nAmount As Double, ByVal nyear As Integer, ByVal sCliename As String, ByVal sSel As String, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecInsPostSI830Upd As eRemoteDB.Execute
		Dim nValid As Short
		
		On Error GoTo InsPostSI830Upd_Err
		
		lrecInsPostSI830Upd = New eRemoteDB.Execute
		
		With lrecInsPostSI830Upd
			.StoredProcedure = "insSi830pkg.InsPostSI830Upd"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_Ord", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dQuot_date", dQuot_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuantity", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehbrand", nVehbrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehmodel", sVehmodel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nyear", nyear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel ", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", nValid, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			InsPostSI830Upd = .Parameters("nValid").Value = 1
		End With
		
InsPostSI830Upd_Err: 
		If Err.Number Then
			InsPostSI830Upd = False
		End If
		On Error GoTo 0
		lrecInsPostSI830Upd = Nothing
	End Function
	
	'%InsPostSI830: Ejecuta el post de la transacción Si830
	Public Function InsPostSI830(ByVal nOperat As String, ByVal nServ_ord As Double, ByVal dQuot_date As Date, ByVal sCliename As String, ByVal nVehbrand As Integer, ByVal sVehmodel As String, ByVal nyear As Integer, ByVal nUsercode As Integer) As Boolean
		
		Dim lrecInsPostSI830 As eRemoteDB.Execute
		Dim nValid As Short
		
		On Error GoTo InsPostSI830_Err
		
		lrecInsPostSI830 = New eRemoteDB.Execute
		
		With lrecInsPostSI830
			.StoredProcedure = "insSi830pkg.InsPostSI830"
			.Parameters.Add("nOperat", nOperat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_Ord", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dQuot_date", dQuot_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehbrand", nVehbrand, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehmodel", sVehmodel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nyear", nyear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", nValid, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			InsPostSI830 = .Parameters("nValid").Value = 1
		End With
		
InsPostSI830_Err: 
		If Err.Number Then
			InsPostSI830 = False
		End If
		On Error GoTo 0
		lrecInsPostSI830 = Nothing
	End Function
	
	'% UpdateSel: Realiza la actualizacion de los registros seleccionados en la tabla Quot_Auto
	Public Function UpdateSel(ByVal nServ_ord As Double, ByVal nId As Integer, ByVal sSel As String, ByVal nUsercode As Integer) As Boolean
		Dim UpdQuot_Auto As eRemoteDB.Execute
		Dim nValid As Short
		
		On Error GoTo UpdateSel_Err
		
		UpdQuot_Auto = New eRemoteDB.Execute
		
		With UpdQuot_Auto
			.StoredProcedure = "insSi830pkg.UpdSelQuot_Auto"
			.Parameters.Add("nServ_order", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValid", nValid, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			UpdateSel = .Parameters("nValid").Value = 1
		End With
		
UpdateSel_Err: 
		If Err.Number Then
			UpdateSel = False
		End If
		On Error GoTo 0
		UpdQuot_Auto = Nothing
	End Function
	
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	Private Sub Class_Initialize_Renamed()
		nServ_ord = eRemoteDB.Constants.intNull
		dQuot_date = eRemoteDB.Constants.dtmNull
		nQuantity = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		nVehbrand = eRemoteDB.Constants.intNull
		sVehmodel = String.Empty
		nAmount = eRemoteDB.Constants.intNull
		nyear = eRemoteDB.Constants.intNull
		sCliename = String.Empty
		sSel = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






