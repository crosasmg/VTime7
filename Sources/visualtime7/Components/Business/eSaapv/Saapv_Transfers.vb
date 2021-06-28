Option Strict Off
Option Explicit On
Public Class Saapv_Transfers
	Implements System.Collections.IEnumerable
	
	
	Private mCol As Collection
	
	Public nCount As Integer
	
	'% Add: Añade una nueva instancia de la clase Saapv_Transfer a la colección
	Public Function Add(ByVal nCod_saapv As Double, ByVal nFunds_origin As Integer, ByVal nTax_regime As Integer, ByVal sAfp_type As String, ByVal nType_transfer As Integer, ByVal nSaving_Loc As Double, ByVal nSaving_UF As Double, ByVal nSaving_PCT As Double, ByVal nInstitution As Integer) As Saapv_Transfer
		'create a new object
		
		Dim objNewMember As Saapv_Transfer
		objNewMember = New Saapv_Transfer
		
		With objNewMember
			.nCod_saapv = nCod_saapv
			.nFunds_origin = nFunds_origin
			.nTax_regime = nTax_regime
			.sAfp_type = sAfp_type
			.nType_transfer = nType_transfer
			.nSaving_Loc = nSaving_Loc
			.nSaving_UF = nSaving_UF
			.nSaving_PCT = nSaving_PCT
			.nInstitution = nInstitution
		End With
		
		mCol.Add(objNewMember)
		
		Add = objNewMember
		objNewMember = Nothing
		
	End Function
	Public Function Find(ByVal nCod_saapv As Double, ByVal nInstitution As Integer) As Boolean
		'+ Se define la variable lrecSaapv_Transfers que se utilizará como cursor.
		Dim lrecReaSaapv_Transfers As eRemoteDB.Execute
		
		lrecReaSaapv_Transfers = New eRemoteDB.Execute
		
		With lrecReaSaapv_Transfers
			.StoredProcedure = "insvi7501_F_pkg.ReaSaapv_Transfer"
			
			.Parameters.Add("nCod_saapv", nCod_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInstitution", nInstitution, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If Not .Run Then
				Find = False
			Else
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nCod_saapv"), .FieldToClass("nFunds_origin"), .FieldToClass("nTax_regime"), .FieldToClass("sAfp_type"), .FieldToClass("nType_transfer"), .FieldToClass("nSaving_Loc"), .FieldToClass("nSaving_UF"), .FieldToClass("nSaving_PCT"), .FieldToClass("nInstitution"))
					.RNext()
				Loop 
			End If
		End With
		lrecReaSaapv_Transfers = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Saapv_Transfer
		Get
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Elimina un elemento de la colección
	'---------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'---------------------------------------------
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






