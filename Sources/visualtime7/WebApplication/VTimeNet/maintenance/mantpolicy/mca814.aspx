<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolchange_mods As ePolicy.Change_mods


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "mca814"
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeModul_oriColumnCaption"), "cbeModul_ori", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeModul_oriColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeModul_endColumnCaption"), "cbeModul_end", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeModul_endColumnToolTip"))
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddCheckColumn(1, GetLocalResourceObject("chkSidemcapColumnCaption"), "chkSidemcap", "",  ,  ,  , True, GetLocalResourceObject("chkSidemcapColumnToolTip"))
			Call .AddCheckColumn(2, GetLocalResourceObject("chkSidempremColumnCaption"), "chkSidemprem", "",  ,  ,  , True, GetLocalResourceObject("chkSidempremColumnToolTip"))
			Call .AddCheckColumn(3, GetLocalResourceObject("chkSidemdeducColumnCaption"), "chkSidemdeduc", "",  ,  ,  , True, GetLocalResourceObject("chkSidemdeducColumnToolTip"))
		Else
			Call .AddCheckColumn(1, GetLocalResourceObject("chkSidemcapColumnCaption"), "chkSidemcap", "",  ,  ,  , False, GetLocalResourceObject("chkSidemcapColumnToolTip"))
			Call .AddCheckColumn(2, GetLocalResourceObject("chkSidempremColumnCaption"), "chkSidemprem", "",  ,  ,  , False, GetLocalResourceObject("chkSidempremColumnToolTip"))
			Call .AddCheckColumn(3, GetLocalResourceObject("chkSidemdeducColumnCaption"), "chkSidemdeduc", "",  ,  ,  , False, GetLocalResourceObject("chkSidemdeducColumnToolTip"))
		End If
		
		Call .AddHiddenColumn("nBranch", Request.QueryString.Item("nBranch"))
		Call .AddHiddenColumn("nProduct", Request.QueryString.Item("nProduct"))
		Call .AddHiddenColumn("dEffecdate", Request.QueryString.Item("dEffecdate"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Columns("cbeModul_ori").Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeModul_ori").Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeModul_ori").Parameters.Add("dEffecdate", Request.QueryString.Item("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("cbeModul_end").Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeModul_end").Parameters.Add("nProduct", Request.QueryString.Item("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeModul_end").Parameters.Add("dEffecdate", Request.QueryString.Item("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Codispl = "MCA814"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbeModul_ori").EditRecord = True
		.Height = 300
		.Width = 410
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") 
     	
        .sDelRecordParam = "nBranch=" & Request.QueryString("nBranch") & 						   "&nProduct=" & Request.QueryString("nProduct") & 						   "&dEffecdate=" & Request.QueryString("dEffecdate") & 						   "&nModul_ori='+ marrArray[lintIndex].cbeModul_ori + '"		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMCA814: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA814()
	'--------------------------------------------------------------------------------------------
	Dim lclschange_mod As Object
	
	mcolchange_mods = New ePolicy.Change_mods
	
	If mcolchange_mods.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclschange_mod In mcolchange_mods
			With mobjGrid
				.Columns("cbeModul_ori").DefValue = lclschange_mod.nModul_ori
				.Columns("cbeModul_end").DefValue = lclschange_mod.nModul_end
				.Columns("chkSidemcap").Checked = lclschange_mod.Sidemcap
				.Columns("chkSidemprem").Checked = lclschange_mod.Sidemprem
				.Columns("chkSidemdeduc").Checked = lclschange_mod.Sidemdeduc
				Response.Write(.DoRow)
			End With
		Next lclschange_mod
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolchange_mods = Nothing
End Sub

'% insPreMCA814Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA814Upd()
	'--------------------------------------------------------------------------------------------
	'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
	Dim lobjClass As ePolicy.Change_mod
	lobjClass = New ePolicy.Change_mod
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.InsPostMCA814(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModul_ori"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull), CStr(eRemoteDB.Constants.strnull)) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantPolicy.aspx", "MCA814", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "mca814"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16:15 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MCA814", "MCA814.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MCA814" ACTION="valMantPolicy.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MCA814"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCA814Upd()
Else
	Call insPreMCA814()
End If
%>
</FORM> 
</BODY>
</HTML>





