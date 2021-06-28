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


'%insDefineHeader. Definición de columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInit_RangeColumnCaption"), "tcnInit_Range", 18, "", True, GetLocalResourceObject("tcnInit_RangeColumnToolTip"), False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnEnd_RangeColumnCaption"), "tcnEnd_Range", 18, "", True, GetLocalResourceObject("tcnEnd_RangeColumnToolTip"), False, 6,  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFactorColumnCaption"), "tcnFactor", 5, "", True, GetLocalResourceObject("tcnFactorColumnToolTip"), False, 2,  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG751_k"
		.sCodisplPage = "MAG751"
		.Top = 100
		.Height = 215
		.Width = 320
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnInit_Range").EditRecord = True
		.Columns("tcnInit_Range").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnEnd_Range").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "pnInit_Range='+ marrArray[lintIndex].tcnInit_Range + '" & "&pnEnd_Range='+ marrArray[lintIndex].tcnEnd_Range + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG751: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG751()
	'--------------------------------------------------------------------------------------------
	Dim lcolaverage_prods As eAgent.average_prods
	Dim lclsaverage_prod As Object
	
	lcolaverage_prods = New eAgent.average_prods
	With mobjGrid
		If lcolaverage_prods.Find() Then
			For	Each lclsaverage_prod In lcolaverage_prods
				.Columns("tcnInit_Range").DefValue = lclsaverage_prod.nInit_Range
				.Columns("tcnEnd_Range").DefValue = lclsaverage_prod.nEnd_Range
				.Columns("tcnFactor").DefValue = lclsaverage_prod.nFactor
				Response.Write(mobjGrid.DoRow())
			Next lclsaverage_prod
		End If
	End With
	
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolaverage_prods = Nothing
End Sub

'% insPreMAG751Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG751Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsaverage_prod As eAgent.average_prod
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsaverage_prod = New eAgent.average_prod
			Call lclsaverage_prod.insPostMAG751_K(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), "Del", Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("pnInit_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnEnd_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsaverage_prod = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG751"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"

//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}
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
</SCRIPT> 
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG751_k.aspx", 1, ""))
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
<FORM METHOD="post" ID="FORM" NAME="frmMAG751" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG751()
Else
	Call insPreMAG751Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






