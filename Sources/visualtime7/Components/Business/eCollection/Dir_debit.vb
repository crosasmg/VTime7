Option Strict Off
Option Explicit On
Public Class Dir_debit
	'%-------------------------------------------------------%'
	'% $Workfile:: Dir_debit.cls                            $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 12/10/04 1:25p                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Global constants definition. Management of direct debit type
	'-Se definen las constantes globales para el manejo del tipo de domiciliación
	
	Enum TypeDirdebit
		cstrBank = 1 '**Bank (Current account)
		'Banco (Cuenta corriente)
		cstrCrediCard = 2 '**Credit card
		'Tarjeta de credito
		cstrProductor = 3 '**Per intermediary
		'Por productor
	End Enum
	
	'**-Global constants definition. Intermediary type management
	'-Se definen las constantes globales para el manejo del tipo de intermediarios
	
	Enum Interm_typ
		clngProducer = 1 '**Producer
		' Productor
		clngOrganizer = 10 '**Organizer
		' Organizador
		clngAgentReceptacle = 20 '**Collector
		' Gestor de cobro
		clngAgent = 4 '**Agent
		' Agente
	End Enum
	
	'**+Properties according the table in the system on 02/20/2001
	'**+ The key fields are sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate
	'+ Propiedades según la tabla en el sistema el 20/02/2001
	'+ Los campos llave corresponden a sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate
	
	'    Column_name                   Type                Computed       Length       Prec   Scale   Nullable       TrimTrailingBlanks        FixedLenNullInSource
	'----------------------   -----------------------     -----------  -------------  ------ ------ ------------  ------------------------  --------------------------
	Public sCertype As String 'char          no            1                           no                   no                         no
	Public nBranch As Integer 'smallint      no            2           5     0         no                  (n/a)                      (n/a)
	Public nProduct As Integer 'smallint      no            2           5     0         no                  (n/a)                      (n/a)
	Public nPolicy As Double 'int           no            4           10    0         no                  (n/a)                      (n/a)
	Public nCertif As Double 'int           no            4           10    0         no                  (n/a)                      (n/a)
	Public dEffecdate As Date 'datetime      no            8                           no                  (n/a)                      (n/a)
	Public sAccount As String 'char          no           25                           yes                  no                         yes
	Public nBankext As Double 'int           no            4           10    0         yes                 (n/a)                      (n/a)
	Public sClient As String 'char          no           14                           no                   no                         no
	Public dNulldate As Date 'datetime      no            8                           yes                 (n/a)                      (n/a)
	Public sCredi_card As String 'char          no           20                           yes                  no                         yes
	Public nTyp_crecard As Integer 'smallint      no            2           5     0         yes                 (n/a)                      (n/a)
	Public sTyp_dirdeb As TypeDirdebit 'char          no            1                           yes                  no                         yes
	Public dCompdate As Date 'datetime      no            8                           yes                 (n/a)                      (n/a)
	Public nUsercode As Integer 'smallint      no            2           5     0         yes                 (n/a)                      (n/a)
	Public dCardExpir As Date 'datetime      no            8                           yes                 (n/a)                      (n/a)
	
	'**+ Auxiliary properties
	'+ Propiedades Auxiliares
	
	Public sTypeNumeraP As String
	Public nReceipt As Double
	Public nDigit As Integer
	Public nPaynumbe As Integer
	Public sDesOffice As String
	Public sDesClient As String
	Public nIntermed As Double
	Public sDesIntermed As String
	Public sCliename As String
	Public sDigit As String
	Public sWhatChange As String
	Public sBankauth As String
	Public sDirInd As String
	
	'**%insUpdDir_debit: This function updates the data of the table "dir_debit"
	'%insUpdDir_debit: Esta función se encarga de actualizar la información en tratamiento de la tabla Dir_debit.
	Public Function insUpdDir_debit() As Boolean
		
		Dim lrecupdDir_debit As eRemoteDB.Execute
		
		lrecupdDir_debit = New eRemoteDB.Execute
		
		insUpdDir_debit = True
		
		On Error GoTo insUpdDir_debit_Err
		
		'**Stored procedure parameters definition 'insudb.updDir_debit'
		'**+Data of 20/02/2001 02:42:21 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.updDir_debit'
		'+ Información leída el 02/20/2001 02:42:21 p.m.
		
		With lrecupdDir_debit
			.StoredProcedure = "updDir_debit"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sWhatChange = CStr(TypeDirdebit.cstrbank) Then
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAccount", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sWhatChange = CStr(TypeDirdebit.cstrbank) Then
				.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nBankext", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sWhatChange = CStr(TypeDirdebit.cstrCrediCard) Then
				.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sCredi_card", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sWhatChange = CStr(TypeDirdebit.cstrCrediCard) Then
				.Parameters.Add("nTyp_crecard", nTyp_crecard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("nTyp_crecard", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sWhatChange = CStr(TypeDirdebit.cstrCrediCard) Then
				.Parameters.Add("sTyp_dirdeb", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sTyp_dirdeb", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTypeNumeraP", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankauth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCardExpir", dCardExpir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDirind", sDirInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdDir_debit = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdDir_debit = Nothing
		
insUpdDir_debit_Err: 
		If Err.Number Then
			insUpdDir_debit = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdDir_debit = Nothing
	End Function
	
	'**%insCreDir_debitBeforeAnul: This function updates the data of the table dir_debit
	'%insCreDir_debitBeforeAnul: Esta función se encarga de actualizar la información en tratamiento de la tabla Dir_debit.
	Public Function insCreDir_debitBeforeAnul() As Boolean
		
		Dim lreccreDir_debitBeforeAnul As eRemoteDB.Execute
		
		lreccreDir_debitBeforeAnul = New eRemoteDB.Execute
		
		insCreDir_debitBeforeAnul = True
		
		On Error GoTo insCreDir_debitBeforeAnul_Err
		
		'**+Stored procedure parameters definition 'insudb.creDir_debitBeforeAnul'
		'**+Data of 02/20/2001 02:51:33 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.creDir_debitBeforeAnul'
		'+ Información leída el 20/02/2001 02:51:33 p.m.
		
		With lreccreDir_debitBeforeAnul
			.StoredProcedure = "creDir_debitBeforeAnul"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If sTyp_dirdeb = TypeDirdebit.cstrbank Then
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAccount", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sTyp_dirdeb = TypeDirdebit.cstrbank Then
				.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nBankext", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If sTyp_dirdeb = TypeDirdebit.cstrCrediCard Then
				.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sCredi_card", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sTyp_dirdeb = TypeDirdebit.cstrCrediCard Then
				.Parameters.Add("nTyp_crecard", nTyp_crecard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nTyp_crecard", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sTyp_dirdeb = TypeDirdebit.cstrCrediCard Then
				.Parameters.Add("sTyp_dirdeb", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sTyp_dirdeb", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankauth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCardExpir", dCardExpir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDirind", sDirInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insCreDir_debitBeforeAnul = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lreccreDir_debitBeforeAnul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreDir_debitBeforeAnul = Nothing
		
insCreDir_debitBeforeAnul_Err: 
		If Err.Number Then
			insCreDir_debitBeforeAnul = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreDir_debitBeforeAnul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreDir_debitBeforeAnul = Nothing
	End Function
	
	'**%insCreDir_debit: This function updates the data of the table dir_debit
	'%insCreDir_debit: Esta función se encarga de actualizar la información en tratamiento de la tabla Dir_debit.
	Public Function insCreDir_debit() As Boolean
		Dim lreccreDir_debit As eRemoteDB.Execute
		
		lreccreDir_debit = New eRemoteDB.Execute
		
		insCreDir_debit = True
		
		On Error GoTo insCreDir_debit_Err
		
		'**+Stored procedure parameters definition 'insudb.creDir_debit'
		'**+Data of 02/28/2001 02:51:33 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.creDir_debit'
		'+ Información leída el 28/02/2001 11:23:44 a.m.
		
		With lreccreDir_debit
			.StoredProcedure = "creDir_debit"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If sTyp_dirdeb = TypeDirdebit.cstrbank Then
				.Parameters.Add("sAccount", sAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sAccount", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sTyp_dirdeb = TypeDirdebit.cstrbank Then
				.Parameters.Add("nBankext", nBankext, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nBankext", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If sTyp_dirdeb = TypeDirdebit.cstrCrediCard Then
				.Parameters.Add("sCredi_card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sCredi_card", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sTyp_dirdeb = TypeDirdebit.cstrCrediCard Then
				.Parameters.Add("nTyp_crecard", nTyp_crecard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nTyp_crecard", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			If sTyp_dirdeb = TypeDirdebit.cstrCrediCard Then
				.Parameters.Add("sTyp_dirdeb", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("sTyp_dirdeb", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBankauth", sBankauth, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCardExpir", dCardExpir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insCreDir_debit = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lreccreDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreDir_debit = Nothing
		
insCreDir_debit_Err: 
		If Err.Number Then
			insCreDir_debit = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreDir_debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreDir_debit = Nothing
	End Function
	
	'**%insUpdDir_debitAnul: This function updates the data of the table dir_debit
	'%insUpdDir_debitAnul: Esta función se encarga de actualizar la información en tratamiento de la tabla Dir_debit.
	Public Function insUpdDir_debitAnul() As Boolean
		
		Dim lrecupdDir_debitAnul As eRemoteDB.Execute
		
		lrecupdDir_debitAnul = New eRemoteDB.Execute
		
		insUpdDir_debitAnul = True
		
		On Error GoTo insUpdDir_debitAnul_Err
		
		'**+Stored procedure parameters definition 'insudb.updDir_debitAnul'
		'**+Data of 20/02/2001 02:51:33 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.updDir_debitAnul'
		'+ Información leída el 20/02/2001 03:46:42 p.m.
		
		With lrecupdDir_debitAnul
			.StoredProcedure = "updDir_debitAnul"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdDir_debitAnul = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdDir_debitAnul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdDir_debitAnul = Nothing
		
insUpdDir_debitAnul_Err: 
		If Err.Number Then
			insUpdDir_debitAnul = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdDir_debitAnul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdDir_debitAnul = Nothing
	End Function
	
	'**%insValDir_debitExist: This routine validates if there is a valid record for a given date
	'%insValDir_debitExist: Esta rutina permite validar si existe en la tabla algún registro valido para la fecha dada.
	Public Function insValDir_debitExist(ByVal dDate As Date) As Boolean
		
		Dim lrecreaDir_debitMaxNulldate As eRemoteDB.Execute
		lrecreaDir_debitMaxNulldate = New eRemoteDB.Execute
		
		insValDir_debitExist = False
		
		On Error GoTo insValDir_debitExist_Err
		
		'**+Stored procedure parameters definition 'insudb.reaDir_debitMaxNulldate'
		'**+Data of 02/21/2001 04:05:07 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.reaDir_debitMaxNulldate'
		'+ Información leída el 21/02/2001 04:05:07 p.m.
		
		With lrecreaDir_debitMaxNulldate
			.StoredProcedure = "reaDir_debitMaxNulldate"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dtmNulldate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				If Not .Parameters("dtmNulldate").Value = eRemoteDB.Constants.dtmNull Then
					If CDate(.Parameters("dtmNulldate").Value) > CDate(dDate) Then
						insValDir_debitExist = True
					End If
				End If
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaDir_debitMaxNulldate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDir_debitMaxNulldate = Nothing
		
insValDir_debitExist_Err: 
		If Err.Number Then
			insValDir_debitExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaDir_debitMaxNulldate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDir_debitMaxNulldate = Nothing
	End Function
	
	'**%insReaPremiumDir_debit: This function reads the data of the table dir_debit
	'%insReaPremiumDir_debit: Esta función se encarga de leer la información en tratamiento de la tabla principal para la transacción.
	Public Function insReaPremiumDir_debit(ByVal sCertype As String, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, ByVal sTypeNumeraP As String, ByVal dEffecdate As Date, ByVal nContrat As Double, ByVal nDraft As Integer) As Boolean
		
		Dim lrecinsReaCO004 As eRemoteDB.Execute
		
		lrecinsReaCO004 = New eRemoteDB.Execute
		
		insReaPremiumDir_debit = True
		
		On Error GoTo insReaPremiumDir_debit_Err
		
		'**+Stored procedure parameters definition 'insudb.insReaCO004'
		'**+Data of 02/21/2001 04:05:07 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.insReaCO004'
		'+ Información leída el 21/02/2001 09:08:44 a.m.
		
		With lrecinsReaCO004
			.StoredProcedure = "insReaCO004"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nPolicy = .FieldToClass("nPolicy")
				sAccount = .FieldToClass("sAccount")
				nBankext = .FieldToClass("nBankext")
				sClient = .FieldToClass("sClient")
				sCredi_card = .FieldToClass("sCredi_card")
				nTyp_crecard = .FieldToClass("nTyp_crecard")
				sDesClient = .FieldToClass("sDesClient")
				nIntermed = .FieldToClass("nIntermed")
				sDesIntermed = .FieldToClass("sDesIntermed")
				sDigit = .FieldToClass("sDigit")
				sBankauth = .FieldToClass("sBankauth")
				dCardExpir = .FieldToClass("dCardExpir")
				sDirInd = .FieldToClass("sDirind")
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				If Not IsDbNull(.FieldToClass("dEffecdateDir")) Then
					Me.dEffecdate = .FieldToClass("dEffecdateDir")
				Else
					Me.dEffecdate = eRemoteDB.Constants.dtmNull
				End If
				
				If Trim(.FieldToClass("sTyp_dirdeb")) = String.Empty Then
					sTyp_dirdeb = CShort("3")
				Else
					sTyp_dirdeb = .FieldToClass("sTyp_dirdeb")
				End If
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsReaCO004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaCO004 = Nothing
		
insReaPremiumDir_debit_Err: 
		If Err.Number Then
			insReaPremiumDir_debit = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsReaCO004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaCO004 = Nothing
	End Function
	
	'**insValCtrol_date: This routine validates if the relationship exists
	'%insValCtrol_date: Esta rutina permite validar si la relación existe.
	Public Function insValCtrol_date(ByVal nType_proce As Integer) As Boolean
		Dim lrecreaCtrol_Date As eRemoteDB.Execute
		
		lrecreaCtrol_Date = New eRemoteDB.Execute
		
		insValCtrol_date = True
		
		On Error GoTo insValCtrol_date_Err
		
		'**+Stored procedure parameters definition 'insudb.reaCtrol_Date'
		'**+Data of 01/16/2001 10:38:22
		'+ Definición de parámetros para stored procedure 'insudb.reaCtrol_Date'
		'+ Información leída el 16/01/2001 10:38:22
		
		With lrecreaCtrol_Date
			.StoredProcedure = "reaCtrol_Date"
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
			Else
				insValCtrol_date = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCtrol_Date = Nothing
		
insValCtrol_date_Err: 
		If Err.Number Then
			insValCtrol_date = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaCtrol_Date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCtrol_Date = Nothing
	End Function
	'insPostCO004: Se realiza la actualización de los datos en la ventana CO004 (Folder)
    Public Function insPostCO004(ByVal sCodispl As String, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nPolicy As Double = 0, _
                                 Optional ByVal nBranch As Double = 0, Optional ByVal nProduct As Double = 0, Optional ByVal nCertif As Integer = 0, _
                                 Optional ByVal nReceipt As Double = 0, Optional ByVal nContrat As Integer = 0, Optional ByVal nDraft As Integer = 0, _
                                 Optional ByVal nBank As Integer = 0, Optional ByVal nWay_Pay As Integer = 0, Optional ByVal sClientPac As String = "", _
                                 Optional ByVal sAccountPac As String = "", Optional ByVal sBankauthPac As String = "", Optional ByVal nCardType As Integer = 0, _
                                 Optional ByVal dCardExpir As Date = #12:00:00 AM#, Optional ByVal sClientCard As String = "", Optional ByVal nCause_amen As Integer = 0, _
                                 Optional ByVal sTypeDoc As String = "", Optional ByVal sTypeChangeWay As String = "", Optional ByVal sChangePremium As String = "", _
                                 Optional ByVal nWayPayNew As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nAgreementNew As Integer = 0, _
                                 Optional ByVal nOriginNew As Integer = 0, Optional ByVal nAFPCommiNew As Double = 0, Optional ByVal nCurrencyNew As Integer = 0, _
                                 Optional ByVal sClientPay As String = "", Optional ByVal sClientEmp As String = "", Optional ByVal nAgreementOld As Integer = 0) As Object

        Dim lrecUpdWayPay As eRemoteDB.Execute

        lrecUpdWayPay = New eRemoteDB.Execute

        On Error GoTo insPostCO004_Err

        With lrecUpdWayPay
            .StoredProcedure = "Inschangewaypay"
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCause_Amen", nCause_amen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeDoc", sTypeDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeChangeWay", sTypeChangeWay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sChangePremium", sChangePremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nWayPayNew", nWayPayNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientPac", sClientPac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAccountPac", sAccountPac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 25, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBankAuthPac", sBankauthPac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCardType", nCardType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCredi_Card", sCredi_card, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dCardExpir", dCardExpir, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientCard", sClientCard, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nAgreementNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOriginNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_Commiss", nAFPCommiNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAfp_Comm_Curr", nCurrencyNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientPay", sClientPay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientEmp", sClientEmp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_AgreeOLD", nAgreementOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostCO004 = .Run(False)
        End With

insPostCO004_Err:
        If Err.Number Then
            insPostCO004 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecUpdWayPay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecUpdWayPay = Nothing

    End Function
	'**%FindRolesExist(): This method validates if a client is associated to a policy/certificate (item)
	'%FindRolesExist(): Metodo que verifica si un cliente en específico esta asociado a una póliza o certificado.
	Public Function FindRolesExist(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dEffecdate As Date) As Boolean
		
		Dim lclsClient As eClient.Client
		Dim lrecReaRoles As eRemoteDB.Execute
		
		lrecReaRoles = New eRemoteDB.Execute
		lclsClient = New eClient.Client
		
		On Error GoTo FindRolesExist_Err
		
		FindRolesExist = False
		With lrecReaRoles
			.StoredProcedure = "reaRoles_CA003"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindRolesExist = True
				'sClient = .FieldToClass("sClient")
				If lclsClient.Find(sClient) Then
					sCliename = lclsClient.sCliename
				End If
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecReaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRoles = Nothing
		
FindRolesExist_Err: 
		If Err.Number Then
			FindRolesExist = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaRoles = Nothing
		'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsClient = Nothing
	End Function
End Class






