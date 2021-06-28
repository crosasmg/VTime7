Option Strict Off
Option Explicit On
Public Class Tar_schooltrads
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_schooltrads.cls                      $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'- local variable to hold collection
	Private mCol As Collection
	
	'%Add: Función que agrega una fila a la colección
	Public Function Add(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nAge_insu As Integer, ByVal nAge_child As Integer, ByVal nUsercode As Integer, ByVal dCompdate As Date, Optional ByVal nPeriod_pay As Integer = 0, Optional ByVal nRate As Double = 0, Optional ByVal dNulldate As Date = #12:00:00 AM#) As Tar_schooltrad
		Dim objNewMember As Tar_schooltrad
		objNewMember = New Tar_schooltrad
		
		With objNewMember
			.dCompdate = dCompdate
			.nUsercode = nUsercode
			.dNulldate = dNulldate
			.nRate = nRate
			.nPeriod_pay = nPeriod_pay
			.nAge_child = nAge_child
			.nAge_insu = nAge_insu
			.dEffecdate = dEffecdate
			.nProduct = nProduct
			.nBranch = nBranch
		End With
		mCol.Add(objNewMember)
		Add = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
	End Function
	
	'%Item: Se usa para referenciar un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_schooltrad
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'%Count: Se usa para obtener el numero de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'%NewEnum: Obtiene un item de la colección
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
	
	'%Remove: Se usa para remover elementos de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: inicializa la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate : Destruye la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Hace la busqueda para llenar la colección
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		On Error GoTo Find_Err
		
		Dim lrecreaTar_Schooltrad As eRemoteDB.Execute
		
		lrecreaTar_Schooltrad = New eRemoteDB.Execute
		
		With lrecreaTar_Schooltrad
			.StoredProcedure = "REATAR_SCHOOLTRAD_GRID"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Do While Not .EOF
					Call Add(.FieldToClass("nBranch"), .FieldToClass("nProduct"), .FieldToClass("dEffecdate"), .FieldToClass("nAge_insu"), .FieldToClass("nAge_child"), .FieldToClass("nUsercode"), .FieldToClass("dCompdate"), .FieldToClass("nPeriod_pay"), .FieldToClass("nRate"), .FieldToClass("dNulldate"))
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
		'UPGRADE_NOTE: Object lrecreaTar_Schooltrad may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_Schooltrad = Nothing
	End Function
End Class






