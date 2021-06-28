<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Const CN_BENEFICIAR As Short = 0
Const CN_INTERMEDIA As Short = 1
Const CN_CLIENT As Short = 2
Const CN_POLICY_CERTIF As Short = 3
Const CN_RECEIPT As Short = 4
Const CN_CLAIM As Short = 5

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim llngAction As String
Dim mobjMenu As eFunctions.Menues



'% insPreLT031Upd: carga los valores de la página LT031
'--------------------------------------------------------------------------------------------
Private Sub insPreLT031Upd()
	'--------------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valLetterSeq.aspx", "LT031", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
	Response.Write("<SCRIPT>self.document.forms[0].tctLettParam.disabled=true;</" & "Script>")
End Sub

'%insDefineHeader: Define el Header del Grid que muestra los parámetros
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	With mobjGrid
		'.Columns.AddTextColumn(7338,"Parámetros", "tctLettParam", 30, vbNullString,  ,vbNullString)
		.Columns.AddTextColumn(10687, _
		                       "Parámetros", _
		                       "tctLettParam", _
		                       30, _
		                       vbNullString, _
		                       , _
		                       "Muestra cada uno de los parámetros requeridos por la carta modelo para obtener la informacion de la correspondencia a ser enviada")
		                       
		'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1010"'
		'.Columns.AddTextColumn(7339,"Valor", "tcnLettParam", 30, vbNullString,  ,vbNullString)
		.Columns.AddTextColumn(10688, _
		                       "Valor", _
		                       "tcnLettParam", _
		                       30, _
		                       vbNullString, _
		                       , _
		                       "Valor del parámetro solicitado en la línea")
		'UPGRADE_ISSUE: The preceding line couldn't be parsed. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1010"'
		.Columns.AddHiddenColumn("nParameter", CStr(0))
		.Columns("tctLettParam").EditRecord = True
		.Codispl = "LT031"
		.Width = 400
		.Height = 210
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		Call .SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insPreLT031()
	'----------------------------------------------------------------------------------------------
	Dim lclsLettParam As eLetter.LettParam
	Dim lclsLettParams As eLetter.LettParams
	Dim lclsLetter As eLetter.Letter
	Dim lclsLettValuess As eLetter.LettValuess
	Dim lclsLettValues As eLetter.LettValues
	
	'+ Se instancian los objetos para poder cargar el grid de parámetros
	lclsLettParam = New eLetter.LettParam
	lclsLettParams = New eLetter.LettParams
	lclsLetter = New eLetter.Letter
	lclsLettValues = New eLetter.LettValues
	lclsLettValuess = New eLetter.LettValuess
	
	If Not lclsLettValuess.Find(Session("nLettRequest"), 1) Then
		With lclsLetter
			.nLetterNum = Session("nLetterNum")
			.dEffecDate = Session("dInpDate")
			lclsLettParams = .LettParameters
		End With
		
		For	Each lclsLettParam In lclsLettParams
			'+ Se definen las columnas del grid según lo obtenido de la lectura de Letters vinculado a los parámetros de un modelo
			Select Case lclsLettParam.nParameters
				Case CN_BENEFICIAR
					insDefineBeneficiar(lclsLettParam.nParameters, vbNullString)
				Case CN_CLAIM
					insDefineClaim(lclsLettParam.nParameters, vbNullString)
				Case CN_CLIENT
					insDefineClient(lclsLettParam.nParameters, vbNullString)
				Case CN_INTERMEDIA
					insDefineIntermedia(lclsLettParam.nParameters, vbNullString)
				Case CN_POLICY_CERTIF
					insDefinePolCertif(lclsLettParam.nParameters, vbNullString)
				Case CN_RECEIPT
					insDefineReceipt(lclsLettParam.nParameters, vbNullString)
			End Select
		Next lclsLettParam
		Response.Write("<SCRIPT>self.document.forms[0].tcnStatusGrid.value = 2;</" & "Script>")
	Else
		lclsLettValuess = Nothing
		lclsLettValuess = New eLetter.LettValuess
		
		If lclsLettValuess.FindByParameters(mobjValues.StringToType(Session("nLettRequest"), eFunctions.Values.eTypeData.etdInteger)) Then
			For	Each lclsLettValues In lclsLettValuess
				With lclsLettValues
					mobjGrid.Columns("tctLettParam").DefValue = .sVariable
					mobjGrid.Columns("tcnLettParam").DefValue = .sValue
					mobjGrid.Columns("nParameter").DefValue = CStr(.nParameters)
					
					Response.Write(mobjGrid.DoRow)
				End With
			Next lclsLettValues
		End If
		Response.Write(("<SCRIPT>self.document.forms[0].tcnStatusGrid.value ='" & lclsLettValuess.AllValues & "';</" & "Script>"))
	End If
	
	With Response
		.Write(mobjGrid.closeTable)
		.Write("<br><br><br>")
		.Write(mobjValues.BeginPageButton)
	End With
	
	lclsLettParam = Nothing
	lclsLettParams = Nothing
	lclsLetter = Nothing
End Sub

'%insDefineBeneficiar: carga los valores de la página LT031
'--------------------------------------------------------------------------------------------
Private Sub insDefineBeneficiar(ByVal nParameter As Short, ByVal sValue As String)
	'--------------------------------------------------------------------------------------------
	With mobjGrid
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(10506) '"Tipo de doc."
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(212) '"Ramo"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(251) '"Producto"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(4) '"Póliza"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(213) '"Certificado"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(121) '"Cliente"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(110) '"Fecha Efecto"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
	End With
End Sub

'%insDefineClaim:
'---------------------------------------------------------------------------
Private Sub insDefineClaim(ByVal nParameter As Short, ByVal sValue As String)
	'---------------------------------------------------------------------------
	With mobjGrid
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(9) '"Siniestro"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
	End With
End Sub

'%insDefineClient:
'---------------------------------------------------------------------------
Private Sub insDefineClient(ByVal nParameter As Short, ByVal sValue As String)
	'---------------------------------------------------------------------------
	With mobjGrid
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(121) '"Cliente"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
	End With
End Sub

'%insDefineIntermedia:
'---------------------------------------------------------------------------
Private Sub insDefineIntermedia(ByVal nParameter As Short, ByVal sValue As String)
	'---------------------------------------------------------------------------
	With mobjGrid
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(122) '"Intermed."
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
	End With
End Sub

'%insDefinePolCertif:
'---------------------------------------------------------------------------
Private Sub insDefinePolCertif(ByVal nParameter As Short, ByVal sValue As String)
	'---------------------------------------------------------------------------
	With mobjGrid
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(10506) '"Tipo de doc."
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(212) '"Ramo"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(251) '"Producto"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(4) '"Póliza"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(213) '"Certificado"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
	End With
End Sub

'%insDefineReceipt:
'---------------------------------------------------------------------------
Private Sub insDefineReceipt(ByVal nParameter As Short, ByVal sValue As String)
	'---------------------------------------------------------------------------
	With mobjGrid
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(10506) '"Tipo de doc."
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(212) '"Ramo"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(251) '"Producto"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(7) '"Recibo"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(10507) '"D/Ctrol.Rec"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
		.Columns("tctLettParam").DefValue = mobjValues.getInternalMsg(10508) '"Núm.Convenio"
		.Columns("tcnLettParam").DefValue = sValue
		.Columns("nParameter").DefValue = CStr(nParameter)
		Response.Write(.DoRow)
	End With
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT031")

llngAction = Request.QueryString.Item("nMainAction")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LT031"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
mobjGrid.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "LT031"


If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "LT031", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
%>
<HTML>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HEAD>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("LT031", Request.QueryString.Item("sWindowDescript")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmLT031" ACTION="valLetterseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

<%
insDefineHeader()

'%CreateHiddenControl: crea un control oculto para manejar el estado de "todos con valor" si el tipo de solicitud es individual    
Response.Write(mobjValues.HiddenControl("tcnStatusGrid", CStr(0)))

If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreLT031()
Else
	insPreLT031Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:50:00 a.m.
Call mobjNetFrameWork.FinishPage("LT031")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








