<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.19
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insRenewalMassive : Muestra los campos correspondientes a las renovación masiva
'-------------------------------------------------------------------------------------------------------
Private Sub insRenewalMassive()
	'-------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=100% BORDER=0>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""7"" CLASS=""HighLighted"" ALIGN=RIGHT><LABEL ID=101068><a NAME=""Renovación masiva"">" & GetLocalResourceObject("AnchorRenovación masivaCaption") & "</a></LABEL></td></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=101069><a NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodoCaption") & "</a></LABEL></td></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""6"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=20%><LABEL ID=101070>" & GetLocalResourceObject("tcdRendateFromCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=28%>")


Response.Write(mobjValues.DateControl("tcdRendateFrom", "",  , GetLocalResourceObject("tcdRendateFromToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><TD>" & vbCrLf)
Response.Write("            <TD WIDTH=20%><LABEL ID=101071>" & GetLocalResourceObject("tcdRenDatetoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD WIDTH=28%>")


Response.Write(mobjValues.DateControl("tcdRenDateto", "",  , GetLocalResourceObject("tcdRenDatetoToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=101069><a NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo2Caption") & "</a></LABEL></td></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""6"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("<TR>" & vbCrLf)
Response.Write("            <TD><DIV ID=""divPoliType2"">" & vbCrLf)
Response.Write("                ")

	
	With Response
		.Write(mobjValues.OptionControl(40673, "optType", GetLocalResourceObject("optType_1Caption"), CStr(1), "1"))
		.Write(mobjValues.OptionControl(40674, "optType", GetLocalResourceObject("optType_2Caption"),  , "2"))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("            </DIV></TD>" & vbCrLf)
Response.Write("        </TR>  " & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""6"" CLASS=""HighLighted""><LABEL ID=101069><a NAME=""Período"">" & GetLocalResourceObject("AnchorPeríodo3Caption") & "</a></LABEL></td></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""6"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13901>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	Response.Write(mobjValues.HiddenControl("tctCertype", "2"))
	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD><TD>			" & vbCrLf)
Response.Write("            <TD><LABEL ID=13909>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=13378>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	mobjValues.TypeOrder = 1
	Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", 1, Session("nOffice"),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip")))
	mobjValues.TypeOrder = 2
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD><TD>			" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeOfficeAgenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	With mobjValues
		.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, Session("nOfficeagen"), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
	End With
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("		    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	mobjValues.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, Session("nAgency"), True,  ,  ,  ,  , "ShowChangeValues(""Agency"")",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("            <TD><TD>			" & vbCrLf)
Response.Write("            <TD><LABEL ID=101075>" & GetLocalResourceObject("valIntermediaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("valIntermedia", "Intermedia", 2,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("valIntermediaToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR><TABLE WIDTH=100% BORDER=0>" & vbCrLf)
Response.Write("			<TR>" & vbCrLf)
Response.Write("				<TD>" & vbCrLf)
Response.Write("					")


Response.Write(mobjValues.CheckControl("chkGenCobAnt", GetLocalResourceObject("chkGenCobAntCaption"),  , "1", "", False,  , GetLocalResourceObject("chkGenCobAntCaption")))


Response.Write("" & vbCrLf)
Response.Write("				</TD>" & vbCrLf)
Response.Write("			</TR>" & vbCrLf)
Response.Write("			</TABLE>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("<SCRIPT>insInitialAgency(1);</" & "SCRIPT>")

End Sub

'% insRenewalPunctual : Muestra los campos correspondientes a las renovación puntual
'-------------------------------------------------------------------------------------------------------
Private Sub insRenewalPunctual()
	'-------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=100%>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""7"" CLASS=""HighLighted"" ALIGN=RIGHT><LABEL ID=101076><a NAME=""Renovación de una póliza"">" & GetLocalResourceObject("AnchorRenovación de una pólizaCaption") & "</a></LABEL></TD></TR>" & vbCrLf)
Response.Write("        <TR></TR><TR></TR><TR></TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=13901>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

	
	Response.Write(mobjValues.HiddenControl("tctCertype", "2"))
	Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=13909>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")

	
	Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), "", eFunctions.Values.eValuesType.clngWindowType))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101079>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, "",  , GetLocalResourceObject("tcnPolicyToolTip"),  , 0,  ,  ,  , "ChangeValuesNum('PolicyNum',this)",  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101080>" & GetLocalResourceObject("tcnCertifCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.NumericControl("tcnCertif", 10, "",  , GetLocalResourceObject("tcnCertifToolTip"),  , 0,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR></TR>" & vbCrLf)
Response.Write("        <TR></TR>" & vbCrLf)
Response.Write("        <TR></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""7"" CLASS=""HighLighted"" ALIGN=RIGHT><LABEL ID=101081><a NAME=""Datos de verificación"">" & GetLocalResourceObject("AnchorDatos de verificaciónCaption") & "</a></LABEL></TD></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""7"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=101082>" & GetLocalResourceObject("tctRenewalCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctRenewal", 15, "",  , GetLocalResourceObject("tctRenewalToolTip"),  ,  ,  ,  , True))


Response.Write(" </TD>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


'Response.Write(mobjValues.CheckControl("chkGenCobAnt", GetLocalResourceObject("chkGenCobAntCaption"), "1", "1", "", True,  , GetLocalResourceObject("chkGenCobAntCaption")))
	Response.Write(mobjValues.HiddenControl("chkGenCobAnt",""))

Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=100%>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""5"" CLASS=""HighLighted"" ALIGN=RIGHT><LABEL ID=101083><a NAME=""Vigencia"">" & GetLocalResourceObject("AnchorVigenciaCaption") & "</a></LABEL></TD></TR>" & vbCrLf)
Response.Write("        <TR><TD COLSPAN=""5"" CLASS=""HorLine""></TD></TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101070>" & GetLocalResourceObject("tcdRendateFromCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctStartDat", 10, "",  , GetLocalResourceObject("tctStartDatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101071>" & GetLocalResourceObject("tcdRenDatetoCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctExpirdat", 10, "",  , GetLocalResourceObject("tctExpirdatToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101086>" & GetLocalResourceObject("tctClientnameCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctClientname", 30, "",  , GetLocalResourceObject("tctClientnameToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=5></TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=101075>" & GetLocalResourceObject("valIntermediaCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.TextControl("tctIntername", 30, "",  , GetLocalResourceObject("tctInternameToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("            ")


Response.Write(mobjValues.HiddenControl("hddIntermed", ""))


Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("<SCRIPT>insInitialAgency(1)</" & "SCRIPT>")

	
End Sub

</script>
    <%
Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca031")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca031"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.19
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>


<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:53 $"
    
// insUpdateProduct : Habilita y establece los valores del producto según sea la selección del ramo
//---------------------------------------------------------------------------------------------------
function insUpdateProduct(){
//---------------------------------------------------------------------------------------------------
	UpdateDiv("valProductDesc","")
	with(self.document.forms[0]){
		valProduct.value=""
		if(cbeBranch.value=="0"){
			valProduct.disabled=true
			self.document.btnvalProduct.disabled=true
		}
		else{
			valProduct.disabled=false
			document.btnvalProduct.disabled=false
		}
	}
	self.document.forms[0].valProduct.Parameters.Param1.sValue=self.document.forms[0].cbeBranch.value;
}
//% ChangeValuesNum: Llama a la página ShowDefValues que ejecuta código necesario
//% para la actualización de campos del folder
//--------------------------------------------------------------------------------
function ChangeValuesNum(sField,sValue){
//--------------------------------------------------------------------------------
    if (sValue.value != '0' && sValue.value != ""){
		switch(sField){
			case "PolicyNum":
				insDefValues(sField,"nBranch=" + self.document.forms[0].cbeBranch.value +
									"&nProduct=" + self.document.forms[0].valProduct.value +
									"&nPolicy=" + self.document.forms[0].tcnPolicy.value)
		}
	}
	else{
	   self.document.forms[0].tctRenewal.value  = "";
	   self.document.forms[0].tctStartDat.value = "";
	   self.document.forms[0].tctExpirdat.value = "";
	   self.document.forms[0].tctClientname.value = "";
	   self.document.forms[0].tctIntername.value = "";
	   self.document.forms[0].chkGenCobAnt.checked = false;
	}
}

//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%	campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend()
//-------------------------------------------------------------------------------------
{
	with(document.forms[0]){
	    cbeOfficeAgen.value="";
	    cbeAgency.value="";
	}
	UpdateDiv('cbeOfficeAgenDesc','');
	UpdateDiv('cbeAgencyDesc','');
}
//% insInitialAgency: manejo de sucursal/oficina/agencia
//-------------------------------------------------------------------------------------------
function insInitialAgency(nInd) {
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
//+ Cambia la sucursal 
		if (nInd == 1){
		    if (typeof(cbeOffice)!='undefined'){
		        if (cbeOffice.value != 0){
	  				if (typeof(cbeOfficeAgen)!='undefined'){
	  					cbeOfficeAgen.disabled = false;
						btncbeOfficeAgen.disabled = false;
						cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
							cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
						else
							cbeAgency.Parameters.Param2.sValue = 0;
					}
			    }
				else{
	  				if(typeof(cbeOfficeAgen)!='undefined'){
						cbeOfficeAgen.disabled = false;
						btncbeOfficeAgen.disabled = false;
						cbeOfficeAgen.Parameters.Param1.sValue = cbeOffice.value;
						cbeOfficeAgen.Parameters.Param2.sValue = 0;
						cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
						if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0){
							cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;}
						else{
							cbeAgency.Parameters.Param2.sValue = 0;}
					}
				}
			}
		}
//+ Cambia la oficina
		else
		{
			if(cbeOfficeAgen.value!="" && cbeOfficeAgen.value>0)
			    {
                cbeAgency.Parameters.Param1.sValue = cbeOffice.value;
			    cbeAgency.Parameters.Param2.sValue = cbeOfficeAgen.value;
			    }
			else{
			    cbeAgency.Parameters.Param1.sValue = 0;
			    cbeAgency.Parameters.Param2.sValue = 0;
			    }
		}
	}
}
//% ShowChangeValues: Se habilitan/deshabilitan los controles de acuerdo a lo definido para
//%	producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		switch(sField){
			case "Agency":
				if(cbeAgency.value!="")
				    insDefValues("cbeAgency", "nAgency=" + cbeAgency.value + "&nOfficeAgen=" + cbeOfficeAgen.value +"&nOffice=" + cbeOffice.value,'/VTimeNet/Policy/PolicyTra')
				break;
		}
	}
}
</SCRIPT>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
        <%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("sCodispl", Request.QueryString.Item("sWindowDescript")))
End With
%>
    <BODY ONUNLOAD="closeWindows();">
    <FORM METHOD="post" ID="FORM" NAME="frmRenewalProcess" ACTION="ValPolicyTra.aspx?x=1">
    <%Response.Write(mobjValues.ShowWindowsName("CA031_K", Request.QueryString.Item("sWindowDescript")))
Response.Write(mobjMenu.setZone(2, "CA031_K", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing

If CStr(Session("nInfo")) = "1" Then
	Call insRenewalMassive()
Else
	Call insRenewalPunctual()
End If
mobjValues = Nothing
%>
    </FORM>
    </BODY>
    </HEAD>
<BODY>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.19
Call mobjNetFrameWork.FinishPage("ca031")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





