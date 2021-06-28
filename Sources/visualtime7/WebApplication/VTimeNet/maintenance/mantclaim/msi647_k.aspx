<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las comunas de la página
Dim mobjMenu As Object

'- Objetos para informar el proveedor
Dim mclsProvider As eClaim.Tab_Provider
Dim mclsClient As eClient.Client


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenZoneColumnCaption"), "cbenZone", "Tabmunicipality", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenZoneColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 2, "", True, GetLocalResourceObject("tcnOrderColumnToolTip"), False, 0,  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = "MSI647"
		.Codisp = "MSI647_k"
		.sCodisplPage = "MSI647"
		.Top = 280
		.Left = 270
		.Height = 200
		.Width = 350
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("cbenZone").EditRecord = True
		.Columns("cbenZone").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "cbenZone=' + marrArray[lintIndex].cbenZone + '&nProvider=" & Request.QueryString.Item("nProvider")
		
		'+ Se pasa el código de proveedor para la actualización y que llegue a ValMantClaim 
		.sEditRecordParam = "nProvider=" & Request.QueryString.Item("nProvider")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.DeleteButton = True
		.AddButton = True
		.Columns("Sel").GridVisible = True
		
	End With
End Sub

'%insPreMSI647_k: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI647_k()
	'--------------------------------------------------------------------------------------------
	Dim lcolTab_prov_zones As eClaim.Tab_prov_zones
	Dim lclsTab_prov_zone As Object
	
	lcolTab_prov_zones = New eClaim.Tab_prov_zones
	
	Response.Write("<DIV ID=""Scroll"" STYLE=""width:480;height:300;overflow:auto;outset gray"">")
	With mobjGrid
		If lcolTab_prov_zones.Find(CInt(Request.QueryString.Item("nProvider"))) Then
			For	Each lclsTab_prov_zone In lcolTab_prov_zones
				.Columns("cbenZone").DefValue = lclsTab_prov_zone.nZone
				.Columns("tcnOrder").DefValue = lclsTab_prov_zone.nOrder
				Response.Write(mobjGrid.DoRow())
			Next lclsTab_prov_zone
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("	</DIV>" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD CLASS=""HORLINE"" COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("MSI647"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonHelp("MSI647"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")

	mobjValues.ActionQuery = False
	Response.Write(mobjValues.ButtonAcceptCancel("window.close();",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	lclsTab_prov_zone = Nothing
	lcolTab_prov_zones = Nothing
End Sub

'% insPreMSI647Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMSI647Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsTab_prov_zone As eClaim.Tab_prov_zone
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTab_prov_zone = New eClaim.Tab_prov_zone
			Call lclsTab_prov_zone.InsPostMSI647Upd(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbenZone"), eFunctions.Values.eTypeData.etdDouble), 0, Session("nUsercode"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantClaim.aspx", "MSI647", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsTab_prov_zone = Nothing
End Sub

</script>
<%Response.Expires = -1

mclsProvider = New eClaim.Tab_Provider
mclsClient = New eClient.Client

mobjValues = New eFunctions.Values

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MSI647"

If mclsProvider.FindProvider(mobjValues.StringToType(Request.QueryString.Item("nProvider"), eFunctions.Values.eTypeData.etdDouble)) Then
	If mclsClient.Find(mclsProvider.sClient, True) Then
	End If
End If
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:57 $|$$Author: Nvaplat18 $"
	
//% insCancel: Acciones a efectuar al cancelar la ventana
//%------------------------------------------------------------------------------------------
function insCancel(){
//%------------------------------------------------------------------------------------------
    return true;
}

//% insStateZone: Se habilita/des-habilita los campos de la ventana al seleccionar un acción de la ventana.
//%------------------------------------------------------------------------------------------
function insStateZone(){
//%------------------------------------------------------------------------------------------      
    return true;
}
</SCRIPT> 
<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = Nothing
End If
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmMSI647" ACTION="valMantClaim.aspx?sCodispl=MSI647&nProvider=<%=Request.QueryString.Item("nProvider")%>">
<%Response.Write(mobjValues.ShowWindowsName("MSI647"))
If Request.QueryString.Item("Type") <> "PopUp" Then%>
		<BR>
		<TABLE WIDTH="100%">
			<TR>
				<TD><LABEL ID=101813><%= GetLocalResourceObject("tcnProviderCaption") %></LABEL></TD>
		        <TD><%=mobjValues.TextControl("tcnProvider", 5, Request.QueryString.Item("nProvider"),  , GetLocalResourceObject("tcnProviderToolTip"), True)%></TD>
		        <TD><%=mobjValues.TextControl("tctProviderName", 40, mclsClient.sCliename,  , GetLocalResourceObject("tctProviderNameToolTip"), True)%></TD>
				<TD><%=mobjValues.HiddenControl("tctClient", mclsClient.sClient)%></TD>
		    </TR>
		</TABLE>
		<BR>
<%End If
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI647_k()
Else
	Call insPreMSI647Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




