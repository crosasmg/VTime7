<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenues As eFunctions.Menues



'%insDefineHeader(). Este procedimiento se encarga de definir las líneas del encabezado
'%del grid.
'---------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sCodisplPage = "SG020"
	
	'+Se definen todas las columnas del Grid.
	
	With mobjGrid.Columns
		Call .AddPossiblesColumn(100452, GetLocalResourceObject("valTransacColumnCaption"), "valTransac", "tabwindowsSG020_1", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  , "ChangeValues();",  , 8, GetLocalResourceObject("valTransacColumnToolTip"), eFunctions.Values.eTypeCode.eString)
		Call .AddPossiblesColumn(100452, GetLocalResourceObject("valOperationColumnCaption"), "valOperation", "TabWindowsSG020", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valOperationColumnToolTip"))
		mobjGrid.Columns("valOperation").Parameters.Add("sCodispl", Request.QueryString.Item("valTransac"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	End With
	
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SG020"
		.Width = 500
		.Height = 200
		
		'+ Si la acción que viaja a través del QueryString es Consulta (401), Elimiación (303) o el
		'+ parámetro nMainAction tiene valor NULO (vbNUllString o ""), la propiedad ActionQuery se setea en TRUE,
		'+ de lo contrario se setea en FALSE
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		Else
			.Columns("Sel").GridVisible = True
			.ActionQuery = False
		End If
		.sDelRecordParam = "nTransac=' + marrArray[lintIndex].valTransac + '" & "&nOperation=' + marrArray[lintIndex].valOperation + '"
	End With
End Sub

'%insPreSG020: Esta ventana se encarga de mostrar en el grid los valores leídos.
'---------------------------------------------------------------------------------------
Private Sub insPreSG020()
	'---------------------------------------------------------------------------------------
	Dim lclsSecur_sche As Object
	Dim lcolSecur_sches As eSecurity.Secur_sches
	Dim llngIndex As Byte
	
	lcolSecur_sches = New eSecurity.Secur_sches
	
	If lcolSecur_sches.FindSche_Transac(Session("sSche_codeWin"), True) Then
		llngIndex = 0
		
		For	Each lclsSecur_sche In lcolSecur_sches
			With mobjGrid
				.Columns("valTransac").DefValue = lclsSecur_sche.sCodispl
				.Columns("valOperation").Parameters.Add("sCodispl", lclsSecur_sche.sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Columns("valOperation").DefValue = lclsSecur_sche.nTransac
				.Columns("valOperation").Descript = lclsSecur_sche.sDesc_tx
				Response.Write(mobjGrid.DoRow())
			End With
		Next lclsSecur_sche
	End If
	
	lclsSecur_sche = Nothing
	lcolSecur_sches = Nothing
	
	Response.Write(mobjGrid.CloseTable())
End Sub

'%insPreSG002Upd: Permite realizar el llamado a la ventana PopUp.
'-----------------------------------------------------------------------------------------
Private Sub insPreSG020Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsSecur_sche As eSecurity.Secur_sche
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		
		lclsSecur_sche = New eSecurity.Secur_sche
		
		Call lclsSecur_sche.insPostSG020("Delete", Session("sSche_codeWin"), Request.QueryString.Item("nTransac"), CInt(Request.QueryString.Item("nOperation")), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		
		Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location=""/VTimeNet/Security/Security/Sequence.aspx?nAction=0" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</" & "Script>")
	End If
	
	lclsSecur_sche = Nothing
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValSecuritySeqSchema.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
	
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG020"
%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>




	
<%
mobjMenues = New eFunctions.Menues

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "SG020", "SG020.aspx"))
End If

With Response
	.Write(mobjValues.WindowsTitle("SG020"))
	.Write(mobjValues.StyleSheet())
End With
%>
    <%="<SCRIPT>nMainAction='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>"%>
    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<SCRIPT>
//% ChangeValues: Refresca los Valores de los parametros de los valores Posibles
//--------------------------------------------------------------------------------------------
function ChangeValues(){
//-------------------------------------------------------------------------------------------*/
    with(self.document.forms[0]){
    	valOperation.Parameters.Param1.sValue=valTransac.value;
		valOperation.value="";
		UpdateDiv("valOperationDesc","");		
		if (valTransac.value != ''){
				valOperation.disabled = false;
				btnvalOperation.disabled = false;
			 }
			else {
				valOperation.disabled = true;
				btnvalOperation.disabled = true;
			}
		}
}
</SCRIPT>
<FORM METHOD="post" ID="FORM" NAME="SG020" ACTION="ValSecuritySeqSchema.aspx?Time=1&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">

   <%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjValues.ShowWindowsName("SG020"))
	Call insPreSG020()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	Call insPreSG020Upd()
End If

%>
   
</FORM>
</BODY>
</HTML>








