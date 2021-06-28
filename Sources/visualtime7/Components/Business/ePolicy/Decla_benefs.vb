Option Strict Off
Option Explicit On
Public Class Decla_benefs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Decla_benefs.cls                         $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 26/09/03 13.20                               $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	
	'-Local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mstrCertype As String
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mintPolicy As Double
	Private mintCertif As Double
	Private mdtmEffecdate As Date
	
	'%Add:
	Public Function Add(ByRef objClass As Decla_benef) As Decla_benef
		If objClass Is Nothing Then
			objClass = New Decla_benef
		End If
		
		With objClass
			mCol.Add(objClass, "DB" & .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .dEffecdate.ToString("yyyyMMdd") & .nNumdecla & .sIrrevoc & .dDatedecla)
			
		End With
		
		'return the object created
		Add = objClass
	End Function
	
	'%Item:
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Decla_benef
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
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecDate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaDecla_benef As eRemoteDB.Execute
		Dim lclsDecla_benef As Decla_benef
		
		On Error GoTo Find_Err
		Find = True
		
		If mstrCertype <> sCertype Or mintBranch <> nBranch Or mlngProduct <> nProduct Or mintPolicy <> nPolicy Or mintCertif <> nCertif Or mdtmEffecdate <> dEffecDate Or lblnFind Then
			
			lrecReaDecla_benef = New eRemoteDB.Execute
			
			mstrCertype = sCertype
			mintBranch = nBranch
			mlngProduct = nProduct
			mintPolicy = nPolicy
			mintCertif = nCertif
			mdtmEffecdate = dEffecDate
			
			'+Definición de parámetros para stored procedure 'ReaDecla_benef_a'
			With lrecReaDecla_benef
				.StoredProcedure = "ReaDecla_benef_a"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsDecla_benef = New Decla_benef
						lclsDecla_benef.sCertype = sCertype
						lclsDecla_benef.nBranch = nBranch
						lclsDecla_benef.nProduct = nProduct
						lclsDecla_benef.nPolicy = nPolicy
						lclsDecla_benef.nCertif = nCertif
						lclsDecla_benef.nNumdecla = .FieldToClass("nNumdecla")
						lclsDecla_benef.dEffecDate = .FieldToClass("dEffecdate")
						lclsDecla_benef.sIrrevoc = .FieldToClass("sIrrevoc")
						lclsDecla_benef.dDatedecla = .FieldToClass("dDatedecla")
						lclsDecla_benef.dNulldate = .FieldToClass("dNulldate")
						Call Add(lclsDecla_benef)
						.RNext()
						'UPGRADE_NOTE: Object lclsDecla_benef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsDecla_benef = Nothing
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
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaDecla_benef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaDecla_benef = Nothing
	End Function
End Class






