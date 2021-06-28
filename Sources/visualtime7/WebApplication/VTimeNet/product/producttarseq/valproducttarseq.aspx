<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de funciones generales
Dim mobjValues As eFunctions.Values

'- Variable para el manejo del querystring
Dim mstrQueryString As Object

'- Se define la constante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

Dim mstrErrors As String
Dim mobjProductTarSeq As Object
Dim mstrLocationBC003 As String
'- Contador para uso general    
Dim mintCount As Object

'- Esta variable es para indicar cuando debe pasarse a la siguiente ventana de la secuencia
'- al aceptar.  Para uso de casos particulares.
Dim lstrGoToNext As Object

'- Cadena para pase de parametros    
Dim mstrString As Object


'% insvalSequence: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalSequence() As String
	Dim lstrName_field As String
	Dim lstrColName As String
	Dim i As Integer
	Dim lstrColValue As String
	'--------------------------------------------------------------------------------------------
	insvalSequence = vbNullString
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ DP8000_K: Tratamiento de tablas lógicas de tarifas
		Case "DP8000_K"
			mobjProductTarSeq = New eTarif.TableTarifSeq
			With Request
				With Request
					insvalSequence = mobjProductTarSeq.insValDP8000_K("DP8000_K", mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valTableTarif"), eFunctions.Values.eTypeData.etdLong))
				End With
			End With
		
		Case "DP8001"
			mobjProductTarSeq = New eTarif.Tarif_tab_col
			insvalSequence = mobjProductTarSeq.insValDP8001("DP8001", mobjValues.StringToType(Session("nId_Table"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(Request.Form.Item("valid_Column"), eFunctions.Values.eTypeData.etdLong), Request.Form.Item("cbeOperator"), mobjValues.StringToType(Request.Form.Item("tcnorder"), eFunctions.Values.eTypeData.etdLong, True), Request.QueryString.Item("WindowType"), Request.QueryString.Item("Action"))
			
		Case "DP8002"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				lstrColValue = ""
				lstrColName = ""
				For i = 1 To CInt(Request.Form.Item("hddTotalCol"))
					lstrName_field = "hddColId" & i
					'+ String con el valor de la columna separado por "|"
					lstrName_field = "Col_" & Request.Form.Item(lstrName_field)
					lstrColValue = lstrColValue & Request.Form.Item(lstrName_field) & "|"
					'+ String con el sName_col separado por "|"
					lstrName_field = "hddColName" & i
					lstrColName = lstrColName & Request.Form.Item(lstrName_field) & "|"
				Next 
				
				mobjProductTarSeq = New eTarif.Tarif_val_col
				insvalSequence = mobjProductTarSeq.insValDP8002("DP8002", lstrColValue, lstrColName, mobjValues.StringToType(Request.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeType_tar"), eFunctions.Values.eTypeData.etdLong, True))
			End If
			
		Case Else
			insvalSequence = "insvalSequence: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
			
	End Select
End Function

'% insPostSequence: Se realizan las actualizaciones de las ventanas
'--------------------------------------------------------------------------------------------
Function insPostSequence() As Boolean
	Dim lstrColType As String
	Dim lstrColName As String
	Dim i As Integer
	Dim lstrColValue As String
	Dim lstrColId As String
	Dim lstrName_field As String
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	lblnPost = False
	
	Dim lobjTarifSeq As eTarif.TableTarifSeq
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ DP8000_K: Tratamiento de tablas lógicas de tarifas
		Case "DP8000_K"
			With Request
				Session("nId_Table") = mobjValues.TypeToString(.Form.Item("valTableTarif"), eFunctions.Values.eTypeData.etdLong)
				If mobjValues.TypeToString(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmnull Then
					lobjTarifSeq = New eTarif.TableTarifSeq
					Session("dEffecdate") = mobjValues.TypeToString(lobjTarifSeq.GetMaxDeffecdate(Session("nId_Table")), eFunctions.Values.eTypeData.etdDate)
					Response.Write("<SCRIPT>top.frames['fraHeader'].document.forms[0].tcdEffecdate.value='" & Session("dEffecdate") & "';</" & "Script>")
					lobjTarifSeq = Nothing
				Else
					Session("dEffecdate") = mobjValues.TypeToString(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate)
				End If
			End With
			lblnPost = True
			
		Case "DP8001"
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				mobjProductTarSeq = New eTarif.Tarif_tab_col
				With Request
					lblnPost = mobjProductTarSeq.InsPostDP8001(.QueryString("Action"), mobjValues.StringToType(Session("nId_Table"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("valid_Column"), eFunctions.Values.eTypeData.etdLong), .Form.Item("cbeOperator"), mobjValues.StringToType(.Form.Item("cbetype_calc"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(.Form.Item("tcnorder"), eFunctions.Values.eTypeData.etdLong, True), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong))
					If lblnPost Then
						Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/product/producttarseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=DP8001" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No" & mstrCommand & "';</" & "Script>")
					End If
				End With
			Else
				lblnPost = True
			End If
			
		Case "DP8002"
			lstrColValue = ""
			lstrColId = ""
			lstrColName = ""
			lstrColType = ""
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				For i = 1 To CInt(Request.Form.Item("hddTotalCol"))
					'+ String con el nid_column separado por "|"
					lstrName_field = "hddColId" & i
					lstrColId = lstrColId & Request.Form.Item(lstrName_field) & "|"
					'+ String con el valor de la columna separado por "|"
					lstrName_field = "Col_" & Request.Form.Item(lstrName_field)
					lstrColValue = lstrColValue & Request.Form.Item(lstrName_field) & "|"
					'+ String con el nData_type separado por "|"
					lstrName_field = "hddColType" & i
					lstrColType = lstrColType & Request.Form.Item(lstrName_field) & "|"
				Next 
				
				mobjProductTarSeq = New eTarif.Tarif_val_col
				With Request
					lblnPost = mobjProductTarSeq.InsPostDP8002(.QueryString("Action"), mobjValues.StringToType(Session("nId_Table"), eFunctions.Values.eTypeData.etdLong), lstrColId, lstrColValue, lstrColType, mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeType_tar"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("tcnAmount"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdLong), mobjValues.StringToType(.Form.Item("hddnRow"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToDate(Session("dEffecdate")))
				End With
				If lblnPost Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/product/producttarseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=DP8002" & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No" & mstrCommand & "';</" & "Script>")
				End If
			Else
				lblnPost = True
			End If
			
	End Select
	insPostSequence = lblnPost
End Function

'% insFinish: se activa al finalizar el proceso
'--------------------------------------------------------------------------------------------
Function insFinish() As Boolean
	'--------------------------------------------------------------------------------------------
	insFinish = True
	
	mstrLocationBC003 = "'/VTimeNet/Common/secWHeader.aspx?sCodispl=DP8000_K&sModule=Product&sProject=ProducttarSeq&sProduct=ProducttarSeq'"
End Function

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mstrCommand = "&sModule=product&sProject=producttarseq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
     <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>



    
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 20 $|$$Date: 10/11/04 14:17 $|$$Author: Nvaplat15 $"
</SCRIPT>
</HEAD>
<BODY>
<FORM ID=FORM1 NAME=FORM1>
<%
mstrLocationBC003 = "'/VTimeNet/Common/GoTo.aspx?sCodispl=DP8000_K'"

'+ Si no se han validado los campos de la página
If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insvalSequence
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ProductSeqErrors"",660,330);self.document.location.href='/VTimeNet/common/blank.htm';")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostSequence Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				'+ Si se está tratando con un frame y no con la ventana principal de la secuencia, 
				'+ se mueve automaticamente a la siguiente página
				If Request.Form.Item("sCodisplReload") = vbNullString Then
					Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/product/producttarseq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes" & mstrCommand & "';</SCRIPT>")
				Else
					Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Product/producttarseq/Sequence.aspx?nMainAction=" & Request.QueryString.Item("nAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & mstrCommand & "';</SCRIPT>")
				End If
			Else
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "DP8001"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
					Case "DP8002"
						Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Session("bQuery") Then
		Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
	Else
		
		'+ Se recarga la página principal de la secuencia            
		If insFinish() Then
			Response.Write("<SCRIPT>top.document.location=" & mstrLocationBC003 & ";</SCRIPT>")
		End If
	End If
End If
mobjProductTarSeq = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




