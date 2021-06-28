<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Este procedimiento se encarga de definir las líneas del encabezado del grid
'-----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid.Columns
		Call .AddTextColumn(40733, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, False, GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(40730, GetLocalResourceObject("tcnSumins_realColumnCaption"), "tcnSumins_real", 18, CStr(0), True, GetLocalResourceObject("tcnSumins_realColumnToolTip"), True, 6,  ,  , "insCalcFields(this,1);insCalcFields(this,2)")
		Call .AddNumericColumn(40731, GetLocalResourceObject("tcnCoinsuranColumnCaption"), "tcnCoinsuran", 5, CStr(0), True, GetLocalResourceObject("tcnCoinsuranColumnToolTip"),  , 2,  ,  , "insCalcFields(this,2)")
		Call .AddNumericColumn(40732, GetLocalResourceObject("tcnSum_insurColumnCaption"), "tcnSum_insur", 18, CStr(0), True, GetLocalResourceObject("tcnSum_insurColumnToolTip"), True, 6,  ,  , "insCalcFields(this,3)")
		Call .AddHiddenColumn("nSumins_cod", CStr(0))
		Call .AddHiddenColumn("hddnSum_insur_old", vbNullString)
	End With
	With mobjGrid
		.Codispl = "CA009"
		.Height = 280
		.Width = 380
		.Columns("Sel").GridVisible = False
		.Columns("tctDescript").EditRecord = True
		.AddButton = False
		.DeleteButton = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA009. Esta rutina se encarga de realizar las operaciones correspondientes a la
'% actualizacion de datos de la ventana
'----------------------------------------------------------------------------------------------
Private Sub insPreCA009()
	'----------------------------------------------------------------------------------------------
	Dim ldblSumins_real As String
	Dim ldblCoinsuran As String
	Dim ldblSum_insur As String
	Dim lintSumins_cod As Short
	Dim lcolSum_insurs As ePolicy.Sum_insurs
	Dim lclsSum_insur As Object
	Dim llintCont_Cod As Short
	Dim lintVar As Double
	Dim lblnFound As Boolean
	
	lcolSum_insurs = New ePolicy.Sum_insurs
	
	lblnFound = lcolSum_insurs.insPreCA009(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble, True), Request.QueryString.Item("sCurrency"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble))
	
	
	mobjGrid.sEditRecordParam = "nCurrency=' + self.document.forms[0].cbeCurrency.value + '" & "&sCurrency=" & lcolSum_insurs.sCurrency
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//- Variable para el control de versiones" & vbCrLf)
Response.Write("document.VssVersion=""$$Revision: 2 $|$$Date: 15/10/03 16:48 $|$$Author: Nvaplat61 $""" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//%	InsChangeCurrency: Ejecuta la busqueda con una nueva moneda" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function InsChangeCurrency(nCurrency, sCurrency){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	var lstrstring = """";" & vbCrLf)
Response.Write("	lstrstring += document.location;" & vbCrLf)
Response.Write("	lstrstring = lstrstring.replace(/&nCurrency=.*/, """");" & vbCrLf)
Response.Write("	lstrstring = lstrstring + ""&nCurrency="" + nCurrency + ""&sCurrency="" + sCurrency;" & vbCrLf)
Response.Write("	document.location = lstrstring;" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("<TABLE>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=13029>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>" & vbCrLf)
Response.Write("        ")

	
	With mobjValues
		.List = lcolSum_insurs.sCurrency
		.TypeList = 1
		Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lcolSum_insurs.nCurrency),  ,  ,  ,  ,  , "InsChangeCurrency(this.value,""" & lcolSum_insurs.sCurrency & """)",  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))
	End With
	
Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("</TABLE>")

	
	If lblnFound Then
		llintCont_Cod = 0
		lintVar = 0
		For	Each lclsSum_insur In lcolSum_insurs
			lintVar = 0
			llintCont_Cod = llintCont_Cod + 1
			With Response
				If lclsSum_insur.nSumins_real = eRemoteDB.Constants.intNull Then
					ldblSumins_real = "0"
					lintVar = lintVar + 1
				Else
					ldblSumins_real = lclsSum_insur.nSumins_real
				End If
				
				If lclsSum_insur.nCoinsuran = eRemoteDB.Constants.intNull Then
					ldblCoinsuran = "0"
					lintVar = lintVar + 1
				Else
					ldblCoinsuran = lclsSum_insur.nCoinsuran
				End If
				
				If lclsSum_insur.nSum_insur = eRemoteDB.Constants.intNull Then
					ldblSum_insur = "0"
					lintVar = lintVar + 1
				Else
					ldblSum_insur = lclsSum_insur.nSum_insur
				End If
				
				If lclsSum_insur.nSumins_cod = eRemoteDB.Constants.intNull Then
					If lclsSum_insur.nCode = eRemoteDB.Constants.intNull Then
						lintSumins_cod = llintCont_Cod
					Else
						lintSumins_cod = lclsSum_insur.nCode
					End If
				Else
					lintSumins_cod = lclsSum_insur.nSumins_cod
				End If
				
				With mobjGrid
					.Columns("tctDescript").DefValue = lclsSum_insur.sDescript
					.Columns("tcnSumins_real").DefValue = ldblSumins_real
					.Columns("tcnCoinsuran").DefValue = ldblCoinsuran
					.Columns("tcnSum_insur").DefValue = ldblSum_insur
					.Columns("nSumins_cod").DefValue = CStr(lintSumins_cod)
					.Columns("hddnSum_insur_old").DefValue = ldblSum_insur
				End With
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsSum_insur
	End If
	Response.Write(mobjGrid.closeTable())
	lclsSum_insur = Nothing
	lcolSum_insurs = Nothing
End Sub

'% insPreCA009Upd: Se encarga de mostrar el código correspondiente a la actualización de la vantana
'--------------------------------------------------------------------------------------------------
Private Sub insPreCA009Upd()
	'--------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% insCalcFields: Segun el valor en el campo tcnSumins_real y tcnCoinsuran se calcula tcnSum_insur" & vbCrLf)
Response.Write("//% Option = 1: Coloca en 100% el campo PORCENTAJE" & vbCrLf)
Response.Write("//% Option = 2: Calcula el VALOR ASEGURADO en base al VALOR REAL y al PORCENTAJE introducido" & vbCrLf)
Response.Write("//% Option = 3: Re-calcula el valor del campo PORCENTAJE tomando los valores de VALOR ASEGURADO y VALOR REAL " & vbCrLf)
Response.Write("//-----------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCalcFields(Field, Option) {" & vbCrLf)
Response.Write("//-----------------------------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	var llngSumins_real=0;" & vbCrLf)
Response.Write("	var llngCoinsuran=0;" & vbCrLf)
Response.Write("	var llngSum_insur=0;" & vbCrLf)
Response.Write("	var lstrCoinsuran="""";" & vbCrLf)
Response.Write("	var Valor=0;" & vbCrLf)
Response.Write("	llngSumins_real= insConvertNumber(self.document.forms[0].tcnSumins_real.value,mstrThousandSep,mstrDecimalSep)" & vbCrLf)
Response.Write("	llngCoinsuran= insConvertNumber(self.document.forms[0].tcnCoinsuran.value,mstrThousandSep,mstrDecimalSep)" & vbCrLf)
Response.Write("	llngSum_insur= insConvertNumber(self.document.forms[0].tcnSum_insur.value,mstrThousandSep,mstrDecimalSep)" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("	if(llngSumins_real>=0 && Option==1)" & vbCrLf)
Response.Write("	{" & vbCrLf)
Response.Write("		llngCoinsuran = 100;" & vbCrLf)
Response.Write("		self.document.forms[0].elements[""tcnCoinsuran""].value = llngCoinsuran;" & vbCrLf)
Response.Write("		return;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	if(llngSumins_real>=0 && llngCoinsuran>0 && Option==2)" & vbCrLf)
Response.Write("	{" & vbCrLf)
Response.Write("		llngSum_insur = 0;" & vbCrLf)
Response.Write("		llngSum_insur = (llngSumins_real * llngCoinsuran) / 100;" & vbCrLf)
Response.Write("//		llngSum_insur = Math.round(llngSum_insur)" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("		self.document.forms[0].tcnSum_insur.value = VTFormat(llngSum_insur, '', '', '', 2, true);" & vbCrLf)
Response.Write("		return;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	if(llngSumins_real>=0 && llngCoinsuran>0 && llngSum_insur>0 && Option==3)" & vbCrLf)
Response.Write("	{" & vbCrLf)
Response.Write("		llngCoinsuran = (llngSum_insur * 100) / llngSumins_real;" & vbCrLf)
Response.Write("		if(llngCoinsuran<100)" & vbCrLf)
Response.Write("		{" & vbCrLf)
Response.Write("			lstrCoinsuran = llngCoinsuran.toString();" & vbCrLf)
Response.Write("			Valor = lstrCoinsuran.indexOf(""."");" & vbCrLf)
Response.Write("			lstrCoinsuran = lstrCoinsuran.replace(""."", "","");" & vbCrLf)
Response.Write("			lstrCoinsuran = lstrCoinsuran.substring(0, Valor+3);" & vbCrLf)
Response.Write("			llngCoinsuran = lstrCoinsuran;" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("		self.document.forms[0].elements[""tcnCoinsuran""].value = llngCoinsuran;" & vbCrLf)
Response.Write("		return;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.actionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA009")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjValues.actionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<%
    Response.Write("<SCRIPT>" & " var mstrThousandSep = """ & mobjValues.msUserThousandSeparator & """;" & " var mstrDecimalSep = """ & mobjValues.msUserDecimalSeparator & """</SCRIPT>")

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
<FORM METHOD="POST" ID="FORM" NAME="CA009" ACTION="valPolicySeq.aspx?Mode=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA009()
Else
	Call insPreCA009Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
   
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("CA009")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




