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
    Dim ngroup As Integer
    Dim nsituation As Integer
    Dim nparticularclas As Integer
    Dim sname As String
    Dim sbrand As String
    Dim smodel As String
    Dim sseries As String
    Dim nyear As String
    Dim sorigin As String
    Dim sregistrationnumber As String
    Dim scapacity As String
    Dim ntakeoff_maxwei As Integer
    Dim sairportbase As String
    Dim sgeographical As String
    Dim nuse As Integer
    Dim snavigationcertificate As String
    Dim nqualificationship As Integer
    Dim sportdeparture As String
    Dim sportarrival As String
    Dim sdimensions As String
    Dim saddicionaltext As String
    Dim nseatnumber As Integer
    Dim ncrewnumber As Integer
    Dim npassengersnumber As Integer
    Dim nnibranumber As Integer
    Dim ncapital As Integer

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

    Dim lclsAviat_marit As ePolicy.Aviat_marit

    '%insPreAV001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
    Private Sub insPreAV001()
        '------------------------------------------------------------------------------
	
        Dim lcolAviat_marits As ePolicy.Aviat_marits
        With Request
            lcolAviat_marits = New ePolicy.Aviat_marits
            Call lcolAviat_marits.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
            If lcolAviat_marits.Count > 0 Then
                lclsAviat_marit = lcolAviat_marits.Item(1)
			
                ''UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                'If IsNothing(Session("nEnter")) Then
                '    Call lclsAviat_marit.insPreCC001(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), Session("nUsercode"), Session("nTransaction"))
                '    Session("nEnter") = 1
                'End If
            Else
                lclsAviat_marit = New ePolicy.Aviat_marit
            End If
		
        End With
        lcolAviat_marits = Nothing
	
    End Sub

'% DefaultValues: Se realiza el manejo de los valores de los campos cuando se carga o recarga la página
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub DefaultValues()
	'------------------------------------------------------------------------------------------------------------------------------------------------------       
        With lclsAviat_marit
            ngroup = .ngroup
            nsituation = .nsituation
            nparticularclas = .nparticularclas
            sname = .sname
            sbrand = .sbrand
            smodel = .smodel
            sseries = .sseries
            nyear = .nyear
            sorigin = .sorigin
            sregistrationnumber = .sregistrationnumber
            scapacity = .scapacity
            ntakeoff_maxwei = .ntakeoff_maxwei
            sairportbase = .sairportbase
            sgeographical = .sgeographical
            nuse = .nuse
            snavigationcertificate = .snavigationcertificate
            nqualificationship = .nqualificationship
            sportdeparture = .sportdeparture
            sportarrival = .sportarrival
            sdimensions = .sdimensions
            saddicionaltext = .saddicionaltext
            nseatnumber = .nseatnumber
            ncrewnumber = .ncrewnumber
            npassengersnumber = .npassengersnumber
            nnibranumber = .nnibranumber
            ncapital = .ncapital
        End With
			        
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
    Call mobjNetFrameWork.BeginPage("AV001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "AV001"

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%> 
<script>
// ShowYear: Muestra el año completo (4 digitos)
//-------------------------------------------------------------------------------------------
function ShowYear(){
//-------------------------------------------------------------------------------------------
var d = new Date();
	with (self.document.forms[0]) {
	    tcnYear.value = getCompleteYear(tcnYear.value)
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
        .Write(mobjMenu.setZone(2, "AV001", "AV001.aspx", CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
<FORM METHOD="POST"	ID="FORM" NAME="frmAV001" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

    Call insPreAV001()
    Call DefaultValues()

%>    
    <TABLE WIDTH="100%">       
        <TR>
            <TD colspan="2" CLASS="HighLighted"><LABEL ID=17607><%= GetLocalResourceObject("AnchorColectivoCaption")%></LABEL></TD>         
            <td width="10%">&nbsp;</td>
            <TD colspan="2" CLASS="HighLighted"><LABEL ID=LABEL1><%= GetLocalResourceObject("AnchorInfbasicaCaption")%></LABEL></TD>
        </TR>
        <TR>
		    <TD colspan="2" CLASS="Horline"></TD>
            <TD></TD>
		    <TD colspan="2" CLASS="Horline"></TD>
		</TR>
        <TR>
            <TD><LABEL ID=LABEL2><%= GetLocalResourceObject("cbovalGroupCaption")%></LABEL></td>
            <%
                With mobjValues.Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <td>
            <%If CStr(Session("sPolitype")) = "1" Then
                    Response.Write(mobjValues.PossiblesValues("cbovalGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, CStr(ngroup), , , , , , , True, , GetLocalResourceObject("cbovalGroupTooltip")))
              Else 
                    Response.Write(mobjValues.PossiblesValues("cbovalGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, CStr(ngroup), , , , , , , False, , GetLocalResourceObject("cbovalGroupTooltip")))
              End If%>
            </TD>
            <td></td>
            <td><LABEL ID=LABEL3><%= GetLocalResourceObject("cbeParticularclasCaption")%></LABEL></td>
            <td><%= mobjValues.PossiblesValues("cbeParticular", "Table5623", eFunctions.Values.eValuesType.clngComboType, nparticularclas, , , , , , , False, , GetLocalResourceObject("cbeParticularToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></td>
        </TR>
        <tr>
            <TD><LABEL ID=LABEL4><%= GetLocalResourceObject("cbovalSituationCaption")%></LABEL></td>
            <% 
                With mobjValues.Parameters
                    .Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                End With
            %>
            <td>
            <%  If CStr(Session("sPolitype")) = "1" Then
                    Response.Write(mobjValues.PossiblesValues("cbovalSituation", "tabSituation", 2, CStr(nsituation), , , , , , , True, , GetLocalResourceObject("cbovalSituationToolTip")))
                Else
                    Response.Write(mobjValues.PossiblesValues("cbovalSituation", "tabSituation", 2, CStr(nsituation), , , , , , , False, , GetLocalResourceObject("cbovalSituationToolTip")))
                End If%>
            </td>                                
        </tr>
        <TR>
            <TD colspan="5" CLASS="HighLighted"><LABEL ID=LABEL5><%= GetLocalResourceObject("AnchorDescripcionCaption")%></LABEL></TD>         
        </TR>
        <TR>
		    <TD colspan="5" CLASS="Horline"></TD>
		</TR>
        <tr>
            <TD><LABEL ID=LABEL6><%= GetLocalResourceObject("tctModelCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctModel", 50, smodel, False, GetLocalResourceObject("tctModelTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL7><%= GetLocalResourceObject("tctBrandCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctBrand", 50, sbrand, False, GetLocalResourceObject("tctBrandTooltip"), , , , , False, , 60)%></TD>                                  
        </tr>
        <tr>
            <TD><LABEL ID=LABEL8><%= GetLocalResourceObject("tctYearCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnYear", 4, nyear, False, GetLocalResourceObject("tctYearTooltip"), , , , , , "ShowYear();", False, , )%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL9><%= GetLocalResourceObject("tctSeriesCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctSeries", 50, sseries, False, GetLocalResourceObject("tctSeriesTooltip"), , , , , False, , 60)%></TD>          
        </tr>
        <tr>
            <TD><LABEL ID=LABEL10><%= GetLocalResourceObject("tctRegistrationnumberCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctRegistrationnumber", 50, sregistrationnumber, False, GetLocalResourceObject("tctRegistrationnumberTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL11><%= GetLocalResourceObject("tctOriginCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctOrigin", 50, sorigin, False, GetLocalResourceObject("tctOriginTooltip"), , , , , False, , 60)%></TD>          
        </tr>
        <tr>
            <TD><LABEL ID=LABEL12><%= GetLocalResourceObject("tcnTakeoff_maxweiCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnTakeoff_maxwei", 18, ntakeoff_maxwei, True, GetLocalResourceObject("tcnTakeoff_maxweiTooltip"), True, 0, , , , , False)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL13><%= GetLocalResourceObject("tctAirportbaseCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctAirportbase", 50, sairportbase, False, GetLocalResourceObject("tctAirportbaseTooltip"), , , , , False, , 60)%></TD>          
        </tr>
        <tr>
            <TD><LABEL ID=LABEL14><%= GetLocalResourceObject("tctGeographicalCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctGeographical", 50, sgeographical, False, GetLocalResourceObject("tctGeographicalTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL15><%= GetLocalResourceObject("cbeUseCaption")%></LABEL></td>
            <TD><%= mobjValues.PossiblesValues("cbeUse", "Table5624", eFunctions.Values.eValuesType.clngComboType, nuse, , , , , , , False, , GetLocalResourceObject("cbeUseToolTip"), eFunctions.Values.eTypeCode.eNumeric)%></TD>          
        </tr>
        <tr>
            <TD><LABEL ID=LABEL16><%= GetLocalResourceObject("tcnCapitalCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnCapital", 18, ncapital, True, GetLocalResourceObject("tcnCapitalTooltip"), True, 6, , , False, , False)%></TD>      
        </tr>
        <TR>
            <TD colspan="5" CLASS="HighLighted"><LABEL ID=LABEL18><%= GetLocalResourceObject("AnchorCapacidadCaption")%></LABEL></TD>         
        </TR>
        <TR>
		    <TD colspan="5" CLASS="Horline"></TD>
		</TR>
        <tr>
            <TD><LABEL ID=LABEL19><%= GetLocalResourceObject("tcnSeatnumberCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnSeatnumber", 5, nseatnumber, True, GetLocalResourceObject("tcnSeatnumberTooltip"), , , , , False, , False)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL20><%= GetLocalResourceObject("tcnCrewnumberCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnCrewnumber", 5, ncrewnumber, True, GetLocalResourceObject("tcnCrewnumberTooltip"), , , , , False, , False)%></TD>
        <tr/>
        <tr>
            <TD><LABEL ID=LABEL21><%= GetLocalResourceObject("tcnPassengersnumberCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnPassengersnumber", 5, npassengersnumber, True, GetLocalResourceObject("tcnPassengersnumberTooltip"), , , , , False, , False)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL22><%= GetLocalResourceObject("tcnNibranumberCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnNibranumber", 5, nnibranumber, True, GetLocalResourceObject("tcnNibranumberTooltip"), , , , , False, , False)%></TD>
        <tr/>
        <TR>
            <td> <LABEL ID=LABEL17><%= GetLocalResourceObject("tctAddicionaltextCaption")%></LABEL></td>
            <td colspan="5">                
                <%= mobjValues.TextAreaControl("tctAddicionaltext", 5, 100, saddicionaltext, , GetLocalResourceObject("tctAddicionaltextTooltip"))%>
            </td>
        </TR>        
  </TABLE>
  	
<%mobjValues = Nothing%>

</FORM>
</body>
</html>
<%
    lclsAviat_marit = Nothing


%>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
    Call mobjNetFrameWork.FinishPage("AV001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









