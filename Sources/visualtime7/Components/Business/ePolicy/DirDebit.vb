Option Strict Off
Option Explicit On
Public Class DirDebit
	'%-------------------------------------------------------%'
	'% $Workfile:: DirDebit.cls                             $%'
	'% $Author:: Nvaplat15                                  $%'
	'% $Date:: 19/08/04 18.40                               $%'
	'% $Revision:: 51                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Descripciòn de la tabla Dir_Debit a la fecha 02/11/2000
	'+ Los campos llave corresponden a sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate
	
	'+  Column_name              Type                     Computed  Length Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+  ------------------------ --------------------     --------- ------ ----- ----- -------- ------------------ --------------------
	Public sCertype As String 'char       no        1                  no        no                  no
	Public nBranch As Integer 'smallint   no        2      5     0     no       (n/a)               (n/a)
	Public nProduct As Integer 'smallint   no        2      5     0     no       (n/a)               (n/a)
	Public nPolicy As Double 'int        no        4      10    0     no       (n/a)               (n/a)
	Public nCertif As Double 'int        no        4      10    0     no       (n/a)               (n/a)
	Public dEffecdate As Date 'datetime   no        8                  no       (n/a)               (n/a)
	Public sAccount As String 'char       no        25                 yes       no                  yes
	Public nBankext As Integer 'int        no        4      10    0     yes      (n/a)               (n/a)
	Public sClient As String 'char       no        14                 no        no                  no
	Public dNulldate As Date 'datetime   no        8                  yes      (n/a)               (n/a)
	Public sCredi_card As String 'char       no        20                 yes       no                  yes
	Public nTyp_crecard As Integer 'smallint   no        2      5     0     yes      (n/a)               (n/a)
	Public sTyp_dirdeb As String 'char       no        1                  yes       no                  yes
	Public nUsercode As Integer 'smallint   no        2      5     0     yes      (n/a)               (n/a)
	Public dCardExpir As Date 'datetime   no        8                  yes      (n/a)               (n/a)
	Public sBankauth As String 'char                 15     0     0     N
	Public sReuse As String 'char                 1      0     0     N
	Public nTyp_acc As Integer
	
	Public nProcess As Integer
	Public sTransaction As String
	
	'- Tipo enumerado que indica el tipo de características del control
	Public Enum eTypeVarCA003
		blnEnabled = 0
		strValue = 1
		blnVisible = 2
	End Enum
	
	'- Tipo enumerado con los objetos que pertenencen a la forma CA004
	Public Enum eTypeControlsCA003
		tctClient = 0
		optBank1 = 1
		optBank2 = 2
		cbeBankExt = 3
		valAccount = 4
		valAgency = 5
		cbeTyp_crecard = 6
		tctBankAuth = 7
		tcdDateExpir = 8
		cbeTyp_Account = 9
		valCredi_card = 10
	End Enum
	
	Private mblnTrans As Boolean
	Private mblnPac As Boolean
	Private mblnOk As Boolean
	Public mstrDirDebit As String
	Public nRole As Integer
	
	'+ Variables que definen el estado y valor de los objetos
	Private bEnabledtctClient As Boolean
	Private sValuetctClient As String
	
	Private bEnabledoptBank1 As Boolean
	Private sValueoptBank1 As String
	
	Private bEnabledoptBank2 As Boolean
	Private sValueoptBank2 As String
	
	Private bEnabledcbeBankExt As Boolean
	Private sValuecbeBankExt As String
	
	Private bEnabledvalAccount As Boolean
	Private sValuevalAccount As String
	
	Private bEnabledvalAgency As Boolean
	Private sValuevalAgency As String
	
	Private bEnabledcbeTyp_crecard As Boolean
	Private sValuecbeTyp_crecard As String
	
	Private bEnabledtctBankAuth As Boolean
	Private sValuetctBankAuth As String
	
	Private bEnabledtcdDateExpir As Boolean
	Private sValuetcdDateExpir As String
	
	Private bEnabledcbeTyp_Account As Boolean
	Private sValuecbeTyp_Account As String
	
	Private bEnabledvalCredi_card As Boolean
	Private sValuevalCredi_card As String
	
	Public nWay_pay As Integer
	Public sDirind As String
	Public nBill_day As Integer
	Public bDisabledAll As Boolean
	
	'% Find: Lee la tabla Dir_Debit
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaDir_debit As eRemoteDB.Execute
		
		On Error GoTo Find_Err

        lrecreaDir_debit = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaDir_debit'
        '+ Información leída el 02/12/1999 11:05:26 AM
        With lrecreaDir_debit
            .StoredProcedure = "reaDir_debit"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Me.sCertype = .FieldToClass("sCertype")
                Me.nBranch = .FieldToClass("nBranch")
                Me.nProduct = .FieldToClass("nProduct")
                Me.nPolicy = .FieldToClass("nPolicy")
                Me.nCertif = .FieldToClass("nCertif")
                Me.dEffecdate = .FieldToClass("dEffecdate")
                Me.sAccount = .FieldToClass("sAccount")
                Me.nBankext = .FieldToClass("nBankext")
                Me.sClient = .FieldToClass("sClient")
                Me.dNulldate = .FieldToClass("dNulldate")
                Me.sCredi_card = .FieldToClass("sCredi_card")
                Me.nTyp_crecard = .FieldToClass("nTyp_crecard")
                Me.sTyp_dirdeb = .FieldToClass("sTyp_dirdeb")
                Me.dCardExpir = .FieldToClass("dCardExpir")
                Me.sBankauth = .FieldToClass("sBankAuth")
                Me.sReuse = .FieldToClass("sReuse")
                Me.nTyp_acc = .FieldToClass("nTyp_acc")
                Find = True
            End If
        End With

Find_Err:
        If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDir_debit = Nothing
	End Function
	
	'% Add: Agrega los datos correspondientes para los contactos el cliente
	Public Function Add() As Boolean
		Dim lrecinsDir_debit As eRemoteDB.Execute
		
		lrecinsDir_debit = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		'+ Definición de parámetros para stored procedure 'insudb.insDir_debit'
		'+ Información leída el 03/11/2000 11:35:28 a.m.
		With lrecinsDir_debit
			.StoredProcedure = "insDir_debit"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_crecard", nTyp_crecard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTyp_dirdeb", sTyp_dirdeb, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProcess", nProcess, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTransaction", sTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dNulldate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCardExpir", dCardExpir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankAuth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReuse", sReuse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acc", nTyp_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDir_debit = Nothing
	End Function
	
	'% insPreCA003: Esta rutina se encarga de realizar las operaciones que corresponden cuando
	'% se entra en el frame de "Datos para la facturación"
	Public Sub insPreCA003(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nTransaction As Integer)
		Dim lclsCertificat As ePolicy.Certificat
		
		lclsCertificat = New ePolicy.Certificat
		
		Call lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif)
		
		nWay_pay = lclsCertificat.nWay_pay
		sDirind = lclsCertificat.sDirind
		bDisabledAll = False
		If nWay_pay = Constantes.eWayPay.clngPayByPAC Or nWay_pay = Constantes.eWayPay.clngPayByTransBank Then
			nBill_day = lclsCertificat.nBill_day
			Select Case nTransaction
				'+ Consulta de: póliza, certificados, cotización, solicitud
				Case Constantes.PolTransac.clngPolicyQuery, Constantes.PolTransac.clngCertifQuery, Constantes.PolTransac.clngQuotationQuery, Constantes.PolTransac.clngProposalQuery
					Call insInitialCA003(lclsCertificat, nTransaction, dEffecdate)
					
					Call insStateCA003(False, False)
				Case Else
					Call insInitialCA003(lclsCertificat, nTransaction, dEffecdate)
					
					Call insStateCA003(True, True)
			End Select
		Else
			bDisabledAll = True
		End If
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
	End Sub
	
	'% insInitialCA003: Esta rutina se encarga asignar e inicializar los valores de la forma
	Private Sub insInitialCA003(ByRef lobjCertificat As Certificat, ByVal nTransaction As Integer, ByVal dEffecdate As Date)
		Dim lclsDirDebit As ePolicy.DirDebit
		Dim lclsDirDebitCli As eClient.Dir_debit_cli
		Dim lclsRoles As ePolicy.Roles
		Dim lclsBk_account As eClient.bk_account
		
		lclsDirDebit = New ePolicy.DirDebit
		lclsDirDebitCli = New eClient.Dir_debit_cli
		lclsRoles = New ePolicy.Roles
		lclsBk_account = New eClient.bk_account
		
		mblnOk = False
		
		'+ Buscar si existe figura "Pagador" para la Póliza
		If lclsRoles.Find(lobjCertificat.sCertype, lobjCertificat.nBranch, lobjCertificat.nProduct, lobjCertificat.nPolicy, lobjCertificat.nCertif, Roles.eRoles.eRolPayer, String.Empty, dEffecdate) Then
			lclsDirDebit.sClient = lclsRoles.sClient
			sValuetctClient = lclsDirDebit.sClient
		Else
			
			'+ Si no se encuentra figura "Pagador" asignar Contratante
			If lclsRoles.Find(lobjCertificat.sCertype, lobjCertificat.nBranch, lobjCertificat.nProduct, lobjCertificat.nPolicy, lobjCertificat.nCertif, Roles.eRoles.eRolContratanting, String.Empty, dEffecdate) Then
				lclsDirDebit.sClient = lclsRoles.sClient
				sValuetctClient = lclsDirDebit.sClient
			End If
		End If
		
		'+ Si el tipo de domiciliación es por Póliza.
		If lobjCertificat.sDirind = "2" Then
			If lclsDirDebit.Find(lobjCertificat.sCertype, lobjCertificat.nBranch, lobjCertificat.nProduct, lobjCertificat.nPolicy, lobjCertificat.nCertif, dEffecdate) Then
				sValuetctClient = lclsDirDebit.sClient
				
				'+ sTyp_dirdeb: Tipo de domiciliación bancaria 1- Banco, 2- Tarjeta de crédito
				If lclsDirDebit.sTyp_dirdeb = "2" Then
					mstrDirDebit = "2"
					sValueoptBank2 = CStr(System.Windows.Forms.CheckState.Checked)
					
					sValuecbeBankExt = CStr(lclsDirDebit.nBankext)
					sValuevalAccount = String.Empty
					sValuecbeTyp_Account = String.Empty
					sValuetctBankAuth = lclsDirDebit.sBankauth
					
					sValuecbeTyp_crecard = CStr(lclsDirDebit.nTyp_crecard)
					sValuevalCredi_card = lclsDirDebit.sCredi_card
					sValuetcdDateExpir = CStr(lclsDirDebit.dCardExpir)
					
				Else
					mstrDirDebit = "1"
					sValueoptBank1 = CStr(System.Windows.Forms.CheckState.Checked)
					
					sValuecbeBankExt = CStr(lclsDirDebit.nBankext)
					sValuevalAccount = lclsDirDebit.sAccount
					sValuecbeTyp_Account = CStr(lclsDirDebit.nTyp_acc)
					sValuetctBankAuth = lclsDirDebit.sBankauth
					
					sValuecbeTyp_crecard = String.Empty
					sValuevalCredi_card = String.Empty
					sValuetcdDateExpir = String.Empty
				End If
			Else
				sValuetctBankAuth = valExistsBankAutNum(lclsDirDebit.sClient, dEffecdate)
			End If
			If lclsDirDebit.sClient = "" Then
				Call insGetClientCA003(lobjCertificat.sCertype, lobjCertificat.nBranch, lobjCertificat.nProduct, lobjCertificat.nPolicy, lobjCertificat.nCertif, dEffecdate)
			End If
		End If
		
		'+ Si el tipo de Domiciliación es por Cliente.
		If lobjCertificat.sDirind = "1" Then
			
			If lclsDirDebitCli.Find(lclsDirDebit.sClient, eRemoteDB.Constants.dtmNull) Then
				mblnOk = True
				
				sValuetctClient = lclsDirDebitCli.sClient
				sValuecbeBankExt = CStr(lclsDirDebitCli.nBankext)
				
				'+ 1- Banco, 2- Tarjeta de crédito
				If lclsDirDebitCli.sTyp_dirdeb = "2" Then
					mstrDirDebit = "2"
					sValueoptBank2 = CStr(System.Windows.Forms.CheckState.Checked)
					
					sValuevalAccount = String.Empty
					sValuecbeTyp_Account = String.Empty
					sValuetctBankAuth = String.Empty
					
					sValuecbeTyp_crecard = CStr(lclsDirDebitCli.nCard_Type)
					sValuevalCredi_card = lclsDirDebitCli.sCredi_card
					sValuetcdDateExpir = CStr(lclsDirDebitCli.dCardExpir)
				Else
					mstrDirDebit = "1"
					sValueoptBank1 = CStr(System.Windows.Forms.CheckState.Checked)
					
					sValuevalAccount = lclsDirDebitCli.sAccount
					
					If lclsBk_account.Find(lclsDirDebitCli.sClient, lclsDirDebitCli.nBankext, lclsDirDebitCli.sAccount) Then
						sValuecbeTyp_Account = CStr(lclsBk_account.nTyp_acc)
					Else
						sValuecbeTyp_Account = String.Empty
					End If
					sValuetctBankAuth = lclsDirDebitCli.sBankauth
					
					sValuecbeTyp_crecard = String.Empty
					sValuevalCredi_card = String.Empty
					sValuetcdDateExpir = String.Empty
				End If
			End If
			
			If lclsDirDebitCli.sClient = "" Then
				Call insGetClientCA003(lobjCertificat.sCertype, lobjCertificat.nBranch, lobjCertificat.nProduct, lobjCertificat.nPolicy, lobjCertificat.nCertif, dEffecdate)
			End If
		End If
		
		mblnTrans = lobjCertificat.nWay_pay = Constantes.eWayPay.clngPayByTransBank
		mblnPac = lobjCertificat.nWay_pay = Constantes.eWayPay.clngPayByPAC
		If Not mblnTrans And Not mblnPac Then
			mblnOk = True
		End If
		
		'UPGRADE_NOTE: Object lclsDirDebit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDirDebit = Nothing
		'UPGRADE_NOTE: Object lclsDirDebitCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDirDebitCli = Nothing
		'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRoles = Nothing
		'UPGRADE_NOTE: Object lclsBk_account may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsBk_account = Nothing
		
	End Sub
	
	'% insStateCA003: Esta rutina se encarga de habilitan o deshabilitan
    Private Sub insStateCA003(ByVal lblnEnabled As Boolean, ByVal lblnClear As Boolean)
        '+ Domiciliacion es por Cliente
        If mblnOk Then
            bEnabledoptBank1 = True
            bEnabledoptBank2 = True
            bEnabledtctClient = True

            bEnabledcbeBankExt = True
            bEnabledvalAccount = True
            bEnabledcbeTyp_Account = True
            bEnabledtctBankAuth = True

            bEnabledcbeTyp_crecard = True
            bEnabledtcdDateExpir = True
            bEnabledvalCredi_card = True
            bDisabledAll = True
        Else
            '+ Domicilizacion es por Poliza
            bEnabledoptBank1 = Not lblnEnabled
            If Not bEnabledoptBank1 Then
                bEnabledoptBank1 = mblnTrans
            End If

            bEnabledoptBank2 = Not lblnEnabled
            If Not bEnabledoptBank2 Then
                bEnabledoptBank2 = mblnPac
            End If

            bEnabledtctClient = Not lblnEnabled

            '+ Domicilizacion es por Poliza-Banco
            If mblnPac Then
                bEnabledcbeBankExt = Not lblnEnabled

                If sValuecbeBankExt = String.Empty Then
                    bEnabledvalAccount = lblnEnabled
                    bEnabledcbeTyp_Account = lblnEnabled
                Else
                    bEnabledvalAccount = Not lblnEnabled
                    bEnabledcbeTyp_Account = Not lblnEnabled
                    bEnabledtctBankAuth = Not lblnEnabled
                End If

                bEnabledcbeTyp_crecard = True
                bEnabledtcdDateExpir = True
                bEnabledvalCredi_card = True
            End If

            '+ Domicilizacion Es por Poliza-Tarjeta
            If mblnTrans Then
                bEnabledcbeBankExt = True
                bEnabledvalAccount = True
                bEnabledcbeTyp_Account = True
                bEnabledtctBankAuth = True

                bEnabledcbeTyp_crecard = Not lblnEnabled
                bEnabledtcdDateExpir = Not lblnEnabled
                bEnabledvalCredi_card = Not lblnEnabled
            End If
        End If
    End Sub
	
	'% StateVarCa003: Obtiene el estado (bEnabled, bVisible, sValue) de la variable indicada.
	Public ReadOnly Property StateVarCa003(ByVal nControlName As eTypeControlsCA003, ByVal nTypeValue As eTypeVarCA003) As Object
        Get
            Dim caseResult As Object = New Object

            Select Case nControlName
                '+ 0 Titular
                Case eTypeControlsCA003.tctClient
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledtctClient
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuetctClient
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 1 Domiciliación bancaria
                Case eTypeControlsCA003.optBank1
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledoptBank1
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValueoptBank1
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 2 Tarjeta de crédito
                Case eTypeControlsCA003.optBank2
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledoptBank2
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValueoptBank2
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 3 Banco
                Case eTypeControlsCA003.cbeBankExt
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledcbeBankExt
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuecbeBankExt
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 4 Cuenta Banco
                Case eTypeControlsCA003.valAccount
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledvalAccount
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuevalAccount
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 5 Oficina
                Case eTypeControlsCA003.valAgency
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledvalAgency
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuevalAgency
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 6 Tipo de tarjeta
                Case eTypeControlsCA003.cbeTyp_crecard
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledcbeTyp_crecard
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuecbeTyp_crecard
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 7 Mandato
                Case eTypeControlsCA003.tctBankAuth
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledtctBankAuth
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuetctBankAuth
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 8 Fecha de vencimiento
                Case eTypeControlsCA003.tcdDateExpir
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledtcdDateExpir
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuetcdDateExpir
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 9 Tipo de Cuenta Banco
                Case eTypeControlsCA003.cbeTyp_Account
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledcbeTyp_Account
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuecbeTyp_Account
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

                    '+ 10 Numero Tarjeta
                Case eTypeControlsCA003.valCredi_card
                    If nTypeValue = eTypeVarCA003.blnEnabled Then
                        caseResult = bEnabledvalCredi_card
                    ElseIf nTypeValue = eTypeVarCA003.strValue Then
                        caseResult = sValuevalCredi_card
                    ElseIf nTypeValue = eTypeVarCA003.blnVisible Then
                        caseResult = ""
                    End If

            End Select
            Return caseResult
        End Get
    End Property
	
	'% insGetClientCA003: Obtiene el nombre del titular del recibo de pago.
	Private Sub insGetClientCA003(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date)
		Dim lobjRoles As ePolicy.Roles
		
		lobjRoles = New ePolicy.Roles
		
		With lobjRoles
			Call .InsGetClientHolder(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
			
			sValuetctClient = .sClient
		End With
		
		'UPGRADE_NOTE: Object lobjRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjRoles = Nothing
	End Sub
	
	'% AnulDirDebit: Anulación de registro de pago automático
	Public Function AnulDirDebit(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecDir_debit As eRemoteDB.Execute
		
		On Error GoTo AnulDirDebit_Err
		
		lrecDir_debit = New eRemoteDB.Execute
		
		With lrecDir_debit
			.StoredProcedure = "UpdDir_debit_ca004"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AnulDirDebit = .Run(False)
		End With
		
AnulDirDebit_Err: 
		If Err.Number Then
			AnulDirDebit = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDir_debit = Nothing
	End Function
	
	' insValCO722: Se encarga de realizar las validaciones de la transacción CO722.
	Public Function insValCO722(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal sBankAuthOld As String, ByVal sBankAuthNew As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsPolicy As ePolicy.Policy
		Dim lclsCertificat As ePolicy.Certificat
		Dim lblnError As Boolean
		Dim lblnCertificat As Boolean
		
		lclsErrors = New eFunctions.Errors
		lclsPolicy = New ePolicy.Policy
		lclsCertificat = New ePolicy.Certificat
		
		On Error GoTo insValCO722_K_Err
		
		'+ Se valida que se haya incluido el Ramo
		If nBranch <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 1022)
			lblnError = True
		End If
		
		'+ Se valida que se haya incluido el Producto
		If nProduct <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 3635)
			lblnError = True
		End If
		
		'+ Se valida que se haya incluido el número de la póliza
		If nPolicy <= 0 Then
			lclsErrors.ErrorMessage(sCodispl, 3003)
			lblnError = True
		End If
		
		If Not lblnError Then
			With lclsPolicy
				If Not .Find(sCertype, nBranch, nProduct, nPolicy) Then
					'+ Si la póliza no está registrada.
					Call lclsErrors.ErrorMessage(sCodispl, 3001)
					lblnError = True
				Else
					'+ Se verifica que la póliza no esté anulada
					If .dNulldate <> eRemoteDB.Constants.dtmNull Then
						Call lclsErrors.ErrorMessage(sCodispl, 3098)
						lblnError = True
					Else
						'+ Se verifica que la póliza esté válida
						If .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or .sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
							Call lclsErrors.ErrorMessage(sCodispl, 3720)
							lblnError = True
						End If
						'+ Se verifica que la póliza esté vigente a la fecha del proceso
						If CDbl(.sStatus_pol) = 6 Or CDbl(.sStatus_pol) = 7 Or CDbl(.sStatus_pol) = 8 Or (dEffecdate > .dExpirdat And .dExpirdat <> eRemoteDB.Constants.dtmNull) Then
							lclsErrors.ErrorMessage(sCodispl, 60261)
							lblnError = True
						End If
					End If
				End If
			End With
		End If
		
		If Not lblnError Then
			'+ Si la póliza es colectiva o multilocalidad se debe incluir el número de certificado.
			If lclsPolicy.sPolitype <> "1" Then
				If nCertif <= 0 Then
					lclsErrors.ErrorMessage(sCodispl, 3006)
					lblnError = True
				End If
			End If
			
			If nCertif >= 0 Then
				lblnCertificat = False
				With lclsCertificat
					If Not .Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						'+ Si el certificado no está registrado.
						Call lclsErrors.ErrorMessage(sCodispl, 3010)
					Else
						lblnCertificat = True
						'+ Se válida que el certificado sea válido
						If .sStatusva = "3" Or .sStatusva = "2" Then
							Call lclsErrors.ErrorMessage(sCodispl, 3724)
						Else
							If .dNulldate <> eRemoteDB.Constants.dtmNull Then
								Call lclsErrors.ErrorMessage(sCodispl, 3099)
							Else
								'+ Se verifica que el certificado este vigente para la póliza.
								If .dStartdate > dEffecdate Or (.dExpirdat <> eRemoteDB.Constants.dtmNull And .dExpirdat < dEffecdate) Then
									lclsErrors.ErrorMessage(sCodispl, 5580)
									lblnError = True
								End If
							End If
						End If
					End If
				End With
			End If
		End If
		
		'+ Se verifica que se haya incluido la fecha
		If dEffecdate = eRemoteDB.Constants.dtmNull Then
			lclsErrors.ErrorMessage(sCodispl, 3404)
			lblnError = True
		Else
			If lblnCertificat Then
				If dEffecdate < lclsCertificat.dChangdat Then
					lclsErrors.ErrorMessage(sCodispl, 10868,  , eFunctions.Errors.TextAlign.RigthAling, " (" & lclsCertificat.dChangdat & ")")
					lblnError = True
				End If
			End If
		End If
		
		'+ Se verifica que se haya incluido el número de mandato nuevo
		If sBankAuthNew = String.Empty Then
			lclsErrors.ErrorMessage(sCodispl, 55007)
			lblnError = True
		Else
			'+ Se verifica que el número de mandato nuevo sea diferente al mandato a modificar.
			If sBankAuthOld = sBankAuthNew Then
				lclsErrors.ErrorMessage(sCodispl, 60221)
				lblnError = True
			Else
				'+ Se verifica que si el nuevo mandato está registrado esté asociado a una póliza del mismo cliente;
				'+ es decir, el mandato debe pertenecer al mismo cliente
				If Not valExistsBankAutCli(sBankAuthNew, sClient, dEffecdate) Then
					lclsErrors.ErrorMessage(sCodispl, 60222)
				End If
			End If
		End If
		
		insValCO722 = lclsErrors.Confirm
		
insValCO722_K_Err: 
		If Err.Number Then
			insValCO722 = insValCO722 & Err.Description
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsPolicy = Nothing
		'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCertificat = Nothing
		
	End Function
	
	'%insPostCO722: Esta función se encaga de validar todos los datos
	Public Function insPostCO722(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date, ByVal sBankauth As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsDir_debit As ePolicy.DirDebit
		Dim lclsPolicy_his As ePolicy.Policy_his
		
		On Error GoTo insPostCO722_Err
		lclsDir_debit = New ePolicy.DirDebit
		
		With lclsDir_debit
			If .Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
				.sBankauth = sBankauth
				.nUsercode = nUsercode
				.dEffecdate = dEffecdate
				'+ Se verifica si el número de mandato está siendo reutilizado por otra póliza.
				If valExistsBankAutPol(sCertype, nBranch, nProduct, nPolicy, nCertif, sBankauth, sClient, dEffecdate) Then
					.sReuse = "1"
				End If
				'+ Se actualiza la información.
				If .Add Then
					'+ Se crea un registro enla historia de la póliza.
					lclsPolicy_his = New ePolicy.Policy_his
					If lclsPolicy_his.FindLastMovement(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
						lclsPolicy_his.dEffecdate = dEffecdate
						lclsPolicy_his.nUsercode = nUsercode
						lclsPolicy_his.nReceipt = eRemoteDB.Constants.intNull
						lclsPolicy_his.sNull_move = String.Empty
						lclsPolicy_his.nOficial_p = eRemoteDB.Constants.intNull
						insPostCO722 = lclsPolicy_his.Update_Policyhis(67)
					End If
					'UPGRADE_NOTE: Object lclsPolicy_his may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lclsPolicy_his = Nothing
				End If
			End If
		End With
insPostCO722_Err: 
		If Err.Number Then
			insPostCO722 = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDir_debit = Nothing
	End Function
	
	'% valExistsBankAutCli: Valida la existencia de un número de mandato para un determinado cliente.
	Public Function valExistsBankAutCli(ByVal sBankauth As String, ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecDir_debit As eRemoteDB.Execute
		
		On Error GoTo valExistsBankAutCli_Err
		
		lrecDir_debit = New eRemoteDB.Execute
		
		With lrecDir_debit
			.StoredProcedure = "reaDir_debit_sBankauth1"
			.Parameters.Add("sBankAuth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If .FieldToClass("sClient") = sClient Then
						valExistsBankAutCli = True
					End If
					Exit Do
					.RNext()
				Loop 
				.RCloseRec()
			Else
				valExistsBankAutCli = True
			End If
		End With
		
valExistsBankAutCli_Err: 
		If Err.Number Then
			valExistsBankAutCli = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDir_debit = Nothing
	End Function
	
	'% valExistsBankAutPol: Valida la existencia de un número de mandato para un determinado cliente.
	Public Function valExistsBankAutPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sBankauth As String, ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecDir_debit As eRemoteDB.Execute
		
		On Error GoTo valExistsBankAutPol_Err
		
		lrecDir_debit = New eRemoteDB.Execute
		
		With lrecDir_debit
			.StoredProcedure = "reaDir_debit_sBankauth1"
			.Parameters.Add("sBankAuth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Do While Not .EOF
					If .FieldToClass("sClient") = sClient Then
						
						'+ Se procesan los registros que no coincidan con la póliza pasada como parámetro.
						If .FieldToClass("sCertype") = sCertype And .FieldToClass("nBranch") = nBranch And .FieldToClass("nProduct") = nProduct And .FieldToClass("nPolicy") = nPolicy And .FieldToClass("nCertif") = nCertif Then
						Else
							valExistsBankAutPol = True
							Exit Do
						End If
					End If
					.RNext()
				Loop 
				.RCloseRec()
			End If
		End With
		
valExistsBankAutPol_Err: 
		If Err.Number Then
			valExistsBankAutPol = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDir_debit = Nothing
	End Function
	
	'% valExistsBankAutNum: Rescata Numero de mandato para el cliente .
	Public Function valExistsBankAutNum(ByVal sClient As String, ByVal dEffecdate As Date) As String
		Dim lrecDir_debit As eRemoteDB.Execute
		
		On Error GoTo valExistsBankAutNum_Err
		
		lrecDir_debit = New eRemoteDB.Execute
		
		valExistsBankAutNum = String.Empty
		With lrecDir_debit
			.StoredProcedure = "reaDir_debit_sBankauth2"
			.Parameters.Add("sClient", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				valExistsBankAutNum = .FieldToClass("sBankAuth")
				.RCloseRec()
			End If
		End With
		
valExistsBankAutNum_Err: 
		If Err.Number Then
			valExistsBankAutNum = String.Empty
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecDir_debit = Nothing
	End Function
	
	
	'% Find_Valsbankaut: Permite obtener cantidad de monedas y la maxima moneda
	Public Function Find_Valsbankaut(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sBankauth As String, ByVal nBankext As Integer) As Boolean
		Dim lrecinsValsbankauth As eRemoteDB.Execute
		Dim lclsinsValsbankauth As DirDebit
		
		On Error GoTo insValsbankauth_Err
		
		lrecinsValsbankauth = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValsbankauth al 08-18-2004 17:48:11
		'+
		With lrecinsValsbankauth
			.StoredProcedure = "insValsbankauth"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankauth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRelapsing", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				Find_Valsbankaut = IIf(.Parameters("nRelapsing").Value = 0, False, True)
			End If
			
		End With
		
insValsbankauth_Err: 
		If Err.Number Then
			Find_Valsbankaut = False
		End If
		
		'UPGRADE_NOTE: Object lrecinsValsbankauth may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValsbankauth = Nothing
		On Error GoTo 0
		
	End Function
End Class






