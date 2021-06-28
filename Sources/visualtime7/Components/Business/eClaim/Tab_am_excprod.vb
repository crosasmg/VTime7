Option Strict Off
Option Explicit On
Public Class Tab_am_excprod
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_am_excprod.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	Public nBranch As Integer
	Public nProduct As Integer
	Public nTariff As Integer
	Public dEffecdate As Date
	Public sIllness As String
	Public nExc_code As Integer
	Public dInit_date As Date
	Public dEnd_date As Date
	Public dNulldate As Date
	Public nUsercode As Integer
	
	Public nStatusInstance As Integer
	
	'% Find:
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal sIllness As String, ByVal Effecdate As Date) As Boolean
		Dim lrectab_am_excprod As New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrectab_am_excprod = New eRemoteDB.Execute
		
		Find = False
		With lrectab_am_excprod
			.StoredProcedure = "reaTab_am_excprod_o"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sIllness", sIllness, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.dEffecdate = .FieldToClass("dEffecdate")
				Me.sIllness = .FieldToClass("sIllness")
				nExc_code = .FieldToClass("nExc_code")
				dInit_date = .FieldToClass("dInit_date")
				dEnd_date = .FieldToClass("dEnd_date")
				dNulldate = .FieldToClass("dNulldate")
				Find = True
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		
		lrectab_am_excprod = Nothing
	End Function
End Class






