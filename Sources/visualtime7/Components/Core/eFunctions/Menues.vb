Option Strict Off
Option Explicit On
Imports eRemoteDB
Public Class Menues
	'%-------------------------------------------------------%'
	'% $Workfile:: Menues.cls                               $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 13/11/03 18.13                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	Private Const IMAGESOURCE As String = "/VTimeNet/Images/"
	
	Public Enum eZone
		cintHeader = 1
		cintFolder = 2
	End Enum
	
	Public Enum TypeActions
		'**+Navigation menu
		'+ Menú de Navegación
		clngMenuNavegation = 200
		'**+Main menu
		'+ Menú principal
		clngActionMainMenu = 201
		'**+Errors menu
		'+ Menú de Errores
		clngActionErrorMenu = 202
		'**+Prevous window
		'+ Ventana anterior
		clngactionpreviouswindow = 203
		'**+Go
		'+ Ir
		clngActionGo = 204
		'**+Exit the system
		'+ Salir del sistema
		clngActionBye = 205
		'**+Exit the errors system
		'+ Salir del Sistema de Errores
		clngActionByeError = 206
		'**+Go to the consult
		'+ Ir a la consulta general
		clngActionGenQue = 207
		'**+Actions menu
		'+ Menú de Acciones
		clngMenuActions = 300
		'**+Record
		'+ Registrar
		clngActionadd = 301
		'**+Update
		'+ Actualizar
		clngActionUpdate = 302
		'**+Cut
		'+ Cortar
		clngActioncut = 303
		'**+Entry
		'+ Entrar
		clngActionInput = 304
		'**+Modify
		'+ Modificar
		clngActionModify = 305
		'**+Duplicate
		'+ Duplicar
		clngActionDuplicate = 306
		'**+Cut table
		'+ Cortar tabla
		clngActionCutTable = 307
		'**+Duplicate table
		'+ Duplicar tabla
		clngActionCopyTable = 308
		'**+Currency
		'+ Moneda
		clngActionCurrency = 309
		'**+Duplicate product
		'+ Duplicar producto
		clngActionDuplicateProduct = 310
        '**+Used for conexion changed
        '+ Usado para cambiar la conección
        clngChangeConnection = 311
        '**+Accept
        '+ Aceptar
        clngAcceptdataAccept = 390
		'**+Cancel
		'+ Cancelar
		clngAcceptdataCancel = 391
		'**+Finish
		'+ Finalizar
		clngAcceptdatafinish = 392
        '**+Ignore changes
        '+ Ignorar Cambios
        clngAcceptdataRefresh = 393
        '**+Used for go to the batch_process
        '+ Usado para ir a ventana de procesos batch
        clngShowBatchProcess = 396
        '**+Consult menu
        '+ Menú de Consulta
        clngMenuInquiry = 400
        '**+Consult
        '+ Consulta
        clngActionQuery = 401
        '**+Condition
        '+ Condición
        clngActionCondition = 402
        '**+Revise
        '+ Revisar
        clngActionReview = 403
        '**+First
        '+ Primero
        clngActionFirst = 490
        '**+Previous
        '+ Anteriores
        clngActionPrevious = 491
        '**+Next
        '+ Próximos
        clngActionNext = 492
        '**+Last
        '+ Ultimo
        clngActionLast = 493
        '**+Help menu
        '+ Menú de Ayuda
        clngMenuHelp = 600
        '**+Help
        '+ Ayuda
        clngActionHelp = 601
        '**+Last globals...
        '+ Últimas globales...
        clngGlobalsHelp = 602
        '**+About...
        '+ Acerca de...
        clngActionAbout = 603
        '**+Menu intems delimitator
        '+ Delimitador de Items de Menú
        clngMenuDelimiter = 99
        '**+Used for the special links
        '+ Usado para los enlaces especiales
        clngActionLinkSpecial = 700
    End Enum

    Enum TypeForm
        clngSpeWithHeader = 1
        clngSeqWithHeader = 2
        clngRepWithOutHeader = 3
        clngSeqWithOutHeader = 4
        clngSpeWithOutHeader = 5
        clngRepWithHeader = 6
        clngFraSpecific = 7
        clngMenu = 8
        clngFraRepetitive = 9
        clngGeneralTable = 10
        clngWindowsPopUp = 11
    End Enum


    '-Objeto para crear los botones de la barra de herramientas
    Private mobjButton As eFunctions.Values

    Public sSche_code As String

    Private mstrMenu As String

    Private mstrMessage12103 As String

    Private mstrCachePath As String

    '+Definicion de variables indicadoras de acceso por nivel de consulta y modificacion.
    Public mblnInqAcces As Boolean

    Public mblnAmeAcces As Boolean

    '-Variable que guarda el número de sesión
    Public sSessionID As String

    '-Código del usuario
    Public nUsercode As Integer

    '% MakeMenu: Se crea el menú de la transacción
    Public Function MakeMenu(ByVal sCodispl As String, ByVal sPage As String, ByVal nZone As Short, ByVal sName As String, Optional ByVal sCompany As String = "", Optional ByVal sSche_code As String = "") As String
        Dim lobjWinActions As eRemoteDB.Query
        Dim lobjAction As eRemoteDB.Query
        Dim lclsSession As ASPSupport = New ASPSupport
        Dim lstrOnClick As String
        Dim lstrJS As String = ""
        Dim lstrZone As String
        Dim lstrTlb As String = ""
        Dim lblnFirst As Boolean
        Dim lintIndex As Short
        Dim lintAction As Short
        Dim lintType As Short
        Dim lintWindowsType As TypeForm
        Dim lblnInput As Boolean
        Dim lstrCodisp As String
        Dim lstrScheCode As String
        Dim lstrFilename As String
        Dim lstrDesMultiCompany As String
        Dim lintAmeLevel As Short
        Dim lintInqlevel As Short
        Dim lintModules As Short
        Dim lstrActionDenied As String
        Dim lstrLinkSpecial As String

        On Error GoTo ErrorHandler

        '+Se pensó dejar el código en mayusculas,
        '+pero al ejecutar las pagínas de validaciones existen casos
        '+en que codispl llega como "BC003_k", pero la busqueda se
        '+hace por "BC003_K", por lo que falla.
        '+Por eso se deja como está, sin hacer Ucase()
        '+    sCodispl = UCase$(sCodispl)

#If LOG Then
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|MakeMenu|" & sCodispl, sSessionID
#End If

        If sSche_code = String.Empty Then
            lclsSession = New eRemoteDB.ASPSupport
            lstrScheCode = UCase(lclsSession.GetASPSessionValue("sSche_code"))
        Else
            lstrScheCode = UCase(sSche_code)
        End If
        If sCompany = String.Empty Then
            If lclsSession Is Nothing Then
                lclsSession = New eRemoteDB.ASPSupport
            End If
            lstrDesMultiCompany = lclsSession.GetASPSessionValue("sDesMultiCompany")
        Else
            lstrDesMultiCompany = sCompany
        End If
        'UPGRADE_NOTE: Object lclsSession may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSession = Nothing


        If lclsSession Is Nothing Then
            lclsSession = New eRemoteDB.ASPSupport
        End If

        lstrLinkSpecial = lclsSession.GetASPQueryStringValue("sLinkSpecial")
        If lstrLinkSpecial = String.Empty Then
            lstrLinkSpecial = "2"
        End If

        lstrFilename = mstrCachePath & "\" & lstrScheCode & "_" & sCodispl & IIf(lstrLinkSpecial = "1", "_NoMenu", String.Empty) & ".html"

        'lstrFilename = mstrCachePath & "\" & lstrScheCode & "_" & sCodispl & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".html"

        MakeMenu = eRemoteDB.FileSupport.LoadFileToText(lstrFilename)

        If MakeMenu = String.Empty Then

            lobjWinActions = New eRemoteDB.Query
            lblnFirst = True
            lstrZone = String.Empty

            With lobjWinActions

                If .OpenQuery("Windows", "sDescript,nWindowty,sCodisp,nInqLevel,nAmelevel,nModules", "sCodispl='" & UCase(sCodispl) & "'") Then
                    sName = .FieldToClass("sDescript")
                    lintWindowsType = .FieldToClass("nWindowty")
                    lstrCodisp = .FieldToClass("sCodisp")
                    lintAmeLevel = .FieldToClass("nAmelevel")
                    lintInqlevel = .FieldToClass("nInqLevel")
                    '+Si es carpeta, estonces se activa indicador de modulo para
                    '+realizar validacion contra levels
                    If lintWindowsType = 8 Then
                        lintModules = 1
                        '+Si no es carpeta se asigna transaccion
                    Else
                        lintModules = 2
                    End If

                    '+Se valida si tiene nivel para consultar y/o actualizar
                    Call ValActionLevel(UCase(sCodispl), lintModules, lstrScheCode, lintInqlevel, lintAmeLevel)
                    lstrActionDenied = ""

                    '+Se indican las acciones restringuidas
                    '+El menú 300 (Acciones) nunca queda deshabilitado ya que siempre se le cargan
                    '+algunas acciones hijas (390-Aceptar, 391-Cancelar, 392-Finalizar, 393-Refrescar)
                    If Not mblnInqAcces And Not mblnAmeAcces Then
                        lstrActionDenied = "400,401,402,403,490,491,492,493,301,302,303,304,305,306,307,308,310"
                    Else
                        If Not mblnAmeAcces Then
                            lstrActionDenied = "301,302,303,304,305,306,307,308,310"
                        End If
                        If Not mblnInqAcces Then
                            lstrActionDenied = "400,401,402,403,490,491,492,493"
                        End If
                    End If
                    .CloseQuery()

                    '+Se buscan las acciones asociadas a la transacciones
                    '+Se ordenan por accion, ya que primero viene las 200, luego las 300, etc.
                    'If .OpenQuery("Win_Actions", "nAction", "sCodispl='" & UCase(IIf(sCodispl Like "MA####", "MA1000", sCodispl)) & "' and nAction <> 99", "nAction") Then
                    If .OpenQuery("Win_Actions", "nAction", "sCodispl='" & UCase(IIf(sCodispl Like "MA####", "MA1000", sCodispl)) & "' and nAction <> 99", "nAction") Then
                        lstrJS = "<SCRIPT LANGUAGE= ""JavaScript"">" & vbCrLf & "top.frames[""fraSequence""].pintZone=1;" & vbCrLf & "pstrCodispl='" & sCodispl & "';</SCRIPT>" & vbCrLf

                        If lstrLinkSpecial <> "1" Then
                            lstrJS = lstrJS & "<SCRIPT src=""/VTimeNet/Scripts/stuHover.js"" type=""text/javascript""></SCRIPT>"
                            lstrJS = lstrJS & "<span id=""tabNav""><ul id=""nav"">"
                        End If

                        lintIndex = -1
                        lintType = 1
                        insMakeToolBar(0, lstrTlb, 0, String.Empty)
                        lobjAction = New eRemoteDB.Query

                        Do While True
                            '+Tipo de menu es "Acciones"
                            If lintType = 3 Then
                                If Not .EndQuery Then
                                    lintAction = .FieldToClass("nAction")
                                    '+Si pasó a sgte tipo de menu, aun se deben cargar las acciones fijas
                                    If lintAction >= 400 Then
                                        lintAction = insStaticMenu(0, lintType, lintIndex)
                                        lintIndex = lintIndex + 1
                                    Else
                                        '+Se apunta indicador a primer menu fijo
                                        lintIndex = 1
                                        .NextRecord()
                                    End If
                                Else
                                    lintAction = insStaticMenu(0, lintType, lintIndex)
                                    lintIndex = lintIndex + 1
                                End If

                                '+Tipo de menu es "Consulta"
                            ElseIf lintType = 4 Then
                                If Not .EndQuery Then
                                    lintAction = .FieldToClass("nAction")
                                    .NextRecord()
                                Else
                                    lintAction = insStaticMenu(0, lintType, lintIndex)
                                    lintIndex = lintIndex + 1
                                End If

                                '+Otros tipos de menu (Sistema, Ayuda, etc.,)
                            Else
                                lintAction = insStaticMenu(0, lintType, lintIndex)
                                lintIndex = lintIndex + 1
                            End If

                            '+Si accion no esta restringuida
                            If InStr(1, lstrActionDenied, CStr(lintAction), CompareMethod.Text) = 0 Then
                                If lintAction = -1 Then
                                    lintType = lintType + 1
                                    If lintType = 3 Or lintType = 4 Then
                                        lintIndex = 0
                                    Else
                                        lintIndex = -1
                                    End If
                                ElseIf Not lobjAction.OpenQuery("Actions", "sDescript, sHel_actio", "nAction=" & lintAction & " AND sStatregt = '1'", "nAction") Then
                                    If lintAction Mod 100 = 0 Then
                                        lintType = lintType + 1
                                        If lintType = 3 Or lintType = 4 Then
                                            lintIndex = 0
                                        Else
                                            lintIndex = -1
                                        End If
                                    End If
                                Else
                                    If lintAction Mod 100 = 0 Then
                                        If lstrLinkSpecial <> "1" Then
                                            If Not lblnFirst Then
                                                '+ Se agrega una nueva categoría al menú
                                                lstrJS = lstrJS & "</ul></li>"
                                            End If
                                            '+ Se define el ancho (en pixels) del menú
                                            lstrJS = lstrJS & "<li class=""top"">" &
                                                                "<a class=""top_link""><span class=""down"">" & Replace(lobjAction.FieldToClass("sDescript"), "&", String.Empty) & "</span></a>" &
                                                                "<ul class=""sub"">"
                                        End If
                                        lblnFirst = False
                                    ElseIf lintAction <> 99 Then
                                        If lintAction = TypeActions.clngActionInput Then
                                            lblnInput = True
                                        End If
                                        lstrOnClick = insMakeLink(sCodispl, lstrCodisp, lintAction, lintWindowsType)
                                        If lstrLinkSpecial <> "1" Then
                                            'lstrJS = lstrJS & "<li><a href=" & lstrOnClick & ">" & insMakeImage(lintAction) & "&nbsp;" & Replace(lobjAction.FieldToClass("sDescript"), "&", String.Empty) & "</a></li>"
                                            If lstrOnClick <> "201" Then
                                                lstrJS = lstrJS & "<li><a href=" & lstrOnClick & ">" & insMakeImage(lintAction) & "&nbsp;" & Replace(lobjAction.FieldToClass("sDescript"), "&", String.Empty) & "</a></li>"
                                            Else
                                                lstrJS = lstrJS & "<li><a href=" & "javascript:top.close();" & ">" & insMakeImage(lintAction) & "&nbsp;" & Replace(lobjAction.FieldToClass("sDescript"), "&", String.Empty) & "</a></li>"
                                                'lstrJS = lstrJS & "<li><a href=" & "#" & " onclick=" & "self.close();" & ">" & insMakeImage(lintAction) & "&nbsp;" & Replace(lobjAction.FieldToClass("sDescript"), "&", String.Empty) & "</a></li>"
                                            End If
                                        End If
                                        '+Se crea el ícono de la barra de herramientas
                                        'If lintAction = TypeActions.clngActionadd Or lintAction = TypeActions.clngActionUpdate Or lintAction = TypeActions.clngActionQuery Or lintAction = TypeActions.clngActioncut Or lintAction = TypeActions.clngActionInput Or lintAction = TypeActions.clngAcceptdataAccept Or lintAction = TypeActions.clngAcceptdataCancel Or lintAction = TypeActions.clngAcceptdatafinish Or lintAction = TypeActions.clngAcceptdataRefresh Or lintAction = TypeActions.clngActionCondition Or lintAction = TypeActions.clngActionDuplicateProduct Or lintAction = TypeActions.clngActionDuplicate Or lintAction = TypeActions.clngChangeConnection Then
                                        If lintAction = TypeActions.clngActionadd Or lintAction = TypeActions.clngActionUpdate Or lintAction = TypeActions.clngActionQuery Or lintAction = TypeActions.clngActioncut Or lintAction = TypeActions.clngActionInput Or lintAction = TypeActions.clngAcceptdataAccept Or lintAction = TypeActions.clngAcceptdataCancel Or lintAction = TypeActions.clngAcceptdatafinish Or lintAction = TypeActions.clngAcceptdataRefresh Or lintAction = TypeActions.clngActionCondition Or lintAction = TypeActions.clngActionDuplicateProduct Or lintAction = TypeActions.clngActionDuplicate Or lintAction = TypeActions.clngChangeConnection Or lintAction = TypeActions.clngShowBatchProcess Then
                                            insMakeToolBar(lintAction, lstrTlb, 1, lstrOnClick, lobjAction.FieldToClass("sHel_actio"))
                                        End If
                                    End If
                                    lobjAction.CloseQuery()
                                End If

                            Else
                                '+Se crea comentario en archivo para indicar que accion no está permitida
                                lstrJS = lstrJS & "//+Accion " & lintAction & " no esta permitida" & vbCrLf
                            End If

                            '+Si ya se procesaron todos los tipos existentes de menu se sale del proceso
                            If lintType > 6 Then
                                Exit Do
                            End If
                        Loop

                        insMakeToolBar(0, lstrTlb, 2, String.Empty)

                        If lstrLinkSpecial <> "1" Then
                            lstrJS = lstrJS & "</ul></li></ul></span>" & vbCrLf
                        End If

                        If lintWindowsType = TypeForm.clngRepWithOutHeader Or lintWindowsType = TypeForm.clngGeneralTable Then
                            lstrZone = setZone(1, sCodispl, sPage)
                        End If

                        .CloseQuery()
                    Else
                        lstrJS = "No se encontró la información de la transacción"
                    End If
                End If
            End With
            MakeMenu = lstrJS & lstrTlb & lstrZone
            If lblnInput Then
                MakeMenu = MakeMenu & "<SCRIPT>ClientRequest(" & TypeActions.clngActionInput & ");</SCRIPT>" & vbCrLf
            End If
            MakeMenu = MakeMenu & "<SCRIPT>setPointer('');</SCRIPT>" & vbCrLf

            Call eRemoteDB.FileSupport.SaveBufferToFile(lstrFilename, Left(sName & Space(60), 60) & MakeMenu)

            'UPGRADE_NOTE: Object lobjWinActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjWinActions = Nothing
            'UPGRADE_NOTE: Object lobjAction may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjAction = Nothing

        Else
            sName = Trim(Left(MakeMenu, 60))
            MakeMenu = Mid(MakeMenu, 61)
        End If

        sName = sName & " (" & lstrDesMultiCompany & ")"
        MakeMenu = MakeMenu & "<SCRIPT>top.document.title='" & sName & "';</SCRIPT>"


#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		AddBufferToFile sSessionID & "|Finish|Method|MakeMenu |" & sCodispl, sSessionID
#End If

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lobjWinActions may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjWinActions = Nothing
        'UPGRADE_NOTE: Object lobjAction may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjAction = Nothing
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Menues.MakeMenu(sCodispl,sPage,nZone,sName)", New Object() {sCodispl, sPage, nZone, sName})
    End Function

    '**% insStaticMenu
    '% insStaticMenu
    Private Function insStaticMenu(ByVal lintModule As Short, ByVal lbytType As Byte, ByVal llngIndex As Integer) As TypeActions
        insStaticMenu = -1

        '+ Si el tipo de menú es mayor que 1.
        If lbytType > 1 Then
            Select Case lbytType

                '**+ Actions for the Navigation menu
                '+Acciones para el menu de Navegación
                Case 2
                    Select Case llngIndex
                        Case -1
                            insStaticMenu = TypeActions.clngMenuNavegation
                        Case 0
                            insStaticMenu = IIf(lintModule = 33, TypeActions.clngActionErrorMenu, TypeActions.clngActionMainMenu)
                        Case 1
                            insStaticMenu = TypeActions.clngactionpreviouswindow
                        Case 2
                            insStaticMenu = TypeActions.clngActionGo
                        Case 3
                            insStaticMenu = IIf(lintModule = 33, TypeActions.clngActionByeError, TypeActions.clngActionBye)
                        Case 4
                            insStaticMenu = TypeActions.clngActionGenQue
                    End Select

                    '**+ Actions for the complement of the actions menu
                    '+Acciones para el complemento del menu de acciones
                Case 3
                    Select Case llngIndex
                        Case 0
                            insStaticMenu = TypeActions.clngMenuActions
                        Case 1
                            insStaticMenu = TypeActions.clngMenuDelimiter
                        Case 2
                            insStaticMenu = TypeActions.clngAcceptdataAccept
                        Case 3
                            insStaticMenu = TypeActions.clngAcceptdataCancel
                        Case 4
                            insStaticMenu = TypeActions.clngAcceptdatafinish
                        Case 5
                            insStaticMenu = TypeActions.clngAcceptdataRefresh
                    End Select

                    '**+ Actions for the Help menu
                    '+Acciones para el menu de Ayuda
                Case 6
                    Select Case llngIndex
                        Case -1
                            insStaticMenu = TypeActions.clngMenuHelp
                        Case 0
                            insStaticMenu = TypeActions.clngActionHelp
                        Case 1
                            insStaticMenu = TypeActions.clngGlobalsHelp
                        Case 2
                            insStaticMenu = TypeActions.clngMenuDelimiter
                        Case 3
                            insStaticMenu = TypeActions.clngActionAbout
                    End Select
            End Select
        End If
    End Function

    '**%Objective:
    '**%Parameters:
    '**%    nAction
    '%Objetivo:
    '%Parámetros:
    '%      nAction
    Private Function insMakeImage(ByVal nAction As Integer) As String
        insMakeImage = "<img border=0 src=""/VTimeNet/Images/"
        Select Case nAction
            Case TypeActions.clngActionQuery,
                          TypeActions.clngActionadd,
                          TypeActions.clngActionUpdate,
                          TypeActions.clngAcceptdataAccept,
                          TypeActions.clngAcceptdataCancel,
                          TypeActions.clngAcceptdatafinish,
                          TypeActions.clngAcceptdataRefresh,
                          TypeActions.clngActionCondition,
                          TypeActions.clngActionGenQue,
                          TypeActions.clngActioncut,
                          TypeActions.clngShowBatchProcess
                insMakeImage = insMakeImage & "A" & nAction.ToString() & "Off.png"">"
            Case Else
                insMakeImage = insMakeImage & "Blank.gif"">"
        End Select
    End Function


    ''**% insMakeLink:
    '% insMakeLink:
    ''' <history>
    ''' [Gherson Isaac Mendoza Nery]   06/11/2018   Modificado
    ''' Motivo: Tarea id 95204 REQ003-01 Investigar cómo eliminar las opciones de Menú de Acciones que llevan al menú principal del Back Office
    ''' Descripcion: Se agrego regla de negocio en el case para que cuando la accion del menu sea regresar al menu principal devuelva 
    ''' el codigo 201 para ser tratado posteriormente para identificar y no redireccionar la pagina ahora va cerrar la ventana emergente.
    ''' </history>
    Private Function insMakeLink(ByVal lstrCodispl As String, ByVal lstrCodisp As String, ByVal llngAction As Integer, ByVal lintWindowType As TypeForm) As String
        Select Case llngAction
            Case TypeActions.clngActionMainMenu
                insMakeLink = "201"
            Case TypeActions.clngActionQuery, TypeActions.clngActionadd, TypeActions.clngActionInput, TypeActions.clngActionUpdate, TypeActions.clngActioncut, TypeActions.clngAcceptdataAccept, TypeActions.clngAcceptdataCancel, TypeActions.clngAcceptdatafinish, TypeActions.clngAcceptdataRefresh, TypeActions.clngActionGo, TypeActions.clngactionpreviouswindow, TypeActions.clngActionMainMenu, TypeActions.clngActionCondition, TypeActions.clngActionFirst, TypeActions.clngActionPrevious, TypeActions.clngActionNext, TypeActions.clngActionLast, 394, TypeActions.clngActionGenQue, TypeActions.clngActionDuplicate, TypeActions.clngActionDuplicateProduct
                insMakeLink = """JAVASCRIPT: ClientRequest(" & llngAction & "," & lintWindowType & ");"""

            Case TypeActions.clngChangeConnection
                insMakeLink = """JAVASCRIPT:insChangeConnect('" & lstrCodispl & "');"""
            Case TypeActions.clngActionBye
                insMakeLink = """JAVASCRIPT:Logout(" & lintWindowType & ");"""
            Case TypeActions.clngActionHelp
                insMakeLink = """JAVASCRIPT:ShowHelp('" & lstrCodispl & "');"""
            Case TypeActions.clngActionAbout
                insMakeLink = """JAVASCRIPT:ShowAbout('" & lstrCodispl & "','" & lstrCodisp & "'," & lintWindowType & ");"""
            Case TypeActions.clngGlobalsHelp
                insMakeLink = """JAVASCRIPT:ShowLastValues();"""
            Case TypeActions.clngShowBatchProcess
                insMakeLink = """JAVASCRIPT:insShowBatchProcess();"""
            Case Else
                insMakeLink = """"""
        End Select
    End Function

    '**% insMakeToolBar: Defines the toolbar menu in a window
    '% insMakeToolBar:Define la barra de herramientas en una ventana
    Private Sub insMakeToolBar(ByVal lintAction As Short, ByRef lstrTlb As String, ByVal lintArea As Short, ByVal lstrOnClick As String, Optional ByVal sActionDes As String = "")
        Dim lstrFile As String

        If lintArea = 0 Then
            lstrTlb = "<BR><A NAME=""BeginPage""></A>" & vbCrLf & "<TABLE BORDER=""0"" ALIGN=RIGHT>" & vbCrLf & "<TR>" & vbCrLf & "<TD WIDTH=60% ALIGN=LEFT CLASS=HIGHLIGHTED>" & vbCrLf & "<LABEL><DIV ID=lblWaitProcess><BR></DIV></LABEL></TD>" & vbCrLf & "<TD WIDTH=20%>&nbsp</TD>"
        ElseIf lintArea = 1 Then
            If mobjButton Is Nothing Then
                mobjButton = New Values
            End If
            lstrFile = IMAGESOURCE & "A" & lintAction & "Off.png"
            lstrTlb = lstrTlb & "<TD>" & mobjButton.AnimatedButtonControl("A" & lintAction, lstrFile, sActionDes, , Replace(Replace(lstrOnClick, "JAVASCRIPT:", String.Empty), """", String.Empty)) & "</TD>" & vbCrLf & "<SCRIPT>document.images['" & "A" & lintAction & "'].belongtoolbar=true</SCRIPT>"
        ElseIf lintArea = 2 Then
            lstrTlb = lstrTlb & " </TR>" & vbCrLf & " </TABLE><BR> " & vbCrLf & "<SCRIPT>" & vbCrLf & "if (top.frames[""fraSequence""].plngMainAction==0){" & vbCrLf & "insHandImage(""A301"", true);" & vbCrLf & "insHandImage(""A302"", true);" & vbCrLf & "insHandImage(""A303"", true);" & vbCrLf & "insHandImage(""A304"", true);" & vbCrLf & "insHandImage(""A401"", true);" & vbCrLf & "insHandImage(""A390"", false);" & vbCrLf & "insHandImage(""A391"", false);" & vbCrLf & "insHandImage(""A392"", false);" & vbCrLf & "insHandImage(""A396"", false);" & vbCrLf & "insHandImage(""A393"", false);" & vbCrLf & "insHandImage(""A394"", true);" & vbCrLf & "}" & "</SCRIPT>" & vbCrLf
        End If
    End Sub
    '**insFindBatchProcess: find the values of the batch process and interface number that was used previously when the user click the Go to Batch Process button.
    '% insFindBatchProcess: encuentra los valores de proceso batch e interfaz que fue usado previamente cuando el usuariohizo click en el botón Ir a Proceso.
    Public Sub insFindBatchProcess(ByVal nUsercode As Short, ByVal sCodispl As String, ByVal dEffecdate As Date, ByRef linBatch_Out As Integer)
        Dim nBatchInterface As eRemoteDB.Execute
        nBatchInterface = New eRemoteDB.Execute
        With nBatchInterface
            .StoredProcedure = "insPre_BtcProcess"
            .Parameters.Add("nUserCode", nUsercode, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbNumeric, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBatch_out", linBatch_Out, Parameter.eRmtDataDir.rdbParamInputOutput, Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                linBatch_Out = .Parameters.Item("nBatch_Out").Value
            End If
        End With
    End Sub

    '% setZone: habilita/deshabilita las acciones del ToolBar, de acuerdo a la zona en donde
    '%          se encuentre (Header-Folder)
    Public Function setZone(ByVal nZone As Short, ByVal sCodispl As String, ByVal sName As String, Optional ByVal nWindowty As TypeForm = 0) As String
        Dim lobjWindows As eRemoteDB.Query
        Dim lblnInSequence As Boolean
        Dim lblnIsMassive As Boolean
        Dim lstrCommand As String
        Dim lblnIsRepWithOutHeader As Boolean

#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|setZone|" & sCodispl, sSessionID
#End If



        If sCodispl <> String.Empty Then
            If sName = String.Empty Or nWindowty = 0 Then
                lobjWindows = New eRemoteDB.Query
                With lobjWindows
                    If .OpenQuery("Windows", "sDescript, nWindowty", "sCodispl='" & UCase(sCodispl) & "'") Then
                        sName = .FieldToClass("sDescript")
                        nWindowty = .FieldToClass("nWindowty")
                        .CloseQuery()
                    End If
                End With
                'UPGRADE_NOTE: Object lobjWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjWindows = Nothing
            Else
            End If
            lblnInSequence = nWindowty = TypeForm.clngFraSpecific Or nWindowty = TypeForm.clngFraRepetitive

            lblnIsMassive = nWindowty = TypeForm.clngRepWithOutHeader Or nWindowty = TypeForm.clngRepWithHeader Or nWindowty = TypeForm.clngFraRepetitive Or nWindowty = TypeForm.clngGeneralTable

            lblnIsRepWithOutHeader = nWindowty = TypeForm.clngRepWithOutHeader Or nWindowty = TypeForm.clngGeneralTable

        End If

        lstrCommand = "<SCRIPT Language=JavaScript>top.frames[""fraSequence""].pintZone=" & nZone & ";" & vbCrLf & "var lblnQuery;" & vbCrLf

        If Not lblnIsRepWithOutHeader Then
            lstrCommand = lstrCommand & "var lintpos;" & vbCrLf & "lintpos = top.document.title.search("" / "");" & vbCrLf & "if (lintpos == -1)" & vbCrLf & "    lintpos = top.document.title.length;" & vbCrLf & "top.document.title= top.document.title.substr(0,lintpos) + "" / " & sName & """;" & vbCrLf
        End If

        lstrCommand = lstrCommand & "function SetupToolBar(){ top.frames[""fraHeader""].pstrCodispl=""" & sCodispl & """;" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A301""," & IIf(lblnIsRepWithOutHeader, " true", " false") & ");" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A302""," & IIf(lblnIsRepWithOutHeader, " true", " false") & ");" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A303"", false);" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A304"", false);" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A310"", false);" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A401""," & IIf(lblnIsRepWithOutHeader, " true", " false") & ");" & vbCrLf & "lblnQuery= (top.plngMainAction==401 " & IIf(lblnInSequence, " || top.fraSequence.pblnQuery", String.Empty) & ");" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A390""," & IIf(lblnInSequence, " !lblnQuery", " false") & ");" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A391"", !lblnQuery);" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A306"", lblnQuery && " & IIf(lblnIsRepWithOutHeader, " true", " false") & ");" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A392"", true);" & vbCrLf & "top.frames[""fraHeader""].insHandImage(""A393""," & IIf(lblnIsMassive, " false", " !lblnQuery") & ");" & vbCrLf & "top.fraHeader.insDisableHeader();" & vbCrLf & "top.frames[""fraHeader""].setPointer('');}" & vbCrLf & "</script>"

        lstrCommand = lstrCommand & "<SCRIPT>function InvokeSetupToolBar(){try{SetupToolBar();}" & vbCrLf & "catch(x){setTimeout('InvokeSetupToolBar()',150);}" & vbCrLf & "finally{}} </" & "Script>" & vbCrLf
        lstrCommand = lstrCommand & "<SCRIPT>InvokeSetupToolBar();</" & "Script>" & vbCrLf

        setZone = lstrCommand

#If LOG Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression LOG did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|setZone|" & sCodispl, sSessionID
#End If
    End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
        Dim clsConfig As New eRemoteDB.VisualTimeConfig
		
        mstrCachePath = clsConfig.LoadSetting("Cache", "C:\VisualTIMENet\VTimeNet\Cache", "Paths")
		'UPGRADE_NOTE: Object clsConfig may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		clsConfig = Nothing
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*Class_Terminate: Se ejecuta cuando se destruye la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mobjButton may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mobjButton = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%ValactionLevel: Valida el nivel del usuario respecto a los indicados
    Public Sub ValActionLevel(ByVal sCodispl As String, ByVal nModules As Short, ByVal sSche_code As String, ByVal nInqLevel As Short, ByVal nAmelevel As Short)
        Dim lrecLevels As eRemoteDB.Execute
        Dim lclsSecur_Sche As Object
        Dim lintInqlevel As Short
        Dim lintAmeLevel As Short
        Dim lintSecurLev As Short

        On Error GoTo ValActionLevel_err

        mblnInqAcces = False
        mblnAmeAcces = False

        '+Se busca tipo de acceso del esquema
        lclsSecur_Sche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")
        If lclsSecur_Sche.valSecur_sche(sSche_code) Then
            lintSecurLev = lclsSecur_Sche.nSecurlev
        Else
            '+Se asigna acceso restringido
            lintSecurLev = 2
        End If
        'UPGRADE_NOTE: Object lclsSecur_Sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecur_Sche = Nothing

        '+Esquema tiene acceso libre
        If lintSecurLev = 1 Then
            mblnInqAcces = True
            mblnAmeAcces = True

            '+Esquema tiene acceso restringuido
        Else

            lrecLevels = New eRemoteDB.Execute

            '**+Parameters definiton to stored prcoedure 'insudb.reaUsers'
            '**+Data read on 01/15/2001 15.29.55
            '+Definición de parámetros para stored procedure 'insudb.reaUsers'
            '+Información leída el 15/01/2001 15.29.55
            With lrecLevels
                .StoredProcedure = "valActionLevel"
                .Parameters.Add("sSche_Code", sSche_code, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sInd_Type", CStr(nModules), Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sCode_mt", sCodispl, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, Tables.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nInqlevel", lintInqlevel, Parameter.eRmtDataDir.rdbParamInputOutput, Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, Tables.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAmelevel", lintAmeLevel, Parameter.eRmtDataDir.rdbParamInputOutput, Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, Tables.eRmtDataAttrib.rdbParamNullable)
                If .Run(False) Then
                    lintInqlevel = .Parameters.Item("nInqLevel").Value
                    lintAmeLevel = .Parameters.Item("nAmelevel").Value
                    If lintInqlevel >= nInqLevel Then
                        mblnInqAcces = True
                    End If
                    If lintAmeLevel >= nAmelevel Then
                        mblnAmeAcces = True
                    End If
                Else
                    mblnAmeAcces = True
                    mblnInqAcces = True
                End If
            End With

            'UPGRADE_NOTE: Object lrecLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecLevels = Nothing
        End If

        Exit Sub

ValActionLevel_err:
        'UPGRADE_NOTE: Object lrecLevels may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecLevels = Nothing
        'UPGRADE_NOTE: Object lclsSecur_Sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSecur_Sche = Nothing
        mblnInqAcces = False
        mblnAmeAcces = False
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ProcError("Menues.ValActionLevel(sCodispl,nModules,sSche_code,nInqLevel,nAmelevel)", New Object() {sCodispl, nModules, sSche_code, nInqLevel, nAmelevel})
    End Sub
End Class






