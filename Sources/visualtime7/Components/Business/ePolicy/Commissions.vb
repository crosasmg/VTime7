Option Strict Off
Option Explicit On
Public Class Commissions
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Commissions.cls                          $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**-Auxiliary variables
	'-Variables auxiliares
	
	'**-Variables definition. This variables will be used in the query
	'- Se definen las variables que se van a utilizar para la busqueda
	
	Private mstrCertype As String
	Private mlngBranch As Integer
	Private mlngProduct As Integer
	Private mlngPolicy As Double
	Private mlngCertif As Double
	Private mdtmEffecdate As Date

    '**%Add: Adds a new instance of the commission class to the collection
    '%Add: Añade una nueva instancia de la clase Commission a la colección
    Public Sub Add(ByRef objClass As Commission)
        If objClass Is Nothing Then
            objClass = New Commission
        End If

        With objClass
            mCol.Add(objClass, .sCertype & .nBranch & .nProduct & .nPolicy & .nCertif & .nIntertyp & .nIntermed & .dEffecdate.ToString("yyyyMMdd"))
        End With
    End Sub

    '**%Find: Returns a collection of objects of the type "commission"
    '%Find: Devuelve una colección de objetos de tipo Commission
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dEffecdate As Date, Optional ByVal nCertif As Double = 0, Optional ByVal lblnFind As Boolean = False) As Boolean
		'**-Variable definition: lrecreaComission.
		'-Se define la variable lrecreaCommission
		Dim lrecreaCommission As eRemoteDB.Execute
		Dim lclsCommission As Commission
		
		On Error GoTo Find_Err
		lrecreaCommission = New eRemoteDB.Execute
		
		If mstrCertype <> sCertype Or mlngBranch <> nBranch Or mlngProduct <> nProduct Or mlngPolicy <> nPolicy Or mlngCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or lblnFind Then
			
			With lrecreaCommission
				.StoredProcedure = "reaCommission"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrCertype = sCertype
					mlngBranch = nBranch
					mlngProduct = nProduct
					mlngPolicy = nPolicy
					mlngCertif = nCertif
					mdtmEffecdate = dEffecdate
					Do While Not .EOF
						lclsCommission = New Commission
						lclsCommission.sCertype = sCertype
						lclsCommission.nBranch = nBranch
						lclsCommission.nProduct = nProduct
						lclsCommission.nPolicy = nPolicy
						lclsCommission.nCertif = nCertif
						lclsCommission.nIntertyp = .FieldToClass("nIntertyp")
						lclsCommission.nIntermed = .FieldToClass("nIntermed")
						lclsCommission.dEffecdate = .FieldToClass("dEffecdate")
						lclsCommission.nAmount = .FieldToClass("nAmount")
						lclsCommission.sCommityp = .FieldToClass("sCommityp")
						lclsCommission.dNulldate = .FieldToClass("dNulldate")
						lclsCommission.nPercent = .FieldToClass("nPercent")
						lclsCommission.nShare = .FieldToClass("nShare")
						lclsCommission.nPerdiscount = .FieldToClass("nPerdiscount")
						lclsCommission.sPluscollec = .FieldToClass("sPluscollec")
						lclsCommission.sPlusoffice = .FieldToClass("sPlusoffice")
						lclsCommission.sPlusquality = .FieldToClass("sPlusquality")
						lclsCommission.nAgreement = .FieldToClass("nAgreement")
						lclsCommission.nPercent_ce = .FieldToClass("nPercent_Ce")
						lclsCommission.nInstallcom = .FieldToClass("nInstallcom")
						lclsCommission.sCliename = .FieldToClass("sCliename")
						lclsCommission.nAgency = .FieldToClass("nAgency")
						Call Add(lclsCommission)
						'UPGRADE_NOTE: Object lclsCommission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclsCommission = Nothing
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCommission may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCommission = Nothing
	End Function
	
	'***Item: Returns an element of the collection (according to the index)
	'*Item: Devuelve un elemento de la colección (segun índice)
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Commission
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'***Count: Returns the quantity of elements in the collection
	'*Count: Devuelve el número de elementos que posee la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'***NewEnum: Enumerates the collection when the collection will be used in a For Each...Next
	'*NewEnum: Permite enumerar la colección para utilizarla en un ciclo For Each... Next
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
	
	'**%Remove: Deletes an element of the collection
	'%Remove: Elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'**%Class_Initialize: Controls the creation of an instance of the collection
	'%Class_Initialize: Controla la creación de una instancia de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Class_Terminate: Controls the destruction of an instance of the collection
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






