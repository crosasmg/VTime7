<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eOptionSystem" %>
<%@ Import namespace="ePolicy" %>

<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo de errores
Dim mobjError As eFunctions.Errors
'- Objeto para el manejo de menues    
Dim mobjMenu As eFunctions.Menues
    Dim msDescadd1 As String
    Dim msDescadd2 As String
    Dim msDescadd3 As String

    Dim msRectype1 As String
    Dim msRectype2 As String
    Dim msRectype3, msRectypeAux As String
    
    Dim msDescaddvalid1 As Boolean
    Dim msDescaddvalid2 As Boolean
    Dim msDescaddvalid3 As Boolean
    
 
    '---------------------------------------------------------------------------------------------
    Private Sub insPreCA069()
        '---------------------------------------------------------------------------------------------
        Dim mobjCertificat As ePolicy.Certificat
        mobjCertificat = New ePolicy.Certificat
    
        Dim lintIndex As Integer
	    
        msDescaddvalid1 = True
        msDescaddvalid2 = True
        msDescaddvalid3 = True
        
       
        If mobjCertificat.Load_insPreCA069(Session("sCertype"), _
                                               mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdInteger), _
                                              mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdInteger), _
                                           mobjValues.StringToType(Session("nPolicy"), eFunctions.Values.eTypeData.etdLong), _
                                             mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdLong), _
                                            mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then

            With mobjCertificat.marrCert_Address
                If .Length >= 0 Then
            
                    For lintIndex = 0 To .Length - 1
                        msRectypeAux = .ElementAt(lintIndex).sRectypeAux
                        If .ElementAt(lintIndex).sRectype = "1" Then
                            msDescadd1 = .ElementAt(lintIndex).sDescadd
                            msDescaddvalid1 = False
                            If .ElementAt(lintIndex).sRectypeAux = .ElementAt(lintIndex).sRectype Then
                                msRectype1 = 1
                                msRectype2 = 2
                                msRectype3 = 2
                            End If
                        ElseIf .ElementAt(lintIndex).sRectype = "2" Then
                            msDescadd2 = .ElementAt(lintIndex).sDescadd
                            msDescaddvalid2 = False
                            If .ElementAt(lintIndex).sRectypeAux = .ElementAt(lintIndex).sRectype Then
                                msRectype1 = 2
                                msRectype2 = 1
                                msRectype3 = 2
                            End If
                        ElseIf .ElementAt(lintIndex).sRectype = "3" Then
                            msDescadd3 = .ElementAt(lintIndex).sDescadd
                            msDescaddvalid3 = False
                            If .ElementAt(lintIndex).sRectypeAux = .ElementAt(lintIndex).sRectype Then
                                msRectype1 = 2
                                msRectype2 = 2
                                msRectype3 = 1
                            End If
                        End If
                    Next
                End If
            
            End With
        End If

        mobjCertificat = Nothing
    
    End Sub
</script>
<%Response.Expires = -1441
    Response.CacheControl = "private"

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjError = New eFunctions.Errors
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjError.sSessionID = Session.SessionID
mobjError.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = Session("bQuery")
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


<SCRIPT>
    //+ Variable para el control de versiones
    document.VssVersion = "$$Revision: 3 $|$$Date: 15/10/03 16:49 $|$$Author: Nvaplat61 $"

    function changevalue(svalue) {
        self.document.forms[0].hddsRecType.value = svalue;  }


</SCRIPT>
    <%=mobjValues.StyleSheet()%>
    <%Response.Write(mobjMenu.setZone(2, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
<FORM METHOD="post" ID="FORM" NAME="frmCA069" ACTION="valPolicySeq.aspx?X=1" >
<%
    Call insPreCA069()
    %>
    <TABLE WIDTH="100%">
        <TR>
     	    <TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr1Caption"), msRectype1, "1", "changevalue(this.value)", msDescaddvalid1)%> </TD>
                        <TD COLSPAN="2"><%= mobjValues.TextControl("Comercial", 60, msDescadd1, , , , , , , True)%> </TD>
         </TR>
          <TR> 
            <TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr2Caption"), msRectype2, "2", "changevalue(this.value)", msDescaddvalid2)%> </TD>
                        <TD COLSPAN="2"><%= mobjValues.TextControl("Particular", 60, msDescadd2, , , , , , , True)%> </TD>
         </TR>
           <TR>  
            <TD COLSPAN="2"><%= mobjValues.OptionControl(0, "optDirTyp", GetLocalResourceObject("optDirTyp_CStr3Caption"), msRectype3, "3", "changevalue(this.value)", msDescaddvalid3)%> </TD>
                        <TD COLSPAN="2"><%= mobjValues.TextControl("Otros", 60, msDescadd3, , , , , , , True)%> </TD>
             </TR>

             <%= mobjValues.HiddenControl("hddsRecType", msRectypeAux)%>
    </TABLE> 
    <%
mobjError = Nothing
mobjValues = Nothing
%>
</FORM>
</BODY>
<HTML>




