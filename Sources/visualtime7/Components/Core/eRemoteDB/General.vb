Option Strict Off
Option Explicit On

Module General
    Public Enum ENUM_MODULE_DB
        APLICATION_DB = 1
        ERRORS_DB = 2
        IMAGES_DB = 3
        FILES_DB = 4
    End Enum


    '**+Objective:
    '**+Version: $$Revision: $
    '+Objetivo:
    '+Version: $$Revision: $

    '**%Objective:
    '**%Parameters:
    '**%    sFieldName     -
    '**%    nType          -
    '**%    vValue         -
    '**%    vDefValue      -
    '**%    bNotDriveError -
    '**%    nErrorNum      -
    '**%    sErrorMsg      -
    '%Objetivo:
    '%Parámetros:
    '%      sFieldName     -
    '%      nType          -
    '%      vValue         -
    '%      vDefValue      -
    '%      bNotDriveError -
    '%      nErrorNum      -
    '%      sErrorMsg      -
    Public Function RmtFieldToClass(ByVal sFieldName As String, ByVal nType As Integer, ByVal vValue As Object, Optional ByVal vDefValue As Object = Nothing, Optional ByVal bNotDriveError As Object = False, Optional ByRef nErrorNum As Integer = 0, Optional ByRef sErrorMsg As String = "", Optional ByVal DoDecrypt As Boolean = False) As Object
        ''On Error GoTo ErrorHandler

        RmtFieldToClass = Nothing

        If Not IsDBNull(vValue) And Not IsNothing(vValue) Then

            Select Case nType
                Case Parameter.eRmtDataType.rdbSmallInt, Parameter.eRmtDataType.rdbInteger, Parameter.eRmtDataType.rdbNumeric
                    Select Case nType
                        Case Parameter.eRmtDataType.rdbSmallInt
                            RmtFieldToClass = CShort(vValue)
                        Case Parameter.eRmtDataType.rdbInteger
                            RmtFieldToClass = CInt(vValue)
                        Case Parameter.eRmtDataType.rdbNumeric
                            RmtFieldToClass = CDec(vValue)
                        Case Else
                            RmtFieldToClass = CSng(vValue)
                            Debug.Print("Type not found " & CStr(nType))
                    End Select

                Case Parameter.eRmtDataType.rdbChar

                    If vValue.ToString = String.Empty And Not (IsNothing(vDefValue)) Then
                        RmtFieldToClass = vDefValue.ToString.Trim
                    Else
                        RmtFieldToClass = vValue.ToString.Trim
                    End If

                Case Parameter.eRmtDataType.rdbVarchar

                    If vValue = String.Empty And Not (IsNothing(vDefValue)) Then
                        RmtFieldToClass = vDefValue
                    Else
                        RmtFieldToClass = Trim(vValue)
                    End If

                Case Parameter.eRmtDataType.rdbDBTimeStamp
                    RmtFieldToClass = Convert.ToDateTime(vValue).ToShortDateString()

                Case Else
                    RmtFieldToClass = vValue

            End Select


        ElseIf Not IsNothing(vDefValue) Then
            RmtFieldToClass = vDefValue

        Else
            Select Case nType
                Case Parameter.eRmtDataType.rdbSmallInt, Parameter.eRmtDataType.rdbInteger
                    RmtFieldToClass = intNull

                Case Parameter.eRmtDataType.rdbNumeric
                    RmtFieldToClass = intNull 'dblNull

                Case Parameter.eRmtDataType.rdbChar
                    If LCase(Left(sFieldName, 1)) = "n" Then
                        RmtFieldToClass = dblNull
                    Else
                        RmtFieldToClass = strNull
                    End If

                Case Parameter.eRmtDataType.rdbDBTimeStamp
                    RmtFieldToClass = dtmNull

                Case Parameter.eRmtDataType.rdbVarchar
                    RmtFieldToClass = strNull

                Case Else
                    Debug.Print("Type not found " & CStr(nType))
                    Select Case LCase(Mid(sFieldName, 1, 1))
                        Case "s"
                            RmtFieldToClass = strNull
                        Case "n"
                            RmtFieldToClass = intNull 'dblNull
                        Case "d"
                            RmtFieldToClass = dtmNull
                    End Select
            End Select
        End If

        If DoDecrypt Then
            RmtFieldToClass = CryptSupport.DecryptString(RmtFieldToClass)
        End If

        Exit Function
ErrorHandler:
        ProcError("General.RmtFieldToClass(sFieldName,nType,vValue,vDefValue)", New Object() {sFieldName, nType, vValue, vDefValue}, , , , bNotDriveError, nErrorNum, sErrorMsg)
    End Function

    '% GetConnectInfo: Recupera la información de la coneccion a la bd
    '--------------------------------------------------------------------------------------------
    Public Sub GetConnectInfo(ByVal nModule As ENUM_MODULE_DB, _
                              ByRef sLogin As String, _
                              ByRef sPassword As String, _
                              ByRef sSesionID As String, _
                              ByRef sDatabase As String, _
                              ByRef sDSN As String, _
                              ByRef bOracleTrace As Boolean)
        '--------------------------------------------------------------------------------------------
        Dim clsASPSupport As eRemoteDB.ASPSupport
        Dim clsConfig As VisualTimeConfig

        On Error Resume Next

        clsASPSupport = New eRemoteDB.ASPSupport
        clsConfig = New VisualTimeConfig

        With clsASPSupport
            If nModule = ENUM_MODULE_DB.ERRORS_DB Then
                sLogin = clsConfig.LoadSetting("sInitials", "insudb", "ErrorSystem")
                sPassword = CryptSupport.DecryptString(clsConfig.LoadSetting("sAccessWo", "NYÈ¿íÝ", "ErrorSystem"))
            Else
                If nModule = ENUM_MODULE_DB.IMAGES_DB Then
                    sLogin = clsConfig.LoadSetting("sInitials", "insudb", "ImagesDB")
                    sPassword = CryptSupport.DecryptString(clsConfig.LoadSetting("sAccessWo", "NYÈ¿íÝ", "ImagesDB"))
                Else
                    If nModule = ENUM_MODULE_DB.FILES_DB Then
                        sLogin = clsConfig.LoadSetting("sInitials", "sp_app", "VirtualOffice")
                        sPassword = CryptSupport.DecryptString(clsConfig.LoadSetting("sAccessWo", "08B12DC5D118", "VirtualOffice"))
                    Else
                        If (clsConfig.LoadSetting("MultiCompany", "2", "Database") = 1) Then
                            sLogin = clsASPSupport.GetASPSessionValue("sInitialsCon")
                            sPassword = CryptSupport.DecryptString(clsASPSupport.GetASPSessionValue("sAccessWoCon"))
                        Else
                            sLogin = clsConfig.LoadSetting("sInitialscon", "insudb", "Database")
                            sPassword = CryptSupport.DecryptString(clsConfig.LoadSetting("sAccessWocon", "NYÈ¿íÝ", "Database"))
                        End If
                    End If
                End If
            End If
            sSesionID = clsASPSupport.SessionID
        End With

        If nModule = ENUM_MODULE_DB.ERRORS_DB Then
            sDSN = clsConfig.LoadSetting("ConnectionString", "Provider=MSDAORA.1;Data Source=TIME;OLE DB Services=-1", "ErrorSystem")
            sDatabase = clsConfig.LoadSetting("DataBase", "TIME", "ErrorSystem")
        Else
            If nModule = ENUM_MODULE_DB.IMAGES_DB Then
                sDSN = clsConfig.LoadSetting("ConnectionString", "Provider=OraOLEDB.Oracle.1;Data Source=TIME;Persist Security Info=True", "ImagesDB")
                sDatabase = clsConfig.LoadSetting("DataBase", "TIME", "ImagesDB")
            Else
                If nModule = ENUM_MODULE_DB.FILES_DB Then
                    sDSN = clsConfig.LoadSetting("ConnectionString", "Provider=OraOLEDB.Oracle.1;Data Source=TIME;Persist Security Info=True", "VirtualOffice")
                    sDatabase = clsConfig.LoadSetting("DataBase", "VIRTUALOFFICE", "VirtualOffice")
                Else
                    sDSN = clsConfig.LoadSetting("ConnectionString", "Provider=MSDAORA.1;Data Source=TIME;OLE DB Services=-1", "Database")
                    sDatabase = clsConfig.LoadSetting("DataBaseName", "TIME", "Database")
                End If
            End If
        End If

        bOracleTrace = (clsConfig.LoadSetting("OracleTrace", "TIME", "Database") = "Yes")

        clsASPSupport = Nothing
        clsConfig = Nothing
        On Error GoTo 0
    End Sub

End Module
