<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility    

Dim lclsTheft As ePolicy.Theft
Dim li_insured As Integer
Dim lstrActReg As String


'%insPreRO001. Esta funcion se encarga deralizar la busqueda de los datos de cliente
'------------------------------------------------------------------------------------
Private Sub insPreRO001()
	'------------------------------------------------------------------------------------
	Dim lcolThefts As ePolicy.Thefts
	Dim lclsMulti_risk As ePolicy.multi_risk
	Dim lcolMulti_risks As ePolicy.multi_risks
	
	With Request
		lcolThefts = New ePolicy.Thefts
		Call lcolThefts.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), .QueryString.Item("sCodispl"))
		
		If lcolThefts.Count > 0 Then
			lclsTheft = lcolThefts(1)
		Else
			lclsTheft = New ePolicy.Theft
			lcolMulti_risks = New ePolicy.multi_risks
			lclsTheft.nInsured = CShort("100")
			'+Se verifica si el producto es de tipo multiriesgo            
			If CStr(Session("sBrancht")) = "2" Then
				Call lcolMulti_risks.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), .QueryString.Item("sCodispl"))
				If lcolMulti_risks.Count > 0 Then
					lclsMulti_risk = lcolMulti_risks(1)
					lclsTheft.sComplCod = lclsMulti_risk.sComplCod
					lclsTheft.nBusinessTy = lclsMulti_risk.nBusinessTy
					lclsTheft.nCommerGrp = lclsMulti_risk.nCommerGrp
					lclsTheft.nCodKind = lclsMulti_risk.nCodKind
					lclsTheft.nConstCat = lclsMulti_risk.nConstCat
					lclsTheft.sDescBussi = lclsMulti_risk.sDescBussi
					lclsMulti_risk = Nothing
				End If
				'Aviso de carga de datos en la ventana de Multiriesgo
				If lclsTheft.sComplCod = vbNullString Then
					Response.Write("<SCRIPT>alert('Debe ingresar datos en la ventana de Multiriesgo');</" & "Script>")
				End If
			End If
			lcolMulti_risks = Nothing
		End If
		lcolThefts = Nothing
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("RO001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "RO001"

lstrActReg = "S" 'Considerar solo los registros activos de los combos de Giro de Negocio	
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.setZone(2, "RO001", "RO001_k.aspx"))
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>    
<SCRIPT>
//% InsChangeField: Se controla el cambio de valor de los campos de la página.
//-----------------------------------------------------------------------------
function InsChangeField(Field){
//-----------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch(Field.name){
            case "cbeBusinessTy":                    				                
				if (li_cbeBusinessTy != cbeBusinessTy.value){
					valConstCat.disabled = true;
					btnvalConstCat.disabled = true;
					valConstCat.value = '';
					UpdateDiv('valConstCatDesc','');
               
    				valCodKind.disabled=true;
					btnvalCodKind.disabled=true;									
					valCodKind.value='';
					UpdateDiv('valCodKindDesc','');	

					valCommerGrp.disabled=true;
					btnvalCommerGrp.disabled=true;																
					valCommerGrp.value='';
					UpdateDiv('valCommerGrpDesc','');								
																							
					li_cbeBusinessTy = cbeBusinessTy.value;					
				}								
								
    			if ((Field.value != '')&&(Field.value!= '0')){										
					valCommerGrp.disabled=false;
					btnvalCommerGrp.disabled=false;					
					// Asignar el valor del parámetro Tipo del Grupo Comercial
					valCommerGrp.Parameters.Param1.sValue = Field.value;					
					// Asignar el valor del parámetro Tipo del Giro de Negocio
					valCodKind.Parameters.Param1.sValue = Field.value;
					// Asignar el valor del parámetro Tipo del Tipo de Construccion
					valConstCat.Parameters.Param1.sValue = Field.value;					
				}				
			break;
            case "valCommerGrp":	            				                        
				if (li_valCommerGrp != valCommerGrp.value){				            
					valConstCat.disabled = true;
					btnvalConstCat.disabled = true;
					valConstCat.value = '';
					UpdateDiv('valConstCatDesc','');	
				
					valCodKind.disabled=true;
					btnvalCodKind.disabled=true;
					valCodKind.value='';
					UpdateDiv('valCodKindDesc','');				
									
					li_valCommerGrp = valCommerGrp.value;
				}
    			if (Field.value != ''){					
					// Asignar el valor del parámetro Grupo Comercial del Giro de Negocio
					valCodKind.disabled=false;
					btnvalCodKind.disabled=false;					
					valCodKind.Parameters.Param2.sValue=Field.value;
					//Asignar el valor del parámetro Grupo Comercial del Tipo de Construcción
					valConstCat.Parameters.Param2.sValue=Field.value;					
				}
			break;	
            case "valCodKind":					
				if (li_valCodKind != valCodKind.value){
					valConstCat.disabled = true;
					btnvalConstCat.disabled = true;
					valConstCat.value = '';
					UpdateDiv('valConstCatDesc','');	
					
					li_valCodKind = valCodKind.value;
				}                
                
    			if (Field.value != ''){					
					// Asignar el valor del parámetro Giro de Negocio del Tipo de Construcción
					valConstCat.disabled = false;
					btnvalConstCat.disabled = false;
					// Asignar el valor del parámetro Giro de Negocio del Tipo de Construcción
					valConstCat.Parameters.Param3.sValue=Field.value;
				}
			break;						
		}
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
'+Se realiza el llamado a la funcion insPreSi007M, para obtener los datos del cliente en tratamiento

Call insPreRO001()
%>
<FORM METHOD="POST" ID="FORM" NAME="frmRO001" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%="<SCRIPT>"%>
<%="var li_cbeBusinessTy=" & lclsTheft.nBusinessTy & ";"%>
<%="var li_valCommerGrp=" & lclsTheft.nCommerGrp & ";"%>
<%="var li_valCodKind=" & lclsTheft.nCodKind & ";"%>
<%="</SCRIPT>"%>
<%li_insured = lclsTheft.nInsured
If li_insured = eRemoteDB.Constants.intNull Then
	li_insured = 100
End If%>
  <TABLE WIDTH="100%">        
        <TR>
            <TD COLSPAN="8" CLASS="HighLighted"><LABEL ID=2539><A NAME="#Clasificación del riesgo"><%= GetLocalResourceObject("AnchorClasificación del riesgoCaption") %></A></LABEL></TD>                    
        </TR>
        <TR>
		    <TD COLSPAN="8" CLASS="Horline"></TD>		    
		</TR>
        <TR>
		<TD><LABEL ID=14727><%= GetLocalResourceObject("cbeBusinessTyCaption")%></LABEL></TD>
		<TD><%
		        mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        Response.Write(mobjValues.PossiblesValues("cbeBusinessTy", "tabBusinessTy", eFunctions.Values.eValuesType.clngComboType, CStr(lclsTheft.nBusinessty), True, , , , , "InsChangeField(this);", CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("cbeBusinessTyToolTip")))%></TD>			
		<TD>&nbsp;</TD>			 
	    <TD><LABEL ID=14728><%= GetLocalResourceObject("valCommerGrpCaption")%></LABEL></TD>	
	    <TD><%
	            mobjValues.Parameters.Add("nBusinessTy", lclsTheft.nBusinessty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	            mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	            Response.Write(mobjValues.PossiblesValues("valCommerGrp", "tabCommerGrp", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsTheft.nCommergrp), True, , , , , "InsChangeField(this);", lclsTheft.nCommergrp <= 0 Or CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("valCommerGrpToolTip")))%></TD>            
        <TD>&nbsp;</TD>
    	<TD><LABEL ID=14729><%= GetLocalResourceObject("valCodKindCaption")%></LABEL></TD>	    		    	
		<TD><%
		        mobjValues.Parameters.Add("nBusinessTy", lclsTheft.nBusinessty, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        mobjValues.Parameters.Add("nCommerGrp", lclsTheft.nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		        Response.Write(mobjValues.PossiblesValues("valCodKind", "TabBussKind", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsTheft.nCodkind), True, , , , , "InsChangeField(this);", lclsTheft.nCodkind <= 0 Or CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("valCodKindToolTip")))%>
		</TD>	    		    
		</TR>
		<TR>			
        <TD><LABEL ID=14730><%= GetLocalResourceObject("tctDescBussiCaption")%></LABEL></TD>
        <TD COLSPAN=4><%= mobjValues.TextControl("tctDescBussi", 30, lclsTheft.sDescBussi, , GetLocalResourceObject("tctDescBussiCaption"), , , , , CStr(Session("sBrancht")) = "2")%></TD>
        
        <TD>&nbsp;</TD>			
        
        <TD><LABEL ID=14731><%= GetLocalResourceObject("valConstCatCaption")%></LABEL></TD>  
        <TD><%mobjValues.Parameters.Add("nBusinessTy", lclsTheft.nBusinessTy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nCommerGrp", lclsTheft.nCommergrp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("nCodKind", lclsTheft.nCodkind, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                mobjValues.Parameters.Add("sFlgActReg", lstrActReg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("valConstCat", "TabConstClass", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsTheft.nConstCat), True, , , , , , lclsTheft.nConstCat <= 0 Or CStr(Session("sBrancht")) = "2", , GetLocalResourceObject("valConstCatCaption")))%>
        </TD>            
		</TR>    
        <TR>
            <TD><LABEL ID=2544><%= GetLocalResourceObject("tcnInsuredCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnInsured", 5, CStr(li_insured), True,GetLocalResourceObject("tcnInsuredToolTip"),  , 2,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2545><%= GetLocalResourceObject("tcnEmployeesCaption")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnEmployees", 4, CStr(lclsTheft.nEmployees), False, GetLocalResourceObject("tcnEmployeesToolTip"), , , , , , , False)%></TD>
            <TD COLSPAN=3>&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="8" CLASS="HighLighted"><LABEL ID=2546><A NAME="Vigilancia"><%= GetLocalResourceObject("AnchorVigilancia2Caption") %></A></LABEL></TD>
        </TR>
        <TR>
		    <TD COLSPAN="8" CLASS="Horline"></TD>		    
		</TR>
        <TR>
        <TR>
            <TD><LABEL ID=2547><%= GetLocalResourceObject("tcnAreaCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnArea", 5, CStr(lclsTheft.nArea), False,GetLocalResourceObject("tcnAreaToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2548><%= GetLocalResourceObject("tcnVigilanceCaption")%></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcnVigilance", 4, CStr(lclsTheft.nVigilance), False, GetLocalResourceObject("tcnVigilanceToolTip"), , , , , , , False)%></TD>
            <TD COLSPAN=3>&nbsp;</TD>
        </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
lclsTheft = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("RO001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









