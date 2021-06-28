<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim ldblFinalAmount As Object

'- Objeto para la carga de los valores en la forma
Dim mclsLoans As ePolicy.Loans

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

Dim mblnQuery As Boolean
'- Variable para el porcentaje de impuesto	
Dim mdblTax As Object

'- Variables para el manejo de el factor de cambio
Dim mclsExchange As Object
Dim mdblExchange As Object

Dim mstrCheckExecute As String

Dim mobjValPolicyTra As ePolicy.ValPolicyTra
Dim mobjCertificat As ePolicy.Certificat
Dim mobjProduct_li As eProduct.Product

Dim mdblSurrValLoc As Object

'- Variable que controla las posiciones decimales
Dim llngDecimal As Short

Dim lstrClient As String
Dim ldblLoans As String
Dim ldblSurrVal As String
Dim ldblInterest As String
Dim ldblInter_Year As String

'% insPreVI011: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI011()
	'--------------------------------------------------------------------------------------------
	mobjValPolicyTra = New ePolicy.ValPolicyTra
	mobjCertificat = New ePolicy.Certificat
	mobjProduct_li = New eProduct.Product
	
	With Request
		Call mobjProduct_li.FindProduct_li(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		
		Call mclsLoans.Find(mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nValCode"), eFunctions.Values.eTypeData.etdDouble), True)
		
		mdblSurrValLoc = mobjCertificat.insGetSurrenAmount("2", mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "VI011", mobjProduct_li.nCurrency, mobjProduct_li.nProdClas)
		
		mobjValPolicyTra = mobjCertificat.mclsValPolicyTra
		
	End With
	
	mblnQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
	If mblnQuery Then
		mstrCheckExecute = "2"
	Else
		mstrCheckExecute = Request.QueryString.Item("nExecute")
	End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "vi011"
mclsLoans = New ePolicy.Loans
mobjMenu = New eFunctions.Menues

Call insPreVI011()
Session("OP006_sCodispl") = vbNullString
%>





    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT> 
	var mlblValue = 0;
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 17/02/06 18:38 $|$$Author: Clobos $"

//% ShowData_loans: Obtiene datos del producto
//------------------------------------------------------------------------------------------- 
function ShowData_loans(){ 
//------------------------------------------------------------------------------------------- 

	with(self.document.forms[0]){
	    lstrParams = "nBranch=" + cbeBranch.value +
	     			 "&nProduct=" + valProduct.value +
	    			 "&dEffecdate=" + tcdEffecdate.value 
	}
	insDefValues("Data_loans", lstrParams,"/VTimeNet/Policy/PolicyTra");
}
    
//% ShowChangeAmount: actualiza el valor del monto en moneda local y el impuesto
//-------------------------------------------------------------------------------------------
function ShowChangeAmount(){
//-------------------------------------------------------------------------------------------
	if (top.fraSequence.plngMainAction != 401)
		with(self.document.forms[0]){
			if (tcnAmount.value!=0){
				tcnAmountLocal.value = VTFormat(insConvertNumber(hddnExchange.value)* insConvertNumber(tcnAmount.value), '', '', '', 6, true);
				tcnAmount.value = VTFormat(tcnAmount.value, '', '', '', 6);
				tcnAmoTax.value = VTFormat(Math.round(insConvertNumber(tcnAmount.value) * insConvertNumber(hddnExchange.value) * (insConvertNumber(hddTaxes.value) / 100)), '', '', '', 6, true);			
				hddFinalOri.value = VTFormat(insConvertNumber(tcnAmount.value) - (insConvertNumber(tcnAmount.value) * insConvertNumber(hddTaxes.value) / 100), '', '', '', 6, true);
		        hddFinal.value = VTFormat(Math.round(insConvertNumber(tcnAmountLocal.value) - insConvertNumber(tcnAmoTax.value)), '', '', '', 6, true);
			}			
			else{
				tcnAmountLocal.value = VTFormat(0, '', '', '', 6);			
				tcnAmoTax.value = VTFormat(0, '', '', '', 6);
				hddFinalOri.value = VTFormat(0, '', '', '', 6);
				hddFinal.value = VTFormat(0, '', '', '', 6);           			
			}
		}
}    
</SCRIPT> 
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "VI011", "VI011.aspx"))
	.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
If CDbl(Request.QueryString.Item("nCurrency")) = 1 Then
	llngDecimal = 0
Else
	llngDecimal = 6
End If

If mobjProduct_li.nProdClas = 3 Or mobjProduct_li.nProdClas = 4 Then
	lstrClient = mobjValPolicyTra.sClient
	ldblLoans = mobjValPolicyTra.DefaultValueVI7000("tcnLoans")
	ldblInterest = mobjValPolicyTra.DefaultValueVI7000("tcnInterest")
	ldblSurrVal = mobjValPolicyTra.DefaultValueVI7000("tcnSurrVal")
	ldblInter_Year = mobjValPolicyTra.DefaultValueVI7000("tcnInter_year")
Else
	lstrClient = mobjValPolicyTra.sClient
	ldblLoans = mobjValPolicyTra.DefaultValueVI009("nLoans")
	ldblInterest = mobjValPolicyTra.DefaultValueVI009("tcnInterest")
	ldblSurrVal = mobjValPolicyTra.DefaultValueVI009("tcnSurrVal")
	mdblSurrValLoc = mobjValPolicyTra.DefaultValueVI009("tcnSurrvalue_loc")
	ldblInter_Year = mobjValPolicyTra.DefaultValueVI009("tcnInter_year")
End If
%>    
  
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VI011" ACTION=valPolicyTra.aspx?sTime=1>    
    	<%=mobjValues.ShowWindowsName("VI011", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
	        <TD COLSPAN="4"><%=mobjValues.ClientControl("tctClient", lstrClient,  , GetLocalResourceObject("tctClientToolTip"),  , True)%></TD>
        </TR>
        <TR> 
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnLoansCaption") %></LABEL></TD> 
  	        <TD><%Response.Write(mobjValues.NumericControl("tcnLoans", 18, ldblLoans,  , GetLocalResourceObject("tcnLoansToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD> 
            <TD><%Response.Write(mobjValues.NumericControl("tcnInterest", 18, ldblInterest,  , GetLocalResourceObject("tcnInterestToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
        </TR> 
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnSurrValCaption") %></LABEL></TD> 
            <TD><%=mobjValues.NumericControl("tcnSurrVal", 18, ldblSurrVal,  , GetLocalResourceObject("tcnSurrValToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnSurrvalue_locCaption") %></LABEL></TD> 
            <TD><%=mobjValues.NumericControl("tcnSurrvalue_loc", 18, mdblSurrValLoc,  , GetLocalResourceObject("tcnSurrvalue_locToolTip"), True, 6,  ,  ,  ,  , True)%></TD>
        </TR> 
		<TR> 
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnMaxAmountCaption") %></LABEL></TD> 
            <TD><%Response.Write(mobjValues.NumericControl("tcnMaxAmount", 18, mobjValPolicyTra.DefaultValueVI009("tcnMaxAmount"),  , GetLocalResourceObject("tcnMaxAmountToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnMaxAmountLocalCaption") %></LABEL></TD> 
            <TD><%Response.Write(mobjValues.NumericControl("tcnMaxAmountLocal", 18, mobjValPolicyTra.DefaultValueVI009("tcnMaxAmountLocal"),  , GetLocalResourceObject("tcnMaxAmountLocalToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
        </TR>
        <TR>
            <TD COLSPAN="5">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100759><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD>&nbsp;</TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100759><A NAME="Orden de pago"><%= GetLocalResourceObject("AnchorOrden de pagoCaption") %></A></LABEL></TD>
        </TR>
        <TR>
			<TD COLSPAN="2" CLASS="HorLine"></TD>
			<TD></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>   
			<TD><LABEL ID=13715><%= GetLocalResourceObject("tcnAmountCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmount", 18, mclsLoans.DefaultValueVI011("tcnAmount"),  , GetLocalResourceObject("tcnAmountToolTip"), True, llngDecimal, mblnQuery,  ,  , "ShowChangeAmount();", mblnQuery)%></TD> 
            <TD>&nbsp;</TD>
            <TD><LABEL ID=13723><%= GetLocalResourceObject("cbePayOrderCaption") %></LABEL></TD>
            <TD><% 
                    mobjValues.TypeList = 2
                    mobjValues.List = "1,3,4,5,6"
                    Response.Write(mobjValues.PossiblesValues("cbePayOrder", "Table193", eFunctions.Values.eValuesType.clngComboType, mclsLoans.DefaultValueVI011("cbePayOrder"), , mblnQuery, , , , , , , GetLocalResourceObject("cbePayOrderToolTip")))
             %></TD>
        </TR>      
        <TR>
			<TD><LABEL ID=13715><%= GetLocalResourceObject("tcnAmountLocalCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnAmountLocal", 18, CStr(mclsLoans.nAmountLoc),  , GetLocalResourceObject("tcnAmountLocalToolTip"), True, llngDecimal, mblnQuery,  ,  ,  , True)%></TD> 
            <TD>&nbsp;</TD>
            <TD><LABEL ID=100759><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnNumber", 5, CStr(mclsLoans.nRequest_nu),  , GetLocalResourceObject("tcnNumberToolTip"),  , 0, True,  ,  ,  , mblnQuery)%></TD>            
        </TR>      
        <TR>
   			<TD><LABEL ID=13715><%= GetLocalResourceObject("tcnAmoTaxCaption") %></LABEL></TD>
            <TD COLSPAN="4"><%=mobjValues.NumericControl("tcnAmoTax", 18, CStr(mclsLoans.nAmotax),  , GetLocalResourceObject("tcnAmoTaxToolTip"), True, 6, mblnQuery,  ,  ,  , True)%></TD> 
        </TR>      
        <TR>
   			<TD><LABEL ID=13715><%= GetLocalResourceObject("tcnInter_yearCaption") %></LABEL></TD>   			
            <TD><%Response.Write(mobjValues.NumericControl("tcnInter_year", 18, ldblInter_Year,  , GetLocalResourceObject("tcnInter_yearToolTip"), True, 6,  ,  ,  ,  , True))%></TD>
	        <TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkRequest", GetLocalResourceObject("chkRequestCaption"), mstrCheckExecute, CStr(1),  , CDbl(Request.QueryString.Item("nExecute")) <> 1 Or mblnQuery,  , GetLocalResourceObject("chkRequestToolTip"))%></TD>
        </TR>
        <%If mclsLoans.nAmotax <> 0 Then
	ldblFinalAmount = mclsLoans.nAmountLoc - mclsLoans.nAmotax
Else
	ldblFinalAmount = mclsLoans.nAmountLoc
End If
Response.Write(mobjValues.HiddenControl("hddFinal", ldblFinalAmount))%>
    </TABLE>
<%
Response.Write(mobjValues.BeginPageButton)
With Request
	Response.Write(mobjValues.HiddenControl("tcnCurrency", .QueryString.Item("nCurrency"))) ' actualizado con DefValues
	If .QueryString.Item("sCodisplOri") = vbNullString Then
		Response.Write(mobjValues.HiddenControl("tctCodisplOri", "VI011"))
	Else
		Response.Write(mobjValues.HiddenControl("tctCodisplOri", .QueryString.Item("sCodisplOri")))
	End If
	Response.Write(mobjValues.HiddenControl("hddFinalOri", CStr(0)))
	Response.Write(mobjValues.HiddenControl("tcnNoteNum", .QueryString.Item("nNoteNum")))
	Response.Write(mobjValues.HiddenControl("tcnOperat", .QueryString.Item("nOperat")))
	Response.Write(mobjValues.HiddenControl("tctDescript", .QueryString.Item("sDescript")))
	Response.Write(mobjValues.HiddenControl("optExecute", .QueryString.Item("nExecute")))
	Response.Write(mobjValues.HiddenControl("hddClient", mclsLoans.DefaultValueVI011("tcnClient")))
	Response.Write(mobjValues.HiddenControl("tctCertype", .QueryString.Item("sCertype")))
	Response.Write(mobjValues.HiddenControl("cbeBranch", .QueryString.Item("nBranch")))
	Response.Write(mobjValues.HiddenControl("valProduct", .QueryString.Item("nProduct")))
	Response.Write(mobjValues.HiddenControl("tcnPolicy", .QueryString.Item("nPolicy")))
	Response.Write(mobjValues.HiddenControl("tcnCertif", .QueryString.Item("nCertif")))
	Response.Write(mobjValues.HiddenControl("tcdEffecdate", .QueryString.Item("dEffecdate")))
	Response.Write(mobjValues.HiddenControl("valCode", .QueryString.Item("nValCode")))
	Response.Write(mobjValues.HiddenControl("cbeOffice", .QueryString.Item("nOffice")))
	Response.Write(mobjValues.HiddenControl("cbeAgency", .QueryString.Item("nAgency")))
	Response.Write(mobjValues.HiddenControl("cbeOfficeAgen", .QueryString.Item("nOfficeAgen")))
	Response.Write(mobjValues.HiddenControl("hddnExchange", mobjValPolicyTra.DefaultValueVI009("tcnExchange")))
	Response.Write(mobjValues.HiddenControl("hddTaxes", ""))
	
	If .QueryString.Item("sCodisplOri") <> vbNullString Then
		Response.Write("<SCRIPT>")
		Response.Write("self.document.forms[0].tcnAmount.value='" & .QueryString.Item("nAmount") & "';")
		Response.Write("setTimeout('$(self.document.forms[0].tcnAmount).change()', 500);")
		Response.Write("</SCRIPT>")
	End If
	Response.Write("<SCRIPT>")
	Response.Write("ShowData_loans();")
	Response.Write("</SCRIPT>")
End With

mobjValPolicyTra = Nothing
mobjProduct_li = Nothing
mobjCertificat = Nothing
mclsLoans = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>    





