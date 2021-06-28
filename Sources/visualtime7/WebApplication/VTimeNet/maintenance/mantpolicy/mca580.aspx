<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "mca580"
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		'+ Ramo
		Call .AddNumericColumn(0, GetLocalResourceObject("nBranchColumnCaption"), "nBranch", 5, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nBranch").PopUpVisible = False
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		
		'+ Producto
		Call .AddNumericColumn(0, GetLocalResourceObject("nProductColumnCaption"), "nProduct", 5, CStr(0),  ,  ,  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nProduct").PopUpVisible = False
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"),  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		
		'+ Estado del registro
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", 1,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		
		.AddHiddenColumn("hdddEffecdate", mobjValues.TypeToString(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		.AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "MCA580"
		.Codisp = "MCA580"
		.Top = 100
		.Height = 210
		.Width = 390
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeBranch").EditRecord = True
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMCA580. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMCA580()
	'------------------------------------------------------------------------------
	Dim lcolTab_branch_quants As eBranches.Tab_branch_quants
	Dim lclsTab_branch_quant As Object
	
	With Request
		lcolTab_branch_quants = New eBranches.Tab_branch_quants
		With mobjGrid
			If lcolTab_branch_quants.Find(mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				For	Each lclsTab_branch_quant In lcolTab_branch_quants
					
					.Columns("cbeBranch").DefValue = lclsTab_branch_quant.nBranch
					.Columns("nBranch").DefValue = lclsTab_branch_quant.nBranch
					.Columns("valProduct").DefValue = lclsTab_branch_quant.nProduct
					.Columns("nProduct").DefValue = lclsTab_branch_quant.nProduct
					.Columns("cbeStatregt").DefValue = lclsTab_branch_quant.sStatregt
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMCA580Upd cuando se eliminen los registros seleccionados - NVAPLAT9 - 11/03/2002
					
					.Columns("sParam").DefValue = "nBranch=" & lclsTab_branch_quant.nBranch & "&nProduct=" & lclsTab_branch_quant.nProduct & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nUserCode=" & Session("nUsercode")
					
					Response.Write(mobjGrid.DoRow())
				Next lclsTab_branch_quant
			End If
		End With
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsTab_branch_quant = Nothing
	lcolTab_branch_quants = Nothing
End Sub

'% insPreMCA580Upd. Se define esta funcion para contruir el contenido de la 
'%                  ventana UPD de Ramos-productos válidos para descuento por volumen
'------------------------------------------------------------------------------------
Private Sub insPreMCA580Upd()
	'------------------------------------------------------------------------------------
	Dim lclsTab_branch_quant As eBranches.Tab_branch_quant
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTab_branch_quant = New eBranches.Tab_branch_quant
			Call lclsTab_branch_quant.insPostMCA580Upd("Del", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .QueryString.Item("sStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantPolicy.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsTab_branch_quant = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "mca580"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:15 $|$$Author: Nvaplat61 $"
	
//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
            case "cbeModulec":
                cbeCover.Parameters.Param5.sValue=cbeModulec.value;
                break;
		}
	}
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MCA580", "MCA580.aspx"))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMCA580" ACTION="valMantPolicy.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName("MCA580"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCA580()
Else
	Call insPreMCA580Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>




