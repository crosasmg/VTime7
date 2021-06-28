<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjWorksheet As eBatch.Worksheet
Dim mobjBatch As eBatch.ValBatch
Dim mdEffecdate As Object
Dim mobjGrid As eFunctions.Grid


'% LoadHeader: se cargan los datos para la secuencia
'--------------------------------------------------------------------------------------------
Private Sub LoadHeader()
	'--------------------------------------------------------------------------------------------
	Dim lblnDisabled As Boolean
	
	'+ En caso que la ventana sea llamada desde otra transacción
	If Request.QueryString.Item("sLinkSpecial") = "CA658" Then
		Session("sLinkSpecial") = Request.QueryString.Item("sLinkSpecial")
		mdEffecdate = Session("dEffecdate")
		Session("sTypeage") = Request.QueryString.Item("sTypeage")
	Else
		Session("sLinkSpecial") = ""
		mdEffecdate = vbNullString
	End If
	
	
	If Request.QueryString.Item("sLinkSpecial") = vbNullString Then
		lblnDisabled = False
	Else
		lblnDisabled = True
	End If
	
	Response.Write(mobjValues.HiddenControl("hdtFileName", ""))
	Response.Write(mobjValues.HiddenControl("hdnWorksheet", ""))
    Response.Write(mobjValues.HiddenControl("hdsProcMasive", "1"))
	Response.Write(mobjValues.HiddenControl("hdsReinsuran", "2"))
	Response.Write(mobjValues.HiddenControl("hdsContinue", "2"))
    Response.Write(mobjValues.HiddenControl("hdsManual", "2"))
    Response.Write(mobjValues.HiddenControl("hdsCheckFile", "2"))
    Response.Write(mobjValues.HiddenControl("hdsNoPreview", "1"))
	
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)

        ' Tipo de Ejecucion & Datos de Pólizas
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Process"">" & GetLocalResourceObject("AnchorProcessCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted"" width=50%><LABEL ID=0><A NAME=""Datos de Pólizas"">" & GetLocalResourceObject("AnchorCaracterísticasCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""3""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write(mobjValues.OptionControl(0, "nOptAct", GetLocalResourceObject("nOptAct_1Caption"), "1", "2", , , , GetLocalResourceObject("nOptAct_1ToolTip")))
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write(mobjValues.OptionControl(0, "nOptAct", GetLocalResourceObject("nOptAct_2Caption"), "1", "1", , , , GetLocalResourceObject("nOptAct_2ToolTip")))
        Response.Write("			</TD>" & vbCrLf)
        
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"">")
        Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct", , , , , lblnDisabled))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)

        'Acción & Producto
        Response.Write("			<TD COLSPAN=""1""><LABEL ID=0>" & GetLocalResourceObject("cbeActionCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            ")

        If Request.QueryString.Item("sLinkSpecial") = "CA658" Then
            Response.Write("" & vbCrLf)
            Response.Write("				<TD>")


            Response.Write(mobjValues.PossiblesValues("cbeAction", "table5578", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nAction"), , , , , , "insChangeField(this);", lblnDisabled, , GetLocalResourceObject("cbeActionToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("            ")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("            <TD>")

            mobjValues.TypeList = CShort("2")
            mobjValues.List = "5"
            Response.Write(mobjValues.PossiblesValues("cbeAction", "table5578", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nAction"), , , , , , "insChangeField(this);", lblnDisabled, , GetLocalResourceObject("cbeActionToolTip")))
		
            Response.Write("</TD>" & vbCrLf)
            Response.Write("			")

        End If
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""2"">")
        Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType, lblnDisabled, Request.QueryString.Item("nProduct")))
        Response.Write("</TD>" & vbCrLf)
        
        'Procesamiento Masivo & Poliza
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">")
        Response.Write(mobjValues.CheckControl("chkProcMasive", GetLocalResourceObject("chkProcMasiveCaption"), "2", "1", "insChangeField(this)"))
        Response.Write("" & vbCrLf)
        Response.Write("				&nbsp;")
        Response.Write(mobjValues.TextControl("tctSeparator", 1, "", , GetLocalResourceObject("tctSeparatorToolTip"), , , , , True))
        Response.Write("</TD>" & vbCrLf)

        Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""2"">")
        Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPolicy"), , GetLocalResourceObject("tcnPolicyToolTip"), , , , , , "insChangeField(this);", lblnDisabled))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)


        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Parametros"">" & GetLocalResourceObject("AnchorParametrosCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Opciones"">" & GetLocalResourceObject("AnchorOpcionesCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""3""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")
        Response.Write(mobjValues.DateControl("tcdEffecdate", mdEffecdate, , GetLocalResourceObject("tcdEffecdateToolTip"), , , , , lblnDisabled))

        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""3"" >")       
        Response.Write(mobjValues.CheckControl("chkNoPreview", GetLocalResourceObject("chkNoPreviewCaption"), "1", "1", "insChangeField(this);", , , GetLocalResourceObject("chkNoPreviewToolTip")))
        Response.Write("</TD>" & vbCrLf)

        
        Response.Write("</TD>" & vbCrLf)

        
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""1""><LABEL ID=0>" & GetLocalResourceObject("cbeWorksheet1Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=""1"">" & vbCrLf)
        Response.Write("            <DIV ID=""MasterSheet"">" & vbCrLf)
        Response.Write("            ")

        mobjValues.Parameters.add("NINTERTYPE", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        '+ SE DEBE AGREGAR PARAMETRO SISTEMA EXTERNO A PAGINA, POR AHORA ESTA CON UN VALOR FIJO
        mobjValues.Parameters.add("NSYSTEM", 3, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        mobjValues.Parameters.ReturnValue("nOpertype", , , True)
        mobjValues.Parameters.ReturnValue("nFormat", , , True)
        mobjValues.Parameters.ReturnValue("sFormat", , , True)
        mobjValues.Parameters.ReturnValue("sOpertype", , , True)
        Response.Write(mobjValues.PossiblesValues("cbeWorksheet1", "TABTABLEMASTERSHEET", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True, , , , , "insChangeField(this);", , , GetLocalResourceObject("cbeWorksheet1ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("			</DIV>" & vbCrLf)
        Response.Write("			<DIV ID=""WorkSheet"">" & vbCrLf)
        Response.Write("			")
        Response.Write(mobjValues.PossiblesValues("cbeWorksheet", "TabtabWorksheet", eFunctions.Values.eValuesType.clngWindowType, "", , , , , , "insChangeField(this);", , , GetLocalResourceObject("cbeWorksheetToolTip")))


        Response.Write("" & vbCrLf)
        Response.Write("			</DIV>" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">")
        Response.Write(mobjValues.CheckControl("chkManual", GetLocalResourceObject("chkManualCaption"), , , "insChangeField(this);", , , GetLocalResourceObject("chkManualCaption")))               
        Response.Write("</TD> " & vbCrLf)

        
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.FileControl("tctFile", 40, , True, , "insChangeField(this)"))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">")


        Response.Write(mobjValues.CheckControl("chkReinsuran", GetLocalResourceObject("chkReinsuranCaption"), "2", "1", "insChangeField(this);", , , GetLocalResourceObject("chkReinsuranCaption")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">")
        Response.Write(mobjValues.CheckControl("chkContinue", GetLocalResourceObject("chkContinueCaption"), , , "ChangeChecked(this);", , , GetLocalResourceObject("chkContinueToolTip")))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR> " & vbCrLf)



        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdContinueCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")
        Response.Write(mobjValues.DateControl("tcdContinue", , , GetLocalResourceObject("tcdContinueToolTip"), , , , , True))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)


        Response.Write("<TD>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdExcludeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")
        Response.Write(mobjValues.DateControl("tcdExclude", , , GetLocalResourceObject("tcdExcludeToolTip"), , , , , True))
        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("</TD>" & vbCrLf)
        
                
        
        Response.Write("        <TR class='TDRecordType' >" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Tipo de Registro"">" & GetLocalResourceObject("AnchorTipo de RegistroCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR class='TDRecordType' >" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
        Response.Write("			<TD></TD>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""3""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR class='TDRecordType' >" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_3Caption"), CStr(mobjBatch.DefaultValueCAL013("opt_Quotation", Request.QueryString.Item("sCertype"), Request.QueryString.Item("sLinkSpecial"))), "3", , lblnDisabled = True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("		</TR>            " & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("        <TR class='TDRecordType' >" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"), CStr(mobjBatch.DefaultValueCAL013("opt_Proposal", Request.QueryString.Item("sCertype"), Request.QueryString.Item("sLinkSpecial"))), "1", , lblnDisabled = True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>            " & vbCrLf)
        Response.Write("        <TR class='TDRecordType' >" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"">")


        Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), CStr(mobjBatch.DefaultValueCAL013("opt_Policy", Request.QueryString.Item("sCertype"), Request.QueryString.Item("sLinkSpecial"))), "2", , lblnDisabled = True))
        Response.Write("</TD>" & vbCrLf)

        Response.Write("		</TR>            " & vbCrLf)
        Response.Write("" & vbCrLf)
        
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD>" & vbCrLf)
        Response.Write("				<DIV ID=""sFile"">")


        Response.Write(mobjValues.CheckControl("chkFile", GetLocalResourceObject("chkFileCaption"), , , "insChangeField(this);", , , GetLocalResourceObject("chkFileToolTip")))


        Response.Write("" & vbCrLf)
        Response.Write("				</DIV>" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR class='TRMassiveFile'>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=0><A NAME=""Archivo"">" & GetLocalResourceObject("AnchorArchivoCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""4"">" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR class='TRMassiveFile'>" & vbCrLf)
        Response.Write("			<TD CLASS=""HorLine"" COLSPAN=""2""></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""4"">" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("		<SCRIPT>ShowDiv('WorkSheet', 'hide');" & vbCrLf)
        Response.Write("				ShowDiv('MasterSheet', 'show');" & vbCrLf)
        Response.Write("				ShowDiv('sFile', 'hide');" & vbCrLf)
        Response.Write("		</" & "SCRIPT>")

	
        Call insDefineHeader()
        
        Response.Write("</TABLE>")
        
        If CStr(Session("CAL013_sLinkSpecial")) = "1" Then
            Response.Write("<SCRIPT>ClientRequest('301')</" & "Script>")
            Session.Contents.Remove(("CAL013_nBranch"))
            Session.Contents.Remove(("CAL013_nBrancht"))
            Session.Contents.Remove(("CAL013_nProduct"))
            Session.Contents.Remove(("CAL013_dEffecdate"))
        End If
    End Sub

    '% LoadHeader: se cargan los datos para la secuencia
    '--------------------------------------------------------------------------------------------
    Private Sub LoadPageInSequence()
        '--------------------------------------------------------------------------------------------
        mobjWorksheet = New eBatch.Worksheet
        Call mobjWorksheet.FindWorksheet(mobjValues.StringToType(Session("nWorksheet"), eFunctions.Values.eTypeData.etdDouble))
	
        Response.Write("" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD WIDTH=""15%""><LABEL>" & GetLocalResourceObject("lblDesBranchCaption") & "</LABEL>&nbsp;&nbsp;&nbsp;</TD>" & vbCrLf)
        Response.Write("            <TD>" & vbCrLf)
        Response.Write("            ")

	
        Response.Write(mobjValues.BranchControl("lblDesBranch", GetLocalResourceObject("lblDesBranchToolTip"), Session("nBranch"), , True) & "/" & mobjValues.ProductControl("valProduct", "Producto asociado a la póliza", Session("nBranch"), eFunctions.Values.eValuesType.clngComboType, , Session("nProduct"), True))
	
        If Session("nPolicy") > 0 Then
            Response.Write("" & vbCrLf)
            Response.Write("            </TD>" & vbCrLf)
            Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("            <TD>")


            Response.Write(mobjValues.TextControl("tcnPolicy", 10, Session("nPolicy"), , , True))
        End If


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeWorksheet1Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.NumericControl("tcnWorksheet", 5, Session("nWorksheet"), , , , , True, , , , True))
        Response.Write(" - ")
        Response.Write(mobjValues.TextControl("tctDescript", 30, mobjWorksheet.sDescript, , , True, , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")


        Response.Write(mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"), , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeActionCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD COLSPAN=1>")


        Response.Write(mobjValues.PossiblesValues("cbeAction", "table5578", eFunctions.Values.eValuesType.clngComboType, Session("nAction"), , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("AnchorProcessCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        If Session("nOptAct") = 1 Then
            Response.Write(mobjValues.TextControl("tcnOptAct", 10, GetLocalResourceObject("nOptAct_2Caption"), , , True))
        ElseIf Session("nOptAct") = 2 Then
            Response.Write(mobjValues.TextControl("tcnOptAct", 10, GetLocalResourceObject("nOptAct_1Caption"), , , True))
        End If

        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("    <SCRIPT>insShowNextWindow();</" & "SCRIPT>")

	
        mobjWorksheet = Nothing
	
        If CStr(Session("CAL013_sLinkSpecial")) = "1" Then
            Response.Write("<SCRIPT>ClientRequest('301')</" & "Script>")
            Session.Contents.Remove(("CAL013_nBranch"))
            Session.Contents.Remove(("CAL013_nBrancht"))
            Session.Contents.Remove(("CAL013_nProduct"))
            Session.Contents.Remove(("CAL013_dEffecdate"))
        End If
    End Sub

    '% insDefineHeader: Se definen las columnas del grid de la ventana.
    '------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '------------------------------------------------------------------------------
        Dim lstrList_Files As String
        Dim lArrList_Files() As String
        Dim i As Integer
        Dim lclsMasiveCharge As eBatch.MasiveCharge
	
        Response.Write("<TR class='TRMassiveFile'>")
        Response.Write("<TD COLSPAN='2'>")
        Response.Write("<TABLE WIDTH='100%'>")
        Response.Write("<TR>")
        Response.Write("<TD>")
	
        mobjGrid = New eFunctions.Grid
	
        '+ Se definen las columns del Grid
        With mobjGrid.Columns
            Call .AddTextColumn(0, GetLocalResourceObject("tctFileColumnCaption"), "tctFile", 50, CStr(eRemoteDB.Constants.strNull), , GetLocalResourceObject("tctFileColumnToolTip"))
        End With
	
        '+ Se asignan las caracteristicas del Grid
        With mobjGrid
            .Codispl = "CAL013"
            .Codisp = "CAL013"
            .sCodisplPage = "CAL013"
            .AddButton = False
            .DeleteButton = False
            .Columns("Sel").OnClick = "InsClickValues(this);"
        End With
	
        lclsMasiveCharge = New eBatch.MasiveCharge
	
        lstrList_Files = lclsMasiveCharge.Find_Files("1")
        lArrList_Files = lstrList_Files.Split("|")
	
        For i = 0 To UBound(lArrList_Files)
            With mobjGrid
                .Columns("tctFile").DefValue = lArrList_Files(i)
            End With
		
            '+Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos en el grid
            Response.Write(mobjGrid.DoRow())
        Next
	
        Response.Write(mobjGrid.closeTable())
        Response.Write("</TD>")
        Response.Write("</TR>")
        Response.Write("</TABLE>")
        Response.Write("</TR>")
        Response.Write("</TD>")
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "cal013_k"
mobjBatch = New eBatch.ValBatch

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


<SCRIPT LANGUAGE= "JavaScript">

var nContent
var message

message =<%Response.Write("""Se procesara " & mobjValues.getMessage(Session("nAction"), "Table5578") & """;")%>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 19/07/06 6:25p $|$$Author: Clobos $"

//% insStateZone: se manejan los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	var lstrLinkSpecial='<%=Session("CAL013_sLinkSpecial")%>'
	var lstrLink='<%=Request.QueryString.Item("sLinkSpecial")%>'

	if(top.frames['fraSequence'].pintZone==1)
		if (lstrLinkSpecial=='1'){
			top.document.location.href='/VTimeNet/common/GoTo.aspx?sCodispl=CAL659'
		}
		else
//+ En caso que se esté invocando desde otra ventana se cierra al Cancelar
			if(lstrLink=='CA658')
				top.close();
			else
				return true;
	else{
        top.document.location.href='/VTimeNet/Common/secWHeader.aspx?sCodispl=CAL013_K&sProject=PolicyRep&sModule=Policy'
    }
}

//% insRefresh: Refresca los valores de la Pagina
//--------------------------------------------------------------------------------------------
function insRefresh(){
//--------------------------------------------------------------------------------------------
	var lstrLinkSpecial='<%=Session("CAL013_sLinkSpecial")%>'
	if (lstrLinkSpecial == '1'){
		tcdEffecdate.value='<%=Session("CAL013_dEffecdate")%>';
		cbeBranch.value='<%=Session("CAL013_nBranch")%>';
		valProduct.value='<%=Session("CAL013_nProduct")%>';
	}
}

//%insShowNextWindow. Se encarga de mostrar la siguiente ventana a ser mostrada
//--------------------------------------------------------------------------------------------
function insShowNextWindow(){
//--------------------------------------------------------------------------------------------
	var lblnDoIt=true;
	
	if (typeof(top.frames['fraSequence'])!='undefined')
	    if (typeof(top.frames['fraSequence'].NextWindows)!='undefined'){
			top.frames['fraSequence'].NextWindows('');
			lblnDoIt = false;
	    }
	    
	if (lblnDoIt) setTimeout('insShowNextWindow()',50)
}

//%insChangeField : Cambia valores de campos dependientes
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
    var frm = self.document.forms[0]
    var lintOpt

    switch(objField.name){
    case 'tcnPolicy':
        if (frm.optType[0].checked) lintOpt = '3'
        else if (frm.optType[1].checked) lintOpt = '1'
             else if (frm.optType[2].checked) lintOpt = '2';
             
        insDefValues('PolicyDate', 'sCertype=' + lintOpt + 
                                   '&nBranch='+ frm.cbeBranch.value +
                                   '&nProduct='+ frm.valProduct.value +
                                   '&nPolicy='+ frm.tcnPolicy.value,
                     '/VTimeNet/policy/policyrep/');
        break;

	case 'chkProcMasive':
  		if (objField.checked){
  			if (frm.cbeAction.value=="6"){
  				if (frm.chkFile.checked){
					ShowDiv('WorkSheet', 'hide');
					ShowDiv('MasterSheet', 'show');
                    $(".TRMassiveFile").show();
					frm.tctSeparator.disabled = false;
					frm.tctFile.disabled = true;
  				}
  			}
  			else{
  				objField.value = "1";
  				frm.hdsProcMasive.value = "1";
  				frm.chkReinsuran.disabled = false;
				frm.tctSeparator.disabled = false;
				frm.tctFile.disabled = true;
				frm.chkContinue.disabled = false;
				frm.chkContinue.checked = false;
				frm.tcdContinue.disabled = true;
				frm.chkManual.disabled = true;
				frm.chkManual.checked = false;
				ShowDiv('WorkSheet', 'hide');
				ShowDiv('MasterSheet', 'show');
                $(".TRMassiveFile").show();
			}
		}
		else{
			if (frm.cbeAction.value=="6"){
  				if (frm.chkFile.checked){
					ShowDiv('WorkSheet', 'show');
					ShowDiv('MasterSheet', 'hide');
                    $(".TRMassiveFile").hide();
					frm.tctSeparator.disabled = true;
					frm.tctFile.disabled = false;
  				}
  			}
  			else{
  				frm.tctSeparator.disabled = true;
				objField.value = "2";
				frm.hdsProcMasive.value = "2";
				frm.tctFile.disabled = false;
				frm.chkContinue.disabled = false;
				frm.chkContinue.checked = false;
				frm.tcdContinue.disabled = true;
				frm.tcdContinue.value = '';
				frm.tctSeparator.disabled = true;
				frm.chkReinsuran.disabled = true;
				frm.chkReinsuran.checked = false;
				frm.cbeAction.disabled = false;
				frm.chkManual.disabled = false;
				ShowDiv('WorkSheet', 'show');
				ShowDiv('MasterSheet', 'hide');
                $(".TRMassiveFile").hide();
			}
		}
		break;

	case 'chkReinsuran':
		if (objField.checked){
			objField.value = "1";
			frm.hdsReinsuran.value = "1";
			frm.cbeAction.disabled = true;
			frm.cbeWorksheet1.disabled = true;
			frm.chkContinue.disabled = true;
			frm.chkContinue.checked = false;
			frm.cbeWorksheet1.value = '';
			frm.tcdContinue.disabled = true;
			frm.hdsContinue.value = "1"
			UpdateDiv('cbeWorksheet1Desc','');
			frm.cbeWorksheet1.cbeWorksheet1Desc='';
			frm.cbeAction.value = '';
			$(".TRMassiveFile").hide();

		}
		else{
			objField.value = "2";
			frm.hdsReinsuran.value = "2";
			frm.hdsContinue.value = "2"
			frm.cbeAction.disabled = false;
			frm.cbeWorksheet1.disabled = false;
			frm.chkContinue.disabled = false;
            $(".TRMassiveFile").show();
		}
		break;

	case 'cbeAction':
		if (objField.value=="6" || objField.value=="7" || objField.value=="8"){
            $(".TRMassiveFile").hide();
			ShowDiv('sFile', 'show');
			frm.chkProcMasive.disabled = true;
			frm.chkProcMasive.checked = false;
			frm.hdsProcMasive.value = "2";
			if (frm.chkProcMasive.checked){
				ShowDiv('WorkSheet', 'hide');
				ShowDiv('MasterSheet', 'show');
			}
			else{
				ShowDiv('WorkSheet', 'show');
				ShowDiv('MasterSheet', 'hide');
			}
			frm.chkManual.disabled = true;
			frm.chkManual.checked = false;
			frm.chkContinue.disabled = true;
			frm.chkContinue.checked = false;
			frm.tcdContinue.disabled = true;
			frm.tcdContinue.value = "";
			frm.btn_tcdContinue.disabled = true;
			frm.chkReinsuran.disabled = true;
			frm.chkReinsuran.checked = false;
			frm.tctSeparator.disabled = true;
			frm.chkFile.disabled = false;
			frm.chkFile.checked = false;
			frm.tctFile.disabled = true;
			frm.cbeWorksheet1.value = '';
			UpdateDiv('cbeWorksheet1Desc','');
			frm.cbeWorksheet.value = '';
			UpdateDiv('cbeWorksheetDesc','');
			frm.cbeWorksheet.disabled = true;
			frm.btncbeWorksheet.disabled = true;
			frm.tcdExclude.disabled = true;
			frm.btn_tcdExclude.disabled = true;
			if (objField.value=="7" || objField.value=="8"){
				ShowDiv('sFile', 'hide');
			}
		}
		else{
//			document.getElementsByTagName("TD")[70].style.display='';
//			document.getElementsByTagName("TD")[68].style.display='';
//			document.getElementsByTagName("TD")[66].style.display='';
//Exclusión o Reemplazdo
			if (objField.value=="2" || objField.value=="3"){
				frm.tcdExclude.disabled = false;
				frm.btn_tcdExclude.disabled = false;
			}
			else{
				frm.tcdExclude.disabled = true;
				frm.btn_tcdExclude.disabled = true;
			}
			
			ShowDiv('sFile', 'hide');
			frm.chkProcMasive.disabled = false;
			if (frm.chkProcMasive.checked){
				ShowDiv('WorkSheet', 'hide');
				ShowDiv('MasterSheet', 'show');
			}
			else{
				ShowDiv('WorkSheet', 'show');
				ShowDiv('MasterSheet', 'hide');
			}
			frm.chkManual.disabled = false;
			frm.chkManual.checked = false;
			frm.chkContinue.disabled = false;
			frm.chkContinue.checked = false;
			frm.tcdContinue.disabled = false;
			frm.btn_tcdContinue.disabled = false;
			frm.tcdContinue.value = "";
			frm.chkReinsuran.disabled = false;
			frm.chkReinsuran.checked = false;
//			frm.chkProcMasive.checked = false;
//			frm.tctSeparator.disabled = true;
			frm.chkFile.disabled = false;
			frm.chkFile.checked = false;
			frm.tctFile.disabled = false;
			frm.cbeWorksheet1.value = '';
			UpdateDiv('cbeWorksheet1Desc','');
			frm.cbeWorksheet.value = '';
			UpdateDiv('cbeWorksheetDesc','');
			frm.cbeWorksheet.disabled = false;
			frm.btncbeWorksheet.disabled = false;
		}
		break;
	
	case 'chkFile':
		if (objField.checked){
//			frm.tctFile.disabled = false;
			frm.chkProcMasive.disabled = false;
			frm.chkProcMasive.checked = false;
			frm.hdsProcMasive.value = "2";
			frm.cbeWorksheet.disabled = false;
			frm.btncbeWorksheet.disabled = false;
		}
		else{
//			frm.tctFile.disabled = true;
			frm.chkProcMasive.disabled = true;
			frm.chkProcMasive.checked = false;
			frm.hdsProcMasive.value = "2";
			frm.cbeWorksheet.disabled = true;
			frm.btncbeWorksheet.disabled = true;
		}
		break;
	
	case 'chkManual':
		if (objField.checked)
			frm.hdsManual.value = "1"
		else
			frm.hdsManual.value = "2"
		break;
    case 'chkNoPreview':
		if (objField.checked)
			frm.hdsNoPreview.value = "1"
		else
			frm.hdsNoPreview.value = "2"
		break;
	case 'chkFile':
		if (objField.checked)
			frm.hdsCheckFile.value = "1"
		else
			frm.hdsCheckFile.value = "2"
		break;
	case 'tctFile':
		frm.hdtFileName.value = objField.value;
		break;
	case 'cbeWorksheet':
	case 'cbeWorksheet1':
		frm.hdnWorksheet.value = objField.value;
		break;
    }            
}

//% ChangeChecked: Se controla el valor de las restricciones
//--------------------------------------------------------------------------------------------
function ChangeChecked(Obj){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch(Obj.name){
		case 'chkContinue':
			if (Obj.checked){
				Obj.value="1";
				tcdContinue.disabled = false;
				btn_tcdContinue.disabled = false;
				tcdContinue.value = tcdEffecdate.value;
				hdsContinue.value = "1";
			}
			else{
				Obj.value="2";
				tcdContinue.disabled = true;
				btn_tcdContinue.disabled = true;
				tcdContinue.value = "";
				hdsContinue.value = "2";
			}
			break
		}
	}
}

//%insFinish. Esta función es utilizada para realizar cambios al momento de finalizar la transacción
//--------------------------------------------------------------------------------    
function insFinish(){
//--------------------------------------------------------------------------------    
	if (nContent == 1){
		if (confirm(message))
			return(true);
	}
    else
	    return(true);
}

//%InsClickValues: Carga valor del campo tctFileName
//--------------------------------------------------------------------------------    
function InsClickValues(Obj){
//--------------------------------------------------------------------------------    
	var i
	with(self.document.forms[0]){
		for (i in marrArray){
			if (i!=Obj.value)
				Sel[i].checked = false;
		}

		if (Obj.checked)
			hdtFileName.value = marrArray[Obj.value].tctFile;
		else
			hdtFileName.value = "";
	}
}
</SCRIPT>    
    <%mobjMenu = New eFunctions.Menues

With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CAL013_K", "CAL013_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();" text=6567>
<FORM METHOD="POST" ID="FORM" NAME="CAL013" ACTION="valPolicyRepSeq.aspx?sMode=1" ENCTYPE="multipart/form-data">
<P>&nbsp;</P>
<%If Request.QueryString.Item("sConfig") = "InSequence" Then
	Call LoadPageInSequence()
Else

        Call LoadHeader()
        %>
    <SCRIPT>

        $(document).ready(function () {
            $(".TDRecordType").hide();
            insChangeField($("[name=chkProcMasive]")[0]);
        });
        
    </SCRIPT>
        <%
    End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjBatch = Nothing
%>





