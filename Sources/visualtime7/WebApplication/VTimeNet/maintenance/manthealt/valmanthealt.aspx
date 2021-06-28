<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

Dim mstrQueryString As String

Dim mstrErrors As String
Dim mstrCodispl As Object
Dim mobjValues As eFunctions.Values
Dim mobjMantHealt As Object

'- Se define la contante para el manejo de errores en caso de advertencias.
Dim mstrCommand As String


'%insValMantHealt: Se realizan las validaciones masivas de la forma.
'--------------------------------------------------------------------------------------------
Function insValMantHealt() As String
	'--------------------------------------------------------------------------------------------
	
	Dim mclstab_am_lim As eBranches.tab_am_lim
	Select Case Request.QueryString.Item("sCodispl")
		
		'+MAM001: Límite de enfermedades
		
		Case "MAM001"
			mclstab_am_lim = New eBranches.tab_am_lim
			With Request
				If CDbl(.QueryString.Item("nZone")) = 1 Then
					insValMantHealt = mclstab_am_lim.insValMAM001_k(.QueryString.Item("sCodispl"), CInt(Request.QueryString.Item("nMainAction")), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valPay_Concep"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble))
				Else
					If .QueryString.Item("WindowType") = "PopUp" Then
						mclstab_am_lim.nBranch = mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble)
						mclstab_am_lim.nProduct = mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble)
						mclstab_am_lim.dEffecdate = mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate)
						mclstab_am_lim.nCover = mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble)
						mclstab_am_lim.nPay_concep = mobjValues.StringToType(.QueryString.Item("nPay_concept"), eFunctions.Values.eTypeData.etdDouble)
						mclstab_am_lim.nModulec = mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble)
						
						insValMantHealt = mclstab_am_lim.insValMAM001(.QueryString.Item("sCodispl"), .QueryString.Item("Action"), .Form.Item("valIllness"), mobjValues.StringToType(.Form.Item("tcnLimit_per"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
				mclstab_am_lim = Nothing
			End With
			
			'+ MAM002 - Exclusiones generales de enfermedades.    
		Case "MAM002"
			If CDbl(Request.QueryString.Item("nZone")) = 1 Then
				mobjMantHealt = New eBranches.Tab_am_gex
				
				With Request
					insValMantHealt = mobjMantHealt.insValMAM002_K(Request.QueryString.Item("sCodispl"), mobjValues.StringToType(Request.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate))
				End With
			Else
				mobjMantHealt = New eBranches.Tab_am_gex
				
				If Request.QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) And Request.QueryString.Item("WindowType") = "PopUp" Then
					With Request
						insValMantHealt = mobjMantHealt.insValMAM002(Request.QueryString.Item("sCodispl"), Request.Form.Item("cbeIllness"), mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Request.Form.Item("cbeExc_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdExc_date"), eFunctions.Values.eTypeData.etdDate), Request.QueryString.Item("Action"))
					End With
				End If
			End If
			
			'+ MAM003 - Enfermedades
		Case "MAM003"
			With Request
				If .QueryString.Item("WindowType") = "PopUp" Then
					mobjMantHealt = New eBranches.tab_am_ill
					insValMantHealt = mobjMantHealt.insValMAM003_k("MAM003", .QueryString("Action"), .Form.Item("tctIllness"), .Form.Item("tctDescript"), .Form.Item("tctIll_OMS"), .Form.Item("cbeStatregt"))
				End If
			End With
			
			'+ Tarifa de MAS Salud
			
		Case "MAM8000"
			mobjMantHealt = New eBranches.Tar_Health
			
			With Request
				If .QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						insValMantHealt = mobjMantHealt.insvalMAM8000_K("MAM8000", mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble, True), mobjValues.StringToType(.Form.Item("valAgreement"), eFunctions.Values.eTypeData.etdDouble, True))
					Else
						insValMantHealt = mobjMantHealt.insvalMAM8000("MAM8000", .QueryString("Action"), Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nCover"), Session("nAgreement"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble))
					End If
				End If
			End With
		Case Else
			insValMantHealt = "insValMantHealt: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
	End Select
End Function

'% insPostMantHealt: Se realizan las actualizaciones a las tablas.
'--------------------------------------------------------------------------------------------
Function insPostMantHealt() As Boolean
	'--------------------------------------------------------------------------------------------
	Dim lblnPost As Boolean
	
	lblnPost = False
	
	Dim mclstab_am_lim As eBranches.tab_am_lim
	With Request
		Select Case .QueryString.Item("sCodispl")
			
			'+MAM001: Límite de enfermedades
			Case "MAM001"
				mclstab_am_lim = New eBranches.tab_am_lim
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						lblnPost = True
						mstrQueryString = "&nBranch=" & mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nCover=" & mobjValues.StringToType(.Form.Item("valCover"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.TypeToString(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nModulec=" & mobjValues.StringToType(.Form.Item("valModulec"), eFunctions.Values.eTypeData.etdDouble) & "&nPay_concept=" & mobjValues.StringToType(.Form.Item("valPay_Concep"), eFunctions.Values.eTypeData.etdDouble) & "&sCodispl=MAM001"
					Else
						mstrQueryString = "&nBranch=" & mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble) & "&nProduct=" & mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble) & "&nCover=" & mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble) & "&dEffecdate=" & mobjValues.TypeToString(.QueryString("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nModulec=" & mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble) & "&nPay_concept=" & mobjValues.StringToType(.QueryString.Item("nPay_Concept"), eFunctions.Values.eTypeData.etdDouble) & "&nMainAction=" & mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
						
						If .QueryString.Item("WindowType") = "PopUp" Then
							lblnPost = mclstab_am_lim.insPostMAM001(CDbl(.QueryString.Item("nZone")) = 1, .QueryString.Item("sCodispl"), mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble), .QueryString.Item("Action"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(.QueryString.Item("nCover"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPay_concept"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nModulec"), eFunctions.Values.eTypeData.etdDouble), .Form.Item("valIllness"), mobjValues.StringToType(.Form.Item("tcnLimit_per"), eFunctions.Values.eTypeData.etdDouble))
						Else
							lblnPost = True
						End If
					End If
				End With
				mclstab_am_lim = Nothing
				
				'+ MAM002 - Exclusiones generales de enfermedades. 
			Case "MAM002"
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						mstrQueryString = "&dEffecdate=" & mobjValues.StringToType(.Form.Item("tcdEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&sCodispl=MAM002"
						lblnPost = True
					Else
						mstrQueryString = "&dEffecdate=" & mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) & "&nMainAction=" & mobjValues.StringToType(.QueryString.Item("nMainAction"), eFunctions.Values.eTypeData.etdDouble)
						
						If .QueryString.Item("WindowType") = "PopUp" And .QueryString.Item("nMainAction") <> CStr(eFunctions.Menues.TypeActions.clngActionQuery) Then
							
							mobjMantHealt = New eBranches.Tab_am_gex
							lblnPost = mobjMantHealt.insPostMAM002(Request.QueryString.Item("Action"), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), Request.Form.Item("cbeIllness"), mobjValues.StringToType(Request.Form.Item("cbeExc_code"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Request.Form.Item("tcdExc_date"), eFunctions.Values.eTypeData.etdDate), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						Else
							lblnPost = True
						End If
					End If
				End With
				
				'+ MAM003 - Enfermedades.
			Case "MAM003"
				mobjMantHealt = New eBranches.tab_am_ill
				With Request
					If CDbl(.QueryString.Item("nZone")) = 1 Then
						lblnPost = True
					Else
						If .QueryString.Item("WindowType") = "PopUp" Then
							lblnPost = mobjMantHealt.insPostMAM003("MAM003", .QueryString("Action"), .Form.Item("tctIllness"), .Form.Item("tctDescript"), .Form.Item("tctIll_OMS"), .Form.Item("cbeStatregt"), mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
						Else
							lblnPost = True
						End If
					End If
				End With
				
				'+ Tabla de tarifa MAS Salud
				
			Case "MAM8000"
				lblnPost = True
				
				With Request
					If Request.QueryString.Item("nAction") <> CStr(eFunctions.Menues.TypeActions.clngAcceptdataFinish) Then
						If CDbl(.QueryString.Item("nZone")) = 1 Then
							Session("nBranch") = .Form.Item("cbeBranch")
							Session("nProduct") = .Form.Item("valProduct")
							Session("nCover") = .Form.Item("valCover")
							Session("dEffecdate") = .Form.Item("tcdEffecdate")
							Session("nAgreement") = .Form.Item("valAgreement")
						Else
							lblnPost = True
							
							lblnPost = mobjMantHealt.InsPostMAM8000(.QueryString("Action"), Session("nBranch"), Session("nProduct"), Session("dEffecdate"), Session("nCover"), Session("nAgreement"), mobjValues.StringToType(.Form.Item("tcnAge"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("cbeSex"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnIni"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnEnd"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.Form.Item("tcnRate"), eFunctions.Values.eTypeData.etdDouble), Nothing, Session("nUsercode"))
						End If
					End If
				End With
		End Select
	End With
	
	insPostMantHealt = lblnPost
End Function

</script>
<%Response.Expires = -1

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT SRC="/VTimeNet/scripts/GenFunctions.js"> </SCRIPT>



	
</HEAD>
<%If Request.QueryString.Item("nZone") = "1" Then
	%><BODY><%	
Else
	%><BODY CLASS="Header"><%	
End If
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:02 $|$$Author: Nvaplat61 $"
	
//%CancelErrors: Acciones al efectuar la cancelación de algún error.
//-----------------------------------------------------------------------------------------
function CancelErrors(){
//-----------------------------------------------------------------------------------------
	self.history.go(-1)
}

//%NewLocation: se recalcula el URL de la página
//-----------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//-----------------------------------------------------------------------------------------
    var lstrLocation = "";
    lstrLocation += Source.location;
    lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp;
    Source.location = lstrLocation
}
</SCRIPT>
<%
mstrCommand = "&sModule=Maintenance&sProject=MantHealt&sCodisplReload=" & Request.QueryString.Item("sCodispl")

mobjValues = New eFunctions.Values

    '+ Si no se han validado los campos de la página
    
    If Request.Form.Item("sCodisplReload") = vbNullString Then
        mstrErrors = insValMantHealt()
        Session("sErrorTable") = mstrErrors
        Session("sForm") = Request.Form.ToString
    Else
        Session("sErrorTable") = vbNullString
        Session("sForm") = vbNullString
    End If

If mstrErrors > vbNullString Then
	With Response
		.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
		.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""MantHealt"",660,330);document.location.href='/VTimeNet/common/blank.htm';")
		.Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		.Write("</SCRIPT>")
	End With
    Else
        If insPostMantHealt() Then
            If Request.QueryString.Item("WindowType") <> "PopUp" Then
                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
                    Response.Write("<SCRIPT>insReloadTop(true,false)</SCRIPT>")
                Else
                    Select Case Request.QueryString.Item("sCodispl")
                        Case "MAM002"
                            Response.Write("<SCRIPT>window.close();top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString  & """;</SCRIPT>")
                            
                        Case Else
                            
                            If IsNothing(Request.QueryString.Item("sCodisplReload")) Then
                                Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & """;</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & mstrQueryString & """;</SCRIPT>")
                            End If
				
                            If Request.QueryString.Item("nZone") = "1" Then
                                If Request.Form.Item("sCodisplReload") <> vbNullString Then
                                    Response.Write("<SCRIPT>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                                End If
                            Else
                                Response.Write("<SCRIPT>top.fraHeader.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_K", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & """;</SCRIPT>")
                            End If
                    End Select
                End If
            Else
                '+ Se recarga la página que invocó la PopUp.
                Select Case Request.QueryString.Item("sCodispl")
                    Case "MAM001"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAM001.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
                    Case "MAM002"
                        If Request.QueryString.Item("nZone") = "1" Then
                            Response.Write("<SCRIPT>top.fraHeader.document.location.href='MAM002_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
                        Else
                            If Request.Form.Item("sCodisplReload") = vbNullString Then
                                Response.Write("<SCRIPT>top.opener.document.location.href='MAM002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
                            Else
                                Response.Write("<SCRIPT>top.close();top.opener.top.opener.document.location.href='MAM002.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & mstrQueryString & "'</SCRIPT>")
                            End If
                        End If

                    Case "MAM003"
                        Response.Write("<SCRIPT>top.opener.document.location.href='MAM003_K.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=302'</SCRIPT>")
                    Case Else
                        Response.Write("<SCRIPT>top.opener.document.location.href='" & Request.QueryString.Item("sCodispl") & ".aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & "'</SCRIPT>")
                End Select
            End If
        End If
	
        If Request.QueryString.Item("nMainAction") = "401" Then
            Session("bQuery") = True
        Else
            Session("bQuery") = False
        End If
End If

mobjValues = Nothing
mobjMantHealt = Nothing

%>
</FORM>
</BODY>
</HTML>
</BODY>
</HTML>




