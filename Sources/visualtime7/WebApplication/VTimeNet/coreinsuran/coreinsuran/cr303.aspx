<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable modular utilizada para la carga y actualización de datos de la forma
Dim mclsContrproc As eCoReinsuran.Contrproc


'% insPreCR303: Realiza la lectura para la carga de los datos de la forma
'------------------------------------------------------------------------------------------------
Private Sub insPreCR303()
	'------------------------------------------------------------------------------------------------	
	Call mclsContrproc.Find(Session("nNumber"), Session("nType"), Session("nBranch"), Session("dEffecdate"), True)
End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsContrproc = New eCoReinsuran.Contrproc

mobjValues.ActionQuery = Session("bQuery")
Call insPreCR303()

mobjValues.sCodisplPage = "cr303"
%>
<SCRIPT>
// EnabledFields: Habilita los campos de acuerdo a la Opción y el valor de los campos
//-----------------------------------------------------------------------------------
function EnabledFields(sOption,sField,nAction){
//-----------------------------------------------------------------------------------
	if(nAction!=401)
	{
		switch(sOption)
		{
			case "First":
			{
				if((nAction==302 || nAction==301) && sField!=-32768)
				{				
					self.document.forms[0].tcnYear_begin.disabled=false;	
					self.document.forms[0].tcnGroup_bene.disabled=false;
					self.document.forms[0].tcnYear_end.disabled=false;	
					self.document.forms[0].tcnExpenses.disabled=false;			
				}	
				if(sField==0)
				{
					self.document.forms[0].tcnYear_begin.disabled=true;	
					self.document.forms[0].tcnGroup_bene.disabled=true;
					self.document.forms[0].tcnYear_end.disabled=true;	
					self.document.forms[0].tcnExpenses.disabled=true;			
				}		
				break;
			}
			case "Second":
			{			   
				if(sField.value!=0)
				{				
					self.document.forms[0].tcnYear_begin.disabled=false;	
					self.document.forms[0].tcnGroup_bene.disabled=false;
					self.document.forms[0].tcnYear_end.disabled=false;	
					self.document.forms[0].tcnExpenses.disabled=false;			
				}	
				else if(sField.value==0)
				{
					self.document.forms[0].tcnYear_begin.disabled=true;	
					self.document.forms[0].tcnGroup_bene.disabled=true;
					self.document.forms[0].tcnYear_end.disabled=true;	
					self.document.forms[0].tcnExpenses.disabled=true;			
					self.document.forms[0].tcnYear_begin.value='';
					self.document.forms[0].tcnGroup_bene.value='';
					self.document.forms[0].tcnYear_end.value='';
					self.document.forms[0].tcnExpenses.value='';					
				}		
				break;			
			
			}
				
		}
	}
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CR303", "CR303.aspx"))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<FORM METHOD="post" ID="FORM" NAME="frmCR303" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <P ALIGN="Center">    
    <LABEL ID=100619><A><%= GetLocalResourceObject("AnchorCaption") %></A></LABEL></A></LABEL><LABEL ID=0> | </LABEL>
    <LABEL ID=100619><A HREF="#Traspaso"><%= GetLocalResourceObject("AnchorTraspasoCaption") %></A></LABEL>    
    </P>

<%=mobjValues.ShowWindowsName("CR303")%>    

    <TABLE WIDTH="100%">
		<TR>	
			<TD>&nbsp;</TD>  
		</TR>   
		<TR>                       
			<TD COLSPAN="6" CLASS="HighLighted"><LABEL ID=100620><A NAME="Participación"><%= GetLocalResourceObject("AnchorParticipaciónCaption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="6"><HR></TD>		    
        </TR>      								            		 	            
        <TR>            
            <TD><LABEL ID=100621><%= GetLocalResourceObject("tcnProfit_shCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnProfit_sh", 4, CStr(mclsContrproc.nProfit_sh),  , GetLocalResourceObject("tcnProfit_shToolTip"), True, 2,  ,  ,  , "EnabledFields(""Second"",this)")%></TD>
            <TD><LABEL ID=100622><%= GetLocalResourceObject("tcnYear_beginCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear_begin", 4, CStr(mclsContrproc.nYear_begin),  , GetLocalResourceObject("tcnYear_beginToolTip"),  , 0,  ,  ,  ,  , True)%></TD>            
            <TD><LABEL ID=100623><%= GetLocalResourceObject("tcnGroup_beneCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnGroup_bene", 3, CStr(mclsContrproc.nGroup_bene),  , GetLocalResourceObject("tcnGroup_beneToolTip"),  , 0,  ,  ,  ,  , True)%></TD>            
        </TR>     
        <TR>            
            <TD><LABEL ID=100624><%= GetLocalResourceObject("tcnYear_endCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear_end", 4, CStr(mclsContrproc.nYear_end),  , GetLocalResourceObject("tcnYear_endToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=100625><%= GetLocalResourceObject("tcnExpensesCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExpenses", 4, CStr(mclsContrproc.nExpenses),  , GetLocalResourceObject("tcnExpensesToolTip"), True, 2,  ,  ,  ,  , True)%></TD>
        </TR>
	</TABLE>        
	<TABLE WIDTH="100%">
		<TR>	
			<TD>&nbsp;</TD>  
		</TR>    	
		<TR>                       
			<TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=100626><A NAME="Traspaso"><%= GetLocalResourceObject("AnchorTraspaso2Caption") %></A></LABEL></TD>
        </TR>        
        <TR>
		    <TD COLSPAN="4"><HR></TD>		    
        </TR>	
        <TR>
            <TD><LABEL ID=100627><%= GetLocalResourceObject("tcnTran_premCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnTran_prem", 4, CStr(mclsContrproc.nTran_prem),  , GetLocalResourceObject("tcnTran_premToolTip"), True, 2)%></TD>
            <TD><LABEL ID=100628><%= GetLocalResourceObject("tcnRate_claimCaption") %></LABEL></TD>            
            <TD><%=mobjValues.NumericControl("tcnRate_claim", 4, CStr(mclsContrproc.nRate_claim),  , GetLocalResourceObject("tcnRate_claimToolTip"), True, 2)%></TD>
            <TD WIDTH="5%">&nbsp;</TD>
            <TD><LABEL ID=100629><%= GetLocalResourceObject("tcnExcessCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExcess", 18, CStr(mclsContrproc.nExcess),  , GetLocalResourceObject("tcnExcessToolTip"), True, 6)%></TD>            
        </TR>
    </TABLE>
    <%Response.Write(mobjValues.BeginPageButton)%>
</FORM>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"     
</SCRIPT>
</BODY>
</HTML>
<SCRIPT>
EnabledFields('First',<%=mclsContrproc.nProfit_sh%>,<%=Request.QueryString.Item("nMainAction")%>)
</SCRIPT>




