<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'-Cantidad de filas a mostrar en el grid    
Const CN_MAXROW As Short = 50

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del menú
Dim mblnFound As Boolean

Dim mcolDisc_xprem As ePolicy.Disc_xprems

Dim mintGroup As Object
Dim mstrTyp_discxp As String

'-Variable que guarda el error en el caso de requerido
Dim lclsGeneral As eGeneral.GeneralFunction
Dim mstrError As String

Dim mstrPolitype As Object
Dim mstrCertif As Object
Dim mblnShowCause As Boolean
Dim mintPageNum As Object
Dim mblnShowAgree As Boolean
Dim mstrShowAgree As String


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mcolDisc_xprem = New ePolicy.Disc_xprems
	
	mblnFound = False
	If CStr(Session("WindowType")) = "3" Then
		mcolDisc_xprem.nMasive = 3
	Else
		mcolDisc_xprem.nMasive = eRemoteDB.Constants.intNull
	End If
	If mcolDisc_xprem.insPreCA016(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), Session("nTransaction")) Then
		Session("WindowType") = ""
		mblnFound = True
	End If
	
	mintGroup = mcolDisc_xprem.nGroup
	mstrTyp_discxp = mcolDisc_xprem.sTyp_discxp
	
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddPossiblesColumn(0, GetLocalResourceObject("cboDisexpriColumnCaption"), "cboDisexpri", "Table30", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cboDisexpriColumnToolTip"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnDisc_codeColumnCaption"), "tcnDisc_code", 5, vbNullString,  , GetLocalResourceObject("tcnDisc_codeColumnToolTip"), False,  ,  ,  ,  , Request.QueryString.Item("Type") = "PopUp")
		.AddTextColumn(0, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , True)
            .AddNumericColumn(0, GetLocalResourceObject("tcnPercentColumnCaption"), "tcnPercent", 9, vbNullString, , GetLocalResourceObject("tcnPercentColumnToolTip"), , 6)
		.AddTextColumn(0, "", "lblPercent", 4, vbNullString,  , "",  ,  ,  , True)
		.AddNumericColumn(0, GetLocalResourceObject("tcnAmountColumnCaption"), "tcnAmount", 18, vbNullString,  , GetLocalResourceObject("tcnAmountColumnToolTip"), True, 6)
		If mblnShowCause Then
			.AddPossiblesColumn(0, GetLocalResourceObject("cbeCauseColumnCaption"), "cbeCause", "Table5631", eFunctions.Values.eValuesType.clngComboType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeCauseColumnToolTip"))
		Else
			.AddHiddenColumn("cbeCause", vbNullString)
		End If
		' Solo aplica para los recargo no aplica para descuentos e impuestos 
		If mblnShowAgree Then
			.AddCheckColumn(0, GetLocalResourceObject("chkAgreeColumnCaption"), "chkAgree", "", 1, CStr(1),  ,  , GetLocalResourceObject("chkAgreeColumnToolTip"))
		Else
			.AddHiddenColumn("chkAgree", "1")
		End If
		.AddHiddenColumn("hddnDisc_code", vbNullString)
		.AddHiddenColumn("hddnExist", vbNullString)
		.AddHiddenColumn("hddsSel", vbNullString)
		.AddHiddenColumn("hddsRequire", vbNullString)
		.AddHiddenColumn("hddsChanallo", vbNullString)
		.AddHiddenColumn("hddnPercent", vbNullString)
		.AddHiddenColumn("hddnAmount", vbNullString)
		.AddHiddenColumn("hddnOriPercent", vbNullString)
		.AddHiddenColumn("hddnOriAmount", vbNullString)
		.AddHiddenColumn("hddnDisexaddper", vbNullString)
		.AddHiddenColumn("hddnDisexsubper", vbNullString)
		.AddHiddenColumn("hddnCurrency", vbNullString)
		.AddHiddenColumn("hddnCause", vbNullString)
		.AddHiddenColumn("hddsAgree", vbNullString)
		.AddHiddenColumn("hddsDisexpri", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "CA016"
		.ActionQuery = mobjValues.ActionQuery
		.Top = 100
		.Height = 350
		.Width = 400
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.DeleteButton = False
		.AddButton = False
		
		.sEditRecordParam = "nGroup=" & mobjValues.StringToType(mintGroup, eFunctions.Values.eTypeData.etdDouble) & "&sTyp_discxp=" & mstrTyp_discxp & "&nPage=" & mintPageNum
		'                            & '                           "&sTyp_discxp1=' + self.document.forms[0].cboDisexpri[0] + '" 
		
		.Columns("tcnDisc_code").EditRecord = True
		.Columns("tctDescript").EditRecord = True
		.Columns("chkAgree").Disabled = Request.QueryString.Item("Type") <> "PopUp"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	Response.Write(mobjValues.HiddenControl("hddPage", mintPageNum))
	
End Sub


'% insPreCA016: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA016()
	'--------------------------------------------------------------------------------------------
	'- Objeto para el manejo particular de los datos de la página
	Dim lclsDisc_xprem As Object
	Dim lclsDisco_expr As eProduct.Disco_expr
	Dim lintCount As Short
	Dim lintIndex As Short
	Dim lblnOk As Boolean
	Dim lstrSel As String
	Dim lblnQuery As Boolean
	Dim lintFirstRow As Double
	Dim lintLastRow As Double
	Dim lintTotalPages As Double
	Dim ldblCalcPage As Double
	
	'+ Si las especificaciones de los recargos/descuentos/impuestos son por grupo
	If mcolDisc_xprem.sTyp_discxp = "3" Or Session("nCertif") > 0 Then
		
		Response.Write("<SCRIPT>mintGroup = '" & mintGroup & "';</" & "Script>")
		Response.Write("<TABLE WIDTH=100% COLS=4><TR>")
		Response.Write("<TD WIDTH=25><LABEL ID=13043>" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD><TD>")
		
		With mobjValues.Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Response.Write(mobjValues.PossiblesValues("valGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, mintGroup, True,  ,  ,  ,  , "insReload()", Session("nCertif") > 0 Or Not mcolDisc_xprem.bGroups Or mcolDisc_xprem.nCountGroup <= 1, 4, GetLocalResourceObject("valGroupToolTip")))
		Response.Write("</TD>")
		
		lblnOk = False
		
		If mblnFound Then
			'+ Si no se trata de consulta	
			If Not mobjValues.ActionQuery Then
				'+ Si el tratamiento es de certificado de la póliza matriz
				If Session("nCerif") = 0 Then
					'+ Si existen más de un grupo a tratar
					If mcolDisc_xprem.nCountGroup > 1 Then
						lblnOk = True
					End If
				End If
			End If
		End If
		If lblnOk Then
			Response.Write("<TD COLSPAN=""5"">" & "</TD>")
			Response.Write("<TD WIDTH=""5%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
		End If
		Response.Write("</TD></TR></TABLE>")
	End If
	
	Response.Write(mobjValues.HiddenControl("hddsTyp_discxp", mcolDisc_xprem.sTyp_discxp))
	
	'+ Variable para controlar la actualización de la información de manera puntual (desde el botón de la ventana)
	Response.Write(mobjValues.HiddenControl("hddbPuntual", CStr(False)))
	
	'+ Si existe información, se procesa.
	Dim lobjError As eFunctions.Errors
	If mblnFound Then
		
		'+Se calcula el primer y ultimo registro	    
		lintFirstRow = ((mintPageNum - 1) * CN_MAXROW) + 1
		lintLastRow = mintPageNum * CN_MAXROW
		ldblCalcPage = mcolDisc_xprem.Count / CN_MAXROW
		lintTotalPages = CShort(ldblCalcPage)
		If lintTotalPages <> ldblCalcPage Then
			lintTotalPages = lintTotalPages + 1
		End If
		
		'+Se inicializan los marcadores de registros cargados en grid y registros leidos
		lintIndex = 0
		lintCount = 0
		lblnQuery = mobjGrid.ActionQuery
		With mobjGrid
			For	Each lclsDisc_xprem In mcolDisc_xprem
				'+Si está fuera del rango no se procesa            
				
				If (lintCount + 1) < lintFirstRow Or (lintCount + 1) > lintLastRow Then
					
				Else
					If lclsDisc_xprem.sDefaulti = "1" Or lclsDisc_xprem.sRequire = "1" Or lclsDisc_xprem.nExist = "1" Then
						lstrSel = "1"
					Else
						lstrSel = lclsDisc_xprem.sSel(mobjValues.ActionQuery)
						
					End If
					.Columns("Sel").Checked = CShort(lstrSel)
					'+ Si se está consultando se muestran sólo los recargos seleccionados para la póliza 
					If (lblnQuery And lstrSel = "1") Or Not lblnQuery Then
						'+ Se rescata si es recargo, descuento o impuesto del producto.
						lclsDisco_expr = New eProduct.Disco_expr
						Call lclsDisco_expr.Find(Session("nBranch"), Session("nProduct"), lclsDisc_xprem.nDisc_code, Session("dEffecdate"))
						.Columns("hddsDisexpri").DefValue = lclsDisco_expr.sDisexpri
						lclsDisco_expr = Nothing
						
						.Columns("hddsSel").DefValue = lstrSel
						.Columns("Sel").OnClick = "InsCheckSelClick(this," & lclsDisc_xprem.sRequire & "," & CStr(lintIndex) & "," & lclsDisc_xprem.nPercent & ")"
						.Columns("cboDisexpri").DefValue = lclsDisc_xprem.sDisexpri
						.Columns("cboDisexpri").Descript = lclsDisc_xprem.sDisexpriDesc
						.Columns("tcnDisc_code").DefValue = lclsDisc_xprem.nDisc_code
						.Columns("tctDescript").DefValue = lclsDisc_xprem.sDescript
						.Columns("tcnPercent").DefValue = lclsDisc_xprem.nPercent
						.Columns("lblPercent").DefValue = lclsDisc_xprem.sFactor
						.Columns("tcnAmount").DefValue = lclsDisc_xprem.nAmount
						.Columns("cbeCause").DefValue = lclsDisc_xprem.nCause
						.Columns("cbeCause").Descript = lclsDisc_xprem.sCauseDesc
						If lclsDisc_xprem.sAgree = "1" Or (lclsDisc_xprem.sDisexpri <> "1" And lclsDisc_xprem.sDisexpri <> "4") Then
							.Columns("chkAgree").Checked = CShort("1")
							.Columns("hddsAgree").DefValue = "1"
						Else
							.Columns("chkAgree").Checked = CShort(mstrShowAgree)
							.Columns("hddsAgree").DefValue = mstrShowAgree
						End If
						.Columns("hddnDisc_code").DefValue = lclsDisc_xprem.nDisc_code
						.Columns("hddnPercent").DefValue = Replace(lclsDisc_xprem.nPercent,",",".")
						.Columns("hddnAmount").DefValue = Replace(lclsDisc_xprem.nAmount,",",".")
						.Columns("hddnExist").DefValue = lclsDisc_xprem.nExist
						.Columns("hddsRequire").DefValue = lclsDisc_xprem.sRequire
						.Columns("hddsChanallo").DefValue = lclsDisc_xprem.sChanallo
						.Columns("hddnOriPercent").DefValue = Replace(lclsDisc_xprem.nOriPercent,",",".")
						.Columns("hddnOriAmount").DefValue = Replace(lclsDisc_xprem.nOriAmount,",",".")
						.Columns("hddnDisexaddper").DefValue = Replace(lclsDisc_xprem.nDisexaddper,",",".")
						.Columns("hddnDisexsubper").DefValue = Replace(lclsDisc_xprem.nDisexsubper,",",".")
						.Columns("hddnCurrency").DefValue = lclsDisc_xprem.nCurrency
						.Columns("hddnCause").DefValue = lclsDisc_xprem.nCause
						lintIndex = lintIndex + 1
						Response.Write(.DoRow)
					End If
				End If
				lintCount = lintCount + 1
				'				.sEditRecordParam = "nGroup=" & mobjValues.StringToType(mintGroup,eFunctions.Values.eTypeData.etdDouble) & '				                    "&sTyp_discxp=" & lclsDisc_xprem.sDisexpri & '				                    "&nPage=" & mintPageNum
			Next lclsDisc_xprem
		End With
	Else
		'+ Si existe algún error
		If mcolDisc_xprem.nError > 0 Then
			lobjError = New eFunctions.Errors
			'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
			lobjError.sSessionID = Session.SessionID
			lobjError.nUsercode = Session("nUsercode")
			'~End Body Block VisualTimer Utility
			Response.Write(lobjError.ErrorMessage(Request.QueryString.Item("sCodispl"), mcolDisc_xprem.nError,  ,  ,  , True))
			lobjError = Nothing
		End If
	End If
	
	Response.Write(mobjGrid.closeTable())
	Response.Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"),  , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nPage")) <= 1 Or IsNothing(Request.QueryString.Item("nPage"))))
	Response.Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"),  , "ControlNextBack('Next')", CShort(lintTotalPages) = CShort(mintPageNum)))
	Response.Write(mobjValues.BeginPageButton)
	
End Sub

'% insPreCA016Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA016Upd()
	'--------------------------------------------------------------------------------------------
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		If .QueryString.Item("Action") = "Update" Then
			Response.Write("<SCRIPT>DisabledAgree(top.opener.marrArray[CurrentIndex].cboDisexpri)</" & "Script>")
		End If
	End With
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA016")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	lclsGeneral = New eGeneral.GeneralFunction
End With

mobjValues.ActionQuery = Session("bQuery")
mstrError = lclsGeneral.insLoadMessage(3676)
lclsGeneral = Nothing

mstrPolitype = Session("sPolitype")
mstrCertif = Session("nCertif")

'+Se determina si se muestra columna de causa    
mblnShowCause = mstrPolitype = "1" Or mstrCertif > 0

'+Se determina si se muestra columna de Aceptado de sobreprima     
'+ Solo aplica para vida y vidactiva 
mstrShowAgree = "1"
If CStr(Session("sbrancht")) = "1" Then
	If CStr(Session("sPolitype")) = "1" Then
		mblnShowAgree = True
		mstrShowAgree = "2"
	End If
End If

'+Se obtiene nro de página
mintPageNum = Request.QueryString.Item("nPage")
If mintPageNum = vbNullString Then mintPageNum = 1

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 11 $|$$Date: 8/10/04 13:02 $|$$Author: Nvaplat15 $"

	var mintGroup = 0;
	
//%InsCheckSelClick: Valida que el elemento no sea requerido, además actualiza el indicador
//%                  auxiliar de selección.
//-------------------------------------------------------------------------------------------
function InsCheckSelClick(Field, sRequire, nIndex, nPercent){
//-------------------------------------------------------------------------------------------
    if (!Field.checked)
       if (sRequire=='1'){
            alert('<%="Err 3676. " & mstrError%>')
            Field.checked = !Field.checked
       }
       if (sRequire=='3'){
            alert('No debe desmarcar un recargo por asegurado desde esta ventana')
            Field.checked = !Field.checked
       }       

    with (self.document.forms[0]){
        if (typeof(hddsSel.length) == 'undefined')
            hddsSel.value = (Field.checked?1:2);
        else
            hddsSel(nIndex).value = (Field.checked?1:2);
    }
}

//% insReload: Se encarga de recargar la página al seleccionar cualquier valor de los campos del encabezado del grid.
//-------------------------------------------------------------------------------------------
function insReload(){
//-------------------------------------------------------------------------------------------
    var lstrQuery
    var lblnChange
	
	lblnChange = false
    with (self.document.forms[0]) {
//+ Caso en que el grupo esté visible
		if (typeof(valGroup)!='undefined') {
			if (mintGroup!=(valGroup.value==''?0:valGroup.value)) {
			    lblnChange = true;
			    mintGroup = valGroup.value;
				lstrQuery = "&nGroup=" + valGroup.value
			}
		} else
			lstrQuery = lstrQuery + "&nGroup=0"

		if (lblnChange==true) {
			document.location.href = document.location.href.replace(/&nGroup=.*/,'') + lstrQuery
		}
    }
}
 
//% insAccept: Se acepta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		self.document.forms[0].hddbPuntual.value = true;
	}
	top.frames['fraHeader'].ClientRequest(390,2);
}

//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros
//-------------------------------------------------------------------------------------------
function ControlNextBack(Option){
//-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngPage = lstrURL.substr(lstrURL.indexOf("&nPage=") + 7)
    lstrURL = lstrURL.replace(/&nPage=.*/,'')
	switch(Option){
		case "Next":
			if(isNaN(llngPage))
				lstrURL = lstrURL + "&nPage=2"
			else{
				llngPage = insConvertNumber(llngPage) + 1;
				lstrURL = lstrURL + "&nPage=" + llngPage
			}
			break;

		case "Back":
			if(!isNaN(llngPage)){
				llngPage = insConvertNumber(llngPage) - 1;
				lstrURL = lstrURL + "&nPage=" + llngPage
			}
	}
	self.document.location.href = lstrURL;
}	
//% DisabledAgree: Habilita o deshabilita 
//----------------------------------------------------------------------------------------------------
function DisabledAgree(Field){
//----------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
        if(Field==1 || Field==4){
            chkAgree.disabled=false;
        }
        else {
            chkAgree.value='1';
            chkAgree.checked=true;
            chkAgree.disabled=true;
        }
    }
}
</SCRIPT>
<%

Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA016" ACTION="ValPolicySeq.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Response.Flush()

mobjNetFrameWork.BeginProcess("DefineHeader")
Call insDefineHeader()
mobjNetFrameWork.FinishProcess("DefineHeader")

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCA016Upd()
Else
	Call insPreCA016()
End If
mcolDisc_xprem = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA016")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




