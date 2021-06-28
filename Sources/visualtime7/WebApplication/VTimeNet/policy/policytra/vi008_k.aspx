<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Variable para dejar (des)habilitada tipo de ejecucion
Dim mblnOptExeDisable As Boolean

'- Variable con opcion de ejecución por defecto
Dim mintOptExeValue As Byte
Dim mintOptExePre As String
Dim mintOptExeDef As String


'% insPreVI008: Inicializa los campos de la ventana
'---------------------------------------------------------------------------
Sub insPreVI008()
	'---------------------------------------------------------------------------	
	Dim lstrCodisplOri As String
	With Request
		lstrCodisplOri = .QueryString.Item("sCodisplOri")
		
		mblnOptExeDisable = lstrCodisplOri = "CA767"
		mintOptExeValue = 1
		
		If lstrCodisplOri = "CA767" Then
			'+ Actualizar
			If .QueryString.Item("nOperat") = "5" Then
				mintOptExeValue = 1
				'+ Preliminar
			Else
				'+ Definitiva
				mintOptExeValue = 2
			End If
		End If
		If mintOptExeValue = 1 Then
			mintOptExePre = "1"
		Else
			mintOptExeDef = "1"
		End If
	End With
	
	With Response
		.Write("<SCRIPT>")
		If mblnOptExeDisable Then
			.Write("var blnLetDisabled = true;")
		Else
			.Write("var blnLetDisabled = false;")
		End If
		.Write("</" & "Script>")
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vi008_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vi008_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

'+ Se invoca a carga inicial de datos
Call insPreVI008()
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaSCRIPT" SRC="/VTimeNet/SCRIPTs/GenFunctions.js"></SCRIPT>
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 391 Then
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
End If
%>
<SCRIPT> 
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $"        

//% insStateZone: se controla el estado de los controles de la página
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------

var lintIndex;
var error;
var nActions = new TypeActions();
var nMainAction = top.frames["fraSequence"].plngMainAction;
var CodisplOri = '<%=Request.QueryString.Item("sCodisplOri")%>'

    try {
		with (self.document.forms[0]) {
			for(lintIndex=0;(lintIndex < elements.length);lintIndex++){
				elements[lintIndex].disabled=false;
				if(self.document.images.length>0)
				    if(typeof(self.document.images["btn" + elements[lintIndex].name])!='undefined')
				       self.document.images["btn" + elements[lintIndex].name].disabled = elements[lintIndex].disabled
			}
			btn_tcdEffecdate.disabled=false;
			btnCheckPolicy.disabled=false;
			tctClient.disabled = true;
			tctClient_Digit.disabled = true;
			btntctClient.disabled = true;
			
			if (CodisplOri == "CA767" && optExeMode[1].checked == true){
				chkGenProposal.disabled = true;
			}
			
//+ Se inhabilitan los campos segun modo consulta 
		optExeMode[0].disabled = (nMainAction == nActions.clngActionQuery) || (blnLetDisabled);
		optExeMode[1].disabled = optExeMode[0].disabled;
		}
	} catch(error){}
}
//% insCancel: se controla la acción Cancelar de la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function InsChangeBranch(nBranch){
//------------------------------------------------------------------------------------------
	
	with (self.document.forms[0]){
		valProduct.value = '';
		valProduct.Parameters.Param1.sValue=nBranch;
		if (nBranch == '') {
			valProduct.disabled = true;
			btnvalProduct.disabled = valProduct.disabled;
			valProduct.value = '';
		}
		else {
			valProduct.disabled = false;
			btnvalProduct.disabled = valProduct.disabled;
		}
	}
}
//-------------------------------------------------------------------------------------------*/
function ShowVerifyData(){
//-------------------------------------------------------------------------------------------
	var nReduction
	with(self.document.forms[0]){
			nReduction=(optReduction[0].checked?1:2);
			
			if ((cbeBranch.value>0)&&
			    (valProduct.value>0)&&
			    (tcnPolicy.value>0))
   				if (valProduct_sBrancht.value == 1)
					ShowPopUp('/VTimeNet/Common/VIC001_K.aspx?sCertype=2&nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&nPolicy=' + tcnPolicy.value + '&nCertif=' + tcnCertif.value + '&nTypeProce=' + nReduction + '&dEffectDate=' + tcdEffecdate.value, 'VIC001_K', 500, 450)
				else
					ShowPolicyData('2', 
								   cbeBranch.value, 
								   valProduct.value,  
								   tcnPolicy.value,  
				                   tcnCertif.value, 
				                   nReduction, 
				                   tcdEffecdate.value)
			else
				alert('Ingrese todos los datos de la póliza');
	}
}
//%insCalExpirDate : Obtiene la fecha de vigencia del rescate.
//--------------------------------------------------------------------------------    
function insCalExpirDate(Field){
	if(Field.value != "")
		insDefValues("insCalExpirDate", 'sCodispl=VI008&nBranch='+self.document.forms[0].cbeBranch.value+'&nProduct='+self.document.forms[0].valProduct.value+'&nPolicy='+self.document.forms[0].tcnPolicy.value+'&nCertif='+self.document.forms[0].tcnCertif.value)
	else
		with (self.document.forms[0]){
			tcdEffecdate.value = "";
			tcdEffecdate.disabled=false;
			btn_tcdEffecdate.disabled=false;
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
		if (nInd == 1){
		    if (typeof(cbeOffice)!='undefined'){
		        if (cbeOffice.value != 0){
	  				if (typeof(cbeOfficeAgen)!='undefined'){
	  					cbeOfficeAgen.disabled = false;
						btncbeOfficeAgen.disabled = false;
						cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
							cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
						else
							cbeAgency.Parameters.Param2.sValue = 0;
					}
			    }
				else{
	  				if(typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.disabled = false;
						btncbeOfficeAgen.disabled = false;
						cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0){
							cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;}
						else{
							cbeAgency.Parameters.Param2.sValue = 0;}
					}
				}
			}
		}
//+ Cambia la oficina
		else
		{
			if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
			    {
                cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
			    cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
			    }
			else{
			    cbeAgency.Parameters.Param1.sValue = 0;
			    cbeAgency.Parameters.Param2.sValue = 0;
			    }
		}
		
		if(tcnPolicy.value>0){
			btnCheckPolicy.disabled=false;
			insDefValues("insCalExpirDate", 'sCodispl=VI008&nBranch='+self.document.forms[0].cbeBranch.value+'&nProduct='+self.document.forms[0].valProduct.value+'&nPolicy='+self.document.forms[0].tcnPolicy.value+'&nCertif='+self.document.forms[0].tcnCertif.value)		
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

//% ChangeOption: Se habilitan/deshabilitan el check de propuesta
//-------------------------------------------------------------------------------------------
function ChangeOption(){
//-------------------------------------------------------------------------------------------

	with(self.document.forms[0]){
		if (optExeMode[1].checked == true){
			chkGenProposal.disabled = true;
		}
		else{
			chkGenProposal.disabled = false;
		}
	}
}

</SCRIPT>
	 <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("VI008", "VI008_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="VI008" ACTION="ValPolicyTra.aspx?x=1">
<BR><BR>
    <% Response.Write(mobjValues.ShowWindowsName("VI008")) %>
	<TABLE WIDTH="100%" BORDER="0">
		<TR>
			<TD WIDTH="50%">
                <TABLE WIDTH="100%" BORDER="0">
					<TR>
						<TD COLSPAN=2 CLASS="HIGHLIGHTED"><LABEL ID=41007><A NAME="Tipo de listado"><%= GetLocalResourceObject("AnchorTipo de listadoCaption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD COLSPAN=2 CLASS="HORLINE"></TD>
					</TR>
                    <TR>
						<TD><%=mobjValues.OptionControl(0, "optExeMode", GetLocalResourceObject("optExeMode_CStr1Caption"), mobjValues.StringToType(mintOptExePre, eFunctions.Values.eTypeData.etdDouble), CStr(1), "ChangeOption();", True)%></TD>
                    </TR>
                    <TR>
						<TD><%=mobjValues.OptionControl(0, "optExeMode", GetLocalResourceObject("optExeMode_CStr2Caption"), mobjValues.StringToType(mintOptExeDef, eFunctions.Values.eTypeData.etdDouble), CStr(2), "ChangeOption();", True)%></TD>
                    </TR>
                </TABLE>
			</TD>
			<TD>
				<TABLE WIDTH="100%" BORDER="0">
					<TR>
						<TD COLSPAN=2 CLASS="HIGHLIGHTED"><LABEL ID=41007><A NAME="Tipo de listado"><%= GetLocalResourceObject("AnchorTipo de listado2Caption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD COLSPAN=2 CLASS="HORLINE"></TD>
					</TR>
                    <TR>
						<TD><%=mobjValues.CheckControl("chkGenReport", GetLocalResourceObject("chkGenReportCaption"), CStr(False), CStr(1),  , True,  , GetLocalResourceObject("chkGenReportToolTip"))%></TD>
                    </TR>
                    <TR>
						<TD><%=mobjValues.CheckControl("chkGenProposal", GetLocalResourceObject("chkGenProposalCaption"), CStr(False), CStr(1),  , True,  , GetLocalResourceObject("chkGenProposalToolTip"))%></TD>
                    </TR>
                </TABLE>
			</TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%" BORDER="0">	
		<TR>
			<TD COLSPAN=4>
                <TABLE WIDTH="100%" BORDER="0">
					<TR>
						<TD COLSPAN=2 CLASS="HIGHLIGHTED"><LABEL ID=41007><A NAME="Tipo de listado"><%= GetLocalResourceObject("AnchorTipo de listado3Caption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD COLSPAN=2 CLASS="HORLINE"></TD>
					</TR>
                    <TR>
						<TD><%=mobjValues.OptionControl(0, "optReduction", GetLocalResourceObject("optReduction_CStr1Caption"), CStr(1), CStr(1),  , True)%></TD>
						<TD><%=mobjValues.OptionControl(0, "optReduction", GetLocalResourceObject("optReduction_CStr2Caption"),  , CStr(2),  , True)%></TD>
                    </TR>
                </TABLE>
           </TD>
        </TR>
        <TR>
			<TD COLSPAN=4></TD>
        </TR>
        <TR>
			<TD COLSPAN=4 CLASS="HORLINE"></TD>
        </TR>
        <TR>
			<TD COLSPAN=4></TD>
        </TR>
        <TR>
			<TD WIDTH="10%"><LABEL ID="0"><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct",  ,  ,  , "InsChangeBranch(this.value)", True)%></TD>
			<TD WIDTH="15%"><LABEL ID="0"><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("sBrancht", False, "Ramo técnico", True)
	Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eValuesType.clngWindowType, True, Request.QueryString.Item("nProduct")))
End With
%>
			</TD>
        </TR>
        <TR>
            <TD><LABEL ID="13918"><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
            <%=mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insCalExpirDate(this);", True)%>
            </TD>
            
            <TD><LABEL ID="13917"><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCertif", 10, Request.QueryString.Item("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID="13875"><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Request.QueryString.Item("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkNulling", GetLocalResourceObject("chkNullingCaption"),  , CStr(1),  , True,  , GetLocalResourceObject("chkNullingToolTip"))%></TD>
        </TR>
		<TR>
		    <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD><%
mobjValues.TypeOrder = 1
Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", 1, Session("nOffice"),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip")))
mobjValues.TypeOrder = 2
%>
			</TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, Session("nOfficeagen"), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
End With
%>
			</TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
			<TD><%
mobjValues.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, Session("nAgency"), True,  ,  ,  ,  , "ShowChangeValues(""Agency"")",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
%>
			</TD>
		</TR>
        <TR>
			<TD COLSPAN="4">
                <TABLE WIDTH="100%" BORDER="0">
					<TR>
						<TD COLSPAN=4 CLASS="HIGHLIGHTED"><LABEL ID=41007><A NAME="Tipo de listado"><%= GetLocalResourceObject("AnchorTipo de listado4Caption") %></A></LABEL></TD>
					</TR>
					<TR>
						<TD COLSPAN=4 CLASS="HORLINE"></TD>
					</TR>
                    <TR>
						<TD WIDTH="28%"><%=mobjValues.AnimatedButtonControl("btnCheckPolicy", "/VTimeNet/images/btn_ValuesOff.png", "Datos de verificación",  , "ShowVerifyData()", True)%>&nbsp;<LABEL ID=101993><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
						<TD COLSPAN="2" WIDTH="18%"><LABEL ID="0"><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
						<TD><%=mobjValues.ClientControl("tctClient", "",  , GetLocalResourceObject("tctClientToolTip"),  , True, "tctCliename", False,  ,  ,  ,  ,  , True)%></TD>
                    </TR>
                </TABLE>
            </TD>
        </TR>
        <TR>
			<TD COLSPAN=4 CLASS="HORLINE"></TD>
        </TR>
        <% =mobjValues.HiddenControl("hddProponum", Request.QueryString.Item("nPolicy"))%>
        <% =mobjValues.HiddenControl("hddCodisplOri", Request.QueryString.Item("sCodisplOri"))%>        
        <% =mobjValues.HiddenControl("hddOperat", Request.QueryString.Item("nOperat"))%>
        
    </TABLE>
	<SCRIPT>insInitialAgency(1)</SCRIPT>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vi008_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




