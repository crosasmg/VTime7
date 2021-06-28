<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid

'-Objeto para el manejo y evaluación de los procesos de póliza.
Dim mobjClaimBenef As eClaim.ClaimBenef

'-Objeto para el manejo y evaluación de los procesos de póliza.
Dim mcol As Microsoft.VisualBasic.Collection

'-Objeto que determina el valor de ciertos parámetros del Stored Procedure.
Dim lstrRole As String

'-Indica el nombre del control que llamo la forma
Dim lstrControlName As Object
Dim lstrControlClieName As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddTextColumn(0, "Cliente", "tctClient", 16, "")
		.AddPossiblesColumn(0, "Figura", "cbeRole", "Table12", eFunctions.Values.eValuesType.clngComboType, vbNullString)
		.AddTextColumn(0, "Nombre", "tctCliename", 60, "")
		.AddHiddenColumn("hddsDigit", vbNullString)
	End With
	
	With mobjGrid
		'+ Se definen las propiedades generales del grid
		.Width = 200
		.Height = 200
		.Codispl = "SI018A"
		.DeleteButton = False
		.AddButton = False
		.Top = 100
		.Columns("sel").GridVisible = False
	End With
	
End Sub

'% insPreSI018: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI018A()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	
	'+ Obtiene el nombre del control que llamó a la ventana.
	If Not Request.QueryString("ControlName") = vbNullString Then
		lstrControlName = Request.QueryString("ControlName")
	End If
	
	If Not Request.QueryString("ControlClieName") = vbNullString Then
		lstrControlClieName = Request.QueryString("ControlClieName")
	End If
	
	'+ Variable pública que indica que la busqueda se realiza para el control de clientes.
	mobjClaimBenef.bClientControl = True
	
	'+ Búsqueda de los clientes asociados a la póliza    
	mcol = mobjClaimBenef.Find_ClaimBenefAsoc(mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDeman_type"), eFunctions.Values.eTypeData.etdDouble))
	lintIndex = 0
	'+ Se agrega el elemento tanto en la forma como en memoria.    
	With mobjGrid
		For	Each mobjClaimBenef In mcol
			.Columns("tctClient").DefValue = mobjClaimBenef.sClient
			.Columns("cbeRole").DefValue = CStr(mobjClaimBenef.nBene_type)
			.Columns("tctCliename").DefValue = mobjClaimBenef.sCliename
			.Columns("hddsDigit").DefValue = mobjClaimBenef.sDigit
			.Columns("tctClient").HRefScript = "SelectedClientClaim(" & lintIndex & ",'" & Request.QueryString("ControlName") & "','" & Request.QueryString("ControlClieName") & "');"
			.Columns("tctClient").HRefScript = .Columns("tctClient").HRefScript & "CloseWindow();"
			lintIndex = lintIndex + 1
			Response.Write(.DoRow)
		Next mobjClaimBenef
		Response.Write(mobjGrid.closeTable())
	End With
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" BORDER=""0"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""5%"">")


Response.Write(mobjValues.ButtonAbout("SI018A"))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD ALIGN=""RIGHT"">")

	Response.Write(mobjValues.ButtonAcceptCancel( ,  , True,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si018a")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si018a"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si018a"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjClaimBenef = New eClaim.ClaimBenef
lstrRole = ""

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.24 $|$$Author: Nvaplat60 $"

//%SelectedClientClaim: Selecciona el cliente y lo coloca en la pantalla SI018
//--------------------------------------------------------------------------------------------
function SelectedClientClaim(nIndex,ControlName,ControlClieName){
//--------------------------------------------------------------------------------------------
	with(opener.document.forms[0]){
		elements[ControlName].value = marrArray[nIndex].tctClient; 
		elements[ControlName + '_Digit'].value = marrArray[nIndex].hddsDigit;
		elements[ControlName + '_Old'].value = marrArray[nIndex].tctClient; 
		elements[ControlName + '_Digit'].value = marrArray[nIndex].hddsDigit;
		elements[ControlName + '_Digit_Old'].value = marrArray[nIndex].hddsDigit;
		
		if(ControlClieName!="" && typeof(opener.document.getElementById(ControlClieName))!="undefined"){ 
		    opener.document.getElementById(ControlClieName).innerHTML = marrArray[nIndex].tctCliename;
		}
	}
	<%
If Request.QueryString("sOnChange") <> vbNullString Then
	Response.Write("opener." & Request.QueryString("sOnChange") & ";")
End If
%>
}

//%	CloseWindow: Cierra la ventana
//------------------------------------------------------------------------------------------- 
function CloseWindow(){
//------------------------------------------------------------------------------------------- 
    window.close(); 
}
</SCRIPT>
<%=mobjValues.StyleSheet()%>        
<SCRIPT>var nMainAction = 302</SCRIPT>
</HEAD>
<BODY>
<FORM METHOD="POST" ID="FORM" NAME="frmClientClaimSel" ACTION="SI018A.aspx?ControlName=<%=Request.QueryString("ControlName")%>&ControlClieName=<%=Request.QueryString("ControlClieName")%>">
    <%Response.Write(mobjValues.ShowWindowsName("SI018A", Request.QueryString("sWindowDescript")))

Call insDefineHeader()

If Request.QueryString("Type") <> "PopUp" Then
	Call insPreSI018A()
End If

'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mcol may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mcol = Nothing
'UPGRADE_NOTE: Object mobjClaimBenef may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjClaimBenef = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("si018a")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




