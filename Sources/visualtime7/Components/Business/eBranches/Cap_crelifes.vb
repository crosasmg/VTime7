Option Strict Off
Option Explicit On
Public Class Cap_crelifes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Cap_crelifes.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'-Local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mintModulec As Integer
	Private mintCover As Integer
	Private mdtmEffecdate As Date
	
	'%Add:
	Public Function Add(ByRef objClass As Cap_crelife) As Cap_crelife
		If objClass Is Nothing Then
			objClass = New Cap_crelife
		End If
		
		With objClass
			mCol.Add(objClass, "TL" & .nBranch & .nProduct & .nModulec & .nCover & .nDuration & .nYear & .dEffecdate.ToString("yyyyMMdd") & .nCapital & .nCurrency)
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	'%Item:
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cap_crelife
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count:
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum:
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
	
	'%Remove:
	Public Sub Remove(ByRef vntIndexKey As Object)
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize:
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate:
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
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal lbFind As Boolean = False) As Boolean
		Dim lrecReaCap_crelife As eRemoteDB.Execute
		Dim lclsCap_crelife As Cap_crelife
		
		On Error GoTo Find_Err
		Find = True
		
		If mintBranch <> nBranch Or mlngProduct <> nProduct Or mintModulec <> nModulec Or mintCover <> nCover Or mdtmEffecdate <> dEffecdate Or lbFind Then
			
			lrecReaCap_crelife = New eRemoteDB.Execute
			
			mintBranch = nBranch
			mlngProduct = nProduct
			mintModulec = nModulec
			mintCover = nCover
			mdtmEffecdate = dEffecdate
			
			'+Definición de parámetros para stored procedure 'ReaCap_crelife'
			With lrecReaCap_crelife
				.StoredProcedure = "ReaCap_crelife"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsCap_crelife = New Cap_crelife
						lclsCap_crelife.nBranch = nBranch
						lclsCap_crelife.nProduct = nProduct
						lclsCap_crelife.nModulec = nModulec
						lclsCap_crelife.nCover = nCover
						lclsCap_crelife.nDuration = .FieldToClass("nDuration")
						lclsCap_crelife.nYear = .FieldToClass("nYear")
						lclsCap_crelife.dEffecdate = .FieldToClass("dEffecdate")
						lclsCap_crelife.nCapital = .FieldToClass("nCapital")
						lclsCap_crelife.dNulldate = .FieldToClass("dNulldate")
						lclsCap_crelife.nCurrency = .FieldToClass("nCurrency")
						Call Add(lclsCap_crelife)
						.RNext()
						'UPGRADE_NOTE: Object lclsCap_crelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCap_crelife = Nothing
					Loop 
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecReaCap_crelife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCap_crelife = Nothing
		On Error GoTo 0
	End Function
End Class






