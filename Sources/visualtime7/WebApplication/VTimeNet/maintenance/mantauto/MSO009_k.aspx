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
           NCURRENCY.disabled=false;
           DEFFECDATE.disabled=false;
           btn_DEFFECDATE.disabled=false;		
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
			mobjValues.sCodisplPage = "MSO009_k"
			With New eFunctions.Menues
					Response.Write(.MakeMenu(Request.QueryString.Item("sCodispl"), "MSO009_k.aspx", 1, "", Request.QueryString.Item("sWindowDescript")))
			End With
		%>    
</HEAD>
<body onunload="closeWindows();">
    <form method="post" id="FORM" action="MSO009_val.aspx?sMode=1">
	    <br/><br/>	       	
 <table style="border: 0; width: 100%;">
  <tbody>
   <tr style="vertical-align: top;">
    <td style="text-align: left; width: 16.5%;"><label for="cbeBranch"><%=GetLocalResourceObject("cbeBranch_Caption")%></label><label title='<%=GetLocalResourceObject("cbeBranch_RequiredMessage")%>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 16.5%;"><%=mobjValues.BranchControl(FieldName:="cbeBranch", Alias_Renamed:=GetLocalResourceObject("cbeBranch_ToolTip"), DefValue:="", FieldProduct:="valProduct",  OnChange:="InputOnChange(this)", Disabled:=True)%></td>
    <td style="text-align: left; width: 16.5%;"><label for="valProduct"><%=GetLocalResourceObject("valProduct_Caption")%></label><label title='<%=GetLocalResourceObject("valProduct_RequiredMessage")%>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 16.5%;"><%=mobjValues.ProductControl(FieldName:="valProduct", Alias_Renamed:=GetLocalResourceObject("valProduct_ToolTip"), BranchValue:="0", ValuesType:=eValuesType.clngWindowType,  Disabled:=True, DefValue:="", OnChange:="InputOnChange(this)", ShowDescript:=True, bAllowInvalid:=False, ProdClass:=eProdClass.clngAll)%></td>
    <td style="text-align: left; width: 17.0%;"><label for="NCURRENCY"><%=GetLocalResourceObject("NCURRENCY_Caption")%></label><label title='<%=GetLocalResourceObject("NCURRENCY_RequiredMessage")%>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 17.0%;"><%=mobjValues.PossiblesValues(FieldName:="NCURRENCY", TableName:="TABLE11", ValuesType:=eValuesType.clngComboType, DefValue:="", NeedParam:=False, ComboSize:=1, OnChange:="InputOnChange(this)", Disabled:=True, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NCURRENCY_ToolTip"), CodeType:=eTypeCode.eNumeric, ShowDescript:=True, bAllowInvalid:=False)%></td>
   </tr>
   <tr style="vertical-align: top;">
    <td style="text-align: left; width: 16.5%;"><label for="DEFFECDATE"><%=GetLocalResourceObject("DEFFECDATE_Caption")%></label><label title='<%=GetLocalResourceObject("DEFFECDATE_RequiredMessage")%>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 16.5%;"><%=mobjValues.DateControl(FieldName:="DEFFECDATE", DefValue:="", isRequired:=True, Alias_Renamed:=GetLocalResourceObject("DEFFECDATE_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=True)%></td>
    <td style="width: 67%;" colspan="4">&nbsp;</td>
   </tr>
  </tbody>
 </table>
     	   
 
    </form>
</body>
</html>