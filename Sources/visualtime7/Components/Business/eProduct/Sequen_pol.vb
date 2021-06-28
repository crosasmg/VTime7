Option Strict Off
Option Explicit On
Public Class Sequen_pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Sequen_pol.cls                           $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 6/10/03 17.23                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema al 07/05/2001.
	'+ Los campos llave corresponden a nBranch , nProduct, sBussityp, nTratypep, sPolitype, sCompon, nSequence, dEffecdate
	
	'+ Column_name          Type
	'---------------------- ----------------------
	Public nBranch As Integer 'NUMBER(5)
	Public nProduct As Integer 'NUMBER(5)
	Public sBussityp As String 'CHAR(1)
	Public nTratypep As Integer 'NUMBER(5)
	Public sPolitype As String 'CHAR(1)
	Public sCompon As String 'CHAR(1)
	Public Nsequence As Integer 'NUMBER(5)
	Public dEffecdate As Date 'DATE
	Public sCodispl As String 'CHAR(8)
	Public sRequire As String 'CHAR(1)
	Public nUsercode As Integer 'NUMBER(5)
	Public sAutomatic As String 'CHAR(1)
	
	Public nType_Amend As Short
	
	'+ Variables auxiliares
	
	Public sDescript As String
	Public sRequirePol As String
	Public Codispl_Exist As String
	Public sSelected As String
	
	'- Indica la existencia de errores al evaluar los datos a mostrar en la página
	Public bError As Boolean
	
	'- Variables para controlar el tipo de póliza permitidos para el producto
	Private mstrIndivind As String
	Private mstrGroupind As String
	Private mstrMultiind As String
	
	'- Contiene el orden de todas las transacciones mostradas en la ventana
	Public sSequence As String
	
	'% Update: Actualiza un registro de la tabla Sequen_pol
	Public Function Update() As Boolean
		Dim lrecinsSequen_pol As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsSequen_pol = New eRemoteDB.Execute
		
		With lrecinsSequen_pol
			.StoredProcedure = "insUpdSequen_pol"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBussityp", sBussityp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSequence", sSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSelected", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomatic", sAutomatic, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Amend", IIf(nType_Amend = -32768, 0, nType_Amend), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsSequen_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsSequen_pol = Nothing
	End Function
	
	'% insValDP012: Realiza la validación de los campos de la página
	Public Function insValDP012(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCount As Integer, ByVal sMassive As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP012_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nCount = 0 Then
				'+ Debe seleccionarse al menos un registro
				Call .ErrorMessage("DP012", 11084)
			End If
			
			'+ Si se acepta la ventana
			If sMassive = "1" Then
				If Not insvalAll_Sequence(nBranch, nProduct, dEffecdate) Then
					'+ Se debe indicar la secuencia de emisión para todos los tipos de póliza
					Call .ErrorMessage("DP012", 11400)
				End If
			End If
			
			insValDP012 = .Confirm
		End With
		
insValDP012_Err: 
		If Err.Number Then
			insValDP012 = "insValDP012: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'% insPostDP012: se realizan las actualizaciones en la tabla
	Public Function insPostDP012(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBussityp As String, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTratypep As Integer, ByVal sCodispl As String, ByVal sSequence As String, ByVal sRequire As String, ByVal sSelected As String, ByVal sAutomatic As String, ByVal nUsercode As Integer, ByVal nType_Amend As Short) As Boolean
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP012_Err
		
		insPostDP012 = True
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			With Me
				.nBranch = nBranch
				.nProduct = nProduct
				.dEffecdate = dEffecdate
				.sBussityp = sBussityp
				.sPolitype = sPolitype
				.sCompon = IIf(sCompon = String.Empty, "1", sCompon)
				.nTratypep = nTratypep
				.sCodispl = sCodispl
				.sSequence = sSequence
				.sRequire = IIf(sRequire = String.Empty, "2", sRequire)
				.sSelected = sSelected
				.sAutomatic = sAutomatic
				.nUsercode = nUsercode
				.nType_Amend = nType_Amend
			End With
			
			'+ Se hacen las actualizaciones correspondientes en la tabla Sequen_pol
			insPostDP012 = Update
			
			'+ Se manda a actualizar la secuencia de ventana del producto
			If insPostDP012 Then
				lclsProd_win = New eProduct.Prod_win
				insPostDP012 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP012", IIf(insPostDP012, "2", "1"), nUsercode)
			End If
		End If
		
insPostDP012_Err: 
		If Err.Number Then
			insPostDP012 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProd_win = Nothing
	End Function
	
	'% inspreDP012: se controla la carga de los datos a manejar en la página
	Public Sub inspreDP012(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBussityp As String, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTratypep As Integer, ByVal nType_Amend As Short)
		Dim lclsProduct As Product
		lclsProduct = New Product
		
		With lclsProduct
			If .Find(nBranch, nProduct, dEffecdate) Then
				mstrIndivind = .sIndivind
				mstrGroupind = .sGroupind
				mstrMultiind = .sMultiind
				If .sIndivind = "2" And .sGroupind = "2" And .sMultiind = "2" Then
					bError = True
				End If
				
				If sBussityp = String.Empty Or sPolitype = String.Empty Or sCompon = String.Empty Or nTratypep = eRemoteDB.Constants.intNull Then
					'+ Se asignan los valores por defecto para los campos de la página.
					'+ Tipo de negocio: Directo
					'+ Tipo de póliza: Individual
					'+ Componente: Póliza
					'+ Transacción: Emisión
					With Me
						.sBussityp = "1"
						.sPolitype = "1"
						.sCompon = "1"
						.nTratypep = CInt("1")
						.nType_Amend = 0
						If mstrIndivind = "2" Then
							.sPolitype = "2"
							If mstrGroupind = "2" Then
								.sPolitype = "3"
								If mstrMultiind = "2" Then
									.sPolitype = "1"
								End If
							End If
						End If
					End With
				Else
					With Me
						.sBussityp = sBussityp
						.sPolitype = sPolitype
						.sCompon = sCompon
						.nTratypep = nTratypep
						.nType_Amend = nType_Amend
					End With
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Sub
	
	'% DefaultValueDP012: Maneja los estados y/o valores por defecto de los campos de la ventana
	Public Function DefaultValueDP012(ByVal sField As String) As Object
        Dim lvarResult As Object = New Object

        Select Case sField
			Case "optDir_value"
				lvarResult = IIf(sBussityp = "1", "1", "2")
			Case "optCoa_value"
				lvarResult = IIf(sBussityp = "2", "1", "2")
			Case "optRea_value"
				lvarResult = IIf(sBussityp = "3", "1", "2")
			Case "optInd_value"
				lvarResult = IIf(sPolitype = "1", "1", "2")
			Case "optCol_value"
				lvarResult = IIf(sPolitype = "2", "1", "2")
			Case "optMul_value"
				lvarResult = IIf(sPolitype = "3", "1", "2")
			Case "optPol_value"
				lvarResult = IIf(sCompon = "1", "1", "2")
			Case "optCert_value"
				lvarResult = IIf(sCompon = "2", "1", "2")
			Case "optInd_disabled"
				lvarResult = IIf(mstrIndivind = "2" Or bError, True, False)
			Case "optCol_disabled"
				lvarResult = IIf(mstrGroupind = "2" Or bError, True, False)
			Case "optMul_disabled"
				lvarResult = IIf(mstrMultiind = "2" Or bError, True, False)
			Case "optPol_disabled"
				lvarResult = IIf(bError, True, False)
				
			Case "optCert_disabled"
				lvarResult = IIf((sPolitype = "2" Or sPolitype = "3") And Not bError, False, True)
		End Select
		DefaultValueDP012 = lvarResult
	End Function
	
	'% insvalAll_Sequence: verifica que el producto tenga la secuencia de emision para todos los
	'%                     tipos de póliza permitidos
	Private Function insvalAll_Sequence(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalAll_Sequence_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valSequen_Product"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValid", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalAll_Sequence = IIf(Trim(.Parameters("sValid").Value) = "1", True, False)
			End If
		End With
		
insvalAll_Sequence_Err: 
		If Err.Number Then
			insvalAll_Sequence = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% valSequenByProduct: verifica que existan secuencia de ventanas para el tratamiento de
	'%                     pólizas para el Ramo-Producto
	Public Function valSequenByProduct(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo valSequenbyProduct_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valSequen_pol_product"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				valSequenByProduct = IIf(.Parameters("nExists").Value = 1, True, False)
			End If
		End With
		
valSequenbyProduct_Err: 
		If Err.Number Then
			valSequenByProduct = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






