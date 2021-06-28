Option Strict Off
Option Explicit On
Friend Class Phones
    '%-------------------------------------------------------%'
    '% $Workfile:: Phones.cls                               $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 5                                        $%'
    '%-------------------------------------------------------%'

    '**% Find. This function is used for reading operations depending on the type of folder that called it.
    '%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
    '%tipo de carpeta que la invoco.
    Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim caseAux As eRemoteDB.Execute = New eRemoteDB.Execute
        Select Case nParentFolder
            Case 4, 17, 40 '- Cliente, de la figura o del proveedor
                caseAux = insReaPhonesCli(Parameters("sClient").Valor, (Parameters("HdEffecdate").Valor))
            Case 1, 3, 5, 11 '- Poliza,solicitud, cotización o certificado
                caseAux = insReaPhonesPol(Parameters("HsCertype").Valor, Parameters("HnBranch").Valor, Parameters("HnProduct").Valor, Parameters("HnPolicy").Valor, Parameters("HnCertif").Valor, (Parameters("HdEffecdate").Valor))
            Case 6 '-Siniestro
                caseAux = insReaPhonesCla(Parameters("HnClaim").Valor)
            Case 20 '-Telefonos
                caseAux = insReaPhones(Parameters("HnRecOwner").Valor, Parameters("HsKeyaddress").Valor, (Parameters("HdEffecdate").Valor))
        End Select
        Return caseAux
    End Function
    '%**% insReaPhones. Telefono puntual
    '
    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaPhones(ByVal lintRecOwner As Integer, ByVal lstrKeyAddress As String, Optional ByVal ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute
		
		insReaPhones = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatPhones'
		'+Definición de parámetros para stored procedure 'insudb.queDatPhones'
		'**+ Information read on December 02,1999 09:07:11 a.m.
		'+Información leída el 02/12/1999 09:07:11 a.m.
		
		With insReaPhones
			.StoredProcedure = "queDatPhonespkg.Find"
			.Parameters.Add("nRecOwner", lintRecOwner, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKeyAddress", lstrKeyAddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaPhones may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPhones = Nothing
			End If
		End With
		
	End Function
	
	'%insReaPhonesPol. Esta funcion retorna los telefonos de una poliza/certificado
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
    Private Function insReaPhonesPol(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Integer, Optional ByVal ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute

        insReaPhonesPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatAddressPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatAddressPol'
        '**+ Information read on January 06,2000  11:19:38 a.m.
        '+Información leída el 06/01/2000 11:19:38 a.m.

        With insReaPhonesPol
            .StoredProcedure = "queDatPhonespkg.FindPol"
            .Parameters.Add("p_sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("p_dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaPhonesPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaPhonesPol = Nothing
            End If
        End With
    End Function
	
	'%insReaPhonesCli. Esta funcion retorna los telefonos de un cliente
	'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
	Private Function insReaPhonesCli(ByVal lstrClient As String, Optional ByVal ldtmEffecdate As Object = Nothing) As eRemoteDB.Execute
		insReaPhonesCli = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClientPol'
		'+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
		'**+ Information read on Novemeber 25, 1999  02:52:20 p.m.
		'+Información leída el 25/11/1999 02:52:20 p.m.
		
		With insReaPhonesCli
			.StoredProcedure = "queDatPhonespkg.FindClient"
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
            .Parameters.Add("dEffecdate", IIf(ldtmEffecdate = eRemoteDB.Constants.dtmNull, Today, ldtmEffecdate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaPhonesCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPhonesCli = Nothing
			End If
		End With
		
	End Function
	
	'%insReaPhonesCla. esta funcion retorna los telefonos de un siniestro
	Private Function insReaPhonesCla(ByVal llngClaim As Double) As eRemoteDB.Execute
		insReaPhonesCla = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatAddressCla'
		'+Definición de parámetros para stored procedure 'insudb.queDatAddressCla'
		'**+ Information read on January 06,2001 12:21:45 p.m.
		'+Información leída el 06/01/2000 12:21:45 p.m.
		
		With insReaPhonesCla
			.StoredProcedure = "queDatPhonespkg.FindClaim"
			.Parameters.Add("nClaim", llngClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaPhonesCla may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaPhonesCla = Nothing
			End If
		End With
		
	End Function
End Class






