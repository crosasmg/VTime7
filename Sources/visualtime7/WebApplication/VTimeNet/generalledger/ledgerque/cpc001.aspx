<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim lclsLedgerAcc As eLedge.LedgerAcc
Dim lcolLedgerAcc As Microsoft.VisualBasic.Collection

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
Const CN_MAXRECORDS As Short = 100

'+ Número de página que se está mostrando
Dim PageNumber As Object

'+ Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean


'% insDefineHeader: Define las columnas del Grid
'-----------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "CPC001"
	
	'+ Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddTextColumn(0, GetLocalResourceObject("tctAccountColumnCaption"), "tctAccount", 20, "",  , GetLocalResourceObject("tctAccountColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctAux_accounColumnCaption"), "tctAux_accoun", 10, "",  , GetLocalResourceObject("tctAux_accounColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 50, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
		
		If mobjValues.StringToType(Request.QueryString.Item("nTypeQuery"), eFunctions.Values.eTypeData.etdDouble) = 5 Then
			Call .AddTextColumn(0, GetLocalResourceObject("tctLastMoveColumnCaption"), "tctLastMove", 10, "",  , GetLocalResourceObject("tctLastMoveColumnToolTip"))
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(0),  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		End If
	End With
	
	With mobjGrid
		.DeleteButton = False
		.AddButton = False
		.Top = 70
		.Codispl = "CPC001"
		.Width = 330
		.Height = 400
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPreCPC001: Carga los datos en le grid de la forma "Folder" 
'---------------------------------------------------------------
Private Sub insPreCPC001()
	'---------------------------------------------------------------
	
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
	
	lclsLedgerAcc = New eLedge.LedgerAcc
	
	lcolLedgerAcc = lclsLedgerAcc.Full_LevelCatalog(mobjValues.StringToType(Request.QueryString.Item("nLed_compan"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nlevelQuant"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nTypeQuery"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CShort(lsFirstRecord), CShort(lsLastRecord))
	
	mintTotalRecordsCount = lcolLedgerAcc.Count
	
	If lcolLedgerAcc.Count > 0 Then
		'+ Se obtiene el número del primer elemento de la página.
		If CDbl(Request.QueryString.Item("BeginProcess")) = 1 Or Request.Form.Item("mlngOptionalBeginProcess") = vbNullString Then
			mlngOptionalBeginProcess = 1
		Else
			mlngOptionalBeginProcess = Request.Form.Item("mlngOptionalBeginProcess")
		End If
		
		'+ Se procede a mostrar los registros encontrados.                
		Call ShowRecords()
	End If
	
	'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
	Response.Write(mobjGrid.CloseTable())
	
	Response.Write("<BR>")
	
	'+ Se incluyen los botones Back y Next en la página.    
	Response.Write(mobjValues.ButtonBackNext( , mblnDisabledBack, mblnDisabledNext))
	
	lclsLedgerAcc = Nothing
End Sub

'% ShowRecords: Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Private Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintRecordShow As Integer
	
	Dim lintRecordIndex As Short
	
	'+ Estableciendo valores iniciales.    
	lintRecordShow = 0
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
	
	For	Each lclsLedgerAcc In lcolLedgerAcc
		lintRecordIndex = lintRecordIndex + 1
		With mobjGrid
			If Request.QueryString.Item("sIndent") = "1" Then
				.Columns("tctAccount").DefValue = lclsLedgerAcc.ValIndentation(lclsLedgerAcc.sAccount)
			Else
				.Columns("tctAccount").DefValue = lclsLedgerAcc.sAccount
			End If
			.Columns("tctAux_Accoun").DefValue = lclsLedgerAcc.sAux_Accoun
			.Columns("tctDescript").DefValue = lclsLedgerAcc.sDescript
			
			'Si nTypeQuery=5 muestra fecha ultimo asiento
			If mobjValues.StringToType(Request.QueryString.Item("nTypeQuery"), eFunctions.Values.eTypeData.etdDouble) = 5 Then
				
				If lclsLedgerAcc.dLastDate = eRemoteDB.Constants.dtmNull Then
					.Columns("tctLastMove").DefValue = CStr(eRemoteDB.Constants.StrNull)
				Else
					.Columns("tctLastMove").DefValue = CStr(lclsLedgerAcc.dLastDate)
				End If
			Else
				.Columns("tcnAmount").DefValue = CStr(lclsLedgerAcc.nAmount)
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
	Next lclsLedgerAcc
	
	With mobjValues
		
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
		If lintRecordShow < CN_MAXRECORDS And mintTotalRecordsCount = CN_MAXRECORDS And mintTotalRecordsCount = lintRecordShow Then
			mblnDisabledNext = True
		End If
	End If
	
End Sub

</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CPC001"

%>
<SCRIPT>
//+ Esta línea guarda la versión procedente de VSS 
//-------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 14:59 $" 
//-------------------------------------------------------------------------------------------

//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(lsWay) {
//-------------------------------------------------------------------------------------------
    with (document.forms[0])
    {
		action = 'CPC001.aspx?lsWay=' + lsWay + '&nMainAction=401' +
				 '&nLed_Compan=<%=Request.QueryString.Item("nLed_Compan")%>' +
				 '&nLevelQuant=<%=Request.QueryString.Item("nLevelQuant")%>' +
				 '&nTypeQuery=<%=Request.QueryString.Item("nTypeQuery")%>' +
				 '&dEffecdate=<%=Request.QueryString.Item("dEffecdate")%>' + 
				 '&sCodispl=<%=Request.QueryString.Item("sCodispl")%>';
		submit();
	}
	
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




    <%With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "CPC001", "CPC001.aspx"))
		mobjMenu = Nothing
	End If
End With%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="post" ID="FORM" NAME="CPC001" ACTION="valLedgerQue.aspx?x=1">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
Response.Write("<BR><BR>")

Call insDefineHeader()
Call insPreCPC001()

Response.Write(mobjValues.BeginPageButton)

mobjGrid = Nothing
mobjValues = Nothing
%>
	</FORM>
</BODY>
</HTML>





