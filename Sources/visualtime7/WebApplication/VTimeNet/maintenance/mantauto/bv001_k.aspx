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

mobjValues.sCodisplPage = "BV001"
%>


<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 5/09/03 18:35 $|$$Author: Nvaplat18 $"
    
//% insStateZone: Habilita los campos de la forma según la acción a ejecutar
//-------------------------------------------------------------------------------------------
    function insStateZone(){
//-------------------------------------------------------------------------------------------    
    var lintIndex;
    var error;
    with(self.document.forms[0]){
		try {
			for(lintIndex=0;lintIndex < elements.length;lintIndex++){
				elements[lintIndex].disabled=false;
				if(self.document.images.length>0)
				    if(typeof(self.document.images["btn" + elements[lintIndex].name])!='undefined')
				       self.document.images["btn" + elements[lintIndex].name].disabled = elements[lintIndex].disabled;
			}

		} catch(error){}
		if(top.frames['fraSequence'].plngMainAction!=301)
			cbeNlic_special.disabled = true;
		cbeNlic_special.value='';
		tctMotor.value='';
		tctChassis.value='';
		tctRegister.value='';
		tctDigit.value='';
	}
}
//% ShowData: Se muestra datos referentes al Motor y Chassis del vehículo
//-------------------------------------------------------------------------------------------
function ShowData(sField){
//-------------------------------------------------------------------------------------------
	if(top.frames['fraSequence'].plngMainAction!=301){
		switch(sField){
			case "Data_Motor":
					insDefValues(sField,"sMotor=" + self.document.forms[0].tctMotor.value + "&nMainAction=" + top.frames['fraSequence'].plngMainAction)
					break;
			case "Data_Chassis":
					insDefValues(sField,"sChassis=" + self.document.forms[0].tctChassis.value + "&nMainAction=" + top.frames['fraSequence'].plngMainAction)
					break;
			case "Data_Regist":
					insDefValues(sField,"sRegist=" + self.document.forms[0].tctRegister.value + "&nMainAction=" + top.frames['fraSequence'].plngMainAction)
					break;
		}
	}
	else{
		switch(sField){
			case "Data_Regist":
					if(self.document.forms[0].tctRegister.value!=''){
						insDefValues(sField,"sRegist=" + self.document.forms[0].tctRegister.value + "&nMainAction=" + top.frames['fraSequence'].plngMainAction + "&sLicense_ty=" + self.document.forms[0].cbeLicense_ty.value)
						break;
					}
			case "Data_License_ty":
				if (self.document.forms[0].cbeLicense_ty.value!='' && self.document.forms[0].cbeLicense_ty.value == 3){
					self.document.forms[0].tctDigit.value = '';
					self.document.forms[0].cbeNlic_special.disabled = true;
					self.document.forms[0].cbeNlic_special.value = "";
					insDefValues(sField,"sLicense_ty=" + self.document.forms[0].cbeLicense_ty.value + "&nMainAction=" + top.frames['fraSequence'].plngMainAction)
					break;
				}
				else{
					if (self.document.forms[0].cbeLicense_ty.value == 2){
						self.document.forms[0].cbeNlic_special.disabled = false;
					}
					else{
						self.document.forms[0].cbeNlic_special.disabled = true;
						self.document.forms[0].cbeNlic_special.value = "";
					}
					self.document.forms[0].tctRegister.value = '';
					self.document.forms[0].tctDigit.value = '';
					self.document.forms[0].tctRegister.disabled = false;
					self.document.forms[0].tctDigit.disabled = false;
				}	
		}
	}
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
</SCRIPT>
	  <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("BV001", "BV001_k.aspx", 1, ""))
End With
mobjMenu = Nothing%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<FORM METHOD="post" ID="FORM" NAME="frmDBVehicle" ACTION="valMantAuto.aspx?x=1">
     <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL ID=40604><A NAME="Patente"><%= GetLocalResourceObject("AnchorPatenteCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"></TD>
            <TD COLSPAN="2" CLASS="Horline"></TD>	    
        </TR>
        <TR>                        
            <TD><LABEL ID=1><%= GetLocalResourceObject("tctMotorCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctMotor", 40, "",  , GetLocalResourceObject("tctMotorToolTip"),  ,  ,  , "ShowData(""Data_Motor"")", True, 1)%></TD>
   			<TD><LABEL ID=3><%= GetLocalResourceObject("cbeLicense_tyCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeLicense_ty", "table80", eFunctions.Values.eValuesType.clngComboType, "1",  ,  ,  ,  ,  , "ShowData(""Data_License_ty"")", True,  , GetLocalResourceObject("cbeLicense_tyToolTip"),  , 3))%></TD>
		</TR>
        <TR>
			<TD><LABEL ID=2><%= GetLocalResourceObject("tctChassisCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctChassis", 40, "",  , GetLocalResourceObject("tctChassisToolTip"),  ,  ,  , "ShowData(""Data_Chassis"")", True, 2)%></TD>
            <TD><LABEL ID=4><%= GetLocalResourceObject("cbeNlic_specialCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeNlic_special", "table5594", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeNlic_specialToolTip"),  , 4)%></TD>
        </TR>
        <TR>
			<TD COLSPAN="2"></TD>
  			<TD><LABEL ID=5><%= GetLocalResourceObject("tctRegisterCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctRegister", 10,  ,  , GetLocalResourceObject("tctRegisterToolTip"),  ,  ,  , "ShowData(""Data_Regist"")", True, 5)%>-<%=mobjValues.TextControl("tctDigit", 1,  ,  , "Dígito verificador de la patente",  ,  ,  ,  , True, 6)%></TD>
        </TR>
    </TABLE> 
</FORM>
</BODY>
</HTML>





