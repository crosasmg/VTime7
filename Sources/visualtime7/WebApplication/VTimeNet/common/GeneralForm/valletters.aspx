<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eLetter" %>
<%@ Import Namespace="ADODB" %>

<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 13/05/2003 10:35:24 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjLetter As eLetter.LettRequest
Dim mobjValues As eFunctions.Values
Private mstrErrors As String
Private UploadRequest As Scripting.Dictionary
Private mstrFileContent As String
Private mstrFilename As String
Private mstrData As String
Private mstrCodispII As String


'+ Se declara la variable para almacenar el String en donde se definen los controles HIDDEN
'+ de la página que la invoca.

Dim mstrCommand As String

Dim lstrRecord As String
Dim lstrPath As Object
Dim mstrProject As String
Dim mstrSubProject As String
Dim mblnUpdContent As Boolean

'- Variable definida para guardar rutas	
Dim mstrPath As String

'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValLetter() As String
	Dim eIniVal As Object
        Dim eEndVal As Integer
        Dim lclsClient As eClient.Client
	'Dim insCommonFunction As Object
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:40:25 p.m.
	'Call insCommonFunction("valletters", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	mobjLetter = New eLetter.LettRequest
	Select Case Request.QueryString.Item("sCodispl")
            Case "SCA801", "SCA802", "SCA803"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    With mobjValues
                        'insValLetter = mobjLetter.ValSCA008(UploadRequest.Item("tctDescript").Item("Value"), .StringToDate(UploadRequest.Item("tcdExpDate").Item("Value")), mstrData, insCalcSendType("chkCustom", 0, 1), UploadRequest.Item("tctAddress").Item("Value"))
                        insValLetter = mobjLetter.ValSCA008(Request.Form.Item("tctDescript"), .StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tcttDs_text"), insCalcSendType("chkCustom", 0, 1), Request.Form.Item("tctAddress"))
                    End With
                Else
                    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                    insValLetter = mobjLetter.ValSCA008Grid(Left(Request.QueryString.Item("sCodispl"), 6), CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))), getVariable("scertype"), mobjValues.StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("ncasenum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), getVariable("sclient"), Today, Session("nUserCode"))
                    'insValLetter = mobjLetter.ValSCA008Grid("SCA802", 304, "2", 10, 1, 110, 0,  0, 0, 0, 0, Today, 8586)
                End If
            Case "SCA805"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    Session("dEffecdate") = Request.Form.Item("tcdDate")
                    Session("nCurrentQuery") = Request.Form.Item("cbeTypeQuery")
                            Session("nBranch") = ""
                            Session("nProduct") = ""
                            Session("nPolicy") = ""
                            Session("nCertif") = ""
                            Session("sCertype") = ""
                            Session("sClient") = ""
                            Session("nClaim") = ""
                            Session("nReceipt") = ""
                            Session("sCheque") = ""
                            Session("nContrat") = ""
                            Session("nProvider") = ""
                            Session("sLoan") = ""
                            Session("nIntermed") = ""
                            Session("nCompany") = ""
                    Select Case Session("nCurrentQuery")
                        Case 1, 3, 5, 11 'Poliza/Certificado/Solicitud
                            Session("nBranch") = Request.Form.Item("cbeBranch")
                            Session("nProduct") = Request.Form.Item("valProduct")
                            Session("nPolicy") = Request.Form.Item("tcnPolicy")
                            Session("nCertif") = Request.Form.Item("tcnCertif")
                            Session("sCodispl_LT") = "SCA802"
                            If Session("nCurrentQuery") = "11" Then ' Cotización
                                Session("sCertype") = "3"
                            End If
                            If Session("nCurrentQuery") = "1" or Session("nCurrentQuery") = "3" Then 'Póliza
                                Session("sCertype") = "2"
                            End If
                            If Session("nCurrentQuery") = "5" Then 'Solicitud
                                Session("sCertype") = "1"
                            End If

                        Case 4 'Cliente
                            lclsClient = New eClient.Client
                            Session("sClient") = lclsClient.ExpandCode(UCase(Request.Form.Item("valCliename")))
                            lclsClient = Nothing
                            Session("sCodispl_LT") = "SCA801"
                        Case 6 'Siniestro
                            Session("nClaim") = Request.Form.Item("tcnClaim")
                            Session("sCodispl_LT") = "SCA803"
                        Case 7 'Recibo
                            Session("nBranch") = Request.Form.Item("cbeBranch")
                            Session("nProduct") = Request.Form.Item("valProduct")
                            Session("nReceipt") = Request.Form.Item("tcnClaim")
                        Case 8 'Cheque
                            Session("sCheque") = Request.Form.Item("tctCheque")
                        Case 9 'Contrato
                            Session("nContrat") = Request.Form.Item("tcnContr")
                        Case 40 'Proveedeor
                            Session("nProvider") = Request.Form.Item("valProvider")
                        Case 60 'Loan/Lease'
                            Session("sLoan") = Request.Form.Item("tctCheque")
                        Case 77 'Intermediario
                            Session("nIntermed") = Request.Form.Item("valIntermed")
                        Case 13 'Reaseguro -- Compañias
                            Session("nCompany") = Request.Form.Item("cbeCompany")
                        Case 80 'Reaseguro - Prima Cedida
                            Session("nPolicy") = Request.Form.Item("tcnPolicy")
                        Case 81 'Reaseguro - Siniestro Cedida
                            Session("nPolicy") = Request.Form.Item("tcnPolicy")
                        Case 82 'Reaseguro - Distribucion del Capital
                            Session("nPolicy") = Request.Form.Item("tcnPolicy")
                    End Select
                    insValLetter = vbNullString
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        With mobjValues
                            'insValLetter = mobjLetter.ValSCA008(UploadRequest.Item("tctDescript").Item("Value"), .StringToDate(UploadRequest.Item("tcdExpDate").Item("Value")), mstrData, insCalcSendType("chkCustom", 0, 1), UploadRequest.Item("tctAddress").Item("Value"))
                            insValLetter = mobjLetter.ValSCA008(Request.Form.Item("tctDescript"), .StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tcttDs_text"), insCalcSendType("chkCustom", 0, 1), Request.Form.Item("tctAddress"))
						
						
                        End With
                    Else
                        'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                        insValLetter = mobjLetter.ValSCA008Grid(Left(Request.QueryString.Item("sCodispl"), 6), CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))), getVariable("scertype"), mobjValues.StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("ncasenum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), getVariable("sclient"), Today, Session("nUserCode"), mstrCodispII)
                    End If

                End If
            Case Else
                If mstrCodispII = "SCA805" Then
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
                        With mobjValues
                            'insValLetter = mobjLetter.ValSCA008(UploadRequest.Item("tctDescript").Item("Value"), .StringToDate(UploadRequest.Item("tcdExpDate").Item("Value")), mstrData, insCalcSendType("chkCustom", 0, 1), UploadRequest.Item("tctAddress").Item("Value"))
                            insValLetter = mobjLetter.ValSCA008(Request.Form.Item("tctDescript"), .StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tcttDs_text"), insCalcSendType("chkCustom", 0, 1), Request.Form.Item("tctAddress"))
						
						
                        End With
                    Else
                        'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                        insValLetter = mobjLetter.ValSCA008Grid(Left(Request.QueryString.Item("sCodispl"), 6), CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))), getVariable("scertype"), mobjValues.StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(getVariable("ncasenum"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), getVariable("sclient"), Today, Session("nUserCode"), mstrCodispII)
                    End If
                End If
        End Select
	'^^Begin Trace Block 08/09/2005 05:40:25 p.m.
	'Call insCommonFunction("valletters", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function



'% insPostLetter: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostLetter() As Boolean
	Dim eIniPost As Object
	Dim eEndPost As Integer
	'Dim insCommonFunction As Object
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:40:25 p.m.
	'Call insCommonFunction("valletters", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	Dim lintSendType As Double
	
	
	
	lintSendType = 0
	
	lintSendType = insCalcSendType("chkEmail", lintSendType, 1)
	lintSendType = insCalcSendType("chkMail", lintSendType, 2)
	lintSendType = insCalcSendType("chkFax", lintSendType, 4)
	
	
	Select Case Request.QueryString.Item("sCodispl")
		
		Case "SCA801", "SCA802", "SCA803"
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				If mobjLetter Is Nothing Then
					mobjLetter = New eLetter.LettRequest
				End If
				With mobjValues
					
					'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
					insPostLetter = mobjLetter.PostSCA008(Request.QueryString.Item("sCodispl"), .StringToType(Request.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdInteger, eRemoteDB.Constants.intNull), .StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger), 1, Request.Form.Item("tctDescript"), CShort(lintSendType), .StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), cstr(mstrFileContent), Request.Form.Item("tctAddress"), getVariable("sclient"), getVariable("scertype"), .StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncase_num"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), insCalcSendType("chkCustom", 0, 1), .StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), Session("nDeman_type"))
					If InStr(1, Session("sLettRequests"), "," & mobjLetter.nLettRequest & ",") = 0 Then
						Session("sLettRequests") = "," & mobjLetter.nLettRequest & ","
					End If
				End With
			Else
				insPostLetter = True
                End If
            Case "SCA805"
                If CDbl(Request.QueryString.Item("nZone")) = 1 Then
                    insPostLetter = True
                Else
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
					
                        If mobjLetter Is Nothing Then
                            mobjLetter = New eLetter.LettRequest
                        End If
                        With mobjValues
						
                            'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                            'insPostLetter = mobjLetter.PostSCA008(mstrCodispII, .StringToType(UploadRequest.Item("tcnLettRequest").Item("Value"), eFunctions.Values.eTypeData.etdInteger, eRemoteDB.Constants.intNull), .StringToType(UploadRequest.Item("tcnLetterNum").Item("Value"), eFunctions.Values.eTypeData.etdInteger), 1, UploadRequest.Item("tctDescript").Item("Value"), CShort(lintSendType), .StringToDate(UploadRequest.Item("tcdExpDate").Item("Value")), CStr(mstrFileContent), UploadRequest.Item("tctAddress").Item("Value"), getVariable("sclient"), getVariable("scertype"), .StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncase_num"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), Today, .StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), insCalcSendType("chkCustom", 0, 1), .StringToDate(UploadRequest.Item("tcdEffecDate").Item("Value")), Session("nDeman_type"))
                            insPostLetter = mobjLetter.PostSCA008(mstrCodispII, .StringToType(Request.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdInteger, eRemoteDB.Constants.intNull), .StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger), 1, Request.Form.Item("tctDescript"), CShort(lintSendType), .StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tcttDs_text"), Request.Form.Item("tctAddress"), getVariable("sclient"), getVariable("scertype"), .StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncase_num"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), Today, .StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), insCalcSendType("chkCustom", 0, 1), .StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), Session("nDeman_type"))
                            If InStr(1, Session("sLettRequests"), "," & mobjLetter.nLettRequest & ",") = 0 Then
                                Session("sLettRequests") = "," & mobjLetter.nLettRequest & ","
                            End If
                        End With
                    Else
                        insPostLetter = True
                    End If
                    
                End If
            Case Else
                If mstrCodispII = "SCA805" Then
				
                    If Request.QueryString.Item("WindowType") = "PopUp" Then
					
                        If mobjLetter Is Nothing Then
                            mobjLetter = New eLetter.LettRequest
                        End If
                        With mobjValues
						
                            'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                            'insPostLetter = mobjLetter.PostSCA008(mstrCodispII, .StringToType(UploadRequest.Item("tcnLettRequest").Item("Value"), eFunctions.Values.eTypeData.etdInteger, eRemoteDB.Constants.intNull), .StringToType(UploadRequest.Item("tcnLetterNum").Item("Value"), eFunctions.Values.eTypeData.etdInteger), 1, UploadRequest.Item("tctDescript").Item("Value"), CShort(lintSendType), .StringToDate(UploadRequest.Item("tcdExpDate").Item("Value")), CStr(mstrFileContent), UploadRequest.Item("tctAddress").Item("Value"), getVariable("sclient"), getVariable("scertype"), .StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncase_num"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), Today, .StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), insCalcSendType("chkCustom", 0, 1), .StringToDate(UploadRequest.Item("tcdEffecDate").Item("Value")), Session("nDeman_type"))
                            insPostLetter = mobjLetter.PostSCA008(mstrCodispII, .StringToType(Request.Form.Item("tcnLettRequest"), eFunctions.Values.eTypeData.etdInteger, eRemoteDB.Constants.intNull), .StringToType(Request.Form.Item("tcnLetterNum"), eFunctions.Values.eTypeData.etdInteger), 1, Request.Form.Item("tctDescript"), CShort(lintSendType), .StringToType(Request.Form.Item("tcdExpDate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tcttDs_text"), Request.Form.Item("tctAddress"), getVariable("sclient"), getVariable("scertype"), .StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), .StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("ncase_num"), eFunctions.Values.eTypeData.etdLong), .StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), Today, .StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdInteger), insCalcSendType("chkCustom", 0, 1), .StringToType(Request.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), Session("nDeman_type"))
                            If InStr(1, Session("sLettRequests"), "," & mobjLetter.nLettRequest & ",") = 0 Then
                                Session("sLettRequests") = "," & mobjLetter.nLettRequest & ","
                            End If
                        End With
                    Else
                        insPostLetter = True
                    End If
                End If
        End Select
	'^^Begin Trace Block 08/09/2005 05:40:25 p.m.
	'Call insCommonFunction("valletters", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function

'% insGetSource: se arma la dirección general en caso de advertencias
'--------------------------------------------------------------------------------------------
Private Sub insGetSource()
	'--------------------------------------------------------------------------------------------
	'- Variable de los modulos	
	Dim lstrModule As String
	
	'- Variable de los proyectos	
	Dim lstrProject As String
	
	Select Case Request.QueryString.Item("sCodispl")
	    Case "SCA801"
		    mstrProject = "Client"
		    mstrSubProject = "ClientSeq"
		    mstrPath = "/VTimeNet/Client/ClientSeq/"
		    '+Pólizas
	    Case "SCA802"
		    mstrProject = "Policy"
		    mstrSubProject = "PolicySeq"
		    mstrPath = "/VTimeNet/Policy/PolicySeq/"
		    '+Siniestros
	    Case "SCA803"
		    mstrProject = "Claim"
		    mstrSubProject = "ClaimSeq"
		    '+Cobranzas
	    Case "SCA804"
		    mstrProject = "Collection"
		    mstrSubProject = "CollectionSeq"
	    Case Else
		    If mstrCodispII = "SCA805" Then
			    mstrProject = "Common"
			    mstrSubProject = ""
		    End If
	
	End Select
	mstrCommand = "&sModule=" & mstrProject & "&sProject=" & mstrSubProject & "&sCodisplReload=" & Request.QueryString.Item("sCodispl") & "&FieldName=" & Request.QueryString.Item("FieldName") 
End Sub


'% insFinish: Se activa cuando la acción es finalizar
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
'	Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
	
	Dim eIniPost As Object
	'Dim insCommonFunction As Object
	Dim eEndPost As Integer
	'--------------------------------------------------------------------------------------------
	Dim lclsValidate As Object
	'Dim lclsClient_blockhis As eClient.Client_blockHis
	'Dim lclsStatisticType As eNetFrameWork.StatisticType
	'Dim lclsClaim_case As eClaim.Claim_case
	
	insFinish = True
	
	'+ Si no se han validado los campos de la página
	Dim lclsClientWin As eClient.ClientWin
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		Select Case Request.QueryString.Item("sCodispl")
			
			'+ Secuencia de Clientes.   
		    Case "SCA801" ', "SCA802", "SCA803"
				
				lclsClientWin = New eClient.ClientWin
				lclsValidate = New eGeneralForm.GeneralForm
				
				'+ Se verifica que no existan ventanas requeridas para la secuencia
				If lclsClientWin.IsPageRequired(Session("sClient"), CInt(Request.QueryString.Item("nMainAction"))) Then
					mstrErrors = lclsValidate.insValGE101("ClientSeq")
				Else
					'lclsClient_blockhis = New eClient.Client_blockHis
					
					'With Request
					'	Call lclsClient_blockhis.insClientFinish(False, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), "Del", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), Session("sClient"), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(99), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull)
					'End With
					'lclsClient_blockhis = Nothing
					
					'lclsStatisticType = New eNetFrameWork.StatisticType
					'Call lclsStatisticType.valComienzo_Nodo(Session("nUsercode"), 3)
					'lclsStatisticType = Nothing
					'Call insCommonFunction("valclientseq", "BC050", eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "BC")
					'Call insCommonFunction("valclientseq", "BC050", eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "BC")
				End If
				
				lclsClientWin = Nothing
				
			'+ Secuencia de póliza.
			Case "SCA802"

			'+ Secuencia de Siniestro.
			Case "SCA803"
				'lclsValidate = New eClaim.Claim_cases
				
				'+ Se verifica que no existan ventanas requeridas para la secuencia
				'mstrErrors = lclsValidate.insValSI099(Session("nClaim"), Session("nCase_num"), Session("nDeman_type"))
		End Select
	End If
	
	If mstrErrors > vbNullString Then
		insFinish = False
		Session("sErrorTable") = mstrErrors
		Session("sForm") = Request.Form.ToString
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""GeneralFormError"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</" & "Script>")
		End With
	Else
		If Session("bQuery") = False Then
			Select Case Request.QueryString.Item("sCodispl")
				
			    '+ Secuencia de póliza.
			    Case "SCA802"

				'+ Secuencia de Siniestro.
				Case "SCA803"
					
					'lclsClaim_case = New eClaim.Claim_case
					'With lclsClaim_case
					'	If .Find(Session("nClaim"), Session("nCase_num"), Session("nDeman_type")) Then
					'		If .sStaReserve = "6" Then
					'			.sStaReserve = "2"
					'		End If
					'		insFinish = .UpdatesStareserve(.nClaim, .nDeman_type, .nCase_num, .sStaReserve)
					'	End If
					'End With
					'lclsClaim_case = Nothing
			End Select
		End If
	End If
	lclsValidate = Nothing
End Function

'% getByteString: Conversión de los datos de String a Byte
'--------------------------------------------------------------------------------------------
Function getByteString(ByRef StringStr As String) As String
	'--------------------------------------------------------------------------------------------
	Dim i As Integer
	'UPGRADE_NOTE: char was upgraded to char_Renamed. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
	Dim char_Renamed As String
	getByteString = ""
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
	For intCount = 1 To Len(StringBin)
		getString = getString & Chr(Asc(Mid(StringBin, intCount, 1)))
	Next 
End Function

'% getVariables
'---------------------------------------------------------------------
Private Function getVariable(ByRef svariable As String) As Object
	'---------------------------------------------------------------------
	Select Case Trim(UCase(Request.QueryString.Item("sCodispl")))
		'+Clientes
		Case "SCA801"
			If svariable = "sclient" Then
				getVariable = Session(svariable)
			End If
			'+Pólizas
		Case "SCA802"
			If svariable = "scertype" Or svariable = "nbranch" Or svariable = "nproduct" Or svariable = "npolicy" Or svariable = "ncertif" Then
				getVariable = Session(svariable)
			End If
			'+Siniestros
		Case "SCA803"
			If svariable = "nclaim" Or svariable = "ncase_num" Or svariable = "ndeman_type" Or svariable = "nbranch" Then
				getVariable = Session(svariable)
			End If
			'+Cobranzas
		Case "SCA804"
			If svariable = "nbordereaux" Then
				getVariable = Session(svariable)
			End If
		Case Else
			getVariable = Session(svariable)
	End Select
End Function

'--------------------------------------------------------------------------
Private Function insCalcSendType(ByRef sObjectName As String, ByRef nPrevVal As Double, ByRef nValuetoAdd As Byte) As Double
	'--------------------------------------------------------------------------
	On Error Resume Next
	'If Not UploadRequest(sObjectName) Is Nothing Then
	If Not Request.Form.Item(sObjectName) Is Nothing Then
		If Err.Number = 0 Then
			insCalcSendType = nPrevVal + nValuetoAdd
		Else
			err.Clear()
			insCalcSendType = nPrevVal
		End If
	End If
End Function
'**% AfterPost: This procedure performs processes after the page is posted.
'% AfterPost: Este procedimiento ejecuta procesos después que la página es posteada.
'------------------------------------------------------------------------------------------------
Private Function AfterPost() As String
	'------------------------------------------------------------------------------------------------
	Dim mobjAfterPost As eFunctions.AfterProcess
	Dim sessitem As Object
	Dim strSessionVariables As String
	Dim strFormVariables As String
	Dim FormItems As Object
	Dim objArray As String
	
	mobjAfterPost = New eFunctions.AfterProcess
	
	
	strSessionVariables = ""
	For	Each sessitem In Session.Contents
		If Not IsNothing(Session.Contents.Item(CStr(sessitem))) Then
			strSessionVariables = strSessionVariables & (sessitem & "=Session object cannot be displayed.&")
		Else
			If IsArray(Session.Contents.Item(CStr(sessitem))) Then
				For	Each objArray In Session.Contents.Item(CStr(sessitem))
					strSessionVariables = strSessionVariables & "&" & Session.Contents(sessitem) & "(" & sessitem & "):" & Session.Contents.Item(CStr(sessitem))(objArray)
				Next objArray
			Else
				strSessionVariables = strSessionVariables & (sessitem & "=" & Session.Contents.Item(CStr(sessitem)) & "&")
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

</script>

<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("ValLetters")
Call insGetSource()
'mstrCommand = "&sModule=Letter&sProject=Letter&sCodisplReload=" & Request.QueryString("sCodispl")
'mstrCommand = "&sModule=Letter&sProject=Letter&sCodisplReload=" & Request.QueryString.Item("sCodispl")
'mstrCommand = "&sModule=Common&sProject=Letters&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 13/05/2003 10:35:24 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ValLetters"


mblnUpdContent = Request.QueryString.Item("WindowType") <> "PopUp"
mstrProject = vbNullString
mstrSubProject = vbNullString

%>
<html>
<head>

    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>

    <script language="JavaScript" src="/VTimeNet/Scripts/Constantes.js"></script>

    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
    <!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
</head>
<body>
    <%

If Not String.IsNullOrEmpty(Request.QueryString.Item("sCodispII")) Then
	mstrCodispII = Left(Request.QueryString.Item("sCodispII"), 6)
	'mstrCommand = "&sModule=Letter&sProject=Letter&sCodisplReload=" & mstrCodispII
Else
	mstrCodispII = ""
End If

If Not Session("bQuery") Or Request.QueryString.Item("nZone") = "1" Then
	
	If (Request.QueryString.Item("sCodispl") = "SCA801" Or Request.QueryString.Item("sCodispl") = "SCA802" Or Request.QueryString.Item("sCodispl") = "SCA803" Or mstrCodispII = "SCA805") And Request.QueryString.Item("WindowType") = "PopUp" Then
		lstrPath = Application("UpLoadFile")
	    insUpLoadFile(lstrPath)
	End If

	'+ Si no se han validado los campos de la página
	If Request.QueryString.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValLetter
		Session("sErrorTable") = mstrErrors
		If (Request.QueryString.Item("sCodispl") = "SCA801" Or Request.QueryString.Item("sCodispl") = "SCA802" Or Request.QueryString.Item("sCodispl") = "SCA803" Or mstrCodispII = "SCA805") And Request.QueryString.Item("WindowType") = "PopUp" Then
			Session("sForm") = "FIELDS=BINARYREAD"
		Else
			Session("sForm") = Request.Form.ToString
		End If
	Else
		Session("sErrorTable") = vbNullString
		Session("sForm") = vbNullString
	End If
End If

If mstrErrors > vbNullString Then
	
	Session("sErrorTable") = mstrErrors
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""LetterErrors"",660,330);")
		If mstrCodispII = "SCA805" Then
			.Write("top.history.go(-1)")
			mstrErrors = vbNullString
		Else
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		End If
		.Write("</SCRIPT>")
	End With
Else
	
	If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insPostLetter() Then
		
			If Request.QueryString.Item("nFastRecord") = "1" Then
				lstrRecord = Request.QueryString.Item("nFastRecord")
			Else
				lstrRecord = "2"
			End If
			
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				
				If Request.QueryString.Item("nZone") = "1" Or Request.QueryString.Item("sCodispl") = "LT001" Then
					Response.Write("<SCRIPT>top.opener.top.fraHeader.document.location.reload();window.close();</SCRIPT>")
				ElseIf mstrCodispII = "SCA805" Then 
					Response.Write("<SCRIPT>top.opener.top.fraFolder.document.location.href=top.opener.top.fraFolder.document.location.href.replace(/&sGoTo.*/,'')+ (top.opener.top.fraFolder.document.location.href.indexOf('?')==-1?'?':'&') + 'sGoToNext=NO';top.opener.top.fraFolder.location.reload();window.close();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>top.opener.top.fraSequence.document.location.href=top.opener.top.fraSequence.document.location.href.replace(/&sGoTo.*/,'')+ (top.opener.top.fraSequence.document.location.href.indexOf('?')==-1?'?':'&') + 'sGoToNext=NO';top.opener.top.fraFolder.location.reload();window.close();</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
				Else
				
					If Request.QueryString.Item("sCodispl") <> "SCA802" And Request.QueryString.Item("sCodispl") <> "SCA899" Then
					    Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&nFastRecord=" & lstrRecord & "';</SCRIPT>")
					Else
					    Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&nFastRecord=" & lstrRecord & "';</SCRIPT>")
					End If    
					
					If Request.QueryString.Item("sCodisplReload") <> vbNullString Then					
					    Response.Write("<SCRIPT>top.window.close()</SCRIPT>")
					End If
				End If
			End If
			'Response.Write(AfterPost)
		End If
	Else
		
		If mstrErrors = vbNullString And Request.QueryString.Item("sCodispII") = "SCA805" Then
			If CDbl(Request.QueryString.Item("nact")) = 1 Then
				Response.Write("<SCRIPT>top.window.close();top.opener.top.window.close()</SCRIPT>")
			Else
				Response.Write("<SCRIPT>top.window.close()</SCRIPT>")
			End If
		Else
			Response.Write("<SCRIPT>window.close();</SCRIPT>")
		    If insFinish() Then
			    'Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	            Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
		    End If
		End If
	End If
End If

mobjValues = Nothing
mobjLetter = Nothing
    %>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 13/05/2003 10:35:24 a.m.
Call mobjNetFrameWork.FinishPage("ValLetters")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





