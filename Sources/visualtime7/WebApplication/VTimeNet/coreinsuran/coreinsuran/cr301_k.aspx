<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "cr301_K"
%>
<SCRIPT>
//- Variable que indica la acción que seleccionó el usuario

	var mstrAction 
	
	<%="mstrAction='" & Session("nMainAction") & "'"%>
//% insCancel: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------	
	if (top.frames["fraSequence"].pintZone==2 && (mstrAction==301)){	
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=CR301_k","EndProcess",300,180)
	}
	else {
	    return true;
	}
}
//% insFinish: Ejecuta la acción de Finalizar de la página.
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
    var lintIndex;
    var error;

    try {
		for(lintIndex=0;lintIndex < self.document.forms[0].elements.length;lintIndex++){
			self.document.forms[0].elements[lintIndex].disabled=false;
			if(self.document.images.length>0)
			    if(typeof(self.document.images["btn" + self.document.forms[0].elements[lintIndex].name])!='undefined')
			       self.document.images["btn" + self.document.forms[0].elements[lintIndex].name].disabled = self.document.forms[0].elements[lintIndex].disabled
			        
		}
	} catch(error){}	
	self.document.btn_tcdEffecdate.disabled=false
	self.document.forms[0].tcdEffecdate.value=''
	self.document.forms[0].tcnNumber.value=''	
	self.document.forms[0].cbeContraType.value=''
	self.document.forms[0].cbeBranch_rei.value=''
	

	if(top.frames['fraSequence'].plngMainAction!=301){
		self.document.forms[0].cbeContraType.disabled=true
		self.document.forms[0].cbeBranch_rei.disabled=true	
	}
	
}

//% ShowData: Se cargan los valores de acuerdo al número de contrato, si éste está previamente registrado en el sistema 
//--------------------------------------------------------------------------------------------------------------------
function ShowData(sField){
//--------------------------------------------------------------------------------------------------------------------
		
	if(self.document.forms[0].tcnNumber.value!='' && self.document.forms[0].tcdEffecdate.value!='' && top.frames['fraSequence'].plngMainAction!=301){
		ShowPopUp("/VTimeNet/CoReinsuran/CoReinsuran/ShowDefValues.aspx?Field=" + sField  + "&nNumber=" + self.document.forms[0].tcnNumber.value + "&dEffecdate=" + self.document.forms[0].tcdEffecdate.value,"ShowDefValuesNumberContr", 1, 1,"no","no",2000,2000);
	}else{
		if(self.document.forms[0].tcnNumber.value!='' && self.document.forms[0].tcdEffecdate.value=='')
			insDefValues('DateContr', 'nNumber=' + self.document.forms[0].tcnNumber.value);
	}
	
}   	
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CR301_K", "CR301_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="frmProportionalTreaties" ACTION="valCoReinsuran.aspx?sMode=1">
    <TABLE WIDTH="100%">            
        <TR>
            <TD WIDTH="25%"><LABEL ID=100598><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD WIDTH="25%"><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "ShowData(""NumberContr"")", True)%></TD>
            <TD WIDTH="10%"><LABEL ID=100598><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
            <TD WIDTH="40%"><%=mobjValues.NumericControl("tcnNumber", 5, Session("nNumber"),  , GetLocalResourceObject("tcnNumberToolTip"),  , 0,  ,  ,  , "ShowData(""NumberContr"")", True)%></TD>
        </TR>
        <TR>    
			<TD><LABEL ID=100600><%= GetLocalResourceObject("cbeContraTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 2
mobjValues.TypeOrder = 1
mobjValues.List = "4,683,684,685,686,687"
mobjValues.BlankPosition = True
Response.Write(mobjValues.PossiblesValues("cbeContraType", "table173", eFunctions.Values.eValuesType.clngComboType, Session("nType"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeContraTypeToolTip")))
%>
            </TD>
			<TD><LABEL ID=100601><%= GetLocalResourceObject("cbeBranch_reiCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBranch_rei", "table5000", 1, Session("nBranch_rei"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeBranch_reiToolTip"))%></TD>
		</TR>            
    </TABLE>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 2 $|$$Date: 3/05/06 11:36 $"     
</SCRIPT>    
</FORM>
</BODY>
</HTML>





