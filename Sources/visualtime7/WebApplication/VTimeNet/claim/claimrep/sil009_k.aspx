<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMenu As eFunctions.Menues
Dim mobjValues As eFunctions.Values


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("sil009_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "sil009_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.15
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SIL009", "SIL009_K.aspx", 1, Request.QueryString("sWindowDescript"), CStr(Session("sDesMultiCompany")), CStr(Session("sSche_code"))))
End With
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $|$$Author: Nvaplat60 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//--------------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------------		    
	return true;
}

//%insFinish: Controla la acción Finalizar de la página
//--------------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------------
    return true;
}

//% insStateZone: se manejan los campos de la página
//--------------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------------
}

//%insStateChek: Habilita/Deshabilita los controles de detalles del reporte
//--------------------------------------------------------------------------------------------------
function insStateChek(lblnEnable, lblnClear){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
		chkBranchDet.checked   = (lblnClear)? "1":""
		chkOfficeDet.checked   = (lblnClear)? "1":""
		chkProductDet.checked  = (lblnClear)? "1":""
		chkTypeMov.checked     = (lblnClear)? "1":""
		chkCause.checked       = (lblnClear)? "1":""
		chkBDraft.checked      = (lblnClear)? "1":""
    
		chkBranchDet.disabled   = (lblnEnable)? false:true
		chkOfficeDet.disabled   = (lblnEnable)? false:true
		chkProductDet.disabled  = (lblnEnable)? false:true
		chkTypeMov.disabled     = (lblnEnable)? false:true
		chkCause.disabled       = (lblnEnable)? false:true
		chkBDraft.disabled      = (lblnEnable)? false:true
	}
}
//%LockControl: Habilita/Deshabilita los controles dependientes de la página
//--------------------------------------------------------------------------------------------------
function LockControl(Control){
//--------------------------------------------------------------------------------------------------	
	UpdateDiv("valCauseDesc","")
	with(self.document.forms[0]){		
		valCause.value=""		
		if(cbeBranch.value==0){		
			valCause.disabled=true		
			self.document.btnvalCause.disabled=true
		}
		else{		
		    elements["valCause"].Parameters.Param1.sValue=cbeBranch.value;
		    elements["valCause"].Parameters.Param2.sValue=valProduct.value;				
			valCause.disabled=false
			document.btnvalCause.disabled=false
		}
	}
	
   insShowValuesSIL009();
}
//%insShowValuesSIL009: Habilita/Deshabilita el "Giro del Negocio" dependiendo de las transacciones
//                      asociadas al Ramo-Producto
//--------------------------------------------------------------------------------------------------
function insShowValuesSIL009(){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){        
        insDefValues('ChgProduct','nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value,'/VTimeNet/Claim/ClaimRep');        
		elements["valCause"].Parameters.Param1.sValue=cbeBranch.value;
		elements["valCause"].Parameters.Param2.sValue=valProduct.value;		
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmSIL009" ACTION="valClaimRep.aspx?sMode=1">
	<BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("SIL009", Request.QueryString("sWindowDescript")))%>
    <TABLE WIDTH="100%">        
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101557>Fechas para el listado</LABEL></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=101559>Tipo de listado</LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD WIDTH="25%"><LABEL ID=101574>Inicial</LABEL></TD>
            <TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdInidate", CStr(Today),  , "Fecha desde la cual se desea listar los siniestros")%></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101589, "optTypeRep", "Detalle", CStr(1), CStr(1), "insStateChek(true, false);")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101575>Final</LABEL></TD>
            <TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdEnddate", CStr(Today),  , "Fecha hasta la cual se desea listar los siniestros")%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.OptionControl(101590, "optTypeRep", "Resumen",  , CStr(2), "insStateChek(false, false);")%></TD>
        </TR>
        <TR>
			<TD COLSPAN="3">&nbsp;</TD>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(101591, "optTypeRep", "Ambos",  , CStr(3), "insStateChek(false, true);")%></TD>
        </TR>
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=101577>Sucursal</LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.PossiblesValues("cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Nombre de la Zona a listar")%></TD>
            <TD><%=mobjValues.CheckControl("chkOfficeDet", "Detalle",  , "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101579>Ramo</LABEL></TD>            
            <TD COLSPAN="3"><%=mobjValues.BranchControl("cbeBranch", "Descripción del ramo a listar", "",  ,  ,  ,  , "LockControl(""Branch"");")%></TD>
            <TD><%=mobjValues.CheckControl("chkBranchDet", "Detalle",  , "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101581>Producto</LABEL></TD>
            <TD COLSPAN="3"><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen los siniestros", "",  ,  , "",  ,  ,  , "insShowValuesSIL009();")%></TD>
            <TD><%=mobjValues.CheckControl("chkProductDet", "Detalle",  , "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101583>Tipo de movimiento</LABEL></TD>
            <TD	COLSPAN="3"><%=mobjValues.PossiblesValues("cbeMov_type", "table140", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  ,  ,  , "Descripción del tipo de movimiento")%></TD>
            <TD><%=mobjValues.CheckControl("chkTypeMov", "Detalle",  , "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101585>Causa</LABEL></TD>
            <TD	COLSPAN="3">
            <%With mobjValues
	.Parameters.Add("nBranch", mobjValues.StringToType(Request.Form("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", mobjValues.StringToType(Request.Form("valProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valCause", "tabclaim_caus", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , "Descripción de la causa a listar"))
End With
%></TD>
            <TD><%=mobjValues.CheckControl("chkCause", "Detalle",  , "1")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=101587>Giro del negocio</LABEL></TD>
            <TD	COLSPAN="3"><%=mobjValues.PossiblesValues("cbeB_draft", "Table1", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , "Giro del negocio a listar")%></TD>
            <TD><%=mobjValues.CheckControl("chkBDraft", "Detalle",  , "1",  , True)%></TD>
        </TR>
        <TR>
			<TD COLSPAN="5">&nbsp;</TD>
		</TR>
        <TR>
            <TD><LABEL ID=101588>Orden de la información</LABEL></TD>
            <TD	COLSPAN="3"><%=mobjValues.PossiblesValues("cbeOrder", "Table521", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , "Orden de la información a listar")%></TD>
            <TD><%=mobjValues.HiddenControl("tcnIndic", CStr(0))%></TD>
        </TR>
    </TABLE>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.15
Call mobjNetFrameWork.FinishPage("sil009_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




