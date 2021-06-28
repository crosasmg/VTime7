<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import Namespace="eFunctions.Values" %>
<%@ Import Namespace="eRemoteDB.Parameter" %>

<script language="VB" runat="Server">

		'- Objeto para el manejo de las funciones generales de carga de valores
		Private mobjValues As New eFunctions.Values
		
		'- Objeto para el manejo de las zonas de la página    
		Private mobjMenu As New eFunctions.Menues
		
</script>
<%
		Response.Expires = -1441
%>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/ValFunctions.js"></script>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/tMenu.js"></script>
<script type="text/javascript" language="JavaScript">

	//% insStateZone: habilita los campos de la forma
	//-----------------------------------------------------------------------------
	function insStateZone(){
	//-----------------------------------------------------------------------------
	    with (document.forms[0]) {	  
           cbeBranch.disabled=false;
           valProduct.disabled=false;
           btnvalProduct.disabled=false;
           DEFFECTDATE.disabled=false;
           btn_DEFFECTDATE.disabled=false;		
	    }
	}
	
	//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
	//-----------------------------------------------------------------------------
	function insCancel(){
	//-----------------------------------------------------------------------------
	   return true
	}
  
  function InputOnChange(field) {
       switch (field.name) {   
        
       }    
    }
</script>
<html>
<head>
    <title></title>
		<%
			Response.Write(mobjValues.StyleSheet())
			mobjValues.sCodisplPage = "MAU7000_k"
			With New eFunctions.Menues
					Response.Write(.MakeMenu(Request.QueryString.Item("sCodispl"), "MAU7000_k.aspx", 1, "", Request.QueryString.Item("sWindowDescript")))
			End With
		%>    
</HEAD>
<body onunload="closeWindows();">
    <form method="post" id="FORM" action="MAU7000_val.aspx?sMode=1">
	    <br/><br/>	       	
 <table style="border: 0; width: 100%;">
  <tbody>
   <tr style="vertical-align: top;">
    <td style="text-align: left; width: 16.5%;"><label for="cbeBranch"><%=GetLocalResourceObject("cbeBranch_Caption")%></label><label title='<%=GetLocalResourceObject("cbeBranch_RequiredMessage")%>'><font color=#FF0000></font></label></td>
    <td style="text-align: left; width: 16.5%;"><%=mobjValues.BranchControl(FieldName:="cbeBranch", Alias_Renamed:=GetLocalResourceObject("cbeBranch_ToolTip"), DefValue:="", FieldProduct:="valProduct",  OnChange:="InputOnChange(this)", Disabled:=True)%></td>
    <td style="text-align: left; width: 16.5%;"><label for="valProduct"><%=GetLocalResourceObject("valProduct_Caption")%></label><label title='<%=GetLocalResourceObject("valProduct_RequiredMessage")%>'><font color=#FF0000></font></label></td>
    <td style="text-align: left; width: 16.5%;"><%=mobjValues.ProductControl(FieldName:="valProduct", Alias_Renamed:=GetLocalResourceObject("valProduct_ToolTip"), BranchValue:="0", ValuesType:=eValuesType.clngWindowType,  Disabled:=True, DefValue:="", OnChange:="InputOnChange(this)", ShowDescript:=True, bAllowInvalid:=False, ProdClass:=eProdClass.clngAll)%></td>
   <tr> <td style="text-align: left; width: 17.0%;"><label for="DEFFECTDATE"><%=GetLocalResourceObject("DEFFECTDATE_Caption")%></label><label title='<%=GetLocalResourceObject("DEFFECTDATE_RequiredMessage")%>'><font color=#FF0000></font></label></td>
    <td style="text-align: left; width: 17.0%;"><%=mobjValues.DateControl(FieldName:="DEFFECTDATE", DefValue:="", isRequired:=True, Alias_Renamed:=GetLocalResourceObject("DEFFECTDATE_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=True)%></td></tr>
   </tr>
  </tbody>
 </table>
     	   
 
    </form>
</body>
</html>