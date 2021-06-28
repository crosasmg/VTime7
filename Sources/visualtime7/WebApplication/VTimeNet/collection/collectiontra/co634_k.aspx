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
Dim mcolClass As Object

'- Variables de trabajo para almacenar los códigos de los documentos a tratar.
Dim mlngReceiptOri As Integer
Dim mlngReceiptDes As Integer
Dim mlngProponumOri As Integer
Dim mlngProponumDes As Integer
Dim mlngBranchOri As Integer
Dim mlngBranchDes As Integer
Dim mlngProductOri As Integer
Dim mlngProductDes As Integer


'% insReaInitial: Se encarga de inicializar las variables de trabajo
'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mlngReceiptOri = eRemoteDB.Constants.intNull
	mlngReceiptDes = eRemoteDB.Constants.intNull
	mlngProponumOri = eRemoteDB.Constants.intNull
	mlngProponumDes = eRemoteDB.Constants.intNull
	mlngBranchOri = eRemoteDB.Constants.intNull
	mlngBranchDes = eRemoteDB.Constants.intNull
	mlngProductOri = eRemoteDB.Constants.intNull
	mlngProductDes = eRemoteDB.Constants.intNull
End Sub

'% insOldValues: Se encarga de asignar los valores obtenidos en vbscript a javascript.
'-----------------------------------------------------------------------------------------
Private Sub insOldValues()
	'-----------------------------------------------------------------------------------------
	If mlngReceiptOri <> eRemoteDB.Constants.intNull And mlngReceiptDes <> eRemoteDB.Constants.intNull And mlngProponumOri <> eRemoteDB.Constants.intNull And mlngProponumDes <> eRemoteDB.Constants.intNull And mlngBranchOri <> eRemoteDB.Constants.intNull And mlngBranchDes <> eRemoteDB.Constants.intNull And mlngProductOri <> eRemoteDB.Constants.intNull And mlngProductDes <> eRemoteDB.Constants.intNull Then
		
		With Response
			
			.Write("<SCRIPT>")
			
			.Write("var mlngReceiptOri = " & CStr(mlngReceiptOri) & ";")
			
			.Write("var mlngReceiptDes = " & CStr(mlngReceiptDes) & ";")
			
			.Write("var mlngProponumOri = " & CStr(mlngProponumOri) & ";")
			
			.Write("var mlngProponumDes = " & CStr(mlngProponumDes) & ";")
			
			.Write("var mlngBranchOri = " & CStr(mlngBranchOri) & ";")
			
			.Write("var mlngBranchDes = " & CStr(mlngBranchDes) & ";")
			
			.Write("var mlngProductOri = " & CStr(mlngProductOri) & ";")
			
			.Write("var mlngProductDes = " & CStr(mlngProductDes) & ";")
			
			.Write("</" & "Script>")
			
		End With
		
	Else
		With Response
			
			.Write("<SCRIPT>")
			
			.Write("var mlngReceiptOri = 0;")
			
			.Write("var mlngReceiptDes = 0;")
			
			.Write("var mlngProponumOri = 0;")
			
			.Write("var mlngProponumDes = 0;")
			
			.Write("var mlngBranchOri = 0;")
			
			.Write("var mlngBranchDes = 0;")
			
			.Write("var mlngProductOri = 0;")
			
			.Write("var mlngProductDes = 0;")
			
			.Write("</" & "Script>")
			
		End With
	End If
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("co634_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.47
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "co634_k"
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
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
	<SCRIPT>
//+ Variable para el control de versiones
	     document.VssVersion="$$Revision: 3 $|$$Date: 10/08/04 17:26 $|$$Author: Nvaplat40 $"
    </SCRIPT>
    
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
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

//% insShowDocument: Muestra la información seleccionada dependiendo del documento (Recibo, Propuesta) y tipo de documento (Origen, destino)
//-------------------------------------------------------------------------------------------
function insShowDocument(Document, TypDocument){
//-------------------------------------------------------------------------------------------
	if (Document!='') {
	    with(self.document.forms[0]){
			switch (Document){
				case 'Proponum': 
					if (TypDocument=='Ori'){
						if (cbeBranchOri.value>0 && valProductOri.value!=0 && valProductOri.value!='' && tcnProponumOri.value>0 && tcnProponumOri.value!='') 
							if (cbeBranchOri.value!=mlngBranchOri || valProductOri.value!=mlngProductOri || tcnProponumOri.value!=mlngProponumOri){
								insDefValues("ShowDataCO634", "sDocument=" + Document + "&sTypDocument=" + TypDocument +
								             "&nBranch=" + cbeBranchOri.value + "&nProduct=" + valProductOri.value +  
				                             "&nReceipt=" + tcnReceiptOri.value + "&nProponum=" + tcnProponumOri.value);
							}	
						else{
						    tcnPolicyOri.value='';
						    cbeCurrencyOri.value=0;
						    tcnAmountOri.value='';
						    tcnDraftOri.value='';
						    tcnAmountTrasOri.value='';	
						    tcnInt_moraTrasOri.value='';
						    cbeCurrencyDes.value=0;
						    tcnAmountDes.value='';
						}
						mlngBranchOri = (cbeBranchOri.value=''?0:cbeBranchOri.value)
						mlngProductOri = valProductOri.value
						mlngProponumOri = (tcnProponumOri.value=''?0:tcnProponumOri.value)
					}	
					break;
				case 'Receipt': 				    
					if (TypDocument=='Ori'){
						if (tcnReceiptOri.value>0 && tcnReceiptOri.value!=''){
							if (tcnReceiptOri.value!=mlngReceiptOri){								
								insDefValues("ShowDataCO634", "sDocument=" + Document + "&sTypDocument=" + TypDocument + 
				                             "&nReceipt=" + tcnReceiptOri.value + "&nProponum=" + tcnProponumOri.value);

							}}
						else{
						    cbeBranchOri.value=0;
						    valProductOri.value='';
						    UpdateDiv('valProductOriDesc', '');
						    tcnPolicyOri.value='';
						    cbeCurrencyOri.value=0;
						    tcnAmountOri.value='';
						    hddnContratOri.value='';
						    tcnDraftOri.value='';
						    tcnAmountTrasOri.value='';	
						    tcnInt_moraTrasOri.value='';
							ShowDiv('DivlblDraftOri', 'hide');
							ShowDiv('DivDraftOri', 'hide');
							ShowDiv('DivlblDraftDes', 'hide');
							ShowDiv('DivDraftDes', 'hide');
						}
						mlngReceiptOri = (tcnReceiptOri.value=''?0:tcnReceiptOri.value);	
					}		
 				    else{
						if (tcnReceiptDes.value>0 && tcnReceiptDes.value!=''){
							if (tcnReceiptDes.value!=mlngReceiptDes){
 							    insDefValues("ShowDataCO634", "sDocument=" + Document + "&sTypDocument=" + TypDocument + 
					            "&nReceipt=" + tcnReceiptDes.value + "&nProponum=" + tcnProponumDes.value);
							}}
						else{
						    cbeBranchDes.value=0;
						    valProductDes.value='';
						    UpdateDiv('valProductDesDesc', '');
						    tcnPolicyDes.value='';
						    cbeCurrencyDes.value=0;
						    tcnAmountDes.value='';
						    hddnContratDes.value='';
						    tcnDraftDes.value='';
						}
						mlngReceiptDes = (tcnReceiptDes.value=''?0:tcnReceiptDes.value)
					}	
			};
		}
	}
}

//% insChangeTypTras: Se setean los campos dependiendo del tipo de traspaso a efectuar.
//-------------------------------------------------------------------------------------------
function insChangeTypTras(Field){
//-------------------------------------------------------------------------------------------
	var lblnDisabled = (Field.value==1?true:false)
	
	if (lblnDisabled)
	   { 
	    ShowDiv('DivlblProponumOri', 'hide');
	    ShowDiv('DivProponumOri', 'hide');
	    ShowDiv('DivlblProponumDes', 'hide');
	    ShowDiv('DivProponumDes', 'hide');
	    ShowDiv('DivlblReceiptOri', 'show');
	    ShowDiv('DivReceiptOri', 'show');
	    ShowDiv('DivlblReceiptDes', 'show');
	    ShowDiv('DivReceiptDes', 'show');  
	    }
	 else    
	    {
	    ShowDiv('DivlblProponumOri', 'show');
	    ShowDiv('DivProponumOri', 'show');
	    ShowDiv('DivlblProponumDes', 'show');
	    ShowDiv('DivProponumDes', 'show');
	    ShowDiv('DivlblReceiptOri', 'hide');
	    ShowDiv('DivReceiptOri', 'hide');
	    ShowDiv('DivlblReceiptDes', 'hide');
	    ShowDiv('DivReceiptDes', 'hide');  
		ShowDiv('DivlblDraftOri', 'hide');
		ShowDiv('DivDraftOri', 'hide');
		ShowDiv('DivlblDraftDes', 'hide');
		ShowDiv('DivDraftDes', 'hide');
	    }
	    
	    
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false	    
    with(self.document.forms[0]){
		cbeBranchOri.value='';
		cbeBranchDes.value='';
		valProductOri.value='';
		$(valProductOri).change();
		valProductDes.value='';
		$(valProductDes).change();
		tcnProponumOri.value='';
		tcnProponumDes.value='';
		tcnPolicyOri.value='';
		tcnPolicyDes.value='';
		tcnReceiptOri.value='';
		tcnReceiptDes.value='';
		cbeCurrencyOri.value='';
		cbeCurrencyDes.value='';
		tcnAmountOri.value='';
		tcnAmountTrasOri.value='';
		tcnAmountDes.value='';
		hddnContratOri.value='';
		hddnContratDes.value='';
		tcnDraftOri.value='';
		tcnDraftDes.value='';		
		tcnInt_moraTrasOri.value='';
		cbeBranchDes.disabled=lblnDisabled;
	    valProductDes.disabled=lblnDisabled;
		cbeBranchOri.disabled=lblnDisabled;
	    valProductOri.disabled=lblnDisabled;
		btnvalProductDes.disabled=valProductDes.disabled;
		btnvalProductOri.disabled=valProductOri.disabled;
		tcnProponumOri.disabled=lblnDisabled;
		tcnProponumDes.disabled=lblnDisabled;
		tcnReceiptOri.disabled=!lblnDisabled;
		tcnReceiptDes.disabled=!lblnDisabled;			
	    mlngReceiptOri='';
	    mlngReceiptDes='';
	    mlngProponumOri='';
	    mlngProponumDes='';
	    mlngBranchOri='';
	    mlngBranchDes='';
	    mlngProductOri='';
	    mlngProductDes='';
	}
}

//% insChangeBranch: Se limpian los campos producto y poliza al cambiar el ramo.
//-------------------------------------------------------------------------------------------
function insChangeBranch(Type){
//-------------------------------------------------------------------------------------------
//+ Si el tipo de suspensión es por póliza/certificado: lblnDisabled true sino false	

    with(self.document.forms[0]){
//+ Si se trata del documento origen
		if (Type=='Ori') {
			valProductOri.value='';
			UpdateDiv('valProductOriDesc', '');
			tcnProponumOri.value='';
			tcnPolicyOri.value='';
			tcnReceiptOri.value='';
			cbeCurrencyOri.value='';
			tcnAmountOri.value='';
	        mlngBranchOri='';	        
	        mlngProductOri='';    
		} else {
//+ Si se trata del documento destino
			valProductDes.value='';
			UpdateDiv('valProductDesDesc', '');
			tcnProponumDes.value='';
			tcnPolicyDes.value='';
			tcnReceiptDes.value='';
	        mlngBranchDes='';	        
	        mlngProductDes='';    
		}
	}
}

</SCRIPT>
    <%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("CO634", "CO634_k.aspx", 1, vbNullString))
	Response.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), "CO634_k.aspx"))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
Call insReaInitial()
Call insOldValues()
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="CO634" ACTION="valCollectionTra.aspx?sMode=2">
    <BR></BR>
    <%Response.Write(mobjValues.ShowWindowsName("CO634", Request.QueryString.Item("sWindowDescript")))%>
    
    <TABLE WIDTH="100%">
        <TR>
		    <TD COLSPAN="5" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		<TR>
		    <TD COLSPAN="5" CLASS="HorLine"></TD>
		</TR>		
		<TR>
			<TD>&nbsp</TD>
		    <TD><%=mobjValues.OptionControl(0, "optTypTras", GetLocalResourceObject("optTypTras_1Caption"), "1", "1", "insChangeTypTras(this)",  , 1)%> </TD>
		    <TD>&nbsp</TD>
		    <TD><%=mobjValues.OptionControl(0, "optTypTras", GetLocalResourceObject("optTypTras_2Caption"), vbNullString, "2", "insChangeTypTras(this)",  , 2)%> </TD>
		    <TD>&nbsp</TD>
        </TR>        
        <TR>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		    <TD></TD>
			<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>		    
		</TR>
		<TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
		    <TD></TD>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>
		</TR>		
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBranchOri", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProductOri)!=""undefined"")document.forms[0].valProductOri.Parameters.Param1.sValue=this.value; insChangeBranch(""Ori"");insShowDocument(""Proponum"",""Ori"")", True,  , GetLocalResourceObject("cbeBranchOriToolTip"),  , 4)%> </TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeBranchDes", "Table10", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "if(typeof(document.forms[0].valProductDes)!=""undefined"")document.forms[0].valProductDes.Parameters.Param1.sValue=this.value; insChangeBranch(""Des"");insShowDocument(""Proponum"",""Des"")", True,  , GetLocalResourceObject("cbeBranchDesToolTip"),  , 11)%> </TD>
        </TR>        
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductOriCaption") %></LABEL></TD>	
			<TD>
				<%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProductOri", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "insShowDocument(""Proponum"",""Ori"")", True, 4, GetLocalResourceObject("valProductOriToolTip"),  , 5))
End With
%>
			</TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductOriCaption") %></LABEL></TD>	
			<TD>
				<%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProductDes", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "insShowDocument(""Proponum"",""Des"")", True, 4, GetLocalResourceObject("valProductDesToolTip"),  , 12))
End With
%>
			</TD>
		</TR>
			<TR>
				<TD><DIV ID="DivlblProponumOri"><LABEL ID=0><%= GetLocalResourceObject("tcnProponumOriCaption") %></LABEL></DIV><DIV ID="DivlblReceiptOri"><LABEL ID=0>Recibo</LABEL></DIV></TD>
				<TD><DIV ID="DivProponumOri"><%=mobjValues.NumericControl("tcnProponumOri", 10, "",  , GetLocalResourceObject("tcnProponumOriToolTip"),  ,  ,  ,  ,  , "insShowDocument(""Proponum"",""Ori"")", True, 6)%></DIV><DIV ID="DivReceiptOri"><%=mobjValues.NumericControl("tcnReceiptOri", 10, "",  , "Número del recibo origen a tratar",  ,  ,  ,  ,  , "insShowDocument(""Receipt"",""Ori"")",  , 8)%></DIV></TD>
				<TD>&nbsp</TD>
				<TD><DIV ID="DivlblProponumDes"><LABEL ID=0><%= GetLocalResourceObject("tcnProponumOriCaption") %></LABEL></DIV><DIV ID="DivlblReceiptDes"><LABEL ID=0>Recibo</LABEL></DIV></TD>
				<TD><DIV ID="DivProponumDes"><%=mobjValues.NumericControl("tcnProponumDes", 10, "",  , GetLocalResourceObject("tcnProponumDesToolTip"),  ,  ,  ,  ,  , "insShowDocument(""Proponum"",""Des"")", True, 13)%></DIV><DIV ID="DivReceiptDes"><%=mobjValues.NumericControl("tcnReceiptDes", 10, "",  , "Número del recibo destino a tratar",  ,  ,  ,  ,  , "insShowDocument(""Receipt"",""Des"")",  , 15)%></DIV></TD>
			</TR>					
		</DIV>							
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicyOri", 10, "",  , GetLocalResourceObject("tcnPolicyOriToolTip"),  ,  ,  ,  ,  ,  , True, 7)%></TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnPolicyOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPolicyDes", 10, "",  , GetLocalResourceObject("tcnPolicyDesToolTip"),  ,  ,  ,  ,  ,  , True, 14)%></TD>
		</TR>	
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrencyOri", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyOriToolTip"),  , 9)%> </TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurrencyOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrencyDes", "Table11", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyDesToolTip"),  , 16)%> </TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAmountOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmountOri", 18, "",  , GetLocalResourceObject("tcnAmountOriToolTip"), True, 6,  ,  ,  ,  , True, 10)%></TD>
			<TD>&nbsp</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAmountOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmountDes", 18, "",  , GetLocalResourceObject("tcnAmountDesToolTip"), True, 6,  ,  ,  ,  , True, 17)%></TD>
        </TR>
        <TR>
			<TD><DIV ID="DivlblDraftOri"><LABEL ID=0><%= GetLocalResourceObject("tcnDraftOriCaption") %></LABEL></DIV></TD>
			<TD><DIV ID="DivDraftOri"><%=mobjValues.NumericControl("tcnDraftOri", 5, "",  ,  ,  ,  ,  ,  ,  ,  , True)%></DIV></TD>
			<TD><%=mobjValues.HiddenControl("hddnContratOri", "")%>&nbsp<%=mobjValues.HiddenControl("hddnContratDes", "")%></TD>
			<TD><DIV ID="DivlblDraftDes"><LABEL ID=0><%= GetLocalResourceObject("tcnDraftOriCaption") %></LABEL></DIV></TD>
			<TD><DIV ID="DivDraftDes"><%=mobjValues.NumericControl("tcnDraftDes", 5, "",  ,  ,  ,  ,  ,  ,  ,  , True)%></DIV></TD>
        </TR>        
        <BR>
        <TR>
		    <TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
			<TD COLSPAN="3">&nbsp</TD>
		</TR>
		
		<TR>
		    <TD COLSPAN="2" CLASS="HorLine"></TD>		    
		</TR>
	    <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAmountTrasOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmountTrasOri", 18, "",  , GetLocalResourceObject("tcnAmountTrasOriToolTip"), True, 6,  ,  ,  ,  , True, 19)%></TD>
			<TD COLSPAN="3">&nbsp</TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnInt_moraTrasOriCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnInt_moraTrasOri", 18, "",  , GetLocalResourceObject("tcnInt_moraTrasOriToolTip"), True, 6,  ,  ,  ,  , True, 20)%></TD>
			<TD COLSPAN="4">&nbsp</TD>
        </TR>

        <%=mobjValues.HiddenControl("tcdStatDate", "")%>
       	
       	<%With Response
	.Write("<SCRIPT>")
	.Write("ShowDiv('DivlblProponumOri', 'hide');")
	.Write("ShowDiv('DivProponumOri', 'hide');")
	.Write("ShowDiv('DivlblProponumDes', 'hide');")
	.Write("ShowDiv('DivProponumDes', 'hide');")
	.Write("ShowDiv('DivlblDraftOri', 'hide');")
	.Write("ShowDiv('DivDraftOri', 'hide');")
	.Write("ShowDiv('DivlblDraftDes', 'hide');")
	.Write("ShowDiv('DivDraftDes', 'hide');")
	.Write("</SCRIPT>")
End With%>

    </TABLE>
</FORM> 
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.47
Call mobjNetFrameWork.FinishPage("co634_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




