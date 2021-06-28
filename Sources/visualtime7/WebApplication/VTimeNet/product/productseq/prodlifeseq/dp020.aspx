<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable para la carga de datos en la forma 
Dim mclsProduct_li As eProduct.Product


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsProduct_li = New eProduct.Product

mobjValues.ActionQuery = Session("bQuery")

Call mclsProduct_li.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

mobjValues.sCodisplPage = "dp020"
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">    

<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:07 $|$$Author: Nvaplat61 $"
	
//% EnabledFields: Habilita los campos sólo si el "%Mínimo garantizado" tiene valor
//--------------------------------------------------------------------------------
function EnabledFields(sOption,Field,sBeneRes,nAction){
//--------------------------------------------------------------------------------
	var nAction
	nAction = <%=Request.QueryString.Item("nMainAction")%>;

	with(self.document.forms[0].elements){
		switch(sOption){
			case "tcn":
				if(Field.value!=0 && Field.value!=''){
					cbeBenefApl.disabled=false;
					tcnBenefexc.disabled=false;
					tcnInterest.disabled=false;
					OptResBenef[0].checked = true;
					OptResBenef[0].disabled = false;
					OptResBenef[1].disabled = false;
					OptResBenef[2].disabled = false;
					OptResBenef[3].disabled = false;
					OptResBenef[4].disabled = false;			
				}
				else{
					cbeBenefApl.disabled=true;
					tcnBenefexc.disabled=true;
					tcnInterest.disabled=true;
					OptResBenef[0].checked = false;
					OptResBenef[0].disabled = true;
					OptResBenef[1].disabled = true;
					OptResBenef[2].disabled = true;
					OptResBenef[3].disabled = true;
					OptResBenef[4].disabled = true;
					cbeBenefApl.value='';
					tcnBenefexc.value='';
					tcnInterest.value='';
				}
				break;
				
			case "opt":
				switch(sBeneRes){
					case 10:
						OptResBenef[0].checked = true;
						break;
					case 20:
						OptResBenef[1].checked = true;
						break;
					case 30:
						OptResBenef[2].checked = true;
						break;
					case 40:
						OptResBenef[3].checked = true;
						break;
					case 50:
						OptResBenef[4].checked = true;
						break;
					default:
						OptResBenef[0].checked = true;
				}					
				if(nAction!=401)
					if(Field.value<=0){
						cbeBenefApl.disabled=false;
						tcnBenefexc.disabled=false;
						tcnInterest.disabled=false;
						OptResBenef[0].disabled = false;
						OptResBenef[1].disabled = false;
						OptResBenef[2].disabled = false;
						OptResBenef[3].disabled = false;
						OptResBenef[4].disabled = false;			
					}			
				break;
		}	
	}
}
</SCRIPT>
<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "DP020", "DP020.aspx"))
	.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP020" ACTION="valProdLifeSeq.aspx?sMode=1">
	<%=mobjValues.ShowWindowsName("DP020")%>
	<TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID=14890><%= GetLocalResourceObject("tcnBenefiltrCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnBenefiltr", 4, CStr(mclsProduct_li.nBenefitr),  , GetLocalResourceObject("tcnBenefiltrToolTip"),  , 2,  ,  ,  , "EnabledFields(""tcn"",this)",  , 1)%></TD>
			<TD WIDTH="15%">&nbsp;</TD>
			<TD><LABEL ID=14888><%= GetLocalResourceObject("cbeBenefAplCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBenefApl", "Table113", 1, CStr(mclsProduct_li.nBenefapl),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBenefAplToolTip"),  , 2)%></TD>
		</TR>			
	</TABLE>
	<BR>
	<TABLE WIDTH="100%">
		<TR>                       
			<TD WIDTH="45%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=100174><A NAME="Beneficio"><%= GetLocalResourceObject("AnchorBeneficioCaption") %></A></LABEL></TD>
            <TD WIDTH="10%">&nbsp;</TD>            
            <TD CLASS="HighLighted"><LABEL ID=100175><A NAME="Resultado"><%= GetLocalResourceObject("AnchorResultadoCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>		    
		    <TD WIDTH="10%"></TD>
		    <TD CLASS="HorLine"></TD>
        </TR>      		
		<TR>
			<TD><LABEL ID=14889><%= GetLocalResourceObject("tcnBenefexcCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnBenefexc", 18, CStr(mclsProduct_li.nBenefexc),  , GetLocalResourceObject("tcnBenefexcToolTip"), True, 6,  ,  ,  ,  , True, 3)%></TD>
			<TD WIDTH="10%">&nbsp;</TD>			
			<TD><%=mobjValues.OptionControl(100176, "OptResBenef", GetLocalResourceObject("OptResBenef_1Caption"),  , "1",  , True, 5)%></TD>

		</TR>
		<TR>			
			<TD><LABEL ID=14891><%= GetLocalResourceObject("tcnInterestCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnInterest", 4, CStr(mclsProduct_li.nBenexcra),  , GetLocalResourceObject("tcnInterestToolTip"),  , 2,  ,  ,  ,  , True, 4)%></TD>
			<TD WIDTH="10%">&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100177, "OptResBenef", GetLocalResourceObject("OptResBenef_2Caption"),  , "2",  , True, 6)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100178, "OptResBenef", GetLocalResourceObject("OptResBenef_3Caption"),  , "3",  , True, 7)%></TD>
		</TR>
		<TR>	
			<TD COLSPAN="3">&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100179, "OptResBenef", GetLocalResourceObject("OptResBenef_4Caption"),  , "4",  , True, 8)%></TD>
		</TR>
		<TR>
			<TD COLSPAN="3">&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(100180, "OptResBenef", GetLocalResourceObject("OptResBenef_5Caption"),  , "5",  , True, 9)%></TD>
		</TR>				
	</TABLE>    
</FORM>
</BODY>
</HTML>
<SCRIPT>
	EnabledFields('opt',<%=mclsProduct_li.nBenefitr%>,<%=mclsProduct_li.sBenRes%>0)
</SCRIPT>




