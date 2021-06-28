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

    '%insPreSH010. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
    Private Sub insPreSH010()
        '------------------------------------------------------------------------------
	
        Dim lcolAviat_marits As ePolicy.Aviat_marits
        With Request
            lcolAviat_marits = New ePolicy.Aviat_marits
            Call lcolAviat_marits.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
            If lcolAviat_marits.Count > 0 Then
                lclsAviat_marit = lcolAviat_marits.Item(1)
			
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
    Call mobjNetFrameWork.BeginPage("SH010")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "SH010"

mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
%> 

<script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<html>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<script>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</script>")
	
	mobjMenu = New eFunctions.Menues
        .Write(mobjMenu.setZone(2, "SH010", "SH010.aspx", CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
<FORM METHOD="POST"	ID="FORM" NAME="frmSH010" ACTION="valpolicyseq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

    Call insPreSH010()
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
            <TD><LABEL ID=LABEL23><%= GetLocalResourceObject("tctNameCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctName", 50, sname, False, GetLocalResourceObject("tctNameTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL24><%= GetLocalResourceObject("tctBrandCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctBrand", 50, sbrand, False, GetLocalResourceObject("tctBrandTooltip"), , , , , False, , 60)%></TD>                                  
        </tr>
        <tr>
            <TD><LABEL ID=LABEL6><%= GetLocalResourceObject("tctModelCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctModel", 50, smodel, False, GetLocalResourceObject("tctModelTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL7><%= GetLocalResourceObject("tctSeriesCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctSeries", 50, sseries, False, GetLocalResourceObject("tctSeriesTooltip"), , , , , False, , 60)%></TD>                                     
        </tr>

        <tr>
            <TD><LABEL ID=LABEL8><%= GetLocalResourceObject("tctYearCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tctYear", 4, nyear, False,  GetLocalResourceObject("tctYearTooltip"), , , , , , , False)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL9><%= GetLocalResourceObject("tctOriginCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctOrigin", 50, sorigin, False, GetLocalResourceObject("tctOriginTooltip"), , , , , False, , 60)%></TD>             
        </tr>
        <tr>
            <TD><LABEL ID=LABEL10><%= GetLocalResourceObject("tctRegistrationnumberCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctRegistrationnumber", 50, sregistrationnumber, False, GetLocalResourceObject("tctRegistrationnumberTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL11><%= GetLocalResourceObject("tctNavigationcertificateCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctNavigationcertificate", 50, snavigationcertificate, False, GetLocalResourceObject("tctNavigationcertificateTooltip"), , , , , False, , 60)%></TD>          
        </tr>
        <tr>
            <TD><LABEL ID=LABEL12><%= GetLocalResourceObject("tcnQualificationshipCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnQualificationship", 50, nqualificationship, True, GetLocalResourceObject("tcnQualificationshipTooltip"), , , , , False, , False)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL13><%= GetLocalResourceObject("tctPortdepartureCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctPortdeparture", 50, sportdeparture, False, GetLocalResourceObject("tctPortdepartureTooltip"), , , , , False, , 60)%></TD>          
        </tr>
        <tr>
            <TD><LABEL ID=LABEL14><%= GetLocalResourceObject("tctPortarrivalCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctPortarrival", 50, sportarrival, False, GetLocalResourceObject("tctPortarrivalTooltip"), , , , , False, , 60)%></TD>   
            <td></td>
            <TD><LABEL ID=LABEL15><%= GetLocalResourceObject("tctDimensionsCaption")%></LABEL></td>
            <TD><%= mobjValues.TextControl("tctDimensions", 50, sdimensions, False, GetLocalResourceObject("tctDimensionsTooltip"), , , , , False, , 60)%></TD>           
        </tr>
        <tr>
            <TD><LABEL ID=LABEL16><%= GetLocalResourceObject("tcnCapitalCaption")%></LABEL></td>
            <TD><%= mobjValues.NumericControl("tcnCapital", 50, ncapital, True, GetLocalResourceObject("tcnCapitalTooltip"), , , , , False, , False)%></TD>      
        </tr>
        <TR>
            <td> <LABEL ID=LABEL17><%= GetLocalResourceObject("tctAddicionaltextCaption")%></LABEL></td>
            <td colspan="5">
                <%= mobjValues.TextAreaControl("tctAddicionaltext", 3, 60, saddicionaltext, , GetLocalResourceObject("tctAddicionaltextTooltip"))%>
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
    Call mobjNetFrameWork.FinishPage("SH010")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>









