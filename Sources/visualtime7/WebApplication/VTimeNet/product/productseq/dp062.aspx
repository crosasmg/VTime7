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
	'+ Se definen las columnas del grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(41467, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(41468, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString,  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		
		'----------------------------------------------------------------------------
		Call .AddHiddenColumn("tctAuxDescript", CStr(0))
		Call .AddHiddenColumn("tctAuxShort_des", CStr(0))
		Call .AddHiddenColumn("tcnAuxSumins_co", CStr(0))
		
		Call .AddHiddenColumn("sAuxSel", CStr(2))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "DP062"
		.Width = 400
		.Height = 190
		.DeleteButton = False
		.AddButton = False
		If Session("bQuery") Then
			.Columns("Sel").GridVisible = False
			.bOnlyForQuery = True
		ElseIf mstrTypeFind = "1" Then 
			.Columns("tctDescript").EditRecord = True
		End If
		If request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "if(document.forms[0].sAuxSel.length>0)document.forms[0].sAuxSel[this.value].value =(this.checked?1:2); else document.forms[0].sAuxSel.value =(this.checked?1:2);"
	End With
End Sub

'% insPreDP02: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreDP062()
	'--------------------------------------------------------------------------------------------
	Dim lclsBas_sumins As eProduct.Bas_sumins
	Dim lcolBas_sumins As eProduct.Bas_suminses
	
	With Server
		lclsBas_sumins = New eProduct.Bas_sumins
		lcolBas_sumins = New eProduct.Bas_suminses
	End With
	mobjGrid.AddButton = True
	
	If lcolBas_sumins.find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate"))) Then
		mobjGrid.DeleteButton = True
		For	Each lclsBas_sumins In lcolBas_sumins
			With mobjGrid
				'-------------------------------------------------------------------------
				.Columns("tctDescript").DefValue = lclsBas_sumins.sDescript
				.Columns("tctShort_des").DefValue = lclsBas_sumins.sShort_des
				'--------------------------------------------------------------------------
				.Columns("tctAuxDescript").DefValue = lclsBas_sumins.sDescript
				.Columns("tctAuxShort_des").DefValue = lclsBas_sumins.sShort_des
				.Columns("tcnAuxSumins_co").DefValue = CStr(lclsBas_sumins.nSumins_co)
				'---------------------------------------------------------------------------
				.sDelRecordParam = "sDescript=' + marrArray[lintIndex].tctAuxDescript + '&sShort_des=' + marrArray[lintIndex].tctAuxShort_des + '&nSumins_co=' + marrArray[lintIndex].tcnAuxSumins_co + '"
				Response.Write(.DoRow)
			End With
		Next lclsBas_sumins
	Else
		mblnVisible = True
	End If
	Response.Write(mobjValues.HiddenControl("nCountReg", CStr(lcolBas_sumins.count)))
	Response.Write(mobjGrid.closeTable())
	
	lclsBas_sumins = Nothing
	lcolBas_sumins = Nothing
End Sub

'% insPreDP062Upd: Se muetra la ventana Popup para efecto de actualización del Gird
'--------------------------------------------------------------------------------------------
Private Sub insPreDP062Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsBas_sumins As eProduct.Bas_sumins
	
	lclsBas_sumins = New eProduct.Bas_sumins
	
	If request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		Call lclsBas_sumins.insPostDP062(request.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), request.QueryString.Item("sDescript"), request.QueryString.Item("sShort_des"), mobjValues.StringToType(request.QueryString.Item("nSumins_co"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & request.QueryString.Item("nMainAction") & "&nOpener=" & request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	End If
	With request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", "DP062", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsBas_sumins = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjGrid.sCodisplPage = "DP062"
mobjValues.sCodisplPage = "DP062"

If IsNothing(request.QueryString.Item("sTypeFind")) Then
	mstrTypeFind = "1"
Else
	mstrTypeFind = "2"
End If
mobjValues.ActionQuery = Session("bQuery")

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">




    <%
With Response
	.Write(mobjValues.StyleSheet())
	If request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP062", "DP062.aspx"))
		mobjMenu = Nothing
	End If
End With

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP062" ACTION="valProductSeq.aspx?mode=2;">
	<%Response.Write(mobjValues.ShowWindowsName("DP062"))

Call insDefineHeader()

If request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP062Upd()
Else
	Call insPreDP062()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




