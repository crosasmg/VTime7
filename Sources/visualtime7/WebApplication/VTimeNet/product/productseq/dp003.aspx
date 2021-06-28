<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mclsProduct As eProduct.Product
    Dim mobjMenu As eFunctions.Menues


    '% insPreDP003: se controla la carga de la página
    '--------------------------------------------------------------------------------------------
    Sub insPreDP003()
        '--------------------------------------------------------------------------------------------
        If Not mclsProduct.insPreDP003(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("bQuery")) Then
            mclsProduct.nTariff = 1
        End If
    End Sub

</script>
<%Response.Expires = -1
    mobjValues = New eFunctions.Values
    mclsProduct = New eProduct.Product
    mobjMenu = New eFunctions.Menues
    Call insPreDP003()
    mobjValues.ActionQuery = Session("bQuery")
    mobjValues.sCodisplPage = "DP003"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%With Response
            Response.Write(mobjValues.StyleSheet())
            Response.Write(mobjValues.WindowsTitle("DP003"))
            Response.Write(mobjMenu.setZone(2, "DP003", "DP003.aspx"))
        End With
        mobjMenu = Nothing
    %>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 3 $|$$Date: 17/02/04 11:53 $"
        //% insLockControl: se realiza el bloqueo de los campos dependientes
        //-------------------------------------------------------------------------------------------
        function insLockControl(Field) {
            //-------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                cbeReinHeap.disabled = (Field.value == 4) ? true : false;
                cbeReinHeap.value = (Field.value == 4) ? 3 : cbeReinHeap.value;
            }
        }
        //% insShowHeader: Recarga los campos del encabezado
        //---------------------------------------------------------------------------------------
        function insShowHeader() {
            //---------------------------------------------------------------------------------------
            var lblnAgain = true
            if (typeof (top.fraHeader.document) != 'undefined')
                if (typeof (top.fraHeader.document.forms[0]) != 'undefined')
                    if (typeof (top.fraHeader.document.forms[0].valProduct) != 'undefined') {
                        top.fraHeader.document.forms[0].tcdEffecdate.value = '<%=Session("dEffecdate")%>'
                        top.fraHeader.document.forms[0].cbeProdType.value = '<%=Session("sBrancht")%>'
                        top.fraHeader.document.forms[0].cbeBranch.value = '<%=Session("nBranch")%>'
                        top.fraHeader.document.forms[0].valProduct.value = '<%=Session("nProduct")%>'
                        lblnAgain = false;
                    }
            if (lblnAgain)
                setTimeout("insShowHeader", 50)
        }
        //% insLockControl: se realiza el bloqueo de los campos dependientes
        //-------------------------------------------------------------------------------------------
        function insLockcbeAssociatedBranch(Field) {
            //-------------------------------------------------------------------------------------------
            with (self.document.forms[0]) {
                cbeAssociatedBranch.disabled = (Field.checked) ? false : true;
                cbeAssociatedBranch.value = (Field.checked) ? cbeAssociatedBranch.value : "";

            }
        }
        insShowHeader();
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP003" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%=mobjValues.ShowWindowsName("DP003")%>
    <br>
    <TABLE WIDTH="100%">
        <tr>
            <td><label id="14268"><%= GetLocalResourceObject("tctDescriptCaption") %></label></td>
            <td><%=mobjValues.TextControl("tctDescript", 30, mclsProduct.sDescript,  , GetLocalResourceObject("tctDescriptToolTip"))%></td>
            <td><label id="14274"><%= GetLocalResourceObject("tctShortDesCaption") %></label></td>
            <td><%=mobjValues.TextControl("tctShortDes", 12, mclsProduct.sShort_des,  , GetLocalResourceObject("tctShortDesToolTip"))%></td>
            <td><label id="LABEL1"><%= GetLocalResourceObject("tctPreffix") %></label></td>
            <td><%=mobjValues.TextControl("tctPreffix", 20, mclsProduct.sPreffix,  , GetLocalResourceObject("tctPreffixToolTip"))%></td>
        </tr>
        <tr>
            <td><label id="14268"><%= GetLocalResourceObject("tctCondSVSCaption") %></label></td>
            <td><%=mobjValues.TextControl("tctCondSVS", 30, mclsProduct.sCondSVS,  , GetLocalResourceObject("tctCondSVSToolTip"))%></td>
            <td colspan="2">&nbsp;</td>
            <td><label id="14267"><%= GetLocalResourceObject("tcdVigDateCaption") %></label></td>
            <td><%=mobjValues.DateControl("tcdVigDate", CStr(mclsProduct.dEffecdate),  , GetLocalResourceObject("tcdVigDateToolTip"),  ,  ,  ,  , True)%></td>
        </tr>
        <tr>
            <td><label id="Label5"><%= GetLocalResourceObject("tctResolutionSBSCaption") %></label></td>
            <td><%=mobjValues.TextControl("tctResolutionSBS", 100, mclsProduct.sResolutionSBS,  , GetLocalResourceObject("tctResolutionSBSToolTip"))%></td>
       </tr>
    </TABLE>
    <BR>



    <TABLE WIDTH="100%">
        <tr>
            <td colspan="3" class="HighLighted"><label id="41236"><%= GetLocalResourceObject("AnchorCaption") %></label></td>
            <td>&nbsp;</td>
            <td colspan="2" class="HighLighted"><label id="41237"><%= GetLocalResourceObject("Anchor2Caption") %></label></td>
        </tr>
        <tr>
            <td colspan="3" class="HorLine"></td>
            <td></td>
            <td colspan="2" class="HorLine"></td>
        </tr>
        <tr>
            <td><%=mobjValues.CheckControl("chkSimulator", GetLocalResourceObject("chkSimulatorCaption"), CStr(mclsProduct.sRealind), "1",  ,  ,  , GetLocalResourceObject("chkSimulatorToolTip"))%></td>
            <td><label id="14272"><%= GetLocalResourceObject("tcnReferenceCaption") %></label></td>
            <td><%=mobjValues.NumericControl("tcnReference", 10, CStr(mclsProduct.nReference),  , GetLocalResourceObject("tcnReferenceToolTip"))%></td>
            <td>&nbsp;</td>
            <td><%=mobjValues.CheckControl("chkIndividual", GetLocalResourceObject("chkIndividualCaption"), mclsProduct.sIndivind, "1",  ,  ,  , GetLocalResourceObject("chkIndividualToolTip"))%></td>
        </tr>
        <tr>
            <td><label id="14277"><%= GetLocalResourceObject("tcnVersionCaption") %></label></td>
            <td colspan="2"><%=mobjValues.NumericControl("tcnVersion", 5, CStr(mclsProduct.nTariff),  , GetLocalResourceObject("tcnVersionToolTip"),  , 0,  ,  ,  ,  , True)%></td>
            <td width="5%">&nbsp;</td>
            <td><%=mobjValues.CheckControl("chkGroups", GetLocalResourceObject("chkGroupsCaption"), mclsProduct.sGroupind, "1",  ,  ,  , GetLocalResourceObject("chkGroupsToolTip"))%></td>
        </tr>
        <tr>
            <td><label id="14275"><%= GetLocalResourceObject("cbeStatusCaption") %></label></td>
            <td colspan="2"><%=mobjValues.PossiblesValues("cbeStatus", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct.sStatregt),  ,  ,  ,  ,  ,  , mclsProduct.sStatregt - 1,  , GetLocalResourceObject("cbeStatusToolTip"))%></td>
            <td>&nbsp;</td>
            <td><%=mobjValues.CheckControl("chkMulti", GetLocalResourceObject("chkMultiCaption"), mclsProduct.sMultiind, "1",  ,  ,  , GetLocalResourceObject("chkMultiToolTip"))%></td>
        </tr>
    </TABLE>

    <TABLE WIDTH="100%">
        <TR>
            <td colspan="2" class="HighLighted"><label id="41238"><%= GetLocalResourceObject("Anchor3Caption") %></label></td>
            <td>&nbsp;</td>
            <td colspan="2" class="HighLighted"><label id="41239"><%= GetLocalResourceObject("Anchor4Caption") %></label></td>
        </TR>
        <TR>
            <td colspan="2" class="HorLine"></td>
            <td></td>
            <td colspan="2" class="HorLine"></td>
        </TR>
        <TR>
            <td><label id="14276"><%= GetLocalResourceObject("cbeTypeHeapCaption") %></label></td>
            <td><%=mobjValues.PossiblesValues("cbeTypeHeap", "Table79", eFunctions.Values.eValuesType.clngComboType, mclsProduct.sCumultyp,  ,  ,  ,  ,  , "insLockControl(this)",  ,  , GetLocalResourceObject("cbeTypeHeapToolTip"))%></td>
            <td width="5%">&nbsp;</td>
            <td><label id="14270"><%= GetLocalResourceObject("valDeclarativeCaption") %></label></td>
            <td><%=mobjValues.PossiblesValues("valDeclarative", "winDeclarative", eFunctions.Values.eValuesType.clngWindowType, mclsProduct.sWin_declar,  ,  ,  ,  ,  ,  ,  , 8, GetLocalResourceObject("valDeclarativeToolTip"), eFunctions.Values.eTypeCode.eString)%></td>
        </TR>
        <TR>
            <td><label id="14273"><%= GetLocalResourceObject("cbeReinHeapCaption") %></label></td>
            <td><%=mobjValues.PossiblesValues("cbeReinHeap", "Table90", eFunctions.Values.eValuesType.clngComboType, mclsProduct.sCumreint,  ,  ,  ,  ,  ,  , mclsProduct.bCumreintDisabled,  , GetLocalResourceObject("cbeReinHeapToolTip"))%></td>
            <td>&nbsp;</td>
            <td><label id="14269"><%= GetLocalResourceObject("tcnCurrencyQCaption") %></label></td>
            <td><%=mobjValues.NumericControl("tcnCurrencyQ", 5, CStr(mclsProduct.nQmaxcurr),  , GetLocalResourceObject("tcnCurrencyQToolTip"),  , 0)%></td>
        </TR>
        <TR>
            <td colspan="2" class="HighLighted"><label id="Label3"><%= GetLocalResourceObject("Anchor5Caption") %></label></td>
            <td> </td>
            <td ><label><%=GetLocalResourceObject("nTypeAccount_Caption") %></label></td>
            <td><%=mobjValues.PossiblesValues(FieldName:="nTypeAccount", TableName:="Table7200", ValuesType:=eFunctions.Values.eValuesType.clngComboType, DefValue:=mclsProduct.nTypeAccount, NeedParam:=False, ComboSize:=1, Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("nTypeAccount_ToolTip"), CodeType:=eFunctions.Values.eTypeCode.eNumeric, ShowDescript:=True, bAllowInvalid:=False) %></td>
        </TR>
        <tr>
        <td colspan="2" class="HorLine">
            </td>
        </tr>
        <tr>
             <td>
               <label id="Label4">
                    <%= GetLocalResourceObject("tcnModuleMinCaption") %></label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnModuleMin", 1, IIf(mclsProduct.nModuleMin < 0, 0, mclsProduct.nModuleMin)  , , GetLocalResourceObject("tcnModuleMinToolTip"))%>
            </td>
            <td>
                &nbsp;
            </td>
              <td colspan="2">
                <%=mobjValues.CheckControl("chkAutomaticBill", GetLocalResourceObject("chkAutomaticBillCaption"), mclsProduct.sAutomaticBill, "1", , , , GetLocalResourceObject("chkAutomaticBillToolTip"))%>
            </td>
         </tr>
         <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
             <td colspan="2">
                <%=mobjValues.CheckControl("chkNumprop", GetLocalResourceObject("chkNumpropCaption"), mclsProduct.sNumprop, "1",  ,  ,  , GetLocalResourceObject("chkNumpropToolTip"))%>
            </td>
        </tr>
      
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkAuto_susc", GetLocalResourceObject("chkAuto_suscCaption"), mclsProduct.sAuto_susc, "1",  ,  ,  , GetLocalResourceObject("chkAuto_suscToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkMassive", GetLocalResourceObject("chkMassiveCaption"), mclsProduct.sMassive, "1",  ,  ,  , GetLocalResourceObject("chkMassiveToolTip"))%>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkRatingServiceUsing", GetLocalResourceObject("chkRatingServiceUsingCaption"), mclsProduct.sRatingServiceUsing, "1",  ,  ,  , GetLocalResourceObject("chkRatingServiceUsingToolTip"))%>
            </td>
        </tr>

        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <%=mobjValues.CheckControl("chkAssociated_Policy_Required", GetLocalResourceObject("chkAssociated_Policy_RequiredCaption"), mclsProduct.sAssociated_Policy_Required, "1",  "insLockcbeAssociatedBranch(this)" ,  ,  , GetLocalResourceObject("chkAssociated_Policy_RequiredToolTip"))%>
            </td>
        </tr>


        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <label id="Label2">
                    <%= GetLocalResourceObject("cbeAssociatedBranchCaption") %>
                </label>
            </td>
            <td>
                <%=mobjValues.PossiblesValues("cbeAssociatedBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, mclsProduct.nAssociatedBranch,  ,  ,  ,  ,  ,  ,mclsProduct.sAssociated_Policy_Required <> "1" ,  , GetLocalResourceObject("cbeAssociatedBranchToolTip"))%>
            </td>
        </tr>
    </TABLE>
    </form>
</body>
</html>
<%
    With Response
	.Write("<SCRIPT>")
        .Write("top.fraHeader.UpdateDiv(""valProductDesc"",'" & mclsProduct.sDescript & "','Normal');")
        .Write("top.fraHeader.document.forms[0].tcdEffecdate.value='" & Session("dEffecdate") & "';")
	.Write("</SCRIPT>")
    End With
    mclsProduct = Nothing
%>
