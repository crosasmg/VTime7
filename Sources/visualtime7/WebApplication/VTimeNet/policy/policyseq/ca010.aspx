<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenu As eFunctions.Menues
    Dim lclsProperty As ePolicy.Property_Renamed
    Dim lclsTab_Goods As ePolicy.Tab_goods
    Dim lcolPropertys As ePolicy.Propertys
    Dim mcolTab_Goodses As ePolicy.Tab_goodses
    Dim lstrAction As String


    '% LoadRates : Realiza un llamado a Tab_Goods para obtener las tasas de los bienes correspondientes
    '%            a la póliza.
    '---------------------------------------------------------------------------------------------------
    Private Sub LoadRates()
        '---------------------------------------------------------------------------------------------------
        If mcolTab_Goodses.Find(Session("nBranch"), Session("nProduct")) Then
            For Each lclsTab_Goods In mcolTab_Goodses
			Response.Write("<SCRIPT>insAddTab_Goods('" & lclsTab_Goods.nCode_good & "','" & mobjValues.TypeToString(lclsTab_Goods.nRate, eFunctions.Values.eTypeData.etdDouble) & "')</" & "Script>")
            Next lclsTab_Goods
        End If
    End Sub

    '% insDefineHeader: Se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid.sCodisplPage = "ca010"
        Dim lclsCertif As ePolicy.Certificat = New ePolicy.Certificat
        lclsCertif.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"))
        '+ Se definen las columnas del grid
        With mobjGrid.Columns
		
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTabGoodsColumnCaption"), "cbeTabGoods", "TabTab_goods", eFunctions.Values.eValuesType.clngComboType, , True, , , , "SearchRate(this.value)", , , GetLocalResourceObject("cbeTabGoodsColumnToolTip"))
            mobjGrid.Columns("cbeTabGoods").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeTabGoods").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, vbNullString, , GetLocalResourceObject("tctDescriptColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnServ_orderColumnCaption"), "tcnServ_order", 10, Session("nServ_order"), False, GetLocalResourceObject("tcnServ_orderColumnToolTip"))
		
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, CStr(lclsProperty.nCurrency), True, , , , , , , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            mobjGrid.Columns("cbeCurrency").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(lclsProperty.nCapital), , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6, , , "insCalcPremium('1')")
            Call .AddHiddenColumn("hddnCapital", CStr(0))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRatePropColumnCaption"), "tcnRateProp", 9, CStr(lclsProperty.nRateProp), , GetLocalResourceObject("tcnRatePropColumnToolTip"), True, 6, , , "insCalcPremium('2')")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(lclsProperty.nPremium), , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6)
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeFrandediColumnCaption"), "cbeFrandedi", "Table64", eFunctions.Values.eValuesType.clngComboType, "1", , , , , "insDisableValue(this)", , , GetLocalResourceObject("cbeFrandediColumnToolTip"))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 4, CStr(lclsProperty.nRate), , GetLocalResourceObject("tcnRateColumnToolTip"), True, 2, , , , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnFixamountColumnCaption"), "tcnFixamount", 18, CStr(lclsProperty.nFixamount), , GetLocalResourceObject("tcnFixamountColumnToolTip"), True, 6, , , , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnMinamountColumnCaption"), "tcnMinamount", 18, CStr(lclsProperty.nMinamount), , GetLocalResourceObject("tcnMinamountColumnToolTip"), True, 6, , , , True)
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnMaxamountColumnCaption"), "tcnMaxamount", 18, CStr(lclsProperty.nMaxamount), , GetLocalResourceObject("tcnMaxamountColumnToolTip"), True, 6, , , , True)
            Call .AddHiddenColumn("nId", "")
            Call .AddHiddenColumn("tcnNotenum", "")
            Call .AddHiddenColumn("tcnOriginalRateProp", "")
            Call .AddHiddenColumn("tcnOriginalPremium", "")
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "CA010"
            .Width = 650
            .Height = 450
            .Top = 80
            .WidthDelete = 550
            .UpdContent = True
            .Columns("Sel").GridVisible = Not .ActionQuery
            .DeleteButton = False            
            .ActionQuery = Session("bQuery")
            .bOnlyForQuery = Session("bQuery")
            .Columns("cbeTabGoods").EditRecord = True
            .Columns("cbeFrandedi").BlankPosition = False
            .Columns("tcnServ_order").Disabled = CStr(Session("nServ_order")) <> vbNullString
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
            
            If lclsCertif.sInd_Multiannual = "1" And lcolPropertys.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
                .AddButton = False                
            End If
        End With
        

    End Sub

    '% insPreCA010: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA010()
        '--------------------------------------------------------------------------------------------
        Dim lblnExist As Boolean
        Dim lintCount As Short
        Dim ldblCapital As Object

        lblnExist = False
	
        '+ Se cargan en la colección Tab_Goodses los tipos de bienes.
        Call mcolTab_Goodses.Find(Session("nBranch"), Session("nProduct"))
	
        '+ Se buscan los bienes asegurables del cliente.
        ldblCapital = 0
	
        If lcolPropertys.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
		
            lintCount = 0
            With mobjGrid
                .DeleteButton = True
                For Each lclsProperty In lcolPropertys
                    .Columns("cbeTabGoods").DefValue = CStr(lclsProperty.nCode_good)
                    .Columns("tctDescript").DefValue = lclsProperty.sDescript
                    .Columns("tcnServ_order").DefValue = CStr(lclsProperty.nServ_order)
                    .Columns("cbeCurrency").DefValue = CStr(lclsProperty.nCurrency)
                    .Columns("tcnCapital").DefValue = CStr(lclsProperty.nCapital)
                    .Columns("hddnCapital").DefValue = CStr(lclsProperty.nCapital)
                    .Columns("tcnRateProp").DefValue = CStr(lclsProperty.nRateProp)
                    .Columns("tcnPremium").DefValue = CStr(lclsProperty.nPremium)
                    .Columns("cbeFrandedi").DefValue = lclsProperty.sFrandedi
                    .Columns("tcnRate").DefValue = CStr(lclsProperty.nRate)
                    .Columns("tcnFixamount").DefValue = CStr(lclsProperty.nFixamount)
                    .Columns("tcnMinamount").DefValue = CStr(lclsProperty.nMinamount)
                    .Columns("tcnMaxamount").DefValue = CStr(lclsProperty.nMaxamount)
                    .Columns("nId").DefValue = CStr(lclsProperty.nId)
                    .Columns("tcnNotenum").DefValue = CStr(lclsProperty.nNotenum)
                    If CStr(Session("sBrancht")) = "3" Then
                        ldblCapital = ldblCapital + lclsProperty.nCapital
                    End If                    
                    .sDelRecordParam = "nId=' + marrArray[lintIndex].nId  + '"
                    .Columns("Sel").OnClick = "insDefValues(""DataAssociate"",""Index=" & lintCount & "&nId=" & lclsProperty.nId & """)"
                    Response.Write(.DoRow)
                    lintCount = lintCount + 1
                Next lclsProperty
            End With
            lblnExist = True
        End If
        Response.Write(mobjGrid.closeTable())
        Response.Write(mobjValues.HiddenControl("hddnVal_extra", ldblCapital))
	
        lclsProperty = Nothing
        lcolPropertys = Nothing
    End Sub

    '% insPreCA010Upd: Se muetra la ventana Popup para efecto de actualización del Gird
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA010Upd()
        '--------------------------------------------------------------------------------------------
        Dim lstrContent As String
        If Request.QueryString.Item("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())
		
            Call lclsProperty.insPostCA010(Session("nTransaction"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Request.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nUserCode"), Request.QueryString.Item("Action"))
            lstrContent = lclsProperty.sContent
		
            '+ Si la acción es un "Update" o "Add", se Actualiza o Añade el registro en selección.
        ElseIf Request.QueryString.Item("Action") = "Update" Or Request.QueryString.Item("Action") = "Add" Then
            Call LoadRates()
        End If
	
        With Request
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA010", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index")), lstrContent))
            If .QueryString.Item("Action") = "Update" Or .QueryString.Item("Action") = "Add" Then
                Response.Write(mobjValues.HiddenControl("hddnVal_extra", CStr(0)))
                Response.Write("<script>")
                Response.Write("self.document.forms[0].hddnVal_extra.value = top.opener.document.forms[0].hddnVal_extra.value;")
                Response.Write("</" & "Script>")
            End If
        End With
	
        lclsProperty = Nothing
    End Sub

</script>
<%Response.Expires = -1
    Response.CacheControl = "private"

    If CStr(Session("CallSequence")) = "Prof_ord" Then
        lstrAction = "/VTimeNet/Prof_ord/Prof_ordseq/valProf_ordseq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction")
    Else
        lstrAction = "valPolicySeq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction")
    End If
    mobjValues = New eFunctions.Values
    mobjGrid = New eFunctions.Grid
    mobjMenu = New eFunctions.Menues
    lclsProperty = New ePolicy.Property_Renamed
    lclsTab_Goods = New ePolicy.Tab_goods
    lcolPropertys = New ePolicy.Propertys
    mcolTab_Goodses = New ePolicy.Tab_goodses

    mobjValues.ActionQuery = Session("bQuery")

    mobjValues.sCodisplPage = "ca010"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%With Response
            .Write(mobjValues.StyleSheet())
            If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
                .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "CA010.aspx"))
                mobjMenu = Nothing
            End If
        End With
    %>
<SCRIPT>
    var marrCA010 = []
        var mintCount = -1

        //% insCalcPremium: Calcula el monto de la prima.
        //-----------------------------------------------------------------------------------------------------------------------------------------
        function insCalcPremium(sOrigen) {
            //-----------------------------------------------------------------------------------------------------------------------------------------
            var ldblCapital
            with (self.document.forms[0]) {
                if (tcnCapital.value != '' && tcnRateProp.value != '')
			tcnPremium.value = VTFormat((insConvertNumber(tcnCapital.value) * insConvertNumber(tcnRateProp.value))/100, '', '', '', 2, true);

                if (sOrigen == '1') {
                    if (tcnCapital.value != hddnCapital.value) {
                        if (tcnCapital.value == '')
                            ldblCapital = 0;
                        else
                            ldblCapital = insConvertNumber(tcnCapital.value, '.', ',');

                        ldblCapital = ldblCapital - insConvertNumber(hddnCapital.value, '.', ',');

                        hddnVal_extra.value = VTFormat(insConvertNumber(hddnVal_extra.value) + ldblCapital, '', '', '', 2, true);
                        hddnCapital.value = tcnCapital.value;
                    }
                }
            }
        }

        //% insDisableValue: Habilita o desabilita los campos correspondientes.
        //-----------------------------------------------------------------------------------------------------------------------------------------
        function insDisableValue(Field) {
            //-----------------------------------------------------------------------------------------------------------------------------------------
            with (document.forms[0]) {
                tcnRate.disabled = (cbeFrandedi.value == 1)
                tcnFixamount.disabled = (cbeFrandedi.value == 1)
                tcnMinamount.disabled = (cbeFrandedi.value == 1)
                tcnMaxamount.disabled = (cbeFrandedi.value == 1)

                if (cbeFrandedi.value == 1) {
                    tcnRate.value = '0,00';
                    tcnFixamount.value = '0';
                    tcnMinamount.value = '0';
                    tcnMaxamount.value = '0';
                }
            }
        }

        //%	SearchRate: Busca la tasa para el tipo de bien seleccionado y se muestra en el control correspondiente.
        //---------------------------------------------------------------------------------------------------------
        function SearchRate(nCode_good) {
            //---------------------------------------------------------------------------------------------------------
            var lblnFound = false

            for (var lintIndex = 0; lintIndex <= mintCount && !lblnFound; lintIndex++) {
                if (marrCA010[lintIndex][1] == nCode_good) lblnFound = true;
            }
            if (lblnFound)
                self.document.forms[0].tcnRateProp.value = marrCA010[lintIndex - 1][0];
            else
                self.document.forms[0].tcnRateProp.value = 0;
        }

        //%	insAddTab_Goods: Carga el arreglo con las tasas correspondientes con los bienes asegurables
        //%                  de la póliza.
        //---------------------------------------------------------------------------------------------
        function insAddTab_Goods(nCode_good, nRate) {
            //---------------------------------------------------------------------------------------------
    var ludtTab_GoodFields = []

            ludtTab_GoodFields[0] = nRate
            ludtTab_GoodFields[1] = nCode_good
            marrCA010[++mintCount] = ludtTab_GoodFields
        }

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="fraContent" ACTION="<%=lstrAction%>">
    <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
        Call insDefineHeader()

        If Request.QueryString.Item("Type") = "PopUp" Then
            Call insPreCA010Upd()
        Else
            Call insPreCA010()
        End If

        mobjValues = Nothing
        mobjGrid = Nothing
        lclsTab_Goods = Nothing
        mcolTab_Goodses = Nothing
    %>
</FORM>
</BODY>
</HTML>




