Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Public Class UserValidate
	
	Public Enum eUserStatus
		clngUserLoginFail = 1
		clngUserLoginEmpty = 2
		clngUserPasswdFail = 3
		clngUserValid = 4
		clngUserLock = 5
		clngUserSchemaFail = 6
		clngAccessDenied = 7
		clngUserSchemaLock = 8
        clngSystemExpired = 9
        clngReachedMaxAttempts = 10
        clngMustChangePassword = 11
	End Enum
	
	Private mlngUsersStatus As eUserStatus

    Public objOptSystem As eGeneral.Opt_system

	Public objUser As eGeneral.Users
    Public objCashBank As Object
	
	Public dLastDate As Date
	Public nDuration As Integer
	Public dSysExpired As Date
	
	Private mstrInitialOrigi As String
	Private mstrAccesswoOrigi As String
    Private mlngMultiCompOrigi As Integer
    Public sPasswordChangeSchema As String
	
	
	'**%StrEncode: Password encryption routine
	'%StrEncode: Rutina de encriptamiento de password
	Public Function StrEncode(ByVal s As String) As String
		Dim key As Integer
		Dim salt As Boolean
		Dim n As Integer
		Dim i As Integer
		Dim ss As String
		Dim k1 As Integer
		Dim k2 As Integer
		Dim k3 As Integer
		Dim k4 As Integer
        Dim t As Integer
        Dim varAux As String = ""

        Static saltvalue As String
        If Trim(s) <> String.Empty Then


            key = 1234567890
            salt = False

            If salt Then
                For i = 1 To 4
                    t = 100 * (1 + Asc(Mid(saltvalue, i, 1))) * Rnd() * (VB.Timer() + 1)
                    Mid(saltvalue, i, 1) = Chr(t Mod 256)
                Next
                s = Mid(saltvalue, 1, 2) & s & Mid(saltvalue, 3, 2)
            End If

            n = Len(s)
            ss = Space(n)
            Dim sn(n) As Integer

            k1 = 11 + (key Mod 233) : k2 = 7 + (key Mod 239)
            k3 = 5 + (key Mod 241) : k4 = 3 + (key Mod 251)

            For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next

            For i = 2 To n : sn(i) = sn(i) Xor sn(i - 1) Xor ((k1 * sn(i - 1)) Mod 256) : Next
            For i = n - 1 To 1 Step -1 : sn(i) = sn(i) Xor sn(i + 1) Xor (k2 * sn(i + 1)) Mod 256 : Next
            For i = 3 To n : sn(i) = sn(i) Xor sn(i - 2) Xor (k3 * sn(i - 1)) Mod 256 : Next
            For i = n - 2 To 1 Step -1 : sn(i) = sn(i) Xor sn(i + 2) Xor (k4 * sn(i + 1)) Mod 256 : Next

            For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next
            varAux = ss
        End If
        Return varAux
    End Function
	
	'**%StrDecode: Password de-encryptment routine
	'%StrDecode: Rutina de des-encriptamiento de password
	Public Function StrDecode(ByVal s As String) As String
		Dim key As Integer
		Dim salt As Boolean
		Dim n As Integer
		Dim i As Integer
		Dim ss As String
		Dim k1 As Integer
		Dim k2 As Integer
		Dim k3 As Integer
        Dim k4 As Integer
        Dim varAux As String = ""

        If Trim(s) <> String.Empty Then

            key = 1234567890
            salt = False

            n = Len(s)
            ss = Space(n)
            Dim sn(n) As Integer

            k1 = 11 + (key Mod 233) : k2 = 7 + (key Mod 239)
            k3 = 5 + (key Mod 241) : k4 = 3 + (key Mod 251)

            For i = 1 To n : sn(i) = Asc(Mid(s, i, 1)) : Next

            For i = 1 To n - 2 : sn(i) = sn(i) Xor sn(i + 2) Xor (k4 * sn(i + 1)) Mod 256 : Next
            For i = n To 3 Step -1 : sn(i) = sn(i) Xor sn(i - 2) Xor (k3 * sn(i - 1)) Mod 256 : Next
            For i = 1 To n - 1 : sn(i) = sn(i) Xor sn(i + 1) Xor (k2 * sn(i + 1)) Mod 256 : Next
            For i = n To 2 Step -1 : sn(i) = sn(i) Xor sn(i - 1) Xor (k1 * sn(i - 1)) Mod 256 : Next

            For i = 1 To n : Mid(ss, i, 1) = Chr(sn(i)) : Next i

            If salt Then varAux = Mid(ss, 3, Len(ss) - 4) Else varAux = ss
        End If
        Return varAux
    End Function
	
	'**%UserStatus: This property returns the status of the user that is being validated
	'%UserStatus: Esta propiedad se encarga de devolver el status del usuario que se está validando.
	ReadOnly Property UserStatus() As eUserStatus
		Get
			UserStatus = mlngUsersStatus
		End Get
    End Property

    ''' <summary>
    ''' Controla el máximo número de logon fallidos
    ''' </summary>
    ''' <param name="nUsercode"></param>
    ''' <remarks></remarks>
    Private Sub ManageMaxAttempts(ByVal nUsercode As Integer, ByVal nFailedLogonAttempts As Integer)

        Dim lrecDBHelper As New eRemoteDB.Execute

        Dim nMaxAttempts As Integer = New eRemoteDB.VisualTimeConfig().LoadSetting("MaxFailedLogonAttempts", 10000, "Security")
        Dim sLockUser As String
        Dim sResetAttempts As String


        'Se verifica que se haya alcanzado el número máximo de intentos definido en la configuración
        If nFailedLogonAttempts >= nMaxAttempts Then
            sLockUser = "1"
            sResetAttempts = "1"
            mlngUsersStatus = eUserStatus.clngReachedMaxAttempts
            'Si se trata de un intento satisfactorio, re reinicia el contador de logons fallidos
        ElseIf nFailedLogonAttempts = 0 Then
            sLockUser = "0"
            sResetAttempts = "1"
            'Si se trata de un intento fallido, per no se alcanza el máximo. Solo se aumenta el contador de fallidos
        Else
            sLockUser = "0"
            sResetAttempts = "0"
        End If

        With lrecDBHelper
            .StoredProcedure = "insManageMaxAttempts"
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sLockUser", sLockUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sResetAttempts", sResetAttempts, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
        End With

        lrecDBHelper = Nothing


    End Sub
	
	'**%ValidateUser: This method validates the user returning true or false,
	'**%depending on whether user is valid or not. If the user is valid, this method
	'**%loads the values of the property "UserStatus"
	'%ValidateUser: Esta función se encarga de realiza la validación del usuario
	'%devolviendo verdadero o falso, dependiendo si el usuario es válido o no. En el caso
	'%que el usuario sea invalido, este método carga los valores de la propiedad UsersStatus
    Public Function ValidateUser(ByVal sInitials As String, ByVal sPasswd As String, Optional ByVal sChangeLogin As String = "", Optional ByVal bValidateAspNet As Boolean = False) As Boolean
        Dim lobjSchema As Secur_sche = New Secur_sche
        Dim lclsConnect As eRemoteDB.Connection
        Dim lblnValUser As Boolean

        Dim lclsConfig As eRemoteDB.VisualTimeConfig
        Dim lbooJustQuote As Boolean
        Dim lstrQuoteType As String

        lclsConfig = New eRemoteDB.VisualTimeConfig

        Me.sPasswordChangeSchema = lclsConfig.LoadSetting("PasswordChangeSchema", "__NO__", "Security")

        lblnValUser = True
        If Trim(sInitials) <> String.Empty Then
            '**+Search for the user through their inicials
            '+Se realiza la busqueda del usuario por medio de la iniciales
            'On Error GoTo ValidateUser_err

            If lblnValUser Then
                objUser = New eGeneral.Users
                If objUser.FindUserInitial(sInitials) Then
                    With objUser
                        If .sLockedOut = "1" Or .sStatregt <> "1" Then
                            mlngUsersStatus = eUserStatus.clngUserLock
                        Else
                            '**+If the user exists, the password is validated
                            '+Si el usuario existe, se realiza la validaci´ón de la contraseña
                            If bValidateAspNet OrElse StrEncode(sPasswd) = .sAccesswo Then
                                Me.ManageMaxAttempts(objUser.nUsercode, 0)
                                lobjSchema = New Secur_sche
                                If lobjSchema.Find(.sSche_code, True) Then
                                    If lobjSchema.sStatregt = "3" Then
                                        mlngUsersStatus = eUserStatus.clngUserSchemaLock
                                    ElseIf objUser.sNeverExpires <> "1" AndAlso objUser.dPasswordExpires <> eRemoteDB.Constants.dtmNull AndAlso objUser.dPasswordExpires <= Date.Today Then
                                        ValidateUser = True
                                        .sSche_code = Me.sPasswordChangeSchema
                                        mlngUsersStatus = eUserStatus.clngMustChangePassword
                                    Else
                                        ValidateUser = True
                                    End If
                                Else
                                    mlngUsersStatus = eUserStatus.clngUserSchemaFail
                                End If
                            Else
                                mlngUsersStatus = eUserStatus.clngUserLoginFail
                                Me.ManageMaxAttempts(objUser.nUsercode, objUser.nFailedLogonAttempts + 1)
                            End If
                        End If
                    End With
                Else
                    mlngUsersStatus = eUserStatus.clngUserLoginFail
                End If

                If ValidateUser Then
                    objOptSystem = New eGeneral.Opt_system
                    Call objOptSystem.find()
                    '+Si se está ejecutando el "CoTizador" en modo "Stand alone", se verifica
                    '+la vigencia de la versión
                    lbooJustQuote = (lclsConfig.LoadSetting("JustQuote", "2", "Quoteizer") = "1")
                    lstrQuoteType = lclsConfig.LoadSetting("QuoteType", "0", "Quoteizer")
                    If lbooJustQuote And lstrQuoteType = "1" And lobjSchema.nDuration > 0 Then
                        If objOptSystem.dLastDate.AddDays(lobjSchema.nDuration) < Today Then
                            ValidateUser = False
                            mlngUsersStatus = eUserStatus.clngSystemExpired
                            dSysExpired = objOptSystem.dLastDate.AddDays(lobjSchema.nDuration)
                        End If
                    End If
                End If
            Else
                mlngUsersStatus = eUserStatus.clngUserLoginFail
            End If
        Else
            mlngUsersStatus = eUserStatus.clngUserLoginEmpty
        End If

ValidateUser_err:
        If Err.Number Then
            ValidateUser = False
        End If
        On Error GoTo 0
        lclsConnect = Nothing
        lobjSchema = Nothing
        lclsConfig = Nothing
    End Function
	
	'**%insLetLoginPsw: This procedure updates the session variables "sInitials" and "sAccessWo"
	'%insLetLoginPsw: Este procedimiento se encarga de actualizar las variables de sesion "sInitials" y "sAccessWo"
	Private Sub insLetLoginPsw(ByVal Login As String, ByVal PassWord As String, Optional ByVal bChangeLogin As Boolean = False)
        Dim objContext As New eRemoteDB.ASPSupport

		On Error Resume Next

        If bChangeLogin Then
            mstrInitialOrigi = objContext.GetASPSessionValue("sInitials")
            mstrAccesswoOrigi = objContext.GetASPSessionValue("sAccessWo")
            mlngMultiCompOrigi = objContext.GetASPSessionValue("nMultiCompany")
        End If
        objContext.SetASPSessionValue("sInitials", Login)
        objContext.SetASPSessionValue("sAccessWo", StrEncode(PassWord))

		On Error GoTo 0
    End Sub
	
	'+ GetVersionInfo: Obtiene la información relacionada a la versión actual
	'                  del sistema (Cotizador)
	Public Function GetVersionInfo(ByVal sSche_code As String) As Boolean
		Dim lobjSchema As Secur_sche
		
		On Error GoTo GetVersionInfo_err
		
		lobjSchema = New Secur_sche
		If lobjSchema.Find(sSche_code, True) Then
            Dim objOptSystem As New eGeneral.Opt_system
			If objOptSystem.Find Then
				dLastDate = objOptSystem.dLastDate
				nDuration = lobjSchema.nDuration
                dSysExpired = objOptSystem.dLastDate.AddDays(lobjSchema.nDuration)
			End If
		End If
		
GetVersionInfo_err: 
		If Err.Number Then
			GetVersionInfo = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjSchema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjSchema = Nothing
	End Function
	
	'+ GetVersionInfo: Obtiene la información relacionada a la versión actual
	'                  del sistema (Cotizador)
	Public Function insValSysEspired(ByVal sSche_code As String) As Boolean
		
		Dim lerrTime As eFunctions.Errors
		Dim lobjSchema As Secur_sche
		
		On Error GoTo insValSysEspired_Err
		
		lerrTime = New eFunctions.Errors
		lobjSchema = New Secur_sche
		
		If lobjSchema.Find(sSche_code, True) Then
            Dim objOptSystem As New eGeneral.Opt_system
			If objOptSystem.Find Then
				'+ Si no se ha indicado duración, se asume que no caduca la versión.
				If lobjSchema.nDuration > 0 Then
					dLastDate = objOptSystem.dLastDate
                    dSysExpired = objOptSystem.dLastDate.AddDays(lobjSchema.nDuration)
					nDuration = dSysExpired.ToOADate - Today.ToOADate
                    If (System.DateTime.FromOADate(dSysExpired.ToOADate - Today.ToOADate)) <= System.DateTime.FromOADate(lobjSchema.nDaysAdv) Then
                        insValSysEspired = True
                    Else
                        insValSysEspired = False
                    End If
				Else
					insValSysEspired = False
				End If
				
			End If
		End If
		
insValSysEspired_Err:
        If Err.Number Then
            insValSysEspired = CShort(insValSysEspired) + CDbl(Err.Description)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lerrTime = Nothing
        'UPGRADE_NOTE: Object lobjSchema may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjSchema = Nothing
    End Function

	Protected Overrides Sub Finalize()
        objUser = Nothing
        objCashBank = Nothing
        MyBase.Finalize()
	End Sub
End Class






