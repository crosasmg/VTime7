<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

Dim mobjMantProduct As Object
Dim mstrErrors As String
Dim mobjValues As eFunctions.Values

'+ Se define la constante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'+ [APV2] HAD 1022. Tasas de interes garantizada APV
'- Variable auxiliar para pase de valores del encabezado al folder.
Dim mstrQueryString As String


'% insValMantProduct: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantProduct() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MDP001: Secuencia de ventanas para proceso de productos
		Case "MDP001"
			mobjMantProduct = New eProduct.Tab_winpro
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantProduct = mobjMantProduct.insValMDP001_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), 1, mobjValues.StringToType(.Form.Item("cbeBranchType"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("nMainAction") <> "401" And Session("nValue") > 0 Then
						insValMantProduct = mobjMantProduct.insValMDP001(.QueryString("sCodispl"), .QueryString("Action"), 1, mobjValues.StringToType(CStr(.Form.Item("Sel").Length), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ MDP8050: Tabla de rentabilidad mensual
		Case "MDP8050"
			mobjMantProduct = New eProduct.Plan_Intwar_Month
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantProduct = mobjMantProduct.insValMDP8050_K(mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbenTypeInvest"), eFunctions.Values.eTypeData.etdLong))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantProduct = mobjMantProduct.insValMDP8050(.QueryString("Action"), mobjValues.StringToType(Session("nYear"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nTypeInvest"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbenMonth"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
			
			'+ MDP002: Archivos asociados a datos particulares.
		Case "MDP002"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjMantProduct = New eProduct.Win_file_g
					insValMantProduct = mobjMantProduct.insValMDP002("MDP002", mobjValues.StringToType(.Form.Item("cboBranch_gen"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctTabname"))
				End If
			End With
			
			'+ MDP8003: Columnas permitidas en una tabla lógica de tarifas.
		Case "MDP8003"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjMantProduct = New eTarif.tarif_column
					
					insValMantProduct = mobjMantProduct.insValMDP8003("MDP8003", .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnId_column"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctTable"), .Form.Item("tctColumn"), .Form.Item("tctName_col"), mobjValues.StringToType(.Form.Item("hdddata_type"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctsize"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tctdecimal"), eFunctions.Values.eTypeData.etdLong), .Form.Item("tctdata_type"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
				End If
			End With
			
			
			'+ MDP037: Tabla genérica de corto plazo			
		Case "MDP037"
			mobjMantProduct = New eProduct.Tab_short
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantProduct = mobjMantProduct.insValMDP037_K("MDP037", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantProduct = mobjMantProduct.insValMDP037("MDP037", .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDays"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremDev"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate_MDP037"))
					Else
						insValMantProduct = vbNullString
					End If
				End If
			End With
			
			'+ [APV2] HAD 1022. Tasas de interes garantizada APV
			'+ MDP7001: Tasas de interes garantizada APV
		Case "MDP7001"
			mobjMantProduct = New eProduct.Tab_apv_warran
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
						insValMantProduct = mobjMantProduct.insValMDP7001_K(.QueryString("sCodispl"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), .QueryString("nMainAction"))
					End If
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantProduct = mobjMantProduct.insValMDP7001(.QueryString("sCodispl"), Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.QueryString.Item("nTable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nMax_year_Aux"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), .QueryString("Action"))
					End If
				End If
			End With
		Case Else
			insValMantProduct = "insValMantProduct: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantAgent: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantProduct() As Boolean
	Dim lintCount As Integer
	Dim lstrRequire As String
	Dim lstrChecked As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	With Request
		Select Case .QueryString.Item("sCodispl")
			
			'+ MDP001: Secuencia de ventanas para proceso de productos
			Case "MDP001"
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("sBrancht") = Request.Form.Item("cbeBranchType")
				Else
					lintCount = 0
					
					For lintCount = 1 To Session("nValue")
						If .Form.GetValues("tcnChecked").GetValue(lintCount - 1) = "1" Then
							lstrChecked = "1"
						Else
							lstrChecked = "2"
						End If
						
						If .Form.GetValues("tcnRChecked").GetValue(lintCount - 1) = "1" Then
							lstrRequire = "1"
						Else
							lstrRequire = "2"
						End If
						lblnPost = mobjMantProduct.insPostMDP001(Session("sBrancht"), mobjValues.StringToType(.Form.GetValues("tcnSequen").GetValue(lintCount - 1), eFunctions.Values.eTypeData.etdDouble), .Form.GetValues("tctCodispl2").GetValue(lintCount - 1), lstrRequire, lstrChecked, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Next 
					Session("sBrancht") = vbNullString
					Session("nValue") = 0
					lintCount = Nothing
					lstrRequire = Nothing
				End If
				lblnPost = True
				
				'+ MDP002: Archivos asociados a datos particulares.
			Case "MDP002"
				With Request
					mobjMantProduct = New eProduct.Win_file_g
					lblnPost = True
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantProduct.insPostMDP002(.Form.Item("tctCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cboBranch_gen"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctTabname"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End With
				
				'+ MDP8003: Columnas permitidas en una tabla lógica de tarifas.
			Case "MDP8003"
				With Request
					mobjMantProduct = New eTarif.tarif_column
					lblnPost = True
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantProduct.insPostMDP8003(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnId_column"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctTable"), .Form.Item("tctColumn"), .Form.Item("tctName_col"), mobjValues.StringToType(.Form.Item("hdddata_type"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctsize"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tctdecimal"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctdata_type"), Session("nUsercode"), .Form.Item("tctTablefk"))
					End If
				End With
				
				'+ MDP037: Tabla genérica de corto plazo
			Case "MDP037"
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Session("dEffecdate_MDP037") = .Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantProduct.insPostMDP037("MDP037", Request.QueryString.Item("Action"), 1, mobjValues.StringToType(.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDays"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremCash"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnPremDev"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate_MDP037"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
				
				'+ [APV2] HAD 1022. Tasas de interes garantizada APV
				'+ MDP7001: Tasas de interes garantizada APV
			Case "MDP7001"
				mobjMantProduct = New eProduct.Tab_apv_warran
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&nTable=" & mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdDouble)
						Session("nBranch") = mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble)
						Session("nProduct") = mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
						If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
							lblnPost = mobjMantProduct.insPostMDP7001_K(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.Form.Item("valWarrn_table"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctDescript"), Session("nUsercode"), .QueryString("nMainAction"))
						Else
							lblnPost = True
						End If
					Else
						If .QueryString.Item("WindowType") = "PopUp" Then
							mstrQueryString = "&nTable=" & mobjValues.StringToType(.QueryString.Item("nTable"), eFunctions.Values.eTypeData.etdDouble)
							lblnPost = mobjMantProduct.insPostMDP7001(Session("nBranch"), Session("nProduct"), mobjValues.StringToType(.QueryString.Item("nTable"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax_year"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"), .QueryString("Action"))
						Else
							lblnPost = True
						End If
					End If
				End With
				
				'+ MDP8050: Tasas de rentabilidad mensual
			Case "MDP8050"
				mobjMantProduct = New eProduct.Plan_Intwar_Month
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						Session("nYear") = mobjValues.StringToType(.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble)
						Session("nTypeInvest") = mobjValues.StringToType(.Form.Item("cbenTypeInvest"), eFunctions.Values.eTypeData.etdDouble)
						
						lblnPost = True
						
					Else
						If .QueryString.Item("WindowType") = "PopUp" Then
							lblnPost = mobjMantProduct.insPostMDP8050(.QueryString("Action"), Session("nYear"), Session("nTypeInvest"), mobjValues.StringToType(.Form.Item("cbenMonth"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate_sec"), eFunctions.Values.eTypeData.etdDouble), Session("nUsercode"))
						Else
							lblnPost = True
						End If
					End If
				End With
		End Select
	End With
	insPostMantProduct = lblnPost
End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mstrCommand = "sModule=Maintenance&sProject=MantProduct&sCodisplReload=" & Request.QueryString.Item("sCodispl")
mstrQueryString = vbNullString

%>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>



	 

<SCRIPT>
//% NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
</HEAD>
<BODY>
<FORM id=form1 name=form1>
<%
'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantProduct
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantProductError"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantProduct Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						
						'+ [APV2] HAD 1022. Tasas de interes garantizada APV
						If Request.QueryString.Item("sCodispl") = "MDP7001" And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
							Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
						Else
							Response.Write("<SCRIPT>;self.history.go(-1);top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
						End If
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				End If
			End If
		Else
			
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				Case "MDP001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MDP001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
				Case "MDP8050"
					Response.Write("<SCRIPT>top.opener.document.location.href='MDP8050.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
				Case "MDP037"
					Response.Write("<SCRIPT>top.opener.document.location.href='MDP037.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
				Case "MDP002"
					Response.Write("<SCRIPT>top.opener.document.location.href='MDP002_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
				Case "MDP8003"
					Response.Write("<SCRIPT>top.opener.document.location.href='MDP8003_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "' </SCRIPT>")
					
					'+ [APV2] HAD 1022. Tasas de interes garantizada APV
				Case "MDP7001"
					Response.Write("<SCRIPT>top.opener.document.location.href='MDP7001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & "' </SCRIPT>")
			End Select
		End If
	End If
	
	If Request.QueryString.Item("nMainAction") = "401" Then
		Session("bQuery") = True
	Else
		Session("bQuery") = False
	End If
End If
mobjMantProduct = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
</BODY>
</HTML>





