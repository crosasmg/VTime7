Option Strict Off
Option Explicit On
Public Class Tab_am_gexs
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_gexs.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección.
	Private mCol As Collection
	
	'- Propiedad auxiliar.
	Private mdtmEffecdate As Date
	
	'% Add: Esta función permite añadir registros a la colección.
	Public Function Add(ByRef lclsTab_am_gex As Tab_am_gex) As Tab_am_gex
		With lclsTab_am_gex
			mCol.Add(lclsTab_am_gex, "MAM" & .nExc_code & .sIllness & .dExc_date & .dEffecdate & .sDesIll)
		End With
		
		'+ Return the object created
		Add = lclsTab_am_gex
		'UPGRADE_NOTE: Object lclsTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_am_gex = Nothing
	End Function
	
	'% Find: Permite realizar la lectura de las exclusiones generales de enfermedades.
	Public Function Find(ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = True) As Boolean
		Dim lrecReaTab_am_gex As eRemoteDB.Execute
		Dim lclsTab_am_gex As Tab_am_gex
		
		On Error GoTo Find_Err
		
		Find = True
		
		If dEffecdate <> mdtmEffecdate Or bFind Then
			
			lrecReaTab_am_gex = New eRemoteDB.Execute
			
			With lrecReaTab_am_gex
				.StoredProcedure = "ReaTab_am_Ill_Gex"
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mdtmEffecdate = dEffecdate
					
					Do While Not .EOF
						lclsTab_am_gex = New Tab_am_gex
						lclsTab_am_gex.nExc_code = .FieldToClass("nExc_code")
						lclsTab_am_gex.sIllness = .FieldToClass("sIllness")
						lclsTab_am_gex.dExc_date = .FieldToClass("dExc_Date")
						lclsTab_am_gex.dEffecdate = .FieldToClass("dEffecdate")
						lclsTab_am_gex.sDesIll = .FieldToClass("DesIll")
						
						Call Add(lclsTab_am_gex)
						'UPGRADE_NOTE: Object lclsTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_am_gex = Nothing
						.RNext()
					Loop 
					.RCloseRec()
				Else
					Find = False
					mdtmEffecdate = CDate(Nothing)
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTab_am_gex may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_am_gex = Nothing
	End Function
	
	'* Item: devuelve un elemento de la colección (según índice, o llave)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_am_gex
		Get
			'+ Se usa al hacer referencia a un elemento de la colección vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5).
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ Se usa al obtener el número de elementos de la colección. Sintaxis: Debug.Print x.Count.
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ Esta propiedad permite enumerar esta colección con la sintaxis For...Each.
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ Se usa al quitar un elemento de la colección vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant Sintaxis: x.Remove(xyz).
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la creación de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ Crea la colección cuando se crea la clase.
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: controla la destrucción de la instancia del objeto de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+ Destruye la colección cuando se termina la clase.
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






