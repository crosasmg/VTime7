Option Strict Off
Option Explicit On
Public Class Det_comlif
	'%-------------------------------------------------------%'
	'% $Workfile:: Det_comlif.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 18                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Propiedades según la tabla en el sistema el 24/04/2001
	'+ El campo llave corresponde a nComtabli, nBranch, nProduct, nPolicy_dur, nMin_durat, dEffecdate.
	
	'+ Column_name         Type                 Length      Prec  Scale Nullable TrimTrailingBlanks FixedLenNullInSource
	'+ ------------------- -------------------- ----------- ----- ----- -------- ------------------ --------------------
	Public nComtabli As Integer 'smallint 2           5     0     no       (n/a)              (n/a)
	Public nBranch As Integer 'smallint 2           5     0     no       (n/a)              (n/a)
	Public nProduct As Integer 'smallint 2           5     0     no       (n/a)              (n/a)
	Public nPolicy_dur As Integer 'smallint 2           5     0     no       (n/a)              (n/a)
	Public nMin_durat As Integer 'smallint 2           5     0     no       (n/a)              (n/a)
	Public dEffecdate As Date 'datetime 8                       no       (n/a)              (n/a)
	Public nPercent As Double 'decimal  5           4     2     yes      (n/a)              (n/a)
	Public nUsercode As Integer 'smallint 2           5     0     yes      (n/a)              (n/a)
	Public dNulldate As Date 'datetime 8                       yes      (n/a)              (n/a)
	
	Public nModulec As Integer
	Public sDesc_Modulec As String
	Public nCover As Integer
	Public sDesc_Cover As String
	Public nCurrency As Integer
	Public sDesc_Currency As String
	Public nAmount As Double
	Public nWay_Pay As Integer
	Public sDesc_Way_Pay As String
	Public nSellchannel As Integer
	Public sDesc_Sellchannel As String
	Public nMax_durat As Integer
	Public sDesc_Branch As String
	
	Public nStatusInstance As Integer
	Public sProductDes As String
	
	Private lblnInquiry As Boolean
	Private lblnModify As Boolean
	
	
	'% Find: Busca la información de una tabla de comisiones de vida.
	Public Function Find(ByVal nComtabli As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy_dur As Integer, ByVal nMin_durat As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nWay_Pay As Integer, ByVal nSellchannel As Integer, ByVal dEffecdate As Date, Optional ByVal lblnFind As Boolean = False) As Boolean
		Dim lrecreaDet_comlif_v As eRemoteDB.Execute
		
		If nComtabli = Me.nComtabli And nBranch = Me.nBranch And nProduct = Me.nProduct And nModulec = Me.nModulec And nCover = Me.nCover And nWay_Pay = Me.nWay_Pay And nSellchannel = Me.nSellchannel And nPolicy_dur = Me.nPolicy_dur And nMin_durat = Me.nMin_durat And dEffecdate = Me.dEffecdate And Not lblnFind Then
			Find = True
		Else
			lrecreaDet_comlif_v = New eRemoteDB.Execute
			With lrecreaDet_comlif_v
                .StoredProcedure = "reaDet_comlif_v"
				.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nMin_durat", nMin_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPolicy_dur", nPolicy_dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nSellchannel", nSellchannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				If .Run Then
					Me.nComtabli = .FieldToClass("nComtabli")
					Me.nBranch = .FieldToClass("nBranch")
					Me.nProduct = .FieldToClass("nProduct")
					Me.nPolicy_dur = .FieldToClass("nPolicy_dur")
					Me.nMin_durat = .FieldToClass("nMin_Durat")
					Me.dEffecdate = .FieldToClass("dEffecdate")
					Me.nPercent = .FieldToClass("nPercent")
					Me.dNulldate = dtmNull '.FieldToClass("nNulldate")
					.RCloseRec()
					Find = True
				Else
					Find = False
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaDet_comlif_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaDet_comlif_v = Nothing
		End If
	End Function
	
	'%insDet_comlif: Esta función se encarga de agregar la información en tratamiento de la
	'%tabla principal para la transacción.
	Public Function Update() As Boolean
		Dim lrecinsDet_comlif As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsDet_comlif = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insDet_comlif'
		'Información leída el 24/04/2001 02:44:47 p.m.
		
		With lrecinsDet_comlif
			.StoredProcedure = "insDet_comlif"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_durat", nMin_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_dur", nPolicy_dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellchannel", nSellchannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Durat", nMax_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Update = .Run(False)
			
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecinsDet_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDet_comlif = Nothing
		
	End Function
	
	'% Del: Esta función se encarga de eliminar información en la tabla principal de la clase.
	Public Function Delete() As Boolean
		Dim lrecinsDelDet_comlif As eRemoteDB.Execute
		
		lrecinsDelDet_comlif = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.insDelDet_comlif'
		'Información leída el 24/04/2001 03:15:30 p.m.
		
		With lrecinsDelDet_comlif
			.StoredProcedure = "insDelDet_comlif"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_dur", nPolicy_dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_durat", nMin_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellchannel", nSellchannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			Delete = .Run(False)
			
		End With
		'UPGRADE_NOTE: Object lrecinsDelDet_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsDelDet_comlif = Nothing
		
	End Function
	
	
	'%Find_date(). Esta funcion se encarga de buscar la mayor de las
	'%fecha de efecto de los registros de una tabla de  comisiones.
	Public Function Find_date() As Boolean
		
		Dim lrecreaDet_comlif_date As eRemoteDB.Execute
		
		lrecreaDet_comlif_date = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaDet_comlif_date'
		'Información leída el 25/04/2001 09:38:52 a.m.
		
		With lrecreaDet_comlif_date
			.StoredProcedure = "reaDet_comlif_date"
			.Parameters.Add("nComtabli", Me.nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.dEffecdate = .FieldToClass("dEffecdate")
				.RCloseRec()
				Find_date = True
			Else
				Me.dEffecdate = CDate("01/01/1800")
				Find_date = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaDet_comlif_date may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaDet_comlif_date = Nothing
		
	End Function
	
	Private Function Find_range(ByVal nComtabli As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy_dur As Integer, ByVal nMin_durat As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nWay_Pay As Integer, ByVal nSellchannel As Integer, ByVal dEffecdate As Date, ByVal nMax_durat As Integer) As Boolean
		Dim lrecREADET_LIMCOF_RANGE As eRemoteDB.Execute
		
		On Error GoTo Find_range_Err
		
		lrecREADET_LIMCOF_RANGE = New eRemoteDB.Execute
		
		'**+ Parameter deifinition for stored procedure 'insudb.reaTab_comrat_range'
		'+Definición de parámetros para stored procedure 'insudb.reaTab_comrat_range'
		'**+ Information read on May 14, 2001 03:36:16 p.m.
		'+Información leída el 14/05/2001 03:36:18 p.m.
		
		With lrecREADET_LIMCOF_RANGE
			.StoredProcedure = "READET_COMLIF_RANGE"
			.Parameters.Add("nComtabli", nComtabli, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy_dur", nPolicy_dur, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMin_durat", nMin_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nWay_Pay", nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSellchannel", nSellchannel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMax_Durat", nMax_durat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(True) Then
				If .FieldToClass("nExist") = 1 Then
					Find_range = True
				End If
			End If
		End With
		
Find_range_Err: 
		If Err.Number Then
			Find_range = False
		End If
		
		'UPGRADE_NOTE: Object lrecREADET_LIMCOF_RANGE may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecREADET_LIMCOF_RANGE = Nothing
		On Error GoTo 0
		
	End Function
	
	'%insValMAG002_K: Esta función se encarga de validar los datos introducidos en la cabecera de la
	'%forma.
	Public Function insValMAG002_K(ByVal sCodispl As String, ByVal nAction As Integer, Optional ByVal nSeleted As Integer = 0, Optional ByVal nComtabli As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsTab_Comlif As Tab_comlif
		
		Dim ldtmMaxDate As Date
		Dim lblnErrors As Boolean
		
		On Error GoTo insValMAG002_K_Err
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsValField.objErr = lclsErrors
		lclsTab_Comlif = New Tab_comlif
		
		lblnInquiry = False
		lblnModify = False
		lblnErrors = False
		
		'+Validación del campo tabla
		
		If nComtabli = eRemoteDB.Constants.intNull Or nComtabli = 0 Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10048)
		Else
			lclsValField.Min = 1
			lclsValField.Max = 32767
			Call lclsValField.ValNumber(nComtabli)
			If nComtabli < 32768 Then
				Me.nComtabli = CShort(nComtabli)
				lclsTab_Comlif.nComtabli = CShort(nComtabli)
				If Not lclsTab_Comlif.Find Then
					Call lclsErrors.ErrorMessage(sCodispl, 60390)
				End If
			End If
		End If
		
		'+Validación del campo fecha
		
		If dEffecdate = dtmNull Then
			lblnErrors = True
			Call lclsErrors.ErrorMessage(sCodispl, 10190)
		Else
			If nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                Me.nComtabli = nComtabli
				Call Find_date()
				
				ldtmMaxDate = Me.dEffecdate

                If dEffecdate <= Today Then
                    lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
                    Call lclsErrors.ErrorMessage(sCodispl, 10868)
                    lblnErrors = True
                    lblnModify = False
                    lblnInquiry = True
                End If

				If dEffecdate < ldtmMaxDate Then
					lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
                    Call lclsErrors.ErrorMessage(sCodispl, 10869, , , "Fecha de última modificación " & CStr(ldtmMaxDate))
					lblnErrors = True
					lblnModify = False
					lblnInquiry = True
				Else
					If nAction <> eFunctions.Menues.TypeActions.clngActionadd Then
						lblnInquiry = True
						lblnModify = True
					Else
						lblnInquiry = False
						lblnModify = False
					End If
				End If
			End If
		End If
		
		
		
		insValMAG002_K = lclsErrors.Confirm
		
		
insValMAG002_K_Err: 
		If Err.Number Then
			insValMAG002_K = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsTab_Comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Comlif = Nothing
		
	End Function
	'%insValMAG002: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
	Public Function insValMAG002(ByVal sCodispl As String, ByVal sAction As String, ByVal nComtabli As Integer, ByVal dEffecdate As Date, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nMin_durat As Integer, ByVal nPolicy_dur As Integer, ByVal nPercent As Double, ByVal nMax_durat As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nWay_Pay As Integer, ByVal nSellchannel As Integer, ByVal nAmount As Double) As String
		
		'- Se define la variable lclsErrors para el envío de errores de la ventana
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsDet_comlif As eAgent.Det_comlif
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		
		On Error GoTo insValMAG002_Err
		
		nProduct = IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct)
		nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
		nWay_Pay = IIf(nWay_Pay = eRemoteDB.Constants.intNull, 0, nWay_Pay)
		nSellchannel = IIf(nSellchannel = eRemoteDB.Constants.intNull, 0, nSellchannel)
		
		'+ Validación del campo Ramo
		
		If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9064)
		End If
		
		'+ Validación de la duración mínima
		
		If nMin_durat = eRemoteDB.Constants.intNull Or nMin_durat = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 9090)
		End If
		
		'+ Validación de la duración maxima
		
		If nMax_durat = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 55524)
		Else
			If nMax_durat < nMin_durat Then
				Call lclsErrors.ErrorMessage(sCodispl, 55525)
			End If
		End If
		
		'+ Validación del año de la póliza
		
		If nPolicy_dur = eRemoteDB.Constants.intNull Then
			Call lclsErrors.ErrorMessage(sCodispl, 9091)
		End If
		
		'+Se valida que los valores introducidos no estén registrados
		
		If nComtabli <> eRemoteDB.Constants.intNull And nBranch > 0 And nProduct <> eRemoteDB.Constants.intNull And nMin_durat <> eRemoteDB.Constants.intNull And nPolicy_dur <> eRemoteDB.Constants.intNull And dEffecdate <> dtmNull And sAction = "Add" Then
			lclsDet_comlif = New eAgent.Det_comlif
			If lclsDet_comlif.Find(nComtabli, nBranch, nProduct, nPolicy_dur, nMin_durat, nModulec, nCover, nWay_Pay, nSellchannel, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 8307)
			Else
				If nMin_durat > 0 And nMax_durat > 0 Then
					
					'+ Se valida que el rango introducido no se encuentre registrado en la BD
					If Find_range(nComtabli, nBranch, nProduct, nPolicy_dur, nMin_durat, nModulec, nCover, nWay_Pay, nSellchannel, dEffecdate, nMax_durat) Then
						Call lclsErrors.ErrorMessage(sCodispl, 60214,  ,  , " [" & nMin_durat & "," & nMax_durat & "] ")
					End If
				End If
			End If
			'UPGRADE_NOTE: Object lclsDet_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lclsDet_comlif = Nothing
		End If
		
		'+ Validación del porcentage de comision
		
		If nPercent <> eRemoteDB.Constants.intNull Then
			lclsValField.Min = 0#
			lclsValField.Max = 99.99
			'lclsErrors.ETextAlign = RigthAling
			lclsValField.objErr = lclsErrors
			
			Call lclsValField.ValNumber(nPercent)
		Else
			If nAmount = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 60428)
			End If
		End If
		
		insValMAG002 = lclsErrors.Confirm
		
		'UPGRADE_NOTE: Object lclsDet_comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsDet_comlif = Nothing
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		
insValMAG002_Err: 
		If Err.Number Then
			insValMAG002 = lclsErrors.Confirm & Err.Description
		End If
		On Error GoTo 0
		
	End Function
	'*InsPostMAG002: Esta función se encarga de crear/actualizar los registros
	'*correspondientes en la tabla de Det_comlif
	Public Function insPostMAG002(ByVal sAction As String, Optional ByVal nSeleted As Integer = 0, Optional ByVal nComtabli As Integer = 0, Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy_dur As Integer = 0, Optional ByVal nMin_durat As Integer = 0, Optional ByVal nPercent As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nWay_Pay As Integer = 0, Optional ByVal nSellchannel As Integer = 0, Optional ByVal nMax_durat As Integer = 0) As Boolean
		
		Dim lclsTab_Comlif As eAgent.Tab_comlif
		
		On Error GoTo insPostMAG002_err
		
		lclsTab_Comlif = New eAgent.Tab_comlif
		
		Me.nComtabli = nComtabli
		Me.nBranch = nBranch
		
		Me.nProduct = IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct)
		
		Me.nPolicy_dur = nPolicy_dur
		Me.nMin_durat = nMin_durat
		Me.nPercent = nPercent
		Me.dEffecdate = dEffecdate
		Me.nUsercode = nUsercode
		
		Me.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
		Me.nCover = IIf(nCover = eRemoteDB.Constants.intNull, 0, nCover)
		Me.nCurrency = nCurrency
		Me.nAmount = nAmount
		Me.nWay_Pay = IIf(nWay_Pay = eRemoteDB.Constants.intNull, 0, nWay_Pay)
		Me.nSellchannel = IIf(nSellchannel = eRemoteDB.Constants.intNull, 0, nSellchannel)
		Me.nMax_durat = nMax_durat
		
		insPostMAG002 = True
		
		Select Case sAction
			
			'+Si la opción seleccionada es Registrar
			
			Case "Add"
				insPostMAG002 = Update()
				
				'+Si la opción seleccionada es Modificar
				
			Case "Update"
				insPostMAG002 = Update()
				
				'+Si la opción seleccionada es Eliminar
				
			Case "Del"
				insPostMAG002 = Delete()
				
		End Select
		
		If insPostMAG002 Then
			lclsTab_Comlif.nComtabli = nComtabli
			lclsTab_Comlif.sStatregt = "1"
			lclsTab_Comlif.nUsercode = nUsercode
			Call lclsTab_Comlif.Update()
		End If
		
insPostMAG002_err: 
		If Err.Number Then
			insPostMAG002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsTab_Comlif may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_Comlif = Nothing
	End Function
	
	'* Class_Initialize: se controla la apertura de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		'nUsercode = GetSetting("TIME", "GLOBALS", "USERCODE", 0)
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






