Option Strict Off
Option Explicit On
Public Class Claim_shw
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_shw.cls                            $%'
	'% $Author:: Ljimenez                                   $%'
	'% $Date:: 26-08-10 22:07                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	Public bClaimByIns As Boolean
	Public bRelaShip As Boolean
	Public bClient As Boolean
	Public sFirstName As String
	Public sLastName As String
	Public sLastName2 As String
	Public dBirthDat As Date
	
	Public bClaim As Boolean
	Public bProcess As Boolean
	Public bPolicy As Boolean
	
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public dEffecdate As Date
	Public dDecladat As Date
	Public dOccurdat As Date
	Public sNumForm As String
	Public nReference As Integer
	Public nAgency As Integer
	Public nOffice As Integer
	Public nOfficeAgen As Integer
	Public sPolitype As String
	Public sStatus_polDes As String
	Public nIntermed As Double
	Public sRole As String
	Public sType As String
	Public sIntermed As String
	Public sbrancht As String
	Public sProduct As String
    Public sClaimTyp As String
    Public nIdCatas As Integer
    'Descripcion del Catastro
    Public sIdCatas As String
    Public sclient As String
    Public nPersonTyp As String

	
	'% showClaimIns: busca los siniestros para un cliente en particular
	Public Function showClaimIns(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nClaim As Double, ByVal nCase_num As Integer, ByVal nDeman_type As Integer, ByVal sClient As String, ByVal nRole As Integer) As Boolean
		Dim lrecClaimByIns As eRemoteDB.Execute
		Dim lintIndex As Integer
		
		On Error GoTo showClaimIns_err
		
		lrecClaimByIns = New eRemoteDB.Execute
		
		showClaimIns = True
		
		With lrecClaimByIns
			.StoredProcedure = "shwClaimByIns"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run()
			
			Me.bClaimByIns = .FieldToClass("nClaimByIns") = 1
			Me.bClient = .FieldToClass("nClient") = 1
			Me.bRelaShip = .FieldToClass("nRelaShip") = 1
			Me.sFirstName = .FieldToClass("sFirstName")
			Me.sLastName = .FieldToClass("sLastName")
			Me.sLastName2 = .FieldToClass("sLastName2")
            Me.dBirthDat = .FieldToClass("dBirthDat")
            Me.sclient = .FieldToClass("SCLIENT")
            Me.nPersonTyp = .FieldToClass("NPERSON_TYP")
        End With
		
showClaimIns_err: 
		If Err.Number Then
			showClaimIns = False
		End If
		On Error GoTo 0
		lrecClaimByIns = Nothing
	End Function
	
	'% showClaimData: busca los siniestros para un cliente en particular
	Public Function showClaimData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nTransaction As Integer, ByVal nClaim As Double, ByVal nType As Short) As Boolean
		Dim lrecClaimData As eRemoteDB.Execute
		Dim lintIndex As Integer
		
		On Error GoTo showClaimData_err
		
		lrecClaimData = New eRemoteDB.Execute
		
		showClaimData = True
		
		With lrecClaimData
			.StoredProcedure = "shwClaimData"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run()
			
			Me.bClaim = .FieldToClass("nbClaim") = 1
			Me.bProcess = .FieldToClass("nbProcess") = 1
			Me.bPolicy = .FieldToClass("nbPolicy") = 1
			
			Me.sCertype = .FieldToClass("sCertype")
			Me.nBranch = .FieldToClass("nBranch")
			Me.nProduct = .FieldToClass("nProduct")
			Me.nPolicy = .FieldToClass("nPolicy")
			Me.nCertif = .FieldToClass("nCertif")
			Me.dEffecdate = .FieldToClass("dEffecdate")
			Me.dDecladat = .FieldToClass("dDecladat")
			Me.dOccurdat = .FieldToClass("dOccurdat")
			Me.sNumForm = .FieldToClass("sNumForm")
			Me.nReference = .FieldToClass("nReference")
			Me.nAgency = .FieldToClass("nAgency")
			Me.nOffice = .FieldToClass("nOffice")
			Me.nOfficeAgen = .FieldToClass("nOfficeAgen")
			Me.sPolitype = .FieldToClass("sPolitype")
			Me.sStatus_polDes = .FieldToClass("sStatus_polDes")
			Me.nIntermed = .FieldToClass("nIntermed")
			Me.sRole = .FieldToClass("sRole")
			Me.sType = .FieldToClass("sType")
			Me.sIntermed = .FieldToClass("sIntermed")
			Me.sbrancht = .FieldToClass("sBrancht")
			Me.sProduct = .FieldToClass("sProduct")
            Me.sClaimTyp = .FieldToClass("sClaimTyp")
            Me.nIdCatas = .FieldToClass("nIdCatas")

            Me.sIdCatas = .FieldToClass("sIdCatas")

		End With
		
showClaimData_err: 
		If Err.Number Then
			showClaimData = False
		End If
		On Error GoTo 0
		lrecClaimData = Nothing
	End Function
End Class






