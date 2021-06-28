Option Strict Off
Option Explicit On
Public Class LettRequestWin
	'**+Objective: Class that supports the table LettRequestWin
	'**+           it's content is:
	'**+Version: $$Revision: $
	'+Objetivo: Clase que le da soporte a la tabla LettRequestWin
	'+          cuyo contenido es:
	'+Version: $$Revision: $
	
	'-Objetivo:
	Const CN_WITHOUT_CONTENT As Short = 2
	
	'+ Tipo definido para validar la existencia de ventanas con contenido apra cargarlas en la secuencia
	
	Private Structure eTypeRequired
		Dim eExist As Boolean
		Dim eWindows As eRemoteDB.Execute
	End Structure
	
	Public nLettRequest As Integer
	Public sClient As String
	Public nAction As Integer
	
	'**- Window content indicator
	'- Indicador de contenido de la ventana
	'    Public Enum eWindowContent
	'        eWithOutContent = 1
	'        eWithContent = 2
	'    End Enum
	
	'**- Window required indicator
	'- Indicador de requerido de la ventana
	'    Public Enum eWindowRequire
	'        eNotRequired = 0
	'        eRequired = 1
	'    End Enum
	
	'**- Window access indicator
	'- Indicador de acceso a la ventana
	'    Public Enum eWindowAccess
	'        eAccessDenied = 0
	'        eAccessOK = 1
	'    End Enum
	
	
	
	'% LoadTabs: Esta función es la encarga de cargar la secuencia de ventanas a mostrar en la
	'%           secuencia de solicitudes de envío.
	Public Function LoadTabs(ByVal nLettRequest As Integer, ByVal nAction As Object, ByVal sUserSchema As String, ByVal nUsercode As Integer, Optional ByVal sClient As Object = Nothing) As String
		
		'- Se define la variable que controlará la lectura de ventanas para la secuencia
		
		Dim lobjRequired As eTypeRequired
		
		'- Se define la variable que devuelve el código HTML para poder "pintar" la secuencia
		
		Dim lclsSequence As eFunctions.Sequence
		
		'- Se crea la variable que contiene el código HTML para la creación de la tabla que simulará la secuencia
		
		Dim lstrHTMLCode As String
		
		'- Contendrá la imágen a asociar a la carpeta en la secuencia
		
		Dim lintPageImage As eFunctions.Sequence.etypeImageSequence
		
		If Not IsIDEMode Then
		End If
		lclsSequence = New eFunctions.Sequence
		
		'+ Se realizan las lecturas de las ventanas que tienen contenido
		
		lobjRequired = ValRequired(nLettRequest, sClient, nAction)
		
		lstrHTMLCode = String.Empty
		
		'+ De haber una secuencia asociada a la solicitud en cuestión, se procede a armarla
		
		If lobjRequired.eExist Then
			
			'+ Se realiza el encabezado de la tabla que define a una secuencia
			
			lstrHTMLCode = lclsSequence.makeTable
			
			If Not lobjRequired.eWindows Is Nothing Then
				While Not EndWindows(lobjRequired.eWindows)
					
					'+ Se busca la imagen asociada a la pestaña en la secuencia para colocarla en los links
					
					lintPageImage = insValimage(sUserSchema, lobjRequired.eWindows)
					
					'+ Se extrae el código HTML para "pintar" una fila en la página
					
					With lobjRequired
						lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(.eWindows.FieldToClass("sCodisp", String.Empty), .eWindows.FieldToClass("sCodispl", String.Empty), .eWindows.FieldToClass("nAction", 0), .eWindows.FieldToClass("sShort_des", String.Empty), lintPageImage)
					End With
					
					'+ Se procesa la próxima ventana
					
					NextWindow(lobjRequired.eWindows)
				End While
			End If
			
			'+ Se "pinta" la última fila de la tabla para completarla en código HTML
			
			lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		End If
		LoadTabs = lstrHTMLCode
		
		lclsSequence = Nothing
		
		Exit Function
	End Function
	
	'%ValRequired: lee las ventanas e indica si tienen o no contenido para cargarlas en la secuencia
	Private Function ValRequired(Optional ByVal nLettRequest As Integer = eRemoteDB.Constants.intNull, Optional ByVal sClient As String = "", Optional ByVal nAction As Integer = eRemoteDB.Constants.intNull) As eTypeRequired
		Dim lrecValRequired_LettReq As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecValRequired_LettReq = New eRemoteDB.Execute
		
		With Me
			If nLettRequest <> eRemoteDB.Constants.intNull Then .nLettRequest = nLettRequest
			If sClient <> String.Empty Then .sClient = sClient
			If nAction <> eRemoteDB.Constants.intNull Then .nAction = nAction
		End With
		
		'+ Definición de parámetros para stored procedure 'insudb.ValRequired_LettReq'
		'+ Información leída el 23/08/2001 02:37:51 PM
		
		With lrecValRequired_LettReq
			.StoredProcedure = "ValRequired_LettReq"
			.Parameters.Add("nLettRequest", Me.nLettRequest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", Me.sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", Me.nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				ValRequired.eExist = True
				ValRequired.eWindows = lrecValRequired_LettReq
				'.RCloseRec
			Else
				ValRequired.eExist = True
				ValRequired.eWindows = Nothing
			End If
		End With
		
		lrecValRequired_LettReq = Nothing
		
		Exit Function
	End Function
	
	'%EndWindows: determina cuando debe detenerse el ciclo para terminar de cargar la secuencia de ventanas
	Private Function EndWindows(ByRef lrecWindows As eRemoteDB.Execute) As Boolean
		If Not IsIDEMode Then
		End If
		
		EndWindows = lrecWindows.EOF
		
		Exit Function
	End Function
	
	Private Sub NextWindow(ByRef lrecWindow As eRemoteDB.Execute)
		If Not IsIDEMode Then
		End If
		
		lrecWindow.RNext()
		
		Exit Sub
	End Sub
	
	'%insValimage: se busca la imágen que deberá tener asociada la pestaña en la secuencia
	Private Function insValimage(ByVal sUserSchema As String, ByRef lobjRequired As eRemoteDB.Execute) As eFunctions.Sequence.etypeImageSequence
		
		'- Contendrá los datos asociados al esquema de seguridad de la transacción en proceso: no admitida, etc.
		
		Dim lclsSecurSche As eSecurity.Secur_sche
		
		If Not IsIDEMode Then
		End If
		lclsSecurSche = New eSecurity.Secur_sche
		With lclsSecurSche
			'If .FindLevels(sUserSchema) Then
			If .ItemLevels(sUserSchema, eSecurity.Secur_sche.eTypeCode.Window, lobjRequired.FieldToClass("sCodispl", String.Empty)) Then
				If lobjRequired.FieldToClass("sContent", 0) = 1 Then
					insValimage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
				Else
					If lobjRequired.FieldToClass("nRequired", 0) = 1 Then
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
			'  End If
		End With
		
		lclsSecurSche = Nothing
		
		Exit Function
	End Function
	
	'%insValContent: valida que las ventanas d ela secuencia tengan contenido para poder terminar con la secuencia
	Public Function insValContent(Optional ByVal nLettRequest As Integer = eRemoteDB.Constants.intNull, Optional ByVal sClient As String = "", Optional ByVal nAction As Integer = eRemoteDB.Constants.intNull) As Boolean
		
		'- Se define la variable que controlará la lectura de ventanas para la secuencia
		
		Dim lobjRequired As eTypeRequired
		
		If Not IsIDEMode Then
		End If
		With Me
			If nLettRequest <> eRemoteDB.Constants.intNull Then .nLettRequest = nLettRequest
			If sClient <> String.Empty Then .sClient = sClient
			If nAction <> eRemoteDB.Constants.intNull Then .nAction = nAction
		End With
		
		'+ Se realiza la lectura para poder determinar si cada una de las ventanas de la secuencia tienen o no contenido y devuelva sus valores
		
		lobjRequired = ValRequired((Me.nLettRequest), (Me.sClient), (Me.nAction))
		
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
		
		Exit Function
	End Function
	
	
	
	'**%Objective: the images in the sequence of windows refresh
	'%Objetivo: se refrescan las imágenes en la secuencia de ventanas
    Public Function InsReloadSequence(ByVal sModule As String, ByVal sProject As String, ByVal sSubProject As String, ByVal sCodispl As String, Optional ByVal bPopUp As Boolean = False, Optional ByVal bCloseErrors As Boolean = False, Optional ByVal bUpdContent As Boolean = True, Optional ByVal nWindowContent As eFunctions.Sequence.etypeImageSequence = eFunctions.Sequence.etypeImageSequence.eOK, Optional ByVal nNewRequire As eFunctions.Sequence.etypeImageSequence = -1, Optional ByVal sIndex As String = "", Optional ByVal sGotoNext As String = "Yes", Optional ByVal bReloadSequence As Boolean = False, Optional ByVal sReload As String = "", Optional ByVal sReloadAction As String = "", Optional ByVal sReloadIndex As String = "", Optional ByVal sWindowDescript As String = "", Optional ByVal sWindowTy As String = "", Optional ByVal sQueryString As String = "", Optional ByVal sSubFrame As String = "", Optional ByVal sScript As String = "", Optional ByVal sMainAction As String = "301", Optional ByVal bConfirmDelete As Boolean = False, Optional ByVal bReloadTop As Boolean = False, Optional ByVal bInline As Boolean = False, Optional ByVal bReloadParentFrame As Boolean = False) As String
        Dim lclsQuery As eRemoteDB.Query = Nothing

        Dim lstrScript As String = String.Empty
        Dim lstrOpener As String = String.Empty
        Dim lstrPage As String = String.Empty
        Dim lstrPath As String = String.Empty

        lstrScript = "<SCRIPT>"

        If bCloseErrors Then
            lstrScript = lstrScript & "closeWinErrors();"
        End If

        If bPopUp Then
            If bCloseErrors Then
                lstrOpener = "top.opener.top.opener."
            Else
                lstrOpener = "opener."
            End If
        Else
            If bCloseErrors Then
                If bReloadTop Then
                    lstrOpener = "opener.top."
                Else
                    lstrOpener = "opener."
                End If
            Else
                If bConfirmDelete Then
                    lstrOpener = "top.opener."
                Else
                    If bReloadTop Then
                        lstrOpener = "top."
                    End If
                End If
            End If
        End If

        bReloadSequence = IIf(bReloadTop, True, bReloadSequence)
        If bReloadSequence Then
            If bReloadTop Then
                lstrPage = "secWHeader.aspx?sCodispl=" & sCodispl & "&sModule=" & sModule & "&sProject=" & sProject & sSubProject & "&sConfig=InSequence" & sQueryString & """;"
            Else
                lstrPage = "Sequence.aspx?nAction=" & sMainAction & "&sGoToNext=" & sGotoNext & "&nOpener=" & sCodispl & "&nMainAction=" & sMainAction & sQueryString & """;"
            End If

            If bReloadTop Then
                lstrPath = "/VTimeNet/Common/"
            Else
                If Len(Trim(sModule)) > 0 Then
                    If Len(Trim(sProject)) > 0 Then
                        If Len(Trim(sSubProject)) > 0 Then
                            sSubProject = "/" & sSubProject
                        End If
                        lstrPath = "/VTimeNet/" & sModule & "/" & sProject & sSubProject & "/"
                    End If
                End If
            End If

            lstrScript = lstrScript & lstrOpener
            If bReloadTop Then
                lstrScript = lstrScript & "document.location=""" & lstrPath & lstrPage
            Else
                lstrScript = lstrScript & "top.fraSequence.document.location=""" & lstrPath & lstrPage
            End If
        Else
            If bUpdContent Then
                lstrScript = lstrScript & lstrOpener & "top.fraSequence.UpdContent('" & sCodispl & "'," & nWindowContent & "," & nNewRequire & ");"
                If Not bPopUp Then
                    If UCase(sGotoNext) = "YES" Then
                        lstrScript = lstrScript & lstrOpener & "top.fraSequence.NextWindows('" & sCodispl & "');"
                    End If
                End If
            End If
        End If

        '**%It is taken into account the updates coming from a popup window as well
        '**%as the deletes, called from the transaction, that is identified by the
        '**%eInvoke value  into the property Grid.nDelMethod of the page treated.
        '% Son consideradas tanto las actualizaciones desde una ventana Popup
        '% como las Eliminaciones invocadas desde la transacción identificadas
        '% estas últimas por el valor eInvoke en la Propiedad Grid.nDelMethod
        '% de la página en tratamiento

        If bPopUp Or bInline Then
            If sCodispl = "GE101" Then
                If bReloadTop Then
                    lstrScript = lstrScript & "opener.top.document.location=""/VTimeNet/Common/secWHeader.aspx?sCodispl=" & sQueryString & "&sModule=" & sModule & "&sProject=" & sProject & sSubProject & """;"
                Else
                    lstrScript = lstrScript & "opener.top.close();"
                End If
            Else
                lclsQuery = New eRemoteDB.Query
                With lclsQuery

                    If .OpenQuery("Windows", "sShort_des,sCodispl,sCodmen,sDescript,nWindowTy,sCodisp,nHeight", "sCodispl='" & sCodispl & "' and sStatregt = '1'") Then
                        lstrScript = lstrScript & IIf(bCloseErrors, "top.opener.", String.Empty)
                        If bReloadParentFrame Then
                            lstrScript = lstrScript & "top.opener.top.fraFolder."
                            sSubFrame = String.Empty
                        Else
                            lstrScript = lstrScript & IIf(bInline And sReloadAction = "Del", "top.fraFolder.", "top.opener.")
                        End If
                        lstrScript = lstrScript & "document.location.href='" & sCodispl & sSubFrame & ".aspx?" & "sCodispl=" & sCodispl & "&sCodisp=" & .FieldToClass("sCodisp") & "&nMainAction=" & sMainAction & "&sCodmen=" & .FieldToClass("sCodmen") & "&sWindowDescript=" & IIf(sWindowDescript = String.Empty, Replace(.FieldToClass("sDescript"), "&", "%26"), sWindowDescript) & "&nWindowTy=" & IIf(sWindowTy = String.Empty, .FieldToClass("nWindowTy"), sWindowTy) & "&nHeight=" & .FieldToClass("nHeight") & "&bQuery=" & IIf(sMainAction = "401", "1", "0") & "&sOnSeq=1" & "&Reload=" & sReload & "&ReloadAction=" & sReloadAction & "&ReloadIndex=" & sReloadIndex & sQueryString & "';"
                        .CloseQuery()
                    End If
                End With
            End If
        End If

        lstrScript = lstrScript & sScript & "</SCRIPT>"

        InsReloadSequence = lstrScript
    End Function
End Class











