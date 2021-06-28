Option Strict Off
Option Explicit On
Public Class Cover_amend
	'%-------------------------------------------------------%'
	'% $Workfile:: Cover_amend.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:21p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'**% Find. This function is used to obtain the clauses.
	'%Find. Se utiliza esta funcion para obtener las clausulas
	Public Function Find(ByVal nParentFolder As Integer, ByVal Parameters As Properties) As eRemoteDB.Execute
		Dim mbojPolicy As Policy
		Dim eRemPolicy As eRemoteDB.Execute
		Dim sTyp_clause As Object
		mbojPolicy = New Policy
		eRemPolicy = mbojPolicy.Find(nParentFolder, Parameters)
        If Not eRemPolicy Is Nothing Then
            With eRemPolicy
                sTyp_clause = .FieldToClass("sTyp_module")
                Select Case sTyp_clause
                    Case "4", String.Empty
                        Find = insReaCover_Var(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, Parameters("nCertif").Valor, Parameters("dEffecdate").Valor)
                    Case "2"
                        Find = insReaCover_co_p_Var(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, Parameters("dEffecdate").Valor)
                    Case "3"
                        Find = insReaCover_co_g_Var(Parameters("sCertype").Valor, Parameters("nBranch").Valor, Parameters("nProduct").Valor, Parameters("nPolicy").Valor, Parameters("dEffecdate").Valor)
                    Case Else
                        'UPGRADE_NOTE: Object Find may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        Find = Nothing
                End Select

                .RCloseRec()
            End With
        End If
        Return eRemPolicy
        'UPGRADE_NOTE: Object mbojPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mbojPolicy = Nothing
		'UPGRADE_NOTE: Object eRemPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		eRemPolicy = Nothing
	End Function
	'**% Lee el cursos que retorna los cambios realizados en la tabla
	'**% Cover a partir de una fecha
    Function insReaCover_Var(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Integer, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As eRemoteDB.Execute
        insReaCover_Var = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.QUEDATREACOVER_VAR'
        '+Definición de parámetros para stored procedure 'insudb.QUEDATREACOVER_VAR'
        With insReaCover_Var
            .StoredProcedure = "QUEDATREACOVER_VAR"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                'UPGRADE_NOTE: Object insReaCover_Var may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCover_Var = Nothing
            End If
        End With
    End Function
	
	'**% Lee el cursos que retorna los cambios realizados en la tabla
	'**% Cover_co_p a partir de una fecha
    Function insReaCover_co_p_Var(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As eRemoteDB.Execute
        insReaCover_co_p_Var = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.QUEDATREACOVER_VAR'
        '+Definición de parámetros para stored procedure 'insudb.QUEDATREACOVER_VAR'
        With insReaCover_co_p_Var
            .StoredProcedure = "QUEDATREACOVER_CO_P_VAR"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaCover_co_p_Var may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCover_co_p_Var = Nothing
            End If
        End With
    End Function
	
	'**% Lee el cursos que retorna los cambios realizados en la tabla
	'**% Cover_co_g a partir de una fecha
    Function insReaCover_co_g_Var(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As eRemoteDB.Execute
        insReaCover_co_g_Var = New eRemoteDB.Execute

        '**+ Parameter definition for stored procedure 'insudb.QUEDATREACOVER_VAR'
        '+Definición de parámetros para stored procedure 'insudb.QUEDATREACOVER_VAR'
        With insReaCover_co_g_Var
            .StoredProcedure = "QUEDATREACOVER_CO_G_VAR"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If Not .Run Then
                .RCloseRec()
                'UPGRADE_NOTE: Object insReaCover_co_g_Var may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                insReaCover_co_g_Var = Nothing
            End If
        End With
    End Function
End Class






