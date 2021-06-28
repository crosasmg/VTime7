Option Strict Off
Option Explicit On
Friend Class Relations
	'%-------------------------------------------------------%'
	'% $Workfile:: Relations.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 4                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used for reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		
		Find = New eRemoteDB.Execute
		
		'**+ Parameter definition for stored procedure 'insudb.reaRelations'
		'+Definici�n de par�metros para stored procedure 'insudb.reaRelations'
		'**+ Information read on Novemeber 24,1999  11:48:14 a.m.
		'+Informaci�n le�da el 24/11/1999 11:48:14 a.m.
		
		With Find
			.StoredProcedure = "queDatRelations"
			.Parameters.Add("sClient", Parameters("sClient").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
			End If
		End With
	End Function
End Class






