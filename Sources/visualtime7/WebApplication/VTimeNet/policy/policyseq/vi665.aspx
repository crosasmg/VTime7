<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo del grid.
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsActiv_Group As ePolicy.Activ_Group
	Dim lclsGroupss As ePolicy.Groupss
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	lclsActiv_Group = New ePolicy.Activ_Group
	lclsGroupss = New ePolicy.Groupss
	'+ Se definen las columnas del grid.
	With mobjGrid.Columns
		If lclsGroupss.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate")) Then
			Call .AddPossiblesColumn(100406, GetLocalResourceObject("cbeGroupColumnCaption"), "cbeGroup", "TabGroups", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("cbeGroupColumnCaption"))
		Else
			Call .AddPossiblesColumn(100406, GetLocalResourceObject("cbeGroupColumnCaption"), "cbeGroup", "TabGroups", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeGroupColumnCaption"))
		End If
		mobjGrid.Columns("cbeGroup").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeGroup").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeGroup").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		mobjGrid.Columns("cbeGroup").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddPossiblesColumn(100406, GetLocalResourceObject("cbeSpecialityColumnCaption"), "cbeSpeciality", "Table16", eFunctions.Values.eValuesType.clngWindowType, "", False,  ,  ,  , "", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("cbeSpecialityColumnToolTip"))
		Call .AddNumericColumn(100408, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5,  ,  , GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.AddButton = True
		.DeleteButton = True
		.Height = 210
		.Width = 430
		.Codispl = "VI665"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sDelRecordParam = "&sCertype='+ marrArray[lintIndex].sCertype + '" & "&nBranch='+ marrArray[lintIndex].nBranch + '" & "&nProduct='+ marrArray[lintIndex].nProduct + '" & "&nPolicy='+ marrArray[lintIndex].nPolicy + '" & "&dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&cbeGroup='+marrArray[lintIndex].cbeGroup + '" & "&cbeSpeciality='+marrArray[lintIndex].cbeSpeciality + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.Columns("cbeSpeciality").EditRecord = True
	End With
	
	lclsActiv_Group = Nothing
	lclsGroupss = Nothing
End Sub

'% insPreVI665: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI665()
	'--------------------------------------------------------------------------------------------
	Dim lblnDataFound As Boolean
	Dim lintIndex As Short
	Dim lcolActiv_Groups As ePolicy.Activ_Groups
	Dim lclsActiv_Group As ePolicy.Activ_Group
	
	lintIndex = 0
	lcolActiv_Groups = New ePolicy.Activ_Groups
	lclsActiv_Group = New ePolicy.Activ_Group
	
	lblnDataFound = lcolActiv_Groups.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"))
	If lblnDataFound Then
		For	Each lclsActiv_Group In lcolActiv_Groups
			With mobjGrid
				' + Se asignan los valores de las columnas del grid
				.Columns("cbeGroup").DefValue = CStr(lclsActiv_Group.nGroup)
				.Columns("cbeSpeciality").DefValue = CStr(lclsActiv_Group.nSpeciality)
				.Columns("tcnPercent").DefValue = CStr(lclsActiv_Group.nPercent)
				Response.Write(.DoRow)
				lintIndex = lintIndex + 1
				
				If lintIndex = 200 Then
					Exit For
				End If
			End With
		Next lclsActiv_Group
	End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TD VALIGN=TOP>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("              ")

	
	With mobjValues
		Call .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("              ")

	
	With mobjValues
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("dEffecdate", Session("deffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>                   " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TD>" & vbCrLf)
Response.Write("</TABLE>")

	
	Response.Write(mobjGrid.closeTable)
	lcolActiv_Groups = Nothing
	lclsActiv_Group = Nothing
End Sub

'% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreVI665Upd()
	'-----------------------------------------------------------------------------------------
	
	Dim lclsActiv_Group As ePolicy.Activ_Group
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		lclsActiv_Group = New ePolicy.Activ_Group
		Call lclsActiv_Group.InsPostVI665Upd("Del", 0, Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbeGroup"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbeSpeciality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		lclsActiv_Group = Nothing
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI665")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Session("dbgFrm") = Request.Form.ToString
Session("dbgQry") = Request.Params.Get("Query_String")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<script LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"
/*---------------------------------------------------------------------------------------------------------*/
</script>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI665", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VI665" ACTION="ValPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nCurrency=<%=Request.Form.Item("cbeCurrency")%>">
<P>
<%
Response.Write(mobjValues.ShowWindowsName("VI665", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
mobjGrid.ActionQuery = Session("bQuery")
mobjValues.ActionQuery = Session("bQuery")
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI665Upd()
Else
	Call insPreVI665()
End If
mobjValues = Nothing
mobjGrid = Nothing

%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI665")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




