<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim lclsGeneral As eGeneral.GeneralFunction
Dim mstrQueryString As String

Dim mstrErrors As String
Dim mobjValues As eFunctions.Values
Dim mclsTab_ActiveLife As eProduct.Tab_ActiveLife


'% insValProdActLifeSeq: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValProdActLifeSeq() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ DP607A: Condiciciones generales de planes de VidActiva
		
		Case "DP607A"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                        insValProdActLifeSeq = mclsTab_ActiveLife.InsValDP607A(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCapMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnMChanInves"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnErrRange"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble, True), 0, 0, 0, Date.MinValue)
				Else
					insValProdActLifeSeq = mclsTab_ActiveLife.InsValDP607AMsg(.QueryString.Item("sCodispl"), .Form.Item("hddOption").Length, .Form.Item("hddOption"))
				End If
			End With
			
			'+ Cargos por Planes de VidActiva
		Case "DP607B"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdActLifeSeq = mclsTab_ActiveLife.InsValDP607B(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdnTypeLoad"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnInitMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapStart"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAddMonth"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			'+ Cargos por Rescate		
		Case "DP607C"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdActLifeSeq = mclsTab_ActiveLife.InsValDP607C(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMonthIni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQMonthEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), 0,0,0,0)
				End If
			End With
			
			'+ Porcentajes de rentabilidad		
		Case "DP607D"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValProdActLifeSeq = mclsTab_ActiveLife.InsValDP607D(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarrMin"), eFunctions.Values.eTypeData.etdDouble, True))
				End If
			End With
			
			
		Case Else
			insValProdActLifeSeq = "insValProdActLifeSeq: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostProdLifeSeq: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostProdLifeSeq() As Boolean
	'--------------------------------------------------------------------------------------------
	
	Dim lblnPost As Boolean
	lblnPost = False
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ DP607A: Condiciciones generales de planes de VidActiva
		
		Case "DP607A"
			lblnPost = True
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
                        lblnPost = mclsTab_ActiveLife.InsPostDP607A(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMChanInves"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnErrRange"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOption"), eFunctions.Values.eTypeData.etdDouble), 0, 0, 0, 0, 0, 0, 0,0,0)
				End If
			End With
			
			'+ Cargos por planes de VidActiva
			
		Case "DP607B"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mclsTab_ActiveLife.InsPostDP607B(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("hdnTypeLoad"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInitMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEndMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnCapStart"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCapEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnAddMonth"), eFunctions.Values.eTypeData.etdDouble, True))
					
					mstrQueryString = "&nModulec=" & Request.Form.Item("hdnModulec") & "&nTypeLoad=" & Request.Form.Item("hdnTypeLoad")
				Else
					lblnPost = True
				End If
			End With
			
			'+ Cargos por Rescate
		Case "DP607C"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mclsTab_ActiveLife.InsPostDP607C(.QueryString("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnQMonthIni"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnQMonthEnd"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble, True), 0,0,0,0,0)
					
					mstrQueryString = "&nModulec=" & Request.Form.Item("hdnModulec")
				Else
					lblnPost = True
				End If
			End With
			
			
			'+ Porcentajes de rentabilidad
		Case "DP607D"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mclsTab_ActiveLife.InsPostDP607D(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdnModulec"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeTypeInvest"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarr"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarrMin"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnIntWarrClear"), eFunctions.Values.eTypeData.etdDouble, True))
					
					mstrQueryString = "&nModulec=" & Request.Form.Item("hdnModulec")
				Else
					lblnPost = True
				End If
			End With
			
			
	End Select
	
	insPostProdLifeSeq = lblnPost
End Function

</script>
<%Response.Expires = -1

lclsGeneral = New eGeneral.GeneralFunction
mstrCommand = "&sModule=Product&sProject=ProductSeq&sSubProject=ProdActLifeSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
mstrQueryString = vbNullString
%>
<HTML>
<HEAD>
	<LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


	
<SCRIPT>

//% insvalTabs: se verifica la existencia de ventanas requeridas en la secuencia
//-------------------------------------------------------------------------------------------
function insvalTabs(){
//-------------------------------------------------------------------------------------------
	
	var lblnTabs = false;
	var Array = top.frames['fraSequence'].sequence;
	for(var lintIndex=0; lintIndex<Array.length; lintIndex++)
		if(Array[lintIndex].Require=="2" ||
		   Array[lintIndex].Require=="5")
			lblnTabs = true;

	if(lblnTabs){
//+ Se envía un error indicando que faltan
		top.frames["fraFolder"].document.location.reload();
		alert("<%=lclsGeneral.insLoadMessage(3902)%>");
	}
	else
		top.close();	
}
</SCRIPT>
</HEAD>

<%If CDbl(Request.QueryString.Item("nZone")) = 1 Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>

<SCRIPT>
function CancelErrors(){
	self.history.go(-1)
}
function NewLocation(Source,Codisp){
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<SCRIPT src="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>
<%
mobjValues = New eFunctions.Values
mclsTab_ActiveLife = New eProduct.Tab_ActiveLife

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValProdActLifeSeq
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ProdActLifeSeqError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write("self.history.go(-1)")
			.Write("</SCRIPT>")
		End With
	Else
		If insPostProdLifeSeq Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location=""/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
				
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Response.Write("<SCRIPT LANGUAGE=JAVASCRIPT>self.history.go(-1);</SCRIPT>")
				End If
			Else
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>opener.top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/ProdActLifeSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				End If
				
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "DP607A"
						If Request.QueryString.Item("Action") = "Add" Then
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=0&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "'</SCRIPT>")
						End If
					Case Else
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
						Else
							Response.Write("<SCRIPT>window.close();opener.top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("Index") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
						End If
				End Select
			End If
		End If
	End If
Else
	Response.Write("<SCRIPT>insvalTabs()</SCRIPT>")
End If
mobjValues = Nothing
mclsTab_ActiveLife = Nothing
lclsGeneral = Nothing
%>
</BODY>
</HTML>





