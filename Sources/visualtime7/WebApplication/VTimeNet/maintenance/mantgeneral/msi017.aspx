<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjOptionInstall As eGeneral.OptionsInstallation


    '**% insDefineFields : defines the structure of the page "painting" the precise fields and the grid
    '%   insDefineFields : define la estructura de la página "pintando" los campos puntuales y el grid
    '--------------------------------------------------------------------------------------------------
    Private Function insPreMSI017() As Object
        '--------------------------------------------------------------------------------------------------
        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""20%"" colspan=""2""><LABEL ID=0>" & GetLocalResourceObject("cbeCurrencyClaimCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""15%"" colspan=""2"">")
        Response.Write(mobjValues.PossiblesValues("cbeCurrencyClaim", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCurrencyClaim),  ,  ,  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401,  , GetLocalResourceObject("cbeCurrencyClaimToolTip")))
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""65%"" colspan=""5""></TD>")
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""35%"" colspan=""3"">")
        Response.Write(mobjValues.CheckControl("chkTaxReserve", GetLocalResourceObject("chkTaxReserveCaption"), mobjOptionInstall.sIndReservClaim, CStr(1),  , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("            </TD>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""65%"" colspan=""6""></TD>")
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""100%"" colspan=""9"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("TitleCaption1") & "</LABEL></TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""100%"" colspan=""9"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR><TD WIDTH=""100%"" colspan=""9""></BR></TD></TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""45%"" colspan=""5"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("TitleCaption2") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""25%""></TD>")
        Response.Write("			<TD WIDTH=""20%"" colspan=""2"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("TitleCaption3") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""45%"" colspan=""5"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""25%""></TD>")
        Response.Write("			<TD WIDTH=""20%"" colspan=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("cbeSectionCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.ComboControl("cbeSection", "1|Manual,2|Automática", CStr(mobjOptionInstall.nSectionClaim),,, GetLocalResourceObject("cbeSectionToolTip"), "Enabled(this,""cbeSection"");"))
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""5%""></TD>")
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnDaysSectionCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnDaysSection", 5, CStr(mobjOptionInstall.nDaysSectionClaim),, GetLocalResourceObject("tcnDaysSectionToolTip"), False, 0,,,,, IIf(mobjOptionInstall.nDaysSectionClaim > 0, False, True)))
        Response.Write("		         <LABEL ID=0> días</LABEL>" & vbCrLf)
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""25%""></TD>")
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnPercentCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnPercent", 5, CStr(mobjOptionInstall.nPercentClaim),, GetLocalResourceObject("tcnPercentToolTip")))
        Response.Write("		         <LABEL ID=0> %</LABEL>" & vbCrLf)
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.HiddenControl("tcnPercent2", CStr(mobjOptionInstall.nPercent_NormClaim)))
        Response.Write("			</TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnCostMinCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnCostMin", 20, CStr(mobjOptionInstall.nCostMinClaim),, GetLocalResourceObject("tcnCostMinToolTip")))
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""5%""></TD>")
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnCostMaxCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnCostMax", 20, CStr(mobjOptionInstall.nCostMaxClaim),, GetLocalResourceObject("tcnCostMaxToolTip"), False, 0))
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""55%"" colspan=""4""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("tcnMaxDaysCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnMaxDays", 4, CStr(mobjOptionInstall.nMaxdaysClaim),, GetLocalResourceObject("tcnMaxDaysToolTip"), False, 0))
        Response.Write("<LABEL ID=0>días</LABEL>")
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""80%"" colspan=""6""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR><TD WIDTH=""100%"" colspan=""9""></BR></TD></TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""35%"" colspan=""4"" CLASS=""HIGHLIGHTED""><LABEL ID=0>" & GetLocalResourceObject("TitleCaption4") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""65%"" colspan=""5""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""35%"" colspan=""4"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""65%"" colspan=""5""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("cbeSimplifiedCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.PossiblesValues("cbeSimplified", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nSimpli_payFreqClaim),,,,,, "Enabled(this,""cbeSimplified"");",,, GetLocalResourceObject("cbeSimplifiedToolTip")))
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""5%""></TD>")
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnYear", 5, CStr(mobjOptionInstall.nYear_simpliClaim),, GetLocalResourceObject("tcnYearToolTip"), False, 0,,,,, IIf(mobjOptionInstall.nYear_simpliClaim.IsNotEmpty, False, True)))
        Response.Write("		         <LABEL ID=0> años</LABEL>" & vbCrLf)
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""65%"" colspan=""5""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	    <TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""10%""><LABEL ID=0>" & GetLocalResourceObject("cbetransitoryCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.PossiblesValues("cbetransitory", "Table36", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nTransi_ParyfreqClaim),,,,,, "Enabled(this,""cbetransitory"");",,, GetLocalResourceObject("cbetransitoryToolTip")))
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""5%""></TD>")
        Response.Write("			<TD WIDTH=""10%"">")
        Response.Write(mobjValues.NumericControl("tcnYear2", 5, CStr(mobjOptionInstall.nYear_transiClaim),, GetLocalResourceObject("tcnYear2ToolTip"), False, 0,,,,, IIf(mobjOptionInstall.nYear_transiClaim.IsNotEmpty, False, True)))
        Response.Write("		         <LABEL ID=0> años</LABEL>" & vbCrLf)
        Response.Write("			</TD>")
        Response.Write("			<TD WIDTH=""65%"" colspan=""5""></TD>")
        Response.Write("	    </TR>" & vbCrLf)
        Response.Write("	</TABLE>")
    End Function
</script>
<%
Response.Expires = -1

'**+ The objects necessary are instancian to work the particularitities of creation of the form by generic routines  
'+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjOptionInstall = New eGeneral.OptionsInstallation

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MSI017"
%> 
<HTML>
<HEAD>
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%="<SCRIPT LANGUAGE=""JavaScript"">"%>
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
    </SCRIPT>
    <META NAME = "GENERATOR" Content="Microsoft Visual Studio 6.0">
    <SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
    <SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>

    <%'Sección para manejo de javascript %>
    <script>
        //Enabled: habilita y desabilita campos
        //------------------------------------------------------------------------------
        function Enabled(Field, Name)
        {
            switch (Name)
            {
                case 'cbeSection':
                    if (Field.value == 1)/* Tramo manual */ {
                        self.document.forms[0].tcnDaysSection.value = '';
                        self.document.forms[0].tcnDaysSection.disabled = true;
                    }
                    else {
                        self.document.forms[0].tcnDaysSection.value = '';
                        self.document.forms[0].tcnDaysSection.disabled = false;
                    }
                    break;
                case 'cbeSimplified':
                    if (Field.value > 0) {
                        self.document.forms[0].tcnYear.value = '';
                        self.document.forms[0].tcnYear.disabled = false;
                    }
                    else {
                        self.document.forms[0].tcnYear.value = '';
                        self.document.forms[0].tcnYear.disabled = true;
                    }
                    break;
                case 'cbetransitory':
					if (Field.value > 0) {
						self.document.forms[0].tcnYear2.value = '';
                        self.document.forms[0].tcnYear2.disabled = false;
					}
                    else {
						self.document.forms[0].tcnYear2.value = '';
                        self.document.forms[0].tcnYear2.disabled = true;
					}
                    break;
            }
        }
    </script>

	<%=mobjValues.StyleSheet()%>
	<TITLE>Generalidades de las opciones de instalación</TITLE>
</HEAD>
	
<BODY ONUNLOAD="closeWindows();">
	<%
If Request.QueryString.Item("Type") <> "PopUp" Then Response.Write(mobjMenu.setZone(2, "MSI017", "MSI017.aspx"))
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" id=form1 name=form1>
	<%

'**+ The reading of the inserted values is made in the table of the options of installation of claim
'+ Se realiza la lectura de los valores caragados en la tabla de la opciones de instalación de siniestros

With mobjOptionInstall
	.insPreMSI017()
End With

'**+ The fields of the page are defined to capture the data
'+ Se definen los campos de la página para capturar los datos

insPreMSI017()
%>
</BODY>
</HTML>