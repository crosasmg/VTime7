<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import Namespace="Segured" %>

<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid
    Dim mlngNotenum As String
    Dim mstrUserName As String
    Dim mintRectype As String
    Dim mlngIndexNotenum As Object


    '% insPreSI021: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSIL7483()
        '--------------------------------------------------------------------------------------------
        Call insDefineHeader()
        Call insReaClaimByRut()
    End Sub

    '% insDefineHeader : Configura las columnas del grid.
    '---------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '---------------------------------------------------------------------------------------------   
        mobjGrid.sCodisplPage = "SIL7483"

        '+ Si la acción es consulta no se establece la propiedad ActionQuery sobre el objeto del
        '+ grid con la variable de sesión bquery, ya que es necesario que aparezcan los links
        '+ sobre las notas para lograr acceder a su descripción.
        If Not Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
            mobjGrid.ActionQuery = Session("bQuery")
        End If

        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            .AddTextColumn(40560, "Producto", "tctProduct", 100, "",  , "")
            .AddNumericColumn(19653, "Número de póliza", "tcnPolicy", 10, ,  , "",  ,  ,  ,  ,  , True)
            .AddNumericColumn(19653, "Número de siniestro", "tcnClaim", 10, ,  , "",  ,  ,  ,  ,  , True)
            .AddDateColumn(40562, "Fecha de declaración", "tcdDecladat",,  , "",  ,  ,  , True)
            .AddDateColumn(40563, "Fecha de pago", "tcdPaid",   ,  , "")
            .AddTextColumn(40561, "Status del siniestro", "tctClaimStatus", 50, "",  , "",  ,  ,  , True)
            .AddTextColumn(40561, "Resultado", "tctResult", 200, "",  , "",  ,  ,  , True)
        End With

        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = Request.QueryString("sCodispl")
            .Codisp = "SIL009"
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = False
            .bCheckVisible = False

            .nMainAction = Request.QueryString("nMainAction")
            '+ Tamaño de la ventana popup
            .Width = 650
            .Height = 550
            .Top = 5

        End With
    End Sub
    '% insreaNotes: Lee las notas asociadas a un ente
    '----------------------------------------------------------------------------
    Private Sub insReaClaimByRut()
        '--------------------------------------------------------------------------------------------
        Dim oResults As Segured.ServicioCF.respuestaConsultaPorRut
        mobjValues.ActionQuery = True

        With mobjGrid
            .AddButton = False
            .DeleteButton = False
            .ActionQuery = True
        End With

        oResults = Segured.CFSeguRedBridge.GetClaimByRut(Integer.Parse(Request.QueryString("sClient")), Request.QueryString("sDigit"))

        If oResults Is Nothing Then
            oResults = New Segured.ServicioCF.respuestaConsultaPorRut
            oResults.resultado = New Segured.ServicioCF.clsResultado
            oResults.resultado.codigo_resultado = -13126342
            oResults.resultado.descripcion_resultado = "El WS no retornó resultados"
        End If


        With mobjGrid
            .Columns("tctProduct").DefValue = oResults.producto
            .Columns("tcnPolicy").DefValue = oResults.numero_poliza
            .Columns("tcnClaim").DefValue = oResults.numero_siniestro
            .Columns("tcdDecladat").DefValue = oResults.fecha_denuncio
            .Columns("tcdPaid").DefValue = oResults.fecha_liquidacion
            .Columns("tctClaimStatus").DefValue = oResults.estado_de_siniestro
            .Columns("tctResult").DefValue = oResults.resultado.descripcion_resultado
            Response.Write(.DoRow)
        End With
        Response.Write(mobjGrid.closeTable)

    End Sub

</script>
<%Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjGrid = New eFunctions.Grid
    mobjMenu = New eFunctions.Menues

    mobjValues.sCodisplPage = "SIL7483"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/Constantes.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/General.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 10/12/03 17:28 $|$$Author: $"

</SCRIPT>
<%
    With Response
        .Write(mobjMenu.setZone(2, "SIL7483", "SIL7483.aspx"))
        'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
        .Write(mobjValues.StyleSheet())
        .Write(mobjValues.WindowsTitle("SIL7483"))
    End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SIL7483" ACTION="ValClaimrep.aspx?x=1&nTransacio=SIL7483&sOriginalForm=<%=Session("sOriginalForm")%>">
<BR>
<%=mobjValues.ShowWindowsName("SIL7483", Request.QueryString("sWindowDescript"))%>
<BR><BR>
<%
    Call insPreSIL7483()
    mobjValues = Nothing
    mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>

   






