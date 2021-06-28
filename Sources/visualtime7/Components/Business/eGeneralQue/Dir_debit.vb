Option Strict Off
Option Explicit On
Public Class Dir_debit
	
	'**% Find. This function is used for read operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Params As Properties) As eRemoteDB.Execute
		Find = New eRemoteDB.Execute
		
		Select Case nParentFolder
			'**+
			'+ Vías de Cobro del Cliente'
			Case 1
				Find = insReaDir_Debit((Params("sCertype").Valor), (Params("nBranch").Valor), (Params("nProduct").Valor), (Params("nPolicy").Valor), IIf(nParentFolder = 1, 0, Params("nCertif").Valor))
			Case Else
				If nParentFolder <> 0 Then
					Find = insReaDir_Debit((Params("sCertype").Valor), (Params("nBranch").Valor), (Params("nProduct").Valor), (Params("nPolicy").Valor), IIf(nParentFolder = 1, 0, Params("nCertif").Valor))
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
	
	
	
	'**%insReaHealth: .
	'% insReaHealth: Lee la información de nivel socioeconomico.
    Function insReaDir_Debit(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, ByRef llngCertif As Integer) As eRemoteDB.Execute

        insReaDir_Debit = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatLevel'
        '+ Definición de parámetros para stored procedure 'insudb.queDatLevel'

        With insReaDir_Debit
            .StoredProcedure = "queDatDir_Debit"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insReaDir_Debit may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaDir_Debit = Nothing
            End If
        End With

    End Function
End Class






