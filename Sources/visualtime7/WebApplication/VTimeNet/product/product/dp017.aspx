<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader:Permite definir las columnas del grid, así como también de habilitar o inhabilitar
'% los botones de agregar y cancelar.
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "dp017"
	
	'+ Se definen las columnas del Grid.
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "DP017"
	End With
	
	With mobjGrid.Columns
		Call .AddNumericColumn(100348, GetLocalResourceObject("tcnAgeStartColumnCaption"), "tcnAgeStart", 5, vbNullString, False, GetLocalResourceObject("tcnAgeStartColumnToolTip"))
		Call .AddNumericColumn(100348, GetLocalResourceObject("tcnAgeEndColumnCaption"), "tcnAgeEnd", 5, vbNullString, False, GetLocalResourceObject("tcnAgeEndColumnToolTip"))
		Call .AddNumericColumn(100349, GetLocalResourceObject("tcnRatepureColumnCaption"), "tcnRatepure", 8, vbNullString, False, GetLocalResourceObject("tcnRatepureColumnToolTip"),  , 5)
		Call .AddNumericColumn(100350, GetLocalResourceObject("tcnRatenoniColumnCaption"), "tcnRatenoni", 8, vbNullString, False, GetLocalResourceObject("tcnRatenoniColumnToolTip"),  , 5)
		Call .AddNumericColumn(100351, GetLocalResourceObject("tcnRateniveColumnCaption"), "tcnRatenive", 8, vbNullString, False, GetLocalResourceObject("tcnRateniveColumnToolTip"),  , 5)
	End With
	
	With mobjGrid
		.Height = 300
		.Width = 380
        .WidthDelete = 450
		.Columns("tcnAgeStart").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		.Columns("tcnAgeEnd").Disabled = Not (Request.QueryString.Item("Action") = "Add")
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or Request.QueryString.Item("nMainAction") = vbNullString Then
			.Columns("Sel").GridVisible = False
			.ActionQuery = True
		End If
		If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
			.Columns("tcnRatepure").EditRecord = True
			.AddButton = True
			.DeleteButton = True
			.sDelRecordParam = "nAgeStart=' + marrArray[lintIndex].tcnAgeStart + '&nAgeEnd=' + marrArray[lintIndex].tcnAgeEnd + '"
			
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End If
	End With
End Sub

'% insPreDP017: Se definen los objetos a ser utilizados a lo largo de la transacción.
'-----------------------------------------------------------------------------------------
Private Sub insPreDP017()
	'-----------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lobjObject As Object
	Dim lintIndex As Object
	Dim lcolRate_lifes As eProduct.Rate_lifes
	Dim lclsRate_life As eProduct.Rate_life
	
	
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("//% insPreZone: Se definen las acciones." & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insPreZone(llngAction){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("	switch (llngAction){" & vbCrLf)
Response.Write("	    case 301:" & vbCrLf)
Response.Write("	    case 302:" & vbCrLf)
Response.Write("	    case 401:" & vbCrLf)
Response.Write("	        document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction" & vbCrLf)
Response.Write("	        break;" & vbCrLf)
Response.Write("	}" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")

	
	
	'+ Se setean los objetos y se realiza el llamado al método que realiza la 
	'+ lectura de los registros a mostrar en las columnas del grid.
	lclsRate_life = New eProduct.Rate_life
	lcolRate_lifes = New eProduct.Rate_lifes
	
	If lcolRate_lifes.Find(Session("nBranch"), Session("nProduct"), Session("nCover"), Session("dEffecdate"), True) Then
		lintCount = 0
		
		For	Each lobjObject In lcolRate_lifes
			With lobjObject
				mobjGrid.Columns("tcnAgeStart").DefValue = .nAgeStart
				mobjGrid.Columns("tcnAgeEnd").DefValue = .nAgeEnd
				mobjGrid.Columns("tcnRatepure").DefValue = .nRatepure
				mobjGrid.Columns("tcnRatenoni").DefValue = .nRatenoni
				mobjGrid.Columns("tcnRatenive").DefValue = .nRatenive
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lobjObject
	End If
	
	Response.Write(mobjGrid.closeTable())
	
	lcolRate_lifes = Nothing
	lobjObject = Nothing
	lclsRate_life = Nothing
End Sub

'% insPreDP017Upd: Permite realizar el llamado a la ventana PopUp, cuando se está eliminando
'% un registro. 
'-----------------------------------------------------------------------------------------
Private Sub insPreDP017Upd()
	'-----------------------------------------------------------------------------------------
	Dim lclsRate_life As eProduct.Rate_life
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete())
		
		
		lclsRate_life = New eProduct.Rate_life
		
		Call lclsRate_life.insPostDP017("Delete", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), CInt(Request.QueryString.Item("nAgeStart")), CInt(Request.QueryString.Item("nAgeEnd")), mobjValues.StringToType(Request.Form.Item("tcnRatepure"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRatenoni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnRatenive"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
		
		lclsRate_life = Nothing
	End If
	
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "ValProduct.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Sub

</script>
<%
Response.Expires = 0

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "dp017"
%>
<SCRIPT LANGUAGE="JavaScript">
//- Variable para el control de versiones
document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:56 $|$$Author: Nvaplat61 $"

//% insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
</SCRIPT>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<%
'+ Se realiza el llamado a las rutinas generales para cargar la página invocada.
With Response
	.Write(mobjValues.StyleSheet())
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "DP017", "DP017.aspx"))
		mobjMenu = Nothing
	End If
	
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
		mobjValues.ActionQuery = True
	End If
End With
%>
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="DP017" ACTION="valProduct.aspx?sZone=2">
<%
Call insDefineHeader()

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>" & mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
	
	Call insPreDP017()
Else
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")) & "<BR>")
	
	Call insPreDP017Upd()
End If

mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
</HTML>




