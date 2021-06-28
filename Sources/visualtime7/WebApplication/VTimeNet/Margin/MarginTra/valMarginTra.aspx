<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eMargin" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

'+ Se define la variable para almacenar el QueryString de los campos que existen en el encabezado de la transacción
Dim mstrQueryString As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMarginTra As Object


'% insvalMarginTra: Se realizan las validaciones de las páginas
'--------------------------------------------------------------------------------------------
Function insvalMarginTra() As String
	'--------------------------------------------------------------------------------------------
	With Request
		Select Case .QueryString.Item("sCodispl")
			'+ MGS001: Tabla de margen de solvencia
			Case "MGS001"
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mobjMarginTra = New eMargin.Margin_master
					insvalMarginTra = mobjMarginTra.insvalMGS001_K(.QueryString("sCodispl"), .QueryString("nMainAction"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTableTyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeClaimClass"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mobjMarginTra = New eMargin.Margin_detail
						insvalMarginTra = mobjMarginTra.insvalMGS001Upd(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("hddTabletyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddSource"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddClaimClass"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdValdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInitialAmoOri"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("valProduct_sModulec"), mobjValues.StringToType(.Form.Item("cbeTyperec"), eFunctions.Values.eTypeData.etdLong))
					Else
						mobjMarginTra = New eMargin.Margin_master
						insvalMarginTra = mobjMarginTra.insvalMGS001(.QueryString("sCodispl"), .Form.GetValues("hddIdRec").Length)
					End If
				End If
				
			Case "MGS002"
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjMarginTra = New eMargin.Margin_Adj
					insvalMarginTra = mobjMarginTra.insvalMGS002Upd(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnAdjustamoori"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			Case "MGSL001"
				mobjMarginTra = New eMargin.Margin_master
				insvalMarginTra = mobjMarginTra.insvalMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
				
			Case "MGSL002"
				mobjMarginTra = New eMargin.Margin_master
				insvalMarginTra = mobjMarginTra.insvalMGSL002(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				
			Case "MGSL003"
				mobjMarginTra = New eMargin.Margin_master
				insvalMarginTra = mobjMarginTra.insvalMGSL003(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate))
				
			Case "MGSL004"
				mobjMarginTra = New eMargin.Margin_master
				insvalMarginTra = mobjMarginTra.insvalMGSL004(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate))
			Case Else
				insvalMarginTra = "insvalMarginTra: Código lógico no encontrado (" & .QueryString.Item("sCodispl") & ")"
		End Select
	End With
End Function

'% inspostMarginTra: Se realizan las actualizaciones de las tablas
'--------------------------------------------------------------------------------------------
Function inspostMarginTra() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	With Request
		Select Case .QueryString.Item("sCodispl")
			'+ MGS001: Tabla de margen de solvencia
			Case "MGS001"
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nInsur_area=" & Session("nInsur_area") & "&nTableTyp=" & .Form.Item("cbeTableTyp") & "&nSource=" & .Form.Item("cbeSource") & "&nClaimClass=" & .Form.Item("cbeClaimClass") & "&dInitDate=" & .Form.Item("tcdInitDate") & "&dEndDate=" & .Form.Item("tcdEndDate") & "&nIdTable=" & Session("nIdTable")
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMarginTra.inspostMGS001(.QueryString("Action"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddIdTable"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddIdRec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddTabletyp"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddSource"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddClaimClass"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInitialAmoOri"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdValdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTyperec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSVSClass"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
						
						mstrQueryString = "&nInsur_area=" & Session("nInsur_area") & "&nTableTyp=" & .Form.Item("hddTabletyp") & "&nSource=" & .Form.Item("hddSource") & "&nClaimClass=" & .Form.Item("hddClaimClass") & "&dInitDate=" & .Form.Item("hddInitDate") & "&dEndDate=" & .Form.Item("hddEndDate") & "&nIdTable=" & mobjMarginTra.nIdTable
					End If
				End If
				
			Case "MGS002"
				If .QueryString.Item("WindowType") = "PopUp" Then
					
					lblnPost = mobjMarginTra.inspostMGS002(.QueryString("Action"), mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("hddIdTable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddIdrec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMovement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAdjustamoori"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), mobjValues.StringToType(.Form.Item("hddValDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					
					mstrQueryString = "&nInsur_area=" & Session("nInsur_area") & "&dInitDate=" & .Form.Item("hddInitDate") & "&nIdTable=" & .Form.Item("hddIdTable") & "&nIdrec=" & .Form.Item("hddIdrec") & "&dValDate=" & .Form.Item("hddValDate") & "&nTableTyp=" & Request.QueryString.Item("nTableTyp") & "&nSource=" & Request.QueryString.Item("nSource") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&nTyperec=" & Request.QueryString.Item("nTyperec") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover")
					
				End If
			Case "MGSL001"
				mobjMarginTra = New eMargin.Margin_master
				lblnPost = mobjMarginTra.inspostMGSL001(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
			Case "MGSL002"
				mobjMarginTra = New eMargin.Margin_master
				lblnPost = mobjMarginTra.inspostMGSL002(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
				
			Case "MGSL003"
				mobjMarginTra = New eMargin.Margin_master
				lblnPost = mobjMarginTra.inspostMGSL003(mobjValues.StringToType(.Form.Item("tcdProcessDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
				
			Case "MGSL004"
				mobjMarginTra = New eMargin.Margin_master
				lblnPost = mobjMarginTra.inspostMGSL004(mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEndDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nusercode"), eFunctions.Values.eTypeData.etdDouble))
		End Select
	End With
	
	inspostMarginTra = lblnPost
End Function

'% insFinish: se realizan las acciones al finalizar la transacción
'--------------------------------------------------------------------------------------------
Public Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	insFinish = True
	With Request
		Select Case .QueryString.Item("sCodispl")
			Case "MGS001"
				mobjMarginTra = New eMargin.Margin_detail
				insFinish = mobjMarginTra.Update_Stadet(mobjValues.StringToType(Session("nInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nIdTable"), eFunctions.Values.eTypeData.etdDouble, True), Session("nUsercode"))
				
		End Select
	End With
End Function

</script>
<%Response.Expires = -1
mstrCommand = "&sModule=Margin&sProject=MarginTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 18/12/03 16:58 $|$$Author: Nvaplat15 $"
</SCRIPT>
</HEAD>
<BODY>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalMarginTra
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

With Response
	If mstrErrors > vbNullString Then
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MarginTraError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	Else
		If inspostMarginTra Then
			.Write("<SCRIPT>")
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If insFinish() Then
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							.Write("insReloadTop(false);")
						Else
							.Write("window.close();insReloadTop(true);")
						End If
					End If
				Else
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						.Write("top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;")
					Else
						.Write("window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;")
					End If
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "';")
				Else
					Response.Write("window.close();opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "';")
				End If
			End If
			.Write("</SCRIPT>")
		End If
	End If
End With
mobjValues = Nothing
mobjMarginTra = Nothing
%>
</BODY>
</HTML>




