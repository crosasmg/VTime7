<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.14
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- The object to handling the general function for the loads of values is defined.
'- Objeto para el manejo de las funciones generales de carga de valores.

Dim mobjValues As eFunctions.Values

'**- The variable mobjGrid to handling the Grid of the window is defined.
'- Se define la variable mobjGrid para el manejo del Grid de la ventana.

Dim mobjGrid As eFunctions.Grid

'**- The object to control the page zones is defined.
'- Objeto para el manejo de las zonas de la página.

Dim mobjMenu As eFunctions.Menues



'**% insDefineHeader: The Grid columns are defined.
'% insDefineHeader: Se definen las columnas del grid.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	
	'**+ The Grid columns are defined
	'+ Se definen todas las columnas del Grid
	
	Call mobjGrid.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnYear_iniColumnCaption"), "tcnYear_ini", 4, CStr(0),  , GetLocalResourceObject("tcnYear_iniColumnToolTip"), False, 0,  ,  ,  , False)
	Call mobjGrid.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnYear_endColumnCaption"), "tcnYear_end", 4, CStr(0),  , GetLocalResourceObject("tcnYear_endColumnToolTip"), False, 0,  ,  ,  , False)
	Call mobjGrid.Columns.AddNumericColumn(0, GetLocalResourceObject("tcnAmountdepColumnCaption"), "tcnAmountdep", 18, CStr(0),  , GetLocalResourceObject("tcnAmountdepColumnToolTip"), True, 6)
	
	With mobjGrid
		.Codispl = "VI7003"
		.Codisp = "VI7003"
		.Top = 100
		.Height = 205
		.Width = 360
		
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = Request.QueryString.Item("nMainAction") = "401"
		.ActionQuery = (Request.QueryString.Item("nMainAction") = "401" Or IsNothing(Request.QueryString.Item("nMainAction")))
		
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("tcnYear_ini").EditRecord = True
		.Columns("tcnYear_ini").Disabled = Request.QueryString.Item("Action") = "Update"
		.Columns("tcnYear_end").Disabled = Request.QueryString.Item("Action") = "Update"
		
		.sDelRecordParam = "nYear_ini='+ marrArray[lintIndex].tcnYear_ini + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'**% insPreVI7003: The mother window is create (Main window).
'% insPreVI7003: Se crea la ventana madre (Principal).
'------------------------------------------------------------------------------
Private Sub insPreVI7003()
	'------------------------------------------------------------------------------
	'- Objetos para el manejo de los datos repetitivos de la página
	
	Dim lcolPer_deposit As ePolicy.Per_deposits
	Dim lclsPer_deposit As Object
	
	lcolPer_deposit = New ePolicy.Per_deposits
	
	If lcolPer_deposit.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate")) Then
		For	Each lclsPer_deposit In lcolPer_deposit
			With mobjGrid
				.Columns("tcnYear_ini").DefValue = lclsPer_deposit.nYear_ini
				.Columns("tcnYear_end").DefValue = lclsPer_deposit.nYear_end
				.Columns("tcnAmountdep").DefValue = lclsPer_deposit.nAmountdep
				
				Response.Write(.DoRow)
			End With
		Next lclsPer_deposit
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolPer_deposit = Nothing
	lclsPer_deposit = Nothing
End Sub

'**% insPreVI7003Upd: Its defines this function to constructs the Pop Up window 
'**% when the action is update or delete
'% insPreVI7003Upd: Se define esta funcion para contruir la ventana Pop Up
'% Cuando la acción es actualizar o borrar
'------------------------------------------------------------------------------
Private Sub insPreVI7003Upd()
	'------------------------------------------------------------------------------
	Dim lclsPer_deposit As ePolicy.Per_deposit
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			
			lclsPer_deposit = New ePolicy.Per_deposit
			
			Call lclsPer_deposit.InsPostVA595Upd(.QueryString("Action"), Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nYear_ini"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), 0, 0, mobjValues.StringToType(Session("dNulldate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble), "VI7003")
			
			lclsPer_deposit = Nothing
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValPolicySeq.aspx", "VI7003", CStr(301), Session("bQuery"), CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI7003")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.15
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = "401"
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.15
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
%> 
<SCRIPT LANGUAGE="JavaScript">
//**+ For the Source Safe control.
//+ Para Control de Versiones. 
//------------------------------------------------------------------------------
document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $"
//------------------------------------------------------------------------------

</SCRIPT>



<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
  <HEAD>
	<META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= " & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.15
		mobjMenu.sSessionID = Session.SessionID
		mobjMenu.nUsercode = Session("nUsercode")
		'~End Body Block VisualTimer Utility
		.Write(mobjMenu.setZone(2, "VI7003", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
	End If
End With
%>
</HEAD>	  
<BODY ONUNLOAD="closeWindows();">      
 <FORM METHOD="POST"	ID="FORM" NAME="frmVI7003" ACTION="valPolicySeq.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("VI7003", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreVI7003()
Else
	Call insPreVI7003Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>	  
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.15
Call mobjNetFrameWork.FinishPage("VI7003")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




