<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim mobjNull_condi As ePolicy.Null_condi
Dim mstrCodisplOri As String
Dim mstrCertype As String


'% insPreCA034: 
'---------------------------------------------------------------------------
Sub insPreCA034()
	'---------------------------------------------------------------------------	
	With Request
		If IsNothing(.QueryString("sCodisplOri")) Then
			mstrCodisplOri = "CA034"
		Else
			mstrCodisplOri = .QueryString.Item("sCodisplOri")
		End If
		
		Session("sCodispl") = mstrCodisplOri
		
		Call mobjNull_condi.insPreCA033_k(mstrCodisplOri, Session("nOperat"), mobjValues.StringToType(.QueryString.Item("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nPolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("nCertif"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate))
	End With
End Sub

</script>
<%mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA034_K"
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca034_k")
mobjNull_condi = New ePolicy.Null_condi
'+ Se hace carga inicial de datos
Call insPreCA034()

%>
<HTML>
<HEAD>
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 12 $|$$Date: 13/10/04 12:12 $|$$Author: Nvaplat28 $"
</SCRIPT>
	<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>


    <%
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjMenu.MakeMenu("CA034", "CA034_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
mobjMenu = Nothing
%>
<SCRIPT>

    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 12 $|$$Date: 13/10/04 12:12 $"

    //% insCancel: se controla la acción Cancelar de la página
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    }
    //%insStateZone: se controla el estado de los campos de la página
    //------------------------------------------------------------------------------------------
    function insStateZone() {
        //------------------------------------------------------------------------------------------
    }
    //% insChangeField: Se recargan los valores cuando cambia el campo
    //-------------------------------------------------------------------------------------------
    function insChangeField(Field) {
        //-------------------------------------------------------------------------------------------    
        with (self.document.forms[0]) {
            switch (Field.name) {
                case "tcnPolicy":
                    insDefValues("Policy_CA099", "sCodispl=CA034&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy= " + tcnPolicy.value)
                    break;
            }
        }
    }
    //% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
    //%	campo SUCURSAL cambia
    //-------------------------------------------------------------------------------------
    function BlankOfficeDepend()
    //-------------------------------------------------------------------------------------
    {
        with (document.forms[0]) {
            cbeOfficeAgen.value = "";
            cbeAgency.value = "";
        }
        UpdateDiv('cbeOfficeAgenDesc', '');
        UpdateDiv('cbeAgencyDesc', '');
    }

    //% insInitialAgency: manejo de sucursal/oficina/agencia
    //-------------------------------------------------------------------------------------------
    function insInitialAgency(nInd) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            //+ Cambia la sucursal 
            switch (nInd) {
                case 1:
                    if (typeof (cbeOffice) != 'undefined') {
                        if (cbeOffice.value != 0) {
                            if (typeof (cbeOfficeAgen) != 'undefined') {
                                cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                cbeOfficeAgen.Parameters.Param2.sValue = 0;
                                cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0)
                                    cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                                else
                                    cbeAgency.Parameters.Param2.sValue = 0;
                            }
                        }
                        else {
                            if (typeof (cbeOfficeAgen) != 'undefined') {
                                cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                cbeOfficeAgen.Parameters.Param2.sValue = 0;
                                cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                                if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0)
                                    cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                                else
                                    cbeAgency.Parameters.Param2.sValue = 0;
                            }
                        }
                    }
                    break;

                //+ Cambia la oficina  
                case 2:
                    if (cbeOfficeAgen.value != "" && cbeOfficeAgen.value > 0) {
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                        cbeOffice.value = cbeOfficeAgen_nBran_off.value;
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                    }
                    else {
                        cbeAgency.Parameters.Param1.sValue = 0;
                        cbeAgency.Parameters.Param2.sValue = 0;
                    }
                    break;
                //+ Cambia la Agencia			  
                case 3:
                    if (cbeAgency.value != "") {
                        cbeOffice.value = cbeAgency_nBran_off.value;
                        if (cbeOfficeAgen.value == '') {
                            cbeOfficeAgen.value = cbeAgency_nOfficeAgen.value;
                            UpdateDiv('cbeOfficeAgenDesc', cbeAgency_sDesAgen.value);
                        }
                        cbeOfficeAgen.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                        cbeAgency.Parameters.Param1.sValue = (cbeOffice.value == '' ? 0 : cbeOffice.value);
                        cbeAgency.Parameters.Param2.sValue = (cbeOfficeAgen.value == '' ? 0 : cbeOfficeAgen.value);
                    }
            }
        }
    }

    //% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para
    //%	producto, póliza o certificado
    //-------------------------------------------------------------------------------------------
    function ShowChangeValues(sField) {
        //-------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            switch (sField) {
                case "Agency":
                    if (cbeAgency.value != "")
                        insDefValues(sField, "nAgency=" + cbeAgency.value + "&nOfficeAgen=" + cbeOfficeAgen.value + "&nOffice=" + cbeOffice.value, '/VTimeNet/Policy/PolicySeq')
                    break;
            }
        }
    }
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmReahPolicy_K" ACTION="ValPolicyTra.aspx?x=1&nProponum=<%=Request.QueryString.Item("npolicy")%>">
	<BR></BR>
	<TABLE WIDTH=100%>
		<TR>
			<TD WIDTH=25%><LABEL ID=13901><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD>
				<%If Request.QueryString.Item("sCertype") = vbNullString Then
	mstrCertype = "2"
Else
	mstrCertype = Request.QueryString.Item("sCertype")
End If
Response.Write(mobjValues.HiddenControl("tctCertype", mstrCertype))

If mstrCodisplOri = "CA767" Then
	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct",  ,  ,  ,  , True))
Else
	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Request.QueryString.Item("nBranch"), "valProduct"))
End If
%>
			</TD>
		</TR>
		<TR>
			<TD><LABEL ID=13909><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%
If mstrCodisplOri = "CA767" Then
	Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType, True, Request.QueryString.Item("nProduct")))
Else
	Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), Request.QueryString.Item("nBranch"), eFunctions.Values.eValuesType.clngWindowType,  , Request.QueryString.Item("nProduct")))
End If
%></TD>
		<TD CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		</TR>
		<TR>
			<TD COLSPAN="2"></TD>
			<TD CLASS="HorLine"></TD>
		</TR>
			
		<TR>			
		    <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
			<TD><%If mstrCodisplOri = "CA767" Then
	Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nProponum"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangeField(tcnPolicy);", True))
Else
	Response.Write(mobjValues.NumericControl("tcnPolicy", 10, Request.QueryString.Item("nPolicy"),  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "insChangeField(tcnPolicy);"))
End If
Response.Write(mobjValues.hiddencontrol("optProcess", "1"))
						
%> 

<%If mstrCodisplOri = "CA034" Then%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), mobjNull_condi.DefaultValueCA033("optExecutePre"), "1")%></TD>
			<%Else%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), mobjNull_condi.DefaultValueCA033("optExecutePre"), "1",  , Not (mobjNull_condi.DefaultValueCA033("optExecuteEnabled")))%></TD>
			<%End If%>

		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCertifCaption") %></LABEL></TD>
			<TD><%If mstrCodisplOri = "CA767" Then
	Response.Write(mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip"),  ,  ,  ,  ,  ,  , True))
Else
	Response.Write(mobjValues.NumericControl("tcnCertif", 10, CStr(0),  , GetLocalResourceObject("tcnCertifToolTip")))
End If
%></TD>
<%If mstrCodisplOri = "CA034" Then%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), mobjNull_condi.DefaultValueCA033("optExecuteDef"), "2")%></TD>
			<%Else%>
				<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"), mobjNull_condi.DefaultValueCA033("optExecuteDef"), "2",  , Not (mobjNull_condi.DefaultValueCA033("optExecuteEnabled")))%></TD>
			<%End If%>
		</TR>
		<TR>
		    <TD><LABEL ID=13803><%= GetLocalResourceObject("tcnServ_orderCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%If mstrCodisplOri = "CA767" Then
	Response.Write(mobjValues.NumericControl("tcnServ_order", 10,  ,  , GetLocalResourceObject("tcnServ_orderToolTip"),  ,  ,  ,  ,  ,  , True))
Else
	Response.Write(mobjValues.NumericControl("tcnServ_order", 10,  ,  , GetLocalResourceObject("tcnServ_orderToolTip")))
End If
%></TD>
			
		<TR>
		    <TD><LABEL ID=13378><%= GetLocalResourceObject("cbeOfficeCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%With mobjValues
	.TypeOrder = 1
	If mstrCodisplOri = "CA767" Then
		Response.Write(.PossiblesValues("cbeOffice", "Table9", 1, Session("nOffice"),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)", True,  , GetLocalResourceObject("cbeOfficeToolTip")))
	Else
		Response.Write(.PossiblesValues("cbeOffice", "Table9", 1, Session("nOffice"),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip")))
	End If
	.TypeOrder = 2
End With
%>
			</TD>
			

			
		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeOfficeAgenCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	If mstrCodisplOri = "CA767" Then
		Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)", True,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	End If
End With
%>
			</TD>

		</TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeAgencyCaption") %></LABEL></TD>
			<TD COLSPAN="4"><%With mobjValues
	.Parameters.Add("nOfficeAgen", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nAgency", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nBran_off",  ,  , True)
	.Parameters.ReturnValue("nOfficeAgen",  ,  , True)
	.Parameters.ReturnValue("sDesAgen",  ,  , True)
	If mstrCodisplOri = "CA767" Then
		Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(mobjNull_condi.nAgency), True,  ,  ,  ,  , "insInitialAgency(3)", True,  , GetLocalResourceObject("cbeAgencyToolTip")))
	Else
		Response.Write(.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(mobjNull_condi.nAgency), True,  ,  ,  ,  , "insInitialAgency(3)",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
	End If
End With
%>
			</TD>
		</TR>
	</TABLE>
	<SCRIPT>	    insInitialAgency(1)</SCRIPT>
<%
Response.Write(mobjValues.HiddenControl("hddCodisplOri", Request.QueryString.Item("sCodisplOri")))
%>
</FORM>
</BODY>
</HTML>
<%
mobjNull_condi = Nothing
mobjValues = Nothing
If mstrCodisplOri = "CA767" Then
	Response.Write("<SCRIPT>insInitialAgency(3);</SCRIPT>")
	Response.Write("<SCRIPT>insChangeField('tcnPolicy');</SCRIPT>")
End If
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca034_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




