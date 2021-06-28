Option Strict Off
Option Explicit On
Public Class Goals
	'%-------------------------------------------------------%'
	'% $Workfile:: Goals.cls                                $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 13                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'Goals' en el sistema 19/12/2001 11:16:56 a.m.
	
	'+       Column name              Type
	'+  ------------------------- ------------
	
	Public nCode As Integer
	Public dEffecdate As Date
	Public nYear As Integer
	Public nPeriodNum As Integer
	Public sType_Infor As String
	Public sPeriodTyp As String
	Public nCurrency As Integer
	Public nBranch As Integer
	Public nProduct As Integer
	Public nGoal As Double
	Public nPercent As Double
	Public nUsercode As Integer
	
	'Find: Función que realiza la busqueda en la tabla 'Goals'
	Public Function Find(ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecGoals As eRemoteDB.Execute
		
		lrecGoals = New eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		'+ Define all parameters for the stored procedures 'insudb.reaGoals'. Generated on 19/12/2001 11:23:56 a.m.
		With lrecGoals
			.StoredProcedure = "reaGoals_v"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCode = .FieldToClass("nCode")
				dEffecdate = .FieldToClass("dEffecdate")
				nYear = .FieldToClass("nYear")
				nPeriodNum = .FieldToClass("nPeriodnum")
				sType_Infor = .FieldToClass("sType_infor")
				sPeriodTyp = .FieldToClass("sPeriodtyp")
				nCurrency = .FieldToClass("nCurrency")
				nBranch = .FieldToClass("nBranch")
				nProduct = .FieldToClass("nProduct")
				nGoal = .FieldToClass("nGoal")
				nPercent = .FieldToClass("nPercent")
				Find = True
				.RCloseRec()
			Else
				Find = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecGoals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGoals = Nothing
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
	End Function
	
	'**% LastDateGoals: This method is in charge of initiliazing the variable
	'**%that contains the last date of modification of the selected in the table Goals
	'%LastDateGoals. Este metodo se encarga de inicializar la variable que contiene
	'%la ultima fecha de modificacion del registro seleccionado en la tabla Goals
	Public Function LastDateGoals(ByVal nCode As Integer, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer) As Date
		Dim lrecreaLastDateGoals As eRemoteDB.Execute
		
		On Error GoTo LastDateGoals_Err
		
		lrecreaLastDateGoals = New eRemoteDB.Execute
		
		With lrecreaLastDateGoals
			.StoredProcedure = "reaLastDateGoals"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
				LastDateGoals = IIf(.FieldToClass("dEffecdate") = dtmNull, CDate("01/01/1800"), .FieldToClass("dEffecdate"))
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecreaLastDateGoals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLastDateGoals = Nothing
		
LastDateGoals_Err: 
		If Err.Number Then
			LastDateGoals = dtmNull
		End If
		On Error GoTo 0
	End Function
	
	'**% Add. this Method adds new records to the table Goals
	'%Add. Este metodo se encarga de agergar nuevos registros a la tabla Goals.
	Public Function Add(ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nGoal As Double, ByVal nPercent As Double, ByVal nUsercode As Integer) As Boolean
		Dim lrecinscreupdGoals As eRemoteDB.Execute
		
		On Error GoTo Add_Err
		lrecinscreupdGoals = New eRemoteDB.Execute
		
		With lrecinscreupdGoals
			.StoredProcedure = "inscreupdGoals"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGoal", nGoal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinscreupdGoals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinscreupdGoals = Nothing
		
Add_Err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
	End Function
	'%Delete. Este metodo se encarga de Eliminar registros a la tabla Goals.
	Public Function Delete(ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer) As Boolean
		Dim lrecinsdelGoals As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecinsdelGoals = New eRemoteDB.Execute
		
		With lrecinsdelGoals
			.StoredProcedure = "insdelGoals"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecinsdelGoals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsdelGoals = Nothing
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
	End Function
	
	'%ExistGoals. Este metodo se verifica la existencia de una meta en la tabla Goasl
	Public Function ExistGoals(ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecvalGoals_o As eRemoteDB.Execute
		
		On Error GoTo ExistGoals_Err
		
		lrecvalGoals_o = New eRemoteDB.Execute
		
		With lrecvalGoals_o
			.StoredProcedure = "valGoals_o"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nYear", nYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPeriodnum", nPeriodNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType_infor", sType_Infor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPeriodtyp", sPeriodTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				ExistGoals = .FieldToClass("Count") > 0
				.RCloseRec()
			End If
		End With
		'UPGRADE_NOTE: Object lrecvalGoals_o may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalGoals_o = Nothing
		
ExistGoals_Err: 
		If Err.Number Then
			ExistGoals = False
		End If
		On Error GoTo 0
	End Function
	
	'**%InsValAG005_K: This method is in charge of performing the validations of the header
	'**%described in the functional of the window AG005
	'%InsValAG005_K: Este metodo se encarga de realizar las validaciones del encabezado (Header)
	'%descritas en el funcional de la ventana AG005
	Public Function InsValAG005_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lclstab_goals As eAgent.tab_goals
		Dim ldtmLastDate As Date
		
		On Error GoTo InsValAG005_K_Err
		lclsErrors = New eFunctions.Errors
		lclstab_goals = New eAgent.tab_goals
		lvalTime = New eFunctions.valField
		lvalTime.objErr = lclsErrors
		ldtmLastDate = CDate("01/01/1800")
		
		
		With lclsErrors
			'**+ Validation of the Table
			'+Validación del Código de la Tabla.
			If nCode = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1942)
			Else
				If Not lclstab_goals.IsExist(nCode) Then
					'++ Aqui va el mensaje de que el codigo de la tabla no existe en el archivo de Tablas de Metas (Tab_Goals)
					Call .ErrorMessage(sCodispl, 60434)
				End If
			End If
			
			'**+ Validation of Period-Year
			'+Validación del Período-Año.
			If nYear = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 9060)
			End If
			
			'**+ Validation of the period type
			'+Validación del Tipo de período.
			If sPeriodTyp = strNull Then
				Call .ErrorMessage(sCodispl, 9061)
			End If
			
			'**+ Validation of the Period number
			'+Validación del Número de período.
			If nPeriodNum = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 9063)
			Else
				If sPeriodTyp <> CStr(eRemoteDB.Constants.intNull) Then
					Select Case sPeriodTyp
						Case "1" '+ Mensual.
							lvalTime.Min = 1
							lvalTime.Max = 12
							
						Case "2" '+ Bimestral.
							lvalTime.Min = 1
							lvalTime.Max = 6
							
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
					lvalTime.Descript = "Número"
					If Not lvalTime.ValNumber(nPeriodNum) Then
					End If
				End If
			End If
			
			'**+ Validation of the Information Type
			'+Validación del Tipo de información.
			If sType_Infor = strNull Then
				Call .ErrorMessage(sCodispl, 9056)
			End If
			
			'**+ Validation of the Currency
			'+Validación de la Moneda.
			If nCurrency = eRemoteDB.Constants.intNull Then
				Call .ErrorMessage(sCodispl, 1351)
			End If
			
			'**+ Validation of the Effect date
			'+Validación de la Fecha de efecto.
			If dEffecdate = dtmNull Then
				Call .ErrorMessage(sCodispl, 1103)
			Else
				If lvalTime.ValDate(dEffecdate) Then
					If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
						ldtmLastDate = LastDateGoals(nCode, nYear, nPeriodNum, sType_Infor, sPeriodTyp, nCurrency)
						If dEffecdate < ldtmLastDate Then
							Call .ErrorMessage(sCodispl, 1021,  , eFunctions.Errors.TextAlign.RigthAling, CStr(ldtmLastDate))
						End If
					End If
				End If
			End If
			
			InsValAG005_K = .Confirm
		End With
		
InsValAG005_K_Err: 
		If Err.Number Then
			InsValAG005_K = InsValAG005_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclstab_goals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstab_goals = Nothing
		'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalTime = Nothing
	End Function
	
	'**% insValAG005Upd: validate the fields on the page AG005, for the grid handle
	'%insValAG005Upd: se validan los campos de la página AG005, para el manejo del grid
	Public Function insValAG005Upd(ByVal sAction As String, ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nGoal As Double, ByVal nPercent As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lvalTime As eFunctions.valField
		Dim lstrBranch As String
		Dim lstrProduct As String
		Dim lclsQuery As eRemoteDB.Query
		
		'**- Variable definition to aknowledge if there are any errors
		'- Se define la variable para saber si existen errores
		Dim lblnValid As Boolean
		'**- Variable definition to aknowledge if there are any production goals for the intermediary
		'- Se define la variable para saber si existen metas de producción para el intermediario
		
		On Error GoTo insValAG005Upd_Err
		
		lclsErrors = New eFunctions.Errors
		lvalTime = New eFunctions.valField
		lclsQuery = New eRemoteDB.Query
		lvalTime.objErr = lclsErrors
		
		'**+ Search the description for field
		'+ Se busca la descripción para "Ramo"
		With lclsQuery
			If (.OpenQuery("Table563", "sDescript", "nCodigint=212")) Then
				lstrBranch = .FieldToClass("sDescript")
				.CloseQuery()
			Else
				lstrBranch = String.Empty
			End If
			
			If .OpenQuery("Table563", "sDescript", "nCodigint=251") Then
				lstrProduct = .FieldToClass("sDescript")
				.CloseQuery()
			Else
				lstrProduct = String.Empty
			End If
		End With
		
		lblnValid = True
		
		With lclsErrors
			'**+ Field "Line of business"
			'+ Campo Ramo.
			If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
				'**+ Must be full
				'+ Debe estar lleno
				Call .ErrorMessage("AG005", 9064)
				lblnValid = False
			End If
			
			'**+ Field Goal
			'+ Campo Meta.
			
			If nGoal = eRemoteDB.Constants.intNull Or nGoal = 0 Then
				
				'**+ If the Line of business is full, it must be full
				'+ Si el ramo está lleno, debe estar lleno.
				
				'If Not lblnValid Then
				If nBranch > 0 And (nGoal = eRemoteDB.Constants.intNull Or nGoal = 0) Then
					Call .ErrorMessage("AG005", 10188)
				End If
			Else
				With lvalTime
					.Min = 1
					.Max = 9999999999.99
					If .ValNumber(nGoal,  , eFunctions.valField.eTypeValField.ValAll) Then
						If CDec(nGoal) <= 0 Then
							Call lclsErrors.ErrorMessage("AG005", 3749)
							lblnValid = False
						End If
					Else
						lblnValid = False
					End If
				End With
				
				'**+ If it is add ,validate that there in not duplicated information
				'+ Si se está registrando se valida que no exista duplicidad de la información.
				
				If sAction = "Add" Then
					If ExistGoals(nCode, dEffecdate, nYear, nPeriodNum, sType_Infor, sPeriodTyp, nCurrency, nBranch, nProduct) Then
						Call .ErrorMessage("AG005", 1927,  , eFunctions.Errors.TextAlign.LeftAling, lstrBranch & " - " & lstrProduct & ":")
						lblnValid = False
					End If
				End If
			End If
			
			insValAG005Upd = .Confirm
			
		End With
		
insValAG005Upd_Err: 
		If Err.Number Then
			insValAG005Upd = insValAG005Upd & Err.Description
		End If
		On Error GoTo 0
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lvalTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lvalTime = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
	End Function
	
	'**%insPostAG005Upd. This method updates the database (as described in the functional specifications) for the page "AG005" (Popup)
	'% insPostAG005Upd: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'% especificaciones funcionales) de la ventana "AG005" (Popup)
	Public Function InsPostAG005Upd(ByVal sAction As String, ByVal nCode As Integer, ByVal dEffecdate As Date, ByVal nYear As Integer, ByVal nPeriodNum As Integer, ByVal sType_Infor As String, ByVal sPeriodTyp As String, ByVal nCurrency As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nGoal As Double, ByVal nPercent As Double, ByVal nUsercode As Integer) As Boolean
		
		Select Case sAction
			Case "Add", "Update"
				InsPostAG005Upd = Add(nCode, dEffecdate, nYear, nPeriodNum, sType_Infor, sPeriodTyp, nCurrency, nBranch, IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), nGoal, nPercent, nUsercode)
			Case "Del"
				InsPostAG005Upd = Delete(nCode, dEffecdate, nYear, nPeriodNum, sType_Infor, sPeriodTyp, nCurrency, nBranch, IIf(nProduct = eRemoteDB.Constants.intNull, 0, nProduct), nUsercode)
		End Select
	End Function
	
	'%Addtableinterm_bud: Función que realiza Incluye los registros correspondientes a una tabla
	'%en Goal en la tabla interm_bud con su respectivo intermediario
	Public Function Addtableinterm_bud(ByVal nCode As Integer, ByVal nIntermed As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
		Dim lrecinstableinterm_bud As eRemoteDB.Execute
		
		lrecinstableinterm_bud = New eRemoteDB.Execute
		
		On Error GoTo Addtableinterm_bud_Err
		
		With lrecinstableinterm_bud
			.StoredProcedure = "instableinterm_bud"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Addtableinterm_bud = True
			Else
				Addtableinterm_bud = False
			End If
		End With
		'UPGRADE_NOTE: Object lrecinstableinterm_bud may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinstableinterm_bud = Nothing
		
Addtableinterm_bud_Err: 
		If Err.Number Then
			Addtableinterm_bud = False
		End If
		On Error GoTo 0
	End Function
End Class






