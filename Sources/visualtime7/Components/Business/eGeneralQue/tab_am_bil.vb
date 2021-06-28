Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Tab_am_bil_NET.Tab_am_bil")> Public Class Tab_am_bil
	'**% Find. This function is used to obtain the result of each folder.
	'%Find. Se utiliza esta funcion para obtener el resultado de cada carpeta
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			Case 1 '- Poliza
				Find = insTab_am_bil((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("HdEffecdate").Valor))
				
				'            Set Find = insTab_am_bilPol(Parameters("sCertype").Valor, Parameters("nBranch").Valor, _
				''                                        Parameters("nProduct").Valor, Parameters("nPolicy").Valor, _
				''                                        Parameters("HdEffecdate").Valor)
			Case 94 '-Prestaciones
				Find = insTab_am_bil((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("HdEffecdate").Valor), Parameters("nGroup_insu").Valor)
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**% insTab_am_bil.
	'%insTab_am_bil. esta funcion retorna la prima para el producto salud
    Private Function insTab_am_bil(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef lintPolicy As Double, ByRef ldtmEffecdate As Date, Optional ByRef lintGroup As Integer = 0) As eRemoteDB.Execute

        insTab_am_bil = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.insTab_am_bil'
        '+Definición de parámetros para stored procedure 'insudb.insTab_am_bil'
        '**+ Information read on December 07,1999.
        '+Información leída el 07/12/1999

        With insTab_am_bil
            .StoredProcedure = "queDatTab_am_bil"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", lintPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", lintGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insTab_am_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insTab_am_bil = Nothing
            End If
        End With
    End Function

    '**% insTab_am_bil.
    '%insTab_am_bil. esta funcion retorna la prima para el producto salud
    Private Function insTab_am_bilPol(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef lintPolicy As Double, ByRef ldtmEffecdate As Date) As eRemoteDB.Execute

        insTab_am_bilPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.insTab_am_bil'
        '+Definición de parámetros para stored procedure 'insudb.insTab_am_bil'
        '**+ Information read on December 07,1999.
        '+Información leída el 07/12/1999

        With insTab_am_bilPol
            .StoredProcedure = "queDatTab_am_bilPol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", lintPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insTab_am_bilPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insTab_am_bilPol = Nothing
            End If
        End With
    End Function
End Class






