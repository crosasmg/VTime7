<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClient" %>

<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo del grid
Dim mobjGrid As eFunctions.Grid

'- Tipo de registro. Identifica el tipo de imagen
Dim mintRectype As Object

'- Numero de imagen que se cargara en la página	
Dim mlngImagenum As Integer

Dim mnMainAction As Object

'- Variables para la validación del archivo
Dim mclsGeneral As eGeneral.GeneralFunction
Dim mstrPathInvalid As String

'- Bloque donde se crea el mensaje de error 
Dim lobjErrors As eGeneral.GeneralFunction
Dim mstrAlert As String


'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	Dim lclsUsers As eGeneral.Users
	
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "sca010"
	
	'+ Si la acción es consulta no se establece la propiedad ActionQuery sobre el objeto del
	'+ grid con la variable de sesión bquery, ya que es necesario que aparezcan los links
	'+ sobre las notas para lograr acceder a su descripción.
	If Not mnMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
		mobjGrid.ActionQuery = Session("bQuery")
	End If
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(19654, GetLocalResourceObject("tcnImagenumColumnCaption"), "tcnImagenum", 10, vbNullString,  , GetLocalResourceObject("tcnImagenumColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddNumericColumn(40572, GetLocalResourceObject("tcnConsecColumnCaption"), "tcnConsec", 5, vbNullString,  , GetLocalResourceObject("tcnConsecColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddTextColumn(40574, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, vbNullString,  , GetLocalResourceObject("tctDescriptColumnToolTip"),  ,  ,  , Session("bQuery"))
		Call .AddDateColumn(40576, GetLocalResourceObject("tcdCompdateColumnCaption"), "tcdCompdate", CStr(Today),  , GetLocalResourceObject("tcdCompdateColumnToolTip"),  ,  ,  , True)
		Call .AddDateColumn(40577, GetLocalResourceObject("tcdNulldateColumnCaption"), "tcdNulldate", "",  , GetLocalResourceObject("tcdNulldateColumnToolTip"),  ,  ,  , Session("bQuery"))
		Call .AddNumericColumn(40573, GetLocalResourceObject("tcnUsercodeColumnCaption"), "tcnUsercode", 5, "",  , GetLocalResourceObject("tcnUsercodeColumnToolTip"), False,  ,  ,  ,  , True)
		Call .AddTextColumn(40575, GetLocalResourceObject("tctClienameColumnCaption"), "tctCliename", 60, "",  , GetLocalResourceObject("tctClienameColumnToolTip"),  ,  ,  , True)
		Call .AddFileColumn(40585, GetLocalResourceObject("sSourceColumnCaption"), "sSource", 40,  , Session("bQuery"))
		Call .AddAnimatedColumn(0, "", "iImage", "/VTimeNet/Images/batchStat04.png", GetLocalResourceObject("iImageColumnToolTip"))
		Call .AddHiddenColumn("nRectype", Request.QueryString.Item("nRectype"))
		Call .AddHiddenColumn("sCodispl", Request.QueryString.Item("sCodispl"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SCA010"
		.Height = 460
		.Width = 550
		.Top = 150
		.Left = 100
		.nMainAction = mnMainAction
		.Columns("tcnConsec").EditRecord = True
		.Columns("tctDescript").EditRecord = True
		.sDelRecordParam = "nImagenum='+ marrArray[lintIndex].tcnImagenum + '&nConsec='+ marrArray[lintIndex].tcnConsec + '"
		.MoveRecordScript = "ChangeImage()"
		
		If Session("bQuery") Then
			.AddButton = False
			.DeleteButton = False
		End If
		
		If CStr(Session("sOriginalForm")) <> vbNullString Then
			.AddButton = False
			.DeleteButton = False
			.ActionQuery = True
		Else
			.AddButton = Not Session("bQuery")
			.DeleteButton = Not Session("bQuery")
			If Not mnMainAction = eFunctions.Menues.TypeActions.clngActionQuery Then
				.ActionQuery = Session("bQuery")
			End If
		End If
		
		.Columns("Sel").GridVisible = Not Session("bQuery")
		.Columns("iImage").Width = 100
		.Columns("iImage").Height = 100
		If Request.QueryString.Item("Action") = "Update" Then
			.Columns("iImage").HRefScript = "ShowZoomImage()"
		End If
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			.Columns("tcnImagenum").GridVisible = False
			.Columns("iImage").GridVisible = False
			.Columns("sSource").GridVisible = False
		End If
		
		If Request.QueryString.Item("Type") = "PopUp" Then
			.Columns("sSource").OnChange = "checkImageDimensions(this.value, """ & mstrAlert & """);"
		End If
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	
	With Response
		If Request.QueryString.Item("Type") = "PopUp" Then
			lclsUsers = New eGeneral.Users
			.Write("<SCRIPT>")
			.Write("var mintUsercode = " & Session("nUsercode") & "; ")
			.Write("var mstrCliename = """ & lclsUsers.FindUserName(Session("nUsercode")) & """;")
			.Write("</" & "Script>")
		End If
	End With
	lclsUsers = Nothing
End Sub

'% insPreSCA010: se realiza al cargar la página 
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA010()
        '--------------------------------------------------------------------------------------------
        Dim lcolImages As New eGeneralForm.Imagess
        Dim lcolImage As New eGeneralForm.Images
	
        If lcolImages.Find(mlngImagenum) Then
            If lcolImages.Count > 0 Then
                For Each lcolImage In lcolImages
                    With lcolImage
                        mobjGrid.Columns("tcnImagenum").DefValue = CStr(.nImagenum)
                        mobjGrid.sEditRecordParam = "nImagenum=" & .nImagenum & "&nConsec=" & .nConsec & "&nRectype=" & mintRectype
                        mobjGrid.Columns("tcnConsec").DefValue = CStr(.nConsec)
                        mobjGrid.Columns("tctDescript").DefValue = .sDescript
                        mobjGrid.Columns("tcnConsec").EditRecord = True
                        mobjGrid.Columns("tctDescript").EditRecord = True
                        mobjGrid.Columns("tcdCompdate").DefValue = CStr(.dCompdate)
                        mobjGrid.Columns("tcdNulldate").DefValue = CStr(.dNulldate)
                        mobjGrid.Columns("tcnUsercode").DefValue = CStr(.nUsercode)
                        mobjGrid.Columns("tctCliename").DefValue = .sCliename
                        Response.Write(mobjGrid.DoRow)
                    End With
                Next
            End If
        End If
        Response.Write(mobjGrid.closeTable)
	
        '+ Si la ventana se invoca como una PopUp
        If Request.QueryString.Item("sCodispl") = "SCA10-3" Then
		
            Response.Write("" & vbCrLf)
            Response.Write("	<BR>" & vbCrLf)
            Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
            Response.Write("		<TR>" & vbCrLf)
            Response.Write("			<TD COLSPAN=""3"" CLASS=""HORLINE""></TD>" & vbCrLf)
            Response.Write("		</TR>" & vbCrLf)
            Response.Write("		<TR>" & vbCrLf)
            Response.Write("			<TD WIDTH=5%>")


            Response.Write(mobjValues.ButtonAbout(Request.QueryString.Item("sCodispl")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("			<TD WIDTH=5%>")


            Response.Write(mobjValues.ButtonHelp(Request.QueryString.Item("sCodispl")))


            Response.Write("</TD>" & vbCrLf)
            Response.Write("			<TD ALIGN=""RIGHT"">")

		
            If mnMainAction = 401 Then
                Response.Write(mobjValues.ButtonAcceptCancel("UpdateImageNum(" & Request.QueryString.Item("nImagenum") & ")", , False, , eFunctions.Values.eButtonsToShow.OnlyCancel))
            Else
                Response.Write(mobjValues.ButtonAcceptCancel("UpdateImageNum(" & Request.QueryString.Item("nImagenum") & ")", , False))
            End If
		
            Response.Write("		" & vbCrLf)
            Response.Write("			</TD>" & vbCrLf)
            Response.Write("		</TR>" & vbCrLf)
            Response.Write("	</TABLE>")

		
        End If
	
        lcolImages = Nothing
End Sub

'% insPreSCA010Upd: Se realiza el manejo de los campos para el manejo de la PopUp
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA010Upd()
	'--------------------------------------------------------------------------------------------
	Dim lobjImages As eGeneralForm.GeneralForm
	Dim lobjCertificat As ePolicy.Certificat
	Dim lblnPost As Boolean
	Dim lclsImagess As eGeneralForm.Imagess
	Dim lclsClient_win As eClient.ClientWin
	
	With Request
		If Request.QueryString.Item("Action") = "Del" Then
			lobjImages = New eGeneralForm.GeneralForm
			Response.Write(mobjValues.ConfirmDelete())
			
			With Request
				lobjImages.sClient = Session("sClient")
				lobjImages.nClaim = Session("nClaim")
				lobjImages.nServ_order = Session("nServ_order")
				
				lobjImages.insPostImages(.QueryString.Item("Action"), .QueryString.Item("sCodispl"), CInt(.QueryString.Item("nImagenum")), CInt(.QueryString.Item("nConsec")),  ,  ,  ,  , Session("nUsercode"))
			End With
			lobjImages = Nothing
			
			'+ Si la transacción en tratamiento es Imagen de la orden de servicio
			'+ y el origen es póliza o propuesta se debe anular el número de imagen en la tabla certificat		 			
			If Request.QueryString.Item("sCodispl") = "SCA593" Then
				If CStr(Session("nOrdClass")) <> "3" Then
					lobjCertificat = New ePolicy.Certificat
					With lobjCertificat
						If .Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), True) Then
							.nImagenum = eRemoteDB.Constants.intNull
							.nUsercode = Session("nUsercode")
							lblnPost = .Update
						End If
					End With
					lobjCertificat = Nothing
				End If
			End If ' -- Request.QueryString("sCodispl") = "SCA593"
			' Actualiza carpeta con contenido o sin contenido
			If Request.QueryString.Item("sCodispl") = "SCA10-2" Then
				lclsImagess = New eGeneralForm.Imagess
				If Not lclsImagess.Find(mlngImagenum) Then
					lclsClient_win = New eClient.ClientWin
					If lclsClient_win.insUpdClient_win(Session("sClient"), Request.QueryString.Item("sCodispl"), "1") Then
						Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</" & "Script>")
					End If
				End If
				lclsImagess = Nothing
				lclsClient_win = Nothing
			End If
		End If ' -- Request.QueryString("Action") = "Del"
		
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "ValImage.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
		
		If Request.QueryString.Item("Action") = "Add" Then
			Response.Write("<SCRIPT>insDefValues()</" & "Script>")
		Else
			If Request.QueryString.Item("Action") = "Update" Then
				Response.Write("<SCRIPT>insShowImage(" & Request.QueryString.Item("Index") & ")</" & "Script>")
			End If
		End If
		
	End With
	If Request.QueryString.Item("Action") <> "Del" Then
		
Response.Write("" & vbCrLf)
Response.Write("<SCRIPT>" & vbCrLf)
Response.Write("    mstrPath = document.forms[0].sSource.value" & vbCrLf)
Response.Write("	insChangeImage()    " & vbCrLf)
Response.Write("</" & "SCRIPT>")

		
	End If
End Sub

'% insKey: Se crea el key de la dirección
'--------------------------------------------------------------------------------------------
Private Sub insKey()
	'--------------------------------------------------------------------------------------------
	Dim lclsGeneralForm As eGeneralForm.Images
	
	lclsGeneralForm = New eGeneralForm.Images
	
	'+ Asignacion del tipo de registro	
	Select Case Request.QueryString.Item("sCodispl")
		Case "SCA10-1"
			'+ Imagen del siniestro
			Call lclsGeneralForm.getImageKey("SCA10-1", Session("nClaim"))
			
		Case "SCA10-2"
			
			'+ Imagen del cliente		
			Call lclsGeneralForm.getImageKey("SCA10-2", Session("sClient"))
			
		Case "SCA10-3"
			'+ Imagen de la propuesta de siniestro
			Call lclsGeneralForm.getImageKey("SCA10-3", Request.QueryString.Item("nImagenum"))
			
		Case "SCA593"
			
			'+ Imagen de la orden de servicio		
			Call lclsGeneralForm.getImageKey("SCA593", Session("nServ_order"))
	End Select
	
	mintRectype = lclsGeneralForm.nRectype
	mobjGrid.Columns("nRectype").DefValue = mintRectype
	mlngImagenum = lclsGeneralForm.nImagenum
	
	'+ Se busca el nombre del usuario que maneja la forma
	With Response
		.Write("<SCRIPT>")
		.Write("var nRectype = " & lclsGeneralForm.nRectype & ";")
		.Write("</" & "Script>")
	End With
	
	lclsGeneralForm = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "sca010"

If Request.QueryString.Item("nMainAction") = "undefined" Then
	mnMainAction = 0
Else
	mnMainAction = Request.QueryString.Item("nMainAction")
End If

If mnMainAction = 401 Then
	mobjValues.ActionQuery = True
	Session("bQuery") = True
End If

mclsGeneral = New eGeneral.GeneralFunction
lobjErrors = New eGeneral.GeneralFunction
mstrAlert = "Err. 60487 " & lobjErrors.insLoadMessage(60487)
lobjErrors = Nothing
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">  


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

//+ Variable para el control de versiones
        document.VssVersion="$$Revision: 5 $|$$Date: 12/10/04 17.31 $"
        
//-	Variable utilizada al amplificar la imagen
	var mstrImageSrc = "";
	var mstrPath="";

// ShowZoomImage: muestra un zoom de la imagen
//--------------------------------------------------------------------------------------------
function ShowZoomImage(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		mstrImageSrc = (mstrPath!=''?mstrPath:"ShowImage.aspx?nImagenum=" + tcnImagenum.value + "&nConsec=" + tcnConsec.value)
	}
	ShowPopUp('/VTimeNet/Common/ZoomImage.aspx', 'Zoom', 350, 350, 'yes', 'yes')
}
// insDefValues: muestra los valores por defecto
//--------------------------------------------------------------------------------------------
function insDefValues(){
//--------------------------------------------------------------------------------------------
//+ Se define la variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0

//+ Se genera el número consecutivo de la imagen (el Nº consecutivo más alto +1)   
	for(var llngIndex = 0;eval(llngIndex)<eval(top.opener.marrArray.length);llngIndex++)
	    if(eval(top.opener.marrArray[llngIndex].tcnConsec)>eval(llngMax))
	        llngMax = top.opener.marrArray[llngIndex].tcnConsec
	
//+ Se asignan los valores del Nro. de Imagen y consecutivo a los campos de la página	
	with (self.document.forms[0]){
		<%If IsNothing(Request.QueryString.Item("nImagenum")) Then%>
			tcnImagenum.value = 0
		<%Else%>
			tcnImagenum.value = <%=Request.QueryString.Item("nImagenum")%>	//+ Imagen
		<%End If%>
		tcnConsec.value = ++llngMax;					//+ Consecutivo
		tcnUsercode.value = mintUsercode	   			//+ Codigo del usuario
		tctCliename.value = mstrCliename		   		//+ Nombre del usuario
	}
	self.document.images["iImage"].src = "/VTimeNet/images/Logo.gif"
}
// ChangeImage: se cambia la imagen al moverse dentro del grid
//--------------------------------------------------------------------------------------------
function ChangeImage(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		self.document.images["iImage"].src = "ShowImage.aspx?nImagenum=" + tcnImagenum.value + "&nConsec=" + tcnConsec.value
	}
}
// insShowImage: muestra la imagen asociada al consecutivo
//--------------------------------------------------------------------------------------------
function insShowImage(Index){
//--------------------------------------------------------------------------------------------
	<%If Request.QueryString.Item("nImagenum") > vbNullString Then%>
		if(Index==-1){
			self.document.images["iImage"].src = "ShowImage.aspx?nImagenum=0" + <%=Request.QueryString.Item("nImagenum")%> + "&nConsec=0<%=Request.QueryString.Item("nConsec")%>"  
        }
		else{
			self.document.images["iImage"].src = "ShowImage.aspx?nImagenum=0" + <%=Request.QueryString.Item("nImagenum")%> + "&nConsec=0" + top.opener.marrArray[Index].tcnConsec
	    }
	<%End If%>
}
// insChangeImage: se muestra la imagen indicada
//--------------------------------------------------------------------------------------------
function insChangeImage(){
//--------------------------------------------------------------------------------------------
    if (typeof(document)!='undefined')
       if (typeof(document.forms[0])!='undefined')
          if (typeof(document.forms[0].sSource)!='undefined')
				if (mstrPath!=document.forms[0].sSource.value){
				    document.iImage.src = document.forms[0].sSource.value
				    mstrPath = document.forms[0].sSource.value
					//checkImageDimensions(mstrPath, '<%=mstrAlert%>'); 
				} 
	setTimeout("insChangeImage()",4000) 
} 
//%	UpdateImageNum: Actualiza el número de imagen en la página requerida.
//-------------------------------------------------------------------------------------------
function UpdateImageNum(nImagenum) {
//-------------------------------------------------------------------------------------------	
	with(top.opener.document.forms[0]) {
//+ Se actualiza el número de la imagen
	    top.opener.document.btnImagenum.value = nImagenum;
        tcnImagenum.value = nImagenum;
    }
    window.close()
}    
</SCRIPT>


	<%
With Response
	If Request.QueryString.Item("WindowType") = "PopUp" Then
		.Write("<SCRIPT>var nMainAction=" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
	Else
		If Request.QueryString.Item("Type") <> "PopUp" Then
			mobjMenu = New eFunctions.Menues
			.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), "SCA010.aspx"))
			.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
			mobjMenu = Nothing
		End If
	End If
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle(Request.QueryString.Item("sCodispl")))
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="frmSCA010" ACTION="valImage.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>" ENCTYPE="multipart/FORM-data">
<%Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
mstrPathInvalid = mclsGeneral.insLoadMessage(55855)
Call insDefineHeader()
Call insKey()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreSCA010Upd()
Else
	Call insPreSCA010()
End If
mobjGrid = Nothing
mclsGeneral = Nothing
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>





