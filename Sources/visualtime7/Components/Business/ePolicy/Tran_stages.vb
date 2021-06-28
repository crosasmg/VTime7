Option Strict Off
Option Explicit On
Public Class Tran_stages
	Implements System.Collections.IEnumerable
	
	'**+Objective: Collection that supports the class Tran_stage.
	'**+Version: $$Revision: 5 $
	'+Objetivo: Colección que le da soporte a la clase Tran_stage.
	'+Version: $$Revision: 5 $
	
	'**-Objective: Local variable to hold collection.
	'-Objetivo: Variable Local para almacenar la colección.
	Private mcolTran_stage As Collection
	
	
	
	'**%Objective: Adds an element to the collection.
	'**%Parameters:
	'**%    lclsTran_stage - Elements of the class
	'%Objetivo: Este método permite agregar un elemento a la colección.
	'%Parámetros:
	'%    lclsTran_stage - Elementos de la clase
	Public Function Add(ByRef lclsTran_stage As Tran_stage) As Tran_stage
		
		'**+ The properties passed to the method are assigned to the collection.
		'+ Las propiedades pasadas al método son asignadas a la colección.


		mcolTran_stage.Add(lclsTran_stage)
		
		'**+Returns the object created.
		'+ Retorna el objeto creado.
		
		Add = lclsTran_stage
		
		Exit Function
	End Function
	
	'**%Objective: Searches for records in the table Tran_stage.
	'**%Parameters:
	'**%    sCertype     - Type of record
	'**%    nBranch      - Code of the line of business
	'**%    nProduct     - Code of the product
	'**%    nPolicy      - Number identifying the policy
	'**%    nCertif      - Number identifying the certificate
	'**%    dEffecdate   - Effective date of the record
	'**%    nCurrency    - Code of the currency
	'%Objetivo: Esta función permite realizar la búsqueda de la información en la tabla Tran_stage.
	'%Parámetros:
	'%    sCertype     - Tipo de registro
	'%    nBranch      - Código del ramo
	'%    nProduct     - Código del producto
	'%    nPolicy      - Número que identifica la póliza
	'%    nCertif      - Número que identifica el certificado
	'%    dEffecdate   - Fecha efectiva del registro
	'%    nCurrency    - Código de la moneda
    Public Function Find(ByVal sCertype As String, _
                         ByVal nBranch As Integer, _
                         ByVal nProduct As Integer, _
                         ByVal nPolicy As Double, _
                         ByVal nCertif As Double, _
                         ByVal dEffecdate As Date, _
                         ByVal nCurrency As Integer) As Boolean

        Dim lclsTran_stage As eRemoteDB.Execute
        Dim lclsTran_stageItem As Tran_stage

        
        lclsTran_stage = New eRemoteDB.Execute

        With lclsTran_stage
            .StoredProcedure = "reaTran_stage_a"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                Do While Not .EOF
                    lclsTran_stageItem = New Tran_stage
                    lclsTran_stageItem.nStage = .FieldToClass("nStage")
                    lclsTran_stageItem.dDestindat = .FieldToClass("dDestindat")
                    lclsTran_stageItem.dOrigindat = .FieldToClass("dOrigindat")
                    lclsTran_stageItem.nRoute = .FieldToClass("nRoute")
                    lclsTran_stageItem.sName_licen = .FieldToClass("sName_licen")
                    lclsTran_stageItem.nAmount = .FieldToClass("nAmount")
                    lclsTran_stageItem.nFrandedi = .FieldToClass("nFrandedi")
                    lclsTran_stageItem.sOrigen = .FieldToClass("sOrigen")
                    lclsTran_stageItem.sDestination = .FieldToClass("sDestination")
                    lclsTran_stageItem.nTypRoute = .FieldToClass("nTyproute")
                    lclsTran_stageItem.nTransptype = .FieldToClass("nTransptype")
                    lclsTran_stageItem.sPurchase_Order = .FieldToClass("sPurchase_Order")
                    lclsTran_stageItem.sApplicationNumber = .FieldToClass("sApplicationNumber")

                    Call Add(lclsTran_stageItem)
                    lclsTran_stageItem = Nothing
                    .RNext()
                Loop

                Find = True
                .RCloseRec()
            Else
                Find = False
            End If
        End With

        lclsTran_stage = Nothing
        lclsTran_stageItem = Nothing

        Exit Function
    End Function
	
	'**%Objective: This property is used when an element in the collection is referenced.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tran_stage
		Get


			Item = mcolTran_stage.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Returns the number of elements in the collection.
	'%Objetivo: Retorna la cantidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get


			Count = mcolTran_stage.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: Allows you to enumerate this collection with a "For...Each" loop.
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get

    '
    'NewEnum = mcolTran_stage._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("Tran_stages.NewEnum()")
    'End Get
    'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTran_stage.GetEnumerator
	End Function
	
	'**%Objective: Removes an element from the collection.
	'**%Parameters:
	'**%    vIndexKey - An expression that specifies the position of an element from the collection
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey - Una expresión que especifica la posición de un elemento de la colección.
	Public Sub Remove(ByRef vIndexKey As Object)


		mcolTran_stage.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Esta método crea la colección cuando se crea la clase.
	Private Sub Class_Initialize_Renamed()


		mcolTran_stage = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Este método destruye la colección cuando se termina la clase.
	Private Sub Class_Terminate_Renamed()


		mcolTran_stage = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











