<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues



'%insPreSG001: Se definen los objetos a ser utilizados y permite realizar el llamado al
'%método de lectura para mostrar la información en la parte de detalle de la página.
'-----------------------------------------------------------------------------------------
Private Sub insPreSG001()
	'-----------------------------------------------------------------------------------------
	Dim lclsUser As eSecurity.User
    Dim bUpdating As Boolean = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate)
	
	lclsUser = New eSecurity.User
	lclsUser.Find(mobjValues.StringToType(Request.QueryString.Item("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	Response.Write(mobjValues.HiddenControl("hddnUsercode", Request.QueryString.Item("nUsercode")))
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=15069>" & GetLocalResourceObject("valScheCodeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PossiblesValues("valScheCode", "tab_schema", eFunctions.Values.eValuesType.clngWindowType, lclsUser.sSche_code,  ,  ,  ,  ,  ,  ,  , 6, GetLocalResourceObject("valScheCodeToolTip"), eFunctions.Values.eTypeCode.eString, 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=15%><LABEL ID=15072>" & GetLocalResourceObject("cbeUserTypCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD> ")


Response.Write(mobjValues.PossiblesValues("cbeUserTyp", "Table105", eFunctions.Values.eValuesType.clngComboType, lclsUser.sType,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeUserTypToolTip"),  , 2))


Response.Write("</TD>" & vbCrLf)
Response.Write("        <TD WIDTH=20%><LABEL ID=15067>" & GetLocalResourceObject("cbeOfficeCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD> ")

	mobjValues.TypeOrder = 1
	Response.Write(mobjValues.PossiblesValues("cbeOffice", "Table9", eFunctions.Values.eValuesType.clngComboType, CStr(lclsUser.nOffice),  ,  ,  ,  ,  , "BlankOfficeDepend();insInitialAgency(1)",  ,  , GetLocalResourceObject("cbeOfficeToolTip"),  , 3))
	mobjValues.TypeOrder = 2
	
Response.Write(" " & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("    </TR>       " & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeOfficeAgenCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	
	With mobjValues
		.Parameters.Add("nOfficeAgen", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		.Parameters.Add("nAgency", lclsUser.nAgency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
		If lclsUser.nOfficeagen > 0 Then
			Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, CStr(lclsUser.nOfficeagen), True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
		Else
			Response.Write(mobjValues.PossiblesValues("cbeOfficeAgen", "TabAgencies_T5556", 2, "", True,  ,  ,  ,  , "insInitialAgency(2)",  ,  , GetLocalResourceObject("cbeOfficeAgenToolTip")))
		End If
	End With
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	    <TD><LABEL ID=0>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		<TD>")

	
	mobjValues.Parameters.Add("nOfficeAgen", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	mobjValues.Parameters.Add("nAgency", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	If lclsUser.nAgency > 0 Then
		Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, CStr(lclsUser.nAgency), True,  ,  ,  ,  , "ShowChangeValues('Agency')",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
	Else
		Response.Write(mobjValues.PossiblesValues("cbeAgency", "TabAgencies_T5555", 2, "", True,  ,  ,  ,  , "ShowChangeValues('Agency')",  ,  , GetLocalResourceObject("cbeAgencyToolTip")))
	End If
	
Response.Write("" & vbCrLf)
Response.Write("		</TD>" & vbCrLf)
Response.Write("	</TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=15%><LABEL ID=15063>" & GetLocalResourceObject("sClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD colspan=""3"">")


Response.Write(mobjValues.ClientControl("sClient", lclsUser.sClient,  , GetLocalResourceObject("sClientToolTip"),  ,  , "lblCliename",  ,  ,  ,  ,  , 4, True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>                            " & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <BR>" & vbCrLf)
Response.Write("        </TR>        " & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD COLSPAN=4 CLASS=""HighLighted""><LABEL ID=100426><A NAME=""Clave"">" & GetLocalResourceObject("AnchorClaveCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
Response.Write("            <TD COLSPAN=2 CLASS=""HighLighted"" ALIGN=LEFT><LABEL ID=100427><A NAME=""Límite"">" & GetLocalResourceObject("AnchorLímiteCaption") & "</A></LABEL></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			 <TD COLSPAN=""4"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("			 <TD></TD>" & vbCrLf)
Response.Write("			 <TD COLSPAN=""2"" CLASS=""HorLine""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=15%><LABEL ID=15066>" & GetLocalResourceObject("tctInitialsCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD> ")


Response.Write(mobjValues.TextControl("tctInitials", 12, lclsUser.sInitials,  , GetLocalResourceObject("tctInitialsToolTip"),  ,  ,  ,  ,  , 5))


        Response.Write("</TD>" & vbCrLf)
        Response.Write(" <TD WIDTH=15%>" & vbCrLf)

        If bUpdating Then
            Response.Write(mobjValues.CheckControl("chkSetPassword", GetLocalResourceObject("chkSetPasswordCaption"), , , "insHandlePasswordField(this.checked);"))
            Response.Write(" ")
        Else
            Response.Write("<LABEL ID=15068>" & GetLocalResourceObject("tctPasswordCaption") & "</LABEL>" & vbCrLf)
        End If
        Response.Write("    </TD>" & vbCrLf)
        Response.Write("  <TD> ")

        Response.Write(mobjValues.PasswordControl("tctPassword", 12, String.Empty, , GetLocalResourceObject("tctPasswordToolTip"), , , , , bUpdating, 6))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("            <TD> &nbsp; </TD>" & vbCrLf)
        Response.Write("            <TD WIDTH=15%><LABEL ID=15065>" & GetLocalResourceObject("tcdDateFromCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD> ")


        Response.Write(mobjValues.DateControl("tcdDateFrom", CStr(lclsUser.dFromDate), , GetLocalResourceObject("tcdDateFromToolTip"), , , , , , 7))


        Response.Write("</TD>" & vbCrLf)
        Response.Write("        </TR>" & vbCrLf)
        Response.Write("    </TABLE>            " & vbCrLf)
        Response.Write("        " & vbCrLf)
        Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <BR>" & vbCrLf)
        Response.Write("        </TR>        " & vbCrLf)
        Response.Write("        <TR>" & vbCrLf)
        Response.Write("            <TD><LABEL ID=15070>" & GetLocalResourceObject("cbeStatregtCaption") & "</LABEL></TD>" & vbCrLf)
        Response.Write("            <TD>")

        With mobjValues
            .BlankPosition = False
            If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionUpdate) Then
                mobjValues.TypeList = 2
                mobjValues.List = "2"
            ElseIf Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd) Then
                lclsUser.sStatregt = "2"
            End If
            Response.Write(.PossiblesValues("cbeStatregt", "Table26", eFunctions.Values.eValuesType.clngComboType, lclsUser.sStatregt, , , , , , , Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionadd), , GetLocalResourceObject("cbeStatregtToolTip"), , 8))
        End With
	
Response.Write("</TD>      " & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=15064>" & GetLocalResourceObject("cbeDeparmentCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeDeparment", "Table84", eFunctions.Values.eValuesType.clngComboType, lclsUser.nDepartme,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeDeparmentToolTip"),  , 10))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.CheckControl("chkNeverExpires", GetLocalResourceObject("chkNeverExpiresCaption"), lclsUser.sNeverChange, CStr(2), "AssignValue(this.checked)",  , 11))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <!--TD WIDTH=15%><LABEL ID=15062>" & GetLocalResourceObject("valMenuCaption") & "</LABEL></TD-->" & vbCrLf)
Response.Write("            <!--TD>")


Response.Write(mobjValues.PossiblesValues("valMenu", "TabMenuUsr", eFunctions.Values.eValuesType.clngWindowType, lclsUser.sMenu,  ,  ,  ,  ,  ,  ,  , 9, GetLocalResourceObject("valMenuToolTip"), eFunctions.Values.eTypeCode.eString, 12))


Response.Write("</TD-->" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD ALIGN=RIGTH>")


Response.Write(mobjValues.CheckControl("chkLockedOut", GetLocalResourceObject("chkLockedOutCaption"), lclsUser.sLockedOut, CStr(1),  ,  , 14))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.HiddenControl("hddNeverChange", CStr(0)))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>                " & vbCrLf)
Response.Write("        <SCRIPT>insInitialAgency(1)</" & "SCRIPT>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("</TABLE>")

End Sub

Protected Sub Page_Load(sender As Object, e As System.EventArgs)

End Sub
</script>
<%
Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG001"
%>
<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17.43 $"

//%insCancel: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}
//%insHandlePasswordField: Permite habilitar/inhabilitar el campo password
//------------------------------------------------------------------------------------------
function insHandlePasswordField(openField) {
    //------------------------------------------------------------------------------------------
    var pwdField = document.getElementsByName("tctPassword")[0];

    pwdField.disabled = !openField;
    pwdField.value = "";
}
//% BlankOfficeDepend: Blanquea los campos OFICINA y AGENCIA si y sólo si el valor del
//%	campo SUCURSAL cambia
//-------------------------------------------------------------------------------------
function BlankOfficeDepend(){
//-------------------------------------------------------------------------------------
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
//%					  producto, póliza o certificado
//-------------------------------------------------------------------------------------------
function ShowChangeValues(sField){
//-------------------------------------------------------------------------------------------
	
	switch(sField)
	{
		case "Agency":
		{
			if(self.document.forms[0].elements["cbeAgency"].value!="")
			    insDefValues(sField, "nAgency=" + self.document.forms[0].elements["cbeAgency"].value +
			                         "&nOfficeAgen=" + self.document.forms[0].elements["cbeOfficeAgen"].value +
			                         "&nOffice=" + self.document.forms[0].elements["cbeOffice"].value,'/VTimeNet/Security/Security');
			break;
		}
	}
	
}

//-----------------------------------------------------------------------------------------
function AssignValue(blnValue)
//-----------------------------------------------------------------------------------------
{
	if(blnValue)
		self.document.forms[0].elements["hddNeverChange"].value=1
	else
		self.document.forms[0].elements["hddNeverChange"].value=2;
	
}

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<%
'+ Se realiza el llamado a las rutinas generales para cargar la página invocada.
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		mobjMenu = New eFunctions.Menues
		.Write("<SCRIPT>var nMainAction = 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
		.Write(mobjMenu.setZone(2, "SG001", "SG001.aspx"))
		mobjMenu = Nothing
	End If
	If Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery) Or CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 303 Then
		mobjValues.ActionQuery = True
	Else
		mobjValues.ActionQuery = False
	End If
End With
%>
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="SG001" ACTION="valSecurity.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
<%
Response.Write(mobjValues.ShowWindowsName("SG001"))
Call insPreSG001()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





