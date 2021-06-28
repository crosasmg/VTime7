Option Strict Off
Option Explicit On
Public Class Contr_rate_IIs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_rate_IIs.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contr_rate_II
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
	
	'Find: Valida que el registro a duplicar no exista en Contr_rate_ii
	Public Function Find(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal dEffecdate As Date) As Object
		
		Dim lrecreaContr_rate_II As eRemoteDB.Execute
		Dim lclsContr_rate_II As eCoReinsuran.Contr_rate_II
		
		On Error GoTo Find_Err
		
		lrecreaContr_rate_II = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaContr_rate_ii al 04-04-2002 11:58:22
		'+
		With lrecreaContr_rate_II
			.StoredProcedure = "reaContr_rate_ii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsContr_rate_II = New eCoReinsuran.Contr_rate_II
					With lclsContr_rate_II
						.nNumber = lrecreaContr_rate_II.FieldToClass("nNumber")
						.nBranch_rei = lrecreaContr_rate_II.FieldToClass("nBranch_rei")
						.nType = lrecreaContr_rate_II.FieldToClass("nType")
						.nCovergen = lrecreaContr_rate_II.FieldToClass("nCovergen")
						.sSmoking = lrecreaContr_rate_II.FieldToClass("sSmoking")
						.sPeriodpol = lrecreaContr_rate_II.FieldToClass("sPeriodpol")
						.nTyperisk = lrecreaContr_rate_II.FieldToClass("nTyperisk")
						.nCap_ini = lrecreaContr_rate_II.FieldToClass("nCap_ini")
						.nAge_reinsu = lrecreaContr_rate_II.FieldToClass("nAge_reinsu")
						.dEffecdate = lrecreaContr_rate_II.FieldToClass("dEffecdate")
						.nCap_end = lrecreaContr_rate_II.FieldToClass("nCap_end")
						.dNulldate = lrecreaContr_rate_II.FieldToClass("dNulldate")
						.nRatewomen = lrecreaContr_rate_II.FieldToClass("nRatewomen")
						.nPremwomen = lrecreaContr_rate_II.FieldToClass("nPremwomen")
						.nRatemen = lrecreaContr_rate_II.FieldToClass("nRatemen")
						.nPremmen = lrecreaContr_rate_II.FieldToClass("nPremmen")
						.dCompdate = lrecreaContr_rate_II.FieldToClass("dCompdate")
						.nUsercode = lrecreaContr_rate_II.FieldToClass("nUsercode")
					End With
					Call Add(lclsContr_rate_II)
					'UPGRADE_NOTE: Object lclsContr_rate_II may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsContr_rate_II = Nothing
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
		'UPGRADE_NOTE: Object lrecreaContr_rate_II may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContr_rate_II = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Contr_rate_II) As Contr_rate_II
		If objClass Is Nothing Then
			objClass = New Contr_rate_II
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nNumber & .nBranch_rei & .nType & .nCovergen & .sSmoking & .sPeriodpol & .nTyperisk & .nCap_ini & .nAge_reinsu & .dEffecdate)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	
	Public Function FindCR765(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal sSmoking As String, ByVal sPeriodpol As String, ByVal nTyperisk As Integer, ByVal nCap_ini As Double, ByVal nAge_reinsu As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lrecreaContr_rate_ii_o As eRemoteDB.Execute
		Dim lclsreaContr_rate_II As eCoReinsuran.Contr_rate_II
		
		On Error GoTo reaContr_rate_ii_o_Err
		
		lrecreaContr_rate_ii_o = New eRemoteDB.Execute
		
		'+ Definición de store procedure reaContr_rate_ii_o al 04-05-2002 09:43:55
		
		With lrecreaContr_rate_ii_o
			.StoredProcedure = "reaContr_rate_ii_o"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodpol", sPeriodpol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCap_ini", nCap_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindCR765 = True
				lclsreaContr_rate_II = New eCoReinsuran.Contr_rate_II
				With lclsreaContr_rate_II
					.nNumber = lrecreaContr_rate_ii_o.FieldToClass("nNumber")
					.nBranch_rei = lrecreaContr_rate_ii_o.FieldToClass("nBranch_rei")
					.nType = lrecreaContr_rate_ii_o.FieldToClass("nType")
					.nCovergen = lrecreaContr_rate_ii_o.FieldToClass("nCovergen")
					.sSmoking = lrecreaContr_rate_ii_o.FieldToClass("sSmoking")
					.sPeriodpol = lrecreaContr_rate_ii_o.FieldToClass("sPeriodpol")
					.nTyperisk = lrecreaContr_rate_ii_o.FieldToClass("nTyperisk")
					.nCap_ini = lrecreaContr_rate_ii_o.FieldToClass("nCap_ini")
					.nAge_reinsu = lrecreaContr_rate_ii_o.FieldToClass("nAge_reinsu")
					.dEffecdate = lrecreaContr_rate_ii_o.FieldToClass("dEffecdate")
					.nCap_end = lrecreaContr_rate_ii_o.FieldToClass("nCap_end")
					.dNulldate = lrecreaContr_rate_ii_o.FieldToClass("dNulldate")
					.nRatewomen = lrecreaContr_rate_ii_o.FieldToClass("nRatewomen")
					.nPremwomen = lrecreaContr_rate_ii_o.FieldToClass("nPremwomen")
					.nRatemen = lrecreaContr_rate_ii_o.FieldToClass("nRatemen")
					.nPremmen = lrecreaContr_rate_ii_o.FieldToClass("nPremmen")
					.dCompdate = lrecreaContr_rate_ii_o.FieldToClass("dCompdate")
					.nUsercode = lrecreaContr_rate_ii_o.FieldToClass("nUsercode")
				End With
			Else
				FindCR765 = False
			End If
		End With
		
reaContr_rate_ii_o_Err: 
		If Err.Number Then
			FindCR765 = False
		End If
		'UPGRADE_NOTE: Object lrecreaContr_rate_ii_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContr_rate_ii_o = Nothing
		On Error GoTo 0
	End Function
End Class






