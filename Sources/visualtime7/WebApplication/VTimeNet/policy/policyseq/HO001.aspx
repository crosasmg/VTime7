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

Dim lclsHomeOwner As ePolicy.HomeOwner

Dim nFloodZone As Integer

Dim nSeismicZone As Integer


'%insPreHO001. Esta funcion se encarga deralizar la busqueda de los datos de cliente
'------------------------------------------------------------------------------------
Private Sub insPreHO001()
	'------------------------------------------------------------------------------------
	Dim lcolHomeOwners As ePolicy.HomeOwners
	Dim lclsTabSeismicFloodZone As eBranches.TabSeismicFloodZone
	
	With Request
		lcolHomeOwners = New ePolicy.HomeOwners
		lclsTabSeismicFloodZone = New eBranches.TabSeismicFloodZone
		
		Call lcolHomeOwners.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
		
		If lcolHomeOwners.Count > 0 Then
			lclsHomeOwner = lcolHomeOwners(1)
		Else
			lclsHomeOwner = New ePolicy.HomeOwner
		End If
		
		If lclsHomeOwner.nFloodZone <= 0 Or lclsHomeOwner.nSeismicZone <= 0 Then
		    If lclsTabSeismicFloodZone.Find_Pol(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
                nFloodZone = lclsTabSeismicFloodZone.nZoneType
                nSeismicZone = lclsTabSeismicFloodZone.nSeismicZone
		    End If
		Else    
            nFloodZone = lclsHomeOwner.nFloodZone
            nSeismicZone = lclsHomeOwner.nSeismicZone
		End If
		
	End With
	lcolHomeOwners = Nothing
	lclsTabSeismicFloodZone = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("HO001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "HO001"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<HTML>
<HEAD>

<SCRIPT LANGUAGE="JavaScript">
//% InsValueInit: Habilita y deshabilita los campos necesarios
//---------------------------------------------------------------------------------------------------
function InsValueInit(sItem){
//---------------------------------------------------------------------------------------------------
	
	with(self.document.forms[0]){
		
		if (cbeSwimPool.value=='')
		{
			chkFencePool.disabled = true;
			tcnFenceHeight.disable = (chkFencePool.checked==true)?false:true;
			chkTrampoline.disabled = true;
		}
		
		if (chkAnimalsInd.checked==true)
		{
			tctAnimalsDes.disabled = true;	
			chkAttackedInd.disabled = true;
		}
		
		
		if (chkPolicy_other.checked==true)
		{
			chkCov_purc.checked = false;
			chkCov_purc.disabled = true;
		}
		else
		{
			chkCov_purc.disabled = false;
		}
		if (chkCov_purc.checked==true)
		{
			chkPolicy_other.checked = false;
			chkPolicy_other.disabled = true;
		}
		else
	    {
	    	chkPolicy_other.disabled = false;
	    }
		if (tcnStories.value=='')
		{
			tcnStories.value = 1;
		}		
		switch (sItem.name) {
			
			case "cbeSwimPool":
				if(sItem.value=='')
				{
					tcnFenceHeight.disabled  = true;
					chkFencePool.checked = false;
					chkTrampoline.checked = false
					chkFencePool.disabled = true;
					chkTrampoline.disabled = true;
				}   
				else 
				{	
					tcnFenceHeight.disabled  = true;
					chkFencePool.checked = false;
					chkTrampoline.checked = false;
					chkFencePool.disabled = false;
					chkTrampoline.disabled = false;
				}
			break;
			
			case "chkFencePool":
				tcnFenceHeight.value = '';
				tcnFenceHeight.disabled  = (chkFencePool.checked==false)?true:false;
			break;
			
			case "chkAnimalsInd":
				
				tctAnimalsDes.value = ''
				chkAttackedInd.checked = false
				tctAnimalsDes.disabled = (chkAnimalsInd.checked==false)?true:false;
				chkAttackedInd.disabled = (chkAnimalsInd.checked==false)?true:false;
			break;	
			
			case "cbeExterConstr":
				tctOther_constr.value = '';
				tctOther_constr.disabled = (cbeExterConstr.value==99)?false:true;
			break;	
			
			case "chkCov_purc":	
				tcnPrice_purch.value = '';
				cbeCurrency_purch.value = '';
				tcnPrice_purch.disabled = (chkCov_purc.checked==false)?true:false;
				tcdDate_purch.value = '';
				tcdDate_purch.disabled = (chkCov_purc.checked==false)?true:false;
				
				chkPolicy_other.checked = false;
				chkPolicy_other.disabled = (chkCov_purc.checked==false)?false:true;
				
				tcnCap_other.value = '';
				cbeCurrency_other.value = '';
				tcdExpir_other.value = '';
				tcnCap_other.disabled = (chkCov_purc.checked==false)?false:true;
				cbeCurrency_other.disabled = (chkCov_purc.checked==false)?false:true;
				tcdExpir_other.disabled = (chkCov_purc.checked==false)?false:true;
				
			break;
			case "tcnPrice_purch":	
				cbeCurrency_purch.value = '';
				cbeCurrency_purch.disabled = (tcnPrice_purch.value=='')?true:false;
			break;
			
			case "chkPolicy_other":
				tcnCap_other.value = '';
				cbeCurrency_other.value = '';
				tcdExpir_other.value = '';
				tcnCap_other.disabled = (chkPolicy_other.checked==false)?true:false;
				cbeCurrency_other.disabled = (chkPolicy_other.checked==false)?true:false;
				tcdExpir_other.disabled = (chkPolicy_other.checked==false)?true:false;
				if (chkPolicy_other.checked==true)
				{
					cbeCurrency_purch.value = '';
					tcnPrice_purch.value = '';
					tcdDate_purch.value = '';
				}			
			break;
			
			case "tcnCap_other":
				
				tcdExpir_other.disabled = (tcnCap_other.value=='')?true:false;
			
			break;
		}
	}
	
	
} 
</SCRIPT>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.setZone(2, "HO001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
'+Se realiza el llamado a la funcion insPreSi007M, para obtener los datos del cliente en tratamiento

Call insPreHO001()

%>

<FORM METHOD="POST" ID="FORM" NAME="frmHO001" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
  <TABLE WIDTH="100%">
                      
         
            <TD><LABEL ID=2822>Tipo de vivienda</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeDwellingType", "TABLE6012", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nDwellingType),  ,  ,  ,  ,  ,  , False, 2,vbNullString, eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2823>Tipo de ocupación</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeOwnerShip", "TABLE6005", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nOwnerShip),  ,  ,  ,  ,  ,  , False, 2,vbNullString, eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2824>Años construcción</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnYear_built", 4, CStr(lclsHomeOwner.nYear_built), False,"Año en el que fue construida la vivienda",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
       
       <TR>
            <TD COLSPAN="10" CLASS="HighLighted"><LABEL ID=2825><A NAME="Antecedentes">Antecedentes</A></LABEL></TD>
                    
        </TR>
        <TR>
		    <TD COLSPAN="10" CLASS="Horline"></TD>
		    <TD></TD>
		</TR>
        
        <TR>       
       
        <TR>
            <TD><LABEL ID=2826>Cobertura por compra de vivienda</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkCov_purc", "", lclsHomeOwner.sCov_purc,  , "InsValueInit(this);", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2827>Precio de compra</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnPrice_purch", 18, CStr(lclsHomeOwner.nPrice_purch), False,"Precio de compra de la vivienda", true , 6,  ,  ,  , "InsValueInit(this);", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2828>Moneda</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency_purch", "TABLE11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nCurrency_purch),  ,  ,  ,  ,  ,  , True, 2,vbNullString, eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2829>Fecha de la compra</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdDate_purch", CStr(lclsHomeOwner.dDate_purch), False,"Fecha de la compra",  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2830>Posee otra póliza</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkPolicy_other", "", lclsHomeOwner.sPolicy_other,  , "InsValueInit(this);", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2831>Monto asegurado</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnCap_other", 18, CStr(lclsHomeOwner.nCap_other), False,"Monto asegurado en la otra póliza que cubre el riesgo", true , 6,  ,  ,  , "InsValueInit(this);", True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2832>Moneda</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurrency_other", "TABLE11", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nCurrency_other),  ,  ,  ,  ,  ,  , True, 2,"Moneda en la que está expresado el monto asegurado en la otra póliza", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2833>Fin de vigencia</LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdExpir_other", CStr(lclsHomeOwner.dExpir_other), False,"fin de vigencia",  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        
        <TR>
            <TD COLSPAN="10" CLASS="HighLighted"><LABEL ID=2834><A NAME="Datos de la construcción">Datos de la construcción</A></LABEL></TD>
                    
        </TR>
        <TR>
		    <TD COLSPAN="10" CLASS="Horline"></TD>
		    <TD></TD>
		</TR>
        
        <TR>       
        <TR>
            <TD><LABEL ID=2835>Construc. exterior</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeExterConstr", "TABLE5536", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsHomeOwner.nExterConstr),  ,  ,  ,  ,  , "InsValueInit(this);", False, 2,"Material con el que se hizo la construcción exterior", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2836>Otro tipo de construcción</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctOther_constr", 30, lclsHomeOwner.sOther_constr, False,"Descripción de otro tipo de construcción",  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2837>Pisos</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnStories", 4, CStr(lclsHomeOwner.nStories), False,vbNullString,  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2838>Tipo de fundación</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeFoundType", "TABLE6003", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nFoundType),  ,  ,  ,  ,  ,  , False, 2,"Tipo de fundación", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2839>Tipo de techo</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeRoofType", "TABLE7038", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nRoofType),  ,  ,  ,  ,  ,  , False, 2,"Tipo de techo", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2840>Año de instalación</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnRoofYear", 4, CStr(lclsHomeOwner.nRoofYear), False,"Año en que se realizó la instalación o último cambio al techo",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
            <TD><LABEL ID=2841>Superficie Constr.</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnHomeSuper", 8, CStr(lclsHomeOwner.nHomeSuper), False,"Superficie de construcción de la vivienda",  , 2,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2842>Superficie Tierra</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnLandSuper", 8, CStr(lclsHomeOwner.nLandSuper), False,"Área del terreno donde está la vivienda",  , 2,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        
        <TR>
            <TD COLSPAN="10" CLASS="HighLighted"><LABEL ID=2843><A NAME="Características de la vivienda">Características de la vivienda</A></LABEL></TD>
                    
        </TR>
        <TR>
		    <TD COLSPAN="10" CLASS="Horline"></TD>
		    <TD></TD>
		</TR>
        
        <TR>       
        <TR>
            <TD><LABEL ID=2844>Estacionamiento</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnGarage", 4, CStr(lclsHomeOwner.nGarage), False,"Cantidad de vehículos que pueden ocupar el estacionamiento",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2845>Chimeneas</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnFirePlace", 4, CStr(lclsHomeOwner.nFirePlace), False,"Cantidad de chimeneas que se tiene en la vivienda",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2846>Habitaciones</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnBedrooms", 4, CStr(lclsHomeOwner.nBedrooms), False,"Cantidad de habitaciones que se tienen en la vivienda",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2847>Baños Completos</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnFullBath", 4, CStr(lclsHomeOwner.nFullBath), False,"Cantidad de baños completos tiene la vivienda",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2848>Medios baños</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnHalfBath", 4, CStr(lclsHomeOwner.nHalfBath), False,"Cantidad de medios baños que tiene la vivienda",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2849>Aire acondicionado</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeAirType", "TABLE6001", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nAirType),  ,  ,  ,  ,  ,  , False, 2,"Sistema de aire acondicionado que posee la vivienda", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2850>Calefacción</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeAlt_heating", "TABLE6002", eFunctions.Values.eValuesType.clngComboType, CStr(lclsHomeOwner.nAlt_heating),  ,  ,  ,  ,  ,  , False, 2,"Sistema de calefacción que posee la vivienda", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=17661>Depósito de Gasolina</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkGas", "", lclsHomeOwner.sGas,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2852>Sistema de riego</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkSprinkSys", "", lclsHomeOwner.sSprinkSys,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2853>Hidrante cercano</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDist_Hydr", 4, CStr(lclsHomeOwner.nDist_Hydr), False,"Distancia al hidrante más cercano (mts2)",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2854>Compañía Alarma</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctAlarm_comp", 30, lclsHomeOwner.sAlarm_comp, False,"Nombre de la compañía que está a cargo del sistema de alarma de la vivienda",  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2855>¿Se permite fumar?</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkNon_smok", "", lclsHomeOwner.sNon_smok,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2856>Dist. Est. Bombero</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnDist_fire", 4, CStr(lclsHomeOwner.nDist_fire), False,"Distancia a la estación de bomberos más cercana",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2857>Estac. Bomberos</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctFireDepart", 30, lclsHomeOwner.sFireDepart, False,"Nombre de la estación de bomberos más cercana",  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2858>Zona de inundación</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeFloodZone", "TABLE6007", eFunctions.Values.eValuesType.clngComboType, CStr(nFloodZone),  ,  ,  ,  ,  ,  , True, 2,"Zona de inundación", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2859>Seg. Inundación</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkFloodInd", "", lclsHomeOwner.sFloodInd,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=17662>Zona sísmica</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSeismicZone", "TABLE7047", eFunctions.Values.eValuesType.clngComboType, CStr(nSeismicZone),  ,  ,  ,  ,  ,  , True, 2,"Zona sísmica", eFunctions.Values.eTypeCode.eNumeric)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=2860>Piscina</LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeSwimPool", "TABLE6004", eFunctions.Values.eValuesType.clngWindowType, CStr(lclsHomeOwner.nSwimPool),  ,  ,  ,  ,  , "InsValueInit(this);",  , 2,"Ubicación de la piscina", eFunctions.Values.eTypeCode.eNumeric)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2861>Con cerca</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkFencePool", "", lclsHomeOwner.sFencePool,  , "InsValueInit(this);", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2862>Altura de la Cerca</LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnFenceHeight", 4, CStr(lclsHomeOwner.nFenceHeight), False,"Altura de la Cerca",  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
        <TR>
            <TD><LABEL ID=2863>Trampolin</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkTrampoline", "", lclsHomeOwner.sTrampoline,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
                
        <TR>
            <TD COLSPAN="10" CLASS="HighLighted"><LABEL ID=2864><A NAME="Mascotas o animales">Mascotas o animales</A></LABEL></TD>
                    
        </TR>
        <TR>
		    <TD COLSPAN="10" CLASS="Horline"></TD>
		    <TD></TD>
		</TR>
        
        <TR>
            <TD><LABEL ID=2865>Mascota/Ganado</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkAnimalsInd", "", lclsHomeOwner.sAnimalsInd,  , "InsValueInit(this);", False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2866>Descripción</LABEL></TD>
            <TD><%=mobjValues.TextControl("tctAnimalsDes", 30, lclsHomeOwner.sAnimalsDes, False,"Descripción de los animales (cantidad, tipo y raza)",  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=2867>¿Han atacado?</LABEL></TD>
            <TD><%=mobjValues.CheckControl("chkAttackedInd", "", lclsHomeOwner.sAttackedInd,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
        </TR>
  </TABLE>
</FORM>
</BODY>
</HTML>
    
<%
mobjValues = Nothing
lclsHomeOwner = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("HO001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>

<SCRIPT>
    InsValueInit('');
</SCRIPT>







