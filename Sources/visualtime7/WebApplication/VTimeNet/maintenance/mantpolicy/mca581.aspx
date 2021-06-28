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
	
	mobjGrid.sCodisplPage = "mca581"
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		'+ Cantidad
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuantityColumnCaption"), "tcnQuantity", 5, CStr(0),  , GetLocalResourceObject("tcnQuantityColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		
		'+ % Descuento
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRate_discColumnCaption"), "tcnRate_disc", 4, CStr(0),  , GetLocalResourceObject("tcnRate_discColumnToolTip"),  , 2,  ,  ,  , False)
		
		'+ Columnas ocultas
		.AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = "MCA581"
		.Codisp = "MCA581"
		.Top = 100
		.Height = 210
		.Width = 390
		.ActionQuery = mobjValues.ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnQuantity").EditRecord = True
		.sEditRecordParam = "nQuantity=" & Request.QueryString.Item("nQuantity") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		.sDelRecordParam = "nQuantity=' + marrArray[lintIndex].tcnQuantity + '&dEffecdate=" & Request.QueryString.Item("dEffecdate")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMCA581. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreMCA581()
	'------------------------------------------------------------------------------
	Dim lcolDisc_quantitys As eBranches.Disc_quantitys
	Dim lclsDisc_quantity As Object
	
	With Request
		lcolDisc_quantitys = New eBranches.Disc_quantitys
		With mobjGrid
			If lcolDisc_quantitys.Find(mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				For	Each lclsDisc_quantity In lcolDisc_quantitys
					
					.Columns("tcnQuantity").DefValue = lclsDisc_quantity.nQuantity
					.Columns("tcnRate_disc").DefValue = lclsDisc_quantity.nRate_disc
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMCA581Upd cuando se eliminen los registros seleccionados - NVAPLAT9 - 11/03/2002
					
					.Columns("sParam").DefValue = "nQuantity=" & lclsDisc_quantity.nQuantity & "&dEffecdate=" & mobjValues.StringToType(Request.QueryString.Item("deffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nUserCode=" & Session("nUsercode")
					Response.Write(mobjGrid.DoRow())
				Next lclsDisc_quantity
			End If
		End With
		
	End With
	Response.Write(mobjGrid.CloseTable())
	
	lclsDisc_quantity = Nothing
	lcolDisc_quantitys = Nothing
End Sub

'% insPreMCA581Upd. Se define esta funcion para contruir el contenido de la 
'%                  ventana UPD de Tabla de descuentos por volumen
'------------------------------------------------------------------------------------
Private Sub insPreMCA581Upd()
	'------------------------------------------------------------------------------------
	Dim lclsDisc_quantity As eBranches.Disc_quantity
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsDisc_quantity = New eBranches.Disc_quantity
			Call lclsDisc_quantity.insPostMCA581Upd("Del", mobjValues.StringToType(.QueryString.Item("nQuantity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nRate_disc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantPolicy.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsDisc_quantity = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "mca581"
%>

<HTML>
  <HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
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
		.Write(mobjMenu.setZone(2, "MCA581", "MCA581.aspx"))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMCA581" ACTION="valMantPolicy.aspx?dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>">
<%
Response.Write(mobjValues.ShowWindowsName("MCA581"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCA581()
Else
	Call insPreMCA581Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>




