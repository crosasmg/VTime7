<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca051_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca051_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"  


//% ChangeValues: Habilita y deshabilita los campos
//------------------------------------------------------------------------------------------
function ChangeValues(Option, Field){
//------------------------------------------------------------------------------------------
	switch(Option){
		case "File":
			with (self.document.forms[0]){
				if (chkFile.checked)
				{	
					tctFile.disabled=false;
					chkFile.value="2"
					chkList.value="1"
					chkList.checked=true
					chkList.disabled=false
				}
				else{
					tctFile.value="";
					tctFile.disabled=true;
					chkFile.value="1"
					chkList.value="2"
					chkList.checked=false
					chkList.disabled=true
				}
			}
			break;
			
		case "List":
			with (self.document.forms[0]){
				if (chkList.checked)
					chkList.value="1"
				else
					chkList.value="2"
			}
			break;

		case "nId":
			ShowPopUp("/VTimeNet/Policy/PolicyTra/ShowDefValues.aspx?Field=" + Option  + "&nId=" + Field.value, "ShowDefValuesRep", 1, 1,"no","no",2000,2000);
			break; 		
		case "nProduct":
			with (self.document.forms[0]){
				tcnPolicy.value='';
				if (valProduct.value == '')
				    tcnPolicy.disabled =true;
				else
					tcnPolicy.disabled =false;
			}
			break; 		
		case "nBranch":	
			with (self.document.forms[0]){
			    tcnPolicy.value='';
			    tcnPolicy.disabled =true;
			}
			break; 		

}
}

//% insFinish: se controla la acción Finalizar de la página
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
	return(true);
}
//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		tcnWorksheet.disabled=false;
        btntcnWorksheet.disabled=false;		
		cbeBranch.disabled=false;
		tctFile.disabled=false;
		tctDescript.disabled=false;
		chkFile.disabled=false;
		chkList.disabled=false;
	}
}
//% insPreZone: Modifica el comportamiento de la página dependiendo de la acción
//% que proviene del menú principal
//------------------------------------------------------------------------------------------
function insPreZone(llngAction)
//------------------------------------------------------------------------------------------
{
}
//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel()		
//------------------------------------------------------------------------------------------
{
	return true
}
</SCRIPT>
		<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
		<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("CA051", "CA051_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
	</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<TD><BR></TD>
		<TD><BR></TD>
		<FORM METHOD="post" ID="FORM" NAME="CA051" ACTION="valPolicyTra.aspx?x=1">
			<TABLE WIDTH="100%">
				<TR>
					<TD WIDTH="5%"><LABEL ID=101267><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
					<TD WIDTH="15%">
						<%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "ChangeValues(""nBranch"", this)", True)%> </TD>
					</TD>
					<TD WIDTH="5%"><LABEL ID=101268><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
					<TD WIDTH="15%">
						<%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  , True,  ,  ,  ,  , "ChangeValues(""nProduct"", this)")%></TD>
					</TD>
				</TR>
				<TR>
					<TD><LABEL ID=101269><%= GetLocalResourceObject("tcnPolicyCaption") %></LABEL></TD>
					<TD>
						<%=mobjValues.NumericControl("tcnPolicy", 10, vbNullString, True, GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  ,  , True)%>
					</TD>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tctFileCaption") %></LABEL></TD>
					<TD>
						<%=mobjValues.TextControl("tctFile", 15, vbNullString,  , GetLocalResourceObject("tctFileToolTip"),  ,  ,  ,  , True)%>
					</TD>
					
				</TR>
				<TR>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tcnWorksheetCaption") %></LABEL></TD>
					<TD>
						<%=mobjValues.PossiblesValues("tcnWorksheet", "TabtabWorksheet", eFunctions.Values.eValuesType.clngWindowType, "",  ,  ,  ,  ,  , "ChangeValues(""nId"", this)", True, 5, GetLocalResourceObject("tcnWorksheetToolTip"),  ,  ,  , True)%></TD>
					</TD>
					<TD><LABEL ID=0><%= GetLocalResourceObject("tctDescriptCaption") %></LABEL></TD>
					<TD>
						<%=mobjValues.TextControl("tctDescript", 30, vbNullString,  , GetLocalResourceObject("tctDescriptToolTip"),  ,  ,  ,  , True)%>
					</TD>

				</TR>
				<TR>
					<TD>
						<%=mobjValues.CheckControl("chkFile", GetLocalResourceObject("chkFileCaption"), , , "ChangeValues(""File"", this)", True,  , GetLocalResourceObject("chkFileToolTip"))%>
					</TD>
					<TD>
						<%=mobjValues.CheckControl("chkList", GetLocalResourceObject("chkListCaption"), , , "ChangeValues(""List"", this)", True,  , GetLocalResourceObject("chkListToolTip"))%>
					</TD>
				</TR>

			</TABLE>
		</FORM>
	</BODY>
</HTML>

<% mobjValues = Nothing%> 

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca051_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




