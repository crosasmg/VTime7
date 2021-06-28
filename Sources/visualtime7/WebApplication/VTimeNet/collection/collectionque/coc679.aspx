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
Dim mintTotalRecordsCount As Integer

'- Contador del número de registros insertados en la página
Dim mlngOptionalBeginProcess As Object

'- Primer y último nombre mostrado en cada página.
Dim lsFirstRecord As Object
Dim lsLastRecord As Object

'- Indica el movimiento a efectuar para la búsqueda de los datos. (Next o Previous)    
Dim lsWay As Object

'- Cantidad máxima de elementos por página.
Const CN_MAXRECORDS As Short = 50

'+ Número de página que se está mostrando
Dim PageNumber As Object

'+ Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean
'-----------------------------------------------------------------------------

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues
Dim lclsPremium As eCollection.Premium
Dim lcolPremiums As eCollection.Premiums


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "COC679"
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 10, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 10, CStr(0))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, CStr(0))
		Call .AddTextColumn(0, GetLocalResourceObject("tctCurrencyColumnCaption"), "tctCurrency", 30, CStr(eRemoteDB.Constants.strnull))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 19, CStr(0),  ,  , True, 6)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnDaysPendColumnCaption"), "tcnDaysPend", 10, CStr(0))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkPrintColumnCaption"), "chkPrint", "", CShort("1"), "1",  , False)
		Call .AddHiddenColumn("hddRow", CStr(0))
		Call .AddHiddenColumn("hddBranch", "")
		Call .AddHiddenColumn("hddProduct", "")
		Call .AddHiddenColumn("hddKey_print", CStr(0))
	End With
	
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "COC679"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCOC679: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreCOC679()
	'--------------------------------------------------------------------------------------------
	Dim sFind As String
	Dim mstrKey As String
	
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
	
	lclsPremium = New eCollection.Premium
	lcolPremiums = New eCollection.Premiums
	
	If Request.QueryString.Item("lsWay") = vbNullString Then
		'+Realiza el proceso completo.
		sFind = "1"
		mstrKey = vbNullString
	Else
		'+Lee solo la tabla temporal ya que los registros estan cargados 
		sFind = "2"
		mstrKey = Request.Form.Item("hddKey")
	End If
	
	If lcolPremiums.FindCOC679(mobjValues.StringToDate(Request.QueryString.Item("dProcess")), True, CInt(lsFirstRecord), CInt(lsLastRecord), sFind, mstrKey) Then
		
		mintTotalRecordsCount = lcolPremiums.mlngCountCOC679
		Response.Write(mobjValues.HiddenControl("hddKey", lcolPremiums.mstrKey))
		
		If lcolPremiums.Count > 0 Then
			
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
	
End Sub

'% ShowRecords: Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Private Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintRecordShow As Integer
	Dim lintRecordIndex As Short
	Dim lstrChains As Object
	
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
	
	For	Each lclsPremium In lcolPremiums
		lintRecordIndex = lintRecordIndex + 1
		
		With mobjGrid
			.Columns("tctBranch").DefValue = lclsPremium.sDesBranch
			.Columns("tctDescript").DefValue = lclsPremium.sDescProd
			
			.Columns("hddBranch").DefValue = CStr(lclsPremium.nBranch)
			.Columns("hddProduct").DefValue = CStr(lclsPremium.nProduct)
			
			.Columns("tcnPolicy").DefValue = CStr(lclsPremium.nPolicy)
			.Columns("tcnCertif").DefValue = CStr(lclsPremium.nCertif)
			.Columns("tcnReceipt").DefValue = CStr(lclsPremium.nReceipt)
			.Columns("tcnDraft").DefValue = CStr(lclsPremium.nDraft)
			.Columns("tctCurrency").DefValue = lclsPremium.sDescCurrency
			.Columns("tcnAmount").DefValue = CStr(lclsPremium.nPremium)
			.Columns("tcnDaysPend").DefValue = CStr(lclsPremium.nDaysPend)
			.Columns("hddRow").DefValue = CStr(lclsPremium.mlngRows)
			.Columns("chkPrint").Checked = CShort(lclsPremium.sCadena)
			.Columns("chkPrint").DefValue = CStr(lclsPremium.mlngRows)
			
			.Columns("chkPrint").OnClick = "sPrint(this," & CStr(lintRecordShow) & ")"
			
			If lintRecordIndex = 1 Then
				lstrChains = lclsPremium.mlngRows
			Else
				lstrChains = lstrChains & "," & lclsPremium.mlngRows
			End If
			
			Response.Write(.DoRow)
		End With
		
		lintRecordShow = lintRecordShow + 1
		
		'+ Incremento del número de registro total.
		mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
		
		'+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
		If lintRecordIndex >= CN_MAXRECORDS Then
			Exit For
		End If
	Next lclsPremium
	
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
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("coc679")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "COC679"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.44.07
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">




    <%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "COC679", Request.QueryString.Item("sWindowDescript"), 6))
%>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 22/10/03 13:10 $|$$Author: Nvaplat11 $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//sPrint: Permite seleccionar los recibos que se van a imprimir.
//------------------------------------------------------------------------------------------
function sPrint(Field,lintIndex){
//------------------------------------------------------------------------------------------
    if(Field.checked){
        sChains = sChains + ", " + Field.value;
        self.document.forms[0].hddChains.value=sChains;
        sChange = "1";
        
		lstrParam =  "sKey="        + self.document.forms[0].hddKey.value   +
		             "&nBranch="     + marrArray[lintIndex].hddBranch  +
				     "&nProduct="    + marrArray[lintIndex].hddProduct +
		             "&nPolicy="     + marrArray[lintIndex].tcnPolicy  +
				     "&nCertif="     + marrArray[lintIndex].tcnCertif  +
		             "&nReceipt="    + marrArray[lintIndex].tcnReceipt +
				     "&nDraft="      + marrArray[lintIndex].tcnDraft   +
				     "&sPrint=1";

		insDefValues('InsPrint', lstrParam);    
        
        
    }else{
        sChains = sChains.replace(Field.value + "," ,"");
        self.document.forms[0].hddChains.value=sChains;
        sChange = "1";
        
		lstrParam =  "sKey="        + self.document.forms[0].hddKey.value   +
		             "&nBranch="     + marrArray[lintIndex].hddBranch  +
				     "&nProduct="    + marrArray[lintIndex].hddProduct +
		             "&nPolicy="     + marrArray[lintIndex].tcnPolicy  +
				     "&nCertif="     + marrArray[lintIndex].tcnCertif  +
		             "&nReceipt="    + marrArray[lintIndex].tcnReceipt +
				     "&nDraft="      + marrArray[lintIndex].tcnDraft   +
				     "&sPrint=2";

		insDefValues('InsPrint', lstrParam);    
        
    }
}

//**% MoveRecord: Performed a submit of the page according to movement's type executed.
//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(lsWay) {
//-------------------------------------------------------------------------------------------
//+Se actualiza la temporal TMP_COC679 de acuerdo a lo seleccionado
    if(sChange=="1"){
        with(self.document.forms[0]){
            insDefValues("Letter", "sKey=" + hddKey.value + 
                                   "&sChains=" + sChains +
                                   "&nFirstRecord=" + lsFirstRecord.value + 
                                   "&nLastRecord=" + lsLastRecord.value);
        }
    }

//+Mueve el registro a la página siguiente o anterior, según corresponda
    switch (lsWay){
        case "Next":
			document.forms[0].action = "COC679.aspx?lsWay=Next&nMainAction=401"
			break;
      case "Back":
          document.forms[0].action = "COC679.aspx?lsWay=Back&nMainAction=401"
  }
  document.forms[0].submit()
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">                                   
<FORM METHOD="POST" ID="FORM" NAME="COC679" ACTION="valCollectionQue.aspx?sMode=2&dProcess="<%=Request.QueryString.Item("dProcess")%>">
    <%Response.Write(mobjValues.ShowWindowsName("COC679", Request.QueryString.Item("sWindowDescript")))%>
    <BR>
<%

Call insDefineHeader()
Call insPreCOC679()

Response.Write(mobjValues.BeginPageButton)

mobjGrid = Nothing
mobjValues = Nothing
lclsPremium = Nothing
lcolPremiums = Nothing
mintTotalRecordsCount = Nothing
%>     
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.44.07
Call mobjNetFrameWork.FinishPage("coc679")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




