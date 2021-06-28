Option Strict Off
Option Explicit On
Public Class ErrorTyp
	
	'+Lista de valores según la tabla Table999 en el sistema 03/04/2002
	Public Enum eStatError
		estDetected = 1
		estAsigned = 2
		estCorreted = 3
		estApproved = 4
		estRejected = 5
		estReversed = 6
		estRegistered = 7
		estPending = 83
		estCleared = 9
		estNew = 10
		estNoAcept = 11
		estAcept = 12
	End Enum
	
	'+Lista de valores según la tabla Table1007 en el sistema 27/06/2000
	Public Enum eTypeError
		eteError = 1
		eteChange = 2
		eteImprove = 3
	End Enum
	
	'+Lista de valores según la tabla Table1006 en el sistema 27/06/2000
	Public Enum ePriority
		epImediatly = 1
		epShortTerm = 2
		epNoUrgent = 3
		epPostpone = 4
	End Enum
	
	'+Propiedades según la tabla en el sistema 27/06/2000
	'          Column_name                            Type              Length    Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	Public nErrorNum As Integer 'int          no     4        10    0     no                                  (n/a)                               (n/a)
	Public sCodisp As String 'char         no     8                    no                                  yes                                 no
	Public sCodispl As String 'char         no     8                    yes                                 yes                                 yes
	Public sDescript_win As String 'char         no     8                    yes                                 yes                                 yes
	Public dDat_assign As Date 'datetime     no     8                    yes                                 (n/a)                               (n/a)
	Public dDate As Date 'datetime     no     8                    yes                                 (n/a)                               (n/a)
	Public dDate_Last As Date 'datetime     no     8                    yes                                 (n/a)                               (n/a)
	Public sDescript As String 'char         no     60                   yes                                 yes                                 yes
	Public tDs_text As String 'text         no     16                   yes                                 (n/a)                               (n/a)
	Public nPriority As ePriority 'smallint     no     2        5     0     yes                                 (n/a)                               (n/a)
	Public nSource As Integer 'smallint     no     2        5     0     yes                                 (n/a)                               (n/a)
	Public nSource_Initial As Integer 'smallint     no     2        5     0     yes                                 (n/a)                               (n/a)
	Public sStat_error As eStatError 'char         no     1                    no                                  yes                                 no
	Public sStat_error_Initial As eStatError 'char         no     1                    no                                  yes                                 no
	Public sUse_assign As String 'char         no     12                   yes                                 yes                                 yes
	Public sVersion As String 'char         no     6                    yes                                 yes                                 yes
	Public sWinDescript As String 'char         no     6                    yes                                 yes                                 yes
	'Public nType_err   As eTypeError             'char         no     1                    yes                                 yes                                 yes
	Public nType_err As Integer 'char         no     1                    yes                                 yes                                 yes
	Public sUser As String 'char         no     6                    yes                                 yes                                 yes
	Public sUser_Last As String 'char         no     6                    yes                                 yes                                 yes
	Public sUser_Sesion As String 'char         no     6                    yes                                 yes                                 yes
	Public sHour As String 'char         no     6                    yes                                 yes                                 yes
	Public sHour_date As String 'char         no     6                    yes                                 yes                                 yes
	Public nDate As Integer 'int          no     4        10    0     no                                  (n/a)                               (n/a)
	Public sHour_Time As String 'char         no     6                    yes                                 yes                                 yes
	Public lstrDescrip As String 'char         no     6                    yes                                 yes                                 yes
	Public oerr_histor As err_histor 'char         no     6                    yes                                 yes                                 yes
	Public sUserName As String 'char         no     6                    yes                                 yes                                 yes
	Public nConsecut As Integer 'int          no     4        10    0     no                                  (n/a)                               (n/a)
	Public tAuxDs_text As String
	Public nSeverity As Integer
    Public nModule_Err As Integer
    Public bErr_Module As Boolean
	Public nDays_user As Integer
	Public sHour_user As String
	Public nUsercode As Integer
    Public nSessionId As Integer
    Public sSrc_Descript As String
	'+ Propiedades Auxiliares
	
	Public sString1 As String
	Public sString2 As String
	Public sString3 As String
	Public sString4 As String
	Public sString5 As String
	Public sString6 As String
	Public sString7 As String
	
	Private mblnCharge As Boolean
	Private Structure udtWindows
		Dim sCodispl As String
		Dim sDescript As String
	End Structure
	Private arrWindows() As udtWindows
	
	
	
	'% Find:Busca todas las Caracteristicas de un Error en Especifico
	Public Function Find(ByVal nError As Integer) As Boolean
		Dim lclssecur As eSecurity.User
		Dim lclsWindows As eSecurity.Windows
		Dim lrecreaErrors As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		Call insInitialize()
		lclsWindows = New eSecurity.Windows
		lrecreaErrors = New eRemoteDB.Execute
		lclssecur = New eSecurity.User
		
		'Definición de parámetros para stored procedure 'insudb.reaErrors'
		'Información leída el 27/06/2000 02:24:14 PM
		
		With lrecreaErrors
			.StoredProcedure = "reaErrors"
			.Parameters.Add("ErrorNum", nError, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			If .Run Then
				sCodisp = .FieldToClass("sCodisp")
				dDat_assign = .FieldToClass("dDat_assign")
				sDescript = .FieldToClass("sDescript")
				tDs_text = .FieldToClass("tDs_text")
				nPriority = .FieldToClass("nPriority")
				nSource = .FieldToClass("nSource")
				sStat_error = .FieldToClass("sStat_error")
				sStat_error_Initial = sStat_error
				sUse_assign = .FieldToClass("sUse_assign")
				sVersion = .FieldToClass("sVersion")
				nType_err = .FieldToClass("nType_err")
				sWinDescript = .FieldToClass("sWinDescript")
				sUser = .FieldToClass("sUser")
				sHour = .FieldToClass("sHour")
				nSeverity = .FieldToClass("nSeverity")
                nModule_Err = .FieldToClass("nModule_Err")
                sSrc_Descript = .FieldToClass("sSrc_Descript")
				nDate = eRemoteDB.Constants.intNull
				sHour_Time = "  :  "
				.RCloseRec()
				Find = True
				If lclssecur.Reauser_Initial(sUser) Then
					sUserName = lclssecur.sCliename
				End If
				If oerr_histor Is Nothing Then
					oerr_histor = New err_histor
				End If
				With oerr_histor
					.FindLastHistor(nError)
					nConsecut = .nConsecut
					sUse_assign = .sUser
					dDat_assign = .dDate
					sUser_Last = .sUser
					dDate_Last = .dDate
				End With
			End If
		End With
		
		lclsWindows = Nothing
		lrecreaErrors = Nothing
		lclssecur = Nothing
		
		Exit Function
	End Function
	
	'% insPostER007_k : Genera la tabla temporal de errores
	Public Function insPostER007_k(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCodisp As String, ByVal sStat_error As String, ByVal nSource As Integer, ByVal nPriority As Integer, ByVal nSeverity As Integer, ByVal nModule_Err As Integer, ByVal nSessionId As String, ByVal nUsercode As Integer) As Boolean
		Dim lrecErrors As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecErrors = New eRemoteDB.Execute
		Dim skey1 As Object
		
		skey1 = sKey(nUsercode, nSessionId)
		
		With lrecErrors
			.StoredProcedure = "InsT_Errors"
			.Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_error", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPriority", nPriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeverity", nSeverity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModule_Err", nModule_Err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", skey1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			insPostER007_k = .Run(False)
		End With
		
		lrecErrors = Nothing
		
		Exit Function
	End Function
	
	'%sKey. Esta propiedad se encarga de devolver la llave de lectura del registro de coberturas
	Public ReadOnly Property sKey(ByVal nUsercode As Integer, ByVal nSessionId As String) As String
		Get
			If Not IsIDEMode Then
			End If
			
			sKey = "Err" & CStr(nSessionId) & "-" & CStr(nUsercode)
			
			Exit Property
		End Get
	End Property
	
	Public ReadOnly Property State() As String
		Get
			Dim lrecTable As eRemoteDB.Query
			
			If Not IsIDEMode Then
            End If
            State = String.Empty
			lrecTable = New eRemoteDB.Query
			With lrecTable
				If .OpenQuery("Table999", "sDescript", "nCodigInt = " & CStr(sStat_error)) Then
					State = .FieldToClass("sDescript")
					.CloseQuery()
				End If
			End With
			lrecTable = Nothing
			
			Exit Property
		End Get
	End Property
	
	Public ReadOnly Property WindowDescript() As String
		Get
			Dim lrecWindows As eRemoteDB.Query
			
			If Not IsIDEMode Then
            End If
            WindowDescript = String.Empty
			lrecWindows = New eRemoteDB.Query
			
			With lrecWindows
				If .OpenQuery("windows", "sDescript", "sCodispl = '" & sCodispl & "'") Then
					WindowDescript = .FieldToClass("sDescript")
					.CloseQuery()
				End If
			End With
			lrecWindows = Nothing
			
			Exit Property
		End Get
	End Property
	
	'%CountItem: propiedad que indica el número de elementos en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If Not IsIDEMode Then
			End If
			
			CountItem = IIf(mblnCharge, UBound(arrWindows), -1)
			
			Exit Property
		End Get
	End Property
	
	'%UpDateStat: Actualiza el Status de un Error
	Public Function UpDateStat() As Boolean
		Dim lrecUpdStatErrors As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecUpdStatErrors = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.UpdStatErrors'
		'Información leída el 27/06/2000 04:59:38 PM
		
		With lrecUpdStatErrors
			.StoredProcedure = "UpdStatErrors"
			.Parameters.Add("nErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_error", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDat_assign", dDat_assign, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUse_assign", sUse_assign, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", tDs_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.bErr_Module = True
			UpDateStat = .Run(False)
		End With
		
		lrecUpdStatErrors = Nothing
		
		Exit Function
	End Function
	
	'% Add:Registra el codigo de un Error y sus Caracteristicas en las Tablas de Errores
	Public Function Add() As Boolean
		Dim lreccreErrors As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lreccreErrors = New eRemoteDB.Execute
        Add = True
		With lreccreErrors
			.StoredProcedure = "creErrors"
			.Parameters.Add("ErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Codisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Descript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Ds_Text", tDs_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Priority", nPriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Source", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Staterror", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Use_Assign", sUse_assign, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Version", sVersion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeverity", nSeverity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NMODULE_ERROR", nModule_Err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			Add = .Run(False)
		End With
		
		lreccreErrors = Nothing
		
		Exit Function
	End Function
	
	'% Update: Actualiza las Caracteristicas de un Error
	Public Function Update() As Boolean
		Dim lrecupdErrors As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecupdErrors = New eRemoteDB.Execute

		With lrecupdErrors
			.StoredProcedure = "updErrors"
			.Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("tDs_Text", tDs_text, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2147483647, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPriority", nPriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSource", nSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sVersion", sVersion, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_Error", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSeverity", nSeverity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("NMODULE_ERROR", nModule_Err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			Update = .Run(False)
		End With
		
		lrecupdErrors = Nothing
		
		Exit Function
	End Function
	
	'% Update: Actualiza las Caracteristicas de un Error
	Public Function Update_T_Errors() As Boolean
        Dim lrecupdErrors As eRemoteDB.Execute
        Dim skey1 As Object
		
		If Not IsIDEMode Then
		End If
		lrecupdErrors = New eRemoteDB.Execute
		

		
		skey1 = sKey(nUsercode, nSessionId)
		
		With lrecupdErrors
			.StoredProcedure = "UpdT_Errors"
			.Parameters.Add("nErrorNum", nErrorNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStat_Error", sStat_error, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SUSE_ASSIGN", sUser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKey", skey1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			Update_T_Errors = .Run(False)
		End With
		
		lrecupdErrors = Nothing
		
		Exit Function
	End Function
	
	'% Update: Actualiza las Caracteristicas de un Error
	Public Function UpdateErrors_Upd() As Boolean
		Dim lrecupdErrors As eRemoteDB.Execute
		
		If Not IsIDEMode Then
		End If
		lrecupdErrors = New eRemoteDB.Execute
		
		Dim skey1 As Object
		
		skey1 = sKey(nUsercode, nSessionId)
		
		With lrecupdErrors
			.StoredProcedure = "UpdErrors_Upd"
			.Parameters.Add("sKey", skey1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			UpdateErrors_Upd = .Run(False)
		End With
		
		lrecupdErrors = Nothing
		
		Exit Function
	End Function
	
	'% insValER001_K: Valida el numero de Error
    Public Function insValER001_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nErroNum As Integer) As String

        Dim lclsErrors As eFunctions.Errors

        If Not IsIDEMode() Then
        End If
        lclsErrors = New eFunctions.Errors

        If nAction = 301 Then
            If Find(nErroNum) Then
                Call lclsErrors.ErrorMessage(sCodispl, 20002)
            End If
        Else
            If Not Find(nErroNum) Then
                Call lclsErrors.ErrorMessage(sCodispl, 20003)
            End If
        End If

        insValER001_K = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function
	
	'% insValER007_K: Valida el numero de Error
	Public Function insValER007_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCodisp As String, ByVal sStat_error As String, ByVal nSource As Integer, ByVal nPriority As Integer, ByVal nSeverity As Integer, ByVal nModule_Err As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		
        If sCodisp = String.Empty And (sStat_error = String.Empty Or sStat_error = "0") And nSource = 0 And nPriority = 0 And (nSeverity = eRemoteDB.Constants.intNull Or nSeverity = 0) And nModule_Err = 9999 Then
            Call lclsErrors.ErrorMessage(sCodispl, 3143)
        End If
		
		insValER007_K = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'% insValER001: Valida Los Campos de la Ventana ER001
	Public Function insValER001(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sCodisp As String, ByVal nPriority As Integer, ByVal nType_err As Integer, ByVal nSource As Integer, ByVal sDescript As String, ByVal tDs_text As String, ByVal nSeverity As Integer, ByVal nModule_Err As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsSecurity As eSecurity.Windows
        Dim sErrosList As String
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		lclsSecurity = New eSecurity.Windows
		
		If Trim(sCodisp) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 20021)
		Else
			If Not lclsSecurity.reaWindows(sCodisp) Then
				Call lclsErrors.ErrorMessage(sCodispl, 1930)
			End If
		End If
		If nPriority = 0 Or nPriority = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 20005)
		End If
		If nType_err = 0 Or nType_err = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1931)
		Else
			sErrosList = InsValER00NDB(nType_err)
			
			If Len(sErrosList) > 0 Then
				Call lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , sErrosList)
			End If
		End If
		
		If nSource = 0 Or nSource = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 1932)
		End If
		If Trim(sDescript) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 20004)
		End If
		If (Trim(tDs_text) = Trim(tAuxDs_text) Or Trim(tDs_text) = String.Empty) Then
			Call lclsErrors.ErrorMessage(sCodispl, 20037)
		End If
		
		If nModule_Err = 9999 Or nModule_Err = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 55993)
		End If
		
		If nSeverity = eRemoteDB.Constants.intNull Or nSeverity = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 55992)
		End If
		
		insValER001 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		lclsSecurity = Nothing
		
		Exit Function
	End Function
	
	'**%Objetivo: Performs validations by accessing to the database.
	'**%Parameters:
	'**%    nType_err   - Type of error
	'%Objetivo: Esta función permite realizar validaciones con acceso a la base de datos.
	'%Parámetros:
	'%    nType_err   - Tipo de error
	Private Function InsValER00NDB(ByVal nType_err As Integer, Optional ByVal sActionst As String = "1") As String
		Dim lclsvaler001 As eRemoteDB.Execute
		
		If Not IsIDEMode Then
        End If
        InsValER00NDB = String.Empty
		lclsvaler001 = New eRemoteDB.Execute
		
		With lclsvaler001
			.StoredProcedure = "valTranser001"
			.Parameters.Add("nType_err", nType_err, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sActionst", sActionst, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 2, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sErrorList", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValER00NDB = Trim(.Parameters("sErrorList").Value)
			End If
		End With
		
		lclsvaler001 = Nothing
		
		Exit Function
	End Function
	
	'% insValER007: Valida Los Campos de la Ventana ER007
	Public Function insValER007(ByVal sCodispl As String, ByVal sStat_error As String, ByVal sUser As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		
		If Trim(sStat_error) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012)
		End If
		
		If Trim(sUser) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 12049)
		End If
		
		insValER007 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'% ivalAction: Localiza la Descripcion de la Transaccion
	Public Function valAction(ByVal lstrCode As String) As Boolean
		Dim mclsSecurty As eSecurity.Windows
		
		If Not IsIDEMode Then
		End If
		
		mclsSecurty = New eSecurity.Windows
		valAction = True
		
		mclsSecurty = New eSecurity.Windows
		
		With mclsSecurty
			If .reaWindows(Trim(lstrCode)) Then
				lstrDescrip = .sDescript
			Else
				lstrDescrip = String.Empty
			End If
		End With
		
		mclsSecurty = Nothing
		
		Exit Function
	End Function
	
	Public Function Find_UserName(ByVal nUsercode As Integer) As Boolean
		Dim lclssecur As eSecurity.User
		
		If Not IsIDEMode Then
		End If
		lclssecur = New eSecurity.User
		
		If sUser = String.Empty Then
			If lclssecur.Find(nUsercode) Then
				sUser = lclssecur.sInitials
				sUser_Sesion = sUser
				sUserName = lclssecur.sCliename
			End If
		End If
		
		Exit Function
	End Function
	
	'% insPostER001: Actualizacion de la Ventana ER001
	Public Function insPostER001(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nErrorNum As Integer, ByVal sCodisp As String, ByVal nPriority As Integer, ByVal nType_err As Integer, ByVal nSource As Integer, ByVal sDescript As String, ByVal tDs_text As String, ByVal sStat_error As String, ByVal sUse_assign As String, ByVal sVersion As String, ByVal dDate As Date, ByVal sHour As String, ByVal nSeverity As Integer, ByVal nModule_Err As Integer) As Boolean
		Dim mobjError_Histor As eErrors.err_histor
		
		If Not IsIDEMode Then
		End If
		mobjError_Histor = New eErrors.err_histor
		
		If nModule_Err = 9998 Then
			nModule_Err = 0
		End If
		
		With Me
			.nErrorNum = nErrorNum
			.sCodisp = sCodisp
			.nPriority = nPriority
			.nType_err = nType_err
			.nSource = nSource
			.sDescript = sDescript
			.tDs_text = tDs_text
			.sStat_error = CShort(sStat_error)
			.sUse_assign = sUse_assign
			.sVersion = sVersion
			.sHour = sHour
			.nSeverity = nSeverity
			.nModule_Err = nModule_Err
		End With
		
		insPostER001 = True
		
		Select Case nAction
			Case eFunctions.Menues.TypeActions.clngActionadd
				If Add Then
					With mobjError_Histor
						.nErrorNum = nErrorNum
						.nConsecut = 0
						.dDate = dDate
						.sHour = sHour
						.sUser = sUse_assign
						.sStat_error = sStat_error
						insPostER001 = .Add
					End With
				End If
			Case eFunctions.Menues.TypeActions.clngActionUpdate
				insPostER001 = Update
			Case eFunctions.Menues.TypeActions.clngActioncut
				With mobjError_Histor
					.nErrorNum = nErrorNum
					insPostER001 = .Delete
				End With
		End Select
		
		mobjError_Histor = Nothing
		
		Exit Function
	End Function
	
	'% insPostER007: Actualizacion de la Ventana ER007
	Public Function insPostER007(ByVal sCodispl As String, ByVal nErrorNum As Integer, ByVal sStat_error As String, ByVal sUser As String, ByVal nSessionId As String, ByVal nUsercode As Integer) As Boolean
		If Not IsIDEMode Then
		End If
		
		With Me
			.nErrorNum = nErrorNum
			.sStat_error = CShort(sStat_error)
			.sUser = sUser
			.nSessionId = nSessionId
			.nUsercode = nUsercode
		End With
		
		insPostER007 = Update_T_Errors
		
insPostER007_err: 
		If Err.Number Then
			insPostER007 = False
		End If
		If Not IsIDEMode Then
		End If
		
		Exit Function
	End Function
	
	'% insPostER007_K_Upd: Actualizacion de la table ERRORS a partir de T_ERRORS
	Public Function insPostER007_Upd(ByVal nSessionId As String, ByVal nUsercode As Integer) As Boolean
		If Not IsIDEMode Then
		End If
		
		With Me
			.nSessionId = nSessionId
			.nUsercode = nUsercode
		End With
		
		insPostER007_Upd = UpdateErrors_Upd
		
insPostER007_Upd_err: 
		If Err.Number Then
			insPostER007_Upd = False
		End If
		If Not IsIDEMode Then
		End If
		
		Exit Function
	End Function
	
	Private Sub Class_Initialize_Renamed()
		If Not IsIDEMode Then
		End If
		
		Call insInitialize()
		
		Exit Sub
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	Private Sub insInitialize()
        Dim lclsValues As eFunctions.Values

        If Not IsIDEMode Then
		End If
		lclsValues = New eFunctions.Values
		
		nErrorNum = eRemoteDB.Constants.intNull
		sCodisp = String.Empty
		sCodispl = String.Empty
		dDat_assign = Today
		dDate = Today
		sDescript = String.Empty
		nPriority = 0
		nSource_Initial = 0
		sStat_error_Initial = CShort("1")
		sUse_assign = String.Empty
		sVersion = "1"
		sWinDescript = String.Empty
		nType_err = 0
		sUser = String.Empty
		sHour = Format(TimeOfDay, "Short Time")
		sHour_date = Format(TimeOfDay, "Short Time")
		lstrDescrip = String.Empty
		sUserName = String.Empty
        tAuxDs_text = eFunctions.Values.GetMessage(866) & vbCrLf & vbCrLf & eFunctions.Values.GetMessage(867) & vbCrLf & vbCrLf & eFunctions.Values.GetMessage(868) & vbCrLf & vbCrLf & eFunctions.Values.GetMessage(869) & vbCrLf & vbCrLf & eFunctions.Values.GetMessage(870) & vbCrLf & vbCrLf & eFunctions.Values.GetMessage(871) & vbCrLf & vbCrLf & eFunctions.Values.GetMessage(872)
        tDs_text = tAuxDs_text
		lclsValues = Nothing
		
		Exit Sub
	End Sub
	
	'% insPreER001: Carga los Valores Iniciales de la Ventana ER001
	Public Function insPreER001(ByVal nErrorNum As Integer, ByVal nUsercode As Integer) As Boolean
		If Not IsIDEMode Then
		End If
		
		insPreER001 = False
		
		If Not Find(nErrorNum) Then
			insPreER001 = Find_UserName(nUsercode)
		Else
			insPreER001 = True
		End If
		
		Exit Function
	End Function
	
	'% Generate: Si la Opcion es Registrar y no se le ha introducido un Numero de Error se Genera el Automatico en la Ventana ER001
	Public Function Generate(ByVal sErrornum As String, ByVal nAction As Integer, ByVal nUsercode As Integer) As Integer
		Dim lgenErrorNum As eGeneral.GeneralFunction
		
		If Not IsIDEMode Then
		End If
		lgenErrorNum = New eGeneral.GeneralFunction
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
			If sErrornum = String.Empty Then
                Generate = lgenErrorNum.Find_Numerator(99, 0, nUsercode, , , , , , , , , , )
                'Generate = lgenErrorNum.Find_Numerator(99, 0, nUsercode, , , , , , , , , , , bErr_Module)
            Else
                Generate = CInt(sErrornum)
			End If
		Else
			Generate = CInt(sErrornum)
		End If
		
		lgenErrorNum = Nothing
		
		Exit Function
	End Function
	
	'% insValER002_K: Valida el numero de Error
	Public Function insValER002_K(ByVal sCodispl As String, ByVal sCodisp As String, ByVal sStat_error As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		
		If Trim(sCodisp) = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 20023)
		End If
		If sStat_error = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling,  , 249, ": ")
        End If
		
		insValER002_K = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'% insValnErrorNum: Valida el numero de Error
	Public Function insValnErrorNum(ByVal sCodispl As String, ByVal nErroNum As Integer) As String
		
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		
		If nErroNum = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 10052)
		Else
			If Not Find(nErroNum) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10053)
			End If
		End If
		
		insValnErrorNum = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'%insValER003: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValER003(ByVal sCodispl As String, ByVal nErrorNum As Integer, ByVal sUser As String, ByVal nType_err As Integer, ByVal sStatError As String, ByVal dDat_assign As Date, ByVal sHour As String, ByVal nDays As Integer, ByVal sHour_Time As String, ByVal tDs_text As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim sErrosList As String
		Dim lclsUser As New eSecurity.User
		
		If Not IsIDEMode Then
		End If
		
		lclsUser = New eSecurity.User
		lclsErrors = New eFunctions.Errors
		
		If sStatError <> String.Empty Then
			
			Find(nErrorNum)
			
			If sUser = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 12049)
			End If
			
			If Not lclsUser.Reauser_Initial(sUser) Then
				Call lclsErrors.ErrorMessage(sCodispl, 12002)
			End If
			
			'+Validar tipo de error
			
			sErrosList = InsValER00NDB(nType_err, "2")
			
			If Len(sErrosList) > 0 Then
				Call lclsErrors.ErrorMessage(sCodispl,  ,  ,  ,  ,  , sErrosList)
			End If
			
			'+Validar Reversar estado
			If sStatError = "6" Then
				If CBool(Trim(CStr(UCase(sUse_assign) <> Trim(UCase(sUser))))) Then
					Call lclsErrors.ErrorMessage(sCodispl, 20039)
				Else
					Call lclsErrors.ErrorMessage(sCodispl, 20040)
				End If
				
				'+No se puede reversar porque no posee estado anterior
				If Me.nConsecut = 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 60397)
				End If
			End If
			
			'+Validar fecha
			If dDat_assign = eRemoteDB.Constants.dtmNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 1012,  , eFunctions.Errors.TextAlign.LeftAling,  , 1046, ": ")
			End If
			
			If sStatError <> "6" Then
				'+Validar corregido
				If sStatError = CStr(eStatError.estCorreted) And sStat_error = eStatError.estAsigned Then
					If Trim(UCase(sUse_assign)) <> Trim(UCase(sUser)) Then
						Call lclsErrors.ErrorMessage(sCodispl, 20041)
					End If
				End If
				
				'+Validar descripción
				If Trim(tDs_text) = String.Empty Then
					Call lclsErrors.ErrorMessage(sCodispl, 20037)
				End If
				
				If Not IsIDEMode Then
				End If

                sHour_Time = Trim(sHour_Time)
                If Len(Trim(sHour_Time)) <= 5 Then
                    Select Case InStr(1, sHour_Time, ":")
                        Case 0
                            If sHour_Time = String.Empty Then
                                sHour_Time = "00:00"
                            Else
                                sHour_Time = sHour_Time & ":00"
                            End If
                        Case 1
                            If Len(sHour_Time) = 1 Then
                                sHour_Time = "00:00"
                            Else
                                sHour_Time = "00" & sHour_Time
                            End If
                        Case 3
                            If Len(sHour_Time) = 3 Then
                                sHour_Time = sHour_Time & "00"
                            End If
                    End Select
                Else
                    sHour_Time = "00:00"
                End If

				If sStatError <> "2" Then
					If nDays = eRemoteDB.Constants.intNull Then
						If Hour(CDate(sHour_Time)) = 0 And Minute(CDate(sHour_Time)) = 0 Then
							Call lclsErrors.ErrorMessage(sCodispl, 20038)
						End If
					End If
				End If
			End If
		End If
		
		
		insValER003 = lclsErrors.Confirm
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'%InsValER006: Validaciones de la ER006
	Public Function InsValER006(ByVal sCodispl As String, ByVal sCodisp As String, ByVal sUse_assign As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		If Not IsIDEMode Then
		End If
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			If Trim(sCodisp) = String.Empty Then
				.ErrorMessage(sCodispl, 20021)
			End If
			
			If Trim(sUse_assign) = String.Empty Then
				lclsErrors.ErrorMessage(sCodispl, 12049)
			End If
			
			InsValER006 = lclsErrors.Confirm
		End With
		
		lclsErrors = Nothing
		
		Exit Function
	End Function
	
	'%InsAssignTransac: Actualizaciones de la transacción ER006
	Private Function InsAssignTransac(ByVal sCodisp As String, ByVal sUse_assign As String) As Boolean
		Dim lrecInsAssignTransac As eRemoteDB.Execute
		
		'+ Definición de store procedure InsAssignTransac al 10-22-2002 12:28:03
		
		If Not IsIDEMode Then
		End If
		lrecInsAssignTransac = New eRemoteDB.Execute
		With lrecInsAssignTransac
			.StoredProcedure = "InsAssignTransac"
			.Parameters.Add("sCodisp", sCodisp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sUse_assign", sUse_assign, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.bErr_Module = True
			InsAssignTransac = .Run(False)
		End With
		
		lrecInsAssignTransac = Nothing
		
		Exit Function
	End Function
	
	'%InsPostER006: Validaciones de la ER006
	Public Function InsPostER006(ByVal sCodisp As String, ByVal sUse_assign As String) As Boolean
		If Not IsIDEMode Then
		End If
		
		InsPostER006 = InsAssignTransac(sCodisp, sUse_assign)
		
		Exit Function
	End Function
	
	'%insvalUser:Devuelve el nombre del usuario
	Public Function insValUser(ByVal strCode As String) As String
		Dim lclsUser As eSecurity.User
		
		If Not IsIDEMode Then
		End If
        insValUser = String.Empty
		lclsUser = New eSecurity.User
		lclsUser = New eSecurity.User
		
		With lclsUser
			If .Reauser_Initial(strCode) Then
				insValUser = .sCliename
			End If
		End With
		
		lclsUser = Nothing
		
		Exit Function
	End Function
	
	Public Function DefaultValueER003(ByVal sStatError As String) As Object
		'    DefaultValueER003 = String.Empty
		If Not IsIDEMode Then
        End If

        DefaultValueER003 = String.Empty
		
		Select Case sStatError
			Case "Asigned"
				DefaultValueER003 = IIf(sStat_error = eStatError.estDetected, "1", "2")
			Case "Correted"
				DefaultValueER003 = IIf(sStat_error = eStatError.estAsigned, "1", "2")
			Case "Approved"
				DefaultValueER003 = IIf(sStat_error = eStatError.estCorreted, "1", "2")
			Case "Reverse_Locked"
				If (sStat_error = eStatError.estCorreted Or sStat_error = eStatError.estApproved) Then
					DefaultValueER003 = False
				Else
					DefaultValueER003 = True
				End If
			Case "Asigned_Locked"
				DefaultValueER003 = IIf(sStat_error = eStatError.estDetected, False, True)
			Case "Correted_Locked"
				If (sStat_error = eStatError.estAsigned Or sStat_error = eStatError.estDetected) Then
					DefaultValueER003 = False
				Else
					DefaultValueER003 = True
				End If
			Case "Approved_Locked", "Rejected_Locked"
				DefaultValueER003 = IIf(sStat_error = eStatError.estCorreted, False, True)
				
		End Select
		
		Exit Function
	End Function
	
	'% insPostER003: Actualizacion de la Ventana ER003
	Public Function insPostER003(ByVal sCodispl As String, ByVal nErrorNum As Integer, ByVal sUser As String, ByVal nType_err As Integer, ByVal sStatError As String, ByVal dDat_assign As Date, ByVal sHour As String, ByVal nDate As Integer, ByVal sHour_Time As String, ByVal tDs_text As String) As Boolean
		Dim lnCosecut As Integer
		Dim mobjError_Histor As eErrors.err_histor
		
		If Not IsIDEMode Then
		End If
		If sStatError <> CStr(eRemoteDB.Constants.intNull) Then
			mobjError_Histor = New eErrors.err_histor
			With Me
				.nErrorNum = nErrorNum
				.nDate = nDate
				.sHour = sHour
				.sHour_Time = sHour_Time
				.tDs_text = tDs_text
				If sStatError = "6" Then
					Find(nErrorNum)
					If .nConsecut <> 0 Then
						lnCosecut = .nConsecut - 1
						If mobjError_Histor.Find(nErrorNum, lnCosecut) Then
							.sStat_error = CShort(mobjError_Histor.sStat_error)
							.dDat_assign = mobjError_Histor.dDate
							.sUse_assign = mobjError_Histor.sUser
							.nErrorNum = nErrorNum
							.tDs_text = tDs_text
							.nType_err = nType_err
							With mobjError_Histor
								.nErrorNum = nErrorNum
								
								.Add()
								.nLastConsecut = nConsecut
								.ReverseLastHistor()
							End With
							UpDateStat()
						End If
					End If
				Else
					.nErrorNum = nErrorNum
					.nDate = nDate
					.sHour = sHour
					.sHour_Time = sHour_Time
					.tDs_text = tDs_text
					.sStat_error = CShort(sStatError)
					.sUse_assign = sUser
					.nType_err = nType_err
					.dDat_assign = dDat_assign
					
					Select Case sStatError
						Case CStr(eStatError.estNoAcept)
							.sStat_error = eStatError.estDetected
							.UpDateStat()
							.sStat_error = eStatError.estNoAcept
						Case CStr(eStatError.estRejected)
							.sStat_error = eStatError.estDetected
							.UpDateStat()
							.sStat_error = eStatError.estRejected
						Case Else
							.UpDateStat()
					End Select
					
					With mobjError_Histor
						.nErrorNum = nErrorNum
						.nConsecut = 0
						.dDate = dDat_assign
						.sHour = sHour
						.sUser = sUser
						.sStat_error = CStr(Me.sStat_error)
						.nDays_user = nDate
						.sHour_user = sHour_Time
						.Add()
						If Me.sStat_error = eStatError.estRejected Or Me.sStat_error = eStatError.estNoAcept Then
							.sStat_error = CStr(eStatError.estDetected)
							.Add()
						End If
					End With
				End If
			End With
			mobjError_Histor = Nothing
		End If
		
		insPostER003 = True
		
		mobjError_Histor = Nothing
		
		Exit Function
	End Function
	
	'% Load: Localiza todos las transacciones del módulo de errores y su Descripción
	Public Function Load() As Boolean
		Dim lclsWindows As eSecurity.Windows
		Dim lcolWindows As eSecurity.Windowss
		Dim lintIndex As Integer
		
		If Not IsIDEMode Then
		End If
		Load = False
		mblnCharge = False
		
		lcolWindows = New eSecurity.Windowss
		
		'+Se buscan transacciones de módulo de errores
		If lcolWindows.FindCodMen("ERROR") Then
			
			lintIndex = lcolWindows.Count
			If lintIndex > 0 Then
				ReDim Preserve arrWindows(lintIndex - 1)
				lintIndex = 0
				For	Each lclsWindows In lcolWindows
					'+Si llega al límite de la matriz se expande
					
					With lclsWindows
						arrWindows(lintIndex).sCodispl = .sCodispl
						arrWindows(lintIndex).sDescript = .sDescript
					End With
					lintIndex = lintIndex + 1
				Next lclsWindows
				Load = True
				mblnCharge = True
			End If
			
		End If
		
		lcolWindows = Nothing
		
		Exit Function
	End Function
	
	'% Item: asigna el valor del arreglo a las variables públicas de la clase
	Public Function Item(ByVal nIndex As Integer) As Boolean
		If Not IsIDEMode Then
		End If
		
		If mblnCharge Then
			sCodispl = arrWindows(nIndex).sCodispl
			sDescript = arrWindows(nIndex).sDescript
			Item = True
		End If
		
		Exit Function
	End Function
End Class











