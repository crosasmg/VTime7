<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values
Dim mstrErrors As Object
Dim mclsClient As eClient.Client_Trans
Dim mclsClientC As eClient.Client
Dim mstrQueryString As String

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim lstrClient As Object
Dim lstrQString As String


'% insvalClient: Se realizan las validaciones masivas de la forma	
'--------------------------------------------------------------------------------------------
Function insvalClient() As Object
	Dim lintCount As Integer
	Dim lblnSelected As Integer
	Dim lblnIsLast as Boolean
	'--------------------------------------------------------------------------------------------
	Dim lobjClient As eClient.Client_Trans
	
	lobjClient = New eClient.Client_Trans
	Dim lobjDoc_req_cli As eClient.Doc_req_cli
	Select Case Request.QueryString.Item("sCodispl")
		'+ Cambio de Código de un Cliente	
		Case "BC005"
			If Request.QueryString.Item("nZone") = "1" Then
				insvalClient = lobjClient.insValHeaderBC005("BC005", CInt(Request.Form.Item("optAct")), Request.Form.Item("dtcClient"), Request.Form.Item("dtcClient_Digit"))
			Else
				If Request.QueryString.Item("WindowType") = "PopUp" Then
					'+ Si es el primer QueryString se encuentra sin contenido, se realizan las  validaciones puntuales.
					If Request.QueryString.Item("sAuxClient") = vbNullString Then
						insvalClient = lobjClient.insValFolderBC005("BC005", Session("OptAction"), Request.Form.Item("tctNewCode"), Request.Form.Item("tctNewCode_Digit"), "", Session("sCodeClient"))
					End If
					'+ Se recorre el QueryString en busca para realizar las validaciones correspondientes
					If Not IsNothing(Request.QueryString.Item("sAuxClient")) Then
						For	Each lstrClient In Request.QueryString.Item("sAuxClient").ToString.Split(",")
							insvalClient = lobjClient.insValFolderBC005("BC005", Session("OptAction"), Request.Form.Item("tctNewCode"), Request.Form.Item("tctNewCode_Digit"), lstrClient, Session("sCodeClient"))
							If insvalClient <> vbNullString Then
								Exit For
							End If
						Next lstrClient
					End If
				End If
			End If
			
			'+ BC006: Cambio de Cliente de una Póliza
		Case "BC006"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				insvalClient = mclsClientC.insValBC006_k("BC006", mclsValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mclsValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble), mclsValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble))
			Else
			    lblnSelected = 0
				If Request.Form.Getvalues("tcnAuxRole").Length  > 0 Then
				For lintCount = 1 To Request.Form.Getvalues("tcnAuxRole").Length 
                        lblnIsLast = 	Request.Form.Getvalues("tcnAuxRole").Length = lintCount 		
						

						If CDbl(Request.Form.GetValues("chkAuxSel").GetValue(lintCount - 1)) = 1  Then
							lblnSelected = 1
						End If

						insvalClient = mclsClientC.insValBC006("BC006", iif (lblnIsLast, lblnSelected,1), Request.Form.Item("gmtClient"), Request.Form.GetValues("tcnAuxClient").GetValue(lintCount - 1))						
                		Next 
				End If
				
				lintCount = Nothing
				lblnSelected = Nothing
			End If
			
			'+ BCC003: Consunta de Intermediarios
		Case "BCC003"
			Session("insvalClient") = ""
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				'insvalClient = mclsClientC.insValBCC003_K("BCC003", Request.QueryString.Item("nMainAction"), Request.Form.Item("tctclient"), mclsValues.StringToDate(Request.Form.Item("tcdEffecdate")))
				Session("insvalClient") = insvalClient
			Else
				insvalClient = True
			End If
			
			'+ BCC001: Busqueda de Clientes		
		Case "BCC001"
			If CDbl(Request.QueryString.Item("nZone")) = 2 Then
				mclsClientC = New eClient.Client
				insvalClient = mclsClientC.insValBCC001(mclsValues.StringToType(Request.Form.Item("cbePerson_typ"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctClient"), Request.Form.Item("tctCliename"), Request.Form.Item("tctLastname"), Request.Form.Item("tctLastname2"), mclsValues.StringToDate(Request.Form.Item("tcdBirthdat")), Request.Form.Item("cboSexclien"))
				
				
				Session("insvalClient") = vbNullString
			Else
				insvalClient = True
			End If
			
			'+ BCL805: Cambio de estado del documento
		Case "BCL805"
			lobjDoc_req_cli = New eClient.Doc_req_cli
			
			insvalClient = lobjDoc_req_cli.InsValBCL805(mclsValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
			
		Case Else
			insvalClient = "insvalClient: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostClient() As Boolean
	Dim mstrAlert As String
	Dim lintCount As Integer
	Dim lblnSelected As Byte
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim mclsClientSeq As eClient.Client_Trans
	Dim lobjClient As eClient.Client_Trans
	Dim lclsClient As eClient.Client
	Dim lstrDigit As String
	
	lobjClient = New eClient.Client_Trans
	mclsClientSeq = New eClient.Client_Trans
	lblnPost = False
	
	Dim mobjErrors As eGeneral.GeneralFunction
	Dim lobjDoc_req_cli_p As eClient.Doc_req_cli
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ Cambio de Código de un Cliente	
		Case "BC005"
            With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&sClient=" & Request.Form.Item("dtcClient")
					Session("sCodeClient") = Request.Form.Item("dtcClient")
					Session("OptAction") = Request.Form.Item("optAct")
					lblnPost = True
				Else
					lclsClient = New eClient.Client
					If Request.QueryString.Item("WindowType") <> "PopUp" Then
						
						'+ Se recorre la colección creada en el campo oculto correspondiente a Clientes.
                            If Not IsNothing(Request.Form.Item("hddClient")) Then
                                For Each lstrClient In Request.Form.Item("hddClient").ToString.Split(",")
                                    lstrDigit = lclsClient.GetRUT(Session("sCodeClient"))
                                    lblnPost = lobjClient.insPostFolderBC005(Session("sCodeClient"), lstrDigit, lstrClient, Session("OptAction"), Session("nUserCode"))
                                Next lstrClient
                            End If
						
						If lblnPost Then
							
							mobjErrors = New eGeneral.GeneralFunction
							
							If CStr(Session("OptAction")) = "1" Then
								mstrAlert = mobjErrors.insLoadMessage(55932)
							Else
								mstrAlert = mobjErrors.insLoadMessage(55933)
							End If
							Response.Write("<SCRIPT>alert (""" & mstrAlert & """);</" & "Script>")
							mobjErrors = Nothing
						End If
					Else
						lblnPost = True
						If Session("OptAction") = 1 Then
							Session("ButtomAdd") = False
						End If
					End If
				End If
			End With
			
			'+ BC006: Cambio de Cliente de una Póliza
		Case "BC006"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
				Session("nBranch") = mclsValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
				Session("nProduct") = mclsValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
				Session("nPolicy") = mclsValues.StringToType(Request.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble)
				Session("nCertif") = mclsValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble)
				Response.Write("<SCRIPT>self.history.go(-1);top.fraFolder.document.location=""" & Request.QueryString.Item("sCodispl") & ".aspx"";</" & "Script>")
			Else
				If (Request.Form.GetValues("tcnAuxRole").Length) > 0 Then
                        For lintCount = 1 To Request.Form.GetValues("tcnAuxRole").Length 
                            If CDbl(Request.Form.GetValues("chkAuxSel").GetValue(lintCount - 1)) = 1 Then
                                lblnSelected = 1
								lblnPost = mclsClientC.insPostBC006(Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Request.Form.GetValues("tcnAuxClient").GetValue(lintCount - 1), CInt(Request.Form.GetValues("tcnAuxRoles").GetValue(lintCount - 1)), Session("nUserCode"), Request.Form.Item("gmtClient"), lblnSelected)

							Else
                                lblnSelected = 0
                            End If
                        Next
				End If
				lintCount = Nothing
				lblnSelected = Nothing
			End If
			
			'+ BCC003: Consunta de Intermediarios			
		Case "BCC003"
			With Request
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("scliename") = mclsClientC.scliename
					Session("sTypeCompany") = 1
					Session("optPolicy") = Request.Form.Item("optPolicy")
					Session("tctClient") = Request.Form.Item("tctclient")
					If CShort(Request.Form.Item("cbeRole")) = 0 Then
						Session("nRole") = eRemoteDB.Constants.intNull
					Else
						Session("nRole") = Request.Form.Item("cbeRole")
					End If
					Session("tcdEffecdate") = Request.Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					lblnPost = True
				End If
			End With
			
			'+ BCC001: Busqueda de Clientes		
		Case "BCC001"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				lblnPost = True
			Else
				lblnPost = True
			End If
			
			'+ BCL805: Cambio de estado del documento
		Case "BCL805"
			lobjDoc_req_cli_p = New eClient.Doc_req_cli
			lblnPost = lobjDoc_req_cli_p.InsPostBCL805(mclsValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"))
			
			If lobjDoc_req_cli_p.nCount = 0 Then
				Response.Write("<SCRIPT>alert('No existen documentos para desistir a la fecha indicada')</" & "Script>")
			Else
				Response.Write("<SCRIPT>alert('Se desistieron " & lobjDoc_req_cli_p.nCount & " documentos')</" & "Script>")
			End If
			
	End Select
	insPostClient = lblnPost
	mclsClientSeq = Nothing
End Function

</script>
<%Response.Expires = 0

mclsValues = New eFunctions.Values
mstrCommand = "&sModule=Client&sProject=Client&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>



	
	<%=mclsValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 3 $|$$Date: 7/11/03 17:08 $"
</SCRIPT>
</HEAD>
<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//% CancelErrors: se controla la acción Cancelar 
//-------------------------------------------------------------------------------------------
function CancelErrors(){
//-------------------------------------------------------------------------------------------
	self.history.go(-1)
}

//% NewLocation: se recalcula el URL de la página
//-------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
mclsClient = New eClient.Client_Trans
mclsClientC = New eClient.Client

'+ Si no se han validado los campos de la página
'	If Request.Form("sCodisplReload") = vbNullString Then	
'		mstrErrors = insvalClient
'	End If

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalClient
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,'ClientErrors',660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mclsValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostClient() Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.document.location.reload();</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("sCodispl") <> "OP004" And Request.QueryString.Item("sCodispl") <> "BCL805" Then
					
					Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
				End If
			End If
			'+ Se mueve automaticamente a la siguiente página
		Else
			Select Case Request.QueryString.Item("sCodispl")
				Case "BCC001"
					Response.Write("<SCRIPT>top.opener.document.location.href='BCC001_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&tctClient=" & Server.URLEncode(Request.Form.Item("tctClient")) & "&tctCliename=" & Server.URLEncode(Request.Form.Item("tctCliename")) & "&tctLastName=" & Server.URLEncode(Request.Form.Item("tctLastName")) & "&tctLastName2=" & Server.URLEncode(Request.Form.Item("tctLastName2")) & "&tcdBirthdat=" & Server.URLEncode(Request.Form.Item("tcdBirthdat")) & "&cboSexclien=" & Request.Form.Item("cboSexclien") & "&cbePerson_typ=" & Request.Form.Item("cbePerson_typ") & "&continue=" & Server.URLEncode("S") & "&nFirstRecord=1&nLastRecord=20'</SCRIPT>")
				Case "BC005"
					lstrQString = "&"
					If Not IsNothing(Request.QueryString.Item("sAuxClient")) Then
						For	Each lstrClient In Request.QueryString.Item("sAuxClient").ToString.Split(",")
							If lstrClient <> vbNullString And lstrClient <> Session("sDelCLient") Then
								lstrQString = lstrQString & "sAuxClient" & "=" & lstrClient & "&"
							End If
						Next lstrClient
					End If
					lstrClient = Request.Form.Item("tctNewCode")
                    
					lstrQString = lstrQString & "sAuxClient=" & lstrClient & "&"
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & lstrQString & "';</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();</SCRIPT>")
						Response.Write("<SCRIPT>top.opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & lstrQString & "';</SCRIPT>")
					End If
			End Select
		End If
	Else
		Response.Write("<SCRIPT>alert('Hubo un error al realizar actualización');</SCRIPT>")
	End If
End If

mclsValues = Nothing
mclsClient = Nothing
%>
</BODY>
</HTML>




º
