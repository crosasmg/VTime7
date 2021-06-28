Option Strict Off
Option Explicit On
Public Class Tab_shipss
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tab_ships'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'Tab_ships'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTab_ships As Collection
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTab_ships -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTab_ships -
	Public Function Add(ByRef lclsTab_ships As Tab_ships) As Tab_ships
		
		'**-set the properties passed into the method
		mcolTab_ships.Add(lclsTab_ships)
		
		'**-return the object created
		Add = lclsTab_ships
		lclsTab_ships = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tab_ships'.
	'**%Parameters:
	'**%    sName          - License plate of the ship
	'**%    dEffecdate     - Effective date
	'**%    sDescript      - Ship name
	'**%    sShipCompClass - Company dedicated to the merchantmen ships classification
	'**%    nManyears      - Years of manufacture
	'%Objetivo: Función que realiza la búsqueda en la tabla 'Tab_ships'.
	'%Parámetros:
	'%    sName          - Número de registro o matrícula de la transacción
	'%    dEffecdate     - Fecha efectiva
	'%    sDescript      - Número de Vapor o nombre de la embarcación
	'%    sShipCompClass - Entidad dedicada a la clasificación de buques mercantes
	'%    nManyears      - Años de fabricación
	Public Function Find(ByVal sName_licen As String, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal sShipCompClass As String, ByVal nManyears As Integer) As Boolean
		Dim lclsTab_ships As eRemoteDB.Execute
		Dim lclsTab_shipsItem As Tab_ships
		
        'If Not IsIDEMode Then
        'End If
		
		lclsTab_ships = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTab_ships'. Generated on <VT:DATETIME>
		With lclsTab_ships
			.StoredProcedure = "REATAB_SHIPS_APKG.REATAB_SHIPS_A"
			.Parameters.Add("sName_licen", sName_licen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShipcompclass", sShipCompClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nManyears", nManyears, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					lclsTab_shipsItem = New Tab_ships
					lclsTab_shipsItem.sName_licen = .FieldToClass("sName_licen")
					lclsTab_shipsItem.sShipCompClass = .FieldToClass("sShipCompClass")
					lclsTab_shipsItem.sDescript = .FieldToClass("sDescript")
					lclsTab_shipsItem.dEffecdate = .FieldToClass("dEffecdate")
					lclsTab_shipsItem.dNullDate = .FieldToClass("dNullDate")
					lclsTab_shipsItem.nManyears = .FieldToClass("nManyears")
					Call Add(lclsTab_shipsItem)
					lclsTab_shipsItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lclsTab_ships = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tab_ships
		Get
			
			Item = mcolTab_ships.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mcolTab_ships.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mcolTab_ships._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("Tab_shipss.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTab_ships.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)
		
		mcolTab_ships.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()
		
		mcolTab_ships = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()
		
		mcolTab_ships = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











