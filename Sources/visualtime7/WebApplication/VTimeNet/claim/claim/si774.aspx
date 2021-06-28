<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClaim" %>
<%@ Import Namespace="eAgent" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjMenu As eFunctions.Menues
    Dim mobjGrid As eFunctions.Grid


    '% insDefineHeader: Se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------

        mobjGrid.sCodisplPage = "si774"

        '+ Se definen las columnas del grid
        With mobjGrid.Columns
            If CStr(Session("sOriginalForm")) <> vbNullString And CStr(Session("sOriginalForm")) = "SI011" Then
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuantityColumnCaption"), "tcnQuantity", 5, "", , , , , , , , False)
                Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSparePartsColumnCaption"), "cbeSpareParts", "Table5579", eFunctions.Values.eValuesType.clngComboType, CStr(0), False, , , , , False, , GetLocalResourceObject("cbeSparePartsColumnToolTip"))
                Call .AddCheckColumn(0, GetLocalResourceObject("chkSpareColumnCaption"), "chkSpare", "", , , , True)
                Call .AddHiddenColumn("tcnQuantity_AUX", CStr(0))
                Call .AddHiddenColumn("cbeSpareParts_AUX", CStr(0))
                Call .AddHiddenColumn("tcnChecked", CStr(2))
                Call .AddHiddenColumn("tcnID", CStr(0))
                Call .AddHiddenColumn("sParam", "")
            Else
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnQuantityColumnCaption"), "tcnQuantity", 5, "", , , , , , , "AddAmount();", False)
                If Request.QueryString("Type") = "PopUp" Then
                    Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSparePartsColumnCaption"), "cbeSpareParts", "TabTable5579", eFunctions.Values.eValuesType.clngComboType, CStr(0), False, , , , , False, , GetLocalResourceObject("cbeSparePartsColumnToolTip"))
                Else
                    Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSparePartsColumnCaption"), "cbeSpareParts", "Table5579", eFunctions.Values.eValuesType.clngComboType, CStr(0), False, , , , , False, , GetLocalResourceObject("cbeSparePartsColumnToolTip"))
                End If
                Call .AddCheckColumn(0, GetLocalResourceObject("chkSpareColumnCaption"), "chkSpare", "", , , , True)
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnUnitValueColumnCaption"), "tcnUnitValue", 18, CStr(0), , , True, 6, , , "AddAmount();", False)
                Call .AddNumericColumn(0, GetLocalResourceObject("tcnTotalValueColumnCaption"), "tcnTotalValue", 18, CStr(0), , , True, 6, , , , True)
                Call .AddHiddenColumn("tcnQuantity_AUX", CStr(0))
                Call .AddHiddenColumn("cbeSpareParts_AUX", CStr(0))
                Call .AddHiddenColumn("tcnUnitValue_AUX", CStr(0))
                Call .AddHiddenColumn("tcnChecked", CStr(2))
                Call .AddHiddenColumn("tcnID", CStr(0))
                Call .AddHiddenColumn("chkSpare_AUX", CStr(0))
                Call .AddHiddenColumn("sParam", "")
            End If

        End With
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Top = 250
            .Left = 100
            .Width = 380
            .Height = 260
            .Codispl = "SI774"
            .Columns("cbeSpareParts").EditRecord = True
            .sDelRecordParam = "nId='         + marrArray[lintIndex].tcnID + '" & "&nUnitValue=' + marrArray[lintIndex].tcnUnitValue + '" & "&nQuantity='  + marrArray[lintIndex].tcnQuantity + '"
            .DeleteButton = False
            .AddButton = False

            If Request.QueryString("nMainAction") <> vbNullString And Request.QueryString("nMainAction") > 0 Then
                Session("nMainAction") = Request.QueryString("nMainAction")
            End If
            If CStr(Session("sOriginalForm")) = "SI011" Then
                .AddButton = True
                .DeleteButton = True
                .ActionQuery = False
                .Top = 200
            Else
                If CDbl(Session("nMainAction")) = 301 Then
                    .AddButton = True
                    .DeleteButton = True
                    .ActionQuery = False
                Else
                    If CDbl(Session("nMainAction")) = 401 Then
                        .AddButton = False
                        .DeleteButton = False
                        .ActionQuery = True
                    End If
                End If

                'If CDbl(Session("nMainAction")) = 302 Then
                '    .AddButton = True
                '    .DeleteButton = True
                '    .ActionQuery = False
                'End If

            End If

            If Request.QueryString("Reload") = "1" Then
                .sReloadIndex = Request.QueryString("ReloadIndex")
            End If
        End With
    End Sub
    '% insPreSI021: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreSI774()
        '--------------------------------------------------------------------------------------------
        Dim lintCount As Integer
        Dim ldblTotalAmount As Double
        Dim ldblTotalAmountClear As Double ' Total Neto de los repuestos
        Dim lclsQuot_Parts As eClaim.Quot_parts
        Dim lclsProf_ord As eClaim.Prof_ord
        Dim lclsTax_Fixval As eAgent.tax_fixval
        Dim ldblIva As Double
        Dim ldblSendCost As Integer
        Dim ldblFreightage As Integer

        ldblTotalAmountClear = 0

        lclsQuot_Parts = New eClaim.Quot_parts
        lclsProf_ord = New eClaim.Prof_ord
        lclsTax_Fixval = New eAgent.tax_fixval


        '+ Campos puntuales de la ventana:

        Response.Write("" & vbCrLf)
        Response.Write("		<BR>" & vbCrLf)
        Response.Write("		<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				")

        If lclsProf_ord.Find_nServ(mobjValues.StringToType(CStr(Session("nServiceOrder")), eFunctions.Values.eTypeData.etdDouble)) Then


            If CStr(Session("sOriginalForm")) <> "SI011" Then
                Response.Write(mobjValues.ShowWindowsName("SI011", GetLocalResourceObject("TitleCaption")))

                Response.Write("" & vbCrLf)
                Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnTotalAmountClearCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("					<TD>")


                Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(Today),   , GetLocalResourceObject("tcdEffecdateToolTip") ,   , , ,  , true))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("" & vbCrLf)
                Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnBudgetNumColumnCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("					<TD>")

                If lclsProf_ord.nNum_Budget > 0 And String.IsNullOrEmpty(Session("SI774_tcnNum_Budget")) Then
                    Session("SI774_tcnNum_Budget") = lclsProf_ord.nNum_Budget
                End If

                Response.Write(mobjValues.NumericControl("tcnNum_Budget", 5, Session("SI774_tcnNum_Budget"), False, GetLocalResourceObject("tcnBudgetNumColumnToolTip"), False, 0, False, "", "", "ChangeValueField(this);", , 0))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("				")


            End If

            Response.Write("" & vbCrLf)
            Response.Write("			</TR>" & vbCrLf)
            Response.Write("			<TR>" & vbCrLf)
            Response.Write("				")


            If CStr(Session("sOriginalForm")) <> "SI011" Then

                Response.Write("" & vbCrLf)
                Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tctProviderColumnCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("					<TD>")


                Response.Write(mobjValues.TextControl("tctProvider", 45, lclsProf_ord.sProvider, False, GetLocalResourceObject("tctProviderColumnToolTip"), , , , "", True, , ))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("						" & vbCrLf)
                Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tctWorkshColumnCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("					<TD>")

                Response.Write(mobjValues.TextControl("tctWorksh", 45, lclsProf_ord.sWorksh, False, GetLocalResourceObject("tctWorkshColumnToolTip"), , , , "", True, , ))



                Response.Write("</TD>" & vbCrLf)
                Response.Write("" & vbCrLf)



                Response.Write("					<TD></TD>" & vbCrLf)
                Response.Write("					<TD>")

                Response.Write("		<BR>" & vbCrLf)

                Response.Write("</TD>" & vbCrLf)
                Response.Write("				")


                Response.Write("			<TR>" & vbCrLf)


                Response.Write("</TD>" & vbCrLf)
                Response.Write("						" & vbCrLf)
                Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tctInspectorColumnCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("					<TD>")

                Response.Write(mobjValues.TextControl("tctInspector", 45, lclsProf_ord.sInspector  , False, GetLocalResourceObject("tctInspectorColumnToolTip"), , , , "", True, , ))



                Response.Write("</TD>" & vbCrLf)
                Response.Write("" & vbCrLf)

                Response.Write("			</TR>" & vbCrLf)



            End If

            Response.Write("" & vbCrLf)
            Response.Write("			</TR>" & vbCrLf)
            Response.Write("			<TR>" & vbCrLf)
            Response.Write("		<BR>" & vbCrLf)
            Response.Write("			</TR>" & vbCrLf)
            Response.Write("		</TABLE>" & vbCrLf)
            Response.Write("	")


            Response.Write(mobjValues.ShowWindowsName("SI774"))




            If CStr(Session("sOriginalForm")) <> "SI011" Then
                If CDbl(Session("nMainAction")) = 301 Then
                    mobjGrid.ActionQuery = False
                    mobjGrid.AddButton = True
                    mobjGrid.DeleteButton = True
                ElseIf CDbl(Session("nMainAction")) = 302 Then
                    mobjGrid.ActionQuery = False
                    mobjGrid.AddButton = False 'True
                    mobjGrid.DeleteButton = False 'True
                    mobjGrid.Columns("cbeSpareParts").EditRecord = False 'True
                Else
                    mobjGrid.ActionQuery = True
                    mobjGrid.AddButton = False
                    mobjGrid.DeleteButton = False
                End If
            End If

            '+ Se obtiene el porcentaje fijo de IVA (Tabla Tax_Fixval)		
            If lclsTax_Fixval.Find(1, CDate(Session("dEffecdate"))) Then
                ldblIva = lclsTax_Fixval.nPercent
            End If

            ldblSendCost = mobjValues.StringToType(CStr(lclsProf_ord.nSendCost), eFunctions.Values.eTypeData.etdDouble, True)
            ldblFreightage = mobjValues.StringToType(CStr(lclsProf_ord.nFreightage), eFunctions.Values.eTypeData.etdDouble, True)

            If ldblIva = eRemoteDB.Constants.intNull Then
                ldblIva = 0
            End If
            If ldblSendCost = eRemoteDB.Constants.intNull Then
                ldblSendCost = 0
            End If
            If ldblFreightage = eRemoteDB.Constants.intNull Then
                ldblFreightage = 0
            End If

            Session("nModulec_SI774") = lclsProf_ord.nModulec
            Session("nCover_SI774") = lclsProf_ord.nCover

        Else
            ldblIva = 0
            ldblSendCost = 0
            ldblFreightage = 0
        End If

        If lclsQuot_Parts.Find(mobjValues.StringToType(CStr(Session("nServiceOrder")), eFunctions.Values.eTypeData.etdDouble) ,  , , , , , ,  , Session("nUsercode")) Then
            ldblTotalAmountClear = 0
            ldblTotalAmount = 0
            For lintCount = 1 To lclsQuot_Parts.CountQuot_parts
                If lclsQuot_Parts.Item(lintCount) Then
                    With mobjGrid
                        If lclsQuot_Parts.nAuto_parts = 888 Then
                            .Columns("cbeSpareParts").EditRecord = False
                            .Columns("Sel").Disabled = True
                        Else
                            .Columns("cbeSpareParts").EditRecord = True
                            .Columns("Sel").Disabled = False
                        End If

                        If CStr(Session("sOriginalForm")) <> vbNullString And CStr(Session("sOriginalForm")) = "SI011" Then

                            .Columns("tcnQuantity").DefValue = CStr(lclsQuot_Parts.nQuantity_Parts)
                            .Columns("cbeSpareParts").DefValue = CStr(lclsQuot_Parts.nAuto_parts)
                            .Columns("tcnID").DefValue = CStr(lclsQuot_Parts.nId)

                            .Columns("chkSpare").DefValue = lclsQuot_Parts.sOriginal
                            .Columns("tcnQuantity_AUX").DefValue = CStr(lclsQuot_Parts.nQuantity_Parts)
                            .Columns("cbeSpareParts_AUX").DefValue = CStr(lclsQuot_Parts.nAuto_parts)
                        Else
                            .Columns("tcnQuantity").DefValue = CStr(lclsQuot_Parts.nQuantity_Parts)
                            .Columns("cbeSpareParts").DefValue = CStr(lclsQuot_Parts.nAuto_parts)
                            If lclsQuot_Parts.sOriginal = "1" Then
                                .Columns("chkSpare").Checked = CShort("1")
                            Else
                                .Columns("chkSpare").Checked = CShort("2")
                            End If

                            If lclsQuot_Parts.nAmount_Part < 0 Then
                                If lclsQuot_Parts.nAuto_parts = 888 Then 'Muestra negativo si la parte es "Monto ajuste de Orden"
                                    .Columns("tcnUnitValue").DefValue = CDbl(lclsQuot_Parts.nAmount_Part)
                                Else
                                    .Columns("tcnUnitValue").DefValue = CStr(0)
                                End If
                            Else
                                .Columns("tcnUnitValue").DefValue = CStr(lclsQuot_Parts.nAmount_Part)
                            End If

                            If lclsQuot_Parts.nAmount_Part < 0 Then
                                If lclsQuot_Parts.nAuto_parts = 888 Then 'Muestra negativo si la parte es "Monto ajuste de Orden"
                                    ldblTotalAmount = CDbl(lclsQuot_Parts.nQuantity_Parts * lclsQuot_Parts.nAmount_Part)
                                Else
                                    ldblTotalAmount = CDbl(lclsQuot_Parts.nQuantity_Parts * 0)
                                End If

                            Else
                                ldblTotalAmount = CDbl(lclsQuot_Parts.nQuantity_Parts * lclsQuot_Parts.nAmount_Part)
                            End If

                            .Columns("tcnTotalValue").DefValue = CStr(ldblTotalAmount)

                            .Columns("tcnQuantity_AUX").DefValue = CStr(lclsQuot_Parts.nQuantity_Parts)
                            .Columns("cbeSpareParts_AUX").DefValue = CStr(lclsQuot_Parts.nAuto_parts)

                            If lclsQuot_Parts.nAmount_Part < 0 Then

                                If lclsQuot_Parts.nAuto_parts = 888 Then 'Muestra negativo si la parte es "Monto ajuste de Orden"
                                    .Columns("tcnUnitValue_AUX").DefValue = CDbl(lclsQuot_Parts.nAmount_Part)
                                Else
                                    .Columns("tcnUnitValue_AUX").DefValue = CStr(0)
                                End If

                            Else
                                .Columns("tcnUnitValue_AUX").DefValue = CStr(lclsQuot_Parts.nAmount_Part)
                            End If

                            .Columns("tcnID").DefValue = CStr(lclsQuot_Parts.nId)


                            If Request.QueryString("nMainAction") = "302" Then
                                .Columns("Sel").OnClick = "insSelected(this.checked,this," & CStr(lclsQuot_Parts.nId) & ")"
                                .Columns("Sel").Checked = CShort("1")
                            End If
                        End If
                    End With
                    ldblTotalAmountClear = ldblTotalAmountClear + ldblTotalAmount
                    Response.Write(mobjGrid.DoRow())
                End If
            Next
        End If
        Response.Write(mobjGrid.closeTable())
        If lintCount > 0 Then
            Response.Write(mobjValues.HiddenControl("nCounter", CStr(lintCount)))
        Else
            Response.Write(mobjValues.HiddenControl("nCounter", CStr(1)))
        End If

        '+ Campos puntuales de la ventana:

        Response.Write("" & vbCrLf)
        Response.Write("		<BR>" & vbCrLf)
        Response.Write("		<TABLE WIDTH=100%>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				")


        If CStr(Session("sOriginalForm")) <> "SI011" Then

            Response.Write("" & vbCrLf)
            Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnTotalAmountClearCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.NumericControl("tcnTotalAmountClear", 18, mobjValues.StringToType(CStr(ldblTotalAmountClear), eFunctions.Values.eTypeData.etdDouble), False, GetLocalResourceObject("tcnTotalAmountClearToolTip"), True, 6, False, "", "", "CalculateTotal();", True, 0))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnIVACaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.NumericControl("tcnIVA", 5, CStr(ldblIva), False, GetLocalResourceObject("tcnIVAToolTip"), False, 2, False, "", "", "CalculateTotal()", True, 0))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnShippingCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.NumericControl("tcnShipping", 18, CStr(ldblSendCost), False, GetLocalResourceObject("tcnShippingToolTip"), True, 6, False, "", "", "CalculateTotal()", CDbl(Session("nMainAction")) = 401, 0))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("				")


        End If

        Response.Write("" & vbCrLf)
        Response.Write("			</TR>" & vbCrLf)
        Response.Write("			<TR>" & vbCrLf)
        Response.Write("				")


        If CStr(Session("sOriginalForm")) <> "SI011" Then

            Response.Write("" & vbCrLf)
            Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnTotalCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.NumericControl("tcnTotal", 18, CStr(0), False, GetLocalResourceObject("tcnTotalToolTip"), False, 6, False, "", "", "", True, 0))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("						" & vbCrLf)
            Response.Write("					<TD><LABEL>" & GetLocalResourceObject("tcnCharterCaption") & "</LABEL></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.NumericControl("tcnCharter", 18, CStr(ldblFreightage), False, GetLocalResourceObject("tcnCharterToolTip"), True, 6, False, "", "", "", CDbl(Session("nMainAction")) = 401, 0))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("" & vbCrLf)
            Response.Write("					<TD></TD>" & vbCrLf)
            Response.Write("					<TD>")


            Response.Write(mobjValues.CheckControl("chkMaiBag", GetLocalResourceObject("chkMaiBagCaption"), CStr(False), , "ChangeValue(this.checked)", , , GetLocalResourceObject("chkMaiBagToolTip")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("				")


        End If

        Response.Write("" & vbCrLf)
        Response.Write("			</TR>" & vbCrLf)
        Response.Write("		</TABLE>" & vbCrLf)
        Response.Write("	")


        If lintCount > 0 Then
            If CStr(Session("sOriginalForm")) <> "SI011" Then
                Response.Write("<script>CalculateTotal();</" & "Script>")
            End If
        End If

        'UPGRADE_NOTE: Object lclsQuot_Parts may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsQuot_Parts = Nothing
        'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsProf_ord = Nothing
        'UPGRADE_NOTE: Object lclsTax_Fixval may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        lclsTax_Fixval = Nothing
    End Sub
    '----------------------------------------------------------------------------------------------
    Private Sub insPreSI774Upd()
        '----------------------------------------------------------------------------------------------
        Dim lclsQuot_Parts As eClaim.Quot_parts

        If Request.QueryString("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())

            lclsQuot_Parts = New eClaim.Quot_parts


            With lclsQuot_Parts
                .nServ_Order = mobjValues.StringToType(CStr(Session("nServiceOrder")), eFunctions.Values.eTypeData.etdDouble)
                .nId = mobjValues.StringToType(Request.QueryString("nId"), eFunctions.Values.eTypeData.etdDouble)
                .nQuantity_Parts = mobjValues.StringToType(Request.QueryString("nQuantity"), eFunctions.Values.eTypeData.etdLong)
                .nAmount_Part = mobjValues.StringToType(Request.QueryString("nUnitValue"), eFunctions.Values.eTypeData.etdDouble)
                .Delete()
            End With

            'UPGRADE_NOTE: Object lclsQuot_Parts may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
            lclsQuot_Parts = Nothing
        End If

        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "ValClaim.aspx", "SI774", .QueryString("nMainAction"), mobjValues.ActionQuery, .QueryString("Index")))
            If .QueryString("Action") <> "Del" Then
                If .QueryString("Action") = "Add" Then
                    Response.Write("<script>self.document.forms[0].elements['tcnID'].value = top.opener.document.forms[0].elements['nCounter'].value;</" & "Script>")
                End If
                Response.Write("<script>self.document.forms[0].elements['chkSpare'].disabled = false;</" & "Script>")
            End If
        End With
    End Sub

</script>
<%  Response.Expires = -1

    mobjValues = New eFunctions.Values
    mobjGrid = New eFunctions.Grid
    mobjMenu = New eFunctions.Menues

    mobjValues.sCodisplPage = "si774"
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <script type="text/javascript" src="/VTimeNet/Scripts/Constantes.js"></script>
    <%
        With Response
            If Request.QueryString("Type") <> "PopUp" Then
                .Write(mobjMenu.setZone(2, "SI774", "SI774.aspx"))
                'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
                mobjMenu = Nothing
                Response.Write("<script>var nMainAction=top.frames['fraSequence'].plngMainAction</script>")
            End If
            .Write(mobjValues.StyleSheet())
            .Write(mobjValues.WindowsTitle("SI774"))
        End With
    %>
    <script>

//% ChangeValue: Cambia el valor del campo "chkMailBag" dependiendo si el mismo está o no marcado
//-------------------------------------------------------------------------------------
function ChangeValue(blnChecked){
//-------------------------------------------------------------------------------------
	if(blnChecked)
		self.document.forms[0].elements["chkMaiBag"].value = 1
	else
	    self.document.forms[0].elements["chkMaiBag"].value = 2;


}

function ChangeValueField(Field)
{
    lstrQString = 'FieldControl='+ Field.name + '&Value=' + Field.value
    insDefValues('SI774',lstrQString);
}

//% CalculateTotal: Calcula el total una vez que se añaden las cantidades del IVA y el flete
//-------------------------------------------------------------------------------------
function CalculateTotal(){
//-------------------------------------------------------------------------------------
	var ldblIVA=0;
	var ldblTotal=0;
	var ldblTotalClear=0;
	var ldblShippingAmount;

	if(self.document.forms[0].elements["tcnIVA"].value!="") 
		ldblIVA = insConvertNumber(self.document.forms[0].elements["tcnIVA"].value);

	if(ldblIVA > 0){
		ldblIVA = (ldblIVA / 100) + 1;
		ldblTotalClear = insConvertNumber(self.document.forms[0].elements["tcnTotalAmountClear"].value);
		ldblShippingAmount = insConvertNumber(self.document.forms[0].elements["tcnShipping"].value);
		ldblTotal = (ldblTotalClear + ldblShippingAmount) * ldblIVA;
		self.document.forms[0].elements["tcnTotal"].value = VTFormat(ldblTotal, '', '', '', 6, true);
	}else{
		ldblTotalClear = insConvertNumber(self.document.forms[0].elements["tcnTotalAmountClear"].value);
		ldblShippingAmount = insConvertNumber(self.document.forms[0].elements["tcnShipping"].value);
		ldblTotal = ldblTotalClear + ldblShippingAmount;
		self.document.forms[0].elements["tcnTotal"].value = VTFormat(ldblTotal, '', '', '', 6, true);
	}
}

//% insSelected: Asigna valor a una columna oculta una vez que se presiona el checkbox de la columna SEL
//-------------------------------------------------------------------------------------
function insSelected(blnChecked, Field, lintIndex){
//-------------------------------------------------------------------------------------
	var ldblTotal = 0;
	var ldblAmount = 0;
	var strParams;
    
    ldblAmount = insConvertNumber(marrArray[lintIndex-1].tcnTotalValue,'','', true);
    ldblTotal  = insConvertNumber(self.document.forms[0].tcnTotalAmountClear.value,'','', true);
    strParams  = "nServ_order=" + <%=Session("nServiceOrder")%> + 
                 "&nId=" + lintIndex; 
    with (document.forms[0]){
		if(!blnChecked){
		    ldblTotal = ldblTotal - ldblAmount;
			strParams = strParams + "&sSel=2" 
		}else{
			ldblTotal = ldblTotal + ldblAmount;
			strParams = strParams + "&sSel=1" 
		}
//+Se asigna el monto total
		self.document.forms[0].elements["tcnTotalAmountClear"].value = VTFormat(ldblTotal, '', '', '', 6, true);
		$(self.document.forms[0].elements["tcnTotalAmountClear"]).change();
	
//+Se actualiza la tabla quot_parts
		insDefValues('Quot_parts',strParams,'/VTimeNet/Claim/Claim');

	}
}

//% AddAmount: Calcula el total de los repuestos - ACM - 20/06/2002
//-------------------------------------------------------------------------------------
function AddAmount(tcnUnitValue)
//-------------------------------------------------------------------------------------
{
	if(self.document.forms[0].elements["tcnUnitValue"].value && self.document.forms[0].elements["tcnQuantity"].value>0)
	{
		self.document.forms[0].elements["tcnTotalValue"].value = VTFormat((insConvertNumber(self.document.forms[0].elements["tcnUnitValue"].value) * insConvertNumber(self.document.forms[0].elements["tcnQuantity"].value)), "", "", "", 6, true);
		self.document.forms[0].elements["tcnUnitValue"].value = VTFormat(insConvertNumber(self.document.forms[0].elements["tcnUnitValue"].value), "", "", "", 6, true);
	}
}

    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="SI774" action="ValClaim.aspx?x=1&nTransacio=SI774&sOriginalForm=<%=Session("sOriginalForm")%>">
    <%
       
        Call insDefineHeader()
        If Request.QueryString("Type") = "PopUp" Then
            Call insPreSI774Upd()
        Else
            Call insPreSI774()
        End If
        'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjValues = Nothing
        'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        mobjGrid = Nothing
    %>
    </form>
</body>
</html>
