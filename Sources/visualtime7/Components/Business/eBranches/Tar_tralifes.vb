Option Strict Off
Option Explicit On
Public Class Tar_tralifes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_tralifes.cls                         $%'
	'% $Author:: Nvaplat18                                  $%'
	'% $Date:: 20/10/03 13.35                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mintBranch As Integer
	Private mlngProduct As Integer
	Private mintModulec As Integer
	Private mintCover As Integer
	Private mstrSmoking As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tar_tralife) As Tar_tralife
		If objClass Is Nothing Then
			objClass = New Tar_tralife
		End If
		
		With objClass
			mCol.Add(objClass, "TL" & .nBranch & .nProduct & .nModulec & .nCover & .sSmoking & .nAge & .nInipercov & .nInipaycov & .dEffecdate.ToString("yyyyMMdd") & .nRatewomen & .nPremwomen & .nRatemen & .nPremmen & .nType_tar & .nEndpercov & .nEndpaycov)
		End With
		
		Add = objClass
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_tralife
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
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sSmoking As String, ByVal dEffecdate As Date, ByVal nTyperisk As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecReaTar_tralife As eRemoteDB.Execute
		Dim lclsTar_tralife As Tar_tralife
		
		On Error GoTo Find_Err
		
		Find = True
		
		sSmoking = IIf(sSmoking = String.Empty, 2, sSmoking)
		
		If mintBranch <> nBranch Or mlngProduct <> nProduct Or mintModulec <> nModulec Or mintCover <> nCover Or mstrSmoking <> CDbl(sSmoking) Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecReaTar_tralife = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'ReaTar_tralife'
			With lrecReaTar_tralife
				.StoredProcedure = "ReaTar_tralife"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Do While Not .EOF
						lclsTar_tralife = New Tar_tralife
						lclsTar_tralife.nBranch = nBranch
						lclsTar_tralife.nProduct = nProduct
						lclsTar_tralife.nModulec = nModulec
						lclsTar_tralife.nCover = nCover
						lclsTar_tralife.sSmoking = .FieldToClass("sSmoking")
						lclsTar_tralife.nAge = .FieldToClass("nAge")
						lclsTar_tralife.nInipercov = .FieldToClass("nInipercov")
						lclsTar_tralife.nInipaycov = .FieldToClass("nInipaycov")
						lclsTar_tralife.dEffecdate = .FieldToClass("dEffecdate")
						lclsTar_tralife.nRatewomen = .FieldToClass("nRatewomen")
						lclsTar_tralife.nPremwomen = .FieldToClass("nPremwomen")
						lclsTar_tralife.nRatemen = .FieldToClass("nRatemen")
						lclsTar_tralife.nPremmen = .FieldToClass("nPremmen")
						lclsTar_tralife.dNulldate = .FieldToClass("dNulldate")
						lclsTar_tralife.nType_tar = .FieldToClass("nType_tar")
						lclsTar_tralife.nEndpercov = .FieldToClass("nEndpercov")
						lclsTar_tralife.nEndpaycov = .FieldToClass("nEndpaycov")
						Call Add(lclsTar_tralife)
						.RNext()
						'UPGRADE_NOTE: Object lclsTar_tralife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTar_tralife = Nothing
					Loop 
					
					mintBranch = nBranch
					mlngProduct = nProduct
					mintModulec = nModulec
					mintCover = nCover
					mstrSmoking = CInt(sSmoking)
					mdtmEffecdate = dEffecdate
					
					.RCloseRec()
				Else
					Find = False
				End If
			End With
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaTar_tralife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTar_tralife = Nothing
		'UPGRADE_NOTE: Object lclsTar_tralife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTar_tralife = Nothing
	End Function
End Class






