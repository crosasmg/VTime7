<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim sCodispl As String
Dim sCodisplPage As String
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%
sCodispl = Trim(Request.QueryString.Item("sCodispl"))
sCodisplPage = LCase(sCodispl) & "_k"

Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(sCodisplPage)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = sCodisplPage
'~End Body Block VisualTimer Utility

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<HTML>
<HEAD>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//% insStateZone: se manejan los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone()
//------------------------------------------------------------------------------------------
{
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel()
//------------------------------------------------------------------------------------------
{		    
	return true;
}

//%insDefValue:Permite asignarle "0,00" al control en caso de no haber indicado
//%valor numerico al campo
//------------------------------------------------------------------------------------------
function insDefValue(Field){
//------------------------------------------------------------------------------------------
    if(Field.value=='')
        self.document.forms[0].tcnExcess.value='0'
}


//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		switch (objField.name){
		case 'cbeStatQuota':
			cbeWaitCode.disabled=objField.value!=1;
			if (cbeWaitCode.disabled) cbeWaitCode.value=0;
			break;
		case 'cbeBranch':
			valProduct.Parameters.Param1.sValue=objField.value;	
			valProduct.Parameters.Param2.sValue=0;			
					
			break;
		case 'cbeInsur_Area':
			if (objField.value==1) { // area de generales
				cbeBranch.Parameters.Param1.sValue='';
				cbeBranch.Parameters.Param2.sValue='1';
			}
			else{ // vida
				cbeBranch.Parameters.Param1.sValue=1;
				cbeBranch.Parameters.Param2.sValue='';
			}
			valProduct.Parameters.Param1.sValue = '';
			break;
		}
    }
}



</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu(sCodispl, sCodispl & "_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM method="post" id="FORM" name="Policy" action="valPolicyRep.aspx?mode=1">

    <BR></BR>
	    
	<%Response.Write(mobjValues.ShowWindowsName("CAL01501", Request.QueryString.Item("sWindowDescript")))%>

    
   
    <TABLE WIDTH=100% BORDER=0 CELLSPACING=2 CELLPADDING=2 >
        <TR>
            <TD WIDTH=50% VALIGN=TOP>
                <TABLE WIDTH=100% BORDER=0 CELLSPACING=1 CELLPADDING=1>
                    <TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_AreaCaption") %></LABEL></TD>
						<TD>
							<%Response.Write(mobjValues.PossiblesValues("cbeInsur_Area", "table5001", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  , "insChangeField(this);",  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip")))
%>
						</TD>
					</TR>
					<TR>
					    <TD><LABEL ID=LABEL1><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
						<TD><%=mobjValues.PossiblesValues("cbeAgency", "table5556", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeAgencyToolTip"))%></TD>
					</TR>  
					<TR>
					    <TD><LABEL ID=LABEL2><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
						<TD><%=mobjValues.PossiblesValues("valIntermedia", "tabIntermedia", eFunctions.Values.eValuesType.clngWindowType, "", False,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valIntermediaToolTip"))%></TD>
					</TR>
					<TR>
						<TD><LABEL ID=LABEL3><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
						<TD>
							<%With mobjValues
	.Parameters.Add("sBrancht", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0)
	.Parameters.Add("sBrancht_not", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0)
	Response.Write(mobjValues.PossiblesValues("cbeBranch", "tabTable10_t", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insChangeField(self.document.forms[0].cbeBranch);",  ,  , GetLocalResourceObject("cbeBranchToolTip"), eFunctions.Values.eTypeCode.eNumeric))
End With
%>
						</TD>
					</TR>
					<TR>
						<TD><LABEL ID=LABEL4><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
						<TD>
								<%With mobjValues
	.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10)
	.Parameters.Add("nProduct", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valProduct", "tabProdmaster", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valProductToolTip")))
End With
%>
						</TD>                  
					</TR>
					<TR>
						<TD><LABEL ID=LABEL5><%= GetLocalResourceObject("cbeStatQuotaCaption") %></LABEL></TD>
						<TD><%=mobjValues.PossiblesValues("cbeStatQuota", "table5526", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  , "insChangeField(this);",  ,  , GetLocalResourceObject("cbeStatQuotaToolTip"))%></TD>
					</TR>	
					<TR>
						<TD><LABEL ID=LABEL6><%= GetLocalResourceObject("cbeWaitCodeCaption") %></LABEL></TD>
						<TD><%=mobjValues.PossiblesValues("cbeWaitCode", "Tab_waitPo", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeWaitCodeToolTip"))%></TD>

					</TR>					
  									

                </TABLE>
            </TD>
            <TD WIDTH=50% VALIGN=TOP>
                <TABLE WIDTH=100% BORDER=0 CELLSPACING=1 CELLPADDING=1>
                    <TR>
                    <TD CLASS="HighLighted" ><LABEL ID=LABEL7><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
                    </TR>
                    <TR>
                        <TD CLASS="HorLine"></TD>
                    </TR>
                    <TR>
						<TD><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_CStr3Caption"), CStr(0), CStr(3))%></TD>
					</TR>
					<TR>
					    <TD><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_CStr1Caption"), CStr(1), CStr(1))%></TD>
					</TR>
						<TR>
						<TD><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_CStr4Caption"), CStr(0), CStr(4))%></TD>
					</TR>
					<TR>
						<TD><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_CStr5Caption"), CStr(0), CStr(5))%></TD>
					</TR>
					<TR>
						<TD><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_CStr6Caption"), CStr(0), CStr(6))%></TD>
					</TR>
					<TR>
						<TD><%=mobjValues.OptionControl(0, "optCertype", GetLocalResourceObject("optCertype_CStr7Caption"), CStr(0), CStr(7))%></TD>
					</TR>
                </TABLE>
            </TD>
        </TR>
        <TR>
            <TD VALIGN=TOP>
                <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=1 WIDTH=100%>
					<TR>
						<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=LABEL8><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
					</TR>
					</TR>
						<TD COLSPAN="4" CLASS="HorLine"></TD>
					<TR>
					</TR>
						<TD><LABEL ID=LABEL9><%= GetLocalResourceObject("tcdDateFromCaption") %>&nbsp;</LABEL>
						<TD><%=mobjValues.DateControl("tcdDateFrom", "", True, GetLocalResourceObject("tcdDateFromToolTip"))%></TD>
						<TD><LABEL ID=LABEL10><%= GetLocalResourceObject("tcdDateToCaption") %>&nbsp;</LABEL></TD>
						<TD><%=mobjValues.DateControl("tcdDateTo", "", True, GetLocalResourceObject("tcdDateToToolTip"))%></TD>
					</TR>
				</TABLE>
            <TD VALIGN=TOP>
                
			</TD>    
        </TR>
    </TABLE>
	<%=mobjValues.HiddenControl("hddUsercode", Session("nUsercode"))%>
   
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.14
Call mobjNetFrameWork.FinishPage(sCodisplPage)
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




