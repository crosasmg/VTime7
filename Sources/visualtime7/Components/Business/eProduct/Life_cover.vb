Option Strict Off
Option Explicit On
Public Class Life_cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Life_cover.cls                           $%'
	'% $Author:: Nvaplat26                                  $%'
	'% $Date:: 3/10/03 17.17                                $%'
	'% $Revision:: 26                                       $%'
	'%-------------------------------------------------------%'
	
	'-Se definen las propiedades de la tabla Life_cover
	
	'+ Column_name                 Type                                                                                                                             Computed                            Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'+ --------------------------- ----------
	Public nBranch As Integer
	Public nCover As Integer
	Public nProduct As Integer
	Public nModulec As Integer
	Public dEffecdate As Date
	Public sAddSuini As String
	Public sAddReini As String
	Public sAddTaxin As String
	Public nCovergen As Integer
	Public nBill_item As Integer
	Public nBranch_gen As Integer
	Public nBranch_est As Integer
	Public nBranch_led As Integer
	Public nBranch_rei As Integer
	Public nCaextexp As Double
	Public sMortacof As String
	Public nCaintexp As Double
	Public sMortacom As String
	Public nRetarif As Integer
	Public sCoveruse As String
	Public nCurrency As Integer
	Public nInterest As Double
	Public dNulldate As Date
	Public nNotenum As Integer
	Public nPrextexp As Double
	Public nPrintexp As Double
	Public sRoureser As String
	Public sRousurre As String
	Public sStatregt As String
	Public nusercode As Integer
	Public sControl As String
	Public nPer_tabmor As Double
	Public sCalrein As String
	Public sDepend As String
	Public sSurv As String
	Public sReinorigcond As String
	Public sCondSVS As String
	Public sRouClaTec As String
	Public sRoutresrisk As String
	
	'+ Se define la variable que contiene el estado de la cada instancia de la clase
	Public nStatusInstance As Integer
	
	'+ Descripción de la cobertura
	Public sDescript As String
	
	'% InsUpdLife_cover: Se encarga de actualizar la tabla
	Private Function InsUpdLife_cover(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdLife_cover As eRemoteDB.Execute
		
		On Error GoTo InsUpdLife_cover_Err
		
		lrecInsUpdLife_cover = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsUpdLife_cover'
		'+Información leída el 30/10/01
		With lrecInsUpdLife_cover
			.StoredProcedure = "InsUpdLife_cover"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddsuini", sAddSuini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddreini", sAddReini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddtaxin", sAddTaxin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCaextexp", nCaextexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCaintexp", nCaintexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCoveruse", sCoveruse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInterest", nInterest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMortacof", sMortacof, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sMortacom", sMortacom, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrextexp", nPrextexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPrintexp", nPrintexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 5, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRousurre", sRousurre, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nusercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sControl", sControl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRetarif", nRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPer_tabmor", nPer_tabmor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCalrein", sCalrein, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDepend", sDepend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSurv", sSurv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReinorigcond", sReinorigcond, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRouClaTec", sRouClaTec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutresrisk", sRoutresrisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			InsUpdLife_cover = .Run(False)
		End With
		
InsUpdLife_cover_Err: 
		If Err.Number Then
			InsUpdLife_cover = False
		End If
		'UPGRADE_NOTE: Object lrecInsUpdLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdLife_cover = Nothing
		On Error GoTo 0
	End Function
	
	'% insValOtherCover: función que valida la selección de suma para - suma - y suma para - mayor
	Private Function insValOtherCover(ByVal sField As String, ByVal sOption As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsLife_cover As eProduct.Life_cover
        Dim sCondition As String = ""
        Dim sValue As String
		
		On Error GoTo insValOtherCover_Err
		
		lclsLife_cover = New eProduct.Life_cover
		
		insValOtherCover = False
		
		If sOption = "Suma" Then
			Select Case sField
				Case "suini"
					sCondition = CStr(1)
				Case "reini"
					sCondition = CStr(2)
				Case "taxin"
					sCondition = CStr(3)
			End Select
		End If
		
		If sOption = "Suma" Then
			sValue = "3"
		Else
			sValue = "1"
		End If
		
		insValOtherCover = lclsLife_cover.FindLife_coverAdd(sCondition, sValue, nBranch, nProduct, nModulec, nCover, dEffecdate)
insValOtherCover_Err: 
		If Err.Number Then
			insValOtherCover = CShort(insValOtherCover) + CDbl(Err.Description)
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_cover = Nothing
	End Function
	
	'% Find: Este metodo se encarga de realiza la lectura de la tabla de coberturas genericas del
	'%       ramo/producto/módulo.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecReaLife_cover As eRemoteDB.Execute
		
		On Error GoTo ReaLife_cover_Err
		
		lrecReaLife_cover = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'ReaLife_cover'
		'+Información leída el 30/10/01
		With lrecReaLife_cover
			.StoredProcedure = "ReaLife_cover"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Find = True
				Me.nBranch = .FieldToClass("nBranch")
				Me.nProduct = .FieldToClass("nProduct")
				Me.nModulec = .FieldToClass("nModulec")
				Me.nCover = .FieldToClass("nCover")
				Me.dEffecdate = .FieldToClass("dEffecdate")
				sAddSuini = .FieldToClass("sAddsuini")
				sAddReini = .FieldToClass("sAddreini")
				sAddTaxin = .FieldToClass("sAddtaxin")
				nCovergen = .FieldToClass("nCovergen")
				nBill_item = .FieldToClass("nBill_item")
				nBranch_est = .FieldToClass("nBranch_est")
				nBranch_gen = .FieldToClass("nBranch_gen")
				nBranch_led = .FieldToClass("nBranch_led")
				nBranch_rei = .FieldToClass("nBranch_rei")
				nCaextexp = .FieldToClass("nCaextexp")
				nCaintexp = .FieldToClass("nCaintexp")
				sCoveruse = .FieldToClass("sCoveruse")
				nCurrency = .FieldToClass("nCurrency")
				nInterest = .FieldToClass("nInterest")
				sMortacof = .FieldToClass("sMortacof")
				sMortacom = .FieldToClass("sMortacom")
				dNulldate = .FieldToClass("dNulldate")
				nNotenum = .FieldToClass("nNotenum")
				nPrextexp = .FieldToClass("nPrextexp")
				nPrintexp = .FieldToClass("nPrintexp")
				sRoureser = .FieldToClass("sRoureser")
				sRousurre = .FieldToClass("sRousurre")
				sStatregt = .FieldToClass("sStatregt")
				nusercode = .FieldToClass("nUsercode")
				sControl = .FieldToClass("sControl")
				nRetarif = .FieldToClass("nRetarif")
				nPer_tabmor = .FieldToClass("nPer_tabmor")
				sCalrein = .FieldToClass("sCalrein")
				sDepend = .FieldToClass("sDepend")
				sSurv = .FieldToClass("sSurv")
				sDescript = .FieldToClass("sDescript")
				sReinorigcond = .FieldToClass("sReinorigcond")
				sCondSVS = .FieldToClass("sCondSVS")
				sRouClaTec = .FieldToClass("sRouClaTec")
				sRoutresrisk = .FieldToClass("sRoutResRisk")
				
			End If
		End With
		
ReaLife_cover_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecReaLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaLife_cover = Nothing
	End Function
	
	'% CoverInProduct: Valida que una cobertura no este asociada a un producto
	Public Function CoverInProduct(ByVal nCover As Integer) As Boolean
		Dim lrecLife_cover As eRemoteDB.Execute
		
		On Error GoTo CoverInProduct_Err
		
		lrecLife_cover = New eRemoteDB.Execute
		
		CoverInProduct = False
		
		With lrecLife_cover
			.StoredProcedure = "ReaLife_cover_By_Covergen"
			.Parameters.Add("nCovergen", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If lrecLife_cover.Run Then
				CoverInProduct = True
				.RCloseRec()
			End If
		End With
		
CoverInProduct_Err: 
		If Err.Number Then
			CoverInProduct = False
		End If
		'UPGRADE_NOTE: Object lrecLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecLife_cover = Nothing
	End Function
	
	'% Add: Crea registros en la tabla
	Public Function Add() As Boolean
		Add = InsUpdLife_cover(1)
	End Function
	
	'% Update: Actualiza registros en la tabla
	Public Function Update() As Boolean
		Update = InsUpdLife_cover(2)
	End Function
	
	'% Delete: Elimina los datos de la tabla
	Public Function Delete() As Boolean
		Delete = InsUpdLife_cover(3)
	End Function
	
	'% Count: Permite determinar la existencia de coberturas para el producto
	Public Function Count(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecreaLife_cover_count As eRemoteDB.Execute
		
		On Error GoTo Count_Err
		
		lrecreaLife_cover_count = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaLife_cover_count'
		'Información leída el 07/05/2001 14:38:56
		With lrecreaLife_cover_count
			.StoredProcedure = "reaLife_cover_count"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Count = .FieldToClass("nCount")
				.RCloseRec()
			End If
		End With
		
Count_Err: 
		If Err.Number Then
			Count = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLife_cover_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_cover_count = Nothing
	End Function
	
	'% Count_a: Permite determinar la existencia de coberturas genericas para el producto
	Public Function Count_a(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCovergen As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecreaLife_cover_count_a As eRemoteDB.Execute
		
		On Error GoTo Count_Err
		
		lrecreaLife_cover_count_a = New eRemoteDB.Execute
		
		With lrecreaLife_cover_count_a
			.StoredProcedure = "reaLife_cover_count_a"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				Count_a = .Parameters("nExists").Value ' .FieldToClass("nCount")
			End If
		End With
		
Count_Err: 
		If Err.Number Then
			Count_a = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaLife_cover_count_a may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_cover_count_a = Nothing
	End Function
	
	'% FindLife_coverAdd: se buscan los datos asociados a la cobertura
	Public Function FindLife_coverAdd(ByVal sCondition As String, ByVal sValue As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaLife_coverAdd As eRemoteDB.Execute
		
		On Error GoTo FindLife_coverAdd_err
		
		lrecreaLife_coverAdd = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaLife_coverAdd'
		'+ Información leída el 11/05/2001 02:00:21 p.m.
		FindLife_coverAdd = False
		
		With lrecreaLife_coverAdd
			.StoredProcedure = "reaLife_coverAdd"
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				FindLife_coverAdd = True
				.RCloseRec()
			End If
		End With
		
FindLife_coverAdd_err: 
		If Err.Number Then
			FindLife_coverAdd = False
		End If
		'UPGRADE_NOTE: Object lrecreaLife_coverAdd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaLife_coverAdd = Nothing
	End Function
	
	'% InsValDP018P: validaciones según especificaciones funcionales de la DP018P
	Public Function InsValDP018P(ByVal sCodispl As String, ByVal nBill_item As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValDP018P_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+Validación campo Concepto de facturación
		With lclsErrors
			If nBill_item = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11308)
			End If
			InsValDP018P = .Confirm
		End With
		
InsValDP018P_Err: 
		If Err.Number Then
			InsValDP018P = "InsValDP018P: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostDP018P: Actualizaciones según especificaciones funcionales de la DP018P
	Public Function insPostDP018P(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nCurrency As Integer, ByVal nBill_item As Integer, ByVal nRetarif As Integer, ByVal sCoveruse As String, ByVal sControl As String, ByVal sCalrein As String, ByVal sDepend As String, ByVal nBranch_led As Integer, ByVal nBranch_rei As Integer, ByVal nBranch_est As Integer, ByVal nBranch_gen As Integer, ByVal sAddSuini As String, ByVal sAddReini As String, ByVal sAddTaxin As String, ByVal nNotenum As Integer, ByVal nusercode As Integer, ByVal sSurv As String, ByVal sReinorigcond As String, ByVal sCondSVS As String) As Boolean
		On Error GoTo insPostDP018P_Err
		
		If Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
			With Me
				.dEffecdate = dEffecdate
				.nCurrency = nCurrency
				.nBill_item = nBill_item
				.nRetarif = nRetarif
				.sCoveruse = sCoveruse
				.sControl = sControl
				.sCalrein = sCalrein
				.sDepend = sDepend
				.nBranch_led = nBranch_led
				.nBranch_rei = nBranch_rei
				.nBranch_est = nBranch_est
				.nBranch_gen = nBranch_gen
				.sAddSuini = sAddSuini
				.sAddReini = sAddReini
				.sAddTaxin = sAddTaxin
				.nNotenum = nNotenum
				.nusercode = nusercode
				.sSurv = sSurv
				.sReinorigcond = IIf(sReinorigcond = String.Empty, "2", sReinorigcond)
				.sCondSVS = sCondSVS
				insPostDP018P = Update
			End With
		End If
		
insPostDP018P_Err: 
		If Err.Number Then
			insPostDP018P = False
		End If
		On Error GoTo 0
	End Function
	
	'% InsValDP50BP: validaciones según especificaciones funcionales de la DP50BP
	Public Function InsValDP50BP(ByVal sCodispl As String, ByVal sMortacom As String, ByVal sMortacof As String, ByVal nInterest As Double, ByVal nPrintexp As Double, ByVal nCaintexp As Double, ByVal nPrextexp As Double, ByVal nCaextexp As Double) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsMortality As Mortality
		Dim lclsConm_master As Conm_master
		
		On Error GoTo InsValDP50BP_err
		
		lclsErrors = New eFunctions.Errors
		lclsMortality = New Mortality
		lclsConm_master = New Conm_master
		
		With lclsErrors
			'+Validación campo tabla de mortalidad hombres
			If Trim(sMortacom) <> String.Empty Then
				If Not lclsMortality.insValMort_master(Trim(sMortacom), "1") Then
					.ErrorMessage(sCodispl, 11006,  , eFunctions.Errors.TextAlign.LeftAling, "Hombres: ")
				End If
			End If
			
			'+Validación campo tabla de mortalidad mujeres
			If Trim(sMortacof) <> String.Empty Then
				If Not lclsMortality.insValMort_master(Trim(sMortacof), "1") Then
					.ErrorMessage(sCodispl, 11006,  , eFunctions.Errors.TextAlign.LeftAling, "Mujeres: ")
				End If
			End If
			
			'+Validación campo % de interes
			If Trim(sMortacom) <> String.Empty Or Trim(sMortacof) <> String.Empty Then
				If nInterest = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 3138)
				Else
					If Trim(sMortacom) <> String.Empty Then
						If Not lclsConm_master.FindConm_master(Trim(sMortacom), nInterest) Then
							.ErrorMessage(sCodispl, 11039,  , eFunctions.Errors.TextAlign.LeftAling, "Hombres: ")
						End If
					Else
						If Not lclsConm_master.FindConm_master(Trim(sMortacof), nInterest) Then
							.ErrorMessage(sCodispl, 11039,  , eFunctions.Errors.TextAlign.LeftAling, "Mujeres: ")
						End If
					End If
				End If
			End If
			
			InsValDP50BP = .Confirm
		End With
		
InsValDP50BP_err: 
		If Err.Number Then
			InsValDP50BP = "InsValDP50BP: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsMortality may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsMortality = Nothing
		'UPGRADE_NOTE: Object lclsConm_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsConm_master = Nothing
	End Function
	
	'% InsPostDP50BP: Actualizar la tabla según especificaciones funcionales de la DP50BP
	Public Function InsPostDP50BP(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sMortacom As String, ByVal sMortacof As String, ByVal nInterest As Double, ByVal nPer_tabmor As Double, ByVal nPrintexp As Double, ByVal nPrextexp As Double, ByVal nCaintexp As Double, ByVal nCaextexp As Double, ByVal sRoureser As String, ByVal sRousurre As String, ByVal sRouClaTec As String, ByVal nusercode As Integer, ByVal sRoutresrisk As String) As Boolean
		With Me
			If .Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
				.dEffecdate = dEffecdate
				.sMortacom = sMortacom
				.sMortacof = sMortacof
				.nInterest = nInterest
				.nPer_tabmor = nPer_tabmor
				.nPrintexp = nPrintexp
				.nPrextexp = nPrextexp
				.nCaintexp = nCaintexp
				.nCaextexp = nCaextexp
				.sRoureser = sRoureser
				.sRousurre = sRousurre
				.sRouClaTec = sRouClaTec
				.nusercode = nusercode
				.sRoutresrisk = sRoutresrisk
				InsPostDP50BP = .Update
			End If
		End With
	End Function

    '% insvalQDuration: Esta rutina verifica que la duración de los
    '%                  pagos no sea mayor al máximo valor definido en la tabla de mortalidad utilizada
    Public Function insValQDuration(ByVal sField As String, ByVal sText As String, ByVal nDuration As Integer) As Boolean
        Dim lstrFieldAux As String

        insValQDuration = True

        Select Case sField
            Case "sMortacof"
                lstrFieldAux = Me.sMortacof
            Case "sMortaCom"
                lstrFieldAux = Me.sMortacom
            Case Else
                lstrFieldAux = String.Empty
        End Select

        '+ Si existe una tabla de mortalidad para las mujeres, se realiza la validación contra ésta

        If lstrFieldAux <> String.Empty Then
            If Not Me.insValMort_master(lstrFieldAux, "1") Then
                insValQDuration = False
            End If
        End If
    End Function

    '% insValMort_master: El objetivo de este metodo es verificar si existe la tabla de mortalidad
    '%                    indicada. En caso de existir retorna los datos de la tabla en sus respectivas propiedades.
    Public Function insValMort_master(ByVal sMortalco As String, ByVal sStatregt As String) As Boolean
		Dim lrecreaMort_master As eRemoteDB.Execute
		
		On Error GoTo insValMort_master_err
		
		lrecreaMort_master = New eRemoteDB.Execute
		
		insValMort_master = False
		
		With lrecreaMort_master
			.StoredProcedure = "reaMort_master"
			.Parameters.Add("sMortalco", sMortalco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insValMort_master = .Run(False)
		End With
		
insValMort_master_err: 
		If Err.Number Then
			insValMort_master = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaMort_master may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaMort_master = Nothing
	End Function
	
	'% DefaultValueDP018P: maneja los estados y/o valores por defecto de la ventana
	Public Function DefaultValueDP018P(ByVal sField As String) As Boolean
		Select Case sField
			'+ Cobertura básica.  Se habilita sólo si en el campo (sCoveruse) se indicó "Ambos"
			Case "chkCover_use_disabled"
				DefaultValueDP018P = IIf(sCoveruse = "2", False, True)
		End Select
	End Function
	
	'* Class_Initialize: Inicializa todas las variables publicas
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sAddSuini = String.Empty
		sAddReini = String.Empty
		sAddTaxin = String.Empty
		nCovergen = eRemoteDB.Constants.intNull
		nBill_item = eRemoteDB.Constants.intNull
		nBranch_est = eRemoteDB.Constants.intNull
		nBranch_gen = eRemoteDB.Constants.intNull
		nBranch_led = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nCaextexp = eRemoteDB.Constants.intNull
		nCaintexp = eRemoteDB.Constants.intNull
		sCoveruse = String.Empty
		nCurrency = eRemoteDB.Constants.intNull
		nInterest = eRemoteDB.Constants.intNull
		sMortacof = String.Empty
		sMortacom = String.Empty
		dNulldate = eRemoteDB.Constants.dtmNull
		nNotenum = eRemoteDB.Constants.intNull
		nPrextexp = eRemoteDB.Constants.intNull
		nPrintexp = eRemoteDB.Constants.intNull
		sRoureser = String.Empty
		sRousurre = String.Empty
		sStatregt = String.Empty
		nusercode = eRemoteDB.Constants.intNull
		nModulec = eRemoteDB.Constants.intNull
		sControl = String.Empty
		nRetarif = eRemoteDB.Constants.intNull
		sCalrein = String.Empty
		nPer_tabmor = eRemoteDB.Constants.intNull
		sDepend = String.Empty
		sSurv = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






