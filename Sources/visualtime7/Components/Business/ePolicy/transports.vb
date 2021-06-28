Option Strict Off
Option Explicit On
Public Class transports
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'transport'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'transport'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcoltransport As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclstransport -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclstransport -
	Public Function Add(ByRef lclstransport As transport) As transport
		
		'**-set the properties passed into the method


		mcoltransport.Add(lclstransport)
		
		'**-return the object created
		Add = lclstransport
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'transport'.
	'**%Parameters:
	'**%    sCertype    - Type of record
	'**%    nBranch     - Code of the line of business
	'**%    nProduct    - Code of the product
	'**%    nPolicy     - Policy number
	'**%    nCertif     - Number identifying the certificate
	'**%    dEffecdate  - Effective date
	'%Objetivo: Función que realiza la busqueda en la tabla 'transport'.
	'%Parámetros:
	'%    sCertype    - Tipo de registro
	'%    nBranch     - Código de la línea del negocio
	'%    nProduct    - Código del producto
	'%    nPolicy     - Número de póliza
	'%    nCertif     - Número que identifica el certificado
	'%    dEffecdate  - Fecha de efecto
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclstransport As eRemoteDB.Execute
		Dim lclstransportItem As transport
		

        lclstransport = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reatransport'. Generated on 29/06/2004 11:53:30 a.m.
		With lclstransport
			.StoredProcedure = "reatransport_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclstransportItem = New transport
					lclstransportItem.sCertype = sCertype
					lclstransportItem.nBranch = nBranch
					lclstransportItem.nProduct = nProduct
					lclstransportItem.nPolicy = nPolicy
					lclstransportItem.nCertif = nCertif
					lclstransportItem.nCurrency = .FieldToClass("nCurrency")
					lclstransportItem.nMaxLimTrip = .FieldToClass("nMaxLimTrip")
					lclstransportItem.nDep_rate = .FieldToClass("nDep_rate")
					lclstransportItem.nDecla_freq = .FieldToClass("nDecla_freq")
					lclstransportItem.nEstAmount = .FieldToClass("nEstAmount")
					lclstransportItem.nOverLine = .FieldToClass("nOverLine")
					lclstransportItem.nModalitySumins = .FieldToClass("nModalitySumins")
					lclstransportItem.nDep_prem = .FieldToClass("nDep_prem")
					Call Add(lclstransportItem)
					lclstransportItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclstransport = Nothing
		lclstransportItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As transport
		Get


			Item = mcoltransport.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get


			Count = mcoltransport.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get

    
    '
    'NewEnum = mcoltransport._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("transports.NewEnum()")
    'End Get
    'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcoltransport.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)


		mcoltransport.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()


		mcoltransport = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()


		mcoltransport = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











