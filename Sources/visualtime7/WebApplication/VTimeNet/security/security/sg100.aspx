<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues



'%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
'%del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "SG100"
	
	'+ Se definen todas las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(100457, GetLocalResourceObject("cbeConceptColumnCaption"), "cbeConcept", "Table293", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConceptColumnToolTip"))
		Call .AddHiddenColumn("nSelValue", CStr(0))
		Call .AddHiddenColumn("nConcept", CStr(0))
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG100"
		'+ Si la acción que viaja a través del QueryString es Consulta (401), Elimiación (303) o el
		'+ parámetro nMainAction tiene valor NULO (vbNUllString o ""), la propiedad ActionQuery se setea en TRUE,
		'+ de lo contrario se setea en FALSE
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		Else
			.Columns("Sel").GridVisible = True
			.ActionQuery = False
		End If
		
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'%insPreSG100: Esta ventana se encarga de mostrar en el grid los valores leídos.
'---------------------------------------------------------------------------------------
Private Sub insPreSG100()
	'---------------------------------------------------------------------------------------
	Dim lclsSecur_sche As Object
	Dim lcolSecur_sches As eSecurity.Secur_sches
	Dim llngIndex As Short
	
	lcolSecur_sches = New eSecurity.Secur_sches
	
	If lcolSecur_sches.FindSche_pcon(Session("sSche_codeWin"), CInt(Request.QueryString.Item("nMainAction")), True) Then
		llngIndex = 0
		
		For	Each lclsSecur_sche In lcolSecur_sches
			With mobjGrid
				.Columns("cbeConcept").DefValue = lclsSecur_sche.nConcept
				.Columns("nConcept").DefValue = lclsSecur_sche.nConcept
				
				If lclsSecur_sche.nSel <> 0 And lclsSecur_sche.nSel <> eRemoteDB.Constants.intNull Then
					.Columns("nSelValue").DefValue = CStr(1)
					.Columns("Sel").Checked = 1
				Else
					.Columns("nSelValue").DefValue = CStr(0)
					.Columns("Sel").Checked = 2
				End If
				
				.Columns("Sel").OnClick = "insHandleGrid(this," & CStr(llngIndex) & ")"
				
				llngIndex = llngIndex + 1
				
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsSecur_sche
	End If
	
	lclsSecur_sche = Nothing
	lcolSecur_sches = Nothing
	
	Response.Write(mobjGrid.CloseTable())
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG100"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




	
<%
mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "SG100", "SG100.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG100"))
	.Write(mobjValues.StyleSheet())
End With
%>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG100" ACTION="ValSecuritySeqSchema.aspx?Time=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

   <%
Response.Write(mobjValues.ShowWindowsName("SG100"))

Call insDefineHeader()

Call insPreSG100()
%>
   
</FORM>
</BODY>
</HTML>

<SCRIPT>
//-------------------------------------------------------------------------------------------
function insHandleGrid(Field, nIndex){
//-------------------------------------------------------------------------------------------

//+ Se actualiza la columna oculta con la marcada.
 
    if (Field.checked)
        self.document.forms[0].nSelValue[nIndex].value = 1
    else self.document.forms[0].nSelValue[nIndex].value = 0    
}    

</SCRIPT>







