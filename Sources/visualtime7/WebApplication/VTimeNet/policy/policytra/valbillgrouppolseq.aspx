<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.23
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility

    Dim mobjValues As eFunctions.Values
    Dim mobjPolicy As ePolicy.Out_moveme
    Dim mstrErrors As String

    Dim mstrCommand As String

    Dim lclsGeneral As eGeneral.GeneralFunction


    '**% insValBillGroupPolSeq: The massive validations of each one of the pages are made.  
    '% insValBillGroupPolSeq: Se realizan las validaciones masivas de cada una de las páginas.
    '--------------------------------------------------------------------------------------------
    Function insValBillGroupPolSeq() As String
        '--------------------------------------------------------------------------------------------
        Select Case Request.QueryString.Item("sCodispl")

        '+ CA036_K: Facturación de colectivos
            Case "CA036_K"
                With Request
                    insValBillGroupPolSeq = mobjPolicy.insValCA036_K("CA036_k", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcnPolicy"), eFunctions.Values.eTypeData.etdDouble, True))
                End With

            '**+ Validations of frame CA036 - Selection of movements.  
            '+ Validaciones del frame CA036 - Selección de movimientos.

            Case "CA036"
                With Request
                    insValBillGroupPolSeq = mobjPolicy.insValCA036(.QueryString.Item("sCodispl"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("tctClient"), .Form.Item("sColinvot"), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdStart"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdEnd"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcdLedgerDate"), eFunctions.Values.eTypeData.etdDate), Session("nCompanyUser"))
                End With

            '**+ Validations of frame CA036A - pending Movements to invoice.  
            '+ Validaciones del frame CA036A - Movimientos pendientes por facturar.

            Case "CA036A"
                Dim nSelection As Integer

                '+Se valida si existen movimientos seleccionados
                If Request.Form.GetValues("Sel") Is Nothing Then
                    nSelection = 0
                Else
                    nSelection = Request.Form.GetValues("Sel").Length
                End If

                insValBillGroupPolSeq = mobjPolicy.insValCA036A(Request.QueryString.Item("sCodispl"), nSelection)

            '**+ Validations of frame CA039 - Condition of selection of movements.  
            '+ Validaciones del frame CA039 - Condición de selección de movimientos.

            Case "CA039"
                insValBillGroupPolSeq = mobjPolicy.insValCA039("CA039", Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcnCertif"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnYear"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("tcnMonth"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("cbeTratypei"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valSituation"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(Request.Form.Item("valGroup"), eFunctions.Values.eTypeData.etdDouble, True))
            Case Else
                insValBillGroupPolSeq = "insValBillGroupPolSeq: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
    End Function

    '% insPostBillGroupPolSeq: Se realizan las actualizaciones de las ventanas.
    '--------------------------------------------------------------------------------------------
    Function insPostBillGroupPolSeq() As Boolean
        '--------------------------------------------------------------------------------------------
        Dim lblnPost As Boolean

        lblnPost = True

        Select Case Request.QueryString.Item("sCodispl")

        '+ CA036_K: Facturación de colectivos
            Case "CA036_K"
                With Request
                    '+ Se asignan los valores indicados en los campos de la página
                    Session("nBranch") = .Form.Item("cbeBranch")
                    Session("nProduct") = .Form.Item("valProduct")
                    Session("nPolicy") = .Form.Item("tcnPolicy")
                    '+ Se asignan los valores por defecto para el manejo de la secuencia
                    Session("sCertype") = "2"
                    Session("nCertifCA039") = ""
                    Session("dEffecdate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)
                    Session("sLastInv") = "2"
                    Session("sClient") = vbNullString
                    Session("nCurrency") = vbNullString
                    Session("dStart") = vbNullString
                    Session("dEnd") = vbNullString
                    Session("sTypeMov") = "1"
                    Session("dLedgerDate") = mobjValues.TypeToString(Today, eFunctions.Values.eTypeData.etdDate)

                    Session("nYear") = vbNullString
                    Session("nMonth") = vbNullString
                    Session("nTratypei") = vbNullString
                    Session("nSituation") = vbNullString
                    Session("nGroup") = vbNullString

                End With

            '**+ Frame CA036 -  Selection of movements.  
            '+ Frame CA036 - Selección de movimientos.    
            Case "CA036"
                With Request
                    Session("nCurrency") = .Form.Item("cbeCurrency")
                    Session("sTypeMov") = .Form.Item("optTypeMov")
                    If .Form.Item("tcdStart") = "" Then
                        Session("dStart") = Date.MinValue
                    Else
                        Session("dStart") = .Form.Item("tcdStart")
                    End If
                    Session("dEnd") = .Form.Item("tcdEnd")
                    Session("dLedgerDate") = .Form.Item("tcdLedgerDate")
                    Session("sClient") = .Form.Item("tctClient")
                End With

            '**+ Frame CA036A -  Pending movements to invoice.  
            '+ Frame CA036A - Movimientos pendientes por facturar.

            Case "CA036A"
                With Request
                    lblnPost = mobjPolicy.inspostCA036A(Session("nBranch"), Session("nProduct"), Session("nPolicy"), .Form.Item("hddSelAux"), .Form.Item("hddCertif"), .Form.Item("hddMovnumbe"), .Form.Item("hddDigit"), Session("nUsercode"))
                End With

            '**+ Frame CA039 -  Condition of selection of movements.  
            '+ Frame CA039 - Condición de selección de movimientos.

            Case "CA039"
                With Request
                    Session("nCertifCA039") = .Form.Item("tcnCertif")
                    Session("nYear") = .Form.Item("tcnYear")
                    Session("nMonth") = .Form.Item("tcnMonth")
                    Session("nTratypei") = .Form.Item("cbeTratypei")
                    Session("nSituation") = .Form.Item("valSituation")
                    Session("nGroup") = .Form.Item("valGroup")
                End With
        End Select

        insPostBillGroupPolSeq = lblnPost
    End Function

    '% insFinish: Se activa cuando la acción es Finalizar.
    '--------------------------------------------------------------------------------------------
    Function insFinish() As Boolean
        '--------------------------------------------------------------------------------------------
        '+ Se verifica que no existan páginas marcadas como requeridas en la secuencia
        Response.Write("<SCRIPT>insvalTabs()</" & "Script>")
    End Function

</script>
<%Response.Expires = -1441
    mobjNetFrameWork = New eNetFrameWork.Layout
    mobjNetFrameWork.sSessionID = Session.SessionID
    mobjNetFrameWork.nUsercode = Session("nUsercode")
    Call mobjNetFrameWork.BeginPage("valbillgrouppolseq")

    mobjValues = New eFunctions.Values
    '^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.23
    mobjValues.sSessionID = Session.SessionID
    mobjValues.nUsercode = Session("nUsercode")
    '~End Body Block VisualTimer Utility

    mobjValues.sCodisplPage = "valbillgrouppolseq"
    mobjPolicy = New ePolicy.Out_moveme
    mstrCommand = "&sModule=Policy&sProject=PolicyTra&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>




<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:53 $|$$Author: Nvaplat61 $"  

//*% insvalTabs: The existence of windows required in the sequence is verified  
//% insvalTabs: Se verifica la existencia de ventanas requeridas en la secuencia.
//-------------------------------------------------------------------------------------------
function insvalTabs(){
//-------------------------------------------------------------------------------------------
<%lclsGeneral = New eGeneral.GeneralFunction
    Response.Write("var lstrMessage = 'Err. 3902: " & lclsGeneral.insLoadMessage(3902) & "';" & vbCrLf)
    lclsGeneral = Nothing
%>
	var lblnTabs = false;
	var Array = top.frames['fraSequence'].sequence;
	
	for(var lintIndex=0; lintIndex<Array.length; lintIndex++)
		if(Array[lintIndex].Require=="2" ||
		   Array[lintIndex].Require=="5")
			lblnTabs = true;

	if(lblnTabs){
//+ Se manda un mensaje de error.  Existen ventanas requeridas
		top.frames["fraFolder"].document.location.reload();
		alert(lstrMessage);
	}
	else
//+ Se procesan los movimientos a facturar
		insDefValues('ProcessCAL036');
}
</SCRIPT>        
</HEAD>
<BODY>
<%
    If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
        '**+ If the fields of the page have not been validated.  
        '+ Si no se han validado los campos de la página.
        '+ Si no se han validado los campos de la página
        If Request.Form.Item("sCodisplReload") = vbNullString Then
            mstrErrors = insValBillGroupPolSeq()
            Session("sErrorTable") = mstrErrors
            Session("sForm") = Request.Form.ToString
        Else
            Session("sErrorTable") = vbNullString
            Session("sForm") = vbNullString
        End If

        If mstrErrors > vbNullString Then
            With Response
                .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
                .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""PolicyBillGroup"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
                .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
                .Write("</SCRIPT>")
            End With
        Else
            If insPostBillGroupPolSeq() Then
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
                    '**+ One moves automatically to the following page.  
                    '+ Se mueve automáticamente a la siguiente página.
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&sSel=" & Request.Form.Item("Sel") & "';</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=Yes&nOpener=" & Request.QueryString.Item("sCodispl") & "&sSel=" & Request.Form.Item("Sel") & "';</SCRIPT>")
                    End If
                Else
                    If Request.Form.Item("sCodisplReload") = vbNullString Then
                        Response.Write("<SCRIPT>window.close();opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sSel=" & Request.Form.Item("Sel") & "';</SCRIPT>")
                    Else
                        Response.Write("<SCRIPT>window.close();opener.opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicyTra/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=NO&nOpener=" & Request.QueryString.Item("sCodispl") & "&sSel=" & Request.Form.Item("Sel") & "';</SCRIPT>")
                    End If

                    '**+ The page is recharged that invoked the PopUp.  
                    '+ Se recarga la página que invocó la PopUp.

                    Select Case Request.QueryString.Item("sCodispl")
                        Case "CA036A"
                            Response.Write("<SCRIPT>opener.document.location.href='CA036A.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=0" & Request.QueryString.Item("ReloadIndex") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&Index=" & Request.QueryString.Item("Index") & "'</SCRIPT>")
                    End Select
                End If
            End If
        End If
    Else
        If insFinish() Then
            Response.Write("<SCRIPT>top.location.reload();</SCRIPT>")
        End If
    End If

    mobjPolicy = Nothing
    mobjValues = Nothing
%>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.23
    Call mobjNetFrameWork.FinishPage("valbillgrouppolseq")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>