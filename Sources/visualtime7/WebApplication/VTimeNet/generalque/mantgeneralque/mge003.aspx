<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralQue" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mcolFolders As eGeneralQue.Folders
Dim mobjMenu As eFunctions.Menues


'% insShowNames:
'------------------------------------------------------------------------------
Private Function insShowNames() As Object
	'------------------------------------------------------------------------------
	Dim lcolFolders As eGeneralQue.Folders
	Dim lclsFolder As Object
	
	lcolFolders = New eGeneralQue.Folders
	Response.Write("<SCRIPT>function insName(nID,nAction){ var lstrName="""" + nID; var lstrImage=""""")
	
	If lcolFolders.Find() Then
		For	Each lclsFolder In lcolFolders
			Response.Write(vbCrLf & " if (nID==" & lclsFolder.nFolder & ") {lstrName=""" & lclsFolder.sRootName & """; " & insPutImage(lclsFolder.nImage) & " }")
		Next lclsFolder
	End If
	Response.Write(vbCrLf & " return (nAction!=0?lstrName:lstrImage);}")
	Response.Write("</" & "Script>")
	
	lcolFolders = Nothing
	lclsFolder = Nothing
End Function

'% insPutImage:
'------------------------------------------------------------------------------
Private Function insPutImage(ByRef nImage As Object) As String
	'------------------------------------------------------------------------------
	Dim lstrImage As Object
	
	If nImage <> 0 And nImage <> eRemoteDB.Constants.intNull Then
		Select Case nImage
			Case 10 ' Poliza
				insPutImage = "lstrImage = ""/VTimeNet/images/GenQue10.gif"""
			Case 11 ' Cliente
				insPutImage = "lstrImage = ""/VTimeNet/images/FindClientOn.png"""
			Case 13 ' Intermediarios
				insPutImage = "lstrImage = ""/VTimeNet/images/batchStat05.png"""
			Case 15
				insPutImage = "lstrImage = ""/VTimeNet/images/GenQue15.gif"""
			Case 16
				insPutImage = "lstrImage = ""/VTimeNet/images/GenQue16.gif"""
			Case 17 ' Siniestros
				insPutImage = "lstrImage = ""/VTimeNet/images/DMESINT.gif"""
			Case 19
				insPutImage = "lstrImage = ""/VTimeNet/images/GenQue19.gif"""
			Case 21
				insPutImage = "lstrImage = ""/VTimeNet/images/GenQue21.gif"""
			Case Else
				insPutImage = vbNullString
		End Select
	Else
		insPutImage = vbNullString
	End If
End Function

'% insShowSeqFolder: Esta funcion se encarga de mostrar las carpetas a mostara por tipo de
'%                   consulta
'--------------------------------------------------------------------------------------------
Private Function insShowSeqFolder(ByVal lintqueryType As Object, ByVal lintParent As Object, ByVal lstrHierarchy As String, ByVal lstrVarName As String) As Object
	'--------------------------------------------------------------------------------------------
	Dim lintBreak As Short
	Dim lobjSeqfolder As Object
	Dim lobjSeqFolders As eGeneralQue.SeqFolders
	Dim lobjFolder As eGeneralQue.Folder
	Dim lblnFirst As Boolean
	Dim lblnParent As Boolean
	lblnFirst = True
	lblnParent = False
	lintBreak = 0
	lobjSeqFolders = New eGeneralQue.SeqFolders
	
	If lintParent = 0 Then
		lblnParent = True
		Response.Write("<SCRIPT>initializeTree(insName('" & CStr(lintqueryType) & "',1),insName('" & CStr(lintqueryType) & "',0),'" & CStr(lintqueryType) & "','C1')" & vbCrLf)
		lstrVarName = "lC"
	End If
	
	If lobjSeqFolders.Find(lintqueryType, lintParent) Then
		'+Se recorren todos los hijos de la carpeta en tratamiento (lintParent)
		For	Each lobjSeqfolder In lobjSeqFolders
			On Error Resume Next
			lobjFolder = mcolFolders("A" & lobjSeqfolder.nFolder)
			On Error GoTo 0
			lintBreak = lintBreak + 1
			If lintParent <> 0 Then
				If lblnFirst Then
					lstrVarName = lstrVarName & lintParent
					lblnFirst = False
				End If
				Response.Write("var " & lstrVarName & lobjFolder.nFolder & "= insAddFolder(" & CStr(lobjFolder.nFolder) & "," & CStr(lobjFolder.nFolder) & ",false," & lstrVarName & ");" & vbCrLf)
			Else
				Response.Write("var " & lstrVarName & lintqueryType & " = CurrentFolder;" & vbCrLf)
				lintParent = lintqueryType
			End If
			If lintBreak < 60 And CDbl(0 & InStr(1, lstrHierarchy, "-" & CStr(lobjFolder.nFolder) & "-")) <= 0 Then
				Call insShowSeqFolder(lintqueryType, lobjFolder.nFolder, lstrHierarchy, lstrVarName)
			End If
		Next lobjSeqfolder
	End If
	
	If lblnParent Then
		Response.Write("redrawTree(); </" & "Script>")
	End If
End Function

</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mcolFolders = New eGeneralQue.Folders

Call mcolFolders.Find()

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.BlankPosition = False
mobjValues.sCodisplPage = "MGE003"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="Scripts/MantGeneralQue.js"></SCRIPT>




    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "MGE003", "MGE003.aspx"))
End With
Session("bQuery") = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)
Call insShowNames()
%>
<SCRIPT>
//% initializeTree: 
//-------------------------------------------------------------------------------------------------------------
function initializeTree(sName, sImagesSrc, sParams, sKey){
//-------------------------------------------------------------------------------------------------------------
    generateTree(sName, sImagesSrc, sParams, sKey)
    CurrentFolder = foldersTree
}
//% generateTree:
//-------------------------------------------------------------------------------------------------------------
function generateTree(sName, sImagesSrc, sParams, sKey){
//-------------------------------------------------------------------------------------------------------------
    foldersTree = folderNode(sName, sImagesSrc, sImagesSrc, 1, sParams, sKey,0)
}
//% insAddRemove:
//-------------------------------------------------------------------------------------------------------------
function insAddRemove(nAction){
//-------------------------------------------------------------------------------------------------------------
    var lintIndex=0

//+ Agregar (Valor diferente de cero)
    if (nAction){
        with (document.forms[0].cbeFolder){
	    	for (lintIndex=0;lintIndex<options.length;lintIndex++)
        	    if (options[lintIndex].selected){
        	        insAddFolder(options[lintIndex].value,options[lintIndex].value)
        	        options[lintIndex].selected = false
        	    }
	    }
	}
	else{
        RemoveFolders(foldersTree,0)
        redrawTree()
	}
    document.forms[0].sTree.value = insCreateHiddenValue(foldersTree,0)
}
//% insAddFolder:
//-------------------------------------------------------------------------------------------------------------
function insAddFolder(Name, Key, Redraw, OnThisFolder){
//-------------------------------------------------------------------------------------------------------------
    var lobjBC003_K
    var lintFolder
    var lstrImagesSrc=insName(Name,0)
    var lstrFolder=insName(Name)
    lintFolder=Key
    if (typeof(Redraw)=='undefined')Redraw=true
    if (typeof(Key)=='undefined')Key=Name
    if (typeof(CurrentFolder)=='undefined') CurrentFolder = foldersTree
    if (typeof(lstrImagesSrc)=='undefined') lstrImagesSrc = ''
    if (typeof(OnThisFolder)!='undefined') CurrentFolder = OnThisFolder
    Key=CurrentFolder[6] + '*'+ Key
    lobjBC003_K = appendChild(CurrentFolder, folderNode(lstrFolder,lstrImagesSrc,lstrImagesSrc,1,lintFolder,Key,CurrentFolder[6]))

    if (Redraw) redrawTree()

    return lobjBC003_K
}
//% insCreateHiddenValue:
//-------------------------------------------------------------------------------------
function insCreateHiddenValue(FolderNode, nParent){
//-------------------------------------------------------------------------------------
    var lintIndex=0
    var lstrString=""
    if (typeof(nParent)=='undefined') nParent=0
    if (nParent==0){
        document.forms[0].sTree.value="";
        lstrString = "0-" + foldersTree[5] + ",";
        nParent = foldersTree[5];
    }
    if (FolderNode.length>9){
        for (lintIndex=9;lintIndex<FolderNode.length;lintIndex++){
            lstrString += nParent + "-" + FolderNode[lintIndex][5] + ","
            lstrString += insCreateHiddenValue(FolderNode[lintIndex],FolderNode[lintIndex][5])
        }
    }
    return lstrString
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmClassSeqFolder" ACTION="valMantGeneralQue.aspx?mode=1">
	<%=mobjValues.ShowWindowsName("MGE003", Request.QueryString.Item("sWindowDescript"))%>
	<TABLE COLS=3 WIDTH="100%">
	    <TR>
	        <TD CLASS="HighLighted"><LABEL ID=100780><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
	        <TD></TD>
	        <TD CLASS="HighLighted"><LABEL ID=100781><%= GetLocalResourceObject("cbeFolderCaption") %></LABEL></TD>
	    </TR>
	    <TR>
	        <TD CLASS="HorLine"></TD>
	        <TD></TD>
	        <TD CLASS="HorLine"></TD>
	    </TR>
	    <TR>
			<TD ALIGN="CENTER">
<%
mobjValues.ActionQuery = False
Response.Write(mobjValues.PossiblesValues("cbeFolder", "TabFolders", eFunctions.Values.eValuesType.clngComboType, vbNullString, False, False,  ,  , 15,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeFolderToolTip")))
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
Response.Write(mobjValues.HiddenControl("sTree", vbNullString))
%>
			</TD>
			<TD ALIGN="CENTER">
			    <TABLE CELLSPACING="10" CELLPADDING="0" WIDTH="100%">
			        <TR><TD ALIGN="CENTER"><%=mobjValues.AnimatedButtonControl("cmdAdd", "/VTimeNet/images/btnLargeNextOff.png", GetLocalResourceObject("cmdAddToolTip"),  , "insAddRemove(1)", CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)%></TD></TR>
			        <TR><TD ALIGN="CENTER"><%=mobjValues.AnimatedButtonControl("cmdRemove", "/VTimeNet/images/btnLargeBackOff.png", GetLocalResourceObject("cmdRemoveToolTip"),  , "insAddRemove(0)", CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)%></TD></TR>
			    </TABLE>
			</TD>
			<TD ALIGN="CENTER">
			<DIV ID=Treezone style="width:270;height:250;border-style:solid;border-color:gray;border-width:thin;background-color:white;overflow:auto; outset gray"></DIV>
			</TD>
	    </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
Call insShowSeqFolder(Session("nQueryTyp"), 0, vbNullString, vbNullString)
mobjValues = Nothing


%>




