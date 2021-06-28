<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga de datos del Grid de la ventana		
Dim mclsLife_load As eProduct.Life_load
Dim mcolLife_loads As eProduct.Life_loads

'+ Cambios en la lógica de descuento de los costos coberturas. 
Dim mstrExist_Modul As String


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddNumericColumn(101231, GetLocalResourceObject("tcnLoad_codColumnCaption"), "tcnLoad_cod", 5, CStr(eRemoteDB.Constants.intNull), True, GetLocalResourceObject("tcnLoad_codColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddCheckColumn(0, GetLocalResourceObject("chkInstallindColumnCaption"), "chkInstallind", "")
		'+ DP064 - Cargos
		.AddPossiblesColumn(101226, GetLocalResourceObject("cbeLoad_typeColumnCaption"), "cbeLoad_type", "table7996", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  , "insSetTaxin(this);",  ,  , GetLocalResourceObject("cbeLoad_typeColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		.AddPossiblesColumn(101227, GetLocalResourceObject("cbePayFreqColumnCaption"), "cbePayFreq", "table36", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbePayFreqColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		.AddTextColumn(101234, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		.AddTextColumn(101235, GetLocalResourceObject("tctShort_desColumnCaption"), "tctShort_des", 12, CStr(eRemoteDB.Constants.strNull),  , GetLocalResourceObject("tctShort_desColumnToolTip"))
		.AddNumericColumn(101232, GetLocalResourceObject("tcnLoadRateColumnCaption"), "tcnLoadRate", 9, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnLoadRateColumnToolTip"), True, 6)
		.AddNumericColumn(101233, GetLocalResourceObject("tcnLoadAmoColumnCaption"), "tcnLoadAmo", 18, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnLoadAmoColumnToolTip"), True, 6)
            .AddNumericColumn(101240, GetLocalResourceObject("tcnMinimumAmountColumnCaption"), "tcnMinimumAmount", 9, CStr(eRemoteDB.Constants.dblNull), , GetLocalResourceObject("tcnMinimumAmountColumnToolTip"), True, 6)
        .AddNumericColumn(101241, GetLocalResourceObject("tcnMaximumAmountColumnCaption"), "tcnMaximumAmount", 18, CStr(eRemoteDB.Constants.dblNull), , GetLocalResourceObject("tcnMaximumAmountColumnToolTip"), True, 6)
        .AddTextColumn(101236, GetLocalResourceObject("tctRoutineColumnCaption"), "tctRoutine", 12, CStr(eRemoteDB.Constants.strNull), , GetLocalResourceObject("tctRoutineColumnToolTip"))
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeAplyColumnCaption"), "cbeAply", "table5709", eFunctions.Values.eValuesType.clngWindowType,  , False,  ,  ,  , "insSetFunds(this);",  ,  , GetLocalResourceObject("cbeAplyColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkPreInvColumnCaption"), "chkPreInv", "",  ,  , "InsClickField(this);")
		.AddPossiblesColumn(101228, GetLocalResourceObject("cbeType_moveColumnCaption"), "cbeType_move", "Table5708", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeType_moveColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valModulecColumnCaption"), "valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, "0", True,  ,  ,  , "InsChangeField(this,""Module"");", mstrExist_Modul = "0",  , GetLocalResourceObject("valModulecColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valCoverColumnCaption"), "valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valCoverColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkFirst_cost_proColumnCaption"), "chkFirst_cost_pro", "")
		Call .AddCheckColumn(0, GetLocalResourceObject("chkTaxinColumnCaption"), "chkTaxin", "")
		Call .AddCheckColumn(0, GetLocalResourceObject("chkRetroColumnCaption"), "chkRetro", "")
		.AddNumericColumn(0, GetLocalResourceObject("tcnMonthiColumnCaption"), "tcnMonthi", 5, CStr(0),  , GetLocalResourceObject("tcnMonthiColumnToolTip"), True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnMontheColumnCaption"), "tcnMonthe", 5, CStr(0),  , GetLocalResourceObject("tcnMontheColumnToolTip"), True)
		'+ Se establece el estado inicial del campo "Estado" según la acción.
		If Request.QueryString.Item("Action") = "Add" Then
			.AddPossiblesColumn(101229, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "table26", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeStatregtColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		Else
			.AddPossiblesColumn(101230, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "table26", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valOriAplyColumnCaption"), "valOriAply", "tab_origin", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "InsChangeField(this,""Origin"");",  ,  , GetLocalResourceObject("valOriAplyColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkPremBasColumnCaption"), "chkPremBas", "")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valFundsColumnCaption"), "valFunds", "tab_funds", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valFundsColumnToolTip"))
            Call .AddCheckColumn(0, GetLocalResourceObject("chkFirst_applyColumnCaption"), "chkFirst_apply", "")
            Call .AddPossiblesColumn(101227, GetLocalResourceObject("cbeIndexColumnCaption"), "cbeIndex_table", "TABLE5520", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull), , , , , , , , GetLocalResourceObject("cbeIndexColumnCaptionToolTip"), eFunctions.Values.eTypeCode.eString)
            
            
		.AddHiddenColumn("tcnExist", CStr(0))
		.AddHiddenColumn("tctOldStatregt", "")
		.AddHiddenColumn("tctExist_Modul", mstrExist_Modul)
		.AddHiddenColumn("tctAddTaxin", "")
		
	End With
	
	With mobjGrid
		'If Request.QueryString.Item("Type") <> "PopUp" Then
		'	.Columns("cbeAply").Parameters.Add("sPreinv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		'Else
		'	.Columns("cbeAply").Parameters.Add("sPreinv", Request.QueryString.Item("sPreInv"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		'End If
		
		'+ Cambios en la lógica de descuento de los costos coberturas. 
		.Columns("valModulec").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valModulec").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valCover").Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valCover").Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valOriAply").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valOriAply").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valOriAply").Parameters.Add("nCollecDoctyp", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.Columns("valFunds").Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valFunds").Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valFunds").Parameters.Add("nOrigin", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valFunds").Parameters.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		
		.Codispl = "DP064"
		.Codisp = "DP064"
		.Width = 780
		.Height = 550
        .WidthDelete = 450
		.FieldsByRow = 2
		.Top = 20
		.Left = 25
		.bCheckVisible = Request.QueryString.Item("Action") <> "Add"
		.Columns("cbeLoad_type").EditRecord = True
		.Columns("cbeStatregt").BlankPosition = False
		.Columns("Sel").GridVisible = Not Session("bQuery")
		
		'+ El estado "En proceso de instalación" (sStatregt = 2) solo es usado por el sistema
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("cbeStatregt").TypeList = 2
			.Columns("cbeStatregt").List = CStr(2)
		End If
		
		'+ Los check box deben estar habilitados solo en la Popup
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("chkPreInv").Disabled = True
			.Columns("chkFirst_cost_pro").Disabled = True
			.Columns("chkTaxin").Disabled = True
			.Columns("chkInstallind").Disabled = True
			.Columns("chkRetro").Disabled = True
                .Columns("chkPremBas").Disabled = True
                .Columns("chkFirst_apply").Disabled = True
		Else
			.Columns("chkPreInv").Disabled = False
			.Columns("chkFirst_cost_pro").Disabled = False
			.Columns("chkTaxin").Disabled = False
			.Columns("chkInstallind").Disabled = False
			.Columns("chkRetro").Disabled = False
			.Columns("chkPremBas").Disabled = False
                .Columns("chkFirst_apply").Disabled = False
            End If
		
		.sDelRecordParam = "nBranch=" & mobjValues.typeToString(Session("nBranch"), eFunctions.Values.eTypeData.etdLong) & "&nProduct=" & mobjValues.typeToString(Session("nProduct"), eFunctions.Values.eTypeData.etdLong) & "&nLoad_cod='+ marrArray[lintIndex].tcnLoad_cod + '" & "&nMonthi='+ marrArray[lintIndex].tcnMonthi + '" & "&dEffecdate=" & mobjValues.typeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nUsercode=" & mobjValues.typeToString(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub
'% reaCurrDesc: Obtiene la descripción de la moneda
'-----------------------------------------------------------------------------
Private Function reaCurrDesc() As String
	'-----------------------------------------------------------------------------
	Dim lclsProduct As eProduct.Product
	lclsProduct = New eProduct.Product
	If lclsProduct.FindProduct_li(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		reaCurrDesc = mobjValues.getMessage(lclsProduct.nCurrency, "Table11")
	End If
	lclsProduct = Nothing
End Function

'% reaModules: Verifica la existencia de modulos para el producto
'-----------------------------------------------------------------------------
Private Function reaModules() As Object
	'-----------------------------------------------------------------------------
	Dim lclsTab_moduls As eProduct.Tab_moduls
	lclsTab_moduls = New eProduct.Tab_moduls
	If lclsTab_moduls.Find(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		mstrExist_Modul = "1"
	Else
		mstrExist_Modul = "0"
	End If
	lclsTab_moduls = Nothing
End Function

'% insPreDP064: Obtiene los cargos de los aportes
'-----------------------------------------------------------------------------
Private Sub insPreDP064()
	'-----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101225>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DIVControl("cbeCurrency",  , reaCurrDesc()))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write(" ")

	
	If mcolLife_loads.Find(Session("nBranch"), Session("nProduct"), Session("dEffecdate")) Then
		If mcolLife_loads.Count > 0 Then
			mobjGrid.DeleteButton = True
			For	Each mclsLife_load In mcolLife_loads
				With mobjGrid
					.Columns("tcnLoad_cod").DefValue = CStr(mclsLife_load.nLoad_cod)
					.Columns("cbeLoad_type").DefValue = CStr(mclsLife_load.nLoad_type)
					.Columns("cbePayFreq").DefValue = CStr(mclsLife_load.nPayFreq)
					.Columns("tctDescript").DefValue = mclsLife_load.sDescript
					.Columns("tctShort_des").DefValue = mclsLife_load.sShort_des
					.Columns("tcnLoadRate").DefValue = CStr(mclsLife_load.nLoadRate)
                    .Columns("tcnLoadAmo").DefValue = CStr(mclsLife_load.nLoadAmo)
                    .Columns("tcnMinimumAmount").DefValue = CStr(mclsLife_load.nMinimumAmount)
                    .Columns("tcnMaximumAmount").DefValue = CStr(mclsLife_load.nMaximumAmount)
					.Columns("tctRoutine").DefValue = mclsLife_load.sRoutine
					.Columns("chkPreInv").Checked = CShort(mclsLife_load.sPreInv)
					.Columns("chkPremBas").Checked = CShort(mclsLife_load.sPremBas)
					.Columns("cbeType_move").DefValue = CStr(mclsLife_load.nType_move)
					.Columns("cbeStatregt").DefValue = mclsLife_load.sStatregt
					.Columns("chkFirst_cost_pro").Checked = CShort(mclsLife_load.sFirst_cost_pro)
					.Columns("valModulec").DefValue = CStr(mclsLife_load.nModulec)
					.Columns("valCover").DefValue = CStr(mclsLife_load.nCover)
					.Columns("tcnExist").DefValue = CStr(1)
					.Columns("tctExist_Modul").DefValue = mstrExist_Modul
					.Columns("chkTaxin").Checked = CShort(mclsLife_load.sTaxin)
					.Columns("tctAddTaxin").DefValue = mclsLife_load.sAddTaxin
					.Columns("tcnMonthi").DefValue = CStr(mclsLife_load.nMonthi)
					.Columns("tcnMonthe").DefValue = CStr(mclsLife_load.nMonthe)
					.Columns("cbeAply").DefValue = CStr(mclsLife_load.nAply)
					.Columns("valOriAply").DefValue = CStr(mclsLife_load.nOriAply)
					.Columns("chkRetro").Checked = CShort(mclsLife_load.sRetro)
					.Columns("chkInstallind").Checked = CShort(mclsLife_load.sInstallind)
					.Columns("valFunds").DefValue = CStr(mclsLife_load.nFunds)
                    .Columns("chkFirst_apply").Checked = CShort(mclsLife_load.sFirst_apply)
                    .Columns("cbeIndex_table").DefValue = CStr(mclsLife_load.nIndex_table)
                        
                        .sEditRecordParam = "sPreInv=" & mclsLife_load.sPreInv
				End With
				Response.Write(mobjGrid.DoRow())
			Next mclsLife_load
		End If
	End If
	Response.Write(mobjGrid.closeTable())
End Sub

'% insPreDP064Upd: Realiza la eliminación de cargos
'-----------------------------------------------------------------------------
Private Sub insPreDP064Upd()
	'-----------------------------------------------------------------------------
	'- Objeto para manejo de los cargos de contribuciones
	Dim mclsLife_load As eProduct.Life_load
	
	Dim lblnPost As Object
	
	If Request.QueryString.Item("Action") = "Del" Then
		mclsLife_load = New eProduct.Life_load
		'+ Muestra el mensaje para eliminar registros
		Response.Write(mobjValues.ConfirmDelete())
		With mclsLife_load
			.nBranch = Session("nBranch")
			.nProduct = Session("nProduct")
			.nMonthi = mobjValues.StringToType(Request.QueryString.Item("nMonthi"), eFunctions.Values.eTypeData.etdLong)
			.nLoad_cod = mobjValues.StringToType(Request.QueryString.Item("nLoad_cod"), eFunctions.Values.eTypeData.etdLong)
			.dEffecdate = Session("dEffecdate")
			.nUsercode = Session("nUsercode")
			.Delete()
		End With
	End If
	mclsLife_load = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valProductSeq.aspx", "DP064", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsLife_load = New eProduct.Life_load
mcolLife_loads = New eProduct.Life_loads
mobjGrid = New eFunctions.Grid

mobjGrid.sCodisplPage = "DP064"
mobjValues.sCodisplPage = "DP064"

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE=javascript>
//+ Esta línea guarda la version procedente de VSS
    document.VssVersion="$$Revision: 3 $|$$Date: 13/02/06 11:28 $"


//% Cambios en la lógica de descuento de los costos coberturas. 
//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(vObj, sField){
//--------------------------------------------------------------------------------------------    
	var sValue;
	sValue = vObj.value;
	if (vObj.disabled==false) {
		with (self.document.forms[0]){
			switch (sField){
				case 'Module':
					valCover.Parameters.Param4.sValue=sValue;
					break;
				case 'Origin':
				    if (sValue!='')
						valFunds.Parameters.Param3.sValue=sValue;
					else
						valFunds.Parameters.Param3.sValue='0';
					break;
			}
		}
	}
	else{
	    vObj.value=0;
	}    
}

//% InsClickField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsClickField(Obj){
//--------------------------------------------------------------------------------------------
	var frm
	frm = self.document.forms[0]
	if (Obj.checked){
		//frm.cbeAply.Parameters.Param1.sValue="1";
		frm.cbePayFreq.disabled = true;
	}
	else{
		//frm.cbeAply.Parameters.Param1.sValue="2";
		frm.cbePayFreq.disabled = false;
	}
}
//% DP064 - Cargos
//% insSetTaxin: Se controla el cambio de valor del campo Afecto a impuesto
//--------------------------------------------------------------------------------------------
function insSetTaxin(vObj){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (vObj.value == '1'){
            chkTaxin.checked=tctAddTaxin.value=='1'?true:false;
            chkTaxin.disabled=true
        }
        else{
            chkTaxin.disabled=false;
            chkTaxin.checked=false;
        }
    }
}
//% DP064 - Cargos
//% insSetFunds: Se controla el campo Fondo según valor del campo Aplica sobre
//--------------------------------------------------------------------------------------------
function insSetFunds(vObj){
//--------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (vObj.value == '11' || vObj.value == '7'){
            valFunds.disabled=false
            btnvalFunds.disabled=false
        }
        else{
            valFunds.value='';
            UpdateDiv("valFundsDesc","");
            valFunds.disabled=true
            btnvalFunds.disabled=true
        }
    }
}
</SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP064"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP064", "DP064.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmDP064" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%=mobjValues.ShowWindowsName("DP064")%>

<%

'+ Cambios en la lógica de descuento de los costos coberturas. 
'+ Busqueda de los modulos de un producto
Call reaModules()

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP064()
Else
	Call insPreDP064Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsLife_load = Nothing
mcolLife_loads = Nothing
%> 
</FORM>
</BODY>
</HTML>




