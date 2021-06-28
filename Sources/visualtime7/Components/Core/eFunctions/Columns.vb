Option Strict Off
Option Explicit On

Imports System.Web

Public Class Columns
    Implements System.Collections.IEnumerable

    Private mCol As Collection
    Private mblnCheck As Boolean
    Private mblnArrayNamed As Boolean

    '**- Defines the property to assign the array's name associated to the grid.
    '- Se define la propiedad para asignar el nombre del arreglo asociado al grid

    Private mstrArrayName As String

    '-Variable que guarda el número de sesión
    Public sSessionID As String

    '-Código del usuario
    Public nUsercode As Integer

    '**%AddCheckColumn: This method creates a "check box" column in the array of columns that belongs
    '**%to the grid.
    '%AddCheckColumn: Este metodo se encarga de crear una columna tipo "check" al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddCheckColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal Descript As String, Optional ByVal Checked As Short = 2, Optional ByVal DefValue As String = "1", Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal Alias_Renamed As String = "") As Column
        Dim objNewMember As Column

        If Not mblnCheck Or Trim(FieldName) <> "Sel" Then
            Call insAddCheckColumn()
        Else
            If Not mblnCheck Then
                Return Nothing
            Else
                mblnCheck = False
            End If
        End If

        objNewMember = New Column
        With objNewMember

            '**+ CheckControl type
            '+Tipo CheckControl

            .Title = Title
            .ControlType = 1
            .FieldName = FieldName
            .Descript = Descript
            .Checked = Checked
            .DefValue = DefValue
            .OnClick = OnClick
            .Disabled = Disabled
            .Alias_Renamed = Alias_Renamed
        End With

        mCol.Add(objNewMember, FieldName)

        AddCheckColumn = objNewMember
    End Function

    '**%AddNumericColumn: This method creates a "numeric control" column in the array of columns that belongs
    '**%to the grid.
    '%AddNumericColumn: Este metodo se encarga de crear una columna tipo numerico al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddNumericColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal Length As Short, Optional ByVal DefValue As String = "", Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal ShowThousand As Boolean = False, Optional ByVal DecimalPlaces As Short = 0, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal bAllowNegativ As Boolean = False) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Numeric type
            '+Tipo Numérico

            .Title = Title
            .ControlType = 2
            .FieldName = FieldName
            .Length = Length
            .DefValue = DefValue
            .isRequired = isRequired
            .Alias_Renamed = Alias_Renamed
            .ShowThousand = ShowThousand
            .DecimalPlaces = DecimalPlaces
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .OnChange = OnChange
            .Disabled = Disabled
            .TabIndex = TabIndex
            .bAllowNegativ = bAllowNegativ
        End With

        mCol.Add(objNewMember, FieldName)

        AddNumericColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '**%AddNumericColumn: This method creates a "Text Control" column in the array of columns that belongs
    '**%to the grid.
    '%AddTextColumn: Este metodo se encarga de crear una columna tipo texto al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddTextColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal Length As Short, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Numeric type
            '+Tipo Numérico

            .Title = Title
            .ControlType = 3
            .FieldName = FieldName
            .Length = Length
            .DefValue = DefValue
            .isRequired = isRequired
            .Alias_Renamed = Alias_Renamed
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .OnChange = OnChange
            .Disabled = Disabled
            .TabIndex = TabIndex
        End With

        mCol.Add(objNewMember, FieldName)

        AddTextColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '**%AddTextAreaColumn: This method creates a "Text Area" column in the array of columns that belongs
    '**%to the grid.
    '%AddTextAreaColumn: Este metodo se encarga de crear una columna tipo area texto al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddTextAreaColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal DefValue As String, ByVal Rows As Short, ByVal Cols As Short, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Numeric type
            '+Tipo Numérico

            .Title = Title
            .ControlType = 10
            .FieldName = FieldName
            .DefValue = DefValue
            .Rows = Rows
            .Cols = Cols
            .isRequired = isRequired
            .Alias_Renamed = Alias_Renamed
            .Disabled = Disabled
            .TabIndex = TabIndex
        End With

        mCol.Add(objNewMember, FieldName)

        AddTextAreaColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '**%AddFileColumn: This method creates a "File" column in the array of columns that belongs
    '**%to the grid.
    '% AddFileColumn: añade una columna para controles de tipo FILE
    Public Function AddFileColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, Optional ByVal Length As Short = 30, Optional ByVal OnClick As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal OnChange As String = "") As Column
        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ File type
            '+Tipo Archivo

            .Title = Title
            .ControlType = 11
            .FieldName = FieldName
            .Length = Length
            .OnClick = OnClick
            .Disabled = Disabled
            .TabIndex = TabIndex
            .OnChange = OnChange
        End With

        mCol.Add(objNewMember, FieldName)

        AddFileColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '**%AddAnimatedColumn: This method creates a "Animated control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddAnimatedColumn: Este metodo se encarga de crear una columna tipo boton-animado al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddAnimatedColumn(ByVal Id As Integer, ByVal Title As String, ByVal ButtonName As String, Optional ByVal Src As String = "", Optional ByVal Alias_Renamed As String = "", Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Animated type button
            '+Tipo Boton animado

            .Title = Title
            .ControlType = 4
            .FieldName = ButtonName
            .Src = Src
            .Alias_Renamed = Alias_Renamed
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .Disabled = Disabled
            .TabIndex = TabIndex
        End With

        mCol.Add(objNewMember, ButtonName)

        AddAnimatedColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%AddHiddenColumn: This method creates a "Hide control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddHiddenColumn: Este metodo se encarga de crear una columna oculta al
    '%arreglo de columnas del grid
    Public Function AddHiddenColumn(ByVal FieldName As String, ByVal DefValue As String) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Hidden type
            '+Tipo Oculto

            .ControlType = 5
            .FieldName = FieldName
            .DefValue = DefValue
        End With

        mCol.Add(objNewMember, FieldName)

        AddHiddenColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%AddDateColumn: This method creates a "Date control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddDateColumn: Este metodo se encarga de crear una columna tipo fecha al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddDateColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, Optional ByVal DefValue As String = "", Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Hidden type
            '+Tipo Oculto

            .ControlType = 6
            .Title = Title
            .FieldName = FieldName
            .DefValue = DefValue
            .isRequired = isRequired
            .Alias_Renamed = Alias_Renamed
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .OnChange = OnChange
            .Disabled = Disabled
            .TabIndex = TabIndex
        End With

        mCol.Add(objNewMember, FieldName)

        AddDateColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%AddPossiblesColumn: This method creates a "Combo control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddPossiblesColumn: Este metodo se encarga de crear una columna tipo "Valores Posibles" al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddPossiblesColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal TableName As String, ByVal ValuesType As Values.eValuesType, Optional ByVal DefValue As String = "", Optional ByVal NeedParam As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal ComboSize As Short = 1, Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal MaxLength As Short = 5, Optional ByVal Alias_Renamed As String = "", Optional ByVal CodeType As Values.eTypeCode = Values.eTypeCode.eNumeric, Optional ByVal TabIndex As Short = 0, Optional ByVal bAllowInvalid As Boolean = False, Optional ByVal ShowDescript As Boolean = True, Optional ByVal Descript As String = "", Optional ByVal NotCache As Boolean = False, Optional ByVal KeyField As String = "") As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember
            '**+ Combo box type
            '+Tipo Combo
            .ControlType = 7
            .Title = Title
            .FieldName = FieldName
            .TableName = TableName
            .ValuesType = ValuesType
            .DefValue = DefValue
            .NeedParam = NeedParam
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .ComboSize = ComboSize
            .OnChange = OnChange
            .Disabled = Disabled
            .MaxLength = MaxLength
            .Alias_Renamed = Alias_Renamed
            .CodeType = CodeType
            .TabIndex = TabIndex
            .bAllowInvalid = bAllowInvalid
            .ShowDescript = ShowDescript
            .Descript = Descript
            .NotCache = NotCache
            .KeyField = KeyField
            Call AddHiddenColumn(FieldName & "Val", DefValue)
        End With

        mCol.Add(objNewMember, FieldName)

        AddPossiblesColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%AddClientColumn: This method creates a "Combo control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddClientColumn: Este metodo se encarga de crear una columna tipo "Valores Posibles" al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddClientColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal FieldClieName As String = "", Optional ByVal isDIVDefine As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal Separate As Boolean = True, Optional ByVal TabIndex As Short = 0, Optional ByVal CreateClient As Boolean = False, Optional ByVal sQueryString As String = "", Optional ByVal bAllowInvalid As Boolean = False, Optional ByVal nTypeForm As Values.eTypeClient = Values.eTypeClient.SearchClient, Optional ByVal Cliename As String = "", Optional ByVal Digit As String = "", Optional ByVal CustomPage As String = "", Optional ByVal bAllowInvalidFormat As Boolean = False) As Column
        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '+ Crea el nombre del DIV para desplegar el nombre del cliente.
            If FieldClieName = String.Empty Then
                FieldClieName = FieldName & "_Name"
            End If

            '**+ Client type
            '+Tipo Cliente
            .ControlType = 8
            .Title = Title
            .FieldName = FieldName
            .DefValue = DefValue
            .isRequired = isRequired
            .Alias_Renamed = Alias_Renamed
            .OnChange = OnChange
            .Disabled = Disabled
            .FieldClieName = FieldClieName
            .isDIVDefine = isDIVDefine
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .Separate = Separate
            .TabIndex = TabIndex
            .CreateClient = CreateClient
            .sQueryStringClient = sQueryString
            .bAllowInvalid = bAllowInvalid
            .nTypeForm = nTypeForm
            .Descript = Cliename
            .Digit = Digit
            .CustomPage = CustomPage
            .bAllowInvalidFormat = bAllowInvalidFormat
        End With

        mCol.Add(objNewMember, FieldName)

        AddClientColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%AddButtonColumn: This method creates a "Button control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddButtonColumn. Este método se encarga de crear una columna para las
    '%notas
    Public Function AddButtonColumn(ByVal Id As Integer, ByVal Title As String, ByVal sCodispl As String, ByVal nNotenum As Double, Optional ByVal ShowSmallImage As Boolean = True, Optional ByVal bQuery As Boolean = True, Optional ByVal nIndexNotenum As Double = 0, Optional ByVal nOriginalNotenum As Double = 0, Optional ByVal nCopyNotenum As Double = 0, Optional ByVal TabIndex As Short = 0, Optional ByVal sFieldName As String = "", Optional ByVal Disabled As Boolean = False) As Column
        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Notes type
            '+Tipo Notas

            .ControlType = 9
            .Title = Title
            .FieldName = IIf(sFieldName = String.Empty, "btnNotenum", sFieldName)
            .sCodispl = sCodispl
            .nNotenum = nNotenum
            .DefValue = CStr(.nNotenum)
            .ShowSmallImage = ShowSmallImage
            .bQuery = bQuery
            .nIndexNotenum = nIndexNotenum
            .nOriginalNotenum = nOriginalNotenum
            .nCopyNotenum = nCopyNotenum
            .TabIndex = TabIndex
            .Disabled = Disabled
        End With

        mCol.Add(objNewMember, objNewMember.FieldName)

        AddButtonColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function

    '**%AddCompanyColumn: This method creates a "Company control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddCompanyColumn. Este método se encarga de crear una columna para las compañías
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddCompanyColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal DefValue As String, Optional ByVal isRequired As Boolean = False, Optional ByVal Alias_Renamed As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal FieldCompanyName As String = "", Optional ByVal isDIVDefine As Boolean = False, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal Separate As Boolean = False, Optional ByVal TabIndex As Short = 0) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember

            '**+ Company type
            '+Tipo Compañía

            .ControlType = 12
            .Title = Title
            .FieldName = FieldName
            .DefValue = DefValue
            .isRequired = isRequired
            .Alias_Renamed = Alias_Renamed
            .OnChange = OnChange
            .Disabled = Disabled
            .FieldCompanyName = FieldCompanyName
            .isDIVDefine = isDIVDefine
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .Separate = Separate
            .TabIndex = TabIndex
        End With

        mCol.Add(objNewMember, FieldName)

        AddCompanyColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%AddAssociateColumn: This method creates a "associate control" columns in the array of columns that belongs
    '**%to the grid.
    '%AddAssociateColumn: Este metodo se encarga de crear una columna para consultas asociadas al
    '%arreglo de columnas del grid
    Public Function AddAssociateColumn(ByVal Id As Integer, ByVal Title As String, ByVal ButtonName As String, ByVal nKeynum As Short, Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0) As Column

        Dim objNewMember As Column
        objNewMember = New Column
        With objNewMember

            '**+ Animated type button
            '+Tipo Boton animado
            .nKeynum = nKeynum
            .Title = Title
            .ControlType = 13
            .FieldName = ButtonName
            .Disabled = Disabled
            .TabIndex = TabIndex
        End With

        mCol.Add(objNewMember, ButtonName)

        AddAssociateColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '%AddBranchColumn: Este metodo se encarga de crear una columna tipo combo de los
    '%                 Ramos comerciales(Table10)
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddBranchColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal Alias_Renamed As String, Optional ByVal FieldProduct As String = "valProduct", Optional ByVal DefValue As String = "", Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False, Optional ByVal TabIndex As Short = 0, Optional ByVal Descript As String = "") As Column
        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember
            '**+ Combo box type (Table10)
            '+Tipo Combo (Table10)
            .ControlType = 14
            .Title = Title
            .FieldName = FieldName
            .TableName = "Table10"
            .ValuesType = Values.eValuesType.clngComboType
            .DefValue = DefValue
            .NeedParam = False
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .ComboSize = 1
            .OnChange = OnChange
            .Disabled = Disabled
            .MaxLength = 5
            .Alias_Renamed = Alias_Renamed
            .CodeType = Values.eTypeCode.eNumeric
            .TabIndex = TabIndex
            .FieldProduct = FieldProduct
            .Descript = Descript
            Call AddHiddenColumn(FieldName & "Val", DefValue)
        End With

        mCol.Add(objNewMember, FieldName)

        AddBranchColumn = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '% ProductControl: Esta función se encarga de construir el código HTML para la construcción
    '%                 de la ventana de valores posibles de productos (TabProdMaster1).
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddProductColumn(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal Alias_Renamed As String, Optional ByVal FieldBranch As String = "cbeBranch", Optional ByVal DefValue As String = "", Optional ByVal MaxLength As Short = 5, Optional ByVal HRefUrl As String = "", Optional ByVal HRefScript As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = True, Optional ByVal TabIndex As Short = 0, Optional ByVal ProdClass As Values.eProdClass = Values.eProdClass.clngAll, Optional ByVal Descript As String = "") As Column
        Dim objNewMember As Column
        Dim lstrTable As String

        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember
            .ControlType = 15
            .Title = Title
            .FieldName = FieldName
            Select Case ProdClass
                Case Values.eProdClass.clngActiveLife
                    lstrTable = "tabProdMaster2"
                Case Values.eProdClass.clngAnnuitiesLife
                    lstrTable = "tabProdMaster3"
                Case Else
                    lstrTable = "tabProdMaster1"
            End Select
            .TableName = lstrTable
            .ValuesType = Values.eValuesType.clngWindowType
            .DefValue = DefValue
            .NeedParam = True
            .HRefUrl = HRefUrl
            .HRefScript = HRefScript
            .ComboSize = 1
            .OnChange = OnChange
            .Disabled = Disabled
            .MaxLength = MaxLength
            .Alias_Renamed = Alias_Renamed
            .CodeType = Values.eTypeCode.eNumeric
            .TabIndex = TabIndex
            .FieldBranch = FieldBranch
            .ProdClass = ProdClass
            .Descript = Descript
            Call AddHiddenColumn(FieldName & "Val", DefValue)
        End With

        mCol.Add(objNewMember, FieldName)

        AddProductColumn = objNewMember
        AddProductColumn.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, Parameter.eRmtDataDir.rdbParamInput, Parameter.eRmtDataType.rdbInteger, 0, 0, 10, Tables.eRmtDataAttrib.rdbParamNullable)

        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function


    'AddComboControl: Este metodo se encarga de crear una columna tipo "Combo Control" al
    '%arreglo de columnas del grid
    'UPGRADE_NOTE: Alias was upgraded to Alias_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Function AddComboControl(ByVal Id As Integer, ByVal Title As String, ByVal FieldName As String, ByVal sListCSV As String, Optional ByVal sDefValue As String = "", Optional ByVal bBlankPosition As Boolean = True, Optional ByVal TabIndex As Short = 0, Optional ByVal Alias_Renamed As String = "", Optional ByVal OnChange As String = "", Optional ByVal Disabled As Boolean = False) As Column

        Dim objNewMember As Column
        Call insAddCheckColumn()
        objNewMember = New Column
        With objNewMember
            '**+ Combo box type
            '+Tipo Combo
            .ControlType = 16
            .Title = Title
            .FieldName = FieldName
            .List = sListCSV
            .DefValue = sDefValue
            .BlankPosition = bBlankPosition
            .TabIndex = TabIndex
            .Alias_Renamed = Alias_Renamed
            .OnChange = OnChange
            .Disabled = Disabled
            Call AddHiddenColumn(FieldName & "Val", sDefValue)
        End With

        mCol.Add(objNewMember, FieldName)

        AddComboControl = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing
    End Function

    '**%insAddCheckColumn: This function creates the "SEL" column in the array of columns that belongs
    '**%to the grid.
    '%insAddCheckColumn: esta funcion se encarga de generar la columna SEL del grid
    Private Sub insAddCheckColumn()
        If mblnCheck Then
            mblnCheck = False
            Call AddCheckColumn(0, HttpContext.GetGlobalResourceObject("BackOfficeResource", "SelectColumnCaption"), "Sel", String.Empty, 2, , "MarkRecord" & IIf(mblnArrayNamed, mstrArrayName, String.Empty) & "(this)", False)
        End If
    End Sub

'AddComboControl: Este metodo se encarga de crear una columna tipo "HTML" al
'%arreglo de columnas del grid
'--------------------------------------------------------------------------------------------
    Public Function AddHTMLColumn(ByVal Id As Long, _
                                  ByVal Title As String, _
                                  ByVal FieldName As String) As Column
    '--------------------------------------------------------------------------------------------
    
        Dim objNewMember As Column
        Call insAddCheckColumn
        objNewMember = New Column
        With objNewMember
    '**+ Columna HTML
    '+ Columna HTML
            .ControlType = 20
            .Title = Title
            .FieldName = FieldName
        End With

        mCol.Add(objNewMember, FieldName)

        AddHTMLColumn = objNewMember
        objNewMember = Nothing
    End Function


    '**% sArrayName: assigne the name of the arrengement associated to the grid.
    '% sArrayName: se asigna el nombre del arreglo asociado al grid

    '**% sArrayName: take the name of the associated arrengement to the grid
    '% sArrayName: se toma el nombre del arreglo asociado al grid
    Public Property sArrayName() As String
        Get
            sArrayName = mstrArrayName
        End Get
        Set(ByVal Value As String)
            mstrArrayName = Value
            If mstrArrayName <> "marrArray" Then
                mblnArrayNamed = True
            End If
        End Set
    End Property

    '*** Item: Restores an element of the collection (according to the index)
    '* Item: Devuelve un elemento de la colección (segun índice)
    '-----------------------------------------------------------
    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Column
        Get
            '-----------------------------------------------------------
            Item = mCol.Item(vntIndexKey)
        End Get
    End Property

    '*** Count: Restores the number of elements that the collection owns
    '* Count: Devuelve el número de elementos que posee la colección
    Public ReadOnly Property Count() As Integer
        Get
            Count = mCol.Count()
        End Get
    End Property

    '*** NewEnum: Allows to enumerate the collection for using it in a cycle For Each... Next
    '* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'NewEnum = mCol._NewEnum
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
        GetEnumerator = mCol.GetEnumerator
    End Function

    '**% Remove: Removes an element froma the collection.
    '% Remove: Elimina un elemento de la colección
    '---------------------------------------------
    Public Sub Remove(ByRef vntIndexKey As Object)
        '---------------------------------------------
        mCol.Remove(vntIndexKey)
    End Sub

    '**% Class_Initialize: Controls the creation of an instance of the collection.
    '% Class_Initialize: Controla la creación de una instancia de la colección
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        mCol = New Collection
        mblnCheck = True
        mblnArrayNamed = False
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**% Class_Terminate. controls the delete of an instance of the collection.
    '% Class_Terminate: Controla la destrucción de una instancia de la colección
    'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Terminate_Renamed()
        'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mCol = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class






