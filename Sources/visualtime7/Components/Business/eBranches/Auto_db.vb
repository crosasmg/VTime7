Option Strict Off
Option Explicit On
Public Class Auto_db
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto_db.cls                              $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 15                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Properties according to the table in the system on Octube 05,2001.
	'*-Propiedades según la tabla en el sistema el 05/10/2001
	'Column_name                 Type                   Computed   Length   Prec  Scale Nullable    TrimTrailingBlanks    FixedLenNullInSource
	'---------------------   ------------------------   --------   -------  ----  ----- --------    -------------------   --------------------
	Public sCertype As String '   char        no  1                   no  no  no
	Public nProduct As Integer '   smallint    no  2   5   0   no  (n/a)   (n/a)
	Public nBranch As Integer '   smallint    no  2   5   0   no  (n/a)   (n/a)
	Public nPolicy As Double '   int         no  4   10  0   no  (n/a)   (n/a)
	Public nCertif As Double '   int         no  4   10  0   no  (n/a)   (n/a)
	Public nAutoZone As Integer '   int         no  4   10  0   yes (n/a)   (n/a)
	Public dEffecdate As Date '   datetime    no  8                   no  (n/a)   (n/a)
	Public sClient As String '   char        no  14                  yes no  yes
	Public nVehType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sChassis As String '   char        no  40                  yes no  yes
	Public sColor As String '   char        no  15                  yes no  yes
	Public sLicense_ty As String '   char        no  1                   yes no  yes
	Public sMotor As String '   char        no  40                  yes no  yes
	Public sRegist As String '   char        no  10                  yes no  yes
	Public sVehCode As String '   char        no  6                   yes no  yes
	Public dCompdate As Date '   datetime    no  8                   yes (n/a)   (n/a)
	Public dExpirdat As Date '   datetime    no  8                   yes (n/a)   (n/a)
	Public dIssuedat As Date '   datetime    no  8                   yes (n/a)   (n/a)
	Public dNulldate As Date '   datetime    no  8                   yes (n/a)   (n/a)
	Public dStartDate As Date '   datetime    no  8                   yes (n/a)   (n/a)
	Public nCapital As Double '   decimal     no  9   18  6   yes (n/a)   (n/a)
	Public nPremium As Double '   decimal     no  9   10  2   yes (n/a)   (n/a)
	Public nVeh_valor As Double '   decimal     no  9   18  6   yes (n/a)   (n/a)
	Public nVal_extra As Double '   decimal     no  9   18  6   yes (n/a)   (n/a)
	Public nTransactio As Integer '   int         no  4   10  0   yes (n/a)   (n/a)
	Public nNullcode As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nUsercode As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nVehplace As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nVehpma As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nYear As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nInd0km As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sReference As String '   char        no  6                   yes no  yes
	Public nValueType As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nDiscClaim As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nDeduc As Integer '   decimal     no  5   4   2   yes (n/a)   (n/a)
	Public nUse As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nPercTabVal As Double '   decimal     no  5   8   5   yes (n/a)   (n/a)
	Public nGroup As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public dLastClaim As Date '   datetime    no  8                   yes (n/a)   (n/a)
	Public nSituation As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	
	
	
	'**-Properties according to the table in the system on December 27,2000.
	'*-Propiedades según la tabla en el sistema el 27/12/2000
	
	'Column_name                 Type                   Computed   Length   Prec  Scale Nullable    TrimTrailingBlanks    FixedLenNullInSource
	'---------------------   ------------------------   --------   -------  ----  ----- --------    -------------------   --------------------
	'    Public sRegist          As String  'char             no         10                 no               no                    no
	'    Public sChassis         As String  'char             no         40                 no               no                    no
	'    Public sMotor           As String  'char             no         40                 no               no                    no
	'    Public sClient          As String  'char             no         14                 yes              no                    yes
	'    Public sColor           As String  'char             no         15                 yes              no                    yes
	Public sVeh_own As String 'char             no         14                 yes              no                    yes
	'    Public sVehcode         As String  'char             no         6                  yes              no                    yes
	Public nVestatus As Integer 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	'    Public dCompdate        As Date    'datetime         no         8                  yes            (n/a)                  (n/a)
	Public nNoteNum As Integer 'int              no         4       10    0    yes            (n/a)                  (n/a)
	'    Public nUsercode        As long 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	'    Public nYear            As long 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	'    Public nVehType         As long 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	Public nAnualKm As Double 'decimal          no         9       12    0    yes            (n/a)                  (n/a)
	Public nActualKm As Double 'decimal          no         9       12    0    yes            (n/a)                  (n/a)
	Public nKeepVeh As Integer 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	Public nRoadType As Integer 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	Public nIndLaw As Integer 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	Public nFuelType As Integer 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	Public nIndAlarm As Integer 'smallint         no         2       5     0    yes            (n/a)                  (n/a)
	
	'**-Auxiliary properties
	'*-Propiedades Auxiliares
	
	'    Public sCertype      As String
	'    Public nBranch       As long
	'    Public nProduct      As long
	'    Public nPolicy       As Long
	'    Public nCertif       As Long
	'    Public dEffecdate    As Date
	Public sLicense_tyW As String
	Public sRegistW As String
	Public sChassisW As String
	Public sMotorW As String
	Public sDesMark As String
	Public nClaim As Double
	Public nCase_num As Integer
	Public nDeman_type As Integer
	Public nWorksh As Integer
	Public sCodispl As String
	Public sInd As String
	Public sVehmodel As String
	
	'**-Properties according to the table in the system on Octube 05,2001.
	'*-Propiedades según la tabla en el sistema el 05/10/2001
	'Column_name                 Type                   Computed   Length   Prec  Scale Nullable    TrimTrailingBlanks    FixedLenNullInSource
	'---------------------   ------------------------   --------   -------  ----  ----- --------    -------------------   --------------------
	Public sDescript As String '   char    no  30                  yes no  yes
	Public nNational As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public nVehBrand As Integer '   smallint    no  2   5   0   yes (n/a)   (n/a)
	Public sStatregt As String '   char    no  1                   yes no  yes
	Public sPolitype As String '   char        no  1                   yes no  yes SQL_Latin1_General_CP1_CI_AS
	Public nPayfreq As Integer '   smallint    no  2   5       0       yes (n/a)   (n/a)   NULL
	Public sCliename As String '   char        no  40                  yes yes yes SQL_Latin1_General_CP1_CI_AS
	Public sBranchName As String '   char    no  30                  yes no  yes
	
	'-Se define La variable lstrTab_name_b utilizada para almacenar el nombre de la tabla de Datos
	'-particulares de Auto.
	Public lstrTab_name_b As String
	
	'**%Find: This method returns TRUE or FALSE depending if the records exists in the table 'Auto_db'
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la tabla 'Auto_db'
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sregister As String, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim lrecreaAuto_db As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreaAuto_db = New eRemoteDB.Execute
		
		If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nCertif <> nCertif Or Me.dEffecdate <> dEffecdate Or lblnFind Then
			
			
			'**+Parameter definition for stored procedure 'insudb.reaAuto_db'
			'**+Information read on December 28,2000 9:22:07 a.m.
			'+Definición de parámetros para stored procedure 'insudb.reaAuto_db'
			'+Información leída el 28/12/2000 9:22:07 a.m.
			
			With lrecreaAuto_db
				.StoredProcedure = "reaAuto_db"
				.Parameters.Add("sRegist", sregister, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Find = .Run
				If Find Then
					sLicense_ty = .FieldToClass("sLicense_ty")
					sRegist = .FieldToClass("sRegist")
					sChassis = .FieldToClass("sChassis")
					sMotor = .FieldToClass("sMotor")
					sClient = .FieldToClass("sClient")
					sColor = .FieldToClass("sColor")
					sVeh_own = .FieldToClass("sVeh_own")
					sVehCode = .FieldToClass("sVehcode")
					nVestatus = .FieldToClass("nVestatus")
					dCompdate = .FieldToClass("dCompdate")
					nNoteNum = .FieldToClass("nNotenum")
					nUsercode = .FieldToClass("nUsercode")
					nYear = .FieldToClass("nYear")
					nVehType = .FieldToClass("nVehType")
					nAnualKm = .FieldToClass("nAnualKm")
					nActualKm = .FieldToClass("nActualKm")
					nKeepVeh = .FieldToClass("nKeepVeh")
					nRoadType = .FieldToClass("nRoadType")
					nIndLaw = .FieldToClass("nIndLaw")
					nFuelType = .FieldToClass("nFuelType")
					nIndAlarm = .FieldToClass("nIndAlarm")
					.RCloseRec()
				End If
			End With
		End If
		'UPGRADE_NOTE: Object lrecreaAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAuto_db = Nothing
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**%ADD: This method is in charge of adding new records to the table "Auto_db".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Auto_db". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreAuto_db As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		
		lreccreAuto_db = New eRemoteDB.Execute
		
		'**+Parameter definition for the stored procedure 'insudb.creAuto_db'
		'**+Information read on December 28,2000 9:24:08 a.m.
		'+Definición de parámetros para stored procedure 'insudb.creAuto_db'
		'+Información leída el 28/12/2000 9:24:08 a.m.
		
		With lreccreAuto_db
			.StoredProcedure = "creAuto_db"
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVeh_own", sVeh_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVestatus", nVestatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnualKm", nAnualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActualKm", nActualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeepVeh", nKeepVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoadType", nRoadType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndLaw", nIndLaw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFuelType", nFuelType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndAlarm", nIndAlarm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nControl", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lreccreAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreAuto_db = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Update: This method is in charge of updating records in the table "Auto_db".  It returns TRUE or FALSE
	'**%depending on whether the stored procedure executed correctly.
	'%Update: Este método se encarga de actualizar registros en la tabla "Auto_db". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		
		Dim lrecupdAuto_db As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdAuto_db = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.updAuto_db'
		'**+Information read on December 28, 2000  9:27:13 a.m.
		'+Definición de parámetros para stored procedure 'insudb.updAuto_db'
		'+Información leída el 28/12/2000 9:27:13 a.m.
		
		With lrecupdAuto_db
			.StoredProcedure = "updAuto_db"
			.Parameters.Add("sLicense_tyW", sLicense_tyW, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegistW", sRegistW, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassisW", sChassisW, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotorW", sMotorW, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVeh_own", sVeh_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVestatus", nVestatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnualKm", nAnualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActualKm", nActualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeepVeh", nKeepVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoadType", nRoadType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndLaw", nIndLaw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFuelType", nFuelType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndAlarm", nIndAlarm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdAuto_db = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Find_db1: This method returns TRUE or FALSE depending if the records exists in the table 'Auto_db'
	'%Find_db1: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la tabla 'Auto_db'
	Public Function Find_db1(ByVal sLicense_ty As String, ByVal sRegist As String, ByVal sChassis As String, ByVal sMotor As String, Optional ByRef lblnFind As Boolean = False) As Boolean
		Dim lrecinsReaAuto_db1 As eRemoteDB.Execute
		
		On Error GoTo Find_db1_Err
		lrecinsReaAuto_db1 = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.insReaAuto_db1'
		'**+Information read on January 03,2001 2:32:45 p.m.
		'+Definición de parámetros para stored procedure 'insudb.insReaAuto_db1'
		'+Información leída el 03/01/2001 2:32:45 p.m.
		With lrecinsReaAuto_db1
			.StoredProcedure = "insReaAuto_db1"
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Find_db1 = .Run
			If Find_db1 Then
				sLicense_ty = .FieldToClass("sLicense_ty")
				sRegist = .FieldToClass("sRegist")
				sChassis = .FieldToClass("sChassis")
				sMotor = .FieldToClass("sMotor")
				sClient = .FieldToClass("sClient")
				sColor = .FieldToClass("sColor")
				sVeh_own = .FieldToClass("sVeh_own")
				sVehCode = .FieldToClass("sVehcode")
				nVestatus = .FieldToClass("nVestatus")
				nNoteNum = .FieldToClass("nNoteNum")
				nYear = .FieldToClass("nYear")
				nVehType = .FieldToClass("nVehType")
				nAnualKm = .FieldToClass("nAnualKm")
				nActualKm = .FieldToClass("nActualKm")
				nKeepVeh = .FieldToClass("nKeepVeh")
				nRoadType = .FieldToClass("nRoadType")
				nIndLaw = .FieldToClass("nIndLaw")
				nFuelType = .FieldToClass("nFuelType")
				nIndAlarm = .FieldToClass("nIndAlarm")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsReaAuto_db1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaAuto_db1 = Nothing
		
Find_db1_Err: 
		If Err.Number Then
			Find_db1 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%Find_AutoDB_Exists: This method returns TRUE or FALSE depending if the records exists in the table 'Auto_db'
	'%Find_AutoDB_Exists: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la tabla 'Auto_db'
	Public Function Find_AutoDB_Exists(ByVal lintType As Integer, ByVal lstrCodReg As String, ByVal lstrCharFind As String) As Boolean
		Dim lrecreaAutoDB_Exists As eRemoteDB.Execute
		
		On Error GoTo Find_AutoDB_Exists_Err
		
		lrecreaAutoDB_Exists = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.reaAutoDB_Exist'
		'**+Information read on January 22,2001  10:39:31 a.m.
		'+Definición de parámetros para stored procedure 'insudb.reaAutoDB_Exists'
		'+Información leída el 22/01/2001 10:39:31 AM
		With lrecreaAutoDB_Exists
			.StoredProcedure = "reaAutoDB_Exists"
			.Parameters.Add("nType", lintType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodReg", lstrCodReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCharFind", lstrCharFind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				sLicense_ty = .FieldToClass("sLicense_ty")
				sRegist = .FieldToClass("sRegist")
				sChassis = .FieldToClass("sChassis")
				nVestatus = .FieldToClass("nVestatus")
				sMotor = .FieldToClass("sMotor")
				sColor = .FieldToClass("sColor")
				sVehCode = .FieldToClass("sVehcode")
				nYear = .FieldToClass("nYear")
				nVehType = .FieldToClass("nVehType")
				sVehmodel = .FieldToClass("sVehmodel")
				sDesMark = .FieldToClass("sDesMark")
				Find_AutoDB_Exists = True
				.RCloseRec()
			Else
				Find_AutoDB_Exists = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaAutoDB_Exists may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaAutoDB_Exists = Nothing
		
Find_AutoDB_Exists_Err: 
		If Err.Number Then
			Find_AutoDB_Exists = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insUpdatedAuto_db: This method returns TRUE when it successfully updates a record in the table 'Auto_db'
	'%insUpdatedAuto_db: Función que retorna VERDADERO al actualizar un registro en la tabla 'Auto_db'
	Public Function insUpdatedAuto_db() As Boolean
		Dim lrecinsUpdAuto_db As eRemoteDB.Execute
		
		On Error GoTo insUpdatedAuto_db_Err
		
		lrecinsUpdAuto_db = New eRemoteDB.Execute
		
		'**+Parameter definition for tored procedure 'insudb.insUpdAuto_dp'
		'**+Information read on January 22,2001 11:58:10 a.m.
		'+Definición de parámetros para stored procedure 'insudb.insUpdAuto_db'
		'+Información leída el 22/01/2001 11:58:10 AM
		With lrecinsUpdAuto_db
			.StoredProcedure = "insUpdAuto_db"
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVeh_own", sVeh_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVestatus", nVestatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNoteNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehType", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnualKm", nAnualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActualKm", nActualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeepVeh", nKeepVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoadType", nRoadType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndLaw", nIndLaw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFuelType", nFuelType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndAlarm", nIndAlarm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCase_num", nCase_num, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDeman_type", nDeman_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWorksh", nWorksh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd", sInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdatedAuto_db = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsUpdAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdAuto_db = Nothing
		
insUpdatedAuto_db_Err: 
		If Err.Number Then
			insUpdatedAuto_db = False
		End If
		On Error GoTo 0
	End Function
	
	
	
	
	'%insVerifyBranch: Esta rutina permite verificar si el ramo es de Vida o Auto.
	Public Function insVerifyBranch(ByVal lintBranch As Integer, ByVal lstrBrancht As String) As Boolean
		Dim lrecTime As eRemoteDB.Execute
		
		lrecTime = New eRemoteDB.Execute
		With lrecTime
			.StoredProcedure = "reaProdmasterBPKG.reaProdmasterB"
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", lstrBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insVerifyBranch = .Run
		End With
		
insVerifyBranch_Err: 
		If Err.Number Then
			insVerifyBranch = False
		End If
		'UPGRADE_NOTE: Object lrecTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTime = Nothing
	End Function
	
	
	'%insValFolder: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insVal_AUC001_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPayfreq As Integer, ByVal nCapital As Double, ByVal nPremium As Double, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTypePolicy As Integer, ByVal nLicense As Integer, ByVal sregister As String, ByVal sMotor As String, ByVal sChassis As String, ByVal sColor As String, ByVal nVehMark As Integer, ByVal sVehmodel As String, ByVal nType As Integer, ByVal nZone As Integer) As String
		Dim lerrTime As eFunctions.Errors
		Dim lobjValues As eFunctions.Values
		Dim lAuto_db As eBranches.Auto_db
		
		'-Se define la variable lstrBrancht utilizada para almacenar el valor del ramo técnico.
		Dim lstrBrancht As String
		
		lerrTime = New eFunctions.Errors
		lobjValues = New eFunctions.Values
		lAuto_db = New eBranches.Auto_db
		
		On Error GoTo insVal_AUC001_K_Err
		
		'+Validación del campo Ramo.
		
		If nBranch < 0 Then
			Call lerrTime.ErrorMessage(sCodispl, 1022)
		Else
			If Not lAuto_db.insVerifyBranch(nBranch, "('3')") Then
				Call lerrTime.ErrorMessage(sCodispl, 3967)
			End If
		End If
		
		insVal_AUC001_K = lerrTime.Confirm
		
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lobjValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValues = Nothing
		'UPGRADE_NOTE: Object lAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lAuto_db = Nothing
		
insVal_AUC001_K_Err: 
		If Err.Number Then
			insVal_AUC001_K = insVal_AUC001_K & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	
	'%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la tabla 'Auto_db'
	Public Function FindReapolicyAuto(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
		
		Dim lrecinsReapolicyAuto As eRemoteDB.Execute
		
		lrecinsReapolicyAuto = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insReapolicyAuto'
		'Información leída el 11/10/2001 09:30:32 a.m.
		
		With lrecinsReapolicyAuto
			.StoredProcedure = "insReapolicyAuto"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				dEffecdate = .FieldToClass("dEffecdate")
				dExpirdat = .FieldToClass("dExpirdat")
				nBranch = .FieldToClass("nBranch")
				nCapital = .FieldToClass("nCapital")
				nCertif = .FieldToClass("nCertif")
				nPayfreq = .FieldToClass("nPayFreq")
				nPolicy = .FieldToClass("nPolicy")
				nPremium = .FieldToClass("nPremium")
				nVehBrand = .FieldToClass("nVehBrand")
				nVehType = .FieldToClass("nVehtype")
				nAutoZone = .FieldToClass("nAutoZone")
				sBranchName = .FieldToClass("sBranchName")
				sChassis = .FieldToClass("sChassis")
				sCliename = .FieldToClass("sClieName")
				sColor = .FieldToClass("sColor")
				sDescript = .FieldToClass("sDescript")
				sLicense_ty = .FieldToClass("sLicense_ty")
				sMotor = .FieldToClass("sMotor")
				sPolitype = .FieldToClass("sPolitype")
				sRegist = .FieldToClass("sRegist")
				sVehmodel = .FieldToClass("sVehmodel")
				.RCloseRec()
				FindReapolicyAuto = True
			Else
				FindReapolicyAuto = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinsReapolicyAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReapolicyAuto = Nothing
		
		
		
		
	End Function
End Class






