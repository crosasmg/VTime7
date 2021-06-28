Option Strict Off
Option Explicit On
Friend Class Disco_expr
	'%-------------------------------------------------------%'
	'% $Workfile:: Disco_expr.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**%Find: This function is used to obtain the links and load them to the properties collection.
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByVal nParentFolder As Integer, ByVal Parameters As Properties) As eRemoteDB.Execute
		Dim ldtmEffecdate As Date
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(Parameters("HdEffecdate").Valor) Then
			ldtmEffecdate = Today
		Else
			ldtmEffecdate = CDate(Parameters("HdEffecdate").Valor)
		End If
		
		Select Case nParentFolder
			Case 1, 11, 5, 3 '- Coberturas de la póliza/Cotización/solicitud o certificado
				Find = insReaDisco_expr(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, IIf(nParentFolder = 1, 0, Parameters("nCertif").Valor), ldtmEffecdate)
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insreaDisco_expr. This function is in charge of returning the recharges and discounts of
	'**% a policy/certificate.
	'%insreaDisco_expr. Esta función se encarga de devolver los recargos y descuentos de una
	'% póliza/certificado.
    Private Function insReaDisco_expr(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Date) As eRemoteDB.Execute
        '**+Parameters definition for stored procedure 'insudb.quedatDisco_expr'
        '+Definición de parámetros para stored procedure 'insudb.quedatDisco_expr'
        '**+ Information read on December 06,1999 02:08:30 p.m.
        '+Información leída el 03/12/1999 02:08:30 p.m.
        insReaDisco_expr = New eRemoteDB.Execute
        With insReaDisco_expr
            .StoredProcedure = "quedatDisco_expr"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaDisco_expr may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaDisco_expr = Nothing
            End If
        End With
    End Function
End Class






