<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eNetFrameWork" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mstrCodisplOri As String
Dim mstrCertype As Object


</script>
<%mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CAL815_K"
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CAL815_k")
'+ Se hace carga inicial de datos

%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 12 $|$$Date: 13/10/04 12:12 $|$$Author: Nvaplat28 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("CAL815", "CAL815_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
Response.Write(mobjMenu.setZone(1, "CAL815", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
<SCRIPT>

//- Variable para el control de versiones
	document.VssVersion="$$Revision: 12 $|$$Date: 13/10/04 12:12 $"
	
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
}
//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(Field){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){
		switch(Field.name){
			case "tcnPolicy":
				insDefValues("Policy_CA099","sCodispl=CAL815&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy= " + tcnPolicy.value)
				break;
		}
	}
}
//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%	campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
	with(document.forms[0]){
	    cbeOfficeAgen.value="";
	    cbeAgency.value="";
	}
	UpdateDiv('cbeOfficeAgenDesc','');
	UpdateDiv('cbeAgencyDesc','');
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
								cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
							else
								cbeAgency.Parameters.Param2.sValue = 0;
						}
				    }
					else{
	  					if(typeof(cbeOfficeAgen)!='undefined'){
							cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
							cbeOfficeAgen.Parameters.Param2.sValue = 0;
							cbeAgency.Parameters.Param1.sValue = (cbeOffice.value==''?0:cbeOffice.value);
							if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
								cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?0:cbeOfficeAgen.value);
							else
								cbeAgency.Parameters.Param2.sValue = 0;
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
			    }
			else{
			    cbeAgency.Parameters.Param1.sValue = 0;
			    cbeAgency.Parameters.Param2.sValue = 0;
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
            }
	    }	
	}
}

//% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para
//%	producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(sField){
			case "Agency":
				if(cbeAgency.value!="")
				    insDefValues(sField, "nAgency=" + cbeAgency.value + "&nOfficeAgen=" + cbeOfficeAgen.value +"&nOffice=" + cbeOffice.value,'/VTimeNet/Policy/PolicySeq')
				break;
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyTra.aspx?x=1">
	<BR></BR>
	<TABLE WIDTH=100%>
		<TR>
			<TD WIDTH=25%><LABEL ID=13901><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD>
				<%Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct"))
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=13909><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%
Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct")))
%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_1Caption"), CStr(1), "1")%></TD>
			
		</TR>
		<TR>
		   <TD>&nbsp;</TD>
		   <TD>&nbsp;</TD>
		   <TD>&nbsp;</TD>
		   <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcess", GetLocalResourceObject("optProcess_2Caption"), CStr(0), "2")%></TD>
		</TR>
		
		<TR>
		    <TD COLSPAN="3"></TD>			   
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%With mobjValues
	.TypeOrder = 1
	Response.Write(.PossiblesValues("cbeOffice", "Table9", 1, Session("nOffice"),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip")))
	.TypeOrder = 2
End With
%>
			</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), CStr(1), "1")%></TD>
			
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	If mstrCodisplOri = "CA767" Then
		Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)", True,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	End If
End With
%>
			</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), CStr(0), "2")%></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
			<TD COLSPAN="4"><%With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2,  , True,  ,  ,  ,  , "insInitialAgency(3)",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
	
End With
%>
			</TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">
		<TR>
           <TD WIDTH=25%><LABEL ID=0><%= GetLocalResourceObject("tcdNullDateCaption") %></LABEL></TD>
           <TD COLSPAN="4" ><%=mobjValues.DateControl("tcdNullDate",  ,  , GetLocalResourceObject("tcdNullDateToolTip"),  ,  ,  ,  , False)%></TD>
       </TR>
       <TR>
	       <TD COLSPAN="2"><%=mobjValues.CheckControl("chkNullDevRec", GetLocalResourceObject("chkNullDevRecCaption"), CStr(True))%><BR></TD>
       </TR>
       <TR>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkNullReceipt", GetLocalResourceObject("chkNullReceiptCaption"), CStr(1))%></TD>
            <TD COLSPAN="2"><%=mobjValues.HiddenControl("ValNullLetter", "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("nDay_payCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("nDay_pay", 3, CStr(30),  , GetLocalResourceObject("nDay_payToolTip"))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkRescReport", GetLocalResourceObject("chkRescReportCaption"), CStr(1), CStr(1))%></TD>
        </TR>
    </TABLE>
	<SCRIPT>insInitialAgency(1)</SCRIPT>
<%
Response.Write(mobjValues.HiddenControl("hddCodisplOri", Request.QueryString.Item("sCodisplOri")))


%>
</FORM>
</BODY>
</HTML>
<%

mobjValues = Nothing
If mstrCodisplOri = "CA767" Then
	Response.Write("<SCRIPT>insInitialAgency(3);</SCRIPT>")
	Response.Write("<SCRIPT>insChangeField('tcnPolicy');</SCRIPT>")
End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("CAL815_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




