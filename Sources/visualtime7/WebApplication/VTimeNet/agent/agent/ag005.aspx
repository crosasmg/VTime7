<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.55
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mlngAction As Object
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid 
'-----------------------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "ag005"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid.Columns
		.AddBranchColumn(40010, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct", CStr(0),  ,  , "insOnChangeBranch(this)", Request.QueryString.Item("Action") = "Update")
		.AddProductColumn(40011, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"), "cbeBranch", Request.Form.Item("valProduct"))
		.AddNumericColumn(40012, GetLocalResourceObject("tcnGoalColumnCaption"), "tcnGoal", 18, CStr(0),  , GetLocalResourceObject("tcnGoalColumnToolTip"), True, 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 9, CStr(0),  , GetLocalResourceObject("tcnPercentColumnToolTip"), True, 6)
	End With
	With mobjGrid
		.Codispl = "AG005"
		.Width = 300
		.Height = 250
		.AddButton = mlngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.DeleteButton = mlngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.Columns("Sel").GridVisible = mlngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.Columns("cbeBranch").EditRecord = mlngAction <> eFunctions.Menues.TypeActions.clngActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
	End With
End Sub

'%insPreAG005: Esta función se encarga de cargar los datos en la forma "Folder" 
'----------------------------------------------------------------------------------------------
Private Sub insPreAG005()
	'----------------------------------------------------------------------------------------------
	Dim lintTable As Integer
	Dim lintCurrency As Integer
	Dim lintYear As Integer
	Dim lstrType_infor As String
	Dim lstrPeriodtyp As String
	Dim lintPeriodnum As Integer
	Dim ldtmEffecdate As Date
	Dim lintCount As Integer
	Dim lobjGoalss As eAgent.Goalss
	Dim lobjGoals As Object
	
	lobjGoalss = New eAgent.Goalss
	
	lintTable = mobjValues.StringToType(Session("nCode"), eFunctions.Values.eTypeData.etdDouble)
	lintYear = mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble)
	lintCurrency = mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	lstrType_infor = Session("sType_infor")
	lstrPeriodtyp = Session("sPeriodtyp")
	lintPeriodnum = mobjValues.StringToType(Session("nPeriodnum"), eFunctions.Values.eTypeData.etdDouble)
	ldtmEffecdate = mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
	Call lobjGoalss.Find(lintTable, ldtmEffecdate, lintYear, lintPeriodnum, lstrType_infor, lstrPeriodtyp, lintCurrency)
	
	mobjGrid.sDelRecordParam = "nBranch=' + marrArray[lintIndex].cbeBranch + '&nProduct=' + marrArray[lintIndex].valProduct + '&nBud=' + marrArray[lintIndex].tcnBud + '"
	For lintCount = 1 To lobjGoalss.Count
		With mobjGrid
			.Columns("cbeBranch").DefValue = CStr(lobjGoalss.Item(lintCount).nBranch)
			.Columns("valProduct").DefValue = CStr(lobjGoalss.Item(lintCount).nProduct)
			.Columns("tcnGoal").DefValue = CStr(lobjGoalss.Item(lintCount).nGoal)
			.Columns("tcnPercent").DefValue = CStr(lobjGoalss.Item(lintCount).nPercent)
		End With
		Response.Write(mobjGrid.DoRow())
	Next 
	
	Response.Write(mobjGrid.CloseTable())
	
	If Request.QueryString.Item("Reload") = "1" Then
		'+ Se recarga la ventana PopUp, en caso que el check de "Continuar" se encuentre marcado
		Select Case Request.QueryString.Item("ReloadAction")
			Case "Add"
				Response.Write("<SCRIPT>EditRecord(-1,nMainAction,'Add')</" & "Script>")
			Case "Update"
				Response.Write("<SCRIPT>EditRecord(" & Request.QueryString.Item("ReloadIndex") & ",nMainAction,'Update')</" & "Script>")
		End Select
	End If
	
	mobjGrid = Nothing
	lobjGoalss = Nothing
End Sub

'% insPreAG005Upd: Se define esta función para contruir el contenido de la ventana "UPD"
'----------------------------------------------------------------------------------------------
Private Sub insPreAG005Upd()
	'----------------------------------------------------------------------------------------------
	Dim lobjGoals As eAgent.Goals
	
	If Request.QueryString.Item("Action") = "Del" Then
		lobjGoals = New eAgent.Goals
		Call lobjGoals.InsPostAG005Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(Session("nCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPeriodnum"), eFunctions.Values.eTypeData.etdDouble), Session("sType_infor"), Session("sPeriodtyp"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnGoal"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		lobjGoals = Nothing
		With Response
			.Write("<SCRIPT>top.opener.DeleteRecord(" & Request.QueryString.Item("Index") & ")</" & "Script>")
			.Write(mobjValues.ConfirmDelete)
		End With
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValAgent.aspx", "AG005", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ag005")

mlngAction = Request.QueryString.Item("nMainAction")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ag005"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "AG005", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
%>
<HTML>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 13.15 $"        
    
// insOnChangeBranch: Esta función se encarga de pasar el parametro BRANCH a los valores 
// posibles que lo requieran y habilitar los campos que dependan del ramo.
//-------------------------------------------------------------------------------------------------------------------
function insOnChangeBranch(lcolumn){
//-------------------------------------------------------------------------------------------------------------------
    with (self.document.forms[0]){		
        if(lcolumn.value!='' && lcolumn.value!=0){           
            if(sAction!="Update"){
                valProduct.disabled = false
	            btnvalProduct.disabled = false        
	        }    
	    }    
    }
}    
</SCRIPT>        





	<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmIntermBud" ACTION="valAgent.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

<%="<script>var sAction='" & Request.QueryString.Item("Action") & "'</script>"%>
<%
Response.Write(mobjValues.ShowWindowsName("AG005", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreAG005Upd()
Else
	Call insPreAG005()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.55
Call mobjNetFrameWork.FinishPage("ag005")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




