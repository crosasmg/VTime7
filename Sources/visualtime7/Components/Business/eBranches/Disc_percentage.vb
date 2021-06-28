Option Strict Off
Option Explicit On
Public Class Disc_percentage
	
	'+ Definición de la tabla Disc_percentage tomada el 10/12/2002.
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
	Public nQprempayed As Integer ' NUMBER        5
	Public nDisc_percentage As Double ' NUMBER        9      6
	Public sExtraprem As String ' CHAR          1                       Yes
	
	
	'% insValMVI8001: Esta función valida los campos de encabezado y detalle de la transacción MVI8001
	Public Function insValMVI8001(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nZone As Short, ByVal sAction As String, Optional ByVal nAge_ini As Integer = 0, Optional ByVal nAge_End As Integer = 0, Optional ByVal nQprempayed As Integer = 0, Optional ByVal nDisc_percentage As Double = 0) As String
		Dim lrecinsValMVI8001 As Object
		Dim lobjErrors As eFunctions.Errors
        Dim lstrError As String = String.Empty
		
		On Error GoTo InsValMVI8001_Err
		
		lrecinsValMVI8001 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecinsValMVI8001
			
			.StoredProcedure = "INSMVI8001PKG.INSVALMVI8001"
			
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
			.Parameters.Add("nQprempayed", nQprempayed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisc_percentage", nDisc_percentage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			lstrError = .Parameters("Arrayerrors").Value
			
			If lstrError <> String.Empty Then
				lobjErrors = New eFunctions.Errors
				With lobjErrors
					.ErrorMessage("MVI8001",  ,  ,  ,  ,  , lstrError)
					insValMVI8001 = .Confirm()
				End With
				'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lobjErrors = Nothing
				
			End If
			
		End With
		
InsValMVI8001_Err: 
		If Err.Number Then
			insValMVI8001 = "insValMVI8001: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValMVI8001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValMVI8001 = Nothing
	End Function
	
	
	'% insPostMVI8001: Esta función actualiza los campos de la transacción MVI8001 -
	Public Function insPostMVI8001(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sAction As String, Optional ByVal nAge_ini As Integer = 0, Optional ByVal nAge_End As Integer = 0, Optional ByVal nQprempayed As Integer = 0, Optional ByVal nDisc_percentage As Double = 0, Optional ByVal sExtraprem As String = "") As Boolean
		Dim lrecinsPostMVI8001 As Object
		
		On Error GoTo insPostMVI8001_Err
		
		lrecinsPostMVI8001 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
		
		With lrecinsPostMVI8001
			
			.StoredProcedure = "INSMVI8001PKG.INSPOSTMVI8001"
			
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_ini", nAge_ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQprempayed", nQprempayed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDisc_percentage", nDisc_percentage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sExtraprem", IIf(sExtraprem = "1", "1", "2"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostMVI8001 = .Run(False)
			
		End With
		
insPostMVI8001_Err: 
		If Err.Number Then
			insPostMVI8001 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsPostMVI8001 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsPostMVI8001 = Nothing
	End Function
End Class






