<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnYearColumnCaption"), "tcnYear", 4, "", , GetLocalResourceObject("tcnYearColumnCaption"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnStartColumnCaption"), "tcnStart", 10, "", , GetLocalResourceObject("tcnStartColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnEndColumnCaption"), "tcnEnd", 10, "", , GetLocalResourceObject("tcnEndColumnToolTip"))
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatRegtColumnCaption"), "cbeStatRegt", "Table26", eFunctions.Values.eValuesType.clngComboType, , , , , , , , , GetLocalResourceObject("cbeStatRegtColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codisp = "CA980_K"
		.Codispl = "CA980"
		.sCodisplPage = "CA980_K"
		.AddButton = True
		.DeleteButton = True
		.Top = 70
		.Width = 400
		.Height = 250
        .WidthDelete = 400
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
		End If
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Or Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
		.Columns("tcnStart").EditRecord = True
		.sDelRecordParam = "nYear='+ marrArray[lintIndex].tcnYear + '" + "&nStart='+ marrArray[lintIndex].tcnStart + '" + "&nEnd='+ marrArray[lintIndex].tcnEnd + '"
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
        End If
	End With
End Sub

'% insPreCA980: Carga los datos en el grid de la forma "Folder" 
'---------------------------------------------------------------
Private Sub insPreCA980()
	'---------------------------------------------------------------
        Dim lcolFolios_comps As ePolicy.Folios_comps
        Dim lclsFolios_comp As ePolicy.Folios_comp
	
        lcolFolios_comps = New ePolicy.Folios_comps
	
        If lcolFolios_comps.Find Then
            For Each lclsFolios_comp In lcolFolios_comps
                With mobjGrid
                    .Columns("tcnYear").DefValue = lclsFolios_comp.nYear
                    .Columns("tcnStart").DefValue = lclsFolios_comp.nStart
                    .Columns("tcnEnd").DefValue = lclsFolios_comp.nEnd
                    .Columns("cbeStatRegt").DefValue = lclsFolios_comp.sStatregt
                    Response.Write(.DoRow)
                End With
            Next lclsFolios_comp

        End If
        
        '+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
        Response.Write(mobjGrid.CloseTable())
        Response.Write(mobjValues.BeginPageButton)
	
        lclsFolios_comp = Nothing
        lcolFolios_comps = Nothing
    End Sub

    '% insPreCA980Upd: Gestiona lo relacionado a la actualización de un registro del Grid
    '------------------------------------------------------------------------------------
    Private Sub insPreCA980Upd()
        '------------------------------------------------------------------------------------
        Dim lclsFolios_comp As ePolicy.Folios_comp
        lclsFolios_comp = New ePolicy.Folios_comp
	
        With Request
            If .QueryString.Item("Action") = "Update" Then
                mobjGrid.Columns("tcnYear").Disabled = True
                mobjGrid.Columns("tcnStart").Disabled = True
            End If
		
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
                lclsFolios_comp.nYear = mobjValues.StringToType(.QueryString.Item("nYear"), eFunctions.Values.eTypeData.etdDouble)
                lclsFolios_comp.nStart = mobjValues.StringToType(.QueryString.Item("nStart"), eFunctions.Values.eTypeData.etdDouble)
                lclsFolios_comp.Delete()
            End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valpolicytra.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), , CShort(.QueryString.Item("Index"))))
		
        End With
	
        lclsFolios_comp = Nothing
    End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "CA980_k"
%>

<HTML>
<HEAD>
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 2 $|$$Date: 15/10/03 16:00 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%If Request.QueryString.Item("Type") <> "PopUp" Then%>		
        <SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%End If%>
	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>



    <%  Response.Write(mobjValues.StyleSheet())
        
        If Request.QueryString.Item("Type") <> "PopUp" Then
            mobjMenu = New eFunctions.Menues
            With Response
                .Write(mobjMenu.MakeMenu("CA980", "CA980_K.aspx", 1, ""))
                .Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
            End With
            mobjMenu = Nothing
        End If
%>

<SCRIPT>
    //% insStateZone: 
    //-----------------------
    function insStateZone() { }
    //-----------------------

    //% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
    //% que proviene del menú principal
    //------------------------------------------------------------------------------
    function insPreZone(llngAction) {
        //------------------------------------------------------------------------------
        switch (llngAction) {
            case 301:
            case 302:
            case 401:
                document.location.href = document.location.href.replace(/&nMainAction.*/, '') + '&nMainAction=' + llngAction
                break;
        }
    }
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //------------------------------------------------------------------------------------------
    function insFinish() {
        //------------------------------------------------------------------------------------------
        return true;
    }

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	<FORM METHOD="post" ID="FORM" NAME="CA980_K" ACTION="valpolicytra.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA980()
Else
	Call insPreCA980Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>     
	</FORM>
</BODY>
</HTML>





