<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues



'% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
'% o inhabilitar los botones de añadir y eliminar.
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms008"
	
	mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngactionquery)
	
	Response.Write(mobjValues.ShowWindowsName("MS008") & "<BR>")
	
	With mobjGrid
		With .Columns
			Call .AddPossiblesColumn(0, GetLocalResourceObject("tcWindowsColumnCaption"), "tcWindows", "tabWindows", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  , 8, GetLocalResourceObject("tcWindowsColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		End With
		
		.Height = 230
		.Width = 400
		.Codispl = "MS008"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sDelRecordParam = "p_nAction='+ marrArray[lintIndex].tcWindows + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%inspreMS008:: Esta función permite realizar la lectura de la tabla principal de la transacción. 
'-------------------------------------------------------------------------------------------
Private Sub inspreMS008()
	'-------------------------------------------------------------------------------------------
	Dim lcolInquiry_ass As eGeneral.Inquiry_ass
	Dim lclsInquiry_as As eGeneral.Inquiry_as
	lcolInquiry_ass = New eGeneral.Inquiry_ass
	lclsInquiry_as = New eGeneral.Inquiry_as
	
	If lcolInquiry_ass.Find(Session("eInquiry")) Then
		For	Each lclsInquiry_as In lcolInquiry_ass
			With mobjGrid
				.Columns("tcWindows").DefValue = lclsInquiry_as.sCodispl
				Response.Write(.DoRow)
			End With
		Next lclsInquiry_as
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
	lcolInquiry_ass = Nothing
	lclsInquiry_as = Nothing
End Sub

'%inspreMS008upd: Esta rutina se encarga de actualizar un registro del Grid 
'-------------------------------------------------------------------------------------------
Private Sub inspreMS008upd()
	Dim lstrErrors As Object
	'-------------------------------------------------------------------------------------------
	Dim lobjError As String
	Dim lclsInquiry_as As eGeneral.Inquiry_as
	lclsInquiry_as = New eGeneral.Inquiry_as
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lobjError = lclsInquiry_as.insValMS008_K(.QueryString.Item("Action"), mobjValues.StringToType(Session("eInquiry"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("p_nAction"), Session("nUsercode"))
			
			If lobjError = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				lclsInquiry_as.sCodispl = .QueryString.Item("p_nAction")
				lclsInquiry_as.nKeynum = Session("eInquiry")
				lclsInquiry_as.Delete()
			Else
				Response.Write(lstrErrors)
			End If
			lclsInquiry_as = Nothing
		End If
	End With
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantsys.aspx", "MS008", Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = 0
%>

<HTML>
	<HEAD>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/Constantes.js"></SCRIPT>




		<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
		
		<%
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms008"

Response.Write(mobjValues.StyleSheet())

mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "MS008", "MS008.aspx"))
End If
%>
		
		<SCRIPT LANGUAGE="JavaSCRIPT">
			var nMainAction = 304;
		</SCRIPT>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="MS008" ACTION="valMantsys.aspx?Time=1">
			<%Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreMS008()
Else
	inspreMS008upd()
End If

mobjGrid = Nothing
%>
		</FORM>
	</BODY>
</HTML>




