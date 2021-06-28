<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "cr768"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDiaInicialColumnCaption"), "tcnDiaInicial", 5, vbNullString,  , GetLocalResourceObject("tcnDiaInicialColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDiaFinalColumnCaption"), "tcnDiaFinal", 5, vbNullString,  , GetLocalResourceObject("tcnDiaFinalColumnCaption"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTarMinimaColumnCaption"), "tcnTarMinima", 18, vbNullString,  , GetLocalResourceObject("tcnTarMinimaColumnCaption"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnTarAdicionalColumnCaption"), "tcnTarAdicional", 18, vbNullString,  , GetLocalResourceObject("tcnTarAdicionalColumnCaption"), True, 6)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CR768"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 300
		.Width = 400
		.Columns("Sel").GridVisible = True
		.Columns("tcnDiaInicial").EditRecord = True
		.Columns("tcnDiaFinal").EditRecord = False
		.Columns("tcnTarMinima").EditRecord = False
		.Columns("tcnTarAdicional").EditRecord = False
		
		.sEditRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") & "&valProduct=" & Request.QueryString.Item("valProduct") & "&tcnNumber=" & Request.QueryString.Item("tcnNumber") & "&cbeBranchrei=" & Request.QueryString.Item("cbeBranchrei") & "&valCovergen=" & Request.QueryString.Item("valCovergen") & "&tcnCapital=" & Request.QueryString.Item("tcnCapital") & "&tcdEffecdate=" & Request.QueryString.Item("tcdEffecdate")
		
		.sDelRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") & "&valProduct=" & Request.QueryString.Item("valProduct") & "&tcnNumber=" & Request.QueryString.Item("tcnNumber") & "&cbeBranchrei=" & Request.QueryString.Item("cbeBranchrei") & "&valCovergen=" & Request.QueryString.Item("valCovergen") & "&tcnCapital=" & Request.QueryString.Item("tcnCapital") & "&tcdEffecdate=" & Request.QueryString.Item("tcdEffecdate") & "&tcnDiaInicial=' + marrArray[lintIndex].tcnDiaInicial + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreCR726: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR768()
	'--------------------------------------------------------------------------------------------
	Dim lclstar_asistviaje As eCoReinsuran.Tar_asistviaje
	Dim lcoltar_asistviajes As eCoReinsuran.tar_asistviajes
	
	Dim lintCoverGen As Object
	Dim lblnFind As Boolean
	Dim i As Integer
	
	lclstar_asistviaje = New eCoReinsuran.Tar_asistviaje
	lcoltar_asistviajes = New eCoReinsuran.tar_asistviajes
	
	lblnFind = lcoltar_asistviajes.Find(mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbeBranchrei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	If lblnFind Then
		For i = 1 To lcoltar_asistviajes.count
        'For i = 0 To lcoltar_asistviajes.count -1
			With mobjGrid
				.Columns("tcnDiaInicial").DefValue = CStr(lcoltar_asistviajes.Item(i).nDay_ini)
				.Columns("tcnDiaFinal").DefValue = CStr(lcoltar_asistviajes.Item(i).nDay_end)
				.Columns("tcnTarMinima").DefValue = CStr(lcoltar_asistviajes.Item(i).nTar_min)
				.Columns("tcnTarAdicional").DefValue = CStr(lcoltar_asistviajes.Item(i).nTar_adic)
				Response.Write(.DoRow)
			End With
		Next 
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreCR726Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCR768Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclstar_asistviaje As eCoReinsuran.Tar_asistviaje
	Dim lblnFind As Boolean
	
	lclstar_asistviaje = New eCoReinsuran.Tar_asistviaje
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			lblnFind = lclstar_asistviaje.insPostCR768(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbeBranchrei"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("valCovergen"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnDiaInicial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiaFinal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTarMinima"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTarAdicional"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		Else
			If .QueryString.Item("Action") = "Add" Then
				mobjGrid.Columns("tcnDiaInicial").Disabled = False
			Else
				mobjGrid.Columns("tcnDiaInicial").Disabled = True
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValCoReinsuranTra.aspx", "CR768", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cr768"

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CR768", "CR768.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CR726" ACTION="valCoReinsuranTra.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("CR768"))
Response.Write("<BR>")
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCR768Upd()
Else
	Call insPreCR768()
End If
%>
<SCRIPT LANGUAGE="JavaScript">
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 21/04/06 16:07 $" 
</SCRIPT>
</FORM> 
</BODY>
</HTML>






