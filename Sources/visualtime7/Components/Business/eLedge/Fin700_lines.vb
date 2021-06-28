Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
<System.Runtime.InteropServices.ProgId("Fin700_Lines_NET.Fin700_Lines")> Public Class Fin700_Lines
	'%-------------------------------------------------------%'
	'% $Workfile:: Fin700_lines.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'-   Se definen las propiedades de la clase
	'-   Nombre de la columna               Tipo
	Public nLed_compan As Integer 'NUMBER (5)    NOT NULL
	Public nArea_Led As Integer 'NUMBER (5)    NOT NULL
	Public nTransac_Ty As Integer 'NUMBER (5)    NOT NULL
	Public sAccount_base As String 'CHAR   (20)   NOT NULL
	Public nGroup As Integer 'NUMBER (5)    NOT NULL
	Public sAccount_FIN700 As String 'CHAR   (20)   NOT NULL
	Public dCompdate As Date 'DATE          NOT NULL
	Public nUsercode As Integer 'NUMBER (5)    NOT NULL
	
	'- Propiedades Auxiliares
	
	Public blnCharge As Boolean
	Public nVoucher As Integer
	Public sDescript As String
	Public dDate_FIN700 As Date
	Public sFile_FIN700 As String
	Public nComplement As Integer
	Public sAux_accoun As String
	Public sDescript_Acc As String
	Public nLine_Type As Integer
	
	'- Tipo registro
	Private Structure udtAcc_lines
		Dim nVoucher As Integer
		Dim sDescript As String
		Dim dDate_FIN700 As Date
		Dim sFile_FIN700 As String
	End Structure
	
	'- Arreglo
	Private arrAcc_lines() As udtAcc_lines
	
	'- Tipo registro
	Private Structure udtFIN700
		Dim nTransac_Ty As Integer
		Dim sAccount As String
		Dim sAux_accoun As String
		Dim nComplement As Integer
		Dim sAccount_FIN700 As String
		Dim nLine_Type As Integer
		Dim sDescript_Acc As String
	End Structure
	
	'- Arreglo
	Private arrFIN700() As udtFIN700
	
	
	'% Count_FIN700: Obtiene la cantidad de registros almacenados en el arreglo
	Public ReadOnly Property Count_FIN700() As Integer
		Get
			Count_FIN700 = UBound(arrFIN700)
		End Get
	End Property
	
	'% CountSI776: Propiedad que retorna la cantidad de elementos añadidos al arreglo
	Public ReadOnly Property CountMCP776() As Integer
		Get
			CountMCP776 = UBound(arrAcc_lines)
		End Get
	End Property
	'%FIN700_Format1(): Formato 1, prametrizado y con respectivos valores by default.
	Private Function FIN700_Format1(ByVal nSessId As Integer, ByVal dDocFecha As Date, ByVal nUnicod As Integer, ByVal nPeriod As Integer, ByVal nTdOid As Integer, ByVal nDocnumInt As Integer, ByVal nDocDetLine As Integer, ByVal nCtaCod As Integer, ByVal sCurrency As String, ByVal sCrecodigo As String, ByVal nCdicodigo As Integer, ByVal sClient As String, ByVal nSucursal As Integer, ByVal sDocglosa As String, ByVal nDocTipodoccalce As Integer, ByVal nDocnumdocCalce As Integer, ByVal dDocDateExpir As Date, ByVal nMontImpDeb As Double, ByVal nMontImpHab As Double, ByVal nMontLocDeb As Double, ByVal nMontLocHab As Double, ByVal sTransaction As String, Optional ByVal sClientName As Object = "", Optional ByVal sStatus_Check As String = "", Optional ByVal dStatus_Check As Date = #12:00:00 AM#) As String
		Dim lstrDetOwner As String
		Dim lstrProviderLevel As String
		Dim lstrProjectType As String
		Dim lstrProjectNumber As String
		Dim lstrQuotaNumber As String
		Dim lstrBankCode As String
		Dim lstrVoucherCode As String
		Dim lstrTCoId As String
		Dim lstrVoucherNumber As String
		Dim lstrLineDetail As String
		Dim lstrStructure As String
		Dim lstrFIN700_Format1 As String
		
		Dim lintCount As Integer
		Dim lstrValue As String
		Dim lstrValueAUX As String
		Dim lstrNewValue As String
		Dim lstrNewDecimalValue As String
		
		
		On Error GoTo FIN700_Format1_err
		'+ Primer valor del formato 1: Codigo sistema  al cual se cargan los datos
		lstrFIN700_Format1 = Format(nSessId, "00")
		
		'+ Detalle del dueño: 20 espacios en blanco
		lstrDetOwner = New String(" ", 20)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrDetOwner
		
		'+ Fecha del comprobante
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(CDate(dDocFecha), "yyyy/MM/dd")
		
		'+ Código de la unidad
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nUnicod, "0000") '"3112"
		
		'+ Numero de período
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nPeriod, "0000")
		
		'+ Tipo de operacion - OJO - PENDIENTES LOS CORREOS DE LORETO PIZARRO
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nTdOid, "201")
		
		'+ Numero interno de documento
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nDocnumInt, "000000000")
		
		'+ Numero de lineas del detalle
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nDocDetLine, "000000000")
		
		'+ Codigo de la cuenta contable
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nCtaCod, "000000000")
		
		'+ Codigo de la moneda
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(sCurrency, "00")
		
		'+ Nivel de proveedor
		lstrProviderLevel = New String("0", 2)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrProviderLevel
		
		'+ Codigo del centro de responsabilidad
		If sCrecodigo = String.Empty Then
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(0, "000000")
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(Trim(sCrecodigo), "000000")
		End If
		
		'+ Codigo de imputacion
		If nCdicodigo < 0 Then
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(0, "0000")
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nCdicodigo, "0000")
		End If
		
		'+ Tipo de proyecto
		lstrProjectType = New String("0", 3)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrProjectType
		
		'+ Numero de proyecto
		lstrProjectNumber = New String("0", 4)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrProjectNumber
		
		'+ Rut, proveedor, cliente, etc.
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(sClient, "0000000000-0")
		
		'+ Numero de la sucursal
		If nSucursal < 0 Then
			nSucursal = 0
		End If
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nSucursal, "0000")
		
		'+ Glosa del documento - Si se trata del proceso de transferencias de solicitiudes de cheques
		'+ (CPL778), este valor corresponde al nombre del beneficiario del cheque - ACM - 21/01/2003
		'    FIN700_Format1 = FIN700_Format1 & Format(sDocglosa, "                                                            ")
		If sTransaction = "CPL778" Then
			If sClientName <> String.Empty Then
				lstrFIN700_Format1 = lstrFIN700_Format1 & sClientName
			Else
				lstrFIN700_Format1 = lstrFIN700_Format1 & RTrim(sDocglosa) & New String(" ", 60 - Len(sDocglosa))
			End If
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & RTrim(sDocglosa) & New String(" ", 60 - Len(sDocglosa))
		End If
		
		'+ Tipo  documento analisis
		lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nDocTipodoccalce, "000")
		
		'+ Numero documento analisis: Transacciones CPL779 o CPL778, formato año/mes (YYYYMM).
		'+ Transacción CPL777, por defecto "000000000". Lleva valor si y sólo si la cuenta contable es de análisis - ACM - 21/01/2003
		Select Case sTransaction
			Case "CPL778", "CPL779"
				lstrFIN700_Format1 = lstrFIN700_Format1 & Format(Year(dDocFecha), "0000") & Format(Month(dDocFecha), "00")
			Case "CPL777"
				lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nDocnumdocCalce, "000000000")
		End Select
		
		'+ Numero de la cuota
		lstrQuotaNumber = New String("0", 4)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrQuotaNumber
		
		'+ Fecha dcto. analisis: Si la transacción es CPL778, el valor a añadir a este parámetro es la
		'+ fecha de pago del cheque (status en la tabla cheque = 6). Por defecto, la fecha que lleva es
		'+ 1900/01/01 - ACM - 21/01/2003
		If sTransaction = "CPL778" Then
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			If sStatus_Check = "6" And Not IsNothing(dStatus_Check) Then
				lstrFIN700_Format1 = lstrFIN700_Format1 & Format(dStatus_Check, "yyyy/MM/dd")
			Else
				lstrFIN700_Format1 = lstrFIN700_Format1 & Format(CDate("01/01/1900"), "yyyy/MM/dd")
			End If
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(dDocDateExpir, "yyyy/MM/dd")
		End If
		
		'+ Monto imputacion DEBE
		If nMontImpDeb >= 0 Then
			lstrValue = CStr(nMontImpDeb)
			lstrValueAUX = String.Empty
			lstrNewValue = String.Empty
			lstrNewDecimalValue = String.Empty
			For lintCount = 1 To Len(lstrValue)
				lstrValueAUX = Mid(lstrValue, lintCount, 1)
				If lstrValueAUX = "," Or lstrValueAUX = "." Then
					Exit For
				Else
					lstrNewValue = lstrNewValue & lstrValueAUX
				End If
			Next 
			For lintCount = InStr(1, lstrValue, lstrValueAUX) To Len(lstrValue)
				lstrNewDecimalValue = lstrNewDecimalValue & Mid(lstrValue, lintCount + 1, 1)
				If lstrNewDecimalValue = String.Empty Then
					lstrNewDecimalValue = lstrNewDecimalValue & "0"
				End If
			Next 
			
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(lstrNewValue, "00000000000000") & "," & Format(lstrNewDecimalValue, "0000")
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(0, "00000000000000") & "," & Format(0, "0000")
		End If
		'    lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nMontImpDeb, "00000000000000,0000")
		
		'+ Monto imputacion HABER
		If nMontImpHab >= 0 Then
			lstrValue = CStr(nMontImpHab)
			lstrValueAUX = String.Empty
			lstrNewValue = String.Empty
			lstrNewDecimalValue = String.Empty
			For lintCount = 1 To Len(lstrValue)
				lstrValueAUX = Mid(lstrValue, lintCount, 1)
				If lstrValueAUX = "," Or lstrValueAUX = "." Then
					Exit For
				Else
					lstrNewValue = lstrNewValue & lstrValueAUX
				End If
			Next 
			For lintCount = InStr(1, lstrValue, lstrValueAUX) To Len(lstrValue)
				lstrNewDecimalValue = lstrNewDecimalValue & Mid(lstrValue, lintCount + 1, 1)
				If lstrNewDecimalValue = String.Empty Then
					lstrNewDecimalValue = lstrNewDecimalValue & "0"
				End If
			Next 
			
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(lstrNewValue, "00000000000000") & "," & Format(lstrNewDecimalValue, "0000")
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(0, "00000000000000") & "," & Format(0, "0000")
		End If
		
		'    lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nMontImpHab, "00000000000000,0000")
		
		'+ Monto imputacion en moneda local DEBE
		If nMontLocDeb >= 0 Then
			lstrValue = CStr(nMontLocDeb)
			lstrValueAUX = String.Empty
			lstrNewValue = String.Empty
			lstrNewDecimalValue = String.Empty
			For lintCount = 1 To Len(lstrValue)
				lstrValueAUX = Mid(lstrValue, lintCount, 1)
				If lstrValueAUX = "," Or lstrValueAUX = "." Then
					Exit For
				Else
					lstrNewValue = lstrNewValue & lstrValueAUX
				End If
			Next 
			For lintCount = InStr(1, lstrValue, lstrValueAUX) To Len(lstrValue)
				lstrNewDecimalValue = lstrNewDecimalValue & Mid(lstrValue, lintCount + 1, 1)
				If lstrNewDecimalValue = String.Empty Then
					lstrNewDecimalValue = lstrNewDecimalValue & "0"
				End If
			Next 
			
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(lstrNewValue, "00000000000000") & "," & Format(lstrNewDecimalValue, "0000")
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(0, "00000000000000") & "," & Format(0, "0000")
		End If
		
		'    lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nMontLocDeb, "00000000000000,0000")
		
		'+ Monto imputacion haber en moneda local HABER
		If nMontLocHab >= 0 Then
			lstrValue = CStr(nMontLocHab)
			lstrValueAUX = String.Empty
			lstrNewValue = String.Empty
			lstrNewDecimalValue = String.Empty
			For lintCount = 1 To Len(lstrValue)
				lstrValueAUX = Mid(lstrValue, lintCount, 1)
				If lstrValueAUX = "," Or lstrValueAUX = "." Then
					Exit For
				Else
					lstrNewValue = lstrNewValue & lstrValueAUX
				End If
			Next 
			For lintCount = InStr(1, lstrValue, lstrValueAUX) To Len(lstrValue)
				lstrNewDecimalValue = lstrNewDecimalValue & Mid(lstrValue, lintCount + 1, 1)
				If lstrNewDecimalValue = String.Empty Then
					lstrNewDecimalValue = lstrNewDecimalValue & "0"
				End If
			Next 
			
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(lstrNewValue, "00000000000000") & "," & Format(lstrNewDecimalValue, "0000")
		Else
			lstrFIN700_Format1 = lstrFIN700_Format1 & Format(0, "00000000000000,0000")
		End If
		
		'    lstrFIN700_Format1 = lstrFIN700_Format1 & Format(nMontLocHab, "00000000000000,0000")
		
		'+ Código del banco
		lstrBankCode = New String("0", 3)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrBankCode
		
		'+ Tipo de la...?
		lstrVoucherCode = New String("0", 3)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrVoucherCode
		
		'+ Tipo de comprobante
		lstrTCoId = New String("0", 3)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrTCoId
		
		'+ Numero del comprobante
		lstrVoucherNumber = New String("0", 9)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrVoucherNumber
		
		'+ Fecha del comprobante
		lstrFIN700_Format1 = lstrFIN700_Format1 & "1900/01/01"
		
		'+ Numero de linea
		lstrLineDetail = New String("0", 9)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrLineDetail
		
		'+ Estructura
		lstrStructure = New String("0", 3)
		lstrFIN700_Format1 = lstrFIN700_Format1 & lstrStructure
		
		FIN700_Format1 = lstrFIN700_Format1
		
FIN700_Format1_err: 
		If Err.Number Then
			FIN700_Format1 = String.Empty
		End If
		On Error GoTo 0
	End Function
	'%FIN700_Format2(): Formato 2, prametrizado y con respectivos valores by default.
	Private Function FIN700_Format2(ByVal nSessId As Integer, ByVal sSource As String, ByVal sUser As String, ByVal dEffecdate As Date, ByVal nPeriod As Integer, ByVal nDocCod As Integer, ByVal nIdentity As Integer, ByVal nDocnum As Integer, ByVal nProvNum As Integer, ByVal sClient As String, ByVal nOffice As Integer, ByVal dExpirdat As Date, ByVal nCurrency As Integer, ByVal nAmountA As Double, ByVal nAmount As Double, ByVal nExent As Double, ByVal nIva As Double, ByVal nTax As Double, ByVal nCustoms As Integer, ByVal nRetAmount As String, ByVal nOriginalTotalAmount As Integer, ByVal nAmountLocal As Integer, ByVal nAmountNetLocal As Double, ByVal nMontImpDeb As Double, ByVal nMontImpHab As Double, ByVal nMontLocDeb As Double, ByVal nMontLocHab As Double) As String
        'se retorna Nothing para corregir una advertencia mientras la funcion no posea [ statements ] ni este siendo utilizada

    End Function
	
	'%Find: Función obtiene un registro único de la tabla "Fin700_lines"
	Public Function Find(ByVal nLed_compan As Integer, ByVal nArea_Led As Integer, ByVal nTransac_Ty As Integer, ByVal sAccount_base As String) As Boolean
		
		Dim lrecreaFIN700_lines As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaFIN700_lines = New eRemoteDB.Execute
		
		'+ Definicion de parametros para stored procedure 'insudb.rea_FIN700_lines'
		'+ Informacion leida el 03/10/2002 11:44:30 AM
		
		With lrecreaFIN700_lines
			.StoredProcedure = "rea_FIN700_lines"
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_Base", sAccount_base, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				nLed_compan = .FieldToClass("nLed_compan")
				nArea_Led = .FieldToClass("nArea_Led")
				nTransac_Ty = .FieldToClass("nTransac_ty")
				sAccount_base = .FieldToClass("sAccount_Base")
				nGroup = .FieldToClass("nGroup")
				sAccount_FIN700 = .FieldToClass("sAccount_FIN700")
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecreaFIN700_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaFIN700_lines = Nothing
		On Error GoTo 0
		
	End Function
	'% Find_FIN700: Realiza la búsqueda de registros para el resumen de las interfaces contables
	Public Function Find_FIN700(ByVal nLed_compan As Integer, ByVal nArea_Led As Integer, ByVal nGroup As Integer) As Boolean
		Dim lRecReaFIN700 As New eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo Find_FIN700_Err
		
		'+ Definicion de parametros para stored procedure 'insudb.rea_FIN700'
		'+ Informacion leida el 03/10/2002 11:44:30 AM
		
		With lRecReaFIN700
			.StoredProcedure = "ReaFIN700"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				ReDim arrFIN700(150)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrFIN700(lintCount).nTransac_Ty = .FieldToClass("nTransac_ty")
					arrFIN700(lintCount).sAccount = .FieldToClass("sAccount")
					arrFIN700(lintCount).sAux_accoun = .FieldToClass("sAux_Accoun")
					arrFIN700(lintCount).nComplement = .FieldToClass("nComplement")
					arrFIN700(lintCount).sAccount_FIN700 = .FieldToClass("sAccount_FIN700")
					arrFIN700(lintCount).sDescript_Acc = .FieldToClass("sDescript")
					arrFIN700(lintCount).nLine_Type = .FieldToClass("nLine_Type")
					.RNext()
				Loop 
				ReDim Preserve arrFIN700(lintCount)
				Find_FIN700 = True
			Else
				Find_FIN700 = False
			End If
		End With
		
Find_FIN700_Err: 
		If Err.Number Then
			Find_FIN700 = False
		End If
		
		'UPGRADE_NOTE: Object lRecReaFIN700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRecReaFIN700 = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'% FindMCP776: Localiza los registros dentro de la tabla Acc_lines
	Public Function FindMCP776(ByVal nLed_compan As Integer, ByVal nLed_year As Integer, ByVal nLed_Month As Integer, ByVal sShowVoucher As String) As Boolean
		Dim recReaAcc_linesFIN700 As New eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo FindMCP776_Err
		
		With recReaAcc_linesFIN700
			.StoredProcedure = "ReaAcc_linesFIN700"
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_year", nLed_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_Month", nLed_Month, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShowVoucher", sShowVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				ReDim arrAcc_lines(150)
				Do While Not .EOF
					lintCount = lintCount + 1
					arrAcc_lines(lintCount).nVoucher = .FieldToClass("nVoucher")
					arrAcc_lines(lintCount).sDescript = .FieldToClass("sDescript")
					arrAcc_lines(lintCount).sFile_FIN700 = .FieldToClass("sFile_FIN700")
					arrAcc_lines(lintCount).dDate_FIN700 = .FieldToClass("dDate_FIN700")
					.RNext()
				Loop 
				ReDim Preserve arrAcc_lines(lintCount)
				FindMCP776 = True
				.RCloseRec()
			Else
				FindMCP776 = False
			End If
		End With
		
		blnCharge = FindMCP776
		
FindMCP776_Err: 
		If Err.Number Then
			FindMCP776 = False
		End If
		
		'UPGRADE_NOTE: Object recReaAcc_linesFIN700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		recReaAcc_linesFIN700 = Nothing
		
		On Error GoTo 0
		
	End Function
	'%insPostCPL777_K: Función que realiza las actualizaciones del proceso CPL777
	Public Function insPostCPL777_K(ByVal nCompany As Integer, ByVal sVoucher As String) As Boolean
		
		'-Se define la varible que nos permitira accesar la base de datos y obtener los datos a procesar.
		
		Dim lexeCPL777 As eRemoteDB.Execute
		
		'-Se define la variable que nos permitira accesar la informacion de la compañia contable.
		
		Dim lclsLed_compan As eLedge.Led_compan
		
		'-Se define la variable que nos permitira realizar la conversion de los montos.
		
		Dim lclsGeneral As eGeneral.Exchange
		
		'-Se define la variable que nos permitira realizae la busqueda el codigo equivalente a la compañia en Tab_equal
		
		Dim lclsTab_equal As Tab_equal
		
		'- Se define la variable que nos permitira manejar el codigo FIN700 asociado a la compañia.
		
		Dim lstrEM As String
		
		'-Se define la variable que contendra la linea de texto, a escribir en el archivo de salida.
		
		Dim lstrWritTxt As String
		
		'- Se define la variable que contendra el codigo de la cuenta segun el fin700 asociada.
		
		Dim lstrAccount_baseAUX As String
		Dim lngAccount_baseAUX As Integer
		
		'-Se define la variable para realizar la busqueda de la informacion de opciones de instalacion contables
		
		Dim lclsOpt_ledger As Opt_ledger
		
		'-Se define la variable  que contendra el nombre del archivo a generar.
		
		Dim lstrFileName As String
		
		'-Se define la variable  que contendra la ruta y nombre del archivo a generar.
		
		Dim lstrFile_FIN700 As String
		
		'-Se definen las variables que contendran: el año, el mes, y el calculo del periodo
		
		Dim lintYear As Integer
		Dim lintMonth As Integer
		Dim lintPeriod As Integer
		
		'-Se definen las varibles que contendran los importes en moneda local
		
		Dim ldblLocalDebit As Double
		Dim ldblLocalCredit As Double
		
		
		lexeCPL777 = New eRemoteDB.Execute
		
		On Error GoTo insPostCPL777_K_Err
		
		With lexeCPL777
			.StoredProcedure = "ReaAcc_Lines_Cpl777pkg.ReaAcc_Lines_Cpl777"
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVoucher", sVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'+ Se realiza la busqueda de los registros a tranferir al fin700 segun los parametros especificados.
			
			If .Run Then
				insPostCPL777_K = True
				If Not .EOF Then
					
					'+Se realiza la busqueda del codigo FIN700 asociado a la compañia en proceso, usado en el nombre del archivo a generar
					
					lclsTab_equal = New Tab_equal
					lstrEM = "EM"
					If lclsTab_equal.IsExist(nCompany, 3, CStr(nCompany), String.Empty) Then
						lstrEM = lclsTab_equal.sCodeAsi
					End If
					
					'+Se realiza la busqueda del año, mes contable de la compañia en tratamiento.
					
					lclsLed_compan = New eLedge.Led_compan
					Call lclsLed_compan.Find(nCompany)
					
					lintYear = Year(lclsLed_compan.dIniLedDat)
					lintMonth = Month(lclsLed_compan.dDate_init)
					
					'+Si se encuentran registros a procesar se realiza la busqueda del año Fin700 en la tabla de opciones de instalacion
					
					lclsOpt_ledger = New Opt_ledger
					Call lclsOpt_ledger.Find()
					
					lstrFile_FIN700 = "1" & lintYear & lintMonth & .RecordCount & "0000.C" & lstrEM
					
					lstrFileName = "C:\Inetpub\wwwroot\VTimeNet\TFiles\" & lstrFile_FIN700
					
					FileOpen(1, lstrFileName, OpenMode.Output)
					Do While Not .EOF
						If Find(lexeCPL777.FieldToClass("nLed_Compan"), lexeCPL777.FieldToClass("nArea_Led"), lexeCPL777.FieldToClass("nTransac_ty"), lexeCPL777.FieldToClass("sAccount_Base")) Then
							lstrAccount_baseAUX = Me.sAccount_FIN700
						Else
							lstrAccount_baseAUX = lexeCPL777.FieldToClass("sAccount_Base")
						End If
						If lstrAccount_baseAUX = String.Empty Then
							lstrAccount_baseAUX = CStr(0)
						End If
						lngAccount_baseAUX = CInt(lstrAccount_baseAUX)
						lclsGeneral = New eGeneral.Exchange
						Call lclsGeneral.Convert(0, .FieldToClass("nDebit"), .FieldToClass("nCurrency"), 1, Today, 0, True)
						ldblLocalDebit = lclsGeneral.pdblResult
						Call lclsGeneral.Convert(0, .FieldToClass("nCredit"), .FieldToClass("nCurrency"), 1, Today, 0, True)
						ldblLocalCredit = lclsGeneral.pdblResult
						
						'+Con los datos obtenidos se realiza el calculo del período
						
						lintYear = Year(.FieldToClass("dLinesEffect"))
						lintMonth = Month(.FieldToClass("dLinesEffect"))
						lintPeriod = lintMonth + (lintYear - lclsOpt_ledger.nYear_Fin700) * 14 + 1
						
						lstrWritTxt = FIN700_Format1(1, .FieldToClass("dEffecdate"), 1, lintPeriod, 201, .FieldToClass("nVoucher"), .FieldToClass("nLine"), lngAccount_baseAUX, .FieldToClass("SCODEASI"), 52 & .FieldToClass("sCost_cente"), 0, .FieldToClass("sClient"), 0, .FieldToClass("sDescript"), 0, 0, CDate("1900/01/01"), .FieldToClass("nDebit"), .FieldToClass("nCredit"), ldblLocalDebit, ldblLocalCredit, "CPL777")
						
						PrintLine(1, lstrWritTxt)
						
						'                    Call UpdAcc_TransaFIN700(lexeCPL777.FieldToClass("nLed_compan"), lexeCPL777.FieldToClass("nVoucher"), lstrFile_FIN700)
						
						lexeCPL777.RNext()
					Loop 
					FileClose(1)
				End If
			End If
		End With
		
		
insPostCPL777_K_Err: 
		If Err.Number Then
			insPostCPL777_K = False
		End If
		'UPGRADE_NOTE: Object lexeCPL777 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeCPL777 = Nothing
	End Function
	'%insPostCPL779_K: Función que realiza las actualizaciones del proceso CPL779
	Public Function insPostCPL779_K(ByVal dEffecdate As Date, ByVal nConcept As Integer) As Boolean
		'-Se define la variable que permitira accesar los datos a procesar.
		
		Dim lexeCPL779 As eRemoteDB.Execute
		
		'-Se define la variable para reliazar la busqueda de la informacion de la compañia contable
		
		Dim lclsLed_compan As eLedge.Led_compan
		
		'-Se define la variable para realizar la busqueda de la informacion de opciones de instalacion contables
		
		Dim lclsOpt_ledger As Opt_ledger
		
		'-Se define la variable que contendra el texto de la linea a imprimir en el archivo detalle y encabezado
		
		Dim lstrWritTxt As String
		
		'-Se define la variable  que contendra el nombre del archivo a generar.
		
		Dim lstrFileName As String
		
		'-Se define la variable  que contendra la ruta y nombre del archivo a generar.
		
		Dim lstrFile_FIN700 As String
		
		'-Se definen las variables que contendran: el año, el mes, y el calculo del periodo
		
		Dim lintYear As Integer
		Dim lintMonth As Integer
		Dim lintPeriod As Integer
		
		Dim lintCount As Integer
		Dim lclsvalue As New eFunctions.Values
		Dim lintlength As Integer
		
		lexeCPL779 = New eRemoteDB.Execute
		lclsLed_compan = New eLedge.Led_compan
		lclsOpt_ledger = New Opt_ledger
		'Set lclsvalue = New eFunctions.Values
		
		On Error GoTo insPostCPL779_K_Err
		
		With lexeCPL779
			.StoredProcedure = "reaCPLCash_bank"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable) 'rdbParamInput, rdbInteger, 0, 0, 10, rdbParamNullable
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 20, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insPostCPL779_K = True
				If Not .EOF Then
					
					'+Si se encuentran registros a procesar se realiza la busqueda del año Fin700 en la tabla de opciones de instalacion
					
					lclsOpt_ledger = New Opt_ledger
					Call lclsOpt_ledger.Find()
					
					'+con los datos obtenidos se realiza el calculo del período
					
					lintPeriod = Month(Today) + (Year(Today) - lclsOpt_ledger.nYear_Fin700) * 14 + 1
					
					'+Se realiza la busqueda de los datos de la compañia contable para obtener año y mes del periodo contable.
					
					Call lclsLed_compan.Find(1)
					
					If lclsLed_compan.dIniLedDat <> eRemoteDB.Constants.dtmNull Then
						lintYear = Year(lclsLed_compan.dIniLedDat)
						lintMonth = Month(lclsLed_compan.dDate_init)
					Else
						lintYear = Year(Today)
						lintMonth = Month(Today)
					End If
					
					'+Se construye el nombre del archivo cabecera segun lo indicado en el funcional
					
					lstrFileName = UCase(lclsvalue.insGetSetting("LoadFile", String.Empty, "CONFIG"))
					
					lintlength = Len(lstrFileName)
					
					If Mid(lstrFileName, lintlength, 1) <> "\" Then
						lstrFileName = lstrFileName & "\"
					End If
					
					
					
					lstrFileName = "1" & lintYear & lintMonth & "0000.H" 'EM definir compañia
					'                lstrFileName = "C:\Inetpub\wwwroot\VTimeNet\TFiles\" & lstrFile_FIN700
					FileOpen(1, lstrFileName, OpenMode.Output)
					
					'+Se construye el nombre del archivo detalle segun lo indicado en el funcional
					
					lstrFile_FIN700 = UCase(lclsvalue.insGetSetting("LoadFile", String.Empty, "CONFIG"))
					
					lintlength = Len(lstrFile_FIN700)
					
					If Mid(lstrFile_FIN700, lintlength, 1) <> "\" Then
						lstrFile_FIN700 = lstrFile_FIN700 & "\"
					End If
					lstrFile_FIN700 = "1" & lintYear & lintMonth & "0000.D" 'EM definir codigo de la compañia
					'lstrFileName = "C:\Inetpub\wwwroot\VTimeNet\TFiles\" & lstrFile_FIN700
					FileOpen(2, lstrFileName, OpenMode.Output)
					
					lintCount = 1
					
					Do While Not lexeCPL779.EOF
						
						'+Se construye la linea de cabecera asociada al registro leido segun indicaciones funcionales.
						
						lstrWritTxt = Format("UD", "  ") & Format("", "                    ") & Format(.FieldToClass("dDat_propos"), "yyyy/MM/dd") & Format(lintPeriod, "0000") & Format("UD", "000") & Format(lintCount, "000000000")
						
						'+Se imprime la linea en el archivo de cabecera
						
						PrintLine(1, lstrWritTxt)
						
						'+Se construyen las 3 lineas del detalle asociadas al registro leido segun indicaciones funcionales.
						
						lstrWritTxt = FIN700_Format1(10, .FieldToClass("dDat_propos"), 0, lintPeriod, .FieldToClass("TDOID"), 0, 1, .FieldToClass("sAcc_ledger"), .FieldToClass("sCurrency"), CStr(0), 0, .FieldToClass("sClient"), 0, .FieldToClass("sCliename"), IIf(.FieldToClass("nTypeSUpport") = 3, 103, 100), .FieldToClass(""), .FieldToClass("dDat_propos"), .FieldToClass("nAmount"), .FieldToClass("nAmount"), .FieldToClass("nAmount_local"), .FieldToClass("nAmount_local"), "CPL779")
						'+Se imprime la linea 1 en el archivo detalle
						
						PrintLine(2, lstrWritTxt)
						
						lstrWritTxt = FIN700_Format1(10, .FieldToClass("dDat_propos"), 0, lintPeriod, .FieldToClass("TDOID"), 0, 2, .FieldToClass("sAcc_ledger"), .FieldToClass("sCurrency"), CStr(0), 0, .FieldToClass("sClient"), 0, .FieldToClass("sCliename"), IIf(.FieldToClass("nTypeSUpport") = 3, 103, 100), .FieldToClass(""), .FieldToClass("dDat_propos"), .FieldToClass("nAmount"), .FieldToClass("nAmount"), .FieldToClass("nAmount_local"), .FieldToClass("nAmount_local"), "CPL779")
						'+Se imprime la linea 2 en el archivo detalle
						PrintLine(2, lstrWritTxt)
						
						lstrWritTxt = FIN700_Format1(10, .FieldToClass("dDat_propos"), 0, lintPeriod, .FieldToClass("TDOID"), 0, 3, .FieldToClass("sAcc_ledger"), .FieldToClass("sCurrency"), CStr(0), 0, .FieldToClass("sClient"), 0, .FieldToClass("sCliename"), IIf(.FieldToClass("nTypeSUpport") = 3, 103, 100), .FieldToClass(""), .FieldToClass("dDat_propos"), .FieldToClass("nAmount"), .FieldToClass("nAmount"), .FieldToClass("nAmount_local"), .FieldToClass("nAmount_local"), "CPL779")
						'+Se imprime la linea 2 en el archivo detalle
						PrintLine(2, lstrWritTxt)
						
						'+Se actualiza la tabla cheques con los valores del nombre del archivo, y la fecha de procesamiento.
						
						Call UpdChequesFIN700(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nConsec"), sFile_FIN700, dEffecdate)
						
						lintCount = lintCount + 1
						lexeCPL779.RNext()
					Loop 
					FileClose(1)
					FileClose(2)
				End If
			End If
		End With
		
		insPostCPL779_K = True
		
insPostCPL779_K_Err: 
		If Err.Number Then
			insPostCPL779_K = False
		End If
		'UPGRADE_NOTE: Object lexeCPL779 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeCPL779 = Nothing
		
	End Function
	
	
	'*%InsPostMCP775: Pass of the information introduced towards the layers of rules of business and access of data.
	'% InsPostMCP775: Pase de la información introducida hacia las capas de reglas de negocio y acceso de datos.
	Public Function InsPostMCP775(ByVal blnHeader As Boolean, ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal nArea_Led As Integer, ByVal nGroup As Integer, ByVal nTransac_Ty As Integer, ByVal sAccount_base As String, ByVal sAccount_FIN700 As String, ByVal nUsercode As Integer) As Boolean
		
		On Error GoTo InsPostMCP775_Err
		
		If blnHeader Then
			InsPostMCP775 = True
		Else
			InsPostMCP775 = Update(nLed_compan, nArea_Led, nGroup, nTransac_Ty, sAccount_base, sAccount_FIN700, nUsercode)
		End If
		
InsPostMCP775_Err: 
		If Err.Number Then
			InsPostMCP775 = False
		End If
		
	End Function
	'*%InsValMCP775: Validation of the data for the page details.
	'% InsValMCP775: Validación de los datos para la página detalle.
	Public Function InsValMCP775(ByVal sCodispl As String, ByVal sAccount_FIN700 As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCP775_Err
		
		lclsErrors = New eFunctions.Errors
		
		If sAccount_FIN700 = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 60380)
		End If
		
		InsValMCP775 = lclsErrors.Confirm
		
InsValMCP775_Err: 
		If Err.Number Then
			InsValMCP775 = InsValMCP775 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'*%InsValMCP775_k: Validation of the data for the page of the headed one.
	'% InsValMCP775_k: Validación de los datos para la página del encabezado.
	Public Function InsValMCP775_k(ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal nArea_Led As Integer, ByVal nGroup As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCP775_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		
		'+ Compañía contable: Debe estar lleno
		If nLed_compan <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
		End If
		
		'+ Área contable: Debe estar lleno
		If nArea_Led <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 36200)
		End If
		
		'+ Grupo: Debe estar lleno
		If nGroup <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60375)
		End If
		
		InsValMCP775_k = lclsErrors.Confirm
		
InsValMCP775_k_Err: 
		If Err.Number Then
			InsValMCP775_k = InsValMCP775_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'*%InsValMCP776_k: Validation of the data for the page of the headed one.
	'% InsValMCP776_k: Validación de los datos para la página del encabezado.
	Public Function InsValMCP776_k(ByVal sCodispl As String, ByVal nLed_compan As Integer, ByVal nLed_year As Integer, ByVal nLed_Month As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMCP776_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Compañía contable: Debe estar lleno
		
		If nLed_compan = 0 Or nLed_compan = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
		End If
		
		'+ Año: Debe estar lleno
		
		If nLed_year = 0 Or nLed_year = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 60338)
		End If
		
		
		
		InsValMCP776_k = lclsErrors.Confirm
		
InsValMCP776_k_Err: 
		If Err.Number Then
			InsValMCP776_k = InsValMCP776_k & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'% ItemFIN700: Obtiene los registros del arreglo dada una posición específica
	Public Function ItemFIN700(ByVal nIndex As Integer) As Boolean
		On Error GoTo ItemFIN700_Err
		
		If nIndex <= Count_FIN700 Then
			With Me
				.nTransac_Ty = arrFIN700(nIndex).nTransac_Ty
				.sAccount_base = arrFIN700(nIndex).sAccount
				.sAux_accoun = arrFIN700(nIndex).sAux_accoun
				.nComplement = arrFIN700(nIndex).nComplement
				.sAccount_FIN700 = arrFIN700(nIndex).sAccount_FIN700
				.sDescript_Acc = arrFIN700(nIndex).sDescript_Acc
				.nLine_Type = arrFIN700(nIndex).nLine_Type
			End With
			ItemFIN700 = True
		Else
			ItemFIN700 = False
		End If
		
ItemFIN700_Err: 
		If Err.Number Then
			ItemFIN700 = False
		End If
		
		On Error GoTo 0
	End Function
	'% ItemMCP776: Obtiene los registros del arreglo dada una posición específica
	Public Function ItemMCP776(ByVal nIndex As Integer) As Boolean
		ItemMCP776 = False
		
		On Error GoTo ItemMCP776_Err
		
		If blnCharge Then
			If nIndex <= UBound(arrAcc_lines) Then
				With arrAcc_lines(nIndex)
					Me.nVoucher = .nVoucher
					Me.sDescript = .sDescript
					Me.dDate_FIN700 = .dDate_FIN700
					Me.sFile_FIN700 = .sFile_FIN700
				End With
				ItemMCP776 = True
			Else
				ItemMCP776 = False
			End If
		End If
		
ItemMCP776_Err: 
		If Err.Number Then
			ItemMCP776 = False
		End If
		
		On Error GoTo 0
	End Function
	'%UpdAcc_TransaFIN700: Función que actualiza la tabla Acc_transa el nombre del archivo generado para el FIN700
	Public Function UpdAcc_TransaFIN700(ByVal nLed_compan As Integer, ByVal nVoucher As Integer, ByVal sFile_FIN700 As String) As Boolean
		
		Dim lrecUpdAcc_TransaFIN700 As eRemoteDB.Execute
		
		On Error GoTo UpdAcc_TransaFIN700_Err
		
		lrecUpdAcc_TransaFIN700 = New eRemoteDB.Execute
		
		'+ Definicion de parametros para stored procedure 'insudb.UpdAcc_TransaFIN700'
		'+ Informacion leida el 03/10/2002 12:11:30 PM
		
		With lrecUpdAcc_TransaFIN700
			.StoredProcedure = "UpdAcc_Transa_FIN700"
			.Parameters.Add("nLed_Compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVoucher", nVoucher, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFile_FIN700", sFile_FIN700, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdAcc_TransaFIN700 = .Run(False)
		End With
		
UpdAcc_TransaFIN700_Err: 
		If Err.Number Then
			UpdAcc_TransaFIN700 = False
		End If
		'UPGRADE_NOTE: Object lrecUpdAcc_TransaFIN700 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdAcc_TransaFIN700 = Nothing
		
	End Function
	'*%Update: updates a registry to the table "FIN700_lines" using the key for this table.
	'% Update: Actualiza un registro a la tabla "FIN700_lines" usando la clave para dicha tabla.
	Public Function Update(ByVal nLed_compan As Integer, ByVal nArea_Led As Integer, ByVal nGroup As Integer, ByVal nTransac_Ty As Integer, ByVal sAccount_base As String, ByVal sAccount_FIN700 As String, ByVal nUsercode As Integer) As Boolean
		
		Dim lclsFIN700_lines As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lclsFIN700_lines = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.insupdFIN700_lines'. Generated on 17/10/2002 01:56:23 p.m.
		With lclsFIN700_lines
			.StoredProcedure = "insUpdFIN700_lines"
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArea_Led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_Ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_base", sAccount_base, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount_FIN700", sAccount_FIN700, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsFIN700_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFIN700_lines = Nothing
	End Function
	'% UpdChequesFIN700: Actualiza un registro a la tabla "Cheques" usando la clave para dicha tabla.
	Private Function UpdChequesFIN700(ByVal nRequest_nu As Integer, ByVal sCheque As String, ByVal nConsec As Integer, ByVal sFile_FIN700 As String, ByVal dDate_FIN700 As Date) As Boolean
		
		Dim lclsFIN700_lines As eRemoteDB.Execute
		
		'On Error GoTo Update_Err
		
		lclsFIN700_lines = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.insupdFIN700_lines'. Generated on 17/10/2002 01:56:23 p.m.
		With lclsFIN700_lines
			.StoredProcedure = "UpdCheques_fin700"
			.Parameters.Add("nLed_compan", nRequest_nu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCheque", sCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFile_Fin700", sFile_FIN700, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_FIN700", dDate_FIN700, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdChequesFIN700 = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			UpdChequesFIN700 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsFIN700_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsFIN700_lines = Nothing
		
	End Function
	
	'%insPostCPL778_K: Función que realiza las actualizaciones del proceso CPL778
	Public Function insPostCPL778_K(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nConcept As Integer) As Boolean
		'-Se define la variable que permitira accesar los datos a procesar.
		
		Dim lexeCPL778 As New eRemoteDB.Execute
		Dim lRea_Tab_Equal_All As New eRemoteDB.Execute
		Dim lUpdCheques_Fin700 As New eRemoteDB.Execute
		Dim lblnRead As Boolean
		
		'-Se define la variable para reliazar la busqueda de la informacion de la compañia contable
		Dim lclsLed_compan As eLedge.Led_compan = New eLedge.Led_compan
		'-Se define la variable para realizar la busqueda de la informacion de opciones de instalacion contables
		Dim lclsOpt_ledger As New Opt_ledger
		'-Se define la variable que contendra el texto de la linea a imprimir en el archivo detalle y encabezado
		Dim lstrWritTxt As String
		'-Se define la variable  que contendra el nombre del archivo a generar.
		Dim lstrFileName As String
        '-Se define la variables  que contendran la ruta y nombre del archivo a generar.
        Dim lstrLoadFile As String = ""
        Dim lstrFile_FIN700 As String
		
		'-Se definen las variables que contendran: el año, el mes, y el calculo del periodo
		
		Dim lintYear As Integer
		Dim lintMonth As Integer
		Dim lintPeriod As Integer
		Dim lintDay As Integer
		
		Dim lintCount As Integer
		Dim ldtmAccountDate As Date
        Dim sCompanyDescript As String = ""
        Dim lclsQuery As New eRemoteDB.Query
		Dim lintLine As Integer
        Dim lstrClientName As String = ""
        Dim lclsvalue As eFunctions.Values
		Dim lintSesion As Integer
		Dim lintlength As Integer
		Dim lintCompanyAnt As Integer
		
		On Error GoTo insPostCPL778_K_Err
		
		With lexeCPL778
			.StoredProcedure = "reaCheques_CPL778"
			.Parameters.Add("dDat_Propos", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConcept", nConcept, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 20, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				insPostCPL778_K = True
				lRea_Tab_Equal_All.StoredProcedure = "ReaTab_Equal_All"
				If lRea_Tab_Equal_All.Run(True) Then
					Do While Not lRea_Tab_Equal_All.EOF
						Do While Not .EOF
							If .FieldToClass("nCompany") = lRea_Tab_Equal_All.FieldToClass("nLed_Compan") Then
								lintCompanyAnt = .FieldToClass("nCompany")
								lblnRead = True
							Else
								lblnRead = False
								Exit Do
							End If
							.RNext()
						Loop 
						If Not lblnRead Then
							Exit Do
						End If
						lRea_Tab_Equal_All.RNext()
					Loop 
				End If
				
				
				
				'+Si se encuentran registros a procesar se realiza la busqueda del año Fin700 en la tabla de opciones de instalacion
				lclsOpt_ledger = New Opt_ledger
				Call lclsOpt_ledger.Find()
				'           Call lclsLed_compan.Find(.FieldToClass("lintCompanyAnt"))
				Call lclsLed_compan.Find(lintCompanyAnt)
				
				If lclsLed_compan.dIniLedDat <> eRemoteDB.Constants.dtmNull Then
					lintYear = Year(lclsLed_compan.dIniLedDat)
					lintMonth = Month(lclsLed_compan.dDate_init)
					lintDay = VB.Day(lclsLed_compan.dDate_init)
				Else
					lintYear = Year(Today)
					lintMonth = Month(Today)
					lintDay = VB.Day(Today)
				End If
				
				If lclsQuery.OpenQuery("Table5565", "sShort_des", "nTypeCode='" & lintCompanyAnt & "'") Then
					sCompanyDescript = lclsQuery.FieldToClass("sShort_des")
					lclsQuery.CloseQuery()
				End If
				
				'+ Se construye el nombre del archivo a enviar al FIN700
				'sCompanyDescript = "CV"
				
				'+ Se busca la ruta en la que se guardará el archivo de texto
				
				'+ Se busca el directorio virtual del archivo a crear
				lclsvalue = New eFunctions.Values
				On Error Resume Next
				lstrFile_FIN700 = UCase(lclsvalue.insGetSetting("LoadFile", String.Empty, "Config"))
				If lstrFile_FIN700 = String.Empty Then
					lstrFile_FIN700 = UCase(lclsvalue.insGetSetting("VirtualRootLoad", String.Empty, "PATHS"))
				End If
				
				On Error GoTo insPostCPL778_K_Err
				
				lintlength = Len(lstrFile_FIN700)
				
				If Mid(lstrFile_FIN700, lintlength, 1) <> "\" Then
					lstrFile_FIN700 = lstrFile_FIN700 & "\"
				End If
				
				lstrFile_FIN700 = lstrLoadFile & lstrFile_FIN700
				
				lexeCPL778.RCloseRec()
				lexeCPL778.Run()
				
				lstrFile_FIN700 = lstrFile_FIN700 & CStr(lintYear) & CStr(lintMonth) & CStr(lintDay) & CStr(.FieldToClass("nConsec")) & "O" & sCompanyDescript & ".txt"
				
				lstrFileName = CStr(lintYear) & CStr(lintMonth) & CStr(lintDay) & CStr(.FieldToClass("nConsec")) & "O" & sCompanyDescript
				'UPGRADE_NOTE: Object lclsvalue may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsvalue = Nothing
				lintSesion = FreeFile
				FileOpen(lintSesion, lstrFile_FIN700, OpenMode.Output)
				'            If Not lblnRead Then
				''+ Antes de generarse el archivo con el formato 1 debe verificarse la información de la
				''+ tabla de "códigos equivalentes en VisualTIME y el FIN700" (tab_equal).
				''+ Si hay alguna inconsistencia se debe generar un archivo de log indicando la fecha y hora
				''+ del proceso, advirtiendo que el archivo no fue generado y detallando el error
				'            End If
				
				'+ Este proceso NO debe procesar las órdenes de pagos cuyo tipo de
				'+ concepto sea "pagos de honorarios"
				lintLine = 0
				Do While Not .EOF
					If .FieldToClass("nConcept") <> 7 Then
						If lclsQuery.OpenQuery("Client", "sClieName", "sClient='" & .FieldToClass("sClient") & "'") Then
							lstrClientName = lclsQuery.FieldToClass("sClieName")
							lclsQuery.CloseQuery()
						End If
						
						lintLine = lintLine + 1
						lstrWritTxt = FIN700_Format1(3, .FieldToClass("dDat_propos"), .FieldToClass("nOffice"), lintPeriod, 300, .FieldToClass("nConsec"), lintLine, 0, CStr(.FieldToClass("nCurrencyOri")), String.Empty, .FieldToClass("nRequest_nu"), .FieldToClass("sClient"), 0, .FieldToClass("sClient"), 0, 0, .FieldToClass("dStat_Date"), .FieldToClass("nAmount"), .FieldToClass("nAmount"), .FieldToClass("nAmount_local"), .FieldToClass("nAmount_local"), "CPL778", lstrClientName, CStr(.FieldToClass("nSta_Cheque")), .FieldToClass("dStat_Date"))
						
						PrintLine(lintSesion, lstrWritTxt)
						
						Call UpdChequesFIN700(.FieldToClass("nRequest_nu"), .FieldToClass("sCheque"), .FieldToClass("nConsec"), lstrFileName, dEffecdate)
					End If
					.RNext()
				Loop 
				FileClose(lintSesion)
			End If
		End With
		
		insPostCPL778_K = True
		
insPostCPL778_K_Err: 
		If Err.Number Then
			insPostCPL778_K = False
		End If
		'UPGRADE_NOTE: Object lexeCPL778 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lexeCPL778 = Nothing
		'UPGRADE_NOTE: Object lRea_Tab_Equal_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lRea_Tab_Equal_All = Nothing
		
		On Error GoTo 0
	End Function
	
	Public Function insValCPL778_K(ByVal sCodispl As String, ByVal dProcessDate As Date) As String
		Dim lclsErrors As New eFunctions.Errors
		
		On Error GoTo insValCPL778_K_err
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(dProcessDate) Then
			Call lclsErrors.ErrorMessage(sCodispl, 60387)
		End If
		
		insValCPL778_K = lclsErrors.Confirm
		
insValCPL778_K_err: 
		If Err.Number Then
			insValCPL778_K = sCodispl & " - " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
		
	End Function
End Class






