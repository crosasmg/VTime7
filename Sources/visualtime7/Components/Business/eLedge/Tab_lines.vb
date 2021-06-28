Option Strict Off
Option Explicit On
Public Class Tab_lines
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_lines.cls                            $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 21/08/03 16.29                               $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'**- The properties of the class are defined
	'-   Se definen las propiedades de la clase
	
	'**- Column_name                                                                                                                      Type                                                                                                                             Computed                            Length      Prec  Scale  Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'-   Nombre de la columna                                                                                                             Tipo                                                                                                                             Computed                            Longitud    Prec  Escala Admite nulos                        TrimTrailingBlanks                  FixedLenNullInSource                Collation
	Public nArea_Led As Integer '                                                                                                  char                                                                                                                             no                                  2                        no                                  no                                  no                                  SQL_Latin1_General_CP1_CI_AS
	Public nTransac_Ty As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public nTratypei As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public nReceipt_ty As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public nProduct_ty As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public sPay_type As String '                                                                                                  char                                                                                                                             no                                  2                        no                                  no                                  no                                  SQL_Latin1_General_CP1_CI_AS
	Public nLed_compan As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      yes                                 (n/a)                               (n/a)                               NULL
	Public nTyp_acco As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public dCompdate As Date '                                                                                                  datetime                                                                                                                         no                                  8                        no                                  (n/a)                               (n/a)                               NULL
	Public sStatregt As String '                                                                                                  char                                                                                                                             no                                  1                        yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public nUsercode As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      no                                  (n/a)                               (n/a)                               NULL
	Public nGroup As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0      yes                                 (n/a)                               (n/a)                               NULL
	
	'**% updTab_lines: This function Allows to update the information of the automatic general ledger entries
	'%   updTab_lines: Modifica los datos del encabezado de las guias contables
	Public Function updTab_lines(ByVal nArea As Integer, ByVal ntTransac_ty As Integer, ByVal ntTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer, ByVal nGroup As Integer, ByVal nUsercode As Integer, ByVal sStatregt As String) As Boolean
		Dim lrecTab_lines As eRemoteDB.Execute
		
		lrecTab_lines = New eRemoteDB.Execute
		
		On Error GoTo updTab_lines_err
		
		updTab_lines = False
		
		'**+ Store procedure parameters definition - 'insudb.updTab_lines'
		'+   Definicion de parametros para stored procedure 'insudb.updTab_lines'
		
		With lrecTab_lines
			.StoredProcedure = "updTab_lines"
			
			.Parameters.Add("nArea_led", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", ntTransac_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", ntTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", sPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			updTab_lines = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecTab_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_lines = Nothing
		
updTab_lines_err: 
		If Err.Number Then
			updTab_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% delTab_lines: This function  deletes the information of the automatic general ledger entries
	'%   delTab_lines: Borrar los datos del encabezado de las guias contables
	Public Function delTab_lines(ByVal nArea As Integer, ByVal ntTransac_ty As Integer, ByVal ntTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer) As Boolean
		
		Dim lstrPay_type As String
		
		Dim lrecTab_lines As eRemoteDB.Execute
		
		lrecTab_lines = New eRemoteDB.Execute
		
		On Error GoTo delTab_lines_err
		
		If sPay_type = String.Empty Then
			lstrPay_type = "0"
		Else
			lstrPay_type = sPay_type
		End If
		
		delTab_lines = False
		
		'**+ Store procedure parameters definition - 'insudb.delTab_lines'
		'+   Definicion de parametros para stored procedure 'insudb.delTab_lines'
		
		With lrecTab_lines
			.StoredProcedure = "delTab_lines"
			
			.Parameters.Add("nArea_led", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", ntTransac_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", ntTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", lstrPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			delTab_lines = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecTab_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_lines = Nothing
		
delTab_lines_err: 
		If Err.Number Then
			delTab_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% creTab_lines: This function  updates the information of the automatic general ledger entries
	'%   creTab_lines: Actualiza los datos del encabezado de las guias contables
	Public Function creTab_lines(ByVal nArea As Integer, ByVal ntTransac_ty As Integer, ByVal ntTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer, ByVal nGroup As Integer, ByVal nUsercode As Integer, ByVal sStatregt As String) As Boolean
		Dim lrecTab_lines As eRemoteDB.Execute
		
		lrecTab_lines = New eRemoteDB.Execute
		
		On Error GoTo creTab_lines_err
		
		creTab_lines = False
		
		'**+ Store procedure parameters definition - 'insudb.creTab_lines'
		'+   Definicion de parametros para stored procedure 'insudb.creTab_lines'
		
		With lrecTab_lines
			.StoredProcedure = "creTab_lines"
			
			.Parameters.Add("nArea_led", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", ntTransac_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", ntTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", sPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			creTab_lines = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecTab_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_lines = Nothing
		
creTab_lines_err: 
		If Err.Number Then
			creTab_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insReaTab_lines: This function  reads the information of the automatic general ledger entries
	'%   insReaTab_lines: Leer los datos del encabezado de las guias contables
	Public Function insReaTab_lines(ByVal nArea As Integer, ByVal ntTransac_ty As Integer, ByVal ntTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer) As Boolean
		Dim lrecTab_lines As eRemoteDB.Execute
		
		lrecTab_lines = New eRemoteDB.Execute
		
		On Error GoTo insReaTab_lines_err
		
		insReaTab_lines = False
		
		'**+ Store procedure parameters definition - 'insudb.reaTab_lines'
		'+   Definicion de parametros para stored procedure 'insudb.reaTab_lines'
		
		With lrecTab_lines
			.StoredProcedure = "reaTab_lines"
			
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaLed", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", ntTransac_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", ntTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", sPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				insReaTab_lines = True
				
				nGroup = .FieldToClass("nGroup")
				
				.RCloseRec()
			Else
				nGroup = 0
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecTab_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_lines = Nothing
		
insReaTab_lines_err: 
		If Err.Number Then
			insReaTab_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insValMCP001_k: Page header validation routine.
	'%   insValMCP001_k: Rutina de validación del encabezado de la ventana.
	Public Function insValMCP001_k(ByVal nArea As Integer, ByVal ntTransac_ty As Integer, ByVal ntTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal nPay As Integer, ByVal nPayType As Integer, ByVal nPayTypeC As Integer, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer, ByVal sCodispl As String, ByVal nAction As Integer, ByVal nGroup As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim sPay_type As String
		
		Dim lblnError As Boolean
		
		On Error GoTo insValMCP001_k_Err
		
		lclsErrors = New eFunctions.Errors
		
		insValMCP001_k = String.Empty
		lblnError = False
		sPay_type = "0"
		
		'+   Validación del campo compañía
		If nLed_compan = 0 Or nLed_compan = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7169)
			lblnError = True
		End If
		
		'+   Validación del campo área
		If nArea = 0 Or nArea = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36200)
			lblnError = True
		End If
		
		'+   Validación del campo transacción, siempre y cuando el area no sea Co/Reaseguro
		If nArea > 0 And nArea <> 4 Then
			If ntTransac_ty = 0 Or ntTransac_ty = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 20023)
				lblnError = True
			End If
		End If
		
		'+   Validación de campo grupo, siempre y cuando el area no sea Co/Reaseguro
		If nArea > 0 And nArea <> 4 And (nGroup = eRemoteDB.Constants.intNull Or nGroup = 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10174)
		End If
		
		If nArea <> 0 And nArea <> eRemoteDB.Constants.intNull Then
			
			'+   Recibo - Origen
			If (ntTratypei = 0 Or ntTratypei = eRemoteDB.Constants.intNull) And nArea = 1 Then
				Call lclsErrors.ErrorMessage(sCodispl, 36202)
				lblnError = True
			End If
			
			'+   Siniestro - Pago
			'+   Valores tomados de la table140 - valTransacty - nPayType
			
			If nArea = 2 And (ntTransac_ty = 5 Or ntTransac_ty = 10 Or ntTransac_ty = 11 Or ntTransac_ty = 12 Or ntTransac_ty = 15 Or ntTransac_ty = 20 Or ntTransac_ty = 23) Then
				If nPayType = 0 Or nPayType = eRemoteDB.Constants.intNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 4045)
					lblnError = True
				Else
					sPay_type = Trim(CStr(nPayType))
				End If
			End If
			
			If nArea = 5 Then
				If nPayTypeC > 0 Then
					sPay_type = Trim(CStr(nPayTypeC))
				End If
			End If
			
			'+   Tipo de cuenta corriente
			If (nTyp_acco = 0 Or nTyp_acco = eRemoteDB.Constants.intNull) And nArea = 3 Then
				Call lclsErrors.ErrorMessage(sCodispl, 7107)
				lblnError = True
			End If
			
			If Not lblnError Then
				
				'+   Si la acción es consultar el registro debe existir en la tabla tab_lines
				
				If nAction = eFunctions.Menues.TypeActions.clngActionQuery Or nAction = eFunctions.Menues.TypeActions.clngActioncut Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
					If Not insReaTab_lines(nArea, IIf(ntTransac_ty = eRemoteDB.Constants.intNull, 0, ntTransac_ty), ntTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36211)
					End If
				End If
				
				'+ Si la acción es agregar el registro no debe de existir en la tabla tab_lines.
				
				If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
					If insReaTab_lines(nArea, IIf(ntTransac_ty = eRemoteDB.Constants.intNull, 0, ntTransac_ty), ntTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan) Then
						Call lclsErrors.ErrorMessage(sCodispl, 36210)
					End If
				End If
			End If
		End If
		
		Me.sPay_type = sPay_type
		
		insValMCP001_k = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMCP001_k_Err: 
		If Err.Number Then
			insValMCP001_k = insValMCP001_k & Err.Description
		End If
		
		On Error GoTo 0
	End Function
End Class






