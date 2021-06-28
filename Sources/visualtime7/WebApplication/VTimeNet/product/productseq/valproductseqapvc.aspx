<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eApvc" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de funciones generales
Dim mobjValues As eFunctions.Values

'- Variable para el manejo del querystring
Dim mstrQueryString As String

'- Se define la constante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrErrors As String
Dim mobjProductSeq As Object
Dim mobjClient_req As Object
Dim mstrLocationBC003 As String
'- Contador para uso general    
Dim mintCount As Object

'- Esta variable es para indicar cuando debe pasarse a la siguiente ventana de la secuencia
'- al aceptar.  Para uso de casos particulares.
Dim lstrGoToNext As String

'- Cadena para pase de parametros    
Dim mstrString As String

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	insFinish = True
End Function


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	'--------------------------------------------------------------------------------------------
	insvalSequence = vbNullString
	
	Dim mobjapvc As eApvc.Product_Apvc
	Select Case Request.QueryString.Item("sCodispl")
		
		
		' diseñador de productos  APVC 
		Case "DP200"
			mobjapvc = New eApvc.Product_Apvc
			With Request
				insvalSequence = mobjapvc.insValDP200(session("nBranch"), session("nProduct"), session("dEffecdate"), mobjValues.StringToType(.Form.Item("tctnPrem_min"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPermin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnMonthmin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnMinstay"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPercentnpren"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnPrem_max"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPrem_min"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbencurrency"), eFunctions.Values.eTypeData.etdLong), session("nusercode"), mobjValues.StringToType(.Form.Item("tctnPercentsalary"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnAmountnprem"), eFunctions.Values.eTypeData.etdLong))
				' diseñador de productos  APVC	                                                
				
				
			End With
			mobjapvc = Nothing
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
			
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = True
	
	Dim mobjapvc As eApvc.Product_Apvc
	Select Case Request.QueryString.Item("sCodispl")
		Case "DP200"
			mobjapvc = New eApvc.Product_Apvc
			With Request
				lblnPost = mobjapvc.insPostDP200(1, session("nBranch"), session("nProduct"), session("dEffecdate"), mobjValues.StringToType(.Form.Item("tctnPrem_min"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPermin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnMonthmin"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnMinstay"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPercentnpren"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnPrem_max"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctnPrem_min"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbencurrency"), eFunctions.Values.eTypeData.etdLong), session("nusercode"), mobjValues.StringToType(.Form.Item("tctnPercentsalary"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctnAmountnprem"), eFunctions.Values.eTypeData.etdLong))
			End With
			mobjapvc = Nothing
	End Select
	insPostSequence = lblnPost
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mstrCommand = "sModule=Product&sProject=ProductSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
     <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>



    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 29/06/06 5:41p $|$$Author: Fmendoza $"
</SCRIPT>
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
mstrLocationBC003 = "'/VTimeNet/Common/GoTo.aspx?sCodispl=DP003_K'"

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	session("sErrorTable") = mstrErrors
	session("sForm") = Request.Form.ToString
Else
	session("sErrorTable") = vbNullString
	session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ProductSeqErrors"",660,330);self.document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página. Sólo en caso que sea la ventana DP043
				'+ se mantiene en la página por si se quiere ir a la secuencia de Carac. de vida.
				'+ Se da el mismo tratamiento a DP607
				lstrGoToNext = "Yes"
				If Request.QueryString.Item("sCodispl") = "DP043" Or Request.QueryString.Item("sCodispl") = "DP607" Or (Request.QueryString.Item("sCodispl") = "DP012" And Request.Form.Item("hddMassive") = "2") Or (Request.QueryString.Item("sCodispl") = "DP048" And Request.Form.Item("hddMassive") = "2") Then
					If Request.QueryString.Item("sCodispl") = "DP043" Then
						Response.Write("<SCRIPT>top.frames['fraFolder'].document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&ReloadAction=Update&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>top.frames['fraFolder'].document.location=top.frames['fraFolder'].document.location</SCRIPT>")
					End If
					If Request.Form.Item("hddbWithInformation") <> "True" Then
						lstrGoToNext = "No"
					End If
				End If
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=" & lstrGoToNext & "&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			Else
				If Request.QueryString.Item("sCodispl") <> "DP578A" Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				End If
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "DP009", "DP037", "DP010", "DP061", "DP062", "DP036", "DP809", "DP059", "DP032", "DP041", "DP100", "DP060", "DP828"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
						
					Case "DP008"
						If Request.QueryString.Item("Action") = "Add" Then
							mstrString = "&nDisexprc=" & Request.Form.Item("tcnCode") & "&nOrderApl=" & Request.Form.Item("tcnOrder_apl") & "&sDescript=" & Request.Form.Item("tctDescript") & "&nType=" & Request.Form.Item("cbeType")
						End If
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						
					Case "DP578"
						If Request.QueryString.Item("Action") = "Add" Then
							mstrString = "&nWay_pay=" & Request.Form.Item("cbeWay_pay") & "&nRate_ex=" & Request.Form.Item("tcnRate_ex") & "&nRate_disc=" & Request.Form.Item("tcnRate_disc") & "&sPrem_first=" & Request.Form.Item("sPrem_first") & "&nNull_day=" & Request.Form.Item("tcnNull_day")
						End If
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrString & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						
					Case "DP578A"
						Response.Write("<SCRIPT>top.opener.top.document.location.reload()</SCRIPT>")
						
					Case "DP027"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&cbenModule=" & Request.Form.Item("cbenModule") & "&cbenCover=" & Request.Form.Item("cbenCover") & "'</SCRIPT>")
						
					Case "DP101"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nCover=" & Request.Form.Item("cbenCover") & "&nRole=" & Request.Form.Item("cbenRole") & "&sIllness=" & Request.Form.Item("valIllness") & "&nModulec=" & Request.Form.Item("cbenModulec") & "&nCurrency=" & Request.Form.Item("nCurrency") & "'</SCRIPT>")
						
					Case "DP033"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nCover=" & Request.Form.Item("nCover") & "&nCovergen=" & Request.Form.Item("nCovergen") & "&nModulec=0" & Request.Form.Item("tcnModulec") & "'</SCRIPT>")
						
					Case "DP048"
						Response.Write("<SCRIPT>top.opener.document.location.href='DP048.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sPolitype=" & Request.Form.Item("hddPolitype") & "&sCompon=" & Request.Form.Item("hddCompon") & "'</SCRIPT>")
						
					Case "DP011"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						
					Case "DP042"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sPoliType=" & Request.Form.Item("tctPoliType") & "&sComponent=" & Request.Form.Item("tctComponent") & "'</SCRIPT>")
						
					Case "DP057"
						Response.Write("<SCRIPT>top.opener.document.location.href='DP057.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & mstrString & "'</SCRIPT>")
						
					Case "DP058"
						Response.Write("<SCRIPT>top.opener.document.location.href='DP058.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&mintTariff=" & Request.Form.Item("nTariff") & "&mstrTypeExcl=" & Request.Form.Item("sType_Excl") & "'</SCRIPT>")
						
					Case "DP064"
						Response.Write("<SCRIPT>top.opener.document.location.href='DP064.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
						
					Case "DP705"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nModulec=" & Request.Form.Item("hddnModulec") & "&nCover=" & Request.Form.Item("hddnCover") & "&nRole=" & Request.Form.Item("hddnRoleSel") & "&nExist=" & Request.Form.Item("hddnExist") & "'</SCRIPT>")
					Case "DP080"
						Response.Write("<SCRIPT>top.opener.document.location.href='DP080.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "'</SCRIPT>")
					Case "DP038"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nDefReq=" & Request.Form.Item("hddDefReq") & "'</SCRIPT>")
						
					Case Else
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or session("bQuery") Then
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	Else
		
		'+ Se recarga la página principal de la secuencia            
		If insFinish() Then
			Response.Write("<SCRIPT>top.opener.top.document.location=" & mstrLocationBC003 & ";</SCRIPT>")
		End If
	End If
End If

'mobjProductSeq = Nothing
mobjValues = Nothing


%>
</FORM>
</BODY>
</HTML>





