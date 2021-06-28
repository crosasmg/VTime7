<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsProduct As eProduct.Product


</script>
<%Response.Expires = -1

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsProduct = New eProduct.Product
End With

mobjValues.ActionQuery = Session("bQuery")

Call mclsProduct.FindProduct_li(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

mobjValues.sCodisplPage = "dp024"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
	.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "DP024.aspx"))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//%insEnabled:Permite habilitar y des-habilitar los controles de la ventana
//--------------------------------------------------------------------------------------------
function insEnabled(Field){
//--------------------------------------------------------------------------------------------
    var lblnChecked
    lblnChecked=Field.checked
    with(document.forms[0]){
        switch(Field.name){
//+Pagos Periódicos
			case "chkPerPay":
				cbePerFreq.disabled=!lblnChecked;
				chkPerUni.disabled=!lblnChecked;
				cbePerFreq.disabled=!lblnChecked;
				chkPerUni.disabled=!lblnChecked;
				cbeRevalType.disabled=!lblnChecked;
				tcnPerMin.disabled=!lblnChecked;
				tcnPerMax.disabled=!lblnChecked;
				chkPerUni.checked=false;
				cbePerFreq.value=0;
				cbeRevalType.value=0;
				tcnRevalFact.value=VTFormat(0, "",  "",  "", 2);
				tcnPerMul.value=VTFormat(0, "",  "",  "", 2);
				tcnRevalFact.value=VTFormat(0, "",  "",  "", 2);
				tcnPerMin.value=VTFormat(0, "",  "",  "", 2);
				tcnPerMax.value=VTFormat(0, "",  "",  "", 2);
				//chkNoPerPay.disabled=lblnChecked;
//+ Se inhabilitan y desmarcan los campos correspondientes a PAGOS NO PERIÓDICOS
//+ ACM - 12/06/2001
				//chkNoPerUni.disabled = true;
				//chkNoPerUni.checked	 = false;
				//chkNoPerPay.checked	 = false;
				tcnNoPerMul.value = VTFormat(0, "",  "",  "", 2);
				tcnNoPerMul.disabled = true;
				tcnNoPerMin.value = VTFormat(0, "",  "",  "", 2);
				//tcnNoPerMin.disabled = true;
				tcnNoPerMax.value = VTFormat(0, "",  "",  "", 2);
				//tcnNoPerMax.disabled = true;
				break;
                       
//+Pagos no periódicos
			case "chkNoPerPay":
				chkNoPerUni.disabled=!lblnChecked;
				tcnNoPerMin.disabled=!lblnChecked;
				tcnNoPerMax.disabled=!lblnChecked;
				//tcnNoPerMul.disabled=true;
				chkNoPerUni.checked=false;
				tcnNoPerMin.value=VTFormat(0, "",  "",  "", 2);
				tcnNoPerMax.value=VTFormat(0, "",  "",  "", 2);
				tcnNoPerMul.value=VTFormat(0, "",  "",  "", 2);
				//chkPerPay.disabled=lblnChecked;
//+ Se inhabilitan y desmarcan los campos correspondientes a PAGOS PERIÓDICOS
//+ ACM - 12/06/2001
				//chkPerUni.disabled = true;
				//chkPerUni.checked	 = false;
				//chkPerPay.checked	 = false;
				tcnPerMul.value = VTFormat(0, "",  "",  "", 2);
				//tcnPerMul.disabled = true;
				tcnPerMin.value = VTFormat(0, "",  "",  "", 2);
				//tcnPerMin.disabled = true;
				tcnPerMax.value = VTFormat(0, "",  "",  "", 2);
				//tcnPerMax.disabled = true;
				
				//cbeRevalType.disabled = true;
				cbeRevalType.value = 0;
				//cbePerFreq.disabled = true;
				cbePerFreq.value = 0;
				tcnRevalFact.value = VTFormat(0, "",  "",  "", 2);
				//tcnRevalFact.disabled = true;

				break;
                         
//+ Tipo de revalorarización
			case "cbeRevalType":
				if(Field.value==3)
					tcnRevalFact.disabled=false
				else{
					tcnRevalFact.disabled=true;
					tcnRevalFact.value=VTFormat(0, "",  "",  "", 2);
				}
				break
                         
//+ Monto uniforme(pagos periódicos)
			case "chkPerUni":
				tcnPerMul.disabled=!lblnChecked;
				tcnPerMin.disabled=lblnChecked;
				tcnPerMax.disabled=lblnChecked;
				
				if(lblnChecked){
					tcnPerMin.value=VTFormat(0, "",  "",  "", 2);
					tcnPerMax.value=VTFormat(0, "",  "",  "", 2);
				}
				else
					tcnPerMul.value=VTFormat(0, "",  "",  "", 2);
					
				break;
                         
//+ Monto uniforme(pagos no periódicos)
			case "chkNoPerUni":
				tcnNoPerMin.disabled=lblnChecked;
				tcnNoPerMax.disabled=lblnChecked;
				tcnNoPerMul.disabled=!lblnChecked;
				if(lblnChecked){
					tcnNoPerMin.value=VTFormat(0, "",  "",  "", 2);
					tcnNoPerMax.value=VTFormat(0, "",  "",  "", 2);
				}
				else
					tcnNoPerMul.value=VTFormat(0, "",  "",  "", 2);
		}
    }
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP043A" ACTION="valProdLifeSeq.aspx?sMode=1">
    <TABLE WIDTH="100%">
		<TR>
            <TD><LABEL ID=14897><%= GetLocalResourceObject("tcnInitialPayCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnInitialPay", 18, CStr(mclsProduct.nPayiniti),  , GetLocalResourceObject("tcnInitialPayToolTip"), True, 6,  ,  ,  ,  ,  , 1)%></TD>
            <TD WIDTH="10%">&nbsp</TD>
            <TD><LABEL ID=14899><%= GetLocalResourceObject("tcnMaxAnnualCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnMaxAnnual", 18, CStr(mclsProduct.nAnnualap),  , GetLocalResourceObject("tcnMaxAnnualToolTip"), True, 6,  ,  ,  ,  ,  , 2)%></TD>
		</TR>
	</TABLE>
	<TABLE WIDTH="100%">
        <TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=100398><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>			
			<TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>
		<TR>                       
			<TD COLSPAN="3"><%=mobjValues.CheckControl("chkPerPay", GetLocalResourceObject("chkPerPayCaption"), mclsProduct.DefaultValueDP024("chkPerPay"),  , "insEnabled(this)",  , 3)%></TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=100399><A NAME="Revalorización"><%= GetLocalResourceObject("AnchorRevalorizaciónCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="3"></TD>
		    <TD COLSPAN="2" CLASS="Horline"></TD>
        </TR>
        <TR>
			<TD WIDTH="20%"><LABEL ID=19398><%= GetLocalResourceObject("cbePerFreqCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbePerFreq", "Table36", 1, CStr(mclsProduct.nDedufreq),  ,  ,  ,  ,  ,  , mclsProduct.DefaultValueDP024("cbePerFreq_disabled"),  , GetLocalResourceObject("cbePerFreqToolTip"),  , 4)%></TD>
			<TD WIDTH="10%">&nbsp</TD>
			<TD><LABEL ID=19397><%= GetLocalResourceObject("cbeRevalTypeCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeRevalType", "Table46", 1, mclsProduct.sRevaltyp,  ,  ,  ,  ,  , "insEnabled(this)", mclsProduct.DefaultValueDP024("cbeRevalType_disabled"),  , GetLocalResourceObject("cbeRevalTypeToolTip"),  , 7)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=14882><%= GetLocalResourceObject("tcnPerMinCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPerMin", 18, mclsProduct.DefaultValueDP024("tcnPerMin"),  , GetLocalResourceObject("tcnPerMinToolTip"), True, 6,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnPerMin_disabled"), 5)%></TD>
			<TD>&nbsp</TD>			
			<TD><LABEL ID=14908><%= GetLocalResourceObject("tcnRevalFactCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnRevalFact", 4, mclsProduct.DefaultValueDP024("tcnRevalFact"),  , GetLocalResourceObject("tcnRevalFactToolTip"), True, 2,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnRevalFact_disabled"), 8)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=14905><%= GetLocalResourceObject("tcnPerMaxCaption") %></LABEL></TD>
			<TD COLSPAN="4"><%=mobjValues.NumericControl("tcnPerMax", 18, mclsProduct.DefaultValueDP024("tcnPerMax"),  , GetLocalResourceObject("tcnPerMaxToolTip"), True, 6,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnPerMax_disabled"), 6)%></TD>
		</TR>
		<TR>			
			<TD COLSPAN="3"><%=mobjValues.CheckControl("chkPerUni", GetLocalResourceObject("chkPerUniCaption"), mclsProduct.sPerunifa, "1", "insEnabled(this)", mclsProduct.DefaultValueDP024("chkPerUni_disabled"), 9)%></TD>
			<TD><LABEL ID=14907><%= GetLocalResourceObject("tcnPerMulCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPerMul", 18, mclsProduct.DefaultValueDP024("tcnPerMul"),  , GetLocalResourceObject("tcnPerMulToolTip"), True, 6,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnPerMul_disabled"), 10)%></TD>
        </TR>
		<TR>
			<TD COLSPAN="5">&nbsp</TD>
		</TR>		
		<TR>
			<TD COLSPAN="5"><%=mobjValues.CheckControl("chkNoPerPay", GetLocalResourceObject("chkNoPerPayCaption"), mclsProduct.DefaultValueDP024("chkNoPerPay"),  , "insEnabled(this)", mclsProduct.DefaultValueDP024("chkNoPerPay_disabled"), 11)%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=14882><%= GetLocalResourceObject("tcnPerMinCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnNoPerMin", 18, mclsProduct.DefaultValueDP024("tcnNoPerMin"),  , GetLocalResourceObject("tcnNoPerMinToolTip"), True, 6,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnNoPerMin_disabled"), 12)%></TD>
            <TD>&nbsp</TD>
            <TD><LABEL ID=14905><%= GetLocalResourceObject("tcnPerMaxCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnNoPerMax", 18, mclsProduct.DefaultValueDP024("tcnNoPerMax"),  , GetLocalResourceObject("tcnNoPerMaxToolTip"), True, 6,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnNoPerMax_disabled"), 13)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"><%=mobjValues.CheckControl("chkNoPerUni", GetLocalResourceObject("chkNoPerUniCaption"), mclsProduct.sNpeunifa,  , "insEnabled(this)", mclsProduct.DefaultValueDP024("chkNoPerUni_disabled"), 14)%></TD>
            <TD>&nbsp</TD>
			<TD><LABEL ID=14902><%= GetLocalResourceObject("tcnNoPerMulCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnNoPerMul", 18, mclsProduct.DefaultValueDP024("tcnNoPerMul"),  , GetLocalResourceObject("tcnNoPerMulToolTip"), True, 6,  ,  ,  ,  , mclsProduct.DefaultValueDP024("tcnNoPerMul_disabled"), 15)%></TD>
		</TR>
    </TABLE>    
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mclsProduct = Nothing
%>







