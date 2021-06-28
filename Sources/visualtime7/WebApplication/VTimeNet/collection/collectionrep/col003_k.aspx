<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**+ Object for the handling of the general functions of load of values.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues



'**% insPreCOL003: The controls of the window are loaded.  
'% insPreCOL003: Se cargan los controles de la ventana.
'----------------------------------------------------------------------------
Private Sub insPreCOL003()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	")

	mobjValues = Nothing
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col003_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col003_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 28/11/03 17:41 $|$$Author: Nvaplat56 $"
    </SCRIPT>




<SCRIPT>

//------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------
{
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------
{
	return true;
}
//------------------------------------------------------------------------------
function insFinish()
//------------------------------------------------------------------------------
{
	return true;
}

//% ChangeAgreementStatus: Se habilta la fecha de pago sólo si se pide lo pendiente..
function insChangeLimitDateStatus()
//------------------------------------------------------------------------------
{
	with (self.document.forms[0]){
		if(typeof(elements["cbeNStatus_Pre"])!='undefined')	{
				
			if (elements["cbeNStatus_Pre"].value == '1'	||
			    elements["cbeNStatus_Pre"].value == '4') {			
				elements["tcdLimitDate"].disabled = false
				elements["btn_tcdLimitDate"].disabled = false
			}	
			else {
				elements["tcdLimitDate"].disabled = true
				elements["btn_tcdLimitDate"].disabled = true
			}
		}
	}
	return true;
}

//% ChangeAgreementStatus: Habilita el control de convenio sólo si la vía de pago es Planilla.
function insChangeAgreementStatus()
//------------------------------------------------------------------------------
{

	with  (self.document.forms[0]){
		if(typeof(elements["cbeWayPay"])!='undefined'){
				
				if(elements["cbeWayPay"].value == '3'){			
					elements["tcnAgreement"].disabled = false
				}	
				else{
					elements["tcnAgreement"].disabled = true
				}
		}
	}
	return true;
}
//% InsChangeValues: Asigna sucursal y agencia perteneciente a un intermediario
function InsChangeValues(obj)
//------------------------------------------------------------------------------
{
	var strParams;
	strParams = "nIntermed=" + obj.value
	insDefValues("SucAgen",strParams,'/VTimeNet/collection/collectionrep');
}

</SCRIPT>
	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "COL003_K.aspx", 1, ""))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="post" ID="FORM" NAME="COL003" ACTION="valCollectionRep.aspx?sMode=1">
	<BR><BR>	
    	<%Response.Write(mobjValues.ShowWindowsName("COL003", Request.QueryString.Item("sWindowDescript")))%>
	<BR><BR>
	<TABLE WIDTH="100%">
        <TR>
		    <TD WIDTH="45%" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD></TD>
		    <TD WIDTH="45%" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		
		<TR>
		    <TD CLASS="HorLine"></TD>
			<TD></TD>
		    <TD CLASS="HorLine"></TD>
		</TR>
		
		<TR>
		    <TD><%=mobjValues.OptionControl(0, "optDetail", GetLocalResourceObject("optDetail_1Caption"),  , "1",  , False,  , GetLocalResourceObject("optDetail_1ToolTip"))%> </TD>
		    <TD></TD>
			<TD><%=mobjValues.OptionControl(0, "optReceiptType", GetLocalResourceObject("optReceiptType_1Caption"),  , "1",  , False,  , GetLocalResourceObject("optReceiptType_1ToolTip"))%> </TD>
        </TR>
        
		<TR>
		    <TD><%=mobjValues.OptionControl(0, "optDetail", GetLocalResourceObject("optDetail_2Caption"), CStr(1), "2",  , False,  , GetLocalResourceObject("optDetail_2ToolTip"))%> </TD>
		    <TD></TD>
		    <TD><%=mobjValues.OptionControl(0, "optReceiptType", GetLocalResourceObject("optReceiptType_2Caption"),  , "2",  , False,  , GetLocalResourceObject("optReceiptType_2ToolTip"))%> </TD>
        </TR>
        
        <TR>
		    <TD></TD>
		    <TD></TD>
		    <TD><%=mobjValues.OptionControl(0, "optReceiptType", GetLocalResourceObject("optReceiptType_3Caption"), CStr(1), "3",  , False,  , GetLocalResourceObject("optReceiptType_3ToolTip"))%> </TD>
        </TR>        
    </TABLE>
	<BR>
	
	<TABLE WIDTH="100%">
		<TR>
            <TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
			<TD WIDTH="34%"><%=mobjValues.PossiblesValues("cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeAgencyToolTip"))%></TD>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD WIDTH="34%"><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeBranchToolTip"))%></TD>
		</TR>
		<TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("cbeZoneCaption") %></LABEL></TD>
			<TD WIDTH="34%"><%=mobjValues.PossiblesValues("cbeZone", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeZoneToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
			
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeWayPayCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeWayPay", "Table5002", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeAgreementStatus()", False,  , GetLocalResourceObject("cbeWayPayToolTip"))%></TD>
			<TD><LABEL ID=10><%= GetLocalResourceObject("tcnAgreementCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAgreement", 10, "",  ,  ,  ,  ,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeNStatus_PreCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeNStatus_Pre", "Table19", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeLimitDateStatus()", False,  , GetLocalResourceObject("cbeNStatus_PreToolTip"))%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdLimitDateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdLimitDate",  ,  , GetLocalResourceObject("tcdLimitDateToolTip"),  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD><%=mobjValues.CheckControl("chkBulletins", GetLocalResourceObject("chkBulletinsCaption"),  ,  ,  , False,  , GetLocalResourceObject("chkBulletinsToolTip"))%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valAgentCodeCaption") %></LABEL></TD>
			<TD>
				<%
With mobjValues.Parameters
	.Add("nIntertyp", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With

Response.Write(mobjValues.PossiblesValues("valAgentCode", "TabIntermedia1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "InsChangeValues(this);",  ,  , GetLocalResourceObject("valAgentCodeToolTip"),  , 2))
%>
			</TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valSupCodeCaption") %></LABEL></TD>
			<TD>
				<%
With mobjValues.Parameters
	.Add("nIntertyp", 10, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With

Response.Write(mobjValues.PossiblesValues("valSupCode", "TabIntermedia1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "", False,  , GetLocalResourceObject("valSupCodeToolTip"),  , 2))
%>
			</TD>
		</TR>
	</TABLE>			

</FORM>

</BODY>

</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col003_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




