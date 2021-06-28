Option Strict Off
Option Explicit On
Public Class OptionsInstallation
    '%-------------------------------------------------------%'
    '% $Workfile:: OptionsInstallation.cls                  $%'
    '% $Author:: Jguajardo                                  $%'
    '% $Date:: 16-03-06 8:08                                $%'
    '% $Revision:: 4                                        $%'
    '%-------------------------------------------------------%'

    Const CN_INSTALLED As String = "1"
    Const CN_NOT_INSTALLED As String = "2"
    Const CN_WITHOUT_CONTENT As Short = 2
    Const CN_MCO001 As Short = 1
    Const CN_MFI023 As Short = 2
    Const CN_MOP001 As Short = 3
    Const CN_MCA000 As Short = 4
    Const CN_MSI017 As Short = 5
    Const CN_MCR002 As Short = 6
    Const CN_MCC001 As Short = 7

    Const CN_FIX As Short = 3

    '- Tipo usuario para definir la estructura necesaria para cargar las opciones para el manejo de tarjetas de crédito
    Private Structure eTypeCreditCard
        Dim nCardType As Integer
        Dim nMemberNum As Integer
        Dim nAccBank As Integer
        Dim sBalanaffect As String
    End Structure

    '- Tipo definido para validar la existencia de ventanas con contenido apra cargarlas en la secuencia
    Private Structure eTypeRequired
        Dim eExist As Boolean
        Dim eWindows As eRemoteDB.Execute
    End Structure

    '- Tipo usuario para definir la estructura necesaria para cargar los módulos instalados en la aplicación
    Private Structure eTypeModule
        Dim nModules As Integer
        Dim sDescript As String
        Dim nSysModul As Integer
        Dim dInstalldate As Date
        Dim sFrame As String
        Dim sAuxSel As String
    End Structure

    '- Arreglo que contendrá las opciones de las tarjetas de crédito
    Private oArrCreditCard() As eTypeCreditCard

    '- Arreglo que contendrá el contenido de los módulos instalados en el sistema
    Private oArrModules() As eTypeModule


    '- Contiene el número de las opciones de la starjetas de crédito
    Public CountCreditCard As Integer

    '- Contiene el número de modulos instalados en el sistema
    '- Contain the number of modules installed in the system
    Public CountModules As Integer

    '- Varibales públicas para contener los atributos de un módulo instalado específico
    '- Publics variables to contain tha atributtes of a installed module
    Public nModule As Integer
    Public sDescript As String
    Public nSysModule As String
    Public dInstalldate As Date
    Public sFrame As String
    Public sAuxSel As String

    '**- Publics variables to contain tha atributtes of the manage of the credit cards
    '- Varibales públicas para contener los atributos del manejo de las tarjetas de crédito
    Public nCardType As Integer
    Public nMemberNum As Integer
    Public nAccBank As Integer
    Public sBalanaffect As String

    '- Varibales públicas para contener los atributos de las opciones generales
    '- Varibales public to contain the attributes of the general options
    Public dInit_Date As Date
    Public sPrint_tx_c As String
    Public sQ_value As String
    Public nModules As Integer
    Public dEffecdate As Date
    Public nLanguage As Integer
    Public sFormatPer As String
    Public dInitMod As Date
    Public sFormatComp As String
    Public nCountry As Integer
    Public sPolicyNum As String
    Public sClaimNum As String
    Public sReceiptNum As String
    Public nCompany As Integer
    Public sSecure As String
    Public sTypeCompany As String
    Public nInsur_Area As Integer
    Public mstrConfigContent As String
    Public sDate As Date
    Public sQuotnumauto As String
    Public nPEP As Integer
    Public nUsperson As Integer


    '**- The variables are defined to use to handle to the attributes of the options of installation of Cash and Bank
    '- Se definen las variables a utilizar para manejar los atributos de las opciones de instalación de Caja y Banco
    '+
    '+ Estructura de tabla insudb.opt_bank al 11-06-2001 13:28:44
    '+     Property                Type         DBType   Size Scale  Prec  Null
    '+-------------------------------------------------------------------------
    Public dEffecdateCash As Date ' DATE       7    0     0    N
    Public nCollect_pCash As Integer ' NUMBER     22   0     5    S
    Public nSta_chequeCash As Integer ' NUMBER     22   0     5    S
    Public nInsur_areaCash As Integer ' NUMBER     22   0     5    S
    Public nExpensesCash As Double ' NUMBER     22   2     10   S
    Public nCurrencyCash As Integer ' NUMBER     22   0     5    S
    Public nFinanInt As Double


    '**- The variables are defined to use to handle to the attributes of the options of installation of Policy
    '- Se definen las variables a utilizar para manejar los atributos de las opciones de instalación de Pólizas
    Public nCurrencyPol As Integer
    Public nPolicySalePol As Integer
    Public nIntermedPol As Integer
    Public sClauseImpPol As String
    Public sSTock_indPol As String

    '**- The variables are defined to use to handle to the attributes of the options of installation of claim
    '- Se definen las variables a utilizar para manejar los atributos de las opciones de instalación de siniestros
    Public nCurrencyClaim As Integer
    Public sIndReservClaim As String
    Public nSectionClaim As Integer
    Public nDaysSectionClaim As Double
    Public nPercentClaim As Double
    Public nPercent_NormClaim As Double
    Public nCostMinClaim As Double
    Public nCostMaxClaim As Double
    Public nMaxdaysClaim As Double
    Public nSimpli_payFreqClaim As Integer
    Public nYear_simpliClaim As Integer
    Public nTransi_ParyfreqClaim As Integer
    Public nYear_transiClaim As Integer

    '**- The variables are defined to use to handle to the attributes of the options of installation of CoReinsuran
    '- Se definen las variables a utilizar para manejar los atributos de las opciones de instalación de CoReaseguro
    Public sCoinsuriCoRe As String
    Public dNulldateCoRe As Date
    Public sReinsurfCoRe As String
    Public sReinsuroCoRe As String
    Public nCoaCessCoRe As Integer

    '**- Varibales public to contain the attributes of the options of intallation of colleciton
    '- Varibales públicas para contener los atributos de las opciones de instalación de Cobranza
    '+     Property                  Type         DBType   Size Scale  Prec  Null
    '+-------------------------------------------------------------------------
    Public dEffecdatePrem As Date
    Public nAcc_bankPrem As Integer
    Public sParCollectPrem As String
    Public sReqAmoPrem As String
    Public nUpperIntPrem As Integer
    Public nLowerIntPrem As Integer
    Public sTechAffectPrem As String
    Public nFixIntPrem As Integer
    Public nAmenLevelPrem As Integer
    Public nPreReceiptPrem As Integer
    Public nIntCalcPrem As Integer
    Public sMod_loLimPrem As String
    Public sMod_upLimPrem As String
    Public sDescriptPrem As String
    Public nCurrcollectexpPrem As Integer ' NUMBER     22   0     5    S
    Public nCollect_expPrem As Double ' NUMBER     22   2     10   S
    Public sClient As String

    Public nLower_limPrem As Double
    Public nUpper_limPrem As Double
    Public nLower_lim_Agree As Double
    Public nUpper_lim_Agree As Double

    Public nUpperPercent As Integer
    Public nUpperPercentAgree As Integer
    Public nLowerPercent As Integer
    Public nLowerPercentAgree As Integer

    Public nUpperPercentAMO As Double
    Public nUpperPercentAgreeAMO As Double
    Public nLowerPercentAMO As Double
    Public nLowerPercentAgreeAMO As Double

    Public nTolerCurr As Integer
    Public nCodToler As Integer

    '- Opciones de persistencia
    Public nQM_MinDurat As Integer
    Public nMonth_Expiry As Integer
    Public nMonth_Punish As Integer

    Public sStock_Ind As String
    Public nInstitution As Long
    Public sDistType As String
    '- Fecha Fija de Valorización de Caja
    Public sDateFix_Cash As String

    '%insValMS010_K: realiza las valiudaciones correspondientes al "header" de la secuencia de las opciones de instalación
    '%insValMS010_K: do the validations corrisponding to "header section" of the option's sequence
    Public Function insValMS010_K(ByVal sPassWord As String, ByVal sPassWordOptConst As String) As String
        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        insValMS010_K = String.Empty

        On Error GoTo insValMS010_K_Err

        With lobjErrors
            If UCase(Trim(sPassWord)) <> UCase(Trim(sPassWordOptConst)) Then .ErrorMessage("MS010_K", 1903,  , eFunctions.Errors.TextAlign.LeftAling)
            insValMS010_K = .Confirm
        End With

insValMS010_K_Err:
        If Err.Number Then
            insValMS010_K = "insValMS010_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '% LoadTabs: Esta función es la encarga de cargar la secuencia de ventanas a mostrar en la
    '%           secuencia opciones de instalación
    Public Function LoadTabs(ByVal nAction As Integer, ByVal sUserSchema As String) As String
        '- Se define la variable que controlará la lectura de ventanas para la secuencia
        Dim lobjRequired As eTypeRequired

        '- Se define la variable que devuelve el código HTML para poder "pintar" la secuencia
        Dim lclsSequence As eFunctions.Sequence

        '- Se crea la variable que contiene el código HTML para la creación de la tabla que simulará la secuencia
        Dim lstrHTMLCode As String

        '- Contendrá la imágen a asociar a la carpeta en la secuencia
        Dim lintPageImage As eFunctions.Sequence.etypeImageSequence

        On Error GoTo LoadTabs_Err

        lclsSequence = New eFunctions.Sequence

        '+ Se realizan las lecturas de las ventanas que tienen contenido
        lobjRequired = ValRequired()

        lstrHTMLCode = String.Empty

        '+ De haber una secuencia asociada a la solicitud en cuestión, se procede a armarla
        If lobjRequired.eExist Then

            '+ Se realiza el encabezado de la tabla que define a una secuencia
            lstrHTMLCode = lclsSequence.makeTable

            While Not EndWindows(lobjRequired.eWindows)
                '+ Se busca la imagen asociada a la pestaña en la secuencia para colocarla en los links
                lintPageImage = insValimage(sUserSchema, lobjRequired.eWindows)

                '+ Se extrae el código HTML para "pintar" una fila en la página
                With lobjRequired
                    lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(.eWindows.FieldToClass("sCodisp", String.Empty), .eWindows.FieldToClass("sCodispl", String.Empty), nAction, .eWindows.FieldToClass("sShort_des", String.Empty), lintPageImage)
                End With

                '+ Se procesa la próxima ventana
                NextWindow(lobjRequired.eWindows)
            End While

            '+ Se "pinta" la última fila de la tabla para completarla en código HTML
            lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
        End If
        LoadTabs = lstrHTMLCode

LoadTabs_Err:
        If Err.Number Then
            LoadTabs = "LoadTabs: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSequence = Nothing
    End Function

    '%ValRequired: lee las ventanas e indica si tienen o no contenido para cargarlas en la secuencia
    Private Function ValRequired() As eTypeRequired
        Dim lrecValRequired_OptInstall As eRemoteDB.Execute

        On Error GoTo ValRequired_Err

        lrecValRequired_OptInstall = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.ValRequired_OptInstall'
        '+ Información leída el 23/08/2001 02:37:51 PM
        With lrecValRequired_OptInstall
            .StoredProcedure = "ValRequired_OptInstall"
            If .Run Then
                ValRequired.eExist = True
                ValRequired.eWindows = lrecValRequired_OptInstall
            Else
                ValRequired.eExist = True
                'UPGRADE_NOTE: Object ValRequired.eWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                ValRequired.eWindows = Nothing
            End If
        End With

ValRequired_Err:
        If Err.Number Then
            With ValRequired
                .eExist = False
                'UPGRADE_NOTE: Object ValRequired.eWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                .eWindows = Nothing
            End With
        End If
        'UPGRADE_NOTE: Object lrecValRequired_OptInstall may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecValRequired_OptInstall = Nothing
        On Error GoTo 0
    End Function

    '%EndWindows: determina cuando debe detenerse el ciclo para terminar de cargar la secuencia de ventanas
    Private Function EndWindows(ByRef lrecWindows As eRemoteDB.Execute) As Boolean
        EndWindows = lrecWindows.EOF
    End Function

    '%NextWindow: busca el sigueinte item disponible para construír la secuencia
    '%NextWindow: find the next item disponible to build the sequence
    Private Sub NextWindow(ByRef lrecWindow As eRemoteDB.Execute)
        lrecWindow.RNext()
    End Sub

    '%insValimage: se busca la imágen que deberá tener asociada la pestaña en la secuencia
    Private Function insValimage(ByVal sUserSchema As String, ByRef lobjRequired As eRemoteDB.Execute) As eFunctions.Sequence.etypeImageSequence
        '- Contendrá los datos asociados al esquema de seguridad de la transacción en proceso: no admitida, etc.
        Dim lclsSecurSche As Object

        On Error GoTo insValimage_Err

        lclsSecurSche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")

        With lclsSecurSche
            If .Find(sUserSchema) Then
                If .ItemLevels(sUserSchema, 2, lobjRequired.FieldToClass("sCodispl", String.Empty)) Then
                    If lobjRequired.FieldToClass("sContent", 0) = 1 Then
                        insValimage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
                    Else
                        If lobjRequired.FieldToClass("sRequired", 0) = 1 Then
                            insValimage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
                        Else
                            insValimage = eFunctions.Sequence.etypeImageSequence.eDeniedS
                        End If
                    End If
                Else
                    If lobjRequired.FieldToClass("sContent", 0) = 2 Then
                        If lobjRequired.FieldToClass("sRequired", 0) = 1 Then
                            insValimage = eFunctions.Sequence.etypeImageSequence.eRequired
                        Else
                            insValimage = eFunctions.Sequence.etypeImageSequence.eEmpty
                        End If
                    Else
                        insValimage = eFunctions.Sequence.etypeImageSequence.eOK
                    End If
                End If
            End If
        End With

insValimage_Err:
        If Err.Number Then
            insValimage = eFunctions.Sequence.etypeImageSequence.eEmpty
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecurSche = Nothing
    End Function

    '%insValContent: valida que las ventanas d ela secuencia tengan contenido para poder terminar con la secuencia
    Public Function insValContent(Optional ByVal nAction As Integer = eRemoteDB.Constants.intNull) As Boolean
        '- Se define la variable que controlará la lectura de ventanas para la secuencia
        Dim lobjRequired As eTypeRequired

        On Error GoTo insValContent_Err

        '+ Se realiza la lectura para poder determinar si cada una de las ventanas de la secuencia tienen o no contenido y devuelva sus valores
        lobjRequired = ValRequired()

        If lobjRequired.eExist Then
            insValContent = True
            While Not EndWindows(lobjRequired.eWindows)
                With lobjRequired.eWindows

                    '+ Si alguna de las ventanas devueltas de la BD tiene el campo sContent = 2, significa que no ha sido validada y aceptada

                    If .FieldToClass("sContent") = CN_WITHOUT_CONTENT Then
                        insValContent = False
                        Exit Function
                    End If
                End With
                NextWindow(lobjRequired.eWindows)
            End While
        Else
            insValContent = False
        End If

insValContent_Err:
        If Err.Number Then
            insValContent = False
        End If
        On Error GoTo 0
    End Function

    '%FindModules: carga los valores de los módulos activos en el sistema
    '%FindModules: load the values asociated to the modules of the system
    Public Function FindModules() As Boolean
        Dim lrecinsOpt_table As eRemoteDB.Execute
        Dim lintTop As Integer
        Dim lintIndex As Integer

        On Error GoTo FindModules_Err

        lrecinsOpt_table = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insOpt_table'
        '+ Información leída el 17/09/2001 03:23:16 PM
        With lrecinsOpt_table
            .StoredProcedure = "insOpt_table"
            If .Run Then
                FindModules = True
                lintTop = 1
                lintIndex = 1
                While Not .EOF
                    If lintTop = lintIndex Then
                        lintTop = lintTop + 20
                        ReDim Preserve oArrModules(lintTop)
                    End If
                    oArrModules(lintIndex).sAuxSel = IIf(.FieldToClass("nSysModul") <> 0 And .FieldToClass("nSysModul") <> eRemoteDB.Constants.intNull, CN_INSTALLED, CN_NOT_INSTALLED)
                    oArrModules(lintIndex).dInstalldate = .FieldToClass("dInstallDate", "01/01/1800")
                    oArrModules(lintIndex).nModules = .FieldToClass("nModules", 0)
                    oArrModules(lintIndex).nSysModul = .FieldToClass("nSysmodul", 0)
                    oArrModules(lintIndex).sDescript = .FieldToClass("sDescript", String.Empty)
                    Select Case .FieldToClass("nModules", 0)
                        Case CN_MCO001
                            oArrModules(lintIndex).sFrame = "MCO001"
                        Case CN_MFI023
                            oArrModules(lintIndex).sFrame = "MFI023"
                        Case CN_MOP001
                            oArrModules(lintIndex).sFrame = "MOP001"
                        Case CN_MCA000
                            oArrModules(lintIndex).sFrame = "MCA000"
                        Case CN_MSI017
                            oArrModules(lintIndex).sFrame = "MSI017"
                        Case CN_MCR002
                            oArrModules(lintIndex).sFrame = "MCR002"
                        Case CN_MCC001
                            oArrModules(lintIndex).sFrame = "MCC001"
                    End Select
                    lintIndex = lintIndex + 1
                    .RNext()
                End While
                ReDim Preserve oArrModules(lintIndex - 1)
                CountModules = lintIndex - 1
                .RCloseRec()
            Else
                FindModules = False
            End If
        End With

FindModules_Err:
        If Err.Number Then
            FindModules = False
            CountModules = 0
            ReDim oArrModules(0)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_table may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_table = Nothing
    End Function

    '%FindOptGeneral: carga los valores de las opciones generales de instalación
    '%FindOptGeneral: load the values corrispondig to Options generals
    Public Function FindOptGeneral() As Boolean
        Dim lrecreaOpt_System As eRemoteDB.Execute

        On Error GoTo FindOptGeneral_Err

        lrecreaOpt_System = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOpt_System'
        '+ Información leída el 20/09/2001 11:06:40 AM
        With lrecreaOpt_System
            .StoredProcedure = "reaOpt_System"
            If .Run Then
                FindOptGeneral = True
                dInit_Date = .FieldToClass("dInit_date", "01/01/1800")
                sPrint_tx_c = .FieldToClass("sPrint_tx_c", String.Empty)
                sQ_value = .FieldToClass("sQ_value", String.Empty)
                nModules = .FieldToClass("nModules", 0)
                dEffecdate = .FieldToClass("dEffecdate", "01/01/1800")
                nLanguage = .FieldToClass("nLanguage", 0)
                sFormatPer = .FieldToClass("sFormatPer", String.Empty)
                dInitMod = .FieldToClass("dInitMod", "01/01/1800")
                sFormatComp = .FieldToClass("sFormatComp", String.Empty)
                nCountry = .FieldToClass("nCountry", 0)
                sPolicyNum = .FieldToClass("sPolicyNum", String.Empty)
                sClaimNum = .FieldToClass("sClaimNum", String.Empty)
                sReceiptNum = .FieldToClass("sReceiptNum", String.Empty)
                nCompany = .FieldToClass("nCompany", 0)
                sSecure = .FieldToClass("sSecure", String.Empty)
                sTypeCompany = .FieldToClass("sTypeCompany", String.Empty)
                nInsur_Area = .FieldToClass("nInsur_Area", 0)
                sQuotnumauto = .FieldToClass("sQuotnumauto", "2")
                nPEP = .FieldToClass("nPEP", "2")
                nUsperson = .FieldToClass("nUsperson", "2")
                .RCloseRec()
            Else
                FindOptGeneral = False
            End If
        End With

FindOptGeneral_Err:
        If Err.Number Then
            FindOptGeneral = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaOpt_System may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_System = Nothing
    End Function

    '%ItemModule: carga los valores de un módulo específico
    '%ItemModule: load the values asociated to module
    Public Sub ItemModule(ByVal Index As Integer)
        '---------------------- --------------------------------------------------
        If Index <= CountModules Then
            With oArrModules(Index)
                nModule = .nModules
                sDescript = .sDescript
                nSysModule = CStr(.nSysModul)
                dInstalldate = .dInstalldate
                sFrame = .sFrame
                sAuxSel = .sAuxSel
            End With
        Else
            nModule = 0
            sDescript = String.Empty
            nSysModule = CStr(0)
            dInstalldate = CDate("01/01/1800")
            sFrame = String.Empty
        End If
    End Sub

    '%insPreMS010: carga los valores de los campos puntuales de la ventana MS010 para que sean mostrados en la misma
    '%insPreMS010: load the values of the puntual fields of the window MS010
    Public Sub insPreMS010()
        FindOptGeneral()
    End Sub

    '%UnCheckModule: actualiza el estado de un módulo instalado previamente en el sistema como desinstalado
    '%UnCheckModule: updates the state of a module installed previously in system like unistalled
    Public Function UnCheckModule(Optional ByVal nSysModul As Integer = 0) As Boolean
        Dim lrecdelOptModules As eRemoteDB.Execute

        On Error GoTo UnCheckModule_Err

        lrecdelOptModules = New eRemoteDB.Execute

        If nSysModul <> 0 Then
            Me.nSysModule = CStr(nSysModul)
        End If

        '+ Definición de parámetros para stored procedure 'insudb.delOptModules'
        '+ Información leída el 21/09/2001 03:25:20 PM
        With lrecdelOptModules
            .StoredProcedure = "delOptModules"
            .Parameters.Add("nSysModul", Me.nSysModule, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UnCheckModule = .Run(False)
        End With

UnCheckModule_Err:
        If Err.Number Then
            UnCheckModule = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdelOptModules may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelOptModules = Nothing
    End Function

    '**%insPreMOP001: load the values of the puntual fields of the window MOP001
    '%insPreMOP001  : carga los valores de los campos puntuales de la ventana MOP001 para que sean mostrados en la misma
    Public Sub insPreMOP001()
        FindOptBank()
    End Sub

    '**%FindOptBank: load the values of the options of installation of collection
    '%FindOptBank: carga los valores de las opciones de instalación de cobranza
    Public Function FindOptBank() As Boolean
        Dim lrecreaOpt_Bank As eRemoteDB.Execute

        On Error GoTo FindOptBank_Err

        lrecreaOpt_Bank = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOpt_Bank'
        '+ Información leída el 02/10/2001 03:11:36 PM
        With lrecreaOpt_Bank
            .StoredProcedure = "reaOpt_Bank"
            If .Run Then
                FindOptBank = True
                dEffecdateCash = .FieldToClass("dEffecdate", eRemoteDB.Constants.dtmNull)
                nCollect_pCash = .FieldToClass("nCollect_p", 0)
                nSta_chequeCash = .FieldToClass("nSta_cheque", 0)
                nInsur_areaCash = .FieldToClass("nInsur_area", 0)
                nExpensesCash = .FieldToClass("nExpenses", 0)
                nCurrencyCash = .FieldToClass("nCurrency", 0)
                dInstalldate = .FieldToClass("dInstalldate", eRemoteDB.Constants.dtmNull)
                nFinanInt = .FieldToClass("nFinanInt", 0)
                .RCloseRec()
            Else
                FindOptBank = False
            End If
        End With

FindOptBank_Err:
        If Err.Number Then
            FindOptBank = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaOpt_Bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_Bank = Nothing
    End Function


    '%FindOptBank: carga los valores de las opciones de instalación de intermediarios
    Public Function FindOptIntermed() As Boolean
        Dim lrecreaopt_Intermed As eRemoteDB.Execute

        On Error GoTo FindOptIntermed_Err

        lrecreaopt_Intermed = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaopt_Intermed'
        '+ Información leída el 02/10/2001 03:11:36 PM
        With lrecreaopt_Intermed
            .StoredProcedure = "reaopt_Intermed"
            If .Run Then
                FindOptIntermed = True
                nQM_MinDurat = .FieldToClass("nQM_MinDurat")
                nMonth_Expiry = .FieldToClass("nMonth_Expiry")
                nMonth_Punish = .FieldToClass("nMonth_Punish")
                .RCloseRec()
            End If
        End With

FindOptIntermed_Err:
        If Err.Number Then
            FindOptIntermed = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaopt_Intermed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaopt_Intermed = Nothing
    End Function


    '**%insPreMCA000: Load the values of the puntual fields of the window MCA000
    '%insPreMCA000  : Carga los valores de los campos puntuales de la ventana MCA000 para que sean mostrados en la misma
    Public Sub insPreMCA000()
        FindOptPolicy()
    End Sub

    '**%FindOptPolicy: load the values of the options of installation of Policy
    '%FindOptPolicy: carga los valores de las opciones de instalación de pólizas
    Public Function FindOptPolicy() As Boolean
        Dim lrecreaOpt_policy As eRemoteDB.Execute

        On Error GoTo FindOptPolicy_Err

        lrecreaOpt_policy = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOpt_policy'
        '+ Información leída el 03/10/2001 12:01:22 AM
        With lrecreaOpt_policy
            .StoredProcedure = "reaOpt_policy"
            If .Run Then
                nCurrencyPol = .FieldToClass("nCurrency", 0)
                nPolicySalePol = .FieldToClass("nPolicySale", 0)
                nIntermedPol = .FieldToClass("nIntermed", 0)
                sClauseImpPol = .FieldToClass("sClauseImp", String.Empty)
                sSTock_indPol = .FieldToClass("sStock_ind", String.Empty)
                nInstitution = .FieldToClass("nInstitution", 0)
                FindOptPolicy = True
                .RCloseRec()
            Else
                FindOptPolicy = False
            End If
        End With

FindOptPolicy_Err:
        If Err.Number Then
            FindOptPolicy = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaOpt_policy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_policy = Nothing
    End Function

    '**%insPreMCR002: load the values of the puntual fields of the window MCR002
    '%insPreMCR002  : carga los valores de los campos puntuales de la ventana MCR002 para que sean mostrados en la misma
    Public Sub insPreMCR002()
        FindOptCoreinsuran()
    End Sub

    '**%FindOptCoreinsuran: load the values of the options of installation of finance
    '%FindOptCoreinsuran  : carga los valores de las opciones de instalación de financiamiento
    Private Function FindOptCoreinsuran() As Boolean
        Dim lrecreaOpt_core As eRemoteDB.Execute

        On Error GoTo FindOptCoreinsuran_Err

        lrecreaOpt_core = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOpt_core'
        '+ Información leída el 03/10/2001 04:32:20 PM

        With lrecreaOpt_core
            .StoredProcedure = "reaOpt_core"
            If .Run Then
                FindOptCoreinsuran = True
                sCoinsuriCoRe = .FieldToClass("sCoinsuri", String.Empty)
                dNulldateCoRe = .FieldToClass("dNulldate", eRemoteDB.Constants.dtmNull)
                sReinsurfCoRe = .FieldToClass("sReinsurf", String.Empty)
                sReinsuroCoRe = .FieldToClass("sReinsuro", String.Empty)
                nCoaCessCoRe = .FieldToClass("nCoaCess", 0)
                sDistType = .FieldToClass("sDistType")
                .RCloseRec()
            Else
                FindOptCoreinsuran = False
            End If
        End With

FindOptCoreinsuran_Err:
        If Err.Number Then
            FindOptCoreinsuran = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaOpt_core may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_core = Nothing
    End Function

    '**%insPreMSI017: load the values of the puntual fields of the window MSI017
    '%insPreMSI017  : carga los valores de los campos puntuales de la ventana MSI017 para que sean mostrados en la misma
    Public Sub insPreMSI017()
        FindOptClaim()
    End Sub

    '**%FindOptClaim: load the values of the options of installation of claim
    '%FindOptClaim: carga los valores de las opciones de instalación de siniestros
    Public Function FindOptClaim() As Boolean
        Dim lrecreaOpt_sinies As eRemoteDB.Execute

        On Error GoTo FindOptClaim_Err

        lrecreaOpt_sinies = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOpt_sinies'
        '+ Información leída el 03/10/2001 01:22:57 AM
        With lrecreaOpt_sinies
            .StoredProcedure = "reaOpt_sinies"
            If .Run Then
                nCurrencyClaim = .FieldToClass("nCurrency", 0)
                sIndReservClaim = .FieldToClass("sIndReserv", String.Empty)
                nSectionClaim = .FieldToClass("nSection", 0)
                nDaysSectionClaim = .FieldToClass("nDaysSection", 0)
                nPercentClaim = .FieldToClass("nPercent", 0)
                nPercent_NormClaim = .FieldToClass("nPercent_Norm", 0)
                nCostMinClaim = .FieldToClass("nCostMin", 0)
                nCostMaxClaim = .FieldToClass("nCostMax", 0)
                nMaxdaysClaim = .FieldToClass("nMaxDays", 0)
                nSimpli_payFreqClaim = .FieldToClass("nSimpli_Payfreq", 0)
                nYear_simpliClaim = .FieldToClass("nYear_Simpli", 0)
                nTransi_ParyfreqClaim = .FieldToClass("nTransi_PayFreq", 0)
                nYear_transiClaim = .FieldToClass("nYear_Transi", 0)
                FindOptClaim = True
                .RCloseRec()
            Else
                FindOptClaim = False
            End If
        End With

FindOptClaim_Err:
        If Err.Number Then
            FindOptClaim = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaOpt_sinies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_sinies = Nothing
    End Function

    '**%insValMSI017: function that makes the corresponding validations of the window MSI017
    '%insValMSI017: función que realiza las validaciones correspondientes la ventana MSI017
    Public Function insValMSI017(ByVal nCurrency As Integer, Optional ByVal nSection As Integer = 0, Optional ByVal nDaysSection As Double = 0, Optional ByVal nCostMin As Double = 0, Optional ByVal nCostMax As Double = 0, Optional ByVal nPercent As Double = 0, Optional ByVal nPercent_Norm As Double = 0) As String
        Dim lobjErrors As eFunctions.Errors
        On Error GoTo insValMSI017_Err

        lobjErrors = New eFunctions.Errors
        insValMSI017 = String.Empty

        With lobjErrors
            '**+ Been worth that selects some currency
            '+ Se valida que se seleccione alguna moneda
            If nCurrency = 0 Then
                .ErrorMessage("MSI017", 10107, , eFunctions.Errors.TextAlign.LeftAling)
            End If

            'Se valida si se seleccionó tramo automático
            If nSection = 2 Then
                If nDaysSection <= 0 Or String.IsNullOrEmpty(nDaysSection.ToString) Then
                    .ErrorMessage("MSI017", 5080, , eFunctions.Errors.TextAlign.LeftAling)
                End If
            End If

            'Se valida costo mínimo
            If (nCostMin > 0 Or nCostMin <> eRemoteDB.Constants.dblNull) And nCostMin > nCostMax Then
                .ErrorMessage("MSI017", 90000507, , eFunctions.Errors.TextAlign.LeftAling)
            End If

            'Se valida costo máximo
            If (nCostMax > 0 Or nCostMax <> eRemoteDB.Constants.dblNull) And nCostMax < nCostMin Then
                .ErrorMessage("MSI017", 90000508, , eFunctions.Errors.TextAlign.LeftAling)
            End If

            'Se valida que porcentaje sobre prima bruta no sea inferior a lo impuesto por la norma
            If nPercent > 0 Or nPercent <> eRemoteDB.Constants.dblNull Then
                If nPercent < nPercent_Norm Then
                    .ErrorMessage("MSI017", 90000509, , eFunctions.Errors.TextAlign.RigthAling, " (" & nPercent_Norm & ")")
                End If
            End If

            insValMSI017 = .Confirm
        End With

insValMSI017_Err:
        If Err.Number Then
            insValMSI017 = "insValMSI017: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '**%insValMCA000: function that makes the corresponding validations of the window MCA000
    '%insValMCA000: función que realiza las validaciones correspondientes la ventana MCA000
    Public Function insValMCA000(ByVal nIntermed As Integer, ByVal nSalePol As Integer) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValMCA000_Err

        lobjErrors = New eFunctions.Errors

        insValMCA000 = String.Empty

        With lobjErrors
            '**+ Been worth that being the sales of policies of type "Direct/Agent", the intermediary has value. In addition, to have value the intermediary, must be valid.
            '+ Se valida que siendo la ventas de pólizas de tipo "Directo/Intermediario", el intermediario tenga valor. Además, de tener valor el intermediario, debe ser válido.
            If nIntermed <= 0 Then
                If nSalePol = 3 Then
                    .ErrorMessage("MCA000", 9129)
                End If
            Else
                If Not ValidIntermed(nIntermed) Then
                    .ErrorMessage("MCA000", 9002)
                End If
            End If
            insValMCA000 = .Confirm
        End With

insValMCA000_Err:
        If Err.Number Then
            insValMCA000 = "insValMCA000: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%insValMAG978: función que realiza las validaciones correspondientes la ventana MAG978
    Public Function insValMAG978(ByVal nQM_MinDurat As Integer, ByVal nMonth_Expiry As Integer, ByVal nMonth_Punish As Integer) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValMAG978_Err

        lobjErrors = New eFunctions.Errors

        If nQM_MinDurat < 0 Then
            lobjErrors.ErrorMessage("MAG978", 978001)
        End If
        If nMonth_Expiry < 0 Then
            lobjErrors.ErrorMessage("MAG978", 978002)
        End If
        If nMonth_Punish < 0 Then
            lobjErrors.ErrorMessage("MAG978", 978003)
        End If


        insValMAG978 = lobjErrors.Confirm


insValMAG978_Err:
        If Err.Number Then
            insValMAG978 = "insValMAG978: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function


    '**%insValMS010: function that makes the corresponding validations of the window MS010
    '%insValMS010: función que realiza las validaciones correspondientes la ventana MS010
    Public Function insValMS010(ByVal dInitDate As Date, ByVal nCompany As Integer, ByVal nInsur_Area As Integer, ByVal nCountry As Integer, ByVal dInstalldate As Date, ByVal nPopUp As Integer, dInit_date_aux As Date) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValMS010_Err

        lobjErrors = New eFunctions.Errors

        insValMS010 = String.Empty

        With lobjErrors

            '**+ Only in the case of validating the window "PopUp" of grid, the date of installation of the module is taken into account
            '+ Sólo en el caso de validar la ventana "PopUp" del grid, se toma en cuenta la fecha de instalación del módulo

            If nPopUp Then
                If dInstalldate = eRemoteDB.Constants.dtmNull Then .ErrorMessage("MS010", 10208,  , eFunctions.Errors.TextAlign.LeftAling)

                '**+ It Is validated that the date of installation of the module in the PoopUp Is equal to Or greater than the date of installation of the system
                '+ Se valida que la fecha de instalacion del mudulo en el PopUp sea igual o mayor a la fecha de instalacion del sistema 

                If dInstalldate < dInit_date_aux Then
                    .ErrorMessage("MS010", 90000518)
                End If

            Else
                '**+ It Is validated that the date of installation of the module Is equal to Or greater than the date of installation of the system
                '+ Se valida que la fecha de instalacion del mudulo sea igual o mayor a la fecha de instalacion del sistema 

                If FindModules() Then
                    For lintIndex = 1 To CountModules
                        If oArrModules(lintIndex).dInstalldate < dInitDate And oArrModules(lintIndex).sAuxSel = CN_INSTALLED Then
                            .ErrorMessage("MS010", 90000518, , , "módulo: " & oArrModules(lintIndex).sDescript.ToString)
                        End If

                    Next
                End If

                '**+ Valid that the date of installation is full
                '+ Se valida que la fecha de instalación esté llena

                If dInitDate = eRemoteDB.Constants.dtmNull Then .ErrorMessage("MS010", 99121,  , eFunctions.Errors.TextAlign.LeftAling)

                '**+ If a company were selected it must be valid in the data base
                '+ Si se seleccionó una compañía debe estar válida en la base de datos

                If nCompany <> 0 Then
                    If Not ExistValidCompany(nCompany) Then .ErrorMessage("MS010", 12039,  , eFunctions.Errors.TextAlign.LeftAling)
                End If

                '**+ Valid that the country is full
                '+ Se valida que el país esté lleno

                If nCountry = 0 Then .ErrorMessage("MS010", 12039,  , eFunctions.Errors.TextAlign.LeftAling)

                If nInsur_Area <= 0 Then .ErrorMessage("MS010", 60215,  , eFunctions.Errors.TextAlign.LeftAling)

            End If
            insValMS010 = .Confirm
        End With

insValMS010_Err:
        If Err.Number Then
            insValMS010 = "insValMS010: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '**%insPostMSI010: makes the pertinent updates after accepting the Grid of the window MSI010
    '%insPostMSI010: realiza las actualizaciones pertinentes luego de aceptar el grid de la MS010
    Public Function insPostMSI010Mod(ByRef sAction As String, ByRef nUsercode As Integer, Optional ByVal nModule As Integer = 0, Optional ByVal dInstalldate As Date = #12:00:00 AM#) As Boolean
        On Error GoTo insPostMSI010Mod_Err

        With Me
            If nModule <> 0 Then .nModule = nModule
            If dInstalldate <> eRemoteDB.Constants.dtmNull Then .dInstalldate = dInstalldate
        End With

        If sAction = "Delete" Then
            insPostMSI010Mod = UnCheckModule(Me.nModule)
        Else
            insPostMSI010Mod = CheckModule(Me.nModule, Me.dInstalldate, nUsercode)
        End If

insPostMSI010Mod_Err:
        If Err.Number Then
            insPostMSI010Mod = False
        End If
        On Error GoTo 0
    End Function

    '**%insPostMCA000: makes the pertinent updates after accepting window MCA000
    '%insPostMCA000: realiza las actualizaciones pertinentes luego de aceptar la ventana MCA000
    Public Function insPostMCA000(ByVal nUsercode As Integer, Optional ByVal nCurrency As Integer = 0, Optional ByVal nPolicySale As Integer = 0, Optional ByVal nIntermed As Integer = 0, Optional ByVal sClauseimp As String = "", Optional ByVal sSTock_ind As String = "") As Boolean
        Dim lrecinsOpt_policy As eRemoteDB.Execute

        On Error GoTo insPostMCA000_Err

        lrecinsOpt_policy = New eRemoteDB.Execute

        With Me
            .nCurrencyPol = nCurrency
            .nPolicySalePol = nPolicySale
            .nIntermedPol = nIntermed
            .sClauseImpPol = IIf(sClauseimp <> String.Empty, "1", "2")
            .sSTock_indPol = IIf(sSTock_ind <> String.Empty, "1", "2")
        End With

        '+ Definición de parámetros para stored procedure 'insudb.insOpt_policy'
        '+ Información leída el 03/10/2001 12:28:28 AM

        With lrecinsOpt_policy
            .StoredProcedure = "insOpt_policy"
            .Parameters.Add("nCurrency", Me.nCurrencyPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicySale", Me.nPolicySalePol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", Me.nIntermedPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClauseImp", Me.sClauseImpPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSTock_ind", Me.sSTock_indPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostMCA000 = .Run(False)
            If insPostMCA000 Then
                insUpdOptSystem((Me.sClauseImpPol))
            End If
        End With

insPostMCA000_Err:
        If Err.Number Then
            insPostMCA000 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_policy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_policy = Nothing
    End Function

    '**%insPostMCR002: makes the pertinent updates after accepting window MCR002
    '%insPostMCR002: realiza las actualizaciones pertinentes luego de aceptar la ventana MCR002
    Public Function insPostMCR002(ByVal nUsercode As Integer, Optional ByVal sCoinsuri As String = "", Optional ByVal dNulldate As Date = #12:00:00 AM#, Optional ByVal sReinsurf As String = "", Optional ByVal sReinsuro As String = "", Optional ByVal nCoaCess As Integer = 0) As Boolean
        Dim lrecinsOpt_core As eRemoteDB.Execute

        On Error GoTo insPostMCR002_Err

        lrecinsOpt_core = New eRemoteDB.Execute

        With Me
            .sCoinsuriCoRe = IIf(sCoinsuri <> String.Empty, "1", "2")
            .sReinsurfCoRe = IIf(sReinsurf <> String.Empty, "1", "2")
            .sReinsuroCoRe = IIf(sReinsuro <> String.Empty, "1", "2")
            If dNulldate <> eRemoteDB.Constants.dtmNull Then .dNulldateCoRe = dNulldate
            If nCoaCess <> 0 Then .nCoaCessCoRe = nCoaCess
        End With

        '+ Definición de parámetros para stored procedure 'insudb.insOpt_core'
        '+ Información leída el 04/10/2001 08:46:59 AM

        With lrecinsOpt_core
            .StoredProcedure = "insOpt_core"
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCoinsuri", Me.sCoinsuriCoRe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReinsurf", Me.sReinsurfCoRe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReinsuro", Me.sReinsuroCoRe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoacess", Me.nCoaCessCoRe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostMCR002 = .Run(False)
        End With

insPostMCR002_Err:
        If Err.Number Then
            insPostMCR002 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_core may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_core = Nothing
    End Function

    '**%insPostMSI017: makes the pertinent updates after accepting window MSI017
    '%insPostMSI017: realiza las actualizaciones pertinentes luego de aceptar la ventana MSI017
    Public Function insPostMSI017(ByVal nUsercode As Integer, Optional ByVal nCurrency As Integer = 0, Optional ByVal sIndReserv As String = "", Optional ByVal nSection As Integer = 0, Optional ByVal nDaysSection As Double = 0, Optional ByVal nPercent As Double = 0, Optional ByVal nCostMin As Double = 0, Optional ByVal nCostMax As Double = 0, Optional ByVal nMaxdays As Double = 0, Optional ByVal nSimpli_payFreq As Integer = 0, Optional ByVal nYear_Simpli As Integer = 0, Optional ByVal nTransi_payFreq As Integer = 0, Optional ByVal nYear_Transi As Integer = 0) As Boolean
        Dim lrecUpdOpt_sinies As eRemoteDB.Execute

        On Error GoTo insPostMSI017_Err

        lrecUpdOpt_sinies = New eRemoteDB.Execute

        With Me
            If nCurrency <> 0 Then .nCurrencyClaim = nCurrency
            .sIndReservClaim = IIf(sIndReserv <> String.Empty, "1", "2")
        End With

        '+ Definición de parámetros para stored procedure 'insudb.UpdOpt_sinies'
        '+ Información leída el 03/10/2001 01:27:56 AM

        With lrecUpdOpt_sinies
            .StoredProcedure = "UpdOpt_sinies"
            .Parameters.Add("nCurrency", Me.nCurrencyClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndReserv", Me.sIndReservClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSection", nSection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDayssection", nDaysSection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCostmin", nCostMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCostmax", nCostMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaxdays", nMaxdays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSimpli_payfreq", nSimpli_payFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear_simpli", nYear_Simpli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransi_payfreq", nTransi_payFreq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear_transi", nYear_Transi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostMSI017 = .Run(False)
        End With

insPostMSI017_Err:
        If Err.Number Then
            insPostMSI017 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecUpdOpt_sinies may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdOpt_sinies = Nothing
    End Function

    '**%insPostMSI010: makes the pertinent updates after accepting window MSI010
    '%insPostMSI010: realiza las actualizaciones pertinentes luego de aceptar la ventana MSI010
    Public Function insPostMSI010(Optional ByVal dInit_Date As Date = #12:00:00 AM#, Optional ByVal sFormatPer As String = "", Optional ByVal sFormatComp As String = "", Optional ByVal nCountry As Integer = 0, Optional ByVal nCompany As Integer = 0, Optional ByVal nIsur_Area As Integer = 0, Optional ByVal sFrame As String = "", Optional ByVal sPrint_tx_c As String = "", Optional ByVal sPolicyNum As String = "", Optional ByVal sClaimNum As String = "", Optional ByVal sReceiptNum As String = "", Optional ByVal sSecure As String = "", Optional ByVal sQuotnumauto As String = "", Optional ByVal nPEP As Integer = 0, Optional ByVal nUsperson As Integer = 0) As Boolean
        Dim lrecupdOptSystem As eRemoteDB.Execute

        On Error GoTo insPostMSI010_Err

        lrecupdOptSystem = New eRemoteDB.Execute

        With Me
            If dInit_Date <> eRemoteDB.Constants.dtmNull Then .dInit_Date = dInit_Date
            If sFormatPer <> String.Empty Then .sFormatPer = sFormatPer
            If sFormatComp <> String.Empty Then .sFormatComp = sFormatComp
            If nCountry <> 0 Then .nCountry = nCountry
            If nCompany <> 0 Then .nCompany = nCompany
            If nIsur_Area > 0 Then .nInsur_Area = nIsur_Area
            If sFrame <> String.Empty Then .sFrame = sFrame
            If sPrint_tx_c <> String.Empty Then sPrint_tx_c = sPrint_tx_c
            If sPolicyNum <> String.Empty Then .sPolicyNum = sPolicyNum
            If sClaimNum <> String.Empty Then .sClaimNum = sClaimNum
            If sReceiptNum <> String.Empty Then .sReceiptNum = sReceiptNum
            If sSecure <> String.Empty Then .sSecure = sSecure
            If sQuotnumauto <> String.Empty Then .sQuotnumauto = sQuotnumauto
            If nPEP <> 0 Then .nPEP = nPEP
            If nUsperson <> 0 Then .nUsperson = nUsperson
        End With

        '+ Definición de parámetros para stored procedure 'insudb.updOptSystem'
        '+ Información leída el 22/09/2001 05:08:49 PM

        With lrecupdOptSystem
            .StoredProcedure = "updOptSystem"
            .Parameters.Add("dInit_date", Me.dInit_Date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFormatPer", Me.sFormatPer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 13, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFormatComp", Me.sFormatComp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 13, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCountry", Me.nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCompany", Me.nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", "MS010", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sPrint_tx_c", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolicynum", Me.sPolicyNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClaimNum", Me.sClaimNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceiptNum", Me.sReceiptNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSecure", Me.sSecure, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_Area", Me.nInsur_Area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sQuotnumauto", Me.sQuotnumauto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPEP", Me.nPEP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsperson", Me.nUsperson, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostMSI010 = .Run(False)
        End With

insPostMSI010_Err:
        If Err.Number Then
            insPostMSI010 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdOptSystem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdOptSystem = Nothing
    End Function

    '**% ValidIntermed: valid that the agent exist in the table of agents
    '%   ValidIntermed: valida que el intermediario exista en la tabla de "interemdiarios"
    Private Function ValidIntermed(ByVal nIntermed As Integer) As Boolean
        Dim lclsIntermedia As Object

        On Error GoTo ValidIntermed_Err

        lclsIntermedia = eRemoteDB.NetHelper.CreateClassInstance("eAgent.Agents")

        With lclsIntermedia
            .nIntermed = nIntermed
            ValidIntermed = .Find_Supervis_v
        End With

ValidIntermed_Err:
        If Err.Number Then
            ValidIntermed = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsIntermedia may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsIntermedia = Nothing
    End Function

    '**%ExistValidCompany: been worth that the selected company exists in Database like valid
    '%ExistValidCompany: valida que la compañía seleccionada exista en la Base de Datos como válida
    Private Function ExistValidCompany(ByVal nCompany As Integer) As Boolean
        Dim lrecreaCompany As eRemoteDB.Execute

        On Error GoTo ExistValidCompany_Err

        lrecreaCompany = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaCompany'
        '+ Información leída el 22/09/2001 04:35:44 PM

        With lrecreaCompany
            .StoredProcedure = "reaCompany"
            .Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ExistValidCompany = .Run
        End With

ExistValidCompany_Err:
        If Err.Number Then
            ExistValidCompany = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCompany = Nothing
    End Function

    '**%CheckModule: add a module in the table
    '%CheckModule: crea un módulo en la tabla
    Public Function CheckModule(ByVal nSysModul As Integer, ByVal dInstalldate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lreccreOptModules As eRemoteDB.Execute

        On Error GoTo CheckModule_Err

        lreccreOptModules = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.creOptModules'
        '+ Información leída el 24/09/2001 11:25:52 AM

        With lreccreOptModules
            .StoredProcedure = "creOptModules"
            .Parameters.Add("nSysModul", nSysModul, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInstallDate", dInstalldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            CheckModule = .Run(False)
        End With

CheckModule_Err:
        If Err.Number Then
            CheckModule = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lreccreOptModules may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccreOptModules = Nothing
    End Function

    '**%insUpdOptSystem: it updates the table of general options of installation with the field Printing of clauses
    '%insUpdOptSystem  : actualiza la tabla de opciones generales de instalación con el campo Impresión de cláusulas
    Private Function insUpdOptSystem(ByRef sClauseImpPol As String) As Boolean
        Dim lrecupdOptSystem As eRemoteDB.Execute

        On Error GoTo insUpdOptSystem_Err

        lrecupdOptSystem = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.updOptSystem'
        '+ Información leída el 03/10/2001 12:39:06 AM
        With lrecupdOptSystem
            .StoredProcedure = "updOptSystem"
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dInit_date", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sFormatPer", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 13, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sFormatComp", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 13, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCountry", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCompany", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sCodispl", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrint_tx_c", sClauseImpPol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sPolicynum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sClaimNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sReceiptNum", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sSecure", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nInsur_Area", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sQuotnumauto", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPEP", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsperson", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdOptSystem = .Run(False)
        End With

insUpdOptSystem_Err:
        If Err.Number Then
            insUpdOptSystem = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecupdOptSystem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdOptSystem = Nothing
    End Function

    '**%insPreMCO001: load the values of the puntual fields of the window MCO001
    '%insPreMCO001  : carga los valores de los campos puntuales de la ventana MCO001 para que sean mostrados en la misma
    Public Sub insPreMCO001()
        FindOptPremium()
    End Sub

    '**%FindOptPremium: load the values of the options of installation of collection
    '%FindOptPremium: carga los valores de las opciones de instalación de cobranza
    Public Function FindOptPremium() As Boolean
        Dim lrecreaOpt_Premium As eRemoteDB.Execute

        On Error GoTo FindOptPremium_Err

        lrecreaOpt_Premium = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaOpt_Premium'
        '+ Información leída el 20/09/2001 11:06:40 AM

        With lrecreaOpt_Premium
            .StoredProcedure = "reaOpt_premiu"
            If .Run Then
                FindOptPremium = True
                dEffecdatePrem = .FieldToClass("dEffecdate", eRemoteDB.Constants.dtmNull)
                nAcc_bankPrem = .FieldToClass("nAcc_bank", 0)
                nLower_limPrem = .FieldToClass("nLower_lim", 0)
                nUpper_limPrem = .FieldToClass("nUpper_lim", 0)
                sParCollectPrem = .FieldToClass("sParCollect", String.Empty)
                sDateFix_Cash = .FieldToClass("sDateFix_Cash", String.Empty)
                sReqAmoPrem = .FieldToClass("sReqAmo", String.Empty)
                nUpperIntPrem = .FieldToClass("nUpperInt", 0)
                nLowerIntPrem = .FieldToClass("nLowerInt", 0)
                sTechAffectPrem = .FieldToClass("sTechAffect", String.Empty)
                nFixIntPrem = .FieldToClass("nFixInt", 0)
                nAmenLevelPrem = .FieldToClass("nAmenLevel", 0)
                nPreReceiptPrem = .FieldToClass("nPreReceipt", 0)
                nIntCalcPrem = .FieldToClass("nIntCalc", 0)
                sMod_loLimPrem = .FieldToClass("sMod_loLim", String.Empty)
                sMod_upLimPrem = .FieldToClass("sMod_upLim", String.Empty)
                sDescriptPrem = .FieldToClass("sDescript", String.Empty)
                nCurrcollectexpPrem = .FieldToClass("nCurrcollectexp", 0)
                nCollect_expPrem = .FieldToClass("nCollect_exp", 0)
                sClient = .FieldToClass("sClient", String.Empty)
                nLower_lim_Agree = .FieldToClass("nLower_lim_Agree", 0)
                nUpper_lim_Agree = .FieldToClass("nUpper_lim_Agree", 0)

                nUpperPercent = .FieldToClass("nUpperPercent", 0)
                nUpperPercentAgree = .FieldToClass("nUpperPercentAgree", 0)
                nLowerPercent = .FieldToClass("nLowerPercent", 0)
                nLowerPercentAgree = .FieldToClass("nLowerPercentAgree", 0)
                nUpperPercentAMO = .FieldToClass("nUpperPercentAMO", 0)
                nUpperPercentAgreeAMO = .FieldToClass("nUpperPercentAgreeAMO", 0)
                nLowerPercentAMO = .FieldToClass("nLowerPercentAMO", 0)
                nLowerPercentAgreeAMO = .FieldToClass("nLowerPercentAgreeAMO", 0)

                nTolerCurr = .FieldToClass("nTolerCurr", 4)
                nCodToler = .FieldToClass("nCodToler", 1)
                sDateFix_Cash = .FieldToClass("sDateFix_Cash")


                .RCloseRec()
            Else
                FindOptPremium = False
            End If
        End With

FindOptPremium_Err:
        If Err.Number Then
            FindOptPremium = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaOpt_Premium may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaOpt_Premium = Nothing
    End Function
    '%insValMCO001: función que realiza las validaciones correspondientes la ventana MCO001
    Public Function insValMCO001(ByVal nAcc_bank As Integer, ByVal nIntcalc As Integer, ByVal nFixint As Integer,
                                 ByVal nUpper_limPrem As Double, ByVal nLower_limPrem As Double, ByVal nLower_lim_Agree As Double,
                                 ByVal nUpper_lim_Agree As Double, ByVal nUpperPercent As Double, ByVal nUpperPercentAgree As Double,
                                 ByVal nLowerPercent As Double, ByVal nLowerPercentAgree As Double, ByVal nUpperPercentAMO As Double,
                                 ByVal nUpperPercentAgreeAMO As Double, ByVal nLowerPercentAMO As Double, ByVal nLowerPercentAgreeAMO As Double,
                                 ByVal nTolerCurr As Integer) As String
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lobjErrors As eFunctions.Errors
        Dim lclsValues As eFunctions.Values

        On Error GoTo insValMCO001_Err

        lobjErrors = New eFunctions.Errors
        lclsValues = New eFunctions.Values

        insValMCO001 = String.Empty

        With lobjErrors

            '+ Si la cuenta bancaria está llena se deben llenar la cantidad de días de domiciliación.
            '+ Además debe ser válida la cuenta

            If nAcc_bank <> eRemoteDB.Constants.intNull Then
                If Not insValidAccBank(nAcc_bank) Then
                    .ErrorMessage("MCO001", 7013, , eFunctions.Errors.TextAlign.LeftAling)
                End If
            End If

            '+ Si la forma de cálculo corresponde a "% fijo" debe estar lleno el interés de mora

            If nIntcalc = CN_FIX Then
                If nFixint = eRemoteDB.Constants.intNull Then .ErrorMessage("MCO001", 21112, , eFunctions.Errors.TextAlign.LeftAling)
            End If

            'If (nLower_limPrem <> eRemoteDB.Constants.intNull And nLowerPercent <> eRemoteDB.Constants.intNull) Or
            If (nLower_limPrem = eRemoteDB.Constants.intNull And nLowerPercent = eRemoteDB.Constants.intNull) Then
                .ErrorMessage("MCO001", 5, , eFunctions.Errors.TextAlign.RigthAling, "(campo: en Defecto)")
            End If

            'If (nUpper_limPrem <> eRemoteDB.Constants.intNull And nUpperPercent <> eRemoteDB.Constants.intNull) Or
            If (nUpper_limPrem = eRemoteDB.Constants.intNull And nUpperPercent = eRemoteDB.Constants.intNull) Then
                .ErrorMessage("MCO001", 5, , eFunctions.Errors.TextAlign.RigthAling, "(campo: en Exceso)")
            End If

            If nTolerCurr = eRemoteDB.Constants.intNull Then
                .ErrorMessage("MCO001", 750024, , eFunctions.Errors.TextAlign.LeftAling, " para el margen de tolerancia")
            End If

            insValMCO001 = .Confirm

        End With

insValMCO001_Err:
        If Err.Number Then
            insValMCO001 = "insValMCO001: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
    End Function

    '%insPostMCO001: realiza las actualizaciones pertinentes luego de aceptar la ventana MCO001
    Public Function insPostMCO001(Optional ByVal nLower_lim As Double = 0, Optional ByVal nUpper_lim As Double = 0, Optional ByVal sParcollect As String = "", Optional ByVal sDateFix_Cash As String = "",
                                  Optional ByVal sReqamo As String = "", Optional ByVal nUpperint As Integer = 0, Optional ByVal nLowerint As Integer = 0,
                                  Optional ByVal nUsercode As Integer = 0, Optional ByVal nBank_acc As Integer = 0, Optional ByVal sTechaffect As String = "",
                                  Optional ByVal nFixint As Integer = 0, Optional ByVal nAmenlevel As Integer = 0, Optional ByVal nPrereceipt As Integer = 0,
                                  Optional ByVal nIntcalc As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sMod_lolim As String = "",
                                  Optional ByVal sMod_uplim As String = "", Optional ByVal nCurrcollectexp As Integer = 0, Optional ByVal nCollect_exp As Double = 0,
                                  Optional ByVal sClient As String = "", Optional ByVal nLower_lim_Agree As Double = 0, Optional ByVal nUpper_lim_Agree As Double = 0,
                                  Optional ByVal nUpperPercent As Double = 0, Optional ByVal nUpperPercentAgree As Double = 0,
                                  Optional ByVal nLowerPercent As Double = 0, Optional ByVal nLowerPercentAgree As Double = 0, Optional ByVal nUpperPercentAMO As Double = 0,
                                  Optional ByVal nUpperPercentAgreeAMO As Double = 0, Optional ByVal nLowerPercentAMO As Double = 0, Optional ByVal nLowerPercentAgreeAMO As Double = 0,
                                  Optional ByVal nTolerCurr As Integer = 4, Optional ByVal nToler As Integer = 1) As Boolean

        Dim lrecinsOpt_Premiu As eRemoteDB.Execute

        On Error GoTo insPostMCO001_Err

        lrecinsOpt_Premiu = New eRemoteDB.Execute

        With Me

            If nToler <> eRemoteDB.Constants.intNull Then
                .nCodToler = nToler
            End If

            If nTolerCurr <> eRemoteDB.Constants.intNull Then
                .nTolerCurr = nTolerCurr
            End If

            If nUpperPercent <> eRemoteDB.Constants.intNull Then
                .nUpperPercent = nUpperPercent
            End If

            If nUpperPercentAgree <> eRemoteDB.Constants.intNull Then
                .nUpperPercentAgree = nUpperPercentAgree
            End If

            If nLowerPercent <> eRemoteDB.Constants.intNull Then
                .nLowerPercent = nLowerPercent
            End If

            If nLowerPercentAgree <> eRemoteDB.Constants.intNull Then
                .nLowerPercentAgree = nLowerPercentAgree
            End If

            If nUpperPercentAMO <> eRemoteDB.Constants.intNull Then
                .nUpperPercentAMO = nUpperPercentAMO
            End If

            If nUpperPercentAgreeAMO <> eRemoteDB.Constants.intNull Then
                .nUpperPercentAgreeAMO = nUpperPercentAgreeAMO
            End If

            If nLowerPercentAMO <> eRemoteDB.Constants.intNull Then
                .nLowerPercentAMO = nLowerPercentAMO
            End If

            If nLowerPercentAgreeAMO <> eRemoteDB.Constants.intNull Then
                .nLowerPercentAgreeAMO = nLowerPercentAgreeAMO
            End If

            If nLower_lim <> eRemoteDB.Constants.intNull Then
                .nLower_limPrem = nLower_lim
            End If

            If nUpper_lim <> eRemoteDB.Constants.intNull Then
                .nUpper_limPrem = nUpper_lim
            End If

            If nLower_lim_Agree <> eRemoteDB.Constants.intNull Then
                .nLower_lim_Agree = nLower_lim_Agree
            End If

            If nUpper_lim_Agree <> eRemoteDB.Constants.intNull Then
                .nUpper_lim_Agree = nUpper_lim_Agree
            End If

            .sParCollectPrem = IIf(sParcollect <> String.Empty, "1", "2")
            .sReqAmoPrem = IIf(sReqamo <> String.Empty, "1", "2")

            If nUpperint <> eRemoteDB.Constants.intNull Then
                .nUpperIntPrem = nUpperint
            End If

            If nLowerint <> eRemoteDB.Constants.intNull Then
                .nLowerIntPrem = nLowerint
            End If

            If nBank_acc <> eRemoteDB.Constants.intNull Then
                .nAcc_bankPrem = nBank_acc
            End If

            .sTechAffectPrem = IIf(sTechaffect <> String.Empty, "1", "2")

            If nFixint <> eRemoteDB.Constants.intNull Then
                .nFixIntPrem = nFixint
            End If

            If nAmenlevel <> eRemoteDB.Constants.intNull Then
                .nAmenLevelPrem = nAmenlevel
            End If

            If nPrereceipt <> eRemoteDB.Constants.intNull Then
                .nPreReceiptPrem = nPrereceipt
            End If

            If nIntcalc <> eRemoteDB.Constants.intNull Then
                .nIntCalcPrem = nIntcalc
            End If

            If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                .dEffecdate = dEffecdate
            End If

            .sMod_loLimPrem = IIf(sMod_lolim <> String.Empty, "1", "2")
            .sMod_upLimPrem = IIf(sMod_uplim <> String.Empty, "1", "2")

            If nCurrcollectexp <> eRemoteDB.Constants.intNull Then
                .nCurrcollectexpPrem = nCurrcollectexp
            End If

            If nCollect_exp <> eRemoteDB.Constants.intNull Then
                .nCollect_expPrem = nCollect_exp
            End If

            If sClient <> String.Empty Then
                Me.sClient = sClient
            End If

            If sDateFix_Cash <> String.Empty Then
                Me.sDateFix_Cash = sDateFix_Cash
            End If

        End With

        '+ Definición de parámetros para stored procedure 'insOpt_Premiu'
        '+ Información leída el 01/10/2001 09:27:16 AM

        With lrecinsOpt_Premiu
            .StoredProcedure = "insOpt_Premiu"
            .Parameters.Add("nLower_lim", Me.nLower_limPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpper_lim", Me.nUpper_limPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sParCollect", Me.sParCollectPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDateFix_Cash", Me.sDateFix_Cash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReqAmo", Me.sReqAmoPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpperInt", Me.nUpperIntPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLowerInt", Me.nLowerIntPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank_acc", Me.nAcc_bankPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTechAffect", Me.sTechAffectPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFixInt", Me.nFixIntPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmenLevel", Me.nAmenLevelPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPreReceipt", Me.nPreReceiptPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntCalc", Me.nIntCalcPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMod_loLim", Me.sMod_loLimPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMod_upLim", Me.sMod_upLimPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrcollectexp", Me.nCurrcollectexpPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCollect_exp", Me.nCollect_expPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLower_lim_Agree", Me.nLower_lim_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpper_lim_Agree", Me.nUpper_lim_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nUpperPercent", Me.nUpperPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpperPercentAgree", Me.nUpperPercentAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLowerPercent", Me.nLowerPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLowerPercentAgree", Me.nLowerPercentAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpperPercentAMO", Me.nUpperPercentAMO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpperPercentAgreeAMO", Me.nUpperPercentAgreeAMO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLowerPercentAMO", Me.nLowerPercentAMO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLowerPercentAgreeAMO", Me.nLowerPercentAgreeAMO, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nTolerCurr", Me.nTolerCurr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCodToler", Me.nCodToler, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostMCO001 = .Run(False)
        End With

insPostMCO001_Err:
        If Err.Number Then
            insPostMCO001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_Premiu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_Premiu = Nothing
    End Function


    '%insPostMAG978: realiza las actualizaciones pertinentes luego de aceptar la ventana MAG978
    Public Function insPostMAG978(ByVal nQM_MinDurat As Short, ByVal nMonth_Expiry As Short, ByVal nMonth_Punish As Short, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsOpt_Premiu As eRemoteDB.Execute

        On Error GoTo insPostMAG978_Err

        lrecinsOpt_Premiu = New eRemoteDB.Execute


        '+ Definición de parámetros para stored procedure 'insUpdOpt_Intermed'
        '+ Información leída el 01/10/2001 09:27:16 AM

        With lrecinsOpt_Premiu
            .StoredProcedure = "insUpdOpt_Intermed"
            .Parameters.Add("nQM_MinDurat", nQM_MinDurat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth_Expiry", nMonth_Expiry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth_Punish", nMonth_Punish, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostMAG978 = .Run(False)
        End With

insPostMAG978_Err:
        If Err.Number Then
            insPostMAG978 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_Premiu may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_Premiu = Nothing
    End Function

    '**%insValidAccBank: valid that the company was valid
    Private Function insValidAccBank(ByVal nAcc_bank As Integer) As Boolean
        Dim lrecreaBank_acc_o As eRemoteDB.Execute

        On Error GoTo insValidAccBank_Err

        lrecreaBank_acc_o = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaBank_acc_o'
        '+ Información leída el 28/09/2001 11:58:06 AM
        With lrecreaBank_acc_o
            .StoredProcedure = "reaBank_acc_o"
            .Parameters.Add("nAcc_Bank", nAcc_bank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insValidAccBank = .Run
        End With

insValidAccBank_Err:
        If Err.Number Then
            insValidAccBank = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaBank_acc_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaBank_acc_o = Nothing
    End Function

    '**%insPostMOP001: makes the pertinent updates after accepting window MOP001
    '%insPostMOP001: realiza las actualizaciones pertinentes luego de aceptar la ventana MOP001
    Public Function insPostMOP001(ByVal dEffecdate As Date, ByVal nUsercode As Integer, Optional ByVal nCollect_p As Integer = 0, Optional ByVal nSta_cheque As Integer = 0, Optional ByVal nInsur_Area As Integer = 0, Optional ByVal nExpenses As Double = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nFinanInt As Double = 0) As Boolean
        Dim lrecinsOpt_bank As eRemoteDB.Execute

        On Error GoTo insPostMOP001_Err

        lrecinsOpt_bank = New eRemoteDB.Execute

        With Me
            If nCollect_p <> 0 Then .nCollect_pCash = nCollect_p
            If nSta_cheque <> 0 Then .nSta_chequeCash = nSta_cheque
            If nInsur_Area <> 0 Then .nInsur_areaCash = nInsur_Area
            If nExpenses <> 0 Then .nExpensesCash = nExpenses
            If nCurrency <> 0 Then .nCurrencyCash = nCurrency
            If nFinanInt <> 0 Then .nFinanInt = nFinanInt
        End With

        '+ Definición de parámetros para stored procedure 'insudb.insOpt_bank'
        '+ Información leída el 02/10/2001 03:30:32 PM

        With lrecinsOpt_bank
            .StoredProcedure = "insOpt_bank"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCollect_p", Me.nCollect_pCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSta_cheque", Me.nSta_chequeCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsur_area", Me.nInsur_areaCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExpenses", Me.nExpensesCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", Me.nCurrencyCash, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFinanInt", Me.nFinanInt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostMOP001 = .Run(False)
        End With

insPostMOP001_Err:
        If Err.Number Then
            insPostMOP001 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_bank may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_bank = Nothing
    End Function

    '**%FindCreditCard: it loads the values associated to the handling of the credit cards in the installation options
    '%FindCreditCard: carga los valores asociados al manejo de las tarjetas de crédito en las opciones de instalación
    Public Function FindCreditCard() As Boolean
        Dim lrecinsOpt_crCard As eRemoteDB.Execute
        Dim lintTop As Integer
        Dim lintIndex As Integer

        On Error GoTo FindCreditCard_Err

        lrecinsOpt_crCard = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insOpt_CrCard'
        '+ Información leída el 17/09/2001 03:23:16 PM
        With lrecinsOpt_crCard
            .StoredProcedure = "reaOpt_CrCard"
            If .Run Then
                FindCreditCard = True
                lintTop = 1
                lintIndex = 1
                While Not .EOF
                    If lintTop = lintIndex Then
                        lintTop = lintTop + 20
                        ReDim Preserve oArrCreditCard(lintTop)
                    End If
                    oArrCreditCard(lintIndex).sBalanaffect = .FieldToClass("sBalanAffect")
                    oArrCreditCard(lintIndex).nMemberNum = .FieldToClass("nMemberNum", eRemoteDB.Constants.dtmNull)
                    oArrCreditCard(lintIndex).nCardType = .FieldToClass("nCardType", 0)
                    oArrCreditCard(lintIndex).nAccBank = .FieldToClass("nAccBank", 0)
                    lintIndex = lintIndex + 1
                    .RNext()
                End While
                ReDim Preserve oArrCreditCard(lintIndex - 1)
                CountCreditCard = lintIndex - 1
                .RCloseRec()
            Else
                FindCreditCard = False
            End If
        End With

FindCreditCard_Err:
        If Err.Number Then
            FindCreditCard = False
            CountCreditCard = 0
            ReDim oArrCreditCard(0)
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsOpt_crCard may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsOpt_crCard = Nothing
    End Function

    '**%ItemCreditCard: load the values asociated to credit cards
    '%ItemCreditCard: carga los valores asociados a las tarjetas de crédito
    Public Sub ItemCreditCard(ByVal Index As Integer)
        '---------------------- --------------------------------------------------
        If Index <= CountCreditCard Then
            With oArrCreditCard(Index)
                nAccBank = .nAccBank
                nCardType = .nCardType
                nMemberNum = .nMemberNum
                sBalanaffect = .sBalanaffect
            End With
        Else
            nAccBank = 0
            nCardType = 0
            nMemberNum = 0
            sBalanaffect = String.Empty
        End If
    End Sub

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()

        dEffecdatePrem = eRemoteDB.Constants.dtmNull

        nAcc_bankPrem = eRemoteDB.Constants.intNull
        nLower_limPrem = eRemoteDB.Constants.intNull
        nUpper_limPrem = eRemoteDB.Constants.intNull
        sParCollectPrem = String.Empty
        sReqAmoPrem = String.Empty
        nUpperIntPrem = eRemoteDB.Constants.intNull
        nLowerIntPrem = eRemoteDB.Constants.intNull
        sTechAffectPrem = String.Empty
        nFixIntPrem = eRemoteDB.Constants.intNull
        nAmenLevelPrem = eRemoteDB.Constants.intNull
        nPreReceiptPrem = eRemoteDB.Constants.intNull
        nIntCalcPrem = eRemoteDB.Constants.intNull
        sMod_loLimPrem = String.Empty
        sMod_upLimPrem = String.Empty
        sDescriptPrem = String.Empty

        nCurrcollectexpPrem = eRemoteDB.Constants.intNull
        nCollect_expPrem = eRemoteDB.Constants.intNull

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '	Public Function LoadFileToText(ByVal sFileName As String) As String
    '		Dim lngHandle As Integer

    '		On Error GoTo LoadFileToText_Err
    '		lngHandle = FreeFile
    '		FileOpen(lngHandle, sFileName, OpenMode.Binary)
    '		' read the string and close the file
    '		LoadFileToText = Space(LOF(lngHandle))
    '		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
    '		FileGet(lngHandle, LoadFileToText)
    '		FileClose(lngHandle)

    'LoadFileToText_Err: 
    '		If Err.Number Then
    '			LoadFileToText = String.Empty
    '		End If
    '		On Error GoTo 0
    '	End Function

    '% insGetSetting: se toman los valore del registro
    Public Function GetFem(ByVal Name As String, ByVal DefValue As String, Optional ByVal Group As String = "") As String
        GetFem = LoadSetting(Name, DefValue, Group)
    End Function

    'UPGRADE_NOTE: Default was upgraded to Default_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function LoadSetting(ByVal sKey As String, Optional ByRef Default_Renamed As Object = Nothing, Optional ByVal sGroup As String = "Settings") As String

        Dim lstrfile As String
        Dim lstrGroup As String
        Dim strResultado As String = ""
        Try
            mstrConfigContent = My.Application.Info.DirectoryPath
            If mstrConfigContent > String.Empty Then
                mstrConfigContent = Left(mstrConfigContent, 2)
            Else
                mstrConfigContent = "D:"
            End If

            mstrConfigContent = mstrConfigContent & "\VisualTIMENet\Configuration\Fem.xml"
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            lstrfile = Dir(mstrConfigContent)

            If lstrfile <> String.Empty Then
                sDate = CDate(Format(FileDateTime(mstrConfigContent), "yyyy/MM/dd"))
                mstrConfigContent = eRemoteDB.FileSupport.LoadFileToText(mstrConfigContent)
                lstrGroup = GetBlock(mstrConfigContent, sGroup, True)
                If lstrGroup <> String.Empty Then
                    strResultado = GetBlock(lstrGroup, sKey, True)
                End If
                If strResultado <> String.Empty Then
                    strResultado = strResultado & "  Instalación : " & sDate
                End If
            End If
            Return strResultado
        Catch ex As Exception
            Return strResultado
        End Try

    End Function

    Private Function GetBlock(ByRef sSource As String, ByVal sTag As String, Optional ByRef bNotDelete As Boolean = False) As String
        Dim strLabel As String
        Dim lngIniPosition As Integer
        Dim lngEndPosition As Integer

        strLabel = "<" & UCase(sTag) & ">"
        lngIniPosition = InStr(UCase(sSource), strLabel)
        If lngIniPosition > 0 Then
            lngIniPosition = lngIniPosition + Len(strLabel)
            strLabel = "</" & UCase(sTag) & ">"
            lngEndPosition = InStr(lngIniPosition, UCase(sSource), strLabel)
            If lngEndPosition > 0 Then
                GetBlock = Mid(sSource, lngIniPosition, lngEndPosition - lngIniPosition)
                If Not bNotDelete Then
                    sSource = Left(sSource, lngIniPosition + 1) & Mid(sSource, lngEndPosition)
                End If
            End If
        End If
    End Function
End Class






