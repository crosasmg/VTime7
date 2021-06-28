<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddClientColumn(100700, GetLocalResourceObject("dtcClientInstColumnCaption"), "dtcClientInst", "",  , GetLocalResourceObject("dtcClientInstColumnToolTip"),  ,  , "lblCliename")
		Call .AddHiddenColumn("sClientInst", "")
		Call .AddHiddenColumn("sIndExists", "")
		Call .AddHiddenColumn("sSelAux", "")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA101"
		.Height = 140
		.Width = 500
		.DeleteButton = False
		.AddButton = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% insPreCA101: se cargan los campos de la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreCA101()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsFinanInstitut As Object
	Dim lcolFinanInstituts As Object
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% insReloadPage: se recarga la página para realizar la búsqueda de acuerdo a lo indicado" & vbCrLf)
Response.Write("//%				   en el campo puntual de la forma" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insReloadPage(){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	with(self.document.forms[0]){" & vbCrLf)
Response.Write("		self.document.location.href = ""/VTimeNet/Policy/PolicySeq/CA101.aspx?sCodispl=CA101&nMainAction="" + ")


Response.Write(Request.QueryString.Item("nMainAction"))


Response.Write(" +  ""&sClientBranch="" + valClientBranch.value" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//% insSelected: se invoca la ventana PopUp para la selección de los registros" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insSelected(Field, nIndex){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	if(typeof(self.document.forms[0].sSelAux[nIndex])=='undefined')" & vbCrLf)
Response.Write("		self.document.forms[0].sSelAux.value = (Field.checked)?'1':'2'" & vbCrLf)
Response.Write("	else" & vbCrLf)
Response.Write("		self.document.forms[0].sSelAux[nIndex].value = (Field.checked)?'1':'2'" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("            <TD WIDTH=30%><LABEL ID=100417>" & GetLocalResourceObject("valClientBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

'UPGRADE_NOTE: The 'ePolicy.FinanInstitut' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lclsFinanInstitut = Server.CreateObject("ePolicy.FinanInstitut")
	mobjValues.ClientRole = CStr(51)
	Response.Write(mobjValues.ClientControl("valClientBranch", lclsFinanInstitut.DefaultClientBranch(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Request.QueryString.Item("sClientBranch")),  , "", "insReloadPage()",  , "lblnCliename", False,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

'UPGRADE_NOTE: The 'ePolicy.FinanInstituts' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lcolFinanInstituts = Server.CreateObject("ePolicy.FinanInstituts")
	If lcolFinanInstituts.FindCA101(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), lclsFinanInstitut.DefaultClientBranch(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Request.QueryString.Item("sClientBranch"))) Then
		lintIndex = 0
		For	Each lclsFinanInstitut In lcolFinanInstituts
			With lclsFinanInstitut
				mobjGrid.Columns("dtcClientInst").DefValue = .sClientInst
				mobjGrid.Columns("sClientInst").DefValue = .sClientInst
				mobjGrid.Columns("sIndExists").DefValue = .nSel
				mobjGrid.Columns("Sel").Checked = .nSel
				mobjGrid.Columns("sSelAux").DefValue = .nSel
				mobjGrid.Columns("Sel").OnClick = "insSelected(this," & lintIndex & ");"
				lintIndex = lintIndex + 1
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsFinanInstitut
		lclsFinanInstitut = lcolFinanInstituts(1)
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	With Response
		.Write("<SCRIPT>")
		.Write("ValidateClient(self.document.forms[0].valClientBranch,""lblnCliename"");")
		.Write("</" & "Script>")
	End With
	
	lcolFinanInstituts = Nothing
	lclsFinanInstitut = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA101")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	Response.Write(mobjMenu.setZone(2, "CA101", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA101" ACTION="valPolicySeq.aspx?sTime=1">
    	<%Response.Write(mobjValues.ShowWindowsName("CA101", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCA101()

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA101")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




