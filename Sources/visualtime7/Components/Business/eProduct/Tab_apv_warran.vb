Option Strict Off
Option Explicit On
Public Class Tab_apv_warran
	
	'- Estructura de tabla Tab_apv_warran al 22-08-2003
	'-     Property                    Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nTable As Integer ' NUMBER     22   0     5    N
	Public nMin_year As Integer ' NUMBER     22   0     5    N
	Public nMax_year As Integer ' NUMBER     22   0     5    N
	Public nRate As Double ' NUMBER     22   2     10   N
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	'-Propiedades auxiliares
	Public sTable_desc As String
	Private nZone As Short
	Private nAction As Short
	Private bMin_year As Boolean
	Private bMax_year As Boolean
	
	
	
	'%Find: Lectura de la tabla de intereses garantizados para APV
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer, ByVal nMin_year As Integer) As Boolean
		
		Dim lrecreatab_apv_warran As eRemoteDB.Execute
		
		On Error GoTo reatab_apv_warran_Err
		
		lrecreatab_apv_warran = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'reatab_apv_warran'
		'**+ The Information was read on  22/08/2003
		
		'+ Definición de parámetros para stored procedure 'reatab_apv_warran'
		'+ Información leída el: 22/08/2003
		
		With lrecreatab_apv_warran
			.StoredProcedure = "reatab_apv_warran"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_year", nMin_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nTable = nTable
				Me.nMin_year = .FieldToClass("nMin_year")
				nMax_year = .FieldToClass("nMax_year")
				nRate = .FieldToClass("nRate")
				Find = True
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreatab_apv_warran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_apv_warran = Nothing
		
		Exit Function
reatab_apv_warran_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreatab_apv_warran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_apv_warran = Nothing
		On Error GoTo 0
	End Function
	
	'%Update: Actualización de la tabla de intereses garantizados para APV
	Public Function Update() As Boolean
		Dim lrecinstab_apv_warran As eRemoteDB.Execute
		
		On Error GoTo instab_apv_warran_Err
		
		lrecinstab_apv_warran = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'instab_apv_warran'
		'**+ The Information was read on  01/09/2003
		
		'+ Definición de parámetros para stored procedure 'instab_apv_warran'
		'+ Información leída el: 01/09/2003
		
		With lrecinstab_apv_warran
			.StoredProcedure = "instab_apv_warran"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_year", nMin_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_year", nMax_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTable_desc", sTable_desc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZone", nZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
instab_apv_warran_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecinstab_apv_warran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinstab_apv_warran = Nothing
		On Error GoTo 0
	End Function
	
	'%insValMDP7001_K: Actualización de la tabla de intereses garantizados para APV
	Public Function insValMDP7001_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer, ByVal sTable_desc As String, ByVal nMainAction As Short) As String
		
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProduct_li As Product
		On Error GoTo insValMDP7001_K_Err
		
		lobjErrors = New eFunctions.Errors
		lclsProduct_li = New Product
		
		With lobjErrors
			If nBranch <= 0 Then
				.ErrorMessage(sCodispl, 1022)
			End If
			
			If nProduct <= 0 Then
				lobjErrors.ErrorMessage(sCodispl, 1014)
			End If
			
			If nBranch > 0 And nProduct > 0 Then
				lclsProduct_li.FindProduct_li(nBranch, nProduct, Today)
				If lclsProduct_li.nProdClas <> 4 Then
					lobjErrors.ErrorMessage(sCodispl, 767098)
				End If
			End If
			
			If nTable <= 0 Then
				lobjErrors.ErrorMessage(sCodispl, 70159)
				
			Else
				If Not Find_Master(nBranch, nProduct, nTable) Then
					If sTable_desc = String.Empty Then
						lobjErrors.ErrorMessage(sCodispl, 3872)
					End If
				End If
			End If
			
			insValMDP7001_K = .Confirm
		End With
		
insValMDP7001_K_Err: 
		If Err.Number Then
			insValMDP7001_K = "insValMDP7001_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct_li may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct_li = Nothing
	End Function
	
	'%insValMDP7001: Actualización de la tabla de intereses garantizados para APV
	Public Function insValMDP7001(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer, ByVal nMin_year As Integer, ByVal nMax_year As Integer, ByVal nMax_year_Aux As Integer, ByVal nRate As Double, ByVal sAction As String) As String
		
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValMDP7001_Err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			If nRate <= 0 Then
				lobjErrors.ErrorMessage(sCodispl, 70160)
			End If
			
			If nMin_year <= 0 Then
				lobjErrors.ErrorMessage(sCodispl, 70165)
			End If
			
			If nMax_year <= 0 Then
				lobjErrors.ErrorMessage(sCodispl, 70168)
			End If
			
			If nMin_year > nMax_year Then
				lobjErrors.ErrorMessage(sCodispl, 70169)
			End If
			
			Call Val_Range(nBranch, nProduct, nTable, nMin_year, nMax_year)
			If sAction <> "Update" Then
				If bMin_year Then
					lobjErrors.ErrorMessage(sCodispl, 70167)
				End If
			End If
			
			If nMax_year <> nMax_year_Aux Then
				If bMax_year Then
					lobjErrors.ErrorMessage(sCodispl, 70158)
				End If
			End If
			
			insValMDP7001 = lobjErrors.Confirm
		End With
		
insValMDP7001_Err: 
		If Err.Number Then
			insValMDP7001 = "insValMDP7001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	
	'%insPostMDP7001_K: Actualización de la tabla de intereses garantizados para APV
	Public Function insPostMDP7001_K(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer, ByVal sTable_desc As String, ByVal nUsercode As Integer, ByVal nMainAction As Short) As Boolean
		
		On Error GoTo insPostMDP7001_K_Err
		
		If nMainAction = 301 Then
			nAction = 1
		ElseIf nMainAction = 302 Then 
			nAction = 2
		ElseIf nMainAction = 303 Then 
			nAction = 3
		End If
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nTable = nTable
			.sTable_desc = sTable_desc
			.nUsercode = nUsercode
			nZone = 1
		End With
		
		insPostMDP7001_K = Update
		
insPostMDP7001_K_Err: 
		If Err.Number Then
			insPostMDP7001_K = False
		End If
		On Error GoTo 0
	End Function
	
	'%insPostMDP7001: Actualización de la tabla de intereses garantizados para APV
	Public Function insPostMDP7001(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer, ByVal nMin_year As Integer, ByVal nMax_year As Integer, ByVal nRate As Double, ByVal nUsercode As Integer, ByVal sAction As String) As Boolean
		
		On Error GoTo insPostMDP7001_Err
		
		If sAction = "Add" Then
			nAction = 1
		ElseIf sAction = "Update" Then 
			nAction = 2
		Else
			nAction = 3
		End If
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.nTable = nTable
			.nMin_year = nMin_year
			.nMax_year = nMax_year
			.nRate = nRate
			.nUsercode = nUsercode
			nZone = 2
		End With
		
		insPostMDP7001 = Update
		
insPostMDP7001_Err: 
		If Err.Number Then
			insPostMDP7001 = False
		End If
		On Error GoTo 0
	End Function
	
	'%Val_Range: Validacion del rango de los años de vigencia
	Private Sub Val_Range(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer, ByVal nMin_year As Integer, ByVal nMax_year As Integer)
		Dim lrecval_t_apv_w_range As eRemoteDB.Execute
		On Error GoTo val_t_apv_w_range_Err
		
		lrecval_t_apv_w_range = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'val_t_apv_w_range'
		'**+ The Information was read on  01/09/2003
		
		'+ Definición de parámetros para stored procedure 'val_t_apv_w_range'
		'+ Información leída el: 01/09/2003
		
		With lrecval_t_apv_w_range
			.StoredProcedure = "val_t_apv_w_range"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_year", nMin_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_year", nMax_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_year_out", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_year_out", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				bMin_year = IIf(.Parameters.Item("nMin_year_out").Value = 1, True, False)
				bMax_year = IIf(.Parameters.Item("nMax_year_out").Value = 1, True, False)
			End If
		End With
		
val_t_apv_w_range_Err: 
		'UPGRADE_NOTE: Object lrecval_t_apv_w_range may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecval_t_apv_w_range = Nothing
		On Error GoTo 0
	End Sub
	
	'%Find: Lectura de la tabla de intereses garantizados para APV
	Public Function Find_Master(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTable As Integer) As Boolean
		
		Dim lrecreatab_apv_warran As eRemoteDB.Execute
		
		On Error GoTo reatab_apv_warran_Err
		
		lrecreatab_apv_warran = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'reatab_apv_warran'
		'**+ The Information was read on  22/08/2003
		
		'+ Definición de parámetros para stored procedure 'reatab_apv_warran'
		'+ Información leída el: 22/08/2003
		
		With lrecreatab_apv_warran
			.StoredProcedure = "reatab_apv_warran_m"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTable", nTable, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nTable = nTable
				sTable_desc = .FieldToClass("sTable_desc")
				Find_Master = True
				.RCloseRec()
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreatab_apv_warran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_apv_warran = Nothing
		
		Exit Function
reatab_apv_warran_Err: 
		If Err.Number Then
			Find_Master = False
		End If
		'UPGRADE_NOTE: Object lrecreatab_apv_warran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreatab_apv_warran = Nothing
		On Error GoTo 0
	End Function
End Class






