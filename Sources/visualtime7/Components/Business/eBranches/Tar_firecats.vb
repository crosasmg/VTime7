Option Strict Off
Option Explicit On
Public Class Tar_firecats
	Implements System.Collections.IEnumerable
	
	'+Variable local para contener colección
	Private mCol As Collection
	Public Function Add(ByRef nActivityCat As Integer, ByRef nConstcat As Integer, ByRef dEffecdate As Date, ByRef nRateBuild As Double, ByRef nRateCont As Double, ByRef nRateRC As Double, ByRef dNulldate As Date) As Tar_firecat
		
		'+Crear un nuevo objeto
		Dim objNewMember As Tar_firecat
		objNewMember = New Tar_firecat
		
		'+Establecer las propiedades que se transfieren al método
		With objNewMember
			.nActivityCat = nActivityCat
			.nConstcat = nConstcat
			.dEffecdate = dEffecdate
			.nRateBuild = nRateBuild
			.nRateCont = nRateCont
			.nRateRC = nRateRC
			.dNulldate = dNulldate
		End With
		mCol.Add(objNewMember)
		
		'+Devolver el objeto creado
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_firecat
		Get
			'+Para hacer referencia a la colección
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	Public ReadOnly Property Count() As Integer
		Get
			'+Para obtener el número de elementos de la colección
			Count = mCol.Count()
		End Get
	End Property
	
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+Esta propiedad permite enumerar la colección con la sintaxis For...Each
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+Para eliminar el número de elementos de la colección
		mCol.Remove(vntIndexKey)
	End Sub
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+Crea la colección cuando se crea la clase
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+Destruye la colección cuando se termina la clase
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%Find : Esta función se encarga de de buscar la colección de datos de acuerdo
	'%a las fecha del registro efectivo
	Public Function Find(ByVal dEffecdate As Date) As Boolean
		Dim lrecselect As eRemoteDB.Execute
		
		If dEffecdate = dtmNull Then
			dEffecdate = Today
		End If
		lrecselect = New eRemoteDB.Execute
		With lrecselect
			.StoredProcedure = "reaTar_FireCat_In_Force"
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nActivityCat"), .FieldToClass("nConstCat"), .FieldToClass("dEffecdate"), .FieldToClass("nRateBuild"), .FieldToClass("nRateCont"), .FieldToClass("nRateRC"), .FieldToClass("dNulldate"))
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecselect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecselect = Nothing
	End Function
End Class






