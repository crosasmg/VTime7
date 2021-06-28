<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Define las columnas del Grid
'-----------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "DP100"
	
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddNumericColumn(41469, GetLocalResourceObject("tcnCode_goodColumnCaption"), "tcnCode_good", 5, "",  , GetLocalResourceObject("tcnCode_goodColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddTextColumn(41474, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		.AddTextColumn(41475, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, "",  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		.AddNumericColumn(41470, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "",  , GetLocalResourceObject("tcnRateColumnToolTip"),  , 6)
		.AddTextColumn(41476, GetLocalResourceObject("tctRoutineColumnCaption"), "tctRoutine", 12, "",  , GetLocalResourceObject("tctRoutineColumnToolTip"))
		.AddCheckColumn(41477, GetLocalResourceObject("chkIncreaseColumnCaption"), "chkIncrease", "",  ,  ,  , True, GetLocalResourceObject("chkIncreaseColumnToolTip"))
		.AddNumericColumn(41471, GetLocalResourceObject("tcnRatChaAddColumnCaption"), "tcnRatChaAdd", 6, CStr(0),  , GetLocalResourceObject("tcnRatChaAddColumnToolTip"),  , 2,  ,  ,  , True)
		.AddCheckColumn(41478, GetLocalResourceObject("chkDecreaseColumnCaption"), "chkDecrease", "",  ,  ,  , True, GetLocalResourceObject("chkDecreaseColumnToolTip"))
		.AddNumericColumn(41472, GetLocalResourceObject("tcnRatChaSubColumnCaption"), "tcnRatChaSub", 6, CStr(0),  , GetLocalResourceObject("tcnRatChaSubColumnToolTip"),  , 2,  ,  ,  , True)
		.AddNumericColumn(41473, GetLocalResourceObject("tcnLevelChaColumnCaption"), "tcnLevelCha", 5, CStr(0),  , GetLocalResourceObject("tcnLevelChaColumnToolTip"))
		.AddHiddenColumn("sParam", vbNullString)
	End With
	With mobjGrid
		.DeleteButton = True
		.AddButton = True
		.Top = 70
		.Codispl = "DP100"
		.Width = 330
		.Height = 420
		.ActionQuery = Session("bQuery")
		.Columns("tctDescript").EditRecord = True
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreDP100: Carga los datos en le grid de la forma "Folder"
'--------------------------------------------------------------
Private Sub insPreDP100()
	'--------------------------------------------------------------
	Dim lclsTab_goods As Object
	Dim lcolTab_goodses As ePolicy.Tab_goodses
	lcolTab_goodses = New ePolicy.Tab_goodses
	If lcolTab_goodses.Find(Session("nBranch"), Session("nProduct")) Then
		For	Each lclsTab_goods In lcolTab_goodses
			With mobjGrid
				.Columns("tcnCode_good").DefValue = lclsTab_goods.nCode_good
				.Columns("tctDescript").DefValue = lclsTab_goods.sDescript
				.Columns("tctShort_des").DefValue = lclsTab_goods.sShort_des
				.Columns("tcnRate").DefValue = lclsTab_goods.nRate
				.Columns("tctRoutine").DefValue = lclsTab_goods.sRoutine
				.Columns("tcnRatChaAdd").DefValue = lclsTab_goods.nRatChaAdd
				.Columns("tcnRatChaSub").DefValue = lclsTab_goods.nRatChaSub
				.Columns("tcnLevelCha").DefValue = lclsTab_goods.nLevelCha
				.Columns("sParam").DefValue = "nCode_good=" & lclsTab_goods.nCode_good
				
				Select Case lclsTab_goods.sChange_typ
					Case 1
						.Columns("chkIncrease").Checked = CShort("2")
						.Columns("chkDecrease").Checked = CShort("2")
					Case 2
						.Columns("chkIncrease").Checked = CShort("1")
						.Columns("chkDecrease").Checked = CShort("2")
					Case 3
						.Columns("chkIncrease").Checked = CShort("2")
						.Columns("chkDecrease").Checked = CShort("1")
					Case 4
						.Columns("chkIncrease").Checked = CShort("1")
						.Columns("chkDecrease").Checked = CShort("1")
				End Select
				Response.Write(.DoRow)
			End With
		Next lclsTab_goods
	End If
	'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
	Response.Write(mobjGrid.CloseTable())
	lcolTab_goodses = Nothing
	lclsTab_goods = Nothing
End Sub
'% insPreDP100Upd: Gestiona lo relacionado a la actualización de un registro de Grid
'-----------------------------------------------------------------------------------
Private Sub insPreDP100Upd()
	'-----------------------------------------------------------------------------------
	Dim lclsTab_goods As ePolicy.Tab_goods
	lclsTab_goods = New ePolicy.Tab_goods
	With Request
		mobjGrid.Columns("chkIncrease").Disabled = False
		mobjGrid.Columns("chkDecrease").Disabled = False
		mobjGrid.Columns("chkIncrease").OnClick = "ShowRate_perc(" & """Mas""" & ")"
		mobjGrid.Columns("chkDecrease").OnClick = "ShowRate_perc(" & """Menos""" & ")"

		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsTab_goods.insPostDP100(.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCode_good"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .Form.Item("tctShort_des"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctRoutine"), mobjValues.StringToType(.Form.Item("tcnRatChaAdd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRatChaSub"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLevelCha"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("chkIncrease"), .Form.Item("chkDecrease"))
    		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & request.QueryString.Item("nMainAction") & "&nOpener=" & request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")

		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProductSeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))

	End With
	lclsTab_goods = Nothing
End Sub

</script>
<%Response.Expires = -1


mobjMenu = New eFunctions.Menues

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP100"


If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "DP100", "DP100.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
End If
mobjMenu = Nothing
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%=mobjValues.StyleSheet()%>
    
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $"

//% ShowRate_perc: Habilita/deshabilita el campo
//-----------------------------------------------------------------------------------------
function ShowRate_perc(Type){
//-----------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (Type == "Mas"){
			elements["tcnRatChaAdd"].disabled = !elements["chkIncrease"].checked
		}
		else {
			elements["tcnRatChaSub"].disabled = !elements["chkDecrease"].checked
		}
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<!--FORM METHOD="post" ID="FORM" NAME="DP100" ACTION="valProductSeq.aspx?nMode=2"-->
<FORM METHOD="POST" ID="FORM" NAME="DP100" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP100()
Else
	Call insPreDP100Upd()
End If
mobjGrid = Nothing
mobjMenu = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





