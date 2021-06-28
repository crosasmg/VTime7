<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.13
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si737_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si737_k"
%>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->


<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/tMenu.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Claim.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Claim.js"></SCRIPT>
<SCRIPT>

    //% insStateZone: habilita los campos de la forma
    //-----------------------------------------------------------------------------
    function insStateZone() {
        //-----------------------------------------------------------------------------
    }

    //% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
    //-----------------------------------------------------------------------------
    function insCancel() {
        //-----------------------------------------------------------------------------
        return true
    }

    //% ChangeValue: 
    //-------------------------------------------------------------------------------------------
    function insChangeValue(Field) {
        //-------------------------------------------------------------------------------------------
        var lstrQstring = '';

        switch (Field.name) {
            case "tcdEffecdate":
                with (self.document.forms[0]) {
                    valCover.value = '';
                    UpdateDiv('valCoverDesc', ' ', 'Normal');
                    if (Field.value != '') {
                        tcdLedgerdat.value = Field.value;
                        valCover.Parameters.Param1.sValue = Field.value;
                    }
                    else {
                        valCover.disabled = true;
                        btnvalCover.disabled = true;
                    }
                }
                break;

            case "cbeBranch":
                with (self.document.forms[0]) {
                    valCover.value = '';
                    UpdateDiv('valCoverDesc', ' ', 'Normal');
                    tcnPolicyHeader.value = '';
                    if (Field.value != '' && Field.value > 0) {
                        valCover.Parameters.Param3.sValue = Field.value;
                        valProduct.Parameters.Param1.sValue = Field.value;
                        btnvalProduct.disabled = false;
                        valProduct.disabled = false;
                        valProduct.value = '';
                        valCover.value = '';
                        tctClientCollect.value = '';
                        tctClientCollect_Digit.value = '';
                        UpdateDiv('valProductDesc', ' ', 'Normal');
                        UpdateDiv('lblCliename', ' ', 'Normal');
                    }
                    else {
                        valProduct.disabled = true;
                        btnvalProduct.disabled = true;
                        valCover.disabled = true;
                        btnvalCover.disabled = true;
                        valProduct.value = '';
                        tctClientCollect.value = '';
                        tctClientCollect_Digit.value = '';
                        cbeOffice.value = '';
                        cbeOfficeAgen.value = '';
                        cbeAgency.value = '';
                        cbeCurrency.value = '';
                        UpdateDiv('cbeOfficeAgenDesc', ' ', 'Normal');
                        UpdateDiv('cbeAgencyDesc', ' ', 'Normal');
                        UpdateDiv('valProductDesc', ' ', 'Normal');
                        UpdateDiv('lblCliename', ' ', 'Normal');
                    }
                }
                break;

            case "valProduct":
                with (self.document.forms[0]) {
                    valCover.value = '';
                    UpdateDiv('valCoverDesc', ' ', 'Normal');

                    if (Field.value != '' && Field.value > 0) {
                        valCover.Parameters.Param4.sValue = Field.value;
                        valCover.disabled = false;
                        btnvalCover.disabled = false;
                        lstrQString = 'dEffecdate=' + tcdEffecdate.value +
	                              '&nBranch=' + cbeBranch.value +
	                              '&nProduct=' + Field.value
                        insDefValues('Brancht', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                    else {
                        valCover.disabled = true;
                        btnvalCover.disabled = true;
                        hddBrancht.value = '0';
                    }
                }
                break;

            case "tcnPolicyHeader":
                with (self.document.forms[0]) {
                    valCover.value = '';
                    UpdateDiv('valCoverDesc', ' ', 'Normal');
                    if (Field.value != '' && Field.value > 0) {

                        valCover.Parameters.Param5.sValue = Field.value;
                        cbeCurrency.disabled = true;
                        lstrQString = 'dEffecdate=' + tcdEffecdate.value +
	                              '&nPolicy=' + Field.value +
	                              '&nCertif=0'
                        insDefValues('CurrenPol', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                    else {
                        valCover.Parameters.Param5.sValue = 0;
                        cbeCurrency.disabled = false;
                        UpdateDiv('hddPoliType', '0', 'Normal');
                    }
                }
                break;

            case "valCover":
                with (self.document.forms[0]) {
                    //Si el campo cover tiene contenido, se muestra el cliente asociado a cobertura,
                    //se inhabilita el campo cliente.	
                    if (Field.value != '' && Field.value > 0) {
                        btntctClientCollect.disabled = true;
                        tctClientCollect.disabled = true;
                        tctClientCollect_Digit.disabled = true;
                        lstrQString = 'dEffecdate=' + tcdEffecdate.value +
    	                          '&nBranch=' + cbeBranch.value +
    	                          '&nProduct=' + valProduct.value +
    	                          '&nCover=' + Field.value
                        insDefValues('ClientCover', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                }
                break;

            case "chkRelation":
                with (self.document.forms[0]) {
                    if (chkRelation.checked) {
                        lstrQString = "&Form='Header'"
                        insDefValues('Relation', lstrQString, '/VTimeNet/Claim/Claim');
                    }
                    else {
                        tcnRelat.value = '';
                    }
                }
                break;
        }
    }
</SCRIPT>

<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("SI737", "SI737_k.aspx", 1, vbNullString))
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.13
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
%>    

<SCRIPT>
    //+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion = "$$Revision: 2 $|$$Date: 15/10/03 12.31 $"
</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SI737" ACTION="valClaim.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0>Fecha de denuncio</LABEL></TD>
            <TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , "Fecha de declaracion o aviso del siniestro",  ,  ,  , "insChangeValue(this);")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Sucursal</LABEL></TD>                                                            
            <TD><%With mobjValues
	'.Parameters.Add("nUsercode", Session("nUsercode"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1,0)",  , 2, "Sucursal donde se registra el siniestro.", eFunctions.Values.eTypeCode.eNumeric))
	.BlankPosition = True
End With%>
            </TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Oficina</LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, Request.Form("cbeOfficeAgen"), True,  ,  ,  ,  , "BlankAgencyDepend();insInitialAgency(2,0)", False,  , "Oficina donde se registra el siniestro"))
End With
%>
            </TD>
            <TD>&nbsp;</TD>            
            <TD><LABEL ID=0>Agencia</LABEL></TD>
            <TD><%With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", eFunctions.Values.eValuesType.clngWindowType, Request.Form("cbeAgency"), True,  ,  ,  ,  , "insInitialAgency(3,0)", False,  , "Agencia donde se registra el siniestro"))
End With
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Ramo</LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", "Ramo al que pertenecen la(s) póliza(s) siniestrada(s)",  , CStr(True),  ,  ,  , "insChangeValue(this);")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Producto</LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", "Producto al que pertenecen la(s) póliza(s) siniestrada(s)",  , eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  , "insChangeValue(this);")%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Póliza</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicyHeader", 10,  , True, "Número de póliza siniestrada",  ,  ,  ,  ,  , "insChangeValue(this);")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Cobertura</LABEL></TD>
            <TD><%With mobjValues
	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("sCertype", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCertif", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(.PossiblesValues("valCover", "TabCover_Pol", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "insChangeValue(this);", False,  , "Código de la cobertura afectada en el siniestro", eFunctions.Values.eTypeCode.eNumeric))
End With
%>
            </TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Moneda</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , 4, "Moneda en la que están expresados los importes de el o los siniestros", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.CheckControl("chkRelation", "Generar relación", CStr(False), CStr(1), "insChangeValue(this);")%></TD>
            <TD><%=mobjValues.NumericControl("tcnRelat", 5,  , True, "Número de la relación que permite la agrupación de varios siniestros",  ,  ,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>

            <TD><LABEL ID=0>Contabilización</LABEL></TD>
            <TD><%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.DateControl("tcdLedgerdat", CStr(Today),  , "Fecha de declaracion o aviso del siniestro")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0>Denunciante RUT </LABEL></TD>
            <TD><%=mobjValues.ClientControl("tctClientCollect", "",  , "Código del cliente denunciante (Contratante o Prestador de Servicio)",  , True, "lblCliename", False,  ,  ,  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.HiddenControl("hddPoliType", "0")%></TD>
            <TD><%=mobjValues.HiddenControl("hddBrancht", "0")%></TD>
        </TR>
    </TABLE>
<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.13
Call mobjNetFrameWork.FinishPage("si737_k")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




