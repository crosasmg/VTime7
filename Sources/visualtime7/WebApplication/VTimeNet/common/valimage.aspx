<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="ePolicy" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<script language="VB" runat="Server">

    '^Begin Header Block VisualTimer Utility 1.1 13/05/2003 10:35:24 a.m.
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    Dim crlf As String = Chr(13) & Chr(10)
    
    Dim mobjValues As eFunctions.Values
    Dim mstrPath As String
    Dim mstrCommand As String
    Dim ScriptObject As FileStream
    Dim ByteCount As Integer
    Dim binData() As Byte
    Dim mobjUploadRequest As Dictionary(Of String, String)
    Dim myRequestFile(4) As String
    Dim mstrErrors As String
    
    Dim fileContentIndex As Integer
    Dim fileContentLength As Integer

'- Constante para identificar si el control FILE tiene o no contenido
Const CN_NOTEMPTY As String = "Con contenido"

'- Campos de la forma utilizados
Dim mlngImagenum As Object
    Dim mintConsec As String
    Dim mstrDescript As String
    Dim mdtmCompdate As String
    Dim mdtmNulldate As String
    Dim mintRectype As String
    Dim mintUsercode As String
    Dim mstrCodispl As String
    Dim mstrContinue As String
    Dim mstrQueryString As String


'% insvalImage: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalImage() As String
	'--------------------------------------------------------------------------------------------
	Dim lclsImages As eGeneralForm.GeneralForm
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ Ventana de Imágenes
		Case "SCA10-2", "SCA10-1", "SCA593", "SCA10-3"
			lclsImages = New eGeneralForm.GeneralForm

            If Request.QueryString.Item("Action") = "Update" And myRequestFile(1) = "" Then
                myRequestFile(1) = CN_NOTEMPTY
            End If

                insvalImage = lclsImages.insValSCA002(mobjUploadRequest("sCodispl").ToString, "Image", mobjUploadRequest("tctDescript").ToString, mobjUploadRequest("tcdCompdate").ToString, mobjUploadRequest("tcdNulldate").ToString, , myRequestFile(1))
                lclsImages = Nothing
			
            Case Else
                insvalImage = "insvalImage: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostGeneralForm: Se realizan las actualizaciones de las ventanas
    '--------------------------------------------------------------------------------------------
    Function insPostGeneralForm() As Boolean
        Dim result As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lclsClient As Object
        Dim lclsClaim As Object
        Dim lclsCertificat As ePolicy.Certificat
        Dim lblnPost As Boolean
	
        lblnPost = True
	
        Dim lclsImages As eGeneralForm.Images
        Dim lclsNumerator As eGeneral.GeneralFunction
        Select Case Request.QueryString.Item("sCodispl")
		
            '+ Ventana de Imágenes
            Case "SCA10-2", "SCA10-1", "SCA593", "SCA10-3"
			
                lclsImages = New eGeneralForm.Images

                With lclsImages
                    .nImagenum = mobjValues.StringToType(mobjUploadRequest("tcnImagenum").ToString, eFunctions.Values.eTypeData.etdLong)
                    .nConsec = mobjValues.StringToType(mobjUploadRequest("tcnConsec").ToString, eFunctions.Values.eTypeData.etdInteger)
                    .sDescript = mobjUploadRequest("tctDescript").ToString
                    .dNulldate = mobjValues.StringToType(mobjUploadRequest("tcdNulldate").ToString, eFunctions.Values.eTypeData.etdDate)
                .nRecType = mobjValues.StringToType(mobjUploadRequest("nRectype").ToString, eFunctions.Values.eTypeData.etdInteger)
                    .nUsercode = mobjValues.StringToType(mobjUploadRequest("tcnUsercode").ToString, eFunctions.Values.eTypeData.etdInteger)
                    .sSource = updImage()
                End With
			
                Select Case Request.QueryString.Item("Action")
				
                    '+ Se crea el registro en la tabla sin la imagen
                    Case "Add"
                        lclsNumerator = New eGeneral.GeneralFunction
					
                        If lclsImages.nImagenum = 0 Then
                            lclsImages.nImagenum = lclsNumerator.Find_Numerator(23, 0, Session("nUsercode"))
                            mobjUploadRequest("tcnImagenum") = lclsImages.nImagenum
                        End If
					
                        If lclsImages.Add Then
                            lblnPost = True

                            Call updImage()
						
                            Select Case Request.QueryString.Item("sCodispl")
                                Case "SCA10-2"
								
                                    '+ Se actualiza el número de imagen en la tabla Client
                                    lclsClient = New eClient.Client
                                    With lclsClient
                                        .sClient = Session("sClient")
                                        .nUsercode = mobjValues.StringToType(mobjUploadRequest("tcnUsercode").ToString, 2)
                                        Call .UpdateImageNum(Integer.Parse(mobjUploadRequest("tcnImagenum").ToString))
                                    End With
								
                                Case "SCA10-3"
                                    lblnPost = True
                                    mstrQueryString = "&WindowType=PopUp&nImagenum=" & mlngImagenum
								
                                Case "SCA10-1"
                                    '+ Se actualiza el número de imagen en la tabla Claim
                                    lclsClaim = New eClaim.Claim
                                    With lclsClaim
                                        If .Find(Session("nClaim")) Then
                                            .nImagenum = Long.Parse(mobjUploadRequest("tcnImagenum").ToString)
                                            .nUsercode = Session("nUsercode")
                                            lblnPost = .Update
                                        End If
                                    End With
								
                                Case "SCA593"
								
                                    '+ Se actualiza el número de imagen en la tabla Prof_ord
                                    lclsClaim = New eClaim.Prof_ord
                                    If lclsClaim.Find_nServ(Session("nServ_order")) Then
                                        'result = lclsClaim.InsPostOS590Upd(Session("nServ_order"), lclsClaim.dMade_date, lclsClaim.sPlace, lclsClaim.nMunicipality, lclsClaim.nStatus_ord, Session("nUsercode"), mlngImagenum, mobjValues.StringToType(CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble, True))
										result = lclsClaim.InsPostOS590Upd(Session("nServ_order"), lclsClaim.dMade_date, lclsClaim.sPlace, lclsClaim.nMunicipality, lclsClaim.nStatus_ord, Session("nUsercode"), Long.Parse(mobjUploadRequest("tcnImagenum").ToString), mobjValues.StringToType(CStr(eRemoteDB.Constants.intNull), eFunctions.Values.eTypeData.etdDouble, True))										
                                    End If
								
                                    '+ Se actualiza el número de imágen en la tabla certificat
                                    If CStr(Session("nOrdClass")) <> "3" Then
                                        lclsCertificat = New ePolicy.Certificat
                                        With lclsCertificat
                                            If .Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then
                                                .nImageNum = Long.Parse(mobjUploadRequest("tcnImagenum").ToString)
                                                .nUsercode = Session("nUsercode")
                                                lblnPost = .Update
                                            End If
                                        End With
                                        lclsCertificat = Nothing
                                    End If
                            End Select
                        End If ' lclsImages.Add
					
                        lclsNumerator = Nothing
					
                    Case "Update"
                        If lclsImages.Update Then
                            lblnPost = True
                            If Request.QueryString.Item("sCodispl") = "SCA10-3" Then
                                mstrQueryString = "&WindowType=PopUp&nImagenum=" & mlngImagenum
                            End If
                        End If
                End Select
			
			lclsImages.EmptyImageFolder()
			lclsImages = Nothing
			
	End Select
	
	If lblnPost Then
		Select Case Request.QueryString.Item("sCodispl")
			Case "SCA10-2"
				'+ Se actualiza Client_Win
				lclsClient = New eClient.ClientWin
				lclsClient.insUpdClient_win(Session("sClient"), CStr(Request.QueryString.Item("sCodispl")), "2")
				
			Case "SCA10-1"
				'+ Se actualiza Claim_Win
				lclsClaim = New eClaim.Claim_win
				Call lclsClaim.Add_Claim_win(Session("nClaim"), Request.QueryString.Item("sCodispl"), "2", Session("nUsercode"))
		End Select
	End If
	insPostGeneralForm = lblnPost
	
	lclsClaim = Nothing
	lclsClient = Nothing
End Function

'% updImage: Actualiza la tabla de Imagenes con la imagen que recibe la página 
'--------------------------------------------------------------------------------------------
Function updImage() As String
	'--------------------------------------------------------------------------------------------
    Dim sFilename As String = Path.GetFileName(myRequestFile(2))
    Dim sSavePath As String
    Dim fileAppend As Integer
    Dim lobjValues As eFunctions.Values
        
    lobjValues = New eFunctions.Values
    lobjValues.sSessionID = Session.SessionID
    lobjValues.sCodisplPage = "valImage"
        
        sSavePath = lobjValues.insGetSetting("ImageTemp", "", "Paths")
               
    If String.IsNullOrEmpty(sFilename) Then Return String.Empty
        
    Do While File.Exists(sSavePath & "\" & sFilename)
        fileAppend += 1
        sFilename = Path.GetFileNameWithoutExtension(myRequestFile(2)) & fileAppend.ToString & _
            Path.GetExtension(myRequestFile(2))
    Loop

        Dim newFile As FileStream = Nothing
        
        newFile = New FileStream(sSavePath & "\" & sFilename, FileMode.Create)
            
        'For i As Integer = fileContentIndex To fileContentLength
        For i As Integer = 0 To fileContentLength - 1
            newFile.WriteByte(binData(fileContentIndex + i))
        Next
        
        newFile.Close()
        
        Return sSavePath & "\" & sFilename
        	
End Function

    Sub BuildUploadRequest(ByVal data() As Byte)
        'Array que contendrá la data decodificada
        Dim postData(data.Length) As Char
    
        'Se inicializa el decodificador ASCII
        Dim decoder As Decoder = Encoding.ASCII.GetDecoder
    
        'Se decodifican los bytes contenidos en binData, y se almacena en el array postData
        decoder.GetChars(data, 0, data.Length, postData, 0)
    
        'Se obtiene el Encoding Type y el Boundary del Form, y se separan en un array.
        Dim contentType As String = Request.ServerVariables("HTTP_CONTENT_TYPE")
        Dim conTypArr() As String = contentType.Split("; ")
    
        'Se verifica que el Encoding Type sea el correcto. De otro modo no se podra leer el archivo.
        If conTypArr(0) = "multipart/form-data" Then
            'Se obtiene el Boundary del Form. Este dato es el que separa los valores de cada control en el Request.
            Dim bound(1) As String
            bound(1) = conTypArr(1).Split("=")(1)
            'Se obtiene un array, que contiene la data de todos los controles del Form.
            Dim formData() As String = (New String(postData)).Split(bound, StringSplitOptions.RemoveEmptyEntries)
        
            'Se inicializa el diccionario.
            mobjUploadRequest = New Dictionary(Of String, String)
        
            Dim endInfo As Integer
            Dim varInfo As String
            Dim varValue As String
        
            For i As Integer = 0 To formData.Length - 1
                'Se ubican los caracteres separadores.
                endInfo = formData(i).IndexOf(crlf & crlf)
            
                If endInfo > -1 Then
                    'Obtiene el nombre de la variable
                    varInfo = formData(i).Substring(2, endInfo - 2)
                    'Obtiene el valor de la variable
                    varValue = formData(i).Substring(endInfo + 4, formData(i).Length - endInfo - 8)
                
                    'Es este elemento un archivo?
                    If varInfo.Contains("filename=") Then
                        myRequestFile(0) = getFieldName(varInfo)
                        myRequestFile(1) = varValue
                        myRequestFile(2) = getFileName(varInfo)
                        myRequestFile(3) = getFileType(varInfo)
                        
                        fileContentIndex = (New String(postData)).IndexOf(varValue)
                        
                        fileContentLength = varValue.Length
                        
                    Else
                        mobjUploadRequest.Add(getFieldName(varInfo), varValue)
                    End If
                End If
            Next
        End If
    End Sub

    ' This function retreives a field's name
    Function getFieldName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("name=")
        Dim endPos As Integer = infoStr.Substring(sPos + 5).IndexOf(Chr(34) & ";")
        If endPos = -1 Then
            endPos = infoStr.Substring(sPos + 6).IndexOf(Chr(34))
        End If
        
        Return infoStr.Substring(sPos + 6, endPos)
    End Function

    ' This function retreives a file field's filename
    Function getFileName(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("filename=")
        Dim endPos As Integer = infoStr.IndexOf(Chr(34) & crlf)
        getFileName = infoStr.Substring(sPos + 10, endPos - (sPos + 10))
    End Function
    
    ' This function retreives a file field's mime type
    Function getFileType(ByVal infoStr As String) As String
        Dim sPos As Integer = infoStr.IndexOf("Content-Type: ")
        Return infoStr.Substring(sPos + 14)
    End Function

'% insGetSource: se arma la dirección general en caso de advertencias
'--------------------------------------------------------------------------------------------
Private Sub insGetSource()
	'--------------------------------------------------------------------------------------------
        Dim lstrModule As String = String.Empty
        Dim lstrProject As String = String.Empty
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ Imagen del siniestro
		Case "SCA10-1"
			lstrModule = "Claim"
			lstrProject = "ClaimSeq"
			mstrPath = "/VTimeNet/Claim/ClaimSeq/"
			
			'+ Imagen del cliente
		Case "SCA10-2"
			lstrModule = "Client"
			lstrProject = "ClientSeq"
			mstrPath = "/VTimeNet/Client/ClientSeq/"
			
			'+ Imagen de la orden de servicio
		Case "SCA593"
			lstrModule = "Prof_ord"
			lstrProject = "Prof_ordseq"
			mstrPath = "/VTimeNet/Prof_ord/Prof_ordseq/"
			
			'+ Imagen de propuesta de siniestro
		Case "SCA10-3"
			lstrModule = "Claim"
			lstrProject = "Claim"
			mstrPath = "/VTimeNet/Claim/Claim/"
	End Select
	mstrCommand = "&sModule=" & lstrModule & "&sProject=" & lstrProject & "&sCodisplReload=" & Request.QueryString.Item("sCodispl")
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
</HEAD>
<BODY>
<%Response.Write("<SCRIPT>")%>
//% CancelErrors: regresa a la ventana que invocó los errores
//-------------------------------------------------------------------------------------------
function CancelErrors(){
	self.history.go(-1)
}
//-------------------------------------------------------------------------------------------

//% NewLocation: Se mueve a la siguiente ventana de la secuencia
//-------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<%
    mstrCommand = "&sModule=Common&sProject=Image&sCodisplReload=" & Request.QueryString.Item("sCodispl")
    Call insGetSource()
    
If CDbl(Request.QueryString.Item("nAction")) <> 390 And CDbl(Request.QueryString.Item("nAction")) <> 392 Then

    binData = Request.BinaryRead(Request.TotalBytes)
        	
    '**+ Its analiced the information to comes to the page
    '+ Se interpretan los datos que llegan a la página	
        	
    BuildUploadRequest(binData)
	
	'+ Se toman los valores de los objectos que recibe la forma	
        mlngImagenum = mobjUploadRequest("tcnImagenum").ToString
        mintConsec = mobjUploadRequest("tcnConsec").ToString
        mstrDescript = mobjUploadRequest("tctDescript").ToString
        mdtmCompdate = mobjUploadRequest("tcdCompdate").ToString
        mdtmNulldate = mobjUploadRequest("tcdNulldate").ToString
        mintUsercode = mobjUploadRequest("tcnUsercode").ToString
        mintRectype = mobjUploadRequest("nRectype").ToString
        mstrCodispl = mobjUploadRequest("sCodispl").ToString
	
	'+ El manejo de errores es en caso que el Ckeck de "Continuar", no se encuentre marcado.
	'+ En ese caso no se incorpora al diccionario de objectos de la página.
	On Error Resume Next
	mstrContinue = mobjUploadRequest("chkContinue").ToString  
	If Err.Number Then
	End If
	
	mstrErrors = insvalImage
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
	
	If mstrErrors > vbNullString Then
		With Response
			.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
			.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """,""ClaimErrors"",660,330);")
			.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
			.Write("</SCRIPT>")
		End With
	Else
		If insPostGeneralForm Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
			Else
				'+ Si la ventana no forma parte de uan secuencia				    
				If Request.QueryString.Item("sCodispl") <> "SCA10-3" Then
					Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "&sGoToNext=NO" & "';</SCRIPT>")
				End If
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "SCA10-1", "SCA10-2", "SCA593", "SCA10-3"
						Response.Write("<SCRIPT>top.opener.document.location.href='SCA010.aspx?sCodispl=" & mstrCodispl & "&Reload=" & mstrContinue & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&Index=" & Request.QueryString.Item("Index") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&sOnSeq=" & Request.QueryString.Item("sOnSeq") & mstrQueryString & "'</SCRIPT>")
				End Select
			End If
		End If
	End If
Else
	If CDbl(Request.QueryString.Item("nAction")) = 390 Then
		Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='" & mstrPath & "Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
	Else
		Response.Write("<SCRIPT>insReloadTop(false);</SCRIPT>")
	End If
End If
mobjUploadRequest = Nothing
mobjValues = Nothing
%>
</BODY>
</HTML>





