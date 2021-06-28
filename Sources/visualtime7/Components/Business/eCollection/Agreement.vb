Option Strict Off
Option Explicit On
Public Class Agreement
	'%-------------------------------------------------------%'
	'% $Workfile:: Agreement.cls                            $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 20/10/04 3:38p                               $%'
	'% $Revision:: 42                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Properties according to the table agreement 26/10/2000
	'+ The fields keys correspond to nCod_agree
	
	'+ Los campos llaves corresponden a nCod_agree
	'+ Estructura de tabla insudb.agreement al 11-23-2001 13:16:09
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+--------------------------------------------------------------------------
	Public sClient As String ' CHAR       14   0     0    N
	Public nCod_Agree As Integer ' NUMBER     22   0     5    N
	Public nQ_draft As Integer ' NUMBER     22   0     5    S
	Public nMax_perc_dcto As Double ' NUMBER     22   2     4    S
	Public dInit_date As Date ' DATE       7    0     0    S
	Public dEnd_date As Date ' DATE       7    0     0    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public sStatregt As String ' CHAR       1    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public nTypeAgree As Integer ' NUMBER     22   0     5    N
	Public nIntermed As Double ' NUMBER     22   0     10   S
	Public nAgency As Double ' NUMBER     22   0     10   S
	Public nType_rec As Integer ' NUMBER     22   0     5    S
	Public sFirstName As String
	Public sLastName As String
	Public sName As String
	Public nposition As Integer
	Public sEmail_Contact As String ' CHAR       60   0     0    S
	Public sPhone_Contact As String ' CHAR       11   0     0    S
	Public nExist As Integer
	
	'+ The variable is defined that contains the state of the each instance of the class.
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	
	Public sCliename As String
	Public sDigit As String
	Public sCliename_contact As String
	Public sDigit_contact As String
	
	'+ Variables para almacenar información del recibo asociado al convenio
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nReceipt As Double
	Public nCurrency As Integer
	Public nPremium As Double
	Public dLimitdate As Date
	Public nStatus_pre As Integer
	Public nContrat As Double
	Public nDraft As Integer
	Public nAmount As Double
	
	'+Variable para almacenar descripcion de códigos
	Public sStatregt_desc As String
	Public sTypeAgree_desc As String
	Public sIntermed_desc As String
	Public sAgency_desc As String
	Public sType_Rec_desc As String
    Public sName_Agree As String

    Public snocollection As String
	
	
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find(ByVal nCod_Agree As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaAgreement As eRemoteDB.Execute
		Dim varrClient() As String
		Dim varrContac() As String
		
		On Error GoTo Find_Err
		
		lreaAgreement = New eRemoteDB.Execute
		
		With lreaAgreement
			.StoredProcedure = "reaAgreementCli"
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				sClient = .FieldToClass("sClient")
				varrClient = Microsoft.VisualBasic.Split(.FieldToClass("sClient_desc"), "|")
				sDigit = varrClient(1)
				sCliename = varrClient(2)
				nCod_Agree = .FieldToClass("nCod_agree")
				nQ_draft = .FieldToClass("nQ_draft")
				nMax_perc_dcto = .FieldToClass("nMax_perc_dcto")
				dInit_date = .FieldToClass("dInit_date")
				dEnd_date = .FieldToClass("dEnd_date")
				sStatregt = .FieldToClass("sStatregt")
				sStatregt_desc = .FieldToClass("sStatregt_Desc")
				nTypeAgree = .FieldToClass("nTypeagree")
				sTypeAgree_desc = .FieldToClass("sTypeAgree_desc")
				nIntermed = .FieldToClass("nIntermed")
				sIntermed_desc = .FieldToClass("sIntermed_desc")
				nAgency = .FieldToClass("nAgency")
				sAgency_desc = .FieldToClass("sAgency_desc")
				nType_rec = .FieldToClass("nType_rec")
				sType_Rec_desc = .FieldToClass("sType_Rec_desc")
				sEmail_Contact = .FieldToClass("sEmail_Contact")
				sPhone_Contact = .FieldToClass("sPhone_Contact")
				sName_Agree = .FieldToClass("sName_Agree")
				
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreement = Nothing
		
	End Function
	
	'**% Find_sClient: It looks for the corresponding data for a client, year and specific concept
	'% Find_sClient: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find_sClient(Optional ByVal Cod_Agree As Integer = 0, Optional ByVal Client As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaAgreementCli As eRemoteDB.Execute
		Dim varrClient() As String
		Dim varrContac() As String
		
		On Error GoTo Find_sClient_Err
		
		lreaAgreementCli = New eRemoteDB.Execute
		
		If Cod_Agree = nCod_Agree And Not lblnFind Then
			Find_sClient = True
		Else
			
			'**+ Definition of parameters for stored procedure 'insudb.reaAgreementCli'
			'**+ read Information 11/01/2000 14:09:20.
			
			'+ Definición de parámetros para stored procedure 'insudb.reaAgreementCli'
			'+ Información leída el 11/01/2000 14:09:20
			
			With lreaAgreementCli
				.StoredProcedure = "reaAgreementCli"
				.Parameters.Add("nCod_Agree", Cod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					sClient = .FieldToClass("sClient")
					varrClient = Microsoft.VisualBasic.Split(.FieldToClass("sClient_desc"), "|")
					sDigit = varrClient(1)
					sCliename = varrClient(2)
					nCod_Agree = .FieldToClass("nCod_agree")
					nQ_draft = .FieldToClass("nQ_draft")
					nMax_perc_dcto = .FieldToClass("nMax_perc_dcto")
					dInit_date = .FieldToClass("dInit_date")
					dEnd_date = .FieldToClass("dEnd_date")
					sStatregt = .FieldToClass("sStatregt")
					sStatregt_desc = .FieldToClass("sStatregt_Desc")
					nTypeAgree = .FieldToClass("nTypeagree")
					sTypeAgree_desc = .FieldToClass("sTypeAgree_desc")
					nIntermed = .FieldToClass("nIntermed")
					sIntermed_desc = .FieldToClass("sIntermed_desc")
					nAgency = .FieldToClass("nAgency")
					sAgency_desc = .FieldToClass("sAgency_desc")
					nType_rec = .FieldToClass("nType_rec")
					sType_Rec_desc = .FieldToClass("sType_Rec_desc")
					sFirstName = .FieldToClass("sFirstName")
					sLastName = .FieldToClass("sLastName")
					sCliename = .FieldToClass("sClienName")
					nposition = .FieldToClass("nPosition")
					sEmail_Contact = .FieldToClass("sEmail_Contact")
					sPhone_Contact = .FieldToClass("sPhone_Contact")
					sName_Agree = .FieldToClass("sName_Agree")
					
					Find_sClient = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_sClient_Err: 
		If Err.Number Then
			Find_sClient = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreementCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreementCli = Nothing
	End Function
	
	'**% Find_sClientOnly: It looks for the corresponding data for a client, year and specific concept
	'% Find_sClientOnly: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function Find_sClientOnly(Optional ByVal Cod_Agree As Integer = 0, Optional ByVal Client As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaAgreementCli As eRemoteDB.Execute
		
		On Error GoTo Find_sClientOnly_Err
		
		If Cod_Agree = nCod_Agree And Client = sClient And Not lblnFind Then
			Find_sClientOnly = True
		Else
			lreaAgreementCli = New eRemoteDB.Execute
			With lreaAgreementCli
				.StoredProcedure = "reaAgreementCliOnly"
				.Parameters.Add("nCod_Agree", Cod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sClient", Client, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					nCod_Agree = .FieldToClass("nCod_agree")
					sClient = .FieldToClass("sClient")
					Find_sClientOnly = True
					.RCloseRec()
				End If
			End With
		End If
		
Find_sClientOnly_Err: 
		If Err.Number Then
			Find_sClientOnly = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreementCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreementCli = Nothing
	End Function
	
	'**% Add: It adds the corresponding data for an agreement of payment by client.
	'% Add: Agrega los datos correspondientes para un convenio de pago por cliente
	Public Function Add() As Boolean
		Dim lreaAgreement As eRemoteDB.Execute
		
		On Error GoTo Add_err
		lreaAgreement = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'insudb.creAgreementCli'
		'**+ read Information 11/01/2000 14:33:46.
		'+ Definición de parámetros para stored procedure 'insudb.creAgreementCli'
		'+ Información leída el 11/01/2000 14:33:46
		With lreaAgreement
			.StoredProcedure = "creAgreementCli"
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_draft", nQ_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_perc_dcto", nMax_perc_dcto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeagree", nTypeAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_rec", nType_rec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastName", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName", sName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosition", nposition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("semail_cont", sEmail_Contact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sphone_cont", sPhone_Contact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_Agree", sName_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("snocollection", snocollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
			Me.nCod_Agree = .Parameters("nCod_agree").Value
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreement = Nothing
		
	End Function
	
	'% Update: It updates the corresponding data for a client, year and specific concept.
	'% Update: Actualiza los datos correspondientes para un cliente, año y concepto específico
	Public Function Update() As Boolean
		Dim lupdAgreement As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lupdAgreement = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'insudb.updAgreementCli'
		'**+ read Information 11/01/2000 14:51:58.
		
		'+ Definición de parámetros para stored procedure 'insudb.updFinanc_cli'
		'+ Información leída el 11/01/2000 14:51:58
		
		With lupdAgreement
			.StoredProcedure = "updAgreementCli"
			
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQ_draft", nQ_draft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_perc_dcto", nMax_perc_dcto, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInit_date", dInit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEnd_date", dEnd_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypeagree", nTypeAgree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgency", nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_rec", nType_rec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFirstName", sFirstName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLastName", sLastName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 19, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName", sName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPosition", nposition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("semail_cont", sEmail_Contact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sphone_cont", sPhone_Contact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_Agree", sName_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("snocollection", snocollection, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lupdAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lupdAgreement = Nothing
		
	End Function
	
	'**% Delete: It eliminates the corresponding data for a client, year and specific concept.
	'% Delete: Elimina los datos correspondientes para un cliente, año y concepto específico
	Public Function Delete() As Boolean
		Dim ldelAgreement As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		
		ldelAgreement = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'insudb.delAgreementCli'
		'**+ read Information 11/01/2000 14:50:44.
		
		'+ Definición de parámetros para stored procedure 'insudb.delFinanc_cli'
		'+ Información leída el 11/01/2000 14:50:44
		
		With ldelAgreement
			.StoredProcedure = "delAgreementCli"
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object ldelAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ldelAgreement = Nothing
		
	End Function
	
	'**% Find: It looks for the corresponding data for a client, year and specific concept.
	'% Find: busca los datos correspondientes para un cliente, año y concepto específico
	Public Function valAgreementCli(Optional ByVal nCod_Agree As Integer = 0, Optional ByVal sClient As String = "", Optional ByVal bFind As Boolean = False) As Boolean
		Dim lreaAgreement As eRemoteDB.Execute
		Dim lintExists As Short
		
		On Error GoTo valAgreementCli_Err
		
		If nCod_Agree = Me.nCod_Agree And sClient = Me.sClient And Not bFind Then
			valAgreementCli = True
		Else
			lreaAgreement = New eRemoteDB.Execute
			With lreaAgreement
				.StoredProcedure = "valAgreementCli"
				.Parameters.Add("nCod_agree", IIf(nCod_Agree = 0, eRemoteDB.Constants.intNull, nCod_Agree), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				.Parameters.Add("sClient", IIf(Trim(sClient) = "", System.DBNull.Value, sClient), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				valAgreementCli = (.Parameters("nExists").Value = 1)
			End With
		End If
		
valAgreementCli_Err: 
		If Err.Number Then
			valAgreementCli = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreement = Nothing
	End Function
	
	'**% insValHeaderMCO505: Validate the header of the page MCO505 - Agreements of payment by client.
	'% insValHeaderMCO505: Se realizan las validaciones del encabezado de la página
	'% MCO505 - Convenios de pago por cliente.
	Public Function insValHeaderMCO505_K(ByVal lstrCodispl As String, ByVal nAction As Integer, ByVal sClient As String) As String
		Dim lobjErrors As Object
		Dim lclsClient As Object
		
		lclsClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		
		On Error GoTo insValHeaderMCO505_K_Err
		
		nExist = 0
		
		If sClient = String.Empty Then
			lobjErrors.ErrorMessage(lstrCodispl, 2001)
		Else
			sClient = lclsClient.ExpandCode(UCase(sClient))
			
			If Not lclsClient.Find(sClient) Then
				'+Se debe indicar si el cliente no existe para llamar a la secuencia de creacion
				lobjErrors.ErrorMessage(lstrCodispl, 1007)
				nExist = 1
			Else
				If nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
					If Not (valAgreementCli( , sClient, True)) Then
						lobjErrors.ErrorMessage(lstrCodispl, 55023)
					End If
				End If
			End If
		End If
		
		insValHeaderMCO505_K = lobjErrors.Confirm
		
		Exit Function
		
insValHeaderMCO505_K_Err: 
		If Err.Number Then
			insValHeaderMCO505_K = CStr(False)
		End If
		
		On Error GoTo 0
	End Function
	
	'**% insValMCO505: Validate the detail of the page MCO505 - Agreements of payment by client.
	'% insValMCO505: Se realizan las validaciones del detalle de la página
	'% MCO505 - Convenios de pago por cliente.
	Public Function insValMCO505(ByVal sCodispl As String, ByVal sAction As String, ByVal sClient As String, ByVal nCod_Agree As Integer, ByVal nQ_draft As Integer, ByVal nMax_perc_dcto As Double, ByVal dInit_date As Date, ByVal dEnd_date As Date, ByVal sStatregt As String, ByVal nTypeAgree As Integer, ByVal nIntermed As Double, ByVal nAgency As Double, ByVal nType_rec As Integer) As String
		Dim lobjErrors As New eFunctions.Errors
		Dim lclsClient As eClient.Client
		
		On Error GoTo insValMCO505_Err
		
		'+ Validaciones del campo "Convenio".
		If nCod_Agree <> eRemoteDB.Constants.intNull Then
			If nCod_Agree = 0 Then
				Call lobjErrors.ErrorMessage(sCodispl, 55004)
			Else
				If sAction = "Add" Then
					If valAgreementCli(nCod_Agree, sClient) Then
						Call lobjErrors.ErrorMessage(sCodispl, 55022)
					Else
						If valAgreementCli(nCod_Agree) Then
							Call lobjErrors.ErrorMessage(sCodispl, 55024)
						End If
					End If
				Else
					'+ Covenio no debe estar siendo usado por ninguna poliza
					If sAction = "Delete" Then
						If Count_Policy(nCod_Agree) Then
							Call lobjErrors.ErrorMessage(sCodispl, 55025)
						End If
					End If
				End If
			End If
		End If
		
		'**+ Validations of the field "Type Aggree".
		'+ Validaciones del campo "Tipo de Convenio".
		If nTypeAgree = 0 Or nTypeAgree = eRemoteDB.Constants.intNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 55095)
		End If
		
		'**+ Validations of the field "Date of beginning".
		'+ Validaciones del campo "Fecha de inicio".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(dInit_date) Or dInit_date = eRemoteDB.Constants.dtmNull Then
			Call lobjErrors.ErrorMessage(sCodispl, 4148)
		End If
		
		'**+ Validations of the field "Date of aim".
		'+ Validaciones del campo "Fecha de fin".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If (dInit_date <> eRemoteDB.Constants.dtmNull And Not IsNothing(dInit_date)) And (dEnd_date <> eRemoteDB.Constants.dtmNull And Not IsNothing(dEnd_date)) Then
			If dInit_date > dEnd_date Then
				Call lobjErrors.ErrorMessage(sCodispl, 55006)
			End If
		End If
		
		'**+ The fields "State" is valid.
		'+ Se valida el campo "Estado".
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
		If IsDbNull(sStatregt) Or IsNothing(sStatregt) Or Trim(sStatregt) = String.Empty Or Trim(sStatregt) = "0" Then
			Call lobjErrors.ErrorMessage(sCodispl, 1922)
		End If
		
		insValMCO505 = lobjErrors.Confirm
		
insValMCO505_Err: 
		If Err.Number Then
			insValMCO505 = CStr(False)
		End If
		
		On Error GoTo 0
	End Function
	
	'**%insPostMCO505: This function is in charge to store the data in the tables, in this
	'**% Agreement case.
	'%insPostMCO505: Esta función se encarga de almacenar los datos en las tablas, en este caso Agreement.
    Public Function insPostMCO505(ByVal sMainAction As String, ByVal lstrClient As String, ByVal llngCod_agree As Integer, ByVal lintQ_draft As Integer, ByVal ldblMax_perc_dcto As Double, ByVal ldtmInit_date As Date, ByVal ldtmEnd_date As Date, ByVal lstrStatregt As String, ByVal llngUsercode As Integer, ByVal nTypeAgree As Integer, ByVal nIntermed As Double, ByVal nAgency As Double, ByVal nType_rec As Integer, ByVal sFirstName As String, ByVal sLastName As String, ByVal sName As String, ByVal nposition As Integer, ByVal lstrmail_cont As String, ByVal lstrphone_cont As String, ByVal sName_Agree As String, Optional ByVal snocollection As String = "0") As Boolean

        Me.sClient = lstrClient
        Me.nCod_Agree = llngCod_agree
        Me.nQ_draft = lintQ_draft
        Me.nMax_perc_dcto = ldblMax_perc_dcto
        Me.dInit_date = ldtmInit_date
        Me.dEnd_date = ldtmEnd_date
        Me.sStatregt = lstrStatregt
        Me.nUsercode = llngUsercode
        Me.nTypeAgree = nTypeAgree
        Me.nIntermed = nIntermed
        Me.nAgency = nAgency
        Me.nType_rec = nType_rec
        Me.sFirstName = sFirstName
        Me.sLastName = sLastName
        Me.sName = sName
        Me.nposition = nposition
        Me.sEmail_Contact = lstrmail_cont
        Me.sPhone_Contact = lstrphone_cont
        Me.sName_Agree = sName_Agree
        Me.snocollection = snocollection

        Select Case sMainAction

            '**+ If the selected option is To register.
            '+ Si la opción seleccionada es Registrar.
            Case "Add"
                '+ Si el numero de convenio esta vacio se cre uno nuevo desde tabla nemerator
                insPostMCO505 = Add()

                '**+ If the selected option is to modify.
                '+ Si la opción seleccionada es Modificar.

            Case "Update"
                insPostMCO505 = Update()

                '**+ If the selected option es To eliminate.
                '+ Si la opción seleccionada es Eliminar.

            Case "Delete"
                insPostMCO505 = Delete()
        End Select
    End Function
	
	'% Find: Busca si existe alguna poliza con el convenio indicado
	Public Function Count_Policy(Optional ByVal Cod_Agree As Integer = 0, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaAgreement As eRemoteDB.Execute
		
		On Error GoTo Count_Policy_Err
		
		lreaAgreement = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.VALPOLICYCOD_AGREE'
		'+ Información leída el 11/01/2000 14:09:20
		
		With lreaAgreement
			.StoredProcedure = "VALPOLICYCOD_AGREE"
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCod_agree", IIf(Cod_Agree = 0, System.DBNull.Value, Cod_Agree), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Count_Policy = (.Parameters("nExists").Value = 1)
			
		End With
		
Count_Policy_Err: 
		If Err.Number Then
			Count_Policy = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaAgreement = Nothing
		
	End Function
	
	'%valAgreementSEF: Verifica si el convenio a tratar es del tipo SEF.
	Public Function valAgreementSEF(ByVal nAgreement As Integer, ByVal nType As Integer) As Boolean
		Dim lrecvalAgreement As eRemoteDB.Execute
		
		On Error GoTo valAgreementSEF_Err
		
		lrecvalAgreement = New eRemoteDB.Execute
		valAgreementSEF = False
		
		'**+Stored procedure parameter definition. 'insudb.valAgreementSEF'
		'**+Data as of 08/11/2000 11:59:57 p.m.
		'+Definición de parámetros para stored procedure 'insudb.valAgreementSEF'
		'+Información leída el 11/08/20  00 11:59:57 p.m.
		
		With lrecvalAgreement
			.StoredProcedure = "reaAgreement_Type"
			.Parameters.Add("nCod_agree", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				If .FieldToClass("NTYPEAGREE") = nType Then
					valAgreementSEF = True
				End If
			End If
		End With
		
valAgreementSEF_Err: 
		If Err.Number Then
			valAgreementSEF = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecvalAgreement may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalAgreement = Nothing
	End Function
	
	'% insvalCOC625: Se realizan las validaciones de los campos de la página
	Public Function insvalCOC625(ByVal sCodispl As String, ByVal nCod_Agree As Integer, ByVal dInit_date As Date, ByVal dEnd_date As Date) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insvalCOC625_err
		
		lobjErrors = New eFunctions.Errors
		
		With lobjErrors
			'+ El código del convenio debe estar lleno
			If nCod_Agree = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55004)
			End If
			
			'+ La fecha desde debe estar llena
			If dInit_date = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 4160)
			End If
			
			'+ La fecha hasta debe estar llena
			If dEnd_date = eRemoteDB.Constants.dtmNull Then
				Call .ErrorMessage(sCodispl, 7164)
			Else
				If dInit_date <> eRemoteDB.Constants.dtmNull Then
					'+ La fecha hasta tiene que ser mayor a la fecha desde
					If dEnd_date <= dInit_date Then
						Call .ErrorMessage(sCodispl, 7165)
					End If
				End If
			End If
			
			insvalCOC625 = .Confirm
		End With
		
insvalCOC625_err: 
		If Err.Number Then
			insvalCOC625 = "insvalCOC625: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
End Class






