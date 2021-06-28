<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo del grid de la página
    Dim mobjGrid As eFunctions.Grid

    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues


    '% insDefineHeader: se definen las propiedades del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility

        mobjGrid.sCodisplPage = "si813"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
        If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
        End If

        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            .AddClientColumn(0, "Asegurado", "valClient", "",  , "Asegurado a la cobertura",  , True, "lblCliename")
            .AddTextColumn(0, "Cobertura", "tctCover", 35, "",  , "Cobertura asociada al asegurado",  ,  ,  , True)
            .AddNumericColumn(0, "Capital", "tcnCapital", 18, "",  , "Capital de la cobertura/asegurado", True, 6,  ,  ,  , True)
            .AddPossiblesColumn(0, "Estado", "cbeActioncov", "table5598", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  , "EnabledCapital(this);",  ,  , "Estado de la cobertura")
            .AddHiddenColumn("hddSel", "")
            .AddHiddenColumn("hddnCover", "")
            .AddHiddenColumn("hddnGroup", "")
            .AddHiddenColumn("hddnModulec", "")
            .AddHiddenColumn("hddnRole", "")
            .AddHiddenColumn("hddsDepend", "")
            .AddHiddenColumn("hddnCapital", "")

        End With

        '+ Se definen las propiedades generales del grid

        With mobjGrid
            .Codispl = Request.QueryString("sCodispl")
            .ActionQuery = mobjValues.ActionQuery
            .Columns("valClient").EditRecord = True
            .Height = 290
            .Width = 400
            .nMainAction = Request.QueryString("nMainAction")
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = Not .ActionQuery
            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
        End With
    End Sub

    '% insPreSI813: se realiza el manejo del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI813()
        '--------------------------------------------------------------------------------------------
        Dim lclsCover As ePolicy.Cover
        Dim lcolCover As ePolicy.Covers
        Dim llngIndex As Short

        lcolCover = New ePolicy.Covers
        llngIndex = 0
        If lcolCover.FindSI813("2", CInt(Session("nBranch")), CInt(Session("nProduct")), CDbl(Session("nPolicy")), CDbl(Session("nCertif")), CDate(Session("dOccurdate_l")), Request.QueryString("sInd"), CInt(Session("nUsercode")), CInt(Session("SessionId").gethashcode())) Then
            For Each lclsCover In lcolCover
                With mobjGrid
                    .Columns("Sel").Checked = CShort(lclsCover.sDefaulti)
                    .Columns("Sel").OnClick = "InsSel(this, " & llngIndex & "," & lclsCover.nGroup & "," & lclsCover.nModulec & "," & lclsCover.nRole & "," & lclsCover.nCover & ",""" & lclsCover.sClient & """,""" & lclsCover.nCapital & """)"
                    .Columns("valClient").DefValue = lclsCover.sClient
                    .Columns("tctCover").DefValue = lclsCover.sDescript
                    .Columns("tcnCapital").DefValue = CStr(lclsCover.nCapital)
                    .Columns("cbeActioncov").DefValue = CStr(lclsCover.nActionCov)

                    ' Si se realizó una exepción de pago				  
                    If lclsCover.sFree_premi = "1" Then
                        .Columns("cbeActioncov").DefValue = "2"
                    End If

                    .Columns("hddnGroup").DefValue = CStr(lclsCover.nGroup)
                    .Columns("hddnModulec").DefValue = CStr(lclsCover.nModulec)
                    .Columns("hddnCover").DefValue = CStr(lclsCover.nCover)
                    .Columns("hddnRole").DefValue = CStr(lclsCover.nRole)
                    .Columns("hddsDepend").DefValue = lclsCover.sDepend
                    Response.Write(.DoRow)
                    llngIndex = llngIndex + 1
                End With
            Next lclsCover
        End If
        'UPGRADE_NOTE: Object lcolCover may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolCover = Nothing
        Response.Write(mobjGrid.closeTable())

    End Sub

    '% insPreSI813Upd: Se realiza el manejo de la ventana PopUp asociada al grid
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI813Upd()
        '--------------------------------------------------------------------------------------------
        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valClaimSeq.aspx", "SI813", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
        End With
    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si813")

'- Objeto para el manejo particular de los datos de la página

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si813"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

    <%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "SI813", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//%InsSel: Funcion que se ejecuta cuando se selecciona una fila
//-------------------------------------------------------------------------------------------
function InsSel(Field, nIndex, nGroup, nModulec, nRole, nCover, sClient, nCapital){
//-------------------------------------------------------------------------------------------
    var lstrQueryString
	if (Field.checked){
	    EditRecord(nIndex, nMainAction, 'Update', '');
	    Field.checked = false;
	}
	else{
        lstrQueryString = 'nGroup=' + nGroup;
        lstrQueryString = lstrQueryString + '&nModulec=' + nModulec;
        lstrQueryString = lstrQueryString + '&nCover=' + nCover;
        lstrQueryString = lstrQueryString + '&nRole=' + nRole;
        lstrQueryString = lstrQueryString + '&sClient=' + sClient;
        lstrQueryString = lstrQueryString + '&nCapital=' + nCapital;
        insDefValues('InsUpdSI813', lstrQueryString);
    }

    if (Field.checked)
        self.document.forms[0].hddSel.value = "1";
    else
        self.document.forms[0].hddSel.value = "0";

}

//% EnabledCapital: Deshabilita el campo capital si el estado es "Excepción de pago"
//-----------------------------------------------------------------------------------------
function EnabledCapital(Field){
//-----------------------------------------------------------------------------------------	
	if(Field.value==2)
		self.document.forms[0].tcnCapital.disabled=false;
	else
		self.document.forms[0].tcnCapital.disabled=true;

}
 //+Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 31/05/04 19:59 $"

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="SI813" ACTION="valClaimSeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript")))
Call insDefineHeader()

If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI813Upd()
Else
	Call insPreSI813()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
Call mobjNetFrameWork.FinishPage("si813")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




