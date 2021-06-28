<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eApvc" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mclsApvc As eApvc.Product_Apvc
Dim mobjMenu As eFunctions.Menues

'% insPreDP003: se controla la carga de la página
'--------------------------------------------------------------------------------------------
Sub insPreDP200()
	'--------------------------------------------------------------------------------------------
	Call mclsApvc.find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mclsApvc = New eApvc.Product_Apvc
mobjMenu = New eFunctions.Menues



mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "DP003"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%With Response
	Response.Write(mobjValues.StyleSheet())
	Response.Write(mobjValues.WindowsTitle("DP200"))
	Response.Write(mobjMenu.setZone(2, "DP200", "DP200.aspx"))
End With

Call insPreDP200()

mobjMenu = Nothing
%>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision:   1.4  $|$$Date:   26 Aug 2005 10:25:10  $"
//% insLockControl: se realiza el bloqueo de los campos dependientes
//-------------------------------------------------------------------------------------------
function insLockControl(Field){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeReinHeap.disabled=(Field.value==4)?true:false;
		cbeReinHeap.value=(Field.value==4)?3:cbeReinHeap.value;
	}
}
//% insShowHeader: Recarga los campos del encabezado
//---------------------------------------------------------------------------------------
function insShowHeader(){
//---------------------------------------------------------------------------------------
    var lblnAgain = true
    if (typeof(top.fraHeader.document)!='undefined')
	    if (typeof(top.fraHeader.document.forms[0])!='undefined')
            if (typeof(top.fraHeader.document.forms[0].valProduct)!='undefined'){
		        top.fraHeader.document.forms[0].tcdEffecdate.value = '<%=Session("dEffecdate")%>'
		        top.fraHeader.document.forms[0].cbeProdType.value='<%=Session("sBrancht")%>'
		        top.fraHeader.document.forms[0].cbeBranch.value='<%=Session("nBranch")%>'
		        top.fraHeader.document.forms[0].valProduct.value='<%=Session("nProduct")%>'
		        lblnAgain = false;
		    }
   if (lblnAgain)
      setTimeout("insShowHeader",50);
}

function ChangeValue(objValue)
{
	if(self.document.forms[0].elements["chkDoc_value"].checked)
		self.document.forms[0].elements["chkDoc_value"].value = 1
	else
		self.document.forms[0].elements["chkDoc_value"].value = 2;
		
}

function ChangeValueCollect(objValue)
{
	if(self.document.forms[0].elements["chkCollect_after"].checked)
		self.document.forms[0].elements["chkCollect_after"].value = 1
	else
		self.document.forms[0].elements["chkCollect_after"].value = 2;
		
}

insShowHeader();
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP003" ACTION="valproductseqapvc.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%=mobjValues.ShowWindowsName("DP200")%>
    <BR>
    <TABLE WIDTH="100%">
        <TD><LABEL ID=0><%= GetLocalResourceObject("cbencurrencyCaption") %> </LABEL></TD>
            <TD>
             <%mobjValues.List = "4,1"
mobjValues.TypeList = 1%>
             <%=mobjValues.PossiblesValues("cbencurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsApvc.nCurrency),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbencurrencyToolTip"))%></TD>
             <TD WIDTH = 50> </TD>
             <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPrem_maxCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPrem_max", 9, CStr(mclsApvc.nPrem_max),  , GetLocalResourceObject("tctnPrem_maxToolTip"))%></TD>
           
        </TR>
            <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPrem_minCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPrem_min", 10, CStr(mclsApvc.nPrem_min),  , GetLocalResourceObject("tctnPrem_minToolTip"))%></TD>
            <TD> </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPercentnprenCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPercentnpren", 4, CStr(mclsApvc.nPercentnprem),  , GetLocalResourceObject("tctnPercentnprenToolTip"),  , 2)%></TD>
            
        </TR>
        <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnAmountnpremCaption") %></LABEL></TD>
                        
             <TD><%=mobjValues.NumericControl("tctnAmountnprem", 9, CStr(mclsApvc.nAmountnprem),  , GetLocalResourceObject("tctnAmountnpremToolTip"))%></TD>
             <TD> </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPercentsalaryCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPercentsalary", 4, CStr(mclsApvc.npercentsalary),  , GetLocalResourceObject("tctnPercentsalaryToolTip"),  , 2)%></TD>
        </TR>

           <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnMinstayCaption") %> </LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnMinstay", 10, CStr(mclsApvc.nMinstay),  , GetLocalResourceObject("tctnMinstayToolTip"))%></TD>
            <TD><%=mobjValues.HiddenControl("tctnPermin", CStr(mclsApvc.nPermin))%>  </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnMonthminCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnMonthmin", 5, CStr(mclsApvc.nMonthmin),  , GetLocalResourceObject("tctnMonthminToolTip"))%></TD>
          
        </TR>
        
	</TABLE>
	
</FORM>
</BODY>
</HTML>
<%
mclsApvc = Nothing
%>





