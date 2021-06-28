Option Strict Off
Option Explicit On
Public Class CliDocumentss
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'CliDocuments'.
	'**+Version: $$Revision: 4 $
	'+Objetivo: Colección que le da soporte a la clase 'CliDocuments'.
	'+Version: $$Revision: 4 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolCliDocuments As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsCliDocuments -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsCliDocuments -
	Public Function Add(ByRef lclsCliDocuments As CliDocuments) As CliDocuments
		
		'**-set the properties passed into the method

		mcolCliDocuments.Add(lclsCliDocuments)
		
		'**-return the object created
		Add = lclsCliDocuments
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'CliDocuments'.
	'**%Parameters:
	'**%    Pending   -
	'%Objetivo: Función que realiza la busqueda en la tabla 'CliDocuments'.
	'%Parámetros:
	'%    sClient - Código de identificación del cliente
	Public Function Find(ByVal sClient As String) As Boolean
		Dim lclsCliDocuments As eRemoteDB.Execute
		Dim lclsCliDocumentsItem As CliDocuments
		
        lclsCliDocuments = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaCliDocuments'. Generated on 11/19/2004 3:04:01 PM
		With lclsCliDocuments
			.StoredProcedure = "reaCliDocuments_a"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					lclsCliDocumentsItem = New CliDocuments
					lclsCliDocumentsItem.sClient = sClient
					lclsCliDocumentsItem.nTypClientDoc = .FieldToClass("nTypClientDoc")
					lclsCliDocumentsItem.sCliNumDocu = .FieldToClass("sCliNumDocu")
					lclsCliDocumentsItem.dIssueDat = .FieldToClass("dIssueDat")
					lclsCliDocumentsItem.dExpirDat = .FieldToClass("dExpirDat")
					Call Add(lclsCliDocumentsItem)
					lclsCliDocumentsItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsCliDocuments = Nothing
		lclsCliDocumentsItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public ReadOnly Property Item(ByVal vIndexKey As Object) As CliDocuments
		Get

			Item = mcolCliDocuments.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get

			Count = mcolCliDocuments.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
    'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
    '
			'NewEnum = mcolCliDocuments._NewEnum
			'
			'Exit Property
'ErrorHandler: '
			'ProcError("CliDocumentss.NewEnum()")
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolCliDocuments.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)

		mcolCliDocuments.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()

		mcolCliDocuments = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()

		mcolCliDocuments = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











