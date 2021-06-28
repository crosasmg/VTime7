<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de la página.
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjClient As eClient.Client
    Dim mobjGrid As eFunctions.Grid
    Dim mobjClientSeq As eClient.ClientSeq
    Dim UsPerson As String
    Dim NoUsPerson As String


    '%insDefineHeader: Configura los títulos del encabezado del grid.
    '---------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------------

        mobjGrid.ActionQuery = Session("bQuery")

        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 4, "",  , GetLocalResourceObject("tcnOrderColumnToolTip"))
            Call .AddClientColumn(0, GetLocalResourceObject("tctClientrColumnCaption"), "tctClientr", "",  , GetLocalResourceObject("tctClientrColumnCaption"),  ,  , "tctClieName")
            Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnPositionColumnCaption"), "tcnPosition", "Table283", 1, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnPositionColumnCaption"))
        End With

        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "BC001J"
            .DeleteButton = True
            .AddButton = True
            .Columns("Sel").GridVisible = True
            .Width = 450
            .Height = 220
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '%insPreBC001J: Se cargan los datos repetitivos de la página.
    '--------------------------------------------------------------------------------------------
    Private Sub insPreBC001J()
        '--------------------------------------------------------------------------------------------
        Dim lclsContac_cli As eClient.Contac_cli
        Dim lclsContac_clis As eClient.Contac_clis

        Response.Write(mobjMenu.setZone(2, "BC001J", "BC001J.aspx"))
        mobjMenu = Nothing

        '+ Obtiene los datos del cliente.
        With mobjClient
            Session("dInpdate") = ""
            If .insPreBC001(Session("sClient"), Request.QueryString.Item("ReloadAction"), mobjValues.StringToType(Request.QueryString.Item("tcdInpDate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("tctClieName"), mobjValues.StringToType(Request.QueryString.Item("valOcupat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("tcdBirthDate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("chkBlockadeJ"), Request.QueryString.Item("tctLegalName"), mobjValues.StringToType(Request.QueryString.Item("tcnEmpl_qua"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("cbeInvoicing"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("chkBill_ind"), mobjValues.StringToType(Request.QueryString.Item("cbeComp_Type"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("chkUSPERSON"), Request.QueryString.Item("chkPEP")) Then
                If .dInpdate <> eRemoteDB.Constants.dtmNull Then
                    Session("dInpdate") = .dInpdate
                Else
                    Session("dInpdate") = Today
                End If
            End If

            UsPerson = "0"
            NoUsPerson = "1"

            If .sUsPerson = "1" Then
                UsPerson = "1"
                NoUsPerson = "2"
            ElseIf .sUsPerson = "2" Then
                NoUsPerson = "1"
                UsPerson = "2"
            Else
                NoUsPerson = "1"
                UsPerson = "2"
            End If

        End With

        Response.Write("" & vbCrLf)
        Response.Write("    <A NAME=""BeginPage""></A>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""18%""><LABEL ID=9744>" & GetLocalResourceObject("tcdInpDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""25%""> ")


        Response.Write(mobjValues.DateControl("tcdInpDate", mobjValues.TypeToString(Session("dInpdate"), eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdInpDateToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""18%""><LABEL ID=9742>" & GetLocalResourceObject("tctLegalNameCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD colspan = 4> ")


        Response.Write(mobjValues.TextControl("tctLegalName", 60, mobjClient.sLegalname, True, GetLocalResourceObject("tctLegalNameToolTip"),  ,  ,  ,))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""18%""><LABEL ID=9742>" & GetLocalResourceObject("tctClieNameCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD colspan = 4> ")


        Response.Write(mobjValues.TextControl("tctClieName", 60, mobjClient.sCliename,  , GetLocalResourceObject("tctClieNameToolTip"),  ,  ,  , "ShowChangeValues()"))


        Response.Write(" </TD>                    " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""18%""><LABEL ID=9741>" & GetLocalResourceObject("valOcupatCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=""25%"">")


        Response.Write(mobjValues.PossiblesValues("valOcupat", "TABLE417", 1, CStr(mobjClient.nSpeciality)))


        Response.Write("</TD>      " & vbCrLf)
        Response.Write("            <TD><LABEL ID=9738>" & GetLocalResourceObject("tcdBirthDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdBirthDate", mobjValues.TypeToString(mobjClient.dBirthdat, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdBirthDateToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("           <TD WIDTH=""18%""><LABEL ID=9741>" & GetLocalResourceObject("tcnEmpl_quaCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("           <TD WIDTH=""25%"">")


        Response.Write(mobjValues.NumericControl("tcnEmpl_qua", 9, CStr(mobjClient.nEmpl_qua), False, GetLocalResourceObject("tcnEmpl_quaToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("           <TD WIDTH=""18%""><LABEL ID=9741>" & GetLocalResourceObject("cbeInvoicingCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("           <TD WIDTH=""18%"">")


        Response.Write(mobjValues.PossiblesValues("cbeInvoicing", "TABLE5522", 1, CStr(mobjClient.nInvoicing),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInvoicingToolTip")))


        Response.Write("</TD>                        " & vbCrLf)
        Response.Write("           <TD WIDTH=""25%"">")


        Response.Write(mobjValues.PossiblesValues("cbeMeasunit", "TABLE5593", 1, "1",  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeMeasunitToolTip")))


        Response.Write("</TD>                        " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("           <TD WIDTH=""22%"">")


        Response.Write(mobjValues.CheckControl("chkBill_ind", GetLocalResourceObject("chkBill_indCaption"), mobjClient.sBill_ind))


        Response.Write("</TD> " & vbCrLf)
        Response.Write("           <TD> </TD>                                               " & vbCrLf)
        Response.Write("           <TD COLSPAN=""4"" WIDTH=""22%"">")


        Response.Write(mobjValues.CheckControl("chkBlockadeJ", GetLocalResourceObject("chkBlockadeJCaption"), mobjClient.sBlockade))


        Response.Write("</TD> " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("           <TD COLSPAN=""1"" WIDTH=""22%"">")


        Response.Write(mobjValues.CheckControl("chkPEP", GetLocalResourceObject("chkPEPCaption"), mobjClient.sPEP))

        Response.Write("</TD> " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("           <TD COLSPAN=""1"" WIDTH=""22%"">")

        Response.Write("           <LABEL >" & GetLocalResourceObject("chkUsPersonCaption") & "</LABEL>" & vbCrLf)
        Response.Write("           <TD COLSPAN=""1"" WIDTH=""22%"">")
        Response.Write(mobjValues.OptionControl(0, "chkUSPERSON", GetLocalResourceObject("chkUSPERSON_1Caption"), UsPerson, "1", , , , GetLocalResourceObject("chkUSPERSON_1ToolTip")))
        'Response.Write("</TD> " & vbCrLf)
        'Response.Write("           <TD COLSPAN=""1"" WIDTH=""22%"">")
        Response.Write(mobjValues.OptionControl(0, "chkUSPERSON", GetLocalResourceObject("chkUSPERSON_2Caption"), NoUsPerson, "2", , , , GetLocalResourceObject("chkUSPERSON_2ToolTip")))


        Response.Write("</TD> " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)


        'Response.Write("</TD>" & vbCrLf)
        'Response.Write("        </TR>" & vbCrLf)
        'Response.Write("        <TR>" & vbCrLf)

        '     <TR> 
        '        <TD><LABEL><%= GetLocalResourceObject("chkUsPersonCaption")%></LABEL></TD> 
        '      <TD><%=mobjValues.OptionControl(0, "chkUSPERSON", GetLocalResourceObject("chkUSPERSON_1Caption"), UsPerson, "1", , , , GetLocalResourceObject("chkUSPERSON_1ToolTip"))%></TD>
        '<TD><%=mobjValues.OptionControl(0, "chkUSPERSON", GetLocalResourceObject("chkUSPERSON_2Caption"), NoUsPerson, "2", , , , GetLocalResourceObject("chkUSPERSON_2ToolTip"))%></TD>
        '  </TR>
        Response.Write("           <TD WIDTH=""18%""><LABEL ID=9741>" & GetLocalResourceObject("cbeComp_TypeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("           <TD WIDTH=""18%"">")


        Response.Write(mobjValues.PossiblesValues("cbeComp_Type", "TABLE5530", 1, CStr(mobjClient.nComp_Type),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeComp_TypeToolTip")))


        Response.Write("</TD>                        " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""4"" CLASS=""HighLighted""><LABEL><A NAME=""#" & GetLocalResourceObject("AnchorContactos2Caption") & """>" & GetLocalResourceObject("AnchorContactos2Caption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""100%"" COLSPAN=""4""><HR></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>")

        'Se declaran variables de sesión para manejar reload de página bc001j

        Session("sBc001j_tcdInpDate") = mobjClient.dInpdate
        Session("sBc001j_tctLegalName") = mobjClient.sLegalname
        Session("sBc001j_tctClieName") = mobjClient.sCliename
        Session("sBc001j_tcdBirthDate") = mobjClient.dBirthdat
        Session("sBc001j_chkBlockadeJ") = mobjClient.sBlockade
        Session("sBc001j_chkPEP") = mobjClient.sPEP
        Session("sBc001j_chkUSPERSON") = mobjClient.sUsPerson
        Session("sBc001j_tcnEmpl_qua") = mobjClient.nEmpl_qua
        Session("sBc001j_cbeInvoicing") = mobjClient.nInvoicing
        Session("sBc001j_chkBill_ind") = mobjClient.sBill_ind
        Session("sBc001j_cbeComp_Type") = mobjClient.nComp_Type




        With Server
            lclsContac_cli = New eClient.Contac_cli
            lclsContac_clis = New eClient.Contac_clis
        End With

        If lclsContac_clis.Find(Session("sClient")) Then

            For Each lclsContac_cli In lclsContac_clis
                With mobjGrid
                    .Columns("tcnOrder").DefValue = CStr(lclsContac_cli.nOrder)
                    .Columns("tctClientr").DefValue = lclsContac_cli.sClientr
                    mobjGrid.Columns("tctClientr").EditRecord = True
                    .sDelRecordParam = "sClientr=' + marrArray[lintIndex].tctClientr + '"
                    .Columns("tcnPosition").DefValue = CStr(lclsContac_cli.nPosition)
                    '.sEditRecordParam = "&nMainAction=" & Request.QueryString.Item("nMainAction")

                    Response.Write(.DoRow)
                End With
            Next lclsContac_cli
        End If
        Response.Write(mobjGrid.closeTable())
        Response.Write(mobjValues.BeginPageButton)

        lclsContac_cli = Nothing
        lclsContac_clis = Nothing
    End Sub

    '% insPreBC001JUpd : Permite realizar las actualizaciones sobre los contactos del cliente judírico.
    '-------------------------------------------------------------------------------------------
    Private Sub insPreBC001JUpd()
        '-------------------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la BC001J" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function UpdateFields(){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("if (top.opener.document.forms[0].chkBill_ind.checked) {top.opener.document.forms[0].chkBill_ind.setAttribute('value','1')} else {top.opener.document.forms[0].chkBill_ind.setAttribute('value','2') }")
        Response.Write("if (top.opener.document.forms[0].chkPEP.checked) {top.opener.document.forms[0].chkPEP.setAttribute('value','1')} else {top.opener.document.forms[0].chkPEP.setAttribute('value','2') }")
        Response.Write("if (top.opener.document.forms[0].chkBlockadeJ.checked) {top.opener.document.forms[0].chkBlockadeJ.setAttribute('value','1')} else {top.opener.document.forms[0].chkBlockadeJ.setAttribute('value','2') }")
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("		tcdInpDate.value = top.opener.document.forms[0].tcdInpDate.value;" & vbCrLf)
        Response.Write("        tctLegalName.value =  top.opener.document.forms[0].tctLegalName.value;" & vbCrLf)
        Response.Write("		tctClieName.value = top.opener.document.forms[0].tctClieName.value;" & vbCrLf)
        Response.Write("		valOcupat.value = top.opener.document.forms[0].valOcupat.value;" & vbCrLf)
        Response.Write("		tcdBirthDate.value = top.opener.document.forms[0].tcdBirthDate.value;" & vbCrLf)
        Response.Write("        tcnEmpl_qua.value = top.opener.document.forms[0].tcnEmpl_qua.value;" & vbCrLf)
        Response.Write("        cbeInvoicing.value = top.opener.document.forms[0].cbeInvoicing.value;" & vbCrLf)
        Response.Write("        cbeComp_Type.value = top.opener.document.forms[0].cbeComp_Type.value;" & vbCrLf)
        Response.Write("		chkBlockadeJ.value = top.opener.document.forms[0].chkBlockadeJ.value;" & vbCrLf)
        Response.Write("        chkBill_ind.value = top.opener.document.forms[0].chkBill_ind.value;" & vbCrLf)
        Response.Write("		chkPEP.value = top.opener.document.forms[0].chkPEP.value;" & vbCrLf)
        Response.Write("		chkUSPERSON.value = top.opener.document.forms[0].chkUSPERSON.value;" & vbCrLf)

        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "SCRIPT>")


        If LCase(Request.QueryString.Item("Action")) = "del" Then
            Response.Write(mobjValues.ConfirmDelete)
            With Request

                Call mobjClientSeq.insPostBC001J("Delete", Session("sClient"), .QueryString.Item("sClientr"),  ,  ,  , Session("nUsercode"))
            End With
        End If
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valClientSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))

        With Response
            .Write(mobjValues.HiddenControl("tcdInpDate", ""))
            .Write(mobjValues.HiddenControl("tctLegalName", ""))
            .Write(mobjValues.HiddenControl("tctClieName", ""))
            .Write(mobjValues.HiddenControl("valOcupat", ""))
            .Write(mobjValues.HiddenControl("tcdBirthDate", ""))
            .Write(mobjValues.HiddenControl("tcnEmpl_qua", ""))
            .Write(mobjValues.HiddenControl("chkBlockadeJ", ""))
            .Write(mobjValues.HiddenControl("chkPEP", ""))
            .Write(mobjValues.HiddenControl("chkUSPERSON", ""))
            .Write(mobjValues.HiddenControl("cbeInvoicing", ""))
            .Write(mobjValues.HiddenControl("chkBill_ind", ""))
            .Write(mobjValues.HiddenControl("cbeComp_Type", ""))
        End With

        Response.Write("<SCRIPT>UpdateFields()</" & "Script>")

        If LCase(Request.QueryString.Item("Action")) = "add" Then
            Response.Write("<SCRIPT>insDefValues();</" & "Script>")
        End If
    End Sub

</script>
<%
Response.Expires = 0


mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjClient = New eClient.Client
mobjGrid = New eFunctions.Grid
mobjClientSeq = New eClient.ClientSeq

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If
%> 
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>




//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $"
		
//%insDefValues: muestra los valores por defecto
//--------------------------------------------------------------------------------------------
function insDefValues(){
//--------------------------------------------------------------------------------------------
//- Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0;
    var llngMaxUlt = 0;
	    
//+ Se genera el número consecutivo del Order
    for (var llngIndex = 0; llngIndex < top.opener.marrArray.length; llngIndex++)
        if (top.opener.marrArray[llngIndex].tcnOrder > llngMax)
            llngMax = top.opener.marrArray[llngIndex].tcnOrder;
		
		
		    if(++llngMax.length > self.document.forms[0].tcnOrder.maxLength){
//+ Se asignan null
				self.document.forms[0].tcnOrder.value = "";						//+ null			
		    }		
			else{
//+ Se asignan el valor por defecto del Order			
				self.document.forms[0].tcnOrder.value = ++llngMax;				//+ Consecutivo			
			}
	}

//%insEnabledFields: Habilita o deshabilita los campos de la ventana, dependiendo si están llenos o no. ACM - 31/07/2001.	
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
//+ Fecha de Ingreso
	    if (elements["tcdInpDate"].value == "")
	        elements["tcdInpDate"].disabled = false;
	    else
	        elements["tcdInpDate"].disabled = true;

//+ Nombre Fantasía
	    if (elements["tctClieName"].value == "")
	        elements["tctClieName"].disabled = false;
	    else
	        elements["tctClieName"].disabled = true;
			
//+ Razón social
	    if (elements["tctLegalName"].value == "")
	        elements["tctLegalName"].disabled = false;
	    else
	        elements["tctLegalName"].disabled = true;		
//+ Giro Comercial
	    if (elements["valOcupat"].value == "0")
	        elements["valOcupat"].disabled = false;
	    else
	        elements["valOcupat"].disabled = true;

//+ Inicio de Operaciones
	    if (elements["tcdBirthDate"].value == "")
	        elements["tcdBirthDate"].disabled = false;
	    else
	        elements["tcdBirthDate"].disabled = true;

//+ Cantidad de Empleados
	    if (elements["tcnEmpl_qua"].value == 1)
	        elements["tcnEmpl_qua"].disabled = false;
	    else
	        elements["tcnEmpl_qua"].disabled = true;

//+ Volumen de Facturación
	    if (elements["cbeInvoicing"].value == "0")
	        elements["cbeInvoicing"].disabled = false;
	    else
	        elements["cbeInvoicing"].disabled = true;

//+ Indicador de Factura
	    if (elements["chkBill_ind"].value == 1)
	        elements["chkBill_ind"].disabled = false;
	    else
	        elements["chkBill_ind"].disabled = true;
					
//+ Bloqueado
	    if (elements["chkBlockadeJ"].value == 1)
	        elements["chkBlockadeJ"].disabled = false;
	    else
	        elements["chkBlockadeJ"].disabled = true;
	
//+ PEP
	    if (elements["chkPEP"].value == 1)
	        elements["chkPEP"].disabled = false;
	    else
	        elements["chkPEP"].disabled = true;

//+ USPERSON
	    if (elements["chkUSPERSON"].value == 1)
	        elements["chkUSPERSON"].disabled = false;
	    else
	        elements["chkUSPERSON"].disabled = true;
	}
}

//%ShowChangeValues: Muestra el contenido de los campos display.	
//-------------------------------------------------------------------------------------------
function ShowChangeValues(){
//-------------------------------------------------------------------------------------------
	if(self.document.forms[0].elements["tctClieName"].value=="")
		self.document.forms[0].elements["tctClieName"].value=self.document.forms[0].elements["tctLegalName"].value;
}

function SetNavigationParams() {
    if (top.fraHeader.qs("LinkSpecialAction") == "301" &&
        top.fraHeader.qs("LinkParamsClient") > "" &&
        $("[name=tctClieName]").val() == "" &&
        $("[name=tctLegalName]").val() == "") {
        $("[name=tctClieName]").val(top.fraHeader.qs("sFirstName"));
        $("[name=tctLegalName]").val(top.fraHeader.qs("sFirstName"));
    }
}

$(function () {
    SetNavigationParams();
});   
</SCRIPT>
        <%
Response.Write(mobjValues.StyleSheet())
Response.Write("<SCRIPT>var nMainAction = " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
%>
    </HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBC001J" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
     <%
         Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))

         '+ Se configura la estructura del grid, deacuerdo al tipo de ventana.
         Call insDefineHeader()




         If LCase(Request.QueryString.Item("Type")) <> "popup" Then
             Call insPreBC001J()
         Else
             Call insPreBC001JUpd()
         End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjClient = Nothing
mobjGrid = Nothing
mobjClientSeq = Nothing
mobjValues = Nothing

If CStr(Session("sOriginalForm")) <> vbNullString Then
	Response.Write("<SCRIPT>insEnabledFields();</SCRIPT>")
End If
%>




