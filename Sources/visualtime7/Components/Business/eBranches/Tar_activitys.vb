Option Strict Off
Option Explicit On
Public Class Tar_activitys
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_activitys.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'- Local variable to hold collection
	Private mCol As Collection
	
	'- Variables para almacenar los datos de la búsqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngCover As Integer
	Private mdtmEffecdate As Date
	Private mintTyperec As Integer
	
	'% Add: Añade una nueva instancia de la clase Tar_activity a la colección
	Public Function Add(ByRef objTar_activity As Tar_activity) As Tar_activity
		With objTar_activity
			mCol.Add(objTar_activity, "MT" & .nBranch & .nProduct & .nSpeciality & .nCover & .dEffecdate)
		End With
		
		Add = objTar_activity
		'UPGRADE_NOTE: Object objTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objTar_activity = Nothing
	End Function
	
	'% Find: Lee las tarifas de recargos por actividad
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nTyperec As Integer, Optional ByRef bFind As Boolean = False) As Boolean
		Dim lrecTar_activity As eRemoteDB.Execute
		Dim lclsTar_activity As Tar_activity
		
		On Error GoTo Find_Err
		
		If bFind Or nBranch <> mlngBranch Or nProduct <> mlngProduct Or nCover <> mlngCover Or dEffecdate <> mdtmEffecdate Or nTyperec <> mintTyperec Then
			
			lrecTar_activity = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'reatar_actlife'
			'+Información leída el 18/12/2001
			With lrecTar_activity
				.StoredProcedure = "reaTar_activity_a"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTyperec", nTyperec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTar_activity = New Tar_activity
						lclsTar_activity.nBranch = nBranch
						lclsTar_activity.nProduct = nProduct
						lclsTar_activity.nSpeciality = .FieldToClass("nSpeciality")
						lclsTar_activity.nCover = nCover
						lclsTar_activity.dEffecdate = dEffecdate
						lclsTar_activity.nPercent = .FieldToClass("nPercent")
						lclsTar_activity.nAmount = .FieldToClass("nAmount")
						lclsTar_activity.nTyperec = nTyperec
						Call Add(lclsTar_activity)
						'UPGRADE_NOTE: Object lclsTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTar_activity = Nothing
						.RNext()
					Loop 
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngCover = nCover
					mdtmEffecdate = dEffecdate
					mintTyperec = nTyperec
					Find = True
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTar_activity = Nothing
		'UPGRADE_NOTE: Object lclsTar_activity may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_activity = Nothing
	End Function
	
	'* Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_activity
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'* NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'* Class_Initialize: se controla la creación de la instancia del objeto
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'* Class_Terminate: se controla la destrucción de la instancia del objeto
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






