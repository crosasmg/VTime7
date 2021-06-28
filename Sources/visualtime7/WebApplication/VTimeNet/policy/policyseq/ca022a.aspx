<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de errores
Dim mobjError As eFunctions.Errors
'- Objeto para el manejo de menues    
Dim mobjMenu As eFunctions.Menues
'- Objeto para el manaejo del grid    
Dim mobjGrid As eFunctions.Grid

Dim mintGroup As String

'-Variable para indicar si se modifican las notas de las cláusulas (opciones de instalación)    
Dim mblnEnableEditDesc As Boolean


'% insDefineHeader : Configura las columnas del grid.
'---------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'---------------------------------------------------------------------------------------------
	Dim lclsOpt_system As eGeneral.Opt_system
	
	lclsOpt_system = New eGeneral.Opt_system
	Call lclsOpt_system.Find()
	
	mblnEnableEditDesc = (lclsOpt_system.sPrint_tx_c = "1")
	
	mobjGrid.ActionQuery = Session("bQuery")
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		.AddNumericColumn(100770, GetLocalResourceObject("tcnClauseColumnCaption"), "tcnClause", 10, "")
		.AddTextColumn(100771, GetLocalResourceObject("tctClauseColumnCaption"), "tctClause", 30, "")
		
		.AddCheckColumn(0, GetLocalResourceObject("chkType_ClauseColumnCaption"), "chkType_Clause", vbNullString,  ,  , "insChangeType(this)", Request.QueryString.Item("Action") <> "Add", GetLocalResourceObject("chkType_ClauseColumnToolTip"))
		
		If Not (Request.QueryString.Item("Type") = "PopUp" And Request.QueryString.Item("Action") = "Add") Then
			.AddTextColumn(0, GetLocalResourceObject("tctDoc_attachColumnCaption"), "tctDoc_attach", 45, CStr(eRemoteDB.Constants.strnull),  , GetLocalResourceObject("tctDoc_attachColumnToolTip"),  ,  ,  , True)
		End If
		
            .AddButtonColumn(0, GetLocalResourceObject("SCA2-AColumnCaption"), "SCA2-A", 0, True, Not mblnEnableEditDesc, , , , , "btnNotenum")
		'.AddCheckColumn 0,"Modificable","chkModified","","1","2",,True,"Indica si producto permite modificar texto de cláusula"
		
		'+Se crean campos ocultos ya que campos anteriores quedan como etiquetas en la matriz, 
		'+por lo que no son enviados por el formulario a la página de validaciones
		.AddHiddenColumn("hddnSelClause", vbNullString)
		.AddHiddenColumn("hddnClause", vbNullString)
		.AddHiddenColumn("hddNoteNum", vbNullString)
		.AddHiddenColumn("hddNoteNum_Prod", vbNullString)
		.AddHiddenColumn("hddCheckFile", "2")
		
	End With
	
	'+ Variable para controlar la actualización de la información de manera puntual (desde el botón de la ventana)
	Response.Write(mobjValues.HiddenControl("hddbPuntual", CStr(False)))
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "CA022A"
		.DeleteButton = False
		.AddButton = False
		.Width = 290
		.Height = 270
		.Columns("Sel").GridVisible = Not .ActionQuery
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		
		.Splits_Renamed.AddSplit(0, "", 2)
		.Splits_Renamed.AddSplit(0, GetLocalResourceObject("2ColumnCaption"), 2)
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
	lclsOpt_system = Nothing
End Sub

'% insPreCA022A : Carga los datos iniciales de la forma
'---------------------------------------------------------------------------------------------
Private Sub insPreCA022A()
	'---------------------------------------------------------------------------------------------
	Dim lintIndex As Integer
	Dim lclsClausesTemp As Object
	
	Dim lclsClauses As ePolicy.Claus_co_gp
	lclsClauses = New ePolicy.Claus_co_gp
	
	lclsClausesTemp = lclsClauses.insPreCA022A(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), mobjValues.StringToType(Request.QueryString.Item("nGroup"), eFunctions.Values.eTypeData.etdDouble), Session("bQuery"))
	mintGroup = mobjValues.StringToType(CStr(lclsClauses.nGroup), eFunctions.Values.eTypeData.etdDouble)
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"" COLS=4>" & vbCrLf)
Response.Write("		<TR>")

	
	'+ Si las especificaciones son por grupo
	If lclsClauses.sTyp_clause = "3" Then
		
Response.Write("" & vbCrLf)
Response.Write("		    <TD WIDTH=""22%""><LABEL ID=""13043"">" & GetLocalResourceObject("valGroupCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		    <TD WIDTH=""30%"">")

		
		With mobjValues.Parameters
			.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		End With
		Response.Write(mobjValues.PossiblesValues("valGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, mintGroup, True,  ,  ,  ,  , "if(mintChange!=this.value) ChangeOption(this);", lclsClauses.nCountGroup <= 1 Or lclsClauses.sClausePreError <> vbNullString,  , GetLocalResourceObject("valGroupToolTip")))
		Response.Write("<SCRIPT> mintChange = '" & mintGroup & "'; </" & "Script>")
		
Response.Write("" & vbCrLf)
Response.Write("			</TD>")

		
		'+ Si no se trata de consulta
		If Not mobjValues.ActionQuery Then
			If lclsClauses.bFindGroup And lclsClauses.nCountGroup > 1 Then
				Response.Write("<TD COLSPAN=""5"">" & "</TD>")
				Response.Write("<TD WIDTH=""5%"">" & mobjValues.AnimatedButtonControl("btn_Apply", "/VTimeNet/images/btnAcceptOff.png", GetLocalResourceObject("btn_ApplyToolTip"),  , "insAccept()",  , 10) & "</TD>")
			End If
		End If
	Else
		Response.Write(mobjValues.HiddenControl("valGroup", vbNullString))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	
	'+ Si no se detectó ningún error
	If lclsClauses.sClausePreError = vbNullString Then
		If lclsClausesTemp.Count >= 0 Then
			With lclsClausesTemp
                    'For lintIndex = 0 To .Count
                    For lintIndex = 0 To .Count 
                        If .Item(lintIndex) Then
                            'mobjGrid.Columns("Sel").OnClick = "insUpdateSelection(this);"
                            mobjGrid.Columns("Sel").OnClick = "insUpdateSelection(this," & CStr(lintIndex) & ")"
						
                            mobjGrid.Columns("Sel").Checked = .nSel
                            mobjGrid.Columns("btnNotenum").Disabled = False
                            mobjGrid.Columns("hddnSelClause").DefValue = .nSel
                            mobjGrid.Columns("tcnClause").DefValue = .nClause
                            mobjGrid.Columns("hddnClause").DefValue = .nClause
                            mobjGrid.Columns("tctClause").DefValue = .sDescriptD
						
                            '+ Se asigna mismo numero de nota existente para compararlo si se genera uno nuevo
                            mobjGrid.Columns("btnNotenum").nCopyNotenum = .nNotenumP
						
                            '+ Indice del arreglo para campo de notas
                            mobjGrid.Columns("btnNotenum").nIndexNotenum = lintIndex
						
                            '+ Las clausulas quedan inicialmente con el número de nota definido en el producto
                            '+ hasta que se asigna un nuevo valor (al editar la nota)
                            If .nNotenumP = eRemoteDB.Constants.intNull Then
                                mobjGrid.Columns("btnNotenum").nOriginalNotenum = .nNotenumS
                                mobjGrid.Columns("hddNoteNum").DefValue = .nNotenumS
                                mobjGrid.Columns("hddNoteNum_Prod").DefValue = .nNotenumS
                                '+ Se asigna numero de nota existente en la clausula de póliza/grupo
                                mobjGrid.Columns("btnNotenum").nNotenum = .nNotenumS
                            Else
                                mobjGrid.Columns("hddNoteNum").DefValue = .nNotenumP
                                mobjGrid.Columns("hddNoteNum_Prod").DefValue = .nNotenumS
                                '+ Se asigna numero de nota existente en la clausula de póliza/grupo
                                mobjGrid.Columns("btnNotenum").nNotenum = .nNotenumP
                                mobjGrid.Columns("btnNotenum").nOriginalNotenum = .nNotenumP
                            End If
						
                            '                        If Not mblnEnableEditDesc Then
                            '							If mobjGrid.Columns("hddNoteNum").DefValue <= 0 then
                            '								mobjGrid.Columns("btnNotenum").Disabled = True
                            '							End If
                            '                        End If
						
                            mobjGrid.sQueryString = "sAllowEdit=" & .sModified
                            'mobjGrid.Columns("chkModified").Checked = .sModified
						
                            ' mobjGrid.Columns("chkType_Clause").Checked = .sType_clause
                            If .sType_clause = "1" Then
                                mobjGrid.Columns("chkType_Clause").Checked = CShort("1")
                            Else
                                mobjGrid.Columns("chkType_Clause").Checked = CShort("2")
                            End If
						
                            mobjGrid.Columns("tctDoc_attach").DefValue = .sDoc_attach
						
                            Response.Write(mobjGrid.DoRow)
                        End If
                    Next
                End With
		End If
	Else
		Response.Write(lclsClauses.sClausePreError)
	End If
	Response.Write(mobjGrid.closeTable)
	lclsClauses = Nothing
	lclsClausesTemp = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA022A")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjError = New eFunctions.Errors
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjError.sSessionID = Session.SessionID
mobjError.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjGrid = New eFunctions.Grid
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjGrid.sSessionID = Session.SessionID
mobjGrid.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>   
//-Variable para almacenar valor de grupo    
    var mintChange = '';

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"
    
//%	ChangeOption: Actualiza ventana para mostrar las cláusulas del grupo colectivo seleccionado
//-------------------------------------------------------------------------------------------
function ChangeOption(Option){
//-------------------------------------------------------------------------------------------
//+Se recarga la ventana si cambia grupo
    mintChange = Option.value;
    self.document.location.href = '/VTimeNet/Policy/PolicySeq/CA022A.aspx?sCodispl=CA022A&nGroup='+Option.value + '&sOnSeq=<%=Request.QueryString.Item("sOnSeq")%>&nMainAction=<%=Request.QueryString.Item("nMainAction")%>'
}

//%insUpdateSelection:Actualiza variable oculta que indica si clausula esta seleccionada
//-------------------------------------------------------------------------------------------
function insUpdateSelection(lobj,lintIndex){
    //-------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
        if (typeof (hddnSelClause[lobj.value]) == 'undefined') {
            if (lobj.checked) hddnSelClause.value = "1";
            else hddnSelClause.value = "2";
        }
        else {
            if (lobj.checked) hddnSelClause[lobj.value].value = "1";
            else hddnSelClause[lobj.value].value = "2";
        }
    }
}

//% insAccept: Se acpta la secuencia en tratamiento 
//------------------------------------------------------------------------------------------
function insAccept(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]) {
		self.document.forms[0].hddbPuntual.value = true;
	}
	top.frames['fraHeader'].ClientRequest(390,2);
}

//%insChangeType: se controla el cambio de tipo de cláusula según archivo
//--------------------------------------------------------------------------------------------------
function insChangeType(Field){
//--------------------------------------------------------------------------------------------------
    with(self.document.forms[0]){
    
        if(Field.checked){
            hddCheckFile.value = "1";
            tctFile.disabled = false;
            btnNotenum.disabled = true;
        } else {
            hddCheckFile.value = "2";
            tctFile.value = "";
            tctFile.disabled = true;
            btnNotenum.disabled = false;
        }
    }
}
</SCRIPT>
    <%=mobjValues.StyleSheet()%>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
<FORM METHOD="post" ID="FORM" NAME="frmCA022A" ACTION="valPolicySeq.aspx?X=1" >
<%
Call insDefineHeader()

Call insPreCA022A()

mobjError = Nothing
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM>
</BODY>
<HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA022A")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




