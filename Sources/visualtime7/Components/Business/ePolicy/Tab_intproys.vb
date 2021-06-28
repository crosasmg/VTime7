Option Strict Off
Option Explicit On
Public Class Tab_intproys
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_intproys.cls                         $%'
	'% $Author:: Mvazquez                                   $%'
	'% $Date:: 10-09-15 8:11                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Añade una nueva instancia de la clase a la colección
	Public Function Add(ByVal lclsTab_intproy As Tab_intproy) As Tab_intproy
		With lclsTab_intproy
			mCol.Add(lclsTab_intproy, .dEffecdate.ToString("yyyyMMdd"))
		End With
		'+ Return the object created
		Add = lclsTab_intproy
		lclsTab_intproy = Nothing
	End Function
	
	'%Find: Función que lee los rangos de tasas definidos
	Public Function Find(ByVal dEffecdate As Date) As Boolean
		Dim lrecTab_intproy As eRemoteDB.Execute
		Dim lclsTab_intproy As Tab_intproy
		
		On Error GoTo Find_Err
		
		lrecTab_intproy = New eRemoteDB.Execute
		
		With lrecTab_intproy
			.StoredProcedure = "INSMVI8022PKG.REAMVI8022"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsTab_intproy = New Tab_intproy
					lclsTab_intproy.dEffecdate = .FieldToClass("dEffecdate")
					lclsTab_intproy.nIntproy_min = .FieldToClass("nIntproy_min")
					lclsTab_intproy.nIntproy_max = .FieldToClass("nIntproy_max")
					lclsTab_intproy.nSvsproy_min = .FieldToClass("nSvsproy_min")
					lclsTab_intproy.nSvsproy_max = .FieldToClass("nSvsproy_max")
					lclsTab_intproy.nMonths_min = .FieldToClass("nMonths_min")
					lclsTab_intproy.nMonths_max = .FieldToClass("nMonths_max")
					lclsTab_intproy.dNulldate = .FieldToClass("dNulldate")
					Call Add(lclsTab_intproy)
					lclsTab_intproy = Nothing
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecTab_intproy = Nothing
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_intproy
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






