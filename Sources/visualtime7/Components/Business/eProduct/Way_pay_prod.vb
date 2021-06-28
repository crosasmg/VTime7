Option Strict Off
Option Explicit On
Public Class Way_pay_prod
	'%-------------------------------------------------------%'
	'% $Workfile:: Way_pay_prod.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 25                                       $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema el 12/11/2001
	'- El campo llave corresponde a nBranch nProduct dEffecdate nWay_pay.
	'- Column_name        Type                 Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'- ------------------ -------------------- ------ ----- ----- -------- ------------------ --------------------
	Public nBranch As Integer 'Numeric  2    5     0     no       (n/a)              (n/a)
	Public nProduct As Integer 'Numeric  2    5     0     no       (n/a)              (n/a)
	Public nWay_pay As Integer 'Numeric  2    5     0     no       (n/a)              (n/a)
	Public nRate_ex As Double 'smallint 5    2     0     yes                         (n/a)
	Public nRate_disc As Double 'smallint 5    2     0     yes                         (n/a)
	Public sPrem_first As String 'char     1                no        no                 yes
	Public dEffecdate As Date 'datetime 8                no       (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                yes      (n/a)              (n/a)
	Public nusercode As Integer 'Numeric  5    5     0     no       (n/a)              (n/a)
	Public dCompdate As Date 'datetime 8                no       (n/a)              (n/a)
	Public nNull_day As Integer 'Numeric  2    5     0     no       (n/a)              (n/a)
	
	
	Public sOneReceipt As String 'Envío a cobro de un solo recibo
    Public sLastReceipt As String 'Envío a cobro de recibo del periodo
    Public sCollection As String
	
	Private Const cintActionAdd As Short = 1
	Private Const cintActionUpdate As Short = 2
	Private Const cintActionDel As Short = 3
	
	Public lintExist As Integer
	
	'% Find: Busca la información de un determinado Ramo Producto Via de pago Fecha DE efecto
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaWay_pay_prod As eRemoteDB.Execute
		Find = True
		On Error GoTo Find_Err
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nWay_pay <> Me.nWay_pay Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecreaWay_pay_prod = New eRemoteDB.Execute
			
			Me.nBranch = nBranch
			Me.nProduct = nProduct
			Me.nProduct = nProduct
			Me.nWay_pay = nWay_pay
			Me.dEffecdate = dEffecdate
			
			With lrecreaWay_pay_prod
				.StoredProcedure = "reaWay_pay_prod"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nWay_pay = .FieldToClass("nWay_pay")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nRate_ex = .FieldToClass("nRate_ex")
					Me.nRate_disc = .FieldToClass("nRate_disc")
					Me.sPrem_first = .FieldToClass("sPrem_first")
					Me.nNull_day = .FieldToClass("nNull_day")
					Me.dNulldate = .FieldToClass("dNulldate")
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
		'UPGRADE_NOTE: Object lrecreaWay_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWay_pay_prod = Nothing
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Add = InsUpdWay_pay_prod(cintActionAdd)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update() As Boolean
		Update = InsUpdWay_pay_prod(cintActionUpdate)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdWay_pay_prod(cintActionDel)
	End Function
	
	'%InsValWay_pay_prod: Lee los datos de la tabla
	Public Function InsValWay_pay_prod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal dEffecdate As Date, Optional ByVal nExist As Integer = 0) As Boolean
		Dim lrecreaWay_pay_prod_v As eRemoteDB.Execute
		
		On Error GoTo reaWay_pay_prod_v_Err
		
		lrecreaWay_pay_prod_v = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaWay_pay_prod 06-05-2002 19:42:00
		With lrecreaWay_pay_prod_v
			.StoredProcedure = "reaWay_pay_prod_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsValWay_pay_prod = True
				lintExist = .Parameters("nExist").Value
			End If
		End With
		
reaWay_pay_prod_v_Err: 
		If Err.Number Then
			InsValWay_pay_prod = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaWay_pay_prod_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaWay_pay_prod_v = Nothing
		On Error GoTo 0
	End Function
	'%insValDP578: Esta función se encarga de validar los datos del Form
	'%Vias de pago por producto
	Public Function insValDP578(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByRef dEffecdate As Date, ByVal nRate_ex As Double, ByVal nRate_disc As Double, ByVal sPrem_first As String, ByRef nNull_day As Integer) As String
		
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
		
		'+ Validación de via de pago
		With lobjErrors
			If nWay_pay = eRemoteDB.Constants.intNull Or nWay_pay = 0 Then
				Call .ErrorMessage(sCodispl, 38044)
			End If
		End With
		
		'+ Recargo y Descuento
		With lobjErrors
			If nRate_ex <> eRemoteDB.Constants.intNull And nRate_ex <> 0 Then
				If nRate_disc <> eRemoteDB.Constants.intNull And nRate_disc <> 0 Then
					Call .ErrorMessage(sCodispl, 55685)
				End If
			End If
		End With
		
		'+ Validación de duplicidad Ramo/Product/Via de pago/Fecha Efecto
		'+ al agregar una fila
		With lobjErrors
			If sAction = "Add" Then
				If Not lblnError Then
					lintExist = 0
					Call InsValWay_pay_prod(nBranch, nProduct, nWay_pay, dEffecdate, lintExist)
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
    Public Function InsPostDP578Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nWay_pay As Integer, ByVal dEffecdate As Date, ByVal nRate_ex As Double, ByVal nRate_disc As Double, ByVal sPrem_first As String, ByVal nNull_day As Integer, ByVal nusercode As Integer, ByVal sLastReceipt As String, ByVal sOneReceipt As String, ByVal scollection As String) As Boolean
        Dim lclsProd_win As eProduct.Prod_win
        Dim lclsWay_pay_prod As eProduct.Way_pay_prods

        On Error GoTo InsPostDP578Upd_Err
        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .nWay_pay = nWay_pay
            .dEffecdate = dEffecdate
            .nRate_ex = nRate_ex
            .nRate_disc = nRate_disc
            .sPrem_first = IIf(sPrem_first = String.Empty, "2", "1")
            .nNull_day = nNull_day
            .nusercode = nusercode
            .sLastReceipt = sLastReceipt
            .sOneReceipt = sOneReceipt
            .sCollection = sCollection

            Select Case sAction
                Case "Add"
                    '+ Se crea el registro
                    InsPostDP578Upd = .Add

                    '+ Se modifica el registro
                Case "Update"
                    InsPostDP578Upd = .Update

                    '+ Se elimina el registro
                Case "Del"
                    InsPostDP578Upd = .Delete

            End Select
        End With

        If InsPostDP578Upd Then
            lclsProd_win = New eProduct.Prod_win
            lclsWay_pay_prod = New eProduct.Way_pay_prods
            If lclsWay_pay_prod.Find(nBranch, nProduct, dEffecdate) Then
                '+ Si existen registros se actualiza la secuencia de ventana del producto como 'con contenido'
                Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP578", "2", nusercode)
            Else
                '+ Si no existen registros se actualiza la secuencia de ventana del producto como 'sin contenido'
                Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP578", "1", nusercode)
            End If
        End If

InsPostDP578Upd_Err:
        If Err.Number Then
            InsPostDP578Upd = False
        End If
        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsWay_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsWay_pay_prod = Nothing
        On Error GoTo 0
    End Function
	
	'%InsUpdWay_pay_prod: Realiza la actualización de la tabla
	Private Function InsUpdWay_pay_prod(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdWay_pay_prod As eRemoteDB.Execute
		
		On Error GoTo InsUpdWay_pay_prod_Err
		
		lrecInsUpdWay_pay_prod = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'InsUpdWay_pay_prod'
		'+ Información leída el 23/01/02
		With lrecInsUpdWay_pay_prod
			.StoredProcedure = "InsUpdWay_pay_prod"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_ex", nRate_ex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate_disc", nRate_disc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPrem_first", sPrem_first, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNull_day", nNull_day, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastReceipt", sLastReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOneReceipt", sOneReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("scollection", sCollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdWay_pay_prod = .Run(False)
		End With
		
InsUpdWay_pay_prod_Err: 
		If Err.Number Then
			InsUpdWay_pay_prod = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdWay_pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdWay_pay_prod = Nothing
		On Error GoTo 0
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nusercode = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		nWay_pay = eRemoteDB.Constants.intNull
		nRate_ex = eRemoteDB.Constants.intNull
		nRate_disc = eRemoteDB.Constants.intNull
		nNull_day = eRemoteDB.Constants.intNull
		sPrem_first = String.Empty
		dNulldate = eRemoteDB.Constants.dtmNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






