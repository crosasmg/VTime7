<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eOptionSystem" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

Dim mobjOptionSystem As eGeneral.Opt_system


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjOptionSystem = New eGeneral.Opt_system
mobjValues.sCodisplPage = "MAM001"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<SCRIPT> 
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 28/10/03 11:58 $|$$Author: Nvaplat11 $"

//% ChangeBranch: Habilita el campo "Producto" y pasa el valor del campo "Ramo" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeBranch(Field){
//--------------------------------------------------------------------------------------------	
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param1.sValue=Field.value;
		}
		if(typeof(valPay_Concep)!='undefined') 
			valPay_Concep.Parameters.Param3.sValue=Field.value;
			
		if(typeof(valModulec)!='undefined') 
			valModulec.Parameters.Param1.sValue=Field.value;	
//+ Se deshabilitan los campos 			
		tcdEffecdate.value = "";
		tcdEffecdate.disabled = true;
		btn_tcdEffecdate.disabled = true;
		valModulec.value = "";
		valModulec.disabled = true;
		btnvalModulec.disabled = true;
		UpdateDiv('valModulecDesc', '');		
		valCover.value = "";
		valCover.disabled = true;
		btnvalCover.disabled = true;
		UpdateDiv('valCoverDesc', '');
		UpdateDiv('valCurrencyDesc', '');
		valPay_Concep.value = "";
		valPay_Concep.disabled = true;
		btnvalPay_Concep.disabled=true;
		UpdateDiv('valPay_ConcepDesc', '');
	}
}

//% ChangeProduct: Habilita los campos y pasa el valor del campo "Producto" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeProduct(Field){
//--------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param2.sValue=Field.value;				
			if(typeof(valPay_Concep)!='undefined') 
				valPay_Concep.Parameters.Param4.sValue=Field.value;
			if(typeof(valModulec)!='undefined') 
				valModulec.Parameters.Param2.sValue=Field.value;	
				
			tcdEffecdate.disabled=false;
			btn_tcdEffecdate.disabled=false;
		}
//+ Se deshabilitan los campos 
		tcdEffecdate.value = "";
		valModulec.value = "";
		valModulec.disabled = true;
		btnvalModulec.disabled = true;
		UpdateDiv('valModulecDesc', '');		
		valCover.value = "";
		valCover.disabled = true;
		btnvalCover.disabled = true;
		UpdateDiv('valCoverDesc', '');
		UpdateDiv('valCurrencyDesc', '');
		valPay_Concep.value = "";
		valPay_Concep.disabled = true;
		btnvalPay_Concep.disabled=true;
		UpdateDiv('valPay_ConcepDesc', '');
		if ((valProduct.value != "") && (tcdEffecdate.value != "") && (valModulec.value != "")){
			valCover.disabled=false;
			btnvalCover.disabled=false;
		}
	}
}

//% ChangeEffecdate: Habilita los campos y pasa el valor del campo "Fecha de efecto" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeEffecdate(Field){
//--------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param4.sValue=Field.value;				
		    if(typeof(valPay_Concep)!='undefined')
				valPay_Concep.Parameters.Param5.sValue=Field.value;
			if(typeof(valModulec)!='undefined') 
				valModulec.Parameters.Param3.sValue=Field.value;					
			if ((valProduct.value != "") && (tcdEffecdate.value != "")){
				valModulec.value = "";
				valCover.value = "";
				valCover.disabled = true;
				btnvalCover.disabled = true;
				UpdateDiv('valCoverDesc', '');
				UpdateDiv('valCurrencyDesc', '');
				valPay_Concep.value = "";
				valPay_Concep.disabled = true;
				btnvalPay_Concep.disabled=true;
				UpdateDiv('valPay_ConcepDesc', '');
				UpdateDiv('valModulecDesc', '');
				insDefValues('MAM001', 'nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value, '/VTimeNet/Maintenance/MantHealt');	
			}	
		} 
	}
}

//% ChangeModulec: Habilita los campos y pasa el valor del campo "Módulo" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeModulec(Field){
//--------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param3.sValue=Field.value;				
		    if(typeof(valPay_Concep)!='undefined')
				valPay_Concep.Parameters.Param1.sValue=Field.value;
			if ((valProduct.value != "") && (tcdEffecdate.value != "") && (valModulec.value != "")){
				valCover.disabled=false;
				btnvalCover.disabled=false;
			}else{
				valCover.disabled=true;
				btnvalCover.disabled=true;
			}
			
			valCover.value = "";				
			UpdateDiv('valCoverDesc', '');
			UpdateDiv('valCurrencyDesc', '');
			valPay_Concep.value = "";
			valPay_Concep.disabled = true;
			btnvalPay_Concep.disabled=true;
			UpdateDiv('valPay_ConcepDesc', '');	
		} 
	}
}

//% ChangeCover: Al cambiar el código de la cobertura.
//--------------------------------------------------------------------------------------------
function ChangeCover(Field){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
		if(typeof(valPay_Concep)!='undefined') 
			valPay_Concep.Parameters.Param2.sValue=Field.value;
			
		if (valCover.value !=""){	
			valPay_Concep.disabled=false;
			btnvalPay_Concep.disabled=false;
		}else{
			valPay_Concep.disabled=true;
			btnvalPay_Concep.disabled=true;
		}
			
		
		
		insDefValues("ShowDataMAM001", "sField=" + "getCurrency" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&dEffecdate=" + tcdEffecdate.value + "&nCover=" + valCover.value, '/VTimeNet/Maintenance/MantHealt');
		UpdateDiv('valCurrencyDesc', valCover_sDescCurrency.value);
	}
}

//% insStateZone: Habilitación/Deshabilitación de campos de la forma según la acción a procesar.
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	self.document.forms[0].cbeBranch.disabled=false;
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}

//% insFinish: controla la acción de Finalizar de la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("MAM001", "MAM001_k.aspx", 1, ""))
End With
mobjMenu = Nothing
Call mobjOptionSystem.find()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmillslimt" ACTION="ValMantHealt.aspx?mode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="10%"><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<%If mobjOptionSystem.sPolicyNum = "1" Then%>
				<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(eRemoteDB.Constants.intNull), "valProduct",  ,  ,  , "ChangeBranch(this);", True)%> </TD>
			<%Else%>
				<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(eRemoteDB.Constants.intNull), "valProduct",  ,  ,  , "ChangeBranch(this);", True)%> </TD>
			<%End If%>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=13382><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True,  ,  ,  ,  , "ChangeProduct(this);")%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=11745><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate",  , True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "ChangeEffecdate(this);", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=13382><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "ChangeModulec(this)", True, 4, GetLocalResourceObject("valModulecToolTip"),  , 4))
End With
%>
			</TD>           
        </TR>
        <TR>            
			<TD><LABEL ID=104862><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
			<%
With mobjValues.Parameters
	.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nModulec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.ReturnValue("sDescCurrency", False, vbNullString, True)
End With
%>
			<TD><%Response.Write(mobjValues.PossiblesValues("valCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "ChangeCover(this);", True,  , GetLocalResourceObject("valCoverToolTip")))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=104863><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>			
			<%=mobjValues.DIVControl("valCurrencyDesc", True)%>
        </TR>
        <TR>
		     <TD><LABEL ID=104864><%= GetLocalResourceObject("valPay_ConcepCaption") %></LABEL></TD>
		     <TD>
<%
mobjValues.Parameters.Add("nModulec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCover", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("dEffecdate", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valPay_Concep", "tabCl_cov_bil", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valPay_ConcepToolTip")))
%>
			</TD>
		</TR>
    </TABLE>
</BODY>
</FORM>
</HTML>




