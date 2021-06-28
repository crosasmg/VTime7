<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim mclsTar_theft_cap As eBranches.Tar_theft_cap
Dim mclsTar_theft_con As eBranches.Tar_theft_con
Dim mclstar_theft_cash As eBranches.tar_theft_cash
Dim mclsTar_build As eBranches.Tar_build

Dim mobjValues As eFunctions.Values
Dim mstrErrors As String
'~End Body Block VisualTimer Utility

'+ Se define la contante para el manejo de errores en caso de advertencias
Dim mstrCommand As String

    '+ Se define la variable para almacenar el QueryString de los campos que existen en el encabezado de la transacción
    Dim mstrQueryString As String
    

'% insvalmanttheft: Se realizan las validaciones masivas de la forma
'--------------------------------------------------------------------------------------------
Function insvalmanttheft() As String
        'Dim eIniVal As Object
        'Dim eEndVal As Integer
	Dim C_MNOTFOUNDCODE As String
	'Dim insCommonFunction As Object
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:42:26 p.m.
        'Call insCommonFunction("valmanttheft", Request.QueryString.Item("sCodispl"), eIniVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	
	'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
	Call mobjNetFrameWork.BeforeValidate(Request.QueryString.Item("sCodispl"))
	'~End Header Block VisualTimer Utility
	
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MRO001: Capitales de Robo
		
		Case "MRO001"
			With Request
                    mclsTar_theft_cap = New eBranches.Tar_theft_cap
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insvalmanttheft = mclsTar_theft_cap.InsValMRO001_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insvalmanttheft = mclsTar_theft_cap.InsValMRO001(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCap_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTar_theft"), eFunctions.Values.eTypeData.etdInteger))
                        End If
                    End If
			End With
			
			'+MRO002: Tasas de Robo
			
		Case "MRO002"
			With Request
                    mclsTar_theft_con = New eBranches.Tar_theft_con
                    
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insvalmanttheft = mclsTar_theft_con.InsValMRO002_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnTar_theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insvalmanttheft = mclsTar_theft_con.InsValMRO002(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nTar_theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeRiskClass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeUbication"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
			End With
			
			'+MRO003: Tasas de Dinero en Cajas de Seguridad
			
		Case "MRO003"
			With Request
				mclstar_theft_cash = New eBranches.tar_theft_cash
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insvalmanttheft = mclstar_theft_cash.InsValMRO003_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("tcnTar_Theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insvalmanttheft = mclstar_theft_cash.InsValMRO003(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nTar_Theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeUbication"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
			End With
			
			'+MRO004: Recargo y Descuento por Construcción
			
		Case "MRO004"
			With Request
				mclsTar_build = New eBranches.Tar_build
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        insvalmanttheft = mclsTar_build.InsValMRO004_k(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcdEffecDate"), eFunctions.Values.eTypeData.etdDate))
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            insvalmanttheft = mclsTar_build.InsValMRO004(.QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCategory"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnExtraPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble))
                        End If
                    End If
			End With
			
		Case Else
			insvalmanttheft = "insvalmanttheft: " & C_MNOTFOUNDCODE & " (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
	
	'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
	Call mobjNetFrameWork.AfterValidate(Request.QueryString.Item("sCodispl"))
	'~End Header Block VisualTimer Utility
	
	'^^Begin Trace Block 08/09/2005 05:42:26 p.m.
        '    Call insCommonFunction("valmanttheft", Request.QueryString.Item("sCodispl"), eEndVal, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
End Function



'% insPostmanttheft: Se realizan las actualizaciones a las tablas
'--------------------------------------------------------------------------------------------
Function insPostmanttheft() As Boolean
	Dim eIniPost As Object
	Dim eEndPost As Integer
	'Dim insCommonFunction As Object
	'--------------------------------------------------------------------------------------------
	'^^Begin Trace Block 08/09/2005 05:42:26 p.m.
        'Call insCommonFunction("valmanttheft", Request.QueryString.Item("sCodispl"), eIniPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
	'~~End Trace Block
	insPostmanttheft = False
	
	'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
	Call mobjNetFrameWork.BeforePost(Request.QueryString.Item("sCodispl"))
	'~End Header Block VisualTimer Utility					            
	
        Select Case Request.QueryString.Item("sCodispl")
		
            '+MRO001: Capitales de Robo
            Case "MRO001"
                mclsTar_theft_cap = New eBranches.Tar_theft_cap
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&nCover=" & Request.Form.Item("cbeCover") & "&nCurrency=" & Request.Form.Item("cbeCurrency") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
                    End If
                    
                    insPostmanttheft = mclsTar_theft_cap.InsPostMRO001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nCurrency"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnCap_init"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnCap_end"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnTar_theft"), eFunctions.Values.eTypeData.etdInteger))
                    
                End With
			
                '+MRO002: Tasas de Robo
            Case "MRO002"
                mclsTar_theft_con = New eBranches.Tar_theft_con
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nTar_theft=" & Request.Form.Item("tcnTar_theft") & "&dEffecdate=" & Request.Form.Item("tcdEffecdate")
                    End If

                    insPostmanttheft = mclsTar_theft_con.InsPostMRO002(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nTar_theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeRiskClass"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("cbeUbication"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+MRO003: Tasas de Dinero en Cajas de Seguridad
            Case "MRO003"
                mclstar_theft_cash = New eBranches.tar_theft_cash
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nTar_Theft=" & Request.Form.Item("tcnTar_Theft") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate")
                    End If
                    
                    insPostmanttheft = mclstar_theft_cash.InsPostMRO003(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nTar_Theft"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeUbication"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
                '+MRO004: Recargo y Descuento por Construcción
            Case "MRO004"
                mclsTar_build = New eBranches.Tar_build
                With Request
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrQueryString = "&nBranch=" & Request.Form.Item("cbeBranch") & "&nProduct=" & Request.Form.Item("valProduct") & "&dEffecDate=" & Request.Form.Item("tcdEffecDate") 
                    End If
                    
                    insPostmanttheft = mclsTar_build.InsPostMRO004(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdInteger), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.QueryString.Item("dEffecDate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("cbeCategory"), eFunctions.Values.eTypeData.etdInteger), mobjValues.StringToType(.Form.Item("tcnExtraPrem"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnDiscount"), eFunctions.Values.eTypeData.etdDouble))
                End With
			
			
        End Select
	
	'^Begin Header Block VisualTimer Utility 1.1 27/05/2003 07:39:41 a.m.
	Call mobjNetFrameWork.AfterValidate(Request.QueryString.Item("sCodispl"))
	'~End Header Block VisualTimer Utility
	
	'^^Begin Trace Block 08/09/2005 05:42:26 p.m.
        'Call insCommonFunction("valmanttheft", Request.QueryString.Item("sCodispl"), eEndPost, Request.Form.ToString(), Request.Params.Get("Query_String"), Session.Contents, "NH")
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
Call mobjNetFrameWork.BeginPage("valmanttheft")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = "valmanttheft"

mstrCommand = "sModule=Maintenance&sProject=MantTheft&sCodisplReload=" & Request.QueryString.Item("sCodispl")
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
	mstrErrors = insvalmanttheft
	Session("sErrorTable") = mstrErrors
	Session("sForm") = Request.Form.ToString
Else
	Session("sErrorTable") = vbNullString
	Session("sForm") = vbNullString
End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""MantTheftError"",660,330);")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
Else
	If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
		If insPostmanttheft Then
			If Request.QueryString.Item("WindowType") <> "PopUp" Then
				
				If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					Response.Write("<SCRIPT>top.document.location.reload();</SCRIPT>")
				Else
					If Request.QueryString.Item("nZone") = "1" Then
						If Request.Form.Item("sCodisplReload") = vbNullString Then
							
							Select Case Request.QueryString.Item("sCodispl")
								Case "MRO001"
                                        Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
								Case "MRO002"
                                        Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
								Case "MRO003"
                                        Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
								Case "MRO004"
                                        Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
							End Select
							
							
						Else
							Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
						End If
					Else
						Response.Write("<SCRIPT>;self.history.go(-1);top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & """;</SCRIPT>")
					End If
				End If
			Else
				
				'+ Se recarga la página que invocó la PopUp
				Select Case Request.QueryString.Item("sCodispl")
					Case "MRO001"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRO001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&nCover=" & Request.QueryString.Item("nCover") & "&nCurrency=" & Request.QueryString.Item("nCurrency") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "' </SCRIPT>")
					Case "MRO002"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRO002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nTar_theft=" & Request.QueryString.Item("nTar_theft") & "&dEffecdate=" & Request.QueryString.Item("dEffecdate") & "' </SCRIPT>")
					Case "MRO003"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRO003.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nTar_Theft=" & Request.QueryString.Item("nTar_Theft") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "' </SCRIPT>")
					Case "MRO004"
						Response.Write("<SCRIPT>top.opener.document.location.href='MRO004.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "&nBranch=" & Request.QueryString.Item("nBranch") & "&nProduct=" & Request.QueryString.Item("nProduct") & "&dEffecDate=" & Request.QueryString.Item("dEffecDate") & "' </SCRIPT>")
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
    mclsTar_theft_cap = Nothing
    mclsTar_theft_con = Nothing
    mclstar_theft_cash = Nothing
    mclsTar_build = Nothing
mobjValues = Nothing
%>
        </FORM>
    </BODY>
</HTML>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 27/05/2003 07:39:47 a.m.
Call mobjNetFrameWork.FinishPage("valmanttheft")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>







