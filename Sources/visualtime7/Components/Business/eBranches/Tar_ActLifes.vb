Option Strict Off
Option Explicit On
Option Compare Text
Public Class Tar_ActLifes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_ActLifes.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'-local variable to hold collection
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngModulec As Integer
	Private mlngCover As Integer
	Private mstrTypetab As String
	Private mstrSmoking As String
	Private mdtmEffecdate As Date
	
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tar_ActLife) As Tar_ActLife
		If objClass Is Nothing Then
			objClass = New Tar_ActLife
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nModulec) & .nCover & .sTypetab & .sSmoking & Format(.nAge) & .dEffecdate.ToString("yyyyMMdd"))
		End With
		
		Add = objClass
		
	End Function
	
	'% Item: Se usa para referenciar un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tar_ActLife
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Se usa para obtener el numero de elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Obtiene un item de la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'------------------------------------------------------------
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Se usa para remover elementos de la colección
	'------------------------------------------------------------
	Public Sub Remove(ByRef vntIndexKey As Object)
		'------------------------------------------------------------
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sTypetab As String, ByVal sSmoking As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTar_ActLife_age As eRemoteDB.Execute
		Dim lclsreaTar_actlife_age As Tar_ActLife
		
		On Error GoTo Find_Err
		
		sSmoking = IIf(sSmoking = "1", "1", "2")
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngModulec <> nModulec Or mlngCover <> nCover Or mstrTypetab <> sTypetab Or mstrSmoking <> sSmoking Or mdtmEffecdate <> dEffecdate Or bFind Then
			
			lrecreaTar_ActLife_age = New eRemoteDB.Execute
			
			With lrecreaTar_ActLife_age
				.StoredProcedure = "reaTar_actlife_age"
				With .Parameters
					.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("sTypetab", sTypetab, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
				End With
				If .Run(True) Then
					Do While Not .EOF
						Find = True
						
						lclsreaTar_actlife_age = New Tar_ActLife
						lclsreaTar_actlife_age.nBranch = nBranch
						lclsreaTar_actlife_age.nProduct = nProduct
						lclsreaTar_actlife_age.nModulec = nModulec
						lclsreaTar_actlife_age.nCover = nCover
						lclsreaTar_actlife_age.sSmoking = IIf(sSmoking = "1", "1", "2")
						lclsreaTar_actlife_age.sTypetab = sTypetab
						lclsreaTar_actlife_age.dEffecdate = dEffecdate
						lclsreaTar_actlife_age.nAge = .FieldToClass("nAge")
						lclsreaTar_actlife_age.nRatewomen = .FieldToClass("nRatewomen")
						lclsreaTar_actlife_age.nPremwomen = .FieldToClass("nPremwomen")
						lclsreaTar_actlife_age.nRatemen = .FieldToClass("nRatemen")
						lclsreaTar_actlife_age.nPremmen = .FieldToClass("nPremmen")
						Call Add(lclsreaTar_actlife_age)
						'UPGRADE_NOTE: Object lclsreaTar_actlife_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsreaTar_actlife_age = Nothing
						.RNext()
					Loop 
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
		'UPGRADE_NOTE: Object lrecreaTar_ActLife_age may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTar_ActLife_age = Nothing
		On Error GoTo 0
	End Function
	'% Class_Initialize: Crea la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'------------------------------------------------------------
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: Destruye la colección
	'------------------------------------------------------------
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'------------------------------------------------------------
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






