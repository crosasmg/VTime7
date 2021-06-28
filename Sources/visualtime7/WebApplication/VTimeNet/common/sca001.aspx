<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

    '- Objeto para creacion de menu 
    Dim mobjMenu As eFunctions.Menues
    '- Objeto para uso de valores    
    Dim mobjValues As eFunctions.Values
    '- Objeto para uso de direcciones    
    Dim mobjAddress As eGeneralForm.Address
    '- Objeto para manejo de grid
    Dim mobjGrid As eFunctions.Grid
    '- Objeto para usos generales
    Dim mobjGeneral As eGeneral.OptionsInstallation

    '- Variables para almacenar parametros de pagina y propiedades de objetos
    Dim mblnActionQuery As Boolean
    Dim mstrKeyAddress As String
    Dim mintRecowner As Object
    Dim mstrRecType As Object
    Dim mstrQuote As String
    Dim mstrAmp As String
    Dim mstrOnSeq As String

    '- Variable para activar o desactivar boton Agregar en la grilla de teléfonos
    Dim mblnExistAddress As Boolean
    Dim mblnExistPhones As Boolean

    '- Variables para almacenar la fecha de ingreso del cliente
    Dim mdatClient As Object
    Dim mdatClientPhone As Object

    '- Variables para manejo temporal del cliente y la acción que se está ejecutando.
    Dim mstrLastClient As Object
    Dim mblnLastAction As Object
    Dim mclsCertificat As ePolicy.Certificat

    Dim mnMainAction As Object


    '%insDefineHeader. Se definen las columnas del grid
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------

        mobjGrid = New eFunctions.Grid

        mobjGrid.sCodisplPage = "sca001"

        With mobjGrid.Columns
            .AddNumericColumn(0, GetLocalResourceObject("tcnOrderColumnCaption"), "tcnOrder", 4, CStr(0), True, GetLocalResourceObject("tcnOrderColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            .AddPossiblesColumn(0, GetLocalResourceObject("cbePhoneTypeColumnCaption"), "cbePhoneType", "Table564", 1)
            .AddNumericColumn(0, GetLocalResourceObject("tcnAreaColumnCaption"), "tcnArea", 5, vbNullString,  , GetLocalResourceObject("tcnAreaColumnToolTip"))
            .AddTextColumn(0, GetLocalResourceObject("tctPhoneColumnCaption"), "tctPhone", 11, vbNullString, True, GetLocalResourceObject("tctPhoneColumnToolTip"))
            mobjGrid.Columns("tctPhone").bNumericText = True
            .AddNumericColumn(0, GetLocalResourceObject("tcnExtensi1ColumnCaption"), "tcnExtensi1", 5, vbNullString,  , GetLocalResourceObject("tcnExtensi1ColumnCaption"))
            .AddNumericColumn(0, GetLocalResourceObject("tcnExtensi2ColumnCaption"), "tcnExtensi2", 5, vbNullString,  , GetLocalResourceObject("tcnExtensi2ColumnCaption"))
            .AddHiddenColumn("tcnKeyPhones", CStr(0))

        End With

        With mobjGrid
            .ActionQuery = Session("bQuery")
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "SCA001"
            .Height = GetLocalResourceObject("HeightPopup")
            .Width = GetLocalResourceObject("WidthPopup")
            If Request.QueryString.Item("nMainAction") = "undefined" Then
                .nMainAction = 0
            Else
                .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            End If
            .Columns("tcnOrder").EditRecord = True
            .Columns("cbePhoneType").EditRecord = True
        End With

    End Sub

    '%insPreSCA001: Despliega la ventana de la transaccion
    '------------------------------------------------------------------------------
    Private Sub insPreSCA001()
        '------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("	var nMainAction ='")


        Response.Write(Request.QueryString.Item("nMainAction"))


        Response.Write("'" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%ShowAddress: Refresca la página cuando se cambia la opcion de tipo de dirección" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function ShowAddress(Value) {" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("    var lstrLocation" & vbCrLf)
        Response.Write("    lstrLocation = self.document.location" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("    lstrLocation = ")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(" + lstrLocation" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("Reload=0")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("sRecType=1")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("sRecType=2")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("sRecType=3")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("sRecType=4")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("nSendAddr=1")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("nSendAddr=2")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("nSendAddr=3")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("nSendAddr=4")


        Response.Write(mstrQuote)


        Response.Write(",")


        Response.Write(mstrQuote)




        Response.Write(mstrQuote)


        Response.Write(")" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(/&RecTypeReload.*/, """");" & vbCrLf)
        Response.Write("    lstrLocation = lstrLocation.replace(/&txtAddress.*/, """");" & vbCrLf)
        Response.Write("    " & vbCrLf)
        Response.Write("    self.document.location.href = lstrLocation + ")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("RecTypeReload=1")


        Response.Write(mstrAmp)


        Response.Write("Reload=0")


        Response.Write(mstrAmp)


        Response.Write("sRecType=")


        Response.Write(mstrQuote)


        Response.Write(" + Value + ")


        Response.Write(mstrQuote)




        Response.Write(mstrAmp)


        Response.Write("nSendAddr=")


        Response.Write(mstrQuote)


        Response.Write(" + Value" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%InsChangeMunicipality: Busca la ciudad y la región dada la comuna" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("function InsChangeMunicipality(nMunicipality){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("    insDefValues('Municipality', 'nMunicipality=' + nMunicipality)" & vbCrLf)
        Response.Write("    " & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("//%UpdValues: Actualiza el contenido de los campos de la region" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("function UpdValues(lintProvince,lstrProvince){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("    with (self.document.forms[0]){" & vbCrLf)
        Response.Write("        elements[")


        Response.Write(mstrQuote)


        Response.Write("tcnProvince")


        Response.Write(mstrQuote)


        Response.Write("].value = lintProvince;" & vbCrLf)
        Response.Write("        elements[")


        Response.Write(mstrQuote)


        Response.Write("tctProvince")


        Response.Write(mstrQuote)


        Response.Write("].value = lstrProvince;" & vbCrLf)
        Response.Write("    }" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%InsChangeZipCode: Actualiza los parametros del codigo de zona" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("function InsChangeZipCode(Field){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("    with (self.document.forms[0]){" & vbCrLf)
        Response.Write("		valLocal.Parameters.Param1.sValue=Field.value;" & vbCrLf)
        Response.Write("		valLocal.disabled = Field.value == 0;" & vbCrLf)
        Response.Write("		btnvalLocal.disabled = valLocal.disabled;" & vbCrLf)
        Response.Write("	    ShowPopUp('ShowDefValues.aspx?sField=ZipCode&amp;nZipCode=' + Field.value,'ShowDefValues',100,100,'No','No',3000,3000)" & vbCrLf)
        Response.Write("    }" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%InsChangeChkDel: actualiza el estado de un checkbox" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("function InsChangeChkDel(Field){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------	" & vbCrLf)
        Response.Write("    Field.value=(Field.checked?'1':'0');" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//% insEnabledFields: Inhabilita los campos de la ventana que estén llenos si la variable" & vbCrLf)
        Response.Write("//%					  de sesión ""sOriginalForm"" es diferente de blanco " & vbCrLf)
        Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insEnabledFields(){" & vbCrLf)
        Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("	    elements[""optAdr[]""].disabled = !(elements[""optAdr[]""].checked);" & vbCrLf)
        'Response.Write("	    txtAddress.disabled = !(txtAddress.value== """");" & vbCrLf)
        Response.Write("        if(typeof(valZipCode)!= 'undefined')" & vbCrLf)
        Response.Write("	    valZipCode.disabled = !(valZipCode.value=="""" || valZipCode.value==0);" & vbCrLf)
        'Response.Write("	    valLocal.disabled = !(valLocal.value=="""" || valLocal.value==0);" & vbCrLf)
        Response.Write("	    cbeCountry.disabled = !(cbeCountry.value=="""" || cbeCountry.value==0);" & vbCrLf)
        Response.Write("	    tctE_mail.disabled = !(tctE_mail.value=="""");" & vbCrLf)
        'Response.Write("	    cbeNoInformEmail.disabled = !(cbeNoInformEmail.value=="""");" & vbCrLf)
        Response.Write("	    tcnLatCardinG.disabled = !(tcnLatCardinG.value=="""" || tcnLatCardinG.value==0);" & vbCrLf)
        Response.Write("	    tcnLatCardinM.disabled = !(tcnLatCardinM.value=="""" || tcnLatCardinM.value==0);" & vbCrLf)
        Response.Write("        tcnLatCardinS.disabled = !(tcnLatCardinS.value=="""" || tcnLatCardinS.value==0);" & vbCrLf)
        Response.Write("        tcnLonCardinG.disabled = !(tcnLonCardinG.value=="""" || tcnLonCardinG.value==0);" & vbCrLf)
        Response.Write("	    tcnLonCardinM.disabled = !(tcnLonCardinM.value=="""" || tcnLonCardinM.value==0);	" & vbCrLf)
        Response.Write("		tcnLonCardinS.disabled = !(tcnLonCardinS.value=="""" || tcnLonCardinS.value==0);" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//% insEnabledFieldsPolicy: Inhabilita los campos de la ventana que estén llenos si la variable" & vbCrLf)
        Response.Write("//%      				    de sesión ""sOriginalForm"" es diferente de blanco " & vbCrLf)
        Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insEnabledFieldsPolicy(){" & vbCrLf)
        Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("		tctBuild.disabled = true;" & vbCrLf)
        Response.Write("		valMunicipality.disabled = true;" & vbCrLf)
        Response.Write("		btnvalMunicipality.disabled = true;" & vbCrLf)
        Response.Write("		valLocal.disabled     = true;" & vbCrLf)
        Response.Write("		btnvalLocal.disabled = true;" & vbCrLf)
        Response.Write("		cbeProvince.disabled = true;" & vbCrLf)
        Response.Write("		cbeCountry.disabled = true;" & vbCrLf)
        Response.Write("		if(tctRecType.value==3)" & vbCrLf)
        Response.Write("			tctPobox.disabled = true;" & vbCrLf)
        Response.Write("		else{" & vbCrLf)
        Response.Write("			txtAddress.disabled = true;" & vbCrLf)
        Response.Write("			tcnFloor.disabled = true;" & vbCrLf)
        Response.Write("			tctDepartment.disabled = true;" & vbCrLf)
        Response.Write("			tctPopulation.disabled = true;" & vbCrLf)
        Response.Write("			tctDescadd.disabled = true;" & vbCrLf)
        Response.Write("			tcnZipCode.disabled = true;" & vbCrLf)
        Response.Write("			tctE_mail.disabled = true;" & vbCrLf)
        Response.Write("			tctE_mail.disabled = true;" & vbCrLf)
        Response.Write("			cbeNoInformEmail.disabled = true;" & vbCrLf)
        Response.Write("			tcnLatCardinG.disabled = true;" & vbCrLf)
        Response.Write("			tcnLatCardinM.disabled = true;" & vbCrLf)
        Response.Write("			tcnLatCardinS.disabled = true;" & vbCrLf)
        Response.Write("			tcnLonCardinG.disabled = true;" & vbCrLf)
        Response.Write("			tcnLonCardinM.disabled = true;	" & vbCrLf)
        Response.Write("			tcnLonCardinS.disabled = true;" & vbCrLf)
        Response.Write("			cmdAdd.disabled = true;" & vbCrLf)
        Response.Write("			if(typeof(cmdDelete)!='undefined')" & vbCrLf)
        Response.Write("				cmdDelete.disabled = true;" & vbCrLf)
        Response.Write("		}" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("//% insEnabledFields: Inhabilita los campos de la ventana que estén llenos si la variable" & vbCrLf)
        Response.Write("//%					  de sesión ""sOriginalForm"" es diferente de blanco " & vbCrLf)
        Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insEnableEmail(Field){" & vbCrLf)
        Response.Write("//---------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("	    if (Field.value==1){" & vbCrLf)
        Response.Write("			tctE_mail.disabled = true;" & vbCrLf)
        Response.Write("			tctE_mail.value = """";" & vbCrLf)
        Response.Write("		}" & vbCrLf)
        Response.Write("		else" & vbCrLf)
        Response.Write("			tctE_mail.disabled = false;" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "SCRIPT>")


        Call insreaAddress(CStr(Request.QueryString.Item("sCodispl")))

        If mobjAddress.nCountry > 0 Then
            Session("nCountry") = mobjAddress.nCountry
        Else
            mobjGeneral = New eGeneral.OptionsInstallation
            If mobjGeneral.FindOptGeneral() Then
                Session("nCountry") = mobjGeneral.nCountry
            End If
            mobjGeneral = Nothing
        End If

        If Request.QueryString.Item("sCodispl") = "SCA101" Or Request.QueryString.Item("sCodispl") = "SCA106" Then

            Response.Write("" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD WIDTH=""100%"" COLSPAN=""5"" ALIGN=""CENTER"">" & vbCrLf)
            Response.Write("               <TABLE>" & vbCrLf)
            Response.Write("                   <TR>")


            mblnActionQuery = mobjValues.ActionQuery
            mobjValues.ActionQuery = False

            If mstrRecType = "1" Then
                With Response
                    .Write("<TD>" & mobjValues.OptionControl(40548, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr2Caption"), CStr(2), CStr(2), "ShowAddress(2)") & "</TD>")
                    .Write("<TD>" & mobjValues.OptionControl(40549, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr1Caption"), CStr(1), CStr(1), "ShowAddress(1)") & "</TD>")
                    .Write("<TD>" & mobjValues.OptionControl(40549, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr3Caption"), CStr(3), CStr(3), "ShowAddress(3)") & "</TD>")
                End With
            ElseIf mstrRecType = "2" Then
                With Response
                    .Write("<TD>" & mobjValues.OptionControl(40550, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr2Caption"), CStr(1), CStr(2), "ShowAddress(2)") & "</TD>")
                    .Write("<TD>" & mobjValues.OptionControl(40551, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr1Caption"), CStr(3), CStr(1), "ShowAddress(1)") & "</TD>")

                    .Write("<TD>" & mobjValues.OptionControl(0, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr3Caption"), CStr(2), CStr(3), "ShowAddress(3)") & "</TD>")
                End With
            Else
                With Response
                    .Write("<TD>" & mobjValues.OptionControl(0, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr2Caption"), CStr(3), CStr(2), "ShowAddress(2)") & "</TD>")
                    .Write("<TD>" & mobjValues.OptionControl(0, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr1Caption"), CStr(2), CStr(1), "ShowAddress(1)") & "</TD>")
                    .Write("<TD>" & mobjValues.OptionControl(0, "optAdr[]", GetLocalResourceObject("optAdr[]_CStr3Caption"), CStr(1), CStr(3), "ShowAddress(3)") & "</TD>")
                End With
            End If

            mobjValues.ActionQuery = mblnActionQuery

            Response.Write("" & vbCrLf)
            Response.Write("                    </TR>" & vbCrLf)
            Response.Write("                </TABLE>" & vbCrLf)
            Response.Write("            </TD>" & vbCrLf)
            Response.Write("        </TR>" & vbCrLf)
            Response.Write("        <TR>" & vbCrLf)
            Response.Write("            <TD WIDTH=""100%"" COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
            Response.Write("        </TR>")


        End If
        If Request.QueryString.Item("sCodispl") = "SCA102" And (Session("nTransaction") = 12 Or Session("nTransaction") = 14) Then

            Response.Write("" & vbCrLf)
            Response.Write("		<TR>" & vbCrLf)
            Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeSendAddrCaption") & "</LABEL></TD>            " & vbCrLf)
            Response.Write("			<TD>")


            Response.Write(mobjValues.PossiblesValues("cbeSendAddr", "table5574", eFunctions.Values.eValuesType.clngComboType, CStr(mclsCertificat.nSendAddr),  ,  ,  ,  ,  , "ShowAddress(this.value)",  ,  , GetLocalResourceObject("cbeSendAddrToolTip"),  , 22))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("		</TR>")


        End If

        If mstrRecType = "3" Then

            Response.Write("" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeCountryCaption") & "<LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, Session("nCountry"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCountryToolTip")))


            Response.Write("</TD>                " & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctPoboxCaption") & "<LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.TextControl("tctPobox", 15, mobjAddress.sPobox,  , GetLocalResourceObject("tctPoboxToolTip"),  ,  ,  ,  , Session("bQuery")))


            Response.Write("</TD>                " & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>    " & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeProvinceCaption") & "<LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, CStr(mobjAddress.nProvince),  ,  ,  ,  ,  , "insParameterLocat(this)", Session("bQuery"),  , GetLocalResourceObject("cbeProvinceToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("valLocalCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")

            mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjAddress.nLocal), True, , , , , "insParameterMunicipality(this)", Session("bQuery"), , GetLocalResourceObject("valLocalToolTip")))
            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("valMunicipalityCaption") & "<LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")

            mobjValues.Parameters.Add("nLocat", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjValues.Parameters.Add("nProvince", mobjAddress.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjValues.Parameters.ReturnValue("nLocal", False, vbNullString, True)
            Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjAddress.nMunicipality), True,  ,  ,  ,  , "InsChangeMunicipality(this.value)", Session("bQuery"), 10, GetLocalResourceObject("valMunicipalityToolTip")))
            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctBuildCaption") & "<LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.TextControl("tctBuild", 10, mobjAddress.sBuild,  , GetLocalResourceObject("tctBuildToolTip"),  ,  ,  ,  , Session("bQuery")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>    ")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("		<TD WIDTH=""25%""><LABEL>" & GetLocalResourceObject("txtAddressCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("		<TD COLSPAN=""3"">")


            Response.Write(mobjValues.TextAreaControl("txtAddress", 2, 40, mobjAddress.sStreet & " " & mobjAddress.sStreet1, True, GetLocalResourceObject("txtAddressToolTip"),  , Session("bQuery"),  , "insChangeAddress();"))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctBuildCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.TextControl("tctBuild", 10, mobjAddress.sBuild,  , GetLocalResourceObject("tctBuildToolTip"),  ,  ,  , "insChangeAddress();", Session("bQuery")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tcnFloorCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.NumericControl("tcnFloor", 5, CStr(mobjAddress.nFloor),  , GetLocalResourceObject("tcnFloorToolTip"),  ,  ,  ,  ,  , "insChangeAddress();", Session("bQuery")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>    " & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctDepartmentCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.TextControl("tctDepartment", 10, mobjAddress.sDepartment,  , GetLocalResourceObject("tctDepartmentToolTip"),  ,  ,  , "insChangeAddress();", Session("bQuery")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctPopulationCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.TextControl("tctPopulation", 40, mobjAddress.sPopulation,  , GetLocalResourceObject("tctPopulationToolTip"),  ,  ,  , "insChangeAddress();", Session("bQuery")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>    " & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctDescaddCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD COLSPAN=""3"">")


            Response.Write(mobjValues.TextAreaControl("tctDescadd", 2, 50, mobjAddress.sDescadd,  , GetLocalResourceObject("tctDescaddToolTip"),  , True))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tcnZipCodeCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.NumericControl("tcnZipCode", 10, CStr(mobjAddress.nZip_Code),  , GetLocalResourceObject("tcnZipCodeToolTip"),  ,  ,  ,  ,  ,  , Session("bQuery")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeCountryCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, Session("nCountry"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCountryToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("cbeProvinceCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType, CStr(mobjAddress.nProvince),  ,  ,  ,  ,  , "insParameterLocat(this)", Session("bQuery"),  , GetLocalResourceObject("cbeProvinceToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("valLocalCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")

            mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjAddress.nLocal), True,  ,  ,  ,  , "insParameterMunicipality(this)", Session("bQuery"),  , GetLocalResourceObject("valLocalToolTip")))
            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("valMunicipalityCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD>")

            mobjValues.Parameters.Add("nLocat", mobjAddress.nLocal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjValues.Parameters.Add("nProvince", mobjAddress.nProvince, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjValues.Parameters.ReturnValue("nLocal", False, vbNullString, True)
            Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType, CStr(mobjAddress.nMunicipality), True,  ,  ,  ,  , "InsChangeMunicipality(this.value)", Session("bQuery"), 10, GetLocalResourceObject("valMunicipalityToolTip")))
            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD COLSPAN=""2"">")


            If Request.QueryString.Item("sCodispl") = "SCA101" Then
                Response.Write(mobjValues.CheckControl("chkInfor", GetLocalResourceObject("chkInforCaption"), mobjAddress.sInfor, "1",  , Session("bQuery"),  , GetLocalResourceObject("chkInforCaption")))
            Else
                Response.Write(mobjValues.HiddenControl("chkInfor", "2"))
            End If

            Response.Write("" & vbCrLf)
            Response.Write("        </TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("		<TD><LABEL>" & GetLocalResourceObject("cbeNoInformEmailCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("		<TD>")


            Response.Write(mobjValues.PossiblesValues("cbeNoInformEmail", "Table8018", eFunctions.Values.eValuesType.clngComboType, mobjAddress.nNotInformEmail,  , False,  ,  ,  , "insEnableEmail(this)",  , 5, GetLocalResourceObject("cbeNoInformEmailToolTip")))


            Response.Write("</TD>	" & vbCrLf)
            Response.Write("<TD>	" & vbCrLf)
            Response.Write(mobjValues.CheckControl("chkEmail", GetLocalResourceObject("chkInforCaption2"), mobjAddress.sSend_mail, "1", , Session("bQuery"), , GetLocalResourceObject("chkInforCaption2")))
            Response.Write("</TD>	" & vbCrLf)

            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD><LABEL>" & GetLocalResourceObject("tctE_mailCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("        <TD COLSPAN=""3"">")


            Response.Write(mobjValues.TextControl("tctE_mail", 60, mobjAddress.sE_mail, False, GetLocalResourceObject("tctE_mailToolTip"),  ,  ,  ,  , Session("bQuery") Or CDbl(mobjAddress.nNotInformEmail) = 1 Or mobjValues.StringToType(Request.QueryString.Item("cbeNoInformEmail"), eFunctions.Values.eTypeData.etdLong) = 1))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>    ")


            'If Request.QueryString("sCodispl")="SCA101" Then 
            '    With Response
            '         .Write "<TD COLSPAN=""4"">" & mobjValues.CheckControl("chkdeldir", GetLocalResourceObject("chkdeldirCaption"),True,"","InsChangeChkDel(this)",Session("bQuery"),, GetLocalResourceObject("chkdeldirToolTip")) & "</TD>"
            '    End With
            'Else
            Response.Write("<TD COLSPAN=""4"">" & mobjValues.HiddenControl("chkdeldir", CStr(eRemoteDB.Constants.intNull)) & "</TD>")
            'End If

            Response.Write("" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("    <TR>")


            If Request.QueryString.Item("sCodispl") = "SCA101" Then
                With Response
                    .Write("<TD><LABEL>" & GetLocalResourceObject("tctCostCaption") & "</LABEL></TD>")
                    .Write("<TD COLSPAN=""3"">" & mobjValues.TextControl("tctCost", 30, mobjAddress.sCostCenter, False, GetLocalResourceObject("tctCostToolTip")) & "</TD>")
                End With
            End If

            Response.Write("    " & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("    " & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD COLSPAN=""4"" ALIGN=""RIGHT"" WIDTH=""100%"" CLASS=""HighLighted""><A NAME=""Coord""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></A></TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR><TD COLSPAN=""4"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
            Response.Write("	<TR>    " & vbCrLf)
            Response.Write("		<TD COLSPAN=""2"" WIDTH=""50%"" CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("	    <TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("	    <TD COLSPAN=""2"" WIDTH=""50%"" CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("Anchor3Caption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("	</TR>" & vbCrLf)
            Response.Write("	<TR>" & vbCrLf)
            Response.Write("	    <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
            Response.Write("	    <TD></TD>" & vbCrLf)
            Response.Write("	    <TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
            Response.Write("	</TR>    " & vbCrLf)
            Response.Write("    <TR>    " & vbCrLf)
            Response.Write("        <TD COLSPAN=""4"" WIDTH=""100%"">" & vbCrLf)
            Response.Write("            <TABLE WIDTH=100%>" & vbCrLf)
            Response.Write("		        <TR>" & vbCrLf)
            Response.Write("					<TD ALIGN=""LEFT""> " & vbCrLf)
            Response.Write("					    <LABEL>" & GetLocalResourceObject("tcnLatCardinGCaption") & "</LABEL>")


            Response.Write(mobjValues.NumericControl("tcnLatCardinG", 5, CStr(mobjAddress.nLat_grade), False, GetLocalResourceObject("tcnLatCardinGToolTip"), False,  ,  ,  ,  ,  , Session("bQuery")))


            Response.Write("" & vbCrLf)
            Response.Write("					    <LABEL>" & GetLocalResourceObject("tcnLatCardinMCaption") & "</LABEL>")


            Response.Write(mobjValues.NumericControl("tcnLatCardinM", 10, CStr(mobjAddress.nLat_minute), False, GetLocalResourceObject("tcnLatCardinMToolTip"), False,  ,  ,  ,  ,  , Session("bQuery")))


            Response.Write("" & vbCrLf)
            Response.Write("					    <LABEL>" & GetLocalResourceObject("tcnLatCardinSCaption") & "</LABEL>")


            Response.Write(mobjValues.NumericControl("tcnLatCardinS", 4, CStr(mobjAddress.nLat_second), False, GetLocalResourceObject("tcnLatCardinSToolTip"),  , 2, False,  ,  ,  , Session("bQuery")))


            Response.Write("" & vbCrLf)
            Response.Write("					</TD>" & vbCrLf)
            Response.Write("					<TD ALIGN=""LEFT"">" & vbCrLf)
            Response.Write("					    <LABEL>" & GetLocalResourceObject("tcnLatCardinGCaption") & "</LABEL>")


            Response.Write(mobjValues.NumericControl("tcnLonCardinG", 5, CStr(mobjAddress.nLon_grade), False, GetLocalResourceObject("tcnLonCardinGToolTip"), False,  ,  ,  ,  ,  , Session("bQuery")))


            Response.Write("" & vbCrLf)
            Response.Write("					    <LABEL>" & GetLocalResourceObject("tcnLatCardinMCaption") & "</LABEL>")


            Response.Write(mobjValues.NumericControl("tcnLonCardinM", 10, CStr(mobjAddress.nLon_minute), False, GetLocalResourceObject("tcnLonCardinMToolTip"), False,  ,  ,  ,  ,  , Session("bQuery")))


            Response.Write("" & vbCrLf)
            Response.Write("					    <LABEL>" & GetLocalResourceObject("tcnLatCardinSCaption") & "</LABEL>")


            Response.Write(mobjValues.NumericControl("tcnLonCardinS", 4, CStr(mobjAddress.nLon_second), False, GetLocalResourceObject("tcnLonCardinSToolTip"),  , 2, False,  ,  ,  , Session("bQuery")))


            Response.Write("" & vbCrLf)
            Response.Write("					</TD>" & vbCrLf)
            Response.Write("				</TR>" & vbCrLf)
            Response.Write("            </TABLE>" & vbCrLf)
            Response.Write("        </TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR>" & vbCrLf)
            Response.Write("        <TD COLSPAN=""4"" ALIGN=""RIGHT"" WIDTH=""100%"" CLASS=""HighLighted"">" & vbCrLf)
            Response.Write("        <A NAME=""Phones""><LABEL>" & GetLocalResourceObject("Anchor4Caption") & "</LABEL></A></TD>" & vbCrLf)
            Response.Write("    </TR>" & vbCrLf)
            Response.Write("    <TR><TD COLSPAN=""4""><HR></TD></TR>" & vbCrLf)
            Response.Write("</TABLE>")


            With mobjGrid
                .sEditRecordParam = "sKeyAddress=" & mstrKeyAddress & "&sRecType=" & mstrRecType & "&nRecOwner=" & mintRecowner & "&sOnSeq=" & Request.QueryString("sOnSeq") & "&sClient=" & Request.QueryString("sClient") & "&nRole=" & Request.QueryString("nRole")
                If Request.QueryString.Item("sCodispl") = "SCA102" Then
                    .sEditRecordParam = .sEditRecordParam & "&nSendAddr=" & mclsCertificat.nSendAddr
                End If
                .sDelRecordParam = "nRecOwner=" & mintRecowner & "&sKeyAddress=" & mstrKeyAddress & "&nKeyPhones='+ marrArray[lintIndex].tcnKeyPhones + '&sOnSeq=" & Request.QueryString("sOnSeq") & "&sClient=" & Request.QueryString("sClient") & "&nRole=" & Request.QueryString("nRole")

                '+ Se definen los botones de la grilla
                If (mstrRecType = "1" Or mstrRecType = "2") Then
                    .AddButton = True
                Else
                    .AddButton = False
                End If
            End With

            Call insShowPhones()
            Response.Write(mobjValues.BeginPageButton)
        End If

        With Response
            .Write(mobjValues.HiddenControl("tctRecType", mstrRecType))
            .Write(mobjValues.HiddenControl("tcnRecOwner", mintRecowner))
            .Write(mobjValues.HiddenControl("tctKeyAddress", mstrKeyAddress))
        End With

        '+ Si la ventana se está mostrando como PopUp y no desde secuencia, se muestra el comando
        '+ de "aceptar" 
        mblnActionQuery = mobjValues.ActionQuery
        If mstrOnSeq = "2" Then
            mobjValues.ActionQuery = False
            If mnMainAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
                Response.Write(mobjValues.ButtonAcceptCancel("StatusControl(true, 2);EnabledControl('fraFolder');top.frames['fraFolder'].document.forms[0].target='fraGeneric';setPointer('wait');", "if(typeof(top.opener.top.fraHeader)!='undefined') top.opener.top.fraHeader.setPointer('');top.close()", True))
            Else
                Response.Write(mobjValues.ButtonAcceptCancel(, , , , eFunctions.Values.eButtonsToShow.OnlyCancel))
            End If
        End If
        mobjValues.ActionQuery = mblnActionQuery

        mobjValues = Nothing

        If Request.QueryString.Item("Reload") = "1" Then
            '+ Se recarga la ventana PopUp, en caso que el check de "Continuar" se encuentre marcado
            Select Case Request.QueryString.Item("ReloadAction")
                Case "Add"
                    Response.Write("<SCRIPT>EditRecord(-1,nMainAction,'Add','" & mobjGrid.sEditRecordParam & "')</" & "Script>")
                Case "Update"
                    Response.Write("<SCRIPT>EditRecord(0" & Request.QueryString.Item("ReloadIndex") & ",nMainAction,'Update','" & mobjGrid.sEditRecordParam & "')</" & "Script>")
            End Select
        End If
    End Sub

    '%insreaAddress: Lee las direcciones asociadas de la tabla Address.
    '--------------------------------------------------------------------------------------------
    Private Sub insreaAddress(ByRef sCodispl As Object)
        '--------------------------------------------------------------------------------------------
        '- Objeto de direcciones
        Dim lcolAddresss As eGeneralForm.Addresss

        '- Objeto para obtener el "denunciante" (reclamante) de la póliza.
        Dim lclsClaimBenef As eClaim.ClaimBenef

        '- Variables para almacenar propiedades de objetos y variables de session
        Dim lstrClient As Object
        Dim lstrCertype As Object
        Dim lstrBranch As Object
        Dim lstrProduct As Object
        Dim lstrPolicy As Object
        Dim lstrCertif As Object
        Dim lstrEffecdate As String
        Dim lstrClaim As Object
        Dim lblnFind As Boolean
        Dim lstrRole As String

        '+ Se crean objetos a usar en proceso
        lcolAddresss = New eGeneralForm.Addresss

        '+ Se inicializan variables de trabajo    
        lstrClient = ""
        lstrCertype = ""
        lstrBranch = "0"
        lstrProduct = "0"
        lstrPolicy = "0"
        lstrCertif = "0"
        lstrEffecdate = "0"
        lstrClaim = "0"

        mdatClient = Today
        Session("dInpdate") = mdatClient

        '+ Si la página se carga por primera vez, entonces se asume que la dirección es de tipo particular
        '+ si no, se toma por defecto el tipo de dirección seleccionado de la página

        If Request.QueryString.Item("RecTypeReload") = vbNullString Then
            mstrRecType = "2"
        Else
            mstrRecType = Request.QueryString.Item("sRecType")
        End If

        Session("SCA101_dEffecDate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)

        Select Case sCodispl

        '+ Direccion de cliente 
            Case "SCA101"
                lstrClient = Session("sClient")
                mintRecowner = 2
                '+ Para las direcciones de cliente se asigna la fecha de ingreso del cliente como fecha de efecto    
                mdatClient = Session("dInpdate")
                Session("SCA101_dEffecDate") = Session("dInpdate")
            Case "SCA106"
                lstrClient = Session("sClient")
                mintRecowner = 2
            '+ Direccion de poliza 
            Case "SCA102"
                If mclsCertificat.nSendAddr = 4 Then
                    mintRecowner = 1
                    lstrClient = vbNullString
                    'mstrRecType = 0
                    mstrRecType = 1
                    lstrCertype = Session("sCertype")
                    lstrBranch = Session("nBranch")
                    lstrProduct = Session("nProduct")
                    lstrPolicy = Session("nPolicy")
                    lstrCertif = Session("nCertif")
                    Session("SCA101_dEffecDate") = Session("dEffecdate")
                ElseIf mclsCertificat.nSendAddr <> 0 Then
                    mintRecowner = 2
                    lstrClient = insreaRoles()
                    lstrCertype = vbNullString
                    lstrBranch = vbNullString
                    lstrProduct = vbNullString
                    lstrPolicy = vbNullString
                    lstrCertif = vbNullString
                End If
                mdatClient = Session("dEffecdate")

            '+ Ubicación del riego
            Case "SCA108"
                mdatClient = Session("SCA101_dEffecDate")
                lstrClient = vbNullString
                mintRecowner = 8
                mstrRecType = "1"
                lstrCertype = Session("sCertype")
                lstrBranch = Session("nBranch")
                lstrProduct = Session("nProduct")
                lstrPolicy = Session("nPolicy")
                lstrCertif = Session("nCertif")
                Session("SCA101_dEffecDate") = Session("dEffecdate")
                mdatClient = Session("dEffecdate")
            '+ Dirección de ocurrencia
            Case "SCA110"
                mintRecowner = 11
                mstrRecType = "1"
                lstrClient = vbNullString
                lstrCertype = Session("sCertype")
                lstrBranch = Session("nBranch")
                lstrProduct = Session("nProduct")
                lstrPolicy = Session("nPolicy")
                lstrCertif = Session("nCertif")
                lstrClaim = Session("nClaim")
                '+ Dirección del cliente en la poliza
            Case "SCA109"
                mintRecowner = 81
                lstrClient = Request.QueryString("sClient")
                mstrRecType = 2
                lstrCertype = Session("sCertype")
                lstrBranch = Session("nBranch")
                lstrProduct = Session("nProduct")
                lstrPolicy = Session("nPolicy")
                lstrCertif = Session("nCertif")
                lstrRole = Request.QueryString("nRole")
                Session("SCA101_dEffecDate") = Session("dEffecdate")
                mdatClient = Session("dEffecdate")

            Case "SCA735"
                mintRecowner = 13
                mstrRecType = "2"
                lstrCertype = Session("sCertype")
                lstrBranch = Session("nBranch")
                lstrProduct = Session("nProduct")
                lstrPolicy = Session("nPolicy")
                lstrCertif = Session("nCertif")
                lstrClaim = Session("nClaim")

                lclsClaimBenef = New eClaim.ClaimBenef

                '+ Se obtiene el código del denunciante (reclamante)		
                If lclsClaimBenef.Find_Demandant(mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCase_Num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
                    lstrClient = lclsClaimBenef.sClient
                End If
            Case "SCA778"
                mintRecowner = 12
                mstrRecType = "1"
                lstrCertype = "2"
                lstrBranch = Session("nBranch")
                lstrProduct = Session("nProduct")
                lstrPolicy = Session("nPolicy")
                lstrCertif = Session("nCertif")
                lstrClaim = Session("nClaim")

                lclsClaimBenef = New eClaim.ClaimBenef
                '+ Se obtiene el código del denunciante (reclamante)		
                If lclsClaimBenef.Find_Demandant(mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCase_Num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
                    lstrClient = lclsClaimBenef.sClient
                End If
        End Select

        '+ Se construye sKeyAddress    
        mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble),
                                                                mstrRecType,
                                                                lstrCertype,
                                                                mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble),
                                                                mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble),
                                                                lstrClient, 0, 0, 0, 0, ,
                                                                mobjValues.StringToType(lstrRole, eFunctions.Values.eTypeData.etdDouble))
        '+ Se recupera la dirección (de cualquier tipo), si existe	
        If Request.QueryString.Item("sCodispl") = "SCA102" Then
            lblnFind = mobjAddress.Find_PolAdd(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), mdatClient)
            If lblnFind Then
                mstrKeyAddress = mobjAddress.sKeyAddress
                mstrRecType = mobjAddress.sRecType
            End If
        Else
            lblnFind = mobjAddress.Find(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), mdatClient, True)
        End If
        mblnExistAddress = lblnFind

        '+ Se buscan los telefonos asociados a la direccion
        If CStr(Session("dInpdate")) <> vbNullString And Request.QueryString.Item("sCodispl") = "SCA101" Then
            mdatClientPhone = Session("dInpdate")
        Else
            mdatClientPhone = Session("SCA101_dEffecDate")
        End If

        If mobjValues.StringToType(Request.QueryString.Item("RecTypeReload"), eFunctions.Values.eTypeData.etdDouble) <> 1 And mblnExistAddress Then
            mblnExistPhones = mobjAddress.FindPhones(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), mdatClientPhone, True)
        Else
            If Request.QueryString.Item("ReloadAction") = "Add" Or Request.QueryString.Item("ReloadAction") = "Update" Then
                mblnExistPhones = mobjAddress.FindPhones(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), mdatClientPhone, False)
                If Not mblnExistPhones Then
                    mblnExistPhones = mobjAddress.FindPhones(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), mdatClientPhone, True)
                End If
            Else
                mblnExistPhones = mobjAddress.FindPhones(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), mdatClientPhone, True)
            End If
        End If

        '+ Se inicializan los datos para el manejo de pólizas
        If Request.QueryString.Item("sCodispl") = "SCA102" And mblnExistAddress Then
            mintRecowner = 1
            lstrClient = vbNullString
            lstrCertype = Session("sCertype")
            lstrBranch = Session("nBranch")
            lstrProduct = Session("nProduct")
            lstrPolicy = Session("nPolicy")
            lstrCertif = Session("nCertif")
            Session("SCA101_dEffecDate") = Session("dEffecdate")
            mstrRecType = "1"
            mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)
            If mclsCertificat.nSendAddr = 3 Then
                mstrRecType = "3"
            End If
        End If

        '+ Si la dirección no existe, pero se recarga con los campos del form

        If Not mblnExistAddress Or Request.QueryString("sUseQSAddress") = "1" Then
            mobjAddress.sStreet = Request.QueryString.Item("txtAddress")
            mobjAddress.sBuild = Request.QueryString.Item("tctBuild")
            mobjAddress.nFloor = mobjValues.StringToType(Request.QueryString.Item("tcnFloor"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.sDepartment = Request.QueryString.Item("tctDepartment")
            mobjAddress.sPopulation = Request.QueryString.Item("tctPopulation")
            mobjAddress.sDescadd = Request.QueryString.Item("tctDescadd")
            mobjAddress.nZip_Code = mobjValues.StringToType(Request.QueryString.Item("tcnZipCode"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nProvince = mobjValues.StringToType(Request.QueryString.Item("cbeProvince"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nLocal = mobjValues.StringToType(Request.QueryString.Item("valLocal"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nMunicipality = mobjValues.StringToType(Request.QueryString.Item("valMunicipality"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.sE_mail = Request.QueryString.Item("tctE_mail")
            mobjAddress.nNotInformEmail = mobjValues.StringToType(Request.QueryString.Item("cbeNoInformEmail"), eFunctions.Values.eTypeData.etdLong)
            mobjAddress.nLat_grade = mobjValues.StringToType(Request.QueryString.Item("tcnLatCardinG"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nLat_minute = mobjValues.StringToType(Request.QueryString.Item("tcnLatCardinM"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nLat_second = mobjValues.StringToType(Request.QueryString.Item("tcnLatCardinS"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nLon_grade = mobjValues.StringToType(Request.QueryString.Item("tcnLonCardinG"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nLon_minute = mobjValues.StringToType(Request.QueryString.Item("tcnLonCardinM"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.nLon_second = mobjValues.StringToType(Request.QueryString.Item("tcnLonCardinS"), eFunctions.Values.eTypeData.etdDouble)
            mobjAddress.sInfor = IIf(Request.QueryString.Item("chkInfor") = "true", "1", "2")
            mobjAddress.sSend_mail = IIf(Request.QueryString.Item("chkEmail") = "true", "1", "2")
        End If

        '+ Dirección de envío de correspondencia para siniestros
        If sCodispl = "SCA735" Then
            mintRecowner = 13
            mstrRecType = "2"
            lstrCertype = "2"
            lstrBranch = Session("nBranch")
            lstrProduct = Session("nProduct")
            lstrPolicy = Session("nPolicy")
            lstrCertif = Session("nCertif")
            lstrClaim = Session("nClaim")

            lclsClaimBenef = New eClaim.ClaimBenef

            '+ Se obtiene el código del denunciante (reclamante)		
            If lclsClaimBenef.Find_Demandant(mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCase_Num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
                lstrClient = lclsClaimBenef.sClient
            End If

            lclsClaimBenef = Nothing

            mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)

            lblnFind = mobjAddress.Find(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), Today, True)

            If Not lblnFind Then

                '+ Se construye la llave con el "nRecOwner" = 2 (Cliente), para mostrar por defecto la dirección del cliente en caso
                '+ de no existir con el tipo de dirección "Envío de correspondencia" (13)
                mstrKeyAddress = lcolAddresss.ConstructKeyAddress(2, CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)

                lblnFind = mobjAddress.Find(mstrKeyAddress, 2, Today, True)

                '+ Se busca si la dirección del cliente se especificó como "Dirección de envio" (sInfor="1")
                '+ En caso contrario se busca con los tipos de dirección "Comercial y Particular"
                If mobjAddress.sInfor = "" Then
                    mstrRecType = "2"
                    mstrKeyAddress = lcolAddresss.ConstructKeyAddress(2, CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)

                    lblnFind = mobjAddress.Find(mstrKeyAddress, 2, Today, True)
                ElseIf mobjAddress.sInfor = "2" Then
                    mstrRecType = "2"
                    mstrKeyAddress = lcolAddresss.ConstructKeyAddress(2, CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)

                    lblnFind = mobjAddress.Find(mstrKeyAddress, 2, Today, True)
                End If

                If lblnFind Then
                    '+ Se construye la nueva llave con el "nRecOwner" = 13 (Envío de correspondencia)
                    mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)
                End If
            End If
        End If

        '+ Dirección del reclamante del siniestro
        If sCodispl = "SCA778" Then
            mintRecowner = 12
            mstrRecType = "1"
            lstrCertype = "2"
            lstrBranch = Session("nBranch")
            lstrProduct = Session("nProduct")
            lstrPolicy = Session("nPolicy")
            lstrCertif = Session("nCertif")
            lstrClaim = Session("nClaim")

            mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), "", 0, 0, 0, 0)
            lblnFind = mobjAddress.Find(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), Today, True)
            If Not lblnFind Then
                lclsClaimBenef = New eClaim.ClaimBenef

                '+ Se obtiene el código del denunciante (reclamante)		
                If lclsClaimBenef.Find_Demandant(mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCase_Num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
                    lstrClient = lclsClaimBenef.sClient
                End If
                lclsClaimBenef = Nothing

                '+ Se construye la llave con el "nRecOwner" = 2 (Cliente), para mostrar por defecto la dirección del cliente en caso
                '+ de no existir con el tipo de dirección "Reclamante" (12)
                mstrKeyAddress = lcolAddresss.ConstructKeyAddress(2, CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)
                lblnFind = mobjAddress.Find(mstrKeyAddress, 2, Today, True)

                '+ Se construye la nueva llave con el "nRecOwner" = 12 (Reclamante)
                mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), lstrClient, 0, 0, 0, 0)
            End If
        End If ' sCodispl = "SCA778"

        '+ Si no existen datos asociados a la póliza se buscan los datos del cliente
        If Not lblnFind And sCodispl = "SCA108" Then
            mintRecowner = 2
            mstrRecType = "2"
            lstrClient = insreaRoles()
            mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)

            lblnFind = mobjAddress.Find(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), Today, True)
            If Not lblnFind Then
                mstrRecType = "1"
                mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)

                lblnFind = mobjAddress.Find(mstrKeyAddress, mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), Today, True)
            End If

            '+ Se inicializan los datos para el manejo de pólizas		
            mstrRecType = "1"
            mintRecowner = 8
            lstrClient = vbNullString
            lstrCertype = Session("sCertype")
            lstrBranch = Session("nBranch")
            lstrProduct = Session("nProduct")
            lstrPolicy = Session("nPolicy")
            lstrCertif = Session("nCertif")
            mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)
        End If ' Not lblnFind And sCodispl = "SCA108"

        Session("Find_address") = 1

        If Not lblnFind Then
            Session("Find_address") = 2
            If Not IsNothing(Request.QueryString.Item("Reload")) Then
                If Not mobjValues.ActionQuery Then
                    mobjAddress.nCountry = Session("nCountry")
                End If
            Else
                Select Case sCodispl
                    Case "SCA108"
                        mobjAddress.nCountry = Session("nCountry")

                    Case "SCA101", "SCA106"
                        mstrKeyAddress = lcolAddresss.ConstructKeyAddress(mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), CShort(CStr(mstrRecType)), CStr(lstrCertype), mobjValues.StringToType(lstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lstrClaim, eFunctions.Values.eTypeData.etdDouble), CStr(lstrClient), 0, 0, 0, 0)
                        If mobjAddress.Find(CStr(mstrKeyAddress), mobjValues.StringToType(mintRecowner, eFunctions.Values.eTypeData.etdDouble), Today, True) Then
                            Session("SCA101_dEffecDate") = mobjAddress.dEffecdate
                        Else
                            Session("SCA101_dEffecDate") = Session("dInpdate")
                            If Not mobjValues.ActionQuery Then
                                mobjAddress.nCountry = Session("nCountry")
                            End If
                        End If
                End Select
            End If
        End If
        lcolAddresss = Nothing
    End Sub

    '%insShowPhones. Esta funcion se encarga de mostrar los teléfonos de la dirección
    '%en tratamiento (mobjAddress)
    '----------------------------------------------------------------------------------1
    Private Sub insShowPhones()
        '----------------------------------------------------------------------------------
        '- Objeto para almacenar un telefonos asociados a la dirección
        Dim lobjPhones As Object

        If Not mobjAddress.Phones Is Nothing Then
            For Each lobjPhones In mobjAddress.Phones
                With mobjGrid
                    .Columns("tcnOrder").DefValue = lobjPhones.nOrder
                    .Columns("cbePhoneType").DefValue = lobjPhones.nPhone_type
                    .Columns("tcnArea").DefValue = lobjPhones.nArea_code
                    .Columns("tctPhone").DefValue = lobjPhones.sPhone
                    .Columns("tcnExtensi1").DefValue = lobjPhones.nExtens1
                    .Columns("tcnExtensi2").DefValue = lobjPhones.nExtens2
                    .Columns("tcnKeyPhones").DefValue = lobjPhones.nKeyPhones
                    Response.Write(.DoRow)
                End With
            Next lobjPhones
        End If

        Response.Write(mobjGrid.closeTable())
        mobjAddress.Phones = Nothing
    End Sub

    '%insPreSCA001Upd: Muestra ventana para editar registros de grilla
    '----------------------------------------------------------------------------------
    Private Sub insPreSCA001Upd()
        '----------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("//% UpdateFields: actualiza los campos ocultos con los campos puntuales de la SCA001" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function UpdateFields(){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	with(self.document.forms[0]){" & vbCrLf)
        Response.Write("		txtAddress.value = top.opener.document.forms[0].txtAddress.value;" & vbCrLf)
        Response.Write("		tctBuild.value = top.opener.document.forms[0].tctBuild.value;" & vbCrLf)
        Response.Write("		tcnFloor.value = top.opener.document.forms[0].tcnFloor.value;" & vbCrLf)
        Response.Write("		tctDepartment.value = top.opener.document.forms[0].tctDepartment.value;" & vbCrLf)
        Response.Write("		tctPopulation.value = top.opener.document.forms[0].tctPopulation.value;" & vbCrLf)
        Response.Write("        tctDescadd.value = top.opener.document.forms[0].tctDescadd.value;" & vbCrLf)
        Response.Write("        tcnZipCode.value = top.opener.document.forms[0].tcnZipCode.value;" & vbCrLf)
        Response.Write("        cbeProvince.value = top.opener.document.forms[0].cbeProvince.value;" & vbCrLf)
        Response.Write("        valLocal.value = top.opener.document.forms[0].valLocal.value;" & vbCrLf)
        Response.Write("        valMunicipality.value = top.opener.document.forms[0].valMunicipality.value;" & vbCrLf)
        Response.Write("        chkInfor.value = top.opener.document.forms[0].chkInfor.checked;" & vbCrLf)
        Response.Write("        chkEmail.value = top.opener.document.forms[0].chkEmail.checked;" & vbCrLf)
        Response.Write("        tctE_mail.value	= top.opener.document.forms[0].tctE_mail.value;" & vbCrLf)
        Response.Write("        cbeNoInformEmail.value = top.opener.document.forms[0].cbeNoInformEmail.value;" & vbCrLf)
        Response.Write("        chkdeldir.checked = top.opener.document.forms[0].chkdeldir.checked;" & vbCrLf)
        Response.Write("        tcnLatCardinG.value = top.opener.document.forms[0].tcnLatCardinG.value;" & vbCrLf)
        Response.Write("        tcnLatCardinM.value = top.opener.document.forms[0].tcnLatCardinM.value;" & vbCrLf)
        Response.Write("        tcnLatCardinS.value = top.opener.document.forms[0].tcnLatCardinS.value;" & vbCrLf)
        Response.Write("        tcnLonCardinG.value = top.opener.document.forms[0].tcnLonCardinG.value;" & vbCrLf)
        Response.Write("        tcnLonCardinM.value = top.opener.document.forms[0].tcnLonCardinM.value;" & vbCrLf)
        Response.Write("        tcnLonCardinS.value = top.opener.document.forms[0].tcnLonCardinS.value;" & vbCrLf)
        Response.Write("        tctRecType.value = top.opener.document.forms[0].tctRecType.value;" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "SCRIPT>")


        '- Objetos de teléfono    
        Dim lobjPhone As eGeneralForm.Phone
        Dim lclsPhones As eGeneralForm.Phones

        '- Nuevo correlativo para número telefónico    
        Dim lintNext As Integer
        With Response
            .Write(mobjValues.HiddenControl("txtAddress", ""))
            .Write(mobjValues.HiddenControl("tctBuild", ""))
            .Write(mobjValues.HiddenControl("tcnFloor", ""))
            .Write(mobjValues.HiddenControl("tctDepartment", ""))
            .Write(mobjValues.HiddenControl("tctPopulation", ""))
            .Write(mobjValues.HiddenControl("tctDescadd", ""))
            .Write(mobjValues.HiddenControl("tcnZipCode", ""))
            .Write(mobjValues.HiddenControl("cbeProvince", ""))
            .Write(mobjValues.HiddenControl("valLocal", ""))
            .Write(mobjValues.HiddenControl("valMunicipality", ""))
            .Write(mobjValues.HiddenControl("chkInfor", ""))
            .Write(mobjValues.HiddenControl("chkEmail", ""))
            .Write(mobjValues.HiddenControl("tctE_mail", ""))
            .Write(mobjValues.HiddenControl("cbeNoInformEmail", ""))
            .Write(mobjValues.HiddenControl("chkdeldir", ""))
            .Write(mobjValues.HiddenControl("tcnLatCardinG", ""))
            .Write(mobjValues.HiddenControl("tcnLatCardinM", ""))
            .Write(mobjValues.HiddenControl("tcnLatCardinS", ""))
            .Write(mobjValues.HiddenControl("tcnLonCardinG", ""))
            .Write(mobjValues.HiddenControl("tcnLonCardinM", ""))
            .Write(mobjValues.HiddenControl("tcnLonCardinS", ""))
            .Write(mobjValues.HiddenControl("tctRecType", ""))
            .Write(mobjValues.HiddenControl("hddSendAddr", Request.QueryString.Item("nSendAddr")))
        End With

        Response.Write("<SCRIPT>UpdateFields()</" & "Script>")

        '+ Eliminacion de teléfono    
        Dim lobjAddress As eGeneralForm.Address
        If Request.QueryString.Item("Action") = "Del" Then
            lobjPhone = New eGeneralForm.Phone
            With Request
                Call lobjPhone.Find(Request.QueryString.Item("sKeyAddress"), mobjValues.StringToType(Request.QueryString.Item("nKeyPhones"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRecowner"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SCA101_dEffecDate"), eFunctions.Values.eTypeData.etdDate))
            End With
            lobjPhone.Delete()
            lobjPhone = Nothing

            '+ Actualizar desde la tabla temporal la tabla oficial Phones
            lobjAddress = New eGeneralForm.Address
            Call lobjAddress.UpdatePhones(Request.QueryString.Item("sKeyAddress"), mobjValues.StringToType(Request.QueryString.Item("nRecowner"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("SCA101_dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
            lobjAddress = Nothing

            Response.Write(mobjValues.ConfirmDelete)
        End If

        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValGeneralForm.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))

        With mobjValues
            Response.Write(.HiddenControl("tctKeyAddress", Request.QueryString.Item("sKeyAddress")))
            Response.Write(.HiddenControl("tcnRecOwner", Request.QueryString.Item("nRecOwner")))
        End With

        '+ Se obtiene el máximo correlativo para ingresar un nuevo teléfono
        If Request.QueryString.Item("Action") = "Add" Then
            lclsPhones = New eGeneralForm.Phones
            lintNext = lclsPhones.insMaxPhone(mobjValues.StringToType(Request.QueryString.Item("nRecOwner"), eFunctions.Values.eTypeData.etdDouble), CShort(Request.QueryString.Item("sRecType")), Request.QueryString.Item("sKeyAddress"), mobjValues.StringToType(Session("SCA101_dEffecDate"), eFunctions.Values.eTypeData.etdDate))
            lclsPhones = Nothing
            Response.Write("<SCRIPT>document.forms[0].tcnOrder.value='" & lintNext & "'</" & "Script>")
        End If
    End Sub

    '% insreaRoles: se busca el RUT del asegurado de la póliza
    '--------------------------------------------------------------------------------------------
    Private Function insreaRoles() As Object
        '--------------------------------------------------------------------------------------------
        '- Objeto con roles de la poliza     
        Dim lclsRoles As ePolicy.Roles
        Dim lstrClient As Object
        lclsRoles = New ePolicy.Roles

        lstrClient = vbNullString
        If lclsRoles.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), 2, vbNullString, mobjValues.StringToType(Session("SCA101_dEffecDate"), eFunctions.Values.eTypeData.etdDate)) Then
            lstrClient = lclsRoles.SCLIENT
        End If
        insreaRoles = lstrClient
        lclsRoles = Nothing
    End Function

</script>
<%Response.Expires = -1

    mobjAddress = New eGeneralForm.Address
    mobjValues = New eFunctions.Values

    mobjValues.sCodisplPage = "sca001"

    mstrLastClient = Session("sClient")
    mblnLastAction = Session("bQuery")

    If Request.QueryString.Item("nMainAction") = "undefined" Then
        mnMainAction = 0
    Else
        mnMainAction = Request.QueryString.Item("nMainAction")
    End If

    mobjValues.ActionQuery = Session("bQuery") Or mnMainAction = 401
    mstrQuote = """"
    mstrAmp = "&"

    If Request.QueryString.Item("sOnSeq") = "2" Then
        If mnMainAction <> eFunctions.Menues.TypeActions.clngActionUpdate Then
            Session("bQuery") = True
        End If
        Session("sClient") = Request.QueryString.Item("sClient")
        mstrOnSeq = "2"
    Else
        mstrOnSeq = "1"
    End If

    If Request.QueryString.Item("sCodispl") = "SCA102" Then
        mclsCertificat = New ePolicy.Certificat
        Call mclsCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"))
        If Request.QueryString.Item("nSendAddr") <> vbNullString Then
            mclsCertificat.nSendAddr = CInt(Request.QueryString.Item("nSendAddr"))
        Else
            If Request.QueryString.Item("sRectype") <> vbNullString Then
                mclsCertificat.nSendAddr = CInt(Request.QueryString.Item("sRectype"))
            End If
        End If
    End If
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">  
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 9 $|$$Date: 24/09/04 15.36 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
    <%
        mobjMenu = New eFunctions.Menues
        With Response
            If Request.QueryString.Item("Type") <> "PopUp" And mstrOnSeq = "1" Then
                .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "SCA001.aspx"))
            End If
            .Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
        End With
        mobjMenu = Nothing
%>
<SCRIPT>
//%insChangeAddress: Actualiza los campos de las direcciones
//---------------------------------------------------------------------------
function insChangeAddress(){
//---------------------------------------------------------------------------
	with(document.forms[0]){
		tctDescadd.value= txtAddress.value + ' ' + tctBuild.value;
		if((tctDepartment.value!=0)&&
		   (tctDepartment.value!=''))
			tctDescadd.value += ', Dpto. ' + tctDepartment.value 
		if((tcnFloor.value!=0)&&
		   (tcnFloor.value!='')) 
			tctDescadd.value += ', Piso ' + tcnFloor.value;
		if((tctPopulation.value!=0)&&
		   (tctPopulation.value!='')) 
			tctDescadd.value += ', ' + tctPopulation.value;
	}
}	
//%insParameterLocat: Actualiza parametros de la region
//---------------------------------------------------------------------------
function insParameterLocat(Field){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		valLocal.Parameters.Param1.sValue=Field.value;
		valMunicipality.Parameters.Param1.sValue=0;
		valMunicipality.Parameters.Param2.sValue=Field.value;		
		valLocal.disabled=(Field.value=='')?true:false;
		valLocal.value='';
		UpdateDiv('valLocalDesc','')
		valMunicipality.value='';
		UpdateDiv('valMunicipalityDesc','')
	}
	
}	
//%insParameterMunicipality: Actualiza parametros de la comuna
//---------------------------------------------------------------------------
function insParameterMunicipality(Field){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		valMunicipality.Parameters.Param1.sValue=Field.value;
		valMunicipality.Parameters.Param2.sValue=cbeProvince.value;
		
		if (Field.value == '')
			valMunicipality.Parameters.Param1.sValue=0;
		
		valMunicipality.disabled=(Field.value=='')?true:false;
		if(valMunicipality_nLocal.value!=Field.value){
			valMunicipality.value='';
			UpdateDiv('valMunicipalityDesc','')
		}
	}
}	
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
   <P ALIGN="CENTER">
   <LABEL><A HREF="#Coord"><%= GetLocalResourceObject("AnchorCoordCaption") %></A></LABEL> | 
   <LABEL><A HREF="#Phones"><%= GetLocalResourceObject("AnchorPhonesCaption") %></A><LABEL>
   </P>
<%End If%>
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"))%>
<FORM METHOD="POST" ID="FORM" NAME="frmSCA001" ACTION="valGeneralForm.aspx?nRecOwner=<%=mintRecowner%><%=mstrAmp%>&sKeyAddress=<%=mstrKeyAddress%><%=mstrAmp%>&nMainAction=<%=Request.QueryString.Item("nMainAction")%><%=mstrAmp%>&sOnSeq=<%=mstrOnSeq%><%=mstrAmp%>&sRecType=<%=mstrRecType%>&sCodispl=<%=Request.QueryString.Item("sCodispl")%>">
<%--<FORM METHOD="POST" ID="FORM" NAME="frmSCA001" ACTION="valGeneralForm.aspx?nRecOwner=<%=mintRecowner%><%=mstrAmp%>&sKeyAddress=<%=mstrKeyAddress%><%=mstrAmp%>&nMainAction=<%=Request.QueryString.Item("nMainAction")%><%=mstrAmp%>&sOnSeq=<%=mstrOnSeq%><%=mstrAmp%>&sRecType=<%=mstrRecType%>">--%>
<TABLE WIDTH="100%" border=0>
<%

    Call insDefineHeader()
    If Request.QueryString.Item("Type") = "PopUp" Then
        Call insPreSCA001Upd()
    Else
        Call insPreSCA001()
    End If

    Session("sClient") = mstrLastClient
    Session("bQuery") = mblnLastAction

    mobjAddress = Nothing
    mobjGrid = Nothing

%>
</TABLE>
</FORM>
</BODY>
</HTML>
<%
    '+ Cuando es invocada de otra transaccion se deshabilitan los campos que viene con datos
    If CStr(Session("sOriginalForm")) <> vbNullString Then
        Response.Write("<SCRIPT>insEnabledFields();</SCRIPT>")
    End If

    If Request.QueryString.Item("sCodispl") = "SCA102" And Request.QueryString.Item("Type") <> "PopUp" Then
        '+ Si la direccion de envío de poliza es distinta a "Por Póliza", 
        '+ entonces no se puede permitir incluir información  de dirección
        Response.Write("<SCRIPT>")
        If mclsCertificat.nSendAddr <> 4 Then
            If (Session("nTransaction") <> 12 And Session("nTransaction") <> 14) Then
                Response.Write("alert(""No puede incluir dirección de envío de póliza (Datos para la facturación)"");")
            End If
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Response.Write("insEnabledFieldsPolicy();")
            End If
        End If
        Response.Write("</SCRIPT>")
    End If

    mclsCertificat = Nothing
%>





