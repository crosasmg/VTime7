<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
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
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valOfficeColumnCaption"), "valOffice", "table5556", 2,  ,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valOfficeColumnToolTip"))
		Call .AddHiddenColumn("hddSel", "")
		Call .AddHiddenColumn("hddOffice", "")
	End With
	
	With mobjGrid
		.Codispl = "MOP633"
		.Codisp = "MOP633"
		.sCodisplPage = "MSI633"
		.Top = 100
		.Height = 224
		.Width = 410
		.AddButton = False
		.DeleteButton = False
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("Sel").OnClick = "insUpdateSelection(this)"
	End With
End Sub

'%insPreMOP633. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMOP633()
	'------------------------------------------------------------------------------
	Dim lcolAgencies As eAgent.Agencies
	Dim lclsAgencies As Object
	
	Response.Write(mobjValues.ShowWindowsName("MOP633"))
	
	lcolAgencies = New eAgent.Agencies
	With mobjGrid
		
		If lcolAgencies.Find_Mop633(mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble), Session("nUser")) Then
			For	Each lclsAgencies In lcolAgencies
				.Columns("valOffice").DefValue = lclsAgencies.nOfficeAgen
				.Columns("hddOffice").DefValue = lclsAgencies.nOfficeAgen
				
				If lclsAgencies.nUsercode > 0 Then
					.Columns("Sel").Checked = 1
					.Columns("hddSel").DefValue = "1"
					.Columns("Sel").DefValue = "1"
				Else
					.Columns("Sel").Checked = 2
					.Columns("hddSel").DefValue = "0"
					.Columns("Sel").DefValue = "0"
				End If
				If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
					If .Columns("Sel").DefValue = "1" Then
						Response.Write(mobjGrid.DoRow())
					End If
				Else
					Response.Write(mobjGrid.DoRow())
				End If
			Next lclsAgencies
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lclsAgencies = Nothing
	lcolAgencies = Nothing
End Sub

'% insPreMOP633Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMOP633Upd()
	'------------------------------------------------------------------------------
	'    Dim lclscash_concepts 
	'    With Request
	'        If .QueryString("Action") = "Del" Then
	'            Response.Write mobjValues.ConfirmDelete()
	'            Set lclscash_concepts = CreateObject("eCashBank.cash_concepts")
	'			With lclscash_concepts
	'				.nCompany  = Session("nCompany")
	'				.nConcept  = mobjValues.StringToType(Request.QueryString("nConcept"), eFunctions.Values.eTypeData.etdDouble)
	'				.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
	'				.sStatregt = Request.QueryString("sStatregt")
	'				Call  .Delete()
	'			End With
	'       End If
	'          Response.Write mobjGrid.DoFormUpd(.QueryString("Action"), "ValMantCashBank.aspx", .QueryString("sCodispl"), .QueryString("nMainAction"),, .QueryString("Index"))
	'    End With
	'    Set lclscash_concepts = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
mobjValues.sCodisplPage = "MOP633"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">


<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:51 $|$$Author: Nvaplat61 $"

//---------------------------------------------------------------------------------------------------------
function insUpdateSelection(lobj){
//---------------------------------------------------------------------------------------------------------
	if(mintArrayCount>0)
	    if(lobj.checked==false){
		    self.document.forms[0].hddSel[lobj.value].value = "0";
		    marrArray[lobj.value].Sel=false;
		}    
		else{
		    self.document.forms[0].hddSel[lobj.value].value = "1";
		    marrArray[lobj.value].Sel=true;	
		}    
	else
		if(lobj.checked==false){
		      self.document.forms[0].hddSel.value = "0";
		      marrArray[lobj.value].Sel=false;		    
		}      
		else{
		      self.document.forms[0].hddSel.value = "1";
		      marrArray[lobj.value].Sel=true;		    
		}  
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MOP633", "MOP633.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("MOP633"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMOP633" ACTION="ValMantCashBank.aspx?sZone=2">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOP633()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMOP633Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





