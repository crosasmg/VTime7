<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
    '^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.47
    Dim mobjNetFrameWork As eNetFrameWork.Layout
    '~End Header Block VisualTimer Utility
    '- Objeto para el manejo de las funciones generales de carga de valores
    Dim mobjValues As eFunctions.Values
    '- Objeto para el manejo del menú
    Dim mobjMenu As eFunctions.Menues
    '- Objeto para el manejo particular de los datos de la página
    Dim mcolPremiums As Object
    Dim mstrString As String
    Dim mblnDisabled As Boolean
    Dim mstrBranch As Object
    Dim mstrProduct As Object
    Dim mstrPolicy As Object
    Dim mstrCertif As String
    Dim mdtmEffecdate As Object

    Sub LoadHeader()
        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">    " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40505><A NAME=""Operación"">" & GetLocalResourceObject("AnchorOperaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("tcdOperationCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.DateControl("tcdOperation", CStr(Today), , GetLocalResourceObject("tcdOperationToolTip"), , , , , True))

        Response.Write(mobjValues.HiddenControl("cbeInsur_area", Session("nInsur_area")))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
        Response.Write("			    <TD>" & vbCrLf)
        Response.Write("					")

        If mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
            Response.Write("" & vbCrLf)
            Response.Write("					")


            Response.Write(mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_1Caption"), "1", "1", , True))


            Response.Write("" & vbCrLf)
            Response.Write("					")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("					")


            Response.Write(mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_1Caption"), "2", "1", , True))


            Response.Write("" & vbCrLf)
            Response.Write("					")

        End If
        Response.Write("</TD>" & vbCrLf)
        Response.Write("				<TD>" & vbCrLf)
        Response.Write("					")

        If mobjValues.StringToType(Request.QueryString.Item("nTypOper"), eFunctions.Values.eTypeData.etdDouble) = 1 Then
            Response.Write("" & vbCrLf)
            Response.Write("					")


            Response.Write(mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_2Caption"), "", "2", "insChangeTypOper(this)", True))


            Response.Write("" & vbCrLf)
            Response.Write("					")

        Else
            Response.Write("" & vbCrLf)
            Response.Write("					")


            Response.Write(mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_2Caption"), "1", "2", "insChangeTypOper(this)", True))


            Response.Write("" & vbCrLf)
            Response.Write("					")

        End If
        Response.Write("" & vbCrLf)
        Response.Write("			    </TD>        " & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("	</TABLE>                " & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=40506><A NAME=""Datos de la suspensión"">" & GetLocalResourceObject("AnchorDatos de la suspensiónCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""6"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("tcdCollSus_iniCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.DateControl("tcdCollSus_ini", Request.QueryString.Item("dCollSus_ini"), , GetLocalResourceObject("tcdCollSus_iniToolTip"), , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("tcdCollSus_endCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.DateControl("tcdCollSus_end", Request.QueryString.Item("dCollSus_end"), , GetLocalResourceObject("tcdCollSus_endToolTip"), , , , , True))


        Response.Write("</TD>                                " & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeSus_reasonCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeSus_reason", "Table5566", eFunctions.Values.eValuesType.clngComboType, Request.QueryString.Item("nSus_reason"), , , , , , , True, , GetLocalResourceObject("cbeSus_reasonToolTip")))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("	</TABLE>" & vbCrLf)
        Response.Write("	")

        mstrString = Request.Params.Get("Query_String")
        With Response
            .Write("<SCRIPT>insShowFolder('" & Request.Params.Get("Query_String") & "');")
            '.Write "UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>','');"
            '.Write "setTimeout(""top.fraFolder.document.location ='CO633A.aspx?sCodispl=CO633A&" & Request.QueryString & ";'"",300);"
            .Write("</" & "Script>")
        End With
    End Sub
    '**************************************************************************************
    Sub LoadFolder()
        'Response.Write("<SCRIPT>alert(""Branch " & Request.QueryString.Item("nBranch") & " , Product " & Request.QueryString.Item("nProduct") & " , Policy " & Request.QueryString.Item("nPolicy") & " , Certif " & Request.QueryString.Item("nCertif") & " " & """);</" & "Script>")
        Response.Write("" & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">    " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40505><A NAME=""Operación"">" & GetLocalResourceObject("AnchorOperaciónCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD WIDTH=""15%""><LABEL ID=0>" & GetLocalResourceObject("tcdOperationCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        Response.Write(mobjValues.DateControl("tcdOperation", CStr(Today), , GetLocalResourceObject("tcdOperationToolTip"), , , , , True))

        Response.Write(mobjValues.HiddenControl("cbeInsur_area", Session("nInsur_area")))

        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD>&nbsp</TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_1Caption"), "1", "1", "insChangeTypOper(this)", True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.OptionControl(0, "optTypOper", GetLocalResourceObject("optTypOper_2Caption"), "", "2", "insChangeTypOper(this)", True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("	</TABLE>	                " & vbCrLf)
        Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD WIDTH=""45%"" COLSPAN=""2"" CLASS=""HighLighted""><LABEL ID=40506><A NAME=""Póliza"">" & GetLocalResourceObject("AnchorPólizaCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""5"" CLASS=""HighLighted""><LABEL ID=40506><A NAME=""Período de la suspensión"">" & GetLocalResourceObject("AnchorPeríodo de la suspensiónCaption") & "</A></LABEL></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		    <TD></TD>" & vbCrLf)
        Response.Write("		    <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, mstrBranch, , , , , , "if(typeof(document.forms[0].valProduct)!=""undefined"")document.forms[0].valProduct.Parameters.Param1.sValue=this.value; insChangeBranch(this)", True, , GetLocalResourceObject("cbeBranchToolTip")))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdCollSus_iniCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD>")


        Response.Write(mobjValues.DateControl("tcdCollSus_ini", , , GetLocalResourceObject("tcdCollSus_iniToolTip"), , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>	" & vbCrLf)
        Response.Write("			<TD>")

        With mobjValues
            .Parameters.Add("nBranch", mstrBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, mstrProduct, True, , , , , , True, 6, GetLocalResourceObject("valProductToolTip")))
        End With
	
        Response.Write("" & vbCrLf)
        Response.Write("			</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdCollSus_endCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("		    <TD><TABLE>" & vbCrLf)
        Response.Write("		           <TR>" & vbCrLf)
        Response.Write("		               <TD>")


        Response.Write(mobjValues.DateControl("tcdCollSus_end", , , GetLocalResourceObject("tcdCollSus_endToolTip"), , , , , True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		               <TD>")


        Response.Write(mobjValues.CheckControl("chkDef", GetLocalResourceObject("chkDefCaption"), CStr(False), , "insChekDef();", True, , GetLocalResourceObject("chkDefToolTip")))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		           </TR>" & vbCrLf)
        Response.Write("		        </TABLE>" & vbCrLf)
        Response.Write("		    </TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnPolicy", 10, mstrPolicy, , GetLocalResourceObject("tcnPolicyToolTip"), , , , , , "insShowValues(""Policy"")", True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeSus_reasonCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.PossiblesValues("cbeSus_reason", "Table5566", eFunctions.Values.eValuesType.clngComboType, , , , , , , , True, , GetLocalResourceObject("cbeSus_reasonToolTip")))


        Response.Write(" </TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("		<TR>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD>")


        Response.Write(mobjValues.NumericControl("tcnCertif", 8, mstrCertif, , GetLocalResourceObject("tcnCertifToolTip"), , , , , , "insShowValues(""Certif"")", True))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
        Response.Write("			<TD><LABEL ID=15071>" & GetLocalResourceObject("valUsercodeCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("			<TD COLSPAN=""1"">")


        Response.Write(mobjValues.PossiblesValues("valUsercode", "tabUsers", eFunctions.Values.eValuesType.clngWindowType, Session("nUsercode"), False, , , , , , True, 4, GetLocalResourceObject("valUsercodeToolTip"), eFunctions.Values.eTypeCode.eNumeric, 1))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("		</TR>" & vbCrLf)
        Response.Write("    </TABLE>")

    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co633_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co633_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 5 $|$$Date: 18/10/04 17:32 $|$$Author: Nvaplat40 $"
	     
var ldtmExpirDat	     

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
//+ Se habilitan los campos por defecto
    with(self.document.forms[0]){
//+ Si el tipo de operación es suspensión el campo causa está habilitado de lo contrario no.
		optTypOper[0].disabled = false;
		optTypOper[1].disabled = false;
		cbeBranch.disabled = false;
		valProduct.disabled = false;
		btnvalProduct.disabled = false;
		tcnPolicy.disabled = false;
		tcnCertif.disabled = false;
		cbeSus_reason.disabled = false;
		tcdCollSus_ini.disabled = false;
		btn_tcdCollSus_ini.disabled = false;
		tcdCollSus_end.disabled = false;
		btn_tcdCollSus_end.disabled = false;
		chkDef.disabled = false;
	}
}

//% insChekDef: actualiza los valores de los campos de fecha al seleccionar Suspensión Definitiva
//--------------------------------------------------------------------------------------------
function insChekDef(){
//--------------------------------------------------------------------------------------------
//+ Se habilitan los campos por defecto
    with(self.document.forms[0]){
        if (chkDef.checked==true){
            tcdCollSus_end.value = ldtmExpirDat;
            tcdCollSus_end.disabled = true;
        }
        else{
            tcdCollSus_end.value = ''
            tcdCollSus_end.disabled = false;
        } 
	}
}



//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
	return true;
}

//-------------------------------------------------------------------------------------------
function insChangeTypOper(Field){
//-------------------------------------------------------------------------------------------
	
    with(self.document.forms[0]){
//+ Si el tipo de operación es suspensión el campo causa está habilitado de lo contrario no.
		cbeSus_reason.disabled = !(Field.value==1?true:false);
		//tcdCollSus_ini.disabled = !(Field.value==1?true:false);
		btn_tcdCollSus_ini.disabled = !(Field.value==1?true:false);
		tcdCollSus_end.disabled = !(Field.value==1?true:false);
		btn_tcdCollSus_end.disabled = !(Field.value==1?true:false);
		chkDef.disabled = !(Field.value==1?true:false);
	}
}

//-------------------------------------------------------------------------------------------
function insChangeTypDoc(Field){
//-------------------------------------------------------------------------------------------
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false
	var lblnDisabled = (Field.value==1?false:true)
	
    with(self.document.forms[0]){
		cbeBranch.disabled = lblnDisabled;
		cbeBranch.value='';
		valProduct.disabled = lblnDisabled;
		btnvalProduct.disabled = valProduct.disabled;
		valProduct.value='';
		UpdateDiv('valProductDesc', '');
		tcnPolicy.disabled = lblnDisabled;
		tcnPolicy.value='';
		tcnCertif.disabled = lblnDisabled;
		tcnCertif.value='';
		tcdCollSus_ini.value='';
		tcdCollSus_end.value='';
		cbeSus_reason.value='';
	}
}

//-------------------------------------------------------------------------------------------
function insShowFolder(sQueryString){
//-------------------------------------------------------------------------------------------
    UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>','');
    setTimeout("top.fraFolder.document.location ='CO633A.aspx?sCodispl=CO633A&" + sQueryString + "'",500);
}

//-------------------------------------------------------------------------------------------
function insChangeBranch(Field){
//-------------------------------------------------------------------------------------------
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false
	
    with(self.document.forms[0]){
		valProduct.value='';
		UpdateDiv('valProductDesc', '');
		tcnPolicy.value='';
		tcnCertif.value='';
	}
}

//-------------------------------------------------------------------------------------------
function insShowValues(sField){
//-------------------------------------------------------------------------------------------
	var lintTypOper = (self.document.forms[0].optTypOper[1].checked==true?self.document.forms[0].optTypOper[1].value:self.document.forms[0].optTypOper[0].value)
	var lstrSus_origi = "1"
	var nOption_aux
	
	with(self.document.forms[0]){
	
		switch(sField){
			case "Policy":
			    if (optTypOper[0].checked == 1)
			        nOption_aux = "1";
			    else
			        nOption_aux = "0";
			    insDefValues("CO633", "sDocument=" + sField + "&nTypOper=" + lintTypOper + "&sSus_origi=" + lstrSus_origi + "&sCertype=2" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nOption=" + nOption_aux)
				
				break;
				
			case "Certif":
				if (cbeBranch.value!="0" &&
				    valProduct.value!="" &&
				    tcnPolicy.value!="" &&
				    tcnCertif.value!=""){
				    insDefValues("CO633", "sDocument=" + sField + "&nTypOper=" + lintTypOper + "&sSus_origi=" + lstrSus_origi + "&sCertype=2" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&nCertif=" + tcnCertif.value)
				}
				break;
		}
	}			
}	
	</SCRIPT>
    <%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CO633", "CO633_k.aspx", 1, vbNullString))
	.Write("<BR><BR>")
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO633" ACTION="valCollectionTra.aspx?sMode=2">
<%
    Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
    Response.Write("</br>" & vbCrLf)
    mstrBranch = Request.QueryString.Item("nBranch")
    If mstrBranch = vbNullString Then mstrBranch = eRemoteDB.Constants.intNull
    
    mstrProduct = Request.QueryString.Item("nProduct")
    If mstrProduct = vbNullString Then mstrProduct = eRemoteDB.Constants.intNull

    mstrPolicy = Request.QueryString.Item("nPolicy")
    If mstrPolicy = vbNullString Then mstrPolicy = eRemoteDB.Constants.intNull

    mstrCertif = Request.QueryString.Item("nCertif")
    If mstrCertif = vbNullString Then mstrCertif = " "
    
    If Request.QueryString.Item("sConfig") = "InSequence" Then
        Call LoadHeader()
    Else
        Call LoadFolder()
    End If%>    
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.47
Call mobjNetFrameWork.FinishPage("co633_k")
    mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





