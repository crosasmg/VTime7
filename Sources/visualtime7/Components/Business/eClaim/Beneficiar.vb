Option Strict Off
Option Explicit On
Public Class Beneficiar
	'%-------------------------------------------------------%'
	'% $Workfile:: Beneficiar.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 10/10/03 17.34                               $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Public sCerType As String
	Public nBranch As Integer
	Public nProduct As Integer
	Public nPolicy As Double
	Public nCertif As Double
	Public sClient As String
	Public dEffecdate As Date
	Public nParticip As Double
	Public nRelation As Integer
	Public nUsercode As Integer
	Public dDatedecla As Date
	Public sIrrevoc As String
	Public nModulec As Integer
	Public nCover As Integer
	
	
	'%InsExists: Verifica la existencia de beneficiarios por poliza
	Public Function InsExists(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal ldblPolicy As Double, ByVal ldblCertif As Double, ByVal ldtmEffecdate As Date) As Boolean
		Dim lrecBeneficiar As eRemoteDB.Execute
		Dim nExists As Integer
		Dim lintExist As Integer
		
		On Error GoTo InsExists_Err
		
		nExists = 0
		InsExists = True
		
		lrecBeneficiar = New eRemoteDB.Execute
		
		With lrecBeneficiar
			.StoredProcedure = "insExists_Beneficiar"
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", ldblPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", ldblCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				lintExist = .Parameters.Item("nExists").Value
				If lintExist > 0 Then
					InsExists = True
				Else
					InsExists = False
				End If
			Else
				InsExists = False
			End If
		End With
		
		lrecBeneficiar = Nothing
		
InsExists_Err: 
		If Err.Number Then
			InsExists = False
		End If
		On Error GoTo 0
	End Function
End Class






