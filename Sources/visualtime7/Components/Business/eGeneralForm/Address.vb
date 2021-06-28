Option Strict Off
Option Explicit On
Public Class Address
	'%-------------------------------------------------------%'
	'% $Workfile:: Address.cls                              $%'
	'% $Author:: Nvapla10                                   $%'
	'% $Date:: 10/11/04 3:38p                               $%'
	'% $Revision:: 11                                       $%'
	'%-------------------------------------------------------%'
	'- Propiedades según la tabla en el sistema el 18/01/2000.
	
	'Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nRecOwner As Integer '                                                            no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public sKeyAddress As String '                                                               no                                  20                      no                                  no                                  no
	Public sRecType As String '                                                               no                                  1                       yes                                 no                                  yes
	Public sStreet As String '                                                               no                                  40                      yes                                 no                                  yes
	Public sStreet1 As String '                                                               no                                  40                      yes                                 no                                  yes
	Public sZone As String '                                                               no                                  30                      yes                                 no                                  yes
	Public sClient As String '                                                               no                                  14                      yes                                 no                                  yes
	Public sCertype As String '                                                               no                                  1                       yes                                 no                                  yes
	Public sE_mail As String '                                                               no                                  60                      yes                                 no                                  yes
	Public dCompdate As Date '                                                           no                                  8                       yes                                 (n/a)                               (n/a)
	Public nLat_second As Double '                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public nLon_second As Double '                                                          no                                  5           4     2     yes                                 (n/a)                               (n/a)
	Public nLat_coord As Integer '                                                              no                                  8           53    NULL  yes                                 (n/a)                               (n/a)
	Public nLon_coord As Integer '                                                              no                                  8           53    NULL  yes                                 (n/a)                               (n/a)
	Public nContrat As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nCountry As Integer '                                                            no                                  2           5     0     no                                  (n/a)                               (n/a)
    Public nLat_cardin As Double '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
    Public nLat_minute As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
    Public nLon_cardin As Double '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
    Public nLon_minute As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nCertif As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nClaim As Double '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nPolicy As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nLocal As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nZip_Code As Double '                                                              no                                  4           10    0     no                                  (n/a)                               (n/a)
	Public nLat_grade As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nLon_grade As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nUsercode As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nBk_agency As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nBank_code As Integer '                                                              no                                  4           10    0     yes                                 (n/a)                               (n/a)
	Public nBranch As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nOffice As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public nProvince As Integer '                                                            no                                  2           5     0     no                                  (n/a)                               (n/a)
	Public nProduct As Integer '                                                            no                                  2           5     0     yes                                 (n/a)                               (n/a)
	Public dEffecdate As Date
	Public dNulldate As Date
	Public nMunicipality As Integer
	Public sCostCenter As String
    Public nNotInformEmail As String
    Public sSend_mail As String
	
	'- Se agrega otra variable Hoja416 Felipe Lagos B
	Public sInfor As String
	Public sBuild As String ' VARCHAR2   10   0     0    S
	Public nFloor As Integer ' NUMBER     22   0     5    S
	Public sDepartment As String ' VARCHAR2   10   0     0    S
	Public sPopulation As String ' VARCHAR2   40   0     0    S
	Public sPobox As String ' CHAR       15   0     0    S
	Public sDescadd As String ' CHAR       100  0     0    S
	
	'- Se definen las variable auxiliares
	
	'- Se define la variable para indicar el estado de cada instancia en la colección
	Public nStatusInstance As Integer
	Private Enum eActions
		clngAdd = 1
		clndUpdate = 2
		clngDelete = 3
	End Enum
	
	Public Enum eTypeRecOwner
		clngPolicyAddress = 1 '+Dirección de la póliza
		clngClientAddress = 2 '+Dirección del Cliente
		clngBenefAddress = 3 '+Dirección del Beneficiarios
		clngInterAddress = 4 '+Dirección del Intermediario
		clngCompanyCAddress = 5 '+Dirección de la compañía (Central).
		clngCompanyLAddress = 6 '+Dirección de la compañía (Local).
		clngAgencyAddress = 7 '+Dirección de la agencia bancaria
		clngRiskAddress = 8 '+Dirección del riesgo asegurado (emisión de póliza)
		clngOfficeAddress = 9 '+Dirección de la sucursal
		clngContratAddress = 10 '+Dirección del contrato de financiamiento
		clngOccurAddress = 11 '+Dirección de occurrencia (siniestro)
		clngDemandantAddress = 12 '+Dirección del reclamante (siniestro)
		clngDeliveryAddress = 13 '+Dirección de envío de correspondencia (siniestro)
		clngAgreementAddress = 14 '+Dirección del convenio
	        clngClientAddressInPolicy = 81 '+Dirección del cliente en la póliza - certificado
	End Enum
	
	'- Se define la variable publica Phones, para contener la colección de telefonos asociados
	'- asociadas a determinada dirección
	
	'UPGRADE_NOTE: Phones was upgraded to Phones_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Phones_Renamed As Phones
	'UPGRADE_NOTE: Phone was upgraded to Phone_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Phone_Renamed As Phone
	
	'- Se definen variable públicas para contener las descripciones de Localidad
	Public slocat_des As String
	Public sprovi_des As String
	Public scount_des As String
	
	'-Variables usadas para la CAC005
	Public sDescCurrency As String
	Public sDescBranch As String
	
	'-Variables usadas para la SCA001 (Validacion si existe direccion asociada a la poliza)
    Public nCount As Integer

    Public Property Phones() As Phones
        Set(ByVal oValue As Phones)
            Phones_Renamed = oValue
        End Set
        Get
            Return Phones_Renamed
        End Get

    End Property

    Public Property Phone() As Phone
        Set(ByVal oValue As Phone)
            Phone_Renamed = oValue
        End Set
        Get
            Return Phone_Renamed
        End Get

    End Property

    '% Add: Permite añadir registros en la tabla de resultados presupuestarios
    Public Function Add() As Boolean
        Add = insUpdAddress(eActions.clngAdd)
    End Function

    '% Update: Permite modificar registros en la tabla de resultados presupuestarios
    Public Function Update() As Boolean
        Update = insUpdAddress(eActions.clndUpdate)
    End Function

    '% Delete: Permite eliminar registros en la tabla de resultados presupuestarios
    Public Function Delete() As Boolean
        Delete = insUpdAddress(eActions.clngDelete)
    End Function

    '% Find: Permite buscar registros en la tabla de resultados presupuestarios
    Function Find(ByVal sKeyAddress As String, ByVal nRecOwner As eTypeRecOwner, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False, Optional ByVal bPhone As Boolean = True) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        On Error GoTo Find_Err

        If sKeyAddress = Me.sKeyAddress And nRecOwner = Me.nRecOwner And dEffecdate = Me.dEffecdate And Not bFind Then
            Find = True
        Else

            'Definición de parámetros para stored procedure 'insudb.reaAddress'
            'Información leída el 11/07/2000 13:27:57

            With lrecreaAddress
                .StoredProcedure = "reaAddress"
                .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAll", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    nRecOwner = .FieldToClass("nRecOwner")
                    sKeyAddress = .FieldToClass("sKeyAddress")
                    dEffecdate = .FieldToClass("dEffecDate")
                    sRecType = .FieldToClass("sRectype")
                    sStreet = .FieldToClass("sStreet")
                    sStreet1 = .FieldToClass("sStreet1")
                    sZone = .FieldToClass("sZone")
                    sClient = .FieldToClass("sClient")
                    sCertype = .FieldToClass("sCertype")
                    sE_mail = .FieldToClass("sE_mail")
                    nLat_second = IIf(.FieldToClass("nLat_second") = intNull, 0, .FieldToClass("nLat_second"))
                    nLon_second = IIf(.FieldToClass("nLon_second") = intNull, 0, .FieldToClass("nLon_second"))
                    nLat_coord = .FieldToClass("nLat_coord")
                    nLon_coord = .FieldToClass("nLon_coord")
                    nContrat = .FieldToClass("nContrat")
                    nCountry = .FieldToClass("nCountry")
                    nLat_cardin = .FieldToClass("nLat_cardin")
                    nLat_minute = IIf(.FieldToClass("nLat_minute") = intNull, 0, .FieldToClass("nLat_minute"))
                    nLon_cardin = .FieldToClass("nLon_cardin")
                    nLon_minute = IIf(.FieldToClass("nLon_minute") = intNull, 0, .FieldToClass("nLon_minute"))
                    nCertif = .FieldToClass("nCertif")
                    nClaim = .FieldToClass("nClaim")
                    nPolicy = .FieldToClass("nPolicy")
                    nLocal = .FieldToClass("nLocal")
                    nZip_Code = .FieldToClass("nZip_code")
                    nLat_grade = IIf(.FieldToClass("nLat_grade") = intNull, 0, .FieldToClass("nLat_grade"))
                    nLon_grade = IIf(.FieldToClass("nLon_grade") = intNull, 0, .FieldToClass("nLon_grade"))
                    nBk_agency = .FieldToClass("nBk_agency")
                    nBank_code = .FieldToClass("nBank_code")
                    nBranch = .FieldToClass("nBranch")
                    nOffice = .FieldToClass("nOffice")
                    nProvince = .FieldToClass("nProvince")
                    nProduct = .FieldToClass("nProduct")
                    nMunicipality = .FieldToClass("nMunicipality")
                    '+ Se agrega con la hoja 41
                    sInfor = .FieldToClass("sInfor")
                    sBuild = .FieldToClass("sBuild")
                    nFloor = .FieldToClass("nFloor")
                    sDepartment = .FieldToClass("sDepartment")
                    sPopulation = .FieldToClass("sPopulation")
                    sPobox = .FieldToClass("sPobox")
                    sDescadd = .FieldToClass("sDescadd")
                    sCostCenter = .FieldToClass("sCostCenter")
                    nNotInformEmail = .FieldToClass("nNotInformEmail")
                    sSend_mail = .FieldToClass("sSend_mail")


                    .RCloseRec()
                    '+ Si se desea obtener los teléfonos asociados a la dirección.
                    If bPhone Then
                        Phones_Renamed = New eGeneralForm.Phones
                        If Not Phones_Renamed.Find(nRecOwner, sKeyAddress, dEffecdate, True) Then
                            'UPGRADE_NOTE: Object Phones_Renamed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                            Phones_Renamed = Nothing
                        End If
                    End If
                End If
            End With
            'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaAddress = Nothing
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0

    End Function
    '% Find: Permite buscar registros en la tabla de resultados presupuestarios
    Function Find_PolAdd(ByVal sKeyAddress As String, ByVal nRecOwner As eTypeRecOwner, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        On Error GoTo Find_Err

        'Definición de parámetros para stored procedure 'insudb.reaAddress'
        'Información leída el 11/07/2000 13:27:57

        With lrecreaAddress
            .StoredProcedure = "reaAddress_PolAdd"
            .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_PolAdd = .Run
            If Find_PolAdd Then
                Me.nRecOwner = .FieldToClass("nRecOwner")
                Me.sKeyAddress = .FieldToClass("sKeyAddress")
                dEffecdate = .FieldToClass("dEffecDate")
                sRecType = .FieldToClass("sRectype")
                sStreet = .FieldToClass("sStreet")
                sStreet1 = .FieldToClass("sStreet1")
                sZone = .FieldToClass("sZone")
                sClient = .FieldToClass("sClient")
                sCertype = .FieldToClass("sCertype")
                sE_mail = .FieldToClass("sE_mail")
                nLat_second = IIf(.FieldToClass("nLat_second") = intNull, 0, .FieldToClass("nLat_second"))
                nLon_second = IIf(.FieldToClass("nLon_second") = intNull, 0, .FieldToClass("nLon_second"))
                nLat_coord = .FieldToClass("nLat_coord")
                nLon_coord = .FieldToClass("nLon_coord")
                nContrat = .FieldToClass("nContrat")
                nCountry = .FieldToClass("nCountry")
                nLat_cardin = .FieldToClass("nLat_cardin")
                nLat_minute = IIf(.FieldToClass("nLat_minute") = intNull, 0, .FieldToClass("nLat_minute"))
                nLon_cardin = .FieldToClass("nLon_cardin")
                nLon_minute = IIf(.FieldToClass("nLon_minute") = intNull, 0, .FieldToClass("nLon_minute"))
                nCertif = .FieldToClass("nCertif")
                nClaim = .FieldToClass("nClaim")
                nPolicy = .FieldToClass("nPolicy")
                nLocal = .FieldToClass("nLocal")
                nZip_Code = .FieldToClass("nZip_code")
                nLat_grade = IIf(.FieldToClass("nLat_grade") = intNull, 0, .FieldToClass("nLat_grade"))
                nLon_grade = IIf(.FieldToClass("nLon_grade") = intNull, 0, .FieldToClass("nLon_grade"))
                nBk_agency = .FieldToClass("nBk_agency")
                nBank_code = .FieldToClass("nBank_code")
                nBranch = .FieldToClass("nBranch")
                nOffice = .FieldToClass("nOffice")
                nProvince = .FieldToClass("nProvince")
                nProduct = .FieldToClass("nProduct")
                nMunicipality = .FieldToClass("nMunicipality")
                '+ Se agrega con la hoja 41
                sInfor = .FieldToClass("sInfor")
                sBuild = .FieldToClass("sBuild")
                nFloor = .FieldToClass("nFloor")
                sDepartment = .FieldToClass("sDepartment")
                sPopulation = .FieldToClass("sPopulation")
                sPobox = .FieldToClass("sPobox")
                sDescadd = .FieldToClass("sDescadd")
                sCostCenter = .FieldToClass("sCostCenter")
                nNotInformEmail = .FieldToClass("nNotInformEmail")
                sSend_mail = .FieldToClass("sSend_mail")

                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAddress = Nothing

Find_Err:
        If Err.Number Then
            Find_PolAdd = False
        End If
        On Error GoTo 0

    End Function
    '% UpdatePhones:
    Function UpdatePhones(ByVal sKeyAddress As String, ByVal nRecOwner As eTypeRecOwner, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        On Error GoTo UpdatePhones_Err

        'Definición de parámetros para stored procedure 'insudb.inscreaPhones'

        With lrecreaAddress
            .StoredProcedure = "insCreaPhones"
            .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            UpdatePhones = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAddress = Nothing

UpdatePhones_Err:
        If Err.Number Then
            UpdatePhones = False
        End If
        On Error GoTo 0

    End Function
    '% FindPhones: Permite buscar registros de telefonos en la tabla de telefonos
    Function FindPhones(ByVal sKeyAddress As String, ByVal nRecOwner As eTypeRecOwner, ByVal dEffecdate As Date, ByVal bExist As Boolean) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute
        On Error GoTo FindPhones_err



        'Definición de parámetros para stored procedure 'insudb.creaTmp_Phones'
        'Información leída el 11/07/2000 13:27:57


        With lrecreaAddress
            nRecOwner = nRecOwner
            sKeyAddress = sKeyAddress
            dEffecdate = dEffecdate

            'UPGRADE_NOTE: Object Me.Phones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            Me.Phones_Renamed = Nothing
            Me.Phones_Renamed = New eGeneralForm.Phones

            If bExist Then

                If Not Me.Phones_Renamed.GetFromAddress(nRecOwner, sKeyAddress, dEffecdate) Then
                    'UPGRADE_NOTE: Object Me.Phones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    Me.Phones_Renamed = Nothing
                    FindPhones = False
                Else
                    FindPhones = True
                End If

            Else

                If Not Me.Phones_Renamed.Find(nRecOwner, sKeyAddress, dEffecdate, True) Then
                    'UPGRADE_NOTE: Object Me.Phones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    Me.Phones_Renamed = Nothing
                    FindPhones = False
                Else
                    FindPhones = True
                End If

            End If

        End With

        'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAddress = Nothing

FindPhones_err:
        If Err.Number Then
            FindPhones = False
        End If
        On Error GoTo 0

    End Function

    '% Find_sInfor: Permite buscar registros en la tabla de direcciones la direccion de envio
    Function Find_sInfor(ByVal nRecOwner As eTypeRecOwner, ByVal sClient As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        On Error GoTo Find_sInfor_err

        'Definición de parámetros para stored procedure 'Reaaddres_sInfor'
        'Información leída el 12/10/2001
        With lrecreaAddress
            .StoredProcedure = "Reaaddres_sInfor"
            .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_sInfor = .Run
            If Find_sInfor Then
                sRecType = .FieldToClass("sRectype")
                sInfor = .FieldToClass("sInfor")
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAddress = Nothing

Find_sInfor_err:
        If Err.Number Then
            Find_sInfor = False
        End If
        On Error GoTo 0
    End Function

    '% Find_sInfor_Count: Permite buscar registros en la tabla de direcciones la direccion de envio
    Function Find_sInfor_Count(ByVal sClient As String, ByVal sRecType As String, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        On Error GoTo Find_sInfor_Count_err
        'Definición de parámetros para stored procedure 'Reaaddres_sInfor'
        'Información leída el 12/10/2001

        With lrecreaAddress
            .StoredProcedure = "ReaInfor_count"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRectype", sRecType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_sInfor_Count = .Run
            If Find_sInfor_Count Then
                sRecType = .FieldToClass("sRectype")
                sInfor = .FieldToClass("sInfor")
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAddress = Nothing

Find_sInfor_Count_err:
        If Err.Number Then
            Find_sInfor_Count = False
        End If
        On Error GoTo 0
    End Function

    '% Find_Address_Certif: Permite buscar registros en la tabla de direcciones la direccion de envio
    Function Find_Address_Certif(ByVal sClient As String, ByVal sRecType As String, ByVal dEffecdate As Date, ByVal nInd_pro As Integer, Optional ByVal bFind As Boolean = False) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        On Error GoTo Find_Address_Certif_err
        'Definición de parámetros para stored procedure 'ReaAddress_Certif'
        'Información leída el 12/10/2001

        With lrecreaAddress
            .StoredProcedure = "ReaAddress_Certif"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRectype", sRecType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nInd_pro", nInd_pro, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Address_Certif = .Run
            If Find_Address_Certif Then
                nCount = .FieldToClass("nCount")
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAddress = Nothing

Find_Address_Certif_err:
        If Err.Number Then
            Find_Address_Certif = False
        End If
        On Error GoTo 0
    End Function

    '% Class_Initialize: se controla la apertura de la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        nRecOwner = numNull
        sKeyAddress = String.Empty
        sRecType = String.Empty
        sStreet = String.Empty
        sStreet1 = String.Empty
        sZone = String.Empty
        sClient = String.Empty
        sCertype = String.Empty
        sE_mail = String.Empty
        dCompdate = dtmNull
        nLat_second = numNull
        nLon_second = numNull
        nLat_coord = numNull
        nLon_coord = numNull
        nContrat = numNull
        nCountry = numNull
        nLat_cardin = numNull
        nLat_minute = numNull
        nLon_cardin = numNull
        nLon_minute = numNull
        nCertif = numNull
        nClaim = numNull
        nPolicy = numNull
        nLocal = numNull
        nZip_Code = numNull
        nLat_grade = numNull
        nLon_grade = numNull
        nUsercode = numNull
        nBk_agency = numNull
        nBank_code = numNull
        nBranch = numNull
        nOffice = numNull
        nProvince = numNull
        nProduct = numNull
        dEffecdate = dtmNull
        dNulldate = dtmNull
        nMunicipality = numNull
        sInfor = String.Empty
        sBuild = String.Empty
        nFloor = numNull
        sDepartment = String.Empty
        sPopulation = String.Empty
        sPobox = String.Empty
        sDescadd = String.Empty

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    '%insUpdAddress. Esta funcion se encarga de realizar la actualización de la tabla address
    '%en la base de datos. Como parametro para la llamada a los SP, utiliza los valores
    '%contenidos en las propiedades de la clase
    Private Function insUpdAddress(ByRef llngAction As eActions) As Boolean
        Dim lrecinsUpdAddress As eRemoteDB.Execute
        lrecinsUpdAddress = New eRemoteDB.Execute

        On Error GoTo insUpdAddress_err

        'Definición de parámetros para stored procedure 'insudb.insUpdAddress'
        'Información leída el 11/07/2000 11:08:23

        With lrecinsUpdAddress
            .StoredProcedure = "insUpdAddress"
            .Parameters.Add("nAction", llngAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sKeyAddress", sKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRectype", sRecType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStreet", Mid(sStreet, 1, 40), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStreet1", Mid(sStreet1, 1, 40), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sZone", sZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", .ClassToField(sClient, eRemoteDB.Parameter.eRmtDataType.rdbVarchar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", .ClassToField(sCertype, eRemoteDB.Parameter.eRmtDataType.rdbChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sE_mail", .ClassToField(sE_mail, eRemoteDB.Parameter.eRmtDataType.rdbVarchar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLat_second", .ClassToField(nLat_second, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLon_second", .ClassToField(nLon_second, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nLat_coord", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("nLon_coord", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat", .ClassToField(nContrat, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCountry", .ClassToField(nCountry, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLat_cardin", .ClassToField(nLat_cardin, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLat_minute", .ClassToField(nLat_minute, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLon_cardin", .ClassToField(nLon_cardin, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLon_minute", .ClassToField(nLon_minute, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", .ClassToField(nCertif, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", .ClassToField(nClaim, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", .ClassToField(nPolicy, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLocal", .ClassToField(nLocal, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZip_code", .ClassToField(nZip_Code, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLat_grade", .ClassToField(nLat_grade, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLon_grade", .ClassToField(nLon_grade, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBk_agency", .ClassToField(nBk_agency, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBank_code", .ClassToField(nBank_code, eRemoteDB.Parameter.eRmtDataType.rdbNumeric), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", .ClassToField(nBranch, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOffice", .ClassToField(nOffice, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvince", .ClassToField(nProvince, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", .ClassToField(nProduct, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dEffecdate", IIf(IsDBNull(.ClassToField(dEffecdate, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp)), Today, dEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", .ClassToField(dNulldate, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMunicipality", .ClassToField(nMunicipality, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sInfor", .ClassToField(sInfor, eRemoteDB.Parameter.eRmtDataType.rdbChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBuild", .ClassToField(sBuild, eRemoteDB.Parameter.eRmtDataType.rdbVarchar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFloor", .ClassToField(nFloor, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDepartment", .ClassToField(sDepartment, eRemoteDB.Parameter.eRmtDataType.rdbVarchar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPopulation", .ClassToField(sPopulation, eRemoteDB.Parameter.eRmtDataType.rdbVarchar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPobox", .ClassToField(sPobox, eRemoteDB.Parameter.eRmtDataType.rdbChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescadd", .ClassToField(sDescadd, eRemoteDB.Parameter.eRmtDataType.rdbChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCostCenter", .ClassToField(sCostCenter, eRemoteDB.Parameter.eRmtDataType.rdbChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nNotInformEmail", .ClassToField(nNotInformEmail, eRemoteDB.Parameter.eRmtDataType.rdbInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSend_mail", .ClassToField(sSend_mail, eRemoteDB.Parameter.eRmtDataType.rdbChar), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insUpdAddress = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecinsUpdAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdAddress = Nothing

insUpdAddress_err:
        If Err.Number Then
            insUpdAddress = False
        End If
        On Error GoTo 0
    End Function

    '+ Se encarga de borrar o actualizar la direccion de la poliza
    '+ Segun hoja 416 Felipe Lagos B.
    Public Function insDelAddr_CA004(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean

        Dim lrecinsDelAddr_CA004 As eRemoteDB.Execute
        Dim lclsPolicy_Win As Object
        lrecinsDelAddr_CA004 = New eRemoteDB.Execute

        On Error GoTo insDelAddr_CA004_err

        'Definición de parámetros para stored procedure 'insudb.insDelAddress'
        'Información leída el 18/10/2001

        With lrecinsDelAddr_CA004
            .StoredProcedure = "insDelAddress"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insDelAddr_CA004 = .Run(False)
        End With

        If insDelAddr_CA004 Then
            lclsPolicy_Win = eRemoteDB.NetHelper.CreateClassInstance("ePolicy.Policy_Win")
            Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "SCA102", "1")
            'UPGRADE_NOTE: Object lclsPolicy_Win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lclsPolicy_Win = Nothing
        End If

        'UPGRADE_NOTE: Object lrecinsDelAddr_CA004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsDelAddr_CA004 = Nothing

insDelAddr_CA004_err:
        If Err.Number Then
            insDelAddr_CA004 = False
        End If
        On Error GoTo 0
    End Function

    '+ Se encarga de borrar o actualizar la direccion de la poliza
    '+ Segun hoja 416 Felipe Lagos B.
    Public Function valAddress_send(ByVal nRecOwner As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal sClient As String) As Boolean
        Dim lrecAddress As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valAddress_send_err

        lrecAddress = New eRemoteDB.Execute

        With lrecAddress
            .StoredProcedure = "valAddress_send"
            .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            valAddress_send = (.Parameters.Item("nExists").Value = 1)
        End With
valAddress_send_err:
        If Err.Number Then
            valAddress_send = False
            On Error GoTo 0
        End If
        'UPGRADE_NOTE: Object lrecAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecAddress = Nothing
    End Function


    '% Find: Permite buscar registros en la tabla de resultados presupuestarios
    Function insPostAddrAgree(ByVal sRecType As String, ByVal nRecOwner As Integer, ByVal ncod_Agree As Double, ByVal sStreet As String, ByVal sBuild As String, ByVal nFloor As Double, ByVal sDepartment As String, ByVal sPopulation As String, ByVal sDescadd As String, ByVal nZipCode As Double, ByVal nCountry As Double, ByVal nProvince As Double, ByVal nLocal As Double, ByVal nMunicipality As Object) As Boolean
        Dim lrecreaAddress As eRemoteDB.Execute
        lrecreaAddress = New eRemoteDB.Execute

        Dim lrecinsUpdAddress As eRemoteDB.Execute
        lrecinsUpdAddress = New eRemoteDB.Execute

        On Error GoTo insUpdAddress_err

        'Definición de parámetros para stored procedure 'insudb.insUpdAddress'
        'Información leída el 11/07/2000 11:08:23

        With lrecinsUpdAddress
            .StoredProcedure = "insupdaddressagree"
            .Parameters.Add("nRecowner", nRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRectype", sRecType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", ncod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStreet", sStreet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCountry", nCountry, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLocal", nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nZip_code", nZip_Code, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvince", nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMunicipality", nMunicipality, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBuild", sBuild, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFloor", nFloor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDepartment", sDepartment, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPopulation", sPopulation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescadd", sDescadd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 100, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            insPostAddrAgree = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecinsUpdAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdAddress = Nothing

insUpdAddress_err:
        If Err.Number Then
            insPostAddrAgree = False
        End If
        On Error GoTo 0

    End Function
End Class






