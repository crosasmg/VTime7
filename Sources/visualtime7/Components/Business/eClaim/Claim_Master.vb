Option Strict Off
Option Explicit On
Public Class Claim_Master
	'%-------------------------------------------------------%'
	'% $Workfile:: Claim_Master.cls                         $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 7/05/04 10:07p                               $%'
	'% $Revision:: 44                                       $%'
	'%-------------------------------------------------------%'
	
	'- Se definen las propiedades principales de la clase correspondientes a la tabla Claim_Master
	'- El campo llave corresponde a nBordereaux_cl
	
	'Name                                Type             Null
	'-------------------------------------   ----------   ----------------------------
	Public nBordereaux_cl As Integer ' NOT NULL
	Public sClient As String ' NOT NULL
	Public nCover As Integer '
	Public dCompdate As Date ' NOT NULL
	Public nUserCode As Integer ' NOT NULL
	
	'-Se definen las variables auxiliares que se usan en la ventana SI737
	Public nClaim As Double
	Public nPolicy As Double
	Public nCertif As Double
	Public nDeman_type As Integer
	Public sCredit As String
	Public sAccount As String
	Public nBene_type As Integer
	Public dOccurdate As Date
	Public nClaim_caus As Integer
	Public nTotalLoss As String
	Public nAmount As Double
	Public nStatClaim As Integer
	Public sClientAseg As String
	Public dBirthdate As Date
	Public nFind_ben As Short
	
	
	
	'*%Add: Add a record to the table "Claim_master"
	'% Add: Agrega un registro a la tabla "Claim_master"
	Public Function Add(ByVal nUserCode As Integer, ByVal nCover As Integer, ByVal nBordereaux As Integer, ByVal sClient As String) As Boolean
		Dim lclsClaim_Master As eRemoteDB.Execute
		
		On Error GoTo AddSI737_Err
		
		lclsClaim_Master = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.creClaim_master'. Generated on 26/06/2002 10:25:51 a.m.
		
		With lclsClaim_Master
			.StoredProcedure = "creClaim_master"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
			
		End With
		lclsClaim_Master = Nothing
AddSI737_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		lclsClaim_Master = Nothing
	End Function
	
	'%Find: Se obtienen los datos asociados a un número de relación
	Public Function Find(ByVal nBordereaux_cl As Integer) As Boolean
		Dim lrecreaClaim_Master As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaClaim_Master = New eRemoteDB.Execute
		'Definición de parámetros para stored procedure 'insudb.reaClaim_Master'
		'Información leída el 20/09/1999 08:02:03 AM
		
		With lrecreaClaim_Master
			.StoredProcedure = "reaClaim_Master"
			.Parameters.Add("nBordereaux_cl", nBordereaux_cl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nBordereaux_cl = .FieldToClass("nBordereaux_cl")
				sClient = .FieldToClass("sClient")
				nCover = .FieldToClass("nCover")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecreaClaim_Master = Nothing
	End Function
	
    Public Function InsPostSI737_Upd(ByVal pstrCodispl As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicyHeader As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal sProvider As String, ByVal nRelat As Integer, ByVal nCause As Integer, ByVal nRelation As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nDemanType As Integer, ByVal nCredit As Integer, ByVal nAccount As Integer, ByVal nGroup As Integer, ByVal nBene_type As Integer, ByVal sClient As String, ByVal dOccurdate As Date, ByVal sTotalLoss As String, ByVal nAmount As Double, ByVal nClaim As Double, ByVal nState As Integer, ByVal nUserCode As Integer, Optional ByVal sIllness As String = "", Optional ByVal sStatClaim As String = "", Optional ByVal nFind_bene As Short = 0) As Boolean
        Dim lrecInsPostSI737_Upd As eRemoteDB.Execute

        On Error GoTo InsPostSI737_Upd_Err

        lrecInsPostSI737_Upd = New eRemoteDB.Execute

        With lrecInsPostSI737_Upd
            .StoredProcedure = "INSSI737PKG.INSPOSTSI737_UPD"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicyHeader", nPolicyHeader, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProvider", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelat", nRelat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRelation", nRelation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDemanType", nDemanType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBene_type", nBene_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dOccurdate", dOccurdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTotalLoss", sTotalLoss, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUserCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sillness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStaClaim", sStatClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFind_bene", nFind_bene, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsPostSI737_Upd = (.Parameters("nStatus").Value = 1)
            Else
                InsPostSI737_Upd = False
            End If
        End With
InsPostSI737_Upd_Err:
        If Err.Number Then
            InsPostSI737_Upd = False
        End If
        lrecInsPostSI737_Upd = Nothing
        On Error GoTo 0
    End Function
	
	
	
	
	'*%InsPostSI737: Pass of the information introduced towards the layers of rules of business and access of data.
	'% InsPostSI737: Pase de la información introducida hacia las capas de reglas de negocio y acceso de datos.
    Public Function InsPostSI737_Upd_Old(ByVal pstrCodispl As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicyHeader As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal sProvider As String, ByVal nRelat As Integer, ByVal nCause As Integer, ByVal nRelation As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nDemanType As Integer, ByVal nCredit As Integer, ByVal nAccount As Integer, ByVal nGroup As Integer, ByVal nBene_type As Integer, ByVal sClient As String, ByVal dOccurdate As Date, ByVal sTotalLoss As String, ByVal nAmount As Double, ByVal nClaim As Double, ByVal nState As Integer, ByVal nUserCode As Integer, ByVal nIdcatas As Integer) As Boolean
        Dim lclsClaim As Claim = New Claim
        Dim lclsCl_cover As Cl_Cover
        Dim lclsClaim_win As Claim_win = New Claim_win
        Dim lclsCases_win As Cases_win
        Dim lclsClaim_auto As Claim_auto
        Dim lclsClaim_case As Claim_case
        Dim lclscl_covers As CL_Covers
        Dim lclsOpt_sinies As Opt_sinies
        Dim lclsAuto As ePolicy.Automobile
        Dim lclsAuto_db As ePolicy.Auto_db
        Dim lclsRole As ePolicy.Roles
        Dim lclsSecurity As eSecurity.Secur_sche
        Dim lclsUser As eSecurity.User = New eSecurity.User
        Dim lclsExchange As eGeneral.Exchange
        Dim sReservstat As String
        Dim lstrClient As String
        Dim lstrClient_Cont As String
        Dim ldblLocAmount As Double
        Dim ldblExchange As Double

        '- Objeto para el manejo de póliza
        Dim lobjPolicy As Object

        '- Objeto para el manejo de product master
        Dim lobjProductMaster As Object

        On Error GoTo InsPostSI737_Upd_Old_err

        lobjPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
        lobjProductMaster = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
        lclsOpt_sinies = New Opt_sinies

        InsPostSI737_Upd_Old = True

        Call lclsOpt_sinies.Find()

        '+Se crea el registro en claim_master

        If nRelation = eRemoteDB.Constants.intNull Then
            nRelation = nRelat
        End If

        If nPolicy = eRemoteDB.Constants.intNull Then
            nPolicy = nPolicyHeader
        End If

        If nRelation <> eRemoteDB.Constants.intNull Then
            If Not Find(nRelation) Then
                InsPostSI737_Upd_Old = Add(nUserCode, nCover, nRelation, sClient)
            End If
        End If

        '+Se inserta el registro en la tabla Claim
        If InsPostSI737_Upd_Old Then
            lclsClaim = New Claim
            lclsClaim.nBordereaux_cl = nRelation
            InsPostSI737_Upd_Old = lclsClaim.insPostSI001(Claim_win.eClaimTransac.clngClaimIssue, dEffecdate, nClaim, nOffice, nOfficeAgen, nAgency, nBranch, nProduct, nPolicy, nCertif, String.Empty, dCompdate, eRemoteDB.Constants.intNull, nUserCode, dOccurdate, nIdcatas)
            If InsPostSI737_Upd_Old Then
                If lclsClaim.Find(nClaim, True) Then
                    lclsClaim.nBordereaux_cl = nRelation
                    lclsClaim.sClaimTyp = sTotalLoss
                    InsPostSI737_Upd_Old = lclsClaim.Update
                End If
            End If
        End If

        If InsPostSI737_Upd_Old Then

            '+ Se cargan los datos que corresponden con la póliza
            Call lobjPolicy.Find("2", nBranch, nProduct, nPolicy)

            '+ Se cargan los datos que corresponden con el certificado.
            Call lobjProductMaster.FindProdMaster(nBranch, nProduct)

            '+ Obtiene todos los datos de cada ventana que pertenece a la sequencia.
            lclsClaim_win = New Claim_win
            Call lclsClaim_win.LoadTabs(CStr(Claim_win.eClaimTransac.clngClaimIssue), CStr(nClaim), lobjProductMaster.sbrancht, lobjPolicy.sBussityp, "", CStr(nUserCode))
        End If

        '+Se inserta el registro en Claim_Case y ClaimBenef
        If InsPostSI737_Upd_Old Then
            '+ Contratante de la poliza
            '+ Si el denunciante es el mismo que el contratan no se guarda contratante
            '+ Se busca el contratante de la poliza
            lclsRole = New ePolicy.Roles
            If lclsRole.Find("2", nBranch, nProduct, nPolicy, nCertif, ePolicy.Roles.eRoles.eRolContratanting, "0", dEffecdate, True) Then
                lstrClient_Cont = lclsRole.SCLIENT
            Else
                lstrClient_Cont = String.Empty
            End If
            lclsRole = Nothing
            '+
            If lstrClient_Cont <> String.Empty And nPolicyHeader = eRemoteDB.Constants.intNull And nBene_type <> 1 Then
                '           If sProvider <> lstrClient_Cont Then
                '+ Contratante
                InsPostSI737_Upd_Old = lclsClaim.insPostSI004(nClaim, 1, nDemanType, eRemoteDB.Constants.intNull, CStr(6), eRemoteDB.Constants.intNull, "1", lstrClient_Cont, "", "2", 1, dOccurdate, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, nOffice, nOfficeAgen, nAgency, eRemoteDB.Constants.intNull, nUserCode, nCause, String.Empty, String.Empty, String.Empty, "Add", "PopUp", sTotalLoss, True)
            End If

            '+ Denunciante RUT
            If nCover <> eRemoteDB.Constants.intNull Then
                InsPostSI737_Upd_Old = lclsClaim.insPostSI004(nClaim, 1, nDemanType, eRemoteDB.Constants.intNull, CStr(6), eRemoteDB.Constants.intNull, "1", sProvider, "", "2", 25, dOccurdate, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, nOffice, nOfficeAgen, nAgency, eRemoteDB.Constants.intNull, nUserCode, nCause, String.Empty, String.Empty, String.Empty, "Add", "PopUp", sTotalLoss, True)
            Else
                InsPostSI737_Upd_Old = lclsClaim.insPostSI004(nClaim, 1, nDemanType, eRemoteDB.Constants.intNull, CStr(6), eRemoteDB.Constants.intNull, "1", sProvider, "", "2", 1, dOccurdate, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, nOffice, nOfficeAgen, nAgency, eRemoteDB.Constants.intNull, nUserCode, nCause, String.Empty, String.Empty, String.Empty, "Add", "PopUp", sTotalLoss, True)
            End If


            '+ Asegurado afectado
            If InsPostSI737_Upd_Old Then
                InsPostSI737_Upd_Old = lclsClaim.insPostSI004(nClaim, 1, nDemanType, eRemoteDB.Constants.intNull, CStr(6), eRemoteDB.Constants.intNull, "1", sClient, "", "1", nBene_type, dOccurdate, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, nOffice, nOfficeAgen, nAgency, eRemoteDB.Constants.intNull, nUserCode, nCause, String.Empty, String.Empty, String.Empty, "Add", "PopUp", sTotalLoss, True)
            End If
        End If

        '+Si se trata del ramo automovil se incluyen los datos del auto.
        If lobjProductMaster.sbrancht = 3 Then
            lclsAuto = New ePolicy.Automobile
            lclsAuto_db = New ePolicy.Auto_db
            If lclsAuto.Find("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate, True) Then
                If lclsAuto_db.Find(lclsAuto.sRegist, True) Then
                    lclsCases_win = New Cases_win
                    Call lclsCases_win.LoadTabs("1", nClaim, 1, nDemanType, 1, lobjProductMaster.sbrancht, String.Empty, nUserCode, False)
                    lclsCases_win = Nothing

                    lclsRole = New ePolicy.Roles
                    If lclsRole.Find("2", nBranch, nProduct, nPolicy, nCertif, ePolicy.Roles.eRoles.eRolUsalDirver, "0", dEffecdate, True) Then
                        lstrClient = lclsRole.SCLIENT
                    Else
                        lstrClient = sClient
                    End If
                    lclsRole = Nothing

                    lclsClaim_auto = New Claim_auto
                    If lclsClaim_auto.insPostSI018("SI737", nClaim, eRemoteDB.Constants.intNull, "4", lstrClient, "3", "", "", "", eRemoteDB.Constants.intNull, nUserCode, 1, nDemanType, lstrClient, "", "", "", "", eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.dtmNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, "", eRemoteDB.Constants.intNull, "", eRemoteDB.Constants.dtmNull, "", "", "", "", "") Then
                        lclsClaim_case = New Claim_case
                        If nCover > 0 Then
                            Call lclsClaim_case.UpdatesStareserve(nClaim, nDemanType, 1, "2")
                        Else
                            Call lclsClaim_case.UpdatesStareserve(nClaim, nDemanType, 1, "6")
                        End If
                        Call lclsClaim_win.Add_Claim_win(nClaim, "SI016", "2", nUserCode)
                    End If
                    lclsClaim_auto = Nothing
                    lclsClaim_win = Nothing
                End If
            End If
            lclsAuto = Nothing
            lclsAuto_db = Nothing

            '+Si se trata de generales se coloca el caso como con contenido, ya que no existen ventanas
            '+de datos particulares en siniestros para este tipo de ramo.

        ElseIf lobjProductMaster.sbrancht = 1 Then
            lclsClaim_case = New Claim_case
            If nCover > 0 Then
                Call lclsClaim_case.UpdatesStareserve(nClaim, nDemanType, 1, "2")
            Else
                Call lclsClaim_case.UpdatesStareserve(nClaim, nDemanType, 1, "6")
            End If
        End If

        '+Si se indico cobertura se insertan los registros correspondientes en cl_cover, cl_m_cover, cov_used y claim_his
        If nCover > 0 Then
            lclscl_covers = New CL_Covers
            lclsCl_cover = New Cl_Cover
            If lclscl_covers.Find_SI007("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate, lobjProductMaster.sbrancht, nClaim, 1, nDemanType, CShort(lclsClaim.sClaimTyp)) Then
                lclsSecurity = New eSecurity.Secur_sche
                lclsUser = New eSecurity.User
                For Each lclsCl_cover In lclscl_covers
                    Call lclsUser.Find(nUserCode)
                    If lclsSecurity.Reload(eSecurity.Secur_sche.eTypeTable.Limits, lclsUser.sSche_code) Then
                        If Not lclsSecurity.valLimits(eSecurity.Secur_sche.eTypeLimits.clngLimitsClaimDec, lclsUser.sSche_code, CShort(nBranch), CShort(nCurrency), CDec(nAmount), nProduct) Then

                            '**+ Pending status of approval is assigned due to the surpass of the declaration limit
                            '+ Se asigna estado pendiente de aprobación ya que fue sobrepasado el límite de declaración.
                            sReservstat = "2"
                        End If
                    End If
                    If lclsCl_cover.nCover = nCover Then
                        lclsCl_cover.sFrantype = IIf(lclsCl_cover.sFrantype = "", "1", lclsCl_cover.sFrantype)
                        lclsExchange = New eGeneral.Exchange
                        Call lclsExchange.Convert(0, nAmount, nCurrency, lclsOpt_sinies.nCurrency, dEffecdate, ldblLocAmount)
                        ldblLocAmount = lclsExchange.pdblResult
                        ldblExchange = lclsExchange.pdblExchange
                        InsPostSI737_Upd_Old = lclsCl_cover.insPostSI007(nClaim, Claim_win.eClaimTransac.clngClaimIssue, lclsCl_cover.sClient, eRemoteDB.Constants.intNull, dEffecdate, String.Empty, nUserCode, nCurrency, eRemoteDB.Constants.intNull, 1, nDemanType, True, eRemoteDB.Constants.dtmNull, ldblExchange, nAmount, nAmount, 0, lclsCl_cover.nFra_amount, lclsCl_cover.nFrandeda, 0, lclsCl_cover.nBranch_est, lclsCl_cover.nBranch_rei, lclsCl_cover.nBranch_led, lclsCl_cover.nModulec, lclsCl_cover.nCover, lclsCl_cover.nGroup, lclsCl_cover.sReservstat, lclsCl_cover.sFrantype, lclsCl_cover.sAutomrep, "1", 0, ldblLocAmount, 0, nAmount, 0, dEffecdate, "2", String.Empty, 0)
                        If InsPostSI737_Upd_Old Then
                            Call lclsCl_cover.insPostSI007_total(nClaim, 1, nDeman_type, eRemoteDB.Constants.intNull, nCurrency, dEffecdate, ldblLocAmount, nAmount, nUserCode)
                        End If
                        lclsExchange = Nothing
                    End If
                Next lclsCl_cover
            End If
            lclscl_covers = Nothing
            lclsCl_cover = Nothing
            lclsSecurity = Nothing
        End If

        '+Se realiza el llamado al proceso que se encarga de finalizar la declaracion del siniestro (similar a SI050)

        If InsPostSI737_Upd_Old Then
            If lclsUser Is Nothing Then
                lclsUser = New eSecurity.User
                Call lclsUser.Find(nUserCode)
            End If
            InsPostSI737_Upd_Old = insFinishClaim(1, CShort(nBranch), nProduct, nPolicy, nClaim, lclsUser.sSche_code, nUserCode)
            lclsUser = Nothing
        End If

InsPostSI737_Upd_Old_err:
        If Err.Number Then
            InsPostSI737_Upd_Old = False
        End If

        On Error GoTo 0
        lobjPolicy = Nothing
        lobjProductMaster = Nothing
        lclsOpt_sinies = Nothing

    End Function
	
	
	
	Public Function InsValSI737_k(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal nRelat As Integer, ByVal dLedgerdat As Object, ByVal sClient As String, ByVal nCompany As Integer) As String
		
		
		Dim lrecInsValSI737_k As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = ""

        On Error GoTo InsValSI737_k_Err
		
		lrecInsValSI737_k = New eRemoteDB.Execute
		
		With lrecInsValSI737_k
			.StoredProcedure = "INSSI737PKG.INSVALSI737_K"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOfficeAgen", nOfficeAgen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLedgerdat", dLedgerdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCompany", nCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("ArrayErrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrorAll = .Parameters("Arrayerrors").Value
			End If
		End With
		
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrorAll)
			End If
			InsValSI737_k = .Confirm
		End With
		
		lclsErrors = Nothing
		lrecInsValSI737_k = Nothing
		
InsValSI737_k_Err: 
		If Err.Number Then
			InsValSI737_k = InsValSI737_k & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lrecInsValSI737_k = Nothing
	End Function
	
	
	
	'*%InsValSI737_k_Old: Validation of the data for the page of the headed one.
	'% InsValSI737_k_Old: Validación de los datos para la página del encabezado.
	Public Function InsValSI737_k_Old(ByVal sCodispl As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal nRelat As Integer, ByVal dLedgerdat As Object, ByVal sClient As String, ByVal nCompany As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsPremium As Object
		Dim lblnPendingDraft As Boolean
		Dim lclsLedCompan As Object
		Dim lclsCtrol_Date As eGeneral.Ctrol_date
		Dim lintCount As Integer
		Dim lclsValField As eFunctions.valField
		Dim lclsValues As eFunctions.Tables
		Dim lcolFinance As Object
		Dim lclsFinance As Object
		Dim ldtmLastPayDate As Date
		Dim lclsProduct As eProduct.Product
		Dim lclsClient As eClient.Client
		Dim lclsRoles As New ePolicy.Roleses
		
		On Error GoTo InsValSI737_k_Old_Err
		
		lclsErrors = New eFunctions.Errors
		lcolFinance = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDrafts")
		lclsFinance = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDraft")
		lclsProduct = New eProduct.Product
		lclsPolicy = New ePolicy.Policy
		lclsValues = New eFunctions.Tables
		lclsValField = New eFunctions.valField
		lclsCtrol_Date = New eGeneral.Ctrol_date
		lclsClient = New eClient.Client
		
		'+Se valida el campo "Fecha de denuncio"
		
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 55746)
		Else
			If dEffecdate > Today Then
				Call lclsErrors.ErrorMessage(sCodispl, 13282)
			End If
		End If
		
		'+ Se valida el campo sucursal
		If nOffice <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9120)
		End If
		
		'+ Validacion sobre el campo Oficina
		If nOfficeAgen = eRemoteDB.Constants.intNull Or nOfficeAgen = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55519)
		End If
		
		'+ Validacion sobre el campo Agencia
		If nAgency = eRemoteDB.Constants.intNull Or nAgency = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1080)
		End If
		
		'+ Se valida el campo Ramo
		If nBranch <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1022)
		End If
		
		'+ Se valida el campo Producto
		If nProduct <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1014)
		End If
		
		'+ Se valida el campo Póliza
		If nPolicy > 0 Then
			If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
				Call lclsErrors.ErrorMessage(sCodispl, 3001)
			Else
				Call lclsProduct.Find(nBranch, nProduct, lclsPolicy.dIssuedat)
				If lclsPolicy.sStatus_pol = "3" Then
					Call lclsErrors.ErrorMessage(sCodispl, 3720)
				End If
				If lclsPolicy.sStatus_pol = "6" Then
					Call lclsErrors.ErrorMessage(sCodispl, 3098)
				End If
				If lclsPolicy.sStatus_pol <> "6" And lclsPolicy.sStatus_pol <> "3" Then
					
					lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
					lblnPendingDraft = False
					If lclsPremium.insLoadReceiptsPerPolicy("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
						If lclsPremium.Item(lclsPremium.CountItem) Then
							Call lclsPremium.Find("2", lclsPremium.nReceipt, nBranch, nProduct, 0, 0)
							If lclsPremium.nStatus_pre = 1 Or lclsPremium.nStatus_pre = 4 Then
								
								'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
								If (DateDiff(Microsoft.VisualBasic.DateInterval.Day, dEffecdate, lclsPremium.dExpirdat) < lclsProduct.nNotCancelDay) Or lclsProduct.nNotCancelDay <= 0 Then
									Call lclsErrors.ErrorMessage(sCodispl, 55727)
								Else
									'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
									If (DateDiff(Microsoft.VisualBasic.DateInterval.Day, dEffecdate, lclsPremium.dExpirdat) > lclsProduct.nNotCancelDay) Or lclsProduct.nNotCancelDay > 0 Then
										Call lclsErrors.ErrorMessage(sCodispl, 55727)
									End If
								End If
							End If
						End If
					End If
					
					'+ Si Poliza y Certificado estan correctos
					lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
					lblnPendingDraft = False
					
					'+ Si la póliza no está en convenio de pago no puede tener recibos pendientes de cobro
					If lclsPremium.Find_ByPolicy("2", nBranch, nProduct, nPolicy, nCertif, lclsPolicy.sColinvot, Nothing) Then
						If lcolFinance.Find_ClaimDraftCollect(nClaim) Then
							For lintCount = 1 To lcolFinance.Count
								If lclsFinance.nStat_draft = 1 Then
									lblnPendingDraft = True
									Exit For
								End If
							Next 
						End If
						If Trim(lclsPolicy.sConColl) = String.Empty Or lclsPolicy.sConColl = "2" Then
							If lblnPendingDraft Then
								Call lclsErrors.ErrorMessage(sCodispl, 4329)
							End If
						Else
							If lclsPremium.FindLastPayDate("2", nBranch, nProduct, nPolicy) Then
								ldtmLastPayDate = lclsPremium.dExpirdat
								'+ Si la resta de la fecha de ocurrencia del siniestro menos la fecha "HASTA" del último recibo pagado
								'+ es menor a la cantidad de días de gracia, el mensaje #4287 es mostrado como una ADVERTENCIA;
								'+ de lo contrario es mostrado como un ERROR - ACM - 04/06/2002
							End If
							If lblnPendingDraft Then
								Call lclsErrors.ErrorMessage(sCodispl, 4287)
							End If
						End If
					End If
					'+ Poliza/Certificado no debe tener recibos con estado pendiente y origen Rehabilitar
					
					If Not lclsPremium.insValPendPremRehab("2", nBranch, nProduct, nPolicy, 0) Then
						Call lclsErrors.ErrorMessage(sCodispl, 3663)
					End If
					If lclsPolicy.dStartdate > dEffecdate Then
						Call lclsErrors.ErrorMessage(sCodispl, 55747)
					End If
					'+ Poliza/Certificado no debe tener recibos con estado pendiente y origen Rehabilitar
					If Not lclsPremium.insValPendPremRehab("2", nBranch, nProduct, nPolicy, 0) Then
						Call lclsErrors.ErrorMessage(sCodispl, 3663)
					End If
					If lclsPolicy.dStartdate > dEffecdate Then
						Call lclsErrors.ErrorMessage(sCodispl, 55747)
					End If
				End If
				
				'+ Se verifica la vigencia de la póliza.
				If dEffecdate < lclsPolicy.dStartdate Then
					'lclsErrors.sTypeMessage = Warning
					Call lclsErrors.ErrorMessage(sCodispl, 4019)
				End If
				
				If dEffecdate > lclsPolicy.dExpirdat Then
					'lclsErrors.sTypeMessage = Warning
					Call lclsErrors.ErrorMessage(sCodispl, 4019)
				End If
			End If
		End If
		
		'+ Si el campo cobertura está lleno, el campo Moneda tambien debe estarlo.
		If nCover > 0 Then
			If nCurrency <= 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 750024)
			End If
		End If
		
		'+ Se valida el campo "Fecha de contabilizacion"
		If dLedgerdat = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1087)
		Else
			lclsLedCompan = eRemoteDB.NetHelper.CreateClassInstance("eLedge.Led_compan")
			Call lclsLedCompan.Find(nCompany)
			
			'+ Si la transacción no corresponde a una consulta
			If lclsLedCompan.dDate_init <> eRemoteDB.Constants.dtmNull And lclsLedCompan.dDate_init <> 0 Then
				With lclsValField
					.objErr = lclsErrors
					.Min = lclsLedCompan.dDate_init
					.Max = String.Empty
					.ErrRange = 1006
					If .ValDate(dLedgerdat,  , eFunctions.valField.eTypeValField.onlyvalid) Then
						'+ Se hace el llamado a la función que consigue la fecha del último asiento
						'+ automático de siniestro
						Call lclsCtrol_Date.Find(2)
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						If Not IsDbNull(lclsCtrol_Date.dEffecdate) And lclsCtrol_Date.dEffecdate <> eRemoteDB.Constants.dtmNull Then
							If dLedgerdat < lclsCtrol_Date.dEffecdate Then
								Call lclsErrors.ErrorMessage(sCodispl, 1008)
							End If
						End If
					End If
				End With
			End If
		End If
		
		'+ Se valida el campo "Denunciante RUT"
		If sClient = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 55748)
		Else
			If Not lclsClient.Find(sClient) Then
				Call lclsErrors.ErrorMessage(sCodispl, 2044)
			Else
				If lclsClient.dDeathdat <> eRemoteDB.Constants.dtmNull Then
					Call lclsErrors.ErrorMessage(sCodispl, 2051)
				End If
			End If
			
			'        If nPolicy > 0 Then
			'            If Not lclsRoles.Find_by_Policy("2", nBranch, nProduct, _
			''                                            nPolicy, nCertif, sClient, _
			''                                            dEffecdate) Then
			'             '   lclsErrors.sTypeMessage = Warning
			'                Call lclsErrors.ErrorMessage(sCodispl, 4025)
			'            End If
			'        End If
		End If
		
		InsValSI737_k_Old = lclsErrors.Confirm
		
InsValSI737_k_Old_Err: 
		If Err.Number Then
			InsValSI737_k_Old = InsValSI737_k_Old & Err.Description
		End If
		On Error GoTo 0
		
		lclsErrors = Nothing
		lclsPolicy = Nothing
		lclsPremium = Nothing
		lclsLedCompan = Nothing
		lclsCtrol_Date = Nothing
		lclsValField = Nothing
		lclsValues = Nothing
		lcolFinance = Nothing
		lclsFinance = Nothing
		lclsProduct = Nothing
		lclsRoles = Nothing
	End Function
	
	
	'*%InsValSI737: Validation of the data for the page details.
	'% InsValSI737: Validación de los datos para la página detalle.
	Public Function InsValSI737(ByVal pstrCodispl As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicyHeader As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal nRelat As Integer, ByVal nCause As Integer, ByVal nRelation As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nDemanType As Integer, ByVal nCredit As Integer, ByVal nAccount As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal dOccurdate As Date, ByVal sTotalLoss As String, ByVal nAmount As Double, ByVal nClaim As Double, ByVal sStaclaim As String) As Object
		
		Dim lrecInsValSI737 As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = ""

        On Error GoTo InsValSI737_Err
		
		lrecInsValSI737 = New eRemoteDB.Execute
		
		With lrecInsValSI737
			.StoredProcedure = "INSSI737PKG.INSVALSI737"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicyHeader", nPolicyHeader, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCause", nCause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOccurdate", dOccurdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStaclaim", sStaclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nFind_ben", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ArrayErrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				lstrErrorAll = .Parameters("Arrayerrors").Value
				nFind_ben = .Parameters("nFind_ben").Value
			End If
		End With
		
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage(pstrCodispl,  ,  ,  ,  ,  , lstrErrorAll)
			End If
			InsValSI737 = .Confirm
		End With
		
		lclsErrors = Nothing
		lrecInsValSI737 = Nothing
		
InsValSI737_Err: 
		If Err.Number Then
			InsValSI737 = InsValSI737 & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
		lrecInsValSI737 = Nothing
	End Function
	
	
	
	'*%InsValSI737_Old: Validation of the data for the page details.
	'% InsValSI737_Old: Validación de los datos para la página detalle.
	Public Function InsValSI737_Old(ByVal pstrCodispl As String, ByVal dEffecdate As Date, ByVal nOffice As Integer, ByVal nOfficeAgen As Integer, ByVal nAgency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicyHeader As Integer, ByVal nCover As Integer, ByVal nCurrency As Integer, ByVal nRelat As Integer, ByVal nCause As Integer, ByVal nRelation As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nDemanType As Integer, ByVal nCredit As Integer, ByVal nAccount As Integer, ByVal nGroup As Integer, ByVal nRole As Integer, ByVal sClient As String, ByVal dOccurdate As Date, ByVal sTotalLoss As String, ByVal nAmount As Double, ByVal nClaim As Double, ByVal nState As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsClaim As Claim
		Dim lclsValues As eFunctions.Tables
		Dim lclsProduct As eProduct.Product
		Dim lclsClient As eClient.Client
		Dim lclsRoles As ePolicy.Roleses
		Dim lintCount As Integer
		Dim lclsCertificat As Object
        Dim lclsPremium As Object = New Object

        Dim lblnPendingDraft As Boolean
		Dim lcolFinance As Object
		Dim lclsFinance As Object
		Dim ldtmLastPayDate As Date
		
		Dim lblnFindPol As Boolean
		Dim blnStatus_pre As Boolean
		
		On Error GoTo InsValSI737_Old_Err
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy = New ePolicy.Policy
		
		lcolFinance = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDrafts")
		lclsFinance = eRemoteDB.NetHelper.CreateClassInstance("eFinance.FinanceDraft")
		
		lblnFindPol = False
		blnStatus_pre = False
		
		'+Se valida el campo Póliza
		If nPolicy <= 0 And nPolicyHeader <= 0 Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 3003)
		Else
			If nPolicy > 0 Then
				If Not lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
					Call lclsErrors.ErrorMessage(pstrCodispl, 3001)
				Else
					lclsProduct = New eProduct.Product
					Call lclsProduct.Find(nBranch, nProduct, lclsPolicy.dIssuedat)
					
					lblnFindPol = True
					If lclsPolicy.sStatus_pol = "3" Then
						Call lclsErrors.ErrorMessage(pstrCodispl, 3720)
					End If
					If lclsPolicy.sStatus_pol = "6" Then
						Call lclsErrors.ErrorMessage(pstrCodispl, 3098)
					End If
					If lclsPolicy.sStatus_pol <> "6" And lclsPolicy.sStatus_pol <> "3" Then
						
						lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
						
						If lclsPremium.insLoadReceiptsPerPolicy("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
							If lclsPremium.Item(lclsPremium.CountItem) Then
								Call lclsPremium.Find("2", lclsPremium.nReceipt, nBranch, nProduct, 0, 0)
								If lclsPremium.nStatus_pre = 1 Or lclsPremium.nStatus_pre = 4 Then
									
									'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
									If (DateDiff(Microsoft.VisualBasic.DateInterval.Day, dOccurdate, lclsPremium.dExpirdat) < lclsProduct.nNotCancelDay) Or lclsProduct.nNotCancelDay <= 0 Then
										Call lclsErrors.ErrorMessage(pstrCodispl, 55727)
										blnStatus_pre = True
									Else
										'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
										If (DateDiff(Microsoft.VisualBasic.DateInterval.Day, dOccurdate, lclsPremium.dExpirdat) > lclsProduct.nNotCancelDay) Or lclsProduct.nNotCancelDay > 0 Then
											If Not blnStatus_pre Then
												Call lclsErrors.ErrorMessage(pstrCodispl, 55727)
												blnStatus_pre = True
											End If
										End If
									End If
								End If
							End If
						End If
						
						'+Si la póliza no está en convenio de pago no puede tener recibos pendientes de cobro
						If lclsPremium.Find_ByPolicy("2", nBranch, nProduct, nPolicy, 0, lclsPolicy.sColinvot, Nothing) Then
							
							If lclsPremium.insLoadReceiptsPerPolicy("2", nBranch, nProduct, nPolicy, 0, dEffecdate) Then
								For lintCount = 1 To lclsPremium.CountItem
									If lclsPremium.Item(lintCount) Then
										If lclsPremium.Find_Receipt_Exist(lclsPremium.nReceipt) Then
											If lclsPremium.nStatus_pre = 1 Or lclsPremium.nStatus_pre = 4 Then
												If Not blnStatus_pre Then
													Call lclsErrors.ErrorMessage(pstrCodispl, 55727)
												End If
												Exit For
											End If
										End If
									End If
								Next 
							End If
						End If
						
						'+Si Poliza y Certificado estan correctos
						lclsPremium = eRemoteDB.NetHelper.CreateClassInstance("eCollection.Premium")
						lblnPendingDraft = False
						
						'+Si la póliza no está en convenio de pago no puede tener recibos pendientes de cobro
						If lclsPremium.Find_ByPolicy("2", nBranch, nProduct, nPolicy, nCertif, lclsPolicy.sColinvot, Nothing) Then
							If lcolFinance.Find_ClaimDraftCollect(nClaim) Then
								For lintCount = 1 To lcolFinance.Count
									If lclsFinance.nStat_draft = 1 Then
										lblnPendingDraft = True
										Exit For
									End If
								Next 
							End If
							If Trim(lclsPolicy.sConColl) = String.Empty Or lclsPolicy.sConColl = "2" Then
								If lblnPendingDraft Then
									Call lclsErrors.ErrorMessage(pstrCodispl, 4329)
								End If
							Else
								If lclsPremium.FindLastPayDate("2", nBranch, nProduct, nPolicy) Then
									ldtmLastPayDate = lclsPremium.dExpirdat
									'+ Si la resta de la fecha de ocurrencia del siniestro menos la fecha "HASTA" del último recibo pagado
									'+ es menor a la cantidad de días de gracia, el mensaje #4287 es mostrado como una ADVERTENCIA;
									'+ de lo contrario es mostrado como un ERROR - ACM - 04/06/2002
								End If
								If lblnPendingDraft Then
									Call lclsErrors.ErrorMessage(pstrCodispl, 4287)
								End If
							End If
						End If
					End If
					'+ Poliza/Certificado no debe tener recibos con estado pendiente y origen Rehabilitar
					
					If Not lclsPremium.insValPendPremRehab("2", nBranch, nProduct, nPolicy, 0) Then
						Call lclsErrors.ErrorMessage(pstrCodispl, 3663)
					End If
					lclsPremium = Nothing
					
					If lclsPolicy.dStartdate > dEffecdate Then
						Call lclsErrors.ErrorMessage(pstrCodispl, 55747)
					End If
					'+Se verifica que la cobertura indicada este asociada a la poliza
					If nCover <> eRemoteDB.Constants.intNull Then
						lclsValues = New eFunctions.Tables
						
						With lclsValues
							.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							
							If Not .reaTable("TabCover_pol", nCover) Then
								Call lclsErrors.ErrorMessage(pstrCodispl, 4249)
							Else
								If nPolicy <> eRemoteDB.Constants.intNull Then
                                    If .Fields("nCurrency") <> nCurrency Then
                                        Call lclsErrors.ErrorMessage(pstrCodispl, 60456)
                                    End If
								End If
							End If
						End With
						lclsValues = Nothing
					End If
				End If
			Else
				nPolicy = nPolicyHeader
				If lclsPolicy.Find("2", nBranch, nProduct, nPolicy) Then
					lblnFindPol = True
				End If
			End If
		End If
		
		'+Se valida el campo "Item"
		If lblnFindPol Then
			If lclsPolicy.sPolitype <> "1" And (nCertif = 0 Or nCertif = eRemoteDB.Constants.intNull) Then
				Call lclsErrors.ErrorMessage(pstrCodispl, 3006)
			Else
				
				'+Se realiza el llamado a la rutina de lectura del certificado
				If lclsPolicy.sPolitype <> "1" Then
					lclsCertificat = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Certificat")
					If Not lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif) Then
						If lclsPolicy.sPolitype <> "1" And nCertif <> 0 And nCertif <> eRemoteDB.Constants.intNull Then
							Call lclsErrors.ErrorMessage(pstrCodispl, 3010)
						End If
					Else
						'+ Se valida el estado del certificado
						If lclsCertificat.sStatusva = "3" Then
							Call lclsErrors.ErrorMessage(pstrCodispl, 3723)
						End If
						If nCover > 0 Then
							lclsValues = New eFunctions.Tables
							If dEffecdate = eRemoteDB.Constants.dtmNull Then
								dEffecdate = Today
							End If
							
							With lclsValues
								.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, 0, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
								
								If Not .reaTable("TabCover_pol", nCover) Then
									Call lclsErrors.ErrorMessage(pstrCodispl, 4249)
								End If
							End With
							lclsValues = Nothing
						End If
					End If
					lclsCertificat = Nothing
				End If
			End If
		End If
		
		'+Se valida la Figura
		If nRole <= 0 Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 4297)
		End If
		
		'+Se valida el Cliente
		If sClient = String.Empty Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 4122)
		Else
			lclsClient = New eClient.Client
			
			If lclsClient.Find(sClient) Then
				'+ Se valida que el cliente este vivo
				If lclsClient.dDeathdat <> eRemoteDB.Constants.dtmNull Then
					Call lclsErrors.ErrorMessage(pstrCodispl, 2051)
				End If
				'+Se valida que el cliente no esté bloqueado
				If lclsClient.sBlockade = "1" Then 'Blockade = 1 ---> Bloqueado
					Call lclsErrors.ErrorMessage(pstrCodispl, 2063)
				End If
				
				lclsRoles = New ePolicy.Roleses
				If Not lclsRoles.Find_by_Policy("2", nBranch, nProduct, nPolicy, nCertif, sClient, dEffecdate) Then
					Call lclsErrors.ErrorMessage(pstrCodispl, 4025)
				End If
				lclsRoles = Nothing
			Else
				Call lclsErrors.ErrorMessage(pstrCodispl, 2044)
			End If
			lclsClient = Nothing
		End If
		
		'+Se valida la fecha de ocurrencia
		If dOccurdate = eRemoteDB.Constants.dtmNull Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 4018)
		Else
			lclsClaim = New Claim
			If lblnFindPol Then
				If dOccurdate < lclsPolicy.dStartdate Then
					Call lclsErrors.ErrorMessage(pstrCodispl, 4019)
				End If
			End If
			If dOccurdate > dEffecdate Then
				Call lclsErrors.ErrorMessage(pstrCodispl, 4020)
			End If
			If lclsClaim.FindClaim_per_Policy(nPolicy, nClaim, dOccurdate) Then
				lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.Warning
				Call lclsErrors.ErrorMessage(pstrCodispl, 4021)
			End If
			lclsClaim = Nothing
		End If
		
		'+Se valida el campo causa
		If nCause <= 0 Then
			Call lclsErrors.ErrorMessage(pstrCodispl, 10872)
		End If
		
		InsValSI737_Old = lclsErrors.Confirm
		
InsValSI737_Old_Err: 
		If Err.Number Then
			InsValSI737_Old = InsValSI737_Old & Err.Description
		End If
		On Error GoTo 0
		
		lclsErrors = Nothing
		lclsPolicy = Nothing
		lclsProduct = Nothing
		lcolFinance = Nothing
		lclsFinance = Nothing
	End Function
	
	'% ShowCurrency:
	Public Function ShowCurrency(Byval dEffecdate As Date, Byval nBranch As Integer, Byval nProduct As Integer, Byval nPolicy As Integer, Byval nCertif As Integer) As String
		Dim lclsValues As eFunctions.Tables
		
		lclsValues = New eFunctions.Tables
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			dEffecdate = Today
		End If
		
		With lclsValues
			.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		If lclsValues.reaTable("tabCurren_pol") Then
            ShowCurrency = lclsValues.Fields("nCurrency")
		Else
			ShowCurrency = ""
		End If
		lclsValues = Nothing
		
	End Function
	
	'% CalNumberRelation: Obtiene el numero de la relación de siniestros
	Public Function CalNumberRelation(ByVal nUserCode As Integer) As Double
		Dim lclsGeneral As eGeneral.GeneralFunction
		
		On Error GoTo CalNumberRelation_err
		lclsGeneral = New eGeneral.GeneralFunction
		CalNumberRelation = lclsGeneral.Find_Numerator(65, 0, nUserCode)
		
CalNumberRelation_err: 
		If Err.Number Then
			CalNumberRelation = -1
		End If
		On Error GoTo 0
		lclsGeneral = Nothing
	End Function
	
	'*%Find: Function that makes the search in the table 'Claim_master'.
	'% Find: Función que realiza la busqueda en la tabla 'Claim_master'.
	Public Function FindSI737(ByVal nClaim As Double) As Boolean
		Dim lclsClaim_Master As eRemoteDB.Execute
		
		
		lclsClaim_Master = New eRemoteDB.Execute
		'+ Define all parameters for the stored procedures 'insudb.reaClaim_master'. Generated on 09/07/2002 10:01:08 a.m.
		With lclsClaim_Master
			.StoredProcedure = "reaClaim_master_a"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				Me.nBordereaux_cl = .FieldToClass("nBordereaux_cl")
				Me.nClaim_caus = .FieldToClass("NCAUSECOD")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.sCredit = .FieldToClass("sCreditnum")
				Me.sAccount = .FieldToClass("sAccnum")
				Me.nBene_type = .FieldToClass("nBene_type")
				Me.sClient = .FieldToClass("sClient")
				Me.dOccurdate = .FieldToClass("dOccurDat")
				Me.nTotalLoss = .FieldToClass("sClaimtyp")
				Me.nAmount = .FieldToClass("nDamages")
				Me.nClaim = .FieldToClass("nClaim")
				Me.nStatClaim = .FieldToClass("sStaClaim")
				Me.nDeman_type = .FieldToClass("nDeman_type")
				Me.sClientAseg = .FieldToClass("sClientAseg")
				Me.dBirthdate = .FieldToClass("dBirthdate")
				Me.nCover = .FieldToClass("nCover")
				
				FindSI737 = True
				.RCloseRec()
				
			Else
				FindSI737 = False
			End If
		End With
		lclsClaim_Master = Nothing
	End Function
	'%insFinishClaim(). Realiza el manejo de la SI050 con los datos indicados, actualizando el siniestro
	'%con el estado correspondiente y verificando los limites de suscripcion.
	Public Function insFinishClaim(Byval lintWait_code As Integer, Byval nBranch As Integer, Byval nProduct As Integer, Byval nPolicy As Integer, Byval nClaim As Double, Byval sSche_code As Object, Byval nUserCode As Integer) As Boolean
		'- Objeto para el manejo de security
		Dim mobjClaim As Claim
		
		'- Objeto para el manejo de las ventanas de siniestro
		Dim mobjClaimWin As Object
		
		Dim lintListIndex As Integer
		Dim lblnAutomatic As Boolean
		Dim lblnOk As Boolean
		Dim lstrStaClaim As String
		Dim lintcboWaitcode As Integer
		Dim lblnEnabledcboWaitCode As Boolean
		
		mobjClaimWin = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Claim_win")
		mobjClaim = New Claim
		
		lblnOk = True
		insFinishClaim = True
		
		'- (2) Estado del siniestro en tramitación [por defecto]
		lstrStaClaim = "2"
		lintListIndex = lintWait_code
		
		'+ Si los valores son mayores que 5 corresponden a estados manuales, en caso contrario a estados automáticos.
		If lintWait_code > 5 Then
			lblnAutomatic = False
		Else
			lblnAutomatic = True
		End If
		
		'+Si existe alguna carpeta que no halla sido carga con información.
		
		If Not mobjClaimWin.insValSequence(Claim_win.eClaimTransac.clngClaimIssue, nClaim, "2", nBranch, nProduct, nPolicy, False) Then
			
			'+Si quedó alguna carpeta sin llenar, se procede a hacer el manejo automático de la ventana PopUp
			
			lblnAutomatic = True
			
			lintcboWaitcode = 1
			
			If lblnAutomatic Then
				lblnOk = False
				lblnEnabledcboWaitCode = True
				lintcboWaitcode = 1
			End If
		Else
			If lblnAutomatic Then
				If mobjClaim.ValLimitsClaimDec(sSche_code) Then
					lblnOk = False
				End If
			Else
				lblnOk = False
			End If
			If Not (lblnOk) Then
				
				'+ Se cambia el estado del combo al valor 6: "Límite de declaración".
				lintcboWaitcode = 6
				
				'+ Estado del siniestro en pendiente de aprobación.
				lstrStaClaim = "8"
			End If
		End If
		
		'+ Si corresponde a un estado automático.
		If lblnAutomatic Then
			'+ Si el proceso terminó satisfactoriamente.
			If lblnOk Then
				lintcboWaitcode = 0
				lblnEnabledcboWaitCode = True
			Else
				lblnEnabledcboWaitCode = True
			End If
		Else
			lblnEnabledcboWaitCode = False
		End If
		
		Call mobjClaim.insExecuteSI050(Claim_win.eClaimTransac.clngClaimIssue, nClaim, lintcboWaitcode, lblnEnabledcboWaitCode, eRemoteDB.Constants.intNull, nUserCode)
		
		mobjClaimWin = Nothing
		mobjClaim = Nothing
		
	End Function
End Class






