<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'-****Variables para el manejo de paginacion*****
    
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
Const CN_MAXRECORDS As Short = 10

'+ Número de página que se está mostrando
Dim PageNumber As Object

'+ Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean
    
'-****fin de bloque*****
    
'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página.
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú.
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo particular de los datos de la página.
Dim mclsTar_Apv As eBranches.Tar_Apv
Dim mcolTar_Apvs As eBranches.Tar_Apvs

'% insDefineHeader: Se definen las propiedades del grid.
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid.
	
	With mobjGrid.Columns
		'+ Estructura del GRID modificada debido a cambios en el funcional de la transacción - ACM - 06/08/2003
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_initColumnCaption"), "tcnAge_init", 4, vbNullString,  , GetLocalResourceObject("tcnAge_initColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAge_endColumnCaption"), "tcnAge_end", 4, vbNullString,  , GetLocalResourceObject("tcnAge_endColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_initColumnCaption"), "tcnCapital_init", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_initColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_endColumnCaption"), "tcnCapital_end", 18, vbNullString,  , GetLocalResourceObject("tcnCapital_endColumnToolTip"), True, 6,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicy_year_iniColumnCaption"), "tcnPolicy_year_ini", 5, vbNullString,  , GetLocalResourceObject("tcnPolicy_year_iniColumnToolTip"),  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicy_year_endColumnCaption"), "tcnPolicy_year_end", 5, vbNullString,  , GetLocalResourceObject("tcnPolicy_year_endColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSexColumnCaption"), "cbeSex", "Table18", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeSexColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnRateColumnCaption"), "tcnRate", 16, vbNullString,  , GetLocalResourceObject("tcnRateColumnToolTip"), True, 12)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFix_CostColumnCaption"), "tcnFix_Cost", 16, vbNullString,  , GetLocalResourceObject("tcnFix_CostColumnCaption"), True, 12)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnType_tarColumnCaption"), "tcnType_tar", "Table5584", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnType_tarColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeCalcTypeColumnCaption"), "cbeCalcType", "Table5660", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeCalcTypeColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOptionColumnCaption"), "cbeOption", "Table5519", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeOptionColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkSmokingColumnCaption"), "chkSmoking", "",  ,  ,  , Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeRiskColumnCaption"), "cbeTypeRisk", "Table5639", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeTypeRiskColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid.
	
	With mobjGrid
		.Codispl = "MVI7000"
		.Codisp = "MVI7000"
		.sCodisplPage = "MVI7000"
		.ActionQuery = mobjValues.ActionQuery
		.Top = 100
		.Height = 510
		.Width = 400
		.Columns("tcnAge_init").EditRecord = True
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
		
		.sEditRecordParam = "nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "&nModulec=" & Request.QueryString.Item("nModulec") & "&nCover=" & Request.QueryString.Item("nCover") & "&nRole=" & Request.QueryString.Item("nRole")
		
		.sDelRecordParam = .sEditRecordParam & "&nAge_init=' + marrArray[lintIndex].tcnAge_init + '&nAge_end=' + marrArray[lintIndex].tcnAge_end + ' &nCapital_init=' + marrArray[lintIndex].tcnCapital_init + ' &nCapital_end=' + marrArray[lintIndex].tcnCapital_end + " & "'&nType_calc=' +  marrArray[lintIndex].cbeCalcType + '&nSex=' + marrArray[lintIndex].cbeSex + '&nCurrency=' + marrArray[lintIndex].cbeCurrency + " & "'&nPolicy_year_ini=' + marrArray[lintIndex].tcnPolicy_year_ini + '&nPolicy_year_end=' +  marrArray[lintIndex].tcnPolicy_year_end + " & "'&nOption=' + marrArray[lintIndex].cbeOption + '&sSmoking=' +  marrArray[lintIndex].chkSmoking + '&nTypeRisk=' +  marrArray[lintIndex].cbeTypeRisk + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
		.Columns("tcnType_Tar").BlankPosition = False
	End With
End Sub

'% insPreMVI7000: Se realiza el manejo del grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI7000()
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
        
	If mcolTar_Apvs.Find(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), CInt(lsFirstRecord), CInt(lsLastRecord)) Then
            
		If mcolTar_Apvs.Count > 0 Then
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
		.Write("<SCRIPT>top.fraHeader.document.forms[0].cbeBranch.value='" & Request.QueryString.Item("nBranch") & "';</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].valProduct.value='" & Request.QueryString.Item("nProduct") & "';</" & "Script>")
		'.Write("<SCRIPT>top.fraHeader.document.forms[0].tcdEffecdate.value=" & Request.QueryString.Item("dEffecdate") & ";</" & "Script>")
		.Write("<SCRIPT>top.fraHeader.document.forms[0].valModulec.value=" & Request.QueryString.Item("nModulec") & ";</" & "Script>")
        .Write("<SCRIPT>top.fraHeader.document.forms[0].valCover.value=" & Request.QueryString.Item("nCover") & ";</" & "Script>")
        .Write("<SCRIPT>top.fraHeader.document.forms[0].valRole.value=" & Request.QueryString.Item("nRole") & ";</" & "Script>")
	End With

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
	
	For	Each mclsTar_Apv In mcolTar_Apvs
		lintRecordIndex = lintRecordIndex + 1
		
		With mobjGrid

			.Columns("tcnAge_init").DefValue = mclsTar_Apv.nAge_init
			.Columns("tcnAge_end").DefValue = mclsTar_Apv.nAge_end
			.Columns("tcnCapital_init").DefValue = mclsTar_Apv.nCapital_init
			.Columns("tcnCapital_end").DefValue = mclsTar_Apv.nCapital_end
			.Columns("tcnRate").DefValue = mclsTar_Apv.nRate
			.Columns("tcnFix_Cost").DefValue = mclsTar_Apv.nFix_Cost

			.Columns("tcnType_tar").DefValue = mclsTar_Apv.nType_tar
            .Columns("tcnType_tar").Descript = mclsTar_Apv.sType_tar

			'+ Campos nuevos añadidos a la tabla TAR_APV correspondientes a los cambios de APV2 - ACM - 06/08/2003

			.Columns("cbeCalcType").DefValue = mclsTar_Apv.nType_calc
            .Columns("cbeCalcType").Descript = mclsTar_Apv.sType_calc

			.Columns("cbeSex").DefValue = mclsTar_Apv.nSex
            .Columns("cbeSex").Descript = mclsTar_Apv.sSexClien
                    
			.Columns("cbeCurrency").DefValue = mclsTar_Apv.nCurrency
            .Columns("cbeCurrency").Descript = mclsTar_Apv.sCurrency
                    
			.Columns("tcnPolicy_year_ini").DefValue = mclsTar_Apv.nPolicy_year_ini
			.Columns("tcnPolicy_year_end").DefValue = mclsTar_Apv.nPolicy_year_end
				
            If mclsTar_Apv.nOption > 0 Then
                .Columns("cbeOption").DefValue = mclsTar_Apv.nOption
                .Columns("cbeOption").Descript = mclsTar_Apv.sOption
            End If
                
            .Columns("chkSmoking").Checked = mclsTar_Apv.sSmoking
                    
			.Columns("cbeTypeRisk").DefValue = mclsTar_Apv.nTyperisk
            .Columns("cbeTypeRisk").Descript = mclsTar_Apv.sTyperisk

			Response.Write(.DoRow)
		End With
		
		lintRecordShow = lintRecordShow + 1
		
		'+ Incremento del número de registro total.
		mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
		
		'+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
		If lintRecordIndex >= CN_MAXRECORDS Then
			Exit For
		End If
	Next mclsTar_Apv
	
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
		If lintRecordShow = CN_MAXRECORDS And mintTotalRecordsCount = CN_MAXRECORDS And mintTotalRecordsCount = lintRecordShow Then
			mblnDisabledNext = True
		End If
	End If
End Sub
    

'% insPreMVI7000Upd: Se realiza el manejo de la ventana PopUp asociada al grid.
'--------------------------------------------------------------------------------------------
Private Sub insPreMVI7000Upd()
	'--------------------------------------------------------------------------------------------
        Dim sSmoking As String
        
        With Request
            If .QueryString.Item("Action") = "Del" Then
                Response.Write(mobjValues.ConfirmDelete())
			
                If .QueryString.Item("sSmoking") Is Nothing Or .QueryString.Item("sSmoking") = "false" Then
                    sSmoking = "2"
                Else
                    sSmoking = "1"
                End If
			
                Call mclsTar_Apv.insPostMVI7000(.QueryString.Item("Action"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nRole"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAge_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nAge_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCapital_end"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nType_calc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nSex"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nPolicy_year_ini"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(.QueryString.Item("nOption"), eFunctions.Values.eTypeData.etdDouble), sSmoking, mobjValues.StringToType(.QueryString.Item("nTypeRisk"), eFunctions.Values.eTypeData.etdDouble))
			
			
            End If
		
            Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantNoTraLife.aspx", "MVI7000", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
        End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mclsTar_Apv = New eBranches.Tar_Apv
mcolTar_Apvs = New eBranches.Tar_Apvs

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVI7000"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 3 $|$$Date: 15/10/03 16:10 $|$$Author: Nvaplat61 $"

//**% MoveRecord: Performed a submit of the page according to movement's type executed.
//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(lsWay) {
//-------------------------------------------------------------------------------------------
//+Mueve el registro a la página siguiente o anterior, según corresponda
    switch (lsWay){
        case "Next":
			document.forms[0].action = "mvi7000.aspx?lsWay=Next&nMainAction=302" 
										+ <%="'&nBranch=" & Request.QueryString.Item("nBranch") & "'"%>
										+ <%="'&nProduct=" & Request.QueryString.Item("nProduct") & "'"%>
										+ <%="'&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "'"%>
										+ <%="'&nModulec=" & Request.QueryString.Item("nModulec") & "'"%>
										+ <%="'&nCover=" & Request.QueryString.Item("nCover") & "'"%>		
                                        + <%="'&nRole=" & Request.QueryString.Item("nRole") & "'"%>		
			break;
      case "Back":
          document.forms[0].action = "mvi7000.aspx?lsWay=Back&nMainAction=302"
										+ <%="'&nBranch=" & Request.QueryString.Item("nBranch") & "'"%>
										+ <%="'&nProduct=" & Request.QueryString.Item("nProduct") & "'"%>
										+ <%="'&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "'"%>
										+ <%="'&nModulec=" & Request.QueryString.Item("nModulec") & "'"%>
										+ <%="'&nCover=" & Request.QueryString.Item("nCover") & "'"%>		
                                        + <%="'&nRole=" & Request.QueryString.Item("nRole") & "'"%>		
  }
  document.forms[0].submit()
}

</SCRIPT>

<%
Response.Write(mobjValues.StyleSheet())

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "MVI7000", "MVI7000.aspx"))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI7000" ACTION="valMantNoTraLife.aspx?sMode=2">

<%Response.Write(mobjValues.ShowWindowsName("MVI7000"))
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreMVI7000Upd()
Else
	Call insPreMVI7000()
End If

    mobjGrid = Nothing
mobjMenu = Nothing
mobjValues = Nothing
mclsTar_Apv = Nothing
mcolTar_Apvs = Nothing
%>
</FORM> 
</BODY>
</HTML>





