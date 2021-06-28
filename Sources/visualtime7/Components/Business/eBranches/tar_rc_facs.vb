Option Strict Off
Option Explicit On
Public Class tar_rc_facs
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'tar_rc_fac'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'tar_rc_fac'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcoltar_rc_fac As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclstar_rc_fac -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclstar_rc_fac -
	Public Function Add(ByRef lclstar_rc_fac As tar_rc_fac) As tar_rc_fac
		
		'**-set the properties passed into the method

		mcoltar_rc_fac.Add(lclstar_rc_fac)
		
		'**-return the object created
		Add = lclstar_rc_fac
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'tar_rc_fac'.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    dEffecDate - date which from the record is valid.
	'%Objetivo: Función que realiza la busqueda en la tabla 'tar_rc_fac'.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    dEffecDate - fecha de efecto del registro.
	Public Function Find(ByVal nBranch As Short, ByVal nProduct As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclstar_rc_fac As eRemoteDB.Execute
		Dim lclstar_rc_facItem As tar_rc_fac
		
        lclstar_rc_fac = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reatar_rc_fac'. Generated on 6/16/2004 2:10:07 PM
		With lclstar_rc_fac
			.StoredProcedure = "reatar_rc_fac_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclstar_rc_facItem = New tar_rc_fac
					lclstar_rc_facItem.nBranch = .FieldToClass("nBranch")
					lclstar_rc_facItem.nProduct = .FieldToClass("nProduct")
					lclstar_rc_facItem.dEffecDate = .FieldToClass("dEffecDate")
					lclstar_rc_facItem.nCap_init = .FieldToClass("nCap_init")
					lclstar_rc_facItem.nCap_end = .FieldToClass("nCap_end")
					lclstar_rc_facItem.nRate = .FieldToClass("nRate")
					Call Add(lclstar_rc_facItem)
					lclstar_rc_facItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclstar_rc_fac = Nothing
		lclstar_rc_facItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As tar_rc_fac
		Get

			Item = mcoltar_rc_fac.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcoltar_rc_fac.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcoltar_rc_fac._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("tar_rc_facs.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcoltar_rc_fac.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcoltar_rc_fac.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcoltar_rc_fac = New Collection
		
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











