<%@ Control Language="VB" ClassName="ctrlFidelidad" AutoEventWireup="false"  Strict="false" CodeFile="ctrlFidelidad.ascx.vb" Inherits="ctrlFidelidad" EnableViewState="false" %>
 
 
<%@ Import Namespace="eFunctions.Extensions" %>
 

<script type="text/javascript" language="JavaScript"> 

    function ShowFidelidadPrivada() { 

        ShowPopUp('/VTimeNet/Common/CMU700.aspx?nType=6', 'dump', 600, 200);

    }

    function massiveChargeFidelity(value) {

        ShowPopUp('/VTimeNet/policy/policyseq/MU700.aspx_files/MU700_Massive_Fidelity.aspx', 'dump', 510, 100);

    }

    function InputOnChange(fidelityInputControl) {
        var nFI_POLICYTYPE = '<%=Request.QueryString("nFI_POLICYTYPE")%>'
        switch (fidelityInputControl.name) {

            case 'NSALARY_Fidelity':

                var fieldnSalaryValue = self.document.forms[0].NSALARY_Fidelity.value.toString().replace(',', '.');


                var fieldnFactorValue = self.document.forms[0].NFACTOR_Fidelity.value.toString().replace(',', '.');
                var fieldnValueValue = self.document.forms[0].NVALUE_Fidelity.value.toString().replace('.', ',');

                if (nFI_POLICYTYPE != 1) {
                    if (isNumber(fieldnSalaryValue) && isNumber(fieldnFactorValue)) {
                        fieldnValueValue = 0;
                    }
                    else {
                        fieldnValueValue = 0;
                    }
                }
                self.document.forms[0].NVALUE_Fidelity.value = fieldnValueValue;

                break;

            case 'NFACTOR_Fidelity':

                var fieldnSalaryValue = self.document.forms[0].NSALARY_Fidelity.value.toString().replace(',', '.');



                var fieldnFactorValue = self.document.forms[0].NFACTOR_Fidelity.value.toString().replace(',', '.');
                var fieldnValueValue = self.document.forms[0].NVALUE_Fidelity.value.toString().replace(',', '.'); ;

                if (isNumber(fieldnSalaryValue) && isNumber(fieldnFactorValue)) {
                    fieldnValueValue = 0;
                }
                else {
                    fieldnValueValue = 0;
                }

                self.document.forms[0].NVALUE_Fidelity.value = fieldnValueValue.toString().replace('.', ',');
                break;

            default:
                break;
        }
    }

    function isNumber(n) {
      return !isNaN(parseFloat(n)) && isFinite(n);
  }

  function onChangetcnFI_POLICYTYPE(fieldFI_POLICYTYPE) {
      Session_POLICYTYPE(fieldFI_POLICYTYPE);
      if (fieldFI_POLICYTYPE.value == '1') {
          document.getElementById("divGridFidelity").style.display = '';
          //self.document.forms[0].nFI_POLICYTYPE.value = fieldFI_POLICYTYPE.value;
      }
      else {
          document.getElementById("divGridFidelity").style.display = '';
      }
    }

    function InputOnChangeNpos(nposition) {
        self.document.forms[0].NFACTOR_Fidelity.value = self.document.forms[0].NPOSITION_Fidelity_NFACTOR.value;
       
    }

//------------------------------------------------------------------------------------------
    function insShowDefValue_Fidelity() {
//------------------------------------------------------------------------------------------
    var lstrQueryString;
	var lintBranch  = 0;

	lintClient = self.document.forms[0].SCLIENT_Fidelity.value

	if (lintClient != " ") {
	    insDefValues('ShowDataClientFidelity', '&sClient=' + lintClient, '/VTimeNet/policy/policyseq/');
    }
}



function Session_POLICYTYPE(Field) {
    var lstrString;

    lstrString_Val = 'sSession=' + Field.value;

    insDefValues("POLICYTYPE", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
}


function Session_NNUMBEROFEMPLOYEES(Field) {
    var lstrString;

    lstrString_Val = 'sSession=' + Field.value;

    insDefValues("NNUMBEROFEMPLOYEES", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
}

function Session_NINSURTYPE(Field) {
    var lstrString;

    lstrString = 'sSession=' + Field.value;

    insDefValues("NINSURTYPE", lstrString_Val, '/VTimeNet/Policy/PolicySeq');
}



</script>


<script runat="server">

</script>
    <div>
        <table width="100%">
<%="" %>
<%
    
    If (Request.QueryString("sOnSeq") = "1"  AndAlso Request.QueryString("fromDelete") <> "1" )   Then   
        Response.Write("<script> ")
        Response.Write(" cleanValueFromStorage(); ")
        Response.Write("</script> ")
    End If       

If Request.QueryString.Item("Type") <> "PopUp" OrElse Request.QueryString.Item("gridName") = "Fidelity" Then
%> 
            <tr>
                <td colspan="5" class="HighLighted">
                    <label> <%=resxValues.FindDictionaryValue("Fidelity_Title") %> </label>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="Horline">
                </td>
            </tr>
<%

    End If
    
    If Request.QueryString.Item("Type") <> "PopUp"  Then

 %>        
            <tr>
                <td>
                    <label> <%=resxValues.FindDictionaryValue("NFI_POLICYTYPE_Fidelity_Caption") %>
                        </label>
                </td>
                <td>
                    <%
                        Response.Write(mObjValues.PossiblesValues("tcnFI_POLICYTYPE", "TABLE7205", eFunctions.Values.eValuesType.clngComboType, IIf(mObjPuntualFidelity.nFI_POLICYTYPE <>  eRemoteDB.Constants.intNull , mObjPuntualFidelity.nFI_POLICYTYPE, Session("POLICYTYPE")), , , , , , "onChangetcnFI_POLICYTYPE(this); setValueToStorage(""tcnFI_POLICYTYPE""); ", , , resxValues.FindDictionaryValue("NFI_POLICYTYPE_Fidelity_ToolTip")))
                    %>
                </td>
                <td>
                    <label>
                        <%=resxValues.FindDictionaryValue("NNUMBEROFEMPLOYEES_Fidelity_Caption") %>
                    </label>
                </td>
                <td>
                    <%
                        Response.Write(mobjValues.NumericControl("tcnNUMBEROFEMPLOYEES", 5,   IIf(mObjPuntualFidelity.NNUMBEROFEMPLOYEES <>  eRemoteDB.Constants.intNull , mObjPuntualFidelity.NNUMBEROFEMPLOYEES, Session("NNUMBEROFEMPLOYEES"))   ,, resxValues.FindDictionaryValue("NNUMBEROFEMPLOYEES_Fidelity_ToolTip") , True,, false , 0,   ,  "setValueToStorage('tcnNUMBEROFEMPLOYEES');Session_NNUMBEROFEMPLOYEES(this);  "   ,   ,  , False))
                    %>
                </td>
            </tr>
            <tr>
                <td>
                    <label> <%=resxValues.FindDictionaryValue("NINSURTYPE_Fidelity_Caption") %>
                        </label>
                </td>
                <td>
                    <%
                        Response.Write(mObjValues.PossiblesValues("tcnINSURTYPE", "TABLE7206", eFunctions.Values.eValuesType.clngComboType, IIf(mObjPuntualFidelity.NINSURTYPE <>  eRemoteDB.Constants.intNull , mObjPuntualFidelity.NINSURTYPE, Session("NINSURTYPE")), , , , , ,  "setValueToStorage(""tcnINSURTYPE"");Session_NINSURTYPE(this);" , , , resxValues.FindDictionaryValue("NINSURTYPE_Fidelity_ToolTip")))
                    %>
                </td>
                <td colspan="3">
                </td>
            </tr>
        </table>
    </div>
<%
    
    

       If mObjPuntualFidelity.nFI_POLICYTYPE = 0 OrElse mObjPuntualFidelity.nFI_POLICYTYPE = eRemoteDB.Constants.intNull THEN
        
            mObjPuntualFidelity.nFI_POLICYTYPE = mObjValues.StringToType(Request.Form.Item("tcnFI_POLICYTYPE"), eFunctions.Values.eTypeData.etdInteger)

            If mObjPuntualFidelity.nFI_POLICYTYPE = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnFI_POLICYTYPE'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnFI_POLICYTYPE.value = getValueFromStorage('tcnFI_POLICYTYPE'); ")        
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnFI_POLICYTYPE.onblur(); ")        
                
                'Response.Write(" top.frames['fraFolder'].document.forms[0].tcnFI_POLICYTYPE.onblur(); ")       
                Response.Write(" } ")
                Response.Write("</script> ")
            End If 

        End if      
    
        If mObjPuntualFidelity.NNUMBEROFEMPLOYEES = 0 OrElse mObjPuntualFidelity.NNUMBEROFEMPLOYEES = eRemoteDB.Constants.intNull THEN
        
            mObjPuntualFidelity.NNUMBEROFEMPLOYEES = mObjValues.StringToType(Request.Form.Item("tcnNUMBEROFEMPLOYEES"), eFunctions.Values.eTypeData.etdInteger)

            If mObjPuntualFidelity.NNUMBEROFEMPLOYEES = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnNUMBEROFEMPLOYEES'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnNUMBEROFEMPLOYEES.value = getValueFromStorage('tcnNUMBEROFEMPLOYEES'); ")        
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnNUMBEROFEMPLOYEES.onblur(); ")        
                Response.Write(" } ")
                Response.Write("</script> ")
            End If 
        End If

           If mObjPuntualFidelity.NINSURTYPE = 0 OrElse mObjPuntualFidelity.NINSURTYPE = eRemoteDB.Constants.intNull THEN
        
            mObjPuntualFidelity.NINSURTYPE = mObjValues.StringToType(Request.Form.Item("tcnINSURTYPE"), eFunctions.Values.eTypeData.etdInteger)

            If mObjPuntualFidelity.NINSURTYPE = eRemoteDB.Constants.intNull Then
                Response.Write("<script> ")
                Response.Write(" if (isNumber(getValueFromStorage('tcnINSURTYPE'))) { ")
                Response.Write(" top.frames['fraFolder'].document.forms[0].tcnINSURTYPE.value = getValueFromStorage('tcnINSURTYPE'); ")        
                'Response.Write(" top.frames['fraFolder'].document.forms[0].tcnINSURTYPE.onblur(); ")        
                Response.Write(" } ")
                Response.Write("</script> ")
            End If        
        
        End if 
    

    
    End If
%>
<div>
<table width="100%">
            <tr>
                <td colspan="5">
<%  If Request.QueryString.Item("Type") <> "PopUp"   Then
                   Response.Write("<div id='divGridFidelity' style='overflow-y:scroll; height:150px;'>") 
  End If  %>
                    <%
   
                        insDefineHeader_Fidelity()
                        If Request.QueryString.Item("Type") <> "PopUp" Then
                            insPreMU700_Fidelity()
                        Else If Request.QueryString.Item("gridName") =  "Fidelity"  Then
                            insPreMU700Upd_Fidelity()
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
                    <%=mObjValues.AnimatedButtonControl("btnCargaMasiva", "/VTimeNet/images/batchStat03.png", "Carga masiva fidelidad privada", , "massiveChargeFidelity(this)", False)%>
                </td>
                <td>
                    <label>
                        <%=resxValues.FindDictionaryValue("MASSIVECHARGE_Fidelity_Caption") %>
                    </label>
                </td>
                <td>
                </td>
                <td style="text-align: right">
                    <label id="0">
                    <%=resxValues.FindDictionaryValue("Show_Fidelity_Caption") %>
                    </label>
                </td>
                <td width="2%" style="text-align: right">
                    <%=mobjValues.AnimatedButtonControl("btnShowFidelidadPrivada", "/VTimeNet/images/btn_ValuesOff.png", resxValues.FindDictionaryValue("Show_Fidelity_ToolTip") , , "ShowFidelidadPrivada()", False) %>
                </td>
            </tr>
<%

        Response.Write("<script> ")
        Response.Write(" onChangetcnFI_POLICYTYPE(top.frames['fraFolder'].document.forms[0].tcnFI_POLICYTYPE); ")
    Response.Write("</script> ")
    
    
    End If
%>
        </table>
    </div>