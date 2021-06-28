<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Se define la variable para la carga de datos del Grid de la ventana		
Dim mclsDocuments As eClaim.Documents
Dim mcolDocumentss As eClaim.Documentss


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------	    
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "si015"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
        mobjGrid.ActionQuery = Session("bQuery")
    Else
            mobjGrid.ActionQuery = False
            Session("bQuery") = False
    End If
	With mobjGrid
		.Codispl = "SI015"
		.Top = 150
		.Left = 100
		.Width = 600
		.Height = 380
		.MoveRecordScript = "ChangePropodate()"
		
		
		.sEditRecordParam = "sCase='+ getCasePart()+'&nCaseNum='+ getCasePart(0)+'&nDemanType='+ getCasePart(1)+'&sClient='+ getCasePart(2)+'&nId='+ getCasePart(3)+'"
	End With
	
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		
		If Request.QueryString("Action") = "Add" Or Request.QueryString("Action") = "Update" Then
			Call .AddPossiblesColumn(0, "Documento", "tcnCode", "TABTAB_DOCU", eFunctions.Values.eValuesType.clngComboType, CStr(0), True,  ,  ,  , "", Request.QueryString("Action") <> "Add",  , "Tipo de documento solicitado.")
			.Item("tcnCode").BlankPosition = False
			.Item("tcnCode").Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Item("tcnCode").Parameters.Add("nCase_num", Request.QueryString("nCaseNum"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Item("tcnCode").Parameters.Add("nDeman_type", Request.QueryString("nDemanType"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Else
			Call .AddHiddenColumn("tcnCode", "0")
			Call .AddTextColumn(40325, "Documento", "tctDescript", 45, "0",  , "Documento solicitado de acuerdo al producto al que pertenece la póliza asociada al siniestro",  ,  ,  , True)
			mobjGrid.Columns("tctDescript").EditRecord = True
		End If
		Call .AddTextColumn(0, "Descripción", "tctDescdocu", 45, "0",  , "Descripción adicional del documento",  ,  ,  , False)
		
		Call .AddNumericColumn(0, "Nº documento", "tcnDocnumbe", 10,  ,  , "Número que identifica el documento")
		Call .AddNumericColumn(0, "Cantidad", "tcnQuantity", 5,  ,  , "Cantidad de documentos recepcionados")
		Call .AddNumericColumn(0, "Monto", "tcnAmount", 18,  ,  , "Cantidad de documentos recepcionados",,6)
		Call .AddPossiblesColumn(0, "Moneda", "cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  , False,  , vbNullString)
		Call .AddDateColumn(0, "Fecha solicitud", "tcdPropo_date", "",  , "Fecha en que ha sido solicitado el documento",  ,  , "ChangePropodate()", False)
		Call .AddDateColumn(0, "Fecha límite", "tcdPrescdate", "",  , "Fecha límite para la entrega del documento en tratamiento",  ,  ,  , False)
		Call .AddDateColumn(0, "Fecha de recepción", "tcdRecepdate", "",  , "Fecha en la que la empresa ha recibido el documento",  ,  ,  , False)
		Call .AddHiddenColumn("tcnDoc_code", "0")
		Call .AddHiddenColumn("tcnDays_Presc", "0")
		Call .AddHiddenColumn("tcnConsec", "0")
	End With
	With mobjGrid
		.Columns("Sel").GridVisible = True
		.DeleteButton = False
		.AddButton = True
		If Request.QueryString("Reload") = "1" Then
			.sReloadIndex = Request.QueryString("ReloadIndex")
		End If
	End With
End Sub

'%insPreSI015: Esta función se encarga de cargar los datos en la forma
'--------------------------------------------------------------------------------------------
Private Sub insPreSI015()
	'--------------------------------------------------------------------------------------------
	Dim lintCase_num As Object
	Dim lintDeman_type As Object
	Dim lstrClient As Object
	Dim lintIndex As Short
	Dim lstrId As Object
	Dim larrCase() As Object
	
	
Response.Write("	" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=9547>Caso</LABEL></TD>")

	With mobjValues
		.Parameters.Add("nClaim", Session("nClaim"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("sDemandant", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.BlankPosition = False
		
		Response.Write("<TD>" & mobjValues.PossiblesValues("cbeCase", "TabBuildingAllCases", 1, Request.QueryString("sCase"), True,  ,  ,  ,  , "ChangeCase(this.value)",  ,  , "Caso del cliente al que se le realiza la solicitud de documentos", eFunctions.Values.eTypeCode.eString) & "</TD>")
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            <TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=""4"">&nbsp;</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	
	If mobjValues.CodeValue <> vbNullString Then
        larrCase = mobjValues.CodeValue.Split("/")
		lintCase_num = larrCase(0)
		lintDeman_type = larrCase(1)
		lstrClient = larrCase(2)
		lstrId = larrCase(3)
	Else
		lintCase_num = 0
		lintDeman_type = 0
		lstrClient = 0
		lstrId = 0
	End If
	
	If mobjValues.CodeValue <> vbNullString Then
		If mcolDocumentss.Find(CDbl(Session("nClaim")), lintCase_num, lintDeman_type, lstrClient, lstrId, CInt(Session("nBranch"))) Then
			lintIndex = 0
			For	Each mclsDocuments In mcolDocumentss
				With mobjGrid
					.Columns("tctDescdocu").DefValue = mclsDocuments.sDesc_docu
					.Columns("tctDescript").DefValue = mclsDocuments.sDescript
					.Columns("tcdRecepdate").DefValue = CStr(mclsDocuments.dRecepdate)
					.Columns("tcnCode").DefValue = mobjValues.TypeToString(mclsDocuments.nCode, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcnDoc_code").DefValue = mobjValues.TypeToString(mclsDocuments.nDoc_code, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcnDocnumbe").DefValue = mobjValues.TypeToString(mclsDocuments.nDocnumbe, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcnQuantity").DefValue = mobjValues.TypeToString(mclsDocuments.nQuantity, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcnDays_Presc").DefValue = mobjValues.TypeToString(mclsDocuments.nDays_Presc, eFunctions.Values.eTypeData.etdDouble)
					.Columns("tcdPropo_date").DefValue = CStr(mclsDocuments.dPropo_date)
					.Columns("tcdPrescdate").DefValue = CStr(mclsDocuments.dPrescdate)
					.Columns("tcnConsec").DefValue = CStr(mclsDocuments.nConsec)
					.Columns("tcnAmount").DefValue = mclsDocuments.nAmount
					.Columns("cbeCurrency").DefValue = mclsDocuments.nCurrency
					
					If mclsDocuments.nDoc_code = 0 Or mclsDocuments.nDoc_code = eRemoteDB.Constants.intNull Then
						.Columns("Sel").checked = CShort("2")
					Else
						.Columns("Sel").checked = CShort("1")
					End If
					.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				End With
				Response.Write(mobjGrid.DoRow())
				lintIndex = lintIndex + 1
			Next mclsDocuments
		End If
		Response.Write(mobjGrid.closeTable())
	End If
	Response.Write("<SCRIPT>DisabledCase()</" & "Script>")
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("si015")

'- Se crean las instancias de las variables modulares
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "si015"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsDocuments = New eClaim.Documents
mcolDocumentss = New eClaim.Documentss

If Request.QueryString("Type") <> "PopUp" Then
	With Response
		.Write(mobjMenu.setZone(2, "SI015", "SI015.aspx"))
		.Write("<SCRIPT> var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End With
End If
Response.Write(mobjValues.StyleSheet() & vbCrLf)
%>
    <%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 2-05-13 9:20 $"        
//%ChangePropodate: Actualiza la fecha límite al cambiar la fecha solicitud
//-------------------------------------------------------------------------------------------
function ChangePropodate(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){		
		if (tcdPropo_date.value == '')
			tcdPropo_date.value = '<%'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'%>
<%=mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)%>';

		insDefValues('PrescDate','nClaim=' + <%=Session("nClaim")%> + '&dPropoDate=' + tcdPropo_date.value + '&nDays_Presc=' + tcnDays_Presc.value ,'/VTimeNet/Claim/ClaimSeq');
	}
}

//%PrepareAdditionalDocument: 
//-------------------------------------------------------------------------------------------
function PreparePopUp(){
//-------------------------------------------------------------------------------------------
	<%If Request.QueryString("Action") = "Add" Then%>
	document.getElementsByName("tctDescdocu")[0].value = "";
	//document.getElementsByName("tctDescript")[0].value= "Adicional";
	document.getElementsByName("tcnCode")[0].value=9798;
	document.getElementsByName("tcnConsec")[0].value=0;
	<%End If%>
}

//%	ChangeCase: Ejecuta la busqueda con un nuevo caso
//-------------------------------------------------------------------------------------------
function ChangeCase(sCode){
//-------------------------------------------------------------------------------------------
	var lstrHref = ''
	if (sCode != <%="'" & Request.QueryString("sCase") & "'"%>)
	{
		lstrHref += self.document.location.href;
		self.document.location.href = (lstrHref.search("sCase") < 0?lstrHref + "&sCase=" + sCode:lstrHref.substr(0,lstrHref.search("sCase")-1) + "&sCase=" +  sCode);
		
    }
}


//-------------------------------------------------------------------------------------------
function getCasePart(nPos){
//-------------------------------------------------------------------------------------------
	var sCode = document.getElementsByName("cbeCase")[0].value;
    
    if (typeof(nPos) != "undefined" )
		return (sCode.split("/")[nPos]);
	else
		return sCode
}


//-------------------------------------------------------------------------------------------
function insCheckSelClick(Field,lintIndex){
//-------------------------------------------------------------------------------------------
    var lstrParam=''
	var sCode = getCasePart();

	var sCN= getCasePart(0);
	var sDT= getCasePart(1);
	var sCL= getCasePart(2);
	var sId= getCasePart(3);
	
    lstrParam = "sCase="+sCode+"&nCaseNum="+sCN + "&nDemanType=" + sDT + "&sClient="+sCL +"&nCode="+marrArray[lintIndex].tcnCode +"&nId="+sId+"&nConsec="+marrArray[lintIndex].tcnConsec;

    
    if (!Field.checked)
    {
        EditRecord(lintIndex,nMainAction,"Del",lstrParam);
    }
    else
    {
        EditRecord(lintIndex,nMainAction,"Update",lstrParam);
    }
    Field.checked = !Field.checked
}

//%DisabledCase: deshabilita el campo caso en caso de existir uno solo
//-------------------------------------------------------------------------------------------
function DisabledCase(){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0])
	{
		if(cbeCase.length==1)
			cbeCase.disabled = true
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmSI015" ACTION="valClaimSeq.aspx?Time=1">
    <%Response.Write(mobjValues.ShowWindowsName("SI015", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString("Type") <> "PopUp" Then
	Call insPreSI015()
Else
	Response.Write("<SCRIPT>document.body.onload=PreparePopUp;</script>")
	If Request.QueryString("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		Call mclsDocuments.insPostSI015(mobjValues.StringToType(CStr(Session("nClaim")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nCaseNum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nDemanType"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString("sClient"), Request.QueryString("Action"), mobjValues.StringToType(Request.QueryString("nCode"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, mobjValues.StringToType(CStr(Session("nUsercode")), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nId"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull,  , mobjValues.StringToType(CStr(Session("nBranch")), eFunctions.Values.eTypeData.etdDouble), vbNullString, Request.QueryString("nConsec"))
		'UPGRADE_NOTE: Object mclsDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
		mclsDocuments = Nothing
		'+ Se hace el llamado al frame de la secuencia para que el mismo se recargue
		'+ y muestren las ventanas con/sin contenido - ACM - 26/08/2002
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Claim/ClaimSeq/Sequence.aspx?nAction=" & Request.QueryString("nMainAction") & "&sGoToNext=NO" & "';</SCRIPT>")
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "ValClaimSeq.aspx", "SI015", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
	If Request.QueryString("Action") <> "Del" Then
		Response.Write("<SCRIPT>ChangePropodate();</SCRIPT>")
	End If
End If

'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
'UPGRADE_NOTE: Object mclsDocuments may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mclsDocuments = Nothing
'UPGRADE_NOTE: Object mcolDocumentss may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mcolDocumentss = Nothing
%>
</FORM>
</BODY>
</HTML>
 
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.39
Call mobjNetFrameWork.FinishPage("si015")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




