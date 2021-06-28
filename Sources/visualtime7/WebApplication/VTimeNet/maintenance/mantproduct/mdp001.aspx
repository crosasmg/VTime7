<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'Dim insPreMDP001Upd() As Object

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues


'% insDefineHeader: Definición de columnas del Grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid
		.DeleteButton = False
		.AddButton = False
	End With
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddHiddenColumn("tcnSequen", CStr(eRemoteDB.Constants.strNull))
		Call .AddTextColumn(100811, GetLocalResourceObject("tctCodisplColumnCaption"), "tctCodispl", 15, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctCodisplColumnToolTip"))
		Call .AddHiddenColumn("tctCodispl2", "")
		Call .AddTextColumn(100802, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull), True, GetLocalResourceObject("tctDescriptColumnToolTip"))
		Call .AddCheckColumn(100803, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", " ")
		Call .AddHiddenColumn("tcnChecked", CStr(0))
		Call .AddHiddenColumn("tcnRChecked", "")
	End With
	mobjGrid.Columns("Sel").OnClick = "insSelect(this);"
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		'+ Si la transacción es "Consulta", se oculta la columna SEL
		If Request.QueryString.Item("nMainAction") = "401" Or Request.QueryString.Item("nMainAction") = vbNullString Then
			mobjGrid.ActionQuery = True
		End If
		.Height = 300
		.Width = 350
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% insPreMDP001: Muestra el Grid con los datos
'------------------------------------------------------------------------------
Private Sub insPreMDP001()
	'------------------------------------------------------------------------------
	Dim lcolTab_winpros As eProduct.Tab_winpros
	Dim lclsTab_winpro As eProduct.Tab_winpro
	
Response.Write("")

	
	Dim lintrecord_item As Short
	With Server
		lcolTab_winpros = New eProduct.Tab_winpros
		lclsTab_winpro = New eProduct.Tab_winpro
	End With
	lintrecord_item = 0
	If lcolTab_winpros.Find_Win(Session("sBrancht")) Then
		For	Each lclsTab_winpro In lcolTab_winpros
			With mobjGrid
				If lclsTab_winpro.nSequence = eRemoteDB.Constants.intNull Then
					.Columns("Sel").Checked = 0
					.Columns("tcnChecked").DefValue = CStr(0)
				Else
					.Columns("Sel").Checked = 1
					.Columns("tcnChecked").DefValue = CStr(1)
				End If
				.Columns("tcnSequen").DefValue = CStr(lclsTab_winpro.nSequence)
				.Columns("tctCodispl").DefValue = lclsTab_winpro.sCodispl
				.Columns("tctCodispl2").DefValue = lclsTab_winpro.sCodispl
				.Columns("tctDescript").DefValue = lclsTab_winpro.sDescript
				.Columns("chkRequire").Checked = mobjValues.StringToType(lclsTab_winpro.sRequire, eFunctions.Values.eTypeData.etdDouble)
				.Columns("tcnRChecked").DefValue = mobjValues.StringToType(lclsTab_winpro.sRequire, eFunctions.Values.eTypeData.etdDouble)
			End With
			'+ Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid
			mobjGrid.Columns("chkRequire").OnClick = "insCheckClick(this," & lintrecord_item & ")"
			lintrecord_item = lintrecord_item + 1
			Response.Write(mobjGrid.DoRow())
		Next lclsTab_winpro
	End If
	Session("nValue") = lcolTab_winpros.count
	Response.Write(mobjGrid.closeTable())
	lclsTab_winpro = Nothing
	lcolTab_winpros = Nothing
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
    <%="<script>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</script>"%>
    <%
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	Response.Write(mobjMenues.setZone(2, "MDP001", "MDP001.aspx"))
	mobjMenues = Nothing
End If
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:17 $|$$Author: Nvaplat61 $"

//% insCheckClick: Controla el check del campo tcnRChecked
//------------------------------------------------------------------------------------------
function insCheckClick(Field, nIndex){
//-------------------------------------------------------------------------------------------
   document.forms[0].tcnRChecked(nIndex).value= (Field.checked?1:2);
}
//% insSelect: Manejo del control Sel
//-------------------------------------------------------------------------------------------
function insSelect(Field){
//-------------------------------------------------------------------------------------------
   var lintIndex=0

    if (!Field.checked)
    {
		if (document.forms[0].elements["chkRequire"].checked = 1)
 		    document.forms[0].elements["Sel"].checked = 1;
		
		if (typeof(document.forms[0].elements["Sel"].length)=='undefined')
		     document.forms[0].elements["tcnChecked"].value=2;
		else
		     document.forms[0].elements["tcnChecked"][Field.value].value=2;
	}           
    else
    {
		with (document.forms[0])
		{
			if (typeof(elements["Sel"].length)=='undefined')
			    elements["tcnChecked"].value=(elements["Sel"].checked?1:2)
			else
			    for (lintIndex=0;lintIndex<=(elements["Sel"].length-1);lintIndex++)
			        elements["tcnChecked"][lintIndex].value = (elements["Sel"][lintIndex].checked?1:2)
		}
	}
}
//%insStateZone: Activa controles
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" ID="FORM" NAME="frmTabCommission" ACTION="valMantProduct.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMDP001()
'Else
	'Call insPreMDP001Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





