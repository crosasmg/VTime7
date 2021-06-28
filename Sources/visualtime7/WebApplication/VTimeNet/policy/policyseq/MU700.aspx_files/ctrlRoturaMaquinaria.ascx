<%@ Control Language="VB" ClassName="ctrlRoturaMaquinaria" AutoEventWireup="false"  Strict="false" CodeFile="ctrlRoturaMaquinaria.ascx.vb" Inherits="ctrlRoturaMaquinaria" EnableViewState="false" %>

<%@ Import Namespace="eFunctions.Extensions" %>
<script type="text/javascript" language="JavaScript">

    function ShowRoturasMaquinarias() {

        ShowPopUp('/VTimeNet/Common/CMU702.aspx?nType=4', 'dump', 600, 200);

    }

    function massiveRotMaqui(value) {

        ShowPopUp('/VTimeNet/policy/policyseq/MU700.aspx_files/MU700_Massive_RotMaqui.aspx', 'dump', 510, 100);

    }

    function InputOnChangeRotMaqui(inputControl) {
        switch (inputControl.name) {

            case 'NCAPITAL_RotMaqui':

                var nCapitalValue = insConvertNumber(self.document.forms[0].NCAPITAL_RotMaqui.value);
                var nRateValue = insConvertNumber(self.document.forms[0].NRATE_RotMaqui.value);

                var nPremiumValue = 0;

                if (isNumber(nCapitalValue) && isNumber(nRateValue)) {
                    nPremiumValue = (parseFloat(nCapitalValue) * (parseFloat(nRateValue) / 1000));
                }
                else {
                    nPremiumValue = 0;
                }

                self.document.forms[0].NPREMIUM_RotMaqui.value = nPremiumValue.toString().replace('.', ',');

                break;

            case 'NRATE_RotMaqui':

                var nCapitalValue = insConvertNumber(self.document.forms[0].NCAPITAL_RotMaqui.value);
                var nRateValue = insConvertNumber(self.document.forms[0].NRATE_RotMaqui.value);

                var nPremiumValue = 0;

                if (isNumber(nCapitalValue) && isNumber(nRateValue)) {
                    nPremiumValue =  (parseFloat(nCapitalValue) * (parseFloat(nRateValue) / 1000));
                }
                else {
                    nPremiumValue = 0;
                }

                self.document.forms[0].NPREMIUM_RotMaqui.value = nPremiumValue.toString().replace('.', ',');

                break;

            default:
                break;
        }
    }



</script>
<script runat="server">

</script>
   <div>
        <table width="100%">
<%
If Request.QueryString.Item("Type") <> "PopUp" orelse Request.QueryString.Item("gridName") = "RotMaqui"  Then
%>        
            <tr>
                <td colspan="5" class="HighLighted">
                    <label> <%=resxValues.FindDictionaryValue("RotMaqui_Title") %> </label>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="Horline">
                </td>
            </tr>
<%
    End If
%>
            <tr>
                <td colspan="5">
<%  If Request.QueryString.Item("Type") <> "PopUp"   Then
                   Response.Write("<div style='overflow-y:scroll; height:150px;'>") 
  End If  %>
                    <%
   
                        insDefineHeader_RotMaqui()
                        If Request.QueryString.Item("Type") <> "PopUp" Then
                            insPreMU700_RotMaqui()
                        Else If Request.QueryString.Item("gridName") =  "RotMaqui"  Then
                            insPreMU700Upd_RotMaqui()
                        End If
    
                    %>
<%  If Request.QueryString.Item("Type") <> "PopUp"   Then
                   Response.Write("</div >") 
  End If  %>
                </td>
            </tr>
<%
  If Request.QueryString.Item("Type") <> "PopUp" Then    
%>
            <tr>
                <td width="2%">
                    <%=mobjValues.AnimatedButtonControl("btnCargaMasiva", "/VTimeNet/images/batchStat03.png", "Carga masiva rotura de maquinaria", , "massiveRotMaqui(this)", False) %>
                </td>
                <td>
                    <label><%=resxValues.FindDictionaryValue("MASSIVECHARGE_RotMaqui_Caption") %></label>
                </td>
                <td>
                </td>
                <td style="text-align: right">
                    <label id="0">
                        <%=resxValues.FindDictionaryValue("Show_RotMaqui_Caption") %>
                    </label>
                </td>
                <td width="2%" style="text-align: right">
                    <%=mobjValues.AnimatedButtonControl("btnShowRoturasMaquinarias", "/VTimeNet/images/btn_ValuesOff.png", resxValues.FindDictionaryValue("Show_RotMaqui_ToolTip") , , "ShowRoturasMaquinarias()", False) %>
                </td>
            </tr>
<%
  End If
%>
        </table>
    </div>