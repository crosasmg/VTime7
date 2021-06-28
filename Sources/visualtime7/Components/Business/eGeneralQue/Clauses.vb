Option Strict Off
Option Explicit On
Friend Class Clauses
	'%-------------------------------------------------------%'
	'% $Workfile:: Clauses.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: This function is used to obtain the clauses.
	'%Find. Se utiliza esta funcion para obtener las clausulas
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Dim ldtmEffecdate As Date
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(Parameters("HdEffecdate").Valor) Then
			ldtmEffecdate = Today
		Else
			ldtmEffecdate = CDate(Parameters("HdEffecdate").Valor)
		End If
		
		Select Case nParentFolder
			Case 1, 11, 5, 3 '**- Policy coverage/Quotes/Apllication
				'- Coberturas de la póliza/Cotización/solicitud
				Find = insreaClauses((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(nParentFolder = 1, 0, Parameters("nCertif").Valor), ldtmEffecdate)
			Case Else
				If nParentFolder <> 0 Then
					Find = insreaClauses((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), IIf(nParentFolder = 1, 0, Parameters("nCertif").Valor), ldtmEffecdate, (Parameters("nClause").Valor), (Parameters("nId").Valor))
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
	
	'**% insreaClauses. This function searches the policy clauses
	'%insreaClauses. Esta función se encarga de buscar las clausulas de la poliza
    Private Function insreaClauses(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer, ByRef ldmtEffecdate As Object, Optional ByRef nClause As Integer = eRemoteDB.Constants.intNull, Optional ByRef nId As Integer = eRemoteDB.Constants.intNull) As eRemoteDB.Execute

        insreaClauses = New eRemoteDB.Execute


        '**+ Parameter definition for stored procedure 'insudb.quedatDisco_expr'
        '+Definición de parámetros para stored procedure 'insudb.quedatDisco_expr'
        '**+ Information read on Decemeber 03, 1999  02:08:30 p.m.
        '+Información leída el 03/12/1999 02:08:30 p.m.

        With insreaClauses
            .StoredProcedure = "queDatClause"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldmtEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClause", nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insreaClauses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insreaClauses = Nothing
            End If
        End With
    End Function
End Class






