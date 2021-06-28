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

mobjValues.sCodisplPage = "cr304_K"
%>
<SCRIPT>
//- Variable que indica la acción que seleccionó el usuario

	var mstrAction 
</SCRIPT>	
<%="<SCRIPT>mstrAction='" & Session("nMainAction") & "'"%>
//% insCancel: 
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------	
	if (top.frames["fraSequence"].pintZone==2 && (mstrAction==301)){	
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=CR304_k","EndProcess",300,180)
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
	self.document.forms[0].cboContraType.value=''
	self.document.forms[0].cboBranch.value=''
	
	
	if(top.frames['fraSequence'].plngMainAction!=301){
		self.document.forms[0].cboContraType.disabled=true
		self.document.forms[0].cboBranch.disabled=true	
	}
	
}

//% getCompleteYear: Esta rutina se encarga de devolver el año completo (4 digitos) cuando se introduce incompleto (2 dígitos).
//----------------------------------------------------------------------------------------------------------------------------
function getCompleteYear(lstrValue){
//------------------------------------------------------------------------------------------------------------------------------
    var ldtmYear = new Date()
    var lintPos  
    var lstrYear
    var llngValue = 0
    do {
       lstrValue = lstrValue.replace(".","")
    }
    while (lstrValue != lstrValue.replace(".",""))
    if (lstrValue == '') llngValue = 0
    else llngValue = parseFloat(lstrValue)
    if (llngValue<1000){
        if (llngValue<=50)
            llngValue += 2000
        else
            if (llngValue<100)
                llngValue += 1900
            else
                llngValue += 2000
    }
    return "" + llngValue    
 }   

//% ShowData: Se cargan los valores de acuerdo al número de contrato, si éste está previamente registrado en el sistema 
//--------------------------------------------------------------------------------------------------------------------
function ShowData(sField){
//--------------------------------------------------------------------------------------------------------------------
	if(self.document.forms[0].tcnNumber.value!='')
		ShowPopUp("/VTimeNet/CoReinsuran/CoReinsuran/ShowDefValues.aspx?Field=" + sField  + "&nNumber=" + self.document.forms[0].tcnNumber.value + 
		                                                            "&dEffecdate=" + self.document.forms[0].tcdEffecdate.value, "ShowDefValuesNumberContr_np", 1, 1,"no","no",2000,2000);						        			   
}   	
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CR304_K", "CR304_K.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<TD><BR></TD>
<TD><BR></TD>
<FORM METHOD="POST" ID="FORM" NAME="frmProportionalTreaties" ACTION="valCoReinsuran.aspx?sMode=1">
    <TABLE WIDTH="100%">            
        <TR>
            <TD><LABEL ID=100640><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=100640><%= GetLocalResourceObject("tcnNumberCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnNumber", 4, Session("nNumber"),  , GetLocalResourceObject("tcnNumberToolTip"),  , 0,  ,  ,  , "ShowData(""NumberContr_np"")", True)%></TD>
		</TR>
		
		<TR>
            <TD><LABEL ID=100642><%= GetLocalResourceObject("cboContraTypeCaption") %></LABEL></TD>
            <TD><%mobjValues.TypeList = 1
mobjValues.TypeOrder = 1
mobjValues.List = "680,681,682,683,685,686,687,689,690,691,692"
mobjValues.BlankPosition = True
Response.Write(mobjValues.PossiblesValues("cboContraType", "table173", eFunctions.Values.eValuesType.clngComboType, Session("nType"),  ,  ,  ,  ,  ,  , True,  , ""))%></TD>                   

            <TD><LABEL ID=100643><%= GetLocalResourceObject("cboBranchCaption") %></LABEL></TD>            
            <TD><%=mobjValues.PossiblesValues("cboBranch", "table5000", 1, Session("nBranch_rei"),  ,  ,  ,  ,  ,  , True,  , "")%></TD>
		</TR>
    </TABLE>
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"     
</SCRIPT>    
</FORM>
</BODY>
</HTML>





