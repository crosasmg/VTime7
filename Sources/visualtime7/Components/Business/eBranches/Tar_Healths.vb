Option Strict Off
Option Explicit On
Public Class Tar_Healths
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_Healths.cls                         $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 20/10/03 13.35                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mdtmEffecdate As Date
	Private mlngCover As Integer
	Private mlngAgreement As Integer
	Private mlngAge As Integer
	Private mlngSex As Integer
	Private mlngCount_Insu_Ini As Integer
	Private mlngCount_Insu_End As Integer
	Private mdblRate As Double
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tar_Health) As Tar_Health
		If objClass Is Nothing Then
			objClass = New Tar_Health
		End If
		
		With objClass
			mCol.Add(objClass, "TL" & .nBranch & .nProduct & .dEffecdate.ToString("yyyyMMdd") & .nCover & .nAgreement & .nAge & .nSex & .nInsu_Count_Ini & .nInsu_Count_End & .nRate)
		End With
		
		Add = objClass
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_Health
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
	
	'%NewEnum: Permite remover un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Class_Initialize: se controla la creación de la instancia del objeto
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: se controla la destrucción de la instancia del objeto
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'% Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nAgreement As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaTar_Health As eRemoteDB.Execute
		Dim lclsTar_Health As Tar_Health
		
		On Error GoTo Find_Err
		
		Find = True
		
		lrecReaTar_Health = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaTar_Health'
		With lrecReaTar_Health
			.StoredProcedure = "ReaTar_Health"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					lclsTar_Health = New Tar_Health
					
					lclsTar_Health.nBranch = nBranch
					lclsTar_Health.nProduct = nProduct
					lclsTar_Health.dEffecdate = .FieldToClass("dEffecdate")
					lclsTar_Health.nCover = nCover
					lclsTar_Health.nAgreement = nAgreement
					lclsTar_Health.nAge = .FieldToClass("nAge")
					lclsTar_Health.nSex = .FieldToClass("nSex")
					lclsTar_Health.nInsu_Count_Ini = .FieldToClass("NCOUNT_INSU_INI")
					lclsTar_Health.nInsu_Count_End = .FieldToClass("NCOUNT_INSU_END")
					lclsTar_Health.nRate = .FieldToClass("nRate")
					lclsTar_Health.dNulldate = .FieldToClass("dNulldate")
					
					Call Add(lclsTar_Health)
					.RNext()
					'UPGRADE_NOTE: Object lclsTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsTar_Health = Nothing
				Loop 
				
				mintBranch = nBranch
				mlngProduct = nProduct
				mlngCover = nCover
				mlngAgreement = nAgreement
				mdtmEffecdate = dEffecdate
				
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_Health = Nothing
		'UPGRADE_NOTE: Object lclsTar_Health may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_Health = Nothing
	End Function
End Class






