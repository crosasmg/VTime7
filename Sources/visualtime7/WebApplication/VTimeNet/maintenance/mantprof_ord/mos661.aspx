<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
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
	
	mobjGrid.sCodisplPage = "mos661"
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		'+ Código de tipo de órden de servicio
		Call .AddNumericColumn(0, GetLocalResourceObject("nOrd_typeCostColumnCaption"), "nOrd_typeCost", 5, CStr(0),  , GetLocalResourceObject("nOrd_typeCostColumnToolTip"),  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nOrd_typeCost").PopUpVisible = False
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeOrd_typeCostColumnCaption"), "cbeOrd_typeCost", "Table5597", eFunctions.Values.eValuesType.clngWindowType, CStr(0),  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeOrd_typeCostColumnToolTip"))
		
		'+ Costo del tipo de orden de servicio
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		
		.AddHiddenColumn("hddnCurrency", Request.QueryString.Item("nCurrency"))
		.AddHiddenColumn("hdddEffecdate", Request.QueryString.Item("dEffecdate"))
		
		.AddHiddenColumn("sParam", vbNullString)
		
	End With
	
	With mobjGrid
		.Codispl = "MOS661"
		.Codisp = "MOS661"
		.Top = 100
		.Height = 200
		.Width = 390
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeOrd_typeCost").EditRecord = True
		.Columns("cbeOrd_typeCost").Disabled = Request.QueryString.Item("Action") = "Update"
		.sEditRecordParam = "nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMOS661. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMOS661()
	'------------------------------------------------------------------------------
	Dim lcolOrd_types As eClaim.Ord_types
	Dim lclsOrd_type As Object
	
	With Request
		lcolOrd_types = New eClaim.Ord_types
		With mobjGrid
			If lcolOrd_types.Find(mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				
				For	Each lclsOrd_type In lcolOrd_types
					.Columns("nOrd_typeCost").DefValue = lclsOrd_type.nOrd_typeCost
					.Columns("cbeOrd_typeCost").DefValue = lclsOrd_type.nOrd_typeCost
					.Columns("tcnAmount").DefValue = lclsOrd_type.nAmount
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMOS661Upd cuando se eliminen los registros seleccionados - NVAPLAT9 - 05/04/2002
					.Columns("sParam").DefValue = "nCurrency=" & lclsOrd_type.nCurrency & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nOrd_typeCost=" & lclsOrd_type.nOrd_typeCost & "&nUserCode=" & Session("nUsercode")
					Response.Write(mobjGrid.DoRow())
				Next lclsOrd_type
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsOrd_type = Nothing
	lcolOrd_types = Nothing
	
End Sub

'% insPreMOS661Upd. Se define esta funcion para contruir el contenido de la 
'%                  ventana de actualización de la Tipos de órdenes de servicios
'%                  profesionales
'------------------------------------------------------------------------------
Private Sub insPreMOS661Upd()
	'------------------------------------------------------------------------------
	Dim lclsOrd_type As eClaim.Ord_type
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsOrd_type = New eClaim.Ord_type
			Call lclsOrd_type.insPostMOS661Upd("Del", mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(0), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantProf_ord.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsOrd_type = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "mos661"
%>

<HTML>
  <HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//-Variable para el control de Versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:20 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.setZone(2, "MOS661", "MOS661.aspx"))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmMOS661" ACTION="valMantProf_ord.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName("MOS661"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMOS661()
Else
	Call insPreMOS661Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>




