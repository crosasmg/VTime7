Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Web
Imports System.Threading
Imports System.Globalization
Imports System.Configuration
Imports System.Windows.Forms
Imports System.IO
Imports System.Resources

Public Module Extensions
    <Runtime.CompilerServices.Extension()>
    Public Function FindDictionaryValue(ByVal entries As IEnumerable(Of DictionaryEntry), ByVal key As String) As String
        Return entries.Where(Function(x) x.Key = key).FirstOrDefault().Value
    End Function
End Module
Public Class Values
    '%-------------------------------------------------------%'
    '% $Workfile:: Values.cls                               $%'
    '% $Author:: Clobos                                     $%'
    '% $Date:: 4-04-06 18:48                                $%'
    '% $Revision:: 6                                        $%'
    '%-------------------------------------------------------%'

    Public sCodisplPage As String

    '-Numero que identifiCA el formnulario donde se encuentra el control de grid
    Public nParentForm As Short


    Private Const C_Separator As String = vbCrLf

    Private Const numNull As Short = -32768
    Private Const dblNull As Double = -32768.3276

    Public Const vbChecked As String = "1"
    Public Const vbUnChecked As String = "0"

    Public Enum eValuesType
        clngComboType = 1
        clngWindowType = 2
    End Enum

    Public Enum eTypeCode
        eNumeric = 1
        eString = 2
    End Enum

    Enum ecbeTypeList
        none
        Inclution
        Exclution
    End Enum

    Public Enum ecbeOrder
        Code = 1
        Descript = 2
    End Enum

    Public Enum eProdClass
        clngAll = 1
        clngActiveLife = 2
        clngAnnuitiesLife = 3
    End Enum

    Private mstrList As String
    Private meTypeList As ecbeTypeList
    Private meOrder As ecbeOrder
    Public mblnBlank As Boolean
    Private mstrBlankDesc As String
    Private mvntCodeValue As Object
    Private mstrBeginPageLink As String

    Private mParameters As Parameters
    Private Count As Short

    '-Se define la variable mstrCombHtm, para contener la instrucción HTML
    Private mstrCombHtm As String

    '-Se define la variable mstrDefValue, para contener el valor por defecto a seleccionar
    Private mstrDefValue As String

    '-Se define la variable que indica si la transacción se ejecuta en modo consulta
    Public mblnActionQuery As Boolean

    '-Se define la variable que indica si valida el campo
    Private mblnValid As Boolean

    '-Se define la variable mblnIsValid, para verificar si el contenido de un campo es valido o no
    Private mblnIsValid As Boolean

    '-Se define los tipos de campos
    Private Enum eTypeField
        clngNumericValue = 1
        clngTextValue = 2
        clngDateValue = 3
    End Enum

    '- Se defina la variable privada mblnGridField, para indicar si el campo se encuentra dentro
    '- Grid (o tabla)

    Private mblnGridField As Boolean

    '- Tipos de datos
    Public Enum eTypeData
        etdDate = 1
        etdInteger = 2
        etdLong = 3
        etdDouble = 4
        etdOthers = 5
        etdBoolean = 6
    End Enum

    '- Botones a incluir en un control ButtonAcceptCancel
    Public Enum eButtonsToShow
        All = 0
        OnlyAccept = 1
        OnlyCancel = 2
    End Enum

    '- Tipo de forma mostrar cuando se activa el control de clientes
    Public Enum eTypeClient
        SearchClient = 1
        SearchClientPolicy = 2
        SearchClientClaim = 3
    End Enum

    '- Se definen las variables para verificar si la búsqueda de una ventana ya se realizó

    Private mstrDescriptWindows As String
    Private mstrCodispl As String

    '- Variables para ser usadas para estableces el alto y ancho de las imágenes
    Public Width As Integer
    Public Height As Integer

    '-Variable que guarda el QueryString a pasarle a la ventana llamada
    Public sQueryString As String

    '- Se declaran las variables para almacenar el formato de la fecha para el Usuario, asi como el separado usado en la misma
    Public msUserDateFormat As String
    Public msUserDateSeparator As String

    '- Se declaran las variables para almacenar el formato de los numeros para el Usuario , asi como el separado usado en la misma

    Public msUserDecimalSeparator As String
    Public msUserThousandSeparator As String

    '- Se declaran las variables para almacenar el formato de los numeros para el Servidor, asi como el separado usado en la misma

    Public msServerDecimalSeparator As String

    '- Se declara la variable para almacenar el rol a incluir/excluir en la ventana "Clientes permitidos en la póliza"
    Public ClientRole As String

    '- Variable para oculatr campos que se requiere esten creados, 0 no se muestra, 100 se muestra normal
    Public Opacity As Integer = 100

    '- Se declara propiedad para validar un TexControl que se ocupa como NumericControl
    Public bNumericText As Boolean

    '- Se declara la propiedad para controlar el dígito verificador del RUT del cliente
    Public sDigit As String

    '- Propiedad para controlar la creación del vínculo en el grid, cuando es consulta
    Public EditRecordQuery As Boolean

    Public setdate As String

    Private mstrCachePath As String

    '-Variable que guarda el número de sesión
    Public sSessionID As String

    '-Código del usuario
    Public nUsercode As Integer

    Private mblnCacheEnabled As Boolean

    Private mblnCreateCache As Boolean

    '-Variable que guarda la descripción de un valor por defecto de un possiblevalues
    '-o el nombre del cliente en caso de un control de cliente
    Public sDescript As String

    Public Enum eUrlType
        cstrGrid = 1
        cstrFolder = 2
    End Enum

    '% ComboControl : Retorna un combo con los valores de una lista (string) separada
    '% por coma.
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Function ComboControl(ByVal sName As String, ByVal sListCSV As String, Optional ByVal sDefValue As String = "", Optional ByVal bBlankPosition As Boolean = True, Optional ByVal TabIndex As Short = 0, Optional ByVal Alias_Renamed As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal GridField As Boolean = False) As String
        Dim lintComma As Short
        Dim lintPaid As Short
        Dim lstrCode As String = String.Empty
        Dim lstrDescript As String = String.Empty

        If Not mblnActionQuery And Not mblnGridField Then

            ComboControl = "<SELECT NAME=""" & sName & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """"

            If Disabled Then
                ComboControl = DisabledControl(ComboControl)
            End If

            If OnChange = String.Empty OrElse OnChange = String.Empty Then
                ComboControl = ComboControl & ">"
            Else
                ComboControl = ComboControl & " ONCHANGE='" & OnChange & "'>"
            End If
            ComboControl = ComboControl & IIf(bBlankPosition, "<OPTION VALUE=""0""></OPTION>", String.Empty)

            lintComma = InStr(sListCSV, ",")
            Do While lintComma <> 0
                lstrDescript = Left(sListCSV, lintComma - 1)
                lintPaid = InStr(sListCSV, "|") - 1
                If lintPaid > 0 Then
                    lstrCode = Left(sListCSV, lintPaid)
                    lstrDescript = Mid(lstrDescript, lintPaid + 2)
                Else
                    lstrCode = lstrDescript
                End If
                ComboControl = ComboControl & "<OPTION VALUE=""" & lstrCode & """" & IIf(Trim(lstrCode) = sDefValue, " SELECTED", String.Empty) & ">" & lstrDescript & "</OPTION>"
                sListCSV = Mid(sListCSV, lintComma + 1)
                lintComma = InStr(sListCSV, ",")
            Loop
            If Not Len(sListCSV) = 0 Then
                lstrDescript = sListCSV
                lintPaid = InStr(sListCSV, "|") - 1
                If lintPaid > 0 Then
                    lstrCode = Left(sListCSV, lintPaid)
                    lstrDescript = Mid(lstrDescript, lintPaid + 2)
                Else
                    lstrCode = lstrDescript
                End If
                ComboControl = ComboControl & "<OPTION VALUE=""" & lstrCode & """" & IIf(Trim(lstrCode) = sDefValue, " SELECTED", String.Empty) & ">" & lstrDescript & "</OPTION>"
            End If

            ComboControl = ComboControl & "</SELECT>"
        Else
            lintComma = InStr(sListCSV, ",")
            Do While lintComma <> 0
                lstrDescript = Left(sListCSV, lintComma - 1)
                lintPaid = InStr(sListCSV, "|") - 1
                If lintPaid > 0 Then
                    lstrCode = Left(sListCSV, lintPaid)
                    lstrDescript = Mid(lstrDescript, lintPaid + 2)
                Else
                    lstrCode = lstrDescript
                End If
                If lstrCode = sDefValue Then
                    sListCSV = ""
                    Exit Do
                End If
                sListCSV = Mid(sListCSV, lintComma + 1)
                lintComma = InStr(sListCSV, ",")
            Loop
            If Not Len(sListCSV) = 0 Then
                lstrDescript = sListCSV
                lintPaid = InStr(sListCSV, "|") - 1
                If lintPaid > 0 Then
                    lstrCode = Left(sListCSV, lintPaid)
                    lstrDescript = Mid(lstrDescript, lintPaid + 2)
                Else
                    lstrCode = lstrDescript
                End If
            End If
            If lstrCode <> sDefValue Then
                lstrDescript = ""
            End If
            ComboControl = "<LABEL TITLE=""" & Alias_Renamed & """ CLASS=""FIELD"">"
            ComboControl = ComboControl & lstrDescript
            ComboControl = ComboControl & "</LABEL>"
            ComboControl = ComboControl
        End If
    End Function

    '% ButtonAdd: Retorna la estructura de un botón para la inserción de datos.
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function ButtonAdd(ByVal HRefScript As String, Optional ByVal Name As String = "cmdAdd", Optional ByVal Alias_Renamed As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        Dim varAux As String = ""
        '+ Si se encuentra en consulta no se muestra el control.
        If Not mblnActionQuery Then
            '+ Se obtiene el Alias que corresponde al botón.
            If Trim(Alias_Renamed) = String.Empty Then
                Alias_Renamed = HttpContext.GetGlobalResourceObject("BackOfficeResource", "AddTitle")
            End If
            varAux = AnimatedButtonControl(Name, "/VTimeNet/Images/btnAddOff.png", Alias_Renamed, , HRefScript, Disabled, TabIndex)
        End If
        Return varAux
    End Function

    '% ButtonDelete: Retorna la estructura de un botón de eliminación.
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function ButtonDelete(ByVal HRefScript As String, Optional ByVal Name As String = "cmdDelete", Optional ByVal Alias_Renamed As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        Dim varAux As String = ""
        '+ Si se encuentra en consulta no se muestra el control.
        If Not mblnActionQuery Then
            '+ Se obtiene el Alias que corresponde al botón.
            If Trim(Alias_Renamed) = String.Empty Then
                Alias_Renamed = HttpContext.GetGlobalResourceObject("BackOfficeResource", "DeleteTitle")
            End If
            varAux = AnimatedButtonControl(Name, "/VTimeNet/Images/btnDeleteOff.png", Alias_Renamed, , HRefScript, Disabled, TabIndex)
        End If
        Return varAux
    End Function

    '% DIVControl: Devuelve la estructura para una etiqueta DIV.
    Public Function DIVControl(ByVal FieldName As String, Optional ByVal isInTable As Boolean = False, Optional ByVal DefValue As String = "") As Object
        DIVControl = ""
        If isInTable Then DIVControl = DIVControl & "<TD>"
        DIVControl = DIVControl & "<DIV ID=" & FieldName & " CLASS=Field>" & DefValue & "</DIV>"
        If isInTable Then DIVControl = DIVControl & "</TD>"
    End Function

    '%PossiblesValues. Esta función se encarga de construir el código HTML
    '%para la construcción de un combo o valores posibles.
    '
    '+ DISABLED...
    '+ La propiedad Disabled sólo es aplicable cuando se trata de un combo convencional.
    '
    '+ Cuando se ejecuta una consulta el control se guía por la variable "mblnActionQuery"
    '+   mas NO por el parámetro Disabled para construit el control.
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function PossiblesValues(ByVal FieldName As String, ByVal TableName As String, ByVal ValuesType As eValuesType, Optional ByVal DefValue As String = "", Optional ByVal NeedParam As Boolean = False, Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal ComboSize As Short = 1, Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal MaxLength As Short = 5, Optional ByVal Alias_Renamed As String = "", Optional ByVal CodeType As eTypeCode = eTypeCode.eNumeric, Optional ByVal TabIndex As Short = 0, Optional ByVal ShowDescript As Boolean = True, Optional ByVal bAllowInvalid As Boolean = False, Optional ByVal Descript As String = "", Optional ByVal NotCache As Boolean = False, Optional ByVal sKeyField As String = "") As String
        Dim lstrToolTip As String
        Dim lobjParam As Parameter
        Dim lintIndex As Short
        Dim lobjTables As Tables = New Tables
        Dim lblnFindData As Boolean
        Dim lstrValue As String = ""
        Dim lblnTable As Boolean
        Dim lstrFilename As String = ""
        Dim lstrBuffer As String = ""

#If Log Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Log did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Control|PossiblesValues|" & TableName, sSessionID
#End If

        mblnGridField = GridField
        '+ComboSize se utiliza para definir el ancho del combo
        If ComboSize <= 0 Then
            ComboSize = 1
        End If

        lblnTable = (UCase(Left(TableName, 5)) = "TABLE")
        If lblnTable Then
            If Not IsNumeric(Mid(TableName, 6)) Then
                lblnTable = False
            End If
        End If
        sDescript = String.Empty
        If mblnCacheEnabled And Not NotCache And lblnTable And ValuesType = eValuesType.clngComboType Then

            lstrFilename = mstrCachePath & "\Tables\" & TableName & "_" & sCodisplPage & "_" & FieldName
            If Disabled Then
                lstrFilename = lstrFilename & "_Disable"
            Else
                lstrFilename = lstrFilename & "_Enable"
            End If
            If mblnActionQuery Or GridField Then
                lstrFilename = lstrFilename & "_Read"
            Else
                lstrFilename = lstrFilename & "_Edit"
            End If

            If meTypeList <> ecbeTypeList.none Then
                If meTypeList = ecbeTypeList.Inclution Then
                    lstrFilename = lstrFilename & "_Inc"
                Else
                    lstrFilename = lstrFilename & "_Exc"
                End If

                lstrFilename = lstrFilename & Replace(mstrList, ",", String.Empty)
            End If

            lstrFilename = lstrFilename & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".htm"

            lstrBuffer = eRemoteDB.FileSupport.LoadFileToText(lstrFilename)
            If lstrBuffer <> String.Empty Then
                mstrDefValue = Trim(String.Empty & DefValue)
                If mstrDefValue <> String.Empty Then
                    If mblnActionQuery Or GridField Then
                        If Descript = String.Empty Then
                            If DefValue <> CStr(numNull) Then
                                Descript = getCacheDescript(FieldName, TableName, DefValue)
                                If Descript = String.Empty Then
                                    lobjTables = New Tables
                                    With lobjTables
                                        If NeedParam Then
                                            .Parameters = mParameters
                                        End If
                                        If .reaTable(TableName, DefValue, sKeyField) Then
                                            Descript = Trim(.Fields(.DescriptField))
                                            .closeTable()
                                        End If
                                    End With
                                    'UPGRADE_NOTE: Object lobjTables may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                                    lobjTables = Nothing
                                End If
                            Else
                                Descript = String.Empty
                            End If
                        Else
                            Descript = HTMLEncode(Descript)
                        End If


                        If HRefUrl <> String.Empty Then
                            PossiblesValues = Replace(lstrBuffer, "<REF>", "<A HREF=""" & HRefUrl & """>" & Descript & "</A>")
                        ElseIf HRefScript <> String.Empty And Not mblnActionQuery Then
                            PossiblesValues = Replace(lstrBuffer, "<REF>", "<A HREF=""JAVASCRIPT:" & HRefScript & """>" & Descript & "</A>")
                        Else
                            PossiblesValues = Replace(lstrBuffer, "<REF>", Descript)
                        End If


                    Else
                        If mstrDefValue = String.Empty Then
                            'If CodeType = eTypeCode.eNumeric Then
                            mstrDefValue = "0"
                            'Else
                            '   mstrDefValue = CStr(eRemoteDB.Constants.strNull)
                            'End If
                        End If
                        mstrDefValue = "VALUE=""" & mstrDefValue & """"
                        PossiblesValues = Replace(lstrBuffer, mstrDefValue, mstrDefValue & " SELECTED")
                    End If
                Else
                    mstrDefValue = "VALUE=""0"""
                    PossiblesValues = Replace(lstrBuffer, mstrDefValue, mstrDefValue & " SELECTED")
                End If

                'UPGRADE_NOTE: Object Parameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                Parameters = Nothing
                mstrList = String.Empty
                meTypeList = ecbeTypeList.none

#If Log Then
				'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Log did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
				eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Control|PossiblesValues|" & TableName, sSessionID
#End If
                Exit Function
            End If
        End If

        mstrCombHtm = Space(1024)
        If TableName <> String.Empty Then
            If ValuesType = eValuesType.clngComboType Then
                mstrDefValue = Trim(String.Empty & DefValue)
                If mstrDefValue = String.Empty Then
                    'If CodeType = eTypeCode.eNumeric Then
                    mstrDefValue = "0"
                    'Else
                    'mstrDefValue = CStr(eRemoteDB.Constants.strNull)
                    'End If
                End If
                If Not mblnActionQuery And Not mblnGridField Then
                    'mstrCombHtm = "<SELECT SIZE=""" & ComboSize & """ NAME=""" & FieldName & """" & " TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """"
                    mstrCombHtm = "<SELECT SIZE=""" & ComboSize & """ NAME=""" & FieldName & """ ID=""" & FieldName & """" & " TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """"
                    If Disabled Then
                        mstrCombHtm = DisabledControl(mstrCombHtm)
                    End If
                    If OnChange = String.Empty OrElse OnChange = String.Empty Then
                        mstrCombHtm = mstrCombHtm & ">"
                    Else
                        mstrCombHtm = mstrCombHtm & " ONCHANGE='" & OnChange & "'>"
                    End If
                Else
                    mstrCombHtm = "<LABEL TITLE=""" & Alias_Renamed & """ CLASS=""FIELD"">"
                    lstrBuffer = mstrCombHtm & "<REF>"
                    If HRefUrl <> String.Empty Then
                        mstrCombHtm = mstrCombHtm & "<A HREF=""" & HRefUrl & """>"
                    Else
                        '+ Se le anexa el ancla siempre y cuando NO se este efectuando una consulta.
                        If HRefScript <> String.Empty And Not mblnActionQuery Then
                            mstrCombHtm = mstrCombHtm & "<A HREF=""JAVASCRIPT:" & HRefScript & """>"
                        End If
                    End If
                End If
                '+Se carga el valor en blanco del combo
                If mblnBlank Then
                    If CodeType = eTypeCode.eNumeric Then
                        Call ConstructValue(0, mstrBlankDesc)
                    Else
                        'Call ConstructValue(eRemoteDB.Constants.strNull, mstrBlankDesc)
                        Call ConstructValue("0", mstrBlankDesc)
                    End If
                End If
                If Descript = String.Empty Then
                    '+ Esto es para el caso de que el valor sea numnull y sea un grid; evitando que se haga una lectura para buscar la descripción.
                    If mstrDefValue = CStr(numNull) And mblnGridField Then
                    Else
                        Call LoadDataTable(TableName, NeedParam)
                    End If
                Else
                    Descript = HTMLEncode(Descript)
                    mstrCombHtm = mstrCombHtm & Descript
                End If
                If Not mblnActionQuery And Not mblnGridField Then
                    mstrCombHtm = mstrCombHtm & "</SELECT>"
                    lstrBuffer = lstrBuffer & "</SELECT>"
                Else
                    If HRefScript <> String.Empty Or HRefUrl <> String.Empty Then
                        mstrCombHtm = mstrCombHtm & "</A>"
                        lstrBuffer = lstrBuffer & "</A>"
                    End If
                    mstrCombHtm = mstrCombHtm & "</LABEL>"
                    lstrBuffer = lstrBuffer & "</LABEL>"
                End If

                If lstrFilename <> String.Empty Then
                    If mblnActionQuery Or mblnGridField Then
                        lstrBuffer = Replace(lstrBuffer, " SELECTED", String.Empty)
                    Else
                        lstrBuffer = Replace(mstrCombHtm, " SELECTED", String.Empty)
                    End If
                    If mblnCreateCache Then
                        eRemoteDB.FileSupport.SaveBufferToFile(lstrFilename, lstrBuffer)
                    End If
                End If
            Else
                lstrToolTip = HttpContext.GetGlobalResourceObject("BackOfficeResource", "PossibleValueTitle")
                If lstrToolTip = String.Empty Then
                    lstrToolTip = Alias_Renamed
                Else
                    lstrToolTip = Alias_Renamed & " (" & lstrToolTip & ")"
                End If

                '+ Si se indico un valor por defecto, se lee la tabla para obtener la descripción del valor
                If Descript = String.Empty Then
                    sDescript = String.Empty
                    If DefValue <> String.Empty And DefValue <> "0" And DefValue <> CStr(numNull) And ShowDescript Then
                        lobjTables = New Tables
                        If NeedParam Then
                            lobjTables.Parameters = mParameters
                        End If
                        If lobjTables.reaTable(TableName, DefValue, sKeyField) Then
                            If Not mblnActionQuery Then
                                sDescript = "&nbsp;&nbsp;"
                            End If
                            sDescript = sDescript & Trim(lobjTables.Fields(lobjTables.DescriptField))
                            lblnFindData = True
                        End If
                    End If
                Else
                    If Not mblnActionQuery Then
                        sDescript = "&nbsp;&nbsp;"
                    End If
                    sDescript = sDescript & Trim(Descript)
                End If

                If Not mblnActionQuery Then
                    mstrCombHtm = "<TABLE CELLPADING=0 CELLSPACING=0 BORDER=0>" & C_Separator & "<TR>" & C_Separator & "<TD>" & C_Separator
                    If CodeType = eTypeCode.eNumeric Then
                        mstrCombHtm = mstrCombHtm & NumericControl(FieldName, MaxLength, DefValue, , Alias_Renamed, False, 0, GridField, HRefUrl, HRefScript, "ShowValues(this,true," & IIf(ShowDescript, "true", "false") & "," & IIf(bAllowInvalid, "true", "false") & ");" & OnChange, Disabled, TabIndex)
                    Else
                        mstrCombHtm = mstrCombHtm & TextControl(FieldName, MaxLength, DefValue, False, Alias_Renamed, GridField, HRefUrl, HRefScript, "ShowValues(this,true," & IIf(ShowDescript, "true", "false") & "," & IIf(bAllowInvalid, "true", "false") & ");" & OnChange, Disabled, TabIndex)
                    End If

                    mstrCombHtm = mstrCombHtm & AnimatedButtonControl("btn" & FieldName, "/VTimeNet/images/btn_ValuesOff.png", lstrToolTip, , "ShowValues(document.forms[" & nParentForm & "].elements['" & FieldName & "'],false," & IIf(ShowDescript, "true", "false") & ")", Disabled, TabIndex) & "</TD>" & C_Separator & "<TD><DIV ID=""" & FieldName & "Desc" & """style=""position:relative;"" CLASS=Field>" & sDescript & "</DIV></TD>" & C_Separator & "</TR>" & C_Separator & "</TABLE>" & C_Separator & "<SCRIPT>document.forms[" & nParentForm & "]." & Trim(FieldName) & ".CanShowValues=true</SCRIPT>" & C_Separator
                Else
                    mstrCombHtm = "<TABLE CELLPADING=0 CELLSPACING=0 BORDER=0>" & C_Separator & "<TR>" & C_Separator & "<TD>" & C_Separator & NumericControl(FieldName, MaxLength, DefValue, , , False, 0, GridField, HRefUrl, HRefScript, "ShowValues(this,true);" & OnChange, , TabIndex) & "</TD>" & C_Separator & "<TD><DIV ID=""" & FieldName & "Desc" & """style=""position:relative;"" CLASS=Field>" & sDescript & "</DIV></TD>" & C_Separator & "</TR>" & C_Separator & "</TABLE>" & C_Separator
                End If

                '+ Se crean los campos ocultos en la página, en caso que se retornen  más de dos parámteros del Tab_table
                If Parameters.Count_ReturnValue > 0 Then
                    For lintIndex = 1 To Parameters.Count_ReturnValue
                        lobjParam = Parameters.Item_ReturnValue(lintIndex)
                        If lobjParam.CreateColumn Then
                            If lblnFindData Then
                                lstrValue = lobjTables.Fields(lobjParam.Name)
                            End If
                            mstrCombHtm = mstrCombHtm & HiddenControl(FieldName & "_" & lobjParam.Name, lstrValue)
                        End If
                    Next lintIndex
                    lintIndex = 0
                End If

                If Not mblnActionQuery Then
                    mstrCombHtm = mstrCombHtm & "<SCRIPT>" & C_Separator & "var Parameters_" & FieldName & "= new Object;" & C_Separator

                    If NeedParam Then
                        For Each lobjParam In mParameters
                            lintIndex = lintIndex + 1
                            With lobjParam
                                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                mstrCombHtm = mstrCombHtm & " var " & FieldName & lintIndex & "= new Object;" & C_Separator & FieldName & lintIndex & ".sName='" & .Name & "';" & C_Separator & FieldName & lintIndex & ".sValue='" & IIf(IsDBNull(.Value), "VT_EMPTY", .Value) & "';" & C_Separator & FieldName & lintIndex & ".sDirection='" & .Direction & "';" & C_Separator & FieldName & lintIndex & ".sParType='" & .ParType & "';" & C_Separator & FieldName & lintIndex & ".sSize='" & .Size & "';" & C_Separator & FieldName & lintIndex & ".sNumericScale='" & .NumericScale & "';" & C_Separator & FieldName & lintIndex & ".sPrecision='" & .Precision & "';" & C_Separator & FieldName & lintIndex & ".sAttributes='" & .Attributes & "';" & C_Separator & "Parameters_" & FieldName & ".Param" & lintIndex & "=" & FieldName & lintIndex & ";" & C_Separator
                            End With
                        Next lobjParam
                    End If

                    mstrCombHtm = mstrCombHtm & "var RParameters_" & FieldName & "= new Object;" & C_Separator

                    If Parameters.Count_ReturnValue > 0 Then
                        For lintIndex = 1 To Parameters.Count_ReturnValue
                            mstrCombHtm = mstrCombHtm & " var R" & FieldName & lintIndex & "= new Object;" & C_Separator
                            With Parameters.Item_ReturnValue(lintIndex)
                                mstrCombHtm = mstrCombHtm & "R" & FieldName & lintIndex & ".Name='" & .Name & "';" & C_Separator & "R" & FieldName & lintIndex & ".Visible='" & IIf(.VisibleColumn, "True", "False") & "';" & C_Separator & "R" & FieldName & lintIndex & ".Title='" & .TitleColumn & "';" & C_Separator & "R" & FieldName & lintIndex & ".Create='" & IIf(.CreateColumn, "True", "False") & "';" & C_Separator & "RParameters_" & FieldName & ".Param" & lintIndex & "=R" & FieldName & lintIndex & ";" & C_Separator
                            End With
                        Next lintIndex
                    End If

                    '+ Se asignan las propiedades del control, para ser evaluadas cuando se cargue la ventana de valores posibles
                    'mstrCombHtm = mstrCombHtm & "document.forms(0).elements('" & FieldName & "').TypeList='" & TypeList & "';" & C_Separator & "document.forms(0).elements('" & FieldName & "').List='" & List & "';" & C_Separator & "document.forms(0).elements('" & FieldName & "').TypeOrder='" & TypeOrder & "';" & C_Separator
                    mstrCombHtm = mstrCombHtm & "document.forms[0].elements['" & FieldName & "'].TypeList='" & TypeList & "';" & C_Separator & "document.forms[0].elements['" & FieldName & "'].List='" & List & "';" & C_Separator & "document.forms[0].elements['" & FieldName & "'].TypeOrder='" & TypeOrder & "';" & C_Separator

                    'mstrCombHtm = mstrCombHtm & "RParameters_" & FieldName & ".nCount=" & Parameters.Count_ReturnValue & ";" & C_Separator & "document.forms(0).elements('" & FieldName & "').RParameters =" & "RParameters_" & FieldName & ";" & C_Separator & "Parameters_" & FieldName & ".nCount=" & Parameters.Count & ";" & C_Separator & "document.forms(0).elements('" & FieldName & "').Parameters =" & "Parameters_" & FieldName & ";" & C_Separator & "document.forms(0).elements('" & FieldName & "').sTabName='" & TableName & "';" & C_Separator & "SetParameters(document.forms(0).elements('" & FieldName & "'));" & C_Separator & "</SCRIPT>" & C_Separator
                    mstrCombHtm = mstrCombHtm & "RParameters_" & FieldName & ".nCount=" & Parameters.Count_ReturnValue & ";" & C_Separator & "document.forms[0].elements['" & FieldName & "'].RParameters =" & "RParameters_" & FieldName & ";" & C_Separator & "Parameters_" & FieldName & ".nCount=" & Parameters.Count & ";" & C_Separator & "document.forms[0].elements['" & FieldName & "'].Parameters =" & "Parameters_" & FieldName & ";" & C_Separator & "document.forms[0].elements['" & FieldName & "'].sTabName='" & TableName & "';" & C_Separator & "SetParameters(document.forms[0].elements['" & FieldName & "']);" & C_Separator & "</SCRIPT>" & C_Separator
                End If
            End If
            PossiblesValues = mstrCombHtm
        End If
        'UPGRADE_NOTE: Object Parameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Parameters = Nothing
        mstrList = String.Empty
        meTypeList = ecbeTypeList.none

        mblnBlank = True
#If Log Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Log did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Control|PossiblesValues|" & TableName, sSessionID
#End If
    End Function

    '%ActionQuery: Propiedad para validar si las transacciones estan en modo consulta

    '%ActionQuery: Propiedad para validar si las transacciones estan en modo consulta
    Public Property ActionQuery() As Boolean
        Get
            ActionQuery = mblnActionQuery
        End Get
        Set(ByVal Value As Boolean)
            Dim lclsASPSupport As eRemoteDB.ASPSupport
            lclsASPSupport = New eRemoteDB.ASPSupport
            If lclsASPSupport.GetASPRequestValue("nMainAction") = 401.ToString() Then
                mblnActionQuery = True
            Else
                mblnActionQuery = Value
            End If
            'UPGRADE_NOTE: Object lclsASPSupport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsASPSupport = Nothing
        End Set
    End Property

    '%TypeList: Propiedad para indicar el tipo de lista (1-Inclusion, 2-Exclusion)

    '%TypeList: Propiedad para indicar el tipo de lista (1-Inclusion, 2-Exclusion)
    Public Property TypeList() As ecbeTypeList
        Get
            TypeList = meTypeList
        End Get
        Set(ByVal Value As ecbeTypeList)
            meTypeList = Value
        End Set
    End Property

    '%List: Propiedad para indicar la lista de Inclusion o Exclusion, los elementos se separan
    '*      coma

    '%List: Propiedad para indicar la lista de Inclusion o Exclusion, los elementos se separan
    '*      coma
    Public Property List() As String
        Get
            List = mstrList
        End Get
        Set(ByVal Value As String)
            mstrList = Value
            '    PropertyChanged "List"
        End Set
    End Property

    '%BlankPosition: Propiedad para indicar si el combo tiene la posición en blanco(0)

    '%BlankPosition: Propiedad para indicar si el combo tiene la posición en blanco(0)
    Public Property BlankPosition() As Boolean
        Get
            BlankPosition = mblnBlank
        End Get
        Set(ByVal Value As Boolean)
            mblnBlank = Value
        End Set
    End Property

    '%CodeValue: Valor actual del combo o valores posibles
    Public ReadOnly Property CodeValue() As Object
        Get
            CodeValue = mvntCodeValue
        End Get
    End Property

    '%Parameters: Coleccion de parametros de los valores posibles

    '%Parameters: Coleccion de parametros de los valores posibles
    Public Property Parameters() As Parameters
        Get
            If mParameters Is Nothing Then
                mParameters = New Parameters
            End If

            Parameters = mParameters
        End Get
        Set(ByVal Value As Parameters)
            mParameters = Value
        End Set
    End Property

    '% Property Get ShowWindowsName: Esta propiedad devuelve el código HTML que muestra el título
    '%                               de la ventana.
    Public ReadOnly Property WindowsTitle(ByVal sCodispl As String, Optional ByVal sDescript As String = "") As String
        Get
            Dim lclsQuery As eRemoteDB.Query

            '#If LOG Then
            '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|WindowsTitle|" & sCodispl, sSessionID
            '#End If

            If sCodispl <> mstrCodispl Then
                If sDescript = String.Empty Then
                    lclsQuery = New eRemoteDB.Query
                    With lclsQuery
                        If .OpenQuery("Windows", "sDescript", "sCodispl ='" & sCodispl & "'") Then
                            mstrDescriptWindows = .FieldToClass("sDescript")
                            mstrCodispl = sCodispl

                            .CloseQuery()
                        End If
                    End With
                    'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsQuery = Nothing
                Else
                    mstrCodispl = sCodispl
                    mstrDescriptWindows = HTMLDecode(sDescript)
                End If
            End If
            WindowsTitle = "<TITLE>" & mstrDescriptWindows & "</TITLE>"

            '#If LOG Then
            '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|WindowsTitle|" & sCodispl, sSessionID
            '#End If
        End Get
    End Property

    '%BeginPageLink:Esta funcion se encarga de colocar el link para el boton de ir al principio
    Public ReadOnly Property BeginPageLink() As String
        Get
            BeginPageLink = mstrBeginPageLink
        End Get
    End Property

    '%BeginPageButton: Esta funcion se encarga de colocar la figura de ir al inicio en la página
    Public ReadOnly Property BeginPageButton() As String
        Get
            BeginPageButton = "<P ALIGN=""Center"">" & AnimatedButtonControl("cmbStart", "/VTimeNet/images/btnBack.gif", "Ir al inicio de la página", , "window.scroll(0,0)") & "</P>"
        End Get
    End Property

    '% TypeOrder: indica el orden en que se van a cargar los valores de los combos

    '% TypeOrder: indica el orden en que se van a cargar los valores de los combos
    Public Property TypeOrder() As ecbeOrder
        Get
            TypeOrder = meOrder
        End Get
        Set(ByVal Value As ecbeOrder)
            meOrder = Value
        End Set
    End Property

    '%BlankDescript. Esta propiedad se encarga de indicar la descripción de la posición en blanco
    '% de los combos

    Public Property BlankDescript() As String
        Get
            BlankDescript = mstrBlankDesc
        End Get
        Set(ByVal Value As String)
            mstrBlankDesc = Value
        End Set
    End Property

    '%LoadDataTable: Carga los valores de la tabla
    Private Sub LoadDataTable(ByVal Source As String, Optional ByVal NeedParameters As Boolean = False, Optional ByVal ToValid As Boolean = False)
        Dim recTable As Tables
        Dim lintIndex As Integer

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|LoadDataTable|" & Source, sSessionID
        '#End If

        If Source <> String.Empty Then
            '+Cambio TABLAS
            recTable = New Tables
            recTable.ActionQuery = ActionQuery
            If NeedParameters Then
                If mParameters.Count = 0 Then
                    Exit Sub
                End If
                recTable.Parameters = mParameters
            End If
            Count = 0
            recTable.TypeOrder = TypeOrder
            If recTable.reaTable(Source) Then
                If Not mParameters Is Nothing Then
                    If mParameters.Count_ReturnValue > 0 Then
                        For lintIndex = 1 To mParameters.Count_ReturnValue
                            mParameters.Item_ReturnValue(lintIndex).Value = recTable.Fields(mParameters.Item_ReturnValue(lintIndex).Name)
                        Next lintIndex
                    End If
                End If
                Call Load_Values(recTable, ToValid)
                mblnCreateCache = True
            Else
                mblnCreateCache = False
            End If

            'UPGRADE_NOTE: Object recTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            recTable = Nothing
            'UPGRADE_NOTE: Object mParameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            mParameters = Nothing
        End If
        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|LoadDataTable|" & Source, sSessionID
        '#End If
    End Sub

    '%Load_Values: Carga los valores de un combo o un valores posibles
    Private Sub Load_Values(ByVal recTab As Tables, Optional ByVal ToValid As Boolean = False)
        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|Load_Values|", sSessionID
        '#End If
        If TypeList <> ecbeTypeList.none Then
            Call IncExcLoad(recTab, ToValid)
        Else
            Call NormalLoad(recTab, ToValid)
        End If
        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|Load_Values|", sSessionID
        '#End If
    End Sub

    '%IncExcLoad: Carga los valores de un combo o un valores posibles cuando
    '%             se incluyen o excluyen elementos
    Private Sub IncExcLoad(ByVal rec As Tables, Optional ByVal ToValid As Boolean = False)
        Dim blnReady As Boolean
        List = "*" & List & "*"
        With rec
            Do While Not .EOF And mstrDefValue <> String.Empty

                blnReady = True
                If TypeList = ecbeTypeList.Exclution Then
                    blnReady = Not FindValue(rec)
                Else
                    blnReady = FindValue(rec)
                End If
                If blnReady Then
                    If Not ToValid Then
                        Call ConstructValue(.Fields(.KeyField), .Fields(.DescriptField))
                        Count = Count + 1
                    Else
                        If CStr(String.Empty & .Fields(.KeyField)) = mstrDefValue Then
                            mblnIsValid = True
                            Exit Do
                        End If
                    End If
                End If
                .NextRecord()
            Loop
            .closeTable()
        End With
    End Sub

    '%NormalLoad: Carga los valores de un combo o un valores posibles cuando
    '%             la carga es normal, se incluyen todos los elementos
    Private Sub NormalLoad(ByVal rec As Tables, Optional ByVal ToValid As Boolean = False)
        Dim lstrDescript As String
        With rec
            Do While Not .EOF And mstrDefValue <> String.Empty
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                If IsDBNull(.Fields(.DescriptField)) Then
                    lstrDescript = String.Empty
                Else
                    lstrDescript = .Fields(.DescriptField)
                End If
                If Not ToValid Then
                    Call ConstructValue(.Fields(.KeyField), lstrDescript)
                    Count = Count + 1
                Else
                    If CStr(String.Empty & .Fields(.KeyField)) = mstrDefValue Then
                        mblnIsValid = True
                        Exit Do
                    End If
                End If
                .NextRecord()
            Loop
            .closeTable()
        End With
    End Sub

    '%FindValue: Busca si un elemento ya se encuentra en la coleccion de tablas
    Private Function FindValue(ByVal rec As Tables) As Boolean
        FindValue = False
        With rec
            If InStr(1, List, "*" & .Fields(.KeyField) & ",") Or InStr(1, List, "," & .Fields(.KeyField) & "*") Or InStr(1, List, "*" & .Fields(.KeyField) & "*") Or InStr(1, List, "," & .Fields(.KeyField) & ",") Then
                FindValue = True
            End If

            '+ Se realiza el llamado a la función Fields para que el valor de la descripción
            '+ se incluya en la colección.
            Call .Fields(.DescriptField)
        End With
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
        HTMLEncode = Replace(HTMLEncode, "Ãº", "&uacute;")
        HTMLEncode = Replace(HTMLEncode, """", "&quot;")
        HTMLEncode = Replace(HTMLEncode, vbCrLf, "&#13;")
        'HTMLEncode = Replace(HTMLEncode, " ", "&nbsp;")
        HTMLEncode = Replace(HTMLEncode, " ", "&#32;")
    End Function

    '%ConstructValue: Crea cada elemento del combo
    Private Sub ConstructValue(ByVal lvarValue As Object, ByVal lstrDescript As String)

        'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
        If IsDBNull(lvarValue) Then
            lvarValue = String.Empty
        End If

        If mblnActionQuery Or mblnGridField Then
            If Trim(lvarValue) = mstrDefValue Then
                sDescript = HTMLEncode(lstrDescript)
                mstrCombHtm = mstrCombHtm & sDescript
            End If
        Else
            mstrCombHtm = mstrCombHtm & "<OPTION VALUE=""" & Trim(lvarValue) & """" & IIf(Trim(lvarValue) = mstrDefValue, " SELECTED", "") & ">" & HTMLEncode(lstrDescript) & "</OPTION>"
        End If
        If Trim(lvarValue) = mstrDefValue Then
            mvntCodeValue = Trim(lvarValue)
        Else
            If Count = 0 Then
                If Not BlankPosition And mstrDefValue = "0" Then
                    mvntCodeValue = Trim(lvarValue)
                End If
            End If
        End If
    End Sub

    '%DateControl: Esta funcion se encarga de devolver el código html para la colocación
    '%             de un campo fecha.
    '-++++++++++ : Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function DateControl(ByVal FieldName As String, Optional ByVal DefValue As String = "", Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        Dim lintTabIndexButton As Short

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Control|DateControl|" & FieldName, sSessionID
        '#End If

        lintTabIndexButton = 0

        DefValue = TypeToString(DefValue, eTypeData.etdDate)

        If ActionQuery Or GridField Then
            DateControl = insQueryField(eTypeField.clngDateValue, DefValue, Alias_Renamed, , HRefUrl, HRefScript)
        Else
            'DateControl = "<INPUT TYPE=""TEXT"" name=""" & FieldName & """ SIZE=""10"" VALUE=""" & DefValue & """ MAXLENGTH=""10""" & " TITLE=""" & Alias_Renamed & """" & " TABINDEX=" & TabIndex & " ONBLUR='" & FieldName & ".IsReq=" & IIf(isRequired, "1", "0") & ";" & FieldName & ".Alias=""" & Trim(Alias_Renamed) & """;" & "if(ValDate(" & FieldName & " , """ & msUserDateFormat & """ , """ & msUserDateSeparator & """))"
            DateControl = "<INPUT TYPE=""TEXT"" id=""" & FieldName & """  name=""" & FieldName & """ SIZE=""10"" VALUE=""" & DefValue & """ MAXLENGTH=""10""" & " TITLE=""" & Alias_Renamed & """" & " TABINDEX=" & TabIndex & " onBlurCode='this.IsReq=" & IIf(isRequired, "1", "0") & ";this.Alias=""" & Trim(Alias_Renamed) & """;" & "if(ValDate(this, """ & msUserDateFormat & """ , """ & msUserDateSeparator & """))"
            If OnChange = String.Empty OrElse OnChange = String.Empty Then
                DateControl = DateControl & "{}'"
            Else
                DateControl = DateControl & "{" & OnChange & ";}'"
            End If
            If Disabled Then
                DateControl = DisabledControl(DateControl)
            End If
            DateControl = DateControl & ">"
            '+Se coloca el boton para los valores posibles
            If TabIndex > 0 Then
                lintTabIndexButton = TabIndex + 1
            End If
            DateControl = Trim(DateControl) & AnimatedButtonControl("btn_" & FieldName, "/VTimeNet/images/btn_CalendarOff.png", Alias_Renamed, , "OpenCalendar(document.forms[" & nParentForm & "].elements['" & FieldName & "'], '" & nParentForm & "', '" & msUserDateFormat & "' , '" & msUserDateSeparator & "')", Disabled, TabIndex)
            DateControl = DateControl & ValText()
        End If
        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Control|DateControl|" & FieldName, sSessionID
        '#End If
    End Function

    '%NumericControl: Esta funcion se encarga de devolver el código html para la colocación
    '%                de un campo numerico
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function NumericControl(ByVal FieldName As String, ByVal Length As Short, Optional ByVal DefValue As String = "", Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal ShowThousand As Boolean = False, Optional ByVal DecimalPlaces As Short = 0, Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal Formated As Boolean = True, Optional ByVal bAllowNegativ As Boolean = False) As String
        Dim lstrOnChange As String
        Dim lintLength As Short
        Dim lintPoint As Short

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Control|NumericControl|" & FieldName, sSessionID
        '#End If

        DefValue = TypeToString(DefValue, eTypeData.etdDouble, ShowThousand, DecimalPlaces)

        If DecimalPlaces > 0 Then
            lintLength = Length + IIf(bAllowNegativ, 2, 1)
        Else
            lintLength = Length
        End If

        If ShowThousand Then
            lintPoint = Int(Length / 3)
            lintLength = lintLength + lintPoint
        End If

        If ActionQuery Or GridField Then
            NumericControl = insQueryField(eTypeField.clngNumericValue, DefValue, Alias_Renamed, DecimalPlaces, HRefUrl, HRefScript, ShowThousand)
        Else
            'NumericControl = "<INPUT TYPE=""TEXT"" NAME=""" & FieldName & """ SIZE=""" & lintLength & """ MAXLENGTH=""" & lintLength & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " STYLE=""text-align:right"" VALUE=""" & IIf(Formated, insFormatField(DefValue, ShowThousand, eTypeField.clngNumericValue, DecimalPlaces), DefValue) & """"

            'lstrOnChange = "if(ValNumber(document.forms[0].elements['" & FieldName & "'],""" & msUserThousandSeparator & """,""" & msUserDecimalSeparator & """,""" & IIf(bAllowNegativ, "true", "false") & """," & DecimalPlaces & "))"
            'lstrOnChange = "if(ValNumber(this,""" & msUserThousandSeparator & """,""" & msUserDecimalSeparator & """,""" & IIf(bAllowNegativ, "true", "false") & """," & DecimalPlaces & "))"
            'lstrOnChange = "if(ValNumber(this,'" & msUserThousandSeparator & "','" & msUserDecimalSeparator & "','" & IIf(bAllowNegativ, "true", "false") & "'," & DecimalPlaces & "))"
            lstrOnChange = "if(ValNumber(this,""" & msUserThousandSeparator & """,""" & msUserDecimalSeparator & """,""" & IIf(bAllowNegativ, "true", "false") & """," & DecimalPlaces & "))"

            If OnChange = String.Empty OrElse OnChange = String.Empty Then
                lstrOnChange = lstrOnChange & "{}"
            Else
                'lstrOnChange = lstrOnChange & "{" & OnChange & ";}"

                If OnChange.Contains("setTimeout") Then
                    lstrOnChange = lstrOnChange & "{" & OnChange.Replace("""", "\'") & ";}"
                Else
                    lstrOnChange = lstrOnChange & "{" & OnChange.Replace("""", "'") & ";}"
                End If
            End If
            'NumericControl = "<INPUT TYPE=""TEXT"" NAME=""" & FieldName & """ SIZE=""" & lintLength & """ MAXLENGTH=""" & lintLength & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " STYLE=""text-align:right"" VALUE=""" & IIf(Formated, insFormatField(DefValue, ShowThousand, eTypeField.clngNumericValue, DecimalPlaces), DefValue) & """"
            NumericControl = "<INPUT TYPE=""TEXT""  id=""" & FieldName & """ NAME=""" & FieldName & """ SIZE=""" & lintLength & """ MAXLENGTH=""" & lintLength & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " STYLE=""text-align:right"" onBlurCode='" & lstrOnChange.Replace("'", "&#39;") & "' VALUE=""" & IIf(Formated, insFormatField(DefValue, ShowThousand, eTypeField.clngNumericValue, DecimalPlaces), DefValue) & """"

            If Disabled Then
                NumericControl = DisabledControl(NumericControl)
            End If

            NumericControl = NumericControl & ">" & ValText() & "<SCRIPT>self.document.forms[" & nParentForm & "]." & FieldName & ".HolePlace='0" & CStr(Length - DecimalPlaces) & "';" & " self.document.forms[" & nParentForm & "]." & FieldName & ".IsReq=" & IIf(isRequired, "1", "0") & ";" & " self.document.forms[" & nParentForm & "]." & FieldName & ".ShowThousand=" & IIf(ShowThousand, "1", "0") & ";" & " self.document.forms[" & nParentForm & "]." & FieldName & ".Alias=""" & Trim(Alias_Renamed) & """;" & " self.document.forms[" & nParentForm & "]." & FieldName & ".DecimalPlace=0" & CStr(DecimalPlaces) & ";</SCRIPT>" & "<SCRIPT LANGUAGE=javascript FOR=" & FieldName & " EVENT=onkeypress>if (window.event.keyCode==32)window.event.keyCode=8;</SCRIPT>"
            'NumericControl = NumericControl & " ONBLUR=""" & "self.document.forms[" & nParentForm & "]." & FieldName & ".HolePlace='0" & CStr(Length - DecimalPlaces) & "';" & " self.document.forms[" & nParentForm & "]." & FieldName & ".IsReq=" & IIf(isRequired, "1", "0") & ";" & " self.document.forms[" & nParentForm & "]." & FieldName & ".ShowThousand=" & IIf(ShowThousand, "1", "0") & ";" & " self.document.forms[" & nParentForm & "]." & FieldName & ".Alias='" & Trim(Alias_Renamed) & "';" & " self.document.forms[" & nParentForm & "]." & FieldName & ".DecimalPlace=0" & CStr(DecimalPlaces) & ";" & lstrOnChange & """"                
            'NumericControl = NumericControl & ">" & ValText() & "<SCRIPT LANGUAGE=javascript FOR=" & FieldName & " EVENT=onkeypress>if (window.event.keyCode==32)window.event.keyCode=8;</SCRIPT>"
            'NumericControl = NumericControl & ">" & ValText() & "<SCRIPT>self.document.forms[" & nParentForm & "]." & FieldName & ".HolePlace='0" & CStr(Length - DecimalPlaces) & "';" & " self.document.forms[" & nParentForm & "]." & FieldName & ".IsReq=" & IIf(isRequired, "1", "0") & ";" & " self.document.forms[" & nParentForm & "]." & FieldName & ".ShowThousand=" & IIf(ShowThousand, "1", "0") & ";" & " self.document.forms[" & nParentForm & "]." & FieldName & ".Alias=""" & Trim(Alias_Renamed) & """;" & " self.document.forms[" & nParentForm & "]." & FieldName & ".DecimalPlace=0" & CStr(DecimalPlaces) & ";</SCRIPT>" & "<SCRIPT LANGUAGE=javascript FOR=" & FieldName & " EVENT=onkeypress>if (window.event.keyCode==32)window.event.keyCode=8;</SCRIPT>" & "<SCRIPT LANGUAGE=javascript FOR=" & FieldName & " EVENT=onblur>" & lstrOnChange & "</SCRIPT>"
        End If

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Control|NumericControl|" & FieldName, sSessionID
        '#End If
    End Function

    '%TextControl: Esta funcion se encarga de devolver el código html para la colocación
    '%             de un campo texto
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function TextControl(ByVal FieldName As String, ByVal Length As Short, Optional ByVal DefValue As String = "", Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal MaxLength As Short = 0) As String

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Control|TextControl|" & FieldName, sSessionID
        '#End If

        'El parametro Length representa el tamano del campo, el parametro MaxLength representa el maximo de caracteres permitidos
        'Si el MaxLength es 0 entonces se asume como maximo lo indicado en el Length

        If MaxLength = 0 Then
            MaxLength = Length
        End If

        If ActionQuery Or GridField Then
            TextControl = insQueryField(eTypeField.clngTextValue, HTMLEncode(DefValue), Alias_Renamed, , HRefUrl, HRefScript)
        Else
            'TextControl = "<INPUT TYPE=""TEXT"" name=""" & FieldName & """ SIZE=""" & Length & """ MAXLENGTH=""" & MaxLength & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " VALUE=""" & HTMLEncode(DefValue) & """ ONBLUR='" & FieldName & ".IsReq=" & IIf(isRequired, "1", "0") & ";" & FieldName & ".Alias=""" & Trim(Alias_Renamed) & """;if(ValText(" & FieldName & ",""" & mstrList & """,""" & bNumericText & """))"
            TextControl = "<INPUT TYPE=""TEXT"" id=""" & FieldName & """ name=""" & FieldName & """ SIZE=""" & Length & """ MAXLENGTH=""" & Length & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " VALUE=""" & HTMLEncode(DefValue) & """ onBlurCode='this.IsReq=" & IIf(isRequired, "1", "0") & ";this.Alias=""" & Trim(Alias_Renamed) & """;if(ValText(this,""" & mstrList & """,""" & bNumericText & """))"
            If OnChange = String.Empty OrElse OnChange = String.Empty Then
                TextControl = TextControl & "{}'"
            Else
                TextControl = TextControl & "{" & OnChange & ";}'"
            End If
            If Disabled Then
                TextControl = DisabledControl(TextControl)
            End If
            TextControl = TextControl & ">" & ValText()
            mstrList = String.Empty
            bNumericText = False
        End If
        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Control|TextControl|" & FieldName, sSessionID
        '#End If
    End Function

    '% PasswordControl: devuelve la estructura en HTML para un control de texto de Password
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function PasswordControl(ByVal FieldName As String, ByVal Length As Short, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        If ActionQuery Or GridField Then
            PasswordControl = "<LABEL TITLE=""" & Alias_Renamed & """ CLASS=""FIELD"">" & New String("*", Len(DefValue)) & "</LABEL>"
        Else
            PasswordControl = "<INPUT id=""" & FieldName & """ TYPE=""PASSWORD"" name=""" & FieldName & """ SIZE=""" & Length & """ MAXLENGTH=""" & Length & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " VALUE=""" & HTMLEncode(DefValue) & """ onBlurCode='this.IsReq=" & IIf(isRequired, "1", "0") & ";this.Alias=""" & Trim(Alias_Renamed) & """;if(ValText(this))"
            'PasswordControl = "<INPUT TYPE=""PASSWORD"" name=""" & FieldName & """ SIZE=""" & Length & """ MAXLENGTH=""" & Length & """ TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """" & " VALUE=""" & HTMLEncode(DefValue) & """ ONBLUR='" & FieldName & ".IsReq=" & IIf(isRequired, "1", "0") & ";" & FieldName & ".Alias=""" & Trim(Alias_Renamed) & """;if(ValText(" & FieldName & "))"
            If OnChange = String.Empty OrElse OnChange = String.Empty Then
                PasswordControl = PasswordControl & "{}'"
            Else
                PasswordControl = PasswordControl & "{" & OnChange & ";}'"
            End If
            If Disabled Then
                PasswordControl = DisabledControl(PasswordControl)
            End If
            PasswordControl = PasswordControl & ">" & ValText()
        End If
    End Function

    '%TextAreaControl: Código HTML para generar un control de area de texto
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function TextAreaControl(ByVal FieldName As String, ByVal Rows As Short, ByVal Cols As Short, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal GridField As Boolean = False, Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal OnChange As String = "") As String
        If ActionQuery Or GridField Then
            TextAreaControl = "<P TITLE=""" & Alias_Renamed & """ CLASS=FIELD> " & DefValue & "</P>"
        Else
            TextAreaControl = "<TEXTAREA id=""" & FieldName & """ NAME=""" & FieldName & """ COLS=" & CStr(Cols) & " ROWS=" & Rows & " WRAP=VIRTUAL" & " TITLE=""" & Alias_Renamed & """" & " TABINDEX=" & TabIndex
            'TextAreaControl = "<TEXTAREA NAME=" & FieldName & " COLS=" & CStr(Cols) & " ROWS=" & Rows & " WRAP=VIRTUAL" & " TITLE=""" & Alias_Renamed & """" & " TABINDEX=" & TabIndex
            If Disabled Then
                TextAreaControl = DisabledControl(TextAreaControl)
            End If
            If OnChange <> String.Empty OrElse OnChange <> String.Empty Then
                'TextAreaControl = TextAreaControl & " ONBLUR='" & OnChange & "'"
                TextAreaControl = TextAreaControl & " onBlurCode='" & OnChange & "'"
            End If

            TextAreaControl = TextAreaControl & " style=""filter:alpha(opacity=" & Opacity & ")"""

            TextAreaControl = TextAreaControl & ">" & HTMLEncode(DefValue) & "</TEXTAREA>"
        End If
        Opacity = 100

    End Function

    '% ButtonControl: devuelve la estructura de un Botón de acción
    Public Function ButtonControl(ByVal FieldName As String, ByVal Value As String, Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        ButtonControl = "<INPUT TYPE=""BUTTON""" & " NAME=""" & FieldName & """" & " VALUE=""" & Value & """" & " TABINDEX=" & TabIndex

        If OnClick <> String.Empty Then
            ButtonControl = ButtonControl & " ONCLICK='" & OnClick & "'"
        End If

        If Disabled Then
            ButtonControl = DisabledControl(ButtonControl)
        End If

        ButtonControl = ButtonControl & ">"
    End Function

    '% ButtonControl: devuelve la estructura de un Botón de acción, para invocar la ventana "Acerca de"
    Public Function ButtonAbout(ByVal sCodispl As String, Optional ByVal sCodisp As String = "") As String
        Dim lobjQuery As eRemoteDB.Query

        If sCodisp = String.Empty Then
            lobjQuery = New eRemoteDB.Query
            With lobjQuery
                If .OpenQuery("Windows", "sCodisp", "sCodispl='" & sCodispl & "'") Then
                    sCodisp = .FieldToClass("sCodisp")
                End If
            End With
            'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjQuery = Nothing
        End If
        ButtonAbout = AnimatedButtonControl("btnAbout", "/VTimeNet/images/a209off.gif", HttpContext.GetGlobalResourceObject("BackOfficeResource", "AboutTitle"), , "insAbout('" & sCodispl & "','" & sCodisp & "')")
    End Function

    '% ButtonAcceptCancel: Retorna la estructura conjunta de un botón de Aceptar y un botón Cancelar.
    Public Function ButtonAcceptCancel(Optional ByVal OnClickAccept As String = "", Optional ByVal OnClickCancel As String = "window.close()", Optional ByVal IsSubmitControl As Boolean = True, Optional ByVal nColSpan As Short = 0, Optional ByVal nButtonsToShow As eButtonsToShow = eButtonsToShow.All, Optional ByVal TabIndex As Short = 0) As String
        Dim lstrAccept As String
        Dim lstrCancel As String
        Dim lstrOnclickAccept As String = ""
        Dim lstrScript As String = ""

        '+ Si un botón de tipo Submit necesita una instrucción particular sobre el evento OnClick, se construye la
        '+ misma de manera conjunta, siendo el submit el último comando a ejecutar.
        If IsSubmitControl Then
            'lstrScript = "<SCRIPT>" & vbCrLf & "function InsDoSubmit(){" & vbCrLf & "    var lstrDoSubmit = '1';" & vbCrLf & "    if (typeof(top) != 'undefined')" & vbCrLf & "        if (typeof(top.frames) != 'undefined')" & vbCrLf & "            if (typeof(top.frames.fraFolder) != 'undefined')" & vbCrLf & "                if (typeof(top.frames.fraFolder.mstrDoSubmit) != 'undefined')" & vbCrLf & "                    lstrDoSubmit = top.frames.fraFolder.mstrDoSubmit;" & vbCrLf & "    document.cmdAccept.disabled = true;" & vbCrLf & "    if (lstrDoSubmit == '1'){" & vbCrLf & "        if (typeof(top) != 'undefined')" & vbCrLf & "            if (typeof(top.frames) != 'undefined')" & vbCrLf & "                if (typeof(top.frames.fraSubmit) != 'undefined')" & vbCrLf & "                    self.document.forms(" & nParentForm & ").target = 'fraSubmit';" & vbCrLf & "        self.document.forms(" & nParentForm & ").submit();" & vbCrLf & "    }" & vbCrLf & "    else" & vbCrLf & "        setTimeout('InsDoSubmit()',50);" & vbCrLf & "}" & vbCrLf & "</SCRIPT>"
            lstrScript = "<SCRIPT>" & vbCrLf & "function InsDoSubmit(){" & vbCrLf & "    var lstrDoSubmit = '1';" & vbCrLf & "    if (typeof(top) != 'undefined')" & vbCrLf & "        if (typeof(top.frames) != 'undefined')" & vbCrLf & "            if (typeof(top.frames.fraFolder) != 'undefined')" & vbCrLf & "                if (typeof(top.frames.fraFolder.mstrDoSubmit) != 'undefined')" & vbCrLf & "                    lstrDoSubmit = top.frames.fraFolder.mstrDoSubmit;" & vbCrLf & "    document.cmdAccept.disabled = true;" & vbCrLf & "    if (lstrDoSubmit == '1'){" & vbCrLf & "        if (typeof(top) != 'undefined')" & vbCrLf & "            if (typeof(top.frames) != 'undefined')" & vbCrLf & "                if (typeof(top.frames.fraSubmit) != 'undefined')" & vbCrLf & "                    self.document.forms[" & nParentForm & "].target = 'fraSubmit';" & vbCrLf & "        self.document.forms[" & nParentForm & "].submit();" & vbCrLf & "    }" & vbCrLf & "    else" & vbCrLf & "        setTimeout('InsDoSubmit()',50);" & vbCrLf & "}" & vbCrLf & "</SCRIPT>"
            If OnClickAccept = String.Empty Then
                lstrOnclickAccept = "InsDoSubmit()"
            Else
                lstrOnclickAccept = OnClickAccept & ";InsDoSubmit()"
            End If
        End If

        If Not mblnActionQuery Then
            lstrAccept = HttpContext.GetGlobalResourceObject("BackOfficeResource", "AcceptInformationTitle")
            lstrCancel = HttpContext.GetGlobalResourceObject("BackOfficeResource", "CancelInformationTitle")

            '+ En caso de definir un nCOLSPAN se anexará a la estructura, las etiquetas <TD> para ingresarlo
            '+ dentro de un <TABLE>
            ButtonAcceptCancel = IIf(nColSpan > 0, "<TD COLSPAN=" & nColSpan & " ALIGN=""Right"">", "")

            Select Case nButtonsToShow
                Case eButtonsToShow.All
                    ButtonAcceptCancel = ButtonAcceptCancel & AnimatedButtonControl("cmdAccept", "/VTimeNet/Images/btnAcceptOff.png", lstrAccept, , IIf(IsSubmitControl, lstrOnclickAccept, OnClickAccept), , TabIndex) & "<SCRIPT LANGUAGE=javascript FOR=cmdAccept EVENT=onkeypress>if (window.event.keyCode==13)" & IIf(IsSubmitControl, lstrOnclickAccept, OnClickAccept) & ";</SCRIPT>" & "&nbsp;" & AnimatedButtonControl("cmdCancel", "/VTimeNet/Images/btnCancelOff.png", lstrCancel, , OnClickCancel, , TabIndex)
                Case eButtonsToShow.OnlyAccept
                    ButtonAcceptCancel = ButtonAcceptCancel & AnimatedButtonControl("cmdAccept", "/VTimeNet/Images/btnAcceptOff.png", lstrAccept, , IIf(IsSubmitControl, lstrOnclickAccept, OnClickAccept), , TabIndex) & "<SCRIPT LANGUAGE=javascript FOR=cmdAccept EVENT=onkeypress>if (window.event.keyCode==13)" & IIf(IsSubmitControl, lstrOnclickAccept, OnClickAccept) & ";</SCRIPT>"
                Case eButtonsToShow.OnlyCancel
                    ButtonAcceptCancel = ButtonAcceptCancel & AnimatedButtonControl("cmdCancel", "/VTimeNet/Images/btnCancelOff.png", lstrCancel, , OnClickCancel, , TabIndex)
            End Select

            ButtonAcceptCancel = lstrScript & ButtonAcceptCancel & IIf(nColSpan > 0, "</TD>", String.Empty)
        Else
            lstrCancel = HttpContext.GetGlobalResourceObject("BackOfficeResource", "CancelInformationTitle")

            '+ En caso de definir un nCOLSPAN se anexará a la estructura, las etiquetas <TD> para ingresarlo
            '+ dentro de un <TABLE>
            ButtonAcceptCancel = IIf(nColSpan > 0, "<TD COLSPAN=" & nColSpan & " ALIGN=""Right"">", "") & AnimatedButtonControl("cmdCancel", "/VTimeNet/Images/btnCancelOff.png", lstrCancel, , OnClickCancel, , TabIndex)
            ButtonAcceptCancel = lstrScript & ButtonAcceptCancel & IIf(nColSpan > 0, "</TD>", String.Empty)
        End If
    End Function

    '% ButtonBackNext: Retorna la estructura conjunta de un botón de "Back" y un botón "Next" para
    '%                 el desplazamiento entre registros.
    Public Function ButtonBackNext(Optional ByVal nColSpan As Short = 0, Optional ByVal isDisabledBack As Boolean = False, Optional ByVal isDisabledNext As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        Dim lstrNext As String
        Dim lstrPrevious As String
        Dim varAux As String = ""

        '+Se toma en cuenta si se está consultando, para no mostrar los registros

        If Not mblnActionQuery Then

            '+Se busca la descripción de los campos próximos y anteriores

            lstrPrevious = HttpContext.GetGlobalResourceObject("BackOfficeResource", "PreviousRecord")
            lstrNext = HttpContext.GetGlobalResourceObject("BackOfficeResource", "NextRecord")

            '+ En caso de definir un nCOLSPAN se anexará a la estructura, las etiquetas <TR> y <TD> para ingresarlo
            '+ dentro de un <TABLE>
            varAux = IIf(nColSpan > 0, "<TR><TD COLSPAN=" & nColSpan & " ALIGN=""Right"">", "") & AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", lstrPrevious, , "MoveRecord('Back')", isDisabledBack, TabIndex) & "&nbsp;" & AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", lstrNext, , "MoveRecord('Next')", isDisabledNext, TabIndex) & IIf(nColSpan > 0, "</TD></TR>", "")
        End If
        Return varAux
    End Function

    '% ClientControl: Devuelve la estructura para la búsqueda de los clientes.
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function ClientControl(ByVal FieldName As String, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal FieldClieName As String = "", Optional ByVal isDIVDefine As Boolean = False, Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal nTypeForm As eTypeClient = eTypeClient.SearchClient, Optional ByVal TabIndex As Short = 0, Optional ByVal CreateClient As Boolean = False, Optional ByVal isPopUp As Boolean = False, Optional ByVal sQueryString As String = "", Optional ByVal bAllowInvalid As Boolean = False, Optional ByVal Cliename As String = "", Optional ByVal Digit As String = "", Optional ByVal CustomPage As String = "", Optional ByVal bAllowInvalidFormat As Boolean = False) As Object
        Dim lstrOnChange As String
        Dim lstrOnchangeDigit As String
        Dim lobjClient As Object
        Dim lstrCliename As String
        Dim lstrDigit As String

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Control|ClientControl|" & FieldName, sSessionID
        '#End If

        sDigit = String.Empty
        lstrCliename = String.Empty
        sDescript = String.Empty

        '+ Crea el nombre del DIV para desplegar el nombre del cliente.
        If FieldClieName = String.Empty Then
            FieldClieName = FieldName & "_Name"
        End If

        '+ Arma la estructura del evento OnChange del botón.
        lstrOnChange = "if ($(""#" & FieldName & "_Old"").val() != $(""#" & FieldName & """).val()" & "){ValidateClient(this,""" & FieldClieName & """," & IIf(CreateClient, "true", "false") & "," & nTypeForm & ",""" & ClientRole & """," & TypeList & ",""" & sQueryString & """," & IIf(bAllowInvalid, "true", "false") & "," & IIf(bAllowInvalidFormat, "true", "false") & ");}"
        'lstrOnChange = "if (" & FieldName & "_Old.value != " & FieldName & ".value" & "){ValidateClient(this,""" & FieldClieName & """," & IIf(CreateClient, "true", "false") & "," & nTypeForm & ",""" & ClientRole & """," & TypeList & ",""" & sQueryString & """," & IIf(bAllowInvalid, "true", "false") & "," & IIf(bAllowInvalidFormat, "true", "false") & ");}"
        OnChange = Replace(Replace(OnChange, "'", """"), """", "\""")

        ClientControl = "<SCRIPT>var mintTypeForm=" & nTypeForm & ";</SCRIPT><TABLE CELLPADING=0 CELLSPACING=0 BORDER=0><TR><TD>" & TextControl(FieldName, 14, DefValue, isRequired, Alias_Renamed, GridField, HRefUrl, HRefScript, lstrOnChange, Disabled, TabIndex)

        If Not mblnActionQuery And Not GridField Then

            'lstrOnchangeDigit = "if (" & FieldName & "_Digit" & "_Old.value != " & FieldName & "_Digit.value" & "){ValidateDigit(this,self.document.forms[" & nParentForm & "]." & FieldName & ",false," & nTypeForm & ",""" & ClientRole & """," & TypeList & ",""" & sQueryString & "&sOnChange=" & OnChange & """," & IIf(bAllowInvalid, "true", "false") & "," & IIf(bAllowInvalidFormat, "true", "false") & ",""" & FieldClieName & """);}"
            lstrOnchangeDigit = "if ($(""#" & FieldName & "_Digit" & "_Old"").val() != $(""#" & FieldName & "_Digit"").val()" & "){ValidateDigit(this,self.document.forms[" & nParentForm & "]." & FieldName & ",false," & nTypeForm & ",""" & ClientRole & """," & TypeList & ",""" & sQueryString & "&sOnChange=" & OnChange & """," & IIf(bAllowInvalid, "true", "false") & "," & IIf(bAllowInvalidFormat, "true", "false") & ",""" & FieldClieName & """);}"
            lstrCliename = String.Empty
            lstrDigit = String.Empty

            If DefValue <> String.Empty Then
                If Cliename = String.Empty Then
                    lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
                    With lobjClient
                        If .Find(DefValue) Then
                            lstrCliename = .sCliename
                            lstrDigit = .sDigit
                        End If
                    End With
                    'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjClient = Nothing
                Else
                    lstrCliename = Cliename
                    lstrDigit = Digit
                End If
                sDescript = lstrCliename
            End If

            If CustomPage = String.Empty Then
                ClientControl = ClientControl & "<LABEL>-</LABEL>" & TextControl(FieldName & "_Digit", 1, lstrDigit, False, Alias_Renamed & "(Dígito)", GridField, , , lstrOnchangeDigit, Disabled, TabIndex) & AnimatedButtonControl("btn" & FieldName, "/VTimeNet/Images/btn_ValuesOff.png", Alias_Renamed, , "insShowClientQuery(""" & FieldName & """," & nTypeForm & ",""" & FieldClieName & """,""" & ClientRole & """," & TypeList & ",""" & sQueryString & "&sOnChange=" & OnChange & """," & IIf(bAllowInvalid, "true", "false") & ")", Disabled, TabIndex)

            Else
                ClientControl = ClientControl & "<LABEL>-</LABEL>" & TextControl(FieldName & "_Digit", 1, lstrDigit, False, Alias_Renamed & "(Dígito)", GridField, , , lstrOnchangeDigit, Disabled, TabIndex) & AnimatedButtonControl("btn" & FieldName, "/VTimeNet/Images/btn_ValuesOff.png", Alias_Renamed, , "insShowClientCustomPage(""" & CustomPage & """,""" & FieldName & """," & nTypeForm & ",""" & FieldClieName & """,""" & ClientRole & """," & TypeList & ",""" & sQueryString & "&sOnChange=" & OnChange & """," & IIf(bAllowInvalid, "true", "false") & ")", Disabled, TabIndex)

            End If

            If Not isDIVDefine Then
                ClientControl = Trim(ClientControl) & "</TD>" & IIf(isPopUp, "</TR><TR>", String.Empty) & "<TD><DIV ID='" & FieldClieName & "' CLASS=Field>" & lstrCliename & "</DIV>"
            End If
            ClientControl = ClientControl & HiddenControl(FieldName & "_Old", DefValue) & HiddenControl(FieldName & "_Digit" & "_Old", lstrDigit) & "</TD></TR></TABLE>"

        Else
            If DefValue <> String.Empty Then
                If Cliename = String.Empty Then
                    lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
                    With lobjClient
                        If .Find(DefValue) Then
                            sDigit = .sDigit
                            sDescript = .sCliename
                        End If
                    End With
                    'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lobjClient = Nothing
                Else
                    sDigit = Digit
                    sDescript = Cliename
                End If
                ClientControl = ClientControl & "-" & "</TD><TD>" & TextControl(FieldName & "_Digit", 2, sDigit, isRequired, Alias_Renamed, GridField, HRefUrl, HRefScript, , Disabled) & " " & "</TD><TD>" & TextControl(FieldName & "Des", 30, sDescript, isRequired, Alias_Renamed, GridField, HRefUrl, HRefScript, , Disabled)
            End If
            ClientControl = ClientControl & "</TD></TR></TABLE>"
        End If

        ClientRole = String.Empty
        TypeList = ecbeTypeList.none

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Control|ClientControl|" & FieldName, sSessionID
        '#End If
    End Function

    '% FileControl: devuelve la estructura de un campo para busqueda de archivos
    Public Function FileControl(ByVal FieldName As String, Optional ByVal Size As Short = 30, Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal OnChange As String = "") As String
        FileControl = "<INPUT TYPE=""FILE""" & " NAME=""" & FieldName & """" & " SIZE=""" & Size & """" & " TABINDEX=" & TabIndex

        If OnClick <> String.Empty Then
            FileControl = FileControl & " ONCLICK='" & OnClick & "'"
        End If

        If OnChange <> String.Empty OrElse OnChange <> String.Empty Then
            FileControl = FileControl & " ONCHANGE='" & OnChange & "'"
        End If

        If Disabled Then
            FileControl = DisabledControl(FileControl)
        End If

        FileControl = FileControl & ">"
    End Function

    '% SubmitControl: devuelve la estructura de un Botón Submit
    Public Function SubmitControl(ByVal FieldName As String, ByVal Value As String, Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        SubmitControl = "<INPUT TYPE=""SUBMIT""" & " NAME=""" & FieldName & """" & " VALUE=""" & Value & """" & " TABINDEX=" & TabIndex

        If OnClick <> String.Empty Then
            SubmitControl = SubmitControl & " ONCLICK='" & OnClick & "'"
        End If

        If Disabled Then
            SubmitControl = DisabledControl(SubmitControl)
        End If

        SubmitControl = SubmitControl & ">"
    End Function

    '% CheckControl: devuelve la estructura de un Check
    Public Function CheckControl(ByVal FieldName As String, ByVal Descript As String, Optional ByVal Checked As String = "", Optional ByVal DefValue As String = "1", Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal Alias_Renamed As String = "") As String

        CheckControl = "<INPUT TYPE=""CHECKBOX"" NAME=""" & FieldName & """" & " VALUE=""" & DefValue & """ TITLE=""" & Alias_Renamed & """ TABINDEX=" & TabIndex

        '+ Se marca como predeterminado
        If Checked = "1" Then
            CheckControl = CheckControl & " CHECKED"
        End If

        '+ Se dehabilita el control
        If ActionQuery Or Disabled Then
            CheckControl = DisabledControl(CheckControl)
        End If

        If OnClick <> String.Empty Then
            CheckControl = CheckControl & " ONCLICK='" & OnClick & ";'"
        End If

        CheckControl = CheckControl & "><LABEL TITLE=""" & Alias_Renamed & """>" & Descript & "</LABEL>"
    End Function

    '% OptionControl: devuelve la estructura para un OptionButton
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function OptionControl(ByVal Id As Integer, ByVal FieldName As String, ByVal Descript As String, Optional ByVal Checked As String = "", Optional ByVal DefValue As String = "1", Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal Alias_Renamed As String = "") As String

        OptionControl = "<TABLE VSPACE=""0"" HSPACE=""0"" CELLSPACING=""0"" CELLPADDING=""0"" BORDER=""0""><TR><TD><INPUT TYPE=""RADIO"" NAME=""" & FieldName & """" & " VALUE=""" & DefValue & """" & " TABINDEX=" & TabIndex & " TITLE=""" & Alias_Renamed & """"

        '+ Se marca como predeterminado
        If Checked = "1" Then
            OptionControl = OptionControl & " CHECKED "
        End If

        '+ Se dehabilita el control
        If ActionQuery Or Disabled Then
            OptionControl = DisabledControl(OptionControl)
        End If

        If OnClick <> String.Empty Then
            OptionControl = OptionControl & " ONCLICK='" & OnClick & ";'"
        End If
        OptionControl = OptionControl & "></TD><TD TITLE=""" & Alias_Renamed & """><LABEL ID=" & Id & ">" & Descript & "</LABEL></TD></TR></TABLE>"
    End Function

    '% HiddenControl: devuelve la estructura de un control oculto
    Public Function HiddenControl(ByVal FieldName As String, ByVal DefValue As String) As String
        'HiddenControl = "<INPUT TYPE=""HIDDEN"" NAME=""" & FieldName & """" & " VALUE=""" & DefValue & """>"
        HiddenControl = "<INPUT TYPE=""HIDDEN"" id=""" & FieldName & """" & " NAME=""" & FieldName & """" & " VALUE=""" & DefValue & """>"
    End Function

    '%insQueryField. Esta funcion se encarga de sustituir los campos por etiquetas
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Function insQueryField(ByVal TypeField As eTypeField, ByVal DefValue As String, ByVal Alias_Renamed As String, Optional ByVal DecimalPlaces As Short = 0, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal ShowThousand As Boolean = False) As String
        insQueryField = "<LABEL TITLE=""" & Alias_Renamed & """ CLASS=""FIELD"">"
        If HRefUrl <> String.Empty Then
            insQueryField = insQueryField & "<A HREF=""" & HRefUrl & """>"
        Else
            If HRefScript <> String.Empty And (Not mblnActionQuery Or EditRecordQuery) Then
                insQueryField = insQueryField & "<A HREF=""JAVASCRIPT:"" ONCLICK=""" & HRefScript & "; return false"">"
            End If
        End If

        If TypeField = eTypeField.clngDateValue Or TypeField = eTypeField.clngNumericValue Then
            insQueryField = Trim(insQueryField) & insFormatField(DefValue, ShowThousand, TypeField, DecimalPlaces)
        Else
            insQueryField = Trim(insQueryField) & HTMLEncode(insFormatField(DefValue, ShowThousand, TypeField, DecimalPlaces))
        End If
        If HRefScript <> String.Empty Or HRefUrl <> String.Empty Then
            insQueryField = Trim(insQueryField) & "</A>"
        End If
        insQueryField = Trim(insQueryField) & "</LABEL>"
    End Function

    '%insFormatField: Retorna el valor formateado según su tipo
    Private Function insFormatField(ByVal DefValue As String, ByVal ShowThousand As Boolean, ByVal TypeField As eTypeField, Optional ByVal DecimalPlaces As Short = 0) As String
        '+Se formatea el valor cuando es numérico
        If DefValue > String.Empty Then
            If TypeField = eTypeField.clngNumericValue Then
                If IsNumeric(DefValue) Then
                    DefValue = FormatNumber(DefValue, DecimalPlaces, , , ShowThousand)
                Else
                    DefValue = String.Empty
                End If

                '+Se formatea el valor cuando es fecha
            ElseIf TypeField = eTypeField.clngDateValue Then
                If IsDate(DefValue) Then
                    DefValue = FormatDateTime(CDate(DefValue), DateFormat.ShortDate)
                    '+Se puede escribir de la sgte manera para mostrar tambien la hora:
                    '+  DefValue = FormatDateTime(DefValue)
                Else
                    DefValue = String.Empty
                End If
            End If
        End If
        insFormatField = DefValue
    End Function

    '%CreateCalendar: Código HTML para crear un calendario
    Public Function CreateCalendar(ByVal CurDate As Date) As String
        Dim lintMaxDay As Short
        Dim lintDayOfWeek As Short
        Dim lintCount As Short
        Dim lintCount2 As Short
        Dim lstrYearMonth As String
        Dim ldtmAux As Date
        Dim lintPlus As Short
        Dim lintIndex As Short
        Dim lobjGeneralForm As Object
        Dim lstrHollidayArray() As String
        Dim lblnHolliday As Boolean

        lintPlus = 0

        '+Se construyen los meses que van a aparecer en el combo de meses

        TypeOrder = ecbeOrder.Code
        mblnBlank = False
        lstrYearMonth = PossiblesValues("cboMonth", "table7013", eValuesType.clngComboType, CStr(DatePart(Microsoft.VisualBasic.DateInterval.Month, CurDate))) & HiddenControl("tcnCurMonth", CStr(DatePart(Microsoft.VisualBasic.DateInterval.Month, CurDate))) & "<SELECT NAME=cboYear>"

        '+Se construyen los años que van a aparecer en el combo de años

        For lintIndex = DatePart(Microsoft.VisualBasic.DateInterval.Year, CurDate) - 60 To DatePart(Microsoft.VisualBasic.DateInterval.Year, CurDate) - 1
            lstrYearMonth = lstrYearMonth & "<OPTION VALUE=" & CStr(lintIndex - DatePart(Microsoft.VisualBasic.DateInterval.Year, CurDate)) & ">" & CStr(lintIndex) & "</option>"
        Next

        lstrYearMonth = lstrYearMonth & "<OPTION VALUE=0 SELECTED>" & Format(CurDate, "yyyy") & "</option>"

        For lintIndex = DatePart(Microsoft.VisualBasic.DateInterval.Year, CurDate) + 1 To DatePart(Microsoft.VisualBasic.DateInterval.Year, CurDate) + 60
            lstrYearMonth = lstrYearMonth & "<OPTION VALUE=" & CStr(lintIndex - DatePart(Microsoft.VisualBasic.DateInterval.Year, CurDate)) & ">" & CStr(lintIndex) & "</option>"
        Next

        lstrYearMonth = lstrYearMonth & "</SELECT>"

        ldtmAux = DateAdd(Microsoft.VisualBasic.DateInterval.Day, (DatePart(Microsoft.VisualBasic.DateInterval.Day, CurDate) - 1) * -1, CurDate)
        CreateCalendar = String.Empty
        lintMaxDay = DatePart(Microsoft.VisualBasic.DateInterval.Day, DateAdd(Microsoft.VisualBasic.DateInterval.Day, -1, DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, DateAdd(Microsoft.VisualBasic.DateInterval.Day, (DatePart(Microsoft.VisualBasic.DateInterval.Day, CurDate) - 1) * -1, CurDate))))
        lintDayOfWeek = 1

        CreateCalendar = "<TABLE BORDER=""0"" CELLSPACING=""3"" CELLPADDING=""3"" CLASS=""Calendar"">" & "<TR>" &
                                    "<TD>" & AnimatedButtonControl("cmdBack", "/VTimeNet/images/btnLargeBackOff.png", HttpContext.GetGlobalResourceObject("BackOfficeResource", "PreviousMonth"), , "insBackNext(0)") & "</TD>" &
                                    "<TD COLSPAN=""5"" ALIGN=""CENTER""><LABEL>" & UCase(Mid(lstrYearMonth, 1, 1)) & Mid(lstrYearMonth, 2) & "</LABEL></td>" &
                                    "<TD>" & AnimatedButtonControl("cmdNext", "/VTimeNet/images/btnLargeNextOff.png", HttpContext.GetGlobalResourceObject("BackOfficeResource", "NextMonth"), , "insBackNext(1)") & "</TD>" & "</TR> " & "<TR> " &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Sunday") & "</TH>" &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Monday") & "</TH>" &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Tuesday") & "</TH> " &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Wednesday") & "</TH> " &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Thursday") & "</TH>" &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Friday") & "</TH> " &
                                    "<TH WIDTH=25pcx>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "Saturday") & "</TH> " & "</TR> <TR>"
        For lintCount = 1 To Weekday(ldtmAux) - 1
            CreateCalendar = CreateCalendar & "<TD ALIGN=""CENTER""></TD>" & C_Separator
            lintDayOfWeek = lintDayOfWeek + 1
        Next lintCount



        '+ Se crea el arreglo que contiene los dias feriados de un mes segun la tabla hollidays
        lobjGeneralForm = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Hollidays")

        lstrHollidayArray = Microsoft.VisualBasic.Split(lobjGeneralForm.strHollidaysArray(DatePart(Microsoft.VisualBasic.DateInterval.Month, CurDate)), "|")

        'UPGRADE_NOTE: Object lobjGeneralForm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjGeneralForm = Nothing

        For lintCount = 1 To lintMaxDay
            If lintDayOfWeek > 7 Then
                CreateCalendar = CreateCalendar & "</tr> <tr>"
                lintDayOfWeek = 1
            End If

            '+ Se realiza el ciclo para buscar los días feriados
            For lintCount2 = 1 To UBound(lstrHollidayArray) - 1
                If lintCount = CDbl(lstrHollidayArray(lintCount2)) Then
                    lblnHolliday = True
                    Exit For
                ElseIf lintCount < CDbl(lstrHollidayArray(lintCount2)) Then
                    lblnHolliday = False
                    Exit For
                End If
            Next lintCount2

            '+ Si el dia en tratamiento es feriado entonces se coloca el fondo rojo
            If lblnHolliday Then

                If lintCount = DatePart(Microsoft.VisualBasic.DateInterval.Day, CurDate) Then
                    If lintDayOfWeek = 1 Or lintDayOfWeek = 7 Then
                        CreateCalendar = CreateCalendar & "<td align=""CENTER"" CLASS=""HOLLIDAYS""><STRONG><A style=""color=red"" HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></STRONG></td>" & C_Separator
                    Else
                        CreateCalendar = CreateCalendar & "<TD ALIGN=""CENTER""  CLASS=""HOLLIDAYS""><STRONG><A HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></STRONG></td>" & C_Separator
                    End If
                Else
                    If lintDayOfWeek = 1 Or lintDayOfWeek = 7 Then
                        CreateCalendar = CreateCalendar & "<td align=""CENTER"" CLASS=""HOLLIDAYS""><A style=""color=red"" HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></td>" & C_Separator
                    Else
                        CreateCalendar = CreateCalendar & "<td align=""CENTER"" CLASS=""HOLLIDAYS""><A HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></td>" & C_Separator
                    End If
                End If
            Else
                If lintCount = DatePart(Microsoft.VisualBasic.DateInterval.Day, CurDate) Then
                    If lintDayOfWeek = 1 Or lintDayOfWeek = 7 Then
                        CreateCalendar = CreateCalendar & "<td align=""CENTER""><STRONG><A style=""color=red"" HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></STRONG></td>" & C_Separator
                    Else
                        CreateCalendar = CreateCalendar & "<td align=""CENTER""><STRONG><A HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></STRONG></td>" & C_Separator
                    End If
                Else
                    If lintDayOfWeek = 1 Or lintDayOfWeek = 7 Then
                        CreateCalendar = CreateCalendar & "<td align=""CENTER""><A style=""color=red"" HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></td>" & C_Separator
                    Else
                        CreateCalendar = CreateCalendar & "<td align=""CENTER""><A HREF=""JAVASCRIPT:compute(" & Str(lintCount) & ")"">" & Format(lintCount, "00") & "</A></td>" & C_Separator
                    End If
                End If
            End If

            lblnHolliday = False
            lintDayOfWeek = lintDayOfWeek + 1
            If lintDayOfWeek = 7 Then
            End If
        Next lintCount
        CreateCalendar = CreateCalendar & " </TR>" & "</TABLE>"
    End Function

    '%Valtext. Esta funcion se encarga de generar el código JavaScript, para la
    '%validación de un campo texto
    Private Function ValText() As String
        Dim varAux = ""
        If Not mblnValid Then
            varAux = "<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/ValFunctions.js""></SCRIPT>" & "<SCRIPT>var mstrDoSubmit = ""1"";</SCRIPT>"
            mblnValid = True
        End If
        Return varAux
    End Function

    '% DateToString: devuelve un campo fecha con formato
    Public Function DateToString(ByVal dDate As Date) As String
        DateToString = TypeToString(dDate, eTypeData.etdDate)
    End Function

    '% DateToString: devuelve un campo fecha con formato
    Public Function StringToDate(ByVal sDate As String) As Date
        If IsDate(sDate) Then
            StringToDate = FormatDateTime(CDate(sDate), DateFormat.ShortDate)
        End If
    End Function

    '% Date_Diff: Devuelve el número de intervalos de tiempo entre dos fechas
    Public Function Date_Diff(ByVal Interval As String, ByVal dDate_Ini As Date, ByVal dDate_Fin As Date, Optional ByVal FirstDayOfWeek As FirstDayOfWeek = FirstDayOfWeek.Sunday, Optional ByVal FirstWeekOfYear As FirstWeekOfYear = FirstWeekOfYear.Jan1) As Integer
        Date_Diff = DateDiff(Interval, dDate_Ini, dDate_Fin, FirstDayOfWeek, FirstWeekOfYear)
    End Function

    '% StringToType: devuelve NULL (equivalente para cada tipo de dato), o la conversion al tipo
    '%               correspondiente.
    Public Function StringToType(ByVal sField As String, ByVal Stype As eTypeData, Optional ByVal ZeroIsNull As Boolean = False) As Object
        On Error GoTo StringToType_Err
        Dim lblnIsZero As Boolean
        StringToType = Nothing
        If Not sField Is Nothing Then
            sField = sField.Trim()
        End If
        If Stype >= eTypeData.etdInteger AndAlso Stype <= eTypeData.etdDouble AndAlso Not String.IsNullOrEmpty(sField) AndAlso Not IsNumeric(sField) Then
            Err.Raise(vbObjectError + 4096, "eFunctions.Values.StringToType", "Can not convert value """ & sField & """ to numeric type.")
        End If
        If Not String.IsNullOrEmpty(sField) AndAlso Stype <> eTypeData.etdDate AndAlso IsNumeric(sField) Then
            lblnIsZero = CDbl(sField) = 0
        End If
        If Trim(sField) = String.Empty Or (lblnIsZero And ZeroIsNull) Then
            '+ Se asigna el equivalente de NULL para cada uno de los tipos
            Select Case Stype
                Case eTypeData.etdDate
                    StringToType = eRemoteDB.Constants.dtmNull
                Case eTypeData.etdInteger, eTypeData.etdLong, eTypeData.etdDouble
                    StringToType = eRemoteDB.Constants.intNull
                Case eTypeData.etdOthers
                    StringToType = Nothing
                Case eTypeData.etdBoolean
                    StringToType = False
            End Select
        Else

            '+ Se asigna la conversion para cada uno de los tipos
            Select Case Stype
                Case eTypeData.etdDate
                    If Trim(sField) = "12:00:00 a.m." Then
                        StringToType = eRemoteDB.Constants.dtmNull
                    Else
                        StringToType = SysDateFormat(sField)
                    End If
                Case eTypeData.etdLong
                    StringToType = CLng(sField)
                Case eTypeData.etdInteger
                    StringToType = CInt(sField)
                Case eTypeData.etdDouble
                    StringToType = CDbl(sField)
                Case eTypeData.etdOthers
                    StringToType = CInt(sField)
                Case eTypeData.etdBoolean
                    StringToType = CBool(sField)

            End Select
        End If
        Exit Function

StringToType_Err:
        ProcError("Values.StringToType(sField,Stype,ZeroIsNull)", New Object() {sField, Stype, ZeroIsNull})
    End Function

    '% TypeToString: Convertir una valor dado su tipo a un valor de tipo cadena
    Public Function TypeToString(ByVal sField As Object, ByVal sTypeData As eTypeData, Optional ByVal ShowThousand As Boolean = False, Optional ByVal DecimalPlaces As Short = 0) As Object
        On Error GoTo TypeToString_Err

        Select Case sTypeData
            Case eTypeData.etdLong, eTypeData.etdInteger, eTypeData.etdDouble
                If IsNumeric(sField) Then
                    If sField = numNull Or CDbl(Mid(sField, 1, 6)) = numNull Then
                        TypeToString = ""
                    Else
                        TypeToString = CStr(FormatNumber(sField, DecimalPlaces, , , ShowThousand))
                    End If
                Else
                    TypeToString = ""
                End If

            Case eTypeData.etdDate
                If IsDate(sField) Then
                    If CDate(sField) = eRemoteDB.Constants.dtmNull Then
                        TypeToString = ""
                    Else
                        TypeToString = FormatDateTime(sField, DateFormat.ShortDate)
                        '+Se puede escribir de la sgte manera para mostrar tambien la hora:
                        '+                    TypeToString = FormatDateTime(sField)
                    End If
                Else
                    TypeToString = ""
                End If

            Case Else
                If IsNumeric(sField) Then
                    If sField = numNull Or CDbl(Mid(sField, 1, 6)) = numNull Then
                        TypeToString = ""
                    Else
                        TypeToString = CStr(sField)
                    End If
                Else
                    TypeToString = CStr(sField)
                End If
        End Select

TypeToString_Err:
        If Err.Number Then
            TypeToString = "TypeToString(" & sField & "): " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% DisabledControl: manejo general para deshabilitar controles
    Private Function DisabledControl(ByVal HTMLControl As String) As String
        DisabledControl = HTMLControl & " NOTAB DISABLED ONFOCUS=""ChangeFocus(this)"""
    End Function

    '%IsValid. Se utiliza esta funcion para verificar si el contenido de un campo
    '%es correcto.
    Public Function IsValid(ByVal TableName As String, Optional ByVal ValueToCheck As String = "0", Optional ByVal NeedParam As Boolean = False) As Boolean
        If TableName <> String.Empty Then
            mstrDefValue = Trim(String.Empty & ValueToCheck)
            If mstrDefValue = String.Empty Then
                IsValid = True
            Else
                mblnIsValid = False
                Call LoadDataTable(TableName, NeedParam, True)
            End If
        Else
            mblnIsValid = False
            IsValid = False
        End If
        IsValid = mblnIsValid
        'UPGRADE_NOTE: Object mParameters may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mParameters = Nothing
    End Function

    '%LoadValues: Cargar los valores de un combo o valores posibles
    Public Function LoadValues(ByVal Source As String, Optional ByVal Condition As String = "", Optional ByVal HRefScript As String = "", Optional ByVal ComboSize As Short = 8, Optional ByVal NeedParameters As Boolean = False, Optional ByVal ShowDescript As Boolean = True) As String
        Dim lobjTables As Tables
        Dim lblnExit As Boolean
        Dim lobjGrid As Grid
        Dim lintCount As Short
        Dim lintRParameters As Short
        Dim llngRecordQ As Integer
        Dim lblnReady As Boolean
        Dim lclsParameter As Parameter
        Dim sValAux As String = ""

        '    #If LOG Then
        '        eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|LoadValues|" & Source, sSessionID
        '    #End If

        lobjTables = New Tables

        llngRecordQ = 0
        List = "*" & List & "*"

        lobjGrid = New Grid
        With lobjGrid.Columns
            .AddTextColumn(0, HttpContext.GetGlobalResourceObject("BackOfficeResource", "CodeColumnCaption"), "tctCode", 20, String.Empty)
            If ShowDescript Then
                .AddTextColumn(0, HttpContext.GetGlobalResourceObject("BackOfficeResource", "DescriptionColumnCaption"), "tctDescript", 30, String.Empty)
            End If
            If Parameters.Count_ReturnValue > 0 Then
                For lintRParameters = 1 To Parameters.Count_ReturnValue
                    lclsParameter = Parameters.Item_ReturnValue(lintRParameters)
                    .AddTextColumn(0, lclsParameter.TitleColumn, lclsParameter.Name, 30, String.Empty)
                    lobjGrid.Columns((lclsParameter.Name)).GridVisible = lclsParameter.VisibleColumn
                Next lintRParameters
            End If
        End With
        With lobjGrid
            .AltRowColor = True
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = False
        End With
        With lobjTables
            .Condition = Condition
            .TypeOrder = TypeOrder
            If NeedParameters Then
                If mParameters.Count = 0 Then
                    lblnExit = True
                Else
                    .Parameters = mParameters
                End If
            End If
            If Not lblnExit Then
                If .reaTable(Source) Then
                    lintCount = 0
                    Do While (Not lobjTables.EOF) And (llngRecordQ < 201)
                        llngRecordQ = llngRecordQ + 1
                        lblnReady = True

                        If TypeList = ecbeTypeList.Exclution Then
                            lblnReady = Not FindValue(lobjTables)
                        ElseIf TypeList = ecbeTypeList.Inclution Then
                            lblnReady = FindValue(lobjTables)
                        End If

                        If lblnReady Then
                            If ShowDescript Then
                                lobjGrid.Columns("tctDescript").HRefScript = "insReturnValues(" & CStr(lintCount) & ")"
                                lobjGrid.Columns("tctDescript").DefValue = Trim("" & .Fields(.DescriptField))
                                'lobjGrid.mblnLoadValue = True
                            Else
                                lobjGrid.Columns("tctCode").HRefScript = "insReturnValues(" & CStr(lintCount) & ")"
                                'lobjGrid.mblnLoadValue = True
                            End If
                            lobjGrid.Columns("tctCode").DefValue = Trim("" & .Fields(.KeyField))

                            If Parameters.Count_ReturnValue > 0 Then
                                For lintRParameters = 1 To Parameters.Count_ReturnValue
                                    lclsParameter = Parameters.Item_ReturnValue(lintRParameters)
                                    lobjGrid.Columns((lclsParameter.Name)).DefValue = Trim(.Fields(lclsParameter.Name))
                                Next lintRParameters
                            End If

                            sValAux = RTrim(sValAux) & lobjGrid.DoRow()
                            lintCount = lintCount + 1
                        End If
                        .NextRecord()
                    Loop
                    If llngRecordQ >= 201 And Not lobjTables.EOF Then
                        sValAux = RTrim(sValAux) & "<SCRIPT>alert('" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "AlertOnly200Elements") & "')</SCRIPT>"
                    End If
                    .closeTable()
                End If
            End If
            Condition = String.Empty
            .Condition = Condition
            sValAux = RTrim(sValAux) & lobjGrid.closeTable()
            Return sValAux
        End With
        'UPGRADE_NOTE: Object lobjGrid may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjGrid = Nothing
        'UPGRADE_NOTE: Object lclsParameter may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsParameter = Nothing
        '    #If LOG Then
        '        eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|LoadValues|" & Source, sSessionID
        '    #End If
    End Function

    '% AnimatedButtonControl: devuelve el código HTML para la construcción de imágenes
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AnimatedButtonControl(ByVal ButtonName As String, Optional ByVal Src As String = "", Optional ByVal Alias_Renamed As String = "", Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal nIndex As Integer = 0) As String
        Dim lstrSRCOff As String

        If Src = String.Empty Then
            Src = "btn_" & ButtonName & "Off.png"
        End If
        lstrSRCOff = Src

        If HRefUrl <> String.Empty Then
            AnimatedButtonControl = "<A HREF='" & HRefUrl & "'"
        Else
            AnimatedButtonControl = "<A HREF='JAVASCRIPT:;' ONCLICK='if(insDisabledButton(document." & ButtonName & "," & nIndex & ")){" & Replace(HRefScript, "'", """") & "}; return false'"
        End If
        AnimatedButtonControl = AnimatedButtonControl & " OnmouseOver='insChangeImage(""" & ButtonName & """,1)'" & " OnmouseOut='insChangeImage(""" & ButtonName & """,2)'>" & "<IMG BORDER=0 ALIGN=MIDDLE SRC=""" & lstrSRCOff & """" & IIf(TabIndex = 0, "", " TABINDEX=" & TabIndex) & " NAME=""" & ButtonName & """" & IIf(Width = 0, "", " WIDTH=" & Width) & IIf(Height = 0, "", " HEIGHT=" & Height) & " ALT=""" & Alias_Renamed & """" & " Title=""" & Alias_Renamed & """" & " OnmouseMove='MouseMoveImage(this, true)'" & " OnmouseOut='MouseMoveImage(this, false)'>" & "</A>"
        If Disabled Then
            If nIndex = 0 Then
                AnimatedButtonControl = AnimatedButtonControl & "<SCRIPT>document." & ButtonName & ".disabled=true</SCRIPT>"
            ElseIf nIndex > 0 Then
                AnimatedButtonControl = AnimatedButtonControl & "<SCRIPT>document." & ButtonName & "[" & nIndex & "].disabled=true</SCRIPT>"
            End If
        End If
    End Function

    '%GetHelpPath: Esta funcion se encarga de generar el donde se encuentran los funcionales
    '%             para la transacción seleccionada de Visual TIME
    Public Function GetHelpPath(ByVal Codispl As String) As String
        Dim lobjWindows As eRemoteDB.Query
        Dim lclsConfig As eRemoteDB.VisualTimeConfig
        Dim lstrCodisp As String
        Dim lstrPath As String
        Dim lintUseCod As Short

        Dim lbooJustQuote As Boolean
        Dim lintWindowTy As Short

        On Error GoTo getHelpPath_err

        lobjWindows = New eRemoteDB.Query


        GetHelpPath = String.Empty

        If Codispl <> String.Empty Then
            If lobjWindows.OpenQuery("windows", "sHelpPath,scodisp,nwindowty,scodispl", "scodispl = '" & Codispl & "'") Then

                lstrCodisp = IIf(LCase(lobjWindows.FieldToClass("sCodisp")) = "ma1000", LCase(lobjWindows.FieldToClass("sCodispl")), LCase(lobjWindows.FieldToClass("sCodisp")))
                lstrPath = LCase(lobjWindows.FieldToClass("sHelpPath"))
                If String.IsNullOrEmpty(lstrPath) And LCase(lobjWindows.FieldToClass("sCodisp")) = "ma1000" Then
                    'no tiene funcional asociada, debe mostrar funcional ma1000
                    lstrCodisp = "ma1000"
                    lstrPath = "Mantenimiento"
                Else
                    lstrPath = LCase(lobjWindows.FieldToClass("sHelpPath"))
                End If
                lintWindowTy = CShort(LCase(lobjWindows.FieldToClass("nwindowty")))
                lobjWindows.CloseQuery()

                '+Se determina si para completar la ruta de ayuda se usa el código lógico o físico según:
                '+   "1:directorio" : Se usa código físico
                '+   "2:directorio" : Se usa código lógico
                '+   "directorio"   : Se usa código físico
                '+   ""             : Se usa código físico
                '+En el primer caso, Val() dará 1, en el segundo, 2, y los siguientes 0.
                lintUseCod = Val(lstrPath)

                '+Si existe indicador se elimina de ruta
                If lintUseCod <> 0 Then
                    lstrPath = Mid(lstrPath, InStr(lstrPath, ":") + 1)
                End If

                '+Se define si se usa código lógico o fisico
                lclsConfig = New eRemoteDB.VisualTimeConfig
                If lintWindowTy = 8 Then
                    GetHelpPath = lclsConfig.LoadSetting("Help", "http://www.ease.com/apoyo/visualtime/Funcionales-html/es_es/funcionales/", "Paths") & lstrPath & "VisualTime.html"
                Else
                    If lintUseCod = 2 Then
                        GetHelpPath = lclsConfig.LoadSetting("Help", "http://www.ease.com/apoyo/visualtime/Funcionales-html/es_es/funcionales/", "Paths") & lstrPath & "/" & Codispl & ".html"
                    Else
                        GetHelpPath = lclsConfig.LoadSetting("Help", "http://www.ease.com/apoyo/visualtime/Funcionales-html/es_es/funcionales/", "Paths") & lstrPath & "/" & lstrCodisp & ".html"
                    End If
                End If
                'UPGRADE_NOTE: Object lclsConfig may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsConfig = Nothing
            End If
        End If

getHelpPath_err:
        If Err.Number Then
            GetHelpPath = "GetHelpPath:" & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjWindows = Nothing
    End Function

    '%ShowWindowsName: Esta funcion devuelve el código HTML que muestra la descripción de
    '%                 la ventana.
    Public Function ShowWindowsName(ByVal Codispl As String, Optional ByVal sWindowDescript As String = "") As String
        Dim lobjWindows As eRemoteDB.Query
        Dim lstrName As String = ""

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|ShowWindowsName|" & Codispl, sSessionID
        '#End If
        If Codispl = mstrCodispl Then
            lstrName = HTMLDecode(mstrDescriptWindows)
        Else
            If sWindowDescript = String.Empty Then
                lobjWindows = New eRemoteDB.Query
                With lobjWindows
                    If .OpenQuery("Windows", "sDescript", "sCodispl ='" & Codispl & "'") Then
                        lstrName = .FieldToClass("sDescript")
                        .CloseQuery()
                    End If
                End With
                'UPGRADE_NOTE: Object lobjWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjWindows = Nothing
            Else
                lstrName = HTMLDecode(sWindowDescript)
            End If
        End If
        ShowWindowsName = "<H2 CLASS=""WindowsName"">" & "&nbsp;" & HTMLEncode(lstrName) & "</H2><HR>"

        '#If LOG Then
        '    eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|ShowWindowsName|" & Codispl, sSessionID
        '#End If
    End Function

    '% ConfirmDelete: Esta funcion devuelve el código HTML que muestra la confirmacion cuando
    '%                la página va a eliminar algún registro
    Public Function ConfirmDelete(Optional ByVal ShowImage As Boolean = False, Optional ByVal HRefScript As String = "") As String
        Dim lstrHTMLCode As String

        On Error GoTo ConfirmDelete_Err

        If ShowImage Then
            If Trim(HRefScript) = String.Empty Then
                HRefScript = "insConfirmDelete()"
            End If
            lstrHTMLCode = "<BR>" & C_Separator & "<TABLE WIDTH=""100%"">" & C_Separator & vbTab & "<TR ALIGN=""Right"">" & C_Separator & vbTab & vbTab & "<TD>" & ButtonAcceptCancel(HRefScript, , False, , eButtonsToShow.OnlyAccept) & "</TD>" & C_Separator & vbTab & "</TR>" & "</TABLE>"
        Else
            lstrHTMLCode = "<B><LABEL>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "DataDeleted") & "</LABEL></B>" & C_Separator
        End If

        ConfirmDelete = lstrHTMLCode
ConfirmDelete_Err:
        If Err.Number Then
            ConfirmDelete = "ConfirmDelete: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% DataNotFound: Devuelve un mensaje para indicar búsquedas fallidas en la base de datos
    Public Function DataNotFound(Optional ByVal nColSpan As Short = 0) As String
        Dim strResultado As String = ""
        Try
            If nColSpan > 0 Then
                strResultado = "<TD COLSPAN=""" & nColSpan & """ ALIGN=""CENTER"">"
            End If

            strResultado = strResultado & "<LABEL>" & HttpContext.GetGlobalResourceObject("BackOfficeResource", "DataNotFound") & "</LABEL>"

            If nColSpan > 0 Then
                strResultado = strResultado & "</TD>"
            End If
            Return strResultado
        Catch ex As Exception
            Return strResultado
        End Try
    End Function

    '% ButtonImages: devuelve la estructura para la imagen asociada al llamado
    '%              de la ventana de Imagenes
    Public Function ButtonImages(ByVal sCodispl As String, ByVal nImageNum As Integer, Optional ByVal ShowSmallImage As Boolean = True, Optional ByVal bQuery As Boolean = True, Optional ByVal nIndexImageNum As Short = 0, Optional ByVal TabIndex As Short = 0, Optional ByVal GridField As Boolean = False) As String
        Dim lstrSRC As String
        Dim lstrAlias As String

        lstrAlias = GetMessage(803)

        '+ Se asocia la imagen correcta
        If nImageNum = 0 Or nImageNum = numNull Then
            lstrSRC = IIf(ShowSmallImage, "/VTimeNet/Images/btnWONotes.png", "/VTimeNet/Images/menu_WONotes.png")
        Else
            lstrSRC = IIf(ShowSmallImage, "/VTimeNet/Images/btnWNotes.png", "/VTimeNet/Images/menu_transaction.png")
        End If

        If GridField Then
            ButtonImages = AnimatedButtonControl("btnImagenum", lstrSRC, lstrAlias, , "ShowImagePopUp('" & sCodispl & "',(CurrentIndex>=0?(top.opener.marrArray[CurrentIndex].btnImagenum==''?0:top.opener.marrArray[CurrentIndex].btnImagenum):0)," & IIf(bQuery, Menues.TypeActions.clngActionQuery, Menues.TypeActions.clngActionadd) & "," & nIndexImageNum & ")", False, TabIndex)
        Else
            ButtonImages = AnimatedButtonControl("btnImagenum", lstrSRC, lstrAlias, , "ShowImagePopUp('" & sCodispl & "'," & nImageNum & "," & IIf(bQuery, Menues.TypeActions.clngActionQuery, Menues.TypeActions.clngActionadd) & "," & nIndexImageNum & ")", False, TabIndex)
        End If

        '+ Si se permite editar la nota, se crea un campo oculta para su actualización en la página

        If Not bQuery Then
            ButtonImages = ButtonImages & HiddenControl("tcnImagenum", CStr(nImageNum))
        End If
    End Function

    '% ButtonNotes: devuelve la estructura para la imagen asociada al llamado
    '%              de la ventana de Notas
    Public Function ButtonNotes(ByVal sCodispl As String, ByVal nNotenum As Double, Optional ByVal ShowSmallImage As Boolean = True, Optional ByVal bQuery As Boolean = True, Optional ByVal nIndexNotenum As Double = 0, Optional ByVal nOriginalNotenum As Double = 0, Optional ByVal nCopyNotenum As Double = 0, Optional ByVal TabIndex As Short = 0, Optional ByVal GridField As Boolean = False, Optional ByVal sFieldName As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal nIndex As Integer = 0) As String
        Dim lstrSRC As String
        Dim lstrAlias As String

        '+ Se asocia la descripción de la imagen
        lstrAlias = HttpContext.GetGlobalResourceObject("BackOfficeResource", "NoteGeneralInformation")

        '+ Se asocia la imagen correcta
        If nNotenum = 0 Or nNotenum = numNull Then
            lstrSRC = IIf(ShowSmallImage, "/VTimeNet/Images/btnWONotes.png", "/VTimeNet/Images/menu_WONotes.png")
        Else
            lstrSRC = IIf(ShowSmallImage, "/VTimeNet/Images/btnWNotes.png", "/VTimeNet/Images/menu_transaction.png")
        End If

        If GridField Then
            ButtonNotes = AnimatedButtonControl(IIf(sFieldName = String.Empty, "btnNotenum", sFieldName), lstrSRC, lstrAlias, , "ShowNotesPopUp('" & sCodispl & "'," & nNotenum & "," & IIf(bQuery, Menues.TypeActions.clngActionQuery, Menues.TypeActions.clngActionadd) & "," & nIndexNotenum & "," & nOriginalNotenum & "," & nCopyNotenum & ",'" & sQueryString & "')", Disabled, TabIndex, nIndex)
        Else
            ButtonNotes = AnimatedButtonControl(IIf(sFieldName = String.Empty, "btnNotenum", sFieldName), lstrSRC, lstrAlias, , "ShowNotesPopUp('" & sCodispl & "'," & nNotenum & "," & IIf(bQuery, Menues.TypeActions.clngActionQuery, Menues.TypeActions.clngActionadd) & "," & nIndexNotenum & "," & nOriginalNotenum & "," & nCopyNotenum & ",'" & sQueryString & "')", Disabled, TabIndex)
        End If
        '+ Si se permite editar la nota, se crea un campo oculta para su actualización en la página
        If Not bQuery Then
            ButtonNotes = ButtonNotes & HiddenControl(IIf(sFieldName = "btnNotenum" Or sFieldName = String.Empty, "tcnNotenum", sFieldName), CStr(nNotenum))
        End If
    End Function

    '% ButtonLedCompan : Crea un botón de Selección de Compañías Contables
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function ButtonLedCompan(ByVal FieldName As String, ByVal DefValue As Short, Optional ByVal Alias_Renamed As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal OnChange As String = "") As String

        Dim lclsLedCompan As Object
        Dim lobjValues As Values
        Dim lstrOnChange As String = ""

        lobjValues = New Values


        If OnChange <> String.Empty OrElse OnChange <> String.Empty Then
            lstrOnChange = "&OnChange=" & OnChange
        End If

        ButtonLedCompan = "<TABLE WIDTH=100%>" & "<TR>" & "<TD WIDTH=20pcx>" & lobjValues.AnimatedButtonControl(FieldName, "/VTimeNet/Images/Lupa.bmp", Alias_Renamed, , "ShowPopUp('/VTimeNet/Common/CP099.aspx?FieldName=" & FieldName & lstrOnChange & "','LedCompSel', 300, 260, 'no', 'no')", Disabled) & "</TD>" & "<TD>" & lobjValues.HiddenControl("tcn" & FieldName, "0") & lobjValues.DIVControl(FieldName & "Desc") & "</TD>" & "</TR>" & "</TABLE>"

        If DefValue <> numNull Then
            lclsLedCompan = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Led_compan")
            If lclsLedCompan.Find(DefValue) Then
                ButtonLedCompan = ButtonLedCompan & "<SCRIPT>UpdateDiv(""" & FieldName & "Desc"",""" & lclsLedCompan.sDescript & """);" & "with(self.document.forms[" & nParentForm & "]){ tcnLedCompan.value = " & lclsLedCompan.nLed_compan & "} </SCRIPT>"
            End If
            'UPGRADE_NOTE: Object lclsLedCompan may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsLedCompan = Nothing
        End If

        'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjValues = Nothing
    End Function

    '% ButtonAssociate: devuelve la estructura para la imagen asociada al llamado
    '%                  de la ventana de consultas asociadas
    Public Function ButtonAssociate(ByVal nKeynum As Short, ByVal ButtonName As String, Optional ByVal bQuery As Boolean = True, Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As String
        Dim lstrSRC As String
        Dim lstrAlias As String

        lstrAlias = "Consultas Asociadas"

        '+ Se asocia la imagen correcta
        lstrSRC = "/VTimeNet/Images/menu_query.png"

        ButtonAssociate = AnimatedButtonControl(ButtonName, lstrSRC, lstrAlias, , "ShowPopUp('/VTimeNet/Common/Associated.aspx?nKeynum=" & nKeynum & "&sStringCa=" & sQueryString & "','Associate', 320, 280, 'no', 'no')")

        sQueryString = ""

    End Function

    '% CompanyControl: Devuelve la estructura para la búsqueda de las compañias.
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function CompanyControl(ByVal FieldName As String, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal FieldCompanyName As String = "", Optional ByVal isDIVDefine As Boolean = False, Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal TabIndex As Short = 0) As String
        Dim lstrOnChange As String
        Dim lobjCompany As Object = New Object
        Dim lstrCompanyname As String
        Dim lintCount As Short

        lstrCompanyname = String.Empty
        lintCount = 0

        '+ Arma la estructura del evento OnChange del botón.

        lstrOnChange = "ValidateCompany(this,""" & FieldCompanyName & """); "
        lstrOnChange = lstrOnChange & OnChange

        CompanyControl = "<TABLE CELLPADING=0 CELLSPACING=0 BORDER=0><TR><TD>"
        CompanyControl = CompanyControl & " " & TextControl(FieldName, 4, DefValue, isRequired, Alias_Renamed, GridField, HRefUrl, HRefScript, lstrOnChange, Disabled, TabIndex) & " "

        If Not mblnActionQuery And Not GridField Then
            CompanyControl = Trim(CompanyControl) & AnimatedButtonControl("btn" & FieldName, "/VTimeNet/Images/btn_ValuesOff.png", Alias_Renamed, , "insShowCompanyQuery('" & FieldName & "','" & FieldCompanyName & "')", , TabIndex)

            If Not Trim(FieldCompanyName) = String.Empty Then
                If Not isDIVDefine Then

                    If DefValue <> String.Empty Then
                        lobjCompany = eRemoteDB.NetHelper.CreateClassInstance("eCoReinsuran.Company")
                        If lobjCompany.insPreparedQuery(DefValue, String.Empty, String.Empty) Then
                            If lobjCompany.ItemCompany(lintCount) Then
                                lstrCompanyname = lobjCompany.sCliename
                            Else
                                lstrCompanyname = String.Empty
                            End If
                        End If
                        'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lobjCompany = Nothing
                    Else
                        lstrCompanyname = String.Empty
                    End If

                    CompanyControl = Trim(CompanyControl) & "</TD><TD> <DIV ID='" & FieldCompanyName & "' CLASS=Field>" & lstrCompanyname & "</DIV>"
                End If
            End If
            CompanyControl = CompanyControl & "</TD></TR></TABLE>"
        Else
            If DefValue <> String.Empty Then
                lobjCompany = eRemoteDB.NetHelper.CreateClassInstance("eCoReinsuran.Company")
                If lobjCompany.insPreparedQuery(DefValue, String.Empty, String.Empty) Then
                    If lobjCompany.ItemCompany(lintCount) Then
                        lstrCompanyname = "</TD><TD>" & TextControl(FieldName & "Des", 30, lobjCompany.sCliename, isRequired, Alias_Renamed, GridField, HRefUrl, HRefScript, , Disabled)
                    End If
                End If
            End If
            If FieldCompanyName <> String.Empty Then
                If DefValue <> String.Empty Then
                    If Not isDIVDefine Then
                        CompanyControl = CompanyControl & "</TD><TD><DIV ID='" & FieldCompanyName & "' CLASS=Field>" & lobjCompany.sCliename & "</DIV>" & "</TD></TR></TABLE>"
                    Else
                        CompanyControl = CompanyControl & HiddenControl(Trim(FieldName) & "Name", lobjCompany.sCliename) & "</TD></TR></TABLE>"
                    End If
                Else
                    CompanyControl = CompanyControl & HiddenControl(Trim(FieldName) & "Name", String.Empty) & "</TD></TR></TABLE>"
                End If
            Else
                CompanyControl = CompanyControl & lstrCompanyname & "</TD></TR></TABLE>"
            End If
            'UPGRADE_NOTE: Object lobjCompany may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjCompany = Nothing
        End If
    End Function

    '%StyleSheet.Este método devuelve como resultado el "LINK" a la hoja de estilo
    Public Function StyleSheet() As String
        StyleSheet = "<LINK REL=""StyleSheet"" TYPE=""text/css"" HREF=""/VTimeNet/common/" & sStyleSheetName & ".css"">"
        StyleSheet = StyleSheet & vbCrLf & "    <SCRIPT>mstrSrvDecSep = '" & msServerDecimalSeparator & "'; " & "mstrUsrDecSep = '" & msUserDecimalSeparator & "'; </SCRIPT>"
        StyleSheet = StyleSheet & vbCrLf & "     <script src=""/VTimeNet/Scripts/jquery-1.9.1.js"" type=""text/javascript""></script>"
        'StyleSheet = StyleSheet & vbCrLf & "    <SCRIPT>$(document).ready(function(){$('input, textarea').each(function(i,v){$(v).change(new Function('ONCH_' + v.name, $(v).attr('onBlurCode') )) }) }); </SCRIPT>"
        StyleSheet = StyleSheet & vbCrLf & "    <SCRIPT>$(document).ready(function(){$('input, textarea').each(function(i,v){if($(v).attr('onBlurCode')>''){$(v).change(new Function('ONCH_' + v.name, $(v).attr('onBlurCode') )) }}) }); </SCRIPT>"
    End Function
    '**%Objective:
    '%Objetivo:

    Public ReadOnly Property sStyleSheetName() As String
        Get
            Dim lobjASPSupport As eRemoteDB.ASPSupport
            Dim lstrThemeVT As String
            Dim lstrStyleSheet As String

            On Error GoTo ErrorHandler

            lobjASPSupport = New eRemoteDB.ASPSupport()
            '+ Se obtiene el nombre del tema indicado en el FrontOffice, contenido en la variable de Session
            lstrThemeVT = lobjASPSupport.GetASPSessionValue("VT_Theme")
            lstrStyleSheet = String.Empty

            If lstrThemeVT = String.Empty Then
                '+ Se obtiene el nombre indicado en el WebConfig, como tema por defecto
                lstrThemeVT = ConfigurationManager.AppSettings("ThemeDefault")
            End If

            If lstrThemeVT = String.Empty Then
                '+ En caso que no se obtenga el nombre del tema, se asigna la hoja de estilo original
                lstrStyleSheet = "Custom"
            Else
                lstrStyleSheet = lstrThemeVT & "VT"
            End If

            sStyleSheetName = lstrStyleSheet
            lobjASPSupport = Nothing
            Exit Property
ErrorHandler:
            lobjASPSupport = Nothing
            ProcError("Values.sStyleSheetName()")
        End Get
    End Property


    '**% SysDateFormat: convert date to format of server
    '**%                This function receives a date in string format and returns a valid date.
    '**%                The date is received as a string and is in the format determined
    '**%                by the session variable SESSION_DATE_FORMAT.
    '**%                The date returned is in the format determined by the session
    '**%                variable SERVER_DATE_FORMAT
    Public Function SysDateFormat(ByVal sDate As String) As Date
        '**+ Declare variables to process received date string
        Dim nYear As Short
        Dim nMonth As Short
        Dim nDay As Short

        '**+ Determine Year based on session format
        nYear = CShort(Val(Mid(sDate, InStr(msUserDateFormat, "Y"), 4)))

        '**+ Determine Month or Day based on session format
        nMonth = CShort(Val(Mid(sDate, InStr(msUserDateFormat, "M"), 2)))

        '**+ Determine Month or Day based on session format
        nDay = CShort(Val(Mid(sDate, InStr(msUserDateFormat, "D"), 2)))

        SysDateFormat = DateSerial(nYear, nMonth, nDay)
    End Function
    '% GetNumFem_Config: Devuelve el valor del tag NumFem del archivo de configuracion
    Public Function GetNumFem_Configxml() As String
        Dim clsConfig As New eRemoteDB.VisualTimeConfig


        GetNumFem_Configxml = UCase(clsConfig.LoadSetting("NumFem", String.Empty, "Version"))

        'UPGRADE_NOTE: Object clsConfig may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        clsConfig = Nothing
    End Function

    '**% SessionDateFormat: This sub procedure is used to get the Session_Date_Format and
    '**%                    Session_Date_Separator variables from ASP global.asa, Session_OnStart()
    '**%                    procedure, to be used by DateControl function
    Public Sub SessionDateFormat()

        Dim currentCultureInfo As CultureInfo = Thread.CurrentThread.CurrentCulture

        With currentCultureInfo.DateTimeFormat
            msUserDateFormat = .ShortDatePattern.ToUpper
            msUserDateSeparator = .DateSeparator
        End With
        With currentCultureInfo.NumberFormat
            msUserDecimalSeparator = .NumberDecimalSeparator
            msUserThousandSeparator = .NumberGroupSeparator
        End With

        msServerDecimalSeparator = msUserDecimalSeparator ' UCase(clsConfig.LoadSetting("Server_Decimal_Separator", ",", "Regional Settings"))

    End Sub

    '% Class_Initialize: se controla la apertura del objeto
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mstrDescriptWindows = String.Empty
        mstrCodispl = String.Empty
        mblnValid = False
        mblnIsValid = False
        mblnBlank = True
        mstrBlankDesc = String.Empty
        Opacity = 100
        setdate = New String("0", 2 - Len(CStr(VB.Day(Today)))) & CStr(VB.Day(Today)) & "/" & New String("0", 2 - Len(CStr(Month(Today)))) & CStr(Month(Today)) & "/" & New String("0", 4 - Len(CStr(Year(Today)))) & CStr(Year(Today))

        mstrBeginPageLink = "<A NAME=""BeginPage"">&nbsp;</A>"
        TypeOrder = ecbeOrder.Descript
        sQueryString = ""
        ClientRole = String.Empty
        meTypeList = ecbeTypeList.none
        EditRecordQuery = False

        sCodisplPage = "None"

        '+ Se inicializan las variables para el manejo de las fechas

        Call SessionDateFormat()

        Dim clsConfig As New eRemoteDB.VisualTimeConfig
        mstrCachePath = clsConfig.LoadSetting("Cache", "C:\Inetpub\wwwroot\VTimeNet\cache", "Paths")
        mblnCacheEnabled = (UCase(clsConfig.LoadSetting("CacheEnabled", "Yes", "Database")) = "YES")

        clsConfig = Nothing

        '    #If LOG Then
        'If Trim$(sSessionID) = String.Empty Then
        'Dim lclsASPSupport As eRemoteDB.ASPSupport
        'Set lclsASPSupport = New eRemoteDB.ASPSupport
        'Call lclsASPSupport.GetASPSessionValue("SessionID")
        'sSessionID = lclsASPSupport.SessionID
        'Set lclsASPSupport = Nothing
        'End If
        '#End If
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% getMessage: Obtiene el mensaje correspondiente de la tabla general que recibe
    '%             como parámetro.  Por defecto carga de Table563
    Public Function getMessage(ByVal nCode As Short, Optional ByVal sTable As String = "Table563") As String
        Dim lclsTable As Tables
        Dim lclsQuery As eRemoteDB.Query
        Dim lstrKeyField As String

#If Log Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Log did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Begin|Method|getMessage|" & CStr(nCode), sSessionID
#End If

        On Error GoTo getMessage_err

        lclsQuery = New eRemoteDB.Query

        '+ Se busca el campo de filtro

        lclsTable = New Tables
        lstrKeyField = lclsTable.SearchKeyField(sTable)
        'UPGRADE_NOTE: Object lclsTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTable = Nothing

        getMessage = String.Empty

        With lclsQuery
            If .OpenQuery(sTable, "sDescript", lstrKeyField & "=" & nCode) Then
                getMessage = .FieldToClass("sDescript")
                .CloseQuery()
            End If
        End With

getMessage_err:
        If Err.Number Then
            getMessage = String.Empty
        End If
        'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsQuery = Nothing
#If Log Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Log did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		eRemotedb.FileSupport.AddBufferToFile sSessionID & "|Finish|Method|getMessage|" & CStr(nCode), sSessionID
#End If
    End Function

    '% getListTable: Se genera una lista con los valores de una tabla para ser usada en el list Include/Exclude
    Public Function getListTable(ByVal lstrTable As String) As String
        Dim recTable As Tables
        Dim lstrList As String

        lstrList = String.Empty

        If lstrTable <> String.Empty Then
            recTable = New Tables

            If recTable.reaTable(lstrTable, String.Empty) Then
                With recTable
                    Do While Not .EOF
                        If (lstrList <> String.Empty) Then
                            lstrList = lstrList & ","
                        End If

                        lstrList = lstrList & .Fields(.KeyField)
                        .NextRecord()
                    Loop

                    .closeTable()
                End With
            End If

            'UPGRADE_NOTE: Object recTable may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            recTable = Nothing
        End If

        getListTable = lstrList
    End Function

    '**% StatusControl: call the functions for setting the status of the controls
    '% StatusControl: invoca la función que captura/asigna el estado de cada uno de los
    '%                controles de la página.
    Public Function StatusControl(ByVal bFirst As Boolean, ByVal nZone As Short, ByVal sWindowType As String) As String
        StatusControl = "StatusControl(" & IIf(bFirst, "true", "false") & ","
        If sWindowType = "PopUp" Then
            StatusControl = StatusControl & "2);"
        Else
            StatusControl = StatusControl & nZone & ");"
        End If
    End Function

    '%insReturnUserNumber: Retornar el valor de formateado
    Public Function insReturnUserNumber(ByVal ServerValue As Object, Optional ByVal ShowThousand As Boolean = False, Optional ByVal DecimalPlaces As Short = 0) As String
        insReturnUserNumber = insFormatField(CStr(ServerValue), ShowThousand, eTypeField.clngNumericValue, DecimalPlaces)
    End Function


    '% FIELDSET: define el código HTML para los "frames" a mostrar en la página
    Public Function FIELDSET(ByVal Id As Integer, ByVal sTitle As String) As String
        FIELDSET = "<FIELDSET>" & vbCrLf & "    <LEGEND CLASS=""HighLighted""><LABEL ID=" & Id & "><A NAME=""" & sTitle & """>" & sTitle & "</A></LABEL></LEGEND>"
    End Function

    '% closeFIELDSET: define el código HTML para cerrar los "frames" a mostrar en la página
    Public Function closeFIELDSET() As String
        closeFIELDSET = "</FIELDSET>"
    End Function

    '% BranchControl: Esta función se encarga de construir el código HTML para la construcción
    '%                del combo de los Ramos comerciales(Table10).
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function BranchControl(ByVal FieldName As String, ByVal Alias_Renamed As String, Optional ByVal DefValue As String = "", Optional ByVal FieldProduct As String = "valProduct", Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal Descript As String = "") As String
        BranchControl = JSBranchControl(FieldProduct, FieldName) & PossiblesValues(FieldName, "Table10", eValuesType.clngComboType, DefValue, , GridField, HRefUrl, HRefScript, , IIf(FieldProduct <> String.Empty, "InsChange" & FieldName & "(this.value);", String.Empty) & OnChange, Disabled, , Alias_Renamed, , TabIndex, , , Descript)
    End Function

    '% JSBranchControl: Construye el código JS relacionado a BranchControl
    Private Function JSBranchControl(ByVal FieldProduct As String, ByVal FieldName As String) As String
        If FieldProduct <> String.Empty Then
            JSBranchControl = "<SCRIPT>" & vbCrLf & "function InsChange" & FieldName & "(nBranch){" & vbCrLf & "    with(self.document.forms[" & nParentForm & "]){" & vbCrLf & "        if (typeof(" & FieldProduct & ") != 'undefined'){" & vbCrLf & "            " & FieldProduct & ".disabled=(nBranch=='0'||nBranch==''?true:false);" & vbCrLf & "            btn" & FieldProduct & ".disabled=" & FieldProduct & ".disabled;" & vbCrLf & "            " & FieldProduct & ".value = '';" & vbCrLf & "            UpdateDiv('" & FieldProduct & "Desc', '');" & vbCrLf & "            " & FieldProduct & ".Parameters.Param1.sValue = nBranch;" & vbCrLf & "        }" & vbCrLf & "    }" & vbCrLf & "}" & vbCrLf & "</SCRIPT>" & vbCrLf
        End If

    End Function

    '% ProductControl: Esta función se encarga de construir el código HTML para la construcción
    '%                 de la ventana de valores posibles de productos (TabProdMaster1).
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function ProductControl(ByVal FieldName As String, ByVal Alias_Renamed As String, Optional ByVal BranchValue As String = "", Optional ByVal ValuesType As eValuesType = eValuesType.clngWindowType, Optional ByVal Disabled As Boolean = True, Optional ByVal DefValue As String = "", Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal TabIndex As Short = 0, Optional ByVal ShowDescript As Boolean = True, Optional ByVal bAllowInvalid As Boolean = False, Optional ByVal ProdClass As eProdClass = eProdClass.clngAll, Optional ByVal Descript As String = "") As String
        Dim lstrTable As String

        Select Case ProdClass
            Case eProdClass.clngActiveLife
                lstrTable = "tabProdMaster2"
            Case eProdClass.clngAnnuitiesLife
                lstrTable = "tabProdMaster3"
            Case Else
                lstrTable = "tabProdMaster1"
        End Select

        Parameters.Add("nBranch", StringToType(BranchValue, eTypeData.etdInteger), Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbInteger, 0, 0, 10, Tables.eRmtDataAttrib.rdbParamNullable)
        ProductControl = PossiblesValues(FieldName, lstrTable, ValuesType, DefValue, True, GridField, HRefUrl, HRefScript, , OnChange, Disabled, 5, Alias_Renamed, eTypeCode.eNumeric, TabIndex, ShowDescript, bAllowInvalid, Descript)
    End Function

    '%UpdContent: Llama a la función JS que actualiza la imagen de la transacción
    Public Function UpdContent(ByVal sCodispl As String, ByVal sContent As String, Optional ByVal sIndex As String = "") As String
        UpdContent = "<SCRIPT>" & vbCrLf & "if (typeof(top.fraSequence)!='undefined'){" & vbCrLf & "    if (typeof(top.fraSequence.UpdContent)!='undefined')" & vbCrLf & "        top.fraSequence.UpdContent('" & sCodispl & sIndex & "','" & sContent & "');" & vbCrLf & "}" & vbCrLf & "else{" & vbCrLf & "    if (typeof(top.opener.top.fraSequence)!='undefined')" & vbCrLf & "        if (typeof(top.opener.top.fraSequence.UpdContent)!='undefined')" & vbCrLf & "            top.opener.top.fraSequence.UpdContent('" & sCodispl & sIndex & "','" & sContent & "');" & vbCrLf & "}" & vbCrLf & "</SCRIPT>"
    End Function

    '% ButtonHelp: imagen para invocar el funcional de la transacción
    Public Function ButtonHelp(ByVal sCodispl As String) As String
        If sCodispl = "MENU" Then
            ButtonHelp = AnimatedButtonControl("btnHelp", "/VTimeNet/Images/btnHelp.gif", HttpContext.GetGlobalResourceObject("BackOfficeResource", "HelpButtonTooltip"), , "ShowPopUp('/VTimeNet/Common/Help.aspx?sCodispl=" & sCodispl & "','Help',600,500,'Yes','Yes',50,20)")
        Else
            ButtonHelp = AnimatedButtonControl("btnAbout", "/VTimeNet/Images/btnAboutOn.png", HttpContext.GetGlobalResourceObject("BackOfficeResource", "HelpButtonFunctionalTooltip") & " (" & sCodispl & ")", , "ShowPopUp('/VTimeNet/Common/Help.aspx?sCodispl=" & sCodispl & "','Help',600,500,'Yes','Yes',50,20)")
        End If
    End Function

    '% SumTypeDate: Esta función se encarga de validar los campo tipo fecha
    Public Function SumTypeDate(ByVal sFieldTyp As String, ByVal nFieldDurat As Short, ByVal dFieldDate As Date) As Date
        SumTypeDate = DateAdd(sFieldTyp, nFieldDurat, dFieldDate)
    End Function

    '% CloseShowDefValues: Esta función se encarga de generar el código HTML para el final de las ShowDefValues
    Public Function CloseShowDefValues(ByVal sFrame As String) As String
        If sFrame <> "" Then
            CloseShowDefValues = "top.frames['" & sFrame & "'].UpdateDiv('lblWaitProcess','<BR>','');" & vbCrLf & "if (typeof(top.frames['" & sFrame & "'])!='undefined')" & vbCrLf & "    if (typeof(top.frames['" & sFrame & "'].mstrDoSubmit)!='undefined')" & vbCrLf & "        top.frames['" & sFrame & "'].mstrDoSubmit='1';" & vbCrLf & "window.close();self.document.location.href='/VTimeNet/Common/blank.htm';"
        Else
            CloseShowDefValues = "window.close();self.document.location.href='/VTimeNet/Common/blank.htm';"
        End If

    End Function


    '% insGetSetting: se toman los valore del registro
    Public Function insGetSetting(ByVal Name As String, ByVal DefValue As String, Optional ByVal Group As String = "") As String
        Dim lclsConfig As New eRemoteDB.VisualTimeConfig

        insGetSetting = lclsConfig.LoadSetting(Name, DefValue, Group)
        'UPGRADE_NOTE: Object lclsConfig may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsConfig = Nothing

    End Function

    '%GetUrl: Obtiene la dirección Url de una página cuando se recarga desde un grid
    '%        o un transacción con encabezo.
    Public Function GetUrl(ByVal sUrlType As eUrlType, ByVal bReload As Boolean, ByVal sCodisp As String, Optional ByVal sCodispl As String = "", Optional ByVal sOnSeq As String = "", Optional ByVal sContinue As String = "", Optional ByVal sAction As String = "", Optional ByVal sReloadIndex As String = "", Optional ByVal sMainAction As String = "", Optional ByVal sWindowDescript As String = "", Optional ByVal sWindowTy As String = "", Optional ByVal sQueryString As String = "", Optional ByVal sScript As String = "") As String
        Dim lstrUrl As String = ""

        If sUrlType = eUrlType.cstrGrid Then
            If sScript = String.Empty Then
                lstrUrl = "<SCRIPT>"
                If bReload Then
                    lstrUrl = lstrUrl & "window.close();opener."
                End If
                lstrUrl = lstrUrl & "top.opener.document.location.href='" & sCodispl & ".aspx?Reload=" & sContinue & "&ReloadAction=" & sAction & "&ReloadIndex=" & sReloadIndex & "&sCodispl=" & sCodispl & "&sCodisp=" & sCodisp & "&sOnSeq=" & sOnSeq & "&nMainAction=" & sMainAction & "&sWindowDescript=" & sWindowDescript & "&nWindowTy=" & sWindowTy & sQueryString & "'</SCRIPT>"
            Else
                lstrUrl = sScript
            End If
        End If
        GetUrl = lstrUrl
    End Function

    Private Function getCacheDescript(ByVal FieldName As String, ByVal TableName As String, ByVal DefValue As String) As String
        Dim lstrFilename As String
        Dim lstrBuffer As String
        Dim lstrKey As String
        Dim llngBegin As Integer
        Dim llngFinish As Integer

        lstrFilename = mstrCachePath & "\Tables\" & TableName & "_" & sCodisplPage & "_" & FieldName & "_<TYPE>_Edit"
        If meTypeList <> ecbeTypeList.none Then
            If meTypeList = ecbeTypeList.Inclution Then
                lstrFilename = lstrFilename & "_Inc"
            Else
                lstrFilename = lstrFilename & "_Exc"
            End If

            lstrFilename = lstrFilename & Replace(mstrList, ",", String.Empty)
        End If
        lstrFilename = lstrFilename & "_" & Threading.Thread.CurrentThread.CurrentCulture.Name & ".htm"
        getCacheDescript = String.Empty
        lstrBuffer = eRemoteDB.FileSupport.LoadFileToText(Replace(lstrFilename, "<TYPE>", "Enable"))
        If lstrBuffer = String.Empty Then
            lstrBuffer = eRemoteDB.FileSupport.LoadFileToText(Replace(lstrFilename, "<TYPE>", "Disable"))
        End If
        If lstrBuffer <> String.Empty Then
            lstrKey = "<OPTION VALUE=""" & DefValue & """>"
            llngBegin = InStr(lstrBuffer, lstrKey)
            If llngBegin > 0 Then
                llngBegin = llngBegin + Len(lstrKey)
                llngFinish = InStr(llngBegin, lstrBuffer, "</OPTION>")
                getCacheDescript = Mid(lstrBuffer, llngBegin, llngFinish - llngBegin)
            End If
        End If
    End Function

    '% PolicyControl: Esta función se encarga de construir el código HTML para la construcción
    '%                del campo Cotización/Propuesta/Póliza
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function PolicyControl(ByVal FieldName As String, ByVal Alias_Renamed As String, ByVal FieldBranch As String, ByVal BranchValue As String, ByVal FieldProduct As String, ByVal ProductValue As String, Optional ByVal CertypeQuery As String = "", Optional ByVal DefValue As String = "", Optional ByVal FieldCertif As String = "", Optional ByVal CertifValue As String = "0", Optional ByVal GridField As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal bShowQuery As Boolean = True) As String
        Dim lintDigit As Short
        Dim lstrCertype As String
        Dim lstrOnChange As String
        Dim lobjCertificat As Object

        lintDigit = 0

        '+ Si el tipo de registro posible en la consulta de póliza no se indica, se asume
        '+ "Póliza" para la búsqueda del dígito verificador
        lstrCertype = IIf(CertypeQuery = String.Empty, "2", CertypeQuery)

        '+ Arma la estructura del evento OnChange del campo en donde se coloca en Nro. de póliza
        lstrOnChange = "if($(""#" & FieldName & "_Old"").val()!=this.value" & "){" & "$(self.document.forms[" & nParentForm & "]." & FieldName & "_Old).val(this.value);" & OnChange & "}"
        'lstrOnChange = "if(" & FieldName & "_Old.value!=" & FieldName & ".value" & "){" & "self.document.forms[" & nParentForm & "]." & FieldName & "_Old.value=self.document.forms[" & nParentForm & "]." & FieldName & ".value;" & OnChange & "}"

        If DefValue <> String.Empty Then
            '+ Se busca el dígito verificador asociado a la póliza/certificado
            lobjCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
            If lobjCertificat.Find(lstrCertype, BranchValue, ProductValue, DefValue, CertifValue) Then
                lintDigit = lobjCertificat.nDigit
            End If
        End If

        PolicyControl = NumericControl(FieldName, 10, DefValue, False, Alias_Renamed, False, 0, GridField, HRefUrl, HRefScript, lstrOnChange, Disabled, TabIndex, False, False) & "<LABEL>-</LABEL>" & NumericControl(FieldName & "_Digit", 1, CStr(lintDigit), False, Alias_Renamed & " (Dígito)", False, 0, GridField, String.Empty, String.Empty, String.Empty, True)

        If Not ActionQuery And Not GridField Then
            PolicyControl = PolicyControl & HiddenControl(FieldName & "_Old", DefValue) & vbCrLf


            If bShowQuery Then
                PolicyControl = PolicyControl & AnimatedButtonControl("btn" & FieldName, "/VTimeNet/Images/btn_ValuesOff.png", Alias_Renamed & " (Valores posibles)", String.Empty, "insShowPolicyQuery" & FieldName & "(""" & FieldName & """,""" & FieldBranch & """,""" & FieldProduct & """,""" & FieldCertif & """,self.document.forms[" & nParentForm & "]." & FieldBranch & ".value,self.document.forms[" & nParentForm & "]." & FieldProduct & ".value,'" & TypeList & "','" & List & "')", Disabled, TabIndex) & vbCrLf

                '+ Código para mostrar los valores posibles de póliza (GE010)
                PolicyControl = PolicyControl & "<SCRIPT>" & vbCrLf & "self.document.forms[" & nParentForm & "].elements['" & FieldName & "'].CertypeQuery=" & IIf(CertypeQuery = String.Empty, "0", CertypeQuery) & ";" & vbCrLf & "function insShowPolicyQuery" & FieldName & "(FieldName, FieldBranch, FieldProduct, FieldCertif, BranchValue, ProductValue, TypeList, List){" & vbCrLf & "ShowPopUp('/VTimeNet/Common/PopUp.aspx?Type=PopUp&sPageName=PolicyQuery&FieldPolicy=' + FieldName + '&FieldBranch=' + FieldBranch + '&FieldProduct=' + FieldProduct + '&FieldCertif=' + FieldCertif + '&nBranch=' + BranchValue + '&nProduct=' + ProductValue + '&TypeList=' + TypeList + '&List=' + List + '&sCertypeQuery=' + self.document.forms[" & nParentForm & "]." & FieldName & ".CertypeQuery, 'ControldePoliza', 750 , 510, 'no', 'no', 20, 20)" & vbCrLf & "}" & vbCrLf & "</SCRIPT>"
            End If
        End If
        'UPGRADE_NOTE: Object lobjCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjCertificat = Nothing
    End Function

    '% DelCache: Esta función se encarga de borrar la información de cache relacionada al proceso
    Public Function DelCache(ByVal nDirectory As Short, ByVal sFile As String, Optional ByVal sMenu As String = "", Optional ByVal nInsur_Area As Integer = 0) As String
        Dim lstrFilename As String
        Dim lstrssche_code As String
        Dim lobjSecur_sche As eRemoteDB.Query
        On Error Resume Next

        lstrFilename = mstrCachePath

        Select Case nDirectory
            Case 1
                lstrFilename = lstrFilename & "\" & sFile & "_*"
            Case 2
                lstrFilename = lstrFilename & "\Tables\"
                Dim folder As New DirectoryInfo(lstrFilename)
                For Each file As FileInfo In folder.GetFiles()
                    If UCase(file.Name) Like UCase("*" & sFile & "*") Then
                        If nInsur_Area = "1" Then
                            If file.Name Like "2_*" Then
                                Kill(file.FullName)
                            End If
                        Else
                            If file.Name Like "1_*" Then
                                Kill(file.FullName)
                            End If
                        End If
                    End If
                Next
            Case 3
                lstrFilename = lstrFilename & "\Messages\" & sFile & "*"
            Case 4
                lobjSecur_sche = New eRemoteDB.Query
                With lobjSecur_sche
                    If .OpenQuery("secur_sche", "ssche_code") Then
                        lstrssche_code = .FieldToClass("ssche_code")
                        Do
                            Kill(lstrFilename & "\" & lstrssche_code & "_" & sFile & "*")
                            Kill(lstrFilename & "\" & lstrssche_code & "_" & sMenu & "*")
                            .NextRecord()
                            If Not .EndQuery Then
                                lstrssche_code = .FieldToClass("ssche_code")
                            Else
                                lstrssche_code = String.Empty
                            End If
                        Loop Until lstrssche_code = String.Empty
                    End If
                End With
        End Select

        If nDirectory <> 4 And nDirectory <> 2 Then
            Kill(lstrFilename)
        End If

        'UPGRADE_NOTE: Object lobjSecur_sche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjSecur_sche = Nothing
        On Error GoTo 0

    End Function
    Public Shared Function VTReplace(ByVal soriginal As Object, ByVal sFind As String, ByVal sREplace As String) As String
        If soriginal Is Nothing Then
            Return vbNullString
        Else
            Return Replace(soriginal, sFind, sREplace)
        End If
    End Function


    Public Shared Function GetMessage(ByVal nErrorNum As Integer) As String
        Dim result As String = String.Empty
        Try
            With New eRemoteDB.Query
                If .OpenQuery("Message", "sMessaged", String.Format("nErrorNum = {0} and sStatregt ='1'", nErrorNum)) Then
                    result = .FieldToClass("sMessaged")
                    .CloseQuery()
                End If
            End With
            Return result
        Catch ex As Exception
            Return result
        End Try
    End Function


    '% getMessage: Obtiene el mensaje correspondiente de la tabla general que recibe
    '%             como parámetro.  Por defecto carga de InternalMsg
    Public Shared Function getInternalMsg(ByVal nCode As Short, Optional ByVal sTable As String = "InternalMsg") As String
        Dim lclsTable As Tables
        Dim lclsQuery As eRemoteDB.Query
        Dim lstrKeyField As String

        lclsQuery = New eRemoteDB.Query

        '+ Se busca el campo de filtro

        lclsTable = New Tables
        lstrKeyField = lclsTable.SearchKeyField(sTable)
        lclsTable = Nothing

        getInternalMsg = String.Empty

        With lclsQuery
            If .OpenQuery(sTable, "sDescript", lstrKeyField & "=" & nCode) Then
                getInternalMsg = .FieldToClass("sDescript")
                .CloseQuery()
            Else
                getInternalMsg = sTable & "-" & nCode & "(X)"
            End If
        End With

        lclsQuery = Nothing
        lclsTable = Nothing

        Exit Function
    End Function
    '% GetResxValue: Obtiene todo los valores del archivo de recursos (.resx) a partir del sCodispl
    Public Shared Function GetResxValue(ByVal sCodispl As String, Optional ByVal bIsHeader As Boolean = False, Optional sFolderNameParameter As String = Nothing, Optional sEXENameParameter As String = Nothing) As IEnumerable(Of DictionaryEntry)

        Dim lclsQuery As eRemoteDB.Query
        Dim lstrLang As String
        Dim lstrWebApplicationPath As String
        Dim lstrbackOfficePath As String
        Dim lstrPath As String
        Dim enumerator As IEnumerable(Of DictionaryEntry)

        Try
            '+ Idioma actual
            lstrLang = System.Configuration.ConfigurationManager.AppSettings("DefaultLanguage").ToString()

            '+ Ruta fisica de la aplicación
            lstrWebApplicationPath = System.Configuration.ConfigurationManager.AppSettings("WebApplicationPath").ToString()

            '+ Directorio fisico de la aplicación
            lstrbackOfficePath = System.Configuration.ConfigurationManager.AppSettings("BackOfficePath").ToString()

            lclsQuery = New eRemoteDB.Query

            Dim sTable As String = "WINDOWS W, TAB_SYS_EXE T"
            Dim sFields As String = "T.SFOLDERNAME, T.SEXE_NAME"
            Dim sCondition As String = String.Format("W.SCODISPL = '{0}' AND W.SSTATREGT = '1' AND W.NMODULES = T.NEXE_CODE", sCodispl)


            If String.IsNullOrEmpty(sFolderNameParameter) OrElse String.IsNullOrEmpty(sEXENameParameter) Then
                '+ Se obtiene el directorio fisico donde se encuentra el archivo de recurso
                With lclsQuery
                    .OpenQuery(sTable, sFields, sCondition)
                    lstrPath = System.IO.Path.Combine(lstrWebApplicationPath, lstrbackOfficePath, .FieldToClass("sFoldername"), .FieldToClass("sExe_name"))
                    .CloseQuery()
                End With
            Else
                lstrPath = System.IO.Path.Combine(lstrWebApplicationPath, lstrbackOfficePath, sFolderNameParameter, sEXENameParameter)
            End If

            Dim fileName As String = String.Format("{0}\App_LocalResources\{1}{2}.aspx.{3}.resx", lstrPath, sCodispl, IIf(bIsHeader, "_K", String.Empty), lstrLang)

            '+ Obtiene todos los valores en el archivo de recursos
            Using reader As New Resources.ResXResourceReader(fileName)
                enumerator = reader.OfType(Of DictionaryEntry)()
            End Using

            Return enumerator

        Catch ex As Exception
            Throw New Exception("GetResxValue: " & ex.Message)
        End Try

    End Function

End Class