<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------        
	mobjGrid = New eFunctions.Grid
	'+Se definen todas las columnas del Grid
	
	With mobjGrid.Columns
		Call .AddClientColumn(0, GetLocalResourceObject("tctClientColumnCaption"), "tctClient", Session("sClient"),  , GetLocalResourceObject("tctClientColumnToolTip"),  ,  , "tctCliename")
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnAgreementColumnCaption"), "tcnAgreement", 4, "", True, GetLocalResourceObject("tcnAgreementColumnToolTip"), False, 0,  ,  ,  , False)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdInit_dateColumnCaption"), "tcdInit_date",  , False, GetLocalResourceObject("tcdInit_dateColumnToolTip"),  ,  ,  , False)
		Call .AddDateColumn(0, GetLocalResourceObject("tcdEnd_DateColumnCaption"), "tcdEnd_Date",  , False, GetLocalResourceObject("tcdEnd_DateColumnToolTip"),  ,  ,  , False)
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPerc_CommColumnCaption"), "tcnPerc_Comm", 5, "", False, GetLocalResourceObject("tcnPerc_CommColumnToolTip"), False, 2,  ,  ,  , False)
	End With
	
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "MAG554_k"
		.sCodisplPage = "MAG554"
		.Top = 100
		.Height = 288
		.Width = 380
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tctClient").EditRecord = True
		.Columns("tctClient").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnAgreement").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "tctClient=' + marrArray[lintIndex].tctClient + '" & "&tcnAgreement=' + marrArray[lintIndex].tcnAgreement + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreMAG554: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreMAG554()
	'--------------------------------------------------------------------------------------------
	Dim lcolcommiss_agrees As eAgent.commiss_agrees
	Dim lclscommiss_agree As Object
	
	lcolcommiss_agrees = New eAgent.commiss_agrees
	
	With mobjGrid
		If lcolcommiss_agrees.Find() Then
			For	Each lclscommiss_agree In lcolcommiss_agrees
				.Columns("tctClient").DefValue = lclscommiss_agree.sClient
				.Columns("tcnAgreement").DefValue = lclscommiss_agree.nAgreement
				.Columns("tcdInit_date").DefValue = lclscommiss_agree.dInit_Date
				.Columns("tcdEnd_Date").DefValue = lclscommiss_agree.dEnd_date
				.Columns("tcnPerc_Comm").DefValue = lclscommiss_agree.nPerc_Comm
				Response.Write(mobjGrid.DoRow())
			Next lclscommiss_agree
		End If
	End With
	
	
	Response.Write(mobjGrid.CloseTable())
	Response.Write(mobjValues.BeginPageButton)
	lclscommiss_agree = Nothing
	lcolcommiss_agrees = Nothing
End Sub

'% insPreMAG554Upd: Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreMAG554Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclscommiss_agree As eAgent.commiss_agree
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			lclscommiss_agree = New eAgent.commiss_agree
			
			Call lclscommiss_agree.insPostMAG554(False, .QueryString.Item("Action"), Session("nUsercode"), .QueryString.Item("tctClient"), mobjValues.StringToType(.QueryString.Item("tcnAgreement"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.dtmNull, eRemoteDB.Constants.intNull)
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valMantAgent.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
			Response.Write("<SCRIPT>$(self.document.forms[0].tctClient).change();</" & "Script>")
		End If
		
	End With
	lclscommiss_agree = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG554"
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:34 $"

//% insCancel: Se invoca al cancelar una operación en la ventana
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//% insStateZone:
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------      
    return true;
}

//+ insPreZone: Controla las acciones a ejecutar sobre la ventana
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}

</SCRIPT> 
<HTML>
<HEAD>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction = " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</SCRIPT>")
End If
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenu = New eFunctions.Menues
		.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG554_k.aspx", 1, ""))
		mobjMenu = Nothing
	End If
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
<FORM METHOD="post" ID="FORM" NAME="frmMAG554" ACTION="valMantAgent.aspx?sTime=1">
<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMAG554()
Else
	Call insPreMAG554Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>






