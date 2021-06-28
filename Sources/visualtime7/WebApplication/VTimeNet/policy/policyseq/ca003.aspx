<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Dim mobjNetFrameWork As eNetFrameWork.Layout

    '- Objeto para el manejo de las funciones generales de carga de valores 
    Dim mobjValues As New eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mclsDirDebit As ePolicy.DirDebit
    Dim mstrTable_Account As String
    Dim mstrQueryString As String
    Dim lblnDisabTar As Boolean
    Dim lblnDisabCta As Boolean
    Dim mstrUpd As String


</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("CA003")
    '~End Header Block VisualTimer Utility
    Response.CacheControl = "private"

    'mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mclsDirDebit = New ePolicy.DirDebit
    mobjMenu = New eFunctions.Menues
    '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
    mobjMenu.sSessionID = Session.SessionID
    mobjMenu.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility
%> 

<SCRIPT> 
    var mstrType_Debit
    var mstrInd
    var mintDay
    var mstrCLient

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 4 $|$$Date: 12/11/03 18:06 $|$$Author: Nvaplat18 $"

//% insChangeType_Debit: Controla la propiedad OnClick de los campos  
//----------------------------------------------------------------------------- 
function insChangeType_Debit(){ 
//----------------------------------------------------------------------------- 

    with (self.document.forms[0]){ 
		if (mstrInd == ""){
        valAccount.value = ""; 
        if (optBank[0].checked){
            
//Tarjeta de credito
            cbeTyp_crecard.value = ""; 
            cbeTyp_crecard.disabled = true;
            valcredi_card.value = ""; 
            valcredi_card.disabled = true;
            btnvalcredi_card.disabled = true;
            tcdDateExpir.value = "";
            tcdDateExpir.disabled = true;
            btn_tcdDateExpir.disabled = true;                

//Cta Bancaria
            cbeBankExt.value = ""; 
            cbeBankExt.disabled = false;
            btncbeBankExt.disabled = false;
            valAccount.value = ""; 
            valAccount.disabled = true;
            btnvalAccount.disabled = true;
            cbeTyp_Account.value = "";
            cbeTyp_Account.disabled = true; 
            tctBankAuth.value = ""; 
            tctBankAuth.disabled = false; 
            tcnBillDay.value = mintDay; 
            tcnBillDay.disabled = true; 
            valAccount.sTabName = "tabbk_account"; 
        } 
        else {
            if (optBank[1].checked){
//Tarjeta de credito
                cbeTyp_crecard.value = ""; 
                cbeTyp_crecard.disabled = false;
                valcredi_card.value = ""; 
                valcredi_card.disabled = false;
                btnvalcredi_card.disabled = false;                
                tcdDateExpir.value = "";
                tcdDateExpir.disabled = false;
                btn_tcdDateExpir.disabled = false;

//Cta Bancaria
                cbeBankExt.value = "";
                cbeBankExt.disabled = false;
                btncbeBankExt.disabled = false;
                valAccount.value = ""; 
                valAccount.disabled = true;
                btnvalAccount.disabled = true;                
                cbeTyp_Account.disabled = true; 
                cbeTyp_Account.value = "";
                tctBankAuth.value = ""; 
                tctBankAuth.disabled = false; 
				tcnBillDay.value = mintDay; 
				tcnBillDay.disabled = true; 
                valAccount.sTabName = "tabcred_card"; 
            }
        } 
        }
    }
} 

//% InsChange_Client: Habilita o deshabilita los campos del folder.
//----------------------------------------------------------------
function InsChange_Client(FieldName){
//----------------------------------------------------------------
    var Field = document.getElementById(FieldName);
    with (self.document.forms[0]){
        if (Field.value == "") {
            cbeBankExt.disabled = true;
            cbeBankExt.value = "";
            btncbeBankExt.disabled = valAccount.disabled;

            valAccount.disabled = true;
            valAccount.value = "";
            btnvalAccount.disabled = valAccount.disabled;

            valcredi_card.disabled = true;
            valcredi_card.value = "";
            btnvalcredi_card.disabled = valcredi_card.disabled;

            cbeTyp_Account.disabled = true;
            cbeTyp_Account.value = "";
        }
        else {
            cbeBankExt.disabled = false;
            btncbeBankExt.disabled = cbeBankExt.disabled;

            valAccount.disabled = false;
            btnvalAccount.disabled = valAccount.disabled;
            valAccount.Parameters.Param2.sValue=Field.value

            valcredi_card.disabled = false;
            btnvalcredi_card.disabled = valcredi_card.disabled;
            valcredi_card.Parameters.Param2.sValue=Field.value            

            if (mstrCLient != Field.value){
                mstrCLient = Field.value;
                cbeBankExt.value = "";

                valAccount.disabled = true;
                valAccount.value = "";
                btnvalAccount.disabled = valAccount.disabled;

                cbeTyp_Account.disabled = true;

                valcredi_card.disabled = true;
                valcredi_card.value = ""; 
                btnvalcredi_card.disabled = valcredi_card.disabled; 
            } 
        } 
    } 
} 

//% InsChange_Bank: Cambia valor de banco 
//-----------------------------------------------------
function InsChange_Bank(Field){
//-----------------------------------------------------

    with (self.document.forms[0]){
        if (Field.value == "") {
            valAccount.disabled = true;
            valAccount.value = "";
            btnvalAccount.disabled = valAccount.disabled;
            cbeTyp_Account.disabled = true;
        }
        else {
			if (optBank[0].checked){
				valAccount.disabled = false;
				valAccount.value = "";
				cbeTyp_Account.value = "";
				tctBankAuth.value = "";
				btnvalAccount.disabled = valAccount.disabled;

				valAccount.Parameters.Param2.sValue=Field.value         
            }
        }
    }
}

//% InsChangeAccount: Se ejecuta en el OnChange del campo Cuenta   
//--------------------------------------------------------------------------------------------
function InsChangeAccount(){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (cbeBankExt.value != "" && 
            valAccount.value != "")
            insDefValues('Account', 'sClient=' + tctClient.value + '&nBank_code=' + cbeBankExt.value + '&sAccount=' + valAccount.value + '&sType_debit=' + mstrType_Debit, '/VTimeNet/Policy/PolicySeq/')            
        else {
            tctBankAuth.value = "";
            tcdDateExpir.value = "";
        }
    }
}

//% InsChangecredi_card: Se ejecuta en el OnChange del campo Número   
//--------------------------------------------------------------------------------------------
function InsChangecredi_card(){
//--------------------------------------------------------------------------------------------

    with (self.document.forms[0]){
        if (valcredi_card.value != ""){
            if (tctClient.value != "")
                insDefValues('CreditCard_Data', 'sClient=' + tctClient.value + '&sAccount=' + valcredi_card.value, '/VTimeNet/Policy/PolicySeq/')
        }
        else {
            cbeTyp_crecard.value = "";
            tcdDateExpir.value = "";
        }
    }
}

</SCRIPT>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></SCRIPT>
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
    Response.Write(mobjValues.StyleSheet())
    Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
    mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ACTION="valPolicySeq.aspx?Action=Add">
<%
    Call mclsDirDebit.insPreCA003(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble))

    mstrUpd = mclsDirDebit.mstrDirDebit
    mobjValues.ActionQuery = Session("bQuery") Or mclsDirDebit.bDisabledAll

    If mclsDirDebit.mstrDirDebit = "2" Then
        mstrTable_Account = "tabcred_card"
        lblnDisabTar = False
        lblnDisabCta = True
    Else
        If mclsDirDebit.mstrDirDebit = "1" Then
            mstrTable_Account = "tabbk_account"
            mclsDirDebit.mstrDirDebit = "1"
            lblnDisabTar = True
            lblnDisabCta = False
        Else
            If mclsDirDebit.nWay_pay = 2 Then
                mstrTable_Account = "tabcred_card"
                mclsDirDebit.mstrDirDebit = "2"
            Else
                mstrTable_Account = "tabbk_account"
                mclsDirDebit.mstrDirDebit = "1"
            End If
            lblnDisabTar = True
            lblnDisabCta = False
        End If
    End If
    With Response
        .Write(mobjValues.HiddenControl("hddWay_pay", CStr(mclsDirDebit.nWay_pay)))
        .Write(mobjValues.HiddenControl("hddDirind", mclsDirDebit.sDirind))
        .Write("<SCRIPT>")
        .Write("mstrType_Debit=""" & mclsDirDebit.mstrDirDebit & """;")
        .Write("mintDay=""" & mclsDirDebit.nBill_day & """;")
        .Write("mstrInd=""" & mstrUpd & """;")
        .Write("</SCRIPT>")
    End With
%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD COLSPAN="3"></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optBank", GetLocalResourceObject("optBank_1Caption"), mclsDirDebit.mstrDirDebit, "1", "insChangeType_Debit();", mclsDirDebit.mstrDirDebit = "2")%></TD>
            <TD COLSPAN="3">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optBank", GetLocalResourceObject("optBank_2Caption"), CStr(3 - CShort(mclsDirDebit.mstrDirDebit)), "2", "insChangeType_Debit();", mclsDirDebit.mstrDirDebit = "1")%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=12939><%= GetLocalResourceObject("tcnBillDayCaption") %></LABEL></TD> 
            <TD><%=mobjValues.NumericControl("tcnBillDay", 2, CStr(mclsDirDebit.nBill_day),  , GetLocalResourceObject("tcnBillDayToolTip"),  ,  ,  ,  ,  ,  , True)%></TD>  
        </TR>
<%
'+ Si la Vía de pago es diferente a "PAC" y "TRANSBANK" se inhabilitan todos los campos de la ventana.
If mclsDirDebit.nWay_pay <> 1 And mclsDirDebit.nWay_pay <> 2 Then
	lblnDisabTar = True
	lblnDisabCta = True
	Response.Write("<SCRIPT>self.document.forms[0].optBank[0].disabled=true;</SCRIPT>")
	Response.Write("<SCRIPT>self.document.forms[0].optBank[1].disabled=true;</SCRIPT>")
End If
%>        
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=12942><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
            <TD COLSPAN="4">
                <%mobjValues.TypeList = 1
                    mstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")

                    Response.Write(mobjValues.ClientControl("tctClient", mclsDirDebit.StateVarCa003(0, 1), , GetLocalResourceObject("tctClientToolTip"), "InsChange_Client('tctClient')", mclsDirDebit.StateVarCa003(0, 0), "lblTitular", False, , , , eFunctions.Values.eTypeClient.SearchClientPolicy, , , , mstrQueryString))%>
            </TD>
        </TR>    
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR> 
        <TR> 
            <TD><LABEL><%= GetLocalResourceObject("cbeBankExtCaption") %></LABEL></TD>
            <TD><%
                    'mobjValues.Parameters.Add("sClient", mclsDirDebit.StateVarCa003(0, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    'mobjValues.Parameters.Add("nWay_pay", mclsDirDebit.nWay_pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write("<SCRIPT>mstrCLient = '" & mclsDirDebit.StateVarCa003(0, 1) & "';</script>")
                    Response.Write(mobjValues.PossiblesValues("cbeBankExt", "table7", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsDirDebit.StateVarCa003(3, 1)), False,  ,  ,  ,  , "InsChange_Bank(this);", False,  , GetLocalResourceObject("cbeBankExtToolTip"),  , 3))
%>
            </TD>         
            <TD>&nbsp;</TD>        
            <TD><LABEL ID=0><%= GetLocalResourceObject("valAccountCaption") %></LABEL>
            <TD>
            <%
                With mobjValues
                    .Parameters.Add("sClient", mclsDirDebit.StateVarCa003(0, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nBankExt", mclsDirDebit.StateVarCa003(3, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("valAccount", "tabbk_account", eFunctions.Values.eValuesType.clngWindowType, mclsDirDebit.StateVarCa003(4, 1), True,  ,  ,  ,  , "InsChangeAccount();", mclsDirDebit.StateVarCa003(4, 0), 20, GetLocalResourceObject("valAccountToolTip"), eFunctions.Values.eTypeCode.eString, 4, False, True))
                End With
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=12945><%= GetLocalResourceObject("cbeTyp_AccountCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTyp_Account", "table190", eFunctions.Values.eValuesType.clngComboType, CStr(mclsDirDebit.StateVarCa003(9, 1)),  ,  ,  ,  ,  ,  , mclsDirDebit.StateVarCa003(9, 0),  , GetLocalResourceObject("cbeTyp_AccountToolTip"))%></TD>
			<TD>&nbsp;</TD>            
            <TD><LABEL ID=12939><%= GetLocalResourceObject("tctBankAuthCaption") %></LABEL></TD> 
            <TD><%=mobjValues.TextControl("tctBankAuth", 15, CStr(mclsDirDebit.StateVarCa003(7, 1)),  , GetLocalResourceObject("tctBankAuthToolTip"))%></TD> 
        </TR>
        <TR>
            <TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=12945><%= GetLocalResourceObject("cbeTyp_crecardCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTyp_crecard", "table183", eFunctions.Values.eValuesType.clngComboType, CStr(mclsDirDebit.StateVarCa003(6, 1)),  ,  ,  ,  ,  ,  , lblnDisabTar,  , GetLocalResourceObject("cbeTyp_crecardToolTip"))%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valcredi_cardCaption") %></LABEL>
            <TD>
                <%
                    mobjValues.Parameters.Add("sClient", mclsDirDebit.StateVarCa003(0, 1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    mobjValues.Parameters.Add("nBankExt", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    Response.Write(mobjValues.PossiblesValues("valcredi_card", "tabcred_card", eFunctions.Values.eValuesType.clngWindowType, mclsDirDebit.StateVarCa003(10, 1), True,  ,  ,  ,  , "InsChangecredi_card();", lblnDisabTar, 20, GetLocalResourceObject("valcredi_cardToolTip"), eFunctions.Values.eTypeCode.eString, 4, False, True))
%>
            </TD>
        </TR>
        <TR>
            <TD><LABEL ID=12944><%= GetLocalResourceObject("tcdDateExpirCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.DateControl("tcdDateExpir", mclsDirDebit.StateVarCa003(8, 1),  , GetLocalResourceObject("tcdDateExpirToolTip"),  ,  ,  ,  , lblnDisabTar)%></TD>
        </TR>
    </TABLE>
<%
    If Not mobjValues.ActionQuery Then
        Response.Write("<SCRIPT>insChangeType_Debit()</SCRIPT>")
    End If
    mobjValues = Nothing
    mclsDirDebit = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
    Call mobjNetFrameWork.FinishPage("CA003")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer
%>




