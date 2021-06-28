<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGeneral As eGeneral.GeneralFunction

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "MVI70"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
            Call .AddPossiblesColumn(0, GetLocalResourceObject("valFundsColumnCaption"), "valFunds", "TABFUND_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "ChgFund()", Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("valFundsColumnToolTip"))
            With mobjGrid.Columns("valFunds").Parameters
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .ReturnValue("nOrigin", False, , True)
            End With
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOriginColumnCaption"), "cbeOrigin", "Table5633", eFunctions.Values.eValuesType.clngComboType, , , , , , , True, , GetLocalResourceObject("cbeOriginColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5, vbNullString, , GetLocalResourceObject("tcnPercentColumnToolTip"), True, 2)

	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "MVI70"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 250
		.Width = 350
		.bCheckVisible = False
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("valFunds").EditRecord = True
            .sEditRecordParam = "nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&dEffecdate=" & Session("dEffecdate")
            .sDelRecordParam = .sEditRecordParam & "&nFunds='+ marrArray[lintIndex].valFunds + '" & "&nOrigin='+ marrArray[lintIndex].cbeOrigin + '"
	End With
End Sub

'% insPreMVI70: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
    Private Sub insPreMVI70()
        '--------------------------------------------------------------------------------------------
        Dim lclsClass As Object
        Dim mcolClass As ePolicy.Fund_distributions
	
        mcolClass = New ePolicy.Fund_distributions
	
        If mcolClass.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            For Each lclsClass In mcolClass
                With mobjGrid
                    .Columns("valFunds").DefValue = lclsClass.nFunds
                    .Columns("cbeOrigin").DefValue = lclsClass.nOrigin
                    .Columns("tcnPercent").DefValue = lclsClass.nPercent
                    Response.Write(.DoRow)
                End With
            Next lclsClass
        End If
	
        Response.Write(mobjGrid.closeTable())
        mcolClass = Nothing
    End Sub

'% insPreMVI70Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI70Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjClass As ePolicy.Fund_distribution
	
	lobjClass = New ePolicy.Fund_distribution
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
                Call lobjClass.InsPostmvi70_Upd("MVI70", .QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                            mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                            mobjValues.StringToType(Session("nTypeProfile"), eFunctions.Values.eTypeData.etdDouble),
                                                            mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                            mobjValues.StringToType(.QueryString.Item("nFunds"), eFunctions.Values.eTypeData.etdDouble), _
                                                            mobjValues.StringToType(.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble), _
                                                            0, _
                                                            mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantNoTraLife.aspx", "MVI70", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjClass = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("MVI70")

mobjValues = New eFunctions.Values
mobjGeneral = New eGeneral.GeneralFunction

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "MVI70"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI70", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 30/12/03 11:46 $|$$Author: Nvaplat26 $"

    //%ChgPercent(#): Asigna el valor de la tasa según al actividad/deporte dealto riesgo seleccionada
    //--------------------------------------------------------------------------------------------------
    function ChgFund() {
        //--------------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            cbeOrigin.value = valFunds_nOrigin.value;
        }
    }
</SCRIPT>        
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI70" ACTION="valMantNoTraLife.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("MVI70", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI70Upd()
Else
	Call insPreMVI70()
End If

mobjGrid = Nothing
mobjValues = Nothing
mobjGeneral = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("MVI70")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>