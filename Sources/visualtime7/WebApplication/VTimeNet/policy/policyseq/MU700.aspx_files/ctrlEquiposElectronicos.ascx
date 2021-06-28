<%@ Control Language="VB" ClassName="ctrlEquiposElectronicos" AutoEventWireup="false"  Strict="false" CodeFile="ctrlEquiposElectronicos.ascx.vb" Inherits="ctrlEquiposElectronicos" EnableViewState="false" %>


<%@ Import Namespace="eFunctions.Extensions" %> 
<script type="text/javascript" language="JavaScript">

    function ShowEquiposElectronicos() {

        ShowPopUp('/VTimeNet/Common/CMU701.aspx?nType=3', 'dump', 600, 200);

    }


    function massiveChargeElectricEquipment(value) {

        ShowPopUp('/VTimeNet/policy/policyseq/MU700.aspx_files/MU700_Massive_ElectricEquipment.aspx', 'dump', 510, 100);

    }

    function InputOnChangeEquipElect(inputControl) {
        switch (inputControl.name) {

            case 'NCAPITAL_EquipElect':

                var nCapitalValue = insConvertNumber(self.document.forms[0].NCAPITAL_EquipElect.value);
                var nRateValue = insConvertNumber(self.document.forms[0].NRATE_EquipElect.value);

                var nPremiumValue = 0;

                if (isNumber(nCapitalValue) && isNumber(nRateValue)) {
                    nPremiumValue = (parseFloat(nCapitalValue) * (parseFloat(nRateValue) / 1000));
                }
                else {
                    nPremiumValue = 0;
                }

                self.document.forms[0].NPREMIUM_EquipElect.value = nPremiumValue.toString().replace('.', ',');

                break;

            case 'NRATE_EquipElect':

                var nCapitalValue = insConvertNumber(self.document.forms[0].NCAPITAL_EquipElect.value);
                var nRateValue = insConvertNumber(self.document.forms[0].NRATE_EquipElect.value);

                var nPremiumValue = 0;

                if (isNumber(nCapitalValue) && isNumber(nRateValue)) {
                    nPremiumValue = (parseFloat(nCapitalValue) * (parseFloat(nRateValue) / 1000));
                }
                else {
                    nPremiumValue = 0;
                }

                self.document.forms[0].NPREMIUM_EquipElect.value = nPremiumValue.toString().replace('.', ',');

                break;

            default:
                break;
        }
    }

</script>
<script runat="server">

</script>
    <div>
        <table width="100%" >
<%
    
If Request.QueryString.Item("Type") <> "PopUp" orelse Request.QueryString.Item("gridName") = "EquipElect"  Then
%>        
            <tr>
                <td colspan="5" class="HighLighted">
                    <label><% Response.Write(resxValues.FindDictionaryValue("EquipElect_Title")) %></label>
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

                        insDefineHeader_EquipElec()
                        If Request.QueryString.Item("Type") <> "PopUp" Then
                            insPreMU700_EquipElect()
                        Else If Request.QueryString.Item("gridName") =  "EquipElect"  Then
                            insPreMU700Upd_EquipElect()
                        End If
    
                    %>
<%  If Request.QueryString.Item("Type") <> "PopUp"   Then
                   Response.Write("</div>") 
  End If  %>
                </td>
            </tr>
<%
If Request.QueryString.Item("Type") <> "PopUp" Then    
%>
            <tr>
                <td width="2%">

                    <%--<%=mobjValues.CheckControl("chkMassChargEquipElect", resxValues.FindDictionaryValue("MASSIVECHARGE_EquipElect_Caption"), ,  ,"massiveCharge(1);"  , ,  , resxValues.FindDictionaryValue("MASSIVECHARGE_EquipElect_ToolTip") ) %>--%>

                    <%=mobjValues.AnimatedButtonControl("btnCargaMasiva", "/VTimeNet/images/batchStat03.png", "Carga masiva equipos electronicos", , "massiveChargeElectricEquipment(this)", False) %>

                </td>
                <td>
                    <label>
                        <%=resxValues.FindDictionaryValue("MASSIVECHARGE_EquipElect_Caption") %>
                    </label>
                </td>
                <td>
                </td>
                <td style="text-align: right">
                    <label id="0">
                        <%=resxValues.FindDictionaryValue("Show_EquipElect_Caption") %>
                    </label>
                </td>
                <td width="2%" style="text-align: right">
                    <%=mobjValues.AnimatedButtonControl("btnShowEquiposElectronicos", "/VTimeNet/images/btn_ValuesOff.png", resxValues.FindDictionaryValue("Show_EquipElect_ToolTip"), , "ShowEquiposElectronicos()", False) %>
                </td>
            </tr>
        </table>
<%
End If    
%>
    </div>

