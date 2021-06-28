<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.16
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid

Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnNumdeclaColumnCaption"), "tcnNumdecla", 10, "",  , GetLocalResourceObject("tcnNumdeclaColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddDateColumn(0, GetLocalResourceObject("tcdDatedeclaColumnCaption"), "tcdDatedecla",  ,  , GetLocalResourceObject("tcdDatedeclaColumnToolTip"),  ,  ,  , Request.QueryString.Item("Action") = "Update")
		.AddDateColumn(0, GetLocalResourceObject("tcdNulldateColumnCaption"), "tcdNulldate",  ,  , GetLocalResourceObject("tcdNulldateColumnToolTip"),  ,  ,  , True)
		.AddCheckColumn(0, GetLocalResourceObject("chkIrrevocColumnCaption"), "chkIrrevoc", vbNullString,  ,  ,  ,  , GetLocalResourceObject("chkIrrevocColumnToolTip"))
		.AddHiddenColumn("hddIrrevoc_old", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "VI769"
		.Columns("tcnNumdecla").EditRecord = True
		.Height = 250
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sDelRecordParam = "nNumdecla='+ marrArray[lintIndex].tcnNumdecla + '" & "&dDatedecla='+ marrArray[lintIndex].tcdDatedecla + '"
		.Columns("Sel").GridVisible = Not Session("bQuery")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Sub

'% insPreVI769: Lee los valores de la tabla Decla_benef
'--------------------------------------------------------------------------------------------
Private Sub insPreVI769()
	'--------------------------------------------------------------------------------------------
	Dim mcolDecla_benef As ePolicy.Decla_benefs
	Dim lintIndex As Integer
	Dim lintCount As Integer
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim lintIsError As Byte
	Dim lstrMessage As String
	Dim lclsErrors As eFunctions.Errors
	Dim lstrMessage_tmp As String
	lclsErrors = New eFunctions.Errors
	lstrMessage_tmp = lclsErrors.ErrorMessage("VI769", 56029,  ,  ,  , True)
	If InStr(1, lstrMessage_tmp, "Err.") > 0 Then
		lintIsError = 1
	Else
		If InStr(1, lstrMessage_tmp, "Adv.") > 0 Then
			lintIsError = 0
		End If
	End If
	lclsGeneral = New eGeneral.GeneralFunction
	lstrMessage = lclsGeneral.insLoadMessage(56029)
	mcolDecla_benef = New ePolicy.Decla_benefs
	If mcolDecla_benef.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		lintCount = mcolDecla_benef.Count
		
		If lintCount > 0 Then
			For lintIndex = 1 To lintCount
				With mcolDecla_benef(lintIndex)
					mobjGrid.Columns("Sel").OnClick = "insDelDeclaBenef(this," & .sIrrevoc & ",""" & lstrMessage & """," & lintIsError & ",""" & mobjValues.TypeToString(.dNulldate, eFunctions.Values.eTypeData.etdDate) & """);"
					mobjGrid.Columns("tcnNumdecla").DefValue = CStr(.nNumdecla)
					mobjGrid.Columns("tcdDatedecla").DefValue = mobjValues.TypeToString(.dDatedecla, eFunctions.Values.eTypeData.etdDate)
					mobjGrid.Columns("tcdNulldate").DefValue = CStr(.dNulldate)
					mobjGrid.Columns("chkIrrevoc").Checked = CShort(.sIrrevoc)
					mobjGrid.Columns("hddIrrevoc_old").DefValue = .sIrrevoc
					If .sIrrevoc = "1" And ((Session("nTransaction") >= 12 And Session("nTransaction") <= 15) Or (Session("nTransaction") >= 24 And Session("nTransaction") <= 27)) Then
						mobjGrid.Columns("tcnNumdecla").EditRecord = False
						mobjGrid.EditRecordDisabled = True
					Else
						mobjGrid.Columns("tcnNumdecla").EditRecord = True
					End If
				End With
				mobjGrid.Columns("chkIrrevoc").Disabled = True
				Response.Write(mobjGrid.DoRow())
			Next 
		End If
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolDecla_benef = Nothing
	lclsGeneral = Nothing
End Sub

'% insPreVI769Upd: Se realiza el manejo de los campos del grid 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI769Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjDecla_benef As ePolicy.Decla_benef
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lobjDecla_benef = New ePolicy.Decla_benef
			If lobjDecla_benef.InsPostVI769("VI769", .QueryString.Item("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nNumdecla"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sIrrevoc"), mobjValues.StringToType(.QueryString.Item("dDatedecla"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
			End If
			lobjDecla_benef = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI769", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI769")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI769", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 15/10/03 17:19 $|$$Author: Nvaplat61 $"

//% insDelDeclaBenef: verifica si la transaccion corresponde a una modificación para enviar mensaje
//					  de validación al tratar de eliminar.
//-------------------------------------------------------------------------------------------
function insDelDeclaBenef(Field,chkIrrevoc,Message,IsError,dNulldate){
//-------------------------------------------------------------------------------------------
	if(dNulldate!=''){
//+ Si el registro se encuentra anulado, no se puede eliminar
		alert("Err.: <%=eFunctions.Values.GetMessage(60586)%>");
		Field.checked = false;
		marrArray[Field.value].Sel = Field.checked;
	}
	else
		if (chkIrrevoc == "1" &&
		    Field.checked)
			if (IsError == 1){
				alert('Err. 56029: ' + Message);
				Field.checked = false;
			}
			else
				alert('Adv. 56029: ' + Message);
}    
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmVI769" ACTION="valPolicySeq.aspx?sMode=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("VI769", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreVI769Upd()
Else
	Call insPreVI769()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.16
Call mobjNetFrameWork.FinishPage("VI769")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




