Option Strict Off
Option Explicit On
Public Class Grid
	'%-------------------------------------------------------%'
	'% $Workfile:: Grid.cls                                 $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 29/12/03 10:35a                              $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	Public sCodisplPage As String
	
	'UPGRADE_NOTE: Columns was upgraded to Columns. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Columns As Columns
	'UPGRADE_NOTE: Splits was upgraded to Splits_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Splits_Renamed As Splits
	Public ActionQuery As Boolean
	Public nMainAction As Short
	Public sEditRecordParam As String
	Public sDelRecordParam As String
	Public bOnlyForQuery As Boolean
	Public sQueryString As String
	
	'-Numero que identifica el formnulario donde se encuentra el control de grid
	Public nParentForm As Short
	
	Private mblnHeader As Boolean
	Private mstrCodispl As String
	Private mlngIndex As Integer
	Private mlngIndexRow As Integer
	Private mintColumns As Short
	Private mblnDelButton As Boolean
	Private mblnAddButton As Boolean
	Private mstrDeleteScript As String
	Private mblnDelScript As Boolean
	Private mblnInsBody As Boolean
	Private mstrClassTR As String
	Private mblnArrayNamed As Boolean
	Public bUpdateGrid As Boolean
	
	Private mblnNewRows As Boolean
	
	'**+ Variable definition nReloadIndex and sReloadAction, for indicate to the functions
	'**+ that generates the page that must open the popup window in the nReloadIndex position,
	'**+ with the action that indicates s ReloadAction
	'+ Se definen las variables nReloadIndex y sReloadAction,para indicarle a las funciones
	'+ que generan la página que deben abrir la ventana popup en la posición nReloadIndex, con
	'+ la acción que indique sReloadAction
	
	Public sReloadIndex As String
	Public sReloadAction As String
	
	Public Width As Integer
	Public WidthDelete As Short
	Public Height As Integer
	Public Top As Integer
	'UPGRADE_NOTE: Left was upgraded to Left. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Left As Integer
	Public Codispl As String
	Public Codisp As String
	Public mintFieldByRow As Short
	
	'**- Define the property AltRowColor, for indicate when the rows of different colors are activated.
	'-Se define la Propiedad AltRowColor, para indicar cuando se activan filas de diferentes colores
	
	Public AltRowColor As Boolean
	
	'**- Define the peroperty bCheckVisible, for indicate when the Continue in the PopUp window shows the check
	'-Se define la Propiedad bCheckVisible, para indicar cuando se muestra el check de Continuar en la ventana PopUp
	
	Public bCheckVisible As Boolean
	
	'**- Define the property to handle the Script that is wanted when moving between the rows of the grid.
	'- Se define la propiedad para manejar el Script que se desee al moverse entre las filas del grid
	
	Public MoveRecordScript As String
	
	'**- Define the property to assigne the name of the arrengement associated to the grid.
	'- Se define la propiedad para asignar el nombre del arreglo asociado al grid
	Private mstrArrayName As String
	
	'-Variable para el manejo de los objetos generales
	Private mobjValues As Values
	Private mobjTables As Tables
	
	'-Variable para indicar que se desea actualizar la imagen de la secuencia
	Private mblnUpdContent As Boolean
	
	'- Propiedad para controlar la creación del vínculo en el grid, cuando es consulta
	Public EditRecordQuery As Boolean
	
	'- Propiedad para controlar referencia de la grid
	Public EditRecordDisabled As Boolean
	
	'-Variable que guarda el número de sesión
	Public sSessionID As String
	
	'-Código del usuario
	Public nUsercode As Integer
	
	Public mblnLoadValue As Boolean
	
	Private mstrWindowsCodispl As String
	Private mstrWindowDescript As String
	Private mstrWindowTy As String
	Private mstrCancelScript As String
	
	'**% DoHeader. This method is in charge of making the design of the "grid"
	'**% header of the window.
	'%DoHeader. Este método se encarge de realizar el diseño del encabezado
	'%del "grid" de la ventana.
	Private Function DoHeader(Optional ByVal sQueryString As String = "") As String
		Dim lstrResult As String
		Dim lobjColumn As Column
		Dim lintColumns As Short
		Dim lstrTitle As String
		Dim lstrScript As String
		Dim lstrParamSc As String
		Dim lstrAssigV As String
        Dim lstrQuote As String
		Dim lintIndex As Short
		Dim lintHide As Short
		Dim lintTotalColumn As Short
		Dim lstrFieldName As String
		Dim lstrArrayName As String
		
		
		
		
		lintColumns = 0
		lintIndex = 1
		lintHide = 0
		lstrParamSc = String.Empty
        lstrQuote = String.Empty
		lstrAssigV = String.Empty
		lstrResult = String.Empty
		lintTotalColumn = 0
		
		If mblnHeader Then
			If Splits_Renamed.Count > 0 Then
				lintColumns = 0
				For	Each lobjColumn In Columns
					If lobjColumn.FieldName = "Sel" Then
						If lobjColumn.GridVisible Then
							lstrResult = lstrResult & "<TH></TH>"
						End If
					Else
						If lobjColumn.ControlType = 5 Then
							lintTotalColumn = lintTotalColumn + 1
						Else
							If lintIndex > Splits_Renamed.Count Then
								If lintTotalColumn < Columns.Count Then
									'+ Si ya se procesaro todos los splits, se completan las columnas que faltan
									'+ con un split en blanco
									lstrResult = lstrResult & "<TH COLSPAN=" & CStr(Columns.Count - lintTotalColumn - 1) & " ALIGN=CENTER></TH>"
								End If
								Exit For
							End If
							If Not lobjColumn.GridVisible Then
								lintHide = lintHide + 1
							End If
							lintColumns = lintColumns + 1
							If lintColumns >= Splits_Renamed(lintIndex).nCols Then
								lstrResult = lstrResult & "<TH COLSPAN=" & CStr(lintColumns - lintHide) & " ALIGN=CENTER>" & Splits_Renamed(lintIndex).sTitle & "</TH>"
								'+ Se aumenta el alto de la ventana, por el espacio ocupado por el Split
								If Splits_Renamed(lintIndex).sTitle > String.Empty Then
									Height = Height + 47
									'+ Se disminuye la ubicación de la ventana en el TOP
									Top = Top - 25
									If Top < 0 Then
										Top = 0
									End If
								End If
								lintTotalColumn = lintTotalColumn + Splits_Renamed(lintIndex).nCols
								lintIndex = lintIndex + 1
								lintHide = 0
								lintColumns = 0
							End If
						End If
					End If
				Next lobjColumn
				If lintColumns - lintHide > 0 Then
					lstrResult = lstrResult & "<TH COLSPAN=" & CStr(lintColumns - lintHide) & " ALIGN=CENTER>" & Splits_Renamed(lintIndex).sTitle & "</TH>"
				End If
				If lstrResult <> String.Empty Then
					lstrResult = lstrResult & "</TR><TR>"
				End If
			End If
			
			lintColumns = 0
			lintIndex = 0
			
			'**+ Run the rows of the column's collection, to generate the tables. For this
			'**+ the hidden type controls are not considered.
			'+Se recorren las filas de la colección de columnas, para generar las tabla. Para ello no
			'+se toman en cuenta los controles de tipo oculto.
			For	Each lobjColumn In Columns
				lstrFieldName = Trim(lobjColumn.FieldName)
				If lobjColumn.ControlType <> 5 And lobjColumn.GridVisible Then
					lintColumns = lintColumns + 1
					If Trim(lobjColumn.Title) <> String.Empty Then
						lstrTitle = Trim(lobjColumn.Title)
					Else
						lstrTitle = "&nbsp;"
					End If
					lstrResult = lstrResult & "<TH>" & lobjColumn.Title & "</TH>"
				End If
				lstrAssigV = Trim(lstrAssigV) & " lobjElem." & lstrFieldName & " = " & lstrFieldName & ";" & vbCrLf
                lstrParamSc = lstrParamSc & lstrQuote & lstrFieldName

                Select Case lobjColumn.ControlType
                    '+Control de cliente
                    '+ Se concatena el digito verificador y el nombre del cliente
                    Case 8
                        lstrParamSc = lstrParamSc & lstrQuote & lstrFieldName & "_Digit" & lstrQuote & lobjColumn.FieldClieName
                        lstrAssigV = lstrAssigV & " lobjElem." & lstrFieldName & "_Digit" & " = " & lstrFieldName & "_Digit" & ";" & vbCrLf & " lobjElem." & lobjColumn.FieldClieName & " = " & lobjColumn.FieldClieName & ";" & vbCrLf

                        '+Valores posibles
                        '+Se concatena la descripción
                    Case 7, 15
                        If lobjColumn.ValuesType = Values.eValuesType.clngWindowType Then
                            lstrParamSc = lstrParamSc & lstrQuote & lstrFieldName & "Desc"
                            lstrAssigV = lstrAssigV & " lobjElem." & lstrFieldName & "Desc" & " = " & lstrFieldName & "Desc" & ";" & vbCrLf
                        End If
                End Select

                lstrQuote = ","
                lintIndex = lintIndex + 1
            Next lobjColumn

            lstrArrayName = IIf(mblnArrayNamed, mstrArrayName, String.Empty)

            If Columns.Count > 0 Then
                lstrScript = "<SCRIPT>" & " var " & mstrArrayName & " = new Array(0); var mintArray" & lstrArrayName & "Count= -1;" & vbCrLf
                lstrScript = Trim(lstrScript) & "function MarkRecord" & lstrArrayName & "(Field){" & mstrArrayName & "[Field.value].Sel = Field.checked}" & vbCrLf
                If mblnInsBody Then
                    lstrScript = Trim(lstrScript) & "function DeleteRecord" & lstrArrayName & "(BeginIndex){" & vbCrLf & "  var lintIndex=0; " & vbCrLf & "  for(lintIndex=(BeginIndex+1);(lintIndex<=mintArray" & lstrArrayName & "Count) && (!" & mstrArrayName & "[lintIndex].Sel);lintIndex++){}" & vbCrLf & "  if (lintIndex<=mintArray" & lstrArrayName & "Count)" & " EditRecord" & lstrArrayName & "(lintIndex,nMainAction,'Del','" & sDelRecordParam & sQueryString & "')" & vbCrLf & "}" & vbCrLf
                End If

                lstrScript = Trim(lstrScript) & "function insAddRecordArray" & lstrArrayName & "(" & Trim(lstrParamSc) & "){" & vbCrLf & " var lobjElem = new Object; " & lstrAssigV & " " & mstrArrayName & "[++mintArray" & lstrArrayName & "Count] = lobjElem" & vbCrLf & "}" & vbCrLf & "function EditRecord" & lstrArrayName & "(Field, nMainAction,Action,Param){ " & vbCrLf & " if (typeof(Action)=='undefined') Action='Update';" & vbCrLf & " if (typeof(Param)=='undefined'){Param=''} " & vbCrLf & " else {Param=(Param==''?'':'&' + Param)};" & vbCrLf & " ShowPopUp(""/VTimeNet/Common/EditRecord.aspx?Type=PopUp&Action="" + Action + ""&Index="" + Field + ""&nMainAction="" + nMainAction + ""&sCodispl=" & Codispl & IIf(mblnArrayNamed, """ + ""&sArrayName=" & mstrArrayName, String.Empty) & """ + Param,""" & Replace(Codispl, "-", "") & "Upd"", (Action=='Del'?" & WidthDelete & ":" & CStr(Width) & "), (Action=='Del'?110:" & CStr(Height + 10) & "),'no','no'," & Left & ",(Action=='Del'?200:" & Top & "));}" & vbCrLf & "</SCRIPT>" & vbCrLf
            Else
                lstrScript = String.Empty
            End If

            lstrResult = lstrScript & insDoGridButtons(sQueryString) & "<TABLE WIDTH=100% COLS=" & CStr(lintColumns) & " CLASS=grddata><TR>" & Trim(lstrResult) & "</TR>" & vbCrLf
            mblnHeader = False
            mlngIndexRow = -1
        End If
        mintColumns = lintColumns
        DoHeader = lstrResult
    End Function

    '%HTMLDecode: Decodifica los valores especiales de HTML
    Public Function HTMLDecode(ByVal sValue As String) As String
        HTMLDecode = Trim(sValue)
        HTMLDecode = Replace(HTMLDecode, "%5C", "\")
        HTMLDecode = Replace(HTMLDecode, """", "'")
        HTMLDecode = Replace(HTMLDecode, vbLf, " ")
        HTMLDecode = Replace(HTMLDecode, "&nbsp;", " ")
        HTMLDecode = Replace(HTMLDecode, "Â", "")
        HTMLDecode = Replace(HTMLDecode, "Ã¡", "á")
        HTMLDecode = Replace(HTMLDecode, "Ã©", "é")
        HTMLDecode = Replace(HTMLDecode, "Ã­", "í")
        HTMLDecode = Replace(HTMLDecode, "Ã³", "ó")
        HTMLDecode = Replace(HTMLDecode, "ÃƒÂ³", "ó")
        HTMLDecode = Replace(HTMLDecode, "Ãƒ³", "ó")
        HTMLDecode = Replace(HTMLDecode, "¢", "ó")
        HTMLDecode = Replace(HTMLDecode, "Ãº", "ú")
    End Function

    '%HTMLEncode: Codifica los valores especiales de HTML
    Friend Function HTMLEncode(ByVal strValue As String) As String
        HTMLEncode = Trim(strValue)
        HTMLEncode = Replace(HTMLEncode, "á", "&aacute;")
        HTMLEncode = Replace(HTMLEncode, "é", "&eacute;")
        HTMLEncode = Replace(HTMLEncode, "í", "&iacute;")
        HTMLEncode = Replace(HTMLEncode, "ó", "&oacute;")
        HTMLEncode = Replace(HTMLEncode, "ú", "&uacute;")
        HTMLEncode = Replace(HTMLEncode, "Á", "&Aacute;")
        HTMLEncode = Replace(HTMLEncode, "É", "&Eacute;")
        HTMLEncode = Replace(HTMLEncode, "Í", "&Iacute;")
        HTMLEncode = Replace(HTMLEncode, "Ó", "&Oacute;")
        HTMLEncode = Replace(HTMLEncode, "Ú", "&Uacute;")
        HTMLEncode = Replace(HTMLEncode, "ü", "&uuml;")
        HTMLEncode = Replace(HTMLEncode, "ñ", "&ntilde;")
        HTMLEncode = Replace(HTMLEncode, "Ñ", "&Ntilde;")
        HTMLEncode = Replace(HTMLEncode, "Ã¡", "&aacute;")
        HTMLEncode = Replace(HTMLEncode, "Ã©", "&eacute;")
        HTMLEncode = Replace(HTMLEncode, "Ã­", "&iacute;")
        HTMLEncode = Replace(HTMLEncode, "Ã³", "&oacute;")
        HTMLEncode = Replace(HTMLEncode, "ÃƒÂ³", "&oacute;")
        HTMLEncode = Replace(HTMLEncode, "Ãƒ³", "&oacute;")
        HTMLEncode = Replace(HTMLEncode, "¢", "&oacute;")
        HTMLEncode = Replace(HTMLEncode, "Ãº", "&uacute;")
        HTMLEncode = Replace(HTMLEncode, """", "&quot;")
        HTMLEncode = Replace(HTMLEncode, vbCrLf, "&#13;")
        'HTMLEncode = Replace(HTMLEncode, " ", "&nbsp;")
        HTMLEncode = Replace(HTMLEncode, " ", "&#32;")
    End Function

    '**% insDoGridButtons. This function is in charge of placing the add
    '**% and delete buttons of the grid.
    '%insDoGridButtons. Esta función se encarga de colocar los botones de
    '%agregar y eliminar del grid
    Private Function insDoGridButtons(Optional ByVal sQueryString As String = "") As String
        Dim lobjValues As Values
        Dim lstrResult As String
        If Not bOnlyForQuery Then
            lstrResult = String.Empty
            lobjValues = New eFunctions.Values
            lobjValues.ActionQuery = ActionQuery
            If mblnDelButton Then
                lstrResult = lobjValues.ButtonDelete(mstrDeleteScript, "cmdDelete" & IIf(mblnArrayNamed, mstrArrayName, String.Empty)) & "&nbsp;"
            End If
            If mblnAddButton Then
                lstrResult = Trim(lstrResult) & lobjValues.ButtonAdd("EditRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(-1," & CStr(nMainAction) & ",'Add','" & sEditRecordParam & sQueryString & "')", "cmdAdd" & IIf(mblnArrayNamed, mstrArrayName, String.Empty))
            End If
            insDoGridButtons = Trim(lstrResult) & vbCrLf
        Else
            insDoGridButtons = String.Empty
        End If
    End Function

    '**% This function is in charge of making the script of the add and delete records
    '**% button.
    '% Esta funcion se encarga de realizar los script de los botones de agregar
    '%  y eliminar registros
    Private Function insDoScriptButtons() As String
        Dim lstrResult As String
        If mblnDelButton Then
            lstrResult = "function DeleteRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(){}"
        Else
            lstrResult = String.Empty
        End If
        insDoScriptButtons = lstrResult
    End Function


    Public Property DeleteScriptName() As String
        Get
            DeleteScriptName = mstrDeleteScript
            '    mblnDelScript = False
        End Get
        Set(ByVal Value As String)
            mstrDeleteScript = Value
            mblnInsBody = False
        End Set
    End Property

    Public Property DeleteButton() As Boolean
        Get
            DeleteButton = mblnDelButton
        End Get
        Set(ByVal Value As Boolean)
            mblnDelButton = Value
        End Set
    End Property


    Public Property AddButton() As Boolean
        Get
            AddButton = mblnAddButton
        End Get
        Set(ByVal Value As Boolean)
            mblnAddButton = Value
        End Set
    End Property

    '**% FieldsByRow. Define this property, to assinge the amount of columns
    '**% to show by row, in the case of the Upd window.
    '%FieldsByRow. Se define esta propiedad, para asignar la cantidad de columnas
    '% a mostrar por fila. en el caso de la ventana Upd.

    '**% FieldsByRow. Define this property, to restore the amount of columns
    '**% to show by row, in the case of the Upd window.
    '%FieldsByRow. Se define esta propiedad, para devolver la cantidad de columnas
    '% a mostrar por fila. en el caso de la ventana Upd.
    Public Property FieldsByRow() As Short
        Get
            FieldsByRow = mintFieldByRow
        End Get
        Set(ByVal Value As Short)
            If Value > 0 Then
                mintFieldByRow = Value
            Else
                mintFieldByRow = 1
            End If
        End Set
    End Property

    '**% sArrayName: assigne the name of the arrengement associated to the grid.
    '% sArrayName: se asigna el nombre del arreglo asociado al grid

    '**% sArrayName: take the name of the arrengement associated to the grid
    '% sArrayName: se toma el nombre del arreglo asociado al grid
    Public Property sArrayName() As String
        Get
            sArrayName = mstrArrayName
        End Get
        Set(ByVal Value As String)
            mstrArrayName = Value
            mblnArrayNamed = True
            Columns.sArrayName = Value
            If mblnInsBody Then
                mstrDeleteScript = "DeleteRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(-1)"
            End If
        End Set
    End Property

    '%UpdContent: Actualiza la variable privada para indicar si actualiza la imagen de la tx
    Public WriteOnly Property UpdContent() As Boolean
        Set(ByVal Value As Boolean)
            mblnUpdContent = Value
        End Set
    End Property

    Public WriteOnly Property CancelScript() As String
        Set(ByVal Value As String)
            mstrCancelScript = Value
        End Set
    End Property

    '**% DoRow: This method is in charge of generate each row of the window's grid.
    '%DoRow: Este metodo se encarga de generar cada fila del grid de la ventana.
    Public Function DoRow() As String
        Dim lobjColumn As Column
        Dim lstrResult As String
        Dim lstrTD As String
        Dim lstrTR As String
        Dim lstrAlign As String = ""
        Dim lstrScript As String = ""
        Dim lstrAssigV As String
        Dim lstrColon As String
        Dim lstrQuote As String
        Dim lvntData As Object
        Dim lstrQueryString As String

        '    #If LOG Then
        '        eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|DoRow", sSessionID
        '    #End If

        '**  Concatenate the header of the table in treatment.
        '+ Se concatena el encabezado de la tabla en tratamiento
        If Len(Trim(mstrArrayName)) > 1 Then
            lstrTR = " <TR NAME = grid" & mstrArrayName & " ID= grid" & mstrArrayName
        Else
            lstrTR = " <TR "
        End If

        lstrQueryString = "&sCodisp=" & mstrWindowsCodispl & "&sWindowDescript=" & mstrWindowDescript & "&nWindowTy=" & mstrWindowTy
        If AltRowColor Then
            lstrResult = DoHeader(lstrQueryString) & lstrTR & " CLASS=" & mstrClassTR & ">"
            If mstrClassTR = "EVEN" Then
                mstrClassTR = "UNEVEN"
            Else
                mstrClassTR = "EVEN"
            End If
        Else
            lstrResult = DoHeader(lstrQueryString) & lstrTR & ">"
        End If
        lstrTD = "<TD"
        lstrColon = String.Empty
        lstrQuote = String.Empty
        lstrAssigV = String.Empty
        mlngIndexRow = mlngIndexRow + 1
        '**+ run the column's collection to create a row.
        '+Se recorre la colección de columnas para crear la fila
        Dim lobjClient As Object
        For Each lobjColumn In Columns
            lstrQuote = """"
            With lobjColumn
                '+ Alineacion de la celda
                If .ControlType <> 5 And .GridVisible Then
                    Select Case .ControlType
                        Case 1
                            lstrAlign = "ALIGN=CENTER WIDTH=25pcx"
                        Case 2
                            lstrAlign = "ALIGN=RIGHT"
                        Case 6, 9, 4, 13
                            lstrAlign = "ALIGN=CENTER"
                        Case Else
                            lstrAlign = "ALIGN=LEFT"
                    End Select
                End If
                If Not bOnlyForQuery Then
                    If .FieldName = "Sel" Then
                        lstrAssigV = lstrAssigV & lstrColon & IIf(.Checked = 1, "true", "false")
                    Else
                        If mobjValues Is Nothing Then
                            mobjValues = New Values
                            mobjValues.sCodisplPage = sCodisplPage
                        End If
                        mobjValues.sQueryString = sQueryString
                        mobjValues.EditRecordQuery = EditRecordQuery

                        Select Case .ControlType
                            '+En el caso que el control sea del tipo notas, en vez de asignar el valor de la propiedad DefValue, asignamos el valor
                            '+de la propiedad nNotenum
                            Case 9 'Notes
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.TypeToString(.nNotenum, Values.eTypeData.etdLong) & lstrQuote
                            Case 2 'Numerico
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.TypeToString(.DefValue, Values.eTypeData.etdDouble, .ShowThousand, .DecimalPlaces) & lstrQuote
                            Case 6 'Fecha
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.TypeToString(.DefValue, Values.eTypeData.etdDate) & lstrQuote
                            Case 1 'Check'
                                lstrAssigV = lstrAssigV & lstrColon & IIf(.Checked = 1, "true", "false")
                            Case Else
                                If .ControlType = 10 Then '+ Text Area
                                    .DefValue = Replace(.DefValue, vbCrLf, "\n")
                                End If
                                '+ En caso de valores numericos nulos, se agrega al arreglo un vacio
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.TypeToString(.DefValue, Values.eTypeData.etdOthers)) & lstrQuote
                        End Select
                    End If
                End If
                lstrColon = ","
                If .ControlType <> 5 And .GridVisible Then
                    If lstrTD <> String.Empty Then
                        lstrTD = lstrTD & " " & lstrAlign & ">"
                    End If
                    lstrResult = lstrResult & lstrTD
                End If
                If .ControlType <> 5 And .GridVisible Then
                    lstrTD = vbCrLf & "</TD><TD"
                End If
                If mobjValues Is Nothing Then
                    mobjValues = New Values
                    mobjValues.sCodisplPage = sCodisplPage
                    mobjValues.ActionQuery = ActionQuery
                End If

                '**+ In case the controls has the property EditRecord marked, associate the call to the script that
                '**+ shows the UPD window.
                '+En el caso de que el control tenga marcada la propiedad EditRecord, se asocia el llamado al script que
                '+Muestra la ventana UPD
                If .EditRecord And .ControlType <> 5 And .GridVisible Then
                    .HRefScript = insEditRecord(mlngIndex, lstrQueryString)
                Else
                    If EditRecordDisabled Then
                        .HRefScript = ""
                    Else
                        If mblnLoadValue Then
                            .HRefScript = ""
                        End If
                    End If
                End If

                '+ En caso de que tengamos un valores posibles que retorna más de 2 columnas, y se quiere asignar esas
                '+ columnas a un campo del grid
                If .sPossiblesVName <> String.Empty Then
                    If .DefValue = String.Empty Then
                        If .ControlType <> 1 Then
                            lvntData = Columns(.sPossiblesVName).Parameters.Item_ReturnValue(.sParamName).Value
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                            If Not IsDBNull(lvntData) Then
                                .DefValue = lvntData
                            End If
                        End If
                    End If
                End If

                If .GridVisible Then
                    Select Case lobjColumn.ControlType
                        Case 1 'Check
                            If Trim(.FieldName) = "Sel" Then
                                mlngIndex = mlngIndex + 1
                                lstrResult = lstrResult & mobjValues.CheckControl(.FieldName, .Descript, CStr(.Checked), CStr(mlngIndex), mstrArrayName & "[this.value].Sel=this.checked;" & .OnClick, .Disabled)
                            Else
                                lstrResult = lstrResult & mobjValues.CheckControl(.FieldName, .Descript, CStr(.Checked), .DefValue, .OnClick, .Disabled, , .Alias_Renamed)
                            End If

                        Case 2 'Numerico
                            lstrResult = lstrResult & mobjValues.NumericControl(.FieldName, .Length, .DefValue, .isRequired, .Alias_Renamed, .ShowThousand, .DecimalPlaces, True, .HRefUrl, .HRefScript, .OnChange, .Disabled)

                        Case 3 'Texto
                            lstrResult = lstrResult & mobjValues.TextControl(.FieldName, .Length, HTMLEncode(HTMLDecode(.DefValue)), .isRequired, .Alias_Renamed, True, .HRefUrl, .HRefScript, .OnChange, .Disabled)
                            'lstrResult = HTMLEncode(HTMLDecode(lstrResult))

                        Case 4 'Boton Animado
                            mobjValues.Width = .Width
                            mobjValues.Height = .Height
                            lstrResult = lstrResult & mobjValues.AnimatedButtonControl(.FieldName, .Src, .Alias_Renamed, .HRefUrl, .HRefScript, .Disabled, .TabIndex, mlngIndexRow)

                        Case 5 'Hide
                            lstrResult = lstrResult & mobjValues.HiddenControl(.FieldName, .DefValue)

                        Case 6 'Fecha
                            lstrResult = lstrResult & mobjValues.DateControl(.FieldName, .DefValue, .isRequired, .Alias_Renamed, True, .HRefUrl, .HRefScript, .OnChange, .Disabled)

                        Case 7 'Combo
                            mobjValues.Parameters = .Parameters
                            mobjValues.BlankPosition = .BlankPosition
                            mobjValues.TypeList = .TypeList
                            mobjValues.List = .List
                            mobjValues.TypeOrder = .TypeOrder
                            lstrResult = lstrResult & mobjValues.PossiblesValues(.FieldName, .TableName, Values.eValuesType.clngComboType, mobjValues.HTMLDecode(.DefValue), .NeedParam, True, .HRefUrl, .HRefScript, .ComboSize, .OnChange, .Disabled, .MaxLength, .Alias_Renamed, .CodeType, .TabIndex, .ShowDescript, .bAllowInvalid, .Descript, .NotCache, .KeyField)
                            If .ValuesType = Values.eValuesType.clngWindowType Then
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.sDescript) & lstrQuote
                            End If
                            'UPGRADE_NOTE: Object mobjValues.Parameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            mobjValues.Parameters = Nothing

                        Case 8 'Cliente
                            mobjValues.ClientRole = .ClientRole
                            mobjValues.TypeList = .TypeList
                            lstrResult = lstrResult & mobjValues.ClientControl(.FieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled, .FieldClieName, .isDIVDefine, True, .HRefUrl, .HRefScript, .nTypeForm, , .CreateClient, , .sQueryStringClient, .bAllowInvalid, .Descript, .Digit, .CustomPage, .bAllowInvalidFormat)
                            lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.sDigit & lstrQuote
                            lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.sDescript) & lstrQuote

                        Case 9 'Notas
                            lstrResult = lstrResult & mobjValues.ButtonNotes(.sCodispl, .nNotenum, .ShowSmallImage, .bQuery, .nIndexNotenum, .nOriginalNotenum, .nCopyNotenum, , True, .FieldName, .Disabled, mlngIndexRow)

                        Case 10 'Text Area
                            lstrResult = lstrResult & mobjValues.TextAreaControl(.FieldName, .Rows, .Cols, .DefValue, .isRequired, .Alias_Renamed, False, .Disabled, .TabIndex)

                        Case 11 'File
                            lstrResult = lstrResult & mobjValues.FileControl(.FieldName, .Length, .OnClick, .Disabled, .TabIndex, .OnChange)

                        Case 12 'Company
                            lstrResult = lstrResult & mobjValues.CompanyControl(.FieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled, .FieldCompanyName, .isDIVDefine, True, .HRefUrl, .HRefScript, .TabIndex)

                        Case 13 'Consulta Asociada
                            mobjValues.sQueryString = .sQueryString
                            lstrResult = lstrResult & mobjValues.ButtonAssociate(.nKeynum, .FieldName)

                        Case 14 'Combo de ramos comerciales (Table10)
                            mobjValues.BlankPosition = .BlankPosition
                            mobjValues.TypeList = .TypeList
                            mobjValues.List = .List
                            lstrResult = lstrResult & mobjValues.BranchControl(.FieldName, .Alias_Renamed, .DefValue, String.Empty, True, .HRefUrl, .HRefScript, .OnChange, .Disabled, .TabIndex, .Descript)

                        Case 15 'Valores posibles de productos
                            mobjValues.BlankPosition = .BlankPosition
                            mobjValues.TypeList = .TypeList
                            mobjValues.List = .List
                            lstrResult = lstrResult & mobjValues.ProductControl(.FieldName, .Alias_Renamed, Columns(.FieldBranch).DefValue, Values.eValuesType.clngComboType, .Disabled, .DefValue, True, .HRefUrl, .HRefScript, .OnChange, .TabIndex, True, , .ProdClass, .Descript)
                            If .ValuesType = Values.eValuesType.clngWindowType Then
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.sDescript) & lstrQuote
                            End If

                        Case 16 'ComboControl
                            lstrResult = lstrResult & mobjValues.ComboControl(.FieldName, .List, mobjValues.HTMLDecode(.DefValue), .BlankPosition, .TabIndex, .Alias_Renamed, .OnChange, True, True)
                       Case 20 'HTML
                            lstrResult = lstrResult & .DefValue
                    End Select
                Else
                    If .ControlType = 1 And .FieldName = "Sel" Then
                        mlngIndex = mlngIndex + 1

                    End If

                    '+Si el tipo de control es Valores posibles, Control de cliente, o Producto
                    If .ValuesType = Values.eValuesType.clngWindowType Then
                        If .DefValue > String.Empty Then
                            If .ControlType = 7 Or .ControlType = 8 Or .ControlType = 14 Or .ControlType = 15 Then

                                '+Si no esta indicada la descripción se busca en la tabla
                                If .Descript = String.Empty Then
                                    If .ControlType = 8 Then
                                        lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
                                        If lobjClient.Find(.DefValue) Then
                                            .Descript = lobjClient.sCliename
                                            .Digit = lobjClient.sDigit
                                        End If
                                        'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                        lobjClient = Nothing
                                    Else
                                        If mobjTables Is Nothing Then
                                            mobjTables = New Tables
                                        End If
                                        If .NeedParam Then
                                            mobjTables.Parameters = .Parameters
                                        End If
                                        If mobjTables.reaTable(.TableName, .DefValue) Then
                                            .Descript = mobjTables.Fields(mobjTables.DescriptField)
                                        End If
                                    End If
                                End If

                                '+Si el tipo de control Control de cliente, se asigna el digito
                            End If
                        Else
                            .Digit = String.Empty
                            .Descript = String.Empty
                        End If
                        If .ControlType = 8 Then
                            lstrAssigV = lstrAssigV & lstrColon & lstrQuote & .Digit & lstrQuote
                        End If
                        lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(.Descript) & lstrQuote
                    End If
                End If
            End With
            lstrQuote = String.Empty
        Next lobjColumn

        '**+ Add the chain that contains the javascript's script to execute by each column of the arrengement (grid)
        '+Se agrega la cadena que contiene el script de javascript a ejecutarse por cada columna del arreglo (grid)

        If Not bOnlyForQuery Then
            lstrScript = "<SCRIPT>" & "insAddRecordArray" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(" & lstrAssigV & ");" & "</SCRIPT>"
        End If
        DoRow = Trim(lstrResult) & Trim(lstrScript) & "</TD></TR>" & vbCrLf

        '    #If LOG Then
        '        eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|DoRow", sSessionID
        '    #End If

    End Function

    '%DoFormUpd: Retorna el código HTML que dibuja la ventana PopUp del grid definido
    Public Function DoFormUpd(ByVal Action As String, ByVal ValPage As String, ByVal Codispl As String, ByVal MainAction As String, Optional ByVal ActionQuery As Boolean = False, Optional ByVal Index As Short = 0, Optional ByVal sContent As String = "") As String
        Dim lstrOnBlur As String
        Dim lstrCondition As String
        Dim lstrResult As String
        Dim lobjColumn As Column
        Dim lintColumns As Short
        Dim lstrTR As String
        Dim lstrTD As String
        Dim lobjValues As Values = New Values
        Dim lstrScript As String
        Dim lstrAssigV As String
        Dim lintIndex As Short
        Dim lintQuantity As Short
        Dim lstrFieldName As String

        Dim lintCountSplitProc As Short
        Dim lintCountColSplitProc As Short
        Dim lblnCreateSplit As Boolean
        Dim lblnCreateHorLine As Boolean

        lintColumns = 0
        lintIndex = 0
        lstrTR = "<TR>"
        lstrTD = "<TD>"
        lintQuantity = 0
        lstrResult = String.Empty
        lstrScript = String.Empty
        lstrAssigV = String.Empty
        lstrOnBlur = String.Empty
        lstrCondition = String.Empty
        mblnNewRows = True

        If UCase(Action) = "DELETE" Or UCase(Action) = "DEL" Then
            lobjValues = New Values
            lobjValues.sCodisplPage = sCodisplPage
            If mblnInsBody Then
                lstrResult = "<SCRIPT>top.opener.DeleteRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(" & CStr(Index) & ");</SCRIPT>"
            End If
            lstrResult = lstrResult & lobjValues.ConfirmDelete(True) & vbCrLf
            '+Se llama a la función que refresca la imagen de contenido de la transacción
            If mblnUpdContent Then
                If sContent <> String.Empty Then
                    lstrResult = lstrResult & lobjValues.UpdContent(Codispl, sContent)
                End If
            End If
            'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjValues = Nothing
        Else
            lintCountSplitProc = 1
            lintCountColSplitProc = 0
            lblnCreateSplit = False
            lblnCreateHorLine = False
            For Each lobjColumn In Columns
                With lobjColumn
                    lstrFieldName = .FieldName
                    If .PopUpVisible Then
                        If lstrFieldName <> "Sel" Then 'Hide
                            'If mintFieldByRow <= 2 Then
                            '+ Si la cantidad de Splits procesados es menor a la cantidad total de Splits
                            If lintCountSplitProc <= Splits_Renamed.Count Then
                                '+ Si ya se procesaron todos los campos que pertenecen al Split
                                If lintCountColSplitProc = Splits_Renamed(lintCountSplitProc).nCols Then
                                    If Splits_Renamed(lintCountSplitProc).sTitle <> String.Empty Then
                                        lstrResult = lstrResult & "<TR>" & "<TD COLSPAN=""" & mintFieldByRow * 2 & """>&nbsp;</TD></TR>"
                                    End If
                                    lintCountSplitProc = lintCountSplitProc + 1
                                    lintCountColSplitProc = 0
                                End If

                                '+ Si se va a empezar a procesar el Split
                                If lintCountColSplitProc = 0 And lintCountSplitProc <= Splits_Renamed.Count And .ControlType <> 5 Then
                                    If Splits_Renamed(lintCountSplitProc).sTitle <> String.Empty Then
                                        lstrResult = lstrResult & lstrTR & "<TD COLSPAN=""" & mintFieldByRow * 2 & """ CLASS=""HighLighted"">" & "<LABEL>" & Splits_Renamed(lintCountSplitProc).sTitle & "</LABEL></TD></TR>" & lstrTR & "<TD COLSPAN=""" & mintFieldByRow * 2 & """ CLASS=""HorLine""></TD></TR>"
                                    End If
                                End If
                                'End If
                            End If

                            If .ControlType <> 5 Then
                                lstrResult = lstrResult & IIf(ControlByRow(8, lintIndex), "<TR VALIGN=""TOP"">", lstrTR) & lstrTD
                                lstrResult = Trim(lstrResult) & "<LABEL>" & Trim(.Title) & "</LABEL></TD><TD>"
                            End If
                            If lobjValues Is Nothing Then
                                lobjValues = New Values
                                lobjValues.sCodisplPage = sCodisplPage
                            End If
                            lobjValues.sQueryString = sQueryString
                            Select Case .ControlType
                                Case 1 'Check
                                    lstrResult = Trim(lstrResult) & lobjValues.CheckControl(lstrFieldName, .Descript, CStr(.Checked), .DefValue, .OnClick, .Disabled Or EditRecordQuery, .TabIndex, .Alias_Renamed)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".checked = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 2 'Numerico
                                    lstrResult = Trim(lstrResult) & lobjValues.NumericControl(lstrFieldName, .Length, .DefValue, .isRequired, .Alias_Renamed, .ShowThousand, .DecimalPlaces, False, .HRefUrl, .HRefScript, .OnChange, .Disabled Or EditRecordQuery, .TabIndex, , .bAllowNegativ)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 3 'Texto
                                    lobjValues.bNumericText = .bNumericText
                                    lobjValues.List = .List
                                    lstrResult = Trim(lstrResult) & lobjValues.TextControl(lstrFieldName, .Length, .DefValue, .isRequired, .Alias_Renamed, False, .HRefUrl, .HRefScript, .OnChange, .Disabled Or EditRecordQuery, .TabIndex)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 4 'Imagen
                                    lobjValues.Width = .Width
                                    lobjValues.Height = .Height
                                    lstrResult = Trim(lstrResult) & lobjValues.AnimatedButtonControl(lstrFieldName, .Src, .Alias_Renamed, .HRefUrl, .HRefScript, .Disabled Or EditRecordQuery, .TabIndex)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 5 'Hide
                                    lstrResult = Trim(lstrResult) & lobjValues.HiddenControl(lstrFieldName, .DefValue)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 6 'Fecha
                                    lstrResult = Trim(lstrResult) & lobjValues.DateControl(lstrFieldName, .DefValue, .isRequired, .Alias_Renamed, False, .HRefUrl, .HRefScript, .OnChange, .Disabled Or EditRecordQuery, .TabIndex)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 7 'Combo
                                    lobjValues.Parameters = .Parameters
                                    lobjValues.BlankPosition = .BlankPosition
                                    lobjValues.TypeList = .TypeList
                                    lobjValues.List = .List
                                    lobjValues.TypeOrder = .TypeOrder
                                    lstrResult = Trim(lstrResult) & lobjValues.PossiblesValues(lstrFieldName, .TableName, .ValuesType, .DefValue, .NeedParam, False, .HRefUrl, .HRefScript, .ComboSize, .OnChange, .Disabled Or EditRecordQuery, .MaxLength, .Alias_Renamed, .CodeType, .TabIndex, , .bAllowInvalid, .Descript, .NotCache, .KeyField)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf
                                    'UPGRADE_NOTE: Object lobjValues.Parameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                    lobjValues.Parameters = Nothing
                                    If .ValuesType = Values.eValuesType.clngWindowType Then
                                        lstrAssigV = lstrAssigV & "UpdateDiv('" & lstrFieldName & "Desc" & "',top.opener." & mstrArrayName & "[Index]." & lstrFieldName & "Desc" & ");" & vbCrLf
                                        '+ se quita esta condición, ya que siempre debería ejecutar el onblur cuandom se trate de un valores posibles
                                        '                                    If .OnChange > String.Empty Then
                                        'lstrOnBlur = lstrOnBlur & .FieldName & ".onblur();" & vbCr
                                        lstrOnBlur = lstrOnBlur & "$('#" & .FieldName & "').change();" & vbCr
                                        lstrCondition = lstrCondition & "typeof(" & .FieldName & ".onblur)!='undefined' && "
                                        '                                    End If
                                    End If

                                Case 8 'Cliente
                                    lobjValues.ClientRole = .ClientRole
                                    lobjValues.TypeList = .TypeList
                                    lstrResult = Trim(lstrResult) & lobjValues.ClientControl(lstrFieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled Or EditRecordQuery, .FieldClieName, .isDIVDefine, False, , , .nTypeForm, .TabIndex, .CreateClient, .Separate, .sQueryStringClient, .bAllowInvalid, String.Empty, String.Empty, .CustomPage, .bAllowInvalidFormat)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf
                                    lstrAssigV = lstrAssigV & lstrFieldName & "_Digit" & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & "_Digit" & ";" & vbCrLf
                                    lstrAssigV = lstrAssigV & "UpdateDiv('" & .FieldClieName & "',top.opener." & mstrArrayName & "[Index]." & .FieldClieName & ");" & vbCrLf
                                    If .OnChange > String.Empty Then
                                        lstrOnBlur = lstrOnBlur & .FieldName & "_Digit" & "_Old.value = '';$('#" & .FieldName & "_Digit" & "').change();" & vbCr
                                        lstrCondition = lstrCondition & "typeof(" & .FieldName & "_Digit" & ".onblur)!='undefined' && "
                                    End If

                                Case 9 'Notas
                                    lstrResult = Trim(lstrResult) & lobjValues.ButtonNotes(.sCodispl, .nNotenum, .ShowSmallImage, .bQuery, .nIndexNotenum, .nOriginalNotenum, .nCopyNotenum, .TabIndex, True, lstrFieldName)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 10 'Text Area Control
                                    lobjValues.Opacity = .Opacity
                                    lstrResult = Trim(lstrResult) & lobjValues.TextAreaControl(lstrFieldName, .Rows, .Cols, .DefValue, .isRequired, .Alias_Renamed, False, .Disabled Or EditRecordQuery, .TabIndex)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf
                                    lobjValues.Opacity = 100
                                Case 11 'File
                                    lstrResult = Trim(lstrResult) & lobjValues.FileControl(lstrFieldName, .Length, .OnClick, .Disabled Or EditRecordQuery, .TabIndex, .OnChange)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 12 'Company
                                    If Not .Separate Then
                                        lstrResult = Trim(lstrResult) & lobjValues.CompanyControl(lstrFieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled Or EditRecordQuery, .FieldCompanyName, .isDIVDefine, False, , , .TabIndex)
                                    Else
                                        lstrResult = Trim(lstrResult) & lobjValues.CompanyControl(lstrFieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled Or EditRecordQuery, .FieldCompanyName & "Des", True, False, , , .TabIndex) & "</TR><TR COLSPAN=2>" & lobjValues.DIVControl(IIf(.FieldCompanyName = String.Empty, lstrFieldName, .FieldCompanyName) & "Des", , String.Empty)
                                    End If
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 14 'BranchControl
                                    lobjValues.BlankPosition = .BlankPosition
                                    lobjValues.TypeList = .TypeList
                                    lobjValues.List = .List
                                    lstrResult = Trim(lstrResult) & lobjValues.BranchControl(lstrFieldName, .Alias_Renamed, .DefValue, .FieldProduct, False, .HRefUrl, .HRefScript, .OnChange, .Disabled Or EditRecordQuery, .TabIndex)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                                Case 15 'ProductControl
                                    lobjValues.Parameters = .Parameters
                                    lobjValues.BlankPosition = .BlankPosition
                                    lobjValues.TypeList = .TypeList
                                    lobjValues.List = .List
                                    lstrResult = Trim(lstrResult) & lobjValues.ProductControl(lstrFieldName, .Alias_Renamed, Columns(.FieldBranch).DefValue, .ValuesType, .Disabled Or EditRecordQuery, .DefValue, False, .HRefUrl, .HRefScript, .OnChange, .TabIndex, True)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".Parameters.Param1.sValue  = " & .FieldBranch & ".value;" & vbCrLf
                                    'UPGRADE_NOTE: Object lobjValues.Parameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                    lobjValues.Parameters = Nothing
                                    If .ValuesType = Values.eValuesType.clngWindowType Then
                                        lstrAssigV = lstrAssigV & "UpdateDiv('" & lstrFieldName & "Desc" & "',top.opener." & mstrArrayName & "[Index]." & lstrFieldName & "Desc" & ");" & vbCrLf
                                        If .OnChange > String.Empty Then
                                            'lstrOnBlur = lstrOnBlur & .FieldName & ".onblur();" & vbCr
                                            lstrOnBlur = lstrOnBlur & "$('#" & .FieldName & "').change();" & vbCr
                                            lstrCondition = lstrCondition & "typeof(" & .FieldName & ".onblur)!='undefined' && "
                                        End If
                                    End If

                                Case 16 'ComboControl
                                    lstrResult = Trim(lstrResult) & lobjValues.ComboControl(lstrFieldName, .List, .DefValue, .BlankPosition, .TabIndex, .Alias_Renamed, .OnChange, .Disabled)
                                    lstrAssigV = lstrAssigV & lstrFieldName & ".value = top.opener." & mstrArrayName & "[Index]." & lstrFieldName & ";" & vbCrLf

                            End Select
                            If .ControlType <> 5 Then
                                lintQuantity = lintQuantity + 1
                                If lintQuantity = mintFieldByRow Then
                                    lstrTR = "</TD></TR>" & vbCrLf
                                    mblnNewRows = True
                                    lintQuantity = 0
                                Else
                                    '                                If lintQuantity > mintFieldByRow Then
                                    '                                    lstrTR = "</TD>"
                                    '                                    lintQuantity = 1
                                    '                                Else
                                    lstrTR = "</TD>" & vbCrLf
                                    '                                End If
                                End If
                                lintColumns = lintColumns + 1
                                lintCountColSplitProc = lintCountColSplitProc + 1
                            End If
                        End If
                    End If
                End With
                lintIndex = lintIndex + 1
            Next lobjColumn
            If lintColumns > 0 Then
                lstrTR = "</TR>"
                lstrTD = "</TD>"
                lstrScript = "<SCRIPT LANGUAGE=javascript>var CurrentIndex=parseFloat(" & CStr(Index) & "); " & vbCrLf & " var marrControls = new Array(); " & vbCrLf & " function ChangeSubmit(Option){" & vbCrLf & " var lstrQueryString=document.location.href.replace(/.*sCodispl=/,'');lstrQueryString=(lstrQueryString.indexOf('&')>=0?lstrQueryString.substring(lstrQueryString.indexOf('&')):'');" & " switch (Option) {" & vbCrLf & " case ""Add"": " & vbCrLf & " document.forms[" & nParentForm & "].action=""" & ValPage & "?nZone=2&sCodispl=" & Codispl & "&Action=Add&ReloadIndex=-1&Index=-1&WindowType=PopUp&nMainAction=" & MainAction & """+ lstrQueryString;" & vbCrLf & " break;" & vbCrLf & " case ""Update"":" & vbCrLf & " document.forms[" & nParentForm & "].action =""" & ValPage & "?nZone=2&sCodispl=" & Codispl & "&Action=Update&ReloadIndex="" + CurrentIndex + ""&Index="" + CurrentIndex + ""&WindowType=PopUp&nMainAction=" & MainAction & """+ lstrQueryString;}}" & vbCrLf
                lstrScript = lstrScript & " function ShowFields(Index){if(Index==-1)return 0;" & vbCrLf & " with (self.document.forms[" & nParentForm & "]){" & vbCrLf & lstrAssigV & "}}" & vbCrLf & " function MoveRecord(Option){ " & vbCrLf & "    var lintIndex = CurrentIndex; " & vbCrLf & "    switch (Option){" & vbCrLf & "       case ""Back"":" & vbCrLf & "            lintIndex--; " & vbCrLf & "            break;" & vbCrLf & "       case ""Next"":" & vbCrLf & "            lintIndex++;" & vbCrLf & "    }" & vbCrLf & "    if (lintIndex >= 0)" & vbCrLf & "        if (lintIndex < top.opener." & mstrArrayName & ".length){" & vbCrLf & "            ShowFields(lintIndex);" & vbCrLf & "            CurrentIndex = lintIndex }" & vbCrLf & "    ChangeSubmit('Update');" & vbCrLf & "    with(self.document.forms[" & nParentForm & "]){" & vbCrLf & lstrOnBlur & "    }"
                If MoveRecordScript > String.Empty Then
                    lstrScript = lstrScript & MoveRecordScript & ";" & vbCrLf
                End If
                lstrScript = lstrScript & "} </SCRIPT>"
            End If
            lstrResult = Trim(lstrScript) & "<TABLE COLS=" & CStr(mintFieldByRow * 2) & " WIDTH=100% CELLSPACING=1 CELLPADDING=1>" & vbCrLf & Trim(lstrResult) & lstrTD & lstrTR & "</TABLE>" & insConstructFooter(Action, ActionQuery, Index, MainAction) & vbCrLf
            'lstrResult = lstrResult & "<SCRIPT>" & vbCrLf & "var lblnContinue = true;" & vbCrLf & "function insShowDescript(){" & vbCrLf & "    with(self.document.forms[" & nParentForm & "]){" & vbCrLf & "        if(" & lstrCondition & "lblnContinue){" & vbCrLf & lstrOnBlur & vbCrLf & "            lblnContinue = false" & vbCrLf & "        }" & vbCrLf & "    }" & vbCrLf & "}" & vbCrLf & "    if (lblnContinue)" & vbCrLf & "        setTimeout(""insShowDescript()"",50);" & vbCrLf & "</SCRIPT>"
            lstrResult = lstrResult & "<SCRIPT>" & vbCrLf & "var lblnContinue = true;" & vbCrLf & "function insShowDescript(){" & vbCrLf & "    with(self.document.forms[" & nParentForm & "]){" & vbCrLf & "        if(lblnContinue){" & vbCrLf & lstrOnBlur & vbCrLf & "            lblnContinue = false" & vbCrLf & "        }" & vbCrLf & "    }" & vbCrLf & "}" & vbCrLf & "    if (lblnContinue)" & vbCrLf & "        setTimeout(""insShowDescript()"",50);" & vbCrLf & "</SCRIPT>"

        End If
        DoFormUpd = lstrResult
        'UPGRADE_NOTE: Object lobjColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjColumn = Nothing
    End Function

    '% insConstructFooter: se definen los botones de la ventana PopUp cuando se edita el registro
    Private Function insConstructFooter(ByVal sAction As String, ByVal bActionQuery As Boolean, ByVal nIndex As Short, ByVal sMainAction As String) As String
        Dim lstrResult As String
        Dim lobjValues As Values

        lstrResult = "<TABLE WIDTH=100%>" & vbCrLf

        '**+ If the action is Update, write the buttons for the next and previous
        '+Si la accion es Update, se escriben los botones para próximo y anterior

        lobjValues = New Values
        lobjValues.sCodisplPage = sCodisplPage

        sAction = UCase(sAction)

        If (sAction = "UPDATE" Or sAction = "QUERY") And sMainAction <> CStr(Menues.TypeActions.clngActionCondition) Then
            lstrResult = lstrResult & lobjValues.ButtonBackNext(3)
        End If
        '**+ The continue check is shown if itis wanted to, or if the action id differen tha Consult by condition
        '+ El Check de continuar se muestra si así se desea, o si la acción es diferente a Consulta por condición
        lstrResult = lstrResult & "<TR><TD COLSPAN=3><HR></TD></TR>" & vbCrLf & "<TR><TD>" & IIf(sMainAction <> CStr(Menues.TypeActions.clngActionCondition) And bCheckVisible And Not EditRecordQuery, lobjValues.CheckControl("chkContinue", "Continuar", "0", , , , , "Continuar con la acción en tratamiento"), "&nbsp;") & "</TD>" & vbCrLf & "<TD WIDTH=80% CLASS=HIGHLIGHTED><LABEL><DIV ID=lblWaitProcess><BR></DIV></LABEL></TD>" & "<TD ALIGN=""Right"">"

        If Not bActionQuery Then
            lstrResult = lstrResult & lobjValues.ButtonAcceptCancel("StatusControl(true, 2);EnabledControl('fraFolder');top.frames['fraFolder'].document.forms[" & nParentForm & "].target='fraGeneric';setPointer('wait');if(typeof(top.opener.top.fraHeader)!='undefined') top.opener.top.fraHeader.setPointer('wait');", mstrCancelScript & "if(typeof(top.opener.top.fraHeader)!='undefined') top.opener.top.fraHeader.setPointer('');top.close()", True)
        Else
            With lobjValues
                .ActionQuery = False
                lstrResult = lstrResult & .ButtonAcceptCancel("StatusControl(true, 2);EnabledControl('fraFolder')", "top.close()", , , Values.eButtonsToShow.OnlyCancel)
                .ActionQuery = bActionQuery
            End With
        End If
        lstrResult = Trim(lstrResult) & "</TABLE>"

        Select Case UCase(sAction)
            Case "ADD"
                lstrResult = Trim(lstrResult) & "<SCRIPT>ChangeSubmit(""Add"");</SCRIPT>" & vbCrLf
            Case "UPDATE"
                lstrResult = Trim(lstrResult) & "<SCRIPT>ShowFields(" & CStr(nIndex) & ");" & vbCrLf & "ChangeSubmit(""Update"");</SCRIPT>" & vbCrLf
            Case "QUERY"
                lstrResult = Trim(lstrResult) & "<SCRIPT>ShowFields(" & CStr(nIndex) & ");</SCRIPT>" & vbCrLf
        End Select
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
        insConstructFooter = Trim(lstrResult)
    End Function

    '% insEditRecord: se coloca el código para editar los registros del grid
    Private Function insEditRecord(ByVal nIndex As Integer, Optional ByVal sQueryString As String = "") As String
        Dim lstrAction As String
        Dim lstrMainAction As String

        lstrAction = IIf(EditRecordQuery, "Query", "Update")
        lstrMainAction = IIf(EditRecordQuery, "401", "nMainAction")

        insEditRecord = "EditRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(" & CStr(nIndex) & "," & lstrMainAction & ",'" & lstrAction & "','" & sEditRecordParam & sQueryString & "')"
    End Function

    '**% CloseTable. This method is in charge of generate the html label </TABLE>
    '**% to end a table.
    '%CloseTable. este método se encarga de generar la etiqueta html </TABLE>
    '%para finalizar una tabla.
    Public Function closeTable() As String
        Dim lobjValues As Values

        '**+ If the mblnHeader variable owns true, means that the table
        '**+ has no records. Then, show the row that sends the mesage of
        '**+ "There is no records to show"
        '+ Si la variable mblnHeader posee verdadero, quiere decir que la tabla no
        '+tiene registros. Entonces, se moestra la fila que manda el mensaje de "
        '+No existen registros a mostrar"
        If mblnHeader Then
            '**+ Place in false the variable that indicate to the grid that records can be deleted.
            '+Se coloca en falso la variable que le indica al grid que se pueden eliminar registros
            lobjValues = New Values
            lobjValues.sCodisplPage = sCodisplPage
            mblnDelButton = False
            closeTable = DoHeader()
            If Not bUpdateGrid Then
                closeTable = closeTable & "<TR>" & lobjValues.DataNotFound(mintColumns) & "</TR> " & vbCrLf
            End If
        Else
            closeTable = String.Empty
        End If

        If bUpdateGrid Then
            closeTable = closeTable & DoRowUpd()
        End If

        closeTable = Trim(closeTable) & "</TABLE>" & vbCrLf

        '**+ Verify if the PopUp page must be reloaded.
        '+Se verifica si se debe recargar la página PopUp
        If Trim(sReloadIndex) <> String.Empty Then
            closeTable = Trim(closeTable) & "<SCRIPT>EditRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(" & sReloadIndex & ",nMainAction,'" & IIf(sReloadIndex = "-1", "Add", "Update") & "','" & sEditRecordParam & "');</SCRIPT>" & vbCrLf
        End If
        mstrClassTR = "EVEN"
        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '% ControlByRow: verifica si en la fila existe un tipo de control dado
    Private Function ControlByRow(ByVal ControlType As Short, ByVal nCurrentIndex As Short) As Boolean
        Dim lintCount As Short
        Dim lintMaxColumn As Short

        lintCount = nCurrentIndex + 1
        lintMaxColumn = nCurrentIndex + 2

        ControlByRow = False

        While lintCount <= lintMaxColumn
            If Columns.Count > lintCount Then
                With Columns(lintCount)
                    If .FieldName <> "Sel" And .ControlType <> 5 Then
                        If Columns(lintCount).ControlType = ControlType Then
                            If mblnNewRows Then
                                ControlByRow = True
                            End If
                            lintCount = lintMaxColumn
                        End If
                    End If
                End With
            End If
            lintCount = lintCount + 1
        End While
        mblnNewRows = False
    End Function

    '%Class_Terminate: Se ejecuta cuando se instancia la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Dim lclsASPSupport As eRemoteDB.ASPSupport

        mblnHeader = True
        mblnArrayNamed = False
        mlngIndex = -1
        WidthDelete = 370
        Width = 290
        Height = 160
        Top = 200
        Left = 100
        Codispl = "GE000"
        Codisp = String.Empty
        Columns = New Columns
        Splits_Renamed = New Splits
        mintColumns = 0
        mintFieldByRow = 1
        mblnAddButton = True
        mblnDelButton = True
        mstrDeleteScript = "DeleteRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(-1)"
        mblnDelScript = True
        nMainAction = 0
        sEditRecordParam = String.Empty
        sDelRecordParam = String.Empty
        mblnInsBody = True
        mstrClassTR = "EVEN"
        AltRowColor = False
        sReloadIndex = String.Empty
        sReloadAction = String.Empty
        bCheckVisible = True
        MoveRecordScript = String.Empty
        mstrArrayName = "marrArray"
        Columns.sArrayName = mstrArrayName
        sQueryString = ""
        mblnUpdContent = False
        EditRecordQuery = False

        lclsASPSupport = New eRemoteDB.ASPSupport
        If lclsASPSupport.GetASPRequestValue("nMainAction") = Menues.TypeActions.clngActionQuery.ToString() Then
            bOnlyForQuery = True
        Else
            bOnlyForQuery = False
        End If
        'UPGRADE_NOTE: Object lclsASPSupport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsASPSupport = Nothing

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%Class_Terminate: Se ejecuta cuando se destruye la clase
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object Columns may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Columns = Nothing
        'UPGRADE_NOTE: Object Splits_Renamed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Splits_Renamed = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    '%SetWindowParameters:
    Public Sub SetWindowParameters(ByVal sCodispl As String, ByVal sWindowDescript As String, ByVal sWindowTy As String)
        mstrWindowsCodispl = sCodispl
        mstrWindowDescript = HTMLEncode(sWindowDescript)
        mstrWindowTy = sWindowTy
    End Sub

    '**% DoRow: This method is in charge of generate each row of the window's grid.
    '%DoRow: Este metodo se encarga de generar cada fila del grid de la ventana.
    Private Function DoRowUpd() As String
        Dim lobjColumn As Column
        Dim lstrResult As String
        Dim lstrTD As String
        Dim lstrTR As String
        Dim lstrAlign As String = ""
        Dim lstrScript As String = ""
        Dim lstrAssigV As String
        Dim lstrColon As String
        Dim lstrQuote As String
        Dim lvntData As Object
        Dim lstrQueryString As String
        Dim nMaxTabIndex As Short

        '    #If LOG Then
        '        eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|DoRow", sSessionID
        '    #End If

        '**  Concatenate the header of the table in treatment.
        '+ Se concatena el encabezado de la tabla en tratamiento
        If Len(Trim(mstrArrayName)) > 1 Then
            lstrTR = " <TR NAME = grid" & mstrArrayName & " ID= grid" & mstrArrayName
        Else
            lstrTR = " <TR "
        End If

        lstrQueryString = "&sCodisp=" & mstrWindowsCodispl & "&sWindowDescript=" & mstrWindowDescript & "&nWindowTy=" & mstrWindowTy
        If AltRowColor Then
            lstrResult = DoHeader(lstrQueryString) & lstrTR & " CLASS=" & mstrClassTR & ">"
            If mstrClassTR = "EVEN" Then
                mstrClassTR = "UNEVEN"
            Else
                mstrClassTR = "EVEN"
            End If
        Else
            lstrResult = DoHeader(lstrQueryString) & lstrTR & ">"
        End If
        lstrTD = "<TD"
        lstrColon = String.Empty
        lstrQuote = String.Empty
        lstrAssigV = String.Empty
        mlngIndexRow = mlngIndexRow + 1
        '**+ run the column's collection to create a row.
        '+Se recorre la colección de columnas para crear la fila
        Dim lobjClient As Object
        For Each lobjColumn In Columns

            lstrQuote = """"
            With lobjColumn
                '+Al crear esta fila para ingresar datos, todos los campos deben quedar habilitados y sin valor
                .Disabled = False
                .DefValue = ""

                '+Se almacena el máximo tabulador para crear posteriormente el control del botón
                If .TabIndex > nMaxTabIndex Then
                    nMaxTabIndex = .TabIndex
                End If

                '+ Alineacion de la celda
                If .ControlType <> 5 And .GridVisible Then
                    Select Case .ControlType
                        Case 1
                            lstrAlign = "ALIGN=CENTER WIDTH=25pcx"
                        Case 2
                            lstrAlign = "ALIGN=RIGHT"
                        Case 6, 9, 4, 13
                            lstrAlign = "ALIGN=CENTER"
                        Case Else
                            lstrAlign = "ALIGN=LEFT"
                    End Select
                End If
                If Not bOnlyForQuery Then
                    If .FieldName = "Sel" Then
                        lstrAssigV = lstrAssigV & lstrColon & IIf(.Checked = 1, "true", "false")
                    Else
                        If mobjValues Is Nothing Then
                            mobjValues = New Values
                            mobjValues.sCodisplPage = sCodisplPage
                        End If
                        mobjValues.sQueryString = sQueryString
                        mobjValues.EditRecordQuery = EditRecordQuery
                        mobjValues.nParentForm = 1

                        Select Case .ControlType
                            '+En el caso que el control sea del tipo notas, en vez de asignar el valor de la propiedad DefValue, asignamos el valor
                            '+de la propiedad nNotenum
                            Case 9 'Notes
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.TypeToString(.nNotenum, Values.eTypeData.etdLong) & lstrQuote
                            Case 2 'Numerico
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.TypeToString(.DefValue, Values.eTypeData.etdDouble, .ShowThousand, .DecimalPlaces) & lstrQuote
                            Case 6 'Fecha
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.TypeToString(.DefValue, Values.eTypeData.etdDate) & lstrQuote
                            Case 1 'Check'
                                lstrAssigV = lstrAssigV & lstrColon & IIf(.Checked = 1, "true", "false")
                            Case Else
                                If .ControlType = 10 Then '+ Text Area
                                    .DefValue = Replace(.DefValue, vbCrLf, "\n")
                                End If
                                '+ En caso de valores numericos nulos, se agrega al arreglo un vacio
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.TypeToString(.DefValue, Values.eTypeData.etdOthers)) & lstrQuote
                        End Select
                    End If
                End If
                lstrColon = ","
                If .ControlType <> 5 And .GridVisible Then
                    If lstrTD <> String.Empty Then
                        lstrTD = lstrTD & " " & lstrAlign & ">"
                    End If
                    lstrResult = lstrResult & lstrTD
                End If
                If .ControlType <> 5 And .GridVisible Then
                    lstrTD = vbCrLf & "</TD><TD"
                End If
                If mobjValues Is Nothing Then
                    mobjValues = New Values
                    mobjValues.sCodisplPage = sCodisplPage
                    mobjValues.ActionQuery = ActionQuery
                End If

                '**+ In case the controls has the property EditRecord marked, associate the call to the script that
                '**+ shows the UPD window.
                '+En el caso de que el control tenga marcada la propiedad EditRecord, se asocia el llamado al script que
                '+Muestra la ventana UPD
                If .EditRecord And .ControlType <> 5 And .GridVisible Then
                    .HRefScript = insEditRecord(mlngIndex, lstrQueryString)
                Else
                    If EditRecordDisabled Then
                        .HRefScript = ""
                    Else
                        If mblnLoadValue Then
                            .HRefScript = ""
                        End If
                    End If
                End If

                '+ En caso de que tengamos un valores posibles que retorna más de 2 columnas, y se quiere asignar esas
                '+ columnas a un campo del grid
                If .sPossiblesVName <> String.Empty Then
                    If .DefValue = String.Empty Then
                        If .ControlType <> 1 Then
                            lvntData = Columns(.sPossiblesVName).Parameters.Item_ReturnValue(.sParamName).Value
                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                            If Not IsDBNull(lvntData) Then
                                .DefValue = lvntData
                            End If
                        End If
                    End If
                End If

                If .GridVisible Then
                    Select Case lobjColumn.ControlType
                        Case 1 'Check
                            If Trim(.FieldName) = "Sel" Then
                                mlngIndex = mlngIndex + 1
                                lstrResult = lstrResult & mobjValues.CheckControl(.FieldName, .Descript, CStr(.Checked), CStr(mlngIndex), mstrArrayName & "[this.value].Sel=this.checked;" & .OnClick, .Disabled)
                            Else
                                lstrResult = lstrResult & mobjValues.CheckControl(.FieldName, .Descript, CStr(.Checked), .DefValue, .OnClick, False, , .Alias_Renamed)
                            End If

                        Case 2 'Numerico
                            lstrResult = lstrResult & mobjValues.NumericControl(.FieldName, .Length, "", .isRequired, .Alias_Renamed, .ShowThousand, .DecimalPlaces, False, .HRefUrl, .HRefScript, .OnChange, .Disabled, .TabIndex)

                        Case 3 'Texto
                            lstrResult = lstrResult & mobjValues.TextControl(.FieldName, .Length, "", .isRequired, .Alias_Renamed, False, .HRefUrl, .HRefScript, .OnChange, .Disabled, .TabIndex)

                        Case 4 'Boton Animado
                            mobjValues.Width = .Width
                            mobjValues.Height = .Height
                            lstrResult = lstrResult & mobjValues.AnimatedButtonControl(.FieldName, .Src, .Alias_Renamed, .HRefUrl, .HRefScript, .Disabled, .TabIndex, mlngIndexRow)

                        Case 5 'Hide
                            lstrResult = lstrResult & mobjValues.HiddenControl(.FieldName, .DefValue)

                        Case 6 'Fecha
                            lstrResult = lstrResult & mobjValues.DateControl(.FieldName, .DefValue, .isRequired, .Alias_Renamed, False, .HRefUrl, .HRefScript, .OnChange, .Disabled, .TabIndex)

                        Case 7 'Combo
                            mobjValues.Parameters = .Parameters
                            mobjValues.BlankPosition = .BlankPosition
                            mobjValues.TypeList = .TypeList
                            mobjValues.List = .List
                            mobjValues.TypeOrder = .TypeOrder
                            lstrResult = lstrResult & mobjValues.PossiblesValues(.FieldName, .TableName, Values.eValuesType.clngComboType, CStr(0), .NeedParam, False, .HRefUrl, .HRefScript, .ComboSize, .OnChange, False, .MaxLength, .Alias_Renamed, .CodeType, .TabIndex, .ShowDescript, .bAllowInvalid, .Descript, .NotCache, .KeyField)
                            If .ValuesType = Values.eValuesType.clngWindowType Then
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.sDescript) & lstrQuote
                            End If
                            'UPGRADE_NOTE: Object mobjValues.Parameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            mobjValues.Parameters = Nothing

                        Case 8 'Cliente
                            mobjValues.ClientRole = .ClientRole
                            mobjValues.TypeList = .TypeList
                            lstrResult = lstrResult & mobjValues.ClientControl(.FieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled, .FieldClieName, .isDIVDefine, True, .HRefUrl, .HRefScript, .nTypeForm, .TabIndex, .CreateClient, , .sQueryStringClient, .bAllowInvalid, .Descript, .Digit, .CustomPage, .bAllowInvalidFormat)
                            lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.sDigit & lstrQuote
                            lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.sDescript) & lstrQuote

                        Case 9 'Notas
                            lstrResult = lstrResult & mobjValues.ButtonNotes(.sCodispl, .nNotenum, .ShowSmallImage, .bQuery, .nIndexNotenum, .nOriginalNotenum, .nCopyNotenum, .TabIndex, , .FieldName, .Disabled, mlngIndexRow)

                        Case 10 'Text Area
                            lstrResult = lstrResult & mobjValues.TextAreaControl(.FieldName, .Rows, .Cols, .DefValue, .isRequired, .Alias_Renamed, False, .Disabled, .TabIndex)

                        Case 11 'File
                            lstrResult = lstrResult & mobjValues.FileControl(.FieldName, .Length, .OnClick, .Disabled, .TabIndex, .OnChange)

                        Case 12 'Company
                            lstrResult = lstrResult & mobjValues.CompanyControl(.FieldName, .DefValue, .isRequired, .Alias_Renamed, .OnChange, .Disabled, .FieldCompanyName, .isDIVDefine, True, .HRefUrl, .HRefScript, .TabIndex)

                        Case 13 'Consulta Asociada
                            mobjValues.sQueryString = .sQueryString
                            lstrResult = lstrResult & mobjValues.ButtonAssociate(.nKeynum, .FieldName)

                        Case 14 'Combo de ramos comerciales (Table10)
                            mobjValues.BlankPosition = .BlankPosition
                            mobjValues.TypeList = .TypeList
                            mobjValues.List = .List
                            lstrResult = lstrResult & mobjValues.BranchControl(.FieldName, .Alias_Renamed, .DefValue, String.Empty, True, .HRefUrl, .HRefScript, .OnChange, .Disabled, .TabIndex, .Descript)

                        Case 15 'Valores posibles de productos
                            mobjValues.BlankPosition = .BlankPosition
                            mobjValues.TypeList = .TypeList
                            mobjValues.List = .List
                            lstrResult = lstrResult & mobjValues.ProductControl(.FieldName, .Alias_Renamed, Columns(.FieldBranch).DefValue, Values.eValuesType.clngComboType, .Disabled, .DefValue, True, .HRefUrl, .HRefScript, .OnChange, .TabIndex, True, , .ProdClass, .Descript)
                            If .ValuesType = Values.eValuesType.clngWindowType Then
                                lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(mobjValues.sDescript) & lstrQuote
                            End If

                        Case 16 'ComboControl
                            lstrResult = lstrResult & mobjValues.ComboControl(.FieldName, CStr(0), CStr(.BlankPosition), .TabIndex, CShort(.Alias_Renamed), .OnChange, CStr(True), True)
                    End Select
                Else
                    If .ControlType = 1 And .FieldName = "Sel" Then
                        mlngIndex = mlngIndex + 1

                    End If

                    '+Si el tipo de control es Valores posibles, Control de cliente, o Producto
                    If .ValuesType = Values.eValuesType.clngWindowType Then
                        If .DefValue > String.Empty Then
                            If .ControlType = 7 Or .ControlType = 8 Or .ControlType = 14 Or .ControlType = 15 Then

                                '+Si no esta indicada la descripción se busca en la tabla
                                If .Descript = String.Empty Then
                                    If .ControlType = 8 Then
                                        lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
                                        If lobjClient.Find(.DefValue) Then
                                            .Descript = lobjClient.sCliename
                                            .Digit = lobjClient.sDigit
                                        End If
                                        'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                        lobjClient = Nothing
                                    Else
                                        If mobjTables Is Nothing Then
                                            mobjTables = New Tables
                                        End If
                                        If .NeedParam Then
                                            mobjTables.Parameters = .Parameters
                                        End If
                                        If mobjTables.reaTable(.TableName, .DefValue) Then
                                            .Descript = mobjTables.Fields(mobjTables.DescriptField)
                                        End If
                                    End If
                                End If

                                '+Si el tipo de control Control de cliente, se asigna el digito
                            End If
                        Else
                            .Digit = String.Empty
                            .Descript = String.Empty
                        End If
                        If .ControlType = 8 Then
                            lstrAssigV = lstrAssigV & lstrColon & lstrQuote & .Digit & lstrQuote
                        End If
                        lstrAssigV = lstrAssigV & lstrColon & lstrQuote & mobjValues.HTMLDecode(.Descript) & lstrQuote
                    End If
                End If
            End With
            lstrQuote = String.Empty
        Next lobjColumn

        '**+ Add the chain that contains the javascript's script to execute by each column of the arrengement (grid)
        '+Se agrega la cadena que contiene el script de javascript a ejecutarse por cada columna del arreglo (grid)

        If Not bOnlyForQuery Then
            lstrScript = "<SCRIPT>" & "insAddRecordArray" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(" & lstrAssigV & ");" & "</SCRIPT>"
        End If

        DoRowUpd = Trim(lstrResult) & Trim(lstrScript) & "</TD>"

        DoRowUpd = "</FORM><FORM METHOD=""POST"" ID=""FORM1"" NAME=""CA051-1"" ACTION=""ValPolicyRepSeq.aspx?sCodispl=CAL659&Action=Add&nZone=2&nMainAction=302&WindowType=PopUp&EditWithoutPopPup=True"">" & DoRowUpd & "<TD>" & mobjValues.ButtonAcceptCancel("", "", True, , Values.eButtonsToShow.OnlyAccept, nMaxTabIndex + 1) & "</TD></TR>" & vbCrLf

        '    #If LOG Then
        '        eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|DoRow", sSessionID
        '    #End If

    End Function
End Class






