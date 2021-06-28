Option Strict Off
Option Explicit On
Public Class Menu
	'%-------------------------------------------------------%'
	'% $Workfile:: Menu.cls                                 $%'
	'% $Author:: Mvazquez                                   $%'
	'% $Date:: 14/03/06 19:54                               $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	Public sSessionID As String
	Public sInitial As String
	Public sSche_code As String
	
	Private mstrMenu As String
	Private mstrMessage12103 As String
	Private mstrCachePath As String
	
	'%insLoadMenu: Se encarga de devolver el código HTML para la carga del Menu del sistema
	Public Function insLoadMenu(ByVal sModule As String) As String
		Dim lclsWindows As eSecurity.Windows
        Dim lclsFunctions As eFunctions.Values
        Dim lclsSecur_sche As eSecurity.Secur_sche
		Dim lclsGeneral As eGeneral.GeneralFunction
        Dim lstrMenu As String = ""
        Dim lblnIsValid As Boolean
		Dim lstrFilename As String
		Dim lintSecurLev As Short
		Dim lbooJustQuote As Boolean
		
		On Error GoTo insLoadMenu_Err
		
		lstrFilename = mstrCachePath & "\" & sSche_code & "_" & sModule & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".html"
		
        insLoadMenu = eRemoteDB.FileSupport.LoadFileToText(lstrFilename)
		
		If insLoadMenu = String.Empty Then
			
			'+ Si existe un módulo (Menú) a procesar.
			If sModule <> String.Empty Then
				
				lclsFunctions = New eFunctions.Values
				lbooJustQuote = (lclsFunctions.insGetSetting("JustQuote", "2", "Quoteizer") = "1")
				
				'+ Se obtiene el mensage de: "Transacción no permitida para su esquema"
				lclsGeneral = New eGeneral.GeneralFunction
				mstrMessage12103 = lclsGeneral.insLoadMessage(12103)

                lstrMenu = "<TABLE BORDER=0 WIDTH=""100%"" >" & vbCrLf & "<TD>" & vbCrLf & "<IMG ALIGN=MIDDLE SRC='/VTimeNet/Images/" & sModule & ".png'>&nbsp;"

                lclsWindows = New eSecurity.Windows
				
				With lclsWindows
					If .reaWindows(sModule) Then
						lstrMenu = lstrMenu & "  <LABEL ID=-1>" & .sDescript & "</LABEL>" & vbCrLf
					End If

                    lstrMenu = lstrMenu & "</TD>" & vbCrLf & "<TD WIDTH=35% ALIGN=LEFT CLASS=HIGHLIGHTED><LABEL><DIV ID=lblWaitProcess><BR></DIV></LABEL></TD>" & vbCrLf & "<TD ALIGN=RIGHT>" & vbCrLf & "<TABLE BORDER=0>" & vbCrLf & "<TD>" & vbCrLf & lclsFunctions.AnimatedButtonControl("btnGo", "/VTimeNet/images/A204Off.png", eFunctions.Values.GetMessage(10401),  , "insShowGoTo()") & "</TD>" & vbCrLf

                    If Not lbooJustQuote Then
                        lstrMenu = lstrMenu & "<TD>" & vbCrLf & lclsFunctions.AnimatedButtonControl("btnGeneralQue", "/VTimeNet/images/A207Off.png", eFunctions.Values.GetMessage(10406),  , "insGeneralQue()") & "</TD>" & vbCrLf
                    End If
                    If lbooJustQuote Then
                        lstrMenu = lstrMenu & "<TD>" & vbCrLf & lclsFunctions.ButtonHelp("MENU") & "</TD>" & vbCrLf & "<TD>" & vbCrLf & lclsFunctions.AnimatedButtonControl("btnInfoQuote", "/VTimeNet/images/infoquote.gif", eFunctions.Values.GetMessage(10408),  , "insInfoQuote()") & "</TD>" & vbCrLf
                    Else
                        lstrMenu = lstrMenu & "<TD>" & vbCrLf & lclsFunctions.ButtonAbout("MENU") & "</TD>" & vbCrLf
                    End If

                    lstrMenu = lstrMenu & "<TD>" & vbCrLf & lclsFunctions.AnimatedButtonControl("btnExit", "/VTimeNet/images/A205Off.png", eFunctions.Values.GetMessage(10405),  , "Logout()") & "</TD>" & vbCrLf & "</TABLE>" & vbCrLf & "</TD>" & vbCrLf & "</TABLE>" & vbCrLf

                    '+Si es una transaccion disponible
                    If .sStatregt = "1" Then
						lstrMenu = lstrMenu & "<SCRIPT>" & vbCrLf & "function generateTree(){" & vbCrLf & "var lstrCod, lstrDesc;" & vbCrLf & vbCrLf & "lstrCod='" & sModule & "';" & vbCrLf & "lstrDesc='" & lclsWindows.sDescript & "';" & vbCrLf & "foldersTree = folderNode(lstrDesc,""/VTimeNet/images/" & sModule & "T.gif"",""/VTimeNet/images/" & sModule & "T.gif"",1)" & vbCrLf
						
						'+Antes de cargar las transacciones, se verifica una vez si se debe validar cada
						'+una de dichas transascciones
						lclsSecur_sche = New eSecurity.Secur_sche
						
						'+Si no encuentra esquema, se indica por omision que realice validaciones,
						'+para evitar que ingrese como supervisor
						'+(aunque de todas formas no existiran registros en Levels)
						If Not lclsSecur_sche.Find(sSche_code) Then
							lintSecurLev = 2
						Else
							lintSecurLev = lclsSecur_sche.nSecurlev
						End If
						'UPGRADE_NOTE: Object lclsSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsSecur_sche = Nothing
						
						Call LoadSubMenues(sModule, String.Empty, lintSecurLev, sSche_code)
						
						lstrMenu = lstrMenu & mstrMenu
						
						lstrMenu = lstrMenu & vbCrLf & "}" & vbCrLf & "initializeTree();UpdateDiv('lblWaitProcess','<BR>','');" & vbCrLf & "</SCRIPT>"
					End If
				End With
			End If
            Call eRemoteDB.FileSupport.SaveBufferToFile(lstrFilename, lstrMenu)
			insLoadMenu = lstrMenu
		End If
		
insLoadMenu_Err: 
		If Err.Number Then
			insLoadMenu = insLoadMenu & Err.Description
		End If
		On Error GoTo 0
        lclsWindows = Nothing
        lclsFunctions = Nothing
        lclsGeneral = Nothing
        lclsSecur_sche = Nothing
	End Function
	
	'%LoadSubMenues: Rutina que permite cargar las opciones de menú de una rama de un módulo especifico.
	Private Sub LoadSubMenues(ByVal sCodispl As String, ByVal sFolder As String, Optional ByVal nValidate As Short = 2, Optional ByVal sSche_code As String = "")
		Dim lcolWindowsRemote As eSecurity.Windowss
		Dim lclsWindowsRemote As eSecurity.Windows
		Dim lstrNewCodispl As String
		Dim lstrVariable As String
		Dim lstrUrl As String
		
		On Error GoTo LoadSubMenues_Err
		
		If sFolder = String.Empty Then
			sFolder = "foldersTree"
		End If
		
		lstrVariable = String.Empty
		
		lcolWindowsRemote = New eSecurity.Windowss
		If lcolWindowsRemote.FindCodMen(sCodispl, nValidate, sSche_code) Then
			'+Se cargan cada uno de los nietos y demás descendencia de la raiz. (3er nivel del menú en adelante).
			For	Each lclsWindowsRemote In lcolWindowsRemote
				With lclsWindowsRemote
					lstrNewCodispl = .sCodispl
					'+Se crea objeto de la transaccion raiz
					If lstrVariable = String.Empty Then
						lstrVariable = "lobj" & Replace(lstrNewCodispl, "-", "")
						mstrMenu = mstrMenu & vbCrLf & "var " & lstrVariable & ";"
					End If
					
					'+Si la transaccion es tipo menu(subcarpeta), se carga la descendencia
					If .nWindowTy = 8 Then
						mstrMenu = mstrMenu & vbCrLf & lstrVariable & " = appendChild(" & sFolder & ", folderNode('" & .sDescript & "','',''))"
						
						'+Si se deben hacer validaciones de las transacciones
						If nValidate <> 1 Then
							If .nIndPermitted = 1 Then
								'+Validar, considerando raiz habilitada
								nValidate = 3
							Else
								'+Validar, considerando raiz deshabilitada
								nValidate = 4
							End If
						End If
						
						Call LoadSubMenues(lstrNewCodispl, lstrVariable, nValidate, sSche_code)
					Else
						'+ Si la transacción está permitida por el esquema de seguridad
						If .nIndPermitted = 1 Then
                            lstrUrl = insMakeURL(.sCodisp, .sDescript, .nWindowTy, .sFoldername, .sExe_name, .nHeight, .sCodispl)
							mstrMenu = mstrMenu & vbCrLf & "lstrCod='" & lstrNewCodispl & "';" & vbCrLf & "lstrDesc='" & .sDescript & "';" & vbCrLf & "    " & lstrVariable & " = leafNode2(generateDocEntry(lstrCod," & .nImg_index & ", lstrDesc, 'insGoTo(""" & lstrUrl & """);', ''),'','');" & vbCrLf & "    appendChild(" & lstrVariable & ", generateDocEntry(lstrCod,0, lstrDesc, 'portugal.html', ''));" & vbCrLf & "    appendChild(" & sFolder & ", " & lstrVariable & ");" & vbCrLf
						Else
							'+ Si la transacción no está permitida por el esquema de seguridad, se asigna mensaje de error respectivo.
							mstrMenu = mstrMenu & vbCrLf & "    " & lstrVariable & " = leafNode2(generateDocEntry('" & lstrNewCodispl & "'," & 10 & ", '" & .sDescript & "', 'javascript:alert("" " & mstrMessage12103 & """);', ''),'','')"
							mstrMenu = mstrMenu & vbCrLf & "    " & "appendChild(" & lstrVariable & ", generateDocEntry('" & lstrNewCodispl & "',0, '" & .sDescript & "', 'portugal.html', ''))"
							mstrMenu = mstrMenu & vbCrLf & "    " & "appendChild(" & sFolder & ", " & lstrVariable & "); "
							
						End If
					End If
				End With
			Next lclsWindowsRemote
		End If
        lcolWindowsRemote = Nothing
        lclsWindowsRemote = Nothing
		
LoadSubMenues_Err: 
		If Err.Number Then
			On Error GoTo 0
            lcolWindowsRemote = Nothing
            lclsWindowsRemote = Nothing
		End If
	End Sub
	
	Public Function Modules(ByVal sHistory As String, ByVal sOldModule As String) As String
		Dim lrecWindows As eRemoteDB.Query
		Dim lobjValues As eFunctions.Values
		Dim lstrBuffer As String
		Dim lstrCodmen As String
		Dim lstrButton As String
		Dim lstrInitial As String
		Dim lstrString As String = String.Empty
		Dim lstrFilename As String
		
		lstrFilename = mstrCachePath & "\Modules" & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".html"
		
        Modules = eRemoteDB.FileSupport.LoadFileToText(lstrFilename)
		
		If Modules = String.Empty Then
			
			lrecWindows = New eRemoteDB.Query
			lobjValues = New eFunctions.Values
			lobjValues.sSessionID = sSessionID
			
			lstrString = ""
			With lrecWindows
				lstrBuffer = "<TABLE>"
				lstrInitial = String.Empty
				
				'+ Se carga el menu de la ultima transaccion de la cual proviene el usuario
				
				If sHistory <> String.Empty And Left(sHistory, 2) <> "ER" Then
					lstrInitial = Left(sHistory, 8)
					If lstrInitial <> String.Empty Then
						If .OpenQuery("Windows", "sCodmen", "sCodispl = """ & Trim(lstrInitial) & """") Then
							lstrCodmen = .FieldToClass("sCodmen")
							Do 
								If .OpenQuery("Windows", "sCodmen", "sCodispl = """ & Trim(lstrCodmen) & """") Then
									If .FieldToClass("sCodmen") <> "MENU" Then
										lstrCodmen = .FieldToClass("sCodmen")
									Else
										lstrInitial = lstrCodmen
										lstrCodmen = String.Empty
									End If
								Else
									lstrCodmen = String.Empty
									lstrInitial = String.Empty
								End If
							Loop Until lstrCodmen = String.Empty
						Else
							lstrInitial = String.Empty
						End If
					End If
					If lstrInitial <> String.Empty Then
						sInitial = lstrInitial
						lstrInitial = "MenuName.aspx?sModule=" & lstrInitial
					End If
				End If
				
				If lstrInitial = String.Empty Then
					If sOldModule <> String.Empty Then
						lstrInitial = "MenuName.aspx?sModule=" & sOldModule
					End If
				End If
				
				If .OpenQuery("Windows", "*", "sCodmen = 'MENU' and sStatregt = '1' ", "nSequence") Then
					Do While Not .EndQuery
						lstrButton = "MenuName.aspx?sModule=" & .FieldToClass("sCodispl")
						If lstrInitial = "" Then
							lstrInitial = lstrButton
						End If
						If lstrString = "" Then
                            lstrString = eFunctions.Values.GetMessage(224)
                        End If
						If lstrString <> String.Empty Then
                            lstrButton = lobjValues.AnimatedButtonControl(.FieldToClass("sCodispl"), "/VTimeNet/images/" & .FieldToClass("sCodispl") & ".png", Trim(lstrString) & " " & LCase(.FieldToClass("sDescript")),  , "LoadModules('" & .FieldToClass("sCodispl") & "');")
                        Else
                            lstrButton = lobjValues.AnimatedButtonControl(.FieldToClass("sCodispl"), "/VTimeNet/images/" & .FieldToClass("sCodispl") & ".png", .FieldToClass("sDescript"),  , "LoadModules('" & .FieldToClass("sCodispl") & "');")
                        End If
						lstrBuffer = lstrBuffer & "<TR><TD ALIGN=CENTER><LABEL ID=-1 CLASS=TINY>" & lstrButton & "<BR>" & .FieldToClass("sDescript") & "</></LABEL></TD></TR>"
						.NextRecord()
					Loop 
					.CloseQuery()
				End If
				lstrBuffer = lstrBuffer & "</TABLE>"
			End With
			If lstrInitial <> String.Empty Then
				lstrBuffer = lstrBuffer & "<SCRIPT> if (typeof(top.FraHeader)!='undefined') top.FraHeader.document.location.href='" & lstrInitial & "';</SCRIPT>"
			End If
            lrecWindows = Nothing
            lobjValues = Nothing
			Modules = lstrBuffer
            Call eRemoteDB.FileSupport.SaveBufferToFile(lstrFilename, lstrBuffer)
		End If
	End Function
	
	'% insMakeURL: Crea un URL con las paginas
	Public Function insMakeURL(ByVal sCodisp As String, ByVal sDescript As String, ByVal nWindowTy As Integer, ByVal sModule As String, ByVal sProject As String, ByVal nHeight As Integer, ByVal sCodispl As String) As String
        Dim lstrBaseName As String = ""
        Select Case nWindowTy
			Case eFunctions.Menues.TypeForm.clngSpeWithHeader, eFunctions.Menues.TypeForm.clngRepWithHeader
				lstrBaseName = "SpeWHeader"
				
			Case eFunctions.Menues.TypeForm.clngSeqWithHeader, eFunctions.Menues.TypeForm.clngSeqWithOutHeader
				lstrBaseName = "SecWHeader"
				
			Case eFunctions.Menues.TypeForm.clngSpeWithOutHeader, eFunctions.Menues.TypeForm.clngRepWithOutHeader, eFunctions.Menues.TypeForm.clngGeneralTable
				lstrBaseName = "SpeWOHeader"
		End Select
		
		If nHeight <= 0 Then
			nHeight = 130
		End If
		
        insMakeURL = "/VTimeNet/Common/" & lstrBaseName & ".aspx?sCodispl=" & sCodispl & "&sModule=" & sModule & "&sProject=" & sProject & "&nHeight=" & nHeight & "&sCodisp=" & sCodisp & "&sWindowDescript=" & System.Web.HttpUtility.UrlEncode(sDescript) & "&nWindowTy=" & nWindowTy
	End Function
	
    Private Sub Class_Initialize_Renamed()
        With New eRemoteDB.VisualTimeConfig
            mstrCachePath = .LoadSetting("Cache", "C:\VisualTIMENet\VTimeNet\Cache", "Paths")
        End With
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






