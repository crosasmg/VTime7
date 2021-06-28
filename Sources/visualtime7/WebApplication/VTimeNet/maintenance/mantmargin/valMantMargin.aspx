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
Dim mobjMantMargin As Object


'% insvalMantMargin: Se realizan las validaciones de las páginas
'--------------------------------------------------------------------------------------------
Function insvalMantMargin() As String
	'--------------------------------------------------------------------------------------------
	With Request
		Select Case .QueryString.Item("sCodispl")
			'+ MMGS001: Tabla de valores SVS para margen de solvencia
			Case "MMGS001"
				mobjMantMargin = New eMargin.Tab_svs
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantMargin = mobjMantMargin.insvalMMGS001_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeFactor"), eFunctions.Values.eTypeData.etdDouble, True))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalMantMargin = mobjMantMargin.insvalMMGS001(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("cbeSVSClass"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnValue"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddFactor"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
				
				'+ MMGS002: Tipo de información para ingreso manual margen de solvencia 
			Case "MMGS002"
				mobjMantMargin = New eMargin.Margin_Allow
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insvalMantMargin = mobjMantMargin.insvalMMGS002_K(.QueryString("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTableTyp"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeClaimClass"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insvalMantMargin = mobjMantMargin.insvalMMGS002(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("hddInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddTableTyp"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("hddSource"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("hddIdRec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddClaimClass"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
				
			Case Else
				insvalMantMargin = "insvalMantMargin: Código lógico no encontrado (" & .QueryString.Item("sCodispl") & ")"
		End Select
	End With
End Function

'% inspostMantMargin: Se realizan las actualizaciones de las tablas
'--------------------------------------------------------------------------------------------
Function inspostMantMargin() As Boolean
	Dim sMainAction As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	With Request
		Select Case .QueryString.Item("sCodispl")
			'+ MMGS001: Tabla de valores SVS para margen de solvencia
			Case "MMGS001"
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nInsur_area=" & .Form.Item("cbeInsur_area") & "&nFactor=" & .Form.Item("cbeFactor")
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&nInsur_area=" & .Form.Item("hddInsur_area") & "&nFactor=" & .Form.Item("hddFactor")
						
						lblnPost = mobjMantMargin.inspostMMGS001(.QueryString("Action"), mobjValues.StringToType(.Form.Item("hddInsur_area"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddFactor"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSVSClass"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdInitDate"), eFunctions.Values.eTypeData.etdDate), Session("nUsercode"), mobjValues.StringToType(.Form.Item("tcnValue"), eFunctions.Values.eTypeData.etdDouble, True))
					End If
				End If
				
				'+ MMGS002: Tipo de información para ingreso manual margen de solvencia 
			Case "MMGS002"
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nInsur_area=" & .Form.Item("cbeInsur_area") & "&nTableTyp=" & .Form.Item("cbeTableTyp") & "&nSource=" & .Form.Item("cbeSource") & "&nClaimClass=" & .Form.Item("cbeClaimClass") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
					'+    Si tipo de tabla es pasivo se crea un registro vacio
					If CDbl(.Form.Item("cbeTableTyp")) = 5 Then
						If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 302 Then
							sMainAction = "Add"
							lblnPost = mobjMantMargin.inspostMMGS002(sMainAction, mobjValues.StringToType(.Form.Item("cbeInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeTableTyp"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("cbeSource"), eFunctions.Values.eTypeData.etdInteger, True), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("cbeClaimClass"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate, True), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))
						End If
					End If
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&nInsur_area=" & .Form.Item("hddInsur_area") & "&nTableTyp=" & .Form.Item("hddTableTyp") & "&nSource=" & .Form.Item("hddSource") & "&nClaimClass=" & .Form.Item("hddClaimClass") & "&dEffecdate=" & .Form.Item("hddEffecdate")
						
						lblnPost = mobjMantMargin.inspostMMGS002(.QueryString("Action"), mobjValues.StringToType(.Form.Item("hddInsur_area"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddTableTyp"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("hddSource"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("hddIdRec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hddClaimClass"), eFunctions.Values.eTypeData.etdInteger, True), mobjValues.StringToType(.Form.Item("hddEffecdate"), eFunctions.Values.eTypeData.etdDate, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
					End If
				End If
		End Select
	End With
	
	inspostMantMargin = lblnPost
End Function

</script>
<%Response.Expires = -1
mstrCommand = "&sModule=Maintenance&sProject=MantMargin&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
    <%=mobjValues.StyleSheet()%>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:08 $|$$Author: Nvaplat61 $"
</SCRIPT>
</HEAD>
<BODY>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalMantMargin
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

With Response
	If mstrErrors > vbNullString Then
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantMarginError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	Else
		If inspostMantMargin Then
			.Write("<SCRIPT>")
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						.Write("insReloadTop(false);")
					Else
						.Write("window.close();insReloadTop(true);")
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
mobjMantMargin = Nothing
%>
</BODY>
</HTML>




