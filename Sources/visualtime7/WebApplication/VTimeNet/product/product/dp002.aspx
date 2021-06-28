<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader:Permite definir las columnas del grid, así como habilitar o inhabilitar el 
'% botón de eliminar y registrar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "dp002"
	
	'+ Se definen las columnas del Grid.
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "DP002"
	End With
	
	With mobjGrid.Columns
            .AddNumericColumn(41214, GetLocalResourceObject("tcnProductColumnCaption"), "tcnProduct", 7, vbNullString, , GetLocalResourceObject("tcnProductColumnToolTip"), , , , , , Not (Request.QueryString.Item("Action") = "Add"))
		.AddTextColumn(41215, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		.AddTextColumn(41216, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, vbNullString,  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		.AddPossiblesColumn(41212, GetLocalResourceObject("cbeBranchtColumnCaption"), "cbeBrancht", "Table37", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBranchtColumnCaption"))
		.AddPossiblesColumn(41213, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(2),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatregtColumnCaption"))
	End With
	
	With mobjGrid
		.Height = 260
		.Width = 330
		.bCheckVisible = Request.QueryString.Item("Action") <> "Add"
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
				.ActionQuery = True
			End If
		End If
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionDuplicateProduct) Then
			.Columns("Sel").GridVisible = True
			.Columns("Sel").Disabled = False
			.AddButton = False
			.DeleteButton = False
		End If
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
			.Columns("tctDescript").EditRecord = True
			.AddButton = True
			.DeleteButton = False
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End If
	End With
End Sub

'% insPreDP002: Se definen los objetos a ser utilizados.
'-----------------------------------------------------------------------------------------
Private Sub insPreDP002()
	'-----------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lintIndex As Object
	Dim lcolProducts As eProduct.Products
	Dim lclsProduct As Object
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//% insPreZone: Se definen las acciones a utilizar." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 310:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("//% insProductDuplicate: Este evento es llamado desde el OnClick de la columna selección cuando " & vbCrLf)
Response.Write("//% se está duplicando un producto. Permite realizar el llamado a la página que duplica el producto " & vbCrLf)
Response.Write("//% seleccionado." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insProductDuplicate(Field, nProduct, dEffecdate){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	if (Field.checked) {" & vbCrLf)
Response.Write("		ShowPopUp(""DP063_k.aspx?nProduct=""+nProduct+""&dEffecdate=""+dEffecdate,""DP063"",250,200,""no"",""no"",100,100);" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
	'+ Se setea el objeto y se realiza la lectura del o los registros a ser mostrados
	'+ en las columnas del grid.
	lcolProducts = New eProduct.Products
	
	If lcolProducts.Find(CShort(session("nBranch")), CDate(session("dEffecdate")), True) Then
		lintCount = 0
		
		For	Each lclsProduct In lcolProducts
			With lclsProduct
				mobjGrid.Columns("tcnProduct").DefValue = .nProduct
				mobjGrid.Columns("tctDescript").DefValue = .sDescript
				mobjGrid.Columns("tctShort_des").DefValue = .sShort_des
				mobjGrid.Columns("cbeBrancht").DefValue = .sBrancht
				mobjGrid.Columns("cbeStatregt").DefValue = .sStatregt
				mobjGrid.Columns("Sel").OnClick = "insProductDuplicate(this," & CStr(.nProduct) & ",""" & mobjValues.TypeToString(.dEffecdate, eFunctions.Values.eTypeData.etdDate) & """)"
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lclsProduct
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolProducts = Nothing
	lclsProduct = Nothing
End Sub

'% insPreDP002Upd: Permite realizar el llamado a la ventana PopUp.
'-----------------------------------------------------------------------------------------
Private Sub insPreDP002Upd()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProduct.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp002"
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
'+ Se realiza el llamado a las rutinas generales para cargar la página invocada.
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP002", "DP002.aspx"))
		mobjMenu = Nothing
	End If
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		mobjValues.ActionQuery = True
	End If
End With
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP002" ACTION="valProduct.aspx?sZone=2">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("DP002"))
	Call insPreDP002()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreDP002Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




