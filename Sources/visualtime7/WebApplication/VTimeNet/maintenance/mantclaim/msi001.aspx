<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
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
		.AddCheckColumn(0, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString)
		.AddHiddenColumn("hddsSel", "")
		.AddHiddenColumn("hddsRequire", "")
		.AddHiddenColumn("hddsExist", "")
		.AddHiddenColumn("hddnSequence", "")
		.AddHiddenColumn("hddsCodispl", "")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "MSI001"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.AddButton = False
		.DeleteButton = False
	End With
	
	If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
		mobjGrid.ActionQuery = True
		mobjGrid.Columns("Sel").GridVisible = True
	End If
End Sub

'% insPreMSI001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI001()
	'--------------------------------------------------------------------------------------------
	Dim lclsTab_wincla As Object
	Dim lintCount As Short
	Dim lcolTab_wincla As eClaim.Tab_winclas
	
	lintCount = 0
	
	lcolTab_wincla = New eClaim.Tab_winclas
	
	If lcolTab_wincla.Find(CInt(Request.QueryString.Item("nTraTypec")), Request.QueryString.Item("sBrancht"), Request.QueryString.Item("sBussityp")) Then
		For	Each lclsTab_wincla In lcolTab_wincla
			With mobjGrid
				.Columns("tctCodispl").DefValue = lclsTab_wincla.sCodispl
				.Columns("hddsCodispl").DefValue = lclsTab_wincla.sCodispl
				.Columns("tctDescript").DefValue = lclsTab_wincla.sDescript
				.Columns("chkRequire").Checked = lclsTab_wincla.sRequire
				.Columns("hddsRequire").DefValue = lclsTab_wincla.sRequire
				.Columns("chkRequire").OnClick = "insChangeRequire(this, " & lintCount & ");"
				.Columns("Sel").Checked = lclsTab_wincla.sExist
				.Columns("Sel").OnClick = "insSelect(this, " & lintCount & ");"
				.Columns("hddsSel").DefValue = lclsTab_wincla.sExist
				.Columns("hddsExist").DefValue = lclsTab_wincla.sExist
				.Columns("hddnSequence").DefValue = lclsTab_wincla.nSequence
				lintCount = lintCount + 1
				Response.Write(.DoRow)
			End With
		Next lclsTab_wincla
	End If
	Response.Write(mobjGrid.closeTable())
	lcolTab_wincla = Nothing
	lclsTab_wincla = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjGrid = New eFunctions.Grid
mobjMenu = New eFunctions.Menues
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("MSI001"))
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
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $" 
</SCRIPT>
</HEAD>
<BODY>
<FORM METHOD="post" ID="FORM" ACTION="valmantclaim.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nTraTypec=<%=Request.QueryString.Item("nTraTypec")%>&sBrancht=<%=Request.QueryString.Item("sBrancht")%>&sBussityp=<%=Request.QueryString.Item("sBussityp")%>">
<%Response.Write(mobjValues.ShowWindowsName("MSI001"))
Response.Write(mobjMenu.setZone(2, "MSI001", "MSI001.aspx"))

Call insDefineHeader()
Call insPreMSI001()
mobjValues = Nothing
mobjGrid = Nothing
mobjMenu = Nothing
%>    
</FORM>
</BODY>
</HTML>




