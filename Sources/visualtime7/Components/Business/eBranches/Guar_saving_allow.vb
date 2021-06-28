Option Strict Off
Option Explicit On
Public Class Guar_saving_allow
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_riskInsu.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy_Year_Ini As Integer
	Public nPolicy_Year__End As Integer
	Public nGuarSav_Year As Integer
	Public dEffecdate As Date
	Public nUsercode As Integer
	
	
	Private mvarGuar_saving_allows As Guar_saving_allows
	
	
	
	
	Public Property Guar_saving_allows() As Guar_saving_allows
		Get
			If mvarGuar_saving_allows Is Nothing Then
				mvarGuar_saving_allows = New Guar_saving_allows
			End If
			
			
			Guar_saving_allows = mvarGuar_saving_allows
		End Get
		Set(ByVal Value As Guar_saving_allows)
			mvarGuar_saving_allows = Value
		End Set
	End Property
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarGuar_saving_allows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarGuar_saving_allows = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	
	Public Function insValDP8005(ByVal nGuarSav_Max As Integer, ByVal nPolicy_Year_Ini As Integer, ByVal nPolicy_Year_End As Integer, ByVal nGuarSav_Year As Integer) As String
		Dim lclsObject As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
		
        Dim lstrErrorAll As String = String.Empty
		
		lclsObject = New eRemoteDB.Execute
		
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSDP8005PKG.VALDP8005_REP"
			
			.Parameters.Add("nGuarSav_Max", nGuarSav_Max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_Year_Ini", nPolicy_Year_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_Year_End", nPolicy_Year_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarSav_Year", nGuarSav_Year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayErrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
			
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage("DP8005",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			insValDP8005 = .Confirm
		End With
		
Find_Err: 
		If Err.Number Then insValDP8005 = Err.Description
		
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	Public Function insPostDP8005(ByVal nAction As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy_Year_Ini As Integer, ByVal nPolicy_Year_End As Integer, ByVal nGuarSav_Year As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsObject As eRemoteDB.Execute
		
		lclsObject = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSDP8005PKG.UPDDP8005_REP"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_Year_Ini", nPolicy_Year_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_Year_End", nPolicy_Year_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGuarSav_Year", nGuarSav_Year, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostDP8005 = .Run(False)
			
		End With
Find_Err: 
		If Err.Number Then insPostDP8005 = False
		
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		On Error GoTo 0
	End Function
End Class






