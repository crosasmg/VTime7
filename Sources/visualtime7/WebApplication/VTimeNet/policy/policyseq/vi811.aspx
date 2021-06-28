<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.16
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo general de la página
Dim mcolNopayroll As ePolicy.Nopayrolls
Dim bActionQuery As Boolean
Dim lclsProduct As eProduct.Product
Dim lclsPolicy As ePolicy.Policy
Dim lblnModul As Boolean
Dim lblnNopayroll As Boolean
Dim lclsGroups As ePolicy.Groups
Dim lblnGroups As Boolean
Dim lobjError As eFunctions.Errors


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.17
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddHiddenColumn("valGroups", vbNullString)
		Call .AddHiddenColumn("valModulec", vbNullString)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ChangeValues(this,""Cover"", '" & Request.QueryString.Item("Action") & "')", Request.QueryString.Item("Action") = "Update", 5, GetLocalResourceObject("valCoverColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valRoleColumnCaption"), "valRole", "tabTab_covrol3", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , True, 5, GetLocalResourceObject("valRoleColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQLifesColumnCaption"), "tcnQLifes", 5, vbNullString,  , GetLocalResourceObject("tcnQLifesColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "VI811"
		.ActionQuery = bActionQuery
		.Columns("valRole").EditRecord = True
		.Height = 230
		.Width = 420
		.WidthDelete = 440
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sEditRecordParam = "nGroups=' + document.forms[0].valGroup.value + '&nModulec=' + document.forms[0].valModule.value + '"
		.sDelRecordParam = "nGroups=' + document.forms[0].valGroup.value + '&nModulec=' + document.forms[0].valModule.value + '&nCover=' + marrArray[lintIndex].valCover + '&nRole=' + marrArray[lintIndex].valRole + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		With .Columns("valCover").Parameters
			.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If IsNothing(Request.QueryString.Item("nModulec")) Then
				.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Add("nModulec", mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
			.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		With .Columns("valRole").Parameters
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If IsNothing(Request.QueryString.Item("nModulec")) Then
				.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				.Add("nModulec", mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
		End With
	End With
End Sub

'% insPreVI811: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI811()
	'--------------------------------------------------------------------------------------------
	Dim lclsNopayroll As Object
	mcolNopayroll = New ePolicy.Nopayrolls
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("        	<TD><LABEL ID=13052>" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nGroups"), True,  ,  ,  ,  , "ChangeValues(this, ""Group"")", lblnNopayroll Or lblnGroups, 5, GetLocalResourceObject("valGroupToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valModuleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nGroup", mobjValues.StringToType(Request.QueryString.Item("nGroups"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valModule", "tabTabModul_CO_PG", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nModulec"), True,  ,  ,  ,  , "ChangeValues(this, ""Modulec"")", lblnModul, 5, GetLocalResourceObject("valModuleToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	If mcolNopayroll.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGroups"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsNopayroll In mcolNopayroll
			With mobjGrid
				.Columns("valCover").DefValue = lclsNopayroll.nCover
				.Columns("valRole").Parameters.Add("nCover", lclsNopayroll.nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valRole").DefValue = lclsNopayroll.nRole
				.Columns("tcnQLifes").DefValue = lclsNopayroll.nQLifes
				
				Response.Write(.DoRow)
			End With
		Next lclsNopayroll
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreVI811Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI811Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsNopayroll As ePolicy.Nopayroll
	lclsNopayroll = New ePolicy.Nopayroll
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			If lclsNopayroll.inspostVI811(.QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nGroups"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
				Response.Write(mobjValues.ConfirmDelete())
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
			End If
		End If
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI811", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
			
			If .QueryString.Item("Action") <> "Del" Then
				Response.Write("<SCRIPT>")
				If Not IsNothing(Request.QueryString.Item("nGroups")) Then
					Response.Write("self.document.forms[0].valGroups.value=" & Request.QueryString.Item("nGroups") & ";")
				End If
				
				If Not IsNothing(Request.QueryString.Item("nModulec")) Then
					Response.Write("self.document.forms[0].valModulec.value=" & Request.QueryString.Item("nModulec") & ";")
				End If
				Response.Write("</" & "Script>")
			End If
			
		End With
	End With
	lclsNopayroll = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI811")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Session("bQuery")
bActionQuery = mobjValues.ActionQuery
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
		document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 16:49 $"
// ChangeValues: se controla el cambio de valor de los campos de la página
//-------------------------------------------------------------------------------------------
function ChangeValues(Field, Option, Action){
//-------------------------------------------------------------------------------------------
	var lstrAction
	switch(Option){
		case "Group":
		case "Modulec":
			with(self.document){
//+ Se recarga la página para mostrar los datos en la grilla con los nuevos valores.
				lstrAction = location.href;
				lstrAction = lstrAction.replace(/&nGroups=.*/, "") + "&nGroups=" + forms[0].valGroup.value;
				lstrAction = lstrAction.replace(/&nModulec=.*/, "") + "&nModulec=" + forms[0].valModule.value;
				location.href = lstrAction;
			}
			break;
		case "Cover":
			self.document.forms[0].valRole.Parameters.Param3.sValue = Field.value;
			if(Action != "Update")	
				if (self.document.forms[0].valCover.value != ""){
					self.document.forms[0].valRole.disabled = false;
					self.document.forms[0].btnvalRole.disabled = false;
				}
				else{
					self.document.forms[0].valRole.disabled = true;
					self.document.forms[0].btnvalRole.disabled = true;
					self.document.forms[0].valRole.value = "";
					UpdateDiv('valRoleDesc', '');
				}	
	}
}
	</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI811", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI811" ACTION="valPolicySeq.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("VI811", Request.QueryString.Item("sWindowDescript")))

lclsProduct = New eProduct.Product
lclsPolicy = New ePolicy.Policy
lclsGroups = New ePolicy.Groups

lblnModul = True
lblnNopayroll = False
If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
	lblnModul = False
End If

'Se valida si existe grupos asociados a la póliza matriz y de esta forma habilitar o deshabilitar el campo de grupo

lblnGroups = True
If lclsGroups.valGroupExist(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate")) Then
	lblnGroups = False
End If


Call lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPOlicy"), True)
If lclsPolicy.sNopayroll = "2" Or lclsPolicy.sNopayroll = "" Then
	lobjError = New eFunctions.Errors
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
	lobjError.sSessionID = Session.SessionID
	lobjError.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	Response.Write(lobjError.ErrorMessage(Request.QueryString.Item("sCodispl"), 55965,  ,  ,  , True))
	lobjError = Nothing
	lblnNopayroll = True
	bActionQuery = True
End If

If lblnNopayroll = True Then
	lblnModul = True
End If

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI811Upd()
Else
	Call insPreVI811()
End If

lclsProduct = Nothing
lclsGroups = Nothing
mcolNopayroll = Nothing
lclsPolicy = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.16
Call mobjNetFrameWork.FinishPage("VI811")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




