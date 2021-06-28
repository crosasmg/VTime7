<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsGen_cover As eProduct.Gen_cover

'+ Objeto que retiene la descripción del primer consecutivo de la nota    
Dim mstrNoteDescript As String


'%insPreDP034:función que realiza el llenado de cada uno de los campos de la transacción
'%en caso de existir previamente el registro en la tabla Gen_cover.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP034()
	'--------------------------------------------------------------------------------------------
	Dim lcolNotes As eGeneralForm.Notess
	
	mstrNoteDescript = vbNullString
	Call mclsGen_cover.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), Session("nModulec"), Session("nCover"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	'+ Se obtiene la descripción de la nota para ser mostrada sobre el frame principal	                        
	If mclsGen_cover.nNotenum <> eRemoteDB.Constants.intNull Then
		lcolNotes = New eGeneralForm.Notess
		If lcolNotes.Find(mclsGen_cover.nNotenum) Then
			mstrNoteDescript = lcolNotes(1).tDs_text
		End If
	End If
	lcolNotes = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsGen_cover = New eProduct.Gen_cover

mobjValues.ActionQuery = Session("bQuery")

Call insPreDP034()

mobjValues.sCodisplPage = "dp034"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP034", "DP034.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 17:04 $|$$Author: Nvaplat61 $"
       
//% insDisabledField: se controla el valor de los campos dependientes
//---------------------------------------------------------------------------------------------
function insDisabledField(Field){
//---------------------------------------------------------------------------------------------
	with(self.document.forms[0].elements){
		if(Field.checked)
			chkPreSel.checked = Field.checked;
		chkPreSel.disabled = Field.checked;
	}
	
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP034" ACTION="valCoverSeq.aspx?sMode=1">
    <%=mobjValues.ShowWindowsName("DP034")%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=14447><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCur_allow_Gen", 1, CStr(mclsGen_cover.nCurrency), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 1))
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=14442><%= GetLocalResourceObject("valBillitemCaption") %></LABEL></TD>
        	<TD><%With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valBillitem", "tabTab_bill_i", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsGen_cover.nBill_item), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBillitemToolTip"),  , 2))
%>
        	</TD>
        </TR>
	    <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeRetarifCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeRetarif", "table5559", 1, CStr(mclsGen_cover.nRetarif),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRetarifToolTip"),  , 3)%></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tctCondSVSCaption") %></LABEL></TD>
		    <TD><%=mobjValues.TextControl("tctCondSVS", 30, mclsGen_cover.sCondSVS,  , GetLocalResourceObject("tctCondSVSToolTip"),  ,  ,  ,  ,  , 5)%></TD>
        </TR>
    </TABLE>
	<TABLE WIDTH=100%>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"	WIDTH=50%><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD COLSPAN="3"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14444><%= GetLocalResourceObject("cbeBranchLedgerCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranchLedger", "table75", 1, CStr(mclsGen_cover.nBranch_led),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchLedgerToolTip"),  , 4)%></TD>
			<TD COLSPAN="2">&nbsp;</TD>
			<TD><%=mobjValues.CheckControl("chkRequired", GetLocalResourceObject("chkRequiredCaption"), mclsGen_cover.sRequire,  , "insDisabledField(this);",  , 8, GetLocalResourceObject("chkRequiredToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14445><%= GetLocalResourceObject("cbeBranchReinsuCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranchReinsu", "Table5000", 1, CStr(mclsGen_cover.nBranch_rei),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchReinsuToolTip"),  , 5)%></TD>
            <TD COLSPAN="2">&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkPreSel", GetLocalResourceObject("chkPreSelCaption"), mclsGen_cover.sDefaulti,  ,  , mclsGen_cover.sRequire = "1", 9, GetLocalResourceObject("chkPreSelToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchStatisCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranchStatis", "table71", 1, CStr(mclsGen_cover.nBranch_est),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchStatisToolTip"),  , 6)%></TD>
            <TD COLSPAN="2">&nbsp;</TD>
			<TD><%=mobjValues.CheckControl("chkInd_Med_Exp", GetLocalResourceObject("chkInd_Med_ExpCaption"), mclsGen_cover.sInd_Med_Exp,  ,  ,  , 10, GetLocalResourceObject("chkInd_Med_ExpToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14443><%= GetLocalResourceObject("cbeBranchGenericCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranchGeneric", "table634", 1, CStr(mclsGen_cover.nBranch_gen),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranchGenericToolTip"),  , 7)%></TD>
            <TD COLSPAN="2">&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkReinOrigCond", GetLocalResourceObject("chkReinOrigCondCaption"), mclsGen_cover.sReinorigcond, CStr(1),  ,  , 11, GetLocalResourceObject("chkReinOrigCondToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("txtNoteCaption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH=100%>
        <TR>
			<TD><%=mobjValues.TextAreaControl("txtNote", 2, 60, mstrNoteDescript,  , GetLocalResourceObject("txtNoteToolTip"),  , True, 11)%></TD>
			<TD><%=mobjValues.ButtonNotes("SCA2-Y", mclsGen_cover.nNotenum, False, mobjValues.ActionQuery,  ,  ,  , 12)%></TD>
		</TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsGen_cover = Nothing
%>




