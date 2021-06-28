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

'- Objeto para el manejo particular de los datos de la página
Dim mcolTar_activitys As eBranches.Tar_activitys


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnSpecialityColumnCaption"), "tcnSpeciality", 10,  ,  , GetLocalResourceObject("tcnSpecialityColumnToolTip"))
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valSpecialityColumnCaption"), "valSpeciality", "Table16", eFunctions.Values.eValuesType.clngWindowType, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valSpecialityColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, "",  , GetLocalResourceObject("tcnPercentColumnToolTip"), True, 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI630"
		.sCodisplPage = "MVI630"
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.Columns("valSpeciality").EditRecord = True
		.Height = 190
		.Width = 400
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nTyperec='+ self.document.forms[0].cbeTypeRec.value + '"
		.sDelRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nTyperec='+ self.document.forms[0].cbeTypeRec.value + '&nSpeciality='+ marrArray[lintIndex].valSpeciality + '"
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI630: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI630()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_activity As Object
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=10%><LABEL ID=0>" & GetLocalResourceObject("cbeTypeRecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	With mobjValues
		.BlankPosition = False
		Response.Write(mobjValues.PossiblesValues("cbeTypeRec", "Table5558", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTyperec"),  ,  ,  ,  ,  , "ChangeValues(this)"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	mcolTar_activitys = New eBranches.Tar_activitys
	If mcolTar_activitys.Find(mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.QueryString.Item("nTyperec"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsTar_activity In mcolTar_activitys
			With mobjGrid
				.Columns("tcnSpeciality").DefValue = lclsTar_activity.nSpeciality
				.Columns("valSpeciality").DefValue = lclsTar_activity.nSpeciality
				.Columns("tcnPercent").DefValue = lclsTar_activity.nPercent
				Response.Write(.DoRow)
			End With
		Next lclsTar_activity
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreMVI630Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI630Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsTar_activity As eBranches.Tar_activity
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsTar_activity = New eBranches.Tar_activity
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsTar_activity.insPostMVI630(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nSpeciality"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		Else
			With Response
				.Write(mobjValues.HiddenControl("hddnBranch", vbNullString))
				.Write(mobjValues.HiddenControl("hddnProduct", vbNullString))
				.Write(mobjValues.HiddenControl("hddnCover", vbNullString))
				.Write(mobjValues.HiddenControl("hdddEffecdate", vbNullString))
				.Write(mobjValues.HiddenControl("hddnTyperec", vbNullString))
				'+ Se almacenan en campos ocultos los valores necesarios para validar y/o actualizar los datos en las tablas
				.Write("<SCRIPT>")
				.Write("with(self.document.forms[0]){")
				.Write("hddnBranch.value=" & Request.QueryString.Item("nBranch") & ";")
				.Write("hddnProduct.value=" & Request.QueryString.Item("nProduct") & ";")
				.Write("hddnCover.value=" & Request.QueryString.Item("nCover") & ";")
				.Write("hdddEffecdate.value='" & Request.QueryString.Item("dEffecdate") & "';")
				.Write("hddnTyperec.value=" & Request.QueryString.Item("nTyperec") & ";")
				.Write("}")
				.Write("</" & "Script>")
			End With
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantLife.aspx", "MVI630", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTar_activity = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.sCodisplPage = "MVI630"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
	
//% ChangeValues: se maneja el cambio de valor de los campos de la página
//-------------------------------------------------------------------------------------------
function ChangeValues(Field){
//-------------------------------------------------------------------------------------------
	var lstrLocation
	
//+ Se recarga la página para mostrar los datos en el grid de acuerdo al tipo de recargo			
	lstrLocation = self.document.location.href;
	lstrLocation = lstrLocation.replace(/&nTyperec=.*/,"") + "&nTyperec=" + Field.value;
	self.document.location.href = lstrLocation;
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
<FORM METHOD="POST" NAME="MVI630" ACTION="valMantLife.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MVI630"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI630Upd()
Else
	Call insPreMVI630()
End If
%>
</FORM> 
</BODY>
</HTML>




