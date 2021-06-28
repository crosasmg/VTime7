Option Strict Off
Option Explicit On
Public Class Tab_Providers
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Providers.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Variable local para contener colección
	Private mCol As Collection
	Public nCount As Integer
	
	'% Add: Agrega un nuevo registro a la colección
    Public Function Add(ByVal dOutdate As Date, ByVal dInpdate As Date, ByVal sStatregt As String, ByVal sStatregt_desc As String, ByVal sClient As String, ByVal sDigit As String, ByVal sCliename As String, ByVal nTypeProv As Integer, ByVal sTypeProv As String, ByVal nProvider As Integer, ByVal nProv_group As Integer, ByVal nOffice As Integer, ByVal sOffice As String, ByVal nMax_serv_ord As Integer, ByVal nTypeSupport As Integer, ByVal sTypeSupport As String, ByVal nPer_disc As Double, ByVal sConcesionary As String, ByVal nProvZone As Integer, ByVal nProvBranch As Integer, ByVal sAgencyDesc As String) As Tab_Provider
        Dim lclsTab_Provider As Tab_Provider

        lclsTab_Provider = New Tab_Provider

        '**+ Establish the properties that are going to be tranfer to the method
        '+ Establecer las propiedades que se transfieren al método
        With lclsTab_Provider
            .dOutdate = dOutdate
            .dInpdate = dInpdate
            .sStatregt = sStatregt
            .sStatregt_desc = sStatregt_desc
            .sClient = sClient
            .sDigit = sDigit
            .sCliename = sCliename
            .nTypeProv = nTypeProv
            .sTypeProv = sTypeProv
            .nProvider = nProvider
            .nProv_group = nProv_group
            .nOffice = nOffice
            .sOffice = sOffice
            .nMax_serv_ord = nMax_serv_ord
            .nTypeSupport = nTypeSupport
            .sTypeSupport = sTypeSupport
            .nPer_disc = nPer_disc
            .sConcesionary = sConcesionary
            .nProvZone = nProvZone
            .nProvBranch = nProvBranch
            .sAgencyDesc = sAgencyDesc
        End With

        If nProv_group = eRemoteDB.Constants.intNull Then
            mCol.Add(lclsTab_Provider, "Prov_" & nProvider & sClient)
        Else
            mCol.Add(lclsTab_Provider, "Prov_" & nProvider & nProv_group & sClient)
        End If

        Add = lclsTab_Provider
        lclsTab_Provider = Nothing

    End Function
	
	'% Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Tab_Provider
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'% Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'% NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'% Remove: Permite remover un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'% Class_Initialize: se controla la creación de la instancia del objeto
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Class_Terminate: se controla la destrucción de la instancia del objeto
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'**% Find: locate all the providers
	'% Find: Localiza todos los Proveedores
	Public Function Find(ByVal nRows As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaProvider As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lreaProvider = New eRemoteDB.Execute
		
		mCol = Nothing
		mCol = New Collection
		
		With lreaProvider
			.StoredProcedure = "reaTab_provider_MSI011"
			nCount = 1
			If .Run Then
				Find = True
				Do While Not .EOF And nCount < nRows
					nCount = nCount + 1
					.RNext()
				Loop 
				
				Do While Not .EOF And nCount < nRows + 50
					nCount = nCount + 1
                    Call Add(.FieldToClass("dOutDate"), .FieldToClass("dInpDate"), .FieldToClass("sStatregt"), .FieldToClass("sStatregt_Desc"), .FieldToClass("sClient"), .FieldToClass("sDigit"), .FieldToClass("sCliename"), .FieldToClass("nTypeProv"), .FieldToClass("sTypeProv"), .FieldToClass("nProvider"), .FieldToClass("nProv_group"), .FieldToClass("nOffice"), .FieldToClass("sOffice"), .FieldToClass("nMax_serv_ord"), .FieldToClass("nTypeSupport"), .FieldToClass("sTypeSupport"), .FieldToClass("nPer_disc"), .FieldToClass("sConcesionary"), .FieldToClass("nProvZone"), .FieldToClass("nProvBranch"), .FieldToClass("sAgencyDesc"))
					.RNext()
				Loop 
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		lreaProvider = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**% Find_Branch: locate the associeted branch to one provider
	'% Find_Branch: Localiza los Ramos asociados a un Proveedor
	Public Function Find_Branch(ByVal nProvider As Integer, ByVal sClient As String) As Boolean
		Dim lrecProvider As eRemoteDB.Execute
		
		On Error GoTo Find_Branch_err
		
		lrecProvider = New eRemoteDB.Execute
		
		mCol = Nothing
		mCol = New Collection
		
		With lrecProvider
			.StoredProcedure = "reaProv_branch_v"
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find_Branch = True
				While Not .EOF
					Call Add_Branch(.FieldToClass("nProvider"), .FieldToClass("sDescript"), .FieldToClass("nBranch"))
					.RNext()
				End While
				.RCloseRec()
			Else
				Find_Branch = False
			End If
		End With
		lrecProvider = Nothing
		
Find_Branch_err: 
		If Err.Number Then
			Find_Branch = False
		End If
		On Error GoTo 0
		
	End Function
	
	'% Add_Branch: Agrega los Ramos asociados a un Proveedor
	Public Function Add_Branch(ByVal nProvider As Integer, ByVal sDescript As String, ByVal nBranch As Integer) As Tab_Provider
		Dim lclsTab_Provider As Tab_Provider
		
		lclsTab_Provider = New Tab_Provider
		
		With lclsTab_Provider
			.nProvider = nProvider
			.sDescript = sDescript
			.nBranch = nBranch
		End With
		
		mCol.Add(lclsTab_Provider, "Branch_" & sDescript)
		
		Add_Branch = lclsTab_Provider
		lclsTab_Provider = Nothing
		
	End Function
End Class






