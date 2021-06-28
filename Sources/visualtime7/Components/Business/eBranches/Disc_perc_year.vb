Option Strict Off
Option Explicit On
Public Class Disc_perc_year
	
	'+ Definición de la tabla Disc_perc_year tomada el 10/12/2002.
	'+ Column_Name                          Type         Length  Prec     Scale  Nullable
	'------------------------------         --------------- -   -------- ------- --------
	Public nBranch As Integer ' NUMBER        22     5      0         No
	Public nProduct As Integer ' NUMBER        22     5      0         No
	Public nModulec As Integer ' NUMBER        22     5      0         No
	Public nCover As Integer ' NUMBER        22     5      0         No
	Public nRole As Integer ' NUMBER        22     5      0         No
	Public dEffecdate As Date ' DATE           7                      No
	Public nAge_ini As Integer ' NUMBER        22     5      0         No
	Public nAge_End As Integer ' NUMBER        22     5      0         Yes
	Public nUsercode As Integer ' NUMBER        5
	Public nMonth_init As Integer ' NUMBER        5
	Public nMonth_end As Integer ' NUMBER        5
    Public nDisc_perc_year As Double ' NUMBER        9      6
    Public ndisc_perc_year_exc As Double ' NUMBER        9      6
    Public nDisc_perc_year_nrec As Double ' NUMBER        9      6
    Public ntyperisk As Integer ' NUMBER        5

	'% insValMVI8003: Esta función valida los campos de encabezado y detalle de la transacción MVI8003
	Public Function insValMVI8003(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nZone As Short, ByVal sAction As String, Optional ByVal nAge_ini As Integer = 0, Optional ByVal nAge_End As Integer = 0, Optional ByVal nMonth_init As Integer = 0, Optional ByVal nMonth_end As Integer = 0, Optional ByVal nDisc_perc_year As Double = 0) As String
		Dim lrecinsValMVI8003 As Object
		Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
		
		On Error GoTo InsValMVI8003_Err
		
		lrecinsValMVI8003 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecinsValMVI8003
			
			.StoredProcedure = "INSMVI8003PKG.INSVALMVI8003"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nZone", nZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_init", nMonth_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMonth_end", nMonth_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisc_perc_year", nDisc_perc_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage("MVI8003",  ,  ,  ,  ,  , lstrError)
					insValMVI8003 = .Confirm()
				End With
				'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjErrors = Nothing
				
			End If
			
		End With
		
InsValMVI8003_Err: 
		If Err.Number Then
			insValMVI8003 = "insValMVI8003: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValMVI8003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValMVI8003 = Nothing
	End Function
	
	
	'% insPostMVI8003: Esta función actualiza los campos de la transacción MVI8003 -
    Public Function insPostMVI8003(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, Optional ByVal nAge_ini As Integer = 0, Optional ByVal nAge_End As Integer = 0, Optional ByVal nMonth_init As Integer = 0, Optional ByVal nMonth_end As Integer = 0, Optional ByVal nDisc_perc_year As Double = 0, Optional ByVal ndisc_perc_year_exc As Double = 0, Optional ByVal ntyperisk As Integer = 0, Optional ByVal nDisc_perc_year_nrec As Double = 0) As Boolean
        Dim lrecinsPostMVI8003 As Object

        On Error GoTo insPostMVI8003_Err

        lrecinsPostMVI8003 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")

        With lrecinsPostMVI8003

            .StoredProcedure = "INSMVI8003PKG.INSPOSTMVI8003"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth_init", nMonth_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonth_end", nMonth_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_perc_year", nDisc_perc_year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("ndisc_perc_year_exc", ndisc_perc_year_exc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ntyperisk", ntyperisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDisc_perc_year_nrec", nDisc_perc_year_nrec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            .Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



            insPostMVI8003 = .Run(False)

        End With

insPostMVI8003_Err:
        If Err.Number Then
            insPostMVI8003 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecinsPostMVI8003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsPostMVI8003 = Nothing
    End Function
End Class






