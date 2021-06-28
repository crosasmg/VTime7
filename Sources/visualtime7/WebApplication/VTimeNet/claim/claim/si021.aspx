<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.34.12
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim mintTransacio As Object


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	'- Se definen las columnas del grid
	With mobjGrid.Columns
		'- Orden     
		Call .AddNumericColumn(0, "Orden de servicio", "tcnOrderServ", 10, CStr(0),  , "Número de la orden de servicio de la línea en tratamiento",  ,  ,  ,  ,  , True)
		'- Profesional
		If Request.QueryString("Type") = "PopUp" Then
			Call .AddPossiblesColumn(0, "Profesional", "tcCProvider", "tabTab_provider", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "changeDisabled()",  ,  , "Código del profesional a cargo de las órdenes de servicio")
			With mobjGrid.Columns("tcCProvider").Parameters
				.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nTypeProv", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
		Else
			Call .AddTextColumn(0, "Profesional", "tctProvider", 45, CStr(eRemoteDB.Constants.strnull),  , "Código del profesional a cargo de las órdenes de servicio",  ,  ,  , True)
		End If
		'- Sucursal
		Call .AddTextColumn(0, "Sucursal", "tctOffice", 35, CStr(eRemoteDB.Constants.strnull),  , "Sucursal a la que pertenece el profesional, asignado a la orden de servicio",  ,  ,  , True)
		'- Siniestro
		Call .AddNumericColumn(0, "Siniestro", "tcnClaim", 10, CStr(0),  , "Número de siniestro al que pertenece la orden de servicio",  ,  ,  ,  ,  , True)
		'- Caso
		Call .AddTextColumn(0, "Caso", "tctCase", 35, CStr(eRemoteDB.Constants.strnull),  , "Número del caso al que pertenece la orden de servicio",  ,  ,  , True)
		'- Descripcion Ramo
		Call .AddTextColumn(0, "Ramo", "tctBranch", 30, CStr(eRemoteDB.Constants.strnull),  , "Ramo al que pertenece la póliza, propuesta o siniestro",  ,  ,  , True)
		'- Descripcion producto
		Call .AddTextColumn(0, "Producto", "tctProduct", 30, CStr(eRemoteDB.Constants.strnull),  , "Producto al que pertenece la póliza, propuesta o siniestro",  ,  ,  , True)
		'- Póliza	
		Call .AddNumericColumn(0, "Póliza", "tcnPolicy", 10, CStr(0),  , "Número de la póliza a la que pertenece la orden de servicio",  ,  ,  ,  ,  , True)
		'- Propuesta
		Call .AddNumericColumn(0, "Propuesta", "tcnProponum", 10, CStr(0),  , "Número de la propuesta a la que pertenece la orden de servicio",  ,  ,  ,  ,  , True)
		'- Certificado
		Call .AddNumericColumn(0, "Certificado", "tcnCertif", 10, CStr(0),  , "Número identificativo del certificado al que pertenece la orden de servicio",  ,  ,  ,  ,  , True)
		'- Fecha planificada
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		Call .AddDateColumn(0, "Fecha planificada", "tcdFec_prog", CStr(Today),  , "Fecha en que se ha planificado la realización de la orden de servicio",  ,  ,  , True)
		'- Hora Planificada
		Call .AddTextColumn(0, "Hora planificada", "tctTime_prog", 5, CStr(eRemoteDB.Constants.strnull),  , "Hora en que se ha planificado la realización de la orden de servicio",  ,  ,  , True)
		'- Fecha realizada
		Call .AddDateColumn(0, "Fecha realizada", "tcdMade_date",  , True, "Fecha en que ha sido realizada la orden de servicio")
		'- Hora realizada
		Call .AddTextColumn(0, "Hora realizada", "tctMade_time", 5, CStr(eRemoteDB.Constants.strnull),  , "Hora en que ha sido realizada la orden de servicio",  ,  , "insFormatHours(this)")
		If Request.QueryString("Type") <> "PopUp" Then
			'- Ajustar provision
			Call .AddCheckColumn(0, "Ajustar provisión", "chkAjReserv", "", 0, CStr(0),  , True)
			'- Pagar
			Call .AddCheckColumn(0, "Pagar", "chkPayment", "", 0, CStr(0),  , True)
		End If
		'- Tipo de inspección (Descripcion)
		Call .AddTextColumn(0, "Tipo de inspección", "tctOrderType", 30, CStr(eRemoteDB.Constants.strnull),  , "Tipo de inspección asociado a la orden de servicio",  ,  ,  , True)
		'- Estado (Descripcion)
		Call .AddPossiblesColumn(0, "Estado", "cbeStatus_ord", "Table215", eFunctions.Values.eValuesType.clngComboType)
		'Campos ocultos/auxiliares
		.AddHiddenColumn("tcnauxCertif", CStr(eRemoteDB.Constants.strnull)) 'Codigo del certificado
		.AddHiddenColumn("tcnauxPolicy", CStr(eRemoteDB.Constants.strnull)) 'Codigo de la poliza
		.AddHiddenColumn("tcnauxClaim", CStr(eRemoteDB.Constants.strnull)) 'Codigo del siniestro
		.AddHiddenColumn("tcnauxBranch", CStr(eRemoteDB.Constants.strnull)) 'Codigo del ramo
		.AddHiddenColumn("tcnauxProduct", CStr(eRemoteDB.Constants.strnull)) 'Codigo del producto
		.AddHiddenColumn("tcnProvider", CStr(eRemoteDB.Constants.strnull)) 'Codigo del profesional
		.AddHiddenColumn("tcnOffice", CStr(eRemoteDB.Constants.strnull)) 'Codigo de la officina
		.AddHiddenColumn("tcnOrderType", CStr(eRemoteDB.Constants.strnull)) 'Codigo del tipo de orden
		.AddHiddenColumn("tcnStatus_ord", CStr(eRemoteDB.Constants.strnull)) 'codigo estado
		.AddHiddenColumn("tcnNumCase", CStr(eRemoteDB.Constants.strnull)) 'Numero de caso
		.AddHiddenColumn("tctStaReserve", CStr(eRemoteDB.Constants.strnull)) 'Estado de la Reserva del Caso
		.AddHiddenColumn("tctStaclaim", CStr(eRemoteDB.Constants.strnull)) 'Estado del siniestro
		'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
		.AddHiddenColumn("tcdDateDone", CStr(Today)) 'Fecha de pago de la orden
		.AddHiddenColumn("hddClient", CStr(eRemoteDB.Constants.strnull)) 'Codigo del cliente
		.AddHiddenColumn("hddCurrency", CStr(eRemoteDB.Constants.strnull)) 'Codigo de la moneda
		.AddHiddenColumn("hddnOrdClass", CStr(eRemoteDB.Constants.strnull)) 'Origen de la orden
		.AddHiddenColumn("hddnStatus_ord", CStr(eRemoteDB.Constants.strnull)) 'Estado de la orden
		.AddHiddenColumn("hddnClaim", CStr(eRemoteDB.Constants.strnull)) 'Numero de siniestro
		.AddHiddenColumn("hddsAjReserv", "0") 'Ajustar reserva
		.AddHiddenColumn("hddsPayment", "0") 'Pagar
		.AddHiddenColumn("hddnTypDemand", CStr(eRemoteDB.Constants.strnull)) 'tipo de demandante
		.AddHiddenColumn("hddnClient_Demand", CStr(eRemoteDB.Constants.strnull)) 'codigo del cliente demandante
		.AddHiddenColumn("hddnTransac", CStr(eRemoteDB.Constants.strnull)) 'Transaccion
		.AddHiddenColumn("hddnOrderServ", CStr(eRemoteDB.Constants.strnull)) 'numero de orden de servicio
		.AddHiddenColumn("hddnTypeProcess", CStr(eRemoteDB.Constants.strnull)) 'Indica si se va a realizar una order o se va a cambiar el estado
		.AddHiddenColumn("hddheadProvider", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadBranch", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadProduct", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadPolicy", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadProponum", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadCertif", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadClaim", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadOffice", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadOrderType", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadStatus_ord", CStr(eRemoteDB.Constants.strnull))
		.AddHiddenColumn("hddheadFec_prog", CStr(eRemoteDB.Constants.strnull))
	End With
	'- Se definen las propiedades generales del grid
	With mobjGrid
		.Width = 780
		.Height = 450
		.Codispl = "SI021"
		.ActionQuery = mobjValues.ActionQuery
		.DeleteButton = False
		.AddButton = False
		.Top = 90
		.Left = 4
		.nMainAction = Request.QueryString("nMainAction")
		.Columns("Sel").GridVisible = Not .ActionQuery
		.FieldsByRow = 2
		If Request.QueryString("Type") <> "PopUp" Then
			.Columns("chkPayment").OnClick = "ShowPages(1,this)"
			.Columns("chkAjReserv").OnClick = "ShowPages(2,this)"
		End If
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub
'% insPreSI021: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreSI021()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lclsProf_ord As eClaim.Prof_ord
	Dim lcolProf_ords As eClaim.Prof_ords
	lintIndex = 0
	lclsProf_ord = New eClaim.Prof_ord
	lcolProf_ords = New eClaim.Prof_ords
	If lcolProf_ords.Find_Provider(mobjValues.StringToType(Request.QueryString("nProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nProponum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nOrderType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nStatus_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("dFec_prog"), eFunctions.Values.eTypeData.etdDate)) Then
		For	Each lclsProf_ord In lcolProf_ords
			With mobjGrid
				'- se copian los parametros del encabezado en variables ocultas para manejar la recarga de la popup.
				.Columns("hddheadProvider").DefValue = mobjValues.StringToType(Request.QueryString("nProvider"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadBranch").DefValue = mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadProduct").DefValue = mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadPolicy").DefValue = mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadProponum").DefValue = mobjValues.StringToType(Request.QueryString("nProponum"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadCertif").DefValue = mobjValues.StringToType(Request.QueryString("nCertif"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadClaim").DefValue = mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadOffice").DefValue = mobjValues.StringToType(Request.QueryString("nOffice"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadOrderType").DefValue = mobjValues.StringToType(Request.QueryString("nOrderType"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadStatus_ord").DefValue = mobjValues.StringToType(Request.QueryString("nStatus_ord"), eFunctions.Values.eTypeData.etdDouble)
				.Columns("hddheadFec_prog").DefValue = mobjValues.StringToType(Request.QueryString("dFec_prog"), eFunctions.Values.eTypeData.etdDate)
				'- La columna Sel se habilita cuando la linea en tratamiento esta "Asignada no realizada"(2)
				'- o "devuelta sin ser realizada (6)"
				If lclsProf_ord.nStatus_ord = 2 Or lclsProf_ord.nStatus_ord = 6 Then
					.Columns("Sel").Disabled = False
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				Else
					.Columns("Sel").Disabled = True
				End If
				
				.Columns("tcnOrderServ").DefValue = CStr(lclsProf_ord.nServ_Order)
				.Columns("tcnProvider").DefValue = CStr(lclsProf_ord.nProvider)
				
				If lclsProf_ord.nProvider = 0 Then
					.Columns("tctProvider").DefValue = " " & " " & lclsProf_ord.sProviderName
				Else
					.Columns("tctProvider").DefValue = lclsProf_ord.nProvider & " " & lclsProf_ord.sProviderName
				End If
				
				.Columns("tctOffice").DefValue = lclsProf_ord.sOfficeName
				.Columns("tcnOffice").DefValue = CStr(lclsProf_ord.nOffice)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nOffice") <= 0 Then
					mobjGrid.Columns("tctOffice").GridVisible = True
				Else
					mobjGrid.Columns("tctOffice").GridVisible = False
				End If
				.Columns("tcnClaim").DefValue = CStr(lclsProf_ord.nClaim)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nClaim") <= 0 Then
					mobjGrid.Columns("tcnClaim").GridVisible = True
				Else
					mobjGrid.Columns("tcnClaim").GridVisible = True
				End If
				.Columns("tctCase").DefValue = lclsProf_ord.sCase
				.Columns("tctBranch").DefValue = lclsProf_ord.sDes_branch
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nBranch") <= 0 Then
					mobjGrid.Columns("tctBranch").GridVisible = True
				Else
					mobjGrid.Columns("tctBranch").GridVisible = False
				End If
				.Columns("tctProduct").DefValue = lclsProf_ord.sDes_product
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nProduct") <= 0 Then
					mobjGrid.Columns("tctProduct").GridVisible = True
				Else
					mobjGrid.Columns("tctProduct").GridVisible = False
				End If
				.Columns("tcnPolicy").DefValue = CStr(lclsProf_ord.nPolicy)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nPolicy") <= 0 Then
					mobjGrid.Columns("tcnPolicy").GridVisible = True
				Else
					mobjGrid.Columns("tcnPolicy").GridVisible = False
				End If
				.Columns("tcnProponum").DefValue = CStr(lclsProf_ord.nProponum)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nProponum") <= 0 Then
					mobjGrid.Columns("tcnProponum").GridVisible = True
				Else
					mobjGrid.Columns("tcnProponum").GridVisible = False
				End If
				.Columns("tcnCertif").DefValue = CStr(lclsProf_ord.nCertif)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nCertif") <= 0 Then
					mobjGrid.Columns("tcnCertif").GridVisible = True
				Else
					mobjGrid.Columns("tcnCertif").GridVisible = False
				End If
				.Columns("tcdFec_prog").DefValue = CStr(lclsProf_ord.dFec_prog)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("dFec_prog") <= eRemoteDB.Constants.dtmnull Then
					mobjGrid.Columns("tcdFec_prog").GridVisible = True
				Else
					mobjGrid.Columns("tcdFec_prog").GridVisible = False
				End If
				.Columns("tctTime_prog").DefValue = lclsProf_ord.sTime_prog
				If lclsProf_ord.dMade_date = eRemoteDB.Constants.dtmNull Then
					.Columns("tcdMade_date").DefValue = CStr(lclsProf_ord.dFec_prog)
				Else
					.Columns("tcdMade_date").DefValue = CStr(lclsProf_ord.dMade_date)
				End If
				If lclsProf_ord.sMade_time = CStr(eRemoteDB.Constants.strnull) Then
					.Columns("tctMade_time").DefValue = lclsProf_ord.sTime_prog
				Else
					.Columns("tctMade_time").DefValue = lclsProf_ord.sMade_time
				End If
				.Columns("tcnauxPolicy").DefValue = CStr(lclsProf_ord.nPolicy)
				.Columns("tcnauxCertif").DefValue = CStr(lclsProf_ord.nCertif)
				.Columns("tcnauxClaim").DefValue = CStr(lclsProf_ord.nClaim)
				.Columns("tcnauxBranch").DefValue = CStr(lclsProf_ord.nBranch)
				.Columns("tcnauxProduct").DefValue = CStr(lclsProf_ord.nProduct)
				.Columns("tctOrderType").DefValue = lclsProf_ord.sOrderType
				.Columns("tcnOrderType").DefValue = CStr(lclsProf_ord.nOrderType)
				If Request.QueryString("Type") = "Popup" Or Request.QueryString("nOrderType") <= 0 Then
					mobjGrid.Columns("tctOrderType").GridVisible = True
				Else
					mobjGrid.Columns("tctOrderType").GridVisible = False
				End If
				.Columns("tcnStatus_ord").DefValue = CStr(lclsProf_ord.nStatus_ord)
				.Columns("cbeStatus_ord").DefValue = CStr(lclsProf_ord.nStatus_ord)
				'- Permitir cambiar el estado a "devuelta sin ser realizada" o "anulada",
				'- sólo para aquellas ordenes que no hayan sido pagadas o anuladas.
				If Not lclsProf_ord.nStatus_ord = 4 And Not lclsProf_ord.nStatus_ord = 5 Then
					.Columns("cbeStatus_ord").GridVisible = True
					.Columns("tctCase").EditRecord = True
				Else
					.Columns("tctCase").EditRecord = False
					.Columns("tctCase").HRefScript = "ValidateStatus_ord();"
				End If
				'- Se habilita la columna Ajustar provisión y pagar cuando la orden 
				'- tiene estado realizada, y el origen del estado es siniestro
				If lclsProf_ord.nStatus_ord = 3 And lclsProf_ord.nOrdClass = 3 Then
					.Columns("chkAjReserv").Disabled = False
					.Columns("chkPayment").Disabled = False
				Else
					.Columns("chkAjReserv").Disabled = True
					.Columns("chkPayment").Disabled = True
				End If
				.Columns("chkAjReserv").OnClick = "ShowPages(2," & lintIndex & ")"
				.Columns("chkPayment").OnClick = "ShowPages(1," & lintIndex & ")"
				.Columns("chkAjReserv").DefValue = "0"
				.Columns("chkPayment").DefValue = "0"
				.Columns("tcdDateDone").DefValue = CStr(lclsProf_ord.dDate_done)
				.Columns("tctStaClaim").DefValue = lclsProf_ord.sStaclaim
				.Columns("tctStaReserve").DefValue = lclsProf_ord.sStaReserve
				.Columns("tcnNumCase").DefValue = CStr(lclsProf_ord.nCase_num)
				.Columns("hddnOrdClass").DefValue = CStr(lclsProf_ord.nOrdClass)
				.Columns("hddnStatus_ord").DefValue = CStr(lclsProf_ord.nStatus_ord)
				.Columns("hddnClaim").DefValue = CStr(lclsProf_ord.nClaim)
				.Columns("hddnTypDemand").DefValue = CStr(lclsProf_ord.nDeman_type)
				.Columns("hddnTransac").DefValue = CStr(lclsProf_ord.nTransac)
				.Columns("hddClient").DefValue = lclsProf_ord.sClient
				.Columns("hddCurrency").DefValue = CStr(lclsProf_ord.nCurrency)
				.Columns("hddnOrderServ").DefValue = CStr(lclsProf_ord.nServ_Order)
				.Columns("hddnClient_Demand").DefValue = lclsProf_ord.sClient_Deman
				.sEditRecordParam = "hddnStatus_ord=' + marrArray[" & CStr(lintIndex) & "].hddnStatus_ord + '" & "&hddnOrdClass=' + marrArray[" & CStr(lintIndex) & "].hddnOrdClass + '" & "&hddnClaim=' + marrArray[" & CStr(lintIndex) & "].hddnClaim + '" & "&hddsAjReserv=' + marrArray[" & CStr(lintIndex) & "].hddsAjReserv + '" & "&hddsPayment=' + marrArray[" & CStr(lintIndex) & "].hddsPayment + '" & "&hddnTypeProcess=' + 1 + '"
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsProf_ord
	End If
	
	If mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble) <> eRemoteDB.Constants.intNull Then
		Session("nClaim") = mobjValues.StringToType(Request.QueryString("nClaim"), eFunctions.Values.eTypeData.etdDouble)
	Else
		Session("nClaim") = eRemoteDB.Constants.intNull
		
	End If
	
	Response.Write(mobjGrid.closeTable() & mobjValues.BeginPageButton)
	'UPGRADE_NOTE: Object lclsProf_ord may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsProf_ord = Nothing
	'UPGRADE_NOTE: Object lcolProf_ords may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolProf_ords = Nothing
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreSI021Upd()
	'----------------------------------------------------------------------------------------------
	'- Variable que se usa para el manejo del combo en la ventana popup.
	Dim lbolView As Boolean
	If Request.QueryString("Sel") = 1 Then
		lbolView = True
	Else
		lbolView = False
		mobjGrid.Columns("cbeStatus_ord").BlankPosition = False
		mobjGrid.Columns("cbeStatus_ord").TypeList = CShort("1")
		mobjGrid.Columns("cbeStatus_ord").List = "6,5"
	End If
	
	If Not Request.QueryString("hddnStatus_ord") = 4 And Not Request.QueryString("hddnStatus_ord") = 5 Then
		Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "ValClaim.aspx", "SI021", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
	End If
	
	If lbolView Then
		Response.Write("<SCRIPT>self.document.forms[0].cbeStatus_ord.value = 3;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].cbeStatus_ord.disabled = true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].chkContinue.disabled = true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].hddnTypeProcess.value = 0;</" & "Script>")
	Else
		Response.Write("<SCRIPT>self.document.forms[0].cbeStatus_ord.value = 6;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tcdMade_date.disabled = true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tctMade_time.disabled = true;</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].hddnTypeProcess.value = 1;</" & "Script>")
	End If
	If mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		Response.Write("<SCRIPT>self.document.forms[0].tcCProvider.value = '';</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].tctCase.value = '';</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].btntcCProvider.disabled = true;</" & "Script>")
	Else
		Response.Write("<SCRIPT>self.document.forms[0].tcCProvider.value = '" & Request.QueryString("nProv") & "'</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].btntcCProvider.disabled = true;</" & "Script>")
	End If
	If Request.QueryString("nProv") <> eRemoteDB.Constants.intNull Then
		Response.Write("<SCRIPT>self.document.forms[0].tcCProvider.value = '" & Request.QueryString("nProv") & "'</" & "Script>")
		Response.Write("<SCRIPT>self.document.forms[0].btntcCProvider.disabled = true;</" & "Script>")
	End If
	
	Response.Write("<SCRIPT>self.document.forms[0].chkContinue.checked = 0;</" & "Script>")
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si021")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si021"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = "si021"
Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.34.12
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401

%>
<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
With Response
	If Request.QueryString("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "SI021", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
		'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
	End If
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("SI021", Request.QueryString("sWindowDescript")))
End With
%>
<SCRIPT>
//- Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 12.31 $|$$Author: Nvaplat60 $"

//-------------------------------------------------------------------------------------------
function ValidateStatus_ord(){
//-------------------------------------------------------------------------------------------
    alert('El estado actual no permite modificaciones');
    self.document.forms[0].hddnTypeProcess.value = 1;
}
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrQueryString = '&nOrderServ=' + marrArray[lintIndex].hddnOrderServ + '&nStatus_ord=' + marrArray[lintIndex].hddnStatus_ord + '&nProv=' +  marrArray[lintIndex].tcnProvider + '&Sel=1'
    if (Field.checked){
        EditRecord(lintIndex,nMainAction,'Update',marrArray[lintIndex].tctParam + lstrQueryString)
        Field.checked = !Field.checked
    }
}
//% ShowPages: Llama a las ventanas de pago de siniestro y/o cualquiera que sea el caso
//-------------------------------------------------------------------------------------------
function ShowPages(sField,Field){
//-------------------------------------------------------------------------------------------
//pago de siniestros
	with (marrArray[Field]){
		if (sField == '1'){
			if (hddnOrdClass == 3){
			    top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=SI008_K&nClaim=' + tcnauxClaim + '&nCaseNum=' + tcnNumCase + '&nDeman_Type=' + hddnTypDemand + '&sClient=' + hddnClient_Demand + '&nPayType=3' + '&dPayDate=' + tcdMade_date ;
			}
		}
		else if (hddnOrdClass == 3){//Llama a la transaccion SI007 ajustes de provision
                 insDefValues('SI021','nBranch=' + tcnauxBranch + '&nCertif=' + tcnauxCertif + '&nPolicy=' + tcnauxPolicy + '&nClaim=' + tcnauxClaim + '&nProduct=' + tcnauxProduct + '&nCase=' + tcnNumCase + '&nDeman_type=' + hddnTypDemand + '&sClient=' + hddClient,'/VTimeNet/Claim/Claim');			 	 
				 top.document.location.href = '/VTimeNet/common/GoTo.aspx?sCodispl=SI007_2';
			 }
	}
}
//%insFormatHours.Esta funcion se encarga de formatear el campo Hora de planificación
//-----------------------------------------------------------------------------------
function insFormatHours(Field){
//---------------------------------------------------------------------------------------
    Field.value = Field.value.replace(':','')
    switch (Field.value.length){
        case 1 :
            Field.value = '00:0' + Field.value
            break;
        case 2 :
            Field.value = '00:' + Field.value
            break;
        case 3 :
            Field.value = '0' + Field.value.substr(0,1) + ':' + Field.value.substr(1,2)
            break;
        case 4 :
            Field.value = Field.value.substr(0,2) + ':' + Field.value.substr(2,2)
            break;
    }
}

function changeDisabled(){
with (self.document.forms[0]){
	tcCProvider.disabled = true ;
}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SI021" ACTION="ValClaim.aspx?x=1&nTransacio=<%=mintTransacio%>">
    <%Response.Write(mobjValues.ShowWindowsName("SI021", Request.QueryString("sWindowDescript")))%>
<BR>
<%
Call insDefineHeader()
If Request.QueryString("Type") = "PopUp" Then
	Call insPreSI021Upd()
Else
	Call insPreSI021()
End If
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.34.12
Call mobjNetFrameWork.FinishPage("si021")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




