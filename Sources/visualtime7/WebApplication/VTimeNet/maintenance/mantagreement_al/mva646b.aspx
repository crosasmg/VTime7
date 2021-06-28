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
		Call .AddHiddenColumn("hddIntertyp", "")
		Call .AddHiddenColumn("hddnExist", "")
		Call .AddHiddenColumn("hddsSel", "")
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVA646B"
		.sCodisplPage = "MVA646B"
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'% insPreMVA646B: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVA646B()
	'--------------------------------------------------------------------------------------------
	Dim lcolInt_typ_agre As eBranches.Int_typ_agres
	Dim lclsInt_typ_agre As Object
	Dim lintIndex As Short
	
	lcolInt_typ_agre = New eBranches.Int_typ_agres
	lintIndex = 0
	If lcolInt_typ_agre.Find(Session("nAgreement")) Then
		For	Each lclsInt_typ_agre In lcolInt_typ_agre
			With mobjGrid
				.Columns("hddIntertyp").DefValue = lclsInt_typ_agre.nIntertyp
				.Columns("hddnExist").DefValue = lclsInt_typ_agre.sSel
				.Columns("hddsSel").DefValue = lclsInt_typ_agre.sSel
				.Columns("Sel").Checked = lclsInt_typ_agre.sSel
				.Columns("tctDescript").DefValue = lclsInt_typ_agre.sDescript
				.Columns("Sel").OnClick = "InsChangeSel(this," & lintIndex & ")"
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsInt_typ_agre
	End If
	Response.Write(mobjGrid.closeTable())
	lcolInt_typ_agre = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA646B"
%>
<html>
<head>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA646B", "MVA646B.aspx"))
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
<form METHOD="POST" NAME="MVA646B" ACTION="ValAgreementSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MVA646B"))
Response.Write("<BR>")
Call insDefineHeader()
Call insPreMVA646B()
mobjValues = Nothing
%>
</form> 
</body>
</html>




