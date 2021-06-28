Option Strict Off
Option Explicit On
Public Class Fire
	'%-------------------------------------------------------%'
	'% $Workfile:: Fire.cls                                 $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'**+Properties according the table in the system on 12/14/2000
	'+ Propiedades según la tabla en el sistema el 14/12/2000
	
	'Column_name                     Type              Computed   Length    Prec  Scale  Nullable   TrimTrailingBlanks   FixedLenNullInSource
	'----------------------  ----------------------- ----------- ---------  ----  -----  --------   ------------------   ---------------------
	Public sCertype As String 'char         no         1       no     no      no
	Public sCodispl As String 'char         no         1       no     no      no
	Public nBranch As Integer 'smallint     no         2        5     0       no            (n/a)                  (n/a)
	Public nProduct As Integer 'smallint     no         2        5     0       no            (n/a)                  (n/a)
	Public nPolicy As Double 'int          no         4       10     0       no            (n/a)                  (n/a)
	Public nCertif As Double 'int          no         4       10     0       no            (n/a)                  (n/a)
	Public dEffecdate As Date 'datetime     no         8                      no            (n/a)                  (n/a)
	Public nCapital As Double 'decimal      no         9       12     0       yes           (n/a)                  (n/a)
	Public nSpCombType As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nArticle As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nBuildType As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nActivityType As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nFamily As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nSeismicZone As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nSideCloseType As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nCl_risk As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nRoofType As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public dCompdate As Date 'datetime     no         8                      yes           (n/a)                  (n/a)
	Public sClient As String 'char         no        14                      yes             no                    yes
	Public sDecla_type As String 'char         no         1                      yes             no                    yes
	Public nDep_prem As Double 'decimal      no         9       10     2       yes           (n/a)                  (n/a)
	Public dExpirdat As Date 'datetime     no         8                      yes           (n/a)                  (n/a)
	Public sFactu_freq As String 'char         no         1                      yes             no                    yes
	Public nDetailArt As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nFloor_quan As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public dIssuedat As Date 'datetime     no         8                      yes           (n/a)                  (n/a)
	Public sLocat_risk As String 'char         no         1                      yes             no                    yes
	Public nNullcode As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public dNulldate As Date 'datetime     no         8                      yes           (n/a)                  (n/a)
	Public sPopulat_ty As String 'char         no         1                      yes             no                    yes
	Public nPremium As Double 'decimal      no         9       10     2       yes           (n/a)                  (n/a)
	Public nProm_rate As Double 'decimal      no         5        4     2       yes           (n/a)                  (n/a)
	Public dStartdate As Date 'datetime     no         8                      yes           (n/a)                  (n/a)
	Public nUsercode As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nIndPeriod As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nConstCat As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nTransactio As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nActivityCat As Integer 'smallint     no         2        5     0       yes           (n/a)                  (n/a)
	Public nAnt_prem As Double 'decimal      no         5        3     2       yes           (n/a)                  (n/a)
	Public sDecla_freq As String 'char         no         1                      yes             no                    yes
	
	'**+Additional properties
	'+Propiedades adicionales
	
	'**% Update: This function returns TRUE after the updating of a record in the table "Fire"
	'% Update: Función que retorna VERDADERO al actualizar un registro en la tabla 'Fire'
	Public Function Update() As Boolean
		
		Dim lrecupdFire As eRemoteDB.Execute
		
		lrecupdFire = New eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		'**+Stored procedure parameters definition 'insudb.reaDoc_Quotation'
		'**+Data of 12/4/2000 2:52:54 p.m.
		'Definición de parámetros para stored procedure 'insudb.updFire'
		'Información leída el 14/12/2000 2:52:54 p.m.
		
		With lrecupdFire
			.StoredProcedure = "updFire"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nConstCat", nConstCat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFloor_quan", nFloor_quan, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSpCombType", nSpCombType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSideCloseType", nSideCloseType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndPeriod", nIndPeriod, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoofType", nRoofType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBuildType", nBuildType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeismicZone", nSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnt_Prem", nAnt_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 3, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDep_prem", nDep_prem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTransactio", nTransactio, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFamily", nFamily, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActivityType", nActivityType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDecla_Freq", sDecla_freq, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDecla_Type", sDecla_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdFire may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdFire = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'% Find: This function reads the table "Fire" and returns TRUE if the data was found
	'% Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Fire'
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean

        Dim lrecreaCertificnn As eRemoteDB.Execute

        lrecreaCertificnn = New eRemoteDB.Execute

        On Error GoTo Find_Err

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or lblnFind Then

            '**+Stored procedure parameters definition 'insudb.reaCertificnn'
            '**+Data of 12/19/2000 10:19:01 a.m.
            'Definición de parámetros para stored procedure 'insudb.reaCertificnn'
            'Información leída el 19/12/2000 10:19:01 a.m.

            With lrecreaCertificnn
                .StoredProcedure = "reaCertificnn"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    sCertype = .FieldToClass("sCertype")
                    nBranch = .FieldToClass("nBranch")
                    nProduct = .FieldToClass("nProduct")
                    nPolicy = .FieldToClass("nPolicy")
                    nCertif = .FieldToClass("nCertif")
                    dEffecdate = .FieldToClass("dEffecdate")
                    nCapital = .FieldToClass("nCapital")
                    nSpCombType = .FieldToClass("nSpCombType")
                    nArticle = .FieldToClass("nArticle")
                    nBuildType = .FieldToClass("nBuildType")
                    nActivityType = .FieldToClass("nActivityType")
                    nFamily = .FieldToClass("nFamily")
                    nSeismicZone = .FieldToClass("nSeismicZone")
                    nSideCloseType = .FieldToClass("nSideCloseType")
                    nCl_risk = .FieldToClass("nCl_risk")
                    nRoofType = .FieldToClass("nRoofType")
                    dCompdate = .FieldToClass("dCompdate")
                    sClient = .FieldToClass("sClient")
                    sDecla_type = .FieldToClass("sDecla_type")
                    nDep_prem = .FieldToClass("nDep_prem")
                    dExpirdat = .FieldToClass("dExpirdat")
                    sFactu_freq = .FieldToClass("sFactu_freq")
                    nDetailArt = .FieldToClass("nDetailArt")
                    nFloor_quan = .FieldToClass("nFloor_quan")
                    dIssuedat = .FieldToClass("dIssuedat")
                    sLocat_risk = .FieldToClass("sLocat_risk")
                    nNullcode = .FieldToClass("nNullcode")
                    dNulldate = .FieldToClass("dNulldate")
                    sPopulat_ty = .FieldToClass("sPopulat_ty")
                    nPremium = .FieldToClass("nPremium")
                    nProm_rate = .FieldToClass("nProm_rate")
                    dStartdate = .FieldToClass("dStartdate")
                    nUsercode = .FieldToClass("nUsercode")
                    nIndPeriod = .FieldToClass("nIndPeriod")
                    nConstCat = .FieldToClass("nConstCat")
                    nTransactio = .FieldToClass("nTransactio")
                    nActivityCat = .FieldToClass("nActivityCat")
                    nAnt_prem = .FieldToClass("nAnt_prem")
                    sDecla_freq = .FieldToClass("sDecla_freq")
                    .RCloseRec()
                End If
            End With
        End If
        'UPGRADE_NOTE: Object lrecreaCertificnn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaCertificnn = Nothing

Find_Err:
        If Err.Number Then
            Find = False
        End If
    End Function
	
	'**% Find_DetArt: This routine reads the table of type of business
	'% Find_DetArt: Esta función lee la tabla de tipo de negocio
    Public Function Find_DetArt(ByVal nArticle As Integer, ByVal nDetailArt As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean

        Dim lrecreaTab_In_Bus_DetArt As eRemoteDB.Execute

        lrecreaTab_In_Bus_DetArt = New eRemoteDB.Execute

        On Error GoTo Find_DetArt_Err

        '**+Stored procedure parameters definition 'insudb.reaTab_In_Bus_DetArt'
        '**+Data of 12/19/2000 10:47:28 a.m.
        'Definición de parámetros para stored procedure 'insudb.reaTab_In_Bus_DetArt'
        'Información leída el 19/12/2000 10:47:28 a.m.

        With lrecreaTab_In_Bus_DetArt
            .StoredProcedure = "reaTab_In_Bus_DetArt"
            .Parameters.Add("nArticle", nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDetailArt", nDetailArt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_DetArt = .Run
            If Find_DetArt Then
                nFamily = .FieldToClass("nFamily")
                nActivityType = .FieldToClass("nActivityType")
                .RCloseRec()
            End If
        End With
        'UPGRADE_NOTE: Object lrecreaTab_In_Bus_DetArt may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_In_Bus_DetArt = Nothing

Find_DetArt_Err:
        If Err.Number Then
            Find_DetArt = False
        End If
    End Function
End Class






