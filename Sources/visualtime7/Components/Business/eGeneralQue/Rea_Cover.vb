Option Strict Off
Option Explicit On
Public Class Rea_Cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Rea_Cover.cls                            $%'
	'% $Author:: Pgarin                                     $%'
	'% $Date:: 5/04/06 16:14                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is for reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		
		Select Case nParentFolder
			Case 13 'Contratos de la c?a
				Find = insRea_Cover((Parameters("nCompany").Valor), (Parameters("dEffecdate").Valor))
				
			Case 0 'Contratos de la c?a
				Find = insRea_Cover((Parameters("nCompany").Valor), (Parameters("dEffecdate").Valor))
				
			Case Else
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
		End Select
	End Function
	
	'**%insReaPart_contr. This function restores the participant companies of a reinsurance company.
	'%insReaPart_contr. Esta funcion devuelve las compa?ias participantes de un contrato de
	'%reaseguro.
	Private Function insRea_Cover(ByRef lintCompany As Integer, ByRef ldtmEffecdate As Object) As eRemoteDB.Execute
		
		insRea_Cover = New eRemoteDB.Execute
		
		'**+Parameter definition for stored procedure 'insudb.queDatPart_contr'
		'+Definici?n de par?metros para stored procedure 'insudb.queDatPart_contr'
		'**+ Information read on January 05,2000 03:32:25 p.m.
		'+Informaci?n le?da el 05/01/2000 03:32:25 p.m.
		
		With insRea_Cover
			.StoredProcedure = "QUECOVERCIA"
			.Parameters.Add("nCompany", lintCompany, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object insRea_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				insRea_Cover = Nothing
			End If
		End With
		
		
	End Function
End Class






