<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "DP017_K"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


    <%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("DP017", "DP017_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With

mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 8/09/03 12:13 $|$$Author: Nvaplat18 $"

//% insCancel: Se cancela la página invocada.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Permite habilitar los objetos e imágenes de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
//+ Habilita el ramo    
		cbeBranch.disabled = false;
    
//+ Habilita el control de fecha    
		tcdDate.disabled = false;
		btn_tcdDate.disabled = false;
    }
}

//% LoadCover: Se cancela la página invocada.
//------------------------------------------------------------------------------------------
function LoadCover(){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		if (valProduct.value.replace(/ */,'') == ''){
//+ Establece el estado del campo cobertura si el producto es válido
		   valProduct.value='';
		   valCover.disabled = true;
		   btnvalCover.disabled = true;
		   valCover.value = '';
		   UpdateDiv('valCoverDesc', '');
		}
		else{
		    valCover.disabled = false;
		    btnvalCover.disabled = false;
		}
		valCover.Parameters.Param2.sValue=document.forms[0].cbeBranch.value;
		valCover.Parameters.Param3.sValue=document.forms[0].valProduct.value;
    }
}

//% insSetProduct: Establece el valor y estado del producto cuando se cambia el ramo
//------------------------------------------------------------------------------------------
function insSetProduct(lobjBranch){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
//+ Actualiza el valor del producto según el ramo seleccionado    
		if(typeof(document.forms[0].valProduct)!="undefined")
		    valProduct.Parameters.Param1.sValue=lobjBranch.value;
        
		valProduct.value = '';
		UpdateDiv('valProductDesc', '');
		valCover.value = '';
		UpdateDiv('valCoverDesc', '');

//+ Establece el estado del campo producto si el ramo es válido
		if(lobjBranch.value!=0){
		    valProduct.disabled = false;
		    btnvalProduct.disabled = false
		}
		else{
		    valProduct.disabled = true;
		    btnvalProduct.disabled = true;
			valCover.disabled = true;
			btnvalCover.disabled = true;
			
//+ En caso de modificar el ramo directamente se verifica el estado de la cobertura.        
		    if (valCover.disabled==false){
		        btnvalCover.disabled = true;
		        valCover.value = '';
		        valCover.disabled = true;
		    }
		}
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="DP017_k" ACTION="valProduct.aspx?mode=1">
	<BR> <BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=14212><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", 1, CStr(0),  ,  ,  ,  ,  , "insSetProduct(this)", True,  , GetLocalResourceObject("cbeBranchToolTip"),  , 1)%></TD>
            <TD><LABEL ID=14215><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<%
With mobjValues.Parameters
	.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>          
			<TD><%=mobjValues.PossiblesValues("valProduct", "tabProdMaster1", 2,  , True,  ,  ,  , 20, "LoadCover()", True, 5, GetLocalResourceObject("valProductToolTip"),  , 2)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=14213><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
            <%
With mobjValues.Parameters
	.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCovernoShow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCoverMax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
%>
            <TD><%=mobjValues.PossiblesValues("valCover", "tabLife_cover", 2,  , True,  ,  ,  , 20,  , True, 4, GetLocalResourceObject("valCoverToolTip"),  , 3)%></TD>
            <TD><LABEL ID=14214><%= GetLocalResourceObject("tcdDateCaption") %></LABEL></TD>
            <TD>
<%=mobjValues.DateControl("tcdDate", CStr(Today),  , GetLocalResourceObject("tcdDateToolTip"),  ,  ,  ,  , True, 4)%></TD>
        </TR>        
    </TABLE>
</FORM>
</BODY>
</HTML>
<% mobjValues = Nothing%>





