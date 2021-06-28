Option Strict Off
Option Explicit On
Friend Class Curr_acc
	'%-------------------------------------------------------%'
	'% $Workfile:: Curr_acc.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	Public mblnFind As Boolean
	
	'**% Find: This function is used to obtain the links and load them to the properties collection.
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim lclsRemote As eRemoteDB.Execute
        Dim caseAux As eRemoteDB.Execute = New eRemoteDB.Execute

        Select Case nParentFolder

            Case 1
                lclsRemote = New eRemoteDB.Execute

                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                lclsRemote = insReaCurr_accPol(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), (Parameters("HdEffecdate").Valor))

                If mblnFind Then
                    caseAux = lclsRemote
                Else
                    'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    caseAux = Nothing
                End If

                'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclsRemote = Nothing
            Case 4
                caseAux = insReaCurr_accCli((Parameters("sClient").Valor), (Parameters("HdEffecdate").Valor))

            Case Else
                If nParentFolder <> 0 Then
                    lclsRemote = New eRemoteDB.Execute

                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                    lclsRemote = insReaCurr_accPol(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, IIf(Parameters("nCertif").Valor <> "" And Parameters("nCertif").Valor <> String.Empty, Parameters("nCertif").Valor, 0), System.DBNull.Value, eRemoteDB.Constants.intNull, Parameters("nOrigin").Valor)

                    If mblnFind Then
                        caseAux = lclsRemote
                    Else
                        'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        caseAux = Nothing
                    End If

                    'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                    lclsRemote = Nothing

                End If
        End Select
        Return caseAux
    End Function
	
	'**% insReaCurr_accClie. This function returns a client's current accounts (passed as a parameter)
	'%insReaCurr_accCli. Esta Función devuelve las cuentas corrientes de un cliente (pasado como parametro)
	Private Function insReaCurr_accCli(ByRef lstrClient As String, ByRef ldtmEffecdate As Object, Optional ByRef lintTyp_acco As Integer = 5, Optional ByRef lstrType_acc As String = "0") As eRemoteDB.Execute
		
		insReaCurr_accCli = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClaimCli'
		'+Definición de parámetros para stored procedure 'insudb.queDatClaimCli'
		'**+ Information read on November 29,1999 03:01:50 p.m.
		'+Información leída el 29/11/1999 03:01:50 p.m.
		
		With insReaCurr_accCli
			.StoredProcedure = "queDatCurr_accCli"
			.Parameters.Add("sType_acc", lstrType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTyp_acco", lintTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", lstrClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insReaCurr_accCli may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaCurr_accCli = Nothing
			End If
		End With
	End Function
	
	'**% insReaCurr_accPol. This function returns a policy's current accounts (passed as a parameter)
	'%insReaCurr_accPol. Esta función devuelve las cuentas corrientes de una póliza (pasada como parámetro)
    Private Function insReaCurr_accPol(ByVal lstrCertype As String, ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal llngPolicy As Double, ByVal llngCertif As Integer, ByVal ldtmEffecdate As Object, Optional ByRef lintFunds As Integer = 0, Optional ByRef lintOrigin As Integer = 0) As eRemoteDB.Execute
        insReaCurr_accPol = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.queDatPolicyCurr_acc'
        '+Definición de parámetros para stored procedure 'insudb.queDatPolicyCurr_acc'
        '**+ Information read on November 29,1999 03:01:50 p.m.
        '+Información leída el 29/11/1999 03:01:50 p.m.

        With insReaCurr_accPol
            .StoredProcedure = "queDatPolicyCurr_acc"

            .Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", llngPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", llngCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", lintFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", lintOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If Not .Run Then
                'UPGRADE_NOTE: Object insReaCurr_accPol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCurr_accPol = Nothing
            Else
                mblnFind = True

                If .FieldToClass("nPolicy") = 0 Then
                    mblnFind = False
                End If
            End If
        End With
    End Function
	
	'*** Class_Initialize: Assigns the initial values to the properties of the class
	'* Class_Initialize: Asigna los valores iniciales a las propiedades de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		mblnFind = True
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






