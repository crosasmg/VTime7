<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mintOrigin As Object
Dim mclsSaapv As eSaapv.Saapv
Dim mintIndefinite As Byte


'% insPreVI7501_D: Realiza la lectura de los campos a mostrar en pantalla
'---------------------------------------------------------------------
Private Sub insPreVI7501_D()
	'---------------------------------------------------------------------
	Call mclsSaapv.Find(mobjValues.TypeToString(Session("nCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.TypeToString(Session("nInstitution"), eFunctions.Values.eTypeData.etdLong))
	
	If mclsSaapv.nOrigin = 0 Or mclsSaapv.nOrigin = eRemoteDB.Constants.intNull Then
		If CStr(Session("nType_saapv")) = "6" Then
			mintOrigin = "3"
		Else
			mintOrigin = "2"
		End If
	Else
		mintOrigin = mclsSaapv.nOrigin
	End If
	
	If mclsSaapv.dEndDate = eRemoteDB.Constants.dtmNull Then
		mintIndefinite = 1
	Else
		mintIndefinite = 0
	End If
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7501_D")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsSaapv = New eSaapv.Saapv
mobjValues.ActionQuery = Session("bQuery")
Call insPreVI7501_D()
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 21/11/11 16:49 $|$$Author: ljimenez $"

//insChangeCheck: Si se chequea plazo indefinido, se inhabilita el mes de término y se desmarca el Aporte único
//-------------------------------------------------------------------------------------------------------------
function insChangeCheck(lobject){
//-------------------------------------------------------------------------------------------------------------
	if (self.document.forms[0].chkIndefinite.checked){
        self.document.forms[0].tcdEndDate.disabled = true;
        self.document.forms[0].tcdEndDate.value = "";
        self.document.forms[0].chkLumpsum.checked = false;
    } else {
	    self.document.forms[0].tcdEndDate.disabled = false;
	    self.document.forms[0].btn_tcdEndDate.disabled = false;	    
	}
}

//insChangeCheck_2: Si se chequea Aporte único, se desmarca el plazo indefinido y se habilita el mes de término
//------------------------------------------------------------------------------------------------------------
function insChangeCheck_2(){
//------------------------------------------------------------------------------------------------------------
    if (self.document.forms[0].chkLumpsum.checked){
        self.document.forms[0].chkIndefinite.checked = false;
        self.document.forms[0].tcdEndDate.disabled = false;
        self.document.forms[0].btn_tcdEndDate.disabled = false;	    
    }
}

//insChange: Si la cuenta origen es "Depósitos convenidos" el régimen tributario es sin beneficio
//------------------------------------------------------------------------------------------------------------
function insChange(nOrigin){
//------------------------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
	    if (nOrigin == '3'){
            cbeTax_regime.disabled = true;
            cbeTax_regime.value = '99';
        //} else {
        //    cbeTax_regime.disabled = false;
	    //    cbeTax_regime.value = '';
	    }
	}
}
</SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmVI7501_D" ACTION="valVI7501tra.aspx?nMainAction=301&nHolder=1">
	<%=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"))%>
    <BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=0>Cuenta Origen</LABEL></TD>
            <TD><%

mobjValues.Parameters.Add("nBranch", Session("nBranch_saapv"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", Session("nProduct_saapv"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nCollecDocTyp", mclsSaapv.nWay_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

Response.Write(mobjValues.PossiblesValues("cbeOrigin", "TAB_ORIGIN", eFunctions.Values.eValuesType.clngWindowType, mintOrigin, True,  ,  ,  ,  ,  , False,  , "Cuenta origen del depósito.",  , 1))
%>
            </TD>            
            <TD><LABEL ID=0>Monto en pesos</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount", 18, CStr(mclsSaapv.nAmount),  , "Monto a depositar expresado en pesos.", True, 0,  ,  ,  ,  , False, 2)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Monto en UF</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount_UF", 18, CStr(mclsSaapv.nAmount_uf),  , "Monto a depositar expresado en UF.", True, 6,  ,  ,  ,  , False, 3)%></TD>
            <TD><LABEL ID=0>Monto en %</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount_PCT", 9, CStr(mclsSaapv.nAmount_pct),  , "Porcentaje de renta.", True, 6,  ,  ,  ,  , False, 4)%></TD>
        </TR>
        <TR>
            <TD><%=mobjValues.CheckControl("chkLumpsum", "Aporte Unico", CStr(mclsSaapv.nInd_Lumpsum), "1", "insChangeCheck_2();", False, 5, "Indica que se hará un pago único de fondos.")%></TD>
            <TD><%=mobjValues.CheckControl("chkIndefinite", "Plazo Indefinido", CStr(mintIndefinite), "1", "insChangeCheck();", False, 6, "Si se chequea plazo indefinido, se inhabilita el mes de término.")%></TD>
            <TD><LABEL ID=0>Mes Término</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEndDate", CStr(mclsSaapv.dEndDate),  , "Indica la fecha de término del ahorro.",  ,  ,  ,  , mclsSaapv.dEndDate = eRemoteDB.Constants.dtmNull, 7)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=0>Régimen Tributario</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTax_regime", "Table950", eFunctions.Values.eValuesType.clngComboType, CStr(mclsSaapv.nTax_regime),  ,  ,  ,  ,  ,  , False,  , "Régimen tributario de la cuenta principal de la póliza.",  , 8)%></TD>
        </TR>
    </TABLE>

<%
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenu = Nothing
'UPGRADE_NOTE: Object mclsSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsSaapv = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>insChange('" & mintOrigin & "');</SCRIPT>")
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI7501_D")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




