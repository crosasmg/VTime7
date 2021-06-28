<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
Dim mintOneTime As Byte
Dim mintCount As Integer
Dim mlngReceipt As Integer
Dim mintDraft As Integer

'- Objeto para el manejo particular de los datos de la página
Dim mcolT_bulletins_dets As eCollection.T_bulletins_dets


'% insFindCO632A: Se efectua el find para verificar si existe información a mostrar en el grid.
'--------------------------------------------------------------------------------------------
Private Sub insFindCO632A()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_bulletins_det As Object
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		
		If mcolT_bulletins_dets.Find_CO632(mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("dCollectDate"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("sIndColl_exp"), Request.QueryString.Item("sStyle_bull"), Request.QueryString.Item("sQueryOption"), mobjValues.StringToType(Request.QueryString.Item("nBulletins"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCertype"), mobjValues.StringToType(Request.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), mobjValues.StringToType(Request.QueryString.Item("nReceipt"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nInsur_area"), eFunctions.Values.eTypeData.etdDouble), Session("nOneTime"), mobjValues.StringToType(Request.QueryString.Item("nCurrencyBul"), eFunctions.Values.eTypeData.etdDouble, True)) Then
			Session("nOneTime") = "0"
			Response.Write(mobjValues.HiddenControl("nItems", CStr(mcolT_bulletins_dets.Count)))
			mintCount = mcolT_bulletins_dets.Count
		Else
			Response.Write(mobjValues.HiddenControl("nItems", CStr(0)))
			mintCount = 0
		End If
		
	End If
	
End Sub

'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lobjColumn As eFunctions.Column
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "co632a"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		'+ Si no se muestran registros en el grid
		If Request.QueryString.Item("sQueryOption") = "0" Then
			lobjColumn = .AddPossiblesColumn(9999, GetLocalResourceObject("cbeCollecDocTypColumnCaption"), "cbeCollecDocTyp", "table5587", 1, CStr(0),  ,  ,  ,  , "insChangeTypeDoc(this, ""Add"");", Request.QueryString.Item("Action") <> "Add")
			lobjColumn.TypeList = 2
			lobjColumn.List = "3,4,5,6,7"
			'+ Si se muestran registros en el grid se excluye el tipo de documento recibo y cuota.
			
		Else
			lobjColumn = .AddPossiblesColumn(9999, GetLocalResourceObject("cbeCollecDocTypColumnCaption"), "cbeCollecDocTyp", "table5587", 1, CStr(0),  ,  ,  ,  , "insChangeTypeDoc(this, ""Add"");", Request.QueryString.Item("Action") <> "Add")
			lobjColumn.TypeList = 1
			lobjColumn.List = "1,2,17"
		End If
		
		Call .AddHiddenColumn("cbeBranch", "")
		Call .AddHiddenColumn("valProduct", "")
		Call .AddHiddenColumn("tcnPolicy", "")
		Call .AddHiddenColumn("tcnCertif", "")
		'+Para nro de docto se usa campo de texto ya que tambien se usa para desplegar 
		'+el nro de contrato y cuota de financiamiento de la forma : "contrato(cuota)"
		Call .AddTextColumn(9999, GetLocalResourceObject("tcnDocumentColumnCaption"), "tcnDocument", 10, "",  ,  ,  ,  , "ShowDocument();", True)
		Call .AddHiddenColumn("tcnContrat", "")
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			Call .AddNumericColumn(9999, GetLocalResourceObject("tcnDraftColumnCaption"), "tcnDraft", 5, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , "ShowDocument();", True)
		Else
			Call .AddHiddenColumn("tcnDraft", "")
		End If
		
		Call .AddHiddenColumn("tcnCod_agree", "")
		Call .AddHiddenColumn("dtcClient", "")
		
		'+ Se obtiene la moneda seleccionada en caso de que el grid tenga elementos.
		If Request.QueryString.Item("nCurrency") = "0" Then
			Call .AddPossiblesColumn(9999, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", 1)
		Else
			Call .AddPossiblesColumn(9999, GetLocalResourceObject("cbeCurrencyColumnCaption"), "cbeCurrency", "table11", 1, Request.QueryString.Item("nCurrency"),  ,  ,  ,  ,  , True)
		End If
		
		Call .AddNumericColumn(9999, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6,  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdStatDateColumnCaption"), "tcdStatDate", vbNullString,  , GetLocalResourceObject("tcdStatDateColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdExpirDatColumnCaption"), "tcdExpirDat", vbNullString,  , GetLocalResourceObject("tcdExpirDatColumnToolTip"),  ,  ,  , True)
		Call .AddClientColumn(0, GetLocalResourceObject("sClientColumnCaption"), "sClient", "",  , GetLocalResourceObject("sClientColumnToolTip"), "InsClient()", True)
		
		Call .AddHiddenColumn("tcnInsurArea", "")
		Call .AddHiddenColumn("tcdnStat_draft", "")
		Call .AddHiddenColumn("tcnStatus_pre", "")
		Call .AddHiddenColumn("tcnReceipt", "")
		Call .AddHiddenColumn("tctCertype", "2")
		Call .AddHiddenColumn("dCollectDateHdr", Request.QueryString.Item("dCollectDate"))
		Call .AddHiddenColumn("tcnId", CStr(0))
		Call .AddHiddenColumn("tcnDigit", "")
		Call .AddHiddenColumn("tcnPaynumbe", "")
		Call .AddHiddenColumn("nBulletinsHdr", Request.QueryString.Item("nBulletins"))
		Call .AddHiddenColumn("tcnType", "")
		Call .AddHiddenColumn("tcnTratypei", "")
		Call .AddHiddenColumn("sIndColl_expHdr", Request.QueryString.Item("sIndColl_exp"))
		Call .AddHiddenColumn("sStyle_bullHdr", Request.QueryString.Item("sStyle_bull"))
		Call .AddHiddenColumn("sQueryOptionHdr", Request.QueryString.Item("sQueryOption"))
		Call .AddHiddenColumn("tctCollector", "0")
		Call .AddHiddenColumn("nInsur_areaHdr", Request.QueryString.Item("nInsur_area"))
		Call .AddHiddenColumn("nBranchHdr", Request.QueryString.Item("nBranch"))
		Call .AddHiddenColumn("nProductHdr", Request.QueryString.Item("nProduct"))
		Call .AddHiddenColumn("nPolicyHdr", Request.QueryString.Item("nPolicy"))
		Call .AddHiddenColumn("nReceiptHdr", Request.QueryString.Item("nReceipt"))
		Call .AddHiddenColumn("sClientHdr", Request.QueryString.Item("sClient"))
		Call .AddHiddenColumn("sStatusHdr", Request.QueryString.Item("sStatus"))
		Call .AddHiddenColumn("nCurrencyHdr", "0")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CO632A"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbeCollecDocTyp").EditRecord = True
		.FieldsByRow = 2
		.Top = 50
		.Height = 320
		.Width = 660
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.AddButton = True
		.DeleteButton = False
		
		If mintCount = 0 Then
			.sEditRecordParam = "nCount=" & mintCount & "&nBulletins=" & Request.QueryString.Item("nBulletins") & "&nInsur_area=" & Request.QueryString.Item("nInsur_area") & "&dCollectDate=" & Request.QueryString.Item("dCollectDate") & "&sIndColl_exp=" & Request.QueryString.Item("sIndColl_exp") & "&sStyle_bull=" & Request.QueryString.Item("sStyle_bull") & "&sQueryOption=" & Request.QueryString.Item("sQueryOption") & "&sCertype=2" & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&sClient=" & Request.QueryString.Item("sClient") & "&nReceipt=" & Request.QueryString.Item("nReceipt") & "&nCurrencyBul=" & Request.QueryString.Item("nCurrencyBul") & "&sStatus=" & Request.QueryString.Item("tctStatus") & "&nCurrency=' + 0 + '"
		Else
			.sEditRecordParam = "nCount=" & mintCount & "&nBulletins=" & Request.QueryString.Item("nBulletins") & "&nInsur_area=" & Request.QueryString.Item("nInsur_area") & "&dCollectDate=" & Request.QueryString.Item("dCollectDate") & "&sIndColl_exp=" & Request.QueryString.Item("sIndColl_exp") & "&sStyle_bull=" & Request.QueryString.Item("sStyle_bull") & "&sQueryOption=" & Request.QueryString.Item("sQueryOption") & "&sCertype=2" & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nPolicy=" & Request.QueryString.Item("nPolicy") & "&sClient=" & Request.QueryString.Item("sClient") & "&nReceipt=" & Request.QueryString.Item("nReceipt") & "&nCurrencyBul=" & Request.QueryString.Item("nCurrencyBul") & "&sStatus=" & Request.QueryString.Item("tctStatus") & "&nCurrency=' + marrArray[0].nCurrencyHdr + '"
		End If
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCO632A: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCO632A()
	'Dim ShowTotalsBulletins() As Object
	'--------------------------------------------------------------------------------------------
	Dim lclsT_bulletins_det As Object
	Dim lintIndex As Short
	
	If mintCount > 0 Then
		mintOneTime = 0
		lintIndex = 0
		For	Each lclsT_bulletins_det In mcolT_bulletins_dets
			With mobjGrid
				'+ Se almacena la moneda del primer elemento del grid.
				If lintIndex = 0 Then
					.Columns("nCurrencyHdr").DefValue = lclsT_bulletins_det.nCurrency
				End If
				
				If lclsT_bulletins_det.sSel = "1" Then
					.Columns("Sel").checked = CShort("1")
				Else
					.Columns("Sel").checked = CShort("0")
				End If
				
				If lclsT_bulletins_det.nCollecdoctyp <> 8 Then
					.Columns("cbeCollecDocTyp").EditRecord = False
				Else
					.Columns("cbeCollecDocTyp").EditRecord = True
				End If
				
				.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				.Columns("cbeCollecDocTyp").DefValue = lclsT_bulletins_det.nCollecdoctyp
				
				If lclsT_bulletins_det.nBranch <> eRemoteDB.Constants.intNull Then
					.Columns("cbeBranch").DefValue = lclsT_bulletins_det.nBranch
					.Columns("valProduct").DefValue = lclsT_bulletins_det.nProduct 'Producto
				End If
				
				If lclsT_bulletins_det.nContrat = eRemoteDB.Constants.intNull Then
					If lclsT_bulletins_det.nReceipt = eRemoteDB.Constants.intNull Then
						.Columns("tcnDocument").DefValue = ""
					Else
						.Columns("tcnDocument").DefValue = lclsT_bulletins_det.nReceipt
					End If
				Else
					.Columns("tcnDocument").DefValue = lclsT_bulletins_det.nContrat & "(" & lclsT_bulletins_det.nDraft & ")"
				End If
				
				.Columns("tcnPolicy").DefValue = lclsT_bulletins_det.nPolicy
				.Columns("tcnCertif").DefValue = lclsT_bulletins_det.nCertif
				.Columns("tcnReceipt").DefValue = lclsT_bulletins_det.nReceipt
				.Columns("tcnDigit").DefValue = lclsT_bulletins_det.nDigit
				.Columns("tcnPaynumbe").DefValue = lclsT_bulletins_det.nPaynumbe
				.Columns("tcnContrat").DefValue = lclsT_bulletins_det.nContrat
				.Columns("dtcClient").DefValue = lclsT_bulletins_det.sClient
				.Columns("sClient").DefValue = lclsT_bulletins_det.sClient
				.Columns("sClient").Descript = lclsT_bulletins_det.sCliename
				.Columns("sClient").Digit = lclsT_bulletins_det.sClieDigit
				If lclsT_bulletins_det.nDraft <> eRemoteDB.Constants.intNull Then
					.Columns("tcnDraft").DefValue = lclsT_bulletins_det.nDraft
				End If
				.Columns("tcnCod_agree").DefValue = lclsT_bulletins_det.nCod_agree
				.Columns("tcdStatDate").DefValue = lclsT_bulletins_det.dStatdate
				.Columns("tcdExpirDat").DefValue = lclsT_bulletins_det.dExpirdat
				.Columns("tcnAmount").DefValue = lclsT_bulletins_det.nAmount
				.Columns("cbeCurrency").DefValue = lclsT_bulletins_det.nCurrency
				.Columns("tcnId").DefValue = lclsT_bulletins_det.nId
				.Columns("tcnType").DefValue = lclsT_bulletins_det.nType
				.Columns("tcnTratypei").DefValue = lclsT_bulletins_det.nTratypei
				
				lintIndex = lintIndex + 1
				Response.Write(.DoRow)
			End With
		Next lclsT_bulletins_det
		If lintIndex > 0 Then
			ShowTotalsBulletins()
		End If
	End If
	
	Response.Write(mobjGrid.closeTable())
End Sub

    Function ShowTotalsBulletins() As Double
        Dim lobjT_bulletins_det As eCollection.T_bulletins_det
        lobjT_bulletins_det = New eCollection.T_bulletins_det
        
        Dim mobjValues As eFunctions.Values = New eFunctions.Values
	
        With lobjT_bulletins_det
            .nBulletins = Session("nBulletins")
            .dCollectDate = Session("dCollectDate")
            .calTotalsBulletins()
            Response.Write("<SCRIPT>")
            ShowTotalsBulletins = .nTotalGeneral
            Response.Write("top.fraHeader.UpdateDiv('lblTotSaldo','" & mobjValues.TypeToString(ShowTotalsBulletins, eFunctions.Values.eTypeData.etdDouble, True, 6) & "');")
            Response.Write("</" & "Script>")
        End With
	
        lobjT_bulletins_det = Nothing
    End Function
    
'% insPreCO632AUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCO632AUpd()
	'--------------------------------------------------------------------------------------------
	Dim lclsT_bulletins_det As eCollection.T_bulletins_det
	
	lclsT_bulletins_det = New eCollection.T_bulletins_det
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			'If lclsT_bulletins_det.insPostCO632A() Then
			'End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valCollectionTra.aspx", "CO632A", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If Request.QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT> insChangeTypeDoc(self.document.forms[0].cbeCollecDocTyp, 'Update'); </" & "Script>")
		End If
	End With
End Sub

'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mlngReceipt = eRemoteDB.Constants.intNull
	mintDraft = eRemoteDB.Constants.intNull
	
End Sub

'% insOldValues: Se encarga de asignar los valores obtenidos en vbscript a javascript.
'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mlngReceipt <> eRemoteDB.Constants.intNull And mintDraft <> eRemoteDB.Constants.intNull Then
		With Response
			.Write("<SCRIPT>")
			.Write("var mlngReceipt = " & CStr(mlngReceipt) & ";")
			.Write("var mintDraft = " & CStr(mintDraft) & ";")
			.Write("</" & "Script>")
		End With
	Else
		With Response
			.Write("<SCRIPT>")
			.Write("var mlngReceipt = 0;")
			.Write("var mintDraft = -1;")
			.Write("</" & "Script>")
		End With
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co632a")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CO632A"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mcolT_bulletins_dets = New eCollection.T_bulletins_dets

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">




<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 5 $|$$Date: 28/05/04 17:59 $|$$Author: Nvaplat7 $"
    </SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CO632A", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
End If
Call insReaInitial()
Call insOldValues()
%>
<SCRIPT>
//% insCheckSelClick: Actuliza la columna sel del grid a la hora de seleccionar o deseleccionar un registro.
//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
		insDefValues("UpdSelCO632", "nBulletins=" + marrArray[lintIndex].nBulletinsHdr + "&nCollecDocTyp=" + marrArray[lintIndex].cbeCollecDocTyp + "&nId=" + marrArray[lintIndex].tcnId + "&sSel=" + (Field.checked?'1':'0'))
}
//% insChangeTypeDoc: Habilita y deshabilita las columnas de la ventana PopUp.
//-------------------------------------------------------------------------------------------
function insChangeTypeDoc(Field, sAction){
//-------------------------------------------------------------------------------------------
	var lblnAdd = (sAction=='Add'?true:false)
	with(self.document.forms[0]){
//+ Se deshabilitan todos los campos de la forma si se esta registrando información.
		if (lblnAdd) {
			cbeBranch.value = '';
			valProduct.value = '';
			tcnPolicy.value = '';
			tcnDocument.value='';
			tcnDraft.value = ''
			cbeCurrency.value = ''
			dtcClient.value = ''
			tcnAmount.value = '';
			tcdStatDate.value='';
			tcdExpirDat.value='';
			sClient.value='';	
			UpdateDiv('sClient_Name','','Normal');	
			sClient_Digit.value='';																
			cbeCollecDocTyp.disabled = !lblnAdd;
			tcnDraft.disabled = true
			tcnAmount.disabled = true;
			tcdStatDate.disabled = true;
			sClient.disabled = true;
			sClient_Digit.disabled = true;
			btn_tcdStatDate.disabled = tcdStatDate.disabled;
			tcdExpirDat.disabled = true;
			btn_tcdExpirDat.disabled = tcdExpirDat.disabled;
	    	}		
		
        switch (Field.value){
//+ Tipo de documeto: Recibo
            case "1":			
				tcnDocument.disabled = false;
				cbeCurrency.disabled = true;
				//+ Se habilitan los otros campos
				cbeCollecDocTyp.disabled = false;
                tcnDraft.disabled = true;
                tcnAmount.disabled = false;
                tcdStatDate.disabled = false;
                btn_tcdStatDate.disabled = false;
                sClient.disabled = true;
                tcdExpirDat.disabled = false;
                btn_tcdExpirDat.disabled = false;								
				break;
							
//+ Tipo de documeto: Cuota de financiamiento.
            case "2":			
				tcnDocument.disabled = false;
				cbeCurrency.disabled = true;		
				//+ Se habilitan los otros campos
				cbeCollecDocTyp.disabled = false;
                tcnDraft.disabled = false;
                tcnAmount.disabled = false;
                tcdStatDate.disabled = false;
                btn_tcdStatDate.disabled = false;
                tcdExpirDat.disabled = false;
                sClient.disabled = true;
                btn_tcdExpirDat.disabled = false;								
				break;
				
		 case "17":			
				tcnDocument.disabled = true;
				cbeCurrency.disabled = false;		
				//+ Se habilitan los otros campos
				cbeCollecDocTyp.disabled = false;
                tcnDraft.disabled = true;
                tcnAmount.disabled = false;
                tcdStatDate.disabled = false;
                btn_tcdStatDate.disabled = false;
                tcdExpirDat.disabled = false;
                sClient.disabled = false;
                btn_tcdExpirDat.disabled = false;								
				break;
				
//+ Tipo de documento: Saldo a favor del cliente 
           default :
				tcnDocument.disabled = true;
				cbeCurrency.disabled = false;
				//+ Se habilitan los otros campos				
				cbeCollecDocTyp.disabled = false;
                tcnDraft.disabled = true;
                tcnAmount.disabled = false;
                tcdStatDate.disabled = false;
                btn_tcdStatDate.disabled = false;
                tcdExpirDat.disabled = false;
                btn_tcdExpirDat.disabled = false;								
             break;                    
        }
    }
}

//% ShowDocument: Se encarga de mostrar la información dependiendo del tipo de documento seleccionado.
//-------------------------------------------------------------------------------------------
function ShowDocument(){
//-------------------------------------------------------------------------------------------
	var lstrParamString;
		
    with(self.document.forms[0]){
    
		if (tcnDocument.value > 0 && tcnDocument.value != '') {
			mlngReceipt = tcnDocument.value
			mintDraft = tcnDraft.value

		    switch (cbeCollecDocTyp.value){
		   
//+ Tipo de documeto: Recibo.
			    case "1":            
				    lstrParamString = "nCollecDocTyp=" + cbeCollecDocTyp.value + "&nReceipt=" + tcnDocument.value;
					insDefValues("ShowDataCO632", lstrParamString);
					break;
					
//+ Tipo de documeto: Cuota de financiamiento.
			    case "2":
			        if (tcnDraft.value >= 0 && tcnDraft.value != ''){
					insDefValues("ShowDataCO632", "nCollecDocTyp=" + cbeCollecDocTyp.value + "&nContrat=" + tcnDocument.value + "&nDraft=" + tcnDraft.value + "&sStyle_bull=" + document.forms[0].sStyle_bullHdr.value)
					}
					break;
					
                default:
					break;     
				

	        }
		} 
    }
}
function InsClient(){
    with(self.document.forms[0]){
		dtcClient.value = sClient.value;
     }

}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO632A" ACTION="valCollectionTra.aspx?sMode=2&<%=Request.Params.Get("Query_String")%>">
    <%Response.Write(mobjValues.ShowWindowsName("CO632A", Request.QueryString.Item("sWindowDescript")))

Call insFindCO632A()
Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCO632AUpd()
Else
	Call insPreCO632A()
End If
%>
</FORM> 
</BODY>
</HTML>


<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("co632a")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




