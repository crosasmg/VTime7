<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefinerHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsWindows As eSecurity.Windows
	Dim lstrImg_index As String
	Dim lstrImage As String
	Dim lstrSrc_Image As String
	Dim llngIndex As Integer
	
	lclsWindows = New eSecurity.Windows
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "SG098_K"
	
	With mobjGrid.Columns
		lstrImg_index = "nImg_index"
		lstrSrc_Image = "nSrc_Image"
		lstrImage = "Images"
		
		For llngIndex = 1 To lclsWindows.nQuantImages
			Call .AddHiddenColumn(lstrImg_index, CStr(0))
			Call .AddHiddenColumn(lstrSrc_Image, "")
			Call .AddAnimatedColumn(0, " ", lstrImage)
			
			lstrImg_index = "nImg_index" & CStr(llngIndex)
			lstrSrc_Image = "nSrc_Image" & CStr(llngIndex)
			lstrImage = "Images" & CStr(llngIndex)
		Next 
	End With
	
	lclsWindows = Nothing
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "SG098"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% ShowImages: Se cargan las imágenes en el grid.
'--------------------------------------------------------------------------------------------
Private Sub ShowImages()
	'--------------------------------------------------------------------------------------------
	Dim llngIndex As Integer
	Dim lclsWindows As eSecurity.Windows
	Dim lstrImg_index As String
	Dim lstrImage As String
	Dim lstrSrc_Image As String
	
	lclsWindows = New eSecurity.Windows
	
	With lclsWindows
		lstrImg_index = "nImg_index"
		lstrSrc_Image = "nSrc_Image"
		lstrImage = "Images"
		
		For llngIndex = 1 To .nQuantImages
			mobjGrid.Columns(lstrImg_index).DefValue = CStr(llngIndex)
			mobjGrid.Columns(lstrImage).Src = .PathImages(llngIndex)
			mobjGrid.Columns(lstrSrc_Image).DefValue = mobjGrid.Columns(lstrImage).Src
			mobjGrid.Columns(lstrImage).HRefScript = "insAccept('" & lstrSrc_Image & "'," & CStr(llngIndex) & ")"
			
			lstrImg_index = "nImg_index" & CStr(llngIndex)
			lstrSrc_Image = "nSrc_Image" & CStr(llngIndex)
			lstrImage = "Images" & CStr(llngIndex)
		Next 
		
		Response.Write(mobjGrid.DoRow())
	End With
	
	lclsWindows = Nothing
	
	Response.Write(mobjGrid.closeTable())
	
	Response.Write(mobjValues.ButtonAcceptCancel( , "insCloseWindows()", False,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG098_K"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("SG098"))
	.Write(mobjValues.WindowsTitle("SG098"))
End With
%>
	
<SCRIPT>
	var mstrMessage = ""
	var mstrIndex = ""
	
//% insAccept: Permite guardar en una columna
//------------------------------------------------------------------------------------------------
function insAccept(lstrImage, nIndex){
//------------------------------------------------------------------------------------------------
    opener.document.forms[0].nImage_index.value = nIndex
    opener.document.forms[0].btnImg_index.src   = marrArray[0][lstrImage]

    window.close()
}
//% insCloseWindows: 
//------------------------------------------------------------------------------------------------
function insCloseWindows(){
//------------------------------------------------------------------------------------------------
	window.close()
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="insCloseWindows();">
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>

<%
Call insDefinerHeader()
Call ShowImages()

mobjValues = Nothing
%>





