<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjNetFrameWork As eNetFrameWork.Layout

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim lclsCtrol_date As eGeneral.Ctrol_date
    Dim mdEffecdate As String

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL960_k")
Response.Cache.SetCacheability(HttpCacheability.NoCache)

    mobjValues = New eFunctions.Values
    lclsCtrol_date = New eGeneral.Ctrol_date
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")

mobjValues.sCodisplPage = "AGL960_k"
mobjMenu = New eFunctions.Menues
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



    
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 15/10/03 16:40 $|$$Author: Nvaplat61 $"
    //% insCancel: se controla la acción Cancelar de la página
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    } 
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("AGL960", "AGL960_k.aspx", 1, vbNullString))
	.Write(mobjMenu.setZone(1, "AGL960", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAGL960" ACTION="ValAgentRep.aspx?smode=1">
<%
    If lclsCtrol_date.Find(107) Then
        mdEffecdate = mobjValues.TypeToString(lclsCtrol_date.dEffecdate, eFunctions.Values.eTypeData.etdDate)
        mdEffecdate = Today
    End If
%>

	<BR><BR>
    	<%=mobjValues.ShowWindowsName("AGL960", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="1" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption1") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="1" CLASS="HorLine"></TD>
        </TR>
        <TR>
        </TR>
        <TR>
        </TR>
        <TR>
            <TD><%= mobjValues.OptionControl(0, "optproccess", GetLocalResourceObject("optProccess_1Caption"), "1", "1", , , , GetLocalResourceObject("optProccess_1ToolTip"))%></TD>
        </TR>    
        <TR>
            <TD><%= mobjValues.OptionControl(0, "optproccess", GetLocalResourceObject("optProccess_2Caption"), , "2", , , , GetLocalResourceObject("optProccess_2ToolTip"))%></TD>                        
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("nContrat_PayCaption") %></LABEL></TD>

            <TD><%=mobjValues.PossiblesValues("nContrat_Pay", "tabcontrat_pay", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("nContrat_PayToolTip"))%></TD>    
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcdProcess_dateCaption") %></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdProcess_date", CStr(mdEffecdate), , GetLocalResourceObject("tcdProcess_dateToolTip"))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%=GetLocalResourceObject("tcdValue_dateCaption")%></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdValue_date", CStr(Today), True, GetLocalResourceObject("tcdValue_dateToolTip")) %></TD>
        </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%

lclsCtrol_date = Nothing
mobjValues = Nothing
mobjMenu = Nothing
%>
<%
Call mobjNetFrameWork.FinishPage("AGL960_k")
mobjNetFrameWork = Nothing
%>




