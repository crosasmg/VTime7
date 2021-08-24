Option Strict Off
Option Explicit On
Public Class CliDocuments
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'CliDocuments' in the system 11/19/2004 3:04:01 PM
	'+Objetivo: Propiedades según la tabla 'CliDocuments' en el sistema 11/19/2004 3:04:01 PM
	
	'+ Código del cliente
	Public sClient As String
	'+ Código del tipo de documento de identificación del cliente.
	Public nTypClientDoc As Short
	'+ Código del tipo de documento de identificación del cliente.
	Public sCliNumDocu As String
	'+Fecha de emisión del documento
	Public dIssueDat As Date
	'+Fecha de expiración del documento
	Public dExpirDat As Date
	
	'%Objetivo: Elimina los documentos asociados a un cliente
	'%Parámetros:
	'%    sClient - Código de identificación del cliente
	Public Function DeleteAll(ByVal sClient As String) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		
        lclsCliDocuments = New eRemoteDB.Execute
		
		With lclsCliDocuments
			.StoredProcedure = "delCliDocuments"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nTypClientDoc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DeleteAll = .Run(False)
		End With
		
		lclsCliDocuments = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Add a record to the table "CliDocuments"
	'**%Parameters:
	'**%    nUsercode - Code of the user
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'**%    sCliNumDocu - Code of the type of ID card of the client.
	'**%    dIssueDat - Date of emission of the document
	'**%    dExpirDat - Date of expiration of the document
	'%Objetivo: Agrega un registro a la tabla "CliDocuments"
	'%Parámetros:
	'%    nUsercode - Código del usuario
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	'%    sCliNumDocu - Código del tipo de documento de identificación del cliente.
	'%    dIssueDat - Fecha de emisión del documento
	'%    dExpirDat - Fecha de expiración del documento
	Private Function Add(ByVal nUsercode As Integer, ByVal sClient As String, ByVal nTypClientDoc As Short, ByVal sCliNumDocu As String, ByVal dIssueDat As Date, ByVal dExpirDat As Date) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		
        lclsCliDocuments = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creCliDocuments'. Generated on 11/19/2004 3:04:01 PM
		
		With lclsCliDocuments
			.StoredProcedure = "creCliDocuments"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypClientDoc", nTypClientDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliNumDocu", sCliNumDocu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssueDat", dIssueDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirDat", dExpirDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
		lclsCliDocuments = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Updates a registry to the table "CliDocuments" using the key for this table.
	'**%Parameters:
	'**%    nUsercode - Code of the user
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'**%    sCliNumDocu - Code of the type of ID card of the client.
	'**%    dIssueDat - Date of emission of the document
	'**%    dExpirDat - Date of expiration of the document
	'%Objetivo: Actualiza un registro a la tabla "CliDocuments" usando la clave para dicha tabla.
	'%Parámetros:
	'%    nUsercode - Código del usuario
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	'%    sCliNumDocu - Código del tipo de documento de identificación del cliente.
	'%    dIssueDat - Fecha de emisión del documento
	'%    dExpirDat - Fecha de expiración del documento
	Private Function Update(ByVal nUsercode As Integer, ByVal sClient As String, ByVal nTypClientDoc As Short, ByVal sCliNumDocu As String, ByVal dIssueDat As Date, ByVal dExpirDat As Date) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		
        lclsCliDocuments = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updCliDocuments'. Generated on 11/19/2004 3:04:01 PM
		With lclsCliDocuments
			.StoredProcedure = "updCliDocuments"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypClientDoc", nTypClientDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliNumDocu", sCliNumDocu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssueDat", dIssueDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirDat", dExpirDat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
		lclsCliDocuments = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Delete a registry the table "CliDocuments" using the key for this table.
	'**%Parameters:
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'%Objetivo: Elimina un registro a la tabla "CliDocuments" usando la clave para dicha tabla.
	'%Parámetros:
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	Private Function Delete(ByVal sClient As String, ByVal nTypClientDoc As Short) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		
        lclsCliDocuments = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delCliDocuments'. Generated on 11/19/2004 3:04:01 PM
		With lclsCliDocuments
			.StoredProcedure = "delCliDocuments"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypClientDoc", nTypClientDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		lclsCliDocuments = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: It verifies the existence of a registry in table "CliDocuments" using the key of this table.
	'**%Parameters:
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'%Objetivo: Verifica la existencia de un registro en la tabla "CliDocuments" usando la clave de dicha tabla.
	'%Parámetros:
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	Private Function IsExist(ByVal sClient As String, ByVal nTypClientDoc As Short) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsCliDocuments = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valCliDocumentsExist'. Generated on 11/19/2004 3:04:01 PM
		With lclsCliDocuments
			.StoredProcedure = "reaCliDocuments_v"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypClientDoc", nTypClientDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsCliDocuments = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl -
	'**%    nMainAction -
	'**%    sAction -
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'**%    sCliNumDocu - Code of the type of ID card of the client.
	'**%    dIssueDat - Date of emission of the document
	'**%    dExpirDat - Date of expiration of the document
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl - Código de la transacción
	'%    nMainAction - Número de la acción a realizar
	'%    sAction - Acción del grid a realizar
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	'%    sCliNumDocu - Código del tipo de documento de identificación del cliente.
	'%    dIssueDat - Fecha de emisión del documento
	'%    dExpirDat - Fecha de expiración del documento
	Public Function InsValBC6000Upd(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sClient As String, ByVal nTypClientDoc As Short, ByVal sCliNumDocu As String, ByVal dIssueDat As Date, ByVal dExpirDat As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsGeneralFunction As Object
		
        lclsErrors = New eFunctions.Errors
		lclsGeneralFunction = eRemoteDB.NetHelper.CreateClassInstance("eGeneral.GeneralFunction")
		
		If Not lclsGeneralFunction.InsFormatValue(2, nTypClientDoc, sCliNumDocu) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7320)
		End If
		
		If (nTypClientDoc <> 0 Or nTypClientDoc <> eRemoteDB.Constants.intNull) And sCliNumDocu = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 3379)
		End If
		
		If dIssueDat = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 7321)
		Else
			If dIssueDat > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 7011)
			End If
			
			If DatePart(Microsoft.VisualBasic.DateInterval.Year, dIssueDat) < 1900 Then
				Call lclsErrors.ErrorMessage(sCodispl, 90297)
			End If
			
			If DatePart(Microsoft.VisualBasic.DateInterval.Year, dIssueDat) > 3000 Then
				Call lclsErrors.ErrorMessage(sCodispl, 90298)
			End If
		End If
		
		If dExpirDat <> eRemoteDB.Constants.dtmNull And dExpirDat < dIssueDat Then
			Call lclsErrors.ErrorMessage(sCodispl, 7322)
		End If
		
		If dExpirDat <> eRemoteDB.Constants.dtmNull And DatePart(Microsoft.VisualBasic.DateInterval.Year, dExpirDat) > 3000 Then
			Call lclsErrors.ErrorMessage(sCodispl, 90298)
		End If
		
		If dExpirDat <> eRemoteDB.Constants.dtmNull And DatePart(Microsoft.VisualBasic.DateInterval.Year, dExpirDat) < 1900 Then
			Call lclsErrors.ErrorMessage(sCodispl, 90297)
		End If
		
		If sAction = "Add" And IsExist(sClient, nTypClientDoc) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7318)
		End If
		
		If (sAction = "Add" Or sAction = "Update") And IsExistDoc(sClient, nTypClientDoc, sCliNumDocu) Then
			Call lclsErrors.ErrorMessage(sCodispl, 7319)
		End If
		
		InsValBC6000Upd = lclsErrors.Confirm
		
		lclsErrors = Nothing
		lclsGeneralFunction = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%    sCodispl -
	'**%    nMainAction -
	'**%    sAction -
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'**%    sCliNumDocu - Code of the type of ID card of the client.
	'**%    dIssueDat - Date of emission of the document
	'**%    dExpirDat - Date of expiration of the document
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%    sCodispl - Código de la transacción
	'%    nMainAction - Número de la acción a realizar
	'%    sAction - Acción del grid a realizar
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	'%    sCliNumDocu - Código del tipo de documento de identificación del cliente.
	'%    dIssueDat - Fecha de emisión del documento
	'%    dExpirDat - Fecha de expiración del documento
	Public Function InsValBC6000(ByVal sCodispl As String, ByVal sClient As String, ByVal nPerson_typ As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lstrContent As String
		
        lclsErrors = New eFunctions.Errors
		lstrContent = insBC6000Content(sClient)
		If lstrContent = "3" Then
			If nPerson_typ = CDbl("1") Then
				lclsErrors.ErrorMessage(sCodispl, 90365)
			Else
				lclsErrors.ErrorMessage(sCodispl, 90366)
			End If
		End If
		InsValBC6000 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    sCodispl -
	'**%    nMainAction -
	'**%    sAction -
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'**%    sCliNumDocu - Code of the type of ID card of the client.
	'**%    dIssueDat - Date of emission of the document
	'**%    dExpirDat - Date of expiration of the document
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    sCodispl - Código de la transacción
	'%    nMainAction - Número de la acción a realizar
	'%    sAction - Acción del grid a realizar
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	'%    sCliNumDocu - Código del tipo de documento de identificación del cliente.
	'%    dIssueDat - Fecha de emisión del documento
	'%    dExpirDat - Fecha de expiración del documento
	Public Function InsPostBC6000(ByVal pblnHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sClient As String, ByVal nTypClientDoc As Short, ByVal sCliNumDocu As String, ByVal dIssueDat As Date, ByVal dExpirDat As Date, Optional ByRef nTypeCompany As Short = 0) As Boolean

		If pblnHeader Then
			InsPostBC6000 = True
		Else
			If sAction = "Add" Then
				InsPostBC6000 = Add(nUsercode, sClient, nTypClientDoc, sCliNumDocu, dIssueDat, dExpirDat)
			ElseIf sAction = "Update" Then 
				InsPostBC6000 = Update(nUsercode, sClient, nTypClientDoc, sCliNumDocu, dIssueDat, dExpirDat)
			ElseIf sAction = "Del" Then 
				InsPostBC6000 = Delete(sClient, nTypClientDoc)
			End If
		End If
		
		If InsPostBC6000 Then
			Call insBC6000Content(sClient, nUsercode, nTypeCompany, sAction)
		End If
		
		Exit Function
	End Function
	
	'**%Objective: verify what the content is necesary to be applied to the BC6000 page based
	'**%           on the following conditions: client's age and records in table CliDocuments
	'**%Parameters:
	'**%    sClient - Code of identification of the client
	'%Objetivo: verifica el contenido necesario ha ser aplicado a la BC6000 basado en las siguientes
	'%          condiciones: edad del cliente y registros en la tabla CliDocuments
	'%Parámetros:
	'%    sClient - Código de identificación del cliente
    Public Function insBC6000Content(ByVal sClient As String, Optional ByVal nUsercode As Short = 0, Optional ByVal nTypeCompany As Short = 0, Optional ByVal sAction As String = "") As String
        Dim lblnCliDcoument As Boolean
        Dim lclsCliDocumentss As CliDocumentss
        Dim lclsCliDocuments As CliDocuments
        Dim lclsClient As Client
        Dim lclsClientWin As eClient.ClientWin
        Dim lstrContent As String = String.Empty
        Dim lclsTab_business As Tab_business
        Dim lclsAdress As eGeneralForm.Addresss
        Dim lintTypeCompany As Short
        Dim lblnExist As Boolean
        Dim lblnRequierd As Boolean


        lclsClientWin = New eClient.ClientWin
        lclsClient = New Client

        If lclsClient.Find(sClient) Then

            '+ Se realizan las validaciones para un cliente Natural

            If lclsClient.nPerson_typ = 1 Then
                'INICIO Dmendoza 18/08/2021
                '+ Se quita la validación de que sea obligatorio el DNI para personas naturales
                lclsCliDocumentss = New CliDocumentss
                lblnRequierd = False
                If lclsCliDocumentss.Find(sClient) Then
                    lblnCliDcoument = True
                    lblnRequierd = True
                    'For Each lclsCliDocuments In lclsCliDocumentss
                    '    If lclsCliDocuments.nTypClientDoc = 2 Then
                    '        'If lclsCliDocuments.nTypClientDoc = 1 Then
                    '        lblnRequierd = True
                    '        Exit For
                    '    End If
                    'Next lclsCliDocuments
                Else
                    lblnRequierd = False
                End If
                lclsCliDocumentss = Nothing
                'FIN Dmendoza 18/08/2021

                If DateDiff(Microsoft.VisualBasic.DateInterval.Year, lclsClient.dBirthdat, Today) >= 18 And lclsClient.dBirthdat <> eRemoteDB.Constants.dtmNull Then
                    If Not lblnRequierd Then
                        lstrContent = "3"
                        Call lclsClientWin.insUpdClient_win(sClient, "BC6000", "3", , , nUsercode)
                    Else
                        lstrContent = IIf(lblnCliDcoument, "2", "1")
                        Call lclsClientWin.insUpdClient_win(sClient, "BC6000", lstrContent)
                    End If
                Else
                    lstrContent = IIf(lblnCliDcoument, "2", "1")
                    Call lclsClientWin.insUpdClient_win(sClient, "BC6000", lstrContent)
                End If
            Else

                '+ Si es jurídico

                lclsTab_business = New Tab_business
                lclsAdress = New eGeneralForm.Addresss
                lclsCliDocumentss = New CliDocumentss

                With lclsTab_business
                    If lclsClient.nTypeCompany <> eRemoteDB.Constants.intNull Then
                        lintTypeCompany = lclsClient.nTypeCompany
                        lblnExist = False
                    Else
                        lintTypeCompany = nTypeCompany
                        lblnExist = True
                    End If
                    lblnRequierd = False
                    If lclsCliDocumentss.Find(sClient) Then
                        lblnCliDcoument = True
                        For Each lclsCliDocuments In lclsCliDocumentss
                            If lclsCliDocuments.nTypClientDoc = 1 Then
                                'If lclsCliDocuments.nTypClientDoc = 1 Then
                                lblnRequierd = True
                                Exit For
                            End If
                        Next lclsCliDocuments
                    Else
                        lblnRequierd = False
                        lblnCliDcoument = False
                    End If

                    If .Find(lintTypeCompany) Then
                        If lblnExist Then
                            If CDbl(.sRUC_ind) = 1 Then '+ Requiere RUC
                                If Not lblnRequierd Then
                                    lstrContent = "3"
                                Else
                                    lstrContent = "2"
                                End If
                            Else
                                lstrContent = "1"
                            End If
                        Else
                            If CDbl(.sRUC_ind) = 1 Then '+ Requiere RUC
                                If Not lblnRequierd Then
                                    lstrContent = "3"
                                Else
                                    lstrContent = "2"
                                End If
                            Else
                                lstrContent = "1"
                            End If
                        End If


                        If Not lblnRequierd Then
                            Call lclsClientWin.insUpdClient_win(sClient, "BC6000", lstrContent)
                        Else
                            lstrContent = IIf(lblnCliDcoument, "2", "1")
                            Call lclsClientWin.insUpdClient_win(sClient, "BC6000", lstrContent)
                        End If
                    Else
                        lstrContent = IIf(lblnCliDcoument, "2", "1")
                        Call lclsClientWin.insUpdClient_win(sClient, "BC6000", lstrContent)
                    End If
                End With

                lclsTab_business = Nothing
                lclsCliDocumentss = Nothing
                lclsAdress = Nothing
            End If
        End If
        insBC6000Content = lstrContent
        lclsClient = Nothing
        lclsClientWin = Nothing

        Exit Function
    End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%    sClient - Code of identification of the client
	'**%    nTypClientDoc - Code of the type of ID card of the client.
	'**%    sCliNumDocu - Code of the type of ID card of the client.
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%    sClient - Código de identificación del cliente
	'%    nTypClientDoc - Código del tipo de documento de identificación del cliente.
	'%    sCliNumDocu - Código del tipo de documento de identificación del cliente.
	Private Function IsExistDoc(ByVal sClient As String, ByVal nTypClientDoc As Short, ByVal sCliNumDocu As String) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		Dim lintExist As Short
		
        lclsCliDocuments = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valCliDocumentsExist'. Generated on 11/19/2004 3:04:01 PM
		
		With lclsCliDocuments
			.StoredProcedure = "reaTypeNumDoc"
			
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypClientDoc", nTypClientDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliNumDocu", sCliNumDocu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				IsExistDoc = (.Parameters("nExist").Value = 1)
			Else
				IsExistDoc = False
			End If
		End With
		
		lclsCliDocuments = Nothing
		
		Exit Function
	End Function
End Class











