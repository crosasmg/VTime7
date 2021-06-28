<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

Dim mobjMenu As eFunctions.Menues

Dim mclsProd_am_bil As eProduct.Prod_Am_Bil

Dim mcolProd_am_bils As eProduct.Prod_Am_Bils

'- Objeto para el manejo del grid. 

Dim mobjGrid As eFunctions.Grid

Dim mblnDisabled As Boolean
Dim mstrIllness As String

Dim mintModulec As Object
Dim mlngCover As Object
Dim mintRole As Object
Dim mintCurrency As Object

Dim mintModulecChange As Object
Dim mlngCoverChange As Object
Dim mintRoleChange As Object
'    Dim mintCurrencyChange
Dim mstrIllnessChange As Object

Dim mblnModulec As Boolean


'% insDefaultValues: Se encarga de mostrar la tarifa por defecto seleccionada
'-----------------------------------------------------------------------------------------
Private Sub insDefaultValues()
	'-----------------------------------------------------------------------------------------
	
	Dim lclsProduct As eProduct.Product
	lclsProduct = New eProduct.Product
	mblnModulec = False
	If lclsProduct.IsModule(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		mblnModulec = True
	End If
	lclsProduct = Nothing
	
	mintModulec = mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
	mlngCover = mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)
	mintRole = mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble)
	mintCurrency = mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble)
	mstrIllness = Request.QueryString.Item("sIllness")
	
	If mintModulec <= 0 Then
		mintModulec = 0
	End If
	
	If mlngCover <= 0 Then
		mlngCover = 0
	End If
	
	If mintRole <= 0 Then
		mintRole = 0
	End If
	
	If mintCurrency <= 0 Then
		mintCurrency = 0
	End If
	
End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	If Request.QueryString.Item("nModulec") = vbNullString Then
		mintModulecChange = 0
	Else
		mintModulecChange = Request.QueryString.Item("nModulec")
	End If
	
	If Request.QueryString.Item("nCover") = vbNullString Then
		mlngCoverChange = 0
	Else
		mlngCoverChange = Request.QueryString.Item("nCover")
	End If
	
	If Request.QueryString.Item("nRole") = vbNullString Then
		mintRoleChange = 0
	Else
		mintRoleChange = Request.QueryString.Item("nRole")
	End If
	
	If Request.QueryString.Item("sIllness") = vbNullString Then
		mstrIllnessChange = 0
	Else
		mstrIllnessChange = Request.QueryString.Item("sIllness")
	End If
	
End Sub

'% insDefineHeader: Se definen los campos del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------  
	Dim lobjCol As eFunctions.Column
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "DP101"
	'+ Se definen las columnas del grid.
	
	With mobjGrid
		'		.Columns.AddPossiblesColumn 0, "Agrupación Prestación"  , "tcnGroup_Pres" , "table159",eFunctions.Values.eValuesType.clngComboType,, True,,,,,Request.QueryString("Action") <> "Add",, "Agrupación de Conceptos de pago"
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnGroup_PresColumnCaption"), "tcnGroup_Pres", "Table159", 1,  ,  ,  ,  ,  ,  , True ,  , GetLocalResourceObject("tcnGroup_PresColumnToolTip"))
		lobjCol = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnPay_ConcepColumnCaption"), "tcnPay_Concep", "tabcl_cov_bil2", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "ReaGroupPrest();", Request.QueryString.Item("Action") <> "Add",  , GetLocalResourceObject("tcnPay_ConcepColumnToolTip"))
		lobjCol.Parameters.ReturnValue("nGroup_Pres",  ,  , True)
		
            If Request.QueryString.Item("Type") = "PopUp" Then
                .Columns.AddHiddenColumn("cbenModulec", mintModulec)
                .Columns.AddHiddenColumn("cbenCover", mlngCover)
                .Columns.AddHiddenColumn("cbenRole", mintRole)
                .Columns.AddHiddenColumn("cbenCurrency", mintCurrency)
                .Columns.AddHiddenColumn("valIllness", mstrIllness)
            End If
            .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnDed_TypeColumnCaption"), "tcnDed_Type", "Table269", 1, , , , , , , , , GetLocalResourceObject("tcnDed_TypeColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_PercenColumnCaption"), "tcnDed_Percen", 4, vbNullString, , GetLocalResourceObject("tcnDed_PercenColumnToolTip"), , 2)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_AmountColumnCaption"), "tcnDed_Amount", 18, vbNullString, , GetLocalResourceObject("tcnDed_AmountColumnToolTip"), True, 6)
		
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_QuantiColumnCaption"), "tcnDed_Quanti", 5, vbNullString, , GetLocalResourceObject("tcnDed_QuantiColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnIndem_RateColumnCaption"), "tcnIndem_Rate", 5, CStr(100), , GetLocalResourceObject("tcnIndem_RateColumnToolTip"), , 2)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimitColumnCaption"), "tcnLimit", 18, vbNullString, , GetLocalResourceObject("tcnLimitColumnToolTip"), True, 6)
            lobjCol = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnTyplimColumnCaption"), "tcnTyplim", "Table269", eFunctions.Values.eValuesType.clngComboType, , , , , , "insChangeTyplim(this)", , , GetLocalResourceObject("tcnTyplimColumnToolTip"))
            lobjCol.TypeList = CShort("1")
            lobjCol.List = "4,7,8,10,11"
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCountColumnCaption"), "tcnCount", 5, vbNullString, , GetLocalResourceObject("tcnCountColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimit_exeColumnCaption"), "tcnLimit_exe", 18, vbNullString, , GetLocalResourceObject("tcnLimit_exeColumnToolTip"), True, 6)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPunishColumnCaption"), "tcnPunish", 4, vbNullString, , GetLocalResourceObject("tcnPunishColumnToolTip"), , 2)
		
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnDed_Quanti_2ColumnCaption"), "tcnDed_Quanti_2", 5, vbNullString, , GetLocalResourceObject("tcnDed_Quanti_2ColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnIndem_Rate_2ColumnCaption"), "tcnIndem_Rate_2", 5, CStr(100), , GetLocalResourceObject("tcnIndem_Rate_2ColumnToolTip"), , 2)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimit_2ColumnCaption"), "tcnLimit_2", 18, vbNullString, , GetLocalResourceObject("tcnLimit_2ColumnToolTip"), True, 6)
            lobjCol = .Columns.AddPossiblesColumn(0, GetLocalResourceObject("tcnTyplim_2ColumnCaption"), "tcnTyplim_2", "Table269", eFunctions.Values.eValuesType.clngComboType, , , , , , "insChangeTyplim(this)", , , GetLocalResourceObject("tcnTyplim_2ColumnToolTip"))
            lobjCol.TypeList = CShort("1")
            lobjCol.List = "4,7,8,10,11"
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnCount_2ColumnCaption"), "tcnCount_2", 5, vbNullString, , GetLocalResourceObject("tcnCount_2ColumnToolTip"))
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnLimit_exe_2ColumnCaption"), "tcnLimit_exe_2", 18, vbNullString, , GetLocalResourceObject("tcnLimit_exe_2ColumnToolTip"), True, 6)
            .Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPunish_2ColumnCaption"), "tcnPunish_2", 4, vbNullString, , GetLocalResourceObject("tcnPunish_2ColumnToolTip"), , 2)
		
		
            .sEditRecordParam = "nCover=" & mlngCover & "&nModulec=" & mintModulec & "&nRole=" & mintRole & "&sIllness=" & mstrIllness & "&nCurrency=' + self.document.forms[0].nCurrency.value + '"
		
            .sDelRecordParam = "nGroup_Pres=' + marrArray[lintIndex].tcnGroup_Pres  + '" & "&nPay_Concep=' + marrArray[lintIndex].tcnPay_Concep  + '" & "&nCover=" & mlngCover & "&nModulec=" & mintModulec & "&nRole=" & mintRole & "&sIllness=" & mstrIllness
		
            'With .Columns("tcnPay_ConcepColumnCaption").Parameters
            '.Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("nRole", mintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            'End With
		
            With .Columns("tcnPay_Concep").Parameters
                .Add("nModulec", mintModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nCover", mlngCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("nRole", mintRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            End With
		 
        End With
        With mobjGrid
            .Columns("tcnPay_Concep").EditRecord = True
            .AddButton = False
            .DeleteButton = False
            .Codispl = "DP101"
            .Width = 450
            .Height = 600
            .Top = 1
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
		
            Call .Splits_Renamed.AddSplit(0, vbNullString, 2)
            Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("3ColumnCaption"), 3)
            Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("7ColumnCaption"), 7)
            Call .Splits_Renamed.AddSplit(0, GetLocalResourceObject("8ColumnCaption"), 7)
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
        End With
End Sub

'% insPreDP101: Se cargan los controles de la página.
'--------------------------------------------------------------------------------------------
Private Sub insPreDP101()
	'--------------------------------------------------------------------------------------------
	Dim lblnDataFound As Boolean
	Dim lintIndex As Short
	Dim mclsProductli As eProduct.Product
	Dim mclsProduct_ge As eProduct.Product_ge
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim mclsProduct As eProduct.Product
	Dim lindexnModulec As Object
	Dim lindexnCover As Object
	Dim lindexnrole As Object
	Dim lindexsillness As Object
	Dim lblnModulec As Boolean
	
	lintIndex = 0
	lclsGeneral = New eGeneral.GeneralFunction
	
	mclsProductli = New eProduct.Product
	mclsProduct = New eProduct.Product
	mclsProduct_ge = New eProduct.Product_ge
	
	'+ Si tiene módulos asociados
	lblnModulec = mclsProduct.IsModule(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
	
	'+ Setea las variables, si son nulas le asignan 0
	If mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		lindexnModulec = 0
	Else
		lindexnModulec = Request.QueryString.Item("nModulec")
	End If
	
	If mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		lindexnCover = 0
	Else
		lindexnCover = Request.QueryString.Item("nCover")
	End If
	
	If mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		lindexnrole = 0
	Else
		lindexnrole = Request.QueryString.Item("nRole")
	End If
	
	If mobjValues.StringToType(Request.QueryString.Item("sIllness"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		lindexsillness = 0
	Else
		lindexsillness = Request.QueryString.Item("sIllness")
	End If
	
	Call mclsProduct.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
        If Not Session("bQuery") Then
            '+ Si es de vida 
            If CStr(Session("sBrancht")) = "1" Then
			If mclsProductli.FindProduct_li(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True) Then
                    '+ Si no se ha llenado la ventana de características de vida se envía error	                             
                    If mclsProductli.nCurrency = 0 Then
                        Response.Write("<SCRIPT> alert(""" & lclsGeneral.insLoadMessage(11414) & """); </" & "Script> ")
                        mobjGrid.AddButton = False
                        mobjGrid.DeleteButton = False
                    Else
                        lblnDataFound = mcolProd_am_bils.FindProd_Am_Bil(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnModulec, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnrole, eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sIllness"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), True)
                        '+ Si ingresó la cobertura se habilitan los botones
                        If mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                            '                       lindexsillness <> vbNullString Then
                            '+ Se habilitan los botones
                            mobjGrid.AddButton = True
                            mobjGrid.DeleteButton = True
                        End If
                        '+ Si encuentra datos en Prod_am_bil					            					       
                        If lblnDataFound Then 'ojo aqui
								                'If mclsProductli.nCurrency <> mcolProd_am_bils.nCurrencyAux Then
								                  '  Response.Write "<SCRIPT> alert(""" & "11407: " & lclsGeneral.insLoadMessage(11407) & """); </" & "Script> "
								               ' End If
                            '+ Se habilitan los botones
                            mobjGrid.AddButton = True
                            mobjGrid.DeleteButton = True
                        Else
                            '+ Si no tiene módulo asociado e ingresó una cobertura se habilitan los botones
                            If Not lblnModulec And mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                                mobjGrid.AddButton = True
                                mobjGrid.DeleteButton = True
                            End If
                        End If
                    End If
                Else
                    Response.Write("<SCRIPT> alert(""No entro vida""); </" & "Script> ")
                End If
            Else
                Response.Write("<SCRIPT> alert(""No entro""); </" & "Script> ")
                '+ Si el producto es de generales	      
                If mclsProduct_ge.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
				
                    lblnDataFound = mcolProd_am_bils.FindProd_Am_Bil(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnModulec, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(lindexnrole, eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sIllness"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))

                    '+ Si ingresó la cobertura se habilitan los botones
                    If mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> 0 And mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
                        '+ Se habilitan los botones
                        mobjGrid.AddButton = True
                        mobjGrid.DeleteButton = True
                    End If
                End If
            End If
        Else
            Response.Write("<SCRIPT> alert(""No entro query""); </" & "Script> ")
        End If
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TD VALIGN=TOP>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("		    <TD WIDTH =""15%""><LABEL ID=14390>" & GetLocalResourceObject("cbenCurrencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	'+ Si el producto es de vida se asocia la moneda que tiene Product_li            
	If CStr(Session("sBrancht")) = "1" Then
		Response.Write(mobjValues.PossiblesValues("cbenCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProductli.nCurrency),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenCurrencyToolTip"),  , 1))
		Response.Write(mobjValues.HiddenControl("nCurrency", CStr(mclsProductli.nCurrency)))
	Else
		'+ Si el producto es de generales se asocia la moneda que tine Product_ge                
		Response.Write(mobjValues.PossiblesValues("cbenCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsProduct_ge.nCurrency),  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenCurrencyToolTip"),  , 1))
		Response.Write(mobjValues.HiddenControl("nCurrency", CStr(mclsProduct_ge.nCurrency)))
	End If
Response.Write("" & vbCrLf)
Response.Write("            </TD>            " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>		" & vbCrLf)
Response.Write("			<TD WIDTH =""15%""><LABEL>" & GetLocalResourceObject("cbenModulecCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	With mobjValues
		Call .Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Call .Parameters.Add("dEffecdate", mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		Response.Write(mobjValues.PossiblesValues("cbenModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngComboType, .StringToType(lindexnModulec, eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  , "insReload(this)", Not lblnModulec, 5, GetLocalResourceObject("cbenModulecToolTip")))
		
	End With
	
Response.Write("</TD> " & vbCrLf)
Response.Write("            <TD WIDTH =""15%""><LABEL>" & GetLocalResourceObject("cbenCoverCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	With mobjValues
		'+ Verifica si el producto es de "vida" o de "generales".
		If CStr(Session("sBrancht")) = "1" Then
			'+ Si es un producto de vida.
			.Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", lindexnModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("cbenCover", "tablife_covmod", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), True, , , , , "insReload(this)", lblnModulec And lindexnModulec = 0, 5, GetLocalResourceObject("cbenCoverToolTip")))
                'Response.Write(mobjValues.PossiblesValues("cbenCover", "tablife_covmod", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), True, , , , , "insReload(this)", , 5, GetLocalResourceObject("cbenCoverToolTip")))
		Else
			'+ Si es un producto de generales.
			.Parameters.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", lindexnModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", mobjValues.StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                Response.Write(mobjValues.PossiblesValues("cbenCover", "tabgen_cover3", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), True, , , , , "insReload(this)", lblnModulec And lindexnModulec = 0, 5, GetLocalResourceObject("cbenCoverToolTip")))
                'Response.Write(mobjValues.PossiblesValues("cbenCover", "tabgen_cover3", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(lindexnCover, eFunctions.Values.eTypeData.etdDouble), True, , , , , "insReload(this)", , 5, GetLocalResourceObject("cbenCoverToolTip")))
		End If
	End With
	
Response.Write("</TD>                   " & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=13052>" & GetLocalResourceObject("valIllnessCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
        Response.Write(mobjValues.PossiblesValues("valIllness", "tabtab_am_ill_2", eFunctions.Values.eValuesType.clngWindowType, lindexsillness, False, , , , , "changeIllness(this);", mblnDisabled, 8, GetLocalResourceObject("valIllnessToolTip"), eFunctions.Values.eTypeCode.eString))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("cbenRoleCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("                ")

	
        Response.Write(mobjValues.PossiblesValues("cbenRole", "table12", eFunctions.Values.eValuesType.clngComboType, lindexnrole, False, , , , , "insReload(this)", , 5, GetLocalResourceObject("cbenRoleToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("            </TD> " & vbCrLf)
Response.Write("		</TR>        " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    </TD>		" & vbCrLf)
Response.Write("</TABLE>											       " & vbCrLf)
Response.Write("")

	
	If lblnDataFound Then
		For	Each mclsProd_am_bil In mcolProd_am_bils
			With mobjGrid
				.Columns("tcnGroup_Pres").DefValue = CStr(mclsProd_am_bil.nGroup_Pres)
				.Columns("tcnPay_Concep").DefValue = CStr(mclsProd_am_bil.nPay_Concep)
				.Columns("tcnDed_Type").DefValue = CStr(mclsProd_am_bil.nDed_Type)
				.Columns("tcnDed_Amount").DefValue = CStr(mclsProd_am_bil.nDed_Amount)
				.Columns("tcnDed_Percen").DefValue = CStr(mclsProd_am_bil.nDed_Percen)
				.Columns("tcnDed_Quanti").DefValue = CStr(mclsProd_am_bil.nDed_Quanti)
				.Columns("tcnIndem_rate").DefValue = CStr(mclsProd_am_bil.nIndem_rate)
				.Columns("tcnLimit").DefValue = CStr(mclsProd_am_bil.nLimit)
				.Columns("tcnTypLim").DefValue = CStr(mclsProd_am_bil.nTypLim)
				.Columns("tcnCount").DefValue = CStr(mclsProd_am_bil.nCount)
				.Columns("tcnLimit_exe").DefValue = CStr(mclsProd_am_bil.nLimit_exe)
				.Columns("tcnPunish").DefValue = CStr(mclsProd_am_bil.nPunish)
				.Columns("tcnDed_Quanti_2").DefValue = CStr(mclsProd_am_bil.nDed_Quanti_2)
				.Columns("tcnIndem_Rate_2").DefValue = CStr(mclsProd_am_bil.nIndem_Rate_2)
				.Columns("tcnLimit_2").DefValue = CStr(mclsProd_am_bil.nLimit_2)
				.Columns("tcnTypLim_2").DefValue = CStr(mclsProd_am_bil.nTypLim_2)
				.Columns("tcnCount_2").DefValue = CStr(mclsProd_am_bil.nCount_2)
				.Columns("tcnLimit_exe_2").DefValue = CStr(mclsProd_am_bil.nLimit_exe_2)
				.Columns("tcnPunish_2").DefValue = CStr(mclsProd_am_bil.nPunish_2)
				Response.Write(.DoRow)
			End With
			
			lintIndex = lintIndex + 1
			
			If lintIndex = 200 Then
				Exit For
			End If
			
		Next mclsProd_am_bil
	End If
	
	Response.Write(mobjGrid.closeTable)
	
	mcolProd_am_bils = Nothing
	mclsProd_am_bil = Nothing
End Sub

'% insPreDP101Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreDP101Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsProduct_Win As eProduct.Prod_win
	lclsProduct_Win = New eProduct.Prod_win
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		Call mclsProd_am_bil.insPostDP101("Delete", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nGroup_Pres"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPay_concep"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sIllness"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Session("nUsercode"))
		
		'+ Se verifica si existen registros
		Call mclsProd_am_bil.FindCurrency(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), Session("sIllness"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
		'+ Si se eliminaron todos los registros de la tabla se actualiza Prod_win con el valor sin contenido		
		If mclsProd_am_bil.nCurrency = 0 Then
			Call lclsProduct_Win.Add_Prod_win(Session("nBranch"), Session("nProduct"), Session("dEffecdate"), "DP101", "1", Session("nUsercode"))
		End If
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProductSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	
        'Response.Write("<SCRIPT>alert(top.opener.document.forms[0].nCurrency.value);</" & "Script>")
        
        'Response.Write("<SCRIPT>alert(" & Request.QueryString.Item("nCurrencyAux") & ");</" & "Script>")

        'Response.Write("<SCRIPT>self.document.forms[0].nCurrencyAux.value=top.opener.document.forms[0].nCurrency.value;</" & "Script>")
	'+ Se actualiza la página del menú    
	Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
	
	lclsProduct_Win = Nothing
	mclsProd_am_bil = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mclsProd_am_bil = New eProduct.Prod_Am_Bil
mcolProd_am_bils = New eProduct.Prod_Am_Bils

mobjValues.sCodisplPage = "DP101"
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript">

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $|$$Author: Nvaplat61 $"

//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(Field){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    var lblnChange

    lstrQuery='';
	lblnChange = false;
	
    with (self.document.forms[0]) {
            self.document.forms[0].target = 'fraGeneric';
            UpdateDiv('lblWaitProcess', '<MARQUEE>Procesando, por favor espere...</MARQUEE>', '');

			lstrQuery = lstrQuery + "&nCover=" + cbenCover.value + "&nRole=" + cbenRole.value + "&sIllness=" + valIllness.value + "&nModulec=" + cbenModulec.value + "&nCurrency=" + nCurrency.value
		    document.location.href = document.location.href.replace(/&nCover=.*/,'') + lstrQuery
	}
  }


//% ReaGroupPrest: Lee Agrupación de Prestación para el concepto elegido
//-------------------------------------------------------------------------------------------
function ReaGroupPrest(){
//-------------------------------------------------------------------------------------------
	self.document.forms[0].tcnGroup_Pres.value = self.document.forms[0].tcnPay_Concep_nGroup_Pres.value;    
}
/*---------------------------------------------------------------------------------------------------------*/

function changeIllness(Field) {
    insReload(Field);
}

</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "DP101", "DP101.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If

Response.Write(mobjValues.StyleSheet())
Call insReaInitial()
Call insDefaultValues()

%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP101" ACTION="valProductSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP101"))
Call insDefineHeader()

mobjGrid.ActionQuery = Session("bQuery")

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreDP101Upd()
Else
	Call insPreDP101()
End If

mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>






