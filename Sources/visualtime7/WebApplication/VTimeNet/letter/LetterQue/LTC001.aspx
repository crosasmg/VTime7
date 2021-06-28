<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'**- The Object to handling the load values general functions is defined
'- Objeto para el manejo de las funciones generales de carga de valores		
Dim mobjValues As eFunctions.Values

'**- Definition of the object to handle the grid and its properties
'- Se define la variable para el manejo del Grid de la ventana
Dim mobjGrid As eFunctions.Grid

'**- The object to handling the page zones is defined
'- Objeto para el manejo de las zonas de la página
Dim mobjMenues As eFunctions.Menues


'**% insDefineHeader: Defines the columns of the grid 
'% insDefineHeader: Define las columnas del grid
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'dim dtmNull As String
	'-------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "LTC001"
	
	mobjValues.ActionQuery = False
	With mobjGrid
		If Request.QueryString.Item("Type") <> "PopUp" Then
			With .Columns
				.AddTextColumn(7291,"Cliente", "sCliename", 30, "", True,"Nombre del cliente",  ,  ,  , False)
				.AddNumericColumn(7292,"Solicitud", "nLettRequest", 15, CStr(0), True,"Número de la solicitud", False)
				.AddTextColumn(7293,"Descripcion", "tctLetters", 30, "",  ,"Descripción del modelo de carta")
				.AddTextColumn(7294,"Estado", "sStatLetter", 15, "", True,"Estado de la carta",  ,  ,  , False)
				.AddTextColumn(7295,"Solicitante", "nUserS", 20, "", True,"Persona que realizó la solicitud de correspondencia",  ,  ,  , False)
				'.AddDateColumn 7296,"Date","ddExpDate","",True,"Date of creation",,,,true
				
				.AddTextColumn(7297,"Tipo de Carta", "nTypeLetter", 15, "", True,"Tipo del modelo de carta",  ,  ,  , False)
                    '                .AddAnimatedColumn 7298,"View Letter", "btnLocation","/VTimeNet/images/A394Off.gif",vbNullString,,"JAVASCRIPT: insOpenDocument()"
				Call .AddHiddenColumn("sClientnas", "")
				Call .AddHiddenColumn("nLettRequs", "")
                    Call .AddHiddenColumn("ddExpDates", Nothing)
                    Call .AddHiddenColumn("ddExpDate", Nothing)
				Call .AddHiddenColumn("mint_AuxiCheck", CStr(0))
			End With
		End If
		.Height = 280
		.Width = 400
		.Codispl = "LTC001"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'**% inspreLTC001: The controls of the page are loaded and the values found in the search
'% inspreLTC001: Se cargan los controles de la página y los valores encontrados en la busqueda
'----------------------------------------------------------------------------------------------
Private Sub inspreLTC001()
	'----------------------------------------------------------------------------------------------
	Dim lclsletters As eLetter.LettRequest
        Dim lcolletterss As eLetter.LettRequests
        Dim lclsLettRequest As eLetter.LettRequest
        Dim lintIndex As Short
        Dim lstrCodispl As String
        Dim lParams As String
        lintIndex = 0
        lstrCodispl = "LTC001"
	lclsletters = New eLetter.LettRequest
        lcolletterss = New eLetter.LettRequests
        lclsLettRequest = New eLetter.LettRequest
	If lcolletterss.FindLetter(Session("toptCondition"), _
	                           mobjValues.StringToType(Session("tnRequest"), eFunctions.Values.eTypeData.etdInteger), _
	                           Session("tnClient"), _
	                           mobjValues.StringToType(Session("tcbeBranch"), eFunctions.Values.eTypeData.etdInteger), _
	                           mobjValues.StringToType(Session("tvalProduct"), eFunctions.Values.eTypeData.etdInteger), _
	                           mobjValues.StringToType(Session("tnPolicy"), eFunctions.Values.eTypeData.etdLong), _
	                           mobjValues.StringToType(Session("tnCertificate"), eFunctions.Values.eTypeData.etdInteger), _
	                           mobjValues.StringToType(Session("tnClaim"), eFunctions.Values.eTypeData.etdInteger), _
	                           mobjValues.StringToType(Session("tdEffectDat1"), eFunctions.Values.eTypeData.etdDate), _
	                           mobjValues.StringToType(Session("tdEffectDat2"), eFunctions.Values.eTypeData.etdDate), _
	                           mobjValues.StringToType(Session("lsAplicant"), eFunctions.Values.eTypeData.etdInteger)) Then
	                           
		For	Each lclsletters In lcolletterss
			With mobjGrid
				'.Columns("Sel").OnClick  = "insClickPeriods(this," & lintIndex & ")"
                    .Columns("sCliename").DefValue = lclsletters.sClieName
                    lParams = ""
                    If lclsLettRequest.Find(lclsletters.nLettRequest, True) Then
                        If lclsLettRequest.nClaim <> CShort(eRemoteDB.Constants.intNull) Then
                            lstrCodispl = "SCA803"
                            lParams = "'',0,0,0,0,''," & lclsLettRequest.nClaim & "," & lclsLettRequest.nCase_num & "," & lclsLettRequest.nDeman_type
                        ElseIf lclsLettRequest.nPolicy <> CShort(eRemoteDB.Constants.intNull) Then
                            lstrCodispl = "SCA802"
                            lParams = lclsLettRequest.sCertype & "," & lclsLettRequest.nBranch & "," & lclsLettRequest.nProduct & "," & lclsLettRequest.nPolicy & "," & lclsLettRequest.nCertif & ",'" & lclsLettRequest.DinpDate & "',0,0,0,''"
                        Else
                            lstrCodispl = "SCA801"
                            lParams = "'',0,0,0,0,'',0,0,0," & lclsLettRequest.sClient
                        End If
                    End If
                    .Columns("sCliename").HRefScript = "insOpenDocument(" & lclsletters.nLettRequest & ",'" & lclsletters.sClient & "'," & lclsletters.nLetterNum & ",1,'" & lstrCodispl & "'," & lParams & ")"
				.Columns("nLettRequest").DefValue = CStr(lclsletters.nLettRequest)
				.Columns("tctLetters").DefValue = lclsletters.sDescriptt
				.Columns("sStatLetter").DefValue = lclsletters.sDescripts
				.Columns("nUserS").DefValue = lclsletters.sclieNameSol
				.Columns("ddExpDate").DefValue = CStr(lclsletters.DinpDate)
				If lclsletters.nTypeLetter = 1 Then
					.Columns("nTypeLetter").DefValue = "Template"
				Else
					.Columns("nTypeLetter").DefValue = "Customized"
				End If
				.Columns("sClientnas").DefValue = lclsletters.sClient
				.Columns("nLettRequs").DefValue = CStr(lclsletters.nLettRequest)
				.Columns("ddExpDates").DefValue = CStr(lclsletters.DinpDate)
				Response.Write(.DoRow)
			End With
			lintIndex = lintIndex + 1
		Next lclsletters
	End If
	Response.Write(mobjValues.HiddenControl("ctnCounters", CStr(lintIndex - 1)))
	Response.Write(mobjGrid.CloseTable)
	mobjValues.ActionQuery = False
	Response.Write(mobjValues.BeginPageButton)
	lclsletters = Nothing
        lcolletterss = Nothing
        lclsLettRequest = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("LTC001")
%>


<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->



<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<HTML>
	<HEAD>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
	<SCRIPT>
	
//**+ This line keep the source safe version
//+ Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------ 
	document.VssVersion="$$Revision: 4 $|$$Date: 7/06/06 7:14p $" 
//------------------------------------------------------------------------------------------ 

//**% insCancel: Executes the action To cancel of the page
//% insCancel: ejecuta la acción Cancelar de la página
//---------------------------------------------------------------------------------------------
		function insCancel()
//---------------------------------------------------------------------------------------------
		{
//**+ Only this process will be effected when the user cancels the transaction always
//+ Sólamente se efectuará este proceso cuando el usuario cancela la transacción siempre 
			return true;
		}   

//**% insFinish(): This function executes the code when the action is finish
//% insFinish(): Esta función ejecuta el código cuando la acción es finalizar
//---------------------------------------------------------------------------------------------
		function insSubmit()	
//---------------------------------------------------------------------------------------------
		{
			return true;
		}

//**% insOpenDocument(): This function executes the instruction to visualize the selected document
//% insOpenDocument(): Esta función ejecuta la instrucción de visualizar el documento seleccionado
//--------------------------------------------------------------------------------    
function insOpenDocument(nLettRequest,nClientReg,nLetterNum,nLanguage,lstrCodispl,sCertype,nBranch,nProduct,nPolicy,nCertif,dEffecdate,nClaim,nCase_num,nDeman_type,sClient){
//--------------------------------------------------------------------------------    
    var lstrQueryString;
    var lstrAction;
    var lstrParams;
    ltrAction = "<%=Request.QueryString.Item("Action")%>";

    if (lstrCodispl == 'SCA801'){
        lstrParams = "&sQuery=1&sClient=" + sClient;
    }
    if (lstrCodispl == 'SCA802'){
        lstrParams = "&sQuery=1&sCertype=" + sCertype + "&nBranch=" + nBranch + "&nProduct=" + nProduct + "&nPolicy=" + nPolicy + "&nCertif=" + nCertif + "&dEffecdate=" + dEffecdate;
    }
    if (lstrCodispl == 'SCA803'){
        lstrParams = "&sQuery=1&nClaim=" + nClaim + "&nCase_num=" + nCase_num + "&nDeman_type=" + nDeman_type;
    }

    lstrQueryString = "/VTimeNet/Letter/Letter/Variables.aspx?sCustomLetter=1&sCodispl=" + lstrCodispl + "&Type=upd&Action=" + lstrAction + "&nClientReg=" + nClientReg + "&nLettRequest=" + nLettRequest + "&nLetterNum=" + nLetterNum + "&nLanguage=" + nLanguage + lstrParams;
    ShowPopUp(lstrQueryString,"Values", 50,50,"no","no", 2000, 2000);
}

//**% insClickPeriods: Update the AuxSel with the value checked 
//% insClickPeriods: Actualiza el AuxSel con el valor seleccionado 
//-------------------------------------------------------------------------------------------
function insClickPeriods(Field, nIndex){
//-------------------------------------------------------------------------------------------
	var lintSelected;
    if (Field.checked)
		lintSelected = 1;
    else
		lintSelected = 2;	
	if (marrArray.length == 1){
		self.document.forms[0].mint_AuxiCheck.value = lintSelected;
	}
	else {
		self.document.forms[0].mint_AuxiCheck[nIndex].value = lintSelected;
	}	
}

	</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0">
<%
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "LTC001"
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	mobjMenues = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
	mobjMenues.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	Response.Write("<SCRIPT>var	nMainAction	=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Response.Write(mobjValues.ShowWindowsName("LTC001", Request.QueryString.Item("sWindowDescript")))
	Response.Write(mobjMenues.setZone(2, "LTC001", Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy")))
	%>
		<TABLE BORDER="0" ALIGN="center" WIDTH=100%>
			<TR>
				<!--TD CLASS="HighLighted"><LABEL ID=7290>Consult of letter</LABEL></TD-->
			</TR>
			<TR><!--TD CLASS="HorLine"></TD--></TR>
		</TABLE> <%	
	mobjMenues = Nothing
End If
If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
	mobjValues.ActionQuery = True
End If
%>
	<SCRIPT LANGUAGE="JavaScript">
		var nMainAction = 302;	
	</SCRIPT>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="LTC001" ACTION="valletterque.aspx?x=1">
			<%Call insDefineHeader()
inspreLTC001()
mobjGrid = Nothing%>
		</FORM>
	</BODY>
</HTML>
<%
mobjValues = Nothing

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:59 a.m.
Call mobjNetFrameWork.FinishPage("LTC001")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







