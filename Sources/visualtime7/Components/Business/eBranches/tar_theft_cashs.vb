Option Strict Off
Option Explicit On
Public Class tar_theft_cashs
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'tar_theft_cash'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colecci�n que le da soporte a la clase 'tar_theft_cash'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colecci�n.
	Private mcoltar_theft_cash As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclstar_theft_cash -
	'%Objetivo: Agrega un elemento a la colecci�n.
	'%Par�metros:
	'%    lclstar_theft_cash -
	Public Function Add(ByRef lclstar_theft_cash As tar_theft_cash) As tar_theft_cash
		
		'**-set the properties passed into the method

		mcoltar_theft_cash.Add(lclstar_theft_cash)
		
		'**-return the object created
		Add = lclstar_theft_cash
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'tar_theft_cash'.
	'**%Parameters:
	'**%   nTar_theft -  Code of the theft tariff
	'**%   dEffecdate -  Date which from the record is valid.
	'%Objetivo: Funci�n que realiza la busqueda en la tabla 'tar_theft_cash'.
	'%Par�metros:
	'%    nTar_theft  - C�digo de la tarifa de robo
	'%    dEffecdate  - Fecha de efecto del registro
	Public Function Find(ByVal nTar_theft As Short, ByVal dEffecDate As Date) As Boolean
		Dim lclstar_theft_cash As eRemoteDB.Execute
		Dim lclstar_theft_cashItem As tar_theft_cash
		
        lclstar_theft_cash = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reatar_theft_cash'. Generated on 28/06/2004 03:44:24 p.m.
		With lclstar_theft_cash
			.StoredProcedure = "reatar_theft_cash_a"
			.Parameters.Add("nTar_Theft", nTar_theft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclstar_theft_cashItem = New tar_theft_cash
					lclstar_theft_cashItem.nTar_theft = .FieldToClass("nTar_Theft")
					lclstar_theft_cashItem.dEffecDate = .FieldToClass("dEffecDate")
					lclstar_theft_cashItem.nUbication = .FieldToClass("nUbication")
					lclstar_theft_cashItem.nRate = .FieldToClass("nRate")
					Call Add(lclstar_theft_cashItem)
					lclstar_theft_cashItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclstar_theft_cash = Nothing
		lclstar_theft_cashItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colecci�n.
	'%Par�metros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As tar_theft_cash
		Get

			Item = mcoltar_theft_cash.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colecci�n.
	Public ReadOnly Property Count() As Integer
		Get

            Count = mcoltar_theft_cash.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colecci�n por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcoltar_theft_cash._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("tar_theft_cashs.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcoltar_theft_cash.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colecci�n.
	'%Par�metros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcoltar_theft_cash.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colecci�n cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcoltar_theft_cash = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colecci�n cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











