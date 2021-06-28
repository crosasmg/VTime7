<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues



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
		.Codispl = "MBC001"
		.sCodisplPage = "MBC001"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'% insPreMBC001: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMBC001()
	'--------------------------------------------------------------------------------------------
	'- Objeto para manejar un objeto de ventanas de la secuencia de clientes
	Dim lclsTab_wincli As Object
	'- Objeto para el manejo de una coleccion de los datos de la página
	Dim lcolTab_wincli As eClient.Tab_Winclis
	'- Indice correlativo de transacciones existentes    
	Dim lintCount As Short
	
	lintCount = 0
	lcolTab_wincli = New eClient.Tab_Winclis
	If lcolTab_wincli.Find(Request.QueryString.Item("sType_clie"), Request.QueryString.Item("sType_seq")) Then
		For	Each lclsTab_wincli In lcolTab_wincli
			With mobjGrid
				.Columns("tctCodispl").DefValue = lclsTab_wincli.sCodispl
				.Columns("hddsCodispl").DefValue = lclsTab_wincli.sCodispl
				.Columns("tctDescript").DefValue = lclsTab_wincli.sDescript
				.Columns("chkRequire").Checked = lclsTab_wincli.sRequire
				.Columns("hddsRequire").DefValue = lclsTab_wincli.sRequire
				.Columns("chkRequire").OnClick = "insChangeRequire(this, " & lintCount & ");"
				.Columns("Sel").Checked = lclsTab_wincli.sExist
				.Columns("Sel").OnClick = "insSelect(this, " & lintCount & ");"
				.Columns("hddsSel").DefValue = lclsTab_wincli.sExist
				.Columns("hddsExist").DefValue = lclsTab_wincli.sExist
				.Columns("hddnSequence").DefValue = lclsTab_wincli.nSequence
				lintCount = lintCount + 1
				Response.Write(.DoRow)
			End With
		Next lclsTab_wincli
	End If
	lcolTab_wincli = Nothing
	
	Response.Write(mobjGrid.closeTable())
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MBC001"
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"

//%insSelect: Actualiza las columnas ocultas
//-------------------------------------------------------------------------------------------
function insSelect(Field, nIndex){
//-------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if (typeof(hddsSel[nIndex])=='undefined'){
            hddsSel.value=(Field.checked)?1:2
            if (!Field.checked) chkRequire.checked = false;
        }
        else{
            hddsSel[nIndex].value=(Field.checked)?1:2
            if (!Field.checked) chkRequire[nIndex].checked = false;
        }
    }
}

//%insSelect: Actualiza la columna de selección
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
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MBC001", "MBC001.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MBC001" ACTION="valMantClient.aspx?sType_clie=<%=Request.QueryString.Item("sType_clie")%>&sType_seq=<%=Request.QueryString.Item("sType_seq")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MBC001"))
Call insDefineHeader()
Call insPreMBC001()
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>




