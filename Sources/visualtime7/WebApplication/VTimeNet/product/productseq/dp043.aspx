<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGridS As eFunctions.Grid
    Dim mobjGridP As eFunctions.Grid


    '% insPreDP043: Esta función permite realizar la lectura de la tabla para el grid.
    '---------------------------------------------------------------------------------------------
    Private Sub insPreDP043()
        '---------------------------------------------------------------------------------------------
        Dim lclsPayinsu_prod As eProduct.Durpay_prod
        Dim lclsDurinsu_prod As eProduct.Durinsu_prod
        Dim lclsProdlifeseq As eProduct.ProdLifeSeq
        Dim lclsProduct_li As eProduct.Product
        Dim lblnFindDurinsu As String
        Dim lblnFindPayinsu As String
        Dim lclsErrors As eFunctions.Errors
        Dim lstrValue As Object
        lblnFindDurinsu = "false"
        lblnFindPayinsu = "false"
        lclsProdlifeseq = New eProduct.ProdLifeSeq
        '+ Obtiene los datos de la ventana.
        With lclsProdlifeseq


            .insPreDP043(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Request.QueryString.Item("ReloadAction"), mobjValues.StringToType(Request.QueryString.Item("nProdclas"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sMorCapii"), mobjValues.StringToType(Request.QueryString.Item("nMinRent"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sRoutine"), mobjValues.StringToType(Request.QueryString.Item("nMaxRent"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sAssociai"), mobjValues.StringToType(Request.QueryString.Item("nTypdurins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypdurpay"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sRoutinsu"), Request.QueryString.Item("sAssoTotal"), Request.QueryString.Item("sPremiumtype"), Request.QueryString.Item("sIdurvari"), Request.QueryString.Item("sPdurvari"), mobjValues.StringToType(Request.QueryString.Item("bWithInformation"), Values.eTypeData.etdBoolean), Request.QueryString.Item("nBeforeIdurvari"), Request.QueryString.Item("sRoutpay"), Request.QueryString.Item("sApv"), Request.QueryString.Item("sNo_Holidays"))

            mobjGridP.sEditRecordParam = "nTypDurPay=" & .mclsProduct_li.nTypdurpay & "&nTypDurins=" & .mclsProduct_li.nTypdurins
            mobjGridS.sEditRecordParam = "nTypDurins=" & .mclsProduct_li.nTypdurins
            Response.Write(mobjValues.HiddenControl("hddbWithInformation", mobjValues.StringToType(.bWithInformation, eFunctions.Values.eTypeData.etdBoolean)))

            If .bFindDurinsu Then
                lblnFindDurinsu = "true"
            End If
            If .bFindPayinsu Then
                lblnFindPayinsu = "true"
            End If

            Response.Write("" & vbCrLf)
            Response.Write("    <P ALIGN=""Center"">    " & vbCrLf)
            Response.Write("    <LABEL ID=41422><A HREF=""#Seguro"">" & GetLocalResourceObject("AnchorSeguroCaption") & "</A></LABEL>" & vbCrLf)
            Response.Write("    <LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL>" & vbCrLf)
            Response.Write("    <LABEL ID=0><A HREF=""#Pagos"">" & GetLocalResourceObject("AnchorPagosCaption") & "</A></LABEL>" & vbCrLf)
            Response.Write("    </P>" & vbCrLf)
            Response.Write("    <TABLE WIDTH=""100%"" border=""0"">" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=14871>" & GetLocalResourceObject("cbeProdclasCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeProdclas", "Table124", 1, CStr(.mclsProduct_li.nProdClas),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeProdclasToolTip"),  , 1))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=14875>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeCurrency", "Table11", 1, CStr(.mclsProduct_li.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 2))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR> " & vbCrLf)
            Response.Write("    </TABLE>" & vbCrLf)
            Response.Write("    <TABLE WIDTH=""100%"" border=""0"">" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""3"">")


            Response.Write(mobjValues.CheckControl("chkMorCapii", GetLocalResourceObject("chkMorCapiiCaption"), .mclsProduct_li.sMorcapii, "1",  ,  , 3))


            Response.Write("</TD>  " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.CheckControl("chkApv", GetLocalResourceObject("chkApvCaption"), .mclsProduct_li.sApv, "1",  ,  , 3))


            Response.Write("</TD>  " & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("<TD>  " & vbCrLf)

            'Nuevo Campo "No Validar días festivos"
            Response.Write(mobjValues.CheckControl("chkNo_Holidays", GetLocalResourceObject("chkNo_HolidaysCaption"), .mclsProduct_li.sNo_Holidays, "1"))

            Response.Write("</TD>  " & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41430>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("            <TD></TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("sRoutineCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.TextControl("sRoutine", 12, .mclsProduct_li.sRoutine_C,  , GetLocalResourceObject("sRoutineToolTip"),  ,  ,  ,  ,  , 4))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnMinRentCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.NumericControl("tcnMinRent", 18, CStr(.mclsProduct_li.nMinrent),  , GetLocalResourceObject("tcnMinRentToolTip"),  , 6,  ,  ,  ,  ,  , 5))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnMaxRentCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.NumericControl("tcnMaxRent", 18, CStr(.mclsProduct_li.nMaxrent),  , GetLocalResourceObject("tcnMaxRentToolTip"),  , 6,  ,  ,  ,  ,  , 6))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>        " & vbCrLf)
            Response.Write("        <TR>                       " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41426>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41427>" & GetLocalResourceObject("Anchor5Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        </TR>        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>            " & vbCrLf)
            Response.Write("            <TD></TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("        </TR>      " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.CheckControl("chkAssociai", GetLocalResourceObject("chkAssociaiCaption"), .mclsProduct_li.sAssociai, "1", "EnabledFields(" & lblnFindDurinsu & "," & .mclsProduct_li.nTypdurins & "," & lblnFindPayinsu & "," & .mclsProduct_li.nTypdurpay & ")",  , 7))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41435, "optPremiumtype", GetLocalResourceObject("optPremiumtype_1Caption"), .mclsProduct_li.insDefaultValueDP043("OptPremiumType1"), "1",  ,  , 10))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41436, "optAssoTotal", GetLocalResourceObject("optAssoTotal_1Caption"), .mclsProduct_li.insDefaultValueDP043("optAssoTotal1"), "1",  ,  , 8))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41437, "optPremiumtype", GetLocalResourceObject("optPremiumtype_2Caption"), .mclsProduct_li.insDefaultValueDP043("OptPremiumType2"), "2",  ,  , 11))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>        " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41438, "optAssoTotal", GetLocalResourceObject("optAssoTotal_2Caption"), .mclsProduct_li.insDefaultValueDP043("optAssoTotal2"), "2",  ,  , 9))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>            " & vbCrLf)
            Response.Write("        </TR>        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=41428><A NAME=""Seguro"">" & GetLocalResourceObject("AnchorSeguro2Caption") & "</A></LABEL></TD>" & vbCrLf)
            Response.Write("        </TR>                       " & vbCrLf)
            Response.Write("        <TR>                       " & vbCrLf)
            Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>            " & vbCrLf)
            Response.Write("        </TR>                       " & vbCrLf)
            Response.Write("        <TR>                       " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41429>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD WIDTH=10%>&nbsp;</TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41430>" & GetLocalResourceObject("Anchor7Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        </TR>        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("            <TD></TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("        </TR>      " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=14871>" & GetLocalResourceObject("cbeTypdurinsCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            ")

            mobjValues.TypeList = 2
            mobjValues.List = CStr(3)

            Response.Write("" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeTypdurins", "Table5589", 1, CStr(.mclsProduct_li.nTypdurins),  ,  ,  ,  ,  , "InsChangeDurInsur();",  ,  , GetLocalResourceObject("cbeTypdurinsToolTip"),  , 12))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41440, "optIdurvari", GetLocalResourceObject("optIdurvari_1Caption"), 2 - mobjValues.StringToType(.mclsProduct_li.sIdurvari, eFunctions.Values.eTypeData.etdDouble), "1", "InsChangeDurInsur();", True, 14))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tctRoutinsuCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.TextControl("tctRoutinsu", 12, .mclsProduct_li.sRoutinsu,  , GetLocalResourceObject("tctRoutinsuToolTip"),  ,  ,  ,  ,  , 13))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41442, "optIdurvari", GetLocalResourceObject("optIdurvari_2Caption"), 3 - mobjValues.StringToType(.mclsProduct_li.sIdurvari, eFunctions.Values.eTypeData.etdDouble), "2", "InsChangeDurInsur();", True, 15))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
            Response.Write("	            <DIV ID=""Scroll"" STYLE=""width:300;height:100;overflow:auto;outset gray"">")


            '- Se muestran las duraciones del seguro
            If .bFindDurinsu Then
                For Each lclsDurinsu_prod In .mcolDurinsu_prod
                    mobjGridS.Columns("tcnIdurafix").DefValue = CStr(lclsDurinsu_prod.nIdurafix)
                    mobjGridS.Columns("cbeTypeInsur1").DefValue = CStr(lclsDurinsu_prod.nTypdurins)
                    mobjGridS.Columns("tcnMinDurIns").DefValue = CStr(lclsDurinsu_prod.nMinDurIns)
                    Response.Write(mobjGridS.DoRow())
                Next lclsDurinsu_prod
                Response.Write(mobjValues.HiddenControl("hddRecordCountS", CStr(.mcolDurinsu_prod.Count)))
            End If
            Response.Write(mobjGridS.closeTable())

            Response.Write("" & vbCrLf)
            Response.Write("				</DIV>" & vbCrLf)
            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Pagos"">" & GetLocalResourceObject("AnchorPagos2Caption") & "</A></LABEL></TD>" & vbCrLf)
            Response.Write("        </TR>                       " & vbCrLf)
            Response.Write("        <TR>                       " & vbCrLf)
            Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>            " & vbCrLf)
            Response.Write("        </TR>                       " & vbCrLf)
            Response.Write("        <TR>                       " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41429>" & GetLocalResourceObject("Anchor6Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=41430>" & GetLocalResourceObject("Anchor7Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        </TR>        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("            <TD></TD>            " & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>" & vbCrLf)
            Response.Write("        </TR>      " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=14871>" & GetLocalResourceObject("cbeTypdurinsCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            ")

            mobjValues.TypeList = 2
            mobjValues.List = "3"

            Response.Write("" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeTypDurPay", "Table5589", 1, CStr(.mclsProduct_li.nTypdurpay),  ,  ,  ,  ,  , "InsChangeDurInsur();",  ,  , GetLocalResourceObject("cbeTypDurPayToolTip"),  , 16))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41440, "optIpayvari", GetLocalResourceObject("optIpayvari_1Caption"), 2 - mobjValues.StringToType(.mclsProduct_li.sPdurvari, eFunctions.Values.eTypeData.etdDouble), "1", "InsChangeDurInsur();", True, 18, GetLocalResourceObject("optIpayvari_1ToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("sRoutineCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.TextControl("tctRoutpay", 12, .mclsProduct_li.sRoutpay,  , GetLocalResourceObject("tctRoutpayToolTip"),  ,  ,  ,  ,  , 17))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">")


            Response.Write(mobjValues.OptionControl(41442, "optIpayvari", GetLocalResourceObject("optIpayvari_2Caption"), 3 - mobjValues.StringToType(.mclsProduct_li.sPdurvari, eFunctions.Values.eTypeData.etdDouble), "2", "InsChangeDurInsur();", True, 19, GetLocalResourceObject("optIpayvari_2ToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("           " & vbCrLf)
            Response.Write("<!-- Se añade el campo ""nQMonVPN"" para realizar cambio referente a APV2 - ACM - 25/08/2003 -->")


            lclsProduct_li = New eProduct.Product
            Call lclsProduct_li.FindProduct_li(Session("nBranch"), Session("nProduct"), Today, True)
            If lclsProduct_li.nQmonVPN < 0 Then
                lstrValue = vbNullString
            Else
                lstrValue = lclsProduct_li.nQmonVPN
            End If

            If .mclsProduct_li.nProdClas = 4 Then

                Response.Write("" & vbCrLf)
                Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("nQMonVPNCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD>")


                Response.Write(mobjValues.NumericControl("nQMonVPN", 5, lstrValue,  , GetLocalResourceObject("nQMonVPNToolTip"), False, 0, False,  ,  ,  ,  ,  ,  , True))


                Response.Write("</TD>")


            Else

                Response.Write("" & vbCrLf)
                Response.Write("				<TD><LABEL ID=0>" & GetLocalResourceObject("nQMonVPNCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("				<TD>")


                Response.Write(mobjValues.NumericControl("nQMonVPN", 5, "",  , GetLocalResourceObject("nQMonVPNToolTip"), False, 0, False,  ,  ,  , False,  ,  , True))


                Response.Write("</TD>")


            End If


            Response.Write("          " & vbCrLf)
            Response.Write("            " & vbCrLf)
            Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("            <TD COLSPAN=""2"">" & vbCrLf)
            Response.Write("				<DIV ID=""Scroll"" style=""width:300;height:150;overflow:auto;outset gray"">")


            '- Se muestran las duraciones de los pagos
            If .bFindPayinsu Then
                For Each lclsPayinsu_prod In .mcolDurpay_prod
                    mobjGridP.Columns("cbeIdurafix").Parameters.Add("nTypdurins", lclsPayinsu_prod.nTypdurins, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjGridP.Columns("cbeIdurafix").DefValue = CStr(lclsPayinsu_prod.nIdurafix)
                    mobjGridP.Columns("cbeTypeInsur").DefValue = CStr(lclsPayinsu_prod.nTypdurins)
                    mobjGridP.Columns("cbeTypePay").DefValue = CStr(lclsPayinsu_prod.nTypdurpay)
                    mobjGridP.Columns("tcnPdurafix").DefValue = CStr(lclsPayinsu_prod.nPdurafix)
                    mobjGridP.Columns("hddID").DefValue = CStr(lclsPayinsu_prod.nId)
                    Response.Write(mobjGridP.DoRow())
                Next lclsPayinsu_prod
                Response.Write(mobjValues.HiddenControl("hddRecordCountP", CStr(.mcolDurpay_prod.Count)))
            End If
            Response.Write(mobjGridP.closeTable())

            Response.Write("" & vbCrLf)
            Response.Write("				</DIV>" & vbCrLf)
            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>       " & vbCrLf)
            Response.Write("        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnBmgCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.NumericControl("tcnBmg", 5, CStr(.mclsProduct_li.nBmg),  , GetLocalResourceObject("tcnBmgToolTip"), False, 0, False,  ,  ,  , False,  ,  , True))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        " & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tctRoutinevpnCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.TextControl("tctRoutinevpn", 12, .mclsProduct_li.sRoutinevpn,  , GetLocalResourceObject("tctRoutinevpnToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        " & vbCrLf)
            Response.Write("    </TABLE> " & vbCrLf)
            Response.Write("    <BR>     " & vbCrLf)
            Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
            Response.Write("        <TR>    " & vbCrLf)
            Response.Write("            <TD ALIGN=""CENTER"">" & vbCrLf)
            Response.Write("                <LABEL ID=41434><A HREF=""JAVASCRIPT:ShowSubSequence()"">" & GetLocalResourceObject("btnSequenceCaption") & "</A></LABEL>" & vbCrLf)
            Response.Write("                &nbsp;" & vbCrLf)
            Response.Write("                ")


            Response.Write(mobjValues.AnimatedButtonControl("btnSequence", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("btnSequenceToolTip"),  , "ShowSubSequence()"))


            Response.Write("" & vbCrLf)
            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("	</TABLE>")


            Response.Write(mobjValues.HiddenControl("hddBeforeIdurvari", Request.QueryString.Item("nBeforeIdurvari")))
            Response.Write(mobjValues.BeginPageButton)
            Response.Write("<SCRIPT>EnabledFields(" & lblnFindDurinsu & "," & .mclsProduct_li.nTypdurins & "," & lblnFindPayinsu & "," & .mclsProduct_li.nTypdurpay & ")</" & "Script>")
            If .mblnError Then
                lclsErrors = New eFunctions.Errors
                Response.Write(lclsErrors.ErrorMessage("DP043", 55972,  ,  ,  , True))
            End If

            Response.Write("" & vbCrLf)
            Response.Write("<SCRIPT>" & vbCrLf)
            Response.Write("	self.document.forms[0].hddBeforeIdurvari.value='")


            Response.Write(mobjValues.StringToType(.mclsProduct_li.sIdurvari, eFunctions.Values.eTypeData.etdDouble))


            Response.Write("';" & vbCrLf)
            Response.Write("</" & "SCRIPT>")


        End With
        lclsErrors = Nothing
    End Sub

    '% insDefineHeader: Este procedimiento se encarga de definir las características de los grid
    '%					mostrados en pantalla
    '---------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------------
        mobjGridS = New eFunctions.Grid
        mobjGridP = New eFunctions.Grid

        mobjGridS.sCodisplPage = "DP043"
        mobjGridP.sCodisplPage = "DP043"

        '+ Características de la duración del seguro
        With mobjGridS
            .sArrayName = "Durinsu"

            Call .Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeInsur1ColumnCaption"), "cbeTypeInsur1", "Table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("nTypDurins") <> "7",  , GetLocalResourceObject("cbeTypeInsur1ColumnToolTip"))
            Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnIdurafixColumnCaption"), "tcnIdurafix", 5, CStr(0), False, GetLocalResourceObject("tcnIdurafixColumnToolTip"))
            Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnMinDurInsColumnCaption"), "tcnMinDurIns", 3, CStr(0), False, GetLocalResourceObject("tcnMinDurInsColumnToolTip"))
            .Columns("cbeTypeInsur1").TypeList = 1
            '+ Se colocan sólo los valores "Edad alcanzada", "Años", "Meses" y "Dias", sólo se utiliza en caso que 
            '+ la duración del seguro sea "Años/Edad alcanzada/Meses/Dias"
            .Columns("cbeTypeInsur1").List = "1,2,8,9"
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "DP043"
            .AddButton = True
            .Columns("Sel").GridVisible = True
            .ActionQuery = mobjValues.ActionQuery
            .sDelRecordParam = "nIduraFix=' + Durinsu[lintIndex].tcnIdurafix + '" & "&nTypdurins=' + Durinsu[lintIndex].cbeTypeInsur1 + '" & "&nMinDurIns=' + Durinsu[lintIndex].tcnMinDurIns + '"
            .Height = 190
            .Width = 330

            If Request.QueryString.Item("nTypDurins") <> "7" Then
                .Columns("cbeTypeInsur1").DefValue = Request.QueryString.Item("nTypDurins")
            End If

            If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sArrayName") = "Durinsu" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With

        '+ Características de la duración de los pagos
        With mobjGridP
            .sArrayName = "Payinsu"
            Call .Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeInsurColumnCaption"), "cbeTypeInsur", "Table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insChangeValues(this)", Request.QueryString.Item("nTypDurins") <> "7",  , GetLocalResourceObject("cbeTypeInsurColumnToolTip"))
            Call .Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeIdurafixColumnCaption"), "cbeIdurafix", "tabDurInsu_prod", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeIdurafixColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
            Call .Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeTypePayColumnCaption"), "cbeTypePay", "Table5589", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("nTypDurPay") <> "7",  , GetLocalResourceObject("cbeTypePayColumnToolTip"))
            Call .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPdurafixColumnCaption"), "tcnPdurafix", 5, CStr(0), False, GetLocalResourceObject("tcnPdurafixColumnToolTip"))
            Call .Columns.AddHiddenColumn("hddID", vbNullString)
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "DP043"
            .ActionQuery = mobjValues.ActionQuery
            .sDelRecordParam = "nId=' + Payinsu[lintIndex].hddID + '"
            .Height = 280
            .Width = 300
            .AddButton = True
            .Columns("cbeTypeInsur").EditRecord = True
            .Columns("cbeTypeInsur").TypeList = 1
            .Columns("cbeTypePay").TypeList = 1
            '+ Se colocan sólo los valores "Edad alcanzada", "Años", "Meses" y "Dias", sólo se utiliza en caso que 
            '+ la duración del seguro sea "Años/Edad alcanzada/Meses/Dias"
            .Columns("cbeTypeInsur").List = "1,2,8,9"
            .Columns("cbeTypePay").List = "1,2,8,9"
            With .Columns("cbeIdurafix")
                .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nTypdurins", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With

            If Request.QueryString.Item("nTypDurPay") <> "7" Then
                .Columns("cbeTypePay").DefValue = Request.QueryString.Item("nTypDurpay")
            End If

            If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sArrayName") = "Payinsu" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '% insPreDP043Upd: se realiza el manejo de las ventanas Popup asociadas a las grillas de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreDP043Upd()
        '--------------------------------------------------------------------------------------------
        Dim lclsData_prod As Object

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("//% insChangeValues: se maneja el cambio de valores de los controles" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insChangeValues(Field){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("	self.document.forms[0].cbeIdurafix.Parameters.Param4.sValue=Field.value;" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la DP043" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function UpdateFields(){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("		cbeProdclas.value = top.opener.document.forms[0].cbeProdclas.value;" & vbCrLf)
        Response.Write("		cbeCurrency.value = top.opener.document.forms[0].cbeCurrency.value;" & vbCrLf)
        Response.Write("		if (top.opener.document.forms[0].chkMorCapii.checked)" & vbCrLf)
        Response.Write("			chkMorcapii.value = '1';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			chkMorcapii.value = '2';" & vbCrLf)
        Response.Write("		tcnMinRent.value = top.opener.document.forms[0].tcnMinRent.value;" & vbCrLf)
        Response.Write("		sRoutine.value = top.opener.document.forms[0].sRoutine.value;" & vbCrLf)
        Response.Write("		tcnMaxRent.value = top.opener.document.forms[0].tcnMaxRent.value;" & vbCrLf)
        Response.Write("		if (top.opener.document.forms[0].chkAssociai.checked)" & vbCrLf)
        Response.Write("			chkAssociai.value = '1';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			chkAssociai.value = '2';" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("		if (top.opener.document.forms[0].optPremiumtype[1].checked)" & vbCrLf)
        Response.Write("			optPremiumtype.value = '2';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			optPremiumtype.value = '1';" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("		if (top.opener.document.forms[0].optAssoTotal[1].checked)" & vbCrLf)
        Response.Write("			optAssoTotal.value = '2';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			optAssoTotal.value = '1';" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("        cbeTypdurins.value = top.opener.document.forms[0].cbeTypdurins.value;" & vbCrLf)
        Response.Write("		if (top.opener.document.forms[0].optIdurvari[1].checked)" & vbCrLf)
        Response.Write("			optIdurvari.value = '2';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			optIdurvari.value = '1';" & vbCrLf)
        Response.Write("      " & vbCrLf)
        Response.Write("        cbeTypDurPay.value = top.opener.document.forms[0].cbeTypDurPay.value;" & vbCrLf)
        Response.Write("		if (top.opener.document.forms[0].optIpayvari[1].checked)" & vbCrLf)
        Response.Write("			optIpayvari.value = '2';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			optIpayvari.value = '1';" & vbCrLf)

        Response.Write("		if (top.opener.document.forms[0].chkNo_Holidays.checked)" & vbCrLf)
        Response.Write("			chkNo_Holidays.value = '1';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			chkNo_Holidays.value = '2';" & vbCrLf)

        Response.Write("		if (top.opener.document.forms[0].chkApv.checked)" & vbCrLf)
        Response.Write("			chkApv.value = '1';" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			chkApv.value = '2';" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("        tctRoutinsu.value = top.opener.document.forms[0].tctRoutinsu.value;" & vbCrLf)
        Response.Write("        tctRoutpay.value = top.opener.document.forms[0].tctRoutpay.value;" & vbCrLf)
        Response.Write("		hddbWithInformation.value = top.opener.document.forms[0].hddbWithInformation.value;" & vbCrLf)
        Response.Write("		" & vbCrLf)
        Response.Write("		if(typeof(cbeTypeInsur)!='undefined')" & vbCrLf)
        Response.Write("			if(cbeTypeInsur.disabled)" & vbCrLf)
        Response.Write("				cbeTypeInsur.value = cbeTypdurins.value;" & vbCrLf)
        Response.Write("	" & vbCrLf)
        Response.Write("		if(typeof(cbeTypePay)!='undefined')" & vbCrLf)
        Response.Write("			if(cbeTypePay.disabled){" & vbCrLf)
        Response.Write("				cbeTypePay.value = cbeTypDurPay.value" & vbCrLf)
        Response.Write("				self.document.forms[0].cbeIdurafix.Parameters.Param4.sValue=cbeTypdurins.value;" & vbCrLf)
        Response.Write("			}" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("</" & "SCRIPT>")


        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                If Request.QueryString.Item("sArrayName") = "Durinsu" Then
                    lclsData_prod = New eProduct.Durinsu_prod
                    Call lclsData_prod.insPostDP043UPD(.QueryString("Action"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.QueryString.Item("nIduraFix"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypdurins"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nMinDurIns"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUsercode"))
                Else
                    lclsData_prod = New eProduct.Durpay_prod
                    Call lclsData_prod.insPostDP043UPD(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Session("nBranch"), Session("nProduct"), Session("dEffecdate"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))
                End If
                lclsData_prod = Nothing
            End If
            If Request.QueryString.Item("sArrayName") = "Durinsu" Then
                '		    Response.Write "<NOTSCRIPT> alert(""" & "nTypDurins = " & Request.QueryString("nTypDurins") & """); </" & "Script> "		    
                Response.Write(mobjGridS.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
            Else
                '		    Response.Write "<NOTSCRIPT> alert(""" & "nTypDurpay = " & Request.QueryString("nTypDurpay") & """); </" & "Script> "		    
                Response.Write(mobjGridP.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
                If .QueryString.Item("Action") <> "Del" Then
                    Response.Write("<SCRIPT>self.document.forms[0].cbeIdurafix.disabled=(top.opener.document.forms[0].optIdurvari[1].checked)?false:true;</" & "Script>")
                End If
            End If
        End With

        With Response
            .Write(mobjValues.HiddenControl("cbeProdclas", ""))
            .Write(mobjValues.HiddenControl("cbeCurrency", ""))
            .Write(mobjValues.HiddenControl("chkMorcapii", ""))
            .Write(mobjValues.HiddenControl("tcnMinRent", ""))
            .Write(mobjValues.HiddenControl("sRoutine", ""))
            .Write(mobjValues.HiddenControl("tctRoutpay", ""))
            .Write(mobjValues.HiddenControl("tcnMaxRent", ""))
            .Write(mobjValues.HiddenControl("chkAssociai", ""))
            .Write(mobjValues.HiddenControl("optPremiumtype", ""))
            .Write(mobjValues.HiddenControl("optAssoTotal", ""))
            .Write(mobjValues.HiddenControl("cbeTypdurins", ""))
            .Write(mobjValues.HiddenControl("optIdurvari", ""))
            .Write(mobjValues.HiddenControl("cbeTypDurPay", ""))
            .Write(mobjValues.HiddenControl("optIpayvari", ""))
            .Write(mobjValues.HiddenControl("tctRoutinsu", ""))
            .Write(mobjValues.HiddenControl("hddbWithInformation", ""))
            .Write(mobjValues.HiddenControl("chkApv", ""))
            .Write(mobjValues.HiddenControl("chkNo_Holidays", ""))

        End With
        Response.Write("<SCRIPT>UpdateFields()</" & "Script>")
    End Sub

</script>
<%Response.Expires = -1
    '- Se define la variable para la carga de datos en la forma 
    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "DP043"
    mobjMenu = New eFunctions.Menues
    mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
%>
<HTML>
<HEAD>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>    
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
    With Response
        If Request.QueryString.Item("Type") <> "PopUp" Then
            .Write(mobjMenu.setZone(2, "DP043", "DP043.aspx"))
            .Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
        End If
        .Write(mobjValues.StyleSheet())
    End With
    mobjMenu = Nothing
%>
</HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 16/12/03 13:21 $|$$Author: Nvaplat11 $"

//- Tipo con las posibles acciones a ejecutar
    var eActions = new TypeActions()

//% EnabledFields: Habilita los campos de los frames 'Seguro'
//--------------------------------------------------------------------------------------------
function EnabledFields(bFindDurinsu, nTypdurins, bFindPayinsu, nTypdurpay){
//--------------------------------------------------------------------------------------------
    var nAction = <%=Request.QueryString.Item("nMainAction")%>;

    if(nAction!=eActions.clngActionQuery){
        with(self.document.forms[0]){
//			cmdAddDurinsu.disabled = !bFindDurinsu;
//			cmdAddPayinsu.disabled = !bFindPayinsu;
			
			cmdAddDurinsu.disabled = !(nTypdurins == 1 || nTypdurins == 2 || nTypdurins == 7 || nTypdurins == 8 || nTypdurins == 9);
            cmdAddPayinsu.disabled = !(nTypdurpay == 1 || nTypdurpay == 2 || nTypdurpay == 7 || nTypdurpay == 8 || nTypdurpay == 9);
			
			if (typeof(cmdDeleteDurinsu) != 'undefined') cmdDeleteDurinsu.disabled = cmdAddDurinsu.disabled;
			if (typeof(cmdDeletePayinsu) != 'undefined') cmdDeletePayinsu.disabled = cmdAddPayinsu.disabled;
			optAssoTotal[0].disabled = !chkAssociai.checked
			optAssoTotal[1].disabled = !chkAssociai.checked
			if (!chkAssociai.checked) { 
				optAssoTotal[0].checked = chkAssociai.checked
				optAssoTotal[1].checked = chkAssociai.checked
			}
            optIdurvari[0].disabled = !(nTypdurins == 1 || nTypdurins == 2 || nTypdurins == 7 || nTypdurins == 8 || nTypdurins == 9);
            optIpayvari[0].disabled = !(nTypdurpay == 1 || nTypdurpay == 2 || nTypdurpay == 7 || nTypdurpay == 8 || nTypdurpay == 9);
            optIdurvari[1].disabled = optIdurvari[0].disabled;
            optIpayvari[1].disabled = optIpayvari[0].disabled;
            if (optIdurvari[0].disabled){
				optIdurvari[0].checked = false;
				optIdurvari[1].checked = false;
			}
            if (optIpayvari[0].disabled){
				optIpayvari[0].checked = false;
				optIpayvari[1].checked = false;
			}
            tctRoutinsu.disabled = nTypdurins != 4;
            tctRoutpay.disabled = nTypdurpay != 4;
            if (tctRoutinsu.disabled)
				tctRoutinsu.value = '';
            if (tctRoutpay.disabled)
				tctRoutpay.value = '';
			
		
        }
    }
}
//% ShowSubSequence: Muestra la subsecuencia de características de vida
//--------------------------------------------------------------------------------------------
function ShowSubSequence(){
//--------------------------------------------------------------------------------------------
    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Product&sProject=ProductSeq/ProdLifeSeq&sCodispl=DP043', 'ProdLifeSeq', 750, 500, 'no', 'no', 20, 20,'yes')  
}
//%InsChangeDurInsur: Recarga la página cuando se modifica las opciones de duracion del seguro
//%                   Forma y Tiempo
//---------------------------------------------------------------------------------------------
function InsChangeDurInsur(){
//---------------------------------------------------------------------------------------------
    var lstrstring = '';
    lstrstring += document.location;
    lstrstring = lstrstring.replace(/&nProdclas=.*/, "");    
    
    with (self.document.forms[0]){
		lstrstring = lstrstring + "&nProdclas=" + cbeProdclas.value +
		                          "&nTypDurins=" + cbeTypdurins.value +
		                          "&sIdurvari="  + (optIdurvari[1].checked?2:1) +
		                          "&nTypdurpay=" + cbeTypDurPay.value +
		                          "&sPdurvari="  + (optIpayvari[1].checked?2:1) +
		                          "&nCurrency=" + cbeCurrency.value +
		                          "&sMorcapii=" + (chkMorCapii.checked?1:2) +
		                          "&nMinrent=" + tcnMinRent.value +
		                          "&sRoutine=" + sRoutine.value +
		                          "&sRoutpay=" + tctRoutpay.value +
		                          "&nMaxrent=" + tcnMaxRent.value +
		                          "&sAssociai=" + (chkAssociai.checked?1:2) +
		                          "&sPremiumtype=" + (optPremiumtype[1].checked?2:1) +
		                          "&sAssoTotal=" + (optAssoTotal[1].checked?2:1) +
		                          "&sRoutinsu=" + tctRoutinsu.value +
		                          "&ReloadAction=Add" +
		                          "&nBeforeIdurvari=" + hddBeforeIdurvari.value + 
		                          "&sApv=" + (chkApv.checked?1:2) +
                                  "&bWithInformation=" + hddbWithInformation.value +
                                  "&sNo_Holidays=" + (chkNo_Holidays.checked?1:2) ;
	}	
	
    self.document.location = lstrstring;
}
</SCRIPT>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProdCoverLife" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
    Response.Write(mobjValues.ShowWindowsName("DP043"))
    Call insDefineHeader()
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreDP043()
    Else
        Call insPreDP043Upd()
    End If
%>
</FORM>
</BODY>
</HTML>