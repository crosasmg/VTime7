<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eApvc" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mclsApvc As eApvc.Life_Apvc
Dim mobjMenu As eFunctions.Menues
Dim mclsProduct As eProduct.Product
Dim mobjblnCertif As Boolean

'% insPreDP003: se controla la carga de la página
'--------------------------------------------------------------------------------------------
Sub insPreCA200()
	'--------------------------------------------------------------------------------------------
	Dim lblnfind As Boolean
	mobjblnCertif = True
	If (CStr(Session("ncertif")) > "0") Then
		mobjblnCertif = True
	Else
		mobjblnCertif = False
	End If
	
	lblnfind = mclsApvc.FindCA200(Session("scertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjblnCertif)
	' se incluye para la Opción de indemnización                                    
	Call mclsProduct.insInitialVI7001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nTransaction"))
	
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mclsApvc = New eApvc.Life_Apvc
mobjMenu = New eFunctions.Menues
mclsProduct = New eProduct.Product



mobjValues.ActionQuery = Session("bQuery")
mobjValues.sCodisplPage = "CA200"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft FrontPage 5.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%With Response
	Response.Write(mobjValues.StyleSheet())
	Response.Write(mobjValues.WindowsTitle("CA200"))
	Response.Write(mobjMenu.setZone(2, "CA200", "CA200.aspx"))
End With

Call insPreCA200()

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



insShowHeader();
    </SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP003" ACTION="valpolicyseqapvc.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <%=mobjValues.ShowWindowsName("CA200")%>
    <BR>
     <TABLE WIDTH="100%">
      <TR>
       <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD> 
      </TR>
      <TR>
     	  <TD COLSPAN="5" CLASS="Horline"></TD>
      </TR>
            <TD WIDTH = 35% ><LABEL ID=0><%= GetLocalResourceObject("tctnAmountprenCaption") %> </LABEL></TD>
            <TD WIDTH = 15% ><%=mobjValues.NumericControl("tctnAmountpren", 9, CStr(mclsApvc.nAmountnprem),  , GetLocalResourceObject("tctnAmountprenToolTip"),  ,  ,  ,  ,  ,  , mobjblnCertif)%></TD>
            <TD WIDTH = 60> </TD>
            <TD WIDTH = 35% > <LABEL ID=0><%= GetLocalResourceObject("cbenCurrencyemplCaption") %> </LABEL></TD>
            <TD WIDTH = 15% >
            <%mobjValues.List = "4,1"
mobjValues.TypeList = 1%>
            <%=mobjValues.PossiblesValues("cbenCurrencyempl", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsApvc.nCurrencyempl),  ,  ,  ,  ,  ,  , mobjblnCertif,  , GetLocalResourceObject("cbenCurrencyemplToolTip"))%></TD>
           <TR>
           <TD><LABEL ID=0><%= GetLocalResourceObject("tctnpercentsalaryCaption") %> </LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnpercentsalary", 10, CStr(mclsApvc.npercentsalary),  , GetLocalResourceObject("tctnpercentsalaryToolTip"),  , 2,  ,  ,  ,  , mobjblnCertif)%></TD>
            <TD WIDTH = 12> </TD>
             <TD><LABEL ID=0><%= GetLocalResourceObject("tctnMinstayCaption") %> </LABEL></TD>
             <TD><%=mobjValues.NumericControl("tctnMinstay", 9, CStr(mclsApvc.nMinstay),  , GetLocalResourceObject("tctnMinstayToolTip"),  ,  ,  ,  ,  ,  , mobjblnCertif)%></TD>
           </TR> 
       <TR>
        
              <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPercentnprentCaption") %>  </LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPercentnprent", 10, CStr(mclsApvc.nPercentnprem),  , GetLocalResourceObject("tctnPercentnprentToolTip"),  , 2,  ,  ,  ,  , mobjblnCertif)%></TD>
        
             <TD WIDTH = 12> </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPrem_minCaption") %> </LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPrem_min", 9, CStr(mclsApvc.nPrem_min),  , GetLocalResourceObject("tctnPrem_minToolTip"),  ,  ,  ,  ,  ,  , mobjblnCertif)%></TD>
         </TR> 
       <TR>
              <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPrem_maxCaption") %> </LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPrem_max", 9, CStr(mclsApvc.nPrem_max),  , GetLocalResourceObject("tctnPrem_maxToolTip"),  ,  ,  ,  ,  ,  , mobjblnCertif)%></TD>
       
             <%=mobjValues.HiddenControl("chkspremium", CStr(1))%>
             <TD> </TD>
             <TD><LABEL ID=0><%= GetLocalResourceObject("tctnstayCaption") %> </LABEL></TD>
             <TD><%=mobjValues.NumericControl("tctnstay", 9, CStr(mclsApvc.nstay),  , GetLocalResourceObject("tctnstayToolTip"),  ,  ,  ,  ,  ,  , mobjblnCertif)%></TD>
          
        </TR>
        <TR>
               <TD><LABEL ID=0><%= GetLocalResourceObject("cbeBankExtCaption") %> </LABEL></td>
               <TD><%
mobjValues.Parameters.Add("sClient", "0", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nWay_pay", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("cbeBankExt", "table7", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsApvc.nBankext), True,  ,  ,  ,  ,  , mobjblnCertif,  , GetLocalResourceObject("cbeBankExtToolTip"),  , 3))
%>
               </TD>
                <TD WIDTH = 12> </TD>
                <TD><LABEL ID=0><%= GetLocalResourceObject("tctsAccountCaption") %> </LABEL> </TD>
               <TD><%=mobjValues.TextControl("tctsAccount", 15, mclsApvc.sAccount,  , GetLocalResourceObject("tctsAccountToolTip"),  ,  ,  ,  , mobjblnCertif)%></TD> 
        </TR>
          <TR>
        <TR>
            <TD><LABEL ID=12945><%= GetLocalResourceObject("cbeTyp_AccountCaption") %></LABEL></TD>
              <%mobjValues.List = "1,2"
mobjValues.TypeList = 1%>
            <TD><%=mobjValues.PossiblesValues("cbeTyp_Account", "table190", eFunctions.Values.eValuesType.clngComboType, CStr(mclsApvc.nTyp_Acc),  ,  ,  ,  ,  ,  , mobjblnCertif,  , GetLocalResourceObject("cbeTyp_AccountToolTip"))%></TD>
			<TD>&nbsp;</TD>            
          <%If Not mobjblnCertif Then%> 
             <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %>  </LABEL></td>
             <TD> <LABEL ID=0><%	Response.Write(mclsApvc.CertificatQuantity(Session("scertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)))
	
Else
	%>
          <TD></TD>
          <TD></TD>     
         <%End If%>  
       
            
           </TR>
        
        
        <%If mobjblnCertif Then
	%>
        <TR>
     	    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD> 
       </TR>
          <TR>
     	  <TD COLSPAN="5" CLASS="Horline"></TD>
        </TR>
          <TR>
           <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPremiumcCaption") %><LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPremiumc", 9, CStr(mclsApvc.nPremiumc),  , GetLocalResourceObject("tctnPremiumcToolTip"))%></TD>
            <TD> </TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnPercentiumcCaption") %> </LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnPercentiumc", 4, CStr(mclsApvc.nPercentiumc),  , GetLocalResourceObject("tctnPercentiumcToolTip"),  , 2)%></TD>
        </TR>
          <TR>
      <TD WIDTH = 35% > <LABEL ID=0><%= GetLocalResourceObject("cbenCurrencyworkCaption") %>  </LABEL></TD>
            <TD WIDTH = 15% >
            <%	mobjValues.List = "4,1"
	mobjValues.TypeList = 1%>
            <%=mobjValues.PossiblesValues("cbenCurrencywork", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsApvc.nCurrencywork),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenCurrencyworkToolTip"))%></TD>
     
            <TD></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tctnamountsalaryCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tctnamountsalary", 9, CStr(mclsApvc.namountsalary),  , GetLocalResourceObject("tctnamountsalaryToolTip"))%></TD>
        </TR>
        
                  <TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("cbenTyp_profitworkerCaption") %> </LABEL></TD>
            <TD>
               <%	mobjValues.List = "1,2"
	mobjValues.TypeList = 1%>
            <%=mobjValues.PossiblesValues("cbenTyp_profitworker", "table950", eFunctions.Values.eValuesType.clngComboType, CStr(mclsApvc.nTyp_profitworker),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTyp_profitworkerToolTip"))%></TD>
            <TD></TD>
            <TD><LABEL><%= GetLocalResourceObject("valOptionCaption") %></LABEL></TD>
			<TD><%	With mobjValues.Parameters
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	Response.Write(mobjValues.PossiblesValues("valOption", "TAB_OPTION", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsProduct.nOption), True,  ,  ,  ,  ,  , mclsProduct.bOption,  , GetLocalResourceObject("valOptionToolTip")))%></TD>
        </TR>
    
              </TR>
      <%End If%>  
	</TABLE>
	
</FORM>
</BODY>
</HTML>
<%

mclsApvc = Nothing
%>




