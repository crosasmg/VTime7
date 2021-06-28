Option Strict Off
Option Explicit On
Public Class Freq_way_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Freq_way_prod.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:35p                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'- Definición de la tabla FREQ_WAY_PROD tomada el 07/05/2002 15:31
	'- Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nWay_pay As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nCurrency As Integer ' NUMBER        22     5      0 Yes
	Public nPre_issue As Double ' NUMBER        22    10      2 Yes
	Public nPre_amend As Double ' NUMBER        22    10      2 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public dCompdate As Date ' DATE           7              No
	Public nusercode As Integer ' NUMBER        22     5      0 No
	Public nPayFreq As Integer ' NUMBER        22     5      0 No
	Public nQprem As Double
	Public sIva As String
    Public nLimit_ExcTax As Double
    Public sNo_sell As String
	
	'- Propiedades auxiliares
	Public nExist As Integer
	Public sDescript As String
	Public lintExist As Integer
	
	Private Const cintActionAdd As Short = 1
	Private Const cintActionUpdate As Short = 2
	Private Const cintActionDel As Short = 3
	
	'% Find: Busca la información de un determinado Ramo/Producto/Via/Frecuencia de pago/Fecha de efecto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal nPayFreq As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaFreq_way_prod As eRemoteDB.Execute
		Find = True
		On Error GoTo Find_Err
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nWay_pay <> Me.nWay_pay Or nPayFreq <> Me.nPayFreq Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecreaFreq_way_prod = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFreq_way_prod'
			'+ Información leída el 07/05/2002 15:39:55
			
			With lrecreaFreq_way_prod
				.StoredProcedure = "reaFreq_way_prod"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nWay_pay = .FieldToClass("nWay_pay")
					Me.nPayFreq = .FieldToClass("nPayFreq")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nCurrency = .FieldToClass("nCurrency")
					Me.nPre_issue = .FieldToClass("nPre_issue")
					Me.nPre_amend = .FieldToClass("nPre_amend")
					Me.dNulldate = .FieldToClass("dNulldate")
					Me.nQprem = .FieldToClass("nQprem")
					Me.sIva = .FieldToClass("sIva")
					Me.nLimit_ExcTax = .FieldToClass("nLimit_ExcTax")
                    Me.sNo_sell = .FieldToClass("sNo_sell")
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaFreq_way_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFreq_way_prod = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdFreq_way_prod(cintActionAdd)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdFreq_way_prod(cintActionUpdate)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdFreq_way_prod(cintActionDel)
	End Function
	
	'%InsValFreq_way_prod: Lee los datos de la tabla, valida la existencia de una fila
	Public Function InsValFreq_way_prod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal nPayFreq As Integer, ByVal dEffecdate As Date, Optional ByVal nExist As Integer = 0) As Boolean
		Dim lrecreaFreq_way_prod_v As eRemoteDB.Execute
		
		On Error GoTo reaFreq_way_prod_v_Err
		
		lrecreaFreq_way_prod_v = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaFreq_way_prod 06-05-2002 19:42:00
		With lrecreaFreq_way_prod_v
			.StoredProcedure = "reaFreq_way_prod_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValFreq_way_prod = .Parameters("nExist").Value = 1
			Else
				InsValFreq_way_prod = False
			End If
		End With
		
reaFreq_way_prod_v_Err: 
		If Err.Number Then
			InsValFreq_way_prod = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaFreq_way_prod_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFreq_way_prod_v = Nothing
		On Error GoTo 0
	End Function
	'%insValDP578: Esta función se encarga de validar los datos del Form
	'%Vias de pago por producto
	Public Function insValDP578(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal nPayFreq As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nPre_issue As Double, ByVal nPre_amend As Double) As String
		
		'- Se define el objeto para el manejo de las clases
		Dim lobjErrors As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lblnError As Boolean
		
		Dim lintBranch As Integer
		Dim lintProduct As Integer
		
		lobjErrors = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		
		On Error GoTo insValDP578_Err
		lblnError = False
		
		'+ Validación de Moneda
		With lobjErrors
			If (nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0) And ((nPre_issue <> eRemoteDB.Constants.intNull And nPre_issue <> 0) Or (nPre_amend <> eRemoteDB.Constants.intNull And nPre_amend <> 0)) Then
				Call .ErrorMessage(sCodispl, 1351)
			End If
		End With
		
		'+ Valida la exitencia previa del registro Ramo/Producto/Via de pago/Frecuencia de Pago/Fecha efecto
		'+ al agregar una fila
		With lobjErrors
			If sAction = "Add" Then
				If Not lblnError Then
					lintExist = 0
					Call InsValFreq_way_prod(nBranch, nProduct, nWay_pay, nPayFreq, dEffecdate, lintExist)
					If lintExist = 1 Then
						Call .ErrorMessage(sCodispl, 10284)
					End If
				End If
			End If
		End With
		
		
		
		insValDP578 = lobjErrors.Confirm
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		
insValDP578_Err: 
		If Err.Number Then
			insValDP578 = "insValDP578: " & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	'%InsPostDP578Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (DP578)
    Public Function InsPostDP578Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal nPayFreq As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nPre_issue As Double, ByVal nPre_amend As Double, ByVal nusercode As Integer, ByVal nQprem As Double, ByVal sIva As String, ByVal nLimit_ExcTax As Double, ByVal sNo_sell As String) As Boolean
        On Error GoTo InsPostDP578Upd_Err
        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .nWay_pay = nWay_pay
            .nPayFreq = nPayFreq
            .dEffecdate = dEffecdate
            .nCurrency = nCurrency
            .nPre_issue = nPre_issue
            .nPre_amend = nPre_amend
            .nusercode = nusercode
            .nQprem = nQprem
            .sIva = sIva
            .nLimit_ExcTax = nLimit_ExcTax
            .sNo_sell = sNo_sell
            Select Case sAction
                Case "Add"
                    '+ Se crea el registro
                    InsPostDP578Upd = .Add

                Case "Update"
                    '+ Se modifica el registro
                    InsPostDP578Upd = .Update

                Case "Del"
                    '+ Se elimina el registro
                    InsPostDP578Upd = .Delete
            End Select
        End With

InsPostDP578Upd_Err:
        If Err.Number Then
            InsPostDP578Upd = False
        End If
        On Error GoTo 0
    End Function
	
	'%InsUpdFreq_way_prod: Realiza la actualización de la tabla
	Private Function InsUpdFreq_way_prod(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdFreq_way_prod As eRemoteDB.Execute
		
		On Error GoTo InsUpdFreq_way_prod_Err
		
		lrecInsUpdFreq_way_prod = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'InsUpdFreq_way_prod'
		'+ Información leída el 07/05/2002
		With lrecInsUpdFreq_way_prod
			.StoredProcedure = "InsUpdFreq_way_prod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayFreq", nPayFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPre_issue", nPre_issue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPre_amend", nPre_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQPrem", nQprem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIva", sIva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Limit_ExcTax", nLimit_ExcTax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNo_sell", sNo_sell, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdFreq_way_prod = .Run(False)
		End With
		
InsUpdFreq_way_prod_Err: 
		If Err.Number Then
			InsUpdFreq_way_prod = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdFreq_way_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdFreq_way_prod = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nusercode = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nWay_pay = eRemoteDB.Constants.intNull
		nPayFreq = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nCurrency = eRemoteDB.Constants.intNull
		nPre_issue = eRemoteDB.Constants.intNull
		nPre_amend = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nQprem = eRemoteDB.Constants.intNull
		sIva = CStr(eRemoteDB.Constants.strNull)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Find_O: Busca si existen registros en la tabla frecuencias permitidas por vías
	'%       de pago y producto
	Public Function Find_O(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaFreq_way_prod_o As eRemoteDB.Execute
		
		On Error GoTo reaFreq_way_prod_o_Err
		
		lrecreaFreq_way_prod_o = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaFreq_way_prod_o al 07-29-2002 10:48:06
		'+
		With lrecreaFreq_way_prod_o
			.StoredProcedure = "reaFreq_way_prod_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Find_O = True
			Else
				Find_O = False
			End If
		End With
		
reaFreq_way_prod_o_Err: 
		If Err.Number Then
			Find_O = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaFreq_way_prod_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFreq_way_prod_o = Nothing
		On Error GoTo 0
		
	End Function
End Class






