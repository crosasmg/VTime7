<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(40590, GetLocalResourceObject("cbeBankAsocColumnCaption"), "cbeBankAsoc", "tabBank_Agree_Banks", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeBankAsocColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDateColumnCaption"), "tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Columns("cbeBankAsoc").NeedParam = CBool("1")
		.Columns("cbeBankAsoc").Parameters.Add("sType_Bankagree", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Codispl = "MCO782"
		.sCodisplPage = "MCO782"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 220
		.Width = 340
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.AddButton = True
		.DeleteButton = True
		.Columns("Sel").GridVisible = True
		.Columns("cbeBankAsoc").EditRecord = True
		.sDelRecordParam = "nBank='+ marrArray[lintIndex].cbeBankAsoc + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCO782()
	'--------------------------------------------------------------------------------------------
	Dim lclsMultipac As Object
	Dim lcolMultipac As eCollection.Bank_Agrees
	
	lcolMultipac = New eCollection.Bank_Agrees
	If lcolMultipac.FindMultipac(Session("cbeBank")) Then
		For	Each lclsMultipac In lcolMultipac
			With mobjGrid
				.Columns("cbeBankAsoc").DefValue = lclsMultipac.nBank
				.Columns("tcdDate").DefValue = lclsMultipac.dAgree_Date
				Response.Write(.DoRow)
			End With
		Next lclsMultipac
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolMultipac = Nothing
	lclsMultipac = Nothing
	
End Sub

'% insPreCodisplUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMCO782Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjClass As Object
	
	Dim lclsMultipac As eCollection.Bank_Agree
	Dim lstrErrors As Object
	If Request.QueryString.Item("Action") = "Del" Then
		
		Response.Write(mobjValues.ConfirmDelete())
		lclsMultipac = New eCollection.Bank_Agree
		
		With lclsMultipac
			.nBank_Lider = Session("cbeBank")
			.nBank = mobjValues.StringToType(Request.QueryString.Item("nBank"), eFunctions.Values.eTypeData.etdDouble)
			.DelMultipac()
		End With
		
		lclsMultipac = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCollection.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
		.Write(mobjValues.HiddenControl("cbeBank", Request.QueryString.Item("cbeBank")))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MCO782"
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT>
//- Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:57 $|$$Author: Nvaplat61 $"
    </SCRIPT>



<SCRIPT LANGUAGE=JavaScript>

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	
	Response.Write(mobjMenu.setZone(2, "MCO782", "MCO782.aspx"))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmTabBank" ACTION="valMantCollection.aspx?sMode=2">
<%Response.Write(mobjValues.ShowWindowsName("MCO782"))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMCO782Upd()
Else
	Call insPreMCO782()
End If
%>
</FORM> 
</BODY>
</HTML>





