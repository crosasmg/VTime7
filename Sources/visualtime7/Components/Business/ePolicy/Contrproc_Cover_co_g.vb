Option Strict Off
Option Explicit On
Public Class Contrproc_Cover_co_g
	'%-------------------------------------------------------%'
	'% $Workfile:: Contrproc_Cover_co_g.cls                 $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 29/01/04 18.01                               $%'
	'% $Revision:: 22                                       $%'
	'%-------------------------------------------------------%'
	
	'   Column_name                  Type                     Computed Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'   --------------------------- ------------------------ -------- ----------- ----- ----- -------- ------------------ --------------------
	Public nCover As Integer
	Public nBranchRei As Integer
	Public nNumber As Integer
	Public nType As Integer
	Public sCoverDesc As String
	Public sBranch_Reides As String 'Descripcion del ramo de reaseguro
	Public sDesc_Contrato As String 'Descripcion del Tipo de Contrato
	Public dDate_Contrato As Date 'Fecha de efecto del contrato
	
	Public nPriority As Short
	Public nCapital_Rei As Double
	Public nShare_Rei As Double
	
	Public nCompany As Integer
	Public sCliename As String
	Public nClasific As Short
	Public sDesc_Clasif As String
	Public nCapital As Double
	Public nShare As Double
	Public nCommissi As Double
	Public nReser_rate As Double
	Public nInter_rate As Double
	Public dAcceDate As Date
	
	
	
	'% Find_Prioridad: Rescata la prioridad de aplicación de los contratos de Reaseguro
	Public Function Find_Priority(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Find_Priority_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "ReaPriority_Contratos"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPriority", nPriority, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCapital_rei", nCapital_Rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nShare_Rei", nShare_Rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Me.nPriority = .Parameters("nPriority").Value
				Me.nCapital_Rei = .Parameters("nCapital_Rei").Value
				Me.nShare_Rei = .Parameters("nShare_Rei").Value
				Find_Priority = True
			End If
		End With
		
Find_Priority_Err: 
		If Err.Number Then
			Find_Priority = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
End Class






