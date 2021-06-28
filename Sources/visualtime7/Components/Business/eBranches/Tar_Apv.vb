Option Strict Off
Option Explicit On
Public Class Tar_Apv
	
	'+ Definición de la tabla TAR_Apv tomada el 10/12/2002.
	'+ Column_Name                          Type         Length  Prec     Scale  Nullable
	'------------------------------         --------------- -   -------- ------- --------
	Public nBranch As Integer ' NUMBER        22     5      0         No
	Public nProduct As Integer ' NUMBER        22     5      0         No
	Public dEffecdate As Date ' DATE           7                      No
	Public nRole As Integer ' NUMBER        22     5      0         No
	Public nModulec As Integer ' NUMBER        22     5      0         No
	Public nCover As Integer ' NUMBER        22     5      0         No
	Public nAge_init As Integer ' NUMBER        22     5      0         No
	Public nCapital_init As Double ' NUMBER        22    18      6         No
	Public nAge_End As Integer ' NUMBER        22     5      0         Yes
	Public nCapital_end As Double ' NUMBER        22    12      0         Yes
	Public nRate As Double ' NUMBER        22    16     12         Yes
	Public nFix_cost As Double ' NUMBER        22    16     12         Yes
	Public nType_tar As Integer ' NUMBER        22     5      0         Yes
    Public sType_tar As string ' NUMBER        22     5      0         Yes
	Public nUsercode As Integer ' NUMBER        5
	'- Variables nuevas declaradas atendiendo a modificación APV2 - ACM - 06/08/2003
	Public nType_calc As Integer '      NOT NULL NUMBER(5)
    Public sType_calc As String
	Public nSex As Short '      NOT NULL NUMBER(5)
    Public sSexClien As String 
	Public nCurrency As Integer '      NOT NULL NUMBER(5)
    Public sCurrency As String
	Public nPolicy_Year_ini As Integer '      NOT NULL NUMBER(5)
	Public nPolicy_Year_end As Integer '      NOT NULL NUMBER(5)
	Public nOption As Integer '      NOT NULL NUMBER(5)
    Public sOption As String

	Public sSmoking As String '      NOT NULL CHAR(1 Byte)

	Public nTyperisk As Integer '      NOT NULL NUMBER(2)
    Public sTyperisk As String
	
	'- Variables auxiliares.
	
	Private mblnAge As Boolean
	Private mblnCapital As Boolean
	Private mblnYear_ini As Boolean
	Private mblnYear_end As Boolean
	
	
	'% Delete: Elimina un registro de la tabla de tarifas de APV..
	Public Function Delete() As Boolean
		Dim lrecDelTar_Apv As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		lrecDelTar_Apv = New eRemoteDB.Execute
		
		With lrecDelTar_Apv
			.StoredProcedure = "delTar_Apv"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NTYPE_CALC", nType_calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NSEX", nSex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NPOLICY_YEAR_INI", nPolicy_Year_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecDelTar_Apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDelTar_Apv = Nothing
	End Function
	
	'% Update: Crea/Actualiza un registro dentro de la tabla de Tarifas de APV.
	Public Function Update() As Boolean
		Dim lrecUpdTar_Apv As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecUpdTar_Apv = New eRemoteDB.Execute
		
		With lrecUpdTar_Apv
			.StoredProcedure = "insUpdTar_Apv"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 12, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFix_cost", nFix_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 12, 16, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_tar", nType_tar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+ Cambios [APV2] - ACM - 08/08/2003
			.Parameters.Add("NTYPE_CALC", nType_calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NSEX", nSex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NCURRENCY", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NPOLICY_YEAR_INI", nPolicy_Year_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NPOLICY_YEAR_END", nPolicy_Year_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'+ Cambios - Campos agregados - 19/06/2008
			.Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecUpdTar_Apv may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdTar_Apv = Nothing
	End Function
	
	'% insValMVI7000_K: Esta función valida los campos del encabezado de la transacción MVI7000 -
	'% Tarifas de APV.
	Public Function insValMVI7000_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsProduct As eProduct.Product
		Dim lblnValid As Boolean
		
		On Error GoTo InsValMVI7000_Err
		
		lobjErrors = New eFunctions.Errors
		lclsProduct = New eProduct.Product
		
		lblnValid = True
		
		With lobjErrors
			
			'+ Ramo: Debe estar lleno.
			
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1022)
				lblnValid = False
			End If
			
			'+ Producto: El producto debe estar lleno.
			
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1014)
				lblnValid = False
			End If
			
			If lblnValid Then
				If lclsProduct.FindProdMaster(nBranch, nProduct) Then
					If lclsProduct.sBrancht <> eProduct.Product.pmBrancht.pmlife And lclsProduct.sBrancht <> eProduct.Product.pmBrancht.pmMixed Then
						
						'+ El producto debe ser de vida o combinado.
						
						Call .ErrorMessage(sCodispl, 3403)
					End If
				End If
			End If
			
			'+ Fecha: Debe estar llena.
			
			If dEffecdate = dtmNull Then
				Call .ErrorMessage(sCodispl, 3404)
			End If
			
			'+ Módulo: Si el producto es modular, el código del módulo debe estar lleno.
			
			If lclsProduct.IsModule(nBranch, nProduct, dEffecdate) Then
				If nModulec = eRemoteDB.Constants.intNull Then
					Call .ErrorMessage(sCodispl, 12165)
				End If
			End If
			
			'+ Cobertura: El código de la cobertura debe estar lleno.
			
			If nCover = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 11163)
			End If
			
			'+ Tipo de asegurado: El tipo de asegurado debe estar lleno.
			
			If nRole = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 10241)
			End If
			
			insValMVI7000_K = .Confirm
		End With
		
InsValMVI7000_Err: 
		If Err.Number Then
			insValMVI7000_K = "insvalMVI7000: " & Err.Description
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% insValMVI7000Upd: Esta función valida los campos del detalle de la transacción MVI7000 -
	'% Tarifas de APV.
    Public Function insValMVI7000Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer,
                                     ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer,
                                     ByVal nCover As Integer, ByVal nRole As Integer, ByVal nAge_init As Integer,
                                     ByVal nAge_End As Integer, ByVal nCapital_init As Double, ByVal nCapital_end As Double,
                                     ByVal nRate As Double, ByVal nFix_cost As Double, ByVal nType_calc As Integer,
                                     ByVal nSex As Short, ByVal nCurrency As Integer, ByVal nPolicy_Year_ini As Integer,
                                     ByVal nPolicy_Year_end As Integer, ByVal nOption As Integer, ByVal sSmoking As String,
                                     ByVal nTyperisk As Integer) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lblnValid As Boolean

        On Error GoTo InsValMVI7000_Err

        lobjErrors = New eFunctions.Errors

        lblnValid = True

        With lobjErrors

            '+ Edad inicial: La edad inicial debe estar llena.

            If nAge_init = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 11109)
                lblnValid = False
            End If

            '+ Edad final: La edad final debe estar llena.

            If nAge_End = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 11110)
            Else

                '+ La edad final debe ser mayor a la edad inicial.

                If lblnValid Then
                    If nAge_End < nAge_init Then
                        Call .ErrorMessage(sCodispl, 11036)
                    End If
                End If
            End If

            lblnValid = True

            '+ Capital inicial: El capital inicial debe estar lleno.

            If nCapital_init = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 11111)
                lblnValid = False
            End If

            '+ Capital final: El capital final debe estar lleno.

            If nCapital_end = eRemoteDB.Constants.intNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 11112)
            Else

                '+ El capital final debe ser mayor al capital inicial.

                If lblnValid Then
                    If nCapital_end < nCapital_init Then
                        Call .ErrorMessage(sCodispl, 10148)
                    End If
                End If
            End If

            '+ Debe estar llena la Tasa o el Costo Fijo.

            If (nRate = 0 Or nRate = eRemoteDB.Constants.intNull) And (nFix_cost = 0 Or nFix_cost = eRemoteDB.Constants.intNull) Then
                Call .ErrorMessage(sCodispl, 10124)
            End If

            '+ Se verifica que la edad y el capital no se encuentre en otro rango dentro de la tabla.

            If sAction = "Add" Then
                If valTar_Apv_Range(nBranch, nProduct, dEffecdate, nModulec, nCover, nRole, nAge_init, nAge_End, nCapital_init, nCapital_end, nType_calc, nSex, nPolicy_Year_ini, nPolicy_Year_end, nPolicy_Year_ini, nOption, sSmoking, nTyperisk) Then
                    If mblnAge Then
                        Call .ErrorMessage(sCodispl, 11138, , eFunctions.Errors.TextAlign.LeftAling, "Edad: ")
                    End If

                    If mblnCapital Then
                        Call .ErrorMessage(sCodispl, 11138, , eFunctions.Errors.TextAlign.LeftAling, "Capital: ")
                    End If

                    '+ Validaciones nuevas [APV2] - ACM - 07/08/2003
                    If mblnYear_ini Then
                        Call .ErrorMessage(sCodispl, 70102, , eFunctions.Errors.TextAlign.LeftAling, "Año póliza inicial: ")
                    End If

                    If mblnYear_end Then
                        Call .ErrorMessage(sCodispl, 70102, , eFunctions.Errors.TextAlign.LeftAling, "Año póliza final: ")
                    End If
                End If
            End If

            '+ Validaciones nuevas [APV2] - ACM - 07/08/2003
            If nPolicy_Year_end <= 0 Then
                Call .ErrorMessage(sCodispl, 70100)
            Else
                If nPolicy_Year_end < nPolicy_Year_ini Then
                    Call .ErrorMessage(sCodispl, 70106)
                End If
            End If

            If nPolicy_Year_ini <= 0 Then
                Call .ErrorMessage(sCodispl, 70100)
            End If

            If nSex <= 0 Then
                Call .ErrorMessage(sCodispl, 70035)
            End If

            If nCurrency <= 0 Then
                Call .ErrorMessage(sCodispl, 5126)
            End If

            '+ Validaciones nuevas - 19/06/2008
            '+ Opcion de indemnización: Debe estar lleno.
            If nOption <= 0 Then
                Call .ErrorMessage(sCodispl, 56006)
            End If

            '+ Tipo de riesgo: Debe estar lleno.
            If nTyperisk <= 0 Then
                Call .ErrorMessage(sCodispl, 3225)
            End If

            insValMVI7000Upd = lobjErrors.Confirm
        End With

InsValMVI7000_Err:
        If Err.Number Then
            insValMVI7000Upd = "insValMVI7000Upd: " & Err.Description
        End If

        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function
	
	'% insPostMVI7000: Esta función se encarga de crear/actualizar/eliminar los registros
	'%                 correspondientes en la tabla de Tarifas de APV - Tab_Apv.
	Public Function insPostMVI7000(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal nAge_init As Integer, ByVal nAge_End As Integer, ByVal nCapital_init As Double, ByVal nCapital_end As Double, ByVal nRate As Double, ByVal nFix_cost As Double, ByVal nType_tar As Integer, ByVal nUsercode As Integer, Optional ByVal nType_calc As Integer = 0, Optional ByVal nSex As Short = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nPolicy_Year_ini As Integer = 0, Optional ByVal nPolicy_Year_end As Integer = 0, Optional ByVal nOption As Integer = 0, Optional ByVal sSmoking As String = "", Optional ByVal nTyperisk As Integer = 0) As Boolean
		On Error GoTo insPostMVI7000_Err
		
		If sSmoking = String.Empty Or sSmoking = "2" Then
			sSmoking = "2"
		Else
			sSmoking = "1"
		End If
		
		With Me
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nRole = nRole
			.nModulec = nModulec
			.nCover = nCover
			.nAge_init = nAge_init
			.nAge_End = nAge_End
			.nCapital_init = nCapital_init
			.nCapital_end = nCapital_end
			.nRate = nRate
			.nFix_cost = nFix_cost
			.nType_tar = nType_tar
			.nUsercode = nUsercode
			.nType_calc = nType_calc
			.nSex = nSex
			.nCurrency = nCurrency
			.nPolicy_Year_ini = nPolicy_Year_ini
			.nPolicy_Year_end = nPolicy_Year_end
			.nOption = nOption
			.sSmoking = sSmoking
			.nTyperisk = nTyperisk
		End With
		
		Select Case sAction
			Case "Add", "Update"
				insPostMVI7000 = Update
				
			Case "Del"
				insPostMVI7000 = Delete
		End Select
		
insPostMVI7000_Err: 
		If Err.Number Then
			insPostMVI7000 = False
		End If
		
		On Error GoTo 0
	End Function
	
	'% valTar_Apv_Range: Esta función verifica la existencia de la edad y del capital dentro de otro rango.
    Private Function valTar_Apv_Range(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, _
                                      ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, _
                                      ByVal nAge_init As Integer, ByVal nAge_End As Integer, ByVal nCapital_init As Double, _
                                      ByVal nCapital_end As Double, ByVal nType_calc As Integer, ByVal nSex As Integer, _
                                      ByVal nyear_ini As Integer, ByVal nyear_end As Integer, ByVal nPolicy_Year_ini As Integer, _
                                      ByVal nOption As Integer, ByVal sSmoking As String, ByVal nTyperisk As Integer) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo valTar_Apv_Range_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "valTar_Apv_Range"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_init", nCapital_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_end", nCapital_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Modificaciones por Fallas por no coincidencia en SP VALTAR_APV_RANGE OMAR ARCAYA - 11/03/2016
            .Parameters.Add("nType_calc", nType_calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSex", nSex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Modificaciones por Fallas por no coincidencia en SP VALTAR_APV_RANGE OMAR ARCAYA - 11/03/2016
            '+ Modificaciones por nuevas validaciones de rangos [APV2] - ACM - 08/08/2003
            .Parameters.Add("nYear_ini", nyear_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear_end", nyear_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Modificaciones por Fallas por no coincidencia en SP VALTAR_APV_RANGE OMAR ARCAYA - 11/03/2016
            .Parameters.Add("nPolicy_Year_ini", nPolicy_Year_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Modificaciones por Fallas por no coincidencia en SP VALTAR_APV_RANGE OMAR ARCAYA - 11/03/2016
            .Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists_age", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists_capital", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '+ Modificaciones por nuevas validaciones de rangos [APV2] - ACM - 08/08/2003
            .Parameters.Add("nExists_year_ini", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists_year_end", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters("nExists_age").Value = 1 Then
                    mblnAge = True
                End If

                If .Parameters("nExists_capital").Value = 1 Then
                    mblnCapital = True
                End If

                If .Parameters("nExists_year_ini").Value = 1 Then
                    mblnYear_ini = True
                End If

                If .Parameters("nExists_year_end").Value = 1 Then
                    mblnYear_end = True
                End If

                valTar_Apv_Range = True
            End If
        End With

valTar_Apv_Range_Err:
        If Err.Number Then
            valTar_Apv_Range = False
        End If

        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsRemote = Nothing
    End Function
End Class






