<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eSchedule" %>
<%@ Import namespace="eReports" %>
<script language="VB" runat="Server">

Dim sKey As Object
Dim mobjValues As eFunctions.Values

Dim mclsCoReinsuran As eCoReinsuran.ValCoReinsuranRep
Dim mclsCoReinsuranUtil As eCoReinsuran.t_reinsurutil
Dim mclsCoReinsuranRein As eCoReinsuran.Reinsuran
Dim mclsCoReinsuranClaim As eClaim.Claim
Dim mclsCoReinsuranCuentecn As eCoReinsuran.Cuentecn

Dim mstrErrors As String

'- Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'- de la página que la invoca.

Dim mstrCommand As String
Dim mstrString As String


'% insValCoReinsuran: Se realizan las validaciones masivas de la forma.
'-------------------------------------------------------------------------------------------
Function insValCoReinsuran() As String
	'-------------------------------------------------------------------------------------------
	Dim lintString As Object
	Dim lclsErrors As Object
	Dim lclsError As eFunctions.Errors
	
	lclsError = New eFunctions.Errors
	
        
	Dim lobjGeneral As eGeneral.GeneralFunction
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CRL001: Generación de cesiones de primas.
		
		Case "CRL001"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ CRL002: Generación de cesiones de siniestros.
		Case "CRL002"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL002(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ CRL003: Relación de cesiones de siniestros.
			
		Case "CRL003"
			With Request
				insValCoReinsuran = mclsCoReinsuranClaim.insValCRL003_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), 1)
			End With
			mclsCoReinsuran = Nothing
        
            Case "CRL995"
                With Request
                    'insValCoReinsuran = mclsCoReinsuranClaim.insValCRL995_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
                    'insValCoReinsuran = "Falta Metodo"
                    insValCoReinsuran = ""
                End With
                mclsCoReinsuran = Nothing
                
                '+ CRL004: Relación de cesiones de prima.
		Case "CRL004"
			With Request
				insValCoReinsuran = mclsCoReinsuranRein.InsValCRL004_K(.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ CRL005: Impresión de seciones de prima.
			
		Case "CRL005"
			With Request
				insValCoReinsuran = mclsCoReinsuranRein.insValCRL005_K(.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCessType"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ CRL006: Impresión de cesiones de siniestros.
			
		Case "CRL006"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL006(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdDateFrom"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCessType"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ CRL007: Generación cuentas técnicas de reaseguro.
		Case "CRL007"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL007(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
				
			End With
			
			'+ CRL008: Generación cuentas técnicas de reaseguro facultativo.
		Case "CRL008"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL008(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
			'+ CRL009: Gen. de cesiones de siniestros en reaseg.
		Case "CRL009"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL009(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
			End With
			
			'+ Impresión de ctas. técnicas de reaseguro.
		Case "CRL010"
			With Request
				insValCoReinsuran = mclsCoReinsuranCuentecn.insValCRL010_K(.QueryString.Item("Action"), .QueryString.Item("sCodispl"), mobjValues.StringToType(Request.Form.Item("cbePerType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPerNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear_contr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cboContraType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
				
			End With
			
			'+ CRL011 - Generación de cuentas corrientes de coaseguro.
			
		Case "CRL011"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL011(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdLong))
			End With
			
			'+ CRL012 - Generación de cuentas corrientes de reaseguro.
			
		Case "CRL012"
			With Request
				insValCoReinsuran = mclsCoReinsuran.insValCRL012(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeTypeRea"), eFunctions.Values.eTypeData.etdDouble))
			End With
			
                '+ CRL013: Generación de cesiones de primas RNP.
		
            Case "CRL013"
                With Request
                    insValCoReinsuran = mclsCoReinsuran.insValCRL013(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
                End With
                
                '+ CRL012 - Generación de ordenes de pago de una cuenta técnica.
            Case "CRL046"
                insValCoReinsuran = mclsCoReinsuran.insValCRL046(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
			
            Case "CRL663"
                With Request
                    insValCoReinsuran = mclsCoReinsuran.insValCRL663(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), Session("nCompanyUser"))
                End With
			
            Case "CRL893"
                With Request
                    insValCoReinsuran = mclsCoReinsuran.insValCRL893(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnType_rel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch_rei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate))

                End With
			
            Case "CRL894"
                With Request
                    insValCoReinsuran = mclsCoReinsuran.insValCRL894(.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("tcnMonth_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMonth_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnYear_end"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+ CRL895: Estado de cuenta por periodo
            Case "CRL895"
                With Request
                    insValCoReinsuran = mclsCoReinsuran.insValCRL895_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
                End With
                '			Set mclsCoReinsuran = Nothing
			
            Case "CRL847_1"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        lobjGeneral = New eGeneral.GeneralFunction
					
                        Session("sKey") = lobjGeneral.getsKey(Session("nUsercode"))
                        Session("valTab_79") = mobjValues.StringToType(.Form.Item("valTab_79"), eFunctions.Values.eTypeData.etdDouble)
                        Session("valTab_gencov") = .Form.Item("valTab_gencov")
                        Session("cbeBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
                        Session("ValMaxRet") = mobjValues.StringToType(.Form.Item("ValMaxRet"), eFunctions.Values.eTypeData.etdDouble)
                        Session("tcdEffecdate") = mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
                        Session("IndSoloCorredores") = .Form.Item("IndSoloCorredores")
					
                        insValCoReinsuran = mclsCoReinsuran.insValCRL847_1_K(mobjValues.StringToType(.Form.Item("valTab_79"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valTab_gencov"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("ValMaxRet"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                        lobjGeneral = Nothing
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValCoReinsuran = mclsCoReinsuran.insValCRL847_1(.Form.Item("tcsCod_cumulo"), mobjValues.StringToType(.Form.Item("tcnVal_Max_Ces"), eFunctions.Values.eTypeData.etdDouble))
						
                        End If
                    End If
                    'Set lclsError = Nothing
				
                End With
			
            Case Else
                insValCoReinsuran = "insValCoReinsuran: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostCoReinsuran: Se realizan las actualizaciones de las ventanas.
    '-------------------------------------------------------------------------------------------
    Function insPostCoReinsuran() As Boolean
        '-------------------------------------------------------------------------------------------
        Dim lclsBatch_param As eSchedule.Batch_param
        Dim sExecute As String
	
	
        Dim lclsTmp_Crl847A As eCoReinsuran.Tmp_Crl847A
        Dim lclsTmp_Crl847 As eCoReinsuran.ValCoReinsuranRep
        Dim lobjGeneral As eGeneral.GeneralFunction
        Select Case Request.QueryString.Item("sCodispl")
		
            '+ CRL001: Generación de cesiones de primas.
            Case "CRL001"
			
                If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                    sExecute = "1"
                Else
                    'Forma preliminar
                    sExecute = "2"
                End If
			
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.UpdateCRL001(mobjValues.StringToType(Session("dLastExecuteDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), sExecute)
				
                    If insPostCoReinsuran Then
                        Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 42
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        '+Parametros de proceso se toman según rutina UpdateCRL001 de eCoreinsuran.ValCoreinsuranRep [v27]
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, 1)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sExecute)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostCoReinsuran = True
                End If
			
                '+CRL002: Generación de cesiones de siniestros.
            Case "CRL002"
			
                'Forma preliminar
                If Request.Form.Item("optEjecucion") = "2" Then 'Forma preliminar
                    sExecute = "2"
                Else
                    sExecute = "1" 'Forma definitiva 
                End If
			
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.UpdateCRL002(mobjValues.StringToType(Session("dLastExecuteDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble), sExecute, mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdLong))
				
                    If insPostCoReinsuran Then
                        Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 43
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("dLastExecuteDate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sExecute)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdLong))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, "1")
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, eRemoteDB.Constants.intNull)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostCoReinsuran = True
                End If
			
                '+ CRL003: Relación de cesiones de siniestros.
			
            Case "CRL003"
                insPostCoReinsuran = True
			
                '+ CRL004: Relación de cesiones de prima.		    
		
            Case "CRL995"
                insPostCoReinsuran = True
			
                '+ CRL004: Relación de cesiones de prima.		                
            Case "CRL004"
			
                If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                    sExecute = "1"
                Else
                    'Forma preliminar
                    sExecute = "2"
                End If
			
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.insPostCRL004(mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), sExecute, mobjValues.StringToType(Request.Form.Item("cbeCompRei"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("dtcClient"))
                    If insPostCoReinsuran Then
                        Session("sKey") = mclsCoReinsuran.sKey
                        Call insPrintDocuments()
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_param
                    With lclsBatch_param
                        .nBatch = 44
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sExecute)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeCompRei"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Request.Form.Item("dtcClient"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nUserCode"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaRes, .sKey)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostCoReinsuran = True
                End If
			
			
			
                '+ CRL005: Impresión de sesiones de prima.		    
			
            Case "CRL005"
                insPostCoReinsuran = True
			
                '+ CRL006: Impresión de cesiones de siniestros.
			
            Case "CRL006"
                insPostCoReinsuran = True
			
                '+ CRL007: Generación cuentas técnicas de reaseguro.
			
            Case "CRL007"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.UpdateCRL007(mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeContra"), eFunctions.Values.eTypeData.etdDouble))
				
                    If insPostCoReinsuran Then
                        Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                    End If
                Else
                    Dim lintDays As Integer
                    Dim lintMonth As Integer
                    Dim lintYear As Integer
                    Dim lstrDate As String
                    Dim dLastDate As Date
                    
                    lintMonth = mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble)
                    lintYear = mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble)

                    Select Case lintMonth
                        Case 1, 3, 5, 7, 8, 10, 12
                            lintDays = 31
				
                        Case 4, 6, 9, 11
                            lintDays = 30
				
                        Case 2
                            If (lintYear Mod 4) = 0 Then
                                lintDays = 29
                            Else
                                lintDays = 28
                            End If
                    End Select
                    
                    '+ Se arma la fecha con el mes extraído y con los días introducidos en el campo días.
                    lstrDate = Trim(Str(lintDays)) & "/" & Trim(Str(lintMonth)) & "/" & Trim(Str(lintYear))
			
                    dLastDate = CDate(lstrDate)
                    
                    
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 27
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, dLastDate)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, DateSerial(mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), 1, 1))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeTypeContra"), eFunctions.Values.eTypeData.etdDouble))
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostCoReinsuran = True
                End If
			
                '+ CRL008: Generación cuentas técnicas de reaseguro facultativo.
            Case "CRL008"
                With mobjValues
                    insPostCoReinsuran = mclsCoReinsuran.UpdateCRL008(.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                End With
                If insPostCoReinsuran Then
                    Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                End If
			
                '+ CRL009: Gen. de cesiones de siniestros en reaseg.
			
            Case "CRL009"
                If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                    sExecute = "1"
                Else
                    'Forma preliminar
                    sExecute = "2"
                End If
                
                lobjGeneral = New eGeneral.GeneralFunction
					
                sKey = lobjGeneral.getsKey(Session("nUsercode"))
                lobjGeneral = Nothing
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.UpdateCRL009(mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), sExecute, sKey)
                    If insPostCoReinsuran Then
                        Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 45
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, Session("nUserCode"))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sExecute)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostCoReinsuran = True
                End If
                
                
                '+ CRL010: Impresión de ctas. técnicas de reaseguro.
            Case "CRL010"
                insPostCoReinsuran = True
			
                '+ CRL011: Generación de cuentas corrientes de coaseguro.
            Case "CRL011"
                insPostCoReinsuran = mclsCoReinsuran.UpdateCRL011(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
                '+ CRL012 - Generación de cuentas corrientes de reaseguro.
            Case "CRL012"
                insPostCoReinsuran = mclsCoReinsuran.UpdateCRL012(mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeRea"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("sTypeCompanyUser"), eFunctions.Values.eTypeData.etdDouble))
			
                '+ CRL013: Generación de cesiones de primas RNP.
            Case "CRL013"
			
                If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                    sExecute = "1"
                Else
                    'Forma preliminar
                    sExecute = "2"
                End If
			
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.UpdateCRL013(mobjValues.StringToType(Request.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), sExecute)
				
                    If insPostCoReinsuran Then
                        Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                    End If
                Else
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 170
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateStart"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, sExecute)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                    insPostCoReinsuran = True
                End If
            Case "CRL046"
                If CStr(Session("BatchEnabled")) <> "1" Then
                    insPostCoReinsuran = mclsCoReinsuran.insPostCRL046(mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUserCode"))
				
                    If insPostCoReinsuran Then
                        Response.Write("<SCRIPT>alert('El proceso finalizo satisfactoriamente');</" & "Script>")
                    End If
                Else
                    insPostCoReinsuran = True
                    lclsBatch_param = New eSchedule.Batch_Param
                    With lclsBatch_param
                        .nBatch = 46
                        .nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .sKey)
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                        .Add(eSchedule.Batch_Param.enmBatchParArea.batchParAreaProc, .nUsercode)
                        .Save()
                    End With
                    Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
                    lclsBatch_param = Nothing
                End If
						
                '+ CRL663 - Generación de ordenes de pago de una cuenta técnica.
            Case "CRL663"
                insPostCoReinsuran = mclsCoReinsuran.UpdateCRL663(mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
            Case "CRL893"
			
			
                ' If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                'mstrString = "&nCompany=" & mobjValues.StringToType(Request.Form.Item("valCompany"), eFunctions.Values.eTypeData.etdDouble) & "&nNumber=" & mobjValues.StringToType(Request.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble) & "&nBranch_rei=" & mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble) & "&nType=" & mobjValues.StringToType(Request.Form.Item("cbeContraType"), eFunctions.Values.eTypeData.etdDouble) & "&nType_rel=" & mobjValues.StringToType(Request.Form.Item("hddnType_rel"), eFunctions.Values.eTypeData.etdDouble) & "&dDate_ini=" & mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate) & "&dDate_end=" & mobjValues.StringToType(Request.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate) & "&nTypeproc=" & mobjValues.StringToType(Request.Form.Item("optEjecucion"), eFunctions.Values.eTypeData.etdDouble)
				
                Session("nCompany") = Request.Form.Item("valCompany")
                Session("nNumber") = mobjValues.StringToType(Request.Form.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble)
                Session("dDate_ini") = mobjValues.StringToType(Request.Form.Item("tcdDateTo"), eFunctions.Values.eTypeData.etdDate)
                Session("dDate_end") = mobjValues.StringToType(Request.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate)
                Session("nTypeproc") = Request.Form.Item("optEjecucion")
				
                'insPostCoReinsuran = True
                'If Request.Form.Item("chkPrint") = "1" Then
                'Call insPrintDocuments()
                'End If
                'Else
                mclsCoReinsuranUtil = New eCoReinsuran.t_reinsurutil
                insPostCoReinsuran = mclsCoReinsuranUtil.insPostCRL893(mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dDate_ini"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("dDate_end"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmount_pr"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnres_risklast"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnres_cllast"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcntotam_in"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAm_comm"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnreser_cl"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnamadmin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnres_risk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnres_risk"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnamlastneg"), eFunctions.Values.eTypeData.etdDouble), CDbl(Request.Form.Item("tcntotam_out")), mobjValues.StringToType(Request.Form.Item("tcnAmountutil"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnAmpartutil"), eFunctions.Values.eTypeData.etdDouble), Session("nTypeproc"))
                'If Request.Form.Item("chkPrint") = "1" Then
                Call insPrintDocuments()
                'End If
				
                'End If
                mclsCoReinsuranUtil = Nothing
			
            Case "CRL894"
                insPostCoReinsuran = True
			
                '+ CRL895: Estado de cuentas por periodo.
			
            Case "CRL895"
			
                If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                    sExecute = "1"
                Else
                    'Forma preliminar
                    sExecute = "2"
                End If
			
                insPostCoReinsuran = mclsCoReinsuran.insPostCRL895(mobjValues.StringToType(Request.Form.Item("tcdInitdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnddate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdLong), sExecute)
			
                If insPostCoReinsuran Then
                    Session("sKey") = mclsCoReinsuran.sKey
                    Call insPrintDocuments()
                End If
			
            Case "CRL847_1"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insPostCoReinsuran = True
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lclsTmp_Crl847A = New eCoReinsuran.Tmp_Crl847A
                        insPostCoReinsuran = lclsTmp_Crl847A.InsPostCRL847_1(Request.QueryString.Item("Action"), Request.Form.Item("tcsCod_cumulo"), mobjValues.StringToType(Request.Form.Item("tcnVal_Max_Ces"), eFunctions.Values.eTypeData.etdDouble), Session("sKey"))
                        lclsTmp_Crl847A = Nothing
                    Else
                        If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                            lclsTmp_Crl847 = New eCoReinsuran.ValCoReinsuranRep
                            'insPostCoReinsuran = True
                            insPostCoReinsuran = lclsTmp_Crl847.InsPostCRL847_1(Session("valTab_79"), Session("valTab_gencov"), Session("cbeBranch"), Session("tcdEffecdate"), Session("ValMaxRet"), Session("IndSoloCorredores"), Session("sKey"))
                            lclsTmp_Crl847 = Nothing
                            Call insPrintDocuments()
                        End If
                    End If
                End If
        End Select
	
        If Request.QueryString.Item("sCodispl") <> "CRL046" And Request.QueryString.Item("sCodispl") <> "CRL847_1" And Request.QueryString.Item("sCodispl") <> "CRL004" And Request.QueryString.Item("sCodispl") <> "CRL893" And Request.QueryString.Item("sCodispl") <> "CRL895" Then
            Call insPrintDocuments()
        End If
    End Function

    '%insPrintDocuments : Realiza la ejecución del reporte
    '-------------------------------------------------------------------------------------------
    Private Sub insPrintDocuments()
        '-------------------------------------------------------------------------------------------
        Dim nCompany As Object
        Dim nCurrency As Object
        Dim nCessType As Object
        Dim nBranchRei As Object
        Dim nCessOri As Object
        Dim mobjDocuments As eReports.Report
        Dim sExecute As String
	
        mobjDocuments = New eReports.Report
	
        With mobjDocuments
            Select Case Request.QueryString.Item("sCodispl")
			
                '+ CRL003: Relación de cesiones de siniestros.
			
                Case "CRL003"
				
                    .sCodispl = "CRL003"
                    .ReportFilename = "CRL003.rpt"
				
                    nCessType = System.DBNull.Value
                    nCompany = Request.Form.Item("cbeCompany")
                    nCurrency = System.DBNull.Value
                    nBranchRei = Request.Form.Item("cbeBranchRei")
                    nCessOri = System.DBNull.Value
				
                    If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                        sExecute = "1"
                    Else
                        'Forma preliminar
                        sExecute = "2"
                    End If
                    'nCessOri   = Request.Form("cbeCessOri")
                    'nCessType  = Request.Form("cbeCessType")		
                    'nCurrency  = Request.Form("cbeCurrency")
				
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdInitdate")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnddate")))
                    .setStorProcParam(3, nCompany)
                    .setStorProcParam(4, nCurrency)
                    .setStorProcParam(5, nCessType)
                    .setStorProcParam(6, nBranchRei)
                    .setStorProcParam(7, nCessOri)
                    .setStorProcParam(8, sExecute)
				
                    Response.Write((.Command))
                    
                Case "CRL995"
				
                    .sCodispl = "CRL995"
                    .ReportFilename = "CRL995.rpt"
                    If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                        sExecute = "1"
                    Else
                        'Forma preliminar
                        sExecute = "2"
                    End If
							
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdEnddate")))
                    .setStorProcParam(2, sExecute)
                				
                    Response.Write((.Command))
                    
                    '+ CRL004: Relación de cesiones de prima.		    
				
                Case "CRL004"
                    .Reset()
                    .sCodispl = "CRL004"
                    If CDbl(Request.Form.Item("cbeCessType")) = 1 Then
                        .ReportFilename = "CRL004A.rpt"
                    Else
                        .ReportFilename = "CRL004.rpt"
                    End If
				
                    .setStorProcParam(1, Session("sKey"))
				
                    Response.Write((.Command))
				
                    '+ CRL005: Impresión de seciones de prima.		    
				
                Case "CRL005"
                    .sCodispl = "CRL005"
                    If CDbl(Request.Form.Item("cbeCessType")) = 1 Then
                        .ReportFilename = "CRL005C.rpt"
                    Else
                        .ReportFilename = "CRL005.rpt"
                    End If
                    If CDbl(Request.Form.Item("cbeCompRei")) <= 0 Then
                        nCompany = -1
                    Else
                        nCompany = Request.Form.Item("cbeCompRei")
                    End If
                    If CDbl(Request.Form.Item("cbeCurrency")) <= 0 Then
                        nCurrency = -1
                    Else
                        nCurrency = Request.Form.Item("cbeCurrency")
                    End If
                    If CDbl(Request.Form.Item("cbeBranchRei")) <= 0 Then
                        nBranchRei = -1
                    Else
                        nBranchRei = Request.Form.Item("cbeBranchRei")
                    End If
				
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdDateFrom")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdDateTo")))
                    .setStorProcParam(3, nCompany)
                    .setStorProcParam(4, nCurrency)
                    .setStorProcParam(5, Request.Form.Item("cbeCessType"))
                    .setStorProcParam(6, nBranchRei)
                    .setStorProcParam(7, Session("nUsercode"))
                    .setStorProcParam(8, Session("nCompanyUser"))
				
                    Response.Write((.Command))
				
                    '+ CRL006: Impresión de cesiones de siniestros.
				
                Case "CRL006"
                    .sCodispl = "CRL006"
                    .ReportFilename = "CRL006.rpt"
                    .setParamField(1, "CessType", "Relación de cesiones de siniestro")
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdDateFrom")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdDateTo")))
                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.Item("cbeCompany"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(6, mobjValues.StringToType(Request.Form.Item("cbeBranchRei"), eFunctions.Values.eTypeData.etdDouble))
                    .setStorProcParam(7, Session("nUsercode"))
                    .setStorProcParam(8, Session("nCompanyUser"))
                    Response.Write((.Command))
				
                    '+ CRL010: Impresión de ctas. técnicas de reaseguro.
				
                Case "CRL010"
                    .sCodispl = "CRL010"
                    .ReportFilename = "CRL010.rpt"
                    .setStorProcParam(1, Request.Form.Item("cbeBranchRei"))
                    .setStorProcParam(2, Request.Form.Item("tcnCompany"))
                    .setStorProcParam(3, Request.Form.Item("cbeCurrency"))
                    .setStorProcParam(4, Request.Form.Item("tcnPerNum"))
                    .setStorProcParam(5, Request.Form.Item("cboContraType"))
                    .setStorProcParam(6, Request.Form.Item("cbePerType"))
                    .setStorProcParam(7, Request.Form.Item("tcnYear_contr"))
				
                    Response.Write((.Command))
                Case "CRL847_1"
                    .sCodispl = "CRL847_1"
                    .ReportFilename = "RPT_REASEGURO_FULLRESCUM.rpt"
                    .setStorProcParam(1, Session("valTab_79"))
                    .setStorProcParam(2, Session("valTab_gencov"))
                    .setStorProcParam(3, Session("cbeBranch"))
                    .setStorProcParam(4, .setdate(Session("tcdEffecdate")))
                    .setStorProcParam(5, Session("ValMaxRet"))
                    .setStorProcParam(6, Session("IndSoloCorredores"))
                    .setStorProcParam(7, Session("sKey"))
				
                    Response.Write((.Command))
				
                    '+ CRL893: DEF
                Case "CRL893"
                    .sCodispl = "CRL893"
                    .ReportFilename = "CRL893.rpt"
                    .setStorProcParam(1, Session("nCompany"))
                    .setStorProcParam(2, .setdate(Session("dDate_ini")))
                    .setStorProcParam(3, .setdate(Session("dDate_end")))
                    .setStorProcParam(4, Session("nNumber"))
                    .setStorProcParam(5, Session("nTypeproc"))
				
                    Response.Write((.Command))
                    '+ CRL894: Impresión de estado de cuenta.
				
                Case "CRL894"
                    .sCodispl = "CRL894"
                    .ReportFilename = "CRL894.rpt"
				
                    If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                        sExecute = "1"
                    Else
                        'Forma preliminar
                        sExecute = "2"
                    End If
				
                    .setStorProcParam(1, Request.Form.Item("cbeBranchRei"))
                    .setStorProcParam(2, Request.Form.Item("tcnCompany"))
                    .setStorProcParam(3, Request.Form.Item("tcnMonth_ini"))
                    .setStorProcParam(4, Request.Form.Item("tcnYear_ini"))
                    .setStorProcParam(5, Request.Form.Item("tcnMonth_end"))
                    .setStorProcParam(6, Request.Form.Item("tcnYear_end"))
                    .setStorProcParam(7, sExecute)
				
                    Response.Write((.Command))
				
                    '+ CRL895: Impresión de estado de cuenta por cobertura
				
                Case "CRL895"
				
                    .sCodispl = "CRL895"
                    .ReportFilename = "CRL895.rpt"
                    nCompany = Request.Form.Item("cbeCompany")
                    nBranchRei = Request.Form.Item("cbeBranchRei")
				
                    If Request.Form.Item("optEjecucion") = "1" Then 'Forma definitiva 
                        sExecute = "1"
                    Else
                        'Forma preliminar
                        sExecute = "2"
                    End If
				
                    .setStorProcParam(1, .setdate(Request.Form.Item("tcdInitdate")))
                    .setStorProcParam(2, .setdate(Request.Form.Item("tcdEnddate")))
                    .setStorProcParam(3, nCompany)
                    .setStorProcParam(4, nBranchRei)
                    .setStorProcParam(5, Session("sKey"))
                    .setStorProcParam(6, sExecute)
				
                    Response.Write((.Command))
				
            End Select
        End With
	
        mobjDocuments = Nothing
    End Sub

</script>
<%Response.Expires = -1

mstrCommand = "&sModule=CoReinsuran&sProject=CoReinsuranRep&sCodisplReload=" & Request.QueryString.Item("sCodispl")
With Server
	mobjValues = New eFunctions.Values
	mclsCoReinsuran = New eCoReinsuran.ValCoReinsuranRep
	mclsCoReinsuranRein = New eCoReinsuran.Reinsuran
	mclsCoReinsuranClaim = New eClaim.Claim
	mclsCoReinsuranCuentecn = New eCoReinsuran.Cuentecn
End With
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>







<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 27/03/06 19:32 $|$$Author: Pgarin $"

//------------------------------------------------------------------------------------------
function CancelErrors(){
//------------------------------------------------------------------------------------------
    self.history.go(-1)}
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
	
        '+ Si no se han validado los campos de la página.
        
    If Request.QueryString.Item("sCodisplReload") = vbNullString Then
        mstrErrors = insValCoReinsuran()
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
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""CoReinsuranErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
            .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
            .Write("</SCRIPT>")
        End With
    Else
        If Not insPostCoReinsuran() Then
            Response.Write("<SCRIPT>alert('Problemas en la actualización');</SCRIPT>")
        End If
        If Request.QueryString.Item("WindowType") <> "PopUp" Then
            If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Or Request.QueryString.Item("sCodispl") = "CRL002" Or Request.QueryString.Item("sCodispl") = "CRL007" Or Request.QueryString.Item("sCodispl") = "CRL003" Or Request.QueryString.Item("sCodispl") = "CRL995" Or Request.QueryString.Item("sCodispl") = "CRL011" Or Request.QueryString.Item("sCodispl") = "CRL004" Then
                If Request.Form.Item("sCodisplReload") = vbNullString Then
                    Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
                Else
                    Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
                End If
            Else
                Response.Write("<SCRIPT>top.frames['fraFolder'].document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrString & """;</SCRIPT>")
            End If
        Else
            Select Case Request.QueryString.Item("sCodispl")
                Case "CRL847_1"
                    Response.Write("<SCRIPT>top.opener.document.location.href='CRL847_1.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
            End Select
        End If
    End If

    mobjValues = Nothing
    mclsCoReinsuran = Nothing
    mclsCoReinsuranRein = Nothing
    mclsCoReinsuranClaim = Nothing
    mclsCoReinsuranCuentecn = Nothing
%>
</BODY>
</HTML>





