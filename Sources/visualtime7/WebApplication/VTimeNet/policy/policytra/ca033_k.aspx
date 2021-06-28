<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility
Dim mobjNull_condi As ePolicy.Null_condi
Dim mstrCodisplOri As String


'% insPreCA033: Permite obtener la información de necesaria para el manejo de la ventana
'---------------------------------------------------------------------------
Sub insPreCA033()
	'---------------------------------------------------------------------------	
	With Request
		If IsNothing(.QueryString("sCodisplOri")) Then
			mstrCodisplOri = "CA033"
		Else
			mstrCodisplOri = .QueryString.Item("sCodisplOri")
		End If
		Call mobjNull_condi.insPreCA033_k(mstrCodisplOri, Session("nOperat"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		
		If mobjNull_condi.nAgency = eRemoteDB.Constants.intNull Then
			mobjNull_condi.nAgency = Session("nAgency")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca033_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca033_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mobjNull_condi = New ePolicy.Null_condi
'+ Se hace carga inicial de datos
Call insPreCA033()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 10-05-06 12:12 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//% ShowPoliza: Se encarga de validar el tipo de Póliza
//--------------------------------------------------------------------------------------------
function ShowPoliza(){
//--------------------------------------------------------------------------------------------
	if (self.document.forms[0].cbeBranch.value!=0 && 
		self.document.forms[0].valProduct.value!=0 && 
		self.document.forms[0].tcnPolicy.value)
		insDefValues('ValPolitype', "nBranch=" + self.document.forms[0].cbeBranch.value + 
									"&nProduct=" + self.document.forms[0].valProduct.value + 
									"&nPolicy=" + self.document.forms[0].tcnPolicy.value);
	else{
		self.document.forms[0].tcnCertif.disabled=false;
		self.document.forms[0].tcnCertif.value='';
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

//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
//+ Cambia la sucursal 
        if (nInd == 1){
            cbeOfficeAgen.value = '';
            UpdateDiv('cbeOfficeAgenDesc','');
            cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
            cbeOfficeAgen.Parameters.Param2.sValue = '0';
            cbeAgency.value = '';
            UpdateDiv('cbeAgencyDesc','');
            cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
            cbeAgency.Parameters.Param2.sValue = '0';
        }
//+ Cambia la oficina 
        else{
            if (nInd == 2){
                if(cbeOfficeAgen.value != ''){
                    cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                    cbeAgency.value = '';
                    UpdateDiv('cbeAgencyDesc','');
                    cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
                    cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
                }
                else{
                    cbeAgency.Parameters.Param2.sValue = '0';
                }
            }
//+ Cambia la Agencia
            else{
                if (nInd == 3){
                    if(cbeAgency.value != ''){
                        cbeOffice.value = cbeAgency_nBran_off.value;
                        if (cbeOfficeAgen.value == ''){
                            cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                            UpdateDiv('cbeOfficeAgenDesc',cbeAgency_sDesAgen.value);
                        }
                        cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value==''?'0':cbeOfficeAgen.value);
                    }
                }
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
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA033", "CA033_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>

<SCRIPT>
//%insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	var frm = self.document.forms[0];

	<%
'+Se hace el sgte manejo en vez de asignar directamente el valor	
'+ya que podría retornar 'Verdadero' o 'True', 
'+palabras que no existen en JavaScript
If mobjNull_condi.DefaultValueCA033("optExecuteEnabled") Then%>
	var bOptEnabled = true;
	<%Else%>
	var bOptEnabled = false;
	<%End If%>
	frm.cbeBranch.disabled = false;
	frm.valProduct.disabled = false;
	frm.btnvalProduct.disabled = false;
	frm.optExecute[0].disabled = !bOptEnabled;
	frm.optExecute[1].disabled = !bOptEnabled;
	frm.tcnPolicy.disabled = false;
	frm.tcnCertif.disabled = false;
	frm.cbeOffice.disabled = false;
	frm.cbeOfficeAgen.disabled = false;
	frm.btncbeOfficeAgen.disabled = false;
	frm.cbeAgency.disabled = false;
	frm.btncbeAgency.disabled = false;

//    insInitialAgency(1);
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmNullPolicy" ACTION="ValPolicyTra.aspx?sCodisplOri=<%=mstrCodisplOri%>&nProponum=<%=Request.QueryString.Item("npolicy")%>">
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=20%><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH=30%><%Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct",  ,  ,  , "ShowPoliza()", True))%></TD>
            <TD WIDTH=10%>&nbsp;</TD>
            <TD WIDTH=40% COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><A NAME="Ejecucion"><%= GetLocalResourceObject("AnchorEjecucionCaption") %></A></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD COLSPAN="2" CLASS="HORLINE"></TD>
		</TR>
		<TR>
			<TD><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct"),  ,  ,  , "ShowPoliza()"))%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), mobjNull_condi.DefaultValueCA033("optExecutePre"), "1",  , True)%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD>
				<%If mstrCodisplOri = "CA767" Then
                        Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPropoNum"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  ,  , True))
                    Else
                        Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "ShowPoliza()", True))
                    End If
%>
            </TD>
            <TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), mobjNull_condi.DefaultValueCA033("optExecuteDef"), "2",  , True)%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
            <TD> <%=mobjValues.NumericControl("tcnCertif", 6, Request.QueryString.Item("nCertif"),  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <TD COLSPAN="3">&nbsp;</TD>
		</TR>
		<TR>
		    <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD COLSPAN="4"><%=mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)", True,  , GetLocalResourceObject("cbeOfficeToolTip"))%></TD>
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
			<TD COLSPAN="4"><%
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
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
			<TD COLSPAN="4"><%
mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nBran_off",  ,  , True)
mobjValues.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
mobjValues.Parameters.ReturnValue("sDesAgen",  ,  , True)
Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(mobjNull_condi.nAgency), True,  ,  ,  ,  , "insInitialAgency(3)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
%>
			</TD>
		</TR>
    </TABLE>
    <%=mobjValues.HiddenControl("hdddStardate", "")%>
    <%=mobjValues.HiddenControl("cbeTransactio", CStr(1))%>
</BODY>
	<%
mobjNull_condi = Nothing
mobjValues = Nothing
%> 
</FORM>
</HTML>
<%
Response.Write("<SCRIPT>insInitialAgency(3)</SCRIPT>")
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("ca033_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







