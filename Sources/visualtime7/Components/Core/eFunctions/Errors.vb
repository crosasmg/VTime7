Option Strict Off
Option Explicit On
Public Class Errors

    '-Se define las constantes usadas en el módulo
    Const CT_ERR As String = "Err. "
    Const CT_ADV As String = "Adv. "
    Const CT_MEN As String = "Men. "
    Const CT_NOMENCO As String = "No se encuentra el mensaje correspondiente"

    '-Se define la lista enumerada para ser usada con la propiedad ETextAlign
    Public Enum TextAlign
        LeftAling
        RigthAling
    End Enum

    '-Se define la lista enumerada para los tipos de errores
    Public Enum ErrorsType
        ErrorTyp = 1
        Warning = 2
        Message = 3
    End Enum

    '-Se define la variable mError para contener los errores generados
    Private mstrErr As String

    '- Se define la variable que indica que existe por lo menos un Error
    Private mblnError As Boolean

    '- Se define la variable que indica si se indicó número de línea
    Private mblnLine As Boolean

    '- Se define la variable publica
    '- Obliga a eliminar el boton aceptar de los mensajes y advertencias
    Public bError As Boolean

    '- Se define la variable que indica el formato del mensaje de error
    Public Highlighted As Boolean

    '- Se define la variable que indica el tipo de error a enviar.  Es un valor impuesto por
    '- el usuario.
    Public sTypeMessage As ErrorsType

    '-Variable que guarda el número de sesión
    Public sSessionID As String

    '-Código del usuario
    Public nUsercode As Integer
    '-Variable que almacena el nivel de actualizacion de un usuario para una transacción.
    Private nAmenlevel As Integer

    '-Variable que almacena el nivel de seguridad de un esquema.
    Private nSecurlev As Integer 

    Private mclsMessage As Message

    Public Sub New()
        MyBase.New()
        nAmenlevel = eRemoteDB.Constants.intNull
        mstrErr = String.Empty
        sTypeMessage = CShort("0")
        bError = False
        mclsMessage = New Message

        If eRemoteDB.ServiceEnviroment.isServiceConsumer Then
            If Not IsNothing(eRemoteDB.ServiceEnviroment.LastErrorValidate) Then
                eRemoteDB.ServiceEnviroment.LastErrorValidate.Clear()
                eRemoteDB.ServiceEnviroment.LastErrorValidate = Nothing
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        mclsMessage = Nothing
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Inicializa el nivel de actualizacion de un usuario para una transacción.
    ''' </summary>
    ''' <param name="sCodispl">Codispl de la ventana que tiene asociado el error</param>
    ''' <remarks></remarks>
    Private Sub Class_nAmenlevel(ByVal sCodispl As String)
        Dim lobjAspSupport As eRemoteDB.ASPSupport
        Dim lclsSecur_sche As Object

        Dim sSche_code As String

        lobjAspSupport = New eRemoteDB.ASPSupport
        lclsSecur_sche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")

        sSche_code = lobjAspSupport.GetASPSessionValue("SSCHE_CODE")

        If lclsSecur_sche.insReaLevels_v(sSche_code, 2, sCodispl) Then
            If lclsSecur_sche.sPermitted = 1 Then
                nAmenlevel = lclsSecur_sche.nAmelevel
            End If
        Else
            nAmenlevel = -1
            If lclsSecur_sche.insReaSecur_sche(sSche_code) Then
                nSecurlev = lclsSecur_sche.nSecurlev
            Else  
                nSecurlev = -1
            End If
        End If

        lobjAspSupport = Nothing
        lclsSecur_sche = Nothing
    End Sub

    ''' <summary>
    ''' Esta funcion se encarga de buscar y enviar el mensaje de error correspondiente dependiendo de los parametros con los que sea invocada.
    ''' </summary>
    ''' <param name="sCodispl">Codispl de la ventana que tiene asociado el error</param>
    ''' <param name="nErrorNum">Nro. de error a enviar</param>
    ''' <param name="nLine">Línea del grid en donde se produce el error</param>
    ''' <param name="Alignment">Alineación del texto que complementa el error</param>
    ''' <param name="EText">Texto que complementa el error</param>
    ''' <param name="bPuntual">Indica si el error se enviará de manera puntual</param>
    ''' <param name="ArrayErrors">Arreglo de registros de errores.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ErrorMessage(ByVal sCodispl As String, Optional ByVal nErrorNum As Integer = 0, Optional ByVal nLine As Integer = 0, Optional ByVal Alignment As TextAlign = TextAlign.RigthAling, Optional ByVal EText As String = "", Optional ByVal bPuntual As Boolean = False, Optional ByVal ArrayErrors As String = "") As String
        Dim recMessage As eRemoteDB.Execute
        Dim lstrMessage As String = String.Empty
        Dim lstrErrorType As ErrorsType
        Dim lstrsStatregt As String = String.Empty
        Dim lstrError As String = String.Empty
        Dim lstrMessageOriginal As String = String.Empty

        Dim llnglevel As Integer
        Dim lstrlevel As String
        Dim llngIndex As Integer


        If nAmenlevel = eRemoteDB.Constants.intNull Then
            Call Class_nAmenlevel(sCodispl)
        End If

        '+Se realiza la validacion dada una cadena de registros de errores
        Dim larrError() As String
        Dim larrDetError() As String
        Dim lvntError As Object
        If ArrayErrors > String.Empty Then

            larrError = Microsoft.VisualBasic.Split(ArrayErrors, "||")
            For Each lvntError In larrError
                larrDetError = Microsoft.VisualBasic.Split(lvntError, "|")
                If UBound(larrDetError) < 3 Then
                    ReDim Preserve larrDetError(3)
                End If
                ErrorMessage(sCodispl, larrDetError(0), IIf(larrDetError(1) = String.Empty, 0, larrDetError(1)), IIf(larrDetError(2) = String.Empty, 1, larrDetError(2)), larrDetError(3), , String.Empty)
            Next lvntError

            '+Se realiza la validacion dado un error
        ElseIf nErrorNum > 0 Then
            lstrError = mclsMessage.Load(sCodispl, nErrorNum, bPuntual)
            If lstrError = String.Empty Then
                recMessage = New eRemoteDB.Execute
                With recMessage
                    .StoredProcedure = "getmessage"
                    .Parameters.Add("sCodispl", sCodispl, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nErrornum", nErrorNum, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, Tables.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sMessaged", String.Empty, Parameter.eRmtDataDir.rdbParamOutput, Parameter.eRmtDataType.rdbVarchar, 500, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sErrortyp", String.Empty, Parameter.eRmtDataDir.rdbParamOutput, Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nLevel", nAmenlevel, Parameter.eRmtDataDir.rdbParamOutput, Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, Tables.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sStatregt", String.Empty, Parameter.eRmtDataDir.rdbParamOutput, Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)

                    If .Run(False) Then
                        lstrMessage = Trim(String.Empty & .Parameters("sMessaged").Value)
                        lstrMessageOriginal = lstrMessage
                        lstrErrorType = Val(Trim("0" & .Parameters("sErrortyp").Value))
                        lstrsStatregt = Trim(String.Empty & .Parameters("sStatregt").Value)
                        llnglevel = .Parameters("nLevel").Value
                        If llnglevel < 0 Then
                            llnglevel = 0
                        End If
                    End If
                End With
                recMessage = Nothing

                '+Se realiza la apertura del recordset referido a la tabla de mensajes de error
                '+Si no se encuentra el mensaje se envia el comentario correspondiente
                If lstrMessage = String.Empty Then
                    lstrErrorType = ErrorsType.ErrorTyp
                    lstrMessage = CT_NOMENCO
                Else
                    lstrMessage = "<LEFT> " & lstrMessage & " <RIGTH>"

                    '+Si se encuentra el mensaje se verifica que este se encuentre asociado a la ventana en tratamiento.
                    '+Para ello se realiza la apertura del recordset referido a la tabla de mensajes asociados a una ventana.
                    If CStr(sTypeMessage) = "0" Then
                        '+Si el mensaje se encuentra asociado a la ventana en tratamiento se asigna a la variable correspondiente
                        '+el tipo de mensaje a enviar
                        If lstrErrorType <> 0 Then
                            If lstrErrorType = ErrorsType.ErrorTyp Then
                                mblnError = True
                            End If
                        Else
                            '+Si el mensaje no se encuentra asociado a la ventana en tratamiento se asigna a la variable correspondiente
                            '+el valor "1" que indica que es un "Error"
                            lstrErrorType = ErrorsType.ErrorTyp
                            lstrsStatregt = "1"
                            mblnError = True
                        End If
                    Else
                        lstrErrorType = sTypeMessage
                        lstrsStatregt = "1"
                        mblnError = IIf(lstrErrorType = ErrorsType.ErrorTyp, True, False)
                    End If
                End If

                If lstrsStatregt <> "3" Then
                    '+Se asigna el Valor a retornar por la funcion de errores
                    If bPuntual Then
                        If Highlighted Then
                            lstrError = "<BR><B><LABEL>"
                        Else
                            lstrError = "<SCRIPT>alert("""
                        End If
                    Else
                        '+Se hace el llenado de los mensajes de error en el arreglo
                        lstrError = "<TR><TD ALIGN='CENTER'>"
                    End If

                    If lstrErrorType = ErrorsType.ErrorTyp Then
                        lstrError = lstrError & CT_ERR
                    Else
                        If lstrErrorType = ErrorsType.Message Then
                            lstrError = lstrError & CT_MEN
                        Else
                            lstrError = lstrError & CT_ADV
                        End If
                    End If

                    If bPuntual Then
                        If Highlighted Then
                            lstrError = lstrError & Trim(lstrMessage) & "</LABEL></B>"
                        Else
                            lstrError = lstrError & CStr(nErrorNum) & ":  " & Trim(lstrMessage) & """)</SCRIPT>"
                        End If
                    Else
                        lstrError = lstrError & "</TD>" & "<TD ALIGN='CENTER'>" & CStr(nErrorNum) & "</TD>" & "<TD>" & Trim(lstrMessage) & "</TD><TD><LINE></TD></TR>"
                    End If
                    lstrError = lstrError & "&" & llnglevel
                End If
                Call mclsMessage.Add(sCodispl, nErrorNum, bPuntual, lstrError, lstrErrorType)

                '+Se elimina la informacion de nivel de seguridad del mensaje
                llngIndex = InStr(lstrError, "&")
                If llngIndex <> 0 Then
                    lstrError = Left(lstrError, llngIndex - 1)
                End If

                '+Se agrega validacion del nivel contra win_message
                '+Advertencias y Mensajes se transaforman en error si usuario
                '+no tiene el nivel de seguridad mínimo
                If nAmenlevel <> -1 Then
                    If llnglevel > nAmenlevel Then
                        If lstrErrorType = ErrorsType.Message Then
                            lstrError = Replace(lstrError, "Men.", "Err.")
                        ElseIf lstrErrorType = ErrorsType.Warning Then
                            lstrError = Replace(lstrError, "Adv.", "Err.")
                        End If
                        mblnError = True
                    End If
                '+Se agrega validacion del nivel contra win_message
                '+Advertencias y Mensajes se transaforman en error si el esquema
                '+no tiene el nivel de seguridad mínimo
                ElseIf nSecurlev <> -1 Then
                    If llnglevel > nSecurlev Then
                        If lstrErrorType = ErrorsType.Message Then
                            lstrError = Replace(lstrError, "Men.", "Err.")
                        ElseIf lstrErrorType = ErrorsType.Warning Then
                            lstrError = Replace(lstrError, "Adv.", "Err.")
                        End If
                        mblnError = True
                    End If
                End If
            Else
                llngIndex = InStr(lstrError, "&")
                If llngIndex <> 0 Then
                    lstrErrorType = mclsMessage.nErrorType 
                    lstrlevel = Right(lstrError, 1)
                    lstrError = Left(lstrError, llngIndex - 1)
                    If lstrlevel <> String.Empty Then
                        llnglevel = CShort(lstrlevel)
                    End If

                    '+Se agrega validacion del nivel contra win_message
                    '+Advertencias y Mensajes se transaforman en error si usuario
                    '+no tiene el nivel de seguridad mínimo
                    If nAmenlevel <> -1 Then
                        If llnglevel > nAmenlevel Then
                            If lstrErrorType = ErrorsType.Message Then
                                lstrError = Replace(lstrError, "Men.", "Err.")
                            ElseIf lstrErrorType = ErrorsType.Warning Then
                                lstrError = Replace(lstrError, "Adv.", "Err.")
                            End If
                            mblnError = True
                        End If
                    '+Se agrega validacion del nivel contra win_message
                    '+Advertencias y Mensajes se transaforman en error si el esquema
                    '+no tiene el nivel de seguridad mínimo
                    ElseIf nSecurlev <> -1 Then
                        If llnglevel > nSecurlev Then
                            If lstrErrorType = ErrorsType.Message Then
                                lstrError = Replace(lstrError, "Men.", "Err.")
                            ElseIf lstrErrorType = ErrorsType.Warning Then
                                lstrError = Replace(lstrError, "Adv.", "Err.")
                            End If
                            mblnError = True
                        End If
                    End If
                End If
                If mclsMessage.nErrorType = ErrorsType.ErrorTyp Then
                    mblnError = True
                End If
            End If
            '+Se concatena el texto extra del mensaje
            If (Alignment = TextAlign.RigthAling) Then
                lstrError = Replace(Replace(lstrError, "<RIGTH>", EText), "<LEFT> ", String.Empty)
            Else
                lstrError = Replace(Replace(lstrError, "<LEFT>", EText), " <RIGTH>", String.Empty)
            End If
            If nLine = 0 Then
                lstrError = Replace(lstrError, "<LINE>", String.Empty)
            Else
                lstrError = Replace(lstrError, "<LINE>", CStr(nLine))
                mblnLine = True
            End If
            If bPuntual Then
                ErrorMessage = Trim(lstrError)
            Else
                If lstrError <> String.Empty Then
                    mstrErr = Trim(mstrErr) & lstrError

                    If eRemoteDB.ServiceEnviroment.isServiceConsumer Then
                        If IsNothing(eRemoteDB.ServiceEnviroment.LastErrorValidate) Then
                            eRemoteDB.ServiceEnviroment.LastErrorValidate = New ArrayList()
                        End If
                        eRemoteDB.ServiceEnviroment.LastErrorValidate.Add(New String() {lstrErrorType, nErrorNum.ToString, lstrMessage, nLine.ToString})
                    End If
                End If
            End If
            EText = String.Empty
            sTypeMessage = CShort("0")
        End If
    End Function

    ''' <summary>
    ''' Cierra la tabla y coloca los botones de acción que correspondan
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Confirm() As String
        Dim lclsValues As Values
        Dim lstrPercent As String
        Dim varAux As String = ""

        If mstrErr <> String.Empty Then
            If mblnLine Then
                lstrPercent = "5%"
            Else
                lstrPercent = "10%"
            End If

            varAux = "<DIV ID=""Scroll"" STYLE=""WIDTH:650;HEIGHT:244;OVERFLOW:auto;outset gray"">" & "<TABLE CLASS=grdData BORDER='1' WIDTH='100%'><TH WIDTH='" & lstrPercent & "' ALIGN='CENTER'>Tipo</TH><TH WIDTH='10%' ALIGN='CENTER'>Número</TH><TH WIDTH='80%'>Descripción</TH>"

            If mblnLine Then
                varAux = varAux & "<TH WIDTH='5%'>Línea</TH>"
            End If

            varAux = varAux & mstrErr
            lclsValues = New Values
            '+ Se añade una fila con los botones de Aceptar y Cancelar
            varAux = varAux & "</TABLE></DIV>" & "<P><TABLE WIDTH='100%'>" & "<TR><TD COLSPAN='2'><HR></TD></TR>" & "<TR>" & "<TD WIDTH='5%'>" & lclsValues.ButtonHelp("GE002") & "</TD>" & "<TD ALIGN='RIGHT' CLASS='Button'>"

            '+ Si existe por lo menos un error se coloca sólo el botón de Cancelar
            If mblnError Or bError Then
                varAux = varAux & lclsValues.ButtonAcceptCancel(, "CancelErrors()", , , Values.eButtonsToShow.OnlyCancel)
            Else
                varAux = varAux & lclsValues.ButtonAcceptCancel(, , True)
            End If
            varAux = varAux & "</TD></TR></TABLE></P>"
            lclsValues = Nothing
        End If
        Return varAux
    End Function

End Class
