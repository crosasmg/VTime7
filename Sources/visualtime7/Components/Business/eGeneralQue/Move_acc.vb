Option Strict Off
Option Explicit On
Friend Class Move_Acc
	'%-------------------------------------------------------%'
	'% $Workfile:: Move_Acc.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	'**% Find. This function is used to obtain the links and load the to the properties collection.
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 47
				Find = insReaMove_Acc((Parameters("sType_acc").Valor), (Parameters("nTyp_acco").Valor), (Parameters("nCurrency").Valor), (Parameters("sClient").Valor), (Parameters("HdEffecdate").Valor))
				
			Case 72
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
				Find = insReaMove_Accpol(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), System.DBNull.Value, Parameters("nOrigin").Valor)
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insReaMove_Acc. This function restores the transactions of a current account.
	'%insReaMove_Acc. Esta Función devuelve los movimientos de la cuenta corriente
	Private Function insReaMove_Acc(ByRef lstrType_acc As String, ByRef lintTyp_acco As Integer, ByRef lintCurrency As Integer, ByRef lstrClient As String, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute
		
		insReaMove_Acc = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClaimCli'
		'+Definición de parámetros para stored procedure 'insudb.queDatClaimCli'
		'**+ Information read on Novemeber 29,1999  03:01:50 p.m.
		'+Información leída el 29/11/1999 03:01:50 p.m.
		
		With insReaMove_Acc
			.StoredProcedure = "queDatMove_Acc"
			.Parameters.Add("sType_acc", lstrType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", lintTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", lintCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaMove_Acc = Nothing
			End If
		End With
	End Function
	
	'**% insReaMove_AccPol. This function returns a policy's current accounts (passed as a parameter)
	'%insReaCurr_accPol. Esta Función devuelve las cuentas corrientes de una póliza (pasada como parámetro)
    Private Function insReaMove_Accpol(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Integer, ByVal ldtmEffecdate As Object, Optional ByRef lintOrigin As Integer = 0) As eRemoteDB.Execute
        insReaMove_Accpol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatPolicyMove_Acc'
        '+Definición de parámetros para stored procedure 'insudb.queDatPolicyMove_Acc'
        '**+ Information read on November 29,1999 03:01:50 p.m.
        '+Información leída el 29/11/1999 03:01:50 p.m.

        With insReaMove_Accpol
            .StoredProcedure = "queDatPolicyMove_Acc"

            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", lintOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insReaMove_Accpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaMove_Accpol = Nothing
            End If
        End With
    End Function
End Class






