<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim msStatregt As Object
Dim mblnInformation As Object
Dim mblnVisibleInformation As Object
Dim mintInformation As Object
Dim mblnDisabledStatProduct As Object


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "GI1405"

%>
<SCRIPT>
//% insAcceptData: Se ejecutan las acciones al aceptar la ventana
//-------------------------------------------------------------------------------------------
function insAcceptData(){
//-------------------------------------------------------------------------------------------
	self.document.forms[0].target = 'fraGeneric';
	UpdateDiv('lblWaitProcess','<MARQUEE>Procesando, por favor espere...</MARQUEE>','');
	top.close();
}
//% updateStatus: Actualiza estado de botones y cursor de mouse
//-------------------------------------------------------------------------------------------
function updateStatus(bClose){
//-------------------------------------------------------------------------------------------
    var lintZone = 2
	var lintWindowty = '7'    
    var lintActionType = '' 
    var lintIndex = ''
    var lintMainAction = '' 
    var lstrKey = '' 
    var lobjErr
    
    if(typeof(bClose)=='undefined')
		bClose = true
	
	if(typeof(opener.top)!='unknown')

        if(typeof(opener.top.fraFolder)!='undefined')
            if(typeof(opener.top.fraFolder.document)!='undefined')        
                if(typeof(opener.top.fraFolder.document.cmdAccept)!='undefined')
		            opener.top.fraFolder.document.cmdAccept.disabled = false;
	
//+ Se habilitan/deshabilitan las acciones del ToolBar
        if(typeof(opener.top.fraHeader)!='undefined'){
			with(opener.top.fraHeader){
			    if (document.location.href.indexOf("InSequence")>=0 && (lintWindowty=='7' || lintWindowty=='9'))
			    	insHandImage("A390", true);
			    else
			        insHandImage("A390", !(lintZone==2 || lintWindowty==5));

			    insHandImage("A301", !(lintZone==2));
			    insHandImage("A302", !(lintZone==2));
			    insHandImage("A303", !(lintZone==2));
			    insHandImage("A304", !(lintZone==2));
			    insHandImage("A401", !(lintZone==2));
			    insHandImage("A402", !(lintZone==2));
			    insHandImage("A392", (lintZone==2 || lintWindowty==5));
			    insHandImage("A393", (lintZone==2));
			    insHandImage("A391", true);
			}
		}
        
        try{
            opener.top.fraHeader.setPointer('');
        }
        catch(lobjErr){
			if(typeof(opener.top.fraFolder)!='undefined')
				opener.top.fraFolder.setPointer('');
			else {
				opener.top.setPointer('');
			}
        }

	if(bClose){
	    if (lintActionType=='Check' &&
	        typeof(self.document.forms[0].cmdAccept)=='undefined'){
	    	lintIndex-=1;
	    	cancelEditRecord(mstrQueryString,lintIndex,lintMainAction,lintZone);
	    }
		window.close();
    }
}
</SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <%=mobjValues.WindowsTitle("GI1405")%>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


        <%=mobjValues.StyleSheet()%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="POST" ID="FORM" NAME="frmProductEnd" ACTION="valInterfaceSeq.aspx?nAction=392&sCodispl=GI1405&nZone=2">
            <TABLE WIDTH=100% BORDER="1" CELLPADDING=5 BGCOLOR="white">
                <TR>
                    <TD WIDTH=50%>
                        <LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL>
                        <%=Session("sFile")%>
                    </TD>
                    <TD WIDTH=50%>
                        <LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL>
                        <%=Session("sTable")%>
                    </TD>
                </TR>
            </TABLE>
            <TABLE WIDTH=100%>
	        <TR>
				<TD COLSPAN="4" CLASS="Horline"></TD>
			</TR>
			<TR>
				<TD WIDTH=5%><%=mobjValues.ButtonAbout("GI1405")%></TD>
				<TD WIDTH=5%><%=mobjValues.ButtonHelp("GI1405")%></TD>
				<TD WIDTH=60% ALIGN=LEFT CLASS=HIGHLIGHTED><LABEL><DIV ID=lblWaitProcess></DIV></LABEL></TD>
				<TD ALIGN=RIGHT><%=mobjValues.ButtonAcceptCancel("insAcceptData();", "updateStatus(true);", True)%></TD>
			</TR>
			</TABLE>
        </FORM>
<%
mobjValues = Nothing%>
    </BODY>
</HTML>






