Option Strict Off
Option Explicit On
Imports System.Configuration
Public Class Prof_ord
	'%-------------------------------------------------------%'
	'% $Workfile:: Prof_ord.cls                             $%'
    '% $Author:: Nvapla10$%'

	'% $Date:: 31/08/04 6:05p                               $%'
	'% $Revision:: 98                                       $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla insudb.prof_ord al 04-19-2002 09:23:08
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nClaim As Double ' NUMBER     22   0     10   S
	Public nCase_Num As Integer ' NUMBER     22   0     5    S
	Public nDeman_Type As Integer ' NUMBER     22   0     5    S
    Public nCover As Integer ' NUMBER     22   0     5    S
    Public nModulec As Integer ' NUMBER     22   0     5    S
    Public nInspector As Integer
    Public nTransac As Integer ' NUMBER     22   0     5    S
	Public nServ_Order As Double ' NUMBER     22   0     10   N
	Public nAmount As Double ' NUMBER     22   2     14   S
	Public nCurrency As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public dDate_done As Date ' DATE       7    0     0    S
	Public dFec_prog As Date ' DATE       7    0     0    S
	Public dMade_date As Date ' DATE       7    0     0    S
	Public nProvider As Integer ' NUMBER     22   0     5    N
	Public sMade_time As String ' CHAR       5    0     0    S
	Public nStatus_ord As Integer ' NUMBER     22   0     5    S
	Public sTime_prog As String ' CHAR       5    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nWorksh As Integer ' NUMBER     22   0     5    S
	Public nOrdertype As Integer ' NUMBER     22   0     5    S
	Public nNoteorder As Double ' NUMBER     22   0     10   S
	Public nOrdClass As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    N
	Public sCerType As String ' CHAR       1    0     0    S
	Public nBranch As Integer ' NUMBER     22   0     5    S
	Public nProduct As Integer ' NUMBER     22   0     5    S
	Public nPolicy As Double ' NUMBER     22   0     10   S
	Public nCertif As Double ' NUMBER     22   0     10   S
	Public nMunicipality As Integer ' NUMBER     22   0     5    N
	Public nProfgroup As Integer ' NUMBER     22   0     5    S
	Public sPlace As String ' CHAR       30   0     0    S
	Public nFreightage As Double ' NUMBER     22   2     7    S
	Public nNotenum As Double ' NUMBER     22   0     10   S
	Public sMailbag As String ' CHAR       1    0     0    S
	Public nImageNum As Double ' NUMBER     22   0     10   S
	Public sName_Cont As String ' CHAR       60   0     0    S
	Public sAdd_Contact As String ' CHAR       100  0     0    S
	Public sPhone_Cont As String ' CHAR       11   0     0    S
	Public nOrd_typeCost As Integer ' NUMBER     22   0     5    N
	Public dAssigndate As Date ' DATE       7    0     0    S
	Public dInpdate As Date ' DATE       7    0     0    S
	Public sWsdeduc As String
	Public sClient_Case As String
	Public nIVA As Double
	Public nSendCost As Double
	Public nMat_amount As Double
	Public nHand_amount As Double
	Public nDeduc_amount As Double
	Public nDeprec_amount As Double
	
    'Variables para operaciones de Web Service de Ordenes de trabajo
    Public sClientRec As Integer
    Public sDigitRec As String
    Public sFirstNameRec As String
    Public sLastNameRec As String
    Public sLastName2Rec As String

    Public sClientSin As Integer
    Public sDigitSin As String
    Public sFirstNameSin As String
    Public sLastNameSin As String
    Public sLastName2Sin As String

    Public sClientTit As Integer
    Public sDigitTit As String
    Public sFirstNameTit As String
    Public sLastNameTit As String
    Public sLastName2Tit As String

    Public dDecladat As String
    Public sClientComp As Integer
    Public sRegist As String

	'**Auxiliary properties used at SI021
	'- Propiedades auxiliares usadas en SI021
	Public sCase As String
	Public sStaclaim As String
	Public sStaReserve As String
	Public sBrancht As String
	Public sDes_status As String
	Public sDes_branch As String
	Public sDes_product As String
	Public sOrderType As String
	Public nOffice As Integer
	Public sOfficeName As String
	Public nProponum As Integer
	Public sClient_Deman As String
	Public nQuotpart_order As Double
	
    Public sProvider As String
    Public sInspector As String
	'**-Auxiliary properties
	'- Propiedades auxiliares
	Public nAction As Integer
	
	'**- Professional name
	'- Nombre del profesional
	Public sProviderName As String
	
	'**-Workshop name
	'- Nombre del taller
	Public sWorksh As String
    '- variable que indica si la orden tiene información asociada.
    Public bProf_ordSoon As Boolean
    Public nNum_Budget As Integer


    Public Enum eOrdClass
        cstrCertypeProposal = 1 'Propuesta
        cstrCertypePolicy = 2 'Poliza
        cstrCertypeClaim = 3 'Siniestro
	End Enum
	
	Public Enum eServ_orderStatus
        cstrPendingForAssign = 1 'Por asignar
        cstrAssignedNotExecuted = 2 'Asignada No realizada
		cstrExecuted = 3 'Realizada
		cstrPayd = 4 'Pagada
		cstrCancelled = 5 'Anulada
        cstrReturnedNotExecuted = 6 'Devuelta sin ser realizada
        cstrQuotationApproved = 7 'Cotización aceptada
	End Enum
	
	Public Enum eOrderType
        cstrQuotationSupply = 4 'Cotización de repuestos
	End Enum
	
	'- Propiedades auxiliares usadas en OS001
	Private Const cintActionAdd As Short = 1
	Private Const cintActionUpdate As Short = 2
    Private Const cintActionDel As Short = 3

    '- Propiedades necesaria en la implementación del servicio AUDATEX
    Private dOccurdat As Date
    Private sFirstname As String
    Private sLastname As String
    Private sChassis As String
    Private nCapital As Double
    Public sMensajeAUDATEX As String

    Const CT_AUT As String = "AUT"
    Const CT_ATS As String = "ATS"
    Const CT_RJT As String = "RJT"
    Const CT_RJS As String = "RJS"

    Public sWan As String
    Public bEstatusAutorizacion As Boolean


	'**%insValProf_ord: The object of this function is to validate if a record exist in the Prof_ord table
	'%insValProf_ord: El objetivo de esta función es validar si existe un registro en la tabla Prof_ord
	Public Function ValProf_ord(ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nServ_Order As Double, Optional ByVal lblnFind As Boolean = False) As Boolean
		
		'** Define the variable lrecProf_ord to execute the stored procedure
		'Se define la variable lrecProf_ord para ejecutar el stored procedure
		
		Dim lrecProf_ord As eRemoteDB.Execute
		Static llngOldClaim As Double
		Static lintOldCase_num As Integer
		Static lintOldDeman_type As Integer
		Static llngOldServ_order As Double
		Static lblnRead As Boolean
		
		On Error GoTo ValProf_ord_Err
		If llngOldClaim <> nClaim Or lintOldCase_num <> nCase_Num Or lintOldDeman_type <> nDeman_Type Or llngOldServ_order <> nServ_Order Or lblnFind Then
			
			llngOldClaim = nClaim
			lintOldCase_num = nCase_Num
			lintOldDeman_type = nDeman_Type
			llngOldServ_order = nServ_Order
			
			lrecProf_ord = New eRemoteDB.Execute
			
			With lrecProf_ord
				.StoredProcedure = "reaProf_ord_1" 'Listo
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCase", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nDeman", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If (.Run) Then
					lblnRead = True
					
					nStatus_ord = IIf(.FieldToClass("nStatus_ord") = eRemoteDB.Constants.intNull, String.Empty, CShort(.FieldToClass("nStatus_ord")))
					
					.RCloseRec()
				Else
					lblnRead = False
				End If
			End With
			lrecProf_ord = Nothing
		End If
		
		ValProf_ord = lblnRead
ValProf_ord_Err: 
		If Err.Number Then
			ValProf_ord = False
		End If
		On Error GoTo 0
	End Function
	
	'%FindProviderOrder: Busca un proveedor para nZone determinada con máxima prioridad
	'%                   y mínima cantidad de informes pendientes
	Public Function FindProviderOrder(ByVal nZone As Integer, Optional ByVal nProvider As Integer = 0, Optional ByVal sProviderName As String = "") As Boolean
		Dim lrecreaFindProviderOrder As eRemoteDB.Execute
		
		On Error GoTo FindProviderOrder_Err
		
		lrecreaFindProviderOrder = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure inscalprovider 23/04/2002 12:56:00
		'+
		With lrecreaFindProviderOrder
			.StoredProcedure = "inscalprovider"
			With .Parameters
				.Add("nZone", nZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sProviderName", sProviderName, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			If .Run(False) Then
				FindProviderOrder = True
				Me.nProvider = .Parameters("nProvider").Value
				Me.sProviderName = .Parameters("sProviderName").Value
			End If
		End With
		
FindProviderOrder_Err: 
		If Err.Number Then
			FindProviderOrder = False
		End If
		On Error GoTo 0
		lrecreaFindProviderOrder = Nothing
	End Function
	
	'**% Update_ProfOrdGeneric: Makes the update of the "prof_ord" table
	'% Update_ProfOrdGeneric: Realiza las actualizaciones de la tabla "prof_ord"
	Public Function Update_ProfOrdGeneric() As Boolean
		Dim lupdProf_ord As eRemoteDB.Execute
		Dim llngServ_order As Integer
		
		On Error GoTo Update_ProfOrdGeneric_Err
		
		lupdProf_ord = New eRemoteDB.Execute
		
		With lupdProf_ord
			.StoredProcedure = "insProf_ord"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", Me.nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFec_prog", dFec_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime_prog", sTime_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrderType", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteOrder", nNoteorder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWsDeduc", IIf(sWsdeduc = String.Empty, "2", sWsdeduc), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_Cont", sName_Cont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPhone_Cont", sPhone_Cont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdd_Contact", sAdd_Contact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIva", nIVA, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSendCost", nSendCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFreightage", nFreightage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuotpart_order", nQuotpart_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAssigndate", dAssigndate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInspector", nInspector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_ProfOrdGeneric = .Run(False)
			If Update_ProfOrdGeneric Then
				Me.nServ_Order = .Parameters("nServ_order").Value
			End If
		End With
		
Update_ProfOrdGeneric_Err: 
		If Err.Number Then
			Update_ProfOrdGeneric = False
		End If
		On Error GoTo 0
		lupdProf_ord = Nothing
	End Function
	
	'**% Update_Si021: makes the update of the "prof_ord" table for the SI021 form
	'% Update_Si021: Realiza las actualizaciones de la tabla "prof_ord" para la forma SI021
	Public Function Update_Si021() As Object
		Dim lupdProf_ord As eRemoteDB.Execute
		
		On Error GoTo Update_Si021_Err
		lupdProf_ord = New eRemoteDB.Execute
		
		With lupdProf_ord
			.StoredProcedure = "insProf_ord_v"
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMade_date", dMade_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMade_time", sMade_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_Si021 = .Run(False)
		End With
		
		lupdProf_ord = Nothing
		
Update_Si021_Err: 
		If Err.Number Then
			Update_Si021 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% Update_SI021_Upd: makes the update of the table "prof_ord" for the form SI021 when it acts
	'**%                  a PopUp window
	'% Update_SI021_Upd: Realiza las actualizaciones de la tabla "prof_ord" para la forma SI021,
	'%                   cuando se comporta como una ventana PopUp
	Private Function Update_Si021_Upd() As Boolean
		Dim lrecinsProf_ord As eRemoteDB.Execute
		
		On Error GoTo Update_Si021_Upd_Err
		
		lrecinsProf_ord = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.insProf_ord'
		'**+ Data read on 03/30/2001 02:35:34 p.m.
		'+ Definición de parámetros para stored procedure 'insudb.insProf_ord'
		'+ Información leída el 30/03/2001 02:35:34 p.m.
		
		With lrecinsProf_ord
			.StoredProcedure = "insProf_ord"
			.Parameters.Add("nAction", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransac", nTransac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFec_prog", dMade_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime_prog", sMade_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrderType", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteOrder", nNoteorder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWsDeduc", IIf(sWsdeduc = String.Empty, "2", sWsdeduc), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dInpdate", dInpdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_Cont", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPhone_Cont", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdd_Contact", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMunicipality", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIva", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSendCost", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFreightage", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 7, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQuotpart_order", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAssigndate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update_Si021_Upd = .Run(False)
		End With
		lrecinsProf_ord = Nothing
		
Update_Si021_Upd_Err: 
		If Err.Number Then
			Update_Si021_Upd = False
		End If
		On Error GoTo 0
	End Function
	
	'** insValSI011: makes all the validations of the SI011
	'insValSI011: Se realiza todas la validaciones de la SI011
	Public Function insValSI011(ByVal sCodispl As String, ByVal sCase As String, ByVal nServ_Order As Double, ByVal nOrdertype As Short, ByVal nClaim As Double, ByVal nProvider As Integer, ByVal sBrancht As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date, ByVal dFec_prog As Date, ByVal dOldDate_prog As Date, ByVal sOldTime_prog As String, ByVal sTime_prog As String, ByVal nStatus As Integer, ByVal nWorksh As Integer, ByVal sAction As String, ByVal dAssigndate As Date, ByVal valNumber As Integer,  ByVal nCover As Integer) As String
		
		Dim lrecinsValsi011 As eRemoteDB.Execute
		Dim lobjErrors As eFunctions.Errors
		Dim lstrError As String
		
		On Error GoTo insValsi011_Err
		
		lrecinsValsi011 = New eRemoteDB.Execute
		
		
		'+ Definición de store procedure insValsi011 al 07-21-2003 17:08:14
		
		With lrecinsValsi011
			.StoredProcedure = "insSi011pkg.insValsi011"
			.Parameters.Add("sCase", sCase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrdertype", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFec_prog", dFec_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dOlddate_prog", dOldDate_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sOldtime_prog", sOldTime_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime_prog", sTime_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAssigndate", dAssigndate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("valNumber", valNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("aRrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			'+ Las validaciones de seguridad no estan en el procedure por todos los cambios que se
			'+ deben realizar en seguridad
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
					insValSI011 = lobjErrors.Confirm
				End With
				lobjErrors = Nothing
			End If
		End With
		
insValsi011_Err: 
		If Err.Number Then
			insValSI011 = "insValsi011: " & Err.Description
		End If
		lrecinsValsi011 = Nothing
		On Error GoTo 0
	End Function
	
	'**%ChangeDate: Validates that the dates located in the grid do not come from the database and have not
	'**%changed so the validations with today's date are not performed
	Private Function ChangeDate(ByVal dOldDate_prog As Date, ByVal dFec_prog As Date, ByVal sOldTime_prog As String, ByVal sTime_prog As String) As Boolean
		On Error GoTo ChangeDate_Err
		ChangeDate = True
		If dOldDate_prog <> eRemoteDB.Constants.dtmNull Then
			ChangeDate = dFec_prog <> dOldDate_prog
			Exit Function
		End If
		If sOldTime_prog <> String.Empty Then
			ChangeDate = sTime_prog <> sOldTime_prog
		End If
		
ChangeDate_Err: 
		If Err.Number Then
			ChangeDate = False
		End If
	End Function
	
	'**%insPostSI011: This routine is in charge to create the records in the payment orders table
	'%insPostSI011: Esta rutina se encarga de crear los registros en la tabla de ordenes de pago
	Public Function insPostSI011(ByVal nMovement As Integer, ByVal sAction As String, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nServ_Order As Double, ByVal dFec_prog As Date, ByVal nProvider As Integer, ByVal nStatus_ord As String, ByVal sTime_prog As String, ByVal nWorksh As Integer, ByVal nOrdertype As Integer, ByVal nNoteorder As Double, ByVal nUsercode As Integer, ByVal sWsdeduc As String, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dAssigndate As Date, ByVal nInspector As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
		Dim lrecinsPostsi011 As eRemoteDB.Execute
		
		On Error GoTo insPostsi011_Err
		
		lrecinsPostsi011 = New eRemoteDB.Execute
		
		'+ Definición de store procedure insPostsi011 al 07-21-2003 17:04:35
		
		With lrecinsPostsi011
			.StoredProcedure = "insSi011pkg.insPostsi011"
			.Parameters.Add("nMovement", nMovement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFec_prog", dFec_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime_prog", sTime_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrdertype", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteorder", nNoteorder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sWsdeduc", sWsdeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAssigndate", dAssigndate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInspector", nInspector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContent", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			insPostSI011 = .Parameters("nContent").Value = 1
			
		End With
		
insPostsi011_Err: 
		If Err.Number Then
			insPostSI011 = False
		End If
		lrecinsPostsi011 = Nothing
		On Error GoTo 0
	End Function
	
	'**%insValHeader: This function is in charge to validate the entered data into the header form.
	'%insValHeader: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValSI021_k(ByVal sCodispl As String, ByVal nProvider As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProponum As Double, ByVal nCertif As Double, ByVal nClaim As Double, ByVal nOffice As Integer, ByVal nOrdertype As Integer, ByVal nStatus_ord As Integer, ByVal dFec_prog As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lobjTab_provider As eClaim.Tab_Provider
		Dim lobjPolicy As ePolicy.Policy
		Dim lobjCertificat As ePolicy.Certificat
		Dim lobjClaim As eClaim.Claim
		Dim lblnError As Boolean
		Dim lrecInsValSI021_k As eRemoteDB.Execute
		On Error GoTo insValSI021_k_Err
		lclsErrors = New eFunctions.Errors
		Dim lstrError As String
		lobjTab_provider = New eClaim.Tab_Provider
		
		insValSI021_k = CStr(True)
		
		If nProvider = eRemoteDB.Constants.intNull And nBranch = 0 And nProduct = eRemoteDB.Constants.intNull And nPolicy = eRemoteDB.Constants.intNull And nProponum = eRemoteDB.Constants.intNull And nCertif = eRemoteDB.Constants.intNull And nClaim = eRemoteDB.Constants.intNull And nOffice = 0 And nOrdertype = 0 And nStatus_ord = 0 And dFec_prog = eRemoteDB.Constants.dtmNull Then
			'+ Debe existir por lo menos un campo
			Call lclsErrors.ErrorMessage(sCodispl, 60477)
			insValSI021_k = lclsErrors.Confirm
			lobjTab_provider = Nothing
			lclsErrors = Nothing
			Exit Function
		Else
			lrecInsValSI021_k = New eRemoteDB.Execute
			With lrecInsValSI021_k
				.StoredProcedure = "insSi021pkg.insvalsi021_k"
				.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nOrdertype", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dFec_prog", dFec_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Run(False)
				lstrError = .Parameters("Arrayerrors").Value
				
				If lstrError <> String.Empty Then
					lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
				End If
				
			End With
			
		End If
		
		
		insValSI021_k = lclsErrors.Confirm
		
insValSI021_k_Err: 
		If Err.Number Then
			insValSI021_k = "insValSI021_k: " & Err.Description
		End If
		lrecInsValSI021_k = Nothing
		lobjTab_provider = Nothing
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	'**%insValSI021: This function is in charge of validating the entered data into the detail zone for the form.
	'%insValSI021: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValSI021(ByVal sCodispl As String, ByVal nServ_Order As Double, ByVal dMade_date As Date, ByVal sMade_time As String, ByVal nStaclaim As Integer, ByVal nStaReserve As Integer, ByVal dDate_done As Date, ByVal nOrdertype As Integer, ByVal nStatus_ord As Integer) As String
		Dim lrecInsValSI021 As eRemoteDB.Execute
		Dim lobjErrors As eFunctions.Errors
		Dim lstrError As String
		
		On Error GoTo InsValSI021_Err
		
		lrecInsValSI021 = New eRemoteDB.Execute
		With lrecInsValSI021
			.StoredProcedure = "Inssi021pkg.insvalsi021"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMade_date", dMade_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMade_time", sMade_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStaclaim", nStaclaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStaReserve", nStaReserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrdertype", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
					insValSI021 = lobjErrors.Confirm
				End With
				lobjErrors = Nothing
				
			End If
			
		End With
		
		
InsValSI021_Err: 
		If Err.Number Then
			insValSI021 = "InsValSI021: " & Err.Description
		End If
		lrecInsValSI021 = Nothing
		On Error GoTo 0
	End Function
	
	'**%insPostSI011: This routine is incharge to create the records in the payment orders table
	'%insPostSI011: Esta rutina se encarga de crear los registros en la tabla de ordenes de pago
	Public Function insPostSI021(ByVal sWindowType As String, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nDeman_Type As Integer, ByVal nTransac As Integer, ByVal nServ_Order As Double, ByVal dMade_date As Date, ByVal sMade_time As String, ByVal nUsercode As Integer, ByVal nStatus_ord As Integer) As Boolean
		Dim lclsClaim_win As eClaim.Claim_win
		Dim lblnProf_Ord As Boolean
		
		On Error GoTo Prof_Ord_Err
		
		lblnProf_Ord = True
		
		lclsClaim_win = New eClaim.Claim_win
		
		Me.nClaim = nClaim
		Me.nCase_Num = nCase_Num
		Me.nDeman_Type = nDeman_Type
		Me.nTransac = nTransac
		Me.nServ_Order = nServ_Order
		Me.dMade_date = dMade_date
		Me.sMade_time = sMade_time
		Me.nUsercode = nUsercode
		Me.nStatus_ord = nStatus_ord
		
		If sWindowType = "PopUp" Then
			insPostSI021 = Update_Si021_Upd
		End If
		
		lclsClaim_win = Nothing
		
Prof_Ord_Err: 
		If Err.Number Then
			lblnProf_Ord = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find_nServ: se buscan los datos asociados a la orden de servicio
	Public Function Find_nServ(ByVal nServ_Order As Double) As Boolean
		Dim lrecreaprof_ord_o As eRemoteDB.Execute
		On Error GoTo reaprof_ord_o_Err
		lrecreaprof_ord_o = New eRemoteDB.Execute
		
		With lrecreaprof_ord_o
			.StoredProcedure = "reaProf_ord_o"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				Me.nClaim = .FieldToClass("nClaim")
				Me.nCase_Num = .FieldToClass("nCase_num")
				Me.nDeman_Type = .FieldToClass("nDeman_type")
				Me.nTransac = .FieldToClass("nTransac")
				Me.nServ_Order = .FieldToClass("nServ_order")
				Me.nAmount = .FieldToClass("nAmount")
				Me.nCurrency = .FieldToClass("nCurrency")
				Me.dCompdate = .FieldToClass("dCompdate")
				Me.dDate_done = .FieldToClass("dDate_done")
				Me.dFec_prog = .FieldToClass("dFec_prog")
				Me.dMade_date = .FieldToClass("dMade_date")
				Me.nProvider = .FieldToClass("nProvider")
				Me.sMade_time = .FieldToClass("sMade_time")
				Me.nStatus_ord = .FieldToClass("nStatus_ord")
				Me.sTime_prog = .FieldToClass("sTime_prog")
				Me.nUsercode = .FieldToClass("nUsercode")
				Me.nWorksh = .FieldToClass("nWorksh")
				Me.nOrdertype = .FieldToClass("nOrdertype")
				Me.nNoteorder = .FieldToClass("nNoteorder")
				Me.nOrdClass = .FieldToClass("nOrdclass")
				Me.sClient = .FieldToClass("sClient")
				Me.sCerType = .FieldToClass("sCertype")
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nPolicy = .FieldToClass("nPolicy")
				Me.nCertif = .FieldToClass("nCertif")
				Me.nMunicipality = .FieldToClass("nMunicipality")
				Me.nProfgroup = .FieldToClass("nProfgroup")
				Me.sPlace = .FieldToClass("sPlace")
				Me.nFreightage = .FieldToClass("nFreightage")
				Me.sMailbag = .FieldToClass("sMailbag")
				Me.dAssigndate = .FieldToClass("dAssignDate")
				Me.dInpdate = .FieldToClass("dInpdate")
				Me.nImageNum = .FieldToClass("nImageNum")
				Me.nNotenum = .FieldToClass("nNoteNum")
				Me.sWsdeduc = .FieldToClass("sWsDeduc")
				Me.nIVA = .FieldToClass("nIva")
				Me.nSendCost = .FieldToClass("nSendCost")
				Me.nFreightage = .FieldToClass("nFreightage")
				Me.sName_Cont = .FieldToClass("sName_Cont")
				Me.sAdd_Contact = .FieldToClass("sAdd_Contact")
				Me.sPhone_Cont = .FieldToClass("sPhone_Cont")
				Me.nOrd_typeCost = .FieldToClass("nOrd_typeCost")
				Me.nMat_amount = .FieldToClass("nMat_amount")
				Me.nHand_amount = .FieldToClass("nHand_amount")
				Me.nDeduc_amount = .FieldToClass("nDeduc_amount")
				Me.nDeprec_amount = .FieldToClass("nDeprec_amount")
				Me.sProvider = .FieldToClass("sProvider")
                Me.sWorksh = .FieldToClass("sWorksh")
                Me.sInspector = .FieldToClass("sInspector")
                Me.nInspector = .FieldToClass("nInspector")
                Me.nModulec = .FieldToClass("nModulec")
                Me.nCover = .FieldToClass("nCover")
				Find_nServ = True
				.RCloseRec()
			End If
		End With
		
reaprof_ord_o_Err: 
		If Err.Number Then
			Find_nServ = False
		End If
		lrecreaprof_ord_o = Nothing
		On Error GoTo 0
	End Function
	
	'%insValOS001_k: Esta función se encarga de validar los datos del encabezado
	'% de la transacción Solicitud de ordenes de servicios (OS001)
	Public Function insValOS001_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal nOrdClass As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProponum As Double, ByVal nCertif As Double, ByVal nClaim As Double, ByVal nCase_Num As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
        Dim lobjPolicy As ePolicy.Policy = New ePolicy.Policy
        Dim lobjClaim_his As eClaim.Claim_his
		
		Dim lstrSep As String
        Dim lstrError As String = ""

        On Error GoTo insValOS001_k_Err
		
		lstrSep = "||"
		
		lclsErrors = New eFunctions.Errors
		lobjClaim_his = New eClaim.Claim_his
		
		lblnError = False
		
		'+ Validación Origen de la Orden de servicio
		With lclsErrors
			If nOrdClass <= 0 Then
				lblnError = True
				'Call .ErrorMessage(sCodispl, 55676)
				lstrError = lstrError & lstrSep & "55676"
			Else
				
				'+ Validación Ramo
				If nOrdClass = 1 Or nOrdClass = 2 Then
					If nBranch <= 0 Then
						lblnError = True
						'Call .ErrorMessage(sCodispl, 1022)
						lstrError = lstrError & lstrSep & "1022"
					End If
				End If
				
				'+ Validación Producto
				If nOrdClass = 1 Or nOrdClass = 2 Then
					If nProduct <= 0 Then
						lblnError = True
						'Call .ErrorMessage(sCodispl, 1014)
						lstrError = lstrError & lstrSep & "1014"
					End If
				End If
				
				'+ Validación Propuesta
				If nOrdClass = 1 Then
					If nProponum <= 0 Then
						lblnError = True
						'Call .ErrorMessage(sCodispl, 55677)
						lstrError = lstrError & lstrSep & "55677"
					End If
				End If
				
				'+ Validación Poliza
				If nOrdClass = 2 Then
					If nPolicy <= 0 Then
						lblnError = True
						'Call .ErrorMessage(sCodispl, 3003)
						lstrError = lstrError & lstrSep & "3003"
					End If
				End If
				
				'+ Si la Propuesta o Póliza es colectiva, el certificado debe estar lleno
				If nOrdClass = 1 Or nOrdClass = 2 Then
					If Not lblnError Then
						If lobjPolicy Is Nothing Then
							lobjPolicy = New ePolicy.Policy
							With lobjPolicy
								If Not .Find(IIf(nOrdClass = 1, "1", "2"), nBranch, nProduct, IIf(nOrdClass = 1, nProponum, nPolicy)) Then
									'+ Si el origen es "propuesta" y no se consiguió como "propuesta de emisión" se busca como "propuesta de endoso"
									If nOrdClass = 1 Then
										If Not .Find("6", nBranch, nProduct, nProponum) Then
											'+ Si el origen es "propuesta" y no se consiguió como "propuesta de endoso" se busca como "propuesta de renovación" si no se consigue registro
											'+ se envía validación que no existe
											If Not .Find("7", nBranch, nProduct, nProponum) Then
												'Call lclsErrors.ErrorMessage(sCodispl, 55683)
												lstrError = lstrError & lstrSep & "55683"
											End If
										End If
									Else
										'Call lclsErrors.ErrorMessage(sCodispl, 3001)
										lstrError = lstrError & lstrSep & "3001"
									End If
								Else
									If .sPolitype = "2" Then
										If nCertif <= 0 Then
											'Call lclsErrors.ErrorMessage(sCodispl, 3006)
											lstrError = lstrError & lstrSep & "3006"
										End If
									End If
								End If
							End With
						End If
					End If
				End If
				
				'+ Validación Siniestro
				If nOrdClass = 3 Then ' ************ Siniestro
					If nClaim <= 0 Then
						lblnError = True
						'Call .ErrorMessage(sCodispl, 4006)
						lstrError = lstrError & lstrSep & "4006"
					Else
						'+ Validación Caso
						If nCase_Num <= 0 Then
							lblnError = True
							'Call .ErrorMessage(sCodispl, 4310)
							lstrError = lstrError & lstrSep & "4310"
						End If
						
						If Not lobjClaim_his.FindMovReserv(nClaim) Then
							lblnError = True
							lstrError = lstrError & lstrSep & "56198"
						Else
							If Not lobjClaim_his.FindMovReservCase(nClaim, nCase_Num) Then
								lblnError = True
								lstrError = lstrError & lstrSep & "750122"
							End If
						End If
						
					End If
				End If
			End If
			'insValOS001_k = lclsErrors.Confirm
			If lstrError <> String.Empty Then
				lstrError = Mid(lstrError, 3)
				lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrError)
				insValOS001_k = lclsErrors.Confirm
			End If
			
		End With
		
insValOS001_k_Err: 
		If Err.Number Then
			insValOS001_k = "insValOS001_k: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	'% insValOS001: Esta función se encarga de validar los datos de la Forma
	'% de la transacción Solicitud de ordenes de servicios (OS001)
	Public Function insValOS001(ByVal sCodispl As String, ByVal sAction As String, ByVal nOrdClass As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProponum As Integer, ByVal nCertif As Integer, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nServ_Order As Double, ByVal nProvider As Integer, ByVal dAssigndate As Date, ByVal dFec_prog As Date, ByVal sTime_prog As String, ByVal sPlace As String, ByVal nWorksh As Integer, ByVal nMunicipality As Integer, ByVal sName_Cont As String, ByVal sAdd_Contact As String, ByVal sPhone_Cont As String, ByVal nStatus_ord As Integer, ByVal nOrd_typeCost As Integer, ByVal nOrdertype As Integer, ByVal nNotenum As Integer, ByVal tcdMade_date As Date, ByVal tctMade_time As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		Dim lclsClaim As Claim
		Dim lobjPolicy As ePolicy.Policy
		On Error GoTo insValOS001_Err
		
		lclsErrors = New eFunctions.Errors
		lclsClaim = New Claim
		lobjPolicy = New ePolicy.Policy
		
		'+ Fecha de planificación igual o posterior a fecha fecha de asignación
		With lclsErrors
			
			'+Si la línea esta seleccionada para "realizarla", se validan la fecha y hora de realizacion que deben estar llenas
			If nStatus_ord = 3 Then
				If tcdMade_date = eRemoteDB.Constants.dtmNull Then
					Call .ErrorMessage(sCodispl, 4165)
				End If
				
				If tctMade_time = String.Empty Or tctMade_time = "00:00" Then
					Call .ErrorMessage(sCodispl, 4166)
				End If
			End If
			
			If dAssigndate <> eRemoteDB.Constants.dtmNull And dFec_prog <> eRemoteDB.Constants.dtmNull Then
				If dFec_prog < dAssigndate Then
					Call .ErrorMessage(sCodispl, 55678)
				End If
			End If
			
			'+ Validación Estado
			If nStatus_ord = 0 Or nStatus_ord = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 4304)
			End If
			
			'+ Validación Tipo de inspección (para establecimiento de costos)
			If nOrd_typeCost = 0 Or nOrd_typeCost = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 55850)
			End If
			
			'+ Validación Tipo de inspección (para siniestros de automovil)
			If nBranch = 1 Then
				If nOrdertype = 0 Or nOrd_typeCost = nOrdertype Then
					Call .ErrorMessage(sCodispl, 55851)
				End If
			End If
			
			If nClaim > 0 Then
				If lclsClaim.Find(nClaim, True) Then
					If dFec_prog < lclsClaim.dDecladat And dFec_prog <> eRemoteDB.Constants.dtmNull Then
						Call .ErrorMessage(sCodispl, 56197)
					End If
					If dAssigndate < lclsClaim.dDecladat And dAssigndate <> eRemoteDB.Constants.dtmNull Then
						Call .ErrorMessage(sCodispl, 56197)
					End If
				End If
			Else
				If nProponum > 0 Then
					If Not lobjPolicy.Find("1", nBranch, nProduct, nProponum) Then
						'+ Si el origen es "propuesta" y no se consiguió como "propuesta de emisión" se busca como "propuesta de endoso"
						If Not lobjPolicy.Find("6", nBranch, nProduct, nProponum) Then
							'+ Si el origen es "propuesta" y no se consiguió como "propuesta de endoso" se busca como "propuesta de renovación" si no se consigue registro
							'+ se envía validación que no existe
							If lobjPolicy.Find("7", nBranch, nProduct, nProponum) Then
								If dFec_prog < lobjPolicy.dDate_origi And dFec_prog <> eRemoteDB.Constants.dtmNull Then
									Call .ErrorMessage(sCodispl, 56196)
								End If
								If dAssigndate < lobjPolicy.dDate_origi And dAssigndate <> eRemoteDB.Constants.dtmNull Then
									Call .ErrorMessage(sCodispl, 56196)
								End If
							End If
						Else
							If dFec_prog < lobjPolicy.dDate_origi And dFec_prog <> eRemoteDB.Constants.dtmNull Then
								Call .ErrorMessage(sCodispl, 56196)
							End If
							If dAssigndate < lobjPolicy.dDate_origi And dAssigndate <> eRemoteDB.Constants.dtmNull Then
								Call .ErrorMessage(sCodispl, 56196)
							End If
						End If
					Else
						If dFec_prog < lobjPolicy.dDate_origi And dFec_prog <> eRemoteDB.Constants.dtmNull Then
							Call .ErrorMessage(sCodispl, 56196)
						End If
						If dAssigndate < lobjPolicy.dDate_origi And dAssigndate <> eRemoteDB.Constants.dtmNull Then
							Call .ErrorMessage(sCodispl, 56196)
						End If
					End If
				End If
			End If
			
			'+ Si el estado es "Asignada-No realizada, debe indicar código del proveedor
			If (nProvider = eRemoteDB.Constants.intNull Or nProvider = 0) And nStatus_ord = 2 Then
				Call .ErrorMessage(sCodispl, 767027)
			End If
			
			insValOS001 = lclsErrors.Confirm
		End With
		
insValOS001_Err: 
		If Err.Number Then
			insValOS001 = "insValOS001: " & Err.Description
		End If
		On Error GoTo 0
		
		lclsErrors = Nothing
		lclsClaim = Nothing
		lobjPolicy = Nothing
		
	End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add_OS001() As Boolean
		Add_OS001 = InsUpdOS001(cintActionAdd)
	End Function
	
	'%Update: Actualiza los datos de la tabla
	Public Function Update_OS001() As Boolean
		Update_OS001 = InsUpdOS001(cintActionUpdate)
	End Function
	
	'%Delete: Borra los datos de la tabla
	Public Function Delete_OS001() As Boolean
		Delete_OS001 = InsUpdOS001(cintActionDel)
	End Function
	
	'%InsPostOS001Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (OS001)
	Public Function InsPostOS001Upd(ByVal sAction As String, ByVal nOrdClass As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProponum As Integer, ByVal nCertif As Integer, ByVal nClaim As Double, ByVal nCase_Num As Integer, ByVal nServ_Order As Double, ByVal nProvider As Integer, ByVal dAssigndate As Date, ByVal dFec_prog As Date, ByVal sTime_prog As String, ByVal sPlace As String, ByVal nWorksh As Integer, ByVal nMunicipality As Integer, ByVal sName_Cont As String, ByVal sAdd_Contact As String, ByVal sPhone_Cont As String, ByVal nStatus_ord As Integer, ByVal nOrd_typeCost As Integer, ByVal nOrdertype As Integer, ByVal nNotenum As Integer, ByVal nDeman_Type As Integer, ByVal nUsercode As Integer, ByVal tcdMade_date As Date, ByVal tctMade_time As String) As Boolean
		Dim lintAction As Integer
		
		On Error GoTo InsPostOS001Upd_Err
		
		With Me
			.nOrdClass = nOrdClass
			
			Select Case nOrdClass
				Case 1
					.sCerType = CStr(eOrdClass.cstrCertypeProposal)
					nPolicy = nProponum
				Case 2
					.sCerType = CStr(eOrdClass.cstrCertypePolicy)
				Case 3
					.sCerType = CStr(eOrdClass.cstrCertypePolicy) 'cstrCertypeClaim
			End Select
			
			.nBranch = nBranch
			.nProduct = nProduct
			.nPolicy = nPolicy
			.nCertif = nCertif
			.nClaim = nClaim
			.nCase_Num = nCase_Num
			.nServ_Order = nServ_Order
			.nProvider = nProvider
			.dAssigndate = dAssigndate
			.dFec_prog = dFec_prog
			.sTime_prog = sTime_prog
			.sPlace = Mid(sPlace, 1, 50)
			.nWorksh = nWorksh
			.nMunicipality = nMunicipality
			.sName_Cont = Mid(sName_Cont, 1, 60)
			.sAdd_Contact = Mid(sAdd_Contact, 1, 100)
			.sPhone_Cont = sPhone_Cont
			.nStatus_ord = nStatus_ord
			.nOrd_typeCost = nOrd_typeCost
			.nOrdertype = nOrdertype
			.nNoteorder = nNotenum
			.nUsercode = nUsercode
			.nDeman_Type = nDeman_Type
			.dMade_date = tcdMade_date
			.sMade_time = tctMade_time
			
			If sAction = "Del" Then
				lintAction = cintActionDel
			Else
				If sAction = "Update" Then
					lintAction = cintActionUpdate
				Else
					If sAction = "Add" Then
						lintAction = cintActionAdd
					End If
				End If
			End If
			
			Select Case lintAction
				Case 1
					'+ Se crea el registro
					InsPostOS001Upd = .Add_OS001
					
					'+ Se modifica el registro
				Case 2
					InsPostOS001Upd = .Update_OS001
					
					'+ Se elimina el registro
				Case 3
					InsPostOS001Upd = .Delete_OS001
					
			End Select
		End With
		
InsPostOS001Upd_Err: 
		If Err.Number Then
			InsPostOS001Upd = False
		End If
		On Error GoTo 0
	End Function
	'% insValOS590: Esta función se encarga de validar los datos de la Forma
	'% de la transacción Datos generales de la orden de servicio (OS590)
	Public Function insValOS590(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nServ_Order As Double) As String
		
		'+ Se definen los objetos para el manejo de las clases
		Dim lclsErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim lclsProduct As Object
		Dim lclsPolicy As Object
		
		lclsErrors = New eFunctions.Errors
		lclsProduct = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Product")
		lclsPolicy = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy")
		
		On Error GoTo insValOS590_Err
		
		If nServ_Order <= 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 11146)
		Else
			If Find_nServ(nServ_Order) Then
				'+ Validación Estado
				With lclsErrors
					If nAction <> 401 Then
						If nStatus_ord = 5 Then
							Call .ErrorMessage(sCodispl, 4255)
						End If
						If nStatus_ord = 3 Or nStatus_ord = 4 Then
							Call .ErrorMessage(sCodispl, 4120)
						End If
					End If
					'+ Validación del tipo de producto
					Call lclsProduct.FindProdMaster(nBranch, nProduct)
					Call lclsPolicy.Find_TabNameB(nBranch)
					
                    If Trim(lclsProduct.sBrancht) <> "3" And Trim(lclsProduct.sBrancht) <> "6" Then
						If lclsPolicy.sTabname <> "FIRE" Then
							Call lclsErrors.ErrorMessage(sCodispl, 55930)
						End If
					End If
				End With
			Else
				Call lclsErrors.ErrorMessage(sCodispl, 4056)
			End If
		End If
		
		insValOS590 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
insValOS590_Err: 
		If Err.Number Then
			insValOS590 = "insValOS590: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	'%InsPostOS590Upd: Esta función realiza los cambios de BD según especificaciones funcionales
	'%                 de la transacción (OS590)
	Public Function InsPostOS590Upd(ByVal nServ_Order As Double, ByVal dMade_date As Date, ByVal sPlace As String, ByVal nMunicipality As Integer, ByVal nStatus_ord As Integer, ByVal nUsercode As Integer, ByVal nImageNum As Integer, ByVal nNotenum As Integer) As Boolean
		Dim lrecInsUpdOS590 As eRemoteDB.Execute
		
		On Error GoTo InsPostOS590Upd_Err
		
		lrecInsUpdOS590 = New eRemoteDB.Execute
		
		
		'+ Definición de parámetros para stored procedure 'InsUpdProf_ord_O'
		'+ Información leída el 10/04/2002
		
		With lrecInsUpdOS590
			.StoredProcedure = "InsUpdProf_ord_O"
			.Parameters.Add("nserv_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMade_date", dMade_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPlace", sPlace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nImageNum", nImageNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nnotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsPostOS590Upd = .Run(False)
		End With
		
		
InsPostOS590Upd_Err: 
		If Err.Number Then
			InsPostOS590Upd = False
		End If
		On Error GoTo 0
	End Function
	
	
	'%InsUpdOS001: Realiza la actualización de la tabla
	Private Function InsUpdOS001(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdOS001 As eRemoteDB.Execute
		
		On Error GoTo InsUpdOS001_Err
		
		lrecInsUpdOS001 = New eRemoteDB.Execute
		
		
		'+ Definición de parámetros para stored procedure 'InsUpdProf_ord_2'
		'+ Información leída el 10/04/2002
		
		With lrecInsUpdOS001
			.StoredProcedure = "InsUpdProf_ord_2"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrdClass", nOrdClass, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_Num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dFec_prog", dFec_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dAssignDate", dAssigndate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nStatus_ord", nStatus_ord, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sTime_prog", sTime_prog, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrderType", nOrdertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCertype", sCerType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPlace", sPlace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrd_TypeCost", nOrd_typeCost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sName_cont", sName_Cont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAdd_contact", sAdd_Contact, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPhone_cont", sPhone_Cont, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 11, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNoteorder", nNoteorder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dMade_date", dMade_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMade_time", sMade_time, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			
			InsUpdOS001 = .Run(False)
		End With
		
InsUpdOS001_Err: 
		If Err.Number Then
			InsUpdOS001 = False
		End If
		lrecInsUpdOS001 = Nothing
		On Error GoTo 0
	End Function
	
	Private Sub Class_Initialize_Renamed()
		nOrdClass = eRemoteDB.Constants.intNull
		nServ_Order = eRemoteDB.Constants.intNull
		nClaim = eRemoteDB.Constants.intNull
		nCase_Num = eRemoteDB.Constants.intNull
		nDeman_Type = eRemoteDB.Constants.intNull
		dFec_prog = eRemoteDB.Constants.dtmNull
		dAssigndate = eRemoteDB.Constants.dtmNull
		nProvider = eRemoteDB.Constants.intNull
		nStatus_ord = eRemoteDB.Constants.intNull
		sTime_prog = CStr(eRemoteDB.Constants.strNull)
		nUsercode = eRemoteDB.Constants.intNull
		nWorksh = eRemoteDB.Constants.intNull
		nOrdertype = eRemoteDB.Constants.intNull
		sCerType = CStr(eRemoteDB.Constants.strNull)
		sCase = CStr(eRemoteDB.Constants.strNull)
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nPolicy = eRemoteDB.Constants.intNull
		nCertif = eRemoteDB.Constants.intNull
		nMunicipality = eRemoteDB.Constants.intNull
		sPlace = CStr(eRemoteDB.Constants.strNull)
		nOrd_typeCost = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		sName_Cont = CStr(eRemoteDB.Constants.strNull)
		sAdd_Contact = CStr(eRemoteDB.Constants.strNull)
		sPhone_Cont = CStr(eRemoteDB.Constants.strNull)
		sWorksh = CStr(eRemoteDB.Constants.strNull)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%valServ_order: El objetivo de esta función es verificar si es válido en numero de la orden a incluir
	Public Function valServ_order(ByVal nServ_Order As Double) As Boolean
		
		'Se define la variable lrecServ_order para ejecutar el stored procedure
		
		Dim lrecServ_order As eRemoteDB.Execute
		
		On Error GoTo valServ_order_Err
		
		lrecServ_order = New eRemoteDB.Execute
		
		With lrecServ_order
			.StoredProcedure = "reaProf_ord_3"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				valServ_order = False
				.RCloseRec()
			Else
				valServ_order = True
			End If
		End With
		lrecServ_order = Nothing
		
valServ_order_Err: 
		If Err.Number Then
			valServ_order = False
		End If
		On Error GoTo 0
	End Function
	
	'%LoadTabs: Arma la secuencia para las Ordenes de servicio segun el ramo
	Public Function LoadTabsProf_ord(ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nBranch As Integer, ByVal nServ_Order As Double, ByVal nProduct As Integer, ByVal sCerType As String, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal dMade_date As Date = #12:00:00 AM#) As Object
		Const CN_BRANCH_AUTO As String = "OS590   AU001   OS591   CA010   CA012   SCA649  SCA593  "
		Const CN_BRANCH_FIRE As String = "OS590   IN010   OS592_1 OS592_2 OS592_3 OS592_4 OS592_5 CA010   CA012   SCA649  SCA593  "
		Dim lrecWindows As eRemoteDB.Query
		Dim lclsSecurSche As eSecurity.Secur_sche
		Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lclsProduct As eProduct.Product
		Dim lclsPolicy As ePolicy.Policy
		Dim lobjTables As Object
		Dim lintCountWindows As Integer
        Dim lstrCodisp As String = ""
        Dim lstrCodispl As String
        Dim lstrShort_desc As String = ""
        Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		Dim lstrHTMLCode As String
		Dim lclsSequence As eFunctions.Sequence
		Dim lstrWindows As String
		Dim ldblnotenum As Double
		Dim ldblImageNum As Double
		
		On Error GoTo LoadTabsProf_ord_Err
		
		lclsSecurSche = New eSecurity.Secur_sche
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		lclsProduct = New eProduct.Product
		lclsPolicy = New ePolicy.Policy
		
		lstrHTMLCode = String.Empty
		Call lclsProduct.FindProdMaster(nBranch, nProduct)
		If Trim(CStr(lclsProduct.sBrancht)) = "3" Then
			lstrWindows = CN_BRANCH_AUTO
		Else
			lclsPolicy.Find_TabNameB(nBranch)
			If lclsPolicy.sTabname = "FIRE" Then
				lstrWindows = CN_BRANCH_FIRE
			Else
				lstrWindows = String.Empty
			End If
		End If
		lblnRequired = True
		
		lstrHTMLCode = lclsSequence.makeTable
		lintCountWindows = 1
		lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		
		Do While Trim(lstrCodispl) <> String.Empty
			lblnContent = False
			lblnRequired = False
			lstrCodispl = Trim(lstrCodispl)
			If lstrCodispl = "OS591" Or lstrCodispl = "OS592_1" Then
				lblnRequired = True
			End If
			
			'+ Se asignan los valores a las variables de descripción
			If lrecWindows.OpenQuery("Windows", "sCodisp, sShort_des", "sCodispl='" & lstrCodispl & "'") Then
				lstrCodisp = lrecWindows.FieldToClass("sCodisp")
				lstrShort_desc = lrecWindows.FieldToClass("sShort_des")
				lrecWindows.CloseQuery()
			End If
			
			'+ Se busca la imagen a colocar en los links
			With lclsSecurSche
				If Not .valTransAccess(sUserSchema, lstrCodisp, "1") Then
					If lblnContent Then
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
					Else
						If lblnRequired Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
						End If
					End If
				Else
					
					'+ Se verifica contenido de las ventanas
					Select Case lstrCodispl
						
						'+ OS590: Datos generales de la orden de servicio
						Case "OS590"
							lblnContent = Find_nServ(nServ_Order)
							ldblnotenum = Me.nNotenum
							ldblImageNum = Me.nImageNum
							
							'+ OS591: Daños codificados - FULLCAR
						Case "OS591"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Auto_damages")
							lblnContent = lobjTables.Find(nServ_Order)
							lobjTables = Nothing
							
							'+ OS592_1: Características de construcción - FULLHOUSE
						Case "OS592_1"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Construction")
							lblnContent = lobjTables.Find(nServ_Order)
							lobjTables = Nothing
							
							'+ OS592_2: Riesgo de incendio
						Case "OS592_2"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Fire_risk")
							lblnContent = lobjTables.Find(nServ_Order)
							lobjTables = Nothing
							
							'+ OS592_3: Riesgo de robo
						Case "OS592_3"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Theft_risk")
							lblnContent = lobjTables.Find(nServ_Order)
							lobjTables = Nothing
							
							'+ OS592_4: Riesgos adicionales
						Case "OS592_4"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Add_risk")
							lblnContent = lobjTables.Find(nServ_Order)
							lobjTables = Nothing
							
							'+ OS592_5: Colindancia
						Case "OS592_5"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Adjacence")
							lblnContent = lobjTables.Find(nServ_Order)
							lobjTables = Nothing
							
							'+ CA010: Bienes asegurados de la póliza o certificado
						Case "CA010"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Propertys")
							lblnContent = lobjTables.Find(sCerType, nBranch, nProduct, nPolicy, nCertif, dMade_date)
							lobjTables = Nothing
							
							'+ CA012: Elementos de protección
						Case "CA012"
							If Find_nServ(nServ_Order) Then
								lobjTables = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Protection")
								
								With lobjTables
									
									'+ Se asignan a las propiedades para buscar los elementos de proteccion
									.sCerType = sCerType
									.nBranch = Me.nBranch
									.nProduct = Me.nProduct
									.nPolicy = nPolicy
									.nCertif = nCertif
									
									lblnContent = .Find()
								End With
								
								lobjTables = Nothing
							End If
							
							'+ SCA649: Notas del informe de inspección
						Case "SCA649"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Notess")
							lblnContent = lobjTables.Find(ldblnotenum)
							lobjTables = Nothing
							
							'+ SCA593: Imágenes del informe de inspección
						Case "SCA593"
							lobjTables = eRemoteDB.NetHelper.CreateClassInstance("eGeneralForm.Imagess")
							lblnContent = lobjTables.Find(ldblImageNum)
							lobjTables = Nothing
					End Select
					
					If Not lblnContent Then
						If lblnRequired Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						End If
					Else
						mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
					End If
				End If
			End With
			lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
			'+ Se mueve al siguiente registro encontrado
			lintCountWindows = lintCountWindows + 8
			lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
		Loop 
		
		lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		
		LoadTabsProf_ord = lstrHTMLCode
		
LoadTabsProf_ord_Err: 
		If Err.Number Then
			LoadTabsProf_ord = "LoadTabsProf_ord: " & Err.Description
		End If
		On Error GoTo 0
		lclsProduct = Nothing
		lclsPolicy = Nothing
		lclsSecurSche = Nothing
		lrecWindows = Nothing
		lclsSequence = Nothing
		lobjTables = Nothing
	End Function
	
	'%LoadTabs: Arma la secuencia para las Ordenes de servicio segun el ramo
	Function valconstraintsi011(ByVal nServ_Order As Double) As Boolean
		Dim lrecvalConstraintprof_ord As eRemoteDB.Execute
		
		On Error GoTo valConstraintprof_ord_Err
		
		lrecvalConstraintprof_ord = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure valConstraintprof_ord al 11-19-2003 16:26:22
		'+
		With lrecvalConstraintprof_ord
			.StoredProcedure = "valConstraintprof_ord"
			.Parameters.Add("nServ_order", nServ_Order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			valconstraintsi011 = .Run(False)
			If valconstraintsi011 Then
				bProf_ordSoon = .Parameters("nCount").Value <> 0
			End If
		End With
		
valConstraintprof_ord_Err: 
		If Err.Number Then
			valconstraintsi011 = False
		End If
		lrecvalConstraintprof_ord = Nothing
		On Error GoTo 0
    End Function


    Public Function Find_webServiceInfo(ByVal nClaim As Double) As Boolean
        Dim lfindInfo As eRemoteDB.Execute
        On Error GoTo reaprof_ord_o_Err
        lfindInfo = New eRemoteDB.Execute

        With lfindInfo
            .StoredProcedure = "reaclaimwsinfo"
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run() Then

                sClientRec = .FieldToClass("sClientRec")
                sDigitRec = .FieldToClass("sDigitRec")
                sFirstNameRec = .FieldToClass("sFirstNameRec")
                sLastNameRec = .FieldToClass("sLastNameRec")
                sLastName2Rec = .FieldToClass("sLastName2Rec")

                sClientSin = .FieldToClass("sClientSin")
                sDigitSin = .FieldToClass("sDigitSin")
                sFirstNameSin = .FieldToClass("sFirstNameSin")
                sLastNameSin = .FieldToClass("sLastNameSin")
                sLastName2Sin = .FieldToClass("sLastName2Sin")

                sClientTit = .FieldToClass("sClientTit")
                sDigitTit = .FieldToClass("sDigitTit")
                sFirstNameTit = .FieldToClass("sFirstNameTit")
                sLastNameTit = .FieldToClass("sLastNameTit")
                sLastName2Tit = .FieldToClass("sLastName2Tit")

                dDecladat = .FieldToClass("dDecladat")

                nPolicy = .FieldToClass("nPolicy")
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                sRegist = .FieldToClass("sRegist")

                Find_webServiceInfo = True
                .RCloseRec()

            End If
        End With

reaprof_ord_o_Err:
        If Err.Number Then
            Find_webServiceInfo = False
        End If
        lfindInfo = Nothing
        On Error GoTo 0
    End Function
    '%GetWanForAudatex: Se obtiene el código wan de identificación del expediente (orden de servicio) dentro del sistema audatex dada una orden de servicio
    '---------------------------------------------------------------------------------------------------------
    Private Sub GetWanForAudatex(ByVal nServ_order As Integer)
        '---------------------------------------------------------------------------------------------------------
        Dim lreGetwanforaudatex As eRemoteDB.Execute

        lreGetwanforaudatex = New eRemoteDB.Execute

        With lreGetwanforaudatex
            .StoredProcedure = "INTEGRATIONREQINSPECTIONPKG.GETWANFORAUDATEX"
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sWan = .FieldToClass("sWan")
                sClient = .FieldToClass("sClient")
            End If
        End With

        lreGetwanforaudatex = Nothing

    End Sub

    '%GetServ_OrderForAudatex: Se obtiene nro de orden de servicio dentro del sistema audatex su id identificativo dentro de AUDATEX
    '---------------------------------------------------------------------------------------------------------
    Private Sub GetServ_OrderForAudatex(ByVal sWan As String)

    End Sub

    '%upDateOrderForAudatex: Se actualizan las ordenes de servicio con el resultado de la llamada del servicio audatex
    '---------------------------------------------------------------------------------------------------------
    Private Function upDateOrderForAudatex(ByVal nServ_order As Integer, _
                                           ByVal nCodigo As Integer, _
                                           ByVal sWan As String, _
                                           ByVal sRefusemsg As String, _
                                           ByVal dOperdate As Date, _
                                           ByVal nUsercode As Integer, _
                                           ByVal sClient As String, _
                                  Optional ByVal sEvento As String = eRemoteDB.Constants.strNull, _
                                  Optional ByVal nAmount_Paint As Double = 0, _
                                  Optional ByVal nAmount_Mechan As Double = 0, _
                                  Optional ByVal nAmount_Part As Double = 0, _
                                  Optional ByVal nAmount As Double = 0, _
                                  Optional ByVal nNum_budget As Integer = 0, _
                                  Optional ByVal bVerifyAmount As Boolean = False) As Boolean
        '---------------------------------------------------------------------------------------------------------

    End Function

    '% BUILDNumeroExpediente: Se construye el formato de número de expediente encviado al servicio AUDATEX
    Private Function BUILDNumeroExpediente(ByVal nClaim As Integer, _
                                           ByVal nCase_Num As Integer, _
                                           ByVal nDeman_Type As Integer, _
                                           ByVal nServ_Order As Double) As String
        BUILDNumeroExpediente = nClaim.ToString.PadLeft(10, "0") & nCase_Num.ToString.PadLeft(5, "0") & nDeman_Type.ToString.ToString.PadLeft(5, "0") & nServ_Order.ToString.ToString.PadLeft(10, "0")
    End Function
    '% FindOrderToAudatex: Se obtine la información de la orden de servicio a ser enviada a AUDATEX
    Public Function FindOrderToAudatex(ByVal nServ_Order As Double, _
                                       ByVal dEffecdate As Date) As Boolean


    End Function

    '% sendOrderToAudatex: Este método envía los datos necesarios relacionados con una Orden de Servicio, para que AUDATEX cree un
    '% Expediente, y se genere la Solicitud de Inspección
    Public Function sendOrderToAudatex(ByVal nServ_Order As Double, _
                                       ByVal dOperdate As Date, _
                                       ByVal nUsercode As Integer) As Boolean


    End Function

    '% receiveInspectionResult: Se encarga de llamar a AUDATEX, enviándole los Datos de la Orden de Servicio (Expediente), y trae de vuelta los Datos del Resultado de la Inspección, que son los Montos (Presupuesto) de: Total Mano de Obra (incluye Hojalata y Mecánica), Total Pintura, Total Partes (Repuestos)
    '%  y por ende, debe actualizar dichos montos en la BD de VT
    Public Function receiveInspectionResult(ByVal nUsercode As Integer, _
                                            ByVal dOperdate As Date, _
                                   Optional ByVal sWan As String = eRemoteDB.Constants.strNull, _
                                   Optional ByVal nServ_Order As Double = eRemoteDB.Constants.dblNull, _
                                   Optional ByVal nNum_budget As Double = 0) As Boolean


    End Function

    '% receiveInspectionResultBatchByEvent: Procesamienot masivo de ordenes de servicio consultadas para procesamiento masivo por tipo de Evento
    Public Function receiveInspectionResultBatchByEvent(ByVal sEvent As String, _
                                                        ByVal dInitDate As Date, _
                                                        ByVal dFinalDate As Date, _
                                                        ByVal nUsercode As Integer) As Boolean

    End Function

    '% receiveInspectionResult: Este es un proceso “masivo” que pregunta a AUDATEX cuáles son Todos los Expedientes que, entre ayer y hoy, han
    '% cambiado de Status a “Autorizado por el Perito” o “Rechazado por el Perito”, y por cada Expediente retornado, llama a otro Método para
    '% traer el Resultado del Presupuesto. Los datos que nos interesan en cuanto a este punto son: Monto Total Mano de Obra (incluye Hojalata y Mecánica),
    '% Monto Total Pintura, Monto Total Partes (Repuestos), y por ende, debe actualizar dichos montos en la BD de VT
    Public Function receiveInspectionResultBatch(ByVal dInitDate As Date, _
                                                 ByVal dFinalDate As Date, _
                                                 ByVal nUsercode As Integer) As Boolean


    End Function

End Class