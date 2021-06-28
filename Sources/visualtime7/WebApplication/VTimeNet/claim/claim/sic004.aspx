<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.48
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    '- Objeto para el manejo de las funciones generales de carga de valores.

    Dim mobjValues As eFunctions.Values

    '- Objeto para el manejo de las zonas de la página    

    Dim mobjMenu As eFunctions.Menues

    '- Objeto para el manejo del numero de movimiento.

    Dim mintOperType As Integer

    '- Objeto para el manejo del Grid de la Pagina    

    Dim mobjGrid As eFunctions.Grid



    '% insDefineHeader:Este procedimiento se encarga de definir las columnas del grid y de habilitar
    '% o inhabilitar los botones de añadir y eliminar.
    '-----------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '-----------------------------------------------------------------------------------------
	
        mobjGrid = New eFunctions.Grid
        '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
        mobjGrid.sSessionID = Session.SessionID
        mobjGrid.nUsercode = Session("nUsercode")
        '~End Body Block VisualTimer Utility
	
        mobjGrid.sCodisplPage = "sic004"
        Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	
        '+ Se definen las columnas del Grid.
	
        With mobjGrid
            .Codispl = Request.QueryString("sCodispl")
            .Codisp = "SIC004"
        End With
	
        With mobjGrid.Columns
            Call .AddTextColumn(0, "Cobertura", "tctGencov", 30, vbNullString, , "Cobertura", , , , True)
            Call .AddTextColumn(0, "Concepto", "tctConcept", 30, vbNullString, , "Concepto", , , , True)
            Call .AddNumericColumn(0, "Monto sin impuesto", "tcnAmount", 18, CStr(0), , "Monto sin impuesto", True, 6, , , , True)
            Call .AddNumericColumn(0, "Impuesto", "tcnVat_amount", 18, CStr(0), , "Impuesto", True, 6, , , , True)
            Call .AddNumericColumn(0, "Monto total", "tcnAmountTot", 18, CStr(0), , "Monto total", True, 6, , , , True)
        End With
	
        '+ Se definen las propiedades generales del grid.
	
        With mobjGrid
            .Codispl = "SIC004"
            .DeleteButton = False
            .AddButton = False
            .Columns("Sel").GridVisible = False
            .ActionQuery = True
        End With
    End Sub

    '**% insPreSIC004: This function allows to make the reading of the main table of the transaction.  
    '% insPreSIC004: Esta función permite realizar la lectura de la tabla principal de la transacción.
    '-----------------------------------------------------------------------------------------
    Private Sub insPreSIC004()
        '-----------------------------------------------------------------------------------------
        Dim lintCount As Short
        Dim lcolCl_m_covers As eClaim.Cl_m_covers
        Dim lclsCl_m_cover As eClaim.Cl_m_cover
        Dim ldblAmountM As Double
        Dim ldblTaxes As Double
        Dim ldblAmountTot As Double
        Dim lstrCurrency As String
        Dim ldblAux As Double
	
        lcolCl_m_covers = New eClaim.Cl_m_covers
        If lcolCl_m_covers.Find(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nDeman_type")), eFunctions.Values.eTypeData.etdDouble), mintOperType, True) Then
		
            lintCount = 0
            ldblAmountM = 0
            ldblTaxes = 0
            ldblAmountTot = 0
            lstrCurrency = vbNullString
            ldblAux = 0
		
            For Each lclsCl_m_cover In lcolCl_m_covers
                With lclsCl_m_cover
                    mobjGrid.Columns("tctGencov").DefValue = .sGencov
                    mobjGrid.Columns("tctConcept").DefValue = .sConcept
                    lstrCurrency = .sCurrency
                    mobjGrid.Columns("tcnAmount").DefValue = CStr(.nAmount)
                    ldblAux = .nAmount
                    If .nVat_amount = eRemoteDB.Constants.intNull Then
                        mobjGrid.Columns("tcnVat_amount").DefValue = CStr(0)
                    Else
                        mobjGrid.Columns("tcnVat_amount").DefValue = CStr(.nVat_amount)
                    End If
				
                    'Total (Monto - impuesto)
                    If CDbl(mobjGrid.Columns("tcnAmount").DefValue) < 0 Then
                        mobjGrid.Columns("tcnAmountTot").DefValue = CStr(ldblAux - CDbl(mobjGrid.Columns("tcnVat_amount").DefValue) + 0)
                    Else
                        mobjGrid.Columns("tcnAmountTot").DefValue = CStr(ldblAux + CDbl(mobjGrid.Columns("tcnVat_amount").DefValue) + 0)
                    End If
				
                    'Totales Acumulados
                    ldblAmountTot = ldblAmountTot + CDbl(mobjGrid.Columns("tcnAmountTot").DefValue)
                    ldblTaxes = ldblTaxes + CDbl(mobjGrid.Columns("tcnVat_amount").DefValue)
                    ldblAmountM = ldblAmountM + CDbl(mobjGrid.Columns("tcnAmount").DefValue)
				
                    Response.Write(mobjGrid.DoRow())
                End With
			
                lintCount = lintCount + 1
			
                If lintCount = 200 Then
                    Exit For
                End If
            Next lclsCl_m_cover
		
            Response.Write(mobjValues.HiddenControl("hddnCurrency", lstrCurrency))
            Response.Write(mobjValues.HiddenControl("hddnAmount", CStr(ldblAmountM)))
            Response.Write(mobjValues.HiddenControl("hddnTaxes", CStr(ldblTaxes)))
            Response.Write(mobjValues.HiddenControl("hddnAmountTot", CStr(ldblAmountTot)))
		
            Response.Write("" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("	<SCRIPT>" & vbCrLf)
            Response.Write("  		document.getElementById(""lblCurrency"").innerHTML = document.forms[0].hddnCurrency.value" & vbCrLf)
            Response.Write(" 		document.getElementById(""lblAmount"").innerHTML = document.forms[0].hddnAmount.value" & vbCrLf)
            Response.Write("		document.getElementById(""lblTaxes"").innerHTML = document.forms[0].hddnTaxes.value" & vbCrLf)
            Response.Write("		document.getElementById(""lblAmountTot"").innerHTML= document.forms[0].hddnAmountTot.value" & vbCrLf)
            Response.Write("	</" & "SCRIPT>" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("	")

		
        End If
        Response.Write(mobjGrid.closeTable())
        'UPGRADE_NOTE: Object lcolCl_m_covers may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolCl_m_covers = Nothing
	
        '% Se llenan los valores cuando el tipo de movimiento que se esta consultando es "Pago de honorarios"
        lcolCl_m_covers = New eClaim.Cl_m_covers
	
        If mintOperType = 5 Then
            If lcolCl_m_covers.FindProvider(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(CStr(Session("nCase_num")), eFunctions.Values.eTypeData.etdDouble), mintOperType) Then
                For Each lclsCl_m_cover In lcolCl_m_covers
                    With lclsCl_m_cover
                        Response.Write(mobjValues.HiddenControl("hddTypeProf", .sDescript))
                        Response.Write(mobjValues.HiddenControl("hddRutProf", .sClient))
                        Response.Write(mobjValues.HiddenControl("hddOrderPay", CStr(.nServ_Order)))
                    End With
                Next lclsCl_m_cover
			
                Response.Write("" & vbCrLf)
                Response.Write("		<SCRIPT>" & vbCrLf)
                Response.Write("		    document.getElementById(""lblTypeProf"").innerHTML = document.forms[0].hddTypeProf.value" & vbCrLf)
                Response.Write("		    document.getElementById(""lblRutProf"").innerHTML = document.forms[0].hddRutProf.value" & vbCrLf)
                Response.Write("		    document.getElementById(""lblOrderPay"").innerHTML = document.forms[0].hddOrderPay.value" & vbCrLf)
                Response.Write("		</" & "SCRIPT>" & vbCrLf)
                Response.Write("		")

			
            End If
        End If
        'UPGRADE_NOTE: Object lcolCl_m_covers may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lcolCl_m_covers = Nothing
    End Sub

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("sic004")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "sic004"


    If mobjValues.StringToType(CStr(Session("nOper_type")), eFunctions.Values.eTypeData.etdDouble) = 5 Then
        '% Movimiento que pertenece al Pago de Honorarios
        mintOperType = 5
    Else
        mintOperType = mobjValues.StringToType(CStr(Session("nOper_type")), eFunctions.Values.eTypeData.etdDouble)
    End If
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%
        With Response
            .Write(mobjValues.StyleSheet())
	
            If Request.QueryString("Type") <> "PopUp" Then
                mobjMenu = New eFunctions.Menues
                '^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.48
                mobjMenu.sSessionID = Session.SessionID
                mobjMenu.nUsercode = Session("nUsercode")
                '~End Body Block VisualTimer Utility
                .Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString("nMainAction") & "</SCRIPT>")
                .Write(mobjMenu.setZone(2, "SIC004", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
            End If
	
            If Request.QueryString("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
                mobjValues.ActionQuery = True
            End If
        End With
    %>
    <script>

        var mstrAction = "";

        //% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
        //------------------------------------------------------------------------------------------
        function insCancel()
        //------------------------------------------------------------------------------------------
        {
            return true;
        }

    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="SIC004" action="ValClaim.ASPX?sZone=2&nMainAction=<%=Request.QueryString("nMainAction")%>">
    <%
        Response.Write(mobjValues.ShowWindowsName("SIC004", Request.QueryString("sWindowDescript")))
    %>
    <table width="60%">
        <%If mintOperType = 5 Then%>
        <tr>
            <td>
                <label id="0">
                    Tipo de Profesional
                </label>
            </td>
            <td>
                <div id="lblTypeProf" class="Field">
                    0</div>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0">
                    Rut del Profesional
                </label>
            </td>
            <td>
                <div id="lblRutProf" class="Field">
                    0</div>
            </td>
            <td>
                <label id="0">
                    Monto</label>
            </td>
            <td>
                <div id="lblAmount" class="Field">
                    0</div>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0">
                    Orden de pago
                </label>
            </td>
            <td>
                <div id="lblOrderPay" class="Field">
                    0</div>
            </td>
            <td>
                <label id="0">
                    Impuesto</label>
            </td>
            <td>
                <div id="lblTaxes" class="Field">
                    0</div>
            </td>
        </tr>
        <tr>
            <td>
                <label id="0">
                    Moneda
                </label>
            </td>
            <td>
                <div id="lblCurrency" class="Field">
                    0</div>
            </td>
            <td>
                <label id="0">
                    Total</label>
            </td>
            <td>
                <div id="lblAmountTot" class="Field">
                    0</div>
            </td>
        </tr>
        <%Else%>
        <tr>
            <td>
                <label id="0">
                    Moneda
                </label>
            </td>
            <td>
                <div id="lblCurrency" class="Field">
                    0</div>
            </td>
            <td>
                <label id="0">
                    Monto</label>
            </td>
            <td>
                <div id="lblAmount" class="Field">
                    0</div>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <label id="0">
                    Impuesto</label>
            </td>
            <td>
                <div id="lblTaxes" class="Field">
                    0</div>
            </td>
            <tr>
            </tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <label id="0">
                    Total</label>
            </td>
            <td>
                <div id="lblAmountTot" class="Field">
                    0</div>
            </td>
        </tr>
        <%End If%>
        <br>
    </table>
    <br>
    <%
        Call insDefineHeader()
        Call insPreSIC004()

        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjGrid = Nothing
    %>
    </form>
</body>
</html>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.48
    Call mobjNetFrameWork.FinishPage("sic004")
    'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
