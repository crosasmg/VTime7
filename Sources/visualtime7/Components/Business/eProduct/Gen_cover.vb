Option Strict Off
Option Explicit On
Imports eFunctions.Extensions
Public Class Gen_cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Gen_cover.cls                            $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 28/04/04 11.31                               $%'
	'% $Revision:: 53                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Columnas segun tabla en el sistema al 04/03/2001
	'   Column_name                Type                 Length      Prec  Scale Nullable TrimTrailingBlanks                  FixedLenNullInSource
	'----------------------------- -------------------- ----------- ----- ----- -------- ----------------------------------- --------------------
	Public nModulec As Integer 'smallint 2           5     0     no       (n/a)                               (n/a)
	Public nBranch As Integer 'smallint 2           5     0     no       (n/a)                               (n/a)
	Public nCover As Integer 'smallint 2           5     0     no       (n/a)                               (n/a)
	Public nProduct As Integer 'smallint 2           5     0     no       (n/a)                               (n/a)
	Public dEffecdate As Date 'datetime 8                       no       (n/a)                               (n/a)
	Public sAddReini As String 'char     1                       yes      no                                  yes
	Public nBranch_led As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public sAddSuini As String 'char     1                       yes      no                                  yes
	Public nBranch_est As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public sAddTaxin As String 'char     1                       yes      no                                  yes
	Public sAutomrep As String 'char     1                       yes      no                                  yes
	Public nBill_item As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nBranch_gen As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nBranch_rei As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nCacalcov As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nCacalfix As Double 'decimal  9           12    0     yes      (n/a)                               (n/a)
	Public sCacalfri As String 'char     1                       yes      no                                  yes
	Public sCacalili As String 'char     1                       yes      no                                  yes
	Public nCacalmax As Double 'decimal  9           12    0     yes      (n/a)                               (n/a)
	Public nCacalper As Double 'decimal  5           5     2     yes      (n/a)                               (n/a)
	Public sCacalrei As String 'char     1                       yes      no                                  yes
	Public nRateCapAdd As Double 'decimal  5           6     2     yes      (n/a)                               (n/a)
	Public nRateCapSub As Double 'decimal  5           6     2     yes      (n/a)                               (n/a)
	Public sCh_typ_cap As String 'char     1                       yes      no                                  yes
	Public nRatePreAdd As Double 'decimal  5           6     2     yes      (n/a)                               (n/a)
	Public nRatePreSub As Double 'decimal  5           6     2     yes      (n/a)                               (n/a)
	Public sChange_typ As String 'char     1                       yes      no                                  yes
	Public dCompdate As Date 'datetime 8                       yes      (n/a)                               (n/a)
	Public nCover_in As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nCoverapl As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nCovergen As Integer 'smallint 2           5     0     no       (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public sDefaulti As String 'char     1                       yes      no                                  yes
	Public sFrancApl As String 'char     1                       yes      no                                  yes
	Public nFrancFix As Double 'decimal  9           10    0     yes      (n/a)                               (n/a)
	Public nFrancMax As Double 'decimal  9           10    0     yes      (n/a)                               (n/a)
	Public nFrancMin As Double 'decimal  9           10    0     yes      (n/a)                               (n/a)
	Public nFrancrat As Double 'decimal  5           4     2     yes      (n/a)                               (n/a)
	Public sFrantype As String 'char     1                       yes      no                                  yes
	Public nMedreser As Double 'decimal  9           12    0     yes      (n/a)                               (n/a)
	Public nNotenum As Integer 'int      4           10    0     yes      (n/a)                               (n/a)
	Public dNulldate As Date 'datetime 8                       yes      (n/a)                               (n/a)
	Public nPremifix As Double 'decimal  9           10    2     yes      (n/a)                               (n/a)
	Public nPremimax As Double 'decimal  9           10    2     yes      (n/a)                               (n/a)
	Public nPremimin As Double 'decimal  9           10    2     yes      (n/a)                               (n/a)
	Public nPremirat As Double 'decimal  5           9     6     yes      (n/a)                               (n/a)
	Public sRequire As String 'char     1                       yes      no                                  yes
	Public sRoucapit As String 'char     12                      yes      no                                  yes
	Public sRoufranc As String 'char     12                      yes      no                                  yes
	Public sRoupremi As String 'char     12                      yes      no                                  yes
	Public sRoureser As String 'char     12                      yes      no                                  yes
	Public sStatregt As String 'char     1                       yes      no                                  yes
	Public nUsercode As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nChCapLev As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nChPreLev As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nCacalmin As Double 'decimal  9           12    0     yes      (n/a)                               (n/a)
	Public sFDRequire As String 'char     1                       yes      no                                  yes
	Public sFDChantyp As String 'char     1                       yes      no                                  yes
	Public nFDUserLev As Integer 'smallint 2           5     0     yes      (n/a)                               (n/a)
	Public nFDRateAdd As Double 'decimal  5           6     2     yes      (n/a)                               (n/a)
	Public nFDRateSub As Double 'decimal  5           6     2     yes      (n/a)                               (n/a)
	Public sInd_Med_Exp As String 'char     1                       yes      yes                                  yes
	Public nApply_Perc As Double 'decimal  5           5     2     yes      (n/a)                               (n/a)
	Public sRou_verify As String 'char     12                      yes      no                                  yes
	Public nRetarif As Integer 'smallint     5                      yes      no                                  yes
	Public sBas_sumins As String
	Public sReinorigcond As String
	Public sCondSVS As String
    Public nId_table As Integer
    Public nFrancRatCla As Double
    Public nFrancFixCla As Double
    Public nFranxMinCla As Double
    Public nFranxMaxCla As Double
    Public sRouFrancCla As String
    Public sRASA_routine As String
    Public sIndManualDeductible As String
    Public nFrancDays As String
    Public sCoveruse As String
	'+ Variables auxiliares
	
	'- Descripción de la cobertura
	Public sDescript As String
	
	'- Variables necesarias el mantenimiento de la la tabla gen_cover por medio
    '- de la ventana DP052
	Private mstrCacalrei As String
	Private mstrCh_typ_cap As String
	Private mstrAddSuini As String
	Private mstrAddReini As String
	Private mstrAddTaxin As String
	Private mstrRevalapl As String
	Private mblnAddErr As Boolean
	Private mblnBigErr As Boolean
    Public sRevalapl As String

    Public sPrint_capital As String
    Public nPrint_order As Integer

    '- Constantes para el número posible de frames en la subsecuencia de Coberturas.
    Private Const CN_FRAMESNUMGENCOVER As Integer = 7
	Private Const CN_FRAMESNUMLIFECOVER As Integer = 3
	
	'- Se define la constante para los codispl en la subsecuencia de cobertura (Cob. genéricas)
    Private Const CN_WINDOWSGENCOV As String = "DP034   DP052   DP052A  DP035   DP035A  DP049   DP7002"
	
	'- Se define la constante para los codispl en la subsecuencia de cobertura (Cob. VIDA)
	Private Const CN_WINDOWSLIFECOV As String = "DP018P  DP50BP  DP049   "
	
	'- Variables necesarias el mantenimiento de la la tabla gen_cover por medio
	'- de la ventana DP052A
	Private mblnFindExist As Boolean
	Private mblnCapitalBasic As Boolean
	Private mblnOtherCover As Boolean
	Private mstrOtherCover As String
	
	'% Update_Status: actualiza el estado de la cobertura
	Public Function Update_Status(ByVal sBrancht As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sStatregt As String) As Boolean
		Dim lclsLife_cover As Life_cover
		
		On Error GoTo Update_Status_err
		
		Update_Status = False
		
        'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
            lclsLife_cover = New Life_cover
            With lclsLife_cover
                If .Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
                    .sStatregt = sStatregt
                    Update_Status = .Update
                End If
            End With
        Else
            If Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
                Me.sStatregt = sStatregt
                Update_Status = Update()
            End If
        End If

Update_Status_err:
        If Err.Number Then
            Update_Status = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife_cover = Nothing
    End Function
	
	'% Update_DP033: actualiza los datos de la ventana DP033
	Public Function Update_DP033() As Boolean
		Dim lrecinsGen_coverDP033 As eRemoteDB.Execute
		
		On Error GoTo Update_DP033_err
		
		lrecinsGen_coverDP033 = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insGen_coverDP033'
		'+ Información leída el 03/04/2001 11:40:37 a.m.
		
		With lrecinsGen_coverDP033
			.StoredProcedure = "insGen_coverDP033"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led1", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est1", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen1", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei1", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomrep1", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix1", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalfri1", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalili1", sCacalili, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalrei1", sCacalrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBas_sumins", sBas_sumins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in1", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency1", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl1", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancfix1", nFrancFix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancmax1", nFrancMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancmin1", nFrancMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancrat1", nFrancrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype1", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMedreser1", nMedreser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremifix1", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimax1", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimin1", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat1", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoucapit1", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoufranc1", sRoufranc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoupremi1", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser1", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_Med_Exp", sInd_Med_Exp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApply_Perc", nApply_Perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRou_verify", sRou_verify, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update_DP033 = .Run(False)
		End With
		
Update_DP033_err: 
		If Err.Number Then
			Update_DP033 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsGen_coverDP033 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsGen_coverDP033 = Nothing
	End Function
	
	'% Delete_DP033: elimina los datos de la ventana DP033
	Public Function Delete_DP033() As Boolean
		Dim lrecinsdelgen_cover As eRemoteDB.Execute
		
		On Error GoTo Delete_DP033_err
		
		lrecinsdelgen_cover = New eRemoteDB.Execute
		
		'+ Definición de parámetros para stored procedure 'insudb.insdelgen_cover'
		'+ Información leída el 03/04/2001 11:38:46 a.m.
		
		With lrecinsdelgen_cover
			.StoredProcedure = "insdelgen_cover"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("datEfecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_DP033 = .Run(False)
		End With
		
Delete_DP033_err: 
		If Err.Number Then
			Delete_DP033 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsdelgen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsdelgen_cover = Nothing
	End Function
	
	'% Find: Este metodo se encarga de realiza la lectura de la tabla de coberturas genericas del
	'%       ramo/producto.
	Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaGen_cover As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		Find = False
		
		If nBranch <> Me.nBranch Or nProduct <> Me.nProduct Or nCover <> Me.nCover Or nModulec <> Me.nModulec Or dEffecdate <> Me.dEffecdate Or bFind Then
			lrecreaGen_cover = New eRemoteDB.Execute
			
			'Definición de parámetros para stored procedure 'insudb.reaGen_cover'
			'Información leída el 31/10/2001 12:32 a.m.
			
			With lrecreaGen_cover
				.StoredProcedure = "reaGen_cover_3"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sAddReini = .FieldToClass("sAddreini")
					nBranch_led = .FieldToClass("nBranch_led")
					sAddSuini = .FieldToClass("sAddsuini")
					nBranch_est = .FieldToClass("nBranch_est")
					sAddTaxin = .FieldToClass("sAddtaxin")
					sAutomrep = .FieldToClass("sAutomRep")
					nBill_item = .FieldToClass("nBill_item")
					nBranch_gen = .FieldToClass("nBranch_gen")
					nBranch_rei = .FieldToClass("nBranch_rei")
					nCacalcov = .FieldToClass("nCacalcov")
					nCacalfix = .FieldToClass("nCacalFix")
					sCacalfri = .FieldToClass("sCacalfri")
					sCacalili = .FieldToClass("sCacalili")
					sBas_sumins = .FieldToClass("sBas_sumins")
					nCacalmax = .FieldToClass("nCacalmax")
					nCacalper = .FieldToClass("nCacalper")
					sCacalrei = .FieldToClass("sCacalrei")
					nRateCapAdd = .FieldToClass("nRateCapAdd")
					nRateCapSub = .FieldToClass("nRateCapSub")
					sCh_typ_cap = .FieldToClass("sCh_typ_cap")
					nRatePreAdd = .FieldToClass("nRatePreAdd")
					nRatePreSub = .FieldToClass("nRatePreSub")
					sChange_typ = .FieldToClass("sChange_typ")
					nCover_in = .FieldToClass("nCover_in")
					nCoverapl = .FieldToClass("nCoverapl")
					nCovergen = .FieldToClass("nCoverGen")
					nCurrency = .FieldToClass("nCurrency")
					sDefaulti = .FieldToClass("sDefaulti")
					sFrancApl = .FieldToClass("sFrancapl")
					nFrancFix = .FieldToClass("nFrancfix")
					nFrancMax = .FieldToClass("nFrancmax")
					nFrancMin = .FieldToClass("nFrancmin")
					nFrancrat = .FieldToClass("nFrancRat")
					sFrantype = .FieldToClass("sFranType")
					nMedreser = .FieldToClass("nMedReser")
					nNotenum = .FieldToClass("nNotenum")
					dNulldate = .FieldToClass("dNulldate")
					nPremifix = .FieldToClass("nPremifix")
					nPremimax = .FieldToClass("nPremimax")
					nPremimin = .FieldToClass("nPremimin")
					nPremirat = .FieldToClass("nPremirat")
					sRequire = .FieldToClass("sRequire")
					sRoucapit = .FieldToClass("sRoucapit")
					sRoufranc = .FieldToClass("sRouFranc")
					sRoupremi = .FieldToClass("sRoupremi")
					sRoureser = .FieldToClass("sRoureser")
					sStatregt = .FieldToClass("sStatregt")
					nChCapLev = .FieldToClass("nChCapLev")
					nChPreLev = .FieldToClass("nChPreLev")
					nCacalmin = .FieldToClass("nCacalmin")
					sFDRequire = .FieldToClass("sFDRequire")
					sFDChantyp = .FieldToClass("sFDChantyp")
					nFDUserLev = .FieldToClass("nFDUserLev")
					nFDRateAdd = .FieldToClass("nFDRateAdd")
					nFDRateSub = .FieldToClass("nFDRateSub")
					sInd_Med_Exp = .FieldToClass("sInd_Med_exp")
					nApply_Perc = .FieldToClass("nApply_Perc")
					sRou_verify = .FieldToClass("sRou_verify")
					nRetarif = .FieldToClass("nRetarif")
					sBas_sumins = .FieldToClass("sBas_sumins")
					sReinorigcond = .FieldToClass("sReinorigcond")
					sBas_sumins = .FieldToClass("sBas_sumins")
					sCondSVS = .FieldToClass("sCondSVS")
                    nId_table = .FieldToClass("nId_table")
                    nFrancRatCla = .FieldToClass("nFrancRatCla")
                    nFrancFixCla = .FieldToClass("nFrancFixCla")
                    nFranxMinCla = .FieldToClass("nFranxMinCla")
                    nFranxMaxCla = .FieldToClass("nFranxMaxCla")
                    sRouFrancCla = .FieldToClass("sRouFrancCla")
                    sRASA_routine = .FieldToClass("sRASA_routine")
                    sIndManualDeductible = .FieldToClass("sIndManualDeductible")
                    nFrancDays = .FieldToClass("nFrancDays")
                    sCoveruse = .FieldToClass("sCoveruse")
                    sPrint_capital = .FieldToClass("sPrint_capital", "1")
                    nPrint_order = .FieldToClass("nPrint_order")
					.RCloseRec()
					Find = True
					Me.nBranch = nBranch
					Me.nProduct = nProduct
					Me.nModulec = nModulec
					Me.nCover = nCover
					Me.dEffecdate = dEffecdate
				End If
			End With
		Else
			Find = True
		End If
		
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover = Nothing
	End Function
	
	'% Table52_Exists_Not_have: Esta propiedad indica la existencia o no del Código
	'%                          de Tipo de Carencia y si su descripción es =  "No tiene",
	'%                          en la transacción  DP035A
	Public ReadOnly Property Table52_Exists_Not_have(ByVal sCaren_type As String) As Boolean
		Get
			Dim lobjGen_cover As eRemoteDB.Execute
			
			On Error GoTo Table52_Exists_Not_have_err
			
			lobjGen_cover = New eRemoteDB.Execute
			
			With lobjGen_cover
				.StoredProcedure = "reatable52"
				.Parameters.Add("sCaren_type", sCaren_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				Table52_Exists_Not_have = .Run
			End With
			
Table52_Exists_Not_have_err: 
			If Err.Number Then
				Table52_Exists_Not_have = False
			End If
			'UPGRADE_NOTE: Object lobjGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjGen_cover = Nothing
		End Get
	End Property
	
	'% insValArrayDP033: valida los datos pertenecientes a la DP033
	Public Function insValDP033(ByVal sWindowType As String, ByVal sExist As String, ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCovergen As Integer, ByVal sStatregt As String) As String
		Dim lintCounter As Integer
		Dim lintRow As Integer
		Dim lintRowAux As Integer
		Dim lintCovergen As Integer
		Dim lblnExist As Boolean
		Dim lstrStatregt As String
		
		Dim lclsLife_cover As Life_cover
		Dim lcolLife_cover As Life_covers
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValues As eFunctions.Values
		Dim lclTab_modul As Tab_modul
		
		lclsValues = New eFunctions.Values
		
		On Error GoTo insValDP033_err
		
		lclsErrors = New eFunctions.Errors
		
		If sWindowType <> "PopUp" Then
			If sExist = String.Empty Then
				Call lclsErrors.ErrorMessage("DP033", 1924)
			End If
            'If sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) Then
            If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
                lclTab_modul = New Tab_modul
                Call lclTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate)
                If lclTab_modul.styp_rat = "1" Then
                    If Find_Percent(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
                        Call lclsErrors.ErrorMessage("DP033", 60593)
                    End If
                End If
                'UPGRADE_NOTE: Object lclTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
                lclTab_modul = Nothing
            End If
        Else
            lclsLife_cover = New Life_cover

            '+Si tiene codigo y los demas campos estan vacios es un error
            If nCover <> eRemoteDB.Constants.intNull Then
                If nCovergen = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage("DP033", 11297)
                End If
                If sStatregt = "0" Then
                    Call lclsErrors.ErrorMessage("DP033", 11298)
                End If
            End If

            '+ El código debe estar lleno para incluir información en los demás campos
            If nCovergen <> eRemoteDB.Constants.intNull Or sStatregt <> "0" Then
                If nCover = eRemoteDB.Constants.intNull Then
                    Call lclsErrors.ErrorMessage("DP033", 1084)
                End If
            End If

            'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
            If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
                lblnExist = lclsLife_cover.Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
                lintCovergen = lclsLife_cover.nCovergen
                lstrStatregt = lclsLife_cover.sStatregt
            Else
                lblnExist = Find(nBranch, nProduct, IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec), nCover, dEffecdate)
                lintCovergen = Me.nCovergen
                lstrStatregt = Me.sStatregt
            End If

            '+ Si existe alguna de las claves repetida es un error
            If lblnExist And sAction <> "Update" Then
                Call lclsErrors.ErrorMessage("DP033", 11078)
            End If

            '+Si existe alguna de las cobertura repetida es un error

            If sAction <> "Update" Then
                'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
                If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
                    lcolLife_cover = New Life_covers
                    If lcolLife_cover.Find_Covergen(nBranch, nProduct, dEffecdate) Then
                        For Each lclsLife_cover In lcolLife_cover
                            If lclsLife_cover.nCover <> nCover And lclsLife_cover.nModulec = nModulec And lclsLife_cover.nCovergen = nCovergen Then
                                Call lclsErrors.ErrorMessage("DP033", 11369, , eFunctions.Errors.TextAlign.LeftAling, lclsLife_cover.sDescript & ": ")
                            End If
                        Next lclsLife_cover
                    End If
                Else
                    If FindCoverGen_Product(nBranch, nProduct, dEffecdate, nModulec, nCovergen) Then
                        If Me.nCover <> nCover Then
                            Call lclsErrors.ErrorMessage("DP033", 11369, , eFunctions.Errors.TextAlign.LeftAling, sDescript & ": ")
                        End If
                    End If
                End If
            End If

            '+ No se puede cambiar el estado a "En proceso de instalación"
            If lblnExist Then
                If sStatregt = CStr(Product.pmStatregt.pmEnProcesoDeInstalacion) And lstrStatregt <> CStr(Product.pmStatregt.pmEnProcesoDeInstalacion) Then
                    Call lclsErrors.ErrorMessage("DP033", 11339)
                End If
            End If

            '+ La cobertura genérica debe estar asociada al mismo Nro. de cobertura
            If insvalOtherExist(nBranch, nProduct, dEffecdate, nCover, nCovergen, nModulec, sBrancht) Then
                Call lclsErrors.ErrorMessage("DP033", 11387, , eFunctions.Errors.TextAlign.RigthAling, "(" & mstrOtherCover & ")")
            End If
        End If

        insValDP033 = lclsErrors.Confirm

insValDP033_err:
        If Err.Number Then
            insValDP033 = "insValDP033: " & insValDP033 & Err.Description
        End If
        On Error GoTo 0

        'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife_cover = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        'UPGRADE_NOTE: Object lcolLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolLife_cover = Nothing
    End Function
	
	'% insvalOtherExist: verifica que la cobertura genérica no se encuentre asociada a otro módulo
	Private Function insvalOtherExist(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nCovergen As Integer, ByVal nModulec As Integer, ByVal sBrancht As String) As Boolean
		Dim lcolGen_cover As Gen_covers
		Dim lclsGen_cover As Gen_cover
		Dim lclsLife_cover As Life_cover
		Dim lcolLife_cover As Life_covers
		
		On Error GoTo insvalOtherExist_err
		
		insvalOtherExist = False
		
		mstrOtherCover = String.Empty
		
		'+ Si la Cobertura genérica está asociada a una cob. de otro módulo,
		'+ el código de la cob. debe ser el mismo
        'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
            lcolLife_cover = New Life_covers
            If lcolLife_cover.Find_Covergen(nBranch, nProduct, dEffecdate) Then
                For Each lclsLife_cover In lcolLife_cover
                    If lclsLife_cover.nCovergen = nCovergen Then
                        If lclsLife_cover.nCover <> nCover And lclsLife_cover.nModulec <> nModulec Then
                            mstrOtherCover = IIf(mstrOtherCover = String.Empty, String.Empty, mstrOtherCover & ", ")
                            mstrOtherCover = mstrOtherCover & "Mod. " & lclsLife_cover.nModulec & "/Cob. " & lclsLife_cover.nCover
                            insvalOtherExist = True
                        End If
                    End If
                Next lclsLife_cover
            End If
        Else
            lcolGen_cover = New Gen_covers
            If lcolGen_cover.Find_All(nBranch, nProduct, dEffecdate) Then
                For Each lclsGen_cover In lcolGen_cover
                    If lclsGen_cover.nCovergen = nCovergen Then
                        If lclsGen_cover.nCover <> nCover And lclsGen_cover.nModulec <> nModulec Then
                            mstrOtherCover = IIf(mstrOtherCover = String.Empty, String.Empty, mstrOtherCover & ", ")
                            mstrOtherCover = mstrOtherCover & "Mod. " & lclsGen_cover.nModulec & "/Cob. " & lclsGen_cover.nCover
                            insvalOtherExist = True
                        End If
                    End If
                Next lclsGen_cover
            End If
        End If

insvalOtherExist_err:
        If Err.Number Then
            insvalOtherExist = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lcolGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolGen_cover = Nothing
        'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGen_cover = Nothing
    End Function
	
	'% getTableDP033: devuelve el tab_table correspondiente al campo Cobertura Genérica asociado
	'%                a la ventana DP033
	Public Function getTableDP033(ByVal sBrancht As String) As String
        'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
            getTableDP033 = "tabTab_LifCov"
        Else
            getTableDP033 = "tabTabGenCov"
        End If
    End Function
	
	'% InsPostDP033: Actualiza el indicador de contenido de la DP033
	Public Function InsPostDP033(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUsercode As Integer, ByVal sBrancht As String) As Boolean
		Dim lclsLife_cover As Life_cover
		Dim lclsProd_win As Prod_win
		
		Dim lstrContent As String
		
		lstrContent = "1"
        'If sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
            If Me.Count(nBranch, nProduct, dEffecdate) > 0 Then
                lstrContent = "2"
            End If
        Else
            lclsLife_cover = New Life_cover
            If lclsLife_cover.Count(nBranch, nProduct, dEffecdate) > 0 Then
                lstrContent = "2"
            End If
        End If

        lclsProd_win = New Prod_win
        InsPostDP033 = lclsProd_win.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP033", lstrContent, nUsercode)

        'UPGRADE_NOTE: Object lclsProd_win may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProd_win = Nothing
        'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife_cover = Nothing
    End Function
	
	'% InsPostDP033Upd: Actualiza las coberturas de un producto según especificaciones funcionales
	Public Function InsPostDP033Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nCovergen As Integer, ByVal nUsercode As Integer, ByVal sStatregt As String, ByVal sBrancht As String) As Boolean
		InsPostDP033Upd = True
		
		Select Case sAction
			Case "Add", "Update"
				InsPostDP033Upd = insUpdDP033(sAction, nBranch, nProduct, nModulec, nCover, dEffecdate, nCovergen, nUsercode, sStatregt, sBrancht)
				
			Case "Del"
				InsPostDP033Upd = insDelDP033(sAction, nBranch, nProduct, nModulec, nCover, dEffecdate, nCovergen, nUsercode, sStatregt, sBrancht)
		End Select
		
		If InsPostDP033Upd Then
			InsPostDP033Upd = InsPostDP033(nBranch, nProduct, dEffecdate, nUsercode, sBrancht)
		End If
	End Function
	
	'% insDelDP033: elimina cada una de las coberturas de un producto
	Private Function insDelDP033(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nCovergen As Integer, ByVal nUsercode As Integer, ByVal sStatregt As String, ByVal sBrancht As String) As Boolean
		Dim lclsLife_cover As Life_cover
		
        'If sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
            Me.nBranch = nBranch
            Me.nProduct = nProduct
            Me.nModulec = nModulec
            Me.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
            Me.nCover = nCover
            Me.nCovergen = nCovergen
            Me.dEffecdate = dEffecdate
            Me.nUsercode = nUsercode
            insDelDP033 = Delete_DP033()
        Else
            lclsLife_cover = New eProduct.Life_cover
            With lclsLife_cover
                .nBranch = nBranch
                .nProduct = nProduct
                .nCover = nCover
                .dEffecdate = dEffecdate
                .nModulec = nModulec
                .nusercode = nUsercode
                insDelDP033 = .Delete
            End With
        End If
        'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife_cover = Nothing
    End Function
	
	'% insUpdDP033: inserta o modifica cada una de las coberturas de un producto
	Private Function insUpdDP033(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nCovergen As Integer, ByVal nUsercode As Integer, ByVal sStatregt As String, ByVal sBrancht As String) As Boolean
		Dim lclsLife_cover As Life_cover
		Dim lblnFind As Boolean
		
		lblnFind = True
		
        'If sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht <> CStr(Product.pmBrancht.pmlife) And sBrancht <> CStr(Product.pmBrancht.pmNotTraditionalLife) And sBrancht <> CStr(Product.pmBrancht.pmMedicalAtention)) Then
            If sAction = "Update" Then
                lblnFind = Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
            End If
            If lblnFind Then
                Me.nBranch = nBranch
                Me.nProduct = nProduct
                Me.nModulec = IIf(nModulec = eRemoteDB.Constants.intNull, 0, nModulec)
                Me.nCover = nCover
                Me.dEffecdate = dEffecdate
                Me.nCovergen = nCovergen
                Me.nUsercode = nUsercode
                Me.sStatregt = sStatregt

                If sAction = "Add" Then
                    insUpdDP033 = insReaTabGencov(nCovergen)
                End If

                insUpdDP033 = Update_DP033()
            End If
        Else
            lclsLife_cover = New eProduct.Life_cover
            With lclsLife_cover
                If sAction = "Update" Then
                    lblnFind = .Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
                End If
                If lblnFind Then
                    .nBranch = nBranch
                    .nProduct = nProduct
                    .nModulec = nModulec
                    .nCover = nCover
                    .dEffecdate = dEffecdate
                    .nCovergen = nCovergen
                    .nusercode = nUsercode
                    .sStatregt = sStatregt

                    If sAction = "Add" Then
                        insUpdDP033 = insReaTab_lifcov(lclsLife_cover, nCovergen)
                    End If

                    If sAction = "Add" Then
                        insUpdDP033 = .Add
                    ElseIf sAction = "Update" Then
                        insUpdDP033 = .Update
                    End If
                End If
            End With
        End If

        'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsLife_cover = Nothing
    End Function
	
	'% insReaTabGencov: función que lee los datos (por defecto) de la cobertura general de
	'%                  la secuencia
	Private Function insReaTabGencov(ByVal nCovergen As Integer) As Boolean
		Dim lclsTab_GenCov As Tab_gencov
		
		lclsTab_GenCov = New Tab_gencov
		
		insReaTabGencov = True
		
		With lclsTab_GenCov
			If Not .Find(nCovergen) Then
				insReaTabGencov = False
			Else
				nBranch_led = .nBranch_led
				nBranch_est = .nBranch_est
				nBranch_gen = .nBranch_gen
				nBranch_rei = .nBranch_rei
				sAutomrep = .sAutomrep
				nCacalfix = .nCacalfix
				sCacalfri = .sCacalfri
				sCacalili = .sCacalili
				sCacalrei = .sCacalrei
				nCover_in = .nCover_in
				nCurrency = .nCurrency
				sFrancApl = .sFrancApl
				nFrancFix = .nFrancFix
				nFrancMax = .nFrancMax
				nFrancMin = .nFrancMin
				nFrancrat = .nFrancrat
				sFrantype = .sFrantype
				nMedreser = .nMedreser
				nPremifix = .nPremifix
				nPremimax = .nPremimax
				nPremimin = .nPremimin
				nPremirat = .nPremirat
				sRoucapit = .sRoucapit
				sRoufranc = .sRoufranc
				sRoupremi = .sRoupremi
				sRoureser = .sRoureser
				sDescript = .sDescript
			End If
		End With
		'UPGRADE_NOTE: Object lclsTab_GenCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_GenCov = Nothing
	End Function
	
	'% insReaTab_Lifcov: función que lee los datos (por defecto) de la cobertura general a la secuencia
	Private Function insReaTab_lifcov(ByRef lclsLife_cover As Life_cover, ByVal nCovergen As Integer) As Boolean
		Dim lclsTab_lifcov As Tab_lifcov
		
		lclsTab_lifcov = New Tab_lifcov
		
		If lclsTab_lifcov.Find(nCovergen) Then
			insReaTab_lifcov = True
			With lclsLife_cover
				.nBranch_est = lclsTab_lifcov.nBranch_est
				.nBranch_gen = lclsTab_lifcov.nBranch_gen
				.nBranch_led = lclsTab_lifcov.nBranch_led
				.nBranch_rei = lclsTab_lifcov.nBranch_rei
				.sCoveruse = lclsTab_lifcov.sCoveruse
				.nCurrency = lclsTab_lifcov.nCurrency
				.sRoureser = lclsTab_lifcov.sRoureser
				.sRousurre = lclsTab_lifcov.sRousurre
				'+ Valor por defecto (No aplica)
				.sAddReini = "2"
				.sAddSuini = "2"
				.sAddTaxin = "2"
				.nBill_item = eRemoteDB.Constants.intNull
				.nNotenum = eRemoteDB.Constants.intNull
				.nCaextexp = eRemoteDB.Constants.intNull
				.nCaintexp = eRemoteDB.Constants.intNull
				.nInterest = eRemoteDB.Constants.intNull
				.sMortacof = String.Empty
				.sMortacom = String.Empty
				.nPrextexp = eRemoteDB.Constants.intNull
				.nPrintexp = eRemoteDB.Constants.intNull
			End With
		End If
		'UPGRADE_NOTE: Object lclsTab_lifcov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_lifcov = Nothing
	End Function
	
	'% InitValues: se inicializan los valores de las variables públicas de la clase
	Private Sub InitValues()
		nModulec = eRemoteDB.Constants.intNull
		nBranch = eRemoteDB.Constants.intNull
		nCover = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		dEffecdate = eRemoteDB.Constants.dtmNull
		sAddReini = String.Empty
		nBranch_led = eRemoteDB.Constants.intNull
		sAddSuini = String.Empty
		nBranch_est = eRemoteDB.Constants.intNull
		sAddTaxin = String.Empty
		sAutomrep = String.Empty
		nBill_item = eRemoteDB.Constants.intNull
		nBranch_gen = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nCacalcov = eRemoteDB.Constants.intNull
		nCacalfix = eRemoteDB.Constants.intNull
		sCacalfri = String.Empty
		sCacalili = String.Empty
		sBas_sumins = String.Empty
		nCacalmax = eRemoteDB.Constants.intNull
		nCacalper = eRemoteDB.Constants.intNull
		sCacalrei = String.Empty
		nRateCapAdd = eRemoteDB.Constants.intNull
		nRateCapSub = eRemoteDB.Constants.intNull
		sCh_typ_cap = String.Empty
		nRatePreAdd = eRemoteDB.Constants.intNull
		nRatePreSub = eRemoteDB.Constants.intNull
		sChange_typ = String.Empty
		dCompdate = eRemoteDB.Constants.dtmNull
		nCover_in = eRemoteDB.Constants.intNull
		nCoverapl = eRemoteDB.Constants.intNull
		nCovergen = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		sDefaulti = String.Empty
		sFrancApl = String.Empty
		nFrancFix = eRemoteDB.Constants.intNull
		nFrancMax = eRemoteDB.Constants.intNull
		nFrancMin = eRemoteDB.Constants.intNull
		nFrancrat = eRemoteDB.Constants.intNull
		sFrantype = String.Empty
		nMedreser = eRemoteDB.Constants.intNull
		nNotenum = eRemoteDB.Constants.intNull
		dNulldate = eRemoteDB.Constants.dtmNull
		nPremifix = eRemoteDB.Constants.intNull
		nPremimax = eRemoteDB.Constants.intNull
		nPremimin = eRemoteDB.Constants.intNull
		nPremirat = eRemoteDB.Constants.intNull
		sRequire = String.Empty
		sRoucapit = String.Empty
		sRoufranc = String.Empty
		sRoupremi = String.Empty
		sRoureser = String.Empty
		sStatregt = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		nChCapLev = eRemoteDB.Constants.intNull
		nChPreLev = eRemoteDB.Constants.intNull
		nCacalmin = eRemoteDB.Constants.intNull
		sFDRequire = String.Empty
		sFDChantyp = String.Empty
		nFDUserLev = eRemoteDB.Constants.intNull
		nFDRateAdd = eRemoteDB.Constants.intNull
		nFDRateSub = eRemoteDB.Constants.intNull
		sInd_Med_Exp = String.Empty
		nApply_Perc = eRemoteDB.Constants.intNull
        sRou_verify = String.Empty
        sRASA_routine = String.Empty
	End Sub
	
	'% Update: realiza el mantenimiento de la historia en la estructura 'Gen_cover'
	Public Function Update() As Boolean
		Dim lrecinsGen_cover As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecinsGen_cover = New eRemoteDB.Execute
		
		With lrecinsGen_cover
			.StoredProcedure = "insGen_cover"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBill_item", nBill_item, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRequire", sRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNotenum", nNotenum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalrei", sCacalrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalmin", nCacalmin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalmax", nCacalmax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCh_typ_cap", sCh_typ_cap, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateCapAdd", nRateCapAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRateCapSub", nRateCapSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddsuini", sAddSuini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddreini", sAddReini, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAddtaxin", sAddTaxin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChCapLev", nChCapLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalili", sCacalili, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sBas_sumins", sBas_sumins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalcov", nCacalcov, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalper", nCacalper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoupremi", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremifix", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCoverapl", nCoverapl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChange_typ", sChange_typ, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatePreAdd", nRatePreAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRatePreSub", nRatePreSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nChPreLev", nChPreLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomrep", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancfix", nFrancFix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmax", nFrancMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmin", nFrancMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancrat", nFrancrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMedreser", nMedreser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoufranc", sRoufranc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFDRequire", sFDRequire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFDChantyp", sFDChantyp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFDUserLev", nFDUserLev, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFDRateAdd", nFDRateAdd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFDRateSub", nFDRateSub, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInd_Med_Exp", sInd_Med_Exp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApply_Perc", nApply_Perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRou_verify", sRou_verify, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRetarif", nRetarif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sReinorigcond", sReinorigcond, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancRatCla", nFrancRatCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 4, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancFixCla", nFrancFixCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranxMinCla", nFranxMinCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranxMaxCla", nFranxMaxCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRouFrancCla", sRouFrancCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRASA_routine", sRASA_routine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndManualDeductible", sIndManualDeductible, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancDays", nFrancDays, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCoveruse", sCoveruse, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrint_capital", sPrint_capital, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPrint_order", nPrint_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            
            Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsGen_cover = Nothing
	End Function
	
	'% insValDP034: función que realiza las validaciones puntuales o masivas de todos los campos
	'%              dependiendo del valor lógico del parámetro lblnall
    Public Function insValDP034(ByVal sCodispl As String, ByVal nBill_item As Integer, ByVal sCoveruse As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nModulec As Double, ByVal nCover As Double, ByVal dEffecdate As Date, ByVal nPrint_order As Integer) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValDP034_Err

        lobjErrors = New eFunctions.Errors

        '+Validación campo Concepto de facturación

        If nBill_item = 0 Or nBill_item = eRemoteDB.Constants.intNull Then
            Call lobjErrors.ErrorMessage(sCodispl, 11308)
        End If
        If InsExistsCoverUse(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
            Call lobjErrors.ErrorMessage(sCodispl, 90000506)
        End If
        If InsExistsCoverPrint(nBranch, nProduct, nModulec, nCover, dEffecdate, nPrint_order) Then
            Call lobjErrors.ErrorMessage(sCodispl, 9000052)
        End If
        insValDP034 = lobjErrors.Confirm

insValDP034_Err:
        If Err.Number Then
            insValDP034 = "insValDP034: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function
	
	'% insValDP035A: Esta funcion se encarga de realizar las validaciones correspondientes al
	'%               frame en tratamiento.
    Public Function insValDP035A(ByVal nFranchiseTyp As Integer, ByVal nFranchiseRate As Double, ByVal sFranchiseRou As String, ByVal nFranchiseReq As Integer, ByVal nFranchiseApl As Integer, ByVal nFranchiseFix As Double, ByVal nFranchiseMax As Double, ByVal nFranchiseMin As Double, ByVal nFranchiseAdd As Double, ByVal nFranchiseSub As Double, ByVal nMediumValue As Double, ByVal nFrancRatCla As Double, ByVal nFrancFixCla As Double, ByVal nFranxMinCla As Double, ByVal nFranxMaxCla As Double, ByVal sRouFrancCla As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsValTime As eFunctions.valField
        Dim lclsValues As eFunctions.Values
        Dim resxValues As IEnumerable(Of DictionaryEntry) = eFunctions.Values.GetResxValue("DP035A", False, "Product", "ProductSeq\CoverSeq")
        Dim lblnApllyForCapitalFilledOut As Boolean = True
        Dim lblnApllyForClaimFilledOut As Boolean = True

        On Error GoTo insValDP035A_Err

        lclsErrors = New eFunctions.Errors
        lclsValTime = New eFunctions.valField
        lclsValues = New eFunctions.Values
        lclsValTime.objErr = lclsErrors


        '+Validación si el campo tipo es diferente de no aplica y los campos,
        '+Porcentaje e Importe fijo de "Aplica sobre capital" (nFranchiseApl = 2) están vacíos

        If nFranchiseTyp > 1 And nFranchiseApl = 2 And ((nFranchiseRate = 0 Or Fix(nFranchiseRate) = eRemoteDB.Constants.intNull) And (nFranchiseFix = 0 Or Fix(nFranchiseFix) = eRemoteDB.Constants.intNull)) Then
            Call lclsErrors.ErrorMessage("DP035A", 11319, , eFunctions.Errors.TextAlign.RigthAling, " - " & resxValues.FindDictionaryValue("Anchor4Caption"))
            lblnApllyForCapitalFilledOut = False
        End If

        '+Validación si el campo tipo es diferente de no aplica y Aplica y los campos,
        '+Porcentaje e Importe fijo "Aplica sobre siniestro" (nFranchiseApl = 3) están vacíos

        If nFranchiseTyp > 1 And nFranchiseApl = 3 And ((nFrancRatCla = 0 Or Fix(nFrancRatCla) = eRemoteDB.Constants.intNull) And (nFrancFixCla = 0 Or Fix(nFrancFixCla) = eRemoteDB.Constants.intNull)) Then
            Call lclsErrors.ErrorMessage("DP035A", 11319, , eFunctions.Errors.TextAlign.RigthAling, " - " & resxValues.FindDictionaryValue("Anchor5Caption"))
            lblnApllyForClaimFilledOut = False
        End If

        '+Validación si el campo tipo es diferente de no aplica y los campos, Porcentaje e Importe fijo de 
        '+de las secciones "Aplica sobre capital" y "Aplica sobre siniestro" están vacíos
        If nFranchiseTyp > 1 And Not (lblnApllyForCapitalFilledOut Or lblnApllyForClaimFilledOut) Then
            If (nFranchiseRate = 0 Or Fix(nFranchiseRate) = eRemoteDB.Constants.intNull) And (nFranchiseFix = 0 Or Fix(nFranchiseFix) = eRemoteDB.Constants.intNull) Then
                Call lclsErrors.ErrorMessage("DP035A", 11319)
            End If
        End If

        '+Validación si el campo requerido está marcado, el campo
        '+tipo no puede tener el valor de no aplica

        If nFranchiseReq = 1 And nFranchiseTyp = 0 Then
            Call lclsErrors.ErrorMessage("DP035A", 11317)
        End If

        '+Validación si el campo "Tipo" tiene  valor diferente a 'no plica'
        '+el campo "Aplica sobre" no puede contener el valor: no aplica

        If nFranchiseTyp > 1 And (nFranchiseApl = 0 Or nFranchiseApl = 1) And (nFranchiseFix = 0 Or nFranchiseFix = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage("DP035A", 11318)
        End If

        '+Validación si el campo "Porcentaje" tiene valor no debe especificarse
        '+"Importe fijo de franquicia"
        If nFranchiseRate <> 0 And nFranchiseRate <> eRemoteDB.Constants.intNull And nFranchiseFix <> 0 And nFranchiseFix <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("DP035A", 11075)
        End If

        '+Validación si el campo "Monto Fijo-capital" tiene valor no debe especificarse
        '+"Porcentaje-capital"
        If nFranchiseFix <> 0 And Fix(nFranchiseFix) <> eRemoteDB.Constants.intNull And nFranchiseRate <> 0 And Fix(nFranchiseRate) <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("DP035A", 11075)
        End If

        ''+Validación si el campo "Monto Fijo-siniestro" tiene valor no debe especificarse
        ''+"Porcentaje-siniestro"
        'If nFrancFixCla <> 0 And Fix(nFrancFixCla) <> eRemoteDB.Constants.intNull And nFrancRatCla <> 0 And Fix(nFrancRatCla) <> eRemoteDB.Constants.intNull Then
        '    Call lclsErrors.ErrorMessage("DP035A", 11075)
        'End If

        '+Se valida que la franquicia máxima sea numérico
        If nFranchiseMax <> 0 And nFranchiseMax <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "#,###,###,##0.##"
            lclsValTime.Max = 9999999999.0#
            lclsValTime.Min = 0
            lclsValTime.Descript = eFunctions.Values.GetMessage(222)
            Call lclsValTime.ValNumber(nFranchiseMax)
        End If

        '+Se valida que la franquicia mínima sea numérico
        If nFranchiseMin <> 0 And nFranchiseMin <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "#,###,###,##0.##"
            lclsValTime.Max = 9999999999.0#
            lclsValTime.Min = 0
            lclsValTime.Descript = eFunctions.Values.GetMessage(223)
            Call lclsValTime.ValNumber(nFranchiseMin)
        End If

        '+Importe máximo-capital no puede ser menor o igual al mínimo-capital
        If nFranchiseMin > 0 And nFranchiseMax > 0 Then
            If nFranchiseMin >= nFranchiseMax Then
                Call lclsErrors.ErrorMessage("DP035A", 11048)
            End If
        End If

        '+Importe máximo-siniestro no puede ser menor o igual al mínimo-siniestro
        If nFranxMinCla > 0 And nFranxMaxCla > 0 Then
            If nFranxMinCla >= nFranxMaxCla Then
                Call lclsErrors.ErrorMessage("DP035A", 11048)
            End If
        End If

        '+Se valida que el porcentaje de franquicia sea numérico
        If nFranchiseRate <> 0 And nFranchiseRate <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "##.00"
            lclsValTime.Max = 99.99
            lclsValTime.Min = 0
            lclsValTime.Descript = eFunctions.Values.GetMessage(217)
            Call lclsValTime.ValNumber(nFranchiseRate)
        End If

        '+Se valida que la franquicia fija sea numérico
        If nFranchiseFix <> 0 And nFranchiseFix <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "#,###,###,##0"
            lclsValTime.Max = 9999999999.0#
            lclsValTime.Min = 0
            lclsValTime.Descript = eFunctions.Values.GetMessage(218)
            Call lclsValTime.ValNumber(nFranchiseFix, , eFunctions.valField.eTypeValField.onlyvalid)
        End If

        '+Se valida que la aumento franquicia sea numérico
        If nFranchiseAdd <> 0 And nFranchiseAdd <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "###0.00"
            lclsValTime.Max = 9999.99
            lclsValTime.Min = 0.01
            lclsValTime.Descript = eFunctions.Values.GetMessage(219)
            Call lclsValTime.ValNumber(nFranchiseAdd, , eFunctions.valField.eTypeValField.onlyvalid)
        End If

        '+Se valida que la disminución franquicia sea numérico
        If nFranchiseSub <> 0 And nFranchiseSub <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "###0.00"
            lclsValTime.Max = 9999.99
            lclsValTime.Min = 0.01
            lclsValTime.Descript = eFunctions.Values.GetMessage(220)
            Call lclsValTime.ValNumber(nFranchiseSub, , eFunctions.valField.eTypeValField.onlyvalid)
        End If

        '+Se valida que el valor medio sea numérico
        If nMediumValue <> 0 And nMediumValue <> eRemoteDB.Constants.intNull Then
            lclsValTime.ValFormat = "###,###,###,##0"
            lclsValTime.Max = 999999999999.0#
            lclsValTime.Min = 0
            lclsValTime.Descript = eFunctions.Values.GetMessage(221)
            Call lclsValTime.ValNumber(nMediumValue, , eFunctions.valField.eTypeValField.onlyvalid)
        End If

        '+Validación si el campo "Monto Fijo-capital" tiene valor no debe especificarse
        '+"Monto mínimo ni máximo"
        If nFranchiseFix <> 0 And Fix(nFranchiseFix) <> eRemoteDB.Constants.intNull And nFranchiseMin <> 0 And Fix(nFranchiseMin) <> eRemoteDB.Constants.intNull And nFranchiseMax <> 0 And Fix(nFranchiseMax) <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("DP035A", 11076)
        End If

        '+Validación si el campo "Monto Fijo-siniestro" tiene valor no debe especificarse
        '+"Monto mínimo ni máximo"
        If nFrancFixCla <> 0 And Fix(nFrancFixCla) <> eRemoteDB.Constants.intNull And nFranxMinCla <> 0 And Fix(nFranxMinCla) <> eRemoteDB.Constants.intNull And nFranxMaxCla <> 0 And Fix(nFranxMaxCla) <> eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage("DP035A", 11076)
        End If

        insValDP035A = lclsErrors.Confirm

insValDP035A_Err:
        If Err.Number Then
            insValDP035A = "insValDP035A: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsValTime may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValTime = Nothing
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
    End Function
	
	'%insPostDP034: Permite realizar las actualizaciones en las tablas
    Public Function insPostDP034(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nBill_item As Integer, ByVal nBranch_gen As Integer, ByVal nBranch_rei As Integer, ByVal nBranch_led As Integer, ByVal nBranch_est As Integer, ByVal nCurrency As Integer, ByVal sDefaulti As String, ByVal sRequire As String, ByVal sInd_Med_Exp As String, ByVal nNotenum As Integer, ByVal nUsercode As Integer, ByVal nRetarif As Integer, ByVal sReinorigcond As String, ByVal sCondSVS As String, ByVal sCoveruse As String, ByVal sPrint_capital As String, ByVal nPrint_order As Integer) As Boolean
        On Error GoTo insPostDP034_Err
        If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
            With Me
                If Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
                    .nBranch = nBranch
                    .nProduct = nProduct
                    .nModulec = nModulec
                    .nCover = nCover
                    .dEffecdate = dEffecdate
                    .sStatregt = "2"
                    .nUsercode = nUsercode
                    .nBill_item = nBill_item
                    .nBranch_rei = IIf(nBranch_rei <> 0, nBranch_rei, eRemoteDB.Constants.intNull)
                    .nBranch_gen = IIf(nBranch_gen <> 0, nBranch_gen, eRemoteDB.Constants.intNull)
                    .nBranch_led = IIf(nBranch_led <> 0, nBranch_led, eRemoteDB.Constants.intNull)
                    .nBranch_est = IIf(nBranch_est <> 0, nBranch_est, eRemoteDB.Constants.intNull)
                    .nCurrency = IIf(nCurrency <> 0, nCurrency, eRemoteDB.Constants.intNull)
                    .sDefaulti = IIf(sDefaulti = String.Empty, "2", sDefaulti)
                    .sRequire = IIf(sRequire = String.Empty, "2", sRequire)
                    .sInd_Med_Exp = IIf(sInd_Med_Exp = String.Empty, "2", sInd_Med_Exp)
                    .nNotenum = nNotenum
                    .nRetarif = nRetarif
                    .sReinorigcond = IIf(sReinorigcond = String.Empty, "2", sReinorigcond)
                    .sCondSVS = sCondSVS
                    .sCoveruse = sCoveruse
                    .sPrint_capital = IIf(sPrint_capital = String.Empty, "2", sPrint_capital)
                    .nPrint_order = nPrint_order
                    insPostDP034 = Update()
                End If
            End With
        End If

insPostDP034_Err:
        If Err.Number Then
            insPostDP034 = False
        End If
        On Error GoTo 0
    End Function
	
	'% insPostDP035A: Esta función se encarga de validar los datos introducidos en la zona de
	'%                contenido para "frame" especifico.
    Public Function insPostDP035A(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sAutomaticRep As String, ByVal sFranchiseApl As String, ByVal nFranchiseFix As Double, ByVal nFranchiseMax As Double, ByVal nFranchiseMin As Double, ByVal nFranchiseRate As Double, ByVal sFranchiseTyp As String, ByVal nMediumValue As Double, ByVal sFranchiseRou As String, ByVal sReserveRou As String, ByVal sFranchiseReq As String, ByVal nFranchAdd As Integer, ByVal nFranchSub As Integer, ByVal nFranchiseAdd As Double, ByVal nFranchiseSub As Double, ByVal nFranchiseLev As Integer, ByVal nFrancRatCla As Double, ByVal nFrancFixCla As Double, ByVal nFranxMinCla As Double, ByVal nFranxMaxCla As Double, ByVal sRouFrancCla As String, ByVal sRASA_routine As String, ByVal sIndManualDeductible As String, ByVal nFrancDays As Double) As Boolean
        Dim lclsGen_cover As eProduct.Gen_cover
        Dim lclsProdwin As eProduct.Prod_win

        On Error GoTo insPostDP035A_Err

        lclsGen_cover = New eProduct.Gen_cover

        With lclsGen_cover
            If .Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
                .nBranch = nBranch
                .nProduct = nProduct
                .nModulec = nModulec
                .nCover = nCover
                .dEffecdate = dEffecdate
                .sAutomrep = sAutomaticRep
                .sFrancApl = sFranchiseApl
                .sFrancApl = IIf(sFranchiseApl <> "0", sFranchiseApl, String.Empty)
                .nFrancFix = nFranchiseFix
                .nFrancMax = nFranchiseMax
                .nFrancMin = nFranchiseMin
                .nFrancrat = nFranchiseRate
                .sFrantype = IIf(sFranchiseTyp <> "0", sFranchiseTyp, String.Empty)
                .nMedreser = nMediumValue
                .sRoufranc = sFranchiseRou
                .sRoureser = sReserveRou
                .sFDRequire = sFranchiseReq
                .sRASA_routine = sRASA_routine
                .sIndManualDeductible = sIndManualDeductible
                .nFrancDays = nFrancDays
                If nFranchAdd = 0 And nFranchSub = 0 Then
                    .sFDChantyp = "1"
                Else
                    If nFranchAdd = 1 And nFranchSub = 0 Then
                        .sFDChantyp = "2"
                    Else
                        If nFranchAdd = 0 And nFranchSub = 1 Then
                            .sFDChantyp = "3"
                        Else
                            If nFranchAdd = 1 And nFranchSub = 1 Then
                                .sFDChantyp = "4"
                            End If
                        End If
                    End If
                End If
                .nFDUserLev = nFranchiseLev
                .nFDRateAdd = nFranchiseAdd
                .nFDRateSub = nFranchiseSub
                .nFrancRatCla = nFrancRatCla
                .nFrancFixCla = nFrancFixCla
                .nFranxMinCla = nFranxMinCla
                .nFranxMaxCla = nFranxMaxCla
                .sRouFrancCla = sRouFrancCla

                insPostDP035A = .Update
            End If
        End With

        If insPostDP035A Then
            lclsProdwin = New Prod_win
            insPostDP035A = lclsProdwin.Add_Prod_win(nBranch, nProduct, dEffecdate, "DP035A", "2", nUsercode)
        End If

insPostDP035A_Err:
        If Err.Number Then
            insPostDP035A = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGen_cover = Nothing
        'UPGRADE_NOTE: Object lclsProdwin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProdwin = Nothing
    End Function
	
	'% Find_AddCapital: Permite hacerse del indicador de suma para obtener el capital
	'%                  asegurado de la póliza
	Public Function Find_AddCapital(ByVal sCondition As String, ByVal sValue As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaGen_coverAdd As eRemoteDB.Execute
		
		On Error GoTo Find_AddCapital_Err
		
		lrecreaGen_coverAdd = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaGen_coverAdd'
		'Información leída el 27/04/2001 14:32:26
		
		With lrecreaGen_coverAdd
			.StoredProcedure = "reaGen_coverAdd"
			.Parameters.Add("sCondition", sCondition, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				If sCondition = "1" Then
					sAddSuini = .FieldToClass("sAddsuini")
				ElseIf sCondition = "2" Then 
					sAddReini = .FieldToClass("sAddreini")
				Else
					sAddTaxin = .FieldToClass("sAddtaxin")
				End If
				Find_AddCapital = True
				.RCloseRec()
			End If
		End With
		
Find_AddCapital_Err: 
		If Err.Number Then
			Find_AddCapital = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaGen_coverAdd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_coverAdd = Nothing
	End Function
	
	'% insPreDP052: permite obtener la información de necesaria para el manejo de la ventana
	Public Function insPreDP052(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsProduct As eProduct.Product
		
		On Error GoTo insPreDP052_Err
		
		lclsProduct = New eProduct.Product
		
		Call lclsProduct.Find(nBranch, nProduct, dEffecdate)
		Call Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
		With Me
			.sCh_typ_cap = IIf(Trim(.sCh_typ_cap) <> String.Empty, .sCh_typ_cap, "9")
			.sAddSuini = IIf(Trim(.sAddSuini) <> String.Empty, .sAddSuini, "9")
			.sAddReini = IIf(Trim(.sAddReini) <> String.Empty, .sAddReini, "9")
			.sAddTaxin = IIf(Trim(.sAddTaxin) <> String.Empty, .sAddTaxin, "9")
			.sRevalapl = IIf(Trim(lclsProduct.sRevalapl) <> String.Empty, lclsProduct.sRevalapl, "9")
			.sCacalrei = IIf(Trim(.sCacalrei) <> String.Empty, .sCacalrei, "9")
			.sCacalfri = IIf(Trim(.sCacalfri) <> String.Empty, .sCacalfri, "9")
			.sCacalili = IIf(Trim(.sCacalili) <> String.Empty, .sCacalili, "9")
			.sBas_sumins = IIf(Trim(.sBas_sumins) <> String.Empty, .sBas_sumins, "9")
			mstrCacalrei = .sCacalrei
			mstrCh_typ_cap = .sCh_typ_cap
			mstrAddSuini = .sAddSuini
			mstrAddReini = .sAddReini
			mstrAddTaxin = .sAddTaxin
		End With
		
insPreDP052_Err: 
		If Err.Number Then
			insPreDP052 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsProduct = Nothing
	End Function
	
	'% DefaultValueDP052: Esta rutina se encarga de desplegar los valores correspondientes
	'%                    a las condiciones de cálculo de prima
	Public Function DefaultValueDP052(ByVal sField As String) As String
        Dim lvarReturnValue As String = ""
        Dim lutdField() As String
		Dim lstrAuxField As String
		Dim lstrTypeField As String
		
		lutdField = Microsoft.VisualBasic.Split(sField, ".", Len(sField))
		lstrTypeField = lutdField(UBound(lutdField) - 1)
		lstrAuxField = lutdField(UBound(lutdField))
		
		If InStr(1, sField, "chkIndex", 1) > 0 Then
			Select Case sField
				Case "chkIndex.disabled"
					If mstrRevalapl = "3" Then
						lvarReturnValue = "true"
					End If
				Case "chkIndex"
					If mstrRevalapl = "3" Then
						lvarReturnValue = "0"
					Else
						lvarReturnValue = IIf(mstrCacalrei = "0", "1", "0")
					End If
			End Select
		End If
		
		If mstrCh_typ_cap = "1" Then
			Select Case sField
				Case "chkCapitalAddCh", "chkCapitalSubCh"
					lvarReturnValue = "0"
				Case "tcnCapitalAddCh.disabled", "tcnCapitalSubCh.disabled"
					lvarReturnValue = "false"
			End Select
			
		ElseIf mstrCh_typ_cap = "2" Then 
			Select Case sField
				Case "chkCapitalAddCh"
					lvarReturnValue = "1"
				Case "chkCapitalSubCh"
					lvarReturnValue = "0"
				Case "tcnCapitalAddCh.disabled"
					lvarReturnValue = "true"
				Case "tcnCapitalSubCh.disabled"
					lvarReturnValue = "false"
			End Select
			
		ElseIf mstrCh_typ_cap = "3" Then 
			Select Case sField
				Case "chkCapitalAddCh"
					lvarReturnValue = "0"
				Case "chkCapitalSubCh"
					lvarReturnValue = "1"
				Case "tcnCapitalAddCh.disabled"
					lvarReturnValue = "false"
				Case "tcnCapitalSubCh.disabled"
					lvarReturnValue = "true"
			End Select
			
		ElseIf mstrCh_typ_cap = "4" Then 
			Select Case sField
				Case "chkCapitalAddCh", "chkCapitalSubCh"
					lvarReturnValue = "1"
				Case "tcnCapitalAddCh.disabled", "tcnCapitalSubCh.disabled"
					lvarReturnValue = "true"
			End Select
		End If
		
		If lstrTypeField = "optCapital" Then
			Select Case mstrAddSuini
				Case "1"
					lvarReturnValue = IIf(lstrAuxField = "AddAdd", "true", "false")
				Case "2"
					lvarReturnValue = IIf(lstrAuxField = "No", "true", "false")
				Case "3"
					lvarReturnValue = IIf(lstrAuxField = "AddBig", "true", "false")
			End Select
		End If
		
		If lstrTypeField = "optReinsu" Then
			Select Case mstrAddReini
				Case "1"
					lvarReturnValue = IIf(lstrAuxField = "AddAdd", "true", "false")
				Case "2"
					lvarReturnValue = IIf(lstrAuxField = "No", "true", "false")
				Case "3"
					lvarReturnValue = IIf(lstrAuxField = "AddBig", "true", "false")
			End Select
		End If
		
		If lstrTypeField = "optTax" Then
			Select Case mstrAddTaxin
				Case "1"
					lvarReturnValue = IIf(lstrAuxField = "AddAdd", "true", "false")
				Case "2"
					lvarReturnValue = IIf(lstrAuxField = "No", "true", "false")
				Case "3"
					lvarReturnValue = IIf(lstrAuxField = "AddBig", "true", "false")
			End Select
		End If
		
		Erase lutdField
		DefaultValueDP052 = lvarReturnValue
	End Function
	
	'% insValDP052: En esta rutina se realizan las validaciones de la ventana de datos para el calculo de prima
	Public Function insValDP052(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal sCacalili As String, ByVal nCacalmin As Double, ByVal nCacalmax As Double, ByVal nRateCapSub As Double, ByVal nRateCapAdd As Double, ByVal sAddSuini As String, ByVal sAddReini As String, ByVal sAddTaxin As String) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsGen_cover As Gen_cover
		
		On Error GoTo insValDP052_err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsGen_cover = New Gen_cover
		
		nCacalmin = IIf(nCacalmin = eRemoteDB.Constants.intNull, 0, nCacalmin)
		nCacalmax = IIf(nCacalmax = eRemoteDB.Constants.intNull, 0, nCacalmax)
		nRateCapSub = IIf(nRateCapAdd = eRemoteDB.Constants.intNull, 0, nRateCapSub)
		nRateCapAdd = IIf(nRateCapAdd = eRemoteDB.Constants.intNull, 0, nRateCapSub)
		
		With lclsGen_cover
			.nBranch = nBranch
			.nProduct = nProduct
			.nCover = nCover
			.nModulec = nModulec
			.dEffecdate = dEffecdate
			.sAddSuini = sAddSuini
			.sAddReini = sAddReini
			.sAddTaxin = sAddTaxin
		End With
		
		If nCacalmax <> 0 Then
			If sCacalili = "1" Then
				Call lclsErrors.ErrorMessage(sCodispl, 11383)
			End If
		End If
		
		'+Se valida que el capital mínimo sea menor al máximo
		If nCacalmax <= nCacalmin Then
			If Not (nCacalmax = 0 And nCacalmin = 0) Then
				If nCacalmax <> 0 Then
					Call lclsErrors.ErrorMessage(sCodispl, 11048)
				End If
			End If
		End If
		
		'+Se valida el porcentaje de aumento
		If nRateCapSub <> 0 Then
			With lclsValField
				.objErr = lclsErrors
				.Max = 99.99
				.Min = 0.01
				.Descript = "Disminuir "
				Call .ValNumber(nRateCapSub,  , eFunctions.valField.eTypeValField.ValAll)
			End With
		End If
		
		'+Se valida el porcentaje de aumento
		If nRateCapAdd <> 0 Then
			With lclsValField
				.objErr = lclsErrors
				.Max = 9999.99
				.Min = 0.01
				.Descript = "Aumentar "
				Call .ValNumber(nRateCapAdd,  , eFunctions.valField.eTypeValField.ValAll)
			End With
		End If
		
		Call ValidAddSuini(lclsGen_cover)
		Call ValidAddReini(lclsGen_cover)
		Call ValidAddTaxin(lclsGen_cover)
		
		If mblnAddErr Then
			Call lclsErrors.ErrorMessage(sCodispl, 11393)
		End If
		
		If mblnBigErr Then
			Call lclsErrors.ErrorMessage(sCodispl, 11392)
		End If
		
		insValDP052 = lclsErrors.Confirm
		
insValDP052_err: 
		If Err.Number Then
			insValDP052 = "insValDP052: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGen_cover = Nothing
	End Function
	
	'% ValidAddSuini: Permite validar la existencia de otra cobertura para el producto
	'%                con opción de mayor o suma(capital asegurado)
	Private Sub ValidAddSuini(ByVal oGen_cover As Gen_cover)
		Select Case oGen_cover.sAddSuini
			Case "1"
				If Find_AddCapital("1", "3", oGen_cover.nBranch, oGen_cover.nProduct, oGen_cover.nCover, oGen_cover.nModulec, oGen_cover.dEffecdate) Then
					mblnAddErr = True
				End If
			Case "3"
				If Find_AddCapital("1", "1", oGen_cover.nBranch, oGen_cover.nProduct, oGen_cover.nCover, oGen_cover.nModulec, oGen_cover.dEffecdate) Then
					mblnBigErr = True
				End If
		End Select
	End Sub
	
	'% ValidAddReini: Permite validar la existencia de otra cobertura para el producto
	'%                con opción de mayor o suma(capital a reasegurar)
	Private Sub ValidAddReini(ByVal oGen_cover As Gen_cover)
		Select Case oGen_cover.sAddReini
			Case "1"
				If Find_AddCapital("2", "3", oGen_cover.nBranch, oGen_cover.nProduct, oGen_cover.nCover, oGen_cover.nModulec, oGen_cover.dEffecdate) Then
					mblnAddErr = True
				End If
			Case "3"
				If Find_AddCapital("2", "1", oGen_cover.nBranch, oGen_cover.nProduct, oGen_cover.nCover, oGen_cover.nModulec, oGen_cover.dEffecdate) Then
					mblnBigErr = True
				End If
		End Select
	End Sub
	
	'% ValidAddTaxin: Permite validar la existencia de otra cobertura para el producto
	'%                con opción de mayor o suma(capital para el cálculo de impuestos)
	Private Sub ValidAddTaxin(ByVal oGen_cover As Gen_cover)
		Select Case oGen_cover.sAddTaxin
			Case "1"
				If Find_AddCapital("3", "3", oGen_cover.nBranch, oGen_cover.nProduct, oGen_cover.nCover, oGen_cover.nModulec, oGen_cover.dEffecdate) Then
					mblnAddErr = True
				End If
			Case "3"
				If Find_AddCapital("3", "1", oGen_cover.nBranch, oGen_cover.nProduct, oGen_cover.nCover, oGen_cover.nModulec, oGen_cover.dEffecdate) Then
					mblnBigErr = True
				End If
		End Select
	End Sub
	
	'%insPostDP052.En esta rutina se realizan las validaciones de la ventana de datos para el calculo de prima
	Public Function insPostDP052(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sCacalrei As String, ByVal nCacalmin As Double, ByVal nCacalmax As Double, ByVal sCapitalAddCh As String, ByVal sCapitalSubCh As String, ByVal nRateCapAdd As Double, ByVal nRateCapSub As Double, ByVal sAddSuini As String, ByVal sAddReini As String, ByVal sAddTaxin As String, ByVal nChCapLev As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostDP052_Err
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			With Me
				If Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
					.nBranch = nBranch
					.nProduct = nProduct
					.nModulec = nModulec
					.nCover = nCover
					.dEffecdate = dEffecdate
					.sStatregt = "2"
					.nUsercode = nUsercode
					.nCacalmin = nCacalmin
					.nCacalmax = nCacalmax
					.nRateCapAdd = nRateCapAdd
					.nRateCapSub = nRateCapSub
					.nChCapLev = nChCapLev
					Call insUpdateDP052(sCapitalAddCh, sCapitalSubCh)
					.sCacalrei = IIf(sCacalrei = String.Empty, "2", sCacalrei)
					.sCh_typ_cap = mstrCh_typ_cap
					.sAddSuini = sAddSuini
					.sAddReini = sAddReini
					.sAddTaxin = sAddTaxin
					insPostDP052 = Update
				End If
			End With
		End If
		
insPostDP052_Err: 
		If Err.Number Then
			insPostDP052 = False
		End If
		On Error GoTo 0
	End Function

    '% DefaultValueCapital: se asigna el valor por defecto de los Check "Aumentar" y "Disminuir"
    '%                      de la forma DP052
    Public Function DefaultValueCapital(ByVal sValue As String, ByVal sField As String) As String
        Dim strResultado As String = ""
        Try
            Select Case sField
                Case "AddCh"
                    strResultado = IIf(sValue = "2" Or sValue = "4", "1", "2")
                Case "SubCh"
                    strResultado = IIf(sValue = "3" Or sValue = "4", "1", "2")
            End Select
            Return strResultado
        Catch ex As Exception
            Return strResultado
        End Try
    End Function

    '% insUpdateDP052: se evalúa el tipo de cambio posible de realizar sobre el capital
    Private Sub insUpdateDP052(ByVal sCapitalAddCh As String, ByVal sCapitalSubCh As String)
		If sCapitalAddCh <> "1" And sCapitalSubCh <> "1" Then
			mstrCh_typ_cap = "1"
		Else
			If sCapitalAddCh = "1" And sCapitalSubCh <> "1" Then
				mstrCh_typ_cap = "2"
			Else
				If sCapitalAddCh <> "1" And sCapitalSubCh = "1" Then
					mstrCh_typ_cap = "3"
				Else
					If sCapitalAddCh = "1" And sCapitalSubCh = "1" Then
						mstrCh_typ_cap = "4"
					End If
				End If
			End If
		End If
	End Sub
	
	'% insValDP052A: En esta rutina se realizan las validaciones de la ventana de datos
	'%               para el calculo de prima
	Public Function insValDP052A(ByVal sCodispl As String, ByVal sCapital As String, ByVal sRoucapit As String, ByVal nCacalfix As Double, ByVal nCacalper As Integer, ByVal nCoverapl As Integer, ByVal nOtherCover As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As String
		Dim lclsErrors As eFunctions.Errors
		Dim lclsValField As eFunctions.valField
		Dim lclsSumcov_apl As eProduct.Sumcov_apl
		
		Call Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
		
		nCacalmax = IIf(nCacalmax <> eRemoteDB.Constants.intNull, nCacalmax, 0)
		nCacalmin = IIf(nCacalmin <> eRemoteDB.Constants.intNull, nCacalmin, 0)
		nCacalfix = IIf(nCacalfix <> eRemoteDB.Constants.intNull, nCacalfix, 0)
		nCoverapl = IIf(nCoverapl <> eRemoteDB.Constants.intNull, nCoverapl, 0)
		nCacalper = IIf(nCacalper <> eRemoteDB.Constants.intNull, nCacalper, 0)
		nOtherCover = IIf(nOtherCover <> eRemoteDB.Constants.intNull, nOtherCover, 0)
		
		On Error GoTo insValDP052A_Err
		
		lclsErrors = New eFunctions.Errors
		lclsValField = New eFunctions.valField
		lclsSumcov_apl = New eProduct.Sumcov_apl
		
		'+Se verifica que alguno de los campos correspondientes al calculo de la prima este lleno
		
		'+Ilimitado:3
		If sCapital = "3" And (nCacalmax <> 0 Or nCacalmin <> 0) Then
			Call lclsErrors.ErrorMessage(sCodispl, 11383)
		End If
		
		'+Sólo rutina:1
		If sCapital = "1" Then
			If sRoucapit = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 1925,  , eFunctions.Errors.TextAlign.LeftAling, "Rutina: ")
			End If
		End If
		
		'+Fijo
		If sCapital = "4" Then
			'+Si el campo "Fijo" esta marcado el campo "Importe" debe estar lleno
			If nCacalfix = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11321)
			End If
		End If
		
		'+Otra Cobertura
		If sCapital = "5" Then
			'+Si el campo "% Otra Cobertura" esta marcado se debe incluir la cobertura y el porcentaje
			If nCacalper = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 11311)
			End If
			
			If nCacalper <> 0 Then
				If nCoverapl <> 0 Then
					lclsValField.objErr = lclsErrors
					lclsValField.Max = 999.99
					lclsValField.Min = 0.01
					lclsValField.Descript = "% "
					Call lclsValField.ValNumber(nCacalper,  , eFunctions.valField.eTypeValField.ValAll)
				End If
			End If
			
			If nOtherCover = 0 Or nOtherCover = eRemoteDB.Constants.intNull Then
				Call lclsErrors.ErrorMessage(sCodispl, 11312)
			End If
			
		End If
		
		If sCapital = "6" Then
			If Not lclsSumcov_apl.Find_Val(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
				Call lclsErrors.ErrorMessage(sCodispl, 11313)
			End If
		End If
		
		insValDP052A = lclsErrors.Confirm
		
insValDP052A_Err: 
		If Err.Number Then
			insValDP052A = "insValDP052A: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		'UPGRADE_NOTE: Object lclsValField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsValField = Nothing
		'UPGRADE_NOTE: Object lclsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSumcov_apl = Nothing
	End Function
	
	'% insPostDP052A: Permite realizar las actualizaciones en la tabla
	Public Function insPostDP052A(ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal sCapital As String, ByVal sRoucapit As String, ByVal nCacalfix As Double, ByVal nCacalper As Double, ByVal nOtherCover As Integer, ByVal nUsercode As Integer) As Object
		Dim lclsSumcov_apl As eProduct.Sumcov_apl
		
		On Error GoTo insPostDP052A_Err
		
		lclsSumcov_apl = New eProduct.Sumcov_apl
		
		If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			With Me
				If Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
					.nBranch = nBranch
					.nProduct = nProduct
					.nModulec = nModulec
					.nCover = nCover
					.dEffecdate = dEffecdate
					.sStatregt = "2"
					.nUsercode = nUsercode
					.sRoucapit = sRoucapit
					.sBas_sumins = IIf(sCapital = "6", "1", "2")
					.sCacalfri = IIf(sCapital = "2", "1", "2")
					.sCacalili = IIf(sCapital = "3", "1", "2")
					'+Fijo
					If sCapital = "4" Then
						.nCacalfix = nCacalfix
						.nCacalcov = eRemoteDB.Constants.intNull
						.nCacalper = eRemoteDB.Constants.intNull
						'+%Otra Cobertura
					ElseIf sCapital = "5" Then 
						.nCacalfix = eRemoteDB.Constants.intNull
						.nCacalcov = IIf(nOtherCover <> eRemoteDB.Constants.intNull, nOtherCover, 0)
						.nCacalper = IIf(nCacalper <> eRemoteDB.Constants.intNull, nCacalper, 0)
					Else
						.nCacalfix = eRemoteDB.Constants.intNull
						.nCacalcov = eRemoteDB.Constants.intNull
						.nCacalper = eRemoteDB.Constants.intNull
					End If
					insPostDP052A = .Update
					
					If sCapital <> "6" Then
						insPostDP052A = lclsSumcov_apl.DeleteDP052A(.nBranch, .nProduct, .nModulec, .nCover, .dEffecdate, .nUsercode)
					End If
				End If
			End With
		End If
		
insPostDP052A_Err: 
		If Err.Number Then
			insPostDP052A = False
		End If
		'UPGRADE_NOTE: Object lclsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSumcov_apl = Nothing
		On Error GoTo 0
		
	End Function
	
	'% LoadTabs: Esta función es la encarga de carga la información necesaria para cada
	'%           pestaña que sera mostrada en la forma.
	Public Function LoadTabs(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nCovergen As Integer) As String
        'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
            LoadTabs = LoadTabsLifeCover(bQuery, nBranch, nProduct, nModulec, dEffecdate, nCover, nCovergen)
        Else
            LoadTabs = LoadTabsGenCover(bQuery, nBranch, nProduct, dEffecdate, sBrancht, nModulec, nCover)
        End If
    End Function
	
	'% LoadTabsCoverGen: Esta función es la encarga de carga la información necesaria para cada
	'%                   pestaña que sera mostrada para coberturas genéricas.
	Private Function LoadTabsGenCover(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal sBrancht As String, ByVal nModulec As Integer, ByVal nCover As Integer) As String
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsGen_cover As eProduct.Gen_cover
		Dim lclsSumcov_apl As Sumcov_apl
		Dim lclsCl_cov_bil As Object
		Dim lclsProduct_ge As Product_ge
		Dim lclsSequence As eFunctions.Sequence
		Dim lintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lclTab_modul As Tab_modul
		
		
		'-Se define la variable que indica la existencia de las ventanas de la secuencia
		Dim lintCount As Integer
		Dim lintAux As Integer
		Dim lstrEnaDP049 As String
		Dim lstrHTMLCode As String
		Dim lintAction As Integer

        Dim lvntAux As Object = New Object

        '-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
        '-extraído de la constante cstrWindows
        Dim lstrCodispl As String
		
		On Error GoTo LoadTabsGenCover_err
		
		lclsSumcov_apl = New Sumcov_apl
		lclsGen_cover = New eProduct.Gen_cover
		lclsQuery = New eRemoteDB.Query
		lclsSequence = New eFunctions.Sequence
		
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		
		lclsGen_cover.Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
		
		lintAux = 1
		
		lstrHTMLCode = lclsSequence.makeTable
		
		For lintCount = 1 To CN_FRAMESNUMGENCOVER
			
			'+Se extrae el código de la ventana
			
			lstrCodispl = Trim(Mid(CN_WINDOWSGENCOV, lintAux, 8))
			lintAux = lintAux + 8
			
			'+ Se verifica la situación de la transacción DP049, ya que es requerida, opcional o no debe
			'+ aparecer según el valor del campo concepto de pago de la transacción DP031 de
			'+ la secuencia de productos
			lstrEnaDP049 = String.Empty
			
			If lstrCodispl = "DP049" Then
				lclsProduct_ge = New Product_ge
				If lclsProduct_ge.Find(nBranch, nProduct, dEffecdate) Then
					If lclsProduct_ge.sPayconre = String.Empty Or lclsProduct_ge.sPayconre = "3" Then
						lstrEnaDP049 = "Opcional"
					Else
						If lclsProduct_ge.sPayconre = "1" Then
							lstrEnaDP049 = "Oculta"
						Else
							If lclsProduct_ge.sPayconre = "2" Then
								lstrEnaDP049 = "Requerida"
							End If
						End If
					End If
				End If
				'UPGRADE_NOTE: Object lclsProduct_ge may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				lclsProduct_ge = Nothing
			End If
			
			If lstrEnaDP049 <> "Oculta" Then
				
				Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
				
				'+ Se obtiene por cada transacción un campo (requerido) de la misma
				'+ para identificar si tiene o no contenido
				Select Case lstrCodispl
					
					'+Campo obligatorio para la transacción (DP034)
					Case "DP034"
						'+Campo Moneda
						lvntAux = lclsGen_cover.nCurrency
						If lvntAux <> eRemoteDB.Constants.intNull Then
							'+Campo Concepto de facturación
							lvntAux = lclsGen_cover.nBill_item
							
							If lvntAux <> eRemoteDB.Constants.intNull Then
								'+Campo ramos Interfac. Contabilidad
								lvntAux = lclsGen_cover.nBranch_led
								
								If lvntAux <> eRemoteDB.Constants.intNull Then
									'+Campo ramos Interfac. Estadística
									lvntAux = lclsGen_cover.nBranch_est
									
									If lvntAux <> eRemoteDB.Constants.intNull Then
										
										'+Campo ramos Interfac. Genérico si el producto es de tipo multirriesgo
										
										If sBrancht = CStr(Product.pmBrancht.pmMedicalAtention) Then
											lvntAux = lclsGen_cover.nBranch_gen
										End If
									End If
								End If
							End If
						End If
						
						'+Campo obligatorio para la transacción (DP052)
					Case "DP052"
						lvntAux = lclsGen_cover.sAddReini
						
						'+Campos opcionales para la transacción (DP052A)
					Case "DP052A"
						If lclsGen_cover.nCacalfix <> eRemoteDB.Constants.intNull And lclsGen_cover.nCacalfix <> 0 Then
							lvntAux = lclsGen_cover.nCacalfix
						Else
							If lclsGen_cover.sCacalfri <> String.Empty Then
								lvntAux = lclsGen_cover.sCacalfri
							Else
								If lclsGen_cover.sCacalili <> String.Empty Then
									lvntAux = lclsGen_cover.sCacalili
								Else
									If lclsGen_cover.nCacalper <> eRemoteDB.Constants.intNull And lclsGen_cover.nCacalper <> 0 Then
										lvntAux = lclsGen_cover.nCacalper
									Else
										If lclsSumcov_apl.FindByCover(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
											lvntAux = 1
										Else
											If lclsGen_cover.sRoucapit <> String.Empty And lclsGen_cover.sRoucapit <> String.Empty Then
												lvntAux = lclsGen_cover.sRoucapit
											Else
												'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
												lvntAux = System.DBNull.Value
											End If
										End If
									End If
								End If
							End If
						End If
						
						'+Campo obligatorio para la transacción (DP035)
					Case "DP035"
						lclTab_modul = New Tab_modul
                        Call lclTab_modul.Find(nBranch, nProduct, nModulec, dEffecdate)
                        lvntAux = 0
                        If lclTab_modul.styp_rat = "1" Then
                            lvntAux = 1
                        End If
						'UPGRADE_NOTE: Object lclTab_modul may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						lclTab_modul = Nothing
                        If lvntAux <> 1 Then
                            '+Si el campo "En cobertura" (nCover_in), esta lleno ninguno de los campos asociados a la determinación de la prima de la cobertura en tratamiento puede tener valor 
                            If lclsGen_cover.nCover_in <> eRemoteDB.Constants.intNull Then
                                lvntAux = lclsGen_cover.nCover_in
                            Else
                                If lclsGen_cover.nPremifix <> eRemoteDB.Constants.intNull And lclsGen_cover.nPremifix <> 0 Then
                                    lvntAux = lclsGen_cover.nPremifix
                                Else
                                    If lclsGen_cover.nId_table <> eRemoteDB.Constants.intNull Then
                                        lvntAux = lclsGen_cover.nId_table
                                    Else
                                        If Trim(lclsGen_cover.sRoupremi) <> String.Empty Then
                                            lvntAux = lclsGen_cover.sRoupremi
                                        Else
                                            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                            If lclsGen_cover.nPremirat <> eRemoteDB.Constants.intNull And lclsGen_cover.nPremirat <> 0 Then
                                                lvntAux = lclsGen_cover.nPremirat
                                            Else
                                                If lclsGen_cover.sChange_typ <> String.Empty And lclsGen_cover.sChange_typ <> "0" Then
                                                    lvntAux = lclsGen_cover.sChange_typ
                                                Else
                                                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                                                    lvntAux = System.DBNull.Value
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        '+Campo obligatorio para la transacción (DP035A)

                    Case "DP035A"
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						lvntAux = IIf(lclsGen_cover.sFrantype <> String.Empty, lclsGen_cover.sFrantype, System.DBNull.Value)
						
					Case "DP049"
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						lvntAux = System.DBNull.Value
						lclsCl_cov_bil = eRemoteDB.NetHelper.CreateClassInstance("eClaim.Cl_cov_bil")
						If lclsCl_cov_bil.valExistDP049(nBranch, nProduct, nModulec, nCover, eRemoteDB.Constants.intNull, dEffecdate) Then
							lvntAux = 1
						End If
                    Case "DP7002"
                        lvntAux = 1
					Case Else
						'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
						lvntAux = System.DBNull.Value
						
				End Select
				
				lintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
				
				'+ Se asigna la imagen asociada a la página asociada al Codispl
				
				'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"'
                'If lvntAux = eRemoteDB.Constants.intNull Or lvntAux.ToString = String.Empty Or lvntAux = eRemoteDB.Constants.dtmNull Or IsDBNull(lvntAux) Then
                If (IsNumeric(lvntAux) AndAlso lvntAux = eRemoteDB.Constants.intNull) Or _
                   (TypeName(lvntAux) = "String" AndAlso lvntAux = String.Empty) Or _
                   (TypeName(lvntAux) = "Date" AndAlso lvntAux = eRemoteDB.Constants.dtmNull) Or _
                   IsDBNull(lvntAux) Then
                    '+Ventanas sin contenido
                    If lstrCodispl = "DP049" Then
                        If lstrEnaDP049 = "Requerida" Then
                            lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                        End If
                    Else
                        If lstrCodispl <> "DP035A" And lstrCodispl <> "DP052" Then
                            lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
                        End If
                    End If
                Else
                    '+Ventanas con contenido
                    lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
                End If

                lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), lintPageImage)
            End If
		Next lintCount
		
		LoadTabsGenCover = lstrHTMLCode & lclsSequence.closeTable()
		
LoadTabsGenCover_err: 
		If Err.Number Then
			LoadTabsGenCover = "LoadTabsGenCover: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCl_cov_bil = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsGen_cover = Nothing
		'UPGRADE_NOTE: Object lclsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSumcov_apl = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
	End Function
	
	'% LoadTabsLifeCover: Esta función es la encarga de carga la información necesaria para cada
    '%                    pestaña que será mostrada para coberturas de vida
    Public Function InsExistsCoverUse(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecCal0150X As eRemoteDB.Execute

        On Error GoTo InsExistsCoverUse_Err

        InsExistsCoverUse = False

        lrecCal0150X = New eRemoteDB.Execute

        With lrecCal0150X
            .StoredProcedure = "REAEXISTCOVERUSE"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then

                If .Parameters.Item("nExists").Value = 1 Then
                    InsExistsCoverUse = True
                End If
            End If
        End With

        lrecCal0150X = Nothing

InsExistsCoverUse_Err:
        If Err.Number Then
            InsExistsCoverUse = False
        End If
        On Error GoTo 0
    End Function
	Private Function LoadTabsLifeCover(ByVal bQuery As Boolean, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal dEffecdate As Date, ByVal nCover As Integer, ByVal nCovergen As Integer) As String
		Dim lclsLife_cover As eProduct.Life_cover
		Dim lclsCl_cov_bil As eProduct.Cl_cov_bil
		Dim lclsQuery As eRemoteDB.Query
		Dim lclsSequence As eFunctions.Sequence
		
		Dim lintCount As Integer
		Dim lintAux As Integer
		Dim lstrHTMLCode As String
		Dim lintAction As Integer
		Dim lintPageImage As eFunctions.Sequence.etypeImageSequence
		
		On Error GoTo LoadTabsLifeCover_err
		
		lclsLife_cover = New eProduct.Life_cover
		lclsQuery = New eRemoteDB.Query
		lclsSequence = New eFunctions.Sequence
		
		'-Se define la variable lstrCodispl en la cual se almacena el código de la ventana
		'-extraído de la constante cstrWindows
		
		Dim lstrCodispl As String
		
		lclsLife_cover.Find(nBranch, nProduct, nModulec, nCover, dEffecdate)
		
		lstrHTMLCode = lclsSequence.makeTable
		
		lintAux = 1
		lintAction = IIf(bQuery, eFunctions.Menues.TypeActions.clngActionQuery, eFunctions.Menues.TypeActions.clngActionUpdate)
		
		With lclsLife_cover
			For lintCount = 1 To CN_FRAMESNUMLIFECOVER
				
				'+ Se extrae el código de la ventana
				lstrCodispl = Mid(CN_WINDOWSLIFECOV, lintAux, 8)
				lintAux = lintAux + 8
				
				Call lclsQuery.OpenQuery("Windows", "sCodisp, sCodispl, sShort_des", "sCodispl='" & lstrCodispl & "'")
				
				Select Case Trim(lstrCodispl)
					'+ Se obtiene por cada transacción un campo (requerido) de la misma para identificar
					'+ si tiene o no contenido
					Case "DP018P"
						If .nBill_item = eRemoteDB.Constants.intNull Then
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
						Else
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
					Case "DP50BP"
						lintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						If .nInterest <> eRemoteDB.Constants.intNull Or .nPer_tabmor <> eRemoteDB.Constants.intNull Or .nCaextexp <> eRemoteDB.Constants.intNull Or .nCaintexp <> eRemoteDB.Constants.intNull Or .nPrextexp <> eRemoteDB.Constants.intNull Or .nPrintexp <> eRemoteDB.Constants.intNull Or .sRoureser <> String.Empty Or .sRousurre <> String.Empty Then
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
						
					Case "DP049"
						lclsCl_cov_bil = New eProduct.Cl_cov_bil
						If lclsCl_cov_bil.valCl_Cov_BilByProduct(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						Else
							lintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
						End If
				End Select
				
				
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lclsQuery.FieldToClass("sCodisp"), lstrCodispl, lintAction, lclsQuery.FieldToClass("sShort_des"), lintPageImage)
				
			Next lintCount
		End With
		LoadTabsLifeCover = lstrHTMLCode & lclsSequence.closeTable()
		
LoadTabsLifeCover_err: 
		If Err.Number Then
			LoadTabsLifeCover = "LoadTabsLifeCover: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsLife_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsLife_cover = Nothing
		'UPGRADE_NOTE: Object lclsQuery may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsQuery = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
		'UPGRADE_NOTE: Object lclsCl_cov_bil may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCl_cov_bil = Nothing
	End Function
	
	'% getDescript: toma la descripción de la cobertura genérica
	Public Function getDescript(ByVal sBrancht As String, ByVal nCovergen As Integer) As String
		Dim lclsTab_lifcov As Tab_lifcov
		Dim lclsTab_GenCov As Tab_gencov
		
		On Error GoTo getDescript_err
		
        'If sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Then
        If (sBrancht = CStr(Product.pmBrancht.pmlife) Or sBrancht = CStr(Product.pmBrancht.pmNotTraditionalLife) Or sBrancht = CStr(Product.pmBrancht.pmMedicalAtention)) Then
            lclsTab_lifcov = New Tab_lifcov
            With lclsTab_lifcov
                If .Find(nCovergen) Then
                    getDescript = .sDescript
                End If
            End With
        Else
            lclsTab_GenCov = New Tab_gencov
            With lclsTab_GenCov
                If .Find(nCovergen) Then
                    getDescript = .sDescript
                End If
            End With
        End If

getDescript_err:
        If Err.Number Then
            getDescript = "getDescript: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsTab_lifcov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_lifcov = Nothing
        'UPGRADE_NOTE: Object lclsTab_GenCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTab_GenCov = Nothing
    End Function
	
	'% Find_Count: Permite indicar si existe menos de una cobertura
	Public Function Count(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date) As Integer
		Dim lrecreaGen_cover_count As eRemoteDB.Execute
		
		On Error GoTo Count_Err
		
		lrecreaGen_cover_count = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaGen_cover_count'
		'Información leída el 10/05/2001 13:15:00
		
		With lrecreaGen_cover_count
			.StoredProcedure = "reaGen_cover_count"
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
		'UPGRADE_NOTE: Object lrecreaGen_cover_count may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover_count = Nothing
	End Function
	
	'%Find_Desc:Permite obtener la descripcion de la cobertura generica asociada a Gen_cover
	Public Function Find_Desc(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaGen_cover_desc As eRemoteDB.Execute
		
		On Error GoTo Find_Desc_Err
		
		lrecreaGen_cover_desc = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaGen_cover_desc'
		'Información leída el 10/05/2001 14:00:10
		
		If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nCover <> nCover Or Me.dEffecdate <> dEffecdate Then
			
			With lrecreaGen_cover_desc
				.StoredProcedure = "reaGen_cover_desc"
				.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					sDescript = .FieldToClass("sDescript")
					Find_Desc = True
					.RCloseRec()
				End If
			End With
		Else
			Find_Desc = True
		End If
		
Find_Desc_Err: 
		If Err.Number Then
			Find_Desc = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaGen_cover_desc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover_desc = Nothing
	End Function
	
	'% insPreDP052A: permite obtener la información de necesaria para el manejo de la ventana
	Public Function insPreDP052A(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsSumcov_apl As Sumcov_apl
		Dim lcolBas_suminses As Bas_suminses
		
		lclsSumcov_apl = New Sumcov_apl
		lcolBas_suminses = New Bas_suminses
		
		On Error GoTo insPreDP052A_Err
		
		mblnFindExist = lclsSumcov_apl.FindExist(nBranch, nProduct, nModulec, nCover, dEffecdate)
		mblnCapitalBasic = lcolBas_suminses.Find(nBranch, nProduct, dEffecdate,  , "DP052A", nModulec, nCover)
		
		If Find(nBranch, nProduct, nModulec, nCover, dEffecdate) Then
			mblnOtherCover = Count(nBranch, nProduct, dEffecdate) > 1
			Call Find_Desc(nBranch, nProduct, nCover, dEffecdate)
			sCacalfri = IIf(sCacalfri <> String.Empty, sCacalfri, "2")
			sCacalili = IIf(sCacalili <> String.Empty, sCacalili, "2")
			sBas_sumins = IIf(sBas_sumins <> String.Empty, sBas_sumins, "2")
			nCacalper = IIf(nCacalper <> eRemoteDB.Constants.intNull, nCacalper, 0)
			nCacalfix = IIf(nCacalfix <> eRemoteDB.Constants.intNull, nCacalfix, 0)
			nCacalmax = IIf(nCacalmax <> eRemoteDB.Constants.intNull, nCacalmax, 0)
			nCacalmin = IIf(nCacalmin <> eRemoteDB.Constants.intNull, nCacalmin, 0)
		End If
		
insPreDP052A_Err: 
		If Err.Number Then
			insPreDP052A = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsSumcov_apl may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSumcov_apl = Nothing
		'UPGRADE_NOTE: Object lcolBas_suminses may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lcolBas_suminses = Nothing
	End Function
	
	'% DefaultValueDP052A: esta Rutina se encarga de desplegar los valores correspondientes
	'%                     a la determinacion del capital
	Public Function DefaultValueDP052A(ByVal sField As String) As String
        Dim lstrReturnValue As String = ""

        Select Case sField
			Case "valOtherCover"
				lstrReturnValue = sDescript
				
			Case "optCapitalFree"
				If sCacalfri = "1" Then
					lstrReturnValue = "1"
				ElseIf sCacalili = "1" Then 
					lstrReturnValue = "2"
				End If
				
			Case "optBas_Sumins"
				If sBas_sumins = "1" Then
					lstrReturnValue = "1"
				ElseIf sBas_sumins = "1" Then 
					lstrReturnValue = "2"
				End If
				
			Case "optCapitalUnlim"
				If nCacalmin <> 0 And nCacalmax <> 0 Then
					lstrReturnValue = "1"
				Else
					lstrReturnValue = "2"
				End If
				
			Case "optCapitalUnlim.disabled"
				If nCacalmin <> 0 And nCacalmax <> 0 Then
					lstrReturnValue = "true"
				Else
					lstrReturnValue = "false"
				End If
				
			Case "optCapitalFix"
				lstrReturnValue = IIf(nCacalfix <> 0, "1", "2")
				
			Case "tcnCapitalFix.disabled"
				lstrReturnValue = IIf(nCacalfix <> 0, "false", "true")
				
			Case "optOtherCover"
				lstrReturnValue = IIf(nCacalper <> 0, "1", "2")
				
			Case "optOtherCover.disabled", "valOtherCover.disabled", "tcnOtherCover.disabled"
				lstrReturnValue = IIf(mblnOtherCover, "false", "true")
				
			Case "optCapitalBasic"
				lstrReturnValue = IIf(mblnFindExist, "1", "2")
				
			Case "optCapitalBasic.disabled"
				lstrReturnValue = IIf(mblnCapitalBasic, "false", "true")
				
			Case "optOnlyRout"
				If sCacalili <> "1" And nCacalper <> 0 And nCacalfix <> 0 And Not mblnOtherCover Then
					lstrReturnValue = "true"
				End If
		End Select
		DefaultValueDP052A = lstrReturnValue
	End Function
	
	'% CoverInProduct: Valida que una cobertura no este asociada a un producto
	Public Function CoverInProduct(ByVal nCover As Integer) As Boolean
		Dim lrecGen_cover As eRemoteDB.Execute
		
		On Error GoTo CoverInProduct_Err
		
		lrecGen_cover = New eRemoteDB.Execute
		
		CoverInProduct = False
		
		With lrecGen_cover
			.StoredProcedure = "reaGen_cover_2"
			.Parameters.Add("nCovergen", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If lrecGen_cover.Run Then
				CoverInProduct = True
				.RCloseRec()
			End If
		End With
		
CoverInProduct_Err: 
		If Err.Number Then
			CoverInProduct = False
		End If
		'UPGRADE_NOTE: Object lrecGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecGen_cover = Nothing
	End Function
	
	'% FindCoverGen_Product: retorna las coberturas asociadas a una cobertura genérica
	Public Function FindCoverGen_Product(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nModulec As Integer, ByVal nCovergen As Integer) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo FindCoverGen_Product_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "reaGen_coverCoverGen"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nCover = .FieldToClass("nCover")
				sDescript = .FieldToClass("sDescript")
				FindCoverGen_Product = True
				.RCloseRec()
			End If
		End With
		
FindCoverGen_Product_Err: 
		If Err.Number Then
			FindCoverGen_Product = False
		End If
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function
	
	'% Class_Initialize: se inicializan los valores de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		Call InitValues()
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'% Find_Percent: devuelve el porcentaje a aplicar para el modulo
	Public Function Find_Percent(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lrecreaGen_cover As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaGen_cover = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.reaGen_cover'
		'Información leída el 31/10/2001 12:32 a.m.
		
		nApply_Perc = 0
		
		With lrecreaGen_cover
			.StoredProcedure = "reaGen_cover_Percent"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nApply_Perc", nApply_Perc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Run(False)
			Find_Percent = .Parameters.Item("nApply_Perc").Value = 1
			
		End With
		
Find_Err: 
		If Err.Number Then
			Find_Percent = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecreaGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaGen_cover = Nothing
    End Function

    '%Find_Desc:Permite obtener la moneda de la cobertura generica asociada a Gen_cover 
    Public Function Find_Currency(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaGen_cover_curr As eRemoteDB.Execute

        lrecreaGen_cover_curr = New eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'insudb.reaGen_cover_desc'
        'Información leída el 10/05/2001 14:00:10

        If Me.nBranch <> nBranch Or Me.nProduct <> nProduct Or Me.nCover <> nCover Or Me.dEffecdate <> dEffecdate Then

            With lrecreaGen_cover_curr
                .StoredProcedure = "reaGen_cover_curr"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If .Run Then
                    nCurrency = .FieldToClass("nCurrency")
                    Find_Currency = True
                    .RCloseRec()
                End If
            End With
        Else
            Find_Currency = True
        End If

        lrecreaGen_cover_curr = Nothing

        Exit Function
    End Function

    '% LoadTabsLifeCover: Esta función es la encarga de carga la información necesaria para cada
    '%                    pestaña que será mostrada para coberturas de vida
    Public Function InsExistsCoverPrint(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal dEffecdate As Date, ByVal nPrint_order As Integer) As Boolean
        Dim lrecCal0150X As eRemoteDB.Execute
        Dim nExist As Integer

        On Error GoTo InsExistsCoverUse_Err

        InsExistsCoverPrint = False

        lrecCal0150X = New eRemoteDB.Execute

        With lrecCal0150X
            .StoredProcedure = "REAEXISTCOVERPRINT"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPrint_order", nPrint_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then

                If .Parameters.Item("nExists").Value >= 1 Then
                    InsExistsCoverPrint = True
                End If
            End If
        End With

        lrecCal0150X = Nothing

InsExistsCoverUse_Err:
        If Err.Number Then
            InsExistsCoverPrint = False
        End If
        On Error GoTo 0
    End Function

End Class






