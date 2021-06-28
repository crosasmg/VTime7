Option Strict Off
Option Explicit On
Public Class Tar_rc_dess
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tar_rc_des'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Tar_rc_des'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTar_rc_des As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTar_rc_des -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTar_rc_des -
	Public Function Add(ByRef lclsTar_rc_des As Tar_rc_des) As Tar_rc_des
		
		'**-set the properties passed into the method

		mcolTar_rc_des.Add(lclsTar_rc_des)
		
		'**-return the object created
		Add = lclsTar_rc_des
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tar_rc_des'.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'%Objetivo: Función que realiza la busqueda en la tabla 'Tar_rc_des'.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	Public Function Find(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclsTar_rc_des As eRemoteDB.Execute
		Dim lclsTar_rc_desItem As Tar_rc_des
		
        lclsTar_rc_des = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTar_rc_des'. Generated on 16/06/2004 12:07:08 p.m.
		With lclsTar_rc_des
			.StoredProcedure = "reaTar_rc_des_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTar_rc_desItem = New Tar_rc_des
					lclsTar_rc_desItem.nBranch = .FieldToClass("nBranch")
					lclsTar_rc_desItem.nProduct = .FieldToClass("nProduct")
					lclsTar_rc_desItem.nCover = .FieldToClass("nCover")
					lclsTar_rc_desItem.dEffecDate = .FieldToClass("dEffecDate")
					lclsTar_rc_desItem.nCap_init = .FieldToClass("nCap_init")
					lclsTar_rc_desItem.nCap_end = .FieldToClass("nCap_end")
					lclsTar_rc_desItem.nRate = .FieldToClass("nRate")
					Call Add(lclsTar_rc_desItem)
					lclsTar_rc_desItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTar_rc_des = Nothing
		lclsTar_rc_desItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tar_rc_des
		Get

			Item = mcolTar_rc_des.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTar_rc_des.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolTar_rc_des._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Tar_rc_dess.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTar_rc_des.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTar_rc_des.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTar_rc_des = New Collection
		
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











