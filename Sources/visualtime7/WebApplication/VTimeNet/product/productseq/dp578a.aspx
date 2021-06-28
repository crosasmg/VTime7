<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Objeto para obtener la información de fecuencias permitidas por vías de pago
Dim mclsWay_pay_prod As eProduct.Way_pay_prod
Dim mclsFreq_way_prod As eProduct.Freq_way_prod
Dim mcolFreq_way_prods As eProduct.Freq_way_prods


'%insDefineHeader. Definición de columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid.ActionQuery = Session("bQuery")
	With mobjGrid
		
		'+ Frecuencia de pago
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbePayFreqColumnCaption"), "cbePayFreq", "tabPay_Fracti", eFunctions.Values.eValuesType.clngComboType, "0", True,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbePayFreqColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		With mobjGrid.Columns("cbePayFreq").Parameters
			.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nQuota", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("dEffecdate", mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		'+ Moneda		
		.Columns.AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurrencyColumnCaption"))
		
		'+ Prima mínima de emisión
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPre_issueColumnCaption"), "tcnPre_issue", 18, "",  , GetLocalResourceObject("tcnPre_issueColumnToolTip"), True, 6)
		
		'+ Prima mínima de endoso 		
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnPre_amendColumnCaption"), "tcnPre_amend", 18, "",  , GetLocalResourceObject("tcnPre_amendColumnToolTip"), True, 6)
		
		'+ Cantidad minima de primas basicas
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnQpremColumnCaption"), "tcnQprem", 5, "",  , GetLocalResourceObject("tcnQpremColumnToolTip"), True)
		
		.Columns.AddCheckColumn(0, GetLocalResourceObject("sIvaColumnCaption"), "sIva", "",  ,  ,  , Request.QueryString.Item("Type") <> "PopUp")
		
		'+ Limite para el exceso tributario
		.Columns.AddNumericColumn(0, GetLocalResourceObject("tcLimit_ExcTaxColumnCaption"), "tcLimit_ExcTax", 18, "",  , GetLocalResourceObject("tcLimit_ExcTaxColumnToolTip"), True, 6)
		
            .Columns.AddCheckColumn(0, GetLocalResourceObject("sNo_sellColumnCaption"), "sNo_sell", "", , , , Request.QueryString.Item("Type") <> "PopUp")
		
		.Columns.AddHiddenColumn("tcnExist", CStr(0))
		.Columns.AddHiddenColumn("tcnWay_pay", CStr(0))
		.Columns.AddHiddenColumn("tcnCode", CStr(0))
		.Columns.AddHiddenColumn("sParam", vbNullString)
		
	End With
	
	With mobjGrid
		.Codispl = "DP578A"
		.Codisp = "DP578A"
		.Top = 135
		.Left = 100
		.Width = 350
		.Height = 400
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = True
            .Columns("sIva").OnClick = "ChangeValues(this)"
            .Columns("sNo_sell").OnClick = "ChangeValues(this)"
            
            .Columns("cbePayFreq").EditRecord = True
		.Columns("cbePayFreq").Disabled = Request.QueryString.Item("Action") = "Update"
		.sEditRecordParam = "nWay_pay='+ self.document.forms[0].tcnWay_pay.value + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreDP578A: Carga los datos de la forma
'---------------------------------------------------------------------------------------
Private Sub insPreDP578A()
	'---------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lintIndexFind As Object
	
	Call mclsWay_pay_prod.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("// insCheckSelClick : Establece La acción a ejecutar dependiendo del estado del campo Sel" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCheckSelClick(Field,lintIndex){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    var lstrParam=''" & vbCrLf)
Response.Write("    if (!Field.checked){" & vbCrLf)
Response.Write("		with (self.document.forms [0]){" & vbCrLf)
Response.Write("        lstrParam = ""nWay_pay=""+marrArray[lintIndex].tcnWay_pay + " & vbCrLf)
Response.Write("					""&nPayFreq="" + marrArray[lintIndex].cbePayFreq" & vbCrLf)
Response.Write("        }" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Del"",lstrParam)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    else{" & vbCrLf)
Response.Write("		with (self.document.forms [0]){" & vbCrLf)
Response.Write("			lstrParam=	""nWay_pay=""+marrArray[lintIndex].tcnWay_pay + " & vbCrLf)
Response.Write("						""&nPayFreq="" + marrArray[lintIndex].cbePayFreq" & vbCrLf)
Response.Write("		}" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Update"",lstrParam)" & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    Field.checked = !Field.checked" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>" & vbCrLf)
Response.Write("	<DIV ID=""Scroll"" STYLE=""width:550;height:225;overflow:auto;outset gray"">")

	
	lintIndex = 0
	With mobjGrid
		If mcolFreq_way_prods.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
			For	Each mclsFreq_way_prod In mcolFreq_way_prods
				.Columns("tcnExist").DefValue = CStr(mclsFreq_way_prod.nExist)
				.Columns("tcnWay_pay").DefValue = CStr(mclsFreq_way_prod.nWay_pay)
				.Columns("cbePayFreq").DefValue = CStr(mclsFreq_way_prod.nPayFreq)
				.Columns("cbeCurrency").DefValue = CStr(mclsFreq_way_prod.nCurrency)
				.Columns("tcnPre_issue").DefValue = CStr(mclsFreq_way_prod.nPre_issue)
				.Columns("tcnPre_amend").DefValue = CStr(mclsFreq_way_prod.nPre_amend)
				.Columns("tcnQprem").DefValue = CStr(mclsFreq_way_prod.nQprem)
				
				.Columns("sIva").DefValue = mclsFreq_way_prod.sIva
				.Columns("tcLimit_ExcTax").DefValue = CStr(mclsFreq_way_prod.NLimit_ExcTax)
				
				If mclsFreq_way_prod.sIva = "1" Then
					.Columns("sIva").Checked = CShort("1")
				Else
					.Columns("sIva").Checked = CShort("2")
				End If
                    .Columns("sNo_sell").DefValue = mclsFreq_way_prod.sNo_sell
                    If mclsFreq_way_prod.sNo_sell = "1" Then
                        .Columns("sNo_sell").Checked = CShort("1")
                    Else
                        .Columns("sNo_sell").Checked = CShort("2")
                    End If

                    
				If mclsFreq_way_prod.nExist = 1 Then
					.Columns("Sel").Checked = 1
				Else
					.Columns("Sel").Checked = 2
				End If
				
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				
				.Columns("sParam").DefValue = "nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nWay_pay=" & mclsFreq_way_prod.nWay_pay & "&nPayFreq=" & mclsFreq_way_prod.nPayFreq & "&dEffecdate=" & mobjValues.TypeToString(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
				Response.Write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			Next mclsFreq_way_prod
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	
Response.Write("" & vbCrLf)
Response.Write("	</DIV>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("DP578A"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")


Response.Write(mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	<TABLE>")

	
End Sub

'% insPreDP578AUpd: Realiza la eliminación de una fila de frecuencias por via de pago/producto
'----------------------------------------------------------------------------------------------
Private Sub insPreDP578AUpd()
	'----------------------------------------------------------------------------------------------
	Dim mclsFreq_way_prod As eProduct.Freq_way_prod
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		mclsFreq_way_prod = New eProduct.Freq_way_prod
		
            Call mclsFreq_way_prod.InsPostDP578Upd("Del", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWay_pay"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPayFreq"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPre_issue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnPre_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQprem"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("sIva"), mobjValues.StringToType(Request.Form.Item("tcLimit_ExcTax"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("sNo_sell"))
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valProductSeq.aspx", "DP578A", Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("DP758")

'- Variables auxiliares

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "DP578"

mclsWay_pay_prod = New eProduct.Way_pay_prod
mclsFreq_way_prod = New eProduct.Freq_way_prod
mcolFreq_way_prods = New eProduct.Freq_way_prods

mobjGrid = New eFunctions.Grid
mobjGrid.sCodisplPage = "DP578"
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "DP578"

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 14/11/03 12:59 $"        

/*% ChangeValue: Recarga la página tras  cambiar valores en combos
/*---------------------------------------------------------------------------------------------------------*/
function ChangeValues(Field){
/*---------------------------------------------------------------------------------------------------------*/
	if (Field.checked==true)
// si esta desmarcado y se marca 
		Field.defvalue = "1";
	else
// si esta marcado y se desmarca 
		Field.defvalue = "2";
}
</SCRIPT>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("DP578A"))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT> var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmDP578A" ACTION="valProductSeq.aspx?Time=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreDP578A()
Else
	Call insPreDP578AUpd()
End If

    mobjGrid = Nothing
    mobjValues = Nothing
    mclsFreq_way_prod = Nothing
    mcolFreq_way_prods = Nothing
    mclsWay_pay_prod = Nothing
%>
</FORM>
</BODY> 
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("DP578")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer
%>




