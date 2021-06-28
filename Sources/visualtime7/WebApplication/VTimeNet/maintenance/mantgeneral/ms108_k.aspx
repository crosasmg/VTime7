<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddNumericColumn(100026, GetLocalResourceObject("tcnLocalColumnCaption"), "tcnLocal", 5, "",  , GetLocalResourceObject("tcnLocalColumnToolTip"))
		Call .AddTextColumn(100027, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(100028, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, "",  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(100030, GetLocalResourceObject("tcnProvince1ColumnCaption"), "tcnProvince1", 30, "",  , GetLocalResourceObject("tcnProvince1ColumnToolTip"))
			Call .AddHiddenColumn("tcnProvince", CStr(0))
		Else
			Call .AddPossiblesColumn(100026, GetLocalResourceObject("tcnProvinceColumnCaption"), "tcnProvince", "Province", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnProvinceColumnToolTip"))
		End If
		Call .AddTextColumn(100029, GetLocalResourceObject("tctLegal_locColumnCaption"), "tctLegal_loc", 6, "",  , GetLocalResourceObject("tctLegal_locColumnToolTip"))
		Call .AddHiddenColumn("sParam", vbNullString)
		Call .AddHiddenColumn("nProvince", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codisp = "MS108_K"
		.Codispl = "MS108"
		.sCodisplPage = "MS108"
		.AddButton = True
		.DeleteButton = True
		.Top = 70
		.Width = 330
		.Height = 270
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
		End If
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctDescript").EditRecord = True
		.sDelRecordParam = "nLocal='+ marrArray[lintIndex].tcnLocal + '"
	End With
End Sub

'% insPreMS108: Carga los datos en el grid de la forma "Folder" 
'---------------------------------------------------------------
Private Sub insPreMS108()
	'---------------------------------------------------------------
	Dim lcolTab_locats As eGeneralForm.Tab_locats
	Dim lclsTab_locat As Object
	
	lcolTab_locats = New eGeneralForm.Tab_locats
	
	If lcolTab_locats.Find Then
		For	Each lclsTab_locat In lcolTab_locats
			With mobjGrid
				.Columns("tcnLocal").DefValue = lclsTab_locat.nLocal
				.Columns("tctDescript").DefValue = lclsTab_locat.sDescript
				.Columns("tctShort_des").DefValue = lclsTab_locat.sShort_des
				.Columns("tcnProvince1").DefValue = lclsTab_locat.sDescript_Prov
				.Columns("tcnProvince").DefValue = lclsTab_locat.nProvince
				.Columns("tctLegal_loc").DefValue = lclsTab_locat.sLegal_loc
				.Columns("sParam").DefValue = "nLocal=" & lclsTab_locat.nLocal
				
				Response.Write(.DoRow)
			End With
		Next lclsTab_locat
		
		'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
		Response.Write(mobjGrid.CloseTable())
		Response.Write(mobjValues.BeginPageButton)
	End If
	
	lclsTab_locat = Nothing
	lcolTab_locats = Nothing
End Sub

'% insPreMS108Upd: Gestiona lo relacionado a la actualización de un registro del Grid
'------------------------------------------------------------------------------------
Private Sub insPreMS108Upd()
	'------------------------------------------------------------------------------------
	Dim lclsTab_locat As eGeneralForm.Tab_locat
	lclsTab_locat = New eGeneralForm.Tab_locat
	
	With Request
		If .QueryString.Item("Action") = "Update" Then
			mobjGrid.Columns("tcnLocal").Disabled = True
		End If
		
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsTab_locat.nLocal = mobjValues.StringToType(.QueryString.Item("nLocal"), eFunctions.Values.eTypeData.etdDouble)
			lclsTab_locat.Delete()
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantGeneral.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
	End With
	
	lclsTab_locat = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS108"
%>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:00 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\mantgeneral\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%End If%>
	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





    <%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	With Response
		.Write(mobjMenu.MakeMenu("MS108", "MS108_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjMenu = Nothing
End If
%>

<SCRIPT>
//% insStateZone: 
//-----------------------
function insStateZone(){}
//-----------------------

//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
			document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	<FORM METHOD="post" ID="FORM" NAME="MS108_K" ACTION="valMantGeneral.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS108()
Else
	Call insPreMS108Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>     
	</FORM>
</BODY>
</HTML>





