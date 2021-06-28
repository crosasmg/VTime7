Option Strict Off
Option Explicit On
Public Class Tran_merchs
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'Tran_merch'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'Tran_merch'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolTran_merch As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsTran_merch -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsTran_merch -
	Public Function Add(ByRef lclsTran_merch As Tran_merch) As Tran_merch
		'**-set the properties passed into the method


		mcolTran_merch.Add(lclsTran_merch)
		
		'**-return the object created
		Add = lclsTran_merch
		lclsTran_merch = Nothing
		
		Exit Function
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'Tran_merch'.
	'**%Parameters:
	'**%    sCertype   - Type of record
	'**%    nBranch    - Code of the line of business
	'**%    nProduct   - Code of the product
	'**%    nPolicy    - Policy number
	'**%    nCertif    - Number identifying of the certificate
	'**%    nCurrency  - Code of the currency
	'**%    dEffecdate - Effective date of the record
	'%Objetivo: Función que realiza la busqueda en la tabla 'Tran_merch'.
	'%Parámetros:
	'%    sCertype   - Tipo de registro
	'%    nBranch    - Código del ramo comercial
	'%    nProduct   - Código del producto
	'%    nPolicy    - Número de póliza
	'%    nCertif    - Número que identifica el certificado
	'%    nCurrency  - Código de la moneda
	'%    dEffecdate - Fecha de efecto del registro
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsTran_merch As eRemoteDB.Execute
		Dim lclsTran_merchItem As Tran_merch
		

        lclsTran_merch = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaTran_merch'. Generated on 02/07/2004 11:08:19 a.m.
		With lclsTran_merch
			.StoredProcedure = "reaTran_merch_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Do While Not .EOF
					lclsTran_merchItem = New Tran_merch
					lclsTran_merchItem.sCertype = sCertype
					lclsTran_merchItem.nBranch = nBranch
					lclsTran_merchItem.nProduct = nProduct
					lclsTran_merchItem.nPolicy = nPolicy
					lclsTran_merchItem.nCertif = nCertif
					lclsTran_merchItem.nClassmerch = .FieldToClass("nClassMerch")
					lclsTran_merchItem.nPacking = .FieldToClass("nPacking")
					lclsTran_merchItem.sDescript = .FieldToClass("sDescript")
					lclsTran_merchItem.nQuantrans = .FieldToClass("nQuanTrans")
					lclsTran_merchItem.nUnit = .FieldToClass("nUnit")
					lclsTran_merchItem.nAmount = .FieldToClass("nAmount")
					lclsTran_merchItem.sFranDedi = .FieldToClass("sFranDedi")
					lclsTran_merchItem.nFranDedRate = .FieldToClass("nFranDedRate")
					lclsTran_merchItem.nMinAmount = .FieldToClass("nMinAmount")
					lclsTran_merchItem.nCurrency = .FieldToClass("nCurrency")
					Call Add(lclsTran_merchItem)
					lclsTran_merchItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsTran_merch = Nothing
		lclsTran_merchItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As Tran_merch
		Get


			Item = mcolTran_merch.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get


			Count = mcolTran_merch.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get

    '
    'NewEnum = mcolTran_merch._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("Tran_merchs.NewEnum()")
    'End Get
    'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolTran_merch.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)


		mcolTran_merch.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()


		mcolTran_merch = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()


		mcolTran_merch = Nothing
		
		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











