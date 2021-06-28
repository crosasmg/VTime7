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
	mobjGrid.sCodisplPage = "SG017"
	
	'+Se definen todas las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(100455, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOfficeColumnCaption"))
		Call .AddCheckColumn(100455, GetLocalResourceObject("sInd_updaColumnCaption"), "sInd_upda", "", 1, CStr(2))
		Call .AddCheckColumn(100456, GetLocalResourceObject("sInd_inquColumnCaption"), "sInd_inqu", "", 1, CStr(2))
		
		Call .AddHiddenColumn("nSelValue", CStr(0))
		Call .AddHiddenColumn("nOffice", CStr(0))
		Call .AddHiddenColumn("nInd_upda", CStr(0))
		Call .AddHiddenColumn("nInd_inqu", CStr(0))
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG017"
		
		'+ Si la acción que viaja a través del QueryString es Consulta (401), Elimiación (303) o el
		'+ parámetro nMainAction tiene valor NULO (vbNUllString o ""), la propiedad ActionQuery se setea en TRUE,
		'+ de lo contrario se setea en FALSE
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
			.ActionQuery = True
		Else
			.ActionQuery = False
		End If
		
		.Columns("Sel").GridVisible = True
		.AddButton = False
		.DeleteButton = False
	End With
End Sub

'%insPreSG017: Esta ventana se encarga de mostrar en el grid los valores leídos.
'---------------------------------------------------------------------------------------
Private Sub insPreSG017()
	'---------------------------------------------------------------------------------------
	Dim lclsSecur_sche As Object
	Dim lcolSecur_sches As eSecurity.Secur_sches
	Dim llngIndex As Short
	Dim lintAction As Integer
	
	lcolSecur_sches = New eSecurity.Secur_sches
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
		lintAction = 1
	Else
		lintAction = 2
	End If
	
	If lcolSecur_sches.FindOff_acc(Session("sSche_codeWin"), lintAction, True) Then
		llngIndex = 0
		
		For	Each lclsSecur_sche In lcolSecur_sches
			With mobjGrid
				.Columns("cbeOffice").DefValue = lclsSecur_sche.nOffice
				.Columns("nOffice").DefValue = lclsSecur_sche.nOffice
				
				If lclsSecur_sche.nSel <> eRemoteDB.Constants.intNull And lclsSecur_sche.nSel <> 0 Then
					.Columns("nSelValue").DefValue = CStr(1)
					.Columns("Sel").Checked = 1
				Else
					.Columns("nSelValue").DefValue = CStr(0)
					.Columns("Sel").Checked = 2
				End If
				
				If lclsSecur_sche.sInd_upda <> "2" And lclsSecur_sche.sInd_upda <> "" Then
					
					.Columns("nInd_upda").DefValue = CStr(1)
					.Columns("sInd_upda").Checked = 1
					
					.Columns("sInd_inqu").disabled = True
					
				Else
					.Columns("nInd_upda").DefValue = CStr(2)
					.Columns("sInd_upda").Checked = 2
					
					.Columns("sInd_inqu").disabled = False
				End If
				
				If lclsSecur_sche.sInd_inqu <> "2" And lclsSecur_sche.sInd_inqu <> "" Then
					.Columns("nInd_inqu").DefValue = CStr(1)
					.Columns("sInd_inqu").Checked = 1
				Else
					.Columns("nInd_inqu").DefValue = CStr(2)
					.Columns("sInd_inqu").Checked = 2
				End If
				
				.Columns("Sel").OnClick = "insHandleGrid(this," & CStr(llngIndex) & ",1)"
				.Columns("sInd_upda").OnClick = "insHandleGrid(this," & CStr(llngIndex) & ",2)"
				.Columns("sInd_inqu").OnClick = "insHandleGrid(this," & CStr(llngIndex) & ",3)"
				
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
mobjValues.sCodisplPage = "SG017"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




	
<%
mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "SG017", "SG017.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG017"))
	.Write(mobjValues.StyleSheet())
End With
%>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG017" ACTION="ValSecuritySeqSchema.aspx?Time=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

   <%
Response.Write(mobjValues.ShowWindowsName("SG017"))

Call insDefineHeader()

Call insPreSG017()
%>
   
</FORM>
</BODY>
</HTML>

<SCRIPT>
//-------------------------------------------------------------------------------------------
function insHandleGrid(Field, nIndex, nCol){
//-------------------------------------------------------------------------------------------

//+ Se actualiza la columna oculta con la marcada.
 
//+ Si corresponde a la columna de selección.
 
    if (nCol==1)
    {
        if (Field.checked)
            self.document.forms[0].nSelValue[nIndex].value = 1
        else
			self.document.forms[0].nSelValue[nIndex].value = 0;
    }
    
//+ Si corresponde a la columna de Actualización.

    if (nCol==2)
    {
        if (Field.checked)
        {
            self.document.forms[0].nInd_inqu[nIndex].value = 1;
            self.document.forms[0].sInd_inqu[nIndex].checked = true;
            self.document.forms[0].sInd_inqu[nIndex].disabled = true;
            self.document.forms[0].nInd_upda[nIndex].value = 1;
        }
        else
        {
            self.document.forms[0].nInd_inqu[nIndex].value = 2;
            self.document.forms[0].sInd_inqu[nIndex].checked = false;
            self.document.forms[0].sInd_inqu[nIndex].disabled = false;                
            self.document.forms[0].nInd_upda[nIndex].value = 2;
        } 
    }  
    
//+ Si corresponde a la columna de Consulta.

    if (nCol==3)
    {
        if (Field.checked)
            self.document.forms[0].nInd_inqu[nIndex].value = 1
        else
			self.document.forms[0].nInd_inqu[nIndex].value = 2;
    }
}

//- Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:05 $|$$Author: Iusr_llanquihue $"

</SCRIPT>







