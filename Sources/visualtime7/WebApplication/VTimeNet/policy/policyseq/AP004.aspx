<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
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

    '+declaracion de los objectos con referencias a tablas.
    Dim lclsProduct As eProduct.Product
    Dim mintModul As Byte


    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lstrQueryString As String
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility

	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
		.AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", vbNullString,  , GetLocalResourceObject("tctClientColumnToolTip"), "insChangeValues('" & mintModul & "');", Request.QueryString.Item("Action") = "Update",  ,  ,  ,  ,  ,  , 6, lstrQueryString,  , eFunctions.Values.eTypeClient.SearchClientPolicy)
		mobjGrid.Columns("tctClient").TypeList = 2
		mobjGrid.Columns("tctClient").ClientRole = "1,13,16,25"
		.AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabmodules", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insChangeValues('" & mintModul & "');", mintModul = 0,  , GetLocalResourceObject("valModulecColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "TABTAB_COVROL5", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("valCoverColumnCaption"))
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeClass_apColumnCaption"), "cbeClass_ap", "Table174", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeClass_apColumnCaption"))
		.AddHiddenColumn("tctClient_Role", CStr(0))
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.sDelRecordParam = "sClient='+ marrArray[lintIndex].tctClient + '" & "&nModulec='+ marrArray[lintIndex].valModulec + '" & "&nCover='+ marrArray[lintIndex].valCover + '"
		.CODISPL = "AP004"
		.ActionQuery = Session("bQuery")
		.AddButton = True
		.DeleteButton = True
		.Columns("tctClient").EditRecord = True
		.Height = 380
		.Width = 420
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valModulec").Parameters.add("sCertype", mobjValues.StringToType(Session("sCertype"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.add("nPolicy", mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.add("nCertif", mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

		.Columns("valCover").Parameters.add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.add("nModulec", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.add("nRole", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.add("sCacaltyp", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
    End Sub
    '% insPreAP004: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreAP004()
	'--------------------------------------------------------------------------------------------
	Dim lclsclass_ap As ePolicy.Class_ap
	Dim lcolclass_ap As ePolicy.Class_aps
	Dim nCoverGroup As Object

	lclsclass_ap = New ePolicy.Class_ap
	lcolclass_ap = New ePolicy.Class_aps

	'+ Si Request.QueryString("nCharge") <> 1, se asigna por default el valor encontrado en FindGroupCover
	'+ Si es igual, entonces se trata del grupo actual

        'INI FIX UGVT7-MG
        If lcolclass_ap.Find(mobjValues.StringToType(Session("sCertype"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

            '+clase que busca en la tabla class_ap
            '  mobjValues.StringToType(Request.QueryString("cbeGroup"),eFunctions.Values.eTypeData.etdDouble),     If lcolclass_ap.Find(Session("sCertype"),                          mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong),                          mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong),                          mobjValues.StringToType(session("nPolicy"), eFunctions.Values.eTypeData.etdDouble),                          mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble),                          mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
            For Each lclsclass_ap In lcolclass_ap
                With mobjGrid
                    .Columns("tctClient").DefValue = lclsclass_ap.sClient
                    .Columns("valModulec").DefValue = CStr(lclsclass_ap.nModulec)
                    .Columns("valCover").Parameters.Add("nModulec", lclsclass_ap.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("valCover").DefValue = CStr(lclsclass_ap.nCover)
                    .Columns("cbeClass_ap").DefValue = CStr(lclsclass_ap.nClass_ap)
                    Response.Write(.DoRow)
                End With
            Next lclsclass_ap
            'End If

        End If
        'END FIX UGVT7-MG

	Response.Write(mobjGrid.closeTable())
	lcolclass_ap = Nothing
	lclsclass_ap = Nothing
    End Sub

    '% insPreAP004Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreAP004Upd()
	'--------------------------------------------------------------------------------------------
	'+objecto con referencia a la tabla "class_ap"
	Dim lobjclass_ap As ePolicy.Class_ap
	lobjclass_ap = New ePolicy.Class_ap
	Dim lclsRefresh As ePolicy.ValPolicySeq
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjclass_ap.insPostAP004Upd(.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdLong), Session("dEffecdate"), eRemoteDB.Constants.intNull, Session("nUserCode")) Then

				lclsRefresh = New ePolicy.ValPolicySeq
				Response.Write(lclsRefresh.RefreshSequence(.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("sBrancht"), Session("sPolitype"), "No"))
				lclsRefresh = Nothing

			End If
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "AP004", .QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
	End With
	lobjclass_ap = Nothing
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AP004")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
'- Objeto para el manejo particular de los datos de la página
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
'mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JAVASCRIPT" SRC="/VTimeNet/SCRIPTS/GENFUNCTIONS.JS"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "AP004", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

//% insChangeValues: Se asignan los valores de los parametros de los tab_tables
//----------------------------------------------------------------------------------------
function insChangeValues(mintModul){
//----------------------------------------------------------------------------------------
	var sAction = '<%=Request.QueryString.Item("Action")%>';
	with (self.document.forms[0])
	{
		if (sAction == 'Update')
		{
			valCover.disabled = true;
			valModulec.disabled = true;
		}
		else
		{
			valModulec.disabled = false;
			valCover.Parameters.Param3.sValue = valModulec.value;
			valCover.Parameters.Param5.sValue = tctClient_Role.value;
			valCover.value = '';
			UpdateDiv('valCoverDesc','');
			valCover.disabled = (tctClient.value == '' || (mintModul == '1' && valModulec.value == ''));
		}
		btnvalCover.disabled = valCover.disabled;
		btnvalModulec.disabled = valModulec.disabled;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="AP004" ACTION="ValPolicySeq.aspx?X=1">
<%
Response.Write(mobjValues.ShowWindowsName("AP004", Request.QueryString.Item("sWindowDescript")))

lclsProduct = New eProduct.Product
If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
	mintModul = 1
Else
	mintModul = 0
End If
lclsProduct = Nothing

'+Define la cabezera del Frame
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreAP004Upd()
Else
	Call insPreAP004()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("AP004")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




