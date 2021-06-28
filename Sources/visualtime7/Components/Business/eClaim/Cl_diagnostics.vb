Option Strict Off
Option Explicit On
Public Class Cl_diagnostics
	Implements System.Collections.IEnumerable
	'+ Local variable to hold collection
	Private mCol As Collection

    '**Add:  It adds a new element to the collection
    '% Add: añade un nuevo elemento a la colección
    Public Sub Add(ByVal lclsDiagnostic As Cl_diagnostic)
        mCol.Add(lclsDiagnostic, "CLD" & lclsDiagnostic.dDiag_date)
    End Sub

    '% Find: Busca los datos asosiados al Diagnóstico
    Public Function Find(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsDiagnostic As Cl_diagnostic
		Dim lrecreaCl_diagnostic As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaCl_diagnostic = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaCl_diagnostic'
		'+ Información leída el 14/07/2001 05:11:24 p.m.
		
		With lrecreaCl_diagnostic
			.StoredProcedure = "reaCl_diagnostic"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsDiagnostic = New Cl_diagnostic
					lclsDiagnostic.dDiag_date = .FieldToClass("dDiag_date")
					lclsDiagnostic.sDescript = .FieldToClass("sDescript")
					lclsDiagnostic.nNotenum = .FieldToClass("nNotenum")
					lclsDiagnostic.nEvalStat = .FieldToClass("nEvalStat")
					Call Add(lclsDiagnostic)
					.RNext()
					lclsDiagnostic = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		lclsDiagnostic = Nothing
		lrecreaCl_diagnostic = Nothing
	End Function
	
	'%Item: Obtiene el valor del elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Cl_diagnostic
		Get
			'used when referencing an element in the collection
			'vntIndexKey contains either the Index or Key to the collection,
			'this is why it is declared as a Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Obtiene el número de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			'used when retrieving the number of elements in the
			'collection. Syntax: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Obtiene el número de un elemento de la colección
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		'used when removing an element from the collection
		'vntIndexKey contains either the Index or Key, which is why
		'it is declared as a Variant
		'Syntax: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Inicializa los elementos de la colección
	Private Sub Class_Initialize_Renamed()
		'creates the collection when this class is created
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Destruye los elementos involucrados en la colección
	Private Sub Class_Terminate_Renamed()
		'destroys collection when this class is terminated
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






