Option Strict Off
Option Explicit On
Public Class Tar_rc_bass
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tar_rc_bas'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Tar_rc_bas'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTar_rc_bas As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTar_rc_bas -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTar_rc_bas -
	Public Function Add(ByRef lclsTar_rc_bas As Tar_rc_bas) As Tar_rc_bas
		
		'**-set the properties passed into the method

		mcolTar_rc_bas.Add(lclsTar_rc_bas)
		
		'**-return the object created
		Add = lclsTar_rc_bas
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tar_rc_bas'.
	'**%Parameters:
	'**%    nBranch    - code of the commercial branch.
	'**%    nProduct   - code of the product.
	'**%    nCover     - code of the cover.
	'**%    dEffecDate - date which from the record is valid.
	'%Objetivo: Función que realiza la busqueda en la tabla 'Tar_rc_bas'.
	'%Parámetros:
	'%    nBranch    - codigo del ramo comercial.
	'%    nProduct   - codigo del producto.
	'%    nCover     - codigo de la cobertura.
	'%    dEffecDate - fecha de efecto del registro.
	Public Function Find(ByVal nBranch As Short, ByVal nProduct As Short, ByVal nCover As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclsTar_rc_bas As eRemoteDB.Execute
		Dim lclsTar_rc_basItem As Tar_rc_bas
		
        lclsTar_rc_bas = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTar_rc_bas'. Generated on 6/16/2004 2:38:25 PM
		With lclsTar_rc_bas
			.StoredProcedure = "reaTar_rc_bas_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTar_rc_basItem = New Tar_rc_bas
					lclsTar_rc_basItem.nBranch = .FieldToClass("nBranch")
					lclsTar_rc_basItem.nProduct = .FieldToClass("nProduct")
					lclsTar_rc_basItem.nCover = .FieldToClass("nCover")
					lclsTar_rc_basItem.dEffecDate = .FieldToClass("dEffecDate")
					lclsTar_rc_basItem.nArticle = .FieldToClass("nArticle")
					lclsTar_rc_basItem.nDetailArt = .FieldToClass("nDetailArt")
                    lclsTar_rc_basItem.nRate = .FieldToClass("nRate")
                    lclsTar_rc_basItem.nCommergrp = .FieldToClass("nCommergrp")
					Call Add(lclsTar_rc_basItem)
					lclsTar_rc_basItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTar_rc_bas = Nothing
		lclsTar_rc_basItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tar_rc_bas
		Get

			Item = mcolTar_rc_bas.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTar_rc_bas.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolTar_rc_bas._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Tar_rc_bass.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTar_rc_bas.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTar_rc_bas.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTar_rc_bas = New Collection
		
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











