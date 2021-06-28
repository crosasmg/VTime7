Option Strict Off
Option Explicit On
Public Class Guar_saving_allows
	Implements System.Collections.IEnumerable
	'variable local para contener colección
	Private mCol As Collection
	
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nPolicy_Year_Ini As Integer, ByVal nPolicy_Year_End As Integer, ByVal nGuarSav_Year As Integer) As Guar_saving_allow
		
		'crear un nuevo objeto
		Dim objNewMember As Guar_saving_allow
		objNewMember = New Guar_saving_allow
		
		'establecer las propiedades que se transfieren al método
		With objNewMember
			.nBranch = nBranch
			.nProduct = nProduct
			.dEffecdate = dEffecdate
			.nPolicy_Year_Ini = nPolicy_Year_Ini
			.nPolicy_Year__End = nPolicy_Year_End
			.nGuarSav_Year = nGuarSav_Year
		End With
		
		mCol.Add(objNewMember)
		
		'devolver el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
		
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Guar_saving_allow
		Get
			'se usa al hacer referencia a un elemento de la colección
			'vntIndexKey contiene el índice o la clave de la colección,
			'por lo que se declara como un Variant
			'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	
	
	Public ReadOnly Property Count() As Integer
		Get
			'se usa al obtener el número de elementos de la
			'colección. Sintaxis: Debug.Print x.Count
			Count = mCol.Count()
		End Get
	End Property
	
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'esta propiedad permite enumerar
			'esta colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'se usa al quitar un elemento de la colección
		'vntIndexKey contiene el índice o la clave, por lo que se
		'declara como un Variant
		'Sintaxis: x.Remove(xyz)
		
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	'% Find: Realiza la busqueda para llenar la colección
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsObject As eRemoteDB.Execute
		
		lclsObject = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSDP8005PKG.READP8005_REP"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					Call Add(nBranch, nProduct, dEffecdate, .FieldToClass("NPOLICY_YEAR_INI"), .FieldToClass("NPOLICY_YEAR_END"), .FieldToClass("NGUARSAV_YEAR"))
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
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		
	End Function
End Class






