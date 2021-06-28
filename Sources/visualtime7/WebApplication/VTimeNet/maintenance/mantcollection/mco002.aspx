<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 11:44:26 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- The object to handling the general function for the load of values is define
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim nRow As Integer


'**%insDefineHeader: It defined the grid columns 
'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 11:44:26 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MCO002"
	
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(105846, GetLocalResourceObject("cbeNullcodeColumnCaption"), "cbeNullcode", "Table95", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeNullcodeColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
		
		
            Call .AddPossiblesColumn(105847, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nBranch") , , , , , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value", , , GetLocalResourceObject("cbeBranchColumnToolTip"))
		mobjGrid.Columns("cbeBranch").OnChange = "document.forms[0].valProduct.Parameters.Param1.sValue=this.value"
		Call .AddPossiblesColumn(105848, GetLocalResourceObject("valProductColumnCaption"), "valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valProductColumnToolTip"))
		mobjGrid.Columns("valProduct").Parameters.Add("nBranch", Request.QueryString.Item("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .AddPossiblesColumn(105849, GetLocalResourceObject("cbePolitypeColumnCaption"), "cbePolitype", "Table17", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbePolitypeColumnCaption"), eFunctions.Values.eTypeCode.eString)
		Call .AddPossiblesColumn(105850, GetLocalResourceObject("cbeTratypeiColumnCaption"), "cbeTratypei", "Table24", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 2, GetLocalResourceObject("cbeTratypeiColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
		Call .AddPossiblesColumn(105851, GetLocalResourceObject("cbePolicyColumnCaption"), "cbePolicy", "Table7505", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbePolicyColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		Call .AddPossiblesColumn(105852, GetLocalResourceObject("cbeCertifColumnCaption"), "cbeCertif", "Table7505", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 1, GetLocalResourceObject("cbeCertifColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		
	End With
	
	With mobjGrid
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Codispl = "MCO002"
		.Codisp = "MCO002"
		.Top = 100
		.Height = 350
		.Width = 520
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeNullcode").EditRecord = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		.Columns("cbeNullcode").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbeBranch").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("valProduct").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbePolitype").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("cbeTratypei").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nNullcode='+ marrArray[lintIndex].cbeNullcode + '" & "&nBranch='+ marrArray[lintIndex].cbeBranch + '" & "&nProduct='+ marrArray[lintIndex].valProduct + '" & "&sPolitype='+ marrArray[lintIndex].cbePolitype + '" & "&nTratypei='+ marrArray[lintIndex].cbeTratypei + '" & "&sPolicy='+ marrArray[lintIndex].cbePolicy + '" & "&sCertif='+ marrArray[lintIndex].cbeCertif + '"
		.sEditRecordParam = "dEffecdate=" & Request.QueryString.Item("dEffecdate")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		Call .SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
End Sub

'**% insPreMCO002: Show data in the grid
'%insPreMCO002: Muestra la información en el grid
'------------------------------------------------------------------------------
Private Sub insPreMCO002()
	'------------------------------------------------------------------------------
	Dim lcolrnullcondis As eCollection.rnullcondis
	Dim lclsrnullcondi As Object
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	With Request
		lcolrnullcondis = New eCollection.rnullcondis
		With mobjGrid
			If lcolrnullcondis.Find(mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), nRow) Then
				For	Each lclsrnullcondi In lcolrnullcondis
					.Columns("cbeNullcode").DefValue = lclsrnullcondi.nNullcode
					.Columns("cbeBranch").DefValue = lclsrnullcondi.nBranch
					.Columns("valProduct").Parameters.Add("nBranch", lclsrnullcondi.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Columns("valProduct").DefValue = lclsrnullcondi.nProduct
					.Columns("valProduct").Parameters.Add("nBranch", lclsrnullcondi.nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
					.Columns("cbePolitype").DefValue = lclsrnullcondi.sPolitype
					.Columns("cbePolicy").DefValue = lclsrnullcondi.sPolicy
					.Columns("cbeCertif").DefValue = lclsrnullcondi.sCertif
					.Columns("cbeTratypei").DefValue = lclsrnullcondi.nTratypei
					.sEditRecordParam = "nBranch=" & lclsrnullcondi.nBranch
					Response.Write(mobjGrid.DoRow())
				Next lclsrnullcondi
			End If
		End With
	End With
	Response.Write(mobjGrid.CloseTable())
	lclsrnullcondi = Nothing
	lcolrnullcondis = Nothing
End Sub

'**% insPreMCO002Upd: This function makes the call to updates an validates on the database
'% insPreMCO002Upd. Se define esta funcion para contruir el contenido de la 
'%                     ventana UPD de los archivos de datos particulares
'------------------------------------------------------------------------------
Private Sub insPreMCO002Upd()
	'------------------------------------------------------------------------------
	Dim lclsrnullcondi As eCollection.rnullcondi
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsrnullcondi = New eCollection.rnullcondi
			
			Call lclsrnullcondi.InsPostMCO002(False, .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nMainAction")), .QueryString.Item("Action"), Session("nUsercode"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nNullcode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("sPolitype"), CStr(.QueryString.Item("spolicy")), CStr(.QueryString.Item("sCertif")), mobjValues.StringToType(.QueryString.Item("nTratypei"), eFunctions.Values.eTypeData.etdInteger))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValMantCollection.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lclsrnullcondi = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MCO002")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 11:44:26 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MCO002"

%>



<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 30/10/03 19:10 $|$$Author: Nvaplat26 $"

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
				lstrURL = lstrURL + "&nRow=13"
			else{
				llngRow = insConvertNumber(llngRow) + 12;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 12;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}	
</SCRIPT>
<HTML>
    <HEAD>
<%
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MCO002", "MCO002.aspx"))
		mobjMenu = Nothing
	End If
End With
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST"	ID="FORM" NAME="frmMCO002" ACTION="ValMantCollection.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMCO002()
Else
	Call insPreMCO002Upd()
End If
%>	  
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')")%>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>	  
		</FORM>
	</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 11:44:26 a.m.
Call mobjNetFrameWork.FinishPage("MCO002")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




