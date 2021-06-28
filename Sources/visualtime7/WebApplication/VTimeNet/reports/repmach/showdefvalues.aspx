<%@ Page Language="VB" explicit="true" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eProduct" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 7/4/03 11.35.42
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values


'%Main: Rutina principal de proceso
'--------------------------------------------------------------------------------------------
Public Sub Main()
	'--------------------------------------------------------------------------------------------
	response.Write(mobjValues.StyleSheet() & vbCrLf)
	response.Write("<SCRIPT>")
	
	Select Case Request.QueryString.Item("Field")
		Case "getDigit"
			Call insGetDigit()
		Case "disModul"
			Call insdisModul()
		Case "disCertif"
			Call insdiscertif()
		Case "disCertifClie"
			Call insdiscertifsclie()
		Case "disCertifPro"
			Call insdiscertifProp()
		Case "getClaim"
			Call insGetClaim()
	End Select
	
	response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
	response.Write("</" & "Script>")
	
	'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjValues = Nothing
	
End Sub


'% insGetDigit: Verifica si existe el automóvil en la base de datos de automóviles
'%                Se utiliza para el campo patente de la página AU557
'--------------------------------------------------------------------------------------------
Sub insGetDigit()
	'--------------------------------------------------------------------------------------------
	Dim lclsAuto_db As ePolicy.Auto_db
	
	lclsAuto_db = New ePolicy.Auto_db
	If lclsAuto_db.Find_db1(Request.QueryString.Item("sLicense_ty"), Request.QueryString.Item("sRegist")) Then
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sDigitName") & ".value='" & lclsAuto_db.sDigit & "';")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sDigitName") & ".disabled=true;")
		End With
	End If
	'UPGRADE_NOTE: Object lclsAuto_db may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsAuto_db = Nothing
End Sub

Sub insdisModul()
	'--------------------------------------------------------------------------------------------
	Dim lclstabmodul As eProduct.Product
	
	lclstabmodul = New eProduct.Product
	
	'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
	If lclstabmodul.IsModule(CInt(Request.QueryString.Item("nbranch")), CInt(Request.QueryString.Item("nproduct")), Today) Then
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nModulecName") & ".disabled=false;")
		End With
	Else
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nModulecName") & ".disabled=true;")
		End With
	End If
	'UPGRADE_NOTE: Object lclstabmodul may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclstabmodul = Nothing
End Sub

Sub insdiscertif()
	'--------------------------------------------------------------------------------------------
	Dim lclsncertif As ePolicy.ValPolicyRep
	lclsncertif = New ePolicy.ValPolicyRep
	
	If lclsncertif.insValPolColec("2", CInt(Request.QueryString.Item("nbranch")), CInt(Request.QueryString.Item("nproduct")), CDbl(Request.QueryString.Item("npolicy"))) Then
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value=0;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=false;")
		End With
	Else
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value=0;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=true;")
		End With
	End If
	'UPGRADE_NOTE: Object lclsncertif may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsncertif = Nothing
End Sub

Sub insdiscertifProp()
	'--------------------------------------------------------------------------------------------
	Dim lclsncertif As ePolicy.ValPolicyRep
	Dim lscertype As String
	lclsncertif = New ePolicy.ValPolicyRep
	
	If Request.QueryString.Item("scertype") = "2" Then
		lscertype = "6"
	Else
		If Request.QueryString.Item("scertype") = "3" Then
			lscertype = "7"
		Else
			lscertype = "1"
		End If
	End If
	If lclsncertif.insValPolColec(lscertype, CInt(Request.QueryString.Item("nbranch")), CInt(Request.QueryString.Item("nproduct")), CDbl(Request.QueryString.Item("npolicy"))) Then
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value=0;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=false;")
		End With
	Else
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value=0;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=true;")
		End With
	End If
	'UPGRADE_NOTE: Object lclsncertif may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsncertif = Nothing
End Sub

'% insGetClaim: Verifica si existe el Siniestro, para obtener la sucursal, oficina, y agencia
'%                
'--------------------------------------------------------------------------------------------
Sub insGetClaim()
	'--------------------------------------------------------------------------------------------
	Dim lclsClaim As eClaim.Claim
	
	lclsClaim = New eClaim.Claim
	If lclsClaim.Find_1(CDbl(Request.QueryString.Item("nClaim"))) Then
		With response
			.Write("top.frames['fraHeader'].document.forms[0].P_COD_OFICINA.Parameters.Param1.sValue='" & lclsClaim.nOffice & "';")
			.Write("top.frames['fraHeader'].document.forms[0].P_COD_AGENCIA.Parameters.Param1.sValue='" & lclsClaim.nOffice & "';")
			.Write("top.frames['fraHeader'].document.forms[0].P_COD_AGENCIA.Parameters.Param2.sValue='" & lclsClaim.nOfficeAgen & "';")
			
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nOfficeName") & ".value='" & lclsClaim.nOffice & "';")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nOfficeName") & ".disabled=true;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nOfficeAgenName") & ".value='" & lclsClaim.nOfficeAgen & "';")
			.Write("top.frames['fraHeader'].$('#" & Request.QueryString.Item("nOfficeAgenName") & "').change();")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nAgencyName") & ".value='" & lclsClaim.nAgency & "';")
			.Write("top.frames['fraHeader'].$('#" & Request.QueryString.Item("nAgencyName") & "').change();")
		End With
		
	Else
		response.Write("alert(""Err 4005: El siniestro no está registrado"");")
		response.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nClaimName") & ".value='';")
	End If
	'UPGRADE_NOTE: Object lclsClaim may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsClaim = Nothing
End Sub

'% insdiscertifsclie: Deshabilita el numero del certificado y el rut del asegurado
'------------------------------------------------------------------------------------------------------------------------------------------------
Sub insdiscertifsclie()
	'------------------------------------------------------------------------------------------------------------------------------------------------
	Dim sBrancht As Object
	Dim lclsProduct As eProduct.Product
	Dim lclscertifclie As ePolicy.Policy
	
	lclsProduct = New eProduct.Product
	
	If lclsProduct.FindProdMaster(CInt(Request.QueryString.Item("nbranch")), CInt(Request.QueryString.Item("nproduct"))) Then
		sBrancht = lclsProduct.sBrancht
	End If
	'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsProduct = Nothing
	
	lclscertifclie = New ePolicy.Policy
	If lclscertifclie.Find("2", CInt(Request.QueryString.Item("nbranch")), CInt(Request.QueryString.Item("nproduct")), CDbl(Request.QueryString.Item("npolicy"))) Then
		
		Session("sNopayroll") = lclscertifclie.sNopayroll
		Session("sPolitype") = lclscertifclie.sPolitype
		'+ Si la poliza es de tipo colectiva se habilita el campo de ingreso del certificado
		If lclscertifclie.sPolitype = "2" Then
			With response
				.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value='';")
				.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=false;")
			End With
		Else
			With response
				.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value='0';")
				.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=true;")
			End With
		End If
		
		'+ Si el Producto es Vida y Si la poliza no es innominada se habilita el ingreso del asegurado 
		If sBrancht = "1" Or sBrancht = "5" Or sBrancht = "7" Then
			If lclscertifclie.sNopayroll <> "1" Then
				With response
					.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sClieName") & ".value='';")
					.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sClieName") & ".disabled=false;")
				End With
			End If
		Else
			With response
				.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sClieName") & ".value='';")
				.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sClieName") & ".disabled=true;")
			End With
		End If
	Else
		response.Write("alert(""Err 8071: La Póliza no está registrada"");")
		With response
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".value=0;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("nCertifName") & ".disabled=true;")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sClieName") & ".value='';")
			.Write("top.frames['fraHeader'].document.forms[0]." & Request.QueryString.Item("sClieName") & ".disabled=true;")
		End With
	End If
	'UPGRADE_NOTE: Object lclscertifclie may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclscertifclie = Nothing
	
End Sub

</script>
<%response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("showdefvalues")
mobjValues = New eFunctions.Values

'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.35.42
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<HTML>
<HEAD>
	<%'UPGRADE_NOTE: Language element 'SCRIPT' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file '/VTimeNet/Scripts/GenFunctions.js' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%'UPGRADE_NOTE: Language element '#INCLUDE' was migrated to the same language element but still may have a different behavior. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1011.htm  %>
<%'UPGRADE_NOTE: The file 'vtime/Includes/Constantes.aspx' was not found in the migration directory. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1003.htm  %>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 9 $|$$Date: 15/03/04 14:13 $|$$Author: Nvaplat53 $"
</SCRIPT>
</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%

Call Main()

%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 11.35.42
Call mobjNetFrameWork.FinishPage("showdefvalues")
'UPGRADE_NOTE: Object mobjNetFrameWork may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





