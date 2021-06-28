<%@ Control Language="VB" ClassName="ctrlDineroValores" AutoEventWireup="false" CodeFile="ctrlDineroValores.ascx.vb" Inherits="ctrlDineroValores"  EnableViewState="false" %>

<%@ Import Namespace="eFunctions.Extensions" %>


<script type="text/javascript" language="JavaScript">
    
    
    function Session_NMONEY_TRANSIT(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NMONEY_TRANSIT", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }


    function Session_NMONEY_PERMANENCE(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NMONEY_PERMANENCE", lstrString_Val, '/VTimeNet/Policy/PolicySeq');

    }    

</script>
<script runat="server">

</script>
<%
    If Request.QueryString.Item("Type") <> "PopUp" Then    
%>
   
    <div>
        <table width="100%">
            <tr>
                <td colspan="4" class="HighLighted">
                    <label> <%=resxValues.FindDictionaryValue("MoneyValues_Title")  %> </label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="Horline">
                </td>
            </tr>
            <tr>
                <td>
                    <label> <%=resxValues.FindDictionaryValue("NMONEYTRANSIT_MoneyValues_Caption") %>
                        </label>
                </td>
                <td>
                    <%
                        Response.Write(mObjValues.NumericControl("tcnMoney_Transit", 18, IIf(mObjInformacionDineroValores.NMONEY_TRANSIT <>  eRemoteDB.Constants.intNull , mObjInformacionDineroValores.NMONEY_TRANSIT, Session("NMONEY_TRANSIT")), True, resxValues.FindDictionaryValue("NMONEYTRANSIT_MoneyValues_ToolTip"), True, 6, , , ,  "Session_NMONEY_TRANSIT(this)" , False))
                    %>
                </td>
                <td>
                    <label>
                        <%=resxValues.FindDictionaryValue("NMONEYPERMANENCE_MoneyValues_Caption") %>
                    </label>
                </td>
                <td>
                    <%
                        Response.Write(mObjValues.NumericControl("tcnMoney_Permanence", 18, IIf(mObjInformacionDineroValores.NMONEY_PERMANENCE <>  eRemoteDB.Constants.intNull , mObjInformacionDineroValores.NMONEY_PERMANENCE, Session("NMONEY_PERMANENCE")), True, resxValues.FindDictionaryValue("NMONEYPERMANENCE_MoneyValues_ToolTip"), True, 6, , , , "Session_NMONEY_PERMANENCE(this)", False))
                    %>
                </td>
            </tr>
        </table>
    </div>
<%
    End If
%>