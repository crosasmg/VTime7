Option Strict Off
Option Explicit On
Public Class Addresss
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Addresss.cls                             $%'
	'% $Author:: Nvaplat53                                  $%'
	'% $Date:: 30/08/04 4:25p                               $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'- Se definen las variables auxiliares para evitar una búsqueda innecesaria
	
	Private lauxKeyAddress As String
	Private lAuxRecowner As Integer
	Private lAuxEffecdate As Date
	
	'- Propiedades auxiliares de la consulta CAC005 - Ubicación del riesgo.
	
	Private mlngProvince As Integer
	Private mlngLocal As Integer
	Private mlngMunicipality As Integer
	Private mdtmEffecdate As Date
	Private mstrCondition As String
	
	Public Enum eTypeRecType
		cstrComercial = 1 '+Dirección Comercial
		cstrParticular = 2 '+Dirección Particular
		cstrCasilla = 3 '+Dirección Casilla
	End Enum
	
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nStatusInstance As Integer, ByVal nRecOwner As Integer, ByVal sKeyAddress As String, ByVal sRecType As String, ByVal sStreet As String, ByVal sStreet1 As String, ByVal sZone As String, ByVal sClient As String, ByVal sCertype As String, ByVal sE_mail As String, ByVal nLat_second As Double, ByVal nLon_second As Double, ByVal nLat_coord As Double, ByVal nLon_coord As Double, ByVal nContrat As Integer, ByVal nCountry As Integer, ByVal nLat_cardin As Integer, ByVal nLat_minute As Integer, ByVal nLon_cardin As Integer, ByVal nLon_minute As Integer, ByVal nCertif As Integer, ByVal nClaim As Double, ByVal nPolicy As Integer, ByVal nLocal As Integer, ByVal nZip_Code As Integer, ByVal nLat_grade As Integer, ByVal nLon_grade As Integer, ByVal nBk_agency As Integer, ByVal nBank_code As Integer, ByVal nBranch As Integer, ByVal nOffice As Integer, ByVal nProvince As Integer, ByVal nProduct As Integer, ByVal nMunicipality As Integer, ByVal sInfor As String, ByVal sBuild As String, ByVal nFloor As Integer, ByVal sDepartment As String, ByVal sPopulation As String, ByVal sPobox As String, ByVal sDescadd As String, ByVal sCostCenter As String, ByVal nNotInformEmail As String) As Address
		
		'+ Create a new object
		
		Dim objNewMember As Address
		
		objNewMember = New Address
		
		If sKeyAddress = String.Empty Then
			sKeyAddress = ConstructKeyAddress(CInt(nRecOwner), CInt(sRecType), sCertype, nBranch, nProduct, nPolicy, nCertif, nClaim, sClient, nBank_code, nBk_agency, nOffice, nContrat)
		End If
		'+ Set the properties passed into the method
		With objNewMember
			.nStatusInstance = nStatusInstance
			.nRecOwner = nRecOwner
			.sKeyAddress = sKeyAddress
			.sRecType = sRecType
			.sStreet = sStreet
			.sStreet1 = sStreet1
			.sZone = sZone
			.sClient = sClient
			.sCertype = sCertype
			.sE_mail = sE_mail
			.nLat_second = nLat_second
			.nLon_second = nLon_second
			.nLat_coord = nLat_coord
			.nLon_coord = nLon_coord
			.nContrat = nContrat
			.nCountry = nCountry
			.nLat_cardin = nLat_cardin
			.nLat_minute = nLat_minute
			.nLon_cardin = nLon_cardin
			.nLon_minute = nLon_minute
			.nCertif = nCertif
			.nClaim = nClaim
			.nPolicy = nPolicy
			.nLocal = nLocal
			.nZip_Code = nZip_Code
			.nLat_grade = nLat_grade
			.nLon_grade = nLon_grade
			.nBk_agency = nBk_agency
			.nBank_code = nBank_code
			.nBranch = nBranch
			.nOffice = nOffice
			.nProvince = nProvince
			.nProduct = nProduct
			.nMunicipality = nMunicipality
			.sInfor = sInfor
			.sBuild = sBuild
			.nFloor = nFloor
			.sDepartment = sDepartment
			.sPopulation = sPopulation
			.sPobox = sPobox
			.sDescadd = sDescadd
			.nNotInformEmail = nNotInformEmail
		End With
		
		mCol.Add(objNewMember, "A" & sRecType)
		
		'+ Return the object created
		
		Add = objNewMember
		
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% AddCAC005: Añade un nuevo elemento a la colección.
	Public Function AddCAC005(ByVal nStatusInstance As Integer, ByVal sStreet As String, ByVal sStreet1 As String, ByVal nCertif As Integer, ByVal nPolicy As Integer, ByVal nBranch As Integer, ByVal sDescBranch As String, ByVal sDescCurrency As String, ByVal nZip_Code As Integer) As Address
		Dim objNewMember As Address
		objNewMember = New Address
		With objNewMember
			.nStatusInstance = nStatusInstance
			.sStreet = sStreet
			.sStreet1 = sStreet1
			.nCertif = nCertif
			.nPolicy = nPolicy
			.nBranch = nBranch
			.sDescBranch = sDescBranch
			.sDescCurrency = sDescCurrency
			.nZip_Code = nZip_Code
		End With
		mCol.Add(objNewMember)
		AddCAC005 = objNewMember
		'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		objNewMember = Nothing
		
	End Function
	
	'% Find: busca los datos
	Public Function Find(ByVal nRecOwner As Integer, ByVal dEffecdate As Date, Optional ByVal sKeyAddress As String = "", Optional ByVal Certype As String = "", Optional ByVal Branch As Integer = 0, Optional ByVal Product As Integer = 0, Optional ByVal Policy As Integer = 0, Optional ByVal Certif As Integer = 0, Optional ByVal Claim As Double = 0, Optional ByVal Client As String = "", Optional ByVal Bank As Integer = 0, Optional ByVal Agency As Integer = 0, Optional ByVal Office As Integer = 0, Optional ByVal Contrat As Integer = 0, Optional ByVal lblnAll As Boolean = False) As Boolean
		Dim lrecreaAddress_All As eRemoteDB.Execute
		Dim lobjAddress As Address
		
		On Error GoTo Find_Err
		
		If sKeyAddress = String.Empty Then
			sKeyAddress = ConstructKeyAddress(CInt(nRecOwner), 0, Certype, Branch, Product, Policy, Certif, Claim, Client, Bank, Agency, Office, Contrat)
		End If
		If lauxKeyAddress = sKeyAddress And lAuxRecowner = nRecOwner And lAuxEffecdate = dEffecdate Then
			Find = True
		Else
			lrecreaAddress_All = New eRemoteDB.Execute
			
			'+ Definición de parámetros para stored procedure 'insudb.reaAddress_All'
			'+ Información leída el 30/12/1999 15:17:05
			With lrecreaAddress_All
				.StoredProcedure = "reaAddress"
				.Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nAll", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				
				If Find Then
					
					'+ Se inicializa el objeto mCol para casos de diferentes consultas consecutivas
					'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					mCol = Nothing
					mCol = New Collection
					
					Do While Not .EOF
						lobjAddress = Add(0, CShort(.FieldToClass("nRecowner")), .FieldToClass("sKeyAddress"), .FieldToClass("sRectype"), .FieldToClass("sStreet"), .FieldToClass("sStreet1"), .FieldToClass("sZone"), .FieldToClass("sClient"), .FieldToClass("sCertype"), .FieldToClass("sE_mail"), .FieldToClass("nLat_second", 0), .FieldToClass("nLon_second", 0), .FieldToClass("nLat_coord", 0), .FieldToClass("nLon_coord", 0), .FieldToClass("nContrat", 0), CShort(.FieldToClass("nCountry")), .FieldToClass("nLat_cardin", 0), .FieldToClass("nLat_minute", 0), .FieldToClass("nLon_cardin", 0), .FieldToClass("nLon_minute", 0), .FieldToClass("nCertif", 0), .FieldToClass("nClaim", 0), .FieldToClass("nPolicy", 0), .FieldToClass("nLocal", 0), .FieldToClass("nZip_code", 0), .FieldToClass("nLat_grade", 0), .FieldToClass("nLon_grade", 0), .FieldToClass("nBk_agency", 0), .FieldToClass("nBank_code", 0), .FieldToClass("nBranch", 0), .FieldToClass("nOffice", 0), .FieldToClass("nProvince", 0), .FieldToClass("nProduct", 0), .FieldToClass("nMunicipality", numNull), .FieldToClass("sInfor"), .FieldToClass("sBuild"), .FieldToClass("nFloor", numNull), .FieldToClass("sDepartment"), .FieldToClass("sPopulation"), .FieldToClass("sPobox"), .FieldToClass("sDescadd"), .FieldToClass("sCostCenter"), .FieldToClass("nNotInformEmail"))
						
						With lobjAddress
							.Phones_Renamed = New eGeneralForm.Phones
							If Not .Phones_Renamed.Find(nRecOwner, lrecreaAddress_All.FieldToClass("sKeyAddress"), dEffecdate, lblnAll) Then
								'UPGRADE_NOTE: Object lobjAddress.Phones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
								.Phones_Renamed = Nothing
							End If
						End With
						.RNext()
					Loop 
					.RCloseRec()
					
					'+ Se asignan los valores a las variables auxiliares, para futuras búsquedas
					lauxKeyAddress = sKeyAddress
					lAuxRecowner = nRecOwner
					lAuxEffecdate = dEffecdate
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaAddress_All may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaAddress_All = Nothing
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'% FindAddressBCC001: busca las direcciones y telefonos de los clientes
	Public Function FindAddressBCC001(ByVal sClient As String) As Boolean
		Dim lclsAddress As Address
		Dim lrecreaAddress As eRemoteDB.Execute
		
		
		'- Se define la variable lrecreaClientName
		lclsAddress = New Address
		lrecreaAddress = New eRemoteDB.Execute
		
		On Error GoTo FindAddressBCC001_err
		
		'+ Definición de parámetros para stored procedure 'insudb.reaAddress_Phones'
		'+ Información leída el 05/04/2001
		
		With lrecreaAddress
			.StoredProcedure = "reaAddress_Phones"
			.Parameters.Add("nRecowner", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindAddressBCC001 = True
				Do While Not .EOF
					lclsAddress = Add(numNull, numNull, .FieldToClass("sKeyAddress"), String.Empty, .FieldToClass("sStreet"), .FieldToClass("sStreet1"), String.Empty, String.Empty, String.Empty, .FieldToClass("sE_mail"), numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, numNull, String.Empty, String.Empty, numNull, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, CStr(numNull))
					lclsAddress.nZip_Code = .FieldToClass("nZip_code")
					lclsAddress.slocat_des = .FieldToClass("locat_des")
					lclsAddress.sprovi_des = .FieldToClass("provi_des")
					lclsAddress.scount_des = .FieldToClass("count_des")
					
					lclsAddress.Phone_Renamed = New Phone
					lclsAddress.Phone_Renamed.nArea_code = .FieldToClass("nArea_code")
					lclsAddress.Phone_Renamed.sPhone = .FieldToClass("sPhone")
					lclsAddress.Phone_Renamed.nPhone_type = .FieldToClass("nPhone_type")
					lclsAddress.Phone_Renamed.nExtens1 = .FieldToClass("nExtens1")
					lclsAddress.Phone_Renamed.nExtens2 = .FieldToClass("nExtens2")
					.RNext()
				Loop 
			Else
				FindAddressBCC001 = False
			End If
			
		End With
		
		
		'UPGRADE_NOTE: Object lclsAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAddress = Nothing
		'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAddress = Nothing
		
FindAddressBCC001_err: 
		If Err.Number Then
			FindAddressBCC001 = False
		End If
		On Error GoTo 0
	End Function
	
	
	'% Update: realiza el tratamiento de cada instancia de la clase en la colección
	Public Function Update() As Boolean
		Dim lclsAddress As Address
		Update = True
		For	Each lclsAddress In mCol
			With lclsAddress
				
				If lauxKeyAddress = String.Empty Then
					lAuxEffecdate = .dEffecdate
					lauxKeyAddress = .sKeyAddress
					lAuxRecowner = .nRecOwner
				End If
				
				Select Case .nStatusInstance
					
					'+ Si la acción es Agregar
					Case 1
						Update = .Add()
						
						'+ Si la acción es Actualizar
					Case 2
						Update = .Update()
						
						'+ Si la acción es Eliminar
					Case 3
						Update = .Delete()
				End Select
				
				If Update Then
					.nStatusInstance = 0
				End If
				
				'+ Se actualizan los telefonos de la dirección actualizada
				
				If Not .Phones_Renamed Is Nothing Then
					Update = .Phones_Renamed.Update()
				End If
				
			End With
		Next lclsAddress
		
		'UPGRADE_NOTE: Object lclsAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAddress = Nothing
	End Function
	
	'%ConstructKeyAddress: Obtiene la clave de acceso de la dirrección según el propietario (RecOwner) y tipo de dirección (Rectype).
	Public Function ConstructKeyAddress(ByVal nRecOwner As Address.eTypeRecOwner, ByVal sRecType As eTypeRecType, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Integer = 0, Optional ByVal nCertif As Integer = 0, Optional ByVal nClaim As Double = 0, Optional ByVal sClient As String = "", Optional ByVal nBank As Integer = 0, Optional ByVal nAgency As Integer = 0, Optional ByVal nOffice As Integer = 0, Optional ByVal nContrat As Integer = 0, Optional ByRef ncod_Agree As Integer = 0, Optional ByVal nRole As Integer = 0) As String
        Dim lstrKey As String
        ConstructKeyAddress = String.Empty
		
		lstrKey = String.Empty
		
		Select Case nRecOwner
			'+ Póliza.
			'+ Nota: En este caso habría que tomar en cuenta si hace falta el campo "Office". (FAB)
			
			Case Address.eTypeRecOwner.clngPolicyAddress
				lstrKey = sCertype & nBranch & nProduct & nPolicy
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(nCertif) Then
					lstrKey = lstrKey & nCertif
				End If
				'+ Cliente
			Case Address.eTypeRecOwner.clngClientAddress
				lstrKey = sClient
				'+ Beneficiario
				'+ Nota: según manejo del beneficiaria para crear la sKeyAddress es necesario: sCertype, nBranch, nProduct, nCertif, nOffice, sClient; lo que desborda la longitud del campo antes mencionado.
				'+ por esta razón solamente se tomó en cuenta el código del cliente (en tal caso habría que redefinir la longitud del campo). (FAB)
			Case Address.eTypeRecOwner.clngBenefAddress
				lstrKey = sClient
				'+ Intermediario
			Case Address.eTypeRecOwner.clngInterAddress
				lstrKey = sClient
				'+ Compañia (Central)
			Case Address.eTypeRecOwner.clngCompanyCAddress
				lstrKey = sClient
				'+ Compañia (Local)
			Case Address.eTypeRecOwner.clngCompanyLAddress
				lstrKey = sClient
				'+ Agencia bancaria
			Case Address.eTypeRecOwner.clngAgencyAddress
				lstrKey = nBank & nAgency
				'+ Ubicación del riesgo
			Case Address.eTypeRecOwner.clngRiskAddress
				lstrKey = sCertype & nBranch & nProduct & nPolicy & nCertif
				'+ Sucursal
			Case Address.eTypeRecOwner.clngOfficeAddress
				lstrKey = CStr(nOffice)
				'+ Contrato
			Case Address.eTypeRecOwner.clngContratAddress
				lstrKey = CStr(nContrat)
				'+ Ocurrencia
			Case Address.eTypeRecOwner.clngOccurAddress
				lstrKey = sCertype & nBranch & nProduct & nPolicy & nCertif & nClaim
				
				'+ Reclamante
			Case Address.eTypeRecOwner.clngDemandantAddress
				lstrKey = sCertype & nBranch & nProduct & nPolicy & nCertif & nClaim
				
				'+ Envío de correspondencia
			Case Address.eTypeRecOwner.clngDeliveryAddress
				lstrKey = sCertype & nBranch & nProduct & nPolicy & nCertif & nClaim & sClient
				
				'+ Dirección Convenio
			Case Address.eTypeRecOwner.clngAgreementAddress
				lstrKey = ConstructKeyAddress & nRecOwner & ncod_Agree
                '+ Dirección del cliente en la póliza - certificado
		        Case Address.eTypeRecOwner.clngClientAddressInPolicy
                		lstrKey = sCertype & nBranch & nProduct & nPolicy & nCertif & nRole & sClient
		End Select
		
		If sRecType = 0 Then
			ConstructKeyAddress = lstrKey
		Else
			ConstructKeyAddress = Trim(CStr(sRecType)) & lstrKey
		End If
	End Function
	
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Address
		Get
			'+ Used when referencing an element in the collection.
			'+ vntIndexKey contains either the Index or Key to the collection,
			'+ this is why it is declared as a Variant
			'+ Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
			
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'* Count: cuenta los elementos de la colección
	Public ReadOnly Property Count() As Integer
		Get
			'+ Used when retrieving the number of elements in the collection.
			'+ Syntax: Debug.Print x.Count
			
			Count = mCol.Count()
		End Get
	End Property
	
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
	
	'* Remove: elimina un elemento de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		'+ Used when removing an element from the collection.
		'+ vntIndexKey contains either the Index or Key, which is why
		'+ it is declared as a Variant
		'+ Syntax: x.Remove(xyz)
		
		mCol.Remove(vntIndexKey)
	End Sub
	
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
	
	'% FindCAC005: Permite realizar la consulta de ubicación de riesgos.
	Public Function FindCAC005(ByVal nProvince As Integer, ByVal nLocal As Integer, ByVal nMunicipality As Integer, ByVal dEffecdate As Date, ByVal sCondition As String, Optional ByVal bFind As Boolean = True) As Boolean
		Dim lrecReaCAC005 As eRemoteDB.Execute
		On Error GoTo FindCAC005_err
		lrecReaCAC005 = New eRemoteDB.Execute
		FindCAC005 = True
		If nProvince <> mlngProvince Or nLocal <> mlngLocal Or nMunicipality <> mlngMunicipality Or dEffecdate <> mdtmEffecdate Or sCondition <> mstrCondition Or bFind Then
			'UPGRADE_NOTE: Object mCol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			mCol = Nothing
			mCol = New Collection
			'+ Definición de parámetros para stored procedure 'insudb.insReaCAC003'.
			With lrecReaCAC005
				.StoredProcedure = "reaAddress_riskpkg.reaAddress_risk"
				.Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				.Parameters.Add("nLocal", IIf(IsNothing(nLocal) Or nLocal = numNull, System.DBNull.Value, nLocal), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				.Parameters.Add("nMunicipality", IIf(IsNothing(nMunicipality) Or nMunicipality = numNull, System.DBNull.Value, nMunicipality), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				.Parameters.Add("dEffecdate", IIf(IsNothing(dEffecdate) Or dEffecdate = dtmNull, Today, dEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 255, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					mlngProvince = nProvince
					mlngLocal = nLocal
					mlngMunicipality = nMunicipality
					mdtmEffecdate = dEffecdate
					mstrCondition = sCondition
					Do While Not .EOF
						Call AddCAC005(0, .FieldToClass("sStreet"), .FieldToClass("sStreet1"), .FieldToClass("nCertif"), .FieldToClass("nPolicy"), .FieldToClass("nBranch"), .FieldToClass("sDescript"), .FieldToClass("sDescCurrency"), .FieldToClass("nZip_code"))
						.RNext()
					Loop 
					.RCloseRec()
				Else
					FindCAC005 = False
					mlngProvince = 0
					mlngLocal = 0
					mlngMunicipality = 0
					mdtmEffecdate = CDate(String.Empty)
					mstrCondition = String.Empty
				End If
			End With
		End If
		
FindCAC005_err: 
		If Err.Number Then
			FindCAC005 = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaCAC005 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaCAC005 = Nothing
		On Error GoTo 0
	End Function
End Class






