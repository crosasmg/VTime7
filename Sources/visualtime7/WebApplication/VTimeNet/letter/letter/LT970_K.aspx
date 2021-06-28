<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: The Object to handling the load values general functions is defined
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores        
Dim mobjValues As eFunctions.Values

'**-Objetive: Definition of the object to handle the grid and its properties
'-Objetivo: Se define la variable para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'**-Objetive: The object to handling the page zones is defined
'-Objeto: para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues


'**%Objetive: Defines the columns of the grid 
'%Objetivo: Define las columnas del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	With mobjGrid
		.sSessionID = Session.SessionID
		.sCodisplPage = Request.QueryString.Item("sCodispl")
	End With
	'**+ The columns of the grid are defined
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		.AddPossiblesColumn(15750,"Tipo de endoso", "cbeEndorseType", "Table3012", eFunctions.Values.eValuesType.clngComboType,  String.Empty,  ,  ,  ,  ,  ,  ,  ,vbNullString)
		.AddPossiblesColumn(15751,"Modelo de Carta", "valLetterNum", "tabletters", eFunctions.Values.eValuesType.clngWindowType, String.Empty,  ,  ,  ,  ,  ,  ,  ,"Número que identifica el modelo de carta seleccionado.",  ,  ,  ,  ,  , True)
	End With
	
	'**+ The general properties of the grid are defined
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.ActionQuery = mobjValues.ActionQuery
		.bOnlyForQuery = .ActionQuery
		.sDelRecordParam = "nLetterNum='+ marrArray[lintIndex].valLetterNum + '" & "&nEndorseType='+ marrArray[lintIndex].cbeEndorseType + '"
		.Height = 170
		.Width = 350
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.Columns("Sel").GridVisible = Not .ActionQuery
		.Columns("cbeEndorseType").EditRecord = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
End Sub

'%**Objetive: The controls of the page are load
'%Objetivo: Se cargan los controles de la página
'-------------------------------------------------------------------------------------------
Private Sub insPreLT970()
	'-------------------------------------------------------------------------------------------
	Dim lcolEndorsLetterss As eLetter.EndorsLetterss
	Dim lclsEndorsLetters As Object
	
	lcolEndorsLetterss = New eLetter.EndorsLetterss
	With mobjGrid
		If lcolEndorsLetterss.Find() Then
			For	Each lclsEndorsLetters In lcolEndorsLetterss
				.Columns("cbeEndorseType").DefValue = lclsEndorsLetters.nEndorseType
				.Columns("cbeEndorseType").Descript = lclsEndorsLetters.sDescriptTable3012
				.Columns("valLetterNum").DefValue = lclsEndorsLetters.nLetterNum
				.Columns("valLetterNum").Descript = lclsEndorsLetters.sDescriptTab_Letter
				Response.Write(.DoRow)
			Next lclsEndorsLetters
		End If
	End With
	Response.Write(mobjGrid.closeTable)
	
	Response.Write(mobjValues.BeginPageButton)
	
	lclsEndorsLetters = Nothing
	lcolEndorsLetterss = Nothing
End Sub

'**%Objetive: The fields of the PopUp are defined
'%Objetivo: Se definen los campos de la PopUp del detalle
'-------------------------------------------------------------------------------------------
Private Sub insPreLT970Upd()
	'-------------------------------------------------------------------------------------------
	Dim lclsEndorsLetters As eLetter.EndorsLetters
	
	With Request
		If .QueryString.Item("Action") = "Del" Then
			
			lclsEndorsLetters = New eLetter.EndorsLetters
			
			If lclsEndorsLetters.InsPostLT970(.QueryString.Item("Action"), Session("nUsercode"), CShort(.QueryString.Item("nEndorseType")), CShort(.QueryString.Item("nLetterNum"))) Then
				Response.Write(mobjValues.ConfirmDelete())
			End If
			
			lclsEndorsLetters = Nothing
			
		End If
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valLetter.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
End Sub

</script>
<%
'----------------------------------------------------------------------------------------------------
'**+Objective:	It allows the user to associate letter templates to the different types of 
'**+			endorsements. Those letters will automatically be selected during the 
'**+			policy/certificate setup or policy/certificate modification process, filling a 
'**+			claim process or when the information of the client is created or updated.
'**+Version: $$Revision: 12 $
'+Objetivo: Permite a los usuarios del sistema asociar las plantillas de las cartas con diferentes
'+			tipos de endosos. Estas cartas serán automaticamente seleccionadas durante la emisión o
'+			procesos de modificación de polizas y certificados, en la declaración de un siniestro
'+			o cuando la información del cliente sea generada o actualizada.
'+Version: $$Revision: 12 $
'----------------------------------------------------------------------------------------------------
Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Visual TIME Templates">
    <%=mobjValues.StyleSheet()%>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

//**-Objetive: This line keep the source safe version
//-Objeto: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 12 $|$$Date: 4/07/04 2:49p $$Author: Dblanco $"
//------------------------------------------------------------------------------------------

//**%Objetive: It allows to cancel the page
//%Objetivo: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//**%Objetive: It allows to finish the page
//%Objetivo: Permite finalizar la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
    return true;
}

//**%Objetive: The actions are defined
//%Objetivo: Se definen las acciones.
//-------------------------------------------------------------------------------------------------------------------
function insPreZone(llngAction){
//-------------------------------------------------------------------------------------------------------------------
    switch (llngAction){
        case 302:
        case 305:
        case 401:
			document.location.href = document.location.href.replace(/&nMainAction.*/,'') + '&nMainAction=' + llngAction            
            break;
    }
}
</SCRIPT>
<%
With Request
	mobjValues.ActionQuery = (.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)) Or (.QueryString.Item("nMainAction") = vbNullString)
	
	If .QueryString.Item("Type") <> "PopUp" Then
		Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
		mobjMenues = New eFunctions.Menues
		mobjMenues.sSessionID = Session.SessionID
		Response.Write("<SCRIPT>var nMainAction=302</SCRIPT>")
		
		Response.Write(mobjMenues.MakeMenu(.QueryString.Item("sCodispl"), .QueryString.Item("sCodispl") & "_K.aspx", 1, .QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
		mobjMenues = Nothing
	End If
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">


<FORM METHOD="POST" ID="FORM" NAME="<%=Request.QueryString.Item("sCodispl")%>" ACTION="valLetter.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&nWindowTy=<%=Request.QueryString.Item("nWindowTy")%>">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	insPreLT970()
Else
	insPreLT970Upd()
End If
mobjGrid = Nothing
mobjValues = Nothing

mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
%>
</FORM>
</BODY>
</HTML>








