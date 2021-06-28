<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

Dim lobjDBConnect As eRemoteDB.Connection
Dim cConnect As String
Dim sDatabase As String
Dim sServer As String
Dim sLogin As String
Dim sPassword As String

Dim sRut As String
Dim sDig As String
Dim sTipPer As String
Dim sApePat As String
Dim sApeMat As String
Dim sNombre As String
Dim sRazSoc As String
Dim sFullNom As String
Dim sCodUse As String
Dim oConn As Object
Dim oRs As Object
Dim OraResp As String
Dim iError As Short
Dim sParams As String
Dim sForm As String
Dim sErrorPKG As Byte
Dim sMsgOra As Byte
Dim sErrOra As Byte


Function Reemplazar(ByVal sDato As String) As String
	Reemplazar = Replace(sDato, ";", ",")
End Function

Function CreateConnString(ByRef DataSource As String, ByRef UserID As String, ByRef Password As String) As String
	
	CreateConnString = "Provider=MSDAORA.1;" & "Data Source=" & DataSource & ";" & "User ID=" & UserID & ";" & "Password=" & Password
End Function

</script>
<%Response.Expires = -1

lobjDBConnect = New eRemoteDB.Connection
With lobjDBConnect
	.bErr_Module = CStr(Session("bErrorModule")) = "1"
	
	sDatabase = .Database
	sServer = ""
	sLogin = .Login
	sPassword = .Password
End With
cConnect = CreateConnString(sDatabase, sLogin, sPassword)
lobjDBConnect = Nothing

OraResp = ""
sForm = Request.Form.Item("sForm")
sRut = Request.Form.Item("txtrut")
sDig = UCase(Request.Form.Item("txtdvrut"))
sTipPer = Request.Form.Item("cboTipoPersona")
sApePat = UCase(Reemplazar(Trim(Request.Form.Item("txtapepat"))))
sApeMat = UCase(Reemplazar(Trim(Request.Form.Item("txtapemat"))))
sNombre = UCase(Reemplazar(Trim(Request.Form.Item("txtnombre"))))
sRazSoc = UCase(Reemplazar(Trim(Request.Form.Item("txtrazonsocial"))))
sCodUse = Session("nUserCode")

Select Case Trim(sTipPer)
	Case CStr(1), CStr(3)
		sFullNom = sApePat & " " & sApeMat & " " & sNombre
		sRazSoc = ""
	Case Else
		sApePat = ""
		sApeMat = ""
		sFullNom = sRazSoc
		sNombre = Mid(sFullNom, 1, 19)
End Select

sParams = sRut & ";" & sDig & ";" & sNombre & ";" & sApePat & ";" & sApeMat & ";" & sRazSoc & ";" & sFullNom & ";" & sTipPer & ";" & sCodUse


'UPGRADE_NOTE: The 'NR_Client.clsTransaccion' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
oConn = Server.CreateObject("NR_Client.clsTransaccion")
iError = oConn.Insert_DataClient(cConnect, sParams, oRs, OraResp)

If Len(OraResp) > 0 Or iError < 0 Then
	iError = -1
Else
	iError = 0
	sErrorPKG = oRs("ErrorPackage")
	sMsgOra = oRs("MsgOracle")
	sErrOra = oRs("ErrOracle")
	oRs.Close()
	If sErrorPKG <> 0 Then
		iError = -1
	End If
End If
oRs = Nothing
oConn = Nothing
%>
<html>
<head>
   <title>Datos Cliente</title>
   <link href="/VTimeNet/common/custom.css" rel="stylesheet" type="text/css">
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
   </head>
<body bgcolor="#FFFFF4" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" rightmargin="0" bottommargin="0">
<!--	    1. <%=OraResp%>	<br>2. <%=sErrorPKG%>	<br>3. <%=sMsgOra%>	<br>4. <%=sErrOra%>-->
<br>
<%If iError <> 0 Then%>
<table width="100%" border="1" cellspacing="0" cellpadding="0" align="center">
  <tr> 
    <table width="90%" border="0" cellpadding="0" cellspacing="0" bordercolorlight="navy" bordercolordark="navy" align="center">
      <tr> 
        <td>
          <table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolorlight="navy" bordercolordark="navy">
		    <tr> 
		      <td height="20" align="center" bordercolorlight="navy"><label><%= GetLocalResourceObject("AnchorCaption") %></label></td>
			</tr>			
		    <tr> 
		      <td><hr></td>
			</tr>
			<tr> 
			  <td height="50" align="center" bordercolorlight="navy"><label><%= GetLocalResourceObject("Anchor2Caption") %></label></td>
			</tr>
			<tr> 
			  <td height="40">&nbsp;</td>
			</tr>			
			<tr> 
			  <td height="30" align="right" valign="bottom">
				<a href="javascript:window.close();" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image2','','/VTimeNet/images/btnCancelOn.png',1)"><img border="0" name="Image2" src="/VTimeNet/images/btnCancelOff.png" title="Cancelar la información de la ventana"></a>
			  </td>
			</tr>			
		  </table>
		</td>
  	  </tr>
	</table>
  </td>   
</tr>
</table> 
<%Else%>    
	<script LANGUAGE="javascript">
	    try{
	        Obj = eval('opener.parent.fraFolder.' + '<%=sForm%>');
	    }
	    catch(e){
	        Obj = eval('opener.parent.fraHeader.' + '<%=sForm%>');
	    }
	    
		Obj.dtcClient.value = "<%=sRut%>";
		Obj.dtcClient_Digit.value="<%=sDig%>";
		Obj.getElementsByTagName("DIV")["lblCliename"].innerHTML = "<%=sFullNom%>"
		
		window.close();	
	</script>
<%End If%>

<br>
</body>
<script LANGUAGE="javascript">
	<!--
	function MM_swapImgRestore() { file://v3.0
	   var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
	}

	function MM_preloadImages() { file://v3.0
	  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
	    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
	    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
	}
	 
	function MM_findObj(n, d) { file://v3.0
	  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
	    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
	  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
	  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document); return x;
	}
	 
	function MM_swapImage() { file://v3.0
	  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
	   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
	}
	file://-->
	//-->
</script>
</html>




