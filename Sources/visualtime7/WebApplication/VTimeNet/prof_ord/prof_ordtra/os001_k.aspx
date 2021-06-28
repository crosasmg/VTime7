<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

'- Variable que almacena la transacción que llama a esta
Dim mstrCodisplOri As String


'% insPreOS001_K: Asignación a la variable de session.
'---------------------------------------------------------------------------
Sub insPreOS001_K()
	'---------------------------------------------------------------------------	
	With Request
		If IsNothing(.QueryString("sCodisplOri")) Then
			mstrCodisplOri = "OS001_K"
		Else
			mstrCodisplOri = .QueryString.Item("sCodisplOri")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "OS001_K"

'+ Se hace carga la opción de llamado
Call insPreOS001_K()
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT= "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones
     document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"
     
     var mlngClaim = ''; mlngBranch = ''; mlngProduct = ''; mlngPolicy = ''; mlngCertif = ''; mlngProponum = '';
     
//% insStateZone: habilita los campos de la forma
//-----------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------
   with (document.forms[0])
        cbeOrdClass.disabled=false;
}

//%insCertificat : Deja certificado con cero y deshabilitado.
//--------------------------------------------------------------------------------    
function insCertificat(){
//------------------------------------------------------------
	var lstrQuery

	with (self.document.forms[0]){
		if (cbeBranch.value!=mlngBranch ||
		    valProduct.value!=mlngProduct ||
		    tcnPolicy.value!=mlngPolicy ||
		    tcnProponum.value!=mlngProponum ||
		    tcnCertif.value!=mlngCertif) {
		
			mlngBranch = cbeBranch.value;
		    mlngProduct = valProduct.value;
		    mlngPolicy = tcnPolicy.value;
		    mlngProponum = tcnProponum.value;
		    mlngCertif = tcnCertif.value;
		    
			lstrQuery = "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value;
			if (cbeOrdClass.value==1) 
				lstrQuery = lstrQuery + "&sCertype=1" + "&nPolicy=" + tcnProponum.value;
			else
				lstrQuery = lstrQuery + "&sCertype=2" + "&nPolicy=" + tcnPolicy.value;
			if (lstrQuery!='')
				insDefValues("Policy_CA099", lstrQuery, '/VTimeNet/Policy/PolicyTra');
		}
	}
}

//% ReloadPage: Recarga la página y asigna los valores almacenados en el QueryString - ACM - 18/07/2002
//-----------------------------------------------------------------------------------------------------
function ReloadPage(nValue)
//-----------------------------------------------------------------------------------------------------
{
	var lstrLocation = '';
	
	if(nValue!="" && nValue>0)
	{
		lstrLocation += document.location.href;
		lstrLocation = lstrLocation.replace(/&nClaim.*/,"")		
		lstrLocation = lstrLocation.replace(/&nCaseNumber.*/,"")				
		lstrLocation = lstrLocation + "&nClaim="    + self.document.forms[0].elements["tcnClaim"].value + 
		                              "&nOrdClass=" + self.document.forms[0].elements["cbeOrdClass"].value +
		                              "&lblnReload=true";
		document.location.href = lstrLocation;
	}
}

//%ChangeCaseNumber: 
//---------------------------------------------------------------------------------------------
function ChangeCaseNumber(Field){
//---------------------------------------------------------------------------------------------
   var lstrString = '';
   var lstrLocation = '';

   lstrString += self.document.forms[0].valCase.value;

   lstrLocation += document.location.href;
   lstrLocation = lstrLocation.replace(/&sCase_num.*/,"");
   lstrLocation = lstrLocation + "&sCase_num=" + lstrString;
   lstrLocation = lstrLocation + "&nCase_num=" + Field + "&nBranch=" + self.document.forms[0].elements["cbeBranch"].value + 
                                 "&nProduct="  + self.document.forms[0].elements["valProduct"].value  +
                                 "&nPolicy="   + self.document.forms[0].elements["tcnPolicy"].value   + 
                                 "&nProponum=" + self.document.forms[0].elements["tcnProponum"].value;
                                
   document.location.href = lstrLocation;
}

//%insChargeParam: Actualiza los valores y el estado del campo Caso según sea el valor del campo Siniestro.
//%------------------------------------------------------------------------------------------
function insChargeParam(){
//%------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
//+ Si se trata de origen: siniestro.
		if (cbeOrdClass.value==3)
			if (tcnClaim.value!=mlngClaim) {
				mlngClaim = tcnClaim.value
				insDefValues("Find_Claim", "nClaim=" + tcnClaim.value, '/VTimeNet/Prof_ord/Prof_ordTra');
			}
	}
	
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insCancel(){
//-----------------------------------------------------------------------------
   return true
}

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-----------------------------------------------------------------------------
function insFinish(){
//-----------------------------------------------------------------------------
	top.document.location.reload()

}

//% insChangeField: Se recargan los valores cuando cambia el campo
//----------------------------------------------------------------
function insChangeField(){
//----------------------------------------------------------------    
	with (self.document.forms[0]){
		cbeBranch.disabled = true;
		valProduct.disabled = true;
		btnvalProduct.disabled = true;
		tcnPolicy.disabled = true;
		tcnProponum.disabled = true;
		tcnCertif.disabled = true;
		tcnClaim.disabled = true;
		valCase.disabled = true;

		cbeBranch.value = "";
		valProduct.value = "";
		UpdateDiv('valProductDesc','');
		tcnPolicy.value = "";
		tcnProponum.value = "";
		tcnCertif.value = "";
		tcnClaim.value = "";
		valCase.value = "";
		UpdateDiv('valCaseDesc', '');
		
		switch(cbeOrdClass.value){
//+ 1-Propuesta
            case "1": 
				cbeBranch.disabled = false;
				tcnProponum.disabled = false;
				break;
	
//+ 2-Póliza
			case "2": {
				cbeBranch.disabled = false;
				tcnPolicy.disabled = false;
				break;
			}		
//+ 3-Siniestro	
			case "3": 
				tcnClaim.disabled = false;
				valCase.disabled = false;
				break;
		}
	}
}
</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "OS001_K_k.aspx", 1, vbNullString))
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="OS001_K" ACTION="valProf_ordTra.aspx?sMode=1">
    <BR><BR>
    <TABLE WIDTH="100%">    
        <TR>        
            <TD><LABEL ID=11781><%= GetLocalResourceObject("cbeOrdClassCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOrdClass", "Table560", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nOrdClass"),  ,  ,  ,  ,  , "insChangeField()", True,  , GetLocalResourceObject("cbeOrdClassToolTip"))%></TD>
        </TR>               
            <TD WIDTH="15% COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"),  ,  ,  ,  ,  , True)%></TD>
			<TD> <LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True, Request.QueryString.Item("nProduct"))%></TD>
        </TR>        
		<TR>
			<TD WIDTH="15% COLSPAN="3"><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicy", 8, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insCertificat()", True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnProponumCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnProponum", 8, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnProponumToolTip"),  , 0,  ,  ,  , "insCertificat()", True)%></TD>
			<TD><LABEL ID=13849><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCertif", 4, "0",  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True)%></TD>
		</TR>
		<TR>
            <TD><LABEL ID=0><%= GetLocalResourceObject("tcnClaimCaption") %> </LABEL> </TD>
            <TD> <%=mobjValues.NumericControl("tcnClaim", 10, Request.QueryString.Item("nClaim"),  , GetLocalResourceObject("tcnClaimToolTip"),  , 0,  ,  ,  , "ReloadPage(this.value);", True)%></TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valCaseCaption") %></LABEL></TD>
			<TD><%
With mobjValues
	.BlankPosition = False
	.Parameters.Add("nClaim", mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If mobjValues.StringToType(Request.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
		Response.Write(mobjValues.PossiblesValues("valCase", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nCase_Num"), True,  ,  ,  ,  , "ChangeCaseNumber(this.value);", False,  , GetLocalResourceObject("valCaseToolTip")))
	Else
		Response.Write(mobjValues.PossiblesValues("valCase", "TabClaim_cases", eFunctions.Values.eValuesType.clngComboType,  , True,  ,  ,  ,  , "ChangeCaseNumber(this.value);", True,  , GetLocalResourceObject("valCaseToolTip")))
	End If
End With
%>
			</TD>
        </TR>
    </TABLE>
<%
With Response
	If Not IsNothing(Request.QueryString.Item("lblnReload")) AndAlso CBool(Request.QueryString.Item("lblnReload")) Then
		.Write("<SCRIPT>insChargeParam();</SCRIPT>")
	End If
	If IsNothing(Request.QueryString.Item("sCodisplOri")) Then
		.Write(mobjValues.HiddenControl("tctCodisplOri", "OS001_K"))
	Else
		.Write(mobjValues.HiddenControl("tctCodisplOri", " "))
	End If
End With
%>
</FORM>
</BODY>
</HTML><%
mobjValues = Nothing
%>





