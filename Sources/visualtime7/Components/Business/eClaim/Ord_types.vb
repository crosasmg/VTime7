Option Strict Off
Option Explicit On
Public Class Ord_types
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Ord_types.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'local variable to hold collection
	Private mCol As Collection
	
	'%Add: Agrega un elemento a la colección.
	Public Function Add(ByVal objClass As Ord_type) As Ord_type
		If objClass Is Nothing Then
			objClass = New Ord_type
		End If
		
		With objClass
			mCol.Add(objClass, .nCurrency & .dEffecdate.ToString("yyyyMMdd") & .nOrd_typeCost)
		End With
		
		Add = objClass
		objClass = Nothing
		
	End Function
	
	'%Find: Lee los datos de la tabla para la transacción MOS661
	Public Function Find(ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaOrd_type_a As eRemoteDB.Execute
		Dim lclsOrd_type As Ord_type
		lrecReaOrd_type_a = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lrecReaOrd_type_a
			.StoredProcedure = "ReaOrd_type_a"
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				Do While Not .EOF
					lclsOrd_type = New Ord_type
					lclsOrd_type.nCurrency = .FieldToClass("nCurrency")
					lclsOrd_type.dEffecdate = .FieldToClass("dEffecdate")
					lclsOrd_type.nOrd_typeCost = .FieldToClass("nOrd_typeCost")
					lclsOrd_type.nAmount = .FieldToClass("nAmount")
					
					Call Add(lclsOrd_type)
					.RNext()
					lclsOrd_type = Nothing
				Loop 
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		lrecReaOrd_type_a = Nothing
		On Error GoTo 0
	End Function
	
	'%Item: Obtiene el elemnto de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Ord_type
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Obtiene el número de elemntos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Obtiene el nuevo elemento de la colección para recorrerlo
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
	
	'%Class_Initialize: Inicializa los elementos de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Destruye los elementos de la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






