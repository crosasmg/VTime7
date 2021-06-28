Option Strict Off
Option Explicit On
Friend Class Addresses
	'%-------------------------------------------------------%'
	'% $Workfile:: Addresses.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 8                                        $%'
	'%-------------------------------------------------------%'
	'prueba
	'**% Find: Use this function to obtain the links and load it to the properties collection
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 4, 17, 40 '- Direcciones de un cliente, de la figura o del proveedor
				Find = insReaAddressCli((Parameters("sClient").Valor), (Parameters("HdEffecdate").Valor))
			Case 1, 3, 5, 11 '- Direcciones de poliza,solicitud, cotización o certificado
				Find = insReaAddressPol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), (Parameters("HdEffecdate").Valor))
			Case 6 '-Siniestro
				Find = insReaAddressCla((Parameters("nClaim").Valor))
			Case 13 '-Direcciones
                Find = insReaAddress((Parameters("nRecOwner").Valor), (Parameters("sKeyaddress").Valor), (Parameters("HdEffecdate").Valor))
            Case Else
                Find = Nothing
        End Select
	End Function
	
	'**% insReaAddressCli. This function returns the addresses of a client.
	'%insReaAddressCli. esta funcion retorna las direcciones de un cliente
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaAddressCli(ByRef lstrClient As String, Optional ByRef ldtmEffecdate As Object = eRemoteDB.Constants.dtmNull) As eRemoteDB.Execute
        insReaAddressCli = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatClientPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on Novemeber 25, 1999  02:52:20 p.m.
        '+Información leída el 25/11/1999 02:52:20 p.m.

        With insReaAddressCli
            .StoredProcedure = "queDatAddressCli"
            .Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dEffecdate", IIf(ldtmEffecdate = eRemoteDB.Constants.dtmNull, Today, ldtmEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaAddressCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaAddressCli = Nothing
            End If
        End With
    End Function
	
	'**% insReaAddress: this function returns the addresses.
	'%insReaAddress. esta funcion retorna las direcciones
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
	Private Function insReaAddress(ByRef lintRecOwner As Integer, ByRef lstrKeyAddress As String, Optional ByRef ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute
		insReaAddress = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatCloentPol'
		'+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
		'**+ Information read on Novemeber 25, 1999  02:52:20 p.m.
		'+Información leída el 25/11/1999 02:52:20 p.m.
		
		With insReaAddress
			.StoredProcedure = "queDatAddress"
			.Parameters.Add("nRecOwner", lintRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKeyAddress", lstrKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", IIf(ldtmEffecdate = eRemoteDB.Constants.dtmNull, Today, ldtmEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaAddress may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaAddress = Nothing
			End If
		End With
	End Function
	
	
	'**% insReaAddressPol. This function returns the addresses of a policy/certificate
	'%insReaAddressPol. esta funcion retorna las direcciones de una poliza/certificado
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaAddressPol(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, Optional ByRef ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaAddressPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatAddressPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatAddressPol'
        '**+ Information read on January 06,2000  11:19:38 a.m.
        '+Información leída el 06/01/2000 11:19:38 a.m.

        With insReaAddressPol
            .StoredProcedure = "queDatAddressPol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaAddressPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaAddressPol = Nothing
            End If
        End With
    End Function
	
	'**% insReaAddressCla. This function returns the addressess of a claim.
	'%insReaAddressCla. esta funcion retorna las direcciones de un siniestro
	Private Function insReaAddressCla(ByRef llngClaim As Double) As eRemoteDB.Execute
		insReaAddressCla = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatAddressCla'
		'+Definición de parámetros para stored procedure 'insudb.queDatAddressCla'
		'**+ Information read on January 06,2001 12:21:45 p.m.
		'+Información leída el 06/01/2000 12:21:45 p.m.
		
		With insReaAddressCla
			.StoredProcedure = "queDatAddressCla"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaAddressCla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaAddressCla = Nothing
			End If
		End With
	End Function
End Class






