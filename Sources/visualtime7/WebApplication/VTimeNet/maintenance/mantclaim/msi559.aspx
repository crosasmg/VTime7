<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de la página. 
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas. 
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo de las zonas de la pantalla. 
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Configura los títulos del encabezado del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		If Request.QueryString.Item("Action") = "Add" Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctServiceColumnCaption"), "tctService", 10, "",  , GetLocalResourceObject("tctServiceColumnToolTip"),  ,  ,  , False)
			Call .AddTextColumn(0, GetLocalResourceObject("tctSubServiceColumnCaption"), "tctSubService", 4, "",  , GetLocalResourceObject("tctSubServiceColumnToolTip"),  ,  ,  , False)
		Else
			Call .AddTextColumn(0, GetLocalResourceObject("tctServiceColumnCaption"), "tctService", 10, "",  , GetLocalResourceObject("tctServiceColumnToolTip"),  ,  ,  , True)
			Call .AddTextColumn(0, GetLocalResourceObject("tctSubServiceColumnCaption"), "tctSubService", 4, "",  , GetLocalResourceObject("tctSubServiceColumnToolTip"),  ,  ,  , True)
		End If
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "", True, GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
            Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), "valProduct", CStr(0), , , "insOnChangeBranch(this)")
            Call .AddProductColumn(0, GetLocalResourceObject("valProductCaption"), "valProduct", GetLocalResourceObject("valProductToolTip"), "cbeBranch", Request.Form.Item("valProduct"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MSI559"
		.Height = 320
		.Width = 400
		.AddButton = True
		.DeleteButton = True
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nYear=" & mobjValues.TypeToString(Session("nYear"), eFunctions.Values.eTypeData.etdDouble) & "&sService='+ marrArray[lintIndex].tctService + '" & "&sSubService='+ marrArray[lintIndex].tctSubService + '" & "&nCurrency=" & mobjValues.TypeToString(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.TypeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nUsercode=" & mobjValues.TypeToString(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
		
		.Columns("Sel").GridVisible = True
		.Columns("tctService").EditRecord = True
		
		If Request.QueryString.Item("nMainAction") = "401" Then
			.bOnlyForQuery = True
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		End If
		
		'+ Permite continuar si el check está marcado
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMSI559: Se cargan los datos repetitivos de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI559()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lclsTab_Fonasa As eClaim.Tab_Fonasa
	Dim lclsTab_Fonasas As eClaim.Tab_Fonasas
	
	lclsTab_Fonasa = New eClaim.Tab_Fonasa
	lclsTab_Fonasas = New eClaim.Tab_Fonasas
	
	
	If lclsTab_Fonasas.Find(mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsTab_Fonasa In lclsTab_Fonasas
			With mobjGrid
				.Columns("tctService").DefValue = lclsTab_Fonasa.sService
				.Columns("tctSubService").DefValue = lclsTab_Fonasa.sSubService
				.Columns("tctDescript").DefValue = lclsTab_Fonasa.sDescript
				.Columns("tcnAmount").DefValue = CStr(lclsTab_Fonasa.nAmount)
				'.Columns("cbeBranch").DefValue = CStr(lclsTab_Fonasa.nBranch)
				'.Columns("valProduct").DefValue = CStr(lclsTab_Fonasa.nProduct)
			End With
			' DoRow se encarga de mostrar los elementos del grid 
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_Fonasa
	End If
	Response.Write(mobjGrid.closeTable())
	
	lclsTab_Fonasa = Nothing
	lclsTab_Fonasas = Nothing
End Sub


'% insPreMSI559Upd : Permite realizar las actualizaciones sobre los aranceles Fonasa.
'-------------------------------------------------------------------------------------------
Private Sub insPreMSI559Upd()
	'-------------------------------------------------------------------------------------------
	
	' Accion para eliminacion de datos del grid
	Dim lclsTab_Fonasa As eClaim.Tab_Fonasa
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_Fonasa = New eClaim.Tab_Fonasa
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsTab_Fonasa
			.nYear = mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble)
			.sService = Request.QueryString.Item("sService")
			.sSubService = Request.QueryString.Item("sSubService")
			.nCurrency = mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			.nUsercode = Session("nUsercode")
			.Delete()
		End With
	End If
	
	With Response
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", "MSI559", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
%>  
<HTML> 
<HEAD> 
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:53 $"
    // insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
    // posibles que lo requieran y habilitar los campos que dependan del ramo.
    //-------------------------------------------------------------------------------------------------------------------
    function insOnChangeBranch(lcolumn) {
        //-------------------------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (lcolumn.value != '' && lcolumn.value != 0) {
                if (sAction != "Update") {
                    valProduct.disabled = false
                    btnvalProduct.disabled = false
                }
            }
        }
    }    
</SCRIPT>

    <%Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT> var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MSI559", "MSI559.aspx"))
	mobjMenu = Nothing
End If
Response.Write(mobjValues.ShowWindowsName("MSI559"))
Response.Write(mobjValues.WindowsTitle("MSI559"))
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmMSI559" ACTION="valMantClaim.aspx?sZone=2">
<%
'+ Se configura la estructura del grid, deacuerdo al tipo de ventana.
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMSI559Upd()
Else
	Call insPreMSI559()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>	
</FORM>
</BODY>
</HTML>

 






