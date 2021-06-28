Option Strict Off
Option Explicit On
Public Class Res_AverageSOATs
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Res_AverageSOAT'.
	'**+Version: $$Revision: 1 $
	'+Objetivo: Colección que le da soporte a la clase 'Res_AverageSOAT'.
	'+Version: $$Revision: 1 $
	
	'**-Objective: Local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la colección.
	Private mcolRes_AverageSOAT As Collection
	
	'**%Objective: Adds an element to the collection.
	'**%Parameters:
	'**%    lclsRes_AverageSOAT -  Class of the boards Res_AverageSOAT
	'%Objetivo: Este método permite agregar un elemento a la colección.
	'%Parámetros:
	'%    lclsRes_AverageSOAT - Clase de la tabla Res_AverageSOAT
	Public Function Add(ByRef lclsRes_AverageSOAT As Res_AverageSOAT) As Res_AverageSOAT
        		
		'**+ The properties passed to the method are assigned to the collection.
		'+ Las propiedades pasadas al método son asignadas a la colección.
		
		mcolRes_AverageSOAT.Add(lclsRes_AverageSOAT)
		
		'**+Returns the object created.
		'+ Retorna el objeto creado.
		
		Add = lclsRes_AverageSOAT
		lclsRes_AverageSOAT = Nothing
		
		Exit Function

	End Function
	
	'**%Objective: Searches for records in the table 'Res_AverageSOAT'.
	'**%Parameters:
	'**%    nCli_category - Client category - SOAT Claim
	'**%    nCurrency     - Code of the currency
	'%Objetivo: Este método realiza la lectura de la información de la tabla en tratamiento 'Res_AverageSOAT'.
	'%Parámetros:
	'%    nCli_category - Categorias de clientes - Siniestros SOAT
	'%    nCurrency     - Código de la moneda
	Public Function Find(ByVal nCli_category As Integer, ByVal nCurrency As Integer) As Boolean
		Dim lclsRes_AverageSOAT As eRemoteDB.Execute
		Dim lclsRes_AverageSOATItem As Res_AverageSOAT
		
        On Error GoTo ErrorHandler
		
		lclsRes_AverageSOAT = New eRemoteDB.Execute
		
		With lclsRes_AverageSOAT
			.StoredProcedure = "reaRes_AverageSOAT_a"
			.Parameters.Add("nCli_category", nCli_category, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsRes_AverageSOATItem = New Res_AverageSOAT
					lclsRes_AverageSOATItem.nCli_category = .FieldToClass("nCli_Category")
					lclsRes_AverageSOATItem.nCurrency = .FieldToClass("nCurrency")
					lclsRes_AverageSOATItem.sIllness = .FieldToClass("sIllness")
					lclsRes_AverageSOATItem.nResaveclin = .FieldToClass("nResaveClin")
					lclsRes_AverageSOATItem.nResavehosp = .FieldToClass("nResaveHosp")
					lclsRes_AverageSOATItem.nRes_average = .FieldToClass("nRes_Average")
					lclsRes_AverageSOATItem.nResavetdis = .FieldToClass("nResavetdis")
					lclsRes_AverageSOATItem.nDaysavedis = .FieldToClass("nDaysavedis")
					lclsRes_AverageSOATItem.sStatregt = .FieldToClass("sStatregt")
					Call Add(lclsRes_AverageSOATItem)
					lclsRes_AverageSOATItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsRes_AverageSOAT = Nothing
		
		Exit Function
ErrorHandler: 
        If Err.Number Then
            Find = False
        End If
	End Function
	
	'**%Objective: This property is used when an element in the collection is referenced.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As Res_AverageSOAT
		Get
			'On Error GoTo ErrorHandler
			
			Item = mcolRes_AverageSOAT.Item(vIndexKey)
			
			Exit Property

		End Get
	End Property
	
	'**%Objective: Returns the number of elements in the collection.
	'%Objetivo: Retorna la cantidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			'On Error GoTo ErrorHandler
			
			Count = mcolRes_AverageSOAT.Count()
			
			Exit Property

		End Get
	End Property
	
	'**%Objective: Allows you to enumerate this collection with a "For...Each" loop.
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			''On Error GoTo ErrorHandler
			'
			'NewEnum = mcolRes_AverageSOAT._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Res_AverageSOATs.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolRes_AverageSOAT.GetEnumerator
	End Function
	
	'**%Objective: Removes an element from the collection.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Public Sub Remove(ByRef vIndexKey As Object)
		'On Error GoTo ErrorHandler
		
		mcolRes_AverageSOAT.Remove(vIndexKey)
		
		Exit Sub

	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Esta método crea la colección cuando se crea la clase.
	Private Sub Class_Initialize_Renamed()
		'On Error GoTo ErrorHandler
		
		mcolRes_AverageSOAT = New Collection
		
		Exit Sub

	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Este método destruye la colección cuando se termina la clase.
	Private Sub Class_Terminate_Renamed()
		mcolRes_AverageSOAT = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






