<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjSuspend As ePolicy.Suspend


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjSuspend = New ePolicy.Suspend

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjMenu.setZone(2, "CA035", "CA035.aspx"))
	.Write(mobjValues.StyleSheet())
End With
mobjMenu = Nothing
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>





<SCRIPT>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insUpdHeader: Refresca el Código de cliente que se introdujo en el Header
//-------------------------------------------------------------------------------------------
function insUpdHeader(lstrCliename){
//-------------------------------------------------------------------------------------------
    var lblnAgain = true    
    if (typeof(top.fraHeader.document)!='undefined')		
        if (typeof(top.fraHeader.document.forms[0])!='undefined'){
		  	    top.fraHeader.UpdateDiv('tctCliename', lstrCliename);
                lblnAgain = false         
            }
   if (lblnAgain)
      setTimeout("insUpdHeader(lstrCliename)",50)
}
//% EnabledNextReceipt: Habilita el campo fecha de próx. facturación
//-------------------------------------------------------------------------------------------
function EnabledNextReceipt(){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0])
		elements["tcdNextReceip"].disabled = false
}
//% CancelErrors: se realiza el manejo en caso de errores
//-------------------------------------------------------------------------------------------
function CancelErrors(){self.history.back}
//-------------------------------------------------------------------------------------------
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCA035" ACTION="valPolicyTra.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Call mobjSuspend.insPreCA035("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
Response.Write("<SCRIPT>setTimeout(""insUpdHeader('" & mobjSuspend.mclsPolicy.sClient & "-" & mobjSuspend.mclsPolicy.sCliename & "')"",50)</SCRIPT>")
%>
<%=mobjValues.ShowWindowsName("CA035")%>
<TABLE WIDTH="100%">
    <TR>
		<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Suspensión"><%= GetLocalResourceObject("AnchorSuspensiónCaption") %> </A></LABEL></TD>
		<TD WIDTH=10%>&nbsp;</TD>
		<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><a NAME="Tipo"><%= GetLocalResourceObject("AnchorTipoCaption") %></A></LABEL></TD>
    </TR>
    <TR>
		<TD COLSPAN="2" CLASS="HorLine"></TD>
		<TD></TD>
		<TD COLSPAN="2" CLASS="HorLine"></TD>
    </TR>
    <TR>      
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdExpirdatCaption") %></LABEL></TD>
		<TD><%=mobjValues.DateControl("tcdExpirdat", mobjValues.DatetoString(mobjSuspend.dExpirdat),  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  ,  , 2)%> </TD>
		<TD>&nbsp;</TD>
		<TD><%=mobjValues.OptionControl(2, "optReceipt", GetLocalResourceObject("optReceipt_CStr2Caption"),  , CStr(2),  ,  , 7, GetLocalResourceObject("optReceipt_CStr2ToolTip"))%></TD>		
	</TR>
	<TR>        
		<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCode_susCaption") %></LABEL></TD>
		<TD><%=mobjValues.PossiblesValues("cbeCode_sus", "Table151", eFunctions.Values.eValuesType.clngComboType, CStr(mobjSuspend.nCode_sus),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCode_susToolTip"), eFunctions.Values.eTypeCode.eNumeric, 3)%> </TD>
		<TD>&nbsp;</TD>
		<TD><%=mobjValues.OptionControl(3, "optReceipt", GetLocalResourceObject("optReceipt_CStr3Caption"),  , CStr(3),  ,  , 8, GetLocalResourceObject("optReceipt_CStr3ToolTip"))%></TD> 
	</TR>
	<TR>
		<TD>&nbsp;</TD>
		<TD>&nbsp;</TD>
		<TD>&nbsp;</TD>
		<TD><%=mobjValues.OptionControl(1, "optReceipt", GetLocalResourceObject("optReceipt_CStr0Caption"), CStr(1), CStr(0),  ,  , 9, GetLocalResourceObject("optReceipt_CStr0ToolTip"))%></TD>  
	</TR>
    <TR>
		<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Nueva"><%= GetLocalResourceObject("AnchorNuevaCaption") %> </a></LABEL></TD>
		<TD WIDTH=10%>&nbsp;</TD>
		<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=0><a NAME="Carta"><%= GetLocalResourceObject("AnchorCartaCaption") %></a></LABEL></TD>
    </TR>
    <TR>
		<TD COLSPAN="2" CLASS="HorLine"></TD>
		<TD></TD>
		<TD COLSPAN="2" CLASS="HorLine"></TD>
    </TR>
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdStartdateCaption") %></LABEL></TD>
		<TD><%If mobjSuspend.mclsPolicy.sColtimre <> "1" Then
	Response.Write(mobjValues.DateControl("tcdStartdate", mobjValues.StringtoType(CStr(mobjSuspend.mclsPolicy.dStartDate), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdStartdateToolTip"),  ,  ,  , "EnabledNextReceipt();",  , 4))
Else
	Response.Write(mobjValues.DateControl("tcdStartdate", mobjValues.StringtoType(CStr(mobjSuspend.mclsPolicy.dStartDate), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdStartdateToolTip"),  ,  ,  ,  , True, 4))
End If%> </TD>      
		<TD>&nbsp;</TD>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tctMailNumCaption") %> </LABEL></TD>
		<TD><%=mobjValues.TextControl("tctMailNum", 6, mobjSuspend.sMailnumb,  , GetLocalResourceObject("tctMailNumToolTip"),  ,  ,  ,  ,  , 10)%></TD>	     
    </TR>
    <TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdPolExpirdateCaption") %></LABEL></TD>
		<TD><%If mobjSuspend.mclsPolicy.sColtimre <> "1" Then
	Response.Write(mobjValues.DateControl("tcdPolExpirdate", mobjValues.StringtoType(CStr(mobjSuspend.mclsPolicy.dExpirdat), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdPolExpirdateToolTip"),  ,  ,  , "EnabledNextReceipt();",  , 5))
Else
	Response.Write(mobjValues.DateControl("tcdPolExpirdate", mobjValues.StringtoType(CStr(mobjSuspend.mclsPolicy.dExpirdat), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdPolExpirdateToolTip"),  ,  ,  ,  , True, 5))
End If%> </TD>      
		<TD>&nbsp;</TD>
		<TD>&nbsp;</TD>  
    </TR>
    <TR>
		<TD>&nbsp;</TD>  
		<TD>&nbsp;</TD>
		<TD>&nbsp;</TD>
		<TD>&nbsp;</TD>		
    </TR>    
  	<TR>  	
		<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Fecha"><%= GetLocalResourceObject("AnchorFechaCaption") %> </a></LABEL></TD>
		<TD WIDTH=10%>&nbsp;</TD>
		<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><a NAME="Nota"><%= GetLocalResourceObject("AnchorNotaCaption") %></a></LABEL></TD>
	</TR>
	<TR>  
		<TD COLSPAN="2" CLASS="HorLine"></TD>
		<TD></TD>
		<TD COLSPAN="2" CLASS="HorLine"></TD>
	</TR>
	<TR>
		<TD><LABEL ID=0><%= GetLocalResourceObject("tcdNextReceipCaption") %></LABEL></TD>
		<TD><%If mobjSuspend.mclsPolicy.sColtimre <> "1" Then
	Response.Write(mobjValues.DateControl("tcdNextReceip", mobjValues.StringtoType(CStr(mobjSuspend.mclsPolicy.dNextReceip), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdNextReceipToolTip"),  ,  ,  ,  , True, 6))
Else
	Response.Write(mobjValues.DateControl("tcdNextReceip", mobjValues.StringtoType(CStr(mobjSuspend.mclsPolicy.dNextReceip), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdNextReceipToolTip"),  ,  ,  ,  , True, 6))
End If%> </TD>      
		<TD>&nbsp;</TD>	
		<TD></TD>
		<TD><%
Response.Write(mobjValues.ButtonNotes("SCA2-11", mobjSuspend.nNotenum, False, mobjValues.ActionQuery))
%> <TD>
	</TR>
  </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjSuspend = Nothing

If CStr(Session("sOriginalForm")) <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields();</script>")
End If
%>
		




