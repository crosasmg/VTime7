<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

Dim mstrErrors As String

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mobjValues As eFunctions.Values
Dim mobjValCashBankRep As Object
Dim sKey As Object

    Dim mblnTimeOut As Boolean

'% insValCashBankRep: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValCashBankRep() As String
	'--------------------------------------------------------------------------------------------
	Dim lclsCash As Object
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ OPL001: Informe de Cheques
		Case "OPL001"
			With Request
				lclsCash = New eCashBank.Cheque
				insValCashBankRep = lclsCash.insValOPL001_K(.QueryString("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ OPL002: Listado de depósito
		Case "OPL002"
			With Request
				lclsCash = New eCashBank.Cheque
				insValCashBankRep = lclsCash.insValOPL002_K(.QueryString("sCodispl"), Request.Form.Item("tctDepositNum"), mobjValues.StringToType(Request.Form.Item("valAccCash"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ OPL003: Actualización de saldos bancarios
		Case "OPL003"
			lclsCash = New eCashBank.ValCashBankRep
			insValCashBankRep = lclsCash.insValOPL003_K("OPL003")
			
			
			'+ OPL004: Cheque/Voucher
		Case "OPL004"
			With Request
				lclsCash = New eCashBank.ValCashBankRep
				insValCashBankRep = lclsCash.insValOPL004_K(.QueryString("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcnTypeList"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRequest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ OPL020: Relación de Ordenes de Pago
		Case "OPL020"
			With Request
				lclsCash = New eCashBank.Cheque
				insValCashBankRep = lclsCash.insValOPL020_K(.QueryString("sCodispl"), mobjValues.StringToDate(Request.Form.Item("tcdInitDate")), mobjValues.StringToDate(Request.Form.Item("tcdEndDate")), mobjValues.StringToType(Request.Form.Item("valAccountNum"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ OPL719: Cierres y aperturas de cajas
		Case "OPL719"
			mobjValCashBankRep = New eCashBank.Cash_stat
			With Request
				
				insValCashBankRep = mobjValCashBankRep.insValOPL719_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdProcDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCashnum"), eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"), mobjValues.StringToType(.Form.Item("chkPrint"), eFunctions.Values.eTypeData.etdDouble))
				
				Session("nCash_opertyp") = mobjValues.StringToType(.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True)
				Session("nCashnum") = .Form.Item("tcnCashnum")
				Session("dProcDat") = mobjValues.TypeToString(.Form.Item("tcdProcDat"), eFunctions.Values.eTypeData.etdDate)
			End With

            Case "OPL729"
                mobjValCashBankRep = New eCashBank.Cash_stat
                With Request
				
                    insValCashBankRep = mobjValCashBankRep.insValOPL719_K(.QueryString("sCodispl"), mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdProcDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), Session("nUserCode"), mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble))
				
                    Session("nCash_opertyp") = mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble, True)
                    Session("nCashnum") = 0
                    Session("dProcDat") = mobjValues.TypeToString(.Form.Item("tcdProcDat"), eFunctions.Values.eTypeData.etdDate)
                End With

		Case Else
			insValCashBankRep = "insValCashBankRep: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	lclsCash = Nothing
End Function

'% insPostCashBankRep: Se efectua el proceso
'--------------------------------------------------------------------------------------------
Private Function insPostCashBankRep() As Boolean
	'--------------------------------------------------------------------------------------------
	'-Objeto para transacciones batch	
	Dim lclsBatch_param As eSchedule.Batch_param
	Dim lblnPost As Boolean
        Dim lobjGeneral As eGeneral.GeneralFunction
        Dim eIniPost As Object
        Dim eEndPost As Integer
        Dim nRequest_nu As Double
        Dim nOffice As Double
        Dim nBank As Double
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ OPL001: Informe de Cheques
		Case "OPL001"
			lblnPost = True
			insPrintDocuments()
			
			'+ OPL001: Listado de depósito
		Case "OPL002"
			lblnPost = True
			insPrintDocuments()
			
			'+ OPL003: Actualización de saldos bancarios
		Case "OPL003"
                mobjValCashBankRep = New eCashBank.ValCashBankRep
                lblnPost = mobjValCashBankRep.insPostOPL003_K(Session("nUsercode"))

                If lblnPost Then
                    Response.Write("<SCRIPT>alert('El proceso se ejecutó satisfactoriamente ');</" & "Script>")
                End If
			
			'+ OPL004: Cheque/Voucher		
		Case "OPL004"
                mobjValCashBankRep = New eCashBank.ValCashBankRep

                If mobjValues.StringToType(Request.Form.Item("tcnRequest"), eFunctions.Values.eTypeData.etdDouble, True) < 0 Then
                    nRequest_nu = 0
                Else
                    nRequest_nu = mobjValues.StringToType(Request.Form.Item("tcnRequest"), eFunctions.Values.eTypeData.etdDouble)
                End If
			
                If mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True) < 0 Then
                    nOffice = 0
                Else
                    nOffice = mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble)
                End If
			
                If mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble, True) < 0 Then
                    nBank = 0
                Else
                    nBank = mobjValues.StringToType(Request.Form.Item("cbeBank"), eFunctions.Values.eTypeData.etdDouble)
                End If
		    
                lobjGeneral = New eGeneral.GeneralFunction
		
                Session("sKey") = lobjGeneral.getsKey(Session("nUsercode"))
		
                lblnPost = mobjValCashBankRep.insPostOPL004(mobjValues.StringToType(Request.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), nRequest_nu, nOffice, nBank, Session("nUsercode"), Session("sKey"))

                lobjGeneral = Nothing
                insPrintDocuments()
			
			'+ OPL020: Relación de Ordenes de Pago		    
		Case "OPL020"
			lblnPost = True
			insPrintDocuments()
			
			'+ OPL719: Cierres y aperturas de cajas	
		Case "OPL719"
			mobjValCashBankRep = New eCashBank.Cash_stat
            lobjGeneral = New eGeneral.GeneralFunction    
			lblnPost = mobjValCashBankRep.insPostOPL719_K(mobjValues.StringToType(Session("nCash_opertyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dProcDat"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nCashnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			Dim lclsCash As ValCashBankRep = New eCashBank.ValCashBankRep
                
			If CStr(Session("BatchEnabled")) <> "1" Or mobjValues.StringToType(Request.Form.Item("chkPrint"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
                If mobjValues.StringToType(Request.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True) <> 4 And _
                    mobjValues.StringToType(Request.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True) <> 9 Then
                        Session("sKey") = lobjGeneral.getsKey(Session("nUsercode"))
                        If lclsCash.insBTC00118( Session("sKey"),mobjValues.StringToType(Session("nCashnum"), eFunctions.Values.eTypeData.etdDouble),mobjValues.StringToType(Session("dProcDat"), eFunctions.Values.eTypeData.etdDate),Session("nUsercode"))  then
				            Call insPrintDocuments()
                        End if                            
                End If 
			Else
                If mobjValues.StringToType(Request.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True) <> 4 And _
                    mobjValues.StringToType(Request.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True) <> 9 Then
                    
				    lclsBatch_param = New eSchedule.Batch_param
				    With lclsBatch_param
					    .nBatch = 118
					    .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCashnum"), eFunctions.Values.eTypeData.etdDouble))
					    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("dProcDat"), eFunctions.Values.eTypeData.etdDate))
					    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("cbeCash_opertyp"), eFunctions.Values.eTypeData.etdDouble, True))
                       '.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, mobjValues.StringToType(Request.Form.Item("chkPrint"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, 1)
					    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					    .Save()
				    End With
                        Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & " \n\n Dirigirse a Procesos Batch para visualizar los Reportes');</" & "Script>")
	
				
				    lclsBatch_param = Nothing
				End If 
			End If
            Case "OPL729"
                mobjValCashBankRep = New eCashBank.Cash_stat
                lobjGeneral = New eGeneral.GeneralFunction
                Dim lclsCash As ValCashBankRep = New eCashBank.ValCashBankRep
                mobjValCashBankRep = New eCashBank.Cash_stat
                lobjGeneral = New eGeneral.GeneralFunction
                Session("sKey") = lobjGeneral.getsKey(Session("nUsercode"))
                lblnPost = true
                If lclsCash.insBTC00118(Session("sKey"), mobjValues.StringToType(0, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dProcDat"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode")) Then
                    Call insPrintDocuments()
                End If
			    
        End Select
	
	insPostCashBankRep = lblnPost
	
End Function

'**% insPrintDocuments: Document printing
'%   insPrintDocuments: Impresión de los documentos
'-----------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------------
	Dim mobjDocuments As eReports.Report
	Dim sDescript As String
	Dim nStatuscheck As Integer
	Dim nNumbank As Integer
	Dim dEndDate As Date
	Dim nConcept As Integer
	Dim nRequest_nu As Double
	Dim nOffice As Integer
	Dim nBank As Integer
	Dim nCurrency As Integer
	Dim nAcc_bank As Integer
	Dim nFlag As Double
	
	mobjDocuments = New eReports.Report
	Dim lclsCash As eCashBank.Bank_mov
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			
			'+ OPL001: Informe de Cheques
			Case "OPL001"

				sDescript = mobjValues.getMessage(CShort(Request.Form.Item("cbeStatusCheck")), "Table187")
				If mobjValues.StringToType(Request.Form.Item("cbeStatusCheck"), eFunctions.Values.eTypeData.etdDouble, True) < 0 Then
					nStatuscheck = 0
				Else
					nStatuscheck = mobjValues.StringToType(Request.Form.Item("cbeStatusCheck"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				If mobjValues.StringToType(Request.Form.Item("valOriAccount"), eFunctions.Values.eTypeData.etdDouble, True) < 0 Then
					nNumbank = 0
				Else
					nNumbank = mobjValues.StringToType(Request.Form.Item("valOriAccount"), eFunctions.Values.eTypeData.etdDouble)
				End If
				
				With mobjDocuments
					.sCodispl = "OPL001"
					.ReportFilename = "OPL001.rpt"
					.setStorProcParam(1, Request.Form.Item("tcdInitdate"))
					.setStorProcParam(2, Request.Form.Item("tcdEnddate"))
					.setStorProcParam(3, sDescript)
					.setStorProcParam(4, nStatuscheck)
					.setStorProcParam(5, nStatuscheck)
					.setStorProcParam(6, .setdate(Request.Form.Item("tcdInitdate")))
					.setStorProcParam(7, .setdate(Request.Form.Item("tcdEnddate")))
					.setStorProcParam(8, nNumbank)
					Response.Write((.Command))
				End With
				
				'+ OPL002: Listado de depósito
			Case "OPL002"
				lclsCash = New eCashBank.Bank_mov
				If lclsCash.Find_sDep_number(Request.Form.Item("tctDepositNum"), 6) Then
					With mobjDocuments
						.sCodispl = "OPL002"
						.ReportFilename = "OPL002.rpt"
						.setStorProcParam(1, Request.Form.Item("tctDepositNum"))
						.setStorProcParam(2, Request.Form.Item("valAccCash"))
						Response.Write((.Command))
					End With
				Else
					With mobjDocuments
						.sCodispl = "OPL002"
						.ReportFilename = "OPL002_a.rpt"
						.setStorProcParam(1, Request.Form.Item("tctDepositNum"))
						.setStorProcParam(2, Request.Form.Item("valAccCash"))
						Response.Write((.Command))
					End With
				End If
				mobjDocuments = Nothing
				
				'+ OPL004: Cheque/Voucher
			Case "OPL004"
				With mobjDocuments
                        .sCodispl = "OPL004"
                        .ReportFilename = "OPL004.rpt"
                        .setStorProcParam(1, Session("sKey"))
                        Response.Write((.Command))
                    End With
				'+ OPL020: Relación de Ordenes de Pago		    
			Case "OPL020"
				nFlag = 0
				
				If mobjValues.StringToType(Request.Form.Item("chkOffice"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
					nFlag = nFlag + 1
				End If
				
				If mobjValues.StringToType(Request.Form.Item("chkAccount"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
					nFlag = nFlag + 1
				End If
				
				With mobjDocuments
					
					.sCodispl = "OPL020"
					.ReportFilename = "OPL020_2.rpt"
					
					.setStorProcParam(1, Request.Form.Item("tcdInitdate"))
					.setStorProcParam(2, Request.Form.Item("tcdEndDate"))
					.setStorProcParam(3, Request.Form.Item("cbeConcept"))
					.setStorProcParam(4, Request.Form.Item("cbeOffice"))
					.setStorProcParam(5, Request.Form.Item("cbeCurrency"))
					.setStorProcParam(6, Request.Form.Item("valAccountNum"))
					.setStorProcParam(7, "1")
					.setStorProcParam(8, Request.Form.Item("cbeSta_cheque"))
					
					Response.Write((.Command))
				End With
				'+ OPL719: Cierres y aperturas de cajas
			Case "OPL719"
				If CDbl(Request.Form.Item("cbeCash_opertyp")) <> 4 And CDbl(Request.Form.Item("cbeCash_opertyp")) <> 9 Then
					
					If mobjValues.StringToType(Request.Form.Item("chkPrint"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
						.sCodispl = "OPL719"
						.ReportFilename = "OPL719btc.rpt"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						.Reset()
							
						'+Ingreso de recaudación de primas registradas.
						.ReportFilename = "OPL719_det1btc.rpt"
						.sCodispl = "OPL719"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						.Reset()
							
						'+Detalle de pagos ingresados.       	            
						.ReportFilename = "OPL719_det2btc.rpt"
						.sCodispl = "OPL719"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						.Reset()
							
						'+Detalle de depósitos ingresados.    
						.ReportFilename = "OPL719_det3btc.rpt"
						.sCodispl = "OPL719"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						.Reset()
							
						'+Detalle de ingresos no operacionales.    				
						.ReportFilename = "OPL719_det4btc.rpt"
						.sCodispl = "OPL719"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						.Reset()
							
						'+Detalle de diferencias.    				
						.ReportFilename = "OPL719_det5btc.rpt"
						.sCodispl = "OPL719"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						.Reset()
							
						'+Detalle de ingresos operacionales.    				
						.ReportFilename = "OPL719_det6btc.rpt"
						.sCodispl = "OPL719"
						.setStorProcParam(1, Session("sKey"))
						Response.Write((.Command))
						
						mblnTimeOut = True
					End If
				End If
                Case "OPL729"
                    .sCodispl = "OPL729"
                    .ReportFilename = "OPL719btc.rpt"
                    .setStorProcParam(1, Session("sKey"))
                    Response.Write((.Command))
                    .Reset()

            End Select
	End With
	mobjDocuments = Nothing
	Server.ScriptTimeOut = 90
End Sub

</script>
<%
Response.Expires = -1
mblnTimeOut = False


mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>






</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 18/12/03 17:56 $|$$Author: Nvaplat7 $"

//---------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------
	self.history.go(-1)	
}

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

mstrCommand = "&sModule=CashBank&sProject=CashBankRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValCashBankRep
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""CashBankRepError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostCashBankRep Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.QueryString.Item("sCodispl") = "OPL719" Then
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
				Select Case Request.QueryString.Item("sCodispl")
					Case "OPL719"
						If Request.QueryString.Item("sCodisplReload") = vbNullString Then
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
					Case "OPL003"
						Response.Write("<SCRIPT>top.close();opener.top.document.location.reload();</SCRIPT>")
                        Case "OPL0729"
                            Response.Write("<SCRIPT>top.close();opener.top.document.location.reload();</SCRIPT>")

                        Case Else
                            Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
                    End Select
			End If
		End If
	End If
End If

mobjValues = Nothing
mobjValCashBankRep = Nothing
%>
</BODY>
</HTML>




