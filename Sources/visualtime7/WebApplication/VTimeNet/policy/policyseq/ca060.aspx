<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="ePolicy" %>
<script language="VB" runat="Server">

    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    Dim mobjGrid As eFunctions.Grid
    Dim mobjMenu As eFunctions.Menues
    Dim lclsCover_Detail As ePolicy.Cover_Detail
    Dim lclsDetailsallowed As ePolicy.Detailsallowed
    Dim lcollclsCover_Details As ePolicy.Cover_Details
    Dim mcolDetailsallowedses As ePolicy.Detailsallowedses
    Dim lstrAction As String

    Dim lintGroup As New Integer
    
    '% insDefineHeader: Se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid.sCodisplPage = "CA060"
	
        '+ Se definen las columnas del grid
        With mobjGrid.Columns
               
            lintGroup = mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble)
         
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeModulecColumnCaption"), "cbeModulec", "TABMODULES_CA060", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True, , , , "insChangeModulec(this)", Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("cbeModulecColumnToolTip"))
            
            mobjGrid.Columns("cbeModulec").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("NGROUP_INSU", lintGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeModulec").Parameters.Add("DPROCESS", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

           
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeCoverColumnCaption"), "cbeCover", "TABGEN_COVER2", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  , , , ,  Request.QueryString.Item("Action") = "Update" ,  , GetLocalResourceObject("cbeCoverColumnToolTip"))
		
            .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table5625", eFunctions.Values.eValuesType.clngComboType, CStr(0), , , , , "insChangeField(this);", Request.QueryString.Item("Action") = "Update")
          
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTabGoodsColumnCaption"), "cbeTabGoods", "TAB_DETAILSALLOWED", eFunctions.Values.eValuesType.clngWindowType, CStr(0), True, , , , "insDescriptGood(this); ", Request.QueryString.Item("Action") = "Update", , GetLocalResourceObject("cbeTabGoodsColumnToolTip"))
            mobjGrid.Columns("cbeTabGoods").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeTabGoods").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeTabGoods").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeTabGoods").Parameters.Add("nType", Request.QueryString("nType") , eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
              
           
            If Request.QueryString.Item("Type") = "PopUp" Then
                Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, vbNullString, , GetLocalResourceObject("tctDescriptColumnToolTip"))
            End If
            
            Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "tabCurren_pol", eFunctions.Values.eValuesType.clngComboType, CStr(lclsCover_Detail.nCurrency), True, , , , , , , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
            mobjGrid.Columns("cbeCurrency").Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("nCertif", Session("nCertif"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            mobjGrid.Columns("cbeCurrency").Parameters.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
            
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapitalColumnCaption"), "tcnCapital", 18, CStr(lclsCover_Detail.nCapital), , GetLocalResourceObject("tcnCapitalColumnToolTip"), True, 6, , , "insCalcPremium(this);")
            Call .AddHiddenColumn("hddnCapital", CStr(0))
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 4, CStr(lclsCover_Detail.nRate), , GetLocalResourceObject("tcnRateColumnToolTip"), True, 2, , , "insCalcPremium(this);")
            Call .AddNumericColumn(0, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(lclsCover_Detail.nPremium), , GetLocalResourceObject("tcnPremiumColumnToolTip"), True, 6, , , , True)
           
        End With
	
        '+ Se definen las propiedades generales del grid
        With mobjGrid
            .Codispl = "CA060"
            .Width = 650
            .Height = 450
            .Top = 80
            .WidthDelete = 550
            .UpdContent = True
            .Columns("Sel").GridVisible = Not .ActionQuery
            .DeleteButton = False
            .ActionQuery = Session("bQuery")
            .bOnlyForQuery = Session("bQuery")
            
         
            .Columns("cbeCover").Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Columns("cbeCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
        
            .sDelRecordParam = "nType=' + marrArray[lintIndex].cbeType + '&nCode_good=' + marrArray[lintIndex].cbeTabGoods  + '&nModulec=' + marrArray[lintIndex].cbeModulec  + '&nCover=' + marrArray[lintIndex].cbeCover  + '"
            .Columns("cbeType").EditRecord = True
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
    End Sub

    '% insPreCA060: Se cargan los controles de la página
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA060()
        '--------------------------------------------------------------------------------------------
        Dim lblnExist As Boolean
        Dim lintCount As Short
        Dim ldblCapital As Object
	
        lblnExist = False
	
        '+ Se cargan en la colección Detailsallowedseslos tipos de bienes.
        Call mcolDetailsallowedses.Find(Session("nBranch"), Session("nProduct")  ,Session("dEffecdate")   )
	
        '+ Se buscan los bienes asegurables del cliente. 
        ldblCapital = 0
	
        If lcollclsCover_Details.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
		
            lintCount = 0
            With mobjGrid
                .DeleteButton = True
                For Each lclsCover_Detail In lcollclsCover_Details
                    
                    .Columns("tcnRate").DefValue = CStr(lclsCover_Detail.nRate)
                    .Columns("cbeCurrency").DefValue = CStr(lclsCover_Detail.nCurrency)
                    .Columns("tcnCapital").DefValue = CStr(lclsCover_Detail.nCapital)
                    .Columns("hddnCapital").DefValue = CStr(lclsCover_Detail.nCapital)
                    .Columns("tcnPremium").DefValue = CStr(lclsCover_Detail.nPremium)
                    .Columns("cbeType").DefValue = CStr(lclsCover_Detail.ntype)
                    .Columns("cbeModulec").DefValue = lclsCover_Detail.nModulec
                    .Columns("cbeCover").DefValue = lclsCover_Detail.nCover
                    .Columns("cbeTabGoods").DefValue = CStr(lclsCover_Detail.nCode_good)
                   
                    .Columns("cbeCover").Parameters.Add("nModulec", lclsCover_Detail.nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("cbeTabGoods").Parameters.Add("nType", lclsCover_Detail.ntype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    
                    .sEditRecordParam = "&nType=" & CStr(lclsCover_Detail.ntype)
                   
                    Response.Write(.DoRow)
                 
                    lintCount = lintCount + 1
                Next lclsCover_Detail
            End With
            lblnExist = True
        End If
        
        Response.Write(mobjGrid.closeTable())

        lclsCover_Detail = Nothing
        lcollclsCover_Details = Nothing
    End Sub

    '% insPreCA060Upd: Se muetra la ventana Popup para efecto de actualización del Gird
    '--------------------------------------------------------------------------------------------
    Private Sub insPreCA060Upd()
        '--------------------------------------------------------------------------------------------
        Dim lstrContent As String
        If Request.QueryString.Item("Action") = "Del" Then
            Response.Write(mobjValues.ConfirmDelete())
                   
            Call lclsCover_Detail.insPostCA060(mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("Action"), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCode_good"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPremium"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCapital"), eFunctions.Values.eTypeData.etdDouble))
            lstrContent = lclsCover_Detail.sContent
		
        End If
	
        With Request
            
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), , CShort(.QueryString.Item("Index"))))
        End With
	
        lclsCover_Detail = Nothing
    End Sub

</script>
<%Response.Expires = -1
    Response.CacheControl = "private"

 
    lstrAction = "valPolicySeq.aspx?nMainAction=" & Request.QueryString.Item("nMainAction")
  
    mobjValues = New eFunctions.Values
    mobjGrid = New eFunctions.Grid
    mobjMenu = New eFunctions.Menues
    lclsCover_Detail = New ePolicy.Cover_Detail
    lclsDetailsallowed = New ePolicy.Detailsallowed
    lcollclsCover_Details = New ePolicy.Cover_Details
    mcolDetailsallowedses = New ePolicy.Detailsallowedses

    mobjValues.ActionQuery = Session("bQuery")

    mobjValues.sCodisplPage = "CA060"
%>
<html>
<head>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <%With Response
            .Write(mobjValues.StyleSheet())
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Write("<script>var nMainAction=304</script>")
                .Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "CA060.aspx"))
                mobjMenu = Nothing
            End If
        End With
    %>
    <script>
        var marrCA060 = new Array(0)
        var mintCount = -1

        //% insCalcPremium: Calcula el monto de la prima.
        //-----------------------------------------------------------------------------------------------------------------------------------------
        function insCalcPremium(sOrigen) {
            //-----------------------------------------------------------------------------------------------------------------------------------------
            var ldblCapital
            with (self.document.forms[0]) {

                tcnPremium.value = VTFormat((insConvertNumber(tcnCapital.value) * insConvertNumber(tcnRate.value)) / 1000, '', '', '', 2, true);

                if (sOrigen == '1') {
                    if (tcnCapital.value != hddnCapital.value) {
                        if (tcnCapital.value == '')
                            ldblCapital = 0;
                        else
                            ldblCapital = insConvertNumber(tcnCapital.value, '.', ',');

                        ldblCapital = ldblCapital - insConvertNumber(hddnCapital.value, '.', ',');

                        hddnCapital.value = tcnCapital.value;
                    }
                }
            }
        }

       
        
        //%	insAddTab_Goods: Carga el arreglo con las tasas correspondientes con los bienes asegurables
        //%                  de la póliza.
        //---------------------------------------------------------------------------------------------
        function insAddTab_Goods(nCode_good, nType) {
            //---------------------------------------------------------------------------------------------
            var ludtTab_GoodFields = new Array(2)

            ludtTab_GoodFields[0] = nType
            ludtTab_GoodFields[1] = nCode_good
            marrCA060[++mintCount] = ludtTab_GoodFields
        }


        //% insChangeField: Se recargan los valores cuando cambia el campo
        //-------------------------------------------------------------------------------------------
        function insChangeField(Field) {
            //-------------------------------------------------------------------------------------------    
            with (self.document.forms[0]) {
                switch (Field.name) {
                    case "cbeModulec":
                     //   cbeCover.Parameters.Param8.sValue = (cbeModulec.value == '' ? 0 : cbeModulec.value);
                        cbeTabGoods.Parameters.Param4.sValue = (cbeType.value == '' ? 0 : cbeType.value);
                       
                        break;
                    case "cbeType":
                        cbeTabGoods.Parameters.Param4.sValue = (cbeType.value == '' ? 0 : cbeType.value);
                       
                        break;
                }
            }
        }


     
        //% insDescriptGood: Se recargan los valores cuando cambia el campo
        function insDescriptGood(Field) {
            with (self.document.forms[0]) {
                if (Field.name == "cbeTabGoods") {
                 
                    if (Field.value == '') {
                        tctDescript.disabled = false;
                        tctDescript.value = '';
                    }
                    else {
                        tctDescript.disabled = true;
                       // tctDescript.value = cbeTabGoods_Desc.Value;
                        tctDescript.value = cbeTabGoodsDesc.textContent;
                       
                    }
                  }        
                }
            }
      

      //%insChangeModulec: se controla el cambio de valor del campo "Módulo"
//--------------------------------------------------------------------------------------------------
function insChangeModulec(Field){
//--------------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
         if (Field.value==""){
              cbeCover.Parameters.Param5.sValue = 0;}
        else
         {   cbeCover.Parameters.Param5.sValue = Field.value;}

	        <%
            If Request.QueryString.Item("Action") <> "Update" Then
	            %>
        	     
		            cbeCover.value="";
		            UpdateDiv("cbeCoverDesc","");
            <%End If%>
	}
}

    </script>
</head>
<body onunload="closeWindows();">
    <form method="POST" id="FORM" name="fraContent" action="<%=lstrAction%>">
     <%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
        Call insDefineHeader()

       If Request.QueryString.Item("Type") <> "PopUp" Then
          Call insPreCA060()
        Else
          Call insPreCA060Upd()
        End If

        mobjValues = Nothing
        mobjGrid = Nothing
        lclsDetailsallowed = Nothing
        mcolDetailsallowedses = Nothing
    %>
    </form>
</body>
</html>
