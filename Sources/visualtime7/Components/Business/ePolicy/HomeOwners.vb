Option Strict Off
Option Explicit On
Public Class HomeOwners
	Implements System.Collections.IEnumerable
	'**+Objective: Collection that supports the class 'HomeOwner'.
	'**+Version: $$Revision: 2 $
	'+Objetivo: Colección que le da soporte a la clase 'HomeOwner'.
	'+Version: $$Revision: 2 $
	
	'**+Objective: Local variable to hold collection.
	'+Objetivo: Variable Local para almacenar la colección.
	Private mcolHomeOwner As Collection
	
	
	
	'**%Objective: It adds an element to the collection.
	'**%Parameters:
	'**%    lclsHomeOwner -
	'%Objetivo: Agrega un elemento a la colección.
	'%Parámetros:
	'%    lclsHomeOwner -
	Public Function Add(ByRef lclsHomeOwner As HomeOwner) As HomeOwner
		
		'**-set the properties passed into the method


		mcolHomeOwner.Add(lclsHomeOwner)
		
		'**-return the object created
		Add = lclsHomeOwner
		
		Exit Function
	End Function
	
	'**%Objective: Function that makes the search in the table 'HomeOwner'.
	'**%Parameters:
	'**%     scertype   -  type of registry
	'**%     nbranch    -  branch
	'**%     nproduct   -  product
	'**%     npolicy    -  i number of poliza
	'**%     ncertif    -  i number of certificate
	'**%     deffecdate -  date of effect of the registry
	'%Objetivo: Función que realiza la busqueda en la tabla 'HomeOwner'.
	'%Parámetros:
	'%     scertype   -   tipo de registro
	'%     nbranch    -   ramo
	'%     nproduct   -   producto
	'%     npolicy    -   numero de poliza
	'%     ncertif    -   numero de certificado
	'%     deffecdate -   fecha de efecto del registro
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsHomeOwner As eRemoteDB.Execute
		Dim lclsHomeOwnerItem As HomeOwner
		

        lclsHomeOwner = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.reaHomeOwner'. Generated on 30/06/2004 03:43:06 p.m.
		With lclsHomeOwner
			.StoredProcedure = "reaHomeOwner_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					lclsHomeOwnerItem = New HomeOwner
					lclsHomeOwnerItem.sCertype = sCertype
					lclsHomeOwnerItem.nBranch = nBranch
					lclsHomeOwnerItem.nProduct = nProduct
					lclsHomeOwnerItem.nPolicy = nPolicy
					lclsHomeOwnerItem.nCertif = nCertif
					lclsHomeOwnerItem.nDwellingType = .FieldToClass("nDwellingType")
					lclsHomeOwnerItem.nOwnerShip = .FieldToClass("nOwnerShip")
					lclsHomeOwnerItem.nYear_built = .FieldToClass("nYear_built")
					lclsHomeOwnerItem.sCov_purc = .FieldToClass("sCov_purc")
					lclsHomeOwnerItem.nPrice_purch = .FieldToClass("nPrice_purch")
					lclsHomeOwnerItem.nCurrency_purch = .FieldToClass("nCurrency_purch")
					lclsHomeOwnerItem.dDate_purch = .FieldToClass("dDate_purch")
					lclsHomeOwnerItem.sPolicy_other = .FieldToClass("sPolicy_other")
					lclsHomeOwnerItem.nCap_other = .FieldToClass("nCap_other")
					lclsHomeOwnerItem.nCurrency_other = .FieldToClass("nCurrency_other")
					lclsHomeOwnerItem.dExpir_other = .FieldToClass("dExpir_other")
					lclsHomeOwnerItem.nExterConstr = .FieldToClass("nExterConstr")
					lclsHomeOwnerItem.sOther_constr = .FieldToClass("sOther_constr")
					lclsHomeOwnerItem.nStories = .FieldToClass("nStories")
					lclsHomeOwnerItem.nRoofType = .FieldToClass("nRoofType")
					lclsHomeOwnerItem.nRoofYear = .FieldToClass("nRoofYear")
					lclsHomeOwnerItem.nHomeSuper = .FieldToClass("nHomeSuper")
					lclsHomeOwnerItem.nLandSuper = .FieldToClass("nLandSuper")
					lclsHomeOwnerItem.nGarage = .FieldToClass("nGarage")
					lclsHomeOwnerItem.nFirePlace = .FieldToClass("nFirePlace")
					lclsHomeOwnerItem.nBedrooms = .FieldToClass("nBedrooms")
					lclsHomeOwnerItem.nFullBath = .FieldToClass("nFullBath")
					lclsHomeOwnerItem.nHalfBath = .FieldToClass("nHalfBath")
					lclsHomeOwnerItem.nAirType = .FieldToClass("nAirType")
					lclsHomeOwnerItem.nAlt_heating = .FieldToClass("nAlt_heating")
					lclsHomeOwnerItem.sGas = .FieldToClass("sGas")
					lclsHomeOwnerItem.sSprinkSys = .FieldToClass("sSprinkSys")
					lclsHomeOwnerItem.sAlarm_comp = .FieldToClass("sAlarm_comp")
					lclsHomeOwnerItem.nDist_Hydr = .FieldToClass("nDist_Hydr")
					lclsHomeOwnerItem.sNon_smok = .FieldToClass("sNon_smok")
					lclsHomeOwnerItem.nDist_fire = .FieldToClass("nDist_fire")
					lclsHomeOwnerItem.sFireDepart = .FieldToClass("sFireDepart")
                    lclsHomeOwnerItem.nFloodZone = .FieldToClass("nFloodZone")
                    lclsHomeOwnerItem.nSeismicZone = .FieldToClass("nSeismicZone")
					lclsHomeOwnerItem.sFloodInd = .FieldToClass("sFloodInd")
					lclsHomeOwnerItem.nSwimPool = .FieldToClass("nSwimPool")
					lclsHomeOwnerItem.sFencePool = .FieldToClass("sFencePool")
					lclsHomeOwnerItem.nFenceHeight = .FieldToClass("nFenceHeight")
					lclsHomeOwnerItem.sTrampoline = .FieldToClass("sTrampoline")
					lclsHomeOwnerItem.sAnimalsInd = .FieldToClass("sAnimalsInd")
					lclsHomeOwnerItem.sAnimalsDes = .FieldToClass("sAnimalsDes")
					lclsHomeOwnerItem.sAttackedInd = .FieldToClass("sAttackedInd")
					lclsHomeOwnerItem.nFoundType = .FieldToClass("nFoundType")
					Call Add(lclsHomeOwnerItem)
					lclsHomeOwnerItem = Nothing
					.RNext()
				Loop 
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
		lclsHomeOwner = Nothing
		lclsHomeOwnerItem = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: This property is used when reference to an element becomes of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Esta propiedad es usada cuando se hace referencia a un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Default Public ReadOnly Property Item(ByVal vIndexKey As Object) As HomeOwner
		Get


			Item = mcolHomeOwner.Item(vIndexKey)
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: It returns the amount of existing elements in the collection.
	'%Objetivo: Retorna la contidad de elementos existentes en la colección.
	Public ReadOnly Property Count() As Integer
		Get


			Count = mcolHomeOwner.Count()
			
			Exit Property
		End Get
	End Property
	
	'**%Objective: This property allows you to enumerate this collection with the "For...Each".
	'%Objetivo: Esta propiedad permite enumerar los elementos de la colección por medio de un "For...Each".
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get

    '
    'NewEnum = mcolHomeOwner._NewEnum
    '
    'Exit Property
    'ErrorHandler: '
    'ProcError("HomeOwners.NewEnum()")
    'End Get
    'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mcolHomeOwner.GetEnumerator
	End Function
	
	'**%Objective: It allows to remove an element of the collection.
	'**%Parameters:
	'**%    vIndexKey   -
	'%Objetivo: Permite eliminar un elemento de la colección.
	'%Parámetros:
	'%    vIndexKey -
	Public Sub Remove(ByRef vIndexKey As Object)


		mcolHomeOwner.Remove(vIndexKey)
		
		Exit Sub
	End Sub
	
	'**%Objective: Creates the collection when this class is created.
	'%Objetivo: Crea la colección cuando se crea esta clase.
	Private Sub Class_Initialize_Renamed()


		mcolHomeOwner = New Collection
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'**%Objective: Destroys collection when this class is terminated.
	'%Objetivo: Destruye la colección cuando se termina esta clase.
	Private Sub Class_Terminate_Renamed()


		Exit Sub
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class











