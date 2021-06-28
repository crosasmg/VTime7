Option Strict Off
Option Explicit On
Public Class Tab_ClaRevcond
	
	
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_ClaRevcond.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:34p                                $%'
	'% $Revision:: 6                                        $%'
	'%-------------------------------------------------------%'
	'         Column_name        Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource                Collation
	'---------------- ----- ----- ----------------------------------- ----------------------------------- ----------------------------------- --------------------------------------------------------------------------------------------------------------------------------
	Public nOper_type As Integer 'nOper_type     smallint    no  2   5   0   no  (n/a)   (n/a)   NULL
	Public nGen_opera As Integer 'dCompdate      datetime    no  8           no  (n/a)   (n/a)   NULL
	Public nInd_rev As Integer 'nGen_opera     smallint    no  2   5   0   yes (n/a)   (n/a)   NULL
	Public nPay_ind As Integer 'nInd_rev       smallint    no  2   5   0   yes (n/a)   (n/a)   NULL
	Public nRec_esp_in As Integer 'nPay_ind       smallint    no  2   5   0   yes (n/a)   (n/a)   NULL
	Public nRecover_in As Integer 'nRec_esp_in    smallint    no  2   5   0   yes (n/a)   (n/a)   NULL
	Public nReserve_in As Integer 'nRecover_in    smallint    no  2   5   0   yes (n/a)   (n/a)   NULL
	Public nUsercode As Integer 'nReserve_in    smallint    no  2   5   0   yes (n/a)   (n/a)   NULL
	Public nStatusInstance As Integer 'nUsercode      smallint    no  2   5   0   no  (n/a)   (n/a)   NULL
	Public sCodispl As String
	
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "tab_cl_ope". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Find(ByRef nOper_type As Integer) As Boolean
		Dim lrectab_cl_ope As New eRemoteDB.Execute
		On Error GoTo Find_Err
		lrectab_cl_ope = New eRemoteDB.Execute
		
		Find = False
		With lrectab_cl_ope
			.StoredProcedure = "reatab_cl_ope"
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Find = .Run
			
			If Find Then
				Me.nInd_rev = .FieldToClass("nInd_rev")
			End If
			
		End With
		
		lrectab_cl_ope = Nothing
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	Public Function Add() As Boolean
		Dim lreccreTab_cl_ope As New eRemoteDB.Execute
		lreccreTab_cl_ope = New eRemoteDB.Execute
		
		
		With lreccreTab_cl_ope
			'Definición de parámetros para stored procedure 'insudb.creTab_cl_ope'
			'Información leída el 20/09/2001 04:10:27 p.m.
			
			With lreccreTab_cl_ope
				.StoredProcedure = "creTab_cl_ope"
				.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nGen_opera", nGen_opera, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nInd_rev", nInd_rev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPay_ind", nPay_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRec_esp_in", nRec_esp_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nRecover_in", nRecover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nReserve_in", nReserve_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				Add = .Run(False)
			End With
			lreccreTab_cl_ope = Nothing
			
			
		End With
	End Function
	
	'%Update_ Actualiza la informacion de tab_cl_ope
	Public Function Update() As Boolean
		Dim lrectab_cl_ope As New eRemoteDB.Execute
		lrectab_cl_ope = New eRemoteDB.Execute
        'lrectab_cl_ope = Nothing
		With lrectab_cl_ope
			.StoredProcedure = "updTab_cl_ope"
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGen_opera", nGen_opera, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInd_rev", nInd_rev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPay_ind", nPay_ind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRec_esp_in", nRec_esp_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRecover_in", nRecover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReserve_in", nReserve_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
        End With
        lrectab_cl_ope = Nothing
	End Function
	'% Delete: Borra un registro de tab_cl_ope
	Public Function Delete() As Boolean
		Dim lrectab_cl_ope As New eRemoteDB.Execute
		lrectab_cl_ope = New eRemoteDB.Execute
        'lrectab_cl_ope = Nothing
		With lrectab_cl_ope
			.StoredProcedure = "delTab_cl_ope"
			.Parameters.Add("nOper_type", nOper_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
        End With
        lrectab_cl_ope = Nothing
	End Function
	
	'%insValMSI016_K: esta función se encarga de validar, masiva y puntualmente, los campos del grid
	Public Function insValMSI016_K(ByVal sCodispl As String, ByVal nOper_type As Integer, ByVal nGen_opera As Integer, ByVal nInd_rev As Integer, ByVal nPay_ind As Integer, ByVal nRec_esp_in As Integer, ByVal nRecover_in As Integer, ByVal nReserve_in As Integer, ByVal nUsercode As Integer, ByVal sAction As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		lclsErrors = New eFunctions.Errors
		
		On Error GoTo insValMSI016_K_Err
		
		If sAction = "Add" And Find(nOper_type) Then
			lclsErrors.ErrorMessage(sCodispl, 10935)
		Else
			If nOper_type > 0 Then
				If nInd_rev = 1 Then
					If nGen_opera <= 0 Then
						lclsErrors.ErrorMessage(sCodispl, 10223)
					End If
					If nReserve_in <= 0 Then
						lclsErrors.ErrorMessage(sCodispl, 10226)
					End If
					If nPay_ind <= 0 Then
						lclsErrors.ErrorMessage(sCodispl, 10876)
					End If
					If nRec_esp_in <= 0 Then
						lclsErrors.ErrorMessage(sCodispl, 10877)
					End If
					If nRecover_in <= 0 Then
						lclsErrors.ErrorMessage(sCodispl, 10878)
					End If
				End If
			Else
				lclsErrors.ErrorMessage(sCodispl, 55742)
			End If
		End If
		insValMSI016_K = lclsErrors.Confirm
		
insValMSI016_K_Err: 
		If Err.Number Then
			insValMSI016_K = insValMSI016_K & Err.Description
		End If
		On Error GoTo 0
		
		lclsErrors = Nothing
	End Function
	
	
	'% insPostMSI016_K: Crea/actualiza los registros correspondientes en la tabla de tab_cl_ope
	Public Function insPostMSI016_K(ByVal sCodispl As String, ByVal sAction As String, ByVal nOper_type As Integer, ByVal nGen_opera As Integer, ByVal nInd_rev As Integer, ByVal nPay_ind As Integer, ByVal nRec_esp_in As Integer, ByVal nRecover_in As Integer, ByVal nReserve_in As Integer, ByVal nUsercode As Integer) As Boolean
		
		
		On Error GoTo insPostMSI016_K_Err
		With Me
			.sCodispl = sCodispl
			.nOper_type = IIf(nOper_type = 0, eRemoteDB.Constants.intNull, nOper_type)
			.nGen_opera = IIf(nGen_opera = 0, eRemoteDB.Constants.intNull, nGen_opera)
			.nInd_rev = nInd_rev
			.nPay_ind = IIf(nPay_ind = 0, eRemoteDB.Constants.intNull, nPay_ind)
			.nRec_esp_in = IIf(nRec_esp_in = 0, eRemoteDB.Constants.intNull, nRec_esp_in)
			.nRecover_in = IIf(nRecover_in = 0, eRemoteDB.Constants.intNull, nRecover_in)
			.nReserve_in = IIf(nReserve_in = 0, eRemoteDB.Constants.intNull, nReserve_in)
			.nUsercode = nUsercode
		End With
		
		
		
		If (sAction = "Add") Or Not Find(nOper_type) Then
			insPostMSI016_K = Add
		Else
			insPostMSI016_K = Update
		End If
		
insPostMSI016_K_Err: 
		If Err.Number Then
			insPostMSI016_K = False
		End If
		On Error GoTo 0
	End Function
End Class






