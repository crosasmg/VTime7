<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ OutputCache Duration="1" VaryByParam="None" NoStore="true" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>

<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores  
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid

    ''' <summary>
    ''' Este procedimiento se encarga de definir las líneas del encabezado del grid
    ''' </summary>
    Private Sub insDefineHeader()
        mobjGrid = New eFunctions.Grid
        mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")

        mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"),
                                     Request.QueryString.Item("sWindowDescript"),
                                     Request.QueryString.Item("nWindowTy"))

        With mobjGrid.Columns
            .AddPossiblesColumn(0, GetLocalResourceObject("NBANK_CODEColumnCaption"), "NBANK_CODE", "Table7", eFunctions.Values.eValuesType.clngComboType)
            .AddPossiblesColumn(0, GetLocalResourceObject("NINSTRUMENT_TYColumnCaption"), "NINSTRUMENT_TY", "Table5539", eFunctions.Values.eValuesType.clngComboType)
            .AddPossiblesColumn(0, GetLocalResourceObject("NCARD_TYPEColumnCaption"), "NCARD_TYPE", "Table183", eFunctions.Values.eValuesType.clngComboType)
            .AddTextColumn(0, GetLocalResourceObject("SNUMBERColumnCaption"), "SNUMBER", 25, "")
            .AddDateColumn(0, GetLocalResourceObject("DCARDEXPIRColumnCaption"), "DCARDEXPIR")
            .AddDateColumn(0, GetLocalResourceObject("DSTARTDATEColumnCaption"), "DSTARTDATE")
            .AddDateColumn(0, GetLocalResourceObject("DTERM_DATEColumnCaption"), "DTERM_DATE")
            .AddNumericColumn(0, GetLocalResourceObject("NQUOTAColumnCaption"), "NQUOTA", 5, , , , True, , , , "QuotaOnChange(this)")
            .AddNumericColumn(0, GetLocalResourceObject("NAMOUNTColumnCaption"), "NAMOUNT", 18, , , , True, 6)
            .AddPossiblesColumn(0, GetLocalResourceObject("NCURRENCYColumnCaption"), "NCURRENCY", "Table11", eFunctions.Values.eValuesType.clngComboType)
            .AddHiddenColumn("NCONSECUTIVE", "")
            .AddHiddenColumn("DEFFECDATE", "")
        End With
        With mobjGrid
            .Codispl = "FR001"
            .Height = 420
            .Width = 450
            .Columns("NINSTRUMENT_TY").EditRecord = True
            .AddButton = True
            .DeleteButton = True
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If

            .sDelRecordParam = "NCONSECUTIVE='+ marrArray[lintIndex].NCONSECUTIVE + '" &
                              "&DEFFECDATE='+ marrArray[lintIndex].DEFFECDATE + '"
		
        End With
    End Sub

    ''' <summary>
    ''' Esta rutina se encarga de realizar las operaciones correspondientes a la actualizacion de datos de la ventana
    ''' </summary>
    Private Sub insPreFR001()
        For Each item As ePolicy.FinancialInstrument In ePolicy.FinancialInstrument.Retrieve(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
            With mobjGrid
                .Columns("NBANK_CODE").DefValue = item.NBANK_CODE
                .Columns("NINSTRUMENT_TY").DefValue = item.NINSTRUMENT_TY
                .Columns("NCARD_TYPE").DefValue = item.NCARD_TYPE
                .Columns("SNUMBER").DefValue = item.SNUMBER
                .Columns("DCARDEXPIR").DefValue = item.DCARDEXPIR
                .Columns("DSTARTDATE").DefValue = item.DSTARTDATE
                .Columns("DTERM_DATE").DefValue = item.DTERM_DATE
                .Columns("NQUOTA").DefValue = item.NQUOTA
                .Columns("NAMOUNT").DefValue = item.NAMOUNT
                .Columns("NCURRENCY").DefValue = item.NCURRENCY
                .Columns("NCONSECUTIVE").DefValue = item.NCONSECUTIVE
                .Columns("DEFFECDATE").DefValue = item.DEFFECDATE
                Response.Write(.DoRow)
            End With
        Next
        Response.Write(mobjGrid.closeTable())
    End Sub

    ''' <summary>
    ''' Se encarga de mostrar el código correspondiente a la actualización de la vantana
    ''' </summary>
    Private Sub insPreFR001Upd()
        With Request
            If .QueryString.Item("Action") = "Del" Then
                If ePolicy.FinancialInstrument.Delete(Session("sCertype"),
                                                      Session("nBranch"),
                                                      Session("nProduct"),
                                                      Session("nPolicy"),
                                                      Session("nCertif"),
                                                      Session("dEffecdate"),
                                                      .QueryString.Item("NCONSECUTIVE"),
                                                      mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate),
                                                      Session("nUsercode")) Then
                    Response.Write(mobjValues.ConfirmDelete())
                End If
            End If
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
    End Sub

</script>
<html>
<head>
<title/>
<%
    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
    mobjValues.ActionQuery = Session("bQuery")
    Response.Write("<SCRIPT>" & " var mstrThousandSep = """ & mobjValues.msUserThousandSeparator & """;" & " var mstrDecimalSep = """ & mobjValues.msUserDecimalSeparator & """</SCRIPT>")
    mobjMenu = New eFunctions.Menues   
    With Response       
        .Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
        .Write(mobjValues.StyleSheet() & vbCrLf)
        If Request.QueryString.Item("Type") <> "PopUp" Then
            .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
            .Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
        End If
    End With
%>
<script type="text/javascript" language="javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript" language="javascript">
    function QuotaOnChange(field) {
        with (document.forms[0]) {
            if ((NINSTRUMENT_TY.value == 5 || NINSTRUMENT_TY.value == 4) &&
                DSTARTDATE.value != '' &&
                NQUOTA.value != '') 
                insDefValues('FR001.Quota', "DSTARTDATE=" + DSTARTDATE.value + "&NQUOTA=" + NQUOTA.value, '/VTimeNet/Policy/PolicySeq')
        }
    }
</script>
</head>
<body onunload="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
<form method="post" id="FORM" name="FR001" action="valPolicySeq.aspx?Mode=1">
<%
    Call insDefineHeader() 
    If Request.QueryString.Item("Type") <> "PopUp" Then
        Call insPreFR001()
    Else
        Call insPreFR001Upd()
    End If   
%>
</form>
</body>
</html>
