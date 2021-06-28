<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid


'%insDefineHeader. Definición de columnas del GRID
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "ms5577_k"
	
	'+ Se definen las columns del Grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeAgencyColumnCaption"), "cbeAgency", "Table5555", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  , "insChangeAgency(this.value);", Request.QueryString.Item("Action") = "Update",  , GetLocalResourceObject("cbeAgencyColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOfficeAgenColumnCaption"), "cbeOfficeAgen", "Table5556", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeOfficeAgenColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeBran_OffColumnCaption"), "cbeBran_Off", "Table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeBran_OffColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkPayColumnCaption"), "chkPay", vbNullString, 2,  ,  , Request.QueryString.Item("Type") <> "PopUp")
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	'+ Se asignan las caracteristicas del Grid
	
	With mobjGrid
		.Codispl = "MS5577"
		.Width = 420
		.Height = 235
		.Columns("cbeAgency").EditRecord = True
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	
	
End Sub

'%insPreMS5577_K: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------
Private Sub insPreMS5577_K()
	'--------------------------------------------------------------------------------
	Dim lcolagencies As eAgent.Agencies
	Dim lclsAgencie As eAgent.Agencie
	Dim lblnFind As Object
	lcolagencies = New eAgent.Agencies
	lclsAgencie = New eAgent.Agencie
	
	If lcolagencies.Find() Then
		
		For	Each lclsAgencie In lcolagencies
			With mobjGrid
				.Columns("cbeAgency").DefValue = CStr(lclsAgencie.nAgency)
				.Columns("cbeOfficeAgen").DefValue = CStr(lclsAgencie.nOfficeAgen)
				.Columns("cbeBran_Off").DefValue = CStr(lclsAgencie.nbran_off)
				.Columns("chkPay").Checked = CShort(lclsAgencie.sPay)
				'+ Se "arma" un QueryString en la columna oculta sParam. 
				
				.Columns("sParam").DefValue = "nAgency=" & lclsAgencie.nAgency & "&nOfficeAgen=" & lclsAgencie.nOfficeAgen & "&nBran_Off=" & lclsAgencie.nbran_off
				
			End With
			Response.Write(mobjGrid.DoRow())
		Next lclsAgencie
		
	End If
	Response.Write(mobjGrid.closeTable())
	
	lcolagencies = Nothing
	lclsAgencie = Nothing
	
End Sub

'% insPreMS5577Upd. Se define esta función para contruir el contenido de la ventana UPD
'------------------------------------------------------------------------------------
Private Sub insPreMS5577_K_Upd()
	'------------------------------------------------------------------------------------
	Dim lclsAgencie As eAgent.Agencie
	
	lclsAgencie = New eAgent.Agencie
	
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		Call lclsAgencie.insPostMS5577(Request.QueryString.Item("nAgency"), Request.QueryString.Item("nOfficeAgen"), Session("nUsercode"), Request.QueryString.Item("nBran_Off"), "Del", Request.QueryString("Action"))
		lclsAgencie = Nothing
	End If
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valMantSys.aspx", "MS5577", Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "ms5577_k"
%>
<HTML>
<HEAD>
   <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
  	




	 
<%
Response.Write(mobjValues.WindowsTitle("MS5577"))
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<SCRIPT>var nMainAction=0</SCRIPT>")
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>" & vbCrLf)
End If
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		.Write(mobjMenu.MakeMenu("MS5577", "MS5577_k.aspx", 1, ""))
		mobjMenu = Nothing
	Else
		.Write("<SCRIPT>var sAction='" & Request.QueryString.Item("Action") & "'</SCRIPT>")
	End If
End With
%>
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:29 $"

//+ Controla el estado de los campos de la página
//-------------------------------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------------------------------
}

//-------------------------------------------------------------------------------------------------------------------
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

//+ Funcion que cancela las las acciones de la Pagina
//-------------------------------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------------------------------
	return(true)
}

//+ Funcion que finaliza las las acciones de la Pagina
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

//+ Se ejecuta al cambiar el valor de la agencia para habilitar o desabilitar los demás campos 
//+ de la popup
//------------------------------------------------------------------------------------------
function insChangeAgency(value){
//------------------------------------------------------------------------------------------
	if (sAction != 'Update') 
		with (self.document.forms[0]){	
			if (value != 0){			
				cbeOfficeAgen.disabled = false;						
				cbeBran_Off.disabled = false;
				}
			else{			
				cbeOfficeAgen.disabled = true;
				cbeBran_Off.disabled = true;
				}
			cbeOfficeAgen.value = 0;
			cbeBran_Off.value = 0;			
			}	
}
</SCRIPT>		
</HEAD>
<BODY ONUNLOAD="closeWindows();" >
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
%>
<FORM METHOD="POST" ID="FORM" NAME="frmAgencies" ACTION="valmantsys.aspx?mode=1">
 <%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Response.Write(mobjValues.ShowWindowsName("MS5577"))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreMS5577_K()
Else
	Call insPreMS5577_K_Upd()
End If
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>




