Option Strict Off
Option Explicit On
Public Class Tar_prem_SOAP
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_prem_SOAP.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 16                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla en el sistema al 17/12/2001
    '+ Los campos llave corresponden a nVehType y dEffecdate
	
	'+ Name                   'Type                        Nullable
	'+ ---------------------- ---------------------------- --------
    Public nVehType As Integer 'Number(10)      No
    Public dEffecdate As Date 'Date            No
    Public dNulldate As Date 'Date            Yes
    Public nPremium As Double 'Number(18, 6)   Yes
    Public nUsercode As Integer 'Number(5)       No
	
    '% insValMSO008_K: se realizan las validaciones asociadas a las tarifas de prima de SOAP
    Public Function insValMSO008_K(ByVal sCodispl As String, ByVal deffecdate As Date) As String
        Dim lobjErrors As eFunctions.Errors
        On Error GoTo insValMSO008_K_Err
        lobjErrors = New eFunctions.Errors

        insValMSO008_K = String.Empty

        If dEffecdate = dtmNull Then
            lobjErrors.ErrorMessage(sCodispl, 10190)
        Else
            If dEffecdate <= Today Then
                lobjErrors.ErrorMessage(sCodispl, 10868)
            End If
        End If

        insValMSO008_K = lobjErrors.Confirm

insValMSO008_K_Err:
        If Err.Number Then
            insValMSO008_K = insValMSO008_K & Err.Description
        End If
        lobjErrors = Nothing
        On Error GoTo 0
    End Function

    '% insValMSO008: se realizan las validaciones asociadas a las tarifas de prima de SOAP
    Public Function insValMSO008(ByVal sAction As String, ByVal sCodispl As String, ByVal nVehType As Integer, ByVal dEffecdate As Date, ByVal nPremium As Double) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values

        lobjErrors = New eFunctions.Errors
        lclsValues = New eFunctions.Values

        With lobjErrors
            '+ Tipo de vehiculo
            If nVehType = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 13988)
            End If

            '+ Prima
            If nPremium = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 55614)
            End If

            If sAction = "Add" AndAlso Me.Find(nVehType, dEffecdate) Then
                Call lobjErrors.ErrorMessage(sCodispl, 11199)
            End If

            If sAction = "Update" AndAlso Me.FindStart(nVehType, dEffecdate) Then
                Call lobjErrors.ErrorMessage(sCodispl, 11199)
            End If

            insValMSO008 = .Confirm
        End With

    End Function

    '% insValMSO008: Se lee un registro de prima SOAP
    Private Function Find(ByVal nVehType As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecTar_prem_SOAP As eRemoteDB.Execute
        lrecTar_prem_SOAP = New eRemoteDB.Execute


        With lrecTar_prem_SOAP
            .StoredProcedure = "REATAR_PREM_SOAP_V"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nVehType = .FieldToClass("NVEHTYPE")
                Me.nPremium = .FieldToClass("NPREMIUM")
                Find = True
                .RCloseRec()
            End If
        End With

    End Function

    '% insValMSO008: Se lee un registro de prima SOAP
    Private Function FindStart(ByVal nVehType As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecTar_prem_SOAP As eRemoteDB.Execute
        lrecTar_prem_SOAP = New eRemoteDB.Execute


        With lrecTar_prem_SOAP
            .StoredProcedure = "REATAR_PREM_SOAP_D"
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.nVehType = .FieldToClass("NVEHTYPE")
                Me.nPremium = .FieldToClass("NPREMIUM")
                FindStart = True
                .RCloseRec()
            End If
        End With

    End Function


    '% Add: se agrega un registro a la tabla
	Private Function Add() As Boolean
		Add = Update(1)
	End Function
	
	'% Delete: se elimina un registro de la tabla
	Private Function Delete() As Boolean
		Delete = Update(3)
	End Function
	
	'% Update: se realiza la actualización de la tabla
	Private Function Update(ByVal nAction As Integer) As Boolean
		Dim lrecTar_prem_SOAP As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecTar_prem_SOAP = New eRemoteDB.Execute
		
        With lrecTar_prem_SOAP
            .StoredProcedure = "insUpdTar_prem_SOAP"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
        lrecTar_prem_SOAP = Nothing
	End Function
	

    '% insPostMSO008Upd: se realizan las actualizaciones sobre la tabla
    Public Function insPostMSO008Upd(ByVal sAction As String, ByVal dEffecdate As Date, ByVal nVehType As Integer, Optional ByVal nPremium As Double = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
        On Error GoTo insPostMSO008_err

        With Me
            .dEffecdate = dEffecdate
            .nVehType = nVehType
            .nPremium = nPremium
            .nUsercode = nUsercode
        End With

        Select Case sAction
            Case "Add"
                insPostMSO008Upd = Add()
            Case "Update"
                insPostMSO008Upd = Update(2)
            Case "Del"
                insPostMSO008Upd = Delete()
        End Select

insPostMSO008_err:
        If Err.Number Then
            insPostMSO008Upd = False
        End If
        On Error GoTo 0
    End Function
	
	'* Class_Initialize: se controla el acceso a la clase
    Private Sub Class_Initialize_Renamed()
        nVehType = eRemoteDB.Constants.intNull
        dEffecdate = dtmNull
        nPremium = eRemoteDB.Constants.dblNull
        dNulldate = dtmNull
        nUsercode = eRemoteDB.Constants.intNull
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class
