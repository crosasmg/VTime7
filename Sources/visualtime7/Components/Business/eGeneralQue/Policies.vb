Option Strict Off
Option Explicit On
Friend Class Policies
	'%-------------------------------------------------------%'
	'% $Workfile:: Policies.cls                             $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'- Variable para guardar el ramo tecnico
	Public sBrancht As String
	Public nUsercode As Integer
	
	'**% Find. This function is used for reading operations depending on the type of folder that called it.
	'%Find. Se utiliza esta funcion para realizar las operaciones de lectura dependiendo del
	'%tipo de carpeta que la invoco.
	Public Function Find(ByRef nParentFolder As Integer, ByRef Parameters As Properties) As eRemoteDB.Execute
		Dim lstrCertype As String
		Dim lobjPolicy As Policy
		Find = New eRemoteDB.Execute
		Select Case nParentFolder
			Case 4
				If Parameters("nCurrentFolder").Valor = 1 Then
					lstrCertype = "2"
				ElseIf Parameters("nCurrentFolder").Valor = 11 Then 
					lstrCertype = "3"
				Else
					lstrCertype = "1"
				End If
				'**+ Parameter definition for stored procedure 'insudb.queDatPoliciesCli'
				'+Definición de parámetros para stored procedure 'insudb.queDatPoliciesCli'
				'** Information read on November 25,1999  09:16:57 a.m.
				'+Información leída el 25/11/1999 09:16:57 a.m.
				
				With Find
					.StoredProcedure = "queDatPoliciesCli"
					.Parameters.Add("sCertype", lstrCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("sClient", Parameters("HsClient").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					
					If Not .Run Then
						
						'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						Find = Nothing
					End If
				End With
				'-Documentos de Cotización
			Case 15
				
				'**+ Parameter definition for stored procedure 'insudb.queDatPolicyDoc'
				'+Definición de parámetros para stored procedure 'insudb.queDatPolicyDoc'
				'**+ Information read on December 07,1999 10:11:25 a.m.
				'+Información leída el 07/12/1999 10:11:25 a.m.
				
				With Find
					.StoredProcedure = "queDatPolicyDoc"
					.Parameters.Add("sDocument", Parameters("sDocument").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 15, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run Then
						'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						Find = Nothing
					End If
				End With
			Case 1, 5, 11
				'+ Si desde la carpeta de pólizas se habre la carpeta de propuestas asociadas
				If nParentFolder = 1 And Parameters("nCurrentFolder").Valor = 5 Then
					With Find
						.StoredProcedure = "queDatPropbyPolicy"
						.Parameters.Add("sCertype", Parameters("sCertype").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nBranch", Parameters("nBranch").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nProduct", Parameters("nProduct").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nPolicy", Parameters("nPolicy").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						.Parameters.Add("nCertif", IIf(nParentFolder = 1, 0, Parameters("HnCertif").Valor), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
						If Not .Run Then
							'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							Find = Nothing
						End If
					End With
				Else
					lobjPolicy = New Policy
					Find = lobjPolicy.Find(nParentFolder, Parameters)
				End If
				'+ Prestamos
			Case 60
				With Find
					.StoredProcedure = "queDatPolicy"
					.Parameters.Add("sCertype", Parameters("sCertype").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nBranch", Parameters("nBranch").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nProduct", Parameters("nProduct").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nPolicy", Parameters("nPolicy").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("nCertif", IIf(nParentFolder = 1, 0, Parameters("HnCertif").Valor), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Parameters.Add("dEffecdate", Parameters("HdEffecdate").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					If Not .Run Then
						'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						Find = Nothing
					End If
				End With
			Case Else
				If nParentFolder = 0 Then
					
					If Parameters("nCurrentQuery").Valor = GenFunct.eQueryType.qtOriginalPolicy Then
						
						'**+ Parameter definition for stored procedure 'insudb.queDatPolicyOrig'
						'+Definición de parámetros para stored procedure 'insudb.queDatPolicyOrig'
						'**+ Information read on Decemeber 13, 1999  11:38:24 a.m.
						'+Información leída el 13/12/1999 11:38:24 a.m.
						
						With Find
							.StoredProcedure = "queDatPolicyOrig"
							.Parameters.Add("sOriginalPolicy", Parameters("sOriginal").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							If Not .Run Then
								'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
								Find = Nothing
							End If
						End With
					Else
						
						'**+Parameter definiiton for stored procedure 'insudb.queDatPolicy'
						'+Definición de parámetros para stored procedure 'insudb.queDatPolicy'
						'**+ Information read on December 07,1999 10:16:33 a.m.
						'+Información leída el 07/12/1999 10:16:33 a.m.
						With Find
							.StoredProcedure = "queDatPolicy"
							.Parameters.Add("sCertype", Parameters("HsCertype").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nBranch", Parameters("HnBranch").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nProduct", Parameters("HnProduct").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("nPolicy", Parameters("HnPolicy").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("nCertif", IIf(nParentFolder = 1, 0, Parameters("HnCertif").Valor), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                            .Parameters.Add("sBrancht", sBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eFunctions.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							.Parameters.Add("dEffecdate", Parameters("HdEffecdate").Valor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
							If Not .Run Then
								'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
								Find = Nothing
							End If
						End With
					End If
				Else
					'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					Find = Nothing
				End If
		End Select
	End Function
End Class






