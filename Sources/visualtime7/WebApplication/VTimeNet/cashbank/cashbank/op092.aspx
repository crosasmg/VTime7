<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjCurr_acc As eCashBank.Curr_acc
Dim mobjMove_acc As eCashBank.Move_acc
Dim moptCre As String
Dim moptDeb As String
Dim moptAmoCre As Object
Dim moptAmoDeb As Object


'%insPreOP092: Esta función se encaga de obtener los datos de la cuenta corriente
'--------------------------------------------------------------------------------------------
Private Sub insPreOP092()
	'--------------------------------------------------------------------------------------------
	mobjCurr_acc = New eCashBank.Curr_acc
	mobjMove_acc = New eCashBank.Move_acc
	
	Call mobjCurr_acc.findClientCurr_acc(mobjValues.StringToType(Session("nTypeAccount"), eFunctions.Values.eTypeData.etdInteger), Session("sBussiType"), Session("sClient"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger))
	
	If mobjCurr_acc.nBalance < 0 Then
		moptDeb = "1"
		moptCre = "2"
	Else
		moptDeb = "2"
		moptCre = "1"
	End If
	
	If mobjMove_acc.FindMove(mobjValues.StringToType(Session("nTypeAccount"), eFunctions.Values.eTypeData.etdInteger), Session("sBussiType"), Session("sClient"), mobjValues.StringToType(Session("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nTransact"), eFunctions.Values.eTypeData.etdInteger)) Then
		If mobjMove_acc.nCredit <> 0 Then
			moptAmoDeb = "2"
			moptAmoCre = "1"
		Else
			moptAmoDeb = "1"
			moptAmoCre = "2"
		End If
	End If
	
End Sub

</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With

mobjValues.sCodisplPage = "op092"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 5 $|$$Date: 14/10/04 9:46 $|$$Author: Mmmiola $"
    
//--------------------------------------------------------------------------------------------
function ChangeType(Field){
//--------------------------------------------------------------------------------------------
<%If Session("nTypeAccount") = 5 Then%>
	if(typeof(document.forms[0].cbeBranch)!='undefined'){

	    if (Field.value == 323)
	        {
			document.forms[0].cbeBranch.disabled=false;
			}
	    else	    
			{
			document.forms[0].cbeBranch.value = 0;
			document.forms[0].valProduct.value = "";
			document.forms[0].tcnPolicy.value = "";
			document.forms[0].valProduct.disabled = true;
			document.forms[0].btnvalProduct.disabled = true;
			document.forms[0].tcnPolicy.disabled = true;
			document.forms[0].cbeBranch.disabled = true;
			UpdateDiv("valProductDesc","");
			}
	
		}
<%Else
	Select Case Session("nTypeAccount")
		Case 1, 10, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25%>
	if(typeof(document.forms[0].cbeBranch)!='undefined'){
	    if (Field.value == 323 || Field.value == 322 )
			document.forms[0].cbeBranch.disabled=false;
	    else	    
			{
			document.forms[0].cbeBranch.value = 0;
			document.forms[0].valProduct.value = "";
			document.forms[0].tcnPolicy.value = "";
			document.forms[0].valProduct.disabled = true;
			document.forms[0].btnvalProduct.disabled = true;
			document.forms[0].tcnPolicy.disabled = true;
			document.forms[0].cbeBranch.disabled = true;
			UpdateDiv("valProductDesc","");
			}
	}		
<%	End Select
End If%>	
}
//--------------------------------------------------------------------------------------------
function ChangeBranch(Field){
//--------------------------------------------------------------------------------------------

	if(typeof(document.forms[0].valProduct)!='undefined'){
	    if (Field.value == "")
	        {
			self.document.forms[0].valProduct.value = "";
			self.document.forms[0].tcnPolicy.value = "";
			self.document.forms[0].valProduct.disabled=true;
			self.document.forms[0].btnvalProduct.disabled=true;
			self.document.forms[0].tcnPolicy.disabled=true;
		}
	    else
	        {
			self.document.forms[0].valProduct.Parameters.Param1.sValue=Field.value;
			self.document.forms[0].valProduct.disabled=false;
			self.document.forms[0].btnvalProduct.disabled=false;
			self.document.forms[0].tcnPolicy.disabled=false;
		}
	}
}
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeCredit(nAmount){
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	var ldblOrigAmo=0;
	var ldblCredit=0;
	var ldblEndAmo=0;
	
	if (nAmount=='')
		self.document.forms[0].gmnNewBalance.value='';
	else{

		if(self.document.forms[0].optBOCreDeb[0].checked)
			ldblOrigAmo = (insConvertNumber(self.document.forms[0].gmnOldBalance.value) * -1);
			
		if(self.document.forms[0].optBOCreDeb[1].checked)
			ldblOrigAmo = insConvertNumber(self.document.forms[0].gmnOldBalance.value);
			
		if(self.document.forms[0].optAmoCreDeb[0].checked)
			ldblCredit = (insConvertNumber(self.document.forms[0].gmnCredit.value) * -1);

		if(self.document.forms[0].optAmoCreDeb[1].checked)
			ldblCredit = insConvertNumber(self.document.forms[0].gmnCredit.value);
	
		ldblEndAmo = ldblOrigAmo + ldblCredit;
	
		if (ldblEndAmo < 0)
		{
 			self.document.forms[0].optBNCreDeb[0].checked = true;
			self.document.forms[0].optBNCreDeb[1].checked = false;
		}
		else
		{
			self.document.forms[0].optBNCreDeb[0].checked = false;
			self.document.forms[0].optBNCreDeb[1].checked = true;		
		}
		if (ldblEndAmo<0) ldblEndAmo= ldblEndAmo * -1
		self.document.forms[0].gmnNewBalance.value = VTFormat(ldblEndAmo, '', '', '', 2, true);
	}    
}

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function ChangeAmount()
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
{
	if(typeof(self.document.forms[0].elements["gmnCredit"].value)!='undefined' &&
		self.document.forms[0].elements["gmnCredit"].value>0)
		ChangeCredit(self.document.forms[0].elements["gmnCredit"].value);
}
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
function insPostOP006()
//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
{
	if ((<%=Session("nTypeAccount")%>==1)||(<%=Session("nTypeAccount")%>==21))
	{
	document.forms[0].cbeBranch.disabled=false;
	}
}
</SCRIPT>
    <%Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "OP092_K", "OP092.aspx"))
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
	mobjValues.ActionQuery = True
End If
%>
</HEAD>
<%Call insPreOP092()%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCurrenAccounMove" ACTION="ValCashBank.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <P ALIGN="Center">
        <LABEL ID=40110><A HREF="#Movimiento"> <%= GetLocalResourceObject("AnchorMovimientoCaption") %></A></LABEL>&nbsp;|&nbsp;
        <LABEL ID=40111><A HREF="#Saldo actual"> <%= GetLocalResourceObject("AnchorSaldo actualCaption") %></A></LABEL>&nbsp;|&nbsp;
        <LABEL ID=40112><A HREF="#Monto"> <%= GetLocalResourceObject("AnchorMontoCaption") %></A></LABEL>&nbsp;|&nbsp;
        <LABEL ID=40113><A HREF="#Saldo después de movimiento"> <%= GetLocalResourceObject("AnchorSaldo después de movimientoCaption") %></A></LABEL>
    </P>
    <TABLE WIDTH="100%" COLS="5">
        </TR>
        <TR>
            <TD COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=8784><%= GetLocalResourceObject("cbeTypeMovCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeTypeMov", "table401", eFunctions.Values.eValuesType.clngComboType, mobjValues.TypetoString(mobjMove_acc.nType_move, eFunctions.Values.eTypeData.etdInteger),  ,  ,  ,  ,  , "ChangeType(this);",  ,  , GetLocalResourceObject("cbeTypeMovToolTip"))%></TD>            
			<TD>&nbsp;</TD>
            <TD><LABEL ID=8783><%= GetLocalResourceObject("txtDescriptCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("txtDescript", 30, mobjMove_acc.sDescript,  , GetLocalResourceObject("txtDescriptToolTip"))%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=8787><%= GetLocalResourceObject("gmdValDateCaption") %></LABEL></TD>
			<%If mobjValues.TypetoString(mobjMove_acc.dValueDate, eFunctions.Values.eTypeData.etdDate) = vbNullString Then%>
			<TD><%=	mobjValues.DateControl("gmdValDate", CStr(Today),  , GetLocalResourceObject("gmdValDateToolTip"))%></TD>
			<%Else%>
			<TD><%=mobjValues.DateControl("gmdValDate", mobjValues.TypetoString(mobjMove_acc.dValueDate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("gmdValDateToolTip"))%></TD>
			<%End If%>
			<TD COLSPAN="1">&nbsp;</TD>
    		<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", 1, CStr(mobjMove_acc.nBranch),  ,  ,  ,  ,  , "ChangeBranch(this);", True,  , GetLocalResourceObject("cbeBranchToolTip")))%></TD>
         </TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
		    <%
		        With mobjValues.Parameters
		            .Add("nBranch", mobjValues.StringToType(CStr(mobjMove_acc.nBranch), eFunctions.Values.eTypeData.etdInteger), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        End With
		    %>
		    <TD><%=mobjValues.PossiblesValues("valProduct", "tabProdmaster1", 2, mobjValues.StringToType(CStr(mobjMove_acc.nProduct), eFunctions.Values.eTypeData.etdInteger), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valProductToolTip"))%></TD>			
		    <TD COLSPAN="1">&nbsp;</TD>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPolicy", 8, CStr(mobjMove_acc.nPolicy),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  ,  , True)%></TD>             						
		</TR>	
        <TR>
            <TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40114><A NAME="Saldo actual"><%= GetLocalResourceObject("AnchorSaldo actual2Caption") %></A></LABEL></TD>
			<TD>&nbsp;</TD>            
            <TD COLSPAN="2"CLASS="HighLighted"><LABEL ID=40116><A NAME="Monto"><%= GetLocalResourceObject("AnchorMonto2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD></TD>            
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>



        <TR>
            <TD COLSPAN="2">
                <TABLE WIDTH="100%">
                <TR>
			        <TD><%=mobjValues.OptionControl(40117, "optBOCreDeb", GetLocalResourceObject("optBOCreDeb_DCaption"), moptDeb,  ,  , True)%></TD>
                    <TD><%=mobjValues.OptionControl(40118, "optBOCreDeb", GetLocalResourceObject("optBOCreDeb_ACaption"), moptCre,  ,  , True)%></TD>
                 	<TD><%=mobjValues.NumericControl("gmnOldBalance", 18, CStr(System.Math.Abs(mobjCurr_acc.nBalance)),  , GetLocalResourceObject("gmnOldBalanceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			    </TR>
			    </TABLE>
			</TD>
			<TD></TD>
            <TD COLSPAN="2">
                <TABLE WIDTH="100%">
				    <TR>
<%
If moptAmoCre = moptAmoDeb Then
	moptAmoCre = 1
End If
%>
						<TD><%=mobjValues.OptionControl(40119, "optAmoCreDeb", GetLocalResourceObject("optAmoCreDeb_1Caption"), moptAmoDeb, "1", "ChangeAmount()")%></TD>
						<TD><%=mobjValues.OptionControl(40120, "optAmoCreDeb", GetLocalResourceObject("optAmoCreDeb_2Caption"), moptAmoCre, "2", "ChangeAmount()")%></TD>
						<%If mobjValues.StringToType(CStr(mobjMove_acc.nAmount), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then%>
						    <TD><%=mobjValues.NumericControl("gmnCredit", 18, mobjValues.StringToType(CStr(mobjMove_acc.nAmount), eFunctions.Values.eTypeData.etdDouble),  , GetLocalResourceObject("gmnCreditToolTip"), True, 6,  ,  ,  , "ChangeCredit(this.value);")%></TD>
						<%Else%>
						    <TD><%=mobjValues.NumericControl("gmnCredit", 18, CStr(System.Math.Abs(mobjValues.StringToType(CStr(mobjMove_acc.nAmount), eFunctions.Values.eTypeData.etdDouble))),  , GetLocalResourceObject("gmnCreditToolTip"), True, 6,  ,  ,  , "ChangeCredit(this.value);")%></TD>						    
						<%End If%>
				    </TR>
				</TABLE>
            </TD>
		</TR>	
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=40115><A NAME="Saldo después de movimiento"><%= GetLocalResourceObject("AnchorSaldo después de movimiento2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD WIDTH="100%" COLSPAN="5" CLASS="HorLine"></TD>
        </TR>
        <TR>
        	<TD><%=mobjValues.OptionControl(40121, "optBNCreDeb", GetLocalResourceObject("optBNCreDeb_DCaption"),  ,  ,  , True)%></TD>
			<TD><%=mobjValues.OptionControl(40122, "optBNCreDeb", GetLocalResourceObject("optBNCreDeb_ACaption"),  ,  ,  , True)%></TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.NumericControl("gmnNewBalance", 19, "",  , GetLocalResourceObject("gmnNewBalanceToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
		</TR>
		<%If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 303 And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then%>
        <TR>
            <TD WIDTH="100%" COLSPAN="4">&nbsp;</TD>
        </TR>
        <%End If%>
    </TABLE>
<Script>insPostOP006();</Script>    
</FORM>
</BODY>
</HTML>
<%
    mobjCurr_acc = Nothing
    mobjMove_acc = Nothing
    mobjValues = Nothing
    mobjMenu = Nothing
%>




