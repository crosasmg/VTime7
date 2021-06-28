<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Variable auxiliar para pase de valores del encabezado al folder
Dim mstrString As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMantCashBank As Object


'% insValMantCashBank: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantCashBank() As String
        '--------------------------------------------------------------------------------------------
        Dim mobjAgent As eAgent.Agencie
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MOP633: Asignación de cajas a usuarios
		Case "MOP633"
			mobjAgent = New eAgent.Agencie
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantCashBank = mobjAgent.insValMOP633_k(mobjValues.StringToType(Request.Form.Item("valUsercod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			'+ MOP634: Asignación de cajas a usuarios
		Case "MOP634"
			mobjMantCashBank = New eCashBank.User_cashnum
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				With Request
					insValMantCashBank = mobjMantCashBank.insValMOP634("MOP634", .Form.Item("sAction"), mobjValues.StringToType(.Form.Item("tcnCashNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valUser"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valCashSup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valHeadSup"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatus"), mobjValues.StringToType(.Form.Item("ValOfficeAgen"), eFunctions.Values.eTypeData.etdDouble))
				End With
			End If
                mobjMantCashBank = Nothing
		Case "MOP702"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantCashBank = New eCashBank.Cash_conclass
				With Request
					insValMantCashBank = mobjMantCashBank.ValidateMOP702_k("MOP702", mobjValues.StringToType(.Form.Item("cbeClass_concept"), eFunctions.Values.eTypeData.etdDouble))
					Session("nClass_concept") = .Form.Item("cbeClass_concept")
				End With
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					mobjMantCashBank = New eCashBank.Cash_conclass
					With Request
						insValMantCashBank = mobjMantCashBank.ValidateMOP702("MOP702", .QueryString("Action"), mobjValues.StringToType(Session("nClass_concept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatregt"))
					End With
				Else
					insValMantCashBank = vbNullString
				End If
			End If
			
		Case "MOP699"
			mobjMantCashBank = New eCashBank.cash_concepts
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantCashBank = mobjMantCashBank.insValMOP699_k(Session("nUsercode"), .Form.Item("cbeCompany"), .QueryString("sCodispl"))
					
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantCashBank = mobjMantCashBank.insValMOP699(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), Session("nCompany"), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cboDescript"), .Form.Item("cboStatregt"))
					End If
				End If
			End With
			
		Case "MOP711"
			mobjMantCashBank = New eCashBank.pay_ord_concepts
			
			With Request
				
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					Session("nCompany") = .Form.Item("cbeCompany")
					insValMantCashBank = mobjMantCashBank.insValMOP711_k(.QueryString("sCodispl"), Session("nCompany"))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantCashBank = mobjMantCashBank.insValMOP711(.QueryString("sCodispl"), .QueryString("Action"), Session("nCompany"), mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatregt"))
					End If
				End If
			End With
			
			'+ MOP822 : Validación de las condiciones de la fecha de valorización
		Case "MOP822"
			mobjMantCashBank = New eCashBank.Valdatconditions
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					insValMantCashBank = mobjMantCashBank.valMOP822_K(.QueryString("Action"), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valDocTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valDefaultDat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valChangesDat"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End With
			
			
		Case Else
			insValMantCashBank = "insValMantCashBank: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	
End Function

'% insPostMantCashBank: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantCashBank() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim mobjAgent As eAgent.Agencie
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MOP633: Conceptos de entrada de dinero en caja
		Case "MOP633"
			mobjAgent = New eAgent.Agencie
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrString = "&nOffice=" & mobjValues.StringToType(.Form.Item("cbeOffice"), eFunctions.Values.eTypeData.etdDouble, True)
					Session("nUser") = mobjValues.StringToType(.Form.Item("valUsercod"), eFunctions.Values.eTypeData.etdDouble)
					lblnPost = True
				Else
					If Request.Form.Item("hddOffice") <> vbNullString Then
						lblnPost = mobjAgent.insUpdUser_Office(Session("nUser"), Request.Form.Item("hddSel"), Request.Form.Item("hddOffice"), Session("nUsercode"))
					Else
						lblnPost = True
					End If
				End If
			End With
			mobjAgent = Nothing
			'+ MOP634: Asignación de cajas a usuarios
		Case "MOP634"
                If Request.QueryString.Item("WindowType") = "PopUp" Then
                    mobjMantCashBank = New eCashBank.User_cashnum
                    
                    With Request
                        lblnPost = mobjMantCashBank.insPostMOP634(Request.Form.Item("sAction"),
                                                                  mobjValues.StringToType(Request.Form.Item("tcnCashNum"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Request.Form.Item("valUser"), eFunctions.Values.eTypeData.etdDouble),
                                                                  Request.Form.Item("cbeStatus"),
                                                                  mobjValues.StringToType(Request.Form.Item("valCashSup"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Request.Form.Item("valHeadSup"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble),
                                                                  mobjValues.StringToType(Request.Form.Item("ValOfficeAgen"), eFunctions.Values.eTypeData.etdDouble))
                    End With
				
                    If lblnPost Then
                        '+ Si se está procesando un registro con el mismo usuario que está ejecutando la aplicación, se procede a modificar la variable de session.
                        If mobjValues.StringToType(Request.Form.Item("valUser"), eFunctions.Values.eTypeData.etdDouble) = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble) Then
                            '+ Si el estado del registro es activo
                            If Request.Form.Item("cbeStatus") = "1" Then
                                Session("nCashNum") = mobjValues.StringToType(Request.Form.Item("tcnCashNum"), eFunctions.Values.eTypeData.etdDouble)
                            Else
                                Session("nCashNum") = eRemoteDB.Constants.intNull
                            End If
                        End If
                    End If
                    mobjMantCashBank = Nothing
                Else
                    lblnPost = True
                End If
			
            Case "MOP702"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mobjMantCashBank.insPostMOP702(False, .QueryString("Action"), mobjValues.StringToType(Session("nClass_concept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
                '+MOP699: Conceptos de entrada de dinero en caja
            Case "MOP699"
                mobjMantCashBank = New eCashBank.cash_concepts
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        'mstrString = "&nCompany="  & .Form("cbeCompany")                    
                        Session("nCompany") = .Form.Item("cbeCompany")
                        lblnPost = True
                    Else
					
					
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjMantCashBank.insPostMOP699(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("nCompany"), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cboStatregt"))
                        Else
                            lblnPost = True
                        End If
                    End If
                End With
			
			
                '+MOP711: 
            Case "MOP711"
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    lblnPost = True
                Else
                    With Request
                        mobjMantCashBank = New eCashBank.pay_ord_concepts
					
                        lblnPost = mobjMantCashBank.insPostMOP711(CDbl(.QueryString.Item("nZone")) = 1, .QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Session("nCompany"), mobjValues.StringToType(.Form.Item("cbeConcept"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("cbestatregt"))
                    End With
                End If
			
			
			
                '+MOP822: Actualización de las condiciones de la fecha de valorización
            Case "MOP822"
                With Request
                    If .QueryString.Item("WindowType") = "PopUp" Then
                        mobjMantCashBank = New eCashBank.Valdatconditions
					
                        lblnPost = mobjMantCashBank.insPostMOP822(.QueryString("Action"), mobjValues.StringToType(.Form.Item("hddId"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valConcept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valDocTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valDefaultDat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valChangesDat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
                    Else
                        lblnPost = True
                    End If
                End With
			
			
        End Select
	
	insPostMantCashBank = lblnPost
End Function

</script>
<%Response.Expires = 0
mstrCommand = "&sModule=Maintenance&sProject=MantCashBank&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>



	
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
function CancelErrors(){self.history.go(-1)}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%mstrString = ""

mobjValues = New eFunctions.Values




'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantCashBank
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If


If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantCashBankError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantCashBank Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>insReloadTop(true);</SCRIPT>")
                    End If
                Else
                    If Request.QueryString.Item("nZone") = "1" Then
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & """;</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
                        End If
                    Else
                        Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
                    End If
                End If
            Else                
                '+ Se recarga la página que invocó la PopUp
                Select Case Request.QueryString.Item("sCodispl")
                    Case "MOP634"
                        If Request.Form.Item("sCodisplReload") = vbNullString Then
                            Response.Write("<SCRIPT>top.opener.document.location.href='MOP634_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
                        Else
                            Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='MOP634_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
                        End If
                        
                    Case "MOP702"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MOP702.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302 '</SCRIPT>")
                    Case "MOP711"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MOP711.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MOP699"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MOP699.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
                    Case "MOP822"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MOP822_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case Else
                        Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
                End Select
		End If
	End If
End If
mobjValues = Nothing
mobjMantCashBank = Nothing
%>
</BODY>
</HTML>




