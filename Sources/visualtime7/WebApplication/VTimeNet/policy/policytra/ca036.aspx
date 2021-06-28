<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues


    '% InsPreCA036: se controla el acceso a la página
    '--------------------------------------------------------------------------------------------
    Private Sub InsPreCA036()
        '--------------------------------------------------------------------------------------------
        Dim lclsPolicy As ePolicy.Policy
        Dim lstrQueryString As String

        lclsPolicy = New ePolicy.Policy

        If lclsPolicy.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy")) Then
            Session("sColinvot") = lclsPolicy.sColinvot
            '+ Si el tipo de facturación es por póliza se asigna valor por defecto al Titular
            If CStr(Session("sClient")) = vbNullString Then
                If lclsPolicy.sColinvot = "1" Then
                    Session("sClient") = lclsPolicy.SCLIENT
                End If
            End If
            If Session("dStart") = vbNullString Then
                If Session("dStart") = Date.MinValue.ToString Then
                    Session("dStart") = ""
                Else
                    Session("dStart") = lclsPolicy.dNextReceip
                End If
            Else

            End If

        End If
        Response.Write(mobjValues.ShowWindowsName("CA036", Request.QueryString.Item("sWindowDescript")))
        mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)

        lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=0" & "&dEffecdate=" & Session("dEffecdate")

        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=13561>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""4"">")


        Response.Write(mobjValues.ClientControl("tctClient", Session("sClient"),  , GetLocalResourceObject("tctClientToolTip"),  ,  , "lblCliename",  ,  ,  ,  , eFunctions.Values.eTypeClient.SearchClientPolicy, 2,  ,  , lstrQueryString))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=13562>" & GetLocalResourceObject("cbeCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""4"">")


        With mobjValues
            .BlankPosition = False
            .Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Response.Write(.PossiblesValues("cbeCurrency", "tabCurren_Pol", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"), True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip"),  , 3))

        End With

        Response.Write("" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=35% COLSPAN=2 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Fecha de movimientos"">" & GetLocalResourceObject("AnchorFecha de movimientosCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=3 CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de movimientos"">" & GetLocalResourceObject("AnchorTipo de movimientosCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=2 CLASS=""Horline""></TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""10%""> </TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=3 CLASS=""Horline""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=13563>" & GetLocalResourceObject("tcdStartCaption") & "</LABEL></TD>    " & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdStart", Session("dStart"),  , GetLocalResourceObject("tcdStartToolTip"),  ,  ,  ,  ,  , 4))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypeMov", GetLocalResourceObject("optTypeMov_CStr1Caption"), Session("sTypeMov"), CStr(1),  ,  , 5, GetLocalResourceObject("optTypeMov_CStr1ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypeMov", GetLocalResourceObject("optTypeMov_CStr4Caption"), CStr(Session("sTypeMov") - 3), CStr(4),  ,  , 6, GetLocalResourceObject("optTypeMov_CStr4ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=13564>" & GetLocalResourceObject("tcdEndCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdEnd", Session("dEnd"),  , GetLocalResourceObject("tcdEndToolTip"),  ,  ,  ,  ,  , 7))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypeMov", GetLocalResourceObject("optTypeMov_CStr2Caption"), CStr(Session("sTypeMov") - 1), CStr(2),  ,  , 8, GetLocalResourceObject("optTypeMov_CStr2ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypeMov", GetLocalResourceObject("optTypeMov_CStr5Caption"), CStr(Session("sTypeMov") - 4), CStr(5),  ,  , 9, GetLocalResourceObject("optTypeMov_CStr5ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""3"">&nbsp</TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"">")


        Response.Write(mobjValues.OptionControl(0, "optTypeMov", GetLocalResourceObject("optTypeMov_CStr3Caption"), CStr(Session("sTypeMov") - 2), CStr(3),  ,  , 10, GetLocalResourceObject("optTypeMov_CStr3ToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=13565>" & GetLocalResourceObject("tcdLedgerDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD> ")


        Response.Write(mobjValues.DateControl("tcdLedgerDate", Session("dLedgerDate"),  , GetLocalResourceObject("tcdLedgerDateToolTip"),  ,  ,  ,  ,  , 11))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""3"">&nbsp</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>")


        Response.Write(mobjValues.HiddenControl("sColinvot", lclsPolicy.sColinvot))

        If lclsPolicy.sColinvot = "1" Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                .Write("disable_Client();")
                .Write("</" & "Script>")
            End With

        End If

        lclsPolicy = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("ca036")
    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "ca036"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT LANGUAGE="JavaScript">

    //+ disable_Client : Si la facturación es “Por póliza” se bloquean los campos de cliente
//---------------------------------------------------------------------------
    function disable_Client()
  //---------------------------------------------------------------------------
    {
        with (self.document.forms[0])
        {
            btntctClient.disabled = true;
            tctClient_Digit.disabled = true;
            tctClient.disabled = true;
        }
}
</SCRIPT>



    <%mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        With Response
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle("CA036", Request.QueryString.Item("sWindowDescript")))
            .Write(mobjMenu.setZone(2, "CA036", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
        End With
        mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"  
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CA036" ACTION="ValBillGroupPolSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
    Call InsPreCA036()
%>
</FORM>
</BODY>
</HTML>
<%
    mobjValues = Nothing

%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
    Call mobjNetFrameWork.FinishPage("ca036")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
