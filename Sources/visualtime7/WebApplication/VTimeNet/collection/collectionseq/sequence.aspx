<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 3/4/03 12.00.00
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

Dim lobjCollectionSeq As eCollection.CollectionSeq
Dim lclsSequence As eFunctions.Sequence


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("Sequence")

lclsSequence = New eFunctions.Sequence
'^Begin Body Block VisualTimer Utility 1.1 3/4/03 12.00.00
lclsSequence.sSessionID = Session.SessionID
lclsSequence.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>

<HTML>
<HEAD>
	
	<META NAME="ProgId" content="FrontPage.Editor.Document">
	<BASE TARGET="fraFolder">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Sequence.js"></SCRIPT>
<SCRIPT>    
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $|$$Author: Iusr_llanquihue $"
    
        var pblnQuery=(<%=Session("CO001_nAction")%>=='2'?true:false); //Consulta

//% insfraHeaderComplete: 
//--------------------------------------------------------------------------------------------
function insfraHeaderComplete(){
//--------------------------------------------------------------------------------------------

if(typeof(top.fraHeader.pstrCodispl) != 'undefined'){
    return true;
}
//else{
//    setTimeout(insfraHeaderComplete(), 1000);
//}
}
        
        
</SCRIPT>
</HEAD>
<BODY>
<SCRIPT>insfraHeaderComplete();</SCRIPT>
<%
lobjCollectionSeq = New eCollection.CollectionSeq

'+ Se arma la secuencia
Response.Write(lobjCollectionSeq.insLoadTabs(Session("CO001_nAction"), Session("nBordereaux")))

    If Request.QueryString.Item("sGoToNext") <> "NO" Then 
        Response.Write("<SCRIPT>NextWindows('" & Request.QueryString.Item("nOpener") & "')</SCRIPT>")
    End If

lobjCollectionSeq = Nothing

If Session("CO001_nAction") = 2 Then
	Session("bQuery") = True
Else
	Session("bQuery") = False
    End If
    
%>

</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 3/4/03 12.00.00
Call mobjNetFrameWork.FinishPage("Sequence")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>




