Option Strict Off
Option Explicit On
Public Class Branchr
	'%-------------------------------------------------------%'
	'% $Workfile:: Branchr.cls                              $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 29/01/04 18.01                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'-Propiedades según la tabla en el sistema el 18/01/2000.
	
	'   Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'   ---------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nCurrency As Integer 'smallint     no       2           5     0     no       (n/a)              (n/a)
	Public nBranchRei As Integer
	Public sAddReini As String
	Public nCapital As Double
	Public nPremium As Double
	Public nCapital_max As Double
	Public nUsercode As Integer
	Public nRest As Double
	Public nRetention As Double
	Public nModulec As Integer
	Public nCover As Integer
	Public sClient As String
	Public nChange As Integer
	Public sHeapCode As String
	Public nNumber As Integer
	
	'-Se definen las variable auxiliares
	Public nType As Integer
	Public sCliename As String
	Public sCoverDesc As String
	Public sChangeDes As String 'Descripcion de cambio permitido
	Public sCurrDes As String 'Descripcion de la moneda
	Public sBranch_Reides As String 'Descripcion del ramo de reaseguro
	Public sDigit As String 'Digito verificador del cliente
	Public sGridCovDesc As String 'Descripcion corta de la cobertura
	Public sModuDesc As String 'Descripcion del modulo
	Public nCapital_cov As Double 'Capital de la cobertura
	Public nReserve As Double 'Monto de reserva
	Public nClasific As Integer 'Clasificación de riesgo de la compañía
	Public nCapital_Rei As Double 'Capital de Reaseguro
	Public sDesc_Contrato As String 'Descripcion del Tipo de Contrato
	Public dDate_Contrato As Date 'Fecha de efecto del contrato
	
	'-Se define la variable para indicar el estado de cada instancia en la colección
	
	Public nStatusInstance As Integer
	'UPGRADE_NOTE: Reinsurans was upgraded to Reinsurans_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Public Reinsurans As ePolicy.Reinsurans
	
	'%ReloadReinsuranPol: Este metodo recarga la informacion de la polica de reaseguro
	Public Function ReloadReinsuranPol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal sCumReint As String, ByVal sHeapCode As String, ByVal nChange As Integer, ByVal nCovergen As Integer) As Boolean
		Dim lrecreaBranchr_all As eRemoteDB.Execute
		Dim lclsReinsuran As ePolicy.Reinsuran
		Dim lcolReinsurans As ePolicy.Reinsurans
		
		lrecreaBranchr_all = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.reaBranchr_All'
		'+Información leída el 30/12/1999 15:17:05
		With lrecreaBranchr_all
            .StoredProcedure = "insCalCapReinsu"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranchRei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_rei", nCapital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRetention_rei", nRetention, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If nChange = 1 Then
				.Parameters.Add("nInitial", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			ElseIf nChange = 2 Then 
				.Parameters.Add("nInitial", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Parameters.Add("nInitial", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("sKey", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sHeapCode", sHeapCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChange", nChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCliename", sCliename, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverGen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			ReloadReinsuranPol = .Run()
			
			If ReloadReinsuranPol Then
				Do While Not .EOF
					lclsReinsuran = New ePolicy.Reinsuran
					lcolReinsurans = New ePolicy.Reinsurans
					
                    If Not (Reinsurans Is Nothing) Then
                        lclsReinsuran = lcolReinsurans.Add(1, sCertype, nBranch, nProduct, nPolicy, nCertif, .FieldToClass("nBranch_rei"), .FieldToClass("nType"), dEffecdate, 1, eRemoteDB.Constants.dtmNull, .FieldToClass("nCapital"), .FieldToClass("nCapitalMax"), 0, .FieldToClass("nCurrency"), sHeapCode, 0, .FieldToClass("nNumber"), 0, .FieldToClass("nQuotaSha"), eRemoteDB.Constants.dtmNull, "2", .FieldToClass("nModulec"), .FieldToClass("nCover"), .FieldToClass("sClient"), .FieldToClass("nChange"))
                        lclsReinsuran.Update(False)
                    End If
					.RNext()
				Loop 
				.RCloseRec()
				'UPGRADE_NOTE: Object lclsReinsuran may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsReinsuran = Nothing
				'UPGRADE_NOTE: Object lcolReinsurans may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lcolReinsurans = Nothing
			End If
		End With
		
		'UPGRADE_NOTE: Object lrecreaBranchr_all may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaBranchr_all = Nothing
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
        Reinsurans = New Reinsurans
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object Reinsurans_Renamed may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Reinsurans = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class






