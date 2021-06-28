Option Strict Off
Option Explicit On
Public Class Premium_mo
	'%-------------------------------------------------------%'
	'% $Workfile:: Premium_mo.cls                           $%'
	'% $Author:: Nvaplat11                                  $%'
	'% $Date:: 11/03/04 5:09p                               $%'
	'% $Revision:: 72                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the system table on 11/06/2000.
	'**-The key fields are sCertype, nBranch, nProduct, nReceipt, nDigit, nPaynumbe, nTransac
	'-Propiedades según la tabla en el sistema el 06/11/2000.
	'-Los campos llaves corresponden a sCertype, nBranch, nProduct, nReceipt, nDigit, nPaynumbe, nTransac
	
	'   Column name                Type                 Computed Length Prec Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'   -------------------------  -------------------- -------- ------ ---- ----- -------- ------------------ ---------------------
	Public nReceipt As Double 'int      no       4           10    0     no                                  (n/a)                               (n/a)
	Public sCertype As String 'char     no       1                       no                                  no                                  no
	Public nBranch As Integer 'smallint no       2           5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer 'smallint no       2           5     0     no                                  (n/a)                               (n/a)
	Public nDigit As Integer 'smallint no       2           5     0     no                                  (n/a)                               (n/a)
	Public nPaynumbe As Integer 'smallint no       2           5     0     no                                  (n/a)                               (n/a)
	Public nTransac As Integer 'smallint no       2           5     0     no                                  (n/a)                               (n/a)
	Public nAmount As Double 'decimal  no       9           10    2     yes                                 (n/a)                               (n/a)
	Public nCard_type As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public sAux_accoun As String 'char     no       20                      yes                                 no                                  yes
	Public nBalance As Double 'decimal  no       9           10    2     yes                                 (n/a)                               (n/a)
	Public nBank_code As Double 'int      no       4           10    0     yes                                 (n/a)                               (n/a)
	Public nBordereaux As Double 'int      no       4           10    0     yes                                 (n/a)                               (n/a)
	Public dCar_datexp As Date 'datetime no       8                       yes                                 (n/a)                               (n/a)
	Public sCard_num As String 'char     no       20                      yes                                 no                                  yes
	Public nCash_mov As Integer 'int      no       4           10    0     yes                                 (n/a)                               (n/a)
	Public nCause_amen As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public sCessicoi As String 'char     no       1                       yes                                 no                                  yes
	Public sChang_acc As String 'char     no       20                      yes                                 no                                  yes
	Public dCompdate As Date 'datetime no       8                       yes                                 (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public sDocnumbe As String 'char     no       10                      yes                                 no                                  yes
	Public sInd_rever As String 'char     no       1                       yes                                 no                                  yes
	Public nInt_mora As Double 'decimal  no       9           10    2     yes                                 (n/a)                               (n/a)
	Public sIntermei As String 'char     no       1                       yes                                 no                                  yes
	Public nNullcode As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public sPay_form As String 'char     no       2                       yes                                 no                                  yes
	Public dPosted As Date 'datetime no       8                       yes                                 (n/a)                               (n/a)
	Public nPremium As Double 'decimal  no       9           10    2     yes                                 (n/a)                               (n/a)
	Public nReceipt_fa As Integer 'int      no       4           10    0     yes                                 (n/a)                               (n/a)
	Public dStatdate As Date 'datetime no       8                       yes                                 (n/a)                               (n/a)
	Public sStatisi As String 'char     no       1                       yes                                 no                                  yes
	Public nUsercode As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public dLedgerdat As Date 'datetime no       8                       yes                                 (n/a)                               (n/a)
	Public nType As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public nExchange As Double 'decimal  no       9           10    6     yes                                 (n/a)                               (n/a)
	Public sIndAssocPro As String 'char     no       1                       yes                                 no                                  yes
	Public nPaysoondisc As Double 'decimal  no       9           10    2     yes                                 (n/a)                               (n/a)
	Public nCashnum As Integer 'smallint no       2           5     0     yes                                 (n/a)                               (n/a)
	Public nBulletins As Double 'int      no       4           10    0     yes                                 (n/a)                               (n/a)
	Public nBillnum As Double 'int      no       10                0     yes
	Public sBillType As String 'char     no       1                       yes
	Public nCollector As Double 'int      no       4           10    0     yes                                 (n/a)                               (n/a)
	Public sIndcheque As String 'char     no       1                       yes
	
	'**-Auxiliary variables (CO001)
	'- Variables auxiliares (CO001)
	Public sSel As String
	Public sDescript As String
	Public nLocalAmount As Double
	Public sClient As String
	Public sClienname As String
	Public nPolicy As Double
	Public sNumForm As String
	Public nProponum As Double
	Public sMessage As String
	Public sMessage5137 As String
	Public nSequence As Integer
	
	'**-Auxiliary variables (CO011)
	'- Variables auxiliares (CO011)
	Public nDocNumbe As Integer
	Public sTyperec As String
	Public nCase_num As Integer
	Public nDeman_Type As Integer
	Public nTratypei As Integer
	Public nAmountD As Double
	Public nTypDocu As Integer
	
	'**-Auxiliary variables (CO009)
	'- Variables auxiliares (CO009)
	Public nCollecDocTyp As Integer
	Public nCollecDocTypDoc As Integer
	Public nCertif As Double
	Public nContrat As Double
	Public nDraft As Integer
	Public sDirdebit As String
	Public nTyp_crecard As Integer
	Public nIndex As Integer
    Public sString As String
    Public sBrancht As String
	
	'**-Auxiliary variables (COC001)
	'- Variables auxiliares (COC001)
	Public nWay_Pay As Integer
	Public sWay_Pay As String
	Public sCollector As String
	Public nOffice As Integer
	Public sOfficeIns As String
	Public sOrigReceipt As String
	
	Public sKey As String
    Public nModulec_aux As Integer
    Public nMov_Type_aux As Integer
    Public nIntermedia_aux As Integer

	'%Find_LastnBordereaux: Obtiene el boldereaux (relación) más reciente asociada al documento pasado como parámetro de un documento.
	Public Function Find_LastnBordereaux(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nBulletins As Double, ByVal nProponum As Double, ByVal nIdmov As Integer) As Boolean
		Dim lrecPremium_mo As eRemoteDB.Execute
		
		On Error GoTo Find_LastnBordereaux_Err
		
		lrecPremium_mo = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure reaVdocument_lastpay_d al 03-26-2002 15:23:38
		'+
		With lrecPremium_mo
			.StoredProcedure = "reaVdocument_lastpay_d"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIdmov", nIdmov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				Me.sCertype = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nReceipt = .FieldToClass("nReceipt")
				Me.nContrat = .FieldToClass("nContrat")
				Me.nDraft = .FieldToClass("nDraft")
				Me.nBordereaux = .FieldToClass("nBordereaux")
				Me.nCollecDocTyp = .FieldToClass("nCollecDocTyp")
				Me.nCollecDocTypDoc = .FieldToClass("nCollecDocTypDoc")
				Find_LastnBordereaux = True
			End If
			
		End With
		
Find_LastnBordereaux_Err: 
		If Err.Number Then
			Find_LastnBordereaux = False
		End If
		'UPGRADE_NOTE: Object lrecPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecPremium_mo = Nothing
		On Error GoTo 0
	End Function

    '%Find_Nmodulec: Busca el modulo de las pólizas
    Public Function Find_Nmodulec(ByVal nBranchaux As Integer, ByVal nProductaux As Integer, ByVal nPolicyaux As Double, ByVal nCertifaux As Integer)
        Dim lrecPremium_mo As eRemoteDB.Execute

        On Error GoTo Find_Nmodulec_Err
        lrecPremium_mo = New eRemoteDB.Execute

        With lrecPremium_mo
            .StoredProcedure = "REAMODULEC"
            .Parameters.Add("NBRANCH", nBranchaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", nProductaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", nPolicyaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NCERTIF", nCertifaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NMODULECAUX", nModulec_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SBRANCHT", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            nModulec_aux = .Parameters("NMODULECAUX").Value
            sBrancht = .Parameters("SBRANCHT").Value
            Find_Nmodulec = True
        End With

Find_Nmodulec_Err:
        If Err.Number Then
            Find_Nmodulec = False
        End If
        'UPGRADE_NOTE: Object lrecPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPremium_mo = Nothing
        On Error GoTo 0
    End Function

    '%Find_Nmodulec: Busca el tipo de relación de pago
    Public Function Find_nType_Mov(ByVal nBordereaux_aux As Integer)
        Dim lrecPremium_mo As eRemoteDB.Execute

        On Error GoTo Find_nType_Mov_Err
        lrecPremium_mo = New eRemoteDB.Execute

        With lrecPremium_mo
            .StoredProcedure = "REATYPE_MOV"
            .Parameters.Add("NBORDEREAUX_AUX", nBordereaux_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NTYPE_MOV_AUX", nMov_Type_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            nMov_Type_aux = .Parameters("NTYPE_MOV_AUX").Value
            Find_nType_Mov = True
        End With

Find_nType_Mov_Err:
        If Err.Number Then
            Find_nType_Mov = False
        End If
        'UPGRADE_NOTE: Object lrecPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPremium_mo = Nothing
        On Error GoTo 0
    End Function

    '%Find_nIntermedia: Busca la existencia de intermediario
    Public Function Find_nIntermedia(ByVal nBranch_aux As Integer, ByVal nProduct_aux As Integer, ByVal nPolicy_aux As Integer, ByVal nReceipt_aux As Integer) As Integer
        Dim lrecPremium_mo As eRemoteDB.Execute

        On Error GoTo Find_nIntermedia_Err
        lrecPremium_mo = New eRemoteDB.Execute

        With lrecPremium_mo
            .StoredProcedure = "REANINTERMEDIA"
            .Parameters.Add("NBRANCH", nBranch_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPRODUCT", nProduct_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NPOLICY", nPolicy_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NRECEIPT", nReceipt_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NINTERMED", nIntermedia_aux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            nIntermedia_aux = .Parameters("NINTERMED").Value
            Find_nIntermedia = nIntermedia_aux
        End With

Find_nIntermedia_Err:
        If Err.Number Then
            Find_nIntermedia = 0
        End If
        'UPGRADE_NOTE: Object lrecPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPremium_mo = Nothing
        On Error GoTo 0
    End Function

    '%Find_dposted: lee la fecha de ingreso a la contabilidad de un recibo
    Public Function Find_dPosted(ByVal llngReceipt As Double) As Boolean
        Dim lrecreaPremium_mo As eRemoteDB.Execute

        lrecreaPremium_mo = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.reaPremium_mo'
        '**+Data of 11/06/2000 05:36:45 p.m.
        '+Definición de parámetros para stored procedure 'insudb.reaPremium_mo'
        '+Información leída el 06/11/2000 05:36:45 p.m.

        With lrecreaPremium_mo
            .StoredProcedure = "reaPremium_mo"
            .Parameters.Add("nReceipt", llngReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Find_dPosted = True
                nReceipt = llngReceipt
                dPosted = .FieldToClass("dPosted")
                .RCloseRec()
            Else
                Find_dPosted = False
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPremium_mo = Nothing

Find_dPosted_Err:
        If Err.Number Then
            Find_dPosted = False
        End If
    End Function

    '**%insCrePremium_mo: This method adds the data in process for a premium invoice from the history table
    '%insCrePremium_mo: Esta función se encarga de agregar la información en tratamiento de la tabla de historia de un recibo.
    Public Function insCrePremium_mo(ByVal nType As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nReceipt As Double, Optional ByVal nTyp_crecard As Integer = 0) As Boolean

        Dim lreccrePremium_mo As eRemoteDB.Execute

        lreccrePremium_mo = New eRemoteDB.Execute

        insCrePremium_mo = True

        On Error GoTo insCrePremium_mo_Err

        '**+Stored procedure parameters definition 'insudb.crePremium_mo'
        '**+Data of 02/20/2001 02:48:29 p.m.
        '+Definición de parámetros para stored procedure 'insudb.crePremium_mo'
        '+Información leída el 02/20/2001 02:48:29 p.m.

        With lreccrePremium_mo
            .StoredProcedure = "crePremium_mo"
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nTransac", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nCard_type", IIf(sDirdebit = "2", nTyp_crecard, System.DBNull.Value), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sAux_accoun", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nBank_code", IIf(sDirdebit = "1", nBank_code, System.DBNull.Value), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nBordereaux", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dCar_datexp", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sCard_num", IIf(sDirdebit = "2", sCard_num, System.DBNull.Value), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCash_mov", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCause_amen", nCause_amen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCessicoi", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sChang_acc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sDocnumbe", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sInd_rever", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInt_mora", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIntermei", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nNullcode", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("sPay_form", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPosted", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nReceipt_fa", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatisi", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExchange", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nPaySoonDisc", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndasSocPro", sIndAssocPro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBulletins", nBulletins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCashNum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBillNum", nBillnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBillType", sBillType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndCheque", sIndcheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insCrePremium_mo = .Run(False)

        End With

insCrePremium_mo_Err:
        If Err.Number Then
            insCrePremium_mo = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lreccrePremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccrePremium_mo = Nothing
    End Function

    '%insReaLastMovPremium_mo: Esta funcion realiza la búsqueda de la máxima fecha del último movimiento del recibo.
    Public Function insReaLastMovPremium_mo(ByVal nReceipt As Double, ByVal sCertype As String, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Date
        Dim lrecreaPremium_moLastMov As eRemoteDB.Execute

        lrecreaPremium_moLastMov = New eRemoteDB.Execute

        On Error GoTo insReaLastMovPremium_mo_Err
        '+Definición de parámetros para stored procedure 'insudb.reaPremium_moLastMov'
        '+Información leída el 21/02/2001 01:26:29 p.m.

        With lrecreaPremium_moLastMov
            .StoredProcedure = "reaPremium_moLastMov"
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insReaLastMovPremium_mo = .FieldToClass("ldtmStatdate")
            End If
            '        If Not .FieldToClass("ldtmStatdate") = dtmNull Then
            '            insReaLastMovPremium_mo = .FieldToClass("ldtmStatdate")
            '        End If

        End With

insReaLastMovPremium_mo_Err:
        If Err.Number Then
            insReaLastMovPremium_mo = eRemoteDB.Constants.dtmNull
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaPremium_moLastMov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPremium_moLastMov = Nothing
    End Function

    '%insReaLastMovDraft_hist: Esta funcion realiza la búsqueda de la máxima fecha del último movimiento del recibo.
    Public Function insReaLastMovDraft_hist(ByVal nContrat As Double, ByVal nDraft As Integer) As Date
        Dim lrecreaDraft_histLastMov As eRemoteDB.Execute

        lrecreaDraft_histLastMov = New eRemoteDB.Execute

        On Error GoTo insReaLastMovDraft_hist_Err

        '+Definición de parámetros para stored procedure 'insudb.reaPremium_moLastMov'
        '+Información leída el 21/02/2001 01:26:29 p.m.

        With lrecreaDraft_histLastMov
            .StoredProcedure = "reaDraft_histLastMov"
            .Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insReaLastMovDraft_hist = .FieldToClass("ldtmStatdate")
            End If
        End With

insReaLastMovDraft_hist_Err:
        If Err.Number Then
            insReaLastMovDraft_hist = eRemoteDB.Constants.dtmNull
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaDraft_histLastMov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDraft_histLastMov = Nothing
    End Function

    '%valExistBordereaux: Valida si existe el número de relación dentro de los registros de movimientos del recibo
    Public Function valExistBordereaux(ByVal nBordereaux As Double) As Boolean
        Dim lrecreaPremium_moBordereaux As eRemoteDB.Execute

        On Error GoTo valExistBordereaux_Err
        lrecreaPremium_moBordereaux = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.reaPremium_moBordereaux'
        '**+Data of 02/21/2001 01:26:29 p.m.
        '+Definición de parámetros para stored procedure 'insudb.reaPremium_moBordereaux'
        '+Información leída el 21/02/2001 01:26:29 p.m.

        With lrecreaPremium_moBordereaux
            .StoredProcedure = "reaPremium_moBordereaux"
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nReceipt = .FieldToClass("nReceipt")
                nCurrency = .FieldToClass("nCurrency")
                valExistBordereaux = True
                .RCloseRec()
            Else
                valExistBordereaux = False
            End If
        End With

valExistBordereaux_Err:
        If Err.Number Then
            valExistBordereaux = False
        End If
        'UPGRADE_NOTE: Object lrecreaPremium_moBordereaux may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPremium_moBordereaux = Nothing
        On Error GoTo 0
    End Function

    '**%ReverseReceptableToReturn: This method changes the status of the receipt.
    '**%(collected/refunded changes to pending respectively). It also updates the balance of
    '**%receipt and updates the bank account depending if the direct debit operation was performed from
    '**%the checking account or from a credit card.
    '%ReverseReceptableToReturn: Permite cambiar el estado del recibo. De cobrado
    '%o devuelto pasa a pendiente de cobro o devolución respectivamente. También actualiza el balance
    '%pendiente del recibo, y por último afecta la cuenta bancaria, dependiendo si el pago del recibo
    '%fue por domiciliación bancaria o por cargo automático a tarjeta de crédito.
    Public Function ReverseReceptableToReturn() As Boolean
        Dim lrecinsReverseCO009 As eRemoteDB.Execute

        On Error GoTo ReverseReceptableToReturn_Err

        lrecinsReverseCO009 = New eRemoteDB.Execute

        '**+Stored procedure parameters definition 'insudb.insReverseCO009'
        '**+Data of 03/03/2001 16:37:47
        '+Definición de parámetros para stored procedure 'insudb.insReverseCO009'
        '+Información leída el 03/03/2001 16:37:47

        With lrecinsReverseCO009
            .StoredProcedure = "insReverseCO009"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeR", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDirdebit", sDirdebit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCard_type", nCard_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCard_num", sCard_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAux_accoun", sAux_accoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBalance", nBalance, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank_code", nBank_code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCar_datexp", dCar_datexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCash_mov", nCash_mov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCause_amen", nCause_amen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCessicoi", sCessicoi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChang_acc", sChang_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDocnumbe", sDocnumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInd_rever", sInd_rever, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInt_mora", nInt_mora, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIntermei", sIntermei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNULLcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPay_form", sPay_form, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dPosted", dPosted, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt_fa", nReceipt_fa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate", dStatdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatisi", sStatisi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLedgerdat", dLedgerdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType", IIf(nType = Premium.Collec_Devolu.clngReceptable, 4, 5), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExchange", nExchange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            ReverseReceptableToReturn = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecinsReverseCO009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReverseCO009 = Nothing

ReverseReceptableToReturn_Err:
        If Err.Number Then
            ReverseReceptableToReturn = False
        End If
        On Error GoTo 0
    End Function

    '%insValCOL636: Permite validar los datos introducidos en la zona de detalle para
    '%forma.
    Public Function insValCOL636(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dProcessDate As Date, ByVal nCollectorType As Integer) As String

        '-Se define la variable lblnError utilizada para saber si existe o no un error.
        Dim lblnError As Boolean

        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        lblnError = True

        On Error GoTo insValCOL636_err

        '+Se valida el area de seguros.
        If nInsur_area = eRemoteDB.Constants.intNull Or nInsur_area = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 55031)
            lblnError = False
        End If


        '+Se valida el tipo de cobrador.
        If nCollectorType = eRemoteDB.Constants.intNull Or nCollectorType = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 55571)
            lblnError = False
        End If

        insValCOL636 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValCOL636_err:
        If Err.Number Then
            insValCOL636 = "insValCOL636: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '%insPostCOL636: Permite efectuar el pago de comisiones para los cobradores
    Public Function insPostCOL636(ByVal nInsur_area As Integer, ByVal dProcessDate As Date, ByVal nCollectorType As Integer, ByVal nUsercode As Integer) As String

        Dim lProcessCOL636 As New eRemoteDB.Execute

        On Error GoTo insPostCOL636_Err

        'Definición de parámetros para stored procedure 'insudb.insProcess_COL636'
        With lProcessCOL636
            .StoredProcedure = "insProcess_COL636"
            .Parameters.Add("InsurArea", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Processdate", dProcessDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Collectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Usercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCOL636 = CStr(.Run(False))
        End With

        'UPGRADE_NOTE: Object lProcessCOL636 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lProcessCOL636 = Nothing

insPostCOL636_Err:
        If Err.Number Then
            insPostCOL636 = CStr(False)
        End If

    End Function

    '%insValCOL686: Permite validar los datos introducidos en la zona de detalle para
    '%forma.
    Public Function insValCOL686(ByVal sCodispl As String, ByVal nInsur_area As Integer, ByVal dInitDate As Date, ByVal dFinaldate As Date, ByVal nCollectorType As Integer) As String

        '-Se define la variable lblnError utilizada para saber si existe o no un error.
        Dim lblnError As Boolean

        Dim lobjErrors As eFunctions.Errors

        lobjErrors = New eFunctions.Errors

        lblnError = True

        On Error GoTo insValCOL686_err

        '+Se valida el area de seguros.
        If nInsur_area = eRemoteDB.Constants.intNull Or nInsur_area = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 55031)
            lblnError = False
        End If

        '+Se efectua las validaciones concernientes a las fechas de inicio y final.
        If dInitDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 9071)
            lblnError = False
            If dFinaldate = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 9072)
            End If
        Else
            If dFinaldate = eRemoteDB.Constants.dtmNull Then
                Call lobjErrors.ErrorMessage(sCodispl, 9072)
                lblnError = False
            Else
                If dFinaldate <= dInitDate Then
                    Call lobjErrors.ErrorMessage(sCodispl, 60113)
                    lblnError = False
                End If
            End If
        End If

        '+Se valida el tipo de cobrador.
        If nCollectorType = eRemoteDB.Constants.intNull Or nCollectorType = 0 Then
            Call lobjErrors.ErrorMessage(sCodispl, 55571)
            lblnError = False
        Else
            If nCollectorType = 1 Then
                Call lobjErrors.ErrorMessage(sCodispl, 60277)
                lblnError = False
            End If
        End If

        insValCOL686 = lobjErrors.Confirm

        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValCOL686_err:
        If Err.Number Then
            insValCOL686 = "insValCOL686: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '%insPostCOL686: Permite efectuar la preparación de ctas. ctes. para los cobradores
    Public Function insPostCOL686(ByVal nInsur_area As Integer, ByVal dInitDate As Date, ByVal dFinaldate As Date, ByVal nCollectorType As Integer, ByVal nUsercode As Integer, ByVal nExecute As Short) As String
        Dim lProcessCOL686 As New eRemoteDB.Execute

        On Error GoTo insPostCOL686_Err

        'Definición de parámetros para stored procedure 'insudb.insProcess_COL686'
        With lProcessCOL686
            .StoredProcedure = "insProcess_COL686"
            .Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dInitDate", dInitDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dFinaldate", dFinaldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Collectortype", nCollectorType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExecute", nExecute, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCOL686 = CStr(.Run(False))

            If CBool(insPostCOL686) Then
                Me.sKey = .Parameters("sKey").Value
            End If

        End With

        'UPGRADE_NOTE: Object lProcessCOL686 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lProcessCOL686 = Nothing

insPostCOL686_Err:
        If Err.Number Then
            insPostCOL686 = CStr(False)
        End If

    End Function

    '%ShowDefValuesCo09: Permite buscar todos los datos informativos de la página CO09 (reverso de cobro).
    '+La variable de entrada representa el campo nReceiptNum y el nBoardereaux ambos NUMBER(10)
    Public Function ShowDefValuesCo09(ByVal ldblBordereaux As Double) As String
        Dim lclsShowDefValuesCo09 As New eRemoteDB.Execute

        On Error GoTo lclsShowDefValuesCo09_Err

        'Definición de parámetros para stored procedure 'insudb.insProcess_COL686'
        With lclsShowDefValuesCo09
            .StoredProcedure = "TABAGREEMENT_CO09"
            .Parameters.Add("nBordereaux", ldblBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                ShowDefValuesCo09 = .FieldToClass("NCOD_AGREE")
            End If

        End With

lclsShowDefValuesCo09_Err:
        If Err.Number Then
            ShowDefValuesCo09 = Err.Description
        End If

        'UPGRADE_NOTE: Object lclsShowDefValuesCo09 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsShowDefValuesCo09 = Nothing
        On Error GoTo 0
    End Function


    '%insValCO009: Permite validar los datos introducidos en la zona de detalle para la forma.
    Public Function insValCO009(ByVal sCodispl As String, ByVal dOperdate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDraft As Integer, ByVal sReceiptNum As String, ByVal nBordereaux As Double, ByVal sRevAll As String, ByVal sTypOper As String) As String

        Dim lrecInsValCO009 As eRemoteDB.Execute
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty

        On Error GoTo insValCO009_err

        lrecInsValCO009 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción
        With lrecInsValCO009
            .StoredProcedure = "insCO09PKG.InsValCO09"
            .Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReceiptNum", sReceiptNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRevall", sRevAll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypOper", sTypOper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If

            If sTypOper = eRemoteDB.strNull Then
                Call .ErrorMessage(sCodispl, 1088, , eFunctions.Errors.TextAlign.RigthAling, " de ingreso de caja")
            End If
            insValCO009 = .Confirm
        End With

insValCO009_err:
        If Err.Number Then
            insValCO009 = "insValCO009_err: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecInsValCO009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValCO009 = Nothing
    End Function

    '%insPostCO009: Se encarga de realizar el reverso de la información ingresada en la transacción CO009.
    Public Function insPostCO009(ByVal dOperdate As Date, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal nContrat As Double, ByVal nDraft As Integer, ByVal nBordereaux As Double, ByVal sIndrevall As String, ByVal nUsercode As Integer, ByVal nOptTypOper As Short, ByVal dDateIncrease As Date, ByVal sClient As String) As Boolean
        Dim lrecinsUpdCO009 As New eRemoteDB.Execute

        On Error GoTo insPostCO009_Err

        lrecinsUpdCO009 = New eRemoteDB.Execute

        With lrecinsUpdCO009
            .StoredProcedure = "insUpdCO009"
            .Parameters.Add("dOperdate", dOperdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndrevall", IIf(sIndrevall = "1", 1, 0), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOptTypOper", nOptTypOper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nAction", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDateIncrease", dDateIncrease, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCO009 = .Run(False)
        End With

insPostCO009_Err:
        If Err.Number Then
            insPostCO009 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsUpdCO009 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdCO009 = Nothing
    End Function

    '%insValCOC001_k: Esta función se encarga de validar los datos introducidos en la cabecera de la forma.
    Public Function insValCOC001_k(ByVal sCodispl As String, ByVal dStatdate As Date, ByVal dEndDate As Date, ByVal nUser_CashNum As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsUser_CashNum As Object

        On Error GoTo insValCOC001_k_Err

        lclsErrors = New eFunctions.Errors

        '+Validacion de la fecha inicial.
        If dStatdate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9071)
        End If

        '+Validacion de la fecha final.
        If dEndDate = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 9072)
        End If

        If dEndDate <> eRemoteDB.Constants.dtmNull And dStatdate <> eRemoteDB.Constants.dtmNull Then
            If dEndDate < dStatdate Then
                Call lclsErrors.ErrorMessage(sCodispl, 3240)
            End If
        End If

        '+Validacion de la existencia de la caja.
        If nUser_CashNum <> eRemoteDB.Constants.intNull Then
            lclsUser_CashNum = eRemoteDB.NetHelper.CreateClassInstance("eCashBank.User_cashnum")
            If Not lclsUser_CashNum.Find(nUser_CashNum) Then
                Call lclsErrors.ErrorMessage(sCodispl, 55886)
            End If
            'UPGRADE_NOTE: Object lclsUser_CashNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsUser_CashNum = Nothing
        End If

        insValCOC001_k = lclsErrors.Confirm

insValCOC001_k_Err:
        If Err.Number Then
            insValCOC001_k = insValCOC001_k & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        nReceipt = eRemoteDB.Constants.intNull
        sCertype = CStr(eRemoteDB.Constants.strNull)
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nDigit = eRemoteDB.Constants.intNull
        nPaynumbe = eRemoteDB.Constants.intNull
        nTransac = eRemoteDB.Constants.intNull
        nAmount = eRemoteDB.Constants.intNull
        nCard_type = eRemoteDB.Constants.intNull
        sAux_accoun = CStr(eRemoteDB.Constants.strNull)
        nBalance = eRemoteDB.Constants.intNull
        nBank_code = eRemoteDB.Constants.intNull
        nBordereaux = eRemoteDB.Constants.intNull
        dCar_datexp = eRemoteDB.Constants.dtmNull
        sCard_num = CStr(eRemoteDB.Constants.strNull)
        nCash_mov = eRemoteDB.Constants.intNull
        nCause_amen = eRemoteDB.Constants.intNull
        sCessicoi = CStr(eRemoteDB.Constants.strNull)
        sChang_acc = CStr(eRemoteDB.Constants.strNull)
        dCompdate = eRemoteDB.Constants.dtmNull
        nCurrency = eRemoteDB.Constants.intNull
        sDocnumbe = CStr(eRemoteDB.Constants.strNull)
        sInd_rever = CStr(eRemoteDB.Constants.strNull)
        nInt_mora = eRemoteDB.Constants.intNull
        sIntermei = CStr(eRemoteDB.Constants.strNull)
        nNullcode = eRemoteDB.Constants.intNull
        sPay_form = CStr(eRemoteDB.Constants.strNull)
        dPosted = eRemoteDB.Constants.dtmNull
        nPremium = eRemoteDB.Constants.intNull
        nReceipt_fa = eRemoteDB.Constants.intNull
        dStatdate = eRemoteDB.Constants.dtmNull
        sStatisi = CStr(eRemoteDB.Constants.strNull)
        nUsercode = eRemoteDB.Constants.intNull
        dLedgerdat = eRemoteDB.Constants.dtmNull
        nType = eRemoteDB.Constants.intNull
        nExchange = eRemoteDB.Constants.intNull
        sIndAssocPro = CStr(eRemoteDB.Constants.strNull)
        nPaysoondisc = eRemoteDB.Constants.intNull
        nCashnum = eRemoteDB.Constants.intNull
        nBulletins = eRemoteDB.Constants.intNull
        nBillnum = eRemoteDB.Constants.intNull
        sBillType = CStr(eRemoteDB.Constants.strNull)
        nCollector = eRemoteDB.Constants.intNull
        sIndcheque = CStr(eRemoteDB.Constants.strNull)

        sSel = CStr(eRemoteDB.Constants.strNull)
        sDescript = CStr(eRemoteDB.Constants.strNull)
        nLocalAmount = eRemoteDB.Constants.intNull
        sClient = CStr(eRemoteDB.Constants.strNull)
        sClienname = CStr(eRemoteDB.Constants.strNull)
        nPolicy = eRemoteDB.Constants.intNull
        sNumForm = CStr(eRemoteDB.Constants.strNull)
        sMessage = CStr(eRemoteDB.Constants.strNull)
        sMessage5137 = CStr(eRemoteDB.Constants.strNull)
        nSequence = eRemoteDB.Constants.intNull

        nDocNumbe = eRemoteDB.Constants.intNull
        sTyperec = CStr(eRemoteDB.Constants.strNull)
        nCase_num = eRemoteDB.Constants.intNull
        nDeman_Type = eRemoteDB.Constants.intNull
        nTratypei = eRemoteDB.Constants.intNull
        nAmountD = eRemoteDB.Constants.intNull
        nTypDocu = eRemoteDB.Constants.intNull

        nCollecDocTyp = eRemoteDB.Constants.intNull
        nCollecDocTypDoc = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        sDirdebit = CStr(eRemoteDB.Constants.strNull)
        nTyp_crecard = eRemoteDB.Constants.intNull
        nIndex = eRemoteDB.Constants.intNull

        nOffice = eRemoteDB.Constants.intNull
        sOfficeIns = CStr(eRemoteDB.Constants.strNull)
        sOrigReceipt = CStr(eRemoteDB.Constants.strNull)
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%insReaReceipt_LastMovPay: Obtiene el último movimiento de pago de un recibo siempre y cuando no tenga movimientos de reverso.
    '%Devuelve: True -> Monto de cobro y el de interés de mora si no existe previamente movimiento de reverso; False -> Si existe previamente movimiento de reverso en cuyo caso los montos son cero.
    Public Function insReaReceipt_LastMovPay(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer) As Date
        Dim lrecPremium_mo As eRemoteDB.Execute
        Dim ldtmDate As Date

        lrecPremium_mo = New eRemoteDB.Execute

        On Error GoTo insReaReceipt_LastMovPay_Err

        With lrecPremium_mo
            .StoredProcedure = "reaReceipt_LastMovPay"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReady", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRowid", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBordereaux", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInt_mora", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                '+ Si el registro es apto para el tratamiento (No existe previamente un movimiento de cobro)
                If .Parameters("nReady").Value = 1 Then
                    insReaReceipt_LastMovPay = System.DateTime.FromOADate(True)
                    Me.dStatdate = .Parameters("dStatdate").Value
                    Me.nBordereaux = .Parameters("nBordereaux").Value
                    Me.nAmount = .Parameters("nAmount").Value
                    Me.nInt_mora = .Parameters("nInt_mora").Value
                End If
            End If
        End With

insReaReceipt_LastMovPay_Err:
        If Err.Number Then
            insReaReceipt_LastMovPay = eRemoteDB.Constants.dtmNull
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecPremium_mo may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecPremium_mo = Nothing
    End Function

    '%valDocumentAll_LastPay: Verifica si es posible reversar toda la relación pasada conmo parámetro.
    '% True -> Si existen documentos posteriores (no es posible reversar toda la relación); False -> Si no existen documentos posteriores (Si es posible reversar toda la relación)
    Public Function valDocumentAll_LastPay(ByVal nBordereaux As Double) As Boolean
        Dim lrecvalDocumentall_lastpay As eRemoteDB.Execute

        On Error GoTo valDocumentall_lastpay_Err

        lrecvalDocumentall_lastpay = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure valDocumentall_lastpay al 03-26-2002 12:41:22
        '+
        With lrecvalDocumentall_lastpay
            .StoredProcedure = "valDocumentall_lastpay"
            .Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReady", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters("nReady").Value = 1 Then
                    valDocumentAll_LastPay = True
                End If
            End If
        End With

valDocumentall_lastpay_Err:
        If Err.Number Then
            valDocumentAll_LastPay = False
        End If
        'UPGRADE_NOTE: Object lrecvalDocumentall_lastpay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecvalDocumentall_lastpay = Nothing
        On Error GoTo 0
    End Function

    '%valDocBill : Verifica que el documento no ha sido facturado
    Public Function valDocBill(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double) As Boolean
        Dim lrecvalDocBill As eRemoteDB.Execute

        On Error GoTo valDocBill_Err

        lrecvalDocBill = New eRemoteDB.Execute

        With lrecvalDocBill
            .StoredProcedure = "valDocBill"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters("nExists").Value = 1 Then
                    valDocBill = True
                Else
                    valDocBill = False
                End If
            End If
        End With

valDocBill_Err:
        If Err.Number Then
            valDocBill = False
        End If
        'UPGRADE_NOTE: Object lrecvalDocBill may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecvalDocBill = Nothing
        On Error GoTo 0
    End Function
End Class






