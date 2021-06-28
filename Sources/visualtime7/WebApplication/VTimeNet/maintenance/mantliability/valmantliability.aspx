<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

    Dim mclsTar_rc_bas As eBranches.Tar_rc_bas
    Dim mclstar_rc_fac As eBranches.tar_rc_fac
    Dim mclsTar_rc_des As eBranches.Tar_rc_des
    Dim mobjValues As eFunctions.Values
    Dim mstrErrors As String

'+ Se define la contante para el manejo de errores en caso de advertencias
    Dim mstrCommand As String


'% insValidateInformation: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
    Function insValvalmantliability() As String
        'Dim C_MNOTFOUNDCODE As String
        Dim invValvalmantliability As String
        '--------------------------------------------------------------------------------------------

        Select Case Request.QueryString.Item("sCodispl")
		
            '+MRC001: Tasa básica de responsabilidad civil
		
            Case "MRC001"
                With Request
                    mclsTar_rc_bas = New eBranches.Tar_rc_bas
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValvalmantliability = mclsTar_rc_bas.InsValMRC001_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValvalmantliability = mclsTar_rc_bas.InsValMRC001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeArticle"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeDetailArt"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
                '+MRC002: Factor de recargo de tasa básica de RC
			
            Case "MRC002"
                With Request
                    mclstar_rc_fac = New eBranches.tar_rc_fac
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValvalmantliability = mclstar_rc_fac.InsValMRC002_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValvalmantliability = mclstar_rc_fac.InsValMRC002(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCap_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
                '+MRC003: Descuento sobre volúmen de RC
			
            Case "MRC003"
                With Request
                    mclsTar_rc_des = New eBranches.Tar_rc_des
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insValvalmantliability = mclsTar_rc_des.InsValMRC003_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insValvalmantliability = mclsTar_rc_des.InsValMRC003(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCap_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
                End With
            Case Else
                'invValvalmantliability = "invValvalmantliability: " & C_MNOTFOUNDCODE & " (" & Request.QueryString.Item("sCodispl") & ")"
                invValvalmantliability = "invValvalmantliability: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
                
        End Select
	
    End Function



'% insPostInformation: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
    Function insPostvalmantliability() As Boolean
        insPostvalmantliability = False
	
        Select Case Request.QueryString.Item("sCodispl")
		
            '+MRC001: Tasa básica de responsabilidad civil
            Case "MRC001"
                With Request
                    mclsTar_rc_bas = New eBranches.Tar_rc_bas
                    insPostvalmantliability = mclsTar_rc_bas.InsPostMRC001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), eRemoteDB.Constants.intNull, mobjValues.StringToType(.Form.Item("cbeDetailArt"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeArticle"), eFunctions.Values.eTypeData.etdInteger))
                End With
                '+MRC002: Factor de recargo de tasa básica de RC
            Case "MRC002"
                With Request
                    mclstar_rc_fac = New eBranches.tar_rc_fac
                    insPostvalmantliability = mclstar_rc_fac.InsPostMRC002(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCap_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                End With
                '+MRC003: Descuento sobre volúmen de RC
            Case "MRC003"
                With Request
                    mclsTar_rc_des = New eBranches.Tar_rc_des
                    insPostvalmantliability = mclsTar_rc_des.InsPostMRC003(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCap_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
        End Select
	
	
    End Function



'% insFinish: Se activa cuando la acción es finalizar
'-----------------------------------------------------------------------------------------------------------------------
    Function insFinish() As Object
        '-----------------------------------------------------------------------------------------------------------------------
        Response.Write("<SCRIPT>insReloadTop(true, false);</" & "Script>")
    End Function

</script>
<%  Response.Expires = -1441

    mobjValues = New eFunctions.Values
    mobjValues.sSessionID = Session.SessionID
    mobjValues.sCodisplPage = "valmantliability"

    mstrCommand = "sModule=Maintenance&sProject=MantLiability&sCodisplReload=" & Request.QueryString.Item("sCodispl")
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
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/ConstLanguage.aspx" -->
<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/General.aspx" -->

        
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
</HEAD>
<BODY>
<FORM id=Form1 name=Form1>
<%
'+ Si no se han validado los campos de la página

If Request.Form.Item("sCodisplReload") = vbNullString Then
	mstrErrors = insValvalmantliability
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantProductError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
    If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insPostvalmantliability Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					If Request.QueryString.Item("nZone") = "1" Then
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							Select Case Request.QueryString.Item("sCodispl")
								Case "MRC001"
									Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("cbeCover") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
								Case "MRC002"
									Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
								Case "MRC003"
									Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("cbeCover") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
							End Select
						Else
							Select Case Request.QueryString.Item("sCodispl")
								Case "MRC001"
							        Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("cbeCover") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
								Case "MRC002"
							        Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
								Case "MRC003"
							        Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("cbeCover") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") & """;</SCRIPT>")
								Case Else
							        Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
							End Select
						End If
					Else
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
					End If
				End If
			Else
				
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "MRC001"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRC001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "' </SCRIPT>")
					Case "MRC002"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRC002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "' </SCRIPT>")
					Case "MRC003"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRC003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "' </SCRIPT>")
						
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
mclsTar_rc_bas = Nothing
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
