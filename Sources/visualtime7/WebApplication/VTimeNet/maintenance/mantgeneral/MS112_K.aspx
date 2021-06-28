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

Dim nRow As Double


'% insDefineHeader: Define las columnas del Grid
'-------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddNumericColumn(100026, GetLocalResourceObject("tcnMunicipalityColumnCaption"), "tcnMunicipality", 5, "",  , GetLocalResourceObject("tcnMunicipalityColumnToolTip"))
		Call .AddTextColumn(100027, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddTextColumn(100028, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, "",  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(100030, GetLocalResourceObject("tcnLocal1ColumnCaption"), "tcnLocal1", 30, "",  , GetLocalResourceObject("tcnLocal1ColumnToolTip"))
			Call .AddHiddenColumn("tcnLocal", CStr(0))
		Else
			mobjValues.Parameters.Add("nZip_code", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Call .AddPossiblesColumn(100026, GetLocalResourceObject("tcnLocalColumnCaption"), "tcnLocal", "TABTAB_LOCAT", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnLocalColumnToolTip"))
		End If
		
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codisp = "MS112_K"
		.Codispl = "MS112"
		.sCodisplPage = "MS112"
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
		.sDelRecordParam = "nMunicipality='+ marrArray[lintIndex].tcnMunicipality + '"
	End With
End Sub

'% insPreMS112: Carga los datos en el grid de la forma "Folder" 
'---------------------------------------------------------------
Private Sub insPreMS112()
	'---------------------------------------------------------------
	Dim lcolMunicipalitys As eGeneralForm.Municipalitys
	Dim lclsMunicipality As Object
	
	lcolMunicipalitys = New eGeneralForm.Municipalitys
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	If lcolMunicipalitys.Find(nRow) Then
		For	Each lclsMunicipality In lcolMunicipalitys
			With mobjGrid
				.Columns("tcnMunicipality").DefValue = lclsMunicipality.nMunicipality
				.Columns("tcnLocal").DefValue = lclsMunicipality.nLocal
				.Columns("tctDescript").DefValue = lclsMunicipality.sDescript
				.Columns("tctShort_des").DefValue = lclsMunicipality.sShort_des
				.Columns("tcnLocal1").DefValue = lclsMunicipality.sDescript_Prov
				Response.Write(.DoRow)
			End With
		Next lclsMunicipality
		
		'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
		Response.Write(mobjGrid.CloseTable())
		Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow"))))
		Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')"))
		Response.Write(mobjValues.BeginPageButton)
	End If
	
	lclsMunicipality = Nothing
	lcolMunicipalitys = Nothing
End Sub

'% insPreMS112Upd: Gestiona lo relacionado a la actualización de un registro del Grid
'------------------------------------------------------------------------------------
Private Sub insPreMS112Upd()
	'------------------------------------------------------------------------------------
	Dim lclsMunicipality As eGeneralForm.Municipality
	lclsMunicipality = New eGeneralForm.Municipality
	
	With Request
		If .QueryString.Item("Action") = "Update" Then
			mobjGrid.Columns("tcnMunicipality").Disabled = True
		End If
		
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsMunicipality.nMunicipality = mobjValues.StringToType(.QueryString.Item("nMunicipality"), eFunctions.Values.eTypeData.etdDouble)
			lclsMunicipality.Delete()
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantGeneral.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
	End With
	
	lclsMunicipality = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MS112"
%>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 4/11/03 19:07 $|$$Author: Nvaplat28 $"

//% insStateZone: 
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
}
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
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    lstrURL = lstrURL.replace(/&nMainAction=.*/,'')
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}	
	}
	if (top.frames['fraSequence'].plngMainAction!=0)
		lstrURL = lstrURL + "&nMainAction=" + top.frames['fraSequence'].plngMainAction
	self.document.location.href = lstrURL;
}	
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
		.Write(mobjMenu.MakeMenu("MS112", "MS112_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjMenu = Nothing
End If
%>


</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	<FORM METHOD="post" ID="FORM" NAME="MS112_K" ACTION="valMantGeneral.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS112()
Else
	Call insPreMS112Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>     
	</FORM>
</BODY>
</HTML>





