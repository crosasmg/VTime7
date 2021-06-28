<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mstrErrors As Object
'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mobjValues As eFunctions.Values
Dim mobjValAgentRep As Object
Dim mobjReport As eReports.Report

Dim sKey As String
Dim mstrKey As String

Dim lstrError As String=String.Empty
Dim lintString As Integer
Dim mstrString As String

'+[APV2] 1014_BB. Calculo de comisiones de APV
Dim mstrMonth As String
Dim mstrYear As String


'% insValAgentRep: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValAgentRep() As Object
	Dim insValPolicy As Boolean
	'--------------------------------------------------------------------------------------------
	
	
	Select Case Request.QueryString.Item("sCodispl")
		'+AGL001:Preparación ctas. ctes. intermediarios
		Case "AGL001"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				
				insValAgentRep = mobjValAgentRep.insValAGL001_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ AGL002: Actualización ctas. ctes. por préstamo
		Case "AGL002"
			With Request
				insValAgentRep = New eAgent.Intermedia
                    insValAgentRep = insValAgentRep.insValAGL002_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ AGL955: Cálculo de estipendios
		Case "AGL955"
			With Request
				mobjValAgentRep = New eAgent.ValAgentRep
				insValAgentRep = mobjValAgentRep.insValAGL955_k(.Form.Item("sOptInfo"), mobjValues.StringToType(.Form.Item("nYear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nMonth"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
			End With

                '+AGL960: Solicitud de pago de comisiones de estipendio
            Case "AGL960"
                mobjValAgentRep = New eAgent.ValAgentRep
                With Request
                    insValAgentRep = mobjValAgentRep.insValAGL960_K("AGL960", mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValue_date"), eFunctions.Values.eTypeData.etdDate))
                End With
                
                '+ AGL003: Listado de las ctas ctes intermediarios.
		Case "AGL003"
			With Request
				insValAgentRep = New eAgent.Agents
				insValAgentRep = insValAgentRep.insValAGL003_K(.QueryString("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optClient"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valClient"))
			End With
			
			'+ AGL005: Listado de comisiones por recibo
		Case "AGL005"
			With Request
				mobjValAgentRep = New eAgent.Agents
				insValAgentRep = mobjValAgentRep.insvalAGL005_K(mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ AGL008: Traspaso de cartera de intermediarios - 16/02/2002
		Case "AGL008"
			mobjValAgentRep = New eAgent.Intermedia
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				With Request
					insValAgentRep = mobjValAgentRep.insValAGL008_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcdProcDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valInterBefore"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valInterNew"), eFunctions.Values.eTypeData.etdDouble))
				End With
				
				Session("dDateProcess") = mobjValues.StringToType(Request.Form.Item("tcdProcDat"), eFunctions.Values.eTypeData.etdDate)
				Session("nInsur_Area") = mobjValues.StringToType(Request.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble)
				Session("nMunicipality") = mobjValues.StringToType(Request.Form.Item("cbeMunicipality"), eFunctions.Values.eTypeData.etdDouble)
				Session("nInterBefore") = mobjValues.StringToType(Request.Form.Item("valInterBefore"), eFunctions.Values.eTypeData.etdDouble)
				Session("nBranch") = mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
				Session("nProduct") = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
				Session("sOptProcess") = Request.Form.Item("optProcess")
				Session("sTypeBusiness") = Request.Form.Item("optBusiness")
				Session("sTypePolicy") = Request.Form.Item("optPolicy")
				Session("nInterNew") = Request.Form.Item("valInterNew")
				Session("schkSaldo") = Request.Form.Item("chkSaldo")
				
				If CStr(Session("schkSaldo")) <> "1" Then
					Session("schkSaldo") = "0"
				End If
				
				Session("sOptProcTyp") = Request.Form.Item("OptProcTyp")
			End If
			
			'+AGL009: Solicitud de pago de comisiones
		Case "AGL009"
			mobjValAgentRep = New eAgent.Intermedia
			With Request
                    insValAgentRep = mobjValAgentRep.insValAGL009_K("AGL009", mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdValue_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPay_comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDocSupport"), eFunctions.Values.eTypeData.etdDouble))
				
				
			End With
			
			'+ AGL014: Listado de préstamos / anticipos - 11/03/2004
		Case "AGL014"
			mobjValAgentRep = New eAgent.Intermedia
			With Request
				insValAgentRep = mobjValAgentRep.insValAGL014_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcnTypeClient"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClientCode"), mobjValues.StringToType(.Form.Item("tcdStardate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ AGL583: Parametros incent. de Super. Generales
		Case "AGL583"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL583_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeInterTyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+AGL596: Incentivos de agentes 
		Case "AGL596"
			
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL596_K(.QueryString("sCodispl"), .Form.Item("cboINTERTYP"), mobjValues.StringToType(.Form.Item("tcdfinicial"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdffinal"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+AGL603: Bono de cumplimiento-Generales
		Case "AGL603"
			
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL603(.Form.Item("cboINTERTYP"), mobjValues.StringToType(.Form.Item("tcdfinicial"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdffinal"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+AGL605: Preparación de cuentas corrientes
		Case "AGL605"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL605_K("AGL605", mobjValues.StringToType(.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateProc"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optTyp"))
			End With
			
			'+AGL618: Incentivos de agentes de mantención
		Case "AGL618"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL618_K("AGL618", mobjValues.StringToType(.Form.Item("cboIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitialDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFinalDate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+AGL620: Liquidación de comisiones 
		Case "AGL620"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL620_K("AGL620", mobjValues.StringToType(.Form.Item("optTyp_Proc_Aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPay_Comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateVal"), eFunctions.Values.eTypeData.etdDate), .Form.Item("optTyp"))
			End With

			'+AGL621: Cartola de intermediarios
		Case "AGL621"
			With Request
                mobjValAgentRep = New eAgent.AccountStatement
                insValAgentRep = mobjValAgentRep.insValAGL621_K("AGL621", 0, "0", String.Empty, mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End With

			'+AGL703: LT Comisiones Pagadas
		Case "AGL703"
			With Request
				If .QueryString.Item("nZone") = "1" Then
					mobjValAgentRep = New eAgent.Intermedia
					insValAgentRep = mobjValAgentRep.insValAGL703_k(mobjValues.StringToType(.Form.Item("tcdDate_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDate_end"), eFunctions.Values.eTypeData.etdDate))
				Else
					insValAgentRep = True
				End If
			End With
			
			'+AGL728: Carga de comisiones por póliza
		Case "AGL728"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL728_K("AGL728", mobjValues.StringToType(.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate))
				
			End With
			
			
			
			'+AGL771:Transferencia de datos del intermediario
		Case "AGL771"
			With Request
				mobjValAgentRep = New eBatch.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL771(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeInter_typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+AGL772: Interfaz de anticipos de intermediarios
		Case "AGL772"
			mobjValAgentRep = New eBatch.Intermedia
			With Request
				insValAgentRep = mobjValAgentRep.insValAGL772("AGL772", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate, True))
			End With
			
			'+AGL774-5: Intefaces de liquidaciones de comisiones 
		Case "AGL774", "AGL775"
			With Request
				mobjValAgentRep = New eBatch.Intermedia
				insValAgentRep = mobjValAgentRep.insValLiq_Comm(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIntertyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate))
				
			End With
			
			'+ AGL7000: Comisiones del producto APV.
			'+[APV2] 1014_BB. Calculo de comisiones de APV
		Case "AGL7000"
			With Request
				mobjValAgentRep = New eBatch.Intermedia
				insValAgentRep = mobjValAgentRep.insValAGL7000(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ AGL919: Obtención de información para la FECU
		Case "AGL919"
			mobjValAgentRep = New eAgent.ValAgentRep
			With Request
				insValAgentRep = mobjValAgentRep.insvalAGL919_K("AGL919", mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ AGL920: Producción y remuneración de intermediarios de seguros
		Case "AGL920"
			mobjValAgentRep = New eAgent.ValAgentRep
			With Request
				insValAgentRep = mobjValAgentRep.insvalAGL920_K("AGL920", mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ AGL921: Certificados de intermediarios
		Case "AGL921"
			mobjValAgentRep = New eAgent.ValAgentRep
			With Request
				insValAgentRep = mobjValAgentRep.insvalAGL921_K("AGL921", mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ AGL922: Generación de archivos FECU
		Case "AGL922"
			mobjValAgentRep = New eAgent.ValAgentRep
			With Request
				insValAgentRep = mobjValAgentRep.insvalAGL922_K("AGL922", mobjValues.StringToType(.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate))
			End With
		Case "AGL786"
			If Request.QueryString.Item("nZone") <> "2" Then
				mobjValAgentRep = New eAgent.ValAgentRep
				insValAgentRep = mobjValAgentRep.insValAGL786_k("AGL786", mobjValues.StringToDate(Request.Form.Item("tcdDateFrom")), mobjValues.StringToDate(Request.Form.Item("tcdDateTo")), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong))
				
				
				'+		    Set mobjValAgentRep = Nothing   		    
			Else
				insValPolicy = True
			End If
			'+AGL918 Resumen de Liquidaciones de comisiones
		Case "AGL918"
			mobjValAgentRep = New eAgent.ValAgentRep
			insValAgentRep = mobjValAgentRep.insValAGL918_k(mobjValues.StringToType(Request.Form.Item("tcddatefrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcddateto"), eFunctions.Values.eTypeData.etdDate))
			'+AGL8001 Cálculo de persistencia
		Case "AGL8001"
			mobjValAgentRep = New eAgent.ValAgentRep
			insValAgentRep = mobjValAgentRep.insValAGL8001_k("AGL8001", mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cboMonth"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("optPreliminary"), Session("nUsercode"))
			
		Case Else
			insValAgentRep = "insValAgentRep: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostAgentRep: Se efectua el proceso
'--------------------------------------------------------------------------------------------
Private Function insPostAgentRep() As Boolean
	Dim lintCount As Integer
	Dim lintIndex As Short
	Dim lstrSel As String
	Dim lintCheck As Object
	Dim lintSelected As Byte
	'--------------------------------------------------------------------------------------------
	'-Objeto para transacciones batch	
	Dim lclsBatch_param As eSchedule.Batch_param
	'-Indicador de imprimir reportes
	Dim lblnPrintReport As Object
	Dim mobjGeneralFunction As eGeneral.GeneralFunction
	
	Dim lblnPost As Boolean
	Dim lclsErrors As eFunctions.Errors
	Dim lnAGL620 As Object
	
	lblnPost = False
	
	Dim lclsPay_Comm As eAgent.ValAgentRep
	Dim mclsAgl703 As eAgent.ValAgentRep
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ Preparation of intermediary current accounts
		Case "AGL001"
			With Request
				lblnPost = mobjValAgentRep.insPostAGL001(mobjValues.StringToDate(Request.Form.Item("tcdInitDate")), mobjValues.StringToDate(Request.Form.Item("tcdEndDate")), .Form.Item("chkUpdate_Ind"), Session("nUsercode"))
				If lblnPost Then
					insPrintDocuments()
				End If
			End With
			'+AGL918 Resumen de Liquidaciones de comisiones
		Case "AGL918"
			mobjValAgentRep = New eAgent.ValAgentRep
			If Request.Form.Item("optReport") = "1" Then
				lblnPost = mobjValAgentRep.insPostAGL918(mobjValues.StringToType(Request.Form.Item("tcddatefrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcddateto"), eFunctions.Values.eTypeData.etdDate))
			Else
				lblnPost = mobjValAgentRep.insPostAGL918B(mobjValues.StringToType(Request.Form.Item("tcddatefrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcddateto"), eFunctions.Values.eTypeData.etdDate))
			End If
			If lblnPost Then
				sKey = mobjValAgentRep.p_skey
				insPrintDocuments()
			End If
			
			'+ Listado de Actualizacion de ctas ctes por préstamo.
		Case "AGL002"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lblnPost = True
				insPrintDocuments()
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 108
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("chkLoansDelay"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdProcessDate"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optProcess"))
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				
				lblnPost = True
			End If
			
			'+ AGL955: Cálculo de estipendios
            Case "AGL955"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    With Request
                        mobjValAgentRep = New eAgent.ValAgentRep
                        lblnPost = mobjValAgentRep.insPostAGL955_k(.Form.Item("sOptInfo"), mobjValues.StringToType(.Form.Item("nYear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("nMonth"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mstrKey, mobjValues.StringToType(.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("lblEffecdate"), eFunctions.Values.eTypeData.etdDate, True), .Form.Item("optproccess"))
                    End With
			
                    sKey = mobjValAgentRep.sKey
			
                    Call insPrintDocuments()
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 7903
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("sOptInfo"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nYear"), eFunctions.Values.eTypeData.etdLong))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nMonth"), eFunctions.Values.eTypeData.etdLong))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("lblEffecdate"), eFunctions.Values.eTypeData.etdDate, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 0)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optproccess"))
                        
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optproccess"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("nYear"), eFunctions.Values.eTypeData.etdLong))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("nMonth"), eFunctions.Values.eTypeData.etdLong))

                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "\n\n Dirigirse a Procesos Masivos');</" & "Script>")
                    lclsBatch_param = Nothing
				
                    lblnPost = True
                    
                End If
                
                '+ AGL003: Listado de las ctas ctes intermediarios.

            Case "AGL960"
                mobjValAgentRep = New eAgent.ValAgentRep
                With Request
                    lblnPost = mobjValAgentRep.insPostAGL960_K(mobjValues.StringToType(.Form.Item("nContrat_Pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdValue_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optproccess"))
                End With
               
                sKey = mobjValAgentRep.sKey
                
                Call insPrintDocuments()
		Case "AGL003"
			lblnPost = True
			insPrintDocuments()
			
		Case "AGL005"
			lblnPost = True
			Call insPrintDocuments()
			
		Case "AGL583"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				lblnPost = mobjValAgentRep.insPostAGL583(mobjValues.StringToType(.Form.Item("cbeInterTyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdStartDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+AGL596: Incentivos de agentes 	
		Case "AGL596"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				lblnPost = mobjValAgentRep.insPostAGL596(.Form.Item("cboINTERTYP"), mobjValues.StringToType(.Form.Item("tcdfinicial"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdffinal"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
				
			End With
			
			'+AGL618: Incentivos de agentes de mantención
		Case "AGL618"
			With Request
				mobjValAgentRep = New eAgent.Intermedia
				lblnPost = mobjValAgentRep.insPostAGL618(mobjValues.StringToType(.Form.Item("cboIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdInitialDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFinalDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End With
                
			'+AGL620: Liquidación de comisiones		
		Case "AGL620"
			If CStr(Session("BatchEnabled")) <> "1" Then
				With Request
					mobjValAgentRep = New eAgent.Intermedia
					If .Form.Item("optTyp_Proc_Aux") <> "2" Then
						lblnPost = mobjValAgentRep.insPostAGL620_K("AGL620", mobjValues.StringToType(.Form.Item("optTyp_Proc_Aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPay_Comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateVal"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optTyp"), .Form.Item("optProcess"))
						Session("nPay_Comm") = mobjValAgentRep.nPay_Comm
					Else
						Session("nPay_Comm") = mobjValues.StringToType(.Form.Item("tcnPay_Comm"), eFunctions.Values.eTypeData.etdDouble)
						lblnPost = True
					End If
				End With
				
				If lblnPost Then
					insPrintDocuments()
				End If
			Else
				lclsBatch_param = New eSchedule.Batch_param
				mobjReport = New eReports.Report
				With lclsBatch_param
					If Request.Form.Item("optTyp") = "1" Then
						.nBatch = 37 '+Vendedores
					Else
						.nBatch = 77 '+Supervisores
					End If
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optTyp_Proc_Aux"), eFunctions.Values.eTypeData.etdDouble))
					lnAGL620 = mobjValues.StringToType(Request.Form.Item("tcnPay_Comm"), eFunctions.Values.eTypeData.etdDouble)
					
					If lnAGL620 = eRemoteDB.Constants.intNull Then
						lnAGL620 = "0"
					End If
					
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(lnAGL620, eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdateEnd"), eFunctions.Values.eTypeData.etdDate))
					
					lnAGL620 = mobjValues.StringToType(Request.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble)
					
					If lnAGL620 = eRemoteDB.Constants.intNull Then
						lnAGL620 = "0"
					End If
					
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble))
					lnAGL620 = mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble)
					
					If lnAGL620 = eRemoteDB.Constants.intNull Then
						lnAGL620 = "0"
					End If
					
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(lnAGL620, eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdateVal"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optTyp"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjReport.setdate(Request.Form.Item("tcdEffecdate")))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjReport.setdate(Request.Form.Item("tcdEffecdateEnd")))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optProcess"))
					.Save()
				End With
				mobjReport = Nothing
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				
				lblnPost = True
			End If
			
			'+AGL621: Cartola de intermediarios
            Case "AGL621"
                With Request
                    lblnPost = mobjValAgentRep.insPostAGL621_K(0, "0", String.Empty, mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble), 0, mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEffecdateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With

            '+AGL728: Carga de comisiones por pólizas
            Case "AGL728"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjValAgentRep = New eAgent.Intermedia
                    With Request
                        lblnPost = mobjValAgentRep.insPostAGL728_K(.Form.Item("optProcess"), mobjValues.StringToType(.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        If lblnPost Then
                            lclsErrors = New eFunctions.Errors
                            lstrError = lclsErrors.ErrorMessage("AGL728_K", 4327, , , , True)
                            lintString = InStr(1, lstrError, "Err.")
                            If lintString > 0 Then
                                lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
                            End If
                            Response.Write(lstrError)
                            lclsErrors = Nothing
                        End If
					
                        If lblnPost Then
                            insPrintDocuments()
                        End If
					
                    End With
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    mobjReport = New eReports.Report
                    With lclsBatch_param
                        .nBatch = 35
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))

                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optProcess"))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    mobjReport = Nothing
                    lclsBatch_param = Nothing
				
                    lblnPost = True
				
                End If
			
                '+AGL603: Bono de cumplimiento-Generales
			
            Case "AGL603"
                With Request
                    mobjValAgentRep = New eAgent.Intermedia
                    lblnPost = mobjValAgentRep.insPostAGL603(Request.Form.Item("cboINTERTYP"), mobjValues.StringToType(.Form.Item("tcdfinicial"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdffinal"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                End With
                '+AGL703: Preparación de cuentas corrientes
            Case "AGL703"
                With Request
                    If .QueryString.Item("nZone") = "1" Then
                        lblnPost = True
                        mstrString = "&dDateIni=" & .Form.Item("tcdDate_ini") & "&dDateEnd=" & .Form.Item("tcdDate_end")
                    Else
                        lintIndex = 0
                        lstrSel = "0"
                        mobjGeneralFunction = New eGeneral.GeneralFunction
                        mstrKey = mobjGeneralFunction.getsKey(Session("nUsercode"))
                        mobjGeneralFunction = Nothing
                        If Not IsNothing(.Form.Item("hddsRequire")) Then
                            For Each lintCheck In .Form.GetValues("hddsRequire")
                                lintIndex = lintIndex + 1
                                If lintCheck = 1 Then
                                    lclsPay_Comm = New eAgent.ValAgentRep
                                    lblnPost = lclsPay_Comm.insPostAGL703_A(CDbl(.Form.GetValues("hddnintertyp").GetValue(lintIndex - 1)), .Form.GetValues("hddsintertyp").GetValue(lintIndex - 1), CDbl(.Form.GetValues("hddnpay_comm").GetValue(lintIndex - 1)), mobjValues.StringToType(.Form.GetValues("hdddpay_date").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hdddprocsup").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hdddval_date").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("hdddcompdate").GetValue(lintIndex - 1), eFunctions.Values.eTypeData.etdDate), mstrKey)
                                    lstrSel = "1"
                                    lclsPay_Comm = Nothing
                                End If
                            Next lintCheck
                        End If
                        If lstrSel = "1" Then
                            mclsAgl703 = New eAgent.ValAgentRep
                            lblnPost = mclsAgl703.insPostAGL703(mobjValues.StringToType(.Form.Item("hdddDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hdddDateEnd"), eFunctions.Values.eTypeData.etdDate), mstrKey)
						
                            sKey = mstrKey
                            mclsAgl703 = Nothing
                            Call insPrintDocuments()
                        End If
                        lblnPost = True
                    End If
                End With
			
                '+AGL605: Preparación de cuentas corrientes
            Case "AGL605"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjValAgentRep = New eAgent.Intermedia
                    With Request
                        lblnPost = mobjValAgentRep.insPostAGL605_K(mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optProcess"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optTyp"))
                    End With
				
                    If lblnPost Then
                        insPrintDocuments()
                    End If
                Else
                    Dim lintInterm_typ As Integer
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        If Request.Form.Item("optTyp") = "1" Then
                            .nBatch = 36
                        Else
                            .nBatch = 76
                        End If
                        
                        If Request.Form.Item("valInterm_Typ") = vbNullString Then
                            lintInterm_typ = 0
                        Else
                            lintInterm_typ = Request.Form.Item("valInterm_Typ")
                        End If
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valInterm_Typ"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optTyp"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))
                        
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdEffecdate"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optProcess"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, lintInterm_typ)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    lblnPost = True
                End If
			
			
			
                '+AGL771:Transferencia de datos del intermediario
            Case "AGL771"
                With Request
                    mobjValAgentRep = New eBatch.Intermedia
                    lblnPost = mobjValAgentRep.insPostAGL771(mobjValues.StringToType(.Form.Item("cbeInter_typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
                End With
			
                '+AGL772: Interfaz de anticipos generados	    
            Case "AGL772"
                mobjValAgentRep = New eBatch.Intermedia
                With Request
                    lblnPost = mobjValAgentRep.insPostAGL772("AGL772", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeIntertyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate, True), Session("nUsercode"))
                End With
			
                If lblnPost Then
                    Response.Write("<SCRIPT> alert('Proceso terminado satisfactoriamente')</" & "Script>")
                End If
			
			
                '+ AGL008: Traspaso de cartera de intermediarios - 16/01/2002
            Case "AGL008"
			
                lintCount = 0
                lintSelected = 0
                With Request
                    If CStr(Session("sOptProcess")) = "2" Then '+ Puntual 
                        lblnPost = True
                        sKey = "1"
                        If Not String.IsNullOrEmpty(Request.Form("Sel")) Then
                            For lintCount = 1 To Request.Form.Item("Sel").Length
                                lintSelected = CDbl(Request.Form.GetValues("Sel").GetValue(lintCount - 1)) + 1
                                mobjValAgentRep = New eAgent.Intermedia
                                lblnPost = mobjValAgentRep.insPostAGL008(mobjValues.StringToType(Session("dDateProcess"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("sCertype").GetValue(lintSelected - 1), mobjValues.StringToType(.Form.GetValues("nBranch").GetValue(lintSelected - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("nProduct").GetValue(lintSelected - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("nPolicy").GetValue(lintSelected - 1), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("dStartdate").GetValue(lintSelected - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.GetValues("dExpirdat").GetValue(lintSelected - 1), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nInterBefore"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInterNew"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.GetValues("nIntermedpol").GetValue(lintSelected - 1), eFunctions.Values.eTypeData.etdDouble), sKey, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sOptProcTyp"))
                                sKey = mobjValAgentRep.sKey
                            Next
                        End If
                    Else
                        lblnPost = True
                    End If
                End With
			
                lintCount = Nothing
                lintSelected = Nothing
			
                If CStr(Session("sOptProcess")) = "1" Then '+ Masivo 
                    If lblnPost Then
                        insPrintDocuments()
                    End If
                Else
                    If (lblnPost And (sKey <> vbNullString And sKey <> "1")) Then
                        insPrintDocuments()
                    End If
                End If
			
                '+ AGL009: Solicitud de pago de comisiones.
            Case "AGL009"               
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjValAgentRep = New eAgent.Intermedia
                    With Request
                        lblnPost = mobjValAgentRep.insPostAGL009_K(mobjValues.StringToType(.Form.Item("cbeInterm_typ"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdValue_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optProcess"), mobjValues.StringToType(.Form.Item("tcnPay_comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeType_Support"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDocSupport"), eFunctions.Values.eTypeData.etdDouble))
                        sKey = mobjValAgentRep.sKey
					
                    End With
				
                    If lblnPost Then
                        insPrintDocuments()
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 8
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeInterm_typ"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdProcess_date"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdValue_date"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        If mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "1")
                        Else
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "2")
                        End If
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))
                        
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPay_comm"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeType_Support"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnDocSupport"), eFunctions.Values.eTypeData.etdDouble))
                        
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("optProcess"))
                        .Save()
                    End With
				
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    lblnPost = True
                End If
			
                '+ AGL014: Listado de préstamos / anticipos
            Case "AGL014"
                lblnPost = True
                If lblnPost Then
                    insPrintDocuments()
                End If
			
                '+ AGL774-5: Intefaces de liquidaciones de comisiones 
            Case "AGL774", "AGL775"
                With Request
                    mobjValAgentRep = New eBatch.Intermedia
                    lblnPost = mobjValAgentRep.insPostLiq_Comm(mobjValues.StringToType(.Form.Item("cbeInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeIntertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdExpirdat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddType_proce"), eFunctions.Values.eTypeData.etdDouble))
                    If lblnPost Then
                        Response.Write("<SCRIPT> alert('Proceso terminado satisfactoriamente')</" & "Script>")
                    Else
                        Response.Write("<SCRIPT> alert('No Existe información a Procesar')</" & "Script>")
                        lblnPost = True
                    End If
                End With
			
                '+ AGL7000: Comisiones del prodcuto APV.
                '+[APV2] 1014_BB. Calculo de comisiones de APV
            Case "AGL7000"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjValAgentRep = New eBatch.Intermedia
				
                    mstrMonth = Request.Form.Item("cbeMonth")
                    mstrYear = CStr(Year(DateSerial(CInt(Request.Form.Item("tcnYear")), CInt(Request.Form.Item("cbeMonth")), 1)))
                    If Len(mstrMonth) = 1 Then
                        mstrMonth = "0" & mstrMonth
                    End If
                    With Request
                        lblnPost = mobjValAgentRep.insPostAGL7000(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mstrMonth, mstrYear, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        If lblnPost Then
                            Call insPrintDocuments()
                        End If
                    End With
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
				
                    mstrMonth = Request.Form.Item("cbeMonth")
                    mstrYear = CStr(Year(DateSerial(CInt(Request.Form.Item("tcnYear")), CInt(Request.Form.Item("cbeMonth")), 1)))
                    If Len(mstrMonth) = 1 Then
                        mstrMonth = "0" & mstrMonth
                    End If
				
                    With lclsBatch_param
                        .nBatch = 70
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrMonth)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mstrYear)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "1") '+Hacer commit
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mstrMonth)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mstrYear)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    lblnPost = True
                End If
			
                '+ AGL919: Obtención de información para la FECU
            Case "AGL919"
                mobjValAgentRep = New eAgent.ValAgentRep
                lblnPost = mobjValAgentRep.inspostAGL919_K(mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
			
                '+ AGL920: Producción y remuneración de intermediarios de seguros
            Case "AGL920"
                insPrintDocuments()
                lblnPost = True
			
                '+ AGL921: Certificados de intermediarios 
            Case "AGL921"
                insPrintDocuments()
                lblnPost = True
			
                '+ AGL922: Generación de archivos FECU
            Case "AGL922"
                mobjValAgentRep = New eAgent.ValAgentRep
                lblnPost = mobjValAgentRep.inspostAGL922_K(mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nSessionID"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
                insPrintDocuments()
                '+ AGL786: Validación de % de comisiones
            Case "AGL786"
                If Request.QueryString.Item("nZone") <> "2" Then
                    mobjValAgentRep = New eAgent.ValAgentRep
                    lblnPost = mobjValAgentRep.insPostAGL786_K(mobjValues.StringToType(Request.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong))
                    Session("sFile_name") = "../../tFiles/" & mobjValAgentRep.sFile_name
                Else
                    lblnPost = True
                End If
                '+AGL8001 Cálculo de persistencia
            Case "AGL8001"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjValAgentRep = New eAgent.ValAgentRep
                    lblnPost = mobjValAgentRep.insPostAGL8001_k(mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Request.Form.Item("cboMonth"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("optPreliminary"), Session("nUsercode"))
                Else
                    '+Se almacenan los parámetros del proceso batch
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 161
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optPreliminary"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdInteger))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cboMonth"), eFunctions.Values.eTypeData.etdInteger))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    lblnPost = True
                End If
			
        End Select
	
	insPostAgentRep = lblnPost
End Function

'%   insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------------
	
	Dim dInitDate As String
	Dim dEndDate As Object
	Dim nOffice As Byte
	Dim nIntermed As Byte
	Dim sClient As String
	Dim mobjDocuments As eReports.Report
	
	mobjDocuments = New eReports.Report
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			Case "AGL001"
				.sCodispl = "AGL001"
				.ReportFilename = "AGL001.RPT"
				If Request.Form.Item("chkUpdate_Ind") = "2" Then
					.setParamField(1, "sTittle", "Cierre de cuentas corrientes de intermediarios")
				Else
					.setParamField(1, "sTittle", "Pre-cierre de cuentas corrientes de intermediarios")
				End If
				.setStorProcParam(1, mobjValAgentRep.sKey)
				Response.Write((.Command))
				
				'+ AGL002: Listado de Actualizacion de ctas ctes por préstamo.
			Case "AGL002"
				With mobjDocuments
					.sCodispl = "AGL002"
					.ReportFilename = "AGL002.rpt"
					.setStorProcParam(1, .setdate(Request.Form.Item("tcdProcessDate")))
					.setStorProcParam(2, Request.Form.Item("cbeInsur_Area"))
					.setStorProcParam(3, Request.Form.Item("chkLoansDelay"))
					.setStorProcParam(4, Session("nUsercode"))
					.setStorProcParam(5, Request.Form.Item("optProcess"))
					Response.Write((.Command))
					.Reset()
				End With
				
				'+ AGL955: Estipendios.
                Case "AGL955"
                    .sCodispl = "AGL955"
                    If Request.Form.Item("sOptinfo") = "1" Then
                        .ReportFilename = "AGL955_2.rpt"
                    Else
                        .ReportFilename = "AGL955.rpt"
                    End If
                    
                    .setParamField(1, "sKey", sKey)
                        
                    .setStorProcParam(1, sKey)
                    .setStorProcParam(2, Request.Form.Item("optproccess"))
                    .setStorProcParam(3, Request.Form.Item("nYear"))
                    .setStorProcParam(4, Request.Form.Item("nMonth"))
                        
                    Response.Write((.Command))
                    .Reset()
                        

                    '+ AGL960: Pago Estipendios.
                Case "AGL960"
                    .sCodispl = "AGL960"
                    .ReportFilename = "AGL960.rpt"
                    .setStorProcParam(1, sKey)
                    .setStorProcParam(2, Request.Form.Item("optProccess"))
                    Response.Write((.Command))
                    .Reset()
				                    
                    
                    '+ Listado de las ctas ctes intermediarios.
                Case "AGL003"
                    With mobjDocuments
                        .sCodispl = "AGL003"
                        If mobjValues.StringToType(Request.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate) = vbNullString Then
                            dInitDate = vbNullString
                        Else
                            dInitDate = mobjValues.StringToType(Request.Form.Item("tcdIniDate"), eFunctions.Values.eTypeData.etdDate)
                        End If
					
                        If mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate) = vbNullString Then
                            dEndDate = Today
                        Else
                            dEndDate = mobjValues.StringToType(Request.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate)
                        End If
					
                        If mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
                            nOffice = 0
                        Else
                            nOffice = mobjValues.StringToType(Request.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble)
                        End If
					
                        If mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble) < 0 Then
                            nIntermed = 0
                        Else
                            nIntermed = mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble)
                        End If
					
                        If Request.Form.Item("valClient") = vbNullString Then
                            sClient = "0"
                        Else
                            sClient = Request.Form.Item("valClient")
                        End If
                        Select Case Request.Form.Item("optList")
                            '+ Detalle
                            Case CStr(0)
                                .ReportFilename = "agl003id.rpt"
                                '+ Resumen
                            Case CStr(1)
                                .ReportFilename = "agl003ir.rpt"
                                '+ Ambos		                
                            Case CStr(2)
                                .ReportFilename = "agl003id.rpt"
                        End Select
					
                        .setStorProcParam(1, .setdate(dInitDate))
                        .setStorProcParam(2, .setdate(dEndDate))
                        .setStorProcParam(3, Request.Form.Item("optList"))
                        .setStorProcParam(4, nOffice)
                        .setStorProcParam(5, nIntermed)
                        .setStorProcParam(6, sClient)
                        Response.Write((.Command))
                        .Reset()
					
                        If Request.Form.Item("optList") = "2" Then
                            .ReportFilename = "agl003ir.rpt"
                            .bTimeOut = True
                            .nTimeOut = 5000
                            Response.Write((.Command))
                        End If
                    End With
				
                    '+ AGL005: Listado de comisiones por recibo			
                Case "AGL005"
                    .sCodispl = "AGL005"
                    .ReportFilename = "AGL005.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdInitdate")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnddate")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    Response.Write((.Command))
				
                    '+ AGL008_K: Traspaso de Cartera de Intermediarios - 16/01/2002
                    '				.setStorProcParam  1, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble)
                    '				.setStorProcParam  3, Session("sTypePolicy")
                    '               .setStorProcParam 13, Session("sOptProcTyp")    			
                Case "AGL008"
                    .sCodispl = "AGL008"
                    .ReportFilename = "AGL008_i.rpt"
                    '.setStorProcParam(1, CStr(Session("nInsur_Area")) & CStr(Session("sTypeBusiness")) & CStr(Session("sTypePolicy")))
                    .setStorProcParam(1, Session("nInsur_Area"))
                    .setStorProcParam(2, CStr(Session("StypeBusiness")))
                    .setStorProcParam(3, CStr(Session("sTypePolicy")))
                    .setStorProcParam(4, mobjValues.StringToType(Session("nMunicipality"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(5, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(7, mobjValues.StringToType(Session("nInterBefore"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(8, mobjValues.StringToType(Session("nInterNew"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(9, Session("sOptProcess"))
                    .setStorProcParam(10, Session("nUsercode"))
                    .setStorProcParam(11, .setdate(Session("dDateProcess")))
                    '.setStorProcParam(10, CStr(Session("schkSaldo")) & CStr(Session("sOptProcTyp")))
                    .setStorProcParam(12, CStr(Session("schkSaldo")))
                    .setStorProcParam(13, sKey)
                    Response.Write((.Command))
                    
                    '+ AGL014_K: Listado de préstamos / anticipos - 18/01/2002			
                    '				.setStorProcParam 8, .setdate(Cstr(Request.Form("tcdEnddate")))			
                Case "AGL014"
                    .sCodispl = "AGL014"
                    .ReportFilename = "AGL014.rpt"
                    .setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(2, Request.Form.Item("tctClientCode"))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeInterType"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(7, Request.Form.Item("cbeStatloan"))
                    .setStorProcParam(8, .setdate(CStr(Request.Form.Item("tcdStardate"))) & .setdate(CStr(Request.Form.Item("tcdEnddate"))))
                    .setStorProcParam(9, mobjValues.StringToType(Request.Form.Item("tcnLoan"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(10, mobjValues.StringToType(Request.Form.Item("cboLoanType"), eFunctions.Values.eTypeData.etdDouble, True))
                    .setStorProcParam(11, mobjValues.StringToType(Request.Form.Item("cboPayForm"), eFunctions.Values.eTypeData.etdDouble, True))
                    Response.Write((.Command))
				
                    '+AGL620: Liquidación de comisiones
                Case "AGL620"
                    .sCodispl = "AGL620"
                    If Request.Form.Item("optTyp") = "1" Then
                        .ReportFilename = "AGL620BTC.rpt"
                        .setStorProcParam(1, Session("nPay_Comm"))
                        .setStorProcParam(2, Request.Form.Item("optProcess"))
                        .setStorProcParam(3, .setdate(Request.Form.Item("tcdEffecdate")))
                        .setStorProcParam(4, .setdate(Request.Form.Item("tcdEffecdateEnd")))
					
                    Else
                        .ReportFilename = "AGL620_1.rpt"
                        '.setParamField(1, "dStartDate", Request.Form.Item("tcdEffecdate"))
                        '.setParamField(2, "dEndDate", Request.Form.Item("tcdEffecdateEnd"))
                        .setStorProcParam(1, Session("nPay_Comm"))
                        .setStorProcParam(2, Request.Form.Item("optProcess"))
                    End If
                    Response.Write((.Command))
				
                    '+AGL703: Libro Timbrado de Comisiones de Pagadas
                Case "AGL703"
                    .sCodispl = "AGL703"
                    .ReportFilename = "agl703.rpt"
				
                    .setStorProcParam(1, Request.Form.Item("hdddDateIni"))
                    .setStorProcParam(2, Request.Form.Item("hdddDateEnd"))
                    .setStorProcParam(3, sKey)
				
                    Response.Write((.Command))
				
                    '+AGL728: Carga de comisiones por póliza
                Case "AGL728"
                    .sCodispl = "AGL728"
                    .ReportFilename = "AGL728.rpt"
                    .setStorProcParam(1, Request.Form.Item("optProcess"))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdProcess_date")))
				
                    Response.Write((.Command))
				
                    '+AGL728: Carga de comisiones por póliza
                Case "AGL605"
                    .sCodispl = "AGL605"
                    .ReportFilename = "AGL605.rpt"
                    .setParamField(1, "dEndDate", Request.Form.Item("tcdEffecdate"))
                    .setParamField(2, "sOptProcess", Request.Form.Item("optProcess"))
                    .setParamField(3, "nIntertyp", mobjValues.StringToType(Request.Form.Item("valInterm_typ"), eFunctions.Values.eTypeData.etdLong))
				
                    Response.Write((.Command))
				
                    '+ AGL009: Solicitud de pago de comisiones
                Case "AGL009"
                    .sCodispl = "AGL009"
                    .ReportFilename = "AGL009.rpt"
                    .setStorProcParam(1, sKey)
                    .setStorProcParam(2, Request.Form.Item("optProcess"))
                    Response.Write((.Command))
				
                    '+ AGL7000: Comisiones del prodcuto APV.
                    '+[APV2] 1014_BB. Calculo de comisiones de APV
                Case "AGL7000"
                    .sCodispl = "AGL7000"
                    .ReportFilename = "AGL7000.rpt"
                    .setStorProcParam(1, mstrMonth)
                    .setStorProcParam(2, mstrYear)
                    '.setStorProcParam 3, mobjValues.StringToType(Request.Form("cbeBranch"),eFunctions.Values.eTypeData.etdDouble)
                    '.setStorProcParam 4, mobjValues.StringToType(Request.Form("valProduct"),eFunctions.Values.eTypeData.etdDouble)                                                
                    Response.Write((.Command))
				
                Case "AGL918"
                    .sCodispl = "AGL918"
                    If Request.Form.Item("optReport") = "1" Then
                        .ReportFilename = "rpt_inter_detagemres.rpt"
                    Else
                        .ReportFilename = "rpt_inter_detagemresb.rpt"
                    End If
                    .setStorProcParam(1, sKey)
                    Response.Write((.Command))
                    '+ AGL920: Producción y remuneración de intermediarios de seguros
                Case "AGL920"
                    .sCodispl = "AGL920"
                    .ReportFilename = "AGL920_1.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdInit_date")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd_date")))
                    Response.Write((.Command))
                    .Reset()
				
                    .sCodispl = "AGL920"
                    .ReportFilename = "AGL920_2.rpt"
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdInit_date")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd_date")))
                    Response.Write((.Command))
				
                    '+ AGL921: Certificados de intermediarios
                Case "AGL921"
                    .sCodispl = "AGL921"
                    If Request.Form.Item("valInterm_typ") <> vbNullString Or Request.Form.Item("chkGen_certif") = "1" Or Request.Form.Item("dtcClient") <> vbNullString Then
                        .ReportFilename = "AGL921_1.rpt"
                        .setStorProcParam(1, .setdate(Request.Form.Item("tcdInit_date")))
                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd_date")))
                        .setStorProcParam(3, Request.Form.Item("dtcClient"))
                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("valInterm_typ"), eFunctions.Values.eTypeData.etdLong))
                        .setStorProcParam(5, Request.Form.Item("chkGen_certif"))
                        Response.Write((.Command))
                    End If
				
                    '+ Si se desea ver la nómina de los intermediarios que generan certificados
                    If Request.Form.Item("chkNom_certif") = "1" Then
                        .Reset()
                        .sCodispl = "AGL921"
                        .ReportFilename = "AGL921_2.rpt"
                        .setStorProcParam(1, .setdate(Request.Form.Item("tcdInit_date")))
                        .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnd_date")))
                        Response.Write((.Command))
                    End If
				
                    '+ AGL922: Generación de archivos FECU
                Case "AGL922"
                    .sCodispl = "AGL922"
                    .ReportFilename = "AGL922.rpt"
                    Response.Write((.Command))
				
            End Select
	End With
	
	mobjDocuments = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valagentrep")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valagentrep"
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>






<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 3 $|$$Date: 18/08/09 12:31p $|$$Author: Gletelier $"
</SCRIPT>
</HEAD>

<%If Request.QueryString.Item("nZone") = "1" Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
    //---------------------------------------------------------------------------------------
    function CancelErrors() {
        //---------------------------------------------------------------------------------------
        self.history.go(-1)
    }

    //---------------------------------------------------------------------------------------
    function NewLocation(Source, Codisp) {
        //---------------------------------------------------------------------------------------
        var lstrLocation = "";
        lstrLocation += Source.location;
        lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp;
        Source.location = lstrLocation
    }
</SCRIPT>
<%
mstrCommand = "&sModule=Agent&sProject=AgentRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValAgentRep
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If
If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""AgentRepError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostAgentRep Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.QueryString.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();top.opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				Select Case Request.QueryString.Item("sCodispl")
					Case "AGL605"
						If Request.QueryString.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.close();opener.top.document.location.reload();</SCRIPT>")
						End If
					Case "AGL008"
						If Request.QueryString.Item("nZone") = "1" Then
							If CStr(Session("sOptProcess")) = "1" Then
								Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
							Else
								Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							End If
						Else
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
						End If
					Case "AGL703"
						If Request.QueryString.Item("nZone") = "2" Then
							Response.Write("<SCRIPT>alert('VARIABLE:->" & "AQUI PONER VALOR" & "');</SCRIPT>")
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							End If
						End If
					Case "AGL955"
						If Request.QueryString.Item("nZone") = "2" Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
							End If
						End If
					Case "AGL786"
						If Request.QueryString.Item("nZone") = "2" Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							If Request.Form.Item("sCodisplReload") = vbNullString Then
								Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
							Else
								Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
							End If
						End If
					Case Else
						If Request.QueryString.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();top.opener.top.document.location.reload();</SCRIPT>")
						End If
				End Select
			End If
		End If
	Else
		Select Case Request.QueryString.Item("sCodispl")
			Case "AGL786"
				Response.Write("<SCRIPT>alert('No se encontraron datos. No se Genero Archivo');</SCRIPT>")
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
		End Select
	End If
End If

mobjValues = Nothing
mobjValAgentRep = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("valagentrep")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





