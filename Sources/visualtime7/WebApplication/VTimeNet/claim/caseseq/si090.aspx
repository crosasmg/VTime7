<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
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
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
            If Request.QueryString.Item("Type") <> "PopUp" Then
                
                .AddTextColumn(0, GetLocalResourceObject("tctDamage_codColumnCaption"), "tctDamage_cod", 30, "", , GetLocalResourceObject("tctDamage_codColumnToolTip"))
                .AddTextColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 30, "", , GetLocalResourceObject("tcnAmountColumnToolTip"))
                .AddHiddenColumn("valDamage_cod", CStr(0))
                
            Else
                
                Call .AddPossiblesColumn(0, GetLocalResourceObject("valDamage_codColumnCaption"), "valDamage_cod", "tabTab_Fonasa", eFunctions.Values.eValuesType.clngWindowType, , True, , , , "SelectedService(this)", Request.QueryString.Item("Action") = "Update",10 , GetLocalResourceObject("valDamage_codColumnToolTip"))
                mobjGrid.Columns("valDamage_cod").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valDamage_cod").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valDamage_cod").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valDamage_cod").Parameters.ReturnValue("nAmount", , , True)
                'ReturnValues mobjGrid.Columns.("valDamage_cod").Parameters.Add("nAmount", numnull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                
                .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 10, "", , GetLocalResourceObject("tcnAmountColumnToolTip"))
                
            End If
	End With
	
	'+ Se definen las propiedades generales del grid
        With mobjGrid
            
            .sCodisplPage = "SI090"
            .Width = 400
            .Height = 230
            .WidthDelete = 550
            
            If Request.QueryString.Item("Action") = "Update" Then
                .Columns("valDamage_cod").Disabled = True
            End If
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Columns("tctDamage_cod").EditRecord = True
            End If
		
            .Codispl = "SI090"
            .sDelRecordParam = "nDamage_cod='+ marrArray[lintIndex].valDamage_cod + '"
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
End Sub

'% insPreSI090: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI090()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsClaim_Dama As eClaim.Claim_Dama
	Dim lcolClaim_damas As eClaim.Claim_damas
	
	lintIndex = 0
        With Server
            lclsClaim_Dama = New eClaim.Claim_Dama
            lcolClaim_damas = New eClaim.Claim_damas
        End With

'        If lcolClaim_damas.Find_SI090(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
'                                mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), _
'                                mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), _
'                                mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
'            
'            For Each lclsClaim_Dama In lcolClaim_damas
'                With mobjGrid
'                    .Columns("valDamage_cod").DefValue = CStr(lclsClaim_Dama.nDamage_cod)
'                    .Columns("tctDamage_cod").DefValue = lclsClaim_Dama.sDes_Damage_cod
'                    .Columns("tcnAmount").DefValue = lclsClaim_Dama.nAmount
'                    Response.Write(.DoRow)
'                End With
'                lintIndex = lintIndex + 1
'            Next lclsClaim_Dama
'        End If
        Response.Write(mobjGrid.closeTable())
	
        lclsClaim_Dama = Nothing
        lcolClaim_damas = Nothing
End Sub

'----------------------------------------------------------------------------------------------
Private Sub insPreSI090Upd()
	'----------------------------------------------------------------------------------------------
	Dim lclsClaim_Dama As eClaim.Claim_Dama
	Dim lblnPost As Boolean
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsClaim_Dama = New eClaim.Claim_Dama
			lblnPost = lclsClaim_Dama.insPostSI020("SI090", _
			                                       .QueryString.Item("Action"), _
			                                       mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
			                                       mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), _
			                                       mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), _
			                                       mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble), _
			                                       mobjValues.StringToType(.QueryString.Item("nDamage_cod"), eFunctions.Values.eTypeData.etdDouble), _
			                                       mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
			                                       0)
			
			Response.Write(mobjValues.ConfirmDelete)
			lclsClaim_Dama = Nothing
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCaseSeq.aspx", "SI090", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("SI090")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "SI090"
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With

mobjGrid.ActionQuery = Session("bQuery")
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <%  Response.Write(mobjValues.StyleSheet())
        If Request.QueryString.Item("Type") <> "PopUp" Then
            With Response
                .Write(mobjMenu.setZone(2, "SI090", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
                .Write("<SCRIPT>var nMainAction=304</SCRIPT>")
            End With
            mobjMenu = Nothing
        End If
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 15/10/03 12.24 $"        
</SCRIPT>
<SCRIPT>
//% ShowDescrip: 
//-------------------------------------------------------------------------------------------
//function ShowDescript(){
//    //-------------------------------------------------------------------------------------------
//    with (self.document.forms[0]) {
//        if (valDamage_cod.disabled == false)
//            $(valDamage_cod).change();
//    }
//}

//% ShowDescrip: 
//-------------------------------------------------------------------------------------------
function SelectedService() {
    //-------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        if (tcnAmount.value == '')
            tcnAmount.value = valDamage_cod_nAmount.value;
    }
}  

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSI090" ACTION="valCaseSeq.aspx?smode=1">
    <%Response.Write(mobjValues.ShowWindowsName("SI090", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreSI090Upd()
	'If Request.QueryString.Item("Action") = "Update" Then
	'	Response.Write("<SCRIPT>ShowDescript();</SCRIPT>")
	'End If
Else
	Call insPreSI090()
End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjGrid = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
Call mobjNetFrameWork.FinishPage("SI090")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




