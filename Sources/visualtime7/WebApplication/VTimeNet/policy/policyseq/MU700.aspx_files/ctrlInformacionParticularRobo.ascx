<%@ Control Language="VB" ClassName="ctrlInformacionParticularRobo" AutoEventWireup="false"
    CodeFile="ctrlInformacionParticularRobo.ascx.vb" Inherits="ctrlInformacionParticularRobo"
    EnableViewState="false" %>
<%@ Import Namespace="eFunctions.Extensions" %>

<script type="text/javascript" language="javascript">
    function onChangeInsured(field) {
        if (field.value != '') {
            top.frames['fraFolder'].document.forms[0].tcnTheftCapital.value = '';
        }
    }
    function onChangeTheftCapital(field) {
        if (field.value != '') {
            top.frames['fraFolder'].document.forms[0].tcnInsured.value = '';
        }
    }
    function Session_NINSURED(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NINSURED", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }

    function Session_NTHEFTCAPITAL(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NTHEFTCAPITAL", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }
    function Session_NSECURITYMEN(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NSECURITYMEN", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }

    function Session_NAREA(Field) {
        var lstrString;

        lstrString_Val = 'sSession=' + Field.value;

        insDefValues("NAREA", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
    }



    
    
</script>
<script runat="server">

</script>
<%
    
    If (Request.QueryString("sOnSeq") = "1" AndAlso Request.QueryString("fromDelete") <> "1") Then
        Response.Write("<script> ")
        Response.Write(" cleanValueFromStorage(); ")
        Response.Write("</script> ")
    End If
    
    If Request.QueryString.Item("Type") <> "PopUp" Then
%>
<div>
    <table width="100%">
        <tr>
            <td colspan="4" class="HighLighted">
                <label>
                    <%=resxValues.FindDictionaryValue("ParticularlyTheft_MultiRisk_Title") %></label>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="Horline">
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    <%=resxValues.FindDictionaryValue("NINSURED_MultiRisk_Caption") %></label>
            </td>
            <td>
                <%
    Response.Write(mObjValues.NumericControl("tcnInsured", 5, IIf(mObjInformacionParticularRobo.NINSURED <>  eRemoteDB.Constants.intNull , mObjInformacionParticularRobo.NINSURED, Session("NINSURED")), True, resxValues.FindDictionaryValue("NINSURED_MultiRisk_ToolTip"), True, 2, , , , "onChangeInsured(this); setValueToStorage('tcnInsured') ;Session_NINSURED(this);", False))
                %>
            </td>
            <td>
                <label>
                    <%=resxValues.FindDictionaryValue("NTHEFTCAPITAL_MultiRisk_Caption") %>
                </label>
            </td>
            <td>
                <%
                    Response.Write(mObjValues.NumericControl("tcnTheftCapital", 18, IIf(mObjInformacionParticularRobo.NTHEFTCAPITAL <>  eRemoteDB.Constants.intNull , mObjInformacionParticularRobo.NTHEFTCAPITAL, Session("NTHEFTCAPITAL")), , resxValues.FindDictionaryValue("NTHEFTCAPITAL_MultiRisk_ToolTip"), True, 6, , , , "onChangeTheftCapital(this); setValueToStorage('tcnTheftCapital') ; Session_NTHEFTCAPITAL(this);  ", False))
                %>
            </td>
        </tr>
        <tr>
            <td>
                <label>
                    <%=resxValues.FindDictionaryValue("NSECURITYMEN_MultiRisk_Caption") %></label>
            </td>
            <td>
                <%
                    Response.Write(mObjValues.NumericControl("tcnSecurityMen", 5, IIf(mObjInformacionParticularRobo.NSECURITYMEN <> eRemoteDB.Constants.intNull, mObjInformacionParticularRobo.NSECURITYMEN, Session("NSECURITYMEN")), True, resxValues.FindDictionaryValue("NSECURITYMEN_MultiRisk_ToolTip"), True, 2, , , , "setValueToStorage('tcnSecurityMen') ; Session_NSECURITYMEN(this); ", False))
                %>
            </td>
            <td>
                <label>
                    <%=resxValues.FindDictionaryValue("NAREA_MultiRisk_Caption") %>
                </label>
            </td>
            <td>
                <%
                    Response.Write(mObjValues.NumericControl("tcnArea", 8, IIf(mObjInformacionParticularRobo.NAREA <> eRemoteDB.Constants.intNull, mObjInformacionParticularRobo.NAREA, Session("NAREA")), , resxValues.FindDictionaryValue("NAREA_MultiRisk_ToolTip"), True, , , , , "setValueToStorage('tcnArea');Session_NAREA(this);", , , False))
                %>
            </td>
        </tr>
    </table>
    <%
    
        If mObjInformacionParticularRobo.NINSURED = 0 OrElse mObjInformacionParticularRobo.NINSURED = eRemoteDB.Constants.intNull Then
        
            mObjInformacionParticularRobo.NINSURED = mObjValues.StringToType(Request.Form.Item("tcnInsured"), eFunctions.Values.eTypeData.etdInteger)

            If mObjInformacionParticularRobo.NINSURED = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnInsured'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnInsured.value = getValueFromStorage('tcnInsured'); ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnInsured.onblur(); ")
                Response.Write(" } ")
                Response.Write("</script> ")
            End If

        End If
    
        If mObjInformacionParticularRobo.NTHEFTCAPITAL = 0 OrElse mObjInformacionParticularRobo.NTHEFTCAPITAL = eRemoteDB.Constants.intNull Then
        
            mObjInformacionParticularRobo.NTHEFTCAPITAL = mObjValues.StringToType(Request.Form.Item("tcnTheftCapital"), eFunctions.Values.eTypeData.etdInteger)

            If mObjInformacionParticularRobo.NTHEFTCAPITAL = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnInsured'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnTheftCapital.value = getValueFromStorage('tcnTheftCapital'); ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnTheftCapital.onblur(); ")
                Response.Write(" } ")
                Response.Write("</script> ")
            End If

        End If
    
        If mObjInformacionParticularRobo.NSECURITYMEN = 0 OrElse mObjInformacionParticularRobo.NSECURITYMEN = eRemoteDB.Constants.intNull Then
        
            mObjInformacionParticularRobo.NSECURITYMEN = mObjValues.StringToType(Request.Form.Item("tcnSecurityMen"), eFunctions.Values.eTypeData.etdInteger)

            If mObjInformacionParticularRobo.NSECURITYMEN = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnSecurityMen'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnSecurityMen.value = getValueFromStorage('tcnSecurityMen'); ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnSecurityMen.onblur(); ")
                Response.Write(" } ")
                Response.Write("</script> ")
            End If

        End If
    
        If mObjInformacionParticularRobo.NAREA = 0 OrElse mObjInformacionParticularRobo.NINSURED = eRemoteDB.Constants.intNull Then
        
            mObjInformacionParticularRobo.NAREA = mObjValues.StringToType(Request.Form.Item("tcnArea"), eFunctions.Values.eTypeData.etdInteger)

            If mObjInformacionParticularRobo.NAREA = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnArea'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnArea.value = getValueFromStorage('tcnArea'); ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnArea.onblur(); ")
                Response.Write(" } ")
                Response.Write("</script> ")
            End If

        End If
    
    End If
    %>
</div>
