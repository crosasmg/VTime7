<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefinerHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cac007"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	With mobjGrid.Columns
		Call .AddHiddenColumn("cbeBranch", "")
		Call .AddTextColumn(40643, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "",  , GetLocalResourceObject("tctBranchColumnToolTip"))
	End With
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "CAC007"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = True
	End With
End Sub
'% ShowBranches: Se cargan los valores en el grid.
'--------------------------------------------------------------------------------------------
Private Sub ShowBranches()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lcolPolicys As ePolicy.Policys
	Dim lobjObject As Object
	Dim lclsGeneral As eGeneral.GeneralFunction
	lclsGeneral = New eGeneral.GeneralFunction
	Response.Write("<SCRIPT>mstrMessage='" & lclsGeneral.insLoadMessage(3895) & "';</" & "Script>")
	lcolPolicys = New ePolicy.Policys
	If lcolPolicys.reaTable10 Then
		lintCount = 0
		For	Each lobjObject In lcolPolicys
			With lobjObject
				mobjGrid.Columns("cbeBranch").DefValue = .nBranch
				mobjGrid.Columns("tctBranch").DefValue = .sDescript
				Response.Write(mobjGrid.DoRow())
			End With
			lintCount = lintCount + 1
			If lintCount = 200 Then
				Exit For
			End If
		Next lobjObject
		lcolPolicys = Nothing
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write("<TABLE WIDTH='100%'>")
	Response.Write("<TR>")
	Response.Write("<TD COLSPAN=3 CLASS='HORLINE'></TD>")
	Response.Write("</TR>")
	Response.Write("<TR>")
	Response.Write("<TD ALIGN='left'>")
	Response.Write(mobjValues.ButtonAbout("CAC007"))
	Response.Write(mobjValues.ButtonHelp("CAC007"))
	Response.Write("<TD>")
	Response.Write("<TD ALIGN='right'>")
	Response.Write(mobjValues.ButtonAcceptCancel("insConstruct()", "insCloseWindows()", False,  , eFunctions.Values.eButtonsToShow.All))
	Response.Write("</TD>")
	Response.Write("</TR>")
	Response.Write("</TABLE>")
	lcolPolicys = Nothing
	lobjObject = Nothing
	lclsGeneral = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac007")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac007"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("CAC007", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.WindowsTitle("CAC007", Request.QueryString.Item("sWindowDescript")))
End With
%>
<SCRIPT>
	var mstrMessage = ""
	var mstrIndex = ""
	
//% insConstruct: permite contruir la cadena para la consulta
//------------------------------------------------------------------------------------------------
function insConstruct(){
//------------------------------------------------------------------------------------------------
	var lstrCondition = " Address.nBranch in("
	
	for(var lintIndex = 0;lintIndex<marrArray.length;lintIndex++){
		if(marrArray[lintIndex].Sel==true){
			if(mstrIndex =="")
				mstrIndex+= marrArray[lintIndex].cbeBranch
			else							
				mstrIndex+= ","	+ marrArray[lintIndex].cbeBranch 
		}
	}
	lstrCondition+= mstrIndex + ")"
	if(mstrIndex=="")
		alert("Err. 3895: " + mstrMessage)
	else{
		opener.document.forms[0].sBranchCondition.value = lstrCondition
		window.close()
	}
}
//% insCloseWindows: Cierra la popup
//------------------------------------------------------------------------------------------------
function insCloseWindows(){
//------------------------------------------------------------------------------------------------
	if(mstrIndex==""){
		opener.document.forms[0].optBranch[0].checked = true
	}
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
Call ShowBranches()
mobjGrid = Nothing
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac007")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




