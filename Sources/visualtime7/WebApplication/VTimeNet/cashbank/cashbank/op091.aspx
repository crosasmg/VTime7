<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú    
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manejo de la información de la ventana (cuentas corrientes)
Dim mobjCurr_acc As eCashBank.Curr_acc
Dim mobjMove_acc As eCashBank.Move_acc
Dim mobjClient As eClient.Client

'- Variables auxiliares para asignación de valores a campos de la forma
Dim moptDeb As Object
Dim moptCre As Object
Dim mdblAmount As Object
Dim mdblPayAmount As Object
Dim mintTypePay As Integer
Dim mdtmEffecdate As Object
Dim mstrClient As String


'%insPreOP091: Esta función se encaga de obtener los datos de la cuenta corriente
'--------------------------------------------------------------------------------------------
Private Sub insPreOP091()
	'--------------------------------------------------------------------------------------------
	mobjCurr_acc = New eCashBank.Curr_acc
	mobjMove_acc = New eCashBank.Move_acc
	
	'+ Inicialización de valores de las variables auxiliares
	
	mintTypePay = 0
	mdblAmount = 0
	mdblPayAmount = 0
	
	If mobjValues.StringToType(Request.QueryString.Item("nTypeTrans"), eFunctions.Values.eTypeData.etdDouble) <> 1 Then
		If mobjMove_acc.Find_document(4, mobjValues.StringToType(Request.QueryString.Item("nRemNum"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			If mobjCurr_acc.findClientCurr_acc(mobjMove_acc.nTyp_acco, mobjMove_acc.sType_acc, mobjMove_acc.sClient, mobjMove_acc.nCurrency) Then
				If mobjMove_acc.nType_pay = 1 Then
					mintTypePay = 3
				Else
					mintTypePay = mobjMove_acc.nType_pay
				End If
				mdblPayAmount = mobjMove_acc.nAmount
				If mobjCurr_acc.nBalance < 0 Then
					mdblAmount = System.Math.Abs(mobjCurr_acc.nBalance)
					moptDeb = 1
					moptCre = 2
				Else
					moptDeb = 2
					moptCre = 1
					mdblAmount = System.Math.Abs(mobjCurr_acc.nBalance * -1)
				End If
			End If
			mdtmEffecdate = mobjMove_acc.dOperdate
			mstrClient = mobjMove_acc.sClient
		End If
	Else
		mdtmEffecdate = Today
	End If
	
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeTypeTrans.value=" & Request.QueryString.Item("nTypeTrans") & ";</" & "Script>")
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].gmnRemNum.value=" & Request.QueryString.Item("nRemNum") & ";</" & "Script>")
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjClient = New eClient.Client

mobjValues.sCodisplPage = "op091"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 19/05/04 16:34 $|$$Author: Nvaplat7 $"

	var mstrLocation = "";

//% LockControl: Bloquea el combo de tipo de negocio
//-------------------------------------------------------------------------------------------
function LockControl(nTypeAccount){
//-------------------------------------------------------------------------------------------

	if (nTypeAccount == 2 ||
		nTypeAccount == 3 ||
		nTypeAccount == 8)
	{
		self.document.forms[0].cbeBussiType.value = "0";
		self.document.forms[0].cbeBussiType.disabled = false;
	}
	else
	{
		self.document.forms[0].cbeBussiType.value = "0";
		self.document.forms[0].cbeBussiType.disabled = true;
	}
//+ Se asigna a la variable modular de JScript "mstrLocation" el valor de la ruta de la página
//+ actual y se agregan al QueryString los parámetros nTypeAccount y nBussinessType para no perder
//+ estos valores cuando se recargue la página - ACM - 22/05/2001
	mstrLocation += document.location.href;
	mstrLocation = mstrLocation.replace(/&nTypeAccount.*/, "");
	mstrLocation = mstrLocation.replace(/&nBussinessType.*/, "");
	mstrLocation = mstrLocation.replace(/&dEffecdate.*/, "");
	mstrLocation = mstrLocation + "&nTypeAccount=" + nTypeAccount;
	mstrLocation = mstrLocation + "&nBussinessType=" + self.document.forms[0].cbeBussiType.value;
	mstrLocation = mstrLocation + "&dEffecdate=" + self.document.forms[0].tcdEffecdate.value;
}

//% ShowLocalValues: Se muestra el valor de los campos "moneda" y "saldo"
//-------------------------------------------------------------------------------------------
function ShowLocalValues(sParam){
//-------------------------------------------------------------------------------------------
	
	if (self.document.forms[0].cbeCurrency.value==1 || 
	    self.document.forms[0].cbeCurrency.value==0){
		ShowDiv('divlblValDate', 'hide');
		ShowDiv('divtcdValDate', 'hide');
		self.document.forms[0].tcdValDate.value = '';
		}
	else{	  		
		ShowDiv('divlblValDate', 'show');
		ShowDiv('divtcdValDate', 'show');
self.document.forms[0].tcdValDate.value = '<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>';
	    }
	
	if (self.document.forms[0].valClient.value != '' &&
		self.document.forms[0].cbeTypeAccount.value != 0)
		if (sParam == "nCurrency")
	        ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=BussiType&nTypeAccount=" + self.document.forms[0].cbeTypeAccount.value + "&sBussiType=" + self.document.forms[0].cbeBussiType.value + "&sClient=" + self.document.forms[0].valClient.value + "&sCodispl=OP091&sZone=opener", "ShowDefValuesCurrency", 1, 1, "no", "no", 2000, 2000);
		else
			if (self.document.forms[0].cbeCurrency.value !=0)		
				ShowPopUp("/VTimeNet/CashBank/CashBank/ShowDefValues.aspx?Field=Balance&nTypeAccount=" + self.document.forms[0].cbeTypeAccount.value + "&sBussiType=" + self.document.forms[0].cbeBussiType.value+ "&sClient=" + self.document.forms[0].valClient.value + "&nCurrency=" + self.document.forms[0].cbeCurrency.value, "ShowDefValuesAmount", 1, 1,"no","no",2000,2000);

}

//-------------------------------------------------------------------------------------------
function insChangeBussiness(Field){
//-------------------------------------------------------------------------------------------
	if(mstrLocation!="")
	{
		mstrLocation = mstrLocation.replace(/&nBussinessType.*/, "");
		mstrLocation = mstrLocation + "&nBussinessType=" + self.document.forms[0].cbeBussiType.value;
	}
	else
	{
		mstrLocation += document.location.href;
		mstrLocation = mstrLocation.replace(/&nBussinessType.*/, "");
		mstrLocation = mstrLocation + "&nBussinessType=" + self.document.forms[0].cbeBussiType.value;
	}
}

//+ Actualiza los parametros del campo moneda
//-------------------------------------------------------------------------------------------
function updCurrency(){
//-------------------------------------------------------------------------------------------
    var objCurrParams 

    objCurrParams = self.document.forms[0].cbeCurrency.Parameters 
    objCurrParams.Param1.sValue = self.document.forms[0].cbeTypeAccount.value;
    objCurrParams.Param2.sValue = self.document.forms[0].cbeBussiType.value;
    objCurrParams.Param4.sValue = self.document.forms[0].valClient.value;

    self.document.forms[0].cbeCurrency.disabled = false;
    self.document.forms[0].btncbeCurrency.disabled = false;
    self.document.forms[0].cbeCurrency.value = '';
    UpdateDiv('cbeCurrencyDesc','');

}

//+Ejecuta proceso después de cambiar cliente
//+Se creo este procesdo centralizado en vez de usar el OnChange por
//+que este no soporta dos comandos
//-------------------------------------------------------------------------------------------
function insChangeClient(){
//-------------------------------------------------------------------------------------------
    updCurrency();
    ShowLocalValues('nCurrency');
}

</SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP091", "OP091.aspx"))
If CDbl(Request.QueryString.Item("nTypeTrans")) <> 1 Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<%
    Call insPreOP091()
    %>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="ValCashBank.aspx?nTypeTrans=<%=Request.QueryString.Item("nTypeTrans")%>&nRemNum=<%=Request.QueryString.Item("nRemNum")%>">

    <P ALIGN="Center">   
        <LABEL ID=40103><A HREF="#Saldo de la cuenta corriente"> <%= GetLocalResourceObject("AnchorSaldo de la cuenta corrienteCaption") %></A></LABEL><LABEL ID=0> | </LABEL>
		<LABEL ID=40105><A HREF="#Pago"> <%= GetLocalResourceObject("AnchorPagoCaption") %></A></LABEL>
    </P>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=8817><%= GetLocalResourceObject("cbeTypeAccountCaption") %></LABEL></TD>
<%


If Request.QueryString.Item("nTypeAccount") <> vbNullString Then
	Response.Write("<TD>" & mobjValues.PossiblesValues("cbeTypeAccount", "table400", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nTypeAccount"),  ,  ,  ,  ,  , "LockControl(this.value);updCurrency();",  ,  , GetLocalResourceObject("cbeTypeAccountToolTip")) & "</TD>")
Else
	Response.Write("<TD>" & mobjValues.PossiblesValues("cbeTypeAccount", "table400", eFunctions.Values.eValuesType.clngComboType, CStr(mobjCurr_acc.nTyp_acco),  ,  ,  ,  ,  , "LockControl(this.value);updCurrency();",  ,  , GetLocalResourceObject("cbeTypeAccountToolTip")) & "</TD>")
End If
%>
            <TD><LABEL ID=8810><%= GetLocalResourceObject("cbeBussiTypeCaption") %></LABEL></TD>
<%
If Request.QueryString.Item("nBussinessType") <> vbNullString Then
	Response.Write("<TD>" & mobjValues.PossiblesValues("cbeBussiType", "table20", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nBussinessType"),  ,  ,  ,  ,  , "insChangeBussiness(this.value);updCurrency();", True,  , GetLocalResourceObject("cbeBussiTypeToolTip"), eFunctions.Values.eTypeCode.eString) & "</TD>")
Else
	Response.Write("<TD>" & mobjValues.PossiblesValues("cbeBussiType", "table20", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , "insChangeBussiness(this.value);updCurrency();", True,  , GetLocalResourceObject("cbeBussiTypeToolTip"), eFunctions.Values.eTypeCode.eString) & "</TD>")
End If
%>
        </TR>
        <TR>
            <TD><LABEL ID=8811><%= GetLocalResourceObject("valClientCaption") %></LABEL></TD>
<%
If Request.QueryString.Item("sClientCode") <> vbNullString Then
	mstrClient = mobjClient.ExpandCode(Request.QueryString.Item("sClientCode"))
	mobjClient = Nothing
	Response.Write("<TD COLSPAN=3>" & mobjValues.ClientControl("valClient", mstrClient,  , GetLocalResourceObject("valClientToolTip"), "insChangeClient()",  , "lblCliename") & "</TD>")
Else
	Response.Write("<TD COLSPAN=3>" & mobjValues.ClientControl("valClient", mobjCurr_acc.sClient,  , GetLocalResourceObject("valClientToolTip"), "insChangeClient()",  , "lblCliename") & "</TD>")
End If
%>
        </TR>
        <TR>
            <TD><LABEL ID=8812><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
            <TD><%mobjValues.Parameters.Add("nTyp_Acco", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sType_Acc", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nIntermed", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sClient", "", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TABCURR_CLI_INTER", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjCurr_acc.nCurrency), True,  ,  ,  ,  , "ShowLocalValues(""nAmount"")",  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))
%></TD>
            <TD><DIV ID="divlblValDate"><LABEL ID=0><%= GetLocalResourceObject("tcdValDateCaption") %></LABEL></DIV></TD>
<TD><DIV ID="divtcdValDate"><% %>
<%=mobjValues.DateControl("tcdValDate", CStr(Today),  , GetLocalResourceObject("tcdValDateToolTip"))%></DIV></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8813><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<%
If Request.QueryString.Item("dEffecdate") <> vbNullString Then
	Response.Write("<TD>" & mobjValues.DateControl("tcdEffecdate", Request.QueryString.Item("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip")) & "</TD>")
Else
	Response.Write("<TD>" & mobjValues.DateControl("tcdEffecdate", mdtmEffecdate,  , GetLocalResourceObject("tcdEffecdateToolTip")) & "</TD>")
End If
%>
            <TD COLSPAN=2>&nbsp;</TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40106><A NAME="Saldo de la cuenta corriente"><%= GetLocalResourceObject("AnchorSaldo de la cuenta corriente2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>			
            <TD><%=mobjValues.OptionControl(40108, "optCreDeb", GetLocalResourceObject("optCreDeb_moptCreCaption"), moptDeb, moptCre,  , True)%></TD>            
            <TD><%=mobjValues.OptionControl(40109, "optCreDeb", GetLocalResourceObject("optCreDeb_moptDebCaption"), moptCre, moptDeb,  , True)%></TD>
            <TD><%=mobjValues.NumericControl("gmnAmount", 18, mdblAmount,  , GetLocalResourceObject("gmnAmountToolTip"), True, 6,  ,  ,  ,  , True,  , True)%></TD>            
            <TD></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40107><A NAME="Pago"><%= GetLocalResourceObject("AnchorPago2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="4"><HR></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8816><%= GetLocalResourceObject("cbeTypePayCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.TypeList = 2
	.List = "1,4,3"
	Response.Write(mobjValues.PossiblesValues("cbeTypePay", "table193", eFunctions.Values.eValuesType.clngComboType, CStr(CShort(mintTypePay)),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypePayToolTip")))
End With%>
            <TD><LABEL ID=8814><%= GetLocalResourceObject("gmnPayAmountCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("gmnPayAmount", 18, mdblPayAmount,  , GetLocalResourceObject("gmnPayAmountToolTip"), True, 6)%></TD>
        </TR>
    </TABLE>
    <%Response.Write(mobjValues.BeginPageButton)%>
</FORM>
</BODY>
</HTML>
<%
mobjCurr_acc = Nothing
mobjMove_acc = Nothing
mobjValues = Nothing
mobjMenu = Nothing

Response.Write("<SCRIPT>ShowDiv('divlblValDate', 'hide');ShowDiv('divtcdValDate', 'hide');</SCRIPT>")

%>




