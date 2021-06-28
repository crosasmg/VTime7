Option Strict Off
Option Explicit On
Public Class Tar_theft_cons
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tar_theft_con'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Tar_theft_con'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTar_theft_con As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTar_theft_con -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTar_theft_con -
	Public Function Add(ByRef lclsTar_theft_con As Tar_theft_con) As Tar_theft_con
		
		'**-set the properties passed into the method

		mcolTar_theft_con.Add(lclsTar_theft_con)
		
		'**-return the object created
		Add = lclsTar_theft_con
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tar_theft_con'.
	'**%Parameters:
	'**%    nTar_theft  - code of the theft tariff
	'**%    dEffecdate  - date which from the record is valid.
	'%Objetivo: Función que realiza la busqueda en la tabla 'Tar_theft_con'.
	'%Parámetros:
	'%    nTar_theft   - Código de la tarifa de robo
	'%    dEffecdate   - Fecha de efecto del registro.
	Public Function Find(ByVal nTar_theft As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclsTar_theft_con As eRemoteDB.Execute
		Dim lclsTar_theft_conItem As Tar_theft_con
		
        lclsTar_theft_con = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTar_theft_con'. Generated on 6/28/2004 10:33:03 AM
		With lclsTar_theft_con
			.StoredProcedure = "reaTar_theft_con_a"
			.Parameters.Add("nTar_theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTar_theft_conItem = New Tar_theft_con
					lclsTar_theft_conItem.nTar_theft = .FieldToClass("nTar_theft")
					lclsTar_theft_conItem.dEffecDate = .FieldToClass("dEffecdate")
					lclsTar_theft_conItem.nInsured = .FieldToClass("nInsured")
					lclsTar_theft_conItem.nRiskClass = .FieldToClass("nRiskClass")
					lclsTar_theft_conItem.nUbication = .FieldToClass("nUbication")
					lclsTar_theft_conItem.nRate = .FieldToClass("nRate")
					Call Add(lclsTar_theft_conItem)
					lclsTar_theft_conItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTar_theft_con = Nothing
		lclsTar_theft_conItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tar_theft_con
		Get

			Item = mcolTar_theft_con.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolTar_theft_con.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolTar_theft_con._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Tar_theft_cons.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTar_theft_con.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolTar_theft_con.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolTar_theft_con = New Collection
		
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











