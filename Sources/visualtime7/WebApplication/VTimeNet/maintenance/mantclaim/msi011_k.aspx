<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

Dim nRow As Integer
Dim mintRowscount As Integer


'% insDefineHeader: Se definen las columns del Grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCodigoColumnCaption"), "tcnCodigo", 6, CStr(eRemoteDB.Constants.intNull),  , GetLocalResourceObject("tcnCodigoColumnToolTip"),  ,  ,  ,  , "ShowHead();ShowImages();", Request.QueryString.Item("Action") = "Update")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypProviderColumnCaption"), "cbeTypProvider", "Table7027", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTypProviderColumnToolTip"))
		
		If Request.QueryString.Item("nMainAction") = "302" Then
			Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"),  , True, "lblCliename",  ,  ,  ,  ,  , True)
		Else
			Call .AddClientColumn(0, GetLocalResourceObject("dtcClientColumnCaption"), "dtcClient", vbNullString,  , GetLocalResourceObject("dtcClientColumnToolTip"), "ShowImages();", True, "lblCliename",  ,  ,  ,  ,  , True)
		End If
		
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenOfficeColumnCaption"), "cbenOffice", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbenOfficeColumnToolTip"))
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkZoneColumnCaption"), "chkZone", "",  ,  , "ShowZone()",  , GetLocalResourceObject("chkZoneColumnToolTip"))
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkZoneColumnCaption"), "chkZone", "",  ,  , "ShowZone()", True, GetLocalResourceObject("chkZoneColumnToolTip"))
		End If
		
		If Request.QueryString.Item("nMainAction") = "302" Then
			Call .AddAnimatedColumn(0, "", "cbeImage0", "/VTimeNet/images/btnWNotes.png")
		Else
			Call .AddAnimatedColumn(0, "", "cbeImage0", "/VTimeNet/images/btnWNotes.png",  ,  ,  , True)
		End If
		mobjGrid.Columns("cbeImage0").HRefScript = "insShowMSI647(document.forms[0].tcnCodigo.value,document.forms[0].cbeTypProvider.value);"
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkBranchColumnCaption"), "chkBranch", "",  ,  , "ShowBranches()",  , GetLocalResourceObject("chkBranchColumnToolTip"))
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkBranchColumnCaption"), "chkBranch", "",  ,  , "ShowBranches()", True, GetLocalResourceObject("chkBranchColumnToolTip"))
		End If
		If Request.QueryString.Item("nMainAction") = "302" Then
			Call .AddAnimatedColumn(0, "", "cbeImage", "/VTimeNet/images/btnWNotes.png")
		Else
			Call .AddAnimatedColumn(0, "", "cbeImage", "/VTimeNet/images/btnWNotes.png",  ,  ,  , True)
		End If
		mobjGrid.Columns("cbeImage").HRefScript = "insShowMSI035(document.forms[0].tcnCodigo.value,document.forms[0].cbeTypProvider.value);"
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddCheckColumn(0, GetLocalResourceObject("chkGroupColumnCaption"), "chkGroup", "",  ,  , "ShowGroup()",  , GetLocalResourceObject("chkGroupColumnToolTip"))
		Else
			Call .AddCheckColumn(0, GetLocalResourceObject("chkGroupColumnCaption"), "chkGroup", "",  ,  , "ShowGroup()", True, GetLocalResourceObject("chkGroupColumnToolTip"))
		End If
		If Request.QueryString.Item("nMainAction") = "302" Then
			Call .AddAnimatedColumn(0, "", "cbeImage1", "/VTimeNet/images/btnWNotes.png")
		Else
			Call .AddAnimatedColumn(0, "", "cbeImage1", "/VTimeNet/images/btnWNotes.png",  ,  ,  , True)
		End If
		mobjGrid.Columns("cbeImage1").HRefScript = "insShowMSI019(document.forms[0].tcnCodigo.value);"
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMax_serv_ordColumnCaption"), "tcnMax_serv_ord", 5, "",  , GetLocalResourceObject("tcnMax_serv_ordColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDateInitialColumnCaption"), "tcdDateInitial", CStr(Today),  , GetLocalResourceObject("tcdDateInitialColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdDateEndColumnCaption"), "tcdDateEnd",  ,  , GetLocalResourceObject("tcdDateEndColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbenTypeSupportColumnCaption"), "cbenTypeSupport", "Table5570", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbenTypeSupportColumnCaption"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkConcesionaryColumnCaption"), "chkConcesionary", "", CShort("1"),  ,  , Request.QueryString.Item("Type") <> "PopUp")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPer_discColumnCaption"), "tcnPer_disc", 4, "",  , GetLocalResourceObject("tcnPer_discColumnToolTip"),  , 2)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStateColumnCaption"), "cbeState", "Table26", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStateColumnCaption"))
	End With
	
	'+ Se asignan las caracteristicas del Grid
	With mobjGrid
		.Columns("dtcClient").EditRecord = True
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MSI011_K"
		.sCodisplPage = "MSI011"
		.Columns("cbeImage").GridVisible = False
		.Columns("cbeImage0").GridVisible = False
		.Columns("cbeImage1").GridVisible = False
		.sDelRecordParam = "sCodisp=" & Session("sCodispl") & "&nCodigo='+ marrArray[lintIndex].tcnCodigo + '" & "&nTypProvider='+ marrArray[lintIndex].cbeTypProvider + '" & "&sClient='+ marrArray[lintIndex].dtcClient + '" & "&dDateInitial='+ marrArray[lintIndex].tcdDateInitial + '" & "&dDateEnd='+ marrArray[lintIndex].tcdDateEnd + '" & "&chkBranch='+ marrArray[lintIndex].chkBranch + '" & "&nOffice='+ marrArray[lintIndex].cbenOffice + '" & "&nMax_serv_ord='+ marrArray[lintIndex].tcnMax_serv_ord + '" & "&nTypeSupport='+ marrArray[lintIndex].cbenTypeSupport + '" & "&nPer_disc='+ marrArray[lintIndex].tcnPer_disc + '" & "&sConcesionary='+ marrArray[lintIndex].chkConcesionary + '" & "&chkZone='+ marrArray[lintIndex].chkZone + '" & "&chkBranch='+ marrArray[lintIndex].chkBranch + '" & "&chkGroup='+ marrArray[lintIndex].chkGroup + '" & "&sState='+ marrArray[lintIndex].cbeState + '" & "&sDigit='+ marrArray[lintIndex].dtcClient_Digit + '"
		.Width = 750
		.Height = 450
		.FieldsByRow = 2
		.Top = 60
		.Left = 30
		If Request.QueryString.Item("Action") = "Add" Then
			.CancelScript = "insCancelScript();"
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Or Request.QueryString.Item("nMainAction") = vbNullString
	End With
End Sub
'% insPreMSI011_K: Proceso que carga los Valores de las columnas del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI011_K()
	'--------------------------------------------------------------------------------------------
	Dim lcolProviders As eClaim.Tab_Providers
	Dim lclsProvider As Object
	Dim lintIndex As Short
	lcolProviders = New eClaim.Tab_Providers
	lintIndex = 0
	
	If mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
		nRow = 1
	Else
		nRow = mobjValues.StringToType(Request.QueryString.Item("nRow"), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	If lcolProviders.Find(nRow) Then
		mintRowscount = lcolProviders.Count
		For	Each lclsProvider In lcolProviders
			With mobjGrid
				.Columns("tcnCodigo").DefValue = lclsProvider.nProvider
				.Columns("cbeTypProvider").DefValue = lclsProvider.nTypeProv
				.Columns("cbeTypProvider").Descript = lclsProvider.sTypeProv
				.Columns("dtcClient").DefValue = lclsProvider.sClient
				.Columns("dtcClient").Digit = lclsProvider.sDigit
				.Columns("dtcClient").Descript = lclsProvider.sCliename
				.Columns("cbenOffice").DefValue = lclsProvider.nOffice
				.Columns("cbenOffice").Descript = lclsProvider.sOffice
				
				If lclsProvider.nProvZone <> eRemoteDB.Constants.intNull Then
					.Columns("chkZone").checked = CShort("1")
				Else
					.Columns("chkZone").checked = CShort("2")
				End If
				
				If lclsProvider.nProvBranch <> eRemoteDB.Constants.intNull Then
					.Columns("chkBranch").checked = CShort("1")
					.Columns("cbeImage").Src = "/VTimeNet/images/btnWNotes.png"
				Else
					.Columns("chkBranch").checked = CShort("2")
					.Columns("cbeImage").Src = "/VTimeNet/images/btnWONotes.png"
				End If
				
				If Request.QueryString.Item("nMainAction") <> vbNullString Then
					.Columns("cbeImage").HRefScript = "insShowMSI035(" & lintIndex & ", " & lclsProvider.nProvider & "," & lclsProvider.nTypeProv & " );"
				End If
				.Columns("tcdDateInitial").DefValue = lclsProvider.dInpdate
				.Columns("tcdDateEnd").DefValue = lclsProvider.dOutDate
				.Columns("cbenTypeSupport").DefValue = lclsProvider.nTypeSupport
				.Columns("cbenTypeSupport").Descript = lclsProvider.sTypeSupport
				.Columns("chkConcesionary").checked = lclsProvider.sConcesionary
				
				If lclsProvider.nProv_group <> eRemoteDB.Constants.intNull Then
					.Columns("chkGroup").checked = CShort("1")
					.Columns("cbeImage1").Src = "/VTimeNet/images/btnWNotes.png"
				Else
					.Columns("chkGroup").checked = CShort("2")
					.Columns("cbeImage1").Src = "/VTimeNet/images/btnWONotes.png"
				End If
				
				.Columns("tcnMax_serv_ord").DefValue = lclsProvider.nMax_serv_ord
				
				If Request.QueryString.Item("nMainAction") <> vbNullString Then
					.Columns("cbeImage1").HRefScript = "insShowMSI019(" & lintIndex & ", " & lclsProvider.nProvider & ");"
				End If
				.Columns("tcnPer_disc").DefValue = lclsProvider.nPer_disc
				.Columns("cbeState").DefValue = lclsProvider.sStatregt
				.Columns("cbeState").Descript = lclsProvider.sStatregt_Desc
				
				'+ Se ejecuta el metodo DoRow, que se encarga de mostrar los elementos de grid					
				Response.Write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			End With
			
			Response.Write("<SCRIPT>" & "insAddCustomerFields(""" & lclsProvider.nProvider & """" & ",""" & lclsProvider.nTypeProv & """)</" & "Script>")
		Next lclsProvider
	End If
	Response.Write(mobjGrid.closeTable())
	
	lclsProvider = Nothing
	lcolProviders = Nothing
	
End Sub

'% insPreMSI011_K_Upd: Proceso que Actualiza los valores de un registro del Grid
'--------------------------------------------------------------------------------------------
Private Sub insPreMSI011_K_Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsProvider As eClaim.Tab_Provider
	Dim lstrErrors As String
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsProvider = New eClaim.Tab_Provider
			lstrErrors = lclsProvider.insValMSI011_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nCodigo"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nTypProvider"), eFunctions.Values.eTypeData.etdDouble, True), .QueryString.Item("sClient"), mobjValues.StringToDate(.QueryString.Item("dDateInitial")), mobjValues.StringToDate(.QueryString.Item("dDateEnd")), .QueryString.Item("sState"), mobjValues.StringToType(.QueryString.Item("cbenOffice"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("tcnMax_serv_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbenTypeSupport"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("tcnPer_disc"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("chkConcesionary"), Session("nUsercode"), Session("nExists_reg"), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType("0", eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sDigit"))
			
			If lstrErrors = vbNullString Then
				Response.Write(mobjValues.ConfirmDelete())
				Call lclsProvider.insPostMSI011_K(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nCodigo"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nTypProvider"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sClient"), mobjValues.StringToDate(.QueryString.Item("dDateInitial")), mobjValues.StringToDate(.QueryString.Item("dDateEnd")), .QueryString.Item("sState"), mobjValues.StringToType("2", eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbenOffice"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnMax_serv_ord"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("cbenTypeSupport"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("tcnPer_disc"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("chkConcesionary"), Session("nUsercode"), mobjValues.StringToType(Session("nExists_reg"), eFunctions.Values.eTypeData.etdDouble))
				
			Else
				Response.Write(lstrErrors)
			End If
			lclsProvider = Nothing
		End If
	End With
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantClaim.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(Request.QueryString.Item("Index"))))
	Response.Write(mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

Session("nExists_reg") = 1
mobjValues.sCodisplPage = "MSI011"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"> </SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MSI011_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT>
	var marrMSI011		= []
	var mintCount		= -1
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 15:53 $|$$Author: Nvaplat61 $"
//% insAddCustomerFields: Añade los registros obtenidos en la consulta a un arreglo - VCVG - 25/10/2001
//------------------------------------------------------------------------------------------------------------
function insAddCustomerFields(nProvider, nTypeProv){
//------------------------------------------------------------------------------------------------------------
    var ludtCustomerFields   = []
    
    ludtCustomerFields[0]    = nProvider
    ludtCustomerFields[1]    = nTypeProv    
    marrMSI011[++mintCount]	 = ludtCustomerFields    
}

//% insStateZone: se controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){}
//-------------------------------------------------------------------------------------------------------------------
//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
			document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insFinish: se controla la Finalización de la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}
//% ShowHead: Inicializa las variables claves del registro tab_provider
//------------------------------------------------------------------------------------------
function ShowHead(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnCodigo.value == ""){
			cbeTypProvider.disabled = true
			dtcClient.disabled = true
			dtcClient_Digit.disabled = true
			btndtcClient.disabled = true
			cbenOffice.disabled = true
			cbeTypProvider.value = ""
			dtcClient.value = ""
			dtcClient_Digit.value = ""
			lblCliename.value = ""
			cbenOffice.value = ""
		}
		else{
			cbeTypProvider.disabled = false
			dtcClient.disabled = false
			dtcClient_Digit.disabled = false
			btndtcClient.disabled = false
			cbenOffice.disabled = false
		}
	}
}
//% ShowImages: Activa las Imagenes de las ventanas requeridas
//------------------------------------------------------------------------------------------
function ShowImages(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		if (tcnCodigo.value == ""){
			cbeImage0.disabled=true
			cbeImage.disabled=true
			cbeImage1.disabled=true
			chkZone.disabled=true
			chkBranch.disabled=true
			chkGroup.disabled=true
		}
		else{
			if(dtcClient.value!=""){
				cbeImage0.disabled=false
				cbeImage.disabled=false
				cbeImage1.disabled=false
				chkZone.disabled=false
				chkBranch.disabled=false
				chkGroup.disabled=false
			}	
		}
	
		if(dtcClient.value!="")
			ShowPopUp("/VTimeNet/Maintenance/MantClaim/ShowDefValues.aspx?Field=Provider&nProvider=" + self.document.forms[0].elements['tcnCodigo'].value + "&sClient=" + self.document.forms[0].elements['dtcClient'].value, "ShowDefValuesMantClaim", 1, 1,"no","no",2000,2000);
	}		
}
//% ShowZone: Tipo de Provider dependiendo de la Comuna
//------------------------------------------------------------------------------------------
function ShowZone(){
//-------------------------------------------------------------------------------------------
	var lstrError = '0';
	with (self.document.forms[0]){
		if(chkZone.checked==true){
			if (tcnCodigo.value==""){
			    alert('Err. 4116: <%=eFunctions.Values.GetMessage(4116)%>');
				lstrError = '1';
			}
			if (dtcClient.value==""){
			    alert('Err. 12043: <%=eFunctions.Values.GetMessage(12043)%>');
				lstrError = '1';
			}
			if (dtcClient_Digit.value==""){
			    alert('Err. 2090: <%=eFunctions.Values.GetMessage(2090)%>');
				lstrError = '1';
			}
		}
		if (lstrError=='0'){
			if(typeof(marrArray)=='undefined'){
					if(chkZone.checked==true){
						ShowPopUp('MSI647_K.aspx?nProvider=' + tcnCodigo.value + '&nTypeProv=' + cbeTypProvider.value + '&nMainAction=' + nMainAction ,'MSI011',500,450,'no','no',200,80);
					}
			}
			else
				if(marrArray[lintIndex].chkZone.checked=="1"){
					with (marrArray[lintIndex])
						ShowPopUp('MSI647_K.aspx?nProvider=' + tcnCodigo.value + '&nTypeProv=' + cbeTypProvider.value + '&nMainAction=' + nMainAction ,'MSI011',500,450,'no','no',200,80);
			}
		}
	}
}
//% ShowBranches: Tipo de Provider dependiendo del Ramo
//------------------------------------------------------------------------------------------
function ShowBranches(){
//-------------------------------------------------------------------------------------------
	var lstrError = '0';
	with (self.document.forms[0]){
		if(chkBranch.checked=="1"){
			if (tcnCodigo.value==""){
			    alert('Err. 4116: <%=eFunctions.Values.GetMessage(4116)%>');
				lstrError = '1';
			}
			if (dtcClient.value==""){
			    alert('Err. 12043: <%=eFunctions.Values.GetMessage(12043)%>');
				lstrError = '1';
			}
			if (dtcClient_Digit.value==""){
			    alert('Err. 2090: <%=eFunctions.Values.GetMessage(2090)%>');
				lstrError = '1';
			}
		}
		if (lstrError=='0'){
			if(typeof(marrArray)=='undefined'){
				if(chkBranch.checked=="1"){
					ShowPopUp("MSI035_K.aspx?nTypeProv=" + cbeTypProvider.value + "&nProvider=" + tcnCodigo.value + "&sClient=" + dtcClient.value ,"MSI011",500,550,"no","no",200,80);
				}
			}
			else
				if(marrArray[lintIndex].chkBranch.checked=="1"){
					with (marrArray[lintIndex])
						ShowPopUp("MSI035_K.aspx?nTypeProv=" + cbeTypProvider + "&nProvider=" + tcnCodigo + "&sClient=" + dtcClient,"MSI011",500,550,"no","no",200,80);
				}
		}
	}
}
//% ShowGroup: Tipo de Provider dependiendo del Grupo
//------------------------------------------------------------------------------------------
function ShowGroup(){
//-------------------------------------------------------------------------------------------
	var lstrError = '0';
	with (self.document.forms[0]){
		if(chkGroup.checked=="1"){
			if (tcnCodigo.value==""){
			    alert('Err. 4116: <%=eFunctions.Values.GetMessage(4116)%>');
				lstrError = '1';
			}
			if (dtcClient.value==""){
			    alert('Err. 12043: <%=eFunctions.Values.GetMessage(12043)%>');
				lstrError = '1';
			}
			if (dtcClient_Digit.value==""){
			    alert('Err. 2090: <%=eFunctions.Values.GetMessage(2090)%>');
				lstrError = '1';
			}
		}
		if (lstrError=='0'){
			if(typeof(marrArray)=='undefined'){
			    with (self.document.forms[0])
				    if(chkGroup.checked=="1"){
					    ShowPopUp("MSI019_K.aspx?nProvider=" + tcnCodigo.value,"MSI011",500,550,"no","no",200,80);
					}
			}
			else
				if(marrArray[lintIndex].chkGroup.checked=="1"){
					with (marrArray[lintIndex])
				        ShowPopUp("MSI019_K.aspx?nProvider=" + tcnCodigo + "&sClient=" + dtcClient,"MSI011",500,550,"no","no",200,80);
				}
		}
	}
}
//% ShowAddress: Permite mostrar la Localidad del Cliente
//------------------------------------------------------------------------------------------
function ShowAddress(){
//------------------------------------------------------------------------------------------
	lstrQueryString = "/VTimeNet/Maintenance/MantClaim/ShowDefValues.aspx?Field=sClient";
    lstrQueryString = lstrQueryString + "&sClient=" + self.document.forms[0].dtcClient.value;
    ShowPopUp(lstrQueryString,"Values",1,1,"no","no", 2000, 2000);
}
//% insShowMSI647: Asociar Zonas(Comunas)
//------------------------------------------------------------------------------------------------------------
function insShowMSI647(mintProvider ,mintTypeProv){
//------------------------------------------------------------------------------------------------------------------------
	ShowPopUp('MSI647_K.aspx?nProvider=' + mintProvider + '&nTypeProv=' + mintTypeProv + '&nMainAction=' + nMainAction ,'MSI011',500,500,'no','no',200,80);
}
//% insShowMSI035: Asociar Ramos
//------------------------------------------------------------------------------------------------------------
function insShowMSI035(mintProvider ,mintTypeProv){
//------------------------------------------------------------------------------------------------------------------------
	ShowPopUp('MSI035_K.aspx?nProvider=' + mintProvider + '&nTypeProv=' + mintTypeProv ,'MSI011',500,450,'no','no',200,80);
}
//% insShowMSI019: Asociar Grupos
//------------------------------------------------------------------------------------------------------------------------
function insShowMSI019(mintProvider){
//-----------------------------------------------------------------------------------------	
	ShowPopUp('MSI019_K.aspx?nProvider=' + mintProvider,'MSI011',500,550,'no','no',200,80);
}
//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngRow))
				lstrURL = lstrURL + "&nRow=51"
			else{
				llngRow = insConvertNumber(llngRow) + 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
			break;

		case "Back":
			if(!isNaN(llngRow)){
				llngRow = insConvertNumber(llngRow) - 50;
				lstrURL = lstrURL + "&nRow=" + llngRow
			}
	}
	self.document.location.href = lstrURL;
}	
//% insCancelScript: Cancela PopUp
//------------------------------------------------------------------------------------------------------------------------
function insCancelScript(){
//-----------------------------------------------------------------------------------------	
    var strParams; 
	var lobjself_doc_form = self.document.forms[0]; 
	strParams = "nProvider=" + lobjself_doc_form.tcnCodigo.value + 
				"&nTypProvider=" + lobjself_doc_form.cbeTypProvider.value + 
				"&sClient=" + lobjself_doc_form.dtcClient.value;
 
	insDefValues("CancelUpdMsi011",strParams,'/VTimeNet/Maintenance/MantClaim'); 
} 
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmProviders" ACTION="valMantClaim.aspx?sMode=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR></BR>")
End If
Response.Write("<SCRIPT>var	nMainAction	= '" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Write("<BR>")
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMSI011_K()
Else
	Call insPreMSI011_K_Upd()
End If
%>
<%=mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow")))%>
<%=mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')", mintRowscount <> 50)%>
<%
mobjValues = Nothing%>
</FORM>
</BODY>
</HTML>





