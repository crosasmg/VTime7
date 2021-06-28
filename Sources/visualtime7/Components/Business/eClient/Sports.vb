Option Strict Off
Option Explicit On
Public Class Sports
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Sports.cls                               $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:30p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'% Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Sport) As Sport
		If objClass Is Nothing Then
			objClass = New Sport
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & .nSport & .sClient)
		End With
		
		'Return the object created
		Add = objClass
		
	End Function
	
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Sport
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: enumera los elementos dentro de la colección
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
	
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Find: Lee los datos de la tabla
	Public Function Find(ByVal sClient As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaSport As eRemoteDB.Execute
		Dim lclsSport As Sport
		
		On Error GoTo reaSport_Err
		lrecreaSport = New eRemoteDB.Execute
		
		With lrecreaSport
			.StoredProcedure = "reaSport"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsSport = New Sport
					lclsSport.nSport = .FieldToClass("nSport")
					lclsSport.sDescript = .FieldToClass("sDescript")
					lclsSport.sSel = .FieldToClass("sSel")
					
					Call Add(lclsSport)
					'UPGRADE_NOTE: Object lclsSport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsSport = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
reaSport_Err: 
		If Err.Number Then
			Find = False
		End If
		'UPGRADE_NOTE: Object lrecreaSport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSport = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Find_by_client: Lee los deportes de un cliente'--------------------------------------
	Public Function Find_by_client(ByVal sClient As String, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaSport As eRemoteDB.Execute
		Dim lclsSport As Sport
		
		On Error GoTo reaSport_by_client_Err
		lrecreaSport = New eRemoteDB.Execute
		
		With lrecreaSport
			.StoredProcedure = "reaSport_by_client"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_by_client = True
				Do While Not .EOF
					lclsSport = New Sport
					lclsSport.nSport = .FieldToClass("nSport")
					lclsSport.sDescript = .FieldToClass("sDescript")
					lclsSport.sSel = .FieldToClass("sSel")
					
					Call Add(lclsSport)
					'UPGRADE_NOTE: Object lclsSport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsSport = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find_by_client = False
			End If
		End With
		
reaSport_by_client_Err: 
		If Err.Number Then
			Find_by_client = False
		End If
		'UPGRADE_NOTE: Object lrecreaSport may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSport = Nothing
		On Error GoTo 0
		
	End Function
	
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: elimina la colección
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






