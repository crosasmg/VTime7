<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid


'% insDefineHeader: Define las columnas del Grid
'-------------------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	'+ Se definen las columnas del grid  
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyColumnToolTip"))
		Call .AddPossiblesColumn(0, GetLocalResourceObject("ValusersColumnCaption"), "Valusers", "tabusers", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("ValusersColumnToolTip"))
		Call .AddHiddenColumn("hddnUsercode_old", CStr(0))
	End With
	
	'+ Se definen las propiedades generales del grid.
	With mobjGrid
		.Codispl = "SG852"
		.sCodisplPage = "SG852"
		.AddButton = True
		.DeleteButton = True
		.Top = 70
		.Width = 600
		.Height = 230
		.WidthDelete = 650
		
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.ActionQuery = True
		End If
		
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnPolicy").EditRecord = True
		.sDelRecordParam = "nPolicy='+ marrArray[lintIndex].tcnPolicy + '" & "&nUsercode='+ marrArray[lintIndex].Valusers + '"
	End With
End Sub

'% insPreSG852: Carga los datos en el grid de la forma "Folder" 
'---------------------------------------------------------------
Private Sub insPreSG852()
	'---------------------------------------------------------------
	Dim lcolPolicy_securitys As eSecurity.policy_securitys
	Dim lclsPolicy_security As Object
	
	lcolPolicy_securitys = New eSecurity.policy_securitys
	
	If lcolPolicy_securitys.Find Then
		For	Each lclsPolicy_security In lcolPolicy_securitys
			With mobjGrid
				.Columns("tcnPolicy").DefValue = lclsPolicy_security.nPolicy
				.Columns("ValUsers").DefValue = lclsPolicy_security.nUsercode
				.Columns("hddnUsercode_old").DefValue = lclsPolicy_security.nUsercode
				Response.Write(.DoRow)
			End With
		Next lclsPolicy_security
		
		'+ Se llama a la propiedad CloseTable, para dar por finalizada la creación de la tabla (Grid)
		Response.Write(mobjGrid.CloseTable())
		Response.Write(mobjValues.BeginPageButton)
	Else
		Response.Write(mobjGrid.CloseTable())
		Response.Write(mobjValues.BeginPageButton)
	End If
	lclsPolicy_security = Nothing
	lcolPolicy_securitys = Nothing
End Sub

'% insPreSG852Upd: Gestiona lo relacionado a la actualización de un registro del Grid
'------------------------------------------------------------------------------------
Private Sub insPreSG852Upd()
	'------------------------------------------------------------------------------------
	Dim lclsPolicy_security As eSecurity.Policy_security
	lclsPolicy_security = New eSecurity.Policy_security
	
	With Request
		If .QueryString.Item("Action") = "Update" Then
			mobjGrid.Columns("tcnPolicy").Disabled = True
		End If
		
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsPolicy_security.InsPostSG852("Del", mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdLong), eRemoteDB.Constants.intNull)
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valsecurity.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
	End With
	
	lclsPolicy_security = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG852"
%>

<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 17/04/04 19:42 $|$$Author: Nvaplat37 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%If Request.QueryString.Item("Type") <> "PopUp" Then%>
		<%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\security\security\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>
	<%End If%>
	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>





    <%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenu = New eFunctions.Menues
	With Response
		.Write(mobjMenu.MakeMenu("SG852", "SG852_K.aspx", 1, ""))
		.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")
	End With
	mobjMenu = Nothing
End If
%>

<SCRIPT>
//% insStateZone: 
//-----------------------
function insStateZone(){}
//-----------------------

//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//------------------------------------------------------------------------------
function insPreZone(llngAction){
//------------------------------------------------------------------------------
	switch (llngAction){
	    case 301:
	    case 302:
	    case 401:
			document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction
	        break;
	}
}
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
%>
	<FORM METHOD="post" ID="FORM" NAME="SG852_K" ACTION="valsecurity.aspx?mode=1">
<%
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreSG852()
Else
	Call insPreSG852Upd()
End If

mobjGrid = Nothing
mobjValues = Nothing
%>     
	</FORM>
</BODY>
</HTML>





