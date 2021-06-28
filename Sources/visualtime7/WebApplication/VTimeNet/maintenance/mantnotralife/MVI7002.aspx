<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objetos genéricos para manejo de valores, menú y grilla.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader: Definición de columnas del Grid
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOriginColumnCaption"), "cbeOrigin", "Table5633", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOriginColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 4, vbNullString, False, GetLocalResourceObject("tcnOrderColumnToolTip"))
        End With
	
	With mobjGrid
		.Columns("cbeOrigin").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MVI7002"
		.sCodisplPage = "MVI7002"
		.Columns("cbeOrigin").EditRecord = True
		.AddButton = True
		.DeleteButton = True
		.Height = 160
		.Width = 350
		.sDelRecordParam = "nOrigin=' + marrArray[lintIndex].cbeOrigin +  '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI7002: Muestra la grilla con datos.
'--------------------------------------------------------------------------------------------------------------------
Private Sub insPreMVI7002()
	'--------------------------------------------------------------------------------------------------------------------
	
Response.Write("")

	
	Dim lintCount As Short
	Dim lobjObject As Object
	Dim lcolTab_Ord_Origins As eBranches.Tab_Ord_Origins
	
	lcolTab_Ord_Origins = New eBranches.Tab_Ord_Origins
	
	If lcolTab_Ord_Origins.Find(Session("nBranch"), Session("nProduct")) Then
		
		lintCount = 0
		For	Each lobjObject In lcolTab_Ord_Origins
			With lobjObject
				mobjGrid.Columns("cbeOrigin").DefValue = .nOrigin
				mobjGrid.Columns("tcnOrder").DefValue = .nOrder
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 1000 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolTab_Ord_Origins = Nothing
	lobjObject = Nothing
End Sub

'% insPreMVI7002Upd: Muestra ventana para actualizar registros.
'-----------------------------------------------------------------------------------------
Private Sub insPreMVI7002Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsTab_Ord_Origin As eBranches.Tab_Ord_Origin
	If Request.QueryString.Item("Action") = "Del" Then
		lclsTab_Ord_Origin = New eBranches.Tab_Ord_Origin
		
            If lclsTab_Ord_Origin.InsPostMVI7002Upd("Del", Request.QueryString.Item("nOrigin"), eRemoteDB.Constants.intNull, Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), vbNullString, eRemoteDB.Constants.intNull, vbNullString, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull) Then
			
                Response.Write(mobjValues.ConfirmDelete())
                Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), , CShort(Request.QueryString.Item("Index"))))
            End If
		
		lclsTab_Ord_Origin = Nothing
	Else
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	End If
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MVI7002"
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>





<%
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MVI7002", "MVI7002"))
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.43 $|$$Author: Nvaplat61 $"

//% insCancel: Eejcuta acción deL botón cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone: Activa controles.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<form method="post" ID="FORM" NAME="MVI7002" ACTION="valMantNoTraLife.aspx?mode=1">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreMVI7002()
Else
	Call insPreMVI7002Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





