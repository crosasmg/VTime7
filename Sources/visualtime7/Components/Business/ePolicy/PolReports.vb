Option Strict Off
Option Explicit On
Public Class PolReports
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: PolReports.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colecci?n.
	
	Private mCol As Collection
	
	'+ Se definen las propiedades auxiliares.
	
	Private mstrCertype As String
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mintPolicy As Double
	Private mintCertif As Double
	Private mstrCodispl As String
	Private mintTransactype As Integer
	Private mdtmEffecdate As Date
	'% AddPolReport: Este m?todo permite a?adir registros a la colecci?n.
	Public Function AddPolReport(ByRef objClass As PolReport) As PolReport
		If objClass Is Nothing Then
			objClass = New PolReport
		End If
		
		With objClass
			mCol.Add(objClass, "A" & .sCodispl & .nTransactype)
		End With
		
		'return the object created
		AddPolReport = objClass
	End Function
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As PolReport
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
	'% FindPolReport: Verifica que exista informaci?n en la tabla de conmutativos.
	Public Function FindPolReport(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = True) As Boolean
		Dim lrecReaPolReport As eRemoteDB.Execute
		Dim lclsPolReport As PolReport
		
		On Error GoTo FindPolReport_Err
		FindPolReport = True
		If sCertype <> mstrCertype Or nBranch <> mintBranch Or nProduct <> mintProduct Or nPolicy <> mintPolicy Or nCertif <> mintCertif Or dEffecdate <> mdtmEffecdate Or lblnFind Then
			
			'+ Definici?n de par?metros para stored procedure
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			lrecReaPolReport = New eRemoteDB.Execute
			With lrecReaPolReport
				.StoredProcedure = "reaPolReport"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mstrCertype = sCertype
					mintBranch = nBranch
					mintProduct = nProduct
					mintPolicy = nPolicy
					mintCertif = nCertif
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
						lclsPolReport = New PolReport
						lclsPolReport.sCodispl = .FieldToClass("sCodispl")
						lclsPolReport.nTransactype = .FieldToClass("nTransactype")
						Call AddPolReport(lclsPolReport)
						'UPGRADE_NOTE: Object lclsPolReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsPolReport = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindPolReport = False
					mstrCertype = String.Empty
					mintBranch = 0
					mintProduct = 0
					mintPolicy = 0
					mintCertif = 0
					mdtmEffecdate = CDate(Nothing)
				End If
			End With
			
		End If
		
FindPolReport_Err: 
		If Err.Number Then
			FindPolReport = False
		End If
		'UPGRADE_NOTE: Object lrecReaPolReport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaPolReport = Nothing
		On Error GoTo 0
	End Function
End Class






