Option Strict Off
Option Explicit On
Public Class Insured_expdis
	'%-------------------------------------------------------%'
	'% $Workfile:: Insured_expdis.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used to obtain the clauses.
	'%Find. Se utiliza esta funcion para obtener las clausulas
	Public Function Find(ByVal nParentFolder As Integer, ByVal Parameters As Properties) As eRemoteDB.Execute
		Find = insReaLifecov_var(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, Parameters("nCertif").Valor, Parameters("HdEffecdate").Valor)
	End Function
	'**% Lee el cursos que retorna los cambios realizados en la tabla
	'**% Cover a partir de una fecha
    Function insReaLifecov_var(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As eRemoteDB.Execute
        insReaLifecov_var = New eRemoteDB.Execute

        With insReaLifecov_var
            .StoredProcedure = "QUEDATINSURED_EXPDIS"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaLifecov_var may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaLifecov_var = Nothing
            End If
        End With
    End Function
End Class






