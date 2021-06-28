<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "DP8003"
	
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100408, GetLocalResourceObject("tcnAge_initColumnCaption"), "tcnAge_init", 3, "",  , GetLocalResourceObject("tcnAge_initColumnCaption"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(100409, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 3, "",  , GetLocalResourceObject("tcnAge_endColumnCaption"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(100410, GetLocalResourceObject("tcnCapminiColumnCaption"), "tcnCapmini", 18, "",  , GetLocalResourceObject("tcnCapminiColumnToolTip"), True, 6)
		Call .AddNumericColumn(100411, GetLocalResourceObject("tcnCapmaximColumnCaption"), "tcnCapmaxim", 18, "",  , GetLocalResourceObject("tcnCapmaximColumnToolTip"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP8003"
		.Width = 400
		.Height = 250
		.Columns("tcnAge_init").EditRecord = True
		
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = True
			.bOnlyForQuery = True
		End If
		
		.sEditRecordParam = "nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nModulec=" & Session("nModulec") & "&nCover=" & Session("nCover") & "&nRole=" & Session("nRole") & "&dEffecdate=" & Session("dEffecdate")
		
		.sDelRecordParam = "nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nModulec=" & Session("nModulec") & "&nCover=" & Session("nCover") & "&nRole=" & Session("nRole") & "&dEffecdate=" & Session("dEffecdate") & "&nAge_init='+ marrArray[lintIndex].tcnAge_init + '" & "&nAge_end='+ marrArray[lintIndex].tcnAge_end + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'% insPreDP01: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8003()
	'--------------------------------------------------------------------------------------------
	Dim lclsCapital_age As eProduct.Capital_age
	Dim lcolCapital_ages As eProduct.Capital_ages
	
	With Server
		lclsCapital_age = New eProduct.Capital_age
		lcolCapital_ages = New eProduct.Capital_ages
	End With
	
	If lcolCapital_ages.FindCapital_age(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdLong)) Then
		
		For	Each lclsCapital_age In lcolCapital_ages
			With mobjGrid
				.Columns("tcnAge_init").DefValue = CStr(lclsCapital_age.nAge_init)
				.Columns("tcnAge_end").DefValue = CStr(lclsCapital_age.nAge_end)
				.Columns("tcnCapmini").DefValue = CStr(lclsCapital_age.nCapmini)
				.Columns("tcnCapmaxim").DefValue = CStr(lclsCapital_age.nCapmaxim)
				
				Response.Write(.DoRow)
			End With
		Next lclsCapital_age
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsCapital_age = Nothing
	lcolCapital_ages = Nothing
End Sub

'% insPreDP010Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP8003Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsCapital_age As eProduct.Capital_age
	Dim nAction As Object
	
	lclsCapital_age = New eProduct.Capital_age
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		Response.Write(mobjValues.ConfirmDelete)
		
		Call lclsCapital_age.insPostDP8003(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nAge_end"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nAge_init"), eFunctions.Values.eTypeData.etdLong), 0, 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdLong))
		
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/RolesSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valRolesseq.aspx", "DP8003", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
	End With
	
	lclsCapital_age = Nothing
	
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = Session("bQuery")
mobjGrid.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "DP8003"
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP8003", "DP8003.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:04 $|$$Author: Nvaplat61 $"
    
</SCRIPT>	
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
With Response
	.Write("<FORM METHOD=""POST"" ID=""FORM"" NAME=""frmDP045"" ACTION=""valrolesseq.aspx?nMainAction=" & Request.QueryString.Item("nMai3") & """>")
	.Write(mobjValues.ShowWindowsName("DP8003"))
	.Write("<BR>")
End With

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP8003Upd()
Else
	Call insPreDP8003()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




