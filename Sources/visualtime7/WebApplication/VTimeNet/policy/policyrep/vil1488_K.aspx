<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.05
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility


    '**- The object to handling the general function to load values is defined
    '- Objeto para el manejo de las funciones generales de carga de valores

    Dim mobjValues As eFunctions.Values
    Dim sDirectory As String

    '**- The object to handling the generic routines is defined
    '- Objeto para el manejo de las rutinas genéricas

    Dim mobjMenu As eFunctions.Menues


</script>
<%
    Response.Expires = -1
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("vil1488_k")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
    sDirectory = mobjValues.insGetSetting("ExportDirectoryReport", "/Reports/", "Paths")
    mobjValues.sCodisplPage = "vil1488_k"

    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.05
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

%>
<meta name="GENERATOR" content="eTransaction Designer for Visual TIME">
<script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script language="JavaScript" src="/VTimeNet/Scripts/valFunctions.js"></script>
<script language="JavaScript" src="/VTimeNet/Scripts/tMenu.js"></script>

<script>

//**+ Source Safe control of version
//+ Para Control de Versiones de Source Safe

    //document.VssVersion="$$Revision: 1 $|$$Date: 8/10/03 19:15 $"

//**% insStateZone: This function enable/disable the fields of the page according to the action 
//**% to be performed
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
}

//**% insCancel: This function executes the action to cancel of the page.
//% insCancel: Esta función ejecuta la acción Cancelar de la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//% insChangeBranch: Esta funcion cambia el valor del parametro nBranch
//------------------------------------------------------------------------------------------
function insChangeBranch(Value){
//------------------------------------------------------------------------------------------
    
    self.document.forms[0].tcnNumCart.Parameters.Param1.sValue = Value;
}

//% insChangeProduct: Esta funcion cambia el valor del parametro nProduct
//------------------------------------------------------------------------------------------
function insChangeProduct(Value){
//------------------------------------------------------------------------------------------

    self.document.forms[0].tcnNumCart.Parameters.Param2.sValue = Value;

}

//% insChangePolicy: Esta funcion cambia el valor del parametro nPolicy
//------------------------------------------------------------------------------------------
function insChangePolicy(Value){
//------------------------------------------------------------------------------------------

    self.document.forms[0].tcnNumCart.Parameters.Param3.sValue = Value;

    if (Value != '0' && Value != ""){
        insDefValues("PolicyNum", "nBranch=" + self.document.forms[0].cbeBranch.value +
						          "&nProduct=" + self.document.forms[0].valProduct.value +
						          "&nPolicy=" + self.document.forms[0].tcnPolicy.value)
    }

}

//% insChangeDate: Esta funcion cambia las fechas de acuerdo al período
//------------------------------------------------------------------------------------------
function insChangeDate(Value){
//------------------------------------------------------------------------------------------
    nYear = self.document.forms[0].tcnYear.value
    
	with (self.document.forms[0]){	   
		switch (Value){
		    case "1":
		        tcdInitDate.value='01/01/' + nYear;
		        tcdEndDate.value='31/03/' + nYear;
		        break;
		    case "2":
		        tcdInitDate.value='01/04/' + nYear;
		        tcdEndDate.value='30/06/' + nYear;
		        break;
		    case "3":
		        tcdInitDate.value='01/07/' + nYear;
		        tcdEndDate.value='30/09/' + nYear;
		        break;
		    case "4":
		        tcdInitDate.value='01/10/' + nYear;
		        tcdEndDate.value='31/12/' + nYear;
		        break;
		}
	}
}

//% insChangeYear: Esta funcion cambia se ejecuta cuando cambia lel año
//------------------------------------------------------------------------------------------
function insChangeYear(){
//------------------------------------------------------------------------------------------
    
	with (self.document.forms[0]){	   
	    if (tcnYear.value!=''){
		    optMonth[0].checked=true;
		    optMonth[0].disabled=false;
		    optMonth[1].disabled=false;
		    optMonth[2].disabled=false;
		    optMonth[3].disabled=false;
	        insChangeDate('1');
	    }
	    else{
	        tcdInitDate.value=''
		    tcdEndDate.value='';
		    optMonth[0].checked=true;
		    optMonth[0].disabled=true;
		    optMonth[1].disabled=true;
		    optMonth[2].disabled=true;
		    optMonth[3].disabled=true;
	    }
	}
}

//**% FindShowCertifShowCertif: This function enabled or disabled the field nCertif. 
//% FindShowCertifShowCertif: Esta función habilita o inhabilita el campo nCertif.
//-----------------------------------------------------------------------------
function FindShowCertif(){
//-----------------------------------------------------------------------------
	ShowPopUp("/VTimeNet/Policy/PolicyRep/ShowDefValues.aspx?Field=Switch_Curr_Pol" + "&nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value + "&nPolicy=" + self.document.forms[0].tcnPolicy.value,"ShowDefValuesCollectionTra", 1, 1,"no","no",2000,2000)
}

//% EnableFields: Habilita / Deshabilita los campos PÓLIZA y CERTIFICADO 
//-------------------------------------------------------------------------------------------------
function EnableFields(bChecked)
//-------------------------------------------------------------------------------------------------
{
	if(bChecked)
	{
//+ Si está encendido el checkbutton, se procesa de forma masiva, de lo contrario se
//+ debe especificar el número de la póliza y el certificado 
		self.document.forms[0].elements['tcnPolicy'].disabled = true;
		self.document.forms[0].elements['tcnCertif'].disabled = true;
		self.document.forms[0].elements['hddMassive'].value   = 1;
	}
	else
	{
		self.document.forms[0].elements['tcnPolicy'].disabled = false;
		self.document.forms[0].elements['tcnCertif'].disabled = false;
		self.document.forms[0].elements['hddMassive'].value   = 2;
	}
}

//% insInitials:se ejecuta al entrar en la transacción
//-----------------------------------------------------------------------------
function insInitials(){
//-----------------------------------------------------------------------------
    lDate = '<%Response.write("Today")%>';
}
//% ShowDateIni:Cambia automáticamente al primer día del mes/año en consulta.

function ShowDateIni()
//------------------------------------------------------------------------------------------
{
	insDefValues("ShowDateIni", "tcdDate_ini=" + self.document.forms[0].elements['tcdDate_ini'].value, '/VTimeNet/Policy/PolicyRep');
}
//% ShowDateIni:Cambia automáticamente al último día del mes/año en consulta.
function ShowDateEnd()
//------------------------------------------------------------------------------------------
{
	insDefValues("ShowDateEnd", "tcdDate_end=" + self.document.forms[0].elements['tcdDate_end'].value, '/VTimeNet/Policy/PolicyRep');
}

</script>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <%With Response
            .Write(mobjValues.StyleSheet() & vbCrLf)
            .Write(mobjMenu.MakeMenu("VIL1488", "vil1488_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
        End With
        mobjMenu = Nothing%>
</head>
<body onunload="closeWindows();">
    <td>
        <br>
    </td>
    <td>
        <br>
    </td>
    <form method="POST" id="FORM" name="vil1488" action="valPolicyRep.aspx?x=1">
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))%>
    <br>
    <br>
    <table width="100%" align="CENTER">
        <tr>
            <td>
                <label id="13658">
                    Ramo</label>
            </td>
            <td>
                <%=mobjValues.BranchControl("cbeBranch", "Código del ramo comercial a listar", vbNullString, "valProduct",  ,  ,  , "insChangeBranch(this.value);if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value",  , 3)%>
            </td>
            <%
                With mobjValues.Parameters
                    .Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <td>
                <label id="13664">
                    Producto</label>
            </td>
            <td>
                <%=mobjValues.ProductControl("valProduct", "Código del producto a listar", CStr(0), eFunctions.Values.eValuesType.clngWindowType, False, vbNullString,  ,  ,  , "insChangeProduct(this.value)", 4)%>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <label id="13663">
                    P&oacuteliza</label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnPolicy", 8, vbNullString, , "Número de póliza", , 0, , , , "insChangePolicy(this.value);FindShowCertif();")%>
            </td>
            <td>
                <label id="13660">
                    Certificado</label>
            </td>
            <td>
                <%=mobjValues.NumericControl("tcnCertif", 8, "0",  , "Número de certificado dentro de un colectivo",  , 0)%>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0">
                    N&uacutemero cartola</label>
            </td>
            <td>
                <%With mobjValues
                        .Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("nPolicy", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                        .Parameters.ReturnValue("ncartpol", True, "Cartola", True)
                        Response.Write(.PossiblesValues("tcnNumCart", "TabCartol", eFunctions.Values.eValuesType.clngWindowType, , True, , , , , , , , "Número de cartola"))
                                End With%>
            </td>
            <%If Request.QueryString.Item("sCodispl") = "VIL1488" Then%>
            <td>
                <label>
                    Generar pdf por cartola</label>
            </td>
            <td>
                <%=mobjValues.CheckControl("chkprocess", "", CStr(2), "1",  , False)%>
            </td>
            <%End If%>
            <td>
            </td>
        </tr>
        <tr>
            <%If Request.QueryString.Item("sCodispl") = "VIL1488" Then%>
            <td>
                <label id="0">
                    Ruta archivos</label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctPath", 60, sDirectory, True, "Path donde se registran los archivos PDF en el servidor",  ,  ,  ,  , True)%>
            </td>
            <%End If%>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HighLighted">
                <label id="0">
                    Per&iacuteodo Cartola</label>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="HorLine">
            </td>
        </tr>
        <tr>
            <td>
                <label id="13644">
                    Fecha desde</label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdDate_ini",  ,  , "Fecha de inicio para seleccionar los movimientos",  ,  ,  , "ShowDateIni()")%>
            </td>
            <%If Request.QueryString.Item("sCodispl") = "VIL1488C" Then%>
            <td>
                <label id="13644">
                    Fecha hasta</label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdDate_end",  ,  , "Fecha de fin para seleccionar los movimientos",  ,  ,  , "ShowDateEnd()")%>
            </td>
            <%Else%>
            <td>
                <label id="13644">
                    Fecha hasta</label>
            </td>
            <td>
                <%=mobjValues.DateControl("tcdDate_end",  ,  , "Fecha de fin para seleccionar los movimientos")%>
            </td>
            <%End If%>
            <td>
                <label id="13660">
                </label>
            </td>
        </tr>
        <%Response.Write(mobjValues.HiddenControl("hddMassive", CStr(1)))%>
        <%Response.Write(mobjValues.HiddenControl("hddsCodispl", Request.QueryString.Item("sCodispl")))%>
    </table>
    <%

        With Response
            .Write("<SCRIPT>")
            .Write("insInitials();")
            .Write("</SCRIPT>")
        End With
    %>
    </form>
</body>
<%mobjValues = Nothing%>
</html>
<%
    '^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.05
    Call mobjNetFrameWork.FinishPage("vil1488_k")
    
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer
%>
