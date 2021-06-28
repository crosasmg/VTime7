<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">

Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefinerHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsFolder As eGeneralQue.Folder
	Dim lstrImg_index As String
	Dim lstrImage As String
	Dim lstrSrc_Image As String
	Dim llngIndex As Integer
	
	lclsFolder = New eGeneralQue.Folder
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid.Columns
		lstrImg_index = "nImg_index"
		lstrSrc_Image = "nSrc_Image"
		lstrImage = "Images"
		
		For llngIndex = 1 To lclsFolder.nQuantImages
			Call .AddHiddenColumn(lstrImg_index, CStr(0))
			Call .AddHiddenColumn(lstrSrc_Image, "")
			Call .AddAnimatedColumn(0, " ", lstrImage)
			
			lstrImg_index = "nImg_index" & CStr(llngIndex)
			lstrSrc_Image = "nSrc_Image" & CStr(llngIndex)
			lstrImage = "Images" & CStr(llngIndex)
		Next 
	End With
	
	lclsFolder = Nothing
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "MGE002"
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
	Dim lclsFolder As eGeneralQue.Folder
	Dim lstrImg_index As String
	Dim lstrImage As String
	Dim lstrSrc_Image As String
	
	lclsFolder = New eGeneralQue.Folder
	
	If (Request.QueryString.Item("nImage") <> vbNullString And Request.QueryString.Item("nImage") <> CStr(eRemoteDB.Constants.intNull)) Then
		lstrSrc_Image = lclsFolder.PathImages(CInt(Request.QueryString.Item("nImage")))
		Response.Write("<LABEL ID=100683> " & GetLocalResourceObject("AnchorCaption") & " </LABEL>" & "    " & mobjValues.AnimatedButtonControl("btnVp_Image", lstrSrc_Image))
	End If
	
	With lclsFolder
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
	
	lclsFolder = Nothing
	
	Response.Write(mobjGrid.closeTable())
	Response.Write("<P Align=Right>")
	Response.Write(mobjValues.ButtonAcceptCancel( , "insCloseWindows()", False,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
	Response.Write("</P>")
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("MGE002"))
	.Write(mobjValues.WindowsTitle("MGE002"))
End With
%>
	
<SCRIPT>
	var mstrMessage = ""
	var mstrIndex = ""
	
//% insAccept: Permite guardar en una columna
//------------------------------------------------------------------------------------------------
function insAccept(lstrImage, nIndex){
//------------------------------------------------------------------------------------------------
    opener.document.forms[0].tcnImage.value = nIndex
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





