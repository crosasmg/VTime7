Option Strict Off
Option Explicit On
Public Class Process
	'%-------------------------------------------------------%'
	'% $Workfile:: Process.cls                              $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	
	'**- Properties according to the table in the system on November 06,2000
	'- Propiedades según la tabla en el sistema el 06/11/2000
	'**- The field keys correspond to nReference, nCode_activ, nCode_proce, sKey_process.
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
	Public nCertif As Double 'int      no       4      10    0     yes      (n/a)               (n/a)
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
	Public nPolicy As Double 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public nProduct As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nReceipt As Integer 'int      no       4      10    0     yes      (n/a)               (n/a)
	Public dStartdate As Date 'datetime no       8                  yes      (n/a)               (n/a)
	Public sStartHour As String 'char     no       8                  yes      no                  yes
	Public nStatus_pro As Integer 'smallint no       2      5     0     yes      (n/a)               (n/a)
	Public nUsercode As Integer 'smallint no       2      5     0     no       (n/a)               (n/a)
	
	'**%Find: Returns TRUE or FALSE if the records exists in the table "Process"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Process"
	Public Function Find(ByVal nReference As Integer, ByVal nCode_activ As Integer, ByVal nCode_proce As Integer, ByVal sKey_process As String) As Boolean
		Dim lrecreaProcess As eRemoteDB.Execute
		lrecreaProcess = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		
		'**+ Parameter definition for stored procedure 'insudb.reaProcess'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcess'
		'**+ Information read on November 06,2000  11:44:01 a.m.
		'+ Información leída el 06/11/2000 11:44:01 a.m.
		
		With lrecreaProcess
			.StoredProcedure = "reaProcess"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Find: Returns TRUE or FALSE if the records exists in the table "ProcessPolicy"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "ProcessPolicy"
	Public Function Find_policy(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nCode_proce As Integer, ByVal nCode_activ As Integer) As Boolean
		Dim lrecreaProcessPolicy As eRemoteDB.Execute
		
		lrecreaProcessPolicy = New eRemoteDB.Execute
		
		On Error GoTo Find_Policy_Err
		
		'**+ Parameter definition for stored procedure 'insudb.reaProcessPolicy'
		'+ Definición de parámetros para stored procedure 'insudb.reaProcessPolicy'
		'**+ Information read on Novemeber 06,2000  01:30:33 p.m.
		'+ Información leída el 06/11/2000 01:30:33 p.m.
		
		With lrecreaProcessPolicy
			.StoredProcedure = "reaProcessPolicy"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find_policy = True
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
				Find_policy = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaProcessPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaProcessPolicy = Nothing
		
Find_Policy_Err: 
		If Err.Number Then
			Find_policy = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Add_policy: creates a record in process, for a determined policy.
	'% Add_policy: crea un registro en process, para una póliza determinada
	Public Function Add_Policy() As Boolean
		Dim lreccreProcessPolicy As eRemoteDB.Execute
		
		lreccreProcessPolicy = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.creProcessPolicy'
		'+ Definición de parámetros para stored procedure 'insudb.creProcessPolicy'
		'**+ Information read on Novemeber 06,2000  02:04:21 p.m.
		'+ Información leída el 06/11/2000 02:04:21 p.m.
		
		With lreccreProcessPolicy
			.StoredProcedure = "creProcessPolicy"
			.Parameters.Add("nReference", nReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_activ", nCode_activ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode_proce", nCode_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey_process", sKey_process, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCheque", nCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFinishDate", dFinishDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFinishHour", sFinishHour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIn_charge", nIn_charge, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOp_acc_ban", nOp_acc_ban, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOpe_date", dOpe_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOp_office", nOp_office, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOp_transa", nOp_transa, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dPlan_date", dPlan_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPlan_hour", sPlan_hour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStartHour", sStartHour, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_pro", nStatus_pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
End Class






