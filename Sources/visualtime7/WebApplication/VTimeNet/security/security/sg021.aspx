<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

    Private sScheCode As String
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid

    Dim mobjVTBranchGrid As eFunctions.Grid
    Dim mobjVTPayWayGrid As eFunctions.Grid
    Dim mobjVTRolesGrid As eFunctions.Grid
    Dim mobjVNTBranchGrid As eFunctions.Grid
    Dim mobjVNTReasonGrid As eFunctions.Grid
    Dim mobjVNTPayWayGrid As eFunctions.Grid
    Dim mobjProfile As eSecurity.SecurScheSurr



    '**********************************************************************************************************
    '*************************************** FUNCIONES VBScript ***********************************************
    '*************************************** FUNCTIONS VBScript ***********************************************
    '**********************************************************************************************************


    '%insDefineRow: define la fila correspondiente en base a los valores arrojados de la lectura
    '%insDefineRow: defines the corresponding row on the basis of the thrown values of the reading
    '--------------------------------------------------------------------------------------------
    Private Function IsSelected(ByRef bSelected As Boolean) As String
        '--------------------------------------------------------------------------------------------
        If bSelected Then
            IsSelected = "1"
        Else
            IsSelected = "2"
        End If
    End Function


    '%insDefineRow: define la fila correspondiente en base a los valores arrojados de la lectura
    '%insDefineRow: defines the corresponding row on the basis of the thrown values of the reading
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineRow(ByRef oGrid As eFunctions.Grid, ByRef oItem As eSecurity.GenericItem)
        '--------------------------------------------------------------------------------------------
        With oGrid
            'Response.write oItem.sDescript & oItem.bSelected
            .Columns(2).Checked = CShort(IsSelected((oItem.bSelected)))
            .Columns(2).DefValue = CStr(oItem.nId)
            .Columns(3).DefValue = oItem.sDescript
            Response.Write(.DoRow)
        End With
    End Sub


    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineVTBranchGrid()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjVTBranchGrid
            .Columns.AddCheckColumn(0, vbNullString, "chkSelectedVTBranch", vbNullString, False)
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctBranchNameColumnCaption"), "tctBranchName", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            .Columns("Sel").GridVisible = False
            .AddButton = False
            .DeleteButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '.Columns("chkSelectedVTBranch").OnClick = "VTBranchSelected(this);"
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
        End With
    End Sub

    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineVTPayWayGrid()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjVTPayWayGrid
            .Columns.AddCheckColumn(0, vbNullString, "chkSelectedVTPayWay", vbNullString, False)
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctPayWayNameColumnCaption"), "tctPayWayName", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")

            .Columns("Sel").GridVisible = False
            .AddButton = False
            .DeleteButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '.Columns("chkSelectedVTPayWay").OnClick = "VTPayWaySelected(this);"
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
        End With
    End Sub


    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineVTRoleGrid()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjVTRolesGrid
            .Columns.AddCheckColumn(0, vbNullString, "chkSelectedVTRole", vbNullString, False)
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctRoleNameColumnCaption"), "tctRoleName", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")

            .Columns("Sel").GridVisible = False
            .AddButton = False
            .DeleteButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '.Columns("chkSelectedRolePayWay").OnClick = "VTRoleSelected(this);"
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
        End With
    End Sub



    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineVNTBranchGrid()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjVNTBranchGrid
            .Columns.AddCheckColumn(0, vbNullString, "chkSelectedVNTBranch", vbNullString, False)
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctBranchNameColumnCaption"), "tctBranchName", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")

            .Columns("Sel").GridVisible = False
            .sArrayName = "xVNTBranchGrid"
            .AddButton = False
            .DeleteButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '.Columns("chkSelectedVNTBranch").OnClick = "VNTBranchSelected(this);"
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
        End With
    End Sub

    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineVNTPayWayGrid()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjVNTPayWayGrid
            .Columns.AddCheckColumn(0, vbNullString, "chkSelectedVNTPayWay", vbNullString, False)
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctPayWayNameColumnCaption"), "tctPayWayName", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            .sArrayName = "xVNTPWGrid"

            .Columns("Sel").GridVisible = False
            .AddButton = False
            .DeleteButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '.Columns("chkSelectedVNTPayWay").OnClick = "VNTPayWaySelected(this);"
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
        End With
    End Sub


    '%insDefineHeader: define el header del grid a mostrara en la página de los módulos activos e inactivos en el sistema
    '--------------------------------------------------------------------------------------------------------------------------------------------
    Private Sub insDefineVNTReasonGrid()
        '--------------------------------------------------------------------------------------------------------------------------------------------
        With mobjVNTReasonGrid
            .Columns.AddCheckColumn(0, vbNullString, "chkSelectedVNTReason", vbNullString, False)
            .Columns.AddTextColumn(0, GetLocalResourceObject("tctReasonNameColumnCaption"), "tctReasonName", 30, vbNullString,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
            .sArrayName = "xReasonGrid"

            .Columns("Sel").GridVisible = False
            .AddButton = False
            .DeleteButton = False
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
            '.Columns("chkSelectedVNTReasonPayWay").OnClick = "VNTReasonSelected(this);"
            .ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
        End With
    End Sub


    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '----------------------------------------------------------------------------------------------
    Private Sub insDrawVNTBranchGrid()
        '----------------------------------------------------------------------------------------------
        Dim lobjItem As eSecurity.GenericItem

        lobjItem = New eSecurity.GenericItem

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  


        With mobjProfile
            If .FindVNTBranches(sScheCode) Then
                For Each lobjItem In mobjProfile.cVNTBranches
                    insDefineRow(mobjVNTBranchGrid, lobjItem)
                Next lobjItem
            End If
        End With
        Response.Write(mobjVNTBranchGrid.closeTable())
    End Sub


    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '----------------------------------------------------------------------------------------------
    Private Sub insDrawVNTReasonGrid()
        '----------------------------------------------------------------------------------------------
        Dim lintIndex As Object
        Dim lobjItem As eSecurity.GenericItem

        lobjItem = New eSecurity.GenericItem

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  


        With mobjProfile
            If .FindVNTReasons(sScheCode) Then
                For Each lobjItem In mobjProfile.cVNTReasons
                    insDefineRow(mobjVNTReasonGrid, lobjItem)
                Next lobjItem
            End If
        End With
        Response.Write(mobjVNTReasonGrid.closeTable())
    End Sub



    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '----------------------------------------------------------------------------------------------
    Private Sub insDrawVNTPayWayGrid()
        '----------------------------------------------------------------------------------------------
        Dim lintIndex As Object
        Dim lobjItem As eSecurity.GenericItem

        lobjItem = New eSecurity.GenericItem

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  


        With mobjProfile
            If .FindVNTPayWays(sScheCode) Then
                For Each lobjItem In mobjProfile.cVNTPayWays
                    insDefineRow(mobjVNTPayWayGrid, lobjItem)
                Next lobjItem
            End If
        End With
        Response.Write(mobjVNTPayWayGrid.closeTable())
    End Sub


    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '----------------------------------------------------------------------------------------------
    Private Sub insDrawVTBranchGrid()
        '----------------------------------------------------------------------------------------------
        Dim lobjItem As eSecurity.GenericItem

        lobjItem = New eSecurity.GenericItem

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  


        With mobjProfile
            If .FindVTBranches(sScheCode) Then
                For Each lobjItem In mobjProfile.cVTBranches
                    insDefineRow(mobjVTBranchGrid, lobjItem)
                Next lobjItem
            End If
        End With
        Response.Write(mobjVTBranchGrid.closeTable())
    End Sub


    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '----------------------------------------------------------------------------------------------
    Private Sub insDrawVTRolesGrid()
        '----------------------------------------------------------------------------------------------
        Dim lintIndex As Object
        Dim lobjItem As eSecurity.GenericItem

        lobjItem = New eSecurity.GenericItem

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  


        With mobjProfile
            If .FindVTRoles(sScheCode) Then
                For Each lobjItem In mobjProfile.cVTRoles
                    insDefineRow(mobjVTRolesGrid, lobjItem)
                Next lobjItem
            End If
        End With
        Response.Write(mobjVTRolesGrid.closeTable())
    End Sub



    '%insDefineGrid: define el grid según lo leído de las tablas incolucradas
    '----------------------------------------------------------------------------------------------
    Private Sub insDrawVTPayWayGrid()
        '----------------------------------------------------------------------------------------------
        Dim lintIndex As Object
        Dim lobjItem As eSecurity.GenericItem

        lobjItem = New eSecurity.GenericItem

        '+ Se instancian los objetos para poder cargar el grid de valores
        '+ The objects are instancian to be able to load grid of values  


        With mobjProfile
            If .FindVTPayWays(sScheCode) Then
                For Each lobjItem In mobjProfile.cVTPayWays
                    insDefineRow(mobjVTPayWayGrid, lobjItem)
                Next lobjItem
            End If
        End With
        Response.Write(mobjVTPayWayGrid.closeTable())
    End Sub


    '% insPreSG021Upd: carga los valores de la página SG021
    '--------------------------------------------------------------------------------------------------
    Private Function insPreSG021() As Object
        '--------------------------------------------------------------------------------------------------
        mobjProfile.Find(sScheCode, False)
        Response.Write(mobjValues.FIELDSET(0, "Vida no tradicional"))


        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("		<TD VALIGN=TOP width=50%>" & vbCrLf)
        Response.Write("			<TABLE WIDTH=""100%"" COLS=2>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("cbenTypeRescCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbenTypeResc", "table5569", eFunctions.Values.eValuesType.clngComboType, CStr(mobjProfile.nTypeResc),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTypeRescToolTip"),  , 2))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("					" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD colspan =2>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.CheckControl("chkModDateR", GetLocalResourceObject("chkModDateRCaption"), mobjProfile.sModDateR, "1",  ,  ,  , GetLocalResourceObject("chkModDateRToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD colspan =2>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.CheckControl("chkModDateP", GetLocalResourceObject("chkModDatePCaption"), mobjProfile.sModDateP, "1",  ,  ,  , GetLocalResourceObject("chkModDatePToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("cbeValueTypCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbeValueTyp", "table5615", eFunctions.Values.eValuesType.clngComboType, CStr(mobjProfile.nValueTyp),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeValueTypToolTip"),  , 2))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodoCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.CheckControl("chkRescTot", GetLocalResourceObject("chkRescTotCaption"), mobjProfile.sRescTot, "1",  ,  ,  , GetLocalResourceObject("chkRescTotToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				    <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.CheckControl("chkRescPar", GetLocalResourceObject("chkRescParCaption"), mobjProfile.sRescPar, "1",  ,  ,  , GetLocalResourceObject("chkRescParToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo2Caption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				   <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optTypeExecut", GetLocalResourceObject("optTypeExecut_1Caption"), mobjProfile.sTypeExecutP, "1",  ,  ,  , GetLocalResourceObject("optTypeExecut_1ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optTypeExecut", GetLocalResourceObject("optTypeExecut_2Caption"), mobjProfile.sTypeExecutD, "2",  ,  ,  , GetLocalResourceObject("optTypeExecut_2ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optTypeExecut", GetLocalResourceObject("optTypeExecut_3Caption"), mobjProfile.sTypeExecutB, "3",  ,  ,  , GetLocalResourceObject("optTypeExecut_3ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("	        </TABLE>" & vbCrLf)
        Response.Write("		</TD>" & vbCrLf)
        Response.Write("		<TD>" & vbCrLf)
        Response.Write("			<TABLE width=100%>" & vbCrLf)
        Response.Write("            <TR> <TD>" & vbCrLf)
        Response.Write("			")

        insDrawVNTBranchGrid()
        Response.Write("" & vbCrLf)
        Response.Write("            </TD></TR>" & vbCrLf)
        Response.Write("            <TR> <TD>" & vbCrLf)
        Response.Write("			")

        insDrawVNTReasonGrid()
        Response.Write("" & vbCrLf)
        Response.Write("            </TD></TR>" & vbCrLf)
        Response.Write("            <TR> <TD>" & vbCrLf)
        Response.Write("			")

        insDrawVNTPayWayGrid()
        Response.Write("" & vbCrLf)
        Response.Write("			<!--DIV ID=""Scroll2"" style=""width:300;height:200;overflow:auto;outset gray"">" & vbCrLf)
        Response.Write("	        </DIV-->" & vbCrLf)
        Response.Write("            </TD></TR>" & vbCrLf)
        Response.Write("			</TABLE>" & vbCrLf)
        Response.Write("	    </TD>" & vbCrLf)
        Response.Write("	</TR>" & vbCrLf)
        Response.Write("	</TABLE>")


        Response.Write(mobjValues.CloseFIELDSET())

        Response.Write("<BR>")
        Response.Write(mobjValues.FIELDSET(0, "Vida tradicional"))


        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("    <TR>" & vbCrLf)
        Response.Write("		<TD VALIGN=TOP width=50%>" & vbCrLf)
        Response.Write("			<TABLE WIDTH=""100%"" COLS=2>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD><LABEL ID=0>" & GetLocalResourceObject("cbenTypeRescCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("					<TD>" & vbCrLf)
        Response.Write("						")

        mobjValues.BlankPosition = False
        Response.Write(mobjValues.PossiblesValues("cbeTypeRescV", "table5569", eFunctions.Values.eValuesType.clngComboType, CStr(mobjProfile.nTypeRescV),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeTypeRescVToolTip"),  , 2))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("					" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD colspan =2>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.CheckControl("chkModDatePV", GetLocalResourceObject("chkModDatePVCaption"), mobjProfile.sModDatePV, "1",  ,  ,  , GetLocalResourceObject("chkModDatePVToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("					<TD colspan =2>" & vbCrLf)
        Response.Write("						")

        Response.Write(mobjValues.CheckControl("chkModDateRV", GetLocalResourceObject("chkModDateRVCaption"), mobjProfile.sModDateRV, "1",  ,  ,  , GetLocalResourceObject("chkModDateRVToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					</TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodoCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.CheckControl("chkRescTotV", GetLocalResourceObject("chkRescTotVCaption"), mobjProfile.sRescTotV, "1",  ,  ,  , GetLocalResourceObject("chkRescTotVToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				    <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.CheckControl("chkRescParV", GetLocalResourceObject("chkRescParVCaption"), mobjProfile.sRescParV, "1",  ,  ,  , GetLocalResourceObject("chkRescParVToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo2Caption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				   <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optTypeExecutV", GetLocalResourceObject("optTypeExecutV_1Caption"), mobjProfile.sTypeExecutVP, "1",  ,  ,  , GetLocalResourceObject("optTypeExecutV_1ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optTypeExecutV", GetLocalResourceObject("optTypeExecutV_2Caption"), mobjProfile.sTypeExecutVD, "2",  ,  ,  , GetLocalResourceObject("optTypeExecutV_2ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optTypeExecutV", GetLocalResourceObject("optTypeExecutV_3Caption"), mobjProfile.sTypeExecutVB, "3",  ,  ,  , GetLocalResourceObject("optTypeExecutV_3ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo5Caption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				   <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optAnulRec", GetLocalResourceObject("optAnulRec_1Caption"), mobjProfile.sAnulRecY, "1",  ,  ,  , GetLocalResourceObject("optAnulRec_1ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optAnulRec", GetLocalResourceObject("optAnulRec_2Caption"), mobjProfile.sAnulRecN, "2",  ,  ,  , GetLocalResourceObject("optAnulRec_2ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optAnulRec", GetLocalResourceObject("optAnulRec_3Caption"), mobjProfile.sAnulRecB, "3",  ,  ,  , GetLocalResourceObject("optAnulRec_3ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo6Caption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				   <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optRequest", GetLocalResourceObject("optRequest_1Caption"), mobjProfile.sRequestY, "1",  ,  ,  , GetLocalResourceObject("optRequest_1ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optRequest", GetLocalResourceObject("optRequest_2Caption"), mobjProfile.sRequestN, "2",  ,  ,  , GetLocalResourceObject("optRequest_2ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optRequest", GetLocalResourceObject("optRequest_3Caption"), mobjProfile.sRequestB, "3",  ,  ,  , GetLocalResourceObject("optRequest_3ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				    <TD CLASS=""HighLighted"" COLSPAN=2><LABEL ID=100440><A NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo7Caption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("			    </TR>" & vbCrLf)
        Response.Write("				<TR><TD colspan=2 ><HR></TD></TR>				    " & vbCrLf)
        Response.Write("				<TR>" & vbCrLf)
        Response.Write("				   <TD>" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optReport", GetLocalResourceObject("optReport_1Caption"), mobjProfile.sReportY, "1",  ,  ,  , GetLocalResourceObject("optReport_1ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optReport", GetLocalResourceObject("optReport_2Caption"), mobjProfile.sReportN, "2",  ,  ,  , GetLocalResourceObject("optReport_2ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("					")

        Response.Write(mobjValues.OptionControl(0, "optReport", GetLocalResourceObject("optReport_3Caption"), mobjProfile.sReportB, "3",  ,  ,  , GetLocalResourceObject("optReport_3ToolTip")))
        Response.Write("" & vbCrLf)
        Response.Write("                    </TD>" & vbCrLf)
        Response.Write("				</TR>" & vbCrLf)
        Response.Write("" & vbCrLf)
        Response.Write("	        </TABLE>" & vbCrLf)
        Response.Write("		</TD>" & vbCrLf)
        Response.Write("		<TD align=top border =1>" & vbCrLf)
        Response.Write("			<TABLE width=100%>" & vbCrLf)
        Response.Write("            <TR> <TD>" & vbCrLf)
        Response.Write("			")

        insDrawVTBranchGrid()
        Response.Write("" & vbCrLf)
        Response.Write("            </TD></TR>" & vbCrLf)
        Response.Write("            <TR> <TD>" & vbCrLf)
        Response.Write("			<DIV ID=""Scroll2"" style=""height:250;overflow:auto;outset gray"">" & vbCrLf)
        Response.Write("			")

        insDrawVTRolesGrid()
        Response.Write("" & vbCrLf)
        Response.Write("	        </DIV>" & vbCrLf)
        Response.Write("            </TD></TR>" & vbCrLf)
        Response.Write("            <TR> <TD>" & vbCrLf)
        Response.Write("			")

        insDrawVTPayWayGrid()
        Response.Write("" & vbCrLf)
        Response.Write("            </TD></TR>" & vbCrLf)
        Response.Write("			</TABLE>" & vbCrLf)
        Response.Write("	    </TD>" & vbCrLf)
        Response.Write("	</TR>" & vbCrLf)
        Response.Write("	</TABLE>")


        Response.Write(mobjValues.CloseFIELDSET())
    End Function

</script>
<%Response.Expires = -1

sScheCode = Session("sSche_codeWin")

'+ Se instancian los objetos necesarios para trabajr las particularidades de creación de la forma por rutinas genéricas
'+ The objects necessary are instancian to work the particularitities of creation of the form by generic routines  

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mobjProfile = New eSecurity.SecurScheSurr


mobjVTBranchGrid = New eFunctions.Grid
mobjVTPayWayGrid = New eFunctions.Grid
mobjVTRolesGrid = New eFunctions.Grid
mobjVNTBranchGrid = New eFunctions.Grid
mobjVNTReasonGrid = New eFunctions.Grid
mobjVNTPayWayGrid = New eFunctions.Grid

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "SG021"
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
Response.Write(mobjMenu.setZone(2, "SG021", "SG021.aspx"))
mobjMenu = Nothing
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
		<FORM METHOD="POST" ACTION="valsecurityseqschema.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%


'+ Define la estructura del grid para luego cargarlo
'+ Defines the structure of grid soon to load it
Call insDefineVTBranchGrid()
Call insDefineVTPayWayGrid()
Call insDefineVTRoleGrid()


Call insDefineVNTBranchGrid()
Call insDefineVNTReasonGrid()
Call insDefineVNTPayWayGrid()
insPreSG021()
%>
    </BODY>
</HTML>



<SCRIPT>
//%insSelected: realiza el manejo para la edición de un registro particular del grid para eliminarlo, agregarlo o modificarlo
//%insSelected: makes the handling for the editing of a particular registry of grid to eliminate it, to add it or to modify it
//-----------------------------------------------------------------------------------------------------------------------------------
function insSelected(Field){
//-----------------------------------------------------------------------------------------------------------------------------------
}
</SCRIPT>





