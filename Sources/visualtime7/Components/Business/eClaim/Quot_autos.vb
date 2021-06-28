Option Strict Off
Option Explicit On
Public Class Quot_autos
	Implements System.Collections.IEnumerable
	
	'- local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Quot_auto) As Quot_auto
		If objClass Is Nothing Then
			objClass = New Quot_auto
		End If
		
		With objClass
			mCol.Add(objClass, "QA" & .nServ_ord & .nId)
		End With
		
		'Return the object created
		Add = objClass
		objClass = Nothing
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Quot_auto
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
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'this property allows you to enumerate
			'this collection with the For...Each syntax
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
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
	Public Function Find(ByVal nServ_ord As Double, ByVal nOrdertype As Short, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecReaQuot_auto As eRemoteDB.Execute
		Dim lclsQuot_auto As Quot_auto
		
		On Error GoTo Find_Err
		
		lrecReaQuot_auto = New eRemoteDB.Execute
		
		Find = True
		
		'+Definición de parámetros para stored procedure 'ReaQuot_auto_a'
		With lrecReaQuot_auto
			.StoredProcedure = "ReaQuot_auto_a"
			.Parameters.Add("nServ_ord", nServ_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrdertype ", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsQuot_auto = New Quot_auto
					lclsQuot_auto.nServ_ord = .FieldToClass("nServ_ord")
					lclsQuot_auto.nId = .FieldToClass("nId")
					lclsQuot_auto.dQuot_date = .FieldToClass("dQuot_date")
					lclsQuot_auto.nQuantity = .FieldToClass("nQuantity")
					lclsQuot_auto.sDescript = .FieldToClass("sDescript")
					lclsQuot_auto.nVehbrand = .FieldToClass("nVehbrand")
					lclsQuot_auto.sVehmodel = .FieldToClass("sVehmodel")
					lclsQuot_auto.nAmount = .FieldToClass("nAmount")
					lclsQuot_auto.nyear = .FieldToClass("nYear")
					lclsQuot_auto.sCliename = .FieldToClass("sCliename")
					lclsQuot_auto.sSel = .FieldToClass("sSel")
					Call Add(lclsQuot_auto)
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
		On Error GoTo 0
		lrecReaQuot_auto = Nothing
	End Function
	
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






