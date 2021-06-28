<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Sub insInitialVar()
	With Response
		.Write("<SCRIPT>")
		.Write("var mstrReceiptNum = " & Session("sReceiptNum") & ";")
		.Write("</" & "Script>")
	End With
End Sub

</script>
<%Response.Expires = 0
With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
End With
Call insInitialVar()
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>

<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("COC003", "COC003_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
</SCRIPT>
<SCRIPT>
//% InsChangeField: se controla los parámetros del campo ramo/producto.
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
    if(mstrReceiptNum=="2" || mstrReceiptNum=="3"){
		with (self.document.forms[0]){
			switch (sField){
				case 'Branch':
					valProduct.Parameters.Param1.sValue=sValue;
					valProduct.disabled = (sValue == '0');
					btnvalProduct.disabled = valProduct.disabled;
					valProduct.value = '';
			        UpdateDiv('valProductDesc','');
			        if(mstrReceiptNum=="2"){
					    valProduct.disabled = true;				    
					    btnvalProduct.disabled = true;
					    insDefValues("Receipt_Branch", "nReceipt=" + tcnReceipt.value + "&nBranch=" + cbeBranch.value)			            
			        }
					break;			
			}		
		}
	}	
}
//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues(Field){
//-------------------------------------------------------------------------------------------
    with (document.forms[0]){           
        if(mstrReceiptNum=="1"){
            if (Field.value != 0 && Field.value != ""){            
			    insDefValues("Receipt", "nReceipt=" + tcnReceipt.value)
			}        
			else{
				insDefValues("Blank_COC003", "")
			}    
		}	
		if(mstrReceiptNum=="2"){
		    if (Field.value != 0 && Field.value != ""){
				insDefValues("Receipt_Branch", "nReceipt=" + tcnReceipt.value + "&nBranch=" + cbeBranch.value)

			}   
		}
	}
}
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    with(self.document.forms[0]){	
        tcnReceipt.disabled = false;	
        cbeBranch.disabled = true;	
        valProduct.disabled = true;	
		if (mstrReceiptNum=="2")
		    cbeBranch.disabled = false;
		if (mstrReceiptNum=="3"){
		    cbeBranch.disabled = false;
		    valProduct.disabled = false;
		}
	}
	if (mstrReceiptNum=="3")
	    document.images["btnvalProduct"].disabled=false;    
	else    
	    document.images["btnvalProduct"].disabled=true;    
}
//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	insReloadTop(false);
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmPremiumQue" ACTION="valCollectionQue.aspx?mode=1">
<BR><BR>
		<%=mobjValues.FIELDSET(999, "Datos del recibo en consulta")%>
		<TABLE WIDTH=100%>
			<TR>
			    <TD WIDTH="8%"><LABEL ID=10521><%= GetLocalResourceObject("tcnReceiptCaption") %></LABEL></TD>
				<TD WIDTH="12%"><%=mobjValues.NumericControl("tcnReceipt", 10, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnReceiptToolTip"),  ,  ,  ,  ,  , "ShowDefValues(this)", True)%> </TD>
			    <TD WIDTH="8%"><LABEL ID=10521><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
				<TD WIDTH="27%"><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)", True,  , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
				<TD WIDTH="8%"><LABEL ID=13771><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>            
				<TD WIDTH="37%"><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valProductToolTip")))
End With%>
				</TD>			
		</TABLE>
		<%=mobjValues.closeFIELDSET()%>
		
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing%>





