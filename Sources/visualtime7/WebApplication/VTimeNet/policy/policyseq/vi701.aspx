<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.44.15
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Variables generales
Dim mblnFoundGroups As Boolean
Dim mblnFoundSituation As Boolean
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues
'- Declaracion de los objetos con referencias a tablas.
Dim mclsLife As ePolicy.Life
Dim mclspolicy As ePolicy.Policy
    Dim hhdenddate As Object
    Dim blnPolicy As Boolean = False
    Dim mblnColtimre As Boolean = False

'% insPreVI001: Realiza la lectura de los campos a mostrar en pantalla
'----------------------------------------------------------------------------------------------
    Private Sub insPreVI001()
        '----------------------------------------------------------------------------------------------
        Dim lclsGroups As ePolicy.Groups
        Dim lclsSituation As ePolicy.Situation
        Dim lblnExistPolicy As Boolean
        lclsGroups = New ePolicy.Groups
        lclsSituation = New ePolicy.Situation
	
        '+Búsqueda en la tabla Policy        
        If mclspolicy.Find(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), 2), mobjValues.StringToType(Session("nProduct"), 2), mobjValues.StringToType(Session("nPolicy"), 3)) Then
            lblnExistPolicy = True
            
            '+ Se verifica si es la poliza matriz
            blnPolicy = (mclspolicy.sPolitype = "2" And Session("nCertif") = 0)
            'Si la póliza posee renovación simultánea
            If mclspolicy.sColtimre = "1" And Session("nCertif") > 0 Then
                mblnColtimre= True
            Else
                mblnColtimre= False
            End IF
        Else
            lblnExistPolicy = False
            mblnColtimre= False
        End If
	
        With mobjValues
		
            'Response.Write "<NOTSCRIPT>alert('"& mclspolicy.sRepPrintCov &"');</" & "Script>"
            '+ Búsqueda en la tabla life
            Call mclsLife.InsPreVI701(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"))
            '+ Búsqueda en la tabla groups si tiene grupos asociados a la poliza
            mblnFoundGroups = lclsGroups.valGroupExist(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("dEffecdate"))
		
            mblnFoundSituation = lclsSituation.insReaSituation(Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"))
        End With
        lclsGroups = Nothing
        lclsSituation = Nothing

    End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("VI701")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
mclspolicy = New ePolicy.Policy
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.44.16
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "VI701", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

//%insChangeField: Ejecuta acciones al cambiar valor de un campo
//-------------------------------------------------------------------------------------------
function insChangeField(objField){
//-------------------------------------------------------------------------------------------
    var frm = self.document.forms[0]
    var lintQuot
    switch(objField.name){
        case 'tcdEnd_cre':
            if (frm.tcdInit_cre.value != '') {
                    if (frm.tcdEnd_cre.value != '') {

                        var dIniDate = strToDtm(frm.tcdInit_cre.value, 'dd/mm/yyyy', '/');
                        var dEndDate = strToDtm(frm.tcdEnd_cre.value, 'dd/mm/yyyy', '/');

                        if (dEndDate < dIniDate) {
                            document.forms[0].tcdEnd_cre.value = '';
                            alert('La fecha hasta debe ser mayor a la fecha de inicio del credito');
                        } else {
                            insDefValues("insValquota", "&dIniDate=" + frm.tcdInit_cre.value + "&dEndDate=" + frm.tcdEnd_cre.value, '/VTimeNet/Policy/PolicySeq');
                        }


                    } else {
                        document.forms[0].tcdEnd_cre.value = '';
                    } 
               
            }
            break;
        case 'tcnQ_Quot':
        case 'tcdInit_cre':

            //+Actualizar fecha de vencimiento y/o cantidad de cuotas
            //+Se toma fecha desde
            if (frm.tcdInit_cre.value != '') {
                if (frm.tcnQ_Quot.value != '') {
                    //document.forms[0].tcdEnd_cre.value = '';
                //else {
                    lintQuot = frm.tcnQ_Quot.value;

                    var dCurrDate = strToDtm(frm.tcdInit_cre.value, 'dd/mm/yyyy', '/');

                    //+A fecha desde se agregan los meses indicados (cuotas)
                    var dNewDate = new Date(dCurrDate.getFullYear(), dCurrDate.getMonth() + parseInt(lintQuot, 10), dCurrDate.getDate())

                    //+Llamada a Procedimiento 
                    insDefValues("insValDate_End", "&sCertype=" + '<%=Session("sCertype")%>' + "&nBranch=" + '<%=Session("nBranch")%>' + "&nProduct=" + '<%=Session("nProduct")%>' + "&nPolicy=" + '<%=Session("nPolicy")%>' + "&nCertif=" + '<%=Session("nCertif")%>' + "&dEffecdate=" + '<%=Session("dEffecdate")%>' + "&dNewDate=" + dtmToStr(dNewDate), '/VTimeNet/Policy/PolicySeq');
                }
            }
    }

}

//%strToDtm: Transforma un fecha en un objeto Date()
//-------------------------------------------------------------------------------------------
function strToDtm(strDate, strFormat, strSep){
//-------------------------------------------------------------------------------------------
    var lposDay=0, lposMonth=0, lposYear=0
    var arrDateFormatPart = strFormat.split(strSep)
    
    for (var i=0;i<arrDateFormatPart.length;){
        switch(arrDateFormatPart[i]){
            case 'd':
            case 'dd':
                lposDay = i;
                break;
                
            case 'm':
            case 'mm':
                lposMonth = i;
                break;
            
            case 'yy':
            case 'yyyy':
                lposYear = i;
                break;            
            
        }
        i+=1;
    }
    var arrDatePart = strDate.split(strSep)

//+Se crea fecha. Se usa la base (10) al usar parseInt para qie transforme correctamente 
//+lo valores que comienzan con '0'. De lo contrario, al comenzar con '0' asume base 8
//+por lo que valores como '09' con transformados en el valor 0
    var ldtmRet = new Date(parseInt(arrDatePart[lposYear],10), parseInt(arrDatePart[lposMonth],10)-1, parseInt(arrDatePart[lposDay],10));
    
    return ldtmRet
}

//% dtmToStr: Retorna valor de objeto fecha en una cadena con formato
//---------------------------------------------------------------------------------
function dtmToStr(dDate){
//---------------------------------------------------------------------------------
    var lstrRet = new String();
    var lintDay
    var lintMonth
    
    lintDay = dDate.getDate()
    lintMonth = dDate.getMonth() + 1
    
    if (lintDay < 10)
    {
        lstrRet += '0'
    }
    
    lstrRet += lintDay + '/'
    
    if (lintMonth < 10)
    {
        lstrRet += '0'
    }
    lstrRet += lintMonth + '/' + dDate.getFullYear();
    
    return lstrRet
}
</SCRIPT>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="FRMVI701" ACTION="valPolicySeq.aspx?nMainAction=301&nHolder=1">
<%
Response.Write(mobjValues.ShowWindowsName("VI701", Request.QueryString.Item("sWindowDescript")))
mclsLife = New ePolicy.Life
Call insPreVI001()
%>
    <TABLE WIDTH="100%">

    <% '+ Si no es la poliza matriz se muestran todos los campos
        If Not blnPolicy Then
            %>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeGroupCaption") %></LABEL></TD>
			<TD><%
With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbeGroup", "tabGroups", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsLife.nGroup), True,  ,  ,  ,  ,  , Not mblnFoundGroups, 5, GetLocalResourceObject("cbeGroupToolTip")))
%>
            </TD>	          	   
			<TD WIDTH=10%>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeSituationCaption") %></LABEL></TD>
			<TD><%
With mobjValues.Parameters
	.Add("sCertype", Session("sCertype"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", Session("nBranch"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", Session("nProduct"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nPolicy", Session("nPolicy"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("cbeSituation", "tabSituation", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLife.nSituation), True,  ,  ,  ,  ,  , Not mblnFoundSituation, 5, GetLocalResourceObject("cbeSituationToolTip")))
%>
			</TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tctCreditnumCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctCreditnum", 20, mclsLife.sCreditnum, , GetLocalResourceObject("tctCreditnumToolTip"), , , , , blnPolicy)%></TD> 
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCred_proCaption") %></LABEL></TD>
			<TD><%mobjValues.BlankPosition = 0
			        Response.Write(mobjValues.PossiblesValues("cbeCred_pro", "Table5590", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLife.nCred_pro), , , , , , , , 5, GetLocalResourceObject("cbeCred_proToolTip")))
%> </TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnQ_QuotCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnQ_Quot", 5, CStr(mclsLife.nQ_Quot), True , GetLocalResourceObject("tcnQ_QuotToolTip"),  False, 0,  ,  ,  , "insChangeField(this)")%> </TD>
			<TD COLSPAN=3>&nbsp;</TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdInit_creCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdInit_cre", CStr(mclsLife.dInit_cre), , GetLocalResourceObject("tcdInit_creToolTip"), , , , "insChangeField(this)", blnPolicy)%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEnd_creCaption") %></LABEL></TD>
            <TD><%= mobjValues.DateControl("tcdEnd_cre", CStr(mclsLife.dEnd_cre), , GetLocalResourceObject("tcdEnd_creToolTip"), , , , "insChangeField(this)", blnPolicy)%> </TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAmount_creCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmount_cre", 18, CStr(mclsLife.nAmount_cre), , GetLocalResourceObject("tcnAmount_creToolTip"), True, 6, , , , , blnPolicy)%></TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnAmount_actCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnAmount_act", 18, CStr(mclsLife.nAmount_act), , GetLocalResourceObject("tcnAmount_actToolTip"), True, 6, , , , , blnPolicy)%></TD>
        </TR>
<% End If %>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeCurren_creCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCurren_cre", "Table11", eFunctions.Values.eValuesType.clngComboType, CStr(mclsLife.nCurren_cre),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCurren_creToolTip"))%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=LABEL1><%= GetLocalResourceObject("tcnRateDesgCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnRateDesg", 9, CStr(mclsLife.nRateDesg),  , GetLocalResourceObject("tcnRateDesgToolTip"), True, 6)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeTypPremiumCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeTypPremium", "Table5560", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsLife.nTyppremium),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeTypPremiumToolTip"))%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=LABEL4><%= GetLocalResourceObject("tcnPremiumCaption")%></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnPremium", 18, CStr(mclsLife.nPremium_ca), , GetLocalResourceObject("tcnPremiumToolTip"), True, 6)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=LABEL3><%= GetLocalResourceObject("cbeCalcapitalCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeCalcapital", "Table5560", eFunctions.Values.eValuesType.clngWindowType, CStr(mclsLife.nCalcapital),  ,  ,  ,  ,  ,  ,  , 5, GetLocalResourceObject("cbeCalcapitalToolTip"))%> </TD>
			<TD>&nbsp;</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcnCapitalmaxCaption") %></LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnCapitalmax", 18, CStr(mclsLife.nCapitalMax),  , GetLocalResourceObject("tcnCapitalmaxToolTip"), True, 6)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=LABEL2><%= GetLocalResourceObject("tctAccnumCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctAccnum", 20, mclsLife.sAccnum,  , GetLocalResourceObject("tctAccnumToolTip"),  ,  ,  ,  , Session("nCertif") = 0)%></TD> 
<!--			<TD COLSPAN=2><%=mobjValues.CheckControl("chkRepPrintCov", GetLocalResourceObject("chkRepPrintCovCaption"), mclspolicy.sRepPrintCov, "1",  , Session("nCertif") <> 0,  , "")%></TD>-->
			<TD>&nbsp;</TD>
			<TD colspan=3>&nbsp;</TD>
        </TR>        
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
'+ libera las variables 
mobjValues = Nothing
mobjMenu = Nothing
mclsLife = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.44.16
Call mobjNetFrameWork.FinishPage("VI701")
mobjNetFrameWork = Nothing

'^End Footer Block VisualTimer%>




