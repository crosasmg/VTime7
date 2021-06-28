<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
Dim insPreVI8001Upd() As Object
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objetos/Variables para el manejo de la transacción
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsCov_prembas As ePolicy.Cov_prembas
Dim mclsTCover As Object
Dim mblnFound As Boolean
Dim mstrClient As String
Dim mstrRole As Object
Dim mclsGeneral As eGeneral.GeneralFunction
Dim mstrError As String
Dim mintCurrency As String
Dim mdblLegAmount As Object
Dim lblnFer As Object
Dim mintCount As Integer



'%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
'%del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	
	Dim lblnDisabledAge As Boolean
	Dim lintLength As Short
	Dim lobjColumn As eFunctions.Column
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	
	lintLength = 30
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se habilitan los campos de edad si el cliente es VIP
	If Request.QueryString.Item("Vip") = "1" Then
		lblnDisabledAge = False
	Else
		lblnDisabledAge = True
	End If
	
	If mclsCov_prembas.InsPreVI8001_A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mstrClient) Then
		
		mintCount = mclsCov_prembas.nCountRoles
		
		If mstrRole = vbNullString Then
			mstrRole = mclsCov_prembas.mclsRoles.nRole
		End If
		
		If mstrClient = vbNullString Then
			mstrClient = mclsCov_prembas.mclsRoles.sClient
		End If
		
		mblnFound = mclsCov_prembas.insPreVI8001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransactio"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mstrClient)
	End If
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		lobjColumn = .AddNumericColumn(40789, GetLocalResourceObject("tcnModulecColumnCaption"), "tcnModulec", 5, CStr(0), True, GetLocalResourceObject("tcnModulecColumnToolTip"), True, 0,  ,  ,  , True)
		lobjColumn = .AddNumericColumn(40790, "", "tcnCover", 5, CStr(0), True, GetLocalResourceObject("tcnCoverColumnToolTip"), True, 0,  ,  ,  , True)
		lobjColumn = .AddTextColumn(40785, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", lintLength, vbNullString,  , GetLocalResourceObject("tctCoverColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(40791, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(0), True, GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6)
		Call .AddNumericColumn(40792, GetLocalResourceObject("tcnRatecoveColumnCaption"), "tcnRatecove", 9, CStr(0), True, GetLocalResourceObject("tcnRatecoveColumnToolTip"), True, 6)
		Call .AddNumericColumn(40793, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0), True, GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
	End With
	
	'+Se asignan la configuración de la ventana (GRID) 
	With mobjGrid
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").Disabled = True
		.ActionQuery = Session("bQuery")
		.Codispl = Request.QueryString.Item("sCodispl")
		.Width = 800
		.Height = 400
		.FieldsByRow = 2
		.Splits_Renamed.AddSplit(0, "", 1)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		.Top = 20
		.Left = 25
		.DeleteButton = False
		.AddButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.EditRecordQuery = mobjValues.ActionQuery
	End With
End Sub

'%insPreVI8001. Esta rutina se encarga de realizar las operaciones correspondientes a la
'%actualizacion de datos de la ventana de Coberturas
'---------------------------------------------------------------------------------------
Function insPreVI8001() As Object
	'---------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lblnOneCurren As Boolean
	Dim lcolCov_prembass As ePolicy.Cov_prembass
	Dim lclsCov_prembas As Object
	Dim lstrQueryString As String
	
	lcolCov_prembass = New ePolicy.Cov_prembass
	
	lblnOneCurren = (mclsCov_prembas.mclsCurren_pol.CountCurrenPol + 1) <= 1
	
Response.Write("" & vbCrLf)
Response.Write("        <TABLE WIDTH=""100%"" COLS=4>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD><LABEL ID=13050>" & GetLocalResourceObject("cbeCurrencDesCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD>" & vbCrLf)
Response.Write("                ")

	
	mobjValues.TypeList = 1
	mobjValues.List = mclsCov_prembas.mclsCurren_pol.Charge_Combo
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrencDes", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCov_prembas.nCurrency),  ,  ,  ,  ,  , "ReloadPage()", lblnOneCurren,  , GetLocalResourceObject("cbeCurrencDesToolTip")))
	Response.Write("<SCRIPT> mintCurrencyChange = '" & mintCurrency & "'; </" & "Script>")
	
Response.Write("" & vbCrLf)
Response.Write("                </TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                 <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Datos del cliente seleccionado"">" & GetLocalResourceObject("AnchorDatos del cliente seleccionadoCaption") & "</A></LABEL></TD> " & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD COLSPAN=""5"" CLASS=""Horline""></TD>        " & vbCrLf)
Response.Write("            </TR> " & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            ")

	
	If mintCount = 1 Then
		
Response.Write("           " & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("                <TR>" & vbCrLf)
Response.Write("                    <TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                    <TD>")


Response.Write(mobjValues.TextControl("tctClient", 14, mclsCov_prembas.mclsRoles.sClient,  , GetLocalResourceObject("tctClientToolTip"), True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("                    <TD>")


Response.Write(mobjValues.TextControl("tctCliename", 40, mclsCov_prembas.mclsRoles.sCliename,  , GetLocalResourceObject("tctClienameToolTip"), True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("                </TR>" & vbCrLf)
Response.Write("            ")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("                <TR>" & vbCrLf)
Response.Write("                    <TD><LABEL ID=0>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                    <TD>" & vbCrLf)
Response.Write("                    ")

		
		mobjValues.TypeList = 1
		mobjValues.ClientRole = CStr(2)
		lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
		Response.Write(mobjValues.ClientControl("tctClient", mclsCov_prembas.mclsRoles.sClient,  , GetLocalResourceObject("tctClientToolTip"), "ReloadPage()",  ,  ,  ,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy,  ,  ,  , lstrQueryString))
		
Response.Write("" & vbCrLf)
Response.Write("                    </TD>" & vbCrLf)
Response.Write("                </TR>" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            ")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("            " & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.NumericControl("tcnAge", 2, CStr(mclsCov_prembas.mclsRoles.nAge),  , GetLocalResourceObject("tcnAgeToolTip"),  ,  , True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("                <TD><LABEL ID=0>" & GetLocalResourceObject("tcnAgeInsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("                <TD>")


Response.Write(mobjValues.NumericControl("tcnAgeIns", 2, CStr(mclsCov_prembas.mclsRoles.nAge(True)),  , GetLocalResourceObject("tcnAgeInsToolTip"),  ,  , True,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("            <TR>" & vbCrLf)
Response.Write("                <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            </TR>" & vbCrLf)
Response.Write("		")

	
	'+ Si no se trata de consulta    
	If Not mobjValues.ActionQuery Then
		'+ Si existen más de una moneda a tratar
		If Not lblnOneCurren Then
			Response.Write("<TD COLSPAN=""5"">" & "</TD>")
			Response.Write("<TD WIDTH=""5%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
		End If
	End If
	
Response.Write("" & vbCrLf)
Response.Write("        </TABLE>")

	
	
	'+ Si existe información para procesar
	If mblnFound Then
		If lcolCov_prembass.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mstrClient) Then
			
			For	Each lclsCov_prembas In lcolCov_prembass
				
				With mobjGrid
					.Columns("Sel").Checked = CShort("1")
					
					.Columns("tcnModulec").DefValue = lclsCov_prembas.nModulec
					.Columns("tcnCover").DefValue = lclsCov_prembas.nCover
					.Columns("tctCover").DefValue = lclsCov_prembas.sDescript
					'+ Suma asegurada solicitada                
					.Columns("tcnCapital").DefValue = lclsCov_prembas.nCapital
					'+ Tasa                
					.Columns("tcnRateCove").DefValue = lclsCov_prembas.nRate
					'+ Prima
					.Columns("tcnPremium").DefValue = lclsCov_prembas.nPremium
					
					Response.Write(.doRow)
				End With
				
			Next lclsCov_prembas
			
		End If
		
	End If
	
	'+Se cierra el recorrido de la tabla 
	Response.Write(mobjGrid.CloseTable())
	
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("    <TD ALIGN=RIGHT><LABEL ID=40784>" & GetLocalResourceObject("AnchorCaption") & "</LABEL>" & vbCrLf)
Response.Write("    ")

	
	If mblnFound Then
		Response.Write(mobjValues.DIVControl("tcnTotPremium", True))
		Response.Write("<SCRIPT>")
		Response.Write("InsCalTotalPremium();")
		Response.Write("self.document.forms[0].action = '" & "ValPolicySeq.aspx?nRole=" & mstrRole & "&sClient=" & mstrClient & "&nIndexCover=" & Request.QueryString.Item("nIndexCover") & "';")
		Response.Write("</" & "Script>")
	End If
	
Response.Write("</TD>" & vbCrLf)
Response.Write("</TABLE>")

	
	Response.Write(mobjValues.BeginPageButton)
End Function

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI8001")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	mobjValues.ActionQuery = Session("bQuery")
	If Not mobjValues.ActionQuery Then
		mclsGeneral = New eGeneral.GeneralFunction
		mstrError = mclsGeneral.insLoadMessage(55963)
		mclsGeneral = Nothing
	End If
End With


mstrClient = Request.QueryString.Item("sClient")
mstrRole = Request.QueryString.Item("nRole")

%>



<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 28/06/06 5:21p $|$$Author: Fmendoza $"

    var mintCurrencyChange = 0;

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        self.document.forms[0].hddbPuntual.value = true;
    }
    top.frames['fraHeader'].ClientRequest(390,2);
}

//% InsCalTotalPremium: Calcula la prima total de las coberturas seleccionadas
//-------------------------------------------------------------------------------------------
function InsCalTotalPremium(){
//-------------------------------------------------------------------------------------------
	var ldblPremium = 0;

	for(var lintIndex=0; lintIndex<marrArray.length;lintIndex++){
		if (marrArray[lintIndex].Sel){
			if (marrArray[lintIndex].tcnPremium == '') marrArray[lintIndex].tcnPremium = 0;
			ldblPremium += insConvertNumber(marrArray[lintIndex].tcnPremium);
		}
	}
    UpdateDiv('tcnTotPremium', VTFormat(ldblPremium,'', '', '', 6, true));
}

//% insDisabled: Se encarga de desabiltar el boton de aceptar.
//-------------------------------------------------------------------------------------------
function insDisabled(){
//-------------------------------------------------------------------------------------------
	top.frames['fraHeader'].document.A390.disabled=false;
}

//% ReloadPage: se recarga la página
//-------------------------------------------------------------------------------------------
function ReloadPage(){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    
    with(self.document.forms[0]){        
        lstrQuery = "&sClient=" + tctClient.value +
                    "&nCurrency=" + cbeCurrencDes.value
        
        document.location.href = "VI8001.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&sCodisp=<%=Request.QueryString.Item("sCodisp")%>&nMainAction=" + nMainAction +
                                 lstrQuery        
    }                                                                 
}

</SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & ".aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & ";</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VI8001" ACTION="ValPolicySeq.aspx?nRole=<%=mstrRole%>&sClient=<%=mstrClient%>&nIndexCover=<%=Request.QueryString.Item("nIndexCover")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
mclsCov_prembas = New ePolicy.Cov_prembas
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
        'Call insPreVI8001Upd()
Else
	Call insPreVI8001()
End If
mclsCov_prembas = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
Call mobjNetFrameWork.FinishPage("VI8001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




