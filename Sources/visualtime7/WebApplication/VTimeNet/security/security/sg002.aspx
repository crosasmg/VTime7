<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eSecurity" %>
<script language="VB" runat="Server">

    '-Se crea constante para limitar numero de registro (anteriormente estaba en duro)
    Const MAX_RECORDS As Short = 200

    '-Objeto para el manejo de las funciones generales de carga de valores.
    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenues As eFunctions.Menues

    Dim mintRow As Short


    '%insDefineHeader:Permite definir las columnas del grid, así como habilitar o inhabilitar el 
    '%botón de eliminar y registrar.
    '-----------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '-----------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        mobjGrid.sCodisplPage = "SG002"

        '+ Se definen las columnas del Grid.

        With mobjGrid
            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "SG002"
            .Height = 380
            .Width = 600
            .Top = 200
            .Left = 100
            .AddButton = True
            .DeleteButton = True
        End With

        With mobjGrid.Columns
            Call .AddPossiblesColumn(100422, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table511", eFunctions.Values.eValuesType.clngComboType,  , False, , , ,  , , , GetLocalResourceObject("cbeTypeColumnTooltip"))
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddTextColumn(0, GetLocalResourceObject("tctCodeColumnCaption"), "tctCode", 8, "", False)
                mobjGrid.Columns("tctCode").EditRecord = True
            End If

            If Request.QueryString.Item("sInd_type") = "2" Then
                Call .AddPossiblesColumn(100428, GetLocalResourceObject("valModTranColumnCaption"), "valModTran", "tabTransac", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , False, 8, GetLocalResourceObject("valModTranColumnToolTip"), eFunctions.Values.eTypeCode.eString,  , True)
            Else
                Call .AddPossiblesColumn(100428, GetLocalResourceObject("valModTranColumnCaption"), "valModTran", "tabWindows_Menu", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , False, 8, GetLocalResourceObject("valModTranColumnToolTip"), eFunctions.Values.eTypeCode.eString,  , True)
            End If
            Call .AddCheckColumn(100432, GetLocalResourceObject("chkSupervisColumnCaption"), "chkSupervis", "", 2, CStr(1))
            Call .AddNumericColumn(100430, GetLocalResourceObject("tcnAmelevelColumnCaption"), "tcnAmelevel", 1, CStr(0), False, GetLocalResourceObject("tcnAmelevelColumnToolTip"),  , 0)
            Call .AddNumericColumn(100431, GetLocalResourceObject("tcnInqlevelColumnCaption"), "tcnInqlevel", 1, CStr(0), False, GetLocalResourceObject("tcnInqlevelColumnToolTip"),  , 0)
            Call .AddCheckColumn(100433, GetLocalResourceObject("chkPermittedColumnCaption"), "chkPermitted", "", 1, CStr(1))
        End With

        With mobjGrid
            .Columns("cbeType").Disabled = (Request.QueryString.Item("Action") = "Update")
            .Columns("valModTran").Disabled = (Request.QueryString.Item("Action") = "Update")


            '+ Si la acción que viaja a través del QueryString es Consulta (401), Elimiación (303) o el
            '+ parámetro nMainAction tiene valor NULO (vbNUllString o ""), la propiedad ActionQuery se setea en TRUE,
            '+ de lo contrario se setea en FALSE
            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Or CDbl(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction"))) = 303 Then
                .Columns("Sel").GridVisible = False
                .ActionQuery = True
            Else
                .Columns("Sel").GridVisible = True
                .ActionQuery = False
            End If

            .Columns("valModTran").Disabled = True

            If Request.QueryString.Item("Type") = "PopUp" Then
                .Columns("chkSupervis").Disabled = False
                .Columns("chkPermitted").Disabled = False
            Else
                .Columns("chkSupervis").Disabled = True
                .Columns("chkPermitted").Disabled = True
            End If

            .Columns("cbeType").OnChange = "insHandleGrid(this,""" & Request.QueryString.Item("Action") & """)"

            .sDelRecordParam = "sInd_type=' + marrArray[lintIndex].cbeType + '&sModules=' + marrArray[lintIndex].valModTran + '"

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '%insPreSG002: Se definen los objetos a ser utilizados.
    '-----------------------------------------------------------------------------------------
    Private Sub insPreSG002()
        '-----------------------------------------------------------------------------------------
        Dim lintCount As Short
        Dim lintIndex As Object
        Dim lcolSecur_sches As eSecurity.Secur_sches
        Dim lclsSecur_sche As Object


        Response.Write("" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//%insPreZone: Se definen las acciones a utilizar." & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function insPreZone(llngAction){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("	switch (llngAction){" & vbCrLf)
        Response.Write("	    case 301:" & vbCrLf)
        Response.Write("	    case 302:" & vbCrLf)
        Response.Write("	    case 401:" & vbCrLf)
        Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
        Response.Write("	        break;" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("function ControlNextBack(Option){" & vbCrLf)
        Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
        Response.Write("    var lstrURL = self.document.location.href.replace(/&nRow=.*/,'');" & vbCrLf)
        Response.Write("    var lintRow = ")


        Response.Write(mintRow)


        Response.Write(";" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("	switch(Option){" & vbCrLf)
        Response.Write("		case ""Next"":" & vbCrLf)
        Response.Write("			lintRow = lintRow + 50;" & vbCrLf)
        Response.Write("			break;" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("		case ""Back"":" & vbCrLf)
        Response.Write("			if(lintRow > 1){" & vbCrLf)
        Response.Write("				lintRow = lintRow - 50;" & vbCrLf)
        Response.Write("			}" & vbCrLf)
        Response.Write("	}" & vbCrLf)
        Response.Write("	self.document.location.href = lstrURL = lstrURL + ""&nRow="" + lintRow;" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("</" & "SCRIPT>" & vbCrLf)
        Response.Write("")



        '+ Se setea el objeto y se realiza la lectura del o los registros a ser mostrados
        '+ en las columnas del grid.
        lcolSecur_sches = New eSecurity.Secur_sches

        If lcolSecur_sches.FindLevels(Session("sSche_codeWin"), True, mintRow) Then
            lintCount = 0
            For Each lclsSecur_sche In lcolSecur_sches
                With lclsSecur_sche
                    mobjGrid.Columns("cbeType").DefValue = .sInd_Type
                    mobjGrid.Columns("tctCode").DefValue = .sCode_mt
                    mobjGrid.Columns("valModTran").DefValue = .sCode_mt
                    mobjGrid.Columns("valModTran").Descript = .sDescCode_mt

                    If .sSupervis = "1" Then
                        mobjGrid.Columns("chkSupervis").Checked = 1
                    Else
                        mobjGrid.Columns("chkSupervis").Checked = 2
                    End If

                    mobjGrid.Columns("tcnAmelevel").DefValue = .nAmelevel
                    mobjGrid.Columns("tcnInqlevel").DefValue = .nInqlevel

                    If .sPermitted = "1" Then
                        mobjGrid.Columns("chkPermitted").Checked = 1
                    Else
                        mobjGrid.Columns("chkPermitted").Checked = 2
                    End If

                    mobjGrid.sEditRecordParam = "sInd_type=' + marrArray[" & lintCount & "].cbeType + '"

                    Response.Write(mobjGrid.DoRow())
                End With

                lintCount = lintCount + 1
            Next lclsSecur_sche
        End If

        Response.Write(mobjGrid.closeTable())

        lcolSecur_sches = Nothing
        lclsSecur_sche = Nothing
    End Sub

    '%insPreSG002Upd: Permite realizar el llamado a la ventana PopUp.
    '-----------------------------------------------------------------------------------------
    Private Sub insPreSG002Upd()
        '-----------------------------------------------------------------------------------------
        Dim lclsSecur_sche As eSecurity.Secur_sche
        If Request.QueryString.Item("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())


            lclsSecur_sche = New eSecurity.Secur_sche

            Call lclsSecur_sche.insDelLevels(Session("sSche_codeWin"), Request.QueryString.Item("sModules"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))

            Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Security/Security/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</" & "Script>")
        End If

        lclsSecur_sche = Nothing

        Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValSecuritySeqSchema.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))


    End Sub

</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjValues.sCodisplPage = "SG002"

    If Request.QueryString.Item("nRow") = vbNullString Then
        mintRow = 1
    Else
        mintRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdInteger)
    End If
%>
<script language="JavaScript">

    //%insCancel: Permite cancelar la página.
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }

    //-------------------------------------------------------------------------------------------
    function insHandleGrid(Field, sAction) {
        //-------------------------------------------------------------------------------------------
        //+ Se actualiza la columna oculta con la marcada.
        if (sAction == 'Add') {
            if (Field.value != "0") {
                if (Field.value == "1") {
                    self.document.forms[0].valModTran.disabled = false;
                    self.document.forms[0].btnvalModTran.disabled = false;
                    document.forms[0].valModTran.sTabName = 'tabWindows_Menu';
                }
                else {
                    UpdateDiv("valModTranDesc", "");
                    self.document.forms[0].valModTran.disabled = false;
                    self.document.forms[0].btnvalModTran.disabled = false;
                    document.forms[0].valModTran.sTabName = 'tabTransac';
                }
            }
            else {
                self.document.forms[0].valModTran.disabled = true;
                self.document.forms[0].btnvalModTran.disabled = true;
            }
        }
    }
</script>
<script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>




<html>
<head>
    <script>
        //- Variable para el control de versiones
        document.VssVersion = "$$Revision: 6 $|$$Date: 25/11/03 4:26p $|$$Author: Nvaplat18 $"
    </script>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">

    <%

        '+ Se realiza el llamado a las rutinas generales para cargar la página invocada.
        mobjMenues = New eFunctions.Menues

        If Request.QueryString.Item("Type") <> "PopUp" Then
            Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
            Response.Write(mobjMenues.setZone(2, "SG002", "SG002.aspx"))
        End If

        With Response
            .Write(mobjValues.WindowsTitle("SG002"))
            .Write(mobjValues.StyleSheet())
        End With
    %>

</SCRIPT>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="SG002" action="valSecuritySeqSchema.aspx?sTime=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

        <%
            Call insDefineHeader()

            If Request.QueryString.Item("Type") <> "PopUp" Then
                Response.Write(mobjValues.ShowWindowsName("SG002"))
                Call insPreSG002()
                Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", mintRow = 1))
                Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')"))
            Else
                Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
                Call insPreSG002Upd()
            End If
            mobjValues = Nothing
            mobjGrid = Nothing
        %>
    </form>
</body>
</html>






