<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 10.58.59
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjMenu As eFunctions.Menues
Dim lstrAction As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de la tabla Fire    
Dim mclsFire As ePolicy.Fire

Dim mclsPolicy As ePolicy.Policy


'% insPreIN010: hace la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------------------------
Private Sub insPreIN010()
	'----------------------------------------------------------------------------------------------
	Call mclsPolicy.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"))
	Call mclsFire.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("IN010")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 10.58.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "IN010"
mclsFire = New ePolicy.Fire
mclsPolicy = New ePolicy.Policy

'-  Cuando se llama desde la secuencia de ordenes de servicio, se carga la pagina en modo consulta
If CStr(Session("CallSequence")) = "Prof_ord" Then
	lstrAction = "/VTimeNet/Prof_ord/Prof_ordseq/valProf_ordseq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction")
	Session("bQuery") = True
Else
	lstrAction = "valPolicySeq.aspx?Action=Update"
End If
mobjValues.ActionQuery = Session("bQuery")
Session("bQuery") = False
Call insPreIN010()

%>


<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 10.58.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
With Response
	.Write(mobjValues.styleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>

<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:49 $"
    
//% ChangeValues: función para el manejo del evento OnChange de los campos
//-------------------------------------------------------------------------------------------
function ChangeValues(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {

        if (typeof (cboDetailArt) != 'undefined') {
            cboDetailArt.Parameters.Param1.sValue=Field.value;
            cboDetailArt.disabled=(Field.value=='')?true:false;
            self.document.btncboDetailArt.disabled = (Field.value == '') ? true : false;
            if (hdnDetailArt.value != cboArticle.value) {
                cboDetailArt.value = '';
                UpdateDiv("cboDetailArtDesc", "");
            }
            hdnDetailArt.value = cboArticle.value;
        }
	}
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmFire" ACTION="<%=lstrAction%>">
    <%Response.Write(mobjValues.ShowWindowsName("IN010", Request.QueryString.Item("sWindowDescript")))%>
    <P ALIGN="CENTER">
	<LABEL ID=41025><A HREF="#Cob. terremoto"><%= GetLocalResourceObject("AnchorCob. terremotoCaption") %></A></LABEL><LABEL ID=41026> | </LABEL>
	<LABEL ID=41027><A HREF="#Declaraciones"><%= GetLocalResourceObject("AnchorDeclaracionesCaption") %></A></LABEL>
    <BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41028><A NAME="Identificación del riesgo"><%= GetLocalResourceObject("AnchorIdentificación del riesgoCaption") %></A></LABEL></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41029><A NAME="Cob. combustión espontánea"><%= GetLocalResourceObject("AnchorCob. combustión espontáneaCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD></TD>
			<TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13263><%= GetLocalResourceObject("cboArticleCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cboArticle", "Table118", eFunctions.Values.eValuesType.clngWindowType, mobjValues.TypeToString(mclsFire.nArticle, eFunctions.Values.eTypeData.etdDouble), False, , , , , "ChangeValues(this)", , 4, GetLocalResourceObject("cboArticleToolTip"))%>
                <%=mobjValues.HiddenControl("hdnDetailArt", mobjValues.TypeToString(mclsFire.nArticle, eFunctions.Values.eTypeData.etdDouble))%>
            </TD>
		    <TD>&nbsp;</TD>
            <TD><LABEL ID=13273><%= GetLocalResourceObject("cbeSpCombTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSpCombType", "Table7040", eFunctions.Values.eValuesType.clngComboType, CStr(mclsFire.nSpCombType), False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSpCombTypeToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13267><%= GetLocalResourceObject("cboDetailArtCaption") %></LABEL></TD>
            <TD><%
With mobjValues
                        .Parameters.Add("nArticle", mclsFire.nArticle, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        Response.Write(mobjValues.PossiblesValues("cboDetailArt", "tabTab_In_Bus", 2, mobjValues.TypeToString(mclsFire.nDetailArt, eFunctions.Values.eTypeData.etdDouble), True, , , , , , False, 4, GetLocalResourceObject("cboDetailArtToolTip")))
End With
%>
			</TD>
		    <TD>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41030><A NAME="Cob. Huracán"><%= GetLocalResourceObject("AnchorCob. HuracánCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13265><%= GetLocalResourceObject("cbeConstCatCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeConstCat", "Table233", 1, CStr(mclsFire.nConstCat),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeConstCatToolTip"))%></TD>
            <TD COLSPAN="1">&nbsp</TD>
            <TD><LABEL ID=13272><%= GetLocalResourceObject("cbeSideCloseTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSideCloseType", "Table7037", 1, CStr(mclsFire.nSideCloseType),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSideCloseTypeToolTip"))%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=13268><%= GetLocalResourceObject("tcnFloor_quanCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnFloor_quan", 4, mobjValues.TypeToString(mclsFire.nFloor_quan, eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnFloor_quanToolTip"))%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41031><A NAME="Cob. lucro cesante"><%= GetLocalResourceObject("AnchorCob. lucro cesanteCaption") %></A></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41032><A NAME="Cob. granizo"><%= GetLocalResourceObject("AnchorCob. granizoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD></TD>
			<TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13269><%= GetLocalResourceObject("tcnIndPeriodCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnIndPeriod", 3, mobjValues.TypeToString(mclsFire.nIndPeriod, eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("tcnIndPeriodToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=13270><%= GetLocalResourceObject("cbeRoofTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeRoofType", "Table7038", 1, mobjValues.TypeToString(mclsFire.nRoofType, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRoofTypeToolTip"))%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41033><A NAME="Cob. terremoto"><%= GetLocalResourceObject("AnchorCob. terremoto2Caption") %></A></LABEL></TD>
			<TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=41034><A NAME="Declaraciones"><%= GetLocalResourceObject("AnchorDeclaraciones2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
		    <TD></TD>
			<TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13264><%= GetLocalResourceObject("cbeBuildTypeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBuildType", "Table7039", 1, CStr(mclsFire.nBuildType),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBuildTypeToolTip"))%></TD>
            <TD>&nbsp;</TD>
			<%If (mclsPolicy.sDeclari <> vbNullString) And (mclsPolicy.sDeclari = "1") Then%>
				<TD><LABEL ID=13266><%= GetLocalResourceObject("tcnDep_premCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnDep_prem", 4, CStr(mclsFire.nDep_prem),  , GetLocalResourceObject("tcnDep_premToolTip"),  , 2,  ,  ,  ,  , False)%></TD>
			<%Else%>
				<TD><LABEL ID=13266><%= GetLocalResourceObject("tcnDep_premCaption") %></LABEL></TD>
				<TD><%=mobjValues.NumericControl("tcnDep_prem", 4, CStr(mclsFire.nDep_prem),  , GetLocalResourceObject("tcnDep_premToolTip"),  , 2,  ,  ,  ,  , True)%></TD>
			<%End If%>
			</TR>
			<TR>
            <TD><LABEL ID=13271><%= GetLocalResourceObject("cbeSeismicZoneCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSeismicZone", "Table7047", 1, CStr(mclsFire.nSeismicZone),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSeismicZoneToolTip"))%></TD>
            <TD>&nbsp;</TD>
			<%If (mclsPolicy.sDeclari <> vbNullString) And (mclsPolicy.sDeclari = "1") Then%>
				<TD><LABEL ID=13261><%= GetLocalResourceObject("cbeDecla_TypeCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeDecla_Type", "Table235", 1, mclsFire.sDecla_type,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeDecla_TypeToolTip"))%></TD>
			</TR>
			<TR>
				<TD COLSPAN="3">&nbsp;</TD>
				<TD><LABEL ID=13262><%= GetLocalResourceObject("cbeDecla_FreqCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeDecla_Freq", "Table108", 1, mclsFire.sDecla_freq,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeDecla_FreqToolTip"))%></TD>
			</TR>
			<%Else%>
				<TD><LABEL ID=13261><%= GetLocalResourceObject("cbeDecla_TypeCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeDecla_Type", "Table235", 1, mclsFire.sDecla_type,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeDecla_TypeToolTip"))%></TD>
			</TR>
			<TR>
				<TD COLSPAN="3">&nbsp;</TD>
				<TD><LABEL ID=13262><%= GetLocalResourceObject("cbeDecla_FreqCaption") %></LABEL></TD>
				<TD><%=mobjValues.PossiblesValues("cbeDecla_Freq", "Table108", 1, mclsFire.sDecla_freq,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeDecla_FreqToolTip"))%></TD>
			</TR>
			<%End If%>
    </TABLE>
	<%=mobjValues.BeginPageButton%>	  
    <%
mobjValues = Nothing
mclsFire = Nothing
mclsPolicy = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 10.58.59
Call mobjNetFrameWork.FinishPage("IN010")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




