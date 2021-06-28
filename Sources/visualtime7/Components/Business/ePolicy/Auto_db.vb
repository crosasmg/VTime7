Option Strict Off
Option Explicit On
Public Class Auto_db
	'%-------------------------------------------------------%'
	'% $Workfile:: Auto_db.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 26/07/04 6:13p                               $%'
	'% $Revision:: 52                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla INSUDB.Auto_db al 05-24-2002 10:23:17
	'+         Property                Type         DBType   Size Scale  Prec  Null
	'+-----------------------------------------------------------------------------
	Public sLicense_ty As String ' CHAR       1    0     0    N
	Public sRegist As String ' CHAR       10   0     0    N
	Public sChassis As String ' CHAR       40   0     0    S
	Public sMotor As String ' CHAR       40   0     0    S
	Public sVeh_own As String ' CHAR       14   0     0    S
	Public sClient As String ' CHAR       14   0     0    S
	Public sColor As String ' CHAR       15   0     0    S
	Public sVehcode As String ' CHAR       6    0     0    S
	Public nVestatus As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nNotenum As Double ' NUMBER     22   0     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public nYear As Integer ' NUMBER     22   0     5    S
	Public nVehType As Integer ' NUMBER     22   0     5    S
	Public nAnualKm As Double ' NUMBER     22   0     12   S
	Public nActualKm As Double ' NUMBER     22   0     12   S
	Public nKeepVeh As Integer ' NUMBER     22   0     5    S
	Public nRoadType As Integer ' NUMBER     22   0     5    S
	Public nIndLaw As Integer ' NUMBER     22   0     5    S
	Public nFuelType As Integer ' NUMBER     22   0     5    S
	Public nIndAlarm As Integer ' NUMBER     22   0     5    S
	Public sDigit As String ' CHAR       1    0     0    N
	Public nLic_special As Integer ' NUMBER     22   0     5    S
	Public nGroupVeh As Integer ' NUMBER     22   0     5
	'- Assist properties
	'- Propiedades Auxiliares
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public dEffecdate As Date
	Public sLicense_tyW As String
	Public sRegistW As String
	Public sChassisW As String
	Public sMotorW As String
	Public nVehBrand As String
	Public nAction As Integer
	Public nValue As Double
	Public sClientName As String
	Public sVehownName As String
	
	Private mstrDescript As String
	Private mstrVehType As String
	Private mstrVehModel As String
	Private mstrVehBrand As String
	
	'- Keep the string for the condition search
	'- Se almacena el string para la búsqueda por condición
	Private mstrCondition As String
	
	'% sCodition: returns the string for the condition search
	'% sCodition: devuelve el string para la búsqueda por condición
	Public ReadOnly Property sCodition() As String
		Get
			sCodition = mstrCondition
		End Get
	End Property
	
	
	'%sDescript: This property returns the description
	'%sDescript: Esta propiedad retorna la descripcion
	Public ReadOnly Property sDescript() As String
		Get
			Call insGetValues()
			sDescript = mstrDescript
		End Get
	End Property
	
	'%sVehModel: This property returns the model of the vehicle
	'%sVehModel: Esta propiedad retorna el modelo del vehiculo
	Public ReadOnly Property sVehModel() As String
		Get
			Call insGetValues()
			sVehModel = mstrVehModel
		End Get
	End Property
	
	'%sVehBrand: This property returns the brand of the vehicle
	'%sVehBrand: Esta propiedad retorna la marca del vehiculo
	Public ReadOnly Property sVehBrand() As String
		Get
			Dim lobjQuery As eRemoteDB.Query
			If mstrVehBrand = String.Empty Then
				Call insGetValues()
				If CStr(nVehBrand) <> String.Empty Then
					lobjQuery = New eRemoteDB.Query
					If lobjQuery.OpenQuery("table7042", "sDescript", "nVehBrand = " & CStr(nVehBrand)) Then
						mstrVehBrand = lobjQuery.FieldToClass("sDescript")
					End If
					'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					lobjQuery = Nothing
				End If
			End If
			sVehBrand = mstrVehBrand
		End Get
	End Property
	
	'%sVehType: This property returns the type of vehicle
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
	
	'%Find: Function that returns TRUE to make the reading of the tecords in the 'Auto_db' table
	'%Find: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto_db'
    Public Function Find(ByVal sRegist As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecreaAuto_db As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaAuto_db = New eRemoteDB.Execute

        If lblnFind Then

            mstrVehBrand = String.Empty
            mstrDescript = String.Empty
            mstrVehModel = String.Empty
            mstrVehBrand = String.Empty

            '+ Parameters definition to stored procedure 'insudb.reaAuto_db'
            '+ Definición de parámetros para stored procedure 'insudb.reaAuto_db'

            With lrecreaAuto_db
                .StoredProcedure = "reaAuto_db"
                .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Find = .Run
                If Find Then
                    sLicense_ty = .FieldToClass("sLicense_ty")
                    sRegist = .FieldToClass("sRegist")
                    sChassis = .FieldToClass("sChassis")
                    sMotor = .FieldToClass("sMotor")
                    sClient = .FieldToClass("sClient")
                    sColor = .FieldToClass("sColor")
                    sVeh_own = .FieldToClass("sVeh_own")
                    sVehcode = .FieldToClass("sVehcode")
                    nVestatus = .FieldToClass("nVestatus")
                    dCompdate = .FieldToClass("dCompdate")
                    nNotenum = .FieldToClass("nNotenum")
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
					nGroupVeh = .FieldToClass("nGroupVeh")
                    .RCloseRec()
                End If
            End With
        End If

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaAuto_db = Nothing
    End Function
	
	'%Add: Function taht returns TRUE to insert a records in the 'Auto_db' table
	'%Add: Función que retorna VERDADERO al insertar un registro en la tabla 'Auto_db'
	Public Function Add() As Boolean
		Dim lreccreAuto_db As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreAuto_db = New eRemoteDB.Execute
		
		'+ Parameters definition to stored procedure 'insudb.creAuto_db'
		'+ Definición de parámetros para stored procedure 'insudb.creAuto_db'
		
		With lreccreAuto_db
			.StoredProcedure = "creAuto_db"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVeh_own", sVeh_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVestatus", nVestatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehtype", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnualkm", nAnualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActualkm", nActualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeepveh", nKeepVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoadtype", nRoadType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndlaw", nIndLaw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFueltype", nFuelType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndalarm", nIndAlarm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nControl", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLic_special", nLic_special, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroupVeh", nGroupVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreAuto_db = Nothing
		On Error GoTo 0
	End Function
	
	'%Update: Function that returns TRUE to update a records in the 'Auto_db' table
	'%Update: Función que retorna VERDADERO al actualizar un registro en la tabla 'Auto_db'
	Public Function Update() As Boolean
		
		Dim lrecupdAuto_db As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdAuto_db = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updAuto_db'
		'+ Información leída el 28/12/2000 9:27:13 a.m.
		
		With lrecupdAuto_db
			.StoredProcedure = "updAuto_db"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChassis", sChassis, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMotor", sMotor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sColor", sColor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVeh_own", sVeh_own, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVehcode", sVehcode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVestatus", nVestatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nVehtype", nVehType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAnualkm", nAnualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nActualkm", nActualKm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nKeepveh", nKeepVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRoadtype", nRoadType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndlaw", nIndLaw, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFueltype", nFuelType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndalarm", nIndAlarm, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDigit", sDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLic_special", nLic_special, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroupVeh", nGroupVeh, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecupdAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdAuto_db = Nothing
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdAuto_db = Nothing
		On Error GoTo 0
	End Function
	'%Find_db1: Function that returns TRUE to make reading of the records in the 'Auto_db' table
	'%Find_db1: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto_db'
    Public Function Find_db1(ByVal sLicense_ty As String, ByVal sRegist As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecinsReaAuto_db1 As eRemoteDB.Execute

        On Error GoTo Find_db1_Err

        lrecinsReaAuto_db1 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insReaAuto_db1'
        '+ Información leída el 03/01/2001 2:32:45 p.m.

        With lrecinsReaAuto_db1
            .StoredProcedure = "insReaAuto_db1"
            .Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_db1 = .Run
            If Find_db1 Then
                Me.sLicense_ty = .FieldToClass("sLicense_ty")
                Me.sRegist = .FieldToClass("sRegist")
                Me.sChassis = .FieldToClass("sChassis")
                Me.sMotor = .FieldToClass("sMotor")
                Me.sClient = .FieldToClass("sClient")
                Me.sColor = .FieldToClass("sColor")
                Me.sVeh_own = .FieldToClass("sVeh_own")
                Me.sVehcode = .FieldToClass("sVehcode")
                Me.nVestatus = .FieldToClass("nVestatus")
                Me.nNotenum = .FieldToClass("nNoteNum")
                Me.nYear = .FieldToClass("nYear")
                Me.nVehType = .FieldToClass("nVehType")
                Me.nAnualKm = .FieldToClass("nAnualKm")
                Me.nActualKm = .FieldToClass("nActualKm")
                Me.nKeepVeh = .FieldToClass("nKeepVeh")
                Me.nRoadType = .FieldToClass("nRoadType")
                Me.nIndLaw = .FieldToClass("nIndLaw")
                Me.nFuelType = .FieldToClass("nFuelType")
                Me.nIndAlarm = .FieldToClass("nIndAlarm")
                Me.sDigit = .FieldToClass("sDigit")
                Me.nLic_special = .FieldToClass("nLic_special")
				Me.nGroupVeh = .FieldToClass("nGroupVeh")
                .RCloseRec()
            End If
        End With

Find_db1_Err:
        If Err.Number Then
            Find_db1 = False
        End If
        'UPGRADE_NOTE: Object lrecinsReaAuto_db1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReaAuto_db1 = Nothing
        On Error GoTo 0
    End Function
	
	'%Find_db1: Function that returns TRUE to make reading of the records in the 'Auto_db' table
	'%Exist_db1: Función que retorna VERDADERO realizar la lectura de registros en la tabla 'Auto_db'
    Public Function Exist_db1(ByVal sLicense_ty As String, ByVal sRegist As String, Optional ByVal lblnFind As Boolean = False) As Boolean
        Dim lrecinsReaAuto_db1 As eRemoteDB.Execute

        On Error GoTo Exist_db1_Err

        lrecinsReaAuto_db1 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insReaAuto_db1'
        '+ Información leída el 03/01/2001 2:32:45 p.m.

        With lrecinsReaAuto_db1
            .StoredProcedure = "insValAuto_db"
            .Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Exist_db1 = (.Parameters("nExists").Value = 1)
        End With

Exist_db1_Err:
        If Err.Number Then
            Exist_db1 = False
        End If
        'UPGRADE_NOTE: Object lrecinsReaAuto_db1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsReaAuto_db1 = Nothing
        On Error GoTo 0
    End Function

    '%insGetValues: This method obtains the values of the vehicle
    '%insGetValues: Este metodo se encarga de obtener los valores del vehiculo
    Private Function insGetValues() As Object
        Dim lobjQuery As eRemoteDB.Query = New eRemoteDB.Query
        If mstrDescript = String.Empty And sVehcode <> String.Empty Then
            lobjQuery = New eRemoteDB.Query
            If lobjQuery.OpenQuery("tab_au_veh", "sDescript,nVehBrand,sVehModel,nVehType", "sVehCode = '" & "" & CStr(sVehcode) & "'") Then
                With lobjQuery
                    Me.nVehBrand = .FieldToClass("nVehBrand")
                    mstrDescript = .FieldToClass("sDescript")
                    mstrVehModel = .FieldToClass("sVehModel")
                End With
            End If
        End If
        Return lobjQuery
        'UPGRADE_NOTE: Object lobjQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjQuery = Nothing
    End Function

    '%Class_Initialize: Controls the creation of an instance of the class
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
	
	'%insValBV001_k: This function is in charge of validate the data introduced the form BV001_k (Header).
	'%insValBV001_k: Esta función se encarga de validar los datos introducidos la forma BV001_k (Header).
	Public Function insValBV001_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sRegist As String, ByVal sLicence As String, ByVal sMotor As String, ByVal sChassis As String) As String
		Dim lobjErrors As Object
		Dim lclsAuto As ePolicy.Automobile
		
		On Error GoTo insValBV001_k_Err
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lclsAuto = New ePolicy.Automobile
		
		'+ Se valida que el campo motor esté lleno y no se encuentre duplicado en el archivo
		'+ si la acción seleccionada es registrar
		If Trim(sMotor) = String.Empty And Trim(sChassis) = String.Empty And Trim(sRegist) = String.Empty And nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
			Call lobjErrors.ErrorMessage(sCodispl, 3951)
		End If
		
		'+ Se valida el campo motor, el cual debe estar lleno cuado la acción es registrar
		'+ y valida que el motor no este registrado en otro vehículo
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			If Trim(sMotor) = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 3850)
			Else
				If insValExistFields(Trim(sMotor), 1) Then
					Call lobjErrors.ErrorMessage(sCodispl, 3848)
				End If
			End If
		End If
		
		'+ Se valida que el campo chásis esté lleno y no se encuentre duplicado en el archivo
		'+ si la acción seleccionada es registrar
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			If Trim(sChassis) = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 3116)
			Else
				If insValExistFields(Trim(sChassis), 2) And Trim(sChassis) <> String.Empty Then
					Call lobjErrors.ErrorMessage(sCodispl, 3488)
				End If
			End If
		End If
		
		'+ Se valida que el campo placa esté lleno, cumpla con el formato y no se encuentre
		'+ duplicado en el archivo si la acción seleccionada es registrar
        If Trim(sRegist) = String.Empty Then
            If Not nAction = eFunctions.Menues.TypeActions.clngActionQuery Then
                Call lobjErrors.ErrorMessage(sCodispl, 3121)
            End If
        Else
            If sLicence = "1" Then
                If Not lclsAuto.ValStructRegist(Trim(sRegist)) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3122)
                End If
            End If

            If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                If insValExistFields(Trim(sRegist), 3) Then
                    Call lobjErrors.ErrorMessage(sCodispl, 3845)
                End If
            End If
        End If
		
		insValBV001_k = lobjErrors.Confirm
		
insValBV001_k_Err: 
		If Err.Number Then
			insValBV001_k = insValBV001_k & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto = Nothing
		On Error GoTo 0
	End Function
	
	'%insValExistFields: This function validates if the field passed like a parameter "lstrstring"
	'%                   is included in the database
	'%insValExistFields: Esta función se encarga validar si el campo pasado como parámetro (lstrstring)
	'%                   se encuentra ya incluído en la Base de Datos
    Public Function insValExistFields(ByVal sFields As String, ByVal nControl As Integer) As Boolean
        '- Variable define to the execution of the SP
        '- Se define la variable para la ejecución del SP
        Dim lrecvalExistFieldsAuto_db As eRemoteDB.Execute

        On Error GoTo insValExistFields_Err

        lrecvalExistFieldsAuto_db = New eRemoteDB.Execute

        insValExistFields = True

        '+ Definición de parámetros para stored procedure 'insudb.valExistFieldsAuto_db'
        With lrecvalExistFieldsAuto_db
            .StoredProcedure = "valExistFieldsAuto_db"
            .Parameters.Add("sField", sFields, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 40, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nControl", nControl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sLicense_ty = .FieldToClass("sLicense_ty")
                sRegist = .FieldToClass("sRegist")
                sChassis = .FieldToClass("sChassis")
                sMotor = .FieldToClass("sMotor")
                sVeh_own = .FieldToClass("sVeh_own")
                sClient = .FieldToClass("sClient")
                sColor = .FieldToClass("sColor")
                sVehcode = .FieldToClass("sVehcode")
                nVestatus = .FieldToClass("nVestatus")
                nNotenum = .FieldToClass("nNotenum")
                nYear = .FieldToClass("nYear")
                nVehType = .FieldToClass("nVehType")
                nAnualKm = .FieldToClass("nAnualKm")
                nActualKm = .FieldToClass("nActualKm")
                nKeepVeh = .FieldToClass("nKeepVeh")
                nRoadType = .FieldToClass("nRoadType")
                nIndLaw = .FieldToClass("nIndLaw")
                nFuelType = .FieldToClass("nFuelType")
                nIndAlarm = .FieldToClass("nIndAlarm")
                sDigit = .FieldToClass("sDigit")
                nLic_special = .FieldToClass("nLic_special")
                sClientName = .FieldToClass("sClientName")
                sVehownName = .FieldToClass("sVehownName")
                insValExistFields = True
                .RCloseRec()
            Else
                insValExistFields = False
            End If
        End With

insValExistFields_Err:
        If Err.Number Then
            insValExistFields = False
        End If
        'UPGRADE_NOTE: Object lrecvalExistFieldsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecvalExistFieldsAuto_db = Nothing
        On Error GoTo 0
    End Function
	
	'%insValBV001: This function is in charge of validating the data introduced in the form BV001 (Folder).
	'%insValBV001: Esta función se encarga de validar los datos introducidos en la forma BV001 (Folder).
	Public Function insValBV001(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sVeh_own As String, ByVal nYear As Integer, ByVal sVehcode As String, ByVal sColor As String, ByVal nVeh_status As Integer) As String
		
		Dim lobjErrors As Object
		Dim lobjValNum As Object
		Dim lclsClient As eClient.Client
		Dim lclsClient_val As eClient.ValClient
		
		On Error GoTo insValBV001_Err
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lobjValNum = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.valField")
		
		'+ execute the validations if the action is not query
		'+ Se ejecutan las validaciones sólo si la acción es diferente de consultar.
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			
			'+ The clients code cannot be without contents
			'+ El codigo del cliente no puede estar sin contenido.
			If Trim(sVeh_own) = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 2001)
			End If
			
			'+ Validate that the year field is full and that is valid for one year
			'+ Se valida que el campo año esté lleno y que sea válido para un año
			If nYear = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 3114)
			Else
				If nYear = 0 Then
					Call lobjErrors.ErrorMessage(sCodispl, 3114)
				Else
					With lobjValNum
						.Min = 1800
						.Max = 9999
					End With
				End If
			End If
			
			'+ Validate that the code field is full
			'+ Se valida que el campo código esté lleno
			If sVehcode = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 3380)
			End If
			
			'+ Validate that the color field is full
			'+ Se valida que el campo color esté lleno
			If sColor = String.Empty Then
				Call lobjErrors.ErrorMessage(sCodispl, 3386)
			End If
			
			'+ Validate that the estate field is full
			'+ Se valida que el campo estado esté lleno
			If nVeh_status = 0 Or nVeh_status = eRemoteDB.Constants.intNull Then
				Call lobjErrors.ErrorMessage(sCodispl, 1922)
			End If
		End If
		
		insValBV001 = lobjErrors.Confirm
		
insValBV001_Err: 
		If Err.Number Then
			insValBV001 = insValBV001 & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lobjValNum may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjValNum = Nothing
		On Error GoTo 0
	End Function
	
	'% insPostBV001: This function is in charge to validate all data introduced in the form BV001
	'% insPostBV001: Esta función se encarga de validar todos los datos introducidos en la forma BV001
	Public Function insPostBV001(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nUsercode As Integer, ByVal sLicense_ty As String, ByVal sRegist As String, ByVal sChassis As String, ByVal sMotor As String, ByVal sColor As String, ByVal sVehcode As String, ByVal nYear As Integer, ByVal sClient As String, ByVal sVeh_own As String, ByVal nVestatus As Integer, ByVal nVehType As Integer, ByVal nNotenum As Double, ByVal sDigit As String, ByVal nLic_special As Integer) As Boolean
		Dim lclsAuto_db As ePolicy.Auto_db
		
		On Error GoTo insPostBV001_Err
		
		lclsAuto_db = New ePolicy.Auto_db
		
		insPostBV001 = True
		
		With lclsAuto_db
			.nAction = nAction
			.nUsercode = nUsercode
			.sLicense_ty = sLicense_ty
			.sRegist = sRegist
			.sChassis = sChassis
			.sMotor = sMotor
			.sColor = sColor
			.sVehcode = sVehcode
			.nYear = nYear
			.sClient = sClient
			.sVeh_own = sVeh_own
			.nVestatus = nVestatus
			.nVehType = nVehType
			.nNotenum = nNotenum
			.sDigit = sDigit
			.nLic_special = nLic_special
            .nGroupVeh = nGroupVeh
		End With
		
		Select Case nAction
			'+ If the select option is Add
			'+ Si la opción seleccionada es Registrar
			
			Case eFunctions.Menues.TypeActions.clngActionadd
				Call lclsAuto_db.Add()
				'+ if the select option is modify
				'+ Si la opción seleccionada es Modificar
				
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				Call lclsAuto_db.Update()
				
		End Select
		
insPostBV001_Err: 
		If Err.Number Then
			insPostBV001 = False
		End If
		'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto_db = Nothing
		On Error GoTo 0
	End Function
	'%insMirror: Function is in charge to invert the chain
	'%insMirror: función encargada de voltear la cadena
    Public Function insMirror(ByVal sRegist As String) As Boolean

        Dim lintStart As Integer
        Dim lstrAux As String
        Dim lintPos As Integer

        lstrAux = String.Empty
        lintStart = Len(sRegist)
        For lintPos = lintStart To 1 Step -1
            lstrAux = lstrAux & Mid(sRegist, lintPos, 1)
        Next

        sRegist = lstrAux

    End Function
	
	'%insValBVC001: This method validates the data introduced in the detail area of the form
	'%insValBVC001: Este metodo valida los datos introducidos en la zona de detalle de la forma.
	Public Function insValBVC001(ByVal sCodispl As String, ByVal sChassis As String, ByVal sMotor As String, ByVal sRegistType As String, ByVal sRegist As String, ByVal sClient As String, ByVal sVehcode As String, ByVal sDescBrand As String, ByVal sVehModel As String, ByVal sColor As String, ByVal nYear As Integer, ByVal nVestatus As Integer, ByVal lintAction As Integer) As String
		Dim lintCount As Integer
		Dim lclsAuto As ePolicy.Automobile
		Dim lclsvalfield As eFunctions.valField
		Dim lclsErrors As eFunctions.Errors
		Dim lclsConstructSql As eRemoteDB.ConstructSelect
		Dim lvalClient As eClient.ValClient
		Dim lClientTime As eClient.Client
		
		On Error GoTo insValBVC001_Err
		
		lclsAuto = New ePolicy.Automobile
		lclsvalfield = New eFunctions.valField
		lclsErrors = New eFunctions.Errors
		lclsConstructSql = New eRemoteDB.ConstructSelect
		lvalClient = New eClient.ValClient
		lClientTime = New eClient.Client
		
		lintCount = 0
		
		With lclsConstructSql
			.NameFatherTable("Auto_db", "Auto_db")
			.SelectClause("sLicense_ty, sRegist, sChassis, sMotor, sVeh_own, sClient, sColor, sVehCode, nVestatus, nNotenum, nUsercode, nYear, nVehType, nAnualKm, nActualKm, nKeepVeh, nRoadType, nIndLaw, nFuelType, nIndAlarm, sDigit, nLic_Special")
		End With
		
		'+ Chassis validation
		'+ Validación del Chassis
		If sChassis <> String.Empty Then
			If Not lclsConstructSql.WhereClause("Auto_db.sChassis", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sChassis, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Motor validation
		'+ Validación del Motor
		If sMotor <> String.Empty Then
			If Not lclsConstructSql.WhereClause("Auto_db.smotor", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sMotor, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Validation of the license plate type
		'+ Validacion del tipo de matrícula
		If sRegistType <> String.Empty And sRegistType <> "0" Then
			If Not lclsConstructSql.WhereClause("Auto_db.sLicense_Ty", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sRegistType, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Validation of the license plate number
		'+ Validación del número de matrícula
		If sRegist <> String.Empty Then
			'+ Format validation of the license plate if the type is "normal"
			'+ Se valida el formato de la matrícula si es de tipo normal
			If sRegistType = "1" Then
				If Not lclsAuto.ValStructRegist(sRegist) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3122)
				End If
			End If
			If Not lclsConstructSql.WhereClause("Auto_db.sRegist", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sRegist, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Owner validation
		'+ Validacion del Propietario
		If sClient <> String.Empty Then
			If lvalClient.Validate(sClient, lintAction) Then
				If Not lClientTime.Find(lvalClient.ClientCode) Then
				End If
			Else
				Select Case lvalClient.Status
					'+ Confirms that the client code complies with the defined structure
					'+ Se verifica que el código del cliente cumpla con la estructura definida.
					Case eClient.ValClient.eTypeValClientErr.StructInvalid
						Call lclsErrors.ErrorMessage(sCodispl, 2012)
						'+ Corfims that the first character of the client code corresponds with a valid character
						'+ Se verifica que el primer caracter del código del cliente corresponda con uno válido.
					Case eClient.ValClient.eTypeValClientErr.TypeNotFound
						Call lclsErrors.ErrorMessage(sCodispl, 2013)
						'+ In case that the client code is blank
						'+ En caso de que esté en blanco el código del cliente.
					Case eClient.ValClient.eTypeValClientErr.FieldEmpty
						Call lclsErrors.ErrorMessage(sCodispl, 2001)
				End Select
			End If
			If Not lclsConstructSql.WhereClause("Auto_db.sVeh_Own", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sClient, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+Vehicle code validation
		'+Validación del Código del vehículo
		If sVehcode <> String.Empty And sVehcode <> "0" Then
			If Not lclsConstructSql.WhereClause("Auto_db.svehcode", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sVehcode, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Color validation
		'+ Validación del Color
		If sColor <> String.Empty Then
			If Not lclsConstructSql.WhereClause("Auto_db.sColor", eRemoteDB.ConstructSelect.eTypeValue.TypCString, sColor, eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Year validation
		'+ Validacion del Año
		If nYear <> 0 And nYear <> eRemoteDB.Constants.intNull Then
			lclsvalfield.objErr = lclsErrors
			lclsvalfield.Min = 1900
			lclsvalfield.Max = 2100
			lclsvalfield.ValFormat = "####"
			If Not lclsvalfield.ValNumber(nYear) Then
				insValBVC001 = CStr(False)
			End If
			If Not lclsConstructSql.WhereClause("Auto_db.nYear", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, CStr(nYear), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		'+ Status of the vehicle validation
		'+ Validacion del Status del vehículo
		If nVestatus <> 0 And nVestatus <> eRemoteDB.Constants.intNull Then
			lclsvalfield.Min = 0
			lclsvalfield.objErr = lclsErrors
			If Not lclsvalfield.ValNumber(nVestatus) Then
				insValBVC001 = CStr(False)
			End If
			If Not lclsConstructSql.WhereClause("Auto_db.nVestatus", eRemoteDB.ConstructSelect.eTypeValue.TypCNumeric, CStr(nVestatus), eRemoteDB.ConstructSelect.eWordConnection.eAnd) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1948)
			End If
		Else
			lintCount = lintCount + 1
		End If
		
		If lintCount = 10 Then
			Call lclsErrors.ErrorMessage(sCodispl, 3143)
		End If
		
		insValBVC001 = lclsErrors.Confirm
		mstrCondition = lclsConstructSql.Answer
		
insValBVC001_Err: 
		If Err.Number Then
			insValBVC001 = insValBVC001 & "insValBVC001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto = Nothing
		'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsvalfield = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsConstructSql may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsConstructSql = Nothing
		'UPGRADE_NOTE: Object lvalClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalClient = Nothing
		'UPGRADE_NOTE: Object lClientTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lClientTime = Nothing
	End Function
	
	'%insPostBVC001: This method updates the database (as described in the functional specifications)
	'%               for the page "BVC001"
	'%insPostBVC001: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%               especificaciones funcionales)de la ventana "BVC001"
	Public Function insPostBVC001() As Boolean
		insPostBVC001 = True
	End Function
	
	'%insPreBV001: This function reads all of the initial data of the transaction "BV001"
	'%insPreBV001: Esta función lee los datos iniciales de la transacción "BV001"
	Public Function insPreBV001(ByVal sLicense_ty As String, ByVal sRegist As String) As Boolean
		Dim lclsAuto As Automobile
		
		On Error GoTo insPreBV001_Err
		
		insPreBV001 = True
		
		With Me
			'+ Search principal data
			'+ Se busca los datos principales
			If .Find_db1(sLicense_ty, sRegist, True) Then
				lclsAuto = New Automobile
				If lclsAuto.Find_Tab_au_val(.sVehcode, .nYear) Then
					.nValue = lclsAuto.nCapital
				End If
			End If
		End With
		
insPreBV001_Err: 
		If Err.Number Then
			insPreBV001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto = Nothing
	End Function
	
	'%insValAU557_K: Esta función se encarga de validar los datos introducidos en la forma AU557_k (Header).
	Public Function insValAU557_K(ByVal sCodispl As String, ByVal sRegist As String, ByVal sLicense_ty As String) As String
		
		Dim lobjErrors As Object
		Dim lclsAuto_db As ePolicy.Auto_db
		
		On Error GoTo insValAu557_K_Err
		
		lobjErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
		lclsAuto_db = New ePolicy.Auto_db
		
		'+ se valida que se ingrese una placa
		If Trim(sRegist) = String.Empty Then
			Call lobjErrors.ErrorMessage(sCodispl, 3121)
		Else
			'+ Se valida que la placa ingresada exista
			If Not lclsAuto_db.Find_db1(sLicense_ty, sRegist) Then
				Call lobjErrors.ErrorMessage(sCodispl, 713033)
			End If
		End If
		
		insValAU557_K = lobjErrors.Confirm
		
insValAu557_K_Err: 
		If Err.Number Then
			insValAU557_K = insValAU557_K & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto_db = Nothing
		On Error GoTo 0
	End Function
	
	'%insPostAU557: Esta función se encarga de cambiar la placa de un automovil AU557
	Public Function insPostAU557(ByVal sRegist As String, ByVal sRegistNew As String, ByVal sLicense_ty As String, ByVal sLicense_tyNew As String, ByVal sDigitNew As String, ByVal nUsercode As Integer) As Boolean
		Dim lclsAuto_db As ePolicy.Auto_db
		Dim lrecupdRegist_claim_thir As eRemoteDB.Execute
		
		On Error GoTo insPostAU557_Err
		
		lrecupdRegist_claim_thir = New eRemoteDB.Execute
		
		If Me.Find_db1(sLicense_ty, sRegist) Then
			
			'+ Definición de store procedure updRegist_claim_thir al 05-30-2002 15:46:01
			'+
			With lrecupdRegist_claim_thir
				.StoredProcedure = "updRegist_claim_thir"
				.Parameters.Add("sLicense_ty", sLicense_ty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sRegist", sRegist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sLicense_tynew", sLicense_tyNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sRegistnew", sRegistNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sDigitnew", sDigitNew, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				insPostAU557 = .Run(False)
			End With
		End If
		
insPostAU557_Err: 
		If Err.Number Then
			insPostAU557 = False
		End If
		'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto_db = Nothing
		'UPGRADE_NOTE: Object lrecupdRegist_claim_thir may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdRegist_claim_thir = Nothing
		On Error GoTo 0
	End Function
	'%insValAU557: Esta función se encarga de validar los datos introducidos en la forma AU557
	Public Function insValAU557(ByVal sCodispl As String, ByVal sRegist As String, ByVal sLicense_ty As String) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsAuto As ePolicy.Automobile
		
		On Error GoTo insValAu557_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ se valida que se ingrese una placa
		If Trim(sRegist) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 3121)
		Else
			'+ Se valida que la placa ingresada no exista en un certificado vigente
			'+Como transaccion no tiene fecha de proceso se usa la del computador
			lclsAuto = New ePolicy.Automobile
			If lclsAuto.valRegistActive(sLicense_ty, sRegist, Today) Then
				Call lclsErrors.ErrorMessage(sCodispl, 713004)
			End If
			
			'+ Se valida que la placa ingresada corresponda al formato
			If sLicense_ty = "1" Then
				If Not lclsAuto.ValStructRegist(Trim(sRegist)) Then
					Call lclsErrors.ErrorMessage(sCodispl, 3122)
				End If
			End If
			'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsAuto = Nothing
		End If
		
		insValAU557 = lclsErrors.Confirm
		
insValAu557_Err: 
		If Err.Number Then
			insValAU557 = insValAU557 & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsAuto may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAuto = Nothing
		On Error GoTo 0
	End Function
End Class






