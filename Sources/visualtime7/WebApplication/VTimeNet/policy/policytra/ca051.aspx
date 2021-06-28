<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" ValidateRequest="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues
Dim mcolColSheets As eBatch.Colsheets
Dim mclsColSheet As Object



'+ insDefineHeader: Definición del Grid
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "ca051"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	With mobjGrid
		With .Columns
			If Request.QueryString.Item("Action") <> "Update" Then
				Call .AddCheckColumn(0, GetLocalResourceObject("chkAuxSelColumnCaption"), "chkAuxSel", vbNullString, False)
			End If
			Call .AddPossiblesColumn(101263, GetLocalResourceObject("tcnSheetColumnCaption"), "tcnSheet", "Table697", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("tcnSheetColumnToolTip"))
			Call .AddTextColumn(101265, GetLocalResourceObject("tctColumnNameColumnCaption"), "tctColumnName", 30, vbNullString,  , GetLocalResourceObject("tctColumnNameColumnToolTip"))
			Call .AddCheckColumn(0, GetLocalResourceObject("chkSelectedColumnCaption"), "chkSelected", vbNullString, False,  , "insCheckCriterio(this, CurrentIndex)", True, GetLocalResourceObject("chkSelectedColumnToolTip"))
			Call .AddNumericColumn(101264, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 5, CStr(0),  , GetLocalResourceObject("tcnOrderColumnToolTip"))
			Call .AddCheckColumn(101266, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString,  ,  , "insCheckValue(this, CurrentIndex)", True, GetLocalResourceObject("chkRequireColumnToolTip"))
			Call .AddTextColumn(101267, GetLocalResourceObject("tctDefaultValueColumnCaption"), "tctDefaultValue", 50, vbNullString, , GetLocalResourceObject("tctDefaultValueColumnToolTip"))
			Call .AddHiddenColumn("hddsField", vbNullString)
			Call .AddHiddenColumn("hddsReqtable", vbNullString)
			Call .AddHiddenColumn("hddsExist", vbNullString)
			Call .AddHiddenColumn("hddnSheeth", vbNullString)
			Call .AddHiddenColumn("hddsColumnNameh", vbNullString)
			Call .AddHiddenColumn("hddnOrderh", vbNullString)
			Call .AddHiddenColumn("hddsRequireh", vbNullString)
			Call .AddHiddenColumn("hddsAuxSelh", vbNullString)
			Call .AddHiddenColumn("hddnId", vbNullString)
			Call .AddHiddenColumn("hddnIdRec", vbNullString)
			Call .AddHiddenColumn("hddsSelectedh", vbNullString)
			Call .AddHiddenColumn("hddsCritery", vbNullString)
			Call .AddHiddenColumn("hddDisable", vbNullString)
            Call .AddHiddenColumn("hddsDefaultValueh", vbNullString)
		End With
		
		.Codispl = "CA051"
		.Width = 480
		.Height = 300
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		.Columns("tcnSheet").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
		
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nSheet='+ marrArray[lintIndex].tcnSheet + '" & "&sColumnName='+marrArray[lintIndex].tctColumnName + '" & "&sField='+marrArray[lintIndex].hddsField + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%inspreCA051upd: Se Actualiza el registro seleccionado en el Grid
'-------------------------------------------------------------------------------------------
Private Sub inspreCA051upd()
	'-------------------------------------------------------------------------------------------
	Dim lclsColSheet As eBatch.Colsheet
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			If mclsColSheet.Delete(Session("nBranch"), Session("nProduct"), Session("nPolicy"), .QueryString("nSheet"), .QueryString("sColumnName"), .QueryString("sField")) Then
				
				Response.Write(mobjValues.ConfirmDelete())
			End If
		Else
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicyTra.aspx", "CA051", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	If Request.QueryString.Item("Action") = "Update" Then
		lclsColSheet = New eBatch.Colsheet
		If lclsColSheet.FindTable5571(Request.QueryString.Item("sField")) Then
			Response.Write("<SCRIPT>self.document.forms[0].chkSelected.disabled=false;</" & "Script>")
		End If
		lclsColSheet = Nothing
		Response.Write("<SCRIPT>if (self.document.forms[0].hddDisable.value=='1') self.document.forms[0].chkRequire.disabled=true; else  self.document.forms[0].chkRequire.disabled=false; </" & "Script>")
	End If
End Sub

'%inspreCA051: Se cargan los Valores en el Grid
'-------------------------------------------------------------------------------------------
Private Sub inspreCA051()
	'-------------------------------------------------------------------------------------------
	Dim lintIndex As Object
	Dim lstrvalue As String
	
	mcolColSheets = New eBatch.Colsheets
	If mcolColSheets.FindCA051(mobjValues.StringToType(Session("nId"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintIndex = 0
		lstrvalue = "1"
		With mobjGrid
			For	Each mclsColSheet In mcolColSheets
				If mclsColSheet.sField = "DBIRTHDAT" And mclsColSheet.sSel = "1" Then
					lstrvalue = "2"
				End If
				.Columns("chkAuxSel").Checked = mclsColSheet.sSel
				.Columns("chkAuxSel").DefValue = mclsColSheet.sSel
				.Columns("chkAuxSel").OnClick = "insSelected(this, " & lintIndex & ")"
				.Columns("tcnSheet").DefValue = mclsColSheet.nSheet
				.Columns("tctColumnName").DefValue = mclsColSheet.sColumnName
				.Columns("tcnOrder").DefValue = mclsColSheet.nOrder
                .Columns("tctDefaultValue").DefValue = mclsColSheet.sDefaultValue
				If mclsColSheet.sGroupRequire = "2" Then
					.Columns("hddDisable").DefValue = "2"
				Else
					If mclsColSheet.sRequire = "1" Then
						.Columns("hddDisable").DefValue = "1"
					Else
						.Columns("hddDisable").DefValue = "2"
					End If
				End If
				.Columns("chkRequire").Checked = mclsColSheet.sRequire
				.Columns("chkRequire").DefValue = mclsColSheet.sRequire
				.Columns("hddsField").DefValue = mclsColSheet.sField
				.Columns("hddsReqtable").DefValue = mclsColSheet.sGroupRequire
				.Columns("hddsExist").DefValue = mclsColSheet.sExists
				.Columns("hddnSheeth").DefValue = mclsColSheet.nSheet
				.Columns("hddsColumnNameh").DefValue = mclsColSheet.sColumnName
				.Columns("hddnOrderh").DefValue = mclsColSheet.nOrder
				.Columns("hddsRequireh").DefValue = mclsColSheet.sRequire
				.Columns("chkSelected").Checked = mclsColSheet.sSelected
				.Columns("chkSelected").DefValue = mclsColSheet.sSelected
				.Columns("hddsSelectedh").DefValue = mclsColSheet.sSelected
                .Columns("hddsDefaultValueh").DefValue = mclsColSheet.sDefaultValue
				If mclsColSheet.sSel = "1" Then
					.Columns("hddsAuxSelh").DefValue = "1"
				Else
					.Columns("hddsAuxSelh").DefValue = "2"
				End If
				.sEditRecordParam = "sField=" & mclsColSheet.sField
				.Columns("hddnId").DefValue = mclsColSheet.nId
				.Columns("hddnIdRec").DefValue = mclsColSheet.nIdRec
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			Next mclsColSheet
		End With
	End If
	
	Response.Write(mobjValues.HiddenControl("hddsFieldValidate", lstrvalue))
	Response.Write(mobjValues.HiddenControl("hddnCount", lintIndex))
	
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca051")
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
			
<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"  

//%insSelected: realiza el manejo para la edición de un registro particular del grid 
//%para eliminarlo, agregarlo o modificarlo
//------------------------------------------------------------------------------------------
function insSelected(Field, nIndex){
//------------------------------------------------------------------------------------------
	if (Field.checked)
	    self.document.forms[0].hddsAuxSelh[nIndex].value = "1"
	else
	    self.document.forms[0].hddsAuxSelh[nIndex].value = "2"

	if (self.document.forms[0].hddsField[nIndex].value == "DBIRTHDAT") {
	    if (Field.checked)
			self.document.forms[0].hddsFieldValidate.value = "2" ;
		else
			self.document.forms[0].hddsFieldValidate.value = "1" ;
	}
}

//insCheckCriterio: Chequea el Criterio y asigna el valor del mismo
//------------------------------------------------------------------------------------------
function insCheckCriterio(Field, nIndex)
//------------------------------------------------------------------------------------------
{
	self.document.forms[0].hddsCritery.value = top.opener.document.forms[0].hddsFieldValidate.value
}

//insCheckValue: coloca la columna con check
//------------------------------------------------------------------------------------------
function insCheckValue(Field, nIndex)
//------------------------------------------------------------------------------------------
{
	if (document.forms[0].hddsReqtable.value=="1")
		document.forms[0].hddsRequire.checked = true;
}
//---------------------------------------------------------------------------------------------
function insShowHeader(){
//---------------------------------------------------------------------------------------------
    var lblnContinue=true
    if (typeof(top.fraHeader.document)!='undefined') {
	    if (typeof(top.fraHeader.document.forms[0])!='undefined') {
			if (typeof(top.fraHeader.document.forms[0].tcnWorksheet)!='undefined'){
				top.fraHeader.document.forms[0].tcnWorksheet.value= '<%=Session("nId")%>'
				lblnContinue = false
			}
		}
	}
    if (lblnContinue)
		setTimeout("insShowHeader()",50);
}
</SCRIPT>

	<%
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca051"

Response.Write(mobjValues.StyleSheet())

mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenues.sSessionID = Session.SessionID
mobjMenues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
	    Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	    
	    If Request.QueryString.Item("Type") <> "PopUp" Then
	        Response.Write(mobjMenues.setZone(2, "CA051", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	    End If
	    
	    If CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401 Then
	        mobjValues.ActionQuery = True
	    End If

mobjMenues = Nothing
%>

</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="CA051" ACTION="valPolicyTra.aspx?sCodispl=CA051&sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>setTimeout('insShowHeader()',50);</SCRIPT>")
	inspreCA051()
Else
	inspreCA051upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mcolColSheets = Nothing
mclsColSheet = Nothing
%>
	</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca051")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




