<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
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
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", 5, "0",  , GetLocalResourceObject("valConceptColumnToolTip"))
			Call .AddPossiblesColumn(0, GetLocalResourceObject("cboDescriptColumnCaption"), "cboDescript", "table22", 1,  ,  ,  ,  ,  ,  ,  , 30, GetLocalResourceObject("cboDescriptColumnToolTip"))
		Else
			Call .AddPossiblesColumn(0, GetLocalResourceObject("valConceptColumnCaption"), "valConcept", "table22", 2,  ,  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("valConceptColumnToolTip"))
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboStatregtColumnCaption"), "cboStatregt", "Table26", 1,  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("cboStatregtColumnCaption"))
	End With
	
	With mobjGrid
		.Codispl = "MOP699"
		.Codisp = "MOP699"
		.sCodisplPage = "MOP699"
		.Top = 100
		.Height = 224
		.Width = 410
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valConcept").EditRecord = True
		.Columns("valConcept").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cboStatregt").TypeList = 2
		.Columns("cboStatregt").List = "2"
		.sDelRecordParam = "nConcept='+ marrArray[lintIndex].valConcept + '" & "&sStatregt=' + marrArray[lintIndex].cboStatregt + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'%insPreMOP699. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMOP699()
	'------------------------------------------------------------------------------
	Dim lcolcash_conceptss As eCashBank.cash_conceptss
	Dim lclscash_concepts As Object
	
	Response.Write(mobjValues.ShowWindowsName("MOP699"))
	
	lcolcash_conceptss = New eCashBank.cash_conceptss
	With mobjGrid
		If lcolcash_conceptss.Find(Session("nUsercode"), Session("nCompany")) Then
			For	Each lclscash_concepts In lcolcash_conceptss
				.Columns("valConcept").DefValue = lclscash_concepts.nConcept
				If Request.QueryString.Item("Type") <> "PopUp" Then
					.Columns("cboDescript").DefValue = lclscash_concepts.nConcept
				End If
				.Columns("cboStatregt").DefValue = lclscash_concepts.sStatregt
				Response.Write(mobjGrid.DoRow())
			Next lclscash_concepts
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclscash_concepts = Nothing
	lcolcash_conceptss = Nothing
End Sub

'% insPreMOP699Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMOP699Upd()
	'------------------------------------------------------------------------------
	Dim lclscash_concepts As eCashBank.cash_concepts
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclscash_concepts = New eCashBank.cash_concepts
			With lclscash_concepts
				.nCompany = Session("nCompany")
				.nConcept = mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble)
				.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
				.sStatregt = Request.QueryString.Item("sStatregt")
				Call .Delete()
			End With
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantCashBank.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclscash_concepts = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
mobjValues.sCodisplPage = "MOP699"
%>



<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
//+Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:51 $|$$Author: Nvaplat61 $"
</SCRIPT>

<HTML>
  <HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MOP699", "MOP699.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("MOP699"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMOP699" ACTION="ValMantCashBank.aspx?sZone=2">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOP699()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMOP699Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





