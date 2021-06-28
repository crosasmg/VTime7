<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

Dim mintIndex As Short


'- Objeto para el manejo particular de los datos de la página
Dim mclsMasiveCharge As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "cal660"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctFieldColumnCaption"), "tctField", 30, vbNullString,  , GetLocalResourceObject("tctFieldColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("tctValueColumnCaption"), "tctValue", 60, vbNullString,  , GetLocalResourceObject("tctValueColumnToolTip"),  ,  ,  , True)
		Call .AddTextColumn(0, GetLocalResourceObject("cbeValue1ColumnCaption"), "cbeValue1", 20, "Tabla permitidad")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeValueColumnCaption"), "cbeValue", "table12", 2, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeValueColumnCaption"), eFunctions.Values.eTypeCode.eString)
            Call .AddHiddenColumn("hddsField", vbNullString)
            Call .AddHiddenColumn("hddsTable", vbNullString)
            Call .AddHiddenColumn("hddTableList", vbNullString)
        End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.AddButton = False
		.DeleteButton = False
		.Codispl = "CAL660"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("tctField").EditRecord = True
		.Columns("Sel").GridVisible = False
		.Columns("cbeValue").GridVisible = False
		.Columns("cbeValue1").Disabled = True
		
		.Height = 300
		.Width = 500
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.bCheckVisible = False
		.MoveRecordScript = "ChangeValue()"
	End With
End Sub

'% insPreCAL660: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCAL660()
	'--------------------------------------------------------------------------------------------
	Dim mcolMasiveCharges As eBatch.MasiveCharges
	mcolMasiveCharges = New eBatch.MasiveCharges
	If mcolMasiveCharges.FindInconsist(Session("sKey"), Session("nUsercode"), Session("nWorksheet")) Then
		
		mintIndex = 0
		If mcolMasiveCharges.nError = 1 Then
			mobjGrid.ActionQuery = True
		End If
		If mcolMasiveCharges.Count > 0 Then
			
			'+Como al procesar cada inconsistencia esta desaparece del grid
			'+sólo se usa el índice de carga si aun quedan registros
			If Request.QueryString.Item("Reload") = "1" Then
				mobjGrid.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
			
			For	Each mclsMasiveCharge In mcolMasiveCharges
				With mobjGrid
					.Columns("tctField").DefValue = mclsMasiveCharge.sFieldName
.Columns("tctField").EditRecord = True
					.Columns("tctValue").DefValue = mclsMasiveCharge.sValue
					
					.Columns("cbeValue1").DefValue = mclsMasiveCharge.sValuesList
					.Columns("cbeValue").DefValue = ""
					
					.Columns("hddsField").DefValue = mclsMasiveCharge.sField
					.Columns("hddsTable").DefValue = mclsMasiveCharge.sTable
					.Columns("hddTableList").DefValue = mclsMasiveCharge.sValuesList
					
					.sEditRecordParam = "sTable=" & mclsMasiveCharge.sValuesList
					
					Response.Write(.DoRow)
					mintIndex = mintIndex + 1
				End With
			Next mclsMasiveCharge
			Session("nContent") = 2
		Else
			Session("nContent") = 1
		End If
	Else
		Session("nContent") = 1
	End If
	If CStr(Session("nContent")) = "1" Then
		Response.Write("<SCRIPT>top.frames[""fraHeader""].nContent= 1; </" & "Script>")
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	mcolMasiveCharges = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal660")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cal660"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

function ChangeValue(){
	self.document.forms[0].cbeValue.sTabName=self.document.forms[0].cbeValue1.value;
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CAL660", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CAL660frm" ACTION="ValPolicyRepSeq.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("CAL660", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicyRepSeq.aspx", "CAL660", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	Response.Write("<SCRIPT>ChangeValue();</SCRIPT>")
Else
	Call insPreCAL660()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("cal660")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




