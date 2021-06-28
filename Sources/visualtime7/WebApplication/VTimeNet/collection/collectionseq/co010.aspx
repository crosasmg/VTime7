<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eCollection" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 11.59.53
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As New eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues

Dim mblnAccess As Boolean
Dim lstrDocument As Object
Dim mstrAlert As String
Dim mdblTotals As Object

Dim mclsCashMovs As eCollection.CashBankAccMovs

Dim mlngCount As Integer
Dim mdblPaidAmount As Double
Dim mdblTotalAmount As Double
Dim mdblTotalAmountGen As Double
Dim mdblExchangeUF As Double
    Dim mstrDocument_Old As String


'%insPrevInf(). Este procedimiento se encarga de cargar los valores a utilizar en la página.
'---------------------------------------------------------------------------------------
Private Sub insPrevInf()
	'---------------------------------------------------------------------------------------
	Dim ldblTotals As Object
	Dim ldblTotals_loc As Object
	
	mblnAccess = True


	Call mclsCashMovs.Find("CO010", Request.QueryString.Item("Type"), Session("nBordereaux"), Session("CO001_nAction"), Session("sStatus"), Session("dCollectDate"), Session("dValueDate"), Session("sRelOrigi"))
	
	mlngCount = mclsCashMovs.nCount
	mdblPaidAmount = System.Math.Round(mclsCashMovs.nPaidAmount)
	mdblTotalAmount = System.Math.Round(mclsCashMovs.nTotalAmount)
	mdblTotalAmountGen = System.Math.Round(mclsCashMovs.nTotalAmountGen)
	mdblExchangeUF = mclsCashMovs.nExchangeUF
	mstrDocument_Old = mclsCashMovs.sDocument_old
	
	'+ Se verifica que no existan documentos anteriores pendientes de cobro asociados a las pólizas involucradas en la devolución.	    
	If mstrDocument_Old <> vbNullString Then
		mstrAlert = "Err. 60254 " & eFunctions.Values.GetMessage(60254) & " (" & mstrDocument_Old & ")"
		mblnAccess = False
		mlngCount = 0
	End If
	
End Sub

'%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
'%del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.00
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "CO010"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+Se asignan la configuración de la ventana (GRID)    
	mobjGrid.ActionQuery = CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery)
	
	'+ Se definen todas las columnas del Grid
	With mobjGrid.Columns
		mobjGrid.sDelRecordParam = "sType=' + marrArray[lintIndex].sType + '&nSequence=' + marrArray[lintIndex].nSequence + '"
		lobjColumn = .AddHiddenColumn("sType", "")
		lobjColumn = .AddHiddenColumn("nSequence", "0")
		lobjColumn = .AddPossiblesColumn(100510, GetLocalResourceObject("nTypDevColumnCaption"), "nTypDev", "Table7501", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insLockControl(this, ""Add"")",  ,  , GetLocalResourceObject("nTypDevColumnToolTip"))
		lobjColumn.EditRecord = True
		lobjColumn = .AddPossiblesColumn(100510, GetLocalResourceObject("nAccBankOColumnCaption"), "nAccBankO", "tabBankAgAccount", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("nAccBankOColumnToolTip"))
		lobjColumn = .AddPossiblesColumn(100511, GetLocalResourceObject("nCurrencyColumnCaption"), "nCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  , "insShowLocalAmount()", True,  , GetLocalResourceObject("nCurrencyColumnToolTip"))
		lobjColumn = .AddNumericColumn(100515, GetLocalResourceObject("nAmountColumnCaption"), "nAmount", 18, Request.QueryString.Item("nTotalRel"),  , GetLocalResourceObject("nAmountColumnToolTip"), True, 6,  ,  , "insShowLocalAmount()")
		lobjColumn = .AddNumericColumn(100516, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 14, CStr(1),  , GetLocalResourceObject("tcnExchangeColumnToolTip"), True, 6,  ,  ,  , True)
		lobjColumn = .AddNumericColumn(100517, GetLocalResourceObject("tcnAmountLocColumnCaption"), "tcnAmountLoc", 18, CStr(0),  , GetLocalResourceObject("tcnAmountLocColumnToolTip"), True, 6,  ,  ,  , True)
		lobjColumn = .AddClientColumn(100518, GetLocalResourceObject("sClientColumnCaption"), "sClient", "",  , GetLocalResourceObject("sClientColumnToolTip"),  ,  , "lblCliename")
		lobjColumn = .AddPossiblesColumn(100512, GetLocalResourceObject("nBankDesColumnCaption"), "nBankDes", "table7", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "self.document.forms[0].nBk_agency.Parameters.Param1.sValue=this.value;", True,  , GetLocalResourceObject("nBankDesColumnToolTip"))
		lobjColumn = .AddPossiblesColumn(100513, GetLocalResourceObject("nBk_agencyColumnCaption"), "nBk_agency", "tabtab_bk_age", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , True,  , GetLocalResourceObject("nBk_agencyColumnToolTip"))
		mobjGrid.Columns("nBk_agency").Parameters.Add("nBank_code", Request.Form.Item("nBankDes"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		lobjColumn = .AddPossiblesColumn(100514, GetLocalResourceObject("nTypAccColumnCaption"), "nTypAcc", "table190", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True) '	
		lobjColumn = .AddTextColumn(100520, GetLocalResourceObject("sAccBankDColumnCaption"), "sAccBankD", 20, "",  , GetLocalResourceObject("sAccBankDColumnToolTip"))
		
		'+ Permite continuar si el check está marcado        
		If Request.QueryString.Item("Reload") = "1" Then
			mobjGrid.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	With mobjGrid
		.Codispl = "CO010" 'Request.QueryString("sCodispl")
		.Columns("nTypDev").TypeList = 2
		.Columns("nTypDev").List = "2"
		.Width = 700
		.Height = 340
		.FieldsByRow = 2
		.Top = 120
		.Left = 50
		
		'+ Se habilitan los botones en caso de que se tenga acceso a la transacción.		
		.AddButton = mblnAccess
		.DeleteButton = mblnAccess
	End With
	
	If CStr(Session("CO001_nAction")) = CStr(eCollection.ColformRef.TypeActionsSeqColl.cstrQuery) Then
		mobjGrid.Columns("Sel").GridVisible = False
	Else
		mobjGrid.Columns("Sel").GridVisible = True
	End If
End Sub

'%insCO010Upd. Esta ventana se encarga de mostrar el código correspondiente a la
'---------------------------------------------------------------------------------------
Private Sub insPreCO010Upd()
	'---------------------------------------------------------------------------------------
	With Response
		If Request.QueryString.Item("Action") = "Del" Then
			insDelItem()
			Response.Write(mobjValues.ConfirmDelete())
		End If
		
		.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValCollectionSeq.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(Request.QueryString.Item("Index"))))
		
		'+ Actualiza el monto en moneda local
		If Request.QueryString.Item("Type") = "PopUp" Then
			If Request.QueryString.Item("Action") <> "Del" Then
				.Write("<SCRIPT> insShowLocalAmount() </" & "Script>")
			End If
		End If
		
		If Request.QueryString.Item("Action") = "Update" Then
			
			Response.Write("<SCRIPT> insLockControl(self.document.forms[0].nTypDev); </" & "Script>")
		End If
	End With
End Sub

'%insCO010. Esta ventana se encarga de mostrar el código correspondiente a la
'---------------------------------------------------------------------------------------
Private Sub insPreCO010()
    	'---------------------------------------------------------------------------------------
	Dim lclsCashMovs As eCollection.CashBankAccMovs
	Dim lclsCashMov As Object
        Dim ldblTotals As Double
	lclsCashMovs = New eCollection.CashBankAccMovs
	
	ldblTotals = System.Math.Abs(mdblTotalAmountGen)
	
	mobjGrid.sEditRecordParam = "nTotalRel=" & mobjValues.TypeToString(ldblTotals, eFunctions.Values.eTypeData.etdDouble, True, 0)
	
	If mclsCashMovs.nCount > 0 Then
		Response.Write(mobjValues.HiddenControl("nItems", CStr(mclsCashMovs.Count)))
		For	Each lclsCashMov In mclsCashMovs
			With mobjGrid
				.Columns("sType").DefValue = lclsCashMov.sType
				.Columns("nSequence").DefValue = lclsCashMov.nSequence
				.Columns("nTypDev").DefValue = lclsCashMov.nTypDev
				.Columns("nTypDev").Descript = lclsCashMov.sTypDev
				.Columns("nAccBankO").DefValue = lclsCashMov.nAccBankO
				.Columns("nAccBankO").Descript = lclsCashMov.sAccBankO
				.Columns("nCurrency").DefValue = lclsCashMov.nCurrency
				.Columns("nCurrency").Descript = lclsCashMov.sCurrency
				.Columns("tcnExchange").DefValue = lclsCashMov.nExchange
				.Columns("nAmount").DefValue = lclsCashMov.nAmount
				.Columns("tcnAmountLoc").DefValue = lclsCashMov.nAmountLoc
				.Columns("sClient").DefValue = lclsCashMov.sClient
				.Columns("sClient").Descript = lclsCashMov.sCliename
				.Columns("nBankDes").DefValue = lclsCashMov.nBankDes
				.Columns("nBankDes").Descript = lclsCashMov.sBank
				.Columns("nBk_agency").DefValue = lclsCashMov.nBk_agency
				.Columns("nBk_agency").Descript = lclsCashMov.sBk_agency
				.Columns("nBk_agency").Parameters.Add("nBank_code", lclsCashMov.nBankDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("nTypAcc").DefValue = lclsCashMov.nTypAcc
				.Columns("nTypAcc").Descript = lclsCashMov.sTypAcc
				.Columns("sAccBankD").DefValue = lclsCashMov.sAccBankD
				Response.Write(.doRow)
			End With
		Next lclsCashMov
	End If
	
	Response.Write(mobjGrid.CloseTable())
	
	Response.Write("<SCRIPT>")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotCobDev','" & mobjValues.TypeToString(mdblTotalAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotIn','" & mobjValues.TypeToString(mdblPaidAmount, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
        Response.Write("top.frames['fraHeader'].UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(mdblTotalAmountGen, eFunctions.Values.eTypeData.etdDouble, True, 0) & "');")
	Response.Write("</" & "Script>")
	
	lclsCashMov = Nothing
	mclsCashMovs = Nothing
	
End Sub

'%insDelItem
'------------------------------
Public Sub insDelItem()
	'------------------------------
	Dim lobjCollection As eCollection.CashBankAccMov
	
	lobjCollection = New eCollection.CashBankAccMov
	lobjCollection.DelCashBankAccMov(Session("nBordereaux"), Request.QueryString.Item("sType"), CInt(Request.QueryString.Item("nSequence")), "2") ' 2) Tipo de Devolución 
	lobjCollection = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CO010")

mclsCashMovs = New eCollection.CashBankAccMovs
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.59.53
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CO010"

mobjMenues = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 11.59.53
mobjMenues.sSessionID = Session.SessionID
mobjMenues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "CO010", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"

//% insShowLocalAmount:
//-------------------------------------------------------------------------------------------
function insShowLocalAmount(){
//-------------------------------------------------------------------------------------------
    insDefValues("LocalAmount","nCurrency=" + document.forms[0].nCurrency.value + "&sType=Normal" + "&nAmount=" + document.forms[0].nAmount.value);
}

//% insLockControl: bloquea los controles que dependen del Tipo de pago
//-------------------------------------------------------------------------------------------
function insLockControl(Field, sAction){
//-------------------------------------------------------------------------------------------		
	var lblnAdd = (sAction=='Add'?true:false)
	
	with(self.document.forms[0]){

//+ Se deshabilitan todos los controles, para ser evaluados luego dependiendo del tipo de pago
		nTypDev.disabled = !lblnAdd;
		if (lblnAdd==true){
			nAccBankO.value = 0;

			sClient.value = "";
			UpdateDiv('lblCliename', '');

			sAccBankD.value = "";

			nBankDes.value = 0;
			nBk_agency.value = 0;
			nTypAcc.value = 0;
		}
			
		nAccBankO.disabled = true;
		btnnAccBankO.disabled = true;
		tcnExchange.disable = true;
		tcnAmountLoc.disable = true;
		sClient.disable = false;
		btnsClient.disable = false;

		nBankDes.disabled = true;
		nBk_agency.disabled = true;
		nTypAcc.disabled = true;
		sAccBankD.disabled = true;

//+ Cuenta bancaria origen
		nAccBankO.disabled = (Field.value!=3?true:false);
		btnnAccBankO.disabled=nAccBankO.disabled;
					
//+ Banco destino
		nBankDes.disabled = (Field.value!=3?true:false);

//+ Agencia bancaria
		nBk_agency.disabled = (Field.value!=3 ||
		                       nBankDes.value==""?true:false);
		btnnBk_agency.disabled = nBk_agency.disabled

//+ Tipo de cuenta
		nTypAcc.disabled = (Field.value!=3?true:false);

//+ Número de cuenta bancaria destino.
		sAccBankD.disabled = (Field.value!=3?true:false);
		
	}		
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.WindowsTitle("CO010", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjValues.StyleSheet())
End With
%>
<%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCO010" ACTION="ValCollectionSeq.aspx?Time=1">
<%
Response.Write(mobjValues.ShowWindowsName("CO010", Request.QueryString.Item("sWindowDescript")))
Call insPrevInf()
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCO010Upd()
Else
	Call insPreCO010()
End If
%>      
</FORM>
</BODY>
</HTML>
<%
'+ En caso de que no se permita el acceso a la transacción se envía la validación correspondiente (60254)
If Not mblnAccess Then
	Response.Write("<SCRIPT> alert(""" & mstrAlert & """); </Script>")
End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 11.59.53
Call mobjNetFrameWork.FinishPage("CO010")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




