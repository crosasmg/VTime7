<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.56
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues

'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "agl008"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+Se definen las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctTypeColumnCaption"), "tctType", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctTypeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBranchColumnCaption"), "tcnBranch", 5, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnBranchColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 5, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnProductColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremanualColumnCaption"), "tcnPremanual", 18, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnPremanualColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnComanualColumnCaption"), "tcnComanual", 18, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnComanualColumnToolTip"), True, 6)
		Call .AddHiddenColumn("sCertype", "0")
		Call .AddHiddenColumn("nBranch", "0")
		Call .AddHiddenColumn("nProduct", "0")
		Call .AddHiddenColumn("nPolicy", "0")
		Call .AddHiddenColumn("dStartdate", "0")
		Call .AddHiddenColumn("dExpirdat", "0")
		Call .AddHiddenColumn("nIntermedpol", "0")
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "AGL008"
		.Codisp = "AGL008_K"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Columns("Sel").Alias_Renamed = "Indica que se desea seleccionar para el traspaso de cartera la información contenida en la línea."
		.Height = 210
		.Width = 420
		.Top = 10
		.Left = 10
	End With
	
End Sub

'------------------------------------------------------------------------------
Private Sub insPreAGL008()
	'------------------------------------------------------------------------------
	Dim lcolIntermedias As eAgent.Intermedias
	Dim lclsIntermedia As Object
	Dim llngProduct As Integer
	
	lcolIntermedias = New eAgent.Intermedias
	
	If mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		llngProduct = 0
	Else
		llngProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	If lcolIntermedias.FindAGL008(mobjValues.StringToType(Session("dDateProcess"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeBusiness"), Session("sTypePolicy"), mobjValues.StringToType(Session("nMunicipality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nInterBefore"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), llngProduct) Then
		
		For	Each lclsIntermedia In lcolIntermedias
			With mobjGrid
				Select Case lclsIntermedia.sCertype
					Case "1"
						.Columns("tctType").DefValue = "Propuesta"
					Case "2"
						.Columns("tctType").DefValue = "Póliza"
				End Select
				
				.Columns("tcnBranch").DefValue = lclsIntermedia.nBranch
				.Columns("tcnProduct").DefValue = lclsIntermedia.nProduct
				.Columns("tcnPolicy").DefValue = lclsIntermedia.nPolicy
				.Columns("tcnPremanual").DefValue = lclsIntermedia.nPremanual
				.Columns("tcnComanual").DefValue = lclsIntermedia.nComanual
				
				.Columns("sCertype").DefValue = lclsIntermedia.sCertype
				.Columns("nBranch").DefValue = mobjValues.TypeToString(lclsIntermedia.nBranch, eFunctions.Values.eTypeData.etdDouble)
				.Columns("nProduct").DefValue = mobjValues.TypeToString(lclsIntermedia.nProduct, eFunctions.Values.eTypeData.etdDouble)
				.Columns("nPolicy").DefValue = mobjValues.TypeToString(lclsIntermedia.nPolicy, eFunctions.Values.eTypeData.etdDouble)
				.Columns("dStartdate").DefValue = mobjValues.TypeToString(lclsIntermedia.dStartdate, eFunctions.Values.eTypeData.etdDate)
				.Columns("dExpirdat").DefValue = mobjValues.TypeToString(lclsIntermedia.dExpirdat, eFunctions.Values.eTypeData.etdDate)
				.Columns("nIntermedpol").DefValue = mobjValues.TypeToString(lclsIntermedia.nIntermedPol, eFunctions.Values.eTypeData.etdDouble)
				
			End With
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsIntermedia
	End If
	Response.Write(mobjGrid.closeTable())
	lcolIntermedias = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agl008")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agl008"

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

    
    <%=mobjValues.StyleSheet()%>
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjMenues.sSessionID = Session.SessionID
	mobjMenues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	With Response
		.Write(mobjValues.StyleSheet())
		.Write(mobjValues.WindowsTitle("AGL008", Request.QueryString.Item("sWindowDescript")))
		.Write(mobjMenues.setZone(2, "AGL008", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	End With
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<SCRIPT>
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------------------------
}
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:{
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction;
	        break;
		}
	}
}
</SCRIPT>

<FORM METHOD="POST" ID="FORM" NAME="frmAGL008" ACTION="ValAgentRep.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName("AGL008", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreAGL008()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.56
Call mobjNetFrameWork.FinishPage("agl008")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




