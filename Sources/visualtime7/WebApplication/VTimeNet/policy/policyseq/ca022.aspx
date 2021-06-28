<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As eFunctions.Grid
    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues
    '- Variable para verificar si el producto en modular
    Dim mblnIsModul As Boolean
    '-Variable para indicar ramo técnico Automovil o Generales	
    Dim isAutoOrGral As Boolean
    Dim lstrAutoOrGral As String

    '-Variable para indicar si se modifican las notas de las cláusulas (opciones de instalación)
    Dim mblnEnableEditDesc As Boolean


    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        Dim lclsProduct As eProduct.Product
        Dim lclsOpt_system As eGeneral.Opt_system
        lclsOpt_system = New eGeneral.Opt_system
        Call lclsOpt_system.Find()
        mblnEnableEditDesc = (lclsOpt_system.sPrint_tx_c = "1")
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
        Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

        lclsProduct = New eProduct.Product
        mblnIsModul = lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate"))
        '+ Se definen las columnas del grid
        With mobjGrid.Columns

            .AddPossiblesColumn(0, GetLocalResourceObject("valClauseColumnCaption"), "valClause", "TabTab_Clause", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "InsChangeClause(this,'" & lstrAutoOrGral & "');", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("valClauseColumnToolTip"))

            If Not isAutoOrGral Then
                .AddPossiblesColumn(0, GetLocalResourceObject("valInsuredColumnCaption"), "valInsured", "TabInsurCoverPol", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  , "InsChangeInsured();",  , 14, GetLocalResourceObject("valInsuredColumnToolTip"), eFunctions.Values.eTypeCode.eString)
            End If
            If mblnIsModul Then
                .AddTextColumn(0, GetLocalResourceObject("tctModuleColumnCaption"), "tctModule", 30, vbNullString,  , GetLocalResourceObject("tctModuleColumnToolTip"),  ,  ,  , True)
            End If

            .AddCheckColumn(0, GetLocalResourceObject("chkType_ClauseColumnCaption"), "chkType_Clause", vbNullString,  ,  , "insChangeType(this)", Request.QueryString.Item("Action") <> "Add", GetLocalResourceObject("chkType_ClauseColumnToolTip"))

            If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Add" Then
                .AddFileColumn(0, GetLocalResourceObject("tctFileColumnCaption"), "tctFile", 45,  , True)
            End If


            If Not (Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Add") Then
                .AddTextColumn(0, GetLocalResourceObject("tctDoc_attachColumnCaption"), "tctDoc_attach", 45, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctDoc_attachColumnToolTip"),  ,  ,  , True)
            End If

            If Request.QueryString.Item("Type") = "PopUp" Then
                .AddTextAreaColumn(0, GetLocalResourceObject("tctCoverColumnCaption"), "tctCover", vbNullString, 3, 45,  , GetLocalResourceObject("tctCoverColumnToolTip"), True)
            Else
                .AddTextColumn(0, GetLocalResourceObject("tctCover_auxColumnCaption"), "tctCover_aux", 120,vbNullString,  , GetLocalResourceObject("tctCover_auxColumnToolTip"),  ,  ,  , True)
            End If

            .AddPossiblesColumn(0, GetLocalResourceObject("cbeCauseColumnCaption"), "cbeCause", "table5631", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCauseColumnToolTip"))

            .AddCheckColumn(0, GetLocalResourceObject("chkAgreeColumnCaption"), "chkAgree", vbNullString,  ,  ,"insChangeDef(this)",  , GetLocalResourceObject("chkAgreeColumnToolTip"))

            .AddButtonColumn(0, GetLocalResourceObject("SCA2-AColumnCaption"), "SCA2-A", 0, True, Request.QueryString.Item("Type") <> "PopUp" Or Session("bQuery"),  ,  ,  ,  , "btnNotenum")

            '+Se crean campos ocultos para la operación masiva de la forma
            .AddHiddenColumn("hddClause", vbNullString)
            .AddHiddenColumn("hddSel", "2")
            .AddHiddenColumn("hddExists", vbNullString)
            .AddHiddenColumn("hddInsured", vbNullString)
            .AddHiddenColumn("hddModulec", vbNullString)
            .AddHiddenColumn("hddCover", vbNullString)
            .AddHiddenColumn("hddCause", vbNullString)
            .AddHiddenColumn("hddId", vbNullString)
            .AddHiddenColumn("hddGroup_Insu", vbNullString)
            .AddHiddenColumn("hddNoteNum", vbNullString)

            '.AddHiddenColumn "tcnNotenum", vbNullString

            .AddHiddenColumn("tcnIniNote", vbNullString)

            .AddHiddenColumn("hddCheckFile", "2")
            .AddHiddenColumn("hddChkAgree", "2")
        End With
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .ActionQuery = mobjValues.ActionQuery
            .WidthDelete = 500

            If Not mblnIsModul Then
                .Splits_Renamed.AddSplit(0, "", 2)
            Else
                .Splits_Renamed.AddSplit(0, "", 3)
            End If

            .Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)

            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Columns("valClause").EditRecord = True
            End If

            .Width = 850

            If Not isAutoOrGral Then
                .Height = 550
            Else
                .Height = 450
            End If

            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            .Columns("Sel").GridVisible = Not .ActionQuery
            '+Se definen los parámetros de valores posibles de cláusulas
            If Request.QueryString.Item("Type") = "PopUp" Then
                .Columns("valClause").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valClause").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valClause").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valClause").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valClause").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valClause").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valClause").Parameters.ReturnValue("nNotenum",  ,  , True)
                .Columns("valClause").Parameters.ReturnValue("sModulec",  ,  , True)
                .Columns("valClause").Parameters.ReturnValue("sCover",  ,  , True)
                .Columns("valClause").Parameters.ReturnValue("nCover",  ,  , True)
                .Columns("valClause").Parameters.ReturnValue("nModulec",  ,  , True)
            End If
            '+Se definen los parámetros de valores posibles de Asegurados/Cobertura
            If Not isAutoOrGral Then
                .Columns("valInsured").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valInsured").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valInsured").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valInsured").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valInsured").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("valInsured").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                If Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Update" Then
                    .Columns("valInsured").Parameters.Add("nCover", Request.QueryString.Item("nCover"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    .Columns("valInsured").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If

                If mblnIsModul Then
                    .Columns("valInsured").Parameters.Add("sIndModul", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Else
                    .Columns("valInsured").Parameters.Add("sIndModul", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End If
                .Columns("valInsured").Parameters.Add("nClause", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                '.Columns("valInsured").Parameters.ReturnValue "sDescRole", True, "Figura", True
                .Columns("valInsured").Parameters.ReturnValue("sDescript", True, "Cobertura", True)
                .Columns("valInsured").Parameters.ReturnValue("sClient",  ,  , True)
                .Columns("valInsured").Parameters.ReturnValue("nModulec",  ,  , True)
                .Columns("valInsured").Parameters.ReturnValue("nCover",  ,  , True)
                .Columns("valInsured").Parameters.ReturnValue("nGroup_insu",  ,  , True)
                If mblnIsModul Then
                    .Columns("valInsured").Parameters.ReturnValue("sDescmodul",  ,  , True)
                End If
            End If
            .sDelRecordParam = "nClause=' + marrArray[lintIndex].valClause + '&nId=' + marrArray[lintIndex].hddId +  '"
            .Columns("chkAgree").Disabled = Request.QueryString.Item("Type") <> "PopUp"
            If Request.QueryString.Item("Type") = "PopUp" and  Request.QueryString.Item("Action") = "Add" Then
                '.Columns("chkAgree").Checked = "1"
            End If
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
        lclsProduct = Nothing
        lclsOpt_system = Nothing
    End Sub
    '% insPreCA022: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA022()
        '--------------------------------------------------------------------------------------------
        '- Objetos para el manejo particular de los datos de la página
        Dim lcolClauses As ePolicy.Clauses
        Dim lclsClauses As Object
        Dim lintCount As Short
        Dim lclsRefresh As ePolicy.ValPolicySeq
        lintCount = 0
        lcolClauses = New ePolicy.Clauses

        If lcolClauses.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("sBrancht"), Session("nUsercode")) Then
            For Each lclsClauses In lcolClauses
                With mobjGrid
                    '+Se cargan columnas ocultas
                    .Columns("hddClause").DefValue = lclsClauses.nClause
                    .Columns("hddSel").DefValue = lclsClauses.nSel
                    .Columns("hddInsured").DefValue = lclsClauses.sClient
                    .Columns("hddModulec").DefValue = lclsClauses.nModulec
                    .Columns("hddCover").DefValue = lclsClauses.nCover
                    .Columns("hddId").DefValue = lclsClauses.nId
                    .Columns("hddGroup_Insu").DefValue = lclsClauses.nGroup_insu
                    .Columns("hddNoteNum").DefValue = lclsClauses.nNotenum
                    .Columns("btnNotenum").nNotenum = lclsClauses.nNotenum
                    .Columns("tcnIniNote").DefValue = lclsClauses.nNotenum
                    '+Se cargan columnas visibles
                    .Columns("Sel").Checked = lclsClauses.nSel
                    '.Columns("Sel").Disabled = lclsClauses.sModified <> "1"
                    .Columns("valClause").HRefScript = ""
                    .Columns("valClause").EditRecord = lclsClauses.sModified = "1"
                    .Columns("valClause").DefValue = lclsClauses.nClause
                    .Columns("valClause").Descript = lclsClauses.sDescript
                    .Columns("cbeCause").DefValue = lclsClauses.nCause
                    .Columns("hddCause").DefValue = lclsClauses.nCause

                    .Columns("chkAgree").DefValue = lclsClauses.sAgree
                    .Columns("hddChkAgree").DefValue = lclsClauses.sAgree
                    .Columns("chkAgree").Checked = lclsClauses.sAgree

                    .Columns("btnNotenum").nIndexNotenum = lintCount
                    If Not mblnEnableEditDesc Then
                        If CDbl(mobjGrid.Columns("hddNoteNum").DefValue) <= 0 Then
                            mobjGrid.Columns("btnNotenum").Disabled = True
                        End If
                    End If
                    .sQueryString = "sAllowEdit=" & lclsClauses.sModified & "&sCodisplOri=" & Request.QueryString.Item("sCodispl")
                    If Not isAutoOrGral Then
                        .Columns("valInsured").DefValue = lclsClauses.sClient
                        .Columns("valInsured").Parameters.Add("nCover", lclsClauses.nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Columns("valInsured").Parameters.Add("nClause", lclsClauses.nClause, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    End If

                    .sEditRecordParam = "nCover=" & lclsClauses.nCover & "&nId=" & lclsClauses.nId
                    .Columns("tctCover_aux").DefValue = lclsClauses.sDesc_cover

                    If mblnIsModul Then
                        .Columns("tctModule").DefValue = lclsClauses.sModulecDesc
                    End If
                    .Columns("chkType_Clause").Checked = IIf(lclsClauses.sType_clause = Nothing, 2, lclsClauses.sType_clause)
                    .Columns("tctDoc_attach").DefValue = lclsClauses.sDoc_attach

                    Response.Write(.DoRow)
                End With
                lintCount = lintCount + 1
            Next lclsClauses

            lclsRefresh = New ePolicy.ValPolicySeq
            Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "No"))
            lclsRefresh = Nothing

        End If

        Response.Write(mobjGrid.closeTable())
        lcolClauses = Nothing
        lclsClauses = Nothing
    End Sub
    '% insPreCA022Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA022Upd()
        '--------------------------------------------------------------------------------------------
        Dim mobjPolicy As ePolicy.Clause

        With Request
            If Request.QueryString.Item("Action") = "Del" Then
                mobjPolicy = New ePolicy.Clause
                Call mobjPolicy.InsPostCA022Upd(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nClause"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valInsured"), mobjValues.StringToType(.Form.Item("hddModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCause"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkAgree"), mobjValues.StringToType(.Form.Item("hddGroup_insu"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIniNote"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hddNotenum"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), Session("nTransaction"), Session("dNulldate"))
                '+ Se refresca la secuencia, para el control de las imágenes
                Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & .QueryString.Item("nMainAction") & "&sGoToNext=NO" & "';</" & "Script>")
                Response.Write(mobjValues.ConfirmDelete)

                mobjPolicy = Nothing
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCA022Seq.aspx", "CA022", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))

            If Request.QueryString.Item("Action") = "Update" Then
                '+ Se actualizan los objetos/variables para el manejo de las notas
                Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum;nCopyNotenum=top.opener.marrArray[CurrentIndex].btnNotenum;</" & "Script>")
            End If
        End With
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA022")
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
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
isAutoOrGral = CStr(Session("sBrancht")) = "3" Or CStr(Session("sBrancht")) = "4"
If isAutoOrGral Then
	lstrAutoOrGral = "1"
Else
	lstrAutoOrGral = "2"
End If
    
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE=JavaScript>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 13 $|$$Date: 22/06/04 13:28 $|$$Author: Nvaplat22 $"

//%InsChangeInsured: Se ejecuta cuando se seleccionado un asegurado
//------------------------------------------------------------------------------------------
function InsChangeInsured(){
//------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
		if (valInsured.value == ''){
			valInsured_sDescript.value = '';
			valInsured_sClient.value = '';
			tctCover.value = '';
			if (typeof(tctModule) != 'undefined'){
				valInsured_sDescmodul.value = '';
			}
		}
		tctCover_sDescript = '';
		tctCover.value = valInsured_sDescript.value;
		valInsured.value = valInsured_sClient.value;
		hddInsured.value = valInsured.value
		hddCover.value = valInsured_nCover.value;
		hddModulec.value = valInsured_nModulec.value;
		hddGroup_Insu.value = valInsured_nGroup_insu.value;
		if (typeof(tctModule) != 'undefined')
			tctModule.value = valInsured_sDescmodul.value;
	}
}
//%InsChangeClause: Se ejecuta cuando se seleccionado una cláusula
//------------------------------------------------------------------------------------------
function InsChangeClause(Obj,AutoOrGral){
//------------------------------------------------------------------------------------------		              
	with (self.document.forms[0]){
		tcnIniNote.value = valClause_nNotenum.value;
		nOriginalNotenum = valClause_nNotenum.value;
		if (typeof(tctModule) != 'undefined'){
			tctModule.value=valClause_sModulec.value;
			hddModulec.value = valClause_nModulec.value;
		}
		tctCover.value=valClause_sCover.value;
		hddCover.value = valClause_nCover.value;
		if ((AutoOrGral == '2') && (Obj.value != ''))
			valInsured.Parameters.Param9.sValue = Obj.value;
	}
}

//%insChangeType: se controla el cambio de tipo de cláusula según archivo
//--------------------------------------------------------------------------------------------------
function insChangeType(Field){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    
        if(Field.checked){
            hddCheckFile.value = "1";
            tctFile.disabled = false;
            btnNotenum.disabled = true;
        } else {
            hddCheckFile.value = "2";
            tctFile.value = "";
            tctFile.disabled = true;
            btnNotenum.disabled = false;
        }
    }
}

//%insChangeDef: 
//--------------------------------------------------------------------------------------------------
function insChangeDef(Field){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    
        if(Field.checked){
            hddChkAgree.value = "1";
        } else {
            hddChkAgree.value = "2";
        }
    }
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sCodispl") & Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmClauses" ACTION="ValCA022Seq.aspx?sMode=2" ENCTYPE="multipart/form-data">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA022Upd()
Else
	Call insPreCA022()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA022")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




