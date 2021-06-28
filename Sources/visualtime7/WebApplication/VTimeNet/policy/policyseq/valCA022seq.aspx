 <%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ADODB" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>

<script language="VB" runat="Server">
Dim mobjPolicySeq As Object
Dim mstrErrors As String
Dim mstrLocationCA001 As String
Dim mobjValues As eFunctions.Values
Dim lclsPolicy As Object
Dim mstrScript As Object
Dim lintCurrency As Object
Dim llngPayfreq As Object

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable para el manejo del QueryString  
Dim mstrQueryString As String

Dim mblnCreateInsured As Object

'-Variable para indicar si ya se ejecutaron las validaciones
Dim mblnReload As Boolean

Dim mstrFileName As Object
Dim mstrUseFile As Object
Dim mstrDefault As Object

'- Objeto para localización de archivos

Dim mobjUploadRequest As Dictionary(Of String, String)

Dim mobjGeneral As Object
Dim lclsRefresh As Object
Dim binData() As Byte
Dim ScriptObject As FileStream
Dim fileContentLength As Integer
Dim fileContentIndex As Integer
Dim myRequestFile(4) As String
Dim crlf As String = Chr(13) & Chr(10)



'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	Dim lintIntermedia As Object
	Dim lintIntermediaOld As Object
	Dim lstrClient As Object
	Dim lstrClientOld As Object
	Dim lclsPolicy_Win As Object
	Dim lstrError As String=String.Empty
    Dim lstrInsured as String 
	
	'    mobjNetFrameWork.BeginProcess "ValSequence|" & Request.QueryString("sCodispl")
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CA022: Cláusula/descriptivo/condición especial        
		Case "CA022"
'UPGRADE_NOTE: The 'ePolicy.Clause' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
			mobjPolicySeq = new ePolicy.Clause

			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                   binData = Request.BinaryRead(Request.TotalBytes)
                   BuildUploadRequest(binData)
                   

					If Not True Then
						lstrError = "1977"
					Else
						mstrFileName =   Path.GetFileName(myRequestFile(2))
					End If
                    If Not String.IsNullOrEmpty(mstrFileName) Then
                        updFile()
                    End If
					
					mstrUseFile = mobjUploadRequest("hddCheckFile")
					mstrDefault = mobjUploadRequest("hddChkAgree")
                    Try
                        lstrInsured = mobjUploadRequest("valInsured")
                    catch
                    end try
                        
                        Session("mstrDefault") = mobjUploadRequest("hddChkAgree")
                        Session("lstrId") = mobjUploadRequest("hddId")
                        Session("lstrClause") = mobjUploadRequest("valClause")
                        Try
                            Session("lstrInsured") = mobjUploadRequest("valInsured")
                        Catch
                        End Try
                        Try
                            Session("lstrModulec") = mobjUploadRequest("hddModulec")
                        Catch
                        End Try

                        Try
                            Session("lstrCover") = mobjUploadRequest("hddCover")
                        Catch
                        End Try

                        Try
                            Session("lstrCause") = mobjUploadRequest("cbeCause")
                        Catch
                        End Try

                        Session("lstrGroup_insu") = mobjUploadRequest("hddGroup_Insu")
                        Session("lstrhddNoteNum") = mobjUploadRequest("hddNoteNum")

                        Try
                            Session("lstrtcnIniNote") = mobjUploadRequest("tcnIniNote")
                        Catch
                        End Try
						
                    	Try
                            Session("cbeCause") = mobjUploadRequest("cbeCause")
                        Catch
                        End Try
						
                        insvalSequence = mobjPolicySeq.InsValCA022Upd(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(mobjUploadRequest("valClause"), eFunctions.Values.eTypeData.etdDouble), lstrInsured, mobjValues.StringToType(mobjUploadRequest("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mobjUploadRequest("hddCover"), eFunctions.Values.eTypeData.etdDouble), mobjUploadRequest("hddSel"), Session("nTransaction"), .QueryString("Action"), mstrUseFile, mstrFileName, mobjValues.StringToType(Session("cbeCause"), eFunctions.Values.eTypeData.etdDouble) )
                    Else
                        insvalSequence = mobjPolicySeq.InsValCA022(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), .QueryString("Action"))
                    End If
                End With
			mobjPolicySeq = Nothing
			
			'+ CA022A: Cláusulas de la póliza matriz
		Case "CA022A"
'UPGRADE_NOTE: The 'ePolicy.Claus_co_gp' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
			mobjPolicySeq = new ePolicy.Claus_co_gp
			
			If Request.QueryString.Item("Action") = "Add" Then
                binData = Request.BinaryRead(Request.TotalBytes)
                BuildUploadRequest(binData)

				If Not True Then
					lstrError = "1977"
				Else
					mstrFileName =    Path.GetFileName(myRequestFile(2))

				End If

                If Not String.IsNullOrEmpty(mstrFileName) Then
                    updFile()
                End If
				
				mstrUseFile = mobjUploadRequest("hddCheckFile")
				
			End If
			
			insvalSequence = mobjPolicySeq.insValCA022A(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjUploadRequest("hddnClause"), mobjUploadRequest("hddnSelClause"), mobjValues.StringToType(mobjUploadRequest("valGroup"), eFunctions.Values.eTypeData.etdDouble), mstrFileName, mstrUseFile)
			
			mobjPolicySeq = Nothing
			
	End Select
	'mobjNetFrameWork.FinishProcess "ValSequence|" & Request.QueryString("sCodispl")
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lintIntermedia As Object
	Dim lintIntermediaOld As Object
	Dim lstrClient As Object
	Dim lstrClientOld As Object
	Dim lblnPost As Boolean
	Dim lclsPolicy_Win As Object
	Dim lclsErrors As Object
	Dim lobjDocuments As Object
	Dim llngTariff As Object
	
	Dim lstrId As Object
	Dim lstrClause As Object
	Dim lstrInsured As Object
	Dim lstrModulec As Object
	Dim lstrCover As Object
	Dim lstrCause As Short
	Dim lstrGroup_insu As Object
	Dim lstrhddNoteNum As Object
	Dim lstrtcnIniNote As Object
	
	lblnPost = True
	
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ CA022: Cláusula/descriptivo/condición especial        
		Case "CA022"
			With Request
'UPGRADE_NOTE: The 'ePolicy.Clause' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
				mobjPolicySeq = new ePolicy.Clause
				If Request.QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjPolicySeq.InsPostCA022Upd(.QueryString("sCodispl"), .QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("lstrId"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("lstrClause"), eFunctions.Values.eTypeData.etdLong), Session("lstrInsured"), mobjValues.StringToType(Session("lstrModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("lstrCover"), eFunctions.Values.eTypeData.etdLong), Session("lstrCause"), Session("mstrDefault"), mobjValues.StringToType(Session("lstrGroup_insu"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("lstrhddNoteNum"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("lstrtcnIniNote"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToDate(Session("dNulldate")), mstrUseFile, mstrFileName)
					
                    End If
                    mobjPolicySeq = Nothing
                End With
			
			'+ CA022A: Cláusulas de la póliza matriz
		Case "CA022A"
'UPGRADE_NOTE: The 'ePolicy.Claus_co_gp' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
			mobjPolicySeq = new ePolicy.Claus_co_gp
			With Request
				lblnPost = mobjPolicySeq.insPostCA022A(.QueryString("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjUploadRequest("hddnClause"), mobjUploadRequest("hddnSelClause"), mobjUploadRequest("hddNoteNum"), mobjUploadRequest("hddNoteNum_Prod"), mobjValues.StringToType(mobjUploadRequest("valGroup"), eFunctions.Values.eTypeData.etdDouble), mstrUseFile, mstrFileName)
				If lblnPost Then
					'+ Si se efectúa la actualización puntual se recarga la página.
					If CBool(IIf(IsNothing(.Form.Item("hddbPuntual")), False, .Form.Item("hddbPuntual"))) Then
'UPGRADE_NOTE: The 'eFunctions.Errors' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
						lclsErrors = new eFunctions.Errors
						'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.17
						lclsErrors.sSessionID = Session.SessionID
						lclsErrors.nUsercode = Session("nUsercode")
						'~End Body Block VisualTimer Utility
						'+ Se manda un mensaje indicando que ya se actualizaron los datos en la tabla
						Response.Write(lclsErrors.ErrorMessage(Request.QueryString.Item("sCodispl"), 55881,  ,  ,  , True))
						lclsErrors = Nothing
						Response.Write("<SCRIPT>top.frames['fraFolder'].document.location=top.frames['fraFolder'].document.location</" & "Script>")
						lblnPost = False
					End If
				End If
			End With
			mobjPolicySeq = Nothing
	End Select
	
	'+Se ejecutan las ventana automaticas
	'    mobjNetFrameWork.FinishProcess "PostSequence|" & Request.QueryString("sCodispl")
	If lblnPost And Request.QueryString.Item("WindowType") <> "PopUp" Then
		Call insGeneralAuto(Request.QueryString.Item("sCodispl"))
	End If
	lclsPolicy_Win = Nothing
	insPostSequence = lblnPost
End Function

'% insCancel: Esta rutina es activada cuando el usuario cancela la transacción en donde
'%              está trabajando.
'--------------------------------------------------------------------------------------------
Function insCancel() As Boolean
	Dim clngCertifPropRenewal As Object
	Dim clngPolicyPropRenewal As Object
	Dim clngCertifPropAmendent As Object
	Dim clngPolicyPropAmendent As Object
	''Dim eRemoteDB.Constants.intNull As Object
	Dim clngPolicyQuotRenewal As Object
	Dim clngCertifQuotRenewal As Object
	Dim clngPropAmendConvertion As Object
	Dim clngCertifQuotAmendent As Object
	Dim eBranches As Object
	Dim clngPolicyQuotAmendent As Object
	Dim clngDuplPolicy As Object
	Dim clngQuotPropAmendentConvertion As Object
	Dim lstrsCertype As String
	'--------------------------------------------------------------------------------------------
	Dim lclsValues As Object
	Dim lclsErrors As Object
	Dim lclsPolicy As Object
	Dim lclsCertificat As Object
	Dim lclsPolicy_his As Object
	Dim lintString As Integer
	Dim lstrError As String=String.Empty
	Dim lclsPolicy_aux As Object
	Dim llngProponum As Object
	Dim lclsPageRetCA050 As Object
	
	'- Variables para almacenar temporalmente el número de Referencia y Código del proceso    
	Dim llngReference As Byte
	Dim lintCodeProce As Byte
	
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsValues = new eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.55
	lclsValues.sSessionID = Session.SessionID
	lclsValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lclsValues.sCodisplPage = "ValPolicySeq"
'UPGRADE_NOTE: The 'eFunctions.Errors' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsErrors = new eFunctions.Errors
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.43.55
	lclsErrors.sSessionID = Session.SessionID
	lclsErrors.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsPolicy = new ePolicy.Policy
'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsCertificat = new ePolicy.Certificat
	
	insCancel = True
	lclsPageRetCA050 = Session("PageRetCA050")
	
	If lclsPageRetCA050 = "CA001C" Then
		mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
	Else
		mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
	End If
	
	'+ Se realiza el llamado al procedimiento que actualiza el campo UserAmend 
	'+ de Policy o Certificat, según sea el caso
	Call insUpdUserAmend()
	
	'+Se realiza el reverso de la modificación
	If CBool(Trim(CStr(CStr(Session("nTransaction")) <> vbNullString))) Then
		If Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = clngPropAmendConvertion Then
			If Not lclsCertificat.insReverRenModPol(Session("sCertype"), lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 0, lclsValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), 0) Then
				Response.Write(lclsErrors.ErrorMessage("CA001_K", 3616,  ,  ,  , True))
			End If
		End If
		
		'+ Sólamente se efectuará este proceso siempre y cuando la ventana no sea la principal (CA001).
		If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyProposal Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuotation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuotation Or Session("nTransaction") = clngPolicyQuotAmendent Or Session("nTransaction") = clngCertifQuotAmendent Or Session("nTransaction") = clngPolicyPropAmendent Or Session("nTransaction") = clngCertifPropAmendent Or Session("nTransaction") = clngPolicyQuotRenewal Or Session("nTransaction") = clngCertifQuotRenewal Or Session("nTransaction") = clngPolicyPropRenewal Or Session("nTransaction") = clngCertifPropRenewal Or Session("nTransaction") = clngQuotPropAmendentConvertion Then
			If Request.Form.Item("optElim") = "Delete" Then
				With lclsPolicy
					.sCertype = Session("sCertype")
					.nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
					.nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
					.nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
					.nCertif = lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
					
					'+ Se asigna el número de referencia
					If Request.Form.Item("tcnReference") = vbNullString Then
						llngReference = 0
					Else
						llngReference = lclsValues.StringToType(Request.Form.Item("tcnReference"), eFunctions.Values.eTypeData.etdDouble)
					End If
					
					'+ Se asigna el código del proceso
					If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngdeclarations Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Then
						lintCodeProce = 4
					End If
					
					If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngQuotationConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyQuery Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifQuery Or Session("nTransaction") = clngDuplPolicy Then
						lintCodeProce = 6
					End If
					'+ Se busca la propuesta que dio origen a la propuesta si esta no es parte de los datos
					'+ que existen para la transaccion
					llngProponum = Session("nProponum")
					If Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Then
						If lclsCertificat.Find("2", Session("nBranch"), Session("nProduct"), Session("npolicy"), Session("nCertif")) Then
							llngProponum = lclsCertificat.nProponum
						End If
					End If
					'+ Se eliminan los datos de la póliza
					If Session("nTransaction") = clngPolicyQuotAmendent Or Session("nTransaction") = clngCertifQuotAmendent Or Session("nTransaction") = clngPolicyPropAmendent Or Session("nTransaction") = clngCertifPropAmendent Or Session("nTransaction") = clngPolicyQuotRenewal Or Session("nTransaction") = clngCertifQuotRenewal Or Session("nTransaction") = clngPolicyPropRenewal Or Session("nTransaction") = clngCertifPropRenewal Then
'UPGRADE_NOTE: The 'ePolicy.Policy_his' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
						lclsPolicy_his = new ePolicy.Policy_his
						Call lclsPolicy_his.DelRecordType_policy_his("2", Session("nBranch"), Session("nProduct"), Session("nPolicy_old"), Session("nCertif"), 9)
						lclsPolicy_his = Nothing
					End If
					If .DelRecursivePolicy(lintCodeProce, llngReference) Then
						'+ Reversa el estado de la propuesta  
						If Session("nTransaction2") = eCollection.Premium.PolTransac.clngQuotationConvertion Or Session("nTransaction2") = eCollection.Premium.PolTransac.clngProposalConvertion Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Then
							lstrsCertype = "1"
							If Session("nTransaction2") = eCollection.Premium.PolTransac.clngQuotationConvertion Then
								lstrsCertype = "3"
							End If
							If lclsCertificat.Find(lstrsCertype, Session("nBranch"), Session("nProduct"), llngProponum, Session("nCertif")) Then
								lclsCertificat.nstatquota = 1
								lclsCertificat.nPol_quot  = eRemoteDB.Constants.intNull
								Call lclsCertificat.Update()
							End If
'UPGRADE_NOTE: The 'ePolicy.Policy_his' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
							lclsPolicy_his = new ePolicy.Policy_his
							If lclsPolicy_his.DelRecordType_policy_his(lstrsCertype, Session("nBranch"), Session("nProduct"), llngProponum, Session("nCertif"), 21) Then
'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
								lclsPolicy_aux = new ePolicy.Policy
								If lclsPolicy_aux.Find(lstrsCertype, Session("nBranch"), Session("nProduct"), Session("nProponum")) Then
									lclsPolicy_aux.nMov_histor = lclsPolicy_aux.nMov_histor - 1
									lclsPolicy_aux.Add()
								End If
							End If
						End If
						lstrError = lclsErrors.ErrorMessage("CA001_K", 3990,  ,  ,  , True)
						lintString = InStr(1, lstrError, "Err.")
						If lintString > 0 Then
							lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
						End If
						Response.Write(lstrError)
					Else
						Response.Write(lclsErrors.ErrorMessage("CA001_K", 3991,  ,  ,  , True))
					End If
				End With
			Else
				
				'+ Mensaje informativo.
				Select Case Session("nTransaction")
					Case eCollection.Premium.PolTransac.clngPolicyIssue, eCollection.Premium.PolTransac.clngCertifIssue, eCollection.Premium.PolTransac.clngRecuperation
						lstrError = lclsErrors.ErrorMessage("CA001_K", 3968,  ,  ,  , True)
						lintString = InStr(1, lstrError, "Men.")
						If lintString > 0 Then
							lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
						End If
						
						Response.Write(lstrError)
						
					Case eCollection.Premium.PolTransac.clngPolicyQuotation, eCollection.Premium.PolTransac.clngCertifQuotation, clngPolicyQuotAmendent, clngCertifQuotAmendent, clngPolicyQuotRenewal, clngCertifQuotRenewal
						
						lstrError = lclsErrors.ErrorMessage("CA001_K", 3970,  ,  ,  , True)
						lintString = InStr(1, lstrError, "Men.")
						If lintString > 0 Then
							lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
						End If
						
						Response.Write(lstrError)
						
					Case eCollection.Premium.PolTransac.clngPolicyProposal, eCollection.Premium.PolTransac.clngCertifProposal, clngPolicyPropAmendent, clngCertifPropAmendent, clngPolicyPropRenewal, clngCertifPropRenewal
						
						lstrError = lclsErrors.ErrorMessage("CA001_K", 3969,  ,  ,  , True)
						lintString = InStr(1, lstrError, "Men.")
						If lintString > 0 Then
							lstrError = Mid(lstrError, 1, lintString - 1) & Mid(lstrError, lintString + 10, Len(lstrError))
						End If
						
						Response.Write(lstrError)
						
				End Select
			End If
		End If
	End If
	
	lclsErrors = Nothing
	lclsValues = Nothing
	lclsPolicy = Nothing
	lclsCertificat = Nothing
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	Dim batchParAreaProc As Object
	'--------------------------------------------------------------------------------------------
	'if 1=2 then
	Dim lclsValues As Object
	Dim lclsPolicy As Object
	Dim lclsPolicy_amend As Object
	Dim lclsPageRetCA050 As Object
	
	
	
	'-Objeto para transacciones batch	
	Dim lclsBatch_param As Object
	
	lclsPageRetCA050 = Session("PageRetCA050")
	
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsValues = new eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.02
	lclsValues.sSessionID = Session.SessionID
	lclsValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lclsValues.sCodisplPage = "ValPolicySeq"
'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsPolicy = new ePolicy.Certificat
	
	'+ Si existe alguna carpeta que no halla sido carga con información.
	insFinish = True
	
	Select Case Session("nTransaction")
		Case "12", "13", "14", "15", "24", "25", "26", "27", "34"
			
			'+ Si se trata de Fin de Proceso (CA048). Modificación de Póliza individual o certificado.
			If Request.Form.Item("chkAfeccer") <> "1" Then
				
				If lclsPolicy.insExecuteCA048(Request.QueryString.Item("sCodispl"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkPendenstat"), mobjValues.StringToType(Request.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chkAfeccer"), mobjValues.StringToType(Session("nCapital"), eFunctions.Values.eTypeData.etdDouble, True)) Then
					If Request.Form.Item("chkPrint") = "1" Then
						insPrintDocuments()
					End If
					
					insFinish = True
					
					If lclsPageRetCA050 = "CA001C" Then
						mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
					Else
						mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
					End If
					
					
					'+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
					Call insUpdUserAmend()
				Else
					
					insFinish = False
				End If
				
			Else
				
'UPGRADE_NOTE: The 'eSchedule.Batch_param' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
				lclsBatch_param = new eSchedule.Batch_param
				With lclsBatch_param
					.nBatch = 160
					.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
					.Add(batchParAreaProc, .sKey)
					.Add(batchParAreaProc, Session("sCertype"))
					.Add(batchParAreaProc, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble))
					.Add(batchParAreaProc, mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble))
					.Add(batchParAreaProc, mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble))
					.Add(batchParAreaProc, mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
					.Add(batchParAreaProc, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
					.Add(batchParAreaProc, .nUsercode)
					.Add(batchParAreaProc, mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))
					.Add(batchParAreaProc, Request.Form.Item("chkPendenstat"))
					.Add(batchParAreaProc, mobjValues.StringToType(Request.Form.Item("cbeWaitCode"), eFunctions.Values.eTypeData.etdDouble, True))
					.Add(batchParAreaProc, mobjValues.StringToType(Request.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble, True))
					.Add(batchParAreaProc, Request.Form.Item("chkAfeccer"))
					.Add(batchParAreaProc, mobjValues.StringToType(Session("nCapital"), eFunctions.Values.eTypeData.etdDouble, True))
					
					.Save()
				End With
				
				Response.Write("<SCRIPT>alert('Se generó la clave de proceso: " & lclsBatch_param.sKey & "');</" & "Script>")
				
				lclsBatch_param = Nothing
				
				If Request.Form.Item("chkPrint") = "1" Then
					insPrintDocuments()
				End If
				
				insFinish = True
				
				If lclsPageRetCA050 = "CA001C" Then
					mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
				Else
					mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
				End If
				
				
				'+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
				Call insUpdUserAmend()
				
			End If
			
		Case "1", "2", "3", "4", "5", "6", "7", "18", "19", "30", "31", "43"
			'+ Si se trata de Fin de Emisión (CA050)            
			If lclsPolicy.insExecuteCA050(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), Session("nCertif"), CStr(Session("nTransaction")), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), Request.Form.Item("gmtDocument"), Request.Form.Item("gmtDocumentTag"), Request.Form.Item("blnEnabledWaitCode"), mobjValues.StringToType(Request.Form.Item("cboWaitCode"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("pblnDocQuotation"), Request.Form.Item("chkCertif"), mobjValues.StringToType(Session("nCapital"), eFunctions.Values.eTypeData.etdDouble, True)) Then
				
				
				'+ Se ejecuta el CAL001
				If Request.Form.Item("chkPrintNow") = "1" Then
					insPrintDocuments()
				End If
				
				
				insFinish = True
				
				'+ Se muestra la página principal de la secuencia
				If (CStr(Session("sPoliType")) = "2" Or CStr(Session("sPoliType")) = "3") And (CStr(Session("nTransaction")) = "1" Or CStr(Session("nTransaction")) = "2" Or CStr(Session("nTransaction")) = "18" Or CStr(Session("nTransaction")) = "19") Then
					'+ Se invoca la secuencia de póliza con la transaccion de Emision de Certificado
					mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&nTransaction=2&bMenu=1&nOrig_call=1'"
				Else
					If lclsPageRetCA050 = "CA001C" Then
						mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
					Else
						mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
					End If
				End If
				
				'+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
				Call insUpdUserAmend()
			Else
				insFinish = False
			End If
			
			'+Si se trata de consultas de cartera
		Case "8", "9", "10", "11", "44"
			'+ Se muestra la página principal de la secuencia
			If lclsPageRetCA050 = "CA001C" Then
				mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
			Else
				mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&bMenu=1&nOrig_call=1'"
			End If
			insFinish = True
			
			'+Si se trata de las transacciones restantes
		Case "16", "17", "20", "22"
			'+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
			Call insUpdUserAmend()
			
			'+Declaraciones
		Case "21"
			'+Se llama la rutina que inicializa el campo User_amend de la tabla Póliza/Certificat
			Call insUpdUserAmend()
	End Select
	
	Session("nFinish") = Request.QueryString.Item("nAction")
	'+ se agrego manejo de fecha de ultima modificación para los endosos    
	If insFinish Then
'UPGRADE_NOTE: The 'ePolicy.policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
		lclsPolicy_amend = new ePolicy.policy
		If lclsPolicy_amend.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
			If Session("nTransaction") = 12 Or Session("nTransaction") = 14 Or Session("nTransaction") = 13 Or Session("nTransaction") = 15 Then
				If Session("nCertif") = 0 Then
					If Session("nTransaction") = 13 Or Session("nTransaction") = 15 Then
						lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate)
					Else
						If Request.Form.Item("chkPendenstat") <> "1" Then
							lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
						End If
					End If
				End If
			ElseIf Session("nTransaction") = 1 Or Session("nTransaction") = 6 Or Session("nTransaction") = 4 Or Session("nTransaction") = 24 Or Session("nTransaction") = 25 Or Session("nTransaction") = 28 Or Session("nTransaction") = 29 Or Session("nTransaction") = 26 Or Session("nTransaction") = 27 Or Session("nTransaction") = 30 Or Session("nTransaction") = 31 Or Session("nTransaction") = 18 Or Session("nTransaction") = 19 Then 
				If Request.Form.Item("chkPendenstat") <> "1" Then
					lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
				End If
			ElseIf Session("nTransaction") = 3 And Session("nCertif") = 0 And Request.Form.Item("chkPendenstat") <> "1" Then 
				lclsPolicy_amend.dChangdat = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			End If
			lclsPolicy_amend.Add()
		End If
		lclsPolicy_amend = Nothing
	End If
	'    end if
	'insFinish = False
End Function


'% insUpdUserAmend: se actualiza el campo nUser_amend de Policy o Certificat, según sea el caso
'--------------------------------------------------------------------------------------------
Sub insUpdUserAmend()
	Dim eBranches As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As Object
	Dim lclsCertificat As Object
	Dim lclsValues As Object
	
'UPGRADE_NOTE: The 'ePolicy.Policy' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsPolicy = new ePolicy.Policy
'UPGRADE_NOTE: The 'ePolicy.Certificat' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsCertificat = new ePolicy.Certificat
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsValues = new eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.08
	lclsValues.sSessionID = Session.SessionID
	lclsValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	lclsValues.sCodisplPage = "ValPolicySeq"
	
	If Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngTempCertifAmendment Or Session("nTransaction") = eCollection.Premium.PolTransac.clngRecuperation Or Session("nTransaction") = eCollection.Premium.PolTransac.clngPolicyIssue Or Session("nTransaction") = eCollection.Premium.PolTransac.clngCertifIssue Then
		If CStr(Session("nCertif")) = vbNullString Or CStr(Session("nCertif")) = "0" Then
			'+ Se actualiza el campo en la tabla Policy        
			With lclsPolicy
				.sCertype = Session("sCertype")
				.nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
				.Update_UserAmend()
			End With
			'+ Se actualiza el campo en la tabla Certificat        
			With lclsCertificat
				.sCertype = Session("sCertype")
				.nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
				.nCertif = lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
				.Update_UserAmend()
			End With
		Else
			'+ Se actualiza el campo en la tabla Certificat        
			With lclsCertificat
				.sCertype = Session("sCertype")
				.nBranch = lclsValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.nProduct = lclsValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.nPolicy = lclsValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
				.nCertif = lclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
				.Update_UserAmend()
			End With
		End If
	End If
	
	lclsPolicy = Nothing
	lclsCertificat = Nothing
	lclsValues = Nothing
End Sub

'+Esta función carga automáticamente con contenido las ventanas correspondientes dependiendo de la que se esté tratando.
'------------------------------------------------------------------
Private Sub insGeneralAuto(ByVal sCodispl As String)
	''Dim eRemoteDB.Constants.intNull As Object
	'------------------------------------------------------------------
	Dim lclsAutoCharge As Object
	
	'    mobjNetFrameWork.BeginProcess "AutoUpdGeneral-" & sCodispl
'UPGRADE_NOTE: The 'ePolicy.AutoCharge' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsAutoCharge = new ePolicy.AutoCharge
	Call lclsAutoCharge.InsAutoUpdGeneral(sCodispl, Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("nGroup"), eFunctions.Values.eTypeData.etdLong), Session("sPoliType"), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToDate(Session("dNulldate")), Session("nTransaction"), Session("nUsercode"), Session("sBrancht"), Session("SessionId"), Session("sBussityp"), eRemoteDB.Constants.intNull)
	lclsAutoCharge = Nothing
	'    mobjNetFrameWork.FinishProcess "AutoUpdGeneral-" & sCodispl
End Sub

'-----------------------------------------------------------------------------------
Private Sub insPrintDocuments()
	'-----------------------------------------------------------------------------------
	Dim mobjDocuments As Object
	Dim lstrQueryString As String
	'    mobjNetFrameWork.BeginProcess "insPrintDocuments"
	
'UPGRADE_NOTE: The 'eReports.Report' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	mobjDocuments = new eReports.Report
	If Request.QueryString.Item("sCodispl") = "CA048" Or Request.QueryString.Item("sCodispl") = "CA050" Then
		'Si se está emitiendo una Cotización,
		'se llama el simulador de Cuadro de póliza
		If CStr(Session("sCertype")) = "3" Then
			lstrQueryString = "sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&nGraph=" & Request.Form.Item("hddGraphics")
			Response.Write(("<SCRIPT>ShowPopUp(""/VTimeNet/Common/PrintPol.aspx?" & lstrQueryString & """, ""PrintPolicy"",700,650,""yes"",""no"",100,20,'yes','yes');</" & "Script>"))
		Else
			With mobjDocuments
				.ReportFilename = "CAL001_B.rpt"
				.sCodispl = "CAL001"
				.setStorProcParam(1, Session("sCertype"))
				.setStorProcParam(2, Session("nBranch"))
				.setStorProcParam(3, Session("nProduct"))
				.setStorProcParam(4, Session("nPolicy"))
				.setStorProcParam(5, Session("nCertif"))
				.setStorProcParam(6, "")
				.setStorProcParam(7, "")
				.setStorProcParam(8, "")
				.setStorProcParam(9, "")
				.setStorProcParam(10, "")
				.setStorProcParam(11, "")
				.setStorProcParam(12, "1")
				.setStorProcParam(13, "1")
				Response.Write((.Command))
			End With
		End If
	Else
		With mobjDocuments
			.ReportFilename = "CAL001.rpt"
			.sCodispl = "CAL001"
			.setStorProcParam(1, Session("sCertype"))
			.setStorProcParam(2, Session("nBranch"))
			.setStorProcParam(3, Session("nProduct"))
			.setStorProcParam(4, Session("nPolicy"))
			.setStorProcParam(5, Session("nCertif"))
			.setStorProcParam(6, Mid(Session("dEffecdate"), 7, 4) & Mid(Session("dEffecdate"), 4, 2) & Mid(Session("dEffecdate"), 1, 2))
			.setStorProcParam(7, "9999")
			.setStorProcParam(8, "")
			.setStorProcParam(9, Session("nTransaction"))
			.setStorProcParam(10, "S")
			.setStorProcParam(11, "TIMENOTHING")
			Response.Write((.Command))
		End With
	End If
	mobjDocuments = Nothing
	'    mobjNetFrameWork.FinishProcess "insPrintDocuments"
End Sub

'% updImage: Actualiza la tabla de Imagenes con la imagen que recibe la página 
'--------------------------------------------------------------------------------------------
Function updFile() As String
	'--------------------------------------------------------------------------------------------
    Dim sFilename As String = Path.GetFileName(myRequestFile(2))
    Dim sSavePath As String
    Dim fileAppend As Integer
    Dim lobjValues As eFunctions.Values
        
    lobjValues = New eFunctions.Values
    lobjValues.sSessionID = Session.SessionID
    lobjValues.sCodisplPage = "valImage"
        
    sSavePath = new eProduct.Tab_Clause().GetLoadFile(True)
               
    If String.IsNullOrEmpty(sFilename) Then Return String.Empty
        
    Do While File.Exists(sSavePath & "\" & sFilename)
        fileAppend += 1
        sFilename = Path.GetFileNameWithoutExtension(myRequestFile(2)) & fileAppend.ToString & _
            Path.GetExtension(myRequestFile(2))
    Loop

        Dim newFile As FileStream = Nothing
        
        newFile = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
            
        'For i As Integer = fileContentIndex To fileContentLength
        For i As Integer = 0 To fileContentLength - 1
            newFile.WriteByte(binData(fileContentIndex + i))
        Next
        
        newFile.Close()
        
        Return sSavePath & "\" & sFilename
        	
End Function

    Sub BuildUploadRequest(ByVal data() As Byte)
        'Array que contendrá la data decodificada
        Dim postData(data.Length) As Char
    
        'Se inicializa el decodificador ASCII
        Dim decoder As Decoder = Encoding.ASCII.GetDecoder
    
        'Se decodifican los bytes contenidos en binData, y se almacena en el array postData
        decoder.GetChars(data, 0, data.Length, postData, 0)
    
        'Se obtiene el Encoding Type y el Boundary del Form, y se separan en un array.
        Dim contentType As String = Request.ServerVariables("HTTP_CONTENT_TYPE")
        Dim conTypArr() As String = contentType.Split("; ")
    
        'Se verifica que el Encoding Type sea el correcto. De otro modo no se podra leer el archivo.
        If conTypArr(0) = "multipart/form-data" Then
            'Se obtiene el Boundary del Form. Este dato es el que separa los valores de cada control en el Request.
            Dim bound(1) As String
            bound(1) = conTypArr(1).Split("=")(1)
            'Se obtiene un array, que contiene la data de todos los controles del Form.
            Dim formData() As String = (New String(postData)).Split(bound, StringSplitOptions.RemoveEmptyEntries)
        
            'Se inicializa el diccionario.
            mobjUploadRequest = New Dictionary(Of String, String)
        
            Dim endInfo As Integer
            Dim varInfo As String
            Dim varValue As String
        
            For i As Integer = 0 To formData.Length - 1
                'Se ubican los caracteres separadores.
                endInfo = formData(i).IndexOf(crlf & crlf)
            
                If endInfo > -1 Then
                    'Obtiene el nombre de la variable
                    varInfo = formData(i).Substring(2, endInfo - 2)
                    'Obtiene el valor de la variable
                    varValue = formData(i).Substring(endInfo + 4, formData(i).Length - endInfo - 8)
                
                    'Es este elemento un archivo?
                    If varInfo.Contains("filename=") Then
                        myRequestFile(0) = getFieldName(varInfo)
                        myRequestFile(1) = varValue
                        myRequestFile(2) =  New Random().Next(100000000, 900000000)  & "_" &   getFileName(varInfo)
                        myRequestFile(3) = getFileType(varInfo)
                        
                        fileContentIndex = (New String(postData)).IndexOf(varValue)
                        
                        fileContentLength = varValue.Length
                        
                    Else
                        mobjUploadRequest.Add(getFieldName(varInfo), varValue)
                    End If
                End If
            Next
        End If
    End Sub


'% getString: Conversión de los datos de Byte a String
'--------------------------------------------------------------------------------------------
Function getString(ByRef sStringBin As String) As String
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Integer
	
	getString = vbNullString
	
'UPGRADE_ISSUE: LenB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
	For lintCount = 1 To Len(sStringBin)
'UPGRADE_ISSUE: MidB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
		getString = getString & Chr(Asc(Mid(sStringBin, lintCount, 1)))
	Next 
	
End Function

'% getByteString: Conversión de los datos de String a Byte
'--------------------------------------------------------------------------------------------
Function getByteString(ByRef sStringStr As String) As String
	'--------------------------------------------------------------------------------------------
	Dim linCount As Integer
	Dim lstrchar As String
	For linCount = 1 To Len(sStringStr)
		lstrchar = Mid(sStringStr, linCount, 1)
'UPGRADE_ISSUE: AscB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
'UPGRADE_ISSUE: ChrB function is not supported. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1040.aspx'
		getByteString = getByteString & Chr(Asc(lstrchar))
	Next 
End Function

    Function getFieldName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("name=")
        Dim endPos As Integer = infoStr.Substring(sPos + 5).IndexOf(Chr(34) & ";")
        If endPos = -1 Then
            endPos = infoStr.Substring(sPos + 6).IndexOf(Chr(34))
        End If
        
        Return infoStr.Substring(sPos + 6, endPos)
    End Function

    ' This function retreives a file field's filename
    Function getFileName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("filename=")
        Dim endPos As Integer = infoStr.IndexOf(Chr(34) & crlf)
        getFileName = infoStr.Substring(sPos + 10, endPos - (sPos + 10))
    End Function
    
    ' This function retreives a file field's mime type
    Function getFileType(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("Content-Type: ")
        Return infoStr.Substring(sPos + 14)
    End Function

</script>
<%Response.Expires = -1441

Response.CacheControl = "private"

'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjValues = new eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mstrCommand = "sModule=Policy&sProject=PolicySeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
'+ se limpia variable de session
Session("nFinish") = ""

%> 
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





	


<SCRIPT>
    //+ Variable para el control de versiones 
    document.VssVersion = "$$Revision: 25 $|$$Date: 6/06/06 4:49p $|$$Author: Fmendoza $"

    var mintTpremium = "";
    //%NewLocation: se recalcula el URL de la página
    //------------------------------------------------------------------------------------------
    function NewLocation(Source, Codisp) {
        //------------------------------------------------------------------------------------------
        var lstrLocation = "";
        lstrLocation += Source.location;
        lstrLocation = lstrLocation.replace(/&OPENER=.*/, "") + "&OPENER=" + Codisp
        Source.location = lstrLocation
    }
</SCRIPT>  
</HEAD>
<BODY>
<FORM ID="valCA022Seq" NAME="valCA022Seq">
<%
'UPGRADE_NOTE: The 'ePolicy.ValPolicySeq' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjPolicySeq = new ePolicy.ValPolicySeq

'- Se define la variable para almacenar la nueva dirección de la CA001
mstrLocationCA001 = vbNullString

Response.Write(mobjValues.StyleSheet())

'UPGRADE_NOTE: The 'eProduct.Tab_Clause' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm

'+ Si no se han validado los campos de la página
If Request.QueryString.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	Session("sErrorTable") = mstrErrors
	Session("sForm") = vbNullString
	mblnReload = False
Else
	mblnReload = True
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		If Request.QueryString.Item("ActionType") = "Check" Then
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & "&sValPage=ca022seq" & "&ActionType=" & Request.QueryString.Item("ActionType") & "&nIndex=" & Request.QueryString.Item("nIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """, ""PolicySeqError"",660,330);")
		Else
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & "&sValPage=ca022seq" & mstrQueryString & """, ""PolicySeqError"",660,330);")
			If Request.QueryString.Item("sCodispl") <> "CA021" Then
				.Write(mobjValues.StatusControl(False, Request.QueryString.Item("nZone"), Request.QueryString.Item("WindowType")))
			End If
		End If
		.Write("</SCRIPT>")
	End With
Else
	
        If Request.QueryString.Item("nAction") <> eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
            If insPostSequence() Then
			
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
                    '+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
                    '+ se mueve automaticamente a la siguiente página
                    If mstrLocationCA001 = vbNullString Then
					
                        '+ Validacion para cuando la CA012 llama a la sequencia desde el modulo "Ordenes profesionales".
                        If CStr(Session("CallSequence")) <> "Prof_ord" Then
                            'UPGRADE_NOTE: The 'ePolicy.ValPolicySeq' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            lclsRefresh = New ePolicy.ValPolicySeq
						
                            Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl") & Request.QueryString.Item("nIndexCover"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "Yes"))
                            lclsRefresh = Nothing
                        Else
                            If Request.QueryString.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Prof_ord/Prof_ordseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
                            End If
                        End If
                    Else
                        '+ Se carga nuevamente la ventana principal de la secuencia
                        If mblnReload Then
                            Response.Write("<SCRIPT>window.close();opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
                        End If
                    End If
                    If Request.QueryString.Item("nZone") = "1" Then
                        Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1)</SCRIPT>")
                    End If
                Else
                    If Request.QueryString.Item("sCodispl") <> "CA014" And Request.QueryString.Item("sCodispl") <> "CA014A" And Request.QueryString.Item("sCodispl") <> "VI021" And Request.QueryString.Item("sCodispl") <> "OS001_K" And Request.QueryString.Item("sCodispl") <> "CA027" And Request.QueryString.Item("sCodispl") <> "VI662" Then
                        If Request.QueryString.Item("sCodispl") = "CA025" Then
                            If mblnReload Then
                                Response.Write("<SCRIPT>top.opener.top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
                            End If
                        Else
                            'UPGRADE_NOTE: The 'ePolicy.ValPolicySeq' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Program Files\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
                            lclsRefresh = New ePolicy.ValPolicySeq
                            Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "No"))
                            lclsRefresh = Nothing
                        End If
                    End If
                    If mblnReload Then
                        Response.Write("<SCRIPT>window.close();top.opener.top.opener.top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.QueryString.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.QueryString.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=304" & mstrQueryString & "'</SCRIPT>")
                    End If
                End If
            End If
        Else
            If Request.QueryString.Item("nAction") = eFunctions.Menues.TypeActions.clngAcceptdatafinish Then
                '+ Se recarga la página principal de la secuencia
                If CStr(Session("CallSequence")) = "Prof_ord" Then
                    mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=OS590&sProject=Prof_ordseq&sModule=Prof_ord'"
                    Response.Write("<SCRIPT>top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
                Else
                    If insFinish() Then
                        If Request.QueryString.Item("sCodisplReload") = "CA048" Then
                            Response.Write("<SCRIPT>window.close();top.opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
                        Else
                            If Request.QueryString.Item("sCodispl") = "CA048" Then
                                mstrLocationCA001 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy&sConfig=&nAction=0" & Request.QueryString.Item("nMainAction") & "&bMenu=1'"
                                Response.Write("<SCRIPT>top.opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
                            ElseIf Request.QueryString.Item("sCodispl") = "CA050" Then
                                Response.Write("<SCRIPT>top.opener.top.document.location=" & mstrLocationCA001 & ";</SCRIPT>")
                            End If
                        End If
                    Else
                        Response.Write("<SCRIPT>alert('No se pudo realizar la actualización final');</SCRIPT>")
                    End If
                End If
            End If
        End If
End If
mobjPolicySeq = Nothing
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.55
'Call mobjNetFrameWork.FinishPage("ValPolicySeq")
'mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





