Option Strict Off
Option Explicit On
Public Class Batch_logs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Batch_logs.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:39p                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'-variable local de la coleccion
	Private mCol As Collection
	
	'-Variables que guardan la llave de busqueda
	Private mstrKey As String
	Private mlngMessseq As Integer
	Private mlngMessline As Integer
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Batch_log) As Batch_log
		If objClass Is Nothing Then
			objClass = New Batch_log
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .sKey & .nMessseq & .nMessline)
		End With
		
		'+Return the object created
		Add = objClass
		
	End Function
	
	'% Item: Recupera un item de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Batch_log
		Get
			Item = mCol.Item(vntIndexKey)
			
		End Get
	End Property
	
	'%Count: Retorna la cantidad de registros de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Enumardor para operación For..Each
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'
			'NewEnum = mCol._NewEnum
			'
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: elimina un registro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		
		mCol.Remove(vntIndexKey)
		
	End Sub
	
	'%FindKey: Lee los datos de la tabla segun el campo sKey
	Public Function FindKey(ByVal sKey As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTmp_batch_log As eRemoteDB.Execute
		Dim lclsBatch_Log As Batch_log
		
		On Error GoTo reaTmp_batch_log_Err
		
		lrecreaTmp_batch_log = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaTmp_batch_log al 09-13-2002 16:13:05
		'+
		With lrecreaTmp_batch_log
			.StoredProcedure = "reaTmp_batch_log"
			.Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				FindKey = Not .EOF
				Do While Not .EOF
					lclsBatch_Log = New Batch_log
					lclsBatch_Log.sKey = sKey
					lclsBatch_Log.nMessseq = .FieldToClass("nMessseq")
					lclsBatch_Log.nMessline = .FieldToClass("nMessline")
					lclsBatch_Log.nMesscod = .FieldToClass("nMesscod")
					lclsBatch_Log.sLog = .FieldToClass("sLog")
					lclsBatch_Log.nUsercode = .FieldToClass("nUsercode")
					
					Call Add(lclsBatch_Log)
					'UPGRADE_NOTE: Object lclsBatch_Log may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsBatch_Log = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				FindKey = False
			End If
		End With
		
reaTmp_batch_log_Err: 
		If Err.Number Then
			FindKey = False
		End If
		'UPGRADE_NOTE: Object lrecreaTmp_batch_log may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTmp_batch_log = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Inicializa las varibales de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
		mstrKey = String.Empty
		mlngMessseq = eRemoteDB.Constants.intNull
		mlngMessline = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Libera objetos de la colección
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






