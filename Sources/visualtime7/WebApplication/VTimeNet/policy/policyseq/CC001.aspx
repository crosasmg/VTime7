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

'- Variables para el manejo de los valores cuando se carga o recarga la página
Dim valinsmodality As Object
Dim tcnguar_type As Object
Dim tctcontracnum As String
Dim tcdcontracdat As Object
Dim valtime_unit As Integer
Dim tcdterm_date As Date
Dim tcntime_eject As Object
Dim tcncredcau As Object
Dim cbeCurrency As String
Dim tcnindemper As Object
Dim tcnmoraallow As Object
Dim tcntransmon1 As Object
Dim tcntransmon2 As Object
Dim tcnindper1 As Object
Dim tcnindper2 As Object
    Dim chksfollowup As Object
    Dim valStatusbond As Object
    Dim chksInsurSector As Object
Dim tctContractObject As String
'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

Dim lclsCredit As ePolicy.Credit

'%insPreCC001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreCC001()
	'------------------------------------------------------------------------------
	
	Dim lcolCredits As ePolicy.Credits
	With Request
		lcolCredits = New ePolicy.Credits
		Call lcolCredits.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
		If lcolCredits.Count > 0 Then
			lclsCredit = lcolCredits.Item(1)
			
'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
			If IsNothing(Session("nEnter")) Then
				Call lclsCredit.insPreCC001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), Session("nTransaction"))
				Session("nEnter") = 1
			End If
		Else
			lclsCredit = New ePolicy.Credit
		End If
		
	End With
	lcolCredits = Nothing
	
End Sub

'% DefaultValues: Se realiza el manejo de los valores de los campos cuando se carga o recarga la página
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub DefaultValues()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim lclsCertificat As ePolicy.Certificat
        
	If Request.QueryString.Item("mblnReloadPage") = "True" Then
		With Request
			
            valStatusbond = .QueryString.Item("valStatusbond")
			valinsmodality = .QueryString.Item("valinsmodality")
			tcnguar_type = .QueryString.Item("tcnguar_type")
			tctcontracnum = .QueryString.Item("tctcontracnum")
			tcdcontracdat = .QueryString.Item("tcdcontracdat")
			tcntime_eject = .QueryString.Item("tcntime_eject")
			tcncredcau = .QueryString.Item("tcncredcau")
			cbeCurrency = .QueryString.Item("cbeCurrency")
			tcnindemper = .QueryString.Item("tcnindemper")
			tcnmoraallow = .QueryString.Item("tcnmoraallow")
			tcntransmon1 = .QueryString.Item("tcntransmon1")
			tcntransmon2 = .QueryString.Item("tcntransmon2")
			tcnindper1 = .QueryString.Item("tcnindper1")
			tcnindper2 = .QueryString.Item("tcnindper2")
            chksfollowup = .QueryString.Item("chksfollowup")
                chksInsurSector =   .QueryString.Item("chksInsurSector")
		End With
		
	Else
		With lclsCredit
			
            valStatusbond = .nBondstatus
			valinsmodality = .nInsmodality
			tcnguar_type = .nGuar_type
			tctcontracnum = .sContracnum
			tcdcontracdat = .dContracdat
			tcntime_eject = .nTime_eject
			valtime_unit = .ntime_unit
			tcdterm_date = .dterm_date
			tcncredcau = .nCredcau
			tcnindemper = .nIndemper
			tcnmoraallow = .nMoraallow
			tcntransmon1 = .nTransmon1
			tcntransmon2 = .nTransmon2
			tcnindper1 = .nIndper1
			tcnindper2 = .nIndper2
            chksfollowup = .sFollowUp
                tctContractObject = .sContractObject
                chksInsurSector = IIf( .sInsurSector = "" , "1" , .sInsurSector )
		End With
	End If
	
	If valinsmodality = eRemoteDB.Constants.intNull Then
		valinsmodality = 2
		
	End If
	
'+ Si la fecha de inicio del contrato está vacía ó si se trata de una emisión, recuperación o reemisión se asigna
'+ defecto los valores de los campos Fecha de vencimiento, Tipo y Plazo en función de la vigencia de la póliza
	If tcdcontracdat = eRemoteDB.Constants.dtmNull Or _
	   Session("nTransaction") = 1 Or _
	   Session("nTransaction") = 3 Or _
	   Session("nTransaction") = 18 Then
            
        tcdcontracdat = Session("dStartdate")

        lclsCertificat = New ePolicy.Certificat
        lclsCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"))
            
'+ Duración de la poliza indicada en días
        If lclsCertificat.nDuration <= 0 And lclsCertificat.dExpirdat <> eRemoteDB.Constants.dtmNull Then
            valtime_unit = 1
            tcntime_eject = System.Math.Abs(DateDiff(Microsoft.VisualBasic.DateInterval.Day, lclsCertificat.dExpirdat, lclsCertificat.dStartdate))
                
'+ Duración de la poliza indicada en meses
        Else
            valtime_unit = 2
            tcntime_eject = lclsCertificat.nDuration
        End If
        tcdterm_date = Session("dExpirdat")  
    End If
        
	If tcncredcau = eRemoteDB.Constants.intNull Then
		tcncredcau = 0
	End If
	
    lclsCertificat = Nothing
        
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("CC001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "CC001"

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%> 

<script  type="text/javascript">
//-------------------------------------------------------------------------------------------
function insValueItem(sItem){
	
	with(self.document.forms[0]){
		
		switch (sItem.name) {
			
			case "valinsmodality":
			
				if(valinsmodality.value==0)
				{
					sItem.value = 1;
					
					tcnguar_type.value = 0;
					tcnguar_type.disabled = true;
					self.document.fraGrid.document.location.href = '/VTimeNet/Common/Blank.htm';
				}   
				else 
				{						
					tcnguar_type.disabled = (valinsmodality.value==2)?false:true;
					tcnguar_type.value = (valinsmodality.value==2)?1:0;
					self.document.fraGrid.document.location.href = (valinsmodality.value==2)?'CC001Frame.aspx?sCodispl=CC001&sCodisp=CC001&nMainAction=<%=Request.QueryString.Item("nMainAction")%>' +  "&valinsmodality=" + self.document.forms[0].valinsmodality.value +  "&tcnguar_type=" + self.document.forms[0].tcnguar_type.value +  "&tctcontracnum=" + self.document.forms[0].tctcontracnum.value  +  "&tcdcontracdat=" + self.document.forms[0].tcdcontracdat.value +  "&tcntime_eject=" + self.document.forms[0].tcntime_eject.value +  "&tcncredcau=" + self.document.forms[0].tcncredcau.value +  "&cbeCurrency=" + self.document.forms[0].cbeCurrency.value +  "&tcnindemper=" + self.document.forms[0].tcnindemper.value +  "&tcnmoraallow=" + self.document.forms[0].tcnmoraallow.value +  "&tcntransmon1=" + self.document.forms[0].tcntransmon1.value +  "&tcntransmon2=" + self.document.forms[0].tcntransmon2.value +  "&tcnindper1=" + self.document.forms[0].tcnindper1.value +  "&tcnindper2=" + self.document.forms[0].tcnindper2.value:'/VTimeNet/Common/Blank.htm';
					
     			}
			break;
			
			case "tcnguar_type":
			
				if(tcnguar_type.value==0)
				{
					tcnguar_type.value = 1;
												
				}   
			break;
			
			
			case "valtime_unit":
				
				tcntime_eject.value ='';
							
			break;
			
			case "tcntime_eject":
			
				if(tcdcontracdat.value!=null & valtime_unit.value!='' & tcntime_eject.value!='' & tcdterm_date.value=='')
				{
					insDefValues('DateAdd','ntime_unit=' + document.forms[0].valtime_unit.value + '&ntime_eject=' + document.forms[0].tcntime_eject.value + '&dcontracdat=' + tcdcontracdat.value);	                	    	
				}
			
			break;
			case "tcnindper1":
							
				if (insConvertNumber(tcnindper1.value,5,2,true) > 0 
				    && insConvertNumber(tcnindper1.value,5,2,true) < 100)
				{
					tcnindper2.disabled = false;
					tcntransmon2.disabled = false;
				}
				else
				{
					tcnindper2.disabled = true;
					tcntransmon2.disabled = true;
					tcnindper2.value = 0;
					tcntransmon2.value = 0;
				}
				
			break;
			
		}
	
	}
}

//% ShowPages: Llama a la ventana de Datos de verificación del recibo (SCO001)
//-------------------------------------------------------------------------------------------
function ShowPage() {
    //-------------------------------------------------------------------------------------------
    //+ Variable lstrLocation: Se usa para armar el QueryString que va a recibir la ventana
    //+ SCO001 para poder realizar la búsqueda de los datos de verificación del recibo - ACM - 26/06/2001
    //if (self.document.forms[0].elements["tcnReceiptNum"].value > 0) {
        //var lstrLocation = "";

        //lstrLocation = lstrLocation + "&nPolicy=" + Session("nPolicy");

        //+ Se hace el llamado a la ventana SCO001
        ShowPopUp("/VTimeNet/Common/CCC703.aspx?sCodispl=CCC703", "", 700, 400, "yes", false, 20, 20)
    //}
}   
//-------------------------------------------------------------------------------------------
</script>

<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<html>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<script>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</script>")
	
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.setZone(2, "CC001", "CC001.aspx", CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
<FORM METHOD="POST"	ID="FORM" NAME="frmCC001" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

Call insPreCC001()
Call DefaultValues()

%>    
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=17605><%= GetLocalResourceObject("valinsmodalityCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valinsmodality", "TABLE6900", eFunctions.Values.eValuesType.clngComboType, valinsmodality,  ,  ,  ,  ,  , "insValueItem(this);", False, 4, GetLocalResourceObject("valinsmodalityToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
             <TD><LABEL ID=LABEL4><%= GetLocalResourceObject("chksInsurSectorCaption") %></LABEL></TD>
               <TD><%= mobjValues.CheckControl("chksInsurSector", "", chksInsurSector, , "", , , GetLocalResourceObject("chksInsurSectorToolTip"))%></TD>
           
            <td colspan="2"><%=mobjValues.HiddenControl("tcnguar_type", eRemoteDB.intNull) %>  </td>
<%--            <TD><LABEL ID=17606><%= GetLocalResourceObject("tcnguar_typeCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("tcnguar_type", "TABLE186", eFunctions.Values.eValuesType.clngComboType, tcnguar_type,  ,  ,  ,  ,  , "insValueItem(this);", True, 4, GetLocalResourceObject("tcnguar_typeToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>            --%>
        </TR>
        
        <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=17607><A NAME="Contrato"><%= GetLocalResourceObject("AnchorContratoCaption") %></A></LABEL></TD>
                    
        </TR>
        <TR>
		    <TD COLSPAN="5" CLASS="Horline"></TD>
		    <TD></TD>
		</TR>
        <TR>
            <TD><LABEL ID=17608><%= GetLocalResourceObject("tctcontracnumCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctcontracnum", 50, tctcontracnum, False, GetLocalResourceObject("tctcontracnumToolTip"),  ,  ,  ,  , False,,60)%></TD>
            <TD COLSPAN="3" CLASS="HighLighted"><LABEL ID=17609><A NAME="Tiempo de ejecución"><%= GetLocalResourceObject("AnchorTiempo de ejecuciónCaption") %></A></LABEL></TD>
            
        </TR>
         <TR>         
		 <TD colspan="3"></TD>
         <TD CLASS="Horline" COLSPAN="2"></TD>		 
		 </TR>  
        <TR>
            <TD><LABEL ID=17610><%= GetLocalResourceObject("tcdcontracdatCaption") %></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdcontracdat", tcdcontracdat, False, GetLocalResourceObject("tcdcontracdatToolTip"), , , , , True)%></TD>
            <TD>&nbsp;</TD>
               
            <TD><LABEL ID=17611><%= GetLocalResourceObject("valtime_unitCaption") %></LABEL></TD>
            <TD><%= mobjValues.PossiblesValues("valtime_unit", "TABLE93", eFunctions.Values.eValuesType.clngWindowType, CStr(valtime_unit), , , , , , "insValueItem(this);", True, 4, GetLocalResourceObject("valtime_unitToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>
            
        </TR>
        <TR>
            <TD><LABEL ID=17612><%= GetLocalResourceObject("tcdterm_dateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdterm_date", CStr(tcdterm_date), False, GetLocalResourceObject("tcdterm_dateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=17613><%= GetLocalResourceObject("tcntime_ejectCaption") %></LABEL></TD>
            <TD><%= mobjValues.NumericControl("tcntime_eject", 6, tcntime_eject, False, GetLocalResourceObject("tcntime_ejectToolTip"), , , , , , "insValueItem(this);", True)%></TD>            
        </TR>
        <TR>
         <TD COLSPAN="5" CLASS="Horline"></TD>
		 <TD></TD>
		 </TR>
        <TR>
            <TD><LABEL ID=17614><%= GetLocalResourceObject("tcncredcauCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcncredcau", 18, tcncredcau, False, GetLocalResourceObject("tcncredcauToolTip"),  True , 6,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            	<TD><LABEL ID=17615><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
 				<%With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With%>                  
				<TD><%mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbeCurrency", "TabCurren_pol", 1, cbeCurrency, True, False,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeCurrencyToolTip")))%>
				</TD>
            </TR>
            <TR>
            <TD><LABEL ID=LABEL3></LABEL></TD>
            <TD><%= mobjValues.HiddenControl("valStatusbond", "0")%></TD>
            <TD COLSPAN="2">
              <LABEL ID=LABEL1><%= GetLocalResourceObject("chksFollowUpCaption") %></LABEL>
            </TD>
            <TD>
                <%= mobjValues.CheckControl("chksFollowUp","", chksfollowup,  ,  "" , , ,  GetLocalResourceObject("chksFollowUpToolTip"))%>
            </TD>
        </TR>
         <TR>
            <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=17616><A NAME="Siniestros"><%= GetLocalResourceObject("AnchorSiniestrosCaption") %></A></LABEL></TD>                    
        </TR>
        <TR>
		    <TD COLSPAN="5" CLASS="Horline"></TD>
		    <TD></TD>
		</TR>
        <TR>
            <TD><LABEL ID=17617><%= GetLocalResourceObject("tcnindemperCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnindemper", 5, tcnindemper, False, GetLocalResourceObject("tcnindemperToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD>&nbsp;</TD>
            <TD CLASS="HighLighted"><LABEL ID=17618><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD> 
        </TR>
        <TR>         
			<TD colspan="3"></TD>
			<TD CLASS="Horline" COLSPAN="2">
            </TD>		 
		</TR>  
        <TR>
            <TD><LABEL ID=17619><%= GetLocalResourceObject("tcnmoraallowCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnmoraallow", 6, tcnmoraallow, False, GetLocalResourceObject("tcnmoraallowToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
            <TD>&nbsp;</TD>
            <TD><LABEL ID=17620><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
            <TD><LABEL ID=17621><%= GetLocalResourceObject("tcntransmon1Caption") %></LABEL></TD>
        </TR>
        <TR>            
            <TD COLSPAN = "3"></TD>
            <TD><%=mobjValues.NumericControl("tcntransmon1", 4, tcntransmon1, False, GetLocalResourceObject("tcntransmon1ToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
            <TD><%=mobjValues.NumericControl("tcnindper1", 5, tcnindper1, False, GetLocalResourceObject("tcnindper1ToolTip"),  , 2,  ,  ,  , "insValueItem(this);", False)%></TD>
            
        </TR>
        <TR>
            <TD  ><LABEL ID=0><%=GetLocalResourceObject("btnShowCCC703Caption") %></LABEL></TD>
            <TD><%=mobjValues.AnimatedButtonControl("btnShowCCC703", "/VTimeNet/Images/btn_ValuesOff.png", GetLocalResourceObject("btnShowCCC703ToolTip"), , "ShowPage();")%></TD>
            <TD>&nbsp;</TD>
            <TD><%=mobjValues.NumericControl("tcntransmon2", 4, tcntransmon2, False, GetLocalResourceObject("tcntransmon2ToolTip"),  ,  ,  ,  ,  ,  , False)%></TD>
            <TD><%=mobjValues.NumericControl("tcnindper2", 5, tcnindper2, False, GetLocalResourceObject("tcnindper2ToolTip"),  , 2,  ,  ,  ,  , False)%></TD>    
            <td></td>
        </TR>
        <TR>
            <td> <LABEL ID=LABEL2><%=GetLocalResourceObject("tctContractObjectCaption") %></LABEL></td>
            <td colspan="5">
                <%=mobjValues.TextAreaControl("tctContractObject", 3, 60,tctContractObject,, GetLocalResourceObject("tctContractObjectTooltip"))  %>
            </td>
        </TR>
        
  </TABLE>
  
  <%If valinsmodality = 2 Then%>
		<IFRAME NAME="fraGrid" src='CC001Frame.aspx?sCodispl=CC001&sCodisp=CC001&nMainAction=<%=Request.QueryString.Item("nMainAction")%>'; WIDTH="100%" HEIGHT="40%" SCROLLING=AUTO FRAMEBORDER="0" TARGET='fraGeneric'>
		</IFRAME>
  <%Else%>
  		<IFRAME NAME="fraGrid" src='/VTimeNet/Common/Blank.htm'; WIDTH="100%" HEIGHT="40%" SCROLLING=AUTO FRAMEBORDER="0" TARGET='fraGeneric'>
		</IFRAME>
  <%End If%>
	
<%mobjValues = Nothing%>

</FORM>
</body>
</html>
<%
lclsCredit = Nothing


%>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("CC001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









