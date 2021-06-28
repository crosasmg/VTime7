<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'es-: Objeto para el manejo de las funciones generales de carga de valores
'en-: Load objects that include general functions to create textBoxes
'en-: menus, and controls
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim oPage As Object
Dim oPageContent As Object


</script>

<%
Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

'UPGRADE_NOTE: The 'eVdata.Page' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
oPage = Server.CreateObject("eVdata.Page")
Call oPage.Find(Request.QueryString.Item("sCodispl"))
Session("sCodisp") = "VDATA"

%>

<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTimeNet/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>


<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tmenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">

<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & "_k.aspx", 1, ""))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>

<SCRIPT>

//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone() {
//-------------------------------------------------------------------------------------------    

<%
For	Each oPageContent In oPage.mcPageContents
	With oPageContent
		If .bHeader Then
			%>
				self.document.forms[0].<%=.sFieldName%>.disabled=false
<%			
		End If
	End With
Next oPageContent
%>

}

//es%: insCancel: se ejecuta la acción Cancelar de la página
//en%: insCancel: to cancel page
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
	return(true);
}

</SCRIPT>
</HEAD>
<BODY CLASS="Header" ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="POST" ID="VData_k" NAME="VData_k" ACTION="ValVData.aspx?time=1">

<TABLE WIDTH="100%">
<TR>
<%
For	Each oPageContent In oPage.mcPageContents
	With oPageContent
		If .bHeader Then
			Response.Write("<TD>" & oPageContent.sCaption & "</TD>")
			
			If oPageContent.sLookupTable > vbNullString Or oPageContent.bListValues = True Then
				
				If oPageContent.sLookupTable > vbNullString Then
					Response.Write("<TD>" & mobjValues.PossiblesValues(oPageContent.sFieldName, "TABLE" & oPageContent.sLookupTable, 1, Session(oPageContent.sFieldName)) & "</TD>")
				Else
					mobjValues.Parameters.Add("sCode", Request.QueryString.Item("sCodispl"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					mobjValues.Parameters.Add("sFieldName", oPageContent.sFieldName, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					Response.Write("<TD>" & mobjValues.PossiblesValues(oPageContent.sFieldName, "VDATA_List", 1, Session(oPageContent.sFieldName), True) & "</TD>")
				End If
				
			Else
				Select Case oPageContent.nType
					Case 0 'bit
						Response.Write("<TD>" & mobjValues.CheckControl(oPageContent.sFieldName, "") & "</TD>")
					Case 1 'char
						Response.Write("<TD>" & mobjValues.TextControl(oPageContent.sFieldName, oPageContent.nLength, Session(oPageContent.sFieldName)) & "</TD>")
					Case 2 'datetime
						Response.Write("<TD>" & mobjValues.DateControl(oPageContent.sFieldName, "", oPageContent.bRequired, oPageContent.sToolTip) & "</TD>")
					Case 3 'decimal
						Response.Write("<TD>" & mobjValues.NumericControl(oPageContent.sFieldName, oPageContent.nLength, Session(oPageContent.sFieldName), oPageContent.bRequired, oPageContent.sToolTip) & "</TD>")
					Case 4 'int
						Response.Write("<TD>" & mobjValues.NumericControl(oPageContent.sFieldName, 5, Session(oPageContent.sFieldName), oPageContent.bRequired, oPageContent.sToolTip) & "</TD>")
					Case 5 'smallint
						Response.Write("<TD>" & mobjValues.NumericControl(oPageContent.sFieldName, 2, Session(oPageContent.sFieldName), oPageContent.bRequired, oPageContent.sToolTip) & "</TD>")
				End Select
			End If
		Else
			
		End If
	End With
Next oPageContent
%>

</TR>
</TABLE>
</BODY>
</HTML>

<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object oPage may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
oPage = Nothing
'UPGRADE_NOTE: Object oPageContent may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
oPageContent = Nothing
%>




