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

'- Objeto para el manejo de las rutinas genéricas

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la pantalla
Dim mobjMenues As eFunctions.Menues



'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage("MAG007")
	
	'+Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(40599, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insOnChangeBranch(this)", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBranchColumnCaption"))
		Call .AddPossiblesColumn(100015, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabprodmaster", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddPossiblesColumn(100016, GetLocalResourceObject("cbeDisexpriColumnCaption"), "cbeDisexpri", "Table30", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  , 4, GetLocalResourceObject("cbeDisexpriColumnToolTip"))
		Call .AddNumericColumn(100018, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnPercentColumnToolTip"), True, 2)
	End With
	
	'+Se asignan las caracteristicas del Grid
	
	With mobjGrid
		'+Se crean los parametros para las listas de valores posibles
		.Columns("valProduct").Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valProduct").Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("cbeBranch").EditRecord = True
		.Codispl = "MAG007"
		.Codisp = "MAG007"
		.sCodisplPage = "MAG007"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		'+ El tipo de esquema "Impuesto" (sDisexpri = 3) no debe ser mostrado
		If Request.QueryString.Item("Action") = "Add" Or Request.QueryString.Item("Action") = "Update" Then
			mobjGrid.Columns("cbeDisexpri").TypeList = 2
			mobjGrid.Columns("cbeDisexpri").List = CStr(3)
		End If
		
		'+Pase de parametros necesarios para la eliminación de registros
		.sDelRecordParam = "nEco_sche=" & mobjValues.typeToString(Session("nEco_sche"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nBranch='+ marrArray[lintIndex].cbeBranch + '" & "&nProduct='+ marrArray[lintIndex].valProduct + '" & "&nUsercode=" & mobjValues.typeToString(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
		.Height = 300
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'------------------------------------------------------------------------------
Private Sub insPreMAG007()
	'------------------------------------------------------------------------------
	Dim lcolDisex_int_ds As eAgent.Disex_int_ds
	Dim lclsDisex_int_d As Object
	
	lcolDisex_int_ds = New eAgent.Disex_int_ds
	If lcolDisex_int_ds.Find(mobjValues.StringToType(Session("nEco_sche"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsDisex_int_d In lcolDisex_int_ds
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsDisex_int_d.nBranch
				.Columns("valProduct").Parameters.Add("nBranch", lclsDisex_int_d.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = lclsDisex_int_d.nProduct
				.Columns("cbeDisexpri").DefValue = lclsDisex_int_d.sDisexpri
				.Columns("tcnPercent").DefValue = lclsDisex_int_d.nPercent
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsDisex_int_d
	End If
	Response.Write(mobjGrid.closeTable())
	lclsDisex_int_d = Nothing
	lcolDisex_int_ds = Nothing
End Sub

'------------------------------------------------------------------------------
Private Sub insPreMAG007Upd()
	'------------------------------------------------------------------------------
	Dim lclsDisex_int_d As eAgent.Disex_int_d
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsDisex_int_d = New eAgent.Disex_int_d
		
		Response.Write(mobjValues.ConfirmDelete())
		With lclsDisex_int_d
			.nEco_sche = mobjValues.StringToType(Request.QueryString.Item("nEco_sche"), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
			.nUsercode = mobjValues.StringToType(Request.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		
		lclsDisex_int_d = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
		If Request.QueryString.Item("Action") = "Add" Then
			.Write("<SCRIPT>if (document.forms[0].cbeBranch.value!=0)document.forms[0].cbeBranch.onchange();</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MAG007")

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG007"
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MAG007")

%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT>

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:34 $"
</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%="<script>var sMainAction='" & Request.QueryString.Item("Action") & "'</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage("MAG007")
	Response.Write(mobjMenues.setZone(2, "MAG007", "MAG007"))
	mobjMenues = Nothing
End If
%>
<SCRIPT>


//% insCancel: Se activa al cancelar la transacción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: Se activa al finalizar la transacción
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

// insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
// posibles que lo requieran y habilitar los campos que dependan del ramo.
//-------------------------------------------------------------------------------------------------------------------
function insOnChangeBranch(lcolumn)
//-------------------------------------------------------------------------------------------------------------------
{
	if(lcolumn.value!="" && lcolumn.value>0)
	{
		with (self.document.forms[0])
		{
		    valProduct.Parameters.Param1.sValue = lcolumn.value
		    if(sMainAction!="Update")
		    {
				valProduct.disabled = false;
				btnvalProduct.disabled = false;
			}
		}
	}
	else
	{
		self.document.forms[0].valProduct.disabled = true;
		self.document.forms[0].btnvalProduct.disabled = true;
	}
}


</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabEcoSche" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG007()
Else
	Call insPreMAG007Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("MAG007")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




