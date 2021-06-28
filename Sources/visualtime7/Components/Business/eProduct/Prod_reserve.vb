Option Strict Off
Option Explicit On
Public Class Prod_reserve
	'%-------------------------------------------------------%'
	'% $Workfile:: Prod_reserve.cls                       $%'
	'% $Author:: Fmendoza                                   $%'
	'% $Date:: 29/06/06 5:34p                               $%'
	'% $Revision:: 3                                        $%'
	'%-------------------------------------------------------%'
	
	'+
	'+ Estructura de tabla Prod_reserve al 06-27-2006 16:53:14
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nModulec As Double ' NUMBER     22   6     18   S
	Public nCover As Double ' NUMBER     22   6     18   S
	Public nType_reserve As Integer ' NUMBER     22   0     5    N
	Public dCompdate As Date ' DATE       7    0     0    S
	Public nUsercode As Integer ' NUMBER     22   0     5    S
	Public dEffecdate As Date
	Public sRoureser As String
	
	'insValDP210: Validaciones de la transacción
	Public Function insValDP210(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType_reserve As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sRoureser As String) As String
		Dim lrecord As eRemoteDB.Execute
		Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insValDP210_Err

        lrecord = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure ins_row al 06-27-2006 16:58:47
        '+
        With lrecord
            .StoredProcedure = "INSDP210PKG.INSVAL"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_reserve", nType_reserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)

            lstrError = .Parameters("Arrayerrors").Value

            If lstrError <> String.Empty Then
                lobjErrors = New eFunctions.Errors
                With lobjErrors
                    .ErrorMessage("DP210", , , , , , lstrError)
                    insValDP210 = .Confirm()
                End With
                'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lobjErrors = Nothing
            End If
        End With

insValDP210_Err:
        If Err.Number Then
            insValDP210 = Err.Description
        End If
        'UPGRADE_NOTE: Object lrecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecord = Nothing
        On Error GoTo 0
    End Function

    'insPostDP210: Actualizaciones de la transaccion
    Public Function insPostDP210(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nType_reserve As Integer, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sRoureser As String) As Boolean

        Dim lrecord As eRemoteDB.Execute
        Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty

        On Error GoTo insPostDP210_Err

        lrecord = New eRemoteDB.Execute

        '+
        '+ Definición de store procedure ins_row al 06-27-2006 16:58:47
        '+
        With lrecord
            .StoredProcedure = "INSDP210PKG.INSPOST"
            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_reserve", nType_reserve, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostDP210 = .Run(False)

        End With

insPostDP210_Err:
        If Err.Number Then
            insPostDP210 = False
        End If
        'UPGRADE_NOTE: Object lrecord may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecord = Nothing
        On Error GoTo 0
    End Function
	'%Class_Initialize: Inicializa las propiedades cuando se instancia la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nType_reserve = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sRoureser = CStr(eRemoteDB.Constants.strNull)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






