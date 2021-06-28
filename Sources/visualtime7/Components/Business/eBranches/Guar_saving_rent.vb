Option Strict Off
Option Explicit On
Public Class Guar_saving_rent
	'%-------------------------------------------------------%'
	'% $Workfile:: Disc_riskInsu.cls                        $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla DISC_RISKINSU tomada el 07/11/2001 16:14
	
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ --------------- - -------- ------- ----- ------ --------
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nValue_Ini As Double ' NUMBER        22    18      6 No
	Public nValue_End As Double ' NUMBER        22    18      6 No
	Public dEffecdate As Date ' DATE           7              No
	Public dNulldate As Date ' DATE           7              Yes
	Public nUsercode As Integer
	Public nGuarant_year As Short ' NUMBER        22     9      6 No
	Public nGuarant_rent As Double ' NUMBER        22     9      6 No
	
	Private mvarGuar_saving_rents As Guar_saving_rents
	
	
	
	Public Property Guar_saving_rents() As Guar_saving_rents
		Get
			If mvarGuar_saving_rents Is Nothing Then
				mvarGuar_saving_rents = New Guar_saving_rents
			End If
			
			
			Guar_saving_rents = mvarGuar_saving_rents
		End Get
		Set(ByVal Value As Guar_saving_rents)
			mvarGuar_saving_rents = Value
		End Set
	End Property
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mvarGuar_saving_rents may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mvarGuar_saving_rents = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	'%  Find: Busca un registron dentro de la tabla Disc_riskInsu
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCapital_init As Double) As Boolean
		Dim lclsObject As eRemoteDB.Execute
		
		lclsObject = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSMVI8000PKG.REAMVI8000"
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nValue_Ini = .FieldToClass("NGUARSAV_VALINI")
				nValue_End = .FieldToClass("NGUARSAV_VALEND")
				dEffecdate = .FieldToClass("dEffecdate")
				nGuarant_year = .FieldToClass("NGUARSAV_YEAR")
				nGuarant_rent = .FieldToClass("NREN_GUARSAV")
				
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then Find = False
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
	End Function
	
	Public Function insPostMVI8000(ByVal nAction As Short, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nValue_Ini As Double, ByVal nValue_End As Double, ByVal nRenGuar As Double, ByVal nYear As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lclsObject As eRemoteDB.Execute
		
		lclsObject = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSMVI8000PKG.UPDVALMVI8000"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NGUARSAV_VALINI", nValue_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NGUARSAV_VALEND", nValue_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NREN_GUARSAV", nRenGuar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NGUARSAV_YEAR", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostMVI8000 = .Run(False)
			
		End With
Find_Err: 
		If Err.Number Then insPostMVI8000 = False
		
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		On Error GoTo 0
	End Function
	
	
	Public Function insValMVI8000(ByVal sZone As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nValue_Ini As Double, ByVal nValue_End As Double, ByVal nRenGuar As Double, ByVal nYear As Integer, ByVal dEffecdate As Date, Optional ByVal nAction As Integer = 0) As String
		Dim lclsObject As eRemoteDB.Execute
		Dim lclsErrors As eFunctions.Errors
		
        Dim lstrErrorAll As String = String.Empty
		
		lclsObject = New eRemoteDB.Execute
		
		
		On Error GoTo Find_Err
		
		With lclsObject
			.StoredProcedure = "INSMVI8000PKG.VALMVI8000"
			
			.Parameters.Add("sZone", sZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NGUARSAV_VALINI", nValue_Ini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NGUARSAV_VALEND", nValue_End, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NREN_GUARSAV", nRenGuar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("NGUARSAV_YEAR", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sArrayErrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lstrErrorAll = .Parameters("sArrayerrors").Value
			End If
			
		End With
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If Len(lstrErrorAll) > 0 Then
				Call .ErrorMessage("MVI8000",  ,  ,  ,  ,  , lstrErrorAll)
			End If
			insValMVI8000 = .Confirm
		End With
		
Find_Err: 
		If Err.Number Then insValMVI8000 = CStr(False)
		
		'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsObject = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
End Class






