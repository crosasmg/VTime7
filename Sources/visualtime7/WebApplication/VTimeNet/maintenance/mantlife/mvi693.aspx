<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de la tabla que actualiza la transacción
Dim mcolAge_collect As eBranches.Age_collects


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnInitAgeColumnCaption"), "tcnInitAge", 2, vbNullString, , GetLocalResourceObject("tcnInitAgeColumnCaption"), , , , , , Request.QueryString.Item("Action") = "Update")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndAgeColumnCaption"), "tcnEndAge", 2, vbNullString, , GetLocalResourceObject("tcnEndAgeColumnCaption"), , , , , , Request.QueryString.Item("Action") = "Update")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnAct_PercColumnCaption"), "tcnAct_Perc", 5, vbNullString, , GetLocalResourceObject("tcnAct_PercColumnToolTip"), True, 2)
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "MVI693"
            .sCodisplPage = "MVI693"
            .ActionQuery = mobjValues.ActionQuery
            .Height = 250
            .Width = 600
            .WidthDelete = 600
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
            .Columns("tcnInitAge").EditRecord = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
            .sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
            .sDelRecordParam = "nInitAge='+ marrArray[lintIndex].tcnInitAge + '" & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

'% insPreMVI693: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI693()
	'--------------------------------------------------------------------------------------------
	Dim lclsAge_collect As eBranches.Age_collect
	
	If Request.QueryString.Item("nMainAction") <> "401" Then
		
Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("		<TABLE>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("tcnAct_PercCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.NumericControl("tcnAct_Perc", 5, vbNullString,  , GetLocalResourceObject("tcnAct_PercToolTip"), True, 2,  ,  ,  , "inschangePercent(this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("	")

		
	End If
	If Request.QueryString.Item("sUpdate") = "1" Then
		lclsAge_collect = New eBranches.Age_collect
		Call lclsAge_collect.Update_Act_perc(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nAct_perc"), eFunctions.Values.eTypeData.etdDouble))
		lclsAge_collect = Nothing
	End If
	
	If mcolAge_collect.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsAge_collect In mcolAge_collect
			With mobjGrid
				.Columns("tcnInitAge").DefValue = CStr(lclsAge_collect.nInitAge)
				.Columns("tcnEndAge").DefValue = CStr(lclsAge_collect.nEndAge)
				.Columns("tcnAct_Perc").DefValue = CStr(lclsAge_collect.nAct_perc)
				Response.Write(.DoRow)
			End With
		Next lclsAge_collect
	End If
	With Response
		.Write(mobjGrid.closeTable())
		If Request.QueryString.Item("nMainAction") <> "401" Then
			.Write("<SCRIPT>Old_percent=self.document.forms[0].tcnAct_Perc.value;</" & "Script>")
		End If
	End With
	lclsAge_collect = Nothing
End Sub

'% insPreMVI693Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI693Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsAge_collect As eBranches.Age_collect
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsAge_collect = New eBranches.Age_collect
			If lclsAge_collect.inspostMVI693(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nInitAge"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode")) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI693", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
			With Response
				.Write(mobjValues.HiddenControl("cbeBranch", Request.QueryString.Item("nBranch")))
				.Write(mobjValues.HiddenControl("valProduct", Request.QueryString.Item("nProduct")))
				.Write(mobjValues.HiddenControl("tcdEffecdate", Request.QueryString.Item("dEffecdate")))
			End With
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mcolAge_collect = New eBranches.Age_collects

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI693"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
	
//- Variable para indicar el porcentaje que tenía el campo antes de cambiarlo	
	var Old_percent
	
//% inschangePercent: se actualizan los datos de la tabla con el porcentaje indicado
//-------------------------------------------------------------------------------------------------------------------
function inschangePercent(Field){
//-------------------------------------------------------------------------------------------------------------------
	var lstrAction
	if(Field.value!="" &&
	   Field.value!=0)
		if(Field.value!=Old_percent){
			Old_percent=Field.value;
			lstrAction = self.document.location.href;
			lstrAction = lstrAction.replace(/&nAct_Perc.*/, '') + '&nAct_Perc=' + Field.value + '&sUpdate=1';
			self.document.location.href=lstrAction;
		}
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI630", "MVI630.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI693" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI693"))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI693Upd()
Else
	Call insPreMVI693()
End If

mobjValues = Nothing
mobjMenu = Nothing
mcolAge_collect = Nothing
%>
</FORM> 
</BODY>
</HTML>





