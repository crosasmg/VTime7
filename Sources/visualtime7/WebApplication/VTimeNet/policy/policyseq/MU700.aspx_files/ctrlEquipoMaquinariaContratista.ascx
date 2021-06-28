<%@ Control Language="VB" ClassName="ctrlEquipoMaquinariaContratista" AutoEventWireup="false"  Strict="false" CodeFile="ctrlEquipoMaquinariaContratista.ascx.vb" Inherits="ctrlEquipoMaquinariaContratista" EnableViewState="false" %>

<%@ Import Namespace="eFunctions.Extensions" %>

<script type="text/javascript" language="JavaScript">

    function ShowMaquinariasContratistas() {

        ShowPopUp('/VTimeNet/Common/CMU702.aspx?nType=5', 'dump', 600, 200);

    }

    function massiveMaquiContract(value) {

        ShowPopUp('/VTimeNet/policy/policyseq/MU700.aspx_files/MU700_Massive_MaquiContract.aspx', 'dump', 510, 100);

    }

    function InputOnChangeMaquiContract(inputControl) {
        switch (inputControl.name) {

            case 'NCAPITAL_EquipMaquiContr':

                var nCapitalValue = insConvertNumber(self.document.forms[0].NCAPITAL_EquipMaquiContr.value);
                var nRateValue = insConvertNumber(self.document.forms[0].NRATE_EquipMaquiContr.value);

                var nPremiumValue = 0;

                if (isNumber(nCapitalValue) && isNumber(nRateValue)) {
                    nPremiumValue =  (parseFloat(nCapitalValue) * (parseFloat(nRateValue) / 1000));
                }
                else {
                    nPremiumValue = 0;
                }

                self.document.forms[0].NPREMIUM_EquipMaquiContr.value = nPremiumValue.toString().replace('.', ',');

                break;

            case 'NRATE_EquipMaquiContr':

                var nCapitalValue = insConvertNumber(self.document.forms[0].NCAPITAL_EquipMaquiContr.value);
                var nRateValue = insConvertNumber(self.document.forms[0].NRATE_EquipMaquiContr.value);

                var nPremiumValue = 0;

                if (isNumber(nCapitalValue) && isNumber(nRateValue)) {
                    nPremiumValue =  (parseFloat(nCapitalValue) * (parseFloat(nRateValue) / 1000));
                }
                else {
                    nPremiumValue = 0;
                }

                self.document.forms[0].NPREMIUM_EquipMaquiContr.value = nPremiumValue.toString().replace('.', ',');

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
<%="" %>
<%

If Request.QueryString.Item("Type") <> "PopUp" orelse Request.QueryString.Item("gridName") = "EquipMaquiContr"  Then
        
%> 
            <tr>
                <td colspan="5" class="HighLighted">
                    <label> <%=resxValues.FindDictionaryValue("EquipMaquiContr_Title") %>
                        </label>
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
<%  If Request.QueryString.Item("Type") <> "PopUp"  Then
                   Response.Write("<div style='overflow-y:scroll; height:150px;'>") 
  End If  %>
                    <%
                        insDefineHeader_EquipMaquiContr()
                        If Request.QueryString.Item("Type") <> "PopUp" Then
                            insPreMU700_EquipMaquiContr()
                        Else If Request.QueryString.Item("gridName") =  "EquipMaquiContr"  Then
                            insPreMU700Upd_EquipMaquiContr()
                        End If
    
                    %>
<%  If Request.QueryString.Item("Type") <> "PopUp"  Then
                   Response.Write("</div>") 
  End If  %>

                </td>
            </tr>
<%
    If Request.QueryString.Item("Type") <> "PopUp" Then    
%>
            <tr>
                <td width="2%">
                    
                    <%=mobjValues.AnimatedButtonControl("btnCargaMasiva", "/VTimeNet/images/batchStat03.png", "Carga masiva equipo y maquinaria de contratistas", , "massiveMaquiContract(this)", False) %>

                </td>
                <td>
                    <label>
                        <%=resxValues.FindDictionaryValue("MASSIVECHARGE_EquipMaquiContr_Caption") %>
                    </label>
                </td>

                <td>
                </td>
                <td style="text-align: right">
                    <label id="LABEL2">
                        <%=resxValues.FindDictionaryValue("Show_EquipMaquiContr_Caption")  %>
                    </label>
                </td>
                <td width="2%" style="text-align: right">
                    <%=mobjValues.AnimatedButtonControl("btnShowMaquinariasContratistas", "/VTimeNet/images/btn_ValuesOff.png", resxValues.FindDictionaryValue("Show_EquipMaquiContr_ToolTip") , , "ShowMaquinariasContratistas()", False) %>
                </td>
            </tr>
<%
    End If
%>
        </table>
    </div>