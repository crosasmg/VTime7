Option Strict Off
Option Explicit On
Public Class Tab_reqexc
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_reqexc.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 21                                       $%'
    '%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema al 07/12/2000.
	'- Los campos llave de la tabla corresponden a: nBranch, nProduct, sType1, nCode1, sType2, nCode2 y dEffecdate.
	
	'   Column_name                   Type      Computed  Length  Prec  Scale Nullable  TrimTrailingBlanks  FixedLenNullInSource
	Public nBranch As Integer 'smallint     no        2      5     0     no            (n/a)               (n/a)
	Public nProduct As Integer 'smallint     no        2      5     0     no            (n/a)               (n/a)
	Public sType1 As String 'char         no        1                  no            no                  no
	Public nCode1 As Integer 'smallint     no        2      5     0     no            (n/a)               (n/a)
	Public nRole1 As Integer 'smallint     no        2      5     0     no            (n/a)               (n/a)
	Public sType2 As String 'char         no        1                  no            no                  no
	Public nCode2 As Integer 'smallint     no        2      5     0     no            (n/a)               (n/a)
	Public nRole2 As Integer 'smallint     no        2      5     0     no            (n/a)               (n/a)
	Public nDefReq As Integer
	Public dEffecdate As Date 'datetime     no        8                  no            (n/a)               (n/a)
	Public dNulldate As Date 'datetime     no        8                  yes           (n/a)               (n/a)
	Public sRelation As String 'char         no        1                  yes           no                  yes
	Public nusercode As Integer 'smallint     no        2      5     0     yes           (n/a)               (n/a)
	
	Public sDesReqExc1 As String
	Public sDesReqExc2 As String
	Public nModulec1 As Integer
	Public nModulec2 As Integer
	
	'-Variable que indica si la relación es inversa
	Public sInvrel As String
	
	'- Constante que contiene las relaciones permitidas para Requisitos y exclusiones "DP038".
    Const cstrRelations As String = "11|22|23|24|32|33|34|42|44|66|"
	
	Public bError As Boolean
	Public sReqExcList As String
	Public sError As String
	
	Enum ReqexclType
		cstrModule = 1
		cstrCover = 2
		cstrDiscExpr = 3
		cstrClause = 4
	End Enum
	
	Enum RelationType
		cstrReq = 1
		cstrExc = 2
		cstrSome = 3
	End Enum
	
	'%valAllowRelation: Valida si la relación establecida entre los dos tipos es permitida.
	Private Function valAllowRelation(ByVal sType1 As String, ByVal sType2 As String) As Boolean
		Dim lstrVal As String
		Dim lstrValRel As String
		Dim lstrRelations As String
		
		valAllowRelation = False
		lstrVal = sType1 & sType2
		lstrRelations = cstrRelations
		Do 
			lstrValRel = Left(lstrRelations, InStr(lstrRelations, "|") - 1)
			If lstrVal = lstrValRel Then
				valAllowRelation = True
				lstrRelations = String.Empty
			End If
			lstrRelations = Mid(lstrRelations, InStr(lstrRelations, "|") + 1)
		Loop Until (lstrRelations = String.Empty)
	End Function
	
	'% valTab_reqexc: Valida que existan registros en la tabla Tab_reqexc
	Public Function valTab_reqexc(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sType As String, ByVal nElement As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecvalTab_reqexc As eRemoteDB.Execute
		
		On Error GoTo valTab_reqexc_err
		
		lrecvalTab_reqexc = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.valTab_reqexc'
		'+ Información leída el 04/04/2001 04:21:00 p.m.
		
		With lrecvalTab_reqexc
			.StoredProcedure = "valTab_reqexc"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nType", IIf(Trim(sType) = String.Empty, System.DBNull.Value, CShort(sType)), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nElement", nElement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCode1 = .FieldToClass("nCode1")
				nCode2 = .FieldToClass("nCode2")
				valTab_reqexc = True
				.RCloseRec()
			End If
		End With
		
valTab_reqexc_err: 
		If Err.Number Then
			valTab_reqexc = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalTab_reqexc = Nothing
	End Function
	
	'% insValDP038Upd: Realiza la validación para los requisitos y exclusiones
	Public Function insValDP038Upd(ByVal sAction As String, ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal nRelation As Integer, ByVal nType1 As Integer, ByVal nElement1 As Integer, ByVal nRole1 As Integer, ByVal nType2 As Integer, ByVal nElement2 As Integer, ByVal nRole2 As Integer, ByVal nDefReq As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		
		On Error GoTo insValDP038_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValues = New eFunctions.Values
		
		'+ Todos los campos deben estar llenos
		With lclsErrors
			If nRelation = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(229) & " :")
            End If
			
			If nType1 = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(225) & " :")
            End If
			
			If nElement1 = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(226) & " :")
            End If
			
			If nType2 = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(227) & " :")
            End If
			
			If nElement2 = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, eFunctions.Values.GetMessage(228) & " :")
            End If
			
			'+ Se valida que la relación esté permitida.
			If nType1 > 0 And nType2 > 0 Then
				If Not valAllowRelation(CStr(nType1), CStr(nType2)) Then
					Call .ErrorMessage(sCodispl, 1929)
				End If
			End If
			
			'+ Se valida de que el código del elemento a relacionar y el relacionado no sean el mismo.
			If nElement1 <> eRemoteDB.Constants.intNull And nElement2 <> eRemoteDB.Constants.intNull And nType1 <> eRemoteDB.Constants.intNull And nType2 <> eRemoteDB.Constants.intNull Then
				If nElement1 = nElement2 And nType1 = nType2 And nRole1 = nRole2 Then
					Call .ErrorMessage(sCodispl, 11204)
				End If
			End If
			
			'+ Valida la existencia de una relación en otra línea de la ventana.
			If sAction = "Add" Then
				Call valRelationDP038(sCodispl, nBranch, nProduct, dEffecdate, CStr(nRelation), CStr(nType1), CStr(nElement1), CStr(nType2), CStr(nElement2), lclsErrors, nDefReq)
			End If
			
			
			'+ Si el producto es del tipo "Vida", la definición corresponde a póliza y el tipo de elemento corresponde a "Cobertura", los campos "Rol" deben estar llenos.
			If nDefReq = 1 Then
				If sBrancht = "1" Or sBrancht = "2" Then
					If nType1 = 2 And (nRole1 = 0 Or nRole1 = eRemoteDB.Constants.intNull) Then
						Call .ErrorMessage(sCodispl, 55979)
					ElseIf nType2 = 2 And (nRole2 = 0 Or nRole2 = eRemoteDB.Constants.intNull) Then 
						Call .ErrorMessage(sCodispl, 55979)
					End If
				End If
			End If
			
			insValDP038Upd = .Confirm
		End With
		
insValDP038_Err: 
		If Err.Number Then
			insValDP038Upd = "insValDP038Upd: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValues = Nothing
	End Function
	
	'%insPostDP038: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP038"
	Public Function insPostDP038(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nRelation As Integer, ByVal nType1 As Integer, ByVal nCode1 As Integer, ByVal nRole1 As Integer, ByVal nType2 As Integer, ByVal nCode2 As Integer, ByVal nRole2 As Integer, ByVal nusercode As Integer, ByVal nDefReq As Integer) As Boolean
		Dim lcolTabreqexcs As eProduct.Tab_reqexcs
		Dim lclsProdwin As eProduct.Prod_win
		Dim lintAction As Integer
		
		lcolTabreqexcs = New eProduct.Tab_reqexcs
		
		lintAction = IIf(sAction = "Add" Or sAction = "Update", 1, 2)
		
		If nMainAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			insPostDP038 = insUpdDP038(lintAction, nBranch, nProduct, nType1, nCode1, nRole1, nType2, nCode2, nRole2, dEffecdate, nRelation, nusercode, nDefReq)
			If insPostDP038 Then
				lclsProdwin = New eProduct.Prod_win
				If Not lcolTabreqexcs.Find(nBranch, nProduct, dEffecdate) Then
					'+ Se actualiza la secuencia de ventana del producto sin contenido
					Call lclsProdwin.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP038", "1", nusercode)
				Else
					'+ Se actualiza la secuencia de ventana del producto con contenido
					Call lclsProdwin.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP038", "2", nusercode)
				End If
			End If
		End If
		
		'UPGRADE_NOTE: Object lclsProdwin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProdwin = Nothing
		'UPGRADE_NOTE: Object lcolTabreqexcs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTabreqexcs = Nothing
	End Function
	
	'% insValDP038: Realiza la validación para los requisitos y exclusiones
	Public Function insValDP038(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTabreqexcs As eProduct.Tab_reqexcs
		
		On Error GoTo insValDP038_Err
		
		lclsErrors = New eFunctions.Errors
		lclsTabreqexcs = New eProduct.Tab_reqexcs
		
		With lclsErrors
			If Not lclsTabreqexcs.Find(nBranch, nProduct, dEffecdate) Then
				Call .ErrorMessage(sCodispl, 1928)
			End If
			
			insValDP038 = .Confirm
		End With
		
insValDP038_Err: 
		If Err.Number Then
			insValDP038 = "insValDP038: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTabreqexcs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTabreqexcs = Nothing
	End Function
	
	'% insUpdDP038: Rutina que actualiza la información en la tabla
	Private Function insUpdDP038(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType1 As Integer, ByVal nCode1 As Integer, ByVal nRole1 As Integer, ByVal nType2 As Integer, ByVal nCode2 As Integer, ByVal nRole2 As Integer, ByVal dEffecdate As Date, ByVal nRelation As Integer, ByVal nusercode As Integer, ByVal nDefReq As Integer) As Boolean
		Dim lrecinsTab_reqexc As eRemoteDB.Execute
		
		On Error GoTo insUpdDP038_Err
		
		lrecinsTab_reqexc = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insTab_reqexc'
		'+ Información leída el 07/05/2001 09:08:28 a.m.
		
		With lrecinsTab_reqexc
			.StoredProcedure = "insTab_reqexc"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReqExc1", nType1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode1", nCode1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole1", IIf(nRole1 = eRemoteDB.Constants.intNull, 0, nRole1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReqExc2", nType2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode2", nCode2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole2", IIf(nRole2 = eRemoteDB.Constants.intNull, 0, nRole2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelReqExc", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDefReq", nDefReq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdDP038 = .Run(False)
		End With
		
insUpdDP038_Err: 
		If Err.Number Then
			insUpdDP038 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsTab_reqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsTab_reqexc = Nothing
	End Function
	
	'% valRelationDP038: Valida la existencia de una relación en otra línea de la ventana.
	Private Sub valRelationDP038(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sRelation As String, ByVal sType1 As String, ByVal sElement1 As String, ByVal sType2 As String, ByVal sElement2 As String, ByRef lobjErrors As eFunctions.Errors, ByVal nDefReq As Integer)
		Dim lclsTabreqexc As eProduct.Tab_reqexc
		Dim lcolTabreqexcs As eProduct.Tab_reqexcs
		
		lcolTabreqexcs = New eProduct.Tab_reqexcs
		
		If lcolTabreqexcs.Find(nBranch, nProduct, dEffecdate,  , nDefReq) Then
			For	Each lclsTabreqexc In lcolTabreqexcs
				With lclsTabreqexc
                    If .sType1 = sType1 And .sType2 = sType2 And .nCode1 = CDbl(sElement1) And .nCode2 = CDbl(sElement2) And .nRole1 = nRole1 Then
                        '+ No pueden existir duplicados en la tabla
                        If .sRelation = sRelation Then
                            Call lobjErrors.ErrorMessage(sCodispl, 11096)
                        Else
                            '+ No puede existir otra relación con los mismos elementos
                            Call lobjErrors.ErrorMessage(sCodispl, 11098)
                        End If
                    End If
                End With
			Next lclsTabreqexc
		End If
		
		'UPGRADE_NOTE: Object lclsTabreqexc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTabreqexc = Nothing
		'UPGRADE_NOTE: Object lcolTabreqexcs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolTabreqexcs = Nothing
	End Sub
	
	'% inspreDP038: Establece el estado inicial de la página DP038
	Public Sub inspreDP038(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String)
		Dim lrecRemote As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo inspreDP038_Err
		
		lrecRemote = New eRemoteDB.Execute
		
		With lrecRemote
			.StoredProcedure = "valTab_reqexc_product"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReqExcList", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				bError = IIf(.Parameters("nExists").Value = 1, False, True)
				sReqExcList = Trim(.Parameters("sReqExcList").Value)
				If bError Then
					lclsErrors = New eFunctions.Errors
					sError = lclsErrors.ErrorMessage("DP038", 11397,  ,  ,  , True)
				End If
			End If
		End With
		
inspreDP038_Err: 
		If Err.Number Then
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRemote = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Sub
End Class






