Option Strict Off
Option Explicit On
Public Class Situation
	'+
	'+ Estructura de tabla Situation al 08-30-2002 18:14:15
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public sCertype As String ' CHAR       1    0     0    N
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nPolicy As Double ' NUMBER     22   0     10   N
	Public nSituation As Integer ' NUMBER     22   0     5    N
	Public sClient As String ' CHAR       14   0     0    S
	Public dCompdate As Date ' DATE       7    0     0    S
	Public sDescript As String ' CHAR       30   0     0    S
    Public nUsercode As Integer ' NUMBER     22   0     5    S
    Public ncod_Agree As Integer
	
	'**- Variable that contains the client's name.
	'- Variable que contiene el nombre del cliente.
	Public sCliename As String
	Public lblnSituation As Boolean
	
	Public nActions As String
	
	Public nAFP As Integer
	
	'**% FindCLieSituation: the client's particualr name
	'% Encuentra la situación particular del cliente.
	Public Function FindClieSituation(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSituation As Integer) As Boolean
		Dim lrecreaSituation_v2 As eRemoteDB.Execute
		lrecreaSituation_v2 = New eRemoteDB.Execute
		
		On Error GoTo FindClieName_Err
		
		'**+ Parameter definition for stored procedure 'insudb.reaSituation_v2'
		'+Definición de parámetros para stored procedure 'insudb.reaSituation_v2'
		'**+ Information read on November 10, 2000  01:18:20 p.m.
		'+Información leída el 10/11/2000 01:18:20 p.m.
		
		FindClieSituation = True
		With lrecreaSituation_v2
			.StoredProcedure = "reaSituation_v2"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				sClient = .FieldToClass("sClient")
				sCliename = .FieldToClass("sCliename")
				Me.nAFP = .FieldToClass("nAFP")
			Else
				FindClieSituation = False
			End If
		End With
		
FindClieName_Err: 
		If Err.Number Then
			FindClieSituation = False
		End If
		
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaSituation_v2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSituation_v2 = Nothing
	End Function
	
	'**% Delete: deletes the records correspondent to the Situation table.
	'% Delete: Elimina los registros correspondientes a la tabla situation
	Public Function Delete(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecdelSituation As eRemoteDB.Execute
		
		lrecdelSituation = New eRemoteDB.Execute
		
		On Error GoTo Delete_err
		'**+ Parameter definition for stored procedure 'insudb.delSituation'
		'+Definición de parámetros para stored procedure 'insudb.delSituation'
		'**+ Information read on November 13, 2000  10:38:48 a.m.
		'+Información leída el 13/11/2000 10:38:48 a.m.
		
		With lrecdelSituation
			.StoredProcedure = "delSituation"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelSituation = Nothing
		
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'**% FindSituationData: Restores the data for a given situation.
	'% FindSituationData: Devuelve los datos para una situación dada.
    Public Function FindSituationData(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSituation As Integer,
                                      Optional ByVal lblnFind As Boolean = False) As Boolean

        '**- Declare variable that determines the result of the function (true/false)
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '**- Variable definition lrecreaSituation_v
        '- Se define la variable lrecreaSituation_v

        Dim lrecreaSituation_v As eRemoteDB.Execute
        lrecreaSituation_v = New eRemoteDB.Execute

        If Me.sCertype <> sCertype Or Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nPolicy <> nPolicy Or Me.nSituation <> nSituation Or lblnFind Then
            With Me
                .sCertype = sCertype
                .nBranch = nBranch
                .nProduct = nProduct
                .nPolicy = nPolicy
                .nSituation = nSituation
            End With
            '**+ Parameter definition for stored procedure 'insudb.reaSituation_v'
            '+ Definición de parámetros para stored procedure 'insudb.reaSituation_v'
            '**+ Information read on November 22,2000  9:28:45
            '+ Información leída el 22/11/2000 9:28:45

            With lrecreaSituation_v
                .StoredProcedure = "reaSituation_v"
                .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    Me.sClient = .FieldToClass("sClient")
                    Me.sDescript = .FieldToClass("sDescript")
                    Me.ncod_Agree = .FieldToClass("nCod_Agree")
                    .RCloseRec()
                    lblnRead = True
                Else
                    lblnRead = False
                End If
            End With
        End If

        FindSituationData = lblnRead
        'UPGRADE_NOTE: Object lrecreaSituation_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaSituation_v = Nothing
    End Function
	
	'**% insReaSituation: read the situation of the risk in colective.
	'%insReaSituation: Realiza la lectura de la situación del riesgo en colectivos.
	Public Function insReaSituation(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaSituation_a As eRemoteDB.Execute
		On Error GoTo insReaSituation_Err
		lrecreaSituation_a = New eRemoteDB.Execute
		insReaSituation = False
		lblnSituation = False
		
		With lrecreaSituation_a
			.StoredProcedure = "reaSituation_a"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				insReaSituation = True
				lblnSituation = True
				.RCloseRec()
			End If
		End With
insReaSituation_Err: 
		If Err.Number Then
			insReaSituation = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaSituation_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSituation_a = Nothing
	End Function
	
	'% Delete: It eliminates an precise registry in screen CA008 (Situations of risk)
	'% Delete: Elimina un registro puntual en la pantalla CA008 (Situaciones de riesgo)
    Public Function DeleteSituation(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double,
                                    ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSituation As Integer, ByVal nCod_agree As Integer) As Boolean
        Dim lrecdelSituation As eRemoteDB.Execute
        Dim lclsPolicyWin As Policy_Win
        Dim lcolSituation As Situations

        lrecdelSituation = New eRemoteDB.Execute

        On Error GoTo Delete_err
        '**+ Parameter definition for stored procedure 'insudb.delSituation'
        '+Definición de parámetros para stored procedure 'insudb.delSituation'
        '**+ Information read on November 13, 2000  10:38:48 a.m.
        '+Información leída el 13/11/2000 10:38:48 a.m.

        With lrecdelSituation
            .StoredProcedure = "delSituationCA008"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                DeleteSituation = True
                lcolSituation = New Situations
                If Not lcolSituation.Find(sCertype, nBranch, nProduct, nPolicy) Then
                    lclsPolicyWin = New Policy_Win
                    lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA008", "1")
                End If
            End If

        End With

Delete_err:
        If Err.Number Then
            DeleteSituation = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecdelSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelSituation = Nothing
        'UPGRADE_NOTE: Object lcolSituation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolSituation = Nothing
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
    End Function
	
	'%Add. This metodo is in charge to make the insertion of the corresponding data for the table
	'%"Situation". Giving back true or false depending on the existence or not on the data
	'%Add. Este metodo se encarga de realizar la insercion de los datos correspondientes para la
	'%tabla "Situation". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
    Public Function Add(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSituation As Integer,
                        ByVal sClient As String, ByVal sDescript As String, ByVal nUsercode As Integer, ByVal nCod_Agree As Integer) As Boolean
        Dim lrecreaSituationCA008 As eRemoteDB.Execute

        On Error GoTo Add_err
        lrecreaSituationCA008 = New eRemoteDB.Execute

        With lrecreaSituationCA008
            .StoredProcedure = "creSituationCA008"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then
            Add = False
        End If
        'UPGRADE_NOTE: Object lrecreaSituationCA008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaSituationCA008 = Nothing
        On Error GoTo 0
    End Function
	
	
	'%Find. This metodo is in charge to make the search of the corresponding data for the table
	'%"Situation". Giving back true or false depending on the existence or not on the data
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "Situation". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lrecreaSituationCA008 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		lrecreaSituationCA008 = New eRemoteDB.Execute
		
		Find = False
		lblnSituation = False
		
		With lrecreaSituationCA008
			.StoredProcedure = "reaSituationCA008"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				lblnSituation = True
				Me.sCertype = sCertype
				Me.nBranch = nBranch
				Me.nProduct = nProduct
				Me.nPolicy = nPolicy
				nSituation = .FieldToClass("nSituation")
				sClient = .FieldToClass("sClient")
                sDescript = .FieldToClass("sDescript")
                ncod_Agree = .FieldToClass("nCod_Agree")
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaSituationCA008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaSituationCA008 = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'%FindCertificatCA008 : This Find is used to verify that the situation to eliminate does not
	'%have certificates associated to the situation of irrigation in processing
	'%FindCertificatCA008 : Este Find es usado para verificar que la situación a eliminar no tenga
	'%certificados asociados a la situación de riego en tratamiento
	Public Function FindCertificatCA008(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSituation As Integer) As Boolean
		Dim lrecreaCertificatCA033 As eRemoteDB.Execute
		
		'+Se valida que si existe algún certificado asociado a la situacion de riesgo, el
		'sistema no debe permitir que se elimine.
		lrecreaCertificatCA033 = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaCertificatCA033'
		'Información leída el 31/01/2001 01:24:35 PM
		With lrecreaCertificatCA033
			.StoredProcedure = "reaCertificatCA033"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
			.Parameters.Add("nCertif", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindCertificatCA008 = True
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaCertificatCA033 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaCertificatCA033 = Nothing
		
	End Function
	
	'%insPostCA008: This function is in charge To execute the necessary actions of
	'%window CA008, depending on the process in execution (To add, To modify, to eliminate).
	'%insPostCA008: Esta función se encarga de Ejecutar las acciones necesarias de
	'%la ventana CA008, dependiendo del proceso en ejecución (Agregar, Modificar, eliminar).
    Public Function insPostCA008(ByVal sActions As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double,
                                 ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nSituation As Integer, ByVal sClient As String,
                                 ByVal sDescription As String, ByVal nCod_Agree As Integer) As Boolean
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lclsClient As eClient.Client        

        lclsClient = New eClient.Client

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nSituation = nSituation
            .sClient = lclsClient.ExpandCode(sClient)
            .sDescript = sDescription
            .nUsercode = nUsercode
            .ncod_Agree = IIf(nCod_Agree = eRemoteDB.Constants.intNull, 0, nCod_Agree)

            Select Case UCase(sActions)
                Case "ADD"
                    .nActions = CStr(1)
                    insPostCA008 = .Add(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy, Me.nSituation, Me.sClient, sDescription, Me.nUsercode, Me.ncod_Agree)
                    If insPostCA008 Then
                        lclsPolicyWin = New Policy_Win
                        lclsPolicyWin.Add_PolicyWin(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy, nCertif, dEffecdate, Me.nUsercode, "CA008", "2")
                        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                        lclsPolicyWin = Nothing
                    End If
                Case "UPDATE"
                    .nActions = CStr(2)
                    insPostCA008 = .Update(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy, Me.nSituation, Me.ncod_Agree)
                Case "DEL"
                    .nActions = CStr(3)
                    insPostCA008 = .Delete(Me.sCertype, Me.nBranch, Me.nProduct, Me.nPolicy)
            End Select
        End With

        'UPGRADE_NOTE: Object lclsClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsClient = Nothing
    End Function
	
	'%insValCA008:  Validate the transaction CA008 (Situation of risk)
	'%insValCA008:  Esta funcion Valida el la transacción CA008 (Situacion de riesgo)
    Public Function insValCA008(ByVal sActions As String, ByVal sCodispl As String, ByVal sCertype As String,
                                ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double,
                                ByVal nSituation As Integer, ByVal sDescription As String, ByVal sPolicyHolder As String,
                                ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCod_Agree As Integer) As String
        Dim lcolSituations As Situations
        Dim lclsErrors As eFunctions.Errors
        Dim lcolPolicy As ePolicy.Policy
        Dim lcolCertif As ePolicy.Certificat

        lclsErrors = New eFunctions.Errors

        insValCA008 = String.Empty

        '+ Se valida si el codigo de la situación no esta lleno
        If nSituation <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 1084)
        Else
            '+ Se valida si el campo de situación esta lleno,
            '+ el campo de la descripción tambien lo debe de estar
            If Trim(sDescription) = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 3300)
            End If
            '+ Se valida que el titular del recibo este lleno
            If Trim(sPolicyHolder) = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 70007)
            End If
        End If

        '+ Se valida que el codigo de la situación no se encuentre registrado
        If UCase(sActions) = "ADD" Then
            If FindSituationData(sCertype, nBranch, nProduct, nPolicy, nSituation) Then
                Call lclsErrors.ErrorMessage(sCodispl, 3299)
            End If
        End If


        '+ Se valida contenga por lo menos una situación agregada al momento de aceptar la pantalla
        lcolSituations = New Situations

        If lcolSituations.Find(sCertype, nBranch, nProduct, nPolicy) Then
            If lcolSituations.Count <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 1924)
            End If
        End If

        '+ Se valida que la ventana de "Información general de colectivos" tenga registros activos
        lcolPolicy = New ePolicy.Policy

        If Not lcolPolicy.Find(sCertype, nBranch, nProduct, nPolicy) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3894)
        Else
            lcolCertif = New ePolicy.Certificat
            Call lcolCertif.Find(sCertype, nBranch, nProduct, nPolicy, 0)

            '+ si la poliza es por situacion de riesgo y la via de pago es convenio.
            If lcolCertif.nWay_pay = 3 And lcolPolicy.sColinvot = "3" Then

                '+se valida que debe ingresar el convenio.
                If nCod_Agree <= 0 Then
                    Call lclsErrors.ErrorMessage("CA002", 60117)
                End If

            End If
            lcolCertif = Nothing

            '+ se valida que no exista una situacion de riesgo para distintos convenios
            If valExistsSituationAgree(sCertype, nBranch, nProduct, nPolicy, nCod_Agree) Then
                Call lclsErrors.ErrorMessage("CA002", 9)
            End If

            ' si es por situacion de riesgo y tiene mas de un convenio debe indicar situacion por convenio
            'If lcolPolicy.sColinvot = "3" And nCod_Agree <= 0 Then
            '    Dim lcolAgreement_pol As Agreement_pols
            '    lcolAgreement_pol = New Agreement_pols

            '    Call lcolAgreement_pol.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
            '    If lcolAgreement_pol.Count > 1 Then
            '        Call lclsErrors.ErrorMessage("CA002", 8)
            '    End If
            'End If

        End If

        insValCA008 = lclsErrors.Confirm

        'UPGRADE_NOTE: Object lcolPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolPolicy = Nothing
        'UPGRADE_NOTE: Object lcolSituations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolSituations = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function
	
	'%insValCA008:  Validate the transaction CA008 (Situation of risk)
	'%insValCA008:  Esta funcion Valida la transacción CA008 (Situacion de riesgo)
	Public Function insValCA008_K(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As String
		Dim lcolSituations As Situations
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		insValCA008_K = String.Empty
		
		
		'+ Se valida contenga por lo menos una situación agregada al momento de aceptar la pantalla
		lcolSituations = New Situations
		
		If Not lcolSituations.Find(sCertype, nBranch, nProduct, nPolicy) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1924)
		End If
		
		insValCA008_K = lclsErrors.Confirm
		
		
		'UPGRADE_NOTE: Object lcolSituations may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolSituations = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%Update. This metodo is in charge to make to update of the corresponding data for the table
	'%"Situation". Giving back true or false depending on the existence or not on the data
	'%Update. Este metodo se encarga de realizar actualizar de los datos correspondientes para la
	'%tabla "Situation". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
    Public Function Update(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSituation As Integer, ByVal nCod_agree As Integer) As Boolean
        Dim lreupdSituationCA008 As eRemoteDB.Execute

        On Error GoTo Update_Err
        lreupdSituationCA008 = New eRemoteDB.Execute

        With lreupdSituationCA008
            .StoredProcedure = "updSituationCA008"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCod_Agree", nCod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        'UPGRADE_NOTE: Object lreupdSituationCA008 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreupdSituationCA008 = Nothing
        On Error GoTo 0
    End Function
	
	'% valExistsSituation: Valida si existen grupos asociados a una póliza
	Public Function valExistsSituation(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nSituation As Integer) As Boolean
		Dim lrecreaGroups_a As eRemoteDB.Execute
		Dim lintExists As Integer
		
		On Error GoTo valExistsSituation_Err
		
		lrecreaGroups_a = New eRemoteDB.Execute
		
		With lrecreaGroups_a
			.StoredProcedure = "valExistsSituation"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSituation", nSituation, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsSituation = (.Parameters("nExists").Value = 1)
		End With
		
valExistsSituation_Err: 
		If Err.Number Then
			valExistsSituation = False
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lrecreaGroups_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGroups_a = Nothing
    End Function
    '% valExistsSituation: Valida si existen grupos asociados a una póliza
    Public Function valExistsSituationAgree(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal ncod_agree As Integer) As Boolean
        Dim lrecreaGroups_a As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valExistsSituation_Err

        lrecreaGroups_a = New eRemoteDB.Execute

        With lrecreaGroups_a
            .StoredProcedure = "valExistsSituation_Agree"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("ncod_agree", ncod_agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            valExistsSituationAgree = (.Parameters("nExists").Value = 1)
        End With

valExistsSituation_Err:
        If Err.Number Then
            valExistsSituationAgree = False
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lrecreaGroups_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaGroups_a = Nothing
    End Function
End Class






