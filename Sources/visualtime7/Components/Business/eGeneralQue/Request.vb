Option Strict Off
Option Explicit On
Public Class Request
	'%-------------------------------------------------------%'
	'% $Workfile:: Request.cls                              $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 30/08/09 11:17p                              $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'%Find: Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%      tipo de carpeta que la invoco.
	Public Function Find(ByVal nParentFolder As Integer, ByVal Parameters As Properties) As eRemoteDB.Execute
		
		If Parameters("HdEffecdate").Valor = eRemoteDB.Constants.dtmNull Then
			Parameters("HdEffecdate").Valor = Today
		End If
		
		'+ Rescates de una póliza
		Find = InsReaRequestPolicy(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, Parameters("nCertif").Valor, Parameters("HdEffecdate").Valor)
		
	End Function
	
	'%InsReaRequestPolicy: Retorna los rescates realizados a la póliza/certificado
    Private Function InsReaRequestPolicy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, ByVal dEffecdate As Date) As eRemoteDB.Execute
        On Error GoTo InsReaRequestPolicy_Err
        InsReaRequestPolicy = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatPremiumPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatPremiumPol'
        '**+ Information read on December 02,1999  09:57:49 a.m.
        '+Información leída el 02/12/1999 09:57:49 a.m.

        With InsReaRequestPolicy
            .StoredProcedure = "QueDatRequest"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object InsReaRequestPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                InsReaRequestPolicy = Nothing
            End If
        End With
InsReaRequestPolicy_Err:
        If Err.Number Then
            'UPGRADE_NOTE: Object InsReaRequestPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            InsReaRequestPolicy = Nothing
        End If
    End Function
End Class






