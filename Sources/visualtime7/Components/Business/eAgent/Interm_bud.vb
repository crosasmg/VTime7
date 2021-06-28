Option Strict Off
Option Explicit On
Public Class Interm_bud
	'%-------------------------------------------------------%'
	'% $Workfile:: Interm_bud.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+  Column_name        Type                   Length      Prec  Scale Nullable
	'--------------------- ---------------------- ----------- ----- ----- --------
	Public nIntermed As Double 'int        4           10    0     no
	Public nCurrency As Integer 'smallint   2           5     0     no
	Public sType_Infor As String 'char       1                       no
	Public sPeriodTyp As String 'char       1                       no
	Public nPeriodNum As Integer 'smallint   2           5     0     no
	Public nBranch As Integer 'smallint   2           5     0     no
	Public nProduct As Integer 'smallint   2           5     0     no
	Public dEffecdate As Date 'datetime   8                       no
	Public nBud_total As Double 'decimal    9           14    2     yes
	Public dNulldate As Date 'datetime   8                       yes
	Public nReal_total As Double 'decimal    9           14    2     yes
	Public nUsercode As Integer 'smallint   2           5     0     yes
	Public nYear As Integer 'smallint   2           5     0     yes
	
	'**+ Auxiliary variables
	'+Variables auxiliares
	
	Public sDesc_prod As String
	
	'**% LastDateInterm_bud: This method is in charge of initiliazing the variable
	'**%that contains the last date of modification of the selected in the table Interm_bud
	'%LastDateInterm_bud. Este metodo se encarga de inicializar la variable que contiene
	'%la ultima fecha de modificacion del registro seleccionado en la tabla Interm_bud
	Public ReadOnly Property LastDateInterm_bud() As Date
		Get
			Dim lrecreaLastDateInterm_bud As eRemoteDB.Execute
			
			On Error GoTo LastDateInterm_bud_Err
			lrecreaLastDateInterm_bud = New eRemoteDB.Execute
			
			'**+ Parameter definitions for stored procedure 'insudb.reaLastDateInterm_bud'
			'+Definición de parámetros para stored procedure 'insudb.reaLastDateInterm_bud'
			'**+ Information read on february 05,2001 4:43:22 p.m.
			'+Información leída el 05/02/2001 4:43:11 PM
			
			With lrecreaLastDateInterm_bud
				.StoredProcedure = "reaLastDateInterm_bud"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					LastDateInterm_bud = IIf(.FieldToClass("dEffecdate") = dtmNull, CDate("01/01/1800"), .FieldToClass("dEffecdate"))
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecreaLastDateInterm_bud may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecreaLastDateInterm_bud = Nothing
			
LastDateInterm_bud_Err: 
			If Err.Number Then
				LastDateInterm_bud = System.Date.FromOADate(False)
			End If
			On Error GoTo 0
		End Get
	End Property
	
	'**% ExistInterm_bud: This method is in charge of verifying
	'**%if the intermediary have records associated in the table interm_bud
	'%ExistInterm_bud. Este metodo se encarga de verificar si un intermediario tiene
	'%registros asociados en la tabla interm_bud
	Public ReadOnly Property ExistInterm_bud() As Boolean
		Get
			Dim lrecvalInterm_bud_o As eRemoteDB.Execute
			
			On Error GoTo ExistInterm_bud_Err
			
			lrecvalInterm_bud_o = New eRemoteDB.Execute
			
			'**+ Parameter definition for stored procedure 'insudb.valInterm_bud_o'
			'+ Definición de parámetros para stored procedure 'insudb.valInterm_bud_o'
			'**+ Information read on February 06,2001  8:59:44 a.m.
			'+ Información leída el 06/02/2001 8:59:44 AM
			
			With lrecvalInterm_bud_o
				.StoredProcedure = "valInterm_bud_o"
				.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					ExistInterm_bud = .FieldToClass("Count") > 0
					.RCloseRec()
				End If
			End With
			'UPGRADE_NOTE: Object lrecvalInterm_bud_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lrecvalInterm_bud_o = Nothing
			
ExistInterm_bud_Err: 
			If Err.Number Then
				ExistInterm_bud = False
			End If
			On Error GoTo 0
		End Get
	End Property
	
	'**% Add. this Method adds new records to the table Interm_bud
	'%Add. Este metodo se encarga de agergar nuevos registros a la tabla Interm_bud.
	Public Function Add(ByVal nAction As Integer) As Boolean
		Dim lrecinsInterm_bud As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lrecinsInterm_bud = New eRemoteDB.Execute
		
		'** Parameter definitions for stored procedure 'insudb.insInterm_bud'
		'Definición de parámetros para stored procedure 'insudb.insInterm_bud'
		'** Information read on February 05,2001 4:50:50 p.m.
		'Información leída el 05/02/2001 4:50:50 PM
		With lrecinsInterm_bud
			.StoredProcedure = "insInterm_bud"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBud_total", nBud_total, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsInterm_bud may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsInterm_bud = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	
	'**%InsValAGC574_K: This method is in charge of performing the validations of the header
	'**%described in the functional of the window AGC574
	'%InsValAGC574_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana AGC574
	Public Function InsValAGC574_K(ByVal sCodispl As String, ByVal lintAction As Integer, Optional ByVal nIntermed As Double = 0, Optional ByVal nGoals As Integer = 0, Optional ByVal nYear As Integer = 0, Optional ByVal sPeriodType As String = "", Optional ByVal nPeriodNumber As Integer = 0, Optional ByVal sTypeInfor As String = "", Optional ByVal nCurrency As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#) As String
		Dim lerrTime As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lclsAgent As eAgent.Intermedia
		Dim lclsInterm_bud As eAgent.Interm_bud
		Dim lclsGoals As eAgent.Goalss
		Dim lblnReady As Boolean
		Dim ldtmLastDate As Date
		
		On Error GoTo InsValAGC574_K_Err
		
		lerrTime = New eFunctions.Errors
		lclsAgent = New eAgent.Intermedia
		lclsInterm_bud = New eAgent.Interm_bud
		lvalTime = New eFunctions.valField
		lclsGoals = New eAgent.Goalss
		
		ldtmLastDate = CDate("01/01/1800")
		
		lblnReady = False
		
		'**+ Validation of the Intermediary
		'+Validación del Intermediario.
		If nIntermed = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 9036)
		Else
			If lclsAgent.Find(nIntermed) Then
				If lclsAgent.nInt_status <> 1 Then
					Call lerrTime.ErrorMessage(sCodispl, 9079)
				End If
				lblnReady = True
			Else
				Call lerrTime.ErrorMessage(sCodispl, 9002)
			End If
		End If
		
		'**+ Validation of Period-Year
		'+Validación del Período-Año.
		If nYear = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 9060)
		Else
			If lblnReady Then
				If nYear < Year(lclsAgent.dInpdate) Then
					Call lerrTime.ErrorMessage(sCodispl, 9081)
				End If
				If lclsAgent.nInt_status = 2 Then
					If nYear > Year(lclsAgent.dNulldate) Then
						Call lerrTime.ErrorMessage(sCodispl, 9085)
					End If
				End If
			End If
		End If
		
		'**+ Validation of the period type
		'+Validación del Tipo de período.
		If sPeriodType = strNull Then
			Call lerrTime.ErrorMessage(sCodispl, 9061)
		End If
		
		'**+ Validation of the Period number
		'+Validación del Número de período.
		If nPeriodNumber = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 9063)
		Else
			lvalTime.objErr = lerrTime
			If sPeriodType <> CStr(eRemoteDB.Constants.intNull) Then
				Select Case sPeriodType
					Case "1" '+ Mensual.
						lvalTime.Min = 1
						lvalTime.Max = 12
						
					Case "2" '+ Bimensual.
						lvalTime.Min = 1
						lvalTime.Max = 2
						
					Case "3" '+ Trimestral.
						lvalTime.Min = 1
						lvalTime.Max = 4
						
					Case "4" '+ Semestral.
						lvalTime.Min = 1
						lvalTime.Max = 2
						
					Case "5" '+ Anual.
						lvalTime.Min = 1
						lvalTime.Max = 1
						
				End Select
				lvalTime.ErrRange = 9058
				If Not lvalTime.ValNumber(nPeriodNumber) Then
				End If
			End If
		End If
		
		'**+ Validation of the Information Type
		'+Validación del Tipo de información.
		If sTypeInfor = strNull Then
			Call lerrTime.ErrorMessage(sCodispl, 9056)
		End If
		
		'**+ Validation of the Currency
		'+Validación de la Moneda.
		If nCurrency = eRemoteDB.Constants.intNull Then
			Call lerrTime.ErrorMessage(sCodispl, 1351)
		End If
		
		'**+ Validation of the Effect date
		'+Validación de la Fecha de efecto.
		If dEffecdate = dtmNull Then
			Call lerrTime.ErrorMessage(sCodispl, 1103)
		Else
			If lvalTime.ValDate(dEffecdate) Then
				If lintAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
					With lclsInterm_bud
						.nIntermed = nIntermed
						.nCurrency = nCurrency
						.sType_Infor = sTypeInfor
						.sPeriodTyp = sPeriodType
						.nPeriodNum = nPeriodNumber
						ldtmLastDate = .LastDateInterm_bud
					End With
					If dEffecdate < ldtmLastDate Then
						lvalTime.Descript = ldtmLastDate & ": "
						Call lerrTime.ErrorMessage(sCodispl, 1021)
					End If
				End If
			End If
		End If
		
		InsValAGC574_K = lerrTime.Confirm
		
InsValAGC574_K_Err: 
		If Err.Number Then
			InsValAGC574_K = InsValAGC574_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lerrTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lerrTime = Nothing
		'UPGRADE_NOTE: Object lclsAgent may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsAgent = Nothing
		'UPGRADE_NOTE: Object lclsInterm_bud may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsInterm_bud = Nothing
		'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalTime = Nothing
		'UPGRADE_NOTE: Object lclsGoals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGoals = Nothing
	End Function
	
	'**% insValDupTabProduct: validate if a production goal of an intemediary is already
	'**% registred, considering the porduct line of business.
	'% insvalDupTabProduct: Permite validar si una meta de producción de un intermediario ya está
	'%                      registrada, tomando en cuenta el ramo producto.
	Private Function insvalDupTabProduct(ByVal nIntermed As Double, ByVal nCurrency As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nPeriodNum As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Boolean
		Me.nIntermed = nIntermed
		Me.nCurrency = nCurrency
		Me.sType_Infor = sType_Infor
		Me.sPeriodTyp = sPeriodTyp
		Me.nPeriodNum = nPeriodNum
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.dEffecdate = dEffecdate
		insvalDupTabProduct = ExistInterm_bud
	End Function
	
	'**% insvalTabBranchProdPar:  validate if a product goal of an intermediary already exists.
	'% insvalTabBranchProdPar: Permite validar si una meta de producción de un intermediario
	'%                         ya está registrada.
	Private Function insvalTabBranchProdPar(ByVal nIntermed As Double, ByVal nCurrency As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nPeriodNum As Integer, ByVal nBranch As Integer, ByVal dEffecdate As Date) As Boolean
		Me.nIntermed = nIntermed
		Me.nCurrency = nCurrency
		Me.sType_Infor = sType_Infor
		Me.sPeriodTyp = sPeriodTyp
		Me.nPeriodNum = nPeriodNum
		Me.nBranch = nBranch
		Me.nProduct = eRemoteDB.Constants.intNull
		Me.dEffecdate = dEffecdate
		insvalTabBranchProdPar = ExistInterm_bud
	End Function
End Class






