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

'- Objeto para el manejo de las zonas de la página

Dim mobjMenues As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'-------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage("MAG004")
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(41208, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  , "self.document.forms[0].valProduct.Parameters.Param1.sValue=this.value; EnableProductField(this.value);", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBranchColumnToolTip"))
		Call .AddPossiblesColumn(100775, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabprodmaster", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddNumericColumn(100776, GetLocalResourceObject("tcnPrem_initColumnCaption"), "tcnPrem_init", 18, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnPrem_initColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(100777, GetLocalResourceObject("tcnPrem_endColumnCaption"), "tcnPrem_end", 18, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnPrem_endColumnToolTip"), True, 6)
		Call .AddNumericColumn(100778, GetLocalResourceObject("tcnComrateColumnCaption"), "tcnComrate", 5, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tcnComrateColumnToolTip"), True, 2)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("valProduct").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valProduct").Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("cbeBranch").EditRecord = True
		.Codispl = "MAG004"
		.Codisp = "MAG004"
		.sCodisplPage = "MAG004"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
			.Columns("Sel").GridVisible = False
		End If
		.sDelRecordParam = "nTable_cod=" & mobjValues.typeToString(Session("nTable_cod"), eFunctions.Values.eTypeData.etdDouble) & "&nCurrency=" & mobjValues.typeToString(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble) & "&sType_infor=" & Session("sType_infor") & "&dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nBranch='+ marrArray[lintIndex].cbeBranch + '" & "&nProduct='+ marrArray[lintIndex].valProduct + '" & "&nPrem_init='+marrArray[lintIndex].tcnPrem_init + '"
		.Height = 260
		.Width = 350
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG004: Esta función se encarga de cargar los datos en la forma "Folder" 
'---------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG004()
	'---------------------------------------------------------------------------------------------------------------
	Dim lcolTab_comrats As eAgent.Tab_comrats
	Dim lclsTab_comrat As Object
	
	lcolTab_comrats = New eAgent.Tab_comrats
	
	If lcolTab_comrats.Find(mobjValues.StringToType(Session("nTable_cod"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Session("sType_infor"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsTab_comrat In lcolTab_comrats
			With mobjGrid
				.Columns("cbeBranch").DefValue = lclsTab_comrat.nBranch
				.Columns("valProduct").Parameters.Add("nBranch", lclsTab_comrat.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valProduct").DefValue = lclsTab_comrat.nProduct
				If lclsTab_comrat.nProduct = 0 Then
					.Columns("valProduct").DefValue = ""
				End If
				.Columns("tcnPrem_init").DefValue = lclsTab_comrat.nPrem_init
				.Columns("tcnPrem_end").DefValue = lclsTab_comrat.nPrem_end
				.Columns("tcnComrate").DefValue = lclsTab_comrat.nComrate
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_comrat
	End If
	Response.Write(mobjGrid.closeTable())
	
	Response.Write("<SCRIPT>top.fraHeader.$('#valTable_cod').change();</" & "Script>")
	lcolTab_comrats = Nothing
End Sub

'% insPreMAG004Upd: Se define esta función para contruir el contenido de la ventana "UPD"
'------------------------------------------------------------------------------------------------------
Private Sub insPreMAG004Upd()
	'------------------------------------------------------------------------------------------------------
	Dim lclsTab_comrat As eAgent.Tab_comrat
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTab_comrat = New eAgent.Tab_comrat
		
		Response.Write(mobjValues.ConfirmDelete())
		
		With lclsTab_comrat
			.nTable_cod = mobjValues.StringToType(Request.QueryString.Item("nTable_cod"), eFunctions.Values.eTypeData.etdDouble)
			.nCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
			.sType_infor = Request.QueryString.Item("sType_infor")
			.dEffecdate = mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
			.nBranch = mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
			.nPrem_init = mobjValues.StringToType(Request.QueryString.Item("nPrem_init"), eFunctions.Values.eTypeData.etdDouble)
			.nUsercode = mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		
		lclsTab_comrat = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MAG004")

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG004"
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MAG004")

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
    <%="<script>var sAction='" & Request.QueryString.Item("Action") & "'</script>"%>    
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	mobjNetFrameWork.sSessionID = Session.SessionID
	mobjNetFrameWork.nUsercode = Session("nUsercode")
	Call mobjNetFrameWork.BeginPage("MAG004")
	Response.Write(mobjMenues.setZone(2, "MAG004", "MAG004"))
	mobjMenues = Nothing
End If
%>
<SCRIPT>

//insPreZone:
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(nAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (nAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + nAction
	        break;
	}
}

//% insCancel: Ejecuta la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return(true);
}

//% insFinish: Ejecuta la acción Finalizar de la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return(true);
}

//EnableProductField: Habilita e inhabilita el campo "Producto" dependiendo del valor del campo "Ramo"
//----------------------------------------------------------------------------------------------------
function EnableProductField(nValue){
//----------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){ 
		if(nValue>0 && nValue!=""){
		    if(sAction.value!="Update"){
		        valProduct.disabled=false;
			    btnvalProduct.disabled=false;
			}    
		}
		else {
			valProduct.disabled=true;
			btnvalProduct.disabled=true;
		}

	    if(nValue=='' || nValue==0){
	        valProduct.value=''
	        $(valProduct).change(); 
            valProduct.disabled = true
	        btnvalProduct.disabled = true	               
	    }
	}
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabExtComm" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG004()
Else
	Call insPreMAG004Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("MAG004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




