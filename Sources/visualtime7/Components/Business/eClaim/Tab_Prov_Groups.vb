Option Strict Off
Option Explicit On
Public Class Tab_Prov_Groups
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Prov_Groups.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'- local variable to hold collection
	Private mCol As Collection
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByVal objClass As Tab_Prov_Group) As Tab_Prov_Group
		mCol.Add(objClass)
		
		'+ Devolver el objeto creado
		Add = objClass
	End Function
	'% Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Prov_Group
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
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	'% Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nProvider As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_prov_group As eRemoteDB.Execute
		Dim lclsTab_Prov_Group As Tab_Prov_Group
		On Error GoTo Find_Err
		Find = True
		lrecreaTab_prov_group = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaTab_Prov_Group_a'
		With lrecreaTab_prov_group
			.StoredProcedure = "ReaTab_Prov_Group_a"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_Prov_Group = New Tab_Prov_Group
					lclsTab_Prov_Group.nProvider = nProvider
					lclsTab_Prov_Group.nProv_group = .FieldToClass("nProv_group")
					lclsTab_Prov_Group.dInpdate = .FieldToClass("dInpdate")
					lclsTab_Prov_Group.dOutdate = .FieldToClass("dOutdate")
					Call Add(lclsTab_Prov_Group)
					.RNext()
					lclsTab_Prov_Group = Nothing
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
		lrecreaTab_prov_group = Nothing
		On Error GoTo 0
	End Function
	'% Class_Initialize: controla la apertura de cada instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'% Class_Terminate: elimina la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






