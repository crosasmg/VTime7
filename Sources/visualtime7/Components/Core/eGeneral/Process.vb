Option Strict Off
Option Explicit On
Public Class Process
	'%-------------------------------------------------------%'
	'% $Workfile:: Process.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:24p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on Novemeber 06,2000
	'- Propiedades según la tabla en el sistema el 06/11/2000
	'**- The key fields corresponds to nReference, nCode_activ, nCode_proce, sKey_process
	'- Los campos llave corresponden a nReference, nCode_activ, nCode_proce, sKey_process
	
	'+ Column_name              Type                 Computed Length Prec  Scale Nullable TrimTrailingBlanks  FixedLenNullInSource
	'+ ------------------------ -------------------- -------- ------ ----- ----- -------- ------------------  --------------------
	Public nReference As Integer 'int      no       4      10    0     no       (n/a)               (n/a)
	Public nCode_activ As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public nCode_proce As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	Public sKey_process As String 'char     no       12                 no       no                  no
	Public nAccount As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nBordereaux As Integer 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public nBranch As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nCertif As Integer 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public nCheque As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nClaim As Double 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public sCodispl As String 'char     no       8                  yes      no                  yes
	Public dCompdate As Date 'datetime no       8                  no       (n/a)               (n/a)
	Public dFinishDate As Date 'datetime no       8                  yes      (n/a)               (n/a)
	Public sFinishHour As String 'char     no       8                  yes      no                  yes
	Public nIn_charge As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nOp_acc_ban As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public dOpe_date As Date 'datetime no       8                  yes      (n/a)               (n/a)
	Public nOp_office As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nOp_transa As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public dPlan_date As Date 'datetime no       8                  yes      (n/a)               (n/a)
	Public sPlan_hour As String 'char     no       8                  yes      no                  yes
	Public nPolicy As Integer 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public nProduct As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nReceipt As Integer 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public dStartdate As Date 'datetime no       8                  yes      (n/a)               (n/a)
	Public sStartHour As String 'char     no       8                  yes      no                  yes
	Public nStatus_pro As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nUsercode As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	
	'**% Find: reads the table.
	'% Find: realiza la lectura de la tabla
	Public Function Find(ByVal nReference As Integer, ByVal nCode_activ As Integer, ByVal nCode_proce As Integer, ByVal sKey_process As String) As Boolean
		Dim lrecreaProcess As eRemoteDB.Execute
		lrecreaProcess = New eRemoteDB.Execute
		
		On Error GoTo Find_err
		
		'**+ Parameter definition for the stored procedure 'insudb.reaProcess'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcess'
		'**+ Information read on Novemeber 06, 2000  11:44:01 a.m.
		'+ Información leída el 06/11/2000 11:44:01 a.m.
		
		With lrecreaProcess
			.StoredProcedure = "reaProcess"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey_process", sKey_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Me.nReference = .FieldToClass("nReference")
				Me.nCode_activ = .FieldToClass("nCode_activ")
				Me.nCode_proce = .FieldToClass("nCode_proce")
				Me.sKey_process = .FieldToClass("sKey_process")
				nAccount = .FieldToClass("nAccount")
				nBordereaux = .FieldToClass("nBordereaux")
				nBranch = .FieldToClass("nBranch")
				nCertif = .FieldToClass("nCertif")
				nCheque = .FieldToClass("nCheque")
				nClaim = .FieldToClass("nClaim")
				sCodispl = .FieldToClass("sCodispl")
				dCompdate = .FieldToClass("dCompdate")
				dFinishDate = .FieldToClass("dFinishDate")
				sFinishHour = .FieldToClass("sFinishHour")
				nIn_charge = .FieldToClass("nIn_charge")
				nOp_acc_ban = .FieldToClass("nOp_acc_ban")
				dOpe_date = .FieldToClass("dOpe_date")
				nOp_office = .FieldToClass("nOp_office")
				nOp_transa = .FieldToClass("nOp_transa")
				dPlan_date = .FieldToClass("dPlan_date")
				sPlan_hour = .FieldToClass("sPlan_hour")
				nPolicy = .FieldToClass("nPolicy")
				nProduct = .FieldToClass("nProduct")
				nReceipt = .FieldToClass("nReceipt")
				dStartdate = .FieldToClass("dStartdate")
				sStartHour = .FieldToClass("sStartHour")
				nStatus_pro = .FieldToClass("nStatus_pro")
				nUsercode = .FieldToClass("nUsercode")
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProcess = Nothing
		
Find_err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Find_Policy: reads the table for a specific policy.
	'% Find_Policy: realiza la lectura de process, para una póliza determinada
	Public Function Find_Policy(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal nCode_proce As Integer, ByVal nCode_activ As Integer) As Boolean
		Dim lrecreaProcessPolicy As eRemoteDB.Execute
		
		lrecreaProcessPolicy = New eRemoteDB.Execute
		
		On Error GoTo Find_Policy_Err
		
		'**+ Parameter definition for stored pocedure 'insud.reaProcessPolicy'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcessPolicy'
		'**+ Information read on November 06,2000  01:30:33 p.m.
		'+ Información leída el 06/11/2000 01:30:33 p.m.
		
		With lrecreaProcessPolicy
			.StoredProcedure = "reaProcessPolicy"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Policy = True
				nReference = .FieldToClass("nReference")
				Me.nCode_activ = .FieldToClass("nCode_activ")
				Me.nCode_proce = .FieldToClass("nCode_proce")
				sKey_process = .FieldToClass("sKey_process")
				nAccount = .FieldToClass("nAccount")
				nBordereaux = .FieldToClass("nBordereaux")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nCertif = .FieldToClass("nCertif")
				nCheque = .FieldToClass("nCheque")
				nClaim = .FieldToClass("nClaim")
				sCodispl = .FieldToClass("sCodispl")
				dFinishDate = .FieldToClass("dFinishDate")
				sFinishHour = .FieldToClass("sFinishHour")
				nIn_charge = .FieldToClass("nIn_charge")
				nOp_acc_ban = .FieldToClass("nOp_acc_ban")
				dOpe_date = .FieldToClass("dOpe_date")
				nOp_office = .FieldToClass("nOp_office")
				nOp_transa = .FieldToClass("nOp_transa")
				dPlan_date = .FieldToClass("dPlan_date")
				sPlan_hour = .FieldToClass("sPlan_hour")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nProduct = .FieldToClass("nProduct")
				nReceipt = .FieldToClass("nReceipt")
				dStartdate = .FieldToClass("dStartdate")
				sStartHour = .FieldToClass("sStartHour")
				nStatus_pro = .FieldToClass("nStatus_pro")
				.RCloseRec()
			Else
				Find_Policy = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProcessPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProcessPolicy = Nothing
		
Find_Policy_Err: 
		If Err.Number Then
			Find_Policy = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add_policy: creates a record for a policy
	'% Add_policy: crea un registro en process, para una póliza determinada
	Public Function Add_Policy() As Boolean
		Dim lreccreProcessPolicy As eRemoteDB.Execute
		
		lreccreProcessPolicy = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.creProcessPolicy'
		'+ Definición de parámetros para stored procedure 'insudb.creProcessPolicy'
		'**+ Information read on November 06,2000  02:04:21 p.m.
		'+ Información leída el 06/11/2000 02:04:21 p.m.
		
		With lreccreProcessPolicy
			.StoredProcedure = "creProcessPolicy"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nReference", IIf(nReference = eRemoteDB.Constants.intNull, System.DBNull.Value, nReference), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCode_activ", IIf(nCode_activ = eRemoteDB.Constants.intNull, System.DBNull.Value, nCode_activ), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCode_proce", IIf(nCode_proce = eRemoteDB.Constants.intNull, System.DBNull.Value, nCode_proce), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sKey_process", IIf(sKey_process = String.Empty, System.DBNull.Value, sKey_process), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nAccount", IIf(nAccount = eRemoteDB.Constants.intNull, System.DBNull.Value, nAccount), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBordereaux", IIf(nBordereaux = eRemoteDB.Constants.intNull, System.DBNull.Value, nBordereaux), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nBranch", IIf(nBranch = eRemoteDB.Constants.intNull, System.DBNull.Value, nBranch), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCertif", IIf(nCertif = eRemoteDB.Constants.intNull, System.DBNull.Value, nCertif), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCheque", IIf(nCheque = eRemoteDB.Constants.intNull, System.DBNull.Value, nCheque), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nClaim", IIf(nClaim = eRemoteDB.Constants.intNull, System.DBNull.Value, nClaim), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sCodispl", IIf(sCodispl = CStr(eRemoteDB.Constants.strNull), System.DBNull.Value, sCodispl), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dFinishDate", IIf(dFinishDate = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dFinishDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sFinishHour", IIf(sFinishHour = CStr(eRemoteDB.Constants.strNull), System.DBNull.Value, sFinishHour), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nIn_charge", IIf(nIn_charge = eRemoteDB.Constants.intNull, System.DBNull.Value, nIn_charge), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOp_acc_ban", IIf(nOp_acc_ban = eRemoteDB.Constants.intNull, System.DBNull.Value, nOp_acc_ban), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dOpe_date", IIf(dOpe_date = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dOpe_date), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOp_office", IIf(nOp_office = eRemoteDB.Constants.intNull, System.DBNull.Value, nOp_office), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nOp_transa", IIf(nOp_transa = eRemoteDB.Constants.intNull, System.DBNull.Value, nOp_transa), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dPlan_date", IIf(dPlan_date = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dPlan_date), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sPlan_hour", IIf(sPlan_hour = CStr(eRemoteDB.Constants.strNull), System.DBNull.Value, sPlan_hour), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nPolicy", IIf(nPolicy = eRemoteDB.Constants.intNull, System.DBNull.Value, nPolicy), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nProduct", IIf(nProduct = eRemoteDB.Constants.intNull, System.DBNull.Value, nProduct), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nReceipt", IIf(nReceipt = eRemoteDB.Constants.intNull, System.DBNull.Value, nReceipt), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("dStartDate", IIf(dStartdate = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dStartdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sStartHour", IIf(sStartHour = CStr(eRemoteDB.Constants.strNull), System.DBNull.Value, sStartHour), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nStatus_pro", IIf(nStatus_pro = eRemoteDB.Constants.intNull, System.DBNull.Value, nStatus_pro), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nUsercode", IIf(nUsercode = eRemoteDB.Constants.intNull, System.DBNull.Value, nUsercode), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add_Policy = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreProcessPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProcessPolicy = Nothing
		
Add_policy_Err: 
		If Err.Number Then
			Add_Policy = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add_Claim: creates a record for a claim
	'% Add_Claim: crea un registro en process, para un siniestro determinado
	Public Function Add_Claim() As Boolean
		Dim lreccreProcessClaim As eRemoteDB.Execute
		
		lreccreProcessClaim = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.creProcessClaim'
		'+ Definición de parámetros para stored procedure 'insudb.creProcessClaim'
		'**+ Information read on January 12,2001 9.28.43
		'+ Información leída el 12/01/2001 9.28.43
		
		With lreccreProcessClaim
			.StoredProcedure = "creProcessClaim"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey_process", sKey_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add_Claim = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreProcessClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProcessClaim = Nothing
		
Add_Claim_Err: 
		If Err.Number Then
			Add_Claim = False
		End If
		On Error GoTo 0
	End Function
	'**%AddProduct: This method is in charge of adding new records to the table "Process".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%AddProduct: Este método se encarga de agregar nuevos registros a la tabla "Process". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function AddProduct() As Boolean
		Dim lreccreProcess As eRemoteDB.Execute
		
		On Error GoTo AddProduct_Err
		
		lreccreProcess = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.creProcess'
		'+ Definición de parámetros para stored procedure 'insudb.creProcess'
		'**+ Information read on Novemeber 15,2000  15.36.31
		'+ Información leída el 15/11/2000 15.36.31
		
		With lreccreProcess
			.StoredProcedure = "creProcess"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey_process", sKey_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStartHour", sStartHour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_pro", nStatus_pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			AddProduct = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreProcess = Nothing
		
AddProduct_Err: 
		If Err.Number Then
			AddProduct = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Find_Claim: Find a determined claim
	'% Find_Claim: realiza la lectura de process, para un Siniestro determinado
	Public Function Find_Claim(ByVal nClaim As Double, ByVal nCode_proce As Integer, ByVal nCode_activ As Integer) As Boolean
		Dim lrecreaProcessClaim As eRemoteDB.Execute
		
		lrecreaProcessClaim = New eRemoteDB.Execute
		
		On Error GoTo Find_Claim_Err
		
		'**+ Parameter definition for the stored procedure 'insud.reaProcessClaim'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcessClaim'
		'**+ Information read on January 12,2001  9.25.23
		'+ Información leída el 12/01/2001 9.25.23
		
		With lrecreaProcessClaim
			.StoredProcedure = "reaProcessClaim"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_Claim = True
				nReference = .FieldToClass("nReference")
				Me.nCode_activ = .FieldToClass("nCode_activ")
				Me.nCode_proce = .FieldToClass("nCode_proce")
				sKey_process = .FieldToClass("sKey_process")
				nAccount = .FieldToClass("nAccount")
				nBordereaux = .FieldToClass("nBordereaux")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nCertif = .FieldToClass("nCertif")
				nCheque = .FieldToClass("nCheque")
				nClaim = .FieldToClass("nClaim")
				sCodispl = .FieldToClass("sCodispl")
				dFinishDate = .FieldToClass("dFinishDate")
				sFinishHour = .FieldToClass("sFinishHour")
				nIn_charge = .FieldToClass("nIn_charge")
				nOp_acc_ban = .FieldToClass("nOp_acc_ban")
				dOpe_date = .FieldToClass("dOpe_date")
				nOp_office = .FieldToClass("nOp_office")
				nOp_transa = .FieldToClass("nOp_transa")
				dPlan_date = .FieldToClass("dPlan_date")
				sPlan_hour = .FieldToClass("sPlan_hour")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nProduct = .FieldToClass("nProduct")
				nReceipt = .FieldToClass("nReceipt")
				dStartdate = .FieldToClass("dStartdate")
				sStartHour = .FieldToClass("sStartHour")
				nStatus_pro = .FieldToClass("nStatus_pro")
				.RCloseRec()
			Else
				Find_Claim = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProcessClaim may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProcessClaim = Nothing
		
Find_Claim_Err: 
		If Err.Number Then
			Find_Claim = False
		End If
		On Error GoTo 0
	End Function
	'**%Update: This method is in charge of updating records in the table "Process".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Process". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update_Finish() As Boolean
		Dim lrecUpdProcess As eRemoteDB.Execute
		
		lrecUpdProcess = New eRemoteDB.Execute
		
		On Error GoTo Update_Finish_Err
		
		'**+ Parameter definition for stored procedure 'insudb.updProcess'
		'+Definición de parámetros para stored procedure 'insudb.updProcess'
		'**+ Information read on January 22, 2001   10.25.34
		'+Información leída el 22/01/2001 10.25.34
		
		With lrecUpdProcess
			.StoredProcedure = "updProcess"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFinishDate", dFinishDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFinishHour", sFinishHour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecUpdProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdProcess = Nothing
		
Update_Finish_Err: 
		If Err.Number Then
			Update_Finish = False
		End If
		On Error GoTo 0
	End Function
	'**%FindProcessByProduct: This method returns TRUE or FALSE depending if the records exists in the table "Process"
	'%FindProcessByProduct: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Process"
	Public Function FindProcessByProduct(ByVal bActiv As Boolean) As Boolean
		Dim lrecreaProcess As eRemoteDB.Execute
		
		On Error GoTo FindProcessByProduct_Err
		
		lrecreaProcess = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insud.reaProcess'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcess'
		'**+ Information read on Novemeber 15,2000  14.05.59
		'+ Información leída el 15/11/2000 14.05.59
		
		FindProcessByProduct = False
		With lrecreaProcess
			.StoredProcedure = "reaProcessPKG.reaProcess"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey_process", sKey_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If bActiv Then
					Do While Not lrecreaProcess.EOF
						If lrecreaProcess.FieldToClass("nCode_Activ") <> 3 Then
							FindProcessByProduct = True
						End If
						lrecreaProcess.RNext()
					Loop 
				Else
					FindProcessByProduct = True
				End If
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProcess may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProcess = Nothing
		
FindProcessByProduct_Err: 
		If Err.Number Then
			FindProcessByProduct = False
		End If
		On Error GoTo 0
	End Function
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Process"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Process"
	Public Function FindProcess_v() As Boolean
		Dim lrecreaProcess_v As eRemoteDB.Execute
		
		lrecreaProcess_v = New eRemoteDB.Execute
		
		'**+ Parameter definition for the stored procedure 'insudb.reaProcess_v'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcess_v'
		'**+ Information read on Novemeber 15,2000  10.56.38
		'+ Información leída el 15/11/2000 10.56.38
		
		With lrecreaProcess_v
			.StoredProcedure = "reaProcess_v"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nReference = .FieldToClass("nReference")
				FindProcess_v = True
				.RCloseRec()
			Else
				FindProcess_v = True
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProcess_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProcess_v = Nothing
	End Function
End Class






