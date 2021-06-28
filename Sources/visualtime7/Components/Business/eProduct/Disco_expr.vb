Option Strict Off
Option Explicit On
Public Class Disco_expr
	'%-------------------------------------------------------%'
	'% $Workfile:: Disco_expr.cls                           $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 12/09/03 15.45                               $%'
	'% $Revision:: 36                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla DISCO_EXPR al 05-31-2002 18:21:04
	'-       Property                Type          DBType    Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nDisexprc As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nBranch_rei As Integer ' NUMBER     22   0     5    S
	Public nBill_item As Integer ' NUMBER     22   0     5    S
	Public sChanallo As String ' CHAR       1    0     0    S
	Public nBranch_est As Integer ' NUMBER     22   0     5    S
	Public sCommissi_i As String ' CHAR       1    0     0    S
	Public nBranch_led As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public sDefaulti As String ' CHAR       1    0     0    S
	Public sDescript As String ' CHAR       30   0     0    S
	Public sDevoallo As String ' CHAR       1    0     0    S
	Public nDisexmax As Double ' NUMBER     22   2     8    S
	Public nDisexmin As Double ' NUMBER     22   2     8    S
	Public nDisexpra As Double ' NUMBER     22   2     8    S
	Public sEdperapl As String ' CHAR       1    0     0    S
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nOrder_apl As Integer ' NUMBER     22   0     5    S
	Public sProrate As String ' CHAR       1    0     0    S
	Public sRequire As String ' CHAR       1    0     0    S
	Public sRoutine As String ' CHAR       12   0     0    S
	Public sShort_des As String ' CHAR       12   0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nDisexAddper As Double ' NUMBER     22   2     6    S
	Public nDisexSubper As Double ' NUMBER     22   2     6    S
	Public nAmelevel As Integer ' NUMBER     22   0     5    S
	Public sDisexpri As String ' CHAR       1    0     0    S
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nNotenum As Integer ' NUMBER     22   0     10   S
	Public sDefpol As String ' CHAR       1    0     0    S
	Public nRate As Double ' NUMBER     22   2     5    S
	Public sTypmar As String ' CHAR       1    0     0    S
	Public sIva As String ' CHAR       1    0     0    S
	Public nAply As Integer ' NUMBER     22   0     5    S
	
	'+  Variable que tiene la descripción de la nota
	Public sDescriptNotes As String
	
	'% Función Update: Realiza la actualización o creación de recargos/descuentos en el producto
	Public Function Update() As Boolean
		Dim lupdDisco_expr As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lupdDisco_expr = New eRemoteDB.Execute
		
		With lupdDisco_expr
			.StoredProcedure = "insDisco_expr"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder_apl", nOrder_apl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDisexpri", sDisexpri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChanallo", sChanallo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDevoallo", sDevoallo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProrate", sProrate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisexAddper", nDisexAddper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisexSubper", nDisexSubper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmelevel", nAmelevel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCommissi_i", sCommissi_i, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexmax", nDisexmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexmin", nDisexmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexpra", nDisexpra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sEdperapl", sEdperapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefpol", sDefpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypMar", sTypmar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIVA", sIva, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAply", nAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lupdDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lupdDisco_expr = Nothing
	End Function
	
	'% Delete: Elimina el recargo/descuento/impuesto del producto
	Public Function Delete() As Boolean
		Dim lclsDisco_expr As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lclsDisco_expr = New eRemoteDB.Execute
		
		With lclsDisco_expr
			.StoredProcedure = "insdelDisco_expr"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
	End Function
	
	'% Find: Realiza la búsqueda de los recargos/descuentos asociados al producto en tratamiento
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecDisco_expr As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nDisexprc <> Me.nDisexprc Or dEffecdate <> Me.dEffecdate Or bFind Then
			
			lrecDisco_expr = New eRemoteDB.Execute
			
			With lrecDisco_expr
				.StoredProcedure = "reaDisco_expr"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDisexprc", nDisexprc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nDisexprc = .FieldToClass("nDisexprc")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					nBill_item = .FieldToClass("nBill_item")
					nBranch_rei = .FieldToClass("nBranch_rei")
					sChanallo = .FieldToClass("sChanallo")
					nBranch_est = .FieldToClass("nBranch_est")
					sCommissi_i = .FieldToClass("sCommissi_i")
					nBranch_led = .FieldToClass("nBranch_led")
					nCurrency = .FieldToClass("nCurrency")
					sDefaulti = .FieldToClass("sDefaulti")
					sDescript = .FieldToClass("sDescript")
					sDevoallo = .FieldToClass("sDevoallo")
					sEdperapl = .FieldToClass("sEdperapl")
					dNulldate = .FieldToClass("dNulldate")
					nOrder_apl = .FieldToClass("nOrder_apl")
					sProrate = .FieldToClass("sProrate")
					sRequire = .FieldToClass("sRequire")
					sRoutine = .FieldToClass("sRoutine")
					sShort_des = .FieldToClass("sShort_des")
					nAmelevel = .FieldToClass("nAmelevel")
					sDisexpri = .FieldToClass("sDisexpri")
					sStatregt = .FieldToClass("sStatregt")
					nNotenum = .FieldToClass("nNotenum", eRemoteDB.Constants.intNull)
					sDefpol = .FieldToClass("sDefpol")
					nDisexpra = .FieldToClass("nDisexpra2", eRemoteDB.Constants.intNull)
					nDisexmin = .FieldToClass("nDisexmin2", eRemoteDB.Constants.intNull)
					nDisexmax = .FieldToClass("nDisexmax2", eRemoteDB.Constants.intNull)
					nDisexAddper = .FieldToClass("nDisexAddper2", eRemoteDB.Constants.intNull)
					nDisexSubper = .FieldToClass("nDisexSubper2", eRemoteDB.Constants.intNull)
					nRate = .FieldToClass("nRate", eRemoteDB.Constants.intNull)
					sTypmar = .FieldToClass("sTypMar", "2")
					sIva = .FieldToClass("sIVA", "2")
					nAply = .FieldToClass("nAply")
					Find = True
					
					.RCloseRec()
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDisco_expr = Nothing
	End Function
	
	'% Find_count: Verifica si el ramo-producto en tratamiento tiene recargos/descuentos
	Public Function Find_count(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecDisco_expr As eRemoteDB.Execute
		
		On Error GoTo Find_count_Err
		lrecDisco_expr = New eRemoteDB.Execute
		
		With lrecDisco_expr
			.StoredProcedure = "reaDisco_expr_count"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If .FieldToClass("Discx_count") > 0 Then
					Find_count = True
				Else
					Find_count = False
				End If
				.RCloseRec()
			Else
				Find_count = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDisco_expr = Nothing
		
Find_count_Err: 
		If Err.Number Then
			Find_count = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostDP008: Realiza el mantenimiento de la historia en la tabla
	Public Function insPostDP008(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nExist As Integer, ByVal nDisexprc As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal sShort_des As String, ByVal nOrder_apl As Integer, ByVal sDisexpri As String, ByVal sStatregt As String) As Boolean
		Dim lcolDisco_exprs As eProduct.Disco_exprs
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP008_Err
		
		lcolDisco_exprs = New eProduct.Disco_exprs
		
		If sAction = "Del" Then
			'+Se elimina el recargo/descuento
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.nDisexprc = nDisexprc
				.dEffecdate = dEffecdate
				.nUsercode = nUsercode
				insPostDP008 = Delete
			End With
		Else
			'+ Si se está insertando el recargo/descuento, se inicializan los valores
			'+ de los campos que no se llenan en la forma DP008
			If nExist = 0 Then
				If nDisexprc = 0 Or nDisexprc = eRemoteDB.Constants.intNull Then
					nDisexprc = lcolDisco_exprs.MakeDiscNumber(nBranch, nProduct, dEffecdate)
				End If
				With Me
					.dEffecdate = dEffecdate
					.nProduct = nProduct
					.nBranch = nBranch
					.nDisexprc = nDisexprc
					.sDescript = sDescript
					.nOrder_apl = nOrder_apl
					.sShort_des = sShort_des
					.sDisexpri = sDisexpri
					.sStatregt = sStatregt
					.nBranch_est = eRemoteDB.Constants.intNull
					.nBranch_led = eRemoteDB.Constants.intNull
					.nBill_item = eRemoteDB.Constants.intNull
					.nBranch_rei = eRemoteDB.Constants.intNull
					.sChanallo = String.Empty
					.sDefaulti = String.Empty
					.sDevoallo = String.Empty
					.sProrate = String.Empty
					.sRequire = String.Empty
					.nDisexAddper = eRemoteDB.Constants.intNull
					.nDisexSubper = eRemoteDB.Constants.intNull
					.nAmelevel = eRemoteDB.Constants.intNull
					.nNotenum = eRemoteDB.Constants.intNull
					.sCommissi_i = String.Empty
					.nCurrency = eRemoteDB.Constants.intNull
					.nDisexmax = eRemoteDB.Constants.intNull
					.nDisexmin = eRemoteDB.Constants.intNull
					.nDisexpra = eRemoteDB.Constants.intNull
					.sEdperapl = String.Empty
					.sRoutine = String.Empty
					.nUsercode = nUsercode
					insPostDP008 = Update
				End With
			Else
				If Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
					With Me
						.dEffecdate = dEffecdate
						.nProduct = nProduct
						.nBranch = nBranch
						.nDisexprc = nDisexprc
						.sDescript = sDescript
						.nOrder_apl = nOrder_apl
						.sShort_des = sShort_des
						.sDisexpri = sDisexpri
						.sStatregt = sStatregt
						.nUsercode = nUsercode
						insPostDP008 = Update
					End With
				End If
			End If
		End If
		If insPostDP008 Then
			lclsProd_win = New eProduct.Prod_win
			If Find(nBranch, nProduct, eRemoteDB.Constants.intNull, dEffecdate) Then
				'+ Si existen registros se actualiza la secuencia de ventana del producto como 'con contenido'
				insPostDP008 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP008", "2", nUsercode)
			Else
				'+ Si no existen registros se actualiza la secuencia de ventana del producto como 'sin contenido'
				insPostDP008 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP008", "1", nUsercode)
			End If
		End If
		
insPostDP008_Err: 
		If Err.Number Then
			insPostDP008 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lcolDisco_exprs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolDisco_exprs = Nothing
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	'% insPostDP08B1: se actualizan los datos asociados al Recargo/Descuento/Impuesto
	Public Function insPostDP08B1(ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal nDisexprc As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nBranch_est As Integer, ByVal nBranch_led As Integer, ByVal nBranch_rei As Integer, ByVal nBill_item As Integer, ByVal nCapitalAdd As Integer, ByVal nCapitalSub As Integer, ByVal sDefaulti As String, ByVal sDevoallo As String, ByVal sProrate As String, ByVal sRequire As String, ByVal nDisexAddper As Double, ByVal nDisexSubper As Double, ByVal nAmelevel As Integer, ByVal nNotenum As Integer, ByVal sTypmar As String, ByVal sIva As String, ByVal nAply As Integer) As Boolean
		Dim lclsDisco_expr As eProduct.Disco_expr
		
		On Error GoTo insPostDP08B1_Err
		
		lclsDisco_expr = New eProduct.Disco_expr
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			With lclsDisco_expr
				If .Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
					.nBranch_est = nBranch_est
					.nBranch_led = nBranch_led
					.nBill_item = nBill_item
					.nBranch_rei = nBranch_rei
					If nCapitalAdd = 1 And nCapitalSub = 1 Then
						.sChanallo = "3"
					ElseIf nCapitalAdd = 1 Then 
						.sChanallo = "1"
					ElseIf nCapitalSub = 1 Then 
						.sChanallo = "2"
					Else
						.sChanallo = "0"
					End If
					.sDefaulti = IIf(sDefaulti = String.Empty, "2", sDefaulti)
					.sDevoallo = IIf(sDevoallo = String.Empty, "2", sDevoallo)
					.sProrate = IIf(sProrate = String.Empty, "2", sProrate)
					.sRequire = IIf(sRequire = String.Empty, "2", sRequire)
					.nDisexAddper = nDisexAddper
					.nDisexSubper = nDisexSubper
					.nAmelevel = nAmelevel
					.nNotenum = nNotenum
					.nUsercode = nUsercode
					.sTypmar = IIf(sTypmar = String.Empty, "2", sTypmar)
					.sIva = IIf(sIva = String.Empty, "2", sIva)
					.nAply = nAply
					insPostDP08B1 = .Update
				End If
			End With
		Else
			insPostDP08B1 = True
		End If
		
insPostDP08B1_Err: 
		If Err.Number Then
			insPostDP08B1 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
	End Function
	
	'% insPostDP08B1_K: Actualiza el estado del recargo/descuento
	Public Function insPostDP08B1_K(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostDP08B1_K_Err
		
		If Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
			sStatregt = CStr(Product.pmStatregt.pmActivo)
			Me.nUsercode = nUsercode
			insPostDP08B1_K = Update
		End If
		
insPostDP08B1_K_Err: 
		If Err.Number Then
			insPostDP08B1_K = False
		End If
		On Error GoTo 0
	End Function
	
	'%insPostDP08B2: Realiza el mantenimiento de la historia en la estructura 'Disco_expr'.
	Public Function insPostDP08B2(ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, ByVal sCommissi_i As String, ByVal nCurrency As Integer, ByVal nDisexmax As Double, ByVal nDisexmin As Double, ByVal nDisexpra As Double, ByVal sEdperapl As String, ByVal sRoutine As String, ByVal sDefpol As String, ByVal nRate As Double) As Boolean
		Dim lclsDisco_expr As eProduct.Disco_expr
		
		On Error GoTo insPostDP08B2_Err
		lclsDisco_expr = New eProduct.Disco_expr
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			With lclsDisco_expr
				Call .Find(nBranch, nProduct, nDisexprc, dEffecdate)
				If Trim(sCommissi_i) = String.Empty Or Trim(sCommissi_i) = "0" Then
					.sCommissi_i = "2"
				Else
					.sCommissi_i = sCommissi_i
				End If
				.sEdperapl = sEdperapl
				.nCurrency = nCurrency
				.nDisexmax = nDisexmax
				.nDisexmin = nDisexmin
				.nDisexpra = nDisexpra
				.sRoutine = sRoutine
				.nUsercode = nUsercode
				.sDefpol = sDefpol
				.nRate = nRate
				insPostDP08B2 = .Update
			End With
		Else
			insPostDP08B2 = True
		End If
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
		
insPostDP08B2_Err: 
		If Err.Number Then
			insPostDP08B2 = False
		End If
		On Error GoTo 0
	End Function
	
	'% insPostDP08B2Upd: Actualiza las condiciones de aplicación de recargos/descuentos
	'% en las tablas "dsex_condi" y "disco_expr"
	Public Function insPostDP08B2Upd(ByVal sAction As String, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, ByVal nApplication As Integer, ByVal nCode As Integer, ByVal nPercent As Double, ByVal nCurrency As Integer, ByVal nPreFix As Double, ByVal nPreMax As Double, ByVal nPreMin As Double, ByVal sPreRoutine As String, ByVal sOptionCapital As String, ByVal sPreComm As String, ByVal sDefpol As String, ByVal nModulec As Integer, ByVal nRate As Double, ByVal nRole As Integer, ByVal sBrancht As String) As Boolean
		Dim lclsDsex_condi As eProduct.Dsex_condi
		Dim lclsDisco_expr As eProduct.Disco_expr
		
		On Error GoTo insPostDP08B2Upd_Err
		
		lclsDsex_condi = New eProduct.Dsex_condi
		lclsDisco_expr = New eProduct.Disco_expr
		
		With lclsDsex_condi
			.nBranch = nBranch
			.nProduct = nProduct
			.nDisexprc = nDisexprc
			.nAplication = nApplication
			.nCode = nCode
			.dEffecdate = dEffecdate
			.nUsercode = nUsercode
			If nModulec = eRemoteDB.Constants.intNull Then
				.nModulec = 0
			Else
				.nModulec = nModulec
			End If
			'+ Si el producto es de vida
			If sBrancht = "1" Then
				'+ Si el tipo de elemento es cobertura se asocia el rol sino se deja en 0
				If nApplication = 1 Then
					.nRole = nRole
				Else
					.nRole = 0
				End If
			Else
				'+ Si el producto no es de vida se deja el rol en 2
				.nRole = 2
			End If
		End With
		
		If sAction = "Upd" Then
			With lclsDsex_condi
				.dNulldate = eRemoteDB.Constants.dtmNull
				.nRate = nPercent
				insPostDP08B2Upd = .Update
			End With
		Else
			insPostDP08B2Upd = lclsDsex_condi.Delete
		End If
		
		'UPGRADE_NOTE: Object lclsDsex_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDsex_condi = Nothing
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
		
insPostDP08B2Upd_Err: 
		If Err.Number Then
			insPostDP08B2Upd = False
		End If
		On Error GoTo 0
		
	End Function
	
	'insValDP008Upd: Función que valida los datos suministrados en la forma DP008
	Public Function insValDP008Upd(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nDisexprc As Integer, ByVal sDisexpri As String, ByVal sDescript As String, ByVal sShort_des As String, ByVal nOrder_apl As Integer, ByVal sStatregt As String, ByVal sOldStatregt As String, ByVal nExist As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsDisco_expr As eProduct.Disco_expr
		
		lclsErrors = New eFunctions.Errors
		lclsDisco_expr = New eProduct.Disco_expr
		
		On Error GoTo insValDP008Upd_Err
		
		If nDisexprc = eRemoteDB.Constants.intNull And (Trim(sDisexpri) <> String.Empty Or Trim(sDescript) <> String.Empty Or Trim(sShort_des) <> String.Empty Or nOrder_apl <> eRemoteDB.Constants.intNull Or Trim(sStatregt) <> String.Empty) Then
			Call lclsErrors.ErrorMessage("DP008", 1084)
		End If
		
		'+ Validación de código de recargo/descuento, si se está registrando
		If nExist = 0 Then
			If nDisexprc <> eRemoteDB.Constants.intNull Then
				If lclsDisco_expr.Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
					Call lclsErrors.ErrorMessage("DP008", 11143)
				End If
			End If
		End If
		
		If nDisexprc <> eRemoteDB.Constants.intNull Then
			
			'+ Validacion del tipo
            If String.IsNullOrEmpty(sDisexpri) Or CDbl(sDisexpri) <= 0 Then
                Call lclsErrors.ErrorMessage("DP008", 11334)
            End If
			
			'+ Validacion de la descripcion
            If String.IsNullOrEmpty(sDescript) Then
                Call lclsErrors.ErrorMessage("DP008", 11299)
            End If
			
			
			'+ Validacion de la descripcion corta
            If String.IsNullOrEmpty(sShort_des) Then
                Call lclsErrors.ErrorMessage("DP008", 11300)
            End If
			
			'+ Validacion del orden
			If nOrder_apl = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage("DP008", 11194)
			Else
				If insValOrderApl(nBranch, nProduct, dEffecdate, nDisexprc, nOrder_apl) Then
					Call lclsErrors.ErrorMessage("DP008", 11195)
				End If
			End If
			
            If String.IsNullOrEmpty(sStatregt) Then
                Call lclsErrors.ErrorMessage("DP008", 11301)
            Else
                If sStatregt < "1" Or sStatregt > "3" Then
                    Call lclsErrors.ErrorMessage("DP008", 11218)
                Else
                    If sOldStatregt.Trim <> String.Empty And sOldStatregt.Trim <> "2" And sStatregt = "2" Then
                        Call lclsErrors.ErrorMessage("DP008", 11218)
                    End If
                End If
            End If
		End If
		
		insValDP008Upd = lclsErrors.Confirm
        lclsErrors = Nothing
        lclsDisco_expr = Nothing
		
insValDP008Upd_Err: 
		If Err.Number Then
			insValDP008Upd = "insValDP008Upd: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%insValDP008: Esta funcion se encarga de realizar las validaciones de la ventana de
	'%             recargos
	Public Function insValDP008(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsDisco_expr As eProduct.Disco_expr
		
		On Error GoTo insValDP008_Err
		
		lerrTime = New eFunctions.Errors
		lclsDisco_expr = New eProduct.Disco_expr
		
		If Not lclsDisco_expr.Find_count(nBranch, nProduct, dEffecdate) Then
			Call lerrTime.ErrorMessage("DP008", 1924)
		End If
		
		insValDP008 = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
		
insValDP008_Err: 
		If Err.Number Then
			insValDP008 = "insValDP008: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'% insValDP08B1: Rutina para las validaciones de la transacción DP08B1.
	Public Function insValDP08B1(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nDisexprc As Integer, ByVal nBill_item As Integer, ByVal nBranch_led As Integer, ByVal nBranch_est As Integer, ByVal nCapitalSub As Integer, ByVal nDisexSubper As Double, ByVal sTypmar As String, ByVal sIva As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTime As eFunctions.valField
		
		On Error GoTo insValDP08B1_Err
		
		lclsErrors = New eFunctions.Errors
		
		If nBill_item = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("DP08B1", 11308)
		End If
		
		If nBranch_led = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("DP08B1", 11309)
		End If
		
		If nBranch_est = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage("DP08B1", 11320)
		End If
		
		If nCapitalSub <> eRemoteDB.Constants.intNull And nCapitalSub = 1 Then
			lclsValTime = New eFunctions.valField
			lclsValTime.objErr = lclsErrors
			If lclsValTime.ValNumber(nDisexSubper,  , eFunctions.valField.eTypeValField.onlyvalid) Then
				If nDisexSubper > 100 Then
					Call lclsErrors.ErrorMessage("DP08B1", 9991)
				End If
			End If
		End If
		
		If sTypmar <> String.Empty Then
			'+ El recargo seleccionado como Margen de utilidad debe ser único
			If Find_typemar(nBranch, nProduct, dEffecdate) Then
				If Me.nDisexprc <> nDisexprc Then
					Call lclsErrors.ErrorMessage("DP08B1", 55818)
				End If
			End If
		End If
		
		If sIva <> String.Empty Then
			'+ El recargo seleccionado como IVA debe ser único
			If Find_IVA(nBranch, nProduct, dEffecdate) Then
				If Me.nDisexprc <> nDisexprc Then
					Call lclsErrors.ErrorMessage("DP08B1", 55819)
				End If
			End If
		End If
		
		insValDP08B1 = lclsErrors.Confirm
		
insValDP08B1_Err: 
		If Err.Number Then
			insValDP08B1 = "insValDP08B1: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTime = Nothing
	End Function
	
	'% insvalDP08B2: valida la información incluída en el frame dp08b2
	Public Function insValDP08B2(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date, ByVal nDisXpreFix As Double, ByVal nCurrency As Integer, ByVal nDisXpreMin As Double, ByVal nDisXpreMax As Double, ByVal sDisxPreRou As String, ByVal sDefpol As String, ByVal nModulec As Integer, ByVal sRate As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsDsex_condi As eProduct.Dsex_condi
		Dim lblnError As Boolean
		
		On Error GoTo insValDP08B2_Err
		lblnError = False
		lclsErrors = New eFunctions.Errors
		lclsDsex_condi = New eProduct.Dsex_condi
		
		'+ Se valida la moneda
		If ((nDisXpreFix <> 0 And nDisXpreFix <> eRemoteDB.Constants.intNull) Or (nDisXpreMax <> 0 And nDisXpreMax <> eRemoteDB.Constants.intNull) Or (nDisXpreMin <> 0 And nDisXpreMin <> eRemoteDB.Constants.intNull)) And (nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0) Then
			Call lclsErrors.ErrorMessage("DP08B2", 1351)
			lblnError = True
		End If
		
		'+ Se valida la prima máxima y la prima mínima
		If nDisXpreMin <> 0 And nDisXpreMin <> eRemoteDB.Constants.intNull And nDisXpreMax <> 0 And nDisXpreMax <> eRemoteDB.Constants.intNull Then
			If nDisXpreMin > nDisXpreMax Then
				Call lclsErrors.ErrorMessage("DP08B2", 11133)
				lblnError = True
			End If
		End If
		
		'+ Se valida que se haya incluido una rutina de cálculo, un valor fijo o un porcentaje en algún elemento
		sRate = Replace(sRate, ",", String.Empty)
		sRate = Replace(sRate, "0", String.Empty)
		sRate = Trim(sRate)
		If Not lblnError Then
			If sDisxPreRou = String.Empty And (nDisXpreFix = 0 Or nDisXpreFix = eRemoteDB.Constants.intNull) And sRate = String.Empty Then
				Call lclsErrors.ErrorMessage("DP08B2", 11416)
			End If
		End If
		
		'+ Si las condiciones del recargo/descuento serán desde el producto, debe indicarse
		'+ por lo menos un elemento sobre quien aplique el recargo/descuento
		If sDefpol = "2" Then
			If nModulec = eRemoteDB.Constants.intNull Then
				nModulec = 0
			End If
			If Not lclsDsex_condi.Find_count(nBranch, nProduct, nDisexprc, dEffecdate, nModulec) Then
				Call lclsErrors.ErrorMessage("DP08B2", 11380)
			End If
		End If
		
		insValDP08B2 = lclsErrors.Confirm
		
insValDP08B2_Err: 
		If Err.Number Then
			insValDP08B2 = "insValDP08B2: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsDsex_condi may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDsex_condi = Nothing
	End Function
	
	'% insValDP08B2Upd: Realiza las validaciones de las condiciones a aplicar
	Public Function insValDP08B2Upd(ByVal sDisxPreRou As String, ByVal nDisXpreFix As Double, ByVal nCapitalAplied As Integer, ByVal sDescript As String, ByVal nPercent As Double, ByVal nModule As Integer, ByVal nAcceptModule As Integer, ByVal nRate As Double) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValTime As eFunctions.valField
		
		On Error GoTo insValDP08B2Upd_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValTime = New eFunctions.valField
		lclsValTime.objErr = lclsErrors
		
		'+Validacion del campo "Porcentaje"
		If (nPercent = 0 Or nPercent = eRemoteDB.Constants.intNull) Then
			If Trim(sDisxPreRou) = String.Empty And (nDisXpreFix = 0 Or nDisXpreFix = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage("DP08B2", 11416)
			End If
		End If
		
		'+ Si aplica sobre capital. Porcentaje
		If nCapitalAplied = CDbl("1") Then
			If nRate <= 0 Or nRate >= 1000 Then
				Call lclsErrors.ErrorMessage("DP08B2", 9993)
			End If
		Else
			'+ Si aplica sobre la prima. Porcentaje
			If nRate <= 0 Or nRate >= 100 Then
				Call lclsErrors.ErrorMessage("DP08B2", 9992)
			End If
		End If
		
		'+ Validación del módulo, si maneja módulos debe ser distinto de 0 o nulo
		If nAcceptModule <> 2 And (nModule = 0 Or nModule = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage("DP08B2", 3678)
		End If
		
		insValDP08B2Upd = lclsErrors.Confirm
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValTime = Nothing
		
insValDP08B2Upd_Err: 
		If Err.Number Then
			insValDP08B2Upd = "insValDP08B2Upd: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'%Función insValOrderApl: Verifica si el orden de aplicación se encuentra repetido
	Private Function insValOrderApl(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nDisexprc As Integer, ByVal nOrder_apl As Integer) As Boolean
		
		Dim lclsDisco_expr As eRemoteDB.Execute
		On Error GoTo insValOrderApl_Err
		
		insValOrderApl = False
		
		lclsDisco_expr = New eRemoteDB.Execute
		With lclsDisco_expr
			.StoredProcedure = "reaDisco_expr"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nDisexprc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					If .FieldToClass("nOrder_apl") <> eRemoteDB.Constants.intNull And .FieldToClass("nDisexprc") <> nDisexprc And .FieldToClass("nOrder_apl") = nOrder_apl Then
						insValOrderApl = True
						Exit Do
					End If
					.RNext()
				Loop 
				.RCloseRec()
			Else
				insValOrderApl = False
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
		
insValOrderApl_Err: 
		If Err.Number Then
			insValOrderApl = False
		End If
		On Error GoTo 0
	End Function
	
	'% LoadTabs: Esta función es la encarga de cargar la subsecuencia de recargos/descuentos
	Public Function LoadTabs(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date) As String
		'- Se definen los objetos usados para cargar la secuencia
		Dim lclsSequence As eFunctions.Sequence
		Dim lclsQuery As eRemoteDB.Query
		
		'- Se define el objeto usado para obtener los valores del recargo/descuento y asignar
		'- la imagen de obligatoriedad o ventana con contenido
		Dim lclsDisco_expr As eProduct.Disco_expr
		
		Dim lstrHTMLCode As String
		Dim lintImage As eFunctions.Sequence.etypeImageSequence
		Dim lstrCodispl As String
		Dim lintCount As Integer
		Dim lintAux As Integer
		Dim lintAction As Integer
		
		Dim lstrWindows As String
		
		On Error GoTo LoadTabs_Err
		
		lclsSequence = New eFunctions.Sequence
		lclsDisco_expr = New eProduct.Disco_expr
		lclsQuery = New eRemoteDB.Query
		
		lstrHTMLCode = lclsSequence.makeTable
		lstrWindows = "DP08B1  DP08B2  "
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		lintAux = 1
		
		With lclsDisco_expr
			If .Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
				For lintCount = 1 To 2
					'+ Se obtiene la información de la ventana a procesar
					lstrCodispl = Trim(Mid(lstrWindows, lintAux, 8))
					lintAux = lintAux + 8
					lintImage = eFunctions.Sequence.etypeImageSequence.eEmpty
					
					If lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'") Then
						Select Case lstrCodispl
							'+ Información general. Recargos/Descuentos
							Case "DP08B1"
								If .nBill_item <> eRemoteDB.Constants.intNull And .nBill_item <> 0 Then
									lintImage = eFunctions.Sequence.etypeImageSequence.eOK
								Else
									lintImage = eFunctions.Sequence.etypeImageSequence.eRequired
								End If
								
								'+ Condiciones de cálculo
							Case "DP08B2"
								If .sEdperapl <> String.Empty Then
									lintImage = eFunctions.Sequence.etypeImageSequence.eOK
								Else
									lintImage = eFunctions.Sequence.etypeImageSequence.eRequired
								End If
						End Select
						
						lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), lintImage)
					End If
				Next lintCount
			End If
		End With
		
		LoadTabs = lstrHTMLCode & lclsSequence.closeTable()
		
LoadTabs_Err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object lclsDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDisco_expr = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'% insValDP08B1_K: verifica si la información fue suministrada para el recargo/descuento
	Public Function insValDP08B1_K(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nDisexprc As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP08B1_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		If Find(nBranch, nProduct, nDisexprc, dEffecdate) Then
			If nBill_item = eRemoteDB.Constants.intNull Or nBill_item = 0 Or sEdperapl = String.Empty Then
				Call lclsErrors.ErrorMessage("DP08B1_K", 3902)
			End If
		End If
		
		insValDP08B1_K = lclsErrors.Confirm
		
insValDP08B1_K_Err: 
		If Err.Number Then
			insValDP08B1_K = "insValDP08B1_K: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% Find_typemar: se buscan los datos del recargo que representa el margen de utilidad
	Public Function Find_typemar(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		
		On Error GoTo Find_typemar_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDisco_expr_typmar'
		'+ Información leída el 06/03/2002
		
		With lclsExecute
			.StoredProcedure = "reaDisco_expr_typmar"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nDisexprc = .FieldToClass("nDisexprc")
				sChanallo = .FieldToClass("sChanallo")
				nDisexAddper = .FieldToClass("nDisexAddper")
				nDisexSubper = .FieldToClass("nDisexSubper")
				Find_typemar = True
			End If
		End With
		
Find_typemar_Err: 
		If Err.Number Then
			Find_typemar = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
	
	'% Find_IVA: se buscan los datos del recargo que representa el IVA
	Public Function Find_IVA(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsExecute As eRemoteDB.Execute
		
		On Error GoTo Find_IVA_Err
		
		lclsExecute = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaDisco_expr_IVA'
		'+ Información leída el 06/03/2002
		
		With lclsExecute
			.StoredProcedure = "reaDisco_expr_IVA"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nDisexprc = .FieldToClass("nDisexprc")
				Find_IVA = True
			End If
		End With
		
Find_IVA_Err: 
		If Err.Number Then
			Find_IVA = False
		End If
		'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsExecute = Nothing
	End Function
End Class






