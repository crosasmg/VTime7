Option Strict Off
Option Explicit On
Public Class Contr_rate_Is
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Contr_rate_Is.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:28p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Public mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintNumber As Integer
	Private mintBranch_rei As Integer
	Private mintType As Integer
	Private mdtmStartdate As Date
	Private mintCovergen As Integer
	Private mintAge_ini As Integer
	Private mintAge_reinsu As Integer
	Private mdtmEffecdate As Date
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Contr_rate_I) As Contr_rate_I
		If objClass Is Nothing Then
			objClass = New Contr_rate_I
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nNumber & .nBranch_rei & .nType & .nCovergen & .nAge_ini & .nAge_reinsu & .dEffecdate)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Contr_rate_I
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
	Public Function Find(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaContr_rate_I As eRemoteDB.Execute
		Dim lclsContr_rate_I As Contr_rate_I
		
		On Error GoTo reaContr_rate_i_Err
		
		lrecReaContr_rate_I = New eRemoteDB.Execute
		Find = True
		
		'+
		'+ Definición de store procedure reaContr_rate_i al 03-27-2002 17:25:52
		'+
		With lrecReaContr_rate_I
			.StoredProcedure = "reaContr_rate_i"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsContr_rate_I = New Contr_rate_I
					With lclsContr_rate_I
						.nNumber = lrecReaContr_rate_I.FieldToClass("nNumber")
						.nBranch_rei = lrecReaContr_rate_I.FieldToClass("nBranch_rei")
						.nType = lrecReaContr_rate_I.FieldToClass("nType")
						.nCovergen = lrecReaContr_rate_I.FieldToClass("nCovergen")
						.nAge_ini = lrecReaContr_rate_I.FieldToClass("nAge_ini")
						.nAge_reinsu = lrecReaContr_rate_I.FieldToClass("nAge_reinsu")
						.dEffecdate = lrecReaContr_rate_I.FieldToClass("dEffecdate")
						.dNulldate = lrecReaContr_rate_I.FieldToClass("dNulldate")
						.nRatewomen = lrecReaContr_rate_I.FieldToClass("nRatewomen")
						.nPremwomen = lrecReaContr_rate_I.FieldToClass("nPremwomen")
						.nRatemen = lrecReaContr_rate_I.FieldToClass("nRatemen")
						.nPremmen = lrecReaContr_rate_I.FieldToClass("nPremmen")
						.dCompdate = lrecReaContr_rate_I.FieldToClass("dCompdate")
						.nUsercode = lrecReaContr_rate_I.FieldToClass("nUsercode")
					End With
					Call Add(lclsContr_rate_I)
					'UPGRADE_NOTE: Object lclsContr_rate_I may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsContr_rate_I = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaContr_rate_i_Err: 
		If Err.Number Then
			Find = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaContr_rate_I may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaContr_rate_I = Nothing
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
	Public Function FindCR726(ByVal nBranch_rei As Integer, ByVal nNumber As Integer, ByVal nType As Integer, ByVal nCovergen As Integer, ByVal nAge_ini As Integer, ByVal nAge_reinsu As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaContr_rate_i_o As eRemoteDB.Execute
		
		On Error GoTo reaContr_rate_i_o_Err
		
		lrecreaContr_rate_i_o = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaContr_rate_i_o al 04-02-2002 12:06:33
		'+
		With lrecreaContr_rate_i_o
			.StoredProcedure = "reaContr_rate_i_o"
			.Parameters.Add("nNumber", nNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_reinsu", nAge_reinsu, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				FindCR726 = True
			Else
				FindCR726 = False
			End If
		End With
		
reaContr_rate_i_o_Err: 
		If Err.Number Then
			FindCR726 = False
		End If
		'UPGRADE_NOTE: Object lrecreaContr_rate_i_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaContr_rate_i_o = Nothing
		On Error GoTo 0
		
	End Function
End Class






