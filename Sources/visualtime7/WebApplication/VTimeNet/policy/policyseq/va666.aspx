<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**+ ----------------------------------------------------------------------------------------
'**+ Ventana Masiva.  Comentario General
'**+ Borrar todos los comentarios que comiencen con: '**+
'**+ Sustituir "Codispl" por el código lógico de la transacción
'**+ ----------------------------------------------------------------------------------------
'**+ Última modificación: 23/10/2001
'**+ ----------------------------------------------------------------------------------------

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
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		'**+ Modificar los parámetros "Title" y "FieldName" de cada columna
		Call .AddDateColumn(0, GetLocalResourceObject("tcdFieldColumnCaption"), "tcdField", vbNullString,  , GetLocalResourceObject("tcdFieldColumnToolTip"))
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnFieldColumnCaption"), "tcnField", 10, vbNullString)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("[cbe][val]FieldColumnCaption"), "[cbe][val]Field", "Table10", eFunctions.Values.eValuesType.clngComboType, CStr(0))
		Call .AddAnimatedColumn(0, GetLocalResourceObject("btnFieldColumnCaption"), "btnField", "/VTimeNet/Images/Time.gif", GetLocalResourceObject("btnFieldColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkFieldColumnCaption"), "chkField", vbNullString)
		Call .AddClientColumn(0, GetLocalResourceObject("dtcFieldColumnCaption"), "dtcField", vbNullString,  , GetLocalResourceObject("dtcFieldColumnToolTip"))
		Call .AddTextAreaColumn(0, GetLocalResourceObject("tctFieldColumnCaption"), "tctField", vbNullString, 50, 50)
		Call .AddTextColumn(0, GetLocalResourceObject("tctFieldColumnCaption"), "tctField", 10, vbNullString)
		Call .AddHiddenColumn("hddField", vbNullString)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "Codispl"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("CampoX").EditRecord = True
		.Height = 350
		.Width = 280
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'*++ Modificar nombre de la función. Modificar "Codispl" por el código lógico de la transacción
'% insPreCodispl: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCodispl()
	'--------------------------------------------------------------------------------------------
	'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
	Dim lclsClass As Object
	Dim mcolClass As Object
	
'UPGRADE_NOTE: The 'eDll.Collection' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	mcolClass = Server.CreateObject("eDll.Collection")
	
	If mcolClass.Find() Then
		For	Each lclsClass In mcolClass
			With mobjGrid
				.Columns("tcdField").DefValue = lclsClass.Propiedad
				.Columns("tcnField").DefValue = lclsClass.Propiedad
				
				'**+			.
				'**+			.				
				'**+			.				
				
				.Columns("tctField").DefValue = lclsClass.Propiedad
				.Columns("hddField").DefValue = lclsClass.Propiedad
				
				Response.Write(.DoRow)
			End With
		Next lclsClass
	End If
	
	Response.Write(mobjGrid.closeTable())
	mcolClass = Nothing
End Sub

'*++ Modificar nombre de la función. Modificar "Codispl" por el código lógico de la transacción
'% insPreCodisplUpd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCodisplUpd()
	'--------------------------------------------------------------------------------------------
	'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
	Dim lobjClass As Object
	
'UPGRADE_NOTE: The 'eDll.Class' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lobjClass = Server.CreateObject("eDll.Class")
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			Response.Write(mobjValues.ConfirmDelete())
			If lobjClass.insPostCodispl() Then
			End If
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "Página_de_validaciones.aspx", "Codispl", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	lobjClass = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VA666")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
'- Objeto para el manejo particular de los datos de la página

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE=JavaScript>
//**+ Las siguientes funciones deben colocarse sólo si la página corresponde al encabezado de la transacción

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
	<%
'**+ Si la ventana pertenece al encabezado colocar después de la referencia a GenFunctions.js:
'**+ <% %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		

	Response.Write mobjValues.StyleSheet()
	If Request.QueryString("Type") <> "PopUp" Then
'**+ Si se trata de una ventana que no forma parte del encabezado colocar:
    		Response.Write mobjMenu.setZone(2,"Codispl", Request.QueryString("sWindowDescript"), Request.QueryString("nWindowTy"))

'**+ Si la ventana pertenece al encabezado colocar:

		Set mobjMenu = Nothing
		Response.Write "<NOTSCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>"
	End If
    %>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="Nombre_de_la_página" ACTION="Página_de_validaciones.aspx?sMode=2">
    <%Response.Write(mobjValues.ShowWindowsName("Codispl", Request.QueryString.Item("sWindowDescript")))
'**+ El llamado a la función ShowWindowsName, debe colocarse sólo si la ventana es 
'**+ la página de detalle de la transacción o es un frame de la secuencia

Call insDefineHeader()

If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreCodisplUpd()
Else
	Call insPreCodispl()
End If

mobjValues = Nothing
mobjGrid = Nothing

%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("VA666")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




