<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    Dim mobjMenu As eFunctions.Menues

    '- Tipo de compañia segun las opciones de instalación del sistema
    Dim mstrTypeCompany As Object

    '- Objeto para buscar los datos del cliente asociado al usuario    
    Dim mclsPolicy As ePolicy.Policy

    '- Variables para almacenar parametros pasados a transaccion
    Dim mintAction As String
    Dim mintTransaction As Object
    Dim mstrCertype As String
    Dim mintBranch As String
    Dim mintProduct As String
    Dim mlngPolicy As String
    Dim mlngProponum As String
    Dim mlngCertif As String
    Dim mdtmEffecdate As String


    Dim mclsSche_Transac As eSecurity.Secur_sche


    '% LoadPageInSequence: se carga la página cuando se encuentra dentro de la secuencia
    '--------------------------------------------------------------------------------------------
    Private Sub LoadPageInSequence()
        '--------------------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT LANGUAGE=javascript>" & vbCrLf)
        Response.Write("<!--" & vbCrLf)
        Response.Write("//%insShowNextWindow. Se encarga de mostrar la siguiente ventana a ser mostrada" & vbCrLf)
        Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insShowNextWindow(){" & vbCrLf)
        Response.Write("//--------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("    var lblnDoIt=true;" & vbCrLf)
        Response.Write("    if (typeof(top.frames['fraSequence'])!='undefined')" & vbCrLf)
        Response.Write("        if (typeof(top.frames['fraSequence'].NextWindows)!='undefined'){" & vbCrLf)
        Response.Write("            top.frames['fraSequence'].NextWindows('');" & vbCrLf)
        Response.Write("            lblnDoIt = false;" & vbCrLf)
        Response.Write("        }" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("//-->" & vbCrLf)
        Response.Write("</" & "SCRIPT>" & vbCrLf)
        Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        <LABEL>" & GetLocalResourceObject("lblDesBranchCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;" & vbCrLf)
        Response.Write("        ")


        Response.Write(mobjValues.BranchControl("lblDesBranch", GetLocalResourceObject("lblDesBranchToolTip"), Session("nBranch"),  , True) & "/" & mobjValues.ProductControl("valProduct", "Producto asociado a la póliza", Session("nBranch"), eFunctions.Values.eValuesType.clngComboType,  , Session("nProduct"), True))

        Response.Write("" & vbCrLf)
        Response.Write("        </TD>" & vbCrLf)
        Response.Write("        ")

        If Session("nTransaction3") = eCollection.Premium.PolTransac.clngQuotPropAmendentConvertion Then
            Response.Write("" & vbCrLf)
            Response.Write("            <TD><DIV ID=""lblPolicyNum"">")


            Response.Write(mclsPolicy.TransactionCA001(mobjValues.StringToType(CStr(26), eFunctions.Values.eTypeData.etdDouble), True))


            Response.Write("</DIV></TD>" & vbCrLf)
            Response.Write("        ")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("            <TD><DIV ID=""lblPolicyNum"">")


            Response.Write(mclsPolicy.TransactionCA001(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), True))


            Response.Write("</DIV></TD>" & vbCrLf)
            Response.Write("        ")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("        <TD ALIGN=""Right"">")


        Response.Write(mobjValues.NumericControl("lblNumPolicy", 8, Session("nPolicy"),  , "", False,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        <TD ALIGN=""Center"">/</TD>" & vbCrLf)
        Response.Write("        <TD>")


        Response.Write(mobjValues.NumericControl("lblCertif", 8, Session("nCertif"),  , "", False,  , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("       </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("        <TD>" & vbCrLf)
        Response.Write("        <LABEL>" & GetLocalResourceObject("lblEffecdateCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;" & vbCrLf)
        Response.Write("            ")


        Response.Write(mobjValues.DateControl("lblEffecdate", Session("dEffecdate"),  , GetLocalResourceObject("lblEffecdateToolTip"), True))


        Response.Write(" " & vbCrLf)
        Response.Write("         </TD>" & vbCrLf)
        Response.Write("         ")

        If Session("nTransaction") = 25 Or Session("nTransaction") = 26 Or Session("nTransaction") = 24 Then
            Response.Write("" & vbCrLf)
            Response.Write("          <TD>" & vbCrLf)
            Response.Write("          <LABEL>")


            Response.Write(mclsPolicy.TransactionCA001(mobjValues.StringToType(CStr(1), eFunctions.Values.eTypeData.etdDouble), True))


            Response.Write("</LABEL>" & vbCrLf)
            Response.Write("          </TD>      " & vbCrLf)
            Response.Write("          <TD ALIGN=""Right"">")


            Response.Write(mobjValues.NumericControl("lblNumPolicy", 8, Session("nPolicy_old"),  , "", False,  , True))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("        <TD ALIGN=""Center"">/</TD>" & vbCrLf)
            Response.Write("        <TD>")


            Response.Write(mobjValues.NumericControl("lblCertif", 8, Session("nCertif"),  , "", False,  , True))


            Response.Write("</TD>      " & vbCrLf)
            Response.Write("       ")

        End If
        Response.Write("  " & vbCrLf)
        Response.Write("    </TR>" & vbCrLf)
        Response.Write("</TABLE>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>insShowNextWindow();</" & "SCRIPT>")


    End Sub

    '% LoadPageStart:Carga la página como inicial
    '-------------------------------------------------------------------------------------------
    Private Sub LoadPageStart()
        Dim strinst As String
        '-------------------------------------------------------------------------------------------
        Session("CallSequence") = ""

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("//- Variable para indicar si se limpian campos" & vbCrLf)
        Response.Write("    var mblnCleanField" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//- Variable qie contiene el tipo enumerado para identificar el tipo de compañía" & vbCrLf)
        Response.Write("    var eCompanyType = new eCompanyType()" & vbCrLf)
        Response.Write("    " & vbCrLf)
        Response.Write("//var eTypeAction  = new TypeAction()" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//- Variable que almacena el tipo de la compañía usuaria" & vbCrLf)
        Response.Write("    var mstrCompanyType = '")


        Response.Write(Session("sTypeCompanyUser"))


        Response.Write("'" & vbCrLf)
        Response.Write("    " & vbCrLf)

        Response.Write("//- Variable que almacena la oficina del usuario de tipo intermediario que maneja la página" & vbCrLf)
        Response.Write("    var mintUserOffice = '")
        If Session("sTypeUser") = "3" Then
            Response.Write(Session("nOffice"))
        End If
        Response.Write("'" & vbCrLf)
        Response.Write("    " & vbCrLf)

        Response.Write("//- Variable que almacena la agencia del usuario de tipo intermediario que maneja la página" & vbCrLf)
        Response.Write("    var mintUserOfficeAgen = '")
        If Session("sTypeUser") = "3" Then
            Response.Write(Session("nOfficeAgen"))
        End If
        Response.Write("'" & vbCrLf)
        Response.Write("    " & vbCrLf)

        Response.Write("//- Variable que almacena la sucursal del usuario de tipo intermediario que maneja la página" & vbCrLf)
        Response.Write("    var mintUserAgency = '")
        If Session("sTypeUser") = "3" Then
            Response.Write(Session("nAgency"))
        End If
        Response.Write("'" & vbCrLf)
        Response.Write("    " & vbCrLf)


        Response.Write("// - Variable para definir el tipo de documento (póliza, Solicitud, Cotización)" & vbCrLf)
        Response.Write("    var mstrCertype" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//-Indicador de carga de menu" & vbCrLf)
        Response.Write("  var mintMenu = '")


        Response.Write(Request.QueryString.Item("bMenu"))


        Response.Write("';" & vbCrLf)
        Response.Write("  " & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//-Descripciones del campo póliza según transaccion" & vbCrLf)

        Response.Write("    var mstrPolicyDescript1 = '")
        Response.Write(mclsPolicy.TransactionCA001(1, True))
        Response.Write("';" & vbCrLf)

        Response.Write("    var mstrPolicyDescript4 = '")
        Response.Write(mclsPolicy.TransactionCA001(4, True))
        Response.Write("';" & vbCrLf)

        Response.Write("    var mstrPolicyDescript6 = '")
        Response.Write(mclsPolicy.TransactionCA001(6, True))
        Response.Write("';" & vbCrLf)

        Response.Write("    var mstrPolicyDescript43 = '")
        Response.Write(mclsPolicy.TransactionCA001(43, True))
        Response.Write("';" & vbCrLf)



        Response.Write("" & vbCrLf)
        Response.Write("//-Correspondencia Javascript de variables VbScript" & vbCrLf)
        Response.Write("    var mdtmEffecdate   = '")

        Response.Write(mdtmEffecdate)


        Response.Write("'" & vbCrLf)
        Response.Write("    var mintBranch      = '")


        Response.Write(mintBranch)


        Response.Write("'" & vbCrLf)
        Response.Write("    var mintProduct     = '")


        Response.Write(mintProduct)


        Response.Write("'" & vbCrLf)
        Response.Write("    var mintPolicy      = '")


        Response.Write(mlngPolicy)


        Response.Write("'" & vbCrLf)
        Response.Write("    var mintProponum    = '")


        Response.Write(mlngProponum)


        Response.Write("'" & vbCrLf)
        Response.Write("    var mintCertificat  = '")


        Response.Write(mlngCertif)


        Response.Write("'" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("</" & "SCRIPT>")

        '$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\policy\policyseq\VTime\Scripts\CA001_K.js#
        Response.Write("")

        Response.Write("")

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/CA001_K.js""></" & "SCRIPT>" & vbCrLf)
        Response.Write("<TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
        Response.Write("<TR>" & vbCrLf)
        Response.Write("    <TD COLSPAN=""2""  VALIGN=TOP>" & vbCrLf)
        Response.Write("        <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
        Response.Write("            <TR>            " & vbCrLf)
        Response.Write("             ")

        If Request.QueryString.Item("sCodispl") = "CA001C" Then
            Response.Write("<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Clave"">" & GetLocalResourceObject("AnchorClaveCaption") & "</A></LABEL></TD>")
        Else
            Response.Write("<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Clave"">" & GetLocalResourceObject("AnchorClave2Caption") & "</A></LABEL></TD>")
        End If

        Response.Write("    " & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD COLSPAN=""2"" CLASS=""Horline""></TR>  " & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=13386>" & GetLocalResourceObject("cbeTransactioCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                <TD>" & vbCrLf)
        Response.Write("                    ")

        If Request.QueryString.Item("sCodispl") = "CA001C" Then
            strinst = "insSelTransaction();" & "InsOfficeca001c(" & Session("nOffice") & ")"
            Response.Write(mobjValues.ComboControl("cbeTransactio", mclsSche_Transac.Sche_Transac(Session("sSche_code"), Request.QueryString.Item("sCodispl")), "4", True, 1, GetLocalResourceObject("cbeTransactioToolTip"), strinst, Request.QueryString.Item("sCodispl_orig") = "CA099C"))
        Else
            Response.Write(mobjValues.ComboControl("cbeTransactio", mclsSche_Transac.Sche_Transac(Session("sSche_code"), Request.QueryString.Item("sCodispl")), mintTransaction, True, 1, GetLocalResourceObject("cbeTransactioToolTip"), "insSelTransaction()", Request.QueryString.Item("sCodisplOrig") = "CAC011"))
        End If
        Response.Write("</TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=13374><DIV ID=""divEffecdate"">" & GetLocalResourceObject("tcdEffecdateCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divEffecdate2"">")


        Response.Write(mobjValues.DateControl("tcdEffecdate", Request.Form.Item("tcdEffecdate"), , GetLocalResourceObject("tcdEffecdateToolTip"), , , , "ShowChangeValues(""Product"")"))


        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=13378><DIV ID=""divOffice"">" & GetLocalResourceObject("cbeOfficeCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divOffice2"">" & vbCrLf)
        Response.Write("                ")


        mobjValues.TypeOrder = 1
        If Request.QueryString.Item("sCodispl") = "CA001C" Then
            strinst = "InsOfficeca001c(" & Session("nOffice") & ");"
            mobjValues.Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)
            mobjValues.BlankPosition = True

            '+ Si el usuario es tipo intermediario se coloca por defecto la office
            If Session("sTypeUser") = "3" Then
                Response.Write(mobjValues.PossiblesValues("cbeOffice", "tabOfficeUser", 1, Session("nOffice"), True, , , , , strinst, True, , GetLocalResourceObject("cbeOfficeToolTip")))
            Else
                Response.Write(mobjValues.PossiblesValues("cbeOffice", "tabOfficeUser", 1, , True, , , , , strinst, True, , GetLocalResourceObject("cbeOfficeToolTip")))
            End If
        Else
            mobjValues.Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)
            mobjValues.BlankPosition = True

            '+ Si el usuario es tipo intermediario se coloca por defecto la office
            If Session("sTypeUser") = "3" Then
                Response.Write(mobjValues.PossiblesValues("cbeOffice", "tabOfficeUser", 1, Session("nOffice"), True, , , , , "BlankOfficeDepend();insInitialAgency(1)", , , GetLocalResourceObject("cbeOfficeToolTip")))
            Else
                Response.Write(mobjValues.PossiblesValues("cbeOffice", "tabOfficeUser", 1, , True, , , , , "BlankOfficeDepend();insInitialAgency(1)", , , GetLocalResourceObject("cbeOfficeToolTip")))
            End If

        End If

        Response.Write("" & vbCrLf)
        Response.Write("                </DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=0><DIV ID=""divOfficeA"">" & GetLocalResourceObject("cbeOfficeAgenCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divOfficeA2"">" & vbCrLf)
        Response.Write("                ")


        With mobjValues

            '+ Si el usuario es tipo intermediario se coloca por defecto la office
            If Session("sTypeUser") = "3" Then
                .Parameters.Add("nOfficeAgen", Session("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nBran_off", , , True)

            If Request.QueryString.Item("sCodispl") = "CA001C" Then
                strinst = "InsOfficeAgenca001c(" & Session("nOfficeAgen") & ")"
                Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, Session("nOfficeAgen"), True, , , , , strinst, True, , GetLocalResourceObject("cbeOfficeAgenToolTip")))
            Else
                '+ Si el usuario es tipo intermediario se coloca por defecto la office
                If Session("sTypeUser") = "3" Then
                    Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, Session("nOfficeAgen"), True, , , , , "insInitialAgency(2)", , , GetLocalResourceObject("cbeOfficeAgenToolTip")))
                Else
                    Response.Write(.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("cbeOfficeAgen"), True, , , , , "insInitialAgency(2)", , , GetLocalResourceObject("cbeOfficeAgenToolTip")))
                End If

            End If
        End With

        Response.Write("</DIV>" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=0><DIV ID=""divAgency"">" & GetLocalResourceObject("cbeAgencyCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divAgency2"">" & vbCrLf)
        Response.Write("                ")


        With mobjValues
            '+ Si el usuario es tipo intermediario se coloca por defecto la office
            If Session("sTypeUser") = "3" Then
                .Parameters.Add("nOfficeAgen", Session("nOffice"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAgency", Session("nOfficeAgen"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Else
                .Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End If
            .Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)

            .Parameters.ReturnValue("nBran_off", , , True)
            .Parameters.ReturnValue("nOfficeAgen", , , True)
            .Parameters.ReturnValue("sDesAgen", , , True)
            If Request.QueryString.Item("sCodispl") = "CA001C" Then
                strinst = "InsAgencyca001c(" & Session("nAgency") & ")"
                Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555A", eFunctions.Values.eValuesType.clngWindowType, Session("nAgency"), True, , , , , strinst, True, , GetLocalResourceObject("cbeAgencyToolTip")))
            Else
                '+ Si el usuario es tipo intermediario se coloca por defecto la office
                If Session("sTypeUser") = "3" Then
                    Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555A", eFunctions.Values.eValuesType.clngWindowType, Session("nAgency"), True, , , , , "insInitialAgency(3)", , , GetLocalResourceObject("cbeAgencyToolTip")))
                Else
                    Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555A", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("cbeAgency"), True, , , , , "insInitialAgency(3)", , , GetLocalResourceObject("cbeAgencyToolTip")))
                End If
            End If
        End With

        Response.Write("</DIV>" & vbCrLf)
        Response.Write("                " & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=0><DIV ID=""divChannel"">" & GetLocalResourceObject("cbeSellchannelCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                    <TD><DIV ID=""divChannel2"">" & vbCrLf)
        Response.Write("                     ")

        If Request.QueryString.Item("sCodispl") = "CA001C" Then
            Response.Write(mobjValues.PossiblesValues("cbeSellchannel", "Table5532", 1, CStr(1), , , , , , , , , GetLocalResourceObject("cbeSellchannelToolTip")))
        Else
            Response.Write(mobjValues.PossiblesValues("cbeSellchannel", "Table5532", 1, CStr(1), , , , , , , , , GetLocalResourceObject("cbeSellchannelToolTip")))
        End If

        Response.Write("</DIV></TD>                    " & vbCrLf)
        Response.Write("            </TR>                                                            " & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=13372>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                <TD>")


        Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  mintBranch, "valProduct", , , , "ShowChangeValues(""nBranch"")"))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("            </TR> " & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=13382>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                <TD>")


        Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), , mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble) <= 0, mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), , , , "ShowChangeValues(""Product"")"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""lblPolicyNum"">" & vbCrLf)
        Response.Write("                ")

        If Request.QueryString.Item("sCodispl") = "CA001C" Then
            Response.Write("<LABEL ID=13382>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL>")
        Else
            Response.Write(mclsPolicy.TransactionCA001(1, True))
        End If
        Response.Write(" " & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("                  " & vbCrLf)
        Response.Write("                <TD>")


        Response.Write(mobjValues.PolicyControl("tcnPolicy", GetLocalResourceObject("tcnPolicyToolTip"), "cbeBranch", mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), "valProduct", mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mstrCertype, mlngPolicy, "tcnCertificat", mlngCertif, , , , "LockControl(""Policy"");ShowChangeValues(""Policy"")", True))


        Response.Write("" & vbCrLf)
        Response.Write("				" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("				<TD><LABEL ID=0><DIV ID=""divPol_dest"">" & GetLocalResourceObject("tcnPolicyDestCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("			    <TD><DIV ID=""divPol_dest2"">" & vbCrLf)
        Response.Write("			    ")


        Response.Write(mobjValues.PolicyControl("tcnPolicyDest", GetLocalResourceObject("tcnPolicyDestToolTip"), "cbeBranch", mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), "valProduct", mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mstrCertype, mlngPolicy, "tcnCertificat", mlngCertif))


        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("			</TR>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=0><DIV ID=""divPol_dest3"">" & GetLocalResourceObject("valCertifCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divPol_dest4"">" & vbCrLf)
        Response.Write("                ")


        With mobjValues
            .Parameters.Add("sCertype", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.ReturnValue("nCertif", True, "Certificado", True)
        End With
        Response.Write(mobjValues.PossiblesValues("valCertif", "TabCertif", eFunctions.Values.eValuesType.clngWindowType, "", True, , , , , "ShowChangeValues(""TabCertif"")", , 14, GetLocalResourceObject("valCertifToolTip")))

        Response.Write("" & vbCrLf)
        Response.Write("                </DIV></TD>" & vbCrLf)
        Response.Write("			</TR>" & vbCrLf)
        Response.Write("			<TR>			    " & vbCrLf)
        Response.Write("                <TD><LABEL ID=13373>" & GetLocalResourceObject("tcnCertificatCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                <TD>" & vbCrLf)
        Response.Write("                ")


        Response.Write(mobjValues.NumericControl("tcnCertificat", 10, mlngCertif, , "", False, , , , , "ShowChangeValues(""Certificat"")") & mobjValues.AnimatedButtonControl("btnPolicyValues", "/VTimeNet/images/btn_ValuesOff.png", "Datos de verificación de una póliza", , "ShowVerifyData()", True))

        Response.Write("" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("        </TABLE>" & vbCrLf)
        Response.Write("        <TABLE WIDTH=100% BORDER=0>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                " & vbCrLf)
        Response.Write("                <TD WIDTH=30%>" & vbCrLf)
        Response.Write("                <LABEL ID=0><DIV ID=""divCotProp"">" & GetLocalResourceObject("tcnQuotPropCaption") & "</DIV></LABEL>" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divCotProp2"">" & vbCrLf)
        Response.Write("                ")


        Response.Write(mobjValues.NumericControl("tcnQuotProp", 10, mlngProponum, , GetLocalResourceObject("tcnQuotPropToolTip"), False, , , , , "ShowChangeValues(""Policy"")", True))


        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><LABEL ID=0><DIV ID=""divProp_Reg"">" & GetLocalResourceObject("tcnProp_RegCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divProp_Reg2"">")


        Response.Write(mobjValues.NumericControl("tcnProp_Reg", 10, vbNullString, , GetLocalResourceObject("tcnProp_RegToolTip"), False, , , , , , True))


        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)

        Response.Write("            <TR>    " & vbCrLf)
        Response.Write("                <TD><LABEL ID=0><DIV ID=""divServOrder"">" & GetLocalResourceObject("tcnServ_orderCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divServOrder2"">")
        Response.Write(mobjValues.NumericControl("tcnServ_order", 10, Request.Form.Item("tcnServ_order"), , GetLocalResourceObject("tcnServ_orderToolTip"), , , , , , "ShowChangeValues(""nServ_order"")"))
        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)

        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divFolio""><LABEL ID=0>" & GetLocalResourceObject("tcnFolioCaption") & "</LABEL></DIV></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divFolio2"">")
        Response.Write(mobjValues.NumericControl("tcnFolio", 10, vbNullString, , GetLocalResourceObject("tcnFolioToolTip"), False, , , , , , True))
        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)

        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divRenewalNum""><LABEL ID=0>" & GetLocalResourceObject("tcnRenewalNumCaption") & "</LABEL></DIV></TD>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divRenewalNum2"">")
        Response.Write(mobjValues.NumericControl("tcnRenewalNum", 10, vbNullString, , GetLocalResourceObject("tcnRenewalNumToolTip"), False, , , , , , True))
        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)

        Response.Write("        </TABLE>" & vbCrLf)
        Response.Write("<DIV ID=""divPolicy_Associated"" style='display:none' >" & vbCrLf)
        Response.Write("    <TABLE width=100%>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=2 CLASS=HighLighted>" & vbCrLf)
        Response.Write("                <LABEL Id=0>" & vbCrLf)
        Response.Write("                    <A NAME=""Clave"">" & GetLocalResourceObject("AnchorPolicy_AssociatedCaption") & "</A>")
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD >" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"" CLASS=""Horline""></TD>  " & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL ID=0>" & vbCrLf)
        Response.Write("                    <DIV ID=divOffice_Associated1>" & GetLocalResourceObject("cbeOffice_AssociatedCaption")  &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divOffice_Associated2>" & vbCrLf )
        mobjValues.Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1,       eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)

        'mobjValues.BlankPosition = True        
        Response.Write("                    " & mobjValues.PossiblesValues("cbeOffice_Associated", "tabOfficeUser", 1, mobjValues.StringToType(Request.Form.Item("cbeOffice_Associated"), eFunctions.Values.eTypeData.etdInteger) , True,  , , , , "BlankOfficeDependAssociated();insInitialAgencyAssociated(1)" , False, , GetLocalResourceObject("cbeOffice_AssociatedToolTip")))
        Response.Write("                </DIV>" & vbCrLf )
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL Id=0>" & vbCrLf)
        Response.Write("                    <DIV ID=divOfficeAgen_Associated1>" & GetLocalResourceObject("cbeOfficeAgen_AssociatedCaption") &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divOfficeAgen_Associated2>" & vbCrLf)
        mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.ReturnValue("nBran_off", , , True)
        Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen_Associated", "TabAgencies_T5556", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.Form.Item("cbeOfficeAgen_Associated") , eFunctions.Values.eTypeData.etdInteger), True, , , , , "insInitialAgencyAssociated(2)", , , GetLocalResourceObject("cbeOfficeAgen_AssociatedToolTip")))
        Response.Write("                </DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL>" & vbCrLf)
        Response.Write("<DIV ID=divAgency_Associated1>" & GetLocalResourceObject("cbeAgency_AssociatedCaption") &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divAgency_Associated2>"  & vbCrLf)
        mobjValues.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.Add("nUsercode", CDbl(Session("nUsercode")), 1, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, 64)
        mobjValues.Parameters.ReturnValue("nBran_off", , , True)
        mobjValues.Parameters.ReturnValue("nOfficeAgen", , , True)
        mobjValues.Parameters.ReturnValue("sDesAgen", , , True)
        Response.Write(mobjValues.PossiblesValues("cbeAgency_Associated", "TabAgencies_T5555A", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(Request.Form.Item("cbeAgency_Associated"), eFunctions.Values.eTypeData.etdInteger), True, , , , , "insInitialAgencyAssociated(3)", , , GetLocalResourceObject("cbeAgency_AssociatedToolTip")))
        Response.Write("                </DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL>" & vbCrLf)
        Response.Write("<DIV ID=divBranch_Associated1>" & GetLocalResourceObject("cbeBranch_AssociatedCaption") &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divBranch_Associated2>" & vbCrLf)
        Response.Write(mobjValues.BranchControl("cbeBranch_Associated", GetLocalResourceObject("cbeBranch_AssociatedToolTip"), mobjValues.StringToType(Request.Form.Item("cbeBranch_Associated"), eFunctions.Values.eTypeData.etdInteger) , "valProduct_Associated", , , , "ShowChangeValues(""nBranch_Associated"")"))
        Response.Write("                </DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL>" & vbCrLf)
        Response.Write("<DIV ID=divProduct_Associated1>" & GetLocalResourceObject("valProduct_AssociatedCaption") &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divProduct_Associated2>" & vbCrLf)
        Response.Write(mobjValues.ProductControl("valProduct_Associated", GetLocalResourceObject("valProduct_AssociatedToolTip"), mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), , mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble) <= 0, mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), , , , "ShowChangeValues(""Product_Associated"")"))
        Response.Write("                </DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL>" & vbCrLf)
        Response.Write("                    <DIV ID=divPolicy_Associated1>" & GetLocalResourceObject("tcnPolicy_AssociatedCaption") &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divPolicy_Associated2>" &  vbCrLf)
        Response.Write(                     mobjValues.PolicyControl("tcnPolicy_Associated", GetLocalResourceObject("tcnPolicy_AssociatedToolTip"), "cbeBranch_Associated", mobjValues.StringToType(mintBranch, eFunctions.Values.eTypeData.etdDouble), "valProduct_Associated", mobjValues.StringToType(mintProduct, eFunctions.Values.eTypeData.etdDouble), mstrCertype, mlngPolicy, "tcnCertificat_Associated", mlngCertif, , , , "LockControl(""Policy_Associated"");ShowChangeValues(""Policy_Associated"")", False))
        Response.Write("                </DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD width=30%>" & vbCrLf)
        Response.Write("                <LABEL>" & vbCrLf)
        Response.Write("<DIV ID=divCertificate_Associated1>" & GetLocalResourceObject("tcnCertificate_AssociatedCaption") &  "</DIV>" )
        Response.Write("                </LABEL>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("                <DIV ID=divCertificate_Associated2>" & vbCrLf)
        'With mobjValues
        '    .Parameters.Add("sCertype", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '    .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '    .Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '    .Parameters.Add("nPolicy", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '    .Parameters.Add("sClient", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '    .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '    .Parameters.ReturnValue("nCertif", True, "Certificado", True)
        'End With        
        Response.Write(mobjValues.NumericControl("tcnCertificat_Associated", 10, mlngCertif, , GetLocalResourceObject("tcnCertificate_AssociatedToolTip"), False, , , , , "ShowChangeValues(""Certificat_Associated"")"))
        Response.Write("                </DIV>" & vbCrLf)
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>" & vbCrLf)
        Response.Write("</DIV>" & vbCrLf)
        Response.Write("    </TD>" & vbCrLf)
        Response.Write("    <TD WIDTH=5%>&nbsp;</TD>" & vbCrLf)
        Response.Write("    <TD VALIGN=TOP>" & vbCrLf)
        Response.Write("        <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
        Response.Write("            <TR>            " & vbCrLf)
        Response.Write("             ")
        Response.Write("                <TD COLSPAN=""2"" CLASS=""HighLighted"" ><DIV ID=""divHorline""><LABEL ID=0><A NAME=""Tipo de negocio""   >" & GetLocalResourceObject("AnchorTipo de negocioCaption") & "</A></LABEL></DIV></TD>            " & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("             ")

        If Request.QueryString.Item("sCodispl") <> "CA001C" Then
            Response.Write("<TD COLSPAN=""2"" CLASS=""Horline""></TD>")
        End If

        Response.Write("    " & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divHorline2"">" & vbCrLf)
        Response.Write("                    ")


        With Response
            .Write(mobjValues.OptionControl(40670, "optBussines", GetLocalResourceObject("optBussines_1Caption"), , "1",,,,GetLocalResourceObject("optBussines_1ToolTip")))
            .Write(mobjValues.OptionControl(40671, "optBussines", GetLocalResourceObject("optBussines_2Caption"), , "2",,,,GetLocalResourceObject("optBussines_2ToolTip")))
            .Write(mobjValues.OptionControl(40672, "optBussines", GetLocalResourceObject("optBussines_3Caption"), , "3",,,,GetLocalResourceObject("optBussines_3ToolTip")))
        End With

        Response.Write("</DIV>" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>            " & vbCrLf)
        Response.Write("                <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de póliza""><DIV ID=""divPoliType"">" & GetLocalResourceObject("AnchorTipo de pólizaCaption") & "</DIV></A></LABEL></TD>            " & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("             ")

        If Request.QueryString.Item("sCodispl") <> "CA001C" Then
            Response.Write("<TD COLSPAN=""2"" CLASS=""Horline""></TD>")
        End If

        Response.Write("    " & vbCrLf)
        Response.Write("            </TR>  " & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD><DIV ID=""divPoliType2"">" & vbCrLf)
        Response.Write("                    ")


        With Response
            .Write(mobjValues.OptionControl(40673, "optType", GetLocalResourceObject("optType_1Caption"), , "1" ,,,,GetLocalResourceObject("optType_1ToolTip")))
            .Write(mobjValues.OptionControl(40674, "optType", GetLocalResourceObject("optType_2Caption"), , "2" ,,,,GetLocalResourceObject("optType_2ToolTip")))
            .Write(mobjValues.OptionControl(40675, "optType", GetLocalResourceObject("optType_3Caption"), , "3" ,,,,GetLocalResourceObject("optType_3ToolTip")))
        End With

        Response.Write("" & vbCrLf)
        Response.Write("                </DIV></TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD>" & vbCrLf)
        Response.Write("                    <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
        Response.Write("                        <TR>            " & vbCrLf)
        Response.Write("                            <TD COLSPAN=""2"" CLASS=""HighLighted""><DIV ID=""divLedgerdate0""><LABEL ID=0><A NAME=""Relaciones"">" & GetLocalResourceObject("AnchorRelacionesCaption") & "</A></LABEL></DIV></TD>            " & vbCrLf)
        Response.Write("                        </DIV></TR>" & vbCrLf)
        Response.Write("                        <TR>" & vbCrLf)
        Response.Write("                        ")

        If Request.QueryString.Item("sCodispl") <> "CA001C" Then
            Response.Write("<TD COLSPAN=""2"" CLASS=""Horline""></TD>")
        End If

        Response.Write("    " & vbCrLf)
        Response.Write("                        </TR>  " & vbCrLf)
        Response.Write("                        <TR>" & vbCrLf)
        Response.Write("                            <TD><LABEL ID=13376><DIV ID=""divLedgerdate"">" & GetLocalResourceObject("tcdLedgerDateCaption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                            <TD><DIV ID=""divLedgerdate2"">")


        Response.Write(mobjValues.DateControl("tcdLedgerDate", Request.Form.Item("tcdLedgerDate"), , GetLocalResourceObject("tcdLedgerDateToolTip")))


        Response.Write("</DIV></TD>" & vbCrLf)
        Response.Write("                        </TR>" & vbCrLf)
        Response.Write("                        <TR style='display:none'>" & vbCrLf)
        Response.Write("                            <TD><LABEL ID=0><DIV ID=""divTypeAccount"">" & GetLocalResourceObject("NTYPEACCOUNT_Caption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                            <TD><DIV ID=""divTypeAccount2"">")
        Response.Write(mobjValues.PossiblesValues(FieldName:="NTYPEACCOUNT", TableName:="TABLE7200", ValuesType:= eFunctions.Values.eValuesType.clngComboType, DefValue:="", NeedParam:=False, ComboSize:=1, Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NTYPEACCOUNT_ToolTip"), CodeType:= eFunctions.Values.eTypeCode.eNumeric, ShowDescript:=True, bAllowInvalid:=False))
        Response.Write("                                </DIV></TD>" & vbCrLf)
        Response.Write("                        </TR>" & vbCrLf)
        Response.Write("                        <TR style='display:none'>" & vbCrLf)
        Response.Write("                            <TD><LABEL ID=0><DIV ID=""divTypeAccount"">" & GetLocalResourceObject("tcnProcess_num_Caption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                            <TD><DIV ID=""divTypeAccount2"">")
        Response.Write(mobjValues.TextControl("tcnProcess_num",  50,  mlngProponum, , GetLocalResourceObject("tcnProcess_numToolTip"),  False, , , , , ,  ))
        Response.Write("                                </DIV></TD>" & vbCrLf)
        Response.Write("                        </TR>" & vbCrLf)
        Response.Write("                        <TR style='display:none'>" & vbCrLf)
        Response.Write("                            <TD><LABEL ID=0><DIV ID=""divTypeAccount"">" & GetLocalResourceObject("nPolicy_Transfer_Caption") & "</DIV></LABEL></TD>" & vbCrLf)
        Response.Write("                            <TD><DIV ID=""divTypeAccount2"">")
        Response.Write(mobjValues.TextControl("tctCodBranch_transfer" , 2, , , GetLocalResourceObject("nPolicy_TransferToolTip"), , , , , , , ))
        Response.Write(mobjValues.NumericControl("tcnPolicy_Transfer", 10, , , GetLocalResourceObject("nPolicy_TransferToolTip"),  False, , , , , , ))
        Response.Write(mobjValues.NumericControl("tcnCertif_transfer", 3, , , GetLocalResourceObject("nPolicy_TransferToolTip"), False,  , , , , , ))
        Response.Write("                                </DIV></TD>" & vbCrLf)
        Response.Write("                        </TR>" & vbCrLf)
        Response.Write("                    </TABLE>" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("            <TR>" & vbCrLf)
        Response.Write("                <TD>                                " & vbCrLf)
        Response.Write("                    <DIV ID=""divType_amend"">                " & vbCrLf)
        Response.Write("                        <TABLE WIDTH=""100%"" BORDER=0>" & vbCrLf)
        Response.Write("                            <TR>            " & vbCrLf)
        Response.Write("                                <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Modificación"">" & GetLocalResourceObject("AnchorModificaciónCaption") & "</A></LABEL></TD>            " & vbCrLf)
        Response.Write("                            </TR>" & vbCrLf)
        Response.Write("                            <TR>" & vbCrLf)
        Response.Write("                                <TD COLSPAN=""2"" CLASS=""Horline""></TD>        " & vbCrLf)
        Response.Write("                            </TR>  " & vbCrLf)
        Response.Write("                            <TR>" & vbCrLf)
        Response.Write("                                <TD><LABEL ID=0>" & GetLocalResourceObject("valType_amendCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                                <TD>")

        With mobjValues
            .Parameters.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(.PossiblesValues("valType_amend", "Tabtype_amend", eFunctions.Values.eValuesType.clngWindowType, Request.Form.Item("valType_amend"), True, , , , , "setTimeout('ShowChangeValues(""Endoso"");',300);", , , GetLocalResourceObject("valType_amendToolTip")))

        End With

        Response.Write("" & vbCrLf)
        Response.Write("                                </TD>" & vbCrLf)
        Response.Write("                            </TR>" & vbCrLf)
        Response.Write("                            <TR>" & vbCrLf)
        Response.Write("                                <TD><LABEL ID=0>" & GetLocalResourceObject("tcdFerCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                                <TD>")


        Response.Write(mobjValues.DateControl("tcdFer", Request.Form.Item("tcdFer"), , GetLocalResourceObject("tcdFerToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("                            </TR>" & vbCrLf)
        Response.Write("                        </TABLE>" & vbCrLf)
        Response.Write("                    </DIV>" & vbCrLf)
        Response.Write("                </TD>" & vbCrLf)
        Response.Write("            </TR>" & vbCrLf)
        Response.Write("        </TABLE>" & vbCrLf)
        Response.Write("    </TD>" & vbCrLf)
        Response.Write("</TR>" & vbCrLf)
        Response.Write("<TR>" & vbCrLf)
        Response.Write("    <TD>" & vbCrLf)
        Response.Write("        <DIV ID=""divConvertion"">" & vbCrLf)
        Response.Write("            ")


        Response.Write(mobjValues.CheckControl("chkConvertion", GetLocalResourceObject("chkConvertionCaption"), "2", "1"))


        Response.Write("" & vbCrLf)
        Response.Write("        </DIV>" & vbCrLf)
        Response.Write("    </TD>" & vbCrLf)
        Response.Write("</TR>" & vbCrLf)
        Response.Write("<TR>" & vbCrLf)
        Response.Write("    <TD></TD>" & vbCrLf)
        Response.Write("</TR>" & vbCrLf)
        Response.Write("<TR>" & vbCrLf)
        Response.Write("    <TD COLSPAN=2>" & vbCrLf)
        Response.Write("        <DIV ID=""divExpireDate"">" & vbCrLf)
        Response.Write("            <TABLE WIDTH=100% BORDER=0>" & vbCrLf)
        Response.Write("                <TR>            " & vbCrLf)
        Response.Write("                    <TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Fecha de vencimiento"">" & GetLocalResourceObject("AnchorFecha de vencimientoCaption") & "</A></LABEL></TD>            " & vbCrLf)
        Response.Write("                </TR>" & vbCrLf)
        Response.Write("                <TR>" & vbCrLf)
        Response.Write("                    <TD COLSPAN=""2"" CLASS=""Horline""></TD>        " & vbCrLf)
        Response.Write("                </TR>                        " & vbCrLf)
        Response.Write("                <TR>" & vbCrLf)
        Response.Write("                    <TD><LABEL ID=40669>" & GetLocalResourceObject("tcdExpDateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                    <TD>")


        Response.Write(mobjValues.DateControl("tcdExpDate", , , GetLocalResourceObject("tcdExpDateToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("                </TR>" & vbCrLf)
        Response.Write("            </TABLE>" & vbCrLf)
        Response.Write("        </DIV>" & vbCrLf)
        Response.Write("    </TD>" & vbCrLf)
        Response.Write("</TR>" & vbCrLf)
        Response.Write("</TABLE>")


        Response.Write(mobjValues.BeginPageButton)
        Response.Write(mobjValues.HiddenControl("sCertype", mstrCertype))
        Response.Write(mobjValues.HiddenControl("hddCod_saapv", Request.QueryString("nCod_saapv")))
        Response.Write(mobjValues.HiddenControl("hddInstitution", Request.QueryString("nInstitution")))

        mobjValues = Nothing

        If mintAction = vbNullString Then
            If CStr(Session("nFinish")) = "392" Then
                Response.Write("<SCRIPT>insInitialFields(true,'" & Request.QueryString.Item("sCodisplOrig") & "')</" & "Script>")
                Response.Write("<SCRIPT>insStateControls(false,true)</" & "Script>")
                Response.Write("<SCRIPT>setTimeout('insShowDescript()',100);</" & "Script>")
            Else
                Response.Write("<SCRIPT>insInitialFields(true,'" & Request.QueryString.Item("sCodisplOrig") & "')</" & "Script>")
            End If
        Else
            Response.Write("<SCRIPT>insInitialAction(true,'" & Request.QueryString.Item("sCodisplOrig") & "')</" & "Script>")
        End If

        If Request.QueryString.Item("sCodispl") = "CA001C" Then
            mintTransaction = "4"
            Session("nTransaction") = mintTransaction
            If Request.QueryString.Item("mintBranch") <> vbNullString Then
                mintBranch = Request.QueryString.Item("mintBranch")
            Else
                mintBranch = "1"
            End If
            mstrCertype = "3"
            Response.Write("<SCRIPT>insHideFields(" & Session("nOfficeAgen") & "," & Session("nAgency") & ")</" & "Script>")
        Else
            'If Request.QueryString("nOrig_call") = 1 Then
            '    Response.Write "<NOTSCRIPT>insInitialAction(false,'" & Request.QueryString("sCodisplOrig") & "')</" & "Script>"
            'End If 

            If mstrTypeCompany = eClient.Client.eType.cstrBrokerOrBrokerageFirm Then
                Call insShowReinsuranData()
            End If
            If Request.QueryString.Item("bMenu") = "1" Then
                Response.Write("<SCRIPT>ShowChangeValues(""Policy"");</" & "Script>")
            End If
        End If
    End Sub

    '% LoadParameters: Carga en variables datos pasados por parametro a transaccion
    '--------------------------------------------------------------------------------------------
    Private Sub LoadParameters()
        '--------------------------------------------------------------------------------------------
        '+ LoadWithAction  : Accion con la que se debe cargar la ventana
        With Request
            mintTransaction = 1
            If .QueryString.Item("sCertype") = vbNullString Then
                mstrCertype = "2"
            Else
                mstrCertype = .QueryString.Item("sCertype")
            End If
            If .QueryString.Item("sCodisplOrig") = "CAC001" Or .QueryString.Item("sCodisplOrig") = "GE010" Or .QueryString.Item("sCodisplOrig") = "CAC011" Or .QueryString.Item("sCodisplOrig") = "CA099A" Or .QueryString.Item("sCodisplOrig") = "SO001" Then
                mintBranch = .QueryString.Item("nBranch")
                mintProduct = .QueryString.Item("nProduct")
                If .QueryString.Item("nPolicy") <> vbNullString Then
                    If CDbl(.QueryString.Item("sCertype")) = 1 Then
                        mlngPolicy = .QueryString.Item("nProponum")
                        mlngProponum = vbNullString
                    Else
                        mlngPolicy = .QueryString.Item("nPolicy")
                        mlngProponum = .QueryString.Item("nProponum")
                    End If
                Else
                    mlngPolicy = .QueryString.Item("nProponum")
                    mlngProponum = vbNullString
                End If
                mlngCertif = .QueryString.Item("nCertif")
                mdtmEffecdate = .QueryString.Item("dStartdate")
                mintAction = .QueryString.Item("LoadWithAction")
                mintTransaction = .QueryString.Item("nTransaction")
                Session("nTransaction") = .QueryString.Item("nTransaction")
            End If
            If .QueryString.Item("sCodispl") = "CA001C" Then
                mintTransaction = "4"
                Session("nTransaction") = mintTransaction
                mintBranch = "1"
                mstrCertype = "3"
            End If
        End With

        '+ Si no se ingresa transaccion, se define una por omision segun la accion
        If mintAction <> vbNullString And mintTransaction = vbNullString Then
            If mintAction = "401" Then
                '+ Consulta de certificado
                mintTransaction = 9
            ElseIf mintAction = "302" Then
                '+ Modificacion normal de certificado            
                mintTransaction = 14
            End If
        End If

        If mintTransaction = vbNullString Then
            '+ Emision de póliza
            mintTransaction = Session("nTransaction")
            If mintTransaction = vbNullString Then
                mintTransaction = 1
            End If
        End If

        '+ Se debe mantener la última transacción si se trata de traspaso de asegurados
        If CStr(Session("sTransHolder")) = "1" Then
            mintTransaction = 46
        End If
    End Sub

    '% insShowReinsuranData: muestra los campos si la compañía usuaria es de Reaseguro
    '--------------------------------------------------------------------------------------------
    Private Sub insShowReinsuranData()
        '--------------------------------------------------------------------------------------------
        With Response
            .Write("<SCRIPT>")
            .Write("with(document.forms['CA001']){")
            .Write("    valInsuranceCompany.value = 0;")
            .Write("    valOriginalOffice.value = 0;")
            .Write("    tctOriginalPolicy.value = '';")
            .Write("}")
            .Write("</" & "Script>")
        End With
    End Sub

</script>
<%Response.Expires = -1441
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.03
'    Dim mobjNetFrameWork 
'    mobjNetFrameWork = Server.CreateObject("eNetFrameWork.Layout")
'    mobjNetFrameWork.sSessionID = Session.SessionID
'    mobjNetFrameWork.nUsercode = Session("nUsercode")
'    Call mobjNetFrameWork.BeginPage("CA001_K")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mclsPolicy = New ePolicy.Policy

'+ Se inicializa la variable de carga de certificado    
Session("blnCertificat") = ""

'+ Se cargan los parametros en variables del modulo
Call LoadParameters()

'+ Se realiza la validacion de operaciones permitidas al esquema del usuario
mclsSche_Transac = New eSecurity.Secur_sche
If Request.QueryString("nCod_saapv") = "" then
        Session("nCod_saapv") = ""
else
        Session("nCod_saapv") = Request.QueryString("nCod_saapv")
end if
If Request.QueryString("nInstitution") = "" then
        Session("nInstitution") = ""
else
        Session("nInstitution") = Request.QueryString("nInstitution")
end if

    If Request.QueryString.Item("sCodispl") = "CA001C" Then
        mintTransaction = 4
    End If
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

<SCRIPT LANGUAGE= "JavaScript">
        var lblnContinue = true;

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 15-10-09 20:50 $|$$Author: Ljimenez $"

        //- Se llena la variable gloval con el tipo de poliza
    sPolitype = '<%=Session("sPolitype")%>'
        //- Variable que almacena la transación original
    nTransaction_ori = '<%=Session("nTransaction2")%>'    

        //- Variable que indica la transaccion que seleccionó el usuario o se paso por parametro
    var mstrTransaction = '<%=mintTransaction%>'
    var mstrTransaction = '<%=Session("nTransaction")%>'
    var mstrTransaction2 = '<%=Session("nTransaction2")%>'
    var mstrIniPage = '<%=Session("PageRetCA050")%>'            


        //- Tipo con las posibles acciones a ejecutar (campo cbeTransactio)
    var ePolTransac = new ePolTransac()


        //% Funciones estandares usadas por menu
        //% insStateZone: se establece el estado de la página
        //--------------------------------------------------------------------------------------------
        function insStateZone() {
            //--------------------------------------------------------------------------------------------
        }

        //% insCancel: Ejecuta la acción Cancelar de la página
        //%                Sólamente se efectuará este proceso cuando el usuario cancela la transacción 
        //--------------------------------------------------------------------------------------------
        function insCancel() {
            //--------------------------------------------------------------------------------------------
            if (top.frames["fraSequence"].pintZone == 2) {
                if (mstrTransaction == ePolTransac.clngPolicyIssue ||
           mstrTransaction == ePolTransac.clngCertifIssue ||
           mstrTransaction == ePolTransac.clngRecuperation ||
           mstrTransaction == ePolTransac.clngCertifProposal ||
           mstrTransaction == ePolTransac.clngPolicyProposal ||
           mstrTransaction == ePolTransac.clngPolicyQuotation ||
           mstrTransaction == ePolTransac.clngCertifQuotation ||
           mstrTransaction == ePolTransac.clngCertifQuotAmendent ||
           mstrTransaction == ePolTransac.clngPolicyQuotAmendent ||
           mstrTransaction == ePolTransac.clngPolicyPropAmendent ||
           mstrTransaction == ePolTransac.clngCertifPropAmendent ||
           mstrTransaction == ePolTransac.clngPolicyQuotRenewal ||
           mstrTransaction == ePolTransac.clngCertifQuotRenewal ||
           mstrTransaction == ePolTransac.clngPolicyPropRenewal ||
           mstrTransaction == ePolTransac.clngTransHolder ||
           mstrTransaction == ePolTransac.clngCertifPropRenewal) {
                    ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=CA001_K", "EndProcess", 300, 150)
                }
                else
                    if (mstrTransaction == ePolTransac.clngTempCertifAmendment ||
               mstrTransaction == ePolTransac.clngTempPolicyAmendment ||
               mstrTransaction == ePolTransac.clngCertifAmendment ||
               mstrTransaction == ePolTransac.clngPolicyAmendment ||
               mstrTransaction == ePolTransac.clngPropAmendConvertion) {
                        insDefValues("insCancel", "", '/VTimeNet/Policy/PolicySeq');
                        setTimeout("insLocation()", 200);
                    }
                    else {
                        insDefValues("UserAmend")
                        top.document.location.href = '/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy'
                    }
            }
            else {
                return (true);
            }
        }


        function insLocation() {
            top.document.location.href = '/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy'
        }
        //% insFinish: Ejecuta la acción de Finalizar de la página.
        //--------------------------------------------------------------------------------------------
        function insFinish() {
            //--------------------------------------------------------------------------------------------
            if (mstrTransaction == ePolTransac.clngPolicyAmendment ||
       mstrTransaction == ePolTransac.clngTempPolicyAmendment ||
       mstrTransaction == ePolTransac.clngCertifAmendment ||
       mstrTransaction == ePolTransac.clngTempCertifAmendment ||
       mstrTransaction == ePolTransac.clngPolicyQuotAmendent ||
       mstrTransaction == ePolTransac.clngCertifQuotAmendent ||
       mstrTransaction == ePolTransac.clngPolicyPropAmendent ||
       mstrTransaction == ePolTransac.clngCertifPropAmendent ||
       mstrTransaction == ePolTransac.clngPropAmendConvertion)
       ShowPopUp("/VTimeNet/Common/PopUp.aspx?sPageName=/VTimeNet/Policy/PolicySeq/CA048&nAction=392","EndProcess",600,300,false, false, 100, 100)
            else {
                if (mstrTransaction == ePolTransac.clngPolicyIssue ||
            mstrTransaction == ePolTransac.clngCertifIssue ||
            mstrTransaction == ePolTransac.clngRecuperation ||
            mstrTransaction == ePolTransac.clngPolicyQuotation ||
            mstrTransaction == ePolTransac.clngCertifQuotation ||
            mstrTransaction == ePolTransac.clngPolicyProposal ||
            mstrTransaction == ePolTransac.clngCertifProposal ||
            mstrTransaction == ePolTransac.clngPolicyReissue ||
            mstrTransaction == ePolTransac.clngCertifReissue ||
	        mstrTransaction == ePolTransac.clngPolicyQuotRenewal ||
			mstrTransaction == ePolTransac.clngCertifQuotRenewal ||
			mstrTransaction == ePolTransac.clngPolicyPropRenewal ||
			mstrTransaction == ePolTransac.clngCertifPropRenewal ||
			mstrTransaction == ePolTransac.clngTransHolder ||
			mstrTransaction == ePolTransac.clngPropQuotConvertion ||
            mstrTransaction2 == ePolTransac.clngQuotationConvertion ||
            mstrTransaction == 43 ||
			mstrTransaction == 45)

                    ShowPopUp("/VTimeNet/Common/PopUp.aspx?sPageName=/VTimeNet/Policy/PolicySeq/CA050&nAction=392", "EndPolicyIssue", 500, 350)
                else {
                    if (mstrIniPage == "CA001C")
                        top.document.location = "/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001C&sProject=PolicySeq&sModule=Policy"
                    else
                        top.document.location = "/VTimeNet/Common/secWHeader.aspx?sCodispl=CA001&sProject=PolicySeq&sModule=Policy"
                }
            }
        }

        function insShowDescript() {
            with (self.document.forms['CA001']) {
                if (lblnContinue) {
            if (typeof($("#tcnPolicy").attr("onBlurCode"))!='undefined'){
                        if (tcnPolicy.value != '') {
                            setTimeout('ShowChangeValues("Policy")', 500)
                            lblnContinue = false
                        }
                    }
                }
            }
        }    

    </script>
    <%
        '+ Creacion de menu de barra de herramientas
        Response.Write(mobjValues.StyleSheet())
        mobjMenu = New eFunctions.Menues
        '^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.03
        mobjMenu.sSessionID = Session.SessionID
        mobjMenu.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
        Response.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CA001_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
        mobjMenu = Nothing
        Response.Write("<script>" & vbCrLf)
        Response.Write("var nMainAction=top.frames['fraSequence'].plngMainAction" & vbCrLf)

        If Request.QueryString.Item("sConfig") = "InSequence" Then
            '+ Se asume que se encuentra en la zona de detalle
            Response.Write("top.frames[""fraSequence""].pintZone=2" & vbCrLf)
        End If
        Response.Write("</script>" & vbCrLf)
    %>
</HEAD>
<BODY ONUNLOAD="closeWindows()">
<FORM METHOD="POST" NAME="CA001" ACTION="valPolicySeq.aspx?sConfig=<%=Request.QueryString.Item("sConfig")%>">
<BR><BR>
    <%
        If Request.QueryString.Item("sConfig") = "InSequence" Then
            Call LoadPageInSequence()
        Else
            Call LoadPageStart()
        End If

        '+ Se liberan de memoria objetos usados
        mobjValues = Nothing
        mclsPolicy = Nothing
        mclsSche_Transac = Nothing
    %>
</FORM>
</BODY>
</HTML>

<%
    '+ Carga los datos de la oficina, sucursal, agencia y canal de venta cuando la CA001 es llamada desde otra transacción
    '+ para consultar, o recuperar información (Llamada desde la transacción CAC001)
    '+ Se deben agregar otras transacciones que llamen a la CA001 con las mismas características
    If Request.QueryString.Item("sCodisplOrig") = "CAC001" Or Request.QueryString.Item("sCodisplOrig") = "CA099A" Then
        With Response
		.Write("<SCRIPT>")
            .Write("setTimeout('ShowChangeValues(""Policy"");',1500);")
            .Write("setTimeout('ShowChangeValues(""Certificat"");',12000);")
		.Write("</SCRIPT>")
        End With
    End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.03
    '	Call mobjNetFrameWork.FinishPage("CA001_K")
    '	mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
