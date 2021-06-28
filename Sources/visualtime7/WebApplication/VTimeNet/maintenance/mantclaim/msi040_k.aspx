<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim nRow As Integer
Dim mintRowscount As Integer


'% insDefineHeader: Se definen las columns del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
            .AddNumericColumn(0, GetLocalResourceObject("tcnIdCatasColumnCaption"), "tcnIdCatas", 10, , , GetLocalResourceObject("tcnIdCatasColumnToolTip"), , , , , , Request.QueryString.Item("Action") = "Update")
            .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "", , GetLocalResourceObject("tctDescriptColumnToolTip"))
            .AddTextColumn(0, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, "", , GetLocalResourceObject("tctShort_desColumnToolTip"))
            .AddPossiblesColumn(0, GetLocalResourceObject("tcnNumberColumnCaption"), "tcnNumber", "TABCONTR_NPROC", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "ChangeVal()", , , GetLocalResourceObject("tcnNumberColumnToolTip"))
            With mobjGrid
                .Columns("tcnNumber").Parameters.Add("NBRANCH", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("tcnNumber").Parameters.Add("NTYPE", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("tcnNumber").Parameters.Add("NTYPE_REL", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("tcnNumber").Parameters.ReturnValue("NBRANCH", , , True)
                .Columns("tcnNumber").Parameters.ReturnValue("NTYPE", , , True)
                .Columns("tcnNumber").Parameters.ReturnValue("NTYPE_REL", , , True)
            End With
            
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeStatregtColumnToolTip"))
            
            .AddHiddenColumn("tcnBranch", 0)
            .AddHiddenColumn("tcnType_rel", 0)
            .AddHiddenColumn("tcnType", 0)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Columns("tcnIdCatas").EditRecord = True
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MSI040_K"
            .sCodisplPage = "MSI040"
            .sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&tcnIdCatas='+ marrArray[lintIndex].tcnIdCatas + '"
		.Width = 450
		.Height = 450
		.Top = 60
		.Left = 30
		If Request.QueryString.Item("Action") = "Add" Then
			.CancelScript = "insCancelScript();"
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Or Request.QueryString.Item("nMainAction") = vbNullString
	End With
End Sub
'% insPreMSI040_K: Proceso que carga los Valores de las columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI040_K()
	'--------------------------------------------------------------------------------------------
        Dim lcolTab_catevents As eClaim.Tab_Catevents
        Dim lclsTab_catevent As Object
        Dim lintIndex As Short
        lcolTab_catevents = New eClaim.Tab_Catevents
        lintIndex = 0
	
        If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
            nRow = 1
        Else
            nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
        End If
	
        If lcolTab_catevents.Find() Then
            mintRowscount = lcolTab_catevents.Count
            For Each lclsTab_catevent In lcolTab_catevents
                With mobjGrid
                    .Columns("tcnIdCatas").DefValue = CStr(lclsTab_catevent.nIdcatas)
                    .Columns("tctDescript").DefValue = lclsTab_catevent.sDescript
                    .Columns("tctShort_des").DefValue = CStr(lclsTab_catevent.sShort_Des)
                    .Columns("tcnNumber").DefValue = lclsTab_catevent.nNumber
                    .Columns("cbeStatregt").DefValue = CStr(lclsTab_catevent.sStatregt)
                    .Columns("tcnBranch").DefValue = lclsTab_catevent.nBranch
                    .Columns("tcnType_rel").DefValue = lclsTab_catevent.nType_Rel
                    .Columns("tcnType").DefValue = lclsTab_catevent.nType
                    '+ Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid					
                    Response.Write(mobjGrid.DoRow())
                    lintIndex = lintIndex + 1
                End With
			
                'Response.Write("<SCRIPT>" & "insAddCustomerFields(""" & lclsTab_catevent.nIdcatas & """)</" & "Script>")
            Next lclsTab_catevent
        End If
        Response.Write(mobjGrid.closeTable())
	
        lclsTab_catevent = Nothing
        lcolTab_catevents = Nothing
	
    End Sub

    '% insPreMSI040_K_Upd: Proceso que Actualiza los valores de un registro del Grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreMSI040_K_Upd()
        '--------------------------------------------------------------------------------------------
        Dim lclsTab_catevent As eClaim.Tab_Catevent
        Dim lstrErrors As String
        With Request
            If .QueryString.Item("Action") = "Del" Then
                lclsTab_catevent = New eClaim.Tab_Catevent
                lstrErrors = lclsTab_catevent.InsValMSI040_K(.QueryString("sCodispl"), .QueryString.Item("Action"), _
                                                                                    mobjValues.StringToType(.QueryString.Item("tcnIdCatas"), eFunctions.Values.eTypeData.etdLong), _
                                                                                    mobjValues.StringToType(.QueryString.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                    mobjValues.StringToType(.QueryString.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                    mobjValues.StringToType(.QueryString.Item("tcnType_rel"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                    mobjValues.StringToType(.QueryString.Item("tcnBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                                                    .QueryString.Item("tctDescript"), _
                                                                                    .QueryString.Item("tctShort_des"), _
                                                                                    .QueryString.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
                If lstrErrors = vbNullString Then
                    Response.Write(mobjValues.ConfirmDelete())
                    Call lclsTab_catevent.InspostSI040Upd(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("tcnIdCatas"), eFunctions.Values.eTypeData.etdLong), _
                                                                                     .QueryString.Item("tctDescript"), .QueryString.Item("tctShort_des"), mobjValues.StringToType(.QueryString.Item("tcnNumber"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("cbeStatregt"), mobjValues.StringToType(.QueryString.Item("tcnBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnType_rel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
                Else
                    Response.Write(lstrErrors)
                End If
                lclsTab_catevent = Nothing
            End If
        End With
	
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(Request.QueryString.Item("Index"))))
        Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

Session("nExists_reg") = 1
mobjValues.sCodisplPage = "MSI040"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"> </SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MSI040_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
	var marrMSI040		= []
	var mintCount		= -1
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"
	//% insAddCustomerFields: Añade los registros obtenidos en la consulta a un arreglo - VCVG - 25/10/2001


//------------------------------------------------------------------------------------------------------------
function ChangeVal() {
    //------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        tcnBranch.value = tcnNumber_NBRANCH.value;
        tcnType.value  = tcnNumber_NTYPE.value;
        tcnType_rel.value = tcnNumber_NTYPE_REL.value;
    }
}
//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------
//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
			document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la Finalización de la página
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
	self.document.location.href = lstrURL;
}	
//% insCancelScript: Cancela PopUp
//------------------------------------------------------------------------------------------------------------------------
function insCancelScript(){
//-----------------------------------------------------------------------------------------	
    var strParams; 
	var lobjself_doc_form = self.document.forms[0]; 
	strParams = "nProvider=" + lobjself_doc_form.tcnCodigo.value + 
				"&nTypProvider=" + lobjself_doc_form.cbeTypProvider.value + 
				"&sClient=" + lobjself_doc_form.dtcClient.value;
 
	insDefValues("CancelUpdMSI040",strParams,'/VTimeNet/Maintenance/MantClaim'); 
} 
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProviders" ACTION="valMantClaim.aspx?sMode=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR></BR>")
End If
Response.Write("<SCRIPT>var	nMainAction	= '" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write("<BR>")
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI040_K()
Else
	Call insPreMSI040_K_Upd()
End If
%>
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')", mintRowscount <> 50)%>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





