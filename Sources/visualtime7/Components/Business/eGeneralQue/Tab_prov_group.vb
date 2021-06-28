Option Strict Off
Option Explicit On
Friend Class Tab_prov_group
    '%-------------------------------------------------------%'
    '% $Workfile:: Tab_prov_group.cls                       $%'
    '% $Author:: Nvaplat7                                   $%'
    '% $Date:: 9/08/03 1:21p                                $%'
    '% $Revision:: 5                                        $%'
    '%-------------------------------------------------------%'

    '**% Find. This function is used for reading operations depending on the type of folder that called it.
    '%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
    '%tipo de carpeta que la invoco.
    Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
        Dim caseAux As Object = New eRemoteDB.Execute
        Select Case nParentFolder
            Case 40 '- Grupos de un proveedor
                caseAux = insReaTab_prov_group((Parameters("nProvider").Valor))
        End Select
        Return caseAux
    End Function

    '**% insReaTab_prov_group. this function returns the groups of a provider
    '%insReaTab_prov_group. esta funcion retorna los Grupos de un proveedor
    Private Function insReaTab_prov_group(ByRef lintProvider As Object) As eRemoteDB.Execute
		
		insReaTab_prov_group = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.queDatClientPol'
		'+Definición de parámetros para stored procedure 'insudb.queDatClientPol'
		'**+ Information read on Novemeber 25,1999  02:52:20 p.m.
		'+Información leída el 25/11/1999 02:52:20 p.m.
		
		With insReaTab_prov_group
			.StoredProcedure = "queDatTab_prov_group"
			.Parameters.Add("nProvider", lintProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				.RCloseRec()
				'UPGRADE_NOTE: Object insReaTab_prov_group may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insReaTab_prov_group = Nothing
			End If
		End With
	End Function
End Class






