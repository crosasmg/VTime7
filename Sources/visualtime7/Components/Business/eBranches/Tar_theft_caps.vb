Option Strict Off
Option Explicit On
Public Class Tar_theft_caps
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tar_theft_cap'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Tar_theft_cap'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTar_theft_cap As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTar_theft_cap -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTar_theft_cap -
	Public Function Add(ByRef lclsTar_theft_cap As Tar_theft_cap) As Tar_theft_cap
		
		'**-set the properties passed into the method
		
		mcolTar_theft_cap.Add(lclsTar_theft_cap)
		
		'**-return the object created
		Add = lclsTar_theft_cap
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tar_theft_cap'.
	'**%Parameters:
	'**%    nBranch    - Code of the commercial branch.
	'**%    nProduct   - Code of the product.
	'**%    nCover     - Code of the cover.
	'**%    nCurrency  - Code of the currency.
	'**%    dEffecdate - Date which from the record is valid.
	'%Objetivo: Función que realiza la busqueda en la tabla 'Tar_theft_cap'.
	'%Parámetros:
	'%    nBranch    - Codigo del ramo comercial.
	'%    nProduct   - Codigo del producto.
	'%    nCover     - Codigo de la cobertura.
	'%    nCurrency  - Código de la moneda.
	'%    dEffecdate - Fecha de efecto del registro.
	Public Function Find(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal nCurrency As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclsTar_theft_cap As eRemoteDB.Execute
		Dim lclsTar_theft_capItem As Tar_theft_cap
		
        lclsTar_theft_cap = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTar_theft_cap'. Generated on 25/06/2004 02:34:27 p.m.
		With lclsTar_theft_cap
			.StoredProcedure = "reaTar_theft_cap_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTar_theft_capItem = New Tar_theft_cap
					lclsTar_theft_capItem.nBranch = .FieldToClass("nBranch")
					lclsTar_theft_capItem.nProduct = .FieldToClass("nProduct")
					lclsTar_theft_capItem.nCover = .FieldToClass("nCover")
					lclsTar_theft_capItem.nCurrency = .FieldToClass("nCurrency")
					lclsTar_theft_capItem.dEffecDate = .FieldToClass("dEffecdate")
					lclsTar_theft_capItem.nCap_init = .FieldToClass("nCap_init")
					lclsTar_theft_capItem.nCap_end = .FieldToClass("nCap_end")
					lclsTar_theft_capItem.nTar_theft = .FieldToClass("nTar_theft")
					Call Add(lclsTar_theft_capItem)
					lclsTar_theft_capItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTar_theft_cap = Nothing
		lclsTar_theft_capItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tar_theft_cap
		Get

			Item = mcolTar_theft_cap.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTar_theft_cap.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolTar_theft_cap._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Tar_theft_caps.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTar_theft_cap.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTar_theft_cap.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTar_theft_cap = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()

		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











