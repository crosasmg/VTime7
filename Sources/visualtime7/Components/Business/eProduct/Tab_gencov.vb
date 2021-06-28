Option Strict Off
Option Explicit On
Public Class Tab_gencov
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_gencov.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 12:36p                               $%'
	'% $Revision:: 31                                       $%'
	'%-------------------------------------------------------%'
	
	'**- Column accourding to the table on 12/28/2000
	'-Columnas segun tabla al 28/12/2000
	
	'+  Column_name        Type                   Length      Prec  Scale Nullable                            TrimTrailingBlanks                  FixedLenNullInSource
	'--------------------- ---------------------- ----------- ----- ----- ----------------------------------- ----------------------------------- --------------------
	Public nCovergen As Integer 'smallint   2           5     0     no                                  (n/a)                               (n/a)
	Public sAutomrep As String 'char       1                       yes                                 no                                  yes
	Public nBranch_led As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_est As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_gen As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public nBranch_rei As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public nCacalfix As Double 'decimal    9           12    0     yes                                 (n/a)                               (n/a)
	Public sCacalfri As String 'char       1                       yes                                 no                                  yes
	Public sCacalili As String 'char       1                       yes                                 no                                  yes
	Public sCacalrei As String 'char       1                       yes                                 no                                  yes
	Public nCover_in As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public nCurrency As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public sDescript As String 'char       30                      yes                                 no                                  yes
	Public sFrancApl As String 'char       1                       yes                                 no                                  yes
	Public nFrancFix As Double 'decimal    9           10    0     yes                                 (n/a)                               (n/a)
	Public nFrancMax As Double 'decimal    9           10    0     yes                                 (n/a)                               (n/a)
	Public nFrancMin As Double 'decimal    9           10    0     yes                                 (n/a)                               (n/a)
	Public nFrancrat As Double 'decimal    5           4     2     yes                                 (n/a)                               (n/a)
	Public sFrantype As String 'char       1                       yes                                 no                                  yes
	Public nMedreser As Double 'decimal    9           12    0     yes                                 (n/a)                               (n/a)
	Public nPremifix As Double 'decimal    9           10    2     yes                                 (n/a)                               (n/a)
	Public nPremimax As Double 'decimal    9           10    2     yes                                 (n/a)                               (n/a)
	Public nPremimin As Double 'decimal    9           10    2     yes                                 (n/a)                               (n/a)
	Public nPremirat As Double 'decimal    5           9     6     yes                                 (n/a)                               (n/a)
	Public sRoucapit As String 'char       12                      yes                                 no                                  yes
	Public sRoufranc As String 'char       12                      yes                                 no                                  yes
	Public sRoupremi As String 'char       12                      yes                                 no                                  yes
	Public sRoureser As String 'char       12                      yes                                 no                                  yes
	Public sShort_des As String 'char       12                      yes                                 no                                  yes
	Public sStatregt As String 'char       1                       yes                                 no                                  yes
	Public nUsercode As Integer 'smallint   2           5     0     yes                                 (n/a)                               (n/a)
	Public sCondSVS As String 'char       10                      no
	Public sInforProv As String 'char       1                       yes
	Public sProvider As String 'char       13                      yes
    Public sRisk As String 'char       1                       yes
    Public nFrancRatCla As Double
    Public nFrancFixCla As Double
    Public nFranxMinCla As Double
    Public nFranxMaxCla As Double
    Public sRouFrancCla As String

	
	'**-Define the auxiliary properties that will be use in the transaction
	'**- DP039 - Consult of hte generic coverages
	'- Se definen las propiedades auxiliares a ser utilizadas en la transacción
	'- DP039 - Consulta de coberturas genéricas.
	Public sCheck As String
	
	'**- Auxiliary variables
	'-Variables auxiliares
	Public WithInformation As String
	Private sSource As String
	
	'% ClientExist_death: Indica la existencia o no del cliente muerto
	Public ReadOnly Property ClientExist_death(ByVal sClient As String) As Boolean
		Get
			Dim lobjTab_gencov As eRemoteDB.Execute
			
			On Error GoTo ClientExist_death_err
			
			lobjTab_gencov = New eRemoteDB.Execute
			
			With lobjTab_gencov
				.StoredProcedure = "reaClient_death"
				.Parameters.Add("sProvider", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				ClientExist_death = .Run
			End With
			
ClientExist_death_err: 
			If Err.Number Then
				ClientExist_death = False
			End If
			'UPGRADE_NOTE: Object lobjTab_gencov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjTab_gencov = Nothing
		End Get
	End Property
	
	'**%Add: Add records to the table Tab_Gencov
	'%Add: Agrega registros a la tabla Tab_Gencov
	Public Function Add() As Boolean
		Dim lreccreTab_gencov As eRemoteDB.Execute
		
		lreccreTab_gencov = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.reaTab_gencov_2'
		'**+ Data read on 09/07/2001 2:05:10 p.m.
		'Definición de parámetros para stored procedure 'insudb.creTab_gencov'
		'Información leída el 07/09/2001 2:05:10 PM
		With lreccreTab_gencov
			.StoredProcedure = "creTab_gencov"
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomrep", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalili", sCacalili, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalrei", sCacalrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancfix", nFrancFix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancmax", nFrancMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancmin", nFrancMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 6, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancrat", nFrancrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMedreser", nMedreser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremifix", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoufranc", sRoufranc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoupremi", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInforProv", sInforProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProvider", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRisk", sRisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancRatCla", nFrancRatCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancFixCla", nFrancFixCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranxMinCla", nFranxMinCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranxMaxCla", nFranxMaxCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRouFrancCla", sRouFrancCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreTab_gencov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTab_gencov = Nothing
		On Error GoTo 0
	End Function
	
	'**%Delete: Delete table records
	'%Delete: Borra los registros de la tabla
	Public Function Delete(ByVal nCover As Integer) As Boolean
		Dim lrecdelTab_GenCov As eRemoteDB.Execute
		
		On Error GoTo Delete_Err
		lrecdelTab_GenCov = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.delTab_GenCov'
		'**+ Data read on 09/10/2001 11:47:00 a.m.
		'Definición de parámetros para stored procedure 'insudb.delTab_GenCov'
		'Información leída el 10/09/2001 11:47:00 AM
		With lrecdelTab_GenCov
			.StoredProcedure = "delTab_GenCov"
			.Parameters.Add("nCovergen", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
		
Delete_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTab_GenCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTab_GenCov = Nothing
		On Error GoTo 0
	End Function
	
	'**% Find: function that read the table data
	'% Find: función que lee los datos de la tabla
	Public Function Find(ByVal nCovergen As Integer, Optional ByVal bFind As Boolean = False) As Boolean
		Dim lrecreaTab_gencov_2 As eRemoteDB.Execute
		
		On Error GoTo Find_Err
		
		lrecreaTab_gencov_2 = New eRemoteDB.Execute
		
		If nCovergen <> Me.nCovergen Or bFind Then
			With lrecreaTab_gencov_2
				.StoredProcedure = "reaTab_gencov_2"
				.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				If .Run Then
					Find = True
					Me.nCovergen = .FieldToClass("nCovergen")
					sAutomrep = .FieldToClass("sAutomrep")
					nBranch_led = .FieldToClass("nBranch_led")
					nBranch_est = .FieldToClass("nBranch_est")
					nBranch_gen = .FieldToClass("nBranch_gen")
					nBranch_rei = .FieldToClass("nBranch_rei")
					nCacalfix = .FieldToClass("nCacalfix")
					sCacalfri = .FieldToClass("sCacalfri")
					sCacalili = .FieldToClass("sCacalili")
					sCacalrei = .FieldToClass("sCacalrei")
					nCover_in = .FieldToClass("nCover_in")
					nCurrency = .FieldToClass("nCurrency")
					sDescript = .FieldToClass("sDescript")
					sFrancApl = .FieldToClass("sFrancapl")
					nFrancFix = .FieldToClass("nFrancfix")
					nFrancrat = .FieldToClass("nFrancrat")
					nFrancMax = .FieldToClass("nFrancmax")
					nFrancMin = .FieldToClass("nFrancmin")
					sFrantype = .FieldToClass("sFrantype")
					nMedreser = .FieldToClass("nMedreser")
					nPremifix = .FieldToClass("nPremifix")
					nPremimax = .FieldToClass("nPremimax")
					nPremimin = .FieldToClass("nPremimin")
					nPremirat = .FieldToClass("nPremirat")
					sRoucapit = .FieldToClass("sRoucapit")
					sRoufranc = .FieldToClass("sRoufranc")
					sRoupremi = .FieldToClass("sRoupremi")
					sRoureser = .FieldToClass("sRoureser")
					sShort_des = .FieldToClass("sShort_des")
					sStatregt = .FieldToClass("sStatregt")
					sCondSVS = .FieldToClass("sCondSVS")
					sInforProv = .FieldToClass("sInforProv")
					sProvider = .FieldToClass("sProvider")
                    sRisk = .FieldToClass("sRisk")
                    nFrancRatCla = .FieldToClass("nFrancRatCla")
                    nFrancFixCla = .FieldToClass("nFrancFixCla")
                    nFranxMinCla = .FieldToClass("nFranxMinCla")
                    nFranxMaxCla = .FieldToClass("nFranxMaxCla")
                    sRouFrancCla = .FieldToClass("sRouFrancCla")
                    .RCloseRec()
				Else
					Find = False
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
		'UPGRADE_NOTE: Object lrecreaTab_gencov_2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecreaTab_gencov_2 = Nothing
	End Function
	
	'%**Update: Update the table Tab_gencov
	'%Update: Actualiza la tabla Tab_gencov
	Public Function Update() As Boolean
		Dim lrecInsUpdTab_GenCov As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		lrecInsUpdTab_GenCov = New eRemoteDB.Execute
		
		'**+ Parameters definition for the stored procedure 'insudb.InsUpdTab_GenCov'
		'**+ Data read on 09/07/2001 2:48:31 p.m.
		'Definición de parámetros para stored procedure 'insudb.InsUpdTab_GenCov'
		'Información leída el 07/09/2001 2:48:31 PM
		With lrecInsUpdTab_GenCov
			.StoredProcedure = "InsUpdTab_GenCov"
			.Parameters.Add("nCovergen", nCovergen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSource", sSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAutomrep", sAutomrep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_led", nBranch_led, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_est", nBranch_est, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_gen", nBranch_gen, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch_rei", nBranch_rei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCacalfix", nCacalfix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalfri", sCacalfri, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalili", sCacalili, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCacalrei", sCacalrei, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover_in", nCover_in, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 120, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrancapl", sFrancApl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancfix", nFrancFix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmax", nFrancMax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancmin", nFrancMin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFrancrat", nFrancrat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sFrantype", sFrantype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nMedreser", nMedreser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremifix", nPremifix, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimax", nPremimax, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremimin", nPremimin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremirat", nPremirat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoucapit", sRoucapit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoufranc", sRoufranc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoupremi", sRoupremi, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoureser", sRoureser, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCondSVS", sCondSVS, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sInforProv", sInforProv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sProvider", sProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRisk", sRisk, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancRatCla", nFrancRatCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFrancFixCla", nFrancFixCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranxMinCla", nFranxMinCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFranxMaxCla", nFranxMaxCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRouFrancCla", sRouFrancCla, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecInsUpdTab_GenCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_GenCov = Nothing
	End Function
	
	'**% InsValDP029_K: Validate the data of the DP029_K
	'% InsValDP029_K: Valida los datos de la DP029_K
	Public Function InsValDP029_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCover As Integer, Optional ByVal bDupCover As Boolean = False) As String
		
		Dim lclsErrors As eFunctions.Errors
		Dim lclsGen_cover As Gen_cover
		Dim lblnFound As Boolean
		
		On Error GoTo InsValDP029_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			If nCover = eRemoteDB.Constants.intNull Then
				.ErrorMessage(sCodispl, 11163)
			Else
				lblnFound = Me.Find(nCover, True)
				Select Case nAction
					Case eFunctions.Menues.TypeActions.clngActionadd
						If lblnFound Then
							.ErrorMessage(sCodispl, 11104)
						End If
						
					Case eFunctions.Menues.TypeActions.clngActionUpdate, eFunctions.Menues.TypeActions.clngActionQuery
						If Not lblnFound Then
							.ErrorMessage(sCodispl, 11007)
						End If
						
					Case eFunctions.Menues.TypeActions.clngActionDuplicate
						If bDupCover Then
							If lblnFound Then
								.ErrorMessage(sCodispl, 11104)
							End If
						Else
							If Not lblnFound Then
								.ErrorMessage(sCodispl, 11007)
							End If
						End If
						
					Case eFunctions.Menues.TypeActions.clngActioncut
						If Not lblnFound Then
							.ErrorMessage(sCodispl, 11007)
						Else
							lclsGen_cover = New Gen_cover
							If lclsGen_cover.CoverInProduct(nCover) Then
								.ErrorMessage(sCodispl, 11410)
							End If
							'UPGRADE_NOTE: Object lclsGen_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
							lclsGen_cover = Nothing
						End If
				End Select
			End If
			InsValDP029_K = .Confirm
		End With
InsValDP029_K_Err: 
		If Err.Number Then
			InsValDP029_K = "InsValDP029_K: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		On Error GoTo 0
	End Function
	
	
	'**% InsPostDP029_K: Update the data of the DP029_K
	'% InsPostDP029_K: Actualiza los datos de la DP029_K
	Public Function InsPostDP029_K(ByVal nAction As Integer, ByVal nCover As Integer, ByVal nUsercode As Integer, Optional ByVal nNewCover As Integer = 0) As Boolean
		On Error GoTo InsPostDP029_K_Err
		
		InsPostDP029_K = True
		With Me
			If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
				.nCovergen = nCover
				.nUsercode = nUsercode
				.sStatregt = "2"
				InsPostDP029_K = .Add
			ElseIf nAction = eFunctions.Menues.TypeActions.clngActionDuplicate And nNewCover > 0 Then 
				If .Find(nCover, True) Then
					.nCovergen = nNewCover
					.nUsercode = nUsercode
					InsPostDP029_K = .Add
				End If
			End If
		End With
		
InsPostDP029_K_Err: 
		If Err.Number Then
			InsPostDP029_K = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValDP029: Validates the page sCodispl as described in the functional specifications
	'%InsValDP029: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana sCodispl
	Public Function insValDP029(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCover As Integer, ByVal sDescription As String, ByVal sShortDesc As String, ByVal nCurrency As Integer, ByVal nBranch_led As Integer, ByVal nBranch_rei As Integer, ByVal nBranch_Sta As Integer, ByVal nBranch_gen As Integer, ByVal sCondSVS As String, ByVal sInforProv As String, ByVal sProvider As String, ByVal sProvider_Digit As String) As String
		Dim lobjErrors As eFunctions.Errors
		Dim lclsTab_GenCov As eProduct.Tab_gencov
		Dim lobjClient As Object
		
		On Error GoTo insValDP029_err
		
		insValDP029 = String.Empty
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			
			lobjErrors = New eFunctions.Errors
			lclsTab_GenCov = New eProduct.Tab_gencov
			
			With lobjErrors
				'**+Validate that the description field of the coverage is full
				'+ Se valida que el campo descripcion de la cobertura este lleno
				If sDescription = String.Empty Then
					.ErrorMessage(sCodispl, 10010)
				End If
				
				'**+ Validate the Short description field
				'+ Se valida el campo Descripcion Corta
				If sShortDesc = String.Empty Then
					.ErrorMessage(sCodispl, 10011)
				End If
				
				'**+ Validate that the currency combo has value
				'+ Se valida que el combo de monedas tenga valor
				If nCurrency = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 1351)
				End If
				
				'**+ Validate that the countable branch combo has value
				'+ Se valida que el combo de ramos contables tenga valor
				If nBranch_led = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11309)
				End If
				
				'+ Se valida que el combo de Clasificación de SVS tenga valor
				If nBranch_Sta = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11320)
				End If
				
				'**+ Validate that the generics branch combo has value
				'+ Se valida que el combo de ramos genericos tenga valor
				If nBranch_gen = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11310)
				End If
				
				'+ Se valida que el Condicionado esté lleno
				If sCondSVS = String.Empty Then
					.ErrorMessage(sCodispl, 99152)
				End If
				
				'+ Si se indicó "Informar al prestador de servicio", debe ser un código válido
				If sInforProv <> String.Empty Then
					If sProvider_Digit = String.Empty Then
						.ErrorMessage(sCodispl, 2012)
					Else
						lobjClient = eRemoteDB.NetHelper.CreateClassInstance("eClient.Client")
						If Not lobjClient.Find(sProvider) Then
							.ErrorMessage(sCodispl, 2012)
						End If
					End If
					
					'+ Se valida si el cliente está muerto
					If sProvider <> String.Empty Then
						lclsTab_GenCov.sProvider = sProvider
						If lclsTab_GenCov.ClientExist_death(sProvider) Then
							.ErrorMessage(sCodispl, 2051)
						End If
					End If
				End If
				
				insValDP029 = .Confirm
			End With
		End If
		
insValDP029_err: 
		If Err.Number Then
			insValDP029 = "insValDP029: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lobjClient may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjClient = Nothing
		'UPGRADE_NOTE: Object lclsTab_GenCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_GenCov = Nothing
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
	End Function
	
	'**%insPostDP029: Updates the database (as described in the functional specifications)
	'**%for the page "DP029"
	'%insPostDP029: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP029"
	Public Function insPostDP029(ByVal nAction As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nCurrency As Integer, ByVal sAutomrep As String, ByVal ldblMedreser As Double, ByVal sRoureser As String, ByVal nBranch_led As Integer, ByVal nBranch_rei As Integer, ByVal nBranch_est As Integer, ByVal nBranch_gen As Integer, ByVal nCovergen As Integer, ByVal sCondSVS As String, ByVal sInforProv As String, ByVal sProvider As String, ByVal sRisk As String, ByVal nUsercode As Integer) As Boolean
		On Error GoTo insPostDP029_err
		
		insPostDP029 = True
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			With Me
				sSource = "1"
				.sDescript = sDescript
				.sShort_des = sShort_des
				.nCurrency = nCurrency
				.sAutomrep = IIf(sAutomrep = String.Empty, "2", sAutomrep)
				.nMedreser = ldblMedreser
				.sRoureser = sRoureser
				.nBranch_led = nBranch_led
				.nBranch_rei = nBranch_rei
				.nBranch_est = nBranch_est
				.nBranch_gen = nBranch_gen
				.nCovergen = nCovergen
				.sCondSVS = sCondSVS
				.sInforProv = IIf(sInforProv = "1", "1", "2")
				.sProvider = sProvider
				.sRisk = IIf(sRisk = "1", "1", "2")
				.nUsercode = nUsercode
				insPostDP029 = .Update
			End With
		End If
		
insPostDP029_err: 
		If Err.Number Then
			insPostDP029 = False
		End If
		On Error GoTo 0
	End Function
	
	'**%insValDP030A: Validates the page "DP030A" as described in the functional specifications
	'%InsValDP030A: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana "DP030A"
	Public Function insValDP030A(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sOptCapital As String, ByVal nCapitalFix As Double, ByVal sCapitalRou As String) As String
		Dim lobjErrors As eFunctions.Errors
		
		On Error GoTo insValDP030A_err
		
		insValDP030A = String.Empty
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			
			lobjErrors = New eFunctions.Errors
			
			With lobjErrors
				'+ Se valida que el campo capital fijo este lleno
				If sOptCapital = "3" And nCapitalFix = eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 11326)
				End If
				
				'+Se verifica que se haya seleccionado un metodo de calculo para el capital de la cobertura
				If sOptCapital <> "1" And sOptCapital <> "2" And sOptCapital <> "3" And sCapitalRou = String.Empty Then
					.ErrorMessage(sCodispl, 11331)
				End If
				
				insValDP030A = .Confirm
			End With
			'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			lobjErrors = Nothing
		End If
		
insValDP030A_err: 
		If Err.Number Then
			insValDP030A = "insValDP030A: " & Err.Description
		End If
		On Error GoTo 0
	End Function
	
	'**%InsPostDP030A: Updates the database (as described in the functional specifications)
	'**%for the page "DP030A"
	'%InsPostDP030A: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP030A"
	Public Function InsPostDP030A(ByVal nAction As Integer, ByVal sCacalrei As String, ByVal sOptCapital As String, ByVal nCacalfix As Double, ByVal sRoucapit As String, ByVal nCovergen As Integer, ByVal nUsercode As Integer) As Boolean
		On Error GoTo InsPostDP030A_err
		
		InsPostDP030A = True
		
		If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
			With Me
				sSource = "2"
				.sCacalrei = sCacalrei
				.sCacalfri = IIf(sOptCapital = "1", 1, "2")
				.sCacalili = IIf(sOptCapital = "2", 1, "2")
				.nCacalfix = nCacalfix
				.sRoucapit = sRoucapit
				.nCovergen = nCovergen
				.nUsercode = nUsercode
				InsPostDP030A = .Update
			End With
		End If
		
InsPostDP030A_err: 
		If Err.Number Then
			InsPostDP030A = False
		End If
		On Error GoTo 0
		
	End Function
	
	'**%insValDP030B: Validates the page sCodispl as described in the functional specifications
	'%InsValDP030B: Este metodo se encarga de realizar las validaciones descritas en el funcional
	'%de la ventana sCodispl
    Public Function insValDP030B(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nCoverIn As Integer, ByVal sPremiumRou As String, ByVal nPremiumFix As Double, ByVal nRate As Double, ByVal nPremiumMin As Double, ByVal nPremiumMax As Double, ByVal nFranchiseFix As Double, ByVal nFranchiseMin As Double, ByVal nFranchiseMax As Double, ByVal soptType As String, ByVal sFranchiseRou As String, ByVal nFranchiseRate As Double, ByVal nFrancRatCla As Double, ByVal nFrancFixCla As Double, ByVal nFranxMinCla As Double, ByVal nFranxMaxCla As Double, ByVal sRouFrancCla As String, ByVal soptAplied As String) As String
        Dim lobjErrors As eFunctions.Errors

        On Error GoTo insValDP030B_err

        insValDP030B = String.Empty

        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then

            lobjErrors = New eFunctions.Errors
            With lobjErrors

                '+ Se valida que al menos uno de los campos de calculo de prima se encuentre lleno
                If nCoverIn = eRemoteDB.Constants.intNull And sPremiumRou = String.Empty And nPremiumFix = eRemoteDB.Constants.intNull And nRate = eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11327)
                End If

                '+ Si se selecciona alguna cobertura del combo "En Cobertura" esta debe tener forma de calculo de prima
                If nCoverIn <> eRemoteDB.Constants.intNull Then
                    If Not Me.valGenCovMethodCal(nCoverIn) Then
                        .ErrorMessage(sCodispl, 11152)
                    End If

                    '+Si el campo "En cobertura" esta lleno los campos correspondientes al calculo de prima deben
                    '+estar vacios a excepcion del nombre de rutina
                    If nPremiumFix <> eRemoteDB.Constants.intNull Or nRate <> eRemoteDB.Constants.intNull Or nPremiumMin <> eRemoteDB.Constants.intNull Or nPremiumMax <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11153, , eFunctions.Errors.TextAlign.RigthAling, "(sólo Rutina)")
                    End If
                End If

                '+Si el campo prima fija tiene valor no se puede incluir porcentaje
                If nPremiumFix <> eRemoteDB.Constants.intNull And nRate <> eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 3044)
                End If

                '+ Si el campo prima fija tiene algun valor no se puede introducir valores en prima maxima
                If nPremiumFix <> eRemoteDB.Constants.intNull And nPremiumMax <> eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11076)
                End If

                If nPremiumMax <> eRemoteDB.Constants.intNull And nPremiumMin <> eRemoteDB.Constants.intNull Then
                    If nPremiumMax <= nPremiumMin Then
                        .ErrorMessage(sCodispl, 11048)
                    End If
                End If

                '+ Si el campo prima fija tiene algun valor no se puede introducir valores en prima minima
                If nPremiumFix <> eRemoteDB.Constants.intNull And nPremiumMin <> eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11076)
                End If

                '+ Si el campo importe fijo de franq/deduc. tiene algun valor no se puede introducir valores en importe minimo
                If nFranchiseFix <> eRemoteDB.Constants.intNull And nFranchiseMin <> eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11076)
                End If

                '+Si se incluye un importe fijo de franq/deduc no debe indicarse importe maximo
                If nFranchiseFix <> eRemoteDB.Constants.intNull And nFranchiseMax <> eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11076)
                End If

                If nFranchiseMax <> eRemoteDB.Constants.intNull And nFranchiseMin <> eRemoteDB.Constants.intNull Then
                    If nFranchiseMax <= nFranchiseMin Then
                        .ErrorMessage(sCodispl, 11048)
                    End If
                End If

                '+ Si el campo importe fijo de franq/deduc. tiene algun valor no se puede introducir valores en porcentaje de franq/deduc
                If nFranchiseFix <> eRemoteDB.Constants.intNull Then
                    If nFranchiseRate <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11075)
                    End If
                End If

                '+ Si el campo importe fijo de franq/deduc. tiene algun valor no se puede introducir valores en porcentaje de siniestro
                If nFrancFixCla <> eRemoteDB.Constants.intNull Then
                    If nFrancRatCla <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11075)
                    End If
                End If

                '+ Se valida que al menos uno de los campos correspondientes a franq/deduc se encuentre lleno
                '+si se encuentra activado el check box de Capital o Ambos
                If soptType <> "1" And soptAplied = "2" Or soptAplied = "4" Then
                    If sFranchiseRou = String.Empty And nFranchiseFix = eRemoteDB.Constants.intNull And nFranchiseRate = eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11319)
                    End If
                End If

                '+ Se valida que al menos uno de los campos correspondientes a franq/deduc se encuentre lleno
                '+si se encuentra activado el check box de Siniestro o Ambos
                If soptType <> "1" And soptAplied = "3" Or soptAplied = "4" Then
                    If sRouFrancCla = String.Empty And nFrancFixCla = eRemoteDB.Constants.intNull And nFrancRatCla = eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11319)
                    End If
                End If

                '+Si se incluye un importe fijo de siniestro de franq/deduc no debe indicarse importe maximo de siniestro
                If nFrancFixCla <> eRemoteDB.Constants.intNull And nFranxMinCla <> eRemoteDB.Constants.intNull And nFranxMaxCla <> eRemoteDB.Constants.intNull Then
                    .ErrorMessage(sCodispl, 11076)
                End If

                'Si Aplica sobre siniestro máximo tiene valor debe ser mayor la campo Aplica sobre siniestro mínimo
                If nFranxMaxCla <> eRemoteDB.Constants.intNull And nFranxMinCla <> eRemoteDB.Constants.intNull Then
                    If nFranxMaxCla <= nFranxMinCla Then
                        .ErrorMessage(sCodispl, 11048)
                    End If
                End If

                insValDP030B = .Confirm
            End With
            'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lobjErrors = Nothing
        End If

insValDP030B_err:
        If Err.Number Then
            insValDP030B = "insValDP030B: " & Err.Description
        End If
        On Error GoTo 0
    End Function
	
	'**%InsPostDP030B: Updates the database (as described in the functional specifications)
	'**%for the page "DP030B"
	'%InsPostDP030B: Este metodo se encarga de realizar el impacto en la base de datos (descrito en las
	'%especificaciones funcionales)de la ventana "DP030B"
    Public Function InsPostDP030B(ByVal nAction As Integer, ByVal nCovergen As Integer, ByVal sRoupremi As String, ByVal nCover_in As Integer, ByVal nPremifix As Double, ByVal nPremirat As Double, ByVal nPremimin As Double, ByVal nPremimax As Double, ByVal sFrantype As String, ByVal sFrancApl As String, ByVal sRoufranc As String, ByVal nFrancrat As Double, ByVal nFrancFix As Double, ByVal nFrancMin As Double, ByVal nFrancMax As Double, ByVal nFrancRatCla As Double, ByVal nFrancFixCla As Double, ByVal nFranxMinCla As Double, ByVal nFranxMaxCla As Double, ByVal sRouFrancCla As String, ByVal nUsercode As Integer) As Boolean
        On Error GoTo InsPostDP030B_err

        InsPostDP030B = True

        If nAction = eFunctions.Menues.TypeActions.clngActionadd Or nAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
            With Me
                sSource = "3"
                .sRoupremi = sRoupremi
                .nCover_in = nCover_in
                .nPremifix = nPremifix
                .nPremirat = nPremirat
                .nPremimin = nPremimin
                .nPremimax = nPremimax
                .sFrantype = sFrantype
                .sFrancApl = sFrancApl
                .sRoufranc = sRoufranc
                .nFrancrat = nFrancrat
                .nFrancFix = nFrancFix
                .nFrancMax = nFrancMax
                .nFrancMin = nFrancMin
                .nCovergen = nCovergen
                .nFrancRatCla = nFrancRatCla
                .nFrancFixCla = nFrancFixCla
                .nFranxMinCla = nFranxMinCla
                .nFranxMaxCla = nFranxMaxCla
                .sRouFrancCla = sRouFrancCla
                .nUsercode = nUsercode
                InsPostDP030B = .Update
            End With
        End If

InsPostDP030B_err:
        If Err.Number Then
            InsPostDP030B = False
        End If
        On Error GoTo 0
    End Function
	
	'**% inValHeaderDP039: Permit to make the headers validation in the transaction DP039 - Consult
	'**% of generic coverages.
	'% insValHeaderDP039: Permite realizar las validaciones del encabezado de la transacción DP039 - Consulta
	'% de coberturas genéricas.
	Public Function insValHeaderDP039(ByVal sCodispl As String, ByVal nCurrency As Integer) As String
		Dim lobjErrors As eFunctions.Errors
		On Error GoTo insValHeaderDP039_err
		lobjErrors = New eFunctions.Errors
		insValHeaderDP039 = String.Empty
		
		'**+ Validate that the currency combo has value
		'+ Se valida que el combo de monedas tenga valor.
		If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
			lobjErrors.ErrorMessage(sCodispl, 1351)
		End If
		insValHeaderDP039 = lobjErrors.Confirm
		
insValHeaderDP039_err: 
		If Err.Number Then
			insValHeaderDP039 = "insValHeaderDP039: " & Err.Description
		End If
		'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lobjErrors = Nothing
		On Error GoTo 0
	End Function
	
	'**% insPreDP039: Create one unique object for the generic coverages consult - DP039
	'% insPreDP039: Crea un único objeto para la consulta de coberturas genéricas - DP039.
	Public Function insPreDP039(ByVal nCurrency As Integer, ByVal nTypCov As Integer) As Object
		'**+ The consult is by "Life coverages"
		'+ La consulta es por "Coberturas de Vida".
		
		If nTypCov = 1 Then
			insPreDP039 = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Tab_lifcovs")
			insPreDP039.Find(nCurrency, IIf(nCurrency = -1, "1", "2"), True)
		ElseIf nTypCov = 2 Then 
			
			'**+ The consult is by "Non life coverages"
			'+ La consulta es por "Coberturas de No Vida".
			
			insPreDP039 = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Tab_gencovs")
			insPreDP039.Find(nCurrency, IIf(nCurrency = -1, "1", "2"), True)
		Else
			
			'**+ The consult is for "All the coverages"
			'+ La consulta es para "Todas las coberturas".
			
			insPreDP039 = eRemoteDB.NetHelper.CreateClassInstance("eProduct.Tab_gencovs")
			insPreDP039.FindAllCovergen(nCurrency, IIf(nCurrency = -1, "1", "2"), True)
		End If
		
	End Function
	
	'**%LoadTabs: fix the sequence for the general branch and life coverages
	'%LoadTabs: arma la secuencia para las coberturas de ramos generales y de vida
	Public Function LoadTabs(ByVal sCodispl As String, ByVal nAction As Integer, ByVal sUserSchema As String, ByVal nCover As Integer) As Object
		Const CN_WINDOWS_LIFE As String = "DP018G  DP019G  DP050G  "
		Const CN_WINDOWS_GEN As String = "DP029   DP030A  DP030B  "
		
		Dim lrecWindows As eRemoteDB.Query
		Dim lclsSecurSche As eSecurity.Secur_sche
		Dim mintPageImage As eFunctions.Sequence.etypeImageSequence
		Dim lintCountWindows As Integer
        Dim lstrCodisp As String = ""
        Dim lstrCodispl As String
        Dim lstrShort_desc As String = ""
        Dim lblnContent As Boolean
		Dim lblnRequired As Boolean
		Dim lstrHTMLCode As String
		Dim lclsSequence As eFunctions.Sequence
		Dim lstrWindows As String
		
		On Error GoTo LoadTabs_Err
		
		lclsSecurSche = New eSecurity.Secur_sche
		lclsSequence = New eFunctions.Sequence
		lrecWindows = New eRemoteDB.Query
		
		If sCodispl = "DP018G_K" Or sCodispl = "DP018G" Or sCodispl = "DP019G" Or sCodispl = "DP050G" Then
			lstrWindows = CN_WINDOWS_LIFE
			sCodispl = "DP018"
		Else
			lstrWindows = CN_WINDOWS_GEN
			sCodispl = "DP029_K"
		End If
		
		lstrHTMLCode = String.Empty
		lblnRequired = True
		
		If ValContent(sCodispl, nCover) Then
			
			lstrHTMLCode = lclsSequence.makeTable
			lintCountWindows = 1
			lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
			Do While Trim(lstrCodispl) <> String.Empty
				'**+ Assigned the values to the content variables
				'+ Se asignan los valores a las variables de contenido
				If InStr(1, WithInformation, Trim(lstrCodispl)) <> 0 Then
					lblnContent = True
				Else
					lblnContent = False
				End If
				'**+Assined the values to the description variables
				'+ Se asignan los valores a las variables de descripcion
				
				With lrecWindows
					If .OpenQuery("Windows", "sCodisp, sShort_des", "sCodispl='" & Trim(lstrCodispl) & "'") Then
						lstrCodisp = .FieldToClass("sCodisp")
						lstrShort_desc = .FieldToClass("sShort_des")
						.CloseQuery()
					End If
				End With
				
				'**+ Search the image to put in the links
				'+ Se busca la imagen a colocar en los links
				With lclsSecurSche
					If Not .valTransAccess(sUserSchema, lstrCodisp, "1") Then
						If lblnContent Then
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedOK
						Else
							If lblnRequired Then
								mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedReq
							Else
								mintPageImage = eFunctions.Sequence.etypeImageSequence.eDeniedS
							End If
						End If
					Else
						If Not lblnContent Then
							If lblnRequired Then
								mintPageImage = eFunctions.Sequence.etypeImageSequence.eRequired
							Else
								mintPageImage = eFunctions.Sequence.etypeImageSequence.eEmpty
							End If
						Else
							mintPageImage = eFunctions.Sequence.etypeImageSequence.eOK
						End If
					End If
				End With
				
				lstrHTMLCode = lstrHTMLCode & lclsSequence.makeRow(lstrCodisp, lstrCodispl, nAction, lstrShort_desc, mintPageImage)
				
				'**+ It moves to the next found record
				'+ Se mueve al siguiente registro encontrado
				lintCountWindows = lintCountWindows + 8
				lstrCodispl = Mid(lstrWindows, lintCountWindows, 8)
			Loop 
			lstrHTMLCode = lstrHTMLCode & lclsSequence.closeTable()
		End If
		
		LoadTabs = lstrHTMLCode
		
LoadTabs_Err: 
		If Err.Number Then
			LoadTabs = "LoadTabs: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsSecurSche may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSecurSche = Nothing
		'UPGRADE_NOTE: Object lrecWindows may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecWindows = Nothing
		'UPGRADE_NOTE: Object lclsSequence may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsSequence = Nothing
	End Function
	
	'**%ValContent: Obtain the required windows for the generic life coverage as well as general
	'%ValContent: Obtiene las ventanas requeridas para las coberturas genéricas
	'%             tanto de vida como de generales
	Public Function ValContent(ByVal sCodispl As String, ByVal nCover As Integer) As Boolean
		Dim lrecinsValContent_Cover As eRemoteDB.Execute
		
		On Error GoTo ValContent_Err
		
		lrecinsValContent_Cover = New eRemoteDB.Execute
		
		'**+parameters definition for the stored procedure 'insudb.insValRequired_TabCover'
		'**+Data read on 07/02/2001 4:03:33 PM
		'+Definición de parámetros para stored procedure 'insudb.insValRequired_TabCover'
		'+Información leída el 02/07/2001 4:03:33 PM
		
		With lrecinsValContent_Cover
			.StoredProcedure = "insValRequired_TabCover"
			.Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				Me.WithInformation = .FieldToClass("WithInformation")
				ValContent = True
				.RCloseRec()
			End If
		End With
		
ValContent_Err: 
		If Err.Number Then
			ValContent = False
		End If
		'UPGRADE_NOTE: Object lrecinsValContent_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinsValContent_Cover = Nothing
		On Error GoTo 0
	End Function
	
	'%valGenCovMethodCal. Esta funcion se encarga de leer la tabla de coberturas genericas y verificar
	'%que la cobertura en tratamiento tenga algun metodo de calculo de prima.
	Public Function valGenCovMethodCal(ByVal llngCover As Integer) As Boolean
		Dim lclsTab_GenCov As Tab_gencov
		
		lclsTab_GenCov = New Tab_gencov
		With lclsTab_GenCov
			If .Find(llngCover) Then
				valGenCovMethodCal = Not ((.sRoupremi = String.Empty Or .sRoupremi = String.Empty) And .nPremifix = eRemoteDB.Constants.intNull And .nPremirat = eRemoteDB.Constants.intNull And .nCover_in = eRemoteDB.Constants.intNull)
			Else
				valGenCovMethodCal = False
			End If
		End With
		'UPGRADE_NOTE: Object lclsTab_GenCov may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsTab_GenCov = Nothing
	End Function
	
	'**%UpdateStatus: Update the cover status
	'%UpdateStatus: Actualiza el estado de la cobertura
	Public Function UpdateStatus(ByVal nCover As Integer, ByVal sStatregt As String) As Boolean
		Dim lrecupdTab_gencovState As eRemoteDB.Execute
		
		On Error GoTo UpdateStatus_Err
		lrecupdTab_gencovState = New eRemoteDB.Execute
		
		'Definición de parámetros para stored procedure 'insudb.updTab_gencovState'
		'Información leída el 06/07/2001 2:03:55 PM
		With lrecupdTab_gencovState
			.StoredProcedure = "updTab_gencovState"
			.Parameters.Add("nCovergen", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			UpdateStatus = .Run(False)
		End With
		
UpdateStatus_Err: 
		If Err.Number Then
			UpdateStatus = False
		End If
		'UPGRADE_NOTE: Object lrecupdTab_gencovState may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTab_gencovState = Nothing
		On Error GoTo 0
	End Function
	
	
	'%Class_Initialize: Procedimiento que se ejecuta al instanciar la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nCovergen = eRemoteDB.Constants.intNull
		sAutomrep = String.Empty
		nBranch_led = eRemoteDB.Constants.intNull
		nBranch_est = eRemoteDB.Constants.intNull
		nBranch_gen = eRemoteDB.Constants.intNull
		nBranch_rei = eRemoteDB.Constants.intNull
		nCacalfix = eRemoteDB.Constants.intNull
		sCacalfri = String.Empty
		sCacalili = String.Empty
		sCacalrei = String.Empty
		nCover_in = eRemoteDB.Constants.intNull
		nCurrency = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		sFrancApl = String.Empty
		nFrancFix = eRemoteDB.Constants.intNull
		nFrancMax = eRemoteDB.Constants.intNull
		nFrancMin = eRemoteDB.Constants.intNull
		nFrancrat = eRemoteDB.Constants.intNull
		sFrantype = String.Empty
		nMedreser = eRemoteDB.Constants.intNull
		nPremifix = eRemoteDB.Constants.intNull
		nPremimax = eRemoteDB.Constants.intNull
		nPremimin = eRemoteDB.Constants.intNull
		nPremirat = eRemoteDB.Constants.intNull
		sRoucapit = String.Empty
		sRoufranc = String.Empty
		sRoupremi = String.Empty
		sRoureser = String.Empty
		sShort_des = String.Empty
		sStatregt = String.Empty
		nUsercode = eRemoteDB.Constants.intNull
		sCheck = String.Empty
		WithInformation = String.Empty
		sSource = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






