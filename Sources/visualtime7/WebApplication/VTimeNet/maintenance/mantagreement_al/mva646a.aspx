<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'-Objeto para el manejo de los datos
Dim mclsAgreement_al As eBranches.Agreement_al


Private Sub InsPreMVA646()
	Dim lblnDisable As Boolean
	mclsAgreement_al.Find(Session("nAgreement"))
	
Response.Write("" & vbCrLf)
Response.Write("<FORM METHOD=""POST"" NAME=""MVA646A"" ACTION=""ValAgreementSeq.aspx?nMainAction=")


Response.Write(Request.QueryString.Item("nMainAction"))


Response.Write(""">")


Response.Write(mobjValues.ShowWindowsName("MVA646A"))


Response.Write("" & vbCrLf)
Response.Write("    <BR>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=""0"">" & GetLocalResourceObject("tctDescriptCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctDescript", 30, mclsAgreement_al.sDescript,  , GetLocalResourceObject("tctDescriptToolTip")))


Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD>&nbsp;</td>" & vbCrLf)
Response.Write("            <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(mclsAgreement_al.dStartdate),  , GetLocalResourceObject("tcdEffecdateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeStatusCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	If mclsAgreement_al.sStatregt = "2" Then
		lblnDisable = True
	Else
		mobjValues.BlankPosition = False
		mobjValues.TypeList = CShort("2")
		mobjValues.List = "2"
		lblnDisable = False
	End If
	Response.Write(mobjValues.PossiblesValues("cbeStatus", "Table26", eFunctions.Values.eValuesType.clngComboType, mclsAgreement_al.sStatregt,  ,  ,  ,  ,  ,  , CBool(lblnDisable),  , GetLocalResourceObject("cbeStatusToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </td>" & vbCrLf)
Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=""0"">" & GetLocalResourceObject("tcdNulldateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	Response.Write(mobjValues.HiddenControl("hddNulldate", mobjValues.TypeToString(mclsAgreement_al.dNulldate, eFunctions.Values.eTypeData.etdDate)))
	Response.Write(mobjValues.DateControl("tcdNulldate", CStr(mclsAgreement_al.dNulldate),  , GetLocalResourceObject("tcdNulldateToolTip"), False,  ,  ,  , True))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        
        
        
        
        
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=""0"">" & GetLocalResourceObject("cbeAgree_TypeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

	
        Response.Write(mobjValues.PossiblesValues("cbeAgree_Type", "Table680", eFunctions.Values.eValuesType.clngComboType, mclsAgreement_al.nAgree_Type, , , , , , "insChangeAgree_Type(this);", False, , GetLocalResourceObject("cbeAgree_TypeToolTip")))
	
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD></TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""10%"">&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=""0"">" & GetLocalResourceObject("valIntermedCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")
        	
        Response.Write(mobjValues.PossiblesValues("valIntermed", "Intermedia", eFunctions.Values.eValuesType.clngWindowType, mclsAgreement_al.nIntermed, , , , , , , Not mclsAgreement_al.nAgree_Type = 1, 10, GetLocalResourceObject("valIntermedToolTip")))
	
        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        
        
        
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><label ID=""0"">" & GetLocalResourceObject("AnchorCaption") & "</label></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optLevelInt", GetLocalResourceObject("optLevelInt_1Caption"), mclsAgreement_al.sLevelint, "1"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optLevelInt", GetLocalResourceObject("optLevelInt_2Caption"), CStr(3 - CShort(mclsAgreement_al.sLevelint)), "2"))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("</FORM> ")

	
	mclsAgreement_al = Nothing
	mobjValues = Nothing
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsAgreement_al = New eBranches.Agreement_al

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA646A"
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

    //%insChangeAgree_Type: Actualiza propiedad del Intermediario
    //---------------------------------------------------------------------------
    function insChangeAgree_Type(Field) {
        //---------------------------------------------------------------------------
        with (self.document.forms[0]) {

            if (Field.value == '1') {

                UpdateDiv('valIntermedDesc', '');
                valIntermed.disabled = false;
                btnvalIntermed.disabled = false;
                valIntermed.value = '';
            }
            else {
                UpdateDiv('valIntermedDesc', '');
                valIntermed.disabled = true;
                btnvalIntermed.disabled = true;
                valIntermed.value = '';
            }

        }
    }
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVA646A", "MVA646.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Call InsPreMVA646()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
</BODY>
</HTML>




