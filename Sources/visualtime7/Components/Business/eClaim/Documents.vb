Option Strict Off
Option Explicit On
Public Class Documents
	'%-------------------------------------------------------%'
	'% $Workfile:: Documents.cls                            $%'
	'% $Author:: Jrengifo                                   $%'
	'% $Date:: 2-05-13 9:19                                 $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'Column_name                        Type      Length      Prec  Scale Nullable
	Public nClaim As Double 'int       4           10    0     no
	Public nDoc_code As Integer 'smallint  2           5     0     no
	Public nCase_num As Integer 'smallint  2           5     0     no
	Public nDeman_type As Integer 'smallint  2           5     0     no
	Public sClient As String 'char      14                      no
	Public dEffecdate As Date 'datetime  8                       yes
	Public dLetfirsdat As Date 'datetime  8                       yes
	Public dLetlastdat As Date 'datetime  8                       yes
	Public dRecepdate As Date 'datetime  8                       yes
	Public nStatus_doc As Integer
	Public nUserCode As Integer 'smallint  2           5     0     yes
	Public nId As Integer
	Public nDocnumbe As Double
	Public nQuantity As Integer
	Public dPropo_date As Date
	Public dPrescDate As Date
	
	'**-Auxiliaries properties
	'- Propiedades auxiliares
	Public nDays_presc As Integer
	Public nCode As Integer
	Public nAction As Integer
	Public sDesc_docu As String
	Public sDescript As String
    Public nConsec As Short
    Public Property nAmount As Double
    Public Property nCurrency As Integer


	
	
	'**%Update_DocumentsGeneric: In case of a claim it updates de claim documents
	'% Update_DocumentsGeneric: Realiza las actualizaciones de los documentos de un caso del siniestro
    Public Function Update_DocumentsGeneric(ByVal nClaim As Double,
                                            ByVal nDoc_code As Integer,
                                            ByVal nCase_num As Integer,
                                            ByVal nDeman_type As Integer,
                                            ByVal sClient As String,
                                            ByVal nId As Integer,
                                            ByVal dRecepdate As Date,
                                            ByVal nDocnumbe As Double,
                                            ByVal nQuantity As Integer,
                                            ByVal dPropo_date As Date,
                                            ByVal dPrescDate As Date,
                                            ByVal nAction As Integer,
                                            ByVal nUserCode As Integer,
                                            ByVal sDesc_docu As String,
                                            ByVal nConsec As Short,
                                            Optional ByVal nAmount As Double = eRemoteDB.Constants.dblNull,
                                            Optional ByVal nCurrency As Integer = eRemoteDB.Constants.intNull) As Boolean
        Dim lrecupdDocuments As eRemoteDB.Execute

        On Error GoTo Update_DocumentsGeneric_Err

        lrecupdDocuments = New eRemoteDB.Execute

        With lrecupdDocuments
            .StoredProcedure = "insDocuments"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDoc_code", nDoc_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dRecepdate", dRecepdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocnumbe", nDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuantity", nQuantity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPropo_date", dPropo_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPrescdate", dPrescDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDesc_docu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 45, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nConsec", nConsec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update_DocumentsGeneric = .Run(False)
        End With

        lrecupdDocuments = Nothing

Update_DocumentsGeneric_Err:
        If Err.Number Then
            Update_DocumentsGeneric = False
        End If
        On Error GoTo 0
    End Function
	
	'%FindDocs_by_status:
	'%Esta Función se encarga de leer los registros de la tabla documents de acuerdo a un estado
	Public Function FindDocs_by_status(ByVal Claim As Double, ByVal Case_num As Integer, ByVal Deman_type As Integer, ByVal Status_doc As Integer, ByVal Effecdate As Date, ByVal Client As String) As Boolean
		Dim lrecinsReaCl_DocumentsPendients As eRemoteDB.Execute
		
		
		On Error GoTo FindDocs_by_status_Err
		FindDocs_by_status = False
		lrecinsReaCl_DocumentsPendients = New eRemoteDB.Execute
		
		'**Parameters definition for the stored procedure 'insudb.insReaC1_DocumentsPendients'
		'Definición de parámetros para stored procedure 'insudb.insReaCl_DocumentsPendients'
		'**Data read on 01/30/2001 10:57:13 AM
		'Información leída el 30/01/2001 10:57:13 AM
		With lrecinsReaCl_DocumentsPendients
			.StoredProcedure = "insReaCl_DocumentsPendients"
			.Parameters.Add("nClaim", Claim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", Case_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", Deman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_Doc", Status_doc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", Effecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindDocs_by_status = True
				nStatus_doc = .FieldToClass("nStatus_doc")
				sClient = .FieldToClass("sClient")
				dEffecdate = .FieldToClass("dEffecdate")
				dLetfirsdat = .FieldToClass("dLetfirsdat")
				dLetlastdat = .FieldToClass("dLetlastdat")
				dRecepdate = .FieldToClass("dRecepdate")
				nId = .FieldToClass("nId")
				nDocnumbe = .FieldToClass("nDocnumbe")
				nQuantity = .FieldToClass("nQuantity")
				dPropo_date = .FieldToClass("dPropo_date")
				dPrescDate = .FieldToClass("dPrescdate")
				.RCloseRec()
			End If
		End With
		lrecinsReaCl_DocumentsPendients = Nothing
		
FindDocs_by_status_Err: 
		If Err.Number Then
			FindDocs_by_status = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValSI015: This function is in charge of making the validations in the window of request apply
	'%insValSI015: Esta funcion se encarga de realizar las validaciones de la ventana de
	'%             solicitud de recaudos
	Public Function insValSI015(ByVal bEmptyTable As Boolean) As String
		Dim lerrTime As eFunctions.Errors
		
		On Error GoTo insValSI015_Err
		
		lerrTime = New eFunctions.Errors
		
		If bEmptyTable Then
			Call lerrTime.ErrorMessage("SI015", 4281)
		End If
		
		insValSI015 = lerrTime.Confirm
		
		lerrTime = Nothing
		
insValSI015_Err: 
		If Err.Number Then
			insValSI015 = "insValSI015: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**% insValSI015Upd: This function is in charge of maiking the window validations of the request apply
	'% insValSI015Upd: Esta funcion se encarga de realizar las validaciones de la ventana de
	'%                 solicitud de recaudos
	Public Function insValSI015Upd(ByVal sCodispl As String, ByVal dRecepdate As Date, ByVal dPropo_date As Date, ByVal dDatePresc As Date, ByVal nClaimNumber As Double) As String
		Dim lerrTime As eFunctions.Errors
		Dim lclsClaim As New Claim
		
		On Error GoTo insValSI015_Err
		
		lerrTime = New eFunctions.Errors
		
		
		'+Se valida que la fecha de solicitud no este en null
		
		If dPropo_date = eRemoteDB.Constants.dtmNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60443)
		Else
			'**+It validates the date of the documents reception
			'+Se valida la fecha de recepción de documentos
			If dRecepdate <> eRemoteDB.Constants.dtmNull Then
				If dRecepdate < dPropo_date Then
					Call lerrTime.ErrorMessage(sCodispl, 4071)
				Else
					If dRecepdate > Today Then
						Call lerrTime.ErrorMessage(sCodispl, 4072)
					End If
				End If
			End If
			If lclsClaim.Find(nClaimNumber) Then
				If dPropo_date < lclsClaim.dDecladat Then
					Call lerrTime.ErrorMessage(sCodispl, 60500)
				End If
			End If
		End If
		
		'Se valida que la fecha límite no este en null
		
		If dDatePresc = eRemoteDB.Constants.dtmNull Then
			Call lerrTime.ErrorMessage(sCodispl, 60446)
		Else
			'Se valida que la fecha limite no sea menor que la fecha de solicitud
			If dPropo_date > dDatePresc Then
				Call lerrTime.ErrorMessage(sCodispl, 60447)
			End If
		End If
		
		insValSI015Upd = lerrTime.Confirm
		
insValSI015_Err: 
		If Err.Number Then
			insValSI015Upd = "insValSI015Upd: " & Err.Description
		End If
		
		lerrTime = Nothing
		lclsClaim = Nothing
		
		On Error GoTo 0
		
	End Function
	
	'%insPostSI015: Esta función se encarga de validar los datos introducidos en la zona de
	'%contenido para "frame" especifico.
    Public Function insPostSI015(ByVal nClaim As Double,
                                 ByVal nCase_num As Integer,
                                 ByVal nDeman_type As Integer,
                                 ByVal sClient As String,
                                 ByVal sAction As String,
                                 ByVal nCode As Integer,
                                 ByVal nDoc_code As Integer,
                                 ByVal nUserCode As Integer,
                                 ByVal nId As Integer,
                                 ByVal nDocnumbe As Double,
                                 ByVal nQuantity As Integer,
                                 ByVal dPropo_date As Date,
                                 ByVal dPrescDate As Date,
                                 Optional ByVal dRecepdate As Date = #12:00:00 AM#,
                                 Optional ByVal nBranch As Integer = 0,
                                 Optional ByVal sDesc_docu As String = "",
                                 Optional ByVal nConsec As Short = 0,
                                 Optional ByVal nAmount As Double = eRemoteDB.Constants.dblNull,
                                 Optional ByVal nCurrency As Integer = eRemoteDB.Constants.intNull) As Boolean

        On Error GoTo insPostSI015_Err

        Select Case sAction
            Case "Add"
                nAction = 1
            Case "Update"
                If nDoc_code = 0 Or nDoc_code = eRemoteDB.Constants.intNull Then
                    nAction = 1
                Else
                    nAction = 2
                End If
            Case "Delete", "Del"
                nAction = 3
        End Select
        If nAction > 0 Then
            insPostSI015 = Update_DocumentsGeneric(nClaim, nCode, nCase_num, nDeman_type, sClient, nId, dRecepdate, nDocnumbe, nQuantity, dPropo_date, dPrescDate, nAction, nUserCode, sDesc_docu, nConsec, nAmount, nCurrency)
        End If

insPostSI015_Err:
        If Err.Number Then
            insPostSI015 = False
        End If
        On Error GoTo 0
    End Function
	'% '**%Find_DocumentPrescDate: Realiza el cálculo de la fecha límite de recepción de un documento
	Public Function Find_DocumentPrescDate(ByVal nClaim As Double, ByVal dPropo_date As Date, ByVal nDays_presc As Integer) As Date
		Dim lclsClaim As eClaim.Claim
		If nDays_presc = eRemoteDB.Constants.intNull Or nDays_presc = 0 Then
			lclsClaim = New eClaim.Claim
			If lclsClaim.Find(nClaim) Then
				Find_DocumentPrescDate = lclsClaim.dPrescdat
			Else
				Find_DocumentPrescDate = eRemoteDB.Constants.dtmNull
			End If
			lclsClaim = Nothing
		Else
			Find_DocumentPrescDate = System.Date.FromOADate(dPropo_date.ToOADate + nDays_presc)
		End If
	End Function
End Class






