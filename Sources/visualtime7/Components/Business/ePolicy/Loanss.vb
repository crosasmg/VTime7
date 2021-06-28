Option Strict Off
Option Explicit On
Public Class Loanss
	Implements System.Collections.IEnumerable
	'**Local variable to hold the collection
	'variable local para contener colección
	Private mCol As Collection
	
	'**% Add: Adds a new instance of the class "Loans" to the collection
	'% Add: Añade una nueva instancia de la clase Loans a la colección
	Public Function Add(ByVal nCode As Double, ByVal dLoan_date As Date, ByVal nAmount As Double, ByVal nInterest As Double) As Loans
		'** Variable definition. This variable will contain the instance that is going to be added
		'- Se define la variable que contendrá la instancia a añadir
		
		Dim objNewMember As Loans
		objNewMember = New Loans
		
		With objNewMember
			.nCode = nCode
			.dLoan_date = dLoan_date
			.nAmount = nAmount
			.nInterest = nInterest
		End With
		
		mCol.Add(objNewMember, "LNS" & nCode)
		
		'**+ Returns then created object
		'+ Retorna el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'**% Find: Returns a collection of objects of type "Loans"
	'% Find: Devuelve una coleccion de objetos de tipo Loans
	Public Function Find(ByVal nBranch As Integer, ByVal nPolicy As Double, ByVal nProduct As Integer, ByVal nCertif As Double, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lclsImprove_lo As Improve_lo
		Dim lclsLoans As Loans
		Dim lrecreaLoans_2 As eRemoteDB.Execute
		
		'**-Variable definition. This variable will hold the sum of the payments of an advance payment
		'-Se define la variable ldblSumAmout utilizada para almacenar la sumatoria de los abonos realizados a
		'-un anticipo.
		Dim ldblSumAmount As Double
		
		On Error GoTo Find_Err
		'+ Definición de parámetros para stored procedure 'reaLoans_2'
		'+ Información leída el 03/04/2001 04:37:39 p.m.
		lclsImprove_lo = New Improve_lo
		lrecreaLoans_2 = New eRemoteDB.Execute
		lclsLoans = New Loans
		With lrecreaLoans_2
			.StoredProcedure = "reaLoans_2"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find = .Run
			If Find Then
				While Not .EOF
					lclsLoans = Add(.FieldToClass("nCode"), .FieldToClass("dLoan_date"), .FieldToClass("nAmount"), .FieldToClass("nInterest"))
					ldblSumAmount = lclsImprove_lo.InsCalImprove_lo(nBranch, nPolicy, nProduct, nCertif, .FieldToClass("nCode"))
					lclsLoans.nSumAmount = .FieldToClass("nAmount") - ldblSumAmount
					lclsLoans.nBalance = .FieldToClass("nBalance")
					lclsLoans.nRequest_nu = .FieldToClass("nRequest_nu")
					lclsLoans.nAmotax = .FieldToClass("nAmotax")
					lclsLoans.dNextReceipt = .FieldToClass("dNextReceipt")
					lclsLoans.nInterestcap = .FieldToClass("nInterestcap")
					.RNext()
				End While
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsImprove_lo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsImprove_lo = Nothing
		'UPGRADE_NOTE: Object lrecreaLoans_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLoans_2 = Nothing
		'UPGRADE_NOTE: Object lclsLoans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLoans = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Loans
		Get
			'**used when referencing an element in the collection
			'**vntIndexKey contains either the Index or Key to the collection,
			'**this is why it is declared as a Variant
			'**Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'**used when retrieving the number of elements in the
			'**collection. Syntax: Debug.Print x.Count
			
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'**this property allows you to enumerate
			'**this collection with the For...Each syntax
			'
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'**used when removing an element from the collection
		'**vntIndexKey contains either the Index or Key, which is why
		'**it is declared as a Variant
		'**Syntax: x.Remove(xyz)
		
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'**creates the collection when this class is created
		
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'**destroys collection when this class is terminated
		
		'destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






