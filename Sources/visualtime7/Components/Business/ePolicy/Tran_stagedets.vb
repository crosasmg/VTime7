Option Strict Off
Option Explicit On
Public Class Tran_stagedets
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class Tran_stagedet.
	'**+Version: $$Revision: 5 $
	'+Objetivo: Colección que le da soporte a la clase Tran_stagedet.
	'+Version: $$Revision: 5 $
	
	'**-Objective: Local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la colección.
	Private mcolTran_stagedet As Collection
	
	
	
	'**%Objective: Adds an element to the collection.
	'**%Parameters:
	'**%    lclsTran_stagedet - Elements of the class
	'%Objetivo: Este método permite agregar un elemento a la colección.
	'%Parámetros:
	'%    lclsTran_stagedet - Elementos de la clase
	Public Function Add(ByRef lclsTran_stagedet As Tran_stagedet) As Tran_stagedet
		'**+ The properties passed to the method are assigned to the collection.
		'+ Las propiedades pasadas al método son asignadas a la colección.


		mcolTran_stagedet.Add(lclsTran_stagedet)
		
		'**+Returns the object created.
		'+ Retorna el objeto creado.
		
		Add = lclsTran_stagedet
		
		Exit Function
	End Function
	
	'**%Objective: Searches for records in the table Tran_stagedet.
	'**%Parameters:
	'**%    sCertype        - Type of record
	'**%    nBranch         - Code of the line of business
	'**%    nProduct        - Code of the product
	'**%    nPolicy         - Number identifying the policy
	'**%    nCertif         - Number identifying the certificate
	'**%    nCurrency       - Code of the currency
	'**%    dEffecdate      - Effective date of the record
	'%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla Tran_stagedet.
	'%Parámetros:
	'%    sCertype        - Tipo de registro
	'%    nBranch         - Código del ramo
	'%    nProduct        - Código del producto
	'%    nPolicy         - Número que identifica la póliza
	'%    nCertif         - Número que identifica el certificado
	'%    nCurrency       - Código de la moneda
	'%    dEffecdate      - Fecha efectiva del registro
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCurrency As Integer, ByVal nStage As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsTran_stagedet As eRemoteDB.Execute
		Dim lclsTran_stagedetItem As Tran_stagedet
		

        lclsTran_stagedet = New eRemoteDB.Execute
		
		With lclsTran_stagedet
			.StoredProcedure = "reaTran_stagedet_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStage", nStage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsTran_stagedetItem = New Tran_stagedet
					lclsTran_stagedetItem.nStage = .FieldToClass("nStage")
					lclsTran_stagedetItem.nClassmerch = .FieldToClass("nClassmerch")
					lclsTran_stagedetItem.sClassdesc = .FieldToClass("sClassdesc")
					lclsTran_stagedetItem.nPacking = .FieldToClass("nPacking")
					lclsTran_stagedetItem.sPackdesc = .FieldToClass("sPackdesc")
					lclsTran_stagedetItem.dEfd_tran_stage = .FieldToClass("dEfd_tran_stage")
					lclsTran_stagedetItem.nAmount = .FieldToClass("nAmount")
					lclsTran_stagedetItem.nFrandedi = .FieldToClass("nFrandedi")
					lclsTran_stagedetItem.nQuantrans = .FieldToClass("nQuantrans")
					lclsTran_stagedetItem.nUnit = .FieldToClass("nUnit")
					lclsTran_stagedetItem.nMerchrate = .FieldToClass("nMerchrate")
					lclsTran_stagedetItem.nUnitvalue = .FieldToClass("nUnitvalue")
					lclsTran_stagedetItem.nNotenum = .FieldToClass("nNotenum")
					lclsTran_stagedetItem.nImageNum = .FieldToClass("nImagenum")
					Call Add(lclsTran_stagedetItem)
					lclsTran_stagedetItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTran_stagedet = Nothing
		lclsTran_stagedetItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when an element in the collection is referenced.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tran_stagedet
		Get
			

			Item = mcolTran_stagedet.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Returns the number of elements in the collection.
	'%Objetivo: Retorna la cantidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get


			Count = mcolTran_stagedet.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Allows you to enumerate this collection with a "For...Each" loop.
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get

    '
    'NewEnum = mcolTran_stagedet._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("Tran_stagedets.NewEnum()")
    'End Get
    'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTran_stagedet.GetEnumerator
	End Function
	
	'**%Objective: Removes an element from the collection.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Public Sub Remove(ByRef vIndexKey As Object)


		mcolTran_stagedet.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Esta método crea la colección cuando se crea la clase.
	Private Sub Class_Initialize_Renamed()
		

		mcolTran_stagedet = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Este método destruye la colección cuando se termina la clase.
	Private Sub Class_Terminate_Renamed()


		mcolTran_stagedet = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











