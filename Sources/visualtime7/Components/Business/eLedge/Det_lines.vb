Option Strict Off
Option Explicit On
Public Class Det_lines
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_lines.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:18p                                $%'
	'% $Revision:: 29                                       $%'
	'%-------------------------------------------------------%'
	
	'**- The properties of the class are defined
	'-   Se definen las propiedades de la clase
	
	'**- Column_name                                                                                                                      Type                                                                                                                             Computed                            Length      Prec  Scale  Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'-   Nombre de la columna                                                                                                             Tipo                                                                                                                             Computed                            Longitud    Prec  Escala Admite nulos                        TrimTrailingBlanks                  FixedLenNullInSource                Collation
	Public nArea_Led As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nTransac_Ty As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nTratypei As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nReceipt_ty As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nProduct_ty As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public sPay_type As String '                                                                                                  char                                                                                                                             no                                  2                       no                                  no                                  no                                  SQL_Latin1_General_CP1_CI_AS
	Public nTyp_acco As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public sAccount As String '                                                                                                  char                                                                                                                             no                                  20                      yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public nConsec As Integer '                                                                                                  int                                                                                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)                               NULL
	Public sAux_accoun As String '                                                                                                  char                                                                                                                             no                                  20                      yes                                 no                                  yes                                 SQL_Latin1_General_CP1_CI_AS
	Public dCompdate As Date '                                                                                                  datetime                                                                                                                         no                                  8                       no                                  (n/a)                               (n/a)                               NULL
	Public nComplement As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nLine_Type As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nParameter As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     yes                                 (n/a)                               (n/a)                               NULL
	Public nUsercode As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public nLed_compan As Integer '                                                                                                  smallint                                                                                                                         no                                  2           5     0     no                                  (n/a)                               (n/a)                               NULL
	Public sExist As Integer
	Public nGroup As Integer
	Public sPay_form As String
	Public sDescript As String
	
	'**% insCreDet_lines: Function that permit to create the records in the Det_lines table.
	'%   insCreDet_lines: Función que permite crear los registros en la tabla Det_lines.
	Public Function insCreDet_lines() As Boolean
		
		'**- Define the parameter arrengement to pass to the stored procedure.
		'-   Se define el arreglo de parámetro a pasar al store procedure.
		
		Dim lrecDet_lines As eRemoteDB.Execute
		Dim lclsTab_lines As eLedge.Tab_lines
		
		lrecDet_lines = New eRemoteDB.Execute
		lclsTab_lines = New eLedge.Tab_lines
		
		On Error GoTo insCreDet_lines_err
		
		insCreDet_lines = True
		
		If Not lclsTab_lines.insReaTab_lines(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan) Then
			If Not lclsTab_lines.creTab_lines(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan, nGroup, nUsercode, "1") Then
				insCreDet_lines = False
			End If
		End If
		
		If insCreDet_lines Then
			With lrecDet_lines
				.StoredProcedure = "creDet_lines"
				
				.Parameters.Add("nArea_led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPay_type", sPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAux_Accoun", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nComplement", nComplement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLine_type", nLine_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nParameter", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPay_form", sPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				insCreDet_lines = .Run(False)
			End With
			
			'UPGRADE_NOTE: Object lrecDet_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecDet_lines = Nothing
		End If
		
insCreDet_lines_err: 
		If Err.Number Then
			insCreDet_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% updDet_lines: Function that permit to update the records in the Det_lines table.
	'%   updDet_lines: Función que permite actualizar los registros en la tabla Det_lines.
	Public Function updDet_lines() As Boolean
		
		'**- Define the parameter arrengement to pass to the stored procedure.
		'-   Se define el arreglo de parámetro a pasar al store procedure.
		
		Dim lstrPay_type As String
		
		Dim lrecDet_lines As eRemoteDB.Execute
		
		lrecDet_lines = New eRemoteDB.Execute
		
		If sPay_type = String.Empty Then
			lstrPay_type = "0"
		Else
			lstrPay_type = sPay_type
		End If
		
		
		On Error GoTo updDet_lines_err
		
		updDet_lines = True
		
		With lrecDet_lines
			.StoredProcedure = "updDet_lines"
			
			.Parameters.Add("nArea_led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", lstrPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_Accoun", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComplement", nComplement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLine_type", nLine_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParameter", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", sPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			updDet_lines = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecDet_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_lines = Nothing
		
updDet_lines_err: 
		If Err.Number Then
			updDet_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insDelDet_lines: Function that permit to delete the records in the Det_lines table.
	'%   insDelDet_lines: Función que permite borrar los registros en la tabla Det_lines.
	Public Function insDelDet_lines() As Boolean
		
		'**- Define the parameter arrengement to pass to the stored procedure.
		'-   Se define el arreglo de parámetro a pasar al store procedure.
		
		Dim lstrPay_type As String
		
		Dim lrecDet_lines As eRemoteDB.Execute
		Dim lclsTab_lines As eLedge.Tab_lines
		
		lrecDet_lines = New eRemoteDB.Execute
		lclsTab_lines = New eLedge.Tab_lines
		
		On Error GoTo insDelDet_lines_err
		
		insDelDet_lines = True
		
		If sPay_type = String.Empty Then
			lstrPay_type = "0"
		Else
			lstrPay_type = sPay_type
		End If
		
		
		With lrecDet_lines
			.StoredProcedure = "delDet_lines"
			
			.Parameters.Add("nArea_led", nArea_Led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", lstrPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insDelDet_lines = .Run(False)
			
			If insDelDet_lines Then
				If Not Find(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan) Then
					insDelDet_lines = lclsTab_lines.delTab_lines(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan)
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecDet_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_lines = Nothing
		
insDelDet_lines_err: 
		If Err.Number Then
			insDelDet_lines = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insPostMCP001: This function is incharge to validate all the introduced data in the form
	'%  insPostMCP001: Esta función se encaga de actualizar todos los datos introducidos en la forma
	Public Function insPostMCP001(ByVal sAction As String, ByVal nConsec As Integer, ByVal nArea_Led As Integer, ByVal nTransac_Ty As Integer, ByVal nTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nDebit As Integer, ByVal nCredit As Integer, ByVal nParameter As Integer, ByVal nComplement As Integer, ByVal sAccount As String, ByVal nUsercode As Integer, ByVal nLed_compan As Integer, ByVal nGroup As Integer, ByVal sPay_form As String) As Boolean
		Dim lclsTab_lines As eLedge.Tab_lines
		
		lclsTab_lines = New eLedge.Tab_lines
		
		On Error GoTo insPostMCP001_err
		
		insPostMCP001 = True
		
		Me.nGroup = nGroup
		
		If Me.nGroup = eRemoteDB.Constants.intNull Then
			Me.nGroup = 0
		End If
		
		With Me
			.nLed_compan = nLed_compan
			
			If nConsec = eRemoteDB.Constants.intNull Then
				Me.nConsec = 0
			Else
				Me.nConsec = nConsec
			End If
			
			.nArea_Led = nArea_Led
			.nTransac_Ty = nTransac_Ty
			.nTratypei = nTratypei
			
			If nReceipt_ty = eRemoteDB.Constants.intNull Then
				.nReceipt_ty = 0
			Else
				.nReceipt_ty = nReceipt_ty
			End If
			
			.nProduct_ty = nProduct_ty
			If sPay_type = String.Empty Then
				.sPay_type = "0"
			Else
				.sPay_type = sPay_type
			End If
			If nTyp_acco = eRemoteDB.Constants.intNull Then
				.nTyp_acco = 0
			Else
				.nTyp_acco = nTyp_acco
			End If
			.sAccount = sAccount
			.nComplement = nComplement
			.sPay_form = sPay_form
			
		End With
		If nDebit = eRemoteDB.Constants.intNull Then
			nDebit = 0
		End If
		
		If nCredit = eRemoteDB.Constants.intNull Then
			nCredit = 0
		End If
		
		If nDebit = 1 Then
			Me.nLine_Type = 1
		Else
			Me.nLine_Type = 2
		End If
		
		Me.nParameter = nParameter
		Me.nUsercode = nUsercode
		
		If sAction <> "" Then
			If sAction = "Add" Then
				insPostMCP001 = insCreDet_lines
				
				If insPostMCP001 Then
					insPostMCP001 = lclsTab_lines.updTab_lines(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan, nGroup, nUsercode, "2")
				End If
			End If
			
			If sAction = "Update" Then
				insPostMCP001 = updDet_lines
				
				If insPostMCP001 Then
					insPostMCP001 = lclsTab_lines.updTab_lines(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan, nGroup, nUsercode, "2")
				End If
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsTab_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_lines = Nothing
		
insPostMCP001_err: 
		If Err.Number Then
			insPostMCP001 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% Find: The Det_lines information is read
	'%   Find: Devuelve los datos de la tabla Det_lines
	Public Function Find(ByVal nArea As Integer, ByVal nTransac_Ty As Integer, ByVal nTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer) As Boolean
		
		'**- Define the variable lrecDet_lines
		'-   Se define la variable lrecDet_lines
		
		Dim lstrPay_type As String
		Dim lrecDet_lines As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecDet_lines = New eRemoteDB.Execute
		
		Find = False
		
		If sPay_type = String.Empty Then
			lstrPay_type = "0"
		Else
			lstrPay_type = sPay_type
		End If
		
		'**+ Parameters definition for the stored procedure 'insudb.reaDet_lines'
		'+   Definicion de parametros para stored procedure 'insudb.reaDet_lines'
		
		With lrecDet_lines
			.StoredProcedure = "reaDet_lines"
			
			.Parameters.Add("nAreaLed", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", lstrPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nConsec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				.RCloseRec()
				Find = True
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecDet_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_lines = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% Find: Busca la existencia del asiento contable de la cuenta seleccionada
	'% para eliminar.
	Public Function FindAccount(ByVal nLed_compan As Integer, ByVal sAccount As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		'- Se define la variable lrecreaAcc_lines
		Dim lrecreaAcc_lines As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If lblnFind Then
			FindAccount = False
			
			lrecreaAcc_lines = New eRemoteDB.Execute
			
			'+ Definicion de parametros para stored procedure 'insudb.reaAcc_lines'
			'+ Informacion leida el 19/06/2001 12:09:30 PM
			With lrecreaAcc_lines
				.StoredProcedure = "reaAcc_linesAccount"
				.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run(True) Then
					If .FieldToClass("sAccount") > "" Then
						FindAccount = True
					End If
				End If
			End With
		End If
		
		'UPGRADE_NOTE: Object lrecreaAcc_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAcc_lines = Nothing
		
Find_Err: 
		If Err.Number Then
			FindAccount = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insvalLocksAcc: It is determinated whether the account is not blocked the debit or credit
	'%   insvalLocksAcc: Valida que las cuentas no tengan bloquedos el debe o haber
	Public Function insValLocksAcc(ByRef lerrTime As Object, ByRef lstrCodispl As String, ByRef lintLed_Compan As Integer, ByRef sAccount As String, ByRef nDebit As Integer, ByRef nCredit As Integer) As Boolean
		Dim lrecLedger_acc As eRemoteDB.Execute
		
		On Error GoTo insValLocksAcc_Err
		
		lrecLedger_acc = New eRemoteDB.Execute
		
		insValLocksAcc = True
		
		With lrecLedger_acc
			.StoredProcedure = "reaLedger_acc"
			
			.Parameters.Add("nLed_compan", lintLed_Compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", Trim(sAccount), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAux_accoun", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				
				'**+ Debit
				'+   Débito
				
				If nDebit = 1 And .FieldToClass("sBlock_deb") = "1" Then
					Call lerrTime.ErrorMessage(lstrCodispl, 36053)
					insValLocksAcc = False
					
					'**+ Credit
					'+   Crédito
					
				ElseIf nCredit = 1 And .FieldToClass("sBlock_cre") = "1" Then 
					Call lerrTime.ErrorMessage(lstrCodispl, 36054)
					insValLocksAcc = False
				End If
				
				.RCloseRec()
			Else
				Call lerrTime.ErrorMessage(lstrCodispl, 1026)
				insValLocksAcc = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecLedger_acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLedger_acc = Nothing
		
insValLocksAcc_Err: 
		If Err.Number Then
			insValLocksAcc = False
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insValMCP001: Page Detail validation routine.
	'%   insValMCP001: Rutina de validación del detalle de la ventana.
	Public Function insValMCP001(ByVal nLed_compan As Integer, ByVal nDebit As Integer, ByVal nCredit As Integer, ByVal nParameter As Integer, ByVal nComplement As Integer, ByVal sAccount As String, ByVal sCodispl As String, ByVal nGroup As Integer, ByVal sAction As String, ByVal nArea_Led As Integer, ByVal nTransac_Ty As Integer, ByVal nTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nConsec As Integer) As String
		
		Dim lintConsec As Integer
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMCP001_Err
		
		lclsErrors = New eFunctions.Errors
		
		lintConsec = nConsec
		insValMCP001 = String.Empty
		
		'+   Validación de los campos débito y crédito
		If (nDebit = 0 Or nDebit = eRemoteDB.Constants.intNull) And (nCredit = 0 Or nCredit = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 36208)
		Else
			If nDebit = 1 Then
				nLine_Type = 1
			Else
				nLine_Type = 2
			End If
		End If
		
		'+   Validación del campo complemento
		If nComplement = 0 Or nComplement = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36209)
		End If
		
		'+   Validación del campo parámetro
		If nParameter = 0 Or nParameter = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 36214)
		End If
		
		'+   Validación del campo cuenta
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Trim(sAccount) = String.Empty Or IsNothing(sAccount) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1027)
		Else
			
			'+ Se valida que la cuenta no tenga bloqueados los débitos y créditos
			Call insValLocksAcc(lclsErrors, sCodispl, nLed_compan, sAccount, nDebit, nCredit)
		End If
		
		'+ Se valida la línea de detalle no debe estar repetida en la ventana.
		If Validate(nArea_Led, nTransac_Ty, nTratypei, nReceipt_ty, nProduct_ty, sPay_type, nTyp_acco, nLed_compan, nComplement, nParameter, nConsec, sAccount) Then
			If sAction = "Add" And sExist = CDbl("1") Then
				Call lclsErrors.ErrorMessage(sCodispl, 55864)
			End If
			If sAction = "Update" And sExist = CDbl("1") And Me.nConsec <> lintConsec Then
				Call lclsErrors.ErrorMessage(sCodispl, 55864)
			End If
		End If
		
		insValMCP001 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
insValMCP001_Err: 
		If Err.Number Then
			insValMCP001 = insValMCP001 & Err.Description
		End If
		
		On Error GoTo 0
	End Function
	
	'% Validate: Verifica que exista la guia contable
	Public Function Validate(ByVal nArea As Integer, ByVal nTransac_Ty As Integer, ByVal nTratypei As Integer, ByVal nReceipt_ty As Integer, ByVal nProduct_ty As Integer, ByVal sPay_type As String, ByVal nTyp_acco As Integer, ByVal nLed_compan As Integer, ByVal nComplement As Integer, ByVal nParameter As Integer, ByVal nConsec As Integer, ByVal sAccount As String) As Boolean
		
		'**- Define the variable lrecDet_lines
		'-   Se define la variable lrecDet_lines
		
		Dim lstrPay_type As String
		Dim lrecDet_lines As eRemoteDB.Execute
		
		On Error GoTo Validate_Err
		
		lrecDet_lines = New eRemoteDB.Execute
		
		Validate = False
		
		If sPay_type = String.Empty Then
			lstrPay_type = "0"
		Else
			lstrPay_type = sPay_type
		End If
		
		'**+ Parameters definition for the stored procedure 'insudb.reaDet_lines'
		'+   Definicion de parametros para stored procedure 'insudb.reaDet_lines'
		
		With lrecDet_lines
			.StoredProcedure = "reaDet_lines_v"
			
			.Parameters.Add("nAreaLed", nArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac_ty", nTransac_Ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypei", nTratypei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt_ty", nReceipt_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ty", nProduct_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPay_type", lstrPay_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_Acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLed_compan", nLed_compan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParameter", nParameter, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComplement", nComplement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.sExist = .FieldToClass("sExist")
				Me.nConsec = .FieldToClass("nConsec")
				Validate = True
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecDet_lines may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDet_lines = Nothing
		
Validate_Err: 
		If Err.Number Then
			Validate = False
		End If
		
		On Error GoTo 0
	End Function
End Class






