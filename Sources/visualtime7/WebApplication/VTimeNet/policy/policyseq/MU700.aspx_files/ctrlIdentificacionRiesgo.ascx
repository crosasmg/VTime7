<%@ Control Language="VB" ClassName="ctrlIdentificacionRiesgo" AutoEventWireup="false" CodeFile="ctrlIdentificacionRiesgo.ascx.vb" Inherits="ctrlIdentificacionRiesgo"  EnableViewState="false" %>

<%@ Import Namespace="eFunctions.Extensions" %>

<script type="text/javascript" language="JavaScript">

    function Session_NCONSCAT(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NCONSCAT", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }

    function Session_NSISMICZONE(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NSISMICZONE", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }

    

 </script>
<script runat="server">

</script>
    <div>
<%

If (Request.QueryString("sOnSeq") = "1"  AndAlso Request.QueryString("fromDelete") <> "1" )   Then    
    Response.Write("<script> ")
    Response.Write(" cleanValueFromStorage(); ")
    Response.Write("</script> ")
End If    
    
   
    If Request.QueryString.Item("Type") <> "PopUp" Then    
%>

        <table width="100%">
            <tr>
                <td colspan="4" class="HighLighted">
                    <label><%=resxValues.FindDictionaryValue("RiskIdentification_Title") %></label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="Horline">
                </td>
            </tr>
            <tr>
                <td>
                    <label><%=resxValues.FindDictionaryValue("NCODKID_MultiRisk_Caption") %></label>
                </td>
                <td>
                    <%
                        Response.Write(mObjValues.PossiblesValues("valCodKind", "TAB_BUSINESS", eFunctions.Values.eValuesType.clngWindowType, mobjIdentificacionRiesgo.NCODKINd, True, , , , , "setValueToStorage('valCodKind');", True, 10, resxValues.FindDictionaryValue("NCODKID_MultiRisk_ToolTip")))
                    %>
                </td>
                <td>
                    <label><%=resxValues.FindDictionaryValue("NCONSCAT_MultiRisk_Caption") %></label>
                </td>
                <td>
                <%
                    Response.Write(mObjValues.PossiblesValues("cbeConstCat", "table233", eFunctions.Values.eValuesType.clngWindowType, IIf( mobjIdentificacionRiesgo.NCONSCAT  = eRemoteDB.Constants.intNull , Session("NCONSCAT"), mobjIdentificacionRiesgo.NCONSCAT), , , , , , "setValueToStorage('cbeConstCat');Session_NCONSCAT(this);", , , resxValues.FindDictionaryValue("NCONSCAT_MultiRisk_ToolTip")))
                %>
                </td>
            </tr>
            <tr>
                <td>
                    <label><%=resxValues.FindDictionaryValue("NSISMICZONE_MultiRisk_Caption") %></label>
                </td>
                <td>
                <%
                    Response.Write(mObjValues.PossiblesValues("cbeSismicZone", "Table7047", 1,  IIf(mobjIdentificacionRiesgo.NSISMICZONE =  eRemoteDB.Constants.intNull ,   Session("NSISMICZONE") , mobjIdentificacionRiesgo.NSISMICZONE)  , , , , , , "setValueToStorage(""cbeSismicZone"") ;Session_NSISMICZONE(this); ", , , resxValues.FindDictionaryValue("NSISMICZONE_MultiRisk_ToolTip")))
                %>
                </td>
                <td>
                    <label>
                    </label>
                </td>
                <td>
                </td>
            </tr>
        </table>
<%
    
    
        If mobjIdentificacionRiesgo.NCODKINd = 0 OrElse mobjIdentificacionRiesgo.NCODKINd = eRemoteDB.Constants.intNull THEN
            mobjIdentificacionRiesgo.NCODKINd = mObjValues.StringToType(Request.Form.Item("valCodKind"), eFunctions.Values.eTypeData.etdInteger)

            If mobjIdentificacionRiesgo.NCODKINd = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('valCodKind'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].valCodKind.value = getValueFromStorage('valCodKind'); ")        
                Response.Write(" top.frames['fraFolder'].document.forms[0].valCodKind.onblur(); ")        
                Response.Write(" } ")
                Response.Write("</script> ")
            End If 

        End if

        If mobjIdentificacionRiesgo.NCONSCAT  = 0 OrElse mobjIdentificacionRiesgo.NCONSCAT = eRemoteDB.Constants.intNull THEN
            mobjIdentificacionRiesgo.NCONSCAT = mObjValues.StringToType(Request.Form.Item("cbeConstCat"), eFunctions.Values.eTypeData.etdInteger)

            If mobjIdentificacionRiesgo.NCONSCAT = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('cbeConstCat'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].cbeConstCat.value = getValueFromStorage('cbeConstCat'); ")        
                Response.Write(" top.frames['fraFolder'].document.forms[0].cbeConstCat.onblur(); ")        
                Response.Write(" } ")
                Response.Write("</script> ")
            End If  

        End If

        If mobjIdentificacionRiesgo.NSISMICZONE = 0 OrElse mobjIdentificacionRiesgo.NSISMICZONE = eRemoteDB.Constants.intNull THEN
            mobjIdentificacionRiesgo.NSISMICZONE = mObjValues.StringToType(Request.Form.Item("cbeSismicZone"), eFunctions.Values.eTypeData.etdInteger)

            If mobjIdentificacionRiesgo.NSISMICZONE = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('cbeSismicZone'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].cbeSismicZone.value = getValueFromStorage('cbeSismicZone'); ")        
                Response.Write(" } ")
                Response.Write("</script> ")
            End If  

        End If    
    
    End If
 %>
    </div>
