<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid



'% insDefineHeader: Configura los datos del grid.
'%--------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'%--------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MVI012"
	
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnTypeinvestColumnCaption"), "tcnTypeinvest", "table5520", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , ,  ,  , GetLocalResourceObject("tcnTypeinvestColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 9, "",  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 6,,,,,,True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdFundDateColumnCaption"), "tcdFundDate",  ,  , GetLocalResourceObject("tcdFundDateColumnToolTip"),  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "MVI012"
		.sCodisplPage = "MVI012"
		.Columns("Sel").GridVisible = False
		.Height = 300
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnTypeinvest").EditRecord = True
		.Columns("tcnTypeinvest").disabled = True
		.AddButton = False
		.DeleteButton = False
		
		'+ Permite continuar si el check está marcado.
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreMVI012: Obtiene los datos de los fondos de inversión.
'%--------------------------------------------------------------------------------------
Private Sub insPreMVI012()
	'%--------------------------------------------------------------------------------------
	Dim mobjPlan_intwar_day As eBranches.Plan_intwar_day
	Dim mobjPlan_intwar_days As eBranches.Plan_intwar_days
	
	mobjPlan_intwar_day = New eBranches.Plan_intwar_day
	mobjPlan_intwar_days = New eBranches.Plan_intwar_days
	
	'+ Se buscan los fondos de inversión asociados al plan siempre y cuando la acción sea
	'+ diferente a una inserción.
	
	If mobjPlan_intwar_days.FindFounds(mobjValues.StringToDate(Session("dEffecdate"))) Then
		With mobjGrid
			For	Each mobjPlan_intwar_day In mobjPlan_intwar_days


				'+ Descripción del fondo.
				.Columns("tcnTypeinvest").DefValue = CStr(mobjPlan_intwar_day.nTypeinvest)
				'+ Valor.
				.Columns("tcnRate").DefValue = CStr(mobjPlan_intwar_day.nRate)
				'+ Fecha del Fondo               
				.Columns("tcdFundDate").DefValue = CStr(mobjPlan_intwar_day.dEffecDate)
				
				Response.Write(.DoRow)
			Next mobjPlan_intwar_day
		End With
	End If
	
	Response.Write(mobjGrid.closeTable)
	
	mobjPlan_intwar_day = Nothing
	mobjPlan_intwar_days = Nothing
End Sub

'% insPreMVI012Upd: Muestra la ventana Popup para las actualizaciones.
'%--------------------------------------------------------------------------------------
Private Function insPreMVI012Upd() As Object
	'%------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantNoTraLife.aspx", "MVI012", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Function

</script>
<%Response.Expires = -1

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA021")

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjGrid.ActionQuery = True
	mobjValues.ActionQuery = True
End If
mobjValues.sCodisplPage = "MVI012"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;

//+ Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//------------------------------------------------------------------------------

//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------
	return true
}
</SCRIPT>    
        <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "MVI012", "MVI012.aspx"))
	End If
End With

mobjMenu = Nothing%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantNoTraLife.aspx?mode=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
            <%=mobjValues.ShowWindowsName("MVI012")%>
            <BR>
            <%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMVI012()
Else
	Call insPreMVI012Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
        </FORM>
    </BODY>
</HTML>
<%
'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA021")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer

%>





