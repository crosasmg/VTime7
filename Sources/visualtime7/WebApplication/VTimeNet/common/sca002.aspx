<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eClaim" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneralForm" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="ePolicy" %> 
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eLedge" %>

<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.33.47
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Tipo de registro que identifica el número de nota
Dim mintRectype As Object

'- Numero de nota que cargara en la página	
Dim mlngNotenum As Object

'- Número del subindice de la nota
Dim mlngIndexNotenum As String

'- Variable que guarda el nombre del usuario
Dim mstrUserName As String

'- Objetos para el manejo de las clases.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid
Dim lclsUsers As eGeneral.Users

'- Indicador de si es posible editar el texto de la nota pese a estar en consulta
'- Esta funcionalidad permite que las notas se puedan consultar, es decir,
'- no agregar ni eliminar notas, pero al mismo tiempo, da la opción de modificar el texto
'- de la nota consultada.
'- Creada inicialmente para el manejo de clausulas (SCA2-A), puede usarse en otras 
'- transacciones según se requiera. Inicialmente toma valor Falso
Dim mblnEnableEditDesc As Boolean
    
Dim StyleNotes As String = ConfigurationManager.AppSettings("StyleNotes").ToString.Trim.ToUpper 

'% insPreSCA002: Inicializa valores para el manejo de notas dependiendo de la transaccion
'--------------------------------------------------------------------------------------------
Private Sub insPreSCA002()
	'Dim eFunctions As Object
	Dim clngRecDiscNote As Byte
	Dim clngNoteLedUpd As Byte
	Dim clngNoteObsPropo As Byte
	Dim clngNoteClause As Byte
	Dim clngClinicHistor As Byte
	Dim clngRiskNote As Byte
	Dim clngCovertextNote As Byte
	Dim clngFinancialNote As Byte
	Dim clngNoteEvaluac As Byte
	''Dim eRemoteDB.Constants.intNull As Object
	Dim clngNoteTransp As Byte
	Dim clngClaimNote As Byte
	Dim clngClauseNote As Byte
	Dim clngClientNote As Byte
	Dim clngProfOrdNote As Byte
	Dim clngCashBankNote As Byte
	Dim clngCarDescriptNote As Byte
	Dim clngPolicyNote As Byte
	Dim clngActionQuery As String
	Dim lclsObject1 As Object
	'--------------------------------------------------------------------------------------------
	'- Objeto para busqueda de datos. No se le define nombre especifico
	'- porque puede ser usado por distintos tipos de objeto
	Dim lclsObject As Object
	
	'+ Obtiene el número de la nota
	mlngNotenum = Request.QueryString.Item("nNotenum")
	
	'+ Inicializa la edicion de nota en consulta
	mblnEnableEditDesc = False
	
	'+ Asignación del tipo de registro	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+ SCA2-5 : Nota de daños ocurridos al vehículo	
		Case "SCA2-5"
			mintRectype = 9
			lclsObject = New eClaim.Claim_case
			
			If CStr(Session("nCase_num")) = vbNullString Then
				Session("nCase_num") = Request.QueryString.Item("nCase_num")
			End If
			
			If CStr(Session("nDeman_type")) = vbNullString Then
				Session("nDeman_type") = Request.QueryString.Item("nDeman_type")
			End If
			
			If lclsObject.Find(mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
				mlngNotenum = lclsObject.nNoteDama
			Else
				mlngNotenum = 0
			End If
			
			'+ SCA2-S : Notas de los casos de siniestros		
		Case "SCA2-S"
			mintRectype = 23
			lclsObject = New eClaim.Claim_case
			
			If Request.QueryString.Item("nCase_num") <> vbNullString Then
				Session("nCase_num") = Request.QueryString.Item("nCase_num")
			End If
			
			If Request.QueryString.Item("nDeman_type") <> vbNullString Then
				Session("nDeman_type") = Request.QueryString.Item("nDeman_type")
			End If
			
			If lclsObject.Find(mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
				mlngNotenum = lclsObject.nNoteDama
			Else
				If mobjValues.StringToType(mlngNotenum, eFunctions.Values.eTypeData.etdLong) <= 0 Then
					mlngNotenum = Request.QueryString.Item("nNotenum")
				End If
			End If
			
			'+ Notas del Siniestro	
		Case "SCA2-8"
			lclsObject = New eClaim.Claim
			If lclsObject.Find(Session("nClaim")) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			
			'+ Notas del Rechazo de un siniestro 	
		Case "SCA2-961"
			lclsObject = New eClaim.Claim_his
			
			If lclsObject.FindTransac(Session("nClaim"), Session("nTRansac")) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			
			
			'+ Notas del Cliente
		Case "SCA2-9"
			lclsObject = New eClient.Client
			If lclsObject.Find(Session("sClient")) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClientNote
			
			'+ SCA2-10 : Declaracion del asegurado
		Case "SCA2-10"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			lclsObject = New eClaim.Claim_Auto
			
			If CStr(Session("nCase_num")) = vbNullString Then
				Session("nCase_num") = Request.QueryString.Item("nCase_num")
			End If
			
			If CStr(Session("nDeman_type")) = vbNullString Then
				Session("nDeman_type") = Request.QueryString.Item("nDeman_type")
			End If
			
			If lclsObject.Find(mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCase_num"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nDeman_type"), eFunctions.Values.eTypeData.etdDouble)) Then
				mlngNotenum = lclsObject.nNotenum
			Else
				mlngNotenum = Request.QueryString.Item("nNotenum")
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			
			'+ Notas cláusulas de la póliza matriz
		Case "SCA2-A"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngNoteClause
			
			If mobjValues.StringToType(Request.QueryString.Item("nCopyNotenum"), eFunctions.Values.eTypeData.etdDouble) <= 0 And mobjValues.StringToType(Request.QueryString.Item("nOriginalNotenum"), eFunctions.Values.eTypeData.etdDouble) > 0 Then
				'+ Se genera la copia de la cláusula original  
				lclsObject = New eGeneralForm.Notes
				mlngNotenum = lclsObject.CopyNotes(mobjValues.StringToType(Request.QueryString.Item("nOriginalNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mintRectype, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			End If
			
			If mlngIndexNotenum > vbNullString Then
				Response.Write("<SCRIPT>UpdateNotenum2('CA022A'," & mlngNotenum & "," & mlngIndexNotenum & ")</" & "Script>")
			End If
			
			If Request.QueryString.Item("nMainAction") <> clngActionQuery Then
				'+ Se busca en las opciones de instalación si se permite editar el texto de las clausulas
				lclsObject = New eGeneral.Opt_System
				Call lclsObject.Find()
				mblnEnableEditDesc = lclsObject.sPrint_tx_c = "1"
			Else
				mblnEnableEditDesc = False
			End If
			'+ Notas Bienes asegurables de la póliza
		Case "SCA2-H"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngRiskNote
			lclsObject = New ePolicy.Certificat
			If lclsObject.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
				mlngNotenum = lclsObject.nNote_drisk
			End If
			
			'+ Notas para Siniestro
		Case "SCA2-J"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			
			'+ Datos de terceros (Subsecuencia de Casos)
		Case "SCA2-K"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			
			'+ Notas financiera del Cliente					
		Case "SCA2-L"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngFinancialNote
			
			'+ Notas de información del vehículo
		Case "SCA2-M"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngCarDescriptNote
			lclsObject = New ePolicy.Auto_db
			If lclsObject.Find_db1(Request.QueryString.Item("sLicense_ty"), Request.QueryString.Item("sRegist")) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			
			'+ Notas de la solicitud de cheques para gastos fijos			
		Case "SCA2-I"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngCashBankNote
			
			'+ Notas de Cláusula/Descriptivo/Condición Especial			
		Case "SCA2-G"
			lclsObject = New eProduct.Tab_Clause
			If lclsObject.Find_Exist(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nClause"), eFunctions.Values.eTypeData.etdDouble), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClauseNote
			
			'+ Notas de Información general de la cobertura			
		Case "SCA2-Y"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngCovertextNote
			
			'+ Notas de coberturas
		Case "SCA2-F"
			lclsObject = New ePolicy.Policy
			With lclsObject
				If .Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), True) Then
					mlngNotenum = lclsObject.nNote_cover
				End If
			End With
			lclsObject = Nothing
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngCovertextNote
			
			'+ Comentarios de la póliza        						
		Case "SCA2-3"
			lclsObject = New ePolicy.Policy
			If lclsObject.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), True) Then
				mlngNotenum = lclsObject.nNote_comme
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngPolicyNote
			
			'+ Anexos de la póliza        						
		Case "SCA2-4"
			lclsObject = New ePolicy.Policy
			If lclsObject.Find(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), True) Then
				mlngNotenum = lclsObject.nNote_adend
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngPolicyNote
			
			'+ Notas asociadas a endosos (policy_his)
		Case "SCA2-810"
			lclsObject = New ePolicy.Policy_his
			If lclsObject.Find_Policy_his_nNotenum(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngPolicyNote
			
			'+ Notas de las rutas aseguradas	
		Case "SCA2-T"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngNoteTransp
			
			'+ Notas para la rutina de Asientos Contables
		Case "SCA2-O"
			lclsObject = New eLedGe.Acc_transa
			If lclsObject.Find(Session("nLedCompan"), Session("nVoucher")) Then
				mlngNotenum = lclsObject.nNotenum
			Else
				mlngNotenum = 0
			End If
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngNoteLedUpd
			
			'+ Notas Historia clínica
		Case "SCA2-N"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClinicHistor
			
			'+ Observaciones de una propuesta
		Case "SCA2-808"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngNoteObsPropo
			
			'+ Evaluaciones restringidas
		Case "SCA804"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngNoteEvaluac
			
			'+ Recargos/Descuentos por asegurado
		Case "SCA2-X"
                mintRectype = eGeneralForm.Notes.eTypeNotes.clngRecDiscNote
                'mintRectype = clngRecDiscNote
			
			'+ Notas Solicitud de ordenes de servicio					
		Case "SCA2-W"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngProfOrdNote
			
			'+ Notas secuencia de Ordenes de servicio
		Case "SCA649"
			lclsObject = New eClaim.Prof_ord
			If lclsObject.Find_nServ(Session("nServ_order")) Then
				mlngNotenum = lclsObject.nNotenum
			End If
			mintRectype = 31
			
			'+ Notas Documentos Solicitados
		Case "SCA2-818"
			mintRectype = 33
			
			'+ Notas Detalle de Artículos
		Case "SCA2-B"
			mintRectype = 11
			
			'+ Daños ocurridos al tercero en siniestro
		Case "SCA2-6"
			mintRectype = 23
			
			'+ Condiciones de renovación
		Case "SCA2-2"
			mintRectype = 10
			
			'+ Beneficiarios en texto libre
		Case "SCA2-1"
			lclsObject = New ePolicy.Certificat
			With lclsObject
				If .Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), 0, True) Then
					mlngNotenum = lclsObject.nNote_Benef
				End If
			End With
			If mlngNotenum = eRemoteDB.Constants.intNull Or mlngNotenum = 0 Then
				lclsObject1 = New ePolicy.Policy
				If lclsObject1.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), True) Then
					mlngNotenum = lclsObject1.nNote_Benef
				End If
				lclsObject1 = Nothing
			End If
			
			lclsObject = Nothing
			mintRectype = 3
			
			'+ Declaración de Salud Simple
		Case "SCA2-34"
			mintRectype = 34
			
			'+ Notas Propuestas de Siniestros 					
		Case "SCA2-PS"
			mintRectype = eGeneralForm.Notes.eTypeNotes.clngClaimNote
			
		Case Else
			mintRectype = 0
			
	End Select
	
	'+ Se eliminan los objetos.
	lclsObject = Nothing
	
	If mobjValues.Stringtotype(mlngNotenum,eFunctions.Values.eTypeData.etdLong) <= 0 Then
		mlngNotenum = 0
	End If
	
	'+ Se busca el nombre del usuario que maneja la forma
	With Response
		.Write("<SCRIPT>")
		.Write("  var nUsercode = " & Session("nUsercode") & "; ")
		.Write("  var sCliename = """ & mstrUserName & """;")
		.Write("  var nRectype = " & mintRectype & ";")
		.Write("</" & "Script>")
	End With
	
	Call insDefineHeader()
	
	If Request.QueryString.Item("Type") <> "PopUp" Then
		Call insreaNotes()
	Else
		Call insreaNotesUpd()
	End If
	
End Sub

'% insDefineHeader : Configura las columnas del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------   
	
	mobjGrid.sCodisplPage = "sca002"
	
	'+ Si la acción es consulta no se establece la propiedad ActionQuery sobre el objeto del
	'+ grid con la variable de sesión bquery, ya que es necesario que aparezcan los links
        '+ sobre las notas para lograr acceder a su descripción.
        If Request.QueryString.Item("sCodispl") = "SCA2-8" Then
            If Session("nTransaction") = eClaim.Claim_win.eClaimTransac.clngClaimQuery Then
                mobjGrid.ActionQuery = True
            Else
                mobjGrid.bOnlyForQuery = False
                Session("bQuery") = False
            End If
        Else
            If Not Request.QueryString.Item("nMainAction") = eFunctions.Menues.TypeActions.clngActionQuery Then
                mobjGrid.ActionQuery = Session("bQuery")
            Else
                mobjGrid.bOnlyForQuery = False
            End If
        End If
	
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddNumericColumn(19653, GetLocalResourceObject("tcnNotenumColumnCaption"), "tcnNotenum", 4, mlngNotenum,  , GetLocalResourceObject("tcnNotenumColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddNumericColumn(40558, GetLocalResourceObject("tcnConsecColumnCaption"), "tcnConsec", 4, "",  , GetLocalResourceObject("tcnConsecColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddTextColumn(40560, GetLocalResourceObject("tctDescriptColumnCaption"), "tctDescript", 60, "",  , GetLocalResourceObject("tctDescriptColumnToolTip"))
'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
		.AddDateColumn(40562, GetLocalResourceObject("tcdCompdateColumnCaption"), "tcdCompdate", mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate), , GetLocalResourceObject("tcdCompdateColumnToolTip"), , , , True)
		.AddDateColumn(40563, GetLocalResourceObject("tcdNulldateColumnCaption"), "tcdNulldate",  ,  , GetLocalResourceObject("tcdNulldateColumnToolTip"))
		.AddNumericColumn(40559, GetLocalResourceObject("nUsercodeColumnCaption"), "nUsercode", 4, Session("nUsercode"),  , GetLocalResourceObject("nUsercodeColumnToolTip"),  ,  ,  ,  ,  , True)
		.AddTextColumn(40561, GetLocalResourceObject("sClienameColumnCaption"), "sCliename", 40, mstrUserName,  , GetLocalResourceObject("sClienameColumnToolTip"),  ,  ,  , True)

        If StyleNotes = "DEVEXPRESS" Then
            .AddTextAreaColumn(19655, GetLocalResourceObject("tcttDs_textColumnCaption"), "tcttDs_text", "", 24, 80, , ,True)
        Else 
            .AddTextAreaColumn(19655, GetLocalResourceObject("tcttDs_textColumnCaption"), "tcttDs_text", "", 11, 63)
        End If

		.AddHiddenColumn("nRectype", mintRectype)
		.AddHiddenColumn("sCodispl", Request.QueryString.Item("sCodispl"))
		.AddHiddenColumn("sOnSeq", Request.QueryString.Item("sOnSeq"))
		.AddHiddenColumn("nClause", Request.QueryString.Item("nClause"))
		.AddHiddenColumn("nID", Request.QueryString.Item("nID"))
		.AddHiddenColumn("sLicense_ty", Request.QueryString.Item("sLicense_ty"))
		.AddHiddenColumn("sRegist", Request.QueryString.Item("sRegist"))
		If LCase(Request.QueryString.Item("Type")) <> "popup" Then
			mobjGrid.Columns("tcnNotenum").GridVisible = False
			mobjGrid.Columns("tcttDs_text").GridVisible = False
		Else
			mobjGrid.Columns("tcttDs_text").GridVisible = True
		End If
            
        If StyleNotes = "DEVEXPRESS" Then
            mobjGrid.Columns("tcttDs_text").Opacity = 0        
        End If
            
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = Request.QueryString.Item("sCodispl")
		.Codisp = "SCA002"
		.DeleteButton = False
		.nMainAction = Request.QueryString.Item("nMainAction")
		.Columns("tctDescript").EditRecord = True
        If StyleNotes = "DEVEXPRESS" Then
            .MoveRecordScript = "htmlEditor.SetHtml(self.document.forms[0].tcttDs_text.value);"        
        End If
		'+ Si el tipo de acción es registrar se permite mostrar el botón de agregar
            If Request.QueryString.Item("nMainAction") <> eFunctions.Menues.TypeActions.clngActionQuery Then
                .AddButton = True
                .Columns("Sel").GridVisible = True
                .sEditRecordParam = "sOnSeq=" & Request.QueryString.Item("sOnSeq") & "&nClause=" & Request.QueryString.Item("nClause") & "&nNotenum=" & mlngNotenum & "&nIndexNotenum=" & mlngIndexNotenum & "&nID=" & Request.QueryString.Item("nID") & "&sLicense_ty=" & Request.QueryString.Item("sLicense_ty") & "&sRegist=" & Request.QueryString.Item("sRegist") & "&sCodisplOri=" & Request.QueryString.Item("sCodisplOri")
                '+ Parámetros del registro a eliminar.                    
                .sDelRecordParam = "nNotenum=' + marrArray[lintIndex].tcnNotenum + '&nConsec=' + marrArray[lintIndex].tcnConsec + '"
                .EditRecordQuery = mobjGrid.ActionQuery
            Else
                .AddButton = False
                .Columns("Sel").GridVisible = False
                .EditRecordQuery = True
			
            End If
		
		'+ Tamaño de la ventana popup
        If StyleNotes = "DEVEXPRESS" Then
		    .Width = 850
		    .Height = 750
        Else                 
		    .Width = 650
		    .Height = 530
        End If
            
		.Top = 5
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
End Sub

'% insreaNotes: Lee las notas asociadas a un ente
'----------------------------------------------------------------------------
Private Sub insreaNotes()
	'--------------------------------------------------------------------------------------------
	'- Variable para almacenar modo de accion mientras se cambia temporalmente
	Dim lblnQuery As Object
	'- Clase y coleccion para manejo de notas
	Dim lclsNote As eGeneralForm.Notes
	Dim lcolNotes As eGeneralForm.Notess
	
	lcolNotes = New eGeneralForm.Notess
	
	'+ Se almacena el modo de ejecucion actual, para permitir edicion
	lblnQuery = mobjValues.ActionQuery
	mobjValues.ActionQuery = False
	
	With mobjGrid
		If CStr(Session("sOriginalForm")) <> vbNullString Then
			.AddButton = False
			.DeleteButton = False
			.ActionQuery = True
		End If
	End With
	'+ Busca la nota y los consecutivos
	If lcolNotes.Find(mobjValues.StringToType(mlngNotenum, eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write(mobjValues.HiddenControl("hddCountNote", lcolNotes.Count))
		If lcolNotes.Count > 0 Then
                If Request.QueryString.Item("nMainAction") <> eFunctions.Menues.TypeActions.clngActionQuery Then
                    mobjGrid.DeleteButton = True
                End If
			
			For	Each lclsNote In lcolNotes
				With mobjGrid
					.Columns("tcnConsec").DefValue = lclsNote.nConsec
					.Columns("tcnNotenum").DefValue = lclsNote.nNotenum
					.Columns("tctDescript").DefValue = lclsNote.sDescript
					.Columns("tcdCompdate").DefValue = lclsNote.dCompdate
					.Columns("tcdNulldate").DefValue = lclsNote.dNulldate
					.Columns("nUsercode").DefValue = lclsNote.nUsercode
					.Columns("sCliename").DefValue = lclsNote.sCliename
					.Columns("tcttDs_text").DefValue = lclsNote.tDs_text
					'+ Columnas ocultas                    
					.Columns("nRectype").DefValue = lclsNote.nRectype
					.Columns("sCodispl").DefValue = Request.QueryString.Item("sCodispl")
					.Columns("sOnSeq").DefValue = Request.QueryString.Item("sOnSeq")
					
					Response.Write(.DoRow)
				End With
			Next lclsNote
		End If
		Response.Write(mobjValues.HiddenControl("hddNoteNum", mlngNotenum))
	Else
		Response.Write(mobjValues.HiddenControl("hddNoteNum", "0"))
		Response.Write(mobjValues.HiddenControl("hddCountNote", "0"))
	End If
	Response.Write(mobjGrid.closeTable)

	'+ Exclusiones.    
	'+ Determina el tipo de página para establecer los botones de acción (Aceptar, Cancelar).
	'+ Excluye las ventana que poseen notas asociadas directamente al frame de la secuencia.
	If Request.QueryString.Item("sCodispl") <> "SCA2-9" And Request.QueryString.Item("sCodispl") <> "SCA2-8" And Request.QueryString.Item("sCodispl") <> "SCA2-F" And Request.QueryString.Item("sCodispl") <> "SCA2-3" And Request.QueryString.Item("sCodispl") <> "SCA2-H" And Request.QueryString.Item("sCodispl") <> "SCA2-5" And Request.QueryString.Item("sCodispl") <> "SCA649" And Request.QueryString.Item("sCodispl") <> "SCA2-1" And Request.QueryString.Item("sCodispl") <> "SCA2-S" Or (Request.QueryString.Item("sCodispl") = "SCA2-S" And Request.QueryString.Item("WindowType") = "PopUp") Or (Request.QueryString.Item("sCodispl") = "SCA2-9" And Request.QueryString.Item("WindowType") = "PopUp") Then
		
		If Request.QueryString.Item("WindowType") = "PopUp" Then
			
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

			
		End If
		If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 And Request.QueryString.Item("WindowType") = "PopUp" Then
			Response.Write(mobjValues.ButtonAcceptCancel("UpdateNotenum(self.document.forms[0].hddNoteNum.value)",  , False,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
		Else
			If Request.QueryString.Item("WindowType") = "PopUp" Then
				'+ sCodisplOri: indica el codispl que originó la transacción y es asignado a través de la propiedad sQueryString.
				Response.Write(mobjValues.ButtonAcceptCancel("UpdateNotenum(self.document.forms[0].hddNoteNum.value" & ",""" & Request.QueryString.Item("sCodisplOri") & """,""" & Request.QueryString.Item("nIndexNotenum") & """)",  , False))
			End If
		End If
		If Request.QueryString.Item("WindowType") = "PopUp" Then
			
Response.Write("		" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

			
		End If
	End If
	
	'+ Se retorna a modo ejecucion original    
	mobjValues.ActionQuery = lblnQuery
	
	lcolNotes = Nothing
	lclsNote = Nothing
End Sub

'% insreaNotesUpd : Permite realizar las actualizaciones sobre la nota en selección
'-------------------------------------------------------------------------------------------
Private Sub insreaNotesUpd()
	Dim clngActionQuery As String
	'-------------------------------------------------------------------------------------------
	'- Variables para menejo de clases    
	Dim lclsGeneralNotes As eGeneralForm.GeneralForm
	Dim lclsClient_win As eClient.ClientWin
	Dim lclsPolicy_Win As ePolicy.Policy_Win
	Dim lcolNotes As eGeneralForm.Notess
	
	lclsGeneralNotes = New eGeneralForm.GeneralForm
	
	'+ Borrar nota
	If Request.QueryString.Item("Action") = "Del" Then
		Response.Write(mobjValues.ConfirmDelete)
		
		'            Call lclsGeneralNotes.insPostGeneralNotes(.QueryString("sCodispl"),         With Request
		With lclsGeneralNotes
			
			.nserv_order = mobjValues.StringToType(Session("nServ_order"), eFunctions.Values.eTypeData.etdDouble)
			.sCertype = mobjValues.StringToType(Session("sCertype"), eFunctions.Values.eTypeData.etdDouble)
			.nBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
			.nProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
			.nPolicy = mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble)
			.nCertif = mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble)
			.dEffecdate = mobjValues.StringToDate(Session("dEffecdate"))
			
			Call .insPostGeneralNotes(Request.QueryString.Item("sCodispl"), "Delete", Session("sClient"), mobjValues.StringToType(Session("nClaim"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("Type"), mobjValues.StringToType(Request.QueryString.Item("nNotenum"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.QueryString.Item("nConsec"), eFunctions.Values.eTypeData.etdDouble))
		End With
		
		Select Case Request.QueryString.Item("sCodispl")
			'+ Notas de cliente			
			Case "SCA2-9"
				lcolNotes = New eGeneralForm.Notess
				If Not lcolNotes.Find(mobjValues.StringToType(Request.QueryString.Item("nNotenum"), eFunctions.Values.eTypeData.etdDouble)) Then
					If mobjValues.StringToType(lcolNotes.mAuxNotenum, eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
						lclsClient_win = New eClient.Clientwin
						If lclsClient_win.insUpdClient_win(Session("sClient"), Request.QueryString.Item("sCodispl"), "1") Then
							Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Client/ClientSeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</" & "Script>")
						End If
					End If
				End If
				'+ Nota de cobertura			    
			Case "SCA2-F"
				lcolNotes = New eGeneralForm.Notess
				If Not lcolNotes.Find(mobjValues.StringToType(Request.QueryString.Item("nNotenum"), eFunctions.Values.eTypeData.etdDouble)) Then
					If mobjValues.StringToType(lcolNotes.mAuxNotenum, eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
						lclsPolicy_Win = New ePolicy.Policy_Win
						If lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), "4") Then
							Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</" & "Script>")
						End If
						lclsPolicy_Win = Nothing
					End If
				End If
				
				'+ Beneficiarios en texto libre
			Case "SCA2-1"
				lcolNotes = New eGeneralForm.Notess
				If Not lcolNotes.Find(mobjValues.StringToType(Request.QueryString.Item("nNotenum"), eFunctions.Values.eTypeData.etdDouble)) Then
					If mobjValues.StringToType(lcolNotes.mAuxNotenum, eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull Then
						lclsPolicy_Win = New ePolicy.Policy_Win
						If lclsPolicy_Win.Add_PolicyWin(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sCodispl"), "1") Then
							Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</" & "Script>")
						End If
						lclsPolicy_Win = Nothing
					End If
				End If
			Case "SCA2-H"
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</" & "Script>")
			Case "SCA2-3"
				Response.Write("<SCRIPT>top.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "';</" & "Script>")
		End Select
		'End With
	End If
	
	lclsGeneralNotes = Nothing
	lclsClient_win = Nothing
	lcolNotes = Nothing
	
	'+ Actualizar nota
	If Request.QueryString.Item("Action") = "Update" Then
		If Request.QueryString.Item("nMainAction") = clngActionQuery Then
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valGeneralForm.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Not mblnEnableEditDesc, Request.QueryString.Item("Index")))
		Else
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valGeneralForm.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Session("bQuery"), Request.QueryString.Item("Index")))
		End If
		
		Response.Write("<SCRIPT>insDefUpdate(" & Request.QueryString.Item("nMainAction") & ");</" & "Script>")
	Else
		If Request.QueryString.Item("nMainAction") = clngActionQuery Then
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valGeneralForm.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Not mblnEnableEditDesc, Request.QueryString.Item("Index")))
		Else
			Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valGeneralForm.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), Session("bQuery"), Request.QueryString.Item("Index")))
		End If
	End If
	
	'+ Según la acción se actualizan los valores de la página luego de diseñada.
	If LCase(Request.QueryString.Item("Action")) = "add" Then
		Response.Write("<SCRIPT>insDefAdd();</" & "Script>")
	End If
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = new eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = new eFunctions.Values

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.33.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
'~End Body Block VisualTimer Utility

mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
lclsUsers = New eGeneral.Users

mstrUserName = lclsUsers.FindUserName(Session("nUsercode"))
lclsUsers = Nothing

'+ Obtiene el número de índice que corresponde con el número de nota
mlngIndexNotenum = Request.QueryString.Item("nIndexNotenum")

mobjValues.sCodisplPage = "sca002"

%>
<HTML>
<HEAD>

<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">  
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>

<%Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("sOnSeq") = "1" And Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy")))
	mobjMenu = Nothing
End If

%>
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 20 $|$$Date: 7/10/04 12.57 $$|$Author: Nvaplat60 $"
	
    var mintCount = -1
    var nMainAction = <%=Request.QueryString.Item("nMainAction")%>

// insDefAdd: Establece el estado de la forma cuando se agrega.
//--------------------------------------------------------------------------------------------
function insDefAdd(){
//--------------------------------------------------------------------------------------------
//- Variable para almacenar el consecutivo más alto existente en el grid
    var llngMax = 0
    
//+ Se genera el número consecutivo de la nota (el Nº consecutivo más alto +1)   
	for(var llngIndex = 0;llngIndex<top.opener.marrArray.length;llngIndex++)
	    if(top.opener.marrArray[llngIndex].tcnConsec>llngMax)
	        llngMax = top.opener.marrArray[llngIndex].tcnConsec

//+ Se asignan los valores a los campos de la página	
//+ Consecutivo
    self.document.forms[0].tcnConsec.value = ++llngMax;					            
}

//**% insDefUpdate: This function establishes the status of the form when it is updated.
//%	insDefUpdate: Establece el estado de la forma cuando se actualiza.
//-------------------------------------------------------------------------------------------
function insDefUpdate(nMAction){
//-------------------------------------------------------------------------------------------

//**+ The fields of rigor are disabled.
//+ Desabilita los campos de rigor.

    self.document.forms[0].tcnConsec.disabled=true;
    self.document.forms[0].tcdCompdate.disabled=true;
    self.document.forms[0].nUsercode.disabled=true;
    self.document.forms[0].sCliename.disabled=true;

//**+ According to the action all the fields of the form are disabled.
//+ Según la acción desabilita todos los campos de la forma.

    if(nMAction==401){
        self.document.forms[0].tctDescript.disabled=true;
        self.document.forms[0].tcttDs_text.disabled=true;
        self.document.forms[0].tcdNulldate.disabled=true;
        self.document.forms[0].chkContinue.disabled=true;
        self.document.forms[0].chkContinue.checked=false;
    }
}

//%	UpdateNotenum2: Actualiza el número de nota en la página principal.
//	Este función se diseño para el caso particular que presentaba la forma CA022A y CA022
//-------------------------------------------------------------------------------------------
function UpdateNotenum2(sCodispl,nNotenum, nIndex){
//-------------------------------------------------------------------------------------------
//- Variable para contener error    
    var lstrErr
    if(sCodispl=='CA022A'){
		if(typeof(nIndex)!='number'){
		   with(top.opener.document.forms[0]){
	            nOriginalNotenum.value = 0;
	            nCopyNotenum.value = nNotenum;
	            tcnNotenum.value = nNotenum;
	       }
	    }
        else{
            with(top.opener.document.forms[0]){
	            try{
	                hddNoteNum[nIndex].value = nNotenum;
	                tcnNotenum[nIndex].value = nNotenum;
	            }catch(lstrErr){
	                hddNoteNum.value = nNotenum;
	            }
	        }
	    }
	}
}    

//%	UpdateNotenum: Actualiza el número de nota en la página requerida.
//%      nNotenum: Contiene el número de la nota a actualizar osbre el control.
//-------------------------------------------------------------------------------------------
function UpdateNotenum(nNotenum, sCodispl, nIndex) {
//-------------------------------------------------------------------------------------------	
//- Variable para contener error
    var llngErr 
  	with(top.opener.document.forms[0]) {
//+ Se actualiza el número de la nota.
	    top.opener.document.btnNotenum.value = nNotenum;
        tcnNotenum.value = nNotenum;
        if(nNotenum>0){
			try{
 				if(top.opener.document.btnNotenum.src.indexOf("Big")!=-1)
 					top.opener.document.btnNotenum.src='/VTimeNet/Images/menu_transaction.png'
				else
 					top.opener.document.btnNotenum.src='/VTimeNet/Images/btnWNotes.png'
			}
			catch(llngErr){
				if (opener.top.fraFolder.document.btnNotenum[nIndex].src.indexOf("Big")!=-1)
					opener.top.fraFolder.document.btnNotenum[nIndex].src='/VTimeNet/Images/menu_transaction.png'
				else
					opener.top.fraFolder.document.btnNotenum[nIndex].src='/VTimeNet/Images/btnWNotes.png'
			}
		}
		else{
			try{
				if(top.opener.document.btnNotenum.src.indexOf("Big")!=-1)
					top.opener.document.btnNotenum.src='/VTimeNet/Images/menu_WONotes.png'
				else
					top.opener.document.btnNotenum.src='/VTimeNet/Images/btnWONotes.png'
			}
			catch(llngErr){
				if (opener.top.fraFolder.document.btnNotenum[nIndex].src.indexOf("Big")!=-1)
					opener.top.fraFolder.document.btnNotenum[nIndex].src='/VTimeNet/Images/menu_transaction.png'
				else
					opener.top.fraFolder.document.btnNotenum[nIndex].src='/VTimeNet/Images/btnWNotes.png'
			}
        }
//+ Se verifica si sobre la página más inmediata que llamó al frame existe un objeto de
//+ nombre "tctNote"; en caso de existir se actualiza sobre este objeto la descripción
//+ larga de la nota, en caso contrario es omitido este proceso.
        try{
            (marrArray!='')?tctNote.value = marrArray[0].tcttDs_text:txtNote.value ='';
        }
        catch(llngErr){}
    
        try {
            if (top.opener.CurrentIndex>=0)
                top.opener.opener.marrArray[opener.CurrentIndex].btnNotenum = nNotenum;
        }
        catch(llngErr){}
    }

//+ Si se asignó número de nota y si se trata de las ventanas CA022 o CA022A. Se invoca al método para actualizar la columna "Sel".
    if (nNotenum>0)
		if (sCodispl=='CA022' ||
		    sCodispl=='CA022A') 
		    opener.top.fraFolder.insUpdSel(true, nIndex);
		    
    window.close()
}    
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
   <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
   <FORM METHOD="POST" NAME="frmSCA002" ACTION="valGeneralForm.aspx?nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sOnSeq=<%=Request.QueryString.Item("sOnSeq")%>&nIndexNotenum=<%=mlngIndexNotenum%>" enctype="application/x-www-form-urlencoded">
   <%
Call insPreSCA002()
mobjValues = Nothing
mobjMenu = Nothing
mobjGrid = Nothing
%>
   </FORM> 
<%
    If Request.QueryString.Item("Type") = "PopUp" And  StyleNotes = "DEVEXPRESS" Then
%>        
    
  <form id="form1" runat="server">
    <div>
        <dxhe:ASPxHtmlEditor ID="htmlEditor" ClientInstanceName="htmlEditor" runat="server" Style="position:absolute; left:140px; top:237px;">
            <SettingsHtmlEditing EnterMode="Default"/>
            <Settings AllowHtmlView="false" AllowPreview="false" />
            <ClientSideEvents HtmlChanged="function (s, e) {self.document.forms[0].tcttDs_text.value = s.GetHtml();}" />
        </dxhe:ASPxHtmlEditor>
    </div>
    </form>
<%
    End If
%>
</BODY>
<%
    If Request.QueryString.Item("Type") = "PopUp" And StyleNotes = "DEVEXPRESS" Then
%> 
<SCRIPT>
    htmlEditor.SetHtml(self.document.forms[0].tcttDs_text.value);
</SCRIPT>
<%
    End If
%>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.33.47
    Call mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>

     