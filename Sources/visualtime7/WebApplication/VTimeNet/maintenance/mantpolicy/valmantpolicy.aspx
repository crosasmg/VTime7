<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eBranches" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String
Dim mstrQueryString As String

'- Variable para el manejo de los errores de la página, devueltos por insvalSequence
Dim mstrErrors As String

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjMantPolicy As Object


'% insValProject: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insValMantPolicy() As String
	'--------------------------------------------------------------------------------------------
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ MCA001: Secuencia de ventanas de procesos de póliza - MAVR - 05/09/2001.
		Case "MCA001"
			insValMantPolicy = vbNullString
			
			'+ MCA005 Validación de los campos de Causas del estado pendiente de la póliza/certificado 
		Case "MCA005"
			With Request
				mobjMantPolicy = New ePolicy.Tab_waitPo
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantPolicy = mobjMantPolicy.insValMCA005(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Request.Form.Item("tcnWait_Code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_Des"), mobjValues.StringToType(Request.Form.Item("cbeAreaWait"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"))
				End If
			End With
			
			'+ MCA006 Definición de columnas del archivo de carga de pólizas
		Case "MCA006"
			With Request
				mobjMantPolicy = New eBatch.Group_columns
				If .QueryString.Item("nZone") = "1" Then
					insValMantPolicy = mobjMantPolicy.insValMCA006_K(mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("optInf"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantPolicy = mobjMantPolicy.insValMCA006(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeTable"), .Form.Item("ValField"), .Form.Item("sColumnName"), .Form.Item("sComment"), mobjValues.StringToType(.Form.Item("nOrder"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sRequire"), .Form.Item("ValList"))
					End If
				End If
			End With
			
		Case "MCA632"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					'+ Crear las variables de session segun el nombre que tengan en la cabecera  MCA632_K
					insValMantPolicy = mobjMantPolicy.insValMCA632_k(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("nMainAction") <> "401" Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							insValMantPolicy = mobjMantPolicy.insValMCA632(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeType_amend"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("chksInd_order_serv"), mobjValues.StringToType(.Form.Item("cbenTypeIssue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLevel"), eFunctions.Values.eTypeData.etdDouble))
						End If
					End If
				End If
			End With
			
			'+ MCA580: Ramos-productos válidos para descuento por volumen
		Case "MCA580"
			mobjMantPolicy = New eBranches.Tab_branch_quant
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantPolicy = mobjMantPolicy.insValMCA580_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mstrQueryString = "&dEffecdate=" & mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
						insValMantPolicy = mobjMantPolicy.insValMCA580(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeStatregt"))
					Else
						insValMantPolicy = vbNullString
					End If
				End If
			End With
			
			'+ MCA581: Descuento por volumen
		Case "MCA581"
			mobjMantPolicy = New eBranches.Disc_quantity
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantPolicy = mobjMantPolicy.insValMCA581_k(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantPolicy = mobjMantPolicy.insValMCA581(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnRate_disc"), eFunctions.Values.eTypeData.etdDouble))
					Else
						insValMantPolicy = vbNullString
					End If
				End If
			End With
			'+ MCA814: Cambio de módulo o plan de forma masiva
		Case "MCA814"
			mobjMantPolicy = New ePolicy.Change_mod
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantPolicy = mobjMantPolicy.InsValMCA814_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						insValMantPolicy = mobjMantPolicy.InsValMCA814(.QueryString("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeModul_ori"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("cbeModul_end"), eFunctions.Values.eTypeData.etdDouble, True))
					Else
						insValMantPolicy = vbNullString
					End If
				End If
			End With
			
			
			'+ MCA005 Validación de los campos de Causas de no conversión a póliza 
		Case "MCA815"
			With Request
				mobjMantPolicy = New ePolicy.Noconvers
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantPolicy = mobjMantPolicy.insValMCA815(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(Request.Form.Item("tcnNo_convers"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeAreaWait"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chksDevo"), Request.Form.Item("chksDisc"), Request.Form.Item("cbeStatregt"))
				End If
			End With
			
			'+ MCA816 Validacion de los objetos del sincronizador del cotizador
		Case "MCA816"
			With Request
				mobjMantPolicy = New eBatch.cot_stand_alone
				If .QueryString.Item("WindowType") = "PopUp" Then
					insValMantPolicy = mobjMantPolicy.insValMCA816_K(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("hddnId_object"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctName"), mobjValues.StringToType(Request.Form.Item("cbeType_object"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble))
				End If
                End With
                
                '+ MCA300 Validacion de los objetos   
            Case "MCA300"
                mobjMantPolicy = New eBranches.Tab_rescost
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValMantPolicy = mobjMantPolicy.InsValMCA300_K(.QueryString("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdAssign_date"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValMantPolicy = mobjMantPolicy.InsValMCA300(.QueryString("sCodispl"), .QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dAssign_date_MCA300"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tcnDesc"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax"), eFunctions.Values.eTypeData.etdDouble))
                        Else
                            insValMantPolicy = vbNullString
                        End If
                    End If
                End With
                
		Case Else
			insValMantPolicy = "insValMantPolicy: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostProject: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostMantPolicy() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Select Case Request.QueryString.Item("sCodispl")
		'+ MCA001: Secuencia de ventanas de procesos de póliza
		Case "MCA001"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&sBussityp=" & .Form.Item("optBussityp") & "&sPolitype=" & .Form.Item("optPolitype") & "&sCompon=" & .Form.Item("optCompon") & "&sTratypep=" & .Form.Item("cbeTratypep") & "&sBrancht=" & .Form.Item("cbeBrancht") & "&nType_Amend=" & .Form.Item("cbeType_Amend")
				Else
					If .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
						With Request
							mobjMantPolicy = New eProduct.Tab_winpol
							lblnPost = mobjMantPolicy.insPostMCA001(.QueryString("sBussityp"), .QueryString("sPolitype"), .QueryString("sCompon"), .Form.Item("hddnSequence"), .QueryString("sTratypep"), .Form.Item("hddsCodispl"), .Form.Item("hddsRequire"), vbNullString, .Form.Item("hddsSel"), .Form.Item("hddAutomatic"), Session("nUsercode"), .QueryString("sBrancht"), mobjValues.StringToType(.QueryString.Item("nType_Amend"), eFunctions.Values.eTypeData.etdInteger))
						End With
					End If
					mstrQueryString = "&sBussityp=1&sPolitype=1&sCompon=1&sTratypep=1"
				End If
				mobjMantPolicy = Nothing
			End With
			
			'+ MCA005 Actualización de los campos de Causas del estado pendiente de la póliza/certificado 
		Case "MCA005"
			With Request
				mobjMantPolicy = New ePolicy.Tab_waitPo
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantPolicy.insPostMCA005(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnWait_Code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), Request.Form.Item("tctShort_Des"), mobjValues.StringToType(Request.Form.Item("cbeAreaWait"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chkConvert"))
				Else
					lblnPost = True
				End If
			End With
			
			'+ MCA006 Definición de columnas del archivo de carga de pólizas
		Case "MCA006"
			With Request
				mobjMantPolicy = New eBatch.Group_columns
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nSheet=" & .Form.Item("cbeSheet") & "&nBranch=" & mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True)
					lblnPost = True
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantPolicy.insPostMCA006(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("cbeTable"), .Form.Item("ValField"), .Form.Item("sColumnName"), .Form.Item("sComment"), mobjValues.StringToType(.Form.Item("nOrder"), eFunctions.Values.eTypeData.etdDouble, True), .Form.Item("sRequire"), .Form.Item("ValList"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("nIdRec"), eFunctions.Values.eTypeData.etdDouble, True))
						
						mstrQueryString = "&nSheet=" & mobjValues.StringToType(.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble, True) & "&nBranch=" & mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble, True)
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ MCA580: Ramos-productos válidos para descuento por volumen
		Case "MCA580"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&dEffecdate=" & mobjValues.TypeToString(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
					lblnPost = True
				Else
					mstrQueryString = "&dEffecdate=" & mobjValues.TypeToString(Request.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate)
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantPolicy.insPostMCA580Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("hdddEffecdate"), eFunctions.Values.eTypeData.etdDate), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ MCA581: Descuento por volumen
		Case "MCA581"
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
					lblnPost = True
				Else
					mstrQueryString = "&dEffecdate=" & Request.QueryString.Item("dEffecdate")
					If .QueryString.Item("WindowType") = "PopUp" Then
						lblnPost = mobjMantPolicy.insPostMCA581Upd(Request.QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnQuantity"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnRate_disc"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
					Else
						lblnPost = True
					End If
				End If
			End With
			
			'+ MCA632: Tipos de endoso 
		Case "MCA632"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & .Form.Item("valProduct") & "&dEffecdate=" & .Form.Item("tcdEffecdate")
				Else
					mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & .QueryString.Item("nProduct") & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nMainAction=" & .QueryString.Item("nMainAction")
					
					If .QueryString.Item("nMainAction") <> "401" Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							lblnPost = mobjMantPolicy.insPostMCA632(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeType_amend"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chksInd_order_serv"), mobjValues.StringToType(.Form.Item("cbenTypeIssue"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnLevel"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkRetarif"))
							
						End If
					End If
				End If
			End With
			
			'+ MCA814: Cambio de módulo o plan de forma masiva
		Case "MCA814"
			With Request
				lblnPost = True
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					
					mstrQueryString = "&nBranch=" & .Form.Item("cbeBranch") & "&nProduct=" & mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & .Form.Item("tcdEffecdate")
				Else
					mstrQueryString = "&nBranch=" & .QueryString.Item("nBranch") & "&nProduct=" & mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & .QueryString.Item("dEffecdate") & "&nMainAction=" & mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
					
					If .QueryString.Item("nMainAction") <> "401" Then
						If .QueryString.Item("WindowType") = "PopUp" Then
							lblnPost = mobjMantPolicy.insPostMCA814(.QueryString("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeModul_ori"), eFunctions.Values.eTypeData.etdDouble), .QueryString("dEffecdate"), mobjValues.StringToType(.Form.Item("cbeModul_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("chkSidemcap"), .Form.Item("chkSidemprem"), .Form.Item("chkSidemdeduc"))
							
						End If
					End If
				End If
			End With
			
			'+ MCA815 Actualización de los campos de Causas de no conversión a póliza
		Case "MCA815"
			With Request
				mobjMantPolicy = New ePolicy.Noconvers
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantPolicy.insPostMCA815(Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("tcnNo_convers"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctDescript"), mobjValues.StringToType(Request.Form.Item("cbeAreaWait"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("chksDevo"), Request.Form.Item("chksDisc"), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnGastAdm"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctRutine"), mobjValues.StringToType(Request.Form.Item("tcnGastMed"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With
			
			'+ MCA816 Actualización de los objetos del sincronizador del cotizador
		Case "MCA816"
			With Request
				mobjMantPolicy = New eBatch.cot_stand_alone
				If .QueryString.Item("WindowType") = "PopUp" Then
					lblnPost = mobjMantPolicy.insPostMCA816_K(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("Action"), mobjValues.StringToType(Request.Form.Item("hddnId_object"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctName"), mobjValues.StringToType(Request.Form.Item("cbeType_object"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnLevel"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnOrder"), eFunctions.Values.eTypeData.etdDouble), Request.Form.Item("tctPath"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
				Else
					lblnPost = True
				End If
			End With

            Case "MCA300"
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Session("dAssign_date_MCA300") = .Form.Item("tcdAssign_date")
                        lblnPost = True
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            lblnPost = mobjMantPolicy.InsPostMCA300Upd(.QueryString("Action"), mobjValues.StringToType(.Form.Item("tcnCode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(Session("dAssign_date_MCA300"), eFunctions.Values.eTypeData.etdDate), .Form.Item("tcnDesc"), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMin"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnMax"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger))
                        Else
                            lblnPost = True
                       
                        End If
                    End If
                End With
			
        End Select
	insPostMantPolicy = lblnPost
	mobjMantPolicy = Nothing
End Function

</script>
<%Response.Expires = -1
mstrCommand = "&sModule=Maintenance&sProject=MantPolicy&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>



	
</HEAD>
<BODY>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 3 $|$$Date: 8/10/09 9:57a $|$$Author: Gletelier $"
	
//%CancelErrors: Acciones al efectual la cancelación de algún error.
//-----------------------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------------------
	self.history.go(-1)
}

//%NewLocation: se recalcula el URL de la página
//-----------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
mobjMantPolicy = New ePolicy.Type_amend
mobjValues = New eFunctions.Values

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValMantPolicy
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantPolicy"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If insPostMantPolicy Then
		If Request.QueryString.Item("WindowType") <> "PopUp" Then
			If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
				Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
			Else
				If Request.QueryString.Item("nZone") = "1" Then
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
					End If
				Else
					Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
				End If
			End If
		Else
			'+ Se recarga la página que invocó la PopUp
			Select Case Request.QueryString.Item("sCodispl")
				'+ Tipos de endonso por Ramo/Producto		
				Case "MCA632"
					Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
					'+ MCA005: Causas del estado pendiente de la póliza/certificado 
				Case "MCA005"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCA005_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
					'+ MCA006: Definición de columnas para carga de póliza
				Case "MCA006"
					If Request.Form.Item("sCodisplReload") = vbNullString Then
						Response.Write("<SCRIPT>top.opener.document.location.href='MCA006.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
					Else
						Response.Write("<SCRIPT>window.close();top.opener.top.opener.document.location.href='MCA006.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
					End If
					'+ Ramos-productos válidos para descuento por volumen
				Case "MCA580"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCA580.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
					'+ Tabla de descuento por volumen
				Case "MCA581"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCA581.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
					'+ Cambio de módulo o plan de forma masiva
				Case "MCA814"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCA814.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
					'+ MCA815: Causas de no conversión a póliza 
				Case "MCA815"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCA815_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
					'+ MCA815: Causas de no conversión a póliza 
				Case "MCA816"
					Response.Write("<SCRIPT>top.opener.document.location.href='MCA816_k.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case "MCA300"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MCA300.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & mstrQueryString & "'</SCRIPT>")
                End Select
		End If
	End If
End If

mobjValues = Nothing
mobjMantPolicy = Nothing
%>
</BODY>
</HTML>





