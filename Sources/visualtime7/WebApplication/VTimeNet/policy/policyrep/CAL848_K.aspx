<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CAL848_K")

'- Objeto para el manejo particular de los datos de la página
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAL848_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 24/03/04 19:44 $|$$Author: Nvaplat15 $"
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insChangeField(nid){
//--------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		    if(cbeBranch.value!="" && cbeBranch.value>0){
			    valProduct.Parameters.Param1.sValue=cbeBranch.value;			
			    valProduct.disabled		= false;
			    btnvalProduct.disabled	= false;
			    }
			else{
			    valProduct.disabled		= true;
			    valProduct.value		= '';
				btnvalProduct.disabled	= true;
			}
	}
			
}
//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd) {
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
//+ Cambia la sucursal 
		switch(nInd){
		case 1:		
		    if (typeof(cbeOffice)!='undefined'){
		        if (cbeOffice.value != 0){
	  				if (typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
							cbeAgency.Parameters.Param1.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
						else
							cbeAgency.Parameters.Param1.sValue = 0;
					    cbeOfficeAgen.disabled    = false;
					    btncbeOfficeAgen.disabled = false;
					    cbeAgency.disabled		  = true;
						btncbeAgency.disabled	  = true;
					}
			    }
				else{
	  				if(typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0){
							cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
							cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
						}
						else{
							cbeAgency.Parameters.Param1.sValue = 0;
							cbeAgency.Parameters.Param2.sValue = 0;
							valIntermedia.Parameters.Param1.sValue = 0;
							valIntermedia.Parameters.Param2.sValue = 0;
							valIntermedia.Parameters.Param3.sValue = 0;
							valIntermedia.value = "";
							UpdateDiv('valIntermediaDesc','');
							cbeOfficeAgen.disabled     = true;
							btncbeOfficeAgen.disabled  = true;
							cbeAgency.disabled         = true;
							btncbeAgency.disabled      = true;}
					}
				}
			}
		    break;

//+ Cambia la oficina
		case 2:
			if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
			    {
                cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
			    cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
			    cbeOffice.value = cbeOfficeAgen_nBran_off.value;
			    cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
			    cbeAgency.disabled    = false;
				btncbeAgency.disabled    = false;
				valIntermedia.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
				valIntermedia.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);						
				valIntermedia.Parameters.Param3.sValue = 0;
				}
			else{
			    cbeAgency.Parameters.Param1.sValue = 0;
			    cbeAgency.Parameters.Param2.sValue = 0;
			    cbeAgency.disabled     = true;
				btncbeAgency.disabled  = true;
			    }
			break;
//+ Cambia la Agencia			
	    case 3:
	        if(cbeAgency.value != ""){
                cbeOffice.value = cbeAgency_nBran_off.value;
                if (cbeOfficeAgen.value == ''){
                    cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                    UpdateDiv('cbeOfficeAgenDesc',cbeAgency_sDesAgen.value);
                }
                cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
                cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
                valIntermedia.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
				valIntermedia.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);						
				valIntermedia.Parameters.Param3.sValue = (cbeAgency.value==''?0:cbeAgency.value);

            }
	    }	
	}
}
//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%                 campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
    with(document.forms[0]){
        cbeOfficeAgen.value="";
        cbeAgency.value="";
        cbeOfficeAgen_nBran_off.value = "";
        cbeAgency_nBran_off.value = "";
        cbeAgency_nOfficeAgen.value = "";
        cbeAgency_sDesAgen.value = "";
    }
    UpdateDiv('cbeOfficeAgenDesc','');
    UpdateDiv('cbeAgencyDesc','');
}
</SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CAL848", "CAL848_K.aspx", 1, vbNullString))
	'Response.Write mobjMenu.setZone(1,"CAL848", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR><BR>
<FORM METHOD="POST" NAME="CAL848_K" ACTION="valPolicyRep.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("CAL848", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH=100% BORDER=0 CELLSPACING=2 CELLPADDING=2 >
        <TR>
            <TD WIDTH=100% VALIGN=TOP>
                <TABLE BORDER=0 CELLSPACING=0 CELLPADDING=1 WIDTH=50%>
					<TR>
						<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
					</TR>
					</TR>
						<TD COLSPAN="2" CLASS="HorLine"></TD>
					<TR>
					</TR>
					<TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateFromCaption") %>&nbsp;</LABEL>
						<TD><%=mobjValues.DateControl("tcdDateFrom", "", True, GetLocalResourceObject("tcdDateFromToolTip"))%></TD>
			        </TR>
			        <TR>			
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcdDateToCaption") %>&nbsp;</LABEL></TD>
						<TD><%=mobjValues.DateControl("tcdDateTo", "", True, GetLocalResourceObject("tcdDateToToolTip"))%></TD>
					</TR>
				</TABLE>
		     </TD>
		 </TR>
		 <TR>
		 </TR>
		 <TR>
			<TD>
                <TABLE WIDTH=100% BORDER=0 CELLSPACING=1 CELLPADDING=1>
                    <TR>
						<TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
						<TD WIDTH=30%>
							<%With mobjValues
	.Parameters.Add("sBrancht", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0)
	.Parameters.Add("sBrancht_not", vbNullString, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0)
	Response.Write(mobjValues.PossiblesValues("cbeBranch", "tabTable10_t", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insChangeField(1);",  ,  , GetLocalResourceObject("cbeBranchToolTip"), eFunctions.Values.eTypeCode.eNumeric))
End With
%>
						</TD>
						<TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
						<TD WIDTH=30%>
								<%With mobjValues
	.Parameters.Add("nBranch", "40", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10)
	Response.Write(.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valProductToolTip")))
End With
%>
						</TD>                  
					</TR>
					
					<TR>
						<TD WIDTH=20%><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
						<TD WIDTH=30% COLSPAN="1">
						    <%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)", False,  , GetLocalResourceObject("cbeOfficeToolTip"))%>
						</TD>
						<TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
						<TD WIDTH=30% COLSPAN="1">
						<%
With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)", True,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
End With
%>
						</TD>
					</TR>
					<TR>
					    <TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
						<TD WIDTH=30% COLSPAN="1"><%
mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nBran_off",  ,  , True)
mobjValues.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
mobjValues.Parameters.ReturnValue("sDesAgen",  ,  , True)
Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, "", True,  ,  ,  ,  , "insInitialAgency(3)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
%>
						</TD>
					    <TD WIDTH=20% ><LABEL ID=0><%= GetLocalResourceObject("valIntermediaCaption") %></LABEL></TD>
					    <%
mobjValues.Parameters.Add("nBran_off", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nOfficeAgen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
%>
						<TD WIDTH=30%><%=mobjValues.PossiblesValues("valIntermedia", "tabintermsuc", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIntermediaToolTip"))%></TD>
					</TR>
					<TR>
						<TD><LABEL ID=0><%= GetLocalResourceObject("cbeOriginCaption") %></LABEL></TD>
                        <TD><%=mobjValues.PossiblesValues("cbeOrigin", "table5580", eFunctions.Values.eValuesType.clngWindowType, "", False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOriginToolTip"))%></TD>
                        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeStatQuotaCaption") %></LABEL></TD>
						<TD><%=mobjValues.PossiblesValues("cbeStatQuota", "table5526", eFunctions.Values.eValuesType.clngComboType, "", False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatQuotaToolTip"))%></TD>
                    </TR>	
                </TABLE>
            </TD>
        </TR>
        <TR>
            <TD VALIGN=TOP>
                
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
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("CAL848_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





