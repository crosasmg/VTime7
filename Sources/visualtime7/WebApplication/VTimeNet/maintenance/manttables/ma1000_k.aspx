<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

    '- Objetos genericos para manejo de valores, menu y grilla
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid

    '- Nombre de tabla general
    Dim mstrTable As String
    '- Codigo de error al hacer busqueda. Cero(0) si no hay error
    Dim mlngNumErr As Integer
    Dim mstrTableNew As String


    '%insDefineHeader: Definición de columnas del Grid
    '-----------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '-----------------------------------------------------------------------------------------
        Dim lclsTabGen As eGeneralForm.TabGen

        lclsTabGen = New eGeneralForm.TabGen
        mobjGrid = New eFunctions.Grid
        mstrTable = lclsTabGen.reaWindow(Request.QueryString.Item("sCodispl"))
        Call lclsTabGen.insSearchKeyValues(CStr(mstrTable))
        mlngNumErr = lclsTabGen.nNumErr

        mstrTableNew = vbNullString
        If lclsTabGen.ReaTable_NameXXX(CStr(mstrTable), Session("companyId")) Then
            mstrTableNew = lclsTabGen.sValorAdic
        End If

        '+ Se definen las columnas del Grid
        With mobjGrid.Columns

            Call .AddNumericColumn(40608, GetLocalResourceObject("tcnCodigintColumnCaption"), "tcnCodigint", lclsTabGen.nFieldLength, vbNullString, False, GetLocalResourceObject("tcnCodigintColumnToolTip"))

            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddTextColumn(40609, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", lclsTabGen.nFieldLengthDesc, vbNullString, False, GetLocalResourceObject("tctDescriptColumnToolTip"))
            Else
                Call .AddTextAreaColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", vbNullString, 4, 75, False, GetLocalResourceObject("tctDescriptColumnToolTip"))
            End If

            Call .AddHiddenColumn("hSizeDes", lclsTabGen.nFieldLengthDesc)
            Call .AddHiddenColumn("hSizeShortDes",lclsTabGen.nFieldLengthDesc)

            Call .AddTextColumn(40610, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", lclsTabGen.nFieldLengthShortDesc, vbNullString, False, GetLocalResourceObject("tctShort_desColumnToolTip"))
            Call .AddPossiblesColumn(40607, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, vbNullString, False, , , , , , , GetLocalResourceObject("cbeStatregtColumnToolTip"))

            If mstrTableNew <> vbNullString Then
                If instr(mstrTableNew,"TABLE") > 0 then
                    Call .AddPossiblesColumn(40607, GetLocalResourceObject("cbeTableNewColumnCaption"), "cbeTableNew", mstrTableNew, eFunctions.Values.eValuesType.clngComboType, vbNullString, False, , , , , , , GetLocalResourceObject("cbeTableNewColumnToolTip"))
                end if
            End If

            mobjGrid.Columns("cbeStatregt").TypeList = 2
            mobjGrid.Columns("cbeStatregt").List = "2"
        End With
        lclsTabGen = Nothing

        With mobjGrid
            .Columns("tcnCodigint").Disabled = Not (Request.QueryString.Item("Action") = "Add")

            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
                .Columns("Sel").GridVisible = False
                .ActionQuery = True
            End If

            .Codispl = Request.QueryString.Item("sCodispl")
            .Codisp = "MA1000_K"
            .sCodisplPage = "MA1000"
            .Columns("tctDescript").EditRecord = True
            .AddButton = True
            .DeleteButton = True
            .Height = 300
            .Width = 750

            .sDelRecordParam = "nCodigint=' + marrArray[lintIndex].tcnCodigint + '"
            '		.sDelRecordParam = "nCodigint=' + marrArray[lintIndex].tcnCodigint + '" & '						  "&sTableNew=' + marrArray[lintIndex].cbeTableNew + '"

            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '% insPreMA1000: Muestra la grilla con datos
    '-----------------------------------------------------------------------------------------
    Private Sub insPreMA1000()
        '-----------------------------------------------------------------------------------------

        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>" & vbCrLf)
        Response.Write("//% insPreZone: Define ubicacion de documento" & vbCrLf)
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
        Response.Write("</" & "SCRIPT>")


        Dim lintCount As Short
        Dim lobjObject As Object
        Dim lcolTabGens As eGeneralForm.TabGens

        lcolTabGens = New eGeneralForm.TabGens

        If lcolTabGens.Find(mstrTable, True, Session("companyId")) And mlngNumErr = 0 Then

            lintCount = 0

            For Each lobjObject In lcolTabGens
                With lobjObject
                    mobjGrid.Columns("tcnCodigint").DefValue = .Key
                    mobjGrid.Columns("tctDescript").DefValue = .sDescript
                    mobjGrid.Columns("tctShort_des").DefValue = .sShort_des
                    mobjGrid.Columns("cbeStatregt").DefValue = .sStatregt
                    If mstrTableNew <> vbNullString Then
                        If InStr(mstrTableNew, "TABLE") > 0 Then
                            mobjGrid.Columns("cbeTableNew").DefValue = .sValorAdic
                        End If
                    End If
                    mobjGrid.Columns("Sel").OnClick = "InsChangeSel(this,""" & mstrTable & """,""" & .Key & """,""" & lintCount & """,""" & Request.QueryString.Item("sCodispl") & """)"
                    Response.Write(mobjGrid.DoRow())
                End With
                lintCount = lintCount + 1
                If lintCount = 500 Then
                    Exit For
                End If
            Next lobjObject
        End If
        Response.Write(mobjGrid.closeTable())
        Response.Write(mobjValues.BeginPageButton)

        lcolTabGens = Nothing
        lobjObject = Nothing
    End Sub

    '% insPreMA1000Upd: Muestra ventana para actualizar registros
    '-----------------------------------------------------------------------------------------
    Private Sub insPreMA1000Upd()
        '-----------------------------------------------------------------------------------------
        Dim lclsTabGen As eGeneralForm.TabGen

        If Request.QueryString.Item("Action") = "Del" Then

            lclsTabGen = New eGeneralForm.TabGen

            If lclsTabGen.insPostMA1000(Request.QueryString.Item("sCodispl"), "Delete", Request.QueryString.Item("nCodigint"), " ", " ", " ", mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sTableNew"), Session("companyId"), mobjValues.StringToType(Session("nInsur_Area"), eFunctions.Values.eTypeData.etdInteger)) Then

                Response.Write(mobjValues.ConfirmDelete())
                Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantTables.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
            End If
            lclsTabGen = Nothing
        Else
            Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValMantTables.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
        End If
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MA1000"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
    <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\maintenance\manttables\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
<%End If%>
<%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>




<%
    With Response
        .Write(mobjValues.StyleSheet())
        If Request.QueryString.Item("Type") <> "PopUp" Then
            mobjMenu = New eFunctions.Menues
            .Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
            .Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MA1000_k.aspx", 1, ""))
            mobjMenu = Nothing
        End If
    End With
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:31 $|$$Author: Nvaplat61 $"

//% insCancel: Eejcuta accion de boton cancelar
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%insStateZone: Activa controles
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//%InsChangeSel: Permite verificar si se seleccionó el registro
//------------------------------------------------------------------------------------------
function InsChangeSel(Field, sTable, nCodigint, nCount, sCodispl){
//------------------------------------------------------------------------------------------

	if (Field.checked && 
	   (sTable == "table16" || 
	    sTable == "table417" ||
	    sTable == "table215")) {
		insDefValues("ValTables", "sTable=" + sTable + "&nCodigint=" + nCodigint + "&nCount=" + nCount + "&sCodispl="+ sCodispl);	
		}
	else
	   {
	    document.cmdDelete.disabled = true;
	    insDefValues("ValTablesXXX", "sTable=" + sTable + "&nCodigint=" + nCodigint + "&nCount=" + nCount + "&sCodispl="+ sCodispl);			
	    }
   }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MA1000_K" ACTION="valMantTables.aspx?mode=1">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMA1000()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMA1000Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>





