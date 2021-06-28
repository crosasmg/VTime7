<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Object for the management of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores   
Dim mobjValues As eFunctions.Values

'- The variable one is defined mobjGrid for the management of the Grid of the window
'- Se define la variable mobjGrid para el manejo del Grid de la ventana  
Dim mobjGrid As eFunctions.Grid

'- Object for the management of the zones of the page
'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues




'%Se definen las columnas del grid
'---------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.sCodisplPage = "LT002"
	
	With mobjGrid.Columns
		
		Call .AddPossiblesColumn(7258,"Proceso", "valProcess", "tabLT002", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  ,  ,"Código del proceso al cual se le asocia el Modelo de Carta")
		Call .AddBranchColumn(7259,"Ramo", "cbeBranch","Código del ramo comercial",  ,  ,  ,  ,  , False)
		Call .AddProductColumn(7260,"Producto", "valProduct","Código del producto",  ,  ,  ,  ,  ,  , False)
		Call .AddPossiblesColumn(7261,"Modelo de Carta", "valLetter", "tabTab_letters", eFunctions.Values.eValuesType.clngWindowType, vbNullString, False,  ,  ,  ,  ,  ,  ,"Número que identifica el Modelo de Carta a asociar")
		Call .AddTextColumn(7262,"Rutina", "tctRoutine", 12, vbNullString,  ,"Nombre de la rutina que contiene los criterios para la selección del Modelo de Carta")
		Call .AddCheckColumn(15749,"Requerido", "chksRequired", "", 1,  ,  , False,"El Modelo de Carta es requerido en la transacción")
		Call .AddHiddenColumn("nConsec", CStr(0))
		Call .AddHiddenColumn("tcnPolicy", 0)
		
	End With
	
	With mobjGrid
		.Codispl = "LT002"
		.ActionQuery = mobjValues.ActionQuery
		.Height = 280
		.Width = 400
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("valProcess").Parameters.Add("sCodispl", Session("sTransaction"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.sDelRecordParam = "nConsec=' + marrArray[lintIndex].nConsec + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'%insPreLT002. Se crea la ventana madre (Principal)
'---------------------------------------------------------------------------
Private Sub insPreLT002()
	'---------------------------------------------------------------------------
	Dim lcolLetters_ass As eLetter.Letters_ass
	Dim lclsLetter_as As Object
	
	lcolLetters_ass = New eLetter.Letters_ass
	
	If lcolLetters_ass.FindLT002(Session("sTransaction")) Then
		For	Each lclsLetter_as In lcolLetters_ass
			With mobjGrid
				.Columns("valProcess").DefValue = lclsLetter_as.nProcess
				.Columns("cbeBranch").DefValue = lclsLetter_as.nBranch
				.Columns("valProduct").DefValue = lclsLetter_as.nProduct
				.Columns("valLetter").DefValue = lclsLetter_as.nLetterNum
				.Columns("tctRoutine").DefValue = lclsLetter_as.sRoutine
				.Columns("nConsec").DefValue = lclsLetter_as.nConsec
				If lclsLetter_as.sRequired = "1" Then
					.Columns("chksRequired").checked = 1
				Else
					.Columns("chksRequired").checked = 0
				End If
				Response.Write(.doRow)
			End With
		Next lclsLetter_as
	End If
	
	lcolLetters_ass = Nothing
	Response.Write(mobjGrid.closeTable() & "<SCRIPT>insShowHeader('" & Session("sTransaction") & "')</" & "Script>" & mobjValues.BeginPageButton)
End Sub

'---------------------------------------------------------------------------
Private Sub insPreLT002Upd()
	'---------------------------------------------------------------------------
	Dim lobjLetters_as As eLetter.Letters_as
	If UCase(Request.QueryString.Item("Action")) = "DEL" Then
		lobjLetters_as = New eLetter.Letters_as
		Call lobjLetters_as.insPostLT002(Request.QueryString.Item("Action"), Session("sTransaction"), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, "", mobjValues.StringToType(Request.QueryString.Item("nConsec"), eFunctions.Values.eTypeData.etdLong), Session("nUsercode"), "")
		Response.Write(mobjValues.ConfirmDelete())
	Else
	End If
	With Request
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valLetter.aspx", Request.QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))) & mobjValues.HiddenControl("sAction", Request.QueryString.Item("Action")))
	End With
	
	lobjLetters_as = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LT002")

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID

mobjValues.sCodisplPage = "LT002"
mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = "401")
mobjMenu = New eFunctions.Menues
mobjMenu.sSessionID = Session.SessionID

%>
<HTML>
<HEAD>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio	6.0">
<SCRIPT	LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.setZone(2, "LT002", Request.QueryString.Item("sWindowDescript"), mobjValues.StringToType(Request.QueryString.Item("nWindowTy"), eFunctions.Values.eTypeData.etdInteger)))
		mobjMenu = Nothing
	End If
	.Write(mobjValues.WindowsTitle("LT002", Request.QueryString.Item("sWindowDescript")))
End With
%>
<SCRIPT>

//%insShowHeader. Esta funcion se encarga de actualizar los datos del encabezado de la ventana.
//---------------------------------------------------------------------------------
function insShowHeader(lstrTransaction,lstrDescript){
//---------------------------------------------------------------------------------
    var lblnDoit=true;
    if (typeof(top.frames["fraHeader"].document)!='undefined')
        if (typeof(top.frames["fraHeader"].document.forms[0].elements["valTransaction"])!='undefined'){
			top.frames["fraHeader"].document.forms[0].elements["valTransaction"].value = lstrTransaction;
			top.frames["fraHeader"].$("#valTransaction").change();
			lblnDoit = false;
		}
    if (lblnDoit) setTimeout("insShowHeader(" + lstrTransaction  + ");",50)
}

//---------------------------------------------------------------------------------
function insCheckBranch(Field){
//---------------------------------------------------------------------------------
	UpdateDiv('valProductDesc',document.forms[0].valProduct.value = '','Normal');
	with (document.forms[0]){
	    btnvalProduct.disabled = valProduct.disabled = Field.value == '0'
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST"	ID="FORM" NAME="frmLT002" ACTION="valLetter.aspx?sZone=2"  >
<%

Response.Write(mobjValues.ShowWindowsName("LT002", Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Call insPreLT002()
Else
	Call insPreLT002Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing
%>	  

<%If Request.QueryString.Item("Type") = "PopUp" And UCase(Request.QueryString.Item("Action")) <> "DEL" Then%>
<SCRIPT>
		insCheckBranch(document.forms[0].cbeBranch);
</SCRIPT>
<%End If%>
</FORM>
</BODY>
</HTML>
<%
Call mobjNetFrameWork.FinishPage("LT002")
mobjNetFrameWork = Nothing
%>








