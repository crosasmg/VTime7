Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("tar_asistviajes_NET.tar_asistviajes")> Public Class tar_asistviajes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_asistviajes.cls                      $%'
	'% $Author:: Rnavarre                                   $%'
	'% $Date:: 21/04/06 16:22                               $%'
	'% $Revision:: 1                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_asistviaje
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
	
	Public Function Add(ByVal objClass As Tar_asistviaje) As Tar_asistviaje
		If objClass Is Nothing Then
			objClass = New Tar_asistviaje
		End If
		
		With objClass
			mCol.Add(objClass, .nBranch & .nProduct & .nNumber & .nBranch_rei & .nCovergen & .nCapital & .dEffecdate & .nDay_ini)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	Public Function FindCR768(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nCovergen As Integer, ByVal nCapital As Integer, ByVal dEffecdate As Date, ByVal nDay_ini As Integer) As Boolean
		
		Dim lrec_reatarasistviaje_o As eRemoteDB.Execute
		Dim lcls_reatarasistviaje As eCoReinsuran.Tar_asistviaje
		
		On Error GoTo FindCR768_Err
		
		lrec_reatarasistviaje_o = New eRemoteDB.Execute
		
		With lrec_reatarasistviaje_o
			.StoredProcedure = "reacontr_tarasistviaje_ii"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDay_ini", nDay_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindCR768 = True
				lcls_reatarasistviaje = New eCoReinsuran.Tar_asistviaje
				With lcls_reatarasistviaje
					.nBranch = lrec_reatarasistviaje_o.FieldToClass("nBranch")
					.nProduct = lrec_reatarasistviaje_o.FieldToClass("nProduct")
					.nNumber = lrec_reatarasistviaje_o.FieldToClass("nNumber")
					.nBranch_rei = lrec_reatarasistviaje_o.FieldToClass("nBranch_rei")
					.nCovergen = lrec_reatarasistviaje_o.FieldToClass("nCovergen")
					.nCapital = lrec_reatarasistviaje_o.FieldToClass("nCapital")
					.dEffecdate = lrec_reatarasistviaje_o.FieldToClass("dEffecdate")
					.nDay_ini = lrec_reatarasistviaje_o.FieldToClass("nDay_ini")
					.nDay_end = lrec_reatarasistviaje_o.FieldToClass("nDay_end")
					.nTar_min = lrec_reatarasistviaje_o.FieldToClass("nTar_min")
					.nTar_adic = lrec_reatarasistviaje_o.FieldToClass("nTar_adic")
					.dCompdate = lrec_reatarasistviaje_o.FieldToClass("dCompdate")
					.nUsercode = lrec_reatarasistviaje_o.FieldToClass("nUsercode")
				End With
			Else
				FindCR768 = False
			End If
		End With
		
FindCR768_Err: 
		If Err.Number Then
			FindCR768 = False
		End If
		'UPGRADE_NOTE: Object lrec_reatarasistviaje_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_reatarasistviaje_o = Nothing
		On Error GoTo 0
	End Function
	
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nNumber As Integer, ByVal nBranchRei As Integer, ByVal nCovergen As Integer, ByVal nCapital As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrec_reatarasistviaje_o As eRemoteDB.Execute
		Dim lcls_reatarasistviaje As eCoReinsuran.Tar_asistviaje
		
		On Error GoTo Find_Err
		
		lrec_reatarasistviaje_o = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaContr_rate_ii_o al 04-05-2002 09:43:55
		
		With lrec_reatarasistviaje_o
			.StoredProcedure = "reacontr_tarasistviaje"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lcls_reatarasistviaje = New eCoReinsuran.Tar_asistviaje
					With lcls_reatarasistviaje
						.nBranch = lrec_reatarasistviaje_o.FieldToClass("nBranch")
						.nProduct = lrec_reatarasistviaje_o.FieldToClass("nProduct")
						.nNumber = lrec_reatarasistviaje_o.FieldToClass("nNumber")
						.nBranch_rei = lrec_reatarasistviaje_o.FieldToClass("nBranch_rei")
						.nCovergen = lrec_reatarasistviaje_o.FieldToClass("nCovergen")
						.nCapital = lrec_reatarasistviaje_o.FieldToClass("nCapital")
						.dEffecdate = lrec_reatarasistviaje_o.FieldToClass("dEffecdate")
						.nDay_ini = lrec_reatarasistviaje_o.FieldToClass("nDay_ini")
						.nDay_end = lrec_reatarasistviaje_o.FieldToClass("nDay_end")
						.nTar_min = lrec_reatarasistviaje_o.FieldToClass("nTar_min")
						.nTar_adic = lrec_reatarasistviaje_o.FieldToClass("nTar_adic")
						.dCompdate = lrec_reatarasistviaje_o.FieldToClass("dCompdate")
						.nUsercode = lrec_reatarasistviaje_o.FieldToClass("nUsercode")
					End With
					Call Add(lcls_reatarasistviaje)
					'UPGRADE_NOTE: Object lcls_reatarasistviaje may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lcls_reatarasistviaje = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrec_reatarasistviaje_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrec_reatarasistviaje_o = Nothing
		On Error GoTo 0
	End Function
End Class






