Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("tab_in_buss_NET.tab_in_buss")> Public Class tab_in_buss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_in_buss.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable for the collection handle
	'- Variable local para el manejo de la coleccion
	Private mCol As Collection
	
	'% Add: adds a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByRef nArticle As Integer, ByRef nDetailArt As Integer, ByRef sDescript As String, ByRef nNoteNum As Integer, ByRef sShort_des As String, ByRef sStatregt As String, ByRef nUsercode As Integer, ByRef nActivityType As Integer, ByRef nFamily As Integer, Optional ByRef sKey As String = "") As Tab_in_bus
		Dim lobjNewMember As Tab_in_bus
		
		On Error GoTo Add_Err
		lobjNewMember = New Tab_in_bus
		With lobjNewMember
			.nArticle = nArticle
			.nDetailArt = nDetailArt
			.sDescript = sDescript
			.nNoteNum = nNoteNum
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			.nActivityType = nActivityType
			.nFamily = nFamily
		End With
		
		If Len(sKey) = 0 Then
			mCol.Add(lobjNewMember)
		Else
			mCol.Add(lobjNewMember, "RD" & nArticle & nDetailArt)
		End If
		
		Add = lobjNewMember
		
Add_Err: 
		If Err.Number Then
            Add = Nothing
		End If
		'UPGRADE_NOTE: Object lobjNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjNewMember = Nothing
		On Error GoTo 0
	End Function
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_in_bus
		Get
			'+ se usa al hacer referencia a un elemento de la colección
			'+ vntIndexKey contiene el índice o la clave de la colección,
			'+ por lo que se declara como un Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	Public ReadOnly Property Count() As Integer
		Get
			'+ se usa al obtener el número de elementos de la
			'+ colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+ esta propiedad permite enumerar
			'+ esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ se usa al quitar un elemento de la colección
		'+ vntIndexKey contiene el índice o la clave, por lo que se
		'+ declara como un Variant
		'+ Sintaxis: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+ destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Leer y cargar Detalle de la Actividad en el Tdbgrid.
	Public Function Find(ByVal nArticle As Integer) As Boolean
		Dim lrecTab_in_bus As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecTab_in_bus = New eRemoteDB.Execute
		
		With lrecTab_in_bus
			.StoredProcedure = "reaTab_in_bus"
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call Add(nArticle, .FieldToClass("nDetailArt"), .FieldToClass("sDescript"), .FieldToClass("nNoteNum"), .FieldToClass("sShort_des"), .FieldToClass("sStatregt"), eRemoteDB.Constants.intNull, .FieldToClass("nActivityType"), .FieldToClass("nFamily"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTab_in_bus may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTab_in_bus = Nothing
	End Function
End Class






