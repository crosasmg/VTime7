Option Strict Off
Option Explicit On
Friend Class Reinsurances
	'%-------------------------------------------------------%'
	'% $Workfile:: Reinsurances.cls                         $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used for reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		
		
		Select Case nParentFolder
			Case 1 'Pólizas
				Find = insReaReinsuranPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), " ", eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, (Parameters("HdEffecdate").Valor))
			Case 33 'Reaseguro - distribucion
				Find = insReaReinsuranPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("sClient").Valor), (Parameters("nModulec").Valor), (Parameters("nCover").Valor), (Parameters("HdEffecdate").Valor))
				
			Case Else
				'            Set Find = insReaReinsuranPol(NumNull, Parameters("nBranch").Valor, _
				''                                          Parameters("nProduct").Valor, Parameters("nPolicy").Valor, _
				''                                          Parameters("nCertif").Valor, Parameters("sClient").Valor, _
				''                                          Parameters("nModulec").Valor, Parameters("nCover").Valor, _
				''                                          Parameters("HdEffecdate").Valor)
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insReaReinsuranPol. This function restores the existence distrubuitions of reinsurance
	'**% of a policy/certificate.
	'%insReaReinsuranPol. Esta funcion  devuelve las distribuciones de reaseguro
	'%existentes, de una poliza/certificado
	Private Function insReaReinsuranPol(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Double, ByRef lstrClient As String, ByRef lintModulec As Integer, ByRef lintCover As Integer, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute
		
		insReaReinsuranPol = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatReinsuranPol'
		'+Definición de parámetros para stored procedure 'insudb.queDatReinsuranPol'
		'**+ Information read on January 05,2000  03:16:31 p.m.
		'+Información leída el 05/01/2000 03:16:31 p.m.
		
		With insReaReinsuranPol
			.StoredProcedure = "queDatReinsuranPol"
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", lintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", lintCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaReinsuranPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaReinsuranPol = Nothing
			End If
		End With
	End Function
End Class






