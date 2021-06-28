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


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctIllnessColumnCaption"), "tctIllness", 8, "", True, GetLocalResourceObject("tctIllnessColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 50, "", True, GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctIll_OMSColumnCaption"), "tctIll_OMS", 6, "", False, GetLocalResourceObject("tctIll_OMSColumnToolTip"),  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", 1,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeStatregtColumnToolTip"), 2)
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAM003"
		.sCodisplPage = "MAM003"
		.Top = 100
		.Height = 256
		.Width = 460
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("Sel").OnClick = "InsSelected(this.value, this.checked)"
		.Columns("tctIllness").EditRecord = True
		.Columns("tctIllness").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAM003: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAM003()
	Dim lclsItem As Object
	'--------------------------------------------------------------------------------------------
	
	Dim lcoltab_am_ill As eBranches.tab_am_ills
	With Response
		
		lcoltab_am_ill = New eBranches.tab_am_ills
		
		With mobjGrid
			If lcoltab_am_ill.Find() Then
				For	Each lclsItem In lcoltab_am_ill
					.Columns("tctIllness").DefValue = lclsItem.sIllness
					.Columns("tctDescript").DefValue = lclsItem.sDescript
					.Columns("tctIll_OMS").DefValue = lclsItem.sIll_OMS
					.Columns("cbeStatregt").DefValue = lclsItem.sStatregt
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostMAM003 cuando se eliminen los registros seleccionados - VCVG - 05/12/2001
					
					.Columns("sParam").DefValue = "sIllness=" & lclsItem.sIllness & "&sDescript=" & lclsItem.sDescript & "&sIll_OMS=" & lclsItem.sIll_OMS & "&nUsercode=" & Session("nUsercode") & "&sStatregt=" & lclsItem.sStatregt
					
					Response.Write(mobjGrid.DoRow())
				Next lclsItem
			End If
		End With
		
		Response.Write(mobjGrid.CloseTable())
		Response.Write(mobjValues.BeginPageButton)
		lclsItem = Nothing
		lcoltab_am_ill = Nothing
	End With
End Sub

'% insPreMAM003Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAM003Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclstab_am_ill As eBranches.tab_am_ill
	Dim lobjErrors As eFunctions.Errors
	lobjErrors = New eFunctions.Errors
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclstab_am_ill = New eBranches.tab_am_ill
			
			Response.Write(mobjValues.ConfirmDelete())
			Call lclstab_am_ill.insPostMAM003("MAM003", "Del", .QueryString.Item("sIllness"), .QueryString.Item("sDescript"), .QueryString.Item("sIll_OMS"), .QueryString.Item("sStatregt"), mobjValues.StringToType(.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdLong))
			lclstab_am_ill = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantHealt.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAM003"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 28/10/03 12:35 $|$$Author: Nvaplat11 $"
	
//% insStateZone: Habilitación/Deshabilitación de campos de la forma según la acción a procesar.
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}

//%insPreZone: Se encarga de recargar la página según la acción en tratamiento.
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}

//%InsSelected: Selecciona o des-seleciona el elemento del grid.
//------------------------------------------------------------------------------------------
function InsSelected(nIndex, bChecked){
//------------------------------------------------------------------------------------------
	if (bChecked=true) {
		insDefValues("ShowDataMAM001", "sField=" + "DelIllness" + "&sIllness=" + marrArray[nIndex].tctIllness + "&nIndex=" + nIndex, '/VTimeNet/Maintenance/MantHealt');
	}
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insFinish: controla la acción de Finalizar de la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT> 
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAM003_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmIllness" ACTION="ValMantHealt.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAM003()
Else
	Call insPreMAM003Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




