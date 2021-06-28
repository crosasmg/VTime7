<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.44.07
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'-----------------------------------------------------------------------------
'- Contador de número de registros
Dim mintTotalRecordsCount As Short

'- Contador del número de registros insertados en la página
Dim mlngOptionalBeginProcess As Object

'- Primer y último nombre mostrado en cada página.
Dim lsFirstRecord As Object
Dim lsLastRecord As Object

'- Indica el movimiento a efectuar para la búsqueda de los datos. (Next o Previous)    
Dim lsWay As Object

'- Cantidad máxima de elementos por página.
Const CN_MAXRECORDS As Short = 100

'+ Número de página que se está mostrando
Dim PageNumber As Object

'+ Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean
'-----------------------------------------------------------------------------	

Dim lclsPremium_mo As eCollection.Premium_mo
Dim lcolPremium_mos As eCollection.Premium_mos
Dim lblnGridvisible As Boolean

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCashnumColumnCaption"), "tcnCashnum", 4, CStr(eRemoteDB.Constants.strnull))
		Call .AddDateColumn(40420, GetLocalResourceObject("tcdStatDateColumnCaption"), "tcdStatDate")
		Call .AddNumericColumn(40412, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 4, CStr(0))
		Call .AddTextColumn(0, GetLocalResourceObject("tctContcuotColumnCaption"), "tctContcuot", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(40416, GetLocalResourceObject("tctOrigReceiptColumnCaption"), "tctOrigReceipt", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnBulletinsColumnCaption"), "tcnBulletins", 9, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(0, GetLocalResourceObject("tctWay_payColumnCaption"), "tctWay_pay", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCollectorColumnCaption"), "tctCollector", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddPossiblesColumn(40407, GetLocalResourceObject("cbeMovementColumnCaption"), "cbeMovement", "Table6", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(40413, GetLocalResourceObject("tcnPremiumColumnCaption"), "tcnPremium", 18, CStr(0),  ,  , True, 6)
		Call .AddPossiblesColumn(40408, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType)
		Call .AddPossiblesColumn(40409, GetLocalResourceObject("cbePay_FormColumnCaption"), "cbePay_Form", "Table182", eFunctions.Values.eValuesType.clngComboType)
		Call .AddNumericColumn(40414, GetLocalResourceObject("tcnBordereauxColumnCaption"), "tcnBordereaux", 4, CStr(0))
		Call .AddPossiblesColumn(40410, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType)
		Call .AddPossiblesColumn(40411, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType)
		'+ Descripción del producto
		Call .AddTextColumn(40417, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(40415, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 8, CStr(0))
		Call .AddTextColumn(40418, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(40419, GetLocalResourceObject("tctOfficeInsColumnCaption"), "tctOfficeIns", 30, CStr(eRemoteDB.Constants.strnull))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "COC001"
		.Columns("Sel").GridVisible = False
		.bOnlyForQuery = True
		.DeleteButton = False
		.AddButton = False
	End With
End Sub

'% insPreCOC001: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCOC001()
	'--------------------------------------------------------------------------------------------
	
	'+ Se inicializan las variables si estas no poseen valor.
	mintTotalRecordsCount = 0
	
	If lsFirstRecord = vbNullString Then
		lsFirstRecord = 1
	End If
	
	If lsLastRecord = vbNullString Then
		lsLastRecord = lsFirstRecord + CN_MAXRECORDS - 1
	End If
	
	'+ Se inicializa el número de página mostrado.       
	PageNumber = 1
	
	'+ Según el tipo de movimiento realizado se cargan el primer y el último registro.
	If Request.QueryString.Item("lsWay") = "Next" Then
		lsFirstRecord = CDbl(Request.Form.Item("lsLastRecord")) + 1
		lsLastRecord = lsFirstRecord + CN_MAXRECORDS - 1
	ElseIf Request.QueryString.Item("lsWay") = "Back" Then 
		lsFirstRecord = CDbl(Request.Form.Item("lsFirstRecord")) - CN_MAXRECORDS
		lsLastRecord = CDbl(Request.Form.Item("lsFirstRecord")) - 1
	End If
	
	With Server
		lclsPremium_mo = New eCollection.Premium_mo
		lcolPremium_mos = New eCollection.Premium_mos
	End With
	
	'+ Condición necesaria para determinar la visibilidad de las columnas Recibo original,Compañía de seguros y Sucursal
	'+ para el caso específico de una compañia de corretaje
	If Session("sCompanyType") = eClient.Client.eType.cstrBrokerOrBrokerageFirm Then
		lblnGridvisible = True
	Else
		lblnGridvisible = False
	End If
	
	If lcolPremium_mos.Find_CollecOper(Session("sCompanyType"), mobjValues.StringToDate(Request.QueryString.Item("dInitDate")), mobjValues.StringToDate(Request.QueryString.Item("dEndDate")), mobjValues.StringToType(Request.QueryString.Item("nCashnum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdDouble),  , CInt(lsFirstRecord), CInt(lsLastRecord)) Then
		
		If lcolPremium_mos.Count > 0 Then
			'+ Se obtiene el número del primer elemento de la página.
			If CDbl(Request.QueryString.Item("BeginProcess")) = 1 Or Request.Form.Item("mlngOptionalBeginProcess") = vbNullString Then
				mlngOptionalBeginProcess = 1
			Else
				mlngOptionalBeginProcess = Request.Form.Item("mlngOptionalBeginProcess")
			End If
			Call ShowRecords()
		End If
		
	Else
		mblnDisabledBack = True
		mblnDisabledNext = True
	End If
	Response.Write(mobjGrid.closeTable())
	
	'+ Se incluyen los botones Back y Next en la página.    
	Response.Write(mobjValues.ButtonBackNext( , mblnDisabledBack, mblnDisabledNext))
	
	'+ Se reasignan los valores del ancabezado de la forma
	With Response
		.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdInitDate.value='" & Request.QueryString.Item("dInitDate") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdEndDate.value='" & Request.QueryString.Item("dEndDate") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeOffice.value=" & Request.QueryString.Item("nOffice") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeCurrency.value=" & Request.QueryString.Item("nCurrency") & ";</" & "Script>")
	End With
	lclsPremium_mo = Nothing
	lcolPremium_mos = Nothing
End Sub



'% ShowRecords: Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Private Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintRecordShow As Short
	Dim lintRecordIndex As Short
	Dim lstrChains As String
	
	'+ Estableciendo valores iniciales.    
	lintRecordShow = 0
	lstrChains = ""
	mblnDisabledBack = False
	mblnDisabledNext = False
	
	If Request.QueryString.Item("BeginProcess") = vbNullString Then
		
		'+ Establece el número de página a mostrar.
		If Request.Form.Item("PageNumber") = vbNullString Then
			PageNumber = 0
		Else
			PageNumber = Request.Form.Item("PageNumber")
		End If
	Else
		PageNumber = 0
	End If
	
	'+ Según el tipo de movimiento realizado se establecen las acciones a tomar
	If Request.QueryString.Item("lsWay") = vbNullString Or Request.QueryString.Item("lsWay") = "Next" Then
		PageNumber = PageNumber + 1
	ElseIf Request.QueryString.Item("lsWay") = "Back" Then 
		mlngOptionalBeginProcess = mlngOptionalBeginProcess - (mlngOptionalBeginProcess - lsFirstRecord)
		PageNumber = PageNumber - 1
		
		'+ Si el número de la página es menor a cero, se asume que se encuentra en la primera página.
		If PageNumber <= 0 Then
			PageNumber = 1
		End If
	End If
	lintRecordIndex = 0
	
	For	Each lclsPremium_mo In lcolPremium_mos
		lintRecordIndex = lintRecordIndex + 1
		
		With mobjGrid
			.Columns("tcnCashNum").DefValue = CStr(lclsPremium_mo.nCashNum)
			.Columns("tcdStatDate").DefValue = CStr(lclsPremium_mo.dStatDate)
			.Columns("tcnReceipt").DefValue = CStr(lclsPremium_mo.nReceipt)
			.Columns("tctContcuot").DefValue = lclsPremium_mo.nContrat & "/" & lclsPremium_mo.nDraft
			.Columns("tcnBulletins").DefValue = CStr(lclsPremium_mo.nBulletins)
			.Columns("tctWay_pay").DefValue = lclsPremium_mo.sWay_pay
			.Columns("tctCollector").DefValue = lclsPremium_mo.sCollector
			.Columns("cbeMovement").DefValue = CStr(lclsPremium_mo.nType)
			.Columns("cbeCurrency").DefValue = CStr(lclsPremium_mo.nCurrency)
			.Columns("tcnPremium").DefValue = CStr(lclsPremium_mo.nPremium)
			.Columns("cbePay_Form").DefValue = lclsPremium_mo.sPay_form
			.Columns("tcnBordereaux").DefValue = mobjValues.TypeToString(lclsPremium_mo.nBordereaux, eFunctions.Values.eTypeData.etdDouble)
			.Columns("cbeOffice").DefValue = CStr(lclsPremium_mo.nOffice)
			.Columns("cbeBranch").DefValue = CStr(lclsPremium_mo.nBranch)
			.Columns("tctDescript").DefValue = lclsPremium_mo.sDescript
			.Columns("tcnPolicy").DefValue = CStr(lclsPremium_mo.nPolicy)
			.Columns("tctOrigReceipt").GridVisible = lblnGridvisible
			.Columns("tctOrigReceipt").DefValue = lclsPremium_mo.sOrigReceipt
			.Columns("tctCliename").GridVisible = lblnGridvisible
			.Columns("tctCliename").DefValue = lclsPremium_mo.sClienname
			.Columns("tctOfficeIns").GridVisible = lblnGridvisible
			.Columns("tctOfficeIns").DefValue = lclsPremium_mo.sOfficeIns
			
			'If lintRecordIndex = 1 Then
			'    lstrChains = lclsPremium.mlngRows
			'Else
			'    lstrChains = lstrChains & "," & lclsPremium.mlngRows
			'End If
			
			Response.Write(.DoRow)
		End With
		
		lintRecordShow = lintRecordShow + 1
		
		'+ Incremento del número de registro total.
		mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
		
		'+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
		If lintRecordIndex >= CN_MAXRECORDS Then
			Exit For
		End If
	Next lclsPremium_mo
	
	With mobjValues
		
		Response.Write(.HiddenControl("hddChains", lstrChains))
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>    " & vbCrLf)
Response.Write("    var sChains=""""" & vbCrLf)
Response.Write("    var sChange=""""" & vbCrLf)
Response.Write("    sChains = self.document.forms[0].hddChains.value;" & vbCrLf)
Response.Write("</" & "SCRIPT>        ")

		
		
		'+ Primer registro a cargar    
		Response.Write(.HiddenControl("lsFirstRecord", lsFirstRecord))
		
		'+ Ultimo registro a cargar        
		Response.Write(.HiddenControl("lsLastRecord", lsLastRecord))
		
		'+ Indice que indica el primer item a leer de la lista.
		Response.Write(.HiddenControl("mlngOptionalBeginProcess", mlngOptionalBeginProcess))
		
		'+ Contador de páginas
		Response.Write(.HiddenControl("PageNumber", PageNumber))
		
	End With
	
	'+ Determina si estará activo o no el Botón [<< Anterior]                                    
	If PageNumber <= 1 Then
		mblnDisabledBack = True
	End If
	
	'+ Determina si estará activo o no el Botón [>> Siguiente]
	If (lintRecordShow < CN_MAXRECORDS) Then
		mblnDisabledNext = True
	Else
		If lintRecordShow = CN_MAXRECORDS And mintTotalRecordsCount = CN_MAXRECORDS And mintTotalRecordsCount = lintRecordShow Then
			mblnDisabledNext = True
		End If
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc001")
With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "coc001"
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "coc001"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">



    <%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.setZone(2, "COC001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 20/10/03 12:27 $|$$Author: Nvaplat11 $"


//**% MoveRecord: Performed a submit of the page according to movement's type executed.
//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(lsWay) {
//-------------------------------------------------------------------------------------------
//+Mueve el registro a la página siguiente o anterior, según corresponda
    switch (lsWay){
        case "Next":
			document.forms[0].action = "COC001.aspx?lsWay=Next&nMainAction=401" 
										+ <%="'&dInitDate=" & Request.QueryString.Item("dInitDate") & "'"%>
										+ <%="'&dEndDate=" & Request.QueryString.Item("dEndDate") & "'"%>
										+ <%="'&nOffice=" & Request.QueryString.Item("nOffice") & "'"%>
										+ <%="'&nCurrency=" & Request.QueryString.Item("nCurrency") & "'"%>
										+ <%="'&nCashnum=" & Request.QueryString.Item("nCashnum") & "'"%>		
			break;
      case "Back":
          document.forms[0].action = "COC001.aspx?lsWay=Back&nMainAction=401"
		  								+ <%="'&dInitDate=" & Request.QueryString.Item("dInitDate") & "'"%>
										+ <%="'&dEndDate=" & Request.QueryString.Item("dEndDate") & "'"%>
										+ <%="'&nOffice=" & Request.QueryString.Item("nOffice") & "'"%>
										+ <%="'&nCurrency=" & Request.QueryString.Item("nCurrency") & "'"%>
										+ <%="'&nCashnum=" & Request.QueryString.Item("nCashnum") & "'"%>										
  }
  document.forms[0].submit()
}
		
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="fraContent" ACTION="valCollectionQue.aspx?mode=2">
<TABLE WIDTH="100%">
    <BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("COC001", Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
Call insPreCOC001()%>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%mobjValues = Nothing
mobjGrid = Nothing
%>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




