<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mclsSituation As ePolicy.Situation


    
    'aaa

'**% insDefineHeader: Defines the columns of the grid 
'% insDefineHeader: Define las columnas del grid
'-------------------------------------------------------------------------------------------
Private Function insDefineHeader() As Object
	'-------------------------------------------------------------------------------------------
        Dim lobjCol As eFunctions.Column
        
        mobjGrid = New eFunctions.Grid
        With mobjGrid.Columns
            lobjCol = .AddPossiblesColumn(0, GetLocalResourceObject("valAgreementCaption"), "valAgreement", "TABAGREESITUATION", Values.eValuesType.clngWindowType, , True, , , , "changeAgree(this.value);")
            lobjCol.Parameters.ReturnValue("sClient", , , True)
            lobjCol.Parameters.ReturnValue("sDigit", , , True)
            lobjCol.Parameters.ReturnValue("sCliename", , , True)
            
            .AddNumericColumn(0, GetLocalResourceObject("tcnSituationColumnCaption"), "tcnSituation", 5, vbNullString, True, GetLocalResourceObject("tcnSituationColumnToolTip"))
            .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString, True, GetLocalResourceObject("tctDescriptColumnToolTip"))
            .AddClientColumn(0, GetLocalResourceObject("cbePolicyHolderColumnCaption"), "cbePolicyHolder", vbNullString, , GetLocalResourceObject("cbePolicyHolderColumnToolTip"), , , , , , , True, , True, eFunctions.Values.eTypeClient.SearchClientPolicy)
        End With

        With mobjGrid.Columns("valAgreement").Parameters
            '+ Los valores de los convenios pueden ser por polizas.
            .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            
        End With
        
        With (mobjGrid)
            .Columns("Sel").OnClick = "OnChangeSel(this);"
            .AddButton = True
            .DeleteButton = True
            .ActionQuery = Session("bQuery")
            .Codispl = Request.QueryString.Item("sCodispl")
            .Width = 450
            .Height = 250
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Columns("tctDescript").EditRecord = True
            
            .sDelRecordParam = "tcnSituation=' + marrArray[lintIndex].tcnSituation + '" & "&nAgreement='+ marrArray[lintIndex].valAgreement + '"
                                
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Update" Then
                .Columns("tcnSituation").Disabled = True
            End If
        End With
    End Function

'**% insPreCA008: The page controls are loading
'%insPreCA008: Se cargan los controles de la página
'-------------------------------------------------------------------------------------------
Private Sub insPreCA008()
	'-------------------------------------------------------------------------------------------
	Dim mclsSituation As Object
	Dim mcolSituations As ePolicy.Situations
	
	mcolSituations = New ePolicy.Situations
	
	If mcolSituations.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
		With mobjGrid
                For Each mclsSituation In mcolSituations
                    .Columns("valAgreement").DefValue = mclsSituation.nCod_agree
                    .Columns("tcnSituation").DefValue = mclsSituation.nSituation
                    .Columns("tctDescript").DefValue = mclsSituation.sDescript
                    .Columns("cbePolicyHolder").DefValue = mclsSituation.sClient
                    Response.Write(.DoRow)
                Next mclsSituation
		End With
	End If
	Response.Write(mobjGrid.CloseTable)
	mcolSituations = Nothing
	mclsSituation = Nothing
End Sub

'**% insPreCA008Upd: The page controls are loading
'%insPreCA008Upd: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCA008Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjError As eFunctions.Errors
	Dim lclsSituation As ePolicy.Situation
	Dim lclsPolicy As ePolicy.Policy
	
	lclsPolicy = New ePolicy.Policy
	lclsSituation = New ePolicy.Situation
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lobjError = New eFunctions.Errors
			
			If lclsSituation.FindCertificatCA008(Session("sCertype"), Session("nBranch"), Session("nPolicy"), Session("nProduct"), CInt(Request.QueryString.Item("tcnSituation"))) Then
				lobjError.Highlighted = True
				Response.Write(lobjError.ErrorMessage("CA008", 3802,  ,  ,  , True))
			Else
                    If lclsSituation.DeleteSituation(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), CInt(Request.QueryString.Item("tcnSituation")), CInt(Request.QueryString.Item("nAgreement"))) Then
                        Response.Write(mobjValues.ConfirmDelete())
                        Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
                    End If
			End If
		Else
			If UCase(.QueryString.Item("Action")) = "ADD" Then
                    If lclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
                        If lclsPolicy.nCod_Agree < 0 Then
                            mobjGrid.Columns("cbePolicyHolder").DefValue = lclsPolicy.SCLIENT
                        End If
                    End If
                End If
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValPolicySeq.aspx", "CA008", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	
	If UCase(Request.QueryString.Item("Action")) = "ADD" Then
		Response.Write("<SCRIPT>$(document.forms[0].cbePolicyHolder).change()</" & "Script>")
	End If
	lobjError = Nothing
	lclsPolicy = Nothing
	lclsSituation = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsSituation = New ePolicy.Situation
End With
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>


    <HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<%
With Response
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "CA008.aspx"))
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
	End If
End With
%>
<SCRIPT>
//% For the Source Safe control
//% Para control de versiones
//---------------------------------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $"
//---------------------------------------------------------------------------------------------------------------------

function changeAgree(obj){

    if (obj != 0){
        self.document.forms[0].cbePolicyHolder.value = self.document.forms[0].valAgreement_sClient.value;
        self.document.forms[0].cbePolicyHolder_Digit.value = self.document.forms[0].valAgreement_sDigit.value;
        UpdateDiv('cbePolicyHolder_Name',self.document.forms[0].valAgreement_sCliename.value);
        
    }

}

//% OnChangeSel: Verifica si es posible borrar
//---------------------------------------------------------------------------------------------------------------------
function OnChangeSel(Field){
//---------------------------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        insDefValues('sSel', "sCertype=" + '<%=Session("sCertype")%>' + "&nBranch=" + <%=Session("nBranch")%> + "&nProduct=" + <%=Session("nProduct")%> + "&nPolicy=" + <%=Session("nPolicy")%>  + "&nCertif=" + <%=Session("nCertif")%> + "&nSituation=" + marrArray[Field.value].tcnSituation + "&sCodispl=" + "CA008" + "&nIndex=" + Field.value ,'/VTimeNet/Policy/PolicySeq')
	}
}              
</SCRIPT>
	</HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmCA008" ACTION="ValPolicySeq.aspx?sTime=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA008()
Else
	Call insPreCA008Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
        </FORM>
    </BODY>
</HTML>




