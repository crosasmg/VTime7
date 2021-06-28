Option Strict Off
Option Explicit On
Public Class Tab_ActiveLifes
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_ActiveLifes.cls                      $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 6/02/06 11:00                                $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Variables que guardan la llave de busqueda
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mdtmEffecdate As Date
	
	'%Add: Agrega un nuevo registro a la colección
	Public Function Add(ByRef objClass As Tab_ActiveLife) As Tab_ActiveLife
		If objClass Is Nothing Then
			objClass = New Tab_ActiveLife
		End If
		
		With objClass
			mCol.Add(objClass, "CP" & Format(.nBranch) & Format(.nProduct) & Format(.nModulec) & .dEffecdate.ToString("yyyyMMdd") & Format(.nOption))
		End With
		
		'+ Se retorna el objeto creado
		Add = objClass
	End Function
	
	'%Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_ActiveLife
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
	
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'%Find: Lee los datos de la tabla
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaTab_activelife_modulec As eRemoteDB.Execute
		Dim lclsTab_ActiveLife As Tab_ActiveLife
		
		On Error GoTo Find_Err
		Find = True
		
		If mlngBranch <> nBranch Or mlngProduct <> nProduct Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			lrecreaTab_activelife_modulec = New eRemoteDB.Execute
			
			'+Definición de parámetros para stored procedure 'reaTab_activelife_modulec'
			With lrecreaTab_activelife_modulec
				.StoredProcedure = "reaTab_activelife_modulec"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
						lclsTab_ActiveLife = New Tab_ActiveLife
						lclsTab_ActiveLife.nBranch = nBranch
						lclsTab_ActiveLife.nProduct = nProduct
						lclsTab_ActiveLife.dEffecdate = dEffecdate
						lclsTab_ActiveLife.nExists = .FieldToClass("nExists")
						lclsTab_ActiveLife.nModulec = .FieldToClass("nModulec")
						lclsTab_ActiveLife.sModulecDesc = .FieldToClass("sModulecDesc")
						lclsTab_ActiveLife.nCapmin = .FieldToClass("nCapmin")
						lclsTab_ActiveLife.nMchainves = .FieldToClass("nMchainves")
						lclsTab_ActiveLife.nErrrange = .FieldToClass("nErrrange")
						lclsTab_ActiveLife.nOption = .FieldToClass("nOption")
						lclsTab_ActiveLife.nPercent = .FieldToClass("nPercent")
						lclsTab_ActiveLife.nMin_prembas = .FieldToClass("nMin_prembas")
                        lclsTab_ActiveLife.nMax_prembas = .FieldToClass("nMax_prembas")
                        lclsTab_ActiveLife.nMin_premmin = .FieldToClass("nMin_premmin")
                        lclsTab_ActiveLife.nMax_premmin = .FieldToClass("nMax_premmin")
                        lclsTab_ActiveLife.nMin_premexc = .FieldToClass("nMin_premexc")
                        lclsTab_ActiveLife.nMax_premexc = .FieldToClass("nMax_premexc")

                        lclsTab_ActiveLife.nMin_premPac = .FieldToClass("nMin_prempac")
                        lclsTab_ActiveLife.nMax_premPac = .FieldToClass("nMax_prempac")

                        Call Add(lclsTab_ActiveLife)
						'UPGRADE_NOTE: Object lclsTab_ActiveLife may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsTab_ActiveLife = Nothing
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
		'UPGRADE_NOTE: Object lrecreaTab_activelife_modulec may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_activelife_modulec = Nothing
		On Error GoTo 0
	End Function
	
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Controla la destrucción de una instancia de la colección
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






