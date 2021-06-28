<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues

Dim mstrOptInfo As String

    
Dim mintYear As Object
Dim mintMonth As Object
Dim mintContrat_Pay As Object
Dim mintBranch As Object
Dim mintProduct As Object
Dim mlngPolicy As Object


'% insPreFolder: Se controla la carga de los datos de la página
'--------------------------------------------------------------------------------------------
Private Sub insPreFolder()
	'--------------------------------------------------------------------------------------------
	mstrOptInfo = "2"
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("AGL955_k")
Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")

mobjValues.sCodisplPage = "AGL955_k"
mobjMenu = New eFunctions.Menues
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")

%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



    
<SCRIPT>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 4 $|$$Date: 15/10/03 16:40 $|$$Author: Nvaplat61 $"

    //% ShowChangeValues: Se cargan los valores de acuerdo producto seleccionado
    //-------------------------------------------------------------------------------------------
    function ShowChangeValues(sField) {
        //-------------------------------------------------------------------------------------------    
        if (self.document.forms[0].nPolicy.value != ''){
            ShowPopUp("/VTimeNet/Agent/Agentrep/ShowDefValues.aspx?Field=" + sField + "&sCertype=2" + "&nPolicy=" + self.document.forms[0].nPolicy.value + "&nBranch=" + self.document.forms[0].cbeBranch.value + "&nProduct=" + self.document.forms[0].valProduct.value, "ShowDefValuesCotProp", 1, 1, "no", "no", 2000, 2000);
        }
    }

    //% InsChangeOptInfo: se controla el cambio para la opción de ejecución
    //--------------------------------------------------------------------------------------------
    function InsChangeOptInfo(Field) {
        //--------------------------------------------------------------------------------------------	
        with (self.document.forms[0]) {
            //+ Si es contrato
            document.forms[0].nYear.focus();
            if (Field.value == "2") {
                ShowDiv('DIVFIX', 'show');
                ShowDiv('DIVPOLICY', 'hide');
                ShowDiv('DIVCONTRAT', 'show');

                nContrat_Pay.disabled = false;
                cbeBranch.disabled = true;
                valProduct.disabled = true;
                btnvalProduct.disabled = true;
                nPolicy.disabled = true;

                cbeBranch.value = '';
                valProduct.value = '';
                btnvalProduct.value = '';
                nPolicy.value = '';
            }
            else {
                //+ Si es poliza
                ShowDiv('DIVFIX', 'show');
                ShowDiv('DIVCONTRAT', 'hide');
                ShowDiv('DIVPOLICY', 'show');

                nContrat_Pay.disabled = true;
                cbeBranch.disabled = false;
                valProduct.disabled = false;
                btnvalProduct.disabled = true;
                nPolicy.disabled = false;

                nContrat_Pay.value = '';

            }
        }
    };

    //% insCancel: se controla la acción Cancelar de la página
    //------------------------------------------------------------------------------------------
    function insCancel() {
        //------------------------------------------------------------------------------------------
        return true;
    } 
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu("AGL955", "AGL955_k.aspx", 1, vbNullString))
	.Write(mobjMenu.setZone(1, "AGL955", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With

Call insPreFolder()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmAGL955" ACTION="ValAgentRep.aspx?smode=1">
	<BR><BR>
    	<%=mobjValues.ShowWindowsName("AGL955", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption2")%></LABEL></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("AnchorCaption1") %></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
            <TD></TD>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><%= mobjValues.OptionControl(0, "optproccess", GetLocalResourceObject("optProccess_1Caption"), "1", "1", , , , GetLocalResourceObject("optProccess_1ToolTip"))%></TD>
            <TD><%= mobjValues.OptionControl(0, "optproccess", GetLocalResourceObject("optProccess_2Caption"), , "2", , , , GetLocalResourceObject("optProccess_2ToolTip"))%></TD>
            <TD>&nbsp;</TD>
			<TD><%
If mstrOptInfo = "1" Then
			            Response.Write(mobjValues.OptionControl(0, "sOptInfo", GetLocalResourceObject("sOptInfo_1Caption"), "2", "1", "InsChangeOptInfo(this);", , 1, GetLocalResourceObject("sOptInfo_1ToolTip")))
Else
			            Response.Write(mobjValues.OptionControl(0, "sOptInfo", GetLocalResourceObject("sOptInfo_1Caption"), "1", "1", "InsChangeOptInfo(this);", , 1, GetLocalResourceObject("sOptInfo_1ToolTip")))
End If
%>

			</TD>
			
            <TD><%
If mstrOptInfo = "2" Then
                        Response.Write(mobjValues.OptionControl(0, "sOptInfo", GetLocalResourceObject("sOptInfo_2Caption"), "1", "2", "InsChangeOptInfo(this);", , 2, GetLocalResourceObject("sOptInfo_2ToolTip")))
Else
                        Response.Write(mobjValues.OptionControl(0, "sOptInfo", GetLocalResourceObject("sOptInfo_2Caption"), "2", "2", "InsChangeOptInfo(this);", , 2, GetLocalResourceObject("sOptInfo_2ToolTip")))
End If
%>
			</TD>
            
            
        </TR>    
	</TABLE>
	<DIV ID="DIVFIX">
		<TABLE WIDTH="100%">
			<TR>
			    <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
			</TR>
			<TR>
			    <TD COLSPAN="5" CLASS="HorLine"></TD>
			</TR>
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("nYearCaption") %></LABEL></TD>
				<TD><%= mobjValues.NumericControl("nYear", 4, "", , "", , 0, , , , , False)%></TD>
				<TD>&nbsp;</TD>
				<TD><LABEL ID=0><%= GetLocalResourceObject("nMonthCaption") %></LABEL></TD>
				<TD><%= mobjValues.NumericControl("nMonth", 4, "", , "", , 0, , , , , False)%></TD>
			</TR>
			<TR>
			    <TD COLSPAN="5">&nbsp;</TD>
			</TR>        
		</TABLE>
    </DIV>
	<DIV ID="DIVCONTRAT">
		<TABLE WIDTH="100%">
			<TR>
			    <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
			</TR>
			<TR>
			    <TD COLSPAN="5" CLASS="HorLine"></TD>
			</TR>
			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("nContrat_PayCaption") %></LABEL></TD>

                <TD><%= mobjValues.PossiblesValues("nContrat_Pay", "tabcontrat_pay", eFunctions.Values.eValuesType.clngWindowType, , False, , , , , , , , GetLocalResourceObject("nContrat_PayToolTip"))%></TD>

  			    <TD>&nbsp;</TD>
			</TR>
			<TR>
			    <TD COLSPAN="5">&nbsp;</TD>
			</TR>        
		</TABLE>
    </DIV>
	<DIV ID="DIVPOLICY">
		<TABLE WIDTH="100%">
			<TR>
	            <TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
	        </TR>
	        <TR>
	            <TD COLSPAN="5" CLASS="HorLine"></TD>
	        </TR>        

			<TR>
				<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
				<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  , "valProduct")%></TD>
				<TD>&nbsp;</TD>
			    <TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
				<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"))%></TD>
	        </TR>
	        <TR>
	            <TD><LABEL ID=0><%= GetLocalResourceObject("nPolicyCaption") %></LABEL></TD>
	            <TD><%= mobjValues.NumericControl("nPolicy", 10, , , GetLocalResourceObject("nPolicyToolTip"), , , , , , "ShowChangeValues('Policy')")%></TD>
	            <TD>&nbsp;</TD>
	        </TR>
		</TABLE>
	</DIV>
	<TABLE WIDTH="100%">
		<TR>
			<TD COLSPAN="5" CLASS="HighLighted"><LABEL><%= GetLocalResourceObject("Anchor5Caption") %></LABEL></TD>
	    </TR>
	    <TR>
			<TD COLSPAN="5" CLASS="HorLine"></TD>
	    </TR>        
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("lblEffecdateCaption") %></LABEL></TD>
			<TD><%= mobjValues.DateControl("lblEffecdate", "", , GetLocalResourceObject("lblEffecdateToolTip"), False)%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0>&nbsp;</LABEL></TD>
			<TD>&nbsp;</TD>
	    </TR>
	</TABLE>
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Response.Write("ShowDiv('DIVFIX', 'show');")
If mstrOptInfo = "1" Then
	Response.Write("ShowDiv('DIVPOLICY', 'show');")
	Response.Write("ShowDiv('DIVCONTRAT', 'hide');")
Else
	Response.Write("ShowDiv('DIVPOLICY', 'hide');")
	Response.Write("ShowDiv('DIVCONTRAT', 'show');")
    End If
    'Response.Write("InsChangeOptInfo(1);")
Response.Write("</SCRIPT>")

mobjValues = Nothing
mobjMenu = Nothing
%>
<%
Call mobjNetFrameWork.FinishPage("AGL955_k")
mobjNetFrameWork = Nothing
%>




