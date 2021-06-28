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


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddHiddenColumn("hddIntermed", "")
		Call .AddHiddenColumn("hddnExist", "")
		Call .AddHiddenColumn("hddsSel", "")
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 50, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA646C"
		.sCodisplPage = "MVA646C"
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% insPreMVA646C: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA646C()
	'--------------------------------------------------------------------------------------------
	Dim lcolInterm_agre As eBranches.Interm_agres
	Dim lclsInterm_agre As Object
	Dim llngIndex As Short
	
	lcolInterm_agre = New eBranches.Interm_agres
	llngIndex = 0
	If lcolInterm_agre.Find(Session("nAgreement")) Then
		For	Each lclsInterm_agre In lcolInterm_agre
			With mobjGrid
				.Columns("hddIntermed").DefValue = lclsInterm_agre.nIntermed
				.Columns("hddnExist").DefValue = lclsInterm_agre.sSel
				.Columns("hddsSel").DefValue = lclsInterm_agre.sSel
				.Columns("Sel").Checked = lclsInterm_agre.sSel
				.Columns("tctDescript").DefValue = lclsInterm_agre.nIntermed & "-" & lclsInterm_agre.sCliename
				.Columns("Sel").OnClick = "InsChangeSel(this," & llngIndex & ")"
				Response.Write(.DoRow)
			End With
			llngIndex = llngIndex + 1
		Next lclsInterm_agre
	End If
	Response.Write(mobjGrid.closeTable())
	lcolInterm_agre = Nothing
	lclsInterm_agre = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA646C"
%>
<html>
<head>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA646C", "MVA646C.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</head>
<script>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"

    //%InsChangeSel : Cambia el indicador de seleción
    //-------------------------------------------------------------
    function InsChangeSel(Field, nIndex) {
        //-------------------------------------------------------------
        with (self.document.forms[0]) {
            try {
                hddsSel[nIndex].value = (Field.checked ? 1 : 2)
            }
            catch (e) {
                hddsSel.value = (Field.checked ? 1 : 2);
            }

        }
    }

</script>
<body ONUNLOAD="closeWindows();">
<form METHOD="POST" NAME="MVA646C" ACTION="ValAgreementSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MVA646C"))
Response.Write("<BR>")
Call insDefineHeader()
Call insPreMVA646C()
mobjValues = Nothing
%>
</form> 
</body>
</html>




