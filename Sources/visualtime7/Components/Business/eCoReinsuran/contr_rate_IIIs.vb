Option Strict Off
Option Explicit On
Public Class contr_rate_IIIs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: contr_rate_IIIs.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlosCamposLlave As Object
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As contr_rate_III) As contr_rate_III
		If objClass Is Nothing Then
			objClass = New contr_rate_III
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nNumber & .nBranch_rei & .nType & .nCovergen & .nDeductible & .nQFamily & .nCapital & .nAge_reinsu & .dEffecdate)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As contr_rate_III
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
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Integer, ByVal nQFamily As Integer, ByVal nCapital As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaContr_rate_iii As eRemoteDB.Execute
		Dim lclsContr_rate_III As eCoReinsuran.contr_rate_III
		
		On Error GoTo reaContr_rate_iii_Err
		
		lrecreaContr_rate_iii = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaContr_rate_iii al 04-09-2002 09:54:27
		'+
		With lrecreaContr_rate_iii
			.StoredProcedure = "reaContr_rate_iii"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsContr_rate_III = New eCoReinsuran.contr_rate_III
					With lclsContr_rate_III
						.nNumber = lrecreaContr_rate_iii.FieldToClass("nNumber")
						.nBranch_rei = lrecreaContr_rate_iii.FieldToClass("nBranch_rei")
						.nType = lrecreaContr_rate_iii.FieldToClass("nType")
						.nCovergen = lrecreaContr_rate_iii.FieldToClass("nCovergen")
						.nDeductible = lrecreaContr_rate_iii.FieldToClass("nDeductible")
						.nQFamily = lrecreaContr_rate_iii.FieldToClass("nQfamily")
						.nCapital = lrecreaContr_rate_iii.FieldToClass("nCapital")
						.nAge_reinsu = lrecreaContr_rate_iii.FieldToClass("nAge_reinsu")
						.dEffecdate = lrecreaContr_rate_iii.FieldToClass("dEffecdate")
						.dNulldate = lrecreaContr_rate_iii.FieldToClass("dNulldate")
						.nRate = lrecreaContr_rate_iii.FieldToClass("nRate")
						.nPremium = lrecreaContr_rate_iii.FieldToClass("nPremium")
						.dCompdate = lrecreaContr_rate_iii.FieldToClass("dCompdate")
						.nUsercode = lrecreaContr_rate_iii.FieldToClass("nUsercode")
					End With
					Call Add(lclsContr_rate_III)
					'UPGRADE_NOTE: Object lclsContr_rate_III may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsContr_rate_III = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaContr_rate_iii_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaContr_rate_iii may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContr_rate_iii = Nothing
		On Error GoTo 0
		
	End Function
	
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
	
	'%Find: Lee los datos de la tabla
	Public Function FindCR766(ByVal nNumber As Integer, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nDeductible As Integer, ByVal nQFamily As Integer, ByVal nCapital As Double, ByVal nAge_reinsu As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaContr_rate_iii_o As eRemoteDB.Execute
		Dim lclsreaContr_rate_III As eCoReinsuran.contr_rate_III
		
		On Error GoTo reaContr_rate_iii_o_Err
		
		lrecreaContr_rate_iii_o = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaContr_rate_iii_o al 04-09-2002 16:50:54
		'+
		With lrecreaContr_rate_iii_o
			.StoredProcedure = "reaContr_rate_iii_o"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeductible", nDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQfamily", nQFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindCR766 = True
				lclsreaContr_rate_III = New eCoReinsuran.contr_rate_III
				With lclsreaContr_rate_III
					.nNumber = lrecreaContr_rate_iii_o.FieldToClass("nNumber")
					.nBranch_rei = lrecreaContr_rate_iii_o.FieldToClass("nBranch_rei")
					.nType = lrecreaContr_rate_iii_o.FieldToClass("nType")
					.nCovergen = lrecreaContr_rate_iii_o.FieldToClass("nCovergen")
					.nDeductible = lrecreaContr_rate_iii_o.FieldToClass("nDeductible")
					.nQFamily = lrecreaContr_rate_iii_o.FieldToClass("nQfamily")
					.nCapital = lrecreaContr_rate_iii_o.FieldToClass("nCapital")
					.nAge_reinsu = lrecreaContr_rate_iii_o.FieldToClass("nAge_reinsu")
					.dEffecdate = lrecreaContr_rate_iii_o.FieldToClass("dEffecdate")
					.dNulldate = lrecreaContr_rate_iii_o.FieldToClass("dNulldate")
					.nRate = lrecreaContr_rate_iii_o.FieldToClass("nRate")
					.nPremium = lrecreaContr_rate_iii_o.FieldToClass("nPremium")
					.dCompdate = lrecreaContr_rate_iii_o.FieldToClass("dCompdate")
					.nUsercode = lrecreaContr_rate_iii_o.FieldToClass("nUsercode")
				End With
			Else
				FindCR766 = False
			End If
		End With
		
reaContr_rate_iii_o_Err: 
		If Err.Number Then
			FindCR766 = False
		End If
		'UPGRADE_NOTE: Object lrecreaContr_rate_iii_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContr_rate_iii_o = Nothing
		On Error GoTo 0
		
	End Function
End Class






