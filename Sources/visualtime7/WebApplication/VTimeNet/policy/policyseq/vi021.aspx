<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim lblnEnabled As Object
Dim mlngEval As Object
Dim tcnEval_Gen As String
Dim cbenStatus_eval As Object
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mintStat_docreq As String


'%insDefineHeader. Esta funcion se encarga de declarar las caracteristicas del Grid
'-------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'-------------------------------------------------------------------------------------------
	Dim lstrQueryString As String
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
        With mobjGrid.Columns
            .AddNumericColumn(0, GetLocalResourceObject("tcnEvalColumnCaption"), "tcnEval", 10, vbNullString, , GetLocalResourceObject("tcnEvalColumnToolTip"))
            If Request.QueryString.Item("Type") = "PopUp" Then
                lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
                .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString, , GetLocalResourceObject("tctClientColumnToolTip"), , Request.QueryString.Item("Action") = "Update", , , , , , , 6, lstrQueryString, , eFunctions.Values.eTypeClient.SearchClientPolicy)
                mobjGrid.Columns("tctClient").TypeList = 2
                mobjGrid.Columns("tctClient").ClientRole = "1,13,16,25"
            Else
                .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString, , GetLocalResourceObject("tctClientColumnToolTip"), , True)
            End If
            .AddNumericColumn(0, GetLocalResourceObject("tcnCumulColumnCaption"), "tcnCumul", 18, vbNullString, , GetLocalResourceObject("tcnCumulColumnToolTip"), True, 6)
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, , GetLocalResourceObject("tctDescriptColumnToolTip"), , , , True)
            Else
                If Request.QueryString.Item("Action") = "Update" Then
                    .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, , GetLocalResourceObject("tctDescriptColumnToolTip"), , , , True)
                Else
                    .AddPossiblesColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", "Table32", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("tctDescriptColumnToolTip"))
                End If
            End If
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .AddCheckColumn(0, GetLocalResourceObject("chkRequireColumnCaption"), "chkRequire", vbNullString, , , , True, GetLocalResourceObject("chkRequireColumnToolTip"))
            Else
                .AddHiddenColumn("chkRequire", vbNullString)
            End If
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .AddCheckColumn(0, GetLocalResourceObject("chkAprobColumnCaption"), "chkAprob", vbNullString, , , , , GetLocalResourceObject("chkAprobColumnToolTip"))
            End If
            .AddDateColumn(0, GetLocalResourceObject("tcdRecep_dateColumnCaption"), "tcdRecep_date", vbNullString, , GetLocalResourceObject("tcdRecep_dateColumnToolTip"))
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeStat_docreqColumnCaption"), "cbeStat_docreq", "Table275", eFunctions.Values.eValuesType.clngComboType, mintStat_docreq, , , , , "self.document.forms[0].tcdDatefree.disabled = this.value != 7", , , GetLocalResourceObject("cbeStat_docreqColumnToolTip"))
		
            .AddButtonColumn(0, "", "SCA2-818", CDbl(Request.QueryString.Item("nNoteNum")), , Request.QueryString.Item("Type") <> "PopUp")
            .AddDateColumn(0, GetLocalResourceObject("tcdDatevigColumnCaption"), "tcdDatevig", vbNullString, , GetLocalResourceObject("tcdDatevigColumnToolTip"))
            .AddDateColumn(0, GetLocalResourceObject("tcdDate_toColumnCaption"), "tcdDate_to", vbNullString, , GetLocalResourceObject("tcdDate_toColumnToolTip"))
            .AddDateColumn(0, GetLocalResourceObject("tcdDatefreeColumnCaption"), "tcdDatefree", vbNullString, True, GetLocalResourceObject("tcdDatefreeColumnToolTip"), , , , CBool(lblnEnabled))
            .AddNumericColumn(0, GetLocalResourceObject("tcnEval_masterColumnCaption"), "tcnEval_master", 10, vbNullString, , GetLocalResourceObject("tcnEval_masterColumnToolTip"), , , , , , True)
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatusdocColumnCaption"), "cbeStatusdoc", "Table275", eFunctions.Values.eValuesType.clngComboType, , , , , , , True, , GetLocalResourceObject("cbeStatusdocColumnToolTip"))
            .AddDateColumn(0, GetLocalResourceObject("tcdDocreqColumnCaption"), "tcdDocreq", vbNullString, , GetLocalResourceObject("tcdDocreqColumnToolTip"), , , , True)
            .AddDateColumn(0, GetLocalResourceObject("tcdDocrecColumnCaption"), "tcdDocrec", vbNullString, , GetLocalResourceObject("tcdDocrecColumnToolTip"), , , , True)
            .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", vbNullString, , GetLocalResourceObject("tcdExpirdatColumnToolTip"), , , , True)
            .AddHiddenColumn("hddnCrThecni", vbNullString)
            .AddHiddenColumn("hddnModulec", vbNullString)
            .AddHiddenColumn("hddnCover", vbNullString)
            .AddHiddenColumn("hddnRole", vbNullString)
            .AddHiddenColumn("hddnEval", vbNullString)
            .AddHiddenColumn("hddsKey", vbNullString)
            .AddHiddenColumn("hddnNotenum_cli", vbNullString)
            .AddHiddenColumn("hddnId", vbNullString)
            .AddHiddenColumn("hddnExist", vbNullString)
        End With
	
	With mobjGrid
		.Columns("tctDescript").EditRecord = True
		.Codispl = "VI021"
		.Top = 0
		.Left = 130
        .Width = 650
        .Height = 580
		'.ActionQuery = mobjValues.ActionQuery
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Splits_Renamed.AddSplit(0, vbNullString, 10)
		Else
			.Splits_Renamed.AddSplit(0, vbNullString, 9)
		End If
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("5ColumnCaption"), 5)
		.sDelRecordParam = "sClient='+ marrArray[lintIndex].tctClient + '" & "&nCrThecni='+ marrArray[lintIndex].hddnCrThecni + '" & "&nRole='+ marrArray[lintIndex].hddnRole + '" & "&nEval='+ marrArray[lintIndex].hddnEval + '" & "&nEval_master='+ marrArray[lintIndex].tcnEval_master + '" & "&nId='+ marrArray[lintIndex].hddnId + '" & "&nCover='+ marrArray[lintIndex].hddnCover + '" & "&nModulec='+ marrArray[lintIndex].hddnModulec + '" & "&sRequire='+ marrArray[lintIndex].chkRequire + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
End Function

'%InsPreVI021: Se realiza la lectura de la información
'-------------------------------------------------------------------------------------------
Private Sub InsPreVI021()
	'-------------------------------------------------------------------------------------------
	Dim lcolLife_docu As ePolicy.Life_docu
	Dim lclsLife_docu As Object
	Dim lblnFound As Boolean
	Dim lclsErrors As eFunctions.Errors
	Dim lstrKey As String
	Dim lintCount As Short
	
	lcolLife_docu = New ePolicy.Life_docu
	
	lblnFound = lcolLife_docu.InsPreVI021(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("SessionId"), Session("nUsercode"), Session("nTransaction"), Request.QueryString.Item("sKey"))
	
	If lblnFound Then
		cbenStatus_eval = lcolLife_docu.nStatus_Eval
	Else
		cbenStatus_eval = 3
	End If
	
	
Response.Write("<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("tcnEval_GenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD><DIV CLASS='FIELD' id=lblnEval>")


Response.Write(tcnEval_Gen)


Response.Write("</DIV></TD>")

	'mobjValues.NumericControl("tcnEval_Gen", 10,tcnEval_Gen ,False, GetLocalResourceObject("tcnEval_GenToolTip"), False,,,,,"",True)
Response.Write("" & vbCrLf)
Response.Write("		<TD><LABEL>" & GetLocalResourceObject("cbenStatus_evalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbenStatus_eval", "Table5572", eFunctions.Values.eValuesType.clngComboType, cbenStatus_eval, False,  ,  ,  ,  , "",  ,  , GetLocalResourceObject("cbenStatus_evalToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	
	
	
	If Not lblnFound Then
		lclsErrors = New eFunctions.Errors
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
		lclsErrors.sSessionID = Session.SessionID
		lclsErrors.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		Response.Write(lclsErrors.ErrorMessage("VI021", lcolLife_docu.nErrorNum,  ,  ,  , True))
		lclsErrors = Nothing
		
	Else
		
		
		lstrKey = lcolLife_docu.sKey
		If lstrKey <> vbNullString Then
			Session("sKey") = lstrKey
		End If
		Response.Write(mobjValues.HiddenControl("hddsKeyM", lstrKey))
		
		lintCount = 0
		tcnEval_Gen = ""
		For	Each lclsLife_docu In lcolLife_docu.mcolLife_docus
			With mobjGrid
				tcnEval_Gen = lclsLife_docu.nEval_Gen
				cbenStatus_eval = lclsLife_docu.nStatus_Eval
				
                    .Columns("tcnEval").DefValue = lclsLife_docu.nEval
				.Columns("tctClient").DefValue = lclsLife_docu.sClient
				.Columns("tcnCumul").DefValue = lclsLife_docu.nCumul
				.Columns("hddnModulec").DefValue = lclsLife_docu.nModulec
				.Columns("hddnCrThecni").DefValue = lclsLife_docu.nCrThecni
				.Columns("hddnCover").DefValue = lclsLife_docu.nCover
				If Request.QueryString.Item("Type") <> "PopUp" Then
					.Columns("tctDescript").DefValue = lclsLife_docu.sDescript
				Else
					If Request.QueryString.Item("Action") = "Update" Then
						.Columns("tctDescript").DefValue = lclsLife_docu.nCrThecni
					Else
						.Columns("tctDescript").DefValue = lclsLife_docu.sDescript
					End If
				End If
				.Columns("tcdRecep_date").DefValue = lclsLife_docu.dRecep_date
				.Columns("hddnRole").DefValue = lclsLife_docu.nRole
				.Columns("cbeStat_docreq").DefValue = lclsLife_docu.nStat_docReq
				mintStat_docreq = lclsLife_docu.nStat_docReq
				.Columns("btnNotenum").nNotenum = lclsLife_docu.nNotenum
				.Columns("tcdDatevig").DefValue = lclsLife_docu.dDatevig
				.Columns("tcdDate_to").DefValue = lclsLife_docu.dDate_to
				.Columns("tcdDatefree").DefValue = lclsLife_docu.dDatefree
				.Columns("tcdDatefree").Disabled = lclsLife_docu.nStat_docReq <> 7
				.Columns("tcnEval_master").DefValue = lclsLife_docu.nEval_master
				.Columns("cbeStatusdoc").DefValue = lclsLife_docu.nStatusdoc
				.Columns("tcdDocreq").DefValue = lclsLife_docu.dDocreq
				.Columns("tcdDocrec").DefValue = lclsLife_docu.dDocrec
				.Columns("tcdExpirdat").DefValue = lclsLife_docu.dExpirdat
				.Columns("hddnEval").DefValue = lclsLife_docu.nEval
				.Columns("hddsKey").DefValue = lstrKey
				.Columns("hddnNotenum_cli").DefValue = lclsLife_docu.nNotenum_cli
				.Columns("hddnId").DefValue = lclsLife_docu.nId
				.Columns("hddnExist").DefValue = lclsLife_docu.nExist
                .Columns("chkRequire").DefValue = lclsLife_docu.sRequest
                    
                    
				If lclsLife_docu.sRequest = "1" Then
                        .Columns("Sel").Disabled = True
                        .Columns("chkRequire").Checked = lclsLife_docu.sRequest
                    Else
                        .Columns("Sel").Disabled = False
                        .Columns("chkRequire").Checked = lclsLife_docu.sRequest
                       
				End If
				If lclsLife_docu.nStat_docReq = 2 Then
					.Columns("chkAprob").Checked = 1
				Else
					.Columns("chkAprob").Checked = 2
				End If
				.Columns("chkAprob").OnClick = "insCheckSelClick(this," & CStr(lintCount) & ")"
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		Next lclsLife_docu
	End If
	Response.Write(mobjGrid.CloseTable)
	Response.Write(mobjValues.BeginPageButton)
	Response.Write("<SCRIPT> return_neval_gen('" & tcnEval_Gen & "'); </" & "Script>")
	
	Response.Write(mobjValues.HiddenControl("hdnEval_Gen", tcnEval_Gen))
	lclsLife_docu = Nothing
	lcolLife_docu = Nothing
End Sub

'%InsPreVI021Upd: Se realiza el manejo de los campos del grid 
'-------------------------------------------------------------------------------------------
Private Function InsPreVI021Upd() As Object
	'-------------------------------------------------------------------------------------------
	Dim lobjPolicySeq As ePolicy.Life_docu
	
	lobjPolicySeq = New ePolicy.Life_docu
	With Request
		
		If .QueryString.Item("Action") = "Del" Then
			Call lobjPolicySeq.InsPostVI021Upd("Del", Session("sKey"), vbNullString, mobjValues.StringToType(Request.QueryString.Item("nCrThecni"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), Session("nUsercode"), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, mobjValues.StringToType(Request.QueryString.Item("nEval"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Request.QueryString.Item("nEval_master"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, Request.QueryString.Item("chkRequire"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"))
			
			Response.Write(mobjValues.ConfirmDelete)
		End If
		lobjPolicySeq = Nothing
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI021", Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	
	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tcdDatefree.disabled = self.document.forms[0].cbeStat_docreq.value != 7;</" & "Script>")
	End If
End Function

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI021")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Session("bQuery")
mintStat_docreq = "1"
%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 7 $|$$Date: 11/11/04 10:21 $|$$Author: Nvaplat7 $"
    
//% insCheckSelClick: Pasa a transaccion correspondiente al seleccionar una de las opciones
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrParam      = new String();

    lstrParam += document.location;
    
	lstrParam = lstrParam.replace(/&sCodispl=.*/, "");

    lstrParam = lstrParam + "sCodispl=VI021&Reload=&ReloadAction=Update&ReloadIndex=0"; 
    
    if (typeof(mstrDoSubmit) == 'undefined')
        mstrDoSubmit = '1';
    else
		mstrDoSubmit = '2';
        
	if (mstrDoSubmit == '1'){
    
		with (self.document.forms[0]){
			lstrParam = lstrParam +  "&nActionPop="   + 'Update' +
	                    "&sDescript="    + marrArray[lintIndex].tctDescript   +
						"&nCrThecni="    + marrArray[lintIndex].hddnCrThecni     +
						"&nModulec="     + marrArray[lintIndex].hddnModulec       +
						"&nCover="	     + marrArray[lintIndex].hddnCover   +
						"&nRole="        + marrArray[lintIndex].hddnRole  +
						"&sClient="      + marrArray[lintIndex].tctClient   + 
					    "&dDate_to="     + marrArray[lintIndex].tcdDate_to +
						"&dDatefree="    + marrArray[lintIndex].tcdDatefree +
						"&nEval="        + marrArray[lintIndex].hddnEval +
						"&dExpirdat="    + '' + 
						"&nNotenum="     + marrArray[lintIndex].hddnNotenum_cli + 
						"&nCumul="       + marrArray[lintIndex].tcnCumul + 
						"&nStatusdoc="   + marrArray[lintIndex].cbeStatusdoc + 
						"&dDocreq="      + marrArray[lintIndex].tcdDocreq + 
						"&dDocrec="      + marrArray[lintIndex].tcdDocrec + 
						"&dExpirdat="    + '' + 
						"&nNotenum_cli=" + marrArray[lintIndex].hddnNotenum_cli + 
						"&nEval_master=" + marrArray[lintIndex].tcnEval_master + 
						"&nId="          + marrArray[lintIndex].hddnId + 
						"&nExist="       + marrArray[lintIndex].hddnExist + 
						"&sRequire="     + marrArray[lintIndex].chkRequire;
		}
	//+Si se desmarca, se actualiza cojn pendiente y sin fecha de recepcion
		if (!Field.checked){
			lstrParam = lstrParam + "&dRecep_date=" +
						            "&sStat_docreq=1";
	    }
		else{
lstrParam = lstrParam + "&dRecep_date=" + '<% %>
<%=Today%>' + 
						            "&sStat_docreq=2";
		}
		    
		insDefValues('UpdT_Life_docu', lstrParam);
		}
		else{
		
			if (Field.checked=='true')   
			   Field.checked = Field.checked;
			else
			   Field.checked = !Field.checked;		
			   
			alert('Por favor espere');
	}
}


function return_neval_gen(tcnEval_Gen){
	UpdateDiv('lblnEval',tcnEval_Gen);
	
}
</SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "VI021", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmVI021" ACTION="ValPolicySeq.aspx?sTime=1">

<%
Response.Write(mobjValues.ShowWindowsName("VI021", Request.QueryString.Item("sWindowDescript")))
insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	InsPreVI021()
Else
	InsPreVI021Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI021")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





