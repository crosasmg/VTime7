<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mblnModule As Boolean


'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        '+ Se definen las columnas del grid.
        Dim lstrTab_Cover As String
        Dim lclsProduct As eProduct.Product
        If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
            lstrTab_Cover = "TABLIFE_COVER"
        Else
            lstrTab_Cover = "TABGEN_COVER2"
        End If
	
        lclsProduct = New eProduct.Product
        mblnModule = lclsProduct.IsModule(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        With mobjGrid.Columns
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_reserveColumnCaption"), "cbeType_reserve", "Table127", eFunctions.Values.eValuesType.clngComboType, "", , , , , , Request.QueryString.Item("Action") = "Update", 5, GetLocalResourceObject("cbeType_reserveColumnCaption"))
            If mblnModule Then
                .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , "insChangeModulec(this);", Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("valModulecColumnToolTip"))
            Else
                .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , , True, , GetLocalResourceObject("valModulecColumnToolTip"))
            End If
            .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", lstrTab_Cover, eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , , mblnModule, , GetLocalResourceObject("valCoverColumnToolTip"))
            '.AddTextColumn(41266, GetLocalResourceObject("tctRoureserColumnCaption"), "tctRoureser", 12, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctRoureserColumnCaption"))
            .AddPossiblesColumn(0, GetLocalResourceObject("tctRoureserColumnCaption"), "tctRoureser", "TABTAB_ROUTINE", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , , , 12, GetLocalResourceObject("tctRoureserColumnCaption"), eFunctions.Values.eTypeCode.eString)
		
            mobjGrid.Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("valModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("valModulec").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
                mobjGrid.Columns("valCover").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                mobjGrid.Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjGrid.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            
            'Reservas
            mobjGrid.Columns("tctRoureser").Parameters.Add("NROUTINETYPE", 4, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
        End With
        '+ Se definen las propiedades generales del grid.
        With mobjGrid
            .AddButton = True
            .DeleteButton = True
            .Height = 280
            .Width = 450
            .Codispl = "DP201"
            .Columns("cbeType_reserve").EditRecord = True
            '.nMainAction = Request.QueryString("nMainAction")
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .Columns("Sel").GridVisible = Not Session("bQuery")
            .sDelRecordParam = "nType_reserve=' + marrArray[lintIndex].cbeType_reserve  + '" & "&nModulec=' + marrArray[lintIndex].valModulec  + '" & "&nCover=' + marrArray[lintIndex].valCover  + '"
        End With
    End Sub
'% insPreDP201: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
    Private Sub insPreDP201()        
        '--------------------------------------------------------------------------------------------
        Dim lblnDataFound As Object
        Dim lindexnModule As Object
        Dim lindexnCover As Object
	
        Dim lclsProd_reserve As Object
        Dim lcolProd_reserve As eProduct.Prod_reserves
	
        lcolProd_reserve = New eProduct.Prod_reserves
	
        If lcolProd_reserve.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		
            For Each lclsProd_reserve In lcolProd_reserve
                With mobjGrid
                    .Columns("cbeType_reserve").DefValue = lclsProd_reserve.nType_reserve
                    .Columns("valModulec").DefValue = lclsProd_reserve.nModulec
                    .Columns("valCover").Parameters.Add("nModulec", lclsProd_reserve.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("valCover").DefValue = lclsProd_reserve.nCover
                    .Columns("tctRoureser").DefValue = lclsProd_reserve.sRoureser
                    Response.Write(.DoRow)
                End With
            Next lclsProd_reserve
        End If
        Response.Write(mobjGrid.closeTable)
        lcolProd_reserve = Nothing
        lclsProd_reserve = Nothing
    End Sub

'% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreDP201Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsProd_reserve As eProduct.Prod_reserve
	lclsProd_reserve = New eProduct.Prod_reserve
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		If lclsProd_reserve.insPostDP210("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.QueryString.Item("nType_reserve"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), "") Then
			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End If
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", "DP201", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	lclsProd_reserve = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP201"

mobjMenu = New eFunctions.Menues

mobjGrid = New eFunctions.Grid
mobjGrid.sCodisplPage = "DP201"

mobjGrid.ActionQuery = Session("bQuery")
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
    function insChangeModulec(Field) {
        //self.document.forms[0].valCover.value = '';
        //UpdateDiv('valCoverDesc', ' ');
        //self.document.forms[0].valCover.disabled = (Field.value == '');
        //self.document.forms[0].btnvalCover.disabled = self.document.forms[0].valCover.disabled;
        //self.document.forms[0].valCover.Parameters.Param4.sValue = Field.value;

        with(self.document.forms[0]){
<%
If CStr(Session("sBrancht")) = "1" Or CStr(Session("sBrancht")) = "2" Then
	%>
		valCover.Parameters.Param4.sValue=Field.value;
<%Else%>
		valCover.Parameters.Param5.sValue=Field.value;
<%End If
If Request.QueryString.Item("Action") <> "Update" Then
	%>
		valCover.value="";
		UpdateDiv("valCoverDesc","");
		valCover.disabled=(Field.value=="" || Field.value==0)?true:false;
		btnvalCover.disabled=(Field.value=="" || Field.value==0)?true:false;
<%End If%>
	}
    }
</SCRIPT>

<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP201", "DP201.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
//- Variable para el control de versiones
       document.VssVersion="$$Revision: 2 $|$$Date: 29/06/06 5:41p $"
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP201" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP201"))
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP201Upd()
Else
	Call insPreDP201()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





