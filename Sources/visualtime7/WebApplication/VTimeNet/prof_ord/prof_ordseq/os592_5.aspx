<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página
Dim mcolAdjacence As eClaim.Adjacences


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim sAccion As String
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "os592_5"
	
	sAccion = Request.QueryString.Item("Action")
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbencardinalColumnCaption"), "cbencardinal", "Table5591", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbencardinalColumnCaption"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctdescriptColumnCaption"), "tctdescript", 30, "",  , GetLocalResourceObject("tctdescriptColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctmat_dividColumnCaption"), "tctmat_divid", 30, "",  , GetLocalResourceObject("tctmat_dividColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcndistantColumnCaption"), "tcndistant", 7, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcndistantColumnToolTip"),  , 2)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Columns("cbencardinal").BlankPosition = False
		.Codispl = "OS592_5"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbencardinal").EditRecord = True
		.DeleteButton = False
		.AddButton = False
		.Top = 200
		.Height = 250
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("Sel").Disabled = True
		.sDelRecordParam = "ncardinal='+ marrArray[lintIndex].cbencardinal + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreOS592_5: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreOS592_5()
	'--------------------------------------------------------------------------------------------
	Dim lclsAdjacence As Object
	mcolAdjacence = New eClaim.Adjacences
	
	If mcolAdjacence.Find(Session("nServ_order")) Then
		For	Each lclsAdjacence In mcolAdjacence
			With mobjGrid
				.Columns("cbencardinal").DefValue = lclsAdjacence.nCardinal
				.Columns("tctdescript").DefValue = lclsAdjacence.sDescript
				.Columns("tctmat_divid").DefValue = lclsAdjacence.sMat_divid
				
				If lclsAdjacence.nDistant = 0 Then
					.Columns("tcndistant").DefValue = ""
				Else
					.Columns("tcndistant").DefValue = lclsAdjacence.nDistant
				End If
				
				.Columns("Sel").OnClick = "insCheckSelClick(this," & lclsAdjacence.nCardinal & ");"
				If lclsAdjacence.nServ_Order = mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble) And lclsAdjacence.sDescript <> vbNullString Then
					.Columns("Sel").Checked = 1
					.Columns("Sel").Disabled = False
				Else
					.Columns("Sel").Checked = 2
					.Columns("Sel").Disabled = True
				End If
				Response.Write(.DoRow)
			End With
		Next lclsAdjacence
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreOS592_5Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'-------------------------------------------------------------------------------------------- 
Private Sub insPreOS592_5Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsAdjacence As eClaim.Adjacence
	lclsAdjacence = New eClaim.Adjacence
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsAdjacence.InsPostOS592_5(.QueryString.Item("Action"), mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("ncardinal"), eFunctions.Values.eTypeData.etdDouble), CStr(eRemoteDB.Constants.intNull), CStr(eRemoteDB.Constants.intNull), eRemoteDB.Constants.intNull) Then
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valProf_ordseq.aspx", "OS592_5", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

mobjValues.sCodisplPage = "os592_5"
%>
<HTML>
<HEAD>
	<SCRIPT>
	//+ Variable para el control de versiones
	        document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 18.00 $"
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,nCardinal){
//-------------------------------------------------------------------------------------------
//+ Se elimina registro
    if(!Field.checked){
		EditRecord(5,nMainAction,'Del','ncardinal=' + nCardinal)
    }
}	        
    </SCRIPT>	
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "OS592_5", "OS592_5.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="OS592_5" ACTION="valProf_ordseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%Response.Write(mobjValues.ShowWindowsName("OS592_5"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreOS592_5Upd()
Else
	If Request.QueryString.Item("Action") = "Del" Then
		Call insPreOS592_5Upd()
	Else
		Call insPreOS592_5()
	End If
End If
%>
</FORM> 
</BODY>
</HTML>





