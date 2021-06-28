Option Strict Off
Option Explicit On
Public Class Commiss_pr
	'%-------------------------------------------------------%'
	'% $Workfile:: Commiss_pr.cls                           $%'
	'% $Author:: Nvaplat19                                  $%'
	'% $Date:: 25/08/03 6:46p                               $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Los campos corresponden a Commiss_pr
	'+--------------------------------------------------------------------------
	Public sCertype As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nReceipt As Double
	Public nDigit As Integer
	Public nPaynumbe As Integer
	Public nIntermed As Double
	Public nProvince As Integer
	Public nAmount As Double
	Public dCompdate As Date
	Public nPercent As Integer
	Public nRole As Integer
	Public nShare As Integer
	Public nUsercode As Integer
	Public nCom_afec As Double
	Public nCom_exen As Double
	Public nDisc_amount As Integer
	
	'+ Variables de comisiones acumuladas
	Public nAmount_sum As Double
	Public nCom_afec_sum As Double
	Public nCom_exen_sum As Double
	
	
	'% Find: Suma las comisiones afectas y exentas de los recibos
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nDigit As Integer, ByVal nPaynumbe As Integer, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lreaCommiss_pr As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lreaCommiss_pr = New eRemoteDB.Execute
		
		With lreaCommiss_pr
			.StoredProcedure = "REACOMMISS_PR"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", nDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPaynumbe", nPaynumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Me.nAmount_sum = .FieldToClass("nAmount")
				Me.nCom_afec_sum = .FieldToClass("nCom_afec")
				Me.nCom_exen_sum = .FieldToClass("nCom_exen")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreaCommiss_pr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreaCommiss_pr = Nothing
		
	End Function
End Class






