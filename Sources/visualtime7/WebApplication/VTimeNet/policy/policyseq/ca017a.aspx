<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eFinance" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'+ Definicón de objetos 
Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjErrors As eFunctions.Errors
Dim lclsPolicy As ePolicy.Policy
Dim lcolFinanDrafts As eFinance.FinanceDrafts
Dim lclsFinanDraft As Object
Dim lclsRefresh As ePolicy.ValPolicySeq

Dim lclsFinance_co As eFinance.financeCO

'- Variables 
Dim lblnValCa017a As Object
Dim lblnbQuota_Dis As Boolean
Dim lblnbInterest_Dis As Boolean
Dim lblnbInitial_Dis As Boolean
Dim lblnbGrid_Dis As Boolean

Dim mintTransaction As Object
Dim mstrMsgLevel As String


'% insPrevInfo: Rescata los datos iniciales de la secuencia, parte encabezado  
'%              Genera contrato de financiamiento 
'-------------------------------------------------------------------------------------------- 
Private Sub insPrevInfo()
	'-------------------------------------------------------------------------------------------- 
	Dim lclsWindows As eSecurity.Windows
	Dim lclsMenues As Object
	
	If Not lclsFinance_co.insPreCA017A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("nTransaction"), Session("nUsercode"), Session("nReceipt"), mobjValues.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.QueryString.Item("cbeQuota"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcnInitial"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcnInterest"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("lblFirst_Draf"), eFunctions.Values.eTypeData.etdDate)) Then
		
		'+Si se generó algún error.             
		If lclsFinance_co.mlngErrorNum > 0 Then
			Response.Write(mobjErrors.ErrorMessage("CA017A", lclsFinance_co.mlngErrorNum,  ,  ,  , True))
		End If
		lblnbQuota_Dis = True
		lblnbInterest_Dis = True
		lblnbInitial_Dis = True
		lblnbGrid_Dis = True
	Else
		Session("nContrat") = lclsFinance_co.nContrat
		lblnbQuota_Dis = lclsFinance_co.bQuota_Dis
		lblnbInterest_Dis = False
		lblnbInitial_Dis = lclsFinance_co.bInitial_Dis
		lblnbGrid_Dis = False
		
		'+Si se generó algún error.
		If lclsFinance_co.mlngErrorNum > 0 Then
			Response.Write(mobjErrors.ErrorMessage("CA017A", lclsFinance_co.mlngErrorNum,  ,  ,  , True))
		End If
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			'+Refresca el menu lateral 
			lclsRefresh = New ePolicy.ValPolicySeq
			Response.Write(lclsRefresh.RefreshSequence(Request.QueryString.Item("sCodispl"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "NO"))
			lclsRefresh = Nothing
			
		End If
		
		'+Se almacenan los permisos requeridos para la transaccion
		lclsWindows = New eSecurity.Windows
		If lclsWindows.reaWindows("CA017A") Then
			
			'+Se validan permisos de tx contra los permisos asignados al esquema
			'+del usuario en proceso
			Call mobjMenu.ValactionLevel("CA017A", CShort("2"), Session("sSche_code"), lclsWindows.nInqlevel, lclsWindows.nAmelevel)
			'+Se almacena si se permite modificacion datos de la transaccion, 
			'+según nivel de seguridad de usuario
			If mobjMenu.mblnAmeAcces Then
				Response.Write("<SCRIPT>mblnChangeAllow = true;</" & "Script>")
			Else
				Response.Write("<SCRIPT>mblnChangeAllow = false;</" & "Script>")
			End If
			mobjMenu = Nothing
		End If
		lclsWindows = Nothing
		
	End If
End Sub

'% insDefineHeader: se definen las propiedades del grid - parte repetitiva 
'-------------------------------------------------------------------------------------------- 
Private Sub insDefineHeader()
	'-------------------------------------------------------------------------------------------- 
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid desde financ_dra (cuotas) 
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5,  ,  , GetLocalResourceObject("tcnDraftColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat",  ,  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18,  ,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6,  ,  ,  , True)
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_siColumnCaption"), "tcnAmount_si", 18,  ,  , GetLocalResourceObject("tcnAmount_siColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIntammouColumnCaption"), "tcnIntammou", 18,  ,  , GetLocalResourceObject("tcnIntammouColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_afeColumnCaption"), "tcnAmount_afe", 18,  ,  , GetLocalResourceObject("tcnAmount_afeColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmount_exenColumnCaption"), "tcnAmount_exen", 18,  ,  , GetLocalResourceObject("tcnAmount_exenColumnToolTip"), True, 6,  ,  ,  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIvaColumnCaption"), "tcnIva", 18,  ,  , GetLocalResourceObject("tcnIvaColumnToolTip"), True, 6,  ,  ,  , True)
		
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCommissionColumnCaption"), "tcnCommission", 18,  ,  , GetLocalResourceObject("tcnCommissionColumnToolTip"),  , 6,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatus_draColumnCaption"), "cbeStatus_dra", "Table253", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatus_draColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnContrat_refColumnCaption"), "tcnContrat_ref", 5,  ,  , GetLocalResourceObject("tcnContrat_refColumnToolTip"), False)
	End With
	
	'+ Se definen las propiedades generales del grid 
	With mobjGrid
		.Codispl = "CA017A"
		.ActionQuery = mobjValues.ActionQuery
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.sDelRecordParam = "nDraft='+ marrArray[lintIndex].tcnDraft + '" & "&dExpirdat='+ marrArray[lintIndex].tcdExpirdat + '"
		.Top = 50
		.Height = 430
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA017A: se realiza el manejo del grid 
'-------------------------------------------------------------------------------------------- 
Private Sub insPreCA017A()
	'-------------------------------------------------------------------------------------------- 
	Dim lblnFind_ref As Object
	Dim lintIndex As Short
	
	If Not lblnbGrid_Dis Then
		lcolFinanDrafts = New eFinance.FinanceDrafts
		
		If lcolFinanDrafts.Find_certificat_financ_dra(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			'+Si existe financiamiento este es mostrado. Si no existe no. Y si existe mas o menos, quiza.
			lintIndex = 0
			For	Each lclsFinanDraft In lcolFinanDrafts
				lintIndex = lintIndex + 1
				With mobjGrid
					.Columns("tcnDraft").DefValue = CStr(lintIndex)
					.Columns("tcdExpirdat").DefValue = lclsFinanDraft.dLimitdate
					.Columns("tcnAmount").DefValue = lclsFinanDraft.nAmount
					.Columns("tcnCommission").DefValue = lclsFinanDraft.nCommission
					.Columns("tcnContrat_ref").DefValue = lclsFinanDraft.nContrat
					.Columns("cbeStatus_dra").DefValue = lclsFinanDraft.nStat_draft
					.Columns("cbeStatus_dra").Descript = lclsFinanDraft.sStat_Draft
					
					.Columns("tcnAmount_si").DefValue = lclsFinanDraft.namount_net
					.Columns("tcnAmount_afe").DefValue = lclsFinanDraft.namo_afec
					.Columns("tcnAmount_exen").DefValue = lclsFinanDraft.namo_exen
					.Columns("tcnIva").DefValue = lclsFinanDraft.niva
					.Columns("tcnIntammou").DefValue = lclsFinanDraft.nIntammou
					
					Response.Write(.DoRow)
				End With
			Next lclsFinanDraft
		End If
		lclsFinanDraft = Nothing
		lcolFinanDrafts = Nothing
	End If
	
	Response.Write(mobjGrid.closeTable())
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA017A")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjErrors = New eFunctions.Errors
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjErrors.sSessionID = Session.SessionID
mobjErrors.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
lclsPolicy = New ePolicy.Policy
lclsFinance_co = New eFinance.financeCO

mobjValues.ActionQuery = Session("bQuery")

lblnValCa017a = True

mintTransaction = Session("nTransaction")

mstrMsgLevel = mobjErrors.ErrorMessage("CA017A", 99011, 0, 2, "", True, "")

'+Se eliminan caracteres innecesarios del mensaje
mstrMsgLevel = Replace(mstrMsgLevel, "<SCRIPT>", "")
mstrMsgLevel = Replace(mstrMsgLevel, "</SCRIPT>", "")
mstrMsgLevel = Replace(mstrMsgLevel, "alert(""", "")
mstrMsgLevel = Replace(mstrMsgLevel, """)", "")

%> 
<HTML> 
<HEAD> 
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT> 

 

 
<SCRIPT LANGUAGE="JavaScript">  
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 14 $|$$Date: 13/10/04 10:14 $|$$Author: Nvaplat28 $"  

//-Indicador si es posible realizar modificacione en transaccion
//-según privilegios del usuario
var mblnChangeAllow;

//% insStateZone: se controla el estado de los campos de la página 
//-------------------------------------------------------------------------------------------- 
function insStateZone(){ 
//-------------------------------------------------------------------------------------------- 
} 

//% insCancel: se controla la acción Cancelar de la página 
//-------------------------------------------------------------------------------------------- 
function insCancel(){ 
//-------------------------------------------------------------------------------------------- 
	return true; 
} 

//% insFinish: se controla la acción Cancelar de la página 
//-------------------------------------------------------------------------------------------- 
function insFinish(){ 
//-------------------------------------------------------------------------------------------- 
    return true; 
}

//%insReload: se controla el cambio de valor del campo "Evaluación" 
//------------------------------------------------------------------------------------------- 
function insReload(nFieldCod){ 
//------------------------------------------------------------------------------------------- 
	var lstrURL; 
	var frm = self.document.forms[0]
	var sMsgError='<%=mstrMsgLevel%>';
	var doReload = false;
	var lblnDataMod = false;
	 
//+Se almacena cual valor cambió
//+Se dejan primero los que requieren validaciones especiales para que sea
//+más simple su manejo
    if( (nFieldCod==1)&&(frm.cbeQuota.value != frm.hddQuotaOld.value) )
        lblnDataMod = true;
    if( (nFieldCod==2)&&(frm.lblFirst_Draf.value != frm.hddFirst_DrafOld.value) )
        lblnDataMod = true;
    if( (nFieldCod==3)&&(insConvertNumber(frm.tcnInterest.value) != insConvertNumber(frm.hddInterestOld.value)) )
        lblnDataMod = true;
	if( (nFieldCod==4)&&(insConvertNumber(frm.tcnInitial.value) != insConvertNumber(frm.hddInitialOld.value)) )
	    lblnDataMod = true;

//+Si algo cambio
    if(lblnDataMod){
//+Si se permiten modificaciones o son los campos inicial y fecha,
//+se permite recargar con nuevos valores
        if (mblnChangeAllow ||
           (nFieldCod=='4' || nFieldCod=='2'))
            doReload = true;
//+No se permiten modificaciones
        else
//+en los casos de cuotas e interes la tx se recarga sólo si 
//+el mensaje era una advertencia, y el usuario acepta la condición.
            if(sMsgError.indexOf('Adv.')>-1)
                doReload = confirm(sMsgError);
            else
                alert(sMsgError);
        
        if(doReload){
	        lstrURL = document.location.href.replace(/Reload=1/,'Reload=2'); 
            lstrURL = lstrURL.replace(/&tcnInitial=.*/,'') + "&tcnInitial=" + self.document.forms[0].tcnInitial.value + "&tcnInterest=" + self.document.forms[0].tcnInterest.value + "&lblFirst_Draf=" + self.document.forms[0].lblFirst_Draf.value + "&cbeQuota=" + self.document.forms[0].cbeQuota.value + "&hddInterest=" +  self.document.forms[0].hddInterest.value;
	        document.location.href = lstrURL;
        }	        
	    else    {
	        frm.tcnInitial.value = frm.hddInitialOld.value;
	        frm.tcnInterest.value = frm.hddInterestOld.value;
	        frm.lblFirst_Draf.value = frm.hddFirst_DrafOld.value;
	        frm.cbeQuota.value = frm.hddQuotaOld.value;
        }
                    
    }    	    
} 
</SCRIPT> 
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
		.Write(mobjMenu.setZone(2, "CA017A", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End If
End With
%> 
</HEAD> 
<BODY ONUNLOAD="closeWindows();"> 
<FORM METHOD="POST" ID="CA017A" NAME="frmCA017A" ACTION="valPolicySeq.aspx?x=1"> 
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insPrevInfo()
%> 
    <TABLE WIDTH="100%"> 
        <TR> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnContratCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnContrat", 10, CStr(lclsFinance_co.nContrat),  , GetLocalResourceObject("tcnContratToolTip"),  ,  ,  ,  ,  ,  , True)%> </TD> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeWay_PayCaption") %></LABEL></TD> 
			<TD><%=mobjValues.PossiblesValues("cbeWay_Pay", "table5002", eFunctions.Values.eValuesType.clngComboType, CStr(lclsFinance_co.nWay_Pay),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWay_PayToolTip"))%> </TD> 
        </TR> 
        <TR> 
            <TD><LABEL ID=8072><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD> 
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsFinance_co.nCurrency),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPremiumCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnPremium", 18, CStr(lclsFinance_co.nPremiumN),  , GetLocalResourceObject("tcnPremiumToolTip"),  , 6,  ,  ,  ,  , True)%> </TD> 
        </TR> 
        <TR> 
<%
' Se muestra solo en secuencia de modificación 
If mintTransaction = eCollection.Premium.PolTransac.clngPolicyAmendment Or mintTransaction = eCollection.Premium.PolTransac.clngTempPolicyAmendment Or mintTransaction = eCollection.Premium.PolTransac.clngCertifAmendment Or mintTransaction = eCollection.Premium.PolTransac.clngTempCertifAmendment Or mintTransaction = eCollection.Premium.PolTransac.clngPolicyQuotAmendent Or mintTransaction = eCollection.Premium.PolTransac.clngCertifQuotAmendent Or mintTransaction = eCollection.Premium.PolTransac.clngPolicyPropAmendent Or mintTransaction = eCollection.Premium.PolTransac.clngCertifPropAmendent Or mintTransaction = eCollection.Premium.PolTransac.clngQuotAmendConvertion Or mintTransaction = eCollection.Premium.PolTransac.clngPropAmendConvertion Or mintTransaction = eCollection.Premium.PolTransac.clngPolicyPropRenewal Or mintTransaction = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Then%> 
				<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPremiumPCaption") %></LABEL></TD> 
				<TD><%=mobjValues.NumericControl("tcnPremiumP", 18, CStr(lclsFinance_co.nPremiumP),  , GetLocalResourceObject("tcnPremiumPToolTip"),  , 6,  ,  ,  ,  , True)%> </TD> 
<%	
Else
	%> 
				<TD></TD> 
				<TD><%=mobjValues.HiddenControl("tcnPremiumP", CStr(lclsFinance_co.nPremiumP))%> </TD> 
<%	
End If
%> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnPremiumTCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnPremiumT", 18, CStr(lclsFinance_co.nPremiumT),  , GetLocalResourceObject("tcnPremiumTToolTip"),  , 6,  ,  ,  ,  , True)%> </TD> 
        </TR> 
        <TR> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbeQuotaCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("cbeQuota", 5, CStr(lclsFinance_co.nQuota),  , GetLocalResourceObject("cbeQuotaToolTip"),  ,  ,  ,  ,  , "insReload(1);", lblnbQuota_Dis)%> </TD> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("lblFirst_DrafCaption") %></LABEL></TD> 
			<TD><%=mobjValues.DateControl("lblFirst_Draf", CStr(lclsFinance_co.dFirst_draf),  , GetLocalResourceObject("lblFirst_DrafToolTip"),  ,  ,  , "insReload(2);")%> </TD> 
        </TR> 
        <TR> 
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnInterest", 4, CStr(lclsFinance_co.nInterest),  , GetLocalResourceObject("tcnInterestToolTip"),  , 2,  ,  ,  , "insReload(3);", lblnbInterest_Dis)%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnInitialCaption") %></LABEL></TD> 
			<TD><%=mobjValues.NumericControl("tcnInitial", 18, CStr(lclsFinance_co.nInitial),  , GetLocalResourceObject("tcnInitialToolTip"),  , 6,  ,  ,  , "insReload(4);", lblnbInitial_Dis)%> </TD> 
<%
If Request.QueryString.Item("hddInterest") <> vbNullString Then
	Response.Write(mobjValues.HiddenControl("hddInterest", Request.QueryString.Item("hddInterest")))
Else
	Response.Write(mobjValues.HiddenControl("hddInterest", CStr(lclsFinance_co.nInterest)))
End If
Response.Write(mobjValues.HiddenControl("tcnPayfreq", CStr(lclsFinance_co.nPayfreq)))
Response.Write(mobjValues.HiddenControl("tcnQuoPend", CStr(lclsFinance_co.nQuotaPend)))
Response.Write(mobjValues.HiddenControl("tcnValQuot", CStr(lclsFinance_co.nValQuota)))
Response.Write(mobjValues.HiddenControl("tcdFirst_draf", CStr(lclsFinance_co.dFirst_draf)))
Response.Write(mobjValues.HiddenControl("hddbValCa017a", lblnValCa017a))

Response.Write(mobjValues.HiddenControl("hddInitialOld", CStr(lclsFinance_co.nInitial)))
Response.Write(mobjValues.HiddenControl("hddInterestOld", CStr(lclsFinance_co.nInterest)))
Response.Write(mobjValues.HiddenControl("hddFirst_DrafOld", CStr(lclsFinance_co.dFirst_draf)))
Response.Write(mobjValues.HiddenControl("hddQuotaOld", CStr(lclsFinance_co.nQuota)))
%>
        </TR> 
    </TABLE> 
<%

Call insDefineHeader()
Call insPreCA017A()

If Request.QueryString.Item("Type") = "PopUp" Then
	%>
    <P ALIGN=RIGHT>
       <TABLE WIDTH=100%>
           <TR><TD CLASS="Horline"></TD></TR>
           <TR><TD ALIGN=RIGHT><%=mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel)%></TD></TR>
    </P>
<%	
End If

mobjValues = Nothing
mobjGrid = Nothing
mobjErrors = Nothing
lclsPolicy = Nothing
lclsFinance_co = Nothing
%> 
</FORM> 
</BODY> 
</HTML> 
 

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04 
Call mobjNetFrameWork.FinishPage("CA017A")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




