<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSaapv" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	Dim lobjColumn As eFunctions.Column
	mobjGrid.sCodisplPage = "VI7502"
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "VI7502"
	Call mobjGrid.SetWindowParameters(Request.QueryString("sCodispl"), Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		.AddNumericColumn(0, "Folio", "tcnCod_saapv", 10,  ,  , "Número de Folio",  ,  ,  ,  ,  , True)
		.AddPossiblesColumn(0, "Institución", "valInstitution", "TabTab_Fn_Institu", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , True,  , "Institución que genera el SAAPV")
		.AddPossiblesColumn(0, "Tipo de SAAPV", "cbeType_saapv", "table5742", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , True,  , "Tipo de saapv")
		.AddPossiblesColumn(0, "Estado actual", "cbestatus_now", "table5741", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  , True,  , "Estado actual del movimiento")
		
		If Request.QueryString("Type") = "PopUp" Then
			lobjColumn = .AddPossiblesColumn(0, "Nuevo Estado", "cbestatus_saapv", "TABTAB_STATE_SAAPV", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , False, 10)
			
		Else
			.AddAnimatedColumn(0, "Modificar SAAPV", "btnDetail0", "..\..\images\menu_query.png", "Muestra la SAAPV'")
			.AddAnimatedColumn(0, "MODP Modificar Poliza", "btnDetail", "..\..\images\menu_query.png", "Muestra la transacción 'Tratamiento de pólizas'")
			.AddAnimatedColumn(0, "TEGR Traspasos de egresos y Retiros", "btnDetail1", "..\..\images\menu_query.png", "Muestra la transacción 'traspasos'")
			lobjColumn = .AddHiddenColumn("cbestatus_saapv", vbNullString)
		End If
		
		.AddNumericColumn(0, "Nº Relación", "tcnBordereaux", 10,  ,  , "Número de relación",  ,  ,  ,  ,  , True)
		.AddTextColumn(0, "Estado Relac.", "tctStatus", 30, "",  , "Estado de la relación",  ,  ,  , True)
		.AddNumericColumn(0, "Monto Relac.", "tcnAmount_rel", 18, CStr(0),  , "Monto de la relación", True, 6,  ,  ,  , True)
		.AddCheckColumn(0, "Autorizac. Tr. con dif.", "chkAutodif", vbNullString,  ,  ,  , Request.QueryString("Type") <> "PopUp" Or Session("bQuery"), "Opción para autorizar imputación con diferencia")
		.AddButtonColumn(0, "Notas", "SCA07502", mobjValues.StringToType(Request.QueryString("nNoteNum"), eFunctions.Values.eTypeData.etdLong), True, Request.QueryString("Type") <> "PopUp" Or Session("bQuery"),  ,  ,  ,  , "btnNotenum")
		
		lobjColumn.Parameters.Add("ntype_saapv", Request.QueryString("nType_saapv"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		lobjColumn.Parameters.Add("ntype_state_origi", Request.QueryString("nstatus_saapv"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		
		.AddHiddenColumn("hdsCertype", vbNullString)
		.AddHiddenColumn("hdnBranch", vbNullString)
		.AddHiddenColumn("hdnProduct", vbNullString)
		.AddHiddenColumn("hdnPolicy", vbNullString)
		.AddHiddenColumn("hdnCertif", vbNullString)
		
		.AddHiddenColumn("hdsCertype_cond", Request.QueryString("sCertype"))
		.AddHiddenColumn("hdnBranch_cond", Request.QueryString("nBranch"))
		.AddHiddenColumn("hdnProduct_cond", Request.QueryString("nProduct"))
		.AddHiddenColumn("hdnPolicy_cond", Request.QueryString("nPolicy"))
		.AddHiddenColumn("hdClient_cond", Request.QueryString("sClient"))
		.AddHiddenColumn("hdCod_saapv_cond", Request.QueryString("nCod_saapv"))
		.AddHiddenColumn("hdInstitution_cond", Request.QueryString("nInstitution"))
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "VI7502"
		.Height = 400
		.Width = 400
		.nMainAction = Request.QueryString("nMainAction")
		.Columns("tcnCod_saapv").EditRecord = True
		.Columns("Sel").Gridvisible = False
		.AddButton = False
		.DeleteButton = False
		.bCheckVisible = False
'		.bButtonBackNext = False
		.ActionQuery = False
	End With
End Sub

'% insPreVI7502: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7502()
	'--------------------------------------------------------------------------------------------
	Dim lclsSaapv As Object
	Dim lcolSaapv As eSaapv.Saapv_pols
	Dim lintCount As Short
	lcolSaapv = New eSaapv.Saapv_pols
	
	If lcolSaapv.Find_VI7502(Request.QueryString("sCertype"), mobjValues.StringToType(Request.QueryString("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, Request.QueryString("sClient"), mobjValues.StringToType(Request.QueryString("nCod_saapv"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString("nInstitution"), eFunctions.Values.eTypeData.etdDouble)) Then
		lintCount = 0
		For	Each lclsSaapv In lcolSaapv
			With mobjGrid
				
				.Columns("hdsCertype").DefValue = lclsSaapv.sCertype
				.Columns("hdnBranch").DefValue = lclsSaapv.nBranch
				.Columns("hdnProduct").DefValue = lclsSaapv.nProduct
				.Columns("hdnPolicy").DefValue = lclsSaapv.nPolicy
				.Columns("hdnCertif").DefValue = lclsSaapv.nCertif
				.Columns("tcnCod_saapv").DefValue = lclsSaapv.nCod_saapv
				.Columns("valInstitution").DefValue = lclsSaapv.nInstitution
				.Columns("cbeType_saapv").DefValue = lclsSaapv.nType_saapv
				.Columns("cbestatus_now").DefValue = lclsSaapv.nstatus_saapv
				
				.Columns("tcnBordereaux").DefValue = lclsSaapv.nBordereaux
				.Columns("tctStatus").DefValue = lclsSaapv.sStatus
				.Columns("tcnAmount_rel").DefValue = lclsSaapv.nAmount_rel
				.Columns("btnNotenum").nNotenum = lclsSaapv.nNotenum
				.Columns("btnNotenum").nIndexNotenum = lintCount
				
				If lclsSaapv.sAutodif = "1" Then
					.Columns("chkAutodif").checked = CShort("1")
					.Columns("chkAutodif").DefValue = "1"
				Else
					.Columns("chkAutodif").checked = CShort("2")
					.Columns("chkAutodif").DefValue = "2"
				End If
				
				.sEditRecordParam = "nType_saapv=" & lclsSaapv.nType_saapv & "&nstatus_saapv=" & lclsSaapv.nstatus_saapv & "&nNoteNum=" & lclsSaapv.nNotenum
				
                    '				.Columns("cbestatus_saapv").Parameters("ntype_saapv").let_Value(lclsSaapv.nType_saapv)
                    '				.Columns("cbestatus_saapv").Parameters("ntype_state_origi").let_Value(lclsSaapv.nstatus_saapv)
                    .Columns("cbestatus_saapv").Parameters.Add("ntype_saapv", lclsSaapv.nType_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Columns("cbestatus_saapv").Parameters.Add("ntype_state_origi", lclsSaapv.nstatus_saapv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				
				
				
				If Request.QueryString("Type") <> "PopUp" Then
					If lclsSaapv.scheckamend = "2" And lclsSaapv.scheckrequest = "2" Then
						.Columns("btnDetail0").src = "..\..\images\btnWNotes.png"
						.Columns("btnDetail0").disabled = False
						.Columns("btnDetail0").HRefScript = "ShowSubSequence('" & lclsSaapv.sCertype & "','" & lclsSaapv.nBranch & "','" & lclsSaapv.nProduct & "','" & lclsSaapv.nPolicy & "','" & lclsSaapv.nCertif & "','" & lclsSaapv.nCod_saapv & "','" & lclsSaapv.nstatus_saapv & "','" & lclsSaapv.nType_saapv & "','" & lclsSaapv.dissue_dat & "','" & lclsSaapv.nInstitution & "','" & lclsSaapv.ntype_ameapv & "','" & lclsSaapv.dlimitdate & "');" '
					Else
						.Columns("btnDetail0").src = "..\..\images\DeniedTr.png"
						.Columns("btnDetail0").disabled = True
					End If
					
					If lclsSaapv.scheckamend = "1" Then
						.Columns("btnDetail").src = "..\..\images\btnWNotes.png"
						.Columns("btnDetail").disabled = False
						.Columns("btnDetail").HRefScript = "InsShowCA001('" & lclsSaapv.sCertype & "','" & lclsSaapv.nBranch & "','" & lclsSaapv.nProduct & "','" & lclsSaapv.nPolicy & "','" & lclsSaapv.nCertif & "','" & lclsSaapv.nCod_saapv & "','" & lclsSaapv.nInstitution & "');" '			        
					Else
						.Columns("btnDetail").src = "..\..\images\DeniedTr.png"
						.Columns("btnDetail").disabled = True
						.Columns("btnDetail").HRefScript = "alert('El estado de la saapv no permite realizar endoso');"
					End If
					
					If lclsSaapv.scheckrequest = "1" Then
						.Columns("btnDetail1").src = "..\..\images\btnWNotes.png"
						.Columns("btnDetail1").disabled = False
						'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
						.Columns("btnDetail1").HRefScript = "InsShowVI7004('" & lclsSaapv.sCertype & "','" & lclsSaapv.nBranch & "','" & lclsSaapv.nProduct & "','" & lclsSaapv.nPolicy & "','" & lclsSaapv.nCertif & "','" & lclsSaapv.nCod_saapv & "','" & Today & "','" & lclsSaapv.nType_saapv & "');"
					Else
						.Columns("btnDetail1").src = "..\..\images\DeniedTr.png"
						.Columns("btnDetail1").disabled = True
						.Columns("btnDetail1").HRefScript = "alert('El estado de la saapv no permite realizar traspaso');"
					End If
				End If
				Response.Write(.DoRow)
			End With
			lintCount = lintCount + 1
		Next lclsSaapv
	End If
	Response.Write(mobjGrid.closeTable())
	'UPGRADE_NOTE: Object lcolSaapv may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolSaapv = Nothing
End Sub

'%** insPreVI7501_GUpd: Show the pop up windows for the updates.
'% insPreVI7501_GUpd: Muestra la ventana Popup para las actualizaciones.
'--------------------------------------------------------------------------------------------
Private Sub insPreVI7502Upd()
	'--------------------------------------------------------------------------------------------
	
	
	With Request
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString("Action"), "valpolicytra.aspx", Request.QueryString("sCodispl"), .QueryString("nMainAction"), False, .QueryString("Index")))
		
		Response.Write("<SCRIPT>self.document.forms[0].tcnNotenum.value = top.opener.marrArray[CurrentIndex].btnNotenum;nCopyNotenum=top.opener.marrArray[CurrentIndex].btnNotenum;</" & "Script>")
	End With
	
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7502")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "VI7502"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = True
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'VTime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 6 $|$$Date: 15/10/03 16:37 $|$$Author: Nvaplat61 $"
    
//% InsShowCA001: Llama a las ventanas de pago de siniestro y/o cualquiera que sea el caso

function ShowSubSequence(sCertype, nBranch , nProduct , nPolicy , nCertif, nCod_saapv, nStatus ,nType_saapv, dEffecdate, nInstitution,ntype_ameapv,dlimitdate){
//--------------------------------------------------------------------------------------------
   var lstrstring = '';
	lstrstring = "&Policy=2" + "&sCertype=" + sCertype + 
	             "&sCodisplOrig=VI7502" + 
				"&bMenu=1" +
	            "&nBranch=" + nBranch +
	            "&nProduct=" + nProduct +
	            "&nPolicy=" + nPolicy +
	            "&nCertif=" + nCertif +
	            "&nCod_saapv=" + nCod_saapv +
	            "&nInstitution=" + nInstitution +
	            "&nStatus=" + nStatus +
	            "&nType_saapv=" + nType_saapv +
	            "&dEffecdate=" + dEffecdate + 
	            "&ntype_ameapv=" + ntype_ameapv +
	            "&dlimitdate=" + dlimitdate +
                "&nMainAction=302";

    ShowPopUp('/VTimeNet/Common/secWHeader.aspx?sModule=Policy&sProject=Policytra&sCodispl=VI7501_K&nHeight=270'+ lstrstring , 'Policytra', 8500, 7000, 'yes','yes', 0, 0,'yes')  

}
//-------------------------------------------------------------------------------------------
function InsShowCA001(sCertype, nBranch , nProduct , nPolicy , nCertif, nCod_saapv, nInstitution){
//-------------------------------------------------------------------------------------------
	var lstrQueryString;
    var LoadWithAction;
    var nTransaction;

	LoadWithAction = '26';
	nTransaction = '26';

	lstrQueryString = "&sCertype=" + sCertype + 
	                  "&sCodisplOrig=VI7502" + 
					  "&bMenu=1" +
	                  "&nBranch=" + nBranch +
	                  "&nProduct=" + nProduct +
	                  "&nPolicy=" + nPolicy +
	                  "&nCertif=" + nCertif +
	                  "&nCod_saapv=" + nCod_saapv +
	                  "&nInstitution=" + nInstitution +
	                  "&LoadWithAction=" + 401 + 
	                  "&nTransaction=" + nTransaction;

	ShowPopUp("/VTimeNet/common/GoTo.aspx?sCodispl=CA001" + lstrQueryString,"VI7502_CA001",8500,7000,true,false,0,0);
}

//-------------------------------------------------------------------------------------------
function InsShowVI7004(sCertype, nBranch , nProduct , nPolicy , nCertif, nCod_saapv, dEffecdate, nType_saapv){
//-------------------------------------------------------------------------------------------
	var lstrQueryString;
    var LoadWithAction;
    var nTransaction;
	var nSurrReas;

	LoadWithAction = '26';
	nTransaction = '26';

	if (nType_saapv == '3')
		nSurrReas = '1'
	else
		if (nType_saapv == '4')
			nSurrReas = '2'
		else
			nSurrReas = ''
	
	lstrQueryString = "&sCertype=" + sCertype + 
	                  "&sCodisplOrig=VI7502" + 
					  "&sTyp_surr=2" +
	                  "&nBranch=" + nBranch +
	                  "&nProduct=" + nProduct +
	                  "&nProponum=" + nPolicy +
	                  "&nCertif=" + nCertif +
	                  "&nCod_saapv=" + nCod_saapv +
	                  "&dEffecdate=" + dEffecdate +
	                  "&LoadWithAction=" + 401 + 
	                  "&nTransaction=" + nTransaction +
					  "&nSurrReas=" + nSurrReas;

	ShowPopUp("/VTimeNet/common/GoTo.aspx?sCodispl=VI7004" + lstrQueryString,"VI7502_CA001",8500,7000,true,false,0,0);
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI7502", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy")))
	'UPGRADE_NOTE: Object mobjMenu may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="VI7502" ACTION="valPolicytra.aspx?sMode=2">
<%
Response.Write(mobjValues.ShowWindowsName("VI7502", Request.QueryString("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString("Type") <> "PopUp" Then
	Call insPreVI7502()
Else
	Call insPreVI7502Upd()
End If


'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjGrid = Nothing
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("VI7502")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




