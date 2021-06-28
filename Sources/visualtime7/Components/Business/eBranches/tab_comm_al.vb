Option Strict Off
Option Explicit On
Option Compare Text
Public Class tab_comm_al
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_comm_al.cls                          $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 33                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Descripción de la tabla tab_comm_al (Tabla de comisiones de VidActiva),
	'+ al 12/10/2001
	'
	'      Property     Type        DataType    PK   NULLABLE
	'--------------------------------------------------------
	Public nComtabli As Integer 'Number(5)    1   NO
	Public nIntertyp As Integer 'Number(5)    2   NO
	Public nSellChannel As Integer 'Number(5)    3   NO
	Public nWay_pay As Integer 'Number(5)    4   NO
	Public nBranch As Integer 'Number(5)    5   NO
	Public nProduct As Integer 'Number(5)    6   NO
	Public nModulec As Integer 'Number(5)    7   NO
	Public nCover As Integer 'Number(5)    8   NO
	Public nAgreement As Integer 'Number(5)    9   NO
	Public nQPB As Integer 'Number(5)   10   NO
	Public dEffecdate As Date 'Date        11   NO
	Public nPercent As Double 'Number(5,2)      NO
	Public nAmount As Double 'Number(10,2)     NO
	Public nCurrency As Integer 'Number(5)        NO
	Public nUsercode As Integer 'Number(5)        NO
	'**% Delete: Deletes an Active Life commision record
	'% Delete: Elimina un registro de comision para vida activa
	'%-------------------------------------------------------------'
	Public Function Delete() As Boolean
		'%-------------------------------------------------------------
		On Error GoTo Delete_Err
		
		Delete = False
		
		Dim lrecinsDeltab_comm_al As eRemoteDB.Execute
		
		lrecinsDeltab_comm_al = New eRemoteDB.Execute
		
		With lrecinsDeltab_comm_al
			.StoredProcedure = "insDelTab_comm_al"
			With .Parameters
				.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nSellChannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nWay_Pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nQPB", nQPB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nUserCode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			Delete = .Run(False)
			
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsDeltab_comm_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDeltab_comm_al = Nothing
	End Function
	
	'**% Update: Updates an Active Life commision record
	'% Update: Actualiza un registro de comision para vida activa
	'%-------------------------------------------------------------
	Public Function Update() As Boolean
		'%-------------------------------------------------------------
		On Error GoTo Update_Err
		
		Update = False
		
		Dim lrecinsUpdtab_comm_al As eRemoteDB.Execute
		
		lrecinsUpdtab_comm_al = New eRemoteDB.Execute
		
		With lrecinsUpdtab_comm_al
			.StoredProcedure = "insUpdTab_comm_al"
			With .Parameters
				.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nSellChannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nWay_Pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nQPB", nQPB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			
			Update = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsUpdtab_comm_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsUpdtab_comm_al = Nothing
	End Function
	'**%Funcion insValAgrement_al: Validates the existence
	'**% of an agreement for LifeActive
	'%Funcion insValAgrement_al. Valida la existencia
	'% de un convenio de VidActiva
	'%---------------------------------------------------
	Private Function insValAgrement_al(ByVal nAgreement As Integer) As Boolean
		'%---------------------------------------------------
		Dim lrecFindAgreement_al As Agreement_al
		
		lrecFindAgreement_al = New Agreement_al
		
		insValAgrement_al = lrecFindAgreement_al.Find(nAgreement)
		
insValAgrement_al_Err: 
		If Err.Number Then
			insValAgrement_al = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecFindAgreement_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecFindAgreement_al = Nothing
	End Function
	'**%Function Find: This function is in charge of obtaining the data for a
	'**% specific Active Life commiss
	'%Funcion Find. Esta funcion se encarga de obtener los datos de una
	'% comision especifica de VidActiva
	Public Function Find(ByVal nComtabli As Integer, ByVal nIntertyp As Integer, ByVal nSellChannel As Integer, ByVal nWay_pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nAgreement As Integer, ByVal nQPB As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecinsReaTab_comm_al_o As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = False
		
		lrecinsReaTab_comm_al_o = New eRemoteDB.Execute
		
		With lrecinsReaTab_comm_al_o
			.StoredProcedure = "reaTab_comm_al_o"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellChannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nQPB", nQPB, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run Then
				Find = True
				nPercent = .FieldToClass("nPercent")
				nAmount = .FieldToClass("nAmount")
				nCurrency = .FieldToClass("nCurrency")
				.RCloseRec()
			End If
		End With
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsReaTab_comm_al_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsReaTab_comm_al_o = Nothing
	End Function
	'**%Funcion insValEffecdate: Validates the existence of a commiss
	'**% linked to a module and intermediary for a previous date
	'%Funcion insValEffecdate. Valida la existencia de
	'% de una comision de VidActiva asociada a un modulo e intermediario
	'% para una fecha anterior a la dada
	'%---------------------------------------------------'
	Private Function InsValEffecdate(ByVal nIntertyp As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		'%---------------------------------------------------
		Dim lrecinsValEffecdate As eRemoteDB.Execute
		
		On Error GoTo InsValEffecdate_Err
		
		InsValEffecdate = True
		
		lrecinsValEffecdate = New eRemoteDB.Execute
		
		With lrecinsValEffecdate
			.StoredProcedure = "insValEffecdate_Tab_comm_al"
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			'+ no pasa la validación si existen registros con fecha de
			'+ efecto mas reciente a la que se pretende editar
			InsValEffecdate = Not .Run
			.RCloseRec()
		End With
		
InsValEffecdate_Err: 
		If Err.Number Then
			InsValEffecdate = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsValEffecdate may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValEffecdate = Nothing
	End Function
	
	'**%Funcion insValCover: This function validates a cover existence
	'%Funcion insValCover. Esta funcion valida la existencia de una cobertura
	'% asociada al producto
	'%---------------------------------------------------
	Private Function insValCover(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		'%---------------------------------------------------
		On Error GoTo insValCover_Err
		
		Dim lclsLife_Cover As eProduct.Life_cover
		
		lclsLife_Cover = New eProduct.Life_cover
		
		insValCover = lclsLife_Cover.Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
		
insValCover_Err: 
		If Err.Number Then
			insValCover = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsLife_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_Cover = Nothing
	End Function
	
	
	'**%Funcion insValModule: This function validates a module existence
	'**% for as given product
	'%Funcion insValModule. Esta funcion valida la existencia de un
	'% modulo asociado a un producto
	'%---------------------------------------------------'
	Private Function insValModule(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		'%---------------------------------------------------
		Dim lclsTab_modul As eProduct.Tab_modul
		
		On Error GoTo insValModule_Err
		
		lclsTab_modul = New eProduct.Tab_modul
		
		insValModule = lclsTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate)
		
insValModule_Err: 
		If Err.Number Then
			insValModule = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_modul = Nothing
	End Function
	
	'**% insValMVA645: Validates an Active Life commision record edition
	'% insValMVA645: Valida la modificacion de un registro de comision
	'% para vida activa
	'%-------------------------------------------------------------
	Public Function insValMVA645(ByVal sAction As String, ByVal nComtabli As Integer, ByVal nIntertyp As Integer, ByVal nSellChannel As Integer, ByVal nWay_pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nAgreement As Integer, ByVal nQPB As Integer, ByVal dEffecdate As Date, Optional ByVal nPercent As Double = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nCurrency As Integer = 0) As String
		'%-------------------------------------------------------------
		Dim lBoolPercent As Boolean
		Dim lBoolAmount As Boolean
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMVA645_err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			'+ La combinación de convenio y cantidad de primas básicas
			'+ no debe estar repetida para la misma tabla, tipo de intermediario,
            '+ canal de venta, vía de pago, ramo, producto, módulo y cobertura.  55858
			If sAction = "Add" And Me.Find(nComtabli, nIntertyp, nSellChannel, nWay_pay, nBranch, nProduct, nModulec, nCover, nAgreement, nQPB, dEffecdate) Then
                Call .ErrorMessage("MVA645", 55858)
			End If
			
			'+ Convenio Si el campo esta lleno y es diferente de cero,
			'+ debe estar registrado en el sistema  55567
            If nAgreement <> 0 And nAgreement <> eRemoteDB.Constants.intNull Then
                If Not insValAgrement_al(nAgreement) Then
                    Call .ErrorMessage("MVA645", 55567)
                End If
            Else
                '+ Convenio debe estar lleno
                Call .ErrorMessage("MVA645", 60117)
            End If

            '+ Cantidad de prima básica Si el campo convenio tiene valor,
            '+ debe estar lleno  55568
            If nQPB = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage("MVA645", 55568)
            End If

            '+ % Comisión Si el campo convenio tiene valor y el campo
            '+ "Comisión" no tiene valor, debe estar lleno  3029
            '+ Comisión Si el campo convenio tiene valor y el campo
            '+ "% Comisión" no tiene valor, debe estar lleno  3029

            lBoolPercent = nPercent <> eRemoteDB.Constants.intNull And nPercent <> 0
            lBoolAmount = nAmount <> eRemoteDB.Constants.intNull And nAmount <> 0

            If Not (lBoolPercent Xor lBoolAmount) Then
                Call .ErrorMessage("MVA645", 3029)
            End If


            insValMVA645 = .Confirm
        End With
		
insValMVA645_err: 
		If Err.Number Then
			insValMVA645 = insValMVA645 & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	'**% insPostMVA645: Updates DB with data of MVA645 transaction
	'% insPostMVA645: Realiza los procesos de actualización de la
	'% base de datos para la transaccion MVA645
	'%-------------------------------------------------------------'
	Public Function insPostMVA645(ByVal sAction As String, ByVal nComtabli As Integer, ByVal nIntertyp As Integer, ByVal nSellChannel As Integer, ByVal nWay_pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nAgreement As Integer, ByVal nQPB As Integer, ByVal dEffecdate As Date, Optional ByVal nPercent As Double = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nUsercode As Integer = 0) As Boolean
		'%-------------------------------------------------------------
		On Error GoTo insPostMVA645_Err
		
		insPostMVA645 = True
		
		
		With Me
			.nComtabli = nComtabli
			.nIntertyp = nIntertyp
			.nSellChannel = nSellChannel
			.nWay_pay = nWay_pay
			.nBranch = nBranch
			.nProduct = nProduct
			.nModulec = nModulec
			.nCover = nCover
			
			If nAgreement = eRemoteDB.Constants.intNull Then
				.nAgreement = 999
			Else
				.nAgreement = nAgreement
			End If
			
			.nQPB = nQPB
			.dEffecdate = dEffecdate
			.nPercent = nPercent
			.nAmount = nAmount
			.nUsercode = nUsercode
		End With
		
		Select Case sAction
			Case "Add", "Update"
				insPostMVA645 = Update
			Case "Del"
				insPostMVA645 = Delete
		End Select
		
insPostMVA645_Err: 
		If Err.Number Then
			insPostMVA645 = False
		End If
		On Error GoTo 0
	End Function
	
	'**% insValMVA645_K: Validates the key transaction of MVA645
	'% insValMVA645_K: Valida la transacción llave de MVA645
	'%-------------------------------------------------------------
	Public Function insValMVA645_K(ByVal nAction As Integer, ByVal nComtabli As Integer, ByVal nIntertyp As Integer, ByVal nSellChannel As Integer, ByVal nWay_pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nDuplicate As Integer, ByVal nComtabli_ori As Integer, ByVal nIntertyp_ori As Integer, ByVal nSellChannel_ori As Integer, ByVal nWay_pay_ori As Integer, ByVal nBranch_ori As Integer, ByVal nProduct_ori As Integer, ByVal nModulec_ori As Integer, ByVal nCover_ori As Integer) As String
		'%------------------------------------------------------------
		'- Objeto para mostrar errores
		Dim lclsErrors As eFunctions.Errors
		Dim lclsTab_commm_als As tab_comm_als
		
		On Error GoTo insValMVA645_K_err
		lclsErrors = New eFunctions.Errors
		With lclsErrors
			'+ Si se esta duplicando, se verifica que no exista datos para la llave que se quiere duplicar
			If nDuplicate = 1 Then
				lclsTab_commm_als = New tab_comm_als
				If lclsTab_commm_als.Find_Agreement(nComtabli, nIntertyp, nSellChannel, nWay_pay, nBranch, nProduct, nModulec, nCover, dEffecdate) Then
					Call .ErrorMessage("MVA645", 55858)
				End If
				If nComtabli = nComtabli_ori And nIntertyp = nIntertyp_ori And nSellChannel = nSellChannel_ori And nWay_pay = nWay_pay_ori And nBranch = nBranch_ori And nProduct = nProduct_ori And nModulec = nModulec_ori And nCover = nCover_ori Then
					Call .ErrorMessage("MVA645", 60474)
				End If
			End If
			
			'+ Tabla
			'+ Debe estar lleno  10048
			If nComtabli = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 10048)
			End If
			
			'+ Tipo de intermediario
			'+ Debe estar lleno  10095
			If nIntertyp = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 10095)
			End If
			
			'+ Canal de venta
			'+ Debe estar lleno  55583
			If nSellChannel = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 55583)
			End If
			
			'+ Vía de pago
			'+ Debe estar lleno  55008
			If nWay_pay = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 55008)
			End If
			
			'+ Ramo
			'+ Debe estar lleno  9064
			If nBranch = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 9064)
			End If
			
			'+ Producto
			'+ Debe estar lleno  11009
			If nProduct = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 11009)
			End If
			
			'+ Módulo
			'+ Debe estar lleno  1901
			If nModulec = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 1901)
			Else
				'+ Si esta lleno y el valor es diferente de cero,
				'+ debe estar registrado para el producto indicado.  55566
				If nModulec <> 0 Then
					If Not insValModule(nBranch, nProduct, nModulec, dEffecdate) Then
						Call .ErrorMessage("MVA645", 55566)
					End If
				End If
			End If
			
			'+ Cobertura Debe estar lleno  11163
			If nCover = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage("MVA645", 11163)
			Else
				'+ Debe estar registrado en la tabla de coberturas del producto  11165
				If Not insValCover(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
					Call .ErrorMessage("MVA645", 11165)
				End If
			End If
			
			'+ Fecha Debe estar llena  1103
			If dEffecdate = dtmNull Then
				Call .ErrorMessage("MVA645", 1103)
			Else
				'+ Si la fecha está llena y la acción es "actualizar",
				'+ no se puede realizar la modificación o eliminación
				'+ si existe un registro (para el tipo de intermediario,
				'+ ramo, producto y módulo indicado) con fecha anterior a la indicada en la pantalla.
				If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then ' update, delete
					If Not InsValEffecdate(nIntertyp, nBranch, nProduct, nModulec, dEffecdate) Then
						Call .ErrorMessage("MVA645", 1021)
					End If
				End If
			End If
			
			insValMVA645_K = .Confirm
		End With
		
insValMVA645_K_err: 
		If Err.Number Then
			insValMVA645_K = "insValMVA645_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsTab_commm_als may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_commm_als = Nothing
		On Error GoTo 0
	End Function
	'% insPostDuplicaMVA645: Realiza los procesos de duplicación de datos
	'% de la tabla TAB_COMM_AL de la transacción MVA645
	'%-------------------------------------------------------------'
	Public Function insPostDuplicaMVA645(ByVal nComtabli As Integer, ByVal nIntertyp As Integer, ByVal nSellChannel As Integer, ByVal nWay_pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal nComtabli_ori As Integer, ByVal nIntertyp_ori As Integer, ByVal nSellChannel_ori As Integer, ByVal nWay_pay_ori As Integer, ByVal nBranch_ori As Integer, ByVal nProduct_ori As Integer, ByVal nModulec_ori As Integer, ByVal nCover_ori As Integer, ByVal dEffecdate_ori As Date) As Boolean
		'%-------------------------------------------------------------
		Dim lrecinsDuptab_comm_al As eRemoteDB.Execute
		On Error GoTo insDuptab_comm_al_Err
		
		lrecinsDuptab_comm_al = New eRemoteDB.Execute
		
		'+ Definición de store procedure insDuptab_comm_al al 07-29-2002 11:47:39
		With lrecinsDuptab_comm_al
			.StoredProcedure = "insDuptab_comm_al"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp", nIntertyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellchannel", nSellChannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay", nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nComtabli_ori", nComtabli_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntertyp_ori", nIntertyp_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellchannel_ori", nSellChannel_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_pay_ori", nWay_pay_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_ori", nBranch_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct_ori", nProduct_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec_ori", nModulec_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_ori", nCover_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate_ori", dEffecdate_ori, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostDuplicaMVA645 = .Run(False)
		End With
		
insDuptab_comm_al_Err: 
		If Err.Number Then
			insPostDuplicaMVA645 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsDuptab_comm_al may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDuptab_comm_al = Nothing
	End Function
	
	'% Initialize: Se inicializan las variables publicas del modulo
	'%-------------------------------------------------------------'
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'%-------------------------------------------------------------'
		nComtabli = eRemoteDB.Constants.intNull
		nIntertyp = eRemoteDB.Constants.intNull
		nSellChannel = eRemoteDB.Constants.intNull
		nWay_pay = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nAgreement = eRemoteDB.Constants.intNull
		nQPB = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nPercent = eRemoteDB.Constants.intNull
		nAmount = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		nUsercode = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






