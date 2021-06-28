<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim sClient_Contact As Object



'%insDefineHeader: Permite definir las columnas del grid, así como también de habilitar o inhabilitar
'%los botones de agregar y cancelar.
'---------------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCod_agreeColumnCaption"), "tcnCod_agree", 5,  , False, GetLocalResourceObject("tcnCod_agreeColumnToolTip"),  , 0,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeAgreeColumnCaption"), "cbeTypeAgree", "Table5529", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  , "insChangeType(this.value,1);",  ,  , GetLocalResourceObject("cbeTypeAgreeColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnQ_draftColumnCaption"), "tcnQ_draft", 5,  , False, GetLocalResourceObject("tcnQ_draftColumnToolTip"),  , 0)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnMax_perc_dctoColumnCaption"), "tcnMax_perc_dcto", 4,  , False, GetLocalResourceObject("tcnMax_perc_dctoColumnToolTip"),  , 2)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInit_dateColumnCaption"), "tcdInit_date",  ,  , GetLocalResourceObject("tcdInit_dateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEnd_dateColumnCaption"), "tcdEnd_date",  ,  , GetLocalResourceObject("tcdEnd_dateColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeStatregtColumnCaption"), "cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatregtColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeType_RecColumnCaption"), "cbeType_Rec", "Table5581", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeType_RecColumnToolTip"))
		Call .AddPossiblesColumn(40991, GetLocalResourceObject("valIntermedColumnCaption"), "valIntermed", "TabIntermedia", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , 10, GetLocalResourceObject("valIntermedColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAgencyColumnCaption"), "cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeAgencyColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tctFisrtNameColumnCaption"), "tctFisrtName", 19, "",  , GetLocalResourceObject("tctFisrtNameColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctLastNameColumnCaption"), "tctLastName", 19, "",  , GetLocalResourceObject("tctLastNameColumnToolTip"),  ,  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctsNameColumnCaption"), "tctsName", 60, "",  , GetLocalResourceObject("tctsNameColumnToolTip"),  ,  ,  , False)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("tcnPositionColumnCaption"), "tcnPosition", "Table283", 1, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("tcnPositionColumnToolTip"))
		Call .AddTextColumn(0, GetLocalResourceObject("tcnmailColumnCaption"), "tcnmail", 60, "")
		Call .AddTextColumn(0, GetLocalResourceObject("tcnphoneColumnCaption"), "tcnphone", 20, "")
		Call .AddTextColumn(0, GetLocalResourceObject("tctsName_agreeColumnCaption"), "tctsName_agree", 60, "")
            If Request.QueryString.Item("Type") <> "PopUp" Then
                Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddressColumnCaption"), "cmdAddress", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddressColumnToolTip"))
            End If
            Call .AddCheckColumn(0,GetLocalResourceObject("chknocollectionCaption"), "chknocollection", vbNullString, , , , , )
           
		
        End With
	
	With mobjGrid
		.Columns("tcnCod_agree").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		.Columns("tcnCod_agree").EditRecord = Not .ActionQuery
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		
		.Height = 630
		.Width = 590
		.Top = 50
		
		.Codispl = "MCO505"
		.Codisp = "MCO505"
		.sCodisplPage = "MCO505"
		
		.MoveRecordScript = "insChangeType(self.document.forms[0].cbeTypeAgree.value,0)"
		
		.sDelRecordParam = "nCod_agree=' + marrArray[lintIndex].tcnCod_agree + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
        End If
        
            If Request.QueryString.Item("Type") <> "PopUp" Then
                .Columns("chknocollection").Disabled = True
            End If
            
	End With
	
End Sub

'%insPreMCO505: Se definen los objetos a ser utilizados a lo largo de la transacción.
'-----------------------------------------------------------------------------------------
Private Sub insPreMCO505()
	'-----------------------------------------------------------------------------------------
	Dim lintIndex As Short
	Dim lcolAgreements As eCollection.Agreements
	Dim lclsAgreement As Object
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//%insPreZone: Se definen las acciones." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
	'+ Se setean los objetos y se realiza el llamado al método que realiza la 
	'+ lectura de los registros a mostrar en las columnas del grid.
	
	lcolAgreements = New eCollection.Agreements
	If lcolAgreements.Find_sClient( , Session("sClient"), True) Then
		lintIndex = 0
		For	Each lclsAgreement In lcolAgreements
			With mobjGrid
				.Columns("tcnCod_agree").DefValue = lclsAgreement.nCod_agree
				.Columns("tcnQ_draft").DefValue = lclsAgreement.nQ_draft
				.Columns("tcnMax_perc_dcto").DefValue = lclsAgreement.nMax_perc_dcto
				.Columns("tcdInit_date").DefValue = lclsAgreement.dInit_date
				.Columns("tcdEnd_date").DefValue = lclsAgreement.dEnd_date
				.Columns("cbeStatregt").DefValue = lclsAgreement.sStatregt
				.Columns("cbeStatregt").Descript = lclsAgreement.sStatregt_desc
				.Columns("cbeTypeAgree").DefValue = lclsAgreement.nTypeAgree
				.Columns("cbeTypeAgree").Descript = lclsAgreement.sTypeAgree_desc
				.Columns("cbeType_Rec").DefValue = lclsAgreement.nType_Rec
				.Columns("cbeType_Rec").Descript = lclsAgreement.sType_Rec_desc
				.Columns("valintermed").DefValue = lclsAgreement.nIntermed
				.Columns("valintermed").Descript = lclsAgreement.sIntermed_desc
				.Columns("cbeAgency").DefValue = lclsAgreement.nAgency
				.Columns("cbeAgency").Descript = lclsAgreement.sAgency_desc
				.Columns("tctFisrtName").DefValue = lclsAgreement.sFirstName
				.Columns("tctLastName").DefValue = lclsAgreement.sLastName
				.Columns("tctsName").DefValue = lclsAgreement.sCliename
				.Columns("tcnPosition").DefValue = lclsAgreement.nposition
				.Columns("tcnmail").DefValue = lclsAgreement.sEmail_Contact
				.Columns("tcnphone").DefValue = lclsAgreement.sPhone_Contact
				.Columns("tctsName_agree").DefValue = lclsAgreement.sName_Agree
				.Columns("cmdAddress").HRefScript = "ShowPopUp('/VTimeNet/Common/sca001upd.aspx?sCodispl=MCO505&sOnSeq=2&sRectype=1&ncod_agree=" & lclsAgreement.nCod_agree & "','ShowAddress',700,400,'yes','yes',150,150)"
                '.Columns("chknocollection").DefValue = lclsAgreement.snocollection
                    If lclsAgreement.snocollection = "1" Then
                        .Columns("chknocollection").Checked = 1
                    Else
                        .Columns("chknocollection").Checked = 0
                    End If
                Response.Write(.DoRow)
				lintIndex = lintIndex + 1
			End With
		Next lclsAgreement
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolAgreements = Nothing
	
End Sub

'% insPreMCO505Upd: Permite realizar el llamado a la ventana PopUp.
'-----------------------------------------------------------------------------------------
Private Sub insPreMCO505Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsAgreement As eCollection.Agreement
	Dim lobjError As eFunctions.Errors
	lclsAgreement = New eCollection.Agreement
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		If lclsAgreement.Count_Policy(mobjValues.StringToType(Request.QueryString.Item("nCod_agree"), eFunctions.Values.eTypeData.etdDouble)) Then
			
			lobjError = New eFunctions.Errors
			lobjError.Highlighted = True
			Response.Write(lobjError.ErrorMessage("c", 55025,  ,  ,  , True))
		Else
                Call lclsAgreement.insPostMCO505("Delete", Session("sClient"), mobjValues.StringToType(Request.QueryString.Item("nCod_agree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnQ_draft"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnMax_perc_dcto"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdInit_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("tcdEnd_date"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("cbeTypeagree"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("valIntermed"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeAgency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeType_rec"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tctFisrtName"), Request.Form.Item("tctLastName"), Request.Form.Item("tctsName"), mobjValues.StringToType(Request.Form.Item("tcnPosition"), eFunctions.Values.eTypeData.etdDouble, True), Request.Form.Item("tcnmail"), Request.Form.Item("tcnphone"), Request.Form.Item("tctsName_Agree"), Request.Form.Item("chknocollection"))
		End If
	End If
	lclsAgreement = Nothing
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantCollection.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = -1


mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MCO505"
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
<%	
End If
%>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%=mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl"))%>




<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		
		.Write("<SCRIPT>var nMainAction=304</SCRIPT>")
		.Write(mobjMenu.setZone(2, "MCO505", "MCO505.aspx"))
		
		mobjMenu = Nothing
	End If
End With
%>

<SCRIPT>

//- Variable para el control de versiones
    document.VssVersion="$$Revision: 8 $|$$Date: 20/10/04 3:28p $|$$Author: Nvapla10 $"

//%insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//------------------------------------------------------------------------------------------
function  insChangeType(nType,nChange){
//------------------------------------------------------------------------------------------

/*+ Cuotas */
        document.getElementsByTagName("TR")[2].style.display='';
/*+ % Descuento */
        document.getElementsByTagName("TR")[3].style.display='';
/*+ Tipo de Recaudación */
        document.getElementsByTagName("TR")[7].style.display='';
/*+ Intermediario */
        document.getElementsByTagName("TR")[8].style.display='';
/*+ Agencia */
        document.getElementsByTagName("TR")[10].style.display='';
/*+ Apellido Paterno */
        document.getElementsByTagName("TR")[12].style.display='';
/*+ Apellido Materno */
        document.getElementsByTagName("TR")[13].style.display='';
/*+ Nombres */
        document.getElementsByTagName("TR")[14].style.display='';
/*+ Redimensionamos la ventana */        
//        top.window.resizeTo(630,590);

    }
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="MCO505" ACTION="valMantCollection.aspx?mode=1">
<%

'Si el nExits= 1, entonces el código del cliente no existe en el sistema, por lo tanto se envia a su creacion
'en la secuencia BC003_k    

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName("MCO505"))
	Call insPreMCO505()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreMCO505Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing

If Request.QueryString.Item("Type") = "PopUp" Then
	'Response.Write "<NOTSCRIPT>insChangeType(self.document.forms[0].cbeTypeAgree.value,0);</SCRIPT>"
End If

%>
</FORM>
</BODY>
</HTML>






