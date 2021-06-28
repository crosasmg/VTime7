Option Strict Off
Option Explicit On
Public Class Reinsurans
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Reinsurans.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Se definen las variables auxiliares para evitar una búsqueda innecesaria
	
	Private mstrCertype As String
	Private mintBranch As Integer
	Private mintProduct As Integer
	Private mlngPolicy As Double
	Private mlngCertif As Double
	Private mintModulec As Integer
	Private mintCover As Integer
	Private mstrClient As String
	Private mdtmEffecdate As Date
	Private mintBranch_rei As Integer
	
	
	'**%Find: This method fills the collection with records from the table "Property" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Property" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	'------------------------------------------------------------
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nBranch_rei As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, Optional ByVal lblnFind As Boolean = False) As Boolean
		'------------------------------------------------------------
		'- Se define la variable lrecreaProperty
		Dim lrecreaReinsuran As eRemoteDB.Execute
		
		If mstrCertype <> sCertype Or mintBranch <> nBranch Or mintProduct <> nProduct Or mlngPolicy <> nPolicy Or mlngCertif <> nCertif Or mdtmEffecdate <> dEffecdate Or mintBranch_rei <> nBranch_rei Or mintModulec <> nModulec Or mintCover <> nCover Or mstrClient <> sClient Or lblnFind Then
			
			mCol = New Collection
			lrecreaReinsuran = New eRemoteDB.Execute
			
			With lrecreaReinsuran
				.StoredProcedure = "reaTReinsuran2"
				.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nType", 4, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					mstrCertype = sCertype
					mintBranch = nBranch
					mintProduct = nProduct
					mlngPolicy = nPolicy
					mlngCertif = nCertif
					mdtmEffecdate = dEffecdate
					mintBranch_rei = nBranch_rei
					mintModulec = nModulec
					mintCover = nCover
					mstrClient = sClient
					
					Do While Not .EOF
						Call Add(CInt("2"), sCertype, nBranch, nProduct, nPolicy, nCertif, nBranch_rei, .FieldToClass("nType"), dEffecdate, .FieldToClass("nCompany"), .FieldToClass("dAccedate"), .FieldToClass("nCapital"), .FieldToClass("nCapital"), .FieldToClass("nCommissi"), .FieldToClass("nCurrency"), .FieldToClass("sHeap_code"), .FieldToClass("nInter_rate"), .FieldToClass("nNumber"), .FieldToClass("nReser_rate"), .FieldToClass("nQuotaSha"), eRemoteDB.Constants.dtmNull, .FieldToClass("sManualMov"), nModulec, nCover, sClient, .FieldToClass("nChange"))
						.RNext()
					Loop 
					.RCloseRec()
					Find = True
					
					'UPGRADE_NOTE: Object lrecreaReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lrecreaReinsuran = Nothing
				Else
					mstrCertype = String.Empty
					mintBranch = eRemoteDB.Constants.intNull
					mintProduct = eRemoteDB.Constants.intNull
					mlngPolicy = eRemoteDB.Constants.intNull
					mlngCertif = eRemoteDB.Constants.intNull
					mdtmEffecdate = eRemoteDB.Constants.dtmNull
					mintBranch_rei = eRemoteDB.Constants.intNull
					mintModulec = eRemoteDB.Constants.intNull
					mintCover = eRemoteDB.Constants.intNull
					mstrClient = String.Empty
					Find = False
				End If
			End With
		Else
			Find = True
		End If
	End Function
	
	'**% Add: adds a new element to the collection.
	'% Add: añade un nuevo elemento a la colección
    Public Function Add(ByVal nStatusInstance As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nBranch_rei As Integer, ByVal nType As Integer, ByVal dEffecdate As Date, ByVal nCompany As Integer, ByVal dAcceDate As Date, ByVal nCapital As Double, ByVal nCapitalmax As Double, ByVal nCommissi As Double, ByVal nCurrency As Integer, ByVal sHeap_code As String, ByVal nInter_rate As Double, ByVal nNumber As Integer, ByVal nReser_rate As Double, ByVal nShare As Double, ByVal dNulldate As Date, ByVal sManualMov As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal sClient As String, ByVal nChange As Integer, Optional ByVal lintCount As Integer = 0, Optional ByVal sContrdes As String = "", Optional ByVal sCompany As String = "", Optional ByVal nClasific As Integer = 0, Optional ByVal nCapital_Rei As Double = 0, Optional ByVal nPremium_Agree As Double = 0) As Reinsuran

        '**+ Create a new object.
        Dim objNewMember As ePolicy.Reinsuran
        objNewMember = New ePolicy.Reinsuran

        '+ Set the properties passed into the method
        With objNewMember
            .nStatusInstance = nStatusInstance
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .nBranch_rei = nBranch_rei
            .nType = nType
            .dEffecdate = dEffecdate
            .nCompany = nCompany
            .dAcceDate = dAcceDate
            .nCapital = nCapital
            .nCapitalmax = nCapitalmax
            .nCapital_Rei = nCapital_Rei
            .nCommissi = nCommissi
            .nCurrency = nCurrency
            .sHeap_code = sHeap_code
            .nInter_rate = nInter_rate
            .nNumber = nNumber
            .nReser_rate = nReser_rate
            .nShare = nShare
            .dNulldate = dNulldate
            .sManualMov = sManualMov
            .nModulec = nModulec
            .nCover = nCover
            .sClient = sClient
            .nChange = nChange
            .sContraDes = sContrdes
            .sCompany = sCompany
            .nClasific = nClasific
            .nPremium_Agree = nPremium_Agree

        End With

        mCol.Add(objNewMember, "A" & nBranch & nProduct & nPolicy & nCertif & nType & nNumber & nCompany & nModulec & nCover & sClient)

        '**+ Return the object created
        Add = objNewMember
        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        objNewMember = Nothing

    End Function
	
	'*** Item: take an element from the collection
	'* Item: toma un elemento de la colección
	Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Reinsuran
		Get
			'+ Used when referencing an element in the collection.
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			On Error Resume Next
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count: counts the elements of the collection.
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ Used when retrieving the number of elements in the collection.
			'+ Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum: enumerates the elements of the collection.
	'* NewEnum: enumera los elementos de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'+This property allows you to enumerate this collection with the For...Each syntax
			'
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="95F9AAD0-1319-4921-95F0-B9D3C4FF7F1C"'
		GetEnumerator = mCol.GetEnumerator
	End Function
	
	'*** Remove: deletes an element from the collection.
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ Used when removing an element from the collection.
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: controls the opening of the class.
	'* Class_Initialize: controla la apertura de la colección
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'+ Creates the collection when this class is created
		
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Terminate: controls the end of the collection.
	'* Class_Terminate: controla el fin de la colección
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'+ Destroys collection when this class is terminated
		
		'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






