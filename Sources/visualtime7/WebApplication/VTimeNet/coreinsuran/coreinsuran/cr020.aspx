<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las rutinas genéricas
    Dim mobjMenu As eFunctions.Menues

    '- Se define la variable mobjGrid para el manejo del Grid de la ventana
    Dim mobjGrid As eFunctions.Grid

    '- Se define la variable para la carga del Grid de la ventana 'CR020'
    Dim mclsRetention As eCoReinsuran.Retention


    '%insDefineHeader. Definición de columnas del GRID
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------	
        mobjGrid.sCodisplPage = "cr020"

        With mobjGrid
            .Codispl = "CR020"
            .Width = 420
            .Height = 385
            .Top = 220
        End With

        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            Call .AddPossiblesColumn(100578, GetLocalResourceObject("cboRisk_TypeColumnCaption"), "cboRisk_Type", "table118", eFunctions.Values.eValuesType.clngComboType,  , False,    ,   ,   ,   ,   ,   , GetLocalResourceObject("cboRisk_TypeColumnToolTip"))
            Call .AddNumericColumn(100578, GetLocalResourceObject("tcnMin_CapitaColumnCaption"), "tcnMin_Capita", 18,   ,  , GetLocalResourceObject("tcnMin_CapitaColumnToolTip"), True, 6)
            Call .AddNumericColumn(100579, GetLocalResourceObject("tcnMax_CapitaColumnCaption"), "tcnMax_Capita", 18,  ,  , GetLocalResourceObject("tcnMax_CapitaColumnToolTip"), True, 6)
            Call .AddNumericColumn(100580, GetLocalResourceObject("tcnMin_rateColumnCaption"), "tcnMin_rate", 4,  ,  , GetLocalResourceObject("tcnMin_rateColumnToolTip"), True, 2)
            Call .AddNumericColumn(100581, GetLocalResourceObject("tcnMax_rateColumnCaption"), "tcnMax_rate", 4,  ,  , GetLocalResourceObject("tcnMax_rateColumnToolTip"), True, 2)
            If Session("nType") = 1 Then
                Call .AddCheckColumn(100585, GetLocalResourceObject("chkExclusionColumnCaption"), "chkExclusion", "")
                Call .AddNumericColumn(100582, GetLocalResourceObject("tcnNew_retentColumnCaption"), "tcnNew_retent", 18,  ,  , GetLocalResourceObject("tcnNew_retentColumnToolTip"), True, 6)
            End If
            If Session("nType") = 5 Or Session("nType") = 6 Or Session("nType") = 7 Or Session("nType") = 8 Then
                Call .AddNumericColumn(100583, GetLocalResourceObject("tcnLines_pctColumnCaption"), "tcnLines_pct", 10,  ,  , GetLocalResourceObject("tcnLines_pctColumnToolTip"))
            End If
            If Session("nType") = 2 Or Session("nType") = 3 Then
                Call .AddNumericColumn(100584, GetLocalResourceObject("tcnPercent_CedColumnCaption"), "tcnPercent_Ced", 5,  ,  , GetLocalResourceObject("tcnPercent_CedColumnToolTip"), True, 2)
            End If
            Call .AddHiddenColumn("tcnConsec", CStr(0))
            Call .AddHiddenColumn("tcnSel", CStr(0))
            Call .AddHiddenColumn("sParam", vbNullString)
        End With

        With mobjGrid
            .Columns("cboRisk_Type").EditRecord = True
            .DeleteButton = True
            .AddButton = True
            .sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
            If Session("bQuery") Then
                .DeleteButton = False
                .AddButton = False
                .Columns("Sel").GridVisible = False
                .bOnlyForQuery = True
            End If
            If Session("nType") = 1 Then
                If Request.QueryString.Item("Type") <> "PopUp" Then
                    .Columns("chkExclusion").Disabled = True
                End If
            End If
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
        End With
    End Sub
    '%insPreCR020: Esta función se encarga de cargar los datos en la forma "Folder" 
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCR020()
        '--------------------------------------------------------------------------------------------

        Dim lblnFind As Boolean
        Dim lintCount As Integer
        With mobjValues
            lblnFind = mclsRetention.Find(.StringToType(Session("nNumber"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nType"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("nBranch_rei"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))


        End With

        If lblnFind Then
            lintCount = 0
            For lintCount = 0 To mclsRetention.Count - 1
                If mclsRetention.ItemRetention(lintCount) Then
                    With mobjGrid
                        .Columns("Sel").DefValue = CStr(mclsRetention.nSel)
                        .Columns("cboRisk_Type").DefValue = CStr(mclsRetention.nRisk_type)
                        .Columns("tcnMin_Capita").DefValue = CStr(mclsRetention.nMin_Capita)
                        .Columns("tcnMax_Capita").DefValue = CStr(mclsRetention.nMax_Capita)
                        .Columns("tcnMin_rate").DefValue = CStr(mclsRetention.nMin_rate)
                        .Columns("tcnMax_rate").DefValue = CStr(mclsRetention.nMax_rate)
                        If Session("nType") = 1 Then
                            .Columns("chkExclusion").Checked = CShort(mclsRetention.sExclusion)
                            .Columns("tcnNew_retent").DefValue = CStr(mclsRetention.nNew_retent)
                        End If
                        If Session("nType") = 5 Or Session("nType") = 6 Or Session("nType") = 7 Or Session("nType") = 8 Then
                            .Columns("tcnLines_pct").DefValue = CStr(mclsRetention.nLines_pct)
                        End If
                        If Session("nType") = 2 Or Session("nType") = 3 Then
                            .Columns("tcnPercent_Ced").DefValue = CStr(mclsRetention.nPercentCed)
                        End If
                        .Columns("tcnConsec").DefValue = CStr(mclsRetention.nConsec)

                        '+ Se "arma" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
                        '+ función insPostCR020 cuando se eliminen los registros seleccionados - VCVG - 11/06/2001

                        .Columns("sParam").DefValue = "nNumber=" & Session("nNumber") & "&nType=" & Session("nType") & "&nBranch=" & Session("nBranch_rei") & "&dEffecdate=" & Session("dEffecdate") & "&nConsec=" & mclsRetention.nConsec & "&nUserCode=" & Session("nUsercode")

                    End With
                    Response.Write(mobjGrid.DoRow())
                End If
            Next
        End If
        'Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (GRID)            	
        Response.Write(mobjGrid.CloseTable())
    End Sub
    '% insPreCR020Upd. Se define esta funcion para contruir el contenido de la ventana UPD de los plenos y retenciones
    '--------------------------------------------------------------------------------------------------------------------
    Private Sub insPreCR020Upd()
        '--------------------------------------------------------------------------------------------------------------------		
        Dim lblnPost As Boolean
        Dim strExit As String
        Dim lintSel As Integer

        If Request.QueryString.Item("Action") = "Del" Then
            lintSel = 3
            strExit = "1"

            Response.Write(mobjValues.ConfirmDelete())

            With Request
                Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR020", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            End With

            With Request
                lblnPost = mclsRetention.insPostCR020("CR020", lintSel, strExit, CInt(.QueryString.Item("nNumber")), CInt(.QueryString.Item("nType")), CInt(.QueryString.Item("nBranch")), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CInt(.QueryString.Item("nConsec")), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, CStr(eRemoteDB.Constants.strNull), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, Session("nUsercode"))

            End With
        ElseIf Request.QueryString.Item("Action") = "Add" Then
            With Request
                Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR020", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            End With

            Response.Write("		" & vbCrLf)
            Response.Write("	<SCRIPT>self.document.forms[0].elements[""tcnSel""].value=1</" & "SCRIPT>")


        Else
            With Request
                Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCoReinsuran.aspx", "CR020", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
            End With

            Response.Write("		" & vbCrLf)
            Response.Write("	<SCRIPT>self.document.forms[0].elements[""tcnSel""].value=2</" & "SCRIPT>")


        End If
    End Sub

</script>
<%Response.Expires = 0
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mclsRetention = New eCoReinsuran.Retention

If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "CR020", "CR020.aspx"))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

mobjValues.sCodisplPage = "cr020"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCR020" ACTION="valCoReinsuran.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<TD><BR></TD>")
	Response.Write(mobjValues.ShowWindowsName("CR020"))
	Call insPreCR020()
Else
	Response.Write(mobjValues.ShowWindowsName("CR020"))
	Call insPreCR020Upd()
End If

%>    
<SCRIPT>    
//+ Esta línea guarda la versión procedente de VSS 
   document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.59 $"     
</SCRIPT> 
</FORM>
</BODY>
</HTML>
	




