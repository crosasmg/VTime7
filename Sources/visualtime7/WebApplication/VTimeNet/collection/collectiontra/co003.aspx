<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

'- Variables para almacenar los valores tartados.
Dim mdblAmount As Integer
Dim mdtmPayDate As String



'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "co003"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPaynumbeColumnCaption"), "tcnPaynumbe", 4, "", True, "", True, 0,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdPaydateColumnCaption"), "tcdPaydate",  , True, "",  ,  , "ProductValues()", False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntammouColumnCaption"), "tcnIntammou", 18, "", True, "", True, 6,  ,  , "SetValue(this.value); ProductValues()", False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 4, "", True, "", True, 2,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, "", True, "", True, 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnStatus_preColumnCaption"), "tcnStatus_pre", "table19", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnStatus_preColumnCaption"))
		Call .AddHiddenColumn("tcnIntammouAUX", "")
		Call .AddHiddenColumn("tcnAction_hdr", Request.QueryString.Item("nAction"))
		Call .AddHiddenColumn("tcnReceipt_hdr", Request.QueryString.Item("nReceipt"))
		Call .AddHiddenColumn("tcdEffecdate_hdr", Request.QueryString.Item("dEffecdate"))
		Call .AddHiddenColumn("tcnRate_hdr", Request.QueryString.Item("nRate"))
		Call .AddHiddenColumn("tctPay_form_hdr", Request.QueryString.Item("sPay_form"))
		Call .AddHiddenColumn("tcnPremium_hdr", Request.QueryString.Item("nPremium"))
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "CO003"
		.Top = 100
		.Height = 256
		.Width = 250
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		
		If Request.QueryString.Item("nAction") = "1" Or Request.QueryString.Item("nAction") = "6" Then
			.Columns("Sel").GridVisible = False
		Else
			.Columns("Sel").GridVisible = Not .ActionQuery
		End If
		.Columns("tcnPaynumbe").EditRecord = True
		.Columns("tcnPaynumbe").Disabled = True
		.Columns("tcnRate").Disabled = True
		.sDelRecordParam = "nAction=" & Request.QueryString.Item("nAction") & "&nReceipt=" & Request.QueryString.Item("nReceipt") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nIntammou=" & Request.QueryString.Item("nIntammou") & "&sPay_form=" & Request.QueryString.Item("sPay_form") & "&nPremium=" & Request.QueryString.Item("nPremium") & "&nnPaynumbe='+ marrArray[lintIndex].tcnnPaynumbe + '"
		.sEditRecordParam = "nAction=" & Request.QueryString.Item("nAction") & "&nReceipt=" & Request.QueryString.Item("nReceipt") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nIntammou=" & Request.QueryString.Item("nIntammou") & "&nPay_form=" & Request.QueryString.Item("nPay_form") & "&nPremium=" & Request.QueryString.Item("nPremium") & "&nRate=" & Request.QueryString.Item("nRate") & "&nStatus_pre=" & Request.QueryString.Item("nStatus_pre")
		.DeleteButton = False
		
		If Request.QueryString.Item("nAction") = "1" Then
			.AddButton = True
		Else
			.AddButton = False
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreCO003. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreCO003()
	'------------------------------------------------------------------------------
	Dim lcolPremiums As eCollection.Premiums
	Dim lclsPremium As Object
	
	lcolPremiums = New eCollection.Premiums
	
	With mobjGrid
		If lcolPremiums.Find_CO003(mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nAction"), eFunctions.Values.eTypeData.etdDouble)) Then
			For	Each lclsPremium In lcolPremiums
				.Columns("tcnPaynumbe").DefValue = lclsPremium.nPaynumbe
				.Columns("tcdPaydate").DefValue = lclsPremium.dPaydate
				.Columns("tcnIntammou").DefValue = lclsPremium.nIntammou
				.Columns("tcnRate").DefValue = lclsPremium.nRate
				.Columns("tcnPremium").DefValue = lclsPremium.nPremium
				.Columns("tcnStatus_pre").DefValue = lclsPremium.nStatus_pre
				.Columns("tcnIntammouAUX").DefValue = lclsPremium.nIntammou
				Response.Write(mobjGrid.DoRow)
				Session("intCount") = Session("intCount") + 1
			Next lclsPremium
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lcolPremiums = Nothing
	lclsPremium = Nothing
End Sub

'% insPreCO003Upd. Se define esta funcion para construir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreCO003Upd()
	'------------------------------------------------------------------------------
	Dim Premium As eCollection.Premium
	
	With Request
		If .QueryString.Item("Type") = "PopUp" Or .QueryString.Item("Type") <> "PopUp" Then
			Premium = New eCollection.Premium
			Premium = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCollectionTra.aspx", "CO003", .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Type") = "PopUp" And .QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insDefValues(""ShowDataCO003"", ""sField=getPayNumbe"" + ""&nReceipt="" + " & .QueryString.Item("nReceipt") & ");</" & "Script>")
			Response.Write("<SCRIPT>self.document.forms[0].tcnRate.value = " & .QueryString.Item("nRate") & "</" & "Script>")
			If .QueryString.Item("nStatus_pre") = "0" Or .QueryString.Item("nStatus_pre") = vbNullString Then
				Response.Write("<SCRIPT>self.document.forms[0].tcnStatus_pre.value = 1" & "</" & "Script>")
			Else
				Response.Write("<SCRIPT>self.document.forms[0].tcnStatus_pre.value = " & .QueryString.Item("nStatus_pre") & "</" & "Script>")
			End If
		End If
		'Session("intCount") = 0
	End With
End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mdblAmount = eRemoteDB.Constants.intNull
	mdtmPayDate = ""
End Sub

'% insOldValues: Se encarga de asignar los valores obtenidos en vbscript a javascript.
'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mdblAmount <> eRemoteDB.Constants.intNull And mdtmPayDate <> "" Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mlngReceipt = " & CStr(mdblAmount) & ";")
			.Write("var mdtmPayDate = " & CStr(mdtmPayDate) & ";")
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mdblAmount = 0;")
			.Write("var mdtmPayDate = '';")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co003")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co003"

%>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
    </SCRIPT>




<SCRIPT>
//%	SetValue: Se almacena el valor del interés a aplicar en la variable auxiliar.
//---------------------------------------------------------------------------------------------
function SetValue(nValue){
//---------------------------------------------------------------------------------------------
	if(nValue!=0 || nValue!="")
		self.document.forms[0].elements["tcnIntammouAUX"].value = nValue;
}	

//%	ProductValues: Se efectúan los cálculos para obtener el interes a aplicar.
//---------------------------------------------------------------------------------------------
function ProductValues(){
//---------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
	    if ((tcdPaydate.value!='' && tcdPaydate.value!=mdtmPayDate) ||
			(tcnIntammou.value!='' && tcnIntammou.value!=mdblAmount)){
			mdtmPayDate = tcdPaydate.value;
			mdblAmount = tcnIntammou.value;
			insDefValues("ShowDataCO003", "sField=calAmountRate" + "&dEffecdate=" + tcdEffecdate_hdr.value + "&nRate=" + tcnRate_hdr.value + "&nIntammou=" + tcnIntammou.value + "&dPaydate=" + tcdPaydate.value);
			
		}
	}
}

</SCRIPT>
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectRef" ACTION="valCollectionTra.aspx?mode=1">
<%

mobjValues.ActionQuery = Request.QueryString.Item("nAction") = "7"
Call insDefineHeader()
With Response
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenu.setZone(2, "CO003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		Call insPreCO003()
		mobjMenu = Nothing
	Else
		Call insPreCO003Upd()
	End If
End With

Call insReaInitial()
Call insOldValues()

mobjGrid = Nothing
mobjValues = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




