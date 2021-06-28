<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mclstar_tr_mer As eBranches.tar_tr_mer
Dim mobjValues As eFunctions.Values
Dim mstrErrors As String
'~End Body Block VisualTimer Utility

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String


'% insValidateInformation: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
    Function insValidateInformation() As String
        Dim eIniVal As Object
        Dim C_MNOTFOUNDCODE As String
        Dim invValidateInformation As String
        'Dim insCommonFunction As Object
        Dim eEndVal As Integer
        '--------------------------------------------------------------------------------------------
        '^^Begin Trace Block 08/09/2005 05:42:28 p.m.
        'Call insCommonFunction("Valmanttransport", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
        '~~End Trace Block
	
        '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
        Call mobjNetFrameWork.BeforeValidate(Request.QueryString.Item("sCodispl"))
        '~End Header Block VisualTimer Utility
	
        Select Case Request.QueryString.Item("sCodispl")
		
            '+MTR001: Tarifa de transporte
		
            Case "MTR001"
                With Request
                    mclstar_tr_mer = New eBranches.tar_tr_mer
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValidateInformation = mclstar_tr_mer.InsValMTR001_k(.QueryString.Item("sCodispl"), _
                                                                               mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), _
                                                                               .QueryString.Item("Action"), _
                                                                               mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                               mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                                                               mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                                                               mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValidateInformation = mclstar_tr_mer.InsValMTR001(.QueryString.Item("sCodispl"), _
                                                                                 mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                 .QueryString.Item("Action"), _
                                                                                 mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                 mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                 mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                 mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                                                                 mobjValues.StringToType(.Form.Item("valClassMerch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                 mobjValues.StringToType(.Form.Item("cbePacking"), eFunctions.Values.eTypeData.etdInteger), _
                                                                                 mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
			
            Case Else
                invValidateInformation = "invValidateInformation: " & C_MNOTFOUNDCODE & " (" & Request.QueryString.Item("sCodispl") & ")"
        End Select
	
        '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
        Call mobjNetFrameWork.AfterValidate(Request.QueryString.Item("sCodispl"))
        '~End Header Block VisualTimer Utility
	
        '^^Begin Trace Block 08/09/2005 05:42:28 p.m.
        'Call insCommonFunction("Valmanttransport", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
        '~~End Trace Block
    End Function



'% insPostInformation: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
    Function insPostInformation() As Boolean
        
        'Dim eIniPost As Object
        'Dim insCommonFunction As Object
        'Dim eEndPost As Integer
        '--------------------------------------------------------------------------------------------
        '^^Begin Trace Block 08/09/2005 05:42:28 p.m.
        'Call insCommonFunction("Valmanttransport", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
        '~~End Trace Block
        insPostInformation = False
	
        '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
        Call mobjNetFrameWork.BeforePost(Request.QueryString.Item("sCodispl"))
        '~End Header Block VisualTimer Utility					            
	
        Select Case Request.QueryString.Item("sCodispl")
		
            '+MTR001: Tarifa de transporte
            Case "MTR001"
                With Request
                    insPostInformation = mclstar_tr_mer.InsPostMTR001(CDbl(.QueryString.Item("nZone")) = 1, _
                                                                           .QueryString.Item("sCodispl"), _
                                                                           mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           .QueryString.Item("Action"), _
                                                                           mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), _
                                                                           mobjValues.StringToType(.Form.Item("valClassMerch"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           mobjValues.StringToType(.Form.Item("cbePacking"), eFunctions.Values.eTypeData.etdInteger), _
                                                                           mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
        End Select
	
        '^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
        Call mobjNetFrameWork.AfterValidate(Request.QueryString.Item("sCodispl"))
        '~End Header Block VisualTimer Utility
	
        '^^Begin Trace Block 08/09/2005 05:42:28 p.m.
        'Call insCommonFunction("Valmanttransport", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
        '~~End Trace Block
    End Function



'% insFinish: Se activa cuando la acción es finalizar
'-----------------------------------------------------------------------------------------------------------------------
Function insFinish() As Object
	'-----------------------------------------------------------------------------------------------------------------------
	Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("MTR001")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "MTR001"

mstrCommand = "sModule=Maintenance&sProject=MantProduct&sCodisplReload=" & Request.QueryString.Item("sCodispl")
%>
<SCRIPT>
//%CancelErrors: Va a la ventana anterior si se produce un error.
//---------------------------------------------------------------------------------------------------
function CancelErrors(){
//---------------------------------------------------------------------------------------------------
    self.history.go(-1)
}
//%NewLocation: Se posiciona en la página seleccionada. 
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
    Source.location = lstrLocation
}
</SCRIPT>
<HTML>
<HEAD>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("GE002"))
End With
%>
    <META NAME="GENERATOR" Content="eTransaction Designer for Visual TIME">    

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
</HEAD>
<BODY>
<FORM id=Form1 name=Form1>
<%
'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValidateInformation
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

    If mstrErrors > vbNullString Then
        
        With Response
            .Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""MantProductError"",660,330);")
            .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
            .Write("</SCRIPT>")
        End With
    Else
        If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
            If insPostInformation() Then
                If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
                    If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                        Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
                    Else
                        If Request.QueryString.Item("nZone") = "1" Then
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCurrency=" & Request.Form.Item("cbeCurrency") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                            End If
                        Else
                            Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                        End If
                    End If
                Else
				
                    '+ Se recarga la página que invocó la PopUp
                    Select Case Request.QueryString.Item("sCodispl")
                        Case "MTR001"
                            Response.Write("<SCRIPT>top.opener.document.location.href='MTR001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "' </SCRIPT>")
                    End Select
                End If
            End If
        Else
            If Session("bQuery") = True Then
                Response.Write("<SCRIPT>insReloadTop(true, false);</SCRIPT>")
            Else
                insFinish()
            End If
        End If
    End If
    mclstar_tr_mer = Nothing
    mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("MTR001")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>



