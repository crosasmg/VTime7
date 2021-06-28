<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues

Dim mstrTypeFind As String
Dim mblnVisible As Boolean
Dim mblnDisabled As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "dp049"
	
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnConceptColumnCaption"), "tcnConcept", 5, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tcnConceptColumnToolTip"))
		Call .AddTextColumn(100126, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(100127, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, "",  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		Call .AddPossiblesColumn(100125, GetLocalResourceObject("cboStatregtColumnCaption"), "cboStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cboStatregtColumnToolTip"))
		Call .AddHiddenColumn("tcnauxConcept", CStr(0))
		Call .AddHiddenColumn("cboauxStatregt", CStr(0))
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("sAuxSel", CStr(2))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP049"
		.Width = 400
		.Height = 250
		.DeleteButton = False
		.AddButton = False
		.Columns("tctDescript").EditRecord = True
		
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = True
			.bOnlyForQuery = True
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "insSelected(this);"
		.DeleteScriptName = vbNullString
	End With
End Sub

'% insPreDP01: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP049()
	'--------------------------------------------------------------------------------------------
	Dim lclsCl_cov_bil As eProduct.Cl_cov_bil
	Dim lcolCl_cov_bil As eProduct.Cl_cov_bils
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insSelected(Field){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("	if(Field.checked)" & vbCrLf)
Response.Write("		EditRecord(Field.value,nMainAction, 'Update')" & vbCrLf)
Response.Write("    else{     " & vbCrLf)
Response.Write("        EditRecord(Field.value,nMainAction, 'Del',""nConcep="" + marrArray[Field.value].tcnConcept )" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    Field.checked = !Field.checked" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	With Server
		lclsCl_cov_bil = New eProduct.Cl_cov_bil
		lcolCl_cov_bil = New eProduct.Cl_cov_bils
	End With
	
	If Not lcolCl_cov_bil.FindCl_cov_bil2(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
		
		For	Each lclsCl_cov_bil In lcolCl_cov_bil
			With mobjGrid
				'	If Session("nUsercode") = "23" Then
				'	    Response.Write "<NOTSCRIPT>"
				'	    Response.Write "alert (""" & "nSelection: " & lclsCl_cov_bil.sDescript & " " & lclsCl_cov_bil.nSelection & """);"
				'	    Response.Write "</" & "Script>"
				'	End If
				.Columns("sel").Checked = lclsCl_cov_bil.nSelection
				.Columns("tcnConcept").DefValue = CStr(lclsCl_cov_bil.npay_concep)
				.Columns("tctDescript").DefValue = lclsCl_cov_bil.sDescript
				.Columns("tctShort_des").DefValue = lclsCl_cov_bil.sShort_des
				
				If mobjValues.StringToType(lclsCl_cov_bil.sStatregt, eFunctions.Values.eTypeData.etdDouble) > 0 Then
					.Columns("cboStatregt").DefValue = lclsCl_cov_bil.sStatregt
					.Columns("cboauxStatregt").DefValue = lclsCl_cov_bil.sStatregt
				Else
					.Columns("cboStatregt").DefValue = ""
					.Columns("cboauxStatregt").DefValue = ""
				End If
				
				.Columns("tcnauxConcept").DefValue = CStr(lclsCl_cov_bil.npay_concep)
				Response.Write(.DoRow)
			End With
		Next lclsCl_cov_bil
	Else
		mblnVisible = True
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lclsCl_cov_bil = Nothing
	lcolCl_cov_bil = Nothing
End Sub

'% insPreDP010Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP049Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsCl_cov_bil As eProduct.Cl_cov_bil
	Dim nAction As Object
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT LANGUAGE=""JavaScript"">" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("// Inhabilita los campos del Pagos/Indemnizaciones" & vbCrLf)
Response.Write("function Disabled(){" & vbCrLf)
Response.Write("//---------------------------------------------------------------------------" & vbCrLf)
Response.Write("    with (self.document.forms[0]){" & vbCrLf)
Response.Write("        tcnConcept.disabled = true;" & vbCrLf)
Response.Write("        tctDescript.disabled = true;" & vbCrLf)
Response.Write("        tctShort_des.disabled = true;" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	lclsCl_cov_bil = New eProduct.Cl_cov_bil
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		Response.Write(mobjValues.ConfirmDelete)
		
		Call lclsCl_cov_bil.insPostDP049(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nConcep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		lclsCl_cov_bil.nSelection = lclsCl_cov_bil.nSelection - 1
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/CoverSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoverseq.aspx", "DP049", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") = "Update" Then
			lclsCl_cov_bil.nSelection = lclsCl_cov_bil.nSelection + 1
			Response.Write("<SCRIPT>Disabled();</" & "Script>")
		End If
	End With
	
	lclsCl_cov_bil = Nothing
	
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

If IsNothing(Request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If

mobjValues.ActionQuery = Session("bQuery")
mobjGrid.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "dp049"
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
		.Write(mobjMenu.setZone(2, "DP049", "DP049.aspx"))
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
	.Write("<FORM METHOD=""POST"" ID=""FORM"" NAME=""frmDP045"" ACTION=""valCoverseq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """>")
	.Write(mobjValues.ShowWindowsName("DP049"))
	.Write("<BR>")
End With

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP049Upd()
Else
	Call insPreDP049()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




