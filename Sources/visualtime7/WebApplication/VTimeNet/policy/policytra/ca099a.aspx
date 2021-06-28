<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

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

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
'~End Body Block VisualTimer Utility

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Parametros de transaccion
Dim mstrDocType As String
Dim mstrOperat As String
Dim mstrAction As Object
Dim mstrOrigin As String
Dim mstrBranch As String
Dim mstrProduct As String
Dim mstrEffecdate As String
Dim mstrEffecdate1 As String
Dim mstrBrancht As String
Dim mstrCertif As String
Dim mstrProponum As String
Dim mstrClient As String
Dim mstrStatus As String
Dim mstrIntermed As String
Dim mstrAgency As String
Dim mstrQs As String
Dim mstrTransaction As Object
Dim lclsTConvertionss As ePolicy.TConvertionss
Dim lclsTConvertions As Object
Dim lstrCertype As Object


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		Call .AddHiddenColumn("hddCertype", vbNullString)
		
		If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
			If Request.QueryString.Item("Type") <> "PopUp" Then
				Call .AddAnimatedColumn(0, GetLocalResourceObject("btnQueryColumnCaption"), "btnQuery", "/VTimeNet/images/menu_query.png", GetLocalResourceObject("btnQueryColumnCaption"),  , "insCA001(CurrentIndex,302)", False)
			End If
		Else
			If Request.QueryString.Item("Type") <> "PopUp" Then
				Call .AddAssociateColumn(0, "Consultas asociadas", "btnQuery", 2)
			End If
		End If
		
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEffecdateColumnCaption"), "tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateColumnToolTip"),  ,  ,  , True)
		If (mstrOperat = "3" Or mstrOperat = "4" Or mstrOperat = "6") Then
			Call .AddDateColumn(0, GetLocalResourceObject("tcdStatdateColumnCaption"), "tcdStatdate", "",  , GetLocalResourceObject("tcdStatdateColumnToolTip"),  ,  ,  , False)
		Else
			Call .AddDateColumn(0, GetLocalResourceObject("tcdStatdateColumnCaption"), "tcdStatdate", "",  , GetLocalResourceObject("tcdStatdateColumnToolTip"),  ,  ,  , True)
		End If
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatColumnCaption"), "cbeStat", "table5526", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , True, 5, GetLocalResourceObject("cbeStatColumnToolTip"), True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valNoConversColumnCaption"), "valNoConvers", "TabNoConversBrancht", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insChangeField(this)", mstrOperat = "6",  , GetLocalResourceObject("valNoConversColumnToolTip"))
		
		'+Si el origen es modificacion
		If mstrOrigin = "2" Or mstrOrigin = "" Then
                Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_amendColumnCaption"), "cbeType_amend", "tabtype_amend", eFunctions.Values.eValuesType.clngComboType, , True, , , , , True, 5, GetLocalResourceObject("cbeType_amendColumnCaption"))
			'			Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_amendColumnCaption"),"cbeType_amend","tabtype_amend",eFunctions.Values.eValuesType.clngWindowType,,true,,,,,true,5, GetLocalResourceObject("cbeType_amendColumnCaption"))         
		End If
		
		If mstrDocType = "1" Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnProponumColumnCaption"), "tcnProponum", 10, CStr(0),  , GetLocalResourceObject("tcnProponumColumnToolTip"),  ,  ,  ,  ,  , True)
		Else
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnProponumColumnCaption"), "tcnProponum", 10, CStr(0),  , GetLocalResourceObject("tcnProponumColumnToolTip"),  ,  ,  ,  ,  , True)
		End If
		Call .AddCheckColumn(0, GetLocalResourceObject("chkDoc_pendColumnCaption"), "chkDoc_pend", "",  , "1",  , True)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnServ_orderColumnCaption"), "tcnServ_order", 10, CStr(0),  , GetLocalResourceObject("tcnServ_orderColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("valServ_orderColumnCaption"), "valServ_order", "Table215", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valServ_orderColumnCaption"), eFunctions.Values.eTypeCode.eNumeric)
		
		'+Si el origen es emision
		If mstrOrigin = "1" Or mstrOrigin = vbNullString Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnFirstPremColumnCaption"), "tcnFirstPrem", 18, CStr(0),  , GetLocalResourceObject("tcnFirstPremColumnToolTip"), True, 6,  ,  ,  , True)
			Call .AddHiddenColumn("hddCurrPrem", CStr(0))
			Call .AddTextColumn(0, GetLocalResourceObject("tctCurrPremColumnCaption"), "tctCurrPrem", 30, "",  , GetLocalResourceObject("tctCurrPremColumnCaption"),  ,  ,  , True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnExchangeColumnCaption"), "tcnExchange", 18, CStr(0),  , GetLocalResourceObject("tcnExchangeColumnToolTip"), True, 6,  ,  ,  , True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnOrig_premColumnCaption"), "tcnOrig_prem", 18, CStr(0),  , GetLocalResourceObject("tcnOrig_premColumnToolTip"), True, 6,  ,  ,  , True)
			Call .AddTextColumn(0, GetLocalResourceObject("tctOrig_currColumnCaption"), "tctOrig_curr", 30, "",  , GetLocalResourceObject("tctOrig_currColumnToolTip"),  ,  ,  , True)
			Call .AddCheckColumn(0, GetLocalResourceObject("chkPrem_cheqColumnCaption"), "chkPrem_cheq", "",  , "1",  , True)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnRelationColumnCaption"), "tcnRelation", 5, CStr(0),  , GetLocalResourceObject("tcnRelationColumnToolTip"),  ,  ,  ,  ,  , True)
            Else
                Call .AddHiddenColumn("tcnRelation", vbNullString)
                Call .AddHiddenColumn("tcnFirstPrem", vbNullString)
                Call .AddHiddenColumn("hddCurrPrem", vbNullString)
                Call .AddHiddenColumn("tcnExchange", vbNullString)
                Call .AddHiddenColumn("tcnOrig_prem", vbNullString)
                Call .AddHiddenColumn("tctOrig_curr", vbNullString)
                Call .AddHiddenColumn("chkPrem_cheq", vbNullString)

                
            End If
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, CStr(0),  , GetLocalResourceObject("tcnPolicyColumnToolTip"),  ,  ,  ,  ,  , True)
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cboWaitCodeColumnCaption"), "cboWaitCode", "tabtab_waitpo", eFunctions.Values.eValuesType.clngComboType, "", True)
		
		If (mstrOperat = "3" Or mstrOperat = "4" Or mstrOperat = "6") Then
			'Call .AddNumericColumn(0, GetLocalResourceObject("tcnCollectColumnCaption"),"tcnCollect",18,0,, GetLocalResourceObject("tcnCollectColumnToolTip"),True,6,,,,False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnCollectColumnCaption"), "tcnCollect", 18, CStr(6),  , GetLocalResourceObject("tcnCollectColumnCaption"), True, 6,  ,  ,  , False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnGastMedColumnCaption"), "tcnGastMed", 18, CStr(6),  , GetLocalResourceObject("tcnGastMedColumnToolTip"), True, 6,  ,  ,  , False)
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnGastProvColumnCaption"), "tcnGastProv", 18, CStr(6),  , GetLocalResourceObject("tcnGastProvColumnToolTip"), True, 6,  ,  ,  , False)
		End If
		'+ Solo para emision
		If mstrAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If mstrOrigin = "1" Or mstrOrigin = "" Then
				Call .AddCheckColumn(0, GetLocalResourceObject("chkDevolutColumnCaption"), "chkDevolut", "", CShort("1"), "1", "insChangeField(this)", False)
				Call .AddAnimatedColumn(0, GetLocalResourceObject("btnOrderColumnCaption"), "btnOrder", "/VTimeNet/images/clfolder.png", GetLocalResourceObject("btnOrderColumnToolTip"), "", "insPayOrder(CurrentIndex)", True)
				Call .AddHiddenColumn("hddPay_order", CStr(2))
			End If
		End If
		
		Call .AddTextColumn(0, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 30, "",  , GetLocalResourceObject("tctClienameColumnToolTip"),  ,  ,  , True)
		
		'+ La fecha de inicio de vigencia solo se deshabilita si la operacion no es aprobar, o si el origen no es Emision, Modificacion o Renovacion
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStartdateColumnCaption"), "tcdStartdate", "",  , GetLocalResourceObject("tcdStartdateColumnToolTip"),  ,  , "insChangeDate(this);", (mstrAction = eFunctions.Menues.TypeActions.clngActionQuery) Or (mstrOperat <> "2") Or (mstrOrigin <> "1" And mstrOrigin <> "2" And mstrOrigin <> "3"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirdatColumnCaption"), "tcdExpirdat", "",  , GetLocalResourceObject("tcdExpirdatColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdMaximum_daColumnCaption"), "tcdMaximum_da", "",  , GetLocalResourceObject("tcdMaximum_daColumnToolTip"),  ,  ,  , True)
		'+ indicador de polzia pendiente 
		Call .AddCheckColumn(0, GetLocalResourceObject("chksPenstatus_polColumnCaption"), "chksPenstatus_pol", "",  , "1",  , False)
		
		Call .AddHiddenColumn("hddCertif", vbNullString)
		Call .AddHiddenColumn("hddClient", vbNullString)
		Call .AddHiddenColumn("hddCollect", vbNullString)
		Call .AddHiddenColumn("hddGastMed", vbNullString)
		Call .AddHiddenColumn("hddGastProv", vbNullString)
		Call .AddHiddenColumn("hddExchange", vbNullString)
		Call .AddHiddenColumn("hddStartDate", vbNullString)
		Call .AddHiddenColumn("hddExpirDat", vbNullString)
		Call .AddHiddenColumn("hddOffice", vbNullString)
		Call .AddHiddenColumn("hddOfficeAgen", vbNullString)
        Call .AddHiddenColumn("hddAgency", vbNullString)
        Call .AddHiddenColumn("hddProduct", vbNullString)
        Call .AddHiddenColumn("hddPen_doc", vbNullString)
        Call .AddAnimatedColumn(0, GetLocalResourceObject("btnObservColumnCaption"), "btnObserv", "/VTimeNet/images/menu_query.png", GetLocalResourceObject("btnObservColumnToolTip"), , "insObserv(CurrentIndex,302)", False)
	End With
	
	With mobjGrid
		If mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble) = 401 Then
			.ActionQuery = True
			'	.EditRecordDisabled = True
		End If
		
		If mstrOperat = "8" Then
			.EditRecordDisabled = True
		End If
		
		.Codispl = "CA099A"
		If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
			.Top = 240
			.Left = 240
			.Width = 600
			.Height = 280
		Else
			.Top = 20
			.Left = 140
			.Width = 680
			.Height = 700
		End If
		.Columns("Sel").GridVisible = Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery)
		
		.sEditRecordParam = "' + sQs + '"
		
		.Columns("valNoConvers").Parameters.Add("sBrancht", mstrBrancht, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Columns("valNoConvers").Parameters.ReturnValue("sDevo", False, "", True)
		.Columns("valNoConvers").Parameters.ReturnValue("sDisc", False, "", True)
		
		.Columns("cboWaitCode").Parameters.Add("sBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		'+Si la operacion es rechazar, anular o regularizar
		If mstrAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
			If mstrOrigin = "1" Or mstrOrigin = "" Then
				.Columns("chkDevolut").GridVisible = False
				.Columns("btnOrder").Disabled = False
				.Columns("btnOrder").GridVisible = False
			End If
		End If
		
		.Columns("Sel").GridVisible = mstrAction <> eFunctions.Menues.TypeActions.clngActionQuery
		
		If Not .ActionQuery Then
			.Columns("tcdStartdate").GridVisible = False
			.Columns("tcdExpirdat").GridVisible = False
			.Columns("tcdMaximum_da").GridVisible = False
			.Columns("tcdStatdate").GridVisible = False
			.Columns("tcdMaximum_da").GridVisible = False
		Else
			.Columns("tcdStartdate").GridVisible = True
			.Columns("tcdExpirdat").GridVisible = True
			.Columns("tcdMaximum_da").GridVisible = True
			.Columns("tcdStatdate").GridVisible = True
			.Columns("tcdMaximum_da").GridVisible = True
		End If
		
		If mstrOrigin = "2" Or mstrOrigin = "" Then
                '			If Request.QueryString.Item("Type") = "PopUp" Then
                '.Columns("cbeType_amend").GridVisible = False
                .Columns("cbeType_amend").Parameters.Add("nbranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("cbeType_amend").Parameters.Add("nproduct", mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Columns("cbeType_amend").Parameters.Add("deffecdate", mobjValues.StringToType(Today, eFunctions.Values.eTypeData.etdDate), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                '            End If
		End If
		
            .FieldsByRow = 2
            .AddButton = False
            .DeleteButton = False
            .nMainAction = mobjValues.StringToType(mstrAction, eFunctions.Values.eTypeData.etdDouble)
            .bCheckVisible = mobjValues.StringToType(mstrAction, eFunctions.Values.eTypeData.etdDouble) <> eFunctions.Menues.TypeActions.clngActionQuery
		
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
		
            If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
                .Columns("tcdStatdate").GridVisible = True
                .Columns("valNoConvers").GridVisible = False
                .Columns("chkDoc_pend").GridVisible = False
                .Columns("tcnServ_order").GridVisible = False
                .Columns("valServ_order").GridVisible = False
                .Columns("tcnPolicy").GridVisible = False
                .Columns("cboWaitCode").GridVisible = False
                If (mstrOperat = "3" Or mstrOperat = "4" Or mstrOperat = "6") Then
                    .Columns("tcnCollect").GridVisible = False
                    .Columns("tcnGastMed").GridVisible = False
                    .Columns("tcnGastProv").GridVisible = False
                End If
                If mstrAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
                    If mstrOrigin = "1" Or mstrOrigin = "" Then
                        .Columns("chkDevolut").GridVisible = False
                        .Columns("btnOrder").GridVisible = False
                    End If
                End If
                .Columns("tctCliename").GridVisible = False
                .Columns("tcdStartdate").GridVisible = False
                .Columns("tcdExpirdat").GridVisible = False
                .Columns("tcdMaximum_da").GridVisible = False
                .Columns("btnObserv").GridVisible = False
                .Columns("tcnFirstPrem").GridVisible = False
                .Columns("tctCurrPrem").GridVisible = False
                .Columns("tcnExchange").GridVisible = False
                .Columns("tcnOrig_prem").GridVisible = False
                .Columns("tctOrig_curr").GridVisible = False
                .Columns("chkPrem_cheq").GridVisible = False
                .Columns("tcnRelation").GridVisible = False
            End If
		
        End With
End Sub

'%insPreMDP037: Esta función se encarga de cargar los datos en la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreCA099()
	'--------------------------------------------------------------------------------------------
	
	Dim sFind As String
	Dim mstrKey As Object
	
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
	
	If Request.QueryString.Item("lsWay") = vbNullString Then
		'+Realiza el proceso completo.
		sFind = "1"
	Else
		'+Lee solo la tabla temporal ya que los registros estan cargados 
		sFind = "2"
	End If
	
	'- Se define la variable para la carga del Grid de la ventana 
	lclsTConvertionss = New ePolicy.TConvertionss
	If CStr(session("sKey")) <> "" Then
		mstrKey = session("sKey")
	Else
		mstrKey = lclsTConvertionss.CreTconvertions(mobjValues.StringToType(mstrBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mstrOrigin, eFunctions.Values.eTypeData.etdDouble), mstrDocType, mobjValues.StringToType(session("nPolicy"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mstrCertif, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mstrProponum, eFunctions.Values.eTypeData.etdDouble, True), mstrClient, mobjValues.StringToType(mstrStatus, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mstrIntermed, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mstrAgency, eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(mstrEffecdate1, eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sExpired"), mobjValues.StringToType(Request.QueryString.Item("nOperat"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nWaitCode"), eFunctions.Values.eTypeData.etdDouble), CInt(lsFirstRecord), CInt(lsLastRecord), session("nUsercode"), session("sCodispl"), Request.QueryString.Item("sCodispl_orig"), mobjValues.StringToType(Request.QueryString.Item("dLastdate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sApplyCostFP"), mobjValues.StringToType(Request.Form.Item("tcnCollect"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnGastMed"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnGastProv"), eFunctions.Values.eTypeData.etdDouble))
		
		session("sKey") = mstrKey
	End If
	
	If lclsTConvertionss.Find(mstrKey, CInt(lsFirstRecord), CInt(lsLastRecord)) Then
		
		mintTotalRecordsCount = lclsTConvertionss.Count
		
		If lclsTConvertionss.Count > 0 Then
			
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
	mobjValues.ActionQuery = False
	
	Response.Write(mobjValues.ButtonBackNext( , mblnDisabledBack, mintTotalRecordsCount <> 50))
	
	lclsTConvertionss = Nothing
	lclsTConvertions = Nothing
End Sub

'% insPreCA099Upd. Se define esta funcion para contruir el contenido de la ventana UPD 
'------------------------------------------------------------------------------------------------------------------
Private Sub insPreCA099Upd()
	'------------------------------------------------------------------------------------------------------------------		
	
	With Request
		If Request.QueryString.Item("nMainAction") = "302" Or Request.QueryString.Item("nMainAction") = "401" Then
			mobjValues.ActionQuery = False
		Else
			mobjValues.ActionQuery = True
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicyTra.aspx", "CA099A", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	If mstrOrigin = "2" Or mstrOrigin = "" Then
		Response.Write("<SCRIPT>showdescript();</" & "Script>")
	End If
	
	If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
		Response.Write("<SCRIPT>insHideFields();</" & "Script>")
	End If
End Sub

'% ShowRecords: Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Private Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintRecordShow As Short
	Dim lintRecordIndex As Short
	Dim lstrChains As String
	Dim lclsGeneral As eGeneral.Exchange
	Dim lstrQueryString As Object
	
	lclsGeneral = New eGeneral.Exchange
	
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
	
	For	Each lclsTConvertions In lclsTConvertionss
		With mobjGrid
			'+Poliza solo se requiere si Origen no es emision
			If mstrOrigin <> "1" Then
				.Columns("tcnPolicy").DefValue = lclsTConvertions.nProponum
			End If
			'+Solo se muestra en modo consulta
                '.Columns("tcnProponum").HRefScript = "EditRecord(" & CStr(lintCount) & ",mlngAction.clngActionQuery, 'Update','" & .sEditRecordParam & "')"
                
                .sEditRecordParam = "' + sQs + '" & "&nProduct=" & lclsTConvertions.nProduct & "&dEffecdate=" & lclsTConvertions.dEffecdate
                .Columns("tcnProponum").HRefScript = "EditRecord(" & CStr(lintRecordIndex) & ",nMainAction, 'Update','" & .sEditRecordParam & "')"
			
			.Columns("Sel").Disabled = (lclsTConvertions.nStatus <> 1 And lclsTConvertions.nStatus <> 3 And lclsTConvertions.nStatus <> 4)
			
			.Columns("hddCertype").DefValue = lclsTConvertions.sCertype
			If lintRecordIndex = 0 Then
				Response.Write(mobjValues.HiddenControl("hddScertype_aux", lclsTConvertions.sCertype))
			End If
			.Columns("tcnProponum").DefValue = lclsTConvertions.nPolicy
			.Columns("hddCertif").DefValue = lclsTConvertions.nCertif
                .Columns("chkDoc_pend").DefValue = lclsTConvertions.sPen_doc
                .Columns("hddPen_doc").DefValue = lclsTConvertions.sPen_doc
			.Columns("tcdEffecdate").DefValue = lclsTConvertions.dEffecdate
			.Columns("cbeStat").DefValue = lclsTConvertions.nStatus
			.Columns("cbeStat").Descript = lclsTConvertions.sStatus
                .Columns("hddProduct").DefValue = lclsTConvertions.nProduct
                If mstrOrigin = "2" Or mstrOrigin = "" Then
                    ' If Request.QueryString.Item("Type") = "PopUp" Then
                    '.Columns("cbeType_amend").GridVisible = False

                    .Columns("cbeType_amend").Parameters("nbranch").Value = lclsTConvertions.nBranch
                    .Columns("cbeType_amend").Parameters("nproduct").Value = lclsTConvertions.nProduct
                    .Columns("cbeType_amend").Parameters("deffecdate").Value = lclsTConvertions.dEffecdate
                    'End If
                End If
                
                '                .sEditRecordParam = mstrQs & "&nProduct='+" & lclsTConvertions.nProduct & "'"
                If (mstrOperat = "3" Or mstrOperat = "4" Or mstrOperat = "6") Then
                    .Columns("tcdStatdate").DefValue = CStr(Today)
                Else
                    .Columns("tcdStatdate").DefValue = lclsTConvertions.dStat_date
                End If
			
                If mstrOperat = "6" Then
                    '+Para regularizar la causa de no conversion será 34-Por regularizacion    			
                    .Columns("valNoConvers").DefValue = "34"
                    .Columns("valNoConvers").Disabled = True
                Else
                    .Columns("valNoConvers").DefValue = lclsTConvertions.nNo_convers
                End If
			
                .Columns("valNoConvers").Descript = lclsTConvertions.sCon_descript
			
                .Columns("cboWaitCode").DefValue = lclsTConvertions.nWait_code
			
                .Columns("tcdStartdate").DefValue = lclsTConvertions.dDate_init
                .Columns("hddStartDate").DefValue = mobjValues.TypeToString(lclsTConvertions.dDate_init, eFunctions.Values.eTypeData.etdDate)
                .Columns("tcdExpirdat").DefValue = lclsTConvertions.dExpirdat
                .Columns("hddExpirdat").DefValue = mobjValues.TypeToString(lclsTConvertions.dExpirdat, eFunctions.Values.eTypeData.etdDate)
                .Columns("tcdMaximum_da").DefValue = lclsTConvertions.dLimit_date
                .Columns("tcnServ_order").DefValue = lclsTConvertions.nServ_order
			
                .Columns("valServ_order").DefValue = lclsTConvertions.nStatus_ord
                .Columns("valServ_order").Descript = lclsTConvertions.sStatus_ord
                If lclsTConvertions.nNum_doc = 1 Then
                    .Columns("Sel").Checked = CShort("1")
                Else
                    .Columns("Sel").Checked = CShort("2")
                End If
                If mstrOrigin = "1" Or mstrOrigin = "" Then
                    .Columns("tcnFirstPrem").DefValue = lclsTConvertions.nFirst_prem
                    .Columns("tctCurrPrem").DefValue = lclsTConvertions.sPrem_currDesc
                    .Columns("hddCurrPrem").DefValue = lclsTConvertions.nPrem_curr
                    .Columns("tcnExchange").DefValue = lclsTConvertions.nExchange
                    .Columns("tcnOrig_prem").DefValue = lclsTConvertions.nOrig_prem
                    .Columns("tctOrig_curr").DefValue = lclsTConvertions.sOrig_curr
                    .Columns("chkPrem_cheq").Checked = mobjValues.StringToType(lclsTConvertions.sPrem_che, eFunctions.Values.eTypeData.etdInteger)
                    .Columns("tcnRelation").DefValue = lclsTConvertions.nBordereaux
                Else
                    .Columns("tcnRelation").DefValue = lclsTConvertions.nBordereaux
                End If
			
                If mstrAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
                    If mstrOrigin = "1" Or mstrOrigin = "" Then
                        .Columns("chkDevolut").DefValue = lclsTConvertions.sDevolut
                        .Columns("chkDevolut").OnClick = "insCheckPayOrder(this," & CStr(lintRecordIndex) & ")"
                        .Columns("btnOrder").HRefScript = "insPayOrder(" & CStr(lintRecordIndex) & ")"
                        .Columns("hddPay_order").DefValue = lclsTConvertions.sPay_order
                    End If
                End If
			
                If (mstrOperat = "3" Or mstrOperat = "4" Or mstrOperat = "6") Then
                    .Columns("tcnCollect").DefValue = lclsTConvertions.nExpenses
                    .Columns("hddCollect").DefValue = lclsTConvertions.nExpenses
                    .Columns("tcnGastMed").DefValue = lclsTConvertions.nHealthexp
                    .Columns("hddGastMed").DefValue = lclsTConvertions.nHealthexp
                    .Columns("tcnGastProv").DefValue = lclsTConvertions.nRoutine
                    .Columns("hddGastProv").DefValue = lclsTConvertions.nRoutine
                    If mstrOperat <> "4" Then
                        Call lclsGeneral.Find(lclsTConvertions.nPrem_curr, lclsTConvertions.dDate_init, True)
                        .Columns("hddExchange").DefValue = CStr(lclsGeneral.nExchange)
                    Else
                        .Columns("hddExchange").DefValue = lclsTConvertions.nExchange
                    End If
                Else
                    .Columns("hddExchange").DefValue = lclsTConvertions.nExchange
                End If
			
                .Columns("tctCliename").DefValue = lclsTConvertions.sCliename
                .Columns("hddClient").DefValue = lclsTConvertions.sclient
			
                If mstrOrigin = "2" Or mstrOrigin = "" Then
                    .Columns("cbeType_amend").DefValue = lclsTConvertions.nType_amend
                    .Columns("cbeType_amend").Descript = lclsTConvertions.sType_amend
                End If
			
                .Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintRecordIndex) & ")"
			
                If Request.QueryString.Item("sCodispl_orig") = "CA099C" Then
                    If CDbl(Request.QueryString.Item("nOperat")) = 5 Then
                        mstrTransaction = 4
                    Else
                        mstrTransaction = 10
                    End If
				
                    .Columns("btnQuery").HRefScript = "insCA001(" & CStr(lintRecordIndex) & "," & mstrTransaction & ")"
                Else
                    Select Case lclsTConvertions.sCertype
                        Case "1"
                            mstrTransaction = "11"
                        Case "3"
                            mstrTransaction = "10"
                        Case "4"
                            mstrTransaction = "39"
                        Case "5"
                            mstrTransaction = "41"
                        Case "6"
                            mstrTransaction = "40"
                        Case "7"
                            mstrTransaction = "42"
                        Case "8"
                            mstrTransaction = "11"
                    End Select
				
                    .Columns("btnQuery").sQueryString = "sCertype='+ marrArray[" & CStr(lintRecordIndex) & "].hddCertype +'" & "!sCodisplOrig=CAC001" & "!nBranch=" & mstrBranch & "!nProduct='+ marrArray[" & CStr(lintRecordIndex) & "].hddProduct +'" & "!nPolicy='+ marrArray[" & CStr(lintRecordIndex) & "].tcnPolicy +'" & "!nProponum='+ marrArray[" & CStr(lintRecordIndex) & "].tcnProponum +'" & "!nCertif='+ marrArray[" & CStr(lintRecordIndex) & "].hddCertif +'" & "!dEffecdate=" & mstrEffecdate & "!dStartdate='+ marrArray[" & CStr(lintRecordIndex) & "].tcdEffecdate +'" & "!LoadWithAction=" & Request.QueryString.Item("nMainAction") & "!nTransaction=" & mstrTransaction
                End If
			
			
                .Columns("chksPenstatus_pol").Checked = lclsTConvertions.sPenstatus_pol
                .Columns("chksPenstatus_pol").Disabled = True
			
			
                .Columns("btnObserv").HRefScript = "insObserv(" & CStr(lintRecordIndex) & ",401)"
			
                lstrCertype = lclsTConvertions.sCertype
                .Columns("tcnPolicy").DefValue = mobjValues.StringToType(lclsTConvertions.nPol_quot, eFunctions.Values.eTypeData.etdDouble)
			
                .Columns("hddOffice").DefValue = lclsTConvertions.nOffice
                .Columns("hddOfficeAgen").DefValue = lclsTConvertions.nOfficeAgen
                .Columns("hddAgency").DefValue = lclsTConvertions.nAgency
			
                Response.Write(.DoRow)
                lintRecordIndex = lintRecordIndex + 1
            End With
		lintRecordShow = lintRecordShow + 1
		
		'+ Incremento del número de registro total.
		mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
		
		'+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
		If lintRecordIndex >= CN_MAXRECORDS Then
			Exit For
		End If
	Next lclsTConvertions
	
	lclsGeneral = Nothing
	
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
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = session.SessionID
mobjNetFrameWork.nUsercode = session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca099a")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = session.SessionID
mobjValues.nUsercode = session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca099a"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = session.SessionID
mobjMenu.nUsercode = session("nUsercode")
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjGrid.sSessionID = session.SessionID
mobjGrid.nUsercode = session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "ca099a"
Call mobjGrid.SetWindowParameters(Request.QueryString.Item(","), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

'+ Se asignan variables de parametro
With Request
	mstrDocType = .QueryString.Item("sTypeDoc")
	mstrOperat = .QueryString.Item("nOperat")
	mstrAction = .QueryString.Item("nMainAction")
	mstrOrigin = .QueryString.Item("nOrigin")
	mstrBranch = .QueryString.Item("nBranch")
	mstrProduct = .QueryString.Item("nProduct")
	If .QueryString.Item("dateCont") = "2" Then
		mstrEffecdate1 = .QueryString.Item("dEffecdate")
	End If
	mstrEffecdate = .QueryString.Item("dEffecdate")
	mstrBrancht = .QueryString.Item("sBrancht")
	mstrCertif = .QueryString.Item("nCertif")
	mstrProponum = .QueryString.Item("nProponum")
	mstrClient = .QueryString.Item("sClient")
	mstrStatus = .QueryString.Item("nStatus")
	mstrIntermed = .QueryString.Item("nIntermed")
	mstrAgency = .QueryString.Item("nAgency")
End With

'+ Se crea cadena de parametros     
    mstrQs = "nBranch=" & mstrBranch & "&dEffecdate1=" & mstrEffecdate & "&nOrigin=" & mstrOrigin & "&sTypeDoc=" & mstrDocType & "&sExpired=" & Request.QueryString.Item("sExpired") & "&sCodispl_orig=" & Request.QueryString.Item("sCodispl_orig") & "&nOperat=" & mstrOperat & "&sBrancht=" & mstrBrancht

If Request.QueryString.Item("dStartdate") = vbNullString Then
	mstrQs = mstrQs & "&dStartdate=" & mstrEffecdate
Else
	mstrQs = mstrQs & "&dStartdate=" & Request.QueryString.Item("dStartdate")
End If

mobjValues.ActionQuery = mobjValues.StringToType(mstrAction, eFunctions.Values.eTypeData.etdDouble)

'+ Se crea cadena con parametros basicos tomados desde pagina principal
Response.Write("<SCRIPT>var sQs = '" & mstrQs & "';</SCRIPT>")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>




	<% Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA099A", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>top.frames['fraSequence'].plngMainAction = '" & mstrAction & "'</SCRIPT>")
	Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
'+ Funciones usadas solo en ventana popup   
	    'If Request.QueryString.Item("Type") = "PopUp" Then
	%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 14-08-09 11:18 $|$$Author: Mpalleres $"
//%insHideFields: Oculta los campos de la popUP
//-------------------------------------------------------------------------------------------
function insHideFields(){
//-------------------------------------------------------------------------------------------

	with(self.document.forms[0]){	
        document.getElementsByTagName("TD")[10].style.display='none'
        document.getElementsByTagName("TD")[11].style.display='none'
        document.getElementsByTagName("TD")[12].style.display='none'    
        document.getElementsByTagName("TD")[13].style.display='none'
        document.getElementsByTagName("TD")[14].style.display='none'
        document.getElementsByTagName("TD")[15].style.display='none'
        document.getElementsByTagName("TD")[16].style.display='none'    
        document.getElementsByTagName("TD")[17].style.display='none'
        document.getElementsByTagName("TD")[18].style.display='none'
        document.getElementsByTagName("TD")[19].style.display='none'
        document.getElementsByTagName("TD")[20].style.display='none'                            
        document.getElementsByTagName("TD")[21].style.display='none'
        document.getElementsByTagName("TD")[22].style.display='none'
        document.getElementsByTagName("TD")[23].style.display='none'
        document.getElementsByTagName("TD")[24].style.display='none'                        
        document.getElementsByTagName("TD")[25].style.display='none'
        document.getElementsByTagName("TD")[26].style.display='none'
        document.getElementsByTagName("TD")[27].style.display='none'
        document.getElementsByTagName("TD")[28].style.display='none'    
        document.getElementsByTagName("TD")[29].style.display='none'
        document.getElementsByTagName("TD")[30].style.display='none'                            
        document.getElementsByTagName("TD")[31].style.display='none'
        document.getElementsByTagName("TD")[32].style.display='none'
        document.getElementsByTagName("TD")[33].style.display='none'
        document.getElementsByTagName("TD")[36].style.display='none'
        document.getElementsByTagName("TD")[37].style.display='none'
        document.getElementsByTagName("TD")[38].style.display='none'    
        document.getElementsByTagName("TD")[39].style.display='none'
        document.getElementsByTagName("TD")[40].style.display='none'                        
        document.getElementsByTagName("TD")[41].style.display='none'
        document.getElementsByTagName("TD")[42].style.display='none'
        document.getElementsByTagName("TD")[43].style.display='none'
        document.getElementsByTagName("TD")[44].style.display='none'                        
        document.getElementsByTagName("TD")[45].style.display='none'
        document.getElementsByTagName("TD")[46].style.display='none'
        document.getElementsByTagName("TD")[47].style.display='none'
        document.getElementsByTagName("TD")[48].style.display='none'    
        document.getElementsByTagName("TD")[49].style.display='none'
        document.getElementsByTagName("TD")[50].style.display='none'                            

	}
}

//%insChangeField: Función que maneja los estados de los controles
//------------------------------------------------------------------------------------------------------
function insChangeField(Field){
//------------------------------------------------------------------------------------------------------
    var lstrOperat = '<%=mstrOperat%>'
    var lerrCatch
    var lstrParam  = new String();
    
    with(self.document.forms[0])
    {
        switch(Field.name)
        {
            case 'chkDevolut':
            {
                btnOrder.disabled = !((lstrOperat=='3'  ||
		                               lstrOperat=='4') &&
		                              (chkDevolut.checked=='1'));
                break;
			}
            case 'valNoConvers':
            {
//+ Se marca la devolucion segun indicador de causa
//+ Se controla error porque en algunos casos no se crea campo chkDevolut
                try
                {
					chkDevolut.checked=(valNoConvers_sDevo.value == '1');
					insChangeField(chkDevolut);
                }
                catch(lerrCatch){}

				if (lstrOperat != '1')
				{
					if (lstrOperat == '3' || lstrOperat == '4' || lstrOperat == '6')
					{
						//+ Se indica monto segun indicador de causa
						if (valNoConvers.value == '')
						{
							tcnCollect.value = VTFormat((0),'', '', '',6,true);
							tcnGastProv.value = VTFormat((0),'', '', '',6,true);
							tcnGastMed.value = VTFormat((0),'', '', '',6,true);
						}
						else
						{
							if (hddGastMed.value != '')
								{ tcnGastMed.value = VTFormat((hddGastMed.value),'', '', '',6,true); }
							if (hddCollect.value != '')
								{ tcnCollect.value = VTFormat((hddCollect.value),'', '', '',6,true); }
							if (hddGastProv.value != '')
								{ tcnGastProv.value = VTFormat((hddGastProv.value),'', '', '',6,true); }
						}
//						if(typeof(tcnCollect)!='undefined')
//						{
//							if(valNoConvers_sDisc.value=='1')
//							{
//								tcnCollect.value=hddCollect.value
//								tcnGastProv.value=hddGastProv.value
//								tcnGastMed.value=hddGastMed.value
//							}
//							else
//							{
//								if (hddCollect.value < '1')
//								tcnCollect.value='0';
//								tcnGastProv.value= '0';
//								tcnGastMed.value='0';
//							}
//						}
					lstrParam = sQs + "&nNoConvers="  + valNoConvers.value +
						              "&sCertype="    + hddCertype.value +
							          "&nCertif="     + hddCertif.value +
								      "&nPolicy=" + tcnProponum.value +
                                      "&nProduct=" + hddProduct.value +
                                      "&dEffecdate=" + tcdEffecdate.value +
								      "&dStat_date="  + tcdStatdate.value +
								      "&dLimit_date=" + tcdMaximum_da.value +
								      "&dDate_init="  + tcdEffecdate.value;
                    insDefValues('CallExpenses',lstrParam,'/VTimeNet/Policy/PolicyTra');
					}
				}
				break;
			}
		}
	}
}

//% insPayOrder: Muestra ventana de ordenes de pago
//-------------------------------------------------------------------------------------------
function insPayOrder(nIndex){
//-------------------------------------------------------------------------------------------
    var lstrParam = new String();
    var nCollect=0;
    var nGast=0;

//+ Se definen parametros a usar en llamada  
    with (self.document.forms[0]){ 
       	if((typeof(tcnCollect)!='undefined')&&
    	   (tcnCollect.value != '')){
			//nGast = VTFormat(((insConvertNumber(tcnCollect.value) + insConvertNumber(tcnGastProv.value) + insConvertNumber(tcnGastMed.value)) / insConvertNumber(hddExchange.value)),'', '', '',6,true);
			nGast = VTFormat(insConvertNumber(tcnCollect.value) + insConvertNumber(tcnGastProv.value) + insConvertNumber(tcnGastMed.value),'', '', '',6,true);
			if (insConvertNumber(tcnFirstPrem.value) > insConvertNumber(nGast))
			    {
			    nCollect = VTFormat((insConvertNumber(tcnFirstPrem.value) - insConvertNumber(nGast)),'', '', '',6,true);
                lstrParam = sQs + "&sCertype="    + hddCertype.value +
			                      "&nPolicy=" + tcnProponum.value +
                                  "&nProduct=" + hddProduct.value +
                                  "&dEffecdate=" + tcdEffecdate.value +
			                      "&nCertif="     + hddCertif.value + 
			                      "&nOffice="     + hddOffice.value +
			                      "&nOfficeAgen=" + hddOfficeAgen.value +
			                      "&nAgency="     + hddAgency.value +
			                      "&nMainAction=<%=mstrAction%>" + 
			                      "&nCurrency="   + hddCurrPrem.value +
			                      "&nConcept=19"  + 
			                      "&nAmount="     + nCollect +
			                      "&sCodisplOri=CA099A&sForm=OP06-2" + 
			                      "&sClient="     + hddClient.value;
                }
		}
		else{
			nCollect = VTFormat(insConvertNumber(tcnFirstPrem.value),'', '', '',6,true);
			lstrParam = sQs + "&sCertype="   + hddCertype.value +
			                  "&nPolicy=" + tcnProponum.value +
                              "&nProduct=" + hddProduct.value +
                              "&dEffecdate=" + tcdEffecdate.value +
			                  "&nCertif="    + hddCertif.value + 
			                  "&nOffice="     + hddOffice.value +
			                  "&nOfficeAgen=" + hddOfficeAgen.value +
			                  "&nAgency="     + hddAgency.value +
			                  "&nMainAction=<%=mstrAction%>" + 
			                  "&nCurrency="  + hddCurrPrem.value +
			                  "&nConcept=19" + 
			                  "&nAmount=" + nCollect +
			                  "&sCodisplOri=CA099A&sForm=OP06-2" + 
			                  "&sClient=" + hddClient.value;
		}
        
		if(hddPay_order.value==1)
			alert('Err. 55145: <%=eFunctions.Values.GetMessage(55145)%>');
		else
			if(nCollect<=0)
				alert('Err. 55146: <%=eFunctions.Values.GetMessage(55146)%>');
			else
				insDefValues('CallPayOrder', lstrParam);
	}
}

//% strToDtm: Transforma un string de tipo fecha en objeto de fecha
//---------------------------------------------------------------------------------
function strToDtm(sDate){
//---------------------------------------------------------------------------------
    var ldtmRet 
    
    ldtmRet = new Date(sDate.substr(6,4), sDate.substr(3,2) - 1, sDate.substr(0,2))
    return ldtmRet
}

//% dtmToStr: Cambia fecha a cadena
//---------------------------------------------------------------------------------
function dtmToStr(dDate){
//---------------------------------------------------------------------------------
    var lstrRet = new String();
    var lintDay
    var lintMonth
    
    lintDay = dDate.getDate()
    lintMonth = dDate.getMonth() + 1
    
    if (lintDay < 10)
    {
        lstrRet += '0'
    }
    
    lstrRet += lintDay + '/'
    
    if (lintMonth < 10)
    {
        lstrRet += '0'
    }
    lstrRet += lintMonth + '/' + dDate.getFullYear();
    
    return lstrRet
}

//% insChangeDate: Cambia fecha de fin de vigencia si cambia el inicio
//---------------------------------------------------------------------------------
function insChangeDate(oField){
//---------------------------------------------------------------------------------
    var ldtmExpir;
    var ldtmNewStart;
    var ldtmOldStart;
    var lintDiff
    var lstrOrigin = '<%=mstrOrigin%>'

//+Solo se actualiza fecha de expiracion si el origne es emision    
    if((lstrOrigin=='1')&&(self.document.forms[0].hddExpirDat.value!=''))
        with(self.document.forms[0])
        {
            ldtmNewStart = strToDtm(tcdStartdate.value);
            ldtmOldStart = strToDtm(hddStartDate.value);
            ldtmExpir    = strToDtm(hddExpirDat.value);

//+Se obtiene la diferencia en milisegundos de las fechas
            lintDiff     = ldtmNewStart - ldtmOldStart;

//+Se agregan los milisegundos a la fecha nueva
            ldtmExpir    = new Date(lintDiff + Date.UTC(ldtmExpir.getFullYear(), ldtmExpir.getMonth(), ldtmExpir.getDate()) + (24 * 60 * 60 * 1000));

            tcdExpirdat.value = dtmToStr(ldtmExpir);
        }
}
//%	showdescript: 
//-------------------------------------------------------------------------------------------
function showdescript(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
	}
}
</SCRIPT>
<%	
	'+ Funciones usadas solo en grilla
    'Else
	%>
<SCRIPT>
    var mlngAction = new TypeActions();

//% insCheckSelClick: Pasa a transaccion correspondiente al seleccionar una de las opciones
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrParam      = new String();
    var lstrOperat     = '<%=mstrOperat%>';
    var lstrDocType    = '<%=mstrDocType%>';
    var lstrClientCode = '<%=mstrClient%>';
	var lstrUsercode   = '<%=session("nUsercode")%>'
	var lstrBordereaux = "";
	var lstrFirst_prem = "";
	var lstrPrem_curr  = "";
	var lstrPolicy     = "";
    
//+ Si operacion no es '5 Actualizar ni 8 Recepción de propuesta'   
    if (lstrOperat != "5" && lstrOperat != "8" )
    {   
//+Si se desmarca, se elimina
		if (!Field.checked)
		{
			with (self.document.forms[0])
			{
		        lstrParam = sQs + "&sCertype="   + marrArray[lintIndex].hddCertype +
                                  "&nPolicy=" + marrArray[lintIndex].tcnProponum +
                                  "&nProduct=" + marrArray[lintIndex].hddProduct +
                                  "&dEffecdate=" + marrArray[lintIndex].tcdEffecdate +
                                  "&nCertif="    + marrArray[lintIndex].hddCertif;
		    }
		    insDefValues('DelTConvertions', lstrParam);
		}
//+Si se marca se actualiza		
		else
		{
			with (self.document.forms [0])
		        lstrParam = sQs + "&sCertype=" + marrArray[lintIndex].hddCertype;
			    lstrParam = sQs + "&nProduct=" + <%=mstrProduct%>;

			if (lstrOperat != "2" && lstrOperat != "7" )
			{
				EditRecord(lintIndex,nMainAction,"Update",lstrParam);
				Field.checked = false;
			}
			else
			{
				with (self.document.forms [0])
				{
					if(typeof(marrArray[lintIndex].tcnRelation)=='undefined')
						lstrBordereaux = ''
					else
						lstrBordereaux = marrArray[lintIndex].tcnRelation;
						
					if(typeof(marrArray[lintIndex].tcnFirstPrem)=='undefined')
						lstrFirst_prem = ''
					else
						lstrFirst_prem = marrArray[lintIndex].tcnFirstPrem;
						
					if(typeof(marrArray[lintIndex].hddCurrPrem)=='undefined')
						lstrPrem_curr = ''
					else
						lstrPrem_curr = marrArray[lintIndex].hddCurrPrem;
						
					if(typeof(marrArray[lintIndex].tcnPolicy)=='undefined')
					{
						lstrPolicy = '';
					}
					else
						lstrPolicy = marrArray[lintIndex].tcnPolicy;
					
					lstrParam = sQs + "&sCertype="     + marrArray[lintIndex].hddCertype    +
                                      "&nPolicy=" + marrArray[lintIndex].tcnProponum +
                                      "&nProduct=" + marrArray[lintIndex].hddProduct +
                                      "&dEffecdate=" + marrArray[lintIndex].tcdEffecdate +
                                      "&sPen_doc=" + marrArray[lintIndex].hddPen_doc + 
									  "&nCertif="      + marrArray[lintIndex].hddCertif     +
									  "&nProponum="    + lstrPolicy                         +
									  "&dDate_init="   + marrArray[lintIndex].tcdEffecdate  +
									  "&nStatus="      + marrArray[lintIndex].cbeStat       +
									  "&dStat_date="   + marrArray[lintIndex].tcdStatdate   +
									  "&nNoConvers="   + marrArray[lintIndex].valNoConvers  +
									  "&dExpirdat="    + marrArray[lintIndex].tcdExpirdat   + 
									  "&dLimit_date="  + marrArray[lintIndex].tcdMaximum_da +
									  "&sObserv=" + ''                                      + 
									  "&nServ_order="  + marrArray[lintIndex].tcnServ_order +
									  "&nStatus_ord="  + marrArray[lintIndex].valServ_order +
									  "&nBordereaux=" + marrArray[lintIndex].tcnRelation +
									  "&nFirst_prem="  + lstrFirst_prem                     +
									  "&nPrem_curr="   + lstrPrem_curr                      +
									  "&sPrem_che="    + ''                                 +
									  "&sPay_order="   + ''                                 +
									  "&sDevolut="     + ''                                 +
									  "&sClient=" + marrArray[lintIndex].hddClient + 
									  "&nUsercode="    + lstrUsercode                       +
									  "&nWait_code="   + marrArray[lintIndex].cboWaitCode	+ 
									  "&sCodispl=CA099A"									+
									  "&nMainAction="  + nMainAction						+
									  "&WindowType=PopUp"									+
									  "&sClickCheck=1";
				}
				insDefValues('InsTConvertions', lstrParam);	
			}
		}
	}
	else
	{	 
//+ La operación no es 8 Recepción de propuesta 
        if (lstrOperat != "8" )
        {	 
	        if (Field.checked)
	        {
	            lstrParam = sQs + "&sCertype=" + marrArray[lintIndex].hddCertype +
	                              "&nPolicy=" + marrArray[lintIndex].tcnProponum +
                                  "&nProduct=" + marrArray[lintIndex].hddProduct +
                                  "&dEffecdate=" + marrArray[lintIndex].tcdEffecdate +
	                              "&nCertif="  + marrArray[lintIndex].hddCertif +
			                      "&LoadWithAction=" + "<%=mstrAction%>";
//+ Cotizacion de poliza o certificado
	            if(lstrDocType=='1')
	            {
	                if(marrArray[lintIndex].hddCertif == 0 ||
	                   marrArray[lintIndex].hddCertif == '')
	                    lstrParam += "&nTransaction=4"
	                else
	                    lstrParam += "&nTransaction=5";
	            }
//+Propuesta de poliza o certificado
	            else
	            {
	                if(marrArray[lintIndex].hddCertif == 0 ||
	                   marrArray[lintIndex].hddCertif == '')
	                    lstrParam += "&nTransaction=6"
	                else
	                    lstrParam += "&nTransaction=7";
	            }

		        ShowPopUp('/VTimeNet/common/GoTo.aspx?sCodispl=CA001_K&sCodisplOri=CA099A&' + lstrParam);
		    }
		}
//+ La operación es 8 Recepción de propuesta 
		else
		{
			if (Field.checked)
			{				
				with (self.document.forms [0])
				{
					if(typeof(marrArray[lintIndex].tcnRelation)=='undefined')
						lstrBordereaux = ''
					else
						lstrBordereaux = marrArray[lintIndex].tcnRelation;
							
					if(typeof(marrArray[lintIndex].tcnFirstPrem)=='undefined')
						lstrFirst_prem = ''
					else
						lstrFirst_prem = marrArray[lintIndex].tcnFirstPrem;
							
					if(typeof(marrArray[lintIndex].hddCurrPrem)=='undefined')
						lstrPrem_curr = ''
					else
						lstrPrem_curr = marrArray[lintIndex].hddCurrPrem;
							
					if(typeof(marrArray[lintIndex].tcnPolicy)=='undefined')
					{
						lstrPolicy = '';
					}
					else
						lstrPolicy = marrArray[lintIndex].tcnPolicy;
						
					lstrParam = sQs + "&sCertype="     + marrArray[lintIndex].hddCertype    +
				                      "&nPolicy=" + marrArray[lintIndex].tcnProponum +
                                      "&nProduct=" + marrArray[lintIndex].hddProduct +
                                      "&dEffecdate=" + marrArray[lintIndex].tcdEffecdate +
									  "&nCertif="      + marrArray[lintIndex].hddCertif     +
									  "&nProponum="    + lstrPolicy                         +
									  "&dDate_init="   + marrArray[lintIndex].tcdEffecdate  +
									  "&nStatus="      + marrArray[lintIndex].cbeStat       +
									  "&dStat_date="   + marrArray[lintIndex].tcdStatdate   +
									  "&nNoConvers="   + marrArray[lintIndex].valNoConvers  +
									  "&dExpirdat="    + marrArray[lintIndex].tcdExpirdat   + 
									  "&dLimit_date="  + marrArray[lintIndex].tcdMaximum_da +
									  "&sObserv=" + ''                                      + 
									  "&nServ_order="  + marrArray[lintIndex].tcnServ_order +
									  "&nStatus_ord="  + marrArray[lintIndex].valServ_order + 
									  "&nBordereaux="  + lstrBordereaux                     +
									  "&nFirst_prem="  + lstrFirst_prem                     +
									  "&nPrem_curr="   + lstrPrem_curr                      +
									  "&sPrem_che="    + ''                                 +
									  "&sPay_order="   + ''                                 +
									  "&sDevolut="     + ''                                 +
									  "&sClient="      + lstrClientCode                     + 
									  "&nUsercode="    + lstrUsercode                       +
									  "&nWait_code="   + marrArray[lintIndex].cboWaitCode ;
				}
				insDefValues('InsTConvertions', lstrParam);	
			}
		}  										
	}
}
//**% MoveRecord: Performed a submit of the page according to movement's type executed.
//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(lsWay) {
//-------------------------------------------------------------------------------------------

//+Mueve el registro a la página siguiente o anterior, según corresponda
    switch (lsWay){
        case "Next":            
		    document.forms[0].action = "CA099A.aspx?lsWay=Next&nMainAction=<%=mstrAction%>" +"&"+ sQs
			break;
      case "Back":
            document.forms[0].action = "CA099A.aspx?lsWay=Back&nMainAction=<%=mstrAction%>" + "&" + sQs
  }
  document.forms[0].submit()
}

</SCRIPT>
<%      'End If%>
<SCRIPT>
//% insObserv: Muestra ventana de observacion 
//-------------------------------------------------------------------------------------------
function insObserv(nIndex,nAction){
//-------------------------------------------------------------------------------------------
    var lstrParam = new String();
    var larrData = new Array();
    var lerrCatch;
    var lstrMainAction
    var lstrAction    

	mstrMainAction = '<%=Request.QueryString.Item("Action")%>'
//+ Se asigna arreglo
//+ Cuando no es popup el arreglo esta en la misma ventana 
//+ Cuando es popup el arreglo esta an la ventana que abrio a la popup
    try
    {
        larrData = marrArray;
    }
    catch(lerrCatch)
    {
        larrData = top.opener.marrArray;
    }   

//+ Se definen parametros a usar en llamada    
    lstrParam = sQs + "&sCertype="   + larrData[nIndex].hddCertype +
                      "&nPolicy=" + larrData[nIndex].tcnProponum +
                      "&nProduct=" + larrData[nIndex].hddProduct +
                      "&dEffecdate=" + larrData[nIndex].tcdEffecdate +
                      "&nCertif="    + larrData[nIndex].hddCertif;

    ShowPopUp('/VTimeNet/Policy/PolicySeq/CA748.aspx?sCodispl=SCA2-808&sOnSeq=2&nMainAction=' + nAction + '&' + lstrParam);
}
//% insCA001: Muestra ventana de observacion 
//-------------------------------------------------------------------------------------------
function insCA001(nIndex,nTransac){
//-------------------------------------------------------------------------------------------
    var lstrParam = new String();
    var larrData = new Array();
    var lerrCatch;
    var lstrMainAction
    var lstrAction    

	mstrMainAction = '<%=Request.QueryString.Item("Action")%>'
//+ Se asigna arreglo
//+ Cuando no es popup el arreglo esta en la misma ventana 
//+ Cuando es popup el arreglo esta an la ventana que abrio a la popup
    try
    {
        larrData = marrArray;
    }
    catch(lerrCatch)
    {
        larrData = top.opener.marrArray;
    }   

//+ Se definen parametros a usar en llamada    
    lstrParam = sQs + "&sCertype="   + larrData[nIndex].hddCertype +
                      "&nPolicy=" + larrData[nIndex].tcnProponum +
                      "&nProduct=" + larrData[nIndex].hddProduct +
                      "&dEffecdate=" + larrData[nIndex].tcdEffecdate +
                      "&sTransaction=" + nTransac +
                      "&sCodispl_orig=CA099C" +	
                      "&nCertif="    + larrData[nIndex].hddCertif;
                      
    ShowPopUp('/VTimeNet/common/GoTo.aspx?sCodispl=CA001C&nMainAction=401' + lstrParam, 'Policyseq', 650, 650, 'yes', 'yes',10,10);

}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmPropoOperat" ACTION="valPolicyTra.aspx?sTime=1&<%=mstrQs%>">
<%
Response.Write(mobjValues.ShowWindowsName("CA099A", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreCA099()
Else
	Call insPreCA099Upd()
End If
lclsTConvertionss = Nothing
lclsTConvertions = Nothing
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca099a")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





