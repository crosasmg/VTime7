<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- The object to handling of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**- The object to handling the generics routines is defined
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LTC001_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LTC001_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

%>
<SCRIPT>

//------------------------------------------------------------------------------
// Para Control de Versiones "NO REMOVER"
//------------------------------------------------------------------------------
	document.VssVersion="$$Revision: 3 $|$$Date: 7/06/06 7:14p $" 
//------------------------------------------------------------------------------

</SCRIPT>
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	        .Write(mobjMenu.MakeMenu("LTC001", "LTC001_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"), Session("sSche_code")))
End With
%>
	<SCRIPT LANGUAGE="JavaScript">

//**% insStateZone: This function updates the status of field (Enable/Disable) of the zone in 
//**%process
//% insStateZone: Esta función actualiza el estado (habilita o deshabilita) de los campos de la
//% zona en proceso
//---------------------------------------------------------------------------------------------
		function insStateZone(lintMainAction)
//---------------------------------------------------------------------------------------------
		{
			nMainAction = lintMainAction;
		    if (nMainAction==401) 
			{
				self.document.forms[0].optCondition[0].disabled = false;
				self.document.forms[0].optCondition[1].disabled = false;
				self.document.forms[0].tcnLettRequest.disabled = false;
				self.document.forms[0].tctClient.disabled = false;
				self.document.forms[0].cbeBranch.disabled = false;
				self.document.forms[0].tcnPolicy.disabled = false;
				self.document.forms[0].tcnClaim.disabled = false;
				self.document.forms[0].tcdEffecdate.disabled = false;
				self.document.forms[0].lsAplicant.disabled = false;
			}
		}
	
//**% insCancel: Executes the action To cancel of the page
//% insCancel: ejecuta la acción Cancelar de la página
//---------------------------------------------------------------------------------------------
		function insCancel()
//---------------------------------------------------------------------------------------------
		{
//**+ Only this process will be effected when the user cancels the transaction always
//+ Sólamente se efectuará este proceso cuando el usuario cancela la transacción siempre 
			return true;
		}   

//**% insSubmit(): This function executes the code when the action is finish
//% insSubmit(): Esta función ejecuta el código cuando la acción es finalizar
//---------------------------------------------------------------------------------------------
		function insSubmit()	
//---------------------------------------------------------------------------------------------
		{
			return true;
		}

//**% insDisabledControl: Enable/Disable the controls dependent on the page.
//% insDisabledControl: Habilita/Deshabilita los controles dependientes de la página
//-------------------------------------------------------------------------------------------
		function insDisabledControl(lnrequest,lnClient,lnClaim,lnBranch,lnProduct,lnPolicy,lnCertif,lnEfedaF,lnEfedaU,lAplicant)
//-------------------------------------------------------------------------------------------
		{
			with(self.document.forms[0])
			{
				tcnLettRequest.disabled = lnrequest;
				tctClient.disabled = lnClient;
				cbeBranch.disabled = lnBranch;
				valProduct.disabled = lnProduct;
				tcnPolicy.disabled = lnPolicy;
				tcnCertificat.disabled = lnCertif;
				tcnClaim.disabled = lnClaim;				
				tcdEffecdate.disabled= lnEfedaF;
				tcdEffecdate1.disabled= lnEfedaU;
				lsAplicant.disabled = lAplicant;
			}
		}

//**% LockControl: It generates the conditions that were happening to "insDisabledControl" to 
//**% enable or to disable the controls of the page.
//% LockControl: Genera las condiciones que se le pasaran a "insDisabledControl" para habilitar
//% o deshabilitar los controles de la pagina.
//---------------------------------------------------------------------------------------------
		function LockControl(Control)
//---------------------------------------------------------------------------------------------
		{
			//alert(Control);
			with(self.document.forms[0]){
			    switch(Control){
					case 'Request':
						if (tcnLettRequest.value != '')
							{
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								insDisabledControl(false,false,false,false,true,false,true,true,true,true);
							}
						else
							{
			    				UpdateDiv('valProductDesc','');
			    				valProduct.value='';
								tcnLettRequest.value = '';
								tctClient.value = '';
								UpdateDiv('lblClieName','');
								cbeBranch.value = '0';
								tcnPolicy.value = '';
								tcnCertificat.value = '';
								tcnClaim.value = '';							
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								lsAplicant.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
							}
						break;							
					case 'Clients':
						if (tctClient.value != '')
							{
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
							}
						else
							{
			    				UpdateDiv('valProductDesc','');
			    				valProduct.value='';
								tcnLettRequest.value = '';
								UpdateDiv('lblClieName','');
								cbeBranch.value = "0";
								tcnPolicy.value = '';
								tcnCertificat.value = '';
								tcnClaim.value = '';
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								lsAplicant.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
							}					
						break;
					case 'Aplicant':
						if (lsAplicant.value != 0)
							{
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								insDisabledControl(true,false,true,true,true,true,true,true,true,false);
							}
						else
							{
			    				UpdateDiv('valProductDesc','');
			    				valProduct.value='';
								tcnLettRequest.value = '';
								UpdateDiv('lblClieName','');
								cbeBranch.value = "0";
								tcnPolicy.value = '';
								tcnCertificat.value = '';
								tcnClaim.value = '';
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								lsAplicant.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
							}					
						break;
			    	case 'Branch':
			    		UpdateDiv('valProductDesc','');
			    		valProduct.value='';
			    		if(cbeBranch.value!='0')
			    			{
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
			    				document.btnvalProduct.disabled=false;
								insDisabledControl(false,false,false,false,false,false,true,false,true,true);
			    			}
			    			else
			    			{
								tcnLettRequest.value = '';
								tctClient.value = '';
								UpdateDiv('lblClieName','');
								tcnPolicy.value = '';
								tcnCertificat.value = '';
								tcnClaim.value = '';
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
			    				document.btnvalProduct.disabled=true;
			    				lsAplicant.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
			    			}
						break;
					case 'Policy':
						if (tcnPolicy.value != '')
							{
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								tcnCertificat.value = '0';
								insDisabledControl(false,false,true,false,true,false,false,false,true,true);
							}
						else
							{
			    				UpdateDiv('valProductDesc','');
			    				valProduct.value='';
								tcnLettRequest.value = '';
								tctClient.value = '';
								UpdateDiv('lblClieName','');
								cbeBranch.value = '0';
								tcnCertificat.value = '';
								tcnClaim.value = '';
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								lsAplicant.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
							}					
						break;											
					case 'Claims':
						if (tcnPolicy.value != '')
							{
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,true);
							}
						else
							{
			    				UpdateDiv('valProductDesc','');
			    				valProduct.value='';
								tcnLettRequest.value = '';
								tctClient.value = '';
								UpdateDiv('lblClieName','');
								cbeBranch.value = '0';
								tcnPolicy.value = '';
								tcnCertificat.value = '';
								tcdEffecdate.value = '';
								tcdEffecdate1.value = '';
								lsAplicant.value = '';
								insDisabledControl(false,false,false,false,true,false,true,false,true,false);
							}					
						break;					
					case 'tcdEffecdate':
			    		if(tcdEffecdate.value!='')
			    		{
			    			tcnLettRequest.disabled = true;
			    			tcdEffecdate1.disabled = false;
			    		}
						else
						{
							tcdEffecdate1.value = '';
							tcdEffecdate1.disabled= true;
							tcnLettRequest.disabled = false;
						}					
			    }
			}
		}
		
	</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="post" ID="FORM" NAME="LTC001" ACTION="valletterque.aspx?x=1">
		<%With Response
	.Write("<BR><BR>")
End With
%> 
		<TABLE WIDTH="100%">
			<TR>
				<TD WIDTH="20%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=7299><a NAME="Consulta">Tipo de consulta</a></LABEL></td>
				<TD>&nbsp;</td>
				<TD WIDTH="65%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=15753><a NAME="Clave">Parámetros</a></LABEL></td>
				<TD>&nbsp;</td>
				<TD WIDTH="15%" COLSPAN="2" CLASS="HighLighted"><LABEL ID=7301><a NAME="Fecha_Consulta">Fecha</a></LABEL></td>
			</TR>
			<TR>
				<TD COLSPAN="2" CLASS="HorLine"></TD>
				<TD></TD>
				<TD COLSPAN="2" CLASS="HorLine"></TD>
				<TD></TD>
				<TD COLSPAN="2" CLASS="HorLine"></TD>
			</TR>
			<TR>
				<TD VALIGN=top>
					<TABLE BORDER="0"  STYLE="BORDER-RIGHT: 1px solid;BORDER-TOP: 1px solid;BORDER-LEFT: 1px solid;BORDER-BOTTOM: 1px solid"  BORDERCOLORDARK=navy bordercolorlight=navy cellpadding=0 cellspacing=0>
						<TR>
							<TD>
								<TABLE BORDER="0" width="170">
									<!--TR>
										<TD VALIGN="top"><LABEL ID=7302>Consult</LABEL></TD>
									</TR-->
									<TR>
										<TD>
											<%=mobjValues.OptionControl(7303, "optCondition","Todos", CStr(1), "0",  , False,  ,"Todas las cartas que cumplan con los parámetros dados")%>
											<%=mobjValues.OptionControl(7304, "optCondition","Pendiente de impresión", CStr(0), "1",  , False,  ,"Cartas pendientes de impresión que cumplan con los parámetros dados")%>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
				</TD>
				<TD></TD>
				<TD></TD>
				<TD>
					<TABLE BORDER="0">			
						<TR>
						    <TD WIDTH="15%"><LABEL ID=7305>Solicitud</LABEL></TD>
						    <TD><%=mobjValues.NumericControl("tcnLettRequest", 5, "",  ,"Numero que identifica la solicitud",  ,  ,  ,  ,  , "LockControl(""Request"");", False)%></TD>
						</TR>
						<TR>
						    <TD><LABEL ID=7306>Cliente</LABEL></TD>
						    <TD COLSPAN = 3><%=mobjValues.ClientControl("tctClient", "",  ,"Código del cliente", "LockControl(""Clients"");", False, "lblClieName")%></TD>
						</TR>				
						
						<TR>
						    <TD><LABEL ID=7312>Solicitante</LABEL></TD>
						    <TD COLSPAN = 3><%Response.Write(mobjValues.PossiblesValues("lsAplicant", "TabUsers", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , "LockControl(""Aplicant"");", False,  ,"Código del solicitante"))%></TD>
						</TR>	
						
						
						<TR>
							<TD><LABEL ID=7307>Ramo</LABEL></td>
							<TD>
								<%
Response.Write(mobjValues.BranchControl("cbeBranch","Número del ramo en proceso", String.Empty))
%>
							</TD>
							<TD><LABEL ID=7308>Producto</LABEL></td>
							<TD><%
Response.Write(mobjValues.ProductControl("valProduct","Número del producto en proceso", String.Empty))
%>
							</TD>
						</TR>
						<TR>
							<TD><LABEL ID=7309>Poliza</LABEL></TD>
							<TD>
						        <%
Response.Write(mobjValues.NumericControl("tcnPolicy", 8, "",  ,"Número de la póliza en proceso",  ,  ,  ,  ,  , "LockControl(""Policy"");", False))
%>
							</TD>
							<TD><LABEL ID=7310>Certificado</LABEL></td>
							<TD><%=mobjValues.NumericControl("tcnCertificat", 8, Request.Form.Item("tcnCertificat"),  ,"Número del certificado",  ,  ,  ,  ,  ,  , True)%></TD>
						</TR>
						<TR>
					        <TD WIDTH=60><LABEL ID=7311>Siniestro</LABEL></TD>
							<TD><%
Response.Write(mobjValues.NumericControl("tcnClaim", 8, "",  ,"Número del siniestro en proceso",  , 0,  ,  ,  , "LockControl(""Claims"");", False))
%>
							</TD>
						</TR>							
									
					</TABLE>
				</TD>
				<TD></TD>
				<TD></TD>
				<TD>
					<TABLE BORDER="0">
						<TR>
							<TD><LABEL ID=7313>Fecha "Desde"</LABEL></td>
							<TD>
							    <%
Response.Write(mobjValues.DateControl("tcdEffecdate", _
                                      "", _
                                      True, _
                                      "Fecha inicial para el rango de fechas definidas por la consulta",  _
                                      , _
                                      , _
                                      , _
                                      "LockControl(""tcdEffecdate"");", _
                                      False))
'"LockControl(""tcdEffecdate"");"
%>
							</TD>
						</TR>
						<TR>
							<TD><LABEL ID=7314>Fecha "Hasta"</LABEL></td>
							<TD>
							    <%
Response.Write(mobjValues.DateControl("tcdEffecdate1",  , True,"Fecha final para el rango de fechas definidas por la consulta",  ,  ,  ,  , False))
%>
							</TD>
						</TR>
					</TABLE>			
				</TD>
			</TR>
		</TABLE>
<%mobjValues = Nothing
mobjMenu = Nothing%>
		  <SCRIPT LANGUAGE=JavaScript>
			self.document.forms[0].tcdEffecdate1.disabled = true;
		  </SCRIPT>
	</FORM>
</BODY>
</HTML>

<SCRIPT LANGUAGE=JavaScript FOR=cbeBranch EVENT=onchange>
	LockControl("Branch"); document.forms[0].elements["valProduct"].Parameters.Param1.sValue=this.value;
</SCRIPT>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
Call mobjNetFrameWork.FinishPage("LTC001_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







