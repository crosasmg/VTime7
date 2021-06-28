<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjMenu As eFunctions.Menues

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjClient As Object
Dim mstrClient As Object
Dim mstrDigit As Object
Dim mintPerson As Object
Dim mblnLink As Boolean
Dim lclsClient_req As eProduct.Client_req
Dim mstrCommand As String


</script>
<%Response.Expires = 0
Session("ConnectID") = Session.SessionID
Session("mintReload") = 1

mobjValues = New eFunctions.Values

mblnLink = False

'+ El parámetro del QueryString "sClientCode" viene lleno si esta ventana es invocada
'+ desde la CA025 (Clientes) - ACM - 27/06/2001
With Request
	If .QueryString.Item("sOriginalForm") <> vbNullString Then
		If .QueryString.Item("sClientCode") <> vbNullString Then
			mblnLink = True
			Session("sClient") = .QueryString.Item("sClientCode")
			mstrClient = .QueryString.Item("sClientCode")
			mstrDigit = .QueryString.Item("sDigit")
			mintPerson = .QueryString.Item("nPerson_typ")
			If .QueryString.Item("sOriginalForm") = "CA025" Then
				Session("sOriginalForm") = .QueryString.Item("sOriginalForm")
			End If
		End If
	End If
	
	If .QueryString.Item("sLinkSpecial") <> vbNullString Then
		Session("sLinkSpecial") = "1"
		mblnLink = True
	End If
	
	If Not IsNothing(.QueryString("LinkParamsClient")) Then
		mblnLink = True
		mstrClient = .QueryString.Item("LinkParamsClient")
		mstrDigit = .QueryString.Item("sDigit")
		If Not String.IsNullOrEmpty(mstrClient)  Then
			Dim oClient as New eClient.Client
			If oClient.Find(mstrClient) Then
				mintPerson = oClient.nPerson_typ
			End If
		End If
		Session("sLinkControl") = .QueryString.Item("LinkParamsClientControl")
	End If

	If Not mblnLink Then
		If .QueryString.Item("action") = "301" Then
			mstrClient = ""
			mstrDigit = ""
		Else
			mstrClient = Session("sClient")
			mstrDigit = Session("Digit")
			mintPerson = Session("nPerson_typ")
		End If
	End If
End With
%>
<HTML>
<HEAD>
	<%=mobjValues.StyleSheet()%>
	<META HTTP-EQUIV="Content-Language" CONTENT="es">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 23/09/03 16:00 $"
		
//%insStateZone. Esta función se encarga de habilitar los controles cuando se selecciona una acción
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
	var llngReload='<%=Request.QueryString.Item("llngReload")%>'
	var lintAction='<%=Request.QueryString.Item("action")%>'
	var lstrOriginalForm='<%=Request.QueryString.Item("sOriginalForm")%>'
	var lstrLinkSpecial='<%=Request.QueryString.Item("sLinkSpecial")%>'
	var lstrClient='<%=Request.QueryString.Item("LinkParamsClient")%>'
	var lstrClientControl='<%=Request.QueryString.Item("LinkParamsClientControl")%>'
	var lstrDigit='<%=Session("Digit")%>'
	if (qs("LinkParamsDigit") >""){
		lstrDigit=qs("LinkParamsDigit");
	} 

//+ Si la variable lintAction tiene valor es porque se recargó la página.
	if (lintAction!='') {
		if (lintAction!=top.fraSequence.plngMainAction) {
			llngReload = '0';
		} else {
			llngReload = '1';
		}
	} else {
		if (top.fraSequence.plngMainAction == 301) {
			llngReload = '0';		
		} else {
			llngReload = '1';
		}
	}
	if (llngReload!='1') {
		if ((lstrLinkSpecial!='') || (lstrOriginalForm!='')) {
			lstrLinkSpecial = '1';
		}else{
			lstrLinkSpecial = '';
			lstrClient = ''
		}
	}    
    
	with (self.document.forms[0])
	{
	    
        if (top.fraSequence.plngMainAction==301){
		    cbePerson_typ.disabled = false;
		    btntctClient.disabled  = true;
		}
		else{   
		   cbePerson_typ.disabled = true;
		   btntctClient.disabled  = false;
		}
		   
		UpdateDiv('tctCliename','');
		elements[0].disabled = false;
		
//+ En caso de que sea registrar se habilida el campo verificador del código de cliente.		
		if (top.fraSequence.plngMainAction==301) {
		    elements[1].disabled = false;
		    cbePerson_typ.value='';
		}
		
		if (typeof(tctClient_Digit)!='undefined') elements[2].disabled = false;

		if (top.fraSequence.plngMainAction==301) {
			if (lstrLinkSpecial!='') {
				elements[0].value = lstrClient;
				if (lstrDigit == "E") {
					elements[1].value = lstrDigit;
					elements[1].disabled = true;
				}
				if (lstrDigit == "E") {
					if (elements[0].value != '') {
						elements[0].disabled = true;
					}
				}else {
					if (elements[0].value != '') {
						elements[0].disabled = false;
					}
				}
			} else {
				if (document.location.href.indexOf("LinkSpecial")==-1) {
					elements[0].value = '';
					elements[1].value = '';
				}
			}		 
			 if (typeof(tctClient_Digit)!='undefined') elements[2].value = "";
		} else {
			if (elements[0].value!='') {
				$(elements[0]).change();
			}
		}
	}
}
   
//%insCancel.Esta función muestra la ventana de cancelación de proceso
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
    var lintAction = top.frames["fraSequence"].plngMainAction;

    if (top.frames["fraSequence"].pintZone==2 && top.frames["fraSequence"].plngMainAction==301)
		ShowPopUp("/VTimeNet/Common/GE101.aspx?sCodispl=BC003_K","EndProcess",300,180)
	else
	    return (true);
	    
}   

//%insFinish. Esta función es utilizada para realizar cambios al momento de finalizar la transacción
//------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------
    return true;
}

//% ExecuteFieldBlur: Ejecuta el método OnBlur (pierde el foco) del campo "tctClient".
//					  El campo al perder el foco, trae la data relacionada con el código
//					  del cliente - ACM - 02/08/2001
//------------------------------------------------------------------------------
function ExecuteFieldBlur(){
//------------------------------------------------------------------------------
//+ Si el valor de el campo "tctClient" es distinto de blanco, entonces se procede a
//+ obligar al campo "tctClient" a que pierda el foco, para que traiga la data asociada
//+ al mismo - ACM - 02/08/2001
	with (self.document.forms[0]) {
		elements["tctClient"].disabled = false;
		if(elements["tctClient"].value!="")
		{
			$(elements["tctClient"]).change();
			elements["tctClient"].disabled = true;
		}
	}
}
</SCRIPT>
<%
mobjMenu = New eFunctions.Menues
Response.Write(mobjMenu.MakeMenu("BC003_K", "BC003_k.aspx", 1, ""))
mobjMenu = Nothing
%>
</HEAD>
<SCRIPT>
    var marrActions = []
    var mintActionQ = -1

//%AddAction: Se asigna los valores al arreglo
//-----------------------------------------------------------------------    
function AddAction(Action,ActionDes,HelAction) {
//-----------------------------------------------------------------------    
    var larrAction = []

    larrAction[0] = Action
    larrAction[1] = ActionDes
    larrAction[2] = HelAction

    marrActions[++mintActionQ] = larrAction
}
</SCRIPT>
<BODY ONUNLOAD="closeWindows();"> 
<BR><BR>
<FORM METHOD= "POST" ACTION="valClientSeq.aspx?TIMEINFO=5" id=form1 name=form1>
	<TABLE WIDTH=100%>
		<TR>
			<TD WIDTH=20%><LABEL><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>
			<TD><%=mobjValues.ClientControl("tctClient", mstrClient, True, GetLocalResourceObject("tctClientToolTip"),  , True, "tctCliename",  ,  ,  ,  ,  ,  ,  , False)%></TD>
<!-- + Se añade un campo oculto para que almacene el valor del parámetro "sOriginalForm", el cual
	   contiene valor únicamente si esta ventana es llamada desde la CA025 - ACM - 31/07/2001 -->	
				<%=mobjValues.HiddenControl("tctOriginalForm", Request.QueryString.Item("sOriginalForm"))%>
		</TR>
		<TR>	
			<TD><LABEL><%= GetLocalResourceObject("cbePerson_typCaption") %><LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbePerson_typ", "table5006", eFunctions.Values.eValuesType.clngComboType, mintPerson,  ,  ,  ,  ,  ,  , Request.QueryString.Item("action") <> "301",  , GetLocalResourceObject("cbePerson_typToolTip"))%></TD>
		</TR>			
	</TABLE>
</FORM>
</BODY>
</HTML>
<SCRIPT>

	var lstrAction = '<%=Request.QueryString.Item("nMainAction")%>'
	var lstrClientCode = '<%=Request.QueryString.Item("sClientCode")%>'

	if (top.fraSequence.plngMainAction==301)
		self.document.forms[0].elements[1].value = ""
	else
		self.document.forms[0].elements[1].value = '<%=mstrDigit%>'

	if (lstrClientCode != ''){
		if (lstrAction!=0)
		    ClientRequest(lstrAction);	
	}
	else{
		if (top.frames["fraSequence"].plngMainAction!=0 && typeof(top.frames["fraSequence"].plngMainAction)!="undefined") {
		    var lintAction = (top.frames["fraSequence"].plngMainAction);
		    ClientRequest(lintAction);
		}
	}	

</SCRIPT>
<%
If CStr(Session("LinkSpecialBC003_K")) <> "" Then
	Response.Write("<SCRIPT>ClientRequest(" & Request.QueryString.Item("LinkSpecialAction") & ")</SCRIPT>")
End If

If Request.QueryString.Item("sOriginalForm") <> vbNullString Then
	lclsClient_req = New eProduct.Client_req
	
	Session("nRole") = Request.QueryString.Item("sRoleCode")
	'+ Si no se envía código del cliente no envía validación        
	If Not IsNothing(Request.QueryString.Item("sClientCode")) And CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) <> 401 Then
		Session("sErrorTable") = lclsClient_req.insValClient_Req(Request.QueryString.Item("sCodispl"), Session("nBranch"), Session("nProduct"), CInt(Request.QueryString.Item("sRoleCode")), Session("nTransaction"), Session("dEffecdate"))
		
		If CStr(Session("sErrorTable")) > vbNullString Then
			mstrCommand = "&sModule=Client&sProject=ClientSeq&sCodisplReload=" & Request.QueryString.Item("sCodispl")
			
			With Response
				.Write("<SCRIPT LANGUAGE=JAVASCRIPT>")
				.Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.URLEncode(mstrCommand) & "&sQueryString=" & Server.URLEncode(Request.Params.Get("Query_String")) & """, ""ClientSeqError"",660,330);")
				.Write("</SCRIPT>")
			End With
		End If
	End If
	lclsClient_req = Nothing
End If
%>




