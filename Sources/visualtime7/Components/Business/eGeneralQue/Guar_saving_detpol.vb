Option Strict Off
Option Explicit On
Public Class Guar_saving_detpol
	'%-------------------------------------------------------%'
	'% $Workfile:: Clients.cls                              $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: This function is used for read operations depending of the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Select Case nParentFolder
			'**+ Policy clientes/certificate
			'+ Clientes de la póliza/Certificado'
			Case 1, 11, 5, 3
				Find = insReaGuar_saving_detpol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), Parameters("nGuarsavid").Valor)
				
				'**+ Guaranteed Savings
				'+ Ahorro Garantizado
			Case 93
				Find = insReaGuar_saving_detpol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), Parameters("nGuarsavid").Valor)
			Case Else
				If nParentFolder = 0 Then
					Find = insReaGuar_saving_detpol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), Parameters("nGuarsavid").Valor)
				Else
					Find = insReaGuar_saving_detpol((Parameters("sCertype").Valor), (Parameters("nBranch").Valor), (Parameters("nProduct").Valor), (Parameters("nPolicy").Valor), (Parameters("nCertif").Valor), Parameters("nGuarsavid").Valor)
				End If
		End Select
	End Function
	
	'**% insReaClientsPol. This function returns the movements of Guaranteed Savings of a policy that receipt as a parameter.
	'%insREaClientsPol. esta funcion retorna los movimientos de Ahorros garantizados de una póliza que recibe como parámetro
    Private Function insReaGuar_saving_detpol(ByRef lstrCertype As String, ByRef lintBranch As Integer, ByRef lintProduct As Integer, ByRef llngPolicy As Double, Optional ByRef llngCertif As Integer = 0, Optional ByRef lintGuarsavid As Integer = 0) As eRemoteDB.Execute

        insReaGuar_saving_detpol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudbqueDatClientPol'
        '+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
        '**+ Information read on Novemeber 25,1999  02:52:20 p.m.
        '+Información leída el 25/11/1999 02:52:20 p.m.

        With insReaGuar_saving_detpol
            .StoredProcedure = "quedatguar_saving_detpol"
            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGuarsavid", lintGuarsavid, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaGuar_saving_detpol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaGuar_saving_detpol = Nothing
            End If
        End With
    End Function
End Class






