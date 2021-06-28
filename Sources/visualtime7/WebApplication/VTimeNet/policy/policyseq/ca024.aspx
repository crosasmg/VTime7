<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
Dim mintCommission As String
Dim mblnCurrentComm As Object
Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") = "PopUp" Then
			If Request.QueryString.Item("Action") = "Update" Then
				If CStr(Session("nInsur_area")) = "1" Then
					Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermedColumnCaption"), "valIntermed", "tabIntermedia_ca024b", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "ShowChangeValues(""" & Request.QueryString.Item("nMainAction") & """,""" & Request.QueryString.Item("nCommityp") & """)", Request.QueryString.Item("Action") = "Update", 10, GetLocalResourceObject("valIntermedColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
				Else
					Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermedColumnCaption"), "valIntermed", "tabIntermedia_ca024", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "ShowChangeValues(""" & Request.QueryString.Item("nMainAction") & """,""" & Request.QueryString.Item("nCommityp") & """)", Request.QueryString.Item("Action") = "Update", 10, GetLocalResourceObject("valIntermedColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
				End If
			Else
				Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermedColumnCaption"), "valIntermed", "tabIntermedia_ca024", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "ShowChangeValues(""" & Request.QueryString.Item("nMainAction") & """,""" & Request.QueryString.Item("nCommityp") & """)", Request.QueryString.Item("Action") = "Update", 10, GetLocalResourceObject("valIntermedColumnToolTip"), eFunctions.Values.eTypeCode.eNumeric)
			End If
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("valIntermedColumnCaption"), "valIntermed", 7, vbNullString,  , GetLocalResourceObject("valIntermedColumnToolTip"))
			If CStr(Session("nInsur_area")) = "1" Then
				Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermedNameColumnCaption"), "valIntermedName", "tabIntermedia_ca024b", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIntermedNameColumnToolTip"))
			Else
				Call .AddPossiblesColumn(0, GetLocalResourceObject("valIntermedNameColumnCaption"), "valIntermedName", "tabIntermedia_ca024", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIntermedNameColumnToolTip"))
			End If
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeRoleColumnCaption"), "cbeRole", "Interm_typ", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeRoleColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAgreementColumnCaption"), "cbeAgreement", "tabAgreement", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAgreementColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInstallComColumnCaption"), "tcnInstallCom", 5,  ,  , GetLocalResourceObject("tcnInstallComColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAgencyColumnCaption"), "cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeAgencyColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnShareColumnCaption"), "tcnShare", 5, "",  , GetLocalResourceObject("tcnShareColumnToolTip"),  , 2)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table47", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True, 6, GetLocalResourceObject("cbeTypeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 5,  ,  , GetLocalResourceObject("tcnPercentColumnToolTip"),  , 2,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, "",  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPercent_CeColumnCaption"), "tcnPercent_Ce", 5,  ,  , GetLocalResourceObject("tcnPercent_CeColumnToolTip"),  , 2)
		Call .AddHiddenColumn("hddtcnPercent", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA024"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("valIntermed").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermed").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermed").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermed").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermed").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If Request.QueryString.Item("Action") = "Update" Then
				Session("Action") = "Update"
				.Columns("valIntermed").Parameters.Add("nTransactio", Session("nTransaction"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Else
				Session("Action") = "Add"
				.Columns("valIntermed").Parameters.Add("nTransactio", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End If
		Else
			.Columns("valIntermed").EditRecord = True
			.Columns("valIntermedName").EditRecord = True
			.Columns("valIntermedName").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermedName").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermedName").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermedName").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermedName").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Columns("valIntermedName").Parameters.Add("nTransactio", 12, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End If
		.sDelRecordParam = "nIntermed='+ marrArray[lintIndex].valIntermed + '" & "&nRole='+ marrArray[lintIndex].cbeRole + '"
		.Top = 50
		.Height = 420
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.UpdContent = True
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("6ColumnCaption"), 6)
		Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("5ColumnCaption"), 5)
	End With
End Sub

'% insPreCA024: se realiza el manejo del grid
'-------------------------------------------------------------------------------------------- 
Private Sub insPreCA024()
	Dim QueryString() As String
	'-------------------------------------------------------------------------------------------- 
	Dim lclsCommission As Object
	Dim lcolCommission As ePolicy.Commissions
	Dim lclsPolicy As ePolicy.Policy
	Dim lstrInd_Comm As String
	Dim lstrConColl As String
	Dim lstrCommityp As String
	
	lclsPolicy = New ePolicy.Policy
	
	With lclsPolicy
		If .Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
			lcolCommission = New ePolicy.Commissions
			lstrInd_Comm = .sInd_Comm
			lstrConColl = .sConColl
			lstrCommityp = .sCommityp
			If Request.QueryString.Item("sInd_comm") <> vbNullString Then
				lstrInd_Comm = Request.QueryString.Item("sInd_comm")
			End If
			If Request.QueryString.Item("nCommityp") <> vbNullString Then
				lstrCommityp = Request.QueryString.Item("nCommityp")
			End If
			If .sCommityp = vbNullString Then
				.sCommityp = "1"
			End If
			If Request.QueryString.Item("sConcoll") <> vbNullString Then
				lstrConColl = QueryString(CInt("sConcoll"))
			End If
			Response.Write(mobjValues.HiddenControl("hddInd_Comm", lstrInd_Comm))
			Response.Write(mobjValues.HiddenControl("hddConColl", lstrConColl))
			
Response.Write("" & vbCrLf)
Response.Write("		<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD COLSPAN=""2"" CLASS=""HighLighted"" WIDTH=40%><LABEL ID=40950>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		        <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("		        <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optInd_Comm", GetLocalResourceObject("optInd_Comm_1Caption"), lstrInd_Comm, "1", "ChangeValues(""Ind_Comm"", this);", .sPolitype <> "2",  , GetLocalResourceObject("optInd_Comm_1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		        <TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL>" & GetLocalResourceObject("cbeTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")

			
			mobjValues.BlankPosition = False
			Response.Write(mobjValues.PossiblesValues("cbeType", "Table47", eFunctions.Values.eValuesType.clngComboType, .sCommityp,  ,  ,  ,  ,  , "ReloadPage()"))
			
Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optInd_Comm", GetLocalResourceObject("optInd_Comm_2Caption"), CStr(CShort(lstrInd_Comm) - 1), "2", "ChangeValues(""Ind_Comm"", this);", .sPolitype <> "2",  , GetLocalResourceObject("optInd_Comm_2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD><LABEL>" & GetLocalResourceObject("tcnPercentCFCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				<TD>")

			
			If Request.QueryString.Item("nCommityp") <> vbNullString Then
				If Request.QueryString.Item("nCommityp") = "2" Then
					Response.Write(mobjValues.NumericControl("tcnPercentCF", 4, Request.QueryString.Item("nPercent"),  , GetLocalResourceObject("tcnPercentCFToolTip"), True, 2,  ,  ,  , "ReloadPage()", False))
				Else
					Response.Write(mobjValues.NumericControl("tcnPercentCF", 4, CStr(0),  , GetLocalResourceObject("tcnPercentCFToolTip"), True, 2,  ,  ,  , "ReloadPage()", True))
				End If
			Else
				Response.Write(mobjValues.NumericControl("tcnPercentCF", 4, CStr(.nCommissi),  , GetLocalResourceObject("tcnPercentCFToolTip"), True, 2,  ,  ,  , "ReloadPage()", .sCommityp <> "2"))
			End If
			
Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("				<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("				<TD COLSPAN=""2"">")


Response.Write(mobjValues.CheckControl("chkConColl", GetLocalResourceObject("chkConCollCaption"), lstrConColl, "1", "ChangeValues(""ConColl"", this);", .sPolitype <> "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("	    </TABLE>" & vbCrLf)
Response.Write("	    <BR>")

			
			If Request.QueryString.Item("nCommityp") = vbNullString Then
				mintCommission = .sCommityp
				Response.Write("<SCRIPT>var mblnCurrentComm=" & .sCommityp & "</" & "Script>")
			Else
				mintCommission = Request.QueryString.Item("nCommityp")
				Response.Write("<SCRIPT>")
				'+ Se asigna valor al campo "Tipo de comisión"
				Response.Write("var mblnCurrentComm=" & Request.QueryString.Item("nCommityp") & ";")
				Response.Write("self.document.forms[0].cbeType.value=" & Request.QueryString.Item("nCommityp") & ";")
				Response.Write("</" & "Script>")
			End If
			
			mobjGrid.sEditRecordParam = "nCommityp=' + " & mintCommission & " + '" & "&nPercent=' + self.document.forms[0].tcnPercentCF.value + '" & "&sInd_Comm=' +  self.document.forms[0].hddInd_Comm.value  + '" & "&sConColl=' +  self.document.forms[0].hddConColl.value  + '"
			
			'+ Si se cambió el tipo de comisión o el % general de la póliza, se actualizan los datos
			'+ de los intermediarios asociados
			'		    If Request.QueryString("sChangeCom") = "1" Then
			'				Set lclsCommission = Server.CreateObject("ePolicy.Commission") 
			'				Call lclsCommission.UpdatePercent(Session("sCertype"), '												  Session("nBranch"), '												  Session("nProduct"), '												  Session("nPolicy"), '												  Session("nCertif"), '												  Session("dEffecdate"), '												  mintCommission, '												  mobjValues.StringToType(Request.QueryString("nPercent"),eFunctions.Values.eTypeData.etdDouble), '												  Session("nUsercode"))
			'		    End If
			If lcolCommission.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"), Session("nCertif")) Then
				For	Each lclsCommission In lcolCommission
					With mobjGrid
						.Columns("valIntermed").DefValue = lclsCommission.nIntermed
						.Columns("valIntermedName").DefValue = lclsCommission.nIntermed
						.Columns("cbeRole").DefValue = lclsCommission.nIntertyp
						.Columns("cbeAgreement").DefValue = lclsCommission.nAgreement
						.Columns("tcnInstallCom").DefValue = lclsCommission.nInstallCom
						.Columns("cbeAgency").DefValue = lclsCommission.nAgency
						.Columns("tcnShare").DefValue = lclsCommission.nShare
						.Columns("cbeType").DefValue = lclsCommission.sCommityp
						.Columns("tcnPercent").DefValue = lclsCommission.nPercent
						.Columns("tcnAmount").DefValue = lclsCommission.nAmount
						.Columns("tcnPercent_Ce").DefValue = lclsCommission.nPercent_Ce
						Response.Write(.DoRow)
					End With
				Next lclsCommission
			End If
			Response.Write(mobjGrid.closeTable())
			lcolCommission = Nothing
		End If
	End With
	lclsPolicy = Nothing
End Sub
'% insPreCA024Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA024Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsCommission As ePolicy.Commission
	Dim lstrContent As String
	lstrContent = vbNullString
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lclsCommission = New ePolicy.Commission
			Response.Write(mobjValues.ConfirmDelete())
			If lclsCommission.insPostCA024(mobjValues.StringToType(.QueryString.Item("bAll"), eFunctions.Values.eTypeData.etdBoolean), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), mintCommission, mobjValues.StringToType(.Form.Item("tcnPercent"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("optInd_Comm"), mobjValues.StringToType(.QueryString.Item("nIntermed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnShare"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkConColl"), .QueryString.Item("Action"), Session("nTransaction"), mobjValues.StringToType(.Form.Item("cbeAgreement"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPercent_ce"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnInstallcom"), eFunctions.Values.eTypeData.etdDouble)) Then
				lstrContent = lclsCommission.sContent
			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA024", .QueryString.Item("nMainAction") & "&bAll=False", mobjValues.ActionQuery, CShort(.QueryString.Item("Index")), lstrContent))
	End With
	lclsCommission = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA024")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Session("bQuery")
%> 
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 14/05/04 16:01 $|$$Author: Nvaplat15 $"

//% ShowChangeValues: Se habilitan/deshabilitan los controles dependiendo de los datos
//%					  asociados al intermediario
//-------------------------------------------------------------------------------------------
function ShowChangeValues(Action, sTypeComm){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].valIntermed.value!='')
		ShowPopUp("/VTimeNet/Policy/PolicySeq/ShowDefValues.aspx?Field=Intermed&nCodeIntermed=" + self.document.forms[0].valIntermed.value + "&sTypeComm=" + sTypeComm, "ShowDefValuesIntermed", 1, 1,"no","no",2000,2000);
}

//%	ReloadPage: recarga la página en caso de cambiar el tipo de comisión o el porcentaje de
//%				comisión fija
//-------------------------------------------------------------------------------------------
function ReloadPage(){
//-------------------------------------------------------------------------------------------

	var lstrLocation = self.document.location.href;
	lstrLocation = lstrLocation.replace(/&nCommityp.*/, "");
	lstrLocation = lstrLocation.replace(/&ReloadAction.*/, "");
	lstrLocation = lstrLocation + "&nCommityp=" + self.document.forms[0].cbeType.value + "&nPercent=" + self.document.forms[0].tcnPercentCF.value + "&sChangeCom=1";
	self.document.location.href = lstrLocation;
}

//% ChangeValues: se controla el cambio de valor de los campos 
//-------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(Option){
			case "Ind_Comm":
				hddInd_Comm.value = Field.value;
				break;
			case "ConColl":
				hddConColl.value = (Field.checked)?1:2;
		}
	}
}
</SCRIPT>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CA024", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="CA024" NAME="frmCA024" ACTION="valPolicySeq.aspx?bAll=True">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA024Upd()
Else
	Call insPreCA024()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA024")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




