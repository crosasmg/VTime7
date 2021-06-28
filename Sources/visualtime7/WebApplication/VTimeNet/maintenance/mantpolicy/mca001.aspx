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
	'Set mobjGrid = Server.CreateObject("eFunctions.Grid")
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctCodisplColumnCaption"), "tctCodispl", 8, vbNullString,  , GetLocalResourceObject("tctCodisplColumnToolTip"))
		.AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 40, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString,  ,  ,  ,  , GetLocalResourceObject("chkRequireColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkAutomaticColumnCaption"), "chkAutomatic", vbNullString,  ,  ,  ,  , GetLocalResourceObject("chkAutomaticColumnToolTip"))
		.AddHiddenColumn("hddsSel", "")
		.AddHiddenColumn("hddsRequire", "")
		.AddHiddenColumn("hddsExist", "")
		.AddHiddenColumn("hddnSequence", "")
		.AddHiddenColumn("hddAutomatic", "")
		.AddHiddenColumn("hddsCodispl", "")
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Columns("Sel").GridVisible = True
		.Codispl = "MCA001"
		.Width = 350
		.Height = 250
		.DeleteButton = False
		.AddButton = False
		If Request.QueryString.Item("nMainAction") = "401" Then
			.DeleteButton = False
			.AddButton = False
			.bOnlyForQuery = True
			.Columns("Sel").Disabled = True
			.Columns("chkRequire").Disabled = True
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.Columns("Sel").OnClick = "InsSelected(this.value, this.checked)"
		.Columns("chkRequire").OnClick = "checkValue(this)"
	End With
End Sub

'% insPreMCA001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreMCA001()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_winpol As Object
	Dim lcolTab_winpols As eProduct.Tab_winpols
	Dim lintCount As Short
	
	lcolTab_winpols = New eProduct.Tab_winpols
	If lcolTab_winpols.Find(Request.QueryString.Item("sBussityp"), CInt(Request.QueryString.Item("sTratypep")), Request.QueryString.Item("sPolitype"), Request.QueryString.Item("sCompon"),  , Request.QueryString.Item("sBrancht"), mobjValues.StringToType(Request.QueryString.Item("nType_Amend"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintCount = 0
		For	Each lclsTab_winpol In lcolTab_winpols
			With mobjGrid
				.Columns("tctCodispl").DefValue = lclsTab_winpol.sCodispl
				.Columns("hddsCodispl").DefValue = lclsTab_winpol.sCodispl
				.Columns("tctDescript").DefValue = lclsTab_winpol.sDescript
				.Columns("chkRequire").Checked = lclsTab_winpol.sRequire
				.Columns("hddsRequire").DefValue = lclsTab_winpol.sRequire
				.Columns("chkRequire").OnClick = "insChangeRequire(this, " & lintCount & ");"
				.Columns("Sel").Checked = lclsTab_winpol.sExist
				.Columns("Sel").OnClick = "insSelect(this, " & lintCount & ");"
				.Columns("chkAutomatic").OnClick = "insChangeAutomatic(this, " & lintCount & ");"
				.Columns("hddsSel").DefValue = lclsTab_winpol.sExist
				.Columns("hddnSequence").DefValue = lclsTab_winpol.nSequence
				.Columns("chkAutomatic").Checked = lclsTab_winpol.sAutomatic
				.Columns("hddAutomatic").DefValue = lclsTab_winpol.sAutomatic
				lintCount = lintCount + 1
				Response.Write(.DoRow)
			End With
		Next lclsTab_winpol
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lclsTab_winpol = Nothing
	lcolTab_winpols = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "mca001"
mobjGrid.sCodisplPage = "mca001"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>





<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 6/10/03 17:29 $"

//% insChangeAutomatic: actualiza la columna oculta para el manejo automático de la 
//%                     ventana en la secuencia 
//-------------------------------------------------------------------------------------------
function insChangeAutomatic(Field, nIndex){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddAutomatic[nIndex])=='undefined')
            hddAutomatic.value=(Field.checked)?1:2
        else
            hddAutomatic[nIndex].value=(Field.checked)?1:2
    }
}

//%insSelect: Actualiza las columnas ocultas
//-------------------------------------------------------------------------------------------
function insSelect(Field, nIndex){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddsSel[nIndex])=='undefined'){
            hddsSel.value=(Field.checked)?1:2
            if (!Field.checked){
				chkRequire.checked = false;
				chkAutomatic[nIndex].checked = false;
				hddAutomatic[nIndex].value = 2;
			}
        }
        else{
            hddsSel[nIndex].value=(Field.checked)?1:2
            if (!Field.checked){
				chkRequire[nIndex].checked = false;
				chkAutomatic[nIndex].checked = false;
				hddAutomatic[nIndex].value = 2;
			}
        }
    }
}

//%insChangeRequire: Cambia el indicador de selección cuando la ventana es requerida
//-------------------------------------------------------------------------------------------
function insChangeRequire(Field, nIndex){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(Sel[nIndex])=='undefined'){
            Sel.checked=(Field.checked)?true:Sel.checked
            hddsRequire.value=(Field.checked)?1:2
            insSelect(Sel, nIndex)
        }
        else{
            Sel[nIndex].checked=(Field.checked)?true:Sel[nIndex].checked
            hddsRequire[nIndex].value=(Field.checked)?1:2
            insSelect(Sel[nIndex], nIndex)
        }
    }
}

</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MCA001", "MCA001.aspx"))
		mobjMenu = Nothing
	End If
End With%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%Response.Write(mobjValues.ShowWindowsName("MCA001"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmSeqWinPol" ACTION="ValMantPolicy.aspx?sContent=1&<%=Request.Params.Get("Query_String")%>">
    <TABLE WIDTH="100%">
        <%
Call insDefineHeader()
Call insPreMCA001()
%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>




