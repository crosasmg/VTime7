Option Strict Off
Option Explicit On
Public Class TypeLocations
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'TypeLocation'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'TypeLocation'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTypeLocation As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTypeLocation -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTypeLocation -
	Public Function Add(ByRef lclsTypeLocation As TypeLocation) As TypeLocation

		'**-set the properties passed into the method
		mcolTypeLocation.Add(lclsTypeLocation)
		
		'**-return the object created
		Add = lclsTypeLocation
		lclsTypeLocation = Nothing
		
		Exit Function

	End Function
	
	'%Objetivo: Función que realiza la busqueda en la tabla 'TypeLocation'.
	Public Function Find() As Boolean
		Dim lclsTypeLocation As eRemoteDB.Execute
		Dim lclsTypeLocationItem As TypeLocation
		
        On Error GoTo ErrorHandler
		
		lclsTypeLocation = New eRemoteDB.Execute
		
        With lclsTypeLocation
            .StoredProcedure = "reaTypeLocation_a"
            If .Run(True) Then
                Do While Not .EOF
                    lclsTypeLocationItem = New TypeLocation
                    lclsTypeLocationItem.nLocat_Type = .FieldToClass("nLocat_Type")
                    lclsTypeLocationItem.sDescript = .FieldToClass("sDescript")
                    lclsTypeLocationItem.sShort_des = .FieldToClass("sShort_des")
                    lclsTypeLocationItem.nLocal_Source = .FieldToClass("nLocal_Source")
                    lclsTypeLocationItem.sStatRegt = .FieldToClass("sStatRegt")

                    Call Add(lclsTypeLocationItem)
                    lclsTypeLocationItem = Nothing
                    .RNext()
                Loop
                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With
		lclsTypeLocation = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Find = False
        End If
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As TypeLocation
		Get

			Item = mcolTypeLocation.Item(vIndexKey)
			
			Exit Property
        End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTypeLocation.Count()
			
			Exit Property
        End Get
	End Property
	
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        GetEnumerator = mcolTypeLocation.GetEnumerator
    End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTypeLocation.Remove(vIndexKey)
		
		Exit Sub

    End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTypeLocation = New Collection
		
		Exit Sub
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()

		mcolTypeLocation = Nothing
		
		Exit Sub
    End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






