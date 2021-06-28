<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjOptionInstall As eGeneral.OptionsInstallation
    Dim mobjGrid As eFunctions.Grid



    '**********************************************************************************************************
    '*************************************** FUNCIONES VBScript ***********************************************
    '*************************************** FUNCTIONS VBScript ***********************************************
    '**********************************************************************************************************

    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '%insDefineHeader: defines header of grid to showed in the page of the active and inactive modules in the system  
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjGrid
            If Request.QueryString.Item("Action") <> "Update" Then
                .Columns.AddCheckColumn(0, vbNullString, "sAuxSel", vbNullString, False)
            End If
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctModuleColumnCaption"), "tctModule", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            .Columns.AddDateColumn(0, GetLocalResourceObject("tcdInstallDateColumnCaption"), "tcdInstallDate")
            .Columns.AddHiddenColumn("tcnModule", CStr(0))
            .Columns.AddHiddenColumn("tctFrame", CStr(0))
            .Columns.AddHiddenColumn("tcddInit_date_aux", CStr(mobjOptionInstall.dInit_Date))
            .Columns("tctModule").EditRecord = True
            .Columns("Sel").GridVisible = False
            .Codispl = "MS010"
            .sCodisplPage = "MS010"
            .Width = 400
            .Height = 210
            .AddButton = False
            .DeleteButton = False
            .DeleteScriptName = vbNullString
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            If Request.QueryString.Item("Action") <> "Update" Then
                .Columns("sAuxSel").OnClick = "insSelected(this);"
            End If
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
            If Request.QueryString.Item("Reload") = "1" Then .sReloadIndex = Request.QueryString.Item("ReloadIndex")
        End With
    End Sub

    '% insPreMS010Upd: carga los valores de la página MS010
    '% insPreMS010Upd: load the values of MS010 page
    '--------------------------------------------------------------------------------------------
    Private Sub insPreMS010Upd()
        '--------------------------------------------------------------------------------------------
        Dim lclsOptionsInstallation As eGeneral.OptionsInstallation

        '+ Si la acción es actualizar un módulo instalado preexistente, se procede a bloquear el campo de la descriprión
        '+ If the action is to update an installed module preexisting, it is come to lock the field of the description

        If Request.QueryString.Item("Action") = "Update" Then

            '+ Si la acción es desinstalar un módulo previamente instalado, se procede a eliminarlo de la tabla de módulos instalados para el sistema
            '+ If the action is to uninstall a module previously installed, it is come to eliminate it of the table of modules installed for the system 

            Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneral.aspx", "MS010", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
        ElseIf Request.QueryString.Item("Action") = "Del" Then

            '+ Se envía el mensaje de "información eliminada"
            '+ The message of eliminated information is sent

            Response.Write(mobjValues.ConfirmDelete)
            Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantGeneral.aspx", "MS010", Request.QueryString.Item("nMainAction"), Session("bQuery"), CShort(Request.QueryString.Item("Index"))))
            lclsOptionsInstallation = New eGeneral.OptionsInstallation
            With mobjValues
                lclsOptionsInstallation.insPostMSI010Mod("Delete", Session("nUsercode"), .StringToType(Request.QueryString.Item("nModule"), eFunctions.Values.eTypeData.etdDouble, True), .StringToDate(Request.QueryString.Item("dInstallDate")))
            End With
            lclsOptionsInstallation = Nothing
        End If
    End Sub

    '% insDefineFields : define la estructura de la página "pintando" los campos puntuales y el grid
    '% insDefineFields : defines the structure of the page "painting" the precise fields and the grid   
    '--------------------------------------------------------------------------------------------------
    Private Function insPreMS010() As Object
        '--------------------------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("		<TD VALIGN=TOP>" & vbCrLf)
        Response.Write("			<TABLE WIDTH=""100%"" COLS=2>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("tcddInit_dateCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.DateControl("tcddInit_date", CStr(mobjOptionInstall.dInit_Date),  , GetLocalResourceObject("tcddInit_dateToolTip"),  ,  ,  ,  , CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("tcnConpanyUserCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.NumericControl("tcnConpanyUser", 4, CStr(mobjOptionInstall.nCompany), False, GetLocalResourceObject("tcnConpanyUserToolTip"), False, False))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				        <TD><LABEL ID=0>" & GetLocalResourceObject("cbeInsur_AreaCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("                        <TD>")

        Response.Write(mobjValues.PossiblesValues("cbeInsur_Area", "table5001", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nInsur_Area),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsur_AreaToolTip"),  , 2))
        Response.Write(" </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("cbeCountryCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.PossiblesValues("cbeCountry", "Table66", eFunctions.Values.eValuesType.clngComboType, CStr(mobjOptionInstall.nCountry)))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("cbeSecureCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.PossiblesValues("cbeSecure", "Table902", eFunctions.Values.eValuesType.clngComboType, mobjOptionInstall.sSecure))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("			 ")
        Response.Write("					<TD>")

        If mobjOptionInstall.nPEP = "1" Then
            Response.Write(mobjValues.CheckControl("chkPEP", GetLocalResourceObject("chkPEPCaption"), CStr(1), , , , , GetLocalResourceObject("chkPEPToolTip")))
        Else
            Response.Write(mobjValues.CheckControl("chkPEP", GetLocalResourceObject("chkPEPCaption"), , , , , , GetLocalResourceObject("chkPEPToolTip")))
        End If

        Response.Write("</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("			 ")
        Response.Write("					<TD>")

        If mobjOptionInstall.nUsperson = "1" Then
            Response.Write(mobjValues.CheckControl("chkUSPERSON", GetLocalResourceObject("chkUSPERSONCaption"), CStr(1), , , , , GetLocalResourceObject("chkUSPERSONToolTip")))
        Else
            Response.Write(mobjValues.CheckControl("chkUSPERSON", GetLocalResourceObject("chkUSPERSONCaption"), , , , , , GetLocalResourceObject("chkUSPERSONToolTip")))
        End If


        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD COLSPAN=""2"" CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("tctPersonForCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>")


        Response.Write(mobjValues.TextControl("tctPersonFor", 13, mobjOptionInstall.sFormatPer,  , GetLocalResourceObject("tctPersonForToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("tctCompanyForCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>")


        Response.Write(mobjValues.TextControl("tctCompanyFor", 13, mobjOptionInstall.sFormatComp,  , GetLocalResourceObject("tctCompanyForToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("			</TABLE>" & vbCrLf)
        Response.Write("		</TD>" & vbCrLf)
        Response.Write("		<TD VALIGN=TOP ALIGN=""CENTER"">" & vbCrLf)
        Response.Write("			<DIV ID=""Scroll"" style=""width:300;height:200;overflow:auto;outset gray"">" & vbCrLf)
        Response.Write("                ")

        insDefineGrid()
        Response.Write("" & vbCrLf)
        Response.Write("	        </DIV>" & vbCrLf)
        Response.Write("	    </TD>" & vbCrLf)
        Response.Write("	</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HIGHLIGHTED""><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HORLINE""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeNumPolicyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeNumPolicy", "Table900", eFunctions.Values.eValuesType.clngComboType, mobjOptionInstall.sPolicyNum))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=10%>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeNumClaimCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeNumClaim", "Table901", eFunctions.Values.eValuesType.clngComboType, mobjOptionInstall.sClaimNum))


        Response.Write("</TD>			" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeNumReceiptCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeNumReceipt", "Table900", eFunctions.Values.eValuesType.clngComboType, mobjOptionInstall.sReceiptNum))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=10% >&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""3"">" & vbCrLf)
        Response.Write("			 ")


        If mobjOptionInstall.sQuotnumauto = "1" Then
            Response.Write(mobjValues.CheckControl("chkQuotNumAut", GetLocalResourceObject("chkQuotNumAutCaption"), CStr(1),  ,  ,  ,  , GetLocalResourceObject("chkQuotNumAutToolTip")))
        Else
            Response.Write(mobjValues.CheckControl("chkQuotNumAut", GetLocalResourceObject("chkQuotNumAutCaption"),  ,  ,  ,  ,  , GetLocalResourceObject("chkQuotNumAutToolTip")))
        End If

        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>")


    End Function

    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '%insDefineGrid: defines grid according to the read thing of the incolucradas tables  
    '----------------------------------------------------------------------------------------------
    Private Sub insDefineGrid()
        '----------------------------------------------------------------------------------------------
        Dim lclsOptionsInstallation As eGeneral.OptionsInstallation
        Dim lintIndex As Integer

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  

        lclsOptionsInstallation = New eGeneral.OptionsInstallation

        With lclsOptionsInstallation
            If .FindModules Then
                For lintIndex = 1 To .CountModules
                    .ItemModule((lintIndex))

                    '+ Se cargan las columnas del grid según lo obtenido de la lectura
                    '+ The columns of grid are loaded according to the obtained thing from the reading  

                    insDefineRow(.nModule, .sDescript, .dInstalldate, .sFrame, .sAuxSel, lintIndex)
                Next
            End If
        End With
        lclsOptionsInstallation = Nothing
        Response.Write(mobjGrid.closeTable())
    End Sub

    '%insDefineRow: define la fila correspondiente en base a los valores arrojados de la lectura
    '%insDefineRow: defines the corresponding row on the basis of the thrown values of the reading
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineRow(ByVal nModule As Integer, ByVal sDesModule As String, ByVal dInstallDate As Date, ByVal sFrame As String, ByVal sAuxSel As String, ByVal nIndex As Integer)
        '--------------------------------------------------------------------------------------------
        With mobjGrid
            .Columns("tctModule").DefValue = sDesModule
            .Columns("tcdInstallDate").DefValue = CStr(dInstallDate)
            .Columns("tcnModule").DefValue = CStr(nModule)
            .Columns("tctFrame").DefValue = sFrame
            .Columns("sAuxSel").Checked = mobjValues.StringToType(sAuxSel, eFunctions.Values.eTypeData.etdDouble)

            '+ Se resta 1 al índice pasado como parámetro para que corresponda con el índice del arreglo en 
            '+ JavaScript creado por las rutinas genéricas
            '+ 1 to the index passed like parameter is reduced so that it corresponds with the index of the adjustment in  
            '+ Javascript created by the generic routines

            .Columns("sAuxSel").DefValue = CStr(nIndex - 1)
            .Columns("Sel").Checked = mobjValues.StringToType(sAuxSel, eFunctions.Values.eTypeData.etdDouble)
            Response.Write(.DoRow)
        End With
    End Sub

</script>
<%Response.Expires = -1

    '+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas
    '+ The objects necessary are instancian to work the particularitities of creation of the form by generic routines  

    mobjValues = New eFunctions.Values
    mobjMenu = New eFunctions.Menues
    mobjOptionInstall = New eGeneral.OptionsInstallation
    mobjGrid = New eFunctions.Grid

    mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 401
    mobjValues.sCodisplPage = "MS010"
%> 
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%="<SCRIPT LANGUAGE=""JavaScript"">"%>
var nMainAction = <%=Request.QueryString.Item("nMainAction")%>;
</SCRIPT>
<HTML>
	<HEAD>
		<META NAME		 = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>


		<%=mobjValues.StyleSheet()%>
		<TITLE>Generalidades de las opciones de instalación</TITLE>
	</HEAD>
	
	<BODY ONUNLOAD="closeWindows();">
		<%
            If Request.QueryString.Item("Type") <> "PopUp" Then Response.Write(mobjMenu.setZone(2, "MS010", "MS010.aspx"))
            mobjMenu = Nothing
            Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
		<FORM METHOD="POST" ACTION="valMantGeneral.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%

            '+ Se realiza la lectura de los valores caragados en la tabla de la opciones generales de instalación
            '+ The reading of the inserted values is made in the table of the general options of installation  

            mobjOptionInstall.insPreMS010()

            '+ Define la estructura del grid para luego cargarlo
            '+ Defines the structure of grid soon to load it

            insDefineHeader()

            '+ Si la página no es llamada desde la ventana emergente del grid, se define la página con todos sus campos puntuales
            '+ If the page is not called from the emergent window of grid, the page with all its precise fields is defined  

            If Request.QueryString.Item("Type") <> "PopUp" Then
                insPreMS010()
            Else

                '+ Se realiza el manejo para llamar a la ventana emergente que actualizará los campos del grid
                '+ The handling is made to call to the emergent window that will update the fields of grid

                insPreMS010Upd()
            End If
%>
    </BODY>
</HTML>


<SCRIPT>
//%insSelected: realiza el manejo para la edición de un registro particular del grid para eliminarlo, agregarlo o modificarlo
//%insSelected: makes the handling for the editing of a particular registry of grid to eliminate it, to add it or to modify it
//-----------------------------------------------------------------------------------------------------------------------------------
function insSelected(Field){
//-----------------------------------------------------------------------------------------------------------------------------------
	if (Field.checked)
		EditRecord(Field.value,nMainAction, 'Update')
    else
        EditRecord(Field.value,nMainAction, 'Del', "nModule=" + marrArray[Field.value].tcnModule +  "&dInstallDate=" + marrArray[Field.value].tcdInstallDate)
}
</SCRIPT>





