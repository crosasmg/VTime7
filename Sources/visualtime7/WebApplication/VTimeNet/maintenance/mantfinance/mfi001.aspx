<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctCodisplColumnCaption"), "tctCodispl", 8, "")
		.AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 40, "")
		.AddCheckColumn(0, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)
		.AddHiddenColumn("hddsSel", "")
		.AddHiddenColumn("hddsRequire", "")
		.AddHiddenColumn("hddsExist", "")
		.AddHiddenColumn("hddnSequence", "")
		.AddHiddenColumn("hddsCodispl", "")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MFI001"
		.sCodisplPage = "MFI001"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").disabled = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'% insPreMFI001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMFI001()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_winfin As Object
	Dim lintCount As Short
	Dim lcolTab_winfin As eFinance.Tab_winFins
	
	lintCount = 0
	lcolTab_winfin = New eFinance.Tab_winFins
	
	If lcolTab_winfin.FindTab_winfin(mobjValues.StringToType(Request.QueryString.Item("nTratypec"), eFunctions.Values.eTypeData.etdDouble), True) Then
		
		For	Each lclsTab_winfin In lcolTab_winfin
			
			With mobjGrid
				.Columns("tctCodispl").DefValue = lclsTab_winfin.sCodispl
				.Columns("hddsCodispl").DefValue = lclsTab_winfin.sCodispl
				.Columns("tctDescript").DefValue = lclsTab_winfin.sDescript
				.Columns("chkRequire").Checked = lclsTab_winfin.sRequire
				.Columns("hddsRequire").DefValue = lclsTab_winfin.sRequire
				.Columns("chkRequire").OnClick = "insChangeRequire(this, " & lintCount & ");"
				.Columns("Sel").Checked = lclsTab_winfin.sExist
				.Columns("Sel").OnClick = "insSelect(this, " & lintCount & ");"
				.Columns("hddsSel").DefValue = lclsTab_winfin.sExist
				.Columns("hddsExist").DefValue = lclsTab_winfin.sExist
				.Columns("hddnSequence").DefValue = lclsTab_winfin.nSequence
				lintCount = lintCount + 1
				Response.Write(.DoRow)
			End With
		Next lclsTab_winfin
	End If
	Response.Write(mobjGrid.closeTable())
	lcolTab_winfin = Nothing
	lclsTab_winfin = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MFI001"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("MFI001"))
End With
%>      
<SCRIPT>

//%insSelect: Actualiza las columnas ocultas
//-------------------------------------------------------------------------------------------
function insSelect(Field, Index){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddsSel[Index])=='undefined'){
            hddsSel.value=(Field.checked)?1:2
            if (!Field.checked) chkRequire.checked = false;
        }
        else{
            hddsSel[Index].value=(Field.checked)?1:2
            if (!Field.checked) chkRequire[Index].checked = false;
        }
    }
}

//%insSelect: Actualiza la columna de selección
//-------------------------------------------------------------------------------------------
function insChangeRequire(Field, Index){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(Sel[Index])=='undefined'){
            Sel.checked=(Field.checked)?true:Sel.checked
            hddsRequire.value=(Field.checked)?1:2
            insSelect(Sel, Index)
        }
        else{
            Sel[Index].checked=(Field.checked)?true:Sel[Index].checked
            hddsRequire[Index].value=(Field.checked)?1:2
            insSelect(Sel[Index], Index)
        }
    }
}
</SCRIPT>
</SCRIPT>

</HEAD>

<BODY>
<FORM METHOD="post" ID="FORM" ACTION="valmantFinance.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nTraTypec=<%=Request.QueryString.Item("nTraTypec")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MFI001"))
Response.Write(mobjMenu.setZone(2, "MFI001", "MFI001.aspx"))

Call insDefineHeader()
Call insPreMFI001()

mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing

%>    
</FORM>
</BODY>
</HTML>





