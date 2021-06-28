Option Strict Off
Option Explicit On

Imports system.Exception 
Imports System.Configuration

Public Class Roles
	'%-------------------------------------------------------%'
	'% $Workfile:: Roles.cls                                $%'
	'% $Author:: Nvaplat9                                   $%'
	'% $Date:: 9/09/04 4:20p                                $%'
	'% $Revision:: 113                                       $%'
    '%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 15/11/2001
	'Column_Name                                     Type      Length  Prec  Scale Nullable
	'----------------------------- --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public nRole As eRoles ' NUMBER        22     5      0 No
	Public SCLIENT As String ' CHAR          14              No
	Public dEffecdate As Date ' DATE           7              No
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 Yes
	Public nIntermed As Integer ' NUMBER        22    10      0 Yes
	Public dBirthdate As Date ' DATE           7              Yes
	Public sSexclien As String ' CHAR           1              Yes
	Public sSmoking As String ' CHAR           1              Yes
	Public nTyperisk As Integer ' CHAR           1              Yes
	Public sVIP As String ' CHAR           1              Yes
    Public sItem As String ' NUMBER        22     5      0 Yes
	Public nStatusrol As Integer ' NUMBER        22     5      0 Yes
	Public nRating As Integer ' NUMBER        22     5      0 Yes
	Public nTypename As Integer ' NUMBER         1     0      0 Yes
	Public nCoverPos As Integer ' NUMBER         1     0      0 Yes
	Public dContinue As Date
	Public nContrat_Pay As Integer
	Public sContinued As String
	Public sPrintName As String
    Public sPrint_RUC_Ind As String
    Public sAssociated_Policy_Required As String
	
	'- Auxiliares para la descripción
	Public sDesT12 As String
	Public sDesT18 As String
	Public sDesT5561 As String
	Public sDesT5592 As String
	
	Public nPerson_typ As Integer
	
	Public bCreateInsured As Boolean
	
	'**- Variable that contains the new Role code.
	'- Variable que contiene el nuevo codigo del Rol
	Public nRoleNew As Integer
	
	'**- Variable that contain the new client's code.
	'- Variable que contiene el nuevo codigo del cliente
	Public sClientNew As String
	
	'- Variable que indica si existe en la tabla Roles
	Public sSel As String
	
	'- Variable que guarda el nombre del cliente asociado
	Public sCliename As String
	
	'- Variable que guarda el digito verificador de un cliente
	Public sDigit As String
	
	'- Variables que indica el tipo de póliza
	Public sPolitype As String
	
	'- Variables que indica el tipo de componente de póliza
	Public sCompon As String
	
	'- Variables que indica si el rol viene seleccionado por defecto
	Public sDefaulti As String
	
	'- Variables que indica si el rol es requerido
	Public sRequire As String
	
	'- Variables que indica la cantidad máxima permitida de un rol para un producto
	Public nMax_role As Integer
	
	'- Variable que guarda la transacción que se está ejecutando
	Public nTransaction As Integer
	
	'**- Variable that indicates if the arrengement contains the information.
	'- variable que indica si el arreglo contiene información
	Private mblnCharge As Boolean
	
	'- Variable que contiene la figura que es titular de la póliza
	Public nHolder As Integer
	
	'- Variable que guarda la descripción de la figura
	Public sDescRole As String
	
	'- Variable que guarda la edad real del cliente
	Public mintAge As Integer
	
	'- Variable que guarda la edad actuarial del cliente
	Public mintInsuAge As Integer
	
	'**- Define the variable that will be used to keep the particular's information always available.
	'-Se define la variable que se usara para conservar la infomación del particular siempre disponible
	Public precParticular As eRemoteDB.Execute
	
	'**- Define a registry to keep each intermediary's data.
	'-Se define un registro para almacenar los datos de cada intermediario
	
	Structure pInfIntermed
		Dim lintIntermed As Integer
		Dim lstrIntertyp As String
		Dim lvntShare As Object
		Dim lstrCommityp As String
		Dim lvntPercent As Object
		Dim lvntAmount As Object
		Dim lstrColcom As String
		Dim lvntPerDiscount As Object 'Puntos de Descuento
		Dim lvntPerDiscountClaim As Object 'Puntos por Siniestralidad
		Dim lstrPlusCollec As String
		Dim lstrPlusOffice As String
		Dim lstrPlusQuality As String
	End Structure
	
	Private pDataIntermed As pInfIntermed
	
	Private lstrDescript(30, 2) As String
	
	'- Tipos de roles segun Table12
	Public Enum eRoles
		eRolContratanting = 1 ' Contratante
		eRolInsured = 2 ' Asegurado
		eRolThirdParty = 3 ' Tercero
		eRolUsalDirver = 4 ' Conductor habitual
		eRolContact = 5 ' Contacto
		eRolCounterGuarantor = 6 ' Contragarante
		eRolAddicionalInsured = 7 ' Asegurado adicional
		eRolBondee = 8 ' Afianzado
		eRolHospital = 9 ' Clínica
		eRolGarage = 10 ' Taller
		eRolProfessional = 12 ' Profesional
		eRolIntermediary = 13 ' Intermediario
		eRolInsuredAffected = 14 ' Asegurado afectado
		eRolEndorsee = 15 ' Endosatario
		eRolBeneficiary = 16 ' Beneficiario
		eRolPersonalAccInsur = 20 ' Aseg.accidentes personales
		eRolParents = 21 ' Padres
		eRolChild = 22 ' Hijo
		eRolSpouse = 23 ' Cónyugue
		eRolSibling = 24 ' Hermano
		eRolPayer = 25 ' Pagador
		eRolEnterpriseGroup = 26 ' Grupo empresarial
	End Enum
	
	'- Este arreglo se emplea para cargar las figuras definidas para un producto
	Private Structure udtRoles
		Dim nRole As Integer
		Dim SCLIENT As String
		Dim sCliename As String
		Dim sRequire As String
		Dim nMax_role As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim nIntermed As Integer
		Dim sDefaulti As String
		Dim sClient2 As String
	End Structure
	
	Private arrRoles() As udtRoles
	
	'**- Properties used in the information search of the policy figures (Find_ClientInfo)
	'- Propiedades utilizadas en la busqueda de información de
	'- las figuras de la póliza (Find_ClientInfo)
	Public sCredit_card As String
	Public sDescript As String
	
	'**- Properties used for the BBB003 Intermediary's search
	'- Propiedades utilizadas para la busqueda de Intermediarios BBB003
	Public sOfficeDes As String
	Public sDescBranch As String
	Public sdescProd As String
	Public dStartdate As Date
	Public DEXPIRDAT As Date
	Public NCAPITAL As Decimal
	Public NPREMIUM As Decimal
	Public nNullcode As Integer
	Public sDescOfficeIns As String
	Public sOriginal As String
	Public nMaxCurr As Double
	Public nCountCur As Integer
	Public sStatus_pol As String
	Public sInterType As String
	Public nShare As Double
	Public sOptCommission As String
	Public sClient2 As String
	
	Public mintIndMod As Integer
	Public mstrCodisplReq As String
	
	Public nAFP As Integer
	Public sReqAddress As String
	Private mclsClient As eClient.Client
    Public sComplCod As String
    Public sMessage As String

	'%insExistsPolicyInsured: Indica si un cliente ya está registrado como
	'%                        asegurado en la poliza
	Private Function insExistsPolicyInsured(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal SCLIENT As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsExistspolicyinsured As eRemoteDB.Execute
		On Error GoTo insExistspolicyinsured_Err
		
		lrecinsExistspolicyinsured = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insExistspolicyinsured al 04-15-2003 12:37:28
		'+
		With lrecinsExistspolicyinsured
			.StoredProcedure = "insExistspolicyinsured"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				insExistsPolicyInsured = .Parameters("nExists").Value = 1
			Else
				insExistsPolicyInsured = False
			End If
		End With
		
insExistspolicyinsured_Err: 
		If Err.Number Then
			insExistsPolicyInsured = False
		End If
		'UPGRADE_NOTE: Object lrecinsExistspolicyinsured may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsExistspolicyinsured = Nothing
		On Error GoTo 0
	End Function
	
	'%insExistsPolicyInsured: Indica si un cliente ya está registrado como
	'%                        asegurado en la poliza
	Public Function insExistsPolicyInsured_Massive(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As String
        Dim lstrError As String = String.Empty
        Dim lrecReaRoles_Tab_covrol As eRemoteDB.Execute
        Dim lclsRoles As Roles

        On Error GoTo Find_Tab_Covrol_Err

        lrecReaRoles_Tab_covrol = New eRemoteDB.Execute

        With lrecReaRoles_Tab_covrol
            .StoredProcedure = "ReaRoles_Tab_covrol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Do While Not .EOF And lstrError = String.Empty
                    If insExistsPolicyInsured(sCertype, nBranch, nProduct, nPolicy, nCertif, .FieldToClass("sClient"), Today) Then
                        lstrError = "56023"
                    End If
                    .RNext()
                    'UPGRADE_NOTE: Object lclsRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsRoles = Nothing
                Loop
                .RCloseRec()
            End If
        End With

        insExistsPolicyInsured_Massive = lstrError
Find_Tab_Covrol_Err:
        If Err.Number Then
            insExistsPolicyInsured_Massive = String.Empty
        End If
        'UPGRADE_NOTE: Object lrecReaRoles_Tab_covrol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaRoles_Tab_covrol = Nothing
        On Error GoTo 0
    End Function



    '% InsUpdRoles: Realiza la actualización de la tabla
    Private Function InsUpdRoles(ByVal nAction As Integer, ByVal nTransaction As Integer) As Boolean
        Dim lrecInsUpdRoles As eRemoteDB.Execute
        Dim lintIndMod As Integer

        On Error GoTo InsUpdRoles_Err

        lrecInsUpdRoles = New eRemoteDB.Execute

        With lrecInsUpdRoles
            .StoredProcedure = "InsUpdRoles"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", IIf(sSmoking = String.Empty, "2", sSmoking), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVip", IIf(sVIP = String.Empty, "2", sVIP), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sItem", sItem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatusrol", nStatusrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRating", nRating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypename", nTypename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIndMod", lintIndMod, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sContinued", IIf(sContinued = String.Empty, "2", sContinued), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrintName", sPrintName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdRoles = .Run(False)
            mintIndMod = .Parameters("nIndMod").Value
        End With

InsUpdRoles_Err:
        If Err.Number Then
            InsUpdRoles = False
        End If
        'UPGRADE_NOTE: Object lrecInsUpdRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsUpdRoles = Nothing
        On Error GoTo 0
    End Function
    '% Add: Crea un registro en la tabla
    Public Function Add() As Boolean
        Add = InsUpdRoles(1, nTransaction)
    End Function

    '% Update: Actualiza los datos de la tabla
    Public Function Update() As Boolean
        Update = InsUpdRoles(2, nTransaction)
    End Function
    '%UpdateClientCode: Función que actualiza la información en la tabla roles...
    Public Function UpdateClientCode(ByVal sClientNew As String, ByVal nRoleNew As Integer) As Boolean
        Dim lrecinsRoles As eRemoteDB.Execute
        lrecinsRoles = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.insRoles'
        'Información leída el 26/01/2000 10:05:30 AM
        With lrecinsRoles
            .StoredProcedure = "insRoles"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientNew", sClientNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoleNew", nRoleNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdateClientCode = .Run(False)
        End With

    End Function

    '% Delete: Borra los datos de la tabla
    Public Function Delete() As Boolean
        Delete = InsUpdRoles(3, nTransaction)
    End Function

    '% Find: Lee los datos de la tabla
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As eRoles, ByVal SCLIENT As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaRoles As eRemoteDB.Execute

        On Error GoTo Find_Err

        If bFind Or Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.nRole <> nRole Or Me.SCLIENT <> SCLIENT Or Me.dEffecdate <> dEffecdate Then
            lrecreaRoles = New eRemoteDB.Execute

            With lrecreaRoles
                .StoredProcedure = "ReaRoles"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Find = True
                    sSel = .FieldToClass("sSel")
                    Me.sCertype = .FieldToClass("sCertype")
                    Me.nBranch = .FieldToClass("nBranch")
                    Me.nProduct = .FieldToClass("nProduct")
                    Me.nPolicy = .FieldToClass("nPolicy")
                    Me.nCertif = .FieldToClass("nCertif")
                    Me.nRole = .FieldToClass("nRole")
                    Me.SCLIENT = .FieldToClass("sClient")
                    Me.dEffecdate = .FieldToClass("dEffecdate")
                    dNulldate = .FieldToClass("dNulldate")
                    nIntermed = .FieldToClass("nIntermed")
                    dBirthdate = .FieldToClass("dBirthdate")
                    sSexclien = .FieldToClass("sSexclien")
                    sSmoking = .FieldToClass("sSmoking")
                    nTyperisk = .FieldToClass("nTyperisk")
                    nTypename = .FieldToClass("nTypename")
                    sVIP = .FieldToClass("sVip")
                    sItem = .FieldToClass("sItem")
                    nStatusrol = .FieldToClass("nStatusrol")
                    nRating = .FieldToClass("nRating")
                    sCliename = .FieldToClass("sCliename")
                    sDigit = .FieldToClass("sDigit")
                    nPerson_typ = .FieldToClass("nPerson_typ")
                    nAFP = .FieldToClass("nAFP")
                    dContinue = .FieldToClass("dContinue")
                    nContrat_Pay = .FieldToClass("nContrat_Pay")
                    'sComplCod = .FieldToClass("sComplCod")
                    .RCloseRec()
                End If
            End With
        End If
Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaRoles = Nothing
        On Error GoTo 0
    End Function

    '% Count: Obtiene la cantidad de registros para la póliza
    Public Function Count(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
        Dim lrecReaRoles_Count As eRemoteDB.Execute

        On Error GoTo Count_Err
        lrecReaRoles_Count = New eRemoteDB.Execute
        With lrecReaRoles_Count
            .StoredProcedure = "ReaRoles_Count"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Count = .Parameters("nExist").Value
        End With

Count_Err:
        If Err.Number Then
            Count = 0
        End If
        'UPGRADE_NOTE: Object lrecReaRoles_Count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaRoles_Count = Nothing
        On Error GoTo 0
    End Function

    '% Count_by_Required: Obtiene la cantidad de roles que son requeridos y no se han
    '%                    incluido en la póliza
    Public Function Count_by_Required(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal sRequire As String) As Integer
        Dim lrecreaRoles As eRemoteDB.Execute

        On Error GoTo Count_by_Required_Err

        lrecreaRoles = New eRemoteDB.Execute

        With lrecreaRoles
            .StoredProcedure = "ReaRolescount_by_Require"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                Count_by_Required = .FieldToClass("nCount")
                .RCloseRec()
            End If
        End With

Count_by_Required_Err:
        If Err.Number Then
            Count_by_Required = 0
        End If
        'UPGRADE_NOTE: Object lrecreaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaRoles = Nothing
        On Error GoTo 0
    End Function

    '% Count_By_Role: Obtiene la cantidad de registros por figura asociado a una póliza
    Public Function Count_By_Role(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As eRoles, ByVal dEffecdate As Date) As Integer
        Dim lrecReaRoles_Count_by_Role As eRemoteDB.Execute
        Dim ncount As Integer

        On Error GoTo Count_By_Role_Err

        lrecReaRoles_Count_by_Role = New eRemoteDB.Execute

        With lrecReaRoles_Count_by_Role
            .StoredProcedure = "ReaRoles_Count_by_Role"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", ncount, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Count_By_Role = .Parameters("nCount").Value
            End If
        End With

Count_By_Role_Err:
        If Err.Number Then
            Count_By_Role = 0
        End If
        'UPGRADE_NOTE: Object lrecReaRoles_Count_by_Role may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaRoles_Count_by_Role = Nothing
        On Error GoTo 0
    End Function

    '% Count_Tab_Covrol: Obtiene la cantidad de roles-coberturas asociados a la poliza
    Public Function Count_Tab_Covrol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Integer
        Dim lrecCount_Tab_Covrol As eRemoteDB.Execute

        On Error GoTo Count_Tab_Covrol_Err

        lrecCount_Tab_Covrol = New eRemoteDB.Execute
        With lrecCount_Tab_Covrol
            .StoredProcedure = "ReaCount_Tab_Covrol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Count_Tab_Covrol = .Parameters("nCount").Value
            End If
        End With

Count_Tab_Covrol_Err:
        If Err.Number Then
            Count_Tab_Covrol = 0
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecCount_Tab_Covrol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecCount_Tab_Covrol = Nothing
    End Function

    '% ReaMax_Item: Obtiene el número del ultimo Item asignado a un asegurado de la póliza
    Public Function ReaMax_Item(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Integer
        Dim lrecReaRoles_Maxitem As eRemoteDB.Execute

        On Error GoTo ReaMax_Item_Err

        ReaMax_Item = 0
        lrecReaRoles_Maxitem = New eRemoteDB.Execute

        With lrecReaRoles_Maxitem
            .StoredProcedure = "ReaRoles_Maxitem"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                ReaMax_Item = .FieldToClass("sItem")
                If ReaMax_Item = eRemoteDB.Constants.intNull Then
                    ReaMax_Item = 0
                End If
                .RCloseRec()
            End If
        End With

ReaMax_Item_Err:
        If Err.Number Then
            ReaMax_Item = 0
        End If
        'UPGRADE_NOTE: Object lrecReaRoles_Maxitem may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaRoles_Maxitem = Nothing
        On Error GoTo 0
    End Function

    '% InsValCA025: Validaciones según especificaciones funcionales de la transacción CA025
    '%              en el caso masivo
    Public Function InsValCA025(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As String, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTransaction As Integer, ByVal sBrancht As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lstrDesReq As String
        Dim lstrValReq As String
        Dim nQuotation As Integer

        On Error GoTo InsValCA025_Err

        lclsErrors = New eFunctions.Errors

        nQuotation = IIf(nTransaction = 4 Or nTransaction = 5 Or nTransaction = 24 Or nTransaction = 25 Or nTransaction = 28 Or nTransaction = 29, 1, 2)

        lstrDesReq = getDescByRequired(sCertype, nBranch, nProduct, nPolicy, nCertif, CDate(dEffecdate), sPolitype, sCompon, "1", , nQuotation)

        '+ Si existen descripciones de figuras requeridas, se despliega el mensaje.
        If lstrDesReq <> String.Empty Then
            lclsErrors.ErrorMessage(sCodispl, 3792, , eFunctions.Errors.TextAlign.RigthAling, " (" & lstrDesReq & ")")
        End If

        '+ Se valida la consistencia de los roles ingresados
        lstrValReq = insvalca025All(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, sPolitype, sCompon, nTransaction, sBrancht)

        If lstrValReq <> String.Empty Then
            lclsErrors.ErrorMessage(sCodispl, , , , , , lstrValReq)
        End If

        InsValCA025 = lclsErrors.Confirm

InsValCA025_Err:
        If Err.Number Then
            InsValCA025 = "InsValCA025: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '% InsPostCA025: Esta función realiza los cambios de BD según especificaciones funcionales
    '%               de la transacción (CA025)
    Public Function InsPostCA025(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecInsPostCA025 As eRemoteDB.Execute

        On Error GoTo InsPostCA025_Err
        lrecInsPostCA025 = New eRemoteDB.Execute
        '+Definición de parámetros para stored procedure 'InsPostCA025'
        '+Información leída el 15/04/2003
        With lrecInsPostCA025
            .StoredProcedure = "InsPostCA025pkg.InsPostCA025"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsPostCA025 = .Run(False)
        End With

InsPostCA025_Err:
        If Err.Number Then
            InsPostCA025 = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA025 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA025 = Nothing
        On Error GoTo 0
    End Function

    '% InsValCA025Upd: Validaciones según especificaciones funcionales de la transacción CA025
    '%                 en el caso puntual
    Public Function InsValCA025Upd(ByVal sCodispl As String, ByVal nExist As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As eRoles, ByVal SCLIENT As String, ByVal dEffecdate As Date, ByVal nIntermed As Integer, ByVal nStatusrol As Integer, ByVal nMax_role As Integer, ByVal sBrancht As String, ByVal sClientOld As String, ByVal nIntermedOld As Integer, ByVal nRoleOld As Integer, ByVal sDigit As String, ByVal dBirthdate As Date, ByVal sSexclien As String, ByVal sSmoking As String, ByVal nRating As Integer, ByVal sPolitype As String, ByVal nTransaction As Integer, ByVal nPrintName As Short, ByVal sPrintName As String, ByVal sVIP As String, ByVal sContinued As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
        Dim lrecinsValca025upd As eRemoteDB.Execute

        On Error GoTo InsValCA025Upd_Err

        lrecinsValca025upd = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure insValca025upd al 06-13-2003 17:17:28
        '+
        With lrecinsValca025upd
            .StoredProcedure = "insValca025upd"
            .Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatusrol", nStatusrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMax_role", nMax_role, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientold", sClientOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermedold", nIntermedOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoleold", nRoleOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRating", nRating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuitonerror", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPrintName", nPrintName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrintName", sPrintName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVip", sVIP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sContinued", sContinued, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCreateinsured", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nCreateinsured", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
                bCreateInsured = .Parameters("nCreateinsured").Value = 1
            End If
        End With
        'UPGRADE_NOTE: Object lrecinsValca025upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValca025upd = Nothing

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            InsValCA025Upd = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

InsValCA025Upd_Err:
        If Err.Number Then
            InsValCA025Upd = "InsValCA025Upd: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsValca025upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValca025upd = Nothing
    End Function

    '% InsPostCA025Upd: Esta función realiza los cambios de BD según especificaciones funcionales
    '%                  de la transacción (CA025)
    Public Function InsPostCA025Upd(ByVal sAction As String, ByVal nTransaction As Integer, ByVal nExist As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As eRoles, ByVal SCLIENT As String, ByVal dEffecdate As String, ByVal nIntermed As Integer, ByVal sBrancht As String, ByVal dBirthdate As Date, ByVal sSexclien As String, ByVal sSmoking As String, ByVal nTyperisk As Integer, ByVal sVIP As String, ByVal nRating As Integer, ByVal nStatusrol As Integer, ByVal sItem As String, ByVal sPolitype As String, ByVal sCompon As String, ByVal sClientOld As String, ByVal nIntermedOld As Integer, ByVal nUsercode As Integer, ByVal nTypename As Integer, ByVal dNulldate As Date, ByVal nCoverPos As Integer, ByVal sInterclient As String, ByVal sRequire As String, ByVal dContinue As Date, ByVal nContrat_Pay As Integer, ByVal sPrintName As String, Optional ByVal sContinued As String = vbNullString) As Boolean
        Dim lrecInsPostCA025Upd As New eRemoteDB.Execute
        Dim lclsConfig As New eRemoteDB.VisualTimeConfig

        On Error GoTo InsPostCA025Upd_Err

        '+Definición de parámetros para stored procedure 'InsPostCA025Upd'
        '+Información leída el 15/04/2003
        With lrecInsPostCA025Upd
            .StoredProcedure = "InsPostCA025pkg.InsPostCA025Upd"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClientOld", sClientOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermedOld", nIntermedOld, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyperisk", nTyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVip", sVIP, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sItem", sItem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatusrol", nStatusrol, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRating", nRating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypename", nTypename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCoverpos", nCoverPos, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInterclient", sInterclient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCreinsured", IIf(bCreateInsured, "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dContinue", dContinue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sContinued", sContinued, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrintName", sPrintName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsPostCA025Upd = .Run(False)

        End With
InsPostCA025Upd_Err:
        If Err.Number Then
            InsPostCA025Upd = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA025Upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA025Upd = Nothing
        On Error GoTo 0
    End Function

    '% DisabledField: Esta función retorna true o false dependiendo de la
    '%                transacción ejecutada
    Public Function DisabledField(ByVal sField As String, ByVal nTransaction As Integer) As Boolean
        DisabledField = True
        Select Case sField
            Case "ClientData"
                If nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyPropAmendent Or nTransaction = Constantes.PolTransac.clngCertifPropAmendent Or nTransaction = Constantes.PolTransac.clngPolicyQuotAmendent Or nTransaction = Constantes.PolTransac.clngCertifQuotAmendent Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyQuotRenewal Or nTransaction = Constantes.PolTransac.clngCertifQuotRenewal Or nTransaction = Constantes.PolTransac.clngCertifPropRenewal Then

                    DisabledField = False
                End If

        End Select
    End Function

    '% GetItem: Obtiene el número de item carga a asignar al asegurado
    Public Function GetItem() As Integer
        GetItem = sItem

        '+Se valida que sea una figura que puede ser asegurado
        If nRole <> eRoles.eRolContratanting And nRole <> eRoles.eRolIntermediary And nRole <> eRoles.eRolBeneficiary And nRole <> eRoles.eRolPayer Then

            '+Se valida que se trate con un certificado y sea una transacción válida
            If nCertif > 0 And (nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngCertifReissue Or nTransaction = Constantes.PolTransac.clngRecuperation Or nTransaction = Constantes.PolTransac.clngCertifQuotation) Then

                If sItem = eRemoteDB.Constants.strNull Then
                    '+Se llama al procedimiento que obtiene el número de Item que le corresponde al asegurado
                    GetItem = ReaMax_Item(sCertype, nBranch, nProduct, nPolicy, nCertif) + 1
                End If
            End If
        End If
    End Function

    '% InsReaHolder: procedimiento que obtiene el titular del recibo
    Private Function InsReaHolder(ByVal dEffecdate As Date, ByVal lclsCertificat As ePolicy.Certificat) As String
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy = New ePolicy.Policy
        Dim lclsSituation As ePolicy.Situation
        Dim lblnInProd As Boolean

        lblnInProd = False
        nHolder = 1
        On Error GoTo InsReaHolder_Err

        '+Busca el titular en Certificat
        With lclsCertificat
            If .nCertif = 0 Then
                lblnInProd = True
            ElseIf .nCertif > 0 Then
                '+si no existe certificado busca el titular en Policy
                lclsPolicy = New ePolicy.Policy
                If lclsPolicy.Find(.sCertype, .nBranch, .nProduct, .nPolicy) Then
                    If lclsPolicy.sColinvot = "1" Then
                        InsReaHolder = lclsPolicy.SCLIENT
                    ElseIf lclsPolicy.sColinvot = "2" Then
                        lblnInProd = True
                    ElseIf lclsPolicy.sColinvot = "3" Then
                        '+ Si tipo de recibos de la poliza es por situación
                        If .nSituation <> eRemoteDB.Constants.intNull Then
                            lclsSituation = New ePolicy.Situation
                            If lclsSituation.FindClieSituation(.sCertype, .nBranch, .nProduct, .nPolicy, .nSituation) Then
                                SCLIENT = lclsSituation.SCLIENT
                                sCliename = lclsSituation.sCliename
                                Me.nAFP = lclsSituation.nAFP
                            End If
                        End If
                    End If
                End If
            End If

            If lblnInProd Then
                '+ Si no se encuentra el titular se busca en el producto el recibo y vuelve a buscar en certificat
                lclsProduct = New eProduct.Product
                If lclsProduct.Find(.nBranch, .nProduct, dEffecdate) Then
                    nHolder = IIf(lclsProduct.sHolder = String.Empty, 1, lclsProduct.sHolder)
                End If
                If Find(.sCertype, .nBranch, .nProduct, .nPolicy, .nCertif, nHolder, String.Empty, dEffecdate) Then
                    InsReaHolder = SCLIENT
                Else
                    If lclsPolicy.sColinvot = "2" Then
                        InsReaHolder = lclsPolicy.SCLIENT
                    End If
                End If
            End If
        End With

InsReaHolder_Err:
        If Err.Number Then
            InsReaHolder = String.Empty
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsSituation = Nothing
    End Function

    '% InsGetClientHolder: Procedimiento para obtener el titular del recibo
    Public Function InsGetClientHolder(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsClient As eClient.Client

        On Error GoTo InsGetClientHolder_Err

        lclsCertificat = New ePolicy.Certificat
        sCliename = String.Empty
        SCLIENT = String.Empty
        If lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
            If lclsCertificat.SCLIENT = String.Empty Then
                SCLIENT = InsReaHolder(dEffecdate, lclsCertificat)
            Else
                nHolder = 1
                SCLIENT = lclsCertificat.SCLIENT
            End If
        End If

        If SCLIENT <> String.Empty And sCliename = String.Empty Then
            lclsClient = New eClient.Client
            If lclsClient.Find(SCLIENT) Then
                sCliename = lclsClient.sCliename
                Me.nAFP = lclsClient.nAFP
            End If
        End If

InsGetClientHolder_Err:
        If Err.Number Then
            InsGetClientHolder = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
    End Function

    '% CalInsuAge: Obtiene la edad real y la edad actuarial de un cliente
    Public Function CalInsuAge(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal dBirthdate As Date, ByVal sSexclien As String, ByVal sSmoking As String, Optional ByVal nRole As Integer = 0) As Boolean
        Dim lrecInsCalInsuAge As eRemoteDB.Execute

        On Error GoTo CalInsuAge_Err

        lrecInsCalInsuAge = New eRemoteDB.Execute

        With lrecInsCalInsuAge
            .StoredProcedure = "InsCalInsuAge"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInsuage", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                CalInsuAge = True
                mintAge = .Parameters("nAge").Value
                mintInsuAge = .Parameters("nInsuage").Value
            End If
        End With

CalInsuAge_Err:
        If Err.Number Then
            CalInsuAge = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecInsCalInsuAge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsCalInsuAge = Nothing
    End Function

    '% nAge: Propiedad para obtener la edad real o actuarial del cliente
    Public ReadOnly Property nAge(Optional ByVal bInsuAge As Boolean = False) As Integer
        Get
            If bInsuAge Then
                nAge = mintInsuAge
            Else
                nAge = mintAge
            End If
        End Get
    End Property

    '% InitValues: Inicializa los valores de las variables publicas de la clase
    Private Sub InitValues()
        sComplCod = String.Empty
        sCertype = String.Empty
        nBranch = eRemoteDB.Constants.intNull
        nProduct = eRemoteDB.Constants.intNull
        nPolicy = eRemoteDB.Constants.intNull
        nCertif = eRemoteDB.Constants.intNull
        nRole = eRemoteDB.Constants.intNull
        SCLIENT = String.Empty
        dEffecdate = eRemoteDB.Constants.dtmNull
        dNulldate = eRemoteDB.Constants.dtmNull
        nUsercode = eRemoteDB.Constants.intNull
        nIntermed = eRemoteDB.Constants.intNull
        dBirthdate = eRemoteDB.Constants.dtmNull
        sSexclien = String.Empty
        sSmoking = String.Empty
        nTyperisk = eRemoteDB.Constants.intNull
        sVIP = String.Empty
        sItem = eRemoteDB.Constants.intNull
        nStatusrol = eRemoteDB.Constants.intNull
        nRating = eRemoteDB.Constants.intNull
        nContrat_Pay = eRemoteDB.Constants.intNull
        sSel = String.Empty
        sCliename = String.Empty
        sPolitype = String.Empty
        sCompon = String.Empty
        sDefaulti = String.Empty
        sRequire = String.Empty
        nMax_role = eRemoteDB.Constants.intNull
        bCreateInsured = True
    End Sub

    '% Class_Initialize: Se ejecuta cuando se instancia la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        Call InitValues()
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '% insValCAL683_k: Realiza la validación de los campos de la ventana CAL683_k
    Public Function insValCAL683_k(ByVal sCodispl As String, ByVal dRunDate As Date) As String
        Dim lobjErrors As Object

        lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        On Error GoTo insValCAL683_k_Err

        '+Validación de la Fecha de ejecución
        If dRunDate = eRemoteDB.Constants.dtmNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 7116)
        End If

        insValCAL683_k = lobjErrors.Confirm
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValCAL683_k_Err:
        If Err.Number Then
            insValCAL683_k = insValCAL683_k & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% InsPostCAL683: Esta función permite realizar el llamado al procedimiento que crea la temporal (VAL696).
    Public Function InsPostCAL683(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal dProcdate As Date, ByVal nOpcejec As Short, ByVal nUsercode As Integer) As String
        Dim lrecInsPostCAL683 As eRemoteDB.Execute = New eRemoteDB.Execute
        Dim lstrKey As String

        With lrecInsPostCAL683
            lstrKey = "t" & Today.ToString("yyyyMMdd") & TimeOfDay.ToString("hhmmss") & nUsercode

            .StoredProcedure = "inspostcal683"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dProcdate", dProcdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOpcejec", nOpcejec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKey", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                InsPostCAL683 = lstrKey
            End If
        End With
        Return lrecInsPostCAL683
        'UPGRADE_NOTE: Object lrecInsPostCAL683 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCAL683 = Nothing
    End Function

    '**% insAutoAM002: fill the clause window automatically
    '% insAutoAM002: llena la ventana de cláusulas automáticamente
    Private Function insAutoAM002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String) As Boolean
        Dim lclsTar_Am_BasProd As eBranches.Tar_am_basprod
        Dim lclsTar_am_detprod As eBranches.Tar_am_detprod = New eBranches.Tar_am_detprod
        Dim lclsTar_am_pol As eBranches.Tar_am_pol = New eBranches.Tar_am_pol
        Dim lclsHealth As eBranches.Health = New eBranches.Health
        Dim lclsTar_Am_Bas As eBranches.Tar_am_bas = New eBranches.Tar_am_bas
        Dim lblnNotDefaulti As Boolean
        Dim lintPos As Integer

        lblnNotDefaulti = True
        On Error GoTo insAutoAM002_Err
        lclsTar_Am_BasProd = New eBranches.Tar_am_basprod
        If lclsTar_Am_BasProd.Load(nBranch, nProduct, dEffecdate) Then
            lintPos = 0
            Do While lclsTar_Am_BasProd.Item(lintPos) And lblnNotDefaulti
                If lclsTar_Am_BasProd.sDefaulti = "1" Then
                    lblnNotDefaulti = False
                Else
                    lintPos = lintPos + 1
                End If
            Loop
            If sPolitype = "1" Or nCertif = 0 Then
                If lintPos <> -1 Then
                    insAutoAM002 = True
                    '**+ If it is not executed the LOAD function, assign values to the public variables of the class - ACM - January 16,2001
                    '+ Si no se ejecuta la función LOAD, se asignan valores a las variables públicas de la clase - ACM - 16/01/2001
                    If lclsTar_Am_Bas Is Nothing Then
                        lclsTar_Am_Bas = New eBranches.Tar_am_bas
                    End If
                    If Not lclsTar_Am_Bas.Load(sCertype, nBranch, nProduct, nPolicy, dEffecdate) Then
                        lclsTar_Am_Bas.sCertype = sCertype
                        lclsTar_Am_Bas.nBranch = nBranch
                        lclsTar_Am_Bas.nProduct = nProduct
                        lclsTar_Am_Bas.nPolicy = nPolicy
                        lclsTar_Am_Bas.nTariff = lclsTar_Am_BasProd.nTariff
                        lclsTar_Am_Bas.dEffecdate = dEffecdate
                        lclsTar_Am_Bas.dNulldate = eRemoteDB.Constants.dtmNull
                        lclsTar_Am_Bas.sDefaulti = "1"

                        If lclsTar_Am_Bas.Add Then
                            If lclsTar_am_detprod Is Nothing Then
                                lclsTar_am_detprod = New eBranches.Tar_am_detprod
                            End If

                            If lclsTar_am_detprod.Load(nBranch, nProduct, lclsTar_Am_BasProd.nTariff, dEffecdate) Then
                                lintPos = 0
                                Do While lclsTar_am_detprod.Item(lintPos)
                                    If lclsTar_am_pol Is Nothing Then
                                        lclsTar_am_pol = New eBranches.Tar_am_pol
                                    End If
                                    lclsTar_am_pol.sCertype = sCertype
                                    lclsTar_am_pol.nBranch = nBranch
                                    lclsTar_am_pol.nProduct = nProduct
                                    lclsTar_am_pol.nPolicy = nPolicy
                                    lclsTar_am_pol.dEffecdate = dEffecdate
                                    lclsTar_am_pol.nTariff = lclsTar_am_detprod.nTariff
                                    lclsTar_am_pol.nAge_init = lclsTar_am_detprod.nAge_init
                                    lclsTar_am_pol.nAge_End = lclsTar_am_detprod.nAge_end
                                    lclsTar_am_pol.nGroup_comp = lclsTar_am_detprod.nGroup_comp
                                    lclsTar_am_pol.nPremium = lclsTar_am_detprod.nPremium
                                    lclsTar_am_pol.dNulldate = eRemoteDB.Constants.dtmNull
                                    Call lclsTar_am_pol.Add()
                                    lintPos = lintPos + 1
                                Loop
                            End If
                        End If
                    End If
                End If
            End If
            If sPolitype = "1" Or nCertif <> 0 Then
                If lclsHealth Is Nothing Then
                    lclsHealth = New eBranches.Health
                End If
                With lclsHealth
                    .nProduct = precParticular.FieldToClass("nProduct", eRemoteDB.Constants.intNull)
                    .nBranch = precParticular.FieldToClass("nBranch", eRemoteDB.Constants.intNull)
                    .sCertype = precParticular.FieldToClass("sCertype", String.Empty)
                    .nPolicy = precParticular.FieldToClass("nPolicy", eRemoteDB.Constants.intNull)
                    .nCertif = precParticular.FieldToClass("nCertif", eRemoteDB.Constants.intNull)
                    .dEffecdate = precParticular.FieldToClass("dEffecdate", eRemoteDB.Constants.dtmNull)
                    .nCapital = precParticular.FieldToClass("nCapital", eRemoteDB.Constants.intNull)
                    .dExpirdat = precParticular.FieldToClass("dExpirdat", eRemoteDB.Constants.dtmNull)
                    .sClient = precParticular.FieldToClass("sClient", String.Empty)
                    .nGroup_comp = precParticular.FieldToClass("nGroup_comp", eRemoteDB.Constants.intNull)
                    .dIssuedat = precParticular.FieldToClass("dIssuedat", eRemoteDB.Constants.dtmNull)
                    .nNullcode = precParticular.FieldToClass("nNullcode", eRemoteDB.Constants.intNull)
                    .dNulldate = precParticular.FieldToClass("dNulldate", eRemoteDB.Constants.dtmNull)
                    .nPremium = precParticular.FieldToClass("nPremium", eRemoteDB.Constants.intNull)
                    .dStartDate = precParticular.FieldToClass("dStartDate", eRemoteDB.Constants.dtmNull)

                    .nTariff = lclsTar_Am_BasProd.nTariff

                    .nTransactio = precParticular.FieldToClass("nTransactio", eRemoteDB.Constants.intNull)
                    insAutoAM002 = .Update
                End With
                precParticular.ReQuery()
            End If
        End If

insAutoAM002_Err:
        If Err.Number Then
            insAutoAM002 = False
        End If
        'UPGRADE_NOTE: Object lclsTar_Am_BasProd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_BasProd = Nothing
        'UPGRADE_NOTE: Object lclsTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_detprod = Nothing
        'UPGRADE_NOTE: Object lclsTar_am_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_pol = Nothing
        'UPGRADE_NOTE: Object lclsHealth may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsHealth = Nothing
        'UPGRADE_NOTE: Object lclsTar_Am_Bas may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_Am_Bas = Nothing
        On Error GoTo 0
    End Function

    '**% valRoles: Searches the number of associated roles to a policy-certificate.
    '% valRoles: Busca el número de roles asociados a un poliza-certificado
    Public Function valRoles(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As Integer, ByVal SCLIENT As String, ByVal dEffecdate As Date) As Integer
        Dim lrecValRoles As eRemoteDB.Execute
        lrecValRoles = New eRemoteDB.Execute

        On Error GoTo valRoles_Err

        With lrecValRoles
            .StoredProcedure = "valRoles"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("deffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                valRoles = .FieldToClass("lCount")
                .RCloseRec()
            Else
                valRoles = 0
            End If
        End With
        'UPGRADE_NOTE: Object lrecValRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecValRoles = Nothing

valRoles_Err:
        If Err.Number Then
            valRoles = 0
        End If
        On Error GoTo 0
    End Function

    '% valExistsRoles: devuelve la fecha de última modificación de la tabla
    Public Function valExistsRoles(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As Integer, ByVal SCLIENT As String, ByVal dEffecdate As Date) As Boolean
        Dim lclsExecute As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valExistsRoles_Err

        lclsExecute = New eRemoteDB.Execute

        With lclsExecute
            .StoredProcedure = "valExistsRoles"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                valExistsRoles = .Parameters("nExists").Value = 1
            End If
        End With

valExistsRoles_Err:
        If Err.Number Then
            valExistsRoles = False
        End If
        'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsExecute = Nothing
        On Error GoTo 0
    End Function

    '% valExistsRoles_Pol: Valida si existe una póliza válida de ramos generales para un cliente
    Public Function valExistsRoles_Pol(ByVal sCertype As String, ByVal SCLIENT As String, ByVal dEffecdate As Date) As Boolean
        Dim lclsExecute As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valExistsRoles_Pol_Err

        lclsExecute = New eRemoteDB.Execute

        With lclsExecute
            .StoredProcedure = "valExistsRoles_Pol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            valExistsRoles_Pol = .Parameters("nExists").Value = 1
        End With

valExistsRoles_Pol_Err:
        If Err.Number Then
            valExistsRoles_Pol = False
        End If
        'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsExecute = Nothing
        On Error GoTo 0
    End Function

    '% getMatchString: Obtiene el match de una cadena; es decir si no existe se concatena de lo contrario no.
    Public Function getMatchString(ByVal sStringOri As String, ByVal sString As String) As String
        If sStringOri <> String.Empty Then
            '+ Si no tiene el delimitador al principio del string, se coloca.
            If Mid(sStringOri, 1, 1) <> "|" Then
                sStringOri = "|" & sStringOri
            End If
            If Mid(sStringOri, Len(sStringOri), 1) <> "|" Then
                sStringOri = sStringOri & "|"
            End If

            '+ Se verifica si no existe la cadena se concatena.
            If sStringOri Like "*|" & sString & "|*" Then
                getMatchString = sStringOri
            Else
                getMatchString = sStringOri & sString & "|"
            End If
        Else
            getMatchString = "|" & sString & "|"
        End If
    End Function

    '% getRolClientPosCover: Obtiene la posición de la subcarpeta de coberturas por Rol-Cliente.
    Public Function getRolClientPosCover(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As Integer, ByVal SCLIENT As String, ByVal dEffecdate As Date) As Integer
        Dim lrecTime As eRemoteDB.Execute
        Dim lintPos As Integer

        On Error GoTo getRolClientPosCover_Err

        lrecTime = New eRemoteDB.Execute

        With lrecTime
            .StoredProcedure = "getRolClientPosCover"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPos", lintPos, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            getRolClientPosCover = .Parameters("nPos").Value
        End With

getRolClientPosCover_Err:
        If Err.Number Then
            getRolClientPosCover = -1
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTime = Nothing
    End Function

    '% insvalca025All: Se validan todos los roles ingresados en la ca025
    Public Function insvalca025All(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As String, ByVal sPolitype As String, ByVal sCompon As String, ByVal nTransaction As Integer, ByVal sBrancht As String) As String
        Dim lrecreaRoles As eRemoteDB.Execute
        Dim lclsConfig As New eRemoteDB.VisualTimeConfig
        Dim lstrDes As String = String.Empty 
        Dim sAllRoles As String = String.Empty  
        Dim bExecutingPEP As Boolean = False
        On Error GoTo insvalca025All_Err

        lrecreaRoles = New eRemoteDB.Execute

        With lrecreaRoles
            .StoredProcedure = "insvalca025All"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTransaction", nTransaction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRolValDesc", lstrDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAllRoles", sAllRoles, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insvalca025All = .Parameters("sRolValDesc").Value


                '+Se busca si se debe realizar el manejo PEP
            If lclsConfig.LoadSetting("Active", "No", "HandlingExtensions") = "Yes" Then
                Dim sclient_Aux As String
                Dim nRole_Aux As Integer
                Dim nUsercode As Integer
                For nCount As Integer = 0 To .Parameters("sAllRoles").Value.ToString.Split("|").Length
                    If .Parameters("sAllRoles").Value.ToString.Split("|")(nCount) = 2 Then
                        nRole_Aux = CInt(.Parameters("sAllRoles").Value.ToString.Split("|")(nCount))
                        sclient_Aux = .Parameters("sAllRoles").Value.ToString.Split("|")(nCount + 1)
                        If Not String.IsNullOrEmpty(sclient_Aux) Then
                            bExecutingPEP = True
                            Call insNotificationEventPEP(sclient_Aux, nBranch, nProduct, nPolicy, nRole_Aux, nUsercode)
                            bExecutingPEP = False
                            Exit For
                        End If
                    End If
                Next
            End If
        End With

insvalca025All_Err:
        If Err.Number Then
            'manejo de errores propios para el manejo de PEP
            If bExecutingPEP Then
                insvalca025All = "90000022|0|1|" + Err.Description + "|"
            Else
                'manejo de errores original de la funcion
                insvalca025All = String.Empty
            End If
            
        End If
        'UPGRADE_NOTE: Object lrecreaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaRoles = Nothing
        On Error GoTo 0
    End Function

    '% getDescByRequired: Obtiene la descripción de las figuras requeridas para la póliza pasada como parámetro.
    Public Function getDescByRequired(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal sRequire As String, Optional ByVal nTypDes As Integer = 1, Optional ByVal nQuotation As Integer = 2) As String
        Dim lrecreaRoles As eRemoteDB.Execute
        Dim lstrDes As String = ""

        On Error GoTo getDescByRequired_Err

        lrecreaRoles = New eRemoteDB.Execute

        With lrecreaRoles
            .StoredProcedure = "getRolesDesc_by_Require"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTypeDes", nTypDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuotation", nQuotation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRolReqDesc", lstrDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            getDescByRequired = .Parameters("sRolReqDesc").Value
        End With

getDescByRequired_Err:
        If Err.Number Then
            getDescByRequired = String.Empty
        End If
        'UPGRADE_NOTE: Object lrecreaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaRoles = Nothing
        On Error GoTo 0
    End Function


    '% valRolReq: Verifica si el cliente-rol es requerido para la póliza
    Public Function valRolReq(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPolitype As String, ByVal sCompon As String, ByVal nRole As Integer) As Boolean
        Dim lrecreaRoles As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valRolReq_Err

        lrecreaRoles = New eRemoteDB.Execute

        With lrecreaRoles
            .StoredProcedure = "valRolReq"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            valRolReq = .Parameters("nExists").Value = 1
        End With

valRolReq_Err:
        If Err.Number Then
            valRolReq = 0
        End If
        'UPGRADE_NOTE: Object lrecreaRoles may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaRoles = Nothing
        On Error GoTo 0
    End Function

    '% getConcatDelimit: Concatena dos string tomando en cuenta el delimitador pasado como parámetro
    Public Function getConcatDelimit(ByVal sStringOri As String, ByVal sString As String, Optional ByVal sDelimit As String = "") As String
        If sStringOri <> String.Empty Then
            '+ Si no tiene el delimitador al principio del string, se coloca.
            If Mid(sStringOri, 1, 1) <> sDelimit Then
                sStringOri = sDelimit & sStringOri
            End If
            If Mid(sStringOri, Len(sStringOri), 1) <> sDelimit Then
                sStringOri = sStringOri & sDelimit
            End If

            getConcatDelimit = sStringOri & sString & sDelimit
        Else
            If sDelimit <> String.Empty Then
                getConcatDelimit = sDelimit & sString & sDelimit
            Else
                getConcatDelimit = sString
            End If
        End If
    End Function

    '% getRatingClientProd: Obtiene la posición de la subcarpeta de coberturas por Rol-Cliente.
    Public Function getRatingClientProd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nRole As Integer, ByVal SCLIENT As String, ByVal dBirthdate As Date, ByVal sSexclien As String, ByVal sSmoking As String, ByVal dEffecdate As Date, Optional ByVal nAgeType As Integer = 1) As Integer
        Dim lrecTime As eRemoteDB.Execute
        Dim lintValue As Integer

        On Error GoTo getRatingClientProd_Err

        lrecTime = New eRemoteDB.Execute

        With lrecTime
            .StoredProcedure = "getRatingClientProd"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthDate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexClien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSmoking", sSmoking, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgeType", nAgeType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRating", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            getRatingClientProd = .Parameters("nRating").Value
        End With

getRatingClientProd_Err:
        If Err.Number Then
            getRatingClientProd = -1
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTime = Nothing
    End Function

    '% InsValEnterpriseGroup: Realiza la validación del rol grupo empresarial
    Private Function InsValEnterpriseGroup(ByVal SCLIENT As String, ByVal dEffecdate As Date) As Boolean
        Dim lrecInsValEnterpriseGroup As eRemoteDB.Execute

        On Error GoTo InsValEnterpriseGroup_Err

        lrecInsValEnterpriseGroup = New eRemoteDB.Execute

        With lrecInsValEnterpriseGroup
            .StoredProcedure = "InsValEnterpriseGroup"
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValid", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsValEnterpriseGroup = .Run(False)
            InsValEnterpriseGroup = (.Parameters("nValid").Value = 1)
        End With

InsValEnterpriseGroup_Err:
        If Err.Number Then
            InsValEnterpriseGroup = False
        End If
        'UPGRADE_NOTE: Object lrecInsValEnterpriseGroup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsValEnterpriseGroup = Nothing
        On Error GoTo 0
    End Function

    '% getErrorGenParRoles: Obtiene una cadena tipo string con los errores producidos por las validaciones generales de la CA025.
    '% El formato de la cadena es |1:3301|2:3302| donde el primer elemento antes de los puntos significa la línea en tratamiento y el elemento
    '+ después de los dos puntos significa el errorr a tratar.
    Public Function getErrorGenParRoles(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sPolitype As String, ByVal sCompon As String, ByVal nRole As Integer, ByVal SCLIENT As String, ByVal sSexclien As String, ByVal dBirthdate As Date, ByVal nPerson_typ As Integer) As String
        Dim lrecTime As eRemoteDB.Execute
        Dim lstrError As String = String.Empty

        On Error GoTo getErrorGenParRoles_Err

        lrecTime = New eRemoteDB.Execute

        With lrecTime
            .StoredProcedure = "valGenArrRoles"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPoliType", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompon", sCompon, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", SCLIENT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSexclien", sSexclien, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dBirthdate", dBirthdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPerson_typ", nPerson_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sError", lstrError, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            getErrorGenParRoles = .Parameters("sError").Value
        End With

getErrorGenParRoles_Err:
        If Err.Number Then
            getErrorGenParRoles = "-1"
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTime = Nothing
    End Function

    Public Sub insNotificationEventPEP(ByVal sclient As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nRole As Integer, ByVal nUsercode As Integer)
        Dim lclsConfig As New eRemoteDB.VisualTimeConfig
        Dim lclsPolicy As New ePolicy.Policy
        Dim objContext As New eRemoteDB.ASPSupport
        Dim nUsercode_Aux As Integer
        Dim bTrace As Boolean = lclsConfig.LoadSetting("Trace", "Yes", "VerificationPEP") = "Yes"

        Dim sEquivalentProduct As String = lclsPolicy.EquivalentFieldToClass("nProduct", nBranch, nProduct)
        If nUsercode <= 0 Then
            nUsercode_Aux = objContext.GetASPSessionValue("nUsercode")
        Else
            nUsercode_Aux = nUsercode
        End If


        Dim sEquivalentUsercode As String = lclsPolicy.EquivalentFieldToClass("nUsercode", , , , , , , nUsercode_Aux)
        Dim sEquivalentRole As String = lclsPolicy.EquivalentFieldToClass("nRole", , , , nRole)

        sMessage = "Comenzando Bloque interface PEP"
        Try
            sMessage += "Comenzando Bloque PEP. "
            Dim asb As System.Reflection.Assembly
            sMessage += "Cargando Assembly. "
            asb = System.Reflection.Assembly.LoadFrom(lclsConfig.LoadSetting("DllFullPath", "", "VerificationPEP"))
            sMessage += "ok. Instanciando clase "
            Dim cls As Object = asb.CreateInstance("CorpvidaIntegration.PEPClient")
            sMessage += "ok. "

            If Not cls Is Nothing Then
                sMessage += "objeto instanciado. "
            Else
                sMessage += "Objeto es nothing. "
            End If
            sMessage += "Asignando EndPoint. "
            cls.RemoteAddress = lclsConfig.LoadSetting("WSEndPoint", "", "VerificationPEP")
            sMessage += "Ok. "
            sMessage += "Invocando notificacion:" + sclient + "," + sEquivalentProduct + "," + nPolicy.ToString() + "," + "," + sEquivalentRole + "," + sEquivalentUsercode
            cls.NotifyNewClient(sclient, sEquivalentProduct, CStr(nPolicy), sEquivalentRole, sEquivalentUsercode)
            sMessage += "Fin Invocacion."
            If bTrace Then
                Throw New Exception("Invocacion PEP sin problemas.")
            End If
        Catch ex As Exception
            If lclsConfig.LoadSetting("IgnoreError", "Yes", "VerificationPEP") = "No" Then
                If bTrace Then
                    Throw New Exception(ex.Message & ".Origen:" & ex.Source & ".Traza:" & sMessage)
                Else
                    Throw New Exception(ex.Message & ".Origen:" & ex.Source)
                End If
            End If
        Finally
        End Try
    End Sub

	'*Class_Terminate: Se controla la destrucción de la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mclsClient = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






