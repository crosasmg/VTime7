<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "SOC001"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctIntermediaColumnCaption"), "tctIntermedia", 63, "",  , GetLocalResourceObject("tctIntermediaColumnToolTip"))
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnStartColumnCaption"), "tcnStart", 10, "", , GetLocalResourceObject("tcnStartColumnToolTip"))
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndColumnCaption"), "tcnEnd", 10, "", , GetLocalResourceObject("tcnEndColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctPolitypeColumnCaption"), "tctPolitype", 30, "",  , GetLocalResourceObject("tctPolitypeColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "",  , GetLocalResourceObject("tctBranchColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "",  , GetLocalResourceObject("tctProductColumnToolTip"))
        Call .AddNumericColumn(0, GetLocalResourceObject("tcnSoldColumnCaption"), "tcnSold", 10, "", , GetLocalResourceObject("tcnSoldColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "SOC001"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreSOC001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSOC001()
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lobjGen As ePolicy.Folios_Agents
	Dim lobjObject As Object
	
	lobjGen = New ePolicy.Folios_Agents
	
	If lobjGen.FindYear(mobjValues.StringToType(Request.QueryString("nYear"), eFunctions.Values.eTypeData.etdInteger))
	
    	lintCount = 0
	
	    For	Each lobjObject In lobjGen
		    With lobjObject
			    mobjGrid.Columns("tctIntermedia").DefValue = .sIntermedia
			    mobjGrid.Columns("tcnStart").DefValue = .nStart
			    mobjGrid.Columns("tcnEnd").DefValue = .nEnd
			    mobjGrid.Columns("tctPolitype").DefValue = .sPolitype
			    mobjGrid.Columns("tctBranch").DefValue = .sDesBranch
			    mobjGrid.Columns("tctProduct").DefValue = .sDesProd
				mobjGrid.Columns("tcnSold").DefValue = .nSold
			    Response.Write(mobjGrid.DoRow())
		    End With
		
		    lintCount = lintCount + 1
		
		    If lintCount = 200 Then
			    Exit For
		    End If
	    Next lobjObject
	
	End If

    Response.Write(mobjGrid.closeTable())
	
	lobjGen = Nothing
	lobjObject = Nothing

End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SOC001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "SOC001"
%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "SOC001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SOC001" ACTION="ValPolicyQue.aspx?Zone=2">
<%
Call insDefineHeader()
Call insPreSOC001()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("SOC001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




