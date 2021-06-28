Option Strict Off
Option Explicit On
Public Class ColformRef
	'%-------------------------------------------------------%'
	'% $Workfile:: ColformRef.cls                           $%'
	'% $Author:: Nvaplat40                                  $%'
	'% $Date:: 24/09/04 6:00p                               $%'
	'% $Revision:: 122                                      $%'
	'%-------------------------------------------------------%'
	
	'-Se definen las constantes globales para el manejo del tipo de relación de cobro
	Public Enum TypeBordereaux
		cstrcollect = 1 '+ Cobro
		cstrReturn = 2 '+ Devolución
		cstrConcil = 3 '+ Conciliación
	End Enum
	
	Enum TypeActionsSeqColl
		cstrAdd = 1 '+ Agregar
		cstrQuery = 2 '+ Consultar
		cstrUpdate = 3 '+ recuperar
		cstrCut = 4 '+ Eliminar
        cstrModify = 5 '+ Modificación
	End Enum
	
	'- ColumName                    Nullable Data_type leng prec Data_default
	'------------------------------ -------- --------- ---- ---- ------------
	Public nBordereaux As Double 'N       NUMBER    10  10    0
	Public sType As String 'Y       CHAR      1
	Public nBank As Double 'Y       NUMBER    10  10    0
	Public nIntermed As Double 'Y       NUMBER    10  10    0
	Public nRel_amoun As Double 'Y       NUMBER    14  12    2
	Public nCurrency As Integer 'Y       NUMBER    5   5     0
	Public dCollect As Date 'Y       DATE      7
	Public dCollectdate As Date 'Y       DATE      7
	Public nPolicy As Double 'Y       NUMBER    10  10    0
	Public nBranch As Integer 'Y       NUMBER    5   5     0
	Public nOffice As Integer 'Y       NUMBER    5   5     0
	Public dCompdate As Date 'N       DATE      7
	Public nUsercode As Integer 'N       NUMBER    5   5     0
	Public sRel_Type As String 'Y       VARCHAR2  1
	Public nProduct As Integer 'Y       NUMBER    5   5     0
	Public sStatus As String 'Y       CHAR      1
	Public sConwin As String 'Y       CHAR      4
	Public nCertif As Double 'Y       NUMBER    10  10    0
	Public sClient As String 'Y       CHAR      14
	Public nUser_amend As Integer 'N       NUMBER    5   5     0
	Public nCashnum As Integer 'N       NUMBER    5   5     0
	Public nInputtyp As Integer 'N       NUMBER    5   5     0
	Public nInsur_area As Integer 'N       NUMBER    5   5     0
	Public nAgreement As Integer 'Y       NUMBER    5   5     0
	Public sInd_Annuity As String 'Y       CHAR      14
	Public dDate_Benlar As Date 'N       DATE      7
	Public nCollector As Double 'Y       NUMBER   10
	Public sCollector_Name As String
	Public sRelOrigi As String 'Y       CHAR     1
	
	Public nAction As Integer
	Public dValueDate As Date
	
	'-Variables auxiliares
	
	Public blnError As Boolean
	Public nPaidAmount As Double
	Public nTotalAmount As Double
	Public nDifference As Double
	Public nDiffTotal As Double
	
	Public nPaidAmountDec As Double
	Public nTotalAmountDec As Double
	Public nDifferenceDec As Double
	Public nDiffTotalDec As Double
	
    Public mobjOpt_Premiu As Object
	Public sKeyGenDoc As String
	Public sDesBank As String
	Public sDigit As String
	Public sCliename As String
	Public nAmountDif As Double
	Public nqDocs As Double
	Public sDesRel_Type As String
	Public nAmountDoc As Double
	Public nSequence As Integer
	
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBordereaux = eRemoteDB.Constants.intNull
		sType = CStr(eRemoteDB.Constants.strNull)
		nBank = eRemoteDB.Constants.intNull
		nIntermed = eRemoteDB.Constants.intNull
		nRel_amoun = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		dCollect = eRemoteDB.Constants.dtmNull
		nPolicy = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nOffice = eRemoteDB.Constants.intNull
		dCompdate = eRemoteDB.Constants.dtmNull
		nUsercode = eRemoteDB.Constants.intNull
		sRel_Type = CStr(eRemoteDB.Constants.strNull)
		nProduct = eRemoteDB.Constants.intNull
		sStatus = CStr(eRemoteDB.Constants.strNull)
		sConwin = CStr(eRemoteDB.Constants.strNull)
		nCertif = eRemoteDB.Constants.intNull
		sClient = CStr(eRemoteDB.Constants.strNull)
		nUser_amend = eRemoteDB.Constants.intNull
		nCashnum = eRemoteDB.Constants.intNull
		nInputtyp = eRemoteDB.Constants.intNull
		nInsur_area = eRemoteDB.Constants.intNull
		nAgreement = eRemoteDB.Constants.intNull
		
		nAction = eRemoteDB.Constants.intNull
		dValueDate = eRemoteDB.Constants.dtmNull
		
		nPaidAmount = eRemoteDB.Constants.intNull
		nTotalAmount = eRemoteDB.Constants.intNull
		nDifference = eRemoteDB.Constants.intNull
		
		dDate_Benlar = eRemoteDB.Constants.dtmNull
		
		sKeyGenDoc = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%findColFormRef: Llena las propiedades de la tabla colformref para una relación especifica
	Public Function findColFormRef(ByVal nBordereaux As Double) As Boolean
		Dim lrecreaColFormRef As eRemoteDB.Execute
		
		On Error GoTo Err_findColFormRef
		
		lrecreaColFormRef = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.reaColFormRef'
		'+ Información leída el 24/11/2000 01:38:11 a.m.
		
		With lrecreaColFormRef
			.StoredProcedure = "reaColFormRef"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(True) Then
				If Not .EOF Then
					Me.blnError = True
					findColFormRef = True
					Me.nBordereaux = .FieldToClass("nBordereaux", 0)
					Me.sType = .FieldToClass("sType", String.Empty)
					Me.sRel_Type = .FieldToClass("sRel_type", String.Empty)
					Me.sStatus = .FieldToClass("sStatus", String.Empty)
					Me.sConwin = .FieldToClass("sConWin", String.Empty)
					Me.nIntermed = .FieldToClass("nIntermed", 0)
					Me.nRel_amoun = .FieldToClass("nRel_amoun", 0)
					Me.nCurrency = .FieldToClass("nCurrency", 0)
					Me.dCollect = .FieldToClass("dCollect")
					Me.nBranch = .FieldToClass("nBranch", 0)
					Me.nPolicy = .FieldToClass("nPolicy", 0)
					Me.nProduct = .FieldToClass("nProduct", 0)
					Me.nCertif = .FieldToClass("nCertif", 0)
					Me.nOffice = .FieldToClass("nOffice", 0)
					Me.sClient = .FieldToClass("sClient", String.Empty)
					Me.nUser_amend = .FieldToClass("nUser_amend", 0)
					Me.nInsur_area = .FieldToClass("nInsur_area", 0)
					Me.nInputtyp = .FieldToClass("nInputtyp", 0)
					Me.dValueDate = .FieldToClass("dValueDate")
					Me.nAgreement = .FieldToClass("nAgreement", eRemoteDB.Constants.intNull)
					Me.nCashnum = .FieldToClass("nCashNum", 0)
					Me.nBank = .FieldToClass("nBank", eRemoteDB.Constants.intNull)
					Me.sInd_Annuity = .FieldToClass("sInd_Annuity", "2")
					Me.dDate_Benlar = .FieldToClass("dDate_Benlar")
					Me.sRelOrigi = .FieldToClass("sRelOrigi")
					Me.sCollector_Name = .FieldToClass("sCollector_Name")
					Me.nCollector = .FieldToClass("nCollector")
					Me.sDesBank = .FieldToClass("sDesBank")
					Me.sDigit = .FieldToClass("sDigit")
					Me.sCliename = .FieldToClass("sCliename")
					Me.dCollectdate = .FieldToClass("dCollectdate")
					.RCloseRec()
				End If
			End If
		End With
		
Err_findColFormRef: 
		If Err.Number Then
			findColFormRef = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaColFormRef = Nothing
	End Function
	
	'%Delete: Esta rutina permite eliminar toda la información asociada a una relación.
	Public Function Delete(ByVal nBordereaux As Double, ByVal sDelete As String) As Boolean
		Dim lrecdelColFormRef As eRemoteDB.Execute
		
		On Error GoTo Err_Delete
		
		lrecdelColFormRef = New eRemoteDB.Execute
		
		With lrecdelColFormRef
			.StoredProcedure = "delColFormRef"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDELETE", sDelete, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Err_Delete: 
		If Err.Number Then
			Delete = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecdelColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelColFormRef = Nothing
	End Function
	
	'%DelPayDev: Esta rutina permite eliminar la información referente solamente a pagos y devoluciones asociada a una relación.
	Public Function DelPayDev(ByVal nBordereaux As Double) As Boolean
		Dim lrecdelColFormRef As eRemoteDB.Execute
		
		On Error GoTo DelPayDev_Err
		
		lrecdelColFormRef = New eRemoteDB.Execute
		
		With lrecdelColFormRef
			.StoredProcedure = "delColFormRefPayDev"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			DelPayDev = .Run(False)
		End With
		
DelPayDev_Err: 
		If Err.Number Then
			DelPayDev = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelColFormRef = Nothing
	End Function
	
	'% Add: Añade un registro en la tabla Colformref
	Public Function Add() As Boolean
		Dim lreccreColFormRef As eRemoteDB.Execute
		Dim ldblBordereaux As Double
		
		On Error GoTo Err_Add
		
		lreccreColFormRef = New eRemoteDB.Execute
		
		With lreccreColFormRef
			.StoredProcedure = "creColFormRef"
			.Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRel_amoun", nRel_amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOffice", nOffice, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRel_type", sRel_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", CollectionSeq.TypeStatusSeq.cstrNotComplete, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConWin", "332", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_Amend", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", IIf(nCashnum < 0, 0, nCashnum), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInputtyp", nInputtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_Annuty", sInd_Annuity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDate_Benlar", dDate_Benlar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", ldblBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectdate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				Me.sStatus = CStr(CollectionSeq.TypeStatusSeq.cstrNotComplete)
				Me.sConwin = "332"
				Me.nBordereaux = .Parameters("nBordereaux").Value
				Add = True
			End If
		End With
		
Err_Add: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreColFormRef = Nothing
	End Function
	
	'% UpdateUserAmend: Actualiza el campo nUseramend de la tabla
	Public Function UpdateUserAmend(ByVal nUserAmend As Integer, Optional ByVal dValueDate As Date = #12:00:00 AM#) As Boolean
		Dim lrecupdColFormRefUser As eRemoteDB.Execute
		
		On Error GoTo Err_UpdateUserAmend
		
		Me.nUser_amend = nUserAmend
		
		lrecupdColFormRefUser = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updColFormRefUser'
		'+ Información leída el 22/01/2001 03:53:29 p.m.
		
		With lrecupdColFormRefUser
			.StoredProcedure = "updColFormRefUser"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUser_amend", nUserAmend, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateUserAmend = .Run(False)
		End With
		
Err_UpdateUserAmend: 
		If Err.Number Then
			UpdateUserAmend = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdColFormRefUser may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdColFormRefUser = Nothing
	End Function
	
	'%UpdateCollect: Pasa toda la información de las tablas fijas involucradas
	' en la cobranza a sus repectivas tablas temporales par luego proceder a modificarlas
	Public Function UpdateCollect() As Boolean
		
		Dim lrecT_DocTyp As eRemoteDB.Execute
		
		On Error GoTo UpdateCollect_Err
		
		lrecT_DocTyp = New eRemoteDB.Execute
		
		With lrecT_DocTyp
			.StoredProcedure = "Instables_Temp_Collect"
			
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRel_Type", sRel_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRent_Vital", sInd_Annuity, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				UpdateCollect = True
				Me.sStatus = CStr(CollectionSeq.TypeStatusSeq.cstrNotComplete)
			End If
		End With
UpdateCollect_Err: 
		If Err.Number Then
			UpdateCollect = False
		End If
		
		'UPGRADE_NOTE: Object lrecT_DocTyp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecT_DocTyp = Nothing
		On Error GoTo 0
	End Function
	'%UpdateConWin: Permite actualizar el contenido de una ventana
	Public Function UpdateConWin() As Boolean
		Dim lrecupdColFormRefConWin As eRemoteDB.Execute
		
		On Error GoTo Err_UpdateConWin
		
		lrecupdColFormRefConWin = New eRemoteDB.Execute
		
		With lrecupdColFormRefConWin
			.StoredProcedure = "updColFormRefConWin"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConWin", sConwin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateConWin = .Run(False)
		End With
		
Err_UpdateConWin: 
		If Err.Number Then
			UpdateConWin = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdColFormRefConWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdColFormRefConWin = Nothing
	End Function
	
	'%UpdateType: Permite actualizar el campo sType de una relación.
	Public Function UpdateType(ByVal nBordereaux As Double, ByVal sType As TypeBordereaux) As Boolean
		Dim lrecColFormRef As eRemoteDB.Execute
		
		On Error GoTo Err_UpdateType
		
		lrecColFormRef = New eRemoteDB.Execute
		
		With lrecColFormRef
			.StoredProcedure = "updColFormRef_sType"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType", sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateType = .Run(False)
		End With
		
Err_UpdateType: 
		If Err.Number Then
			UpdateType = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColFormRef = Nothing
	End Function
	
	'% insPostCO001_K: Actualiza la tabla colformref
	Public Function insPostCO001_K(ByVal nAction As Integer, ByVal nInsur_area As Integer, ByVal nInputtyp As Integer, ByVal sRel_Type As String, ByVal nBank As Double, ByVal nCod_Agree As Integer, ByVal nAccount As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dCollectdate As Date, ByVal dValueDate As Date, ByVal nCurrency As Integer, ByVal nRelanum As Double, ByVal sStatus As String, ByVal nUsercode As Integer, ByVal nCashnum As Integer, ByVal sInd_Annuity As String, ByVal nCollector As Double, ByVal sRelOrigi As String, ByVal dCollect As Date) As Boolean
		On Error GoTo Err_insPostCO001_K
		
		Me.nAction = nAction
		Me.nInsur_area = nInsur_area
		Me.nInputtyp = nInputtyp
		Me.sRel_Type = sRel_Type
		Me.nBank = nBank
		
		If nBank > 0 Then
			Me.nAgreement = nAccount
		Else
			Me.nAgreement = nCod_Agree
		End If
		
		Me.dCollectdate = dCollect
		Me.nBranch = nBranch
		Me.nProduct = nProduct
		Me.nPolicy = nPolicy
		Me.nCertif = nCertif
		Me.sClient = sClient
		Me.dCollect = dCollectdate
		Me.dValueDate = dValueDate
		Me.nCurrency = nCurrency
		Me.nBordereaux = nRelanum
		Me.sStatus = sStatus
		Me.nUser_amend = nUsercode
		Me.nUsercode = nUsercode
		Me.nCashnum = nCashnum
		Me.sInd_Annuity = IIf(sInd_Annuity = "1", "1", "2")
		Me.nCollector = nCollector
		Me.sRelOrigi = sRelOrigi
		
		Select Case nAction
			
			'+Si la opción seleccionada es Registrar
			Case TypeActionsSeqColl.cstrAdd
				insPostCO001_K = Add()
				
				'+Si la opción seleccionada es Eliminar
			Case TypeActionsSeqColl.cstrCut
				insPostCO001_K = Delete(nBordereaux, "delete")
				
				'+Si la opción seleccionada es Recuperar
			Case TypeActionsSeqColl.cstrUpdate
				insPostCO001_K = UpdateUserAmend(nUsercode, dCollectdate)
				
				'+Si la opción seleccionada es Actualizar
			Case TypeActionsSeqColl.cstrModify
				insPostCO001_K = UpdateCollect()
				
			Case Else
				insPostCO001_K = True
		End Select
		
Err_insPostCO001_K: 
		If Err.Number Then
			insPostCO001_K = False
		End If
		On Error GoTo 0
	End Function
	
	'% insvalCO001_K: Realiza la validación de los campos a actualizar en la ventana CO001_k
	Public Function insvalCO001_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nInsurArea As Integer, ByVal nInputtyp As Integer, ByVal sRel_Type As String, ByVal nBank As Double, ByVal nCod_Agree As Integer, ByVal nAccount As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dCollectdate As Date, ByVal nCurrency As Integer, ByVal nRelanum As Double, ByVal nUsercode As Integer, ByVal nCashnum As Integer, ByVal nCollector As Double, ByVal dCollect As Date) As String
		Dim lclsError As eFunctions.Errors
		Dim lobjErrors As eFunctions.Errors
		Dim lblnError As Boolean
		Dim lstrErrors As String
		
		On Error GoTo insvalCO001_K_Err
		
		lclsError = New eFunctions.Errors
		
		With lclsError
			
			'+ Se efectua las validaciones concernientes a la operación a través de
			
			'+Validaciones que se realizan el la BD
			lstrErrors = InsValCO001_KDB(nAction, nInsurArea, nInputtyp, sRel_Type, nBank, nCod_Agree, nAccount, nBranch, nProduct, nPolicy, nCertif, sClient, dCollectdate, nCurrency, nRelanum, nUsercode, nCashnum, nCollector, 1, dCollect)
			
			Call lclsError.ErrorMessage(sCodispl,  ,  ,  ,  ,  , lstrErrors)
			
			insvalCO001_K = .Confirm
		End With
		
insvalCO001_K_Err: 
		If Err.Number Then
			insvalCO001_K = insvalCO001_K & Err.Description
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
	End Function
	
	'%InsValCO001_KDB: Este metodo se encarga de realizar las validaciones que son accesando la BD
	'%                 descritas en el funcional de la ventana "CO001_K"
	Private Function InsValCO001_KDB(ByVal nAction As Integer, ByVal nInsurArea As Integer, ByVal nInputtyp As Integer, ByVal sRel_Type As String, ByVal nBank As Double, ByVal nCod_Agree As Integer, ByVal nAccount As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal dCollectdate As Date, ByVal nCurrency As Integer, ByVal nRelanum As Double, ByVal nUsercode As Integer, ByVal nCashnum As Integer, ByVal nCollector As Double, ByVal nType_proce As Integer, ByVal dCollect As Date) As String
		Dim lrecInsValCO001_KDB As eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'InsValOP001'
		'+Información leída el 10/04/2003
		
		On Error GoTo InsValCO001_KDB_Err
		lrecInsValCO001_KDB = New eRemoteDB.Execute
		
		With lrecInsValCO001_KDB
			.StoredProcedure = "InsValCO001_K"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_Area", nInsurArea, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInputtyp", nInputtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRel_Type", sRel_Type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_Agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAccount", nAccount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollectdate", dCollectdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nRelanum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashnum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollector", nCollector, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_proce", nType_proce, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dCollect", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				InsValCO001_KDB = .Parameters("Arrayerrors").Value
			End If
		End With
		
InsValCO001_KDB_Err: 
		If Err.Number Then
			InsValCO001_KDB = "InsValCO001_KDB: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecInsValCO001_KDB may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValCO001_KDB = Nothing
		On Error GoTo 0
	End Function
	
	
	'% calTotals: Calcula los totales para mostrar en la secuencia de Cobranzas
	Public Function calTotals() As Boolean
		
		Dim ldblGeneric As Double
		Dim lreccalCollectionAmounts As eRemoteDB.Execute
		
		On Error GoTo Err_calTotals
		
		lreccalCollectionAmounts = New eRemoteDB.Execute
		
		With lreccalCollectionAmounts
			.StoredProcedure = "calCollectionAmounts"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterest", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterestDec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			nPaidAmount = System.Math.Round(CDbl(.Parameters("nTotalCO008").Value) + (CDbl(.Parameters("nTotalCO010").Value) * -1), 6)
			nTotalAmount = System.Math.Round(CDbl(.Parameters("nTotalCO001").Value) + CDbl(.Parameters("nTotalInterest").Value), 6)
			nDifference = System.Math.Round(CDbl(.Parameters("nTotalCO012").Value), 6)
			nDiffTotal = System.Math.Round(System.Math.Abs(CDbl(.Parameters("nTotalCO001").Value)) + CDbl(.Parameters("nTotalInterest").Value) - (CDbl(.Parameters("nTotalCO008").Value) + CDbl(.Parameters("nTotalCO010").Value)), 6)
			
			'+Montos con decimales
			nPaidAmountDec = System.Math.Round(CDbl(.Parameters("nTotalCO008Dec").Value) + (CDbl(.Parameters("nTotalCO010").Value) * -1), 6)
			nTotalAmountDec = System.Math.Round(CDbl(.Parameters("nTotalCO001Dec").Value) + CDbl(.Parameters("nTotalInterestDec").Value), 6)
			nDifferenceDec = System.Math.Round(CDbl(.Parameters("nTotalCO012Dec").Value), 6)
			nDiffTotalDec = System.Math.Round(System.Math.Abs(CDbl(.Parameters("nTotalCO001Dec").Value)) + CDbl(.Parameters("nTotalInterestDec").Value) - (CDbl(.Parameters("nTotalCO008Dec").Value) + CDbl(.Parameters("nTotalCO010Dec").Value)), 6)
			
		End With
		
		'UPGRADE_NOTE: Object lreccalCollectionAmounts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccalCollectionAmounts = Nothing
		
Err_calTotals: 
		If Err.Number Then
			calTotals = False
			On Error GoTo 0
		End If
		
	End Function
	
	'%insValFolder: Esta función valida que todas las pestañas de entrada obligatoria hallan sido
	'%rellenadas.
	Public Function insValFolder(ByVal nBordereaux As Double) As String
		Dim lreccalCollectionAmounts As eRemoteDB.Execute
		Dim lclsError As Object
		Dim ldblBalance As Double
		Dim ldblDifTotal As Double
		Dim lblnOk As Boolean
		
        lclsError = New eFunctions.Errors
        mobjOpt_Premiu = New eGeneral.opt_premiu
		
		Call findColFormRef(nBordereaux)
		
		lblnOk = True
		
		'+ Se verifican que no existan ventanas requeridas según el tipo de relación.
		If nRel_amoun >= 0 Then
			If Mid(sConwin, 1, 1) = "3" Or Mid(sConwin, 2, 1) = "3" Or Mid(sConwin, 3, 1) = "3" Then
				lblnOk = False
			End If
		Else
			If Mid(sConwin, 1, 1) = "3" Or Mid(sConwin, 2, 1) = "3" Then
				lblnOk = False
			End If
		End If
		
		'+ Si existen ventanas requeridas se envía mensaje respectivo si no se continua con el proceso.
		If Not lblnOk Then
			lclsError.ErrorMessage("CO001_K", 705003)
        Else
            Dim nLimit As Double

            lreccalCollectionAmounts = New eRemoteDB.Execute

            ldblBalance = getBalanceRelation(nBordereaux, Me.sStatus, Me.dCollect, Me.dValueDate, Me.sRelOrigi)

            '+ Si existe un sobrante de dinero
            If ldblBalance > 0 Then

                nLimit = mobjOpt_Premiu.GETUPPER_LIMEXC(Me.dValueDate, 1, mobjOpt_Premiu.nUpper_lim)


                '+ se verifica que ese sobrante esté entre los límites de tolerancia (para el sobrante)
                If System.Math.Abs(ldblBalance) > nLimit Then
                    ldblDifTotal = System.Math.Abs(ldblBalance) - nLimit
                    lclsError.ErrorMessage("CO001_K", 750077, , eFunctions.Errors.TextAlign.RigthAling, " (" & ldblDifTotal & ")")
                End If
            End If

            '+ Si existe un faltante de dinero
            If ldblBalance < 0 Then

                nLimit = mobjOpt_Premiu.GETLOWER_LIMEXC(Me.dValueDate, 1, mobjOpt_Premiu.nLower_lim)

                '+ se verifica que ese faltante esté entre los límites de tolerancia (para el faltante)
                If System.Math.Abs(ldblBalance) > nLimit Then
                    ldblDifTotal = System.Math.Abs(ldblBalance) - nLimit
                    lclsError.ErrorMessage("CO001_K", 750073, , eFunctions.Errors.TextAlign.RigthAling, " (" & ldblDifTotal & ")")
                End If
            End If
		End If
		
		insValFolder = lclsError.Confirm
		
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
	End Function
	
	'%insPostFolder: Actualización de la secuencia de cobranzas
	Public Function insPostFolder(ByVal nBordereaux As Double, ByVal nCashnum As Integer, ByVal nUsercode As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nReceipt As Double, ByVal nAction As Short) As Boolean
		Dim lclsColformRef As eCollection.ColformRef
		Dim lreccalCollectionAmounts As eRemoteDB.Execute
		Dim lrecUpdFinalsCollect As eRemoteDB.Execute
		Dim ldblGeneric As Double
        Dim lstrKey As String = ""

        On Error GoTo Err_handler
		
		lclsColformRef = New eCollection.ColformRef
		
		lclsColformRef.findColFormRef(nBordereaux)
		
		lreccalCollectionAmounts = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.calCollectionAmounts'
		'+ Información leída el 15/02/2001 13:49:27
		
		With lreccalCollectionAmounts
			.StoredProcedure = "calCollectionAmounts"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", lclsColformRef.sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", lclsColformRef.dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", lclsColformRef.dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterest", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterestDec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", lclsColformRef.sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
		End With
		
		lrecUpdFinalsCollect = New eRemoteDB.Execute
		
		With lrecUpdFinalsCollect
			.StoredProcedure = "UpdFinalsRelation"
			
			.Parameters.Add("dCollectDate", lclsColformRef.dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sType", lclsColformRef.sType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001", lreccalCollectionAmounts.Parameters("nTotalCO001").Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008", lreccalCollectionAmounts.Parameters("nTotalCO008").Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010", lreccalCollectionAmounts.Parameters("nTotalCO010").Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012", lreccalCollectionAmounts.Parameters("nTotalCO012").Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterest", lreccalCollectionAmounts.Parameters("nTotalInterest").Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCashNum", nCashnum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInputTyp", lclsColformRef.nInputtyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", lclsColformRef.nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBank", lclsColformRef.nBank, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAgreement", lclsColformRef.nAgreement, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", lclsColformRef.dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sKeyGenDoc", lstrKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", lclsColformRef.sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmountDif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostFolder = .Run(False)
			Me.sKeyGenDoc = .Parameters("sKeyGenDoc").Value
			Me.nAmountDif = .Parameters("nAmountDif").Value
			
			If insPostFolder Then
				lclsColformRef.findColFormRef(nBordereaux)
				sStatus = lclsColformRef.sStatus
			End If
			
		End With
		
Err_handler: 
		If Err.Number Then
			insPostFolder = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lreccalCollectionAmounts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccalCollectionAmounts = Nothing
		'UPGRADE_NOTE: Object lrecUpdFinalsCollect may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecUpdFinalsCollect = Nothing
		'UPGRADE_NOTE: Object lclsColformRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsColformRef = Nothing
	End Function
	
	'%getBalanceRelation: Obtiene el recibo/cuota anterior pendiente (recibo/cuota más antiguo para el pago)
	Public Function getBalanceRelation(ByVal nBordereaux As Double, ByVal sStatus As String, ByVal dCollect As Date, ByVal dValueDate As Date, ByVal sRelOrigi As String) As Double
		Dim lreccalCollectionAmounts As eRemoteDB.Execute
		Dim lstrDocument As String
		Dim ldblGeneric As Double
		
		On Error GoTo getBalanceRelation_Err
		
		lreccalCollectionAmounts = New eRemoteDB.Execute
		
		getBalanceRelation = 0
		
		With lreccalCollectionAmounts
			.StoredProcedure = "calCollectionAmounts"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterest", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterestDec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			getBalanceRelation = System.Math.Round((System.Math.Abs(.Parameters("nTotalCO001").Value)) + CDbl(.Parameters("nTotalCO012").Value) + CDbl(.Parameters("nTotalInterest").Value) - (CDbl(.Parameters("nTotalCO008").Value) + CDbl(.Parameters("nTotalCO010").Value)), 6)
			
		End With
getBalanceRelation_Err: 
		If Err.Number Then
			getBalanceRelation = 0
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lreccalCollectionAmounts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccalCollectionAmounts = Nothing
	End Function
	
	'%getnDiffTotalRelation: Obtiene el recibo/cuota anterior pendiente (recibo/cuota más antiguo para el pago)
	Public Function getnDiffTotalRelation(ByVal nBordereaux As Double, ByVal sStatus As String, ByVal dCollect As Date, ByVal dValueDate As Date, ByVal sRelOrigi As String) As Double
		Dim lreccalCollectionAmounts As eRemoteDB.Execute
		Dim lstrDocument As String
		Dim ldblGeneric As Double
		
		On Error GoTo getnDiffTotalRelation_Err
		
		lreccalCollectionAmounts = New eRemoteDB.Execute
		
		getnDiffTotalRelation = 0
		
		With lreccalCollectionAmounts
			.StoredProcedure = "calCollectionAmounts"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatus", sStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecDate", dCollect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dValueDate", dValueDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO001Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO008Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO010Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalCO012Dec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterest", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTotalInterestDec", ldblGeneric, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRelOrigi", sRelOrigi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			getnDiffTotalRelation = CDbl(System.Math.Abs(.Parameters("nTotalCO001").Value)) + CDbl(.Parameters("nTotalInterest").Value) - (CDbl(.Parameters("nTotalCO008").Value) + CDbl(.Parameters("nTotalCO010").Value))
			
		End With
getnDiffTotalRelation_Err: 
		If Err.Number Then
			getnDiffTotalRelation = 0
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lreccalCollectionAmounts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccalCollectionAmounts = Nothing
	End Function
	
	
	'**%UpdateConWinPos: This method updates the array that contains the sequence of windows to place the contents
	'%UpdateConWinPos: Este metodo actualiza el arreglo que contiene la sequencia de ventanas para colocar el contenido de una ventana en particular.
	Public Function UpdateConWinPos(ByVal nBordereaux As Double, ByVal nPos As Integer, ByVal sStatus As String) As Boolean
		Dim sBefore As String
		Dim sAfter As String
		
		If findColFormRef(nBordereaux) Then
			If nPos > 1 Then
				sBefore = Left(sConwin, nPos - 1)
			Else
				sBefore = String.Empty
			End If
			If nPos < Len(sConwin) Then
				sAfter = Mid(sConwin, nPos + 1)
			Else
				sAfter = String.Empty
			End If
			
			sConwin = sBefore & sStatus & sAfter
			UpdateConWinPos = UpdateConWin
			UpdateConWinPos = True
		Else
			UpdateConWinPos = False
		End If
		
	End Function
	
	'%UpdateRel_Amoun: Permite actualizar el contenido de una ventana
	Public Function UpdateRel_Amoun() As Boolean
		Dim lrecupdColFormRefConWin As eRemoteDB.Execute
		
		On Error GoTo Err_UpdateRel_Amoun
		
		lrecupdColFormRefConWin = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.updColFormRefConWin'
		'+ Información leída el 06/02/1999 9:13:07
		
		With lrecupdColFormRefConWin
			.StoredProcedure = "updColFormRefRel_amoun"
			
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRel_Amoun", nRel_amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			UpdateRel_Amoun = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecupdColFormRefConWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdColFormRefConWin = Nothing
		
Err_UpdateRel_Amoun: 
		If Err.Number Then
			UpdateRel_Amoun = False
			On Error GoTo 0
		End If
		
	End Function
	
	'%valExistsCO001_K: Verifica si existe información para procesar según condición de filtro de la transacción CO001_K.
	Public Function valExistsCO001_K(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sClient As String, ByVal nCod_Agree As Integer, ByVal nInsur_area As Integer) As Boolean
		Dim lrecvalExistsCO001_K As eRemoteDB.Execute
		Dim llngExists As Integer
		
		On Error GoTo valExistsCO001_K_Err
		
		lrecvalExistsCO001_K = New eRemoteDB.Execute
		
		'+
		'+ Definición de store procedure valExistsco001_k al 02-09-2002 14:31:52
		'+
		With lrecvalExistsCO001_K
			.StoredProcedure = "valExistsco001_k"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCod_agree", nCod_Agree, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInsur_area", nInsur_area, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExists", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			valExistsCO001_K = (.Parameters("nExists").Value = 1)
		End With
		
valExistsCO001_K_Err: 
		If Err.Number Then
			valExistsCO001_K = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecvalExistsCO001_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecvalExistsCO001_K = Nothing
	End Function
	
	'%valRevRelAll: Verifica si es posible reversar toda la relación. True si es posible; False no.
	Public Function valRevRelAll(ByVal nBordereaux As Double) As Boolean
		Dim lrecTRelDoc As eRemoteDB.Execute
		Dim llngExists As Integer
		Dim lintIndRevAll As Integer
		
		On Error GoTo valRevRelAll_Err
		
		lrecTRelDoc = New eRemoteDB.Execute
		
		valRevRelAll = True
		'+
		'+ Definición de store procedure valRevRelAll al 02-09-2002 14:31:52
		'+
		With lrecTRelDoc
			.StoredProcedure = "valRevRelAll"
			.Parameters.Add("nBordeeraux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExist", llngExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nIndRevAll", lintIndRevAll, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nExists").Value = 1 Then
					If .Parameters("nIndRevAll").Value = 1 Then
						valRevRelAll = False
					End If
				End If
			End If
		End With
		
valRevRelAll_Err: 
		If Err.Number Then
			valRevRelAll = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecTRelDoc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTRelDoc = Nothing
	End Function
	
	'%valTRelDoc_Only: Verifica si solamnete está asociado un documento a la relación.
	Public Function valTRelDoc_Only(ByVal nBordereaux As Double) As Boolean
		Dim lrecTRelDoc As eRemoteDB.Execute
		
		On Error GoTo valTRelDoc_Only_Err
		
		lrecTRelDoc = New eRemoteDB.Execute
		
		valTRelDoc_Only = True
		'+
		'+ Definición de store procedure valTRelDoc_Only al 02-09-2002 14:31:52
		'+
		With lrecTRelDoc
			.StoredProcedure = "reaTRelDoc_Count"
			.Parameters.Add("nBordeeraux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			If .Run(False) Then
				If .Parameters("nCount").Value > 1 Then
					valTRelDoc_Only = False
				End If
			End If
		End With
		
valTRelDoc_Only_Err: 
		If Err.Number Then
			valTRelDoc_Only = False
			On Error GoTo 0
		End If
		'UPGRADE_NOTE: Object lrecTRelDoc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecTRelDoc = Nothing
	End Function
	
	'%findColFormRefPar: Llena las propiedades de la tabla colformref para una relación especifica
	Public Function findColFormRefPar(ByVal nBordereaux As Double) As Boolean
		Dim lrecColFormRef As eRemoteDB.Execute
		
		On Error GoTo Err_findColFormRefPar
		
		lrecColFormRef = New eRemoteDB.Execute
		
		With lrecColFormRef
			.StoredProcedure = "reaColFormRefPar"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				findColFormRefPar = True
				Me.nBranch = .FieldToClass("nBranch", 0)
				Me.nPolicy = .FieldToClass("nPolicy", 0)
				Me.nProduct = .FieldToClass("nProduct", 0)
				Me.nCertif = .FieldToClass("nCertif", 0)
				Me.sClient = .FieldToClass("sClient", String.Empty)
				Me.nAgreement = .FieldToClass("nAgreement", eRemoteDB.Constants.intNull)
				Me.sConwin = .FieldToClass("sConWin", String.Empty)
				Me.nCollector = .FieldToClass("nCollector", String.Empty)
				Me.sCollector_Name = .FieldToClass("sCollector_Name", String.Empty)
				.RCloseRec()
			End If
		End With
		
Err_findColFormRefPar: 
		If Err.Number Then
			findColFormRefPar = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColFormRef = Nothing
	End Function
	
	'%getConWinRel: Llena las propiedades de la tabla colformref para una relación especifica
	Public Function getConWinRel(ByVal nBordereaux As Double) As String
		Dim lrecColFormRef As eRemoteDB.Execute
        Dim lstrConWin As String = ""

        On Error GoTo Err_getConWinRel
		
		lrecColFormRef = New eRemoteDB.Execute
		
		With lrecColFormRef
			.StoredProcedure = "getConWinRel"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sConWin", lstrConWin, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 3, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			getConWinRel = Trim(.Parameters("sConWin").Value)
		End With
		
Err_getConWinRel: 
		If Err.Number Then
			getConWinRel = String.Empty
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecColFormRef = Nothing
	End Function
	
	'%Rea_RelAmoun: Calcula la suma de los montos de los documentos de una relación a una fecha de valorización dada
	Public Function Rea_RelAmoun(ByVal nBordereaux As Double, ByVal dDateIncrease As Date, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer) As Double
		Dim lrecRea_RelAmoun As eRemoteDB.Execute
		
		On Error GoTo Err_Rea_RelAmoun
		
		lrecRea_RelAmoun = New eRemoteDB.Execute
		
		With lrecRea_RelAmoun
			.StoredProcedure = "Rea_RelAmoun"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIncrease", dDateIncrease, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRel_Amoun", nRel_amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Rea_RelAmoun = .Parameters("nRel_Amoun").Value
			Me.dValueDate = .Parameters("dDateIncrease").Value
		End With
		
Err_Rea_RelAmoun: 
		If Err.Number Then
			Rea_RelAmoun = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRea_RelAmoun may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRea_RelAmoun = Nothing
	End Function
	
	'%Rea_RelAmoun: Calcula los ingresos por una relacion si se verifica todos los documentos o
	'%              la suma de los montos de los documentos de una relación a una fecha de valorización dada
	'%              si se indica recibo u cutoa de financiamiento
	Public Function Rea_RelAmount_1(ByVal nBordereaux As Double, ByVal dDateIncrease As Date, ByVal nReceipt As Double, ByVal nContrat As Double, ByVal nDraft As Integer) As Double
		Dim lrecRea_RelAmoun As eRemoteDB.Execute
		
		On Error GoTo Err_Rea_RelAmoun
		
		lrecRea_RelAmoun = New eRemoteDB.Execute
		
		With lrecRea_RelAmoun
			.StoredProcedure = "Rea_RelAmount_1"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nReceipt", nReceipt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nContrat", nContrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIncrease", dDateIncrease, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRel_Amoun", nRel_amoun, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 2, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Rea_RelAmount_1 = .Parameters("nRel_Amoun").Value
			Me.dValueDate = .Parameters("dDateIncrease").Value
		End With
		
Err_Rea_RelAmoun: 
		If Err.Number Then
			Rea_RelAmount_1 = 0
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecRea_RelAmoun may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecRea_RelAmoun = Nothing
	End Function
	
	'% FindColFormRefCO788: Obtiene la información de la relación para la transacción CO788
	Public Function FindColFormRefCO788(ByVal nBordereaux As Double, ByVal nCollecDocTyp As Integer, ByVal nDocument As Double, ByVal nDraft As Integer, ByVal nLoans As Double) As Boolean
		Dim lrecreaColFormRef As eRemoteDB.Execute
		
		On Error GoTo FindColFormRefCO788_Err
		
		lrecreaColFormRef = New eRemoteDB.Execute
		'+
		'+ Definición de store procedure reaPremiumexpirdat_pend al 11-13-2002 16:32:55
		'+
		With lrecreaColFormRef
			.StoredProcedure = "InsCO788pkg.ReaColFormRef"
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDocTyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLoans", nLoans, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.nBordereaux = .FieldToClass("nBordereaux")
				Me.sRel_Type = .FieldToClass("sRel_Type")
				Me.sDesRel_Type = .FieldToClass("sDesRel_Type")
				Me.dCollect = .FieldToClass("dCollect")
				Me.nAgreement = .FieldToClass("nAgreement")
				Me.nBank = .FieldToClass("nBank")
				Me.sDesBank = .FieldToClass("sDesBank")
				Me.nRel_amoun = .FieldToClass("nRel_amoun")
				Me.nqDocs = .FieldToClass("nqDocs")
				Me.nAmountDoc = .FieldToClass("nAmountDoc")
				Me.nSequence = .FieldToClass("nSequence")
				Me.sClient = .FieldToClass("sClient")
				Me.sCliename = .FieldToClass("sCliename")
				Me.sDigit = .FieldToClass("sDigit")
				Me.dValueDate = .FieldToClass("dValueDate")
				FindColFormRefCO788 = True
			Else
				FindColFormRefCO788 = False
			End If
		End With
		
FindColFormRefCO788_Err: 
		If Err.Number Then
			FindColFormRefCO788 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaColFormRef may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaColFormRef = Nothing
	End Function
	
	'%InsValCO788: Este metodo se encarga de realizar las validaciones que son accesando la BD
	'%             descritas en el funcional de la ventana "CO788"
	Public Function InsValCO788(ByVal dDate As Date, ByVal nCollecDocTyp As Integer, ByVal nNumDoc As Double, ByVal nDraft As Integer, ByVal nBordereaux As Double, ByVal dDateIncrease As Date, ByVal sClient As String, ByVal nOptDev As Integer, ByVal nOptDocRev As Integer, ByVal nSequence As Integer, ByVal nUsercode As Integer) As String
		Dim lrecInsValCO788 As eRemoteDB.Execute
		Dim lclsError As eFunctions.Errors
        Dim lstrErrors As String = ""

        On Error GoTo InsValCO788_Err
		
		lrecInsValCO788 = New eRemoteDB.Execute
		lclsError = New eFunctions.Errors
		
		With lrecInsValCO788
			.StoredProcedure = "InsCO788pkg.InsValCO788"
			.Parameters.Add("dDate", dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDoctyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumDoc", nNumDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIncrease", dDateIncrease, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOptDev", nOptDev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOptDocRev", nOptDocRev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				lstrErrors = .Parameters("Arrayerrors").Value
			End If
			Call lclsError.ErrorMessage("CO788",  ,  ,  ,  ,  , lstrErrors)
			InsValCO788 = lclsError.Confirm
		End With
		
InsValCO788_Err: 
		If Err.Number Then
			InsValCO788 = "InsValCO788: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lrecInsValCO788 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsValCO788 = Nothing
		'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsError = Nothing
		On Error GoTo 0
	End Function
	
	'%InsPostCO788: Este metodo se encarga de realizar las actualizaciones pertinentes a la transacción CO788
	Public Function InsPostCO788(ByVal dDate As Date, ByVal nCollecDocTyp As Integer, ByVal nNumDoc As Double, ByVal nDraft As Integer, ByVal nBordereaux As Double, ByVal dDateIncrease As Date, ByVal sClient As String, ByVal nOptDev As Integer, ByVal nOptDocRev As Integer, ByVal nUsercode As Integer, ByVal nSequence As Integer) As Boolean
		Dim lrecInsPostCO788 As eRemoteDB.Execute
		
		On Error GoTo InsPostCO788_Err
		
		lrecInsPostCO788 = New eRemoteDB.Execute
		
		InsPostCO788 = False
		
		With lrecInsPostCO788
			.StoredProcedure = "InsCO788pkg.InsPostCO788"
			.Parameters.Add("dDate", dDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCollecDoctyp", nCollecDocTyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNumDoc", nNumDoc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDraft", nDraft, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBordereaux", nBordereaux, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dDateIncrease", dDateIncrease, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOptDev", nOptDev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOptDocRev", nOptDocRev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nSequence", nSequence, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				InsPostCO788 = True
			End If
		End With
		
InsPostCO788_Err: 
		If Err.Number Then
			InsPostCO788 = False
		End If
		'UPGRADE_NOTE: Object lrecInsPostCO788 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsPostCO788 = Nothing
		On Error GoTo 0
	End Function
End Class






