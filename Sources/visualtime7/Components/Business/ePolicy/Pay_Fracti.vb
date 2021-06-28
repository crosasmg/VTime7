Option Strict Off
Option Explicit On
Public Class Pay_Fracti
	'%-------------------------------------------------------%'
	'% $Workfile:: Pay_Fracti.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 13/10/03 4:59p                               $%'
	'% $Revision:: 19                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema 11/01/2000
	'+ Los campos llaves corresponden a sClient, dFinanDate y  nConcept
	
	'+ Column_name              Type
	'+ ------------------------ -----------------------
	
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPayfreq As Integer
	Public nQuota As Integer
	Public dEffecdate As Date
	Public dNulldate As Date
	Public nRatepayf As Double
	Public dCompdate As Date
	Public nUsercode As Integer
	Public nPayfreq_p As Integer
	
	Public sStatregt As String
	Public nAction As Integer
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	Private mdtmEffecdate As Date
	
	'% Find: Leer la información de las frecuencias de pago permitidas y recargo por
	'        fraccionamiento del pago.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nQuota As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreapay_practi As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPayfreq <> nPayfreq Or Me.nQuota <> nQuota Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			'+ Definición de parámetros para stored procedure 'insudb.reapay_practi'
			lrecreapay_practi = New eRemoteDB.Execute
			With lrecreapay_practi
				.StoredProcedure = "reapay_fracti_o"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPayFreq", nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nQuota", nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nPayfreq = nPayfreq
					nQuota = nQuota
					Me.dEffecdate = .FieldToClass("dEffecdate")
					mdtmEffecdate = Me.dEffecdate
					dNulldate = .FieldToClass("dNulldate")
					nRatepayf = .FieldToClass("nRatePayf")
					.RCloseRec()
					Find = True
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreapay_practi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreapay_practi = Nothing
		On Error GoTo 0
	End Function
	
	'% ValDuplicateQuote:  Valida que no exista cuotas duplicadas
	Public Function ValDuplicateQuote(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nQuota As Integer) As Boolean
		Dim lrecreapay_fracti As eRemoteDB.Execute
		
		On Error GoTo ValDuplicateQuote_Err
		
		lrecreapay_fracti = New eRemoteDB.Execute
		
		With lrecreapay_fracti
			.SQL = " Select * from pay_fracti " & " Where nBranch  = " & nBranch & "   and nproduct = " & nProduct & "   and nPayFreq = " & nPayfreq & "   and nQuota   = " & nQuota
			If .Run Then
				ValDuplicateQuote = True
			Else
				ValDuplicateQuote = False
			End If
		End With
		
ValDuplicateQuote_Err: 
		If Err.Number Then
			ValDuplicateQuote = False
		End If
		
		'UPGRADE_NOTE: Object lrecreapay_fracti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreapay_fracti = Nothing
		
	End Function
	
	'%insValDP010_Upd: Rutina que valida columna por columna y fila por fila los valores del Tdbgrid.
	Public Function insValDP010_Upd(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nQuota As Integer, ByVal nRatepayf As Double, ByVal nStatregt As Integer, ByVal nStatregtTble As Integer, ByVal dEffecdate As Date, ByVal nStatus As Integer, Optional ByVal sAction As String = "") As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPay_fracti As ePolicy.Pay_Fracti
		Dim lcolPay_fractis As ePolicy.Pay_fractis
		Dim lclsValTime As eFunctions.valField
		Dim lclsValues As eFunctions.Values
		Dim lclsQuery As eRemoteDB.Query
		
		Dim lintPayFrequency As Integer
		Dim lintCount As Integer
		
		On Error GoTo insValDP010_Upd_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		lclsValTime = New eFunctions.valField
		lclsPay_fracti = New ePolicy.Pay_Fracti
		lcolPay_fractis = New ePolicy.Pay_fractis
		lclsValTime.objErr = lclsErrors
		lclsQuery = New eRemoteDB.Query
		
		'+ Se excluye la forma de pagos de Cuota, valor "8"; ya que pueden repetirse.
		If sAction <> String.Empty And sAction = "Add" Then
			If nPayfreq <> eRemoteDB.Constants.intNull And nPayfreq <> 0 Then
				If lcolPay_fractis.Find(nBranch, nProduct, dEffecdate) Then
					For	Each lclsPay_fracti In lcolPay_fractis
						lintPayFrequency = lclsPay_fracti.nPayfreq
						If lintPayFrequency = nPayfreq And nPayfreq <> 8 Then
                            Call lclsErrors.ErrorMessage("DP010", 1926, , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.getMessage(248))
							'UPGRADE_NOTE: Object lcolPay_fractis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lcolPay_fractis = Nothing
							Exit For
						End If
					Next lclsPay_fracti
					lclsPay_fracti = New ePolicy.Pay_Fracti
				End If
			End If
		End If
		
		'+ Se valida la columna 1: Frecuencia de Pago.
		If nPayfreq = 0 Or nPayfreq = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("DP010", 56165, , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.getMessage(248))
		End If
		
		If nStatregt = 0 Or nStatregt = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("DP010", 9089, , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.getMessage(249))
		End If
		
		'+ Se valida la columna 2: Cantidad de cuotas.
		If nPayfreq = 8 Then
			'+ Si la columna de forma de pago tiene valor y cantidad de cuotas, se verifica que no exista en la ventana.
			If nQuota <> 0 Or nQuota <> eRemoteDB.Constants.intNull Then
				If nStatus = 1 Then
					If lclsPay_fracti.ValDuplicateQuote(nBranch, nProduct, nPayfreq, nQuota) Then
                        Call lclsErrors.ErrorMessage("DP010", 1927)
					End If
				End If
			End If
		End If
		
		'+ Se valida la columna 3: Porcentaje.
		If (nPayfreq = 1 Or nPayfreq = 6) And (Fix(nRatepayf) <> eRemoteDB.Constants.intNull And Fix(nRatepayf) <> 0) Then
			'+ Debe estar sin valor cuando la fracuencia de pago es única o anual
			Call lclsErrors.ErrorMessage("DP010", 11359)
		End If
		
		'+Se valida que el interes este en el rango
		If nRatepayf <> 0 And Fix(nRatepayf) <> eRemoteDB.Constants.intNull Then
			lclsValTime.ValFormat = "#####0.##"
			lclsValTime.Max = 999999.99
			lclsValTime.Min = 0.01
            lclsValTime.Descript = eFunctions.Values.getMessage(247)
			Call lclsValTime.ValNumber(nRatepayf)
		End If
		
		'Se valida que el estado no se pueda cambiar de activo a instalado si esta insertado en la tabla (nStatus = 1)
		If nStatregt = 2 And nStatregtTble <> 2 Then
			Call lclsErrors.ErrorMessage("DP010", 11218)
		End If
		insValDP010_Upd = lclsErrors.Confirm
		
insValDP010_Upd_Err: 
		If Err.Number Then
			insValDP010_Upd = "insValDP010_Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPay_fracti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPay_fracti = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
		'UPGRADE_NOTE: Object lcolPay_fractis may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolPay_fractis = Nothing
		'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTime = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'%insPostDP010: Esta función realiza los cambios de BD según especificaciones funcionales
	'%              de la transacción (DP010)
	Public Function insPostDP010(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal dEffecdate As Date, ByVal nRatepayf As Double, ByVal sStatregt As String, ByVal nQuota As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP010_Err
		
		lclsProd_win = New eProduct.Prod_win
		
		With Me
			.nAction = nAction
			.nBranch = nBranch
			.nProduct = nProduct
			.nPayfreq = nPayfreq
			.dEffecdate = dEffecdate
			.nRatepayf = nRatepayf
			.sStatregt = sStatregt
			.nQuota = nQuota
			.nUsercode = nUsercode
		End With
		
		insPostDP010 = insPay_Fracti
		If insPostDP010 Then
			Call lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP010", "2", nUsercode)
		End If
		
insPostDP010_Err: 
		If Err.Number Then
			insPostDP010 = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
		
	End Function
	
	'% insPay_Fracti: Inserta un registro en la tabla pay_fracti
	Public Function insPay_Fracti() As Boolean
		Dim lrecinsPay_Fracti As eRemoteDB.Execute
		
		On Error GoTo insPay_Fracti_Err
		
		lrecinsPay_Fracti = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insPay_Fracti'
		'+ Información leída el 10/04/2001 12:00:15 p.m.
		
		With lrecinsPay_Fracti
			.StoredProcedure = "insPay_Fracti"
			.Parameters.Add("nAction", Me.nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", Me.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", Me.nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayfreq", Me.nPayfreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Me.dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatepayf", Me.nRatepayf, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", Me.sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuota", Me.nQuota, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", Me.nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPay_Fracti = .Run(False)
		End With
		
insPay_Fracti_Err: 
		If Err.Number Then
			insPay_Fracti = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecinsPay_Fracti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPay_Fracti = Nothing
		
    End Function


End Class






