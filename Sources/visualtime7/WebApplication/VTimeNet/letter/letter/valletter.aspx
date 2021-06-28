<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eLetter" %>
<%@ Import Namespace="ADODB" %>

<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:58 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjLetter As Object
Dim mobjValues As eFunctions.Values
Dim mclsLetters_as As eLetter.Letters_as

Private mstrErrors As String
Private mstrCtroLettInd As String
Private mstrDelivInvalid As String
Private UploadRequest As Scripting.Dictionary
Private mstrFileContent As String
Private sReload As String

'**-Objetive: The constant to handling the errors and warnings is defined
'-Objetivo: Se define constante para el manejo de errores en caso de advertencias
Dim mstrQueryString As String

'**-Objetive: It allows assigning a code "JavaScript" to be executed, according to a particular functionality of a transaction. 
'-Objetivo: Permite el asignar un código "JavaScript", para ser ejecutado, de acuerdo a una funcionalidad particular de una transacción.
Dim mstrScript As String

'**-Objetive: It indicates if the page Is invoked from the window of errors.
'-Objetivo: Indica si la página es Invocada desde la ventana de errores. 
Dim mblnCloseErrors As Boolean

'**-Objetive: It indicates if the page Is invoked from a "PopUp".
'-Objetivo: Indica si la página es Invocada desde una "PopUp".
Dim mblnPopup As Boolean

'**-Objetive: Auxiliary variable to assign "to top.opener" or "to opener" or the object parent, if the page is called from a POPUP or the window of errors. 
'-Objetivo: Variable auxiliar para asignar "top.opener" o "opener" o el objeto padre, si la página es llamada desde una POPUP o la ventana de errores. 
Dim mstrOpener As String

'**-Objetive: Variable used in the case that the transaction has frame extra defined to part of fraFolder (Ver BC001N o BC001J)
'-Objetivo: Variable usada en el caso que la transacción tenga un frame extra definido a parte del fraFolder (Ver BC001N o BC001J)
Dim mstrSubFrame As String

'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca.
Dim mstrCommand As String

Dim lstrPath As Object



'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValLetter() As String
	'dim NumNull As Object
	'--------------------------------------------------------------------------------------------
	UploadRequest = New Scripting.Dictionary
	mobjValues = New eFunctions.Values
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "LT001"
			mobjLetter = New eLetter.Letter
			If CStr(mstrCtroLettInd) = "1" Then
				Session("lstrCtroLettInd") = "1"
			Else
				Session("lstrCtroLettInd") = "2"
			End If
			'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
			If String.IsNullOrEmpty(Request.Form.Item("tcnMinTimeAns")) Or Request.Form.Item("tcnMinTimeAns") = "" Then
				Session("tcnMinTimeAns") = eremotedb.Constants.intNull
			Else
				Session("tcnMinTimeAns") = Request.Form.Item("tcnMinTimeAns")
			End If
			With mobjValues
				'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
				'insValLetter = mobjLetter.validate(Request.QueryString.Item("Action"), .StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctDescript"), Today, .StringToType(Request.Form.Item("cbeLanguage"), eFunctions.Values.eTypeData.etdInteger), CStr(mstrFileContent), Session("nUserCode"), Session("lstrCtroLettInd"), Session("tcnMinTimeAns"))
				insValLetter = mobjLetter.validate(Request.QueryString.Item("Action"), _
				                                   .StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger), _
				                                   Request.Form.Item("tctDescript"), _
				                                   Today, _
				                                   .StringToType(Request.Form.Item("cbeLanguage"), eFunctions.Values.eTypeData.etdInteger), _
				                                   Cstr(mstrFileContent), _
				                                   Session("nUserCode"), _
				                                   Session("lstrCtroLettInd"), _
				                                   Session("tcnMinTimeAns"))
			End With
		Case "LT002"
			mclsLetters_as = New eLetter.Letters_as
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValLetter = mclsLetters_as.insValMLT002_K("LT002", .Form.Item("valTransaction"))
				Else
					If .QueryString.Item("WindowType") <> "PopUp" Then
					Else
						insValLetter = mclsLetters_as.insValMLT002("LT002", _
						                                           .Form.Item("sAction"), _
						                                           Session("sTransaction"), _
						                                           mobjValues.StringToType(.Form.Item("valProcess"), eFunctions.Values.eTypeData.etdInteger), _
						                                           mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), _
						                                           mobjValues.StringToType(.Form.Item("valLetter"), eFunctions.Values.eTypeData.etdInteger), _
						                                           mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger))
					End If
				End If
			End With
			
		Case "LT004"
			mobjLetter = New eLetter.LettAccuse
			With mobjValues
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insValLetter = mobjLetter.valLT004_K(Request.QueryString.Item("nMainAction"), .StringToType(Request.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdInteger), Request.Form.Item("tctClient"))
				Else
					insValLetter = mobjLetter.valLT004(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("dPrintDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("dToHandOver"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("dAnswerDate"), eFunctions.Values.eTypeData.etdDate))
				End If
			End With
			
		Case "LT970"
			mobjLetter = New eLetter.EndorsLetters
			insValLetter = String.Empty
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					insValLetter = mobjLetter.InsValLT970_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeEndorseType"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("valLetterNum"), eFunctions.Values.eTypeData.etdInteger, True))
				End If
			End With
		
		Case "LT500"
			mobjLetter = New eLetter.PrintDocuments
			
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					insValLetter = mobjLetter.insValLT500_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(.Form.Item("cbeShipmentType"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdInteger, True), .Form.Item("tctClient"), .Form.Item("optCertype"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdInteger, True))
				Else
					insValLetter = mobjLetter.insValLT500(Request.QueryString.Item("sCodispl"))
				End If
			End With
			
	End Select
End Function

'% insPostLetter: Se realizan las actualizaciones de las ventanas
'-----------------------------------------------------------------------------------------------------------------------
Function insPostLetter() As Boolean
	'-----------------------------------------------------------------------------------------------------------------------
	Dim dDate As Date
	Dim sClient As Object
	Dim nLettRequest As Object
	Dim nStatLetter As Byte
	
	Select Case Request.QueryString.Item("sCodispl")
		Case "LT001"
			'If mobjLetter Is Nothing Then
			mobjLetter = New eLetter.Letter
			'End If
			With mobjValues
				'dDate = Today
				If Request.QueryString.Item("Action") = "Add" or Request.QueryString.Item("Action") = "Update" Then
					'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
				    dDate = Today
				'Else
				'	dDate = .StringToDate(Request.Form.Item("tcdEffecDate"))
				insPostLetter = mobjLetter.insPostLT001(Request.QueryString.Item("Action"), _
				                                        .StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger), _
				                                        Request.Form.Item("tctDescript"), _
				                                        dDate, _
				                                       .StringToType(Request.Form.Item("cbeLanguage"), eFunctions.Values.eTypeData.etdInteger), _
				                                        CStr(mstrFileContent), _
				                                        .StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), _
				                                        Session("lstrCtroLettInd"), _
				                                        .StringToType(Request.Form.Item("tcnMinTimeAns"), eFunctions.Values.eTypeData.etdInteger), _
				                                        mstrDelivInvalid)
				Else
				    insPostLetter = True
		        End If
				
			End With
		Case "LT002"
			mclsLetters_as = New eLetter.Letters_as
			
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("sTransaction") = .Form.Item("valTransaction")
					insPostLetter = True
				Else
					If .QueryString.Item("WindowType") <> "PopUp" Then
						insPostLetter = True
					Else
						insPostLetter = mclsLetters_as.insPostLT002(Request.Form.Item("sAction"), _
						                                            Session("sTransaction"), _
						                                            mobjValues.StringToType(.Form.Item("valProcess"), eFunctions.Values.eTypeData.etdInteger), _
						                                            mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), _
						                                            mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), _
						                                            mobjValues.StringToType(.Form.Item("valLetter"), eFunctions.Values.eTypeData.etdInteger), _
						                                            .Form.Item("tctRoutine"), _
						                                            mobjValues.StringToType(.Form.Item("nConsec"), eFunctions.Values.eTypeData.etdLong), _
						                                            Session("nUsercode"), _
						                                            .Form.Item("chksRequired"))
						                                            
						                                       
						                                            
					End If
				End If
			End With
		Case "LT004"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("sClient") = Request.Form.Item("tctClient")
					Session("nLettRequest") = Request.Form.Item("tcnLettRequest")
					insPostLetter = True
				Else
					If .QueryString.Item("WindowType") <> "PopUp" Then
						insPostLetter = True
					Else
						If Session("nLettRequest") = 0 Then
							nLettRequest = mobjValues.StringToType(Request.Form.Item("nLettRequest"), eFunctions.Values.eTypeData.etdInteger)
						Else
							nLettRequest = Session("nLettRequest")
						End If
						
						If CStr(Session("sClient")) = String.Empty Then
							sClient = Request.Form.Item("sClient")
						Else
							sClient = Session("sClient")
						End If
						
						If Request.Form.Item("dAnswerDate") <> String.Empty Then
							nStatLetter = 4 'Respondida Table624
						Else
							If Request.Form.Item("dToHandOver") <> String.Empty Then
								nStatLetter = 3 'Entregada Table624
							Else
								nStatLetter = 2
							End If
						End If
						
						insPostLetter = mobjLetter.insPostLT004(CStr(Request.QueryString.Item("Action")), CShort(mobjValues.StringToType(nLettRequest, eFunctions.Values.eTypeData.etdInteger)), sClient, CDate(mobjValues.StringToType(Request.Form.Item("dAnswerDate"), eFunctions.Values.eTypeData.etdDate)), CDate(mobjValues.StringToType(Request.Form.Item("dToHandOver"), eFunctions.Values.eTypeData.etdDate)), CShort(mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger)), CShort(mobjValues.StringToType(Request.Form.Item("nTypeLetter"), eFunctions.Values.eTypeData.etdInteger)), CShort(mobjValues.StringToType(CStr(nStatLetter), eFunctions.Values.eTypeData.etdInteger)), CShort(mobjValues.StringToType(Request.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdInteger)), mstrFileContent, Session("sInitials"), Session("sAccesswo"))
					End If
				End If
			End With
			
		Case "LT970"
			mobjLetter = New eLetter.EndorsLetters
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					insPostLetter = mobjLetter.InsPostLT970(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeEndorseType"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valLetterNum"), eFunctions.Values.eTypeData.etdInteger))
				Else
					insPostLetter = True
				End If
			End With
			
		Case "LT500"
			mobjLetter = New eLetter.PrintDocuments
			
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insPostLetter = True
				mstrQueryString = "&sClient=" & Request.Form.Item("tctClient") & "&nShipmentType=" & mobjValues.StringToType(Request.Form.Item("cbeShipmentType"), eFunctions.Values.eTypeData.etdInteger) & "&nOfficeAgen=" & mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen"), eFunctions.Values.eTypeData.etdInteger) & "&nAgency=" & mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdInteger) & "&nIntermed=" & mobjValues.StringToType(Request.Form.Item("valIntermedia"), eFunctions.Values.eTypeData.etdInteger) & "&sCertype=" & Request.Form.Item("optCertype") & "&nBranch=" & mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger) & "&nProduct=" & mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger) & "&nPolicy=" & mobjValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdInteger) & "&nCertif=" & mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdInteger) & "&sTypeDocument=" & Request.Form.Item("cbeTypeDocument") & "&sStatusDocument=" & Request.Form.Item("cbeStatusDocument")
			Else
				insPostLetter = True
				insPrintDocuments()
			End If
			
	End Select
End Function

'**% insPrintDocuments: This function allows to make the call to the corresponding report depending on the Codispl of the window.  
'% insPrintDocuments: Esta función permite realizar el llamado al reporte correspondiente dependiendo del Codispl de la ventana.
'----------------------------------------------------------------------------------------------------------------------------------------
Private Function insPrintDocuments()
'----------------------------------------------------------------------------------------------------------------------------------------
    Dim mobjDocuments As eReports.Report
    mobjDocuments = New eReports.Report
    Dim bPrint As Boolean
    Dim ReportsCollection As Object	
    
    Select Case Request.QueryString.Item("sCodispl")
'% Impresión de Documentos    
        Case "LTL001"
            With mobjDocuments
                .sCodispl = "LTL001"
                    '.nUserCode = Session("nUsercode")
                .ReportFilename = "LTL001.rpt"
                .setStorProcParam(1, .setdate(Request.Form.Item("tcdEffecdate")))
                Response.Write((.Command))
            End With
            
		Case "LT500"
			Dim lintCount as Integer
			Dim lcolPrintDocumentss As eLetter.PrintDocumentss
			Dim lclsPrintDocuments As eLetter.PrintDocuments
			Dim lclsPrintDocuments1 As New eLetter.PrintDocuments
			Dim lclsProdmaster As eProduct.Product
			Dim lclsPolicy_po As ePolicy.Policy
			Dim insPostLT500 As Boolean 
			Dim PrintLetter as Boolean
			Dim mintChange As String
			Dim lstrBrancht as String
			Dim loc_nReceipt as Integer
			dim sClassificationPolicy as string

			ReportsCollection = ""
			bPrint = True
			Session("bPrint") = bPrint
			
			lcolPrintDocumentss = New eLetter.PrintDocumentss
			
			lintCount = 0
            For	Each mintChange In Request.Form.GetValues("sAuxSel")
           
                If Request.Form.GetValues("sAuxSel").GetValue(lintCount) = "1" Then
                
                    lclsProdMaster = New eProduct.Product
                    If lclsProdMaster.FindProdMaster(mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
                                                     mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger)) Then
		                lstrBrancht = lclsProdMaster.sBrancht
		            End If
		            lclsProdMaster = Nothing
		           
		            lclsPrintDocuments1 = New eLetter.PrintDocuments 
		            PrintLetter = lclsPrintDocuments1.AddUpdatetmpPrintDocuments("Del")
		            lclsPrintDocuments1 = Nothing
                
                    Select Case Trim(Request.Form.GetValues("hddTypeDocument").GetValue(lintCount))
                    Case "1", "5"
                        With mobjDocuments
                        ' Si no es cotizacion
		                    If Request.Form.GetValues("hddsCertype").GetValue(lintCount) = "2" Then
		                        If lstrBrancht = "1" Then
		                        '+ Si no es colectivo
		                        
		                            lclsPrintDocuments1 = New eLetter.PrintDocuments
			                        if lclsPrintDocuments1.Find_Receipt(Request.Form.GetValues("hddsCertype").GetValue(lintCount), _
			                                                            mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
			                                                            mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
			                                                            mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger)) Then
			                            loc_nReceipt = lclsPrintDocuments1.nReceipt
			                        Else
			                            loc_nReceipt = 0
			                        End if 
			                        lclsPrintDocuments1 = Nothing
			                        
			                        lclsPolicy_po = New ePolicy.Policy
			                        If lclsPolicy_po.Find(Request.Form.GetValues("hddsCertype").GetValue(lintCount), _
			                                              mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
			                                              mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
			                                              mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger)) Then
			                                                            
			                            sClassificationPolicy = lclsPolicy_po.sPoliType
			                        End if
			                        lclsPolicy_po = Nothing
		                       
		                            .sCodispl = "CAL001"
		                            If sClassificationPolicy <> "2" Then       '<-- Campo Policy.sPoliType
	                                    .ReportFilename = "CAL001_A.rpt"
	                                Elseif sClassificationPolicy = "2" Then
	                                    If Request.Form.GetValues("hddnCertif").GetValue(lintCount) <= 0 Then
	                                        .ReportFilename = "CAL001_A.rpt"
	                                    Else    	                                        
	                                        .ReportFilename = "CAL001_A_CERTIF.rpt"
	                                    End If
	                                End If
		                       
	                                .setStorProcParam(1, Request.Form.GetValues("hddsCertype").GetValue(lintCount))                                                                                'param. sCerType  : Tipo de Certificado
			                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))                'param. nBranch   : Ramo
			                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))               'param. nProduct  : Producto
			                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))                'param. nPolicy   : Póliza
			                        .setStorProcParam(5, mobjValues.StringToType(Request.Form.GetValues("hddnCertif").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))                'param. nCertif   : Certificado
			                        .setStorProcParam(6, .setdate(Today))                                                                                                                          'param. dEffecDate: Fecha de Efecto
			                        .setStorProcParam(7, "1")                                                                                                                                      'param. sOptEje   : Opción de Ejecución
			                        .setStorProcParam(8, "")                                                                                                                                       'parám. strQuery (consulta masiva)
			                        .setStorProcParam(9, "")                                                                                                                                       'parám. sTransac  : Tipo de Transaccíón
			                        .setStorProcParam(10, loc_nReceipt)                                                                                                                            'parám. nReceipt  : Numero de recibo 
                       			
			                        If bPrint Then
										ReportsCollection = ReportsCollection & .Command & "@"
										.nHeight = 100
										.nLeft   = 100
										.nWidth  = 100
										.nTop    = 100
									Else
										Response.Write(.Command)
									End If
									.Reset()
           			
	                                '+ Si no es colectivo
	                                If sClassificationPolicy <> "2" Then
                                    'Se imprime Relaciones de Seguro de Vida Individual
                                        .sCodispl = "CAL001"
                                        .ReportFilename = "CAL001_DET.rpt"
                                		
                                        .setStorProcParam(1, mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))            'param. nBranch   : Ramo
                                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))           'param. nProduct  : Producto
                                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))            'param. nPolicy   : Póliza
                                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnCertif").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))            'param. nCertif   : Certificado
                                        .setStorProcParam(5, loc_nReceipt)                                                        'parám. nReceipt  : Numero de recibo 
                                		
                                        '.bTimeOut = mblnTimeOut
                                        If bPrint Then
									        ReportsCollection = ReportsCollection & .Command & "@"
									        .nHeight = 100
									        .nLeft   = 100
									        .nWidth  = 100
									        .nTop    = 100
								        Else
									        Response.Write(.Command)
								        End If
								        .Reset()
                                    Else
                                   'Se imprime Relaciones de Seguro de Vida Colectivo
                                        'mblnTimeOut = True
                                        .sCodispl = "CAL001"
                                        .ReportFilename = "CAL001_DET_COLE.rpt"
                            			
                                        .setStorProcParam(1, mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))            'param. nBranch   : Ramo
                                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))           'param. nProduct  : Producto
                                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))            'param. nPolicy   : Póliza
                                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnCertif").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))            'param. nCertif   : Certificado
                                        .setStorProcParam(5, loc_nReceipt)                                                        'parám. nReceipt  : Numero de recibo 
                            			
                                        '.bTimeOut = mblnTimeOut
                                        If bPrint Then
										    ReportsCollection = ReportsCollection & .Command & "@"
										    .nHeight = 100
										    .nLeft   = 100
										    .nWidth  = 100
										    .nTop    = 100
									    Else
										    Response.Write(.Command)
									    End If
									    .Reset()
                                    End If
                                Else
                                    .ReportFilename = "CAL001_Auto.rpt"
			                        .sCodispl = "CAL001"
			                        .setStorProcParam(1, Request.Form.GetValues("hddsCertype").GetValue(lintCount))               'param. sCerType  : Tipo de Certificado
			                        .setStorProcParam(2, mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))                'param. nBranch   : Ramo
			                        .setStorProcParam(3, mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))               'param. nProduct  : Producto
			                        .setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))                'param. nPolicy   : Póliza
			                        .setStorProcParam(5, mobjValues.StringToType(Request.Form.GetValues("hddnCertif").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))                'param. nCertif   : Certificado
			                        .setStorProcParam(6, mobjValues.StringToType(.setdate(Today), eFunctions.Values.eTypeData.etdDate))                                                         'param. dEffecDate: Fecha de Efecto
			                        .setStorProcParam(7, "1")                                                                     'param. sOptEje   : Opción de Ejecución
			                        .setStorProcParam(8, "")                                                                      'parám. strQuery (consulta masiva)
			                        .setStorProcParam(9, "")                                                                      'parám. sTransac  : Tipo de Transaccíón
			                        .setStorProcParam(10, 0)
			                        
			                        If bPrint Then
										ReportsCollection = ReportsCollection & .Command & "@"
										.nHeight = 100
										.nLeft   = 100
										.nWidth  = 100
										.nTop    = 100
									Else
										Response.Write(.Command)
									End If
									.Reset()
                                End If
		                    Else
		                'Si es Cotización
                                If CInt(Request.Form.GetValues("hddnCertif").GetValue(lintCount)) > 0 Then
                                    'mblnTimeOut = True
                                    .sCodispl = "CAL001"
                                    .ReportFilename = "QUOTE.rpt" 
                                    .setStorProcParam(1, 3)  
                                    .setStorProcParam(2, mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))
                                    .setStorProcParam(3, mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))
                                    .setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))
                                    
                                     If bPrint Then
										ReportsCollection = ReportsCollection & .Command & "@"
										.nHeight = 100
										.nLeft   = 100
										.nWidth  = 100
										.nTop    = 100
									Else
										Response.Write(.Command)
									End If
									.Reset()
                                End If
                            End If
		                
                        End With 
                    Case "2"
                        With mobjDocuments
                            .sCodispl = "CAL002"
                            .ReportFilename = "CAL002.rpt"
                            '.setParamField(1,"nInsur_area",Session("nInsur_area"))
                            .setStorProcParam(1, mobjValues.StringToType(Request.Form.GetValues("hddnOfficeAgen").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(2, mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(3, mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(4, mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdDouble))
                            .setStorProcParam(5, 0)
                            .setStorProcParam(6, 0)
                            .setStorProcParam(7, 0)
                            .setStorProcParam(8, 0)
					        .setStorProcParam(9, .setdate(Today)) 					
					        .setStorProcParam(10, VbNullString)
    					     					
                            If bPrint Then
								ReportsCollection = ReportsCollection & .Command & "@"
								.nHeight = 100
								.nLeft   = 100
								.nWidth  = 100
								.nTop    = 100
							Else
								Response.Write(.Command)
							End If
							.Reset()
                        End With
        
                    Case "3"
                    '% Impresión de la Carta
                        'PrintLetter = insOpenDocument(lclsPrintDocuments1.nLettRequest, _
						'						              lclsPrintDocuments.sClient, _
						'						              nIndex)
                    Case "4"
                    '% Impresion del Cuestionario
                        'With mobjDocuments
                        'End With
                    End Select
                    
                    lclsPrintDocuments1 = New eLetter.PrintDocuments
                    PrintLetter = lclsPrintDocuments1.AddUpdatetmpPrintDocuments("Add", Request.Form.GetValues("hddsClient").GetValue(lintCount), Request.Form.GetValues("hddsCertype").GetValue(lintCount), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnBranch").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnProduct").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnPolicy").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnCertif").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnOfficeAgen").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnAgency").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnIntermedia").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnShipmentType").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				Request.Form.GetValues("hddTypeDocument").GetValue(lintCount), 0, _
																				mobjValues.StringToType(Request.Form.GetValues("hddnLettRequest").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(sClassificationPolicy , eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnCodForm").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnConsec").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddsDitribution").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger), _
																				mobjValues.StringToType(Request.Form.GetValues("hddnSituation").GetValue(lintCount), eFunctions.Values.eTypeData.etdInteger))
                    
                    lclsPrintDocuments1 = Nothing
                    
                End if
                lintCount = lintCount + 1
            
                'With lobjDocuments
			    '	.bTimeOut = True
			    '	.nTimeOut = 1000
			    '	.sCodispl = "LTL500"
			    '	.ReportFilename = "LTL500.rpt"
    				
			    '	.setStorProcParam(1, Request.QueryString("sClient"))
			    '	.setStorProcParam(2, Request.QueryString("sCertype"))
			    '	.setStorProcParam(3, Request.QueryString("nBranch"))
			    '	.setStorProcParam(4, Request.QueryString("nProduct"))
			    '	.setStorProcParam(5, Request.QueryString("nPolicy"))
			    '	.setStorProcParam(6, Request.QueryString("nCertif"))
			    '	.setStorProcParam(7, Request.QueryString("nOfficeAgen"))
			    '	.setStorProcParam(8, Request.QueryString("nAgency"))
			    '	.setStorProcParam(9, Request.QueryString("nIntermed"))
			    '	.setStorProcParam(10, Request.QueryString("nShipmentType"))
			    '	.setStorProcParam(11, Request.QueryString("sTypeDocument"))
    				
			    '	.bPrint = bPrint
			    '	.bMultiple = True
    																	
			    '	If bPrint Then
			    '		ReportsCollection = ReportsCollection & .Command & "@"
			    '		.nHeight = 100
			    '		.nLeft   = 100
			    '		.nWidth  = 100
			    '		.nTop    = 100
			    '	Else
			    '		Response.Write(.Command)
			    '	End If				
			    '	.Reset			
			    'End With
                                    
                '% Se actualiza el Status de los documentos a impreso
                lclsPrintDocuments1 = New eLetter.PrintDocuments
	            insPostLT500 = lclsPrintDocuments1.InsPostLT500("Update", _
			    												Session("nUsercode"))
			    
			    lclsPrintDocuments1 = Nothing
			Next mintChange
			lcolPrintDocumentss = Nothing
	End Select

    mobjDocuments = Nothing
    
	If bPrint Then
		Session("ReportsCollection") = ReportsCollection
		Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>") 
		Response.Write("ShowPopUp('/VTimeNet/Common/Reports/Interface.aspx', 'Interface', 0, 0, 'yes', 'no', 10000, 10000);")
		Response.Write("</" & "SCRIPT>")
	End If		    
End Function

'% getByteString: Conversión de los datos de String a Byte
'--------------------------------------------------------------------------------------------
Function getByteString(ByRef StringStr As String) As String 
	'--------------------------------------------------------------------------------------------
	Dim i As Integer
	Dim char_Renamed As String
	For i = 1 To Len(StringStr)
		char_Renamed = Mid(StringStr, i, 1)
		getByteString = getByteString & Chr(Asc(char_Renamed))
	Next 
End Function


'% insUpLoadFile: Se encarga de subir el archivo seleccionado al servidor según ruta pasada como parámetro.
'% FilePath: Ruta física donde se va almacenar el archivo en el servidor. Eje. "c:\InetPub\UpLoad\"
'--------------------------------------------------------------------------------------------
Function insUpLoadFile(ByRef FilePath As Object) As Boolean
	'--------------------------------------------------------------------------------------------
	Dim ForWriting As Byte
	Dim adLongVarChar As Integer
	Dim lngNumberUploaded As Byte
	Dim LenBinary As Object
	Dim strBoundry As String
	Dim lngBoundryPos As Integer
	Dim lngCurrentBegin As Integer
	Dim lngCurrentEnd As Integer
	Dim strData As String
	Dim strDataWhole As String
	Dim lngEndFileName As Integer
	Dim FileName As String
	Dim lngBeginPos As Integer
	Dim ByteCount As Integer
	Dim RequestBin() As Byte
	Dim PosBeg As Double
	Dim PosEnd As Integer
	Dim boundary As String
	Dim boundaryPos As Integer
	Dim Pos As Integer
	Dim Name As String
	Dim PosFile As Integer
	Dim ContentType As String
	Dim Value As String
	Dim PosBound As Integer
	Dim PrevPos As Integer
	Dim tmpLng As Integer
	Dim lngCt As Integer
	Dim strFileData As String
	Dim RST As ADODB.Recordset

    'Posición inicial del patron utilizado para delimitar fin de archivo MS Office 2007
    Dim nThemedata As Integer            

	UploadRequest = New Scripting.Dictionary
	
	ForWriting = 2
	adLongVarChar = 201
	lngNumberUploaded = 0
	
	ByteCount = Request.TotalBytes
	RequestBin = Request.BinaryRead(ByteCount)
	
	RST = New ADODB.Recordset
	LenBinary = Len(RequestBin.toString)
	
	If LenBinary > 0 Then
		RST.Fields.Append("myBinary", adLongVarChar, LenBinary)
		RST.Open()
		RST.AddNew()
		RST.Fields("myBinary").AppendChunk(RequestBin)
		RST.Update()
		strDataWhole = IIF(IsDBNull(RST.Fields.Item("myBinary").Value), Nothing, RST.Fields.Item("myBinary").Value)
	End If
	
	'+ Se calcula el número de elementos a evaluar
	PosBeg = 1
	PosEnd = ByteCount
	
	boundary = Mid(System.Text.Encoding.Unicode.GetString(RequestBin), 1, IIF(PosEnd - PosBeg<=0,1,PosEnd - PosBeg))
	boundaryPos = InStr(1, System.Text.Encoding.Unicode.GetString(RequestBin), boundary)
	
	'+ Se busca entre todos los elementos que recibe la página, el que corresponde a la imagen
	Dim UploadControl As Scripting.Dictionary
	Do Until (boundaryPos = InStr(System.Text.Encoding.Unicode.GetString(RequestBin), boundary & getByteString("--")))
		
		'+ Variable para el manejo del diccionario del objeto
		UploadControl = New Scripting.Dictionary
		
		'Principio de la Cadena
		Dim First_File as integer
		
		'Ajuste al Principio de la Cadena
		Dim AjFirst_File as integer
		
		'Final de la Cadena
		Dim Sec_File as integer
		
		'Ajuste al Final de la Cadena
		Dim AjSec_File as integer
		
		'Ajuste Ubicacion de la cadena Final
		Dim AjFinalSec_File as integer
		
		'+ Se toma el nombre del objeto
		Pos = InStr(boundaryPos, System.Text.Encoding.Unicode.GetString(RequestBin), getByteString("Content-Disposition"))
		Pos = InStr(IIf(Pos<=0,1,Pos), System.Text.Encoding.Unicode.GetString(RequestBin), getByteString("name="))
		PosBeg = Pos + 6
		PosEnd = InStr(CInt(PosBeg), System.Text.Encoding.Unicode.GetString(RequestBin), getByteString(Chr(34)))
	
		Name = RequestBin(boundaryPos) 
		
		PosFile = InStr(boundaryPos, System.Text.Encoding.Unicode.GetString(RequestBin), getByteString("filename="))
		PosBound = InStr(IIf(PosEnd<=0,1,PosEnd), System.Text.Encoding.Unicode.GetString(RequestBin), boundary)
		
		'+ Se verifica si el objeto corresponde a un <INPUT TYPE=FILE id=FILE1 name=FILE1>
		If PosFile <> 0 And (PosFile < PosBound) Then
	        	insUpLoadFile = True
			'get the boundry indicator
			strBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
			lngBoundryPos = InStr(1, strBoundry, "boundary=") + 8
			strBoundry = "--" & Right(strBoundry, Len(strBoundry) - lngBoundryPos)
			'Get first file boundry positions.
			lngCurrentBegin = InStr(1, strData, strBoundry)
			lngCurrentEnd = Len(strBoundry) 
			
			'Get the data between current boundry and remove it from the whole.
			strData = Mid(strData, lngCurrentBegin + 1, lngCurrentEnd - lngCurrentBegin)
			
			strDataWhole = Replace(strDataWhole, strData, "")
			
			'Create the file.
            
			tmpLng = InStr(1, FileName, "\")
			Do While tmpLng > 0
				PrevPos = tmpLng
				tmpLng = InStr(PrevPos + 1, FileName, "\")
			Loop 
			FileName = Right(FileName, Len(FileName) - PrevPos)
			If FileName = String.Empty Then
				insUpLoadFile = False
			End If
			'+ Se añade el nombre al diccionario del objeto
			UploadControl.Add("FileName", FileName)
			Pos = InStr(PosEnd, System.Text.Encoding.Unicode.GetString(RequestBin), getByteString("Content-Type:"))
				insUpLoadFile = True
			'get the boundry indicator
			strBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
			lngBoundryPos = InStr(1, strBoundry, "boundary=") + 8
			strBoundry = "--" & Right(strBoundry, Len(strBoundry) - lngBoundryPos)
			'Get first file boundry positions.
			lngCurrentBegin = InStr(1, strData, strBoundry)
			lngCurrentEnd = Len(strBoundry) 
			
			'Get the data between current boundry and remove it from the whole.
			strData = Mid(strData, lngCurrentBegin + 1, lngCurrentEnd - lngCurrentBegin)
			
			strDataWhole = Replace(strDataWhole, strData, "")
			
			'+ Se toma el tipo, nombre y contenido del archivo
			PosBeg = 1
			PosEnd = ByteCount

            'BUSCO EL PRINCIPIO DE LA CADENA CON LA RUTA DEL ARCHIVO
			First_File = instr(1,strDataWhole.ToString ,"filename")
			AjFirst_File = First_File + 10  
			'BUSCO EL PRINCIPIO DE LA CADENA CON LA RUTA DEL ARCHIVO
			Sec_File = instr(1,strDataWhole.ToString ,".rtf")
			If Sec_File <= 0 then    
			    FileName = ""
			Else
			    AjSec_File = Sec_File + 4
			    AjFinalSec_File = AjSec_File - AjFirst_File
			    FileName = Mid(strDataWhole,AjFirst_File,AjFinalSec_File)
			End If
			
			'Create the file.
			tmpLng = InStr(1, FileName, "\")
			Do While tmpLng > 0
				PrevPos = tmpLng
				tmpLng = InStr(PrevPos + 1, FileName, "\")
			Loop 
			FileName = Right(FileName, Len(FileName) - PrevPos)
			If FileName = String.Empty Then
				insUpLoadFile = False
			End If
			'+ Se añade el nombre al diccionario del objeto
			UploadControl.Add("FileName", FileName)
			Pos = InStr(PosEnd, System.Text.Encoding.Unicode.GetString(RequestBin), getByteString("Content-Type:"))
			PosBeg = Pos + 14
			PosEnd = InStr(CInt(PosBeg), System.Text.Encoding.Unicode.GetString(RequestBin), getByteString(Chr(13)))
			
			'+ Se añade el tipo al diccionario del objeto
			ContentType = getString(Mid(System.Text.Encoding.Unicode.GetString(RequestBin),  1, IIF(PosEnd - PosBeg<=0,1,PosEnd - PosBeg)))
			UploadControl.Add("ContentType", ContentType)
			
			'+ Se toma contenido del archivo
			PosBeg = PosEnd + 4
			PosEnd = InStr(CInt(PosBeg), System.Text.Encoding.Unicode.GetString(RequestBin), boundary) - 2
			Value = Mid(System.Text.Encoding.Unicode.GetString(RequestBin), 1, IIF(PosEnd - PosBeg<=0,1,PosEnd - PosBeg))
			
			lngCt = InStr(1, strData, "Content-Type:")
			
			If lngCt > 0 Then
				lngBeginPos = InStr(lngCt, strData, Chr(13) & Chr(10)) + 4
			Else
				lngBeginPos = lngEndFileName
			End If
			
	    Else		
			insUpLoadFile = True
			'get the boundry indicator
			strBoundry = Request.ServerVariables.Item("HTTP_CONTENT_TYPE")
			lngBoundryPos = InStr(1, strBoundry, "boundary=") + 8
			strBoundry = "--" & Right(strBoundry, Len(strBoundry) - lngBoundryPos)
			'Get first file boundry positions.
			lngCurrentBegin = InStr(1, strData, strBoundry)
			lngCurrentEnd = Len(strBoundry) 
			
			'Get the data between current boundry and remove it from the whole.
			strData = Mid(strData, lngCurrentBegin + 1, lngCurrentEnd - lngCurrentBegin)
			
			strDataWhole = Replace(strDataWhole, strData, "")
			
			'+ Se toma el tipo, nombre y contenido del archivo
			PosBeg = 1
			PosEnd = ByteCount

			'BUSCO EL PRINCIPIO DE LA CADENA CON LA RUTA DEL ARCHIVO
			First_File = instr(1,strDataWhole.ToString ,"filename")
			AjFirst_File = First_File + 10  
			'BUSCO EL PRINCIPIO DE LA CADENA CON LA RUTA DEL ARCHIVO
			Sec_File = instr(1,strDataWhole.ToString ,".rtf")
			If Sec_File <= 0 then    
			    FileName = ""
			Else
			    AjSec_File = Sec_File + 4
			    AjFinalSec_File = AjSec_File - AjFirst_File
			    FileName = Mid(strDataWhole,AjFirst_File,AjFinalSec_File)
			End If
			
			'BUSCO EL PRINCIPIO DE EL CONTENIDO DE LA CARTA
			First_File = instr(1,strDataWhole.ToString ,"{\")
			AjFirst_File = First_File  
                
            nThemedata = strDataWhole.LastIndexOf("\par }{\*\themedata")
                
            IF nThemedata > 0 
                Sec_File = nThemedata
                strDataWhole = strDataWhole.Insert(Sec_File+5,"}")
            Else
                Sec_File = strDataWhole.LastIndexOf("\par }}")
            End If
                
            AjSec_File = Sec_File + 7
                
			AjFinalSec_File = (AjSec_File - AjFirst_File) + 1
			
			If Sec_File <= 0 then    
			    strFileData = ""
			Else
                strFileData = Mid(strDataWhole,AjFirst_File,AjFinalSec_File)
			End If

			'Create the file.
			tmpLng = InStr(1, FileName, "\")
			Do While tmpLng > 0
				PrevPos = tmpLng
				tmpLng = InStr(PrevPos + 1, FileName, "\")
			Loop 
			FileName = Right(FileName, Len(FileName) - PrevPos)
			If FileName = String.Empty Then
				insUpLoadFile = False
			End If
			'+ Se añade el nombre al diccionario del objeto
			UploadControl.Add("FileName", FileName)
			Pos = InStr(PosEnd, System.Text.Encoding.Unicode.GetString(RequestBin), getByteString("Content-Type:"))
			PosBeg = Pos + 14
			PosEnd = InStr(CInt(PosBeg), System.Text.Encoding.Unicode.GetString(RequestBin), getByteString(Chr(13)))
			
			'+ Se añade el tipo al diccionario del objeto
			ContentType = getString(Mid(System.Text.Encoding.Unicode.GetString(RequestBin),  1, IIF(PosEnd - PosBeg<=0,1,PosEnd - PosBeg)))
			UploadControl.Add("ContentType", ContentType)
			
			'+ Se toma contenido del archivo
			PosBeg = PosEnd + 4
			PosEnd = InStr(CInt(PosBeg), System.Text.Encoding.Unicode.GetString(RequestBin), boundary) - 2
			Value = Mid(System.Text.Encoding.Unicode.GetString(RequestBin), 1, IIF(PosEnd - PosBeg<=0,1,PosEnd - PosBeg))
			
			lngCt = InStr(1, strData, "Content-Type:")
			
			If lngCt > 0 Then
				lngBeginPos = InStr(lngCt, strData, Chr(13) & Chr(10)) + 4
			Else
				lngBeginPos = lngEndFileName
			End If
			
			'+ En caso de que se haya seleccionado algún archivo.
			If insUpLoadFile Then
				mstrFileContent = strFileData
			End If
		End If
		'+ Se añade el contenido al diccionario del objeto
		UploadControl.Add("Value", Value)
		
		'+ Se añade el objeto al diccionario principal de la página
		UploadRequest.Add(Name, UploadControl)
		
		'+ Se busca el siguiente objeto
		boundaryPos = InStr(boundaryPos + Len(boundary), System.Text.Encoding.Unicode.GetString(RequestBin), boundary)
	Loop 
	RST = Nothing
	UploadControl = Nothing
End Function

'% getString: Conversión de los datos de Byte a String
'--------------------------------------------------------------------------------------------
Function getString(ByRef StringBin As String) As String
	'--------------------------------------------------------------------------------------------
	Dim intCount As Integer
	getString = ""
	'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1040"'
	For intCount = 1 To Len(StringBin)
		'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1040"'
		'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1040"'
		getString = getString & Chr(Asc(Mid(StringBin, intCount, 1)))
	Next 
End Function

'**% AfterPost: This procedure performs processes after the page is posted.
'% AfterPost: Este procedimiento ejecuta procesos después que la página es posteada.
'------------------------------------------------------------------------------------------------
Private Function AfterPost() As String
	'------------------------------------------------------------------------------------------------
	Dim mobjAfterPost As eFunctions.AfterProcess
	Dim sessitem As String
	Dim strSessionVariables As String
	Dim strFormVariables As String
	Dim FormItems As Object
	Dim objArray As String
	
	mobjAfterPost = New eFunctions.AfterProcess
	
	strSessionVariables = ""
	For	Each sessitem In Session.Contents
		If Not IsNothing(Session.Contents.Item(sessitem)) Then
			strSessionVariables = strSessionVariables & (sessitem & "=Session object cannot be displayed.&")
		Else
			If IsArray(Session.Contents.Item(sessitem)) Then
				For	Each objArray In Session.Contents.Item(sessitem)
					strSessionVariables = strSessionVariables & "&" & Session.Contents(sessitem) & "(" & sessitem & "):" & Session.Contents.Item(sessitem)(objArray)
				Next objArray
			Else
				strSessionVariables = strSessionVariables & (sessitem & "=" & Session.Contents.Item(sessitem) & "&")
			End If
		End If
	Next sessitem
	
	If Not IsNothing(Request.Form) Then
		For	Each FormItems In Request.Form
			If IsArray(Request.Form.Item(FormItems)) Then
				If Not IsNothing(Request.Form.Item(FormItems)) Then
					For	Each objArray In Request.Form.GetValues(FormItems)
						strFormVariables = strFormVariables & "&" & Request.Form.Item(FormItems) & "(" & FormItems & "):" & Request.Form.GetValues(FormItems).GetValue(objArray - 1)
					Next objArray
				End If
			Else
				strFormVariables = strFormVariables & (FormItems & "=" & Request.Form.Item(FormItems) & "&")
			End If
		Next FormItems
	End If
	
	AfterPost = mobjAfterPost.AfterPost(strFormVariables, Request.Params.Get("Query_String"), strSessionVariables)
	mobjAfterPost = Nothing
End Function

'**% insOpenDocument: 
'% insOpenDocument: Realiza la impresión de las cartas pendientes de impresión
'----------------------------------------------------------------------------------------------------------------------------------------
Function insOpenDocument(nLettRequest,sClient,nIndex)
'----------------------------------------------------------------------------------------------------------------------------------------
    Dim lobjLetter As eLetter.LettAccuse
    Dim strContent As String

    strContent = ""
    
    lobjLetter = New eLetter.LettAccuse
    
    With lobjLetter
        Response.Write("<script>" & vbcrlf)
        Response.Write("var mstrFileName;")
        Response.Write("var clsFileSystem;")
        Response.Write("var clsFile;")
        Response.Write("var clsWorkApplication;")
      
        Response.Write("clsFileSystem = new ActiveXObject(""Scripting.FileSystemObject"");")
        Response.Write("mstrFileName = ""C:\\Model of correspondence"" + ""\\"" + clsFileSystem.GetTempName() + "".rtf"";")
            
        Response.Write("</" & "script>")      
        Response.Write("<BR>")
        
        If .Find(nLettRequest, sClient) Then
            strContent = ""
            strContent = .tLetter
			Response.Write(Replace(mobjValues.TextAreaControl("tctLetter" & CStr(nIndex), 2, 2, strContent),"&nbsp;","&#032;"))
            Response.Write("<script>" & vbcrlf)
		Else
            Response.Write("<script>")
        End If

		Response.Write("var arrFields")
		
        Response.Write("clsFileSystem.CreateTextFile(mstrFileName,true);")
        Response.Write("clsFile = clsFileSystem.OpenTextFile(mstrFileName, 2, true);")
        
        Response.Write("arrFields = document.getElementsByName('tctLetter' + '" & CStr(nIndex) & "')")
        
        Response.Write("clsFile.write(arrFields[0].value);")
        Response.Write("clsFile.close();")
        Response.Write("clsWorkApplication = new ActiveXObject(""Word.Application"");")
        Response.Write("clsWorkApplication.Documents.open(mstrFileName);")
        Response.Write("clsWorkApplication.visible = true;")
        Response.Write("clsWorkApplication.activate();")
        Response.Write("clsWorkApplication.Application.PrintOut();")
		Response.Write("clsWorkApplication.ActiveDocument.Close(0);")
		Response.Write("clsWorkApplication.Quit();")       
   
        Response.Write("</" & "script>")
    End With
    lobjLetter = Nothing
End Function



</script>

<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("valLetter")


mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "valLetter"
mstrCommand = "sModule=Letter&sProject=Letter&sCodisplReload=" & Request.QueryString.Item("sCodispl")
mblnPopup = Request.QueryString.Item("WindowType") = "PopUp"
mblnCloseErrors = (Request.QueryString.Item("sCodisplReload") <> String.Empty)
mstrOpener = String.Empty
mstrSubFrame = String.Empty


%>
<html>
<head>
    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
    <%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>

    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>

    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
    <%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>

    <script language="JavaScript" src="/VTimeNet/Scripts/Constantes.js"></script>

    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>

    <script>
//**-Objetive: This line keep the source safe version
//-Objetivo: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------ 
    document.VssVersion="$$Revision: 2 $|$$Date: 8/18/06 2:27p $$Author: Gyerena $" 
//------------------------------------------------------------------------------------------ 
    </script>

</head>
<body>

    <script> 

//---------------------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------------------
    self.history.go(-1)}

//---------------------------------------------------------------------------------------------------    
function NewLocation(Source,Codisp){
//---------------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}
    </script>

    <%
If Request.QueryString.Item("sCodispl") = "LT001" Or (Request.QueryString.Item("sCodispl") = "LT004" And Request.QueryString.Item("nZone") = "2") Then
	If CDbl(Request.QueryString.Item("nAction")) <> 392 Then
		'insUpLoadFile "c:\inetPub\"
		lstrPath = Application("UpLoadFile")
        'lstrPath= "C:\\Users\\Developer\\Desktop\\Doc1.docx"
        'Response.Write("<SCRIPT>alert('" & lstrPath & "')</SCRIPT>")
		insUpLoadFile(lstrPath)
		'On Error Resume Next
		
		If Not Request.Form.Item("chkCtroLettInd") Is Nothing Then
			If Err.Number = 0 Then
				mstrCtroLettInd = "1"
			Else
				err.Clear()
				mstrCtroLettInd = "2"
			End If
		End If
		
		If Not Request.Form.Item("chksDelivInvalidind") Is Nothing Then
			If Err.Number = 0 Then
				mstrDelivInvalid = "1"
			Else
				err.Clear()
				mstrDelivInvalid = "2"
			End If
		End If
	End If
End If

'**+ In case the transactions are without heading the variable mStrSubFrame must be with "_K"
'+ En caso de que la transacciones sea sin encabezado colocar el "_K"

If Request.QueryString.Item("nWindowTy") = "3" Or Request.QueryString.Item("nWindowTy") = "4" Or Request.QueryString.Item("nWindowTy") = "5" Then
	mstrSubFrame = "_K"
End If


'**+ In case that the fields of the page were not validate yet
'+ Si no se han validado los campos de la página
If CDbl(Request.QueryString.Item("nAction")) <> 392 Then
	If Not mblnCloseErrors Then
		mstrErrors = insValLetter
		
		Session("sErrorTable") = mstrErrors
		If Request.QueryString.Item("sCodispl") = "LT001" Or (Request.QueryString.Item("sCodispl") = "LT004" And Request.QueryString.Item("nZone") = "2") Then
			Session("sForm") = "FIELDS=BINARYREAD"
		Else
			Session("sForm") = Request.Form.ToString
		End If
	Else
		Session("sErrorTable") = String.Empty
		Session("sForm") = String.Empty
	End If
'Else
	'insFinish()
End If

If mstrErrors > String.Empty Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LetterError"", 660, 330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostLetter() Then
		With Request
			mstrScript = "<SCRIPT>"
			
			If mblnCloseErrors Then
				mstrScript = mstrScript & "closeWinErrors();"
			End If
			
			If mblnPopup Then
				If mblnCloseErrors Then
					mstrOpener = "top.opener.top.opener."
				Else
					mstrOpener = "opener."
				End If
			Else
				If mblnCloseErrors Then
					mstrOpener = "opener."
				End If
			End If
			
			If mblnPopup Then
				If Request.QueryString.Item("sCodispl") = "LT001" Or (Request.QueryString.Item("sCodispl") = "LT004" And Request.QueryString.Item("nZone") = "2") Then
					On Error Resume Next
					sReload = Request.Form.Item("chkContinue")
					If Request.QueryString.Item("sCodispl") = "LT001" Then
						Response.Write("<SCRIPT>top.opener.document.location.href='LT001_K.aspx?Reload=" & sReload & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & "';</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.opener.document.location.href='LT004.aspx?Reload=" & sReload & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & "';</SCRIPT>")
					End If
				Else
					If .QueryString.Item("sCodispl") = "GE101" Then
						mstrScript = mstrScript & "opener.top.close();"
					Else
						If mblnCloseErrors Then
							mstrScript = mstrScript & "top.opener."
						End If
						If .QueryString.Item("sCodispl") = "LT970" Then
                            mstrScript = mstrScript & "top.opener.document.location.href='" & .QueryString.Item("sCodispl") & "_k.aspx?Reload=" & .Form.Item("chkContinue") & "&ReloadAction=" & .QueryString.Item("Action") & "&ReloadIndex=" & .QueryString.Item("ReloadIndex") & "&nMainAction=" & .QueryString.Item("nMainAction") & "&sWindowDescript=" & .QueryString.Item("sWindowDescript") & "&sCodispl=" & .QueryString.Item("sCodispl") & "&nWindowTy=" & .QueryString.Item("nWindowTy") & mstrQueryString & "';"
					    Else
						    mstrScript = mstrScript & "top.opener.document.location.href='" & .QueryString.Item("sCodispl") & mstrSubFrame & ".aspx?Reload=" & .Form.Item("chkContinue") & "&ReloadAction=" & .QueryString.Item("Action") & "&ReloadIndex=" & .QueryString.Item("ReloadIndex") & "&nMainAction=" & .QueryString.Item("nMainAction") & "&sWindowDescript=" & .QueryString.Item("sWindowDescript") & "&sCodispl=" & .QueryString.Item("sCodispl") & "&nWindowTy=" & .QueryString.Item("nWindowTy") & mstrQueryString & "';"
					    End If    
				    End If
				End If
			Else
				If .QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					mstrScript = "<SCRIPT>" & "insReloadTop(false);"
				Else
					mstrScript = mstrScript & "top.fraFolder.document.location.href='" & .QueryString.Item("sCodispl") & mstrSubFrame & ".aspx?Reload=" & .Form.Item("chkContinue") & "&ReloadAction=" & .QueryString.Item("Action") & "&ReloadIndex=" & .QueryString.Item("ReloadIndex") & "&nMainAction=" & .QueryString.Item("nMainAction") & "&sWindowDescript=" & .QueryString.Item("sWindowDescript") & "&sCodispl=" & .QueryString.Item("sCodispl") & "&nWindowTy=" & .QueryString.Item("nWindowTy") & mstrQueryString & "';"
				End If
			End If
			mstrScript = mstrScript & "</SCRIPT>"
			Response.Write(mstrScript)
		End With
		Response.Write(AfterPost)
	Else
		Response.Write("<SCRIPT>alert('Error en el POST')</SCRIPT>")
	End If
End If

mobjLetter = Nothing
mobjValues = Nothing
mclsLetters_as = Nothing

Call mobjNetFrameWork.FinishPage("valLetters")
mobjNetFrameWork = Nothing
    %>
</body>
</html>


