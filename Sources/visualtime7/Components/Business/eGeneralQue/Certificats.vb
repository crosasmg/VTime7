Option Strict Off
Option Explicit On
Friend Class Certificats
	'%-------------------------------------------------------%'
	'% $Workfile:: Certificats.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: Use this function for read operations depending on the folder type that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			'- Coberturas de la póliza/Cotización/solicitud
			Case 1, 11, 5
				Find = insReaCertificatsPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), Parameters("HsClient").Valor, (Parameters("HdEffecdate").Valor))
				
				'-Certificados
			Case 3
				Find = insReaCertificat((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("HdEffecdate").Valor))
			Case Else
				If CDbl(0 & nParentFolder) = 0 Then
					Find = insReaCertificat((Parameters("HsCertype").Valor), (Parameters("HnBranch").Valor), (Parameters("HnProduct").Valor), (Parameters("HnPolicy").Valor), (Parameters("HnCertif").Valor), (Parameters("HdEffecdate").Valor))
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
	
	'**%insReaCertificatsPol. returns the clients of a policy that receipts as a parameter.
	'%insREaCertificatsPol. esta funcion retorna los Clientes de una póliza que recibe como parámetro
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaCertificatsPol(ByRef sCertype As String, ByRef nBranch As Integer, ByRef nProduct As Integer, ByRef nPolicy As Double, Optional ByRef sClient As String = "", Optional ByRef dEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaCertificatsPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatClientPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on Novemeber 25,1999  02:52:20 p.m.
        '+Información leída el 25/11/1999 02:52:20 p.m.

        With insReaCertificatsPol
            .StoredProcedure = "queDatCertificatPol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaCertificatsPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCertificatsPol = Nothing
            End If
        End With
    End Function

    '**% insReaCertificat. This function returns the Clients of a policy that receipts as a parameter.
    '%insReaCertificat. esta funcion retorna los Clientes de una póliza que recibe como parámetro
    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaCertificat(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertificat As Integer, Optional ByRef dEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaCertificat = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatClientPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on Novemeber 25, 1999  02:52:20 p.m.
        '+Información leída el 25/11/1999 02:52:20 p.m.

        With insReaCertificat
            .StoredProcedure = "queDatCertificat"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertificat", llngCertificat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCertificat = Nothing
            End If
        End With
    End Function
End Class






