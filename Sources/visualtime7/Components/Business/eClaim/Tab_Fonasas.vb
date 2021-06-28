Option Strict Off
Option Explicit On
Public Class Tab_Fonasas
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Fonasas.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal sService As String, ByVal sSubService As String, ByVal nAmount As Double, ByVal sDescript As String) As Tab_Fonasa
		
		Dim objNewMember As Tab_Fonasa
		objNewMember = New Tab_Fonasa
		
		On Error GoTo Add_err
		
		With objNewMember
			.sService = sService
			.sSubService = sSubService
			.sDescript = sDescript
			.nAmount = nAmount
		End With
		mCol.Add(objNewMember, "T_F" & sService & sSubService)
		
		Add = objNewMember
		objNewMember = Nothing
		
Add_err: 
		On Error GoTo 0
	End Function
	
	'% Find: Permite cargar en la colección los daños posibles de un siniestro
	Public Function Find(ByVal nYear As Integer, ByVal nCurrency As Integer, ByVal dEffecdate As Date) As Boolean
		
		Dim lreaTab_Fonasayear As eRemoteDB.Execute
		
		lreaTab_Fonasayear = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'Definición de parámetros para stored procedure 'insudb.ReaTab_FonasaYear'
		'Información leída el 13/02/2001 11:51:00
		With lreaTab_Fonasayear
			.StoredProcedure = "ReaTab_FonasaYear"
			' Parametros de entrada a la StoreProcedure
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("sService"), .FieldToClass("sSubService"), .FieldToClass("nAmount"), .FieldToClass("sDescript"))
					.RNext()
				Loop 
				.RCloseRec()
				Find = True
			Else
				Find = False
			End If
		End With
		
		lreaTab_Fonasayear = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Fonasa
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: enumera los elementos dentro de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'* Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'* Class_Initialize: controla la apertura de cada instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: elimina la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






