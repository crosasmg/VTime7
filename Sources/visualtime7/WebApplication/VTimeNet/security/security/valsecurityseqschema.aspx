<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values
Dim lobjSecuritySeq As eSecurity.Secur_sche
Dim mstrErrors As String
Dim mstrFinish As Object
Dim mobjSG021 As eSecurity.SecurScheSurr
Dim mobjSG855 As eSecurity.SchemaFolder

'+ Se define la constante para el manejo de errores en caso de advertencias.

Dim mstrCommand As String
Dim lclsGeneral As eGeneral.GeneralFunction



'% insValSecuritySchema: Se realizan las validaciones masivas de cada una de las páginas.
'--------------------------------------------------------------------------------------------
Function insValSecurityShema() As String
	Dim llngOffice As String
	Dim blnShowErr As Boolean
	Dim llngConcept As String
	'--------------------------------------------------------------------------------------------
	Dim lobjError As eFunctions.Errors
	Dim lintIndex As Long
	Dim lintProduct As Integer
	
	insValSecurityShema = vbNullString
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ Se realizan las validaciones del encabezado de la página 
		'+ SG013_K - Esquema de seguridad.
		
		Case "SG013_k"
			insValSecurityShema = lobjSecuritySeq.insValSG013_k(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valScheCode"))
			
			'+ Validaciones del frame SG013 - Información general del esquema.
			
		Case "SG013"
			
			insValSecurityShema = lobjSecuritySeq.insValSG013(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctSchemaDes"), Request.Form.Item("tctShort_des"), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdNulldate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctFrom1"), Request.Form.Item("tctTo1"), Request.Form.Item("tctFrom2"), Request.Form.Item("tctTo2"), Request.Form.Item("chkPermission"), Request.Form.Item("tctFromQ1"), Request.Form.Item("tctToQ1"), Request.Form.Item("tctFromQ2"), Request.Form.Item("tctToQ2"), mobjValues.StringToType(Request.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDaysAdv"), eFunctions.Values.eTypeData.etdDouble))
			
			'+ Validaciones del frame SG014 - Monedas autorizadas en un esquema.
			
		Case "SG014"
			insValSecurityShema = ""
			
			'+ Validaciones del frame SG003 - Límites de suscripción y siniestros.
			
		Case "SG003"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				If Request.Form.Item("valProduct") = vbNullString Then
					lintProduct = 0
				Else
					lintProduct = mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble)
				End If
				insValSecurityShema = lobjSecuritySeq.insValSG003(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Session("sSche_codeWin"), CInt(Request.Form.Item("cbeCurrency")), CInt(Request.Form.Item("cbeBranch")), lintProduct)
			Else
				insValSecurityShema = vbNullString
			End If
			
			'+ Validaciones del frame SG017 - Acceso a sucursales.
			
		Case "SG017"
			lobjError = New eFunctions.Errors
			
			With lobjError
				
				lintIndex = 0
				
				If Not IsNothing(Request.Form.Item("nOffice")) Then
					For	Each llngOffice In Request.Form.GetValues("nOffice")
						lintIndex = lintIndex + 1
						
						If CDbl(Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)) = 1 Then
							If CDbl(Request.Form.GetValues("nInd_upda").GetValue(lintIndex - 1)) = 2 And CDbl(Request.Form.GetValues("nInd_inqu").GetValue(lintIndex - 1)) = 2 Then
								insValSecurityShema = .ErrorMessage("SG017", 12164, lintIndex)
							End If
						End If
					Next llngOffice
				End If
				
				insValSecurityShema = .Confirm()
			End With
			
			lobjError = Nothing
			
			'+ Frame SG002 - Niveles de seguridad.
			
		Case "SG002"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValSecurityShema = lobjSecuritySeq.insValSG002(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Session("sSche_codeWin"), Request.Form.Item("cbeType"), Request.Form.Item("valModTran"))
			Else
				insValSecurityShema = vbNullString
			End If
			
			
			'+ Validaciones del frame SG100 - Conceptos de solicitud de pago.
			
		Case "SG100"
			lobjError = New eFunctions.Errors
			
			With lobjError
				
				blnShowErr = False
				If Not lobjSecuritySeq.valTransAccess(Session("sSche_codeWin"), "OP06-1", "2") Then
					blnShowErr = True
				End If
				
				If blnShowErr Then
					lintIndex = 0
					If Not IsNothing(Request.Form.Item("nConcept")) Then
						For	Each llngConcept In Request.Form.GetValues("nConcept")
							lintIndex = lintIndex + 1
							If CDbl(Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)) = 1 Then
								insValSecurityShema = .ErrorMessage("SG100", 12166, lintIndex)
							End If
						Next llngConcept
					End If
				End If
				insValSecurityShema = .Confirm()
			End With
			
			lobjError = Nothing
			
		Case "SG020"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				insValSecurityShema = lobjSecuritySeq.insValSG020(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), Session("sSche_codeWin"), Request.Form.Item("valTransac"), mobjValues.StringToType(Request.Form.Item("valOperation"), eFunctions.Values.eTypeData.etdLong))
			Else
				insValSecurityShema = vbNullString
			End If
			
		Case "SG021"
			With Request
				insValSecurityShema = mobjSG021.ValSG021(Session("sSche_codeWin"), mobjValues.StringToType(.Form.Item("cbenTypeResc"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkModDateR"), .Form.Item("chkRescTot"), .Form.Item("chkRescPar"), mobjValues.StringToType(.Form.Item("optTypeExecut"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkModDateP"), Session("nUserCode"), mobjValues.StringToType(.Form.Item("cbeTypeRescV"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkModDateRV"), .Form.Item("chkRescTotV"), .Form.Item("chkRescParV"), mobjValues.StringToType(.Form.Item("optTypeExecutV"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("optAnulRec"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkModDatePV"), mobjValues.StringToType(.Form.Item("optRequest"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("optReport"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("cbeValueTyp"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkSelectedVNTReason"), .Form.Item("chkSelectedVTPayWay"), .Form.Item("chkSelectedVTBranch"), .Form.Item("chkSelectedVNTPayWay"), .Form.Item("chkSelectedVNTBranch"), .Form.Item("chkSelectedVTRole"))
			End With
			
		Case "SG855"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				
				With Request
					mobjSG855 = New eSecurity.SchemaFolder
					
					insValSecurityShema = mobjSG855.ValSG855(Session("sSche_codeWin"), mobjValues.StringToType(.Form.Item("valFolder"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnInqLevel"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkPermitted"), Session("nUserCode"), .QueryString.Item("sAction"))
				End With
				mobjSG855 = Nothing
			End If
		Case "GE101"
			insValSecurityShema = ""
			
		Case Else
			insValSecurityShema = "insValSecurityShema: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostSecuritySchema: Se realizan las actualizaciones de las ventanas.
'--------------------------------------------------------------------------------------------
Function insPostSecuritySchema() As Boolean
	Dim llngConcept As Object
	Dim llngCurrency As Object
	Dim llngOffice As Object
	Dim lintSelected As Object
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	Dim lintIndex As Long
	
	lblnPost = True
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ Encabezado SG013_k - Esquema de seguridad.
		
		Case "SG013_k"
			lblnPost = lobjSecuritySeq.insPostSG013_k(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("valScheCode"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))
			
			If lblnPost Then
				Session("sSche_codeWin") = UCase(Request.Form.Item("valScheCode"))
			End If
			
			'+ Frame SG013 - Información General del Esquema.
		Case "SG013"
			Session("sStatus") = Request.Form.Item("cbeStatregt")
			lblnPost = lobjSecuritySeq.insPostSG013(CInt(Request.QueryString.Item("nMainAction")), Session("sSche_codeWin"), Request.Form.Item("tctSchemaDes"), Request.Form.Item("tctShort_des"), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdNulldate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("tctFrom1"), Request.Form.Item("tctTo1"), Request.Form.Item("tctFrom2"), Request.Form.Item("tctTo2"), Request.Form.Item("chkPermission"), Request.Form.Item("tctFromQ1"), Request.Form.Item("tctToQ1"), Request.Form.Item("tctFromQ2"), Request.Form.Item("tctToQ2"), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDuration"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnDaysAdv"), eFunctions.Values.eTypeData.etdDouble))
			
			'+ Validaciones del frame SG014 - Monedas autorizadas en un esquema.
			
		Case "SG014"
			lblnPost = lobjSecuritySeq.insDelSchema_cur(Session("sSche_codeWin"))
			
			If lblnPost Then
				
				lintIndex = 0
				
				If Not IsNothing(Request.Form.Item("nCurrency")) Then
					For	Each llngCurrency In Request.Form.GetValues("nCurrency")
						lintIndex = lintIndex + 1
						
						If CDbl(Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)) = 1 Then
							lblnPost = lobjSecuritySeq.insCreSchema_Cur(Session("sSche_codeWin"), llngCurrency, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						End If
					Next llngCurrency
				End If
				
				lblnPost = lobjSecuritySeq.insDelLimitsCur(Session("sSche_codeWin"))
				
				lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nInd_curren", 2, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
			'+ Frame SG003 - Límites de suscripción y siniestros.
			
		Case "SG003"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lblnPost = lobjSecuritySeq.insPostSG003(Request.QueryString.Item("Action"), Session("sSche_codeWin"), mobjValues.StringToType(Request.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnClaim_d"), eFunctions.Values.eTypeData.etdDouble, 0), mobjValues.StringToType(Request.Form.Item("tcnClaim_p"), eFunctions.Values.eTypeData.etdDouble, 0), mobjValues.StringToType(Request.Form.Item("tcnIssuelim"), eFunctions.Values.eTypeData.etdDouble, 0), mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble))
				
				lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nInd_limits", 2, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			Else
				lblnPost = True
			End If
			
			'+ Frame SG017 - Acceso a sucursales.
			
		Case "SG017"
			
			
			lintSelected = 0
			
			lblnPost = lobjSecuritySeq.insDelOff_acc(Session("sSche_codeWin"))
			
			If lblnPost Then
				
				lintIndex = 0
				
				If Not IsNothing(Request.Form.Item("nOffice")) Then
					For	Each llngOffice In Request.Form.GetValues("nOffice")
						lintIndex = lintIndex + 1
						
						If CDbl(Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)) = 1 Then
							lblnPost = lobjSecuritySeq.insCreOff_acc(Session("sSche_codeWin"), llngOffice, Request.Form.GetValues("nInd_inqu").GetValue(lintIndex - 1), Request.Form.GetValues("nInd_upda").GetValue(lintIndex - 1), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							If lblnPost Then
								lintSelected = Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)
							End If
						End If
					Next llngOffice
				End If
				
				If lintSelected = 1 Then
					lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nAccesof", 2, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nAccesof", 1, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End If
			lintSelected = Nothing
			
			'+ Frame SG002 - Niveles de seguridad.
			
		Case "SG002"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lblnPost = lobjSecuritySeq.insPostSG002(Request.QueryString.Item("Action"), Session("sSche_codeWin"), Request.Form.Item("cbeType"), Request.Form.Item("valModTran"), mobjValues.StringToType(Request.Form.Item("tcnAmelevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnInqlevel"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkSupervis"), Request.Form.Item("chkPermitted"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				
				lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nSecurlev", 2, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			Else
				lblnPost = True
			End If
			
			'+ Frame SG100 - Conceptos de solicitud de pagos autorizados.
			
		Case "SG100"
			
			lintSelected = 0
			
			lblnPost = lobjSecuritySeq.insDelSche_pcon(Session("sSche_codeWin"))
			
			If lblnPost Then
				
				lintIndex = 0
				
				If Not IsNothing(Request.Form.Item("nConcept")) Then
					For	Each llngConcept In Request.Form.GetValues("nConcept")
						lintIndex = lintIndex + 1
						
						If CDbl(Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)) = 1 Then
							lblnPost = lobjSecuritySeq.insCreSche_pcon(Session("sSche_codeWin"), llngConcept, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
							If lblnPost Then
								lintSelected = Request.Form.GetValues("nSelValue").GetValue(lintIndex - 1)
							End If
						End If
					Next llngConcept
				End If
				
				If lintSelected = 1 Then
					lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nInd_conce", 2, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = lobjSecuritySeq.InsUpdInd(Session("sSche_codeWin"), "nInd_conce", 1, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				End If
			End If
			
			lintSelected = Nothing
			
			'+ Ventana de Fin de proceso.
			
		Case "GE101"
			If Request.Form.Item("optElim") = "Delete" Then
				
				'+ Se elimina la información relacionada con las transacciones del sistema.
				
				lblnPost = lobjSecuritySeq.insDelSchema(Session("sSche_codeWin"))
			End If
			
			Response.Write("<SCRIPT>opener.top.location.reload();</" & "Script>")
			Response.Write("<SCRIPT>window.close()</" & "Script>")
			
			lblnPost = False
			
			'+ Frame SG020 - Niveles de seguridad por transacción operación.
			
		Case "SG020"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lblnPost = lobjSecuritySeq.insPostSG020(Request.QueryString.Item("Action"), Session("sSche_codeWin"), Request.Form.Item("valTransac"), mobjValues.StringToType(Request.Form.Item("valOperation"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			Else
				lblnPost = True
			End If
		Case "SG021"
			With Request
				lblnPost = mobjSG021.PostSG021(Session("sSche_codeWin"), mobjValues.StringToType(.Form.Item("cbenTypeResc"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkModDateR"), .Form.Item("chkRescTot"), .Form.Item("chkRescPar"), .Form.Item("optTypeExecut"), .Form.Item("chkModDateP"), Session("nUserCode"), mobjValues.StringToType(.Form.Item("cbeTypeRescV"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkModDateRV"), .Form.Item("chkRescTotV"), .Form.Item("chkRescParV"), .Form.Item("optTypeExecutV"), .Form.Item("optAnulRec"), .Form.Item("chkModDatePV"), .Form.Item("optRequest"), .Form.Item("optReport"), mobjValues.StringToType(.Form.Item("cbeValueTyp"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkSelectedVNTReason"), .Form.Item("chkSelectedVTPayWay"), .Form.Item("chkSelectedVTBranch"), .Form.Item("chkSelectedVNTPayWay"), .Form.Item("chkSelectedVNTBranch"), .Form.Item("chkSelectedVTRole"))
				
				
				
			End With
			
		Case "SG855"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				
				mobjSG855 = New eSecurity.SchemaFolder
				
				With Request
					lblnPost = mobjSG855.PostSG855(Session("sSche_codeWin"), mobjValues.StringToType(.Form.Item("valFolder"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnInqLevel"), eFunctions.Values.eTypeData.etdLong), .Form.Item("chkPermitted"), Session("nUserCode"), .QueryString.Item("sAction"))
				End With
				mobjSG855 = Nothing
			Else
				lblnPost = True
			End If
	End Select
	
	insPostSecuritySchema = lblnPost
End Function

'% insFinish: Se activa cuando la acción es Finalizar
'--------------------------------------------------------------------------------------------
Function insFinish() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsSecur_Sche As eSecurity.Secur_sche
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>	" & vbCrLf)
Response.Write("    if(insvalTabs())" & vbCrLf)
Response.Write("    {")

	
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionCut) Or Request.QueryString.Item("Action") = CStr(eFunctions.Menues.TypeActions.clngActionCut) Then
		lclsSecur_Sche = New eSecurity.Secur_sche
		Call lclsSecur_Sche.insPostSG013(Request.QueryString.Item("nMainAction"), Session("sSche_codeWin"), vbNullString, vbNullString, Nothing, Nothing, vbNullString, vbNullString, vbNullString, vbNullString, 0, vbNullString, vbNullString, vbNullString, vbNullString, vbNullString, mobjValues.StringToType(Session("nUserCode"), eFunctions.Values.eTypeData.etdDouble))
		Response.Write("top.location.reload();")
	End If
	
	
Response.Write("" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
lobjSecuritySeq = New eSecurity.Secur_sche
mobjSG021 = New eSecurity.SecurScheSurr

mstrCommand = "&sModule=Security&sProject=Security&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 14/03/06 20:08 $|$$Author: Mvazquez $"
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT>
//% insvalTabs: se verifica la existencia de ventanas requeridas en la secuencia
//-------------------------------------------------------------------------------------------
function insvalTabs(){
//-------------------------------------------------------------------------------------------
	<%lclsGeneral = New eGeneral.GeneralFunction
%>
	var lblnTabs = false;
	var Array = top.frames['fraSequence'].sequence;
	var lobjErr;
	
	for(var lintIndex=0; lintIndex<Array.length; lintIndex++)
		if(Array[lintIndex].Require=="2" ||
		   Array[lintIndex].Require=="5")
			lblnTabs = true;

	if(lblnTabs)
	{
		alert("<%=lclsGeneral.insLoadMessage(3902)%>");
//+ Se habilitan las acciones del ToolBar al usuario
        
        if(typeof(top.fraHeader)!='undefined') {
            top.fraHeader.insHandImage("A390", true);
            top.fraHeader.insHandImage("A391", true);
            top.fraHeader.insHandImage("A392", true);
            top.fraHeader.insHandImage("A393", true);
            top.fraHeader.setPointer('');
        }
	}
	else
	{
		ShowPopUp("/VTimeNet/Security/Security/ShowDefValues.aspx?Field=Finish", "ShowDefValuesFinish", 1, 1,"no","no",2000,2000);
        top.location.reload();
	}
	
<%
lclsGeneral = Nothing%>
	return(lblnTabs)
}
</SCRIPT>    
</HEAD>
<BODY>
<%Response.Write("<SCRIPT>")%>

//-----------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------
    self.history.go(-1)}

//-----------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation;
}

</SCRIPT>
<%
If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	
	'+ Si no se han validado los campos de la página.
	
	If Request.Form.Item("sCodisplReload") = vbNullString Then
		mstrErrors = insValSecurityShema
		Session("sErrorTable") = mstrErrors
	Else
		Session("sErrorTable") = vbNullString
	End If
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sForm=" & Server.URLEncode(Request.Form.ToString) & Server.URLEncode(mstrCommand) & "&sValPage=" & "SecuritySeqSchema" & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""ValSecurityErrors"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSecuritySchema Then
			
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				'+ Se mueve automáticamente a la siguiente página.
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Security/Security/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Security/Security/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				End If
				If CDbl(Request.QueryString.Item("nZone")) = 1 Then
					Response.Write("<SCRIPT> self.history.go(-1) </SCRIPT>")
				End If
			Else
				
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Security/Security/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
				
				'+ Se recarga la página que invocó la PopUp.
				Select Case Request.QueryString.Item("sCodispl")
					Case "SG003"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					Case "SG013"
						Response.Write("<SCRIPT>top.opener.document.location.href='SG013.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "';</SCRIPT>")
					Case "SG014"
						Response.Write("<SCRIPT>top.opener.document.location.href='SG014.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
					Case "SG017"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
					Case "SG002"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
					Case "SG100"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "';</SCRIPT>")
					Case "SG020", "SG855"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
				End Select
			End If
		Else
			Response.Write("<SCRIPT>alert('No se pudo realizar la actualización');</SCRIPT>")
		End If
	End If
Else
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	Else
		insFinish()
	End If
End If

lobjSecuritySeq = Nothing
mobjValues = Nothing
mobjSG021 = Nothing

%>

</BODY>
</HTML>






