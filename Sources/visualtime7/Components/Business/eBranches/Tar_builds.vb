Option Strict Off
Option Explicit On
Public Class Tar_builds
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tar_build'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Tar_build'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTar_build As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTar_build -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTar_build -
	Public Function Add(ByRef lclsTar_build As Tar_build) As Tar_build
		
		'**-set the properties passed into the method

		mcolTar_build.Add(lclsTar_build)
		
		'**-return the object created
		Add = lclsTar_build
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tar_build'.
	'**%Parameters:
	'**%    nBranch    - Code of the line of business.
	'**%    nProduct   - Code of the product.
	'**%    dEffecDate - Date which from the record is valid.
	'%Objetivo: Función que realiza la busqueda en la tabla 'Tar_build'.
	'%Parámetros:
	'%    nBranch    - Código del ramo comercial.
	'%    nProduct   - Código del producto.
	'%    dEffecDate - Fecha de efecto del registro.
	Public Function Find(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclsTar_build As eRemoteDB.Execute
		Dim lclsTar_buildItem As Tar_build
		
        lclsTar_build = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTar_build'. Generated on 29/06/2004 11:58:05 a.m.
		With lclsTar_build
			.StoredProcedure = "reaTar_build_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTar_buildItem = New Tar_build
					lclsTar_buildItem.nBranch = .FieldToClass("nBranch")
					lclsTar_buildItem.nProduct = .FieldToClass("nProduct")
					lclsTar_buildItem.dEffecDate = .FieldToClass("dEffecDate")
					lclsTar_buildItem.nCategory = .FieldToClass("nCategory")
					lclsTar_buildItem.nExtraPrem = .FieldToClass("nExtraPrem")
					lclsTar_buildItem.nDiscount = .FieldToClass("nDiscount")
					Call Add(lclsTar_buildItem)
					lclsTar_buildItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTar_build = Nothing
		lclsTar_buildItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tar_build
		Get

			Item = mcolTar_build.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTar_build.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolTar_build._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Tar_builds.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTar_build.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTar_build.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTar_build = New Collection
		
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











