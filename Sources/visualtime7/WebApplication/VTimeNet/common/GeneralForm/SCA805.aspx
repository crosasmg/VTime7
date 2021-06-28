<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 13/05/2003 10:35:21 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjGrid As eFunctions.Grid
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'-------------------------------------
Private Sub SafeEndorsements()
	'-------------------------------------
	Dim lobjEndorsLetters As eLetter.EndorsLetterss
	lobjEndorsLetters = New eLetter.EndorsLetterss
	'sFirst : Indica que es la primera vez que se esta entrando en esta página para ese cliente
	If CStr(Session("sFirst")) <> "No" Then
		Session("sFirst") = "No"
            Call lobjEndorsLetters.FindEndorsLetters(Session("sClient"), Session("nUsercode"), Session("sCodispl_LT"), Session("sCertype"), mobjValues.StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger),mobjValues.StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdInteger))
	End If
	lobjEndorsLetters = Nothing
End Sub
'% insDefineHeader
'-------------------------------------
Private Sub insDefineHeader()
	'-------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 13/05/2003 10:35:21 a.m.
	mobjGrid.sSessionID = Session.SessionID
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "SCA008"
	
	With mobjGrid.Columns
		.AddNumericColumn(90,"Modelo de carta", "tcnLetterNum", 4, CStr(0),  ,"Código del modelo de carta a utilizar para el envío de la correspondencia.",  ,  ,  ,  ,  , True)
		.AddTextColumn(91,"Descripción", "tctDescript", 30, "",  ,"Descripción del modelo de carta. Es mostrado por el sistema y el usuario no puede cambiar el contenido.",  ,  ,  , True)
		.AddPossiblesColumn(92,"Idioma", "cbeLanguage", "Table85", 1,  ,  ,  ,  ,  ,  ,  ,  ,vbNullString,  ,  ,  , False)
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddAnimatedColumn(93,"Ver Documento", "btnLocation", "/VTimeNet/images/A302Off.png","Permite consultar el modelo de carta. El usuario no puede modificar su contenido.",  , "JAVASCRIPT: insOpenDocument(0,1)")
		Else
			.AddAnimatedColumn(93,"Ver Documento", "btnLocation", "/VTimeNet/images/btn_ValuesOff.png","Permite consultar el modelo de carta. El usuario no puede modificar su contenido.")
		End If
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.AddCheckColumn(94,"e-mail", "chkEmail", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp","Indica que la correspondencia debe ser enviada vía correo electrónico.")
			.AddCheckColumn(95,"Correo", "chkMail", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp","Indica que la correspondencia debe ser enviada a través del correo convencional.")
			.AddCheckColumn(96,"Fax", "chkFax", vbNullString,  ,  ,  , Request.QueryString.Item("Type") <> "PopUp","Indica que la correspondencia debe ser enviada vía fax.")
			.AddCheckColumn(97,"Personalizado", "chkCustom", vbNullString,  ,  , "insCustomLetter(this);", Request.QueryString.Item("Type") <> "PopUp","Indica que la correspondencia es personalizada.")
		Else
			.AddCheckColumn(94,"e-mail", "chkEmail", "Correo electrónico",  ,  ,  , Session("bQuery"),"Indica que la correspondencia debe ser enviada vía correo electrónico.")
			.AddCheckColumn(95,"Correo", "chkMail", "Correo",  ,  ,  , Session("bQuery"),"Indica que la correspondencia debe ser enviada a través del correo convencional.")
			.AddCheckColumn(96,"Fax", "chkFax", "Fax",  ,  ,  , Session("bQuery"),"Indica que la correspondencia debe ser enviada vía fax.")
			.AddCheckColumn(97,"Personalizado", "chkCustom", "",  ,  , "insCustomLetter(this);", Session("bQuery"),"Indica que la correspondencia es personalizada.")
		End If
		
		'Fecha de Expiración
		.AddDateColumn(98,"Fecha máxima de permanencia", "tcdExpDate", "",  ,"Fecha máxima de permanencia en el sistema de las cartas generadas. El sistema elimina a esta fecha las cartas generadas.",  ,  ,  , Session("bQuery"))
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddAnimatedColumn(100,"Editar documento", "btnEditDoc", "/VTimeNet/images/A302Off.png","Permite que se muestre el contenido del modelo de carta a través del editor de texto dispuesto para tal fin.",  , "JAVASCRIPT: insOpenDocument(0,2)", True)
		End If

		If Request.QueryString.Item("Type") = "PopUp" Then
			.AddFileColumn(99,"Ruta modelo de carta", "tctFileName", 40, "", True,  ,"VerifyFile(this)")
		End If
		
		.AddTextAreaColumn(101,"Dirección", "tctAddress", "", 5, 46,  ,"Dirección del cliente donde se envía la correspondencia.", True)
		.AddHiddenColumn("tcnLettRequest", vbNullString)
		.AddHiddenColumn("tcdEffecDate", vbNullString)
		.AddHiddenColumn("tcnLanguage", CStr(eRemoteDB.Constants.intNull))
		.AddHiddenColumn("tcttDs_text", vbNullString)
		'Si este campo es requerido en segun el funcional, hay que "Pender" el codigo relativo a este control "chkRequired".
		'.AddCheckColumn(15838,"Requerido", "chkRequired", vbNullString,  ,  ,  , Session("bQuery"),"Indica que el modelo de carta es requerido para la transacción.")
	End With
	
	With mobjGrid
		.Height = 550
		.Width = 480
		.Top = 50
		.AddButton = False
		.DeleteButton = False
		.bCheckVisible = True
            .Codispl = Session("sCodispl_lt")
		.Codisp = "SCA008"
		.Columns("Sel").Disabled = Session("bQuery")
		.Columns("Sel").Alias_Renamed = "Indicador de modelo de carta seleccionado para el envío."
		'.Columns("chkRequired").Disabled = True
		.Columns("cbeLanguage").BlankPosition = False
		Call .SetWindowParameters(Request.QueryString.Item("sCodispl") & "&sCodispII=" & Request.QueryString.Item("sCodispII"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	End With
	
	mobjGrid.Columns("tctDescript").EditRecord = Not (Session("bQuery"))
End Sub

'%inspreSCA008 
'---------------------------------------------  
Private Function inspreSCA008() As Object
	'---------------------------------------------
	
    Response.Write("<BR><SCRIPT>")
	If Not Request.QueryString.Item("sZone") = "fraFolder" Then
		Response.Write("var nMainAction = top.fraSequence.plngMainAction;")
	Else
		Response.Write("var nMainAction=0;")
	End If
	
	

Response.Write("" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("function insCheckSelClick(Field,lintIndex){" & vbCrLf)
Response.Write("//-------------------------------------------------------------------------------------------" & vbCrLf)
Response.Write("    if (!Field.checked){" & vbCrLf)
Response.Write("        EditRecord(lintIndex,nMainAction,""Del"", ""nLettRequest="" + marrArray[lintIndex].tcnLettRequest + ""&sCodispII=")


Response.Write(Request.QueryString.Item("sCodispII"))


Response.Write(""");        " & vbCrLf)
Response.Write("    }" & vbCrLf)
Response.Write("    else" & vbCrLf)
Response.Write("		EditRecord(lintIndex,nMainAction,""Update"",""nIsAdding=1&sCodispII=")


Response.Write(Request.QueryString.Item("sCodispII"))


Response.Write(""")" & vbCrLf)
Response.Write("    " & vbCrLf)
Response.Write("    Field.checked = !Field.checked" & vbCrLf)
Response.Write("}" & vbCrLf)
Response.Write("</" & "SCRIPT>")


	
	Dim lobjRequest As Object
	Dim lcolRequests As eLetter.LettRequests
	Dim lobjLetter As Object
	Dim lintIndex As Short
	
	lintIndex = 0
	
	lcolRequests = New eLetter.LettRequests

	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'

        If lcolRequests.Find(Session("sCodispl_LT"), _
                         insGetAction(), _
                         getVariable("scertype"), _
                         mobjValues.StringToType(getVariable("nbranch"), eFunctions.Values.eTypeData.etdInteger), _
                         mobjValues.StringToType(getVariable("nproduct"), eFunctions.Values.eTypeData.etdInteger), _
                         mobjValues.StringToType(getVariable("npolicy"), eFunctions.Values.eTypeData.etdLong), _
                         mobjValues.StringToType(getVariable("ncertif"), eFunctions.Values.eTypeData.etdLong), _
                         mobjValues.StringToType(getVariable("nclaim"), eFunctions.Values.eTypeData.etdLong), _
                         mobjValues.StringToType(getVariable("ncase_num"), eFunctions.Values.eTypeData.etdInteger), _
                         mobjValues.StringToType(getVariable("nbordereaux"), eFunctions.Values.eTypeData.etdLong), _
                         getVariable("sclient"), _
                         Today, _
                         Session("nUserCode"), _
                         mobjValues.StringToType(getVariable("nDeman_type"), eFunctions.Values.eTypeData.etdInteger)) Then
	
            For Each lobjRequest In lcolRequests
			
                With lobjRequest
                    mobjGrid.Columns("Sel").OnClick = "insCheckSelClick(this," & CStr(lintIndex) & ")"
				
                    If .nLettRequest <> CShort(eRemoteDB.Constants.intNull) Or (.nEndorseType <> CShort(eRemoteDB.Constants.intNull)) Then
                        mobjGrid.Columns("Sel").Checked = CShort("1")
                    Else
                        mobjGrid.Columns("Sel").Checked = CShort("2")
                    End If
                    mobjGrid.Columns("tcnLetterNum").DefValue = .nLetterNum
                    mobjGrid.Columns("tctDescript").DefValue = .sDescripts
                    mobjGrid.Columns("cbeLanguage").DefValue = .nLanguage
                    mobjGrid.Columns("cbeLanguage").Descript = .sDescriptLanguage
                    mobjGrid.Columns("tcnLettRequest").DefValue = .nLettRequest
				
                    mobjGrid.Columns("tcnLanguage").DefValue = .nLanguage
                    mobjGrid.Columns("tcnLanguage").Descript = .sDescriptLanguage
				
                    If .nSendType And 1 Then
                        mobjGrid.Columns("chkEmail").Checked = CShort("1")
                    Else
                        mobjGrid.Columns("chkEmail").Checked = CShort("2")
                    End If
				
                    If .nSendType And 2 Then
                        mobjGrid.Columns("chkMail").Checked = CShort("1")
                    Else
                        mobjGrid.Columns("chkMail").Checked = CShort("2")
                    End If
				
                    If .nSendType And 4 Then
                        mobjGrid.Columns("chkFax").Checked = CShort("1")
                    Else
                        mobjGrid.Columns("chkFax").Checked = CShort("2")
                    End If
				
                    If Request.QueryString.Item("Type") = "PopUp" Then
                        mobjGrid.Columns("chkCustom").Checked = CShort("2")
                    Else
                        If .nTypeLetter = 1 Then
                            mobjGrid.Columns("chkCustom").Checked = CShort("1")
                        Else
                            mobjGrid.Columns("chkCustom").Checked = CShort("2")
                        End If
                    End If
				
                    mobjGrid.Columns("tcdExpDate").DefValue = .dExpDate
                    mobjGrid.Columns("tctAddress").DefValue = .sStreet
				
                    'mobjGrid.Columns("chkRequired").Checked = .sRequired
                    mobjGrid.Columns("btnLocation").HRefScript = "insOpenDocument(" & lintIndex.ToString() & ",1)"
                    Response.Write(mobjGrid.DoRow)
				
				
                End With
			
                lintIndex = lintIndex + 1
            Next lobjRequest

        End If

	Response.Write(mobjGrid.CloseTable)
	
	lcolRequests = Nothing
	lobjLetter = Nothing
End Function

'% inspreSCA008Upd 
'---------------------------------------------  
Private Function inspreSCA008Upd() As Object
	'---------------------------------------------
	
Response.Write("<SCRIPT>    " & vbCrLf)
Response.Write("   function insCustomLetter(Field){" & vbCrLf)
Response.Write("       document.forms[0].tctFileName.disabled = !(Field.checked)" & vbCrLf)
Response.Write("       document.forms[0].tctFileName.value    = "" """ & vbCrLf)
Response.Write("	   document.forms[0].btnEditDoc.disabled = !(Field.checked)" & vbCrLf)
Response.Write("   }" & vbCrLf)
Response.Write("  </" & "SCRIPT>")

	
	If Request.QueryString.Item("Action") = "Del" Then
		insDelItem()
		Response.Write(mobjValues.ConfirmDelete())
	End If

	Response.Write(mobjGrid.doformUpd(Request.QueryString.Item("Action"), "valLetters.aspx?sCodisp=SCA008&sCodispII=" & Request.QueryString.Item("sCodispII") & "nAction=" & Request.QueryString.Item("nAction"), Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"),  , CShort(Request.QueryString.Item("Index"))))
End Function

'% getVariables
'---------------------------------------------------------------------
Private Function getVariable(ByRef svariable As String) As Object
	'---------------------------------------------------------------------
	Select Case Trim(UCase(Left(Request.QueryString.Item("sCodispl"), 6)))
		
		'+Clientes
		
		Case "SCA801","SCA805"
                If svariable = "sclient" Then
                    getVariable = Session(svariable)
                End If
			
			'+Pólizas
			
		Case "SCA802"
			
Response.Write("            ")

                If svariable = "scertype" Or svariable = "nbranch" Or svariable = "nproduct" Or svariable = "npolicy" Or svariable = "ncertif" Then
                    getVariable = Session(svariable)
                End If
			
			'+Siniestros
			
		Case "SCA803"
                If svariable = "nclaim" Or svariable = "ncase_num" Or svariable = "nDeman_type" Or svariable = "nbranch" Then
                    getVariable = Session(svariable)
                End If
			
			'+Cobranzas
			
		Case "SCA804"
                If svariable = "nbordereaux" Then
                    getVariable = Session(svariable)
                End If
		Case Else
			getVariable = Session(svariable)
	End Select
End Function

'----------------------------------
Private Function insGetAction() As Object
	'----------------------------------
	Select Case Left(Request.QueryString.Item("sCodispl"), 6)
		
		'+ Clientes
		
            Case "SCA801", "SCA805"
                insGetAction = Request.QueryString.Item("nMainAction")
			
                '+ Pólizas  
			
		Case "SCA802"
			insGetAction = Session("ntransaction")
			
			'+ Siniestros          
			
		Case "SCA803"
			insGetAction = Session("ntransaction")
			
			'+ Cobranzas        
		Case "SCA804"
			
		Case Else
			insGetAction = Request.QueryString.Item("nAction")
	End Select
End Function

'----------------------------------------------------------------------------
Private Sub insDelItem()
	'----------------------------------------------------------------------------    
	Dim nLettRequest As Object
	Dim lobjLetter As eLetter.LettRequest
	
	lobjLetter = New eLetter.LettRequest
	
	If Left(Request.QueryString.Item("sCodispl"), 6) = "SCA801" Then
            lobjLetter.Delete(mobjValues.StringToType(Request.QueryString.Item("nLettRequest"), eFunctions.Values.eTypeData.etdInteger), Session("sClient"), "SCA801")
	Else
		lobjLetter.Delete(mobjValues.StringToType(Request.QueryString.Item("nLettRequest"), eFunctions.Values.eTypeData.etdInteger), vbNullString, vbNullString)
		
	End If
	Session("sLettRequests") = Replace(Session("sLettRequests"), "," & Request.QueryString.Item("nLettRequest") & ",", "")
	
	lobjLetter = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("SCA008")

mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 13/05/2003 10:35:21 a.m.
mobjMenu.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 13/05/2003 10:35:21 a.m.
mobjValues.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "SCA008"

mobjValues.ActionQuery = Session("bQuery")
%>
<html>
<head>
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<SCRIPT>
//**-Objetive: This line keep the source safe version
//-Objeto: Esta línea guarda la versión procedente de VSS 
//------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 36 $|$$Date: 4/28/05 3:04p $$Author: Jcastillo $"
//------------------------------------------------------------------------------------------
</SCRIPT>
<SCRIPT>
//**% insAbout: Call to PopUp windows of About of...
//% insAbout: Llama a la ventana de PopUp de Acerca de...
//-------------------------------------------------------------------------------------------
function insAbout(){
//-------------------------------------------------------------------------------------------
    var lstrVersion = '';
	lstrVersion = self.document.VssVersion;
    lstrVersion = lstrVersion.replace("\$\$\Revision: ","&VSSVersion=");
    lstrVersion = lstrVersion.replace(" \$\|\$\$\Date: ","&VSSVersionDate=");
    lstrVersion = lstrVersion.replace(/ .*/,'');

    ShowPopUp("/VTimeNet/Common/about.aspx?sCodispl=SCA805&sCodisp=SCA805" + lstrVersion,"HelpAbout",300,120,"No","No",50,30);
}
//**% insHelp: Call to PopUp windows of Help...
//% insHelp: Llama a la ventana de PopUp de Ayuda...
//-------------------------------------------------------------------------------------------
function insHelp(){
//-------------------------------------------------------------------------------------------
    ShowPopUp("/VTimeNet/Common/Help.aspx?sCodispl=SCA805","Help",600,500,"Yes","Yes",50,20);
}

//**% PreDoSubmit: Call to PopUp windows of Help...
//% PreDoSubmit: Código que se ejecuta al Aceptar la PopUp y, al terminar, hace el llamado al InsDoSubmit
//-------------------------------------------------------------------------------------------
function PreDoSubmit() {
//-------------------------------------------------------------------------------------------
    var clsFileSystem;
    var clsFile;
	
<%'If Request.QueryString.Item("Type") = "PopUp" Then%>
	if(document.forms[0].tctFileName.value.length>0){
	    clsFileSystem = new ActiveXObject('Scripting.FileSystemObject');
	    clsFile = clsFileSystem.OpentextFile(document.forms[0].tctFileName.value)
	    document.forms[0].tcttDs_text.value = clsFile.readAll();
	    clsFile.close();
	}
	InsDoSubmit();
<%' End If %>
}
</SCRIPT>

<SCRIPT>
<% If Request.QueryString.Item("Type") <> "PopUp" And Request.QueryString.Item("sCodispII") <> "SCA805" Then %>
	top.fraSequence.bCalcValPage= 1;
<% End If %>

//--------------------------------------------------------------------------------    
function insOpenDocument(lintIndex, lintbtn){
//--------------------------------------------------------------------------------    
    var lstrQueryString;
    var lstrAction;
	var fso = new ActiveXObject("Scripting.FileSystemObject");
	var lstrLocation;
	//alert(lintIndex);
	//alert(lintbtn);
	//alert(marrArray[lintIndex].tcnLettRequest);
	//alert(marrArray[lintIndex].tcnLetterNum);
	lstrAction = '<%=Request.QueryString.Item("Action")%>';

    if (lintbtn == 2)
		lstrLocation = document.forms[0].tctFileName.value
	else    
		lstrLocation = ''
	
	if (lintbtn == 1 || (fso.FileExists(document.forms[0].tctFileName.value)  && lintbtn == 2)){

<%If Request.QueryString.Item("Type") = "PopUp" Then%>
          lstrQueryString = "/VTimeNet/Letter/Letter/Variables.aspx?sCustomLetter=1&sCodispl=<%=Session("sCodispl_LT")%>&Type=upd&Action=" + lstrAction + "&Location=" + lstrLocation + "&nLetterNum=" + document.forms[0].tcnLetterNum.value + "&nLettRequest=" + document.forms[0].tcnLettRequest.value + "&nLanguage=" + document.forms[0].cbeLanguage.value + "&sBtn=" + lintbtn;
<%Else%>
          lstrQueryString = "/VTimeNet/Letter/Letter/Variables.aspx?sCustomLetter=1&sCodispl=<%=Session("sCodispl_LT")%>&Type=Qry&Action=" + lstrAction + "&Location=" + lstrLocation +  "&nLetterNum=" + marrArray[lintIndex].tcnLetterNum + "&nLettRequest=" + marrArray[lintIndex].tcnLettRequest + "&nLanguage=" + marrArray[lintIndex].tcnLanguage  + "&sBtn=" + lintbtn;
<%End If%>        
        ShowPopUp(lstrQueryString,"Values", 50,50,"no","no", 2000, 2000);
     }
     else
		  alert('Debe indicar una ruta y archivo válidos');
    
} 

//--------------------------------------------------------------------------------    
function VerifyFile(Field){
//--------------------------------------------------------------------------------    
	var fso = new ActiveXObject("Scripting.FileSystemObject");
	
	if (!fso.FileExists(document.forms[0].tctFileName.value))
		alert('Debe indicar una ruta y archivo válidos');
}
</SCRIPT>

</head>

<body ONUNLOAD="closeWindows();">

<% 
'+Variable para el manejo dinámico del tag "FORM" de la pagina.
Dim strDeclareFormTag As String

'+Se arma la linea que conforma el tag "FORM" de la pagina.
'strDeclareFormTag = "<FORM METHOD=" & Chr(34) & "POST" & Chr(34) & "  ID=" & Chr(34) & "FORM" & Chr(34) & "  NAME=" & Chr(34) & "frmSCA008" & Chr(34) & "  ACTION=" & Chr(34) & "valLetters.aspx?time=1" 
strDeclareFormTag = "<FORM METHOD='POST'  ID='FORM'  NAME='frmSCA008'  ACTION='valLetters.aspx?time=1&nMainAction=" + Request.Querystring("nMainAction")

If Not IsNothing(Request.QueryString.Item("sCodispII")) Then
	'strDeclareFormTag += "&" & Request.Params.Get("Query_String") & Chr(34) & "   ENCTYPE=" & Chr(34) & "multipart/form-data" & Chr(34) & " >"
	strDeclareFormTag += "&" & Request.Params.Get("Query_String") '& "   ENCTYPE='multipart/form-data' >"
'Else
'	'strDeclareFormTag += Chr(34) & "   ENCTYPE=" & Chr(34) & "multipart/form-data" & Chr(34) & " >"
'	strDeclareFormTag += Chr(34) & "   ENCTYPE='multipart/form-data' >"
End If
strDeclareFormTag += "'   ENCTYPE='multipart/form-data' >"

'+Se reemplazan las comillas simples por comillas dobles y se escribe este resultado en el documento.
Response.Write(strDeclareFormTag.Replace("'",""""))

Response.Write(mobjValues.StyleSheet())

If Left(Request.QueryString.Item("sCodispl"), 5) <> "SCA80" Then
	Response.Write(mobjValues.ShowWindowsName("SCA805", Request.QueryString.Item("sWindowDescript")))
Else
	Response.Write(mobjValues.ShowWindowsName(Session("sCodispl_LT"), Request.QueryString.Item("sWindowDescript")))
End If


If Request.QueryString.Item("Type") <> "PopUp" And Request.QueryString.Item("sCodispII") <> "SCA805" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End If

mobjMenu = Nothing
SafeEndorsements()
insDefineHeader()


If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreSCA008()
Else
	inspreSCA008Upd()
End If

mobjGrid = Nothing
%>
</FORM>
</body>
    
</html>
<%mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 13/05/2003 10:35:21 a.m.
Call mobjNetFrameWork.FinishPage("SCA008")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>










