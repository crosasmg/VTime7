Option Strict Off
Option Explicit On
Public Class Tab_typErrs
    Implements System.Collections.IEnumerable
    '**+Objective: Collection that supports the class 'Tab_typerr'.
    '**+Version: $$Revision: 5 $
    '+Objetivo: Colección que le da soporte a la clase 'Tab_typerr'.
    '+Version: $$Revision: 5 $

    '**-Objective: Local variable to hold collection.
    '-Objetivo: Variable Local para almacenar la colección.
    Private mcolTab_typerr As Collection



    '**%Objective: Adds an element to the collection.
    '**%Parameters:
    '**%    lclsTab_typerr -
    '%Objetivo: Este método permite agregar un elemento a la colección.
    '%Parámetros:
    '%    lclsTab_typerr -
    Public Function Add(ByRef lclsTab_typerr As TAB_TYPERR) As TAB_TYPERR

        '**+ The properties passed to the method are assigned to the collection.
        '+ Las propiedades pasadas al método son asignadas a la colección.
        If Not IsIDEMode() Then
        End If

        mcolTab_typerr.Add(lclsTab_typerr)

        '**+Returns the object created.
        '+ Retorna el objeto creado.

        Add = lclsTab_typerr

        Exit Function
    End Function

    '**%Objective: Searches for records in the table 'Tab_typerr'.
    '%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla 'Tab_typerr'.
    '%Parámetros:
    Public Function Find() As Boolean
        Dim lclsTab_typerr As eRemoteDB.Execute
        Dim lclsTab_typerrItem As TAB_TYPERR

        If Not IsIDEMode() Then
        End If
        lclsTab_typerr = New eRemoteDB.Execute

        With lclsTab_typerr
            .StoredProcedure = "reaTab_typerr_a"
            If .Run(True) Then
                Do While Not .EOF
                    lclsTab_typerrItem = New TAB_TYPERR
                    lclsTab_typerrItem.nType_err = .FieldToClass("nType_err")
                    lclsTab_typerrItem.nTypeerr_pa = .FieldToClass("nTypeerr_pa")
                    lclsTab_typerrItem.sDescript = .FieldToClass("sDescript")
                    lclsTab_typerrItem.sShort_des = .FieldToClass("sShort_des")
                    lclsTab_typerrItem.sStatregt = .FieldToClass("sStatregt")
                    lclsTab_typerrItem.sTransiti = .FieldToClass("sTransiti")
                    lclsTab_typerrItem.nUsercode = .FieldToClass("nUsercode")

                    Call Add(lclsTab_typerrItem)
                    lclsTab_typerrItem = Nothing
                    .RNext()
                Loop

                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclsTab_typerr = Nothing
        lclsTab_typerrItem = Nothing

        Exit Function
    End Function

    '**%Objective: This property is used when an element in the collection is referenced.
    '**%Parameters:
    '**%    vIndexKey - An expression that specifies the position of an element from the collection
    '%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
    Public ReadOnly Property Item(ByVal vIndexKey As Object) As TAB_TYPERR
        Get
            If Not IsIDEMode() Then
            End If

            Item = mcolTab_typerr.Item(vIndexKey)

            Exit Property
        End Get
    End Property

    '**%Objective: Returns the number of elements in the collection.
    '%Objetivo: Retorna la cantidad de elementos existentes en la colección.
    Public ReadOnly Property Count() As Integer
        Get
            If Not IsIDEMode() Then
            End If

            Count = mcolTab_typerr.Count()

            Exit Property
        End Get
    End Property

    '**%Objective: Allows you to enumerate this collection with a "For...Each" loop.
    '%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
    'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
    'Get
    'If Not IsIDEMode Then
    'End If
    '
    'NewEnum = mcolTab_typerr._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("Tab_typerrs.NewEnum()")
    'End Get
    'End Property

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolTab_typerr.GetEnumerator
    End Function

    '**%Objective: Removes an element from the collection.
    '**%Parameters:
    '**%    vIndexKey - An expression that specifies the position of an element from the collection
    '%Objetivo: Permite eliminar un elemento de la colección.
    '%Parámetros:
    '%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
    Public Sub Remove(ByRef vIndexKey As Object)
        If Not IsIDEMode() Then
        End If

        mcolTab_typerr.Remove(vIndexKey)

        Exit Sub
    End Sub

    '**%Objective: Creates the collection when this class is created.
    '%Objetivo: Esta método crea la colección cuando se crea la clase.
    Private Sub Class_Initialize_Renamed()
        If Not IsIDEMode() Then
        End If

        mcolTab_typerr = New Collection

        Exit Sub
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '**%Objective: Destroys collection when this class is terminated.
    '%Objetivo: Este método destruye la colección cuando se termina la clase.
    Private Sub Class_Terminate_Renamed()
        If Not IsIDEMode() Then
        End If

        mcolTab_typerr = Nothing

        Exit Sub
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
End Class











