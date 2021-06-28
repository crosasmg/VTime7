<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página

Dim mobjMenues As eFunctions.Menues

Dim mintCount As Byte

Dim mcolTab_compros As eAgent.Tab_compros


'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columns del Grid
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100007, GetLocalResourceObject("tcnLineColumnCaption"), "tcnLine", 3, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tcnLineColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(100005, GetLocalResourceObject("cbeDebitSideColumnCaption"), "cbeDebitSide", "Table287", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDebitSideColumnToolTip"))
		Call .AddPossiblesColumn(100005, GetLocalResourceObject("cbeTyp_accoColumnCaption"), "cbeTyp_acco", "table41", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTyp_accoColumnToolTip"))
		Call .AddPossiblesColumn(100006, GetLocalResourceObject("cbeTyp_amountColumnCaption"), "cbeTyp_amount", "table532", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTyp_amountColumnToolTip"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Columns("cbeDebitSide").EditRecord = True
		.Codispl = "MAG005"
		.Codisp = "MAG005"
		.sCodisplPage = "MAG005"
		
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
			mobjGrid.Columns("Sel").GridVisible = False
		End If
		
		.sDelRecordParam = "nType_tran=" & mobjValues.typeToString(Session("nType_tran"), eFunctions.Values.eTypeData.etdDouble) & "&nLine='+marrArray[lintIndex].tcnLine + '"
		.Height = 260
		.Width = 400
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMAG005()
	'------------------------------------------------------------------------------
	
	Dim lclsTab_compro As Object
	
	If mcolTab_compros.Find(mobjValues.StringToType(Session("nType_tran"), eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsTab_compro In mcolTab_compros
			With mobjGrid
				.Columns("tcnLine").DefValue = lclsTab_compro.nLine
				.Columns("cbeDebitSide").DefValue = lclsTab_compro.sDebitSide
				.Columns("cbeTyp_acco").DefValue = lclsTab_compro.nTyp_acco
				.Columns("cbeTyp_amount").DefValue = lclsTab_compro.nTyp_amount
			End With
			
			'+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			
			Response.Write(mobjGrid.DoRow())
			mintCount = lclsTab_compro.nLine
		Next lclsTab_compro
		Response.Write(mobjValues.HiddenControl("nRecordCount", CStr(mintCount + 1)))
	Else
		'+ Si no consigue data asociada, es decir,
		'+ si se está registrando información la primera vez el control oculto vale uno (1) - ACM - 14/05/2002
		Response.Write(mobjValues.HiddenControl("nRecordCount", CStr(1)))
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub
'------------------------------------------------------------------------------
Private Sub insPreMAG005Upd()
	'------------------------------------------------------------------------------
	Dim lclsTab_compro As eAgent.Tab_compro
	
	If Request.QueryString.Item("Action") = "Del" Then
		
		lclsTab_compro = New eAgent.Tab_compro
		
		Response.Write(mobjValues.ConfirmDelete())
		With lclsTab_compro
			.nType_tran = mobjValues.StringToType(Request.QueryString.Item("nType_tran"), eFunctions.Values.eTypeData.etdDouble)
			.nLine = mobjValues.StringToType(Request.QueryString.Item("nLine"), eFunctions.Values.eTypeData.etdDouble)
			.Delete()
		End With
		
		lclsTab_compro = Nothing
	End If
	
	With Response
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantAgent.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("MainAction"),  , CShort(Request.QueryString.Item("Index"))))
		.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
	
	If Request.QueryString.Item("Action") = "Add" Then
		Call mcolTab_compros.Find(mobjValues.StringToType(Session("nType_tran"), eFunctions.Values.eTypeData.etdDouble))
		Response.Write("<SCRIPT>self.document.forms[0].elements['tcnLine'].value=top.opener.document.forms[0].elements['nRecordCount'].value;</" & "Script>")
	End If
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG005"
mcolTab_compros = New eAgent.Tab_compros

%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:34 $"
</SCRIPT>    
    
    <%=mobjValues.StyleSheet()%>
    <%="<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MAG005", "MAG005"))
	mobjMenues = Nothing
End If
%>
<SCRIPT>
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------
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
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmAutomaticAcc" ACTION="valMantAgent.aspx?mode=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("<BR>")
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG005()
Else
	Call insPreMAG005Upd()
End If
mobjValues = Nothing
mcolTab_compros = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
mobjGrid = Nothing
%>





