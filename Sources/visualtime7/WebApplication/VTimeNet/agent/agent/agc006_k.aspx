<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eAgent" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.11.55
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Se define la variable mobjGrid para el manejo del Grid de la ventana	
Dim mobjGrid As eFunctions.Grid

'- Objeto para el manejo de las zonas de la página
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader. Definición de columnas del GRID
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.56
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "agc006_k"
	Call mobjGrid.SetWindowParameters(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), Request.QueryString.Item("nWindowTy"))
	
	'+ Se definen las columnas del grid
	With mobjGrid.Columns
		Call .AddPossiblesColumn(40033, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Interm_typ", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeTypeColumnToolTip"))
		Call .AddClientColumn(CInt("0"), GetLocalResourceObject("tctClientCodeColumnCaption"), "tctClientCode", "",  , GetLocalResourceObject("tctClientCodeColumnToolTip"))
		Call .AddNumericColumn(40036, GetLocalResourceObject("tcnAgentColumnCaption"), "tcnAgent", 10, "",  , GetLocalResourceObject("tcnAgentColumnToolTip"), False,  ,  ,  ,  , False)
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(40038, GetLocalResourceObject("tctAgentNameColumnCaption"), "tctAgentName", 30, "",  , GetLocalResourceObject("tctAgentNameColumnCaption"),  ,  ,  , False)
		End If
		
		Call .AddNumericColumn(40037, GetLocalResourceObject("tcnAgentOrgColumnCaption"), "tcnAgentOrg", 10, "",  , GetLocalResourceObject("tcnAgentOrgColumnToolTip"), False, 0,  ,  ,  , False)
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddTextColumn(40039, GetLocalResourceObject("tctOrgNameColumnCaption"), "tctOrgName", 30, "",  , GetLocalResourceObject("tctOrgNameColumnToolTip"),  ,  ,  , False)
		End If
		
		Call .AddPossiblesColumn(40034, GetLocalResourceObject("cbeOfficeColumnCaption"), "cbeOffice", "table9", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeOfficeColumnToolTip"))
		Call .AddPossiblesColumn(40035, GetLocalResourceObject("cbeStatusColumnCaption"), "cbeStatus", "table200", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeStatusColumnToolTip"))
		
		Call .AddDateColumn(0, GetLocalResourceObject("tcdCommidateColumnCaption"), "tcdCommidate",  ,  , GetLocalResourceObject("tcdCommidateColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tctAnullColumnCaption"), "tctAnull",  ,  , GetLocalResourceObject("tctAnullColumnToolTip"))
		
		If Request.QueryString.Item("Type") <> "PopUp" Then
			Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddressColumnCaption"), "cmdAddress", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddressColumnToolTip"))
		End If
		
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "AGC006"
		.Codisp = "AGC006_K"
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
		.Height = 400
		.Width = 450
		.Top = 10
		.Left = 10
		.bOnlyForQuery = True
		.bCheckVisible = False
		.Columns("Sel").GridVisible = False
	End With
End Sub


'% insPreAGC006: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreAGC006()
	'--------------------------------------------------------------------------------------------
	Dim lclsIntermedia As Object
	Dim lcolIntermedias As eAgent.Intermedias
    Dim nrow As Integer
    Dim nfirst As Integer
	lcolIntermedias = New eAgent.Intermedias
	
	
	Dim lintIndex As Byte
	
	With Response
		.Write(mobjValues.HiddenControl("hddFirstRecord", Request.QueryString.Item("nFirstRecord")))
		.Write(mobjValues.HiddenControl("hddQueryString", Request.Params.Get("Query_String")))
	End With
        nrow = IIf(mobjValues.StringToType(Request.QueryString.Item("nrow"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull, 1, mobjValues.StringToType(Request.QueryString.Item("nrow"), eFunctions.Values.eTypeData.etdDouble))
        nfirst = IIf(mobjValues.StringToType(Request.QueryString.Item("nfirst"), eFunctions.Values.eTypeData.etdDouble) = eRemoteDB.Constants.intNull, 0, mobjValues.StringToType(Request.QueryString.Item("nfirst"), eFunctions.Values.eTypeData.etdDouble))
	    
        If nfirst > 0 Then
            If lcolIntermedias.FindAGC006(mobjValues.StringToType(Request.QueryString.Item("sType"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sClient"), Request.QueryString.Item("sAgent"), Request.QueryString.Item("sAgentName"), mobjValues.StringToType(Request.QueryString.Item("sStatus"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sAgentOrg"), Request.QueryString.Item("sAgentOrgName"), mobjValues.StringToType(Request.QueryString.Item("sOffice"), eFunctions.Values.eTypeData.etdDouble), Request.QueryString.Item("sDateAnull"), Request.QueryString.Item("sCommidate"), nrow) Then
		
                For Each lclsIntermedia In lcolIntermedias
                    With mobjGrid
                        .Columns("cbeType").DefValue = lclsIntermedia.nIntertyp
				
                        .Columns("tctClientCode").DefValue = lclsIntermedia.sClient
                        .Columns("tctClientCode").Digit = lclsIntermedia.sClientDig
                        .Columns("tctClientCode").Descript = " "
				
                        .Columns("tcnAgent").DefValue = lclsIntermedia.nIntermed
                        .Columns("tctAgentName").DefValue = lclsIntermedia.sCliename
                        .Columns("cbeStatus").DefValue = lclsIntermedia.nInt_status
                        .Columns("tcnAgentOrg").DefValue = lclsIntermedia.nSupervis
                        .Columns("tctOrgName").DefValue = lclsIntermedia.sOrgName
                        .Columns("cbeOffice").DefValue = lclsIntermedia.nOffice
                        .Columns("tcdCommidate").DefValue = lclsIntermedia.dCommidate
                        .Columns("tctAnull").DefValue = mobjValues.TypeToString(lclsIntermedia.dNulldate, eFunctions.Values.eTypeData.etdDate)
                        .Columns("cmdAddress").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA106&sOnSeq=2&nMainAction=401&sClient=" & lclsIntermedia.sClient & "','ShowAddress',500,500,'yes','yes','no','no')"
				
                        Response.Write(mobjGrid.DoRow())
                        lintIndex = lintIndex + 1
                    End With
                Next lclsIntermedia
		
                lclsIntermedia = Nothing
            
            End If
        End If
	
        With Response
            
            .Write(mobjGrid.closeTable())
            .Write(mobjValues.AnimatedButtonControl("cmdBack", "/VTimeNet/Images/btnLargeBackOff.png", GetLocalResourceObject("cmdBackToolTip"), , "ControlNextBack('Back')", CDbl(Request.QueryString.Item("nRow")) <= 1 Or IsNothing(Request.QueryString.Item("nRow"))))
            .Write(mobjValues.AnimatedButtonControl("cmdNext", "/VTimeNet/Images/btnLargeNextOff.png", GetLocalResourceObject("cmdNextToolTip"), , "ControlNextBack('Next')", lintIndex < 50))
            .Write(mobjValues.BeginPageButton)
        
        End With
        lclsIntermedia = Nothing
        lcolIntermedias = Nothing
    End Sub

'% insPreAGC006Upd: Se cargan los controles de la página, para evaluar la condición de búsqueda
'--------------------------------------------------------------------------------------------
Private Sub insPreAGC006Upd()
	'--------------------------------------------------------------------------------------------
	Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valAgent.aspx", "AGC006", Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("nIndex"))))
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("agc006_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "agc006_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.55
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>		        
<HTML>
<HEAD>    
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	%>
        <%	'$$EWI_1012:D:\VisualTIMEChile\Result\VTimeStep1\agent\agent\Vtime\Scripts\tMenu.js#%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>    
<%	
End If
%>

<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 5 $|$$Date: 31/05/04 20:13 $" 

//% insCancel: se controla la acción Cancelar de la ventana
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: se controla el estado de los campos de la ventana
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0;lintIndex<document.forms[0].length;lintIndex++)
        document.forms[0].elements[lintIndex].disabled = false
	EditRecord(-1, nMainAction,'Add')
} 	

//% ControlNextBack: Se encarga de amumentar o disminuir la consulta de los registros

function ControlNextBack(Option) {
    //-------------------------------------------------------------------------------------------
    var lstrURL = self.document.location.href
    var llngRow = lstrURL.substr(lstrURL.indexOf("&nRow=") + 6)
    lstrURL = lstrURL.replace(/&nRow=.*/, '')
    switch (Option) {
        case "Next":
            if (isNaN(llngRow))
                lstrURL = lstrURL + "&nRow=51"
            else {
                llngRow = insConvertNumber(llngRow) + 50;
                lstrURL = lstrURL + "&nRow=" + llngRow
            }
            break;

        case "Back":
            if (!isNaN(llngRow)) {
                llngRow = insConvertNumber(llngRow) - 50;
                lstrURL = lstrURL + "&nRow=" + llngRow
            }
    }
    self.document.location.href = lstrURL;
}

</SCRIPT>           
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("AGC006", Request.QueryString.Item("sWindowDescript")))
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.MakeMenu("AGC006", "AGC006_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
		.Write("<SCRIPT>var nMainAction=top.frames[""fraSequence""].plngMainAction</SCRIPT>")
	End If
End With
mobjMenu = Nothing
%>

</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmIntermedInq" ACTION="ValAgent.aspx?sMode=1">
<%
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR><BR>")
End If

Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))

Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write("<BR>")
	Call insPreAGC006()
Else
	Call insPreAGC006Upd()
End If
mobjValues = Nothing
mobjGrid = Nothing
%>    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.11.55
Call mobjNetFrameWork.FinishPage("agc006_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





