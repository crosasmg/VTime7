<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mclsValues As eFunctions.Values


'**% ValExistPolicy: It's verify the policy exist.
'% ValExistPolicy: Se verifica la existencia de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub ValExistPolicy()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim lclsNull_Condi As ePolicy.Null_condi
	Dim llngCertif As Object
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	lclsNull_Condi = New ePolicy.Null_condi
	
	llngCertif = 0
	
	If lclsPolicy.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		If lclsPolicy.sPolitype = "2" Then
			Response.Write("opener.document.forms[0].tcnCertif.disabled=false" & ";")
		Else
			Response.Write("opener.document.forms[0].tcnCertif.disabled=true" & ";")
			Response.Write("opener.document.forms[0].tcnCertif.value=0" & ";")
			Response.Write("opener.document.forms[0].dtcClientCO.value=" & lclsPolicy.sClient & ";")
			Response.Write("opener.document.forms[0].tcdEffecdate.value='" & lclsPolicy.dStartdate & "';")
			Response.Write("opener.document.forms[0].tcdExpirdat.value='" & lclsPolicy.dExpirdat & "';")
			Response.Write("opener.document.forms[0].tcdChangdat.value='" & lclsPolicy.dChangdat & "';")
			
			If lclsPolicy.sClient <> "" Then
				If lclsRoles.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 1, lclsPolicy.sClient, mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
					Response.Write("opener.UpdateDiv(""lblClienameCo"",""" & lclsRoles.sCliename & """,""Normal"");")
				End If
			End If
			
			If lclsNull_Condi.FindClientName("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 2, mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				
				Response.Write("opener.document.forms[0].dtcClientAS.value=" & lclsNull_Condi.sClient & ";")
				Response.Write("opener.UpdateDiv(""lblClienameAs"",""" & lclsNull_Condi.sCliename & """,""Normal"");")
			End If
		End If
	End If
	
	lclsNull_Condi = Nothing
	lclsPolicy = Nothing
End Sub



'% InsShowDataPolicy: Se verifica la existencia de la póliza.
'--------------------------------------------------------------------------------------------
Private Sub InsShowDataPolicy()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim llngCertif As Object
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	
	llngCertif = 0
	
	If lclsPolicy.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		If lclsPolicy.sPolitype = "2" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false" & ";")
            Else
                If Request.QueryString.Item("sCodispl") = "VIL1486_K" Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
                Else
                
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true" & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsPolicy.SCLIENT & "';")
			        
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
                    If Request.QueryString.Item("sCodispl") <> "CAL010_K" Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy.DEXPIRDAT, eFunctions.Values.eTypeData.etdDate) & "';")
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='" & mclsValues.TypeToString(lclsPolicy.dChangdat, eFunctions.Values.eTypeData.etdDate) & "';")
                        'Response.Write "top.frames['fraHeader'].document.forms[0].tcdmodDate.value='" & lclsPolicy.dChangdat & "';"               
                    Else
                        '+ Si es impresion de polizas o certificado cobertura
                        If Request.QueryString.Item("sCodispl") = "CAL010_K" And (Request.QueryString.Item("nTypeRpt") = 1 Or Request.QueryString.Item("nTypeRpt") = 3) Then
                            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdateRpt.value='" & lclsPolicy.dStartdate & "';")
                        End If
                    End If
                    If lclsPolicy.SCLIENT <> "" Then
                        If lclsRoles.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 1, lclsPolicy.SCLIENT, mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
                            Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
                            Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
                        End If
                    End If
			        
                    If lclsRoles.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				
                        Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.SCLIENT & "';")
                        Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
                        Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
                    End If
            
                    
                End If
            End If
        Else
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0;")
            If Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
                Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
		    End If 
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
		
            If Request.QueryString.Item("scodispl") <> "CAL010_K" And Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='';")
                'Response.Write "top.frames['fraHeader'].document.forms[0].tcdmodDate.value='';"
            End If
		    If Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
                Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
            End If                 
        End If
	
	lclsPolicy = Nothing
	lclsRoles = Nothing
End Sub

'% insShowDataCertif: Se obtinen los datos de la poliza/certificado
'--------------------------------------------------------------------------------------------
Private Sub insShowDataCertif()
	'--------------------------------------------------------------------------------------------
	Dim lclsCertificat As ePolicy.Certificat
	Dim lclsRoles As ePolicy.Roles
	
	lclsCertificat = New ePolicy.Certificat
	lclsRoles = New ePolicy.Roles
	
        If lclsCertificat.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                               mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                               mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), _
                               mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		
            If Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
            Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsCertificat.sClient & "';")
            End if                
            If Request.QueryString.Item("sCodispl") <> "CAL010_K" And Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lclsCertificat.dStartdate & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & lclsCertificat.dExpirdat & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='" & lclsCertificat.dChangdat & "';")
            Else
                '+ Si es impresion de polizas o certificado cobertura
                If Request.QueryString.Item("sCodispl") = "CAL010_K" And (Request.QueryString.Item("nTypeRpt") = "1" Or Request.QueryString.Item("nTypeRpt") = "3") Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdateRpt.value='" & lclsCertificat.dStartdate & "';")
                End If
            End If
		
            
            If Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                If lclsRoles.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), 1, "", mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsRoles.SCLIENT & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
                End If
            
		
                If lclsRoles.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.SCLIENT & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
                Else
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",'',""Normal"");")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
                End If
            End If                 
        Else
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
            If Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
                Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
            End If                
            If Request.QueryString.Item("sCodispl") <> "CAL010_K" And Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='';")
                'Response.Write "top.frames['fraHeader'].document.forms[0].tcdmodDate.value='';"
            End If
            If Request.QueryString.Item("sCodispl") <> "VIL1486_K" Then
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
                Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
            End If 
            Dim lobjErrors As eGeneral.GeneralFunction
            Dim lstrAlert As String
            lobjErrors = New eGeneral.GeneralFunction
            
            lstrAlert = "Err. 60129 " & lobjErrors.insLoadMessage(34050)
            
            Response.Write("alert('" & lstrAlert & "');")
            
            lobjErrors = Nothing
        End If
	
	
        lclsRoles = Nothing
        lclsCertificat = Nothing
    End Sub

'% Policy_data: Se obtinen los datos de la poliza/certificado
'--------------------------------------------------------------------------------------------
Private Sub Policy_data()
	Dim lclsPolicy As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsCertificat As ePolicy.Certificat
	Dim lclsRoles As ePolicy.Roles
	Dim lclsNull_Condi As ePolicy.Null_condi
	
	lclsCertificat = New ePolicy.Certificat
	lclsRoles = New ePolicy.Roles
	lclsNull_Condi = New ePolicy.Null_condi
	
	If lclsCertificat.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("opener.document.forms[0].dtcClientCO.value=" & lclsCertificat.sClient & ";")
		Response.Write("opener.document.forms[0].tcdEffecdate.value='" & lclsCertificat.dStartdate & "';")
		Response.Write("opener.document.forms[0].tcdExpirdat.value='" & lclsCertificat.dExpirdat & "';")
		Response.Write("opener.document.forms[0].tcdChangdat.value='" & lclsCertificat.dChangdat & "';")
		
		If lclsCertificat.sClient <> "" Then
			If lclsRoles.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 1, lclsCertificat.sClient, mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("opener.UpdateDiv(""lblClienameCo"",""" & lclsRoles.sCliename & """,""Normal"");")
			End If
		End If
		
		If lclsNull_Condi.FindClientName("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
			
			Response.Write("opener.document.forms[0].dtcClientAS.value=" & lclsNull_Condi.sClient & ";")
			Response.Write("opener.UpdateDiv(""lblClienameAs"",""" & lclsNull_Condi.sCliename & """,""Normal"");")
		End If
	End If
	
	lclsCertificat = Nothing
	lclsNull_Condi = Nothing
	lclsPolicy = Nothing
End Sub

'% insShowCertif: Habilita o deshabilita el campo nCertif dependiendo del tipo de póliza pasada como parámetro.
'--------------------------------------------------------------------------------------------
Private Sub insShowCertif()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	
	lclsPolicy = New ePolicy.Policy
	
	With lclsPolicy
		If .Find(Request.QueryString.Item("sCertype"), CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy"))) Then
			If .sPolitype = "1" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='0';")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
			End If
		End If
	End With
	
	lclsPolicy = Nothing
End Sub

'% insShowLeg: Habilita o deshabilita el campo del Tope de capital por evaluacion.
'--------------------------------------------------------------------------------------------
Private Sub insShowLeg()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	lclsPolicy = New ePolicy.Policy
	With lclsPolicy
		If mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True) <> eRemoteDB.Constants.intNull Then
			If .insprecal825(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				If lclsPolicy.nLegAmount_old <> 0 Then
					Response.Write("top.frames['fraHeader'].document.forms[0].tcnLegAmount_old.value='" & mclsValues.TypeToString(lclsPolicy.nLegAmount_old, eFunctions.Values.eTypeData.etdDouble) & "';")
				End If
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnLegAmount.disabled=false;")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnLegAmount.value='" & mclsValues.TypeToString(lclsPolicy.nLegAmount, eFunctions.Values.eTypeData.etdDouble) & "';")
			End If
			If (lclsPolicy.nLegAmount_old <> 0 And lclsPolicy.nLegAmount_old <> eRemoteDB.Constants.intNull) And lclsPolicy.nLegAmount_old <> lclsPolicy.nLegAmount Then
				Response.Write("alert(""Adv. 55111: " & "Nuevo monto del tope de capital por evaluaciòn es distinto al anterior" & """);")
			End If
		End If
	End With
	lclsPolicy = Nothing
End Sub
'% ShownReceipt: Actualiza los campos desde hasta de nReceipt
'--------------------------------------------------------------------------------------------
Private Sub ShownReceipt()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	lclsPremium = New eCollection.Premium
	With lclsPremium
		If .Find_MaxMin_nReceipt(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy"))) Then
			If lclsPremium.nReceipt_Min <> eRemoteDB.Constants.intNull Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_Beg.value='" & lclsPremium.nReceipt_Min & "';")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_Beg.value='';")
			End If
			If lclsPremium.nReceipt_Max <> eRemoteDB.Constants.intNull Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_End.value='" & lclsPremium.nReceipt_Max & "';")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_End.value='';")
			End If
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_Beg.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_End.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_Beg.disabled=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnRec_End.disabled=true;")
		End If
	End With
	lclsPremium = Nothing
End Sub
'% ShownContrat: Actualiza los campos desde hasta de nContrat
'--------------------------------------------------------------------------------------------
Private Sub ShownContrat()
	'--------------------------------------------------------------------------------------------
	Dim lclsPremium As eCollection.Premium
	lclsPremium = New eCollection.Premium
	With lclsPremium
		If .Find_MaxMin_nContrat(CInt(Request.QueryString.Item("nBranch")), CInt(Request.QueryString.Item("nProduct")), CDbl(Request.QueryString.Item("nPolicy"))) Then
			If lclsPremium.nContrat_Min <> eRemoteDB.Constants.intNull Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_Beg.value='" & lclsPremium.nContrat_Min & "';")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_Beg.value='';")
			End If
			If lclsPremium.nContrat_Max <> eRemoteDB.Constants.intNull Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_End.value='" & lclsPremium.nContrat_Max & "';")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_End.value='';")
			End If
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_Beg.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_End.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_Beg.disabled=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCon_End.disabled=true;")
		End If
	End With
	lclsPremium = Nothing
End Sub

'% GetPolicyData: Obtiene los datos de la póliza
'--------------------------------------------------------------------------------------------
Private Sub GetPolicyData()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim lclsClient As eClient.Client
	Dim lclsAccount_pol As Object
	Dim llngBranch As Integer
	Dim llngProduct As Integer
	Dim llngPolicy As Double
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	lclsClient = New eClient.Client
	
	With mclsValues
		llngBranch = .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
		llngProduct = .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
		llngPolicy = .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
	End With
	
	With lclsPolicy
		Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
		If .Find("2", llngBranch, llngProduct, llngPolicy) Then
			If .sPolitype = "2" Then
				Response.Write("tcnCertif.disabled=false;")
			Else
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value='0';")
				Call GetCertifData()
			End If
			
			Call lclsRoles.Find("2", llngBranch, llngProduct, llngPolicy, 0, 1, vbNullString, Today)
			
			Call lclsClient.Find(lclsRoles.sClient)
			
			
			Response.Write("dtcClient.value='" & lclsRoles.sClient & "';")
			Response.Write("dtcClient_Digit.value='" & lclsClient.sDigit & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','" & lclsRoles.sCliename & "');")
		Else
			Response.Write("tcnCertif.disabled=false;")
			Response.Write("tcnCertif.value='';")
			Response.Write("dtcClient.value='';")
			Response.Write("dtcClient_Digit.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','');")
		End If
		Response.Write("}")
	End With
	lclsPolicy = Nothing
	lclsRoles = Nothing
End Sub

'% GetCertifData: Obtiene los datos del certificado
'--------------------------------------------------------------------------------------------
Private Sub GetCertifData()
	'--------------------------------------------------------------------------------------------
	Dim lclsAccount_pol As ePolicy.Account_Pol
	Dim lcolCommission As ePolicy.Commissions
	Dim llngBranch As Integer
	Dim llngProduct As Integer
	Dim llngPolicy As Double
	Dim llngCertif As Double
	Dim lclsRoles As ePolicy.Roles
	Dim lclsClient As eClient.Client
	
	
	lclsRoles = New ePolicy.Roles
	lclsClient = New eClient.Client
	lclsAccount_pol = New ePolicy.Account_Pol
	lcolCommission = New ePolicy.Commissions
	
	With mclsValues
		llngBranch = .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
		llngProduct = .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
		llngPolicy = .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
		llngCertif = .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
	End With
	Response.Write("with (top.frames['fraHeader'].document.forms[0]){")
	
	Call lclsRoles.Find("2", llngBranch, llngProduct, llngPolicy, 0, 1, vbNullString, Today)
	
	Call lclsClient.Find(lclsRoles.sClient)
	
	
	Response.Write("dtcClient.value='" & lclsRoles.sClient & "';")
	Response.Write("dtcClient_Digit.value='" & lclsClient.sDigit & "';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('lblCliename','" & lclsRoles.sCliename & "');")
	
	With lclsAccount_pol
		If .Find("2", llngBranch, llngProduct, llngPolicy, llngCertif) Then
			Response.Write("tcnYearP.value='" & .nNextyear & "';")
			Response.Write("cbeMonthP.value='" & .nNextmonth & "';")
			Response.Write("tcnYear.value='" & .nNextyear & "';")
			Response.Write("cbeMonth.value='" & .nNextmonth & "';")
			Response.Write("hdddVp_neg.value='" & mclsValues.TypeToString(.dVp_neg, eFunctions.Values.eTypeData.etdDate) & "';")
		Else
			Response.Write("tcnYearP.value='';")
			Response.Write("cbeMonthP.value='';")
			Response.Write("hdddVp_neg.value='';")
		End If
	End With
	
	With lcolCommission
		If .Find("2", llngBranch, llngProduct, llngPolicy, Today, llngCertif) Then
			Response.Write("valIntermed.value='" & .Item(1).nIntermed & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','" & .Item(1).sCliename & "');")
		Else
			Response.Write("valIntermed.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermedDesc','');")
		End If
	End With
	Response.Write("}")
	
	lclsRoles = Nothing
	lclsClient = Nothing
	lclsAccount_pol = Nothing
	lcolCommission = Nothing
End Sub

'% insValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
'%				   Debe ser invocada con la funcion insDefValues
'--------------------------------------------------------------------------------------------
Sub insValPolitype()
	Dim insSurrenDate As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrFrame As String
	
	lclsPolicy = New ePolicy.Policy
	lstrFrame = Request.QueryString.Item("sFrame")
	If lstrFrame = vbNullString Then
		lstrFrame = "fraHeader"
	End If
	'+ Busca por cambio de numero de poliza
	If lclsPolicy.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		'+Asignación del Tipo de póliza
		Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=""0"";")
				If Request.QueryString.Item("sCodispl") = "VI009_K" Then
					'Call insSurrenDate(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0)
                    'REVISA EN LA BASE
				End If
				Call inspreval633()
			Case "2"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value=""0"";")
				Response.Write("tcnCertif.focus();")
			Case "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.focus();")
		End Select
		'+ En caso que se necesite el onblur del campo Certificado de la página
		If Request.QueryString.Item("sExecCertif") = "1" Then
			Response.Write("if(tcnCertif.disabled)")
			Response.Write("top.frames['" & lstrFrame & "'].$('#tcnCertif').change();")
		End If
		Response.Write("}")
	End If
	lclsPolicy = Nothing
End Sub

'% ValPolitype: valida el tipo de póliza para habilitar/deshabilitar el certificado
'% Debe ser invocada con funcion insDefValues
'--------------------------------------------------------------------------------------------
Sub ValPolitype()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrFrame As String
	
	lclsPolicy = New ePolicy.Policy
	lstrFrame = Request.QueryString.Item("sFrame")
	If lstrFrame = vbNullString Then
		lstrFrame = "fraHeader"
	End If
	If lclsPolicy.Find("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		'+Asignación del Tipo de póliza
		Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
		Select Case lclsPolicy.sPolitype
			Case "1"
				Response.Write("tcnCertif.disabled=true;")
				Response.Write("tcnCertif.value=""0"";")
				Session("nCertif") = 0
			Case "2"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value=""0"";")
				Response.Write("tcnCertif.focus();")
			Case "3"
				Response.Write("tcnCertif.disabled=false;")
				Response.Write("tcnCertif.value=""0"";")
				Response.Write("tcnCertif.focus();")
		End Select
		
		If Request.QueryString.Item("sExecCertif") = "1" Then
			Response.Write("if(tcnCertif.disabled)")
			Response.Write("top.frames['" & lstrFrame & "'].$('#tcnCertif').change();")
		End If
		Response.Write("}")
	Else
		Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.disabled=false;")
		Response.Write("top.frames['" & lstrFrame & "'].document.forms[0].tcnCertif.value="""";")
	End If
	lclsPolicy = Nothing
End Sub

'% inspreval633: Rescata los valores por defecto de la poliza puntual 
'--------------------------------------------------------------------------------------------
Sub inspreval633()
	'--------------------------------------------------------------------------------------------
	'+ para cetificado 0 rescata inmediatamente los datos                 
	Dim mclsPolicytra As ePolicy.ValPolicyTra
	Dim lstrFrame As String
	
	mclsPolicytra = New ePolicy.ValPolicyTra
	lstrFrame = "fraHeader"
	
	Session("nBranch") = mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
	Session("nProduct") = mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
	Session("nPolicy") = mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
	
	If IsNothing(Request.QueryString.Item("nCertif")) Then
		Session("nCertif") = 0
	Else
		Session("nCertif") = Request.QueryString.Item("nCertif")
	End If
	Response.Write("with(top.frames['" & lstrFrame & "'].document.forms[0]){")
	If mclsPolicytra.insPreVAL633_K("2", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Today) Then
		Session("sPolitype") = mclsPolicytra.sPolitype
		Response.Write("dtcClient.value='" & mclsPolicytra.sClient & "';")
		Response.Write("dtcClient_Digit.value='" & mclsPolicytra.sCliDigit & "';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('tctCliename','" & mclsPolicytra.sClientName & "');")
		If mclsPolicytra.nIntermed = eRemoteDB.Constants.intNull Then
			Response.Write("valIntermedia.value='" & "" & "';")
		Else
			Response.Write("valIntermedia.value='" & mclsValues.TypeToString(mclsPolicytra.nIntermed, eFunctions.Values.eTypeData.etdDouble) & "';")
		End If
		Response.Write("top.frames['fraHeader'].UpdateDiv('valIntermediaDesc','" & mclsPolicytra.sInterName & "');")
		Response.Write("tcnPayfreq.value='" & mclsValues.TypeToString(mclsPolicytra.nPayfreq, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("tcdNextReceip.value='" & mclsValues.TypeToString(mclsPolicytra.dNextReceip, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("tcnNegVPMonths.value='" & mclsValues.TypeToString(mclsPolicytra.nNegVPMonths, eFunctions.Values.eTypeData.etdDouble) & "';")
		Response.Write("}")
	End If
	mclsPolicytra = Nothing
End Sub

Private Sub GetDate_value()
	Dim lclsPolicy As ePolicy.Policy
	Dim ldtmLast_date As Date
	Dim llngBranch As Integer
	Dim llngProduct As Integer
	Dim lstrDate As String
	
	lclsPolicy = New ePolicy.Policy
	
	llngBranch = mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
	llngProduct = mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
       

        ldtmLast_date = lclsPolicy.GetLast_date_APV(Request.QueryString.Item("sCodisplOri"), llngBranch, llngProduct)
        
        If ldtmLast_date <> eRemoteDB.dtmNull Then
     
            If Microsoft.VisualBasic.Day(ldtmLast_date) < 10 Then
                lstrDate = "0" & Microsoft.VisualBasic.Day(ldtmLast_date) & "/"
            Else
                lstrDate = Microsoft.VisualBasic.Day(ldtmLast_date) & "/"
            End If
	
            If Month(ldtmLast_date) < 10 Then
                lstrDate = lstrDate & "0" & Month(ldtmLast_date) & "/"
            Else
                lstrDate = lstrDate & Month(ldtmLast_date) & "/"
            End If
        
            If Date.MinValue = ldtmLast_date Then
                lstrDate = lstrDate & "1900"
            Else
                lstrDate = lstrDate & Year(ldtmLast_date)
            End If
        
	
	
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecDate.value='" & lstrDate & "';")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecDate.disabled=true;")
            Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdEffecDate.disabled=true;")

        Else
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecDate.disabled=false;")
            Response.Write("top.frames['fraHeader'].document.forms[0].btn_tcdEffecDate.disabled=false;")
            Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecDate.value='';")
        End If
        If Request.QueryString.Item("sCodisplOri") = "VIL1405" Then
            Response.Write("top.frames['fraHeader'].document.forms[0].hddnIdproces.value='" & lclsPolicy.nIdproces & "';")
        End If
	
        lclsPolicy = Nothing
        ldtmLast_date = Nothing
        llngBranch = Nothing
        llngProduct = Nothing
        lstrDate = Nothing
	
End Sub


'%PolicyDateProc: Obtiene fecha de inicio de vigencia de póliza
'----------------------------------------------------------------
Private Sub PolicyDateProc()
	'----------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrDate As String
	
	lclsPolicy = New ePolicy.Policy
	
	If lclsPolicy.Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		If lclsPolicy.dChangdat > lclsPolicy.dStartdate Then
			lstrDate = mclsValues.TypeToString(lclsPolicy.dChangdat, eFunctions.Values.eTypeData.etdDate)
		Else
			lstrDate = mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate)
		End If
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lstrDate & "';")
		Response.Write("if (top.frames['fraHeader'].document.forms[0].chkContinue.checked) top.frames['fraHeader'].document.forms[0].tcdContinue.value = '" & lstrDate & "';")
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
	End If
	lclsPolicy = Nothing
End Sub

'% DataPolicy: Se verifica la existencia de la póliza y certificado para la CAL1415.
'--------------------------------------------------------------------------------------------
Private Sub InsDataPolicyCertif(ByRef ntype As Byte)
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim llngCertif As Object
	Dim llngsCertype As String
	Dim lclsCertificat As ePolicy.Certificat
	Dim lclsNull_Condi As Object
	Dim lclsProduct_li As eProduct.Product
	
	lclsCertificat = New ePolicy.Certificat
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	lclsProduct_li = New eProduct.Product
	
	llngsCertype = "1"
	llngCertif = 0
	
	If ntype = 1 Then
		If (lclsProduct_li.FindProduct_li(mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), Today)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCost.value='" & mclsValues.TypeToString(lclsProduct_li.nCostRe, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].cbeCurrency.value='" & mclsValues.TypeToString(lclsProduct_li.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
			Response.Write("top.frames['fraHeader'].nCost ='" & mclsValues.TypeToString(lclsProduct_li.nCostRe, eFunctions.Values.eTypeData.etdDouble, True, 6) & "';")
			Response.Write("top.frames['fraHeader'].nCurrency ='" & mclsValues.TypeToString(lclsProduct_li.nCurrency, eFunctions.Values.eTypeData.etdDouble) & "';")
		End If
		
		If lclsPolicy.Find(llngsCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If lclsPolicy.sPolitype = "2" Then
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false" & ";")
			Else
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true" & ";")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsPolicy.sClient & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
				
				If lclsPolicy.sClient <> "" Then
					If lclsRoles.Find(llngsCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 1, lclsPolicy.sClient, mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
						Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
						Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
					End If
				End If
				
				If lclsRoles.Find(llngsCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
					
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.sClient & "';")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
					Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
				End If
			End If
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0;")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
		End If
	Else
		If lclsCertificat.Find(llngsCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsCertificat.sClient & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lclsCertificat.dStartdate & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & lclsCertificat.dExpirdat & "';")
			
			If lclsCertificat.sClient <> "" Then
				If lclsRoles.Find(llngsCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), 1, lclsCertificat.sClient, mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
					Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
				End If
			End If
			
			If lclsRoles.Find(llngsCertype, mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.sClient & "';")
				Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
			End If
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
			Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
		End If
	End If
	
	lclsPolicy = Nothing
	lclsRoles = Nothing
	lclsCertificat = Nothing
	lclsProduct_li = Nothing
End Sub

'% InsShowDataProp: Se verifica la existencia de la propuesta.
'--------------------------------------------------------------------------------------------
Private Sub InsShowDataProp()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim llngCertif As Object
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	
	llngCertif = 0
	
	If lclsPolicy.Find("1", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		'+ Si la póliza es colectiva
		If lclsPolicy.sPolitype = "2" Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false" & ";")
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true" & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
			
			'+ Se colocan los datos del contratante            
			If lclsPolicy.sClient <> "" Then
				If lclsRoles.Find("1", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 1, lclsPolicy.sClient, mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsPolicy.sClient & "';")
					Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
				End If
			End If
			
			'+ Se colocan los datos del asegurado principal            
			If lclsRoles.Find("1", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.sClient & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
				Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
			End If
		End If
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0;")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
	End If
	
	lclsPolicy = Nothing
	lclsRoles = Nothing
End Sub

'% InsShowDataPropCer: Se obtinen los datos de la Propuesta/Certificado
'--------------------------------------------------------------------------------------------
Private Sub InsShowDataPropCer()
	'--------------------------------------------------------------------------------------------
	Dim lclsCertificat As ePolicy.Certificat
	Dim lclsRoles As ePolicy.Roles
	
	lclsCertificat = New ePolicy.Certificat
	lclsRoles = New ePolicy.Roles
	
	If lclsCertificat.Find("1", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & lclsCertificat.dStartdate & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & lclsCertificat.dExpirdat & "';")
		
		'+ Datos del Contratante de la propuesta
		If lclsCertificat.sClient <> "" Then
			If lclsRoles.Find("1", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), 1, "", mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsRoles.sClient & "';")
				Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
			End If
		End If
		
		'+ Datos del Asegurado principal        
		If lclsRoles.Find("1", mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsCertificat.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.sClient & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
		End If
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
	End If
	
	lclsRoles = Nothing
	lclsCertificat = Nothing
End Sub

'% InsShowDataPropCot: Se obtinen los datos de la Propuesta/Cotización
'--------------------------------------------------------------------------------------------
Private Sub InsShowDataPropCot()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicy As ePolicy.Policy
	Dim lclsRoles As ePolicy.Roles
	Dim llngCertif As Object
	
	lclsPolicy = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	
	llngCertif = 0
	
	If lclsPolicy.Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble)) Then
		
		'+ Si la póliza es colectiva
		If lclsPolicy.sPolitype = "2" Then
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false" & ";")
		Else
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true" & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
			
			'+ Se colocan los datos del contratante            
			If lclsPolicy.sClient <> "" Then
				If lclsRoles.Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 1, lclsPolicy.sClient, mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsPolicy.sClient & "';")
					Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
					Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
				End If
			End If
			
			'+ Se colocan los datos del asegurado principal            
			If lclsRoles.Find(Request.QueryString.Item("sCertype"), mclsValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(llngCertif, eFunctions.Values.eTypeData.etdDouble), 2, "", mclsValues.StringToType(CStr(lclsPolicy.dStartdate), eFunctions.Values.eTypeData.etdDate)) Then
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.sClient & "';")
				Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
				Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
			End If
		End If
	Else
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true;")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0;")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
		
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
		Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
	End If
	
	lclsPolicy = Nothing
	lclsRoles = Nothing
End Sub


Private Sub ShowDataProduct()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicyRep As ePolicy.ValPolicyRep
	Dim lclsPolicy_po As ePolicy.Policy
	Dim lclsCertificat As Object
	Dim lintBranch As Object
	Dim lclsProduct As eProduct.Product
	Dim lclsRoles As ePolicy.Roles
	Dim llngCertif As Byte
	Dim lclsGeneral As eGeneral.GeneralFunction
	
	llngCertif = 0
	lclsPolicyRep = New ePolicy.ValPolicyRep
	lclsPolicy_po = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	lclsProduct = New eProduct.Product
	'+limpia los valores 
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
	Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].valProduct1.value='';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('valProduct1Desc','','');")
	
	
	'+ se agrego este manejo para el numero unico de poliza
	If lclsPolicy_po.FindPolicybyPolicy("2", CDbl(Request.QueryString.Item("nPolicy"))) Then
		If lclsPolicyRep.InsExistsCal0150X(Request.QueryString.Item("sCodispl"), lclsPolicy_po.nProduct) Then
			
                Response.Write("top.frames['fraHeader'].document.forms[0].valProduct1.value=" & lclsPolicy_po.nProduct & ";")
                If lclsPolicy_po.sPolitype = "1" Then
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true" & ";")
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
                Else
                    Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=false" & ";")
                    'Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
                End If
                Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsPolicy_po.SCLIENT & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy_po.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy_po.DEXPIRDAT, eFunctions.Values.eTypeData.etdDate) & "';")
                Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='" & mclsValues.TypeToString(lclsPolicy_po.dChangdat, eFunctions.Values.eTypeData.etdDate) & "';")
			
			
                If lclsProduct.Find(lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, Today) Then
				
                    Response.Write("top.frames['fraHeader'].UpdateDiv('valProduct1Desc','" & lclsProduct.sDescript & "','Normal');")
                End If
                If lclsRoles.Find("2", lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, lclsPolicy_po.nPolicy, 0, 1, "", lclsPolicy_po.dStartdate) Then
				
                    Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
                End If
			
                If lclsRoles.Find("2", lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, lclsPolicy_po.nPolicy, 0, 2, "", lclsPolicy_po.dStartdate) Then
				
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.SCLIENT & "';")
                    Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
                    Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
                End If
			
                If Request.QueryString.Item("sCodispl") = "CAL01512" Then
                    If lclsPolicy_po.FindDateLastEdit("2", mclsValues.StringToType(CStr(lclsPolicy_po.nBranch), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy_po.nProduct), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(CStr(lclsPolicy_po.nPolicy), eFunctions.Values.eTypeData.etdDouble)) Then
                        Response.Write("top.frames['fraHeader'].document.forms[0].tcdCopydate.value='" & mclsValues.TypeToString(lclsPolicy_po.dDateCopy, eFunctions.Values.eTypeData.etdDate) & "';")
                    End If
                End If
			
            Else
                If lclsProduct.Find(lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, Today) Then
				
				
                    lclsGeneral = New eGeneral.GeneralFunction
                    Response.Write("alert('Err 80192:  " & lclsGeneral.insLoadMessage(80192) & "(  " & lclsPolicy_po.nProduct & "- " & lclsProduct.sDescript & " ) ' );")
                End If
                lclsGeneral = Nothing
            End If
            End If
	
	lclsPolicyRep = Nothing
	lclsPolicy_po = Nothing
	lclsProduct = Nothing
	lclsRoles = Nothing
	
End Sub




Private Sub ShowDataProduct2()
	'--------------------------------------------------------------------------------------------
	Dim lclsPolicyRep As ePolicy.ValPolicyRep
	Dim lclsPolicy_po As ePolicy.Policy
	Dim lclsCertificat As Object
	Dim lintBranch As Object
	Dim lclsProduct As eProduct.Product
	Dim lclsRoles As ePolicy.Roles
	Dim llngCertif As Byte
	Dim lclsGeneral As Object
	
	llngCertif = 0
	lclsPolicyRep = New ePolicy.ValPolicyRep
	lclsPolicy_po = New ePolicy.Policy
	lclsRoles = New ePolicy.Roles
	lclsProduct = New eProduct.Product
	'+limpia los valores 
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientAS_Name','','Normal');")
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
	Response.Write("top.frames['fraHeader'].UpdateDiv('dtcClientCO_Name','','Normal');")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='';")
	Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value='';")
	Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','','');")
	
	'+ se agrego este manejo para el numero unico de poliza
	If lclsPolicy_po.FindPolicybyPolicy("2", CDbl(Request.QueryString.Item("nPolicy"))) Then
		
		Response.Write("top.frames['fraHeader'].document.forms[0].valProduct.value=" & lclsPolicy_po.nProduct & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.disabled=true" & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcnCertif.value=0" & ";")
		Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO.value='" & lclsPolicy_po.sClient & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & mclsValues.TypeToString(lclsPolicy_po.dStartdate, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdExpirdat.value='" & mclsValues.TypeToString(lclsPolicy_po.dExpirdat, eFunctions.Values.eTypeData.etdDate) & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tcdChangdat.value='" & mclsValues.TypeToString(lclsPolicy_po.dChangdat, eFunctions.Values.eTypeData.etdDate) & "';")
		
		
		If lclsProduct.Find(lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, Today) Then
			
			Response.Write("top.frames['fraHeader'].UpdateDiv('valProductDesc','" & lclsProduct.sDescript & "','Normal');")
		End If
		If lclsRoles.Find("2", lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, lclsPolicy_po.nPolicy, 0, 1, "", lclsPolicy_po.dStartdate) Then
			
			Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientCO_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientCO_Digit.value='" & lclsRoles.sDigit & "';")
		End If
		
		If lclsRoles.Find("2", lclsPolicy_po.nBranch, lclsPolicy_po.nProduct, lclsPolicy_po.nPolicy, 0, 2, "", lclsPolicy_po.dStartdate) Then
			
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS.value='" & lclsRoles.sClient & "';")
			Response.Write("top.frames['fraHeader'].document.forms[0].dtcClientAS_Digit.value='" & lclsRoles.sDigit & "';")
			Response.Write("top.frames['fraHeader'].UpdateDiv(""dtcClientAS_Name"",""" & lclsRoles.sCliename & """,""Normal"");")
			
		End If
		lclsGeneral = Nothing
	End If
	lclsPolicyRep = Nothing
	lclsPolicy_po = Nothing
	lclsProduct = Nothing
	lclsRoles = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
'Set mclsValues = Server.CreateObjecmodDatet("eFunctions.Values")
mclsValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mclsValues.sSessionID = Session.SessionID
mclsValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mclsValues.sCodisplPage = "showdefvalues"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 6 $|$$Date: 15-10-09 17:28 $|$$Author: Nmoreno $"
</SCRIPT>
</HEAD>
<BODY>
    <FORM NAME="ShowValues">
    </FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "nPolicy"
		Call ValExistPolicy()
	Case "nCertif"
		Call Policy_data()
	Case "ShowCertif"
		Call insShowCertif()
	Case "ShowLeg"
		insShowLeg()
	Case "GetPolicyData"
		GetPolicyData()
	Case "GetCertifData"
		GetCertifData()
	Case "ShownReceipt"
		ShownReceipt()
		ShownContrat()
	Case "insValsPolitype"
		Call insValPolitype()
	Case "ValPolitype"
		Call ValPolitype()
	Case "Loadpolicy"
		Call inspreval633()
	Case "Date_value"
		Call GetDate_value()
	Case "PolicyDate"
		Call PolicyDateProc()
	Case "ShowDataPolicy"
		Call InsShowDataPolicy()
	Case "ShowDataCertif"
		Call insShowDataCertif()
	Case "DataPolicy"
		Call InsDataPolicyCertif(1)
	Case "DataCertif"
		Call InsDataPolicyCertif(2)
	Case "ShowDataProp"
		Call InsShowDataProp()
	Case "ShowDataPropCer"
		Call InsShowDataPropCer()
	Case "ShowDataPropCot"
		Call InsShowDataPropCot()
	Case "ShowDataProduct"
		Call ShowDataProduct()
	Case "ShowDataProduct2"
		Call ShowDataProduct2()
End Select
Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
mclsValues = Nothing


%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("showdefvalues")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





