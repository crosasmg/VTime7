Option Strict Off
Option Explicit On
Public Class Bulletins_dets
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	'local variable to hold collection
	Private mCol As Collection
	
	Private mdblBulletins As Double
	Private mstrTypDoc As String
	Private mstrDocument As String
	
	'% Find: busca los datos correspondientes a un cliente
	Public Function Find(ByVal Bulletins As Double, ByVal Typdoc As String, ByVal Document As String) As Boolean
        Dim nCollecDocTyp As Object = New Object
        Dim lreaBulletins_det As eRemoteDB.Execute
		
		lreaBulletins_det = New eRemoteDB.Execute
		
		If Bulletins = mdblBulletins And nCollecDocTyp = mstrTypDoc And Document = mstrDocument Then
			Find = True
		Else
			
			'+ Definición de parámetros para stored procedure 'insudb.reaFinanc_cli'
			'+ Información leída el 11/01/2000 14:54:21
			
			With lreaBulletins_det
				.StoredProcedure = "reaBulletins_det"
				.Parameters.Add("nBulletins", Bulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nCollecDocTyp", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'.Parameters.Add "sDocument", Null, rdbParamInput, rdbVarChar, 15, 0, 0, rdbParamNullable
				If .Run Then
					Do While Not .EOF
						Call Add(1, Bulletins, .FieldToClass("nCollecDocTyp"), .FieldToClass("nContrat"), .FieldToClass("nDraft"), .FieldToClass("sCertype"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nReceipt"), .FieldToClass("nAmountPay"), .FieldToClass("nUsercode"), .FieldToClass("nExchange"), .FieldToClass("nPremium"), .FieldToClass("nPolicy"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					mdblBulletins = Bulletins
					mstrTypDoc = nCollecDocTyp
					mstrDocument = Document
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lreaBulletins_det may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lreaBulletins_det = Nothing
		End If
		
	End Function
	
	
	Public Function Add(ByVal StatusInstance As Integer, ByVal Bulletins As Double, ByVal nCollectDocTyp As Integer, ByVal Contrat As Double, ByVal Draft As Integer, ByVal certype As String, ByVal branch As Integer, ByVal product As Integer, ByVal Receipt As Double, ByVal Amountpay As Double, ByVal Usercode As Integer, ByVal nExchange As Double, ByVal nPremium As Double, ByVal nPolicy As Double) As Bulletins_det
        Dim Document As Object = New Object
        Dim Typdoc As Object = New Object

        'create a new object
        Dim objNewMember As Bulletins_det
		objNewMember = New Bulletins_det
		
		'set the properties passed into the method
		With objNewMember
			.nStatusInstance = StatusInstance
			.nBulletins = Bulletins
			.nCollecDocTyp = CStr(nCollectDocTyp)
			.nContrat = Contrat
			.nDraft = Draft
			.sCertype = certype
			.nBranch = branch
			.nProduct = product
			.nReceipt = Receipt
			.nAmountpay = Amountpay
			.nUsercode = Usercode
			.nExchange = nExchange
			.nPremium = nPremium
			.nPolicy = nPolicy
		End With
		
		mCol.Add(objNewMember, "BD" & Bulletins & Receipt & Typdoc & Document & Contrat & Draft)
		
		'return the object created
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Bulletins_det
		Get
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
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
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






