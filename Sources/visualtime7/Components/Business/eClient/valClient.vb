Option Strict Off
Option Explicit On
Imports System.Configuration
Public Class ValClient
    '%-------------------------------------------------------%'
    '% $Workfile:: valClient.cls                            $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:30p                                $%'
    '% $Revision:: 13                                       $%'
    '%-------------------------------------------------------%'

    '- Se codifican las constantes
    Public Enum eTypeValClientErr
        FieldEmpty
        TypeNotFound
        StructInvalid
        IsNotNumeric
        FieldNotFound
        FieldNew

    End Enum

    '- Se codifican las constantes
    Public Enum eTypeClientCode
        Alphanumeric = 1
        Numeric = 2
    End Enum

    '- Variable del tipo de enumeracion
    Private mvarStatus As eTypeValClientErr

    '- Variable codigo del cliente
    Private mstrClientCode As String

    '- Variable nombre del cliente
    Private mstrClientName As String

    '- Variable tipo del cliente
    Private mvarClientType As Client.eClientType

    '- Variable temporal
    Private mblnTemporal As Boolean

    '- Variable de digito verificador
    Public sDigit As String

    ''**-Objective:It indicates whether or not the validation 1007 is sent.
    ''-Objetivo: Indica si se envia la validación 1007

    Public sSendValGenNum As String

    '**-Objective:It indicates the type of client
    '-Objetivo: Indica el tipo de cliente

    Public sTypeClient As String

    '**- Objective: Client code
    '- Objetivo: Código del cliente

    Public mvarClientCode As String

    '**-Objective:It indicates if the client exists
    '-Objetivo: Indica si el cliente existe

    Public bClientExist As Boolean

    '**-Objective: Type of client.
    '-Objetivo: Tipo de cliente.

    Public sType As String

    '% Temporal: manejo de retorno de varible temporal
    '-----------------------------------------------------------
    Public ReadOnly Property Temporal() As Boolean
        Get
            '-----------------------------------------------------------
            Temporal = mblnTemporal
        End Get
    End Property

    '% ClientType: manejo de retorno de varible tipo de cliente
    '-----------------------------------------------------------
    Public ReadOnly Property ClientType() As Integer
        Get
            '-----------------------------------------------------------
            ClientType = mvarClientType
        End Get
    End Property

    '% ClientCode: manejo de retorno de varible codigo del cliente
    '-----------------------------------------------------------
    Public ReadOnly Property ClientCode() As String
        Get
            '-----------------------------------------------------------
            ClientCode = mstrClientCode
        End Get
    End Property

    '% sCliename: manejo de retorno de varible nombre del cliente
    '-----------------------------------------------------------
    Public ReadOnly Property sCliename() As String
        Get
            '-----------------------------------------------------------
            sCliename = mstrClientName
        End Get
    End Property

    '% Status: manejo de retorno de varible tipo de enumeracion
    '-----------------------------------------------------------
    Public ReadOnly Property Status() As eTypeValClientErr
        Get
            '-----------------------------------------------------------
            Status = mvarStatus
        End Get
    End Property

    '% Validate: se verifican los datos del cliente
    Public Function Validate(ByVal sCodClient As String, ByVal nAction As eFunctions.Menues.TypeActions, Optional ByVal bFind As Boolean = True, Optional ByVal bAllowInvalidFormat As Boolean = False) As Boolean
        Dim lclsClient As eClient.Client

        '-Se define la variable encargada de indicar si hubo algun error para algun punto de la
        '-validación
        Dim lblnErr As Boolean
        Dim sClientExist As Boolean
        '- Se define la variable temporal para justificar el código del cliente
        Dim lstrVarAux As String

        Dim lstrFirstChar As String

        On Error GoTo Validate_err

        lstrFirstChar = UCase(Mid(sCodClient, 1, 1))

        '+ Validaciones del dígito verificador
        '+ Debe estar lleno
        If ConfigurationManager.AppSettings("UseClientDigit.Enable") = "True" Or GetTypeClientCode(sCodClient, lstrFirstChar) = eTypeClientCode.Numeric Then
            mvarStatus = 0
            mvarClientType = -1
            mstrClientCode = String.Empty
            mstrClientName = String.Empty
            lstrVarAux = Trim(sCodClient)

            Validate = True

            If Len(lstrVarAux) Then
                lclsClient = New eClient.Client
                If IsNumeric(lstrVarAux) Then
                    If CDbl(lstrVarAux) > 0 Then
                        lstrVarAux = lclsClient.ExpandCode(lstrVarAux)
                        mstrClientCode = lstrVarAux
                        mvarClientCode = mstrClientCode
                        If bFind Then
                            If lclsClient.Find(lstrVarAux) Then
                                mstrClientName = lclsClient.sCliename
                                Select Case lclsClient.nPerson_typ
                                    Case 1
                                        mvarClientType = Client.eClientType.ctPerson
                                    Case 2
                                        mvarClientType = Client.eClientType.ctCompany
                                    Case Else
                                        mvarStatus = eTypeValClientErr.TypeNotFound
                                        lblnErr = True
                                End Select
                                sDigit = lclsClient.sDigit
                            Else
                                mvarStatus = eTypeValClientErr.FieldNotFound
                                lblnErr = True
                            End If
                        End If
                    Else
                        mvarStatus = eTypeValClientErr.StructInvalid
                        lblnErr = True
                    End If
                Else
                    '+Si la acción es registrar.
                    If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                        '+Si el código corresponde con la letra "E"; indica que se desea generar un nuevo cliente automáticamente.
                        If UCase(Trim(lstrVarAux)) = "E" Then
                            mstrClientCode = lclsClient.GetNewClientCode
                            mvarStatus = eTypeValClientErr.FieldNew
                        Else
                            lblnErr = True
                            mvarStatus = eTypeValClientErr.IsNotNumeric
                        End If
                    Else
                        lblnErr = True
                        mvarStatus = eTypeValClientErr.IsNotNumeric
                    End If
                End If
            Else
                If ConfigurationManager.AppSettings("UseClientCodeWhitoutLetter.Enable") = "True" Then
                    mvarStatus = eTypeValClientErr.FieldNew
                Else
                    mvarStatus = eTypeValClientErr.FieldEmpty
                    lblnErr = True
                End If
            End If

            Validate = Not lblnErr
            bClientExist = Validate
        Else

            lclsClient = New Client

            mvarStatus = 0
            mvarClientCode = String.Empty
            lstrVarAux = Trim(Mid(sCodClient, 2))


            With lclsClient
                mvarClientCode = .ExpandCode(lstrFirstChar & lstrVarAux)
                sClientExist = lclsClient.Find(mvarClientCode)
                If Not sClientExist Then
                    'If .sNatural <> String.Empty And .sNatural <> "3" Then
                    'mvarClientType = IIf(.sNatural = "1", 1, 2)
                    'Else
                    '   mvarClientType = "0"
                    'End If
                    'If .sValidateformat = "2" Then
                    'mvarStatus = eTypeValClientErr.StructInvalid
                    'lblnErr = True

                    'ElseIf .sNatural = String.Empty Then
                    '   mvarStatus = eTypeValClientErr.TypeNotFound
                    '  lblnErr = True

                    If Trim(mvarClientCode) = String.Empty Then
                        mvarStatus = eTypeValClientErr.FieldEmpty
                    End If
                    If Not ConfigurationManager.AppSettings("TemporaryFirstLetter") = eRemoteDB.Constants.strNull Then
                        If lstrFirstChar <> ConfigurationManager.AppSettings("TemporaryFirstLetter") Then
                            mvarStatus = eTypeValClientErr.TypeNotFound
                            lblnErr = True
                        End If
                    End If
                    mstrClientName = Trim(.sCliename)
                    bClientExist = False
                    '       sSendValGenNum = .sSendValGenNum
                    sTypeClient = .sCodClientType
                Else
                    'Select Case .nPerson_typ
                    'Case 1
                    '     mvarClientType = Client.eClientType.ctPerson
                    '  Case 2
                    '       mvarClientType = Client.eClientType.ctCompany
                    'End Select
                    mstrClientName = Trim(.sCliename)
                    bClientExist = True
                    'sSendValGenNum = .sSendValGenNum
                    sTypeClient = .sCodClientType
                    lblnErr = False
                End If
                ' ''sSmoking = .sSmoking
                ' ''dBirthdat = .dBirthdat
                ' ''sSexclien = .sSexclien
            End With

            Validate = Not lblnErr

            lclsClient = Nothing
        End If

Validate_err:
        If Err.Number Then
            Validate = False
        End If
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
        On Error GoTo 0
    End Function

    '%Objetivo:
    '%Parámetros:
    '%    sClient   - Código que identifica al cliente.
    '%    sCodispl  - Código identificativo de la ventana (lógico).
    Public Function FindClientNatProv(ByVal sNatural As String, ByVal sProvision As String) As Boolean
        Dim lrecClient As eRemoteDB.Execute

        lrecClient = New eRemoteDB.Execute

        With lrecClient
            .StoredProcedure = "REACLIENTNATPROV"
            .Parameters.Add("sNatural", sNatural, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProvision", sProvision, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sType", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                FindClientNatProv = .Parameters("nExist").Value = 1
                sType = .Parameters("sType").Value
            End If
        End With

        lrecClient = Nothing
    End Function

    Public Function GetTypeClientCode(Optional ByVal sClient As String = eRemoteDB.Constants.strNull, Optional ByVal sFirstCharacter As String = eRemoteDB.Constants.strNull) As eTypeClientCode
        Dim rx As New System.Text.RegularExpressions.Regex("^[A-Z a-z %]*$")
        If sClient <> eRemoteDB.Constants.strNull Or sFirstCharacter <> eRemoteDB.Constants.strNull Then
            If sFirstCharacter = eRemoteDB.Constants.strNull Then
                sFirstCharacter = UCase(Mid(sClient, 1, 1))
            End If
        End If
        If ConfigurationManager.AppSettings("UseClientCodeWhitoutLetter.Enable") = "False" Then
            GetTypeClientCode = eTypeClientCode.Alphanumeric
        ElseIf ConfigurationManager.AppSettings("UseClientCodeWhitoutLetter.Enable") = "True" And (sFirstCharacter <> eRemoteDB.Constants.strNull And rx.IsMatch(sFirstCharacter)) Then
            GetTypeClientCode = eTypeClientCode.Alphanumeric
        Else
            GetTypeClientCode = eTypeClientCode.Numeric
        End If
    End Function
End Class











