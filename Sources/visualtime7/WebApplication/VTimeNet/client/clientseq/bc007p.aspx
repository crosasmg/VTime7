<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
    Private mobjClient As eClient.Client
    Dim BchkUSPERSON As Boolean
    Dim BchkPEP As Boolean
    Dim BchkCRS As Boolean



    '- Objeto para el manejo de los datos del cliente	
    Dim mobjclient_PEP As eClient.Client_SF

    Dim mobjFiscalResidences As eClient.Fiscal_Residences

    Private Sub insReloadVar()

        With Response
            .Write(mobjValues.HiddenControl("hddsPlaceOfBirth", Request.QueryString.Item("sPlace_birth")))
            .Write(mobjValues.HiddenControl("hddnPlaceOfBirth", Request.QueryString.Item("nPlace_birth")))
            .Write(mobjValues.HiddenControl("hddnPosition", Request.QueryString.Item("nPosition")))
            .Write(mobjValues.HiddenControl("hdddStartcondition", Request.QueryString.Item("dStartcondition")))
            .Write(mobjValues.HiddenControl("hdddEndcondition", Request.QueryString.Item("dEndcondition")))
            .Write(mobjValues.HiddenControl("hddnResident_former", Request.QueryString.Item("nResident_former")))
            .Write(mobjValues.HiddenControl("hddnSecond_nationality", Request.QueryString.Item("nSecond_nationality")))
            .Write(mobjValues.HiddenControl("hddsUsAdress", Request.QueryString.Item("sUsAdress")))
            .Write(mobjValues.HiddenControl("hddsSSN", Request.QueryString.Item("sSSN")))
            .Write(mobjValues.HiddenControl("hddsUsLegal_person", Request.QueryString.Item("sUsLegal_person")))
            .Write(mobjValues.HiddenControl("hddsUsphone", Request.QueryString.Item("sUsphone")))
            .Write(mobjValues.HiddenControl("hddsUsAccount", Request.QueryString.Item("sUsAccount")))
            .Write(mobjValues.HiddenControl("hddsUsIrsind", Request.QueryString.Item("sUsIrsind")))
        End With

    End Sub

    '%insDefineHeader. Se definen las columnas del grid
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------

        mobjGrid.sCodisplPage = "BC007P"

        With mobjGrid.Columns
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeCountryColumnCaption"), "cbeCountry", "Table66", 1, , , , , , , BchkCRS)
            .AddTextColumn(0, GetLocalResourceObject("tctsJurisdictionColumnCaption"), "tctsJurisdiction", 30, vbNullString, True, GetLocalResourceObject("tctsJurisdictionColumnToolTip"),,,, BchkCRS)
            .AddTextColumn(0, GetLocalResourceObject("tctsus_ItinnumColumnCaption"), "tctsus_Itinnum", 12, vbNullString, True, GetLocalResourceObject("tctPhoneColumnToolTip"),,,, BchkCRS)
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeNmotive_ItinColumnCaption"), "cbeNmotive_Itin", "Table5516", 1, , , , , , , BchkCRS)

        End With

        With mobjGrid
            .ActionQuery = Session("bQuery")
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "BC007P"
            .Height = 300
            .Width = 400
            If Request.QueryString.Item("nMainAction") = "undefined" Then
                .nMainAction = 0
            Else
                .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            End If

            .Columns("cbeCountry").EditRecord = True
            .DeleteButton = True
            .AddButton = True
            '.sEditRecordParam = "sclient=" & Session("sClient")
            '            .sDelRecordParam = "sclient=" & Session("sClient") & "&nCountry=49" ' & Session("npais")

        End With
    End Sub

    '%insPreSCA001Upd: Muestra ventana para editar registros de grilla
    '----------------------------------------------------------------------------------    
    Private Sub insPreBC007PUpd()
        '------------------------------------------------------------------------------------
        With Response
            .Write(mobjValues.HiddenControl("hddsPlaceOfBirth", Request.QueryString.Item("sPlace_birth")))
            .Write(mobjValues.HiddenControl("hddnPlaceOfBirth", Request.QueryString.Item("nPlace_birth")))
            .Write(mobjValues.HiddenControl("hddnPosition", Request.QueryString.Item("nPosition")))
            .Write(mobjValues.HiddenControl("hdddStartcondition", Request.QueryString.Item("dStartcondition")))
            .Write(mobjValues.HiddenControl("hdddEndcondition", Request.QueryString.Item("dEndcondition")))
            .Write(mobjValues.HiddenControl("hddnResident_former", Request.QueryString.Item("nResident_former")))
            .Write(mobjValues.HiddenControl("hddnSecond_nationality", Request.QueryString.Item("nSecond_nationality")))
            .Write(mobjValues.HiddenControl("hddsUsAdress", Request.QueryString.Item("sUsAdress")))
            .Write(mobjValues.HiddenControl("hddsSSN", Request.QueryString.Item("sSSN")))
            .Write(mobjValues.HiddenControl("hddsUsLegal_person", Request.QueryString.Item("sUsLegal_person")))
            .Write(mobjValues.HiddenControl("hddsUsphone", Request.QueryString.Item("sUsphone")))
            .Write(mobjValues.HiddenControl("hddsUsAccount", Request.QueryString.Item("sUsAccount")))
            .Write(mobjValues.HiddenControl("hddsUsIrsind", Request.QueryString.Item("sUsIrsind")))

            .Write(mobjValues.HiddenControl("hddsClient", Request.QueryString.Item("Sclient")))
            .Write(mobjValues.HiddenControl("hddnCountry", Request.QueryString.Item("nCountry")))
            .Write(mobjValues.HiddenControl("hdddEffecdate", Session("dEffecdate")))
            .Write(mobjValues.HiddenControl("hddsUs_Itinnum", Request.QueryString.Item("sUsItinnum")))
            .Write(mobjValues.HiddenControl("hddnMotive_itin", Request.QueryString.Item("nMotive_itin")))
            .Write(mobjValues.HiddenControl("hddsjurisdiction", Request.QueryString.Item("sJurisdiction")))
            .Write(mobjValues.HiddenControl("hdddNulldate", Request.QueryString.Item("dNullate")))
            .Write(mobjValues.HiddenControl("hddnUsercode", Request.QueryString.Item("nUsercode")))
            .Write(mobjValues.HiddenControl("hddDCompdate", Request.QueryString.Item("dCompdate")))

            If Request.QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete)
                Dim lobdFiscal_residence As Fiscal_Residence
                lobdFiscal_residence = New eClient.Fiscal_Residence
                Call lobdFiscal_residence.DelFiscal_Residence(Request.QueryString.Item("Sclient"), mobjValues.StringToType(Request.QueryString.Item("nCountry"), eFunctions.Values.eTypeData.etdDouble), Date.Now, Session("nUserCode"))
            End If

            .Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valClientSeq.aspx", "BC007P", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
            '.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valClientSeq.aspx", "BC007P", Request.QueryString.Item("nMainAction"), Session("bQuery")))
        End With

    End Sub

    '%insPreSi007M. Esta funcion se encarga deralizar la busqueda de los datos de cliente
    '------------------------------------------------------------------------------------
    Private Sub insPreSi007P()
        '------------------------------------------------------------------------------------
        mobjclient_PEP = New eClient.Client_SF
        mobjClient = New eClient.Client
        mobjFiscalResidences = New eClient.Fiscal_Residences
        mobjclient_PEP.Find(Session("sClient"), Today)
        mobjClient.Find(Session("sClient"))
        mobjFiscalResidences.Find(Session("sClient"), Today)


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mobjclient_PEP = New eClient.Client_SF
    mobjGrid = New eFunctions.Grid

    Dim show As Boolean
    Dim hide As Boolean


    If CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401 Then
        mobjValues.ActionQuery = True
    End If

    With mobjclient_PEP

        If .Find(Session("sClient"), Today) Then
        End If
    End With
    Response.Write("" & vbCrLf)
    Response.Write("<SCRIPT>" & vbCrLf)
    Response.Write("	var nMainAction ='")
    Response.Write(Request.QueryString.Item("nMainAction"))
    Response.Write("'" & vbCrLf)
    Response.Write("" & vbCrLf)
    Response.Write("</SCRIPT>" & vbCrLf)

    '+Se realiza el llamado a la funcion insPreSi007M, para obtener los datos del cliente en tratamiento
    insPreSi007P()
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
     <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
     <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>

<%Response.Write(mobjMenu.setZone(2, "BC007P", "BC007P.aspx"))
    Response.Write(mobjValues.StyleSheet())
    mobjMenu = Nothing
    If Session("chkUSPERSON") = "2" Or Session("chkUSPERSON") Is Nothing Then
        BchkUSPERSON = True
    Else
        BchkUSPERSON = False
    End If
    If Session("chkPEP") = "" Then
        BchkPEP = True
    Else
        BchkPEP = False
    End If
    If Session("chkCRS") = "" Then
        BchkCRS = True
    Else
        BchkCRS = False
    End If


%>


<SCRIPT>
  
//+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 20/01/04 11:44 $"
		
//% CancelErrors: se controla la acción Cancelar 
//---------------------------------------------------------------------------------------------------
	function CancelErrors(){
//---------------------------------------------------------------------------------------------------
	self.history.back
}
	
//% insEnabledFields: Habilita o deshabilita los campos de la ventana, dependiendo si están
//%					  llenos o no. ACM - 31/07/2001.	
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        //+ Fecha de Ingreso
        if (elements["tcdInpDate"].value == "")
            elements["tcdInpDate"].disabled = false
        else
            elements["tcdInpDate"].disabled = true;

    }
}

</SCRIPT>

    <%
        mobjMenu = Nothing%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBC007P" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sPlace_birth=<%=Request.QueryString.Item("sPlace_birth")%>&nPlace_birth=<%=Request.QueryString.Item("nPlace_birth")%>&nPosition=<%=Request.QueryString.Item("nPosition")%>&dStartcondition=<%=Request.QueryString.Item("dStartcondition")%>&dEndcondition=<%=Request.QueryString.Item("dEndcondition")%>&nResident_former=<%=Request.QueryString.Item("nResident_former")%>&nSecond_nationality=<%=Request.QueryString.Item("nSecond_nationality")%>&sUsAdress=<%=Request.QueryString.Item("sUsAdress")%>&sSSN=<%=Request.QueryString.Item("sSSN")%>&sUsLegal_person=<%=Request.QueryString.Item("sUsLegal_person")%>&sUsphone=<%=Request.QueryString.Item("sUsphone")%>&sUsAccount=<%=Request.QueryString.Item("sUsAccount")%>&sUsIrsind=<%=Request.QueryString.Item("sUsIrsind")%>">
    <A NAME="BeginPage"></A>
  
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
    <br />

<%  
    Call insDefineHeader()
    If Request.QueryString.Item("Type") <> "PopUp" Then%>

	<TABLE WIDTH="100%">
	    
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("cbePEPINFORMATIONCaption")%></A></LABEL></TD>
            </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR> 
		<TR></TR><TR></TR><TR></TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeTypeOfPoliticalOfficeCaption")%></LABEL></TD>
            <TD WIDTH="18%"> <%=mobjValues.PossiblesValues("cbeTypeOfPoliticalOffice", "table8004", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_PEP.nPosition), , , , , , , BchkPEP, , GetLocalResourceObject("cbeTypeOfPoliticalOfficeToolTip"))%> </TD>
	    </TR>
	    <TR>
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcdGrantDateCaption")%></LABEL></TD>
	        <TD > <%=mobjValues.DateControl("tcdGrantDate", mobjValues.TypeToString(mobjclient_PEP.dStartcondition, eFunctions.Values.eTypeData.etdDate), , GetLocalResourceObject("tcdGrantDateToolTip"), , , , , BchkPEP)%> </TD>

<!--
	    </TR>
        <TR>
-->
	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("tcdEndDateCaption")%></LABEL></TD>
	        <TD WIDTH="25%"> <%=mobjValues.DateControl("tcdEndDate", mobjValues.TypeToString(mobjclient_PEP.dEndcondition, eFunctions.Values.eTypeData.etdDate), , GetLocalResourceObject("tcdEndDateToolTip"), , , , "ShowChangeValues()", BchkPEP)%> </TD>
	    </TR>
 
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><A NAME="Nombre"><%= GetLocalResourceObject("cbeFATCAINFORMATIONCaption")%></A></LABEL></TD>
        </TR>
	    <TR>	        
	        <TD WIDTH="100%" COLSPAN="4" CLASS="Horline"></TD>
	    </TR>
        <tr></tr><tr></tr><tr></tr>
        <TR>
            <td WIDTH="18%"><label><%= GetLocalResourceObject("txtPlaceOfBirthCaption")%></label></td>
            <td WIDTH="18%"> <%= mobjValues.TextControl("txtPlaceOfBirth", 30, mobjclient_PEP.sPlacebirth, True, GetLocalResourceObject("txtPlaceOfBirthToolTip"), , , , , BchkUSPERSON)%></td>

	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbePlaceOfBirthCaption")%></LABEL></TD>
            <TD WIDTH="18%"> <%= mobjValues.PossiblesValues("cbePlaceOfBirth", "table66", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_PEP.nPlacebirth), , , , , , , BchkUSPERSON, , GetLocalResourceObject("cbePlaceOfBirthToolTip"))%> </TD>
	   
	    </TR>
        <tr></tr><tr></tr>
        <TR>

	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeResidentFormerCaption")%></LABEL></TD>
            <TD WIDTH="18%"> <%= mobjValues.PossiblesValues("cbeResidentFormer", "table66", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_PEP.nResident_former), , , , , , , BchkUSPERSON, , GetLocalResourceObject("cbeResidentFormerToolTip"))%> </TD>

	        <TD WIDTH="18%"><LABEL><%= GetLocalResourceObject("cbeSecondNationalityCaption")%></LABEL></TD>
            <TD WIDTH="18%"> <%=mobjValues.PossiblesValues("cbeSecondNationality", "table5518", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_PEP.nSecond_nationality), , , , , , , BchkUSPERSON, , GetLocalResourceObject("cbeSecondNationalityToolTip"))%> </TD>
	    </TR>
     
        <tr>
        <td rowspan ="4"></td></tr>
        <tr><td ></td><td ></td><td ></td></tr>
        <tr> <td ></td ><td ></td><td ></td></tr>
        <tr> <td ></td><td ></td><td ></td></tr>
        
        <tr>
        <td  class="HighLighted"><label><a name="Nombre3"><%= GetLocalResourceObject("cbeDoNotTaxPayerCaption")%></a></label></td>
        <td COLSPAN="3" class="HighLighted"><label><a name="Nombre3"><%= GetLocalResourceObject("cbeTaxPayerCaption")%></a></label></td>           
	    </tr>   
	    <TR>	        
	        <TD WIDTH="25%" COLSPAN="1" CLASS="Horline"></TD><TD WIDTH="100%" COLSPAN="2" ></TD><TD WIDTH="25%" COLSPAN="1" CLASS="Horline"></TD>
	    </TR>   
        <tr></tr><tr></tr>      <tr></tr><tr></tr>  <tr></tr><tr></tr>  
		 <tr></tr><tr></tr>      <tr></tr><tr></tr>  <tr></tr><tr></tr> 
        <tr>
        <% 
            If mobjClient.sUsPerson = "1" Then
                show = True
                hide = False
            Else
                show = False
                hide = True
            End If

        %>
            <td><label><%= GetLocalResourceObject("txtAddressCaption")%></label></td>
            <td><%= mobjValues.TextControl("txtAddress", 30, mobjclient_PEP.sUsAdress, True, GetLocalResourceObject("txtAddressToolTip"), , show)%>
            </td>
            <td><label><%= GetLocalResourceObject("txtSSNCaption")%></label></td>
            <td><%= mobjValues.TextControl("txtSSN", 30, mobjclient_PEP.sSSN, True, GetLocalResourceObject("txtSSNToolTip"), , , , , hide)%>
            </td>
         </tr>
		<tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr>	 	

         <tr>
         <td><label><%= GetLocalResourceObject("txtUsLegalPersonCaption")%></label></td>
            <td><%= mobjValues.TextControl("txtUsLegalPerson", 30, mobjclient_PEP.sUsLegal_person, True, GetLocalResourceObject("txtUsLegalPersonToolTip"), , show)%>
<!--
            <td><label><%= GetLocalResourceObject("txtUsitinnumCaption")%></label></td> 
            <td><%= mobjValues.TextControl("txtUsitinnum", 30, mobjclient_PEP.sUsItinnum, True, GetLocalResourceObject("txtUsitinnumToolTip"), , , , , hide)%></td>
-->
           <td><label><%= GetLocalResourceObject("txtUsphoneCaption")%></label></td>
            <td><%= mobjValues.TextControl("txtUsphone", 30, mobjclient_PEP.sUsphone, True, GetLocalResourceObject("txtUsphoneToolTip"), , BchkCRS)%>

         </tr>

         <%= mobjValues.HiddenControl("txtUsitinnum", mobjclient_PEP.sUsItinnum)%>

		<tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> 
           <tr>

            <TD><LABEL><%= GetLocalResourceObject("txtUsAccountCaption")%></LABEL></TD>
            <td> <%= mobjValues.TextControl("txtUsAccount", 30, mobjclient_PEP.sUsAccount, True, GetLocalResourceObject("txtUsAccountToolTip"), , show)%>
           
            <td><%= mobjValues.CheckControl("chkUsirsind", GetLocalResourceObject("chkUsirsindCaption"), mobjclient_PEP.sUsIrsind, , , hide)%></td>
         </tr>
		 
		 <tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> 
          <tr>
            </tr>
		 <tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> <tr></tr><tr></tr> 
        <TR>
            <TD COLSPAN="3"></TD>
            <TD COLSPAN="1" CLASS="HighLighted"><LABEL><A NAME="Nombre4"><%= GetLocalResourceObject("cbeFRINFORMATIONCaption")%></A></LABEL></TD>
            </TR>
	    <TR>
            <TD COLSPAN="3"></TD>	        
	        <TD WIDTH="100%" COLSPAN="1" CLASS="Horline"></TD>
	    </TR> 
        <tr>
            <TD COLSPAN="2"></TD>	        
	        <TD WIDTH="100%" COLSPAN="2" ALIGN="LEFT" </TD>

<%  If Request.QueryString.Item("ReloadAction") = "Add" Or Request.QueryString.Item("ReloadAction") = "Update" Then
        Call insReloadVar()
        %>
	<script>
	    document.forms[0].txtPlaceOfBirth.value = document.forms[0].hddsPlaceOfBirth.value
	    document.forms[0].cbePlaceOfBirth.value = document.forms[0].hddnPlaceOfBirth.value
	    document.forms[0].cbeTypeOfPoliticalOffice.value = document.forms[0].hddnPosition.value
	    document.forms[0].tcdGrantDate.value = document.forms[0].hdddStartcondition.value
	    document.forms[0].tcdEndDate.value = document.forms[0].hdddEndcondition.value
	    document.forms[0].cbeResidentFormer.value = document.forms[0].hddnResident_former.value
	    document.forms[0].cbeSecondNationality.value = document.forms[0].hddnSecond_nationality.value
	    document.forms[0].txtAddress.value = document.forms[0].hddsUsAdress.value
	    document.forms[0].txtSSN.value = document.forms[0].hddsSSN.value
	    document.forms[0].txtUsLegalPerson.value = document.forms[0].hddsUsLegal_person.value
	    document.forms[0].txtUsphone.value = document.forms[0].hddsUsphone.value
	    document.forms[0].txtUsAccount.value = document.forms[0].hddsUsAccount.value
	    document.forms[0].chkUsirsind.value = document.forms[0].hddsUsIrsind.value

    </script>
    <%End If%>
<% Else
        Call insPreBC007PUpd()

    End If%>

<%

    If Request.QueryString.Item("Type") <> "PopUp" Then

        With mobjGrid
            .sEditRecordParam = "sclient=" & Session("sClient") & "&sPlace_birth=' + document.forms[0].txtPlaceOfBirth.value + '" & "&nPlace_birth=' + document.forms[0].cbePlaceOfBirth.value + '" & "&nPosition=' + document.forms[0].cbeTypeOfPoliticalOffice.value + '" & "&dStartcondition=' + document.forms[0].tcdGrantDate.value + '" & "&dEndcondition=' + document.forms[0].tcdEndDate.value + '" & "&nResident_former=' + document.forms[0].cbeResidentFormer.value + '" & "&nSecond_nationality=' + document.forms[0].cbeSecondNationality.value + '" & "&sUsAdress=' + document.forms[0].txtAddress.value + '" & "&sSSN=' + document.forms[0].txtSSN.value + '" & "&sUsLegal_person=' + document.forms[0].txtUsLegalPerson.value + '" & "&sUsphone=' + document.forms[0].txtUsphone.value + '" & "&sUsAccount=' + document.forms[0].txtUsAccount.value + '" & "&sUsIrsind=' + document.forms[0].chkUsirsind.value + '"
            '.sEditRecordParam = "sclient=" & Session("sClient") & "&sPlace_birth=' + document.forms[0].txtPlaceOfBirth.value + '" & "&nPlace_birth=' + document.forms[0].cbePlaceOfBirth.value + '" & "&nPosition=' + document.forms[0].cbeTypeOfPoliticalOffice.value + '"
            .sDelRecordParam = "sclient=" & Session("sClient") & "&nCountry=' + marrArray[lintIndex].cbeCountry + '" & "&sPlace_birth=' + document.forms[0].txtPlaceOfBirth.value + '" & "&nPlace_birth=' + document.forms[0].cbePlaceOfBirth.value + '" & "&nPosition=' + document.forms[0].cbeTypeOfPoliticalOffice.value + '" & "&dStartcondition=' + document.forms[0].tcdGrantDate.value + '" & "&dEndcondition=' + document.forms[0].tcdEndDate.value + '" & "&nResident_former=' + document.forms[0].cbeResidentFormer.value + '" & "&nSecond_nationality=' + document.forms[0].cbeSecondNationality.value + '" & "&sUsAdress=' + document.forms[0].txtAddress.value + '" & "&sSSN=' + document.forms[0].txtSSN.value + '" & "&sUsLegal_person=' + document.forms[0].txtUsLegalPerson.value + '" & "&sUsphone=' + document.forms[0].txtUsphone.value + '" & "&sUsAccount=' + document.forms[0].txtUsAccount.value + '" & "&sUsIrsind=' + document.forms[0].chkUsirsind.value + '"
            '.sDelRecordParam = "sclient=" & Session("sClient") & "&nCountry=' + marrArray[lintIndex].cbeCountry + '" & "&sPlace_birth=' + document.forms[0].txtPlaceOfBirth.value + '" & "&nPlace_birth=' + document.forms[0].cbePlaceOfBirth.value + '" & "&nPosition=' + document.forms[0].cbeTypeOfPoliticalOffice.value + '" 
        End With

        Dim mobjFiscalResidences As eClient.Fiscal_Residences
        mobjFiscalResidences = New Fiscal_Residences

        If mobjFiscalResidences.Find(Session("sClient"), Date.Now) Then
            For Each lobjFiscalResidence In mobjFiscalResidences
                With mobjGrid
                    .Columns("cbeCountry").DefValue = lobjFiscalResidence.nCountry
                    .Columns("tctsJurisdiction").DefValue = lobjFiscalResidence.sJurisdiction
                    .Columns("tctsus_Itinnum").DefValue = lobjFiscalResidence.Sus_Itinnum
                    .Columns("cbeNmotive_Itin").DefValue = lobjFiscalResidence.nMotive_Itin
                    Response.Write(.DoRow)
                End With
            Next lobjFiscalResidence
        End If
        '        Session("npais") = "' + marrArray[lintIndex].cbeCountry' "
        Response.Write(mobjGrid.closeTable())
        'Response.Write(mobjValues.BeginPageButton)

    End If

%>
</TD>
</tr>
	</TABLE>
	<!--<P ALIGN="Center"><%=mobjValues.BeginPageButton%></P>-->
		
</FORM>
</BODY>
</HTML>
<%
    mobjValues = Nothing
    mobjclient_PEP = Nothing

%>


