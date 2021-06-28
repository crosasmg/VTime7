<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del grid de la página
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Variable para identificar si se puede o no ingresar información la ventana
Dim mblnValid As Boolean
Dim mstrOnSeq As String

Dim mstrCertype As Object
Dim mlngBranch As Object
Dim mlngProduct As Object
Dim mlngPolicy As Object
Dim mlngCertif As Object
Dim mdtmEffecdate As Object


'% insDefineHeader: se definen las propiedades del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = Request.QueryString.Item("sCodispl")
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnIDColumnCaption"), "tcnID", 4, vbNullString,  , GetLocalResourceObject("tcnIDColumnToolTip"),  ,  ,  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeObservationColumnCaption"), "cbeObservation", "Table5573", eFunctions.Values.eValuesType.clngComboType, CStr(0),  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeObservationColumnToolTip"))
        If Request.QueryString.Item("Type") <> "PopUp" Then    
            Call .AddTextColumn(0, GetLocalResourceObject("tctCompdateColumnCaption"), "tctCompdate", 20, String.Empty, , GetLocalResourceObject("tcdCompdateColumnToolTip"), ,  , ,True)
        End If        
		Call .AddButtonColumn(0, GetLocalResourceObject("SCA2-808ColumnCaption"), "SCA2-808", eRemoteDB.Constants.intNull,  , Request.QueryString.Item("Type") <> "PopUp")
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.Codispl = "CA748"
		.ActionQuery = mobjValues.ActionQuery
		.Columns("cbeObservation").EditRecord = True
		.Height = 220
		.Width = 450
		.nMainAction = mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
		.Columns("Sel").GridVisible = Not .ActionQuery
		.sEditRecordParam = "sCertype=" & mstrCertype & "&nBranch=" & mlngBranch & "&nProduct=" & mlngProduct & "&nPolicy=" & mlngPolicy & "&nCertif=" & mlngCertif & "&dEffecdate=" & mdtmEffecdate & "&sOnSeq=" & mstrOnSeq
		.sDelRecordParam = "sCertype=" & mstrCertype & "&nBranch=" & mlngBranch & "&nProduct=" & mlngProduct & "&nPolicy=" & mlngPolicy & "&nCertif=" & mlngCertif & "&dEffecdate=" & mdtmEffecdate & "&nID='+ marrArray[lintIndex].tcnID + '"
		.AddButton = mblnValid
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
	End With
End Sub

'% insPreCA748: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA748()
	'--------------------------------------------------------------------------------------------
	Dim mcolObs_proposal As ePolicy.Obs_proposals
	Dim lclsObs_proposal As Object
	
	mcolObs_proposal = New ePolicy.Obs_proposals
	
	'+ Se buscan los datos asociados a la póliza/certificado
	If mcolObs_proposal.Find(mobjValues.StringToType(mstrCertype, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble)) Then
		For	Each lclsObs_proposal In mcolObs_proposal
			With mobjGrid
				.Columns("tcnId").DefValue = lclsObs_proposal.nId
				.Columns("cbeObservation").DefValue = lclsObs_proposal.nObservation
                .Columns("tctCompdate").DefValue = lclsObs_proposal.dCompdate.ToString
				.Columns("btnNotenum").nNotenum = mobjValues.StringToType(lclsObs_proposal.nNotenum, eFunctions.Values.eTypeData.etdDouble)
				Response.Write(.DoRow)
			End With
		Next lclsObs_proposal
	End If
	Response.Write(mobjGrid.closeTable())
	
	If CDbl(Request.QueryString.Item("sOnSeq")) <> 1 Then
		If Request.QueryString.Item("sCodispl") <> "CA748" Then
			Response.Write(mobjValues.ButtonAcceptCancel("", "top.close()", False,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
		End If
	End If
	
	mcolObs_proposal = Nothing
End Sub

'% insPreCA748Upd: Se realiza el manejo de la ventana PopUp asociada al grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA748Upd()
	'--------------------------------------------------------------------------------------------
	Dim lclsObs_proposal As ePolicy.obs_proposal
	Dim lclsRefresh As ePolicy.ValPolicySeq
	
	With Request
		'+ Si la acción es eliminar    
		If Request.QueryString.Item("Action") = "Del" Then
			lclsObs_proposal = New ePolicy.obs_proposal
			Response.Write(mobjValues.ConfirmDelete())
			'+ Se elimina el registro de la tabla
			Call lclsObs_proposal.inspostCA748(.QueryString.Item("Action"), mstrCertype, mobjValues.StringToType(mlngBranch, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngProduct, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngPolicy, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mlngCertif, eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(mdtmEffecdate, eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nID"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Constants.intNull, eRemoteDB.Constants.intNull, mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
			
			lclsRefresh = New ePolicy.ValPolicySeq
			Response.Write(lclsRefresh.RefreshSequence("CA748", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Session("sBrancht"), Session("sPolitype"), "No"))
			lclsRefresh = Nothing
		Else
			Response.Write(mobjValues.HiddenControl("hddCertype", mstrCertype))
			Response.Write(mobjValues.HiddenControl("hddBranch", mlngBranch))
			Response.Write(mobjValues.HiddenControl("hddProduct", mlngProduct))
			Response.Write(mobjValues.HiddenControl("hddPolicy", mlngPolicy))
			Response.Write(mobjValues.HiddenControl("hddCertif", mlngCertif))
			Response.Write(mobjValues.HiddenControl("hddEffecdate", mdtmEffecdate))
		End If
		Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "valPolicySeq.aspx", "CA748", .QueryString.Item("nMainAction"), mobjValues.ActionQuery, CShort(.QueryString.Item("Index"))))
	End With
	
	'+ Si la ación es agregar    
	If Request.QueryString.Item("Action") = "Add" Then
		Response.Write("<SCRIPT>insDefAdd()</" & "Script>")
	End If
	lclsObs_proposal = Nothing
End Sub

'% insvalPage: se valida la existencia de la página dentro de la secuencia
'--------------------------------------------------------------------------------------------
Private Function insvalPage() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lclsErrors As eFunctions.Errors
	
	insvalPage = True
	
	lclsErrors = New eFunctions.Errors
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	lclsErrors.sSessionID = Session.SessionID
	lclsErrors.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	'+ La ventana no debe aparecer si se trata de la póliza matriz
	If (CStr(Session("sPolitype")) = "2" Or CStr(Session("sPolitype")) = "3") And mlngCertif = "0" Then
		
		'insvalPage = False
		'Call insDefineHeader()
		'Response.Write mobjGrid.closeTable 
		
		'Response.Write lclsErrors.ErrorMessage("CA748",1402,,,,True)
	End If
	lclsErrors = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA748")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

Session("sOriginalForm") = ""
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Request.QueryString.Item("nMainAction") = "401"

If Request.QueryString.Item("sOnSeq") = "2" Then
	mstrOnSeq = "2"
Else
	mstrOnSeq = "1"
End If

With Request
	mstrCertype = .QueryString.Item("sCertype")
	mlngBranch = .QueryString.Item("nBranch")
	mlngProduct = .QueryString.Item("nProduct")
	mlngPolicy = .QueryString.Item("nPolicy")
	mlngCertif = .QueryString.Item("nCertif")
	mdtmEffecdate = .QueryString.Item("dEffecdate")
End With

If mstrCertype = "" Then
	mstrCertype = Session("sCertype")
End If
If mlngBranch = "" Then
	mlngBranch = Session("nBranch")
End If
If mlngProduct = "" Then
	mlngProduct = Session("nProduct")
End If
If mlngPolicy = "" Then
	mlngPolicy = Session("nPolicy")
End If
If mlngCertif = "" Then
	mlngCertif = Session("nCertif")
End If
If mdtmEffecdate = "" Then
	mdtmEffecdate = Session("dEffecdate")
End If
%>
<HTML>
<HEAD>
<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 3 $|$$Date: 15/10/03 16:49 $"
    // insDefAdd: Establece el valor de los campos de la página cuando se agrega un registro.
    //--------------------------------------------------------------------------------------------
    function insDefAdd() {
        //--------------------------------------------------------------------------------------------
        //- Se define la variable para almacenar el consecutivo más alto existente en el grid



            self.document.forms[0].tcnID.value = top.opener.marrArray.length + 1;

        }
</SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	If mstrOnSeq = "1" Then
		Response.Write(mobjMenu.setZone(2, "CA748", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
		mobjMenu = Nothing
		Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
	Else
		Response.Write("<SCRIPT>var nMainAction ='" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
	End If
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA748" ACTION="valPolicySeq.aspx?sOnSeq=<%=mstrOnSeq%>">
    <%Response.Write(mobjValues.ShowWindowsName("CA748", Request.QueryString.Item("sWindowDescript")))

mblnValid = insvalPage()

If mblnValid Then
	Call insDefineHeader()
	If Request.QueryString.Item("Type") = "PopUp" Then
		Call insPreCA748Upd()
	Else
		Call insPreCA748()
	End If
End If
mobjValues = Nothing
mobjGrid = Nothing
%>
</FORM> 
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA748")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>