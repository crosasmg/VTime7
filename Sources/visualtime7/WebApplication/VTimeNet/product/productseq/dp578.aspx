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
Dim mclsWay_pay_prod As eProduct.Way_pay_prod
Dim mcolWay_pay_prods As eProduct.Way_pay_prods

'- Se define variable para guardar la cantidad de registros	
Dim mintCount As Integer



'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid.ActionQuery = Session("bQuery")
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddAnimatedColumn(0, GetLocalResourceObject("sLinkColumnCaption"), "sLink", "/VTimeNet/Images/clfolder.png", GetLocalResourceObject("sLinkColumnToolTip"))
		End If
		
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeWay_payColumnCaption"), "cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeWay_payColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		.AddNumericColumn(0, GetLocalResourceObject("tcnRate_exColumnCaption"), "tcnRate_ex", 5, "",  , GetLocalResourceObject("tcnRate_exColumnToolTip"), True, 2)
		.AddNumericColumn(0, GetLocalResourceObject("tcnRate_discColumnCaption"), "tcnRate_disc", 5, "",  , GetLocalResourceObject("tcnRate_discColumnToolTip"), True, 2)
		.AddCheckColumn(0, GetLocalResourceObject("chkPrem_firstColumnCaption"), "chkPrem_first", "",  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
		.AddNumericColumn(0, GetLocalResourceObject("tcnNull_dayColumnCaption"), "tcnNull_day", 5,  ,  , GetLocalResourceObject("tcnNull_dayColumnToolTip"))
		.AddCheckColumn(0, GetLocalResourceObject("chkLastReceiptColumnCaption"), "chkLastReceipt", "",  ,  , "EnabledCheckControls(this.name);", Request.QueryString.Item("Type") <> "PopUp")
            .AddCheckColumn(0, GetLocalResourceObject("chkOneReceiptColumnCaption"), "chkOneReceipt", "", , , "EnabledCheckControls(this.name);", Request.QueryString.Item("Type") <> "PopUp")
            .AddCheckColumn(0, GetLocalResourceObject("chkCollectionColumnCaption"), "chkCollection", "", , , "EnabledCheckControls(this.name);", Request.QueryString.Item("Type") <> "PopUp")
		
		.AddHiddenColumn("tcnExist", CStr(0))
		.AddHiddenColumn("tctOldStatregt", "")
	End With
	
	With mobjGrid
		.Codispl = "DP578"
		.Codisp = "DP578"
		.Top = 135
		.Left = 100
		.Width = 500
		.Height = 300
		.DeleteButton = False
		.bCheckVisible = Request.QueryString.Item("Action") <> "Add"
		.Columns("Sel").GridVisible = True
		.Columns("cbeWay_pay").EditRecord = True
		.Columns("cbeWay_pay").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "nWay_pay='+ marrArray[lintIndex].cbeWay_pay + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreDP578: Obtiene los vias de pago del producto
'-----------------------------------------------------------------------------
Private Sub insPreDP578()
	'-----------------------------------------------------------------------------
	Dim lintIndex As Integer
	
	If mcolWay_pay_prods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		If mcolWay_pay_prods.Count > 0 Then
			lintIndex = 0
			mobjGrid.DeleteButton = True
			mintCount = mcolWay_pay_prods.Count - 1
			For lintIndex = 1 To mcolWay_pay_prods.Count
				With mobjGrid
					.Columns("cbeWay_pay").DefValue = CStr(mcolWay_pay_prods.item(lintIndex).nWay_pay)
					.Columns("tcnRate_ex").DefValue = CStr(mcolWay_pay_prods.item(lintIndex).nRate_ex)
					.Columns("tcnRate_disc").DefValue = CStr(mcolWay_pay_prods.item(lintIndex).nRate_disc)
					
					If mcolWay_pay_prods.item(lintIndex).sPrem_first = "1" Then
						mobjGrid.Columns("chkPrem_first").Checked = 1
					Else
						mobjGrid.Columns("chkPrem_first").Checked = 2
					End If
					
					
					
					.Columns("tcnNull_day").DefValue = CStr(mcolWay_pay_prods.item(lintIndex).nNull_day)
					.Columns("tcnExist").DefValue = CStr(1)
					.Columns("sLink").HRefScript = "ShowPayFreq(" & lintIndex - 1 & ")"
					.Columns("Sel").OnClick = "valDelete(" & lintIndex - 1 & ")"
					
					
					If mcolWay_pay_prods.item(lintIndex).sLastReceipt = "1" Then
						mobjGrid.Columns("chkLastReceipt").Checked = 1
					Else
						mobjGrid.Columns("chkLastReceipt").Checked = 2
					End If
					
					If mcolWay_pay_prods.item(lintIndex).sOneReceipt = "1" Then
						mobjGrid.Columns("chkOneReceipt").Checked = 1
					Else
						mobjGrid.Columns("chkOneReceipt").Checked = 2
					End If
					
                        If mcolWay_pay_prods.Item(lintIndex).sCollection = "1" Then
                            mobjGrid.Columns("chkCollection").Checked = 1
                        Else
                            mobjGrid.Columns("chkCollection").Checked = 2
                        End If

                    End With
				Response.Write(mobjGrid.DoRow())
			Next 
		End If
	End If
	Response.Write(mobjGrid.closeTable())
	With Request
		If .QueryString.Item("ReloadAction") = "Add" Then
			Response.Write("<SCRIPT>ShowPayFreq('" & mintCount & "','" & .QueryString.Item("nWay_pay") & "','" & .QueryString.Item("nRate_ex") & "','" & .QueryString.Item("nRate_disc") & "','" & .QueryString.Item("sPrem_first") & "','" & .QueryString.Item("nNull_day") & "')</" & "Script>")
		End If
	End With
End Sub

'% insPreDP578Upd: Realiza la eliminación de recargos/descuentos
'-----------------------------------------------------------------------------
Private Sub insPreDP578Upd()
	'-----------------------------------------------------------------------------
	'- Objeto para manejo de vías de pago
	Dim lclsWay_pay_prod As eProduct.Way_pay_prod
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			'+ Muestra el mensaje para eliminar registros
			Response.Write(mobjValues.ConfirmDelete())
			lclsWay_pay_prod = New eProduct.Way_pay_prod
                Call lclsWay_pay_prod.InsPostDP578Upd("Del", _
                                                               mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), _
                                                               mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), _
                                                               mobjValues.StringToType(.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), _
                                                               mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), _
                                                               0, _
                                                               0, _
                                                               " ", _
                                                               0, _
                                                               mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), _
                                                               String.Empty, _
                                                               String.Empty,
                                                               2)

			Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/ProductSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</" & "Script>")
		End If
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valProductSeq.aspx", "DP578", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	End With
	lclsWay_pay_prod = Nothing
End Sub

</script>
<%Response.Expires = -1

'- Se crean las instancias de las variables modulares
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP5478"

mobjMenu = New eFunctions.Menues
mclsWay_pay_prod = New eProduct.Way_pay_prod
mcolWay_pay_prods = New eProduct.Way_pay_prods
mobjGrid = New eFunctions.Grid
mobjGrid.sCodisplPage = "DP5478"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17:02 $"        
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP578"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "DP578", "DP578.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>    
<SCRIPT>

//% ShowPayFreq: muestra las frecuencias de pago para la vía de pago en tratamiento
//--------------------------------------------------------------------------------------------
function ShowPayFreq(Index){
//--------------------------------------------------------------------------------------------
	ShowPopUp('DP578A.aspx?sCodispl=DP578A&nWay_pay=' + marrArray[Index].cbeWay_pay + '&nMainAction=' + nMainAction ,'DP578',650,300,'no','no',200,180);
}

//% valDelete: Se verifica si se puede eliminar el registro
//------------------------------------------------------------------------------------------
function valDelete(nIndex){
//------------------------------------------------------------------------------------------
	insDefValues('DeleteDP578', 'nWay_pay=' + marrArray[nIndex].cbeWay_pay + '&nIndex=' + nIndex)
}

//% EnabledCheckControls: muestra las frecuencias de pago para la vía de pago en tratamiento
//--------------------------------------------------------------------------------------------
function EnabledCheckControls(sFieldName){
//--------------------------------------------------------------------------------------------
	
	if(sFieldName = 'chkLastReceipt'){
		if (self.document.forms[0].chkLastReceipt.checked)
			self.document.forms[0].chkOneReceipt.checked = true;
	}

	if (sFieldName = 'chkOneReceipt') {
	    if (!self.document.forms[0].chkOneReceipt.checked)
	        self.document.forms[0].chkLastReceipt.checked = false;
	}

//	if (sFieldName = 'chkCollection') {
//	    if (!self.document.forms[0].chkOneReceipt.checked)
//	        self.document.forms[0].chkLastReceipt.checked = false;
//	}
	
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP578" ACTION="valProductSeq.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("DP578"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP578()
Else
	Call insPreDP578Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
mclsWay_pay_prod = Nothing
mcolWay_pay_prods = Nothing
%> 
</FORM>
</BODY>
</HTML>




