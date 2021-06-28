<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">

'**- The object to handling the general function to load values is defined
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**- The object to handling the generic routines is defined
'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'**- The variable mobjGrid to handling the GRID of the window is defined
'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'**- The variables to loads valores are defined
'- Se definen las variables para la carga de los valores
Dim mcolFunds_pols As ePolicy.Funds_pols

Dim mclsFunds_Pol As ePolicy.Funds_Pol

Dim mclsFunds As ePolicy.Funds


'% insDefineHeader: Definición de columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------    
	With mobjGrid
		.Top = 70
		.Left = 190
		.Width = 450
		.Height = 530
		.Codispl = "VI010"
	End With
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddNumericColumn(0, GetLocalResourceObject("tcnCodFundColumnCaption"), "tcnCodFund", 5, CStr(0), False, GetLocalResourceObject("tcnCodFundColumnToolTip"), True,  ,  ,  ,  , True, 1)
		.AddTextColumn(0, "", "tctFundsDescript", 30, "",  , GetLocalResourceObject("tctFundsDescriptColumnToolTip"),  ,  ,  , True, 2)
		.AddCheckColumn(0, GetLocalResourceObject("chkActivFoundColumnCaption"), "chkActivFound", vbNullString,  ,  ,  , True, GetLocalResourceObject("chkActivFoundColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnitsColumnCaption"), "tcnUnits", 12, CStr(0),  , GetLocalResourceObject("tcnUnitsColumnToolTip"), True, 6,  ,  ,  , True, 3)
		.AddNumericColumn(0, GetLocalResourceObject("tcnTotal_AmountColumnCaption"), "tcnTotal_Amount", 18, CStr(0),  , GetLocalResourceObject("tcnTotal_AmountColumnToolTip"), True, 6,  ,  ,  , True, 4)
		.AddNumericColumn(0, GetLocalResourceObject("tcnBalanceColumnCaption"), "tcnBalance", 18, CStr(0),  , GetLocalResourceObject("tcnBalanceColumnToolTip"),  , 6,  ,  ,  , True, 5)
		.AddAnimatedColumn(0, GetLocalResourceObject("btnSignalColumnCaption"), "btnSignal", "/VTimeNet/Images/btnLargeDeleteOff.png", GetLocalResourceObject("btnSignalColumnToolTip"),  , "insChangeSignal()",  , 6)
		.AddNumericColumn(0, GetLocalResourceObject("tcnUnitsChangeColumnCaption"), "tcnUnitsChange", 18, CStr(0),  , GetLocalResourceObject("tcnUnitsChangeColumnToolTip"), True, 6,  ,  , "insCalculate_amount();",  , 7)
		.AddNumericColumn(0, GetLocalResourceObject("tcnValueChangeColumnCaption"), "tcnValueChange", 18, CStr(0),  , GetLocalResourceObject("tcnValueChangeColumnToolTip"), True, 6,  ,  , "insCalculate_amount_1();",  , 8)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAvailableColumnCaption"), "tcnAvailable", 18, CStr(0),  , GetLocalResourceObject("tcnAvailableColumnToolTip"), True, 6,  ,  ,  , True, 9)
		.AddNumericColumn(0, GetLocalResourceObject("tcnBuy_costColumnCaption"), "tcnBuy_cost", 18, CStr(0),  , GetLocalResourceObject("tcnBuy_costColumnToolTip"), True, 6,  ,  ,  , True, 10)
		.AddNumericColumn(0, GetLocalResourceObject("tcnSell_costColumnCaption"), "tcnSell_cost", 18, CStr(0),  , GetLocalResourceObject("tcnSell_costColumnToolTip"), True, 6,  ,  ,  , True, 11)
		.AddNumericColumn(0, GetLocalResourceObject("tcnSwi_costColumnCaption"), "tcnSwi_cost", 18, CStr(0),  , GetLocalResourceObject("tcnSwi_costColumnToolTip"), True, 6,  ,  ,  , True, 12)
		.AddNumericColumn(0, GetLocalResourceObject("tcnSwi_cost_percColumnCaption"), "tcnSwi_cost_perc", 18, CStr(0),  , GetLocalResourceObject("tcnSwi_cost_percColumnToolTip"), True, 6,  ,  ,  , True, 12)
		.AddNumericColumn(0, GetLocalResourceObject("tcnSwi_cost_totColumnCaption"), "tcnSwi_cost_tot", 18, CStr(0),  , GetLocalResourceObject("tcnSwi_cost_totColumnToolTip"), True, 6,  ,  ,  , True, 12)
		.AddNumericColumn(0, GetLocalResourceObject("tcnDeb_accColumnCaption"), "tcnDeb_acc", 18, CStr(0),  , GetLocalResourceObject("tcnDeb_accColumnToolTip"), True, 6,  ,  ,  , True, 13)
		.AddCheckColumn(0, GetLocalResourceObject("chkVigenColumnCaption"), "chkVigen", vbNullString,  ,  ,  , True, GetLocalResourceObject("chkVigenColumnToolTip"))
		.AddHiddenColumn("tcnSignal", CStr(0))
		.AddHiddenColumn("tcnBuy_costH", CStr(0))
		.AddHiddenColumn("tcnSell_costH", CStr(0))
		.AddHiddenColumn("tcnValueChange_aux", CStr(0))
		
		
	End With
	
	With mobjGrid
		.Columns("tctFundsDescript").EditRecord = True
		.Columns("Sel").disabled = True
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("btnSignal").GridVisible = False
			.Columns("tcnUnitsChange").GridVisible = False
			.Columns("tcnValueChange").GridVisible = False
			.Columns("tcnBuy_cost").GridVisible = False
			.Columns("tcnSell_cost").GridVisible = False
			.Columns("tcnDeb_acc").GridVisible = False
			.Columns("tcnAvailable").GridVisible = False
			.Columns("Sel").OnClick = "Confirm();"
		End If
		.DeleteButton = False
		.AddButton = False
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&nCertif=" & Request.QueryString.Item("nCertif") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&nOrigin=" & Request.QueryString.Item("nOrigin") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&sProcessType=" & Request.QueryString.Item("sProcessType")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreVI010: Esta función se encarga de cargar los datos en la forma "Detalle" 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI010()
	'--------------------------------------------------------------------------------------------
	Dim nAvailToBuy As Double
	Dim ldblBalance As Object
	Dim lintCount As Object
	
	With mobjValues
		If mcolFunds_pols.Find_VI010("2", .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			Call mclsFunds.insCalcData("2", .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), Session("sTypeCompanyUser"), .StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
			
			
			nAvailToBuy = mclsFunds_Pol.insCalAvailable("2", .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble))
			lintCount = 0
			For	Each mclsFunds_Pol In mcolFunds_pols
				With mobjGrid
					.Columns("tcnCodFund").DefValue = CStr(mclsFunds_Pol.nFunds)
					.Columns("tctFundsDescript").DefValue = mclsFunds_Pol.sDescript
					.Columns("tcnUnits").DefValue = CStr(mclsFunds_Pol.nQuan_avail)
					.Columns("tcnTotal_Amount").DefValue = CStr(mclsFunds_Pol.nAmount)
					.Columns("tcnBalance").DefValue = CStr(mclsFunds_Pol.nQuan_avail * mclsFunds_Pol.nAmount)
					ldblBalance = ldblBalance + (mclsFunds_Pol.nQuan_avail * mclsFunds_Pol.nAmount)
					.Columns("tcnBuy_costH").DefValue = CStr(mclsFunds_Pol.nBuy_cost)
					.Columns("tcnSell_costH").DefValue = CStr(mclsFunds_Pol.nSell_cost)
					.Columns("tcnSwi_cost").DefValue = CStr(mclsFunds.nSwi_cost)
					.Columns("tcnSwi_cost_perc").DefValue = CStr(mclsFunds.nSwi_cost_perc)
					.Columns("tcnSwi_cost_tot").DefValue = CStr(mclsFunds.getSwitchCost(mclsFunds_Pol.nQuan_avail * mclsFunds_Pol.nAmount))
					'.Columns("btnSignal").src = "/VTimeNet/Images/btnLargeAddOff.png"
					'.Columns("tcnSignal").DefValue = 1
					.Columns("btnSignal").src = "/VTimeNet/Images/btnLargeAddOff.png"
					.Columns("tcnSignal").DefValue = CStr(2)
					.Columns("tcnAvailable").DefValue = CStr(nAvailToBuy)
					If mclsFunds_Pol.sActivFound = "1" Then
						.Columns("chkActivFound").checked = CShort("1")
						.Columns("chkActivFound").DefValue = "1"
					Else
						.Columns("chkActivFound").checked = CShort("2")
						.Columns("chkActivFound").DefValue = "2"
					End If
					.Columns("Sel").checked = CShort(mclsFunds_Pol.sSel)
					If mclsFunds_Pol.sSel = "1" Then
						.Columns("Sel").disabled = False
					Else
						.Columns("Sel").disabled = True
					End If
					.Columns("chkVigen").checked = CShort(mclsFunds_Pol.sVigen)
					.Columns("tcnUnitsChange").DefValue = CStr(mclsFunds_Pol.nUnitsChange)
				End With
				Response.Write(mobjGrid.DoRow())
				lintCount = lintCount + 1
			Next mclsFunds_Pol
		Else
			Response.Write("<SCRIPT>alert('No existe valor cuota a la fecha de valorización para los fondos de la póliza');</" & "Script>")
		End If
		Response.Write(mobjGrid.CloseTable())
	End With
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <HR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""40%"">&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("tcnTotalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("            ")

	
	Response.Write(mobjValues.NumericControl("tcnTotal", 18, ldblBalance,  , GetLocalResourceObject("tcnTotalToolTip"), True, 6,  ,  ,  ,  , True))
	Response.Write(mobjValues.HiddenControl("hddCount", lintCount))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD WIDTH=""05%"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub

'% insPreVI010Upd: Se define esta funcion para contruir el contenido de la ventana UPD de los cambios de fodos de inversiones
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreVI010Upd()
	Dim lblnActionQuery As Boolean
	'----------------------------------------------------------------------------------------------------------------------------
	If Request.QueryString.Item("Action") = "Update" Then
		With Request
			Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyTra.aspx", "VI010", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End With
	End If
	If Request.QueryString.Item("Action") = "Del" Then
Response.Write("" & vbCrLf)
Response.Write("		<TABLE BORDER=1 CELLPADDING=5 BGCOLOR=WHITE  WIDTH=""100%"">" & vbCrLf)
Response.Write("		    <TR>" & vbCrLf)
Response.Write("		        <TD>")

		
		With mobjValues
			lblnActionQuery = .ActionQuery
			.ActionQuery = True
			Response.Write(mobjValues.TextAreaControl("txtMessage", 5, 40, "Se eliminara la propuesta y sus ordenes de compra y venta",  , GetLocalResourceObject("txtMessageToolTip")))
			.ActionQuery = lblnActionQuery
		End With
Response.Write("" & vbCrLf)
Response.Write("		        </TD>" & vbCrLf)
Response.Write("		    </TR>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD ALIGN=RIGHT>")


Response.Write(mobjValues.ButtonAcceptCancel("insAcceptData();", "top.close();", True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("		</TABLE>" & vbCrLf)
Response.Write("	")

		
	End If
End Sub

'% insDefineHeader_A: Definición de columnas del Grid en el caso de proceso definitivo
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader_A()
	'--------------------------------------------------------------------------------------------    
	With mobjGrid
		.Top = 70
		.Left = 190
		.Width = 450
		.Height = 530
		.Codispl = "VI010"
	End With
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "")
		.AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "")
		.AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10)
		.AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10)
		.AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate")
		.AddNumericColumn(0, GetLocalResourceObject("tcnBuysTotColumnCaption"), "tcnBuysTot", 18, CStr(0),  , GetLocalResourceObject("tcnBuysTotColumnToolTip"), True, 6,  ,  ,  , True, 12)
		.AddNumericColumn(0, GetLocalResourceObject("tcnSellsTotColumnCaption"), "tcnSellsTot", 18, CStr(0),  , GetLocalResourceObject("tcnSellsTotColumnToolTip"), True, 6,  ,  ,  , True, 12)
		.AddHiddenColumn("hddsSel", "2")
		.AddHiddenColumn("hddnBranch", vbNullString)
		.AddHiddenColumn("hddnProduct", vbNullString)
		.AddHiddenColumn("hddnPolicy", vbNullString)
		.AddHiddenColumn("hddnCertif", vbNullString)
	End With
	
	With mobjGrid
		.DeleteButton = False
		.AddButton = False
		.EditRecordDisabled = True
	End With
End Sub

'% insPreVI010: Esta función se encarga de cargar los datos en la forma "Detalle" 
'--------------------------------------------------------------------------------------------
Private Sub insPreVI010_A()
	'--------------------------------------------------------------------------------------------
	Dim ldblBalance As Object
	Dim lintCount As Short
	
	With mobjValues
		If mcolFunds_pols.Find_Request_VI010("2", .StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Request.QueryString.Item("nOrigin"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			
			lintCount = 0
			For	Each mclsFunds_Pol In mcolFunds_pols
				With mobjGrid
					.Columns("tctBranch").DefValue = mclsFunds_Pol.sBranch
					.Columns("tctProduct").DefValue = mclsFunds_Pol.sProduct
					.Columns("tcnPolicy").DefValue = CStr(mclsFunds_Pol.nPolicy)
					.Columns("tcnCertif").DefValue = CStr(mclsFunds_Pol.nCertif)
					.Columns("tcdEffecdate").DefValue = CStr(mclsFunds_Pol.dEffecdate)
					.Columns("tcnBuysTot").DefValue = CStr(mclsFunds_Pol.nBuysTot)
					.Columns("tcnSellsTot").DefValue = CStr(mclsFunds_Pol.nSellsTot)
					.Columns("hddnBranch").DefValue = CStr(mclsFunds_Pol.nBranch)
					.Columns("hddnProduct").DefValue = CStr(mclsFunds_Pol.nProduct)
					.Columns("hddnPolicy").DefValue = CStr(mclsFunds_Pol.nPolicy)
					.Columns("hddnCertif").DefValue = CStr(mclsFunds_Pol.nCertif)
					.Columns("Sel").OnClick = "InsCheckSelClick(this," & CStr(lintCount) & ")"
				End With
				Response.Write(mobjGrid.DoRow())
				lintCount = lintCount + 1
			Next mclsFunds_Pol
		End If
		Response.Write(mobjGrid.CloseTable())
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "VI010"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid.sCodisplPage = "VI010"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
mcolFunds_pols = New ePolicy.Funds_pols
mclsFunds_Pol = New ePolicy.Funds_Pol
mclsFunds = New ePolicy.Funds

%>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable Para Control de Versiones de Source Safe
    document.VssVersion="$$Revision: 3 $|$$Date: 16/06/06 8:57p $|$$Author: Gazuaje $"

//% insChangeSignal: Función utilizada para cambiar la imagen para la compra/venta de unidades
//--------------------------------------------------------------------------------------------
function insChangeSignal(){
//--------------------------------------------------------------------------------------------
     if (sTypeWindow == 'PopUp') {
        try {
		    with (self.document.images["btnSignal"]){
    			src=(document.forms[0].tcnSignal.value==1?"/VTimeNet/Images/btnLargeDeleteOff.png":"/VTimeNet/Images/btnLargeAddOff.png");
	    		document.forms[0].tcnSignal.value=(document.forms[0].tcnSignal.value==1?2:1);
		    }
	    } 
	    catch(error){}
    }
}
//% insCalculate_amount: calcula los importes
//-----------------------------------------------------------------------------
function insCalculate_amount(){
//-----------------------------------------------------------------------------
    var lstrParams; 
    with (self.document.forms[0])
    {
		try {
		    lstrParam = "nAmount=" + tcnUnitsChange.value + 
		                "&nUnitVal=" + tcnTotal_Amount.value + 
		                "&nSignal=" + tcnSignal.value + 
		                "&nBuyCost=" + tcnBuy_cost.value + 
		                "&nSellCost=" + tcnSell_cost.value + 
		                "&nSwCost=" + tcnSwi_cost.value + 
		                "&nSwCostPerc=" + tcnSwi_cost_perc.value + 
		                "&nInd=1"
		    insDefValues('Switch_Amount', lstrParam,'/VTimeNet/Policy/policytra');
		} catch(error){}
    }
}

//**% insCalculate_amount_1: Calculates the amounts
//% insCalculate_amount_1: calcula los importes
//-----------------------------------------------------------------------------
function insCalculate_amount_1(){
//-----------------------------------------------------------------------------
    var lstrParams; 
    with (self.document.forms[0])
    {
		try {
		    lstrParam = "nAmount=" + tcnValueChange.value + 
		                "&nUnitVal=" + tcnTotal_Amount.value + 
		                "&nSignal=" + tcnSignal.value + 
		                "&nBuyCost=" + tcnBuy_cost.value + 
		                "&nSellCost=" + tcnSell_cost.value + 
		                "&nSwCost=" + tcnSwi_cost.value +
		                "&nUnitini=" + tcnUnitsChange.value +  
		                "&nSwCostPerc=" + tcnSwi_cost_perc.value +  
		                "&nInd=2"
		    insDefValues('Switch_Amount', lstrParam,'/VTimeNet/Policy/policytra');
		} catch(error){}
	}
}

//% insCalculateSwCost: Calcula el monto del costo por cambio
//------------------------------------------------------------------------------------------- 
function insCalculateSwCost(){
//------------------------------------------------------------------------------------------- 
    
    
    with(self.document.forms[0]){
        tcnSwi_cost_tot.value = VTFormat(   insConvertNumber(tcnSwi_cost.value) + 
                                           ( insConvertNumber(tcnValueChange.value) * insConvertNumber(tcnSwi_cost_perc.value) / 100 )
                                         ,'','','',6,true);
    }
}

//% insCalculateDebit: Calcula el monto de debito
//------------------------------------------------------------------------------------------- 
function insCalculateDebit(){
//------------------------------------------------------------------------------------------- 
    
    insCalculateSwCost();
    
    with(self.document.forms[0]){
        tcnDeb_acc.value = VTFormat( insConvertNumber(tcnSell_cost.value) + 
                                     insConvertNumber(tcnBuy_cost.value) + 
                                     insConvertNumber(tcnSwi_cost_tot.value) ,'','','',6,true);
    }
}

//%InsCheckSelClick: Valida que el elemento no sea requerido, además actualiza el indicador
//%                  auxiliar de selección.
//-------------------------------------------------------------------------------------------
function InsCheckSelClick(Field, nIndex){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if (typeof(hddsSel.length) == 'undefined')
            hddsSel.value = (Field.checked?1:2);
        else
            hddsSel(nIndex).value = (Field.checked?1:2);
    }
}

function Confirm() {
    var lstrParams; 

	lstrParam = "nBranch=" + <%=Request.QueryString.Item("nBranch")%> + 
	            "&nProduct=" + <%=Request.QueryString.Item("nProduct")%> + 
	            "&nPolicy=" + <%=Request.QueryString.Item("nPolicy")%> + 
	            "&nCertif=" + <%=Request.QueryString.Item("nCertif")%>

	if(confirm("Se va a eliminar los movimientos de compra y venta de este traspaso esta seguro?"))
	    insDefValues('Switch_Del', lstrParam,'/VTimeNet/Policy/policytra');
    else 
		top.frames['fraFolder'].document.location.reload();
} 

</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%Response.Write(mobjValues.StyleSheet() & vbCrLf)%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="VI010" ACTION="valPolicyTra.aspx?x=1">
<%


Response.Write("<SCRIPT>var sTypeWindow='" & Request.QueryString.Item("Type") & "'</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "VI010", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		.Write(mobjValues.ShowWindowsName("VI010", Request.QueryString.Item("sWindowDescript")))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjGrid.ActionQuery = Session("bQuery")
	mobjMenu = Nothing
End If

Response.Write(mobjValues.HiddenControl("hddsProcessType", Request.QueryString.Item("sProcessType")))
Response.Write(mobjValues.HiddenControl("hdddEffecdate", Request.QueryString.Item("dEffecdate")))
If Request.QueryString.Item("sProcessType") = "2" Then
	Call insDefineHeader()
	If Request.QueryString.Item("Type") <> "PopUp" Then
		Call insPreVI010()
	Else
		Call insPreVI010Upd()
		Response.Write("<SCRIPT>insCalculate_amount();</SCRIPT>")
	End If
Else
	Call insDefineHeader_A()
	Call insPreVI010_A()
End If
mobjGrid = Nothing
mcolFunds_pols = Nothing
mclsFunds_Pol = Nothing
mobjValues = Nothing
mclsFunds = Nothing
%>
</FORM>
</BODY>
</HTML>





