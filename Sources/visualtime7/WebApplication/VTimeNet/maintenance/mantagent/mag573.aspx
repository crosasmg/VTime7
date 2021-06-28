<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim nRow As Integer
'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage("MAG573")
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeLower_levelColumnCaption"), "cbeLower_level", "tabInter_Typ", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull), False,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeLower_levelColumnToolTip"))
		Call .AddBranchColumn(40599, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"),  , "",  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddProductColumn(40600, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"),  , CStr(eRemoteDB.Constants.intNull), 4,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypPortColumnCaption"), "cbeTypPort", "Table5672", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeTypPortColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissColumnCaption"), "tcnCommiss", 5, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnCommissColumnToolTip"),  , 2)
	End With
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Columns("cbeLower_level").EditRecord = True
		.Codispl = "MAG573"
		.Codisp = "MAG573"
		.sCodisplPage = "MAG573"
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		If Request.QueryString.Item("Action") = "Add" Or Request.QueryString.Item("Action") = "Upd" Then
			mobjGrid.Columns("valProduct").Disabled = True
		End If
		.sDelRecordParam = "nBranch='+ marrArray[lintIndex].cbeBranch + '" & "&nProduct='+ marrArray[lintIndex].valProduct + '" & "&nInterTyp=" & mobjValues.typeToString(Session("nInterTyp"), eFunctions.Values.eTypeData.etdDouble) & "&nLower_level='+ marrArray[lintIndex].cbeLower_level + '" & "&nTypPort='+ marrArray[lintIndex].cbeTypPort + '" & "&dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
		.Height = 300
		.Width = 400
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'%insPreMAG573: Esta función se encarga de cargar los datos en la forma "Folder" 
'------------------------------------------------------------------------------
Private Sub insPreMAG573()
	'------------------------------------------------------------------------------
	Dim lcolSupervis_commiss As eAgent.Supervis_commiss
	Dim lclsSupervis_commis As Object
	Dim lintIndex As Short
	lcolSupervis_commiss = New eAgent.Supervis_commiss
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	If lcolSupervis_commiss.Find(mobjValues.StringToType(Session("nInterTyp"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), nRow) Then
		lintIndex = 0
		For	Each lclsSupervis_commis In lcolSupervis_commiss
			lintIndex = lintIndex + 1
			With mobjGrid
				.Columns("cbeLower_level").DefValue = lclsSupervis_commis.nLower_level
				.Columns("cbeLower_level").Descript = lclsSupervis_commis.sInterTypDes
				.Columns("cbeBranch").DefValue = lclsSupervis_commis.nBranch
				.Columns("cbeBranch").Descript = lclsSupervis_commis.sBranchDes
				.Columns("valProduct").DefValue = lclsSupervis_commis.nProduct
				.Columns("valProduct").Descript = lclsSupervis_commis.sProductDes
				.Columns("tcnCommiss").DefValue = lclsSupervis_commis.nCommiss
				.Columns("cbeTypPort").DefValue = lclsSupervis_commis.nTypPort
			End With
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			Response.Write(mobjGrid.DoRow())
		Next lclsSupervis_commis
	End If
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.BeginPageButton)
	lcolSupervis_commiss = Nothing
End Sub
'% insPreMAG573Upd: Se define esta función para contruir el contenido de la ventana "UPD"
'------------------------------------------------------------------------------
Private Sub insPreMAG573Upd()
	'------------------------------------------------------------------------------
	Dim lclsSupervis_commis As eAgent.Supervis_commis
	If Request.QueryString.Item("Action") = "Del" Then
		lclsSupervis_commis = New eAgent.Supervis_commis
		Response.Write(mobjValues.ConfirmDelete())
		With lclsSupervis_commis
			.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
			.nInterTyp = mobjValues.StringToType(Request.QueryString.Item("nInterTyp"), eFunctions.Values.eTypeData.etdDouble)
			.nCommiss = mobjValues.StringToType(Request.QueryString.Item("nCommiss"), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
			.nLower_level = mobjValues.StringToType(Request.QueryString.Item("nLower_level"), eFunctions.Values.eTypeData.etdDouble)
			.nTypPort = mobjValues.StringToType(Request.QueryString.Item("nTypPort"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		lclsSupervis_commis = Nothing
	End If
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
		If Request.QueryString.Item("Action") = "Upd" Then
			.Write("<SCRIPT>Disabled();</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MAG573")
mobjValues = New eFunctions.Values
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MAG573")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 15:34 $"
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
//insPreZone: Controla las acciones de Busqueda por Condicion
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
//% Disabled: Deshabilita los campos dependiendo de la acción
//---------------------------------------------------------------------------
function Disabled()
//---------------------------------------------------------------------------
{
	with (self.document.forms[0])
	{
		cbeLower_level.disabled = true
		cbeBranch.disabled = true
		valProduct.disabled = true
	}
}
</SCRIPT>    
    <%=mobjValues.StyleSheet()%>
    <%="<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage("MAG573")
	Response.Write(mobjMenues.setZone(2, "MAG573", "MAG573"))
	mobjMenues = Nothing
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTabLifeComm" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG573()
Else
	Call insPreMAG573Upd()
End If
%>
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')")%> 
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>
<%
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("MAG573")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




