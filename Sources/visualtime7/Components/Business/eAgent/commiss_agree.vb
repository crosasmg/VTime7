Option Strict Off
Option Explicit On
Public Class commiss_agree
	'%-------------------------------------------------------%'
	'% $Workfile:: commiss_agree.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'commiss_agree' en el sistema 11/12/2001 05:32:25 p.m.
	
	Public sClient As String
	Public nAgreement As Integer
	Public dInit_Date As Date
	Public dEnd_Date As Date
	Public nPerc_Comm As Double
	
	'% Add: Añade todos los valores relacionados con un registro específico
	Public Function Add(ByVal nUsercode As Integer, ByVal sClient As String, ByVal nAgreement As Integer, ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nPerc_Comm As Double) As Boolean
		Dim lclscommiss_agree As eRemoteDB.Execute
		
		lclscommiss_agree = New eRemoteDB.Execute
		
		'+ Definición de los parámetros del stored procedure 'insudb.crecommiss_agree'. Data generada el 12/11/2001 05:32:25 p.m.
		
		With lclscommiss_agree
			.StoredProcedure = "crecommiss_agree"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_Date", dInit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_Date", dEnd_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerc_Comm", nPerc_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Add = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclscommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscommiss_agree = Nothing
	End Function
	
	'% Update: Actualiza los datos relacionados con un cliente específico
	Public Function Update(ByVal nUsercode As Integer, ByVal sClient As String, ByVal nAgreement As Integer, ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nPerc_Comm As Double) As Boolean
		Dim lclscommiss_agree As eRemoteDB.Execute
		
		lclscommiss_agree = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.updcommiss_agree'. Generated on 11/12/2001 05:32:25 p.m.
		With lclscommiss_agree
			.StoredProcedure = "updcommiss_agree"
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_Date", dInit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_Date", dEnd_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPerc_Comm", nPerc_Comm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclscommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscommiss_agree = Nothing
	End Function
	
	'%Delete: Elimina la información correspondiente a un cliente, año y conceptos específicos
	Public Function Delete(ByVal sClient As String, ByVal nAgreement As Integer) As Boolean
		Dim lclscommiss_agree As eRemoteDB.Execute
		
		lclscommiss_agree = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.delcommiss_agree'. Generated on 11/12/2001 05:32:25 p.m.
		With lclscommiss_agree
			.StoredProcedure = "delcommiss_agree"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lclscommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscommiss_agree = Nothing
	End Function
	
	'Find: Función que realiza la busqueda en la tabla 'insudb.commiss_agree'
	Public Function Find(ByVal sClient As String, ByVal nAgreement As Integer) As Boolean
        Dim lclscommiss_agree As eRemoteDB.Execute

        lclscommiss_agree = New eRemoteDB.Execute
		
		'+ Definición de parámetros del stored procedure 'insudb.valcommiss_agreeExist'. Generado el 12/11/2001 05:32:25 p.m.
		With lclscommiss_agree
			.StoredProcedure = "REACOMMISS_AGREE_V"
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Find = True
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lclscommiss_agree may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclscommiss_agree = Nothing
	End Function
	
	'insValMAG554: Función que realiza la validacion de los datos introducidos en la sección
	'              de detalles de la ventana
	Public Function insValMAG554(ByVal sCodispl As String, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sClient As String, ByVal nAgreement As Integer, ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nPerc_Comm As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsIntermedia As eAgent.Intermedia
		Dim lclsValField As eFunctions.valField
		
		On Error GoTo insValMAG554_Err
		
		lclsErrors = New eFunctions.Errors
		lclsIntermedia = New eAgent.Intermedia
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		
		
		If sAction = "Add" Then
			'+ Rut debe estar lleno
			If sClient = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 55579)
			Else
				'+ Rut Debe estar registrado como un intermediario válido
				If Not lclsIntermedia.FindTypeInterm_Client(sClient, 2) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55580)
				Else
					If lclsIntermedia.nInt_status = 2 Or lclsIntermedia.nInt_status = 4 Then
						Call lclsErrors.ErrorMessage(sCodispl, 55580)
					End If
				End If
			End If
			
			'+ Convenio debe estar lleno
			If (nAgreement = 0 Or nAgreement = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55004)
			Else
				'+ Rut debe ser único
				If Find(sClient, nAgreement) Then
					Call lclsErrors.ErrorMessage(sCodispl, 55022)
				End If
			End If
		End If
		
		'+ Fecha inicial debe estar lleno
		If dInit_Date = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9071)
		End If
		
		'+ Fecha final debe estar lleno
		If dEnd_Date = dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9072)
		End If
		
		'+ Si la fecha inicial y final estan llenos, la fecha final debe ser porterior a la inicial
		If dInit_Date <> dtmNull And dEnd_Date <> dtmNull And dEnd_Date < dInit_Date Then
			Call lclsErrors.ErrorMessage(sCodispl, 55006)
		End If
		
		'+ %Comisión debe estar lleno
		If (nPerc_Comm = 0 Or nPerc_Comm = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 21117)
		Else
			lclsValField.Min = 0.01
			lclsValField.Max = 100#
			lclsValField.Descript = "% de Comisión"
			lclsValField.ErrRange = 11239
			lclsValField.ValNumber(nPerc_Comm)
		End If
		
		insValMAG554 = lclsErrors.Confirm
		
insValMAG554_Err: 
		If Err.Number Then
			insValMAG554 = "insValMAG554: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsIntermedia = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
	End Function
	
	'insPostMAG554: Función que añade la información a la base de datos
	Public Function insPostMAG554(ByVal bHeader As Boolean, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sClient As String, ByVal nAgreement As Integer, ByVal dInit_Date As Date, ByVal dEnd_Date As Date, ByVal nPerc_Comm As Double) As Boolean
		
		On Error GoTo insPostMAG554_err
		If bHeader Then
			insPostMAG554 = True
		Else
			If sAction = "Add" Then
				insPostMAG554 = Add(nUsercode, sClient, nAgreement, dInit_Date, dEnd_Date, nPerc_Comm)
			ElseIf sAction = "Update" Then 
				insPostMAG554 = Update(nUsercode, sClient, nAgreement, dInit_Date, dEnd_Date, nPerc_Comm)
			ElseIf sAction = "Del" Then 
				insPostMAG554 = Delete(sClient, nAgreement)
			End If
		End If
		
insPostMAG554_err: 
		If Err.Number Then
			insPostMAG554 = False
		End If
		On Error GoTo 0
		
	End Function
End Class






