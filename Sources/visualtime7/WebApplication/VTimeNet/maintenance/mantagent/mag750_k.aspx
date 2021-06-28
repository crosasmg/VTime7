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
		.Codisp = "MAG750_k"
		.sCodisplPage = "MAG750"
		.Top = 100
		.Height = 215
		.Width = 350
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

'%insPreMAG750: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG750()
	'--------------------------------------------------------------------------------------------
	Dim lcolgen_bonsups As eAgent.gen_bonsups
	Dim lclsgen_bonsup As Object
	lcolgen_bonsups = New eAgent.gen_bonsups
	
	With mobjGrid
		If lcolgen_bonsups.Find() Then
			For	Each lclsgen_bonsup In lcolgen_bonsups
				.Columns("tcnInit_Range").DefValue = lclsgen_bonsup.nInit_Range
				.Columns("tcnEnd_Range").DefValue = lclsgen_bonsup.nEnd_Range
				.Columns("tcnFactor").DefValue = lclsgen_bonsup.nFactor
				Response.Write(mobjGrid.DoRow())
			Next lclsgen_bonsup
		End If
	End With
	
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	
	lcolgen_bonsups = Nothing
End Sub

'% insPreMAG750Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG750Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsgen_bonsup As eAgent.gen_bonsup
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsgen_bonsup = New eAgent.gen_bonsup
			
			Call lclsgen_bonsup.insPostMAG750_K(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), "Del", Session("nUsercode"), mobjValues.StringToType(.QueryString.Item("pnInit_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("pnEnd_Range"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnFactor"), eFunctions.Values.eTypeData.etdDouble))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
	End With
	lclsgen_bonsup = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.Buffer = 1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG750"
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
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG750_k.aspx", 1, ""))
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
<FORM METHOD="post" ID="FORM" NAME="frmMAG750" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG750()
Else
	Call insPreMAG750Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






