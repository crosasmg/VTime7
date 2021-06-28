<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>

<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As New eFunctions.Values
    Dim mobjGrid As New eFunctions.Grid
    Dim mobjMenu As New eFunctions.Menues
    
'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
'--------------------------------------------------------------------------------------------
'+ Se definen las columnas del grid.
 
        With mobjGrid.Columns
            Call .AddTextColumn(0, "Sección", "tctSection", 30, vbNullString, , "Sección que conforma el producto", , , , True)
            Call .AddTextColumn(0, "Reporte", "tctsReport", 30, vbNullString, , "Nombre Reporte ", , , , False)
            Call .AddTextColumn(0, "Orden", "tctnOrder", 5, vbNullString, , "Orden del reporte", , , , False)
            Call .AddTextColumn(0, "Rutina", "tctsRoutine", 12, vbNullString, , "Rutina para oculta la el reporte ", , , , False)
            Call .AddHiddenColumn("hddSel", 2)
            Call .AddHiddenColumn("hddsCodispl", vbNullString)
            Call .AddHiddenColumn("hddsPolitype", vbNullString)
            Call .AddHiddenColumn("hddsCompon", vbNullString)
            Call .AddHiddenColumn("hddnTratypep", vbNullString)
            Call .AddHiddenColumn("hddnId", vbNullString)
	
        End With
'+ Se definen las propiedades generales del grid.
        With mobjGrid
            .AddButton = False
            .DeleteButton = False
            .Height = 300
            .Width = 350
            .Codispl = "DP809B"
            .nMainAction = Request.QueryString("nMainAction")
            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
            .Columns("Sel").GridVisible = False
        
        End With
End Sub
'% insPreDP809B: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP809B
'--------------------------------------------------------------------------------------------
        Dim lcolSection_pos As New eProduct.Section_pos
        Dim mclsSection_po As New eProduct.Section_po

        Call mclsSection_po.inspreDP048(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Request.QueryString("sPolitype"), Request.QueryString("sCompon"))
	
        If lcolSection_pos.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                mobjValues.StringToType(Request.QueryString("nTratypep"), eFunctions.Values.eTypeData.etdDouble, True), _
                                mclsSection_po.sPolitype, _
                                mclsSection_po.sCompon, _
                                mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                mobjValues.StringToType(Request.QueryString("nType_amend"), eFunctions.Values.eTypeData.etdDouble, True), _
                                mobjValues.StringToType(Request.QueryString("nOrigin"), eFunctions.Values.eTypeData.etdDouble, True)) Then
            
            For Each mclsSection_po In lcolSection_pos
                With mobjGrid
                    If Not String.IsNullOrEmpty(mclsSection_po.sReport)  Then
                        .Columns("tctSection").DefValue = mclsSection_po.sDescript
                        .Columns("tctsReport").DefValue = mclsSection_po.sReport
                        If mclsSection_po.nOrder > 0 Then
                            .Columns("tctnOrder").DefValue = mclsSection_po.nOrder
                        Else
                            .Columns("tctnOrder").DefValue = eRemoteDB.Constants.strNull
                        End If
                        If mclsSection_po.nId > 0 Then
                            .Columns("tctSection").EditRecord = True
                        Else
                            .Columns("tctSection").HRefScript = eRemoteDB.Constants.strNull
                            .Columns("tctSection").EditRecord = False
                        End If
                        
                        .Columns("tctsRoutine").DefValue = mclsSection_po.sRoutine
                        .Columns("Sel").Checked = 2
                        .Columns("hddSel").DefValue = 2
                        Response.Write(.DoRow)
                    End If
                End With
            Next
        End If
        
		With Response
            .Write(mobjGrid.closeTable())
            .Write(mobjValues.BeginPageButton)
		End With

        lcolSection_pos = Nothing
End Sub
</script>

<% 
    Response.Expires = -1

    'Dim mobjValues As eFunctions.Values
    'Dim mobjMenu As eFunctions.Menues
    'Dim mobjGrid As eFunctions.Grid
    
    Dim mintCount As Integer
    
    'mobjValues = New eFunctions.Values
    'mobjGrid = New eFunctions.Grid
    'mobjMenu = New eFunctions.Menues
    
    mobjValues.sCodisplPage = "DP809B"
    mobjGrid.sCodisplPage = "DP809B"

    mobjGrid.ActionQuery = Session("bQuery")
    mobjValues.ActionQuery = Session("bQuery")
%>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
    <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
 
<%
    Response.Write(mobjValues.StyleSheet())
%>
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 1 $|$$Date: 14/06/11 9:48 $"
</SCRIPT>
<SCRIPT>
    //% ShowSubSequence: abre el pop up del cuadro de pólizas
    //--------------------------------------------------------------------------------------------
    function ShowSubSequence(Index) {
        //--------------------------------------------------------------------------------------------
        ShowPopUp('DP809BA.aspx?scodispl_orig=' + marrArray[Index].hddvalCodispl + '&Type=PopUp', 'DP809BA', 580, 500, 'no', 'no', 200, 80);
    }


</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP809B" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString("nMainAction")%>">
<%
    Response.Write(mobjValues.ShowWindowsName("DP809B"))
    Call insDefineHeader()
    Call insPreDP809B()
    mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
