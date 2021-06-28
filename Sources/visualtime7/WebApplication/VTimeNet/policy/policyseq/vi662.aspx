<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objetos para el manejo de las funciones generales de los grid que se muestran en la pantalla
Dim mobjGrid_Bas As eFunctions.Grid
Dim mobjGrid_Uni As eFunctions.Grid

Dim mobjMenu As eFunctions.Menues
Dim mclsLife_educ As ePolicy.life_educ

Dim mintGroup As Object
Dim mintSituation As Object
Dim mdblCapMax As Object
Dim mdblCapCos As Object
Dim mdblPercent As Object
Dim mstrOptTyp As String
Dim mstrOptNom As String
Dim mstrChkPre As String
Dim mstrChkUni As String
Dim mintCurrency As Object

Dim mblnOneTime As Boolean
Dim mblnFound As Boolean
Dim mblnSon As Boolean


'% insDefineHeader_B: se definen las propiedades del grid para Básico
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader_B()
	'--------------------------------------------------------------------------------------------
	Dim lstrQuery As String
	Dim lstrQueryString As String
	Dim mclsRoless As ePolicy.Roleses
	
	mobjGrid_Bas = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
	mobjGrid_Bas.sSessionID = Session.SessionID
	mobjGrid_Bas.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid_Bas.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid_Bas.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	mblnSon = False
	If Session("nCertif") > 0 Then
		'+ Se busca la figura 22)Hijo asociado a la póliza.
		mclsRoless = New ePolicy.Roleses
		If mclsRoless.Find_by_Policy(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), vbNullString, Session("dEffecdate"), 22) Then
			mblnSon = True
		End If
		mclsRoless = Nothing
	End If
	'+ Se definen las columnas del grid
	mobjGrid_Bas.sArrayName = "Bas"
	With mobjGrid_Bas.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnLevel_bColumnCaption"), "tcnLevel_b", "Table5546", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("tcnLevel_bColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_bColumnCaption"), "tcnCapital_b", 18, Request.QueryString.Item("nCapCos"),  , GetLocalResourceObject("tcnCapital_bColumnToolTip"), True, 6,  ,  ,  , Session("nCertif") <> 0)
		'+ Si no existe el rol hijo se muestra la columna número de alumnos.
		If Not mblnSon Then
			Call .AddNumericColumn(0, GetLocalResourceObject("tcnInsured_bColumnCaption"), "tcnInsured_b", 4, "",  , GetLocalResourceObject("tcnInsured_bColumnToolTip"))
		End If
		
		If CStr(Session("sPolitype")) <> "1" And Session("nCertif") <> 0 Then
			'+ Si existe el rol hijo
			If mblnSon Then
				If Request.QueryString.Item("Type") = "PopUp" Then
					lstrQueryString = "&sCertype=" & Session("sCertype") & "&nBranch=" & Session("nBranch") & "&nProduct=" & Session("nProduct") & "&nPolicy=" & Session("nPolicy") & "&nCertif=" & Session("nCertif") & "&dEffecdate=" & Session("dEffecdate")
					Call .AddClientColumn(0, GetLocalResourceObject("tctClient_bColumnCaption"), "tctClient_b", vbNullString,  , GetLocalResourceObject("tctClient_bColumnToolTip"),  ,  ,  ,  ,  ,  ,  ,  ,  , lstrQueryString,  , eFunctions.Values.eTypeClient.SearchClientPolicy)
					mobjGrid_Bas.Columns("tctClient_b").TypeList = 1
					mobjGrid_Bas.Columns("tctClient_b").ClientRole = "22"
				Else
					Call .AddClientColumn(0, GetLocalResourceObject("tctClient_bColumnCaption"), "tctClient_b", "",  , GetLocalResourceObject("tctClient_bColumnToolTip"))
				End If
			End If
		End If
		Call .AddHiddenColumn("hddsParam_B", vbNullString)
		Call .AddHiddenColumn("hddnId", vbNullString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid_Bas
		.Codispl = "VI662"
		.ActionQuery = mobjValues.ActionQuery
		.DeleteButton = Session("nCertif") = 0
		.AddButton = Session("nCertif") = 0
		
		If Session("nCertif") = 0 Then
			.Columns("tcnLevel_b").EditRecord = True
			.Columns("Sel").GridVisible = True
		Else
			.Columns("tcnLevel_b").EditRecord = True
			.Columns("Sel").GridVisible = True
		End If
		
		.Top = 100
		.Height = 250
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("tcnLevel_b").TypeOrder = 1
		
		lstrQuery = "sInBasUni=1" & "&nGroup=' + self.document.forms[0].valGroup.value + '" & "&nSituation=' + self.document.forms[0].valSituation.value + '" & "&nCapMax=' + self.document.forms[0].tcnCapital.value + '" & "&sOptTyp=' + (self.document.forms[0].optTyp[0].checked?1:2) + '" & "&nPercent=' + self.document.forms[0].tcnPercent.value + '" & "&nCapCos=' + self.document.forms[0].tcnCapitalCost.value + '" & "&sChkPre=' + (self.document.forms[0].chkPre.checked?1:2) + '" & "&sChkUni=' + (typeof(self.document.forms[0].chkUniver)=='undefined'?'" & Request.QueryString.Item("sChkUni") & "':(self.document.forms[0].chkUniver.checked?1:2)) + '" & "&nCurrency=' + self.document.forms[0].cbeCurrency.value + '"
		
		'+ Si se trata de una póliza matriz (Cotización o Propuesta).
		
		If (CStr(Session("sPoliType")) <> "1" And Session("nCertif") = 0) And (CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Or CStr(Session("sCertype")) = "2") Then
			lstrQuery = lstrQuery & "&sOptNom=' + (self.document.forms[0].optNomina[0].checked==true?1:2) + '"
		End If
		
		.sEditRecordParam = lstrQuery
		
		.sDelRecordParam = "' + Bas[lintIndex].hddsParam_B + '"
		
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sInBasUni") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insDefineHeader_U: se definen las propiedades del grid para Básico
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader_U()
	'--------------------------------------------------------------------------------------------
	Dim lstrQuery As String
	
	mobjGrid_Uni = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
	mobjGrid_Uni.sSessionID = Session.SessionID
	mobjGrid_Uni.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid_Uni.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid_Uni.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	mobjGrid_Uni.sArrayName = "Uni"
	With mobjGrid_Uni.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnLevel_uColumnCaption"), "tcnLevel_u", "Table5548", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  , Request.QueryString.Item("Action") = "Update", 4, GetLocalResourceObject("tcnLevel_uColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCapital_uColumnCaption"), "tcnCapital_u", 18, Request.QueryString.Item("nCapCos"),  , GetLocalResourceObject("tcnCapital_uColumnToolTip"), True, 6,  ,  ,  , Session("nCertif") <> 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnInsured_uColumnCaption"), "tcnInsured_u", 4, "",  , GetLocalResourceObject("tcnInsured_uColumnToolTip"))
		Call .AddHiddenColumn("hddsParam_U", vbNullString)
		Call .AddHiddenColumn("hddnId", vbNullString)
	End With
	'+ Se definen las propiedades generales del grid
	With mobjGrid_Uni
		.Codispl = "VI662"
		.ActionQuery = mobjValues.ActionQuery
		.DeleteButton = Session("nCertif") = 0
		.AddButton = Session("nCertif") = 0
		.Columns("tcnLevel_u").TypeOrder = 1
		
		If Session("nCertif") = 0 Then
			If mstrOptNom = "1" Then
				.Columns("tcnLevel_u").EditRecord = True
				.Columns("Sel").GridVisible = True
			Else
				.Columns("tcnLevel_u").EditRecord = False
				.Columns("Sel").GridVisible = False
			End If
		Else
			.Columns("tcnLevel_u").EditRecord = True
			.Columns("Sel").GridVisible = True
		End If
		.Top = 100
		.Height = 230
		.Width = 300
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		lstrQuery = "sInBasUni=2" & "&nGroup=' + self.document.forms[0].valGroup.value + '" & "&nSituation=' + self.document.forms[0].valSituation.value + '" & "&nCapMax=' + self.document.forms[0].tcnCapital.value + '" & "&sOptTyp=' + (self.document.forms[0].optTyp[0].checked?1:2) + '" & "&nPercent=' + self.document.forms[0].tcnPercent.value + '" & "&nCapCos=' + self.document.forms[0].tcnCapitalCost.value + '" & "&sChkPre=' + (self.document.forms[0].chkPre.checked?1:2) + '" & "&sChkUni=' + (self.document.forms[0].chkUniver.checked?1:2) + '" & "&nCurrency=' + self.document.forms[0].cbeCurrency.value + '"
		
		'+ Si se trata de una póliza matriz (Cotización o Propuesta).
		If (CStr(Session("sPoliType")) <> "1" And Session("nCertif") = 0) And (CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Or CStr(Session("sCertype")) = "2") Then
			lstrQuery = lstrQuery & "&sOptNom=' + (self.document.forms[0].optNomina[0].checked==true?1:2) + '"
		End If
		
		.sEditRecordParam = lstrQuery
		.sDelRecordParam = "' + Uni[lintIndex].hddsParam_U + '"
		If Request.QueryString.Item("Reload") = "1" And Request.QueryString.Item("sInBasUni") = "2" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreVi662_B: se realiza el manejo del grid para básico
'--------------------------------------------------------------------------------------------
Private Sub insPreVi662_B()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife_levels As ePolicy.life_levels
	Dim lclsLife_levelss As ePolicy.life_levelss
	Dim lintIndex As Short
	
	lclsLife_levels = New ePolicy.life_levels
	lclsLife_levelss = New ePolicy.life_levelss
	
	lintIndex = 0
	If lclsLife_levelss.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), CStr(1), mstrOptNom) Then
		For	Each lclsLife_levels In lclsLife_levelss
			With mobjGrid_Bas
				
				If Session("nCertif") > 0 Then
					.Columns("Sel").Checked = lclsLife_levels.nExists
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ", ""Bas"")"
				End If
				
				.Columns("tcnLevel_b").DefValue = CStr(lclsLife_levels.nLevel)
				.Columns("tcnCapital_b").DefValue = CStr(lclsLife_levels.nCapital)
				'+ Si no existe el rol hijo se muestra la columna número de alumnos.
				If Not mblnSon Then
					.Columns("tcnInsured_b").DefValue = CStr(lclsLife_levels.nInsured)
				End If
				
				.Columns("hddsParam_B").DefValue = "nLevel=" & lclsLife_levels.nLevel & "&sTyplevel=" & lclsLife_levels.sTyplevel & "&nId=" & lclsLife_levels.nId & "&nGroup=" & lclsLife_levels.nGroup & "&sInBasUni=1"
				.Columns("hddnId").DefValue = CStr(lclsLife_levels.nId)
				If CStr(Session("sPolitype")) <> "1" And Session("nCertif") <> 0 Then
					If mblnSon Then
						.Columns("tctClient_b").DefValue = lclsLife_levels.sClient
					End If
				End If
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsLife_levels
	End If
	Response.Write(mobjGrid_Bas.closeTable())
	lclsLife_levels = Nothing
	lclsLife_levelss = Nothing
End Sub

'% insPreVi662_B: se realiza el manejo del grid para básico
'--------------------------------------------------------------------------------------------
Private Sub insPreVi662_U()
	'--------------------------------------------------------------------------------------------
	Dim lclsLife_levels As ePolicy.life_levels
	Dim lclsLife_levelss As ePolicy.life_levelss
	Dim lintIndex As Short
	
	lclsLife_levels = New ePolicy.life_levels
	lclsLife_levelss = New ePolicy.life_levelss
	
	lintIndex = 0
	If lclsLife_levelss.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), CStr(2), mstrOptNom) Then
		For	Each lclsLife_levels In lclsLife_levelss
			With mobjGrid_Uni
				
				If Session("nCertif") > 0 Then
					.Columns("Sel").Checked = lclsLife_levels.nExists
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ", ""Uni"")"
				End If
				
				.Columns("tcnLevel_u").DefValue = CStr(lclsLife_levels.nLevel)
				.Columns("tcnCapital_u").DefValue = CStr(lclsLife_levels.nCapital)
				.Columns("tcnInsured_u").DefValue = CStr(lclsLife_levels.nInsured)
				
				
				.Columns("hddsParam_U").DefValue = "nLevel=" & lclsLife_levels.nLevel & "&sTyplevel=" & lclsLife_levels.sTyplevel & "&nId=" & lclsLife_levels.nId & "&nGroup=" & lclsLife_levels.nGroup
				.Columns("hddnId").DefValue = CStr(lclsLife_levels.nId)
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsLife_levels
	End If
	Response.Write(mobjGrid_Uni.closeTable())
	lclsLife_levels = Nothing
	lclsLife_levelss = Nothing
End Sub
'% insPreVi662Upd: Se realiza el manejo de la ventana PopUp asociada a los diferentes grid
'------------------------------------------------------------------------------------------------------------------------------------------------------
Private Sub insPreVi662Upd()
	'------------------------------------------------------------------------------------------------------------------------------------------------------
	Dim lclsLife_levels As ePolicy.life_levels
	lclsLife_levels = New ePolicy.life_levels
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lclsLife_levels.InsPostvi662(Request.QueryString.Item("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("sTyplevel"), mobjValues.StringToType(.QueryString.Item("nLevel"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(.QueryString.Item("nId"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, vbNullString, Session("nUsercode"), Session("sPolitype"), Session("sBrancht")) Then
			End If
			lclsLife_levels = Nothing
		End If
		If Request.QueryString.Item("sInBasUni") = "1" Then
			Response.Write(mobjGrid_Bas.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI662", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		Else
			Response.Write(mobjGrid_Uni.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "VI662", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		End If
	End With
	If Session("nCertif") = 0 Then
		
Response.Write("" & vbCrLf)
Response.Write("  		<SCRIPT>" & vbCrLf)
Response.Write("  			if(typeof(top.opener.document.forms[0].optNomina)!='undefined')" & vbCrLf)
Response.Write("				if (top.opener.document.forms[0].optNomina[1].checked)" & vbCrLf)
Response.Write("					if(typeof(self.document.forms[0].tcnInsured_b)!='undefined')" & vbCrLf)
Response.Write("						self.document.forms[0].tcnInsured_b.disabled = true" & vbCrLf)
Response.Write("		</" & "SCRIPT>" & vbCrLf)
Response.Write("		")

		
	End If
End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mblnOneTime = True
	If Request.QueryString.Item("nGroup") = vbNullString Then
		mintGroup = 0
	Else
		mintGroup = Request.QueryString.Item("nGroup")
		mblnOneTime = False
	End If
	
	If Request.QueryString.Item("nSituation") = vbNullString Then
		mintSituation = vbNullString
	Else
		mintSituation = Request.QueryString.Item("nSituation")
		mblnOneTime = False
	End If
	
	If Request.QueryString.Item("nPercent") = vbNullString Then
		mdblPercent = 0
	Else
		mdblPercent = Request.QueryString.Item("nPercent")
		mblnOneTime = False
	End If
	
	If Request.QueryString.Item("nCapMax") = vbNullString Then
		mdblCapMax = Request.QueryString.Item("nCapMax")
	Else
		mdblCapMax = Request.QueryString.Item("nCapMax")
		mblnOneTime = False
	End If
	
	If Request.QueryString.Item("nCapCos") = vbNullString Then
		mdblCapCos = Request.QueryString.Item("nCapCos")
	Else
		mdblCapCos = Request.QueryString.Item("nCapCos")
		mblnOneTime = False
	End If
	
	
	If Request.QueryString.Item("sOptTyp") = vbNullString Then
		mstrOptTyp = "1"
	Else
		mstrOptTyp = Request.QueryString.Item("sOptTyp")
		mblnOneTime = False
	End If
	
	
	If Request.QueryString.Item("sField") = "optNomina" Then
		If Request.QueryString.Item("sOptNom") = vbNullString Then
			mstrOptNom = "1"
		Else
			mstrOptNom = Request.QueryString.Item("sOptNom")
			mblnOneTime = False
		End If
	End If
	
	If Request.QueryString.Item("sChkPre") = vbNullString Then
		mstrChkPre = "1"
	Else
		mstrChkPre = Request.QueryString.Item("sChkPre")
		mblnOneTime = False
	End If
	
	If Request.QueryString.Item("sChkUni") = vbNullString Then
		mstrChkUni = "1"
	Else
		mstrChkUni = Request.QueryString.Item("sChkUni")
		mblnOneTime = False
	End If
	
	If Request.QueryString.Item("nCurrency") = vbNullString Then
		mintCurrency = 0
	Else
		mintCurrency = Request.QueryString.Item("nCurrency")
		mblnOneTime = False
	End If
	
End Sub

'% insDefValues: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insDefValues()
	'-----------------------------------------------------------------------------------------
	mblnFound = False
	
	
	'+ Si es la primera vez
	'+ Se obtienen los valores por defecto
	
	'+ Buscamos el primer grupo si la póliza tiene grupos definidos
	Dim mclsCertificat As ePolicy.Certificat
	If mblnOneTime Then
		'Dim mclsGroups
		'Set mclsGroups = Server.CreateObject("ePolicy.Groupss")
		
		'If mclsGroups.Find(Session("sCertype"), 		'				   Session("nBranch"), 		'				   Session("nProduct"), 		'				   Session("nPolicy"), 		'				   Session("dEffecdate")) Then
		'	mintGroup = mclsGroups.Item(1).nGroup
		'Else
		'	mintGroup = 0
		'End If
		'Set mclsGroups = Nothing
		
		mclsCertificat = New ePolicy.Certificat
		
		If mclsCertificat.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
			mintGroup = mclsCertificat.nGroup
		Else
			mintGroup = 0
		End If
		mclsCertificat = Nothing
		
	End If
	
	If mclsLife_educ.insPreVI662(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintCurrency, eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), Session("nTransaction")) Then
		mblnFound = True
		
		mdblCapCos = mclsLife_educ.nCost_Anual
		mintSituation = mclsLife_educ.nSituation
		mdblCapMax = mclsLife_educ.nCapital
		mdblPercent = mclsLife_educ.nPercentsec
		
		If Request.QueryString.Item("sField") <> "optNomina" Then
			If mclsLife_educ.sTypenom = "1" Then
				mstrOptNom = "1"
			Else
				mstrOptNom = "2"
			End If
		End If
		
		If mblnOneTime Then
			
			If mclsLife_educ.sPrebasic = "1" Then
				mstrChkPre = "1"
			Else
				mstrChkPre = "2"
			End If
			
			If mclsLife_educ.sHighschool = "1" Then
				mstrChkUni = "1"
			Else
				mstrChkUni = "2"
			End If
			
			If CDbl(mclsLife_educ.sTypinsur) = 1 Then
				mstrOptTyp = "1"
			Else
				mstrOptTyp = "2"
			End If
		End If
		mintCurrency = mclsLife_educ.nCurrency
	Else
		If Request.QueryString.Item("sField") <> "optNomina" Then
			mstrOptNom = "1"
		End If
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI662")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.14
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsLife_educ = New ePolicy.life_educ

mobjValues.ActionQuery = Session("bQuery")
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 21/04/04 17:54 $|$$Author: Nvaplat37 $"

	var mintGroup = 0; mstrOptNom = '0'; nMainAction = 304;

//
//
//
//
//No borrar estas lineas, proboca un error en la actualización
//de la pagina cuando se sube a SS. (Luis Moreno).
//
//
//
//
//
//
//
//% onChangeoptTyp: Controla la habilitación del campo %Segundo sostenedor
//--------------------------------------------------------------------------------------------
function OnChangeoptTyp(nValue){
//-------------------------------------------------------------------------------
	if (nValue == 1){
		self.document.forms[0].tcnPercent.disabled = true;
		self.document.forms[0].tcnPercent.value = 0
		}
	else{
		self.document.forms[0].tcnPercent.disabled = false;
		self.document.forms[0].tcnPercent.value = 0;
		}
}

//% insChangeUniver: Controla la habilitación del botón agregar y eliminar del grid Curso
//--------------------------------------------------------------------------------------------
function insChangeUniver(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (chkUniver.checked==true) {
			if(typeof(cmdAddUni)!='undefined')
				cmdAddUni.disabled = false
			if(typeof(cmdDeleteUni)!='undefined')
				cmdDeleteUni.disabled = false
		}
		else {
			if(typeof(cmdAddUni)!='undefined')
				cmdAddUni.disabled = true
			if(typeof(cmdDeleteUni)!='undefined')
				cmdDeleteUni.disabled = true
		}
    }
}

//% insValues: Manejo de inicio de controles
//--------------------------------------------------------------------------------------------
function insValues(){
//--------------------------------------------------------------------------------------------
	var nTransaction = "<%=Session("nTransaction")%>"
	var nCertif = "<%=Session("nCertif")%>"
	with(self.document.forms[0]){
		if (nCertif == "0"){
			valSituation.disabled    = true
			if (nTransaction == "1" ||
				nTransaction == "3" ||
				nTransaction == "4" ||
				nTransaction == "24"||
				nTransaction == "28" ){
				if ("<%=mclsLife_educ.bGroups%>"=="True"){
					valGroup.disabled    = false
				}
			}
			else{
				valGroup.disabled    = true
			}
		}
		else{
			valSituation.disabled    = false
		}
	}
}

//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(Obj){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    var lblnChange
    with (self.document.forms[0]) {

//+ Si el campo tipo de nómina existe como objeto

		lstrQuery = "&sInBasUni=" + ('<%=Request.QueryString.Item("sInBasUni")%>'=='2'?2:1) ;

//+ Si existe cambio de valor en el campo grupo
		if (mintGroup!=(valGroup.value==''?0:valGroup.value)) {
			lblnChange = true;
			mintGroup = valGroup.value;
		}

		if(typeof(optNomina)!='undefined')
			if (mstrOptNom!=(optNomina[0].checked==true?1:2)) {
				lblnChange = true;
				mstrOptNom = (optNomina[0].checked==true?1:2);
				lstrQuery = lstrQuery + "&sOptNom=" + (optNomina[0].checked==true?1:2);
			}

		if (lblnChange==true) {
			lstrQuery = lstrQuery + "&nGroup=" + valGroup.value + "&nSituation=" + valSituation.value;
			lstrQuery = lstrQuery + "&nCapMax=" + tcnCapital.value + "&sOptTyp=" + (optTyp[0].checked==true?1:2);
			lstrQuery = lstrQuery + "&nPercent=" + tcnPercent.value + "&sField=" + Obj.name;
			lstrQuery = lstrQuery + "&nCapCos=" + tcnCapitalCost.value + "&sChkPre=" + (chkPre.checked==true?1:2);
			lstrQuery = lstrQuery + "&sChkUni=" + (chkUniver.checked==true?1:2) + "&nCurrency=" + cbeCurrency.value;
			document.location.href = document.location.href.replace(/&sInBasUni=.*/,'') + lstrQuery
		}
    }
}

//% insCheckSelClick:
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex, sArray){
//-------------------------------------------------------------------------------------------
	var lstrQuery;

	with (document.forms[0]) {
	    if (!Field.checked){

			if (sArray=='Bas')
				lstrQuery = Bas[lintIndex].hddsParam_B
			else
				lstrQuery = Uni[lintIndex].hddsParam_U

			insDefValues("UpdateCheckVI662", lstrQuery)
		}
		else {
			Field.checked = !Field.checked
	//+ Si existe cambio de valor en el campo grupo
			lstrQuery = "sInBasUni=" + (sArray=='Bas'?1:2);

			if(typeof(optNomina)!='undefined')
				lstrQuery = lstrQuery + "&sOptNom=" + (optNomina[0].checked==true?1:2);

			lstrQuery = lstrQuery + "&nGroup=" + valGroup.value + "&nSituation=" + valSituation.value;
			lstrQuery = lstrQuery + "&nCapMax=" + tcnCapital.value + "&sOptTyp=" + (optTyp[0].checked==true?1:2);
			lstrQuery = lstrQuery + "&nPercent=" + tcnPercent.value;
			lstrQuery = lstrQuery + "&nCapCos=" + tcnCapitalCost.value + "&sChkPre=" + (chkPre.checked==true?1:2);
			lstrQuery = lstrQuery + "&sChkUni=" + (chkUniver.checked==true?1:2) + "&nCurrency=" + cbeCurrency.value;

			if (sArray=='Bas')
				EditRecordBas(lintIndex,nMainAction, "Update", lstrQuery)
			else
				EditRecordUni(lintIndex,nMainAction, "Update", lstrQuery)
		}
	}
}
</SCRIPT>
	<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI662", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	Call insReaInitial()
	Call insDefValues()
End If

%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI662" ACTION="ValPolicySeq.aspx?sMode=2&sInBasUni=<%=Request.QueryString.Item("sInBasUni")%>">
    <%Response.Write(mobjValues.ShowWindowsName("VI662", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader_B()
Call insDefineHeader_U()

Call mclsLife_educ.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), 0, mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"))

If Request.QueryString.Item("Type") <> "PopUp" Then
	
	%>
    <TABLE WIDTH="100%" BORDER=0>
		<TR>
			<TD WIDTH=25%><LABEL ID=0><%= GetLocalResourceObject("valGroupCaption") %></LABEL></TD>
		    <TD WIDTH=25%><%	
	With mobjValues
		.Parameters.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("valGroup", "TabGroups", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble, True), True,  ,  ,  ,  , "insReload(this)", Not mclsLife_educ.bGroups Or CStr(Session("nCertif")) > "0", 4, GetLocalResourceObject("valGroupToolTip")))
	Response.Write("<SCRIPT> mintGroup = '" & mintGroup & "'; </SCRIPT>")
	%>
		    </TD>

            <%	If (CStr(Session("sPoliType")) <> "1" And Session("nCertif") = 0) And (CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Or CStr(Session("sCertype")) = "2") Then
		%>
				<TD COLSPAN="2" WIDTH = 50% CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<%	Else%>
	            <TD WIDTH = 50%>&nbsp;</TD>
			<%	End If%>
		</TR>
            <%	If (CStr(Session("sPoliType")) <> "1" And Session("nCertif") = 0) And (CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Or CStr(Session("sCertype")) = "2") Then%>
		        <TR>
                    <TD COLSPAN="2"></TD>
                    <TD COLSPAN="2" CLASS="Horline"></TD>
                </TR>
            <%	End If%>
		<TR>
			<TD><LABEL ID=13531><%= GetLocalResourceObject("valSituationCaption") %></LABEL></TD>
			<%	
	With mobjValues.Parameters
		.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	%>

			<TD><%	Response.Write(mobjValues.PossiblesValues("valSituation", "tabSituation", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mintSituation, eFunctions.Values.eTypeData.etdDouble), True,  ,  ,  ,  ,  , Not mclsLife_educ.bSituation,  , GetLocalResourceObject("valSituationToolTip"),  , 2))%></TD>

            <%	If (CStr(Session("sPoliType")) <> "1" And Session("nCertif") = 0) And (CStr(Session("sCertype")) = "1" Or CStr(Session("sCertype")) = "3" Or CStr(Session("sCertype")) = "2") Then
		%>  
  	            <TD><%		If mstrOptNom = "1" Then
			Response.Write(mobjValues.OptionControl(10, "optNomina", GetLocalResourceObject("optNomina_CStr1Caption"), CStr(1), CStr(1), "insReload(this)", Not mclsLife_educ.bTipnom))
		Else
			Response.Write(mobjValues.OptionControl(10, "optNomina", GetLocalResourceObject("optNomina_CStr1Caption"), CStr(2), CStr(1), "insReload(this)", Request.QueryString.Item("sField") <> "optNomina"))
		End If
		%>
				</TD>
  	            <TD>
  					<%		If mstrOptNom = "1" Then
			Response.Write(mobjValues.OptionControl(10, "optNomina", GetLocalResourceObject("optNomina_CStr2Caption"), CStr(2), CStr(2), "insReload(this)", Not mclsLife_educ.bTipnom))
		Else
			Response.Write(mobjValues.OptionControl(10, "optNomina", GetLocalResourceObject("optNomina_CStr2Caption"), CStr(1), CStr(2), "insReload(this)", Request.QueryString.Item("sField") <> "optNomina"))
		End If
		Response.Write("<SCRIPT> mstrOptNom = '" & mstrOptNom & "'; </SCRIPT>")
		%>

				</TD>
  	        <%	Else%>
  	            <TD COLSPAN="2"></TD>
  	        <%	End If%>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCapitalCaption") %></LABEL></TD>
  	        <TD><%	
	'+ Si no se ha asignado valor desde el query string se toma el de la tabla
	If mobjValues.StringToType(mdblCapMax, eFunctions.Values.eTypeData.etdDouble) <= 0 Then
		mdblCapMax = mobjValues.StringToType(CStr(mclsLife_educ.nCapital), eFunctions.Values.eTypeData.etdDouble)
	End If
	
	Response.Write(mobjValues.NumericControl("tcnCapital", 18, mdblCapMax,  , GetLocalResourceObject("tcnCapitalToolTip"), True, 6,  ,  ,  ,  , Session("nCertif") <> 0))
	%>
  			</TD>
  	        <TD>&nbsp;</TD>
  	        <TD>&nbsp;</TD>
        </TR>
		<TR>
			<TD COLSPAN="4">
				<TABLE WIDTH="100%" BORDER=0>
				   <TR>
						<TD  COLSPAN="4">&nbsp;</TD>
				   </TR>
					<TR>
					    <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
					</TR>
					<TR>
					    <TD COLSPAN="4" CLASS="Horline"></TD>
					</TR>
				   <TR>
						<TD WIDTH="40%">
							<%	If mstrOptTyp = "1" Then
		Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_CStr1Caption"), CStr(1), CStr(1), "OnChangeoptTyp(1);"))
	Else
		Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_CStr1Caption"), CStr(2), CStr(1), "OnChangeoptTyp(1);"))
	End If
	%>
						</TD>
						<TD WIDTH="30%"><LABEL ID=0><%= GetLocalResourceObject("tcnPercentCaption") %></LABEL></TD>
						<TD WIDTH="30%"><%=mobjValues.NumericControl("tcnPercent", 5, mdblPercent,  , GetLocalResourceObject("tcnPercentToolTip"),  , 2,  ,  ,  ,  , mdblPercent <= 0)%></TD>
				   </TR>
				   <TR>
						<TD>
							<%	If mstrOptTyp = "1" Then
		Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_CStr2Caption"), CStr(2), CStr(2), "OnChangeoptTyp(2);"))
	Else
		Response.Write(mobjValues.OptionControl(0, "optTyp", GetLocalResourceObject("optTyp_CStr2Caption"), CStr(1), CStr(2), "OnChangeoptTyp(2);"))
	End If
	%>
						</TD>
						<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCapitalCostCaption") %></LABEL></TD>
						<TD><%=mobjValues.NumericControl("tcnCapitalCost", 18, mdblCapCos,  , GetLocalResourceObject("tcnCapitalCostToolTip"), True, 6)%></TD>
				   </TR>
				   <TR>
						<TD>
							<%	If mstrChkPre = "1" Then
		Response.Write(mobjValues.CheckControl("chkPre", GetLocalResourceObject("chkPreCaption"), CStr(1), CStr(1)))
	Else
		Response.Write(mobjValues.CheckControl("chkPre", GetLocalResourceObject("chkPreCaption"), CStr(2), CStr(1)))
	End If
	%>
						</TD>
						<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
						<TD><%	
	mobjValues.TypeList = 1
	mobjValues.List = mclsLife_educ.sCurren_pol
	mobjValues.BlankPosition = False
	Response.Write(mobjValues.PossiblesValues("cbeCurrency", "table11", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(mintCurrency, eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCurrencyToolTip")))
	%>
						</TD>
				   </TR>
				    <TR>
					  <TD COLSPAN="3">
					     <%	Call insPreVi662_B()%>
					  </TD>
				   </TR>
				</TABLE>
			</TD>
        </TR>
        <TR>
            <TD  COLSPAN="4">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="4" CLASS="Horline"></TD>
        </TR>
		<TR>
			<TD COLSPAN="4">
				<TABLE WIDTH="100%" BORDER=0>
					<TR>
						<TD WIDTH="40%">
							<%	
	If mstrChkUni = "1" Then
		Response.Write(mobjValues.CheckControl("chkUniver", GetLocalResourceObject("chkUniverCaption"), CStr(1), CStr(1), "insChangeUniver()"))
	Else
		Response.Write(mobjValues.CheckControl("chkUniver", GetLocalResourceObject("chkUniverCaption"), CStr(2), CStr(1), "insChangeUniver(this)"))
	End If
	%>
						</TD>
					</TR>
					<TR>
						<TD COLSPAN="3">
						<%	Call insPreVi662_U()%>
						</TD>
					</TR>
				</TABLE>
			</TD>
        </TR>
    </TABLE>
	<%	If mobjValues.ActionQuery = False Then
		%>
<SCRIPT>
			insValues();
			insChangeUniver();
</SCRIPT>

<%		
	End If
Else
	Call insPreVi662Upd()
End If

mobjValues = Nothing
mobjMenu = Nothing
mclsLife_educ = Nothing
mobjGrid_Bas = Nothing
mobjGrid_Uni = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.14
Call mobjNetFrameWork.FinishPage("VI662")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




