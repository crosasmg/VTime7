<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las los campos de la página
Dim mintOrdClass As Object
Dim mlngBranch As Object
Dim mlngProduct As Object
Dim mlngPolicy As Object
Dim mlngProponum As Object
Dim mlngCertif As Object
Dim mlngClaim As Object
Dim mlngCase_num As Object
Dim mstrBrancht As Object
Dim mintDeman_type As Object
Dim mstrKeyAddress As String
Dim mblnQuery As Object
'+ Dirección del siniestro 
Dim mstrDescadd As String
Dim mintMunicipality As Object
Dim mblndisZone As Boolean
Dim tcdEffecdate As Object


'%insReaInitialValue. Se encarga de llenar inicializar los campos de la transacción
'------------------------------------------------------------------------------
Private Sub insReaInitialValue()
	'------------------------------------------------------------------------------
	mblndisZone = False
	Select Case Request.QueryString.Item("sCodispl")
		'+ Cuando es llamada desde la secuencia de póliza
		Case "OS001"
			mintOrdClass = Session("sCertype")
			If CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "6" Or CStr(Session("sCertype")) = "7" Then
				mintOrdClass = 1
			End If
			mlngBranch = Session("nBranch")
			mlngProduct = Session("nProduct")
			mlngPolicy = Session("nPolicy")
			mlngProponum = Session("nPolicy")
			mlngCertif = Session("nCertif")
			mlngClaim = eRemoteDB.Constants.intNull
			mlngCase_num = eRemoteDB.Constants.intNull
			mstrBrancht = Session("sBrancht")
			mintDeman_type = eRemoteDB.Constants.intNull
			mstrKeyAddress = "1" & mintOrdClass & mlngBranch & mlngProduct & mlngPolicy & mlngCertif
			mblndisZone = True
			
			'+ Cuando es llamada desde la ventana de transacción (Transacciones de órdenes de servicio).
		Case "OS001_K"
			With Request
				mintOrdClass = mobjValues.StringToType(.QueryString.Item("nOrdClass"), eFunctions.Values.eTypeData.etdDouble)
				mlngBranch = mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
				mlngProduct = mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
				mlngProponum = mobjValues.StringToType(.QueryString.Item("nProponum"), eFunctions.Values.eTypeData.etdDouble)
				mlngPolicy = mobjValues.StringToType(.QueryString.Item("npolicy"), eFunctions.Values.eTypeData.etdDouble)
				If mintOrdClass = "1" Then
					mlngPolicy = mlngProponum
				End If
				mlngCertif = mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble)
				mlngClaim = mobjValues.StringToType(.QueryString.Item("nClaim"), eFunctions.Values.eTypeData.etdDouble)
				mlngCase_num = mobjValues.StringToType(.QueryString.Item("nCase_num"), eFunctions.Values.eTypeData.etdDouble)
				mstrBrancht = .QueryString.Item("sBrancht")
				mintDeman_type = mobjValues.StringToType(.QueryString.Item("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)
				mstrKeyAddress = "1" & mintOrdClass & mlngBranch & mlngProduct & mlngPolicy & mlngCertif
				mblndisZone = False
			End With
	End Select
End Sub

'%insDefineHeader: Se definen las columnas del grid
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lclsAddress As eGeneralForm.Address
	Dim lclsPolicy As ePolicy.Policy
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	If CStr(Session("dEffecdate")) = vbNullString Then
		tcdEffecdate = Today
	Else
		tcdEffecdate = Session("dEffecdate")
	End If
	
	lclsAddress = New eGeneralForm.Address
	If lclsAddress.Find(mstrKeyAddress, 8, tcdEffecdate) Then
		mstrDescadd = Mid(lclsAddress.sDescadd, 1, 30)
		mintMunicipality = lclsAddress.nMunicipality
	Else
		mstrDescadd = vbNullString
		mintMunicipality = eRemoteDB.Constants.intNull
	End If
	lclsAddress = Nothing
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		'+ Orden
		Call .AddNumericColumn(0, GetLocalResourceObject("nServ_orderColumnCaption"), "nServ_order", 10, CStr(0),  , GetLocalResourceObject("nServ_orderColumnToolTip"),  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nServ_order").PopUpVisible = False
		
		'+ Proveedor titular de la orden de servicio
		Call .AddNumericColumn(0, GetLocalResourceObject("nProviderColumnCaption"), "nProvider", 5, CStr(0),  , GetLocalResourceObject("nProviderColumnToolTip"),  ,  ,  ,  ,  , True)
		mobjGrid.Columns("nProvider").PopUpVisible = False
		
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeProviderColumnCaption"), "cbeProvider", "TabTab_provider", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  , "insChangeField(""cbeProvider"",lintActionType,'" & mstrKeyAddress & "');",  ,  , GetLocalResourceObject("cbeProviderColumnToolTip"))
		With mobjGrid.Columns("cbeProvider").Parameters
			.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nTypeProv", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		
		'+ Fecha de asignación
		.AddDateColumn(0, GetLocalResourceObject("tcdAssignDateColumnCaption"), "tcdAssignDate", CStr(Today),  , GetLocalResourceObject("tcdAssignDateColumnToolTip"))
		
		'+ Fecha de planificación
		.AddDateColumn(0, GetLocalResourceObject("tcdFec_progColumnCaption"), "tcdFec_prog",  ,  , GetLocalResourceObject("tcdFec_progColumnToolTip"))
		
		'+ Hora de planificación
		.AddTextColumn(0, GetLocalResourceObject("tctTime_progColumnCaption"), "tctTime_prog", 5, "00:00",  , GetLocalResourceObject("tctTime_progColumnCaption"),  ,  , "insFormatHours(this)")
		
		'- Fecha realizada
		.AddDateColumn(0, GetLocalResourceObject("tcdMade_dateColumnCaption"), "tcdMade_date",  , True, GetLocalResourceObject("tcdMade_dateColumnToolTip"))
		'- Hora realizada
		.AddTextColumn(0, GetLocalResourceObject("tctMade_timeColumnCaption"), "tctMade_time", 5, "00:00",  , GetLocalResourceObject("tctMade_timeColumnToolTip"),  ,  , "insFormatHours(this)")
		
		'+ Lugar de inspección
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddTextAreaColumn(0, GetLocalResourceObject("tctPlaceColumnCaption"), "tctPlace", mstrDescadd, 2, 20,  , GetLocalResourceObject("tctPlaceColumnCaption"), mblndisZone)
		Else
			.AddTextColumn(0, GetLocalResourceObject("tctPlaceColumnCaption"), "tctPlace", 50, mstrDescadd,  , GetLocalResourceObject("tctPlaceColumnCaption"),  ,  ,  , mblndisZone)
		End If
		
		'+ Taller (Se muestra sólo si se trata de un producto de automóvil)
		If mstrBrancht = "3" Then
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeWorkshColumnCaption"), "cbeWorksh", "TabTab_provider", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeWorkshColumnCaption"))
			With mobjGrid.Columns("cbeWorksh").Parameters
				.Add("nBranch", mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nTypeProv", 2, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
		End If
		
		'+ Municipalidad
		If mblndisZone Then
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeZoneColumnCaption"), "cbeZone", "Tabmunicipality", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(mintMunicipality, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  , "insChangeField(""cbeZone"",lintActionType,'" & mstrKeyAddress & "');", True,  , GetLocalResourceObject("cbeZoneColumnCaption"))
		Else
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeZoneColumnCaption"), "cbeZone", "Tabmunicipality", eFunctions.Values.eValuesType.clngWindowType, mobjValues.StringToType(mintMunicipality, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  , "insChangeField(""cbeZone"",lintActionType,'" & mstrKeyAddress & "');", False,  , GetLocalResourceObject("cbeZoneColumnCaption"))
		End If
		'+ Contacto
		.AddTextAreaColumn(0, GetLocalResourceObject("tctName_contColumnCaption"), "tctName_cont", " ", 2, 20,  , GetLocalResourceObject("tctName_contColumnToolTip"))
		
		'+ Dirección del contacto 
		'      .AddTextColumn 0,"Dirección del contacto","tctAdd_contact",100," ",,"Dirección del contacto"
		.AddTextAreaColumn(0, GetLocalResourceObject("tctAdd_contactColumnCaption"), "tctAdd_contact", " ", 2, 20,  , GetLocalResourceObject("tctAdd_contactColumnToolTip"))
		
		'+ Teléfono del contacto 
		.AddTextColumn(0, GetLocalResourceObject("tctPhone_contColumnCaption"), "tctPhone_cont", 11, " ",  , GetLocalResourceObject("tctPhone_contColumnToolTip"))
		
		'+ Estado
		.AddPossiblesColumn(0, GetLocalResourceObject("cbeStatus_ordColumnCaption"), "cbeStatus_ord", "table215", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , Request.QueryString.Item("Action") <> "Update", 5, GetLocalResourceObject("cbeStatus_ordColumnCaption"))
		
		'+ Tipo de inspección 
		'.AddPossiblesColumn(0, GetLocalResourceObject("cbeOrd_TypeCostColumnCaption"), "cbeOrd_TypeCost", "table5597", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeOrd_TypeCostColumnToolTip"))
        .AddHiddenColumn ("cbeOrd_TypeCost", "1") 		
		
		lclsPolicy = New ePolicy.Policy
		If lclsPolicy.Find_TabNameB(mlngBranch) Then
			If lclsPolicy.sTabname = "FIRE" Then
				.AddPossiblesColumn(0, GetLocalResourceObject("cbeOrderTypeColumnCaption"), "cbeOrderType", "table7100", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeOrderTypeColumnCaption"))
				mobjGrid.Columns("cbeOrderType").TypeList = 1
				mobjGrid.Columns("cbeOrderType").List = "5,9,10,11,12,13"
			Else
				'+ Tipo de orden de servicio
				.AddPossiblesColumn(0, GetLocalResourceObject("cbeOrderTypeColumnCaption"), "cbeOrderType", "table7100", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeOrderTypeColumnCaption"))
				mobjGrid.Columns("cbeOrderType").TypeList = 2
				mobjGrid.Columns("cbeOrderType").List = "1,3,7"
			End If
		End If
		lclsPolicy = Nothing
		'+ Notas
		.AddButtonColumn(0, GetLocalResourceObject("SCA2-WColumnCaption"), "SCA2-W", CDbl(Request.QueryString.Item("nNoteNum")),  , Request.QueryString.Item("Type") <> "PopUp",  ,  ,  ,  , "btnNotenum")
		
		'+ Columnas ocultas
		.AddHiddenColumn("hddnOrdClass", mintOrdClass)
		.AddHiddenColumn("hddnBranch", mlngBranch)
		.AddHiddenColumn("hddnProduct", mlngProduct)
		.AddHiddenColumn("hddnPolicy", mlngPolicy)
		.AddHiddenColumn("hddnProponum", mlngProponum)
		.AddHiddenColumn("hddnCertif", mlngCertif)
		.AddHiddenColumn("hddnClaim", mlngClaim)
		.AddHiddenColumn("hddnCase_num", mlngCase_num)
		.AddHiddenColumn("hddnServ_order", Request.Form.Item("nServ_order"))
		.AddHiddenColumn("hddsBrancht", mstrBrancht)
		.AddHiddenColumn("hddnDeman_type", mintDeman_type)
		.AddHiddenColumn("sParam", vbNullString)
		.AddHiddenColumn("hddnMunicipality", vbNullString)
		
		If Request.QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>var lintActionType=1;</" & "Script>")
		Else
			Response.Write("<SCRIPT>var lintActionType=2;</" & "Script>")
		End If
		
	End With
	
	With mobjGrid
		.DeleteButton = False
		.AddButton = Not mblnQuery
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "OS001"
		.Width = 700
		.Height = 450
		.FieldsByRow = 2
		.Top = 200
		.Left = 90
		.ActionQuery = mblnQuery
		If mblnQuery Then
			.nMainAction = 401
		Else
			.nMainAction = 302
		End If
		
		.Columns("Sel").GridVisible = False
		.Columns("tctName_cont").GridVisible = False
		.Columns("tctAdd_contact").GridVisible = False
		.Columns("tctPhone_cont").GridVisible = False
		.Columns("cbeOrd_TypeCost").GridVisible = False
		.Columns("cbeOrderType").GridVisible = False
		.Columns("cbeProvider").EditRecord = True
		.Columns("nServ_order").EditRecord = True
		
		.sEditRecordParam = "nOrdClass=" & mintOrdClass & "&nBranch=" & mlngBranch & "&nProduct=" & mlngProduct & "&nPolicy=" & mlngPolicy & "&nProponum=" & mlngProponum & "&nCertif=" & mlngCertif & "&nClaim=" & mlngClaim & "&nCase_num=" & mlngCase_num & "&sBrancht=" & mstrBrancht & "&nDeman_type=" & mintDeman_type
		
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreOS001. Se crea la ventana madre (Principal)
'------------------------------------------------------------------------------
Private Sub insPreOS001()
	'------------------------------------------------------------------------------
	Dim lcolProf_ords As eClaim.Prof_ords
	Dim lclsProf_ord As Object
	Dim lblnFound As Boolean
	Dim lintIndex As Object
	
	lblnFound = False
	With Request
		lcolProf_ords = New eClaim.Prof_ords
		With mobjGrid
			lblnFound = lcolProf_ords.Find_OS001(mintOrdClass, mlngBranch, mlngProduct, mlngPolicy, mlngCertif, mlngClaim, mlngCase_num, mintDeman_type)
			lintIndex = 0
			If lblnFound Then
				For	Each lclsProf_ord In lcolProf_ords
					.Columns("nServ_order").DefValue = lclsProf_ord.nServ_order
					.Columns("hddnServ_order").DefValue = lclsProf_ord.nServ_order
					.Columns("cbeProvider").DefValue = lclsProf_ord.nProvider
					.Columns("nProvider").DefValue = lclsProf_ord.nProvider
					.Columns("tcdFec_prog").DefValue = lclsProf_ord.dFec_prog
					.Columns("tcdAssignDate").DefValue = lclsProf_ord.dAssignDate
					.Columns("tctTime_prog").DefValue = lclsProf_ord.sTime_prog
					
					.Columns("tctPlace").DefValue = lclsProf_ord.sPlace
					'+ Taller (Se muestra sólo si se trata de un producto de automóvil)
					If Request.QueryString.Item("sBrancht") = "3" Then
						.Columns("cbeWorksh").DefValue = lclsProf_ord.nWorksh
					End If
					.Columns("cbeZone").DefValue = lclsProf_ord.nMunicipality
					.Columns("tctName_cont").DefValue = lclsProf_ord.sName_cont
					.Columns("tctAdd_contact").DefValue = lclsProf_ord.sAdd_contact
					.Columns("tctPhone_cont").DefValue = lclsProf_ord.sPhone_cont
					.Columns("cbeStatus_ord").DefValue = lclsProf_ord.nStatus_ord
					'.Columns("cbeOrd_TypeCost").DefValue = lclsProf_ord.nOrd_TypeCost
					.Columns("cbeOrderType").DefValue = lclsProf_ord.nOrderType
					.Columns("btnNotenum").nNotenum = lclsProf_ord.nNoteorder
					.Columns("hddnMunicipality").DefValue = lclsProf_ord.nMunicipality
					
					If lclsProf_ord.dMade_Date = eRemoteDB.Constants.dtmnull Then
						.Columns("tcdMade_date").DefValue = lclsProf_ord.dFec_prog
					Else
						.Columns("tcdMade_date").DefValue = lclsProf_ord.dMade_Date
					End If
					
					If lclsProf_ord.sMade_time = eRemoteDB.Constants.strnull Then
						.Columns("tctMade_time").DefValue = lclsProf_ord.sTime_prog
					Else
						.Columns("tctMade_time").DefValue = lclsProf_ord.sMade_time
					End If
					
					'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
					'+ función insPostOS001Upd cuando se eliminen los registros seleccionados - NVAPLAT9 - 15/04/2002
					
					.Columns("sParam").DefValue = "nOrdClass=" & lclsProf_ord.nOrdClass & "&nBranch=" & lclsProf_ord.nBranch & "&nProduct=" & lclsProf_ord.nProduct & "&nPolicy=" & lclsProf_ord.nPolicy & "&nProponum=" & lclsProf_ord.nPolicy & "&nCertif=" & lclsProf_ord.nCertif & "&nClaim=" & lclsProf_ord.nClaim & "&nCase_num=" & lclsProf_ord.nCase_num & "&nServ_order=" & lclsProf_ord.nServ_order & "&nUserCode=" & Session("nUsercode")
					Response.Write(mobjGrid.DoRow())
					lintIndex = lintIndex + 1
				Next lclsProf_ord
			End If
		End With
	End With
	
	Response.Write(mobjValues.HiddenControl("hddnItems", lintIndex))
	
	Response.Write(mobjGrid.CloseTable())
	
	lclsProf_ord = Nothing
	lcolProf_ords = Nothing
	
End Sub

'% insPreOS001Upd. Se define esta funcion para contruir el contenido de la 
'%                  ventana de actualización de la Tabla Ordenes de servicios
'----------------------------------------------------------------------------
Private Sub insPreOS001Upd()
	'----------------------------------------------------------------------------
	Dim lclsProf_ord As eClaim.Prof_ord
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclsProf_ord = New eClaim.Prof_ord
			
			Call lclsProf_ord.insPostOS001Upd("Del", mintOrdClass, mlngBranch, mlngProduct, mlngPolicy, mlngProponum, mlngCertif, mlngClaim, mlngCase_num, mobjValues.StringToType(.QueryString.Item("nServ_order"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeProvider"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdAssignDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdFec_prog"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tctTime_prog"), .Form.Item("tctPlace"), mobjValues.StringToType(.Form.Item("cbeWorksh"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeZone"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctName_cont"), .Form.Item("tctAdd_contact"), .Form.Item("tctPhone_cont"), mobjValues.StringToType(.Form.Item("cbeStatus_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrd_typeCost"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeOrderType"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnNotenum"), eFunctions.Values.eTypeData.etdDouble), mintDeman_type, Session("nUsercode"), eRemoteDB.Constants.dtmnull, VbNullString)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum</" & "Script>")
		End If
		
	End With
	lclsProf_ord = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("OS001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

'+ Si se trata de la secuencia.
If Request.QueryString.Item("sCodispl") = "OS001" Then
	mblnQuery = Session("bQuery")
Else
	mblnQuery = (CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401)
End If
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT LANGUAGE="JavaScript"> 
//+ Variable para el control de versiones 
     document.VssVersion="$$Revision: 7 $|$$Date: 8/12/03 12:48 $|$$Author: Nvaplat22 $" 
     
//%insFormatHours.Esta funcion se encarga de formatear el campo Hora de planificación 
//-----------------------------------------------------------------------------------  
function insFormatHours(Field){ 
//----------------------------------------------------------------------------------- 
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

//% insChangeField: Se recargan los valores cuando cambia el campo
//-------------------------------------------------------------------------------------------
function insChangeField(sField, ActionType, sKey){
//-------------------------------------------------------------------------------------------    
	with (self.document.forms[0]){	 
		switch(sField){
			case "cbeProvider":
				if (ActionType == 1){
					if (cbeProvider.value =='')        		      
						cbeStatus_ord.value= "1";  
					else
						cbeStatus_ord.value= "2";  				   
					cbeStatus_ord.disabled = true;
				}
				break;
			case "cbeZone":
				var sExecute
				if (hddnMunicipality.value==cbeZone.value){
					sExecute = "1";
				}					
				else{
					sExecute = "1";
					hddnMunicipality.value = cbeZone.value;
				}
				if (cbeZone.value != '')				
					insDefValues(sField, "sCodispl=" + '<%=Request.QueryString.Item("sCodispl")%>' + "&nZone=" + cbeZone.value + "&nProvider=" + cbeProvider.value + "&sKey=" + sKey + "&sExecute=" + sExecute, '/VTimeNet/Prof_ord/Prof_ordTra');
				break;             
		}
    }
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmOS001" ACTION="valPolicySeq.aspx?sZone=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insReaInitialValue()
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreOS001()
Else
	Call insPreOS001Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("OS001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




