Option Strict Off
Option Explicit On
Public Class Age_Products
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Age_Product'.
	'**+Version: $$Revision: 1 $
	'+Objetivo: Colección que le da soporte a la clase 'Age_Product'.
	'+Version: $$Revision: 1 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcol As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsAge_Product - Instance of the class Age_Product
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsAge_Product - Instancia de la clase Age_Product
	Public Function Add(ByRef lclsAge_Product As Age_Product) As Age_Product

        '**-set the properties passed into the method
		mcol.Add(lclsAge_Product)
		
		'**-return the object created
		Add = lclsAge_Product
		lclsAge_Product = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Age_Product'.
	'**%Parameters:
	'               nBranch      - Branch code
	'               nProduct     - Product code
	'               nMachineCode - Machine code
	'
	'%Objetivo: Función que realiza la busqueda en la tabla 'Age_Product'.
	'%Parámetros:
	'               nBranch      - Código del Ramo
	'               nProduct     - Código del Producto
	'               nMachineCode - Código de Maquinaria
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMachineCode As Integer) As Boolean
		Dim lclsAge_Product As eRemoteDB.Execute
		Dim lclsAge_ProductItem As Age_Product
		
        lclsAge_Product = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaAge_Product'. Generated on <VT:DATETIME>
		With lclsAge_Product
			.StoredProcedure = "reaAge_Product"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMachineCode", nMachineCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsAge_ProductItem = New Age_Product
					lclsAge_ProductItem.nBranch = .FieldToClass("nBranch")
					lclsAge_ProductItem.nProduct = .FieldToClass("nProduct")
					lclsAge_ProductItem.nMachineCode = .FieldToClass("nMachineCode")
					lclsAge_ProductItem.nAge = .FieldToClass("nAge")
					lclsAge_ProductItem.dCompdate = .FieldToClass("dCompdate")
					lclsAge_ProductItem.nUsercode = .FieldToClass("nUsercode")
					Call Add(lclsAge_ProductItem)
					lclsAge_ProductItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lclsAge_Product = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   - Position of the referenced register
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Posicion del registro referenciado
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As Age_Product
		Get

			Item = mcol.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcol.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcol._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Age_Products.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcol.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   - Identifies the registry to be deleted
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Identifica el registro a ser eliminado
	Public Sub Remove(ByRef vIndexKey As Object)

		mcol.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcol = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()
		
		mcol = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











