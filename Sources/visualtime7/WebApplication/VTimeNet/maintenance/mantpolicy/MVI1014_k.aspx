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
           NMODULEC.disabled=false;
           btnNMODULEC.disabled=false;
           NCOVER.disabled=false;
           btnNCOVER.disabled=false;
           NTYPERISK.disabled=false;
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
	    if (NMODULEC = undefined) {
	        if (NMODULEC.value = "") {
	            NMODULEC.value = 0;
	        }
	    }
       switch (field.name) {   
            case 'cbeBranch':
                if (field.value != "")
                    with (self.document.forms[0]) {
                        NMODULEC.Parameters.Param1.sValue = cbeBranch.value
                        NMODULEC.Parameters.Param2.sValue = valProduct.value
                        NMODULEC.Parameters.Param3.sValue = DEFFECDATE.value;
                        NCOVER.Parameters.Param1.sValue = cbeBranch.value
                        NCOVER.Parameters.Param2.sValue = valProduct.value
                        NCOVER.Parameters.Param3.sValue = NMODULEC.value
                        NCOVER.Parameters.Param4.sValue = DEFFECDATE.value;


                    }
                break;
            case 'valProduct':
                if (field.value != "")
                    with (self.document.forms[0]) {
                        NMODULEC.Parameters.Param1.sValue = cbeBranch.value
                        NMODULEC.Parameters.Param2.sValue = valProduct.value
                        NMODULEC.Parameters.Param3.sValue = DEFFECDATE.value;
                        NCOVER.Parameters.Param1.sValue = cbeBranch.value
                        NCOVER.Parameters.Param2.sValue = valProduct.value
                        NCOVER.Parameters.Param3.sValue = NMODULEC.value
                        NCOVER.Parameters.Param4.sValue = DEFFECDATE.value;


                    }
                break;
            case 'DEFFECDATE':
                if (field.value != "")
                    with (self.document.forms[0]) {
                        NMODULEC.Parameters.Param1.sValue = cbeBranch.value
                        NMODULEC.Parameters.Param2.sValue = valProduct.value
                        NMODULEC.Parameters.Param3.sValue = DEFFECDATE.value;
                        NCOVER.Parameters.Param1.sValue = cbeBranch.value
                        NCOVER.Parameters.Param2.sValue = valProduct.value
                        NCOVER.Parameters.Param3.sValue = NMODULEC.value;
                        NCOVER.Parameters.Param4.sValue = DEFFECDATE.value;
                }
                break;
            case 'NMODULEC':
                if (field.value != "")
                    with (self.document.forms[0]) {
                        NCOVER.Parameters.Param1.sValue = cbeBranch.value
                        NCOVER.Parameters.Param2.sValue = valProduct.value
                        NCOVER.Parameters.Param3.sValue = NMODULEC.value
                        NCOVER.Parameters.Param4.sValue = DEFFECDATE.value;

                    }
                break;
        
       }    
    }
</script>
<html>
<head>
    <title></title>
		<%
			Response.Write(mobjValues.StyleSheet())
			mobjValues.sCodisplPage = "MVI1014_k"
			With New eFunctions.Menues
					Response.Write(.MakeMenu(Request.QueryString.Item("sCodispl"), "MVI1014_k.aspx", 1, "", Request.QueryString.Item("sWindowDescript")))
			End With
		%>    
</HEAD>
<body onunload="closeWindows();">
    <form method="post" id="FORM" action="MVI1014_val.aspx?sMode=1">
	    <br/><br/>	       	
 <table style="border: 0; width: 100%;">
  <tbody>
   <tr style="vertical-align: top;">
    <td style="text-align: left; width: 25%;"><label for="cbeBranch"><%= GetLocalResourceObject("cbeBranch_Caption") %></label>
<label id=0 title='<%= GetLocalResourceObject("cbeBranch_RequiredMessage") %>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 25%;"><%=mobjValues.BranchControl(FieldName:="cbeBranch", Alias_Renamed:=GetLocalResourceObject("cbeBranch_ToolTip"), DefValue:="", FieldProduct:="valProduct",  OnChange:="InputOnChange(this)", Disabled:=True)%></td>
    <td style="text-align: left; width: 25%;"><label for="valProduct"><%= GetLocalResourceObject("valProduct_Caption") %></label>
<label id=0 title='<%= GetLocalResourceObject("valProduct_RequiredMessage") %>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 25%;"><%=mobjValues.ProductControl(FieldName:="valProduct", Alias_Renamed:=GetLocalResourceObject("valProduct_ToolTip"), BranchValue:="0", ValuesType:=eValuesType.clngWindowType,  Disabled:=True, DefValue:="", OnChange:="InputOnChange(this)", ShowDescript:=True, bAllowInvalid:=False, ProdClass:=eProdClass.clngAll)%></td>
   </tr>
   <tr style="vertical-align: top;">
   <td style="text-align: left; width: 25%;"><label for="DEFFECDATE"><%= GetLocalResourceObject("DEFFECDATE_Caption") %></label>
<label id=0 title='<%= GetLocalResourceObject("DEFFECDATE_RequiredMessage") %>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 25%;"><%=mobjValues.DateControl(FieldName:="DEFFECDATE", DefValue:="", isRequired:=True, Alias_Renamed:=GetLocalResourceObject("DEFFECDATE_ToolTip"), OnChange:="InputOnChange(this)", Disabled:=True)%></td>
    <td style="text-align: left; width: 25%;"><label for="NMODULEC"><%= GetLocalResourceObject("NMODULEC_Caption") %></label>
<label id=0 title='<%= GetLocalResourceObject("NMODULEC_RequiredMessage") %>'><font color=#FF0000>*</font></label></td>
    <td style="text-align: left; width: 25%;"><% mobjValues.Parameters.Add("NBRANCH", eRemoteDB.Constants.intNull, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable) %>
<% mobjValues.Parameters.Add("NPRODUCT", eRemoteDB.Constants.intNull, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable) %>
<% mobjValues.Parameters.Add("DEFFECDATE", eRemoteDB.Constants.dtmNull, eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable) %>
<%=mobjValues.PossiblesValues(FieldName:="NMODULEC", TableName:="TABTAB_MODUL", ValuesType:=eValuesType.clngWindowType, DefValue:="0", NeedParam:=True, ComboSize:=1, OnChange:="InputOnChange(this)", Disabled:=True, MaxLength:=0, Alias_Renamed:=GetLocalResourceObject("NMODULEC_ToolTip"), CodeType:=eTypeCode.eNumeric, ShowDescript:=True, bAllowInvalid:=False)%></td>
  </tr>
   <tr style="vertical-align: top;">
    <td style="text-align: left; width: 25%;">  
        <label for="NCOVER"><%= GetLocalResourceObject("NCOVER_Caption") %></label>
        <label id=Label2 title='<%= GetLocalResourceObject("NCOVER_RequiredMessage") %>'>
            <font color=#FF0000>*</font>
        </label>
    </td>
    <td style="text-align: left; width: 25%;">
        <% mobjValues.Parameters.Add("NBRANCH", eRemoteDB.Constants.intNull, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable) %>
        <% mobjValues.Parameters.Add("NPRODUCT", eRemoteDB.Constants.intNull, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable) %>
        <% mobjValues.Parameters.Add("NMODULEC", 0, eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable) %>
        <% mobjValues.Parameters.Add("DEFFECDATE", eRemoteDB.Constants.dtmNull, eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable) %>
        <%=mobjValues.PossiblesValues(FieldName:="NCOVER", TableName:="TAB_COVER", ValuesType:=eValuesType.clngWindowType, DefValue:="", NeedParam:=True, ComboSize:=1, OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=0, Alias_Renamed:=GetLocalResourceObject("NCOVER_ToolTip"), CodeType:=eTypeCode.eNumeric, ShowDescript:=True, bAllowInvalid:=False)%>
    </td>
    <td style="text-align: left; width: 25%;">
        <label for="NTYPERISK"><%= GetLocalResourceObject("NTYPERISK_Caption") %></label>
        <label id=0 title='<%= GetLocalResourceObject("NTYPERISK_RequiredMessage") %>'>
        <font color=#FF0000>*</font>
        </label>
    </td>
    <td style="text-align: left; width: 25%;"><%=mobjValues.PossiblesValues(FieldName:="NTYPERISK", TableName:="TABLE5639", ValuesType:=eValuesType.clngComboType, DefValue:="7", NeedParam:=False, ComboSize:=1, OnChange:="InputOnChange(this)", Disabled:=True, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NTYPERISK_ToolTip"), CodeType:=eTypeCode.eNumeric, ShowDescript:=True, bAllowInvalid:=False)%></td>
    
   </tr>
   <tr style="vertical-align: top;">
   </tr>
  </tbody>
 </table>
     	   
 
    </form>
</body>
</html>