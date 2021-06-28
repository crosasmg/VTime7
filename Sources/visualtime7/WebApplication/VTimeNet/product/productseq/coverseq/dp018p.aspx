<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsLife_cover As eProduct.Life_cover
Dim mclsTab_Lifcov As eProduct.Tab_lifcov

'+ Objeto que retiene la descripción del primer consecutivo de la nota    
Dim mstrNoteDescript As String


'% insPreDP018P: se realiza la búsqueda del valor de los campos 
'--------------------------------------------------------------------------------------------
Private Sub insPreDP018P()
	'--------------------------------------------------------------------------------------------
	Dim lcolNotes As eGeneralForm.Notess
	mstrNoteDescript = vbNullString
	Call mclsLife_cover.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	Call mclsTab_Lifcov.Find(mobjValues.StringToType(Session("nCovergen"), eFunctions.Values.eTypeData.etdDouble))
	
	'+ Se obtiene la descripción de la nota para ser mostrada sobre el frame principal	                        
	
	If mclsLife_cover.nNotenum <> eRemoteDB.Constants.intNull Then
		lcolNotes = New eGeneralForm.Notess
		If lcolNotes.Find(mclsLife_cover.nNotenum) Then
			mstrNoteDescript = lcolNotes(1).tDs_text
		End If
	End If
	lcolNotes = Nothing
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsLife_cover = New eProduct.Life_cover
	mclsTab_Lifcov = New eProduct.Tab_lifcov
End With

mobjValues.ActionQuery = Session("bQuery")

Call insPreDP018P()

mobjValues.sCodisplPage = "dp018p"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP018P.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 17:04 $|$$Author: Nvaplat61 $"    

//%InsChangeBranch_Rei: Habilita la opción de Suma para reaseguro si se indica ramo
//---------------------------------------------------------------------------------
function InsChangeBranch_Rei(nBranch){
//---------------------------------------------------------------------------------
	with (self.document.forms[0]){
		OptAddreini[0].disabled = nBranch == '0';
		OptAddreini[1].disabled = OptAddreini[0].disabled;
		OptAddreini[2].disabled = OptAddreini[0].disabled;
		
		if (OptAddreini[0].disabled)
			OptAddreini[2].checked = true;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP018P" ACTION="valCoverSeq.aspx?sMode=1">
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=14349><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCur_Allow_Gen", 1, CStr(mclsLife_cover.nCurrency), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 1))
%>
			</TD>
			<TD WIDTH=2%>&nbsp;</TD>
            <TD><LABEL ID=14348><%= GetLocalResourceObject("valBillitemCaption") %></LABEL></TD>
			<TD><%With mobjValues.Parameters
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valBillitem", "tabTab_bill_i", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsLife_cover.nBill_item), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valBillitemToolTip"),  , 2))
%>
			</TD>
        </TR>
	    <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeRetarifCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeRetarif", "table5559", 1, CStr(mclsLife_cover.nRetarif),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRetarifToolTip"),  , 3)%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkCover_use", GetLocalResourceObject("chkCover_useCaption"), mclsLife_cover.sCoveruse,  ,  , mclsTab_Lifcov.sCoveruse <> "2", 4, GetLocalResourceObject("chkCover_useToolTip"))%></TD>
		</TR>
        <TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tctCondSVSCaption") %></LABEL></TD>
		    <TD><%=mobjValues.TextControl("tctCondSVS", 30, mclsLife_cover.sCondSVS,  , GetLocalResourceObject("tctCondSVSToolTip"),  ,  ,  ,  ,  , 5)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100097><A NAME="Ramos para interfaces"><%= GetLocalResourceObject("AnchorRamos para interfacesCaption") %></A></LABEL></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD COLSPAN="3"></TD>
        </TR>
        <TR>
			<TD><LABEL ID=14350><%= GetLocalResourceObject("cbeBranch_ledCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeBranch_led", "table75", 1, CStr(mclsLife_cover.nBranch_led),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_ledToolTip"),  , 6))
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkControl", GetLocalResourceObject("chkControlCaption"), mclsLife_cover.sControl,  ,  ,  , 9, GetLocalResourceObject("chkControlToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=14351><%= GetLocalResourceObject("cbeBranch_reiCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = True
Response.Write(mobjValues.PossiblesValues("cbeBranch_rei", "table5000", 1, CStr(mclsLife_cover.nBranch_rei),  ,  ,  ,  ,  , "InsChangeBranch_Rei(this.value);",  ,  , GetLocalResourceObject("cbeBranch_reiToolTip"),  , 7))
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkSurv", GetLocalResourceObject("chkSurvCaption"), mclsLife_cover.sSurv,  ,  ,  , 10, GetLocalResourceObject("chkSurvToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranch_estCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeBranch_est", "table71", 1, CStr(mclsLife_cover.nBranch_est),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_estToolTip"),  , 8))
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkCalrein", GetLocalResourceObject("chkCalreinCaption"), mclsLife_cover.sCalRein,  ,  ,  , 11, GetLocalResourceObject("chkCalreinToolTip"))%></TD>
        </TR>
        <TR>
      		<TD><LABEL ID=14353><%= GetLocalResourceObject("cbeBranch_genCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBranch_gen", "table634", 1, CStr(mclsLife_cover.nBranch_gen),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBranch_genToolTip"),  , 9)%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkDepend", GetLocalResourceObject("chkDependCaption"), mclsLife_cover.sDepend,  ,  ,  , 12, GetLocalResourceObject("chkDependToolTip"))%></TD>
		</TR>
        <TR>
      		<TD COLSPAN="5"><%=mobjValues.CheckControl("chkReinOrigCond", GetLocalResourceObject("chkReinOrigCondCaption"), mclsLife_cover.sReinorigcond, CStr(1),  ,  , 13, GetLocalResourceObject("chkReinOrigCondToolTip"))%></TD>
		</TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="6" CLASS="HighLighted"><LABEL ID=0><A NAME="Suma para"><%= GetLocalResourceObject("AnchorSuma paraCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="6" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><B><LABEL ID=14682><%= GetLocalResourceObject("AnchorCaption") %></LABEL></B></TD>
	        <TD><%=mobjValues.OptionControl(100103, "OptAddsuini", GetLocalResourceObject("OptAddsuini_CStr1Caption"), mclsLife_cover.sAddSuini, CStr(1),  ,  , 13, GetLocalResourceObject("OptAddsuini_CStr1ToolTip"))%></TD>
			<TD><B><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></B></TD>
	        <TD><%=mobjValues.OptionControl(100103, "OptAddreini", GetLocalResourceObject("OptAddreini_CStr1Caption"), mclsLife_cover.sAddreini, CStr(1),  , mclsLife_cover.nBranch_rei = eRemoteDB.Constants.intNull, 16, GetLocalResourceObject("OptAddreini_CStr1ToolTip"))%></TD>
			<TD><B><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></B></TD>
	        <TD><%=mobjValues.OptionControl(100103, "OptAddtaxin", GetLocalResourceObject("OptAddtaxin_CStr1Caption"), mclsLife_cover.sAddtaxin, CStr(1),  ,  , 19, GetLocalResourceObject("OptAddtaxin_CStr1ToolTip"))%></TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100105, "OptAddsuini", GetLocalResourceObject("OptAddsuini_CStr3Caption"), 4 - mobjValues.StringToType(mclsLife_cover.sAddSuini, eFunctions.Values.eTypeData.etdDouble), CStr(3),  ,  , 14, GetLocalResourceObject("OptAddsuini_CStr3ToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100105, "OptAddreini", GetLocalResourceObject("OptAddreini_CStr3Caption"), 4 - mobjValues.StringToType(mclsLife_cover.sAddreini, eFunctions.Values.eTypeData.etdDouble), CStr(3),  , mclsLife_cover.nBranch_rei = eRemoteDB.Constants.intNull, 17, GetLocalResourceObject("OptAddreini_CStr3ToolTip"))%></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100105, "OptAddtaxin", GetLocalResourceObject("OptAddtaxin_CStr3Caption"), 4 - mobjValues.StringToType(mclsLife_cover.sAddtaxin, eFunctions.Values.eTypeData.etdDouble), CStr(3),  ,  , 20, GetLocalResourceObject("OptAddtaxin_CStr3ToolTip"))%></TD>
        </TR>
        <TR>
			<TD>&nbsp;</TD>
	        <TD><%=mobjValues.OptionControl(100107, "OptAddsuini", GetLocalResourceObject("OptAddsuini_CStr2Caption"), 3 - mobjValues.StringToType(mclsLife_cover.sAddSuini, eFunctions.Values.eTypeData.etdDouble), CStr(2),  ,  , 15, GetLocalResourceObject("OptAddsuini_CStr2ToolTip"))%></TD>
			<TD>&nbsp;</TD>
	        <TD><%=mobjValues.OptionControl(100107, "OptAddreini", GetLocalResourceObject("OptAddreini_CStr2Caption"), 3 - mobjValues.StringToType(mclsLife_cover.sAddreini, eFunctions.Values.eTypeData.etdDouble), CStr(2),  , mclsLife_cover.nBranch_rei = eRemoteDB.Constants.intNull, 18, GetLocalResourceObject("OptAddreini_CStr2ToolTip"))%></TD>
			<TD>&nbsp;</TD>
	        <TD><%=mobjValues.OptionControl(100107, "OptAddtaxin", GetLocalResourceObject("OptAddtaxin_CStr2Caption"), 3 - mobjValues.StringToType(mclsLife_cover.sAddtaxin, eFunctions.Values.eTypeData.etdDouble), CStr(2),  ,  , 21, GetLocalResourceObject("OptAddtaxin_CStr2ToolTip"))%></TD>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100098><A NAME="Notas"><%= GetLocalResourceObject("txtNoteCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
			<TD><%=mobjValues.TextAreaControl("txtNote", 2, 60, CStr(mstrNoteDescript),  , GetLocalResourceObject("txtNoteToolTip"),  , True, 23)%></TD>
			<TD><%=mobjValues.ButtonNotes("SCA2-Y", mclsLife_cover.nNotenum, False, mobjValues.ActionQuery,  ,  ,  , 24)%></TD>
		</TR>
    </TABLE>
    <%=mobjValues.BeginPageButton%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsLife_cover = Nothing
mclsTab_Lifcov = Nothing
%>





