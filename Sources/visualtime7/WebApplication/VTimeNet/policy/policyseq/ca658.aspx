<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'- Objeto manejo de los mensajes de error	
Dim lclsGeneral As eGeneral.GeneralFunction

'- Variable para almacenar el URL donde se maneja el Grid    
Dim mstrLocation As String

'- Variable que almacena el mensaje de error
Dim lstrMessage As String


'% insPreCA658: se realiza el manejo del grid
'--------------------------------------------------------------------------------------------
Private Sub insPreCA658()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient_tmp As ePolicy.Client_tmp
	Dim lclsErrors As eFunctions.Errors
	
	lclsClient_tmp = New ePolicy.Client_tmp
	lclsErrors = New eFunctions.Errors
	'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
	lclsErrors.sSessionID = Session.SessionID
	lclsErrors.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	With lclsClient_tmp
		.sCertype = Session("sCertype")
		.nBranch = mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble)
		.nProduct = mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)
		.nPolicy = mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble)
		Call .insPreCA658(Request.QueryString.Item("Type"), Session("sPolitype"), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble))
	End With
	
	If lclsClient_tmp.bErrors Then
		mobjValues.ActionQuery = True
	End If
	mstrLocation = lclsClient_tmp.DefaultValueCA658(vbNullString, "URLFrame")
	
Response.Write("	" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2""  CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("AnchorCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD width=10%>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HighLighted""><LABEL>" & GetLocalResourceObject("Anchor2Caption") & "</LABEL></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("			<TD></TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"), lclsClient_tmp.DefaultValueCA658(vbNullString, "optAge_Temp"), "1", "ChangeValues(""optType"")",  ,  , GetLocalResourceObject("optType_1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optAge", GetLocalResourceObject("optAge_1Caption"), "1", "1", "ChangeValues(""optAge"", this)", lclsClient_tmp.DefaultValueCA658(vbNullString, "optAge_1"),  , GetLocalResourceObject("optAge_1ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), lclsClient_tmp.DefaultValueCA658(vbNullString, "optAge_Def"), "2", "ChangeValues(""optType"")",  ,  , GetLocalResourceObject("optType_2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optAge", GetLocalResourceObject("optAge_2Caption"),  , "2", "ChangeValues(""optAge"", this)", lclsClient_tmp.DefaultValueCA658(vbNullString, "optAge_2"),  , GetLocalResourceObject("optAge_2ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"">&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD COLSPAN=""2"">")


Response.Write(mobjValues.OptionControl(0, "optAge", GetLocalResourceObject("optAge_3Caption"),  , "3", "ChangeValues(""optAge"", this)", lclsClient_tmp.DefaultValueCA658(vbNullString, "optAge_3"),  , GetLocalResourceObject("optAge_3ToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""5"">&nbsp;</TD>" & vbCrLf)
Response.Write("		</TR>		" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnInsuredCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnInsured", 8, CStr(0),  , GetLocalResourceObject("tcnInsuredToolTip"), True,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.CheckControl("chkMassive", GetLocalResourceObject("chkMassiveCaption"),  ,  , "ChangeValues(""MassiveCharge"",this)", lclsClient_tmp.DefaultValueCA658(vbNullString, "chkMassive"),  , GetLocalResourceObject("chkMassiveToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("	<IFRAME NAME=""fraGrid"" SRC=")


Response.Write("""" & mstrLocation & """")


Response.Write(" WIDTH=""100%"" HEIGHT=""52%"" SCROLLING=AUTO FRAMEBORDER=""0"">" & vbCrLf)
Response.Write("	</IFRAME>")

	
	If lclsClient_tmp.bErrors Then
		Response.Write(lclsErrors.ErrorMessage("CA658", 1402,  ,  ,  , True))
	End If
	
	lclsClient_tmp = Nothing
	lclsErrors = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA658")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")

lclsGeneral = New eGeneral.GeneralFunction
lstrMessage = lclsGeneral.insLoadMessage(60561)
lclsGeneral = Nothing

%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "CA658", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//+ Variable para el control de versiones
       document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $"
//% ChangeValues: se controla el cambio de valor de los campos de la página
//-------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//-------------------------------------------------------------------------------------------
	var optType
	var linaction
	switch(Option){
		case "optType":
			with(self.document.forms[0]){
				chkMassive.checked=false;
				optAge[0].checked=(!optType[0].checked)?true:optAge[0].checked;
				optAge[0].disabled=(optType[0].checked)?false:true;
				optAge[1].disabled=(optType[0].checked)?false:true;
				optAge[2].disabled=(optType[0].checked)?false:true;

				tcnInsured.value=(!optType[0].checked)?"":tcnInsured.value;
				self.document.frames['fraGrid'].location=(optType[0].checked)?'CA658Frame.aspx?sCodispl=CA658&sCodisp=CA658&nMainAction=304&sOnSeq=1&nOptAge=1':'/VTimeNet/Common/Blank.htm';
			}
			break;
		
		case "optAge":
		    if (self.document.forms[0].hddMessCtrl.value != 0) 
		        if (self.document.forms[0].hddMessCtrl.value != Field.value) {
		            if(confirm('Err 60561:   <%=lstrMessage%>  '))
                        deleteValues();
					else
						self.document.frames['fraGrid'].location='CA658Frame.aspx?sCodispl=CA658&sCodisp=CA658&nMainAction=304&sOnSeq=1&nOptAge=1';
				}				
			self.document.frames['fraGrid'].document.forms[0].OptAge.value = Field.value;	
			break;
/*+ Al invocarse a la carga masiva la acción debe tener valor 5 */
		case "MassiveCharge":
			with(self.document.forms[0]){
				if(optAge[0].checked)
					option='1';
				if(optAge[1].checked)
					option='2';
				if(optAge[2].checked)
					option='3';
			}
			if(Field.checked){
				with(self.document.forms[0]){
					if(optType[0].checked)
						linaction='5';
					else
						linaction='1';
				}	
				ShowPopUp('../../common/GoTo.aspx?sCodispl=CAL013_K&sLinkSpecial=CA658&nAction=' + linaction + '&sTypeage=' + option + '&sCertype=' + <%=Session("sCertype")%> + '&nBranch='+ <%=Session("nBranch")%> + '&nProduct=' + <%=Session("nProduct")%> + '&nPolicy=' + <%=Session("nPolicy")%> + '&nTransaction=' + <%=Session("nTransaction")%>,'MassiveCharge',750,500,'no', 'yes',10,10)
			}
			break;
	}
}

//% ShowChangeValues: Se cargan los valores de acuerdo al auto que se seleccione 
//-------------------------------------------------------------------------------------------
function deleteValues(){
//-------------------------------------------------------------------------------------------
	var strParams; 
	
	insDefValues("deleteValues","",'/VTimeNet/Policy/PolicySeq'); 
	self.document.frames['fraGrid'].location='CA658Frame.aspx?sCodispl=CA658&sCodisp=CA658&nMainAction=304&sOnSeq=1&nOptAge=2';
}   
</SCRIPT> 
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CA658" ACTION="valPolicySeq.aspx?sMode=2">
<%
Response.Write(mobjValues.HiddenControl("hddMessCtrl", CStr(0)))
Response.Write(mobjValues.ShowWindowsName("CA658", Request.QueryString.Item("sWindowDescript")))
Call insPreCA658()
mobjValues = Nothing
%>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA658")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




