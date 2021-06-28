<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsPolicy As ePolicy.Policy
Dim mintTransacio As Object
Dim nWay_Pay As Object
Dim nDirect As Object
Dim nIndirect As Object


'%insPreFolder: Esta función carga los datos iniciales en la ventana
'-----------------------------------------------------------------------------------------
Private Function insPreFolder() As Object
	'-----------------------------------------------------------------------------------------
	Dim lclsClientRoles As ePolicy.Roles
	Dim lclsClient As eClient.Client
	Dim lclsCertificat As ePolicy.Certificat
	
	Dim lstrClientEmpl As String
	Dim lstrClientPaga As String
	Dim lstrEmpleador As String
	Dim lstrPagador As Object
	
	lclsClientRoles = New ePolicy.Roles
	lclsClient = New eClient.Client
	lclsCertificat = New ePolicy.Certificat
	
	If Len(lstrClientEmpl) < 14 Then
		lstrClientEmpl = lclsClient.ExpandCode(lstrClientEmpl)
	End If
	
	If Len(lstrClientPaga) < 14 Then
		lstrClientPaga = lclsClient.ExpandCode(lstrClientPaga)
	End If
	
	If lclsClientRoles.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 85, lstrClientEmpl, Today, True) Then
		lstrEmpleador = lclsClientRoles.sClient
	End If
	
	If lclsClientRoles.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), 25, lstrClientEmpl, Today, True) Then
		lstrClientPaga = lclsClientRoles.sClient
	End If
	
	If lclsCertificat.Find("2", Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif")) Then
		nWay_Pay = lclsCertificat.nWay_Pay
	End If
	
	If lstrEmpleador <> vbNullString And lstrClientPaga <> vbNullString Then
		If nWay_Pay = 3 Then
			If lstrEmpleador = lstrClientPaga Then
				nDirect = 1
				nIndirect = 0
			Else
				nDirect = 0
				nIndirect = 1
			End If
		ElseIf nWay_Pay = 1 Or nWay_Pay = 2 Then 
			nDirect = 1
			nIndirect = 0
		End If
	End If
	
	Call mclsPolicy.insPreCA642("CA642", Session("nBranch"), Session("nProduct"), Session("nPolicy"))
	
	lclsClientRoles = Nothing
	lclsClient = Nothing
	lclsCertificat = Nothing
	
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("ca642")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "ca642"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mclsPolicy = New ePolicy.Policy
Response.Write(mobjMenu.setZone(2, "CA642", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
	<%=mobjValues.StyleSheet()%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 26/03/04 17:54 $"

//% InsShowHeader: Muestra los valores en la página del encabezado
//------------------------------------------------------------------
function InsShowHeader(){
//------------------------------------------------------------------
    var lblnContinue=true
}


//% InsChangeField: se controla cuando cambia la frecuencia de pago.
//------------------------------------------------------------------
function InsChangeField(objField){
//------------------------------------------------------------------
	with (self.document.forms[0])
	{
		tcdNewChangdat.disabled = true;
		btn_tcdNewChangdat.disabled = true;
		switch (objField.name)
		{
			case 'tcnNewPayfreq':
			{
				if (objField.value < valNpayfreq.value)
				{
//+ Mayor periodicidad a Menor periodicidad (ej: mensual a anual)
				    if (objField.value != "6")
				    {
						tcdNewChangdat.value = tcdDateTo.value;
						ShowChangeValues("NewNextreceip");
                    }
                    else
                    {
						tcdNewChangdat.value = tcdDateToForce.value;
						tcdNewNextreceip.value = tcdDateTo.value;
                    }
                }
                else
                {
                    if (objField.value > valNpayfreq.value)
                    {
//+ Menor periodicidad a Mayor periodicidad (ej: anual a mensual)
                       if (objField.value != "6")
                       {
							tcdNewChangdat.value = tcdDateTo.value;
							ShowChangeValues("NewNextreceip");
							tcdNewNextreceip.value = tcdDateTo.value;
                        }
                        else
                        {
                            tcdNewChangdat.value = tcdDateTo.value;
                            ShowChangeValues("NewNextreceip");
                        }
                    }
                    else
                    {
						if (objField.value = valNpayfreq.value)
						{
                            tcdNewChangdat.value = tcdChangdat.value;
                            tcdNewNextreceip.value = tcdNextreceip.value;
						}
					}
                }
				break;
			}
		}
	}
}
//% ShowChangeValues: Se cargan los valores de acuerdo producto seleccionado
//--------------------------------------------------------------------------
function ShowChangeValues(sField){
//--------------------------------------------------------------------------
    var strParams;
    switch(sField)
    {
	    case "NewNextreceip":
	    {
      		strParams = "nPayfreq="      + self.document.forms[0].tcnNewPayfreq.value  +
                        "&nPayfreqOld="  + self.document.forms[0].valNpayfreq.value   +
	                    "&dChandat="     + self.document.forms[0].tcdNewChangdat.value + 
	                    "&dExpirdat="    + self.document.forms[0].tcdExpirdat.value +
	                    "&dChandat_ori=" + self.document.forms[0].tcdChangdat.value
			insDefValues(sField,strParams,'/VTimeNet/Policy/PolicyTra');
	        break;
	    }
	    case "FindRefund":
	    {
	        if (self.document.forms[0].tcnNewPayfreq.value < self.document.forms[0].valNpayfreq.value)
	        {
//+ Mayor periodicidad a Menor periodicidad
      				strParams = "dEffecdate=" + self.document.forms[0].tcdStartdate.value + 
							    "&sInd=1"
					insDefValues(sField,strParams,'/VTimeNet/Policy/PolicyTra'); 
			}
            else
            {
//+ Menor periodicidad a Mayor periodicidad
      				strParams = "dEffecdate=" + self.document.forms[0].tcdStartdate.value + 
							    "&sInd=2"
					insDefValues(sField,strParams,'/VTimeNet/Policy/PolicyTra'); 
            }
			break;
		}
    }
}
</SCRIPT>
</HEAD>
<%Call insPreFolder()%>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmReahPolicy" ACTION="ValPolicyTra.aspx?x=1&nTransacio=<%=mintTransacio%>">
    	<%=mobjValues.ShowWindowsName("CA642", Request.QueryString.Item("sWindowDescript"))%>
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=13909><%= GetLocalResourceObject("tcnNewPayfreqCaption") %></LABEL></TD>
            <TD COLSPAN="2">
                 <%mobjValues.TypeList = 2
mobjValues.List = "6,8"
mobjValues.BlankPosition = False
mobjValues.Parameters.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("nQuota", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

Response.Write(mobjValues.PossiblesValues("tcnNewPayfreq", "tabPay_fracti", eFunctions.Values.eValuesType.clngComboType, "", True,  ,  ,  ,  , "InsChangeField(this);ShowChangeValues(""FindRefund"");ShowChangeValues(""NewNextreceip"");",  ,  , GetLocalResourceObject("tcnNewPayfreqToolTip")))%>
		    </TD>
            <TD><LABEL ID=13908><%= GetLocalResourceObject("tcdNewChangdatCaption") %></LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdNewChangdat",  ,  , GetLocalResourceObject("tcdNewChangdatToolTip"),  ,  ,  , "ShowChangeValues(""NewNextreceip"");")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13908><%= GetLocalResourceObject("tcdNewNextreceipCaption") %></LABEL></TD>
		    <TD COLSPAN="2"><%=mobjValues.DateControl("tcdNewNextreceip",  ,  , GetLocalResourceObject("tcdNewNextreceipToolTip"),  ,  ,  ,  , True)%></TD>
            <TD></TD>
        </TR>
        </TR>
    </TABLE>
    <TABLE WIDTH="100%">
		<BR>
        <TR>
            <TD COLSPAN="2" CLASS="HighLighted" ><LABEL ID=0><A NAME="Datos para la verificación"><%= GetLocalResourceObject("AnchorDatos para la verificaciónCaption") %></A></LABEL></TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HorLine"></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13901><%= GetLocalResourceObject("tctsClientCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.ClientControl("tctsClient", mclsPolicy.DefaultValueCA642("tctsClient"),  , GetLocalResourceObject("tctsClientToolTip"),  , True, "lblCliename", False)%></TD>
            <TD><%=mobjValues.DIVControl("lblCliename1", False, "")%>
            </TD>
	    </TR>
        <TR>
            <TD><LABEL ID=13906><%= GetLocalResourceObject("tcdStartdateCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdStartdate", mclsPolicy.DefaultValueCA642("tcdStartdate"),  , GetLocalResourceObject("tcdStartdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13907><%= GetLocalResourceObject("tcdExpirdatCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdExpirdat", mclsPolicy.DefaultValueCA642("tcdExpirdat"),  , GetLocalResourceObject("tcdExpirdatToolTip"),  ,  ,  ,  , True)%></TD>
        <TR>
            <TD><LABEL ID=13908><%= GetLocalResourceObject("tcdChangdatCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdChangdat", mclsPolicy.DefaultValueCA642("tcdChangdat"),  , GetLocalResourceObject("tcdChangdatToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13906><%= GetLocalResourceObject("tcdNextreceipCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%=mobjValues.DateControl("tcdNextreceip", mclsPolicy.DefaultValueCA642("tcdNextreceip"),  , GetLocalResourceObject("tcdNextreceipToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13907><%= GetLocalResourceObject("valNpayfreqCaption") %></LABEL></TD>
            <TD COLSPAN="1"><%Response.Write(mobjValues.PossiblesValues("valNpayfreq", "table36", eFunctions.Values.eValuesType.clngWindowType, mclsPolicy.DefaultValueCA642("valNpayfreq"),  ,  ,  ,  , 20,  , True, 4, GetLocalResourceObject("valNpayfreqToolTip")))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13907><%= GetLocalResourceObject("cbeWayPayCaption") %></LABEL></TD>
            <TD COLSPAN="1"><%=mobjValues.PossiblesValues("cbeWayPay", "Table5002", eFunctions.Values.eValuesType.clngComboType, nWay_Pay, False, True,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeWayPayToolTip"),  , 17)%>
							<%=mobjValues.HiddenControl("hddWayPay", CStr(1))%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=13907></LABEL></TD>
            <TD COLSPAN="1"><%=mobjValues.OptionControl(0, "optDirecta", GetLocalResourceObject("optDirecta_Caption"), nDirect,  ,  , True, 7)%>
							<%=mobjValues.OptionControl(0, "optDirectb", GetLocalResourceObject("optDirectb_Caption"), nIndirect,  ,  , True, 7)%></TD>
        </TR>
        <TR>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkStatusprepp", GetLocalResourceObject("chkStatuspreppCaption"), mclsPolicy.DefaultValueCA642("chkStatusprepp"),  ,  , True,  , GetLocalResourceObject("chkStatuspreppToolTip"))%></TD>
            <TD><%=mobjValues.CheckControl("chkStatusprepc", GetLocalResourceObject("chkStatusprepcCaption"), mclsPolicy.DefaultValueCA642("chkStatusprepc"),  ,  , True,  , GetLocalResourceObject("chkStatusprepcToolTip"))%></TD>
        </TR>
    </TABLE>
    <%
If mobjValues.TypeToString(mclsPolicy.DefaultValueCA642("tcdNewChangdat"), eFunctions.Values.eTypeData.etdDate) = eRemoteDB.Constants.dtmNull Then
	Response.Write(mobjValues.HiddenControl("tcdDateTo", ""))
Else
	Response.Write(mobjValues.HiddenControl("tcdDateTo", mobjValues.TypeToString(mclsPolicy.DefaultValueCA642("tcdNewChangdat"), eFunctions.Values.eTypeData.etdDate)))
End If

        If mclsPolicy.DefaultValueCA642("tcdDateToForce") = eRemoteDB.Constants.dtmNull Then
            Response.Write(mobjValues.HiddenControl("tcdDateToForce", ""))
        Else
            Response.Write(mobjValues.HiddenControl("tcdDateToForce", mobjValues.TypeToString(mclsPolicy.DefaultValueCA642("tcdDateToForce"), eFunctions.Values.eTypeData.etdDate)))
        End If
%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
mclsPolicy = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.20
Call mobjNetFrameWork.FinishPage("ca642")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





