<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

'-   Los objetos de la página son definidos

Dim mobjValues As eFunctions.Values
Dim mobjLedge As eLedge.t_Ledger_File
Dim mobjLedgerAcc As eLedge.LedgerAcc
Dim mobjLedgerAutDetail As eLedge.LedgerAutDetail
Dim mstrCommand As String
Dim mobjAcc_lines As Object
Dim mobjAcc_transa As eLedge.Acc_transa
Dim mobjBal_histor As eLedge.Bal_histor
Dim mblnTimeOut As Boolean

Private mstrErrors As String


'%   insValLedgerRep: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValLedgerRep() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		'+   Interfaz contable (Archivos txt)
		
		Case "CPL637"
			insValLedgerRep = mobjLedge.insValCPL637("CPL637", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeFile"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble, True))
			
			'+   Plan de cuentas.				
		Case "CPL001"
			mobjLedgerAcc = New eLedge.LedgerAcc
			
			With Request
				insValLedgerRep = mobjLedgerAcc.insValCPL001_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeLevels"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+   Cierre contable.
		Case "CPL002"
			mobjAcc_transa = New eLedge.Acc_transa
			
			With Request
				insValLedgerRep = mobjAcc_transa.insValCPL002_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdClosedate")))
			End With
			
			'+   Balance de comprobación.
		Case "CPL003"
			mobjBal_histor = New eLedge.Bal_histor
			
			With Request
				insValLedgerRep = mobjBal_histor.insValCPL003_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeLevels"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYearE"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonthE"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+   Diario Mayor.				
		Case "CPL004"
			mobjAcc_lines = New eLedge.Acc_lines
			
			With Request
				insValLedgerRep = mobjAcc_lines.insValCPL004_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("optProcess"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(.Form.Item("tcdInitdate")), mobjValues.StringToDate(.Form.Item("tcdEnddate")), .Form.Item("tctAccount"), .Form.Item("tctAux_accoun"), mobjValues.TypeToString(.Form.Item("cbeCost_cente"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+   Transferencia de pagos de honorarios.
			
		Case "CPL778", "CPL779"
			mobjAcc_lines = New eLedge.Fin700_Lines
			With Request
				insValLedgerRep = mobjAcc_lines.insValCPL778_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToDate(.Form.Item("tcdProc_date")))
			End With
			mobjAcc_lines = Nothing
			
			'+   Asientos automáticos de "Primas".
			'+   Asientos automáticos de "Siniestros".
			'+   Asientos automáticos de "Caja ingreso".
			'+   Asientos automáticos de "Caja egreso".
			'+   Asientos automáticos de "Cuentas corrientes".
			
			'+ Asientos automáticos de "Cuentas corrientes" APV.
			
		Case "CPL999"
			insValLedgerRep = mobjLedgerAutDetail.insValCPL999_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeArea_Led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdTo_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate))
			
		Case Else
			insValLedgerRep = "insValLedgerRep: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'%   insPostLedgerRep: Se efectua el proceso
'--------------------------------------------------------------------------------------------
Private Function insPostLedgerRep() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lclsBatch_param As eSchedule.Batch_param
	
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+   Interfaz contable (Archivos txt)
		
		Case "CPL637"
			lblnPost = mobjLedge.insPostCPL637(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeFile"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("optExecute"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
			
			If lblnPost Then
				Call insPrintDocuments()
			End If
			
			'+   Plan de cuentas.				
		Case "CPL001"
			insPostLedgerRep = True
			insPrintDocuments()
			
			'+   Cierre contable.				
		Case "CPL002"
			insPostLedgerRep = True
			insPrintDocuments()
			
			'+   Cierre contable.				
		Case "CPL003"
			insPostLedgerRep = True
			insPrintDocuments()
			
			'+   Diario Mayor.				
		Case "CPL004"
			lblnPost = True
			insPrintDocuments()
			
			'+ Asientos automáticos de "Primas".
			'+ Asientos automáticos de "Siniestros".
			'+ Asientos automáticos de "Cuentas corrientes".
			
			'+ Asientos automáticos de "Cuentas corrientes" APV.
			
		Case "CPL999"
			If CStr(Session("BatchEnabled")) <> "1" Then
				lblnPost = mobjLedgerAutDetail.insPostCPL999_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbeArea_Led"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdTo_date"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("optExecute"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True))
				
				If lblnPost And Request.Form.Item("chkPrint") = "1" Then
					Call insPrintDocuments()
				End If
			Else
				lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        
                        Select Case mobjValues.StringToType(Request.Form.Item("cbeArea_Led"), eFunctions.Values.eTypeData.etdDouble)
                            Case 1
                                .nBatch = 1
                            Case 2
                                .nBatch = 2
                            Case 3
                                .nBatch = 3
                            Case 4
                                .nBatch = 444
                            Case 5
                                .nBatch = 5
                            Case 6
                                .nBatch = 6
                            Case 40
                                .nBatch = 40
                        End Select
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdTo_date"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeArea_Led"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optExecute"))
                        '4 : No tiene reporte asociado
                        If .nBatch <> 444 Then
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("tcdTo_date"), eFunctions.Values.eTypeData.etdDate))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("cbeArea_Led"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                        .Save()
                    End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				lblnPost = True
			End If
			
			'+  Proceso de transferencia de solicitudes de cheques.
		Case "CPL778"
			mobjAcc_lines = New eLedge.Fin700_Lines
			lblnPost = mobjAcc_lines.insPostCPL778_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdProc_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble))
			mobjAcc_lines = Nothing
			
			'+  Proceso de transferencia de pago de honorarios.
		Case "CPL779"
			mobjAcc_lines = New eLedge.Fin700_Lines
			lblnPost = mobjAcc_lines.insPostCPL779_K(mobjValues.StringToType(Request.Form.Item("tcdProc_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble))
			mobjAcc_lines = Nothing
	End Select
	
	insPostLedgerRep = lblnPost
	
End Function

'%   insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	Dim lstrDate As String
	'-----------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	Dim mobjDocuments1 As eReports.Report
	
	mobjDocuments = New eReports.Report
	mobjDocuments1 = New eReports.Report
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "CPL637"
			With mobjDocuments
				.sCodispl = "CPL637"
				.ReportFilename = "CPL637.RPT"
				
				.setStorProcParam(1, (mobjLedge.sKey))
				
				Response.Write((.Command))
			End With
			
			'+   Plan de cuentas.				
		Case "CPL001"
			With mobjDocuments
				.sCodispl = "CPL001"
				
				If mobjValues.StringToType(Request.Form.Item("chkDetail"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
					.ReportFilename = "CPL001D.RPT"
				Else
					.ReportFilename = "CPL001.RPT"
				End If
				
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeLevels"), eFunctions.Values.eTypeData.etdDouble))
				
				Response.Write((.Command))
			End With
			
			'+   Cierre contable.				
			
		Case "CPL002"
			With mobjDocuments
				.sCodispl = "CPL002"
				
				.ReportFilename = "CPL002.RPT"
				
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdClosedate"), eFunctions.Values.eTypeData.etdDate))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("optClose"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, Session("nUsercode"))
				
				Response.Write((.Command))
			End With
			
			'+   Balance de comprobación.
			
		Case "CPL003"
			With mobjDocuments
				.sCodispl = "CPL003"
				
				.ReportFilename = "CPL003.RPT"
				
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("cbeLevels"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnYearE"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("cbeMonthE"), eFunctions.Values.eTypeData.etdDouble))
				
				Response.Write((.Command))
			End With
			
			'+   Diario Mayor.				
		Case "CPL004"
			With mobjDocuments
				.sCodispl = "CPL004"
				
				Select Case Request.Form.Item("cbeReportType")
					Case "1"
						.ReportFilename = "CPL004_Day.RPT"
						.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(2, .setdate(Request.Form.Item("tcdInitdate")))
						.setStorProcParam(3, .setdate(Request.Form.Item("tcdEnddate")))
						.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnInitVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnEndVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(6, Request.Form.Item("chkSum"))
						Response.Write((.Command))
					Case "2"
						.ReportFilename = "CPL004_May.RPT"
						.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(4, Request.Form.Item("tctAccount"))
						.setStorProcParam(5, Request.Form.Item("tctAux_accoun"))
						.setStorProcParam(6, Request.Form.Item("cbeCost_cente"))
						Response.Write((.Command))
					Case "3"
						.ReportFilename = "CPL004_DayResume.RPT"
						.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate))
						.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
						.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnInitVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnEndVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(6, Request.Form.Item("chkSum"))
						Response.Write((.Command))
					Case "4"
						.ReportFilename = "CPL004_Day.RPT"
						.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate))
						.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
						.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnInitVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnEndVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(6, Request.Form.Item("chkSum"))
						Response.Write((.Command))
						
						.ReportFilename = "CPL004_May.RPT"
						.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeMonth"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(4, Request.Form.Item("tctAccount"))
						.setStorProcParam(5, Request.Form.Item("tctAux_accoun"))
						.setStorProcParam(6, Request.Form.Item("cbeCost_cente"))
						Response.Write((.Command))
						
						.ReportFilename = "CPL004_DayResume.RPT"
						.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("cbeLedCompan"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate))
						.setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
						.setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("tcnInitVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(5, mobjValues.StringToType(Request.Form.Item("tcnEndVoucher"), eFunctions.Values.eTypeData.etdDouble))
						.setStorProcParam(6, Request.Form.Item("chkSum"))
						Response.Write((.Command))
				End Select
				
			End With
			
			
			
			'+ Asientos contables automáticos.
			
		Case "CPL999"
			lstrDate = Mid(mobjValues.TypeToString(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), 7, 4) & Mid(mobjValues.TypeToString(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), 4, 2) & Mid(mobjValues.TypeToString(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), 1, 2)
			Select Case mobjValues.StringToType(Request.Form.Item("cbeArea_Led"), eFunctions.Values.eTypeData.etdDouble)
				
				'+ Asientos automáticos de "Primas".
				Case 1
					With mobjDocuments
						'+ Asientos Contabilizados
						.sCodispl = "CPL999"
						.ReportFilename = "Posting.rpt"
						.setStorProcParam(1, "1")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Detalle asientos Contabilizados
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "Posting2.rpt"
						.setStorProcParam(1, "1")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Asientos NO Contabilizados        
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "ErrorsPostingPremium.rpt"
						.setStorProcParam(1, lstrDate)
						.setStorProcParam(2, Session("nUsercode"))
						.bTimeOut = True
						.nTimeOut = 30000
						Response.Write((.Command))
						mblnTimeOut = .bTimeOut
					End With
					
					'+ Asientos automáticos de "Siniestros".
				Case 2
					With mobjDocuments
						'+ Asientos Contabilizados
						.sCodispl = "CPL999"
						.ReportFilename = "Posting.rpt"
						.setStorProcParam(1, "2")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Detalle asientos Contabilizados
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "Posting2.rpt"
						.setStorProcParam(1, "2")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Asientos NO Contabilizados                                
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "ErrorsPostingClaim.rpt"
						.setStorProcParam(1, lstrDate)
						.setStorProcParam(2, Session("nUsercode"))
						.bTimeOut = True
						.nTimeOut = 25000
						Response.Write((.Command))
						mblnTimeOut = .bTimeOut
					End With
					
					'+ Asientos automáticos de "Cuentas corrientes".
				Case 3
					With mobjDocuments
						'+ Asientos Contabilizados
						.sCodispl = "CPL999"
						.ReportFilename = "Posting.rpt"
						.setStorProcParam(1, "3")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Detalle asientos Contabilizados
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "Posting2.rpt"
						.setStorProcParam(1, "3")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Asientos NO Contabilizados                        
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "ErrorsPostingCurr_acc.rpt"
						.setStorProcParam(1, lstrDate)
						.setStorProcParam(2, Session("nUsercode"))
						.bTimeOut = True
						.nTimeOut = 25000
						Response.Write((.Command))
						mblnTimeOut = .bTimeOut
					End With
					
					
					'+ Asientos automáticos de "Caja-Ingresos".
				Case 5
					
					With mobjDocuments
						'+ Asientos Contabilizados
						.sCodispl = "CPL999"
						.ReportFilename = "Posting.rpt"
						.setStorProcParam(1, "5")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Detalle asientos Contabilizados
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "Posting2.rpt"
						.setStorProcParam(1, "5")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Asientos NO Contabilizados        
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "ErrorsPostingInCash.rpt"
						.setStorProcParam(1, lstrDate)
						.setStorProcParam(2, Session("nUsercode"))
						.bTimeOut = True
						.nTimeOut = 25000
						Response.Write((.Command))
						mblnTimeOut = .bTimeOut
					End With
					
					'+ Asientos automáticos de "Caja-Egresos".
				Case 6
					With mobjDocuments
						'+ Asientos Contabilizados
						.sCodispl = "CPL999"
						.ReportFilename = "Posting.rpt"
						.setStorProcParam(1, "6")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Detalle asientos Contabilizados
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "Posting2.rpt"
						.setStorProcParam(1, "6")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Asientos NO Contabilizados                                
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "ErrorsPostingExpCash.rpt"
						.setStorProcParam(1, lstrDate)
						.setStorProcParam(2, Session("nUsercode"))
						.bTimeOut = True
						.nTimeOut = 25000
						Response.Write((.Command))
						mblnTimeOut = .bTimeOut
					End With
					
					
					'**+ Automatic current account entries - APV. 
					'+ Asientos automáticos de "Cuentas corrientes" - APV.
				Case 40
					With mobjDocuments
						
						'+ Asientos Contabilizados
						
						.sCodispl = "CPL999"
						.ReportFilename = "Posting.rpt"
						.setStorProcParam(1, "40")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
						
						'+ Detalle asientos Contabilizados
						.Reset()
						.sCodispl = "CPL999"
						.ReportFilename = "Posting2.rpt"
						.setStorProcParam(1, "40")
						.setStorProcParam(2, lstrDate)
						.bTimeOut = True
						.nTimeOut = 20000
						Response.Write((.Command))
					End With
					
					
					mobjDocuments1 = New eReports.Report
					
					With mobjDocuments1
						
						'+ Asientos NO Contabilizados                        
						
						.sCodispl = "CPL999"
						.ReportFilename = "ErrorsPostingCurr_accAPV.rpt"
						.setStorProcParam(1, lstrDate)
						.setStorProcParam(2, Session("nUsercode"))
						.bTimeOut = True
						.nTimeOut = 25000
						Response.Write((.Command))
						mblnTimeOut = .bTimeOut
					End With
					
					mobjDocuments1 = Nothing
					
			End Select
	End Select
	
	mobjDocuments = Nothing
End Sub

</script>
<%

Response.Expires = -1

mblnTimeOut = False

mobjValues = New eFunctions.Values
mobjLedge = New eLedge.t_Ledger_File
mobjLedgerAutDetail = New eLedge.LedgerAutDetail


mstrCommand = "&sModule=GeneralLedGer&sProject=LedGerRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")

%>
<HTML>
	<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
		<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
		<%=mobjValues.StyleSheet()%>






	</HEAD>
	<BODY>

<SCRIPT>
//%   CancelErrors: Función que se ejecuta cuando se oprime el botón de cancelar
//---------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------
	self.history.go(-1)
}

//%   NewLocation: Función que permte establecer el URL de la página a cargar
//---------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//---------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%

'+   Se define la contante para el manejo de errores en caso de advertencias

If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
	
	'+   Si no se han validado los campos de la página
	
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValLedgerRep
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
	
End If

'+   Se invoca al menejo de errores

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LedgerRepError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostLedgerRep() Then
		If Request.QueryString.Item("sCodispl") = "CPL999" Then
			If Request.Form.Item("sCodisplReload") = vbNullString Then
				If mblnTimeOut Then
					Response.Write(("<SCRIPT>setTimeout('insReloadTop(true, false);',30000);</SCRIPT>"))
				Else
					Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
				End If
			Else
				If mblnTimeOut Then
					Response.Write(("<SCRIPT>setTimeout('top.close();opener.top.document.location.reload();',30000);</SCRIPT>"))
				Else
					Response.Write("<SCRIPT>top.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			End If
		Else
			If Request.Form.Item("sCodisplReload") = vbNullString Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				Response.Write("<SCRIPT>opener.top.document.location.reload();</SCRIPT>")
			End If
		End If
	Else
		Response.Write("<SCRIPT>alert('No se ejecuto proceso');</SCRIPT>")
	End If
End If

mobjValues = Nothing
mobjLedge = Nothing
mobjLedgerAutDetail = Nothing
%>
	</BODY>
</HTML>





