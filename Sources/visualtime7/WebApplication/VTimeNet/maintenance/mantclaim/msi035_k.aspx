<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mclsProvider As eClaim.Tab_Provider

Dim mclsClient As eClient.Client

Dim mobjGrid As eFunctions.Grid


'+ insDefineHeader(): Definición del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(101814, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 40, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddHiddenColumn("tcnBranch", "")
		Call .AddHiddenColumn("tcnCheck", "")
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Codispl = "MSI035"
		.Codisp = "MSI035_K"
		.sCodisplPage = "MSI035"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
		.Top = 10
		.Height = 200
		.Width = 350
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'+ insPreMSI035_K(): Se cargan los Valores del Grid
'------------------------------------------------------------------------------
Private Sub insPreMSI035_K()
	'------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolProviders As eClaim.Tab_Providers
	Dim lclsProvider As eClaim.Tab_Provider
	
	lclsProvider = New eClaim.Tab_Provider
	lcolProviders = New eClaim.Tab_Providers
	
	If lcolProviders.Find_Branch(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), mclsClient.sClient) Then
		lintIndex = 0
		For	Each lclsProvider In lcolProviders
			With mobjGrid
				If lclsProvider.nProvider <> CDbl("0") Then
					.Columns("Sel").Checked = CShort("1")
				Else
					.Columns("Sel").Checked = CShort("2")
				End If
				.Columns("tcnCheck").DefValue = CStr(.Columns("Sel").Checked)
				.Columns("tctDescript").DefValue = lclsProvider.sDescript
				.Columns("tcnBranch").DefValue = CStr(lclsProvider.nBranch)
				.Columns("Sel").OnClick = "CheckValues(this," & lintIndex & ")"
				lintIndex = lintIndex + 1
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsProvider
	End If
	Response.Write(mobjGrid.closeTable())
	lclsProvider = Nothing
	lcolProviders = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mclsProvider = New eClaim.Tab_Provider
mclsClient = New eClient.Client

If mclsProvider.FindProvider(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble)) Then
	If mclsClient.Find(mclsProvider.sClient, True) Then
	End If
End If

mobjValues.sCodisplPage = "MSI035"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>

<SCRIPT> 
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"

//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
      function insStateZone(){
//------------------------------------------------------------------------------------------      
}

//% CheckValues: se controla la propiedad check del campo tcnCheck
//------------------------------------------------------------------------------------------
function CheckValues(Field, I){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if(typeof(tcnCheck[I])=='undefined')
			tcnCheck.value = (Field.checked)?1:2;
		else
			tcnCheck[I].value = (Field.checked)?1:2;
	}
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProviderBranch" ACTION="valMantClaim.aspx?sCodispl=MSI035&nTypeProv=<%=Request.QueryString.Item("nTypeProv")%>&nProvider=<%=Request.QueryString.Item("nProvider")%>&sClient=<%=Request.QueryString.Item("sClient")%>">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=101813><%= GetLocalResourceObject("tcnProviderCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tcnProvider", 5, Request.QueryString.Item("nProvider"),  , GetLocalResourceObject("tcnProviderToolTip"), True)%></TD>
            <TD><%=mobjValues.TextControl("tctProviderName", 40, mclsClient.sCliename,  , GetLocalResourceObject("tctProviderNameToolTip"), True)%></TD>
			<TD><%=mobjValues.HiddenControl("tctClient", mclsClient.sClient)%></TD>
        </TR>
    </TABLE>
    <%

Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()
Call insPreMSI035_K()

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write("<TABLE>")
		.Write("<TR>")
		.Write("<TD>")
		.Write(mobjValues.ButtonAbout("MSI035"))
		.Write("</TD>")
		.Write("<TD>")
		.Write(mobjValues.ButtonHelp("MSI035"))
		.Write("</TD>")
		.Write("<TD WIDTH=""95%""></TD>")
		.Write("<TD ALIGN=""RIGHT"">")
		mobjValues.ActionQuery = False
		'.Write mobjValues.ButtonAcceptCancel("window.close();",,,,eFunctions.Values.eButtonsToShow.OnlyCancel)
		.Write("</TD>")
		.Write("</TR>")
		.Write("<TR>")
		.Write("<TD></TD>")
		.Write("<TD COLSPAN=""2"">")
		.Write(mobjValues.BeginPageButton)
		.Write("</TD>")
		.Write("<TD></TD>")
		.Write("</TR>")
		.Write("</TABLE>")
	End With
End If

%>
    <TABLE WIDTH="100%">
        <TR>
            <TD ALIGN="Right"><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.All)%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsProvider = Nothing
mclsClient = Nothing
mobjGrid = Nothing
%>





