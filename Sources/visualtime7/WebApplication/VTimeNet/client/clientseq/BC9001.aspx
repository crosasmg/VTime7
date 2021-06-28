<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de los datos del cliente	
Dim mobjclient_UsersWeb As eClient.UsersWeb

'-Variables para manejar el option de fumador
Dim loptNoInfo As Object '3
Dim loptSmoker As Object '1
Dim loptNoSmoker As Object '2

'% insDefineHeader:	se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsUsersWebAccess As eClient.UsersWebAccess
	Dim lblnDisabled As Boolean
	
	
	lclsUsersWebAccess = New eClient.UsersWebAccess
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "BC9001"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	'+Se definen todas las columnas del Grid
	With mobjGrid.Columns
		Call .AddBranchColumn(0, GetLocalResourceObject("cbeBranchColumnCaption"), "cbeBranch", GetLocalResourceObject("cbeBranchColumnToolTip"), "valProduct")
		Call .AddProductColumn(0, GetLocalResourceObject("valProductColumnCaption"), "valProduct", GetLocalResourceObject("valProductColumnToolTip"))
		Call .AddHiddenColumn("sParam", vbNullString)
	End With
	
	
	'Call lclsUsersWebAccess.Find(Session("sClient"))
	
	lblnDisabled = False
	
	'+ Se definen las columnas del grid
	With mobjGrid
		.Codispl = "BC9001"
		.Codisp = "BC9001"
		.sCodisplPage = "BC9001"
		.Top = 200
		.Left = 150
		.Height = 200
		.Width = 400
		.sEditRecordParam = "sInitials=' + document.forms[0].tctInitials.value        + '" & "&sPassword=' + document.forms[0].tctPassword.value + '" & "&sStatus=' + document.forms[0].cbeStatus.value          + '" & "&nRol=' + document.forms[0].cbeRol.value       + '"
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		'.Columns("cbeBranch").EditRecord = True		
		.Columns("cbeBranch").Disabled = Request.QueryString.Item("Action") = "Update"
		.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdLong)
	End With
	
	
	'+ Se definen las propiedades generales	del	grid
	
	lclsUsersWebAccess = Nothing
	
End Sub
'%insPreAG553: Esta función se encarga de cargar los datos en la forma "Folder" 
'--------------------------------------------------------------------------------------------
Private Sub insPreBC9001()
	'--------------------------------------------------------------------------------------------
	
	Dim lcolUsersWebAccess As eClient.UsersWebAccesss
	Dim lclsUsersWebAccess As eClient.UsersWebAccess
	Dim llngIntermedia As Object
	Dim lblsClientia As Object
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL>" & GetLocalResourceObject("tctInitialsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD> ")


Response.Write(mobjValues.TextControl("tctInitials", 12, mobjclient_UsersWeb.sUser,  , GetLocalResourceObject("tctInitialsToolTip"),  ,  ,  ,  ,  , 5))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL>" & GetLocalResourceObject("tctPasswordCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD> ")


Response.Write(mobjValues.PasswordControl("tctPassword", 15, mobjclient_UsersWeb.sPassword,  , GetLocalResourceObject("tctPasswordToolTip"),  ,  ,  ,  ,  , 6))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL>" & GetLocalResourceObject("cbeStatusCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD> ")


Response.Write(mobjValues.PossiblesValues("cbeStatus", "table26", eFunctions.Values.eValuesType.clngComboType, mobjclient_UsersWeb.sStatregt,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeStatusToolTip")))


Response.Write(" </TD" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("	        <TD><LABEL>" & GetLocalResourceObject("cbeRolCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD> ")


Response.Write(mobjValues.PossiblesValues("cbeRol", "table9032", eFunctions.Values.eValuesType.clngComboType, CStr(mobjclient_UsersWeb.nRol),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeRolToolTip")))


Response.Write(" </TD" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<P ALIGN=""Center"">")


Response.Write(mobjValues.BeginPageButton)


Response.Write("</P>" & vbCrLf)
Response.Write("")

	
	lcolUsersWebAccess = New eClient.UsersWebAccesss
	lclsUsersWebAccess = New eClient.UsersWebAccess
	With mobjGrid
		If lcolUsersWebAccess.Find(Session("sClient")) Then
			
			For	Each lclsUsersWebAccess In lcolUsersWebAccess
				.Columns("cbeBranch").DefValue = CStr(lclsUsersWebAccess.nBranch)
				.Columns("valProduct").DefValue = CStr(lclsUsersWebAccess.nProduct)
				
				'+ Se "Construye" un QueryString en la columna oculta sParam. Estos valores serán pasados a la 
				'+ función insPostAG553 cuando se eliminen los registros seleccionados - VCVG - 11/12/2001
				.Columns("sParam").DefValue = "sClient=" & Session("sClient") & "&nBranch=" & lclsUsersWebAccess.nBranch & "&nProduct=" & lclsUsersWebAccess.nProduct & "&nUsercode=" & Session("nUsercode")
				.sEditRecordParam = "sInitials=' + document.forms[0].tctInitials.value        + '" & "&sPassword=' + document.forms[0].tctPassword.value + '" & "&sStatus=' + document.forms[0].cbeStatus.value          + '" & "&nRol='+ document.forms[0].cbeRol.value       + '"
				Response.Write(mobjGrid.DoRow())
			Next lclsUsersWebAccess
		End If
	End With
	Response.Write(mobjGrid.CloseTable())
	lclsUsersWebAccess = Nothing
	lcolUsersWebAccess = Nothing
	mobjGrid = Nothing
	Response.Write(mobjValues.BeginPageButton)
End Sub
'% insPreBC9001Upd. Se define esta función para construir el contenido de la ventana UPD de los Ramos y Productos permitidos
'----------------------------------------------------------------------------------------------------------------------------
Private Sub insPreBC9001Upd()
	'----------------------------------------------------------------------------------------------------------------------------
	Dim lclsUsersWebAccess As eClient.UsersWeb
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			lclsUsersWebAccess = New eClient.UsersWeb
			Response.Write(mobjValues.ConfirmDelete())
			Call lclsUsersWebAccess.PostBC9001_upd(3, .QueryString.Item("sClient"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			lclsUsersWebAccess = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valClientSeq.aspx", "BC9001", .QueryString.Item("nMainAction"),  , CShort(.QueryString.Item("Index"))))
		
		If .QueryString.Item("Action") <> "Del" Then
Response.Write("		" & vbCrLf)
Response.Write("		<SCRIPT>		" & vbCrLf)
Response.Write("			self.document.forms[0].valProduct.Parameters.Param1.sValue=self.document.forms[0].cbeBranch.value;" & vbCrLf)
Response.Write("		</" & "SCRIPT>")

			
		End If
		
	End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjclient_UsersWeb = New eClient.UsersWeb

If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

With mobjclient_UsersWeb
	Call .Find(Session("sClient"))
	If Request.QueryString.Item("sInitials") <> vbNullString Then
		.sUser = Request.QueryString.Item("sInitials")
	End If
	If Request.QueryString.Item("sPassword") <> vbNullString Then
		.sPassword = Request.QueryString.Item("sPassword")
	End If
	If Request.QueryString.Item("nROl") <> vbNullString Then
		.nRol = CShort(Request.QueryString.Item("nRol"))
	End If
	If Request.QueryString.Item("sStatus") <> vbNullString Then
		.sStatregt = Request.QueryString.Item("sStatus")
	End If
End With
%>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//+ Variable para el control de versiones
		document.VssVersion="$$Revision: 1 $|$$Date: 15/01/10 6:53 $"
		
//% CancelErrors: se controla la acción Cancelar 
//---------------------------------------------------------------------------------------------------
	function CancelErrors(){
//---------------------------------------------------------------------------------------------------
	self.history.back
}
</SCRIPT>

    <%
With Response
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "BC9001", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("BC9001", Request.QueryString.Item("sWindowDescript")))
	mobjMenu = Nothing
End With%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmBC9001" ACTION="valClientSeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
    <A NAME="BeginPage"></A>
    <%'=mobjValues.ShowWindowsName(Request.QueryString("sCodispl"))

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreBC9001()
Else
	Call insPreBC9001Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing


%>
<SCRIPT>
//%	insStateBC9001: recarga la página para mostrar la información del grid
//-------------------------------------------------------------------------------------------
function insStateBC9001(lblnEnabled){
//-------------------------------------------------------------------------------------------
    var lintIndex=0;
    lblnEnabled = !lblnEnabled
    with (document.forms[0])
    {
        for (lintIndex=0;lintIndex<document.forms[0].elements.length;lintIndex++)
        {
            elements[lintIndex].disabled = lblnEnabled
        }
    }
    with (self.document)
    {
        //images['btnvalClient'].disabled = lblnEnabled        
    }
}
</SCRIPT>

	
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjclient_UsersWeb = Nothing
%>




