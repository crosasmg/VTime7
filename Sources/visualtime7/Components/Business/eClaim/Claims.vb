Option Strict Off
Option Explicit On
Public Class Claims
	Implements System.Collections.IEnumerable
	'%-------------------------------------------------------%'
	'% $Workfile:: Claims.cls                               $%'
	'% $Author:: Nvaplat22                                  $%'
	'% $Date:: 13/04/04 11:38a                              $%'
	'% $Revision:: 14                                       $%'
	'%-------------------------------------------------------%'
	
	Private mCol As Collection
	
	'**% Add: add a new element to the collection
	'% Add: añade un nuevo elemento a la colección
	Public Function Add(ByVal nClaim As Double, ByVal sCerType As String, ByVal nCausecod As Integer, ByVal nPolicy As Integer, ByVal sClaimTyp As String, ByVal nBranch As Integer, ByVal nCertif As Integer, ByVal sClient As String, ByVal sCoinsuri As String, ByVal dCompdate As Date, ByVal dDecladat As Date, ByVal sIns_claim As String, ByVal sLeadcial As String, ByVal nLoc_cos_re As Double, ByVal nLoc_out_am As Double, ByVal nLoc_pay_am As Double, ByVal nLoc_rec_am As Double, ByVal nLoc_reserv As Double, ByVal sMailnumb As String, ByVal nMovement As Integer, ByVal nNotenum As Integer, ByVal nNullcode As Integer, ByVal dOccurdat As Date, ByVal nOffice As Integer, ByVal nOffice_own As Integer, ByVal nOffictra As Integer, ByVal dPrescdat As Date, ByVal sPrinted As String, ByVal sReinsuri As String, ByVal dShow_date As Date, ByVal sShow_statu As String, ByVal sStaclaim As Claim.Estatclaim, ByVal nUnaccode As Integer, ByVal nUsercode As Integer, ByVal nProduct As Integer, ByVal nWaitCl_Code As Integer, ByVal sNumForm As String, ByVal nTax_amo As Double, ByVal nImageNum As Integer, ByVal sCess_npr As String, ByVal sBranchDesc As String, ByVal sOfficeDesc As String, ByVal sProductDesc As String, ByVal sCauseDesc As String, ByVal sStatusDesc As String, ByVal nPremium As Double, ByVal nCapital As Double, ByVal sCliename As String, ByVal sClient2 As String) As Claim
		
		Dim objNewMember As Claim
		objNewMember = New Claim
		
		On Error GoTo Add_err
		
		With objNewMember
			.nClaim = nClaim
			.sCerType = sCerType
			.nCausecod = nCausecod
			.nPolicy = nPolicy
			.sClaimTyp = sClaimTyp
			.nBranch = nBranch
			.nCertif = nCertif
			.sClient = sClient
			.sCoinsuri = sCoinsuri
			.dCompdate = dCompdate
			.dDecladat = dDecladat
			.sIns_claim = sIns_claim
			.sLeadcial = sLeadcial
			.nLoc_cos_re = nLoc_cos_re
			.nLoc_out_am = nLoc_out_am
			.nLoc_pay_am = nLoc_pay_am
			.nLoc_rec_am = nLoc_rec_am
			.nLoc_reserv = nLoc_reserv
			.sMailnumb = sMailnumb
			.nMovement = nMovement
			.nNotenum = nNotenum
			.nNullcode = nNullcode
			.dOccurdat = dOccurdat
			.nOffice = nOffice
			.nOffice_own = nOffice_own
			.nOffictra = nOffictra
			.dPrescdat = dPrescdat
			.sPrinted = sPrinted
			.sReinsuri = sReinsuri
			.dShow_date = dShow_date
			.sShow_statu = sShow_statu
			.sStaclaim = sStaclaim
			.nUnaccode = nUnaccode
			.nUsercode = nUsercode
			.nProduct = nProduct
			.nWaitCl_Code = nWaitCl_Code
			.sNumForm = sNumForm
			.nTax_amo = nTax_amo
			.nImageNum = nImageNum
			.sCess_npr = sCess_npr
			.sBranchDesc = sBranchDesc
			.sOfficeDesc = sOfficeDesc
			.sProductDesc = sProductDesc
			.sCauseDesc = sCauseDesc
			.sStatusDesc = sStatusDesc
			.nPremium = nPremium
			.nCapital = nCapital
			.sCliename = sCliename
			.sClient2 = sClient2
		End With
		
		mCol.Add(objNewMember, "C" & nClaim)
		
		Add = objNewMember
		objNewMember = Nothing
		
Add_err: 
		On Error GoTo 0
	End Function
	
	'**% AddSIC001: add a new element to the collection
	'% AddSIC001: añade un nuevo elemento a la colección
	Public Function AddSIC001(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Object, ByVal sDemanTypeDesc As String, ByVal dOccurdat As Date, ByVal sStaclaim As String, ByVal sStaclaimDesc As String, ByVal nLoc_out_am As Double, ByVal nLoc_pay_am As Double, ByVal nTax_amo As Double, ByVal nLoc_rec_am As Double, ByVal nLoc_cos_re As Double, ByVal nClaimCost As Double, ByVal nBranch As Integer, ByVal sBranchDesc As String, ByVal nProduct As Integer, ByVal sProductDesc As String, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nBene_type As Integer, ByVal sBene_type As String, ByVal sRecover_Typ As String, ByVal sClient As String, ByVal nAmount As Double, ByVal sCliename As String) As Claim
		Dim objNewMember As Claim
		objNewMember = New Claim
		
		On Error GoTo Add_err
		
		With objNewMember
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.sDemanTypeDesc = sDemanTypeDesc
			.dOccurdat = dOccurdat
			.sStaclaim = CShort(sStaclaim)
			.sStaClaimDes = sStaclaimDesc
			.nLoc_out_am = nLoc_out_am
			.nLoc_pay_am = nLoc_pay_am
			.nTax_amo = nTax_amo
			.nLoc_rec_am = nLoc_rec_am
			.nLoc_cos_re = nLoc_cos_re
			.nClaimCost = CStr(nClaimCost)
			.nBranch = nBranch
			.sBranchDesc = sBranchDesc
			.nProduct = nProduct
			.sProductDesc = sProductDesc
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nBene_type = nBene_type
			.sBene_type = sBene_type
			.sRecover_Typ = sRecover_Typ
			.sClient2 = sClient
			.nAmount = nAmount
			.sCliename = sCliename
		End With
		
		mCol.Add(objNewMember)
		
		AddSIC001 = objNewMember
		objNewMember = Nothing
		
Add_err: 
		On Error GoTo 0
	End Function
	
	'**% AddSIC002: add a new element to the collection
	'% AddSIC002: añade un nuevo elemento a la colección
	Public Function AddSIC002(ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByRef dOccurdat As Date, ByVal sStaclaim As String, ByVal sStaclaimDesc As String, ByVal nLoc_out_am As Double, ByVal nLoc_pay_am As Double, ByVal nTax_amo As Double, ByVal nLoc_rec_am As Double, ByVal nLoc_cos_re As Double, ByVal nClaimCost As Double, ByVal sRecover_Typ As String, ByVal sClient2 As String, ByVal nAmount As Double, ByVal nPremium As Double, ByVal nRecuper As Double, ByVal nSalvata As Double) As Claim
		Dim objNewMember As Claim
		objNewMember = New Claim
		
		On Error GoTo Add_err
		
		With objNewMember
			.nClaim = nClaim
			.nCase_num = nCase_num
			.nDeman_type = nDeman_type
			.dOccurdat = dOccurdat
			.sStaclaim = CShort(sStaclaim)
			.sStaClaimDes = sStaclaimDesc
			.nLoc_out_am = nLoc_out_am
			.nLoc_pay_am = nLoc_pay_am
			.nTax_amo = nTax_amo
			.nLoc_rec_am = nLoc_rec_am
			.nLoc_cos_re = nLoc_cos_re
			.nClaimCost = CStr(nClaimCost)
			.sRecover_Typ = sRecover_Typ
			.sClient2 = sClient2
			.nAmount = nAmount
			.nPremium = nPremium
			.nRecuper = nRecuper
			.nSalvata = nSalvata
		End With
		
		mCol.Add(objNewMember, "C" & nClaim & nCase_num & nDeman_type)
		
		AddSIC002 = objNewMember
		objNewMember = Nothing
		
Add_err: 
		On Error GoTo 0
	End Function
	'''''''''''''''''''''''''''''''''''''''''''
	'**% AddSIC005: add a new element to the collection
	'% AddSIC005: añade un nuevo elemento a la colección
	Public Function AddSIC005(ByVal nClaim As Double, ByVal sOper_Type As String, ByVal sCurrency As String, ByVal dOperdate As Date, ByVal nAmount As Double, ByVal sOffice As String, ByVal sBranch As String, ByVal sProduct As String, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nInc_amount As Double, ByVal sPolitype As String, ByVal nOper_type As Integer) As Claim
		Dim objNewMember As Claim
		objNewMember = New Claim
		
		On Error GoTo Add_err
		
		With objNewMember
			.nClaim = nClaim
			.sOper_TypeDesc = sOper_Type
			.sCurrencyDesc = sCurrency
			.dOperdate = dOperdate
			.nAmount = nAmount
			.sOfficeDesc = sOffice
			.sBranchDesc = sBranch
			.sProductDesc = sProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nInc_amount = nInc_amount
			.sPolitype = sPolitype
			.nOper_type = nOper_type
		End With
		
		mCol.Add(objNewMember, "C" & mCol.Count() & nClaim)
		
		
		AddSIC005 = objNewMember
		objNewMember = Nothing
		
Add_err: 
		
		On Error GoTo 0
	End Function
	
	'**%Find: This method fills the collection with records from the table "Claim" returning TRUE or FALSE
	'**%depending on the existence of the records
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Claim" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find_Status(ByVal sStaclaim As String) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		Const dtmNull As Date = Nothing
		
		lrecClaim = New eRemoteDB.Execute
		
		On Error GoTo Find_Status_err
		'**%This constant indicates that a value is a entire type with an assigned null to pass it as a parameter
		'% Constante que indica que a un valor de tipo entero se le asigna null para pasarlo como parametro
		
		With lrecClaim
			.StoredProcedure = "reaClaim_State"
			.Parameters.Add("sStaclaim", sStaclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nClaim"), String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nPolicy"), String.Empty, .FieldToClass("nBranch"), .FieldToClass("nCertif"), String.Empty, String.Empty, dtmNull, .FieldToClass("dDecladat"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, dtmNull, String.Empty, String.Empty, dtmNull, String.Empty, CShort(sStaclaim), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nProduct"), eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("sBranch"), .FieldToClass("sOffice"), .FieldToClass("sproduct"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sCliename"), String.Empty)
					.RNext()
				Loop 
				.RCloseRec()
				Find_Status = True
			Else
				Find_Status = False
			End If
		End With
		
Find_Status_err: 
		If Err.Number Then
			Find_Status = False
		End If
		On Error GoTo 0
		lrecClaim = Nothing
	End Function
	'%Find: Este metodo carga la coleccion de elementos de la tabla "Claim" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function Find_SIO51(ByVal nClaim As Double, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal dOccurdatIni As Date, ByVal dOccurdatEnd As Date, ByVal sStaclaim As String) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		
		lrecClaim = New eRemoteDB.Execute
		
		On Error GoTo Find_SIO51_err
		'**%This constant indicates that a value is a entire type with an assigned null to pass it as a parameter
		'% Constante que indica que a un valor de tipo entero se le asigna null para pasarlo como parametro
		
		With lrecClaim
			.StoredProcedure = "reaClaim_SI051"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdatIni", dOccurdatIni, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdatEnd", dOccurdatEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Do While Not .EOF
					Call Add(.FieldToClass("nClaim"), String.Empty, eRemoteDB.Constants.intNull, .FieldToClass("nPolicy"), String.Empty, .FieldToClass("nBranch"), .FieldToClass("nCertif"), String.Empty, String.Empty, eRemoteDB.Constants.dtmNull, .FieldToClass("dDecladat"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, String.Empty, String.Empty, eRemoteDB.Constants.dtmNull, String.Empty, CShort(sStaclaim), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("nProduct"), eRemoteDB.Constants.intNull, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, String.Empty, .FieldToClass("sBranch"), .FieldToClass("sOffice"), .FieldToClass("sproduct"), String.Empty, String.Empty, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, .FieldToClass("sCliename"), String.Empty)
					.RNext()
				Loop 
				.RCloseRec()
				Find_SIO51 = True
			Else
				Find_SIO51 = False
			End If
		End With
		
Find_SIO51_err: 
		If Err.Number Then
			Find_SIO51 = False
		End If
		On Error GoTo 0
		lrecClaim = Nothing
	End Function
	
	
	'%FindSIC001: Este metodo carga la coleccion de elementos de la tabla "Claim" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindSIC001(ByVal sClient As String, ByVal nBene_type As Integer, ByVal dOccurdat As Date) As Boolean
		
		Dim lrecClaim As eRemoteDB.Execute
		
		lrecClaim = New eRemoteDB.Execute
		
		On Error GoTo Find_Status_err
		
		With lrecClaim
			.StoredProcedure = "reaClaimBenef_Claim"
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdate", IIf(dOccurdat = eRemoteDB.Constants.dtmNull, System.Date.FromOADate(Today.ToOADate - 365), dOccurdat), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Call AddSIC001(.FieldToClass("nClaim"), .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), .FieldToClass("sDemanType"), .FieldToClass("dOccurdat"), .FieldToClass("sStaClaim"), .FieldToClass("sStaClaim_Des"), .FieldToClass("nLoc_out_am"), .FieldToClass("nLoc_pay_am"), .FieldToClass("nTax_amo"), .FieldToClass("nLoc_rec_am"), .FieldToClass("nLoc_cos_re"), .FieldToClass("nClaimCost"), .FieldToClass("nBranch"), .FieldToClass("sBranch"), .FieldToClass("nProduct"), .FieldToClass("sProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nBene_type"), .FieldToClass("sBene_type"), .FieldToClass("sRecover_Typ"), .FieldToClass("sClient2"), .FieldToClass("nAmount"), .FieldToClass("sClieName"))
					.RNext()
				Loop 
				.RCloseRec()
				FindSIC001 = True
			Else
				FindSIC001 = False
			End If
		End With
		lrecClaim = Nothing
		
Find_Status_err: 
		If Err.Number Then
			FindSIC001 = False
		End If
		On Error GoTo 0
	End Function
	
	'%FindSIC002: Este metodo carga la coleccion de elementos de la tabla "Claim" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindSIC002(ByVal sCerType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dOccurdat As Date) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		
		lrecClaim = New eRemoteDB.Execute
		
		On Error GoTo Find_Status_err
		
		With lrecClaim
			.StoredProcedure = "reaClaim_Sic002"
			.Parameters.Add("sCertype", sCerType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdate", dOccurdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					Call AddSIC002(.FieldToClass("nClaim"), .FieldToClass("nCase_num"), .FieldToClass("nDeman_type"), .FieldToClass("dOccurdat"), .FieldToClass("sStaClaim"), .FieldToClass("sStaClaim_Des"), .FieldToClass("nLoc_out_am"), .FieldToClass("nLoc_pay_am"), .FieldToClass("nTax_amo"), .FieldToClass("nLoc_rec_am"), .FieldToClass("nLoc_cos_re"), .FieldToClass("nClaimCost"), .FieldToClass("sRecover_Typ"), .FieldToClass("sClient"), .FieldToClass("nAmount"), .FieldToClass("nPremium"), .FieldToClass("nRecuper"), .FieldToClass("nSalvata"))
					.RNext()
				Loop 
				.RCloseRec()
				FindSIC002 = True
			Else
				FindSIC002 = False
			End If
		End With
		lrecClaim = Nothing
		
Find_Status_err: 
		If Err.Number Then
			FindSIC002 = False
		End If
		On Error GoTo 0
	End Function
	
	'%FindSIC005: Este metodo carga la coleccion de elementos de la tabla "Claim" devolviendo Verdadero o
	'%falso, dependiendo de la existencia de los registros.
	Public Function FindSIC005(ByVal dInitDate As Date, ByVal nBranch As Integer, ByVal nOper_type As Integer, ByVal nOffice As Integer, ByVal nProduct As Integer, ByVal nCurrency As Integer) As Boolean
		Dim lrecClaim As eRemoteDB.Execute
		
		lrecClaim = New eRemoteDB.Execute
		
		On Error GoTo Find_Status_err
		
		If nBranch = eRemoteDB.Constants.intNull Then
			nBranch = 0
		End If
		If nOper_type = eRemoteDB.Constants.intNull Then
			nOper_type = 0
		End If
		If nOffice = eRemoteDB.Constants.intNull Then
			nOffice = 0
		End If
		If nProduct = eRemoteDB.Constants.intNull Then
			nProduct = 0
		End If
		If nCurrency = eRemoteDB.Constants.intNull Then
			nCurrency = 0
		End If
		
		With lrecClaim
			.StoredProcedure = "reaClaim_mov_SIC005"
			.Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Do While Not .EOF
					
					Call AddSIC005(.FieldToClass("nClaim"), .FieldToClass("sOper_Type"), .FieldToClass("sCurrency"), .FieldToClass("dOperDate"), .FieldToClass("nAmount"), .FieldToClass("sOffice"), .FieldToClass("sBranch"), .FieldToClass("sProduct"), .FieldToClass("nPolicy"), .FieldToClass("nCertif"), .FieldToClass("nInc_Amount"), .FieldToClass("sPolitype"), .FieldToClass("nOper_Type"))
					.RNext()
				Loop 
				.RCloseRec()
				FindSIC005 = True
			Else
				FindSIC005 = False
			End If
		End With
		
Find_Status_err: 
		If Err.Number Then
			FindSIC005 = False
		End If
		On Error GoTo 0
		lrecClaim = Nothing
	End Function
	
	
	'*** Item: takes an element from the collection
	'* Item: toma un elemento de la colección
	Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Claim
		Get
			Item = mCol.Item(vntIndexKey)
		End Get
	End Property
	
	'*** Count:  It counts the number of an element inside the collection
	'* Count: cuenta el número de elementos dentro de la colección
	Public ReadOnly Property Count() As Integer
		Get
			Count = mCol.Count()
		End Get
	End Property
	
	'*** NewEnum:  It enumerates the elements inside the collection
	'* NewEnum: enumera los elementos dentro de la colección
	'UPGRADE_NOTE: NewEnum property was commented out. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3FC1610-34F3-43F5-86B7-16C984F0E88E"'
	'Public ReadOnly Property NewEnum() As stdole.IUnknown
		'Get
			'NewEnum = mCol._NewEnum
		'End Get
	'End Property
	
	Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
				GetEnumerator = mCol.GetEnumerator
	End Function
	
	'***Remove:  It deletes an element inside the collection
	'* Remove: elimina un elemento dentro de la colección
	Public Sub Remove(ByRef vntIndexKey As Object)
		mCol.Remove(vntIndexKey)
	End Sub
	
	'*** Class_Initialize: It control the opening of each instance of the collection
	'* Class_Initialize: controla la apertura de cada instancia de la colección
	Private Sub Class_Initialize_Renamed()
		mCol = New Collection
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'*** Class_Terminate: It deletes the collection
	'* Class_Terminate: elimina la colección
	Private Sub Class_Terminate_Renamed()
		mCol = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






