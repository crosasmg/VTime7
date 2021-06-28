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
Dim sForm As String

Dim oConn As Object
Dim oRs As Object
Dim OraResp As Object
Dim iError As Object
Dim sParams As String


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

sRut = Request.QueryString.Item("sClient")
sDig = UCase(Request.QueryString.Item("sDigit"))
sForm = Request.QueryString.Item("sForm")
sParams = ""

'UPGRADE_NOTE: The 'NR_Client.clsTransaccion' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
oConn = Server.CreateObject("NR_Client.clsTransaccion")
iError = oConn.GetPerson_Typ(cConnect, sParams, oRs, OraResp)
%>
<html>
<head>
<title>Datos Cliente</title>
	<link href="/VTimeNet/common/custom.css" rel="stylesheet" type="text/css">
    </head>
<body bgcolor="#FFFFF4" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" rightmargin="0" bottommargin="0" onLoad="javascript:Desactivar();">
<!--<%=OraResp%> -->
<form name="frm" method="post" action="NRClientApp_res.aspx">
<input type="hidden" name="sForm" value="<%=sForm%>">
<br>
<table width="100%" border="1" cellspacing="0" cellpadding="0" align="center">
  <tr> 
    <table width="90%" border="0" cellpadding="0" cellspacing="0" bordercolorlight="navy" bordercolordark="navy" align="center">
      <tr> 
        <td>
          <table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolorlight="navy" bordercolordark="navy">
		    <tr> 
		      <td width="30%" height="20" bordercolorlight="navy"><label ID="0"><%= GetLocalResourceObject("AnchorCaption") %></label></td>
			  <td width="70%" class="field" bordercolorlight="navy">
				<input type="text" name="txtrut" value="<%=sRut%>" size="14" readonly disabled>
				-
				<input type="text" name="txtdvrut" value="<%=sDig%>" size="1" readonly disabled> 
			  </td>
			</tr>
			<td><label><%= GetLocalResourceObject("Anchor2Caption") %><label></td>
			<td>
			   <select SIZE="1" NAME="cboTipoPersona" TABINDEX="0" TITLE="Tipo de persona" OnChange="javascript:Activar();"> 
			      <option VALUE="0" SELECTED></option> 
<%If Len(CStr(OraResp)) = 0 Then
	Do While Not oRs.Eof
		Response.Write("<OPTION VALUE='" & oRs.Fields("NPERSON_TYP") & "'>" & oRs.Fields("SDESCRIPT") & "</OPTION>" & vbNewLine)
		oRs.MoveNext()
	Loop 
	oRs.Close()
End If
oRs = Nothing
oConn = Nothing%> 							      
			   </select>
			 </td>			
			<tr> 
			  <td width="30%" height="20" bordercolorlight="navy"><label ID="0"><%= GetLocalResourceObject("Anchor3Caption") %></label></td>
			  <td width="70%" class="field" bordercolorlight="navy">
			       <input type="text" name="txtapepat" size="19" maxlength="19">
			  </td>
			</tr>
			<tr> 
			  <td width="30%" height="20" bordercolorlight="navy"><label ID="0"><%= GetLocalResourceObject("Anchor4Caption") %></label></td>
			  <td width="70%" class="field" bordercolorlight="navy">
			     <input type="text" name="txtapemat" size="19" maxlength="19">
			  </td>
			</tr>
			<tr> 
			  <td width="30%" height="20" bordercolorlight="navy"><label ID="0"><%= GetLocalResourceObject("Anchor5Caption") %></label></td>
			  <td width="70%" class="field" bordercolorlight="navy">
			     <input type="text" name="txtnombre" size="19" maxlength="19">
			  </td>
			</tr>			
			<tr> 
			  <td width="30%" height="20" bordercolorlight="navy"><label ID="0"><%= GetLocalResourceObject("Anchor6Caption") %></label></td>
			  <td width="70%" class="field" bordercolorlight="navy">
			     <input type="text" name="txtrazonsocial" size="19" maxlength="60">
			  </td>
			</tr>			
			<tr> 
			  <td height="30" colspan="2"><hr></td>
			</tr>
			<tr> 
			  <td height="30" colspan="2" align="right">
			    <a href="javascript:Aceptar()" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image1','','/VTimeNet/images/btnAcceptOn.png',1)"><img border="0" name="Image1" src="/VTimeNet/images/btnAcceptOff.png" title="Aceptar la información de la ventana"></a>
				  &nbsp;
				<a href="javascript:Cerrar();" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image2','','/VTimeNet/images/btnCancelOn.png',1)"><img border="0" name="Image2" src="/VTimeNet/images/btnCancelOff.png" title="Cancelar la información de la ventana"></a>
			  </td>
			</tr>			
		  </table>
		</td>
  	  </tr>
	</table>
  </td>   
</tr>
</table> 
<br>
</form>
</body>
<script LANGUAGE="javascript">
<!--
    Blanquear();
    
    function Desactivar(){
        Borrar();
        document.frm.txtapepat.disabled=true;
        document.frm.txtapemat.disabled=true;
        document.frm.txtnombre.disabled=true;
        document.frm.txtrazonsocial.disabled=true;
    }
    
    function Borrar(){
        document.frm.txtapepat.value="";
        document.frm.txtapemat.value="";
        document.frm.txtnombre.value="";
        document.frm.txtrazonsocial.value="";
    }

    function Activar(){
		var cIndTyp=document.frm.cboTipoPersona.selectedIndex;
		var cTypPer=document.frm.cboTipoPersona[cIndTyp];
        if ((cTypPer.value==1) || (cTypPer.value==3)){
            Desactivar();
            document.frm.txtapepat.disabled=false;
            document.frm.txtapemat.disabled=false;
            document.frm.txtnombre.disabled=false;
        }
        else{
            Desactivar();
            document.frm.txtrazonsocial.disabled=false;
        }
    }

	function Cerrar(){
	
		//alert(opener.parent.fraHeader.location.href); //fraGeneric.elements.lenght);
		//var	Obj = eval('opener.parent.fraHeader.' + '<%=sForm%>');
		//Obj.getElementById("lblCliename").innerHTML = "Jorge Renato Serrano Jara"
		window.close();
		
	}
	
	function Aceptar(){
		if (ValidarForm()){
			document.frm.txtrut.disabled=false;
			document.frm.txtdvrut.disabled=false;
            document.frm.txtapepat.disabled=false;
            document.frm.txtapemat.disabled=false;
            document.frm.txtnombre.disabled=false;
            document.frm.txtrazonsocial.disabled=false;
			window.document.frm.submit();
		}
	}
	
	function ValidarForm(){
		var cIndTyp=document.frm.cboTipoPersona.selectedIndex;
		var cTypPer=document.frm.cboTipoPersona[cIndTyp];
		var sApePat=document.frm.txtapepat;
		var sNombre=document.frm.txtnombre;
		var sRazSoc=document.frm.txtrazonsocial;
		
		if (cTypPer.value=="0"){
			alert("No olvide ingresar el tipo de persona");
			document.frm.cboTipoPersona.focus();
			return false;
		}
		
		if ((cTypPer.value=="1") || (cTypPer.value=="3")){
			if (sApePat.value.length<2){
				alert("No olvide ingresar el Apellido Materno");
				if (sApePat.disabled) sApePat.disabled=false;
				sApePat.focus();
				return false;				
			}
			if (sNombre.value.length<2){
				alert("No olvide ingresar el Nombre");
				if (sNombre.disabled) sNombre.disabled=false;
				sNombre.focus();
				return false;				
			}			
		}
		else
		{
			if (sRazSoc.value.length<2){
				alert("No olvide ingresar Razón Social");
				if (sRazSoc.disabled) sRazSoc.disabled=false;
				sRazSoc.focus();			
				return false
			}
		}
		return true;
	}
	
	function Blanquear()
	{
	    try{
	        Obj = eval('opener.parent.fraFolder.' + '<%=sForm%>');
	    }
	    catch(e){
	        Obj = eval('opener.parent.fraHeader.' + '<%=sForm%>');
	    }
		Obj.dtcClient.value = "";
		Obj.dtcClient_Digit.value="";
	}

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




