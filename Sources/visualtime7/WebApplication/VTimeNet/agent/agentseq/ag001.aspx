<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.57
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim llngAction As Object
Dim mblnDisable As Boolean



'% InsPreAG001: Valores iniciales de la ventana
'---------------------------------------------------------------------------------------------------------------------------------------
Private Sub InsPreAG001()
	'---------------------------------------------------------------------------------------------------------------------------------------
	Dim lclsIntermedia As eAgent.Intermedia
	Dim lclsIntermed_his As eAgent.Intermed_his
	Dim lclseRemote As eRemoteDB.Query
	Dim lstrCause_null As String
	Dim ldtmEffecdate As Object
	Dim ldtmEffecdate_his As Object
	Dim llngIntermedia As Object
	Dim lblnIntermedia As Boolean
	
	lclsIntermedia = New eAgent.Intermedia
	lclsIntermed_his = New eAgent.Intermed_his
	lclseRemote = New eRemoteDB.Query
	
	Response.Write(mobjValues.ShowWindowsName("AG001", Request.QueryString.Item("sWindowDescript")))
	lstrCause_null = ""
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 And CStr(Session("nLastIntermediary")) <> vbNullString Then
		lblnIntermedia = lclsIntermedia.Find(Session("nIntermed"))
		If (Not lblnIntermedia) Or lclsIntermedia.sClient = vbNullString Then
			lblnIntermedia = lclsIntermedia.Find(Session("nLastIntermediary"))
		End If
		llngIntermedia = Session("nLastIntermediary")
	Else
		llngIntermedia = Session("nIntermed")
		lblnIntermedia = lclsIntermedia.Find(Session("nIntermed"))
	End If
	
	If (llngAction = eFunctions.Menues.TypeActions.clngActionAdd Or llngAction = eFunctions.Menues.TypeActions.clngActionDuplicate) Then
		lclsIntermedia.nInt_status = 3
	End If
	
	With lclsIntermed_his
		.nIntermed = Session("nIntermed")
		Call .ReaLastDateIntermed_his()
		ldtmEffecdate_his = .dEffecdate
		If IsNothing(ldtmEffecdate_his) Then
			ldtmEffecdate_his = Today
		End If
	End With
	
	If CStr(llngAction) = CStr(eFunctions.Menues.TypeActions.clngActionAdd) Then
		If Not IsDbNull(lclsIntermedia.dInpdate) Then
			ldtmEffecdate = lclsIntermedia.dInpdate
		End If
	ElseIf CStr(llngAction) = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then 
		ldtmEffecdate = Today
	End If
	If ldtmEffecdate = eRemoteDB.Constants.dtmNull Then
		ldtmEffecdate = lclsIntermedia.dInpdate
	End If
	
	lstrCause_null = vbNullString
	mobjValues.ActionQuery = llngAction = eFunctions.Menues.TypeActions.clngactionquery
	Response.Write("<SCRIPT>top.fraHeader.document.forms[0].valIntermedia.value='" & Session("nIntermed") & "';top.fraHeader.$('#valIntermedia').change()</" & "Script>")
	
Response.Write("" & vbCrLf)
Response.Write("    <P ALIGN=""Center"">" & vbCrLf)
Response.Write("        <LABEL ID=40040><A HREF=""#Cliente"">" & GetLocalResourceObject("AnchorClienteCaption") & "</A></LABEL><LABEL ID=40041> | </LABEL>" & vbCrLf)
Response.Write("        <LABEL ID=40042><A HREF=""#Control""> " & GetLocalResourceObject("AnchorControlCaption") & "</A></LABEL>" & vbCrLf)
Response.Write("    </P>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8246>" & GetLocalResourceObject("tcdEffecDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdEffecDate", ldtmEffecdate_his,  , GetLocalResourceObject("tcdEffecDateToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("tcdEffecDate_old", ldtmEffecdate))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>    " & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8230>" & GetLocalResourceObject("cbeInterTypeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeInterType", "TabInter_typ", 1, CStr(lclsIntermedia.nIntertyp),  ,  ,  ,  ,  ,  ,  ,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("			")

	If lclsIntermedia.sLife = "1" And lclsIntermedia.sNonLife = "1" Then
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkAll", GetLocalResourceObject("chkAllCaption"), CStr(1), "1", "DisabledField();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkAll", GetLocalResourceObject("chkAllCaption"),  , "1", "DisabledField();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8232>" & GetLocalResourceObject("tctLegalNumCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctLegalNum", 10, lclsIntermedia.sInter_id,  , GetLocalResourceObject("tctLegalNumToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeInsu_areaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

	If lclsIntermedia.sLife = "1" And lclsIntermedia.sNonLife = "1" Then
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsu_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
		Select Case lclsIntermedia.sNonLife
			Case "1"
				
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsu_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

				
			Case "2"
				
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsu_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

				
			Case Else
				
Response.Write("" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PossiblesValues("cbeInsu_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

				
		End Select
	End If
	
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR> " & vbCrLf)
Response.Write("            ")

	If lclsIntermedia.sValid = "1" Or Not lblnIntermedia Then
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkValid", GetLocalResourceObject("chkValidCaption"), CStr(1), "1", "ChangeValid(this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	Else
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkValid", GetLocalResourceObject("chkValidCaption"), CStr(2), "2", "ChangeValid(this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

	End If
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40043><A NAME=""Cliente"">" & GetLocalResourceObject("AnchorCliente2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8228>" & GetLocalResourceObject("dtcClientCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.ClientControl("dtcClient", lclsIntermedia.sClient,  , GetLocalResourceObject("dtcClientToolTip"),  ,  , "lblCliename", False,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8235>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	mobjValues.TypeOrder = 1
        Response.Write(mobjValues.PossiblesValues("cbeOffice", "TABOFFICE", 1, CStr(lclsIntermedia.nOffice), , , , , , "BlankFields(this.value);insInitialAgency(1)", , , ""))
	mobjValues.TypeOrder = 2
	
Response.Write("" & vbCrLf)
Response.Write("            </TD>" & vbCrLf)
Response.Write("            <TD></TD>            " & vbCrLf)
Response.Write("            <TD><LABEL ID=8235>" & GetLocalResourceObject("cbeOfficeAgenCaption") & "</LABEL></TD>            " & vbCrLf)
Response.Write("			<TD>")

	With mobjValues
		.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(lclsIntermedia.nOfficeAgen), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8235>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""4"">")

	
	mobjValues.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(lclsIntermedia.nAgency), True,  ,  ,  ,  , "ShowChangeValues()",  ,  , GetLocalResourceObject("cbeAgencyToolTip"),  ,  ,  , False))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8236>" & GetLocalResourceObject("valSupervisCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valSupervis", "tabintermedia_superv", 2, CStr(lclsIntermedia.nSupervis),  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valSupervisToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8236>" & GetLocalResourceObject("valSup_GenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valSup_Gen", "tabintermedia_superv", 2, CStr(lclsIntermedia.nSup_Gen),  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valSup_GenToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8236>" & GetLocalResourceObject("valInsu_assisLifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valInsu_assisLif", "tabIntermedia_Assist", 2, CStr(lclsIntermedia.nInsu_AssistLif),  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valInsu_assisLifToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8236>" & GetLocalResourceObject("valInsu_assistCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">")


Response.Write(mobjValues.PossiblesValues("valInsu_assist", "tabIntermedia_Assist", 2, CStr(lclsIntermedia.nInsu_Assist),  ,  ,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valInsu_assistToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40044><A NAME=""Control"">" & GetLocalResourceObject("AnchorControl2Caption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>            " & vbCrLf)
Response.Write("            <TD COLSPAN=""5"" CLASS=""Horline""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8229>" & GetLocalResourceObject("tcdInputDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			")

	
	If lclsIntermedia.dInpdate <>  eRemoteDB.Constants.dtmNull Then
		
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdInputDate", mobjValues.TypeToString(lclsIntermedia.dInpdate, eFunctions.Values.eTypeData.etdDate),  , GetLocalResourceObject("tcdInputDateToolTip"),  ,  ,  ,  , llngAction = eFunctions.Menues.TypeActions.clngActionUpdate And lclsIntermedia.nInt_status <> 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("				<TD>")


Response.Write(mobjValues.DateControl("tcdInputDate", CStr(Today),  , GetLocalResourceObject("tcdInputDateToolTip"),  ,  ,  ,  , llngAction = eFunctions.Menues.TypeActions.clngActionUpdate And lclsIntermedia.nInt_status <> 3))


Response.Write("</TD>" & vbCrLf)
Response.Write("			")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=19448>" & GetLocalResourceObject("lblNullDateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            ")

	
	If Not IsNothing(lclsIntermedia.dNulldate) Then
		
Response.Write("" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.TextControl("lblNullDate", 10, CStr(lclsIntermedia.dNulldate), False, GetLocalResourceObject("lblNullDateToolTip"), True, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")

		
	Else
		
Response.Write("" & vbCrLf)
Response.Write("					<TD>")


Response.Write(mobjValues.TextControl("lblNullDate", 10, "", False, GetLocalResourceObject("lblNullDateToolTip"), True, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")

		
	End If
	
Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8231>" & GetLocalResourceObject("cbeIntStatusCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("				")

	
	If llngAction = eFunctions.Menues.TypeActions.clngActionUpdate Then
		
		'+ Si el intermediario está "Activo", no aparecerán en el combo los valores "Anulado" ni "En proceso..." ni "Suspendido" y se encontrará deshabilitado.
		If lclsIntermedia.nInt_status = 1 Then
			mobjValues.List = "2,3,4"
			
			'+ Si el intermediario está "Anulado", no aparecerán en el combo los valores "En proceso..." ni "Suspendido"
			'+ (solamente puede ser "Activado")
		ElseIf lclsIntermedia.nInt_status = 2 Then 
			mobjValues.List = "3,4"
			
			'+ Si el intermediario está "En proceso..." no aparecerá ningún otro valor en el combo y se inhabilitará
		ElseIf lclsIntermedia.nInt_status = 3 Then 
			mblnDisable = True
		ElseIf lclsIntermedia.nInt_status = 4 Then 
			mobjValues.List = "2,3"
		End If
		mobjValues.TypeList = 2 'Excluir
	End If
	
	
	If lclseRemote.OpenQuery("Table163", "sDescript", "nNullcode=" & lclsIntermedia.nNullcode) Then
		lstrCause_null = lclseRemote.FieldToClass("sDescript")
	End If
	
Response.Write("" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeIntStatus", "Table200", eFunctions.Values.eValuesType.clngComboType, CStr(lclsIntermedia.nInt_status),  ,  ,  ,  ,  , "SelectedItem(" & lclsIntermedia.nInt_status & ",this.value);", llngAction = eFunctions.Menues.TypeActions.clngActionAdd Or lclsIntermedia.nInt_status = 1,  , ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8233>" & GetLocalResourceObject("tctNullcodeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.TextControl("tctNullcode", 30, lstrCause_null, False, "", True, "", "", "", True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=8235>" & GetLocalResourceObject("cbeLegal_schCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeLegal_sch", "Table5501", 1, CStr(lclsIntermedia.nLegal_sch),  ,  ,  ,  ,  ,  ,  ,  , ""))


Response.Write("</TD>                    " & vbCrLf)
Response.Write("            <TD COLSPAN=""3""></TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("")

	
	Response.Write(mobjValues.BeginPageButton)
	If CStr(Session("sOriginalForm")) <> vbNullString And CStr(Session("sOriginalForm")) = "CA025" And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
		Response.Write("<SCRIPT>insEnabledFields();</" & "Script>")
	End If
	
	Response.Write("<SCRIPT>top.frames['fraHeader'].$('#valIntermedia').change();</" & "Script>")
	
	If lclsIntermedia.nOffice <> eRemoteDB.Constants.intNull Then
		If llngAction <> eFunctions.Menues.TypeActions.clngactionquery Then
			Response.Write("<SCRIPT>OpenDefValues(" & lclsIntermedia.nAgency & "," & lclsIntermedia.nOffice & "," & lclsIntermedia.nOfficeAgen & ");</" & "Script>")
		End If
	End If
	
	If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 306 Then
		'        Response.Write "<NOTSCRIPT>var nMainAction = "&Request.QueryString("nMainAction")&"</" & "Script>"	
		Response.Write("<SCRIPT>insDisableIntStatus();</" & "Script>")
	End If
	
	lclsIntermedia = Nothing
	lclseRemote = Nothing
	lclsIntermed_his = Nothing
	mobjValues = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ag001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ag001"
llngAction = Request.QueryString.Item("nMainAction")
mblnDisable = False
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("AG001", Request.QueryString.Item("sWindowDescript")))
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	.Write(mobjMenu.setZone(2, "AG001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing

'+ NDBC - 10/10/01 - Si es necesario crear un nuevo cliente, se le da el valor del Codispl de la ventana (AG001)
'+ a la variable de sesión 'sOriginalForm', a fin de trabajarla en la página ClientQueryValidate.aspx
Session("sOriginalForm") = "AG001"
%>
	
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 6/08/04 19:03 $"

//% ShowChangeValues: Se muestran la oficina y sucursal asociadas a la agencia en tratamiento
//-------------------------------------------------------------------------------------------
function ShowChangeValues(){
//-------------------------------------------------------------------------------------------	
	var lstrAgency="";
	
	lstrAgency = self.document.forms[0].cbeAgency.value;
	if(lstrAgency=="")
		lstrAgency=0;
	
	if(typeof(self.document.forms[0].cbeAgency)!='undefined')
	    insDefValues("Agencies", "nAgency=" + self.document.forms[0].cbeAgency.value + "&nOfficeAgen=" + self.document.forms[0].cbeOfficeAgen.value +"&nOffice=" + self.document.forms[0].cbeOffice.value,'/VTimeNet/Agent/AgentSeq')
}
	
//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd) {
//-------------------------------------------------------------------------------------------	
	with (self.document.forms[0]){
//+ Cambia la sucursal 
    	if (nInd == 1){
		    if (typeof(cbeOffice)!='undefined'){
		        if (cbeOffice.value != 0){
	  				if (typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
							cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
						else
							cbeAgency.Parameters.Param2.sValue = 0;
					}
			    }
				else{
	  				if(typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0){
							cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;}
						else{
							cbeAgency.Parameters.Param2.sValue = 0;}
					}
				}
			}
		}
//+ Cambia la oficina 
		else
		{
			if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
			    {
                cbeAgency.Parameters.Param1.sValue = cbeOffice.value;	
			    cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
			    }
			else{
			    cbeAgency.Parameters.Param1.sValue = 0;	
			    cbeAgency.Parameters.Param2.sValue = 0;
			    }
		}
	}	
}
	
//% insEnabledFields: Inhabilita los campos de la ventana que estén llenos si la variable
//%					  de sesión "sOriginalForm" es diferente de blanco - ACM - 07/08/2001
//---------------------------------------------------------------------------------------------------
function insEnabledFields(){
//---------------------------------------------------------------------------------------------------
	if(self.document.forms[0].elements["cbeInterType"].value=="" || self.document.forms[0].elements["cbeInterType"].value==0)
		self.document.forms[0].elements["cbeInterType"].disabled=false
	else
		self.document.forms[0].elements["cbeInterType"].disabled=true;
		
	if(self.document.forms[0].elements["tctLegalNum"].value=="" || self.document.forms[0].elements["tctLegalNum"].value==0)
		self.document.forms[0].elements["tctLegalNum"].disabled=false
	else
		self.document.forms[0].elements["tctLegalNum"].disabled=true;

	if(self.document.forms[0].elements["dtcClient"].value=="")
		self.document.forms[0].elements["dtcClient"].disabled=false
	else
		self.document.forms[0].elements["dtcClient"].disabled=true;

	if(self.document.forms[0].elements["cbeOffice"].value=="" || self.document.forms[0].elements["cbeOffice"].value==0)
		self.document.forms[0].elements["cbeOffice"].disabled=false
	else
		self.document.forms[0].elements["cbeOffice"].disabled=true;

	if(self.document.forms[0].elements["valSupervis"].value=="" || self.document.forms[0].elements["valSupervis"].value==0)
		self.document.forms[0].elements["valSupervis"].disabled=false
	else
		self.document.forms[0].elements["valSupervis"].disabled=true;

	if(self.document.forms[0].elements["tcdInputDate"].value=="")
		self.document.forms[0].elements["tcdInputDate"].disabled=false
	else
		self.document.forms[0].elements["tcdInputDate"].disabled=true;

	if(self.document.forms[0].elements["cbeIntStatus"].value=="" || self.document.forms[0].elements["cbeIntStatus"].value==0)
		self.document.forms[0].elements["cbeIntStatus"].disabled=false
	else
		self.document.forms[0].elements["cbeIntStatus"].disabled=true;

	if(self.document.forms[0].elements["tctNullcode"].value=="" || self.document.forms[0].elements["tctNullcode"].value==0)
		self.document.forms[0].elements["tctNullcode"].disabled=false
	else
		self.document.forms[0].elements["tctNullcode"].disabled=true;
}

//% DisabledField: Inhabilita algunos campos de la ventana dependiendo
//%				   de una condición - ACM - 13/05/2002
//-----------------------------------------------------------------------------------
function DisabledField(){
//-----------------------------------------------------------------------------------
	if(typeof(self.document.forms[0].elements["chkAll"])!='undefined' &&
		typeof(self.document.forms[0].elements["cbeInsu_area"])!='undefined')
	{
		if(self.document.forms[0].chkAll.checked)
		{			
			self.document.forms[0].cbeInsu_area.disabled = true;
			self.document.forms[0].cbeInsu_area.value=0;		
		}	
		else	
			self.document.forms[0].cbeInsu_area.disabled = false;
	}
}

//% OpenDefValues: Trae los valores por defecto del campo Agencia - ACM - 13/05/2002
//-------------------------------------------------------------------------------------
function OpenDefValues(nValue, nOffice, nOfficeAgen){
//-------------------------------------------------------------------------------------
	var strParams=''; 

	with (self.document.forms[0]){
		if(typeof(cbeAgency)!='undefined')		
			insDefValues("Agencies", "nAgency=" + nValue,'/VTimeNet/Agent/AgentSeq')
		else{
			if ((nOffice!="" && nOffice>0) &&
			   (nOfficeAgen!="" && nOfficeAgen>0)) { 
				strParams = "nAgency=" + nValue + 
				            "&nOfficeAgen=" + nOfficeAgen + 
				            "&nOffice=" + nOffice 
				insDefValues('Agencies',strParams,'/VTimeNet/Agent/AgentSeq');
			}
		}
	}
}

//% SelectedItem: No permite seleccionar algunos valores del combo "cbeIntStatus" - ACM - 13/05/2002
//-------------------------------------------------------------------------------------
function SelectedItem(nOld_Value, nNewValue)
//-------------------------------------------------------------------------------------
{
	if((nOld_Value==2 || nOld_Value==4) &&
		nNewValue!=1)
		self.document.forms[0].elements["cbeIntStatus"].value = nOld_Value;

	if(nOld_Value==1 && nNewValue==4)
		self.document.forms[0].elements["cbeIntStatus"].value = nOld_Value;
}

//% BlankFields: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%				 campo SUCURSAL cambia - ACM - 13/05/2002
//-------------------------------------------------------------------------------------
function BlankFields(nValue)
//-------------------------------------------------------------------------------------
{
	self.document.forms[0].elements["cbeOfficeAgen"].value="";
	UpdateDiv('cbeOfficeAgenDesc','');
	
	self.document.forms[0].elements["cbeAgency"].value="";
	UpdateDiv('cbeAgencyDesc','');

	self.document.forms[0].elements["cbeOffice"].value=nValue;
}

//% insDisableIntStatus: Inhabilita el campo estado cuando se esta duplicando
//---------------------------------------------------------------------------------------------------
function insDisableIntStatus(){
//---------------------------------------------------------------------------------------------------
	self.document.forms[0].elements["cbeIntStatus"].disabled = true
}


//% ChangeValid: Cambia el valor del check 
//---------------------------------------------------------------------------------------------------
function ChangeValid(Field){
//---------------------------------------------------------------------------------------------------
	if(Field.value=="1"){
		Field.value = "2"	
	}else{
		Field.value = "1"	
	}
}


</SCRIPT>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAG001" ACTION="valAgentSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
	<%Call InsPreAG001()%>
</FORM>
</BODY>
</HTML>


<script>insInitialAgency(1);DisabledField();</script>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.57
Call mobjNetFrameWork.FinishPage("ag001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




