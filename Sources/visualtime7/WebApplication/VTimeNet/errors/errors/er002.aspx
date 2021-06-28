<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eErrors" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mobjError As eErrors.ErrorTyp

'- Objeto para el manejo del grid    
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.sCodisplPage = "er002"
	
	'+ Se definen las columnas del grid    
        With mobjGrid.Columns
            
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddAnimatedColumn(6744, vbNullString, "sLink", "/VTimeNet/images/LINK.gif", "Actualizacion de Errores")
            End If
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddAnimatedColumn(6745, vbNullString, "sLink1", "/VTimeNet/images/clfolder.png", "Actualizacion de Estado de los Errores")
            End If
            Call .AddTextColumn(6747, "Origen del error", "sSrc_Descript", 40, CStr(eRemoteDB.Constants.strNull), , vbNullString)
            Call .AddNumericColumn(6746, "Número del error", "nErrorNum", 6, CStr(0), , vbNullString)
            Call .AddTextColumn(6747, "Descripción breve del error", "sDescript", 40, CStr(eRemoteDB.Constants.strNull), , vbNullString)
        End With
	
			                                                              '+ Se definen las propiedades generales del grid
        With mobjGrid
            
            .Codispl = "ER002"
            .Codisp = "ER002"
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = False
        End With
	
    End Sub
    

    '% insPreER002: Se carga el Grid con la Información
    
    '--------------------------------------------------------------------------------------------
    
    Private Sub insPreER002()
        
        '--------------------------------------------------------------------------------------------
        
        Dim lcolErroquers As eErrors.Errors
        Dim lclsErroquer As eErrors.ErrorTyp
        Dim lIndex As Integer
        Dim nType_mov As Object
        Dim nEndBalance As Object
        Dim nIniBalance As Object
	
        lcolErroquers = New eErrors.Errors
        lclsErroquer = New eErrors.ErrorTyp
	
        If lcolErroquers.Find(Session("sCodispl"), Session("sStaterr"), Session("nSrcerr")) Then
		
            For lIndex = 1 To lcolErroquers.Count
                lclsErroquer = lcolErroquers.Item(lIndex)
                With lclsErroquer
                    Session("Query") = True
                    Session("sCallForm") = "ER002"
                    mobjGrid.Columns("sLink").HRefScript = "ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Errors&sProject=Errors&sCodispl=ER001_K&nErrorNum=" & (.nErrorNum) & "&nMainAction=302&sLinkSpecial=1', 'Errors', 750, 500, 'no', 'no', 20, 20);"
                    mobjGrid.Columns("sLink1").HRefScript = "ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Errors&sProject=Errors&sCodispl=ER003_K&nErrorNum=" & (.nErrorNum) & "&nMainAction=302&sLinkSpecial=1', 'Errors', 750, 500, 'no', 'no', 20, 20);"
                    mobjGrid.Columns("sSrc_Descript").DefValue = .sSrc_Descript
                    mobjGrid.Columns("nErrorNum").DefValue = CStr(.nErrorNum)
                    mobjGrid.Columns("sDescript").DefValue = .sDescript
                    Response.Write(mobjGrid.DoRow())
                End With
            Next
        End If
	
        Response.Write(mobjGrid.closeTable)
        lcolErroquers = Nothing
        lclsErroquer = Nothing
    End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjError = New eErrors.ErrorTyp
mobjGrid = New eFunctions.Grid

mobjValues.sCodisplPage = "er002"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">

	<%=mobjValues.StyleSheet()%>
    <%=mobjMenu.setZone(2, "ER002", "ER002.aspx")%>
    
<SCRIPT>
    function insShowHeader() {
        var lblnContinue = true
        if (typeof (top.fraHeader.document) != 'undefined') {
            if (typeof (top.fraHeader.document.forms[0]) != 'undefined') {
                if (typeof (top.fraHeader.document.forms[0].tctCodisp) != 'undefined') {
                    top.fraHeader.document.forms[0].tctCodisp.value = '<%=Session("sCodispl")%>'
                    top.fraHeader.document.forms[0].cbeStaterr.value = '<%=Session("sStaterr")%>'
                    lblnContinue = false
                }
            }
        }
        if (lblnContinue)
            setTimeout("insShowHeader()", 50);
    }
    setTimeout("insShowHeader()", 50)
</SCRIPT>    
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmErroUpd" ACTION="valerrors.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%Response.Write(mobjValues.ShowWindowsName("ER002"))%>
</FORM>
</BODY>
</HTML>

<%
Call insDefineHeader()
Call insPreER002()

mobjMenu = Nothing
mobjError = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
<%
Response.Write("<SCRIPT>top.fraHeader.$('#tctCodisp').change();</SCRIPT>")

%>












