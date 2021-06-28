<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "MGE002_K"
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100683, GetLocalResourceObject("tcnFolderColumnCaption"), "tcnFolder", 6, CStr(eRemoteDB.Constants.intNull))
		Call .AddTextColumn(100684, GetLocalResourceObject("tctFolderNameColumnCaption"), "tctFolderName", 30, CStr(eRemoteDB.Constants.strNull))
		Call .AddTextColumn(100685, GetLocalResourceObject("tctRootNameColumnCaption"), "tctRootName", 30, CStr(eRemoteDB.Constants.strNull))
		Call .AddAnimatedColumn(0, GetLocalResourceObject("cbeImageColumnCaption"), "cbeImage", "/VTimeNet/images/btn_ValuesOff.png",  ,  , "ShowImages()")
		Call .AddHiddenColumn("tcnImage", "")
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(100683, GetLocalResourceObject("tctFolderKeyColumnCaption"), "tctFolderKey", 30, "")
			Call .AddHiddenColumn("cbeFolderKey", "")
		Else
			Call .AddPossiblesColumn(100683, GetLocalResourceObject("cbeFolderKeyColumnCaption"), "cbeFolderKey", "PropertyLibrary", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 30,  , eFunctions.Values.eTypeCode.eString)
		End If
		Call .AddTextColumn(100687, GetLocalResourceObject("tctClassColumnCaption"), "tctClass", 20, CStr(eRemoteDB.Constants.strNull))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		
		.Codispl = "MGE002"
		.Codisp = "MGE002_K"
		.Columns("tcnImage").GridVisible = False
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.DeleteButton = False
			.AddButton = False
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
			.Columns("tctFolderName").EditRecord = False
		Else
			.DeleteButton = True
			.AddButton = True
			.Columns("Sel").GridVisible = True
			.Columns("tctFolderName").EditRecord = True
		End If
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nFolder='+ marrArray[lintIndex].tcnFolder + '" & "&sFolderName='+ marrArray[lintIndex].tctFolderName + '" & "&sRootName='+ marrArray[lintIndex].tctRootName + '" & "&nImage='+ marrArray[lintIndex].tcnImage + '" & "&nFolderKey='+ marrArray[lintIndex].cbeFolderKey + '" & "&sClass='+ marrArray[lintIndex].tctClass + '"
		.Height = 280
		.Width = 450
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'% insPreMGE002_K: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreMGE002_K()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsFolder As eGeneralQue.Folder
	Dim lclsFolders As eGeneralQue.Folders
	Dim lintIndex As Short
	
	lclsFolder = New eGeneralQue.Folder
	lclsFolders = New eGeneralQue.Folders
	lintIndex = 0
	With Request
		If lclsFolders.Find() Then
			For	Each lclsFolder In lclsFolders
				With mobjGrid
					.Columns("tcnFolder").DefValue = CStr(lclsFolder.nFolder)
					.Columns("tctFolderName").DefValue = lclsFolder.sFolderName
					.Columns("tctRootName").DefValue = lclsFolder.sRootName
					.Columns("tcnImage").DefValue = CStr(lclsFolder.nImage)
					.Columns("cbeImage").HRefScript = "ShowImages(" & CStr(lintIndex) & ")"
					.Columns("cbeFolderKey").DefValue = CStr(lclsFolder.nFolderKey)
					.Columns("tctFolderKey").DefValue = lclsFolder.sFolderKey
					.Columns("tctClass").DefValue = lclsFolder.sClass
					Response.Write(mobjGrid.DoRow())
				End With
				lintIndex = lintIndex + 1
			Next lclsFolder
		End If
	End With
	Response.Write(mobjGrid.closeTable())
	lclsFolder = Nothing
	lclsFolders = Nothing
	
End Sub

'--------------------------------------------------------------------------------------------
Private Sub insPreMGE002_K_Upd()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsFolder As eGeneralQue.Folder
	Dim lstrErrors As String
	
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	With Request
		If .QueryString.Item("Action") = "Del" Then
			
			lclsFolder = New eGeneralQue.Folder
			
			lstrErrors = lclsFolder.insValMGE002_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nFolder"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sFolderName"), .QueryString.Item("sRootName"), mobjValues.StringToType(.QueryString.Item("nImage"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClass"), CInt(.QueryString.Item("nFolderKey")))
			
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				With lclsFolder
					.nFolder = mobjValues.StringToType(Request.QueryString.Item("nFolder"), eFunctions.Values.eTypeData.etdDouble)
					.sFolderName = Request.QueryString.Item("sFolderName")
					.sRootName = Request.QueryString.Item("sRootName")
					.nImage = mobjValues.StringToType(Request.QueryString.Item("nImage"), eFunctions.Values.eTypeData.etdDouble)
					.nFolderKey = CInt(Request.QueryString.Item("nFolderKey"))
					.sClass = Request.QueryString.Item("sClass")
					.Delete()
				End With
			Else
				Response.Write(lstrErrors)
			End If
			lclsFolder = Nothing
		End If
	End With
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneralQue.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MGE002_K"

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
	<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\generalque\mantgeneralque\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%	
End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">





<SCRIPT>
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}

//-------------------------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

function ShowImages(lintIndex){
//-------------------------------------------------------------------------------------------
	if(typeof(marrArray)=='undefined')
		ShowPopUp("frmIconUpd.aspx?nImage=" + self.document.forms[0].tcnImage.value,"MGE002",500,165,"no","no",200,100);
	else
		ShowPopUp("frmIconUpd.aspx?nImage=" + marrArray[lintIndex].tcnImage,"MGE002",500,165,"no","no",200,100);
}
</SCRIPT>
<%

With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("MGE002"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "MGE002_K.aspx"))
		.Write(mobjMenu.MakeMenu("MGE002", "MGE002_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="post" ID="FORM" NAME="frmFolders" ACTION="valMantGeneralQue.aspx?sZone=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMGE002_K()
Else
	Call insPreMGE002_K_Upd()
End If
mobjMenu = Nothing
mobjValues = Nothing
mobjGrid = Nothing


%>
</FORM>
</BODY>
</HTML>




