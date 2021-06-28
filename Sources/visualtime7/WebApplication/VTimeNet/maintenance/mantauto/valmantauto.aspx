<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mstrQueryString As String

Dim mstrErrors As Object
Dim mobjValues As eFunctions.Values
Dim mobjMantAuto As Object

'% insValMantAuto: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantAuto() As Object
	'--------------------------------------------------------------------------------------------
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ MAU001: Tabla de vehículos        
		Case "MAU001"
			With Request
				mobjMantAuto = New eBranches.Tab_au_veh
				If .QueryString.Item("WindowType") <> "PopUp" Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantAuto = mobjMantAuto.InsValMAU001_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valVehcode"))
					Else
						If .QueryString.Item("nMainAction") <> "401" Then
							insValMantAuto = mobjMantAuto.InsValMAU001(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeVehtype"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeVehbrand"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnVehplace"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnVehpma"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nCountTab_au_val"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
						End If
					End If
				Else
					insValMantAuto = mobjMantAuto.InsValMAU001Upd(.QueryString("sCodispl"), .QueryString("Action"), .QueryString("sInGrid"), .QueryString("sVehcode"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ MAU101: Deducibles Permitidos
		Case "MAU101"
			mobjMantAuto = New eBranches.Deduc_auto
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantAuto = mobjMantAuto.insValMAU101_K("MAU101", .QueryString("nMainAction"), mobjValues.StringToType(.Form.Item("cbeType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantAuto = mobjMantAuto.insValMAU101("MAU101", .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnDeduc"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nVehType"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ BV001: Base de datos automóvil        
		Case "BV001"
			mobjMantAuto = New ePolicy.Auto_db
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = mstrQueryString & "&sMotor=" & .Form.Item("tctMotor")
					mstrQueryString = mstrQueryString & "&sChasis=" & .Form.Item("tctChassis")
					mstrQueryString = mstrQueryString & "&sRegist=" & .Form.Item("tctRegister")
					mstrQueryString = mstrQueryString & "&sLicense_ty=" & .Form.Item("cbeLicense_ty")
					mstrQueryString = mstrQueryString & "&sDigit=" & .Form.Item("tctDigit")
					mstrQueryString = mstrQueryString & "&nLic_special=" & .Form.Item("cbeNlic_special")
					
					insValMantAuto = mobjMantAuto.insValBV001_k("BV001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRegister"), .Form.Item("cbeLicense_ty"), .Form.Item("tctMotor"), .Form.Item("tctChassis"))
				Else
					insValMantAuto = mobjMantAuto.insValBV001("BV001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctVehown"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valVehCode"), .Form.Item("tctColor"), mobjValues.StringToType(.Form.Item("cboVehstate"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ MAU551: Tabla de series de patentes        
		Case "MAU551"
			If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				mobjMantAuto = New eBranches.Series
				With Request
					insValMantAuto = mobjMantAuto.InsValMAU551_k(.QueryString("sCodispl"), .QueryString("Action"), .Form.Item("tctSerie"), mobjValues.StringToType(.Form.Item("tcnDigit7"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit6"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit5"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				insValMantAuto = ""
			End If
			
			'+ MAU571: Tarifa de automóvil
		Case "MAU571"
			mobjMantAuto = New ePolicy.Tar_auto
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantAuto = mobjMantAuto.insValMAU571_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valVehcode"))
					mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency") & "&sVehcode=" & Request.Form.Item("valVehcode") & "&optTyp_var=" & Request.Form.Item("optTyp_var") & "&nRateAddSub=" & Request.Form.Item("tctRateAddSub") & "&nId=" & Request.Form.Item("hddnId")
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency") & "&sVehcode=" & Request.Form.Item("valVehcode") & "&optTyp_var=" & Request.Form.Item("optTyp_var") & "&nRateAddSub=" & Request.Form.Item("tctRateAddSub") & "&nId=" & Request.Form.Item("hddnId")
						insValMantAuto = mobjMantAuto.insValMAU571(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("hddsVehcode"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_fix"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insValMantAuto = vbNullString
					End If
				End If
			End With
			
			'+ MAU587: Tabla para descuento por no siniestralidad
		Case "MAU587"
			mobjMantAuto = New eBranches.Au_bon_mod
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantAuto = mobjMantAuto.insValMAU587_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency")
						insValMantAuto = mobjMantAuto.insValMAU587(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInimonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndmonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_claim"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insValMantAuto = vbNullString
					End If
				End If
			End With
			
                '+ MSO009: Tabla para descuento por no siniestralidad
            Case "MSO009"
                mobjMantAuto = New eBranches.Au_bon_mod
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValMantAuto = mobjMantAuto.insValMSO009_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCurrency"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            mstrQueryString = "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency")
                            insValMantAuto = mobjMantAuto.insValMSO009(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInimonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndmonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_claim"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValMantAuto = vbNullString
                        End If
                    End If
                End With
			'+ AU557: Cambio de Patente
		Case "AU557"
			mobjMantAuto = New ePolicy.Auto_db
			With Request
				
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = mstrQueryString & "&sRegist=" & .Form.Item("tctRegister")
					mstrQueryString = mstrQueryString & "&sLicense_ty=" & .Form.Item("cbeLicense_ty")
					mstrQueryString = mstrQueryString & "&sRegistOld=" & .Form.Item("tctRegister")
					mstrQueryString = mstrQueryString & "&sLicense_tyOld=" & .Form.Item("cbeLicense_ty")
					
					insValMantAuto = mobjMantAuto.insValAU557_K(.QueryString("sCodispl"), .Form.Item("tctRegister"), .Form.Item("cbeLicense_ty"))
				Else
					mstrQueryString = mstrQueryString & "&sRegist=" & .Form.Item("tctRegister")
					mstrQueryString = mstrQueryString & "&sLicense_ty=" & .Form.Item("cbeLicense_ty")
					mstrQueryString = mstrQueryString & "&sLicense_tyOld=" & .QueryString.Item("sRegistOld")
					mstrQueryString = mstrQueryString & "&sLicense_tyOld=" & .QueryString.Item("sLicense_tyOld")
					
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
						insValMantAuto = mobjMantAuto.insValAU557(.QueryString("sCodispl"), .Form.Item("tctRegister"), .Form.Item("cbeLicense_ty"))
					Else
						insValMantAuto = True
					End If
				End If
			End With
			'+ MSO008: Tarifa de prima de SOAP
		Case "MSO008"
			mobjMantAuto = New eBranches.Tar_prem_soap
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantAuto = mobjMantAuto.insValMSO008_k(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
					mstrQueryString = "&dEffecdate=" & Request.Form.Item("tcdEffecdate") 
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nVehType=" & Request.Form.Item("cbeVehType")
                            insValMantAuto = mobjMantAuto.insValMSO008(Request.QueryString.Item("Action"), .QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeVehType"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble, True))
                        Else
                            insValMantAuto = vbNullString
					End If
				End If
                End With
                
                '+MSO6000: Localidades para tarifa SOAT
            Case "MSO6000"
                With Request
                    mobjMantAuto = New eBranches.LocateTar_Soat
				
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValMantAuto = mobjMantAuto.InsValMSO6000_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValMantAuto = mobjMantAuto.InsValMSO6000(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valLocal_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnZipCode_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnZipCode_End"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
                
                '+ MSO6001: Tarifa de SOAT
            Case "MSO6001"
                With Request
                    mobjMantAuto = New eBranches.Tar_SOAT
                
                    If .QueryString("nZone") = 1 Then

                        insValMantAuto = mobjMantAuto.InsValMSO6001_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form("cbecurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        If .QueryString("WindowType") = "PopUp" Then
                            insValMantAuto = mobjMantAuto.InsValMSO6001(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTypeCalculate"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valGroupAuto"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnTarif"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valclass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valLocateSoat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnpremiumn"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With

                '+ Tarifa SOAT                                                 
            Case "MSO8500"
                With Request
                    mobjMantAuto = New eBranches.Tar_SOAT
                
                    If .QueryString("nZone") = 1 Then

                        insValMantAuto = mobjMantAuto.InsValMSO8500_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form("cbecurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdInteger))
                    Else
                        If .QueryString("WindowType") = "PopUp" Then
                            insValMantAuto = mobjMantAuto.InsValMSO8500(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valGroupAuto"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valclass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTrademarks"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valMovement"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTypepremiun"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnPremium"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTypeperson"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTypeCalculate"), eFunctions.Values.eTypeData.etdInteger, True))
                        End If
                    End If
                End With
                
                '+ MSO6003: Frecuencias de Pago permitidas SOAT
            Case "MSO6003"
                mobjMantAuto = New eBranches.FPay_AllowClass
                If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    With Request
                    
                        If .QueryString("nZone") = 1 Then
                            mstrQueryString = "&nBranch=" & Request.Form("cbeBranch") & "&nProduct=" & Request.Form("valProduct") & "&dEffecdate=" & Request.Form("tcdEffecdate")
                            insValMantAuto = mobjMantAuto.InsValMSO6003_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                        
                            If Request.QueryString("nMainAction") <> 401 Then
                                mstrQueryString = "&Action=Add"
                            Else
                                mstrQueryString = "&Action=Del"
                            End If
                            mstrQueryString = mstrQueryString & "&nPayFreq=" & .Form("cbePayFreq") & "&nSOATClass=" & .Form("cbeSOATClass") & "&nBranch=" & .QueryString("nBranch") & "&nProduct=" & .QueryString("nProduct") & "&dEffecdate=" & .QueryString("dEffecDate")
                            
                            insValMantAuto = mobjMantAuto.InsValMSO6003(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), "Add", mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("cbePayFreq"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("cbeSOATClass"), eFunctions.Values.eTypeData.etdInteger))
                        End If
                    End With
                End If
                
                
		Case Else
			insValMantAuto = "insValMantAuto: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantAuto: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantAuto() As Boolean
	Dim lstrDigit As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim nMain As Object
	
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		'+ MAU001: Tabla de vehículos
		Case "MAU001"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") <> "PopUp" Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&sVehcode=" & .Form.Item("valVehcode")
						If .QueryString.Item("nMainAction") = "301" Then
							lblnPost = mobjMantAuto.InsPostMAU001(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valVehcode"), "1", vbNullString, vbNullString, 1, eRemoteDB.Constants.intNull, 0, 0, 2, Session("nUsercode"))
						End If
					Else
						If .QueryString.Item("nMainAction") = "301" Then
							nMain = 302
						Else
							nMain = .QueryString.Item("nMainAction")
						End If
						lblnPost = mobjMantAuto.InsPostMAU001(mobjValues.StringToType(nMain, eFunctions.Values.eTypeData.etdDouble), .QueryString("sVehcode"), .Form.Item("cbeStatregt"), .Form.Item("tctDescript"), .Form.Item("tctVehmodel"), mobjValues.StringToType(.Form.Item("cbeVehtype"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeVehbrand"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnVehplace"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnVehpma"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("chkNational"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					End If
				Else
					mstrQueryString = "&sVehcode=" & .QueryString.Item("sVehcode") & "&sInGrid=" & .QueryString.Item("sInGrid")
					lblnPost = mobjMantAuto.InsPostMAU001Upd(.QueryString("Action"), .QueryString("sInGrid"), .QueryString("sVehcode"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
				End If
			End With
			
			'+ MAU101: Deducibles Permitidos
		Case "MAU101"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nVehType=" & .Form.Item("cbeType") & "&dEffecdate=" & .Form.Item("tcdEffecDate")
					lblnPost = True
				Else
					lblnPost = True
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantAuto.insPostMAU101("MAU101", Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnDeduc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nVehType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						mstrQueryString = "&nVehType=" & Request.QueryString.Item("nVehType") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
					End If
				End If
			End With
			
			'+ BV001: Base de datos automóvil        
		Case "BV001"
			With Request
				If IsNothing(.QueryString("sDigit")) Then
					lstrDigit = " "
				Else
					lstrDigit = .QueryString.Item("sDigit")
				End If
				
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					lblnPost = True
				Else
					If CDbl(.QueryString.Item("nMainAction")) <> 401 Then
						
						lblnPost = mobjMantAuto.insPostBV001("BV001", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .QueryString("sLicense_ty"), .QueryString("sRegist"), .QueryString("sChasis"), .QueryString("sMotor"), .Form.Item("tctColor"), .Form.Item("valVehCode"), mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble), "", .Form.Item("tctVehown"), mobjValues.StringToType(.Form.Item("cboVehstate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNoteNum"), eFunctions.Values.eTypeData.etdDouble), lstrDigit, mobjValues.StringToType(.QueryString.Item("nLic_special"), eFunctions.Values.eTypeData.etdDouble, True))
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ MAU551: Tabla de series de patentes        
		Case "MAU551"
			If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				With Request
					mobjMantAuto = New eBranches.Series
					lblnPost = mobjMantAuto.insPostMAU551(.QueryString("Action"), .Form.Item("tctSerie"), mobjValues.StringToType(.Form.Item("tcnDigit7"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit6"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDigit5"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End With
			Else
				lblnPost = True
			End If
			
			'+ MAU571: Tarifa de automóvil
		Case "MAU571"
			With Request
				If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionDuplicate) Then
					If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
						If CDbl(.QueryString.Item("nZone")) = 1 Then
							mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency") & "&sVehcode=" & Request.Form.Item("valVehcode") & "&OptTyp_var=" & Request.Form.Item("optTyp_var") & "&tctRateAddSub=" & Request.Form.Item("tctRateAddSub") & "&nId=" & Request.Form.Item("hddnId")
							lblnPost = True
						Else
                                mstrQueryString = "&nBranch=" & Session("hddnBranch") & "&nProduct=" & Session("hddnProduct") & "&dEffecdate=" & Session("hdddEffecdate") & "&nCurrency=" & Session("hddnCurrency") & "&sVehcode=" & Session("hddsVehcode") & "&OptTyp_var=" & Session("hddsoptTyp_var") & "&tctRateAddSub=" & Session("hddnRateAddSub") & "&nId=" & Session("hddnId")
                                lblnPost = mobjMantAuto.insPostMAU571Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("hddsoptTyp_var"), mobjValues.StringToType(Session("hddnRateAddSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), Session("hddsVehcode"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPrem_fix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						End If
					Else
						lblnPost = True
					End If
				Else
                        lblnPost = mobjMantAuto.insPostDuplicateMAU571(mobjValues.StringToType(.Form.Item("cbeBranch_aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct_aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCurrency_aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate_aux"), eFunctions.Values.eTypeData.etdDate), .Form.Item("valVehcode_aux"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("hddsVehcode"), mobjValues.StringToType(Session("hddnId"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			'+ MAU587: Tabla para aplicación del descuento por no siniestralidad
		Case "MAU587"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency")
					lblnPost = True
				Else
					mstrQueryString = "&nBranch=" & Request.Form.Item("hddnBranch") & "&nProduct=" & Request.Form.Item("hddnProduct") & "&dEffecdate=" & Request.Form.Item("hdddEffecdate") & "&nCurrency=" & Request.Form.Item("hddnCurrency")
					
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantAuto.insPostMAU587Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInimonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndmonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_claim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
					
					lblnPost = True
				End If
			End With
			
                '+ MSO009: Tabla para aplicación del descuento por no siniestralidad
            Case "MSO009"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate") & "&nCurrency=" & Request.Form.Item("valCurrency")
                        lblnPost = True
                    Else
                        mstrQueryString = "&nBranch=" & Request.Form.Item("hddnBranch") & "&nProduct=" & Request.Form.Item("hddnProduct") & "&dEffecdate=" & Request.Form.Item("hdddEffecdate") & "&nCurrency=" & Request.Form.Item("hddnCurrency")
					
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjMantAuto.insPostMSO009Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("hddnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddnId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInimonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndmonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount_claim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            lblnPost = True
                        End If
					
                        lblnPost = True
                    End If
                End With
			
			'+ AU557: de cambio de patente
		Case "AU557"
			With Request
				If CDbl(.QueryString.Item("nZone")) <> 1 Then
					lblnPost = mobjMantAuto.insPostAU557(.QueryString("sRegistOld"), .Form.Item("tctRegister"), .QueryString("sLicense_tyOld"), .Form.Item("cbeLicense_ty"), .Form.Item("tctDigit"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
			'+ MSO008: Tarifa de primas de SOAP
		Case "MSO008"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&dEffecdate=" & .Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					mstrQueryString = "&dEffecdate=" & .QueryString.Item("dEffecdate")
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantAuto.insPostMSO008Upd(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeVehType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
	
                '+MSO6000: Localidades para tarifa SOAT
            Case "MSO6000"
                With Request
                    mstrQueryString = mstrQueryString & "&dEffecDate=" & .Form.Item("tcdEffecDate")
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjMantAuto.InsPostMSO6000(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valLocal_Type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnZipCode_Ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnZipCode_End"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With

                '+MSO6001: Tarifa de SOAT
            Case "MSO6001"
                With Request
            
                    mstrQueryString = "&nBranch=" & Request.Form("cbeBranch") & "&nProduct=" & Request.Form("valProduct") & "&dEffecdate=" & Request.Form("tcdEffecdate") & "&nCurrency=" & Request.Form("cbecurrency")
            
                    If .QueryString("WindowType") = "PopUp" Then
                        'lblnPost = mobjMantAuto.InsPostMSO6001((.QueryString("nZone") = 1), .QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTypeCalculate"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valGroupAuto"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnTarif"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valclass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valLocateSoat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnpremiumn"),eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPremium"),eFunctions.Values.eTypeData.etdDouble))
                        lblnPost = mobjMantAuto.InsPostMSO6001((.QueryString("nZone") = 1), .QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valTypeCalculate"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valGroupAuto"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnTarif"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valclass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valLocateSoat"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("tcnpremiumn"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With

                '+MSO8500: Tarifa de SOAT
            Case "MSO8500"
                With Request
                    mobjMantAuto = New eBranches.Tar_Soat
            
                    mstrQueryString = "&nBranch=" & Request.Form("cbeBranch") & "&nProduct=" & Request.Form("valProduct") & "&dEffecdate=" & Request.Form("tcdEffecdate") & "&nCurrency=" & Request.Form("cbecurrency")
            
                    If .QueryString("WindowType") = "PopUp" Then
                        lblnPost = mobjMantAuto.InsPostMSO8500((.QueryString("nZone") = 1), .QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("ncurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valGroupAuto"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valclass"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valTrademarks"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valModel"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("tcnSeats"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("valMovement"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valTypeperson"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valTypepremiun"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("valTypeCalculate"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form("tcnPremium"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
                
                '+ MSO6003: Frecuencias de pago permitidas SOAT
            Case "MSO6003"
                If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    With Request
                        If .QueryString("nZone") = 1 Then
                            lblnPost = True
                        Else
                            If .QueryString("WindowType") = "PopUp" Then
                                lblnPost = mobjMantAuto.InsPostMSO6003(False, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("cbePayFreq"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form("cbeSOATClass"), eFunctions.Values.eTypeData.etdInteger))
                            Else
                                lblnPost = True
                            End If
                        End If
                    End With
                Else
                    lblnPost = True
                End If
                
        End Select
	
	insPostMantAuto = lblnPost
End Function

</script>
<%
Response.Expires = -1
mstrCommand = "&sModule=Maintenance&sProject=MantAuto&sCodisplReload=" & Request.QueryString.Item("sCodispl")

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:45 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



    
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
</HEAD>
<BODY>
<%
mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantAuto
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		'            .Write "ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.QueryString) & """, ""MantAutoError"",660,330);document.location.href='/VTimeNet/common/blank.htm';"
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantAutoError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantAuto Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			
			'+ The page is recharged that invoked the PopUp.  
			'+ Se recarga la página que invocó la PopUp.
			Select Case Request.QueryString.Item("sCodispl")
				Case "MAU551"
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MAU551_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>opener.top.opener.document.location.href='MAU551_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';window.close();</SCRIPT>")
					End If
				Case "MAU571"
					Response.Write("<SCRIPT>top.opener.document.location.href='MAU571.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
				Case "MSO008"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MSO008.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
                    Case "MSO6000"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MSO6000.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & "&nMainAction=" & Request.QueryString("nMainAction") & "&dEffecDate=" & Request.QueryString("dEffecDate") & "' </SCRIPT>")
                    Case "MSO6001"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MSO6001.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & "&nMainAction=" & Request.QueryString("nMainAction") & "&ncurrency=" & Request.QueryString("ncurrency") & "&dEffecDate=" & Request.QueryString("dEffecDate") & "&nBranch=" & Request.QueryString("nBranch") & "&nProduct=" & Request.QueryString("nProduct") & "' </SCRIPT>")
                    Case "MSO6003"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MSO6003.aspx?Reload=" & Request.Form("chkContinue") & "&ReloadAction=" & Request.QueryString("Action") & "&ReloadIndex=" & Request.QueryString("ReloadIndex") & "&sCodispl=" & Request.QueryString("sCodispl") & "&nMainAction=" & Request.QueryString("nMainAction") & "&nBranch=" & Request.QueryString("nBranch") & "&dEffecDate=" & Request.QueryString("dEffecDate") & "&nProduct=" & Request.QueryString("nProduct") & "' </SCRIPT>")
				Case Else
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
			End Select
		End If
	End If
	If Request.QueryString.Item("nMainAction") = "401" Then
		Session("bQuery") = True
	Else
		Session("bQuery") = False
	End If
End If

mobjValues = Nothing
mobjMantAuto = Nothing
%>
</BODY>
</HTML>




