Option Strict Off
Option Explicit On
Public Class TimeGlobals
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: TimeGlobals.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**% Find: Reads the Recordset with all the records of the History of an error
	'%Find:Levanta el Recordset con todos los registros del Historico de un error
	Public Function Find() As Boolean
		Dim ltempReaTimeGlobal As New eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		ltempReaTimeGlobal = New eRemoteDB.Execute
		
		'** Parameter definition for the stored procedure 'insudb.reaErr_Histor'
		'Definición de parámetros para stored procedure 'insudb.reaErr_Histor'
		'** Information read on June 06,2001  11:29:15
		'Información leída el 06/06/2001 11:29:15
		
		mCol = New Collection
		
		With ltempReaTimeGlobal
			.StoredProcedure = "reaTimeGlobals"
			If .Run Then
				While Not .EOF
					Call Add(0, .FieldToClass("nTypeData"), .FieldToClass("sCode"), .FieldToClass("sDescript"))
					.RNext()
				End While
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object ltempReaTimeGlobal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ltempReaTimeGlobal = Nothing
	End Function
	
	'% Add: se agrega un elemento a la colección
	Public Function Add(ByVal nStatusinstance As Integer, ByVal nTypeData As Integer, ByVal sCode As String, ByVal sDescript As String) As TimeGlobal
		Dim objNewMember As TimeGlobal
		objNewMember = New TimeGlobal
		
		With objNewMember
			.nStatusinstance = nStatusinstance
			.nTypeData = nTypeData
			.sCode = sCode
			.sDescript = sDescript
		End With
		mCol.Add(objNewMember, "Global" & sCode)
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'* Item: utilizado al referenciar un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TimeGlobal
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el Nro. de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite enumerar los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: remueve un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: inicializa la instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: destruye la instancia de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






