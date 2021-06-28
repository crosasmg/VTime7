Option Strict Off
Option Explicit On
Public Class Sequence
	'%-------------------------------------------------------%'
	'% $Workfile:: Sequence.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 4/10/03 4:11p                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Define the variable that contais the table with the HTML code.
	'- Se define la variable que contiene la tabla con código HTML
	
	Private mstrHTMLCode As Object
	
	'**- Define the type for the image to show.
	'- Se define el tipo para la imagen a mostrar
	Public Enum etypeImageSequence
		eEmpty = 0
		eOK = 1
		eRequired = 2
		eDeniedS = 3
		eDeniedOK = 4
		eDeniedReq = 5
		eOnlyQuery = 6
	End Enum
	
	'**+ Variables for the internal handling of the data
	'+ Variables para el manejo interno de los datos
	Private mstrCodisp As String
	Private mstrCodispl As String
	Private mintAction As Short
	Private mstrStatusImage As etypeImageSequence
	Private mstrShort_des As String
	Private mstrDescript As String
	Private mstrRootName As String
	Private mstrRootDescript As String
	Private mstrBranch As String
	Private mstrQueryString As String
	Private mintModule As Short
	Private mintWindowTy As Short
	Private mintInqlevel As Short
	Private mintAmelevel As Short
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	'-Objeto para realizar funcione generales
	Private mclsValues As eFunctions.Values
	
	'**% makeTable: creates the table where the links are placed in the differents pages of the form sequence.
	'% makeTable: se crea la tabla en donde se colocan los links de las diferentes páginas que
	'%            forman la secuencia
	Public Function makeTable(Optional ByVal sRootName As String = "", Optional ByVal sRootDescript As String = "") As String
		mstrRootName = sRootName
		If sRootDescript <> String.Empty Then
			mstrRootDescript = sRootDescript
		End If
		makeTable = JSFunctions & vbCrLf & JSGenerateTree & vbCrLf
	End Function
	
	'**% makeRow: creates a row in the table in which the different links are placed in the different
	'**% pages that form the sequence.
	'% makeRow: se crea una fila en la tabla en donde se colocan los links de las diferentes
	'%          páginas que forman la secuencia
	Public Function makeRow(ByVal sCodisp As String, ByVal sCodispl As String, ByVal nAction As Short, Optional ByVal sShort_des As String = "", Optional ByVal sStatusImage As etypeImageSequence = etypeImageSequence.eEmpty, Optional ByVal sRootName As String = "", Optional ByVal bIsFather As Boolean = False, Optional ByVal sQueryString As String = "", Optional ByVal bOpenBranch As Boolean = False, Optional ByVal sRequired As String = "2", Optional ByVal nIndexChild As Integer = 0, Optional ByVal sDescript As String = "", Optional ByVal nModule As Short = 0, Optional ByVal nWindowty As Short = eRemoteDB.Constants.intNull) As String
		Dim lstrVariable As String
		Dim lstrGoto As String
		Dim lclsSecur_Sche As Object
		
		mstrHTMLCode = String.Empty
		mstrCodisp = Trim(sCodisp)
		mstrCodispl = Trim(sCodispl)
		mstrStatusImage = sStatusImage
		mintAction = nAction
		mstrQueryString = sQueryString
		
		If Not bIsFather Then
            lclsSecur_Sche = eRemoteDB.NetHelper.CreateClassInstance("eSecurity.Secur_sche")
			If lclsSecur_Sche.valTransAccess("", sCodispl, "2") Then
				
				If lclsSecur_Sche.mblnOnlyQuery Then
					mstrStatusImage = etypeImageSequence.eOnlyQuery
					mintAction = 401
				End If
			Else
				mstrStatusImage = etypeImageSequence.eDeniedS
			End If
		End If
		
		If sRootName = String.Empty Then
			mstrRootName = "foldersTree"
		Else
			mstrRootName = "lobj" & sRootName
		End If
		
		If nModule <> 0 And sShort_des <> String.Empty And sDescript <> String.Empty And nWindowty <> eRemoteDB.Constants.intNull Then
			mintModule = nModule
			mstrShort_des = sShort_des
			mstrDescript = sDescript
			mintWindowTy = nWindowty
		Else
			GetPropertyWindows((sCodispl))
		End If
		
		
		
		lstrVariable = "lobj" & Replace(mstrCodispl, "-", "") & IIf(nIndexChild = 0, "", nIndexChild)
		makeRow = vbCrLf & "var " & lstrVariable
		
		If bIsFather Then
			makeRow = makeRow & vbCrLf & lstrVariable & " = appendChild(" & "foldersTree" & ", folderNode('" & mstrShort_des & "','',''" & IIf(bOpenBranch, ",1", String.Empty) & "))" & vbCrLf
		Else
			lstrGoto = makePageLocation & makePageParameters(False)
			
			makeRow = makeRow & vbCrLf & "    " & lstrVariable & "= leafNode2(generateDocEntry(" & mstrStatusImage & ", '" & mstrShort_des & "', '" & lstrGoto & "', '" & mstrDescript & " (" & mstrCodispl & ")" & "','" & Replace(mstrCodispl, "-", "") & IIf(nIndexChild = 0, "", nIndexChild) & "'),'','')" & vbCrLf & "    " & "appendChild(" & lstrVariable & ", generateDocEntry(0, '" & mstrShort_des & "', 'portugal.html', '" & mstrDescript & " (" & mstrCodispl & ")" & "','" & Replace(mstrCodispl, "-", "") & IIf(nIndexChild = 0, "", nIndexChild) & "'))" & vbCrLf & "    " & "appendChild(" & mstrRootName & ", " & lstrVariable & ")" & vbCrLf
		End If
		mstrRootName = mstrBranch
		
		If Not bIsFather Then
			makeRow = makeRow & vbCrLf & "AddWindows(""" & makePageLocation & makePageParameters(True) & """" & mstrStatusImage & """,""" & Trim(sRequired) & """)" & vbCrLf
		End If
		
		'UPGRADE_NOTE: Object lclsSecur_Sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecur_Sche = Nothing
	End Function
	
	'**% makeColumn: creates a column in the table in which the different links are placed in the different
	'**% pages that form the sequence.
	'% makeColumn: se crea una columna en la tabla en donde se colocan los links de las diferentes
	'%             páginas que forman la secuencia
	Private Sub makeColumn()
		mstrHTMLCode = mstrHTMLCode & vbTab & vbTab & "<TD WIDTH=""12%""" & "ALIGN=""LEFT""" & "ONMOUSEOUT=""mOut(this,'0000cd')""" & "ONMOUSEOVER=""mOvr(this,'000080')"">" & vbCrLf
		
		Call makeColumnContent()
		Call closeColumn()
	End Sub
	
	'**% makeColumnContent: places the content in the column.
	'% makeColumnContent: se coloca el contenido dentro de la columna
	Private Sub makeColumnContent()
		mstrHTMLCode = mstrHTMLCode & vbTab & vbTab & vbTab & "<LABEL><A STYLE=""FONT-SIZE: 10pt; " & "COLOR: #ffffff; " & "TEXT-DECORATION: none"" " & "ONMOUSEOUT=""top.window.status=''""" & "ONMOUSEOVER=""top.window.status= & mstrDescript & "">" & "HREF=""" & makePageLocation & makePageParameters(False)
		Call makeItemImage()
		mstrHTMLCode = mstrHTMLCode & mstrShort_des & "</A></LABEL>" & "<SCRIPT>AddWindows(""" & makePageLocation & makePageParameters(True) & """" & mstrStatusImage & """)" & "</SCRIPT>" & vbCrLf
	End Sub
	
	'**% makeItemImage: places the image in the links associated to the column.
	'% makeItemImage: se coloca la imagen en el links asociado a la columna
	Private Sub makeItemImage()
		mstrHTMLCode = mstrHTMLCode & "<IMG ALIGN=MIDDLE BORDER=0 SRC=""/VTimeNet/images/"
		Select Case mstrStatusImage
			Case etypeImageSequence.eDeniedReq
			Case etypeImageSequence.eDeniedOK
			Case etypeImageSequence.eDeniedS
                mstrHTMLCode = mstrHTMLCode & "DeniedTr.png"
            Case etypeImageSequence.eRequired
                mstrHTMLCode = mstrHTMLCode & "NotChecked.png"
            Case etypeImageSequence.eOK
                mstrHTMLCode = mstrHTMLCode & "Checked.png"
            Case etypeImageSequence.eEmpty
                mstrHTMLCode = mstrHTMLCode & "Empty.png"
            Case etypeImageSequence.eOnlyQuery
                mstrHTMLCode = mstrHTMLCode & "FindPolicyOff.png"
        End Select
		mstrHTMLCode = mstrHTMLCode & """>"
	End Sub
	
	'**% makeDefRow: makes the definition of the row of the table.
	'% makeDefRow: se realiza la definición de la fila  de la tabla
	Private Sub makeDefRow()
		mstrHTMLCode = vbTab & "<TR HEIGHT=""20%"">" & vbCrLf
	End Sub
	
	'**% closeColumn: closes the definition of the table.
	'% closeColumn: se cierra la definición de la columna
	Private Sub closeColumn()
		mstrHTMLCode = mstrHTMLCode & vbTab & vbTab & "</TD>" & vbCrLf
	End Sub
	
	'**% closeRow: closes the definition of the row.
	'% closeRow: se cierra la definición de la fila
	Private Sub closeRow()
		mstrHTMLCode = mstrHTMLCode & vbTab & "</TR>" & vbCrLf
	End Sub
	
	'**% closeTable: closes the definition of the table
	'% closeTable: se cierra la definición de la tabla
	Public Function closeTable() As String
        Dim lobjValues As eFunctions.Values 
        lobjValues = New eFunctions.Values 
		closeTable = vbCrLf & "}initializeTree('" & lobjValues.sStyleSheetName & "')</SCRIPT>"
		
        lobjValues = Nothing
		
	End Function
	
	'*** Short_des_Windows: searches the short description of the given Codispl.
	'* Short_des_Windows: busca la descripción corta de un Codispl dado
	Private Sub GetPropertyWindows(ByVal sCodispl As String)
		Dim lclsQuery As eRemoteDB.Query
		lclsQuery = New eRemoteDB.Query
		
		With lclsQuery
			If .OpenQuery("Windows", "sDescript, sShort_des, nModules, nWindowTy, nInqlevel, nAmelevel", "sCodispl='" & sCodispl & "'") Then
				mstrShort_des = .FieldToClass("sShort_des")
				mintModule = .FieldToClass("nModules")
				mstrDescript = .FieldToClass("sDescript")
				mintWindowTy = .FieldToClass("nWindowTy")
				mintInqlevel = .FieldToClass("nInqlevel")
				mintAmelevel = .FieldToClass("nAmelevel")
				.CloseQuery()
			End If
		End With
		
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Sub

    '*** PathPageLocation: searches the short description of the given Codispl.
    '* PathPageLocation: busca la descripción corta de un Codispl dado
    Private ReadOnly Property PathPageLocation(ByVal nModule As String) As String
        Get
            Dim lclsQuery As eRemoteDB.Query
            Dim varAux As String = ""
            lclsQuery = New eRemoteDB.Query

            With lclsQuery
                If .OpenQuery("Tab_sys_exe", "sFolderName, sExe_name", "nExe_code=" & CStr(nModule)) Then
                    varAux = .FieldToClass("sFolderName") & "/" & .FieldToClass("sExe_name")
                    .CloseQuery()
                End If
            End With

            'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsQuery = Nothing
            Return varAux
        End Get
    End Property

    '**% JSFunctions: general handle of the associated array in the sequence of windows.
    '% JSFunctions: manejo general del arreglo asociado a la secuencia de ventanas
    Private Function JSFunctions() As String
		Dim lstrHTMLCode As String
		
		lstrHTMLCode = Space(1024)
		JSFunctions = Space(1024)
		
		lstrHTMLCode = "<IMG NAME=Logo ALT=""Logo de la empresa"" SRC=""/VTimeNet/images/Logo.gif"" WIDTH=100><BR><BR><BR>" & vbCrLf & "<SCRIPT LANGUAGE=""JAVASCRIPT"">" & vbCrLf & "var pintZone=2;" & vbCrLf & "var plngMainAction = 0;" & vbCrLf & "var pstrOnSeq = '1';" & vbCrLf & "function AcceptErrors(){" & vbCrLf & "    top.fraFolder.history.go(-1)" & vbCrLf & "}" & vbCrLf & "<!--" & vbCrLf & "var sequence = new Array(0);" & vbCrLf & "var mintWinCount = 0;" & vbCrLf & "var mintCurWindow = 0;" & vbCrLf & "function mOvr(Obj,ColorOut) {" & vbCrLf & "    if (!Obj.contains(event.fromElement)) {" & vbCrLf & "        Obj.style.cursor = 'hand';" & vbCrLf & "        Obj.bgColor = ColorOut;" & vbCrLf & "    }" & vbCrLf & "}" & vbCrLf & "function mOut(Obj,ColorIn) {" & vbCrLf & "    if (!Obj.contains(event.toElement)) {" & vbCrLf & "        Obj.style.cursor = 'default';" & vbCrLf & "        Obj.bgColor = '';" & vbCrLf & "    }" & vbCrLf & "}" & vbCrLf
		
		JSFunctions = lstrHTMLCode & "function AddWindows(Page,Require,sRequired){" & vbCrLf & "    var lobj = new Object;" & vbCrLf & "    lobj.Page=Page;" & vbCrLf & "    lobj.Require=Require;" & vbCrLf & "    lobj.sRequired=sRequired;" & vbCrLf & "    sequence[mintWinCount++] = lobj;" & vbCrLf & "}" & vbCrLf & "-->" & vbCrLf & "</SCRIPT>" & vbCrLf
	End Function
	
	'% JSGenerateTree: Genera el código para generar el arbol
	Private Function JSGenerateTree() As String
		Dim lrecWindows As eRemoteDB.Query
        Dim lstrImage As String = ""

        lrecWindows = New eRemoteDB.Query
		
		JSGenerateTree = String.Empty
		
		If mstrRootDescript = String.Empty Then
			If lrecWindows.OpenQuery("Windows", "sDescript", "sCodispl = '" & Trim(mstrRootName) & "'") Then
				mstrBranch = lrecWindows.FieldToClass("sDescript")
			Else
				mstrBranch = "Secuencia"
			End If
		Else
			mstrBranch = mstrRootDescript
		End If
		
		If mstrRootName <> String.Empty Then
			lstrImage = "/VTimeNet/images/" & mstrRootName & "T.gif"
		End If
		JSGenerateTree = "<SCRIPT>" & "function generateTree(){" & vbCrLf & "var lintAux1, lintAux2, lintAux3, lintAux4" & vbCrLf & "foldersTree = folderNode(""" & mstrBranch & """,""" & lstrImage & """,""" & lstrImage & """,1)"
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
	End Function
	
	'**% BODYparameters: places the atributes to the BODY label
	'% BODYParameters: coloca los atributos a la etiqueta BODY
	Public Function BODYParameters() As String
		BODYParameters = " VLINK=""white"" LINK=""white"" BACKGROUND=""/VTimeNet/images/FrameSequence.jpg"" TEXT=""white"""
	End Function
	
	'**% makePageLocation: searches the page address
	'% makePageLocation: se busca la dirección de la página
	Private Function makePageLocation() As String
		If Mid(UCase(mstrCodisp), 1, 3) = "SCA" Then
			makePageLocation = "/VTimeNet/Common/" & mstrCodisp & ".aspx"
		ElseIf Mid(UCase(mstrCodisp), 1, 5) = "VDATA" Then 
			makePageLocation = "/VTimeNet/Common/VData/" & mstrCodisp & ".aspx"
		ElseIf (Mid(UCase(mstrCodisp), 1, 5) = "AU001" Or Mid(UCase(mstrCodisp), 1, 5) = "IN010" Or Mid(UCase(mstrCodisp), 1, 5) = "CA010" Or Mid(UCase(mstrCodisp), 1, 5) = "CA012") Then 
			makePageLocation = "/VTimeNet/Policy/PolicySeq/" & mstrCodisp & ".aspx"
		ElseIf mintModule = 99 Then 
			makePageLocation = "/VTimeNet/" & PathPageLocation(CStr(mintModule)) & "/" & mstrCodisp & ".aspx"
		Else
			makePageLocation = mstrCodisp & ".aspx"
		End If
	End Function
	
	'**% makePageParameter: adds the parameters to pass to the page.
	'% makePageParameters: se añaden los parámetros a pasar a la página
	Private Function makePageParameters(ByVal bArray As Boolean) As String
		Dim lstrFinalChar As String
		
		lstrFinalChar = IIf(bArray, """,", "")
		If mclsValues Is Nothing Then
			mclsValues = New eFunctions.Values
		End If
        'makePageParameters = "?sCodispl=" & mstrCodispl & "&sCodisp=" & mstrCodisp & "&nMainAction=" & mintAction & "&sOnSeq=1" & "&sWindowDescript=" & mclsValues.HTMLEncode(mstrDescript) & "&nWindowTy=" & mintWindowTy & mstrQueryString & lstrFinalChar
        makePageParameters = "?sCodispl=" & mstrCodispl & "&sCodisp=" & mstrCodisp & "&nMainAction=" & mintAction & "&sOnSeq=1" & "&sWindowDescript=" & System.Web.HttpUtility.UrlEncode(mstrDescript) & "&nWindowTy=" & mintWindowTy & mstrQueryString & lstrFinalChar
	End Function
	
	'*Class_Terminate: Se controla la destrucción del objeto
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsValues = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






