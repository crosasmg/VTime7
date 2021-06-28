<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjValues As eFunctions.Values

Private mstrErrors As String

'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca.

Dim mstrCommand As String
Dim mobjBookRep As Object

'+ Se declara la variable donde se guarda el skey cuando los procesos de sean batch
Dim mstrKey As String


'% insValBook: Se realizan las validaciones masivas de la forma
'-------------------------------------------------------------------------------------------
Function insValBook() As String
	'-------------------------------------------------------------------------------------------
	Dim lclsCollectionRep As eCollection.CollectionRep
    Dim lclsValAgentRep As eAgent.ValAgentRep
    Dim lclsValCoReinsuranRep As eCoReinsuran.ValCoReinsuranRep
    Dim lclsBooks As eBatch.Books	

	insValBook = vbNullString
	
	Dim mclsPayComm As eAgent.pay_comm
	Dim mclsLedgerAutDetail3 As eLedge.LedgerAutDetail
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "COL504", "CAL503", "COL702", "SIL705"
			insValBook = vbNullString
			
		Case "AGL776"
			lclsValAgentRep = New eAgent.ValAgentRep
			
			insValBook = lclsValAgentRep.insValAGL776_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
			
			lclsValAgentRep = Nothing
			
		Case "COL889"
			lclsCollectionRep = New eCollection.CollectionRep
			
			insValBook = lclsCollectionRep.insValCOL889_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToDate(Request.Form.Item("tcdDateIni")), mobjValues.StringToDate(Request.Form.Item("tcdDateEnd")))
			
			lclsCollectionRep = Nothing
			
		Case "AGL815"
			mclsPayComm = New eAgent.pay_comm
			With Request
				insValBook = mclsPayComm.insValAGL815("AGL815", mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
			End With
			mclsPayComm = Nothing
			
		Case "SIL704"
			mclsLedgerAutDetail3 = New eLedge.LedgerAutDetail
			With Request
				insValBook = mclsLedgerAutDetail3.insValSIL704("SIL704", mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
			End With
			mclsLedgerAutDetail3 = Nothing
            
            Case "CRL706"
                lclsValCoReinsuranRep = New eCoReinsuran.ValCoReinsuranRep
			
                insValBook = lclsValCoReinsuranRep.insValCRL706_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
			
                lclsValCoReinsuranRep = Nothing

        Case "COL602"
                lclsBooks = New eBatch.Books
                insValBook = lclsBooks.insValCol602_k(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger))
                lclsBooks = Nothing                

        Case "SIL604"
            lclsBooks = New eBatch.Books
            insValBook = lclsBooks.insValSil604_k(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger))
            lclsBooks = Nothing

		Case Else
			insValBook = "insValBook: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostBook: Se realizan las actualizaciones de las ventanas
'-------------------------------------------------------------------------------------------
Function insPostBook() As Boolean
	Dim mclsLedgerAutDetail As eLedge.LedgerAutDetail
	'-------------------------------------------------------------------------------------------
	'-Objeto para transacciones batch	
	Dim lclsBatch_param As eSchedule.Batch_param
	Dim lclsGeneralFunction As eGeneral.GeneralFunction
	
	Dim mclsPayComm As eAgent.pay_comm
	Dim mclsLedgerAutDetail3 As eLedge.LedgerAutDetail
	Select Case Request.QueryString.Item("sCodispl")
		
		
		'+ CAL503: Libro timbrado de Producción
		Case "CAL503"
			
			If CStr(Session("BatchEnabled")) <> "1" Then
				'Set lclsGeneralFunction = Server.CreateObject("eGeneral.GeneralFunction")				    
				'mstrKey = lclsGeneralFunction.getsKey(Session("P_SKEY"))				     
				'Set lclsGeneralFunction = Nothing
				
				mobjBookRep = New eBatch.Books
				insPostBook = mobjBookRep.InsCreTMP_CAL503(mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
				
				'/* se saca parametro antes de mstrKey */	
				'mobjValues.StringToType(Session("nUsercode"),eFunctions.Values.eTypeData.etdDouble),                 
				Session("P_SKEY") = mobjBookRep.P_SKEY
				
				Response.Write("<SCRIPT>alert('skey : " & Session("P_SKEY") & "');</" & "Script>")
				If insPostBook Then
					insPrintDocuments()
				End If
				
			Else
				
				lclsBatch_param = New eSchedule.Batch_param
				
				With lclsBatch_param
					.nBatch = 132
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					
					.Save()
				End With
				
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				
				lclsBatch_param = Nothing
				
				insPostBook = True
				
			End If
			
			'+ COL504: Libro timbrado de recuadacion
		Case "COL504"
			If mobjValues.StringToType(Request.Form.Item("tcnOption"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
				
				If CStr(Session("BatchEnabled")) <> "1" Then
					
					lclsGeneralFunction = New eGeneral.GeneralFunction
					mstrKey = lclsGeneralFunction.getsKey(Session("nUsercode"))
					lclsGeneralFunction = Nothing
					
					mobjBookRep = New eLedge.LedgerAutDetail
                        insPostBook = mobjBookRep.InsCreTmp_Lrecaudacion(mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                        mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                        mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), _
                                                                                        mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), _
                                                                                        mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                        mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                        mstrKey)
					Session("P_SKEY") = mstrKey
					
					If insPostBook Then
						Response.Write("<SCRIPT> <b> 'Se generó la clave de proceso: " & Session("P_SKEY") & "');</" & "Script>")
						insPrintDocuments()
					End If
					
				Else
					
					lclsBatch_param = New eSchedule.Batch_param
					
					With lclsBatch_param
						.nBatch = 129
						.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                            .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger))
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "1")
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
						.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, "")
						.Save()
					End With
					
					Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
					
					lclsBatch_param = Nothing
					
					insPostBook = True
					
				End If
			Else
				insPostBook = True
				insPrintDocuments()
			End If
			'+ AGL776: Libro de Comisiones Devengadas
		Case "AGL776"
			If CStr(Session("BatchEnabled")) <> "1" Then
				mclsLedgerAutDetail = New eLedge.LedgerAutDetail
				insPostBook = mclsLedgerAutDetail.InsCreTmp_Agl776(mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
				Session("P_SKEY") = mclsLedgerAutDetail.P_SKEY
				mclsLedgerAutDetail = Nothing
				
				If insPostBook Then
					Response.Write("<SCRIPT> <b> 'Se generó la clave de proceso: " & Session("P_SKEY") & "');</" & "Script>")
                        insPrintDocuments()
				End If
				
			Else
				
				lclsBatch_param = New eSchedule.Batch_param
				
				With lclsBatch_param
					.nBatch = 134
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					
					.Save()
				End With
				
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				
				lclsBatch_param = Nothing
				
				insPostBook = True
				
			End If
			
			'+ COL889: Libro de Primas Devengadas			
		Case "COL889"
			insPostBook = True
			
			insPrintDocuments()
			
			'+ AGL815: Comisiones por Pagar (Póliza)
		Case "AGL815"
			mclsPayComm = New eAgent.pay_comm
			insPostBook = mclsPayComm.Rea_AGL815(mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("sTypeCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
			Session("P_SKEY") = mclsPayComm.P_SKEY
			mclsPayComm = Nothing
			If insPostBook Then
				insPrintDocuments()
			End If
			
			'+ SIL704: Libro de siniestros			
		Case "SIL704"
			If CStr(Session("BatchEnabled")) <> "1" Then
				mclsLedgerAutDetail3 = New eLedge.LedgerAutDetail
				insPostBook = mclsLedgerAutDetail3.InsCreTmp_Sil704(mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
				Session("P_SKEY") = mclsLedgerAutDetail3.P_SKEY
				mclsLedgerAutDetail3 = Nothing
			Else
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 121
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
				insPostBook = True
			End If
			
			'+ SIL705: Libro de siniestros pagados
		Case "SIL705"
			insPostBook = True
			If CStr(Session("BatchEnabled")) = "1" Then
				lclsBatch_param = New eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 122
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
					
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Session("nCompanyUser"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdDateIni"))
					.Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, Request.Form.Item("tcdDateEnd"))
					
					.Save()
				End With
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				lclsBatch_param = Nothing
			Else
				If insPostBook Then
					insPrintDocuments()
				End If
			End If
			
			'+ COL702: Libro de facturas			
		Case "COL702"
			insPostBook = True
			If insPostBook Then
				insPrintDocuments()
			End If
		
            Case "CRL706"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    mobjBookRep = New eCoReinsuran.ValCoReinsuranRep
                    insPostBook = mobjBookRep.InsPostCRL706(mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("optprocess"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Session("P_SKEY") = mobjBookRep.P_SKEY
                    mobjBookRep = Nothing
				
                    If insPostBook Then
                        Response.Write("<SCRIPT> <b> 'Se generó la clave de proceso: " & Session("P_SKEY") & "');</" & "Script>")
                        insPrintDocuments()
                    End If
				
                Else
				
                    lclsBatch_param = New eSchedule.Batch_Param
				
                    With lclsBatch_param
                        .nBatch = 706
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optProcess"))
					
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
					
                        .Save()
                    End With
				
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				
                    lclsBatch_param = Nothing
				
                    insPostBook = True
				
                End If

        '* CAL602: Interfaz Contable de Recaudación
        Case "COL602"
            If CStr(Session("BatchEnabled")) <> "1" Then
                mobjBookRep = New eBatch.Books
                
                mobjBookRep = New eBatch.Books
                'Se llama a procedimiento para generar libro
                insPostBook = mobjBookRep.Cretmp_Col602(mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    
                Session("P_SKEY") = mobjBookRep.P_SKEY
                    
                Response.Write("<SCRIPT>alert('skey : " & Session("P_SKEY") & "');</" & "Script>")
            Else
                lclsBatch_param = New eSchedule.Batch_Param
				
                With lclsBatch_param
                    .nBatch = 602
                    .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                    .Save()
                End With
				
                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                lclsBatch_param = Nothing
                insPostBook = True
            End If


'* SIL604: Libro de Siniestros
        Case "SIL604"
            If CStr(Session("BatchEnabled")) <> "1" Then
                mobjBookRep = New eBatch.Books
                    
                mobjBookRep = New eBatch.Books
                'Se llama a procedimiento para generar libro
                insPostBook = mobjBookRep.Cretmp_Sil604(mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("optOption"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    
                Session("P_SKEY") = mobjBookRep.P_SKEY
                    
                Response.Write("<SCRIPT>alert('skey : " & Session("P_SKEY") & "');</" & "Script>")
            Else
                lclsBatch_param = New eSchedule.Batch_Param
				
                With lclsBatch_param
                    .nBatch = 214
                    .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("optOption"))
                    .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                    .Save()
                End With
				
                Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                lclsBatch_param = Nothing
                insPostBook = True
            End If

    End Select
	
End Function

'%insPrintDocuments : Realiza la ejecución del reporte
'-------------------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-------------------------------------------------------------------------------------------
	
	Dim mobjDocuments As eReports.Report
	mobjDocuments = New eReports.Report
	
	With mobjDocuments
		Select Case Request.QueryString.Item("sCodispl")
			'+ COL504: Libro timbrado de recuadacion
			Case "COL504"
				
				.sCodispl = "COL504"
				'cambio en el formato del libro
				'.ReportFilename = "COL504.rpt"   
				.ReportFilename = "COL504B.rpt"
				.setStorProcParam(1, Session("P_SKEY"))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcnOption"), eFunctions.Values.eTypeData.etdDouble))
				.setStorProcParam(3, .setdate(Request.Form.Item("tcdDateIni")))
				.setStorProcParam(4, .setdate(Request.Form.Item("tcdDateEnd")))
				Response.Write((.Command))
				
				'+ CAL503: Libro timbrado de produccion
			Case "CAL503"
				.sCodispl = "CAL503"
				.ReportFilename = "CAL503.rpt"
				.setStorProcParam(1, Session("P_SKEY"))
				Response.Write((.Command))
				
				'+ AGL776: Libro timbrado de produccion			        
			Case "AGL776"
				.sCodispl = "AGL776"
				.ReportFilename = "AGL776.rpt"
				.setStorProcParam(1, Session("P_SKEY"))
				Response.Write((.Command))
				
				'+ COL889: Libro timbrado de produccion			        
			Case "COL889"
				.sCodispl = "COL889"
				.ReportFilename = "COL889.rpt"
				
				.setStorProcParam(1, .setdate(Request.Form.Item("tcdDateIni")))
				.setStorProcParam(2, .setdate(Request.Form.Item("tcdDateEnd")))
				
				Response.Write((.Command))
				
				'+ AGL815: Comisiones por Pagar (Póliza)
			Case "AGL815"
				.sCodispl = "AGL815"
				.ReportFilename = "AGL815.rpt"
				.setStorProcParam(1, Request.Form.Item("tcdDateIni"))
				.setStorProcParam(2, Request.Form.Item("tcdDateEnd"))
				.setStorProcParam(3, Request.Form.Item("cbeBranch"))
				.setStorProcParam(4, Session("P_SKEY"))
				Response.Write((.Command))
				
				'+ COL702: Libro de facturas
			Case "COL702"
				.sCodispl = "COL702"
				.ReportFilename = "COL702.rpt"
				.setStorProcParam(1, mobjValues.StringToType(Request.Form.Item("tcdDateIni"), eFunctions.Values.eTypeData.etdDate))
				.setStorProcParam(2, mobjValues.StringToType(Request.Form.Item("tcdDateEnd"), eFunctions.Values.eTypeData.etdDate))
				
				Response.Write((.Command))
				
				'+ SIL704: Libro de siniestros
			Case "SIL704"
				.sCodispl = "SIL704"
				.ReportFilename = "SIL704.rpt"
				.setStorProcParam(1, Session("P_SKEY"))
				Response.Write((.Command))
				
				'+ SIL705: Libro de siniestros pagados
			Case "SIL705"
				.sCodispl = "SIL705"
				.ReportFilename = "SIL705.rpt"
				.setStorProcParam(1, Session("nCompanyUser"))
				.setStorProcParam(2, Request.Form.Item("tcdDateIni"))
				.setStorProcParam(3, Request.Form.Item("tcdDateEnd"))
				Response.Write((.Command))
		End Select
	End With
	mobjDocuments = Nothing
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("valclaimrep")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "valbookrep"

mstrCommand = "&sModule=Book&sProject=Book&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>





	
	
<SCRIPT>
// Función que retorna a la pagina anterior
//------------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------------
    self.history.go(-1)}

// Función que define la ubicación de la Pagina
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
</SCRIPT>
</HEAD>
<BODY>

	<%
If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
	
	'+ Si no se han validado los campos de la página
	
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValBook
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ClaimErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostBook Then
	            If Request.Form.Item("sCodisplReload") = vbNullString Then
	                Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
	            Else
	                Response.Write("<SCRIPT>insReloadTop(true);</SCRIPT>")
	            End If
	End If
End If

mobjValues = Nothing
mobjBookRep = Nothing
%>
	</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("valbookrep")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




