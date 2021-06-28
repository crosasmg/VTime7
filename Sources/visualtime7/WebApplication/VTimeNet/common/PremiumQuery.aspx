<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjGrid As eFunctions.Grid
Dim mobjMenu As eFunctions.Menues


'% insDefineHeader: Se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "SCA851"
	Call mobjGrid.SetWindowParameters("SCA851", "", "")
	
	'+ Se definen las columnas del grid  
	
	With mobjGrid.Columns
		.AddTextColumn(0, GetLocalResourceObject("tctBranchColumnCaption"), "tctBranch", 30, "",  , GetLocalResourceObject("tctBranchColumnCaption"))
		.AddTextColumn(0, GetLocalResourceObject("tctProductColumnCaption"), "tctProduct", 30, "",  , GetLocalResourceObject("tctProductColumnCaption"))
		.AddNumericColumn(0, GetLocalResourceObject("tcnPolicyColumnCaption"), "tcnPolicy", 15, CStr(0))
		.AddNumericColumn(0, GetLocalResourceObject("tcnPropColumnCaption"), "tcnProp", 15, CStr(0))
		.AddNumericColumn(0, GetLocalResourceObject("tcnCertifColumnCaption"), "tcnCertif", 15, CStr(0))
		.AddNumericColumn(0, GetLocalResourceObject("tcnReceiptColumnCaption"), "tcnReceipt", 15, CStr(0))
		.AddTextColumn(0, GetLocalResourceObject("tctStatus_preColumnCaption"), "tctStatus_pre", 30, "",  , GetLocalResourceObject("tctStatus_preColumnCaption"))
		.AddDateColumn(0, GetLocalResourceObject("tcdEfeccdateColumnCaption"), "tcdEfeccdate")
		.AddDateColumn(0, GetLocalResourceObject("tctExpirdatColumnCaption"), "tctExpirdat")
		.AddNumericColumn(0, GetLocalResourceObject("tcnBalanceColumnCaption"), "tcnBalance", 18, CStr(eRemoteDB.Constants.intNull),  ,  , True, 6,  ,  ,  , True)
	End With
	
	'+ Se definen las propiedades generales del grid
	
	With mobjGrid
		.DeleteButton = False
		.AddButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

'% insPrePremiumQuery: Se cargan los controles de la página
'--------------------------------------------------------------------------------------------
Private Sub insPrePremiumQuery()
	'dim eRemoteDB.Constants.intNull As Object
	'--------------------------------------------------------------------------------------------
	Dim lintCount As Short
	Dim lobjGen As Object
	Dim lobjObject As Object
	Dim lcolObj As eCollection.Premiums
	
	lcolObj = New eCollection.Premiums
	
	If lcolObj.FindPremiumQuery(Request.QueryString.Item("sClient")) Then
		
		lintCount = 0
		
		For	Each lobjObject In lcolObj
			With lobjObject
				mobjGrid.Columns("tctBranch").DefValue = .sDesBranch
				mobjGrid.Columns("tctProduct").DefValue = .sDesProduct
				
				If .sCertype = "2" Then
					mobjGrid.Columns("tcnPolicy").DefValue = .nPolicy
					mobjGrid.Columns("tcnProp").DefValue  = eRemoteDB.Constants.intNull
				Else
					mobjGrid.Columns("tcnPolicy").DefValue  = eRemoteDB.Constants.intNull
					mobjGrid.Columns("tcnProp").DefValue = .nPolicy
				End If
				mobjGrid.Columns("tcnCertif").DefValue = .nCertif
				mobjGrid.Columns("tcnReceipt").DefValue = .nReceipt
				mobjGrid.Columns("tcnReceipt").HRefScript = "insSelReceipt(" & .nReceipt & "," & .nStatus_pre & "," & .sCertype & ");"
				
				mobjGrid.Columns("tctStatus_pre").DefValue = .sDescStatus_pre
				mobjGrid.Columns("tcdEfeccdate").DefValue = .dEffecdate
				mobjGrid.Columns("tctExpirdat").DefValue = .dExpirdat
				mobjGrid.Columns("tcnBalance").DefValue = .nBalance
				
				
				Response.Write(mobjGrid.DoRow())
			End With
			
			lintCount = lintCount + 1
			
			If lintCount = 200 Then
				Exit For
			End If
		Next lobjObject
	End If
	Response.Write(mobjGrid.closeTable())
	mobjValues.ActionQuery = False
	
Response.Write("" & vbCrLf)
Response.Write("	<BR>" & vbCrLf)
Response.Write("	<TABLE WIDTH=100%>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD COLSPAN=""3"" CLASS=""HORLINE""></TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonAbout("SCA851", "SCA851"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD WIDTH=5%>")


Response.Write(mobjValues.ButtonHelp("SCA851"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD ALIGN=""RIGHT"">")

	
	
	'+ sCodisplOri: indica el codispl que originó la transacción y es asignado a través de la propiedad sQueryString.
	Response.Write(mobjValues.ButtonAcceptCancel( ,  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
	
Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

	
	
	lobjGen = Nothing
	lobjObject = Nothing
	lcolObj = Nothing
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("PremiumQuery")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "SCA851"

%>
<SCRIPT LANGUAGE="JavaScript">
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//------------------------------------------------------------------------------------------
function insSelReceipt(nReceipt,sStatus_pre,sCertype){
//------------------------------------------------------------------------------------------
	alert(nReceipt);
	with (opener.top.frames['fraFolder'].document.forms[0]){
	
		if((sStatus_pre==1)||(sStatus_pre==4)){
		    
		    chkNewReceipt.checked = false;
			chkNewReceipt.disabled = true;
			
		    if(sCertype==2)
				cbeCollecDocTyp.value = 1;
		    else
		        if (hddProdClas.value==4 && hddsApv.value ==1)
					cbeCollecDocTyp.value = 21; //abono propuestas
				else
					cbeCollecDocTyp.value = 7; //abono propuestas
					
			tcnDocument.value = nReceipt;
			opener.top.frames['fraFolder'].$('#tcnDocument').change();
			
		}	
		else{	
			chkNewReceipt.checked = true;
			chkNewReceipt.disabled = true;	
			
			if(sCertype==2)
				cbeCollecDocTyp.value = 1;
		    else
		        if (hddProdClas.value==4 && hddsApv.value ==1)
					cbeCollecDocTyp.value = 21; //abono propuestas
				else
					cbeCollecDocTyp.value = 7; //abono propuestas
					
				
			tcnDocument.value = nReceipt;
			opener.top.frames['fraFolder'].$('#tcnDocument').change();		
        }                   
 	}
 	window.close();	
}

//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 14/11/03 12:55 $"    

</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <%mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.setZone(2, "SCA851", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))

mobjValues.ActionQuery = True
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="post" ID="FORM" NAME="SCA851" ACTION="ValPolicyQue.aspx?Zone=2">
<%
Response.Write(mobjValues.ShowWindowsName("SCA851"))
Response.Write(mobjValues.WindowsTitle("SCA851"))
Response.Write("<BR>")
Call insDefineHeader()
Call insPrePremiumQuery()

mobjGrid = Nothing
mobjValues = Nothing
%>     
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("PremiumQuery")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




