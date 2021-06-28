Option Strict Off
Option Explicit On
Public Class TRehabilitates
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: TRehabilitates.cls                       $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.02                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlosCamposLlave As Object
	Private mdtmEffecdate As Date
	
	
	Public Function Add(ByVal sKey As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nStatquota As Integer, ByVal nCapital As Integer, ByVal sCurrency As String, ByVal nOffice As Integer, ByVal sNameexec As String, ByVal dEffecdate As Date, ByVal sClient_age As String, ByVal sCliename_age As String, ByVal nPayfreq As Integer, ByVal nId As Integer, ByVal nReceipt_a As Integer, ByVal dEffecdate_a As Date, ByVal nPremium_a As Double, ByVal sClient_a As String, ByVal sCliename_a As String, ByVal nReceipt_d As Integer, ByVal dEffecdate_d As Date, ByVal nPremium_d As Double, ByVal sTratypei As String, ByVal sClient_d As String, ByVal sCliename_d As String, ByVal nPremium_tot As Double) As TRehabilitate
		'--------------------- ---------------------------------------------------------------------
		Dim objNewMember As TRehabilitate
		objNewMember = New TRehabilitate
		With objNewMember
			.sKey = sKey
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nStatquota = nStatquota
			.nCapital = nCapital
			.sCurrency = sCurrency
			.nOffice = nOffice
			.sNameexec = sNameexec
			.dEffecdate = dEffecdate
			.sClient_age = sClient_age
			.sCliename_age = sCliename_age
			.nPayfreq = nPayfreq
			.nId = nId
			.nReceipt_a = nReceipt_a
			.dEffecdate_a = dEffecdate_a
			.nPremium_a = nPremium_a
			.sClient_a = sClient_a
			.sCliename_a = sCliename_a
			.nReceipt_d = nReceipt_d
			.dEffecdate_d = dEffecdate_d
			.nPremium_d = nPremium_d
			.sTratypei = sTratypei
			.sClient_d = sClient_d
			.sCliename_d = sCliename_d
			.nPremium_tot = nPremium_tot
		End With
		mCol.Add(objNewMember)
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TRehabilitate
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	Public Function Find(ByVal sKey As String) As Boolean
		
		Dim lrecReatRehabilitate As eRemoteDB.Execute
		
		lrecReatRehabilitate = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecReatRehabilitate
			.StoredProcedure = "reatRehabilitate"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("sKey"), .FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nStatquota"), .FieldToClass("nCapital"), .FieldToClass("sCurrency"), .FieldToClass("nOffice"), .FieldToClass("sNameexec"), .FieldToClass("dEffecdate"), .FieldToClass("sClient_age"), .FieldToClass("sCliename_age"), .FieldToClass("nPayfreq"), .FieldToClass("nId"), .FieldToClass("nReceipt_a"), .FieldToClass("dEffecdate_a"), .FieldToClass("nPremium_a"), .FieldToClass("sClient_a"), .FieldToClass("sCliename_a"), .FieldToClass("nReceipt_d"), .FieldToClass("dEffecdate_d"), .FieldToClass("nPremium_d"), .FieldToClass("sTratypei"), .FieldToClass("sClient_d"), .FieldToClass("sCliename_d"), .FieldToClass("nPremium_tot"))
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecReatRehabilitate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReatRehabilitate = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
End Class






