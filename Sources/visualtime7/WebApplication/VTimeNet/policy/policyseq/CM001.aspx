<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Variables para el manejo de los valores cuando se carga o recarga la página
    Dim mobjMenu As eFunctions.Menues
    Dim lclsTRCM As ePolicy.TRCM
    Dim lclsGroupRisk As ePolicy.TRCM
    Dim lcolTRCM As ePolicy.TRCMs
    Dim lstrAction As String

    Dim nSituation As Integer
    Dim nGroup As Integer
    Dim nWorktype As Integer
    Dim sDesc_work As String
    Dim sWorkname As String
    Dim dInitialdate_work As Date
    Dim dEnddate_work As Date
    Dim dNulldate As Date
    Dim dInitialdate_em As Date
    Dim dEnddate_em As Date
    Dim dInitialdate_m As Date
    Dim dEnddate_m As Date


    '% DefaultValues: Se realiza el manejo de los valores de los campos cuando se carga o recarga la página
    '------------------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub DefaultValues()
        '------------------------------------------------------------------------------------------------------------------------------------------------------       
        With lclsTRCM
            nSituation = .nSituation
            nGroup = .nGroup
            nWorktype = .nWorktype
            sWorkname = .sWorkname
            sDesc_work = .sDesc_work
            dInitialdate_work = .dInitialdate_work
            dEnddate_work = .dEnddate_work
            dNulldate = .dNulldate
            dInitialdate_em = .dInitialdate_em
            dEnddate_em = .dEnddate_em
            dInitialdate_m = .dInitialdate_m
            dEnddate_m = .dEnddate_m

        End With
			        
    End Sub
    '% insPreCM001: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCM001()
        '--------------------------------------------------------------------------------------------
        Dim lblnExist As Boolean
        Dim sPolitype As String
        lcolTRCM = New TRCMs
        sPolitype = Session("sPolitype")
        lblnExist = False
	
        '+ Se cargan en la colección Tab_Goodses los tipos de bienes.
        ' If (sPolitype = "1") Then
        Call lcolTRCM.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nCertif"), Session("nPolicy"), Session("dEffecdate"))
        If lcolTRCM.Count > 0 Then
            lclsTRCM = lcolTRCM.Item(1)
        Else
            lclsTRCM = New ePolicy.TRCM
        End If
        lcolTRCM = Nothing
       
    End Sub


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("CM001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "CM001"
    mobjMenu = New eFunctions.Menues
%> 
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        
        Response.Write(mobjValues.StyleSheet())
        Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
        mobjMenu = Nothing
    %>
</head>
<body onunload="closeWindows();">
    <FORM METHOD="POST"	ID="FORM" NAME="frmCM001" ACTION="valpolicyseq.aspx?Action=Add">
        
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
        Call insPreCM001()
        Call DefaultValues()
    %>

        <TABLE WIDTH="100%">
            <TR>
                <TD COLSPAN="2">&nbsp;</TD>
                <TD COLSPAN="">&nbsp;</TD>
                <TD COLSPAN="3">&nbsp;</TD>
                <TD COLSPAN="">&nbsp;</TD>
            </TR>
            <TR>
              
                <TD COLSPAN="2" CLASS="HighLighted" WIDTH=40%><LABEL ID=40950><%=GetLocalResourceObject("AnchorCaption")%></LABEL></TD>
                <TD WIDTH="10%">&nbsp;</TD>
                <TD COLSPAN="3" CLASS="HighLighted" WIDTH=50%><LABEL ID=LABEL1><%= GetLocalResourceObject("AnchorCaption1") %></LABEL></TD>
                <TD WIDTH="10%">&nbsp;</TD>
            </TR>
            <TR>
                <TD COLSPAN="2" CLASS="HorLine"></TD>
                <TD COLSPAN=""></TD>
                <TD COLSPAN="3" CLASS="HorLine"></TD>
                <TD COLSPAN=""></TD>
            </TR>
            <TR>
                <TD COLSPAN=""><LABEL><%= GetLocalResourceObject("cbeGroupCaption") %></LABEL></TD>
                <TD>

        <% 
            With mobjValues.Parameters
            .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
           %>
        <%=mobjValues.PossiblesValues("cbovalGroup", "tabGroups", Values.eValuesType.clngWindowType, CStr(nGroup), True, , , , , , , , GetLocalResourceObject("cbeGroupTooTip"))%>
                </TD>
                <TD ></TD>
                <TD COLSPAN="1" WIDTH="0%"><LABEL><%=GetLocalResourceObject("tctWorknameCaption")%></LABEL></TD>
                <TD WIDTH="0%">
           <%=mobjValues.TextControl("tctWorkname", 65, CStr(sWorkname), , GetLocalResourceObject("tctWorknameToolTip"))%>        
                </TD>
            </TR>
            <TR>
                <TD COLSPAN="">&nbsp;</TD>
                <TD COLSPAN="">&nbsp;</TD>
            </TR>
            <TR>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("cbeRiskCaption")%></LABEL></TD>
                <TD>
        <%
        With mobjValues.Parameters
            .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
            %>
            <%=mobjValues.PossiblesValues("cbovalSituation", "tabSituation", Values.eValuesType.clngWindowType, CStr(nSituation), True, , , , , , , , GetLocalResourceObject("cbeRiskToolTip")) %>
                <TD>
                <TD COLSPAN="1" WIDTH="0%"><LABEL><%= GetLocalResourceObject("cbeTypeWorkCaption") %></LABEL></TD>
                <TD WIDTH="0%">
        <%=mobjValues.PossiblesValues("cbeTypeWork", "table5622", Values.eValuesType.clngComboType, CStr(nWorktype), , , , , , , , , GetLocalResourceObject("cbeTypeWorkToolTip")) %>        
                </TD>
            </TR>
            </TR>
        
            <TR>
                <TD COLSPAN="2" CLASS="HighLighted" WIDTH=40%><LABEL ID=LABEL2><%= GetLocalResourceObject("AnchorCaption2") %></LABEL></TD>
                <TD WIDTH="10%">&nbsp;</TD>
                <TD COLSPAN="3" CLASS="HighLighted" WIDTH=50%><LABEL ID=LABEL3><%= GetLocalResourceObject("AnchorCaption3") %></LABEL></TD>
                <TD WIDTH="10%">&nbsp;</TD>
            </TR>
            <TR>
                <TD COLSPAN="2" CLASS="HorLine"></TD>
                <TD COLSPAN="">&nbsp;</TD>
                <TD COLSPAN="3" CLASS="HorLine"></TD>
                <TD COLSPAN="">&nbsp;</TD>
            </TR>
            <tr>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("DateInitdateCaption")%></LABEL></TD>
                <td>
                    <%=mobjValues.DateControl("dInitialdate_work", dInitialdate_work, , GetLocalResourceObject("DateInitdate_workTooltip"))%>
                </td>
                <TD COLSPAN=""></TD>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("DateInitdateCaption")%></LABEL></TD>
                <td>
                    <%=mobjValues.DateControl("dInitialdate_m", dInitialdate_m, , GetLocalResourceObject("DateInitdate_mTooltip"))%>
                </td>
            </tr>
            <TR>
                <TD COLSPAN="2"></TD>
                <TD COLSPAN="">&nbsp;</TD>
                <TD COLSPAN="3"></TD>
                <TD COLSPAN="">&nbsp;</TD>
            </TR>
            <tr>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("DateEnddateCaption")%></LABEL></TD>
                <td>
                    <%=mobjValues.DateControl("dEnddate_work", dEnddate_work, , GetLocalResourceObject("DateEnddate_workTooltip"))%>
                </td>
                <TD COLSPAN=""></TD>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("DateEnddateCaption")%></LABEL></TD>
                <td>
                    <%=mobjValues.DateControl("dEnddate_m", dEnddate_m, , GetLocalResourceObject("DateEnddate_mTooltip"))%>
                </td>
            </tr>
            <TR>
                <TD COLSPAN="2" CLASS="HighLighted" WIDTH=40%><LABEL ID=LABEL4>&nbsp;</LABEL></TD>
                <TD WIDTH="10%">&nbsp;</TD>
                <TD COLSPAN="3" CLASS="HighLighted" WIDTH=50%><LABEL ID=LABEL5><%= GetLocalResourceObject("AnchorCaption4") %></LABEL></TD>
                <TD WIDTH="10%">&nbsp;</TD>
            </TR>
            <TR>
                <TD COLSPAN="2"></TD>
                <TD COLSPAN="">&nbsp;</TD>
                <TD COLSPAN="3" CLASS="HorLine"></TD>
                <TD COLSPAN="">&nbsp;</TD>
            </TR>
             <tr>
                <TD COLSPAN="">&nbsp;</TD>
                <td>
                    &nbsp;
                </td>
                 <TD COLSPAN=""></TD>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("DateInitdateCaption")%></LABEL></TD>
                <td>
                    <%=mobjValues.DateControl("dInitialdate_em", dInitialdate_em, , GetLocalResourceObject("DateInitdate_emTooltip"))%>
                </td>
            </tr>
            <TR>
                <TD COLSPAN="2" ></TD>
                <TD COLSPAN="">&nbsp;</TD>
                <TD COLSPAN="3"></TD>
                <TD COLSPAN="">&nbsp;</TD>
            </TR>
            <tr>
                <TD COLSPAN="">&nbsp;</TD>
                <td>
                    &nbsp;
                </td>
                <TD COLSPAN=""></TD>
                <TD COLSPAN=""><LABEL><%=GetLocalResourceObject("DateEnddateCaption")%></LABEL></TD>
                <td>
                    <%=mobjValues.DateControl("dEnddate_em", dEnddate_em, , GetLocalResourceObject("DateEnddate_emTooltip"))%>
                </td>
            </tr>
        </TABLE>
        <BR>
    </form>
</body>
</html>
