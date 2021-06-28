Option Strict Off
Option Explicit On
Public Class Detail_Machines
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class Detail_Machine.
	'**+Version: $$Revision: 1 $
	'+Objetivo: Colección que le da soporte a la clase Detail_Machine.
	'+Version: $$Revision: 1 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcol As Collection
	
	'**%Objective: It adds an element to the collection.
	'%Objetivo: Agrega un elemento a la colección.
	Public Function Add(ByRef lclsDetail_Machine As Detail_Machine) As Detail_Machine

		'**-set the properties passed into the method
		mcol.Add(lclsDetail_Machine)
		
		'**-return the object created
		Add = lclsDetail_Machine
		lclsDetail_Machine = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Detail_Machine'.
	'%Objetivo: Función que realiza la busqueda en la tabla 'Detail_Machine'.
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsDetail_Machine As eRemoteDB.Execute
		Dim lclsDetail_MachineItem As Detail_Machine
		

		lclsDetail_Machine = New eRemoteDB.Execute
		'+ Define all parameters for the stored procedure 'reaDetail_Machine'. Generated on 01/06/2005
		With lclsDetail_Machine
			.StoredProcedure = "reaDetail_Machine"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsDetail_MachineItem = New Detail_Machine
					lclsDetail_MachineItem.sCertype = .FieldToClass("sCertype")
					lclsDetail_MachineItem.nBranch = .FieldToClass("nBranch")
					lclsDetail_MachineItem.nProduct = .FieldToClass("nProduct")
					lclsDetail_MachineItem.nPolicy = .FieldToClass("nPolicy")
					lclsDetail_MachineItem.nCertif = .FieldToClass("nCertif")
					lclsDetail_MachineItem.nMachineCode = .FieldToClass("nMachineCode")
					lclsDetail_MachineItem.nFabYear = .FieldToClass("nFabYear")
					lclsDetail_MachineItem.dEffecdate = .FieldToClass("dEffecdate")
					lclsDetail_MachineItem.dNullDate = .FieldToClass("dNulldate")
					lclsDetail_MachineItem.dCompDate = .FieldToClass("dCompdate")
					lclsDetail_MachineItem.nUsercode = .FieldToClass("nUsercode")
					lclsDetail_MachineItem.nQuantityMachine = .FieldToClass("nQuantityMachine")
					Call Add(lclsDetail_MachineItem)
					lclsDetail_MachineItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lclsDetail_Machine = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As Detail_Machine
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
			'ProcError("Detail_Machines.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcol.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'%Objetivo: Permite eliminar un elemento de la colección.
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











