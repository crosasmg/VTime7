Option Strict Off
Option Explicit On
'UPGRADE_NOTE: Auto was upgraded to Automobile. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
<System.Runtime.InteropServices.ProgId("Automobile_NET.Automobile")> Public Class Automobile
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto.cls                                 $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 26/07/04 6:07p                               $%'
	'% $Revision:: 45                                       $%'
	'%-------------------------------------------------------%'
	'**-Properties according the table in the system on 28/12/2000
	'-Propiedades según la tabla en el sistema el 28/12/2000
	'    Column_name                  Type                   Computed   Length   Prec  Scale  Nullable  TrimTrailingBlanks   FixedLenNullInSource
	'-----------------------    --------------------------  ----------  -------  ----- ------ --------  -------------------  --------------------
	Public sCertype As String 'char           no          1                     no             no                   no
	Public nProduct As Integer 'smallint       no          2       5     0       no            (n/a)                (n/a)
	Public nBranch As Integer 'smallint       no          2       5     0       no            (n/a)                (n/a)
	Public nPolicy As Double 'int            no          4       10    0       no            (n/a)                (n/a)
	Public nCertif As Double 'int            no          4       10    0       no            (n/a)                (n/a)
	Public nAutoZone As Integer 'int            no          4       10    0       yes           (n/a)                (n/a)
	Public dEffecdate As Date 'datetime       no          8                     no            (n/a)                (n/a)
	Public sClient As String 'char           no          14                    yes            no                   yes
	Public nVehType As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public sChassis As String 'char           no          40                    yes            no                   yes
	Public sColor As String 'char           no          15                    yes            no                   yes
	Public sLicense_ty As String 'char           no          1                     yes            no                   yes
	Public sMotor As String 'char           no          40                    yes            no                   yes
	Public sRegist As String 'char           no          10                    yes            no                   yes
	Public sVehcode As String 'char           no          6                     yes            no                   yes
	Public dCompdate As Date 'datetime       no          8                     yes           (n/a)                (n/a)
	Public dExpirdat As Date 'datetime       no          8                     yes           (n/a)                (n/a)
	Public dIssuedat As Date 'datetime       no          8                     yes           (n/a)                (n/a)
	Public dNulldate As Date 'datetime       no          8                     yes           (n/a)                (n/a)
	Public dStartdate As Date 'datetime       no          8                     yes           (n/a)                (n/a)
	Public nCapital As Double 'decimal        no          9       12    0       yes           (n/a)                (n/a)
	Public nPremium As Double 'decimal        no          9       10    2       yes           (n/a)                (n/a)
	Public nVeh_valor As Double 'decimal        no          9       12    0       yes           (n/a)                (n/a)
	Public nVal_extra As Double 'decimal        no          9       12    0       yes           (n/a)                (n/a)
	Public nTransactio As Integer 'int            no          4       10    0       yes           (n/a)                (n/a)
	Public nNullcode As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nUsercode As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nVehplace As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nVehpma As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nYear As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nInd0km As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public sReference As String 'char           no          6                     yes            no                   yes
	Public nValueType As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nDiscClaim As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nDeduc As Double 'decimal        no          5       4     2       yes           (n/a)                (n/a)
	Public nUse As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public nPercTabVal As Double 'decimal        no          5       8     5       yes           (n/a)                (n/a)
	Public nGroup As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public dLastClaim As Date 'datetime       no          8                     yes           (n/a)                (n/a)
	Public nSituation As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	Public sDigit As String ' CHAR          no          1       0     0       yes           (n/a)                (n/a)
	Public sRelapsing As String ' CHAR          no          1       0     0       yes           (n/a)                (n/a)
	Public sN_infrac As String ' CHAR          no          1       0     0       yes           (n/a)                (n/a)
	Public sPromotion As String ' CHAR          no          1       0     0       yes           (n/a)                (n/a)
	Public sReturn As String ' CHAR          no          1       0     0       yes           (n/a)                (n/a)
	Public nLic_special As Integer 'smallint       no          2       5     0       yes           (n/a)                (n/a)
	'**-auxiliary properties
	'-Propiedades Auxiliares
	
	Public nTypeTransac As Integer
	
	Public sVehModel1 As String
	Public nVehBrand As Integer
	Public sDesBrand As String
	Public sDesTypeVeh As String
	Public sCliename As String
	Public dDriverdat As String
	Public sLicense As String
	
	Private mstrDescript As String
	Private mstrVehType As String
	Private mstrVehModel As String
	Private mstrVehBrand As String
	Public nCollectedPrem As Double
    Public sEngine As String
    Public sHybridVehicle As Char
    Public nConsec As Integer
    Public sClient_Dealer As String
    Public sClient_Seller As String
    Public nAgenDealer As Integer
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table "Auto"
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%tabla "Auto"
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaAuto As eRemoteDB.Execute

        On Error GoTo Find_Err

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or lblnFind Then

            lrecreaAuto = New eRemoteDB.Execute
            '**+Parameters Definitions to stored procedure 'insubd. reaAuto'
            '**+Data read on 12/28/200 11:52:49 a.m.
            '+Definición de parámetros para stored procedure 'insudb.reaAuto'
            '+Información leída el 28/12/2000 11:52:49 a.m.

            With lrecreaAuto
                .StoredProcedure = "reaAuto"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    Me.sCertype = sCertype
                    Me.nBranch = nBranch
                    Me.nProduct = nProduct
                    Me.nPolicy = nPolicy
                    Me.nCertif = nCertif
                    Me.dEffecdate = dEffecdate
                    nAutoZone = .FieldToClass("nAutoZone")
                    sClient = .FieldToClass("sClient")
                    dDriverdat = .FieldToClass("dDriverdat")
                    sLicense = .FieldToClass("sLicense")
                    sCliename = .FieldToClass("sCliename")
                    nVehType = .FieldToClass("nVehType")
                    sChassis = .FieldToClass("sChassis")
                    sColor = .FieldToClass("sColor")
                    sRegist = .FieldToClass("sRegist")
                    sLicense_ty = .FieldToClass("sLicense_ty")
                    sMotor = .FieldToClass("sMotor")
                    sVehcode = .FieldToClass("sVehcode")
                    dExpirdat = .FieldToClass("dExpirdat")
                    dIssuedat = .FieldToClass("dIssuedat")
                    dNulldate = .FieldToClass("dNulldate")
                    dStartdate = .FieldToClass("dStartdate")
                    nCapital = .FieldToClass("nCapital")
                    nPremium = .FieldToClass("nPremium")
                    nVeh_valor = .FieldToClass("nVeh_valor")
                    nVal_extra = .FieldToClass("nVal_Extra")
                    nTransactio = .FieldToClass("nTransactio")
                    nNullcode = .FieldToClass("nNullcode")
                    nVehplace = .FieldToClass("nVehplace")
                    nVehpma = .FieldToClass("nVehpma")
                    nYear = .FieldToClass("nYear")
                    nInd0km = .FieldToClass("nInd0km")
                    sReference = .FieldToClass("sReference")
                    nValueType = .FieldToClass("nValuetype")
                    nDiscClaim = .FieldToClass("nDiscclaim")
                    nDeduc = .FieldToClass("nDeduc")
                    nUse = .FieldToClass("nUse")
                    nPercTabVal = .FieldToClass("nPerctabval")
                    nGroup = .FieldToClass("nGroup")
                    dLastClaim = .FieldToClass("dLastClaim")
                    nSituation = .FieldToClass("nSituation")
                    sDigit = .FieldToClass("sDigit")
                    sRelapsing = .FieldToClass("sRelapsing")
                    sN_infrac = .FieldToClass("sN_infrac")
                    sPromotion = .FieldToClass("sPromotion")
                    nLic_special = .FieldToClass("nLic_special")
                    sReturn = .FieldToClass("sReturn")
					nCollectedPrem =.FieldToClass("nCollected_Prem")
                    sEngine = .FieldToClass("sEngine")
                    sHybridVehicle = .FieldToClass("sHybridVehicle")
                    sClient_Dealer = .FieldToClass("sClient_Dealer")
                    sClient_Seller = .FieldToClass("sClient_Seller")
                    nAgenDealer = .FieldToClass("nAgenDealer")

                    .RCloseRec()
                End If

                Dim objTab_au_veh As New eBranches.Tab_au_veh
                If objTab_au_veh.Find(sVehcode) Then
                    nVehBrand = objTab_au_veh.nVehBrand
                End If

            End With
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAuto = Nothing
    End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Auto". Devolviendo verdadero o
	'%        falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lrecupdAuto As eRemoteDB.Execute
		
		lrecupdAuto = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'+Definición de parámetros para stored procedure 'insudb.updAuto'
		'+Información leída el 28/12/2000 1:18:32 p.m.
		With lrecupdAuto
			.StoredProcedure = "updAuto"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAutoZone", nAutoZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dIssuedat", dIssuedat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dStartdate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVeh_valor", nVeh_valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVal_extra", nVal_extra, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNullcode", nNullcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehplace", nVehplace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehpma", nVehpma, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd0km", nInd0km, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReference", sReference, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nValueType", nValueType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDiscClaim", nDiscClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeduc", nDeduc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUse", nUse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercTabVal", nPercTabVal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 8, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dLastClaim", dLastClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelapsing", sRelapsing, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sN_infrac", sN_infrac, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPromotion", sPromotion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("SReturn", sReturn, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLic_special", nLic_special, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollectedPremium", nCollectedPrem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NVEHGROUP", eRemoteDB.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sVehModel", eRemoteDB.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SVEHMAKE", eRemoteDB.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sEngine", sEngine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sHybridVehicle", sHybridVehicle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_Dealer", sClient_Dealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient_Seller", sClient_Seller, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAgenDealer", nAgenDealer, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdAuto = Nothing
	End Function
	
	'%Find_ExisRegistAuto: Función que retorna VERDADERO realizar la lectura de registros en la tabla Auto
    Public Function Find_ExisRegistAuto(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal sRegist As String = "", Optional ByVal sChassis As String = "", Optional ByVal sMotor As String = "", Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecinsValExisRegistAuto2 As eRemoteDB.Execute
        Dim lclsPolicy As ePolicy.Policy

        On Error GoTo Find_ExisRegistAuto_Err

        lrecinsValExisRegistAuto2 = New eRemoteDB.Execute
        Find_ExisRegistAuto = False

        If (sChassis <> String.Empty And Trim(Me.sChassis) <> sChassis) Or (sMotor <> String.Empty And Trim(Me.sMotor) <> sMotor) Or (sRegist <> String.Empty And Trim(Me.sRegist) <> sRegist) Then

            '+Definición de parámetros para stored procedure 'insudb.insValExisRegistAuto2'
            '+Información leída el 16/02/2001 12:13:37 p.m.
            With lrecinsValExisRegistAuto2
                .StoredProcedure = "insValExisRegistAuto2"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    Me.sChassis = .FieldToClass("sChassis")
                    Me.sMotor = .FieldToClass("sMotor")
                    Me.sRegist = .FieldToClass("sRegist")
                    Me.nPolicy = .FieldToClass("nPolicy")
                    If Me.nPolicy <> nPolicy Then
                        Find_ExisRegistAuto = True
                    End If
                    .RCloseRec()
                Else
                    Me.sChassis = String.Empty
                    Me.sMotor = String.Empty
                    Me.sRegist = String.Empty
                    Me.nPolicy = CDbl("0")

                    If sChassis <> String.Empty Then
                        Me.sChassis = sChassis
                    ElseIf sMotor <> String.Empty Then
                        Me.sMotor = sMotor
                    ElseIf sRegist <> String.Empty Then
                        Me.sRegist = sRegist
                    End If
                End If
            End With
        Else
            lclsPolicy = New ePolicy.Policy
            Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy, True)
            If nPolicy <> lclsPolicy.nPolicy Then
                Find_ExisRegistAuto = True
            End If
        End If

Find_ExisRegistAuto_Err:
        If Err.Number Then
            Find_ExisRegistAuto = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsValExisRegistAuto2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValExisRegistAuto2 = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
    End Function
	
	'**%Find_Tab_au_veh: Function that returns TRUE to make reading of the records in the 'Auto_db' table
	'%Find_Tab_au_veh: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto_db'
    Public Function Find_Tab_au_veh(ByVal sVehcode As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaTab_au_veh1 As eRemoteDB.Execute

        On Error GoTo Find_Tab_au_veh1_Err

        lrecreaTab_au_veh1 = New eRemoteDB.Execute

        With lrecreaTab_au_veh1
            .StoredProcedure = "reaTab_au_veh1"
            .Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Tab_au_veh = .Run
            If Find_Tab_au_veh Then
                sDesBrand = .FieldToClass("sDesBrand")
                sVehModel1 = .FieldToClass("sVehmodel")
                nVehType = .FieldToClass("nVehType")
                sDesTypeVeh = .FieldToClass("sDesTypeVeh")
                nVehplace = IIf(.FieldToClass("nVehplace") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nVehplace"))
                nVehpma = IIf(.FieldToClass("nVehpma") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nVehpma"))
                nVehBrand = .FieldToClass("nVehBrand")
                .RCloseRec()
            End If
        End With

Find_Tab_au_veh1_Err:
        If Err.Number Then
            Find_Tab_au_veh = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaTab_au_veh1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_au_veh1 = Nothing
    End Function
	
	'**%Find_Tab_au_veh_byClaseMarcaTipo: Function 
	'%Find_Tab_au_veh_byClaseMarcaTipo: Función que realiza la busqueda del vehiculo por clase, marca y tipo
    Public Function Find_Tab_au_veh_byClaseMarcaTipo(ByVal sClase As String , Byval sMarca As String, sTipo As String, sModelo As String) As Boolean
        Dim lrecreaTab_au_veh1 As eRemoteDB.Execute

        On Error GoTo Find_Tab_au_veh1_Err

        lrecreaTab_au_veh1 = New eRemoteDB.Execute

        With lrecreaTab_au_veh1
            .StoredProcedure = "REATAB_AU_VEH_BYCLASEMARCATIPO"
            .Parameters.Add("sClase", sClase, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sMarca", sMarca, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTipo", sTipo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sModelo", sModelo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 50, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
             Find_Tab_au_veh_byClaseMarcaTipo = .Run
            If Find_Tab_au_veh_byClaseMarcaTipo Then
                sVehcode = .FieldToClass("sVehCode")
                sDesBrand = .FieldToClass("sDesBrand")
                sVehModel1 = .FieldToClass("sVehmodel")
                nVehType = .FieldToClass("nVehType")
                sDesTypeVeh = .FieldToClass("sDesTypeVeh")
                nVehplace = IIf(.FieldToClass("nVehplace") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nVehplace"))
                nVehpma = IIf(.FieldToClass("nVehpma") = eRemoteDB.Constants.intNull, 0, .FieldToClass("nVehpma"))
                nVehBrand = .FieldToClass("nVehBrand")
                .RCloseRec()
            End If
        End With

Find_Tab_au_veh1_Err:
        If Err.Number Then
            Find_Tab_au_veh_byClaseMarcaTipo = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaTab_au_veh1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_au_veh1 = Nothing
    End Function


	'**%Find_Tab_au_val: Function that returns TRUE to make the reading of the records in the 'Auto_db' table
	'%Find_Tab_au_val: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto_db'
    Public Function Find_Tab_au_val(ByVal sVehcode As String, ByVal nYear As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecReaTab_au_val As eRemoteDB.Execute

        On Error GoTo Find_Tab_au_val_Err

        lrecReaTab_au_val = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.ReaTab_au_val'
        '+ Información leída el 03/01/2001 4:59:22 p.m.

        With lrecReaTab_au_val
            .StoredProcedure = "ReaTab_au_val"
            .Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Tab_au_val = .Run
            If Find_Tab_au_val Then
                nCapital = .FieldToClass("nCapital")
                .RCloseRec()
            End If
        End With

Find_Tab_au_val_Err:
        If Err.Number Then
            Find_Tab_au_val = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecReaTab_au_val may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaTab_au_val = Nothing
    End Function
	
	'%valRegistActive: Indica si una placa está en un certificado vigente
	Public Function valRegistActive(ByVal sLicense_ty As String, ByVal sRegist As String, ByVal dEffecdate As Date) As Boolean
		Dim lrecinsValregist_active As eRemoteDB.Execute
		On Error GoTo insValregist_active_Err
		
		lrecinsValregist_active = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure insValregist_active al 07-26-2004 17:06:44
		'+
		With lrecinsValregist_active
			.StoredProcedure = "insValRegist_active"
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			valRegistActive = .Run(False)
			If valRegistActive Then
				valRegistActive = .Parameters("nExists").Value = 1
			End If
		End With
		
insValregist_active_Err: 
		If Err.Number Then
			valRegistActive = False
		End If
		'UPGRADE_NOTE: Object lrecinsValregist_active may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValregist_active = Nothing
		On Error GoTo 0
	End Function
	
	'%ValStructRegist:Esta rutina permite validar si la matrícula normal corresponde con algunos
	'%                de los formatos válidos.
	Public Function ValStructRegist(ByVal sRegist As String) As Boolean
		'-Se define la variable llngCount utilizada para almacenar el indice del vector en tratamiento.
		Dim llngCount As Integer
		
		On Error GoTo ValStructRegist_Err
		
		ValStructRegist = True
		
		If Len(Trim(sRegist)) > 6 Then
			ValStructRegist = False
		Else
			For llngCount = 1 To 6
				If llngCount < 3 Then
					If UCase(Mid(Trim(sRegist), llngCount, 1)) < "A" Or UCase(Mid(Trim(sRegist), llngCount, 1)) > "Z" Then
						ValStructRegist = False
						Exit For
					End If
				Else
					If Mid(Trim(sRegist), llngCount, 1) < "0" Or Mid(Trim(sRegist), llngCount, 1) > "9" Then
						ValStructRegist = False
					End If
				End If
			Next llngCount
		End If
		
ValStructRegist_Err: 
		If Err.Number Then
			ValStructRegist = False
		End If
	End Function
	
	'**%Find_Regist: Function that returns TRUE to make the reading of the records in the 'Auto' table
	'%Find_Regist: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto'
    Public Function Find_Regist(ByVal sRegist As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaAuto_regist As eRemoteDB.Execute

        lrecreaAuto_regist = New eRemoteDB.Execute

        On Error GoTo Find_Regist_Err

        '**+Parameters definition to stored procedure 'insudb.reaAuto_regist'
        '**+Data read on 02/15/2001 12:19:03 p.m.
        '+Definición de parámetros para stored procedure 'insudb.reaAuto_regist'
        '+Información leída el 15/02/2001 12:19:03 p.m.

        With lrecreaAuto_regist
            .StoredProcedure = "reaAuto_regist"
            .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Regist = .Run
            If Find_Regist Then
                nAutoZone = .FieldToClass("nAutoZone")
                sClient = .FieldToClass("sClient")
                nVehType = .FieldToClass("nVehType")
                sChassis = .FieldToClass("sChassis")
                sColor = .FieldToClass("sColor")
                sLicense_ty = .FieldToClass("sLicense_ty")
                sMotor = .FieldToClass("sMotor")
                sRegist = .FieldToClass("sRegist")
                sVehcode = .FieldToClass("sVehcode")
                nCapital = .FieldToClass("nCapital")
                nPremium = .FieldToClass("nPremium")
                nVeh_valor = .FieldToClass("nVeh_valor")
                nVal_extra = .FieldToClass("nVal_extra")
                nUsercode = .FieldToClass("nUsercode")
                nVehplace = .FieldToClass("nVehplace")
                nVehpma = .FieldToClass("nVehpma")
                nYear = .FieldToClass("nYear")
                nDeduc = .FieldToClass("nDeduc")
                nGroup = .FieldToClass("nGroup")
                dLastClaim = .FieldToClass("dLastClaim")
                nSituation = .FieldToClass("nSituation")
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaAuto_regist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAuto_regist = Nothing

Find_Regist_Err:
        If Err.Number Then
            Find_Regist = False
        End If
        'UPGRADE_NOTE: Object lrecreaAuto_regist may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAuto_regist = Nothing
        On Error GoTo 0
    End Function
	
	'**%sDescript: This property returns the description
	'%sDescript: Esta propiedad retorna la descripcion
	Public ReadOnly Property sDescript() As String
		Get
			Call insGetValues()
			sDescript = mstrDescript
		End Get
	End Property
	
	'**%sVehModel: This property returns the model of the vehicle
	'%sVehModel: Esta propiedad retorna el modelo del vehiculo
	Public ReadOnly Property sVehModel() As String
		Get
			Call insGetValues()
			sVehModel = mstrVehModel
		End Get
	End Property
	
	'**%sVehBrand: This property returns the brand of the vehicle
	'%sVehBrand: Esta propiedad retorna la marca del vehiculo
	Public ReadOnly Property sVehBrand() As String
		Get
			Dim lobjQuery As eRemoteDB.Query
			If mstrVehBrand = String.Empty Then
				Call insGetValues()
				lobjQuery = New eRemoteDB.Query
				If lobjQuery.OpenQuery("table7042", "sDescript", "nVehBrand = " & CStr(nVehBrand)) Then
					mstrVehBrand = lobjQuery.FieldToClass("sDescript")
				End If
				'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjQuery = Nothing
			End If
			sVehBrand = mstrVehBrand
		End Get
	End Property
	
	'**%sVehType: This property returns the type of vehicle
	'%sVehType: Esta propiedad retorna el typo de vehiculo
	Public ReadOnly Property sVehType() As String
		Get
			Dim lobjQuery As eRemoteDB.Query
			If mstrVehType = String.Empty Then
				Call insGetValues()
				lobjQuery = New eRemoteDB.Query
				If lobjQuery.OpenQuery("table226", "sDescript", "nVehType = " & CStr(nVehType)) Then
					mstrVehType = lobjQuery.FieldToClass("sDescript")
				End If
				'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjQuery = Nothing
			End If
			sVehType = mstrVehType
		End Get
	End Property
	
	'**%insGetValues: This method obtains the values of the vehicle
	'%insGetValues: Este metodo se encarga de obtener los valores del vehiculo
	Private Function insGetValues() As Object
        Dim lobjQuery As eRemoteDB.Query
        insGetValues = Nothing
		If mstrDescript = String.Empty And sVehcode <> String.Empty Then
			lobjQuery = New eRemoteDB.Query
			If lobjQuery.OpenQuery("tab_au_veh", "sDescript,nVehBrand,sVehModel,nVehType", "sVehCode = " & CStr(sVehcode)) Then
				With lobjQuery
					Me.nVehBrand = .FieldToClass("nVehBrand")
					mstrDescript = .FieldToClass("sDescript")
					mstrVehModel = .FieldToClass("sVehModel")
				End With
			End If
			'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjQuery = Nothing
		End If
	End Function
	'%InsCalDigitSerie: Calcula el digito verificador de una placa
	Public Function InsCalDigitSerie(ByVal sRegist As String) As Boolean
		Dim lrecInsCalDigitSerie As eRemoteDB.Execute
		On Error GoTo InsCalDigitSerie_Err
		
		lrecInsCalDigitSerie = New eRemoteDB.Execute
		
		sDigit = "0"
		'+
		'+ Definición de store procedure InsCalDigitSerie
		'+
		With lrecInsCalDigitSerie
			.StoredProcedure = "InsCalDigitSerie"
			With .Parameters
				.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			.Run(False)
			
			If Trim(.Parameters("sDigit").Value) = String.Empty Then
				InsCalDigitSerie = False
			Else
				Me.sDigit = Trim(.Parameters("sDigit").Value)
				InsCalDigitSerie = True
			End If
			
		End With
		
InsCalDigitSerie_Err: 
		If Err.Number Then
			InsCalDigitSerie = False
		End If
		'UPGRADE_NOTE: Object lrecInsCalDigitSerie may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsCalDigitSerie = Nothing
		On Error GoTo 0
	End Function
	'%next_seqregistauto: Calcula el proximo numero de secuencia
	Function next_seqregistauto() As Boolean
		Dim lrecnexT_seqregistauto As eRemoteDB.Execute
		On Error GoTo nexT_seqregistauto_Err
		
		lrecnexT_seqregistauto = New eRemoteDB.Execute
		
		'+ Definición de store procedure nexT_seqregistauto al 05-06-2002 16:28:49
		
		With lrecnexT_seqregistauto
			.StoredProcedure = "nexT_seqregistauto"
			.Parameters.Add("nVal_seq", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			next_seqregistauto = .Run(False)
			Me.sRegist = Trim(Str(.Parameters("nVal_seq").Value))
		End With
		
nexT_seqregistauto_Err: 
		If Err.Number Then
			next_seqregistauto = False
		End If
		'UPGRADE_NOTE: Object lrecnexT_seqregistauto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecnexT_seqregistauto = Nothing
		On Error GoTo 0
	End Function
	'**%Class_Initialize: Controls the creation of an instance of the class
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mstrVehBrand = String.Empty
		mstrDescript = String.Empty
		mstrVehModel = String.Empty
		mstrVehBrand = String.Empty
		mstrVehType = String.Empty
		sVehcode = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






