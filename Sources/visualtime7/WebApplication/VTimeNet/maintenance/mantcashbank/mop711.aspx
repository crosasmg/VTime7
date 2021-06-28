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
'--------------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodConceptColumnCaption"), "tcnCodConcept", 5, CStr(0), False, GetLocalResourceObject("tcnCodConceptColumnToolTip"), False,  ,  ,  ,  , True)
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeConceptColumnCaption"), "cbeConcept", "Table293", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConceptColumnToolTip"), 1)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbestatregtColumnCaption"), "cbestatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbestatregtColumnCaption"), 2)
	End With
	
	With mobjGrid
		.Codispl = "MOP711"
		.Codisp = "MOP711"
		.sCodisplPage = "MOP711"
		.Left = 160
		.Top = 200
		.Height = 225
		.Width = 400
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbestatregt").TypeList = 2
		.Columns("cbestatregt").List = "2"
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcnCodConcept").EditRecord = True
		End If
		.Columns("cbeConcept").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "nConcept='+ marrArray[lintIndex].cbeConcept + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMOP711. Se crea la ventana madre (Principal)
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreMOP711()
	'--------------------------------------------------------------------------------------------------------------------
	Dim lcolpay_ord_concepts As eCashBank.pay_ord_conceptss
	Dim lclspay_ord_concepts As Object
	
	
	lcolpay_ord_concepts = New eCashBank.pay_ord_conceptss
	
	With mobjGrid
		
		If lcolpay_ord_concepts.Find(mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			For	Each lclspay_ord_concepts In lcolpay_ord_concepts
				.Columns("tcnCodConcept").DefValue = lclspay_ord_concepts.nConcept
				.Columns("cbeConcept").DefValue = lclspay_ord_concepts.nConcept
				.Columns("cbeStatregt").DefValue = lclspay_ord_concepts.sStatregt
				Response.Write(mobjGrid.DoRow())
			Next lclspay_ord_concepts
		End If
	End With
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolpay_ord_concepts = Nothing
End Sub

'% insPreMOP711Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreMOP711Upd()
	'--------------------------------------------------------------------------------------------------------------------
	Dim lclspay_ord_concepts As eCashBank.pay_ord_concepts
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclspay_ord_concepts = New eCashBank.pay_ord_concepts
			With lclspay_ord_concepts
				.nCompany = mobjValues.StringToType(Session("nCompany"), eFunctions.Values.eTypeData.etdDouble)
				.nConcept = mobjValues.StringToType(Request.QueryString.Item("nConcept"), eFunctions.Values.eTypeData.etdDouble)
				.sStatregt = Request.Form.Item("cbeStatregt")
				.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
				Call .Delete()
			End With
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantCashBank.aspx", "MOP711", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclspay_ord_concepts = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
mobjValues.sCodisplPage = "MOP711"
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
		.Write(mobjMenu.setZone(2, "MOP711", "MOP711.aspx"))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("MOP711"))
	.Write(mobjValues.ShowWindowsName("MOP711"))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMOP711" ACTION="ValMantCashBank.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOP711()
Else
	Call insPreMOP711Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>





