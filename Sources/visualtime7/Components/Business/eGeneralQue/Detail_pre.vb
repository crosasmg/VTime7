Option Strict Off
Option Explicit On
Friend Class Detail_pre
	'%-------------------------------------------------------%'
	'% $Workfile:: Detail_pre.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**% Find: this function is used to obtain the links and load the to the properties collection.
	'%Find. Se utiliza esta funcion para obtener los nexos y cargarlo a la colección
	'%de propiedades
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		
		Find = New eRemoteDB.Execute
		
		'**+ Parameters definition for stored procedure 'insudb.queDatDetail_pre
		'+Definición de parámetros para stored procedure 'insudb.queDatDetail_pre'
		'**+ Information read on November 29,1999 06:44:26 p.m.
		'+Información leída el 29/11/1999 06:44:26 p.m.
		
		With Find
			.StoredProcedure = "queDatDetail_pre"
			.Parameters.Add("sCertype", Parameters("sCertype").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", Parameters("nBranch").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", Parameters("nProduct").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", Parameters("nReceipt").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDigit", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPayNumbe", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", Parameters("dEffecdate").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Not .Run Then
				'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				Find = Nothing
			End If
		End With
	End Function
End Class






