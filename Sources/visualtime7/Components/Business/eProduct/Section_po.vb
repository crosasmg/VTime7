Option Strict Off
Option Explicit On
Public Class Section_po
	'%-------------------------------------------------------%'
	'% $Workfile:: Section_po.cls                           $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 1/12/03 3:20p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'- Propiedades según la tabla en el sistema al 07/05/2001.
	'- Los campos llave de la tabla corresponden a: nBranch, nProduct, sPolitype, sCompon, sCodispl
	
	
	'+  Column_name         Type
	'---------------------- ---------------------
	Public nBranch As Integer 'NUMBER(5)
	Public nProduct As Integer 'NUMBER(5)
	Public sPolitype As String 'CHAR(1)
	Public sCompon As String 'CHAR(1)
	Public nTratypep As Integer 'NUMBER(5)
	Public sCodispl As String 'CHAR(8)
	Public dEffecdate As Date
	Public nId As Integer
	Public nSequence As Integer 'NUMBER(5)
	Public nUsercode As Integer 'NUMBER(5)
	Public nOrigin As Integer
    Public nType_amend As Integer
    Public sReport As String
    Public nOrder As Long
    Public sRoutine As String
	
	Public sDescript As String
	
	'- Indica la existencia de errores al evaluar los datos a mostrar en la página
    Public bError As Boolean
	
	'- Variables para controlar el tipo de póliza permitidos para el producto
	Private mstrIndivind As String
	Private mstrGroupind As String
	Private mstrMultiind As String
	
	'% InsValDP048: Este metodo se encarga de realizar las validaciones de la página
	Public Function insValDP048(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal sCodispl As String, ByVal nOrder As Integer, ByVal nUsercode As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValDP048_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nOrder = eRemoteDB.Constants.intNull Then
				'+ El campo Orden debe estar lleno
				Call .ErrorMessage("DP048", 11146)
			Else
				'+ El orden no debe estar asociado a otro registro
				If Find_Dup(nBranch, nProduct, nOrder, sPolitype, sCompon, sCodispl) Then
					Call .ErrorMessage("DP048", 11147)
				End If
			End If
			
			insValDP048 = .Confirm
		End With
		
insValDP048_Err: 
		If Err.Number Then
			insValDP048 = insValDP048 & Err.Description
		End If
		On Error GoTo 0
        lclsErrors = Nothing
	End Function
	
	'% insUpdSection_po: Se actualizan los campos en la tabla
	Private Function insUpdSection_po(ByVal sSelected As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insUpdSection_po_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "insUpdSection_po"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSelected", sSelected, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 500, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_amend", nType_amend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insUpdSection_po = .Run(False)
		End With
		
insUpdSection_po_Err: 
		If Err.Number Then
			insUpdSection_po = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% insPostDP048: se actualizan los campos en la tabla
	Public Function insPostDP048(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal sCodispl As String, ByVal sSel As String, ByVal nUsercode As Integer, ByVal nTratypep As Integer, ByVal nType_amend As Integer, ByVal nOrigin As Integer) As Boolean
		Dim lstrSelected As String
		Dim lclsProd_win As eProduct.Prod_win
		
		On Error GoTo insPostDP048_Err
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.sPolitype = sPolitype
			.sCompon = sCompon
			.sCodispl = sCodispl
			.nSequence = nSequence
			.nUsercode = nUsercode
			.dEffecdate = dEffecdate
			.nTratypep = nTratypep
			.nType_amend = nType_amend
			.nOrigin = nOrigin
		End With
		
		insPostDP048 = insUpdSection_po(sSel)
		
		'+Se manda a actualizar la secuencia de ventana para la transacción DP048
		If insPostDP048 Then
			lclsProd_win = New eProduct.Prod_win
			If insvalSection_po(nBranch, nProduct, sPolitype, sCompon) Then
				insPostDP048 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP048", "2", nUsercode)
			Else
				insPostDP048 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP048", "1", nUsercode)
			End If
		End If
		
insPostDP048_Err: 
		If Err.Number Then
			insPostDP048 = False
		End If

        lclsProd_win = Nothing
		On Error GoTo 0
	End Function
	
	'% inspreDP048: se controla la carga de los datos a manejar en la página
	Public Sub inspreDP048(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String)
		Dim lclsSequen_pol As Sequen_pol
		Dim lclsProduct As Product
		
		lclsSequen_pol = New Sequen_pol
		
		If Not lclsSequen_pol.valSequenByProduct(nBranch, nProduct, dEffecdate) Then
			bError = True
		Else
			lclsProduct = New Product
			With lclsProduct
				If .Find(nBranch, nProduct, dEffecdate) Then
					mstrIndivind = .sIndivind
					mstrGroupind = .sGroupind
					mstrMultiind = .sMultiind
					If sPolitype = String.Empty Or sCompon = String.Empty Then
						'+ Se asignan los valores por defecto para los campos de la página.
						'+ Tipo de póliza: Individual
						'+ Componente: Póliza
						With Me
							.sPolitype = "1"
							.sCompon = "1"
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
							.sPolitype = sPolitype
							.sCompon = sCompon
						End With
					End If
				End If
			End With
		End If
		
        lclsSequen_pol = Nothing
        lclsProduct = Nothing
	End Sub
	
	'% DefaultValueDP048: Maneja los estados y/o valores por defecto de los campos de la ventana
	Public Function DefaultValueDP048(ByVal sField As String) As Object
        Dim lvarResult As Object = New Object

        Select Case sField
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
		DefaultValueDP048 = lvarResult
	End Function
	
	'% Find_Dup: Verifica que el nro. de orden no se encuentre asociado a otra sección
	Private Function Find_Dup(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nSequence As Integer, ByVal sPolitype As String, ByVal sCompon As String, ByVal sCodispl As String) As Boolean
		Dim lrecinsValDupSection_po As eRemoteDB.Execute
		
		On Error GoTo Find_Dup_err
		
		lrecinsValDupSection_po = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insValDupSection_po'
		'+ Información leída el 02/07/2002
		
		With lrecinsValDupSection_po
			.StoredProcedure = "insValDupSection_po"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Find_Dup = IIf(.Parameters("nExists").Value = 1, True, False)
			End If
		End With
		
Find_Dup_err: 
		If Err.Number Then
			Find_Dup = False
		End If
		On Error GoTo 0
        lrecinsValDupSection_po = Nothing
	End Function
	
	'% insvalSection_po: verifica que el producto tenga la secuencia para el cuadro de pólizas
	Private Function insvalSection_po(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal sCompon As String) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo insvalSection_po_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "valSection_po_product"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				insvalSection_po = IIf(.Parameters("nExists").Value > 0, True, False)
			End If
		End With
		
insvalSection_po_Err: 
		If Err.Number Then
			insvalSection_po = False
		End If
		On Error GoTo 0
        lclsRemote = Nothing
    End Function

    Public Function InsPostDP048UPD(ByVal sAction As String, _
                                ByVal nBranch As Long, _
                                ByVal nProduct As Long, _
                                ByVal sPolitype As String, _
                                ByVal sCompon As String, _
                                ByVal nTratypep As Long, _
                                ByVal sCodispl As String, _
                                ByVal dEffecdate As Date, _
                                ByVal nId As Long, _
                                ByVal sReport As String, _
                                ByVal nOrder As Long, _
                                ByVal sRoutine As String, _
                                ByVal nUsercode As String) As Boolean
        '--------------------------------------------------------------------------------------------

        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo insUpdSection_po_2_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "insUpdSection_po_2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTratypep", nTratypep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReport", sReport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsPostDP048UPD = .Run(False)
        End With

insUpdSection_po_2_Err:
        If Err.Number Then
            InsPostDP048UPD = False
        End If
        On Error GoTo 0
        lclsRemote = Nothing
    End Function
End Class






