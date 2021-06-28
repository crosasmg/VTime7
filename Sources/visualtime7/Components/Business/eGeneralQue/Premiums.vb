Option Strict Off
Option Explicit On
Friend Class Premiums
    '%-------------------------------------------------------%'
    '% $Workfile:: Premiums.cls                             $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 6                                        $%'
    '%-------------------------------------------------------%'

    '**% Find. This function is used for reading operations depending on the type of folder that called it.
    '%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
    '%tipo de carpeta que la invoco.
    Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim caseAux As eRemoteDB.Execute = New eRemoteDB.Execute
        Select Case nParentFolder
            '**+ Receipts of a client
            '+ Recibos de un cliente'
            Case 4
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                caseAux = insReaPremiumsCli((Parameters("sClient").Valor), System.DBNull.Value)
                '**+ Receipts of a policy
                '+ Recibos de una poliza
            Case 1
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                caseAux = insReaPremiumsPolicy((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), 0, System.DBNull.Value)
                '**+ Receipts of a certificate
                '+ Recibos de un certificado
            Case 3
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                caseAux = insReaPremiumsPolicy((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), System.DBNull.Value)
                '**+ Individual receipt (when a receipt from the grid is selected)
                '+Recibo individual (cuando se selecciona un recibo del grid)
            Case 7
                caseAux = insReaPremium_o((Parameters("nReceipt").Valor))

            Case 10
                caseAux = insReaPremiumOrig((Parameters("sOrigReceipt").Valor))
                '**+ Receipts of a quotes
                '+Recibos de una cotización
            Case 11
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                caseAux = insReaPremiumsPolicy((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), System.DBNull.Value)

            Case Else
                If nParentFolder = 0 Then
                    Select Case Parameters("nCurrentQuery").Valor
                        Case 7
                            caseAux = insReaPremium_o((Parameters("HnReceipt").Valor))
                        Case 10
                            caseAux = insReaPremiumOrig((Parameters("HsOrigReceipt").Valor))
                    End Select
                Else
                    'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    caseAux = Nothing
                End If
        End Select
        Return caseAux
    End Function

    '**% insReaPremiumCli. This function returns the recipts of a client (passed as a parameter)
    '%insReaPremiumCli. esta funcion retorna los Recibos de un cliente (Pasado como parámetro)
    Private Function insReaPremiumsCli(ByRef lstrClient As String, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute
		
		insReaPremiumsCli = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatPremiumCli'
		'+Definición de parámetros para stored procedure 'insudb.queDatPremiumCli'
		'**+ Information read on November 29,1999  05:03:27 p.m.
		'+Información leída el 29/11/1999 05:03:27 p.m.
		
		With insReaPremiumsCli
			.StoredProcedure = "queDatPremiumCli"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaPremiumsCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPremiumsCli = Nothing
			End If
		End With
	End Function
	
	'**% insReaPremiumsCli. This function returns a specific receipt.
	'%insReaPremiumsCli. esta funcion retorna un recibo especifico
	Private Function insReaPremium_o(ByRef llngReceipt As Integer, Optional ByRef lstrCertype As String = "2", Optional ByRef lintDigit As Integer = 0, Optional ByRef lintPayNumbe As Integer = 0) As eRemoteDB.Execute
		
		insReaPremium_o = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatPremium'
		'+Definición de parámetros para stored procedure 'insudb.queDatPremium'
		'**+ Information read on December 13,1999 01:56:19 p.m.
		'+Información leída el 13/12/1999 01:56:19 p.m.
		
		With insReaPremium_o
			.StoredProcedure = "queDatPremium"
			.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", llngReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", lintDigit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", lintPayNumbe, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaPremium_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPremium_o = Nothing
			End If
		End With
	End Function
	
	'**% insReaPremiumsCli. This function returns a specific recipt
	'%insReaPremiumsCli. esta funcion retorna un recibo especifico
	Private Function insReaPremiumOrig(ByRef lstrOrigReceipt As String) As eRemoteDB.Execute
		
		insReaPremiumOrig = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatPremium'
		'+Definición de parámetros para stored procedure 'insudb.queDatPremium'
		'**+ Information read on December 13,1999  01:56:19 p.m.
		'+Información leída el 13/12/1999 01:56:19 p.m.
		
		With insReaPremiumOrig
			.StoredProcedure = "queDatPremiumOrig"
			.Parameters.Add("sOrigReceipt", lstrOrigReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaPremiumOrig may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPremiumOrig = Nothing
			End If
		End With
	End Function
	'**% MISSING
	'% MISSING
    Private Function insReaPremiumsPolicy(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute

        insReaPremiumsPolicy = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatPremiumPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatPremiumPol'
        '**+ Information read on December 02,1999  09:57:49 a.m.
        '+Información leída el 02/12/1999 09:57:49 a.m.

        With insReaPremiumsPolicy
            .StoredProcedure = "queDatPremiumPol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insReaPremiumsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaPremiumsPolicy = Nothing
            End If
        End With
    End Function
End Class






